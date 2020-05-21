/*
	This C# source code was automatically generated using:

		SQL Server Centric .NET Code Generator
			v 1.0.1697.21165

			Generation Date: 15/01/2005 18:39:14
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
using Bob.DataClasses;
using Bob.Windows.Forms;
using Bob.Windows.ListBoxes;
using SPs = Bob.DataClasses.StoredProcedures;
using Params = Bob.DataClasses.Parameters;

namespace Bob.Exe {

	/// <summary>
	/// Summary description for WinForm_Main.
	/// </summary>
	public class WinForm_Main : System.Windows.Forms.Form {

		private System.Windows.Forms.ComboBox comDatabaseTables;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabCustomers;
		private System.Windows.Forms.Button cmdNewCustomers;
		private System.Windows.Forms.Button cmdEditCustomers;
		private System.Windows.Forms.Button cmdDeleteCustomers;
		private System.Windows.Forms.Button cmdRefreshCustomers;
		private WinListBox_Customers lstWinListBox_Customers;
		private System.Windows.Forms.TabPage tabJob;
		private System.Windows.Forms.Button cmdNewJob;
		private System.Windows.Forms.Button cmdEditJob;
		private System.Windows.Forms.Button cmdDeleteJob;
		private System.Windows.Forms.Button cmdRefreshJob;
		private WinListBox_Job lstWinListBox_Job;
		private System.Windows.Forms.TabPage tabJobPart;
		private System.Windows.Forms.Button cmdNewJobPart;
		private System.Windows.Forms.Button cmdEditJobPart;
		private System.Windows.Forms.Button cmdDeleteJobPart;
		private System.Windows.Forms.Button cmdRefreshJobPart;
		private WinListBox_JobPart lstWinListBox_JobPart;
		private System.Windows.Forms.TabPage tabJobPartType;
		private System.Windows.Forms.Button cmdNewJobPartType;
		private System.Windows.Forms.Button cmdEditJobPartType;
		private System.Windows.Forms.Button cmdDeleteJobPartType;
		private System.Windows.Forms.Button cmdRefreshJobPartType;
		private WinListBox_JobPartType lstWinListBox_JobPartType;
		private System.Windows.Forms.TabPage tabTitle;
		private System.Windows.Forms.Button cmdNewTitle;
		private System.Windows.Forms.Button cmdEditTitle;
		private System.Windows.Forms.Button cmdDeleteTitle;
		private System.Windows.Forms.Button cmdRefreshTitle;
		private WinListBox_Title lstWinListBox_Title;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public WinForm_Main(string ConnectionString) {

			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.ConnectionString = ConnectionString;

			this.comDatabaseTables.Items.Add("Customers");
			this.comDatabaseTables.Items.Add("Job");
			this.comDatabaseTables.Items.Add("JobPart");
			this.comDatabaseTables.Items.Add("JobPartType");
			this.comDatabaseTables.Items.Add("Title");
			this.comDatabaseTables.SelectedIndex = 0;

			try {

				lstWinListBox_Customers.Initialize(ConnectionString, System.Data.SqlTypes.SqlInt32.Null);
				lstWinListBox_Job.Initialize(ConnectionString, System.Data.SqlTypes.SqlInt32.Null);
				lstWinListBox_JobPart.Initialize(ConnectionString, System.Data.SqlTypes.SqlInt32.Null, System.Data.SqlTypes.SqlInt32.Null);
				lstWinListBox_JobPartType.Initialize(ConnectionString);
				lstWinListBox_Title.Initialize(ConnectionString);
			}

			catch (Exception exception) {

				Clipboard.SetDataObject(exception.Message);
				MessageBox.Show(String.Format("The following exception has occured:{0}{0}{1}", Environment.NewLine, exception.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.Enabled = false;
			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing) {

			if (disposing) {

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

			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabCustomers = new System.Windows.Forms.TabPage();
			this.lstWinListBox_Customers = new Bob.Windows.ListBoxes.WinListBox_Customers();
			this.cmdNewCustomers = new System.Windows.Forms.Button();
			this.cmdEditCustomers = new System.Windows.Forms.Button();
			this.cmdDeleteCustomers = new System.Windows.Forms.Button();
			this.cmdRefreshCustomers = new System.Windows.Forms.Button();
			this.tabCustomers.SuspendLayout();
			this.tabJob = new System.Windows.Forms.TabPage();
			this.lstWinListBox_Job = new Bob.Windows.ListBoxes.WinListBox_Job();
			this.cmdNewJob = new System.Windows.Forms.Button();
			this.cmdEditJob = new System.Windows.Forms.Button();
			this.cmdDeleteJob = new System.Windows.Forms.Button();
			this.cmdRefreshJob = new System.Windows.Forms.Button();
			this.tabJob.SuspendLayout();
			this.tabJobPart = new System.Windows.Forms.TabPage();
			this.lstWinListBox_JobPart = new Bob.Windows.ListBoxes.WinListBox_JobPart();
			this.cmdNewJobPart = new System.Windows.Forms.Button();
			this.cmdEditJobPart = new System.Windows.Forms.Button();
			this.cmdDeleteJobPart = new System.Windows.Forms.Button();
			this.cmdRefreshJobPart = new System.Windows.Forms.Button();
			this.tabJobPart.SuspendLayout();
			this.tabJobPartType = new System.Windows.Forms.TabPage();
			this.lstWinListBox_JobPartType = new Bob.Windows.ListBoxes.WinListBox_JobPartType();
			this.cmdNewJobPartType = new System.Windows.Forms.Button();
			this.cmdEditJobPartType = new System.Windows.Forms.Button();
			this.cmdDeleteJobPartType = new System.Windows.Forms.Button();
			this.cmdRefreshJobPartType = new System.Windows.Forms.Button();
			this.tabJobPartType.SuspendLayout();
			this.tabTitle = new System.Windows.Forms.TabPage();
			this.lstWinListBox_Title = new Bob.Windows.ListBoxes.WinListBox_Title();
			this.cmdNewTitle = new System.Windows.Forms.Button();
			this.cmdEditTitle = new System.Windows.Forms.Button();
			this.cmdDeleteTitle = new System.Windows.Forms.Button();
			this.cmdRefreshTitle = new System.Windows.Forms.Button();
			this.tabTitle.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.SuspendLayout();
			// 
			// lstWinListBox_Customers
			// 
			this.lstWinListBox_Customers.Location = new System.Drawing.Point(16, 16);
			this.lstWinListBox_Customers.Name = "lstWinListBox_Customers";
			this.lstWinListBox_Customers.Size = new System.Drawing.Size(496, 355);
			this.lstWinListBox_Customers.TabIndex = 2;
			this.lstWinListBox_Customers.DoubleClick += new System.EventHandler(this.lstWinListBox_Customers_DoubleClick);
			// 
			// cmdNewCustomers
			// 
			this.cmdNewCustomers.Enabled = false;
			this.cmdNewCustomers.Location = new System.Drawing.Point(528, 16);
			this.cmdNewCustomers.Name = "cmdNewCustomers";
			this.cmdNewCustomers.Size = new System.Drawing.Size(104, 23);
			this.cmdNewCustomers.TabIndex = 3;
			this.cmdNewCustomers.Text = "New";
			this.cmdNewCustomers.Click += new System.EventHandler(this.cmdNewCustomers_Click);
			// 
			// cmdEditCustomers
			// 
			this.cmdEditCustomers.Enabled = false;
			this.cmdEditCustomers.Location = new System.Drawing.Point(528, 56);
			this.cmdEditCustomers.Name = "cmdEditCustomers";
			this.cmdEditCustomers.Size = new System.Drawing.Size(104, 23);
			this.cmdEditCustomers.TabIndex = 4;
			this.cmdEditCustomers.Text = "Edit";
			this.cmdEditCustomers.Click += new System.EventHandler(this.cmdEditCustomers_Click);
			// 
			// cmdDeleteCustomers
			// 
			this.cmdDeleteCustomers.Enabled = false;
			this.cmdDeleteCustomers.Location = new System.Drawing.Point(528, 96);
			this.cmdDeleteCustomers.Name = "cmdDeleteCustomers";
			this.cmdDeleteCustomers.Size = new System.Drawing.Size(104, 23);
			this.cmdDeleteCustomers.TabIndex = 5;
			this.cmdDeleteCustomers.Text = "Delete";
			this.cmdDeleteCustomers.Click += new System.EventHandler(this.cmdDeleteCustomers_Click);
			// 
			// cmdRefreshCustomers
			// 
			this.cmdRefreshCustomers.Location = new System.Drawing.Point(528, 344);
			this.cmdRefreshCustomers.Name = "cmdRefreshCustomers";
			this.cmdRefreshCustomers.Size = new System.Drawing.Size(96, 23);
			this.cmdRefreshCustomers.TabIndex = 6;
			this.cmdRefreshCustomers.Text = "Refresh";
			this.cmdRefreshCustomers.Click += new System.EventHandler(this.cmdRefreshCustomers_Click);
			// 
			// tabCustomers
			// 
			this.tabCustomers.Controls.AddRange(new System.Windows.Forms.Control[] {
															 this.lstWinListBox_Customers,
															 this.cmdNewCustomers,
															 this.cmdEditCustomers,
															 this.cmdDeleteCustomers,
															 this.cmdRefreshCustomers});
			this.tabCustomers.Location = new System.Drawing.Point(4, 22);
			this.tabCustomers.Name = "tabCustomers";
			this.tabCustomers.Size = new System.Drawing.Size(648, 390);
			this.tabCustomers.TabIndex = 1;
			this.tabCustomers.Text = "Customers";
			// 
			// lstWinListBox_Job
			// 
			this.lstWinListBox_Job.Location = new System.Drawing.Point(16, 16);
			this.lstWinListBox_Job.Name = "lstWinListBox_Job";
			this.lstWinListBox_Job.Size = new System.Drawing.Size(496, 355);
			this.lstWinListBox_Job.TabIndex = 2;
			this.lstWinListBox_Job.DoubleClick += new System.EventHandler(this.lstWinListBox_Job_DoubleClick);
			// 
			// cmdNewJob
			// 
			this.cmdNewJob.Enabled = false;
			this.cmdNewJob.Location = new System.Drawing.Point(528, 16);
			this.cmdNewJob.Name = "cmdNewJob";
			this.cmdNewJob.Size = new System.Drawing.Size(104, 23);
			this.cmdNewJob.TabIndex = 3;
			this.cmdNewJob.Text = "New";
			this.cmdNewJob.Click += new System.EventHandler(this.cmdNewJob_Click);
			// 
			// cmdEditJob
			// 
			this.cmdEditJob.Enabled = false;
			this.cmdEditJob.Location = new System.Drawing.Point(528, 56);
			this.cmdEditJob.Name = "cmdEditJob";
			this.cmdEditJob.Size = new System.Drawing.Size(104, 23);
			this.cmdEditJob.TabIndex = 4;
			this.cmdEditJob.Text = "Edit";
			this.cmdEditJob.Click += new System.EventHandler(this.cmdEditJob_Click);
			// 
			// cmdDeleteJob
			// 
			this.cmdDeleteJob.Enabled = false;
			this.cmdDeleteJob.Location = new System.Drawing.Point(528, 96);
			this.cmdDeleteJob.Name = "cmdDeleteJob";
			this.cmdDeleteJob.Size = new System.Drawing.Size(104, 23);
			this.cmdDeleteJob.TabIndex = 5;
			this.cmdDeleteJob.Text = "Delete";
			this.cmdDeleteJob.Click += new System.EventHandler(this.cmdDeleteJob_Click);
			// 
			// cmdRefreshJob
			// 
			this.cmdRefreshJob.Location = new System.Drawing.Point(528, 344);
			this.cmdRefreshJob.Name = "cmdRefreshJob";
			this.cmdRefreshJob.Size = new System.Drawing.Size(96, 23);
			this.cmdRefreshJob.TabIndex = 6;
			this.cmdRefreshJob.Text = "Refresh";
			this.cmdRefreshJob.Click += new System.EventHandler(this.cmdRefreshJob_Click);
			// 
			// tabJob
			// 
			this.tabJob.Controls.AddRange(new System.Windows.Forms.Control[] {
															 this.lstWinListBox_Job,
															 this.cmdNewJob,
															 this.cmdEditJob,
															 this.cmdDeleteJob,
															 this.cmdRefreshJob});
			this.tabJob.Location = new System.Drawing.Point(4, 22);
			this.tabJob.Name = "tabJob";
			this.tabJob.Size = new System.Drawing.Size(648, 390);
			this.tabJob.TabIndex = 1;
			this.tabJob.Text = "Job";
			// 
			// lstWinListBox_JobPart
			// 
			this.lstWinListBox_JobPart.Location = new System.Drawing.Point(16, 16);
			this.lstWinListBox_JobPart.Name = "lstWinListBox_JobPart";
			this.lstWinListBox_JobPart.Size = new System.Drawing.Size(496, 355);
			this.lstWinListBox_JobPart.TabIndex = 2;
			this.lstWinListBox_JobPart.DoubleClick += new System.EventHandler(this.lstWinListBox_JobPart_DoubleClick);
			// 
			// cmdNewJobPart
			// 
			this.cmdNewJobPart.Enabled = false;
			this.cmdNewJobPart.Location = new System.Drawing.Point(528, 16);
			this.cmdNewJobPart.Name = "cmdNewJobPart";
			this.cmdNewJobPart.Size = new System.Drawing.Size(104, 23);
			this.cmdNewJobPart.TabIndex = 3;
			this.cmdNewJobPart.Text = "New";
			this.cmdNewJobPart.Click += new System.EventHandler(this.cmdNewJobPart_Click);
			// 
			// cmdEditJobPart
			// 
			this.cmdEditJobPart.Enabled = false;
			this.cmdEditJobPart.Location = new System.Drawing.Point(528, 56);
			this.cmdEditJobPart.Name = "cmdEditJobPart";
			this.cmdEditJobPart.Size = new System.Drawing.Size(104, 23);
			this.cmdEditJobPart.TabIndex = 4;
			this.cmdEditJobPart.Text = "Edit";
			this.cmdEditJobPart.Click += new System.EventHandler(this.cmdEditJobPart_Click);
			// 
			// cmdDeleteJobPart
			// 
			this.cmdDeleteJobPart.Enabled = false;
			this.cmdDeleteJobPart.Location = new System.Drawing.Point(528, 96);
			this.cmdDeleteJobPart.Name = "cmdDeleteJobPart";
			this.cmdDeleteJobPart.Size = new System.Drawing.Size(104, 23);
			this.cmdDeleteJobPart.TabIndex = 5;
			this.cmdDeleteJobPart.Text = "Delete";
			this.cmdDeleteJobPart.Click += new System.EventHandler(this.cmdDeleteJobPart_Click);
			// 
			// cmdRefreshJobPart
			// 
			this.cmdRefreshJobPart.Location = new System.Drawing.Point(528, 344);
			this.cmdRefreshJobPart.Name = "cmdRefreshJobPart";
			this.cmdRefreshJobPart.Size = new System.Drawing.Size(96, 23);
			this.cmdRefreshJobPart.TabIndex = 6;
			this.cmdRefreshJobPart.Text = "Refresh";
			this.cmdRefreshJobPart.Click += new System.EventHandler(this.cmdRefreshJobPart_Click);
			// 
			// tabJobPart
			// 
			this.tabJobPart.Controls.AddRange(new System.Windows.Forms.Control[] {
															 this.lstWinListBox_JobPart,
															 this.cmdNewJobPart,
															 this.cmdEditJobPart,
															 this.cmdDeleteJobPart,
															 this.cmdRefreshJobPart});
			this.tabJobPart.Location = new System.Drawing.Point(4, 22);
			this.tabJobPart.Name = "tabJobPart";
			this.tabJobPart.Size = new System.Drawing.Size(648, 390);
			this.tabJobPart.TabIndex = 1;
			this.tabJobPart.Text = "JobPart";
			// 
			// lstWinListBox_JobPartType
			// 
			this.lstWinListBox_JobPartType.Location = new System.Drawing.Point(16, 16);
			this.lstWinListBox_JobPartType.Name = "lstWinListBox_JobPartType";
			this.lstWinListBox_JobPartType.Size = new System.Drawing.Size(496, 355);
			this.lstWinListBox_JobPartType.TabIndex = 2;
			this.lstWinListBox_JobPartType.DoubleClick += new System.EventHandler(this.lstWinListBox_JobPartType_DoubleClick);
			// 
			// cmdNewJobPartType
			// 
			this.cmdNewJobPartType.Enabled = false;
			this.cmdNewJobPartType.Location = new System.Drawing.Point(528, 16);
			this.cmdNewJobPartType.Name = "cmdNewJobPartType";
			this.cmdNewJobPartType.Size = new System.Drawing.Size(104, 23);
			this.cmdNewJobPartType.TabIndex = 3;
			this.cmdNewJobPartType.Text = "New";
			this.cmdNewJobPartType.Click += new System.EventHandler(this.cmdNewJobPartType_Click);
			// 
			// cmdEditJobPartType
			// 
			this.cmdEditJobPartType.Enabled = false;
			this.cmdEditJobPartType.Location = new System.Drawing.Point(528, 56);
			this.cmdEditJobPartType.Name = "cmdEditJobPartType";
			this.cmdEditJobPartType.Size = new System.Drawing.Size(104, 23);
			this.cmdEditJobPartType.TabIndex = 4;
			this.cmdEditJobPartType.Text = "Edit";
			this.cmdEditJobPartType.Click += new System.EventHandler(this.cmdEditJobPartType_Click);
			// 
			// cmdDeleteJobPartType
			// 
			this.cmdDeleteJobPartType.Enabled = false;
			this.cmdDeleteJobPartType.Location = new System.Drawing.Point(528, 96);
			this.cmdDeleteJobPartType.Name = "cmdDeleteJobPartType";
			this.cmdDeleteJobPartType.Size = new System.Drawing.Size(104, 23);
			this.cmdDeleteJobPartType.TabIndex = 5;
			this.cmdDeleteJobPartType.Text = "Delete";
			this.cmdDeleteJobPartType.Click += new System.EventHandler(this.cmdDeleteJobPartType_Click);
			// 
			// cmdRefreshJobPartType
			// 
			this.cmdRefreshJobPartType.Location = new System.Drawing.Point(528, 344);
			this.cmdRefreshJobPartType.Name = "cmdRefreshJobPartType";
			this.cmdRefreshJobPartType.Size = new System.Drawing.Size(96, 23);
			this.cmdRefreshJobPartType.TabIndex = 6;
			this.cmdRefreshJobPartType.Text = "Refresh";
			this.cmdRefreshJobPartType.Click += new System.EventHandler(this.cmdRefreshJobPartType_Click);
			// 
			// tabJobPartType
			// 
			this.tabJobPartType.Controls.AddRange(new System.Windows.Forms.Control[] {
															 this.lstWinListBox_JobPartType,
															 this.cmdNewJobPartType,
															 this.cmdEditJobPartType,
															 this.cmdDeleteJobPartType,
															 this.cmdRefreshJobPartType});
			this.tabJobPartType.Location = new System.Drawing.Point(4, 22);
			this.tabJobPartType.Name = "tabJobPartType";
			this.tabJobPartType.Size = new System.Drawing.Size(648, 390);
			this.tabJobPartType.TabIndex = 1;
			this.tabJobPartType.Text = "JobPartType";
			// 
			// lstWinListBox_Title
			// 
			this.lstWinListBox_Title.Location = new System.Drawing.Point(16, 16);
			this.lstWinListBox_Title.Name = "lstWinListBox_Title";
			this.lstWinListBox_Title.Size = new System.Drawing.Size(496, 355);
			this.lstWinListBox_Title.TabIndex = 2;
			this.lstWinListBox_Title.DoubleClick += new System.EventHandler(this.lstWinListBox_Title_DoubleClick);
			// 
			// cmdNewTitle
			// 
			this.cmdNewTitle.Enabled = false;
			this.cmdNewTitle.Location = new System.Drawing.Point(528, 16);
			this.cmdNewTitle.Name = "cmdNewTitle";
			this.cmdNewTitle.Size = new System.Drawing.Size(104, 23);
			this.cmdNewTitle.TabIndex = 3;
			this.cmdNewTitle.Text = "New";
			this.cmdNewTitle.Click += new System.EventHandler(this.cmdNewTitle_Click);
			// 
			// cmdEditTitle
			// 
			this.cmdEditTitle.Enabled = false;
			this.cmdEditTitle.Location = new System.Drawing.Point(528, 56);
			this.cmdEditTitle.Name = "cmdEditTitle";
			this.cmdEditTitle.Size = new System.Drawing.Size(104, 23);
			this.cmdEditTitle.TabIndex = 4;
			this.cmdEditTitle.Text = "Edit";
			this.cmdEditTitle.Click += new System.EventHandler(this.cmdEditTitle_Click);
			// 
			// cmdDeleteTitle
			// 
			this.cmdDeleteTitle.Enabled = false;
			this.cmdDeleteTitle.Location = new System.Drawing.Point(528, 96);
			this.cmdDeleteTitle.Name = "cmdDeleteTitle";
			this.cmdDeleteTitle.Size = new System.Drawing.Size(104, 23);
			this.cmdDeleteTitle.TabIndex = 5;
			this.cmdDeleteTitle.Text = "Delete";
			this.cmdDeleteTitle.Click += new System.EventHandler(this.cmdDeleteTitle_Click);
			// 
			// cmdRefreshTitle
			// 
			this.cmdRefreshTitle.Location = new System.Drawing.Point(528, 344);
			this.cmdRefreshTitle.Name = "cmdRefreshTitle";
			this.cmdRefreshTitle.Size = new System.Drawing.Size(96, 23);
			this.cmdRefreshTitle.TabIndex = 6;
			this.cmdRefreshTitle.Text = "Refresh";
			this.cmdRefreshTitle.Click += new System.EventHandler(this.cmdRefreshTitle_Click);
			// 
			// tabTitle
			// 
			this.tabTitle.Controls.AddRange(new System.Windows.Forms.Control[] {
															 this.lstWinListBox_Title,
															 this.cmdNewTitle,
															 this.cmdEditTitle,
															 this.cmdDeleteTitle,
															 this.cmdRefreshTitle});
			this.tabTitle.Location = new System.Drawing.Point(4, 22);
			this.tabTitle.Name = "tabTitle";
			this.tabTitle.Size = new System.Drawing.Size(648, 390);
			this.tabTitle.TabIndex = 1;
			this.tabTitle.Text = "Title";
			//
			// comDatabaseTables
			//
			this.comDatabaseTables = new System.Windows.Forms.ComboBox();
			this.comDatabaseTables.Location = new System.Drawing.Point(16, 16);
			this.comDatabaseTables.Name = "comDatabaseTables ";
			this.comDatabaseTables.Size = new System.Drawing.Size(656, 24);
			this.comDatabaseTables.TabIndex = 0;
			this.comDatabaseTables.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comDatabaseTables.SelectedIndexChanged += new System.EventHandler(comDatabaseTables_SelectedIndexChanged);
			// 
			// tabControl
			// 
			this.tabControl.Controls.AddRange(new System.Windows.Forms.Control[] {
															  this.tabCustomers
															 ,this.tabJob
															 ,this.tabJobPart
															 ,this.tabJobPartType
															 ,this.tabTitle
															});
			this.tabControl.Location = new System.Drawing.Point(16, 45);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(656, 416);
			this.tabControl.TabIndex = 1;
			this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
			// 
			// WinForm_Main
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 13);
			this.ClientSize = new System.Drawing.Size(692, 470);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
												  this.tabControl, this.comDatabaseTables});
			this.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "WinForm_Main";
			this.Text = "\'Bob\' Management";
			this.tabCustomers.ResumeLayout(false);
			this.tabJob.ResumeLayout(false);
			this.tabJobPart.ResumeLayout(false);
			this.tabJobPartType.ResumeLayout(false);
			this.tabTitle.ResumeLayout(false);
			this.tabControl.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {

			string connectionString = System.Configuration.ConfigurationSettings.AppSettings["Bob ConnectionString"];

			if (connectionString == null || connectionString == String.Empty) {

				Bob.Windows.Forms.WinForm_DBConnection oWinForm_DBConnection = new Bob.Windows.Forms.WinForm_DBConnection();
				oWinForm_DBConnection.InitializeData("Bob connection", @"MAINSERVER\MAINSERVER", "Bob");
				oWinForm_DBConnection.ShowDialog(null);
				if (oWinForm_DBConnection.DialogResult == DialogResult.OK) {

					connectionString = oWinForm_DBConnection.ConnectionString;
					oWinForm_DBConnection.Dispose();
					Application.Run(new WinForm_Main(connectionString));
				}
			}
			else {

				Application.Run(new WinForm_Main(connectionString));
			}
		}

		private string ConnectionString = String.Empty;

		private void comDatabaseTables_SelectedIndexChanged(object sender, EventArgs e) {

			switch (comDatabaseTables.SelectedItem.ToString()) {

				case "Customers":
				this.tabControl.SelectedTab = tabCustomers;
				break;

				case "Job":
				this.tabControl.SelectedTab = tabJob;
				break;

				case "JobPart":
				this.tabControl.SelectedTab = tabJobPart;
				break;

				case "JobPartType":
				this.tabControl.SelectedTab = tabJobPartType;
				break;

				case "Title":
				this.tabControl.SelectedTab = tabTitle;
				break;

			}
		}
		private void lstWinListBox_Customers_DoubleClick(object sender, EventArgs e) {

			this.Cursor = Cursors.WaitCursor;

			EditCustomers();

			this.Cursor = Cursors.Default;
		}

		private void EditCustomers() {

			System.Data.SqlTypes.SqlInt32 _CustomerID = (System.Int32)lstWinListBox_Customers.SelectedValue;

			if (_CustomerID.IsNull) {

				return;
			}

			WinForm_Customers oWinForm_Customers = new WinForm_Customers();

			try {

				if (oWinForm_Customers.EditExistingRecord(this, "Edit an existing Customers", ConnectionString, _CustomerID)) {

					if (oWinForm_Customers.DialogResult == DialogResult.OK) {

						if (!oWinForm_Customers.ErrorHasOccured) {

							RefreshCustomers(_CustomerID);
						}
					}
				}
			}
			catch (Bob.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_Customers' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.SqlException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (customException.Parameter.OtherException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_Customers' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.OtherException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else {

					throw;
				}
			}
		}

		private void cmdNewCustomers_Click(object sender, System.EventArgs e) {

			WinForm_Customers oWinForm_Customers = new WinForm_Customers();

			try {

				oWinForm_Customers.AddNewRecord(this, "Add a new Customers", ConnectionString);
				if (oWinForm_Customers.DialogResult == DialogResult.OK) {

					if (!oWinForm_Customers.ErrorHasOccured) {

						System.Data.SqlTypes.SqlInt32 Col_CustomerID = ((Params.spI_Customers)oWinForm_Customers.Parameter).Param_CustomerID;
						RefreshCustomers(Col_CustomerID);
					}
				}
			}
			catch (Bob.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_Customers' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.SqlException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (customException.Parameter.OtherException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_Customers' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.OtherException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else {

					throw;
				}
			}
		}

		private void cmdEditCustomers_Click(object sender, System.EventArgs e) {

			this.Cursor = Cursors.WaitCursor;

			EditCustomers();
			
			this.Cursor = Cursors.Default;
		}

		private void cmdRefreshCustomers_Click(object sender, System.EventArgs e) {

			this.Cursor = Cursors.WaitCursor;

			if (lstWinListBox_Customers.SelectedIndex != -1) {

				RefreshCustomers((System.Int32)lstWinListBox_Customers.SelectedValue);
			}
			else {

				RefreshCustomers(System.Data.SqlTypes.SqlInt32.Null);
			}

			this.Cursor = Cursors.Default;
		}

		private void RefreshCustomers(System.Data.SqlTypes.SqlInt32 Col_CustomerID) {

			try {

				lstWinListBox_Customers.RefreshData(Col_CustomerID);
			}
			catch (Bob.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinListBox_Customers' class. Exception message is: {0}", customException.Parameter.SqlException.Message), customException);
				}
				else if (customException.Parameter.OtherException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinListBox_Customers' class. Exception message is: {0}", customException.Parameter.OtherException.Message), customException);
				}
				else {

					throw;
				}
			}

			cmdNewCustomers.Enabled = true;
			bool Status = (lstWinListBox_Customers.Items.Count != 0);
			cmdEditCustomers.Enabled = Status;
			cmdDeleteCustomers.Enabled = Status;
		}

		private void cmdDeleteCustomers_Click(object sender, System.EventArgs e) {

			System.Data.SqlTypes.SqlInt32 Col_CustomerID = (System.Int32)lstWinListBox_Customers.SelectedValue;
			if (Col_CustomerID.IsNull) {

				return;
			}

			string Message = "Do you want really to delete this record ?";

			if (MessageBox.Show(Message, "Deletion requested", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {

				this.Cursor = Cursors.WaitCursor;

				Params.spD_Customers Param = new Params.spD_Customers();

				Param.SetUpConnection(ConnectionString);
				Param.Param_CustomerID = Col_CustomerID;

				SPs.spD_Customers SP = new SPs.spD_Customers();
				if (SP.Execute(ref Param)) {

					RefreshCustomers(System.Data.SqlTypes.SqlInt32.Null);
				}
				else {

					if (Param.SqlException != null && Param.SqlException.Number == 547) {

						MessageBox.Show(this, "Unable to delete this record:\r\n\r\n" + Param.SqlException.Message, "Error");
					}

					else {

						WinForm_ErrorForm oWinForm_ErrorForm = new WinForm_ErrorForm ();
						oWinForm_ErrorForm.DisplayError("WinForm_Main.cmdDeleteCustomers_Click", Param);
						oWinForm_ErrorForm.Dispose();
					}
				}

				this.Cursor = Cursors.Default;
			}
		}

		private void lstWinListBox_Job_DoubleClick(object sender, EventArgs e) {

			this.Cursor = Cursors.WaitCursor;

			EditJob();

			this.Cursor = Cursors.Default;
		}

		private void EditJob() {

			System.Data.SqlTypes.SqlInt32 _JobId = (System.Int32)lstWinListBox_Job.SelectedValue;

			if (_JobId.IsNull) {

				return;
			}

			WinForm_Job oWinForm_Job = new WinForm_Job();

			try {

				if (oWinForm_Job.EditExistingRecord(this, "Edit an existing Job", ConnectionString, _JobId)) {

					if (oWinForm_Job.DialogResult == DialogResult.OK) {

						if (!oWinForm_Job.ErrorHasOccured) {

							RefreshJob(_JobId);
						}
					}
				}
			}
			catch (Bob.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_Job' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.SqlException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (customException.Parameter.OtherException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_Job' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.OtherException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else {

					throw;
				}
			}
		}

		private void cmdNewJob_Click(object sender, System.EventArgs e) {

			WinForm_Job oWinForm_Job = new WinForm_Job();

			try {

				oWinForm_Job.AddNewRecord(this, "Add a new Job", ConnectionString);
				if (oWinForm_Job.DialogResult == DialogResult.OK) {

					if (!oWinForm_Job.ErrorHasOccured) {

						System.Data.SqlTypes.SqlInt32 Col_JobId = ((Params.spI_Job)oWinForm_Job.Parameter).Param_JobId;
						RefreshJob(Col_JobId);
					}
				}
			}
			catch (Bob.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_Job' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.SqlException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (customException.Parameter.OtherException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_Job' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.OtherException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else {

					throw;
				}
			}
		}

		private void cmdEditJob_Click(object sender, System.EventArgs e) {

			this.Cursor = Cursors.WaitCursor;

			EditJob();
			
			this.Cursor = Cursors.Default;
		}

		private void cmdRefreshJob_Click(object sender, System.EventArgs e) {

			this.Cursor = Cursors.WaitCursor;

			if (lstWinListBox_Job.SelectedIndex != -1) {

				RefreshJob((System.Int32)lstWinListBox_Job.SelectedValue);
			}
			else {

				RefreshJob(System.Data.SqlTypes.SqlInt32.Null);
			}

			this.Cursor = Cursors.Default;
		}

		private void RefreshJob(System.Data.SqlTypes.SqlInt32 Col_JobId) {

			try {

				lstWinListBox_Job.RefreshData(Col_JobId);
			}
			catch (Bob.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinListBox_Job' class. Exception message is: {0}", customException.Parameter.SqlException.Message), customException);
				}
				else if (customException.Parameter.OtherException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinListBox_Job' class. Exception message is: {0}", customException.Parameter.OtherException.Message), customException);
				}
				else {

					throw;
				}
			}

			cmdNewJob.Enabled = true;
			bool Status = (lstWinListBox_Job.Items.Count != 0);
			cmdEditJob.Enabled = Status;
			cmdDeleteJob.Enabled = Status;
		}

		private void cmdDeleteJob_Click(object sender, System.EventArgs e) {

			System.Data.SqlTypes.SqlInt32 Col_JobId = (System.Int32)lstWinListBox_Job.SelectedValue;
			if (Col_JobId.IsNull) {

				return;
			}

			string Message = "Do you want really to delete this record ?";

			if (MessageBox.Show(Message, "Deletion requested", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {

				this.Cursor = Cursors.WaitCursor;

				Params.spD_Job Param = new Params.spD_Job();

				Param.SetUpConnection(ConnectionString);
				Param.Param_JobId = Col_JobId;

				SPs.spD_Job SP = new SPs.spD_Job();
				if (SP.Execute(ref Param)) {

					RefreshJob(System.Data.SqlTypes.SqlInt32.Null);
				}
				else {

					if (Param.SqlException != null && Param.SqlException.Number == 547) {

						MessageBox.Show(this, "Unable to delete this record:\r\n\r\n" + Param.SqlException.Message, "Error");
					}

					else {

						WinForm_ErrorForm oWinForm_ErrorForm = new WinForm_ErrorForm ();
						oWinForm_ErrorForm.DisplayError("WinForm_Main.cmdDeleteJob_Click", Param);
						oWinForm_ErrorForm.Dispose();
					}
				}

				this.Cursor = Cursors.Default;
			}
		}

		private void lstWinListBox_JobPart_DoubleClick(object sender, EventArgs e) {

			this.Cursor = Cursors.WaitCursor;

			EditJobPart();

			this.Cursor = Cursors.Default;
		}

		private void EditJobPart() {

			System.Data.SqlTypes.SqlInt32 _JobPartId = (System.Int32)lstWinListBox_JobPart.SelectedValue;

			if (_JobPartId.IsNull) {

				return;
			}

			WinForm_JobPart oWinForm_JobPart = new WinForm_JobPart();

			try {

				if (oWinForm_JobPart.EditExistingRecord(this, "Edit an existing JobPart", ConnectionString, _JobPartId)) {

					if (oWinForm_JobPart.DialogResult == DialogResult.OK) {

						if (!oWinForm_JobPart.ErrorHasOccured) {

							RefreshJobPart(_JobPartId);
						}
					}
				}
			}
			catch (Bob.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_JobPart' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.SqlException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (customException.Parameter.OtherException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_JobPart' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.OtherException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else {

					throw;
				}
			}
		}

		private void cmdNewJobPart_Click(object sender, System.EventArgs e) {

			WinForm_JobPart oWinForm_JobPart = new WinForm_JobPart();

			try {

				oWinForm_JobPart.AddNewRecord(this, "Add a new JobPart", ConnectionString);
				if (oWinForm_JobPart.DialogResult == DialogResult.OK) {

					if (!oWinForm_JobPart.ErrorHasOccured) {

						System.Data.SqlTypes.SqlInt32 Col_JobPartId = ((Params.spI_JobPart)oWinForm_JobPart.Parameter).Param_JobPartId;
						RefreshJobPart(Col_JobPartId);
					}
				}
			}
			catch (Bob.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_JobPart' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.SqlException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (customException.Parameter.OtherException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_JobPart' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.OtherException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else {

					throw;
				}
			}
		}

		private void cmdEditJobPart_Click(object sender, System.EventArgs e) {

			this.Cursor = Cursors.WaitCursor;

			EditJobPart();
			
			this.Cursor = Cursors.Default;
		}

		private void cmdRefreshJobPart_Click(object sender, System.EventArgs e) {

			this.Cursor = Cursors.WaitCursor;

			if (lstWinListBox_JobPart.SelectedIndex != -1) {

				RefreshJobPart((System.Int32)lstWinListBox_JobPart.SelectedValue);
			}
			else {

				RefreshJobPart(System.Data.SqlTypes.SqlInt32.Null);
			}

			this.Cursor = Cursors.Default;
		}

		private void RefreshJobPart(System.Data.SqlTypes.SqlInt32 Col_JobPartId) {

			try {

				lstWinListBox_JobPart.RefreshData(Col_JobPartId);
			}
			catch (Bob.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinListBox_JobPart' class. Exception message is: {0}", customException.Parameter.SqlException.Message), customException);
				}
				else if (customException.Parameter.OtherException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinListBox_JobPart' class. Exception message is: {0}", customException.Parameter.OtherException.Message), customException);
				}
				else {

					throw;
				}
			}

			cmdNewJobPart.Enabled = true;
			bool Status = (lstWinListBox_JobPart.Items.Count != 0);
			cmdEditJobPart.Enabled = Status;
			cmdDeleteJobPart.Enabled = Status;
		}

		private void cmdDeleteJobPart_Click(object sender, System.EventArgs e) {

			System.Data.SqlTypes.SqlInt32 Col_JobPartId = (System.Int32)lstWinListBox_JobPart.SelectedValue;
			if (Col_JobPartId.IsNull) {

				return;
			}

			string Message = "Do you want really to delete this record ?";

			if (MessageBox.Show(Message, "Deletion requested", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {

				this.Cursor = Cursors.WaitCursor;

				Params.spD_JobPart Param = new Params.spD_JobPart();

				Param.SetUpConnection(ConnectionString);
				Param.Param_JobPartId = Col_JobPartId;

				SPs.spD_JobPart SP = new SPs.spD_JobPart();
				if (SP.Execute(ref Param)) {

					RefreshJobPart(System.Data.SqlTypes.SqlInt32.Null);
				}
				else {

					if (Param.SqlException != null && Param.SqlException.Number == 547) {

						MessageBox.Show(this, "Unable to delete this record:\r\n\r\n" + Param.SqlException.Message, "Error");
					}

					else {

						WinForm_ErrorForm oWinForm_ErrorForm = new WinForm_ErrorForm ();
						oWinForm_ErrorForm.DisplayError("WinForm_Main.cmdDeleteJobPart_Click", Param);
						oWinForm_ErrorForm.Dispose();
					}
				}

				this.Cursor = Cursors.Default;
			}
		}

		private void lstWinListBox_JobPartType_DoubleClick(object sender, EventArgs e) {

			this.Cursor = Cursors.WaitCursor;

			EditJobPartType();

			this.Cursor = Cursors.Default;
		}

		private void EditJobPartType() {

			System.Data.SqlTypes.SqlInt32 _JobPartTypeId = (System.Int32)lstWinListBox_JobPartType.SelectedValue;

			if (_JobPartTypeId.IsNull) {

				return;
			}

			WinForm_JobPartType oWinForm_JobPartType = new WinForm_JobPartType();

			try {

				if (oWinForm_JobPartType.EditExistingRecord(this, "Edit an existing JobPartType", ConnectionString, _JobPartTypeId)) {

					if (oWinForm_JobPartType.DialogResult == DialogResult.OK) {

						if (!oWinForm_JobPartType.ErrorHasOccured) {

							RefreshJobPartType(_JobPartTypeId);
						}
					}
				}
			}
			catch (Bob.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_JobPartType' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.SqlException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (customException.Parameter.OtherException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_JobPartType' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.OtherException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else {

					throw;
				}
			}
		}

		private void cmdNewJobPartType_Click(object sender, System.EventArgs e) {

			WinForm_JobPartType oWinForm_JobPartType = new WinForm_JobPartType();

			try {

				oWinForm_JobPartType.AddNewRecord(this, "Add a new JobPartType", ConnectionString);
				if (oWinForm_JobPartType.DialogResult == DialogResult.OK) {

					if (!oWinForm_JobPartType.ErrorHasOccured) {

						System.Data.SqlTypes.SqlInt32 Col_JobPartTypeId = ((Params.spI_JobPartType)oWinForm_JobPartType.Parameter).Param_JobPartTypeId;
						RefreshJobPartType(Col_JobPartTypeId);
					}
				}
			}
			catch (Bob.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_JobPartType' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.SqlException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (customException.Parameter.OtherException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_JobPartType' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.OtherException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else {

					throw;
				}
			}
		}

		private void cmdEditJobPartType_Click(object sender, System.EventArgs e) {

			this.Cursor = Cursors.WaitCursor;

			EditJobPartType();
			
			this.Cursor = Cursors.Default;
		}

		private void cmdRefreshJobPartType_Click(object sender, System.EventArgs e) {

			this.Cursor = Cursors.WaitCursor;

			if (lstWinListBox_JobPartType.SelectedIndex != -1) {

				RefreshJobPartType((System.Int32)lstWinListBox_JobPartType.SelectedValue);
			}
			else {

				RefreshJobPartType(System.Data.SqlTypes.SqlInt32.Null);
			}

			this.Cursor = Cursors.Default;
		}

		private void RefreshJobPartType(System.Data.SqlTypes.SqlInt32 Col_JobPartTypeId) {

			try {

				lstWinListBox_JobPartType.RefreshData(Col_JobPartTypeId);
			}
			catch (Bob.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinListBox_JobPartType' class. Exception message is: {0}", customException.Parameter.SqlException.Message), customException);
				}
				else if (customException.Parameter.OtherException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinListBox_JobPartType' class. Exception message is: {0}", customException.Parameter.OtherException.Message), customException);
				}
				else {

					throw;
				}
			}

			cmdNewJobPartType.Enabled = true;
			bool Status = (lstWinListBox_JobPartType.Items.Count != 0);
			cmdEditJobPartType.Enabled = Status;
			cmdDeleteJobPartType.Enabled = Status;
		}

		private void cmdDeleteJobPartType_Click(object sender, System.EventArgs e) {

			System.Data.SqlTypes.SqlInt32 Col_JobPartTypeId = (System.Int32)lstWinListBox_JobPartType.SelectedValue;
			if (Col_JobPartTypeId.IsNull) {

				return;
			}

			string Message = "Do you want really to delete this record ?";

			if (MessageBox.Show(Message, "Deletion requested", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {

				this.Cursor = Cursors.WaitCursor;

				Params.spD_JobPartType Param = new Params.spD_JobPartType();

				Param.SetUpConnection(ConnectionString);
				Param.Param_JobPartTypeId = Col_JobPartTypeId;

				SPs.spD_JobPartType SP = new SPs.spD_JobPartType();
				if (SP.Execute(ref Param)) {

					RefreshJobPartType(System.Data.SqlTypes.SqlInt32.Null);
				}
				else {

					if (Param.SqlException != null && Param.SqlException.Number == 547) {

						MessageBox.Show(this, "Unable to delete this record:\r\n\r\n" + Param.SqlException.Message, "Error");
					}

					else {

						WinForm_ErrorForm oWinForm_ErrorForm = new WinForm_ErrorForm ();
						oWinForm_ErrorForm.DisplayError("WinForm_Main.cmdDeleteJobPartType_Click", Param);
						oWinForm_ErrorForm.Dispose();
					}
				}

				this.Cursor = Cursors.Default;
			}
		}

		private void lstWinListBox_Title_DoubleClick(object sender, EventArgs e) {

			this.Cursor = Cursors.WaitCursor;

			EditTitle();

			this.Cursor = Cursors.Default;
		}

		private void EditTitle() {

			System.Data.SqlTypes.SqlInt32 _TitleId = (System.Int32)lstWinListBox_Title.SelectedValue;

			if (_TitleId.IsNull) {

				return;
			}

			WinForm_Title oWinForm_Title = new WinForm_Title();

			try {

				if (oWinForm_Title.EditExistingRecord(this, "Edit an existing Title", ConnectionString, _TitleId)) {

					if (oWinForm_Title.DialogResult == DialogResult.OK) {

						if (!oWinForm_Title.ErrorHasOccured) {

							RefreshTitle(_TitleId);
						}
					}
				}
			}
			catch (Bob.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_Title' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.SqlException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (customException.Parameter.OtherException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_Title' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.OtherException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else {

					throw;
				}
			}
		}

		private void cmdNewTitle_Click(object sender, System.EventArgs e) {

			WinForm_Title oWinForm_Title = new WinForm_Title();

			try {

				oWinForm_Title.AddNewRecord(this, "Add a new Title", ConnectionString);
				if (oWinForm_Title.DialogResult == DialogResult.OK) {

					if (!oWinForm_Title.ErrorHasOccured) {

						System.Data.SqlTypes.SqlInt32 Col_TitleId = ((Params.spI_Title)oWinForm_Title.Parameter).Param_TitleId;
						RefreshTitle(Col_TitleId);
					}
				}
			}
			catch (Bob.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_Title' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.SqlException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (customException.Parameter.OtherException != null) {

					MessageBox.Show(this, String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinForm_Title' class.{1}{1}Exception message is:{1}{0}", customException.Parameter.OtherException.Message, System.Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else {

					throw;
				}
			}
		}

		private void cmdEditTitle_Click(object sender, System.EventArgs e) {

			this.Cursor = Cursors.WaitCursor;

			EditTitle();
			
			this.Cursor = Cursors.Default;
		}

		private void cmdRefreshTitle_Click(object sender, System.EventArgs e) {

			this.Cursor = Cursors.WaitCursor;

			if (lstWinListBox_Title.SelectedIndex != -1) {

				RefreshTitle((System.Int32)lstWinListBox_Title.SelectedValue);
			}
			else {

				RefreshTitle(System.Data.SqlTypes.SqlInt32.Null);
			}

			this.Cursor = Cursors.Default;
		}

		private void RefreshTitle(System.Data.SqlTypes.SqlInt32 Col_TitleId) {

			try {

				lstWinListBox_Title.RefreshData(Col_TitleId);
			}
			catch (Bob.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinListBox_Title' class. Exception message is: {0}", customException.Parameter.SqlException.Message), customException);
				}
				else if (customException.Parameter.OtherException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinListBox_Title' class. Exception message is: {0}", customException.Parameter.OtherException.Message), customException);
				}
				else {

					throw;
				}
			}

			cmdNewTitle.Enabled = true;
			bool Status = (lstWinListBox_Title.Items.Count != 0);
			cmdEditTitle.Enabled = Status;
			cmdDeleteTitle.Enabled = Status;
		}

		private void cmdDeleteTitle_Click(object sender, System.EventArgs e) {

			System.Data.SqlTypes.SqlInt32 Col_TitleId = (System.Int32)lstWinListBox_Title.SelectedValue;
			if (Col_TitleId.IsNull) {

				return;
			}

			string Message = "Do you want really to delete this record ?";

			if (MessageBox.Show(Message, "Deletion requested", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {

				this.Cursor = Cursors.WaitCursor;

				Params.spD_Title Param = new Params.spD_Title();

				Param.SetUpConnection(ConnectionString);
				Param.Param_TitleId = Col_TitleId;

				SPs.spD_Title SP = new SPs.spD_Title();
				if (SP.Execute(ref Param)) {

					RefreshTitle(System.Data.SqlTypes.SqlInt32.Null);
				}
				else {

					if (Param.SqlException != null && Param.SqlException.Number == 547) {

						MessageBox.Show(this, "Unable to delete this record:\r\n\r\n" + Param.SqlException.Message, "Error");
					}

					else {

						WinForm_ErrorForm oWinForm_ErrorForm = new WinForm_ErrorForm ();
						oWinForm_ErrorForm.DisplayError("WinForm_Main.cmdDeleteTitle_Click", Param);
						oWinForm_ErrorForm.Dispose();
					}
				}

				this.Cursor = Cursors.Default;
			}
		}

		private void tabControl_SelectedIndexChanged(object sender, System.EventArgs e) {

			comDatabaseTables.SelectedIndex = comDatabaseTables.FindString(tabControl.SelectedTab.Text);
		}
	}
}
