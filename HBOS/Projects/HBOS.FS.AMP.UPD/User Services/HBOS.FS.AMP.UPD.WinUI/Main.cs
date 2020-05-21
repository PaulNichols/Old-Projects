using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using HBOS.FS.AMP.ExceptionManagement;
using HBOS.FS.AMP.Security;
using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Security;
using HBOS.FS.AMP.UPD.Types.Lookups;
using HBOS.FS.AMP.UPD.Types.Status;
using HBOS.FS.AMP.UPD.WinUI.Classes;
using HBOS.FS.AMP.UPD.WinUI.Forms;
using HBOS.FS.AMP.UPD.WinUI.Helpers;
using HBOS.FS.AMP.UPD.WinUI.Interfaces;
using HBOS.FS.AMP.UPD.WinUI.UserControls;
using HBOS.FS.Common.ExceptionManagement;
using HBOS.FS.Support.Tex;
using SteepValley.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace HBOS.FS.AMP.UPD.WinUI
{
	/// <summary>
	/// Main form for application.
	/// </summary>
	public class Main : UPDRoleCheckedForm
	{
		#region Controls

		private MainMenu mainMenu;

		#endregion

		#region File Menu

		private MenuItem fileMenuItem;

		[PermittedRoles("ImportExchangeRate,ImportHi3Prices,importCompositePrices,importLinkedPrices,ImportOverseasFundWeightings,ImportMarketIndices,ImportLinkedFundSplit")] private MenuItem ImportFilesMenuItem;
		[PermittedRoles("ImportExchangeRate")] private MenuItem FileImportFilesImportCurrencyExchangeRatesMenuItem;
		[PermittedRoles("MaintainCurrencies")] private MenuItem staticDataCurrenciesMenuItem;
		[PermittedRoles("MaintainCountries")] private MenuItem staticDataCountryMenuItem;
		[PermittedRoles("MaintainStockMarkets")] private MenuItem staticDataStockMarketMenuItem;
		[PermittedRoles("importLinkedPrices")] private MenuItem FileImportFilesHi3PricesLinkedMenuItem;
		[PermittedRoles("importCompositePrices")] private MenuItem FileImportFilesHi3PricesCompositeMenuItem;
		[PermittedRoles("ImportHi3Prices")] private MenuItem FileImportFilesHi3PricesMenuItem;
		[PermittedRoles("ImportOverseasFundWeightings")] private MenuItem FileImportFilesHi3AssetFundSplitsMenuItem;
		[PermittedRoles("ImportMarketIndices")] private MenuItem FileImportFilesImportStockMarketIndicesMenuItem;
		[PermittedRoles("ImportCompositeSplits")] private MenuItem FileImportFilesHi3CompositeFundSplitsMenuItem;


		private MenuItem fileExitMenuItem;

		#endregion

		#region View Menu

		private MenuItem viewMenuItem;
		private MenuItem viewCurrentFundStatusmenuItem;
		private MenuItem viewCurrentAssetFundmenuItem;

		#endregion

		#region Company Menu		

		private MenuItem companyMenuItem;

		#endregion		        

		#region Reports Menu

		private MenuItem reportsMenu;
		private MenuItem reportsFundDriftMenu;
		private MenuItem reportsPredictedPriceMenu;
		[PermittedRoles("PriceComparision")] private MenuItem PriceComparisionMenu;

		#endregion

		#region Static Data Menu

		[PermittedRoles("Administrator,MaintainFundMappings,MaintainFundGroups,MaintainUserAccess,MaintainValidationTolerances,MaintainCalculationFactors,MaintainCalculationIndices")] private MenuItem StaticDataMenuItem;
		[PermittedRoles("MaintainFundMappings")] private MenuItem staticDataFundMappingMenuItem;
		[PermittedRoles("MaintainFundGroups")] private MenuItem staticDataFundGroupsMenuItem;
		[PermittedRoles("MaintainUserAccess")] private MenuItem staticDataUsersMenuItem;
		[PermittedRoles("MaintainAssetFunds")] private MenuItem staticDataAssetFundsMenuItem;

		#endregion

		#region Tasks Menu

		[PermittedRoles("AuthorisePrices,ReleasePrices,DistributePrices")] private MenuItem tasksMenuItem;
		[PermittedRoles("AuthorisePrices")] private MenuItem secondLevelMenuItem;
		[PermittedRoles("ReleasePrices")] private MenuItem releaseMenuItem;
		[PermittedRoles("DistributePrices")] private MenuItem taskMenuSeperator;
		[PermittedRoles("DistributePrices")] private MenuItem distributeMenuItem;

		#endregion                

		#region Help Menu

		private MenuItem helpMenuItem;
		private MenuItem helpAboutMenuItem;

		#endregion

		#region Form Controls

		private XPCaption mainTitleBar;
		private Timer statusbarUpdateTimer;
		private StatusBar statusBar;
		private StatusBarPanel statusBarCompany;
		private StatusBarPanel statusBarDatabase;
		private StatusBarPanel currentValuationPoint;
		private ContextMenu statusBarContextMenu;
		private MenuItem refreshStatusBar;
		private StatusBarPanel refreshPanel;

		private IContainer components;

		#endregion

		#region Fields

		//	private bool useConfigDirectory = true;
		private bool m_bLayoutCalled;
		private CurrentImportStatus previousImportStatus;
		private Icon refreshIcon;
		private MenuItem menuItem1;
		private MenuItem companyStatusMenuItem;
		private MenuItem menuItem2;
		[PermittedRoles("EndPricingDay")] private MenuItem ValuationDayMenu;
		[PermittedRoles("MaintainPriceFile")] private MenuItem PriceFileMenu;
		private MenuItem menuItem3;
		private MenuItem menuItem4;
		private MenuItem menuItem5;
		private MenuItem menuItem6;
		[PermittedRoles("SwapConnection")] private MenuItem mnuDatabase;

		private Panel displayArea;

		#endregion

		#region Delegate

		/// <summary>
		/// 
		/// </summary>
		public delegate CurrentImportStatus StatusBarDelegate(string connectionString, string companyCode);

		#endregion

		#region Constructors and destructors

		/// <summary>
		/// Form entry point
		/// </summary>
		public Main()
		{
			T.E();
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Create the companies menu
			Cursor oldCursor = this.Cursor;
			this.Cursor = Cursors.WaitCursor;
			T.Log("Load up the Companies list for the current user");

			//CompanyController companyController = new CompanyController();
			SimpleStringLookupCollection companies = CompanyController.LoadCompanies(GlobalRegistry.ConnectionString);

			bool[] shortCuts = new bool[26];

			UPDPrincipal currentPrincipal = (UPDPrincipal) Thread.CurrentPrincipal;

			foreach (SimpleStringLookup company in companies)
			{
				// Work out the available shortcut keys
				string companyName = company.DisplayValue;
				for (int position = 0; position < companyName.Length; position++)
				{
					char shortCutLetter = companyName.ToUpper(CultureInfo.CurrentUICulture)[position];
					if (false == shortCuts[(int) shortCutLetter - (int) 'A'])
					{
						companyName = companyName.Insert(position, "&");
						shortCuts[(int) shortCutLetter - (int) 'A'] = true;
						break;
					}
				}

				MenuItem companyItem = new MenuItem(companyName,
				                                    new EventHandler(this.menuCompany_Click));
				if (currentPrincipal.CompanyCode.Trim() == company.Key)
					companyItem.Checked = true;
				companyMenuItem.MenuItems.Add(companyItem);

			}

			if (companyMenuItem.MenuItems.Count == 1)
				companyMenuItem.Visible = false;

			T.Log("Configure status bar");
			this.statusBarCompany.Text += ((UPDPrincipal) Thread.CurrentPrincipal).CompanyCode;
			this.statusBarDatabase.Text += GlobalRegistry.SQLServerName + ',' + GlobalRegistry.SQLDatabaseName;
			statusBarDatabase.ToolTipText = GlobalRegistry.Credentials;


			if (refreshIcon == null)
				refreshIcon = refreshPanel.Icon;


//			this.updateStatusBar();
//	
//			Application.DoEvents();

			T.Log("Start status refresh timer");
			statusbarUpdateTimer.Start();

			this.Cursor = oldCursor;
			T.X();
		}

		/// <summary>
		/// Form Unload
		/// </summary>
		~Main()
		{
			T.E();
			statusbarUpdateTimer.Stop();
			T.X();
		}

		#endregion

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof (Main));
			this.mainMenu = new System.Windows.Forms.MainMenu();
			this.fileMenuItem = new System.Windows.Forms.MenuItem();
			this.ImportFilesMenuItem = new System.Windows.Forms.MenuItem();
			this.FileImportFilesImportCurrencyExchangeRatesMenuItem = new System.Windows.Forms.MenuItem();
			this.FileImportFilesImportStockMarketIndicesMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.FileImportFilesHi3PricesMenuItem = new System.Windows.Forms.MenuItem();
			this.FileImportFilesHi3PricesCompositeMenuItem = new System.Windows.Forms.MenuItem();
			this.FileImportFilesHi3PricesLinkedMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.FileImportFilesHi3AssetFundSplitsMenuItem = new System.Windows.Forms.MenuItem();
			this.FileImportFilesHi3CompositeFundSplitsMenuItem = new System.Windows.Forms.MenuItem();
			this.fileExitMenuItem = new System.Windows.Forms.MenuItem();
			this.viewMenuItem = new System.Windows.Forms.MenuItem();
			this.viewCurrentFundStatusmenuItem = new System.Windows.Forms.MenuItem();
			this.viewCurrentAssetFundmenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.companyStatusMenuItem = new System.Windows.Forms.MenuItem();
			this.companyMenuItem = new System.Windows.Forms.MenuItem();
			this.reportsMenu = new System.Windows.Forms.MenuItem();
			this.reportsFundDriftMenu = new System.Windows.Forms.MenuItem();
			this.reportsPredictedPriceMenu = new System.Windows.Forms.MenuItem();
			this.PriceComparisionMenu = new System.Windows.Forms.MenuItem();
			this.StaticDataMenuItem = new System.Windows.Forms.MenuItem();
			this.staticDataAssetFundsMenuItem = new System.Windows.Forms.MenuItem();
			this.staticDataFundMappingMenuItem = new System.Windows.Forms.MenuItem();
			this.staticDataFundGroupsMenuItem = new System.Windows.Forms.MenuItem();
			this.PriceFileMenu = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.staticDataCountryMenuItem = new System.Windows.Forms.MenuItem();
			this.staticDataCurrenciesMenuItem = new System.Windows.Forms.MenuItem();
			this.staticDataStockMarketMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.staticDataUsersMenuItem = new System.Windows.Forms.MenuItem();
			this.tasksMenuItem = new System.Windows.Forms.MenuItem();
			this.secondLevelMenuItem = new System.Windows.Forms.MenuItem();
			this.releaseMenuItem = new System.Windows.Forms.MenuItem();
			this.taskMenuSeperator = new System.Windows.Forms.MenuItem();
			this.distributeMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.ValuationDayMenu = new System.Windows.Forms.MenuItem();
			this.helpMenuItem = new System.Windows.Forms.MenuItem();
			this.helpAboutMenuItem = new System.Windows.Forms.MenuItem();
			this.mainTitleBar = new SteepValley.Windows.Forms.XPCaption();
			this.statusbarUpdateTimer = new System.Windows.Forms.Timer(this.components);
			this.statusBar = new System.Windows.Forms.StatusBar();
			this.statusBarContextMenu = new System.Windows.Forms.ContextMenu();
			this.refreshStatusBar = new System.Windows.Forms.MenuItem();
			this.statusBarCompany = new System.Windows.Forms.StatusBarPanel();
			this.currentValuationPoint = new System.Windows.Forms.StatusBarPanel();
			this.statusBarDatabase = new System.Windows.Forms.StatusBarPanel();
			this.refreshPanel = new System.Windows.Forms.StatusBarPanel();
			this.displayArea = new System.Windows.Forms.Panel();
			this.mnuDatabase = new System.Windows.Forms.MenuItem();
			((System.ComponentModel.ISupportInitialize) (this.statusBarCompany)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.currentValuationPoint)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.statusBarDatabase)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.refreshPanel)).BeginInit();
			this.SuspendLayout();
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
				{
					this.fileMenuItem,
					this.viewMenuItem,
					this.companyMenuItem,
					this.reportsMenu,
					this.StaticDataMenuItem,
					this.tasksMenuItem,
					this.helpMenuItem
				});
			// 
			// fileMenuItem
			// 
			this.fileMenuItem.Index = 0;
			this.fileMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
				{
					this.ImportFilesMenuItem,
					this.fileExitMenuItem
				});
			this.fileMenuItem.Text = "&File";
			// 
			// ImportFilesMenuItem
			// 
			this.ImportFilesMenuItem.Index = 0;
			this.ImportFilesMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
				{
					this.FileImportFilesImportCurrencyExchangeRatesMenuItem,
					this.FileImportFilesImportStockMarketIndicesMenuItem,
					this.menuItem5,
					this.FileImportFilesHi3PricesMenuItem,
					this.FileImportFilesHi3PricesCompositeMenuItem,
					this.FileImportFilesHi3PricesLinkedMenuItem,
					this.menuItem6,
					this.FileImportFilesHi3AssetFundSplitsMenuItem,
					this.FileImportFilesHi3CompositeFundSplitsMenuItem
				});
			this.ImportFilesMenuItem.Text = "&Import Files";
			// 
			// FileImportFilesImportCurrencyExchangeRatesMenuItem
			// 
			this.FileImportFilesImportCurrencyExchangeRatesMenuItem.Index = 0;
			this.FileImportFilesImportCurrencyExchangeRatesMenuItem.Text = "&Currency Exchange Rates";
			this.FileImportFilesImportCurrencyExchangeRatesMenuItem.Click += new System.EventHandler(this.FileImportFilesImportCurrencyExchangeRatesMenuItem_Click);
			// 
			// FileImportFilesImportStockMarketIndicesMenuItem
			// 
			this.FileImportFilesImportStockMarketIndicesMenuItem.Index = 1;
			this.FileImportFilesImportStockMarketIndicesMenuItem.Text = "&Stock Market Indices";
			this.FileImportFilesImportStockMarketIndicesMenuItem.Click += new System.EventHandler(this.FileImportFilesImportStockMarketIndicesMenuItem_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 2;
			this.menuItem5.Text = "-";
			// 
			// FileImportFilesHi3PricesMenuItem
			// 
			this.FileImportFilesHi3PricesMenuItem.Index = 3;
			this.FileImportFilesHi3PricesMenuItem.Text = "Hi3 OEIC &Prices";
			this.FileImportFilesHi3PricesMenuItem.Click += new System.EventHandler(this.FileImportFilesHi3PricesMenuItem_Click);
			// 
			// FileImportFilesHi3PricesCompositeMenuItem
			// 
			this.FileImportFilesHi3PricesCompositeMenuItem.Index = 4;
			this.FileImportFilesHi3PricesCompositeMenuItem.Text = "Hi3 Composit&e Prices";
			this.FileImportFilesHi3PricesCompositeMenuItem.Click += new System.EventHandler(this.FileImportFilesHi3PricesCompositeMenuItem_Click);
			// 
			// FileImportFilesHi3PricesLinkedMenuItem
			// 
			this.FileImportFilesHi3PricesLinkedMenuItem.Index = 5;
			this.FileImportFilesHi3PricesLinkedMenuItem.Text = "Hi3 &Linked Prices";
			this.FileImportFilesHi3PricesLinkedMenuItem.Click += new System.EventHandler(this.FileImportFilesHi3PricesLinkedMenuItem_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 6;
			this.menuItem6.Text = "-";
			// 
			// FileImportFilesHi3AssetFundSplitsMenuItem
			// 
			this.FileImportFilesHi3AssetFundSplitsMenuItem.Index = 7;
			this.FileImportFilesHi3AssetFundSplitsMenuItem.Text = "Hi3 &Asset Fund Splits";
			this.FileImportFilesHi3AssetFundSplitsMenuItem.Click += new System.EventHandler(this.FileImportFilesHi3AssetFundSplitsMenuItem_Click);
			// 
			// FileImportFilesHi3CompositeFundSplitsMenuItem
			// 
			this.FileImportFilesHi3CompositeFundSplitsMenuItem.Index = 8;
			this.FileImportFilesHi3CompositeFundSplitsMenuItem.Text = "Hi3 C&omposite Fund Splits";
			this.FileImportFilesHi3CompositeFundSplitsMenuItem.Click += new System.EventHandler(this.FileImportFilesHi3CompositeFundSplitsMenuItem_Click);
			// 
			// fileExitMenuItem
			// 
			this.fileExitMenuItem.Index = 1;
			this.fileExitMenuItem.Text = "E&xit";
			this.fileExitMenuItem.Click += new System.EventHandler(this.fileExitMenuItem_Click);
			// 
			// viewMenuItem
			// 
			this.viewMenuItem.Index = 1;
			this.viewMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
				{
					this.viewCurrentFundStatusmenuItem,
					this.viewCurrentAssetFundmenuItem,
					this.menuItem1,
					this.companyStatusMenuItem
				});
			this.viewMenuItem.Text = "&View";
			// 
			// viewCurrentFundStatusmenuItem
			// 
			this.viewCurrentFundStatusmenuItem.Index = 0;
			this.viewCurrentFundStatusmenuItem.Text = "Current &Fund Status";
			this.viewCurrentFundStatusmenuItem.Click += new System.EventHandler(this.viewCurrentFundStatusmenuItem_Click);
			// 
			// viewCurrentAssetFundmenuItem
			// 
			this.viewCurrentAssetFundmenuItem.Index = 1;
			this.viewCurrentAssetFundmenuItem.Text = "Current &Asset Fund Status";
			this.viewCurrentAssetFundmenuItem.Click += new System.EventHandler(this.viewCurrentAssetFundmenuItem_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 2;
			this.menuItem1.Text = "-";
			// 
			// companyStatusMenuItem
			// 
			this.companyStatusMenuItem.Index = 3;
			this.companyStatusMenuItem.Text = "Company &Status";
			this.companyStatusMenuItem.Click += new System.EventHandler(this.companyStatusMenuItem_Click);
			// 
			// companyMenuItem
			// 
			this.companyMenuItem.Index = 2;
			this.companyMenuItem.Text = "&Company";
			// 
			// reportsMenu
			// 
			this.reportsMenu.Index = 3;
			this.reportsMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
				{
					this.reportsFundDriftMenu,
					this.reportsPredictedPriceMenu,
					this.PriceComparisionMenu
				});
			this.reportsMenu.Text = "&Reports";
			// 
			// reportsFundDriftMenu
			// 
			this.reportsFundDriftMenu.Index = 0;
			this.reportsFundDriftMenu.Text = "&Fund Drift";
			this.reportsFundDriftMenu.Click += new System.EventHandler(this.reportsFundDriftMenu_Click);
			// 
			// reportsPredictedPriceMenu
			// 
			this.reportsPredictedPriceMenu.Index = 1;
			this.reportsPredictedPriceMenu.Text = "&Historical Prices";
			this.reportsPredictedPriceMenu.Click += new System.EventHandler(this.reportsPredictedPriceMenu_Click);
			// 
			// PriceComparisionMenu
			// 
			this.PriceComparisionMenu.Index = 2;
			this.PriceComparisionMenu.Text = "&Price Comparision";
			this.PriceComparisionMenu.Click += new System.EventHandler(this.PriceComparision_Click);
			// 
			// StaticDataMenuItem
			// 
			this.StaticDataMenuItem.Index = 4;
			this.StaticDataMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
				{
					this.staticDataAssetFundsMenuItem,
					this.staticDataFundMappingMenuItem,
					this.staticDataFundGroupsMenuItem,
					this.PriceFileMenu,
					this.menuItem3,
					this.staticDataCountryMenuItem,
					this.staticDataCurrenciesMenuItem,
					this.staticDataStockMarketMenuItem,
					this.menuItem4,
					this.staticDataUsersMenuItem
				});
			this.StaticDataMenuItem.Text = "&Static Data";
			// 
			// staticDataAssetFundsMenuItem
			// 
			this.staticDataAssetFundsMenuItem.Index = 0;
			this.staticDataAssetFundsMenuItem.Text = "&Asset Funds";
			this.staticDataAssetFundsMenuItem.Click += new System.EventHandler(this.staticDataAssetFundsMenuItem_Click);
			// 
			// staticDataFundMappingMenuItem
			// 
			this.staticDataFundMappingMenuItem.Index = 1;
			this.staticDataFundMappingMenuItem.Text = "&Funds";
			this.staticDataFundMappingMenuItem.Click += new System.EventHandler(this.staticDataFundsMenuItem_Click);
			// 
			// staticDataFundGroupsMenuItem
			// 
			this.staticDataFundGroupsMenuItem.Index = 2;
			this.staticDataFundGroupsMenuItem.Text = "Fund &Groups";
			this.staticDataFundGroupsMenuItem.Click += new System.EventHandler(this.staticDataFundGroupsMenuItem_Click);
			// 
			// PriceFileMenu
			// 
			this.PriceFileMenu.Index = 3;
			this.PriceFileMenu.Text = "&Price Files";
			this.PriceFileMenu.Click += new System.EventHandler(this.PriceFileMenu_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 4;
			this.menuItem3.Text = "-";
			// 
			// staticDataCountryMenuItem
			// 
			this.staticDataCountryMenuItem.Index = 5;
			this.staticDataCountryMenuItem.Text = "C&ountries";
			this.staticDataCountryMenuItem.Click += new System.EventHandler(this.staticDataCountryMenuItem_Click);
			// 
			// staticDataCurrenciesMenuItem
			// 
			this.staticDataCurrenciesMenuItem.Index = 6;
			this.staticDataCurrenciesMenuItem.Text = "&Currencies";
			this.staticDataCurrenciesMenuItem.Click += new System.EventHandler(this.staticDataCurrenciesMenuItem_Click);
			// 
			// staticDataStockMarketMenuItem
			// 
			this.staticDataStockMarketMenuItem.Index = 7;
			this.staticDataStockMarketMenuItem.Text = "&Stock Markets";
			this.staticDataStockMarketMenuItem.Click += new System.EventHandler(this.staticDataStockMarketMenuItem_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 8;
			this.menuItem4.Text = "-";
			// 
			// staticDataUsersMenuItem
			// 
			this.staticDataUsersMenuItem.Index = 9;
			this.staticDataUsersMenuItem.Text = "&Users";
			this.staticDataUsersMenuItem.Click += new System.EventHandler(this.staticDataUsersMenu_Click);
			// 
			// tasksMenuItem
			// 
			this.tasksMenuItem.Index = 5;
			this.tasksMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
				{
					this.secondLevelMenuItem,
					this.releaseMenuItem,
					this.taskMenuSeperator,
					this.distributeMenuItem,
					this.menuItem2,
					this.ValuationDayMenu
				});
			this.tasksMenuItem.Text = "Tas&ks";
			// 
			// secondLevelMenuItem
			// 
			this.secondLevelMenuItem.Index = 0;
			this.secondLevelMenuItem.Text = "Second &Level Authorisation";
			this.secondLevelMenuItem.Click += new System.EventHandler(this.secondLevelMenuItem_Click);
			// 
			// releaseMenuItem
			// 
			this.releaseMenuItem.Index = 1;
			this.releaseMenuItem.Text = "&Release Authorised Prices";
			this.releaseMenuItem.Click += new System.EventHandler(this.releaseMenuItem_Click);
			// 
			// taskMenuSeperator
			// 
			this.taskMenuSeperator.Index = 2;
			this.taskMenuSeperator.Text = "-";
			// 
			// distributeMenuItem
			// 
			this.distributeMenuItem.Index = 3;
			this.distributeMenuItem.Text = "&Distribute";
			this.distributeMenuItem.Click += new System.EventHandler(this.distributeMenuItem_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 4;
			this.menuItem2.Text = "-";
			// 
			// ValuationDayMenu
			// 
			this.ValuationDayMenu.Index = 5;
			this.ValuationDayMenu.Text = "&Start Next Valuation Day";
			this.ValuationDayMenu.Click += new System.EventHandler(this.ValuationDayMenu_Click);
			// 
			// helpMenuItem
			// 
			this.helpMenuItem.Index = 6;
			this.helpMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
				{
					this.helpAboutMenuItem
				});
			this.helpMenuItem.Text = "&Help";
			// 
			// helpAboutMenuItem
			// 
			this.helpAboutMenuItem.Index = 0;
			this.helpAboutMenuItem.Text = "&About";
			this.helpAboutMenuItem.Click += new System.EventHandler(this.menuItemAbout_Click);
			// 
			// mainTitleBar
			// 
			this.mainTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
			this.mainTitleBar.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
			this.mainTitleBar.Location = new System.Drawing.Point(0, 0);
			this.mainTitleBar.Name = "mainTitleBar";
			this.mainTitleBar.Size = new System.Drawing.Size(838, 23);
			this.mainTitleBar.TabIndex = 2;
			this.mainTitleBar.Visible = false;
			// 
			// statusbarUpdateTimer
			// 
			this.statusbarUpdateTimer.Interval = 3000;
			this.statusbarUpdateTimer.Tick += new System.EventHandler(this.onStatusbarUpdateTimerTick);
			// 
			// statusBar
			// 
			this.statusBar.ContextMenu = this.statusBarContextMenu;
			this.statusBar.Location = new System.Drawing.Point(0, 512);
			this.statusBar.Name = "statusBar";
			this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[]
				{
					this.statusBarCompany,
					this.currentValuationPoint,
					this.statusBarDatabase,
					this.refreshPanel
				});
			this.statusBar.ShowPanels = true;
			this.statusBar.Size = new System.Drawing.Size(838, 26);
			this.statusBar.SizingGrip = false;
			this.statusBar.TabIndex = 4;
			// 
			// statusBarContextMenu
			// 
			this.statusBarContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
				{
					this.refreshStatusBar,
					this.mnuDatabase
				});
			// 
			// refreshStatusBar
			// 
			this.refreshStatusBar.Index = 0;
			this.refreshStatusBar.Text = "&Refresh";
			this.refreshStatusBar.Click += new System.EventHandler(this.onStatusbarUpdateTimerTick);
			// 
			// statusBarCompany
			// 
			this.statusBarCompany.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
			this.statusBarCompany.Text = "Company : ";
			this.statusBarCompany.Width = 79;
			// 
			// currentValuationPoint
			// 
			this.currentValuationPoint.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.currentValuationPoint.Text = "Current Valuation Point :";
			this.currentValuationPoint.Width = 648;
			// 
			// statusBarDatabase
			// 
			this.statusBarDatabase.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
			this.statusBarDatabase.Text = "Database : ";
			this.statusBarDatabase.Width = 79;
			// 
			// refreshPanel
			// 
			this.refreshPanel.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
			this.refreshPanel.Icon = ((System.Drawing.Icon) (resources.GetObject("refreshPanel.Icon")));
			this.refreshPanel.MinWidth = 32;
			this.refreshPanel.Width = 32;
			// 
			// displayArea
			// 
			this.displayArea.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
				| System.Windows.Forms.AnchorStyles.Left)
				| System.Windows.Forms.AnchorStyles.Right)));
			this.displayArea.BackColor = System.Drawing.SystemColors.Control;
			this.displayArea.DockPadding.Bottom = 23;
			this.displayArea.ForeColor = System.Drawing.SystemColors.WindowText;
			this.displayArea.Location = new System.Drawing.Point(0, 29);
			this.displayArea.Name = "displayArea";
			this.displayArea.Size = new System.Drawing.Size(845, 486);
			this.displayArea.TabIndex = 5;
			this.displayArea.Text = "page1";
			// 
			// mnuDatabase
			// 
			this.mnuDatabase.Index = 1;
			this.mnuDatabase.Text = "Database";
			// 
			// Main
			// 
			this.AutoScale = false;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
			this.ClientSize = new System.Drawing.Size(838, 538);
			this.Controls.Add(this.displayArea);
			this.Controls.Add(this.statusBar);
			this.Controls.Add(this.mainTitleBar);
			this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu;
			this.MinimumSize = new System.Drawing.Size(680, 501);
			this.Name = "Main";
			this.Text = "Unit Pricing and Distribution";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.Main_Load);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.Form_Layout);
			((System.ComponentModel.ISupportInitialize) (this.statusBarCompany)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.currentValuationPoint)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.statusBarDatabase)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.refreshPanel)).EndInit();
			this.ResumeLayout(false);

		}

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

		#region Event Handlers

		#region Menu Items

		#region File Menu

		/// <summary>
		/// User wnats to import Currency Exchange rates.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FileImportFilesImportCurrencyExchangeRatesMenuItem_Click(object sender, EventArgs e)
		{
			importFile(ImportController.ImportFileType.CurrencyExchangeRate, (string) GlobalRegistry.AppSettings.ImportFileExtensionFilterDetails.GetFilters()["ImportCurrencyExchangeRateFilter"]);
		}

		/// <summary>
		/// User is trying to Import Stock Market Indices.
		/// </summary>
		/// <param name="sender">Import menu item</param>
		/// <param name="e">n/a</param>
		private void FileImportFilesImportStockMarketIndicesMenuItem_Click(object sender, EventArgs e)
		{
			importFile(ImportController.ImportFileType.StockMarketIndices, (string)GlobalRegistry.AppSettings.ImportFileExtensionFilterDetails.GetFilters()["ImportMarketIndicesFilter"]);
		}

		/// <summary>
		/// user wants to import Hi3 Prices
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FileImportFilesHi3PricesMenuItem_Click(object sender, EventArgs e)
		{
			importFile(ImportController.ImportFileType.Hi3Prices, (string) GlobalRegistry.AppSettings.ImportFileExtensionFilterDetails.GetFilters()["ImportHi3PricesFilter"]);
		}

		private void FileImportFilesHi3PricesCompositeMenuItem_Click(object sender, EventArgs e)
		{
			importFile(ImportController.ImportFileType.Hi3PricesComposite, (string) GlobalRegistry.AppSettings.ImportFileExtensionFilterDetails.GetFilters()["ImportHi3CompositePricesFilter"]);
		}

		/// <summary>
		/// user wants to import Hi3 Linked Fund Prices
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FileImportFilesHi3PricesLinkedMenuItem_Click(object sender, EventArgs e)
		{
			importFile(ImportController.ImportFileType.Hi3PricesLinked, (string) GlobalRegistry.AppSettings.ImportFileExtensionFilterDetails.GetFilters()["ImportHi3LinkedPricesFilter"]);
		}

		/// <summary>
		/// User wants to import Hi3 Asset fund splits.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FileImportFilesHi3AssetFundSplitsMenuItem_Click(object sender, EventArgs e)
		{
			importFile(ImportController.ImportFileType.Hi3AssetFundSplits, (string) GlobalRegistry.AppSettings.ImportFileExtensionFilterDetails.GetFilters()["ImportHi3AssetFundSplits"]);
		}

		private void FileImportFilesHi3CompositeFundSplitsMenuItem_Click(object sender, EventArgs e)
		{
			importFile(ImportController.ImportFileType.Hi3CompositeFundSplits, (string) GlobalRegistry.AppSettings.ImportFileExtensionFilterDetails.GetFilters()["ImportHi3CompositeFundSplits"]);
		}

		/// <summary>
		/// Cleanly exit the application
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void fileExitMenuItem_Click(object sender, EventArgs e)
		{
			//
			// TODO: Do we add NUnit and tracing here as it is called in ExitApplication()?
			//
			T.E();
			if (allowMenuChange())
			{
				UPDApplication.ExitApplication();
			}
			T.X();
		}

		#endregion

		#region View Menu

		private void viewCurrentFundStatusmenuItem_Click(object sender, EventArgs e)
		{
			IStatusViewBuilder builder = new CurrentFundStatusBuilder();
			loadUserControl(builder, "Current Fund Status", false);
		}

		private void viewCurrentAssetFundmenuItem_Click(object sender, EventArgs e)
		{
			IStatusViewBuilder builder = new AssetFundStatusBuilder();
			loadUserControl(builder, "Asset Fund Status", false);
		}

		private void companyStatusMenuItem_Click(object sender, EventArgs e)
		{
			loadCompanyStatus();
		}

		#endregion

		#region Company Menu

		private void menuCompany_Click(object sender, EventArgs e)
		{
			T.E("Company Change Started");

			Cursor oldCursor = this.Cursor;
			try
			{
				if (allowMenuChange())
				{
					this.Cursor = Cursors.WaitCursor;
					if (!((MenuItem) sender).Checked)
					{
						string menuCompany = ((MenuItem) sender).Text.Replace("&", "");
						CompanyDetails newCompanyCode;

						//	CompanyController companyController = new CompanyController();
						newCompanyCode = CompanyController.LoadCompanyCodeByName(GlobalRegistry.ConnectionString, menuCompany);

						if (null == newCompanyCode)
						{
							MessageBoxHelper.Show("CannotFindCompanyBody", "CannotFindCompanyTitle", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
						else
						{
							UPDIdentity currentID = (UPDIdentity) Thread.CurrentPrincipal.Identity;
							UPDPrincipal newPrincipal = new UPDPrincipal(currentID, newCompanyCode.CompanyCode,
							                                             newCompanyCode.CompanyValuationDate, newCompanyCode.NextValuationDate,
							                                             newCompanyCode.PreviousValuationDate);
							Thread.CurrentPrincipal = newPrincipal;

							UserController.SetLastCompany(GlobalRegistry.ConnectionString, newCompanyCode.CompanyCode);

							// And fire off a reload to get the permissions sorted.
							this.changeCompany = true;
							this.Close();
						}
					}
				}
			}
			catch (Exception anyException)
			{
				this.Cursor = oldCursor;
				T.DumpException(anyException);
				ExceptionManager.Publish(anyException);
				ErrorDialog.Show(anyException);
			}
			finally
			{
				this.Cursor = oldCursor;
				T.E("Company Change Finished");
			}
		}

		#endregion

		#region Reports Menu

		private void reportsFundDriftMenu_Click(object sender, EventArgs e)
		{
			ReportControlFactory aUserControl = new ReportControlFactory();
			loadUserControl(aUserControl.LoadControl(ReportControlFactory.ControlTypeEnum.FundDrift), "Fund Drift Report");
		}

		private void reportsPredictedPriceMenu_Click(object sender, EventArgs e)
		{
			ReportControlFactory aUserControl = new ReportControlFactory();
			loadUserControl(aUserControl.LoadControl(ReportControlFactory.ControlTypeEnum.Historic), "Historical Prices Report");
		}

		private void PriceComparision_Click(object sender, EventArgs e)
		{
			ReportControlFactory aUserControl = new ReportControlFactory();
			loadUserControl(aUserControl.LoadControl(ReportControlFactory.ControlTypeEnum.PriceComparision), "Price Comparision Report");
		}

		#endregion

		#region Static Data Menu

		private void staticDataAssetFundsMenuItem_Click(object sender, EventArgs e)
		{
			UserControl control = createStaticDataView(new AssetFundStaticDataBuilder());
			loadUserControl(control, "Asset Fund Maintenance");
		}

		private void staticDataFundsMenuItem_Click(object sender, EventArgs e)
		{
			UserControl control = createStaticDataView(new FundStaticDataBuilder());
			loadUserControl(control, "Fund Maintenance");
		}

		private void staticDataFundGroupsMenuItem_Click(object sender, EventArgs e)
		{
			UserControl control = createStaticDataView(new FundGroupStaticDataBuilder());
			loadUserControl(control, "Fund Group Maintenance");
		}

		private void PriceFileMenu_Click(object sender, EventArgs e)
		{
			UserControl control = createStaticDataView(new PriceFileStaticDataBuilder());
			loadUserControl(control, "Price File Maintenance");
		}

		private void staticDataCountryMenuItem_Click(object sender, EventArgs e)
		{
			UserControl control = createStaticDataView(new CountryStaticDataBuilder());
			loadUserControl(control, "Country Maintenance");
		}

		private void staticDataCurrenciesMenuItem_Click(object sender, EventArgs e)
		{
			UserControl control = createStaticDataView(new CurrencyStaticDataBuilder());

			loadUserControl(control, "Currency Maintenance");
		}

		private void staticDataStockMarketMenuItem_Click(object sender, EventArgs e)
		{
			UserControl control = createStaticDataView(new StockMarketStaticDataBuilder());
			loadUserControl(control, "Stock Market Maintenance");
		}

		private void staticDataUsersMenu_Click(object sender, EventArgs e)
		{
			UserControl control = createStaticDataView(new UserStaticDataBuilder());
			loadUserControl(control, "User Maintenance");
		}

		#endregion

		#region Tasks Menu

		/// <summary>
		/// Show the second level authorisation screen.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Not used.</param>
		private void secondLevelMenuItem_Click(object sender, EventArgs e)
		{
			IStatusViewBuilder builder = new SecondLevelAuthorisationBuilder();
			loadUserControl(builder, "Second Level Authorisation", true);
		}

		/// <summary>
		/// User wants to release some prices
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void releaseMenuItem_Click(object sender, EventArgs e)
		{
			IStatusViewBuilder builder = new ReleaseFundViewBuilder();
			loadUserControl(builder, "Release Authorised Prices", false);
		}

		private void distributeMenuItem_Click(object sender, EventArgs e)
		{
			this.fileDistributeMenuItem_Click(sender, e);
		}

		private void fileDistributeMenuItem_Click(object sender, EventArgs e)
		{
			T.E();

			try
			{
				if (allowMenuChange())
				{
					// Display the distribute price data screen
					DistributePriceData distributePrices = new DistributePriceData();

					loadUserControl(distributePrices, "Distribute Price Data");
				}
			}
			catch (Exception exception)
			{
				T.DumpException(exception);
				ExceptionManager.Publish(exception);
				ErrorDialog.Show(exception);
			}
			finally
			{
				T.X();
			}
		}

		private void ValuationDayMenu_Click(object sender, EventArgs e)
		{
			ProgressValuationDay();
		}

		#endregion

		#region Help Menu

		private void menuItemAbout_Click(object sender, EventArgs e)
		{
			T.E();
			Cursor oldCursor = this.Cursor;
			this.Cursor = Cursors.WaitCursor;
			try
			{
				About aboutForm = new About();
				this.Cursor = oldCursor;
				aboutForm.ShowDialog();
			}
			catch
			{
				throw;
			}
			finally
			{
				this.Cursor = oldCursor;
				T.X();
			}
		}

		#endregion

		#endregion

		/// <summary>
		/// React on a click from the statusbarUpdateTimer.
		/// </summary>
		/// <param name="sender">Sending object</param>
		/// <param name="e">Event arguements</param>
		private void onStatusbarUpdateTimerTick(object sender, EventArgs e)
		{
			T.E();
			updateStatusBar();


			T.X();
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Load a user control into the display area panel and set attributes.
		/// The User control should implement ICustomInit which has the method CustomInitialization().
		/// In case any user control does not implement the interface, this method has the IF check
		/// so the appln does not break.
		/// </summary>
		/// <param name="controlToLoad">Control object to be loaded</param>
		/// <param name="titleBarCaption">Text to display within the title bar</param>
		private void loadUserControl(UserControl controlToLoad, string titleBarCaption)
		{
			T.E();

			Cursor oldCursor = this.Cursor;
			try
			{
				this.Cursor = Cursors.WaitCursor;
				if (controlToLoad != null)
				{
					if (allowMenuChange())
					{
						// Clear the display area of any controls
						removeCurrentControl(false);

						this.mainTitleBar.Text = titleBarCaption;

						// Anchor the control to the top left corner of the form
						controlToLoad.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right);

						// Set the dock style

						controlToLoad.Dock = DockStyle.Fill;

						controlToLoad.Parent = this;

						// Add control into display area and display
						this.displayArea.Controls.Add(controlToLoad);

						MethodInfo mi = controlToLoad.GetType().GetMethod("CustomInitialization");
						if (mi != null)
						{
							mi.Invoke(controlToLoad, null);
						}
					}
				}
			}
			catch (Exception ex)
			{
				T.DumpException(ex);
				ExceptionManager.Publish(ex);
				ErrorDialog.Show(ex);
			}
			finally
			{
				this.Cursor = oldCursor;
				T.X();
			}
		}

		/// <summary>
		/// Load a user control into the display area panel and set attributes.
		/// The User control should implement ICustomInit which has the method CustomInitialization().
		/// In case any user control does not implement the interface, this method has the IF check
		/// so the appln does not break.
		/// </summary>
		private void loadUserControl(IStatusViewBuilder inBuilder, string titleBarCaption, bool updateable)
		{
			T.E();

			Cursor oldCursor = this.Cursor;
			try
			{
				this.Cursor = Cursors.WaitCursor;

				UserControl lclControlToLoad;

				if (!updateable)
					lclControlToLoad = StatusViewFactory.CreateView(inBuilder);
				else
					lclControlToLoad = UpdateableStatusViewFactory.CreateView(inBuilder);

				loadUserControl(lclControlToLoad, titleBarCaption);
			}
			catch (Exception ex)
			{
				T.DumpException(ex);
				ExceptionManager.Publish(ex);
				ErrorDialog.Show(ex);
			}
			finally
			{
				this.Cursor = oldCursor;
				T.X();
			}
		}

		private void loadCompanyStatus()
		{
			T.E();

			try
			{
				if (allowMenuChange())
				{
					// Add desired control into the display area panel
					CompanyStatus companyStatusControl = new CompanyStatus();
					companyStatusControl.ProgressValuationDay += new CompanyStatus.ProgressValuationDayDelegate(this.ProgressValuationDay);

					this.loadUserControl(companyStatusControl, "Company Status");
				}
			}
			catch (Exception anyException)
			{
				T.DumpException(anyException);
				ExceptionManager.Publish(anyException);
				ErrorDialog.Show(anyException);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Remove the current control from the main display area
		/// </summary>
		/// <param name="hideTitleBar">Hide the main title bar</param>
		private void removeCurrentControl(bool hideTitleBar)
		{
			T.E();
			try
			{
				this.displayArea.Controls.Clear();
				this.mainTitleBar.Visible = !hideTitleBar;

				// Give applciation time to redraw
				Application.DoEvents();
			}
			finally
			{
				T.X();
			}
		}

		private void importFile(ImportController.ImportFileType importFileType, string filter)
		{
			T.E();
			Cursor oldCursor = this.Cursor;
			try
			{
				if (allowMenuChange())
				{
					string importFile = ImportManager.ChooseImportFile(filter, this);

					if (importFile != null && importFile.Length != 0)
					{
						ImportManager.ImportFile(importFileType, importFile, true, this);
					}
				}
			}
			catch (Exception Ex)
			{
				T.DumpException(Ex);
				ExceptionManager.Publish(Ex);
				ErrorDialog.Show(Ex);
			}
			finally
			{
				this.Cursor = oldCursor;
				T.X();
			}
			if (this.displayArea.Controls.Count == 0) loadCompanyStatus();
		}

		/// <summary>
		/// Creates the static data view using the provided builder.
		/// </summary>
		/// <param name="builder">Builder.</param>
		/// <returns></returns>
		private StaticDataFrame createStaticDataView(IStaticDataBuilder builder)
		{
			T.E();
			StaticDataFrame frame = new StaticDataFrame();
			builder.ConfigureFrame(frame);
			StaticDataEditor editor = builder.GetEntityEditor();
			StaticDataListManager listManager = new StaticDataListManager();

			editor.Parent = frame;
			frame.Body = editor;
			frame.Body.Dock = DockStyle.Fill;

			frame.ListManager = listManager;
			editor.ListManager = listManager;
			builder.ConfigureListManager(listManager);

			frame.Actions = builder.GetActions();
			if (listManager.Items.Count > 0) listManager.SelectedIndex = 0;
			T.X();
			return frame;
		}

		private bool allowMenuChange()
		{
			if (this.displayArea.Controls.Count > 0)
			{
				IUPDControl updControl = this.displayArea.Controls[0] as IUPDControl;
				if (updControl != null)
				{
					return updControl.AllowMenuChange();
				}
			}
			return true;
		}


		private void Form_Layout(object sender, LayoutEventArgs e)
		{
			if (m_bLayoutCalled == false)
			{
				m_bLayoutCalled = true;
				if (SplashScreen.SplashForm != null)
					SplashScreen.SplashForm.Owner = this;
				this.Activate();
				SplashScreen.CloseForm();
				T.Log("Load Company Status");
				loadCompanyStatus();

			}
		}


		private void Main_Load(object sender, EventArgs e)
		{
			UPDApplication.SetForegroundWindow(Handle);	
		}


		/// <summary>
		/// Checks the current status.
		/// </summary>
		/// <param name="currentStatus">Current status.</param>
		public void CheckCurrentStatus(CurrentImportStatus currentStatus)
		{
			if (currentStatus != null)
			{
				// Update the current valuation point status panel.

				currentValuationPoint.Text = "Current Valuation Point : " + new DateTime(
					currentStatus.CurrentValuationDay.Year,
					currentStatus.CurrentValuationDay.Month,
					currentStatus.CurrentValuationDay.Day,
					currentStatus.CurrentValuationPoint.Hour,
					currentStatus.CurrentValuationPoint.Minute,
					0).ToString();
				currentValuationPoint.Icon = null;

				if (null == this.previousImportStatus) // First time around, so there's no comparison needed.
					previousImportStatus = currentStatus;
				else
				{
					if (previousImportStatus.CurrentValuationDay != currentStatus.CurrentValuationDay ||
						previousImportStatus.CurrentValuationPoint != currentStatus.CurrentValuationPoint ||
						previousImportStatus.AssetFundMarketSplits.ImportDateTime != currentStatus.AssetFundMarketSplits.ImportDateTime ||
						previousImportStatus.CurrencyRates.ImportDateTime != currentStatus.CurrencyRates.ImportDateTime ||
						previousImportStatus.FundPrices.ImportDateTime != currentStatus.FundPrices.ImportDateTime ||
						previousImportStatus.MarketIndexValues.ImportDateTime != currentStatus.MarketIndexValues.ImportDateTime)
					{
						T.Log("Current valuation / import points have changed");
						// Something changed, do we need to alert the user?
						foreach (Control control in this.displayArea.Controls)
						{
							if (control is IRefreshable)
							{
								T.Log("Current screen merits reload");
								StringBuilder messageBoxContents = new StringBuilder();
								int numberOfChangedFiles = 0;
								//	int insertFileUpdateMessageAt = 0;

								bool updatePinciple = false;
								if (previousImportStatus.CurrentValuationDay != currentStatus.CurrentValuationDay)
								{
									messageBoxContents.AppendFormat(CultureInfo.CurrentUICulture,
									                                MessageBoxHelper.DialogText("CurrentValuationDayChanged"),
									                                previousImportStatus.CurrentValuationDay.ToShortDateString(),
									                                currentStatus.CurrentValuationDay.ToShortDateString());
									updatePinciple = true;
								}

								if (previousImportStatus.CurrentValuationPoint != currentStatus.CurrentValuationPoint)
								{
									messageBoxContents.AppendFormat(CultureInfo.CurrentUICulture,
									                                MessageBoxHelper.DialogText("CurrentValuationPointChanged"),
									                                previousImportStatus.CurrentValuationPoint.ToShortTimeString(),
									                                currentStatus.CurrentValuationPoint.ToShortTimeString());
									updatePinciple = true;
								}

								if ((previousImportStatus.AssetFundMarketSplits == null && currentStatus.AssetFundMarketSplits != null) ||
									(previousImportStatus.AssetFundMarketSplits != null &&
										currentStatus.AssetFundMarketSplits != null &&
										previousImportStatus.AssetFundMarketSplits.ImportDateTime != currentStatus.AssetFundMarketSplits.ImportDateTime &&
										currentStatus.AssetFundMarketSplits.ImportedByAccount != Thread.CurrentPrincipal.Identity.Name))
								{
									if (currentStatus.AssetFundMarketSplits.ImportFileName == "S")
										messageBoxContents.AppendFormat(CultureInfo.CurrentUICulture,
										                                MessageBoxHelper.DialogText("AssetFundChangedGUI"),
										                                currentStatus.AssetFundMarketSplits.ImportedByName,
										                                currentStatus.AssetFundMarketSplits.ImportDateTime);
									else
										messageBoxContents.AppendFormat(CultureInfo.CurrentUICulture,
										                                MessageBoxHelper.DialogText("NewFileImported"),
										                                "split benchmark",
										                                currentStatus.AssetFundMarketSplits.ImportedByName,
										                                currentStatus.AssetFundMarketSplits.ImportDateTime);
									numberOfChangedFiles++;
								}

								if ((previousImportStatus.CurrencyRates == null &&
									currentStatus.CurrencyRates != null)
									||
									(previousImportStatus.CurrencyRates != null &&
										currentStatus.CurrencyRates != null &&
										previousImportStatus.CurrencyRates.ImportDateTime != currentStatus.CurrencyRates.ImportDateTime))
								{
									messageBoxContents.AppendFormat(CultureInfo.CurrentUICulture,
									                                MessageBoxHelper.DialogText("NewFileImported"),
									                                "currency rate",
									                                currentStatus.CurrencyRates.ImportedByName,
									                                currentStatus.CurrencyRates.ImportDateTime);
									numberOfChangedFiles++;
								}

								if ((previousImportStatus.FundPrices == null &&
									currentStatus.FundPrices != null)
									||
									(previousImportStatus.FundPrices != null &&
										currentStatus.FundPrices != null &&
										previousImportStatus.FundPrices.ImportDateTime != currentStatus.FundPrices.ImportDateTime))
								{
									messageBoxContents.AppendFormat(CultureInfo.CurrentUICulture,
									                                MessageBoxHelper.DialogText("NewFileImported"),
									                                "fund price",
									                                currentStatus.FundPrices.ImportedByName,
									                                currentStatus.FundPrices.ImportDateTime);
									numberOfChangedFiles++;
								}

								if ((previousImportStatus.MarketIndexValues == null &&
									currentStatus.MarketIndexValues != null)
									||
									(previousImportStatus.MarketIndexValues != null &&
										currentStatus.MarketIndexValues != null &&
										previousImportStatus.MarketIndexValues.ImportDateTime != currentStatus.MarketIndexValues.ImportDateTime))
								{
									messageBoxContents.AppendFormat(CultureInfo.CurrentUICulture,
									                                MessageBoxHelper.DialogText("NewFileImported"),
									                                "market index values",
									                                currentStatus.MarketIndexValues.ImportedByName,
									                                currentStatus.MarketIndexValues.ImportDateTime);
									numberOfChangedFiles++;
								}

								// Warn the user what happened.
								//	Assembly myself = Assembly.GetExecutingAssembly();
								//	string[] resources = myself.GetManifestResourceNames();
								// Put a cute icon in the right panel in the status bar.
								//currentValuationPoint.Icon = new Icon(myself.GetManifestResourceStream("HBOS.FS.AMP.UPD.WinUI.Icons.stop.ico"),16,16);

								// Now alert the user to what's going on.
								MessageBoxHelper.Show(messageBoxContents.ToString(), "UnderlyingDataChanged", MessageBoxButtons.OK, MessageBoxIcon.Information);
								//MessageBox.Show(messageBoxContents.ToString(), "Underlying data changed", MessageBoxButtons.OK, MessageBoxIcon.Stop);

								// Now close down the screen.
								//								if (control.GetType().ToString() == "HBOS.FS.AMP.UPD.WinUI.UserControls.StatusView")
								//								{
								((IRefreshable) control).OnRefreshData();
								//								}
								//                            if (control.GetType().ToString() == "HBOS.FS.AMP.UPD.WinUI.UserControls.CurrentFundStatus")
								//                                ((UserControls.CurrentFundStatus)control).RefreshData();

								if (updatePinciple)
								{
									CompanyDetails newCompany = CompanyController.LoadCompanyByCode(GlobalRegistry.ConnectionString, ((UPDPrincipal) Thread.CurrentPrincipal).CompanyCode);

									if (null == newCompany)
									{
										MessageBoxHelper.Show("CannotFindCompanyBody", "CannotFindCompanyTitle", MessageBoxButtons.OK, MessageBoxIcon.Error);
									}
									else
									{
										UPDIdentity currentID = (UPDIdentity) Thread.CurrentPrincipal.Identity;
										UPDPrincipal newPrincipal = new UPDPrincipal(currentID, newCompany.CompanyCode,
										                                             newCompany.CompanyValuationDate, newCompany.NextValuationDate,
										                                             newCompany.PreviousValuationDate);
										Thread.CurrentPrincipal = newPrincipal;
									}
								}

							}

						}
					}
					previousImportStatus = currentStatus;
				}
			}

			// And finally remove the refreshing icon.
			refreshPanel.Icon = null;
			refreshPanel.ToolTipText = null;
			Application.DoEvents();

		}

		/// <summary>
		/// Updates the current validation point on the status bar, displaying a warning dialog
		/// if there has been a change and the user is on a screen which the change impacts.
		/// </summary>
		private void updateStatusBar()
		{
			// Turn the time off before we attempt to update the status bar, otherwise if it takes longer
			// than the time interval this event gets thrown again, and we get into a lovely race condition.
			this.statusbarUpdateTimer.Stop();


			//TODO: reinstate or remove status bar stuff (and poss run asynchronously)
			T.E();

			// Refresh the current valuation point            
			refreshPanel.Icon = this.refreshIcon;
			refreshPanel.ToolTipText = "Checking current status";
			Application.DoEvents();

			StatusBarDelegate statusBarDelegate = new StatusBarDelegate(CurrentStatusController.LoadCurrentStatus);

			//AsyncCallback cb = new AsyncCallback(this.CheckCurrentStatus);

			IAsyncResult ar = statusBarDelegate.BeginInvoke(
				GlobalRegistry.ConnectionString,
				((UPDPrincipal) Thread.CurrentPrincipal).CompanyCode, null, null);


			// Poll while simulating work.
			while (ar.IsCompleted == false)
			{
				Application.DoEvents();
			}

			// Call EndInvoke to retrieve the results.
			CurrentImportStatus currentStatus = statusBarDelegate.EndInvoke(ar);
			this.CheckCurrentStatus(currentStatus);

			//			// Get the current status
			//			//CurrentStatusController statusController = new CurrentStatusController();
			//			CurrentImportStatus currentStatus = CurrentStatusController.LoadCurrentStatus(
			//				ConfigurationSettings.AppSettings["connectionString"],
			//				((UPDPrincipal) Thread.CurrentPrincipal).CompanyCode);


			this.statusbarUpdateTimer.Start();
			T.E();

		}

		private void pauseStatusBarTimer()
		{
			statusbarUpdateTimer.Stop();
		}

		private void ProgressValuationDay()
		{
			this.pauseStatusBarTimer();

			string messageBody = MessageBoxHelper.DialogText("CloseValuationDayBody", new object[] {GlobalRegistry.CurrentCompanyValuationDateAndTime.ToShortDateString(), GlobalRegistry.NextCompanyValuationDateAndTime.ToShortDateString()});
			DialogResult result = MessageBoxHelper.Show(messageBody, "CloseValuationDayTitle", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result == DialogResult.Yes)
			{
				try
				{
					CompanyController.ProgressCompanyValuationDay(GlobalRegistry.ConnectionString, GlobalRegistry.CompanyCode);
					CompanyDetails newCompany = CompanyController.LoadCompanyByCode(GlobalRegistry.ConnectionString, GlobalRegistry.CompanyCode);

					if (null == newCompany)
					{
						MessageBoxHelper.Show("CannotFindCompanyBody", "CannotFindCompanyTitle", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else
					{
						UPDIdentity currentID = (UPDIdentity) Thread.CurrentPrincipal.Identity;
						UPDPrincipal newPrincipal = new UPDPrincipal(currentID, newCompany.CompanyCode,
						                                             newCompany.CompanyValuationDate, newCompany.NextValuationDate,
						                                             newCompany.PreviousValuationDate);
						Thread.CurrentPrincipal = newPrincipal;
					}
				}
				catch (ConstraintViolationException ex)
				{
					GUIExceptionHelper.LogAndDisplayException("ProgressValuationDayConstraintViolationBody", "ProgressValuationDayConstraintViolationTitle", ex, new string[] {GlobalRegistry.CompanyCode});
				}
				finally
				{
					this.updateStatusBar();
				}

			}
			else
			{
				this.updateStatusBar();
			}
		}

		#endregion

		/// <summary>
		/// Populates the database menu.
		/// </summary>
		public void PopulateDatabaseMenu()
		{
			mnuDatabase.MenuItems.Clear();
			SortedList ConnectionStrings = GlobalRegistry.AppSettings.DatabaseSettings.GetConnectionStrings();
			foreach (DictionaryEntry connectionstring in ConnectionStrings)
			{
				MenuItem menuitem = mnuDatabase.MenuItems.Add(connectionstring.Key.ToString(), new EventHandler(DatabaseMenu_Checked));
				menuitem.Checked = (menuitem.Text == GlobalRegistry.AppSettings.DatabaseSettings.ActiveConnectionString);
			}
		}

		private void DatabaseMenu_Checked(object sender, EventArgs e)
		{
			//preserve old settings
			ApplicationSettings currentAppSettings = GlobalRegistry.AppSettings;

//			//get a new settings object and alter the Active connection
//			ApplicationSettings newAppSettings = (ApplicationSettings) currentAppSettings.Clone();
//			newAppSettings.DatabaseSettings.ActiveConnectionString = ((MenuItem) sender).Text;
			GlobalRegistry.ActiveConnection = ((MenuItem) sender).Text;;

			//perform checks to see if this connection string is allowed
			if (UPDApplication.PerformChecks(false))
			{
				//everythings good so swap connections
				GlobalRegistry.PersistUserSettings();
			}
			else
			{
				//problem swaping connections so revert back
				GlobalRegistry.AppSettings = currentAppSettings;
			}
			PopulateDatabaseMenu();
		}
	}
}