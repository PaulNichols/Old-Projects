using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using SysCon = System.Configuration;
using AMPCon = HBOS.FS.AMP.Configuration;

namespace ConfigTestHarness
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class frmTestHarness : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button buttonConfigurationSettings;
		private System.Windows.Forms.Button buttonAssemblySettings;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmTestHarness()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.buttonAssemblySettings = new System.Windows.Forms.Button();
			this.buttonConfigurationSettings = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// buttonAssemblySettings
			// 
			this.buttonAssemblySettings.Location = new System.Drawing.Point(80, 72);
			this.buttonAssemblySettings.Name = "buttonAssemblySettings";
			this.buttonAssemblySettings.Size = new System.Drawing.Size(136, 23);
			this.buttonAssemblySettings.TabIndex = 0;
			this.buttonAssemblySettings.Text = "Assembly Settings";
			this.buttonAssemblySettings.Click += new System.EventHandler(this.buttonAssemblySettings_Click);
			// 
			// buttonConfigurationSettings
			// 
			this.buttonConfigurationSettings.Location = new System.Drawing.Point(80, 120);
			this.buttonConfigurationSettings.Name = "buttonConfigurationSettings";
			this.buttonConfigurationSettings.Size = new System.Drawing.Size(136, 23);
			this.buttonConfigurationSettings.TabIndex = 1;
			this.buttonConfigurationSettings.Text = "Configuration Settings";
			this.buttonConfigurationSettings.Click += new System.EventHandler(this.buttonConfigurationSettings_Click);
			// 
			// frmTestHarness
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.buttonConfigurationSettings);
			this.Controls.Add(this.buttonAssemblySettings);
			this.Name = "frmTestHarness";
			this.Text = "Test Harness";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new frmTestHarness());
		}

		private void buttonConfigurationSettings_Click(object sender, System.EventArgs e)
		{
			XmlNode myConfigNode = (XmlNode)AMPCon.ConfigurationSettings.GetConfig( "Dummy" );
			MessageBox.Show( myConfigNode.OuterXml.ToString() );		
		}

		private void buttonAssemblySettings_Click(object sender, System.EventArgs e)
		{
			string connectionString = AMPCon.ConfigurationSettings.AppSettings[ "RAWConnectionString" ];
			MessageBox.Show( connectionString );

		}
	}

}
