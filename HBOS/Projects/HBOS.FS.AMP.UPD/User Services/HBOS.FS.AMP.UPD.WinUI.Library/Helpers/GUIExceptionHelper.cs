using System;
using HBOS.FS.Common.ExceptionManagement;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.Helpers
{
	/// <summary>
	/// This class is designed to provide a simple way to display certain client 
	/// exception messages.
	/// </summary>
	public class GUIExceptionHelper
	{
		#region Local variables

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor.
		/// </summary>
		public GUIExceptionHelper()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#endregion

		#region Public methods

		//
		// TODO: Look at handling delete errors differently.
		//

		/// <summary>
		/// Log the exception to the client event log and display the correct save exception
		/// message back to the user.
		/// </summary>
		/// <param name="typeName">The object type being actioned, such as User; Fund; Asset Fund.  This is the text taht will be displayed within the message.</param>
		/// <param name="currentException">The exception who's full details will be displayed if dev style UI required.</param>
		public static void LogAndDisplaySaveException(string typeName, Exception currentException)
		{
			T.E();

			logException(currentException);

			// Display the user friendly message to the client  
			try
			{
				displayMessage(typeName, currentException, "Save");
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Log the exception to the client event log and display the correct load exception
		/// message back to the user.
		/// </summary>
		/// <param name="typeName">The object type being actioned, such as User; Fund; Asset Fund.  This is the text taht will be displayed within the message.</param>
		/// <param name="currentException">The exception who's full details will be displayed if dev style UI required.</param>
		public static void LogAndDisplayLoadException(string typeName, Exception currentException)
		{
			T.E();

			logException(currentException);

			// Display the user friendly message to the client  
			try
			{
				displayMessage(typeName, currentException, "Load");
			}
			finally
			{
				T.X();
			}
		}


		/// <summary>
		/// Displays an exception using specific resource ids provided, rather than use defaults based on an action
		/// only allows bodytextargs, create a new method for this class and for messageboxhelper if caption args also required
		/// </summary>
		/// <param name="resourceBody"></param>
		/// <param name="resourceTitle"></param>
		/// <param name="currentException"></param>
		/// <param name="bodyTextArgs"></param>
		public static void LogAndDisplayException(string resourceBody, string resourceTitle, Exception currentException, params string[] bodyTextArgs)
		{
			T.E();

			logException(currentException);

			// Display the user friendly message to the client  
			try
			{
				T.Log("Display the user friendly message to the client");
				MessageBoxHelper.ShowError(resourceBody, resourceTitle, currentException, bodyTextArgs);
			}
			finally
			{
				T.X();
			}
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Display the user friendly message to the client
		/// </summary>
		/// <param name="typeName">The object type being actioned, such as User; Fund; Asset Fund.  This is the text taht will be displayed within the message.</param>
		/// <param name="ex">The exception who's full details will be displayed if dev style UI required.</param>
		/// <param name="action">Current action being executed</param>
		private static void displayMessage(string typeName, Exception ex, string action)
		{
			string resourceTitle = string.Empty;
			string resourceBody = string.Empty;
			T.E();
			// Display the user friendly message to the client  
			try
			{
				// Figure out the resource item to display
				T.Log("Work out the exception type and assign resource item category strings");
				loadResourceTitleAndBodyItems(ex.GetType().ToString(), action, ref resourceTitle, ref resourceBody);
				T.Log("Display the user friendly message to the client");
				MessageBoxHelper.ShowError(resourceBody, resourceTitle, ex, typeName, typeName);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Work out the resource text for the specified exception.
		/// </summary>
		/// <param name="exceptionType">The exception type being raised.</param>
		/// <param name="action">Client action being executed such as Save; Load; Delete.</param>
		/// <param name="titleItem">Resource title item to be populated.</param>
		/// <param name="bodyItem">Resource body text item to be populated.</param>
		private static void loadResourceTitleAndBodyItems(string exceptionType,
		                                                  string action, ref string titleItem, ref string bodyItem)
		{
			T.E();
			const string exceptionNamespace = "HBOS.FS.AMP.UPD.Exceptions.";

			//
			// TODO: Add new exception types to list as they are built
			//

			// Work out the exception type so we can pick out the correct 
			// resource body text then figure out the resource item to display
			try
			{
				T.FurtherInfo("Exception type = " + exceptionType);
				T.FurtherInfo("Action = " + action);

				// Top level switch handles the exception type.
				// The inner switch will handle the action type and the 
				// work out the correct resource items to use.
				switch (exceptionType)
				{
						// Data has changed sinced the client data was loaded
					case exceptionNamespace + "ConcurrencyViolationException":
						switch (action)
						{
							default: // Save
								titleItem = "GenericUnableToSaveTitle";
								bodyItem = "GenericChangedBody";
								break;
						}
						break;

						// A duplicate item already exists wihin the database
					case exceptionNamespace + "ConstraintViolationException":
						switch (action)
						{
							case "Load":
								titleItem = "GenericUnableToLoadTitle";
								bodyItem = "DatabaseError";
								break;
							default: // Save
								titleItem = "GenericDuplicateTitle";
								bodyItem = "GenericDuplicateBody";
								break;
						}
						break;

						// Invalid stored procedure value
					case exceptionNamespace + "InvalidSqlParameterException":
						switch (action)
						{
							case "Load":
								titleItem = "GenericUnableToLoadTitle";
								bodyItem = "DatabaseError";
								break;
							default: // Save
								titleItem = "GenericUnableToSaveTitle";
								bodyItem = "DatabaseError";
								break;
						}
						break;

						// General database error reported
					case exceptionNamespace + "DatabaseException":
						switch (action)
						{
							case "Load":
								titleItem = "GenericUnableToLoadTitle";
								bodyItem = "DatabaseError";
								break;
							default: // Save
								titleItem = "GenericUnableToSaveTitle";
								bodyItem = "DatabaseError";
								break;
						}
						break;

						// If all else fails, report everyone's favorite system error message.
					default: //SystemError
						switch (action)
						{
							case "Load":
								titleItem = "GenericUnableToLoadTitle";
								bodyItem = "GenericUnableToLoadBody";
								break;
							default: // Save
								titleItem = "GenericUnableToSaveTitle";
								bodyItem = "SystemError";
								break;
						}
						break;
				}

				// Some additional traing information
				T.FurtherInfo("titleItem = " + titleItem);
				T.FurtherInfo("bodyItem = " + bodyItem);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Add the current exception into client event log and into the tracing tool
		/// </summary>
		/// <param name="ex">Exception object</param>
		private static void logException(Exception ex)
		{
			// Log the exception. This reduces the code within the calling methods.
			T.E();
			try
			{
				T.Log("Log the exception");
				T.DumpException(ex);
				ExceptionManager.Publish(ex);
			}
			finally
			{
				T.X();
			}
		}

		#endregion
	}
}