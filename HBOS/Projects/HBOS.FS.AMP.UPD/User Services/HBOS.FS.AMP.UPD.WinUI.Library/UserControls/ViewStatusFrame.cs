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
using HBOS.FS.AMP.UPD.Types.Factors;
using HBOS.FS.AMP.UPD.Types.FundGroups;
using HBOS.FS.AMP.UPD.WinUI.Classes;

using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.UserControls
{
	/// <summary>
	/// Summary description for ViewStatusFrame.
	/// </summary>
	public class ViewStatusFrame : System.Windows.Forms.UserControl
	{

		#region Locals

		private FundCollection m_fundsFiltered = new FundCollection();	//Required to display properties
		private FundCollection m_funds = new FundCollection();

		#endregion 

		private HBOS.FS.AMP.Windows.Controls.DataGrid fundsGrid;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// 
		/// </summary>
		public ViewStatusFrame()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

			//set everything up and load the data
			loadForm();
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

		
		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.fundsGrid = new HBOS.FS.AMP.Windows.Controls.DataGrid();
			((System.ComponentModel.ISupportInitialize)(this.fundsGrid)).BeginInit();
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
			this.fundsGrid.Location = new System.Drawing.Point(0, 48);
			this.fundsGrid.Name = "fundsGrid";
			this.fundsGrid.ParentRowsBackColor = System.Drawing.Color.White;
			this.fundsGrid.PrintColumnHeadingFont = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
			this.fundsGrid.PrintColumnSettings = null;
			this.fundsGrid.PrintPageHeadingFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.fundsGrid.PrintStandardFont = new System.Drawing.Font("Arial", 8F);
			this.fundsGrid.ReadOnly = true;
			this.fundsGrid.SelectionBackColor = System.Drawing.Color.Navy;
			this.fundsGrid.Size = new System.Drawing.Size(704, 232);
			this.fundsGrid.TabIndex = 6;
			this.fundsGrid.TabStop = false;
			// 
			// ViewStatusFrame
			// 
			this.Controls.Add(this.fundsGrid);
			this.Name = "ViewStatusFrame";
			this.Size = new System.Drawing.Size(712, 288);
			((System.ComponentModel.ISupportInitialize)(this.fundsGrid)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void refreshDataGrid()
		{          
			T.E();
			// Apply filter to funds collection
			buildFilteredCollection();

			if (m_fundsFiltered.Count > 0)
			{
				//Bind collection to the grid
				fundsGrid.BindToCustomCollection(m_fundsFiltered); // , typeof(Fund)
//				propertiesButton.Enabled = true;
				fundsGrid.Visible = true;
			}
			else
			{
				//hide grid
				fundsGrid.DataSource = null;
				fundsGrid.Visible = false;
			//	propertiesButton.Enabled = false;
			}   
			//rowCountLabel.Text = String.Format("Funds shown: {0} of {1}", m_fundsFiltered.Count, m_funds.Count);
			T.X();
		}


		/// <summary>
		/// Load default data and settings for the form
		/// </summary>
		private void loadForm()
		{
			T.E();
			//populateFilters();			// Populate the items in the filter box
			createGridStyles();         // Set the column styles for the funds grid
			RefreshData();				// Refresh the funds data and the grid
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
		/// Refresh all the data that will be displayed on this control
		/// </summary>
		public void RefreshData()
		{
			T.E();
			Cursor oldCursor = Cursor;
			Cursor = Cursors.WaitCursor;

			//DateTime start = System.DateTime.Now;

			//Stop any GUI events affecting data whilst were setting up
		//	m_userEditing = false;
			
			// Get the currently selected company from the GUI principal thread
			string companyCode = ((UPDPrincipal)System.Threading.Thread.CurrentPrincipal).CompanyCode;

			//Get the connection string
			string connectionString = ConfigurationSettings.AppSettings["ConnectionString"];

			T.Log("Load funds for current company");
			//Load all the funds for the currently selected company
			FundController fController = new FundController(connectionString);
			m_funds = fController.LoadForCompany(companyCode);
			
            for (int i=0; i<m_funds.Count; i++)
            {
                Fund currentFund = m_funds[i];
                T.Log(" *********** FUND : " +  currentFund.FullName + " **************");
                T.Log("Fund Type = " + currentFund.GetType());
                T.Log("Num Indices = " + currentFund.ParentAssetFund.WeightedMovements.Count);
                T.Log("Predicted AssetMovement (fund) = " + currentFund.AssetMovement);
                T.Log("Predicted AssetMovement (Asset Fund) = " + currentFund.ParentAssetFund.PredictedAssetMovement);
                T.Log("Num Factors = " + currentFund.Factors.Count);

                for (int j=0; j<currentFund.Factors.Count; j++)
                {
                    Factor currentFactor = currentFund.Factors[j];
                    T.Log("Factor [" + j + "] : Contribution = " + currentFactor.CalculateEffect() + ", type = " + currentFactor.GetType());
                }
                T.Log(Environment.NewLine);
            }


			//Refresh the grid
			refreshDataGrid();

			//Reset flag so user triggered GUI events will affect data again
		//	m_userEditing = true;
			
			Cursor = oldCursor;

			//DateTime end = DateTime.Now;
			//MessageBox.Show(string.Format("Time taken: {0}", end - start));
		}


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

//				//Reference the current fund
				Fund currentFund = (Fund)m_funds[i];
//
//				//Ignore funds that are not in the current fund group (if "all" not set)
//				if (isFundInFundGroup(currentFund))
//				{
//					//Ignore funds that are not in the current asset fund (if "all" not set)
//					if (isFundInAssetFund(currentFund))
//					{
//						//Check whether user wants unfiltered collection 
//						if (!applyFilterCheckBox.Checked)
//						{
							addFundToCollection = true;					
//						}
//						else
//						{
//							//Step through all filters looking for any that are turned on
//							for (int f = m_filters.Count - 1; f >= 0; f--)
//							{
//								if (!addFundToCollection)
//								{
//									if (m_filters[f].Value)
//									{
//										//A filter is turned on - check which it is and whether fund matches status
//										switch (f)
//										{
//											case 0:
//												addFundToCollection = isFirstLevelAuthorised(currentFund);
//												break;
//											case 1:
//												addFundToCollection = isSecondLevelAuthorised(currentFund);
//												break;
//											case 2:
//												addFundToCollection = isNotSecondLevelAuthorised(currentFund);
//												break;
//											case 3:
//												addFundToCollection = isNotFirstOrSecondLevelAuthorised(currentFund);
//												break;
//											case 4:
//												addFundToCollection = isSecondLevelAuthorisedButNotFirst(currentFund);
//												break;
//											case 5:
//												addFundToCollection = isReleased(currentFund);
//												break;
//											case 6:
//												addFundToCollection = isDistributed(currentFund);
//												break;
//											case 7:
//												addFundToCollection = isAssetMovementOutsideTolerance(currentFund);
//												break;
//											case 8:
//												addFundToCollection = isPriceOutsideTolerance(currentFund);
//												break;
//											default:
//												addFundToCollection = false;
//												break;
//										}
//									}
//								}
//							}
//						}
//					}
//				}

				// Finally, add fund to filtered fund collection
				if ( addFundToCollection )
				{
					m_fundsFiltered.Add(currentFund);
				}
			}
			T.X();
		}
	}
}
