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
using SPs = Bob.DataClasses.StoredProcedures;
using Params = Bob.DataClasses.Parameters;
using Abstracts = Bob.AbstractClasses;

namespace Bob.Windows.Forms {

	/// <summary>
	/// This class derives from the System.Windows.Forms.Form class and was built
	/// to allow you to interact (both for insertion AND update) with a form spefically
	/// designed for the 'Title' table.
	/// </summary>
	public class WinForm_Title : System.Windows.Forms.Form {

		private bool mustBeHidden_Title = false;
		private bool mustBeDisabled_Title = false;
		private bool hasBeenPopulated_Title = false;
		private System.Data.SqlTypes.SqlString internal_Title = System.Data.SqlTypes.SqlString.Null;
		private System.Windows.Forms.Label labTitle;
		private WinForm_DatabaseTextBox Control_Title;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Button cmdCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'Title' column must be hidden.
		/// </summary>
		public bool MustBeHidden_Title {

			get {

				return(this.mustBeHidden_Title);
			}

			set {

				this.mustBeHidden_Title = value;
			}
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'Title' column must be disabled.
		/// </summary>
		public bool MustBeDisabled_Title {

			get {

				return(this.mustBeDisabled_Title);
			}

			set {

				this.mustBeDisabled_Title = value;
			}
		}

		/// <summary>
		/// Allows you to pre-select a value for the 'Title' column.
		/// </summary>
		public void SetValue_Title(System.Data.SqlTypes.SqlString value) {

			this.hasBeenPopulated_Title = true;
			this.internal_Title = value;
		}

		/// <summary>
		/// Allows you to reset the pre-selected value for the 'Title' column.
		/// </summary>
		public void ResetValue_Title() {

			this.hasBeenPopulated_Title = false;
			this.internal_Title = System.Data.SqlTypes.SqlString.Null;
		}


		private void ProcessHiddenAndDisabledControlStatus(bool inAddingProcess) {

			// Title column
			this.Control_Title.Enabled = !this.mustBeDisabled_Title;
			this.Control_Title.Enabled = !this.mustBeHidden_Title;

			if (inAddingProcess) {

				if (this.hasBeenPopulated_Title) {

					if (this.internal_Title.IsNull) {

						this.Control_Title.Text = String.Empty;
					}
					else {

						this.Control_Title.Text = this.internal_Title.Value.ToString();
					}
				}
			}
		}

		/// <summary>
		/// Create a new instance of the Bob.Windows.Forms.WinForm_Title class.
		/// </summary>
		public WinForm_Title() {

			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
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

			this.labTitle = new System.Windows.Forms.Label();
			this.Control_Title = new WinForm_DatabaseTextBox();
			this.cmdOK = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// labTitle
			// 
			this.labTitle.AutoSize = true;
			this.labTitle.Location = new System.Drawing.Point(24, 24);
			this.labTitle.Name = "labTitle";
			this.labTitle.Size = new System.Drawing.Size(110, 13);
			this.labTitle.TabIndex = 0;
			this.labTitle.Text = "Title:";
			// 
			// Control_Title
			// 
			this.Control_Title.Location = new System.Drawing.Point(24, 40);
			this.Control_Title.Name = "Control_Title";
			this.Control_Title.NullIsAllow = false;
			this.Control_Title.InvalidBackColor = System.Drawing.Color.Red;
			this.Control_Title.Size = new System.Drawing.Size(440, 20);
			this.Control_Title.TabIndex = 1;
			this.Control_Title.DataSize = 10;
			this.Control_Title.MaxLength = 10;
			this.Control_Title.TextChanged += new System.EventHandler(this.Control_Title_TextChanged);
			this.Control_Title.DBType = SupportedDatabaseTypes.DBType_varchar;
			// 
			// cmdOK
			// 
			this.cmdOK.Location = new System.Drawing.Point(304, 80);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.TabIndex = 2;
			this.cmdOK.Text = "OK";
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(392, 80);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.TabIndex = 2;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// WinForm_Title
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 13);
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(506, 120);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
						this.labTitle,
						this.Control_Title,
						this.cmdOK,
						this.cmdCancel
						});
			this.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "WinForm_Title";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Title management";
			this.ResumeLayout(false);
		}
		#endregion

		private string connectionString = String.Empty;
		private System.Data.SqlClient.SqlConnection sqlConnection = null;
		private Bob.DataClasses.ConnectionType lastKnownConnectionType = Bob.DataClasses.ConnectionType.None;
		private System.Data.SqlTypes.SqlInt32 currentID;
		private bool IgnoreChanges = false;
		private Bob.DataClasses.IParameter parameter = null;
		private bool errorHasOccured = false;

		/// <summary>
		/// Returns the current type of connection that is going or has been
		/// used against the Sql Server database.
		/// </summary>
		public Bob.DataClasses.ConnectionType ConnectionType {

			get {

				return(this.lastKnownConnectionType);
			}
		}

		/// <summary>
		/// Returns the connection string used to access the Sql Server database.
		/// </summary>
		public string ConnectionString {

			get {

				if (this.lastKnownConnectionType != Bob.DataClasses.ConnectionType.ConnectionString) {

					throw new InvalidOperationException("The connection string was not set in the class constructor.");
				}

				return(this.connectionString);
			}
		}

		/// <summary>
		/// Returns the SqlConnection used to access the Sql Server database.
		/// </summary>
		public System.Data.SqlClient.SqlConnection SqlConnection {

			get {

				if (this.lastKnownConnectionType != Bob.DataClasses.ConnectionType.SqlConnection) {

					throw new InvalidOperationException("The SqlConnection was not set in the class constructor.");
				}

				return(this.sqlConnection );
			}
		}

		/// <summary>
		/// Returns the Parameter (Bob.DataClasses.IParameter) that was used to perform
		/// the latest insertion or update.
		/// </summary>
		public Bob.DataClasses.IParameter Parameter {

			get {

				return(parameter);
			}
		}

		/// <summary>
		/// Returns True if the last call to AddNewRecord or
		/// EditExistingRecord was unsuccessful. Returns False otherwise.
		/// </summary>
		public bool ErrorHasOccured {

			get {

				return(errorHasOccured);
			}
		}

		private void EmptyControls() {

			this.IgnoreChanges = true;

			Control_Title.Text = String.Empty;


			this.IgnoreChanges = false;
		}

		/// <summary>
		/// Allows you to add a new record in the Title table.
		/// </summary>
		/// <param name="caller">The IWin32Window this form will display in front of.</param>
		/// <param name="formTitle">Title of this form.</param>
		/// <param name="connectionString">A valid connection string.</param>
		public void AddNewRecord(System.Windows.Forms.IWin32Window caller, string formTitle, string connectionString) {

			this.Text = formTitle;

			this.lastKnownConnectionType = Bob.DataClasses.ConnectionType.ConnectionString;
			this.connectionString = connectionString;

			RequestForeignTables();
			EmptyControls();

			this.Control_Title.HandleTextChanged(null, null);

			Check_cmdOK_Button(null);

			ProcessHiddenAndDisabledControlStatus(true);

			this.ShowDialog(caller);
		}

		/// <summary>
		/// Allows you to add a new record in the Title table.
		/// </summary>
		/// <param name="caller">The IWin32Window this form will display in front of.</param>
		/// <param name="formTitle">Title of this form.</param>
		/// <param name="sqlConnection">A valid System.Data.SqlClient.SqlConnection.</param>
		public void AddNewRecord(System.Windows.Forms.IWin32Window caller, string formTitle, System.Data.SqlClient.SqlConnection sqlConnection) {

			this.Text = formTitle;

			this.lastKnownConnectionType = Bob.DataClasses.ConnectionType.SqlConnection;
			this.sqlConnection = sqlConnection;

			RequestForeignTables();
			EmptyControls();

			this.Control_Title.HandleTextChanged(null, null);

			Check_cmdOK_Button(null);

			ProcessHiddenAndDisabledControlStatus(true);

			this.ShowDialog(caller);
		}

		/// <summary>
		/// Allows you to update an existing record from the Title table.
		/// </summary>
		/// <param name="caller">The IWin32Window this form will display in front of.</param>
		/// <param name="formTitle">Title of this form.</param>
		/// <param name="connectionString">A valid connection string.</param>
		/// <param name="id">Primary key of the record to update.</param>
		/// <returns>
		/// Returns True if the record was found. Returns False otherwise
		/// (no form is displayed in this case).
		/// </returns>
		public bool EditExistingRecord(System.Windows.Forms.IWin32Window caller, string formTitle, string connectionString, System.Data.SqlTypes.SqlInt32 id) {

			this.Text = formTitle;

			this.lastKnownConnectionType = Bob.DataClasses.ConnectionType.ConnectionString;
			this.connectionString = connectionString;
			this.currentID = id;

			RequestForeignTables();
			EmptyControls();

			if (!RefreshCurrentRecord()) {

				return(false);
			}

			Check_cmdOK_Button(null);

			ProcessHiddenAndDisabledControlStatus(false);

			this.ShowDialog(caller);

			return(true);
		}

		/// <summary>
		/// Allows you to update an existing record from the Title table.
		/// </summary>
		/// <param name="caller">The IWin32Window this form will display in front of.</param>
		/// <param name="formTitle">Title of this form.</param>
		/// <param name="sqlConnection">A valid System.Data.SqlClient.SqlConnection.</param>
		/// <param name="id">Primary key of the record to update.</param>
		/// <returns>
		/// Returns True if the record was found. Returns False otherwise
		/// (no form is displayed in this case).
		/// </returns>
		public bool EditExistingRecord(System.Windows.Forms.IWin32Window caller, string formTitle, System.Data.SqlClient.SqlConnection sqlConnection, System.Data.SqlTypes.SqlInt32 id) {

			this.Text = formTitle;

			this.lastKnownConnectionType = Bob.DataClasses.ConnectionType.SqlConnection;
			this.sqlConnection = sqlConnection;
			this.currentID = id;

			RequestForeignTables();
			EmptyControls();

			if (!RefreshCurrentRecord()) {

				return(false);
			}

			Check_cmdOK_Button(null);

			ProcessHiddenAndDisabledControlStatus(false);

			this.ShowDialog(caller);

			return(true);
		}

		private void Check_cmdOK_Button(System.Windows.Forms.Control CurrentControl) {

			bool Status = true;

			if (CurrentControl == null) {

				Status = (Status && Control_Title.IsValid);
			}
			else {

				System.Type CurrentType = CurrentControl.GetType();
				if (CurrentType == typeof(WinForm_DatabaseTextBox)) {

					Status = ((WinForm_DatabaseTextBox)CurrentControl).IsValid;
				}
				else if (CurrentType == typeof(System.Windows.Forms.ComboBox)) {

					Status = (((System.Windows.Forms.ComboBox)CurrentControl).SelectedIndex != -1);
				}
			}

			cmdOK.Enabled = Status;
		}

		private void RequestForeignTables() {

		}

		private bool RefreshCurrentRecord() {

			EmptyControls();

			Abstracts.Abstract_Title oAbstract_Title = null;
			switch (this.lastKnownConnectionType) {
				case Bob.DataClasses.ConnectionType.ConnectionString:
					oAbstract_Title = new Abstracts.Abstract_Title(this.connectionString);
					break;

				case Bob.DataClasses.ConnectionType.SqlConnection:
					oAbstract_Title = new Abstracts.Abstract_Title(this.sqlConnection);
					break;
			}

			if (oAbstract_Title.Refresh(this.currentID)) {

				this.IgnoreChanges = true;

				if (!oAbstract_Title.Col_Title.IsNull) {

					Control_Title.Text = oAbstract_Title.Col_Title.Value.ToString();
				}


				this.IgnoreChanges = false;
				return(true);
			}
			
			else {

				MessageBox.Show("The record does not exist any more !", "Record not found", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return(false);
			}
		}

		private void Control_Title_TextChanged(object sender, System.EventArgs e) {

			if (!this.IgnoreChanges) {

				Check_cmdOK_Button(Control_Title);
			}
		}

		private void cmdOK_Click(object sender, System.EventArgs e) {

			this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

			this.DialogResult = DialogResult.OK;

			this.errorHasOccured = false;
			if (this.currentID.IsNull) {

				AddRecordInDatabase();
			}
			else {

				UpdateRecordInDatabase();
			}

			if (!this.errorHasOccured) {

				this.Close();
			}

			this.Cursor = System.Windows.Forms.Cursors.Default;
		}

		private void cmdCancel_Click(object sender, System.EventArgs e) {

			this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

			this.DialogResult = DialogResult.Cancel;
			this.Close();

			this.Cursor = System.Windows.Forms.Cursors.Default;
		}

		private void AddRecordInDatabase() {

			Params.spI_Title Param = new Params.spI_Title(false);

			switch (this.lastKnownConnectionType) {
				case Bob.DataClasses.ConnectionType.ConnectionString:
					Param.SetUpConnection(this.connectionString);
					break;

				case Bob.DataClasses.ConnectionType.SqlConnection:
					Param.SetUpConnection(this.sqlConnection);
					break;
			}

			if (Control_Title.Text.Trim() != String.Empty) {

				Param.Param_Title = (System.Data.SqlTypes.SqlString)Control_Title.GetSqlTypesValue;
			}

			SPs.spI_Title SP = new SPs.spI_Title(false);

			if (SP.Execute(ref Param)) {

				this.parameter = Param;
				this.errorHasOccured = false;
			}
			else {

				this.errorHasOccured = true;
				if (Param.SqlException != null && Param.SqlException.Number == 2627) {

					MessageBox.Show(this, "Unable to add this record:\r\n\r\n" + Param.SqlException.Message, "Error");
				}
				else {

					throw new Bob.DataClasses.CustomException(Param, "Bob.Windows.Forms.WinForm_Title", "AddRecordInDatabase");
				}
			}
		}

		private void UpdateRecordInDatabase() {

			Params.spU_Title Param = new Params.spU_Title(false);
			
			switch (this.lastKnownConnectionType) {
				case Bob.DataClasses.ConnectionType.ConnectionString:
					Param.SetUpConnection(this.connectionString);
					break;

				case Bob.DataClasses.ConnectionType.SqlConnection:
					Param.SetUpConnection(this.sqlConnection);
					break;
			}

			Param.Param_TitleId = this.currentID;

			if (Control_Title.Text.Trim() != String.Empty) {

				Param.Param_Title = (System.Data.SqlTypes.SqlString)Control_Title.GetSqlTypesValue;
			}

			SPs.spU_Title SP = new SPs.spU_Title(false);

			if (SP.Execute(ref Param)) {

				this.parameter = Param;
				this.errorHasOccured = false;
			}
			else {

				this.errorHasOccured = true;
				if (Param.SqlException != null && Param.SqlException.Number == 2627) {

					MessageBox.Show(this, "Unable to update this record:\r\n\r\n" + Param.SqlException.Message, "Error");
				}
				else {

					throw new Bob.DataClasses.CustomException(Param, "Bob.Windows.Forms.WinForm_Title", "UpdateRecordInDatabase");
				}
			}
		}

	}
}
