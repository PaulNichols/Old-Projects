using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using System.Windows.Forms;
using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Security;
using HBOS.FS.AMP.UPD.WinUI.Classes;
using HBOS.FS.AMP.UPD.WinUI.Forms;
using HBOS.FS.AMP.UPD.WinUI.Helpers;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI
{
	/// <summary>
	/// Summary description for UPDApplication.
	/// </summary>
	public class UPDApplication
	{
		// Our main UI form
		private static Main mainForm;

		#region Application Interface Entry Point

		/// <summary>
		/// The main entry point into the Application Interface.
		/// </summary>
		[STAThread]
		public static void Main()
		{
			T.E();

			bool ownsAppLock;
			//NOTE: Prepending the unique mutex name Local\ and Global\ will address needing to keep the
			//application to a single instance per user session versus globally in whole OS.
			//That said this, isn't clearly documented in the .NET API because they don't mention
			//that System.Threading.Mutex is a wrapper around the Win32 api functions
			//CreateMutex and OpenMutex, so I don't make any guarantees about it.
			Mutex singleApp = new Mutex(true, typeof (Main).FullName, out ownsAppLock);

			//Check to ownership of the Mutex was granted. If it is not obtained, this means the mutex
			//is already owned by another running instance of SingleInstanceApplication
			if (!ownsAppLock)
			{
				MessageBox.Show("There is already an instance of UPD running");
				switchToExistingInstance();
				return;
			}
			else
			{
				// Add trace statements
				T.Log("Application entry point");

				// Make sure we capture any unhandled exceptions
				T.Log("Bind Exception Handler");
				Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);

				if (PerformChecks(true))
				{
					T.Log("Authentication Checks");
					UPDIdentity updID = new UPDIdentity(GlobalRegistry.ConnectionString);

					//add a handler to reload the application if the certain application settings values change
					GlobalRegistry.AppSettings.ApplicationSettingsChanged -= new EventHandler(SettingsChanged);
					GlobalRegistry.AppSettings.ApplicationSettingsChanged += new EventHandler(SettingsChanged);
					GlobalRegistry.UserSettingsChanged -= new EventHandler(SettingsChanged);
					GlobalRegistry.UserSettingsChanged += new EventHandler(SettingsChanged);

					if (!updID.IsAuthenticated)
					{
						MessageBoxHelper.Show("AuthenticationErrorBody", "AuthenticationErrorTitle", MessageBoxButtons.OK, MessageBoxIcon.Stop);
					}
					else
					{
						// Get last working company for the user, if any
						CompanyDetails companyDetails;
						try
						{
							companyDetails = UserController.GetLastCompany(GlobalRegistry.ConnectionString);

							if (companyDetails == null)
							{
								MessageBoxHelper.Show("PermissionsErrorBody", "PermissionsErrorTitle", MessageBoxButtons.OK, MessageBoxIcon.Stop);
							}
							else
							{
								getCachedData(GlobalRegistry.ConnectionString, companyDetails.CompanyCode);

								enterMessageLoop(updID, companyDetails);
							}
						}
						catch (SqlException ex)
						{
							handleSqlException(ex, true);
						}
					}

				}

				//If this isn't here in a Release build the Mutex will go out of scope and the
				//mutex will actually be disposed of, resulting in the single instance functionality not working.
				GC.KeepAlive(singleApp);
			}

			T.X();
		}

		internal static bool PerformChecks(bool exitApp)
		{
			bool returnValue = true;
			if (isDatabaseConnectionValid(exitApp))
			{
				if (!isClientVersionValid())
				{
					MessageBoxHelper.Show("InvalidClientVersionBody", "InvalidClientVersionTitle", MessageBoxButtons.OK, MessageBoxIcon.Stop);
					returnValue = false;
				}
				else if (!validateDesktopDateTime())
				{
					MessageBoxHelper.Show("TimeErrorBody", "TimeErrorTitle", MessageBoxButtons.OK, MessageBoxIcon.Stop);
					returnValue = false;
				}

			}
			else
			{
				returnValue = false;
			}
			return returnValue;
		}

		private static void SettingsChanged(object sender, EventArgs e)
		{
			UPDApplication.mainForm.ChangedCompany = true;
			UPDApplication.mainForm.Close();

			refreshPrincipleAndIdentity();

			getCachedData(GlobalRegistry.ConnectionString, ((UPDPrincipal) Thread.CurrentPrincipal).CompanyCode);
		}

		private static void refreshPrincipleAndIdentity()
		{
			//refresh the principle so collections such as Roles reflect settings on
			//the new DB
			//UPDIdentity currentID = (UPDIdentity) Thread.CurrentPrincipal.Identity;
			UPDIdentity newID = new UPDIdentity(GlobalRegistry.ConnectionString);
			UPDPrincipal currentPrinciple = (UPDPrincipal) Thread.CurrentPrincipal;
			UPDPrincipal newPrincipal = new UPDPrincipal(newID, currentPrinciple.CompanyCode,
			                                             currentPrinciple.CurrentCompanyValuationDateAndTime, currentPrinciple.NextCompanyValuationDateAndTime,
			                                             currentPrinciple.PreviousCompanyValuationDateAndTime);
			Thread.CurrentPrincipal = newPrincipal;
		}

		private static void getCachedData(string connectionString, string companyCode)
		{
			FundGroupController.FlushFundGroups();

			FundGroupController fundGroupController = new FundGroupController(connectionString);
			fundGroupController.LoadFundGroupLookupsByCompany(companyCode);

			FundController fundController = new FundController(connectionString);
			fundController.ClearHolidays();
			fundController.LoadHolidays();

			LookupController lookupController = new LookupController();
			lookupController.FlushCountries();
			lookupController.LoadCountries(connectionString);

			lookupController = new LookupController();
			lookupController.FlushCurrencies();
			lookupController.LoadCurrencies(connectionString);

		}

		private static bool isClientVersionValid()
		{
			T.E();
			bool result = VersionController.VerifyVersion(GlobalRegistry.ConnectionString, GlobalRegistry.ClientVersion);
			T.X();
			return result;
		}

		private static void enterMessageLoop(UPDIdentity updID, CompanyDetails lastCompany)
		{
			T.E();
			try
			{
				T.Log("Principal Setup");
				AppDomain.CurrentDomain.SetThreadPrincipal(new UPDPrincipal(updID, lastCompany.CompanyCode,
				                                                            lastCompany.CompanyValuationDate, lastCompany.NextValuationDate,
				                                                            lastCompany.PreviousValuationDate));

				// Display main user entry form...
				bool changedCompany = true;
				int x, y, height, width;
				x = y = height = width = -1;

				while (changedCompany)
				{
					// Create a static instance of the main form
					T.Log("Load Main UI Form");
					UPDApplication.mainForm = new Main();
					if (x != -1)
					{
						positionMainForm(x, y, height, width);
					}
					else
					{
						showSplashScreen();
					}
					// Start the message loop on the form
					T.Log("Application.Run");


					UPDApplication.mainForm.PopulateDatabaseMenu();

					Application.Run(UPDApplication.mainForm);

					// The form was closed, lets see why
					changedCompany = UPDApplication.mainForm.ChangedCompany;

					if (changedCompany)
					{
						T.Log("Company changed, close form and loop");
						// Save the position and size so we can set the new form under the new company
						// permissions to match.
						x = UPDApplication.mainForm.Left;
						y = UPDApplication.mainForm.Top;
						height = UPDApplication.mainForm.Height;
						width = UPDApplication.mainForm.Width;
						// Close the old form and tell the GC it's done with.
						UPDApplication.mainForm.Close();
						UPDApplication.mainForm.Dispose();
					}
				}
			}
			catch (SecurityException securityEx)
			{
				GUIExceptionHelper.LogAndDisplayException("PermissionsErrorBody", "PermissionsErrorTitle", securityEx);
			}
			catch (SqlException sqlEx)
			{
				handleSqlException(sqlEx, true);
			}
			catch (Exception ex)
			{
				GUIExceptionHelper.LogAndDisplayException("UnexpectedErrorBody", "UnexpectedErrorTitle", ex, ex.Message);
			}
			finally
			{
				T.X();
			}
		}

		private static void positionMainForm(int x, int y, int height, int width)
		{
			T.E();
			UPDApplication.mainForm.Show();
			UPDApplication.mainForm.Left = x;
			UPDApplication.mainForm.Top = y;
			UPDApplication.mainForm.Height = height;
			UPDApplication.mainForm.Width = width;
			T.X();
		}

		private static void showSplashScreen()
		{
			T.E();
#if ( DEBUG )

#else
			SplashScreen.ShowSplashScreen();
			Application.DoEvents();
			SplashScreen.SetStatus("Loading Application ...", true);
			Thread.Sleep(1000);
			SplashScreen.SetStatus("Loading Application ...", true);
			Thread.Sleep(1000);
			SplashScreen.SetStatus("Loading Application ...", true);
			Thread.Sleep(500);
#endif
			T.X();
		}

		private static bool isDatabaseConnectionValid(bool exitApp)
		{
			T.E();
			bool canConnect = false;
			T.Log("SQL Connection Checks");
			using (SqlConnection connection = new SqlConnection(GlobalRegistry.ConnectionString))
			{
				T.Log("Attempting connection cycle");
				try
				{
					connection.Open();
					T.Log("Connection Opened");
					connection.Close();
					T.Log("Connection Closed");
					canConnect = true;

				}
				catch (SqlException exception)
				{
					handleSqlException(exception, exitApp);
				}
				catch (Exception exception)
				{
					GUIExceptionHelper.LogAndDisplayException("UnexpectedErrorBody", "UnexpectedErrorTitle", exception, exception.Message);
					if (exitApp) UPDApplication.ExitApplication();
				}
			}
			T.Log("Completed SQL Connection checks");
			T.X();
			return canConnect;
		}

		private static void handleSqlException(SqlException exception, bool exitApp)
		{
			T.Log("Caught an SQL exception", exception.Errors[0].Message);
			switch (exception.Errors[0].Number)
			{
				case (int) DatabaseError.SQLServerDoesNotExist:
					GUIExceptionHelper.LogAndDisplayException("SqlServerDoesNotExist", "DatabaseErrorTitle", exception);
					if (exitApp) UPDApplication.ExitApplication();
					break;
				case (int) DatabaseError.LoginFailed:
					GUIExceptionHelper.LogAndDisplayException("LoginFailed", "DatabaseErrorTitle", exception);
					UPDApplication.ExitApplication();
					break;
				case (int) DatabaseError.NoDatabaseAccess:
					GUIExceptionHelper.LogAndDisplayException("NoDatabaseAccess", "DatabaseErrorTitle", exception);
					if (exitApp) UPDApplication.ExitApplication();
					break;
				case (int) DatabaseError.LoginFailedNotAssociatedWithTrustedConnection:
					GUIExceptionHelper.LogAndDisplayException("LoginFailedNotAssociatedWithTrustedConnection", "LoginFailedNotAssociatedWithTrustedConnectionTitle", exception);
					if (exitApp) UPDApplication.ExitApplication();
					break;
				default:
					GUIExceptionHelper.LogAndDisplayException("UnexpectedDatabaseErrorBody", "UnexpectedDatabaseErrorTitle", exception, exception.Message);
					if (exitApp) UPDApplication.ExitApplication();
					break;
			}
		}

		#endregion

		#region Application Exit Point

		/// <summary>
		/// Main exit point for the application interface
		/// </summary>
		public static void ExitApplication()
		{
			//
			// TODO: Add NUnit stuff for the Application interface exit point
			//

			// Add trace statements
			T.E("Application exit point");

			//
			// TODO: Add any application tidy code here
			//

			if (UPDApplication.mainForm != null)
			{
				UPDApplication.mainForm.Close();
				UPDApplication.mainForm.Dispose();
			}

			// SJR - Not sure how the Application.Exit() works, will any
			//       code be executed after the call?
			T.X();
			Application.Exit();

		}

		#endregion

		#region Instance Handler

		/// <summary>
		/// Get the process the application is running on
		/// </summary>
		/// <returns></returns>
		private static Process runningInstance()
		{
			T.E();

			try
			{
				Process current = Process.GetCurrentProcess();
				Process[] processes = Process.GetProcessesByName(current.ProcessName);

				//Loop through the running processes in with the same name
				foreach (Process process in processes)
				{
					//Ignore the current process
					if (process.Id != current.Id)
					{
						//Make sure that the process is running from the exe file.
						if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") ==
							current.MainModule.FileName)
						{
							//Return the other process instance.
							return process;
						}
					}
				}

				//No other instance was found, return null.
				return null;
			}
			finally
			{
				T.X();
			}
		}


		/// <summary>
		/// Check for an existing instance and if one exists, switch to it
		/// </summary>
		/// <returns>True - instance switched.  False - no existing instance found</returns>
		private static bool switchToExistingInstance()
		{
			T.E();
			bool switched = false;

			try
			{
				Process instance = runningInstance();
				if (instance != null)
				{
					switched = true;
					//Make sure the window is not minimized or maximized
					ShowWindowAsync(instance.MainWindowHandle, WS_SHOWNORMAL);

					//Set the real intance to foreground window
					SetForegroundWindow(instance.MainWindowHandle);
				}

			}
			finally
			{
				T.X();
			}

			return switched;
		}

		[DllImport("User32.dll")]
		private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

		/// <summary>
		/// API call to bring specified window to the front
		/// </summary>
		/// <param name="hWnd"></param>
		/// <returns></returns>
		[DllImport("User32.dll")]
		public static extern bool SetForegroundWindow(IntPtr hWnd);

		private const int WS_SHOWNORMAL = 1;

		#endregion

		#region Private Helper Methods

		/// <summary>
		/// Handle a thread exception
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			T.E();

			try
			{
				GUIExceptionHelper.LogAndDisplayException("UnexpectedErrorBody", "UnexpectedErrorTitle", e.Exception, e.Exception.Message);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Validate the Desktop Date Time
		/// </summary>
		/// <returns>Valid or Invalid indicator</returns>
		private static bool validateDesktopDateTime()
		{
			T.E();
			T.Log("Date/Time checks");
			try
			{
				bool returnValue = true;

				// Check Date/Time difference between server and client
				DateTime sqlServerDate = SystemController.GetServerDateTime(GlobalRegistry.ConnectionString);


				TimeSpan difference = DateTime.Now.Subtract(sqlServerDate);
				if (difference.TotalHours > 1.5 || difference.TotalHours < -1.5)
				{
					returnValue = false;
				}

				return returnValue;
			}
			finally
			{
				T.X();
			}
		}

		#endregion
	}
}