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
	/// designed for the 'tblProduct' table.
	/// </summary>
	public class WinForm_tblProduct : System.Windows.Forms.Form {

		private bool mustBeHidden_Pro_StrName = false;
		private bool mustBeDisabled_Pro_StrName = false;
		private bool hasBeenPopulated_Pro_StrName = false;
		private System.Data.SqlTypes.SqlString internal_Pro_StrName = System.Data.SqlTypes.SqlString.Null;
		private System.Windows.Forms.Label labPro_StrName;
		private WinForm_DatabaseTextBox Control_Pro_StrName;
		private bool mustBeHidden_Pro_CurPrice = false;
		private bool mustBeDisabled_Pro_CurPrice = false;
		private bool hasBeenPopulated_Pro_CurPrice = false;
		private System.Data.SqlTypes.SqlMoney internal_Pro_CurPrice = System.Data.SqlTypes.SqlMoney.Null;
		private System.Windows.Forms.Label labPro_CurPrice;
		private WinForm_DatabaseTextBox Control_Pro_CurPrice;
		private bool mustBeHidden_Pro_LngCategoryID = false;
		private bool mustBeDisabled_Pro_LngCategoryID = false;
		private bool hasBeenPopulated_Pro_LngCategoryID = false;
		private System.Data.SqlTypes.SqlInt32 internal_Pro_LngCategoryID = System.Data.SqlTypes.SqlInt32.Null;
		private System.Windows.Forms.Label labPro_LngCategoryID;
		private OlymarsDemo.Windows.ComboBoxes.WinComboBox_tblCategory Control_Pro_LngCategoryID;
		private System.Windows.Forms.Button Button_Pro_LngCategoryID;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Button cmdCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'Pro_StrName' column must be hidden.
		/// </summary>
		public bool MustBeHidden_Pro_StrName {

			get {

				return(this.mustBeHidden_Pro_StrName);
			}

			set {

				this.mustBeHidden_Pro_StrName = value;
			}
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'Pro_StrName' column must be disabled.
		/// </summary>
		public bool MustBeDisabled_Pro_StrName {

			get {

				return(this.mustBeDisabled_Pro_StrName);
			}

			set {

				this.mustBeDisabled_Pro_StrName = value;
			}
		}

		/// <summary>
		/// Allows you to pre-select a value for the 'Pro_StrName' column.
		/// </summary>
		public void SetValue_Pro_StrName(System.Data.SqlTypes.SqlString value) {

			this.hasBeenPopulated_Pro_StrName = true;
			this.internal_Pro_StrName = value;
		}

		/// <summary>
		/// Allows you to reset the pre-selected value for the 'Pro_StrName' column.
		/// </summary>
		public void ResetValue_Pro_StrName() {

			this.hasBeenPopulated_Pro_StrName = false;
			this.internal_Pro_StrName = System.Data.SqlTypes.SqlString.Null;
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'Pro_CurPrice' column must be hidden.
		/// </summary>
		public bool MustBeHidden_Pro_CurPrice {

			get {

				return(this.mustBeHidden_Pro_CurPrice);
			}

			set {

				this.mustBeHidden_Pro_CurPrice = value;
			}
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'Pro_CurPrice' column must be disabled.
		/// </summary>
		public bool MustBeDisabled_Pro_CurPrice {

			get {

				return(this.mustBeDisabled_Pro_CurPrice);
			}

			set {

				this.mustBeDisabled_Pro_CurPrice = value;
			}
		}

		/// <summary>
		/// Allows you to pre-select a value for the 'Pro_CurPrice' column.
		/// </summary>
		public void SetValue_Pro_CurPrice(System.Data.SqlTypes.SqlMoney value) {

			this.hasBeenPopulated_Pro_CurPrice = true;
			this.internal_Pro_CurPrice = value;
		}

		/// <summary>
		/// Allows you to reset the pre-selected value for the 'Pro_CurPrice' column.
		/// </summary>
		public void ResetValue_Pro_CurPrice() {

			this.hasBeenPopulated_Pro_CurPrice = false;
			this.internal_Pro_CurPrice = System.Data.SqlTypes.SqlMoney.Null;
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'Pro_LngCategoryID' column must be hidden.
		/// </summary>
		public bool MustBeHidden_Pro_LngCategoryID {

			get {

				return(this.mustBeHidden_Pro_LngCategoryID);
			}

			set {

				this.mustBeHidden_Pro_LngCategoryID = value;
			}
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'Pro_LngCategoryID' column must be disabled.
		/// </summary>
		public bool MustBeDisabled_Pro_LngCategoryID {

			get {

				return(this.mustBeDisabled_Pro_LngCategoryID);
			}

			set {

				this.mustBeDisabled_Pro_LngCategoryID = value;
			}
		}

		/// <summary>
		/// Allows you to pre-select a value for the 'Pro_LngCategoryID' column.
		/// </summary>
		public void SetValue_Pro_LngCategoryID(System.Data.SqlTypes.SqlInt32 value) {

			this.hasBeenPopulated_Pro_LngCategoryID = true;
			this.internal_Pro_LngCategoryID = value;
		}

		/// <summary>
		/// Allows you to reset the pre-selected value for the 'Pro_LngCategoryID' column.
		/// </summary>
		public void ResetValue_Pro_LngCategoryID() {

			this.hasBeenPopulated_Pro_LngCategoryID = false;
			this.internal_Pro_LngCategoryID = System.Data.SqlTypes.SqlInt32.Null;
		}


		private void ProcessHiddenAndDisabledControlStatus(bool inAddingProcess) {

			// Pro_StrName column
			this.Control_Pro_StrName.Enabled = !this.mustBeDisabled_Pro_StrName;
			this.Control_Pro_StrName.Enabled = !this.mustBeHidden_Pro_StrName;

			if (inAddingProcess) {

				if (this.hasBeenPopulated_Pro_StrName) {

					if (this.internal_Pro_StrName.IsNull) {

						this.Control_Pro_StrName.Text = String.Empty;
					}
					else {

						this.Control_Pro_StrName.Text = this.internal_Pro_StrName.Value.ToString();
					}
				}
			}

			// Pro_CurPrice column
			this.Control_Pro_CurPrice.Enabled = !this.mustBeDisabled_Pro_CurPrice;
			this.Control_Pro_CurPrice.Enabled = !this.mustBeHidden_Pro_CurPrice;

			if (inAddingProcess) {

				if (this.hasBeenPopulated_Pro_CurPrice) {

					if (this.internal_Pro_CurPrice.IsNull) {

						this.Control_Pro_CurPrice.Text = String.Empty;
					}
					else {

						this.Control_Pro_CurPrice.Text = this.internal_Pro_CurPrice.Value.ToString();
					}
				}
			}

			// Pro_LngCategoryID column
			if (this.mustBeDisabled_Pro_LngCategoryID) {

				this.Control_Pro_LngCategoryID.Enabled = false;
				this.Button_Pro_LngCategoryID.Enabled = false;
			}

			if (this.mustBeHidden_Pro_LngCategoryID) {

				this.Control_Pro_LngCategoryID.Visible = false;
				this.Button_Pro_LngCategoryID.Visible = false;
			}

			if (inAddingProcess) {

				if (this.hasBeenPopulated_Pro_LngCategoryID) {

					if (this.internal_Pro_LngCategoryID.IsNull) {

						this.Control_Pro_LngCategoryID.SelectedIndex = -1;
					}
					else {

						this.Control_Pro_LngCategoryID.SelectedValue = this.internal_Pro_LngCategoryID.Value;
					}
				}
			}
		}

		/// <summary>
		/// Create a new instance of the OlymarsDemo.Windows.Forms.WinForm_tblProduct class.
		/// </summary>
		public WinForm_tblProduct() {

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

			this.labPro_StrName = new System.Windows.Forms.Label();
			this.Control_Pro_StrName = new WinForm_DatabaseTextBox();
			this.labPro_CurPrice = new System.Windows.Forms.Label();
			this.Control_Pro_CurPrice = new WinForm_DatabaseTextBox();
			this.labPro_LngCategoryID = new System.Windows.Forms.Label();
			this.Control_Pro_LngCategoryID = new OlymarsDemo.Windows.ComboBoxes.WinComboBox_tblCategory();
			this.Button_Pro_LngCategoryID = new System.Windows.Forms.Button();
			this.cmdOK = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// labPro_StrName
			// 
			this.labPro_StrName.AutoSize = true;
			this.labPro_StrName.Location = new System.Drawing.Point(24, 24);
			this.labPro_StrName.Name = "labPro_StrName";
			this.labPro_StrName.Size = new System.Drawing.Size(110, 13);
			this.labPro_StrName.TabIndex = 0;
			this.labPro_StrName.Text = "Pro_StrName (update this label in the \"Olymars/ColumnLabel\" extended property of the \"Pro_StrName\" column):";
			// 
			// Control_Pro_StrName
			// 
			this.Control_Pro_StrName.Location = new System.Drawing.Point(24, 40);
			this.Control_Pro_StrName.Name = "Control_Pro_StrName";
			this.Control_Pro_StrName.NullIsAllow = false;
			this.Control_Pro_StrName.InvalidBackColor = System.Drawing.Color.Red;
			this.Control_Pro_StrName.Size = new System.Drawing.Size(440, 20);
			this.Control_Pro_StrName.TabIndex = 1;
			this.Control_Pro_StrName.DataSize = 255;
			this.Control_Pro_StrName.MaxLength = 255;
			this.Control_Pro_StrName.TextChanged += new System.EventHandler(this.Control_Pro_StrName_TextChanged);
			this.Control_Pro_StrName.DBType = SupportedDatabaseTypes.DBType_varchar;
			// 
			// labPro_CurPrice
			// 
			this.labPro_CurPrice.AutoSize = true;
			this.labPro_CurPrice.Location = new System.Drawing.Point(24, 80);
			this.labPro_CurPrice.Name = "labPro_CurPrice";
			this.labPro_CurPrice.Size = new System.Drawing.Size(110, 13);
			this.labPro_CurPrice.TabIndex = 2;
			this.labPro_CurPrice.Text = "Pro_CurPrice (update this label in the \"Olymars/ColumnLabel\" extended property of the \"Pro_CurPrice\" column):";
			// 
			// Control_Pro_CurPrice
			// 
			this.Control_Pro_CurPrice.Location = new System.Drawing.Point(24, 96);
			this.Control_Pro_CurPrice.Name = "Control_Pro_CurPrice";
			this.Control_Pro_CurPrice.NullIsAllow = false;
			this.Control_Pro_CurPrice.InvalidBackColor = System.Drawing.Color.Red;
			this.Control_Pro_CurPrice.Size = new System.Drawing.Size(440, 20);
			this.Control_Pro_CurPrice.TabIndex = 3;
			this.Control_Pro_CurPrice.DataSize = 21;
			this.Control_Pro_CurPrice.TextChanged += new System.EventHandler(this.Control_Pro_CurPrice_TextChanged);
			this.Control_Pro_CurPrice.DBType = SupportedDatabaseTypes.DBType_money;
			// 
			// labPro_LngCategoryID
			// 
			this.labPro_LngCategoryID.AutoSize = true;
			this.labPro_LngCategoryID.Location = new System.Drawing.Point(24, 136);
			this.labPro_LngCategoryID.Name = "labPro_LngCategoryID";
			this.labPro_LngCategoryID.Size = new System.Drawing.Size(126, 13);
			this.labPro_LngCategoryID.TabIndex = 4;
			this.labPro_LngCategoryID.Text = "Pro_LngCategoryID (update this label in the \"Olymars/ColumnLabel\" extended property of the \"Pro_LngCategoryID\" column):";
			// 
			// Control_Pro_LngCategoryID
			// 
			this.Control_Pro_LngCategoryID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Control_Pro_LngCategoryID.DropDownWidth = 392;
			this.Control_Pro_LngCategoryID.Location = new System.Drawing.Point(24, 152);
			this.Control_Pro_LngCategoryID.Name = "Control_Pro_LngCategoryID";
			this.Control_Pro_LngCategoryID.Size = new System.Drawing.Size(352, 21);
			this.Control_Pro_LngCategoryID.TabIndex = 5;
			this.Control_Pro_LngCategoryID.SelectedIndexChanged += new System.EventHandler(this.Control_Pro_LngCategoryID_SelectedIndexChanged);
			// 
			// Button_Pro_LngCategoryID
			// 
			this.Button_Pro_LngCategoryID.Location = new System.Drawing.Point(392, 152);
			this.Button_Pro_LngCategoryID.Name = "Button_Pro_LngCategoryID";
			this.Button_Pro_LngCategoryID.TabIndex = 6;
			this.Button_Pro_LngCategoryID.Text = "Add";
			this.Button_Pro_LngCategoryID.Click += new System.EventHandler(this.Button_Pro_LngCategoryID_Click);
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
			// WinForm_tblProduct
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 13);
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(506, 232);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
						this.labPro_StrName,
						this.Control_Pro_StrName,
						this.labPro_CurPrice,
						this.Control_Pro_CurPrice,
						this.labPro_LngCategoryID,
						this.Control_Pro_LngCategoryID,
						this.Button_Pro_LngCategoryID,
						this.cmdOK,
						this.cmdCancel
						});
			this.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "WinForm_tblProduct";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "tblProduct management";
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

			Control_Pro_StrName.Text = String.Empty;

			Control_Pro_CurPrice.Text = String.Empty;

			Control_Pro_LngCategoryID.Enabled = true;
			if (Control_Pro_LngCategoryID.Items.Count != 0) {

				Control_Pro_LngCategoryID.SelectedIndex = 0;
			}


			this.IgnoreChanges = false;
		}

		/// <summary>
		/// Allows you to add a new record in the tblProduct table.
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

			this.Control_Pro_StrName.HandleTextChanged(null, null);
			this.Control_Pro_CurPrice.HandleTextChanged(null, null);

			Check_cmdOK_Button(null);

			ProcessHiddenAndDisabledControlStatus(true);

			this.ShowDialog(caller);
		}

		/// <summary>
		/// Allows you to add a new record in the tblProduct table.
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

			this.Control_Pro_StrName.HandleTextChanged(null, null);
			this.Control_Pro_CurPrice.HandleTextChanged(null, null);

			Check_cmdOK_Button(null);

			ProcessHiddenAndDisabledControlStatus(true);

			this.ShowDialog(caller);
		}

		/// <summary>
		/// Allows you to update an existing record from the tblProduct table.
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
		/// Allows you to update an existing record from the tblProduct table.
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

				Status = (Status && Control_Pro_StrName.IsValid);
				Status = (Status && Control_Pro_CurPrice.IsValid);
				Status = (Status && Control_Pro_LngCategoryID.SelectedIndex != -1);
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

			Refresh_Pro_LngCategoryID();
		}

		private void Control_Pro_LngCategoryID_SelectedIndexChanged(object sender, System.EventArgs e) {

			if (!this.IgnoreChanges) Check_cmdOK_Button(Control_Pro_LngCategoryID);
		}


		private void Refresh_Pro_LngCategoryID() {

			this.IgnoreChanges = true;

			switch (this.lastKnownConnectionType) {
				case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
					Control_Pro_LngCategoryID.Initialize(this.connectionString);
					break;

				case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
					Control_Pro_LngCategoryID.Initialize(this.sqlConnection);
					break;
			}

			try {

				Control_Pro_LngCategoryID.RefreshData(System.Data.SqlTypes.SqlInt32.Null);
			}
			catch (OlymarsDemo.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinComboBox_tblCategory' class. Exception message is: {0}", customException.Parameter.SqlException.Message), customException);
				}
				else if (customException.Parameter.OtherException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinComboBox_tblCategory' class. Exception message is: {0}", customException.Parameter.OtherException.Message), customException);
				}
				else {

					throw;
				}
			}

			if (Control_Pro_LngCategoryID.Items.Count > 0) {

				Control_Pro_LngCategoryID.SelectedIndex = 0;
			}

			this.IgnoreChanges = false;
		}

		private bool RefreshCurrentRecord() {

			EmptyControls();

			Abstracts.Abstract_tblProduct oAbstract_tblProduct = null;
			switch (this.lastKnownConnectionType) {
				case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
					oAbstract_tblProduct = new Abstracts.Abstract_tblProduct(this.connectionString);
					break;

				case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
					oAbstract_tblProduct = new Abstracts.Abstract_tblProduct(this.sqlConnection);
					break;
			}

			if (oAbstract_tblProduct.Refresh(this.currentID)) {

				this.IgnoreChanges = true;

				if (!oAbstract_tblProduct.Col_Pro_StrName.IsNull) {

					Control_Pro_StrName.Text = oAbstract_tblProduct.Col_Pro_StrName.Value.ToString();
				}


				if (!oAbstract_tblProduct.Col_Pro_CurPrice.IsNull) {

					Control_Pro_CurPrice.Text = oAbstract_tblProduct.Col_Pro_CurPrice.Value.ToString();
				}


				if (!oAbstract_tblProduct.Col_Pro_LngCategoryID.IsNull) {

					Control_Pro_LngCategoryID.SelectedValue = oAbstract_tblProduct.Col_Pro_LngCategoryID.Value;
				}

				this.IgnoreChanges = false;
				return(true);
			}
			
			else {

				MessageBox.Show("The record does not exist any more !", "Record not found", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return(false);
			}
		}

		private void Control_Pro_StrName_TextChanged(object sender, System.EventArgs e) {

			if (!this.IgnoreChanges) {

				Check_cmdOK_Button(Control_Pro_StrName);
			}
		}

		private void Control_Pro_CurPrice_TextChanged(object sender, System.EventArgs e) {

			if (!this.IgnoreChanges) {

				Check_cmdOK_Button(Control_Pro_CurPrice);
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

			Params.spI_tblProduct Param = new Params.spI_tblProduct(false);

			switch (this.lastKnownConnectionType) {
				case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
					Param.SetUpConnection(this.connectionString);
					break;

				case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
					Param.SetUpConnection(this.sqlConnection);
					break;
			}

			if (Control_Pro_StrName.Text.Trim() != String.Empty) {

				Param.Param_Pro_StrName = (System.Data.SqlTypes.SqlString)Control_Pro_StrName.GetSqlTypesValue;
			}

			if (Control_Pro_CurPrice.Text.Trim() != String.Empty) {

				Param.Param_Pro_CurPrice = (System.Data.SqlTypes.SqlMoney)Control_Pro_CurPrice.GetSqlTypesValue;
			}

			if (Control_Pro_LngCategoryID.SelectedIndex != -1) {

				Param.Param_Pro_LngCategoryID = (System.Int32)Control_Pro_LngCategoryID.SelectedValue;
			}

			SPs.spI_tblProduct SP = new SPs.spI_tblProduct(false);

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

					throw new OlymarsDemo.DataClasses.CustomException(Param, "OlymarsDemo.Windows.Forms.WinForm_tblProduct", "AddRecordInDatabase");
				}
			}
		}

		private void UpdateRecordInDatabase() {

			Params.spU_tblProduct Param = new Params.spU_tblProduct(false);
			
			switch (this.lastKnownConnectionType) {
				case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
					Param.SetUpConnection(this.connectionString);
					break;

				case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
					Param.SetUpConnection(this.sqlConnection);
					break;
			}

			Param.Param_Pro_GuidID = this.currentID;

			if (Control_Pro_StrName.Text.Trim() != String.Empty) {

				Param.Param_Pro_StrName = (System.Data.SqlTypes.SqlString)Control_Pro_StrName.GetSqlTypesValue;
			}

			if (Control_Pro_CurPrice.Text.Trim() != String.Empty) {

				Param.Param_Pro_CurPrice = (System.Data.SqlTypes.SqlMoney)Control_Pro_CurPrice.GetSqlTypesValue;
			}

			if (Control_Pro_LngCategoryID.SelectedIndex != -1) {

				Param.Param_Pro_LngCategoryID = (System.Int32)Control_Pro_LngCategoryID.SelectedValue;
			}

			SPs.spU_tblProduct SP = new SPs.spU_tblProduct(false);

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

					throw new OlymarsDemo.DataClasses.CustomException(Param, "OlymarsDemo.Windows.Forms.WinForm_tblProduct", "UpdateRecordInDatabase");
				}
			}
		}

		private void Button_Pro_LngCategoryID_Click(object sender, System.EventArgs e) {

			AddNew_tblCategory1(Control_Pro_LngCategoryID);
		}

		private void AddNew_tblCategory1(System.Windows.Forms.ComboBox CurrentCombo) {

			System.Data.SqlTypes.SqlInt32 NewPrimaryKey;

			OlymarsDemo.Windows.Forms.WinForm_tblCategory MyForm = new OlymarsDemo.Windows.Forms.WinForm_tblCategory();
			MyForm.AddNewRecord(this, "Add a new tblCategory", ConnectionString);

			if (MyForm.DialogResult == DialogResult.OK) {

				if (!MyForm.ErrorHasOccured) {

					NewPrimaryKey = ((OlymarsDemo.DataClasses.Parameters.spI_tblCategory)MyForm.Parameter).Param_Cat_LngID;
					Refresh_Pro_LngCategoryID();
					CurrentCombo.SelectedValue = NewPrimaryKey.Value;
				}

				MyForm.Dispose();
				CurrentCombo.Select();
			}
		}

	}
}
