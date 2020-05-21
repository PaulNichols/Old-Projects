using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Configuration;

using HBOS.FS.Common.ExceptionManagement;
using HBOS.FS.Support.Tex;

using HBOS.FS.AMP.ExceptionManagement;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.Funds;
using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.Security;
using HBOS.FS.AMP.UPD.WinUI.Helpers;

namespace HBOS.FS.AMP.UPD.WinUI.UserControls
{
	/// <summary>
	/// Summary description for MaintainTolerance.
	/// </summary>
	public class MaintainTolerance : System.Windows.Forms.UserControl
	{
		# region Controls

		private HBOS.FS.AMP.Windows.Controls.DataGrid fundsGrid;
		private HBOS.FS.AMP.Windows.Controls.HBOSTableStyle mainTableStyle;
		private HBOS.FS.AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn fullNameColumn;
		private System.Windows.Forms.DataGridTextBoxColumn lowerToleranceColumn;
		private System.Windows.Forms.DataGridTextBoxColumn upperToleranceColumn;

		private System.Windows.Forms.GroupBox assetFundGroupBox;
		private System.Windows.Forms.ComboBox assetFundComboBox;
		private System.Windows.Forms.Label priceIncreaseOnlyLabel;
		private System.Windows.Forms.Label assetMovementLabel;
		private System.Windows.Forms.Label lowerToleranceLabel;
		private System.Windows.Forms.Label upperToleranceLabel;
		private System.Windows.Forms.Button updateFundsButton;
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.CheckBox priceIncreaseOnlyCheckBox;
		private HBOS.FS.AMP.Windows.Controls.NumericTextBox assetMovementNumericTextBox;
		private HBOS.FS.AMP.Windows.Controls.NumericTextBox upperToleranceNumericTextBox;
		private HBOS.FS.AMP.Windows.Controls.NumericTextBox lowerToleranceNumericTextBox;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#endregion

		#region Variables

		// Non-GUI stuff
		private const decimal m_maximumToleranceValue = 3;
		private string m_cs = string.Empty;
		private string m_selectedAssetFundCode = string.Empty;
		private AssetFundController m_assetFundController;
		private FundController m_fundController;
		private AssetFundCollection m_currentAssetFunds;
		private string m_title = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).ProductName;
		private FundCollection m_funds_clone;

		private int m_CurrentRow = -1;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		public MaintainTolerance()
		{
			T.E();

			try
			{
				// This call is required by the Windows.Forms Form Designer.
				InitializeComponent();
                // Anchor the User Control.
                this.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom);

                // Set the properties of controls.
                this.fundsGrid.AllowNavigation = false;
                updateFundsButton.Enabled = false;
                cancelButton.Enabled = false;
                saveButton.Enabled = false;
                assetMovementNumericTextBox.Enabled = false;
                upperToleranceNumericTextBox.Enabled = false;
                lowerToleranceNumericTextBox.Enabled = false;
                priceIncreaseOnlyCheckBox.Enabled = false;
			}
			finally
			{
				T.X();
			}
		}

		#endregion

		#region Public Methods
        
		/// <summary>
		/// Load the control onto the form
		/// </summary>
		public void CustomInitialisation()
		{
			T.E();

			try
			{
				m_cs = ConfigurationSettings.AppSettings["ConnectionString"];

				// Get the currently selected company from the GUI principal thread
				UPDPrincipal updPrincipal = (UPDPrincipal) System.Threading.Thread.CurrentPrincipal;

				// Build collection objects...
				m_assetFundController = new AssetFundController();			// Get the asset funds 
				m_fundController = new FundController();					// Get the funds 

				this.loadAssetFundCombo(updPrincipal.CompanyCode);			// Load the asset funds into the list

				this.addDataGridStyles();									// Set the grid styles

				this.refreshFundGrid();

				// Size the Grid
				this.resizeGrid();
			}
			catch (System.Exception ex)
			{
				GUIExceptionHelper.LogAndDisplayException ("SystemError", "UnexpectedErrorTitle", ex);
			}
			finally
			{
				T.X();
			}
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Populate the asset fund combo
		/// </summary>
		private void loadAssetFundCombo(string companyCode)
		{
			T.E();

			try
			{
				// Load the asset funds for the selected company
				m_currentAssetFunds = m_assetFundController.LoadAssetFunds(m_cs, companyCode, true);

				// Get the asset funds and bind to the combo Box
				this.assetFundComboBox.Enabled = true;
				this.assetFundComboBox.DataSource = m_currentAssetFunds;
				this.assetFundComboBox.DisplayMember = "FullName";
				this.assetFundComboBox.ValueMember = "AssetFundCode";
			}
			finally
			{
				T.X();
			}
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
			T.E();

			try
			{
				// Create a new DataGridTableStyle and set MappingName.
				DataGridTableStyle tolerancesGridStyle = new DataGridTableStyle();
				tolerancesGridStyle.MappingName = "";
				tolerancesGridStyle.AlternatingBackColor = Color.WhiteSmoke;

				// Fund Code
				DataGridTextBoxColumn fundCodeColStyle = new DataGridTextBoxColumn();
				fundCodeColStyle.MappingName = "HiPortfoliocode";
				fundCodeColStyle.HeaderText = "Fund Code";
				fundCodeColStyle.Width = 100;
				fundCodeColStyle.ReadOnly = true;

				// Short Name
				DataGridTextBoxColumn shortNameColStyle = new DataGridTextBoxColumn();
				shortNameColStyle.MappingName = "ShortName";
				shortNameColStyle.HeaderText = "Short Name";
				shortNameColStyle.ReadOnly = true;

				// Asset Movement
				AMP.Windows.Controls.DataGridPercentageColumn  assetMovementColStyle = new AMP.Windows.Controls.DataGridPercentageColumn ();
				assetMovementColStyle.MappingName = "AssetMovementTolerance";
				assetMovementColStyle.HeaderText = "Asset Movement";
				assetMovementColStyle.Alignment = HorizontalAlignment.Right;
				assetMovementColStyle.Width = fundCodeColStyle.Width;
				assetMovementColStyle.DecimalPlaces = 5;
				assetMovementColStyle.ReadOnly = true;
				
				// Upper Tolerance
				AMP.Windows.Controls.DataGridPercentageColumn  upperToleranceColStyle = new AMP.Windows.Controls.DataGridPercentageColumn ();
				upperToleranceColStyle.MappingName = "UpperTolerance";
				upperToleranceColStyle.HeaderText = "Upper Tolerance";
				upperToleranceColStyle.Alignment = HorizontalAlignment.Right;
				upperToleranceColStyle.Width = fundCodeColStyle.Width;
				upperToleranceColStyle.DecimalPlaces = 5;
				
				// Lower Tolerance
				AMP.Windows.Controls.DataGridPercentageColumn  lowerToleranceColStyle = new AMP.Windows.Controls.DataGridPercentageColumn ();
				lowerToleranceColStyle.MappingName = "LowerTolerance";
				lowerToleranceColStyle.HeaderText = "Lower Tolerance";
				lowerToleranceColStyle.Alignment = HorizontalAlignment.Right;
				lowerToleranceColStyle.DecimalPlaces = 5;
				lowerToleranceColStyle.Width = fundCodeColStyle.Width;

				// Price Increase Only
				DataGridBoolColumn priceIncreaseOnlyColStyle = new DataGridBoolColumn();
				priceIncreaseOnlyColStyle.AllowNull = false;
				priceIncreaseOnlyColStyle.MappingName = "PriceIncreaseOnly";
				priceIncreaseOnlyColStyle.HeaderText = "Price Increase Only";
				priceIncreaseOnlyColStyle.Width = fundCodeColStyle.Width;

				// Remove any other table styles
				this.fundsGrid.TableStyles.Clear();
                
				// Add column styles to table style.
				tolerancesGridStyle.GridColumnStyles.Add(fundCodeColStyle);
				tolerancesGridStyle.GridColumnStyles.Add(shortNameColStyle);   
				tolerancesGridStyle.GridColumnStyles.Add(assetMovementColStyle);   
				tolerancesGridStyle.GridColumnStyles.Add(upperToleranceColStyle);   
				tolerancesGridStyle.GridColumnStyles.Add(lowerToleranceColStyle);   
				tolerancesGridStyle.GridColumnStyles.Add(priceIncreaseOnlyColStyle);   

				// Add the grid style to the GridStylesCollection.
				this.fundsGrid.TableStyles.Add(tolerancesGridStyle);
				//this.fundsGrid.CopyDefaultTableStyle( tolerancesGridStyle );
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// This routine will extract the fund collection from the grid and if it is dirty it will update the
		/// funds collection attached to the asset fund.
		/// </summary>
		private void saveFundCollection()
		{
			T.E();

			// Persist all items back to the DB when one or more existing items have 
			// changed or appended
			try
			{
				// Make sure there are no outstanding edits
				this.fundsGrid.CommitChanges();

				// Firstly, retrieve the modified funds collection from the grid.
				FundCollection updatedCollection = (FundCollection)this.fundsGrid.RetrieveUpdatedCustomCollection(); // typeof(OEICFund));
        
				// Are there any funds to save.
				if (updatedCollection.Count != 0)
				{
					bool validFlag = m_fundController.Update (m_cs, updatedCollection);

					if ( validFlag )
					{
						this.refreshFundGrid();
					}
					else
					{
						MessageBox.Show("Unable to Save Funds", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
				}
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// This routine fills the grid with fund information for the selected Asset Fund.
		/// </summary>
		private void refreshFundGrid()
		{
			T.E();

			try
			{
				string selectedAssetFundCode = ((AssetFund)assetFundComboBox.SelectedItem).AssetFundCode;

				FundCollection funds = m_fundController.LoadFundsByAssetFund(m_cs,selectedAssetFundCode); // , true );
				this.fundsGrid.BindToCustomCollection (funds ); //, typeof(OEICFund));
			
				// Set control availability.
				if ( fundsGrid.Count == 0)
				{
					updateFundsButton.Enabled = false;
					cancelButton.Enabled = false;
					saveButton.Enabled = false;
					assetMovementNumericTextBox.Enabled = false;
					upperToleranceNumericTextBox.Enabled = false;
					lowerToleranceNumericTextBox.Enabled = false;
					priceIncreaseOnlyCheckBox.Enabled = false;
				}
				else
				{
					updateFundsButton.Enabled = true;
					cancelButton.Enabled = true;
					saveButton.Enabled = true;
					assetMovementNumericTextBox.Enabled = true;
					upperToleranceNumericTextBox.Enabled = true;
					lowerToleranceNumericTextBox.Enabled = true;
					priceIncreaseOnlyCheckBox.Enabled = true;
				}

				// do not allow an appendrow...
				this.fundsGrid.AllowDelete = false;
				this.fundsGrid.AllowNew = false;

				// Clone the Funds so that they can be reverted to their non-committed stated if required.
				m_funds_clone = funds.Clone();

				// Set the initial grid values
				if (m_CurrentRow == -1)
				{
					m_CurrentRow = fundsGrid.CurrentRowIndex;
				}
				else
				{
					fundsGrid.CurrentRowIndex = m_CurrentRow;
				}
				cancelButton.Enabled = false;
				saveButton.Enabled = false;

			}
			finally
			{
				T.X();
			}
		}
		/// <summary>
		/// This routine resizes the columns in the grid for the current size of the user control.
		/// </summary>
		private void resizeGrid()
		{
			T.E();

			try
			{
				//int fixedColumnsWidth;
				GridColumnStylesCollection fundsGridColumns = this.fundsGrid.TableStyles[0].GridColumnStyles;

//				fixedColumnsWidth = fundsGridColumns["HiPortfoliocode"].Width +
//					fundsGridColumns["AssetMovementTolerance"].Width +
//					fundsGridColumns["UpperTolerance"].Width +
//					fundsGridColumns["LowerTolerance"].Width +
//					fundsGridColumns["PriceIncreaseOnly"].Width;
//				fundsGridColumns["ShortName"].Width = this.fundsGrid.Width - fixedColumnsWidth - 40;
			}
			finally
			{
				T.X();
			}
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			try
			{
				this.saveButton = new System.Windows.Forms.Button();
				this.fundsGrid = new HBOS.FS.AMP.Windows.Controls.DataGrid();
				this.mainTableStyle = new HBOS.FS.AMP.Windows.Controls.HBOSTableStyle();
				this.fullNameColumn = new HBOS.FS.AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn();
				this.lowerToleranceColumn = new System.Windows.Forms.DataGridTextBoxColumn();
				this.upperToleranceColumn = new System.Windows.Forms.DataGridTextBoxColumn();
				this.assetFundGroupBox = new System.Windows.Forms.GroupBox();
				this.lowerToleranceNumericTextBox = new HBOS.FS.AMP.Windows.Controls.NumericTextBox();
				this.upperToleranceNumericTextBox = new HBOS.FS.AMP.Windows.Controls.NumericTextBox();
				this.assetMovementNumericTextBox = new HBOS.FS.AMP.Windows.Controls.NumericTextBox();
				this.priceIncreaseOnlyCheckBox = new System.Windows.Forms.CheckBox();
				this.updateFundsButton = new System.Windows.Forms.Button();
				this.assetFundComboBox = new System.Windows.Forms.ComboBox();
				this.priceIncreaseOnlyLabel = new System.Windows.Forms.Label();
				this.assetMovementLabel = new System.Windows.Forms.Label();
				this.lowerToleranceLabel = new System.Windows.Forms.Label();
				this.upperToleranceLabel = new System.Windows.Forms.Label();
				this.cancelButton = new System.Windows.Forms.Button();
				((System.ComponentModel.ISupportInitialize)(this.fundsGrid)).BeginInit();
				this.assetFundGroupBox.SuspendLayout();
				this.SuspendLayout();
				// 
				// saveButton
				// 
				this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
				this.saveButton.Location = new System.Drawing.Point(544, 432);
				this.saveButton.Name = "saveButton";
				this.saveButton.TabIndex = 2;
				this.saveButton.Text = "&Save";
				this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
				// 
				// fundsGrid
				// 
				this.fundsGrid.AlternatingBackColor = System.Drawing.SystemColors.Window;
				this.fundsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
					| System.Windows.Forms.AnchorStyles.Left) 
					| System.Windows.Forms.AnchorStyles.Right)));
				this.fundsGrid.BackColor = System.Drawing.SystemColors.Window;
				this.fundsGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
				this.fundsGrid.CaptionBackColor = System.Drawing.SystemColors.ActiveCaption;
				this.fundsGrid.CaptionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
				this.fundsGrid.DataMember = "";
				this.fundsGrid.FlatMode = false;
				this.fundsGrid.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				this.fundsGrid.ForeColor = System.Drawing.SystemColors.WindowText;
				this.fundsGrid.GridLineColor = System.Drawing.SystemColors.Control;
				this.fundsGrid.HeaderBackColor = System.Drawing.SystemColors.Control;
				this.fundsGrid.HeaderFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				this.fundsGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
				this.fundsGrid.LinkColor = System.Drawing.SystemColors.HotTrack;
				this.fundsGrid.Location = new System.Drawing.Point(8, 128);
				this.fundsGrid.Name = "fundsGrid";
				this.fundsGrid.ParentRowsBackColor = System.Drawing.SystemColors.Control;
				this.fundsGrid.ParentRowsForeColor = System.Drawing.SystemColors.WindowText;
				//this.fundsGrid.PrintColumnWidths = null;
				this.fundsGrid.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
				this.fundsGrid.SelectionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
				this.fundsGrid.Size = new System.Drawing.Size(688, 296);
				this.fundsGrid.TabIndex = 1;
				this.fundsGrid.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
																									this.mainTableStyle});
				this.fundsGrid.RowChanged += new HBOS.FS.AMP.Windows.Controls.DataGrid.RowChangedDelegate(this.fundsGrid_RowChanged);
				// 
				// mainTableStyle
				// 
				this.mainTableStyle.DataGrid = this.fundsGrid;
				this.mainTableStyle.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
																												this.fullNameColumn,
																												this.lowerToleranceColumn,
																												this.upperToleranceColumn});
				this.mainTableStyle.HeaderForeColor = System.Drawing.SystemColors.ControlText;
				this.mainTableStyle.MappingName = "";
				// 
				// fullNameColumn
				// 
				this.fullNameColumn.Format = "";
				this.fullNameColumn.FormatInfo = null;
				this.fullNameColumn.HeaderText = "Full Name";
				this.fullNameColumn.MappingName = "FullName";
				this.fullNameColumn.ToolTipProperty = "";
				this.fullNameColumn.Width = 75;
				// 
				// lowerToleranceColumn
				// 
				this.lowerToleranceColumn.Format = "";
				this.lowerToleranceColumn.FormatInfo = null;
				this.lowerToleranceColumn.HeaderText = "Lower Tolerance";
				this.lowerToleranceColumn.MappingName = "LowerTolerance";
				this.lowerToleranceColumn.Width = 75;
				// 
				// upperToleranceColumn
				// 
				this.upperToleranceColumn.Format = "";
				this.upperToleranceColumn.FormatInfo = null;
				this.upperToleranceColumn.HeaderText = "Upper Tolerance";
				this.upperToleranceColumn.MappingName = "UpperTolerance";
				this.upperToleranceColumn.Width = 75;
				// 
				// assetFundGroupBox
				// 
				this.assetFundGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
					| System.Windows.Forms.AnchorStyles.Right)));
				this.assetFundGroupBox.Controls.Add(this.lowerToleranceNumericTextBox);
				this.assetFundGroupBox.Controls.Add(this.upperToleranceNumericTextBox);
				this.assetFundGroupBox.Controls.Add(this.assetMovementNumericTextBox);
				this.assetFundGroupBox.Controls.Add(this.priceIncreaseOnlyCheckBox);
				this.assetFundGroupBox.Controls.Add(this.updateFundsButton);
				this.assetFundGroupBox.Controls.Add(this.assetFundComboBox);
				this.assetFundGroupBox.Controls.Add(this.priceIncreaseOnlyLabel);
				this.assetFundGroupBox.Controls.Add(this.assetMovementLabel);
				this.assetFundGroupBox.Controls.Add(this.lowerToleranceLabel);
				this.assetFundGroupBox.Controls.Add(this.upperToleranceLabel);
				this.assetFundGroupBox.Location = new System.Drawing.Point(8, 8);
				this.assetFundGroupBox.Name = "assetFundGroupBox";
				this.assetFundGroupBox.Size = new System.Drawing.Size(688, 112);
				this.assetFundGroupBox.TabIndex = 0;
				this.assetFundGroupBox.TabStop = false;
				this.assetFundGroupBox.Text = "Asset Fund";
				// 
				// lowerToleranceNumericTextBox
				// 
				this.lowerToleranceNumericTextBox.DecimalPlaces = 5;
				this.lowerToleranceNumericTextBox.Location = new System.Drawing.Point(280, 72);
				this.lowerToleranceNumericTextBox.MaxLength = 10;
				this.lowerToleranceNumericTextBox.MaxValue = new System.Decimal(new int[] {
																							100,
																							0,
																							0,
																							0});
				this.lowerToleranceNumericTextBox.MinValue = new System.Decimal(new int[] {
																							0,
																							0,
																							0,
																							0});
				this.lowerToleranceNumericTextBox.Name = "lowerToleranceNumericTextBox";
				this.lowerToleranceNumericTextBox.TabIndex = 3;
				this.lowerToleranceNumericTextBox.Text = "";
				this.lowerToleranceNumericTextBox.ValueMultiplier = new System.Decimal(new int[] {
																									100,
																									0,
																									0,
																									0});
				// 
				// upperToleranceNumericTextBox
				// 
				this.upperToleranceNumericTextBox.DecimalPlaces = 5;
				this.upperToleranceNumericTextBox.Location = new System.Drawing.Point(144, 72);
				this.upperToleranceNumericTextBox.MaxLength = 10;
				this.upperToleranceNumericTextBox.MaxValue = new System.Decimal(new int[] {
																							100,
																							0,
																							0,
																							0});
				this.upperToleranceNumericTextBox.MinValue = new System.Decimal(new int[] {
																							0,
																							0,
																							0,
																							0});
				this.upperToleranceNumericTextBox.Name = "upperToleranceNumericTextBox";
				this.upperToleranceNumericTextBox.TabIndex = 2;
				this.upperToleranceNumericTextBox.Text = "";
				this.upperToleranceNumericTextBox.ValueMultiplier = new System.Decimal(new int[] {
																									100,
																									0,
																									0,
																									0});
				// 
				// assetMovementNumericTextBox
				// 
				this.assetMovementNumericTextBox.DecimalPlaces = 5;
				this.assetMovementNumericTextBox.Location = new System.Drawing.Point(16, 72);
				this.assetMovementNumericTextBox.MaxLength = 10;
				this.assetMovementNumericTextBox.MaxValue = new System.Decimal(new int[] {
																							100,
																							0,
																							0,
																							0});
				this.assetMovementNumericTextBox.MinValue = new System.Decimal(new int[] {
																							0,
																							0,
																							0,
																							0});
				this.assetMovementNumericTextBox.Name = "assetMovementNumericTextBox";
				this.assetMovementNumericTextBox.TabIndex = 1;
				this.assetMovementNumericTextBox.Text = "";
				this.assetMovementNumericTextBox.ValueMultiplier = new System.Decimal(new int[] {
																									100,
																									0,
																									0,
																									0});
				// 
				// priceIncreaseOnlyCheckBox
				// 
				this.priceIncreaseOnlyCheckBox.Checked = true;
				this.priceIncreaseOnlyCheckBox.CheckState = System.Windows.Forms.CheckState.Indeterminate;
				this.priceIncreaseOnlyCheckBox.Location = new System.Drawing.Point(464, 72);
				this.priceIncreaseOnlyCheckBox.Name = "priceIncreaseOnlyCheckBox";
				this.priceIncreaseOnlyCheckBox.Size = new System.Drawing.Size(16, 24);
				this.priceIncreaseOnlyCheckBox.TabIndex = 4;
				this.priceIncreaseOnlyCheckBox.ThreeState = true;
				// 
				// updateFundsButton
				// 
				this.updateFundsButton.Location = new System.Drawing.Point(544, 72);
				this.updateFundsButton.Name = "updateFundsButton";
				this.updateFundsButton.Size = new System.Drawing.Size(104, 23);
				this.updateFundsButton.TabIndex = 5;
				this.updateFundsButton.Text = "&Update All Funds";
				this.updateFundsButton.Click += new System.EventHandler(this.updateFundsButton_Click);
				// 
				// assetFundComboBox
				// 
				this.assetFundComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
				this.assetFundComboBox.Location = new System.Drawing.Point(16, 24);
				this.assetFundComboBox.Name = "assetFundComboBox";
				this.assetFundComboBox.Size = new System.Drawing.Size(224, 21);
				this.assetFundComboBox.TabIndex = 0;
				this.assetFundComboBox.SelectionChangeCommitted += new System.EventHandler(this.assetFundComboBox_SelectionChangeCommitted);
				// 
				// priceIncreaseOnlyLabel
				// 
				this.priceIncreaseOnlyLabel.AutoSize = true;
				this.priceIncreaseOnlyLabel.Location = new System.Drawing.Point(426, 56);
				this.priceIncreaseOnlyLabel.Name = "priceIncreaseOnlyLabel";
				this.priceIncreaseOnlyLabel.Size = new System.Drawing.Size(103, 16);
				this.priceIncreaseOnlyLabel.TabIndex = 7;
				this.priceIncreaseOnlyLabel.Text = "Price Increase Only";
				// 
				// assetMovementLabel
				// 
				this.assetMovementLabel.AutoSize = true;
				this.assetMovementLabel.Location = new System.Drawing.Point(16, 56);
				this.assetMovementLabel.Name = "assetMovementLabel";
				this.assetMovementLabel.Size = new System.Drawing.Size(88, 16);
				this.assetMovementLabel.TabIndex = 1;
				this.assetMovementLabel.Text = "Asset Movement";
				// 
				// lowerToleranceLabel
				// 
				this.lowerToleranceLabel.AutoSize = true;
				this.lowerToleranceLabel.Location = new System.Drawing.Point(280, 56);
				this.lowerToleranceLabel.Name = "lowerToleranceLabel";
				this.lowerToleranceLabel.Size = new System.Drawing.Size(85, 16);
				this.lowerToleranceLabel.TabIndex = 5;
				this.lowerToleranceLabel.Text = "Lower tolerance";
				// 
				// upperToleranceLabel
				// 
				this.upperToleranceLabel.AutoSize = true;
				this.upperToleranceLabel.Location = new System.Drawing.Point(144, 56);
				this.upperToleranceLabel.Name = "upperToleranceLabel";
				this.upperToleranceLabel.Size = new System.Drawing.Size(85, 16);
				this.upperToleranceLabel.TabIndex = 3;
				this.upperToleranceLabel.Text = "Upper tolerance";
				// 
				// cancelButton
				// 
				this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
				this.cancelButton.Location = new System.Drawing.Point(624, 432);
				this.cancelButton.Name = "cancelButton";
				this.cancelButton.TabIndex = 3;
				this.cancelButton.Text = "&Cancel";
				this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
				// 
				// MaintainTolerance
				// 
				this.Controls.Add(this.cancelButton);
				this.Controls.Add(this.assetFundGroupBox);
				this.Controls.Add(this.saveButton);
				this.Controls.Add(this.fundsGrid);
				this.Name = "MaintainTolerance";
				this.Size = new System.Drawing.Size(704, 464);
				this.Resize += new System.EventHandler(this.MaintainTolerance_Resize);
				((System.ComponentModel.ISupportInitialize)(this.fundsGrid)).EndInit();
				this.assetFundGroupBox.ResumeLayout(false);
				this.ResumeLayout(false);
			}
			catch (System.Exception ex)
			{
				GUIExceptionHelper.LogAndDisplayException ("SystemError", "UnexpectedErrorTitle", ex);
			}


        }
		#endregion

		#region Control events

		/// <summary>
		/// User has clicked the update button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void updateFundsButton_Click(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				// If the tolerances entered are valid, apply to the rows.
				if ( this.validateEntry() )
				{
					try
					{
						this.fundsGrid.TableStyles[0].GridColumnStyles[ "AssetMovementTolerance" ].ReadOnly = false;

						// For each fund
						for (int index = 0; index < this.fundsGrid.Count; index++)
						{
							// If Asset Movement provided, use it
							if ( assetMovementNumericTextBox.Text != String.Empty )
							{
								this.fundsGrid.SetValue(index, "AssetMovementTolerance", assetMovementNumericTextBox.Value);
							}

							// If Upper tolerance provided, use it
							if ( upperToleranceNumericTextBox.Text != String.Empty )
							{
								this.fundsGrid.SetValue(index, "UpperTolerance", upperToleranceNumericTextBox.Value);
							}

							// If Lower Tolerance provided, use it
							if ( lowerToleranceNumericTextBox.Text != String.Empty )
							{
								this.fundsGrid.SetValue(index, "LowerTolerance", lowerToleranceNumericTextBox.Value );
							}

							// If Price Increaes provided, use it
							if ( priceIncreaseOnlyCheckBox.CheckState != CheckState.Indeterminate )
							{
								this.fundsGrid.SetValue(index, "PriceIncreaseOnly", priceIncreaseOnlyCheckBox.Checked);
							}						
						}
						this.fundsGrid.CommitChanges();					
					}
					catch (FormatException ex)
					{
						GUIExceptionHelper.LogAndDisplaySaveException ("Asset Funds", ex);
					}
					cancelButton.Enabled = true;
					saveButton.Enabled = true;
				}
			}
			catch (System.Exception ex)
			{
				GUIExceptionHelper.LogAndDisplaySaveException ("funds", ex);
			}
			finally
			{
				this.fundsGrid.TableStyles[0].GridColumnStyles[ "AssetMovementTolerance" ].ReadOnly = true;

				T.X();
			}
		}

		/// <summary>
		/// Some data has changed so the Save and Cancel buttons must be enabled
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void fundsGrid_RowChanged(object sender, HBOS.FS.AMP.Windows.Controls.CellEventArgs e)
		{
			T.E();

			try
			{
				saveButton.Enabled = true;
				cancelButton.Enabled = true;
			}
			catch (System.Exception ex)
			{
				GUIExceptionHelper.LogAndDisplayException ("SystemError", "UnexpectedErrorTitle", ex);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// This routine validates the screen entry.
		/// </summary>
		private bool validateEntry()
		{
			T.E();

			try
			{
				bool maxFactorWarning = true;
				bool returnValue = true;

				// Validate the Asset Movement
				if ( assetMovementNumericTextBox.Text != String.Empty )
				{
					if ( this.validateTolerance(assetMovementNumericTextBox.Value,"Asset Movement", ref maxFactorWarning) == false )
					{
						assetMovementNumericTextBox.Focus();
						return false;
					}
				}

				// Validate the upper tolerance
				if ( upperToleranceNumericTextBox.Text != String.Empty )
				{
					maxFactorWarning = true;
					if ( this.validateTolerance( upperToleranceNumericTextBox.Value,"Upper", ref maxFactorWarning) == false )
					{
						upperToleranceNumericTextBox.Focus();
						return false;
					}
				}

				//	Validate the lower tolerance
				if ( lowerToleranceNumericTextBox.Text != String.Empty )
				{
					maxFactorWarning = true;
					if ( this.validateTolerance( lowerToleranceNumericTextBox.Value,"Lower", ref maxFactorWarning) == false )
					{
						lowerToleranceNumericTextBox.Focus();
						return false;
					}
				}

				return returnValue;
			}
			finally
			{
				T.X();
			}

		}

		/// <summary>
		/// Validate the tolerances
		/// </summary>
		/// <param name="toleranceValue"></param>
		/// <param name="toleranceText"></param>
		/// <param name="maxFactorWarning"></param>
		/// <returns></returns>
		private bool validateTolerance(decimal toleranceValue, string toleranceText, ref bool maxFactorWarning)
		{
			T.E();

			try
			{
				bool returnValue = true;

				// Is the value bigger than the maximum allowed
				if ( maxFactorWarning && ( toleranceValue > m_maximumToleranceValue ) )
				{
					if (MessageBox.Show(toleranceText + " Tolerance is greater than " + m_maximumToleranceValue.ToString() + ". Do you wish to continue?",m_title,MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation) == DialogResult.No)
					{
						returnValue = false;
					}
					else
					{
						maxFactorWarning = false;
					}
				}

				return returnValue;
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// User has pressed the save button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void saveButton_Click(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				bool maxFactorWarning = true;
				bool validData = true;

				for( int rowNumber = 0 ; rowNumber  < fundsGrid.Count ; rowNumber ++ )
				{
					decimal assetMovement = (decimal)fundsGrid.GetValue( rowNumber , "AssetMovementTolerance" );
					decimal upperTolerance = (decimal)fundsGrid.GetValue( rowNumber , "UpperTolerance" );
					decimal lowerTolerance = (decimal)fundsGrid.GetValue( rowNumber , "LowerTolerance" );

					// Validate the asset Movement
					if ( this.validateTolerance( assetMovement , "Asset Movement", ref maxFactorWarning) == false )
					{
						this.fundsGrid.UnSelect(rowNumber);
						this.fundsGrid.Select();
						fundsGrid.CurrentCell = new DataGridCell(rowNumber, 2);
						validData = false;
						break;
					}

					// Validate the upper tolerance
					if ( this.validateTolerance( upperTolerance , "Upper", ref maxFactorWarning) == false )
					{
						this.fundsGrid.UnSelect(rowNumber);
						this.fundsGrid.Select();
						fundsGrid.CurrentCell = new DataGridCell(rowNumber, 3);
						validData = false;
						break;
					}

					// Validate the lower tolerance					
					if ( this.validateTolerance( lowerTolerance , "Lower", ref maxFactorWarning) == false )
					{
						this.fundsGrid.UnSelect(rowNumber);
						this.fundsGrid.Select();
						fundsGrid.CurrentCell = new DataGridCell(rowNumber, 4);
						validData = false;
						break;
					}

					if ( lowerTolerance > upperTolerance )
					{
						this.fundsGrid.UnSelect(rowNumber);
						this.fundsGrid.Select();
						fundsGrid.CurrentCell = new DataGridCell(rowNumber, 4);
						validData = false;
						MessageBox.Show( String.Format( "Upper Tolerance of {0} is less than the Lower Tolerance of {1}" , upperTolerance , lowerTolerance ) , "Validation Error" , MessageBoxButtons.OK , MessageBoxIcon.Error );
						break;
					}
				}

				// If the data is valid, save it!
				if ( validData )
				{
					this.saveFundCollection();
				}
			}
			catch (System.Exception ex)
			{
				GUIExceptionHelper.LogAndDisplaySaveException ("funds", ex);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// User has changed selected asset fund
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void assetFundComboBox_SelectionChangeCommitted(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				m_CurrentRow = -1;
				this.refreshFundGrid();
			}
			catch (System.Exception ex)
			{
				GUIExceptionHelper.LogAndDisplayException ("SystemError", "UnexpectedErrorTitle", ex);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// User has resized the control
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MaintainTolerance_Resize(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				this.resizeGrid();
			}
			catch (System.Exception ex)
			{
				GUIExceptionHelper.LogAndDisplayException ("SystemError", "UnexpectedErrorTitle", ex);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// User has pressed the Cancel button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelButton_Click(object sender, System.EventArgs e)
		{
			T.E();

			try
			{
				if (MessageBox.Show("This will cancel all non-saved Fund changes. Are you sure you wish to continue?",m_title,MessageBoxButtons.YesNo,MessageBoxIcon.Information) == DialogResult.Yes )
				{
					this.refreshFundGrid();
				}
			}
			finally
			{
				T.X();
			}
		}

		#endregion

		/// <summary>
		/// Sets up the form on loading.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		protected override void OnLoad(EventArgs e)
        {
            T.E();
            try
            {
                base.OnLoad (e);

                // Scaling
                float scaleWidth = Form.GetAutoScaleSize(this.Font).Width;
                float adjustment = scaleWidth / 5.32F;
		
                assetMovementNumericTextBox.Left = assetMovementLabel.Left;
                lowerToleranceNumericTextBox.Left = lowerToleranceLabel.Left;
                upperToleranceNumericTextBox.Left = upperToleranceLabel.Left;

		        updateFundsButton.Left = priceIncreaseOnlyLabel.Left + priceIncreaseOnlyLabel.Width + 6;
                updateFundsButton.Width = (int)((float)updateFundsButton.Width * adjustment);

            
            }
			catch (System.Exception ex)
			{
				GUIExceptionHelper.LogAndDisplayException ("SystemError", "UnexpectedErrorTitle", ex);
			}
			finally
            {
                T.X();
            }

        }


	}
}

