using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;


namespace HBOS.FS.AMP.Windows.Controls
{
	/// <summary>
	/// The Clerical Medical splash screen - displays a Splash Screen while application initialisation occurs. 
	/// The Splash screen contains details of the application name, application version and a progress message.
	/// </summary>
	public class SplashScreenForm : System.Windows.Forms.Form
	{
		#region Controls

		private System.Windows.Forms.Label lblApplication;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.Label lblLegalCopyright;
		private System.Windows.Forms.Label lblProgress;

		#endregion 

		#region Constructors

		/// <summary>
		/// Construct a new SplashScreenForm
		/// </summary>
		/// <param name="application">Application text to display.</param>
		/// <param name="version">Version text to display></param>
		/// <param name="legalCopyright">Legal copyright to display</param>
		public SplashScreenForm( string application, string version, string legalCopyright )
		{
			InitializeComponent();

			lblApplication.Text = application;
			lblVersion.Text = version;
			lblLegalCopyright.Text = legalCopyright;

			lblProgress.Text = string.Empty;

			this.Show();

			this.Refresh();
		}

		/// <summary>
		/// Construct a new SplashScreenForm
		/// </summary>
		/// <param name="application">Application text to display.</param>
		/// <param name="version">Version text to display></param>
		public SplashScreenForm( string application, string version ) : this( application, version, string.Empty )
		{
		}

		/// <summary>
		/// Construct a new SplashScreenForm
		/// </summary>
		/// <param name="application">Application text to display.</param>
		public SplashScreenForm( string application ) : this( application, string.Empty, string.Empty )
		{
		}

		#endregion

		#region Properties
		/// <summary>
		/// Set the progress text to display
		/// </summary>
		public string Progress
		{
			set
			{
				lblProgress.Text = value;
				lblProgress.Refresh();
			}
		}
		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SplashScreenForm));
			this.lblApplication = new System.Windows.Forms.Label();
			this.lblVersion = new System.Windows.Forms.Label();
			this.lblLegalCopyright = new System.Windows.Forms.Label();
			this.lblProgress = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblApplication
			// 
			this.lblApplication.BackColor = System.Drawing.Color.Transparent;
			this.lblApplication.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblApplication.Location = new System.Drawing.Point(16, 88);
			this.lblApplication.Name = "lblApplication";
			this.lblApplication.Size = new System.Drawing.Size(456, 40);
			this.lblApplication.TabIndex = 0;
			this.lblApplication.Text = "Application Name";
			this.lblApplication.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblVersion
			// 
			this.lblVersion.BackColor = System.Drawing.Color.Transparent;
			this.lblVersion.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblVersion.Location = new System.Drawing.Point(312, 200);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(160, 24);
			this.lblVersion.TabIndex = 1;
			this.lblVersion.Text = "Version";
			this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblLegalCopyright
			// 
			this.lblLegalCopyright.BackColor = System.Drawing.Color.Transparent;
			this.lblLegalCopyright.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblLegalCopyright.Location = new System.Drawing.Point(16, 256);
			this.lblLegalCopyright.Name = "lblLegalCopyright";
			this.lblLegalCopyright.Size = new System.Drawing.Size(448, 16);
			this.lblLegalCopyright.TabIndex = 2;
			this.lblLegalCopyright.Text = "Legal copyright";
			// 
			// lblProgress
			// 
			this.lblProgress.BackColor = System.Drawing.Color.Transparent;
			this.lblProgress.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblProgress.Location = new System.Drawing.Point(24, 200);
			this.lblProgress.Name = "lblProgress";
			this.lblProgress.Size = new System.Drawing.Size(256, 16);
			this.lblProgress.TabIndex = 3;
			this.lblProgress.Text = "Progress";
			// 
			// SplashScreenForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = new System.Drawing.Size(488, 288);
			this.ControlBox = false;
			this.Controls.Add(this.lblProgress);
			this.Controls.Add(this.lblLegalCopyright);
			this.Controls.Add(this.lblVersion);
			this.Controls.Add(this.lblApplication);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SplashScreenForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.ResumeLayout(false);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#endregion
	}
}
