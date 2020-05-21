using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using HBOS.FS.AMP.UPD.WinUI.Classes;

namespace ApplicationSettingsEditor
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : Form
	{
		private TextBox xmlTextBox;
		private Panel panel1;
		private Button SaveButton;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			ApplicationSettings settings = GlobalRegistry.AppSettings;

			serializeSettings(settings);
			xmlTextBox.Focus();
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
			this.xmlTextBox = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.SaveButton = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// xmlTextBox
			// 
			this.xmlTextBox.AcceptsReturn = true;
			this.xmlTextBox.AcceptsTab = true;
			this.xmlTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xmlTextBox.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
			this.xmlTextBox.Location = new System.Drawing.Point(0, 0);
			this.xmlTextBox.Multiline = true;
			this.xmlTextBox.Name = "xmlTextBox";
			this.xmlTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.xmlTextBox.Size = new System.Drawing.Size(736, 552);
			this.xmlTextBox.TabIndex = 0;
			this.xmlTextBox.Text = "";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.SaveButton);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 488);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(736, 64);
			this.panel1.TabIndex = 1;
			// 
			// SaveButton
			// 
			this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.SaveButton.Location = new System.Drawing.Point(632, 24);
			this.SaveButton.Name = "SaveButton";
			this.SaveButton.TabIndex = 1;
			this.SaveButton.Text = "Save";
			this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
			this.ClientSize = new System.Drawing.Size(736, 552);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.xmlTextBox);
			this.Name = "MainForm";
			this.Text = "Application Settings Editor";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			Application.Run(new MainForm());
		}

		private void SaveButton_Click(object sender, EventArgs e)
		{
			try
			{
				GlobalRegistry.PersistApplicationSettings(deserializeSettings(this.xmlTextBox.Text));
				MessageBox.Show("Saved Changes", "Success");
			}
			catch (UnauthorizedAccessException ex)
			{
				MessageBox.Show(ex.Message, "Error");
			}
		}

		private void serializeSettings(ApplicationSettings settings)
		{
			XmlSerializer serializer = new XmlSerializer(typeof (ApplicationSettings));
			StringBuilder stringBuilder = new StringBuilder();
			StringWriter writer = new StringWriter(stringBuilder);

			serializer.Serialize(writer, settings);

			this.xmlTextBox.Text = stringBuilder.ToString();
		}

		private ApplicationSettings deserializeSettings(string xmlSettings)
		{
			XmlSerializer serializer = new XmlSerializer(typeof (ApplicationSettings));
			StringReader reader = new StringReader(xmlSettings);

			ApplicationSettings settings = (ApplicationSettings) serializer.Deserialize(reader);
			return settings;
		}
	}


}