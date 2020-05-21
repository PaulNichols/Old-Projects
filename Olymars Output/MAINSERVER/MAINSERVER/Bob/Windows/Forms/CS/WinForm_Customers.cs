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
	/// designed for the 'Customers' table.
	/// </summary>
	public class WinForm_Customers : System.Windows.Forms.Form {

		private bool mustBeHidden_CompanyName = false;
		private bool mustBeDisabled_CompanyName = false;
		private bool hasBeenPopulated_CompanyName = false;
		private System.Data.SqlTypes.SqlString internal_CompanyName = System.Data.SqlTypes.SqlString.Null;
		private System.Windows.Forms.Label labCompanyName;
		private WinForm_DatabaseTextBox Control_CompanyName;
		private bool mustBeHidden_ContactName = false;
		private bool mustBeDisabled_ContactName = false;
		private bool hasBeenPopulated_ContactName = false;
		private System.Data.SqlTypes.SqlString internal_ContactName = System.Data.SqlTypes.SqlString.Null;
		private System.Windows.Forms.Label labContactName;
		private WinForm_DatabaseTextBox Control_ContactName;
		private bool mustBeHidden_TitleId = false;
		private bool mustBeDisabled_TitleId = false;
		private bool hasBeenPopulated_TitleId = false;
		private System.Data.SqlTypes.SqlInt32 internal_TitleId = System.Data.SqlTypes.SqlInt32.Null;
		private System.Windows.Forms.Label labTitleId;
		private System.Windows.Forms.CheckBox chkControl_TitleId;
		private Bob.Windows.ComboBoxes.WinComboBox_Title Control_TitleId;
		private System.Windows.Forms.Button Button_TitleId;
		private bool mustBeHidden_Address = false;
		private bool mustBeDisabled_Address = false;
		private bool hasBeenPopulated_Address = false;
		private System.Data.SqlTypes.SqlString internal_Address = System.Data.SqlTypes.SqlString.Null;
		private System.Windows.Forms.Label labAddress;
		private WinForm_DatabaseTextBox Control_Address;
		private bool mustBeHidden_City = false;
		private bool mustBeDisabled_City = false;
		private bool hasBeenPopulated_City = false;
		private System.Data.SqlTypes.SqlString internal_City = System.Data.SqlTypes.SqlString.Null;
		private System.Windows.Forms.Label labCity;
		private WinForm_DatabaseTextBox Control_City;
		private bool mustBeHidden_PostalCode = false;
		private bool mustBeDisabled_PostalCode = false;
		private bool hasBeenPopulated_PostalCode = false;
		private System.Data.SqlTypes.SqlString internal_PostalCode = System.Data.SqlTypes.SqlString.Null;
		private System.Windows.Forms.Label labPostalCode;
		private WinForm_DatabaseTextBox Control_PostalCode;
		private bool mustBeHidden_Phone = false;
		private bool mustBeDisabled_Phone = false;
		private bool hasBeenPopulated_Phone = false;
		private System.Data.SqlTypes.SqlString internal_Phone = System.Data.SqlTypes.SqlString.Null;
		private System.Windows.Forms.Label labPhone;
		private WinForm_DatabaseTextBox Control_Phone;
		private bool mustBeHidden_Email = false;
		private bool mustBeDisabled_Email = false;
		private bool hasBeenPopulated_Email = false;
		private System.Data.SqlTypes.SqlString internal_Email = System.Data.SqlTypes.SqlString.Null;
		private System.Windows.Forms.Label labEmail;
		private WinForm_DatabaseTextBox Control_Email;
		private bool mustBeHidden_WebAddress = false;
		private bool mustBeDisabled_WebAddress = false;
		private bool hasBeenPopulated_WebAddress = false;
		private System.Data.SqlTypes.SqlString internal_WebAddress = System.Data.SqlTypes.SqlString.Null;
		private System.Windows.Forms.Label labWebAddress;
		private WinForm_DatabaseTextBox Control_WebAddress;
		private bool mustBeHidden_Fax = false;
		private bool mustBeDisabled_Fax = false;
		private bool hasBeenPopulated_Fax = false;
		private System.Data.SqlTypes.SqlString internal_Fax = System.Data.SqlTypes.SqlString.Null;
		private System.Windows.Forms.Label labFax;
		private WinForm_DatabaseTextBox Control_Fax;
		private bool mustBeHidden_Active = false;
		private bool mustBeDisabled_Active = false;
		private bool hasBeenPopulated_Active = false;
		private System.Data.SqlTypes.SqlBoolean internal_Active = System.Data.SqlTypes.SqlBoolean.Null;
		private System.Windows.Forms.CheckBox Control_Active;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Button cmdCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'CompanyName' column must be hidden.
		/// </summary>
		public bool MustBeHidden_CompanyName {

			get {

				return(this.mustBeHidden_CompanyName);
			}

			set {

				this.mustBeHidden_CompanyName = value;
			}
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'CompanyName' column must be disabled.
		/// </summary>
		public bool MustBeDisabled_CompanyName {

			get {

				return(this.mustBeDisabled_CompanyName);
			}

			set {

				this.mustBeDisabled_CompanyName = value;
			}
		}

		/// <summary>
		/// Allows you to pre-select a value for the 'CompanyName' column.
		/// </summary>
		public void SetValue_CompanyName(System.Data.SqlTypes.SqlString value) {

			this.hasBeenPopulated_CompanyName = true;
			this.internal_CompanyName = value;
		}

		/// <summary>
		/// Allows you to reset the pre-selected value for the 'CompanyName' column.
		/// </summary>
		public void ResetValue_CompanyName() {

			this.hasBeenPopulated_CompanyName = false;
			this.internal_CompanyName = System.Data.SqlTypes.SqlString.Null;
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'ContactName' column must be hidden.
		/// </summary>
		public bool MustBeHidden_ContactName {

			get {

				return(this.mustBeHidden_ContactName);
			}

			set {

				this.mustBeHidden_ContactName = value;
			}
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'ContactName' column must be disabled.
		/// </summary>
		public bool MustBeDisabled_ContactName {

			get {

				return(this.mustBeDisabled_ContactName);
			}

			set {

				this.mustBeDisabled_ContactName = value;
			}
		}

		/// <summary>
		/// Allows you to pre-select a value for the 'ContactName' column.
		/// </summary>
		public void SetValue_ContactName(System.Data.SqlTypes.SqlString value) {

			this.hasBeenPopulated_ContactName = true;
			this.internal_ContactName = value;
		}

		/// <summary>
		/// Allows you to reset the pre-selected value for the 'ContactName' column.
		/// </summary>
		public void ResetValue_ContactName() {

			this.hasBeenPopulated_ContactName = false;
			this.internal_ContactName = System.Data.SqlTypes.SqlString.Null;
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'TitleId' column must be hidden.
		/// </summary>
		public bool MustBeHidden_TitleId {

			get {

				return(this.mustBeHidden_TitleId);
			}

			set {

				this.mustBeHidden_TitleId = value;
			}
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'TitleId' column must be disabled.
		/// </summary>
		public bool MustBeDisabled_TitleId {

			get {

				return(this.mustBeDisabled_TitleId);
			}

			set {

				this.mustBeDisabled_TitleId = value;
			}
		}

		/// <summary>
		/// Allows you to pre-select a value for the 'TitleId' column.
		/// </summary>
		public void SetValue_TitleId(System.Data.SqlTypes.SqlInt32 value) {

			this.hasBeenPopulated_TitleId = true;
			this.internal_TitleId = value;
		}

		/// <summary>
		/// Allows you to reset the pre-selected value for the 'TitleId' column.
		/// </summary>
		public void ResetValue_TitleId() {

			this.hasBeenPopulated_TitleId = false;
			this.internal_TitleId = System.Data.SqlTypes.SqlInt32.Null;
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'Address' column must be hidden.
		/// </summary>
		public bool MustBeHidden_Address {

			get {

				return(this.mustBeHidden_Address);
			}

			set {

				this.mustBeHidden_Address = value;
			}
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'Address' column must be disabled.
		/// </summary>
		public bool MustBeDisabled_Address {

			get {

				return(this.mustBeDisabled_Address);
			}

			set {

				this.mustBeDisabled_Address = value;
			}
		}

		/// <summary>
		/// Allows you to pre-select a value for the 'Address' column.
		/// </summary>
		public void SetValue_Address(System.Data.SqlTypes.SqlString value) {

			this.hasBeenPopulated_Address = true;
			this.internal_Address = value;
		}

		/// <summary>
		/// Allows you to reset the pre-selected value for the 'Address' column.
		/// </summary>
		public void ResetValue_Address() {

			this.hasBeenPopulated_Address = false;
			this.internal_Address = System.Data.SqlTypes.SqlString.Null;
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'City' column must be hidden.
		/// </summary>
		public bool MustBeHidden_City {

			get {

				return(this.mustBeHidden_City);
			}

			set {

				this.mustBeHidden_City = value;
			}
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'City' column must be disabled.
		/// </summary>
		public bool MustBeDisabled_City {

			get {

				return(this.mustBeDisabled_City);
			}

			set {

				this.mustBeDisabled_City = value;
			}
		}

		/// <summary>
		/// Allows you to pre-select a value for the 'City' column.
		/// </summary>
		public void SetValue_City(System.Data.SqlTypes.SqlString value) {

			this.hasBeenPopulated_City = true;
			this.internal_City = value;
		}

		/// <summary>
		/// Allows you to reset the pre-selected value for the 'City' column.
		/// </summary>
		public void ResetValue_City() {

			this.hasBeenPopulated_City = false;
			this.internal_City = System.Data.SqlTypes.SqlString.Null;
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'PostalCode' column must be hidden.
		/// </summary>
		public bool MustBeHidden_PostalCode {

			get {

				return(this.mustBeHidden_PostalCode);
			}

			set {

				this.mustBeHidden_PostalCode = value;
			}
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'PostalCode' column must be disabled.
		/// </summary>
		public bool MustBeDisabled_PostalCode {

			get {

				return(this.mustBeDisabled_PostalCode);
			}

			set {

				this.mustBeDisabled_PostalCode = value;
			}
		}

		/// <summary>
		/// Allows you to pre-select a value for the 'PostalCode' column.
		/// </summary>
		public void SetValue_PostalCode(System.Data.SqlTypes.SqlString value) {

			this.hasBeenPopulated_PostalCode = true;
			this.internal_PostalCode = value;
		}

		/// <summary>
		/// Allows you to reset the pre-selected value for the 'PostalCode' column.
		/// </summary>
		public void ResetValue_PostalCode() {

			this.hasBeenPopulated_PostalCode = false;
			this.internal_PostalCode = System.Data.SqlTypes.SqlString.Null;
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'Phone' column must be hidden.
		/// </summary>
		public bool MustBeHidden_Phone {

			get {

				return(this.mustBeHidden_Phone);
			}

			set {

				this.mustBeHidden_Phone = value;
			}
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'Phone' column must be disabled.
		/// </summary>
		public bool MustBeDisabled_Phone {

			get {

				return(this.mustBeDisabled_Phone);
			}

			set {

				this.mustBeDisabled_Phone = value;
			}
		}

		/// <summary>
		/// Allows you to pre-select a value for the 'Phone' column.
		/// </summary>
		public void SetValue_Phone(System.Data.SqlTypes.SqlString value) {

			this.hasBeenPopulated_Phone = true;
			this.internal_Phone = value;
		}

		/// <summary>
		/// Allows you to reset the pre-selected value for the 'Phone' column.
		/// </summary>
		public void ResetValue_Phone() {

			this.hasBeenPopulated_Phone = false;
			this.internal_Phone = System.Data.SqlTypes.SqlString.Null;
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'Email' column must be hidden.
		/// </summary>
		public bool MustBeHidden_Email {

			get {

				return(this.mustBeHidden_Email);
			}

			set {

				this.mustBeHidden_Email = value;
			}
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'Email' column must be disabled.
		/// </summary>
		public bool MustBeDisabled_Email {

			get {

				return(this.mustBeDisabled_Email);
			}

			set {

				this.mustBeDisabled_Email = value;
			}
		}

		/// <summary>
		/// Allows you to pre-select a value for the 'Email' column.
		/// </summary>
		public void SetValue_Email(System.Data.SqlTypes.SqlString value) {

			this.hasBeenPopulated_Email = true;
			this.internal_Email = value;
		}

		/// <summary>
		/// Allows you to reset the pre-selected value for the 'Email' column.
		/// </summary>
		public void ResetValue_Email() {

			this.hasBeenPopulated_Email = false;
			this.internal_Email = System.Data.SqlTypes.SqlString.Null;
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'WebAddress' column must be hidden.
		/// </summary>
		public bool MustBeHidden_WebAddress {

			get {

				return(this.mustBeHidden_WebAddress);
			}

			set {

				this.mustBeHidden_WebAddress = value;
			}
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'WebAddress' column must be disabled.
		/// </summary>
		public bool MustBeDisabled_WebAddress {

			get {

				return(this.mustBeDisabled_WebAddress);
			}

			set {

				this.mustBeDisabled_WebAddress = value;
			}
		}

		/// <summary>
		/// Allows you to pre-select a value for the 'WebAddress' column.
		/// </summary>
		public void SetValue_WebAddress(System.Data.SqlTypes.SqlString value) {

			this.hasBeenPopulated_WebAddress = true;
			this.internal_WebAddress = value;
		}

		/// <summary>
		/// Allows you to reset the pre-selected value for the 'WebAddress' column.
		/// </summary>
		public void ResetValue_WebAddress() {

			this.hasBeenPopulated_WebAddress = false;
			this.internal_WebAddress = System.Data.SqlTypes.SqlString.Null;
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'Fax' column must be hidden.
		/// </summary>
		public bool MustBeHidden_Fax {

			get {

				return(this.mustBeHidden_Fax);
			}

			set {

				this.mustBeHidden_Fax = value;
			}
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'Fax' column must be disabled.
		/// </summary>
		public bool MustBeDisabled_Fax {

			get {

				return(this.mustBeDisabled_Fax);
			}

			set {

				this.mustBeDisabled_Fax = value;
			}
		}

		/// <summary>
		/// Allows you to pre-select a value for the 'Fax' column.
		/// </summary>
		public void SetValue_Fax(System.Data.SqlTypes.SqlString value) {

			this.hasBeenPopulated_Fax = true;
			this.internal_Fax = value;
		}

		/// <summary>
		/// Allows you to reset the pre-selected value for the 'Fax' column.
		/// </summary>
		public void ResetValue_Fax() {

			this.hasBeenPopulated_Fax = false;
			this.internal_Fax = System.Data.SqlTypes.SqlString.Null;
		}


		private void ProcessHiddenAndDisabledControlStatus(bool inAddingProcess) {

			// CompanyName column
			this.Control_CompanyName.Enabled = !this.mustBeDisabled_CompanyName;
			this.Control_CompanyName.Enabled = !this.mustBeHidden_CompanyName;

			if (inAddingProcess) {

				if (this.hasBeenPopulated_CompanyName) {

					if (this.internal_CompanyName.IsNull) {

						this.Control_CompanyName.Text = String.Empty;
					}
					else {

						this.Control_CompanyName.Text = this.internal_CompanyName.Value.ToString();
					}
				}
			}

			// ContactName column
			this.Control_ContactName.Enabled = !this.mustBeDisabled_ContactName;
			this.Control_ContactName.Enabled = !this.mustBeHidden_ContactName;

			if (inAddingProcess) {

				if (this.hasBeenPopulated_ContactName) {

					if (this.internal_ContactName.IsNull) {

						this.Control_ContactName.Text = String.Empty;
					}
					else {

						this.Control_ContactName.Text = this.internal_ContactName.Value.ToString();
					}
				}
			}

			// TitleId column
			if (this.mustBeDisabled_TitleId) {

				if (!this.internal_TitleId.IsNull) {

					this.chkControl_TitleId.Checked = true;
				}
				this.chkControl_TitleId.Enabled = false;
				this.Control_TitleId.Enabled = false;
				this.Button_TitleId.Enabled = false;
			}

			if (this.mustBeHidden_TitleId) {

				this.chkControl_TitleId.Visible = false;
				this.Control_TitleId.Visible = false;
				this.Button_TitleId.Visible = false;
			}

			if (inAddingProcess) {

				if (this.hasBeenPopulated_TitleId) {

					if (this.internal_TitleId.IsNull) {

						this.Control_TitleId.SelectedIndex = -1;
					}
					else {

						this.Control_TitleId.SelectedValue = this.internal_TitleId.Value;
					}
				}
			}

			// Address column
			this.Control_Address.Enabled = !this.mustBeDisabled_Address;
			this.Control_Address.Enabled = !this.mustBeHidden_Address;

			if (inAddingProcess) {

				if (this.hasBeenPopulated_Address) {

					if (this.internal_Address.IsNull) {

						this.Control_Address.Text = String.Empty;
					}
					else {

						this.Control_Address.Text = this.internal_Address.Value.ToString();
					}
				}
			}

			// City column
			this.Control_City.Enabled = !this.mustBeDisabled_City;
			this.Control_City.Enabled = !this.mustBeHidden_City;

			if (inAddingProcess) {

				if (this.hasBeenPopulated_City) {

					if (this.internal_City.IsNull) {

						this.Control_City.Text = String.Empty;
					}
					else {

						this.Control_City.Text = this.internal_City.Value.ToString();
					}
				}
			}

			// PostalCode column
			this.Control_PostalCode.Enabled = !this.mustBeDisabled_PostalCode;
			this.Control_PostalCode.Enabled = !this.mustBeHidden_PostalCode;

			if (inAddingProcess) {

				if (this.hasBeenPopulated_PostalCode) {

					if (this.internal_PostalCode.IsNull) {

						this.Control_PostalCode.Text = String.Empty;
					}
					else {

						this.Control_PostalCode.Text = this.internal_PostalCode.Value.ToString();
					}
				}
			}

			// Phone column
			this.Control_Phone.Enabled = !this.mustBeDisabled_Phone;
			this.Control_Phone.Enabled = !this.mustBeHidden_Phone;

			if (inAddingProcess) {

				if (this.hasBeenPopulated_Phone) {

					if (this.internal_Phone.IsNull) {

						this.Control_Phone.Text = String.Empty;
					}
					else {

						this.Control_Phone.Text = this.internal_Phone.Value.ToString();
					}
				}
			}

			// Email column
			this.Control_Email.Enabled = !this.mustBeDisabled_Email;
			this.Control_Email.Enabled = !this.mustBeHidden_Email;

			if (inAddingProcess) {

				if (this.hasBeenPopulated_Email) {

					if (this.internal_Email.IsNull) {

						this.Control_Email.Text = String.Empty;
					}
					else {

						this.Control_Email.Text = this.internal_Email.Value.ToString();
					}
				}
			}

			// WebAddress column
			this.Control_WebAddress.Enabled = !this.mustBeDisabled_WebAddress;
			this.Control_WebAddress.Enabled = !this.mustBeHidden_WebAddress;

			if (inAddingProcess) {

				if (this.hasBeenPopulated_WebAddress) {

					if (this.internal_WebAddress.IsNull) {

						this.Control_WebAddress.Text = String.Empty;
					}
					else {

						this.Control_WebAddress.Text = this.internal_WebAddress.Value.ToString();
					}
				}
			}

			// Fax column
			this.Control_Fax.Enabled = !this.mustBeDisabled_Fax;
			this.Control_Fax.Enabled = !this.mustBeHidden_Fax;

			if (inAddingProcess) {

				if (this.hasBeenPopulated_Fax) {

					if (this.internal_Fax.IsNull) {

						this.Control_Fax.Text = String.Empty;
					}
					else {

						this.Control_Fax.Text = this.internal_Fax.Value.ToString();
					}
				}
			}

			// Active column
			this.Control_Active.Enabled = !this.mustBeDisabled_Active;
			this.Control_Active.Enabled = !this.mustBeHidden_Active;

			if (inAddingProcess) {

				if (this.hasBeenPopulated_Active) {

					if (this.internal_Active.IsNull) {

						this.Control_Active.CheckState = CheckState.Indeterminate;
					}
					else {

						this.Control_Active.Checked = this.internal_Active.Value;
					}
				}
			}
		}

		/// <summary>
		/// Create a new instance of the Bob.Windows.Forms.WinForm_Customers class.
		/// </summary>
		public WinForm_Customers() {

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

			this.labCompanyName = new System.Windows.Forms.Label();
			this.Control_CompanyName = new WinForm_DatabaseTextBox();
			this.labContactName = new System.Windows.Forms.Label();
			this.Control_ContactName = new WinForm_DatabaseTextBox();
			this.labTitleId = new System.Windows.Forms.Label();
			this.chkControl_TitleId = new System.Windows.Forms.CheckBox();
			this.Control_TitleId = new Bob.Windows.ComboBoxes.WinComboBox_Title();
			this.Button_TitleId = new System.Windows.Forms.Button();
			this.labAddress = new System.Windows.Forms.Label();
			this.Control_Address = new WinForm_DatabaseTextBox();
			this.labCity = new System.Windows.Forms.Label();
			this.Control_City = new WinForm_DatabaseTextBox();
			this.labPostalCode = new System.Windows.Forms.Label();
			this.Control_PostalCode = new WinForm_DatabaseTextBox();
			this.labPhone = new System.Windows.Forms.Label();
			this.Control_Phone = new WinForm_DatabaseTextBox();
			this.labEmail = new System.Windows.Forms.Label();
			this.Control_Email = new WinForm_DatabaseTextBox();
			this.labWebAddress = new System.Windows.Forms.Label();
			this.Control_WebAddress = new WinForm_DatabaseTextBox();
			this.labFax = new System.Windows.Forms.Label();
			this.Control_Fax = new WinForm_DatabaseTextBox();
			this.Control_Active = new System.Windows.Forms.CheckBox();
			this.cmdOK = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// labCompanyName
			// 
			this.labCompanyName.AutoSize = true;
			this.labCompanyName.Location = new System.Drawing.Point(24, 24);
			this.labCompanyName.Name = "labCompanyName";
			this.labCompanyName.Size = new System.Drawing.Size(110, 13);
			this.labCompanyName.TabIndex = 0;
			this.labCompanyName.Text = "Company Name:";
			// 
			// Control_CompanyName
			// 
			this.Control_CompanyName.Location = new System.Drawing.Point(24, 40);
			this.Control_CompanyName.Name = "Control_CompanyName";
			this.Control_CompanyName.NullIsAllow = true;
			this.Control_CompanyName.InvalidBackColor = System.Drawing.Color.Red;
			this.Control_CompanyName.Size = new System.Drawing.Size(440, 20);
			this.Control_CompanyName.TabIndex = 1;
			this.Control_CompanyName.DataSize = 80;
			this.Control_CompanyName.MaxLength = 80;
			this.Control_CompanyName.TextChanged += new System.EventHandler(this.Control_CompanyName_TextChanged);
			this.Control_CompanyName.DBType = SupportedDatabaseTypes.DBType_nvarchar;
			// 
			// labContactName
			// 
			this.labContactName.AutoSize = true;
			this.labContactName.Location = new System.Drawing.Point(24, 80);
			this.labContactName.Name = "labContactName";
			this.labContactName.Size = new System.Drawing.Size(110, 13);
			this.labContactName.TabIndex = 2;
			this.labContactName.Text = "Customer/Contact Name:";
			// 
			// Control_ContactName
			// 
			this.Control_ContactName.Location = new System.Drawing.Point(24, 96);
			this.Control_ContactName.Name = "Control_ContactName";
			this.Control_ContactName.NullIsAllow = false;
			this.Control_ContactName.InvalidBackColor = System.Drawing.Color.Red;
			this.Control_ContactName.Size = new System.Drawing.Size(440, 20);
			this.Control_ContactName.TabIndex = 3;
			this.Control_ContactName.DataSize = 60;
			this.Control_ContactName.MaxLength = 60;
			this.Control_ContactName.TextChanged += new System.EventHandler(this.Control_ContactName_TextChanged);
			this.Control_ContactName.DBType = SupportedDatabaseTypes.DBType_nvarchar;
			// 
			// labTitleId
			// 
			this.labTitleId.AutoSize = true;
			this.labTitleId.Location = new System.Drawing.Point(24, 136);
			this.labTitleId.Name = "labTitleId";
			this.labTitleId.Size = new System.Drawing.Size(126, 13);
			this.labTitleId.TabIndex = 4;
			this.labTitleId.Text = "Title Id:";
			// 
			// chkControl_TitleId
			// 
			this.chkControl_TitleId.Location = new System.Drawing.Point(5, 152);
			this.chkControl_TitleId.Name = "chkControl_TitleId";
			this.chkControl_TitleId.Size = new System.Drawing.Size(19, 19);
			this.chkControl_TitleId.TabIndex = 5;
			this.chkControl_TitleId.CheckedChanged += new System.EventHandler(this.chkControl_TitleId_CheckedChanged);
			// 
			// Control_TitleId
			// 
			this.Control_TitleId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Control_TitleId.DropDownWidth = 392;
			this.Control_TitleId.Location = new System.Drawing.Point(24, 152);
			this.Control_TitleId.Name = "Control_TitleId";
			this.Control_TitleId.Size = new System.Drawing.Size(352, 21);
			this.Control_TitleId.TabIndex = 6;
			this.Control_TitleId.SelectedIndexChanged += new System.EventHandler(this.Control_TitleId_SelectedIndexChanged);
			// 
			// Button_TitleId
			// 
			this.Button_TitleId.Location = new System.Drawing.Point(392, 152);
			this.Button_TitleId.Name = "Button_TitleId";
			this.Button_TitleId.TabIndex = 7;
			this.Button_TitleId.Text = "Add";
			this.Button_TitleId.Click += new System.EventHandler(this.Button_TitleId_Click);
			// 
			// labAddress
			// 
			this.labAddress.AutoSize = true;
			this.labAddress.Location = new System.Drawing.Point(24, 192);
			this.labAddress.Name = "labAddress";
			this.labAddress.Size = new System.Drawing.Size(110, 13);
			this.labAddress.TabIndex = 8;
			this.labAddress.Text = "Customer/Contact Address:";
			// 
			// Control_Address
			// 
			this.Control_Address.Location = new System.Drawing.Point(24, 208);
			this.Control_Address.Name = "Control_Address";
			this.Control_Address.NullIsAllow = true;
			this.Control_Address.InvalidBackColor = System.Drawing.Color.Red;
			this.Control_Address.Size = new System.Drawing.Size(440, 20);
			this.Control_Address.TabIndex = 9;
			this.Control_Address.DataSize = 120;
			this.Control_Address.MaxLength = 120;
			this.Control_Address.TextChanged += new System.EventHandler(this.Control_Address_TextChanged);
			this.Control_Address.DBType = SupportedDatabaseTypes.DBType_nvarchar;
			// 
			// labCity
			// 
			this.labCity.AutoSize = true;
			this.labCity.Location = new System.Drawing.Point(24, 248);
			this.labCity.Name = "labCity";
			this.labCity.Size = new System.Drawing.Size(110, 13);
			this.labCity.TabIndex = 10;
			this.labCity.Text = "Customer/Contact City:";
			// 
			// Control_City
			// 
			this.Control_City.Location = new System.Drawing.Point(24, 264);
			this.Control_City.Name = "Control_City";
			this.Control_City.NullIsAllow = true;
			this.Control_City.InvalidBackColor = System.Drawing.Color.Red;
			this.Control_City.Size = new System.Drawing.Size(440, 20);
			this.Control_City.TabIndex = 11;
			this.Control_City.DataSize = 30;
			this.Control_City.MaxLength = 30;
			this.Control_City.TextChanged += new System.EventHandler(this.Control_City_TextChanged);
			this.Control_City.DBType = SupportedDatabaseTypes.DBType_nvarchar;
			// 
			// labPostalCode
			// 
			this.labPostalCode.AutoSize = true;
			this.labPostalCode.Location = new System.Drawing.Point(24, 304);
			this.labPostalCode.Name = "labPostalCode";
			this.labPostalCode.Size = new System.Drawing.Size(110, 13);
			this.labPostalCode.TabIndex = 12;
			this.labPostalCode.Text = "Customer/Contact Post Code:";
			// 
			// Control_PostalCode
			// 
			this.Control_PostalCode.Location = new System.Drawing.Point(24, 320);
			this.Control_PostalCode.Name = "Control_PostalCode";
			this.Control_PostalCode.NullIsAllow = true;
			this.Control_PostalCode.InvalidBackColor = System.Drawing.Color.Red;
			this.Control_PostalCode.Size = new System.Drawing.Size(440, 20);
			this.Control_PostalCode.TabIndex = 13;
			this.Control_PostalCode.DataSize = 20;
			this.Control_PostalCode.MaxLength = 20;
			this.Control_PostalCode.TextChanged += new System.EventHandler(this.Control_PostalCode_TextChanged);
			this.Control_PostalCode.DBType = SupportedDatabaseTypes.DBType_nvarchar;
			// 
			// labPhone
			// 
			this.labPhone.AutoSize = true;
			this.labPhone.Location = new System.Drawing.Point(24, 360);
			this.labPhone.Name = "labPhone";
			this.labPhone.Size = new System.Drawing.Size(110, 13);
			this.labPhone.TabIndex = 14;
			this.labPhone.Text = "Customer/Contact Phone:";
			// 
			// Control_Phone
			// 
			this.Control_Phone.Location = new System.Drawing.Point(24, 376);
			this.Control_Phone.Name = "Control_Phone";
			this.Control_Phone.NullIsAllow = true;
			this.Control_Phone.InvalidBackColor = System.Drawing.Color.Red;
			this.Control_Phone.Size = new System.Drawing.Size(440, 20);
			this.Control_Phone.TabIndex = 15;
			this.Control_Phone.DataSize = 48;
			this.Control_Phone.MaxLength = 48;
			this.Control_Phone.TextChanged += new System.EventHandler(this.Control_Phone_TextChanged);
			this.Control_Phone.DBType = SupportedDatabaseTypes.DBType_nvarchar;
			// 
			// labEmail
			// 
			this.labEmail.AutoSize = true;
			this.labEmail.Location = new System.Drawing.Point(24, 416);
			this.labEmail.Name = "labEmail";
			this.labEmail.Size = new System.Drawing.Size(110, 13);
			this.labEmail.TabIndex = 16;
			this.labEmail.Text = "Email Address:";
			// 
			// Control_Email
			// 
			this.Control_Email.Location = new System.Drawing.Point(24, 432);
			this.Control_Email.Name = "Control_Email";
			this.Control_Email.NullIsAllow = true;
			this.Control_Email.InvalidBackColor = System.Drawing.Color.Red;
			this.Control_Email.Size = new System.Drawing.Size(440, 20);
			this.Control_Email.TabIndex = 17;
			this.Control_Email.DataSize = 200;
			this.Control_Email.MaxLength = 200;
			this.Control_Email.TextChanged += new System.EventHandler(this.Control_Email_TextChanged);
			this.Control_Email.DBType = SupportedDatabaseTypes.DBType_nvarchar;
			// 
			// labWebAddress
			// 
			this.labWebAddress.AutoSize = true;
			this.labWebAddress.Location = new System.Drawing.Point(24, 472);
			this.labWebAddress.Name = "labWebAddress";
			this.labWebAddress.Size = new System.Drawing.Size(110, 13);
			this.labWebAddress.TabIndex = 18;
			this.labWebAddress.Text = "Web Address:";
			// 
			// Control_WebAddress
			// 
			this.Control_WebAddress.Location = new System.Drawing.Point(24, 488);
			this.Control_WebAddress.Name = "Control_WebAddress";
			this.Control_WebAddress.NullIsAllow = true;
			this.Control_WebAddress.InvalidBackColor = System.Drawing.Color.Red;
			this.Control_WebAddress.Size = new System.Drawing.Size(440, 20);
			this.Control_WebAddress.TabIndex = 19;
			this.Control_WebAddress.DataSize = 48;
			this.Control_WebAddress.MaxLength = 48;
			this.Control_WebAddress.TextChanged += new System.EventHandler(this.Control_WebAddress_TextChanged);
			this.Control_WebAddress.DBType = SupportedDatabaseTypes.DBType_nvarchar;
			// 
			// labFax
			// 
			this.labFax.AutoSize = true;
			this.labFax.Location = new System.Drawing.Point(24, 528);
			this.labFax.Name = "labFax";
			this.labFax.Size = new System.Drawing.Size(110, 13);
			this.labFax.TabIndex = 20;
			this.labFax.Text = "Customer/Contact Fax:";
			// 
			// Control_Fax
			// 
			this.Control_Fax.Location = new System.Drawing.Point(24, 544);
			this.Control_Fax.Name = "Control_Fax";
			this.Control_Fax.NullIsAllow = true;
			this.Control_Fax.InvalidBackColor = System.Drawing.Color.Red;
			this.Control_Fax.Size = new System.Drawing.Size(440, 20);
			this.Control_Fax.TabIndex = 21;
			this.Control_Fax.DataSize = 48;
			this.Control_Fax.MaxLength = 48;
			this.Control_Fax.TextChanged += new System.EventHandler(this.Control_Fax_TextChanged);
			this.Control_Fax.DBType = SupportedDatabaseTypes.DBType_nvarchar;
			// 
			// Control_Active
			// 
			this.Control_Active.Location = new System.Drawing.Point(24, 584);
			this.Control_Active.Name = "Control_Active";
			this.Control_Active.Size = new System.Drawing.Size(440, 24);
			this.Control_Active.TabIndex = 22;
			this.Control_Active.Text = "Active";
			// 
			// cmdOK
			// 
			this.cmdOK.Location = new System.Drawing.Point(304, 624);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.TabIndex = 23;
			this.cmdOK.Text = "OK";
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(392, 624);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.TabIndex = 23;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// WinForm_Customers
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 13);
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(506, 664);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
						this.labCompanyName,
						this.Control_CompanyName,
						this.labContactName,
						this.Control_ContactName,
						this.labTitleId,
						this.chkControl_TitleId,
						this.Control_TitleId,
						this.Button_TitleId,
						this.labAddress,
						this.Control_Address,
						this.labCity,
						this.Control_City,
						this.labPostalCode,
						this.Control_PostalCode,
						this.labPhone,
						this.Control_Phone,
						this.labEmail,
						this.Control_Email,
						this.labWebAddress,
						this.Control_WebAddress,
						this.labFax,
						this.Control_Fax,
						this.Control_Active,
						this.cmdOK,
						this.cmdCancel
						});
			this.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "WinForm_Customers";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Customers management";
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

			Control_CompanyName.Text = String.Empty;

			Control_ContactName.Text = String.Empty;

			Control_TitleId.SelectedIndex = -1;
			Control_TitleId.Enabled = false;
			chkControl_TitleId.Checked = false;

			Control_Address.Text = String.Empty;

			Control_City.Text = String.Empty;

			Control_PostalCode.Text = String.Empty;

			Control_Phone.Text = String.Empty;

			Control_Email.Text = String.Empty;

			Control_WebAddress.Text = String.Empty;

			Control_Fax.Text = String.Empty;

			Control_Active.Checked = false;


			this.IgnoreChanges = false;
		}

		/// <summary>
		/// Allows you to add a new record in the Customers table.
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

			this.Control_CompanyName.HandleTextChanged(null, null);
			this.Control_ContactName.HandleTextChanged(null, null);
			this.Control_Address.HandleTextChanged(null, null);
			this.Control_City.HandleTextChanged(null, null);
			this.Control_PostalCode.HandleTextChanged(null, null);
			this.Control_Phone.HandleTextChanged(null, null);
			this.Control_Email.HandleTextChanged(null, null);
			this.Control_WebAddress.HandleTextChanged(null, null);
			this.Control_Fax.HandleTextChanged(null, null);

			Check_cmdOK_Button(null);

			ProcessHiddenAndDisabledControlStatus(true);

			this.ShowDialog(caller);
		}

		/// <summary>
		/// Allows you to add a new record in the Customers table.
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

			this.Control_CompanyName.HandleTextChanged(null, null);
			this.Control_ContactName.HandleTextChanged(null, null);
			this.Control_Address.HandleTextChanged(null, null);
			this.Control_City.HandleTextChanged(null, null);
			this.Control_PostalCode.HandleTextChanged(null, null);
			this.Control_Phone.HandleTextChanged(null, null);
			this.Control_Email.HandleTextChanged(null, null);
			this.Control_WebAddress.HandleTextChanged(null, null);
			this.Control_Fax.HandleTextChanged(null, null);

			Check_cmdOK_Button(null);

			ProcessHiddenAndDisabledControlStatus(true);

			this.ShowDialog(caller);
		}

		/// <summary>
		/// Allows you to update an existing record from the Customers table.
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
		/// Allows you to update an existing record from the Customers table.
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

				Status = (Status && Control_CompanyName.IsValid);
				Status = (Status && Control_ContactName.IsValid);
				Status = (Status && Control_Address.IsValid);
				Status = (Status && Control_City.IsValid);
				Status = (Status && Control_PostalCode.IsValid);
				Status = (Status && Control_Phone.IsValid);
				Status = (Status && Control_Email.IsValid);
				Status = (Status && Control_WebAddress.IsValid);
				Status = (Status && Control_Fax.IsValid);
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

			Refresh_TitleId();
		}

		private void Control_TitleId_SelectedIndexChanged(object sender, System.EventArgs e) {

			if (!this.IgnoreChanges) Check_cmdOK_Button(Control_TitleId);
		}

		private void chkControl_TitleId_CheckedChanged(object sender, System.EventArgs e) {

			if (chkControl_TitleId.Checked) {

				Control_TitleId.Enabled = true;
				Control_TitleId.SelectedIndex = 0;
				Button_TitleId.Enabled = true;
			}
			else {

				Control_TitleId.SelectedIndex = -1;
				Control_TitleId.Enabled = false;
				Button_TitleId.Enabled = false;
			}
		}


		private void Refresh_TitleId() {

			this.IgnoreChanges = true;

			switch (this.lastKnownConnectionType) {
				case Bob.DataClasses.ConnectionType.ConnectionString:
					Control_TitleId.Initialize(this.connectionString);
					break;

				case Bob.DataClasses.ConnectionType.SqlConnection:
					Control_TitleId.Initialize(this.sqlConnection);
					break;
			}

			try {

				Control_TitleId.RefreshData(System.Data.SqlTypes.SqlInt32.Null);
			}
			catch (Bob.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinComboBox_Title' class. Exception message is: {0}", customException.Parameter.SqlException.Message), customException);
				}
				else if (customException.Parameter.OtherException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinComboBox_Title' class. Exception message is: {0}", customException.Parameter.OtherException.Message), customException);
				}
				else {

					throw;
				}
			}

			Control_TitleId.SelectedIndex = -1;
			Control_TitleId.Enabled = false;
			chkControl_TitleId.Checked = false;
			chkControl_TitleId.Enabled = (Control_TitleId.Items.Count != 0);

			this.IgnoreChanges = false;
		}

		private bool RefreshCurrentRecord() {

			EmptyControls();

			Abstracts.Abstract_Customers oAbstract_Customers = null;
			switch (this.lastKnownConnectionType) {
				case Bob.DataClasses.ConnectionType.ConnectionString:
					oAbstract_Customers = new Abstracts.Abstract_Customers(this.connectionString);
					break;

				case Bob.DataClasses.ConnectionType.SqlConnection:
					oAbstract_Customers = new Abstracts.Abstract_Customers(this.sqlConnection);
					break;
			}

			if (oAbstract_Customers.Refresh(this.currentID)) {

				this.IgnoreChanges = true;

				if (!oAbstract_Customers.Col_CompanyName.IsNull) {

					Control_CompanyName.Text = oAbstract_Customers.Col_CompanyName.Value.ToString();
				}


				if (!oAbstract_Customers.Col_ContactName.IsNull) {

					Control_ContactName.Text = oAbstract_Customers.Col_ContactName.Value.ToString();
				}


				if (oAbstract_Customers.Col_TitleId.IsNull) {

					chkControl_TitleId.Checked = false;
					chkControl_TitleId_CheckedChanged(null, EventArgs.Empty);
				}
				else {

					chkControl_TitleId.Checked = true;
					Control_TitleId.SelectedValue = oAbstract_Customers.Col_TitleId.Value;
				}

				if (!oAbstract_Customers.Col_Address.IsNull) {

					Control_Address.Text = oAbstract_Customers.Col_Address.Value.ToString();
				}


				if (!oAbstract_Customers.Col_City.IsNull) {

					Control_City.Text = oAbstract_Customers.Col_City.Value.ToString();
				}


				if (!oAbstract_Customers.Col_PostalCode.IsNull) {

					Control_PostalCode.Text = oAbstract_Customers.Col_PostalCode.Value.ToString();
				}


				if (!oAbstract_Customers.Col_Phone.IsNull) {

					Control_Phone.Text = oAbstract_Customers.Col_Phone.Value.ToString();
				}


				if (!oAbstract_Customers.Col_Email.IsNull) {

					Control_Email.Text = oAbstract_Customers.Col_Email.Value.ToString();
				}


				if (!oAbstract_Customers.Col_WebAddress.IsNull) {

					Control_WebAddress.Text = oAbstract_Customers.Col_WebAddress.Value.ToString();
				}


				if (!oAbstract_Customers.Col_Fax.IsNull) {

					Control_Fax.Text = oAbstract_Customers.Col_Fax.Value.ToString();
				}


				if (!oAbstract_Customers.Col_Active.IsNull) {

					Control_Active.Checked = oAbstract_Customers.Col_Active.Value;
				}

				this.IgnoreChanges = false;
				return(true);
			}
			
			else {

				MessageBox.Show("The record does not exist any more !", "Record not found", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return(false);
			}
		}

		private void Control_CompanyName_TextChanged(object sender, System.EventArgs e) {

			if (!this.IgnoreChanges) {

				Check_cmdOK_Button(Control_CompanyName);
			}
		}

		private void Control_ContactName_TextChanged(object sender, System.EventArgs e) {

			if (!this.IgnoreChanges) {

				Check_cmdOK_Button(Control_ContactName);
			}
		}

		private void Control_Address_TextChanged(object sender, System.EventArgs e) {

			if (!this.IgnoreChanges) {

				Check_cmdOK_Button(Control_Address);
			}
		}

		private void Control_City_TextChanged(object sender, System.EventArgs e) {

			if (!this.IgnoreChanges) {

				Check_cmdOK_Button(Control_City);
			}
		}

		private void Control_PostalCode_TextChanged(object sender, System.EventArgs e) {

			if (!this.IgnoreChanges) {

				Check_cmdOK_Button(Control_PostalCode);
			}
		}

		private void Control_Phone_TextChanged(object sender, System.EventArgs e) {

			if (!this.IgnoreChanges) {

				Check_cmdOK_Button(Control_Phone);
			}
		}

		private void Control_Email_TextChanged(object sender, System.EventArgs e) {

			if (!this.IgnoreChanges) {

				Check_cmdOK_Button(Control_Email);
			}
		}

		private void Control_WebAddress_TextChanged(object sender, System.EventArgs e) {

			if (!this.IgnoreChanges) {

				Check_cmdOK_Button(Control_WebAddress);
			}
		}

		private void Control_Fax_TextChanged(object sender, System.EventArgs e) {

			if (!this.IgnoreChanges) {

				Check_cmdOK_Button(Control_Fax);
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

			Params.spI_Customers Param = new Params.spI_Customers(false);

			switch (this.lastKnownConnectionType) {
				case Bob.DataClasses.ConnectionType.ConnectionString:
					Param.SetUpConnection(this.connectionString);
					break;

				case Bob.DataClasses.ConnectionType.SqlConnection:
					Param.SetUpConnection(this.sqlConnection);
					break;
			}

			if (Control_CompanyName.Text.Trim() != String.Empty) {

				Param.Param_CompanyName = (System.Data.SqlTypes.SqlString)Control_CompanyName.GetSqlTypesValue;
			}

			if (Control_ContactName.Text.Trim() != String.Empty) {

				Param.Param_ContactName = (System.Data.SqlTypes.SqlString)Control_ContactName.GetSqlTypesValue;
			}

			if (chkControl_TitleId.Checked && Control_TitleId.SelectedIndex != -1) {

				Param.Param_TitleId = (System.Int32)Control_TitleId.SelectedValue;
			}

			if (Control_Address.Text.Trim() != String.Empty) {

				Param.Param_Address = (System.Data.SqlTypes.SqlString)Control_Address.GetSqlTypesValue;
			}

			if (Control_City.Text.Trim() != String.Empty) {

				Param.Param_City = (System.Data.SqlTypes.SqlString)Control_City.GetSqlTypesValue;
			}

			if (Control_PostalCode.Text.Trim() != String.Empty) {

				Param.Param_PostalCode = (System.Data.SqlTypes.SqlString)Control_PostalCode.GetSqlTypesValue;
			}

			if (Control_Phone.Text.Trim() != String.Empty) {

				Param.Param_Phone = (System.Data.SqlTypes.SqlString)Control_Phone.GetSqlTypesValue;
			}

			if (Control_Email.Text.Trim() != String.Empty) {

				Param.Param_Email = (System.Data.SqlTypes.SqlString)Control_Email.GetSqlTypesValue;
			}

			if (Control_WebAddress.Text.Trim() != String.Empty) {

				Param.Param_WebAddress = (System.Data.SqlTypes.SqlString)Control_WebAddress.GetSqlTypesValue;
			}

			if (Control_Fax.Text.Trim() != String.Empty) {

				Param.Param_Fax = (System.Data.SqlTypes.SqlString)Control_Fax.GetSqlTypesValue;
			}

			Param.Param_Active = new System.Data.SqlTypes.SqlBoolean(Control_Active.Checked);

			SPs.spI_Customers SP = new SPs.spI_Customers(false);

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

					throw new Bob.DataClasses.CustomException(Param, "Bob.Windows.Forms.WinForm_Customers", "AddRecordInDatabase");
				}
			}
		}

		private void UpdateRecordInDatabase() {

			Params.spU_Customers Param = new Params.spU_Customers(false);
			
			switch (this.lastKnownConnectionType) {
				case Bob.DataClasses.ConnectionType.ConnectionString:
					Param.SetUpConnection(this.connectionString);
					break;

				case Bob.DataClasses.ConnectionType.SqlConnection:
					Param.SetUpConnection(this.sqlConnection);
					break;
			}

			Param.Param_CustomerID = this.currentID;

			if (Control_CompanyName.Text.Trim() != String.Empty) {

				Param.Param_CompanyName = (System.Data.SqlTypes.SqlString)Control_CompanyName.GetSqlTypesValue;
			}

			if (Control_ContactName.Text.Trim() != String.Empty) {

				Param.Param_ContactName = (System.Data.SqlTypes.SqlString)Control_ContactName.GetSqlTypesValue;
			}

			if (chkControl_TitleId.Checked && Control_TitleId.SelectedIndex != -1) {

				Param.Param_TitleId = (System.Int32)Control_TitleId.SelectedValue;
			}
			else {

				Param.Param_ConsiderNull_TitleId = true;
				Param.Param_TitleId = System.Data.SqlTypes.SqlInt32.Null;
			}

			if (Control_Address.Text.Trim() != String.Empty) {

				Param.Param_Address = (System.Data.SqlTypes.SqlString)Control_Address.GetSqlTypesValue;
			}

			if (Control_City.Text.Trim() != String.Empty) {

				Param.Param_City = (System.Data.SqlTypes.SqlString)Control_City.GetSqlTypesValue;
			}

			if (Control_PostalCode.Text.Trim() != String.Empty) {

				Param.Param_PostalCode = (System.Data.SqlTypes.SqlString)Control_PostalCode.GetSqlTypesValue;
			}

			if (Control_Phone.Text.Trim() != String.Empty) {

				Param.Param_Phone = (System.Data.SqlTypes.SqlString)Control_Phone.GetSqlTypesValue;
			}

			if (Control_Email.Text.Trim() != String.Empty) {

				Param.Param_Email = (System.Data.SqlTypes.SqlString)Control_Email.GetSqlTypesValue;
			}

			if (Control_WebAddress.Text.Trim() != String.Empty) {

				Param.Param_WebAddress = (System.Data.SqlTypes.SqlString)Control_WebAddress.GetSqlTypesValue;
			}

			if (Control_Fax.Text.Trim() != String.Empty) {

				Param.Param_Fax = (System.Data.SqlTypes.SqlString)Control_Fax.GetSqlTypesValue;
			}

			Param.Param_Active = new System.Data.SqlTypes.SqlBoolean(Control_Active.Checked);

			SPs.spU_Customers SP = new SPs.spU_Customers(false);

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

					throw new Bob.DataClasses.CustomException(Param, "Bob.Windows.Forms.WinForm_Customers", "UpdateRecordInDatabase");
				}
			}
		}

		private void Button_TitleId_Click(object sender, System.EventArgs e) {

			AddNew_Title1(Control_TitleId);
		}

		private void AddNew_Title1(System.Windows.Forms.ComboBox CurrentCombo) {

			System.Data.SqlTypes.SqlInt32 NewPrimaryKey;

			Bob.Windows.Forms.WinForm_Title MyForm = new Bob.Windows.Forms.WinForm_Title();
			MyForm.AddNewRecord(this, "Add a new Title", ConnectionString);

			if (MyForm.DialogResult == DialogResult.OK) {

				if (!MyForm.ErrorHasOccured) {

					NewPrimaryKey = ((Bob.DataClasses.Parameters.spI_Title)MyForm.Parameter).Param_TitleId;
					Refresh_TitleId();
					CurrentCombo.SelectedValue = NewPrimaryKey.Value;
				}

				MyForm.Dispose();
				CurrentCombo.Select();
			}
		}

	}
}
