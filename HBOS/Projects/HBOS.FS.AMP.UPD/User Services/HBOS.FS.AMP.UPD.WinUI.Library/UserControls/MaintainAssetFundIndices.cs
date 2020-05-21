using System;
using System.Configuration;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using HBOS.FS.Common.ExceptionManagement;

using HBOS.FS.AMP.ExceptionManagement;

using HBOS.FS.AMP.Windows.Controls;

using HBOS.FS.AMP.UPD.Security;
using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.Countries;
using HBOS.FS.AMP.UPD.Types.FundGroups;

namespace HBOS.FS.AMP.UPD.WinUI.UserControls
{
	/// <summary>
	/// Summary description for MaintainAssetFundIndices.
	/// </summary>
	public class MaintainAssetFundIndices : System.Windows.Forms.UserControl
	{
        #region Controls

        private System.Windows.Forms.Label selectAssetFundLabel;
        private System.Windows.Forms.ComboBox assetFundComboBox;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveButton;
        private HBOS.FS.AMP.Windows.Controls.DataGrid assetFundIndexDataGrid;

        #endregion

        #region Private variables

        // Non-GUI stuff
        private const string M_IMPORT_REFERENCE = "marketValueSplitGUI";
        private const int M_COUNTRY_COLUMN = 0;
        private const int M_CURRENCY_COLUMN = 2;

        private string m_connectionString = string.Empty;
        private string m_selectedAssetFundCode = string.Empty;

        private int m_selectedAssetFundComboIndex = -1;
        private bool m_dataSaved = false;
        private bool m_userChange = false;      // Flag used to reset the asset fund combo when user cancels changes
        
        private AssetFundController m_indices;
        private AssetFundCollection m_currentAssetFunds;
        private CountryCollection m_countries;
        private MarketIndexCollection m_marketIndices;
        
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
        #endregion

        #region Constructor
		/// <summary>
		/// Default constructor
		/// </summary>
        public MaintainAssetFundIndices()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

            //
            // TODO: see if the connection string should be in the controller
            //
            m_connectionString = ConfigurationSettings.AppSettings["ConnectionString"];
            
            loadControl();

            float scaleWidth = Form.GetAutoScaleSize(this.Font).Width;
            float adjustment = scaleWidth / 5.32F;

            assetFundComboBox.Width = (int) ((float)assetFundComboBox.Width * adjustment);
            assetFundComboBox.Left = selectAssetFundLabel.Left + selectAssetFundLabel.Width + (int)(12F * adjustment);

		}
        #endregion

        #region System methods
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

        #region Public methods
        
        /// <summary>
        /// Reset the collection object and refresh the data grid
        /// </summary>
        public void Reset()
        {
            this.refreshDataGrid(true);
        }

        #endregion

        #region Private methods
        
        /// <summary>
        /// Initialise the control
        /// </summary>
        private void loadControl()
        {
            // Disable buttons until edited
            //enableButtons(false);

            // Build collection objects...
            m_indices = new AssetFundController();                                  // Get the asset funds 
            m_countries = m_indices.LoadCountries(m_connectionString);              // Get the countries
            m_marketIndices = m_indices.LoadStockMarketIndices(m_connectionString); // Get the stock market indices

            this.loadAssetFundCombo();       // Load the asset funds into the list
            this.addDataGridStyles();        // Set the grid styles
            this.refreshDataGrid(true);

            m_userChange = true;
        }
        
        private void enableButtons(bool enable)
        {
            this.saveButton.Enabled = enable;
            this.cancelButton.Enabled = enable;
        }

        /// <summary>
        /// Populate the asset fund combo
        /// </summary>
        private void loadAssetFundCombo()
        {
            // Get the currently selected company from the GUI principal thread
            UPDPrincipal updPrincipal = (UPDPrincipal) System.Threading.Thread.CurrentPrincipal;

            // Load the asset funds for the selected fund group
            m_currentAssetFunds = m_indices.LoadAssetFunds(m_connectionString,
				updPrincipal.CompanyCode, true);

            // Get the asset funds and bind to the combo Box
            this.assetFundComboBox.Enabled = true;
            this.assetFundComboBox.DataSource = m_currentAssetFunds;
            this.assetFundComboBox.DisplayMember = "FullName";
            this.assetFundComboBox.ValueMember = "AssetFundCode";

            m_selectedAssetFundComboIndex = this.assetFundComboBox.SelectedIndex;
        }

        /// <summary>
        /// Return an asset fund object where the code match's the selected 
        /// item from the asset fund combo box
        /// </summary>
        /// <param name="assetFundCode"></param>
        /// <returns></returns>
        private AssetFund retrieveAssetFundFromCollection(string assetFundCode)
        {
            for (int i=0; i<m_currentAssetFunds.Count; i++)
            {
                AssetFund currentAssetFund = (AssetFund)m_currentAssetFunds[i];
                if (currentAssetFund.AssetFundCode == assetFundCode)
                {
                    return currentAssetFund;
                }
            }
            
            // You should never get here!
            return null; //new AssetFund();
        }

        /// <summary>
        /// Add the combox and textbox columns to the grid.
        /// </summary>
        /// <remarks><b>CAUTION</b> Always create DataGridColumnStyle objects and add them to the  
        /// GridColumnStylesCollection before adding DataGridTableStyle objects to the GridTableStylesCollection. 
        /// When you add an empty DataGridTableStyle to the collection, DataGridColumnStyle objects 
        /// are automatically generated for you. Consequently, an exception will be thrown if you 
        /// try to add new DataGridColumnStyle objects with duplicate MappingName values to the 
        /// GridColumnStylesCollection.
        ///</remarks>
        private void addDataGridStyles()
        {
            try
            {
                // Create a new DataGridTableStyle and set MappingName.
                DataGridTableStyle indicesGridStyle = new DataGridTableStyle();
                indicesGridStyle.MappingName = "";
				indicesGridStyle.AlternatingBackColor = Color.WhiteSmoke;

                // Create a DataGridColumnStyle object as a combobox column.
                DataGridComboBoxColumn indexColStyle = new DataGridComboBoxColumn();
                //indexColStyle.DataSource = m_marketIndices.ConvertToDataTable().DefaultView;  // = m_indices.LoadStockMarketIndices("").ConvertToDataTable().DefaultView;
				indexColStyle.DataSource = m_marketIndices; 
                indexColStyle.MappingName = "MarketIndexID";
                indexColStyle.DisplayMember = "IndexName";
                indexColStyle.ValueMember = "MarketIndexID";
                indexColStyle.HeaderText = "Stock Market Index";

                // Add an event for when the combobox item changes
                indexColStyle.SelectedIndexChanged += new HBOS.FS.AMP.Windows.Controls.DataGridComboBoxColumn.SelectedIndexChangedDelegate(indexColStyle_SelectedIndexChanged);

                // Country column
                DataGridTextBoxColumn countryColStyle = new DataGridTextBoxColumn();
                countryColStyle.HeaderText = "Country";
                countryColStyle.MappingName = "countryCode";
                countryColStyle.ReadOnly = true;

                // Currency column 
                DataGridTextBoxColumn currencyColStyle = new DataGridTextBoxColumn();
                currencyColStyle.MappingName = "CurrencyCode";
                currencyColStyle.HeaderText = "Currency";
                currencyColStyle.ReadOnly = true;
            
                // Now add a percentage column
				DataGridPercentageColumn proportionColStyle = new DataGridPercentageColumn();
				proportionColStyle.MappingName = "Proportion";
				proportionColStyle.HeaderText = "Proportion";
				proportionColStyle.DecimalPlaces = 4;

                // Remove any other table styles
                this.assetFundIndexDataGrid.TableStyles.Clear();
                
                // Add column styles to table style.
                indicesGridStyle.GridColumnStyles.Add(countryColStyle);
                indicesGridStyle.GridColumnStyles.Add(indexColStyle);   
                indicesGridStyle.GridColumnStyles.Add(currencyColStyle);
                indicesGridStyle.GridColumnStyles.Add(proportionColStyle);   

                // Add the grid style to the GridStylesCollection.
                this.assetFundIndexDataGrid.TableStyles.Add(indicesGridStyle);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        /// <summary>
        /// Used to add unique currencies to the currency combo - the market
        /// indices collection contains all the currencies, many of which
        /// will be EURO.
        /// </summary>
        /// <returns>A currency collection containing unique items</returns>
        private CurrencyCollection LoadUniqueCurrencies()
        {
            // Create a currency collection
            CurrencyCollection currencies = new CurrencyCollection();

            // Create a hashtable to store the names of the currency codes
            System.Collections.Hashtable tempCurrencies = new Hashtable();

            // Loop through the stock market indices collection
            for ( int i = 0; i < m_marketIndices.Count; i++ )
            {
                MarketIndex currentIndex = (MarketIndex)m_marketIndices[i];

                // Does the currency exist?
                if ( !(tempCurrencies.ContainsKey(currentIndex.CurrencyCode)) )
                {
                    // Add to hashTable for easy interogation
                    tempCurrencies.Add(currentIndex.CurrencyCode, currentIndex);
                        
                    // Create currency object and add to the currency collection...
                    Currency newCurrency = new Currency(currentIndex.CurrencyCode);
                    currencies.Add(newCurrency);
                }
            }

            return currencies;        
        }

        /// <summary>
        /// Refresh the data grid with the correct market value split 
        /// rows for the selected asset fund
        /// </summary>
        private void refreshDataGrid(bool getDataFromDatabase)
        {
            // Get the selected asset fund
            m_selectedAssetFundCode = (string)assetFundComboBox.SelectedValue;

            AssetFund selectedAssetFund = retrieveAssetFundFromCollection(m_selectedAssetFundCode);
         
			//TODO - this has now all change and methods deprecated. New UI required.
			/*
            // Get the asset fund weighted index's for the selected asset fund
            AssetFundIndexWeightedCollection currentMarketValueSplits;
            try
            {
                // Query DB for matching weighted indices for the selected asset fund
                if ( getDataFromDatabase )
                {
                    currentMarketValueSplits = m_indices.LoadMarketValueSplit(m_connectionString,
						selectedAssetFund.AssetFundCode, true);
                }
                else
                {
                    // Otherwise get the original from the data grid
                    currentMarketValueSplits = (AssetFundIndexWeightedCollection)this.assetFundIndexDataGrid.RetrieveOriginalCustomCollection();
                }

//                if (currentMarketValueSplits.Count == 0)
//                {
//                    currentMarketValueSplits = new AssetFundIndexWeightedCollection();
//                    AssetFundIndexWeighted dummyIndex = new AssetFundIndexWeighted();
//                    currentMarketValueSplits.Add(dummyIndex);
//                }
                this.assetFundIndexDataGrid.BindToCustomCollection(currentMarketValueSplits ) ; // , typeof(AssetFundIndexWeighted));

				if ( m_marketIndices.Count > 0 )
				{
					// Default to the first market index
					this.assetFundIndexDataGrid.SetDefaultValue( "MarketIndexID" , m_marketIndices[0].MarketIndexID );
					this.assetFundIndexDataGrid.SetDefaultValue( "CurrencyCode" , m_marketIndices[0].CurrencyCode );
					this.assetFundIndexDataGrid.SetDefaultValue( "countryCode" , m_marketIndices[0].CountryCode );
				}
  
                // Make sure the grid is visisble when the user changes the selection
                this.assetFundIndexDataGrid.Visible = true;
            }
            catch(Exception ex)
            {
                ExceptionManager.Publish( ex );
                ErrorDialog.Show( ex );
            }
              */
		}

        /// <summary>
        /// Save any new or modified grid rows to the database via the controller
        /// </summary>
        private void saveMarketValueSplit()
        {
            // Only save if there have been any modifications
            if ( this.gridIsDirty() )
            {
                // Persist all items back to the DB when one or more existing items have 
                // changed or appended

				//TODO - this has now all change. New UI to be developed
				/*
                try
                {
                    // Firstly, retrieve the modified indices collection from the grid.
                    AssetFundIndexWeightedCollection entireCollection = (AssetFundIndexWeightedCollection)this.assetFundIndexDataGrid.RetrieveEntireCustomCollection();  // typeof(AssetFundIndexWeighted));
            
                    // Set the asset fund code to be that of the currently selected asset fund
                    for(int i = 0; i < entireCollection.Count; i++)
                    {
                        AssetFundIndexWeighted index = (AssetFundIndexWeighted)entireCollection[i];
                        index.AssetFundCode = m_selectedAssetFundCode;
                    }

                    string messageText = string.Empty;
                    bool validFlag = false;

                    // Secondly, validate that the sum of the proportions is exactly 100 and that all the 
                    // asset fund index weighted properties are set.
                    AssetFundController afController = new AssetFundController();

                    validFlag = afController.ValidateMaketValueSplit(entireCollection, out messageText);

                    // If the sum of the proportion value is OK, then save
                    if ( validFlag )
                    {
                        validFlag = afController.UpdateMarketValueSplit(m_connectionString,
							entireCollection, M_IMPORT_REFERENCE);
                    }

                    if ( validFlag )
                    {
                        m_selectedAssetFundComboIndex = this.assetFundComboBox.SelectedIndex;
                    }
                    else
                    {
                        // otherwise, display a message to the user.               
                        if ( messageText.Length == 0 )
                        {
                            messageText = "The modified data was not saved back to the database.";
                        }

                        MessageBox.Show(messageText, "Invalid Data", MessageBoxButtons.OK,
							MessageBoxIcon.Warning);
                    }

                    // Flag that the data has successfully been persisted back to the database
                    m_dataSaved = validFlag;
                }
                catch(Exception ex)
                {
                    ExceptionManager.Publish( ex );
                    ErrorDialog.Show( ex );
                }
				*/
            }
        }

        /// <summary>
        /// Test for modified rows in the grid
        /// </summary>
        /// <returns>Flag indicating if rows have been modified</returns>
        private bool gridIsDirty()
        {
            AssetFundIndexWeightedCollection modifiedIndices = (AssetFundIndexWeightedCollection)this.assetFundIndexDataGrid.RetrieveUpdatedCustomCollection(); // typeof(AssetFundIndexWeighted));
            
            if ( modifiedIndices.Count == 0 )
            {
                return ( false );
            }
            else
            {
                return ( true );
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
            this.selectAssetFundLabel = new System.Windows.Forms.Label();
            this.assetFundComboBox = new System.Windows.Forms.ComboBox();
            this.assetFundIndexDataGrid = new HBOS.FS.AMP.Windows.Controls.DataGrid();
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.assetFundIndexDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // selectAssetFundLabel
            // 
            this.selectAssetFundLabel.AutoSize = true;
            this.selectAssetFundLabel.Location = new System.Drawing.Point(8, 8);
            this.selectAssetFundLabel.Name = "selectAssetFundLabel";
            this.selectAssetFundLabel.Size = new System.Drawing.Size(145, 16);
            this.selectAssetFundLabel.TabIndex = 0;
            this.selectAssetFundLabel.Text = "Please select an asset fund:";
            // 
            // assetFundComboBox
            // 
            this.assetFundComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.assetFundComboBox.Location = new System.Drawing.Point(168, 8);
            this.assetFundComboBox.Name = "assetFundComboBox";
            this.assetFundComboBox.Size = new System.Drawing.Size(224, 21);
            this.assetFundComboBox.TabIndex = 1;
            this.assetFundComboBox.Validating += new System.ComponentModel.CancelEventHandler(this.assetFundComboBox_Validating);
            this.assetFundComboBox.SelectionChangeCommitted += new System.EventHandler(this.assetFundComboBox_SelectionChangeCommitted);
            // 
            // assetFundIndexDataGrid
            // 
            this.assetFundIndexDataGrid.AllowNew = true;
            this.assetFundIndexDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.assetFundIndexDataGrid.DataMember = "";
            this.assetFundIndexDataGrid.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.assetFundIndexDataGrid.GridLineColor = System.Drawing.Color.Black;
            this.assetFundIndexDataGrid.HeaderFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.assetFundIndexDataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.assetFundIndexDataGrid.Location = new System.Drawing.Point(8, 40);
            this.assetFundIndexDataGrid.Name = "assetFundIndexDataGrid";
            //this.assetFundIndexDataGrid.PrintColumnWidths = null;
            this.assetFundIndexDataGrid.Size = new System.Drawing.Size(488, 208);
            this.assetFundIndexDataGrid.TabIndex = 2;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(424, 264);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Location = new System.Drawing.Point(336, 264);
            this.saveButton.Name = "saveButton";
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "&Save";
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // MaintainAssetFundIndices
            // 
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.assetFundIndexDataGrid);
            this.Controls.Add(this.assetFundComboBox);
            this.Controls.Add(this.selectAssetFundLabel);
            this.Name = "MaintainAssetFundIndices";
            this.Size = new System.Drawing.Size(504, 304);
            ((System.ComponentModel.ISupportInitialize)(this.assetFundIndexDataGrid)).EndInit();
            this.ResumeLayout(false);

        }
		#endregion

        #region Control events


        /// <summary>
        /// Execute the save market value split method and refresh the data grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, System.EventArgs e)
        {
            this.saveMarketValueSplit();
            
            if ( m_dataSaved )
            {
                this.refreshDataGrid(true);
            }
        }

        /// <summary>
        /// Pick up a change in the stock market index and update the 
        /// country and currency cells
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void indexColStyle_SelectedIndexChanged(object sender, ComboBoxEventArgs e)
        {
            // Note.  Fidling around with the objects is confusing the hell out 'e'
            int selectedMarketIndexId = Convert.ToInt32( e.SelectedValue );
			int currentRowNumber = Convert.ToInt32( e.RowNumber );

			// Find the selected Market Index
			for( int rowIndex = 0 ; rowIndex < m_marketIndices.Count ; rowIndex ++ )
			{
				if ( m_marketIndices[ rowIndex ].MarketIndexID == selectedMarketIndexId )
				{
					// Assign values directly to the grid
					this.assetFundIndexDataGrid[currentRowNumber, M_COUNTRY_COLUMN] = m_marketIndices[ rowIndex ].CountryCode;
					this.assetFundIndexDataGrid[currentRowNumber, M_CURRENCY_COLUMN] = m_marketIndices[ rowIndex ].CurrencyCode;
					break;
				}
			}

            m_dataSaved = false;
        }

        /// <summary>
        /// Test for modified rows that have not been modified before changing the asset fund
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void assetFundComboBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Have grid rows been modified and not saved?
            if ( this.gridIsDirty() && !m_dataSaved )
            {
                // Prompt user to save changes
                DialogResult messageResult = MessageBox.Show("Information has been modified in the data grid.", "Save Data", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (messageResult)
                {
                    case DialogResult.Yes:
                        this.saveMarketValueSplit();
                        break;
                    case DialogResult.No:
                        m_dataSaved = true;
                        break;
                    default:             // DialogResult.Cancel
                        e.Cancel = true;
                        break;
                }
            }
        }

        /// <summary>
        /// Refresh the data grid with the latest information from the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, System.EventArgs e)
        {
            this.refreshDataGrid(false);
            m_dataSaved = false;
        }

        /// <summary>
        /// Refresh the grid when the asset fund changes
        /// </summary>
        /// <remarks>If changes have been made and the user selects to Cancel
        /// then this event will not be fired.</remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void assetFundComboBox_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
                // Have grid rows been modified and not saved?
            if (m_selectedAssetFundComboIndex > -1 && this.gridIsDirty() && !m_dataSaved )
            {
                // Prompt user to save changes
                DialogResult messageResult = MessageBox.Show("Information has been modified in the data grid.", "Save Data", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (messageResult)
                {
                    case DialogResult.Yes:
                        m_userChange = true;
                        this.saveMarketValueSplit();
                        break;
                    case DialogResult.No:
                        m_dataSaved = true;
                        m_userChange = true;
                        break;
                    default:             // DialogResult.Cancel
                        m_userChange = false;
                        this.assetFundComboBox.SelectedIndex = m_selectedAssetFundComboIndex;
                        break;
                }
            }

            m_selectedAssetFundComboIndex = this.assetFundComboBox.SelectedIndex;

            if ( m_userChange )
            {
                refreshDataGrid(true);
            }
        }
        #endregion    
    }
}
