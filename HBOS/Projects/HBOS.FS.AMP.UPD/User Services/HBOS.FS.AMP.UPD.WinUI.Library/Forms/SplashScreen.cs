using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using HBOS.FS.AMP.UPD.WinUI.Classes;
using Microsoft.Win32;
using Timer = System.Windows.Forms.Timer;

namespace HBOS.FS.AMP.UPD.WinUI.Forms
{
	/// <summary>
	/// Summary description for SplashScreen.
	/// </summary>
	public class SplashScreen : Form
	{
		// Threading
		private static SplashScreen ms_frmSplash;
		private static Thread ms_oThread;

		// Fade in and out.
		private double m_dblOpacityIncrement = .05;
		private double m_dblOpacityDecrement = .08;
		private const int TIMER_INTERVAL = 50;

		// Status and progress bar
		private static string ms_sStatus;
		private double m_dblCompletionFraction = 0;
		private Rectangle m_rProgress;

		// Progress smoothing
		private double m_dblLastCompletionFraction = 0.0;
		private double m_dblPBIncrementPerTimerInterval = .015;

		// Self-calibration support
		private bool m_bFirstLaunch = false;
		private DateTime m_dtStart;
		private bool m_bDTSet = false;
		private int m_iIndex = 1;
		private int m_iActualTicks = 0;
		private ArrayList m_alPreviousCompletionFraction;
		private ArrayList m_alActualTimes = new ArrayList();
/*
		private const string REG_KEY_INITIALIZATION = "Initialization";
*/
		private const string REGVALUE_PB_MILISECOND_INCREMENT = "Increment";
		private const string REGVALUE_PB_PERCENTS = "Percents";

		private Label lblStatus;
		private Label lblTimeRemaining;
		private Timer timer1;
		private Panel pnlStatus;
		private Label lblAssemblyCompany;
		private Label lblAssemblyTitle;
		private Label lblAssemblyCopyright;
		private Label lblVersion;
		private IContainer components;

		/// <summary>
		/// Constructor
		/// </summary>
		public SplashScreen()
		{
			InitializeComponent();
			this.Opacity = .00;
			timer1.Interval = TIMER_INTERVAL;
			timer1.Start();
			this.ClientSize = this.BackgroundImage.Size;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SplashScreen));
			this.lblStatus = new System.Windows.Forms.Label();
			this.pnlStatus = new System.Windows.Forms.Panel();
			this.lblTimeRemaining = new System.Windows.Forms.Label();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.lblAssemblyTitle = new System.Windows.Forms.Label();
			this.lblAssemblyCopyright = new System.Windows.Forms.Label();
			this.lblVersion = new System.Windows.Forms.Label();
			this.lblAssemblyCompany = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblStatus
			// 
			this.lblStatus.BackColor = System.Drawing.Color.Transparent;
			this.lblStatus.ForeColor = System.Drawing.Color.Black;
			this.lblStatus.Location = new System.Drawing.Point(90, 137);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(330, 16);
			this.lblStatus.TabIndex = 0;
			this.lblStatus.Visible = false;
			this.lblStatus.DoubleClick += new System.EventHandler(this.SplashScreen_DoubleClick);
			// 
			// pnlStatus
			// 
			this.pnlStatus.BackColor = System.Drawing.Color.Transparent;
			this.pnlStatus.Location = new System.Drawing.Point(88, 108);
			this.pnlStatus.Name = "pnlStatus";
			this.pnlStatus.Size = new System.Drawing.Size(330, 28);
			this.pnlStatus.TabIndex = 1;
			this.pnlStatus.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlStatus_Paint);
			this.pnlStatus.DoubleClick += new System.EventHandler(this.SplashScreen_DoubleClick);
			// 
			// lblTimeRemaining
			// 
			this.lblTimeRemaining.BackColor = System.Drawing.Color.Transparent;
			this.lblTimeRemaining.ForeColor = System.Drawing.Color.Black;
			this.lblTimeRemaining.Location = new System.Drawing.Point(88, 193);
			this.lblTimeRemaining.Name = "lblTimeRemaining";
			this.lblTimeRemaining.Size = new System.Drawing.Size(148, 38);
			this.lblTimeRemaining.TabIndex = 2;
			this.lblTimeRemaining.Text = "Time remaining";
			this.lblTimeRemaining.Visible = false;
			this.lblTimeRemaining.DoubleClick += new System.EventHandler(this.SplashScreen_DoubleClick);
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// lblAssemblyTitle
			// 
			this.lblAssemblyTitle.BackColor = System.Drawing.Color.Transparent;
			this.lblAssemblyTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblAssemblyTitle.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.lblAssemblyTitle.Location = new System.Drawing.Point(36, 35);
			this.lblAssemblyTitle.Name = "lblAssemblyTitle";
			this.lblAssemblyTitle.Size = new System.Drawing.Size(343, 45);
			this.lblAssemblyTitle.TabIndex = 3;
			this.lblAssemblyTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblAssemblyCopyright
			// 
			this.lblAssemblyCopyright.AutoSize = true;
			this.lblAssemblyCopyright.BackColor = System.Drawing.Color.Transparent;
			this.lblAssemblyCopyright.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblAssemblyCopyright.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.lblAssemblyCopyright.Location = new System.Drawing.Point(101, 83);
			this.lblAssemblyCopyright.Name = "lblAssemblyCopyright";
			this.lblAssemblyCopyright.Size = new System.Drawing.Size(0, 18);
			this.lblAssemblyCopyright.TabIndex = 3;
			// 
			// lblVersion
			// 
			this.lblVersion.BackColor = System.Drawing.Color.Transparent;
			this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblVersion.Location = new System.Drawing.Point(7, 178);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(91, 34);
			this.lblVersion.TabIndex = 4;
			this.lblVersion.Text = "version";
			// 
			// lblAssemblyCompany
			// 
			this.lblAssemblyCompany.BackColor = System.Drawing.Color.Transparent;
			this.lblAssemblyCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblAssemblyCompany.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.lblAssemblyCompany.Location = new System.Drawing.Point(61, 3);
			this.lblAssemblyCompany.Name = "lblAssemblyCompany";
			this.lblAssemblyCompany.Size = new System.Drawing.Size(296, 23);
			this.lblAssemblyCompany.TabIndex = 3;
			this.lblAssemblyCompany.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// SplashScreen
			// 
			this.AutoScale = false;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
			this.BackColor = System.Drawing.Color.LightGray;
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = new System.Drawing.Size(419, 218);
			this.ControlBox = false;
			this.Controls.Add(this.lblVersion);
			this.Controls.Add(this.lblAssemblyTitle);
			this.Controls.Add(this.lblTimeRemaining);
			this.Controls.Add(this.pnlStatus);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.lblAssemblyCopyright);
			this.Controls.Add(this.lblAssemblyCompany);
			this.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(421, 220);
			this.MinimumSize = new System.Drawing.Size(421, 220);
			this.Name = "SplashScreen";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.TopMost = true;
			this.Load += new System.EventHandler(this.SplashScreen_Load);
			this.DoubleClick += new System.EventHandler(this.SplashScreen_DoubleClick);
			this.ResumeLayout(false);

		}

		#endregion

		// ************* Static Methods *************** //

		/// <summary>
		///  A static method to create the thread and 
		/// launch the SplashScreen.
		/// </summary>
		public static void ShowSplashScreen()
		{
			// Make sure it's only launched once.
			if (ms_frmSplash != null)
				return;
			ms_oThread = new Thread(new ThreadStart(SplashScreen.ShowForm));
			ms_oThread.IsBackground = true;
			ms_oThread.ApartmentState = ApartmentState.STA;
			ms_oThread.Start();
		}

		/// <summary>
		/// A property returning the splash screen instance
		/// </summary>
		public static SplashScreen SplashForm
		{
			get { return ms_frmSplash; }
		}

		// A private entry point for the thread.
		private static void ShowForm()
		{
			ms_frmSplash = new SplashScreen();
			Application.Run(ms_frmSplash);
		}

		/// <summary>
		/// A static method to close the SplashScreen
		/// </summary>
		public static void CloseForm()
		{
			if (ms_frmSplash != null && ms_frmSplash.IsDisposed == false)
			{
				// Make it start going away.
				ms_frmSplash.m_dblOpacityIncrement = - ms_frmSplash.m_dblOpacityDecrement;
			}
			ms_oThread = null; // we don't need these any more.
			ms_frmSplash = null;
		}

		/// <summary>
		/// A static method to set the status and update the reference.
		/// </summary>
		/// <param name="newStatus"></param>
		public static void SetStatus(string newStatus)
		{
			SetStatus(newStatus, true);
		}

		/// <summary>
		/// A static method to set the status and optionally update the reference.
		/// This is useful if you are in a section of code that has a variable
		/// set of status string updates.  In that case, don't set the reference.
		/// </summary>
		/// <param name="newStatus"></param>
		/// <param name="setReference"></param>
		public static void SetStatus(string newStatus, bool setReference)
		{
			ms_sStatus = newStatus;
			if (ms_frmSplash == null)
				return;
			if (setReference)
				ms_frmSplash.SetReferenceInternal();
		}

		/// <summary>
		/// Static method called from the initializing application to 
		/// give the splash screen reference points.  Not needed if
		/// you are using a lot of status strings.
		/// </summary>
		public static void SetReferencePoint()
		{
			if (ms_frmSplash == null)
				return;
			ms_frmSplash.SetReferenceInternal();

		}

		// ************ Private methods ************

		// Internal method for setting reference points.
		private void SetReferenceInternal()
		{
			if (m_bDTSet == false)
			{
				m_bDTSet = true;
				m_dtStart = DateTime.Now;
				ReadIncrements();
			}
			double dblMilliseconds = ElapsedMilliSeconds();
			m_alActualTimes.Add(dblMilliseconds);
			m_dblLastCompletionFraction = m_dblCompletionFraction;
			if (m_alPreviousCompletionFraction != null && m_iIndex < m_alPreviousCompletionFraction.Count)
				m_dblCompletionFraction = (double) m_alPreviousCompletionFraction[m_iIndex++];
			else
				m_dblCompletionFraction = (m_iIndex > 0) ? 1 : 0;
		}

		// Utility function to return elapsed Milliseconds since the 
		// SplashScreen was launched.
		private double ElapsedMilliSeconds()
		{
			TimeSpan ts = DateTime.Now - m_dtStart;
			return ts.TotalMilliseconds;
		}

		// Function to read the checkpoint intervals from the previous invocation of the
		// splashscreen from the registry.
		private void ReadIncrements()
		{
			string sPBIncrementPerTimerInterval = RegistryAccess.GetStringRegistryValue(REGVALUE_PB_MILISECOND_INCREMENT, "0.0015");
			double dblResult;

			if (Double.TryParse(sPBIncrementPerTimerInterval, NumberStyles.Float, NumberFormatInfo.InvariantInfo, out dblResult) == true)
				m_dblPBIncrementPerTimerInterval = dblResult;
			else
				m_dblPBIncrementPerTimerInterval = .0015;

			string sPBPreviousPctComplete = RegistryAccess.GetStringRegistryValue(REGVALUE_PB_PERCENTS, "");

			if (sPBPreviousPctComplete != "")
			{
				string[] aTimes = sPBPreviousPctComplete.Split(null);
				m_alPreviousCompletionFraction = new ArrayList();

				for (int i = 0; i < aTimes.Length; i++)
				{
					double dblVal;
					if (Double.TryParse(aTimes[i], NumberStyles.Float, NumberFormatInfo.InvariantInfo, out dblVal))
						m_alPreviousCompletionFraction.Add(dblVal);
					else
						m_alPreviousCompletionFraction.Add(1.0);
				}
			}
			else
			{
				m_bFirstLaunch = true;
				lblTimeRemaining.Text = "";
			}
		}

		// Method to store the intervals (in percent complete) from the current invocation of
		// the splash screen to the registry.
		private void StoreIncrements()
		{
			string sPercent = "";
			double dblElapsedMilliseconds = ElapsedMilliSeconds();
			for (int i = 0; i < m_alActualTimes.Count; i++)
				sPercent += ((double) m_alActualTimes[i]/dblElapsedMilliseconds).ToString("0.####", NumberFormatInfo.InvariantInfo) + " ";

			RegistryAccess.SetStringRegistryValue(REGVALUE_PB_PERCENTS, sPercent);

			m_dblPBIncrementPerTimerInterval = 1.0/(double) m_iActualTicks;
			RegistryAccess.SetStringRegistryValue(REGVALUE_PB_MILISECOND_INCREMENT, m_dblPBIncrementPerTimerInterval.ToString("#.000000", NumberFormatInfo.InvariantInfo));
		}

		//********* Event Handlers ************

		// Tick Event handler for the Timer control.  Handle fade in and fade out.  Also
		// handle the smoothed progress bar.
		private void timer1_Tick(object sender, EventArgs e)
		{
			lblStatus.Text = ms_sStatus;

			if (m_dblOpacityIncrement > 0)
			{
				m_iActualTicks++;
				if (this.Opacity < 1)
					this.Opacity += m_dblOpacityIncrement;
			}
			else
			{
				if (this.Opacity > 0)
					this.Opacity += m_dblOpacityIncrement;
				else
				{
					StoreIncrements();
					this.Close();
					Debug.WriteLine("Called this.Close()");
				}
			}
			if (m_bFirstLaunch == false && m_dblLastCompletionFraction < m_dblCompletionFraction)
			{
				m_dblLastCompletionFraction += m_dblPBIncrementPerTimerInterval;
				int width = (int) Math.Floor(pnlStatus.ClientRectangle.Width*m_dblLastCompletionFraction);
				int height = pnlStatus.ClientRectangle.Height;
				int x = pnlStatus.ClientRectangle.X;
				int y = pnlStatus.ClientRectangle.Y;
				if (width > 0 && height > 0)
				{
					m_rProgress = new Rectangle(x, y, width, height);
					pnlStatus.Invalidate(m_rProgress);
					int iSecondsLeft = 1 + (int) (TIMER_INTERVAL*((1.0 - m_dblLastCompletionFraction)/m_dblPBIncrementPerTimerInterval))/1000;
					if (iSecondsLeft == 1)
						lblTimeRemaining.Text = string.Format("1 second remaining");
					else
						lblTimeRemaining.Text = string.Format("{0} seconds remaining", iSecondsLeft);

				}
			}
		}

		// Paint the portion of the panel invalidated during the tick event.
		private void pnlStatus_Paint(object sender, PaintEventArgs e)
		{
			if (m_bFirstLaunch == false && e.ClipRectangle.Width > 0 && m_iActualTicks > 1)
			{
				LinearGradientBrush brBackground = new LinearGradientBrush(m_rProgress, Color.FromArgb(58, 96, 151), Color.FromArgb(181, 237, 254), LinearGradientMode.Horizontal);
				e.Graphics.FillRectangle(brBackground, m_rProgress);
			}
		}

		// Close the form if they double click on it.
		private void SplashScreen_DoubleClick(object sender, EventArgs e)
		{
			CloseForm();
		}

		private void SplashScreen_Load(object sender, EventArgs e)
		{
			Assembly myself = Assembly.GetEntryAssembly();
			object[] objAttributes = null;

//			T.Log("Binding version numbers and meta data");
			this.lblVersion.Text = "Version " + GlobalRegistry.ClientVersion.ToString();

			objAttributes = myself.GetCustomAttributes(typeof (AssemblyTitleAttribute), true);
			if (objAttributes.Length != 0)
			{
				string assemblyTitle = ((AssemblyTitleAttribute) objAttributes[0]).Title;
				this.lblAssemblyTitle.Text = assemblyTitle;
			}

			objAttributes = myself.GetCustomAttributes(typeof (AssemblyCopyrightAttribute), true);
			if (objAttributes.Length != 0)
			{
				string assemblyCopyright = ((AssemblyCopyrightAttribute) objAttributes[0]).Copyright;
				this.lblAssemblyCopyright.Text = assemblyCopyright;
			}

			objAttributes = myself.GetCustomAttributes(typeof (AssemblyCompanyAttribute), true);
			if (objAttributes.Length != 0)
			{
				string assemblyCompany = ((AssemblyCompanyAttribute) objAttributes[0]).Company;
				this.lblAssemblyCompany.Text = assemblyCompany;
			}


		}
	}

	/// <summary>
	/// A class for managing registry access.
	/// </summary>
	public class RegistryAccess
	{
		private const string SOFTWARE_KEY = "Software";
		private const string COMPANY_NAME = "MyCompany";
		private const string APPLICATION_NAME = "MyApplication";

		/// <summary>
		/// Method for retrieving a Registry Value.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		public static string GetStringRegistryValue(string key, string defaultValue)
		{
			RegistryKey rkCompany;
			RegistryKey rkApplication;

			rkCompany = Registry.CurrentUser.OpenSubKey(SOFTWARE_KEY, false).OpenSubKey(COMPANY_NAME, false);
			if (rkCompany != null)
			{
				rkApplication = rkCompany.OpenSubKey(APPLICATION_NAME, true);
				if (rkApplication != null)
				{
					foreach (string sKey in rkApplication.GetValueNames())
					{
						if (sKey == key)
						{
							return (string) rkApplication.GetValue(sKey);
						}
					}
				}
			}
			return defaultValue;
		}

		/// <summary>
		/// Method for storing a Registry Value.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="stringValue"></param>
		public static void SetStringRegistryValue(string key, string stringValue)
		{
			RegistryKey rkSoftware;
			RegistryKey rkCompany;
			RegistryKey rkApplication;

			rkSoftware = Registry.CurrentUser.OpenSubKey(SOFTWARE_KEY, true);
			rkCompany = rkSoftware.CreateSubKey(COMPANY_NAME);
			if (rkCompany != null)
			{
				rkApplication = rkCompany.CreateSubKey(APPLICATION_NAME);
				if (rkApplication != null)
				{
					rkApplication.SetValue(key, stringValue);
				}
			}
		}
	}
}