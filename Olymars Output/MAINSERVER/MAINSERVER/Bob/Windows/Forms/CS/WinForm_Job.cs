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
	/// designed for the 'Job' table.
	/// </summary>
	public class WinForm_Job : System.Windows.Forms.Form {

		private bool mustBeHidden_Description = false;
		private bool mustBeDisabled_Description = false;
		private bool hasBeenPopulated_Description = false;
		private System.Data.SqlTypes.SqlString internal_Description = System.Data.SqlTypes.SqlString.Null;
		private System.Windows.Forms.Label labDescription;
		private WinForm_DatabaseTextBox Control_Description;
		private bool mustBeHidden_Price = false;
		private bool mustBeDisabled_Price = false;
		private bool hasBeenPopulated_Price = false;
		private System.Data.SqlTypes.SqlMoney internal_Price = System.Data.SqlTypes.SqlMoney.Null;
		private System.Windows.Forms.Label labPrice;
		private WinForm_DatabaseTextBox Control_Price;
		private bool mustBeHidden_StartDate = false;
		private bool mustBeDisabled_StartDate = false;
		private bool hasBeenPopulated_StartDate = false;
		private System.Data.SqlTypes.SqlDateTime internal_StartDate = System.Data.SqlTypes.SqlDateTime.Null;
		private System.Windows.Forms.Label labStartDate;
		private WinForm_DatabaseTextBox Control_StartDate;
		private bool mustBeHidden_EndDate = false;
		private bool mustBeDisabled_EndDate = false;
		private bool hasBeenPopulated_EndDate = false;
		private System.Data.SqlTypes.SqlDateTime internal_EndDate = System.Data.SqlTypes.SqlDateTime.Null;
		private System.Windows.Forms.Label labEndDate;
		private WinForm_DatabaseTextBox Control_EndDate;
		private bool mustBeHidden_CustomerId = false;
		private bool mustBeDisabled_CustomerId = false;
		private bool hasBeenPopulated_CustomerId = false;
		private System.Data.SqlTypes.SqlInt32 internal_CustomerId = System.Data.SqlTypes.SqlInt32.Null;
		private System.Windows.Forms.Label labCustomerId;
		private Bob.Windows.ComboBoxes.WinComboBox_Customers Control_CustomerId;
		private System.Windows.Forms.Button Button_CustomerId;
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
		/// Gets or sets the fact that the controls responsible of the 'Price' column must be hidden.
		/// </summary>
		public bool MustBeHidden_Price {

			get {

				return(this.mustBeHidden_Price);
			}

			set {

				this.mustBeHidden_Price = value;
			}
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'Price' column must be disabled.
		/// </summary>
		public bool MustBeDisabled_Price {

			get {

				return(this.mustBeDisabled_Price);
			}

			set {

				this.mustBeDisabled_Price = value;
			}
		}

		/// <summary>
		/// Allows you to pre-select a value for the 'Price' column.
		/// </summary>
		public void SetValue_Price(System.Data.SqlTypes.SqlMoney value) {

			this.hasBeenPopulated_Price = true;
			this.internal_Price = value;
		}

		/// <summary>
		/// Allows you to reset the pre-selected value for the 'Price' column.
		/// </summary>
		public void ResetValue_Price() {

			this.hasBeenPopulated_Price = false;
			this.internal_Price = System.Data.SqlTypes.SqlMoney.Null;
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'StartDate' column must be hidden.
		/// </summary>
		public bool MustBeHidden_StartDate {

			get {

				return(this.mustBeHidden_StartDate);
			}

			set {

				this.mustBeHidden_StartDate = value;
			}
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'StartDate' column must be disabled.
		/// </summary>
		public bool MustBeDisabled_StartDate {

			get {

				return(this.mustBeDisabled_StartDate);
			}

			set {

				this.mustBeDisabled_StartDate = value;
			}
		}

		/// <summary>
		/// Allows you to pre-select a value for the 'StartDate' column.
		/// </summary>
		public void SetValue_StartDate(System.Data.SqlTypes.SqlDateTime value) {

			this.hasBeenPopulated_StartDate = true;
			this.internal_StartDate = value;
		}

		/// <summary>
		/// Allows you to reset the pre-selected value for the 'StartDate' column.
		/// </summary>
		public void ResetValue_StartDate() {

			this.hasBeenPopulated_StartDate = false;
			this.internal_StartDate = System.Data.SqlTypes.SqlDateTime.Null;
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'EndDate' column must be hidden.
		/// </summary>
		public bool MustBeHidden_EndDate {

			get {

				return(this.mustBeHidden_EndDate);
			}

			set {

				this.mustBeHidden_EndDate = value;
			}
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'EndDate' column must be disabled.
		/// </summary>
		public bool MustBeDisabled_EndDate {

			get {

				return(this.mustBeDisabled_EndDate);
			}

			set {

				this.mustBeDisabled_EndDate = value;
			}
		}

		/// <summary>
		/// Allows you to pre-select a value for the 'EndDate' column.
		/// </summary>
		public void SetValue_EndDate(System.Data.SqlTypes.SqlDateTime value) {

			this.hasBeenPopulated_EndDate = true;
			this.internal_EndDate = value;
		}

		/// <summary>
		/// Allows you to reset the pre-selected value for the 'EndDate' column.
		/// </summary>
		public void ResetValue_EndDate() {

			this.hasBeenPopulated_EndDate = false;
			this.internal_EndDate = System.Data.SqlTypes.SqlDateTime.Null;
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'CustomerId' column must be hidden.
		/// </summary>
		public bool MustBeHidden_CustomerId {

			get {

				return(this.mustBeHidden_CustomerId);
			}

			set {

				this.mustBeHidden_CustomerId = value;
			}
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'CustomerId' column must be disabled.
		/// </summary>
		public bool MustBeDisabled_CustomerId {

			get {

				return(this.mustBeDisabled_CustomerId);
			}

			set {

				this.mustBeDisabled_CustomerId = value;
			}
		}

		/// <summary>
		/// Allows you to pre-select a value for the 'CustomerId' column.
		/// </summary>
		public void SetValue_CustomerId(System.Data.SqlTypes.SqlInt32 value) {

			this.hasBeenPopulated_CustomerId = true;
			this.internal_CustomerId = value;
		}

		/// <summary>
		/// Allows you to reset the pre-selected value for the 'CustomerId' column.
		/// </summary>
		public void ResetValue_CustomerId() {

			this.hasBeenPopulated_CustomerId = false;
			this.internal_CustomerId = System.Data.SqlTypes.SqlInt32.Null;
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

			// Price column
			this.Control_Price.Enabled = !this.mustBeDisabled_Price;
			this.Control_Price.Enabled = !this.mustBeHidden_Price;

			if (inAddingProcess) {

				if (this.hasBeenPopulated_Price) {

					if (this.internal_Price.IsNull) {

						this.Control_Price.Text = String.Empty;
					}
					else {

						this.Control_Price.Text = this.internal_Price.Value.ToString();
					}
				}
			}

			// StartDate column
			this.Control_StartDate.Enabled = !this.mustBeDisabled_StartDate;
			this.Control_StartDate.Enabled = !this.mustBeHidden_StartDate;

			if (inAddingProcess) {

				if (this.hasBeenPopulated_StartDate) {

					if (this.internal_StartDate.IsNull) {

						this.Control_StartDate.Text = String.Empty;
					}
					else {

						this.Control_StartDate.Text = this.internal_StartDate.Value.ToString();
					}
				}
			}

			// EndDate column
			this.Control_EndDate.Enabled = !this.mustBeDisabled_EndDate;
			this.Control_EndDate.Enabled = !this.mustBeHidden_EndDate;

			if (inAddingProcess) {

				if (this.hasBeenPopulated_EndDate) {

					if (this.internal_EndDate.IsNull) {

						this.Control_EndDate.Text = String.Empty;
					}
					else {

						this.Control_EndDate.Text = this.internal_EndDate.Value.ToString();
					}
				}
			}

			// CustomerId column
			if (this.mustBeDisabled_CustomerId) {

				this.Control_CustomerId.Enabled = false;
				this.Button_CustomerId.Enabled = false;
			}

			if (this.mustBeHidden_CustomerId) {

				this.Control_CustomerId.Visible = false;
				this.Button_CustomerId.Visible = false;
			}

			if (inAddingProcess) {

				if (this.hasBeenPopulated_CustomerId) {

					if (this.internal_CustomerId.IsNull) {

						this.Control_CustomerId.SelectedIndex = -1;
					}
					else {

						this.Control_CustomerId.SelectedValue = this.internal_CustomerId.Value;
					}
				}
			}
		}

		/// <summary>
		/// Create a new instance of the Bob.Windows.Forms.WinForm_Job class.
		/// </summary>
		public WinForm_Job() {

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
			this.labPrice = new System.Windows.Forms.Label();
			this.Control_Price = new WinForm_DatabaseTextBox();
			this.labStartDate = new System.Windows.Forms.Label();
			this.Control_StartDate = new WinForm_DatabaseTextBox();
			this.labEndDate = new System.Windows.Forms.Label();
			this.Control_EndDate = new WinForm_DatabaseTextBox();
			this.labCustomerId = new System.Windows.Forms.Label();
			this.Control_CustomerId = new Bob.Windows.ComboBoxes.WinComboBox_Customers();
			this.Button_CustomerId = new System.Windows.Forms.Button();
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
			// labPrice
			// 
			this.labPrice.AutoSize = true;
			this.labPrice.Location = new System.Drawing.Point(24, 80);
			this.labPrice.Name = "labPrice";
			this.labPrice.Size = new System.Drawing.Size(110, 13);
			this.labPrice.TabIndex = 2;
			this.labPrice.Text = "Price (update this label in the \"Olymars/ColumnLabel\" extended property of the \"Price\" column):";
			// 
			// Control_Price
			// 
			this.Control_Price.Location = new System.Drawing.Point(24, 96);
			this.Control_Price.Name = "Control_Price";
			this.Control_Price.NullIsAllow = true;
			this.Control_Price.InvalidBackColor = System.Drawing.Color.Red;
			this.Control_Price.Size = new System.Drawing.Size(440, 20);
			this.Control_Price.TabIndex = 3;
			this.Control_Price.DataSize = 21;
			this.Control_Price.TextChanged += new System.EventHandler(this.Control_Price_TextChanged);
			this.Control_Price.DBType = SupportedDatabaseTypes.DBType_money;
			// 
			// labStartDate
			// 
			this.labStartDate.AutoSize = true;
			this.labStartDate.Location = new System.Drawing.Point(24, 136);
			this.labStartDate.Name = "labStartDate";
			this.labStartDate.Size = new System.Drawing.Size(110, 13);
			this.labStartDate.TabIndex = 4;
			this.labStartDate.Text = "StartDate (update this label in the \"Olymars/ColumnLabel\" extended property of the \"StartDate\" column):";
			// 
			// Control_StartDate
			// 
			this.Control_StartDate.Location = new System.Drawing.Point(24, 152);
			this.Control_StartDate.Name = "Control_StartDate";
			this.Control_StartDate.NullIsAllow = true;
			this.Control_StartDate.InvalidBackColor = System.Drawing.Color.Red;
			this.Control_StartDate.Size = new System.Drawing.Size(440, 20);
			this.Control_StartDate.TabIndex = 5;
			this.Control_StartDate.DataSize = 16;
			this.Control_StartDate.TextChanged += new System.EventHandler(this.Control_StartDate_TextChanged);
			this.Control_StartDate.DBType = SupportedDatabaseTypes.DBType_datetime;
			// 
			// labEndDate
			// 
			this.labEndDate.AutoSize = true;
			this.labEndDate.Location = new System.Drawing.Point(24, 192);
			this.labEndDate.Name = "labEndDate";
			this.labEndDate.Size = new System.Drawing.Size(110, 13);
			this.labEndDate.TabIndex = 6;
			this.labEndDate.Text = "EndDate (update this label in the \"Olymars/ColumnLabel\" extended property of the \"EndDate\" column):";
			// 
			// Control_EndDate
			// 
			this.Control_EndDate.Location = new System.Drawing.Point(24, 208);
			this.Control_EndDate.Name = "Control_EndDate";
			this.Control_EndDate.NullIsAllow = true;
			this.Control_EndDate.InvalidBackColor = System.Drawing.Color.Red;
			this.Control_EndDate.Size = new System.Drawing.Size(440, 20);
			this.Control_EndDate.TabIndex = 7;
			this.Control_EndDate.DataSize = 16;
			this.Control_EndDate.TextChanged += new System.EventHandler(this.Control_EndDate_TextChanged);
			this.Control_EndDate.DBType = SupportedDatabaseTypes.DBType_datetime;
			// 
			// labCustomerId
			// 
			this.labCustomerId.AutoSize = true;
			this.labCustomerId.Location = new System.Drawing.Point(24, 248);
			this.labCustomerId.Name = "labCustomerId";
			this.labCustomerId.Size = new System.Drawing.Size(126, 13);
			this.labCustomerId.TabIndex = 8;
			this.labCustomerId.Text = "CustomerId (update this label in the \"Olymars/ColumnLabel\" extended property of the \"CustomerId\" column):";
			// 
			// Control_CustomerId
			// 
			this.Control_CustomerId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Control_CustomerId.DropDownWidth = 392;
			this.Control_CustomerId.Location = new System.Drawing.Point(24, 264);
			this.Control_CustomerId.Name = "Control_CustomerId";
			this.Control_CustomerId.Size = new System.Drawing.Size(352, 21);
			this.Control_CustomerId.TabIndex = 9;
			this.Control_CustomerId.SelectedIndexChanged += new System.EventHandler(this.Control_CustomerId_SelectedIndexChanged);
			// 
			// Button_CustomerId
			// 
			this.Button_CustomerId.Location = new System.Drawing.Point(392, 264);
			this.Button_CustomerId.Name = "Button_CustomerId";
			this.Button_CustomerId.TabIndex = 10;
			this.Button_CustomerId.Text = "Add";
			this.Button_CustomerId.Click += new System.EventHandler(this.Button_CustomerId_Click);
			// 
			// cmdOK
			// 
			this.cmdOK.Location = new System.Drawing.Point(304, 304);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.TabIndex = 11;
			this.cmdOK.Text = "OK";
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(392, 304);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.TabIndex = 11;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// WinForm_Job
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 13);
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(506, 344);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
						this.labDescription,
						this.Control_Description,
						this.labPrice,
						this.Control_Price,
						this.labStartDate,
						this.Control_StartDate,
						this.labEndDate,
						this.Control_EndDate,
						this.labCustomerId,
						this.Control_CustomerId,
						this.Button_CustomerId,
						this.cmdOK,
						this.cmdCancel
						});
			this.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "WinForm_Job";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Job management";
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

			Control_Price.Text = String.Empty;

			Control_StartDate.Text = String.Empty;

			Control_EndDate.Text = String.Empty;

			Control_CustomerId.Enabled = true;
			if (Control_CustomerId.Items.Count != 0) {

				Control_CustomerId.SelectedIndex = 0;
			}


			this.IgnoreChanges = false;
		}

		/// <summary>
		/// Allows you to add a new record in the Job table.
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
			this.Control_Price.HandleTextChanged(null, null);
			this.Control_StartDate.HandleTextChanged(null, null);
			this.Control_EndDate.HandleTextChanged(null, null);

			Check_cmdOK_Button(null);

			ProcessHiddenAndDisabledControlStatus(true);

			this.ShowDialog(caller);
		}

		/// <summary>
		/// Allows you to add a new record in the Job table.
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
			this.Control_Price.HandleTextChanged(null, null);
			this.Control_StartDate.HandleTextChanged(null, null);
			this.Control_EndDate.HandleTextChanged(null, null);

			Check_cmdOK_Button(null);

			ProcessHiddenAndDisabledControlStatus(true);

			this.ShowDialog(caller);
		}

		/// <summary>
		/// Allows you to update an existing record from the Job table.
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
		/// Allows you to update an existing record from the Job table.
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
				Status = (Status && Control_Price.IsValid);
				Status = (Status && Control_StartDate.IsValid);
				Status = (Status && Control_EndDate.IsValid);
				Status = (Status && Control_CustomerId.SelectedIndex != -1);
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

			Refresh_CustomerId();
		}

		private void Control_CustomerId_SelectedIndexChanged(object sender, System.EventArgs e) {

			if (!this.IgnoreChanges) Check_cmdOK_Button(Control_CustomerId);
		}


		private void Refresh_CustomerId() {

			this.IgnoreChanges = true;

			switch (this.lastKnownConnectionType) {
				case Bob.DataClasses.ConnectionType.ConnectionString:
					Control_CustomerId.Initialize(this.connectionString, System.Data.SqlTypes.SqlInt32.Null);
					break;

				case Bob.DataClasses.ConnectionType.SqlConnection:
					Control_CustomerId.Initialize(this.sqlConnection, System.Data.SqlTypes.SqlInt32.Null);
					break;
			}

			try {

				Control_CustomerId.RefreshData(System.Data.SqlTypes.SqlInt32.Null);
			}
			catch (Bob.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinComboBox_Customers' class. Exception message is: {0}", customException.Parameter.SqlException.Message), customException);
				}
				else if (customException.Parameter.OtherException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinComboBox_Customers' class. Exception message is: {0}", customException.Parameter.OtherException.Message), customException);
				}
				else {

					throw;
				}
			}

			if (Control_CustomerId.Items.Count > 0) {

				Control_CustomerId.SelectedIndex = 0;
			}

			this.IgnoreChanges = false;
		}

		private bool RefreshCurrentRecord() {

			EmptyControls();

			Abstracts.Abstract_Job oAbstract_Job = null;
			switch (this.lastKnownConnectionType) {
				case Bob.DataClasses.ConnectionType.ConnectionString:
					oAbstract_Job = new Abstracts.Abstract_Job(this.connectionString);
					break;

				case Bob.DataClasses.ConnectionType.SqlConnection:
					oAbstract_Job = new Abstracts.Abstract_Job(this.sqlConnection);
					break;
			}

			if (oAbstract_Job.Refresh(this.currentID)) {

				this.IgnoreChanges = true;

				if (!oAbstract_Job.Col_Description.IsNull) {

					Control_Description.Text = oAbstract_Job.Col_Description.Value.ToString();
				}


				if (!oAbstract_Job.Col_Price.IsNull) {

					Control_Price.Text = oAbstract_Job.Col_Price.Value.ToString();
				}


				if (!oAbstract_Job.Col_StartDate.IsNull) {

					Control_StartDate.Text = oAbstract_Job.Col_StartDate.Value.ToString();
				}


				if (!oAbstract_Job.Col_EndDate.IsNull) {

					Control_EndDate.Text = oAbstract_Job.Col_EndDate.Value.ToString();
				}


				if (!oAbstract_Job.Col_CustomerId.IsNull) {

					Control_CustomerId.SelectedValue = oAbstract_Job.Col_CustomerId.Value;
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

		private void Control_Price_TextChanged(object sender, System.EventArgs e) {

			if (!this.IgnoreChanges) {

				Check_cmdOK_Button(Control_Price);
			}
		}

		private void Control_StartDate_TextChanged(object sender, System.EventArgs e) {

			if (!this.IgnoreChanges) {

				Check_cmdOK_Button(Control_StartDate);
			}
		}

		private void Control_EndDate_TextChanged(object sender, System.EventArgs e) {

			if (!this.IgnoreChanges) {

				Check_cmdOK_Button(Control_EndDate);
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

			Params.spI_Job Param = new Params.spI_Job(false);

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

			if (Control_Price.Text.Trim() != String.Empty) {

				Param.Param_Price = (System.Data.SqlTypes.SqlMoney)Control_Price.GetSqlTypesValue;
			}

			if (Control_StartDate.Text.Trim() != String.Empty) {

				Param.Param_StartDate = (System.Data.SqlTypes.SqlDateTime)Control_StartDate.GetSqlTypesValue;
			}

			if (Control_EndDate.Text.Trim() != String.Empty) {

				Param.Param_EndDate = (System.Data.SqlTypes.SqlDateTime)Control_EndDate.GetSqlTypesValue;
			}

			if (Control_CustomerId.SelectedIndex != -1) {

				Param.Param_CustomerId = (System.Int32)Control_CustomerId.SelectedValue;
			}

			SPs.spI_Job SP = new SPs.spI_Job(false);

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

					throw new Bob.DataClasses.CustomException(Param, "Bob.Windows.Forms.WinForm_Job", "AddRecordInDatabase");
				}
			}
		}

		private void UpdateRecordInDatabase() {

			Params.spU_Job Param = new Params.spU_Job(false);
			
			switch (this.lastKnownConnectionType) {
				case Bob.DataClasses.ConnectionType.ConnectionString:
					Param.SetUpConnection(this.connectionString);
					break;

				case Bob.DataClasses.ConnectionType.SqlConnection:
					Param.SetUpConnection(this.sqlConnection);
					break;
			}

			Param.Param_JobId = this.currentID;

			if (Control_Description.Text.Trim() != String.Empty) {

				Param.Param_Description = (System.Data.SqlTypes.SqlString)Control_Description.GetSqlTypesValue;
			}

			if (Control_Price.Text.Trim() != String.Empty) {

				Param.Param_Price = (System.Data.SqlTypes.SqlMoney)Control_Price.GetSqlTypesValue;
			}

			if (Control_StartDate.Text.Trim() != String.Empty) {

				Param.Param_StartDate = (System.Data.SqlTypes.SqlDateTime)Control_StartDate.GetSqlTypesValue;
			}

			if (Control_EndDate.Text.Trim() != String.Empty) {

				Param.Param_EndDate = (System.Data.SqlTypes.SqlDateTime)Control_EndDate.GetSqlTypesValue;
			}

			if (Control_CustomerId.SelectedIndex != -1) {

				Param.Param_CustomerId = (System.Int32)Control_CustomerId.SelectedValue;
			}

			SPs.spU_Job SP = new SPs.spU_Job(false);

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

					throw new Bob.DataClasses.CustomException(Param, "Bob.Windows.Forms.WinForm_Job", "UpdateRecordInDatabase");
				}
			}
		}

		private void Button_CustomerId_Click(object sender, System.EventArgs e) {

			AddNew_Customers1(Control_CustomerId);
		}

		private void AddNew_Customers1(System.Windows.Forms.ComboBox CurrentCombo) {

			System.Data.SqlTypes.SqlInt32 NewPrimaryKey;

			Bob.Windows.Forms.WinForm_Customers MyForm = new Bob.Windows.Forms.WinForm_Customers();
			MyForm.AddNewRecord(this, "Add a new Customers", ConnectionString);

			if (MyForm.DialogResult == DialogResult.OK) {

				if (!MyForm.ErrorHasOccured) {

					NewPrimaryKey = ((Bob.DataClasses.Parameters.spI_Customers)MyForm.Parameter).Param_CustomerID;
					Refresh_CustomerId();
					CurrentCombo.SelectedValue = NewPrimaryKey.Value;
				}

				MyForm.Dispose();
				CurrentCombo.Select();
			}
		}

	}
}
