using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Security;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.FundGroups;
using HBOS.FS.AMP.UPD.Types.Funds;
using HBOS.FS.AMP.UPD.Types.Lookups;
using HBOS.FS.AMP.UPD.WinUI.Classes;
using HBOS.FS.AMP.UPD.WinUI.Helpers;
using HBOS.FS.AMP.Windows.Controls;
using HBOS.FS.Support.Tex;
using DataGrid = HBOS.FS.AMP.Windows.Controls.DataGrid;
using RowChangedDelegate = HBOS.FS.AMP.Windows.Controls.DataGrid.RowChangedDelegate;

namespace HBOS.FS.AMP.UPD.WinUI.UserControls
{
	/// <summary>
	/// Summary description for AssetFundStaticDataEditor.
	/// </summary>
	public class AssetFundStaticDataEditor : StaticDataEditor
	{
//		#region New Asset Fund class
//
//		//assetfund is abstract so we have to create one of these
//
//		/// <summary>
//		/// 
//		/// </summary>
//		public class NewAssetFund : AssetFund
//		{
//			#region enums
//
//			/// <summary>
//			/// 
//			/// </summary>
//			public enum NewAssetFundType
//			{
//				/// <summary>
//				/// LinkedFund
//				/// </summary>
//				Linked = 0,
//
//				/// <summary>
//				/// Open Ended Investment Company (OEIC) fund type
//				/// </summary>
//				Oeic = 1,
//
//				/// <summary>
//				/// Composite Price Series
//				/// </summary>
//				Composite = 2,
//
//				/// <summary>
//				/// For a new asset fund - we don't know what type it is yet!
//				/// </summary>
//				Unknown = 3
//			}
//
//			#endregion
//
//			#region Member Variables
//
//			private NewAssetFundType m_assetFundType = NewAssetFundType.Unknown;
//
//			#endregion
//
//			#region Constructors
//
//			/// <summary>
//			/// 
//			/// </summary>
//			public NewAssetFund() : base()
//			{
//				this.IsNew = true;
//			}
//
//			#endregion
//
//			#region Public Methods and Properties
//
//			/// <summary>
//			/// 
//			/// </summary>
//			/// <returns></returns>
//			public AssetFund ConvertToAssetFund()
//			{
//				T.E();
//				AssetFund af = null;
//				try
//				{
//					switch (m_assetFundType)
//					{
//						case NewAssetFundType.Oeic:
//							af = new AssetFund(AssetFundTypeEnum.Oeic);
//							break;
//						case NewAssetFundType.Composite:
//							af = new AssetFund(AssetFundTypeEnum.Composite);
//							break;
//						case NewAssetFundType.Linked:
//							af = new AssetFund(AssetFundTypeEnum.Linked);
//							break;
//						default:
//							throw new ArgumentException("unkown asset fund type");
//					}
//					//set generic fields for all types
//					af.IsNew = true;
//					af.AssetFundCode = this.AssetFundCode;
//					af.FullName = this.FullName;
//					af.ShortName = this.ShortName;
//
//					UPDPrincipal currentPrincipal = (UPDPrincipal) Thread.CurrentPrincipal;
//
//					af.CompanyCode = currentPrincipal.CompanyCode;
//
//					//swap fund groups
//					for (int i = 0; i < this.FundGroups.Count; i++)
//					{
//						af.FundGroups.Add(this.FundGroups[i]);
//					}
//
//				}
//				finally
//				{
//					T.X();
//				}
//				return af;
//			}
//
//			/// <summary>
//			/// 
//			/// </summary>
//			public override string AssetFundTypeString
//			{
//				get { return m_assetFundType.ToString(); }
//			}
//
//			/// <summary>
//			/// 
//			/// </summary>
//			public NewAssetFundType AssetFundTypeForNew
//			{
//				get { return m_assetFundType; }
//				set { m_assetFundType = value; }
//			}
//
//			#endregion
//		}
//
//		#endregion

		#region Controls

		private TabControl tabs;
		private TabPage propertiesTab;
		private ComboBox fundsTypeCombo;
		private Label fundsTypeLabel;
		private TextBox shortNameTextBox;
		private Label shortNameLabel;
		private TextBox fullNameTextBox;
		private Label fullNameLabel;
		private TextBox codeTextBox;
		private Label codeLabel;
		private Label fundLockedLabel;
		private TabPage associationsTab;
		private GroupBox fundGroupGroupBox;
		private Label label1;
		private TabPage factorsTab;
		private GroupBox applyAllGroupBox;
		private TextBox xFactorNarrTextBox;
		private Label label9;
		private TextBox xFactorText;
		private Label label2;
		private DataGrid fundGrid;
		private TabPage tolerancesTab;
		private GroupBox groupBox1;
		private CheckBox priceIncCheck;
		private Label priceIncLabel;
		private TextBox upperTolText;
		private TextBox lowerTolText;
		private DataGrid tolerancesGrid;
		private Panel pnlFundGroupAssocsListToList;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private Container components = null;


		private TextBox assetMovementTolText;
		//private System.Windows.Forms.TabPage indicesTab;
		private Label label6;

		#endregion

		#region Constants

		//2 ways of doing this - we could either load user through db or use roles.
		//Using roles more efficient

		private const string maintainFactorsRole = "MaintainCalculationFactors";
		private const string maintainIndicesRole = "MaintainCalculationIndices";
		private const string maintainTolerancesRole = "MaintainValidationTolerances";

		private const string toleranceFormatString = "###0.0000";

		#endregion

		#region Locals

		private bool m_benchmarksChanged = false;
		private bool m_priceIncreaseOnlyChanged = false;
		private bool m_fundsEdited = false;
		private IList m_cachedFunds = null;

		private enum fundComboItems
		{
			pleaseSelect = 0,
			linked,
			oeic,
			composite
			
		}

		private const string assetFundTypeName = "asset fund";
		private ListToListControl m_fundGroupsListToList = null;
		private DateTimePicker revalEndDateDTPicker;
		private TextBox revalText;
		private TextBox tpeText;
		private Label revalEndDateLabel;
		private Label revalLabel;
		private Label tpeLabel;
		private Label assetFundMovementToleranceLabel;
		private Label lowerToleranceLabel;
		private Label upperToleranceLabel;

		private bool m_associationsTabClicked = false;
		private bool m_factorsTabClicked = false;
		private bool m_tolerancesTabClicked = false;
		private Label label3;
		private DataGrid dataGrid1;
		private GroupBox benchmarkGroupBox;
		private TabPage benchmarkSplitTab;
		private Label indicesTabDescLabel;
		//private System.Windows.Forms.GroupBox indicesGroupBox;
		private bool m_benchmarkTabClicked = false;
		/*
				private const int m_factorTabPos = 2;
		*/
		/*
				private const int m_tolerancesTabPos = 3;
		*/

//		private const int fundsComboUnknownIndex = 0;
//		private const int fundsComboOeicIndex = 3;
//		private const int fundsComboLinkedIndex = 2;
//		private const int fundsComboCompositeIndex = 1;

		private bool m_tabClearing = false; // a flag to indicate we are clearing tabs (which causes events to fire we dont want to)
		private bool m_refreshAssocs = true; //a flag indicating we need to get fund groups and refresh lists
		private bool m_refreshBenchMarks = true; //a flag indicating we need to get indices and refresh lists

		private bool m_systemGeneratedFactorEvent = false; // a flag to indicate that factor events should be ignored as they are system generated (ie not from a user click)
		private bool m_systemGeneratedEvent = false;
		private TreeView treAllBenchMarks;
		private Panel pnlMiddle;
		private Button btnAddBenchmark;
		private Button btnRemoveBenchmark;
		private ListView lstSelectedBenchmarks;
		private Label lblProportionTotal;
		private System.Windows.Forms.Label LabelPriceFile;
		private System.Windows.Forms.ComboBox ComboBoxPriceFile;

		private FundGroupController fundGroupController = new FundGroupController(GlobalRegistry.ConnectionString);

		#endregion

		#region Events, Event handlers, Delegates & event args

		private void listToList_Changing(object sender, ListToListChangingArgs e)
		{
			T.E();
			Changed = true;
			T.X();
		}

		private void benchMarkTreeToListChanging(object sender, ListToListChangingArgs e)
		{
			T.E();
			Changed = true;
			m_benchmarksChanged = true;
			T.X();
		}

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor for <see cref="AssetFundStaticDataEditor" />
		/// </summary>
		public AssetFundStaticDataEditor()
		{
			T.E();
			try
			{
				// This call is required by the Windows.Forms Form Designer.
				InitializeComponent();

				//custom initialisation
				fundGrid.RowChanged += new DataGrid.RowChangedDelegate(gridRowChanged);
				tolerancesGrid.RowChanged += new DataGrid.RowChangedDelegate(gridRowChanged);

				createSelectedBenchmarksList();
			}
			finally
			{
				T.X();
			}
		}


		private void lstSelectedBenchmarks_AfterLabelEdit(object sender, LabelEditEventArgs e)
		{
			if (e.Label != null && selectedColumns[0].propertyName != string.Empty)
			{
				T.E();
				try
				{
					decimal proportionPercentage = decimal.Parse(e.Label);
					try
					{
						int itemIndex = this.treeHelper.Included.IndexOf(lstSelectedBenchmarks.Items[e.Item].Tag);
						AssetMovementConstituentDecorator AssetMovementConstituentDecorator = (AssetMovementConstituentDecorator) this.treeHelper.Included[itemIndex];
						AssetMovementConstituentDecorator.Proportion = proportionPercentage/100;
						Changed = true;
						m_benchmarksChanged = true;
						e.CancelEdit = true;
						this.FillSelectedBenchMarkList();
					}
					catch (ConstraintViolationException ex)
					{
						e.CancelEdit = true;
						GUIExceptionHelper.LogAndDisplayException("InvalidProportionValueEnteredBody", "InvalidValueEnteredTitle", ex);
					}
				}
				catch (FormatException fex)
				{
					e.CancelEdit = true;
					GUIExceptionHelper.LogAndDisplayException("UnableToConvertEnteredProportionBody", "InvalidValueEnteredTitle", fex);
				}
				finally
				{
					T.X();
				}
			}
		}


		private ListToListControl.ColumnCollection selectedColumns;

		private void createSelectedBenchmarksList()
		{
			selectedColumns = new ListToListControl.ColumnCollection(this.lstSelectedBenchmarks);

			selectedColumns.Add("ProportionDisplay", "%", 55);
			selectedColumns.Add("BenchmarkDisplayName", "Benchmark Name (Country/Currency Code)", 250);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets a value indicating whether any of the controls have changed (not the AssetFund itself).
		/// e.g. whether a textbox has been typed in
		/// </summary>
		/// <value>
		/// 	<c>true</c> if changed; otherwise, <c>false</c>.
		/// </value>
		protected override bool Changed
		{
			get { return base.Changed; }
			set
			{
				T.E();
				try
				{
					base.Changed = value;
					if (!value)
					{
						//if the controller sets this flag to false then user must have just saved
						//successfully.
						//Therefore we can clear any errors...
						clearErrors();

						//...and reset any missing tabs if a new item was saved

						if (!tabs.TabPages.Contains(tolerancesTab))
						{
							adjustTabDisplay();

						}
					}
				}
				finally
				{
					T.X();
				}
			}
		}


		private AssetFund currentAssetFund;

		/// <summary>
		/// Gets or sets the current asset fund being edited.
		/// </summary>
		/// <value></value>
		public AssetFund CurrentAssetFund
		{
			get { return currentAssetFund; }
			set
			{
				T.E();
				try
				{
					bool indexChanged = (currentAssetFund != value);
					//in cosultation with richard - always swap back to properties tab
					//so that the locked message is seen
					if (currentAssetFund == null || currentAssetFund.AssetFundCode != value.AssetFundCode)
					{
						tabs.SelectedTab = propertiesTab;
					}
					currentAssetFund = value;
					m_priceIncreaseOnlyChanged = false;
					m_associationsTabClicked = false;
					m_factorsTabClicked = false;
					m_tolerancesTabClicked = false;
					m_benchmarkTabClicked = false;

					m_refreshAssocs = true;
					m_refreshBenchMarks = true;


					displayAssetFund(indexChanged);

					Changed = false;
					m_benchmarksChanged = false;
				}
				finally
				{
					T.X();
				}
			}
		}

		#endregion

		#region Methods

		//		/// <summary>
		//		/// Display the validation errors in the UI
		//		/// </summary>
		//		/// <param name="assetFundIDErrorMessage"></param>
		//		/// <param name="fullNameErrorMessage"></param>
		//		/// <param name="shortNameErrorMessage"></param>
		//		/// <param name="assetFundTypeErrorMessage"></param>
		//		/// <param name="mvSplitErrMessage"></param>
		//		/// <param name="xFactorErrorMsg"></param>
		//		/// <param name="revalFactorErrorMsg"></param>
		//		/// <param name="tpeErrorMsg"></param>
		//		/// <param name="scalingFactorErrorMsg"></param>
		//		/// <param name="upperTolErrorMsg"></param>
		//		/// <param name="lowerTolErrorMsg"></param>
		//		/// <param name="tolerancesErrorMsg"></param>
		//		/// <param name="assetMovementToleranceErrorMessage"></param>
		//		public void ShowErrors(string assetFundIDErrorMessage, string fullNameErrorMessage, string shortNameErrorMessage, string assetFundTypeErrorMessage, string mvSplitErrMessage,
		//		                       string xFactorErrorMsg, string revalFactorErrorMsg, string tpeErrorMsg, string scalingFactorErrorMsg, string upperTolErrorMsg, string lowerTolErrorMsg, string tolerancesErrorMsg,
		//		                       string assetMovementToleranceErrorMessage)
		//		{
		//			T.E();
		//			try
		//			{
		//				bool propertyError = false;
		//				bool factorError = false;
		//				bool tolerancesError = false;
		//
		//				clearErrors();
		//
		//				bool isValid = true;
		//				if (assetFundIDErrorMessage != null && assetFundIDErrorMessage.Length > 0)
		//				{
		//					propertyError = true;
		//					isValid = false;
		//					codeTextBox.Focus();
		//					setError(codeTextBox, assetFundIDErrorMessage);
		//				}
		//
		//				if (fullNameErrorMessage != null && fullNameErrorMessage.Length > 0)
		//				{
		//					propertyError = true;
		//					if (isValid) //if focus not already determined
		//					{
		//						fullNameTextBox.Focus();
		//					}
		//					isValid = false;
		//					setError(fullNameTextBox, fullNameErrorMessage);
		//				}
		//
		//				if (shortNameErrorMessage != null && shortNameErrorMessage.Length > 0)
		//				{
		//					propertyError = true;
		//					if (isValid) //if focus not already determined
		//					{
		//						shortNameTextBox.Focus();
		//					}
		//					isValid = false;
		//					setError(shortNameTextBox, shortNameErrorMessage);
		//				}
		//
		//				if (assetFundTypeErrorMessage != null && assetFundTypeErrorMessage.Length > 0)
		//				{
		//					propertyError = true;
		//					if (isValid) //if focus not already determined
		//					{
		//						fundsTypeCombo.Focus();
		//					}
		//					isValid = false;
		//					setError(fundsTypeCombo, assetFundTypeErrorMessage);
		//				}
		//
		//				if (mvSplitErrMessage != null && mvSplitErrMessage.Length > 0)
		//				{
		//					if (isValid) //if focus not already determined
		//					{
		//						this.lstSelectedBenchmarks.Focus();
		//					}
		//					isValid = false;
		//					setError(this.lstSelectedBenchmarks, mvSplitErrMessage);
		//				}
		//
		//				if (xFactorErrorMsg != null && xFactorErrorMsg.Length > 0)
		//				{
		//					if (isValid) xFactorText.Focus();
		//					factorError = true;
		//					setError(xFactorText, xFactorErrorMsg);
		//					isValid = false;
		//				}
		//
		//				if (tpeErrorMsg != null && tpeErrorMsg.Length > 0)
		//				{
		//					if (isValid) tpeText.Focus();
		//					factorError = true;
		//					setError(tpeText, tpeErrorMsg);
		//					isValid = false;
		//				}
		//
		//				if (revalFactorErrorMsg != null && revalFactorErrorMsg.Length > 0)
		//				{
		//					if (isValid)
		//					{
		//						if (revalText.Text.Length > 0)
		//						{
		//							revalText.Focus();
		//							setError(revalText, revalFactorErrorMsg);
		//						}
		//						else //must be date thats wrong
		//						{
		//							revalEndDateDTPicker.Focus();
		//							setError(revalEndDateDTPicker, revalFactorErrorMsg);
		//						}
		//					}
		//					factorError = true;
		//					isValid = false;
		//				}
		//
		//
		//				/* todo - scaling factor missing!!
		//				if (scalingFactorErrorMsg != null && scalingFactorErrorMsg.Length > 0)
		//				{
		//					if (isValid) scale.Focus();
		//					factorError = true;
		//					setError(fundGrid, scalingFactorErrorMsg);
		//					isValid = false;
		//				}
		//				*/
		//
		//				if (lowerTolErrorMsg != null && lowerTolErrorMsg.Length > 0)
		//				{
		//					if (isValid) lowerTolText.Focus();
		//					tolerancesError = true;
		//					setError(lowerTolText, lowerTolErrorMsg);
		//					isValid = false;
		//				}
		//
		//				if (upperTolErrorMsg != null && upperTolErrorMsg.Length > 0)
		//				{
		//					if (isValid) upperTolText.Focus();
		//					tolerancesError = true;
		//					setError(upperTolText, upperTolErrorMsg);
		//					isValid = false;
		//				}
		//
		//				if (tolerancesErrorMsg != null && tolerancesErrorMsg.Length > 0)
		//				{
		//					bool isUpperTolError = upperTolErrorMsg == null || upperTolErrorMsg.Length == 0;
		//
		//					tolerancesError = true;
		//
		//					if (upperTolText.Text.Length == 0 && lowerTolText.Text.Length > 0)
		//					{
		//						//see if we're comparing upper tolerance to lower tolerance
		//						string lcaseMsg = tolerancesErrorMsg.ToLower();
		//						if (lcaseMsg.IndexOf("upper tolerance") >= 0 && lcaseMsg.IndexOf("lower tolerance") >= 0)
		//						{
		//							isUpperTolError = false;
		//						}
		//					}
		//
		//					if (isUpperTolError)
		//					{
		//						setError(upperTolText, tolerancesErrorMsg);
		//						if (isValid) upperTolText.Focus();
		//					}
		//					else
		//					{
		//						setError(lowerTolText, tolerancesErrorMsg);
		//						if (isValid) lowerTolText.Focus();
		//					}
		//					isValid = false;
		//				}
		//
		//				if (assetMovementToleranceErrorMessage != null && assetMovementToleranceErrorMessage.Length > 0)
		//				{
		//					if (isValid) assetMovementTolText.Focus();
		//					tolerancesError = true;
		//					setError(assetMovementTolText, assetMovementToleranceErrorMessage);
		//					isValid = false;
		//				}
		//
		//				if (propertyError)
		//				{
		//					//these errors are all on properties tab, so display this so users can see the errors
		//					tabs.SelectedTab = propertiesTab;
		//				}
		//				else if (factorError)
		//				{
		//					tabs.SelectedTab = factorsTab;
		//				}
		//				else if (tolerancesError)
		//				{
		//					tabs.SelectedTab = tolerancesTab;
		//				}
		//				else if (mvSplitErrMessage != null && mvSplitErrMessage.Length > 0)
		//				{
		//					tabs.SelectedTab = benchmarkSplitTab;
		//				}
		//
		//				if (!isValid) showErrorDialog("This asset fund cannot be saved:");
		//			}
		//			finally
		//			{
		//				T.X();
		//			}
		//		}

//		/// <summary>
//		/// Swaps from NewAssetFund to AssetFund of particular type
//		/// PRECONDITION: May only be called after NewAssetFund is validated!
//		/// </summary>
//		public void SwapNewAssetFundForTrueAssetFund()
//		{
//			T.E();
//			try
//			{
//				//now the user has opted to save a VALID asset fund, we can create a specific instance from the generic new type
//				//and swap values
//
//				//fix direct rather than through the set as we don't want to do display processing here
//
//				NewAssetFund newAssetFund = CurrentAssetFund as NewAssetFund;
//				if (newAssetFund != null)
//				{
//					currentAssetFund = newAssetFund.ConvertToAssetFund();
//				}
//			}
//			finally
//			{
//				T.X();
//			}
//
//		}

		/// <summary>
		/// Updates the fund group entity from the view.
		/// </summary>
		public void UpdateAssetFund()
		{
			T.E();
			try
			{
				if (Changed)
				{
					AssetFund currAssetFund = CurrentAssetFund;
					currAssetFund.AssetFundCode = codeTextBox.Text;
					UPDPrincipal currentPrincipal = (UPDPrincipal) Thread.CurrentPrincipal;
					currAssetFund.CompanyCode = currentPrincipal.CompanyCode;
					switch (fundsTypeCombo.SelectedIndex)
					{
						case 1:
							currAssetFund.AssetFundType = AssetFund.AssetFundTypeEnum.Linked;
							break;
						case 2:
							currAssetFund.AssetFundType = AssetFund.AssetFundTypeEnum.Oeic;
							break;
						case 3:
							currAssetFund.AssetFundType = AssetFund.AssetFundTypeEnum.Composite;
							break;
					//	default:
					//		throw new ArgumentException("Unknown fund type combo box index");

					}

					currAssetFund.ShortName = shortNameTextBox.Text;
					currAssetFund.FullName = fullNameTextBox.Text;

					//note: only allow fund type to be changed for new ones

					FundCollection funds = null;
					//do the inline edit first so that global change preference and so that we don't overwrite
					//globally changed value with the old grid value

					if (m_factorsTabClicked || m_tolerancesTabClicked ||
						tabs.SelectedTab == factorsTab || tabs.SelectedTab == tolerancesTab)
					{
						funds = retrieveFunds();
						//if funds have been looked at, retrieve them and 
						if (funds != null && funds.Count > 0)
						{
							//update inline edited values, but then override with an 'all' value if necessary
							if (m_factorsTabClicked || tabs.SelectedTab == factorsTab)
							{
								updateInlineEditedFundsForFactors(funds);
							}
							if (m_tolerancesTabClicked || tabs.SelectedTab == tolerancesTab)
							{
								updateInlineEditedFundsForTolerances(funds);
							}
						}
					}

					if (m_associationsTabClicked || tabs.SelectedTab == associationsTab) //else is old data for old obj
					{
						updateFundGroupAssociations();
					}
					if (m_factorsTabClicked || tabs.SelectedTab == factorsTab) //else is old data for old obj
					{
						updateFactors(funds);
					}
					if (m_tolerancesTabClicked || tabs.SelectedTab == tolerancesTab)
					{
						updateTolerances(funds);
					}
					if (m_benchmarkTabClicked || tabs.SelectedTab == benchmarkSplitTab)
					{
						updateBenchamrks();
					}

					m_priceIncreaseOnlyChanged = false;
				}
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// This method to be invoked when an invalid asset fund value has been propogated through the funds and 
		/// so the original value needs to be restored
		/// </summary>
		/// <param name="existingFunds"></param>
		/// <param name="amToleranceValid"></param>
		/// <param name="upperToleranceValid"></param>
		/// <param name="lowerToleranceValid"></param>
		private void refreshValidValuesFromFundsGrid(FundCollection existingFunds, bool amToleranceValid, bool upperToleranceValid, bool lowerToleranceValid)
		{
			T.E();
			try
			{
				FundCollection gridFunds = (FundCollection) tolerancesGrid.RetrieveUpdatedCustomCollection();
				if (gridFunds != null)
				{
					//copy just the data that its possible to edit
					for (int i = 0; i < gridFunds.Count; i++)
					{
						Fund gridFund = gridFunds[i];
						if (gridFund != null)
						{
							Fund existingFund = existingFunds[i];
							if (existingFund != null)
							{
								if (!amToleranceValid)
								{
									existingFund.AssetMovementTolerance = gridFund.AssetMovementTolerance;
								}
								if (!upperToleranceValid)
								{
									existingFund.UpperTolerance = gridFund.UpperTolerance;
								}
								if (!lowerToleranceValid)
								{
									existingFund.LowerTolerance = gridFund.LowerTolerance;
								}
							}
						}
					}
				}
			}
			finally
			{
				T.X();
			}
		}

		#endregion

		#region Dispose

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			T.E();
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
			T.X();
		}

		#endregion

		#region Private methods / properties

		private void clearErrors()
		{
			T.E();
			try
			{
				clearErrors(tabs);
			}
			finally
			{
				T.X();
			}
		}

		private void updateFactors(FundCollection funds)
		{
			T.E();
			try
			{
				//if any modifications made, filter them down to all funds
				//note - don't allow updates to ALL in addition to individual changes
				if (xFactorText.Text.Length > 0 || xFactorNarrTextBox.Text.Length > 0 ||
					((!(CurrentAssetFund.AssetFundType == AssetFund.AssetFundTypeEnum.Oeic)) &&
						(revalText.Text.Length > 0 || tpeText.Text.Length > 0 || revalEndDateDTPicker.Text.Length > 0)))
				{
					bool xFactorOK = false;
					decimal xFactor = 0M;

					if (xFactorText.Text.Length > 0)
					{
						try
						{
							xFactor = (decimal.Parse(xFactorText.Text))/100;
							xFactorOK = true;
						}
						catch
						{
						}
					}

					bool tpeOK = false;
					decimal tpe = 0M;
					if (tpeText.Text.Length > 0)
					{
						try
						{
							tpe = (decimal.Parse(tpeText.Text))/100;
							tpeOK = true;
						}
						catch
						{
						}
					}

					bool revalOK = false;
					decimal reval = 0M;

					if (revalText.Text.Length > 0)
					{
						try
						{
							reval = (decimal.Parse(revalText.Text))/100;
							revalOK = true;
						}
						catch
						{
						}
					}

					DateTime revalEndDate = DateTime.MinValue;
					//date is only used if a revaluation factor is entered
					if (revalEndDateDTPicker.Checked && revalEndDateDTPicker.Text.Length > 0)
					{
						try
						{
							revalEndDate = revalEndDateDTPicker.Value;
						}
						catch
						{
						}
					}


					if (funds != null)
					{
						//update all funds
						for (int i = 0; i < funds.Count; i++)
						{
							Fund fund = funds[i];
							if (!fund.IsAuthorised) //can only update non authorised funds (confirmed with Kev!)
							{
								if (xFactorOK)
								{
									fund.XFactor = xFactor;
								}
								if (xFactorNarrTextBox.Text.Length > 0)
								{
									//todo validate this in ui
									fund.XFactorDescription = xFactorNarrTextBox.Text;
								}

								if (!(fund is OEICFund))
								{
									LinkedFund lnkedFnd = fund as LinkedFund;
									Composite comp = fund as Composite;

									if (lnkedFnd != null || comp != null)
									{
										if (tpeOK)
										{
											if (lnkedFnd != null)
											{
												if (lnkedFnd.IsLife)
												{
													lnkedFnd.TPE = tpe;
												}
											}
											else
											{
												if (comp.IsLife)
												{
													comp.TPE = tpe;
												}
											}
										}
										if (revalOK)
										{
											if (lnkedFnd != null)
											{
												lnkedFnd.RevalFactor = reval;
											}
											else
											{
												comp.RevalFactor = reval;
											}
										}
										if (revalEndDate != DateTime.MinValue)
										{
											if (lnkedFnd != null)
											{
												lnkedFnd.RevaluationEndDate = revalEndDate;
											}
											else
											{
												comp.RevaluationEndDate = revalEndDate;
											}
										}
										if (revalOK || revalEndDate != DateTime.MinValue)
										{
											//make sure we have a valid effective date
											if (lnkedFnd != null)
											{
												if (lnkedFnd.RevaluationEffectiveDate == DateTime.MinValue)
												{
													lnkedFnd.RevaluationEffectiveDate = DateTime.Today;
													;
												}
											}
											else
											{
												if (comp.RevaluationEffectiveDate == DateTime.MinValue)
												{
													comp.RevaluationEffectiveDate = DateTime.Today;
												}
											}
										}
									}
								}
							}
						} //end for loop (foreach fund)

					} //endif: funds != null

				} //endif - modifications made
			}
			finally
			{
				T.X();
			}
		}

		private static void updateInlineEditedFactors(Fund existingFund, Fund gridFund)
		{
			T.E();
			try
			{
				//just swap the user editable values
				if (existingFund.XFactor != gridFund.XFactor)
				{
					//user will have typed a value that then needs to be made a percentage
					existingFund.XFactor = gridFund.XFactor;
				}
				if (existingFund is LinkedFund)
				{
					bool linkedFundRevalFactorChanged = false;
					LinkedFund existingLinked = (LinkedFund) existingFund;
					LinkedFund gridLinked = (LinkedFund) gridFund;
					if (existingLinked.RevalFactor != gridLinked.RevalFactor)
					{
						existingLinked.RevalFactor = gridLinked.RevalFactor;
						linkedFundRevalFactorChanged = true;
					}

					if (existingLinked.RevaluationEndDate != gridLinked.RevaluationEndDate)
					{
						existingLinked.RevaluationEndDate = gridLinked.RevaluationEndDate;
						linkedFundRevalFactorChanged = true;
					}

					if (linkedFundRevalFactorChanged)
					{
						if (existingLinked.RevaluationEffectiveDate == DateTime.MinValue)
						{
							existingLinked.RevaluationEffectiveDate = DateTime.Today;
						}
					}

					if (existingLinked.TPE != gridLinked.TPE)
					{
						existingLinked.TPE = gridLinked.TPE;
					}

				}
				else if (existingFund is Composite)
				{
					bool compositeRevalFactorChanged = false;
					Composite existingComposite = (Composite) existingFund;
					Composite gridComposite = (Composite) gridFund;
					if (existingComposite.RevalFactor != gridComposite.RevalFactor)
					{
						existingComposite.RevalFactor = gridComposite.RevalFactor;
						compositeRevalFactorChanged = true;
					}

					if (existingComposite.RevaluationEndDate != gridComposite.RevaluationEndDate)
					{
						existingComposite.RevaluationEndDate = gridComposite.RevaluationEndDate;
						compositeRevalFactorChanged = true;
					}

					if (compositeRevalFactorChanged)
					{
						if (existingComposite.RevaluationEffectiveDate == DateTime.MinValue)
						{
							existingComposite.RevaluationEffectiveDate = DateTime.Today;
						}
					}

					if (existingComposite.TPE != gridComposite.TPE)
					{
						existingComposite.TPE = gridComposite.TPE;
					}
				}
			}
			finally
			{
				T.X();
			}
		}

		private static void updateInlineEditedTolerances(Fund existingFund, Fund gridFund)
		{
			T.E();
			try
			{
				if (existingFund.UpperTolerance != gridFund.UpperTolerance)
				{
					existingFund.UpperTolerance = gridFund.UpperTolerance;
				}
				if (existingFund.LowerTolerance != gridFund.LowerTolerance)
				{
					existingFund.LowerTolerance = gridFund.LowerTolerance;
				}
				if (existingFund.PriceIncreaseOnly != gridFund.PriceIncreaseOnly)
				{
					existingFund.PriceIncreaseOnly = gridFund.PriceIncreaseOnly;
				}
			}
			finally
			{
				T.X();
			}
		}

		private void updateInlineEditedFundsForFactors(FundCollection existingFunds)
		{
			T.E();
			try
			{
				//update the inline edited funds
				FundCollection gridFunds = (FundCollection) fundGrid.RetrieveUpdatedCustomCollection();
				if (gridFunds != null)
				{
					//copy just the data that its possible to edit
					for (int i = 0; i < gridFunds.Count; i++)
					{
						Fund gridFund = gridFunds[i];
						if (gridFund != null)
						{
							Fund existingFund = existingFunds[i];
							if (existingFund != null)
							{
								updateInlineEditedFactors(existingFund, gridFund);
							}
						}
					}
				}
			}
			finally
			{
				T.X();
			}
		}

		private void updateInlineEditedFundsForTolerances(FundCollection existingFunds)
		{
			T.E();
			try
			{
				FundCollection gridFunds = (FundCollection) tolerancesGrid.RetrieveUpdatedCustomCollection();
				if (gridFunds != null)
				{
					//copy just the data that its possible to edit
					for (int i = 0; i < gridFunds.Count; i++)
					{
						Fund gridFund = gridFunds[i];
						if (gridFund != null)
						{
							Fund existingFund = existingFunds[i];
							if (existingFund != null)
							{
								updateInlineEditedTolerances(existingFund, gridFund);
							}
						}
					}
				}
			}
			finally
			{
				T.X();
			}

		}

		private void updateTolerances(FundCollection funds)
		{
			T.E();
			try
			{
				//if any modifications made, filter them down to all funds
				decimal amTolerance = 0M;
				bool amToleranceOK = false;
				if (assetMovementTolText.Text.Length > 0)
				{
					if (assetMovementTolText.Text.Length > 0)
					{
						//todo - validate in UI
						try
						{
							amTolerance = decimal.Parse(assetMovementTolText.Text)/100;
							amToleranceOK = true;
						}
						catch
						{
						}
					}
				}
				decimal upperTolerance = 0M;
				bool upperToleranceOK = false;

				if (upperTolText.Text.Length > 0)
				{
					//todo - validate in UI
					try
					{
						upperTolerance = decimal.Parse(upperTolText.Text)/100;
						upperToleranceOK = true;
					}
					catch
					{
					}
				}

				decimal lowerTolerance = 0M;
				bool lowerToleranceOK = false;

				if (lowerTolText.Text.Length > 0)
				{
					//todo - validate in UI
					try
					{
						lowerTolerance = decimal.Parse(lowerTolText.Text)/100;
						lowerToleranceOK = true;
					}
					catch
					{
					}
				}

				if (funds != null)
				{
					for (int i = 0; i < funds.Count; i++)
					{
						Fund fund = funds[i];
						if (amToleranceOK)
						{
							fund.AssetMovementTolerance = amTolerance;
						}
						if (upperToleranceOK)
						{
							fund.UpperTolerance = upperTolerance;
						}
						if (lowerToleranceOK)
						{
							fund.LowerTolerance = lowerTolerance;
						}
						if (m_priceIncreaseOnlyChanged)
						{
							if (priceIncCheck.CheckState == CheckState.Checked)
							{
								fund.PriceIncreaseOnly = true;
							}
							else if (priceIncCheck.CheckState == CheckState.Unchecked)
							{
								fund.PriceIncreaseOnly = false;
							}
							else
							{
								//can't (or rather shouldn't) happen!
								throw new ArgumentException("invalid price increase only state");
							}
						}
					}
				}
				//set the amTolerance on the asset fund for validation by the asset fund controller
				if (CurrentAssetFund != null)
				{
					CurrentAssetFund.AssetMovementTolerance = amTolerance;
				}
			}
			finally
			{
				T.X();
			}
		}

		private void updateBenchamrks()
		{
			T.E();
			try
			{
				if (m_benchmarksChanged)
				{
					//KAJ 12/05/05 
					//Set the Asset Fund for all included benchmarks
					AssetMovementConstituentCollection constituents = (AssetMovementConstituentCollection) AssetMovementConstituentDecorator.FromDecoratedToConstituentList(this.treeHelper.Included);
					foreach (AssetMovementConstituent constituent in constituents)
					{
						constituent.ParentAssetFund = CurrentAssetFund;
					}
					
					//Associate the benchmarks with the Asset Fund
					CurrentAssetFund.AssetMovementConstituents = constituents;
				}
			}
			finally
			{
				T.X();
			}

		}

		private void updateFundGroupAssociations()
		{
			T.E();
			try
			{
				if (CurrentAssetFund != null && m_fundGroupsListToList != null)
				{
					FundGroupCollection fundGroups = CurrentAssetFund.FundGroups;
					fundGroups.Clear();
					IList selItems = m_fundGroupsListToList.SelectedItems();
					if (selItems != null)
					{
						for (int i = 0; i < selItems.Count; i++)
						{
							fundGroups.Add((FundGroup) selItems[i]);
						}
					}

					this.CurrentAssetFund.ShortName = shortNameTextBox.Text;
					this.CurrentAssetFund.FullName = fullNameTextBox.Text;
					this.CurrentAssetFund.PriceFileId=(int)ComboBoxPriceFile.SelectedValue;
					//note only allow fund type to be changed for new ones

					//always set this, so that association change sets the flag.
					CurrentAssetFund.IsDirty = true;
				}
			}
			finally
			{
				T.X();
			}
		}

		private void addFactorsGridStyles(DataGridTableStyle fundGridStyle)
		{
			int numDecimalPlacesForPct = 4;  // number of decimal places to display percent

			T.E();
			try
			{
				//Create the columns
				DataGridTextBoxReadOnlyColumn newCol = new DataGridTextBoxReadOnlyColumn();
				newCol.MappingName = "FullName";
				newCol.HeaderText = "Fund Name";
				newCol.Width = 220;
				fundGridStyle.GridColumnStyles.Add(newCol);

				newCol = new DataGridTextBoxReadOnlyColumn();
				newCol.MappingName = "FundType";
				newCol.HeaderText = "Fund Type";
				newCol.Width = 100;
				fundGridStyle.GridColumnStyles.Add(newCol);

				if (!(CurrentAssetFund.AssetFundType == AssetFund.AssetFundTypeEnum.Oeic))
				{
					newCol = new DataGridTextBoxReadOnlyColumn();
					newCol.MappingName = "IsLifeDisplay";
					newCol.HeaderText = "Is Life";
					newCol.Width = 40;
					newCol.Alignment = HorizontalAlignment.Right;
					fundGridStyle.GridColumnStyles.Add(newCol);
				}

				DataGridPercentageColumn newUpdateCol = new DataGridPercentageColumn();
				newUpdateCol.MappingName = "XFactor";
				newUpdateCol.HeaderText = "X Factor";
				newUpdateCol.Width = 100;
				newUpdateCol.Alignment = HorizontalAlignment.Right;
				newUpdateCol.AllowNegative = true;
				newUpdateCol.DecimalPlaces = numDecimalPlacesForPct;

				fundGridStyle.GridColumnStyles.Add(newUpdateCol);

				if (!(CurrentAssetFund.AssetFundType == AssetFund.AssetFundTypeEnum.Oeic))
				{
					newUpdateCol = new DataGridPercentageColumn();
					newUpdateCol.MappingName = "TPE";
					newUpdateCol.HeaderText = "TPE";
					newUpdateCol.Width = 100;
					newUpdateCol.Alignment = HorizontalAlignment.Right;
					newUpdateCol.AllowNegative = true;
					newUpdateCol.DecimalPlaces = numDecimalPlacesForPct;

					fundGridStyle.GridColumnStyles.Add(newUpdateCol);

					newUpdateCol = new DataGridPercentageColumn();
					newUpdateCol.MappingName = "RevalFactor";
					newUpdateCol.HeaderText = "Revaluation Factor";
					newUpdateCol.Width = 100;
					newUpdateCol.Alignment = HorizontalAlignment.Right;
					newUpdateCol.AllowNegative = true;
					newUpdateCol.DecimalPlaces = numDecimalPlacesForPct;

					fundGridStyle.GridColumnStyles.Add(newUpdateCol);

					DataGridDateColumn newDateUpdateCol = new DataGridDateColumn();
					newDateUpdateCol.MappingName = "RevaluationEffectiveDate";
					newDateUpdateCol.HeaderText = "Reval Start Date";
					newDateUpdateCol.Width = 100;
					newDateUpdateCol.Alignment = HorizontalAlignment.Right;
					fundGridStyle.GridColumnStyles.Add(newDateUpdateCol);

					newDateUpdateCol = new DataGridDateColumn();
					newDateUpdateCol.MappingName = "RevaluationEndDate";
					newDateUpdateCol.HeaderText = "Reval End Date";
					newDateUpdateCol.Width = 100;
					newDateUpdateCol.Alignment = HorizontalAlignment.Right;
					fundGridStyle.GridColumnStyles.Add(newDateUpdateCol);

				}

				newCol = new DataGridTextBoxReadOnlyColumn();
				newCol.MappingName = "IsAuthorisedDisplay";
				newCol.HeaderText = "Authorised";
				newCol.Width = 60;
				newCol.Alignment = HorizontalAlignment.Right;
				fundGridStyle.GridColumnStyles.Add(newCol);
			}
			finally
			{
				T.X();
			}
		}

		private void addTolerancesGridStyles(DataGridTableStyle fundGridStyle)
		{
			T.E();
			try
			{
				//Create the columns
				DataGridTextBoxReadOnlyColumn newCol = new DataGridTextBoxReadOnlyColumn();
				newCol.MappingName = "FullName";
				newCol.HeaderText = "Fund Name";
				newCol.Width = 220;
				fundGridStyle.GridColumnStyles.Add(newCol);

				newCol = new DataGridTextBoxReadOnlyColumn();
				newCol.MappingName = "FundType";
				newCol.HeaderText = "Fund Type";
				newCol.Width = 120;
				fundGridStyle.GridColumnStyles.Add(newCol);

				DataGridPercentageColumn newUpdatePercentCol = new DataGridPercentageColumn();
				newUpdatePercentCol.MappingName = "AssetMovementTolerance";
				newUpdatePercentCol.HeaderText = "AM Tolerance";
				newUpdatePercentCol.Width = 120;
				newUpdatePercentCol.AllowNegative = false;
				newUpdatePercentCol.DecimalPlaces = 4;
				newUpdatePercentCol.ReadOnly = true;
				fundGridStyle.GridColumnStyles.Add(newUpdatePercentCol);

				newUpdatePercentCol = new DataGridPercentageColumn();
				newUpdatePercentCol.MappingName = "LowerTolerance";
				newUpdatePercentCol.HeaderText = "Lower Tolerance";
				newUpdatePercentCol.Width = 120;
				newUpdatePercentCol.AllowNegative = false;
				newUpdatePercentCol.DecimalPlaces = 4;
				fundGridStyle.GridColumnStyles.Add(newUpdatePercentCol);

				newUpdatePercentCol = new DataGridPercentageColumn();
				newUpdatePercentCol.MappingName = "UpperTolerance";
				newUpdatePercentCol.HeaderText = "Upper Tolerance";
				newUpdatePercentCol.Width = 120;
				newUpdatePercentCol.AllowNegative = false;
				newUpdatePercentCol.DecimalPlaces = 4;
				fundGridStyle.GridColumnStyles.Add(newUpdatePercentCol);

				DataGridBool1ClickColumn newUpdateBoolCol = new DataGridBool1ClickColumn();
				newUpdateBoolCol.MappingName = "PriceIncreaseOnly";
				newUpdateBoolCol.HeaderText = "Price Increase Only";
				newUpdateBoolCol.Width = 120;
				fundGridStyle.GridColumnStyles.Add(newUpdateBoolCol);

				newCol = new DataGridTextBoxReadOnlyColumn();
				newCol.MappingName = "FundStatus";
				newCol.HeaderText = "Authorisation Status";
				newCol.Width = 120;
				fundGridStyle.GridColumnStyles.Add(newCol);
			}
			finally
			{
				T.X();
			}

		}


		/*
				private static void addCompositionGridStyles(DataGridTableStyle fundGridStyle)
				{
					T.E();
					try
					{
						//Create the columns
						DataGridTextBoxReadOnlyColumn newCol = new DataGridTextBoxReadOnlyColumn();
						newCol.MappingName = "DisplayName";
						newCol.HeaderText = "Name";
						newCol.Width = 120;
						fundGridStyle.GridColumnStyles.Add(newCol);

						newCol = new DataGridTextBoxReadOnlyColumn();
						newCol.MappingName = "Proportion";
						newCol.HeaderText = "Proportion";
						newCol.Width = 120;
						fundGridStyle.GridColumnStyles.Add(newCol);
					}
					finally
					{
						T.X();
					}
				}
		*/

		/// <summary>
		/// Set up the grid
		/// </summary>
		private void addDataGridStyles()
		{
			T.E();
			try
			{
				if (CurrentAssetFund != null)
				{
					DataGridTableStyle fundGridStyle = null;
					DataGrid currGrid = getGrid();

					//Create a new DataGridTableStyle and set MappingName.
					if (currGrid.TableStyles.Count == 0)
					{
						fundGridStyle = new DataGridTableStyle();
						fundGridStyle.MappingName = "";
						fundGridStyle.AlternatingBackColor = Color.WhiteSmoke;
						currGrid.TableStyles.Add(fundGridStyle);
					}
					else
					{
						fundGridStyle = currGrid.TableStyles[0];
						fundGridStyle.GridColumnStyles.Clear();
					}

					if (tabs.SelectedTab == factorsTab)
					{
						addFactorsGridStyles(fundGridStyle);
					}
					else if (tabs.SelectedTab == tolerancesTab)
					{
						addTolerancesGridStyles(fundGridStyle);
					}
				}

			}
			finally
			{
				T.X();
			}
		}

		private DataGrid getGrid()
		{
			T.E();
			try
			{
				DataGrid currGrid = null;
				if (tabs.SelectedTab == factorsTab)
				{
					currGrid = fundGrid;
				}
				else if (tabs.SelectedTab == tolerancesTab)
				{
					currGrid = tolerancesGrid;
				}
				return currGrid;
			}
			finally
			{
				T.X();
			}

		}

		private void refreshDataGrid(IList gridItems)
		{
			T.E();
			try
			{
				DataGrid currGrid = getGrid();

				//Display the asset funds on the grid (if there are any)
				if (gridItems.Count > 0)
				{
					addDataGridStyles();
					currGrid.BindToCustomCollection(gridItems); // , typeof(AssetFund)
					currGrid.Visible = true;
				}
				else
				{
					currGrid.DataSource = null;
					currGrid.Visible = false;
				}
			}
			finally
			{
				T.X();
			}

		}

		private bool userHasPermissionToViewTab(TabPage tab)
		{
			T.E();
			try
			{
				UPDPrincipal currentPrincipal = (UPDPrincipal) Thread.CurrentPrincipal;
				if (tab == factorsTab)
				{
					return currentPrincipal.IsInRole(maintainFactorsRole);
				}
				else if (tab == benchmarkSplitTab)
				{
					return currentPrincipal.IsInRole(maintainIndicesRole);
				}
				else if (tab == tolerancesTab)
				{
					return currentPrincipal.IsInRole(maintainTolerancesRole);
				}
				else
				{
					return true;
				}
			}
			finally
			{
				T.X();
			}
		}

		private void adjustTabDisplay()
		{
			T.E();
			try
			{
				//when we add or remove tabs it causes events to fire we don't want,
				//so indicate we are clearing tabs with a member variable so that the
				//event code can ignore it
				m_tabClearing = true;

				this.tabs.SuspendLayout();

				if (CurrentAssetFund.IsNew)
				{
					//ok - any fund with zero funds should have the factors & tolerances tab removed
					//but this requires a performance hit to get funds when we might not need to
					//(other than to disallow tab display)

					if (tabs.TabPages.Contains(factorsTab))
					{
						tabs.Visible = false;
						tabs.TabPages.Remove(factorsTab);
					}
					if (tabs.TabPages.Contains(tolerancesTab))
					{
						tabs.Visible = false;
						tabs.TabPages.Remove(tolerancesTab);
					}

					//					//make user save before adding market indices or composition -
					//					//we need to know the type before we can do this!
					//					if (tabs.TabPages.Contains(benchmarkSplitTab))
					//					{
					//						tabs.Visible = false;
					//						tabs.TabPages.Remove(benchmarkSplitTab);
					//					}

				}
				else
				{
					if (userHasPermissionToViewTab(factorsTab))
					{
						if (!tabs.TabPages.Contains(factorsTab)) // && )
						{
							//factors & tolerances must have disappeared (or just on or the other if permissions involved) 
							//due to new click,
							//get all tabs removed and added back in order
							tabs.Visible = false;

							if (tabs.TabPages.Contains(benchmarkSplitTab))
							{
								tabs.TabPages.Remove(benchmarkSplitTab);
							}

							tabs.TabPages.Add(factorsTab);
						}
					}
					else
					{
						if (tabs.TabPages.Contains(factorsTab))
						{
							tabs.Visible = false;
							tabs.TabPages.Remove(factorsTab);
						}
					}

					if (userHasPermissionToViewTab(tolerancesTab))
					{
						//factors & tolerances must have disappeared (or just on or the other if permissions involved) 
						//due to new click,
						//get all tabs removed and added back in order

						if (!tabs.TabPages.Contains(tolerancesTab))
						{
							tabs.Visible = false;
							if (tabs.TabPages.Contains(benchmarkSplitTab))
							{
								tabs.TabPages.Remove(benchmarkSplitTab);
							}

							tabs.TabPages.Add(tolerancesTab);
						}
					}
					else
					{
						if (tabs.TabPages.Contains(tolerancesTab))
						{
							tabs.Visible = false;
							tabs.TabPages.Remove(tolerancesTab);
						}
					}

				}


				//				if (CurrentAssetFund.AssetFundType == AssetFund.AssetFundTypeEnum.Linked || CurrentAssetFund.AssetFundType == AssetFund.AssetFundTypeEnum.Oeic)
				//				{
				if (userHasPermissionToViewTab(benchmarkSplitTab))
				{
					if (!(tabs.TabPages.Contains(benchmarkSplitTab)))
					{
						tabs.Visible = false;
						tabs.TabPages.Add(benchmarkSplitTab);
					}
				}
				else
				{
					if (tabs.TabPages.Contains(benchmarkSplitTab))
					{
						tabs.Visible = false;
						tabs.TabPages.Remove(benchmarkSplitTab);
					}
				}
				//				}
				//				else if (CurrentAssetFund.AssetFundType == AssetFund.AssetFundTypeEnum.Composite)
				//				{
				//					if (tabs.TabPages.Contains(benchmarkSplitTab))
				//					{
				//						tabs.Visible = false;
				//						tabs.TabPages.Remove(benchmarkSplitTab);
				//					}
				//				}
				//				else if (!(CurrentAssetFund is NewAssetFund))
				//				{
				//					//should never happen
				//					throw new ArgumentException("invalid asset fund type");
				//				}


				if (!tabs.Visible)
				{
					//because we've made an adjustment must be object change or swpa from
					//new to a saved obj
					tabs.SelectedTab = propertiesTab;
					tabs.Visible = true;
				}
				this.tabs.ResumeLayout(false);
				m_tabClearing = false;
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Display the properties of the current fund.
		/// Gets called after an index change or after new button pressed
		/// </summary>
		private void displayAssetFund(bool indexChanged)
		{
			T.E();
			try
			{
				m_systemGeneratedEvent = true;
				codeTextBox.Text = CurrentAssetFund.AssetFundCode;
				fullNameTextBox.Text = CurrentAssetFund.FullName;
				shortNameTextBox.Text = CurrentAssetFund.ShortName;
				populateAndSetPriceFiles();

				if (indexChanged)
				{
					m_refreshAssocs = true;
					m_refreshBenchMarks = true;
					clearErrors();
					adjustTabDisplay();

					codeTextBox.ReadOnly = !CurrentAssetFund.IsNew;

				}

				if (CurrentAssetFund.AssetFundType == AssetFund.AssetFundTypeEnum.Linked)
				{
					fundsTypeCombo.SelectedIndex = (int) fundComboItems.linked;
				}
				else if (CurrentAssetFund.AssetFundType == AssetFund.AssetFundTypeEnum.Oeic)
				{
					fundsTypeCombo.SelectedIndex = (int) fundComboItems.oeic;
				}
				else if (CurrentAssetFund.AssetFundType == AssetFund.AssetFundTypeEnum.Composite)
				{
					fundsTypeCombo.SelectedIndex = (int) fundComboItems.composite;
				}

				FundCollection funds = retrieveFunds();
				fundLockedLabel.Visible = AssetFund.IsLocked(funds);


				//can only change the fund type whilst creating (todo - on a new, this changes object type)
				fundsTypeCombo.Enabled = CurrentAssetFund.IsNew;

				if (tabs.SelectedTab == associationsTab)
				{
					displayAssociationsTab();
				}
				else
				{
					if (m_fundGroupsListToList != null)
					{
						m_fundGroupsListToList.ClearLists();
					}
					if (tabs.SelectedTab == factorsTab)
					{
						displayFactorsTab(funds);
					}
					else if (tabs.SelectedTab == tolerancesTab)
					{
						displayTolerancesTab(funds);
					}
					else if (tabs.SelectedTab == benchmarkSplitTab)
					{
						displayBenchmarkTab();
					}
					else if (tabs.SelectedTab != propertiesTab)
					{
						throw new ApplicationException("Unknown tab" + tabs.SelectedTab.GetType());
					}
				}

			}
			finally
			{
				T.X();
				m_tabClearing = false;
				m_systemGeneratedEvent = false;
			}
		}

		private void FillSelectedBenchMarkList()
		{
			this.SuspendLayout();
			this.lstSelectedBenchmarks.Items.Clear();

			foreach (object includedObj in treeHelper.Included)
			{
				ListViewItem selectedItem = selectedColumns.makeListViewItem(includedObj);
				this.lstSelectedBenchmarks.Items.Add(selectedItem);
			}

			this.ResumeLayout();
			DisplayTotalProportion();
		}

		private void DisplayTotalProportion()
		{
			decimal totalProportion = 0;
			foreach (object includedObj in treeHelper.Included)
			{
				totalProportion += ((AssetMovementConstituentDecorator) includedObj).Proportion;
			}

			lblProportionTotal.Text = "Total Proportion: " + totalProportion.ToString("p4");
			if (totalProportion != 1)
			{
				lblProportionTotal.ForeColor = Color.Red;
			}
			else
			{
				lblProportionTotal.ForeColor = Color.Blue;
			}
		}

		private void displayAssociationsTab()
		{
			T.E();
			try
			{
				if (m_refreshAssocs || m_fundGroupsListToList == null)
				{
					m_refreshAssocs = false; //now it needs to be set next time index is changed

					if (m_fundGroupsListToList == null)
					{
						m_fundGroupsListToList = new ListToListControl();

						m_fundGroupsListToList.SetColumnsAndLists(fundGroupController.LoadFundGroupsByCompanyAndType(GlobalRegistry.CompanyCode, FundGroupFactory.FundGroupTypes.Asset), CurrentAssetFund.FundGroups);

						m_fundGroupsListToList.AddingSelectedItems += new ListToListChangingHandler(listToList_Changing);
						m_fundGroupsListToList.RemovingSelectedItems += new ListToListChangingHandler(listToList_Changing);

						pnlFundGroupAssocsListToList.Controls.Add(m_fundGroupsListToList);
					}
					else
					{
						m_fundGroupsListToList.ResetLists(
							fundGroupController.LoadFundGroupsByCompanyAndType(GlobalRegistry.CompanyCode, FundGroupFactory.FundGroupTypes.Asset),
							CurrentAssetFund.FundGroups);
					}

				}
			}
			finally
			{
				T.X();
			}

		}

		private void populateAndSetPriceFiles()
		{
			ComboBoxPriceFile.DataSource=null;
			ComboBoxPriceFile.Items.Clear();
			IList priceFiles=StaticDataPriceFileLookupDecorator.ToDecoratedList( LookupController.LoadPriceFiles(GlobalRegistry.ConnectionString,GlobalRegistry.CompanyCode));
			StaticDataPriceFileLookupDecorator nullItem = new StaticDataPriceFileLookupDecorator( "Please Select",0);
			priceFiles.Insert(0, nullItem);
			ComboBoxPriceFile.DataSource= priceFiles;
			ComboBoxPriceFile.DisplayMember="DisplayValue";
			ComboBoxPriceFile.ValueMember="Key";
			ComboBoxPriceFile.SelectedValue=CurrentAssetFund.PriceFileId;
		}

		private void displayFactorsTab(FundCollection funds)
		{
			T.E();
			try
			{
				refreshDataGrid(funds);

				m_systemGeneratedFactorEvent = true;
				xFactorText.Text = String.Empty;
				xFactorNarrTextBox.Text = String.Empty;
				tpeText.Text = String.Empty;
				revalText.Text = String.Empty;
				//revalEndDateDTPicker.Value = DateTime.Now.Date; //default to todays date (as agreed with Richard)


				if (CurrentAssetFund != null)
				{
					bool isOeic = CurrentAssetFund.AssetFundType == AssetFund.AssetFundTypeEnum.Oeic;

					tpeLabel.Visible = !isOeic;
					tpeText.Visible = !isOeic;
					revalLabel.Visible = !isOeic;
					revalText.Visible = !isOeic;
					revalEndDateLabel.Visible = !isOeic;
					revalEndDateDTPicker.Visible = !isOeic;
				}

				revalEndDateDTPicker.Value = GlobalRegistry.NextCompanyValuationDateAndTime;
			//	revalEndDateDTPicker.MinDate =GlobalRegistry.NextCompanyValuationDateAndTime;
				//we have to do this little fudge else it always sbows the checkbox as ticked
				revalEndDateDTPicker.ShowCheckBox = false;
				revalEndDateDTPicker.Checked = false;
				revalEndDateDTPicker.ShowCheckBox = true;
			}
			finally
			{
				m_systemGeneratedFactorEvent = false;
				T.X();
			}
		}


		//		private void displayCompositionTab()
		//		{
		//			T.E();
		//			try
		//			{
		//				if (CurrentAssetFund != null)
		//				{
		//					//	refreshDataGrid(CurrentAssetFund.WeightedMovements);
		//				}
		//			}
		//			finally
		//			{
		//				T.X();
		//			}
		//		}
		private IList allConstitutes;

		private void createTreeHelper()
		{
			if (treeHelper == null)
			{
				AssetMovementConstituentCollection constituents = AssetFundController.EndLoadAllAvailableBenchmarks(loadAllAvailableBenchmarksAsyncResult);
				allConstitutes = AssetMovementConstituentDecorator.FromConstituentListToDecorated(constituents);
			}
			treeHelper = new ExclusiveListHelper(allConstitutes);
			treeHelper.Include(AssetMovementConstituentDecorator.FromConstituentListToDecorated(CurrentAssetFund.AssetMovementConstituents));
		}

		//		private void createIndicesListToList()
		//		{
		//			m_indicesListToList = new ListToListControl();
		//			m_indicesListToList.View = ListViewMode.ListToGrid;
		//			m_indicesListToList.Dock = DockStyle.Fill;
		//			m_indicesListToList.Parent = pnlMarketIndicesListToList;
		//			m_indicesListToList.EditingSelectedItem += new ListToListEditedHandler(indicesListToList_EditingSelectedItem);
		//
		//			m_indicesListToList.AddingSelectedItems += new ListToListChangingHandler(indicesListToList_Changing);
		//			m_indicesListToList.RemovingSelectedItems += new ListToListChangingHandler(indicesListToList_Changing);
		//
		//			m_indicesListToList.UnselectedColumns.Add("IndexName");
		//			m_indicesListToList.SelectedColumns.Add("ProportionPercentageDisplay", "Proportion (%)", 95);
		//			m_indicesListToList.SelectedColumns.Add("IndexName", "Index Name", 160);
		//			m_indicesListToList.SelectedColumns.Add("CurrencyCode", "Currency", 80);
		//
		//			AssetMovementConstituentCollection constitutes=AssetFundController.LoadAllAvailableBenchmarks(GlobalRegistry.ConnectionString);
		//			m_indicesListToList.SetColumnsAndLists(constitutes,CurrentAssetFund.AssetMovementConstitution);
		//
		//			pnlMarketIndicesListToList.Controls.Add(m_indicesListToList);
		//		}

		private void displayBenchmarkTab()
		{
			this.SuspendLayout();
			T.E();
			try
			{
				if (CurrentAssetFund != null && m_refreshBenchMarks)
				{
					m_refreshBenchMarks = false;
					this.createTreeHelper();
					refreshBenchMarkLists();
				}

				ListToListControl.SetColsAndListsForListView(this.lstSelectedBenchmarks, selectedColumns, this.ClientSize);
			}
			finally
			{
				T.X();
			}
			this.ResumeLayout();
		}

		/*
				private void displayindicesTab()
				{
					T.E();
					try
					{
						if (CurrentAssetFund != null && m_refreshBenchMarks)
						{
							m_refreshBenchMarks = false;


							if (m_indicesListToList == null)
							{
								createIndicesListToList();
							}
							else
							{
								m_indicesListToList.ResetLists(AssetFundController.LoadAllAvailableBenchmarks(GlobalRegistry.ConnectionString),
															   CurrentAssetFund.AssetMovementConstitution);
							}
						}
					}
					finally
					{
						T.X();
					}

				}
		*/

		private void displayTolerancesTab(FundCollection funds)
		{
			T.E();
			try
			{
				refreshDataGrid(funds);

				lowerTolText.Text = String.Empty;
				upperTolText.Text = String.Empty;
				assetMovementTolText.Text = String.Empty;

				int numPriceIncreaseOnlyFunds = 0;
				for (int i = 0; i < funds.Count; i++)
				{
					if (funds[i].PriceIncreaseOnly)
					{
						numPriceIncreaseOnlyFunds++;
					}
				}
				if (numPriceIncreaseOnlyFunds == 0)
				{
					priceIncCheck.CheckState = CheckState.Unchecked;
				}
				else if (numPriceIncreaseOnlyFunds == funds.Count)
				{
					priceIncCheck.CheckState = CheckState.Checked;
				}
				else
				{
					priceIncCheck.CheckState = CheckState.Indeterminate;
				}
			}
			finally
			{
				T.X();
			}
		}

		private FundCollection retrieveFunds()
		{
			T.E();
			try
			{
				/*
				 * TODO list to list can't currently cope with simple lookup on one side and full objects on the other.
				 * What we could do is have two list to lists then translate back to full objects.
				 * For now - is non-optimised - loads up the full collection
				 */

				//we load each time in order to always have as up to date as possible, 
				//unless the user has started to edit any fund data, in which we can't and must hold onto the funds
				//until they hit save
				if (m_cachedFunds == null || !m_fundsEdited)
				{
					//refresh the list
					m_cachedFunds = FundController.LoadFundsByAssetFund(GlobalRegistry.ConnectionString, CurrentAssetFund.AssetFundCode);

					if (m_cachedFunds != null)
					{
						//done this way to avoid circular references between asset funds and funds
						for (int i = 0; i < m_cachedFunds.Count; i++)
						{
							Fund fund = (Fund) m_cachedFunds[i];
							fund.ParentAssetFund = CurrentAssetFund;
						}
					}
				}

				return (FundCollection) m_cachedFunds;
			}
			finally
			{
				T.X();
			}
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

		private bool doSaveNew()
		{
			T.E();
			bool isValid = true;
			try
			{
				isValid = validateNewAssetFund();
				if (isValid)
				{

					if (m_fundsEdited && m_cachedFunds != null)
					{
						AssetFundController.UpdateAssetFundAndChildFunds(GlobalRegistry.ConnectionString, CurrentAssetFund, (FundCollection) m_cachedFunds);
					}
					else
					{
						AssetFundController.UpdateAssetFund(GlobalRegistry.ConnectionString, CurrentAssetFund);
					}

					Changed = false;
					ListManager.ChangeSelected(new SimpleStringLookup(CurrentAssetFund.AssetFundCode, CurrentAssetFund.ShortName));
				}
			}
			finally
			{
				T.X();
			}
			return isValid;
		}

		private bool validateNewAssetFund()
		{
			T.E();
			ArrayList assetErrors = new ArrayList(AssetFundController.ValidateNewAssetFund(GlobalRegistry.ConnectionString, CurrentAssetFund));
			ArrayList fundErrors = new ArrayList();

			try
			{
				if (fundsTypeCombo.SelectedIndex<1)
				{
					assetErrors.Add(AssetFundController.AssetFundValidationError.AssetFundTypeNotSelected);
				}

				if (m_cachedFunds != null)
				{
					fundErrors.AddRange(validateChildFunds((FundCollection) m_cachedFunds));
				}

				if (haveErrors(new Array[]
					{
						(FundController.FundValidationError[]) fundErrors.ToArray(typeof (FundController.FundValidationError)),
						(AssetFundController.AssetFundValidationError[]) assetErrors.ToArray(typeof (AssetFundController.AssetFundValidationError))
					}))
				{
					showValidationErrors(assetErrors, fundErrors);
				}
			}
			finally
			{
				T.X();
			}
			return !(haveErrors(new Array[]
				{
					(FundController.FundValidationError[]) fundErrors.ToArray(typeof (FundController.FundValidationError)),
					(AssetFundController.AssetFundValidationError[]) assetErrors.ToArray(typeof (AssetFundController.AssetFundValidationError))
				}));

		}

		private void showValidationErrors(ArrayList assetFundErrors, ArrayList fundErrors)
		{
			clearErrors();

			bool focusSet = false;
			foreach (AssetFundController.AssetFundValidationError assetFundValidationError in assetFundErrors)
			{
				Control focusControl = resolveControlFromAssetError(assetFundValidationError);
				if (!focusSet)
				{
					SetFocusToErrorControl(focusControl);
					focusSet = true;
				}
				setError(focusControl, MessageBoxHelper.DialogText(assetFundValidationError.ToString()));
			}

			foreach (FundController.FundValidationError fundValidationError in fundErrors)
			{
				Control focusControl = resolveControlFromFundError(fundValidationError);
				if (!focusSet) SetFocusToErrorControl(focusControl);
				focusSet = true;
				setError(focusControl, MessageBoxHelper.DialogText(fundValidationError.ToString()));
			}

			if (haveErrors(new Array[]
				{
					(FundController.FundValidationError[]) fundErrors.ToArray(typeof (FundController.FundValidationError)),
					(AssetFundController.AssetFundValidationError[]) assetFundErrors.ToArray(typeof (AssetFundController.AssetFundValidationError))
				}))
			{
				showErrorDialog(MessageBoxHelper.DialogText("AssetFundCannotSave"));
			}
		}

		private Control resolveControlFromFundError(FundController.FundValidationError validationError)
		{
			Control returnControl = null;
			switch (validationError)
			{
				case FundController.FundValidationError.FundXFactorInvalidNumber:
					goto case FundController.FundValidationError.FundXFactorInvalid;
				case FundController.FundValidationError.FundXFactorInvalid:
					returnControl = xFactorText;
					break;
				case FundController.FundValidationError.FundTPEInvalid:
					goto case FundController.FundValidationError.FundTPEInvalidNumber;
				case FundController.FundValidationError.FundTPEInvalidNumber:
					returnControl = tpeText;
					break;
				case FundController.FundValidationError.FundRevaluationFactorInvalid:
					goto case FundController.FundValidationError.FundRevaluationFactorInvalidNumber;
				case FundController.FundValidationError.FundRevaluationFactorInvalidNumber:
					returnControl = revalText;
					break;
				case FundController.FundValidationError.FundScalingFactorInvalid:
					goto case FundController.FundValidationError.FundScalingFactorInvalidNumber;
				case FundController.FundValidationError.FundScalingFactorInvalidNumber:
					returnControl = fundGrid;
					break;

				case FundController.FundValidationError.FundMaxLowerToleranceExceeded:
					goto case FundController.FundValidationError.FundInvalidNumDecimalPlacesLowerTolerance;
				case FundController.FundValidationError.FundLowerToleranceNegative:
					goto case FundController.FundValidationError.FundInvalidNumDecimalPlacesLowerTolerance;
				case FundController.FundValidationError.FundLowerToleranceZero:
					goto case FundController.FundValidationError.FundInvalidNumDecimalPlacesLowerTolerance;
				case FundController.FundValidationError.FundInvalidNumDecimalPlacesLowerTolerance:
					returnControl = lowerTolText;
					break;
				case FundController.FundValidationError.FundMaxUpperToleranceExceeded:
					goto case FundController.FundValidationError.FundInvalidNumDecimalPlacesUpperTolerance;
				case FundController.FundValidationError.FundUpperToleranceNegative:
					goto case FundController.FundValidationError.FundInvalidNumDecimalPlacesUpperTolerance;
				case FundController.FundValidationError.FundUpperToleranceZero:
					goto case FundController.FundValidationError.FundInvalidNumDecimalPlacesUpperTolerance;
				case FundController.FundValidationError.FundInvalidNumDecimalPlacesUpperTolerance:
					returnControl = upperTolText;
					break;
				case FundController.FundValidationError.FundUpperToleranceLessThanLowerTolerance:
					returnControl = lowerTolText;
					break;
                default:
                    /* UA232 and some others.  The default option was added by MISDC Finance MAW 04/10/05 
                     * to catch errors thrown for controls not on the screen 
                    */
                    returnControl = fundGrid;
                    break;
			}
			return returnControl;

			//				if (revalFactorErrorMsg != null && revalFactorErrorMsg.Length > 0)
			//				{
			//					if (isValid)
			//					{
			//						if (revalText.Text.Length > 0)
			//						{
			//							revalText.Focus();
			//							setError(revalText, revalFactorErrorMsg);
			//						}
			//						else //must be date thats wrong
			//						{
			//							revalEndDateDTPicker.Focus();
			//							setError(revalEndDateDTPicker, revalFactorErrorMsg);
			//						}
			//					}
			//					factorError = true;
			//					isValid = false;
			//				}
			//
			//


		}

		private Control resolveControlFromAssetError(AssetFundController.AssetFundValidationError validationError)
		{
			Control returnControl = null;
			switch (validationError)
			{
				case AssetFundController.AssetFundValidationError.AssetFundPriceFileNotSelected:
					returnControl = ComboBoxPriceFile;
					break;
				case AssetFundController.AssetFundValidationError.AssetFundFieldEmptyCode:
					goto case AssetFundController.AssetFundValidationError.AssetFundDuplicateFieldCode;
				case AssetFundController.AssetFundValidationError.AssetFundDuplicateFieldCode:
					returnControl = codeTextBox;
					break;
				case AssetFundController.AssetFundValidationError.AssetFundDuplicateFieldFullName:
					returnControl = fullNameTextBox;
					break;
				case AssetFundController.AssetFundValidationError.AssetFundFieldEmptyFullName:
					goto case AssetFundController.AssetFundValidationError.AssetFundDuplicateFieldFullName;
				case AssetFundController.AssetFundValidationError.AssetFundDuplicateFieldShortName:
					returnControl = shortNameTextBox;
					break;
				case AssetFundController.AssetFundValidationError.AssetFundFieldEmptyShortName:
					goto case AssetFundController.AssetFundValidationError.AssetFundDuplicateFieldShortName;
				case AssetFundController.AssetFundValidationError.AssetFundTypeNotSelected:
					returnControl = fundsTypeCombo;
					break;
				case AssetFundController.AssetFundValidationError.AssetFundSplitLessThan100Percent:
					returnControl = lstSelectedBenchmarks;
					break;
				case AssetFundController.AssetFundValidationError.AssetFundSplitMoreThan100Percent:
					goto case AssetFundController.AssetFundValidationError.AssetFundSplitLessThan100Percent;
				case AssetFundController.AssetFundValidationError.AssetFundInvalidNumDecimalPlacesTolerance:
					returnControl = assetMovementTolText;
					break;
				case AssetFundController.AssetFundValidationError.AssetFundMaxToleranceExceeded:
					goto case AssetFundController.AssetFundValidationError.AssetFundInvalidNumDecimalPlacesTolerance;
				case AssetFundController.AssetFundValidationError.AssetFundToleranceNegative:
					goto case AssetFundController.AssetFundValidationError.AssetFundInvalidNumDecimalPlacesTolerance;
			}
			return returnControl;
		}

		private static TabPage isParentATabPage(Control parent)
		{
			TabPage tab = null;

			if (parent != null)
			{
				tab = parent as TabPage;
			}

			if (tab == null)
			{
				tab = isParentATabPage(parent.Parent);
			}
			return tab;
		}

		/// <summary>
		/// Sets the focus to the specified control in error.
		/// </summary>
		/// <param name="focusControl">Focus control.</param>
		public static void SetFocusToErrorControl(Control focusControl)
		{
			TabPage tabPage = isParentATabPage(focusControl.Parent);

            if (tabPage != null)
            {
                if (tabPage.Visible)
                {
                    ((TabControl) tabPage.Parent).SelectedTab = tabPage;
                    if (tabPage.CanFocus)
                    {
                        tabPage.Focus();
                    }
                }
            }

			if (focusControl.CanFocus)
			{
				focusControl.Focus();
			}
		}

		private static bool haveErrors(Array[] errors)
		{
			bool returnValue = false;
			foreach (object error in errors)
			{
				if (((Array) error).Length > 0)
				{
					returnValue = true;
					break;
				}
			}

			return returnValue;
		}

		/// <summary>
		/// Only validates the fields we can set on the fund via this asset fund UI
		/// </summary>
		/// <param name="funds"></param>
		/// <returns></returns>
		private FundController.FundValidationError[] validateChildFunds(FundCollection funds)
		{
			T.E();

			ArrayList errors = new ArrayList();

			if (m_fundsEdited)
			{
				try
				{
					if (funds != null & funds.Count > 0)
					{
						FundController fundController = new FundController(GlobalRegistry.ConnectionString);
						foreach (Fund fund in funds)
						{
							if (m_changedUpperTolerance || m_FundGridChanged) errors.AddRange(fundController.ValidateUpperTolerance(fund.UpperTolerance));
							if (m_changedLowerTolerance || m_FundGridChanged) errors.AddRange(fundController.ValidateLowerTolerance(fund.LowerTolerance));
							if (m_changedLowerTolerance || m_changedUpperTolerance || m_FundGridChanged) errors.AddRange(fundController.ValidateTolerances(fund));

							FundController.FundValidationError error=FundController.FundValidationError.NoError;

							if (m_changedXFactor || m_FundGridChanged) error=FundController.ValidateXFactor(fund);
							if (error!=FundController.FundValidationError.NoError)  errors.Add(error);
							if (m_changedTPE || m_FundGridChanged) error=FundController.ValidateTPE(fund);;
							if (error!=FundController.FundValidationError.NoError)  errors.Add(error);
							if (m_changedRevaluationFactor || m_FundGridChanged) error=fundController.ValidateRevaluationFactor(fund);
							if (error!=FundController.FundValidationError.NoError)  errors.Add(error);
							
							if (errors.Count > 0) break;
						}

					}
				}
				finally
				{
					T.X();
				}
			}
			return (FundController.FundValidationError[]) errors.ToArray(typeof (FundController.FundValidationError));
		}

		private object[] validateUpdatedAssetFund()
		{
			T.E();
			ArrayList assetErrors = new ArrayList(AssetFundController.ValidateUpdatedAssetFund(GlobalRegistry.ConnectionString, CurrentAssetFund));
			ArrayList fundErrors = new ArrayList();
			try
			{
				if (m_cachedFunds != null)
				{
					fundErrors.AddRange(validateChildFunds((FundCollection) m_cachedFunds));
				}

				if (haveErrors(new Array[]
					{
						(FundController.FundValidationError[]) fundErrors.ToArray(typeof (FundController.FundValidationError)),
						(AssetFundController.AssetFundValidationError[]) assetErrors.ToArray(typeof (AssetFundController.AssetFundValidationError))
					}))
				{
					showValidationErrors(assetErrors, fundErrors);
				}
			}
			finally
			{
				T.X();
			}

			ArrayList allErrors = new ArrayList();
			allErrors.AddRange(assetErrors);
			allErrors.AddRange(fundErrors);


			return (object[]) allErrors.ToArray(typeof (object));

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
			this.fundsTypeCombo = new System.Windows.Forms.ComboBox();
			this.fundsTypeLabel = new System.Windows.Forms.Label();
			this.shortNameTextBox = new System.Windows.Forms.TextBox();
			this.shortNameLabel = new System.Windows.Forms.Label();
			this.fullNameTextBox = new System.Windows.Forms.TextBox();
			this.fullNameLabel = new System.Windows.Forms.Label();
			this.codeTextBox = new System.Windows.Forms.TextBox();
			this.codeLabel = new System.Windows.Forms.Label();
			this.fundLockedLabel = new System.Windows.Forms.Label();
			this.tolerancesTab = new System.Windows.Forms.TabPage();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.priceIncCheck = new System.Windows.Forms.CheckBox();
			this.priceIncLabel = new System.Windows.Forms.Label();
			this.lowerToleranceLabel = new System.Windows.Forms.Label();
			this.assetMovementTolText = new System.Windows.Forms.TextBox();
			this.upperToleranceLabel = new System.Windows.Forms.Label();
			this.upperTolText = new System.Windows.Forms.TextBox();
			this.assetFundMovementToleranceLabel = new System.Windows.Forms.Label();
			this.lowerTolText = new System.Windows.Forms.TextBox();
			this.tolerancesGrid = new HBOS.FS.AMP.Windows.Controls.DataGrid();
			this.factorsTab = new System.Windows.Forms.TabPage();
			this.applyAllGroupBox = new System.Windows.Forms.GroupBox();
			this.revalEndDateDTPicker = new System.Windows.Forms.DateTimePicker();
			this.revalEndDateLabel = new System.Windows.Forms.Label();
			this.revalText = new System.Windows.Forms.TextBox();
			this.revalLabel = new System.Windows.Forms.Label();
			this.tpeText = new System.Windows.Forms.TextBox();
			this.tpeLabel = new System.Windows.Forms.Label();
			this.xFactorNarrTextBox = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.xFactorText = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.fundGrid = new HBOS.FS.AMP.Windows.Controls.DataGrid();
			this.associationsTab = new System.Windows.Forms.TabPage();
			this.LabelPriceFile = new System.Windows.Forms.Label();
			this.ComboBoxPriceFile = new System.Windows.Forms.ComboBox();
			this.fundGroupGroupBox = new System.Windows.Forms.GroupBox();
			this.pnlFundGroupAssocsListToList = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.benchmarkSplitTab = new System.Windows.Forms.TabPage();
			this.benchmarkGroupBox = new System.Windows.Forms.GroupBox();
			this.lblProportionTotal = new System.Windows.Forms.Label();
			this.pnlMiddle = new System.Windows.Forms.Panel();
			this.btnAddBenchmark = new System.Windows.Forms.Button();
			this.btnRemoveBenchmark = new System.Windows.Forms.Button();
			this.lstSelectedBenchmarks = new System.Windows.Forms.ListView();
			this.treAllBenchMarks = new System.Windows.Forms.TreeView();
			this.indicesTabDescLabel = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.dataGrid1 = new HBOS.FS.AMP.Windows.Controls.DataGrid();
			this.tabs.SuspendLayout();
			this.propertiesTab.SuspendLayout();
			this.tolerancesTab.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tolerancesGrid)).BeginInit();
			this.factorsTab.SuspendLayout();
			this.applyAllGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.fundGrid)).BeginInit();
			this.associationsTab.SuspendLayout();
			this.fundGroupGroupBox.SuspendLayout();
			this.benchmarkSplitTab.SuspendLayout();
			this.benchmarkGroupBox.SuspendLayout();
			this.pnlMiddle.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.SuspendLayout();
			// 
			// tabs
			// 
			this.tabs.Controls.Add(this.propertiesTab);
			this.tabs.Controls.Add(this.associationsTab);
			this.tabs.Controls.Add(this.factorsTab);
			this.tabs.Controls.Add(this.tolerancesTab);
			this.tabs.Controls.Add(this.benchmarkSplitTab);
			this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabs.ItemSize = new System.Drawing.Size(78, 21);
			this.tabs.Location = new System.Drawing.Point(0, 0);
			this.tabs.Name = "tabs";
			this.tabs.Padding = new System.Drawing.Point(8, 3);
			this.tabs.SelectedIndex = 0;
			this.tabs.Size = new System.Drawing.Size(760, 520);
			this.tabs.TabIndex = 3;
			this.tabs.SelectedIndexChanged += new System.EventHandler(this.tabSelected);
			// 
			// propertiesTab
			// 
			this.propertiesTab.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.propertiesTab.Controls.Add(this.fundsTypeCombo);
			this.propertiesTab.Controls.Add(this.fundsTypeLabel);
			this.propertiesTab.Controls.Add(this.shortNameTextBox);
			this.propertiesTab.Controls.Add(this.shortNameLabel);
			this.propertiesTab.Controls.Add(this.fullNameTextBox);
			this.propertiesTab.Controls.Add(this.fullNameLabel);
			this.propertiesTab.Controls.Add(this.codeTextBox);
			this.propertiesTab.Controls.Add(this.codeLabel);
			this.propertiesTab.Controls.Add(this.fundLockedLabel);
			this.propertiesTab.Location = new System.Drawing.Point(4, 25);
			this.propertiesTab.Name = "propertiesTab";
			this.propertiesTab.Size = new System.Drawing.Size(752, 491);
			this.propertiesTab.TabIndex = 0;
			this.propertiesTab.Text = "Properties";
			// 
			// fundsTypeCombo
			// 
			this.fundsTypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.fundsTypeCombo.ItemHeight = 16;
			this.fundsTypeCombo.Items.AddRange(new object[] {
																"Please Select",
																"Linked",
																"OEIC",
																"Composite"});
			this.fundsTypeCombo.Location = new System.Drawing.Point(184, 112);
			this.fundsTypeCombo.Name = "fundsTypeCombo";
			this.fundsTypeCombo.Size = new System.Drawing.Size(116, 24);
			this.fundsTypeCombo.TabIndex = 304;
			this.fundsTypeCombo.SelectedIndexChanged += new System.EventHandler(this.fundsTypeCombo_SelectedIndexChanged);
			// 
			// fundsTypeLabel
			// 
			this.fundsTypeLabel.AutoSize = true;
			this.fundsTypeLabel.ForeColor = System.Drawing.Color.Black;
			this.fundsTypeLabel.Location = new System.Drawing.Point(16, 112);
			this.fundsTypeLabel.Name = "fundsTypeLabel";
			this.fundsTypeLabel.Size = new System.Drawing.Size(75, 18);
			this.fundsTypeLabel.TabIndex = 305;
			this.fundsTypeLabel.Text = "Funds Type";
			// 
			// shortNameTextBox
			// 
			this.shortNameTextBox.Location = new System.Drawing.Point(184, 80);
			this.shortNameTextBox.MaxLength = 50;
			this.shortNameTextBox.Name = "shortNameTextBox";
			this.shortNameTextBox.Size = new System.Drawing.Size(232, 22);
			this.shortNameTextBox.TabIndex = 46;
			this.shortNameTextBox.Text = "";
			this.shortNameTextBox.TextChanged += new System.EventHandler(this.defaultControlChanged);
			// 
			// shortNameLabel
			// 
			this.shortNameLabel.Location = new System.Drawing.Point(16, 80);
			this.shortNameLabel.Name = "shortNameLabel";
			this.shortNameLabel.Size = new System.Drawing.Size(136, 23);
			this.shortNameLabel.TabIndex = 49;
			this.shortNameLabel.Text = "Short Name";
			// 
			// fullNameTextBox
			// 
			this.fullNameTextBox.Location = new System.Drawing.Point(184, 48);
			this.fullNameTextBox.MaxLength = 100;
			this.fullNameTextBox.Name = "fullNameTextBox";
			this.fullNameTextBox.Size = new System.Drawing.Size(232, 22);
			this.fullNameTextBox.TabIndex = 45;
			this.fullNameTextBox.Text = "";
			this.fullNameTextBox.TextChanged += new System.EventHandler(this.defaultControlChanged);
			// 
			// fullNameLabel
			// 
			this.fullNameLabel.Location = new System.Drawing.Point(16, 48);
			this.fullNameLabel.Name = "fullNameLabel";
			this.fullNameLabel.Size = new System.Drawing.Size(136, 23);
			this.fullNameLabel.TabIndex = 48;
			this.fullNameLabel.Text = "Full Name";
			// 
			// codeTextBox
			// 
			this.codeTextBox.Location = new System.Drawing.Point(184, 16);
			this.codeTextBox.MaxLength = 8;
			this.codeTextBox.Name = "codeTextBox";
			this.codeTextBox.Size = new System.Drawing.Size(116, 22);
			this.codeTextBox.TabIndex = 44;
			this.codeTextBox.Text = "";
			this.codeTextBox.TextChanged += new System.EventHandler(this.defaultControlChanged);
			// 
			// codeLabel
			// 
			this.codeLabel.Location = new System.Drawing.Point(16, 16);
			this.codeLabel.Name = "codeLabel";
			this.codeLabel.Size = new System.Drawing.Size(136, 23);
			this.codeLabel.TabIndex = 47;
			this.codeLabel.Text = "Asset Fund Code";
			// 
			// fundLockedLabel
			// 
			this.fundLockedLabel.ForeColor = System.Drawing.Color.Red;
			this.fundLockedLabel.Location = new System.Drawing.Point(16, 152);
			this.fundLockedLabel.Name = "fundLockedLabel";
			this.fundLockedLabel.Size = new System.Drawing.Size(400, 72);
			this.fundLockedLabel.TabIndex = 315;
			this.fundLockedLabel.Text = "The price for one or more Funds associated with this Asset Fund have already been" +
				" authorised today.  Any changes made will not affect the price calculations of a" +
				"uthorised Funds until the following day, unless the price for that Fund is unaut" +
				"horised.";
			this.fundLockedLabel.Visible = false;
			// 
			// tolerancesTab
			// 
			this.tolerancesTab.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.tolerancesTab.Controls.Add(this.groupBox1);
			this.tolerancesTab.Controls.Add(this.tolerancesGrid);
			this.tolerancesTab.Location = new System.Drawing.Point(4, 25);
			this.tolerancesTab.Name = "tolerancesTab";
			this.tolerancesTab.Size = new System.Drawing.Size(752, 491);
			this.tolerancesTab.TabIndex = 3;
			this.tolerancesTab.Text = "Tolerances";
			this.tolerancesTab.Visible = false;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.priceIncCheck);
			this.groupBox1.Controls.Add(this.priceIncLabel);
			this.groupBox1.Controls.Add(this.lowerToleranceLabel);
			this.groupBox1.Controls.Add(this.assetMovementTolText);
			this.groupBox1.Controls.Add(this.upperToleranceLabel);
			this.groupBox1.Controls.Add(this.upperTolText);
			this.groupBox1.Controls.Add(this.assetFundMovementToleranceLabel);
			this.groupBox1.Controls.Add(this.lowerTolText);
			this.groupBox1.ForeColor = System.Drawing.Color.Blue;
			this.groupBox1.Location = new System.Drawing.Point(14, 15);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(720, 161);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Apply to all Funds";
			// 
			// priceIncCheck
			// 
			this.priceIncCheck.Location = new System.Drawing.Point(224, 128);
			this.priceIncCheck.Name = "priceIncCheck";
			this.priceIncCheck.Size = new System.Drawing.Size(16, 16);
			this.priceIncCheck.TabIndex = 3;
			this.priceIncCheck.Text = "checkBox3";
			this.priceIncCheck.CheckedChanged += new System.EventHandler(this.priceIncCheck_CheckedChanged);
			// 
			// priceIncLabel
			// 
			this.priceIncLabel.AutoSize = true;
			this.priceIncLabel.ForeColor = System.Drawing.Color.Black;
			this.priceIncLabel.Location = new System.Drawing.Point(16, 128);
			this.priceIncLabel.Name = "priceIncLabel";
			this.priceIncLabel.Size = new System.Drawing.Size(118, 18);
			this.priceIncLabel.TabIndex = 333;
			this.priceIncLabel.Text = "Price increase only";
			// 
			// lowerToleranceLabel
			// 
			this.lowerToleranceLabel.AutoSize = true;
			this.lowerToleranceLabel.ForeColor = System.Drawing.Color.Black;
			this.lowerToleranceLabel.Location = new System.Drawing.Point(16, 64);
			this.lowerToleranceLabel.Name = "lowerToleranceLabel";
			this.lowerToleranceLabel.Size = new System.Drawing.Size(129, 18);
			this.lowerToleranceLabel.TabIndex = 332;
			this.lowerToleranceLabel.Text = "Lower Tolerance (%)";
			// 
			// assetMovementTolText
			// 
			this.assetMovementTolText.Location = new System.Drawing.Point(224, 32);
			this.assetMovementTolText.MaxLength = 9;
			this.assetMovementTolText.Name = "assetMovementTolText";
			this.assetMovementTolText.Size = new System.Drawing.Size(112, 22);
			this.assetMovementTolText.TabIndex = 0;
			this.assetMovementTolText.Text = "";
			this.assetMovementTolText.TextChanged += new System.EventHandler(this.assetMovementTolText_TextChanged);
			this.assetMovementTolText.Leave += new System.EventHandler(this.toleranceText_Leave);
			// 
			// upperToleranceLabel
			// 
			this.upperToleranceLabel.AutoSize = true;
			this.upperToleranceLabel.ForeColor = System.Drawing.Color.Black;
			this.upperToleranceLabel.Location = new System.Drawing.Point(16, 96);
			this.upperToleranceLabel.Name = "upperToleranceLabel";
			this.upperToleranceLabel.Size = new System.Drawing.Size(129, 18);
			this.upperToleranceLabel.TabIndex = 331;
			this.upperToleranceLabel.Text = "Upper Tolerance (%)";
			// 
			// upperTolText
			// 
			this.upperTolText.Location = new System.Drawing.Point(224, 96);
			this.upperTolText.MaxLength = 9;
			this.upperTolText.Name = "upperTolText";
			this.upperTolText.Size = new System.Drawing.Size(112, 22);
			this.upperTolText.TabIndex = 2;
			this.upperTolText.Text = "";
			this.upperTolText.TextChanged += new System.EventHandler(this.upperTolText_TextChanged);
			this.upperTolText.Leave += new System.EventHandler(this.toleranceText_Leave);
			// 
			// assetFundMovementToleranceLabel
			// 
			this.assetFundMovementToleranceLabel.AutoSize = true;
			this.assetFundMovementToleranceLabel.ForeColor = System.Drawing.Color.Black;
			this.assetFundMovementToleranceLabel.Location = new System.Drawing.Point(16, 32);
			this.assetFundMovementToleranceLabel.Name = "assetFundMovementToleranceLabel";
			this.assetFundMovementToleranceLabel.Size = new System.Drawing.Size(195, 18);
			this.assetFundMovementToleranceLabel.TabIndex = 330;
			this.assetFundMovementToleranceLabel.Text = "Asset  Movement Tolerance (%)";
			// 
			// lowerTolText
			// 
			this.lowerTolText.Location = new System.Drawing.Point(224, 64);
			this.lowerTolText.MaxLength = 9;
			this.lowerTolText.Name = "lowerTolText";
			this.lowerTolText.Size = new System.Drawing.Size(112, 22);
			this.lowerTolText.TabIndex = 1;
			this.lowerTolText.Text = "";
			this.lowerTolText.TextChanged += new System.EventHandler(this.lowerTolText_TextChanged);
			this.lowerTolText.Leave += new System.EventHandler(this.toleranceText_Leave);
			// 
			// tolerancesGrid
			// 
			this.tolerancesGrid.AlternatingBackColor = System.Drawing.SystemColors.Window;
			this.tolerancesGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tolerancesGrid.BackColor = System.Drawing.SystemColors.Window;
			this.tolerancesGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.tolerancesGrid.CaptionBackColor = System.Drawing.SystemColors.ActiveCaption;
			this.tolerancesGrid.CaptionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.tolerancesGrid.DataMember = "";
			this.tolerancesGrid.FlatMode = false;
			this.tolerancesGrid.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.tolerancesGrid.ForeColor = System.Drawing.SystemColors.WindowText;
			this.tolerancesGrid.GridLineColor = System.Drawing.SystemColors.Control;
			this.tolerancesGrid.HeaderBackColor = System.Drawing.SystemColors.Control;
			this.tolerancesGrid.HeaderFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.tolerancesGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.tolerancesGrid.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.tolerancesGrid.Location = new System.Drawing.Point(14, 192);
			this.tolerancesGrid.Name = "tolerancesGrid";
			this.tolerancesGrid.ParentRowsBackColor = System.Drawing.SystemColors.Control;
			this.tolerancesGrid.ParentRowsForeColor = System.Drawing.SystemColors.WindowText;
			this.tolerancesGrid.PrintColumnHeadingFont = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
			this.tolerancesGrid.PrintColumnSettings = null;
			this.tolerancesGrid.PrintPageHeadingFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.tolerancesGrid.PrintStandardFont = new System.Drawing.Font("Arial", 8F);
			this.tolerancesGrid.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
			this.tolerancesGrid.SelectionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.tolerancesGrid.Size = new System.Drawing.Size(720, 272);
			this.tolerancesGrid.TabIndex = 0;
			// 
			// factorsTab
			// 
			this.factorsTab.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.factorsTab.Controls.Add(this.applyAllGroupBox);
			this.factorsTab.Controls.Add(this.fundGrid);
			this.factorsTab.Location = new System.Drawing.Point(4, 25);
			this.factorsTab.Name = "factorsTab";
			this.factorsTab.Size = new System.Drawing.Size(752, 491);
			this.factorsTab.TabIndex = 4;
			this.factorsTab.Text = "Factors";
			this.factorsTab.Visible = false;
			// 
			// applyAllGroupBox
			// 
			this.applyAllGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.applyAllGroupBox.Controls.Add(this.revalEndDateDTPicker);
			this.applyAllGroupBox.Controls.Add(this.revalEndDateLabel);
			this.applyAllGroupBox.Controls.Add(this.revalText);
			this.applyAllGroupBox.Controls.Add(this.revalLabel);
			this.applyAllGroupBox.Controls.Add(this.tpeText);
			this.applyAllGroupBox.Controls.Add(this.tpeLabel);
			this.applyAllGroupBox.Controls.Add(this.xFactorNarrTextBox);
			this.applyAllGroupBox.Controls.Add(this.label9);
			this.applyAllGroupBox.Controls.Add(this.xFactorText);
			this.applyAllGroupBox.Controls.Add(this.label2);
			this.applyAllGroupBox.ForeColor = System.Drawing.Color.Blue;
			this.applyAllGroupBox.Location = new System.Drawing.Point(16, 16);
			this.applyAllGroupBox.Name = "applyAllGroupBox";
			this.applyAllGroupBox.Size = new System.Drawing.Size(720, 224);
			this.applyAllGroupBox.TabIndex = 1;
			this.applyAllGroupBox.TabStop = false;
			this.applyAllGroupBox.Text = "Apply to all Funds";
			// 
			// revalEndDateDTPicker
			// 
			this.revalEndDateDTPicker.Checked = false;
			this.revalEndDateDTPicker.CustomFormat = "";
			this.revalEndDateDTPicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.revalEndDateDTPicker.Location = new System.Drawing.Point(184, 176);
			this.revalEndDateDTPicker.Name = "revalEndDateDTPicker";
			this.revalEndDateDTPicker.ShowCheckBox = true;
			this.revalEndDateDTPicker.Size = new System.Drawing.Size(112, 22);
			this.revalEndDateDTPicker.TabIndex = 5;
			this.revalEndDateDTPicker.Value = new System.DateTime(2005, 4, 27, 14, 3, 43, 390);
			this.revalEndDateDTPicker.Click += new System.EventHandler(this.revalEndDateDTPickerChanged);
			this.revalEndDateDTPicker.Enter += new System.EventHandler(this.revalEndDateDTPickerChanged);
			this.revalEndDateDTPicker.ValueChanged += new System.EventHandler(this.revalEndDateDTPickerChanged);
			// 
			// revalEndDateLabel
			// 
			this.revalEndDateLabel.ForeColor = System.Drawing.Color.Black;
			this.revalEndDateLabel.Location = new System.Drawing.Point(16, 176);
			this.revalEndDateLabel.Name = "revalEndDateLabel";
			this.revalEndDateLabel.Size = new System.Drawing.Size(160, 40);
			this.revalEndDateLabel.TabIndex = 324;
			this.revalEndDateLabel.Text = "Last day of application for Revaluation Factor";
			// 
			// revalText
			// 
			this.revalText.Location = new System.Drawing.Point(184, 144);
			this.revalText.MaxLength = 6;
			this.revalText.Name = "revalText";
			this.revalText.Size = new System.Drawing.Size(112, 22);
			this.revalText.TabIndex = 4;
			this.revalText.Text = "";
			this.revalText.TextChanged += new System.EventHandler(this.revalText_TextChanged);
			// 
			// revalLabel
			// 
			this.revalLabel.ForeColor = System.Drawing.Color.Black;
			this.revalLabel.Location = new System.Drawing.Point(16, 144);
			this.revalLabel.Name = "revalLabel";
			this.revalLabel.Size = new System.Drawing.Size(160, 22);
			this.revalLabel.TabIndex = 321;
			this.revalLabel.Text = "Revaluation Factor (%)";
			// 
			// tpeText
			// 
			this.tpeText.Location = new System.Drawing.Point(184, 112);
			this.tpeText.MaxLength = 6;
			this.tpeText.Name = "tpeText";
			this.tpeText.Size = new System.Drawing.Size(112, 22);
			this.tpeText.TabIndex = 3;
			this.tpeText.Text = "";
			this.tpeText.TextChanged += new System.EventHandler(this.tpeText_TextChanged);
			// 
			// tpeLabel
			// 
			this.tpeLabel.ForeColor = System.Drawing.Color.Black;
			this.tpeLabel.Location = new System.Drawing.Point(16, 112);
			this.tpeLabel.Name = "tpeLabel";
			this.tpeLabel.Size = new System.Drawing.Size(160, 32);
			this.tpeLabel.TabIndex = 319;
			this.tpeLabel.Text = "Tax Provision Estimate (%)";
			// 
			// xFactorNarrTextBox
			// 
			this.xFactorNarrTextBox.Location = new System.Drawing.Point(184, 64);
			this.xFactorNarrTextBox.MaxLength = 1000;
			this.xFactorNarrTextBox.Multiline = true;
			this.xFactorNarrTextBox.Name = "xFactorNarrTextBox";
			this.xFactorNarrTextBox.Size = new System.Drawing.Size(376, 40);
			this.xFactorNarrTextBox.TabIndex = 2;
			this.xFactorNarrTextBox.Text = "";
			this.xFactorNarrTextBox.TextChanged += new System.EventHandler(this.xFactorNarrTextBox_TextChanged);
			// 
			// label9
			// 
			this.label9.ForeColor = System.Drawing.Color.Black;
			this.label9.Location = new System.Drawing.Point(16, 64);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(152, 22);
			this.label9.TabIndex = 317;
			this.label9.Text = "X Factor Narrative";
			// 
			// xFactorText
			// 
			this.xFactorText.Location = new System.Drawing.Point(184, 32);
			this.xFactorText.MaxLength = 6;
			this.xFactorText.Name = "xFactorText";
			this.xFactorText.Size = new System.Drawing.Size(112, 22);
			this.xFactorText.TabIndex = 1;
			this.xFactorText.Text = "";
			this.xFactorText.TextChanged += new System.EventHandler(this.xFactorText_TextChanged);
			// 
			// label2
			// 
			this.label2.ForeColor = System.Drawing.Color.Black;
			this.label2.Location = new System.Drawing.Point(16, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(152, 22);
			this.label2.TabIndex = 315;
			this.label2.Text = "X Factor (%)";
			// 
			// fundGrid
			// 
			this.fundGrid.AlternatingBackColor = System.Drawing.SystemColors.Window;
			this.fundGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.fundGrid.BackColor = System.Drawing.SystemColors.Window;
			this.fundGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.fundGrid.CaptionBackColor = System.Drawing.SystemColors.ActiveCaption;
			this.fundGrid.CaptionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.fundGrid.DataMember = "";
			this.fundGrid.FlatMode = false;
			this.fundGrid.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.fundGrid.ForeColor = System.Drawing.SystemColors.WindowText;
			this.fundGrid.GridLineColor = System.Drawing.SystemColors.Control;
			this.fundGrid.HeaderBackColor = System.Drawing.SystemColors.Control;
			this.fundGrid.HeaderFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.fundGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.fundGrid.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.fundGrid.Location = new System.Drawing.Point(16, 256);
			this.fundGrid.Name = "fundGrid";
			this.fundGrid.ParentRowsBackColor = System.Drawing.SystemColors.Control;
			this.fundGrid.ParentRowsForeColor = System.Drawing.SystemColors.WindowText;
			this.fundGrid.PrintColumnHeadingFont = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
			this.fundGrid.PrintColumnSettings = null;
			this.fundGrid.PrintPageHeadingFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.fundGrid.PrintStandardFont = new System.Drawing.Font("Arial", 8F);
			this.fundGrid.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
			this.fundGrid.SelectionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.fundGrid.Size = new System.Drawing.Size(720, 216);
			this.fundGrid.TabIndex = 2;
			// 
			// associationsTab
			// 
			this.associationsTab.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.associationsTab.Controls.Add(this.LabelPriceFile);
			this.associationsTab.Controls.Add(this.ComboBoxPriceFile);
			this.associationsTab.Controls.Add(this.fundGroupGroupBox);
			this.associationsTab.Location = new System.Drawing.Point(4, 25);
			this.associationsTab.Name = "associationsTab";
			this.associationsTab.Size = new System.Drawing.Size(752, 491);
			this.associationsTab.TabIndex = 2;
			this.associationsTab.Text = "Associations";
			this.associationsTab.Visible = false;
			// 
			// LabelPriceFile
			// 
			this.LabelPriceFile.AutoSize = true;
			this.LabelPriceFile.ForeColor = System.Drawing.Color.Black;
			this.LabelPriceFile.Location = new System.Drawing.Point(16, 27);
			this.LabelPriceFile.Name = "LabelPriceFile";
			this.LabelPriceFile.Size = new System.Drawing.Size(64, 18);
			this.LabelPriceFile.TabIndex = 280;
			this.LabelPriceFile.Text = "Price File:";
			// 
			// ComboBoxPriceFile
			// 
			this.ComboBoxPriceFile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ComboBoxPriceFile.Location = new System.Drawing.Point(88, 24);
			this.ComboBoxPriceFile.Name = "ComboBoxPriceFile";
			this.ComboBoxPriceFile.Size = new System.Drawing.Size(320, 24);
			this.ComboBoxPriceFile.TabIndex = 279;
			this.ComboBoxPriceFile.SelectedIndexChanged += new System.EventHandler(this.ComboBoxPriceFile_SelectedIndexChanged);
			// 
			// fundGroupGroupBox
			// 
			this.fundGroupGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.fundGroupGroupBox.Controls.Add(this.pnlFundGroupAssocsListToList);
			this.fundGroupGroupBox.Controls.Add(this.label1);
			this.fundGroupGroupBox.ForeColor = System.Drawing.Color.Blue;
			this.fundGroupGroupBox.Location = new System.Drawing.Point(16, 72);
			this.fundGroupGroupBox.Name = "fundGroupGroupBox";
			this.fundGroupGroupBox.Size = new System.Drawing.Size(720, 400);
			this.fundGroupGroupBox.TabIndex = 274;
			this.fundGroupGroupBox.TabStop = false;
			this.fundGroupGroupBox.Text = "Fund Groups";
			// 
			// pnlFundGroupAssocsListToList
			// 
			this.pnlFundGroupAssocsListToList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.pnlFundGroupAssocsListToList.Location = new System.Drawing.Point(16, 64);
			this.pnlFundGroupAssocsListToList.Name = "pnlFundGroupAssocsListToList";
			this.pnlFundGroupAssocsListToList.Size = new System.Drawing.Size(688, 328);
			this.pnlFundGroupAssocsListToList.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.ForeColor = System.Drawing.Color.Black;
			this.label1.Location = new System.Drawing.Point(16, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(496, 16);
			this.label1.TabIndex = 276;
			this.label1.Text = "Select the Fund Group(s) that this Asset Fund belongs to.";
			// 
			// benchmarkSplitTab
			// 
			this.benchmarkSplitTab.Controls.Add(this.benchmarkGroupBox);
			this.benchmarkSplitTab.Location = new System.Drawing.Point(4, 25);
			this.benchmarkSplitTab.Name = "benchmarkSplitTab";
			this.benchmarkSplitTab.Size = new System.Drawing.Size(752, 491);
			this.benchmarkSplitTab.TabIndex = 6;
			this.benchmarkSplitTab.Text = "Benchmark Split";
			this.benchmarkSplitTab.Resize += new System.EventHandler(this.BenchmarkTab_Resize);
			// 
			// benchmarkGroupBox
			// 
			this.benchmarkGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.benchmarkGroupBox.Controls.Add(this.lblProportionTotal);
			this.benchmarkGroupBox.Controls.Add(this.pnlMiddle);
			this.benchmarkGroupBox.Controls.Add(this.lstSelectedBenchmarks);
			this.benchmarkGroupBox.Controls.Add(this.treAllBenchMarks);
			this.benchmarkGroupBox.Controls.Add(this.indicesTabDescLabel);
			this.benchmarkGroupBox.ForeColor = System.Drawing.Color.Blue;
			this.benchmarkGroupBox.Location = new System.Drawing.Point(16, 17);
			this.benchmarkGroupBox.Name = "benchmarkGroupBox";
			this.benchmarkGroupBox.Size = new System.Drawing.Size(720, 456);
			this.benchmarkGroupBox.TabIndex = 2;
			this.benchmarkGroupBox.TabStop = false;
			this.benchmarkGroupBox.Text = "Asset Fund Split";
			// 
			// lblProportionTotal
			// 
			this.lblProportionTotal.AutoSize = true;
			this.lblProportionTotal.Location = new System.Drawing.Point(464, 434);
			this.lblProportionTotal.Name = "lblProportionTotal";
			this.lblProportionTotal.Size = new System.Drawing.Size(0, 18);
			this.lblProportionTotal.TabIndex = 8;
			// 
			// pnlMiddle
			// 
			this.pnlMiddle.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.pnlMiddle.Controls.Add(this.btnAddBenchmark);
			this.pnlMiddle.Controls.Add(this.btnRemoveBenchmark);
			this.pnlMiddle.Location = new System.Drawing.Point(336, 160);
			this.pnlMiddle.Name = "pnlMiddle";
			this.pnlMiddle.Size = new System.Drawing.Size(128, 72);
			this.pnlMiddle.TabIndex = 7;
			// 
			// btnAddBenchmark
			// 
			this.btnAddBenchmark.Location = new System.Drawing.Point(8, 8);
			this.btnAddBenchmark.Name = "btnAddBenchmark";
			this.btnAddBenchmark.Size = new System.Drawing.Size(112, 23);
			this.btnAddBenchmark.TabIndex = 0;
			this.btnAddBenchmark.Text = "&Add >>";
			this.btnAddBenchmark.Click += new System.EventHandler(this.btnAddBenchmark_Click);
			// 
			// btnRemoveBenchmark
			// 
			this.btnRemoveBenchmark.Location = new System.Drawing.Point(8, 40);
			this.btnRemoveBenchmark.Name = "btnRemoveBenchmark";
			this.btnRemoveBenchmark.Size = new System.Drawing.Size(112, 23);
			this.btnRemoveBenchmark.TabIndex = 1;
			this.btnRemoveBenchmark.Text = "<< &Remove";
			this.btnRemoveBenchmark.Click += new System.EventHandler(this.btnRemoveBenchmark_Click);
			// 
			// lstSelectedBenchmarks
			// 
			this.lstSelectedBenchmarks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lstSelectedBenchmarks.FullRowSelect = true;
			this.lstSelectedBenchmarks.LabelEdit = true;
			this.lstSelectedBenchmarks.Location = new System.Drawing.Point(464, 56);
			this.lstSelectedBenchmarks.Name = "lstSelectedBenchmarks";
			this.lstSelectedBenchmarks.Size = new System.Drawing.Size(248, 376);
			this.lstSelectedBenchmarks.TabIndex = 6;
			this.lstSelectedBenchmarks.View = System.Windows.Forms.View.Details;
			this.lstSelectedBenchmarks.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.lstSelectedBenchmarks_AfterLabelEdit);
			// 
			// treAllBenchMarks
			// 
			this.treAllBenchMarks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.treAllBenchMarks.ImageIndex = -1;
			this.treAllBenchMarks.Location = new System.Drawing.Point(10, 56);
			this.treAllBenchMarks.Name = "treAllBenchMarks";
			this.treAllBenchMarks.SelectedImageIndex = -1;
			this.treAllBenchMarks.Size = new System.Drawing.Size(336, 376);
			this.treAllBenchMarks.Sorted = true;
			this.treAllBenchMarks.TabIndex = 5;
			// 
			// indicesTabDescLabel
			// 
			this.indicesTabDescLabel.ForeColor = System.Drawing.Color.Black;
			this.indicesTabDescLabel.Location = new System.Drawing.Point(16, 24);
			this.indicesTabDescLabel.Name = "indicesTabDescLabel";
			this.indicesTabDescLabel.Size = new System.Drawing.Size(568, 50);
			this.indicesTabDescLabel.TabIndex = 2;
			this.indicesTabDescLabel.Text = "Select the proportional impact of the Benchmarks that affect this Asset Fund\'s pr" +
				"ice";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(0, 0);
			this.label6.Name = "label6";
			this.label6.TabIndex = 0;
			// 
			// label3
			// 
			this.label3.ForeColor = System.Drawing.Color.Black;
			this.label3.Location = new System.Drawing.Point(16, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(568, 24);
			this.label3.TabIndex = 2;
			this.label3.Text = "The proportional investment breakdown of the Composite Fund.";
			// 
			// dataGrid1
			// 
			this.dataGrid1.AlternatingBackColor = System.Drawing.SystemColors.Window;
			this.dataGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.dataGrid1.BackColor = System.Drawing.SystemColors.Window;
			this.dataGrid1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.dataGrid1.CaptionBackColor = System.Drawing.SystemColors.ActiveCaption;
			this.dataGrid1.CaptionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.dataGrid1.DataMember = "";
			this.dataGrid1.FlatMode = false;
			this.dataGrid1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.dataGrid1.ForeColor = System.Drawing.SystemColors.WindowText;
			this.dataGrid1.GridLineColor = System.Drawing.SystemColors.Control;
			this.dataGrid1.HeaderBackColor = System.Drawing.SystemColors.Control;
			this.dataGrid1.HeaderFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid1.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.dataGrid1.Location = new System.Drawing.Point(16, 56);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.ParentRowsBackColor = System.Drawing.SystemColors.Control;
			this.dataGrid1.ParentRowsForeColor = System.Drawing.SystemColors.WindowText;
			this.dataGrid1.PrintColumnHeadingFont = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
			this.dataGrid1.PrintColumnSettings = null;
			this.dataGrid1.PrintPageHeadingFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.dataGrid1.PrintStandardFont = new System.Drawing.Font("Arial", 8F);
			this.dataGrid1.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
			this.dataGrid1.SelectionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.dataGrid1.Size = new System.Drawing.Size(688, 384);
			this.dataGrid1.TabIndex = 1;
			// 
			// AssetFundStaticDataEditor
			// 
			this.Controls.Add(this.tabs);
			this.Name = "AssetFundStaticDataEditor";
			this.Size = new System.Drawing.Size(760, 520);
			this.Load += new System.EventHandler(this.Form_Load);
			this.tabs.ResumeLayout(false);
			this.propertiesTab.ResumeLayout(false);
			this.tolerancesTab.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.tolerancesGrid)).EndInit();
			this.factorsTab.ResumeLayout(false);
			this.applyAllGroupBox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.fundGrid)).EndInit();
			this.associationsTab.ResumeLayout(false);
			this.fundGroupGroupBox.ResumeLayout(false);
			this.benchmarkSplitTab.ResumeLayout(false);
			this.benchmarkGroupBox.ResumeLayout(false);
			this.pnlMiddle.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		#region UI Events

		#region tolerance tab

		private void lowerTolText_TextChanged(object sender, EventArgs e)
		{
			T.E();
			try
			{
				Changed = true;
				m_fundsEdited = true;
				m_changedLowerTolerance=true;
			}
			catch (Exception ex)
			{
				//This is a top level UI event, so catch it here & show exception
				GUIExceptionHelper.LogAndDisplayException("SystemError", "UnexpectedErrorTitle", ex);
			}
			finally
			{
				T.X();
			}
		}

		private void upperTolText_TextChanged(object sender, EventArgs e)
		{
			T.E();
			try
			{
				Changed = true;
				m_fundsEdited = true;
				m_changedUpperTolerance=true;
			}
			catch (Exception ex)
			{
				//This is a top level UI event, so catch it here & show exception
				GUIExceptionHelper.LogAndDisplayException("SystemError", "UnexpectedErrorTitle", ex);
			}
			finally
			{
				T.X();
			}
		}

		private void assetMovementTolText_TextChanged(object sender, EventArgs e)
		{
			T.E();
			try
			{
				Changed = true;
				m_fundsEdited = true;
			}
			catch (Exception ex)
			{
				//This is a top level UI event, so catch it here & show exception
				GUIExceptionHelper.LogAndDisplayException("SystemError", "UnexpectedErrorTitle", ex);
			}
			finally
			{
				T.X();
			}
		}

		private void priceIncCheck_CheckedChanged(object sender, EventArgs e)
		{
			T.E();
			try
			{
				Changed = true;
				m_priceIncreaseOnlyChanged = true;
				m_fundsEdited = true;
			}
			catch (Exception ex)
			{
				//This is a top level UI event, so catch it here & show exception
				GUIExceptionHelper.LogAndDisplayException("SystemError", "UnexpectedErrorTitle", ex);
			}
			finally
			{
				T.X();
			}
		}

		#endregion

		#region factors tab

		private void xFactorNarrTextBox_TextChanged(object sender, EventArgs e)
		{
			T.E();
			try
			{
				Changed = true;
				m_fundsEdited = true;
				m_changedXFactor=false;
			}
			catch (Exception ex)
			{
				//This is a top level UI event, so catch it here & show exception
				GUIExceptionHelper.LogAndDisplayException("SystemError", "UnexpectedErrorTitle", ex);
			}
			finally
			{
				T.X();
			}
		}

		private void revalText_TextChanged(object sender, EventArgs e)
		{
			T.E();
			try
			{
				Changed = true;
				m_fundsEdited = true;
				m_changedRevaluationFactor=true;
			}
			catch (Exception ex)
			{
				//This is a top level UI event, so catch it here & show exception
				GUIExceptionHelper.LogAndDisplayException("SystemError", "UnexpectedErrorTitle", ex);
			}
			finally
			{
				T.X();
			}
		}

		private void tpeText_TextChanged(object sender, EventArgs e)
		{
			T.E();
			try
			{
				Changed = true;
				m_fundsEdited = true;
				m_changedTPE=true;
			}
			catch (Exception ex)
			{
				//This is a top level UI event, so catch it here & show exception
				GUIExceptionHelper.LogAndDisplayException("SystemError", "UnexpectedErrorTitle", ex);
			}
			finally
			{
				T.X();
			}
		}

		private void xFactorText_TextChanged(object sender, EventArgs e)
		{
			T.E();
			try
			{
				Changed = true;
				m_fundsEdited = true;
				m_changedXFactor=true;
			}
			catch (Exception ex)
			{
				//This is a top level UI event, so catch it here & show exception
				GUIExceptionHelper.LogAndDisplayException("SystemError", "UnexpectedErrorTitle", ex);
			}
			finally
			{
				T.X();
			}
		}

		#endregion	

		private void toleranceText_Leave(object sender, EventArgs e)
		{
			T.E();
			Control senderControl = (sender as Control);
			if (senderControl != null && senderControl.Text != string.Empty)
			{
				decimal tolerance;
				if (safeDecimalParse(senderControl.Text, out tolerance))
				{
					senderControl.Text = tolerance.ToString(toleranceFormatString);
				}
			}
			T.X();
		}

		private void gridRowChanged(object sender, CellEventArgs e)
		{
			T.E();
			try
			{
				m_fundsEdited = true;
				m_FundGridChanged=true;
				Changed = true;
			}
			catch (Exception ex)
			{
				//This is a top level UI event, so catch it here & show exception
				GUIExceptionHelper.LogAndDisplayException("SystemError", "UnexpectedErrorTitle", ex);
			}
			finally
			{
				T.X();
			}

		}

		private void revalEndDateDTPickerChanged(object sender, EventArgs e)
		{
			if (DateTime.Compare(revalEndDateDTPicker.Value,GlobalRegistry.NextCompanyValuationDateAndTime)<0)
			{
				revalEndDateDTPicker.Value=GlobalRegistry.NextCompanyValuationDateAndTime;
			}
			else
			{
				if (!m_systemGeneratedFactorEvent)
				{
					m_fundsEdited = true;
					m_changedRevaluationFactor=true;
					Changed = true;
				}
			}

		}

		private void defaultControlChanged(object sender, EventArgs e)
		{
			if (!m_systemGeneratedEvent)
			{
				Changed = true;
			}
		}

		private void tabSelected(object sender, EventArgs e)
		{
			T.E();
			try
			{
				if (!m_tabClearing)
				{
					bool oldChanged = Changed;
					bool oldPriceIncChanged = m_priceIncreaseOnlyChanged;
					bool oldBenchmarksChanged = m_benchmarksChanged;

					if (tabs.SelectedTab == associationsTab)
					{
						m_associationsTabClicked = true;
						displayAssociationsTab();
					}
					else if (tabs.SelectedTab == factorsTab)
					{
						m_factorsTabClicked = true;
						displayFactorsTab(retrieveFunds());
					}
					else if (tabs.SelectedTab == tolerancesTab)
					{
						m_tolerancesTabClicked = true;
						displayTolerancesTab(retrieveFunds());
					}
					else if (tabs.SelectedTab == benchmarkSplitTab)
					{
						m_benchmarkTabClicked = true;
						displayBenchmarkTab();
					}
					//displaying the data on the page causes events to fire which sets the 
					//changed flag. we only want the changed flag to update when the user 
					//clicks on something, not when we set it ourselves!
					Changed = oldChanged;
					m_priceIncreaseOnlyChanged = oldPriceIncChanged;
					m_benchmarksChanged = oldBenchmarksChanged;
				}
			}
			catch (Exception ex)
			{
				//This is a top level UI event, so catch it here & show exception
				GUIExceptionHelper.LogAndDisplayLoadException(assetFundTypeName, ex);
			}
			finally
			{
				T.X();
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
			parameters.CollectionToExport = AssetFundDecorator.ToDecoratedList(AssetFundController.LoadAssetFundsByCompanyIdForStaticDataExport(GlobalRegistry.ConnectionString, GlobalRegistry.CompanyCode));
			parameters.Exports.Add(new StaticDataExport("HBOS.FS.AMP.UPD.WinUI.Classes.AssetFundStaticDataFundGroups.xslt", "assetfunds_fundgroupmembership"));
			parameters.Exports.Add(new StaticDataExport("HBOS.FS.AMP.UPD.WinUI.Classes.AssetFundStaticDataMarketSplits.xslt", "assetfunds_benchmarksplits"));
			//	parameters.Exports.Add(new StaticDataExport("HBOS.FS.AMP.UPD.WinUI.Classes.AssetFundStaticDataCompositeSplits.xslt", "assetfunds_compsplits"));
			T.X();
		}


		/// <summary>
		/// Does the delete of the fund.
		/// </summary>
		protected override void doDelete()
		{
			T.E();
			try
			{
				UpdateAssetFund();
				CurrentAssetFund.IsDeleted = true;
				AssetFundController.UpdateAssetFund(GlobalRegistry.ConnectionString, CurrentAssetFund);
			}
			catch (AssetFundAssocDeletionException)
			{
				MessageBoxHelper.Show("CannotDeleteAssetFundBody", "CannotDeleteAssetFundTitle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}

			T.X();
		}

		/// <summary>
		/// Load the asset fund into the editor
		/// </summary>
		protected override void doLoadEntity()
		{
			T.E();
			m_cachedFunds = null;
			m_fundsEdited = false;
			m_changedUpperTolerance=false;
			m_changedLowerTolerance=false;
			m_changedXFactor=false;
			m_changedTPE=false;
			m_FundGridChanged=false;
			m_changedRevaluationFactor=false;
			string assetFundCode = ((SimpleStringLookup) ListManager.SelectedItem).Key;
			CurrentAssetFund = AssetFundController.LoadAssetFundForStaticData(GlobalRegistry.ConnectionString, assetFundCode);
			T.X();
		}

		/// <summary>
		/// Action to new when a new asset fund is requested
		/// </summary>
		protected override void doNew()
		{
			T.E();
			try
			{
				CurrentAssetFund = new AssetFund();
				codeTextBox.ReadOnly = false;
				codeTextBox.Focus();
				Changed = true;
				m_benchmarksChanged = false;
				codeTextBox.Text = String.Empty;
				fullNameTextBox.Text = String.Empty;
				shortNameTextBox.Text = String.Empty;

				m_priceIncreaseOnlyChanged = false;
				m_associationsTabClicked = false;
				m_factorsTabClicked = false;
				m_tolerancesTabClicked = false;
				m_benchmarkTabClicked = false;
				m_refreshAssocs = true;
				m_refreshBenchMarks = true;

				displayAssetFund(true);
				fundsTypeCombo.SelectedIndex = 0;

				codeTextBox.Focus();
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Saves the asset fund data displayed in the editor
		/// </summary>
		/// <returns></returns>
		protected override bool doSave()
		{
			T.E();
			bool isValid = true;
			try
			{
				if (Changed)
				{
					try
					{
						//still retains editor newassetfundtype for new items - we only swap over to specific assetfundtype when validated
						UpdateAssetFund();

						if (ListManager.SelectedIsNew)
						{
							isValid = doSaveNew();
						}
						else
						{
							object[] errors = validateUpdatedAssetFund();
							isValid = errors.Length == 0;
							if (isValid)
							{
								if (m_fundsEdited && m_cachedFunds != null)
								{
									AssetFundController.UpdateAssetFundAndChildFunds(GlobalRegistry.ConnectionString, CurrentAssetFund, (FundCollection) m_cachedFunds);
								}
								else
								{
									AssetFundController.UpdateAssetFund(GlobalRegistry.ConnectionString, CurrentAssetFund);
								}

								Changed = false;
								ListManager.ChangeSelected(new SimpleStringLookup(CurrentAssetFund.AssetFundCode, CurrentAssetFund.ShortName));
							}
							else if (m_cachedFunds != null && m_cachedFunds.Count > 0)
							{
								bool amToleranceValid = true;
								bool upperToleranceValid = true;
								bool lowerToleranceValid = true;

								foreach (object error in errors)
								{
									if (error is FundController.FundValidationError)
									{
										if ((FundController.FundValidationError) error == FundController.FundValidationError.FundUpperToleranceLessThanLowerTolerance)
										{
											amToleranceValid = false;
										}
										else if ((FundController.FundValidationError) error == FundController.FundValidationError.FundUpperToleranceNegative ||
											(FundController.FundValidationError) error == FundController.FundValidationError.FundUpperToleranceInvalidNumber ||
											(FundController.FundValidationError) error == FundController.FundValidationError.FundUpperToleranceZero ||
											(FundController.FundValidationError) error == FundController.FundValidationError.FundInvalidNumDecimalPlacesUpperTolerance ||
											(FundController.FundValidationError) error == FundController.FundValidationError.FundMaxUpperToleranceExceeded)
										{
											upperToleranceValid = false;
										}
										else if ((FundController.FundValidationError) error == FundController.FundValidationError.FundLowerToleranceNegative ||
											(FundController.FundValidationError) error == FundController.FundValidationError.FundLowerToleranceInvalidNumber ||
											(FundController.FundValidationError) error == FundController.FundValidationError.FundLowerToleranceZero ||
											(FundController.FundValidationError) error == FundController.FundValidationError.FundInvalidNumDecimalPlacesLowerTolerance ||
											(FundController.FundValidationError) error == FundController.FundValidationError.FundMaxLowerToleranceExceeded)
										{
											lowerToleranceValid = false;
										}
									}
								}
								if (!amToleranceValid || !upperToleranceValid || !lowerToleranceValid)
								{
									refreshValidValuesFromFundsGrid(((FundCollection) m_cachedFunds), amToleranceValid, upperToleranceValid, lowerToleranceValid);
								}
							}

						}
					}
					catch (InvalidFactorException ex)
					{
						isValid = false;
						//TODO - rather than do this by catching exception, perform som UI validation?
						string factorType;

						if (ex is InvalidXFactorException)
						{
							factorType = "x factor";
						}
						else if (ex is InvalidTaxProvisionEstimateException)
						{
							factorType = "tax provision estimate";
						}
						else if (ex is InvalidRevaluationFactorException)
						{
							factorType = "revaluation factor";
						}
						else if (ex is InvalidScalingFactorException)
						{
							factorType = "scaling factor";
						}
						else
						{
							throw new ArgumentException("Invalid factor exception type");
						}
						GUIExceptionHelper.LogAndDisplayException("UnableToSaveAssetFundFactor", "GenericUnableToSaveParmlessTitle2", ex, factorType);
					}
				}
			}
			finally
			{
				T.X();
			}
			return isValid;
		}

		/// <summary>
		/// Gets the description of the current asset fund.
		/// </summary>
		/// <value></value>
		protected override string currentEntityDescription
		{
			get { return CurrentAssetFund.FullName; }
		}

		/// <summary>
		/// Specifies the entity type being edited
		/// </summary>
		/// <value></value>
		protected override string EditType
		{
			get { return "Asset Fund"; }
		}

		private TreeNode GetNode(string nodeTextToCheck, TreeNodeCollection nodes)
		{
			TreeNode returnNode = null;
			foreach (TreeNode node in nodes)
			{
				if (node.Text == nodeTextToCheck)
				{
					returnNode = node;
					break;
				}
			}
			return returnNode;
		}

		private ExclusiveListHelper treeHelper;
		private IAsyncResult loadAllAvailableBenchmarksAsyncResult;
		private bool m_changedUpperTolerance=false;
		private bool m_changedTPE=false;
		private bool m_changedLowerTolerance=false;
		private bool m_changedXFactor=false;
		private bool m_changedRevaluationFactor=false;
		private bool m_FundGridChanged=false;

		/// <summary>
		/// Fills the all benchmarks tree.
		/// </summary>
		public void FillAllBenchmarksTree()
		{
			this.SuspendLayout();

			for (int i = 0; i < treeHelper.Excluded.Count; i++)
			{
				AddBenchMark(((AssetMovementConstituentDecorator) treeHelper.Excluded[i]));
			}
			this.ResumeLayout();
		}

		private void AddBenchMark(AssetMovementConstituentDecorator constituent)
		{
			TreeNode typeNode = GetNode(constituent.BenchMarkType, treAllBenchMarks.Nodes);
			if (typeNode == null)
			{
				typeNode = AddBenchmarkNode(constituent.BenchMarkType, treAllBenchMarks.Nodes);
			}

			TreeNode subTypeNode;
			if (constituent.BenchMarkSubType == null)
			{
				subTypeNode = typeNode;
			}
			else
			{
				subTypeNode = GetNode(constituent.BenchMarkSubType, typeNode.Nodes);
				if (subTypeNode == null)
				{
					subTypeNode = AddBenchmarkNode(constituent.BenchMarkSubType, typeNode.Nodes);
				}
			}

			TreeNode benchMarkNode = GetNode(constituent.BenchmarkDisplayName, subTypeNode.Nodes);
			if (benchMarkNode == null)
			{
				benchMarkNode = AddBenchmarkNode(constituent.BenchmarkDisplayName, subTypeNode.Nodes);
				benchMarkNode.Tag = constituent;
			}


		}

		private TreeNode AddBenchmarkNode(string nodeText, TreeNodeCollection nodes)
		{
			return nodes.Add(nodeText);
		}

		/// <summary>
		/// Validates the benchmark can be added to the asset fund.
		/// </summary>
		/// <returns></returns>
		private StringBuilder validateCanAddBenchmark(string benchmarkFundCode)
		{
			// Use a stringBuilder class to build the output string of 
			// referenced bechmark funds asset funds	
			StringBuilder output = new StringBuilder();

			// Check if a circular reference exists against the asset fund and specified benchmark fund 
			string canAddBenchmark = AssetFundController.CanBenchmarkBeAddedToAssetFund(GlobalRegistry.ConnectionString, this.codeTextBox.Text, benchmarkFundCode);

			// Was there any link back to the initial asset fund?
			if (!canAddBenchmark.Equals(string.Empty))
			{
				// Constants for the delimiter
				const char semiColon = ';';
				const char colon = ':';

				// Array of delimiters to split the result of the circular reference
				char[] delimiters = new char[]
					{
						semiColon,
						colon
					};

				//
				// TODO: Add text to resource file
				//

				// Set message header
				//output.Append("The selected fund cannot be used for benchmarking as it relates back to the current Asset Fund.\n\n");

				// Check for basic circular reference and add simple text message
				if (canAddBenchmark.IndexOfAny(delimiters) == 0)
				{
					output.Append("Current asset fund code : " + canAddBenchmark);
				}
					// Handle more complicated circular references
				else
				{
					int fundCount = 0;

					// Split the string and then iterate over the resulting array of sub strings
					foreach (string subString in canAddBenchmark.Split(delimiters))
					{
						if (fundCount == 0)
						{
							output.AppendFormat("\n\nCurrent asset fund code : {0}\n\n", subString.Trim());
							output.Append("Circular reference;\n");
							fundCount = 1;
						}
						else
						{
							output.AppendFormat("\t{0}. Fund {1}\n", fundCount++, subString.Replace("(", " (parent asset fund = ").Trim());
						}
					}
				}
			}

			return output;
		}

		#endregion

		private void btnAddBenchmark_Click(object sender, EventArgs e)
		{
			TreeNode selectedNode = this.treAllBenchMarks.SelectedNode;

			if (selectedNode != null && selectedNode.Tag != null)
			{
				IList selectedEntities = new object[1];
				selectedEntities[0] = selectedNode.Tag;

				bool cancel = false;
				Fund fund = ((AssetMovementConstituentDecorator) selectedEntities[0]).BenchMark as Fund;
				if (fund != null)
				{
					StringBuilder circularRef = validateCanAddBenchmark(fund.HiPortfolioCode);
					if (circularRef.Length > 0)
					{
						MessageBoxHelper.ShowExclamation("CircularReferenceBody", "CircularReferenceTitle", circularRef.ToString());
						cancel = true;
					}
				}

				if (!cancel)
				{
					((AssetMovementConstituentDecorator) selectedEntities[0]).Proportion = 0;

					if (selectedEntities.Count > 0)
					{
						ListToListChangingArgs args = new ListToListChangingArgs(false, selectedEntities);
						benchMarkTreeToListChanging(this, args);

						if (!args.Cancel)
						{
							treeHelper.Include(selectedEntities);
							TidyParentNodesWithNoChildren(selectedNode);
							this.FillSelectedBenchMarkList();
						}
					}
				}
			}

			this.treAllBenchMarks.SelectedNode = null;
		}

		private void TidyParentNodesWithNoChildren(TreeNode selectedNode)
		{ //limited needs changing

			this.SuspendLayout();
			TreeNode gp = null;
			if (selectedNode.Parent.Parent != null) gp = selectedNode.Parent.Parent;
			TreeNode p = null;
			if (selectedNode.Parent != null) p = selectedNode.Parent;
			;

			selectedNode.Remove();
			if (p != null && p.Nodes.Count == 0) p.Remove();
			if (gp != null && gp.Nodes.Count == 0) gp.Remove();
			this.ResumeLayout();
		}

		private void btnRemoveBenchmark_Click(object sender, EventArgs e)
		{
			IList selectedEntities = ListToListControl.getEntitiesFromListViewItems(lstSelectedBenchmarks.SelectedItems);

			if (selectedEntities.Count > 0)
			{
				ListToListChangingArgs args = new ListToListChangingArgs(false, selectedEntities);
				benchMarkTreeToListChanging(this, args);

				if (!args.Cancel)
				{
					treeHelper.Exclude(selectedEntities);
					this.FillSelectedBenchMarkList();
					AddBenchMark(selectedEntities[0] as AssetMovementConstituentDecorator);
				}
			}
		}

		private void refreshBenchMarkLists()
		{
			treAllBenchMarks.Nodes.Clear();
			this.FillAllBenchmarksTree();
			this.FillSelectedBenchMarkList();
		}

		/// <summary>
		/// Forms load event handler.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		public void Form_Load(object sender, EventArgs e)
		{
			loadAllAvailableBenchmarksAsyncResult = AssetFundController.BeginLoadAllAvailableBenchmarks(GlobalRegistry.ConnectionString);
		}

		private void BenchmarkTab_Resize(object sender, EventArgs e)
		{
			this.SuspendLayout();
			try
			{
				int maxWidth = this.benchmarkGroupBox.Width - 50;
				int maxHeight = this.benchmarkGroupBox.Height - 60;

				Size listSize = new Size((maxWidth - pnlMiddle.Width)/2, maxHeight);
				this.treAllBenchMarks.Size = listSize;
				this.lstSelectedBenchmarks.Size = listSize;


				pnlMiddle.Left = treAllBenchMarks.Right + 1;
				lstSelectedBenchmarks.Left = pnlMiddle.Right + 1;
				indicesTabDescLabel.Left = treAllBenchMarks.Left;
				this.lblProportionTotal.Location = new Point(this.lstSelectedBenchmarks.Left, indicesTabDescLabel.Top);
				indicesTabDescLabel.Width = treAllBenchMarks.Width;
			}
			finally
			{
				this.ResumeLayout();
			}
		}

		private void fundsTypeCombo_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (!m_systemGeneratedEvent)
			{
				Changed = true;
			}
		}

		private void ComboBoxPriceFile_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Changed = true;
		}

	}

}
