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
	/// designed for the 'JobPart' table.
	/// </summary>
	public class WinForm_JobPart : System.Windows.Forms.Form {

		private bool mustBeHidden_JobId = false;
		private bool mustBeDisabled_JobId = false;
		private bool hasBeenPopulated_JobId = false;
		private System.Data.SqlTypes.SqlInt32 internal_JobId = System.Data.SqlTypes.SqlInt32.Null;
		private System.Windows.Forms.Label labJobId;
		private Bob.Windows.ComboBoxes.WinComboBox_Job Control_JobId;
		private System.Windows.Forms.Button Button_JobId;
		private bool mustBeHidden_Description = false;
		private bool mustBeDisabled_Description = false;
		private bool hasBeenPopulated_Description = false;
		private System.Data.SqlTypes.SqlString internal_Description = System.Data.SqlTypes.SqlString.Null;
		private System.Windows.Forms.Label labDescription;
		private WinForm_DatabaseTextBox Control_Description;
		private bool mustBeHidden_JobPartTypeId = false;
		private bool mustBeDisabled_JobPartTypeId = false;
		private bool hasBeenPopulated_JobPartTypeId = false;
		private System.Data.SqlTypes.SqlInt32 internal_JobPartTypeId = System.Data.SqlTypes.SqlInt32.Null;
		private System.Windows.Forms.Label labJobPartTypeId;
		private Bob.Windows.ComboBoxes.WinComboBox_JobPartType Control_JobPartTypeId;
		private System.Windows.Forms.Button Button_JobPartTypeId;
		private bool mustBeHidden_Units = false;
		private bool mustBeDisabled_Units = false;
		private bool hasBeenPopulated_Units = false;
		private System.Data.SqlTypes.SqlDecimal internal_Units = System.Data.SqlTypes.SqlDecimal.Null;
		private System.Windows.Forms.Label labUnits;
		private WinForm_DatabaseTextBox Control_Units;
		private bool mustBeHidden_PricePerUnit = false;
		private bool mustBeDisabled_PricePerUnit = false;
		private bool hasBeenPopulated_PricePerUnit = false;
		private System.Data.SqlTypes.SqlMoney internal_PricePerUnit = System.Data.SqlTypes.SqlMoney.Null;
		private System.Windows.Forms.Label labPricePerUnit;
		private WinForm_DatabaseTextBox Control_PricePerUnit;
		private bool mustBeHidden_TotalPrice = false;
		private bool mustBeDisabled_TotalPrice = false;
		private bool hasBeenPopulated_TotalPrice = false;
		private System.Data.SqlTypes.SqlMoney internal_TotalPrice = System.Data.SqlTypes.SqlMoney.Null;
		private System.Windows.Forms.Label labTotalPrice;
		private WinForm_DatabaseTextBox Control_TotalPrice;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Button cmdCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'JobId' column must be hidden.
		/// </summary>
		public bool MustBeHidden_JobId {

			get {

				return(this.mustBeHidden_JobId);
			}

			set {

				this.mustBeHidden_JobId = value;
			}
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'JobId' column must be disabled.
		/// </summary>
		public bool MustBeDisabled_JobId {

			get {

				return(this.mustBeDisabled_JobId);
			}

			set {

				this.mustBeDisabled_JobId = value;
			}
		}

		/// <summary>
		/// Allows you to pre-select a value for the 'JobId' column.
		/// </summary>
		public void SetValue_JobId(System.Data.SqlTypes.SqlInt32 value) {

			this.hasBeenPopulated_JobId = true;
			this.internal_JobId = value;
		}

		/// <summary>
		/// Allows you to reset the pre-selected value for the 'JobId' column.
		/// </summary>
		public void ResetValue_JobId() {

			this.hasBeenPopulated_JobId = false;
			this.internal_JobId = System.Data.SqlTypes.SqlInt32.Null;
		}

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
		/// Gets or sets the fact that the controls responsible of the 'JobPartTypeId' column must be hidden.
		/// </summary>
		public bool MustBeHidden_JobPartTypeId {

			get {

				return(this.mustBeHidden_JobPartTypeId);
			}

			set {

				this.mustBeHidden_JobPartTypeId = value;
			}
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'JobPartTypeId' column must be disabled.
		/// </summary>
		public bool MustBeDisabled_JobPartTypeId {

			get {

				return(this.mustBeDisabled_JobPartTypeId);
			}

			set {

				this.mustBeDisabled_JobPartTypeId = value;
			}
		}

		/// <summary>
		/// Allows you to pre-select a value for the 'JobPartTypeId' column.
		/// </summary>
		public void SetValue_JobPartTypeId(System.Data.SqlTypes.SqlInt32 value) {

			this.hasBeenPopulated_JobPartTypeId = true;
			this.internal_JobPartTypeId = value;
		}

		/// <summary>
		/// Allows you to reset the pre-selected value for the 'JobPartTypeId' column.
		/// </summary>
		public void ResetValue_JobPartTypeId() {

			this.hasBeenPopulated_JobPartTypeId = false;
			this.internal_JobPartTypeId = System.Data.SqlTypes.SqlInt32.Null;
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'Units' column must be hidden.
		/// </summary>
		public bool MustBeHidden_Units {

			get {

				return(this.mustBeHidden_Units);
			}

			set {

				this.mustBeHidden_Units = value;
			}
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'Units' column must be disabled.
		/// </summary>
		public bool MustBeDisabled_Units {

			get {

				return(this.mustBeDisabled_Units);
			}

			set {

				this.mustBeDisabled_Units = value;
			}
		}

		/// <summary>
		/// Allows you to pre-select a value for the 'Units' column.
		/// </summary>
		public void SetValue_Units(System.Data.SqlTypes.SqlDecimal value) {

			this.hasBeenPopulated_Units = true;
			this.internal_Units = value;
		}

		/// <summary>
		/// Allows you to reset the pre-selected value for the 'Units' column.
		/// </summary>
		public void ResetValue_Units() {

			this.hasBeenPopulated_Units = false;
			this.internal_Units = System.Data.SqlTypes.SqlDecimal.Null;
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'PricePerUnit' column must be hidden.
		/// </summary>
		public bool MustBeHidden_PricePerUnit {

			get {

				return(this.mustBeHidden_PricePerUnit);
			}

			set {

				this.mustBeHidden_PricePerUnit = value;
			}
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'PricePerUnit' column must be disabled.
		/// </summary>
		public bool MustBeDisabled_PricePerUnit {

			get {

				return(this.mustBeDisabled_PricePerUnit);
			}

			set {

				this.mustBeDisabled_PricePerUnit = value;
			}
		}

		/// <summary>
		/// Allows you to pre-select a value for the 'PricePerUnit' column.
		/// </summary>
		public void SetValue_PricePerUnit(System.Data.SqlTypes.SqlMoney value) {

			this.hasBeenPopulated_PricePerUnit = true;
			this.internal_PricePerUnit = value;
		}

		/// <summary>
		/// Allows you to reset the pre-selected value for the 'PricePerUnit' column.
		/// </summary>
		public void ResetValue_PricePerUnit() {

			this.hasBeenPopulated_PricePerUnit = false;
			this.internal_PricePerUnit = System.Data.SqlTypes.SqlMoney.Null;
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'TotalPrice' column must be hidden.
		/// </summary>
		public bool MustBeHidden_TotalPrice {

			get {

				return(this.mustBeHidden_TotalPrice);
			}

			set {

				this.mustBeHidden_TotalPrice = value;
			}
		}

		/// <summary>
		/// Gets or sets the fact that the controls responsible of the 'TotalPrice' column must be disabled.
		/// </summary>
		public bool MustBeDisabled_TotalPrice {

			get {

				return(this.mustBeDisabled_TotalPrice);
			}

			set {

				this.mustBeDisabled_TotalPrice = value;
			}
		}

		/// <summary>
		/// Allows you to pre-select a value for the 'TotalPrice' column.
		/// </summary>
		public void SetValue_TotalPrice(System.Data.SqlTypes.SqlMoney value) {

			this.hasBeenPopulated_TotalPrice = true;
			this.internal_TotalPrice = value;
		}

		/// <summary>
		/// Allows you to reset the pre-selected value for the 'TotalPrice' column.
		/// </summary>
		public void ResetValue_TotalPrice() {

			this.hasBeenPopulated_TotalPrice = false;
			this.internal_TotalPrice = System.Data.SqlTypes.SqlMoney.Null;
		}


		private void ProcessHiddenAndDisabledControlStatus(bool inAddingProcess) {

			// JobId column
			if (this.mustBeDisabled_JobId) {

				this.Control_JobId.Enabled = false;
				this.Button_JobId.Enabled = false;
			}

			if (this.mustBeHidden_JobId) {

				this.Control_JobId.Visible = false;
				this.Button_JobId.Visible = false;
			}

			if (inAddingProcess) {

				if (this.hasBeenPopulated_JobId) {

					if (this.internal_JobId.IsNull) {

						this.Control_JobId.SelectedIndex = -1;
					}
					else {

						this.Control_JobId.SelectedValue = this.internal_JobId.Value;
					}
				}
			}

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

			// JobPartTypeId column
			if (this.mustBeDisabled_JobPartTypeId) {

				this.Control_JobPartTypeId.Enabled = false;
				this.Button_JobPartTypeId.Enabled = false;
			}

			if (this.mustBeHidden_JobPartTypeId) {

				this.Control_JobPartTypeId.Visible = false;
				this.Button_JobPartTypeId.Visible = false;
			}

			if (inAddingProcess) {

				if (this.hasBeenPopulated_JobPartTypeId) {

					if (this.internal_JobPartTypeId.IsNull) {

						this.Control_JobPartTypeId.SelectedIndex = -1;
					}
					else {

						this.Control_JobPartTypeId.SelectedValue = this.internal_JobPartTypeId.Value;
					}
				}
			}

			// Units column
			this.Control_Units.Enabled = !this.mustBeDisabled_Units;
			this.Control_Units.Enabled = !this.mustBeHidden_Units;

			if (inAddingProcess) {

				if (this.hasBeenPopulated_Units) {

					if (this.internal_Units.IsNull) {

						this.Control_Units.Text = String.Empty;
					}
					else {

						this.Control_Units.Text = this.internal_Units.Value.ToString();
					}
				}
			}

			// PricePerUnit column
			this.Control_PricePerUnit.Enabled = !this.mustBeDisabled_PricePerUnit;
			this.Control_PricePerUnit.Enabled = !this.mustBeHidden_PricePerUnit;

			if (inAddingProcess) {

				if (this.hasBeenPopulated_PricePerUnit) {

					if (this.internal_PricePerUnit.IsNull) {

						this.Control_PricePerUnit.Text = String.Empty;
					}
					else {

						this.Control_PricePerUnit.Text = this.internal_PricePerUnit.Value.ToString();
					}
				}
			}

			// TotalPrice column
			this.Control_TotalPrice.Enabled = !this.mustBeDisabled_TotalPrice;
			this.Control_TotalPrice.Enabled = !this.mustBeHidden_TotalPrice;

			if (inAddingProcess) {

				if (this.hasBeenPopulated_TotalPrice) {

					if (this.internal_TotalPrice.IsNull) {

						this.Control_TotalPrice.Text = String.Empty;
					}
					else {

						this.Control_TotalPrice.Text = this.internal_TotalPrice.Value.ToString();
					}
				}
			}
		}

		/// <summary>
		/// Create a new instance of the Bob.Windows.Forms.WinForm_JobPart class.
		/// </summary>
		public WinForm_JobPart() {

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

			this.labJobId = new System.Windows.Forms.Label();
			this.Control_JobId = new Bob.Windows.ComboBoxes.WinComboBox_Job();
			this.Button_JobId = new System.Windows.Forms.Button();
			this.labDescription = new System.Windows.Forms.Label();
			this.Control_Description = new WinForm_DatabaseTextBox();
			this.labJobPartTypeId = new System.Windows.Forms.Label();
			this.Control_JobPartTypeId = new Bob.Windows.ComboBoxes.WinComboBox_JobPartType();
			this.Button_JobPartTypeId = new System.Windows.Forms.Button();
			this.labUnits = new System.Windows.Forms.Label();
			this.Control_Units = new WinForm_DatabaseTextBox();
			this.labPricePerUnit = new System.Windows.Forms.Label();
			this.Control_PricePerUnit = new WinForm_DatabaseTextBox();
			this.labTotalPrice = new System.Windows.Forms.Label();
			this.Control_TotalPrice = new WinForm_DatabaseTextBox();
			this.cmdOK = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// labJobId
			// 
			this.labJobId.AutoSize = true;
			this.labJobId.Location = new System.Drawing.Point(24, 24);
			this.labJobId.Name = "labJobId";
			this.labJobId.Size = new System.Drawing.Size(126, 13);
			this.labJobId.TabIndex = 0;
			this.labJobId.Text = "JobId (update this label in the \"Olymars/ColumnLabel\" extended property of the \"JobId\" column):";
			// 
			// Control_JobId
			// 
			this.Control_JobId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Control_JobId.DropDownWidth = 392;
			this.Control_JobId.Location = new System.Drawing.Point(24, 40);
			this.Control_JobId.Name = "Control_JobId";
			this.Control_JobId.Size = new System.Drawing.Size(352, 21);
			this.Control_JobId.TabIndex = 1;
			this.Control_JobId.SelectedIndexChanged += new System.EventHandler(this.Control_JobId_SelectedIndexChanged);
			// 
			// Button_JobId
			// 
			this.Button_JobId.Location = new System.Drawing.Point(392, 40);
			this.Button_JobId.Name = "Button_JobId";
			this.Button_JobId.TabIndex = 2;
			this.Button_JobId.Text = "Add";
			this.Button_JobId.Click += new System.EventHandler(this.Button_JobId_Click);
			// 
			// labDescription
			// 
			this.labDescription.AutoSize = true;
			this.labDescription.Location = new System.Drawing.Point(24, 80);
			this.labDescription.Name = "labDescription";
			this.labDescription.Size = new System.Drawing.Size(110, 13);
			this.labDescription.TabIndex = 3;
			this.labDescription.Text = "Description (update this label in the \"Olymars/ColumnLabel\" extended property of the \"Description\" column):";
			// 
			// Control_Description
			// 
			this.Control_Description.Location = new System.Drawing.Point(24, 96);
			this.Control_Description.Name = "Control_Description";
			this.Control_Description.NullIsAllow = false;
			this.Control_Description.InvalidBackColor = System.Drawing.Color.Red;
			this.Control_Description.Size = new System.Drawing.Size(440, 20);
			this.Control_Description.TabIndex = 4;
			this.Control_Description.DataSize = 1000;
			this.Control_Description.MaxLength = 1000;
			this.Control_Description.TextChanged += new System.EventHandler(this.Control_Description_TextChanged);
			this.Control_Description.DBType = SupportedDatabaseTypes.DBType_varchar;
			// 
			// labJobPartTypeId
			// 
			this.labJobPartTypeId.AutoSize = true;
			this.labJobPartTypeId.Location = new System.Drawing.Point(24, 136);
			this.labJobPartTypeId.Name = "labJobPartTypeId";
			this.labJobPartTypeId.Size = new System.Drawing.Size(126, 13);
			this.labJobPartTypeId.TabIndex = 5;
			this.labJobPartTypeId.Text = "JobPartTypeId (update this label in the \"Olymars/ColumnLabel\" extended property of the \"JobPartTypeId\" column):";
			// 
			// Control_JobPartTypeId
			// 
			this.Control_JobPartTypeId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Control_JobPartTypeId.DropDownWidth = 392;
			this.Control_JobPartTypeId.Location = new System.Drawing.Point(24, 152);
			this.Control_JobPartTypeId.Name = "Control_JobPartTypeId";
			this.Control_JobPartTypeId.Size = new System.Drawing.Size(352, 21);
			this.Control_JobPartTypeId.TabIndex = 6;
			this.Control_JobPartTypeId.SelectedIndexChanged += new System.EventHandler(this.Control_JobPartTypeId_SelectedIndexChanged);
			// 
			// Button_JobPartTypeId
			// 
			this.Button_JobPartTypeId.Location = new System.Drawing.Point(392, 152);
			this.Button_JobPartTypeId.Name = "Button_JobPartTypeId";
			this.Button_JobPartTypeId.TabIndex = 7;
			this.Button_JobPartTypeId.Text = "Add";
			this.Button_JobPartTypeId.Click += new System.EventHandler(this.Button_JobPartTypeId_Click);
			// 
			// labUnits
			// 
			this.labUnits.AutoSize = true;
			this.labUnits.Location = new System.Drawing.Point(24, 192);
			this.labUnits.Name = "labUnits";
			this.labUnits.Size = new System.Drawing.Size(110, 13);
			this.labUnits.TabIndex = 8;
			this.labUnits.Text = "Units (update this label in the \"Olymars/ColumnLabel\" extended property of the \"Units\" column):";
			// 
			// Control_Units
			// 
			this.Control_Units.Location = new System.Drawing.Point(24, 208);
			this.Control_Units.Name = "Control_Units";
			this.Control_Units.NullIsAllow = true;
			this.Control_Units.InvalidBackColor = System.Drawing.Color.Red;
			this.Control_Units.Size = new System.Drawing.Size(440, 20);
			this.Control_Units.TabIndex = 9;
			this.Control_Units.DataSize = 20;
			this.Control_Units.TextChanged += new System.EventHandler(this.Control_Units_TextChanged);
			this.Control_Units.DBType = SupportedDatabaseTypes.DBType_decimal;
			// 
			// labPricePerUnit
			// 
			this.labPricePerUnit.AutoSize = true;
			this.labPricePerUnit.Location = new System.Drawing.Point(24, 248);
			this.labPricePerUnit.Name = "labPricePerUnit";
			this.labPricePerUnit.Size = new System.Drawing.Size(110, 13);
			this.labPricePerUnit.TabIndex = 10;
			this.labPricePerUnit.Text = "PricePerUnit (update this label in the \"Olymars/ColumnLabel\" extended property of the \"PricePerUnit\" column):";
			// 
			// Control_PricePerUnit
			// 
			this.Control_PricePerUnit.Location = new System.Drawing.Point(24, 264);
			this.Control_PricePerUnit.Name = "Control_PricePerUnit";
			this.Control_PricePerUnit.NullIsAllow = true;
			this.Control_PricePerUnit.InvalidBackColor = System.Drawing.Color.Red;
			this.Control_PricePerUnit.Size = new System.Drawing.Size(440, 20);
			this.Control_PricePerUnit.TabIndex = 11;
			this.Control_PricePerUnit.DataSize = 21;
			this.Control_PricePerUnit.TextChanged += new System.EventHandler(this.Control_PricePerUnit_TextChanged);
			this.Control_PricePerUnit.DBType = SupportedDatabaseTypes.DBType_money;
			// 
			// labTotalPrice
			// 
			this.labTotalPrice.AutoSize = true;
			this.labTotalPrice.Location = new System.Drawing.Point(24, 304);
			this.labTotalPrice.Name = "labTotalPrice";
			this.labTotalPrice.Size = new System.Drawing.Size(110, 13);
			this.labTotalPrice.TabIndex = 12;
			this.labTotalPrice.Text = "TotalPrice (update this label in the \"Olymars/ColumnLabel\" extended property of the \"TotalPrice\" column):";
			// 
			// Control_TotalPrice
			// 
			this.Control_TotalPrice.Location = new System.Drawing.Point(24, 320);
			this.Control_TotalPrice.Name = "Control_TotalPrice";
			this.Control_TotalPrice.NullIsAllow = false;
			this.Control_TotalPrice.InvalidBackColor = System.Drawing.Color.Red;
			this.Control_TotalPrice.Size = new System.Drawing.Size(440, 20);
			this.Control_TotalPrice.TabIndex = 13;
			this.Control_TotalPrice.DataSize = 21;
			this.Control_TotalPrice.TextChanged += new System.EventHandler(this.Control_TotalPrice_TextChanged);
			this.Control_TotalPrice.DBType = SupportedDatabaseTypes.DBType_money;
			// 
			// cmdOK
			// 
			this.cmdOK.Location = new System.Drawing.Point(304, 360);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.TabIndex = 14;
			this.cmdOK.Text = "OK";
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(392, 360);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.TabIndex = 14;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// WinForm_JobPart
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 13);
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(506, 400);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
						this.labJobId,
						this.Control_JobId,
						this.Button_JobId,
						this.labDescription,
						this.Control_Description,
						this.labJobPartTypeId,
						this.Control_JobPartTypeId,
						this.Button_JobPartTypeId,
						this.labUnits,
						this.Control_Units,
						this.labPricePerUnit,
						this.Control_PricePerUnit,
						this.labTotalPrice,
						this.Control_TotalPrice,
						this.cmdOK,
						this.cmdCancel
						});
			this.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "WinForm_JobPart";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "JobPart management";
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

			Control_JobId.Enabled = true;
			if (Control_JobId.Items.Count != 0) {

				Control_JobId.SelectedIndex = 0;
			}

			Control_Description.Text = String.Empty;

			Control_JobPartTypeId.Enabled = true;
			if (Control_JobPartTypeId.Items.Count != 0) {

				Control_JobPartTypeId.SelectedIndex = 0;
			}

			Control_Units.Text = String.Empty;

			Control_PricePerUnit.Text = String.Empty;

			Control_TotalPrice.Text = String.Empty;


			this.IgnoreChanges = false;
		}

		/// <summary>
		/// Allows you to add a new record in the JobPart table.
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
			this.Control_Units.HandleTextChanged(null, null);
			this.Control_PricePerUnit.HandleTextChanged(null, null);
			this.Control_TotalPrice.HandleTextChanged(null, null);

			Check_cmdOK_Button(null);

			ProcessHiddenAndDisabledControlStatus(true);

			this.ShowDialog(caller);
		}

		/// <summary>
		/// Allows you to add a new record in the JobPart table.
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
			this.Control_Units.HandleTextChanged(null, null);
			this.Control_PricePerUnit.HandleTextChanged(null, null);
			this.Control_TotalPrice.HandleTextChanged(null, null);

			Check_cmdOK_Button(null);

			ProcessHiddenAndDisabledControlStatus(true);

			this.ShowDialog(caller);
		}

		/// <summary>
		/// Allows you to update an existing record from the JobPart table.
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
		/// Allows you to update an existing record from the JobPart table.
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

				Status = (Status && Control_JobId.SelectedIndex != -1);
				Status = (Status && Control_Description.IsValid);
				Status = (Status && Control_JobPartTypeId.SelectedIndex != -1);
				Status = (Status && Control_Units.IsValid);
				Status = (Status && Control_PricePerUnit.IsValid);
				Status = (Status && Control_TotalPrice.IsValid);
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

			Refresh_JobId();
			Refresh_JobPartTypeId();
		}

		private void Control_JobId_SelectedIndexChanged(object sender, System.EventArgs e) {

			if (!this.IgnoreChanges) Check_cmdOK_Button(Control_JobId);
		}


		private void Refresh_JobId() {

			this.IgnoreChanges = true;

			switch (this.lastKnownConnectionType) {
				case Bob.DataClasses.ConnectionType.ConnectionString:
					Control_JobId.Initialize(this.connectionString, System.Data.SqlTypes.SqlInt32.Null);
					break;

				case Bob.DataClasses.ConnectionType.SqlConnection:
					Control_JobId.Initialize(this.sqlConnection, System.Data.SqlTypes.SqlInt32.Null);
					break;
			}

			try {

				Control_JobId.RefreshData(System.Data.SqlTypes.SqlInt32.Null);
			}
			catch (Bob.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinComboBox_Job' class. Exception message is: {0}", customException.Parameter.SqlException.Message), customException);
				}
				else if (customException.Parameter.OtherException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinComboBox_Job' class. Exception message is: {0}", customException.Parameter.OtherException.Message), customException);
				}
				else {

					throw;
				}
			}

			if (Control_JobId.Items.Count > 0) {

				Control_JobId.SelectedIndex = 0;
			}

			this.IgnoreChanges = false;
		}

		private void Control_JobPartTypeId_SelectedIndexChanged(object sender, System.EventArgs e) {

			if (!this.IgnoreChanges) Check_cmdOK_Button(Control_JobPartTypeId);
		}


		private void Refresh_JobPartTypeId() {

			this.IgnoreChanges = true;

			switch (this.lastKnownConnectionType) {
				case Bob.DataClasses.ConnectionType.ConnectionString:
					Control_JobPartTypeId.Initialize(this.connectionString);
					break;

				case Bob.DataClasses.ConnectionType.SqlConnection:
					Control_JobPartTypeId.Initialize(this.sqlConnection);
					break;
			}

			try {

				Control_JobPartTypeId.RefreshData(System.Data.SqlTypes.SqlInt32.Null);
			}
			catch (Bob.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinComboBox_JobPartType' class. Exception message is: {0}", customException.Parameter.SqlException.Message), customException);
				}
				else if (customException.Parameter.OtherException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WinComboBox_JobPartType' class. Exception message is: {0}", customException.Parameter.OtherException.Message), customException);
				}
				else {

					throw;
				}
			}

			if (Control_JobPartTypeId.Items.Count > 0) {

				Control_JobPartTypeId.SelectedIndex = 0;
			}

			this.IgnoreChanges = false;
		}

		private bool RefreshCurrentRecord() {

			EmptyControls();

			Abstracts.Abstract_JobPart oAbstract_JobPart = null;
			switch (this.lastKnownConnectionType) {
				case Bob.DataClasses.ConnectionType.ConnectionString:
					oAbstract_JobPart = new Abstracts.Abstract_JobPart(this.connectionString);
					break;

				case Bob.DataClasses.ConnectionType.SqlConnection:
					oAbstract_JobPart = new Abstracts.Abstract_JobPart(this.sqlConnection);
					break;
			}

			if (oAbstract_JobPart.Refresh(this.currentID)) {

				this.IgnoreChanges = true;

				if (!oAbstract_JobPart.Col_JobId.IsNull) {

					Control_JobId.SelectedValue = oAbstract_JobPart.Col_JobId.Value;
				}

				if (!oAbstract_JobPart.Col_Description.IsNull) {

					Control_Description.Text = oAbstract_JobPart.Col_Description.Value.ToString();
				}


				if (!oAbstract_JobPart.Col_JobPartTypeId.IsNull) {

					Control_JobPartTypeId.SelectedValue = oAbstract_JobPart.Col_JobPartTypeId.Value;
				}

				if (!oAbstract_JobPart.Col_Units.IsNull) {

					Control_Units.Text = oAbstract_JobPart.Col_Units.Value.ToString();
				}


				if (!oAbstract_JobPart.Col_PricePerUnit.IsNull) {

					Control_PricePerUnit.Text = oAbstract_JobPart.Col_PricePerUnit.Value.ToString();
				}


				if (!oAbstract_JobPart.Col_TotalPrice.IsNull) {

					Control_TotalPrice.Text = oAbstract_JobPart.Col_TotalPrice.Value.ToString();
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

		private void Control_Units_TextChanged(object sender, System.EventArgs e) {

			if (!this.IgnoreChanges) {

				Check_cmdOK_Button(Control_Units);
			}
		}

		private void Control_PricePerUnit_TextChanged(object sender, System.EventArgs e) {

			if (!this.IgnoreChanges) {

				Check_cmdOK_Button(Control_PricePerUnit);
			}
		}

		private void Control_TotalPrice_TextChanged(object sender, System.EventArgs e) {

			if (!this.IgnoreChanges) {

				Check_cmdOK_Button(Control_TotalPrice);
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

			Params.spI_JobPart Param = new Params.spI_JobPart(false);

			switch (this.lastKnownConnectionType) {
				case Bob.DataClasses.ConnectionType.ConnectionString:
					Param.SetUpConnection(this.connectionString);
					break;

				case Bob.DataClasses.ConnectionType.SqlConnection:
					Param.SetUpConnection(this.sqlConnection);
					break;
			}

			if (Control_JobId.SelectedIndex != -1) {

				Param.Param_JobId = (System.Int32)Control_JobId.SelectedValue;
			}

			if (Control_Description.Text.Trim() != String.Empty) {

				Param.Param_Description = (System.Data.SqlTypes.SqlString)Control_Description.GetSqlTypesValue;
			}

			if (Control_JobPartTypeId.SelectedIndex != -1) {

				Param.Param_JobPartTypeId = (System.Int32)Control_JobPartTypeId.SelectedValue;
			}

			if (Control_Units.Text.Trim() != String.Empty) {

				Param.Param_Units = (System.Data.SqlTypes.SqlDecimal)Control_Units.GetSqlTypesValue;
			}

			if (Control_PricePerUnit.Text.Trim() != String.Empty) {

				Param.Param_PricePerUnit = (System.Data.SqlTypes.SqlMoney)Control_PricePerUnit.GetSqlTypesValue;
			}

			if (Control_TotalPrice.Text.Trim() != String.Empty) {

				Param.Param_TotalPrice = (System.Data.SqlTypes.SqlMoney)Control_TotalPrice.GetSqlTypesValue;
			}

			SPs.spI_JobPart SP = new SPs.spI_JobPart(false);

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

					throw new Bob.DataClasses.CustomException(Param, "Bob.Windows.Forms.WinForm_JobPart", "AddRecordInDatabase");
				}
			}
		}

		private void UpdateRecordInDatabase() {

			Params.spU_JobPart Param = new Params.spU_JobPart(false);
			
			switch (this.lastKnownConnectionType) {
				case Bob.DataClasses.ConnectionType.ConnectionString:
					Param.SetUpConnection(this.connectionString);
					break;

				case Bob.DataClasses.ConnectionType.SqlConnection:
					Param.SetUpConnection(this.sqlConnection);
					break;
			}

			Param.Param_JobPartId = this.currentID;

			if (Control_JobId.SelectedIndex != -1) {

				Param.Param_JobId = (System.Int32)Control_JobId.SelectedValue;
			}

			if (Control_Description.Text.Trim() != String.Empty) {

				Param.Param_Description = (System.Data.SqlTypes.SqlString)Control_Description.GetSqlTypesValue;
			}

			if (Control_JobPartTypeId.SelectedIndex != -1) {

				Param.Param_JobPartTypeId = (System.Int32)Control_JobPartTypeId.SelectedValue;
			}

			if (Control_Units.Text.Trim() != String.Empty) {

				Param.Param_Units = (System.Data.SqlTypes.SqlDecimal)Control_Units.GetSqlTypesValue;
			}

			if (Control_PricePerUnit.Text.Trim() != String.Empty) {

				Param.Param_PricePerUnit = (System.Data.SqlTypes.SqlMoney)Control_PricePerUnit.GetSqlTypesValue;
			}

			if (Control_TotalPrice.Text.Trim() != String.Empty) {

				Param.Param_TotalPrice = (System.Data.SqlTypes.SqlMoney)Control_TotalPrice.GetSqlTypesValue;
			}

			SPs.spU_JobPart SP = new SPs.spU_JobPart(false);

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

					throw new Bob.DataClasses.CustomException(Param, "Bob.Windows.Forms.WinForm_JobPart", "UpdateRecordInDatabase");
				}
			}
		}

		private void Button_JobId_Click(object sender, System.EventArgs e) {

			AddNew_Job1(Control_JobId);
		}

		private void Button_JobPartTypeId_Click(object sender, System.EventArgs e) {

			AddNew_JobPartType2(Control_JobPartTypeId);
		}

		private void AddNew_Job1(System.Windows.Forms.ComboBox CurrentCombo) {

			System.Data.SqlTypes.SqlInt32 NewPrimaryKey;

			Bob.Windows.Forms.WinForm_Job MyForm = new Bob.Windows.Forms.WinForm_Job();
			MyForm.AddNewRecord(this, "Add a new Job", ConnectionString);

			if (MyForm.DialogResult == DialogResult.OK) {

				if (!MyForm.ErrorHasOccured) {

					NewPrimaryKey = ((Bob.DataClasses.Parameters.spI_Job)MyForm.Parameter).Param_JobId;
					Refresh_JobId();
					CurrentCombo.SelectedValue = NewPrimaryKey.Value;
				}

				MyForm.Dispose();
				CurrentCombo.Select();
			}
		}

		private void AddNew_JobPartType2(System.Windows.Forms.ComboBox CurrentCombo) {

			System.Data.SqlTypes.SqlInt32 NewPrimaryKey;

			Bob.Windows.Forms.WinForm_JobPartType MyForm = new Bob.Windows.Forms.WinForm_JobPartType();
			MyForm.AddNewRecord(this, "Add a new JobPartType", ConnectionString);

			if (MyForm.DialogResult == DialogResult.OK) {

				if (!MyForm.ErrorHasOccured) {

					NewPrimaryKey = ((Bob.DataClasses.Parameters.spI_JobPartType)MyForm.Parameter).Param_JobPartTypeId;
					Refresh_JobPartTypeId();
					CurrentCombo.SelectedValue = NewPrimaryKey.Value;
				}

				MyForm.Dispose();
				CurrentCombo.Select();
			}
		}

	}
}
