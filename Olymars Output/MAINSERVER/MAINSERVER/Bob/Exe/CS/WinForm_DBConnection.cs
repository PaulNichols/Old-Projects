/*
	This C# source code was automatically generated using:

		SQL Server Centric .NET Code Generator
			v 1.0.1697.21165

			Generation Date: 02/01/2005 12:32:59
			Generator name: MAINSERVER\Administrator
			Template last update: 13/10/2003 04:51:40
			Template revision: 56177501

			SQL Server version: 08.00.0760
			Server: MAINSERVER\MAINSERVER
			Database: [Bob]

	WARNING: This source is provided "AS IS" without warranty of any kind.
	The author disclaims all warranties, either express or implied, including
	the warranties of merchantability and fitness for a particular purpose.
	In no event shall the author or its suppliers be liable for any damages
	whatsoever including direct, indirect, incidental, consequential, loss of
	business profits or special damages, even if the author or its suppliers
	have been advised of the possibility of such damages.

	    More information: http://www.microsoft.com/france/msdn/olymars
	Latest interim build: http://www.olymars.net/latest.zip
	       Author's blog: http://blogs.msdn.com/olymars
*/

using System;
using System.Windows.Forms;

namespace Bob.Windows.Forms {

	/// <summary>
	/// This class derives from System.Windows.Forms.Form and allows you to collect
	/// from the user information on how to connect to a specific SQL Server database.
	/// </summary>
	public class WinForm_DBConnection: System.Windows.Forms.Form {

		private System.Windows.Forms.Label labServer;
		private System.Windows.Forms.TextBox txtServerName;
		private System.Windows.Forms.Label labDatabase;
		private System.Windows.Forms.CheckBox chkIntegratedSecurity;
		private System.Windows.Forms.TextBox txtDatabase;
		private System.Windows.Forms.TextBox txtUserID;
		private System.Windows.Forms.Label labUserID;
		private System.Windows.Forms.Label labPassword;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdOptions;
		private System.Windows.Forms.Label labTimeOut;
		private System.Windows.Forms.CheckBox chkPooled;
		private System.Windows.Forms.TextBox txtTimeOut;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Create a new instance of the Bob.Windows.Forms.WinForm_DBConnection class.
		/// </summary>
		public WinForm_DBConnection() {

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
		protected override void Dispose(bool disposing) {

			if(disposing) {

				if (components != null) {

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
		private void InitializeComponent() {

			this.chkIntegratedSecurity = new System.Windows.Forms.CheckBox();
			this.labUserID = new System.Windows.Forms.Label();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.labDatabase = new System.Windows.Forms.Label();
			this.labServer = new System.Windows.Forms.Label();
			this.cmdOK = new System.Windows.Forms.Button();
			this.labPassword = new System.Windows.Forms.Label();
			this.txtUserID = new System.Windows.Forms.TextBox();
			this.txtServerName = new System.Windows.Forms.TextBox();
			this.txtDatabase = new System.Windows.Forms.TextBox();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.cmdOptions = new System.Windows.Forms.Button();
			this.labTimeOut = new System.Windows.Forms.Label();
			this.txtTimeOut = new System.Windows.Forms.TextBox();
			this.chkPooled = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// chkIntegratedSecurity
			// 
			this.chkIntegratedSecurity.Location = new System.Drawing.Point(16, 128);
			this.chkIntegratedSecurity.Name = "chkIntegratedSecurity";
			this.chkIntegratedSecurity.Size = new System.Drawing.Size(192, 24);
			this.chkIntegratedSecurity.TabIndex = 7;
			this.chkIntegratedSecurity.Text = "Integrated security";
			this.chkIntegratedSecurity.CheckedChanged += new System.EventHandler(this.chkIntegratedSecurity_CheckedChanged);
			// 
			// labUserID
			// 
			this.labUserID.AutoSize = true;
			this.labUserID.Location = new System.Drawing.Point(32, 168);
			this.labUserID.Name = "labUserID";
			this.labUserID.Size = new System.Drawing.Size(69, 13);
			this.labUserID.TabIndex = 8;
			this.labUserID.Text = "User ID:";
			// 
			// cmdCancel
			// 
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(256, 232);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.TabIndex = 13;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// labDatabase
			// 
			this.labDatabase.AutoSize = true;
			this.labDatabase.Location = new System.Drawing.Point(16, 72);
			this.labDatabase.Name = "labDatabase";
			this.labDatabase.Size = new System.Drawing.Size(118, 13);
			this.labDatabase.TabIndex = 5;
			this.labDatabase.Text = "Database name:";
			// 
			// labServer
			// 
			this.labServer.AutoSize = true;
			this.labServer.Location = new System.Drawing.Point(16, 16);
			this.labServer.Name = "labServer";
			this.labServer.Size = new System.Drawing.Size(134, 13);
			this.labServer.TabIndex = 3;
			this.labServer.Text = "SQL Server name:";
			// 
			// cmdOK
			// 
			this.cmdOK.Location = new System.Drawing.Point(168, 232);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.TabIndex = 12;
			this.cmdOK.Text = "OK";
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// labPassword
			// 
			this.labPassword.AutoSize = true;
			this.labPassword.Location = new System.Drawing.Point(192, 168);
			this.labPassword.Name = "labPassword";
			this.labPassword.Size = new System.Drawing.Size(77, 13);
			this.labPassword.TabIndex = 10;
			this.labPassword.Text = "Password:";
			// 
			// txtUserID
			// 
			this.txtUserID.Location = new System.Drawing.Point(32, 184);
			this.txtUserID.Name = "txtUserID";
			this.txtUserID.Size = new System.Drawing.Size(136, 20);
			this.txtUserID.TabIndex = 9;
			this.txtUserID.Text = "";
			// 
			// txtServerName
			// 
			this.txtServerName.Location = new System.Drawing.Point(16, 32);
			this.txtServerName.Name = "txtServerName";
			this.txtServerName.Size = new System.Drawing.Size(312, 20);
			this.txtServerName.TabIndex = 4;
			this.txtServerName.Text = "";
			// 
			// txtDatabase
			// 
			this.txtDatabase.Location = new System.Drawing.Point(16, 88);
			this.txtDatabase.Name = "txtDatabase";
			this.txtDatabase.Size = new System.Drawing.Size(312, 20);
			this.txtDatabase.TabIndex = 6;
			this.txtDatabase.Text = "";
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(192, 184);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(136, 20);
			this.txtPassword.TabIndex = 11;
			this.txtPassword.Text = "";
			// 
			// cmdOptions
			// 
			this.cmdOptions.Location = new System.Drawing.Point(24, 232);
			this.cmdOptions.Name = "cmdOptions";
			this.cmdOptions.TabIndex = 14;
			this.cmdOptions.Text = ">>>";
			this.cmdOptions.Click += new System.EventHandler(this.cmdOptions_Click);
			// 
			// labTimeOut
			// 
			this.labTimeOut.AutoSize = true;
			this.labTimeOut.Location = new System.Drawing.Point(24, 280);
			this.labTimeOut.Name = "labTimeOut";
			this.labTimeOut.Size = new System.Drawing.Size(110, 13);
			this.labTimeOut.TabIndex = 5;
			this.labTimeOut.Text = "Time-out (s):";
			// 
			// txtTimeOut
			// 
			this.txtTimeOut.Location = new System.Drawing.Point(24, 296);
			this.txtTimeOut.Name = "txtTimeOut";
			this.txtTimeOut.Size = new System.Drawing.Size(104, 20);
			this.txtTimeOut.TabIndex = 15;
			this.txtTimeOut.Text = "15";
			this.txtTimeOut.Leave += new System.EventHandler(this.txtTimeOut_Leave);
			// 
			// chkPooled
			// 
			this.chkPooled.Location = new System.Drawing.Point(160, 296);
			this.chkPooled.Name = "chkPooled";
			this.chkPooled.TabIndex = 16;
			this.chkPooled.Text = "Pooled";
			// 
			// WinForm_DBConnection
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 13);
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(350, 270);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.chkPooled,
																		  this.txtTimeOut,
																		  this.labTimeOut,
																		  this.cmdOptions,
																		  this.cmdCancel,
																		  this.cmdOK,
																		  this.labPassword,
																		  this.labUserID,
																		  this.labDatabase,
																		  this.labServer,
																		  this.txtPassword,
																		  this.txtUserID,
																		  this.chkIntegratedSecurity,
																		  this.txtDatabase,
																		  this.txtServerName});
			this.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "WinForm_DBConnection";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "\'Bob\' connection";
			this.ResumeLayout(false);

		}
		#endregion

		private string connectionString = String.Empty;
		private bool ShowOptions = false;
		/// <summary>
		/// Returns the connection string built with the current values.
		/// </summary>
		public string ConnectionString {

			get {

				if (chkIntegratedSecurity.Checked) {

					connectionString = Bob.DataClasses.Information.BuildConnectionString(txtServerName.Text, txtDatabase.Text, this.TimeOut, this.Pooled);
				}
				else {

					connectionString = Bob.DataClasses.Information.BuildConnectionString(txtServerName.Text, txtDatabase.Text, txtUserID.Text, txtPassword.Text, this.TimeOut, this.Pooled);
				}

				return(connectionString);
			}
		}

		/// <summary>
		/// Returns the server name.
		/// </summary>
		public string Server {

			get {

				return(txtServerName.Text);
			}
		}

		/// <summary>
		/// Returns the database name.
		/// </summary>
		public string Database {

			get {

				return(txtDatabase.Text);
			}
		}

		/// <summary>
		/// Returns True if Integrated Security is chosen. Otherwise, returns False.
		/// </summary>
		public bool IntegratedSecurity {

			get {

				return(chkIntegratedSecurity.Checked);
			}
		}

		/// <summary>
		/// Returns the User ID.
		/// </summary>
		public string UserID {

			get {

				return(txtUserID.Text);
			}
		}

		/// <summary>
		/// Returns the password.
		/// </summary>
		public string Password {

			get {

				return(txtPassword.Text);
			}
		}

		/// <summary>
		/// Returns the time to wait while trying to establish a connection before terminating the attempt and generating an error.
		/// </summary>
		public short TimeOut {

			get {

				return(Convert.ToInt16(txtTimeOut.Text));
			}
		}

		/// <summary>
		/// Returns True if the connection has to be pooled. Otherwise, returns False.
		/// </summary>
		public bool Pooled {

			get {

				return(chkPooled.Checked);
			}
		}

		private void cmdOK_Click(object sender, System.EventArgs e) {

			this.DialogResult = DialogResult.OK;
		}

		private void cmdCancel_Click(object sender, System.EventArgs e) {

			this.DialogResult = DialogResult.Cancel;
		}

		private void chkIntegratedSecurity_CheckedChanged(object sender, System.EventArgs e) {

			bool Status = !chkIntegratedSecurity.Checked;
			labUserID.Visible = Status;
			txtUserID.Visible = Status;
			labPassword.Visible = Status;
			txtPassword.Visible = Status;

			if (Status) {

				txtUserID.Select();
			}
		}

		/// <summary>
		/// Initialize the form with pre-defined values.
		/// </summary>
		/// <param name="formTitle">Title of this form.</param>
		/// <param name="server">Name of the SQL Server instance.</param>
		/// <param name="database">Name of the SQL Server database.</param>
		public void InitializeData(string formTitle, string server, string database) {

			InitializeForm(formTitle, server, database, true, String.Empty, 15, false);
		}

		/// <summary>
		/// Initialize the form with pre-defined values.
		/// </summary>
		/// <param name="formTitle">Title of this form.</param>
		/// <param name="server">Name of the SQL Server instance.</param>
		/// <param name="database">Name of the SQL Server database.</param>
		/// <param name="timeOut">Connection time-out in seconds.</param>
		public void InitializeData(string formTitle, string server, string database, short timeOut) {

			InitializeForm(formTitle, server, database, true, String.Empty, timeOut, false);
		}

		/// <summary>
		/// Initialize the form with pre-defined values.
		/// </summary>
		/// <param name="formTitle">Title of this form.</param>
		/// <param name="server">Name of the SQL Server instance.</param>
		/// <param name="database">Name of the SQL Server database.</param>
		/// <param name="pooled">Whether the connection must be pooled or not.</param>
		public void InitializeData(string formTitle, string server, string database, bool pooled) {

			InitializeForm(formTitle, server, database, true, String.Empty, 15, pooled);
		}

		/// <summary>
		/// Initialize the form with pre-defined values.
		/// </summary>
		/// <param name="formTitle">Title of this form.</param>
		/// <param name="server">Name of the SQL Server instance.</param>
		/// <param name="database">Name of the SQL Server database.</param>
		/// <param name="timeOut">Connection time-out in seconds.</param>
		/// <param name="pooled">Whether the connection must be pooled or not.</param>
		public void InitializeData(string formTitle, string server, string database, short timeOut, bool pooled) {

			InitializeForm(formTitle, server, database, true, String.Empty, timeOut, pooled);
		}

		/// <summary>
		/// Initialize the form with pre-defined values.
		/// </summary>
		/// <param name="formTitle">Title of this form.</param>
		/// <param name="server">Name of the SQL Server instance.</param>
		/// <param name="database">Name of the SQL Server database.</param>
		/// <param name="userID">User logon.</param>
		public void InitializeData(string formTitle, string server, string database, string userID) {

			InitializeForm(formTitle, server, database, false, userID, 15, false);
		}

		/// <summary>
		/// Initialize the form with pre-defined values.
		/// </summary>
		/// <param name="formTitle">Title of this form.</param>
		/// <param name="server">Name of the SQL Server instance.</param>
		/// <param name="database">Name of the SQL Server database.</param>
		/// <param name="userID">User logon.</param>
		/// <param name="timeOut">Connection time-out in seconds.</param>
		public void InitializeData(string formTitle, string server, string database, string userID, short timeOut) {

			InitializeForm(formTitle, server, database, false, userID, timeOut, false);
		}

		/// <summary>
		/// Initialize the form with pre-defined values.
		/// </summary>
		/// <param name="formTitle">Title of this form.</param>
		/// <param name="server">Name of the SQL Server instance.</param>
		/// <param name="database">Name of the SQL Server database.</param>
		/// <param name="userID">User logon.</param>
		/// <param name="pooled">Whether the connection must be pooled or not.</param>
		public void InitializeData(string formTitle, string server, string database, string userID, bool pooled) {

			InitializeForm(formTitle, server, database, false, userID, 15, pooled);
		}

		/// <summary>
		/// Initialize the form with pre-defined values.
		/// </summary>
		/// <param name="formTitle">Title of this form.</param>
		/// <param name="server">Name of the SQL Server instance.</param>
		/// <param name="database">Name of the SQL Server database.</param>
		/// <param name="userID">User logon.</param>
		/// <param name="timeOut">Connection time-out in seconds.</param>
		/// <param name="pooled">Whether the connection must be pooled or not.</param>
		public void InitializeData(string formTitle, string server, string database, string userID, short timeOut, bool pooled) {

			InitializeForm(formTitle, server, database, false, userID, timeOut, pooled);
		}

		private void InitializeForm(string formTitle, string Server, string Database, bool IntegratedSecurity, string UserID, int TimeOut, bool Pooled) {

			this.Text = formTitle;
			this.txtServerName.Text = Server;
			this.txtDatabase.Text = Database;
			this.chkIntegratedSecurity.Checked = IntegratedSecurity;
			this.txtUserID.Text = UserID;
			this.txtTimeOut.Text = TimeOut.ToString();
			txtUserID.Visible = !IntegratedSecurity;
			txtPassword.Visible = !IntegratedSecurity;
			chkPooled.Checked = Pooled;

			txtServerName.Select();
		}

		private void cmdOptions_Click(object sender, System.EventArgs e) {

			ShowOptions = !ShowOptions;

			if (ShowOptions) {

				this.Height = 356;
				this.cmdOptions.Text = "<<<";
			}
			else {

				this.Height = 292;
				this.cmdOptions.Text = ">>>";
			}
		}

		private void txtTimeOut_Leave(object sender, System.EventArgs e) {

			try {

				short TimeOut = Convert.ToInt16(txtTimeOut.Text);
			}
			catch {

				MessageBox.Show(this, "The time-out value must be a valid short", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				txtTimeOut.Select();
			}
		}
	}
}
