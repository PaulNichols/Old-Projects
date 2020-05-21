using System;
using System.Configuration;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Text;

using HBOS.FS.Common.ExceptionManagement;
using HBOS.FS.AMP.ExceptionManagement;
using HBOS.FS.AMP.Windows.Controls;
using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.Security;
using HBOS.FS.AMP.UPD.Types.Funds;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.FundGroups;
using HBOS.FS.AMP.UPD.WinUI.Classes;

using HBOS.FS.Support.Tex;


namespace HBOS.FS.AMP.UPD.WinUI.UserControls
{
    /// <summary>
    /// Summary description for CurrentFundStatus.
    /// </summary>
	public class CurrentFundStatus : System.Windows.Forms.UserControl
	{
		#region Locals

		private FundCollection m_funds = new FundCollection();
		private FundGroupCollection m_fundGroups = new FundGroupCollection();
		//private AssetFundCollection m_assetFunds = new AssetFundCollection();
		private AssetFundCollection m_assetFunds = new AssetFundCollection();
		private FundCollection m_fundsFiltered = new FundCollection();	//Required to display properties
		private bool m_userEditing = false;
		private BooleanFilterCollection m_filters = new BooleanFilterCollection();

		#endregion 

		#region Controls
		private HBOS.FS.AMP.Windows.Controls.DataGrid fundsGrid;
		private System.Windows.Forms.Button printButton;
		private System.Windows.Forms.Button exportButton;
		private System.Windows.Forms.Button propertiesButton;
		private System.Windows.Forms.Button refreshButton;
		private System.Windows.Forms.Label noDataLabel;
		private System.Windows.Forms.CheckBox applyFilterCheckBox;
		private System.Windows.Forms.Label assetFundLabel;
		private System.Windows.Forms.Label fundGroupLabel;
		private System.Windows.Forms.ComboBox assetFundComboBox;
		private System.Windows.Forms.ComboBox fundGroupComboBox;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckedListBox filterListBox;
		private System.Windows.Forms.Label rowCountLabel;
		#endregion

		#region Dispose

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
            T.E();
			base.Dispose( disposing );
            T.X();
		}
		#endregion

		#region Constructor
		/// <summary>
		/// Creates a new <see cref="CurrentFundStatus"/> instance.
		/// </summary>
		public CurrentFundStatus()
		{
			T.E();
            // This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			//set everything up and load the data
			loadForm();
            
            // Resize the filter box column width; Normal scale width = 5.32.
            float scaleWidth = Form.GetAutoScaleSize(this.filterListBox.Font).Width;
            float adjustment = scaleWidth / 5.32F;

            this.filterListBox.ColumnWidth = (int)((float)this.filterListBox.ColumnWidth * adjustment);
            T.X();
		}

		#endregion
        
		#region Private methods

		/// <summary>
		/// Load default data and settings for the form
		/// </summary>
		private void loadForm()
		{
            T.E();
            populateFilters();			// Populate the items in the filter box
			createGridStyles();         // Set the column styles for the funds grid
			RefreshData();				// Refresh the funds data and the grid
            T.X();
		}

		private void populateFilters()
		{
			T.E();
            m_filters.Clear();
			m_filters.Add(new BooleanFilter(false, "1st level authorised"));
			m_filters.Add(new BooleanFilter(false, "2nd level authorised"));
			m_filters.Add(new BooleanFilter(false, "Not 2nd level authorised"));
			m_filters.Add(new BooleanFilter(false, "Not 1st or 2nd level authorised"));
			m_filters.Add(new BooleanFilter(false, "2nd, but not 1st level authorised"));
			m_filters.Add(new BooleanFilter(false, "Released"));
			m_filters.Add(new BooleanFilter(false, "Distributed"));
			m_filters.Add(new BooleanFilter(false, "AM outside tolerance"));
			m_filters.Add(new BooleanFilter(false, "Price outside tolerance"));
			
			//Add all the filters to the displayer
			filterListBox.BeginUpdate();
            filterListBox.Items.Clear();
			foreach (BooleanFilter bf in m_filters)
			{
				filterListBox.Items.Add(bf);
			}
            filterListBox.EndUpdate();
            T.X();

		}

		/// <summary>
		/// Refresh all the data that will be displayed on this control
		/// </summary>
		public void RefreshData()
		{
			T.E();
            Cursor oldCursor = Cursor;
			Cursor = Cursors.WaitCursor;

			//DateTime start = System.DateTime.Now;

			//Stop any GUI events affecting data whilst were setting up
			m_userEditing = false;
			
			// Get the currently selected company from the GUI principal thread
			string companyCode = ((UPDPrincipal)System.Threading.Thread.CurrentPrincipal).CompanyCode;

			//Get the connection string
			string connectionString = ConfigurationSettings.AppSettings["ConnectionString"];

			T.Log("Load funds for current company");
            //Load all the funds for the currently selected company

            //
            // TODO: Figure out the correct fund type object that needs lading
            //
			//FundController fController = new FundController(connectionString);
			//m_funds = fController.LoadForCompany(companyCode);

            //Load the asset funds for the currently selected company
            T.Log("Load asset funds for currency company");
            AssetFundController afController = new AssetFundController();
			//m_assetFunds = afController.LoadAssetFunds(connectionString, companyCode, false);
			
			//TODO - this method has now been deprecated and needs to be replaced
			//with something that returns lookups. 
			//This status screen then needs to deal with the lookups
			//m_assetFunds = afController.LoadAssetFundsLight(connectionString,companyCode);
			
			//Load the Fund Groups for the currently selected company
			T.Log("Load asset fund groups for current company");
            FundGroupController fgController = new FundGroupController(connectionString);
			//m_fundGroups = fgController.LoadFundGroupsByCompany(companyCode);

			CacheHelper cacheHelper = new CacheHelper();
			m_fundGroups = cacheHelper.FundGroups;

			//Add a dummy fund group representing all fund groups for the selected company
			T.Log("Create dummy \"all\" fund group");
            FundGroup fg = FundGroupFactory.CreateFundGroup(FundGroupFactory.FundGroupTypes.Individual);
			fg.FullName = string.Format("All Fund Groups for {0}", companyCode);
			m_fundGroups.Insert(0, fg);

			//Bind the fund group to the combo box
			T.Log("Bind fund group collection to fundGroupComboBox");
            fundGroupComboBox.BeginUpdate();
            fundGroupComboBox.DisplayMember = "FullName";
			fundGroupComboBox.ValueMember = "ID";
			fundGroupComboBox.DataSource = m_fundGroups;
            fundGroupComboBox.EndUpdate();

			//Bind the asset funds to the combo box based on the current fund group
			loadAssetFundCombo();			

			//Refresh the grid
			refreshDataGrid();

			//Reset flag so user triggered GUI events will affect data again
			m_userEditing = true;
			
			Cursor = oldCursor;

			//DateTime end = DateTime.Now;
			//MessageBox.Show(string.Format("Time taken: {0}", end - start));
		}

		/// <summary>
		/// Refresh the fund data grid with fund data for the selected asset fund
		/// </summary>
		private void refreshDataGrid()
		{          
			T.E();
            // Apply filter to funds collection
			buildFilteredCollection();

			if (m_fundsFiltered.Count > 0)
			{
				//Bind collection to the grid
                fundsGrid.BindToCustomCollection(m_fundsFiltered); // , typeof(Fund)
				propertiesButton.Enabled = true;
				fundsGrid.Visible = true;
			}
			else
			{
				//hide grid
				fundsGrid.DataSource = null;
				fundsGrid.Visible = false;
				propertiesButton.Enabled = false;
			}   
			rowCountLabel.Text = String.Format("Funds shown: {0} of {1}", m_fundsFiltered.Count, m_funds.Count);
            T.X();
		}

        /// <summary>
        /// Populate the asset fund combo using the select fund group as a filter
        /// </summary>
        private void loadAssetFundCombo()
        {
            T.E();
            if (fundGroupComboBox.SelectedItem is FundGroup)
			{				
				//Get a clone of the full list of asset funds to provide the datasource for the combo
				//AssetFundCollection assetFunds = m_assetFunds.Clone();
				AssetFundCollection assetFunds = m_assetFunds.Clone();
				
				//Get the selected fund group ID
				int fundGroupID = ((FundGroup)fundGroupComboBox.SelectedItem).ID;

				if (fundGroupID != 0)
				{
					//Filter asset funds to display only those in the current fund group
					for (int i = m_assetFunds.Count - 1; i >= 0; i--)
					{
						//if (m_assetFunds[i].FundGroupID != fundGroupID)
						/* TODO - Fund Group ID to be replaced with Fund Group collection (now a m:m)
						if (m_assetFunds[i].FundGroupId != fundGroupID)
						{
							assetFunds.RemoveAt(i);
						}
						*/
					}	
				}

				//Add a dummy asset fund representing "All Asset Funds" within the selected company / fund group
				//todo - cannot instantiate abstract class - revisit this and check if this should be for oeic linked or composite

				AssetFund af = new LinkedAssetFund(); //new AssetFund();
				af.FullName = "All for selected fund group";
				assetFunds.Insert(0, af);

				//Show asset funds in combo (there will always be at least the "all"
				assetFundComboBox.BeginUpdate();
                assetFundComboBox.DisplayMember = "FullName";
				assetFundComboBox.DataSource = assetFunds;
				assetFundComboBox.Enabled = true;
                assetFundComboBox.EndUpdate();
            }
			else
			{
                assetFundComboBox.BeginUpdate();
                assetFundComboBox.DataSource = null;
				assetFundComboBox.Enabled = false;
                assetFundComboBox.EndUpdate();
			}
            T.X();
        }

        /// <summary>
        /// Create and bind the grid column styles
        /// </summary>
        private void createGridStyles()
        {
            T.E();
            try
            {
                // Create a new DataGridTableStyle for the funds grid.
                DataGridTableStyle fgTableStyle = new DataGridTableStyle();
                fgTableStyle.MappingName = "";
				fgTableStyle.AlternatingBackColor = Color.WhiteSmoke;

                // Hack to allow for multi-line header rows.
                // This requires a very large font to be set through the properties window
                fgTableStyle.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0))); 

                int defaultColWidth = 80;

                // Now add the column styles
                addTextBoxColumnStyle(fgTableStyle, "FullName", "Full Name", 300, HorizontalAlignment.Left);
                addTextBoxColumnStyle(fgTableStyle, "PriceDisplay", "Price", defaultColWidth, HorizontalAlignment.Right);
                addTextBoxColumnStyle(fgTableStyle, "PredictedPriceDisplay", "Predicted .\nPrice .", defaultColWidth, HorizontalAlignment.Right);
                addTextBoxColumnStyle(fgTableStyle, "PriceMovementPercentDisplay", "Imported .\nPrice .\nMovement .", defaultColWidth, HorizontalAlignment.Right);
                addTextBoxColumnStyle(fgTableStyle, "PredictedPriceMovementPercentDisplay", "Predicted .\nPrice .\nMovement .", defaultColWidth, HorizontalAlignment.Right);
                addTextBoxColumnStyle(fgTableStyle, "PriceMovementVarianceDisplay", "Price .\nVariance .", defaultColWidth, HorizontalAlignment.Right);
                addTextBoxColumnStyle(fgTableStyle, "PriceMovementRoundedToleranceDisplay", "Within price\nTolerance", 140, HorizontalAlignment.Left);
                addTextBoxColumnStyle(fgTableStyle, "AssetUnitPriceDisplay", "Asset .\nUnit Price", defaultColWidth, HorizontalAlignment.Right);
                addTextBoxColumnStyle(fgTableStyle, "AssetMovementDisplay", "Asset Price .\nMovement", 100, HorizontalAlignment.Right);
                addTextBoxColumnStyle(fgTableStyle, "PredictedAssetMovementDisplay", "Predicted AM", 90, HorizontalAlignment.Right);
                addTextBoxColumnStyle(fgTableStyle, "AssetMovementVarianceDisplay", "AM Variance", 90, HorizontalAlignment.Right);
                addBooleanColumnStyle(fgTableStyle, "WithinAssetMovementTolerance", "Within AM \nTolerance");
                addTextBoxColumnStyle(fgTableStyle, "FundStatusDisplay", "Current \nStatus ", defaultColWidth, HorizontalAlignment.Left);
                addTextBoxColumnStyle(fgTableStyle, "StatusChangedTime", "Last Status \nChanged ", 130, HorizontalAlignment.Left);

                // Remove any other table styles
                this.fundsGrid.TableStyles.Clear();
                
                // Add the grid style to the GridStylesCollection.
                this.fundsGrid.TableStyles.Add(fgTableStyle);
            }
            catch(Exception ex)
            {
                ExceptionManager.Publish( ex );
                ErrorDialog.Show( ex );
            }
            T.X();
        }
        
        /// <summary>
        /// Add a text box column style to the data grid table style
        /// </summary>
        /// <param name="tableStyle">Data grid table style object</param>
        /// <param name="columnMappingName">column mapping name</param>
        /// <param name="columnHeaderText">column heading text</param>
        /// <param name="columnAlignment">The column alignment</param>
        /// <param name="columnWidth">Default column width</param>
        private void addTextBoxColumnStyle(DataGridTableStyle tableStyle, 
            string columnMappingName, string columnHeaderText, int columnWidth,
            System.Windows.Forms.HorizontalAlignment columnAlignment)
        {
            T.E();
            DataGridTextBoxReadOnlyColumn columnStyle = new DataGridTextBoxReadOnlyColumn();

            // Build column attributes
            columnStyle.MappingName = columnMappingName;
            columnStyle.HeaderText = columnHeaderText;
            columnStyle.Width = columnWidth;
            columnStyle.Alignment = columnAlignment;
			columnStyle.ToolTipProperty = "FullName";

            // Add column style to table style.
            tableStyle.GridColumnStyles.Add(columnStyle);         
            T.X();
        }

        /// <summary>
        /// Add a boolean style column
        /// </summary>
        /// <param name="tableStyle">Data grid table style object</param>
        /// <param name="columnMappingName">column mapping name</param>
        /// <param name="columnHeaderText">column heading text</param>
		private void addBooleanColumnStyle(DataGridTableStyle tableStyle, 
			string columnMappingName, string columnHeaderText)
		{
			T.E();
            // Add a normal textbox column
			DataGridBoolColumn columnStyle = new DataGridBoolColumn();
			columnStyle.MappingName = columnMappingName;
			columnStyle.HeaderText = columnHeaderText;

			// Add column style to table style.
			tableStyle.GridColumnStyles.Add(columnStyle);         
            T.X();
		}

        /// <summary>
        /// Display the currently selected fund properties
        /// </summary>
        /// <param name="selectedRow">currently selected grid row number</param>
        private void displayFundProperties(int selectedRow)
        {
            T.E();
            // Make sure we have funds
            if ( m_fundsFiltered.Count > 0 )
            {
                // Get the currently selected fund object against 
                // the selected data grid row
                Fund selectedFund = m_fundsFiltered[selectedRow];	
            
                // Pass the currently selected fund object to the form 
                // and Display its properties
                Forms.CurrentFundStatusProperties properties = new Forms.CurrentFundStatusProperties(selectedFund);
                properties.ShowDialog(this.Parent);
            }
            T.X();
        }

        #endregion

        #region Private filter methods

		/// <summary>
		/// Apply the filters to the funds
		/// </summary>
		private void buildFilteredCollection()
		{
			T.E();
            //Reset the filtered funds collection
			m_fundsFiltered = new FundCollection();

			// Step through the funds collection and pick the required funds
			// and add to our filtered funds collection
			for (int i = 0; i < m_funds.Count; i++ )
			{
				bool addFundToCollection = false;

				//Reference the current fund
				Fund currentFund = (Fund)m_funds[i];

				//Ignore funds that are not in the current fund group (if "all" not set)
				if (isFundInFundGroup(currentFund))
				{
					//Ignore funds that are not in the current asset fund (if "all" not set)
					if (isFundInAssetFund(currentFund))
					{
						//Check whether user wants unfiltered collection 
						if (!applyFilterCheckBox.Checked)
						{
							addFundToCollection = true;					
						}
						else
						{
							//Step through all filters looking for any that are turned on
							for (int f = m_filters.Count - 1; f >= 0; f--)
							{
								if (!addFundToCollection)
								{
									if (m_filters[f].Value)
									{
										//A filter is turned on - check which it is and whether fund matches status
										switch (f)
										{
											case 0:
												addFundToCollection = isFirstLevelAuthorised(currentFund);
												break;
											case 1:
												addFundToCollection = isSecondLevelAuthorised(currentFund);
												break;
											case 2:
												addFundToCollection = isNotSecondLevelAuthorised(currentFund);
												break;
											case 3:
												addFundToCollection = isNotFirstOrSecondLevelAuthorised(currentFund);
												break;
											case 4:
												addFundToCollection = isSecondLevelAuthorisedButNotFirst(currentFund);
												break;
											case 5:
												addFundToCollection = isReleased(currentFund);
												break;
											case 6:
												addFundToCollection = isDistributed(currentFund);
												break;
											case 7:
												addFundToCollection = isAssetMovementOutsideTolerance(currentFund);
												break;
											case 8:
												addFundToCollection = isPriceOutsideTolerance(currentFund);
												break;
											default:
												addFundToCollection = false;
												break;
										}
									}
								}
							}
						}
					}
				}

				// Finally, add fund to filtered fund collection
				if ( addFundToCollection )
				{
					m_fundsFiltered.Add(currentFund);
				}
			}
            T.X();
		}

		/// <summary>
		/// Return whether the passed fund is in the selected fund group
		/// </summary>
		/// <param name="f"></param>
		/// <returns></returns>
		private bool isFundInFundGroup(Fund f)
		{
			T.E();
            bool retVal = true;
			
			//get the selected fund group
			FundGroup fg = ((FundGroup)fundGroupComboBox.SelectedItem);

			if (fg.ID != 0)
			{
				if (fg is AssetFundGroup)
				{
					//Fund group selected is an individual fund group - check the funds own fund group
					//Fund group selected is an asset fund group - check the funds asset fund for fund group
					//retVal = (f.ParentAssetFund.FundGroupID == fg.ID);
					retVal = false; //TODO - new user control with m:m mapping
				}
				else
				{
					//Fund group selected is an individual fund group - check the funds own fund group
					retVal = false; //TODO - new user control with m:m mapping (f.FundGroupID == fg.ID);
				}
			}
            T.X();
			return retVal;
		}

		/// <summary>
		/// Return whether the passed fund is in the selected asset fund
		/// </summary>
		/// <param name="f"></param>
		/// <returns></returns>
		private bool isFundInAssetFund(Fund f)
		{
			T.E();
            bool retVal = true;
			
			//get the selected asset fund
			//AssetFund af = ((AssetFund)assetFundComboBox.SelectedItem);
			AssetFund af = ((AssetFund)assetFundComboBox.SelectedItem);

			if (af.AssetFundCode != string.Empty)
			{
				retVal = (f.AssetFundID == af.AssetFundCode);
			}
            T.X();
			return retVal;
		}

        /// <summary>
        /// Is the the fund first level authorised?
        /// </summary>
        /// <param name="f">Fund object</param>
        /// <returns>Flag indicating if the fund has been first level authorised</returns>
        private bool isFirstLevelAuthorised(Fund f)
        {
            // Compare tolerances are within ranges for first level authorisation
            return (   f.WithinAssetMovementTolerance 
                && f.WithinPriceDirectionTolerance 
                && f.WithinPriceLowerTolerance
                && f.WithinPriceUpperTolerance );
        }

        /// <summary>
        /// Is the the fund second level authorised?
        /// </summary>
        /// <param name="f">Fund object</param>
        /// <returns>Flag indicating if the fund has been second level authorised</returns>
        private bool isSecondLevelAuthorised(Fund f)
        {
            return ( f.FundStatus == Fund.FundStatusType.SecondLevelAuthorised );
        }

        /// <summary>
        /// Is the fund second but not first level authorised?
        /// </summary>
        /// <param name="f">Fund object</param>
        /// <returns>Flag indicating if the fund has been second level 
        /// authorised and not first level authorised</returns>
        private bool isSecondLevelAuthorisedButNotFirst(Fund f)
        {
            return ( f.FundStatus == Fund.FundStatusType.SecondLevelAuthorised 
                && !this.isFirstLevelAuthorised(f) );
        }

        /// <summary>
        /// Has the fund NOT been second level authorised?
        /// </summary>
        /// <param name="f">Fund object</param>
        /// <returns>Flag indicating if th fund is NOT second level authorised</returns>
        private bool isNotSecondLevelAuthorised(Fund f)
        {
            return ( f.FundStatus != Fund.FundStatusType.SecondLevelAuthorised );
        }

        /// <summary>
        /// Is the fund neither first nor second level authorised?
        /// </summary>
        /// <param name="f">Fund object</param>
        /// <returns>Flag indicating that the fund is neither first nor second level authorised</returns>
        private bool isNotFirstOrSecondLevelAuthorised(Fund f)
        {
            return ( f.FundStatus != Fund.FundStatusType.SecondLevelAuthorised 
                && !this.isFirstLevelAuthorised(f) );
        }

        /// <summary>
        /// Has the fund been released for distribution?
        /// </summary>
        /// <param name="f">Fund object</param>
        /// <returns>Flag indicating if the fund has been released for distribution</returns>
        private bool isReleased(Fund f)
        {
            return ( f.FundStatus == Fund.FundStatusType.Released );
        }

        /// <summary>
        /// Has the fund been distributed
        /// </summary>
        /// <param name="f">Fund object</param>
        /// <returns>Flag indicating that the fund has been distributed</returns>
        private bool isDistributed(Fund f)
        {
            return ( f.FundStatus == Fund.FundStatusType.Distributed );
        }

        /// <summary>
        /// Is the asset movement outside the tolerance
        /// </summary>
        /// <param name="f">Fund object</param>
        /// <returns>Flag indicating the asset movement is outside the tolerance</returns>
        private bool isAssetMovementOutsideTolerance(Fund f)
        {
            return ( !f.WithinAssetMovementTolerance );
        }

        /// <summary>
        /// Is the price outside the tolerance
        /// </summary>
        /// <param name="f">Fund object</param>
        /// <returns>Flag indicating the price is outside the tolerance</returns>
        private bool isPriceOutsideTolerance(Fund f)
        {
            return ( !f.WithinPriceLowerTolerance && !f.WithinPriceUpperTolerance );
        }

        #endregion

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.fundsGrid = new HBOS.FS.AMP.Windows.Controls.DataGrid();
			this.printButton = new System.Windows.Forms.Button();
			this.exportButton = new System.Windows.Forms.Button();
			this.propertiesButton = new System.Windows.Forms.Button();
			this.refreshButton = new System.Windows.Forms.Button();
			this.noDataLabel = new System.Windows.Forms.Label();
			this.rowCountLabel = new System.Windows.Forms.Label();
			this.applyFilterCheckBox = new System.Windows.Forms.CheckBox();
			this.assetFundLabel = new System.Windows.Forms.Label();
			this.fundGroupLabel = new System.Windows.Forms.Label();
			this.assetFundComboBox = new System.Windows.Forms.ComboBox();
			this.fundGroupComboBox = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.filterListBox = new System.Windows.Forms.CheckedListBox();
			((System.ComponentModel.ISupportInitialize)(this.fundsGrid)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// fundsGrid
			// 
			this.fundsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.fundsGrid.BackColor = System.Drawing.Color.DarkGray;
			this.fundsGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.fundsGrid.CaptionBackColor = System.Drawing.Color.White;
			this.fundsGrid.CaptionFont = new System.Drawing.Font("Verdana", 10F);
			this.fundsGrid.CaptionForeColor = System.Drawing.Color.Navy;
			this.fundsGrid.CaptionText = "caption text";
			this.fundsGrid.DataMember = "";
			this.fundsGrid.FlatMode = false;
			this.fundsGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.fundsGrid.GridLineColor = System.Drawing.Color.Black;
			this.fundsGrid.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None;
			this.fundsGrid.HeaderBackColor = System.Drawing.Color.Silver;
			this.fundsGrid.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 28.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.fundsGrid.HeaderForeColor = System.Drawing.Color.Black;
			this.fundsGrid.LinkColor = System.Drawing.Color.Navy;
			this.fundsGrid.Location = new System.Drawing.Point(8, 128);
			this.fundsGrid.Name = "fundsGrid";
			this.fundsGrid.ParentRowsBackColor = System.Drawing.Color.White;
			//this.fundsGrid.PrintColumnWidths = null;
			this.fundsGrid.PrintInPortraitLayout = false;
			this.fundsGrid.ReadOnly = true;
			this.fundsGrid.SelectionBackColor = System.Drawing.Color.Navy;
			this.fundsGrid.Size = new System.Drawing.Size(768, 272);
			this.fundsGrid.TabIndex = 5;
			this.fundsGrid.TabStop = false;
			this.fundsGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.fundsGrid_KeyPress);
			this.fundsGrid.CellDoubleClicked += new HBOS.FS.AMP.Windows.Controls.DataGrid.CellDoubleClickedDelegate(this.fundsGrid_CellDoubleClicked);
			// 
			// printButton
			// 
			this.printButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.printButton.Location = new System.Drawing.Point(600, 408);
			this.printButton.Name = "printButton";
			this.printButton.Size = new System.Drawing.Size(80, 23);
			this.printButton.TabIndex = 8;
			this.printButton.Text = "&Print";
			this.printButton.Click += new System.EventHandler(this.printButton_Click);
			// 
			// exportButton
			// 
			this.exportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.exportButton.Location = new System.Drawing.Point(688, 408);
			this.exportButton.Name = "exportButton";
			this.exportButton.Size = new System.Drawing.Size(88, 23);
			this.exportButton.TabIndex = 9;
			this.exportButton.Text = "&Export";
			this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
			// 
			// propertiesButton
			// 
			this.propertiesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.propertiesButton.Location = new System.Drawing.Point(512, 408);
			this.propertiesButton.Name = "propertiesButton";
			this.propertiesButton.Size = new System.Drawing.Size(80, 23);
			this.propertiesButton.TabIndex = 7;
			this.propertiesButton.Text = "Proper&ties";
			this.propertiesButton.Click += new System.EventHandler(this.propertiesButton_Click);
			// 
			// refreshButton
			// 
			this.refreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.refreshButton.Location = new System.Drawing.Point(424, 408);
			this.refreshButton.Name = "refreshButton";
			this.refreshButton.Size = new System.Drawing.Size(80, 23);
			this.refreshButton.TabIndex = 6;
			this.refreshButton.Text = "&Refresh";
			this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
			// 
			// noDataLabel
			// 
			this.noDataLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.noDataLabel.Location = new System.Drawing.Point(8, 136);
			this.noDataLabel.Name = "noDataLabel";
			this.noDataLabel.Size = new System.Drawing.Size(696, 23);
			this.noDataLabel.TabIndex = 19;
			this.noDataLabel.Text = "No Funds match the selected filter criteria";
			// 
			// rowCountLabel
			// 
			this.rowCountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.rowCountLabel.ForeColor = System.Drawing.Color.Blue;
			this.rowCountLabel.Location = new System.Drawing.Point(8, 411);
			this.rowCountLabel.Name = "rowCountLabel";
			this.rowCountLabel.Size = new System.Drawing.Size(216, 16);
			this.rowCountLabel.TabIndex = 24;
			// 
			// applyFilterCheckBox
			// 
			this.applyFilterCheckBox.Location = new System.Drawing.Point(8, 80);
			this.applyFilterCheckBox.Name = "applyFilterCheckBox";
			this.applyFilterCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.applyFilterCheckBox.Size = new System.Drawing.Size(268, 24);
			this.applyFilterCheckBox.TabIndex = 3;
			this.applyFilterCheckBox.Text = "Apply additional filters";
			this.applyFilterCheckBox.CheckedChanged += new System.EventHandler(this.applyFilterCheckBox_CheckedChanged);
			// 
			// assetFundLabel
			// 
			this.assetFundLabel.AutoSize = true;
			this.assetFundLabel.Location = new System.Drawing.Point(8, 50);
			this.assetFundLabel.Name = "assetFundLabel";
			this.assetFundLabel.Size = new System.Drawing.Size(72, 18);
			this.assetFundLabel.TabIndex = 36;
			this.assetFundLabel.Text = "Asset Fund";
			// 
			// fundGroupLabel
			// 
			this.fundGroupLabel.AutoSize = true;
			this.fundGroupLabel.Location = new System.Drawing.Point(8, 16);
			this.fundGroupLabel.Name = "fundGroupLabel";
			this.fundGroupLabel.Size = new System.Drawing.Size(76, 18);
			this.fundGroupLabel.TabIndex = 35;
			this.fundGroupLabel.Text = "Fund Group";
			// 
			// assetFundComboBox
			// 
			this.assetFundComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.assetFundComboBox.Location = new System.Drawing.Point(88, 48);
			this.assetFundComboBox.Name = "assetFundComboBox";
			this.assetFundComboBox.Size = new System.Drawing.Size(192, 24);
			this.assetFundComboBox.TabIndex = 2;
			this.assetFundComboBox.SelectedIndexChanged += new System.EventHandler(this.assetFundComboBox_SelectedIndexChanged);
			// 
			// fundGroupComboBox
			// 
			this.fundGroupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.fundGroupComboBox.Location = new System.Drawing.Point(88, 16);
			this.fundGroupComboBox.Name = "fundGroupComboBox";
			this.fundGroupComboBox.Size = new System.Drawing.Size(192, 24);
			this.fundGroupComboBox.TabIndex = 1;
			this.fundGroupComboBox.SelectedIndexChanged += new System.EventHandler(this.fundGroupComboBox_SelectedIndexChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.filterListBox);
			this.groupBox1.Location = new System.Drawing.Point(296, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(480, 112);
			this.groupBox1.TabIndex = 39;
			this.groupBox1.TabStop = false;
			// 
			// filterListBox
			// 
			this.filterListBox.BackColor = System.Drawing.SystemColors.Control;
			this.filterListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.filterListBox.CheckOnClick = true;
			this.filterListBox.ColumnWidth = 180;
			this.filterListBox.Enabled = false;
			this.filterListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.filterListBox.Items.AddRange(new object[] {
															   "1st level authorised                       ",
															   "2nd level authorised",
															   "Not 2nd level authorised",
															   "Not 1st or 2nd level authorised",
															   "2nd, but not 1st level authorised",
															   "Released",
															   "Distributed",
															   "AM outside",
															   "Price outside tolerance"});
			this.filterListBox.Location = new System.Drawing.Point(16, 16);
			this.filterListBox.MultiColumn = true;
			this.filterListBox.Name = "filterListBox";
			this.filterListBox.Size = new System.Drawing.Size(424, 85);
			this.filterListBox.TabIndex = 4;
			this.filterListBox.ThreeDCheckBoxes = true;
			this.filterListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.filterListBox_ItemCheck);
			// 
			// CurrentFundStatus
			// 
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.applyFilterCheckBox);
			this.Controls.Add(this.assetFundLabel);
			this.Controls.Add(this.fundGroupLabel);
			this.Controls.Add(this.assetFundComboBox);
			this.Controls.Add(this.fundGroupComboBox);
			this.Controls.Add(this.rowCountLabel);
			this.Controls.Add(this.refreshButton);
			this.Controls.Add(this.propertiesButton);
			this.Controls.Add(this.exportButton);
			this.Controls.Add(this.printButton);
			this.Controls.Add(this.fundsGrid);
			this.Controls.Add(this.noDataLabel);
			this.Name = "CurrentFundStatus";
			this.Size = new System.Drawing.Size(784, 440);
			((System.ComponentModel.ISupportInitialize)(this.fundsGrid)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
        #endregion

        #region Control events

        private void printButton_Click(object sender, System.EventArgs e)
        {
            T.E();
            try
            {
                // Print set to landscape through the grid properties window
                this.fundsGrid.PrintPreview("Current Fund Status");
            }
            catch(Exception ex)
            {
                ExceptionManager.Publish( ex );
                ErrorDialog.Show( ex );
            }
            finally
            {
                T.X();
            }
        }

        private void exportButton_Click(object sender, System.EventArgs e)
        {
            T.E();
            try
            {
                // TODO: Prompt user for file destination
                string filename = "CurrentFundStatus.csv" ; // string.Empty;
                string actualFileName = Classes.ExportData.DisplayFilePicker(ref filename, "Export Data", "c:\temp", "All Text Files(*.csv)|(*.csv)|All files (*.*)|*.*", Classes.ExportData.FilePickerTypes.SaveDialog);

                if ( actualFileName != "" )
                {
                    this.fundsGrid.Write( filename, "HBOS.FS.AMP.UPD.WinUI.UserControls.CurrentFundStatus.xslt");
                    MessageBox.Show( string.Format( "{0} has been successfully exported." , actualFileName ), "Export Data", MessageBoxButtons.OK, MessageBoxIcon.Information );
                }
            }
            catch(Exception ex)
            {
                ExceptionManager.Publish( ex );
                ErrorDialog.Show( ex );
            }
            finally
            {
                T.X();
            }
        }

        /// <summary>
        /// Asset Fund Combo box changed.  Refresh data only if this was done by user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void assetFundComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
			T.E();
            if (m_userEditing)
			{
				refreshDataGrid();
			}
            T.X();
        }

        /// <summary>
        /// Display the currently select funds properties: tolerance and factors
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void propertiesButton_Click(object sender, System.EventArgs e)
        {
            // Show the fund properties --> tolerances and factors
            T.E();
            this.displayFundProperties(this.fundsGrid.CurrentCell.RowNumber);
            T.X();
			
        }

        /// <summary>
        /// Display the currently selected funds properties
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fundsGrid_CellDoubleClicked(object sender, HBOS.FS.AMP.Windows.Controls.CellEventArgs e)
        {
            // Show the fund properties --> tolerances and factors
			//KAJ 09/03/2005 - Double clicking header row causes exception
			T.E();
            if (e.RowNumber >= 0)
			{
				this.displayFundProperties(e.RowNumber);
			}
            T.X();
        }

        private void fundsGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            T.E();
            // Has the ENTER key been pressed within the grid?
            if ( e.KeyChar == '\r' )
            {
                // Display the selected fund properties
                this.displayFundProperties(this.fundsGrid.CurrentCell.RowNumber);
            }
            T.X();
        }

		private void refreshButton_Click(object sender, System.EventArgs e)
		{
			T.E();
            RefreshData();
            T.X();
		}

		private void fundGroupComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			T.E();
            if (m_userEditing)
			{
				// Load asset fund list filtered on the selected fund group
				loadAssetFundCombo();
			}
            T.X();
		}

		private void filterListBox_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
		{	
			T.E();
            //Set the filter
			((BooleanFilter)((CheckedListBox)sender).SelectedItem).Value = (e.NewValue == CheckState.Checked);
            
			// Now refresh the data grid
			refreshDataGrid();
            T.X();
		}

		/// <summary>
		/// User has changed whether to apply the additional filters.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void applyFilterCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			T.E();
            filterListBox.Enabled = applyFilterCheckBox.Checked;
			refreshDataGrid();
            T.X();
		}

		#endregion    		

   }
}
