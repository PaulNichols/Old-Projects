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
	/// designed for the 'tblOrderItem' table.
	/// </summary>
	public class WinForm_tblOrderItem : System.Windows.Forms.Form {

		private bool mustBeHidden_Oit_GuidOrderID = false;
		private bool mustBeDisabled_Oit_GuidOrderID = false;
		private bool hasBeenPopulated_Oit_GuidOrderID = false;
		private System.Data.SqlTypes.SqlGuid internal_Oit_GuidOrderID = System.Data.SqlTypes.SqlGuid.Null;
		private System.Windows.Forms.Label labOit_GuidOrderID;
		private OlymarsDemo.Windows.ComboBoxes.WinComboBox_tblOrder Control_Oit_GuidOrderID;
		private System.Windows.Forms.Button Button_Oit_GuidOrderID;
		private bool mustBeHidden_Oit_GuidProductID = false;
		private bool mustBeDisabled_Oit_GuidProductID = false;
		private bool hasBeenPopulated_Oit_GuidProductID = false;
		private System.Data.SqlTypes.SqlGuid internal_Oit_GuidProductID = System.Data.SqlTypes.SqlGuid.Null;
		private System.Windows.Forms.Label labOit_GuidProductID;
		private OlymarsDemo.Windows.ComboBoxes.WinComboBox_tblProduct Control_Oit_GuidProductID;
		private System.Windows.Forms.Button Button_Oit_GuidProductID;
		private bool mustBeHidden_Oit_LngAmount = false;
		private bool mustBeDisabled_Oit_LngAmount = false;
		private bool hasBeenPopulated_Oit_LngAmount = false;
		private System.Data.SqlTypes.SqlInt32 internal_Oit_LngAmount = System.Data.SqlTypes.SqlInt32.Null;
		private System.Windows.Forms.Label labOit_LngAmount;
		private WinForm_DatabaseTextBox Control_Oit_LngAmount;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Button cmdCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'Oit_GuidOrderID' column must be hidden.
		/// </summary>
		public bool MustBeHidden_Oit_GuidOrderID {

			get {

				return(this.mustBeHidden_Oit_GuidOrderID);
			}

			set {

				this.mustBeHidden_Oit_GuidOrderID = value;
			}
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'Oit_GuidOrderID' column must be disabled.
		/// </summary>
		public bool MustBeDisabled_Oit_GuidOrderID {

			get {

				return(this.mustBeDisabled_Oit_GuidOrderID);
			}

			set {

				this.mustBeDisabled_Oit_GuidOrderID = value;
			}
		}

		/// <summary>
		/// Allows you to pre-select a value for the 'Oit_GuidOrderID' column.
		/// </summary>
		public void SetValue_Oit_GuidOrderID(System.Data.SqlTypes.SqlGuid value) {

			this.hasBeenPopulated_Oit_GuidOrderID = true;
			this.internal_Oit_GuidOrderID = value;
		}

		/// <summary>
		/// Allows you to reset the pre-selected value for the 'Oit_GuidOrderID' column.
		/// </summary>
		public void ResetValue_Oit_GuidOrderID() {

			this.hasBeenPopulated_Oit_GuidOrderID = false;
			this.internal_Oit_GuidOrderID = System.Data.SqlTypes.SqlGuid.Null;
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'Oit_GuidProductID' column must be hidden.
		/// </summary>
		public bool MustBeHidden_Oit_GuidProductID {

			get {

				return(this.mustBeHidden_Oit_GuidProductID);
			}

			set {

				this.mustBeHidden_Oit_GuidProductID = value;
			}
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'Oit_GuidProductID' column must be disabled.
		/// </summary>
		public bool MustBeDisabled_Oit_GuidProductID {

			get {

				return(this.mustBeDisabled_Oit_GuidProductID);
			}

			set {

				this.mustBeDisabled_Oit_GuidProductID = value;
			}
		}

		/// <summary>
		/// Allows you to pre-select a value for the 'Oit_GuidProductID' column.
		/// </summary>
		public void SetValue_Oit_GuidProductID(System.Data.SqlTypes.SqlGuid value) {

			this.hasBeenPopulated_Oit_GuidProductID = true;
			this.internal_Oit_GuidProductID = value;
		}

		/// <summary>
		/// Allows you to reset the pre-selected value for the 'Oit_GuidProductID' column.
		/// </summary>
		public void ResetValue_Oit_GuidProductID() {

			this.hasBeenPopulated_Oit_GuidProductID = false;
			this.internal_Oit_GuidProductID = System.Data.SqlTypes.SqlGuid.Null;
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'Oit_LngAmount' column must be hidden.
		/// </summary>
		public bool MustBeHidden_Oit_LngAmount {

			get {

				return(this.mustBeHidden_Oit_LngAmount);
			}

			set {

				this.mustBeHidden_Oit_LngAmount = value;
			}
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'Oit_LngAmount' column must be disabled.
		/// </summary>
		public bool MustBeDisabled_Oit_LngAmount {

			get {

				return(this.mustBeDisabled_Oit_LngAmount);
			}

			set {

				this.mustBeDisabled_Oit_LngAmount = value;
			}
		}

		/// <summary>
		/// Allows you to pre-select a value for the 'Oit_LngAmount' column.
		/// </summary>
		public void SetValue_Oit_LngAmount(System.Data.SqlTypes.SqlInt32 value) {

			this.hasBeenPopulated_Oit_LngAmount = true;
			this.internal_Oit_LngAmount = value;
		}

		/// <summary>
		/// Allows you to reset the pre-selected value for the 'Oit_LngAmount' column.
		/// </summary>
		public void ResetValue_Oit_LngAmount() {

			this.hasBeenPopulated_Oit_LngAmount = false;
			this.internal_Oit_LngAmount = System.Data.SqlTypes.SqlInt32.Null;
		}


		private void ProcessHiddenAndDisabledControlStatus(bool inAddingProcess) {

			// Oit_GuidOrderID column
			if (this.mustBeDisabled_Oit_GuidOrderID) {

				this.Control_Oit_GuidOrderID.Enabled = false;
				this.Button_Oit_GuidOrderID.Enabled = false;
			}

			if (this.mustBeHidden_Oit_GuidOrderID) {

				this.Control_Oit_GuidOrderID.Visible = false;
				this.Button_Oit_GuidOrderID.Visible = false;
			}

			if (inAddingProcess) {

				if (this.hasBeenPopulated_Oit_GuidOrderID) {

					if (this.internal_Oit_GuidOrderID.IsNull) {

						this.Control_Oit_GuidOrderID.SelectedIndex = -1;
					}
					else {

						this.Control_Oit_GuidOrderID.SelectedValue = this.internal_Oit_GuidOrderID.Value;
					}
				}
			}

			// Oit_GuidProductID column
			if (this.mustBeDisabled_Oit_GuidProductID) {

				this.Control_Oit_GuidProductID.Enabled = false;
				this.Button_Oit_GuidProductID.Enabled = false;
			}

			if (this.mustBeHidden_Oit_GuidProductID) {

				this.Control_Oit_GuidProductID.Visible = false;
				this.Button_Oit_GuidProductID.Visible = false;
			}

			if (inAddingProcess) {

				if (this.hasBeenPopulated_Oit_GuidProductID) {

					if (this.internal_Oit_GuidProductID.IsNull) {

						this.Control_Oit_GuidProductID.SelectedIndex = -1;
					}
					else {

						this.Control_Oit_GuidProductID.SelectedValue = this.internal_Oit_GuidProductID.Value;
					}
				}
			}

			// Oit_LngAmount column
			this.Control_Oit_LngAmount.Enabled = !this.mustBeDisabled_Oit_LngAmount;
			this.Control_Oit_LngAmount.Enabled = !this.mustBeHidden_Oit_LngAmount;

			if (inAddingProcess) {

				if (this.hasBeenPopulated_Oit_LngAmount) {

					if (this.internal_Oit_LngAmount.IsNull) {

						this.Control_Oit_LngAmount.Text = String.Empty;
					}
					else {

						this.Control_Oit_LngAmount.Text = this.internal_Oit_LngAmount.Value.ToString();
					}
				}
			}
		}

		/// <summary>
		/// Create a new instance of the OlymarsDemo.Windows.Forms.WinForm_tblOrderItem class.
		/// </summary>
		public WinForm_tblOrderItem() {

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

			this.labOit_GuidOrderID = new System.Windows.Forms.Label();
			this.Control_Oit_GuidOrderID = new OlymarsDemo.Windows.ComboBoxes.WinComboBox_tblOrder();
			this.Button_Oit_GuidOrderID = new System.Windows.Forms.Button();
			this.labOit_GuidProductID = new System.Windows.Forms.Label();
			this.Control_Oit_GuidProductID = new OlymarsDemo.Windows.ComboBoxes.WinComboBox_tblProduct();
			this.Button_Oit_GuidProductID = new System.Windows.Forms.Button();
			this.labOit_LngAmount = new System.Windows.Forms.Label();
			this.Control_Oit_LngAmount = new WinForm_DatabaseTextBox();
			this.cmdOK = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// labOit_GuidOrderID
			// 
			this.labOit_GuidOrderID.AutoSize = true;
			this.labOit_GuidOrderID.Location = new System.Drawing.Point(24, 24);
			this.labOit_GuidOrderID.Name = "labOit_GuidOrderID";
			this.labOit_GuidOrderID.Size = new System.Drawing.Size(126, 13);
			this.labOit_GuidOrderID.TabIndex = 0;
			this.labOit_GuidOrderID.Text = "Oit_GuidOrderID (update this label in the \"Olymars/ColumnLabel\" extended property of the \"Oit_GuidOrderID\" column):";
			// 
			// Control_Oit_GuidOrderID
			// 
			this.Control_Oit_GuidOrderID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Control_Oit_GuidOrderID.DropDownWidth = 392;
			this.Control_Oit_GuidOrderID.Location = new System.Drawing.Point(24, 40);
			this.Control_Oit_GuidOrderID.Name = "Control_Oit_GuidOrderID";
			this.Control_Oit_GuidOrderID.Size = new System.Drawing.Size(352, 21);
			this.Control_Oit_GuidOrderID.TabIndex = 1;
			this.Control_Oit_GuidOrderID.SelectedIndexChanged += new System.EventHandler(this.Control_Oit_GuidOrderID_SelectedIndexChanged);
			// 
			// Button_Oit_GuidOrderID
			// 
			this.Button_Oit_GuidOrderID.Location = new System.Drawing.Point(392, 40);
			this.Button_Oit_GuidOrderID.Name = "Button_Oit_GuidOrderID";
			this.Button_Oit_GuidOrderID.TabIndex = 2;
			this.Button_Oit_GuidOrderID.Text = "Add";
			this.Button_Oit_GuidOrderID.Click += new System.EventHandler(this.Button_Oit_GuidOrderID_Click);
			// 
			// labOit_GuidProductID
			// 
			this.labOit_GuidProductID.AutoSize = true;
			this.labOit_GuidProductID.Location = new System.Drawing.Point(24, 80);
			this.labOit_GuidProductID.Name = "labOit_GuidProductID";
			this.labOit_GuidProductID.Size = new System.Drawing.Size(126, 13);
			this.labOit_GuidProductID.TabIndex = 3;
			this.labOit_GuidProductID.Text = "Oit_GuidProductID (update this label in the \"Olymars/ColumnLabel\" extended property of the \"Oit_GuidProductID\" column):";
			// 
			// Control_Oit_GuidProductID
			// 
			this.Control_Oit_GuidProductID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Control_Oit_GuidProductID.DropDownWidth = 392;
			this.Control_Oit_GuidProductID.Location = new System.Drawing.Point(24, 96);
			this.Control_Oit_GuidProductID.Name = "Control_Oit_GuidProductID";
			this.Control_Oit_GuidProductID.Size = new System.Drawing.Size(352, 21);
			this.Control_Oit_GuidProductID.TabIndex = 4;
			this.Control_Oit_GuidProductID.SelectedIndexChanged += new System.EventHandler(this.Control_Oit_GuidProductID_SelectedIndexChanged);
			// 
			// Button_Oit_GuidProductID
			// 
			this.Button_Oit_GuidProductID.Location = new System.Drawing.Point(392, 96);
			this.Button_Oit_GuidProductID.Name = "Button_Oit_GuidProductID";
			this.Button_Oit_GuidProductID.TabIndex = 5;
			this.Button_Oit_GuidProductID.Text = "Add";
			this.Button_Oit_GuidProductID.Click += new System.EventHandler(this.Button_Oit_GuidProductID_Click);
			// 
			// labOit_LngAmount
			// 
			this.labOit_LngAmount.AutoSize = true;
			this.labOit_LngAmount.Location = new System.Drawing.Point(24, 136);
			this.labOit_LngAmount.Name = "labOit_LngAmount";
			this.labOit_LngAmount.Size = new System.Drawing.Size(110, 13);
			this.labOit_LngAmount.TabIndex = 6;
			this.labOit_LngAmount.Text = "Oit_LngAmount (update this label in the \"Olymars/ColumnLabel\" extended property of the \"Oit_LngAmount\" column):";
			// 
			// Control_Oit_LngAmount
			// 
			this.Control_Oit_LngAmount.Location = new System.Drawing.Point(24, 152);
			this.Control_Oit_LngAmount.Name = "Control_Oit_LngAmount";
			this.Control_Oit_LngAmount.NullIsAllow = false;
			this.Control_Oit_LngAmount.InvalidBackColor = System.Drawing.Color.Red;
			this.Control_Oit_LngAmount.Size = new System.Drawing.Size(440, 20);
			this.Control_Oit_LngAmount.TabIndex = 7;
			this.Control_Oit_LngAmount.DataSize = 4;
			this.Control_Oit_LngAmount.TextChanged += new System.EventHandler(this.Control_Oit_LngAmount_TextChanged);
			this.Control_Oit_LngAmount.DBType = SupportedDatabaseTypes.DBType_int;
			// 
			// cmdOK
			// 
			this.cmdOK.Location = new System.Drawing.Point(304, 192);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.TabIndex = 8;
			this.cmdOK.Text = "OK";
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(392, 192);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.TabIndex = 8;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// WinForm_tblOrderItem
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 13);
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(506, 232);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
						this.labOit_GuidOrderID,
						this.Control_Oit_GuidOrderID,
						this.Button_Oit_GuidOrderID,
						this.labOit_GuidProductID,
						this.Control_Oit_GuidProductID,
						this.Button_Oit_GuidProductID,
						this.labOit_LngAmount,
						this.Control_Oit_LngAmount,
						this.cmdOK,
						this.cmdCancel
						});
			this.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "WinForm_tblOrderItem";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "tblOrderItem management";
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

			Control_Oit_GuidOrderID.Enabled = true;
			if (Control_Oit_GuidOrderID.Items.Count != 0) {

				Control_Oit_GuidOrderID.SelectedIndex = 0;
			}

			Control_Oit_GuidProductID.Enabled = true;
			if (Control_Oit_GuidProductID.Items.Count != 0) {

				Control_Oit_GuidProductID.SelectedIndex = 0;
			}

			Control_Oit_LngAmount.Text = String.Empty;


			this.IgnoreChanges = false;
		}

		/// <summary>
		/// Allows you to add a new record in the tblOrderItem table.
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

			this.Control_Oit_LngAmount.HandleTextChanged(null, null);

			Check_cmdOK_Button(null);

			ProcessHiddenAndDisabledControlStatus(true);

			this.ShowDialog(caller);
		}

		/// <summary>
		/// Allows you to add a new record in the tblOrderItem table.
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

			this.Control_Oit_LngAmount.HandleTextChanged(null, null);

			Check_cmdOK_Button(null);

			ProcessHiddenAndDisabledControlStatus(true);

			this.ShowDialog(caller);
		}

		/// <summary>
		/// Allows you to update an existing record from the tblOrderItem table.
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
		/// Allows you to update an existing record from the tblOrderItem table.
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

				Status = (Status && Control_Oit_GuidOrderID.SelectedIndex != -1);
				Status = (Status && Control_Oit_GuidProductID.SelectedIndex != -1);
				Status = (Status && Control_Oit_LngAmount.IsValid);
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

			Refresh_Oit_GuidOrderID();
			Refresh_Oit_GuidProductID();
		}

		private void Control_Oit_GuidOrderID_SelectedIndexChanged(object sender, System.EventArgs e) {

			if (!this.IgnoreChanges) Check_cmdOK_Button(Control_Oit_GuidOrderID);
		}


		private void Refresh_Oit_GuidOrderID() {

			this.IgnoreChanges = true;

			switch (this.lastKnownConnectionType) {
				case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
					Control_Oit_GuidOrderID.Initialize(this.connectionString, System.Data.SqlTypes.SqlInt32.Null);
					break;

				case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
					Control_Oit_GuidOrderID.Initialize(this.sqlConnection, System.Data.SqlTypes.SqlInt32.Null);
					break;
			}

			try {

				Control_Oit_GuidOrderID.RefreshData(System.Data.SqlTypes.SqlGuid.Null);
			}
			catch (OlymarsDemo.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinComboBox_tblOrder' class. Exception message is: {0}", customException.Parameter.SqlException.Message), customException);
				}
				else if (customException.Parameter.OtherException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinComboBox_tblOrder' class. Exception message is: {0}", customException.Parameter.OtherException.Message), customException);
				}
				else {

					throw;
				}
			}

			if (Control_Oit_GuidOrderID.Items.Count > 0) {

				Control_Oit_GuidOrderID.SelectedIndex = 0;
			}

			this.IgnoreChanges = false;
		}

		private void Control_Oit_GuidProductID_SelectedIndexChanged(object sender, System.EventArgs e) {

			if (!this.IgnoreChanges) Check_cmdOK_Button(Control_Oit_GuidProductID);
		}


		private void Refresh_Oit_GuidProductID() {

			this.IgnoreChanges = true;

			switch (this.lastKnownConnectionType) {
				case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
					Control_Oit_GuidProductID.Initialize(this.connectionString, System.Data.SqlTypes.SqlInt32.Null);
					break;

				case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
					Control_Oit_GuidProductID.Initialize(this.sqlConnection, System.Data.SqlTypes.SqlInt32.Null);
					break;
			}

			try {

				Control_Oit_GuidProductID.RefreshData(System.Data.SqlTypes.SqlGuid.Null);
			}
			catch (OlymarsDemo.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinComboBox_tblProduct' class. Exception message is: {0}", customException.Parameter.SqlException.Message), customException);
				}
				else if (customException.Parameter.OtherException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinComboBox_tblProduct' class. Exception message is: {0}", customException.Parameter.OtherException.Message), customException);
				}
				else {

					throw;
				}
			}

			if (Control_Oit_GuidProductID.Items.Count > 0) {

				Control_Oit_GuidProductID.SelectedIndex = 0;
			}

			this.IgnoreChanges = false;
		}

		private bool RefreshCurrentRecord() {

			EmptyControls();

			Abstracts.Abstract_tblOrderItem oAbstract_tblOrderItem = null;
			switch (this.lastKnownConnectionType) {
				case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
					oAbstract_tblOrderItem = new Abstracts.Abstract_tblOrderItem(this.connectionString);
					break;

				case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
					oAbstract_tblOrderItem = new Abstracts.Abstract_tblOrderItem(this.sqlConnection);
					break;
			}

			if (oAbstract_tblOrderItem.Refresh(this.currentID)) {

				this.IgnoreChanges = true;

				if (!oAbstract_tblOrderItem.Col_Oit_GuidOrderID.IsNull) {

					Control_Oit_GuidOrderID.SelectedValue = oAbstract_tblOrderItem.Col_Oit_GuidOrderID.Value;
				}

				if (!oAbstract_tblOrderItem.Col_Oit_GuidProductID.IsNull) {

					Control_Oit_GuidProductID.SelectedValue = oAbstract_tblOrderItem.Col_Oit_GuidProductID.Value;
				}

				if (!oAbstract_tblOrderItem.Col_Oit_LngAmount.IsNull) {

					Control_Oit_LngAmount.Text = oAbstract_tblOrderItem.Col_Oit_LngAmount.Value.ToString();
				}


				this.IgnoreChanges = false;
				return(true);
			}
			
			else {

				MessageBox.Show("The record does not exist any more !", "Record not found", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return(false);
			}
		}

		private void Control_Oit_LngAmount_TextChanged(object sender, System.EventArgs e) {

			if (!this.IgnoreChanges) {

				Check_cmdOK_Button(Control_Oit_LngAmount);
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

			Params.spI_tblOrderItem Param = new Params.spI_tblOrderItem(false);

			switch (this.lastKnownConnectionType) {
				case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
					Param.SetUpConnection(this.connectionString);
					break;

				case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
					Param.SetUpConnection(this.sqlConnection);
					break;
			}

			if (Control_Oit_GuidOrderID.SelectedIndex != -1) {

				Param.Param_Oit_GuidOrderID = (System.Guid)Control_Oit_GuidOrderID.SelectedValue;
			}

			if (Control_Oit_GuidProductID.SelectedIndex != -1) {

				Param.Param_Oit_GuidProductID = (System.Guid)Control_Oit_GuidProductID.SelectedValue;
			}

			if (Control_Oit_LngAmount.Text.Trim() != String.Empty) {

				Param.Param_Oit_LngAmount = (System.Data.SqlTypes.SqlInt32)Control_Oit_LngAmount.GetSqlTypesValue;
			}

			SPs.spI_tblOrderItem SP = new SPs.spI_tblOrderItem(false);

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

					throw new OlymarsDemo.DataClasses.CustomException(Param, "OlymarsDemo.Windows.Forms.WinForm_tblOrderItem", "AddRecordInDatabase");
				}
			}
		}

		private void UpdateRecordInDatabase() {

			Params.spU_tblOrderItem Param = new Params.spU_tblOrderItem(false);
			
			switch (this.lastKnownConnectionType) {
				case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
					Param.SetUpConnection(this.connectionString);
					break;

				case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
					Param.SetUpConnection(this.sqlConnection);
					break;
			}

			Param.Param_Oit_GuidID = this.currentID;

			if (Control_Oit_GuidOrderID.SelectedIndex != -1) {

				Param.Param_Oit_GuidOrderID = (System.Guid)Control_Oit_GuidOrderID.SelectedValue;
			}

			if (Control_Oit_GuidProductID.SelectedIndex != -1) {

				Param.Param_Oit_GuidProductID = (System.Guid)Control_Oit_GuidProductID.SelectedValue;
			}

			if (Control_Oit_LngAmount.Text.Trim() != String.Empty) {

				Param.Param_Oit_LngAmount = (System.Data.SqlTypes.SqlInt32)Control_Oit_LngAmount.GetSqlTypesValue;
			}

			SPs.spU_tblOrderItem SP = new SPs.spU_tblOrderItem(false);

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

					throw new OlymarsDemo.DataClasses.CustomException(Param, "OlymarsDemo.Windows.Forms.WinForm_tblOrderItem", "UpdateRecordInDatabase");
				}
			}
		}

		private void Button_Oit_GuidOrderID_Click(object sender, System.EventArgs e) {

			AddNew_tblOrder1(Control_Oit_GuidOrderID);
		}

		private void Button_Oit_GuidProductID_Click(object sender, System.EventArgs e) {

			AddNew_tblProduct2(Control_Oit_GuidProductID);
		}

		private void AddNew_tblOrder1(System.Windows.Forms.ComboBox CurrentCombo) {

			System.Data.SqlTypes.SqlGuid NewPrimaryKey;

			OlymarsDemo.Windows.Forms.WinForm_tblOrder MyForm = new OlymarsDemo.Windows.Forms.WinForm_tblOrder();
			MyForm.AddNewRecord(this, "Add a new tblOrder", ConnectionString);

			if (MyForm.DialogResult == DialogResult.OK) {

				if (!MyForm.ErrorHasOccured) {

					NewPrimaryKey = ((OlymarsDemo.DataClasses.Parameters.spI_tblOrder)MyForm.Parameter).Param_Ord_GuidID;
					Refresh_Oit_GuidOrderID();
					CurrentCombo.SelectedValue = NewPrimaryKey.Value;
				}

				MyForm.Dispose();
				CurrentCombo.Select();
			}
		}

		private void AddNew_tblProduct2(System.Windows.Forms.ComboBox CurrentCombo) {

			System.Data.SqlTypes.SqlGuid NewPrimaryKey;

			OlymarsDemo.Windows.Forms.WinForm_tblProduct MyForm = new OlymarsDemo.Windows.Forms.WinForm_tblProduct();
			MyForm.AddNewRecord(this, "Add a new tblProduct", ConnectionString);

			if (MyForm.DialogResult == DialogResult.OK) {

				if (!MyForm.ErrorHasOccured) {

					NewPrimaryKey = ((OlymarsDemo.DataClasses.Parameters.spI_tblProduct)MyForm.Parameter).Param_Pro_GuidID;
					Refresh_Oit_GuidProductID();
					CurrentCombo.SelectedValue = NewPrimaryKey.Value;
				}

				MyForm.Dispose();
				CurrentCombo.Select();
			}
		}

	}
}
