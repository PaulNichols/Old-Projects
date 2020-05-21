/*
	This C# source code was automatically generated using:

		SQL Server Centric .NET Code Generator
			v 1.0.1697.21165

			Generation Date: 27/12/2004 16:08:32
			Generator name: MAINSERVER\Administrator
			Template last update: 13/10/2003 04:51:40
			Template revision: 56177501

			SQL Server version: 08.00.0760
			Server: MAINSERVER\MAINSERVER
			Database: [OlymarsDemo]

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
using SPs = OlymarsDemo.DataClasses.StoredProcedures;
using Params = OlymarsDemo.DataClasses.Parameters;
using Abstracts = OlymarsDemo.AbstractClasses;

namespace OlymarsDemo.Windows.Forms {

	/// <summary>
	/// This class derives from the System.Windows.Forms.Form class and was built
	/// to allow you to interact (both for insertion AND update) with a form spefically
	/// designed for the 'tblOrder' table.
	/// </summary>
	public class WinForm_tblOrder : System.Windows.Forms.Form {

		private bool mustBeHidden_Ord_DatOrderedOn = false;
		private bool mustBeDisabled_Ord_DatOrderedOn = false;
		private bool hasBeenPopulated_Ord_DatOrderedOn = false;
		private System.Data.SqlTypes.SqlDateTime internal_Ord_DatOrderedOn = System.Data.SqlTypes.SqlDateTime.Null;
		private System.Windows.Forms.Label labOrd_DatOrderedOn;
		private WinForm_DatabaseTextBox Control_Ord_DatOrderedOn;
		private bool mustBeHidden_Ord_LngCustomerID = false;
		private bool mustBeDisabled_Ord_LngCustomerID = false;
		private bool hasBeenPopulated_Ord_LngCustomerID = false;
		private System.Data.SqlTypes.SqlInt32 internal_Ord_LngCustomerID = System.Data.SqlTypes.SqlInt32.Null;
		private System.Windows.Forms.Label labOrd_LngCustomerID;
		private OlymarsDemo.Windows.ComboBoxes.WinComboBox_tblCustomer Control_Ord_LngCustomerID;
		private System.Windows.Forms.Button Button_Ord_LngCustomerID;
		private bool mustBeHidden_Ord_CurTotal = false;
		private bool mustBeDisabled_Ord_CurTotal = false;
		private bool hasBeenPopulated_Ord_CurTotal = false;
		private System.Data.SqlTypes.SqlMoney internal_Ord_CurTotal = System.Data.SqlTypes.SqlMoney.Null;
		private System.Windows.Forms.Label labOrd_CurTotal;
		private WinForm_DatabaseTextBox Control_Ord_CurTotal;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Button cmdCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'Ord_DatOrderedOn' column must be hidden.
		/// </summary>
		public bool MustBeHidden_Ord_DatOrderedOn {

			get {

				return(this.mustBeHidden_Ord_DatOrderedOn);
			}

			set {

				this.mustBeHidden_Ord_DatOrderedOn = value;
			}
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'Ord_DatOrderedOn' column must be disabled.
		/// </summary>
		public bool MustBeDisabled_Ord_DatOrderedOn {

			get {

				return(this.mustBeDisabled_Ord_DatOrderedOn);
			}

			set {

				this.mustBeDisabled_Ord_DatOrderedOn = value;
			}
		}

		/// <summary>
		/// Allows you to pre-select a value for the 'Ord_DatOrderedOn' column.
		/// </summary>
		public void SetValue_Ord_DatOrderedOn(System.Data.SqlTypes.SqlDateTime value) {

			this.hasBeenPopulated_Ord_DatOrderedOn = true;
			this.internal_Ord_DatOrderedOn = value;
		}

		/// <summary>
		/// Allows you to reset the pre-selected value for the 'Ord_DatOrderedOn' column.
		/// </summary>
		public void ResetValue_Ord_DatOrderedOn() {

			this.hasBeenPopulated_Ord_DatOrderedOn = false;
			this.internal_Ord_DatOrderedOn = System.Data.SqlTypes.SqlDateTime.Null;
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'Ord_LngCustomerID' column must be hidden.
		/// </summary>
		public bool MustBeHidden_Ord_LngCustomerID {

			get {

				return(this.mustBeHidden_Ord_LngCustomerID);
			}

			set {

				this.mustBeHidden_Ord_LngCustomerID = value;
			}
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'Ord_LngCustomerID' column must be disabled.
		/// </summary>
		public bool MustBeDisabled_Ord_LngCustomerID {

			get {

				return(this.mustBeDisabled_Ord_LngCustomerID);
			}

			set {

				this.mustBeDisabled_Ord_LngCustomerID = value;
			}
		}

		/// <summary>
		/// Allows you to pre-select a value for the 'Ord_LngCustomerID' column.
		/// </summary>
		public void SetValue_Ord_LngCustomerID(System.Data.SqlTypes.SqlInt32 value) {

			this.hasBeenPopulated_Ord_LngCustomerID = true;
			this.internal_Ord_LngCustomerID = value;
		}

		/// <summary>
		/// Allows you to reset the pre-selected value for the 'Ord_LngCustomerID' column.
		/// </summary>
		public void ResetValue_Ord_LngCustomerID() {

			this.hasBeenPopulated_Ord_LngCustomerID = false;
			this.internal_Ord_LngCustomerID = System.Data.SqlTypes.SqlInt32.Null;
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'Ord_CurTotal' column must be hidden.
		/// </summary>
		public bool MustBeHidden_Ord_CurTotal {

			get {

				return(this.mustBeHidden_Ord_CurTotal);
			}

			set {

				this.mustBeHidden_Ord_CurTotal = value;
			}
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'Ord_CurTotal' column must be disabled.
		/// </summary>
		public bool MustBeDisabled_Ord_CurTotal {

			get {

				return(this.mustBeDisabled_Ord_CurTotal);
			}

			set {

				this.mustBeDisabled_Ord_CurTotal = value;
			}
		}

		/// <summary>
		/// Allows you to pre-select a value for the 'Ord_CurTotal' column.
		/// </summary>
		public void SetValue_Ord_CurTotal(System.Data.SqlTypes.SqlMoney value) {

			this.hasBeenPopulated_Ord_CurTotal = true;
			this.internal_Ord_CurTotal = value;
		}

		/// <summary>
		/// Allows you to reset the pre-selected value for the 'Ord_CurTotal' column.
		/// </summary>
		public void ResetValue_Ord_CurTotal() {

			this.hasBeenPopulated_Ord_CurTotal = false;
			this.internal_Ord_CurTotal = System.Data.SqlTypes.SqlMoney.Null;
		}


		private void ProcessHiddenAndDisabledControlStatus(bool inAddingProcess) {

			// Ord_DatOrderedOn column
			this.Control_Ord_DatOrderedOn.Enabled = !this.mustBeDisabled_Ord_DatOrderedOn;
			this.Control_Ord_DatOrderedOn.Enabled = !this.mustBeHidden_Ord_DatOrderedOn;

			if (inAddingProcess) {

				if (this.hasBeenPopulated_Ord_DatOrderedOn) {

					if (this.internal_Ord_DatOrderedOn.IsNull) {

						this.Control_Ord_DatOrderedOn.Text = String.Empty;
					}
					else {

						this.Control_Ord_DatOrderedOn.Text = this.internal_Ord_DatOrderedOn.Value.ToString();
					}
				}
			}

			// Ord_LngCustomerID column
			if (this.mustBeDisabled_Ord_LngCustomerID) {

				this.Control_Ord_LngCustomerID.Enabled = false;
				this.Button_Ord_LngCustomerID.Enabled = false;
			}

			if (this.mustBeHidden_Ord_LngCustomerID) {

				this.Control_Ord_LngCustomerID.Visible = false;
				this.Button_Ord_LngCustomerID.Visible = false;
			}

			if (inAddingProcess) {

				if (this.hasBeenPopulated_Ord_LngCustomerID) {

					if (this.internal_Ord_LngCustomerID.IsNull) {

						this.Control_Ord_LngCustomerID.SelectedIndex = -1;
					}
					else {

						this.Control_Ord_LngCustomerID.SelectedValue = this.internal_Ord_LngCustomerID.Value;
					}
				}
			}

			// Ord_CurTotal column
			this.Control_Ord_CurTotal.Enabled = !this.mustBeDisabled_Ord_CurTotal;
			this.Control_Ord_CurTotal.Enabled = !this.mustBeHidden_Ord_CurTotal;

			if (inAddingProcess) {

				if (this.hasBeenPopulated_Ord_CurTotal) {

					if (this.internal_Ord_CurTotal.IsNull) {

						this.Control_Ord_CurTotal.Text = String.Empty;
					}
					else {

						this.Control_Ord_CurTotal.Text = this.internal_Ord_CurTotal.Value.ToString();
					}
				}
			}
		}

		/// <summary>
		/// Create a new instance of the OlymarsDemo.Windows.Forms.WinForm_tblOrder class.
		/// </summary>
		public WinForm_tblOrder() {

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

			this.labOrd_DatOrderedOn = new System.Windows.Forms.Label();
			this.Control_Ord_DatOrderedOn = new WinForm_DatabaseTextBox();
			this.labOrd_LngCustomerID = new System.Windows.Forms.Label();
			this.Control_Ord_LngCustomerID = new OlymarsDemo.Windows.ComboBoxes.WinComboBox_tblCustomer();
			this.Button_Ord_LngCustomerID = new System.Windows.Forms.Button();
			this.labOrd_CurTotal = new System.Windows.Forms.Label();
			this.Control_Ord_CurTotal = new WinForm_DatabaseTextBox();
			this.cmdOK = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// labOrd_DatOrderedOn
			// 
			this.labOrd_DatOrderedOn.AutoSize = true;
			this.labOrd_DatOrderedOn.Location = new System.Drawing.Point(24, 24);
			this.labOrd_DatOrderedOn.Name = "labOrd_DatOrderedOn";
			this.labOrd_DatOrderedOn.Size = new System.Drawing.Size(110, 13);
			this.labOrd_DatOrderedOn.TabIndex = 0;
			this.labOrd_DatOrderedOn.Text = "Ord_DatOrderedOn (update this label in the \"Olymars/ColumnLabel\" extended property of the \"Ord_DatOrderedOn\" column):";
			// 
			// Control_Ord_DatOrderedOn
			// 
			this.Control_Ord_DatOrderedOn.Location = new System.Drawing.Point(24, 40);
			this.Control_Ord_DatOrderedOn.Name = "Control_Ord_DatOrderedOn";
			this.Control_Ord_DatOrderedOn.NullIsAllow = false;
			this.Control_Ord_DatOrderedOn.InvalidBackColor = System.Drawing.Color.Red;
			this.Control_Ord_DatOrderedOn.Size = new System.Drawing.Size(440, 20);
			this.Control_Ord_DatOrderedOn.TabIndex = 1;
			this.Control_Ord_DatOrderedOn.DataSize = 16;
			this.Control_Ord_DatOrderedOn.TextChanged += new System.EventHandler(this.Control_Ord_DatOrderedOn_TextChanged);
			this.Control_Ord_DatOrderedOn.DBType = SupportedDatabaseTypes.DBType_datetime;
			// 
			// labOrd_LngCustomerID
			// 
			this.labOrd_LngCustomerID.AutoSize = true;
			this.labOrd_LngCustomerID.Location = new System.Drawing.Point(24, 80);
			this.labOrd_LngCustomerID.Name = "labOrd_LngCustomerID";
			this.labOrd_LngCustomerID.Size = new System.Drawing.Size(126, 13);
			this.labOrd_LngCustomerID.TabIndex = 2;
			this.labOrd_LngCustomerID.Text = "Ord_LngCustomerID (update this label in the \"Olymars/ColumnLabel\" extended property of the \"Ord_LngCustomerID\" column):";
			// 
			// Control_Ord_LngCustomerID
			// 
			this.Control_Ord_LngCustomerID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Control_Ord_LngCustomerID.DropDownWidth = 392;
			this.Control_Ord_LngCustomerID.Location = new System.Drawing.Point(24, 96);
			this.Control_Ord_LngCustomerID.Name = "Control_Ord_LngCustomerID";
			this.Control_Ord_LngCustomerID.Size = new System.Drawing.Size(352, 21);
			this.Control_Ord_LngCustomerID.TabIndex = 3;
			this.Control_Ord_LngCustomerID.SelectedIndexChanged += new System.EventHandler(this.Control_Ord_LngCustomerID_SelectedIndexChanged);
			// 
			// Button_Ord_LngCustomerID
			// 
			this.Button_Ord_LngCustomerID.Location = new System.Drawing.Point(392, 96);
			this.Button_Ord_LngCustomerID.Name = "Button_Ord_LngCustomerID";
			this.Button_Ord_LngCustomerID.TabIndex = 4;
			this.Button_Ord_LngCustomerID.Text = "Add";
			this.Button_Ord_LngCustomerID.Click += new System.EventHandler(this.Button_Ord_LngCustomerID_Click);
			// 
			// labOrd_CurTotal
			// 
			this.labOrd_CurTotal.AutoSize = true;
			this.labOrd_CurTotal.Location = new System.Drawing.Point(24, 136);
			this.labOrd_CurTotal.Name = "labOrd_CurTotal";
			this.labOrd_CurTotal.Size = new System.Drawing.Size(110, 13);
			this.labOrd_CurTotal.TabIndex = 5;
			this.labOrd_CurTotal.Text = "Ord_CurTotal (update this label in the \"Olymars/ColumnLabel\" extended property of the \"Ord_CurTotal\" column):";
			// 
			// Control_Ord_CurTotal
			// 
			this.Control_Ord_CurTotal.Location = new System.Drawing.Point(24, 152);
			this.Control_Ord_CurTotal.Name = "Control_Ord_CurTotal";
			this.Control_Ord_CurTotal.NullIsAllow = true;
			this.Control_Ord_CurTotal.InvalidBackColor = System.Drawing.Color.Red;
			this.Control_Ord_CurTotal.Size = new System.Drawing.Size(440, 20);
			this.Control_Ord_CurTotal.TabIndex = 6;
			this.Control_Ord_CurTotal.DataSize = 21;
			this.Control_Ord_CurTotal.TextChanged += new System.EventHandler(this.Control_Ord_CurTotal_TextChanged);
			this.Control_Ord_CurTotal.DBType = SupportedDatabaseTypes.DBType_money;
			// 
			// cmdOK
			// 
			this.cmdOK.Location = new System.Drawing.Point(304, 192);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.TabIndex = 7;
			this.cmdOK.Text = "OK";
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(392, 192);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.TabIndex = 7;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// WinForm_tblOrder
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 13);
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(506, 232);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
						this.labOrd_DatOrderedOn,
						this.Control_Ord_DatOrderedOn,
						this.labOrd_LngCustomerID,
						this.Control_Ord_LngCustomerID,
						this.Button_Ord_LngCustomerID,
						this.labOrd_CurTotal,
						this.Control_Ord_CurTotal,
						this.cmdOK,
						this.cmdCancel
						});
			this.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "WinForm_tblOrder";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "tblOrder management";
			this.ResumeLayout(false);
		}
		#endregion

		private string connectionString = String.Empty;
		private System.Data.SqlClient.SqlConnection sqlConnection = null;
		private OlymarsDemo.DataClasses.ConnectionType lastKnownConnectionType = OlymarsDemo.DataClasses.ConnectionType.None;
		private System.Data.SqlTypes.SqlGuid currentID;
		private bool IgnoreChanges = false;
		private OlymarsDemo.DataClasses.IParameter parameter = null;
		private bool errorHasOccured = false;

		/// <summary>
		/// Returns the current type of connection that is going or has been
		/// used against the Sql Server database.
		/// </summary>
		public OlymarsDemo.DataClasses.ConnectionType ConnectionType {

			get {

				return(this.lastKnownConnectionType);
			}
		}

		/// <summary>
		/// Returns the connection string used to access the Sql Server database.
		/// </summary>
		public string ConnectionString {

			get {

				if (this.lastKnownConnectionType != OlymarsDemo.DataClasses.ConnectionType.ConnectionString) {

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

				if (this.lastKnownConnectionType != OlymarsDemo.DataClasses.ConnectionType.SqlConnection) {

					throw new InvalidOperationException("The SqlConnection was not set in the class constructor.");
				}

				return(this.sqlConnection );
			}
		}

		/// <summary>
		/// Returns the Parameter (OlymarsDemo.DataClasses.IParameter) that was used to perform
		/// the latest insertion or update.
		/// </summary>
		public OlymarsDemo.DataClasses.IParameter Parameter {

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

			Control_Ord_DatOrderedOn.Text = String.Empty;

			Control_Ord_LngCustomerID.Enabled = true;
			if (Control_Ord_LngCustomerID.Items.Count != 0) {

				Control_Ord_LngCustomerID.SelectedIndex = 0;
			}

			Control_Ord_CurTotal.Text = String.Empty;


			this.IgnoreChanges = false;
		}

		/// <summary>
		/// Allows you to add a new record in the tblOrder table.
		/// </summary>
		/// <param name="caller">The IWin32Window this form will display in front of.</param>
		/// <param name="formTitle">Title of this form.</param>
		/// <param name="connectionString">A valid connection string.</param>
		public void AddNewRecord(System.Windows.Forms.IWin32Window caller, string formTitle, string connectionString) {

			this.Text = formTitle;

			this.lastKnownConnectionType = OlymarsDemo.DataClasses.ConnectionType.ConnectionString;
			this.connectionString = connectionString;

			RequestForeignTables();
			EmptyControls();

			this.Control_Ord_DatOrderedOn.HandleTextChanged(null, null);
			this.Control_Ord_CurTotal.HandleTextChanged(null, null);

			Check_cmdOK_Button(null);

			ProcessHiddenAndDisabledControlStatus(true);

			this.ShowDialog(caller);
		}

		/// <summary>
		/// Allows you to add a new record in the tblOrder table.
		/// </summary>
		/// <param name="caller">The IWin32Window this form will display in front of.</param>
		/// <param name="formTitle">Title of this form.</param>
		/// <param name="sqlConnection">A valid System.Data.SqlClient.SqlConnection.</param>
		public void AddNewRecord(System.Windows.Forms.IWin32Window caller, string formTitle, System.Data.SqlClient.SqlConnection sqlConnection) {

			this.Text = formTitle;

			this.lastKnownConnectionType = OlymarsDemo.DataClasses.ConnectionType.SqlConnection;
			this.sqlConnection = sqlConnection;

			RequestForeignTables();
			EmptyControls();

			this.Control_Ord_DatOrderedOn.HandleTextChanged(null, null);
			this.Control_Ord_CurTotal.HandleTextChanged(null, null);

			Check_cmdOK_Button(null);

			ProcessHiddenAndDisabledControlStatus(true);

			this.ShowDialog(caller);
		}

		/// <summary>
		/// Allows you to update an existing record from the tblOrder table.
		/// </summary>
		/// <param name="caller">The IWin32Window this form will display in front of.</param>
		/// <param name="formTitle">Title of this form.</param>
		/// <param name="connectionString">A valid connection string.</param>
		/// <param name="id">Primary key of the record to update.</param>
		/// <returns>
		/// Returns True if the record was found. Returns False otherwise
		/// (no form is displayed in this case).
		/// </returns>
		public bool EditExistingRecord(System.Windows.Forms.IWin32Window caller, string formTitle, string connectionString, System.Data.SqlTypes.SqlGuid id) {

			this.Text = formTitle;

			this.lastKnownConnectionType = OlymarsDemo.DataClasses.ConnectionType.ConnectionString;
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
		/// Allows you to update an existing record from the tblOrder table.
		/// </summary>
		/// <param name="caller">The IWin32Window this form will display in front of.</param>
		/// <param name="formTitle">Title of this form.</param>
		/// <param name="sqlConnection">A valid System.Data.SqlClient.SqlConnection.</param>
		/// <param name="id">Primary key of the record to update.</param>
		/// <returns>
		/// Returns True if the record was found. Returns False otherwise
		/// (no form is displayed in this case).
		/// </returns>
		public bool EditExistingRecord(System.Windows.Forms.IWin32Window caller, string formTitle, System.Data.SqlClient.SqlConnection sqlConnection, System.Data.SqlTypes.SqlGuid id) {

			this.Text = formTitle;

			this.lastKnownConnectionType = OlymarsDemo.DataClasses.ConnectionType.SqlConnection;
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

				Status = (Status && Control_Ord_DatOrderedOn.IsValid);
				Status = (Status && Control_Ord_LngCustomerID.SelectedIndex != -1);
				Status = (Status && Control_Ord_CurTotal.IsValid);
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

			Refresh_Ord_LngCustomerID();
		}

		private void Control_Ord_LngCustomerID_SelectedIndexChanged(object sender, System.EventArgs e) {

			if (!this.IgnoreChanges) Check_cmdOK_Button(Control_Ord_LngCustomerID);
		}


		private void Refresh_Ord_LngCustomerID() {

			this.IgnoreChanges = true;

			switch (this.lastKnownConnectionType) {
				case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
					Control_Ord_LngCustomerID.Initialize(this.connectionString);
					break;

				case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
					Control_Ord_LngCustomerID.Initialize(this.sqlConnection);
					break;
			}

			try {

				Control_Ord_LngCustomerID.RefreshData(System.Data.SqlTypes.SqlInt32.Null);
			}
			catch (OlymarsDemo.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinComboBox_tblCustomer' class. Exception message is: {0}", customException.Parameter.SqlException.Message), customException);
				}
				else if (customException.Parameter.OtherException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinComboBox_tblCustomer' class. Exception message is: {0}", customException.Parameter.OtherException.Message), customException);
				}
				else {

					throw;
				}
			}

			if (Control_Ord_LngCustomerID.Items.Count > 0) {

				Control_Ord_LngCustomerID.SelectedIndex = 0;
			}

			this.IgnoreChanges = false;
		}

		private bool RefreshCurrentRecord() {

			EmptyControls();

			Abstracts.Abstract_tblOrder oAbstract_tblOrder = null;
			switch (this.lastKnownConnectionType) {
				case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
					oAbstract_tblOrder = new Abstracts.Abstract_tblOrder(this.connectionString);
					break;

				case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
					oAbstract_tblOrder = new Abstracts.Abstract_tblOrder(this.sqlConnection);
					break;
			}

			if (oAbstract_tblOrder.Refresh(this.currentID)) {

				this.IgnoreChanges = true;

				if (!oAbstract_tblOrder.Col_Ord_DatOrderedOn.IsNull) {

					Control_Ord_DatOrderedOn.Text = oAbstract_tblOrder.Col_Ord_DatOrderedOn.Value.ToString();
				}


				if (!oAbstract_tblOrder.Col_Ord_LngCustomerID.IsNull) {

					Control_Ord_LngCustomerID.SelectedValue = oAbstract_tblOrder.Col_Ord_LngCustomerID.Value;
				}

				if (!oAbstract_tblOrder.Col_Ord_CurTotal.IsNull) {

					Control_Ord_CurTotal.Text = oAbstract_tblOrder.Col_Ord_CurTotal.Value.ToString();
				}


				this.IgnoreChanges = false;
				return(true);
			}
			
			else {

				MessageBox.Show("The record does not exist any more !", "Record not found", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return(false);
			}
		}

		private void Control_Ord_DatOrderedOn_TextChanged(object sender, System.EventArgs e) {

			if (!this.IgnoreChanges) {

				Check_cmdOK_Button(Control_Ord_DatOrderedOn);
			}
		}

		private void Control_Ord_CurTotal_TextChanged(object sender, System.EventArgs e) {

			if (!this.IgnoreChanges) {

				Check_cmdOK_Button(Control_Ord_CurTotal);
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

			Params.spI_tblOrder Param = new Params.spI_tblOrder(false);

			switch (this.lastKnownConnectionType) {
				case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
					Param.SetUpConnection(this.connectionString);
					break;

				case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
					Param.SetUpConnection(this.sqlConnection);
					break;
			}

			if (Control_Ord_DatOrderedOn.Text.Trim() != String.Empty) {

				Param.Param_Ord_DatOrderedOn = (System.Data.SqlTypes.SqlDateTime)Control_Ord_DatOrderedOn.GetSqlTypesValue;
			}

			if (Control_Ord_LngCustomerID.SelectedIndex != -1) {

				Param.Param_Ord_LngCustomerID = (System.Int32)Control_Ord_LngCustomerID.SelectedValue;
			}

			if (Control_Ord_CurTotal.Text.Trim() != String.Empty) {

				Param.Param_Ord_CurTotal = (System.Data.SqlTypes.SqlMoney)Control_Ord_CurTotal.GetSqlTypesValue;
			}

			SPs.spI_tblOrder SP = new SPs.spI_tblOrder(false);

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

					throw new OlymarsDemo.DataClasses.CustomException(Param, "OlymarsDemo.Windows.Forms.WinForm_tblOrder", "AddRecordInDatabase");
				}
			}
		}

		private void UpdateRecordInDatabase() {

			Params.spU_tblOrder Param = new Params.spU_tblOrder(false);
			
			switch (this.lastKnownConnectionType) {
				case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
					Param.SetUpConnection(this.connectionString);
					break;

				case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
					Param.SetUpConnection(this.sqlConnection);
					break;
			}

			Param.Param_Ord_GuidID = this.currentID;

			if (Control_Ord_DatOrderedOn.Text.Trim() != String.Empty) {

				Param.Param_Ord_DatOrderedOn = (System.Data.SqlTypes.SqlDateTime)Control_Ord_DatOrderedOn.GetSqlTypesValue;
			}

			if (Control_Ord_LngCustomerID.SelectedIndex != -1) {

				Param.Param_Ord_LngCustomerID = (System.Int32)Control_Ord_LngCustomerID.SelectedValue;
			}

			if (Control_Ord_CurTotal.Text.Trim() != String.Empty) {

				Param.Param_Ord_CurTotal = (System.Data.SqlTypes.SqlMoney)Control_Ord_CurTotal.GetSqlTypesValue;
			}

			SPs.spU_tblOrder SP = new SPs.spU_tblOrder(false);

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

					throw new OlymarsDemo.DataClasses.CustomException(Param, "OlymarsDemo.Windows.Forms.WinForm_tblOrder", "UpdateRecordInDatabase");
				}
			}
		}

		private void Button_Ord_LngCustomerID_Click(object sender, System.EventArgs e) {

			AddNew_tblCustomer1(Control_Ord_LngCustomerID);
		}

		private void AddNew_tblCustomer1(System.Windows.Forms.ComboBox CurrentCombo) {

			System.Data.SqlTypes.SqlInt32 NewPrimaryKey;

			OlymarsDemo.Windows.Forms.WinForm_tblCustomer MyForm = new OlymarsDemo.Windows.Forms.WinForm_tblCustomer();
			MyForm.AddNewRecord(this, "Add a new tblCustomer", ConnectionString);

			if (MyForm.DialogResult == DialogResult.OK) {

				if (!MyForm.ErrorHasOccured) {

					NewPrimaryKey = ((OlymarsDemo.DataClasses.Parameters.spI_tblCustomer)MyForm.Parameter).Param_Cus_LngID;
					Refresh_Ord_LngCustomerID();
					CurrentCombo.SelectedValue = NewPrimaryKey.Value;
				}

				MyForm.Dispose();
				CurrentCombo.Select();
			}
		}

	}
}
