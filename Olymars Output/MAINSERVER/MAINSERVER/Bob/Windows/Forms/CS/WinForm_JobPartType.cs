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
	/// designed for the 'JobPartType' table.
	/// </summary>
	public class WinForm_JobPartType : System.Windows.Forms.Form {

		private bool mustBeHidden_Description = false;
		private bool mustBeDisabled_Description = false;
		private bool hasBeenPopulated_Description = false;
		private System.Data.SqlTypes.SqlString internal_Description = System.Data.SqlTypes.SqlString.Null;
		private System.Windows.Forms.Label labDescription;
		private WinForm_DatabaseTextBox Control_Description;
		private bool mustBeHidden_GeneralUnitCost = false;
		private bool mustBeDisabled_GeneralUnitCost = false;
		private bool hasBeenPopulated_GeneralUnitCost = false;
		private System.Data.SqlTypes.SqlMoney internal_GeneralUnitCost = System.Data.SqlTypes.SqlMoney.Null;
		private System.Windows.Forms.Label labGeneralUnitCost;
		private WinForm_DatabaseTextBox Control_GeneralUnitCost;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Button cmdCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'Description' column must be hidden.
		/// </summary>
		public bool MustBeHidden_Description {

			get {

				return(this.mustBeHidden_Description);
			}

			set {

				this.mustBeHidden_Description = value;
			}
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'Description' column must be disabled.
		/// </summary>
		public bool MustBeDisabled_Description {

			get {

				return(this.mustBeDisabled_Description);
			}

			set {

				this.mustBeDisabled_Description = value;
			}
		}

		/// <summary>
		/// Allows you to pre-select a value for the 'Description' column.
		/// </summary>
		public void SetValue_Description(System.Data.SqlTypes.SqlString value) {

			this.hasBeenPopulated_Description = true;
			this.internal_Description = value;
		}

		/// <summary>
		/// Allows you to reset the pre-selected value for the 'Description' column.
		/// </summary>
		public void ResetValue_Description() {

			this.hasBeenPopulated_Description = false;
			this.internal_Description = System.Data.SqlTypes.SqlString.Null;
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'GeneralUnitCost' column must be hidden.
		/// </summary>
		public bool MustBeHidden_GeneralUnitCost {

			get {

				return(this.mustBeHidden_GeneralUnitCost);
			}

			set {

				this.mustBeHidden_GeneralUnitCost = value;
			}
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'GeneralUnitCost' column must be disabled.
		/// </summary>
		public bool MustBeDisabled_GeneralUnitCost {

			get {

				return(this.mustBeDisabled_GeneralUnitCost);
			}

			set {

				this.mustBeDisabled_GeneralUnitCost = value;
			}
		}

		/// <summary>
		/// Allows you to pre-select a value for the 'GeneralUnitCost' column.
		/// </summary>
		public void SetValue_GeneralUnitCost(System.Data.SqlTypes.SqlMoney value) {

			this.hasBeenPopulated_GeneralUnitCost = true;
			this.internal_GeneralUnitCost = value;
		}

		/// <summary>
		/// Allows you to reset the pre-selected value for the 'GeneralUnitCost' column.
		/// </summary>
		public void ResetValue_GeneralUnitCost() {

			this.hasBeenPopulated_GeneralUnitCost = false;
			this.internal_GeneralUnitCost = System.Data.SqlTypes.SqlMoney.Null;
		}


		private void ProcessHiddenAndDisabledControlStatus(bool inAddingProcess) {

			// Description column
			this.Control_Description.Enabled = !this.mustBeDisabled_Description;
			this.Control_Description.Enabled = !this.mustBeHidden_Description;

			if (inAddingProcess) {

				if (this.hasBeenPopulated_Description) {

					if (this.internal_Description.IsNull) {

						this.Control_Description.Text = String.Empty;
					}
					else {

						this.Control_Description.Text = this.internal_Description.Value.ToString();
					}
				}
			}

			// GeneralUnitCost column
			this.Control_GeneralUnitCost.Enabled = !this.mustBeDisabled_GeneralUnitCost;
			this.Control_GeneralUnitCost.Enabled = !this.mustBeHidden_GeneralUnitCost;

			if (inAddingProcess) {

				if (this.hasBeenPopulated_GeneralUnitCost) {

					if (this.internal_GeneralUnitCost.IsNull) {

						this.Control_GeneralUnitCost.Text = String.Empty;
					}
					else {

						this.Control_GeneralUnitCost.Text = this.internal_GeneralUnitCost.Value.ToString();
					}
				}
			}
		}

		/// <summary>
		/// Create a new instance of the Bob.Windows.Forms.WinForm_JobPartType class.
		/// </summary>
		public WinForm_JobPartType() {

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

			this.labDescription = new System.Windows.Forms.Label();
			this.Control_Description = new WinForm_DatabaseTextBox();
			this.labGeneralUnitCost = new System.Windows.Forms.Label();
			this.Control_GeneralUnitCost = new WinForm_DatabaseTextBox();
			this.cmdOK = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// labDescription
			// 
			this.labDescription.AutoSize = true;
			this.labDescription.Location = new System.Drawing.Point(24, 24);
			this.labDescription.Name = "labDescription";
			this.labDescription.Size = new System.Drawing.Size(110, 13);
			this.labDescription.TabIndex = 0;
			this.labDescription.Text = "Description (update this label in the \"Olymars/ColumnLabel\" extended property of the \"Description\" column):";
			// 
			// Control_Description
			// 
			this.Control_Description.Location = new System.Drawing.Point(24, 40);
			this.Control_Description.Name = "Control_Description";
			this.Control_Description.NullIsAllow = false;
			this.Control_Description.InvalidBackColor = System.Drawing.Color.Red;
			this.Control_Description.Size = new System.Drawing.Size(440, 20);
			this.Control_Description.TabIndex = 1;
			this.Control_Description.DataSize = 255;
			this.Control_Description.MaxLength = 255;
			this.Control_Description.TextChanged += new System.EventHandler(this.Control_Description_TextChanged);
			this.Control_Description.DBType = SupportedDatabaseTypes.DBType_varchar;
			// 
			// labGeneralUnitCost
			// 
			this.labGeneralUnitCost.AutoSize = true;
			this.labGeneralUnitCost.Location = new System.Drawing.Point(24, 80);
			this.labGeneralUnitCost.Name = "labGeneralUnitCost";
			this.labGeneralUnitCost.Size = new System.Drawing.Size(110, 13);
			this.labGeneralUnitCost.TabIndex = 2;
			this.labGeneralUnitCost.Text = "GeneralUnitCost (update this label in the \"Olymars/ColumnLabel\" extended property of the \"GeneralUnitCost\" column):";
			// 
			// Control_GeneralUnitCost
			// 
			this.Control_GeneralUnitCost.Location = new System.Drawing.Point(24, 96);
			this.Control_GeneralUnitCost.Name = "Control_GeneralUnitCost";
			this.Control_GeneralUnitCost.NullIsAllow = false;
			this.Control_GeneralUnitCost.InvalidBackColor = System.Drawing.Color.Red;
			this.Control_GeneralUnitCost.Size = new System.Drawing.Size(440, 20);
			this.Control_GeneralUnitCost.TabIndex = 3;
			this.Control_GeneralUnitCost.DataSize = 21;
			this.Control_GeneralUnitCost.TextChanged += new System.EventHandler(this.Control_GeneralUnitCost_TextChanged);
			this.Control_GeneralUnitCost.DBType = SupportedDatabaseTypes.DBType_money;
			// 
			// cmdOK
			// 
			this.cmdOK.Location = new System.Drawing.Point(304, 136);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.TabIndex = 4;
			this.cmdOK.Text = "OK";
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(392, 136);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.TabIndex = 4;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// WinForm_JobPartType
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 13);
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(506, 176);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
						this.labDescription,
						this.Control_Description,
						this.labGeneralUnitCost,
						this.Control_GeneralUnitCost,
						this.cmdOK,
						this.cmdCancel
						});
			this.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "WinForm_JobPartType";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "JobPartType management";
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

			Control_Description.Text = String.Empty;

			Control_GeneralUnitCost.Text = String.Empty;


			this.IgnoreChanges = false;
		}

		/// <summary>
		/// Allows you to add a new record in the JobPartType table.
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

			this.Control_Description.HandleTextChanged(null, null);
			this.Control_GeneralUnitCost.HandleTextChanged(null, null);

			Check_cmdOK_Button(null);

			ProcessHiddenAndDisabledControlStatus(true);

			this.ShowDialog(caller);
		}

		/// <summary>
		/// Allows you to add a new record in the JobPartType table.
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

			this.Control_Description.HandleTextChanged(null, null);
			this.Control_GeneralUnitCost.HandleTextChanged(null, null);

			Check_cmdOK_Button(null);

			ProcessHiddenAndDisabledControlStatus(true);

			this.ShowDialog(caller);
		}

		/// <summary>
		/// Allows you to update an existing record from the JobPartType table.
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
		/// Allows you to update an existing record from the JobPartType table.
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

				Status = (Status && Control_Description.IsValid);
				Status = (Status && Control_GeneralUnitCost.IsValid);
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

			Abstracts.Abstract_JobPartType oAbstract_JobPartType = null;
			switch (this.lastKnownConnectionType) {
				case Bob.DataClasses.ConnectionType.ConnectionString:
					oAbstract_JobPartType = new Abstracts.Abstract_JobPartType(this.connectionString);
					break;

				case Bob.DataClasses.ConnectionType.SqlConnection:
					oAbstract_JobPartType = new Abstracts.Abstract_JobPartType(this.sqlConnection);
					break;
			}

			if (oAbstract_JobPartType.Refresh(this.currentID)) {

				this.IgnoreChanges = true;

				if (!oAbstract_JobPartType.Col_Description.IsNull) {

					Control_Description.Text = oAbstract_JobPartType.Col_Description.Value.ToString();
				}


				if (!oAbstract_JobPartType.Col_GeneralUnitCost.IsNull) {

					Control_GeneralUnitCost.Text = oAbstract_JobPartType.Col_GeneralUnitCost.Value.ToString();
				}


				this.IgnoreChanges = false;
				return(true);
			}
			
			else {

				MessageBox.Show("The record does not exist any more !", "Record not found", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return(false);
			}
		}

		private void Control_Description_TextChanged(object sender, System.EventArgs e) {

			if (!this.IgnoreChanges) {

				Check_cmdOK_Button(Control_Description);
			}
		}

		private void Control_GeneralUnitCost_TextChanged(object sender, System.EventArgs e) {

			if (!this.IgnoreChanges) {

				Check_cmdOK_Button(Control_GeneralUnitCost);
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

			Params.spI_JobPartType Param = new Params.spI_JobPartType(false);

			switch (this.lastKnownConnectionType) {
				case Bob.DataClasses.ConnectionType.ConnectionString:
					Param.SetUpConnection(this.connectionString);
					break;

				case Bob.DataClasses.ConnectionType.SqlConnection:
					Param.SetUpConnection(this.sqlConnection);
					break;
			}

			if (Control_Description.Text.Trim() != String.Empty) {

				Param.Param_Description = (System.Data.SqlTypes.SqlString)Control_Description.GetSqlTypesValue;
			}

			if (Control_GeneralUnitCost.Text.Trim() != String.Empty) {

				Param.Param_GeneralUnitCost = (System.Data.SqlTypes.SqlMoney)Control_GeneralUnitCost.GetSqlTypesValue;
			}

			SPs.spI_JobPartType SP = new SPs.spI_JobPartType(false);

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

					throw new Bob.DataClasses.CustomException(Param, "Bob.Windows.Forms.WinForm_JobPartType", "AddRecordInDatabase");
				}
			}
		}

		private void UpdateRecordInDatabase() {

			Params.spU_JobPartType Param = new Params.spU_JobPartType(false);
			
			switch (this.lastKnownConnectionType) {
				case Bob.DataClasses.ConnectionType.ConnectionString:
					Param.SetUpConnection(this.connectionString);
					break;

				case Bob.DataClasses.ConnectionType.SqlConnection:
					Param.SetUpConnection(this.sqlConnection);
					break;
			}

			Param.Param_JobPartTypeId = this.currentID;

			if (Control_Description.Text.Trim() != String.Empty) {

				Param.Param_Description = (System.Data.SqlTypes.SqlString)Control_Description.GetSqlTypesValue;
			}

			if (Control_GeneralUnitCost.Text.Trim() != String.Empty) {

				Param.Param_GeneralUnitCost = (System.Data.SqlTypes.SqlMoney)Control_GeneralUnitCost.GetSqlTypesValue;
			}

			SPs.spU_JobPartType SP = new SPs.spU_JobPartType(false);

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

					throw new Bob.DataClasses.CustomException(Param, "Bob.Windows.Forms.WinForm_JobPartType", "UpdateRecordInDatabase");
				}
			}
		}

	}
}
