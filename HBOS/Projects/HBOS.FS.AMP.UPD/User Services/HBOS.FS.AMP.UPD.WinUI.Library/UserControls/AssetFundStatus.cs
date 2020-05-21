using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using HBOS.FS.AMP.Windows.Controls;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.WeightedIndices;
using HBOS.FS.AMP.UPD.Types.FundGroups;
using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.Security;
using HBOS.FS.AMP.ExceptionManagement;
using HBOS.FS.Common.ExceptionManagement;
using HBOS.FS.AMP.UPD.WinUI.Classes;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.UserControls
{
	/// <summary>
	/// Summary description for AssetFundStatus.
	/// </summary>
	public class AssetFundStatus : System.Windows.Forms.UserControl
	{

		#region Variables

		private HBOS.FS.AMP.Windows.Controls.DataGrid assetFundsGrid;
		private System.Windows.Forms.ComboBox fundGroupComboBox;
		private System.Windows.Forms.Label fundGroupLabel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label noDataLabel;
		private System.Windows.Forms.Button exportButton;
		private System.Windows.Forms.Button printButton;
		private System.Windows.Forms.CheckBox applyFilterCheckBox;
		private System.Windows.Forms.Label rowCountLabel;
		private System.Windows.Forms.CheckedListBox filterListBox;
		private System.Windows.Forms.Button refreshButton;

		string m_connectionString = string.Empty;
		private FundGroupCollection m_fundGroups = null;
		private AssetFundCollection m_assetFunds = null;
		private BooleanFilterCollection m_filters = new BooleanFilterCollection();
		private bool m_userEditing = false;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new <see cref="AssetFundStatus"/> instance.
		/// </summary>
		public AssetFundStatus()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			m_connectionString = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
			loadForm();
		}

		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.assetFundsGrid = new HBOS.FS.AMP.Windows.Controls.DataGrid();
            this.fundGroupComboBox = new System.Windows.Forms.ComboBox();
            this.fundGroupLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.filterListBox = new System.Windows.Forms.CheckedListBox();
            this.noDataLabel = new System.Windows.Forms.Label();
            this.exportButton = new System.Windows.Forms.Button();
            this.printButton = new System.Windows.Forms.Button();
            this.applyFilterCheckBox = new System.Windows.Forms.CheckBox();
            this.rowCountLabel = new System.Windows.Forms.Label();
            this.refreshButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.assetFundsGrid)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // assetFundsGrid
            // 
            this.assetFundsGrid.AlternatingBackColor = System.Drawing.SystemColors.Window;
            this.assetFundsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.assetFundsGrid.BackColor = System.Drawing.SystemColors.Window;
            this.assetFundsGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.assetFundsGrid.CaptionBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.assetFundsGrid.CaptionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.assetFundsGrid.DataMember = "";
            this.assetFundsGrid.FlatMode = false;
            this.assetFundsGrid.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.assetFundsGrid.ForeColor = System.Drawing.SystemColors.WindowText;
            this.assetFundsGrid.GridLineColor = System.Drawing.SystemColors.Control;
            this.assetFundsGrid.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.assetFundsGrid.HeaderFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.assetFundsGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.assetFundsGrid.LinkColor = System.Drawing.SystemColors.HotTrack;
            this.assetFundsGrid.Location = new System.Drawing.Point(8, 80);
            this.assetFundsGrid.Name = "assetFundsGrid";
            this.assetFundsGrid.ParentRowsBackColor = System.Drawing.SystemColors.Control;
            this.assetFundsGrid.ParentRowsForeColor = System.Drawing.SystemColors.WindowText;
            //this.assetFundsGrid.PrintColumnWidths = null;
            this.assetFundsGrid.PrintInPortraitLayout = false;
            this.assetFundsGrid.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.assetFundsGrid.SelectionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.assetFundsGrid.Size = new System.Drawing.Size(704, 304);
            this.assetFundsGrid.TabIndex = 4;
            this.assetFundsGrid.TabStop = false;
            // 
            // fundGroupComboBox
            // 
            this.fundGroupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fundGroupComboBox.Location = new System.Drawing.Point(88, 14);
            this.fundGroupComboBox.Name = "fundGroupComboBox";
            this.fundGroupComboBox.Size = new System.Drawing.Size(188, 21);
            this.fundGroupComboBox.TabIndex = 1;
            this.fundGroupComboBox.SelectedIndexChanged += new System.EventHandler(this.fundGroupComboBox_SelectedIndexChanged);
            // 
            // fundGroupLabel
            // 
            this.fundGroupLabel.AutoSize = true;
            this.fundGroupLabel.Location = new System.Drawing.Point(8, 16);
            this.fundGroupLabel.Name = "fundGroupLabel";
            this.fundGroupLabel.Size = new System.Drawing.Size(64, 16);
            this.fundGroupLabel.TabIndex = 16;
            this.fundGroupLabel.Text = "Fund Group";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.filterListBox);
            this.groupBox1.Location = new System.Drawing.Point(296, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(416, 64);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            // 
            // filterListBox
            // 
            this.filterListBox.BackColor = System.Drawing.SystemColors.Control;
            this.filterListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.filterListBox.CheckOnClick = true;
            this.filterListBox.Enabled = false;
            this.filterListBox.Location = new System.Drawing.Point(16, 16);
            this.filterListBox.Name = "filterListBox";
            this.filterListBox.Size = new System.Drawing.Size(392, 45);
            this.filterListBox.TabIndex = 3;
            this.filterListBox.ThreeDCheckBoxes = true;
            this.filterListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.filterListBox_ItemCheck);
            // 
            // noDataLabel
            // 
            this.noDataLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.noDataLabel.Location = new System.Drawing.Point(16, 96);
            this.noDataLabel.Name = "noDataLabel";
            this.noDataLabel.Size = new System.Drawing.Size(680, 23);
            this.noDataLabel.TabIndex = 18;
            this.noDataLabel.Text = "No Asset Funds match the selected filter criteria";
            // 
            // exportButton
            // 
            this.exportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.exportButton.Location = new System.Drawing.Point(636, 392);
            this.exportButton.Name = "exportButton";
            this.exportButton.TabIndex = 7;
            this.exportButton.Text = "&Export";
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // printButton
            // 
            this.printButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.printButton.Location = new System.Drawing.Point(556, 392);
            this.printButton.Name = "printButton";
            this.printButton.TabIndex = 6;
            this.printButton.Text = "&Print";
            this.printButton.Click += new System.EventHandler(this.printButton_Click);
            // 
            // applyFilterCheckBox
            // 
            this.applyFilterCheckBox.Location = new System.Drawing.Point(8, 48);
            this.applyFilterCheckBox.Name = "applyFilterCheckBox";
            this.applyFilterCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.applyFilterCheckBox.Size = new System.Drawing.Size(268, 24);
            this.applyFilterCheckBox.TabIndex = 2;
            this.applyFilterCheckBox.Text = "Apply additional filters";
            this.applyFilterCheckBox.CheckedChanged += new System.EventHandler(this.applyFilterCheckBox_CheckedChanged);
            // 
            // rowCountLabel
            // 
            this.rowCountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rowCountLabel.ForeColor = System.Drawing.Color.Blue;
            this.rowCountLabel.Location = new System.Drawing.Point(8, 395);
            this.rowCountLabel.Name = "rowCountLabel";
            this.rowCountLabel.Size = new System.Drawing.Size(216, 16);
            this.rowCountLabel.TabIndex = 40;
            // 
            // refreshButton
            // 
            this.refreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshButton.Location = new System.Drawing.Point(474, 392);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.TabIndex = 5;
            this.refreshButton.Text = "&Refresh";
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // AssetFundStatus
            // 
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.rowCountLabel);
            this.Controls.Add(this.applyFilterCheckBox);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.printButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.fundGroupLabel);
            this.Controls.Add(this.fundGroupComboBox);
            this.Controls.Add(this.assetFundsGrid);
            this.Controls.Add(this.noDataLabel);
            this.Name = "AssetFundStatus";
            this.Size = new System.Drawing.Size(720, 424);
            ((System.ComponentModel.ISupportInitialize)(this.assetFundsGrid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
		#endregion

		#region Methods

		/// <summary>
		/// Load the combo box (fund groups) and set up the grid
		/// </summary>
		private void loadForm()
		{
			populateFilters();			// Populate the list of filters
			addDataGridStyles();		// Set up the grid
			RefreshData();				// Refresh the asset funds data and the grid
		}

		/// <summary>
		/// Create the additional filters for choosing data items
		/// </summary>
		private void populateFilters()
		{
			m_filters.Clear();
			m_filters.Add(new BooleanFilter(false, "Passed Asset Movement Tolerance"));
			m_filters.Add(new BooleanFilter(false, "Failed Asset Movement Tolerance"));
			
			//Add all the filters to the displayer
			filterListBox.Items.Clear();
			foreach (BooleanFilter bf in m_filters)
			{
				filterListBox.Items.Add(bf);
			}
		}

		/// <summary>
		/// Set up the grid
		/// </summary>
		private void addDataGridStyles()
		{
			try
			{
				//Create the columns
				DataGridTextBoxReadOnlyColumn fundNameCol = new DataGridTextBoxReadOnlyColumn();
				fundNameCol.MappingName = "FullName";
				fundNameCol.HeaderText = "Fund Name";
				fundNameCol.Width = 220;

				DataGridTextBoxReadOnlyColumn unitPriceCol = new DataGridTextBoxReadOnlyColumn();
				unitPriceCol.MappingName = "UnitPriceDisplay";
				unitPriceCol.HeaderText = "Unit Price";
				unitPriceCol.Width = 90;
				unitPriceCol.Alignment = HorizontalAlignment.Right;

				DataGridTextBoxReadOnlyColumn priceMovementCol = new DataGridTextBoxReadOnlyColumn();
				priceMovementCol.MappingName = "UnitPriceMovementDisplay";
				priceMovementCol.HeaderText = "Price Movement";
				priceMovementCol.Width = 90;
				priceMovementCol.Alignment = HorizontalAlignment.Right;

				DataGridTextBoxReadOnlyColumn predictedAssetMovementCol = new DataGridTextBoxReadOnlyColumn();
				predictedAssetMovementCol.MappingName = "PredictedAssetMovementDisplay";
				predictedAssetMovementCol.HeaderText = "Predicted AM";
				predictedAssetMovementCol.Width = 90;
				predictedAssetMovementCol.Alignment = HorizontalAlignment.Right;

				DataGridTextBoxReadOnlyColumn assetMovementVarianceCol = new DataGridTextBoxReadOnlyColumn();
				assetMovementVarianceCol.MappingName = "AssetMovementVarianceDisplay";
				assetMovementVarianceCol.HeaderText = "AM Variance";
				assetMovementVarianceCol.Width = 90;
				assetMovementVarianceCol.Alignment = HorizontalAlignment.Right;

				DataGridBoolColumn withinToleranceCol = new DataGridBoolColumn();
				withinToleranceCol.MappingName = "WithinAssetMovementTolerance";
				withinToleranceCol.HeaderText = "Within tolerance";
				withinToleranceCol.ReadOnly = true;
				withinToleranceCol.Width = 90;
				
				//Create a new DataGridTableStyle and set MappingName.
				DataGridTableStyle assetFundGridStyle = new DataGridTableStyle();
				assetFundGridStyle.MappingName = "";
				assetFundGridStyle.AlternatingBackColor = Color.WhiteSmoke;

				//Add columns to table style.				
				assetFundGridStyle.GridColumnStyles.Add(fundNameCol);
				assetFundGridStyle.GridColumnStyles.Add(unitPriceCol);
				assetFundGridStyle.GridColumnStyles.Add(priceMovementCol);
				assetFundGridStyle.GridColumnStyles.Add(predictedAssetMovementCol);
				assetFundGridStyle.GridColumnStyles.Add(assetMovementVarianceCol);
				assetFundGridStyle.GridColumnStyles.Add(withinToleranceCol);

				//Remove any other table styles and add the grid style to the GridStylesCollection.
				assetFundsGrid.TableStyles.Clear();				
				assetFundsGrid.TableStyles.Add(assetFundGridStyle);
				
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		/// <summary>
		/// Refresh all the data that will be displayed on this control
		/// </summary>
		public void RefreshData()
		{
			Cursor oldCursor = Cursor;
			Cursor = Cursors.WaitCursor;

			//Stop any GUI events affecting data whilst were setting up
			m_userEditing = false;

			//load the fund groups
			loadFundGroupCombo();

			//load all the asset funds
			loadAssetFunds();

			//Refresh the grid
			refreshDataGrid();

			//Reset flag so user triggered GUI events will affect data again
			m_userEditing = true;
			
			Cursor = oldCursor;
		}

		/// <summary>
		/// Populate the fund group combo
		/// </summary>
		private void loadFundGroupCombo()
		{
			T.E();
			try
			{
				//Use controller to get Fund Groups for this company
				FundGroupController fgController = new FundGroupController(m_connectionString);
				m_fundGroups = fgController.LoadFundGroupsByCompany(
					((UPDPrincipal) System.Threading.Thread.CurrentPrincipal).CompanyCode);
			}
			catch(DatabaseException ex)
			{
				ExceptionManager.Publish(ex);
				T.DumpException(ex);
				MessageBoxHelper.ShowError("AssetFundGroupLoadBody","FundGroupLoadTitle",ex);
				m_fundGroups = new FundGroupCollection();
				T.X();
				return;
			}

			//Remove individual fund groups
			for (int i = m_fundGroups.Count - 1; i >= 0; i--)
			{
				if (m_fundGroups[i] is IndividualFundGroup)
				{
					m_fundGroups.RemoveAt(i);
				}
			}			
			
			//Add an 'all fund groups option'
			FundGroup allFundGroups = FundGroupFactory.CreateFundGroup(FundGroupFactory.FundGroupTypes.Asset);
			allFundGroups.FullName = string.Format("All Asset Funds for {0}",
				((UPDPrincipal) System.Threading.Thread.CurrentPrincipal).CompanyCode);
			m_fundGroups.Insert(0, allFundGroups);

			//Get the fund groups and bind to the combo box
			fundGroupComboBox.DisplayMember = "FullName";
			fundGroupComboBox.ValueMember = "ID";
			fundGroupComboBox.DataSource = m_fundGroups;
			T.X();
		}

		/// <summary>
		/// Load the requested Asset Funds from the DB
		/// </summary>
		private void loadAssetFunds()
		{
			//Create a controller which will fetch the asset funds
			AssetFundController controller = new AssetFundController();
			
			//Get all the asset funds for the current company
			m_assetFunds = new AssetFundCollection(controller.LoadAssetFunds(m_connectionString,
				((UPDPrincipal)System.Threading.Thread.CurrentPrincipal).CompanyCode, false));

            for (int i=0; i<m_assetFunds.Count; i++)
            {
                AssetFund currentAssetFund = m_assetFunds[i];
                T.Log(" *********** ASSET FUND : " +  currentAssetFund.FullName + " **************");
                T.Log("Asset Fund Type = " + currentAssetFund.GetType());
                T.Log("Num Indices = " + currentAssetFund.WeightedMovements.Count);
                T.Log("Predicted AssetMovement = " + currentAssetFund.PredictedAssetMovement);
                for (int j=0; j<currentAssetFund.WeightedMovements.Count; j++)
                {
                    IWeightedMovement currentIndexMovement = currentAssetFund.WeightedMovements[j];
                    T.Log("Index [" + j + "] : Contribution = " + currentIndexMovement.CalculateMovement() + ", type = " + currentIndexMovement.GetType());
                }
                T.Log(Environment.NewLine);
            }
		}

		/// <summary>
		/// Apply any filters and display the data
		/// </summary>
		private void refreshDataGrid()
		{
			//clone the asset fund collection so removals caused by filter changes dont mean we need to reload the data
			AssetFundCollection assetFunds = m_assetFunds.Clone();

			for (int i = assetFunds.Count - 1; i >= 0; i--)
			{
				//Remove any funds that are not in the selected fund group
				if (!isAssetFundInFundGroup(assetFunds[i]))
				{
					assetFunds.RemoveAt(i);
				}
				else
				{
					//Apply any additional filters that have been set
					if (applyFilterCheckBox.Checked)
					{
						//Filter funds depending on settings
						bool showPasses = m_filters[0].Value;
						bool showFails = m_filters[1].Value;

						if (!showPasses || !showFails)
						{
							if ((!showPasses && assetFunds[i].WithinAssetMovementTolerance) ||
								(!showFails && !assetFunds[i].WithinAssetMovementTolerance))
							{
								assetFunds.RemoveAt(i);
							}
						}
					}
				}
			}

			//Display the asset funds on the grid (if there are any)
			if (assetFunds.Count > 0)
			{
				assetFundsGrid.BindToCustomCollection(assetFunds); // , typeof(AssetFund)
				assetFundsGrid.Visible = true;
			}
			else
			{
				assetFundsGrid.DataSource = null;
				assetFundsGrid.Visible = false;
			}

			//show asset fund counter label
			rowCountLabel.Text = String.Format("Asset Funds shown: {0} of {1}",
				assetFunds.Count, m_assetFunds.Count);
		}

		/// <summary>
		/// Return whether the passed asset fund is in the selected fund group
		/// </summary>
		/// <param name="af"></param>
		/// <returns></returns>
		private bool isAssetFundInFundGroup(AssetFund af)
		{
			bool retVal = true;

			if (fundGroupComboBox.SelectedItem == null)
			{
				retVal = false;
			}
			else if (((FundGroup)fundGroupComboBox.SelectedItem).ID != 0)
			{
				retVal = false;

				//TODO asset fund now links to a set of fund groups not just one
			 	// retVal = (af.FundGroupID == ((FundGroup)fundGroupComboBox.SelectedItem).ID);
			}
			
			return retVal;
		}

		#endregion

		#region GUI events

		/// <summary>
		/// Print the Grid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void printButton_Click(object sender, System.EventArgs e)
		{
			// Print set to landscape through the grid properties window
			assetFundsGrid.PrintPreview("Current Asset Fund Status");
		}

		/// <summary>
		/// Export the grid contents to file
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void exportButton_Click(object sender, System.EventArgs e)
		{
			string filename = "AssetFundStatus.csv";
			string returnFileName = Classes.ExportData.DisplayFilePicker(ref filename, "Export Data", "c:\temp", "All Text Files(*.csv)|(*.csv)|All files (*.*)|*.*", Classes.ExportData.FilePickerTypes.SaveDialog);

			if ( returnFileName != "" )
			{
				assetFundsGrid.Write( returnFileName, "HBOS.FS.AMP.UPD.WinUI.UserControls.AssetFundStatus.xslt");
				MessageBox.Show( String.Format( "{0} has been successfully exported." , returnFileName ) , "Export Data", MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
			
		}

		/// <summary>
		/// Refresh the data shown on the grid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void refreshButton_Click(object sender, System.EventArgs e)
		{
			RefreshData();
		}

		/// <summary>
		/// New fund group selected - refresh the display
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void fundGroupComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (m_userEditing)
			{
				//reload the data
				refreshDataGrid();
			}
		}

		/// <summary>
		/// Filter item changed - refresh the display
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void filterListBox_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
		{	
			//Set the filter
			((BooleanFilter)((CheckedListBox)sender).SelectedItem).Value = (e.NewValue == CheckState.Checked);
            
			// Now refresh the data grid
			refreshDataGrid();
		}

		/// <summary>
		/// User has changed whether to apply the additional filters - refresh the display
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void applyFilterCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			filterListBox.Enabled = applyFilterCheckBox.Checked;
			refreshDataGrid();
		}

		#endregion
	}
}
