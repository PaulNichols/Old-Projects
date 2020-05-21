using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Configuration;

using HBOS.FS.AMP.UPD.Types.Users;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.Security;

using HBOS.FS.Support.Tex;
using HBOS.FS.AMP.ExceptionManagement;
using HBOS.FS.Common.ExceptionManagement;


namespace HBOS.FS.AMP.UPD.WinUI.UserControls
{
	/// <summary>
	/// UsersControl - allows maintenance of Users
	/// </summary>
	public class UsersControl : System.Windows.Forms.UserControl
	{
		#region Private Variables

		private enum FormState { DisplayUser, EditUser, NewUser}

		private FormState m_currentFormState;
		private string m_companyID;
		private string m_connectionString;
		private UserCollection m_users = null ;
        private UserCollection m_usersClone = null; 
		private bool m_formProcessing = false;

		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Button editUserButton;
		private System.Windows.Forms.Button newUserButton;
		private System.Windows.Forms.Label loginNameLabel;
		private System.Windows.Forms.Label lastUpdatedByLabel;
		private System.Windows.Forms.Label lastUpdateDateLabel;
		private System.Windows.Forms.TextBox lastUpdatedByTextBox;
		private System.Windows.Forms.TextBox lastUpdatedDateTextBox;
		private System.Windows.Forms.GroupBox permissionsGroupBox;
		private System.Windows.Forms.CheckBox maintainFundGroupsCheckBox;
		private System.Windows.Forms.ToolTip controlToolTips;
		private System.Windows.Forms.CheckBox importHI3PricesCheckBox;
		private System.Windows.Forms.CheckBox importFundWeightingsCheckBox;
		private System.Windows.Forms.Label importLabel;
		private System.Windows.Forms.Label pricesLabel;
		private System.Windows.Forms.Label maintenanceLabel;
		private System.Windows.Forms.CheckBox importExchangeRatesCheckBox;
		private System.Windows.Forms.CheckBox prices2ndLevelAuthoriseCheckBox;
		private System.Windows.Forms.CheckBox pricesSecondLevelUndoCheckBox;
		private System.Windows.Forms.CheckBox pricesReleaseCheckBox;
		private System.Windows.Forms.CheckBox pricesUndoReleaseCheckBox;
		private System.Windows.Forms.CheckBox exportOEICSCheckBox;
		private System.Windows.Forms.CheckBox pricesDistributeCheckBox;
		private System.Windows.Forms.CheckBox pricesReDistributeCheckBox;
		private System.Windows.Forms.CheckBox maintainFundMappingsCheckBox;
		private System.Windows.Forms.CheckBox maintainSubscriptionsCheckBox;
		private System.Windows.Forms.CheckBox maintainMethodsCheckBox;
		private System.Windows.Forms.CheckBox maintainUserAccessCheckBox;
		private System.Windows.Forms.CheckBox maintainTolerancesCheckBox;
		private System.Windows.Forms.CheckBox maintainCalculationIndicesCheckBox;
		private System.Windows.Forms.CheckBox maintainCaclulationFactorsCheckBox;
		private System.Windows.Forms.Label distributionLabel;
		private System.Windows.Forms.ComboBox usersComboBox;
		private System.Windows.Forms.TextBox userNameTextBox;
		private System.Windows.Forms.CheckBox importMarketIndicesCheckBox;
		private System.Windows.Forms.Label inactiveAccountLabel;
		private System.Windows.Forms.CheckBox deactivateAccountCheckBox;
		private System.Windows.Forms.Label loginIDLabel;
		private System.Windows.Forms.TextBox loginIDTextBox;
		private System.Windows.Forms.CheckBox maintainSubscribersCheckBox;
		private System.Windows.Forms.CheckBox maintainAdministratorCheckBox;
        private System.Windows.Forms.Button cancelButton;
		private System.ComponentModel.IContainer components;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		public UsersControl()
		{
			T.E();

			try
			{
				// This call is required by the Windows.Forms Form Designer.
				InitializeComponent();

				this.initialiseData();

				float scaleWidth = Form.GetAutoScaleSize(this.Font).Width;
				float adjustment = scaleWidth / 5.32F;

				// Adjust for large fonts
				usersComboBox.Left = userNameTextBox.Left = loginNameLabel.Left + loginNameLabel.Width + 6;
				usersComboBox.Width = userNameTextBox.Width = (int)((float)usersComboBox.Width * adjustment);

				lastUpdatedByLabel.Left = lastUpdateDateLabel.Left = usersComboBox.Left + usersComboBox.Width + 20;

				lastUpdatedByTextBox.Left = lastUpdatedDateTextBox.Left = lastUpdatedByLabel.Left + lastUpdatedByLabel.Width + 6;
				lastUpdatedByTextBox.Width = lastUpdatedDateTextBox.Width = (int)((float)lastUpdatedByTextBox.Width*adjustment);

				importExchangeRatesCheckBox.Width = importMarketIndicesCheckBox.Width =
					importHI3PricesCheckBox.Width = importFundWeightingsCheckBox.Width = 
					exportOEICSCheckBox.Width = (int)((float)importExchangeRatesCheckBox.Width * adjustment);

				pricesLabel.Left = prices2ndLevelAuthoriseCheckBox.Left = pricesSecondLevelUndoCheckBox.Left = 
					pricesReleaseCheckBox.Left = pricesUndoReleaseCheckBox.Left = pricesDistributeCheckBox.Left =
					pricesReDistributeCheckBox.Left  = 
					importExchangeRatesCheckBox.Left + importExchangeRatesCheckBox.Width + 20;

				prices2ndLevelAuthoriseCheckBox.Width = pricesSecondLevelUndoCheckBox.Width = 
					pricesReleaseCheckBox.Width = pricesUndoReleaseCheckBox.Width = pricesDistributeCheckBox.Width =
					pricesReDistributeCheckBox.Width = (int)((float)pricesSecondLevelUndoCheckBox.Width*adjustment);

				maintenanceLabel.Left = maintainFundGroupsCheckBox.Left = maintainFundMappingsCheckBox.Left =
					maintainTolerancesCheckBox.Left = maintainCalculationIndicesCheckBox.Left = 
					maintainCaclulationFactorsCheckBox.Left = maintainAdministratorCheckBox.Left =
					maintainUserAccessCheckBox.Left = 
					pricesSecondLevelUndoCheckBox.Left + pricesSecondLevelUndoCheckBox.Width + 20;

				maintainFundGroupsCheckBox.Width = maintainFundMappingsCheckBox.Width =
					maintainTolerancesCheckBox.Width = maintainCalculationIndicesCheckBox.Width = 
					maintainCaclulationFactorsCheckBox.Width = maintainAdministratorCheckBox.Width =
					maintainUserAccessCheckBox.Width = (int)((float)maintainAdministratorCheckBox.Width * adjustment);

				distributionLabel.Left = maintainSubscriptionsCheckBox.Left = maintainMethodsCheckBox.Left =
					maintainSubscribersCheckBox.Left = 
					maintainAdministratorCheckBox.Left + maintainAdministratorCheckBox.Width + 20;

				maintainSubscriptionsCheckBox.Width = maintainMethodsCheckBox.Width =
					maintainSubscribersCheckBox.Width = (int)((float)maintainSubscriptionsCheckBox.Width * adjustment);
			}
			finally
			{
				T.X();
			}
		}

		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.saveButton = new System.Windows.Forms.Button();
			this.usersComboBox = new System.Windows.Forms.ComboBox();
			this.editUserButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.newUserButton = new System.Windows.Forms.Button();
			this.loginNameLabel = new System.Windows.Forms.Label();
			this.lastUpdatedByLabel = new System.Windows.Forms.Label();
			this.lastUpdateDateLabel = new System.Windows.Forms.Label();
			this.userNameTextBox = new System.Windows.Forms.TextBox();
			this.lastUpdatedByTextBox = new System.Windows.Forms.TextBox();
			this.lastUpdatedDateTextBox = new System.Windows.Forms.TextBox();
			this.inactiveAccountLabel = new System.Windows.Forms.Label();
			this.permissionsGroupBox = new System.Windows.Forms.GroupBox();
			this.maintainSubscribersCheckBox = new System.Windows.Forms.CheckBox();
			this.maintainAdministratorCheckBox = new System.Windows.Forms.CheckBox();
			this.distributionLabel = new System.Windows.Forms.Label();
			this.maintainCaclulationFactorsCheckBox = new System.Windows.Forms.CheckBox();
			this.maintainCalculationIndicesCheckBox = new System.Windows.Forms.CheckBox();
			this.maintainTolerancesCheckBox = new System.Windows.Forms.CheckBox();
			this.maintainUserAccessCheckBox = new System.Windows.Forms.CheckBox();
			this.maintainMethodsCheckBox = new System.Windows.Forms.CheckBox();
			this.maintainSubscriptionsCheckBox = new System.Windows.Forms.CheckBox();
			this.maintainFundMappingsCheckBox = new System.Windows.Forms.CheckBox();
			this.pricesReDistributeCheckBox = new System.Windows.Forms.CheckBox();
			this.pricesDistributeCheckBox = new System.Windows.Forms.CheckBox();
			this.exportOEICSCheckBox = new System.Windows.Forms.CheckBox();
			this.pricesUndoReleaseCheckBox = new System.Windows.Forms.CheckBox();
			this.pricesReleaseCheckBox = new System.Windows.Forms.CheckBox();
			this.pricesSecondLevelUndoCheckBox = new System.Windows.Forms.CheckBox();
			this.prices2ndLevelAuthoriseCheckBox = new System.Windows.Forms.CheckBox();
			this.importExchangeRatesCheckBox = new System.Windows.Forms.CheckBox();
			this.maintenanceLabel = new System.Windows.Forms.Label();
			this.pricesLabel = new System.Windows.Forms.Label();
			this.importLabel = new System.Windows.Forms.Label();
			this.importFundWeightingsCheckBox = new System.Windows.Forms.CheckBox();
			this.importHI3PricesCheckBox = new System.Windows.Forms.CheckBox();
			this.importMarketIndicesCheckBox = new System.Windows.Forms.CheckBox();
			this.maintainFundGroupsCheckBox = new System.Windows.Forms.CheckBox();
			this.controlToolTips = new System.Windows.Forms.ToolTip(this.components);
			this.deactivateAccountCheckBox = new System.Windows.Forms.CheckBox();
			this.loginIDTextBox = new System.Windows.Forms.TextBox();
			this.loginIDLabel = new System.Windows.Forms.Label();
			this.permissionsGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// saveButton
			// 
			this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.saveButton.Location = new System.Drawing.Point(400, 376);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(80, 23);
			this.saveButton.TabIndex = 27;
			this.saveButton.Text = "&Save";
			this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
			// 
			// usersComboBox
			// 
			this.usersComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.usersComboBox.Location = new System.Drawing.Point(104, 24);
			this.usersComboBox.Name = "usersComboBox";
			this.usersComboBox.Size = new System.Drawing.Size(256, 21);
			this.usersComboBox.TabIndex = 2;
			this.controlToolTips.SetToolTip(this.usersComboBox, "The users logon ID");
			this.usersComboBox.SelectedIndexChanged += new System.EventHandler(this.usersComboBox_SelectedIndexChanged);
			// 
			// editUserButton
			// 
			this.editUserButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.editUserButton.Location = new System.Drawing.Point(312, 376);
			this.editUserButton.Name = "editUserButton";
			this.editUserButton.Size = new System.Drawing.Size(80, 23);
			this.editUserButton.TabIndex = 26;
			this.editUserButton.Text = "&Edit User";
			this.controlToolTips.SetToolTip(this.editUserButton, "Edit the current user");
			this.editUserButton.Click += new System.EventHandler(this.editUserButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.Location = new System.Drawing.Point(488, 376);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(88, 23);
			this.cancelButton.TabIndex = 28;
			this.cancelButton.Text = "&Cancel";
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// newUserButton
			// 
			this.newUserButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.newUserButton.Location = new System.Drawing.Point(224, 376);
			this.newUserButton.Name = "newUserButton";
			this.newUserButton.Size = new System.Drawing.Size(80, 23);
			this.newUserButton.TabIndex = 25;
			this.newUserButton.Text = "&New User";
			this.controlToolTips.SetToolTip(this.newUserButton, "Add a new user");
			this.newUserButton.Click += new System.EventHandler(this.newUserButton_Click);
			// 
			// loginNameLabel
			// 
			this.loginNameLabel.AutoSize = true;
			this.loginNameLabel.Location = new System.Drawing.Point(16, 58);
			this.loginNameLabel.Name = "loginNameLabel";
			this.loginNameLabel.Size = new System.Drawing.Size(61, 16);
			this.loginNameLabel.TabIndex = 8;
			this.loginNameLabel.Text = "User Name";
			// 
			// lastUpdatedByLabel
			// 
			this.lastUpdatedByLabel.AutoSize = true;
			this.lastUpdatedByLabel.Location = new System.Drawing.Point(376, 26);
			this.lastUpdatedByLabel.Name = "lastUpdatedByLabel";
			this.lastUpdatedByLabel.Size = new System.Drawing.Size(88, 16);
			this.lastUpdatedByLabel.TabIndex = 9;
			this.lastUpdatedByLabel.Text = "Last Updated By";
			// 
			// lastUpdateDateLabel
			// 
			this.lastUpdateDateLabel.AutoSize = true;
			this.lastUpdateDateLabel.Location = new System.Drawing.Point(376, 58);
			this.lastUpdateDateLabel.Name = "lastUpdateDateLabel";
			this.lastUpdateDateLabel.Size = new System.Drawing.Size(93, 16);
			this.lastUpdateDateLabel.TabIndex = 10;
			this.lastUpdateDateLabel.Text = "Date last updated";
			// 
			// userNameTextBox
			// 
			this.userNameTextBox.Location = new System.Drawing.Point(104, 56);
			this.userNameTextBox.Name = "userNameTextBox";
			this.userNameTextBox.Size = new System.Drawing.Size(120, 20);
			this.userNameTextBox.TabIndex = 3;
			this.userNameTextBox.Text = "";
			this.controlToolTips.SetToolTip(this.userNameTextBox, "The users name");
			this.userNameTextBox.TextChanged += new System.EventHandler(this.userNameTextBox_TextChanged);
			// 
			// lastUpdatedByTextBox
			// 
			this.lastUpdatedByTextBox.Enabled = false;
			this.lastUpdatedByTextBox.Location = new System.Drawing.Point(480, 24);
			this.lastUpdatedByTextBox.Name = "lastUpdatedByTextBox";
			this.lastUpdatedByTextBox.Size = new System.Drawing.Size(96, 20);
			this.lastUpdatedByTextBox.TabIndex = 12;
			this.lastUpdatedByTextBox.Text = "";
			this.controlToolTips.SetToolTip(this.lastUpdatedByTextBox, "The user who last updated this user");
			// 
			// lastUpdatedDateTextBox
			// 
			this.lastUpdatedDateTextBox.Enabled = false;
			this.lastUpdatedDateTextBox.Location = new System.Drawing.Point(480, 56);
			this.lastUpdatedDateTextBox.Name = "lastUpdatedDateTextBox";
			this.lastUpdatedDateTextBox.Size = new System.Drawing.Size(96, 20);
			this.lastUpdatedDateTextBox.TabIndex = 13;
			this.lastUpdatedDateTextBox.Text = "";
			this.controlToolTips.SetToolTip(this.lastUpdatedDateTextBox, "The date of the last update to this user");
			// 
			// inactiveAccountLabel
			// 
			this.inactiveAccountLabel.AutoSize = true;
			this.inactiveAccountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.inactiveAccountLabel.Location = new System.Drawing.Point(232, 26);
			this.inactiveAccountLabel.Name = "inactiveAccountLabel";
			this.inactiveAccountLabel.Size = new System.Drawing.Size(126, 16);
			this.inactiveAccountLabel.TabIndex = 14;
			this.inactiveAccountLabel.Text = "This account is inactive";
			this.inactiveAccountLabel.Visible = false;
			// 
			// permissionsGroupBox
			// 
			this.permissionsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.permissionsGroupBox.Controls.Add(this.maintainSubscribersCheckBox);
			this.permissionsGroupBox.Controls.Add(this.maintainAdministratorCheckBox);
			this.permissionsGroupBox.Controls.Add(this.distributionLabel);
			this.permissionsGroupBox.Controls.Add(this.maintainCaclulationFactorsCheckBox);
			this.permissionsGroupBox.Controls.Add(this.maintainCalculationIndicesCheckBox);
			this.permissionsGroupBox.Controls.Add(this.maintainTolerancesCheckBox);
			this.permissionsGroupBox.Controls.Add(this.maintainUserAccessCheckBox);
			this.permissionsGroupBox.Controls.Add(this.maintainMethodsCheckBox);
			this.permissionsGroupBox.Controls.Add(this.maintainSubscriptionsCheckBox);
			this.permissionsGroupBox.Controls.Add(this.maintainFundMappingsCheckBox);
			this.permissionsGroupBox.Controls.Add(this.pricesReDistributeCheckBox);
			this.permissionsGroupBox.Controls.Add(this.pricesDistributeCheckBox);
			this.permissionsGroupBox.Controls.Add(this.exportOEICSCheckBox);
			this.permissionsGroupBox.Controls.Add(this.pricesUndoReleaseCheckBox);
			this.permissionsGroupBox.Controls.Add(this.pricesReleaseCheckBox);
			this.permissionsGroupBox.Controls.Add(this.pricesSecondLevelUndoCheckBox);
			this.permissionsGroupBox.Controls.Add(this.prices2ndLevelAuthoriseCheckBox);
			this.permissionsGroupBox.Controls.Add(this.importExchangeRatesCheckBox);
			this.permissionsGroupBox.Controls.Add(this.maintenanceLabel);
			this.permissionsGroupBox.Controls.Add(this.pricesLabel);
			this.permissionsGroupBox.Controls.Add(this.importLabel);
			this.permissionsGroupBox.Controls.Add(this.importFundWeightingsCheckBox);
			this.permissionsGroupBox.Controls.Add(this.importHI3PricesCheckBox);
			this.permissionsGroupBox.Controls.Add(this.importMarketIndicesCheckBox);
			this.permissionsGroupBox.Controls.Add(this.maintainFundGroupsCheckBox);
			this.permissionsGroupBox.Enabled = false;
			this.permissionsGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.permissionsGroupBox.Location = new System.Drawing.Point(16, 88);
			this.permissionsGroupBox.Name = "permissionsGroupBox";
			this.permissionsGroupBox.Size = new System.Drawing.Size(560, 272);
			this.permissionsGroupBox.TabIndex = 15;
			this.permissionsGroupBox.TabStop = false;
			this.permissionsGroupBox.Text = "Permissions";
			this.permissionsGroupBox.Paint += new System.Windows.Forms.PaintEventHandler(this.permissionsGroupBox_Paint);
			// 
			// maintainSubscribersCheckBox
			// 
			this.maintainSubscribersCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.maintainSubscribersCheckBox.Location = new System.Drawing.Point(424, 112);
			this.maintainSubscribersCheckBox.Name = "maintainSubscribersCheckBox";
			this.maintainSubscribersCheckBox.Size = new System.Drawing.Size(96, 24);
			this.maintainSubscribersCheckBox.TabIndex = 15;
			this.maintainSubscribersCheckBox.Text = "Subscribers";
			this.controlToolTips.SetToolTip(this.maintainSubscribersCheckBox, "User can maintain the distribution subscribers");
			this.maintainSubscribersCheckBox.CheckedChanged += new System.EventHandler(this.maintainSubscribersCheckBox_CheckedChanged);
			// 
			// maintainAdministratorCheckBox
			// 
			this.maintainAdministratorCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.maintainAdministratorCheckBox.Location = new System.Drawing.Point(296, 208);
			this.maintainAdministratorCheckBox.Name = "maintainAdministratorCheckBox";
			this.maintainAdministratorCheckBox.Size = new System.Drawing.Size(96, 24);
			this.maintainAdministratorCheckBox.TabIndex = 23;
			this.maintainAdministratorCheckBox.Text = "Administrator";
			this.controlToolTips.SetToolTip(this.maintainAdministratorCheckBox, "User is an administrator");
			this.maintainAdministratorCheckBox.CheckedChanged += new System.EventHandler(this.maintainAdministratorCheckBox_CheckedChanged);
			// 
			// distributionLabel
			// 
			this.distributionLabel.AutoSize = true;
			this.distributionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.distributionLabel.Location = new System.Drawing.Point(424, 24);
			this.distributionLabel.Name = "distributionLabel";
			this.distributionLabel.Size = new System.Drawing.Size(61, 16);
			this.distributionLabel.TabIndex = 23;
			this.distributionLabel.Text = "Distribution";
			// 
			// maintainCaclulationFactorsCheckBox
			// 
			this.maintainCaclulationFactorsCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.maintainCaclulationFactorsCheckBox.Location = new System.Drawing.Point(296, 176);
			this.maintainCaclulationFactorsCheckBox.Name = "maintainCaclulationFactorsCheckBox";
			this.maintainCaclulationFactorsCheckBox.Size = new System.Drawing.Size(96, 24);
			this.maintainCaclulationFactorsCheckBox.TabIndex = 21;
			this.maintainCaclulationFactorsCheckBox.Text = "Factors";
			this.controlToolTips.SetToolTip(this.maintainCaclulationFactorsCheckBox, "User can maintain calculation factors");
			this.maintainCaclulationFactorsCheckBox.CheckedChanged += new System.EventHandler(this.maintainCaclulationFactorsCheckBox_CheckedChanged);
			// 
			// maintainCalculationIndicesCheckBox
			// 
			this.maintainCalculationIndicesCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.maintainCalculationIndicesCheckBox.Location = new System.Drawing.Point(296, 144);
			this.maintainCalculationIndicesCheckBox.Name = "maintainCalculationIndicesCheckBox";
			this.maintainCalculationIndicesCheckBox.Size = new System.Drawing.Size(96, 24);
			this.maintainCalculationIndicesCheckBox.TabIndex = 18;
			this.maintainCalculationIndicesCheckBox.Text = "Indices";
			this.controlToolTips.SetToolTip(this.maintainCalculationIndicesCheckBox, "User can maintain calculation indices");
			this.maintainCalculationIndicesCheckBox.CheckedChanged += new System.EventHandler(this.maintainCalculationIndicesCheckBox_CheckedChanged);
			// 
			// maintainTolerancesCheckBox
			// 
			this.maintainTolerancesCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.maintainTolerancesCheckBox.Location = new System.Drawing.Point(296, 112);
			this.maintainTolerancesCheckBox.Name = "maintainTolerancesCheckBox";
			this.maintainTolerancesCheckBox.Size = new System.Drawing.Size(96, 24);
			this.maintainTolerancesCheckBox.TabIndex = 14;
			this.maintainTolerancesCheckBox.Text = "Tolerances";
			this.controlToolTips.SetToolTip(this.maintainTolerancesCheckBox, "User can maintain validation tolerances");
			this.maintainTolerancesCheckBox.CheckedChanged += new System.EventHandler(this.maintainTolerancesCheckBox_CheckedChanged);
			// 
			// maintainUserAccessCheckBox
			// 
			this.maintainUserAccessCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.maintainUserAccessCheckBox.Location = new System.Drawing.Point(296, 240);
			this.maintainUserAccessCheckBox.Name = "maintainUserAccessCheckBox";
			this.maintainUserAccessCheckBox.Size = new System.Drawing.Size(96, 24);
			this.maintainUserAccessCheckBox.TabIndex = 24;
			this.maintainUserAccessCheckBox.Text = "User Access";
			this.controlToolTips.SetToolTip(this.maintainUserAccessCheckBox, "User can mantain user access");
			this.maintainUserAccessCheckBox.CheckedChanged += new System.EventHandler(this.maintainUserAccessCheckBox_CheckedChanged);
			// 
			// maintainMethodsCheckBox
			// 
			this.maintainMethodsCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.maintainMethodsCheckBox.Location = new System.Drawing.Point(424, 80);
			this.maintainMethodsCheckBox.Name = "maintainMethodsCheckBox";
			this.maintainMethodsCheckBox.Size = new System.Drawing.Size(96, 24);
			this.maintainMethodsCheckBox.TabIndex = 11;
			this.maintainMethodsCheckBox.Text = "Methods";
			this.controlToolTips.SetToolTip(this.maintainMethodsCheckBox, "User can maintain the distribution methods");
			this.maintainMethodsCheckBox.CheckedChanged += new System.EventHandler(this.maintainMethodsCheckBox_CheckedChanged);
			// 
			// maintainSubscriptionsCheckBox
			// 
			this.maintainSubscriptionsCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.maintainSubscriptionsCheckBox.Location = new System.Drawing.Point(424, 48);
			this.maintainSubscriptionsCheckBox.Name = "maintainSubscriptionsCheckBox";
			this.maintainSubscriptionsCheckBox.Size = new System.Drawing.Size(96, 24);
			this.maintainSubscriptionsCheckBox.TabIndex = 7;
			this.maintainSubscriptionsCheckBox.Text = "Subscriptions";
			this.controlToolTips.SetToolTip(this.maintainSubscriptionsCheckBox, "User can maintain the distribution subscriptions");
			this.maintainSubscriptionsCheckBox.CheckedChanged += new System.EventHandler(this.maintainSubscriptionsCheckBox_CheckedChanged);
			// 
			// maintainFundMappingsCheckBox
			// 
			this.maintainFundMappingsCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.maintainFundMappingsCheckBox.Location = new System.Drawing.Point(296, 80);
			this.maintainFundMappingsCheckBox.Name = "maintainFundMappingsCheckBox";
			this.maintainFundMappingsCheckBox.Size = new System.Drawing.Size(96, 24);
			this.maintainFundMappingsCheckBox.TabIndex = 10;
			this.maintainFundMappingsCheckBox.Text = "Fund Maps";
			this.controlToolTips.SetToolTip(this.maintainFundMappingsCheckBox, "User can maintain the fund mappings");
			this.maintainFundMappingsCheckBox.CheckedChanged += new System.EventHandler(this.maintainFundMappingsCheckBox_CheckedChanged);
			// 
			// pricesReDistributeCheckBox
			// 
			this.pricesReDistributeCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.pricesReDistributeCheckBox.Location = new System.Drawing.Point(152, 208);
			this.pricesReDistributeCheckBox.Name = "pricesReDistributeCheckBox";
			this.pricesReDistributeCheckBox.TabIndex = 22;
			this.pricesReDistributeCheckBox.Text = "Re-Distribute";
			this.controlToolTips.SetToolTip(this.pricesReDistributeCheckBox, "User can re-distribute pricess");
			this.pricesReDistributeCheckBox.CheckedChanged += new System.EventHandler(this.pricesReDistributeCheckBox_CheckedChanged);
			// 
			// pricesDistributeCheckBox
			// 
			this.pricesDistributeCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.pricesDistributeCheckBox.Location = new System.Drawing.Point(152, 176);
			this.pricesDistributeCheckBox.Name = "pricesDistributeCheckBox";
			this.pricesDistributeCheckBox.TabIndex = 20;
			this.pricesDistributeCheckBox.Text = "Distribute";
			this.controlToolTips.SetToolTip(this.pricesDistributeCheckBox, "User can distribute prices");
			this.pricesDistributeCheckBox.CheckedChanged += new System.EventHandler(this.pricesDistributeCheckBox_CheckedChanged);
			// 
			// exportOEICSCheckBox
			// 
			this.exportOEICSCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.exportOEICSCheckBox.Location = new System.Drawing.Point(16, 176);
			this.exportOEICSCheckBox.Name = "exportOEICSCheckBox";
			this.exportOEICSCheckBox.Size = new System.Drawing.Size(112, 24);
			this.exportOEICSCheckBox.TabIndex = 19;
			this.exportOEICSCheckBox.Text = "OEICS";
			this.controlToolTips.SetToolTip(this.exportOEICSCheckBox, "Export OEIC Prices");
			this.exportOEICSCheckBox.CheckedChanged += new System.EventHandler(this.exportOEICSCheckBox_CheckedChanged);
			// 
			// pricesUndoReleaseCheckBox
			// 
			this.pricesUndoReleaseCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.pricesUndoReleaseCheckBox.Location = new System.Drawing.Point(152, 144);
			this.pricesUndoReleaseCheckBox.Name = "pricesUndoReleaseCheckBox";
			this.pricesUndoReleaseCheckBox.TabIndex = 17;
			this.pricesUndoReleaseCheckBox.Text = "Undo Release";
			this.controlToolTips.SetToolTip(this.pricesUndoReleaseCheckBox, "User can undo price release");
			this.pricesUndoReleaseCheckBox.CheckedChanged += new System.EventHandler(this.pricesUndoReleaseCheckBox_CheckedChanged);
			// 
			// pricesReleaseCheckBox
			// 
			this.pricesReleaseCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.pricesReleaseCheckBox.Location = new System.Drawing.Point(152, 112);
			this.pricesReleaseCheckBox.Name = "pricesReleaseCheckBox";
			this.pricesReleaseCheckBox.TabIndex = 13;
			this.pricesReleaseCheckBox.Text = "Release";
			this.controlToolTips.SetToolTip(this.pricesReleaseCheckBox, "User can release prices");
			this.pricesReleaseCheckBox.CheckedChanged += new System.EventHandler(this.pricesReleaseCheckBox_CheckedChanged);
			// 
			// pricesSecondLevelUndoCheckBox
			// 
			this.pricesSecondLevelUndoCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.pricesSecondLevelUndoCheckBox.Location = new System.Drawing.Point(152, 80);
			this.pricesSecondLevelUndoCheckBox.Name = "pricesSecondLevelUndoCheckBox";
			this.pricesSecondLevelUndoCheckBox.TabIndex = 9;
			this.pricesSecondLevelUndoCheckBox.Text = "Undo 2nd Level";
			this.controlToolTips.SetToolTip(this.pricesSecondLevelUndoCheckBox, "User can revoke second level authorisation");
			this.pricesSecondLevelUndoCheckBox.CheckedChanged += new System.EventHandler(this.pricesSecondLevelUndoCheckBox_CheckedChanged);
			// 
			// prices2ndLevelAuthoriseCheckBox
			// 
			this.prices2ndLevelAuthoriseCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.prices2ndLevelAuthoriseCheckBox.Location = new System.Drawing.Point(152, 48);
			this.prices2ndLevelAuthoriseCheckBox.Name = "prices2ndLevelAuthoriseCheckBox";
			this.prices2ndLevelAuthoriseCheckBox.TabIndex = 5;
			this.prices2ndLevelAuthoriseCheckBox.Text = "2nd Level";
			this.controlToolTips.SetToolTip(this.prices2ndLevelAuthoriseCheckBox, "User can authorise second level prices");
			this.prices2ndLevelAuthoriseCheckBox.CheckedChanged += new System.EventHandler(this.prices2ndLevelAuthoriseCheckBox_CheckedChanged);
			// 
			// importExchangeRatesCheckBox
			// 
			this.importExchangeRatesCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.importExchangeRatesCheckBox.Location = new System.Drawing.Point(16, 48);
			this.importExchangeRatesCheckBox.Name = "importExchangeRatesCheckBox";
			this.importExchangeRatesCheckBox.Size = new System.Drawing.Size(112, 24);
			this.importExchangeRatesCheckBox.TabIndex = 4;
			this.importExchangeRatesCheckBox.Text = "Exchange Rates";
			this.controlToolTips.SetToolTip(this.importExchangeRatesCheckBox, "Import exchange rates file");
			this.importExchangeRatesCheckBox.CheckedChanged += new System.EventHandler(this.importExchangeRatesCheckBox_CheckedChanged);
			// 
			// maintenanceLabel
			// 
			this.maintenanceLabel.AutoSize = true;
			this.maintenanceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.maintenanceLabel.Location = new System.Drawing.Point(296, 24);
			this.maintenanceLabel.Name = "maintenanceLabel";
			this.maintenanceLabel.Size = new System.Drawing.Size(69, 16);
			this.maintenanceLabel.TabIndex = 7;
			this.maintenanceLabel.Text = "Maintenance";
			// 
			// pricesLabel
			// 
			this.pricesLabel.AutoSize = true;
			this.pricesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.pricesLabel.Location = new System.Drawing.Point(152, 24);
			this.pricesLabel.Name = "pricesLabel";
			this.pricesLabel.Size = new System.Drawing.Size(36, 16);
			this.pricesLabel.TabIndex = 6;
			this.pricesLabel.Text = "Prices";
			// 
			// importLabel
			// 
			this.importLabel.AutoSize = true;
			this.importLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.importLabel.Location = new System.Drawing.Point(16, 24);
			this.importLabel.Name = "importLabel";
			this.importLabel.Size = new System.Drawing.Size(72, 16);
			this.importLabel.TabIndex = 4;
			this.importLabel.Text = "Import/Export";
			// 
			// importFundWeightingsCheckBox
			// 
			this.importFundWeightingsCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.importFundWeightingsCheckBox.Location = new System.Drawing.Point(16, 144);
			this.importFundWeightingsCheckBox.Name = "importFundWeightingsCheckBox";
			this.importFundWeightingsCheckBox.Size = new System.Drawing.Size(112, 24);
			this.importFundWeightingsCheckBox.TabIndex = 16;
			this.importFundWeightingsCheckBox.Text = "Weightings";
			this.controlToolTips.SetToolTip(this.importFundWeightingsCheckBox, "Import fund weightings file");
			this.importFundWeightingsCheckBox.CheckedChanged += new System.EventHandler(this.importFundWeightingsCheckBox_CheckedChanged);
			// 
			// importHI3PricesCheckBox
			// 
			this.importHI3PricesCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.importHI3PricesCheckBox.Location = new System.Drawing.Point(16, 112);
			this.importHI3PricesCheckBox.Name = "importHI3PricesCheckBox";
			this.importHI3PricesCheckBox.Size = new System.Drawing.Size(112, 24);
			this.importHI3PricesCheckBox.TabIndex = 12;
			this.importHI3PricesCheckBox.Text = "HI3 Prices";
			this.controlToolTips.SetToolTip(this.importHI3PricesCheckBox, "Import HI3 prices file");
			this.importHI3PricesCheckBox.CheckedChanged += new System.EventHandler(this.importHI3PricesCheckBox_CheckedChanged);
			// 
			// importMarketIndicesCheckBox
			// 
			this.importMarketIndicesCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.importMarketIndicesCheckBox.Location = new System.Drawing.Point(16, 80);
			this.importMarketIndicesCheckBox.Name = "importMarketIndicesCheckBox";
			this.importMarketIndicesCheckBox.Size = new System.Drawing.Size(112, 24);
			this.importMarketIndicesCheckBox.TabIndex = 8;
			this.importMarketIndicesCheckBox.Text = "Indices";
			this.controlToolTips.SetToolTip(this.importMarketIndicesCheckBox, "Import market indices file");
			this.importMarketIndicesCheckBox.CheckedChanged += new System.EventHandler(this.importMarketIndicesCheckBox_CheckedChanged);
			// 
			// maintainFundGroupsCheckBox
			// 
			this.maintainFundGroupsCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.maintainFundGroupsCheckBox.Location = new System.Drawing.Point(296, 48);
			this.maintainFundGroupsCheckBox.Name = "maintainFundGroupsCheckBox";
			this.maintainFundGroupsCheckBox.Size = new System.Drawing.Size(96, 24);
			this.maintainFundGroupsCheckBox.TabIndex = 6;
			this.maintainFundGroupsCheckBox.Text = "Fund Groups";
			this.controlToolTips.SetToolTip(this.maintainFundGroupsCheckBox, "User can maintain the fund group standing data");
			this.maintainFundGroupsCheckBox.CheckedChanged += new System.EventHandler(this.maintainFundGroupsCheckBox_CheckedChanged);
			// 
			// deactivateAccountCheckBox
			// 
			this.deactivateAccountCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.deactivateAccountCheckBox.Location = new System.Drawing.Point(280, 58);
			this.deactivateAccountCheckBox.Name = "deactivateAccountCheckBox";
			this.deactivateAccountCheckBox.Size = new System.Drawing.Size(80, 16);
			this.deactivateAccountCheckBox.TabIndex = 16;
			this.deactivateAccountCheckBox.Text = "Deactivate";
			this.controlToolTips.SetToolTip(this.deactivateAccountCheckBox, "Turn off this account");
			this.deactivateAccountCheckBox.Visible = false;
			this.deactivateAccountCheckBox.CheckedChanged += new System.EventHandler(this.deactivateAccountCheckBox_CheckedChanged);
			// 
			// loginIDTextBox
			// 
			this.loginIDTextBox.Location = new System.Drawing.Point(0, 0);
			this.loginIDTextBox.Name = "loginIDTextBox";
			this.loginIDTextBox.TabIndex = 1;
			this.loginIDTextBox.Text = "";
			this.controlToolTips.SetToolTip(this.loginIDTextBox, "The users name");
			this.loginIDTextBox.Visible = false;
			// 
			// loginIDLabel
			// 
			this.loginIDLabel.AutoSize = true;
			this.loginIDLabel.Location = new System.Drawing.Point(16, 26);
			this.loginIDLabel.Name = "loginIDLabel";
			this.loginIDLabel.Size = new System.Drawing.Size(46, 16);
			this.loginIDLabel.TabIndex = 18;
			this.loginIDLabel.Text = "Login ID";
			// 
			// UsersControl
			// 
			this.Controls.Add(this.loginIDTextBox);
			this.Controls.Add(this.deactivateAccountCheckBox);
			this.Controls.Add(this.permissionsGroupBox);
			this.Controls.Add(this.lastUpdatedDateTextBox);
			this.Controls.Add(this.lastUpdatedByTextBox);
			this.Controls.Add(this.userNameTextBox);
			this.Controls.Add(this.lastUpdateDateLabel);
			this.Controls.Add(this.lastUpdatedByLabel);
			this.Controls.Add(this.loginNameLabel);
			this.Controls.Add(this.newUserButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.editUserButton);
			this.Controls.Add(this.usersComboBox);
			this.Controls.Add(this.saveButton);
			this.Controls.Add(this.loginIDLabel);
			this.Controls.Add(this.inactiveAccountLabel);
			this.Name = "UsersControl";
			this.Size = new System.Drawing.Size(592, 416);
			this.permissionsGroupBox.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Event Handlers

        /// <summary>
        /// This button will either add the new user to the collection or persist all users back to the DB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void saveButton_Click(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				if (this.m_currentFormState == FormState.NewUser)
				{
					// We are adding a new user so we will commit the new user to the collection

					// A new user must really only have a valid user name and logon ID
					if (this.loginIDTextBox.Text.Length==0||this.userNameTextBox.Text.Length==0)
					{
						MessageBox.Show ("A username and logon ID must be entered","Invalid Data", MessageBoxButtons.OK);
					}
					else
					{
						// Save the new user to the collection
						User newUser = new User (this.loginIDTextBox.Text,this.userNameTextBox.Text);
						UserPermissions newPermissions = new UserPermissions(
						
							this.maintainAdministratorCheckBox.Checked
							, this.importExchangeRatesCheckBox.Checked
							, this.importMarketIndicesCheckBox.Checked
							, this.importHI3PricesCheckBox.Checked
							, this.importFundWeightingsCheckBox.Checked
							, this.exportOEICSCheckBox.Checked
							, this.prices2ndLevelAuthoriseCheckBox.Checked
							, this.pricesSecondLevelUndoCheckBox.Checked
							, this.pricesReleaseCheckBox.Checked
							, this.pricesUndoReleaseCheckBox.Checked
							, this.pricesDistributeCheckBox.Checked
							, this.pricesReDistributeCheckBox.Checked
							, this.maintainFundGroupsCheckBox.Checked
							, this.maintainFundMappingsCheckBox.Checked
							, this.maintainSubscriptionsCheckBox.Checked
							, this.maintainMethodsCheckBox.Checked
							, this.maintainSubscribersCheckBox.Checked
							, this.maintainUserAccessCheckBox.Checked
							, this.maintainTolerancesCheckBox.Checked
							, this.maintainCalculationIndicesCheckBox.Checked
							, this.maintainCaclulationFactorsCheckBox.Checked
							, this.m_companyID);

						newUser.Permissions = newPermissions;
						m_users.Add (newUser);

						this.setUpFormControls(FormState.DisplayUser);
					
						// Call event to force a nice refresh
						this.usersComboBox_SelectedIndexChanged(this.usersComboBox, new System.EventArgs());
					}
				}
				else
				{
					// Save the data
					UserController saveUsers = new UserController();

//					saveUsers.SaveSomeUsers(m_users, this.m_companyID, this.m_connectionString);

					// unlikley some body else has updated the DB but in case
					// we have stored new rows and need their timestamps we will refresh
					//KAJ (09/03/2005) - Make sure the refresh returns us to the same user
					this.initialiseData(((User)usersComboBox.SelectedItem).LoginID);
				}
			}
			finally
			{
				T.X();
			}
		}

        /// <summary>
        /// This event will change the details of the displayed user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void usersComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				// Each time the combo box changes we will update the text and check boxes in the control
				// We will also stop any editing

				this.setUpFormControls(FormState.DisplayUser);

				m_formProcessing = true;

				// Set the user details first
				User currentUser = m_users[usersComboBox.SelectedIndex];
            
				this.userNameTextBox.Text = currentUser.UserName;

				// If the user has a permission (and they should), use the permisisons last changed information
				if ( currentUser.Permissions != null )
				{
					this.lastUpdatedByTextBox.Text = currentUser.Permissions.LastChangedBy;
					this.lastUpdatedDateTextBox.Text = currentUser.Permissions.LastChangedDate.ToShortDateString();
				}
				else
				{
					this.lastUpdatedByTextBox.Text = currentUser.LastChangedBy;
					this.lastUpdatedDateTextBox.Text = currentUser.LastChangedDate.ToShortDateString();
				}

				// If the user has now been marked as deleted this will override what the database says
				if (currentUser.IsDeleted)
				{
					this.deactivateAccountCheckBox.Checked = true;
				}
				else
				{
					this.deactivateAccountCheckBox.Checked = currentUser.IsDeletedInDB;
				}

				// Now set the users permissions details
				UserPermissions userPerms = currentUser.Permissions;

				// TODO: Could this be done better?  Some form of data binding maybe
				this.importExchangeRatesCheckBox.Checked = userPerms.ImportExchangeRates;
				this.importFundWeightingsCheckBox.Checked = userPerms.ImportOverSeasFundWeightings;
				this.maintainAdministratorCheckBox.Checked = userPerms.Administrator;
				this.importHI3PricesCheckBox.Checked = userPerms.ImportHI3Prices;
				this.importMarketIndicesCheckBox.Checked = userPerms.ImportMarketIndices;
				this.exportOEICSCheckBox.Checked = userPerms.ExportOEICSPrices;
				this.maintainCaclulationFactorsCheckBox.Checked = userPerms.MaintainCalculationFactors;
				this.maintainCalculationIndicesCheckBox.Checked = userPerms.MaintainCalculationIndices;
				this.maintainFundGroupsCheckBox.Checked = userPerms.MaintainFundGroups;
				this.maintainFundMappingsCheckBox.Checked = userPerms.MaintainFundMappings;
				this.maintainMethodsCheckBox.Checked = userPerms.MaintainDistributionMethods;
				this.maintainSubscriptionsCheckBox.Checked = userPerms.MaintainDistributionSubsriptions;
				this.maintainSubscribersCheckBox.Checked = userPerms.MaintainDistributionSubscribers;
				this.maintainTolerancesCheckBox.Checked = userPerms.MaintainValidationTolerances;
				this.maintainUserAccessCheckBox.Checked = userPerms.MaintainUserAccess;
				this.prices2ndLevelAuthoriseCheckBox.Checked = userPerms.SecondLevelAuthorise;
				this.pricesDistributeCheckBox.Checked = userPerms.DistributePrices;
				this.pricesReDistributeCheckBox.Checked = userPerms.ReDistributePrices;
				this.pricesReleaseCheckBox.Checked = userPerms.ReleasePrices;
				this.pricesSecondLevelUndoCheckBox.Checked = userPerms.UndoSecondLevelAuthorise;
				this.pricesUndoReleaseCheckBox.Checked = userPerms.UndoReleasePrices;

				m_formProcessing = false;

				// If the user has already edited/deleted or created this person we will
				// put it back in an edit state by default.
				if (currentUser.IsDirty||userPerms.IsDirty||currentUser.IsNew||currentUser.IsDeleted)
				{
					this.setUpFormControls(FormState.EditUser);
				}
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// This event updates the username when it has been changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void userNameTextBox_TextChanged(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				// Only update if we are not running code and also not creating a new user
				if (m_formProcessing == false&&this.m_currentFormState != FormState.NewUser)
				{
					// The combo box is not sorted but the data from the database is
					// otherwise this would not work
					User user = m_users[this.usersComboBox.SelectedIndex];

					user.UserName = userNameTextBox.Text;
					this.enableSaveCancel();
				}
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// The edit user button allows users to change the permissions and username
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void editUserButton_Click(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				this.setUpFormControls(FormState.EditUser);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Create a new user
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void newUserButton_Click(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				this.setUpFormControls (FormState.NewUser);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// The deactivate event turns the deleted flag on/off
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deactivateAccountCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				// The combo box is not sorted but the data from the database is
				// otherwise this would not work

				if (this.m_formProcessing == false)
				{
					User user = m_users[this.usersComboBox.SelectedIndex];

					user.IsDeleted = deactivateAccountCheckBox.Checked;
					this.enableSaveCancel();
				}
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// The close button event
		/// It must handle closing when the users collection is dirty
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelButton_Click(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				// If the login text box is visble we are adding a new user so we will treate close as cancel
				if (this.m_currentFormState == FormState.NewUser)
				{
					// Call event to force a nice refresh
					this.usersComboBox_SelectedIndexChanged(this.usersComboBox, new System.EventArgs());
				}
				else
				{
					// TODO :  We must prompt to save any unsaved data and request a close event call from the parent
					DialogResult userChoice =  MessageBox.Show ("Cancel all unsaved updates?", "Cancel changes", MessageBoxButtons.YesNo);

					// This will cause the database to be called to refesh the data source
					// this works fine as the call to the data source is very quick
					//KAJ 09/03/2005 - Refresh after cancel will keep current user active
                
					if (userChoice == DialogResult.Yes)
					{
						this.initialiseData(((User)usersComboBox.SelectedItem).LoginID);
					}
				}
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Paint the permissions box
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void permissionsGroupBox_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			T.E();

			try
			{
				// We will add some divider lines to help things look pretty
				Pen myPen = new Pen(Color.LightGray, 3);
				Graphics myGraphics = e.Graphics;
				myGraphics.DrawLine(myPen, this.pricesSecondLevelUndoCheckBox.Left - 10, 40, this.pricesSecondLevelUndoCheckBox.Left - 10, 265);
				myGraphics.DrawLine(myPen, this.maintainFundGroupsCheckBox.Left - 10, 40, this.maintainFundGroupsCheckBox.Left - 10, 265);
				myGraphics.DrawLine(myPen, this.maintainSubscriptionsCheckBox.Left - 10, 40, this.maintainSubscriptionsCheckBox.Left - 10, 265);
			}
			finally
			{
				T.X();
			}
		}

		#endregion

		#region Private Methods

        /// <summary>
        /// This method is a consolidation of all common code called by clicking on a checkbox
        /// </summary>
        /// <param name="controlClicked"></param>
		private void updateUser (CheckBox controlClicked)
		{
			T.E();

			try
			{
				// Only perform the update if we are not processing and not adding a new user
				if (this.m_formProcessing == false&&this.m_currentFormState != FormState.NewUser)
				{
					// The combo box is not sorted but the data from the database is
					// otherwise this would not work
					UserPermissions userPerms = m_users[this.usersComboBox.SelectedIndex].Permissions;

					switch (controlClicked.Name)
					{
						case "importExchangeRatesCheckBox":
							userPerms.ImportExchangeRates = this.importExchangeRatesCheckBox.Checked;
							break;
						case "importFundWeightingsCheckBox":
							userPerms.ImportOverSeasFundWeightings = this.importFundWeightingsCheckBox.Checked ;
							break;
						case "importHI3PricesCheckBox":
							userPerms.ImportHI3Prices = this.importHI3PricesCheckBox.Checked;
							break;
						case "importMarketIndicesCheckBox":
							userPerms.ImportMarketIndices = this.importMarketIndicesCheckBox.Checked;
							break;
						case "exportOEICSCheckBox":
							userPerms.ExportOEICSPrices = this.exportOEICSCheckBox.Checked;
							break;
						case "maintainCaclulationFactorsCheckBox":
							userPerms.MaintainCalculationFactors = this.maintainCaclulationFactorsCheckBox.Checked;
							break;
						case "maintainCalculationIndicesCheckBox":
							userPerms.MaintainCalculationIndices = this.maintainCalculationIndicesCheckBox.Checked;
							break;
						case "maintainFundGroupsCheckBox":
							userPerms.MaintainFundGroups = this.maintainFundGroupsCheckBox.Checked;
							break;
						case "maintainFundMappingsCheckBox":
							userPerms.MaintainFundMappings=this.maintainFundMappingsCheckBox.Checked;
							break;
						case "maintainMethodsCheckBox":
							userPerms.MaintainDistributionMethods = this.maintainMethodsCheckBox.Checked;
							break;
						case "maintainSubscriptionsCheckBox":
							userPerms.MaintainDistributionSubsriptions = this.maintainSubscriptionsCheckBox.Checked;
							break;
						case "maintainTolerancesCheckBox":
							userPerms.MaintainValidationTolerances = this.maintainTolerancesCheckBox.Checked;
							break;
						case "maintainUserAccessCheckBox":
							userPerms.MaintainUserAccess =this.maintainUserAccessCheckBox.Checked;
							break;
						case "prices2ndLevelAuthoriseCheckBox":
							userPerms.SecondLevelAuthorise = this.prices2ndLevelAuthoriseCheckBox.Checked;
							break;
						case "pricesDistributeCheckBox":
							userPerms.DistributePrices = this.pricesDistributeCheckBox.Checked;
							break;
						case "pricesReDistributeCheckBox":
							userPerms.ReDistributePrices = this.pricesReDistributeCheckBox.Checked;
							break;
						case "pricesReleaseCheckBox":
							userPerms.ReleasePrices = this.pricesReleaseCheckBox.Checked;
							break;
						case "pricesSecondLevelUndoCheckBox":
							userPerms.UndoSecondLevelAuthorise =this.pricesSecondLevelUndoCheckBox.Checked;
							break;
						case "pricesUndoReleaseCheckBox":
							userPerms.UndoReleasePrices = this.pricesUndoReleaseCheckBox.Checked;
							break;
						case "maintainAdministratorCheckBox":
							userPerms.Administrator = this.maintainAdministratorCheckBox.Checked;
							break;
						case "maintainSubscribersCheckBox":
							userPerms.MaintainDistributionSubscribers = this.maintainSubscribersCheckBox.Checked;
							break;
						default:
							// Should never get here
							break;
					}                
					
					this.enableSaveCancel();
				}
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// This routine ensures that the controls on the form are in an approriate state for functionality to work
		/// </summary>
		/// <param name="StateRequired"></param>
		private void setUpFormControls (FormState StateRequired)
		{
			T.E();

			try
			{
				// Turn on processing flag to bypass any events being fired on the relevant data controls
				this.m_formProcessing = true;
			
				switch (StateRequired)
				{
					case FormState.DisplayUser:

						// We are displaying a user
						this.usersComboBox.Visible = true;
						this.loginIDTextBox.Visible = false;
						this.userNameTextBox.Enabled = false;
						this.deactivateAccountCheckBox.Enabled = false;
						this.permissionsGroupBox.Enabled = false;

						// Make sure the new user and edit user buttons are turned back on
						this.newUserButton.Visible = true;
						this.newUserButton.Enabled = true;
						this.editUserButton.Visible = true;
						this.editUserButton.Enabled = true;
						this.saveButton.Enabled = false;

						// Change close button back to close and save back to save
						this.saveButton.Text = "&Save";
						this.saveButton.Enabled = false;
						this.cancelButton.Enabled = false; 
						m_currentFormState = FormState.DisplayUser;
						this.controlToolTips.SetToolTip(this.saveButton, "Save the current data to the data base");
						this.controlToolTips.SetToolTip(this.cancelButton, "Cancel all unsaved changes");

						// Scan the collection to see if the edit save button needs enabling
						foreach (User userObject in m_users)
						{
							if (userObject.IsDirty || userObject.IsNew || userObject.IsDeleted || userObject.PermissionsDeleted || userObject.PermissionsDirty || userObject.PermissionsNew)
							{
								this.enableSaveCancel();
								break;
							}
						}

						break;
					case FormState.EditUser:
						// Edit can only be called after a user has been displayed
						// so we know the state of the controls
						this.editUserButton.Enabled = false;
						this.newUserButton.Enabled = true;
						this.userNameTextBox.Enabled = true;
						this.deactivateAccountCheckBox.Enabled = true;
						this.permissionsGroupBox.Enabled = true;

						m_currentFormState = FormState.EditUser;

						break;

					case FormState.NewUser:
						// Set up the new form layout
						this.loginIDTextBox.Size = this.usersComboBox.Size;
						this.loginIDTextBox.Left= this.usersComboBox.Left;
						this.loginIDTextBox.Top = this.usersComboBox.Top;
						this.usersComboBox.Visible = false;
						this.loginIDTextBox.Visible = true;
						this.loginIDTextBox.Text = string.Empty;
						this.userNameTextBox.Text = string.Empty;
						this.lastUpdatedByTextBox.Text = string.Empty;
						this.lastUpdatedDateTextBox.Text = string.Empty;
						this.userNameTextBox.Enabled = true;
					
						// Hide the edit and new buttons they are no longer needed
						this.newUserButton.Visible = false;
						this.editUserButton.Visible = false;

						// Change save to OK
						this.saveButton.Text = "&OK";
						this.enableSaveCancel();
						this.controlToolTips.SetToolTip(this.saveButton, "Add the new current user to to the current records");
						this.controlToolTips.SetToolTip(this.cancelButton, "Cancel adding this new user");

						// Enable editing of the permissions check boxes
						this.permissionsGroupBox.Enabled = true;

						// Default all permission boxes to false;
						foreach (Control checkBoxes in this.permissionsGroupBox.Controls)
						{
							if (checkBoxes is CheckBox)
							{
								CheckBox currentCheckBox  = (CheckBox) checkBoxes;
								currentCheckBox.Checked = false;
							}
						}

						// We do not open inactive accounts!
						this.deactivateAccountCheckBox.Visible = false;
						this.inactiveAccountLabel.Visible = false;

						m_currentFormState = FormState.NewUser;

						break;
					default:
						// Should never get here
						break;
				}
				// Turn processing back on
				this.m_formProcessing = false;
			}
			finally
			{
				T.X();
			}
		}
		
		/// <summary>
		/// Refresh of the data after save or cancel.  Current user remains active.
		/// </summary>
		/// <param name="userLogin"></param>
		private void initialiseData(string userLogin)
		{
			T.E();

			try
			{
				this.initialiseData();
				usersComboBox.Text = userLogin;
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Routine to initialise the form with data
		/// </summary>
		private void initialiseData ()
		{
			T.E();

			try
			{
				// This method will initialise the form
				this.m_connectionString = ConfigurationSettings.AppSettings["ConnectionString"];

				// Get the currently selected company from the GUI principal thread
				UPDPrincipal updPrincipal = (UPDPrincipal) System.Threading.Thread.CurrentPrincipal;

				this.m_companyID = updPrincipal.CompanyCode;
			
				// TODO: Add any initialization after the InitializeComponent call
				UserController controller = new UserController();
//				m_users = controller.GimmeSomeUsers(m_connectionString, this.m_companyID);

				m_usersClone = m_users.Clone();

				// Ensure form is in correct state
				this.setUpFormControls(FormState.DisplayUser);

				// Assigning the data Source causes the SelectedItem event to fire
				this.usersComboBox.DataSource = m_users;
				this.usersComboBox.DisplayMember = "LoginID";
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Centralised turning on/off save cancel buttons
		/// </summary>
		private void enableSaveCancel ()
		{
			T.E();

			try
			{
				this.cancelButton.Enabled = true;
				this.saveButton.Enabled = true;
			}
			finally
			{
				T.X();
			}
		}

        private void saveUser()
        {
            T.E();

            try
            {
                // Save the data
                UserController saveUsers = new UserController();

//                saveUsers.SaveSomeUsers(m_users, this.m_companyID, this.m_connectionString);

                // unlikley some body else has updated the DB but in case
                // we have stored new rows and need their timestamps we will refresh
                //KAJ (09/03/2005) - Make sure the refresh returns us to the same user
                this.initialiseData(((User)usersComboBox.SelectedItem).LoginID);
            }
            catch( ConstraintViolationException ex )
            {
                ExceptionManager.Publish( ex );
                T.Log( "ConstraintViolationException" );
                T.DumpException( ex );
                MessageBoxHelper.ShowError( "DuplicateUserBody", "DuplicateUserTitle", ex );
            }
            catch( ConcurrencyViolationException ex )
            {
                ExceptionManager.Publish( ex );
                T.Log( "ConcurrencyViolationException" );
                T.DumpException( ex );
                MessageBoxHelper.ShowError( "UserChangedBody", "UserUnableToSaveTitle", ex );
            }

            catch( Exception ex )
            {
                ExceptionManager.Publish( ex );
                T.Log("ConstraintViolationException");
                T.DumpException( ex );
                MessageBoxHelper.ShowError( "SystemError", "UserUnableToSaveTitle", ex );
            }
            finally
            {
                T.X();
            }
        }

		#endregion

		#region CheckBox Common Event Code

		/// <summary>
		/// Checked Changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void importExchangeRatesCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				this.updateUser ( (CheckBox) sender);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Checked Changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void importMarketIndicesCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				this.updateUser ( (CheckBox) sender);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Checked Changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void importHI3PricesCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				this.updateUser ( (CheckBox) sender);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Checked Changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void importFundWeightingsCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				this.updateUser ( (CheckBox) sender);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Checked Changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void exportOEICSCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				this.updateUser ( (CheckBox) sender);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Checked Changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void prices2ndLevelAuthoriseCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				this.updateUser ( (CheckBox) sender);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Checked Changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pricesSecondLevelUndoCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				this.updateUser ( (CheckBox) sender);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Checked Changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pricesReleaseCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				this.updateUser ( (CheckBox) sender);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Checked Changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pricesUndoReleaseCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				this.updateUser ( (CheckBox) sender);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Checked Changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pricesDistributeCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				this.updateUser ( (CheckBox) sender);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Checked Changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pricesReDistributeCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				this.updateUser ( (CheckBox) sender);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Checked Changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void maintainFundGroupsCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				this.updateUser ( (CheckBox) sender);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Checked Changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void maintainFundMappingsCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				this.updateUser ( (CheckBox) sender);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Checked Changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void maintainTolerancesCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				this.updateUser ( (CheckBox) sender);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Checked Changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void maintainCalculationIndicesCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				this.updateUser ( (CheckBox) sender);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Checked Changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void maintainCaclulationFactorsCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				this.updateUser ( (CheckBox) sender);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Checked Changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void maintainSubscriptionsCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				this.updateUser ( (CheckBox) sender);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Checked Changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void maintainMethodsCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				this.updateUser ( (CheckBox) sender);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Checked Changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void maintainUserAccessCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				this.updateUser ( (CheckBox) sender);
			}
			finally
			{
				T.X();
			}
		}
		
		/// <summary>
		/// Checked Changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void maintainAdministratorCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				this.updateUser ( (CheckBox) sender);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Checked Changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void maintainSubscribersCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				this.updateUser ( (CheckBox) sender);
			}
			finally
			{
				T.X();
			}
		}

		#endregion
	}
}
