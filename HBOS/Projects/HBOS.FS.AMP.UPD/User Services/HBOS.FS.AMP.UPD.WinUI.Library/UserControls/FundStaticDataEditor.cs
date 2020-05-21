using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.FundGroups;
using HBOS.FS.AMP.UPD.Types.Funds;
using HBOS.FS.AMP.UPD.Types.Lookups;
using HBOS.FS.AMP.UPD.WinUI.Classes;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.UserControls
{
	/// <summary>
	/// Summary description for FundStaticDataEditor.
	/// </summary>
	public class FundStaticDataEditor : StaticDataEditor
	{
		#region Controls

		private TabControl tabs;
		private TabPage propertiesTab;
		private CheckBox lifeCheck;
		private CheckBox midPriceCheck;
		private CheckBox exDivCheck;
		private CheckBox hi3FundCheck;
		private ComboBox fundTypeCombo;
		private TextBox shareClassText;
		private Label fundTypeLabel;
		private Label shareClassLabel;
		private Label hiPortfolioCodeLable;
		private Label shortNameLabel;
		private Label fullNameLabel;
		private TabPage associationsTab;
		private GroupBox fundGroupGroupBox;
		private Label label5;
		private Label associationsTabAssetFundLabel;
		private TabPage factorsTab;
		private GroupBox scaleFactorGroupBox;
		private Label label8;
		private TextBox scaleFactorText;
		private GroupBox revalFactorGroupBox;
		private Label label4;
		private TextBox revalFactorText;
		private GroupBox tpeFactorGroupBox;
		private Label label3;
		private TextBox tpeFactorText;
		private GroupBox xFactorGroupBox;
		private TextBox xFactorNarrTextBox;
		private Label label9;
		private Label label7;
		private TextBox xFactorText;
		private TabPage tolerancesTab;
		private CheckBox priceIncCheck;
		private Label priceIncLabel;
		private TextBox upperTolText;
		private TextBox lowerTolText;
		private TabPage codesTab;
		private GroupBox codesGroupBox;
		private Label label10;
		private GroupBox assetFundGroupBox;
		private Label assetFundTolLabel;
		private DateTimePicker revalFactorEndDate;
		private Label label11;
		private IContainer components = null;

		private TextBox shortNameText;
		private TextBox fullNameText;
		private TextBox hiPortCodeText;

		#endregion

		#region Privates

		private CheckBox dualPriceCheck;
		private ComboBox assetFundsCombo;
		private Label labAssetFundGroups;
		private ListBox lstAssetFundGroups;
		private Panel pnlFundFundGroups;
		private Panel pnlExternalSystemIds;
		private Label lRevalFactor1stDay;
		private TextBox revalFactor1stDayText;
		private Label lFactorWarning;
		private TextBox assetMovementText;
		private Label lToleranceWarning;
		private Panel pnlNewFund;
		private Label upperToleranceLabel;
		private Label lowerToleranceLabel;
		private TextBox securityCodeText;
		private Label securityCodeLabel;
		private viewFormatter formatter;
		private bool m_eventsAreSystemGenerated = false; // a flag to indicate controls are being set by system not by a user event

		private FundController fundController = new FundController(GlobalRegistry.ConnectionString);
		private CheckBox benchmarkableCheck;
		private FundGroupController fundGroupController = new FundGroupController(GlobalRegistry.ConnectionString);

		private authoringModes authoringMode
		{
			get
			{
				T.E();
				authoringModes result;
				if (CurrentFund == null)
				{
					result = authoringModes.Null;
				}
				else if (ListManager.SelectedIsNew)
				{
					result = authoringModes.New;
				}
				else
				{
					result = authoringModes.Editing;
				}

				T.X();

				return result;
			}
		}

		private void change()
		{
			if (!m_eventsAreSystemGenerated)
			{
				//We now update the object following every change which is a move from the original model.
				//This is because the business rules check the object to see what can be enabled or disabled,
				//so was checking out of date information.
				UpdateFund();

				Changed = true;
			}
		}

		private void displayFundProperties()
		{
			T.E();
			SuspendLayout();
			try
			{
				formatter.DisplayFundProperties();
				formatter.EnableControls();

				Changed = false;
			}
			finally
			{
				ResumeLayout();
			}
			T.X();
		}

		private void fillAssetFundGroups()
		{
			T.E();
			lstAssetFundGroups.DataSource = null;
			lstAssetFundGroups.Items.Clear();

			if (assetFundsCombo.SelectedValue != null)
			{
				IList fundGroups = fundGroupController.LoadFundGroupLookupsByAssetFund(assetFundsCombo.SelectedValue.ToString());
				if (fundGroups != null)
				{
					lstAssetFundGroups.DataSource = fundGroups;
				}
			}
			T.X();
		}

		private ListToListControl fundFundGroupsListToList = null;
		private ListToListControl externalSystemIDsListToList = null;

		private IList m_externalSystemIDs = null;

		private IList externalSystemIDs
		{
			get
			{
				if (m_externalSystemIDs == null)
				{
					IList extIds = LookupController.LoadSystems(GlobalRegistry.ConnectionString,GlobalRegistry.CompanyCode);

					if (extIds == null)
						m_externalSystemIDs = new object[0];
					else
						m_externalSystemIDs = extIds;
				}

				return m_externalSystemIDs;
			}
		}

		private void createFundGroupListToList(IList allFundGroups, IList selectedFundGroups)
		{
			T.E();
			try
			{
				fundFundGroupsListToList = new ListToListControl();
				fundFundGroupsListToList.Dock = DockStyle.Fill;
				fundFundGroupsListToList.Parent = pnlFundFundGroups;
				fundFundGroupsListToList.AddingSelectedItems += new ListToListChangingHandler(listToListDefaultChanging);
				fundFundGroupsListToList.RemovingSelectedItems += new ListToListChangingHandler(listToListDefaultChanging);
				fundFundGroupsListToList.SetColumnsAndLists(allFundGroups, selectedFundGroups);
			}
			finally
			{
			}
		}

		private void fillFundFundGroups()
		{
			T.E();

			IList fundGroups = fundGroupController.LoadFundGroupsByCompanyAndType(GlobalRegistry.CompanyCode, FundGroupFactory.FundGroupTypes.Individual);

			if (fundGroups != null && fundGroups.Count > 0)
			{
				if (fundFundGroupsListToList == null)
				{
					createFundGroupListToList(fundGroups, filterByType(CurrentFund.FundGroups,FundGroupFactory.FundGroupTypes.Individual));
				}
				else
				{
					fundFundGroupsListToList.ResetLists(fundGroups, filterByType(CurrentFund.FundGroups,FundGroupFactory.FundGroupTypes.Individual));
				}
			}
			else
			{
				if (fundFundGroupsListToList != null)
				{
					fundFundGroupsListToList.ClearLists();
				}
			}

			T.X();
		}


		/// <summary>
		/// Filters the collection of fundgroupps by type (Asset or Individual).
		/// </summary>
		/// <param name="fundGroupCollection">Fund group collection.</param>
		/// <param name="type">Type.</param>
		/// <returns></returns>
		protected FundGroupCollection filterByType(FundGroupCollection fundGroupCollection, FundGroupFactory.FundGroupTypes type)
		{
			FundGroupCollection returnCollection=new FundGroupCollection();
			foreach (FundGroup fundGroup in fundGroupCollection)
			{
				// Amended by MAW 14/09/200 05 (Issue: UA131)
				// The previous switch case statement always added the fundgroup to the returning collection
				// no matter what you asked for!
				switch (type)
				{
					case FundGroupFactory.FundGroupTypes.Asset:

						if(fundGroup is AssetFundGroup)
						{
							returnCollection.Add(fundGroup);
						}
						break;
					case FundGroupFactory.FundGroupTypes.Individual:
						if(fundGroup is IndividualFundGroup)
						{
							returnCollection.Add(fundGroup);
						}
						break;
				}
			}
		
			return returnCollection;
		}

		private static bool safeDecimalParse(string value, out decimal result)
		{
			T.E();
			bool parseOk = true;
			try
			{
				if (value == string.Empty)
					result = 0;
				else
					result = Decimal.Parse(value);
			}
			catch (ArgumentNullException)
			{
				result = 0;
			}
			catch (FormatException)
			{
				result = 0;
				parseOk = false;
			}
			catch (OverflowException)
			{
				result = 0;
				parseOk = false;
			}
			T.X();
			return parseOk;
		}

		/*
				private static decimal safeDecimalParse(string value)
				{
					decimal result;
					if (!safeDecimalParse(value, out result))
						throw new InvalidCastException(string.Format("Cannot convert value '{0}' to a decimal", value));
					return result;
				}
		*/

		private const string toleranceFormatString = "###0.0000";

		private void generateHiPortCode()
		{
			T.E();
			try
			{
				hiPortCodeText.Text = fundController.GenerateHiPortfolioCode(CurrentFund);
				CurrentFund.HiPortfolioCode = hiPortCodeText.Text;
			}
			finally
			{
				T.X();
			}
		}

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor for <see cref="FundStaticDataEditor" />
		/// </summary>
		public FundStaticDataEditor()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

		#endregion

		#region Properties

		private Fund currentFund;

		/// <summary>
		/// Gets or sets the current fund being edited.
		/// </summary>
		/// <value></value>
		public Fund CurrentFund
		{
			get { return currentFund; }

			set
			{
				SuspendLayout();
				try
				{
					//set a flag to say ignore any events being fired by control update,
					//as it is system setting of these controls causing the event fire, 
					//not a user event
					m_eventsAreSystemGenerated = true;
					if (currentFund != null && currentFund.HiPortfolioCode != value.HiPortfolioCode)
					{
						tabs.SelectedTab = propertiesTab; //as requested by Richard (analyst)
					}
					currentFund = value;
					clearErrors(tabs);
					formatter = viewFormatterFactory.CreateViewFormatter(this);
					displayFundProperties();
					m_eventsAreSystemGenerated = false;
				}
				finally
				{
					ResumeLayout();
				}
			}
		}

		/// <summary>
		/// Gets the description of the current fund.
		/// </summary>
		/// <value></value>
		protected override string currentEntityDescription
		{
			get { return CurrentFund.FullName; }
		}

		#endregion

		#region Methods

		/// <summary>
		/// creates new list to list if necessary, then sets columns or updates lists as necessary
		/// </summary>
		public void UpdateExternalSystemIDsListToList()
		{
			T.E();
			try
			{
				if (externalSystemIDsListToList == null)
				{
					externalSystemIDsListToList = new ListToListControl();
					externalSystemIDsListToList.View = ListViewMode.ListToGrid;
					externalSystemIDsListToList.Dock = DockStyle.Fill;
					externalSystemIDsListToList.Parent = pnlExternalSystemIds;
					externalSystemIDsListToList.AddingSelectedItems += new ListToListChangingHandler(listToListDefaultChanging);
					externalSystemIDsListToList.RemovingSelectedItems += new ListToListChangingHandler(listToListDefaultChanging);
					externalSystemIDsListToList.EditingSelectedItem += new ListToListEditedHandler(externalSystemIDsListToList_EditingSelectedItem);
					externalSystemIDsListToList.UnselectedColumns.Add("SystemName");
					externalSystemIDsListToList.SelectedColumns.Add("FundCode", "Fund Code", 75);
					externalSystemIDsListToList.SelectedColumns.Add("SystemName", "System Name", 200);
					externalSystemIDsListToList.SetColumnsAndLists(externalSystemIDs, CurrentFund.SystemIDs);
				}
				else
				{
					externalSystemIDsListToList.ResetLists(externalSystemIDs, CurrentFund.SystemIDs);
				}
			}
			finally
			{
				T.X();
			}

		}

		private Control resolveControlFromError(FundController.FundValidationError validationError)
		{
			Control returnControl=null;
			switch (validationError)
			{
				case FundController.FundValidationError.FundCannotUnSelectBenchMark :
					returnControl =this.benchmarkableCheck;
					break;
				case FundController.FundValidationError.FundFieldEmptyHiPortfolioCode :
					goto case FundController.FundValidationError.FundDuplicateFieldHiPortCode;
				case FundController.FundValidationError.FundInvalidCodeClassOrPriceSeries :
					returnControl =shareClassText;
					break;
				case FundController.FundValidationError.FundDuplicateFieldHiPortCode:
					returnControl =hiPortCodeText;
					break;
				case FundController.FundValidationError.FundDuplicateFieldSecurityCode:
					returnControl =securityCodeText;
					break;
				case FundController.FundValidationError.FundFieldEmptyFullName :
					goto case FundController.FundValidationError.FundDuplicateFieldFullName;
				case FundController.FundValidationError.FundDuplicateFieldFullName:
					returnControl =fullNameText;
					break;
				case FundController.FundValidationError.FundFieldEmptyShortName :
					goto case FundController.FundValidationError.FundDuplicateFieldShortName;
				case FundController.FundValidationError.FundDuplicateFieldShortName:
					returnControl =shortNameText;
					break;
				case FundController.FundValidationError.FundTypeNotSelected:
					returnControl =fundTypeCombo;
					break;
				case FundController.FundValidationError.FundFieldEmptyAssetFundID:
					returnControl =assetFundsCombo;
					break;
				case FundController.FundValidationError.FundFieldEmptyClassOrPriceSeries:
					returnControl =shareClassText;
					break;
				case FundController.FundValidationError.FundFieldEmptyExternalSystemId:
					returnControl =pnlExternalSystemIds;
					break;
				case FundController.FundValidationError.FundXFactorInvalid:
					returnControl =xFactorText;
					break;
				case FundController.FundValidationError.FundTPEInvalid:
					returnControl =tpeFactorText;
					break;
				case FundController.FundValidationError.FundRevaluationFactorInvalid:
					returnControl =revalFactorText;
					break;
				case FundController.FundValidationError.FundScalingFactorInvalid:
					returnControl =scaleFactorText;
					break;
				case FundController.FundValidationError.FundMaxLowerToleranceExceeded:
					goto case FundController.FundValidationError.FundInvalidNumDecimalPlacesLowerTolerance;
				case FundController.FundValidationError.FundLowerToleranceNegative:
					goto case FundController.FundValidationError.FundInvalidNumDecimalPlacesLowerTolerance;
				case FundController.FundValidationError.FundLowerToleranceZero:
					goto case FundController.FundValidationError.FundInvalidNumDecimalPlacesLowerTolerance;
				case FundController.FundValidationError.FundInvalidNumDecimalPlacesLowerTolerance:
					returnControl =lowerTolText;
					break;
				case FundController.FundValidationError.FundMaxUpperToleranceExceeded:
					goto case FundController.FundValidationError.FundInvalidNumDecimalPlacesUpperTolerance;
				case FundController.FundValidationError.FundUpperToleranceNegative:
					goto case FundController.FundValidationError.FundInvalidNumDecimalPlacesUpperTolerance;
				case FundController.FundValidationError.FundUpperToleranceZero:
					goto case FundController.FundValidationError.FundInvalidNumDecimalPlacesUpperTolerance;
				case FundController.FundValidationError.FundInvalidNumDecimalPlacesUpperTolerance:
					returnControl =upperTolText;
					break;
				case FundController.FundValidationError.FundUpperToleranceLessThanLowerTolerance:
					returnControl =lowerTolText;
					break;
			}
			return returnControl;
		}

		/// <summary>
		/// Validates the fund is ready for saving.
		/// </summary>
		/// <returns></returns>
		private void showValidationErrors(FundController.FundValidationError[] errors)
		{
			T.E();

			clearErrors(tabs);

			bool focusSet=false;
			foreach (FundController.FundValidationError fundValidationError in errors)
			{
				Control focusControl=resolveControlFromError(fundValidationError);
				if (!focusSet)
				{
					AssetFundStaticDataEditor.SetFocusToErrorControl(focusControl);
					focusSet=true;
				}
				setError(focusControl, MessageBoxHelper.DialogText(fundValidationError.ToString()));
			}

			if (errors.Length>0) showErrorDialog(MessageBoxHelper.DialogText("FundCannotSave"));

			T.X();
		}

		/// <summary>
		/// Updates the CurrentFund from the data in the controls.
		/// </summary>
		public void UpdateFund(out bool xFactorParsedOK, out bool tpeParsedOK, out bool revalFactorParsedOK, out bool scalingFactorParsedOK, out bool upperToleranceParsedOK, out bool lowerToleranceParsedOK)
		{
			T.E();
			formatter.UpdateFund(out xFactorParsedOK, out tpeParsedOK, out revalFactorParsedOK, out scalingFactorParsedOK, out upperToleranceParsedOK, out lowerToleranceParsedOK);
			T.X();
		}

		/// <summary>
		/// Updates the CurrentFund from the data in the controls without advising of parse failure.
		/// </summary>
		public void UpdateFund()
		{
			T.E();
			bool xFactorParsedOK;
			bool tpeParsedOK;
			bool revalFactorParsedOK;
			bool scalingFactorParsedOK;
			bool upperToleranceParsedOK;
			bool lowerToleranceParsedOK;
			formatter.UpdateFund(out xFactorParsedOK, out tpeParsedOK, out revalFactorParsedOK, out scalingFactorParsedOK, out upperToleranceParsedOK, out lowerToleranceParsedOK);
			T.X();
		}

		/// <summary>
		/// Cancels the changes in the widgets and sets them back to the values from the fund.
		/// </summary>
		public void Cancel()
		{
			T.E();
			displayFundProperties();
			T.X();
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
			parameters.CollectionToExport = FundStaticDataExportFundDecorator.ToDecoratedList(fundController.LoadFundsByCompany(GlobalRegistry.CompanyCode)) ;
			parameters.Exports.Add(new StaticDataExport("HBOS.FS.AMP.UPD.WinUI.Classes.FundStaticDataFundGroups.xslt", "funds_fundgroupmembership"));
			parameters.Exports.Add(new StaticDataExport("HBOS.FS.AMP.UPD.WinUI.Classes.FundStaticDataSystems.xslt", "funds_systems"));
			T.X();
		}


		/// <summary>
		/// Loads the fund into the editor controls
		/// </summary>
		protected override void doLoadEntity()
		{
			T.E();
			string fundCode = ((SimpleStringLookup) ListManager.SelectedItem).Key;
			CurrentFund = fundController.LoadStaticData(fundCode);
			T.X();
		}

		/// <summary>
		/// Does the delete of the fund.
		/// </summary>
		protected override void doDelete()
		{
			CurrentFund.IsDeleted = true;
			fundController.UpdateStaticData(CurrentFund);
		}

		/// <summary>
		/// Saves the fund
		/// </summary>
		/// <returns></returns>
		protected override bool doSave()
		{
			T.E();
			bool isValid = true;
			try
			{
				clearErrors(tabs);

				bool xFactorParsedOK;
				bool tpeParsedOK;
				bool revalFactorParsedOK;
				bool scalingFactorParsedOk;
				bool upperToleranceParsedOk;
				bool lowerToleranceParsedOk;

				UpdateFund(out xFactorParsedOK, out tpeParsedOK, out revalFactorParsedOK, out scalingFactorParsedOk, out upperToleranceParsedOk, out lowerToleranceParsedOk);

				FundController.FundValidationError[] errors=fundController.ValidateFund(CurrentFund);

				ArrayList errorsList=new ArrayList(errors);
				if (!xFactorParsedOK) errorsList.Add(FundController.FundValidationError.FundXFactorInvalidNumber);
				if (!tpeParsedOK) errorsList.Add(FundController.FundValidationError.FundTPEInvalidNumber);
				if (!revalFactorParsedOK) errorsList.Add(FundController.FundValidationError.FundRevaluationFactorInvalidNumber);
				if (!scalingFactorParsedOk) errorsList.Add(FundController.FundValidationError.FundScalingFactorInvalidNumber);
				if (!upperToleranceParsedOk) errorsList.Add(FundController.FundValidationError.FundUpperToleranceInvalidNumber);
				if (!lowerToleranceParsedOk) errorsList.Add(FundController.FundValidationError.FundLowerToleranceInvalidNumber);
	
					
				errors= (FundController.FundValidationError[]) errorsList.ToArray(typeof (FundController.FundValidationError));

				isValid = errors.Length==0 && xFactorParsedOK && tpeParsedOK && revalFactorParsedOK && scalingFactorParsedOk;

				if (isValid)
				{
					fundController.UpdateStaticData(CurrentFund);
					Changed = false;
					ListManager.ChangeSelected(new SimpleStringLookup(CurrentFund.HiPortfolioCode, CurrentFund.ShortName));
				}
				else
				{
					showValidationErrors(errors);
				}

			}
			finally
			{
				T.X();
			}
			return isValid;
		}

		/// <summary>
		/// Prepares the editor for the creation of a new fund
		/// </summary>
		protected override void doNew()
		{
			T.E();
			try
			{
				fundTypeCombo.SelectedIndex = -1;
				CurrentFund = new newFund();
				formatter.FillAssetFundsCombo(CurrentFund); //this should clear the combo
				fillFundFundGroups();
				fullNameText.Focus();
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Specifies the entity type being edited
		/// </summary>
		/// <value></value>
		protected override string EditType
		{
			get { return "Fund"; }
		}

		#endregion

		#region Dispose

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
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
			this.tabs = new System.Windows.Forms.TabControl();
			this.propertiesTab = new System.Windows.Forms.TabPage();
			this.benchmarkableCheck = new System.Windows.Forms.CheckBox();
			this.pnlNewFund = new System.Windows.Forms.Panel();
			this.securityCodeLabel = new System.Windows.Forms.Label();
			this.securityCodeText = new System.Windows.Forms.TextBox();
			this.shortNameText = new System.Windows.Forms.TextBox();
			this.fundTypeLabel = new System.Windows.Forms.Label();
			this.fullNameLabel = new System.Windows.Forms.Label();
			this.fullNameText = new System.Windows.Forms.TextBox();
			this.shareClassLabel = new System.Windows.Forms.Label();
			this.shortNameLabel = new System.Windows.Forms.Label();
			this.hiPortfolioCodeLable = new System.Windows.Forms.Label();
			this.hiPortCodeText = new System.Windows.Forms.TextBox();
			this.fundTypeCombo = new System.Windows.Forms.ComboBox();
			this.shareClassText = new System.Windows.Forms.TextBox();
			this.dualPriceCheck = new System.Windows.Forms.CheckBox();
			this.lifeCheck = new System.Windows.Forms.CheckBox();
			this.midPriceCheck = new System.Windows.Forms.CheckBox();
			this.exDivCheck = new System.Windows.Forms.CheckBox();
			this.hi3FundCheck = new System.Windows.Forms.CheckBox();
			this.associationsTab = new System.Windows.Forms.TabPage();
			this.fundGroupGroupBox = new System.Windows.Forms.GroupBox();
			this.pnlFundFundGroups = new System.Windows.Forms.Panel();
			this.label5 = new System.Windows.Forms.Label();
			this.assetFundGroupBox = new System.Windows.Forms.GroupBox();
			this.lstAssetFundGroups = new System.Windows.Forms.ListBox();
			this.labAssetFundGroups = new System.Windows.Forms.Label();
			this.assetFundsCombo = new System.Windows.Forms.ComboBox();
			this.associationsTabAssetFundLabel = new System.Windows.Forms.Label();
			this.factorsTab = new System.Windows.Forms.TabPage();
			this.lFactorWarning = new System.Windows.Forms.Label();
			this.scaleFactorGroupBox = new System.Windows.Forms.GroupBox();
			this.label8 = new System.Windows.Forms.Label();
			this.scaleFactorText = new System.Windows.Forms.TextBox();
			this.revalFactorGroupBox = new System.Windows.Forms.GroupBox();
			this.revalFactorEndDate = new System.Windows.Forms.DateTimePicker();
			this.label11 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.revalFactorText = new System.Windows.Forms.TextBox();
			this.lRevalFactor1stDay = new System.Windows.Forms.Label();
			this.revalFactor1stDayText = new System.Windows.Forms.TextBox();
			this.tpeFactorGroupBox = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.tpeFactorText = new System.Windows.Forms.TextBox();
			this.xFactorGroupBox = new System.Windows.Forms.GroupBox();
			this.xFactorNarrTextBox = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.xFactorText = new System.Windows.Forms.TextBox();
			this.tolerancesTab = new System.Windows.Forms.TabPage();
			this.lToleranceWarning = new System.Windows.Forms.Label();
			this.priceIncCheck = new System.Windows.Forms.CheckBox();
			this.priceIncLabel = new System.Windows.Forms.Label();
			this.assetFundTolLabel = new System.Windows.Forms.Label();
			this.assetMovementText = new System.Windows.Forms.TextBox();
			this.upperToleranceLabel = new System.Windows.Forms.Label();
			this.upperTolText = new System.Windows.Forms.TextBox();
			this.lowerToleranceLabel = new System.Windows.Forms.Label();
			this.lowerTolText = new System.Windows.Forms.TextBox();
			this.codesTab = new System.Windows.Forms.TabPage();
			this.codesGroupBox = new System.Windows.Forms.GroupBox();
			this.pnlExternalSystemIds = new System.Windows.Forms.Panel();
			this.label10 = new System.Windows.Forms.Label();
			this.tabs.SuspendLayout();
			this.propertiesTab.SuspendLayout();
			this.pnlNewFund.SuspendLayout();
			this.associationsTab.SuspendLayout();
			this.fundGroupGroupBox.SuspendLayout();
			this.assetFundGroupBox.SuspendLayout();
			this.factorsTab.SuspendLayout();
			this.scaleFactorGroupBox.SuspendLayout();
			this.revalFactorGroupBox.SuspendLayout();
			this.tpeFactorGroupBox.SuspendLayout();
			this.xFactorGroupBox.SuspendLayout();
			this.tolerancesTab.SuspendLayout();
			this.codesTab.SuspendLayout();
			this.codesGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabs
			// 
			this.tabs.Controls.Add(this.propertiesTab);
			this.tabs.Controls.Add(this.associationsTab);
			this.tabs.Controls.Add(this.factorsTab);
			this.tabs.Controls.Add(this.tolerancesTab);
			this.tabs.Controls.Add(this.codesTab);
			this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabs.Location = new System.Drawing.Point(0, 0);
			this.tabs.Name = "tabs";
			this.tabs.Padding = new System.Drawing.Point(8, 3);
			this.tabs.SelectedIndex = 0;
			this.tabs.Size = new System.Drawing.Size(752, 504);
			this.tabs.TabIndex = 0;
			// 
			// propertiesTab
			// 
			this.propertiesTab.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.propertiesTab.Controls.Add(this.benchmarkableCheck);
			this.propertiesTab.Controls.Add(this.pnlNewFund);
			this.propertiesTab.Controls.Add(this.dualPriceCheck);
			this.propertiesTab.Controls.Add(this.lifeCheck);
			this.propertiesTab.Controls.Add(this.midPriceCheck);
			this.propertiesTab.Controls.Add(this.exDivCheck);
			this.propertiesTab.Controls.Add(this.hi3FundCheck);
			this.propertiesTab.Location = new System.Drawing.Point(4, 25);
			this.propertiesTab.Name = "propertiesTab";
			this.propertiesTab.Size = new System.Drawing.Size(744, 475);
			this.propertiesTab.TabIndex = 0;
			this.propertiesTab.Text = "Properties";
			// 
			// benchmarkableCheck
			// 
			this.benchmarkableCheck.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.benchmarkableCheck.Location = new System.Drawing.Point(16, 360);
			this.benchmarkableCheck.Name = "benchmarkableCheck";
			this.benchmarkableCheck.Size = new System.Drawing.Size(182, 22);
			this.benchmarkableCheck.TabIndex = 317;
			this.benchmarkableCheck.Text = "Benchmarkable";
			this.benchmarkableCheck.CheckedChanged += new System.EventHandler(this.benchmarkAbleCheck_CheckedChanged);
			// 
			// pnlNewFund
			// 
			this.pnlNewFund.Controls.Add(this.securityCodeLabel);
			this.pnlNewFund.Controls.Add(this.securityCodeText);
			this.pnlNewFund.Controls.Add(this.shortNameText);
			this.pnlNewFund.Controls.Add(this.fundTypeLabel);
			this.pnlNewFund.Controls.Add(this.fullNameLabel);
			this.pnlNewFund.Controls.Add(this.fullNameText);
			this.pnlNewFund.Controls.Add(this.shareClassLabel);
			this.pnlNewFund.Controls.Add(this.shortNameLabel);
			this.pnlNewFund.Controls.Add(this.hiPortfolioCodeLable);
			this.pnlNewFund.Controls.Add(this.hiPortCodeText);
			this.pnlNewFund.Controls.Add(this.fundTypeCombo);
			this.pnlNewFund.Controls.Add(this.shareClassText);
			this.pnlNewFund.Location = new System.Drawing.Point(16, 0);
			this.pnlNewFund.Name = "pnlNewFund";
			this.pnlNewFund.Size = new System.Drawing.Size(552, 200);
			this.pnlNewFund.TabIndex = 316;
			// 
			// securityCodeLabel
			// 
			this.securityCodeLabel.AutoSize = true;
			this.securityCodeLabel.ForeColor = System.Drawing.Color.Black;
			this.securityCodeLabel.Location = new System.Drawing.Point(0, 170);
			this.securityCodeLabel.Name = "securityCodeLabel";
			this.securityCodeLabel.Size = new System.Drawing.Size(89, 18);
			this.securityCodeLabel.TabIndex = 305;
			this.securityCodeLabel.Text = "Security Code";
			// 
			// securityCodeText
			// 
			this.securityCodeText.Location = new System.Drawing.Point(168, 168);
			this.securityCodeText.MaxLength = 8;
			this.securityCodeText.Name = "securityCodeText";
			this.securityCodeText.Size = new System.Drawing.Size(116, 22);
			this.securityCodeText.TabIndex = 304;
			this.securityCodeText.Text = "";
			this.securityCodeText.TextChanged += new System.EventHandler(this.defaultChangeHandler);
			// 
			// shortNameText
			// 
			this.shortNameText.Location = new System.Drawing.Point(168, 72);
			this.shortNameText.MaxLength = 50;
			this.shortNameText.Name = "shortNameText";
			this.shortNameText.Size = new System.Drawing.Size(232, 22);
			this.shortNameText.TabIndex = 3;
			this.shortNameText.Text = "";
			this.shortNameText.TextChanged += new System.EventHandler(this.defaultChangeHandler);
			// 
			// fundTypeLabel
			// 
			this.fundTypeLabel.AutoSize = true;
			this.fundTypeLabel.ForeColor = System.Drawing.Color.Black;
			this.fundTypeLabel.Location = new System.Drawing.Point(0, 136);
			this.fundTypeLabel.Name = "fundTypeLabel";
			this.fundTypeLabel.Size = new System.Drawing.Size(69, 18);
			this.fundTypeLabel.TabIndex = 303;
			this.fundTypeLabel.Text = "Fund Type";
			// 
			// fullNameLabel
			// 
			this.fullNameLabel.AutoSize = true;
			this.fullNameLabel.ForeColor = System.Drawing.Color.Black;
			this.fullNameLabel.Location = new System.Drawing.Point(0, 40);
			this.fullNameLabel.Name = "fullNameLabel";
			this.fullNameLabel.Size = new System.Drawing.Size(66, 18);
			this.fullNameLabel.TabIndex = 299;
			this.fullNameLabel.Text = "Full Name";
			// 
			// fullNameText
			// 
			this.fullNameText.Location = new System.Drawing.Point(168, 40);
			this.fullNameText.MaxLength = 100;
			this.fullNameText.Name = "fullNameText";
			this.fullNameText.Size = new System.Drawing.Size(348, 22);
			this.fullNameText.TabIndex = 2;
			this.fullNameText.Text = "";
			this.fullNameText.TextChanged += new System.EventHandler(this.defaultChangeHandler);
			// 
			// shareClassLabel
			// 
			this.shareClassLabel.AutoSize = true;
			this.shareClassLabel.ForeColor = System.Drawing.Color.Black;
			this.shareClassLabel.Location = new System.Drawing.Point(0, 104);
			this.shareClassLabel.Name = "shareClassLabel";
			this.shareClassLabel.Size = new System.Drawing.Size(161, 18);
			this.shareClassLabel.TabIndex = 302;
			this.shareClassLabel.Text = "Share Class / Price Series";
			// 
			// shortNameLabel
			// 
			this.shortNameLabel.AutoSize = true;
			this.shortNameLabel.ForeColor = System.Drawing.Color.Black;
			this.shortNameLabel.Location = new System.Drawing.Point(0, 72);
			this.shortNameLabel.Name = "shortNameLabel";
			this.shortNameLabel.Size = new System.Drawing.Size(76, 18);
			this.shortNameLabel.TabIndex = 300;
			this.shortNameLabel.Text = "Short Name";
			// 
			// hiPortfolioCodeLable
			// 
			this.hiPortfolioCodeLable.AutoSize = true;
			this.hiPortfolioCodeLable.ForeColor = System.Drawing.Color.Black;
			this.hiPortfolioCodeLable.Location = new System.Drawing.Point(0, 8);
			this.hiPortfolioCodeLable.Name = "hiPortfolioCodeLable";
			this.hiPortfolioCodeLable.Size = new System.Drawing.Size(78, 18);
			this.hiPortfolioCodeLable.TabIndex = 301;
			this.hiPortfolioCodeLable.Text = "HiPort Code";
			// 
			// hiPortCodeText
			// 
			this.hiPortCodeText.Location = new System.Drawing.Point(168, 8);
			this.hiPortCodeText.MaxLength = 10;
			this.hiPortCodeText.Name = "hiPortCodeText";
			this.hiPortCodeText.ReadOnly = true;
			this.hiPortCodeText.Size = new System.Drawing.Size(116, 22);
			this.hiPortCodeText.TabIndex = 1;
			this.hiPortCodeText.Text = "";
			// 
			// fundTypeCombo
			// 
			this.fundTypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.fundTypeCombo.Items.AddRange(new object[] {
															   "Composite",
															   "Linked",
															   "OEIC"});
			this.fundTypeCombo.Location = new System.Drawing.Point(168, 136);
			this.fundTypeCombo.Name = "fundTypeCombo";
			this.fundTypeCombo.Size = new System.Drawing.Size(116, 24);
			this.fundTypeCombo.TabIndex = 5;
			this.fundTypeCombo.SelectedIndexChanged += new System.EventHandler(this.fundTypeCombo_SelectedIndexChanged);
			// 
			// shareClassText
			// 
			this.shareClassText.Location = new System.Drawing.Point(168, 104);
			this.shareClassText.MaxLength = 2;
			this.shareClassText.Name = "shareClassText";
			this.shareClassText.Size = new System.Drawing.Size(58, 22);
			this.shareClassText.TabIndex = 4;
			this.shareClassText.Text = "";
			this.shareClassText.TextChanged += new System.EventHandler(this.shareClassText_Changed);
			// 
			// dualPriceCheck
			// 
			this.dualPriceCheck.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.dualPriceCheck.Location = new System.Drawing.Point(16, 232);
			this.dualPriceCheck.Name = "dualPriceCheck";
			this.dualPriceCheck.Size = new System.Drawing.Size(182, 22);
			this.dualPriceCheck.TabIndex = 8;
			this.dualPriceCheck.Text = "Dual Price";
			this.dualPriceCheck.CheckedChanged += new System.EventHandler(this.dualPriceCheck_CheckedChanged);
			// 
			// lifeCheck
			// 
			this.lifeCheck.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lifeCheck.Location = new System.Drawing.Point(16, 328);
			this.lifeCheck.Name = "lifeCheck";
			this.lifeCheck.Size = new System.Drawing.Size(182, 22);
			this.lifeCheck.TabIndex = 11;
			this.lifeCheck.Text = "Life (non-OEICs only)";
			this.lifeCheck.CheckedChanged += new System.EventHandler(this.lifeCheck_CheckedChanged);
			// 
			// midPriceCheck
			// 
			this.midPriceCheck.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.midPriceCheck.Enabled = false;
			this.midPriceCheck.Location = new System.Drawing.Point(16, 264);
			this.midPriceCheck.Name = "midPriceCheck";
			this.midPriceCheck.Size = new System.Drawing.Size(182, 22);
			this.midPriceCheck.TabIndex = 9;
			this.midPriceCheck.Text = "Mid Price is Bid Price";
			this.midPriceCheck.CheckedChanged += new System.EventHandler(this.defaultChangeHandler);
			// 
			// exDivCheck
			// 
			this.exDivCheck.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.exDivCheck.Location = new System.Drawing.Point(16, 296);
			this.exDivCheck.Name = "exDivCheck";
			this.exDivCheck.Size = new System.Drawing.Size(182, 22);
			this.exDivCheck.TabIndex = 10;
			this.exDivCheck.Text = "Ex-Dividend (OEICs only)";
			this.exDivCheck.CheckedChanged += new System.EventHandler(this.defaultChangeHandler);
			// 
			// hi3FundCheck
			// 
			this.hi3FundCheck.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.hi3FundCheck.Location = new System.Drawing.Point(16, 200);
			this.hi3FundCheck.Name = "hi3FundCheck";
			this.hi3FundCheck.Size = new System.Drawing.Size(182, 22);
			this.hi3FundCheck.TabIndex = 7;
			this.hi3FundCheck.Text = "Fund on Hi3";
			this.hi3FundCheck.CheckedChanged += new System.EventHandler(this.defaultChangeHandler);
			// 
			// associationsTab
			// 
			this.associationsTab.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.associationsTab.Controls.Add(this.fundGroupGroupBox);
			this.associationsTab.Controls.Add(this.assetFundGroupBox);
			this.associationsTab.Location = new System.Drawing.Point(4, 25);
			this.associationsTab.Name = "associationsTab";
			this.associationsTab.Size = new System.Drawing.Size(744, 475);
			this.associationsTab.TabIndex = 2;
			this.associationsTab.Text = "Associations";
			this.associationsTab.Visible = false;
			// 
			// fundGroupGroupBox
			// 
			this.fundGroupGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.fundGroupGroupBox.Controls.Add(this.pnlFundFundGroups);
			this.fundGroupGroupBox.Controls.Add(this.label5);
			this.fundGroupGroupBox.ForeColor = System.Drawing.Color.Blue;
			this.fundGroupGroupBox.Location = new System.Drawing.Point(16, 208);
			this.fundGroupGroupBox.Name = "fundGroupGroupBox";
			this.fundGroupGroupBox.Size = new System.Drawing.Size(712, 245);
			this.fundGroupGroupBox.TabIndex = 274;
			this.fundGroupGroupBox.TabStop = false;
			this.fundGroupGroupBox.Text = "Fund Groups";
			// 
			// pnlFundFundGroups
			// 
			this.pnlFundFundGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.pnlFundFundGroups.ForeColor = System.Drawing.SystemColors.ControlText;
			this.pnlFundFundGroups.Location = new System.Drawing.Point(16, 48);
			this.pnlFundFundGroups.Name = "pnlFundFundGroups";
			this.pnlFundFundGroups.Size = new System.Drawing.Size(680, 181);
			this.pnlFundFundGroups.TabIndex = 278;
			// 
			// label5
			// 
			this.label5.ForeColor = System.Drawing.Color.Black;
			this.label5.Location = new System.Drawing.Point(16, 24);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(496, 16);
			this.label5.TabIndex = 277;
			this.label5.Text = "Select the Fund Group(s) that this Fund belongs to.";
			// 
			// assetFundGroupBox
			// 
			this.assetFundGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.assetFundGroupBox.Controls.Add(this.lstAssetFundGroups);
			this.assetFundGroupBox.Controls.Add(this.labAssetFundGroups);
			this.assetFundGroupBox.Controls.Add(this.assetFundsCombo);
			this.assetFundGroupBox.Controls.Add(this.associationsTabAssetFundLabel);
			this.assetFundGroupBox.ForeColor = System.Drawing.Color.Blue;
			this.assetFundGroupBox.Location = new System.Drawing.Point(16, 16);
			this.assetFundGroupBox.Name = "assetFundGroupBox";
			this.assetFundGroupBox.Size = new System.Drawing.Size(712, 184);
			this.assetFundGroupBox.TabIndex = 273;
			this.assetFundGroupBox.TabStop = false;
			this.assetFundGroupBox.Text = "Asset Fund";
			// 
			// lstAssetFundGroups
			// 
			this.lstAssetFundGroups.DisplayMember = "DisplayValue";
			this.lstAssetFundGroups.Location = new System.Drawing.Point(16, 112);
			this.lstAssetFundGroups.Name = "lstAssetFundGroups";
			this.lstAssetFundGroups.Size = new System.Drawing.Size(344, 56);
			this.lstAssetFundGroups.TabIndex = 275;
			this.lstAssetFundGroups.TabStop = false;
			this.lstAssetFundGroups.ValueMember = "Key";
			// 
			// labAssetFundGroups
			// 
			this.labAssetFundGroups.AutoSize = true;
			this.labAssetFundGroups.ForeColor = System.Drawing.SystemColors.ControlText;
			this.labAssetFundGroups.Location = new System.Drawing.Point(16, 80);
			this.labAssetFundGroups.Name = "labAssetFundGroups";
			this.labAssetFundGroups.Size = new System.Drawing.Size(452, 18);
			this.labAssetFundGroups.TabIndex = 274;
			this.labAssetFundGroups.Text = "The fund is associated with these groups through its Asset Fund (read-only)";
			// 
			// assetFundsCombo
			// 
			this.assetFundsCombo.DisplayMember = "DisplayValue";
			this.assetFundsCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.assetFundsCombo.ItemHeight = 13;
			this.assetFundsCombo.Location = new System.Drawing.Point(16, 48);
			this.assetFundsCombo.Name = "assetFundsCombo";
			this.assetFundsCombo.Size = new System.Drawing.Size(344, 21);
			this.assetFundsCombo.TabIndex = 14;
			this.assetFundsCombo.ValueMember = "Key";
			this.assetFundsCombo.SelectedIndexChanged += new System.EventHandler(this.assetFundsCombo_SelectedIndexChanged);
			// 
			// associationsTabAssetFundLabel
			// 
			this.associationsTabAssetFundLabel.AutoSize = true;
			this.associationsTabAssetFundLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.associationsTabAssetFundLabel.ForeColor = System.Drawing.Color.Black;
			this.associationsTabAssetFundLabel.Location = new System.Drawing.Point(16, 24);
			this.associationsTabAssetFundLabel.Name = "associationsTabAssetFundLabel";
			this.associationsTabAssetFundLabel.Size = new System.Drawing.Size(333, 19);
			this.associationsTabAssetFundLabel.TabIndex = 273;
			this.associationsTabAssetFundLabel.Text = "A Fund must be associated with a single Asset Fund";
			// 
			// factorsTab
			// 
			this.factorsTab.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.factorsTab.Controls.Add(this.lFactorWarning);
			this.factorsTab.Controls.Add(this.scaleFactorGroupBox);
			this.factorsTab.Controls.Add(this.revalFactorGroupBox);
			this.factorsTab.Controls.Add(this.tpeFactorGroupBox);
			this.factorsTab.Controls.Add(this.xFactorGroupBox);
			this.factorsTab.Location = new System.Drawing.Point(4, 25);
			this.factorsTab.Name = "factorsTab";
			this.factorsTab.Size = new System.Drawing.Size(744, 475);
			this.factorsTab.TabIndex = 4;
			this.factorsTab.Text = "Factors";
			this.factorsTab.Visible = false;
			// 
			// lFactorWarning
			// 
			this.lFactorWarning.AutoSize = true;
			this.lFactorWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lFactorWarning.ForeColor = System.Drawing.Color.Red;
			this.lFactorWarning.Location = new System.Drawing.Point(16, 448);
			this.lFactorWarning.Name = "lFactorWarning";
			this.lFactorWarning.Size = new System.Drawing.Size(438, 19);
			this.lFactorWarning.TabIndex = 26;
			this.lFactorWarning.Text = "You cannot change the factors once the price has been authorised";
			// 
			// scaleFactorGroupBox
			// 
			this.scaleFactorGroupBox.Controls.Add(this.label8);
			this.scaleFactorGroupBox.Controls.Add(this.scaleFactorText);
			this.scaleFactorGroupBox.ForeColor = System.Drawing.Color.Blue;
			this.scaleFactorGroupBox.Location = new System.Drawing.Point(16, 144);
			this.scaleFactorGroupBox.Name = "scaleFactorGroupBox";
			this.scaleFactorGroupBox.Size = new System.Drawing.Size(560, 72);
			this.scaleFactorGroupBox.TabIndex = 23;
			this.scaleFactorGroupBox.TabStop = false;
			this.scaleFactorGroupBox.Text = "Scaling Factor";
			// 
			// label8
			// 
			this.label8.ForeColor = System.Drawing.Color.Black;
			this.label8.Location = new System.Drawing.Point(16, 24);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(360, 32);
			this.label8.TabIndex = 315;
			this.label8.Text = "Applied only on the day of setting.  Applicable to Composite and Linked funds onl" +
				"y (%).";
			// 
			// scaleFactorText
			// 
			this.scaleFactorText.Location = new System.Drawing.Point(384, 24);
			this.scaleFactorText.MaxLength = 9;
			this.scaleFactorText.Name = "scaleFactorText";
			this.scaleFactorText.Size = new System.Drawing.Size(112, 22);
			this.scaleFactorText.TabIndex = 23;
			this.scaleFactorText.Text = "";
			this.scaleFactorText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.scaleFactorText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.factorTextNumeric_KeyPress);
			this.scaleFactorText.TextChanged += new System.EventHandler(this.defaultChangeHandler);
			// 
			// revalFactorGroupBox
			// 
			this.revalFactorGroupBox.Controls.Add(this.revalFactorEndDate);
			this.revalFactorGroupBox.Controls.Add(this.label11);
			this.revalFactorGroupBox.Controls.Add(this.label4);
			this.revalFactorGroupBox.Controls.Add(this.revalFactorText);
			this.revalFactorGroupBox.Controls.Add(this.lRevalFactor1stDay);
			this.revalFactorGroupBox.Controls.Add(this.revalFactor1stDayText);
			this.revalFactorGroupBox.ForeColor = System.Drawing.Color.Blue;
			this.revalFactorGroupBox.Location = new System.Drawing.Point(16, 320);
			this.revalFactorGroupBox.Name = "revalFactorGroupBox";
			this.revalFactorGroupBox.Size = new System.Drawing.Size(560, 120);
			this.revalFactorGroupBox.TabIndex = 25;
			this.revalFactorGroupBox.TabStop = false;
			this.revalFactorGroupBox.Text = "Revaluation Factor";
			// 
			// revalFactorEndDate
			// 
			this.revalFactorEndDate.CustomFormat = "";
			this.revalFactorEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.revalFactorEndDate.Location = new System.Drawing.Point(384, 88);
			this.revalFactorEndDate.Name = "revalFactorEndDate";
			this.revalFactorEndDate.Size = new System.Drawing.Size(112, 22);
			this.revalFactorEndDate.TabIndex = 3;
			this.revalFactorEndDate.ValueChanged += new System.EventHandler(this.RevalDateChangeHandler);
			// 
			// label11
			// 
			this.label11.ForeColor = System.Drawing.Color.Black;
			this.label11.Location = new System.Drawing.Point(16, 88);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(360, 16);
			this.label11.TabIndex = 317;
			this.label11.Text = "Last day of application:";
			// 
			// label4
			// 
			this.label4.ForeColor = System.Drawing.Color.Black;
			this.label4.Location = new System.Drawing.Point(16, 24);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(360, 16);
			this.label4.TabIndex = 314;
			this.label4.Text = "Applicable to Composite and Linked funds only (%).";
			// 
			// revalFactorText
			// 
			this.revalFactorText.Location = new System.Drawing.Point(384, 24);
			this.revalFactorText.MaxLength = 9;
			this.revalFactorText.Name = "revalFactorText";
			this.revalFactorText.Size = new System.Drawing.Size(112, 22);
			this.revalFactorText.TabIndex = 1;
			this.revalFactorText.Text = "";
			this.revalFactorText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.revalFactorText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.factorTextNumeric_KeyPress);
			this.revalFactorText.TextChanged += new System.EventHandler(this.revalFactorText_TextChanged);
			// 
			// lRevalFactor1stDay
			// 
			this.lRevalFactor1stDay.ForeColor = System.Drawing.Color.Black;
			this.lRevalFactor1stDay.Location = new System.Drawing.Point(16, 56);
			this.lRevalFactor1stDay.Name = "lRevalFactor1stDay";
			this.lRevalFactor1stDay.Size = new System.Drawing.Size(360, 16);
			this.lRevalFactor1stDay.TabIndex = 317;
			this.lRevalFactor1stDay.Text = "First day of application:";
			// 
			// revalFactor1stDayText
			// 
			this.revalFactor1stDayText.Location = new System.Drawing.Point(384, 56);
			this.revalFactor1stDayText.MaxLength = 12;
			this.revalFactor1stDayText.Name = "revalFactor1stDayText";
			this.revalFactor1stDayText.ReadOnly = true;
			this.revalFactor1stDayText.Size = new System.Drawing.Size(112, 22);
			this.revalFactor1stDayText.TabIndex = 1;
			this.revalFactor1stDayText.TabStop = false;
			this.revalFactor1stDayText.Text = "";
			// 
			// tpeFactorGroupBox
			// 
			this.tpeFactorGroupBox.Controls.Add(this.label3);
			this.tpeFactorGroupBox.Controls.Add(this.tpeFactorText);
			this.tpeFactorGroupBox.ForeColor = System.Drawing.Color.Blue;
			this.tpeFactorGroupBox.Location = new System.Drawing.Point(16, 232);
			this.tpeFactorGroupBox.Name = "tpeFactorGroupBox";
			this.tpeFactorGroupBox.Size = new System.Drawing.Size(560, 72);
			this.tpeFactorGroupBox.TabIndex = 24;
			this.tpeFactorGroupBox.TabStop = false;
			this.tpeFactorGroupBox.Text = "Tax Provision Estimate (TPE)";
			// 
			// label3
			// 
			this.label3.ForeColor = System.Drawing.Color.Black;
			this.label3.Location = new System.Drawing.Point(16, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(360, 32);
			this.label3.TabIndex = 313;
			this.label3.Text = "Applied on each day until changed.  Applicable to Composite Life and Linked Life " +
				"funds only (%).";
			// 
			// tpeFactorText
			// 
			this.tpeFactorText.Location = new System.Drawing.Point(384, 24);
			this.tpeFactorText.MaxLength = 9;
			this.tpeFactorText.Name = "tpeFactorText";
			this.tpeFactorText.Size = new System.Drawing.Size(112, 22);
			this.tpeFactorText.TabIndex = 24;
			this.tpeFactorText.Text = "";
			this.tpeFactorText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.tpeFactorText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.factorTextNumeric_KeyPress);
			this.tpeFactorText.TextChanged += new System.EventHandler(this.defaultChangeHandler);
			// 
			// xFactorGroupBox
			// 
			this.xFactorGroupBox.Controls.Add(this.xFactorNarrTextBox);
			this.xFactorGroupBox.Controls.Add(this.label9);
			this.xFactorGroupBox.Controls.Add(this.label7);
			this.xFactorGroupBox.Controls.Add(this.xFactorText);
			this.xFactorGroupBox.ForeColor = System.Drawing.Color.Blue;
			this.xFactorGroupBox.Location = new System.Drawing.Point(16, 16);
			this.xFactorGroupBox.Name = "xFactorGroupBox";
			this.xFactorGroupBox.Size = new System.Drawing.Size(560, 112);
			this.xFactorGroupBox.TabIndex = 21;
			this.xFactorGroupBox.TabStop = false;
			this.xFactorGroupBox.Text = "X Factor";
			// 
			// xFactorNarrTextBox
			// 
			this.xFactorNarrTextBox.Location = new System.Drawing.Point(96, 56);
			this.xFactorNarrTextBox.Multiline = true;
			this.xFactorNarrTextBox.Name = "xFactorNarrTextBox";
			this.xFactorNarrTextBox.Size = new System.Drawing.Size(400, 40);
			this.xFactorNarrTextBox.TabIndex = 315;
			this.xFactorNarrTextBox.Text = "";
			this.xFactorNarrTextBox.TextChanged += new System.EventHandler(this.defaultChangeHandler);
			// 
			// label9
			// 
			this.label9.ForeColor = System.Drawing.Color.Black;
			this.label9.Location = new System.Drawing.Point(16, 56);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(72, 16);
			this.label9.TabIndex = 314;
			this.label9.Text = "Narrative";
			// 
			// label7
			// 
			this.label7.ForeColor = System.Drawing.Color.Black;
			this.label7.Location = new System.Drawing.Point(16, 24);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(360, 16);
			this.label7.TabIndex = 313;
			this.label7.Text = "Applied only on the day of setting (%).";
			// 
			// xFactorText
			// 
			this.xFactorText.Location = new System.Drawing.Point(384, 24);
			this.xFactorText.MaxLength = 9;
			this.xFactorText.Name = "xFactorText";
			this.xFactorText.Size = new System.Drawing.Size(112, 22);
			this.xFactorText.TabIndex = 21;
			this.xFactorText.Text = "";
			this.xFactorText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.xFactorText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.factorTextNumeric_KeyPress);
			this.xFactorText.TextChanged += new System.EventHandler(this.xFactorText_TextChanged);
			// 
			// tolerancesTab
			// 
			this.tolerancesTab.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.tolerancesTab.Controls.Add(this.lToleranceWarning);
			this.tolerancesTab.Controls.Add(this.priceIncCheck);
			this.tolerancesTab.Controls.Add(this.priceIncLabel);
			this.tolerancesTab.Controls.Add(this.assetFundTolLabel);
			this.tolerancesTab.Controls.Add(this.assetMovementText);
			this.tolerancesTab.Controls.Add(this.upperToleranceLabel);
			this.tolerancesTab.Controls.Add(this.upperTolText);
			this.tolerancesTab.Controls.Add(this.lowerToleranceLabel);
			this.tolerancesTab.Controls.Add(this.lowerTolText);
			this.tolerancesTab.Location = new System.Drawing.Point(4, 25);
			this.tolerancesTab.Name = "tolerancesTab";
			this.tolerancesTab.Size = new System.Drawing.Size(744, 475);
			this.tolerancesTab.TabIndex = 3;
			this.tolerancesTab.Text = "Tolerances";
			this.tolerancesTab.Visible = false;
			// 
			// lToleranceWarning
			// 
			this.lToleranceWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lToleranceWarning.ForeColor = System.Drawing.Color.Red;
			this.lToleranceWarning.Location = new System.Drawing.Point(8, 144);
			this.lToleranceWarning.Name = "lToleranceWarning";
			this.lToleranceWarning.Size = new System.Drawing.Size(728, 40);
			this.lToleranceWarning.TabIndex = 315;
			this.lToleranceWarning.Text = "The price for this Fund has already been authorised today.  Unless, this price is" +
				" unauthorised, any changes made will not affect the price calculations until the" +
				" following day.";
			this.lToleranceWarning.Visible = false;
			// 
			// priceIncCheck
			// 
			this.priceIncCheck.Location = new System.Drawing.Point(184, 112);
			this.priceIncCheck.Name = "priceIncCheck";
			this.priceIncCheck.Size = new System.Drawing.Size(16, 16);
			this.priceIncCheck.TabIndex = 20;
			this.priceIncCheck.Text = "checkBox3";
			this.priceIncCheck.CheckStateChanged += new System.EventHandler(this.defaultChangeHandler);
			this.priceIncCheck.CheckedChanged += new System.EventHandler(this.priceIncCheck_CheckedChanged);
			// 
			// priceIncLabel
			// 
			this.priceIncLabel.AutoSize = true;
			this.priceIncLabel.ForeColor = System.Drawing.Color.Black;
			this.priceIncLabel.Location = new System.Drawing.Point(16, 112);
			this.priceIncLabel.Name = "priceIncLabel";
			this.priceIncLabel.Size = new System.Drawing.Size(118, 18);
			this.priceIncLabel.TabIndex = 312;
			this.priceIncLabel.Text = "Price increase only";
			// 
			// assetFundTolLabel
			// 
			this.assetFundTolLabel.AutoSize = true;
			this.assetFundTolLabel.ForeColor = System.Drawing.Color.Black;
			this.assetFundTolLabel.Location = new System.Drawing.Point(16, 80);
			this.assetFundTolLabel.Name = "assetFundTolLabel";
			this.assetFundTolLabel.Size = new System.Drawing.Size(129, 18);
			this.assetFundTolLabel.TabIndex = 309;
			this.assetFundTolLabel.Text = "Asset Movement (%)";
			// 
			// assetMovementText
			// 
			this.assetMovementText.Location = new System.Drawing.Point(184, 80);
			this.assetMovementText.MaxLength = 9;
			this.assetMovementText.Name = "assetMovementText";
			this.assetMovementText.ReadOnly = true;
			this.assetMovementText.Size = new System.Drawing.Size(112, 22);
			this.assetMovementText.TabIndex = 19;
			this.assetMovementText.Text = "";
			this.assetMovementText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// upperToleranceLabel
			// 
			this.upperToleranceLabel.AutoSize = true;
			this.upperToleranceLabel.ForeColor = System.Drawing.Color.Black;
			this.upperToleranceLabel.Location = new System.Drawing.Point(16, 48);
			this.upperToleranceLabel.Name = "upperToleranceLabel";
			this.upperToleranceLabel.Size = new System.Drawing.Size(129, 18);
			this.upperToleranceLabel.TabIndex = 307;
			this.upperToleranceLabel.Text = "Upper Tolerance (%)";
			// 
			// upperTolText
			// 
			this.upperTolText.Location = new System.Drawing.Point(184, 48);
			this.upperTolText.MaxLength = 9;
			this.upperTolText.Name = "upperTolText";
			this.upperTolText.Size = new System.Drawing.Size(112, 22);
			this.upperTolText.TabIndex = 18;
			this.upperTolText.Text = "";
			this.upperTolText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.upperTolText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.toleranceTextNumeric_KeyPress);
			this.upperTolText.TextChanged += new System.EventHandler(this.defaultChangeHandler);
			this.upperTolText.Leave += new System.EventHandler(this.lowerAndUpperTolText_Leave);
			// 
			// lowerToleranceLabel
			// 
			this.lowerToleranceLabel.AutoSize = true;
			this.lowerToleranceLabel.ForeColor = System.Drawing.Color.Black;
			this.lowerToleranceLabel.Location = new System.Drawing.Point(16, 16);
			this.lowerToleranceLabel.Name = "lowerToleranceLabel";
			this.lowerToleranceLabel.Size = new System.Drawing.Size(129, 18);
			this.lowerToleranceLabel.TabIndex = 305;
			this.lowerToleranceLabel.Text = "Lower Tolerance (%)";
			// 
			// lowerTolText
			// 
			this.lowerTolText.Location = new System.Drawing.Point(184, 16);
			this.lowerTolText.MaxLength = 9;
			this.lowerTolText.Name = "lowerTolText";
			this.lowerTolText.Size = new System.Drawing.Size(112, 22);
			this.lowerTolText.TabIndex = 17;
			this.lowerTolText.Text = "";
			this.lowerTolText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.lowerTolText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.toleranceTextNumeric_KeyPress);
			this.lowerTolText.TextChanged += new System.EventHandler(this.defaultChangeHandler);
			this.lowerTolText.Leave += new System.EventHandler(this.lowerAndUpperTolText_Leave);
			// 
			// codesTab
			// 
			this.codesTab.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.codesTab.Controls.Add(this.codesGroupBox);
			this.codesTab.Location = new System.Drawing.Point(4, 25);
			this.codesTab.Name = "codesTab";
			this.codesTab.Size = new System.Drawing.Size(744, 475);
			this.codesTab.TabIndex = 1;
			this.codesTab.Text = "Codes";
			this.codesTab.Visible = false;
			// 
			// codesGroupBox
			// 
			this.codesGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.codesGroupBox.Controls.Add(this.pnlExternalSystemIds);
			this.codesGroupBox.Controls.Add(this.label10);
			this.codesGroupBox.ForeColor = System.Drawing.Color.Blue;
			this.codesGroupBox.Location = new System.Drawing.Point(16, 16);
			this.codesGroupBox.Name = "codesGroupBox";
			this.codesGroupBox.Size = new System.Drawing.Size(712, 440);
			this.codesGroupBox.TabIndex = 0;
			this.codesGroupBox.TabStop = false;
			this.codesGroupBox.Text = "Fund Codes";
			// 
			// pnlExternalSystemIds
			// 
			this.pnlExternalSystemIds.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.pnlExternalSystemIds.ForeColor = System.Drawing.SystemColors.ControlText;
			this.pnlExternalSystemIds.Location = new System.Drawing.Point(16, 56);
			this.pnlExternalSystemIds.Name = "pnlExternalSystemIds";
			this.pnlExternalSystemIds.Size = new System.Drawing.Size(680, 373);
			this.pnlExternalSystemIds.TabIndex = 1;
			// 
			// label10
			// 
			this.label10.ForeColor = System.Drawing.Color.Black;
			this.label10.Location = new System.Drawing.Point(16, 24);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(568, 24);
			this.label10.TabIndex = 0;
			this.label10.Text = "Select the Systems that this Fund\'s price is exported to and set the codes that i" +
				"dentify it.";
			// 
			// FundStaticDataEditor
			// 
			this.Controls.Add(this.tabs);
			this.Name = "FundStaticDataEditor";
			this.Size = new System.Drawing.Size(752, 504);
			this.tabs.ResumeLayout(false);
			this.propertiesTab.ResumeLayout(false);
			this.pnlNewFund.ResumeLayout(false);
			this.associationsTab.ResumeLayout(false);
			this.fundGroupGroupBox.ResumeLayout(false);
			this.assetFundGroupBox.ResumeLayout(false);
			this.factorsTab.ResumeLayout(false);
			this.scaleFactorGroupBox.ResumeLayout(false);
			this.revalFactorGroupBox.ResumeLayout(false);
			this.tpeFactorGroupBox.ResumeLayout(false);
			this.xFactorGroupBox.ResumeLayout(false);
			this.tolerancesTab.ResumeLayout(false);
			this.codesTab.ResumeLayout(false);
			this.codesGroupBox.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		#region Event Handlers

		private void shareClassText_Changed(object sender, EventArgs e)
		{
			if (!m_eventsAreSystemGenerated)
			{
				change();
				generateHiPortCode();
			}
		}

		private void dualPriceCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (!m_eventsAreSystemGenerated)
			{
				if (!dualPriceCheck.Checked) midPriceCheck.Checked = false;
				change();
				formatter.EnableControls();
			}
		}

		private void defaultChangeHandler(object sender, EventArgs e)
		{
			change();
		}

		private void fundTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			T.E();
			if (!m_eventsAreSystemGenerated)
			{
				if (CurrentFund.IsNew)
				{
					change();
					switch (fundTypeCombo.SelectedIndex)
					{
						case 0:
							currentFund = new Composite();
							break;
						case 1:
							currentFund = new LinkedFund();
							break;
						case 2:
							currentFund = new OEICFund();
							break;
						default:
							currentFund = new newFund();
							break;
					}

					formatter = viewFormatterFactory.CreateViewFormatter(this);
					formatter.EnableControls();
				}
			}
			formatter.FillAssetFundsCombo(CurrentFund);
			T.X();
		}

		private void assetFundsCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			T.E();
			if (!m_eventsAreSystemGenerated)
			{
				change();
				generateHiPortCode();
			}
			fillAssetFundGroups();
			T.X();
		}

		private void listToListDefaultChanging(object sender, ListToListChangingArgs e)
		{
			change();
		}

		private void factorTextNumeric_KeyPress(object sender, KeyPressEventArgs e)
		{
			string controlText = ((Control) sender).Text;

			if (!Char.IsNumber(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '-' && !Char.IsControl(e.KeyChar))
			{
				e.Handled = true;
			}

			else if (e.KeyChar == '.' && controlText.IndexOf('.') >= 0)
			{
				e.Handled = true;
			}
			else if (e.KeyChar == '-' && controlText.IndexOf('-') >= 0)
			{
				e.Handled = true;
			}


		}

		private void toleranceTextNumeric_KeyPress(object sender, KeyPressEventArgs e)
		{
			string controlText = ((Control) sender).Text;

			if (!Char.IsNumber(e.KeyChar) && e.KeyChar != '.' && !Char.IsControl(e.KeyChar))
			{
				e.Handled = true;
			}

			else if (e.KeyChar == '.' && controlText.IndexOf('.') >= 0)
			{
				e.Handled = true;
			}
		}


		private void xFactorText_TextChanged(object sender, EventArgs e)
		{
			if (!m_eventsAreSystemGenerated)
			{
				change();
				formatter.EnableControls();
			}
		}

		private void revalFactorText_TextChanged(object sender, EventArgs e)
		{
			if (!m_eventsAreSystemGenerated)
			{
				change();
				formatter.EnableControls();
			}
		}

		private void externalSystemIDsListToList_EditingSelectedItem(object sender, ListToListEditedArgs e)
		{
			ExternalSystemID changedObj = (ExternalSystemID) e.EditedObject;
			try
			{
				if (!m_eventsAreSystemGenerated)
				{
					changedObj.FundCode = e.EditedValue;
					changedObj.IsDirty = true;
					change();
				}
			}
			catch
			{
				e.Cancel = true;
				MessageBox.Show("Unable to convert the edited value to a valid fund code.", "Cannot convert value", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}

		}

		private void lifeCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (!m_eventsAreSystemGenerated)
			{
				change();
				if (!lifeCheck.Checked)
				{
					tpeFactorText.Text = "0.00";
				}
				formatter.EnableControls();
			}
		}

		private void priceIncCheck_CheckedChanged(object sender, EventArgs e)
		{
			change();
		}


		private void lowerAndUpperTolText_Leave(object sender, EventArgs e)
		{
			Control senderControl = (sender as Control);
			if (senderControl != null)
			{
				decimal tolerance;
				if (safeDecimalParse(senderControl.Text, out tolerance))
				{
					senderControl.Text = tolerance.ToString(toleranceFormatString);
				}
			}
		}

		#endregion

		private void benchmarkAbleCheck_CheckedChanged(object sender, EventArgs e)
		{
			change();
		}

		private void RevalDateChangeHandler(object sender, System.EventArgs e)
		{
			if (DateTime.Compare(revalFactorEndDate.Value,GlobalRegistry.NextCompanyValuationDateAndTime)<0)
			{
				revalFactorEndDate.Value=GlobalRegistry.NextCompanyValuationDateAndTime;
			}
			else
			{
				defaultChangeHandler(sender,e);
			}
		}

		#region View Formatter Classes

		private abstract class viewFormatter
		{
			public viewFormatter(FundStaticDataEditor parent)
			{
				T.E();
				this.parent = parent;
				T.X();
			}

			protected FundStaticDataEditor parent;

			public virtual void DisplayFundProperties()
			{
				T.E();
				//cast just the once
				Fund currentFund = parent.CurrentFund; //cast just the once
				//properties tab
				parent.hiPortCodeText.Text = currentFund.HiPortfolioCode;
				parent.fullNameText.Text = currentFund.FullName;
				parent.shortNameText.Text = currentFund.ShortName;
				parent.shareClassText.Text = currentFund.ClassOrSeriesCode.ToUpper();
				parent.securityCodeText.Text = currentFund.SecurityCode;

				parent.hi3FundCheck.Checked = false;
				parent.dualPriceCheck.Checked = false;
				parent.midPriceCheck.Checked = false;
				parent.exDivCheck.Checked = false;
				parent.lifeCheck.Checked = false;
				parent.benchmarkableCheck.Checked = currentFund.IsBenchMarkable;


				//associations tab
				parent.assetFundsCombo.SelectedValue = currentFund.AssetFundID;
				parent.fillFundFundGroups();

				//codes tab
				parent.UpdateExternalSystemIDsListToList();

				//factors tab
				parent.xFactorText.Text = currentFund.XFactorPercent.ToString("##0.0000");
				parent.xFactorNarrTextBox.Text = currentFund.XFactorDescription;

				//clear the ones we don't use / default them
				parent.scaleFactorText.Text = "0.0000";
				parent.revalFactorEndDate.Value = GlobalRegistry.NextCompanyValuationDateAndTime;
				parent.revalFactorEndDate.Text = string.Empty;
				parent.revalFactor1stDayText.Text = GlobalRegistry.CurrentCompanyValuationDateAndTime.ToShortDateString();
				parent.revalFactorText.Text = "0.0000";
				parent.tpeFactorText.Text = "0.0000";

				//tolerances tab
				//note that tolerances stored in db as ratio to 1, not a percentile
				parent.upperTolText.Text = (currentFund.UpperTolerance*100).ToString(toleranceFormatString);
				parent.lowerTolText.Text = (currentFund.LowerTolerance*100).ToString(toleranceFormatString);
				parent.priceIncCheck.Checked = currentFund.PriceIncreaseOnly;
				parent.assetMovementText.Text = (currentFund.AssetMovementTolerance*100).ToString(toleranceFormatString);

				T.X();
			}

			public void EnableControls()
			{
				T.E();
				parent.SuspendLayout();

				try
				{
					doEnableControls();
				}
				finally
				{
					parent.ResumeLayout();
				}

				T.X();
			}

			protected virtual void doEnableControls()
			{
				T.E();

				bool enableState = (parent.authoringMode == authoringModes.New);
				if (parent.fundTypeCombo.Enabled != enableState)
				{
					parent.fundTypeCombo.Enabled = enableState;
				}

				if (parent.shareClassText.Enabled != enableState)
				{
					parent.shareClassText.Enabled = enableState;
				}

				if (parent.assetFundsCombo.Enabled != enableState)
				{
					parent.assetFundsCombo.Enabled = enableState;
				}

				// factors tab

				bool isAuthorised = isFundAuthorised();

				if (isAuthorised)
				{
					disableAllControls(parent.factorsTab);
				}
				else
				{
					enableAllControls(parent.factorsTab);
				}

				//cast just the once
				Fund currentFund = parent.CurrentFund;
				decimal xfactor;

				if (isAuthorised)
				{
					parent.lFactorWarning.Enabled = true;
					parent.lFactorWarning.Show();
				}
				else
				{
					if (currentFund.IsNew)
					{
						enableAllControls(parent.associationsTab);
						enableAllControls(parent.tolerancesTab);
						enableAllControls(parent.codesTab);
						//enableAllControls(parent.factorsTab);

						parent.xFactorGroupBox.Enabled = true;
						parent.xFactorText.Enabled = true;

						if (!safeDecimalParse(parent.xFactorText.Text, out xfactor))
							xfactor = 0;

						enableState = (xfactor != 0);

						if (parent.xFactorNarrTextBox.Enabled != enableState)
						{
							parent.xFactorNarrTextBox.Enabled = enableState;
						}
						parent.xFactorNarrTextBox.Enabled = enableState;
					}
					else
					{
						if (!parent.upperTolText.Enabled) //use any field to test - this re-enables after cancelling from New
						{
							enableAllControls(parent.associationsTab);
							enableAllControls(parent.tolerancesTab);
							enableAllControls(parent.codesTab);
						}

						// Can only modify associated asset funds when adding a new Fund
						parent.assetFundsCombo.Enabled = false;

						enableState = (currentFund != null && FundFactorBusinessRules.CanSetXFactor(currentFund));

						if (parent.xFactorGroupBox.Enabled != enableState)
						{
							parent.xFactorGroupBox.Enabled = enableState;
						}

						enableState = (currentFund != null && FundFactorBusinessRules.CanSetScalingFactor(currentFund));
						if (parent.scaleFactorGroupBox.Enabled != enableState)
						{
							parent.scaleFactorGroupBox.Enabled = enableState;
						}

						enableState = (currentFund != null && FundFactorBusinessRules.CanSetTaxProvisionEstimate(currentFund));
						;
						if (parent.tpeFactorGroupBox.Enabled != enableState)
						{
							parent.tpeFactorGroupBox.Enabled = enableState;
						}

						enableState = (currentFund != null && FundFactorBusinessRules.CanSetRevaluationFactor(currentFund));
						if (parent.revalFactorGroupBox.Enabled != enableState)
						{
							parent.revalFactorGroupBox.Enabled = enableState;
						}


						if (!safeDecimalParse(parent.xFactorText.Text, out xfactor))
							xfactor = 0;

						enableState = (xfactor != 0);

						if (parent.xFactorNarrTextBox.Enabled != enableState)
						{
							parent.xFactorNarrTextBox.Enabled = enableState;
						}

						//parent.revalFactorEndDate.MinDate = GlobalRegistry.NextCompanyValuationDateAndTime;
					}
					parent.lFactorWarning.Hide();
				}

				//tolerance tab
				parent.lToleranceWarning.Visible = isFundAuthorised();
				T.X();
			}


			protected void updateBaseProperties(Fund currentFund)
			{
				T.E();
				try
				{
					//properties tab
					currentFund.HiPortfolioCode = parent.hiPortCodeText.Text;
					currentFund.FullName = parent.fullNameText.Text;
					currentFund.ShortName = parent.shortNameText.Text;
					currentFund.ClassOrSeriesCode = parent.shareClassText.Text.ToUpper();
					currentFund.SecurityCode = parent.securityCodeText.Text;
					currentFund.IsBenchMarkable = parent.benchmarkableCheck.Checked;

					//associations tab
					if (parent.assetFundsCombo != null && parent.assetFundsCombo.SelectedValue != null)
					{
						currentFund.ParentAssetFundID = parent.assetFundsCombo.SelectedValue.ToString();
					}
					else
					{
						currentFund.ParentAssetFundID = string.Empty;
					}

					IList selectedGroups = null;
					if (parent.fundFundGroupsListToList != null)
					{
						selectedGroups = parent.fundFundGroupsListToList.SelectedItems();
					}
					FundGroup[] groups = null;
					if (selectedGroups != null)
					{
						groups = new FundGroup[selectedGroups.Count];
						selectedGroups.CopyTo(groups, 0);
						currentFund.FundGroups = new FundGroupCollection(groups);
					}
				}
				finally
				{
				}

			}

			protected void UpdateFund(out bool xFactorParsedOK, out bool upperToleranceParsedOK, out bool lowerToleranceParsedOK)
			{
				T.E();
				try
				{
					//cast just the once 
					Fund currentFund = parent.CurrentFund;

					xFactorParsedOK = true;

					updateBaseProperties(currentFund); //this stuff separated out as is only things needed for new

					//code tab
					IList selectedSystems = null;
					if (parent.externalSystemIDsListToList != null)
					{
						selectedSystems = parent.externalSystemIDsListToList.SelectedItems();
						if (currentFund.SystemIDs == null)
						{
							currentFund.SystemIDs = new ExternalSystemIDCollection();
						}

						foreach (ExternalSystemID newId in selectedSystems)
						{
							if (!currentFund.SystemIDs.Contains(newId))
							{
								currentFund.SystemIDs.Add(newId);
								newId.IsNew = true;
								newId.HiPortfolioCode = currentFund.HiPortfolioCode;
							}
						}
						foreach (ExternalSystemID deleteId in currentFund.SystemIDs)
						{
							if (!selectedSystems.Contains(deleteId))
							{
								deleteId.IsDeleted = true;
							}
						}
					}

					//factors tab
					if (!isFundAuthorised())
					{
						decimal factorPercent;

						xFactorParsedOK = safeDecimalParse(parent.xFactorText.Text, out factorPercent);
						if (xFactorParsedOK)
						{
							//store as percent - internally divides by 100 to store as ratio
							currentFund.XFactorPercent = factorPercent;
							if (factorPercent != 0)
							{
								currentFund.XFactorDescription = parent.xFactorNarrTextBox.Text;
							}
						}
					}

					//tolerances tab
					decimal tolerancePercent;
					upperToleranceParsedOK = safeDecimalParse(parent.upperTolText.Text, out tolerancePercent);
					if (upperToleranceParsedOK)
					{
						//in db we store as ratio
						currentFund.UpperTolerance = tolerancePercent/100;
					}
					lowerToleranceParsedOK = safeDecimalParse(parent.lowerTolText.Text, out tolerancePercent);
					if (lowerToleranceParsedOK)
					{
						//in db we store as ratio
						currentFund.LowerTolerance = tolerancePercent/100;
					}

					currentFund.PriceIncreaseOnly = parent.priceIncCheck.Checked;
				}
				finally
				{
					T.X();
				}
			}

			public abstract void UpdateFund(out bool xFactorParsedOK, out bool tpeParsedOK, out bool revalFactorParsedOK, out bool scalingFactorParsedOK, out bool upperToleranceParsedOK, out bool lowerToleranceParsedOK);

			public virtual void FillAssetFundsCombo(Fund currentFund)
			{
				T.E();
				parent.assetFundsCombo.DataSource = null;
				parent.assetFundsCombo.Items.Clear();

				IList assetFunds = this.getAssetFunds();

				if (assetFunds != null)
				{
					// Allow a null asset fund to be specified 
					// only when a new fund is being created
					if (currentFund.IsNew)
					{
						SimpleStringLookup nullLookUp = new SimpleStringLookup("0", "");
						assetFunds.Insert(0, nullLookUp);
					}

					parent.assetFundsCombo.DataSource = assetFunds;
				}
				T.X();
			}

			protected abstract IList getAssetFunds();

			protected bool isFundAuthorised()
			{
				Fund currentFund = parent.CurrentFund;

				return (currentFund.FundStatus >= Fund.FundStatusType.SecondLevelAuthorised);
			}

			protected void disableAllControls(Control target)
			{
				T.E();
				enableDisableControls(target, false);
				T.X();
			}

			protected void enableAllControls(Control target)
			{
				T.E();
				enableDisableControls(target, true);
				T.X();
			}

			private void enableDisableControls(Control target, bool state)
			{
				T.E();
				foreach (Control control in target.Controls)
				{
					enableDisableControls(control, state);

					if (control is TabControl ||
						control is TextBox ||
						control is ComboBox ||
						control is ListToListControl ||
						control is CheckBox ||
						control is DateTimePicker ||
						control is GroupBox ||
						control is Label)
					{
						control.Enabled = state;
					}
				}
				T.X();
			}

			protected AssetFundController assetFundController = new AssetFundController();
		}

		private class compositeViewFormatter : viewFormatter
		{
			public compositeViewFormatter(FundStaticDataEditor parent) : base(parent)
			{
				T.E();
				fund = (Composite) parent.CurrentFund;
				T.X();
			}

			private Composite fund;

			public override void DisplayFundProperties()
			{
				T.E();
				parent.fundTypeCombo.SelectedIndex = 0;

				base.DisplayFundProperties();

				parent.hi3FundCheck.Checked = fund.OnHiPortfolio3;
				parent.dualPriceCheck.Checked = fund.IsDualPrice;
				parent.midPriceCheck.Checked = fund.UseMidPriceAsBidPrice;
				parent.lifeCheck.Checked = fund.IsLife;

				//factors tab
				parent.revalFactorText.Text = fund.RevalFactorPercent.ToString("##0.0000");
				if (fund.RevaluationEndDate <= GlobalRegistry.CurrentCompanyValuationDateAndTime)
				{
					parent.revalFactorEndDate.Value = GlobalRegistry.NextCompanyValuationDateAndTime;
				}
				else
				{
					parent.revalFactorEndDate.Value = fund.RevaluationEndDate;
				}

				if (fund.RevaluationEffectiveDate == DateTime.MinValue)
				{
					parent.revalFactor1stDayText.Text = GlobalRegistry.CurrentCompanyValuationDateAndTime.ToShortDateString(); 
				}
				else
				{
					parent.revalFactor1stDayText.Text = fund.RevaluationEffectiveDate.ToShortDateString();
				}

				parent.scaleFactorText.Text = fund.ScaleFactorPercent.ToString("##0.0000");
				parent.tpeFactorText.Text = fund.TPEPercent.ToString("##0.0000");

				T.X();
			}

			protected override void doEnableControls()
			{
				T.E();
				base.doEnableControls();
				parent.exDivCheck.Enabled = false;
				parent.hi3FundCheck.Enabled = true;
				parent.dualPriceCheck.Enabled = true;
				parent.midPriceCheck.Enabled = parent.dualPriceCheck.Checked;
				parent.lifeCheck.Enabled = true;
				parent.benchmarkableCheck.Enabled = true;

				Fund currentFund = parent.CurrentFund;

				if (currentFund.IsNew)
				{
					parent.scaleFactorGroupBox.Enabled = true;
					parent.scaleFactorText.Enabled = true;
					parent.tpeFactorGroupBox.Enabled = parent.lifeCheck.Checked;
					parent.tpeFactorText.Enabled = parent.lifeCheck.Checked;
					parent.revalFactorGroupBox.Enabled = true;
					parent.revalFactorText.Enabled = true;
					parent.benchmarkableCheck.Enabled = true;
				}

				decimal revalFactor;
				if (!safeDecimalParse(parent.revalFactorText.Text, out revalFactor))
					revalFactor = 0;

				parent.revalFactorEndDate.Enabled = (revalFactor != 0);


				T.X();
			}

			public override void UpdateFund(out bool xFactorParsedOK, out bool tpeParsedOK, out bool revalFactorParsedOK, out bool scalingFactorParsedOK,
				out bool upperToleranceParsedOK, out bool lowerToleranceParsedOK)
			{
				T.E();
				try
				{
					base.UpdateFund(out xFactorParsedOK, out upperToleranceParsedOK, out lowerToleranceParsedOK);

					tpeParsedOK = true;
					revalFactorParsedOK = true;
					scalingFactorParsedOK = true;


					//factors tab
					if (!isFundAuthorised())
					{
						decimal factorPercent;
						if (parent.tpeFactorGroupBox.Enabled)
						{
							tpeParsedOK = safeDecimalParse(parent.tpeFactorText.Text, out factorPercent);
							if (tpeParsedOK)
							{
								//store as percent - internally divides by 100 to store as ratio
								fund.TPEPercent = factorPercent;
							}
						}

						if (parent.revalFactorGroupBox.Enabled)
						{
							revalFactorParsedOK = safeDecimalParse(parent.revalFactorText.Text, out factorPercent);
							if (revalFactorParsedOK)
							{
								//store as percent - internally divides by 100 to store as ratio
								fund.RevalFactorPercent = factorPercent;
							}
							fund.RevaluationEndDate = parent.revalFactorEndDate.Value;
							if (fund.RevaluationEffectiveDate == DateTime.MinValue)
								fund.RevaluationEffectiveDate = DateTime.Today;
						}

						if (parent.scaleFactorGroupBox.Enabled)
						{
							scalingFactorParsedOK = safeDecimalParse(parent.scaleFactorText.Text, out factorPercent);
							if (scalingFactorParsedOK)
							{
								fund.ScaleFactorPercent = factorPercent;
							}
						}
					}

					fund.OnHiPortfolio3 = parent.hi3FundCheck.Checked;
					fund.IsDualPrice = parent.dualPriceCheck.Checked;
					fund.UseMidPriceAsBidPrice = (parent.midPriceCheck.Checked && parent.dualPriceCheck.Checked);
					fund.IsLife = parent.lifeCheck.Checked;

				}
				finally
				{
				}
				T.X();
			}


			protected override IList getAssetFunds()
			{
				T.E();
				IList result =
					AssetFundController.LoadAssetFundLookupsByCompanyAndType
					(GlobalRegistry.ConnectionString,
					GlobalRegistry.CompanyCode,
					AssetFund.AssetFundTypeEnum.Composite);
				T.X();
				return result;
			}

		}

		private class linkedFundViewFormatter : viewFormatter
		{
			public linkedFundViewFormatter(FundStaticDataEditor parent) : base(parent)
			{
				T.E();
				fund = (LinkedFund) parent.CurrentFund;
				T.X();
			}

			private LinkedFund fund;

			public override void DisplayFundProperties()
			{
				T.E();
				parent.fundTypeCombo.SelectedIndex = 1;

				base.DisplayFundProperties();

				parent.hi3FundCheck.Checked = fund.OnHiPortfolio3;
				parent.dualPriceCheck.Checked = fund.IsDualPrice;
				parent.midPriceCheck.Checked = fund.UseMidPriceAsBidPrice;
				parent.lifeCheck.Checked = fund.IsLife;


				//factors tab

				parent.revalFactorText.Text = fund.RevalFactorPercent.ToString("##0.0000");
				if (fund.RevaluationEndDate <= GlobalRegistry.CurrentCompanyValuationDateAndTime)
				{
					parent.revalFactorEndDate.Value = GlobalRegistry.NextCompanyValuationDateAndTime;
				}
				else
				{
					parent.revalFactorEndDate.Value = fund.RevaluationEndDate;
				}

				if (fund.RevaluationEffectiveDate == DateTime.MinValue)
				{
					parent.revalFactor1stDayText.Text = GlobalRegistry.CurrentCompanyValuationDateAndTime.ToShortDateString();
				}
				else
				{
					parent.revalFactor1stDayText.Text = fund.RevaluationEffectiveDate.ToShortDateString();
				}

				parent.scaleFactorText.Text = fund.ScaleFactorPercent.ToString("##0.0000");
				parent.tpeFactorText.Text = fund.TPEPercent.ToString("##0.0000");

				T.X();
			}

			protected override void doEnableControls()
			{
				T.E();
				base.doEnableControls();
				parent.exDivCheck.Enabled = false;
				parent.hi3FundCheck.Enabled = true;
				parent.dualPriceCheck.Enabled = true;
				parent.midPriceCheck.Enabled = parent.dualPriceCheck.Checked;
				parent.lifeCheck.Enabled = true;
				parent.benchmarkableCheck.Enabled = true;

				Fund currentFund = parent.CurrentFund;

				if (currentFund.IsNew)
				{
					parent.scaleFactorGroupBox.Enabled = true;
					parent.scaleFactorText.Enabled = true;
					parent.tpeFactorGroupBox.Enabled = parent.lifeCheck.Checked;
					parent.tpeFactorText.Enabled = parent.lifeCheck.Checked;
					parent.revalFactorGroupBox.Enabled = true;
					parent.revalFactorText.Enabled = true;
				}

				decimal revalFactor;
				if (!safeDecimalParse(parent.revalFactorText.Text, out revalFactor))
					revalFactor = 0;

				parent.revalFactorEndDate.Enabled = (revalFactor != 0);

				T.X();
			}

			public override void UpdateFund(out bool xFactorParsedOK, out bool tpeParsedOK, out bool revalFactorParsedOK, out bool scalingFactorParsedOK,
				out bool upperToleranceParsedOK, out bool lowerToleranceParsedOK)
			{
				T.E();
				try
				{
					base.UpdateFund(out xFactorParsedOK, out upperToleranceParsedOK, out lowerToleranceParsedOK);

					//factors tab
					tpeParsedOK = true;
					revalFactorParsedOK = true;
					scalingFactorParsedOK = true;
					if (!isFundAuthorised())
					{
						decimal factorPercent;
						if (parent.tpeFactorGroupBox.Enabled)
						{
							tpeParsedOK = safeDecimalParse(parent.tpeFactorText.Text, out factorPercent);
							if (tpeParsedOK)
							{
								//store as percent - internally divides by 100 to store as ratio
								fund.TPEPercent = factorPercent;
							}
						}

						if (parent.revalFactorGroupBox.Enabled)
						{
							revalFactorParsedOK = safeDecimalParse(parent.revalFactorText.Text, out factorPercent);
							if (revalFactorParsedOK)
							{
								//store as percent - internally divides by 100 to store as ratio
								fund.RevalFactorPercent = factorPercent;
							}
							fund.RevaluationEndDate = parent.revalFactorEndDate.Value;
							if (fund.RevaluationEffectiveDate == DateTime.MinValue)
								fund.RevaluationEffectiveDate = DateTime.Today;
						}

						if (parent.scaleFactorGroupBox.Enabled)
						{
							scalingFactorParsedOK = safeDecimalParse(parent.scaleFactorText.Text, out factorPercent);
							if (scalingFactorParsedOK)
							{
								fund.ScaleFactorPercent = factorPercent;
							}
						}
					}
					fund.OnHiPortfolio3 = parent.hi3FundCheck.Checked;
					fund.IsDualPrice = parent.dualPriceCheck.Checked;
					fund.UseMidPriceAsBidPrice = (parent.midPriceCheck.Checked && parent.dualPriceCheck.Checked);
					fund.IsLife = parent.lifeCheck.Checked;
				}
				finally
				{
					T.X();
				}
			}


			protected override IList getAssetFunds()
			{
				T.E();
				IList result =
					AssetFundController.LoadAssetFundLookupsByCompanyAndType
					(GlobalRegistry.ConnectionString,
					GlobalRegistry.CompanyCode,
					AssetFund.AssetFundTypeEnum.Linked);

				T.X();
				return result;
			}

		}

		private class oeicFundViewFormatter : viewFormatter
		{
			public oeicFundViewFormatter(FundStaticDataEditor parent) : base(parent)
			{
				T.E();
				fund = (OEICFund) parent.CurrentFund;
				T.X();
			}

			private OEICFund fund;

			public override void DisplayFundProperties()
			{
				T.E();
				parent.fundTypeCombo.SelectedIndex = 2;

				base.DisplayFundProperties();

				parent.hi3FundCheck.Checked = fund.OnHiPortfolio3;
				parent.exDivCheck.Checked = fund.IsExDividend;
				T.X();
			}

			protected override void doEnableControls()
			{
				T.E();
				base.doEnableControls();
				parent.hi3FundCheck.Enabled = false;
				parent.dualPriceCheck.Enabled = false;
				parent.midPriceCheck.Enabled = false;
				parent.lifeCheck.Enabled = false;
				parent.benchmarkableCheck.Enabled = true;
				parent.hi3FundCheck.Enabled = true;
				parent.exDivCheck.Enabled = true;
				parent.tpeFactorGroupBox.Enabled = false;
				parent.revalFactorGroupBox.Enabled = false;
				parent.scaleFactorGroupBox.Enabled = false;
				T.X();
			}

			public override void UpdateFund(out bool xFactorParsedOK, out bool tpeParsedOK, out bool revalFactorParsedOK, out bool scalingFactorParsedOK,
				out bool upperToleranceParsedOK, out bool lowerToleranceParsedOK)
			{
				T.E();
				tpeParsedOK = true;
				revalFactorParsedOK = true;
				scalingFactorParsedOK = true;
				base.UpdateFund(out xFactorParsedOK, out upperToleranceParsedOK, out lowerToleranceParsedOK);
				fund.OnHiPortfolio3 = parent.hi3FundCheck.Checked;
				fund.IsExDividend = parent.exDivCheck.Checked;
				T.X();
			}

			protected override IList getAssetFunds()
			{
				T.E();
				IList result =
					AssetFundController.LoadAssetFundLookupsByCompanyAndType
					(GlobalRegistry.ConnectionString,
					GlobalRegistry.CompanyCode,
					AssetFund.AssetFundTypeEnum.Oeic);

				T.X();
				return result;
			}
		}

		private class nullFundViewFormatter : viewFormatter
		{
			public nullFundViewFormatter(FundStaticDataEditor parent) : base(parent)
			{
				T.E();
				T.X();
			}

			public override void DisplayFundProperties()
			{
				T.E();
				parent.hiPortCodeText.Text = string.Empty;
				parent.fullNameText.Text = string.Empty;
				parent.shortNameText.Text = string.Empty;
				parent.shareClassText.Text = string.Empty;
				parent.securityCodeText.Text = string.Empty;

				parent.fundTypeCombo.SelectedIndex = -1;

				parent.hi3FundCheck.Checked = false;
				parent.dualPriceCheck.Checked = false;
				parent.midPriceCheck.Checked = false;
				parent.exDivCheck.Checked = false;
				parent.lifeCheck.Checked = false;
				parent.benchmarkableCheck.Checked = false;


				//associations tab
				parent.assetFundsCombo.SelectedValue = null;

				if (parent.fundFundGroupsListToList != null)
				{
					parent.fundFundGroupsListToList.ClearLists();
				}

				//codes tab
				if (parent.externalSystemIDsListToList != null)
				{
					parent.externalSystemIDsListToList.ClearLists();
				}

				//factors tab
				parent.xFactorText.Text = string.Empty;
				parent.xFactorNarrTextBox.Text = string.Empty;
				parent.tpeFactorText.Text = string.Empty;
				parent.revalFactorText.Text = string.Empty;
				parent.revalFactor1stDayText.Text = string.Empty;
				parent.revalFactorEndDate.Value =GlobalRegistry.NextCompanyValuationDateAndTime;
				parent.scaleFactorText.Text = string.Empty;

				//tolerances tab
				parent.upperTolText.Text = string.Empty;
				parent.lowerTolText.Text = string.Empty;
				parent.priceIncCheck.Checked = false;
				parent.assetMovementText.Text = string.Empty;

				T.X();
			}

			protected override void doEnableControls()
			{
				T.E();
				disableAllControls(parent.tabs);
				T.X();
			}

			public override void UpdateFund(out bool xFactorParsedOK, out bool tpeParsedOK, out bool revalFactorParsedOK, out bool scalingFactorParsedOK,
				out bool upperToleranceParsedOK, out bool lowerToleranceParsedOK)
			{
				T.E();
				try
				{
					throw new NullReferenceException("You cannot update a null fund");
				}
				finally
				{
					T.X();
				}
			}

			protected override IList getAssetFunds()
			{
				T.E();
				IList result = null;
				T.X();
				return result;
			}
		}

		private class newFundViewFormatter : viewFormatter
		{
			public newFundViewFormatter(FundStaticDataEditor parent) : base(parent)
			{
				T.E();
				T.X();
			}

			protected override void doEnableControls()
			{
				T.E();
				disableAllControls(parent.tabs);
				enableAllControls(parent.pnlNewFund);
				parent.hiPortCodeText.Enabled = true;
				parent.fullNameText.Enabled = true;
				parent.shortNameText.Enabled = true;
				parent.shareClassText.Enabled = true;
				parent.fundTypeCombo.Enabled = true;
				parent.assetFundGroupBox.Enabled = true;
				parent.assetFundsCombo.Enabled = true;
				T.X();
			}

			public override void UpdateFund(out bool xFactorParsedOK, out bool tpeParsedOK, out bool revalFactorParsedOK, out bool scalingFactorParsedOK,
				out bool upperToleranceParsedOK, out bool lowerToleranceParsedOK)
			{
				T.E();
				try
				{
					xFactorParsedOK = true;
					tpeParsedOK = true;
					revalFactorParsedOK = true;
					scalingFactorParsedOK = true;
					upperToleranceParsedOK = true;
					lowerToleranceParsedOK = true;
					updateBaseProperties(parent.CurrentFund);
				}
				finally
				{
					T.X();
				}
			}

			public override void FillAssetFundsCombo(Fund currentFund)
			{
				T.E();
				base.FillAssetFundsCombo(currentFund);
				//parent.assetFundsCombo.SelectedValue = -1; // = null;
				T.X();
			}

			private AssetFund.AssetFundTypeEnum fundTypeChosen()
			{
				AssetFund.AssetFundTypeEnum returnType = AssetFund.AssetFundTypeEnum.Oeic;

				switch (parent.fundTypeCombo.SelectedIndex)
				{
					case 0:
						returnType = AssetFund.AssetFundTypeEnum.Composite;
						break;
					case 1:
						returnType = AssetFund.AssetFundTypeEnum.Linked;
						break;
					case 2:
						returnType = AssetFund.AssetFundTypeEnum.Oeic;
						break;
				}
				return returnType;
			}

			protected override IList getAssetFunds()
			{
				T.E();

				T.X();
				return AssetFundController.LoadAssetFundLookupsByCompanyAndType
					(GlobalRegistry.ConnectionString,
					GlobalRegistry.CompanyCode,
					fundTypeChosen());
			}

		}

		private abstract /*static*/ class viewFormatterFactory
		{
			public static viewFormatter CreateViewFormatter(FundStaticDataEditor parent)
			{
				T.E();
				viewFormatter result;
				if (parent.authoringMode == authoringModes.Null)
				{
					result = new nullFundViewFormatter(parent);
				}
				else if (parent.authoringMode == authoringModes.New)
				{
					switch (parent.fundTypeCombo.SelectedIndex)
					{
						case 0:
							result = new compositeViewFormatter(parent);
							break;
						case 1:
							result = new linkedFundViewFormatter(parent);
							break;
						case 2:
							result = new oeicFundViewFormatter(parent);
							break;
						default:
							result = new newFundViewFormatter(parent);
							break;
					}
				}

				else if (parent.CurrentFund is Composite)
				{
					result = new compositeViewFormatter(parent);
				}
				else if (parent.CurrentFund is LinkedFund)
				{
					result = new linkedFundViewFormatter(parent);
				}
				else if (parent.CurrentFund is OEICFund)
				{
					result = new oeicFundViewFormatter(parent);
				}
				else
				{
					throw new ArgumentException("Unexpected type passed to FundStaticDataEditor.viewFormatterFactory", "parent");
				}
				T.X();
				return result;
			}
		}

		#endregion

		#region NewFund special case class

		/// <summary>
		/// Special case of fund used for new funds before the type has been defined
		/// </summary>
		/// <pattern>Special Case (see Fowler: Patterns of Enterprise Application Architecture)</pattern>
		private class newFund : Fund
		{
			public newFund() : base()
			{
				T.E();
				T.X();
			}

			public override string FundType
			{
				get { return "New Fund"; }
			}


		}

		#endregion
	}
}