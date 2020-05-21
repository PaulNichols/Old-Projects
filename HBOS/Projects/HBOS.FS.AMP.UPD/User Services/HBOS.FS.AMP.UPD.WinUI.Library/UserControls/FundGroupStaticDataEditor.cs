using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Types.DistributionFiles;
using HBOS.FS.AMP.UPD.Types.FundGroups;
using HBOS.FS.AMP.UPD.Types.Lookups;
using HBOS.FS.AMP.UPD.WinUI.Classes;
using HBOS.FS.AMP.UPD.WinUI.Forms;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.UserControls
{
	/// <summary>
	/// Summary description for FundGroupStaticDataEditor.
	/// </summary>
	public class FundGroupStaticDataEditor : StaticDataEditor
	{
		#region Member Variables

		private bool m_systemGeneratedEvent = false;

		#endregion

		#region Form Stuff/Constructors/Dispose

		private TabPage tabPageProperties;
		private Label lFullName;
		private Label lShortName;
		private TextBox txtFullName;
		private TextBox txtShortName;
		private GroupBox grpElements;
		private RadioButton rdoFund;
		private RadioButton rdoAssetFund;
		private CheckBox chkForRelease;
		private TabControl tabControlMain;
		private TabPage tabDistribution;
		private Label lDistributionHelp1;
		private Label lDistributionHelp2;
		private GroupBox grpDistribution;
		private Panel pnlListToList;
		private System.Windows.Forms.Label LabelfundGrpNumber;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox allowSelectAllAuthorisationChk;

		/// <summary>
		/// Creates a new <see cref="FundGroupStaticDataEditor"/> instance.
		/// </summary>
		public FundGroupStaticDataEditor()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			m_distributionFiles=null;
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		#endregion

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tabControlMain = new System.Windows.Forms.TabControl();
			this.tabPageProperties = new System.Windows.Forms.TabPage();
			this.allowSelectAllAuthorisationChk = new System.Windows.Forms.CheckBox();
			this.grpElements = new System.Windows.Forms.GroupBox();
			this.rdoAssetFund = new System.Windows.Forms.RadioButton();
			this.rdoFund = new System.Windows.Forms.RadioButton();
			this.chkForRelease = new System.Windows.Forms.CheckBox();
			this.txtShortName = new System.Windows.Forms.TextBox();
			this.txtFullName = new System.Windows.Forms.TextBox();
			this.lShortName = new System.Windows.Forms.Label();
			this.lFullName = new System.Windows.Forms.Label();
			this.tabDistribution = new System.Windows.Forms.TabPage();
			this.grpDistribution = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.pnlListToList = new System.Windows.Forms.Panel();
			this.lDistributionHelp1 = new System.Windows.Forms.Label();
			this.lDistributionHelp2 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.LabelfundGrpNumber = new System.Windows.Forms.Label();
			this.tabControlMain.SuspendLayout();
			this.tabPageProperties.SuspendLayout();
			this.grpElements.SuspendLayout();
			this.tabDistribution.SuspendLayout();
			this.grpDistribution.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControlMain
			// 
			this.tabControlMain.Controls.Add(this.tabPageProperties);
			this.tabControlMain.Controls.Add(this.tabDistribution);
			this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControlMain.Location = new System.Drawing.Point(0, 0);
			this.tabControlMain.Name = "tabControlMain";
			this.tabControlMain.SelectedIndex = 0;
			this.tabControlMain.Size = new System.Drawing.Size(576, 504);
			this.tabControlMain.TabIndex = 0;
			// 
			// tabPageProperties
			// 
			this.tabPageProperties.Controls.Add(this.allowSelectAllAuthorisationChk);
			this.tabPageProperties.Controls.Add(this.grpElements);
			this.tabPageProperties.Controls.Add(this.chkForRelease);
			this.tabPageProperties.Controls.Add(this.txtShortName);
			this.tabPageProperties.Controls.Add(this.txtFullName);
			this.tabPageProperties.Controls.Add(this.lShortName);
			this.tabPageProperties.Controls.Add(this.lFullName);
			this.tabPageProperties.Location = new System.Drawing.Point(4, 25);
			this.tabPageProperties.Name = "tabPageProperties";
			this.tabPageProperties.Size = new System.Drawing.Size(568, 475);
			this.tabPageProperties.TabIndex = 0;
			this.tabPageProperties.Text = "Properties";
			// 
			// allowSelectAllAuthorisationChk
			// 
			this.allowSelectAllAuthorisationChk.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.allowSelectAllAuthorisationChk.Location = new System.Drawing.Point(32, 136);
			this.allowSelectAllAuthorisationChk.Name = "allowSelectAllAuthorisationChk";
			this.allowSelectAllAuthorisationChk.Size = new System.Drawing.Size(128, 40);
			this.allowSelectAllAuthorisationChk.TabIndex = 8;
			this.allowSelectAllAuthorisationChk.Text = "Allow \'Select All\' Authorisation";
			this.allowSelectAllAuthorisationChk.CheckedChanged += new System.EventHandler(this.allowSelectAllAuthorisationChk_CheckedChanged);
			// 
			// grpElements
			// 
			this.grpElements.Controls.Add(this.rdoAssetFund);
			this.grpElements.Controls.Add(this.rdoFund);
			this.grpElements.Location = new System.Drawing.Point(32, 192);
			this.grpElements.Name = "grpElements";
			this.grpElements.Size = new System.Drawing.Size(288, 96);
			this.grpElements.TabIndex = 5;
			this.grpElements.TabStop = false;
			this.grpElements.Text = "Fund Group Type";
			// 
			// rdoAssetFund
			// 
			this.rdoAssetFund.Location = new System.Drawing.Point(16, 56);
			this.rdoAssetFund.Name = "rdoAssetFund";
			this.rdoAssetFund.TabIndex = 1;
			this.rdoAssetFund.Text = "Asset Fund";
			this.rdoAssetFund.Click += new System.EventHandler(this.defaultControlChanged);
			// 
			// rdoFund
			// 
			this.rdoFund.Location = new System.Drawing.Point(16, 24);
			this.rdoFund.Name = "rdoFund";
			this.rdoFund.TabIndex = 0;
			this.rdoFund.Text = "Fund";
			this.rdoFund.Click += new System.EventHandler(this.defaultControlChanged);
			// 
			// chkForRelease
			// 
			this.chkForRelease.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkForRelease.Location = new System.Drawing.Point(32, 104);
			this.chkForRelease.Name = "chkForRelease";
			this.chkForRelease.Size = new System.Drawing.Size(128, 24);
			this.chkForRelease.TabIndex = 4;
			this.chkForRelease.Text = "For Release";
			this.chkForRelease.Click += new System.EventHandler(this.chkForRelease_Click);
			// 
			// txtShortName
			// 
			this.txtShortName.Location = new System.Drawing.Point(144, 70);
			this.txtShortName.MaxLength = 50;
			this.txtShortName.Name = "txtShortName";
			this.txtShortName.Size = new System.Drawing.Size(320, 22);
			this.txtShortName.TabIndex = 3;
			this.txtShortName.Text = "";
			this.txtShortName.TextChanged += new System.EventHandler(this.defaultControlChanged);
			// 
			// txtFullName
			// 
			this.txtFullName.Location = new System.Drawing.Point(144, 30);
			this.txtFullName.MaxLength = 100;
			this.txtFullName.Name = "txtFullName";
			this.txtFullName.Size = new System.Drawing.Size(320, 22);
			this.txtFullName.TabIndex = 2;
			this.txtFullName.Text = "";
			this.txtFullName.TextChanged += new System.EventHandler(this.defaultControlChanged);
			// 
			// lShortName
			// 
			this.lShortName.Location = new System.Drawing.Point(32, 72);
			this.lShortName.Name = "lShortName";
			this.lShortName.Size = new System.Drawing.Size(100, 16);
			this.lShortName.TabIndex = 1;
			this.lShortName.Text = "Short Name:";
			// 
			// lFullName
			// 
			this.lFullName.Location = new System.Drawing.Point(32, 32);
			this.lFullName.Name = "lFullName";
			this.lFullName.Size = new System.Drawing.Size(100, 16);
			this.lFullName.TabIndex = 0;
			this.lFullName.Text = "Full Name:";
			// 
			// tabDistribution
			// 
			this.tabDistribution.Controls.Add(this.grpDistribution);
			this.tabDistribution.Location = new System.Drawing.Point(4, 25);
			this.tabDistribution.Name = "tabDistribution";
			this.tabDistribution.Size = new System.Drawing.Size(568, 475);
			this.tabDistribution.TabIndex = 1;
			this.tabDistribution.Text = "Distribution";
			// 
			// grpDistribution
			// 
			this.grpDistribution.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grpDistribution.Controls.Add(this.label2);
			this.grpDistribution.Controls.Add(this.pnlListToList);
			this.grpDistribution.Controls.Add(this.lDistributionHelp1);
			this.grpDistribution.Controls.Add(this.lDistributionHelp2);
			this.grpDistribution.ForeColor = System.Drawing.Color.Blue;
			this.grpDistribution.Location = new System.Drawing.Point(16, 16);
			this.grpDistribution.Name = "grpDistribution";
			this.grpDistribution.Size = new System.Drawing.Size(536, 456);
			this.grpDistribution.TabIndex = 0;
			this.grpDistribution.TabStop = false;
			this.grpDistribution.Text = "Link Fund Group to Distribution Files";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.ForeColor = System.Drawing.Color.Blue;
			this.label2.Location = new System.Drawing.Point(232, 408);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(288, 32);
			this.label2.TabIndex = 3;
			this.label2.Text = "Please double click a file to alter it\'s settings within the context of this Fund" +
				" Group";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// pnlListToList
			// 
			this.pnlListToList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.pnlListToList.Location = new System.Drawing.Point(16, 56);
			this.pnlListToList.Name = "pnlListToList";
			this.pnlListToList.Size = new System.Drawing.Size(512, 384);
			this.pnlListToList.TabIndex = 2;
			// 
			// lDistributionHelp1
			// 
			this.lDistributionHelp1.AutoSize = true;
			this.lDistributionHelp1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lDistributionHelp1.Location = new System.Drawing.Point(16, 26);
			this.lDistributionHelp1.Name = "lDistributionHelp1";
			this.lDistributionHelp1.Size = new System.Drawing.Size(496, 18);
			this.lDistributionHelp1.TabIndex = 0;
			this.lDistributionHelp1.Text = "To include a Fund on a Distribution file, add the fund to a Release type Fund Gro" +
				"up";
			// 
			// lDistributionHelp2
			// 
			this.lDistributionHelp2.AutoSize = true;
			this.lDistributionHelp2.BackColor = System.Drawing.SystemColors.Control;
			this.lDistributionHelp2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lDistributionHelp2.Location = new System.Drawing.Point(16, 41);
			this.lDistributionHelp2.Name = "lDistributionHelp2";
			this.lDistributionHelp2.Size = new System.Drawing.Size(333, 18);
			this.lDistributionHelp2.TabIndex = 1;
			this.lDistributionHelp2.Text = "and associate that Fund Group with the Distribution file.";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(0, 0);
			this.textBox1.Name = "textBox1";
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.TabIndex = 0;
			// 
			// LabelfundGrpNumber
			// 
			this.LabelfundGrpNumber.Location = new System.Drawing.Point(0, 0);
			this.LabelfundGrpNumber.Name = "LabelfundGrpNumber";
			this.LabelfundGrpNumber.TabIndex = 0;
			// 
			// FundGroupStaticDataEditor
			// 
			this.Controls.Add(this.tabControlMain);
			this.Name = "FundGroupStaticDataEditor";
			this.Size = new System.Drawing.Size(576, 504);
			this.tabControlMain.ResumeLayout(false);
			this.tabPageProperties.ResumeLayout(false);
			this.grpElements.ResumeLayout(false);
			this.tabDistribution.ResumeLayout(false);
			this.grpDistribution.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		#region Properties

		private FundGroup currentFundGroup;
		/// <summary>
		/// Gets or sets the current fund group being edited.
		/// </summary>
		/// <value></value>
		public FundGroup CurrentFundGroup
		{
			get { return currentFundGroup; }
			set
			{
				bool indexChanged = currentFundGroup == null || currentFundGroup.ID != value.ID;

				currentFundGroup = value;
				if (indexChanged)
				{
					//in discussion with Richard (analyst) - always display this tab
					//when changing items
					tabControlMain.SelectedTab = tabPageProperties;
				}
				displayFundGroupProperties();
				enableControls();
				createListToList();
				clearErrors();
				if (indexChanged)
				{
					Changed = false;
				}
			}
		}

	private authoringModes authoringMode
		{
			get
			{
				if (CurrentFundGroup == null)
					return authoringModes.Null;
				else if (ListManager.SelectedIsNew)
					return authoringModes.New;
				else
					return authoringModes.Editing;
			}
		}

		/// <summary>
		/// Gets the description of the current fund group.
		/// </summary>
		/// <value></value>
		protected override string currentEntityDescription
		{
			get
			{
				return CurrentFundGroup.FullName;
			}
		}


		#endregion Properties

		#region Privates

		private void clearErrors()
		{
			clearErrors(tabControlMain);
		}

		private void displayFundGroupProperties()
		{
			try
			{
				m_systemGeneratedEvent = true;

				if (authoringMode == authoringModes.Editing || authoringMode == authoringModes.New)
				{
					txtFullName.Text = CurrentFundGroup.FullName;
					txtShortName.Text = CurrentFundGroup.ShortName;
					chkForRelease.Checked = CurrentFundGroup.ForRelease;
					
					if (CurrentFundGroup.ForRelease)
					{
						allowSelectAllAuthorisationChk.Checked = false;
					}
					else
					{
						allowSelectAllAuthorisationChk.Checked = CurrentFundGroup.AllowSelectAllAuthorisation;
					}

					rdoAssetFund.Checked = (CurrentFundGroup is AssetFundGroup);
					rdoFund.Checked = (CurrentFundGroup is IndividualFundGroup);
				}
				else
				{
					txtFullName.Text = string.Empty;
					txtShortName.Text = string.Empty;
					rdoAssetFund.Checked = false;
					rdoFund.Checked = false;
					chkForRelease.Checked = false;
				}
			}
			finally
			{
				m_systemGeneratedEvent = false;
			}
		}

		private void enableControls()
		{
			txtFullName.Enabled = (authoringMode == authoringModes.New || authoringMode == authoringModes.Editing);
			txtShortName.Enabled = (authoringMode == authoringModes.New || authoringMode == authoringModes.Editing);
			chkForRelease.Enabled = (authoringMode == authoringModes.New || authoringMode == authoringModes.Editing);
			grpElements.Enabled = (authoringMode == authoringModes.New);
			allowSelectAllAuthorisationChk.Enabled = !chkForRelease.Checked;

			setDistributionPageVisibility(chkForRelease.Checked && authoringMode != authoringModes.Null);
		}


		private void setDistributionPageVisibility(bool visible)
		{
			if (visible)
			{
				if (!tabControlMain.TabPages.Contains(tabDistribution))
				{
					tabControlMain.TabPages.Add(tabDistribution);
				}
			}
			else
			{
				tabControlMain.TabPages.Remove(tabDistribution);
			}
		}

		private ListToListControl listToList;

		private void createListToList()
		{
			if (listToList != null)
			{
				pnlListToList.Controls.Remove(listToList);
				listToList = null;
			}

			if (chkForRelease.Checked)
			{
				if (m_distributionFiles == null) m_distributionFiles = distFileController.LoadFilesForCompanyCode(GlobalRegistry.CompanyCode);

				if (m_distributionFiles != null)
				{
					listToList = new ListToListControl();
					IList selFiles = CurrentFundGroup.DistributionFiles;

					if (selFiles != null)
					{
						listToList.SetColumnsAndLists(m_distributionFiles, selFiles);
					}
					else
					{
						listToList.SetColumnsAndLists(m_distributionFiles, null);
					}

					listToList.Dock = DockStyle.Fill;
					listToList.ForeColor = Color.Black;
					listToList.AddingSelectedItems += new ListToListChangingHandler(listToList_Changing);
					listToList.SelectedItemsDoubleClicked+=new ListToListItemDoubleClickedHandler(listToList_SelectedItemDoubleClicked);
					if (authoringMode == authoringModes.Editing)
					{
						listToList.RemovingSelectedItems += new ListToListChangingHandler(listToList_Changing);
					}

					pnlListToList.Controls.Add(listToList);
				}
			}

		}

		private void listToList_SelectedItemDoubleClicked(object sender, EventArgs e)
		{
			ListView selectedList=sender as System.Windows.Forms.ListView;
			if (selectedList!=null && selectedList.SelectedItems.Count>0 
				&& selectedList.SelectedItems[0].Tag!=null)
			{
				FundGroupDistributionFile file=selectedList.SelectedItems[0].Tag as FundGroupDistributionFile;
				if (file!=null)
				{
					popupSettings(ref file);
				}
			}
		}

		private void popupSettings(ref FundGroupDistributionFile file)
		{
			Forms.FundGroupFileSettings settingsPopup=new FundGroupFileSettings(ref file);
			DialogResult result= settingsPopup.ShowDialog();
			this.Changed=result==DialogResult.OK;
		}


		private void change()
		{
			Changed = true;
		}

		private FundGroupController fundGroupController = new FundGroupController(GlobalRegistry.ConnectionString);
		private DistributionFileController distFileController = new DistributionFileController(GlobalRegistry.ConnectionString);
		private static DistributionFileCollection m_distributionFiles;

		/// <summary>
		/// Validates that the fund group can be saved
		/// </summary>
		/// <returns>true if ok, false if any errors</returns>
		private bool validateFundGroup(FundGroup currentFundGroup)
		{
			FundGroupController.FundGroupValidationError fullNameError;
			FundGroupController.FundGroupValidationError shortNameError;
			bool validFundGroupType;

			bool valid = fundGroupController.ValidateFundGroup (currentFundGroup, out fullNameError, out shortNameError,  out validFundGroupType);


			string shortNameErrorMessage = null;
			string fullNameErrorMessage = null;
			string fundGroupTypeErrorMessage = null;
			string fundGroupNumberErrorMessage = null;

			if (fullNameError == FundGroupController.FundGroupValidationError.FieldEmpty)
			{
				fullNameErrorMessage = "You must supply a full name for the fund group.";
			}
			else if (fullNameError == FundGroupController.FundGroupValidationError.DuplicateField)
			{
				fullNameErrorMessage = "Fund Group with full name '" + txtFullName.Text + "' already exists in the system.";
			}

			if (shortNameError == FundGroupController.FundGroupValidationError.FieldEmpty)
			{
				shortNameErrorMessage = "You must supply a short name for the fund group.";
			}
			else if (shortNameError == FundGroupController.FundGroupValidationError.DuplicateField)
			{
				shortNameErrorMessage = "Fund Group with short name '" + txtShortName.Text + "' already exists in the system.";
			}

			if (!validFundGroupType)
			{
				valid = false;			
				fundGroupTypeErrorMessage = "You must specify a fund group type";
			}

//			if (!fundGroupNumberParsedOK || fundGroupNumberError == FundGroupController.FundGroupValidationError.InvalidValue)
//			{
//				valid = false;
//				fundGroupNumberErrorMessage = "Invalid fund group number. If a fund group number is to be supplied, then it must be a value 01-10 or 30.";
//			}
//			else if (fundGroupNumberError == FundGroupController.FundGroupValidationError.DuplicateField)
//			{
//				valid = false;
//				fundGroupNumberErrorMessage = "This fund group number has been assigned to another fund group.";
//			}

			this.ShowErrors(fullNameErrorMessage, shortNameErrorMessage, fundGroupTypeErrorMessage, fundGroupNumberErrorMessage);

			return valid;
		}

		private string fundGroupTypeString()
		{
			T.E();
			if (CurrentFundGroup is AssetFundGroup)
			{
				T.X();
				return "Asset Fund";
			}
			else
			{
				T.X();
				return "Fund";
			}
		}

		#endregion Privates

		#region Public methods

		/// <summary>
		/// Used to determine whether the currently edited fund group is unknown (will only occur for new fund groups),
		/// asset funds, or funds
		/// </summary>
		public enum FundGroupType
		{
			/// <summary>
			/// for a new fund group a selection is yet to be made
			/// </summary>
			unknown,
			/// <summary>
			/// asset fund selected
			/// </summary>
			assetFund,
			/// <summary>
			/// fund selected
			/// </summary>
			fund
		}

		/// <summary>
		/// returns the fund group type selected in the ui (or unknown if a new fund group and user not yet selected)
		/// </summary>
		public FundGroupType CurrentFundGroupType
		{
			get
			{
				if (rdoAssetFund.Checked)
				{
					return FundGroupType.assetFund;
				}
				else if (rdoFund.Checked)
				{
					return FundGroupType.fund;
				}
				else
				{
					return FundGroupType.unknown;
				}
			}
		}

		/// <summary>
		/// Updates the fund group entity from the view.
		/// </summary>
		private bool updateFundGroup()
		{
			T.E();
			bool fundGroupParsedOK = true;
			if (Changed)
			{
				if (authoringMode == authoringModes.New)
				{
					if (CurrentFundGroup is NewFundGroup)
					{
						NewFundGroup newFundGroup = (NewFundGroup) CurrentFundGroup;
						currentFundGroup = newFundGroup.AttemptRealFundGroupConversion();
					}
				}

				this.CurrentFundGroup.ShortName = txtShortName.Text;
				this.CurrentFundGroup.FullName = txtFullName.Text;
				this.CurrentFundGroup.ForRelease = chkForRelease.Checked;
				this.CurrentFundGroup.AllowSelectAllAuthorisation = allowSelectAllAuthorisationChk.Checked;

				if (chkForRelease.Checked && listToList != null)
				{
					this.CurrentFundGroup.DistributionFiles.Clear();
					IList selected = listToList.SelectedItems();
					foreach (DistributionFile dFile in selected)
					{
						this.CurrentFundGroup.DistributionFiles.Add(dFile);
					}
				}

				this.CurrentFundGroup.IsDirty = true;
				this.Changed = false;
			}

			T.X();
			return fundGroupParsedOK;
		}

		/// <summary>
		/// display validation errors in the UI
		/// </summary>
		/// <param name="fullNameErrorMessage"></param>
		/// <param name="shortNameErrorMessage"></param>
		/// <param name="fundGroupTypeErrorMessage"></param>
		/// <param name="fundGroupNumberErrorMessage"></param>
		public void ShowErrors(string fullNameErrorMessage, string shortNameErrorMessage, string fundGroupTypeErrorMessage, string fundGroupNumberErrorMessage)
		{
			clearErrors();
			bool isValid = true;
			if (fullNameErrorMessage != null && fullNameErrorMessage.Length > 0)
			{
				txtFullName.Focus();
				isValid = false;
				setError(txtFullName, fullNameErrorMessage);
			}

			if (shortNameErrorMessage != null && shortNameErrorMessage.Length > 0)
			{
				if (isValid) //if focus not already determined
				{
					txtShortName.Focus();
				}
				isValid = false;
				setError(txtShortName, shortNameErrorMessage);
			}

			if (fundGroupTypeErrorMessage != null && fundGroupTypeErrorMessage.Length > 0)
			{
				if (isValid) //if focus not already determined
				{
					grpElements.Focus();
				}
				isValid = false;
				setError(grpElements, fundGroupTypeErrorMessage);
			}

			if (fundGroupNumberErrorMessage != null && fundGroupNumberErrorMessage.Length > 0)
			{
				if (isValid) //if focus not already determined
				{
					this.listToList.Focus();
				}
				isValid = false;
				setError(listToList, fundGroupNumberErrorMessage);
			}
			if (!isValid) showErrorDialog("This fund group cannot be saved:");

		}

		#endregion Public methods

		#region Event Handlers

		private void defaultControlChanged(object sender, EventArgs e)
		{
			change();
		}

		private void listToList_Changing(object sender, ListToListChangingArgs e)
		{
			if (e.ChangedItems !=null && e.ChangedItems.Count>0)
			{
				if (((DistributionFile)e.ChangedItems[0]).Status==DistributionFileStatuses.Distributed)
				{
					MessageBoxHelper.Show("FileIsDistributedBody","FileIsDistributedTitle",MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					e.Cancel=true;
				}
				else
				{
					change();
				}
			}
		}

		private void chkForRelease_Click(object sender, EventArgs e)
		{
			if (!m_systemGeneratedEvent)
			{
				if (chkForRelease.Checked)
				{
					allowSelectAllAuthorisationChk.Checked = false;
				}
				enableControls();
				createListToList();
				change();
			}
		}

		private void allowSelectAllAuthorisationChk_CheckedChanged(object sender, System.EventArgs e)
		{
			if (!m_systemGeneratedEvent)
			{
				change();		
			}
		}

		#endregion

		#region Overrides

		/// <summary>
		/// Gets the export parameters.
		/// </summary>
		/// <returns></returns>
		protected override void getExportParameters(StaticDataExportParameters parameters)
		{
			T.E();		
			parameters.CollectionToExport = fundGroupController.LoadFundGroupsByCompany(GlobalRegistry.CompanyCode);
			parameters.Exports.Add(new StaticDataExport("HBOS.FS.AMP.UPD.WinUI.Classes.FundGroupStaticData.xslt","fundgroups"));
			T.X();
		}

		/// <summary>
		/// Saves the FundGroup.
		/// </summary>
		/// <returns></returns>
		protected override bool doSave()
		{
			T.E();

			clearErrors();

			bool isValid = true;
			try
			{
				this.updateFundGroup();
				FundGroup currentFundGroup = this.CurrentFundGroup;

				currentFundGroup.CompanyCode = GlobalRegistry.CompanyCode;
				isValid = validateFundGroup(currentFundGroup);
				if (isValid)
				{
					fundGroupController.SaveFundGroup(this.CurrentFundGroup);
					ListManager.ChangeSelected(new SimpleLookup(CurrentFundGroup.ID,CurrentFundGroup.ShortName));
				}
			}
			catch (FundGroupFileAssociationException ex)
			{
				string fundGroupNumberErrorMessage=MessageBoxHelper.DialogText (ex.Message,new object[]{((FundGroupDistributionFile)ex.File).FileDescription});
				this.listToList.Focus();
				isValid = false;
				setError(listToList, fundGroupNumberErrorMessage);
				showErrorDialog("This fund group cannot be saved:");
			}
			catch
			{
				isValid = false;	
			}
			finally
			{
				T.X();
			}
			return isValid;
		}

		/// <summary>
		/// Deletes the fund group
		/// </summary>
		protected override void doDelete()
		{
			CurrentFundGroup.IsDeleted = true;
			fundGroupController.SaveFundGroup(CurrentFundGroup);
		}

		/// <summary>
		/// Indicates if ok to perform delete
		/// </summary>
		/// <returns></returns>
		protected override bool deleteValidation()
		{
			if (CurrentFundGroup.HasAssociatedFunds)
			{
				//TODO: replace with resource
				string msg = MessageBoxHelper.DialogText("FundGroupAssociationsBody", new object[] {fundGroupTypeString()});
				MessageBoxHelper.Show(msg, "FundGroupAssociationsTitle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			else
			{
				return base.deleteValidation();
			}
		}


		/// <summary>
		/// Start editing a brand new fund group
		/// </summary>
		protected override void doNew()
		{
			Changed = true;
			CurrentFundGroup = new NewFundGroup(this);
			txtFullName.Focus();
		}


		/// <summary>
		/// Specifies the entity type being edited
		/// </summary>
		/// <value></value>
		protected override string EditType
		{
			get
			{
				return "Fund Group";
			}
		}

		/// <summary>
		/// Loads the fund group from the database
		/// </summary>
		protected override void doLoadEntity()
		{
			T.E();
			CurrentFundGroup = fundGroupController.LoadFundGroupStaticData((SimpleLookup)ListManager.SelectedItem);
			T.X();
		}

		#endregion

		#region New Fund Group Special Case class

		private class NewFundGroup : FundGroup
		{
			public NewFundGroup(FundGroupStaticDataEditor parent) : base()
			{
				this.parent = parent;
			}

			private FundGroupStaticDataEditor parent;

			public FundGroup AttemptRealFundGroupConversion()
			{		
				FundGroup result = null;

				if (parent.rdoAssetFund.Checked)
				{
					result = FundGroupFactory.CreateFundGroup(FundGroupFactory.FundGroupTypes.Asset);
				}
				else if (parent.rdoFund.Checked)
				{
					result = FundGroupFactory.CreateFundGroup(FundGroupFactory.FundGroupTypes.Individual);
				}
				else
				{
					//cannot convert, so return the same NewFund object and let client do checks
					//to see if converted ok or not
					result = this;
				}

				result.IsNew      = true;
				result.IsDeleted  = false;
				return result;
			}
		}

		#endregion
	}
}