using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using HBOS.FS.AMP.UPD.Security;
using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Types.FundGroups;
using HBOS.FS.AMP.UPD.Types.Funds;
using HBOS.FS.AMP.ExceptionManagement;

using HBOS.FS.Common.ExceptionManagement;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.UserControls
{
	/// <summary>
	/// Summary description for Release.
	/// </summary>
	public class Release : System.Windows.Forms.UserControl
	{
		#region Local variables

		private System.Windows.Forms.Label fundGroupLabel;
		private System.Windows.Forms.ComboBox fundGroupComboBox;
		private HBOS.FS.AMP.Windows.Controls.DataGrid fundsGrid;
		private HBOS.FS.AMP.Windows.Controls.HBOSTableStyle defaultDisplay;
		private HBOS.FS.AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn fundsGridFundName;
		private HBOS.FS.AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn fundsGridAssetFundName;
		private HBOS.FS.AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn fundsGridPrice;
		private HBOS.FS.AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn fundsGridPredictedPrice;
		private HBOS.FS.AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn fundsgridPriceMovement;
		private HBOS.FS.AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn fundsGridPredictedPriceMovement;
		private HBOS.FS.AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn fundsGridPriceVariance;
		private HBOS.FS.AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn fundsGridAssetMovement;
		private HBOS.FS.AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn fundsGridStatus;
		private HBOS.FS.AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn fundsGridChangeTime;
		private HBOS.FS.AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn fundsGridAssetTolerance;
		private HBOS.FS.AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn fundsGridPriceTolerance;
		private HBOS.FS.AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn fundsgridAssetUnitPrice;
		private HBOS.FS.AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn fundsGridPredictedAssetMovement;
		private HBOS.FS.AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn fundsGridAssetMovementVariance;
		private System.Windows.Forms.Button submitButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button releaseAllButton;
		private System.Windows.Forms.Button unreleaseAllButton;
		private System.Windows.Forms.Button printButton;
		private System.Windows.Forms.Button exportButton;

		private string m_connectionString = string.Empty;
		private string m_companyCode = string.Empty;
		private bool m_loaded = false;
		private bool m_permissionUndoRelease = true;
		private bool m_permissionRelease = true;
		private FundCollection m_restoreFunds = null;
		private HBOS.FS.AMP.Windows.Controls.DataGridBool1ClickColumn fundsGridRelease;


		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		public Release()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

		#endregion

		#region Destructor

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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Release));
            this.fundGroupLabel = new System.Windows.Forms.Label();
            this.fundGroupComboBox = new System.Windows.Forms.ComboBox();
            this.fundsGrid = new HBOS.FS.AMP.Windows.Controls.DataGrid();
            this.defaultDisplay = new HBOS.FS.AMP.Windows.Controls.HBOSTableStyle();
            this.fundsGridFundName = new HBOS.FS.AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn();
            this.fundsGridAssetFundName = new HBOS.FS.AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn();
            this.fundsGridRelease = new HBOS.FS.AMP.Windows.Controls.DataGridBool1ClickColumn();
            this.fundsGridPrice = new HBOS.FS.AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn();
            this.fundsGridPredictedPrice = new HBOS.FS.AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn();
            this.fundsgridPriceMovement = new HBOS.FS.AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn();
            this.fundsGridPredictedPriceMovement = new HBOS.FS.AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn();
            this.fundsGridPriceVariance = new HBOS.FS.AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn();
            this.fundsGridPriceTolerance = new HBOS.FS.AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn();
            this.fundsgridAssetUnitPrice = new HBOS.FS.AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn();
            this.fundsGridAssetMovement = new HBOS.FS.AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn();
            this.fundsGridPredictedAssetMovement = new HBOS.FS.AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn();
            this.fundsGridAssetMovementVariance = new HBOS.FS.AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn();
            this.fundsGridAssetTolerance = new HBOS.FS.AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn();
            this.fundsGridStatus = new HBOS.FS.AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn();
            this.fundsGridChangeTime = new HBOS.FS.AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn();
            this.submitButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.releaseAllButton = new System.Windows.Forms.Button();
            this.unreleaseAllButton = new System.Windows.Forms.Button();
            this.printButton = new System.Windows.Forms.Button();
            this.exportButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.fundsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // fundGroupLabel
            // 
            this.fundGroupLabel.AutoSize = true;
            this.fundGroupLabel.CausesValidation = false;
            this.fundGroupLabel.Location = new System.Drawing.Point(8, 8);
            this.fundGroupLabel.Name = "fundGroupLabel";
            this.fundGroupLabel.Size = new System.Drawing.Size(68, 16);
            this.fundGroupLabel.TabIndex = 0;
            this.fundGroupLabel.Text = "Fund Group:";
            // 
            // fundGroupComboBox
            // 
            this.fundGroupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fundGroupComboBox.Location = new System.Drawing.Point(96, 8);
            this.fundGroupComboBox.Name = "fundGroupComboBox";
            this.fundGroupComboBox.Size = new System.Drawing.Size(176, 21);
            this.fundGroupComboBox.TabIndex = 1;
            this.fundGroupComboBox.SelectedIndexChanged += new System.EventHandler(this.fundGroupComboBox_SelectedIndexChanged);
            // 
            // fundsGrid
            // 
            this.fundsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.fundsGrid.DataMember = "";
            this.fundsGrid.FlatMode = false;
            this.fundsGrid.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.fundsGrid.GridLineColor = System.Drawing.Color.Black;
            this.fundsGrid.HeaderFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.fundsGrid.Location = new System.Drawing.Point(16, 40);
            this.fundsGrid.Name = "fundsGrid";
            //this.fundsGrid.PrintColumnWidths = null;
            this.fundsGrid.Size = new System.Drawing.Size(688, 248);
            this.fundsGrid.TabIndex = 2;
            this.fundsGrid.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
                                                                                                  this.defaultDisplay});
            // 
            // defaultDisplay
            // 
            this.defaultDisplay.AlternatingBackColor = System.Drawing.Color.WhiteSmoke;
            this.defaultDisplay.DataGrid = this.fundsGrid;
            this.defaultDisplay.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
                                                                                                             this.fundsGridFundName,
                                                                                                             this.fundsGridAssetFundName,
                                                                                                             this.fundsGridRelease,
                                                                                                             this.fundsGridPrice,
                                                                                                             this.fundsGridPredictedPrice,
                                                                                                             this.fundsgridPriceMovement,
                                                                                                             this.fundsGridPredictedPriceMovement,
                                                                                                             this.fundsGridPriceVariance,
                                                                                                             this.fundsGridPriceTolerance,
                                                                                                             this.fundsgridAssetUnitPrice,
                                                                                                             this.fundsGridAssetMovement,
                                                                                                             this.fundsGridPredictedAssetMovement,
                                                                                                             this.fundsGridAssetMovementVariance,
                                                                                                             this.fundsGridAssetTolerance,
                                                                                                             this.fundsGridStatus,
                                                                                                             this.fundsGridChangeTime});
            this.defaultDisplay.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.defaultDisplay.MappingName = "";
            // 
            // fundsGridFundName
            // 
            this.fundsGridFundName.Format = "";
            this.fundsGridFundName.FormatInfo = null;
            this.fundsGridFundName.HeaderText = "Fund Name";
            this.fundsGridFundName.MappingName = "FullName";
            this.fundsGridFundName.ReadOnly = true;
            this.fundsGridFundName.ToolTipProperty = "";
            this.fundsGridFundName.Width = 170;
            // 
            // fundsGridAssetFundName
            // 
            this.fundsGridAssetFundName.Format = "";
            this.fundsGridAssetFundName.FormatInfo = null;
            this.fundsGridAssetFundName.HeaderText = "Asset Fund Name";
            this.fundsGridAssetFundName.MappingName = "AssetFundName";
            this.fundsGridAssetFundName.ReadOnly = true;
            this.fundsGridAssetFundName.ToolTipProperty = "FullName";
            this.fundsGridAssetFundName.Width = 120;
            // 
            // fundsGridRelease
            // 
            this.fundsGridRelease.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.fundsGridRelease.AllowNull = false;
            this.fundsGridRelease.CanCheck = true;
            this.fundsGridRelease.CanUnCheck = true;
            this.fundsGridRelease.FalseValue = false;
            this.fundsGridRelease.HeaderText = "Release?";
            this.fundsGridRelease.MappingName = "ProgressStatus";
            this.fundsGridRelease.NullValue = ((object)(resources.GetObject("fundsGridRelease.NullValue")));
            this.fundsGridRelease.ToolTipProperty = "FullName";
            this.fundsGridRelease.TrueValue = true;
            this.fundsGridRelease.Width = 50;
            // 
            // fundsGridPrice
            // 
            this.fundsGridPrice.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.fundsGridPrice.Format = "";
            this.fundsGridPrice.FormatInfo = null;
            this.fundsGridPrice.HeaderText = "Price";
            this.fundsGridPrice.MappingName = "PriceDisplay";
            this.fundsGridPrice.ReadOnly = true;
            this.fundsGridPrice.ToolTipProperty = "FullName";
            this.fundsGridPrice.Width = 70;
            // 
            // fundsGridPredictedPrice
            // 
            this.fundsGridPredictedPrice.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.fundsGridPredictedPrice.Format = "";
            this.fundsGridPredictedPrice.FormatInfo = null;
            this.fundsGridPredictedPrice.HeaderText = "Predicted Price";
            this.fundsGridPredictedPrice.MappingName = "PredictedPriceDisplay";
            this.fundsGridPredictedPrice.ReadOnly = true;
            this.fundsGridPredictedPrice.ToolTipProperty = "FullName";
            this.fundsGridPredictedPrice.Width = 70;
            // 
            // fundsgridPriceMovement
            // 
            this.fundsgridPriceMovement.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.fundsgridPriceMovement.Format = "";
            this.fundsgridPriceMovement.FormatInfo = null;
            this.fundsgridPriceMovement.HeaderText = "Price Movement";
            this.fundsgridPriceMovement.MappingName = "PriceMovementPercentDisplay";
            this.fundsgridPriceMovement.ReadOnly = true;
            this.fundsgridPriceMovement.ToolTipProperty = "FullName";
            this.fundsgridPriceMovement.Width = 70;
            // 
            // fundsGridPredictedPriceMovement
            // 
            this.fundsGridPredictedPriceMovement.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.fundsGridPredictedPriceMovement.Format = "";
            this.fundsGridPredictedPriceMovement.FormatInfo = null;
            this.fundsGridPredictedPriceMovement.HeaderText = "Predicted Price Movement";
            this.fundsGridPredictedPriceMovement.MappingName = "PredictedPriceMovementPercentDisplay";
            this.fundsGridPredictedPriceMovement.ReadOnly = true;
            this.fundsGridPredictedPriceMovement.ToolTipProperty = "FullName";
            this.fundsGridPredictedPriceMovement.Width = 70;
            // 
            // fundsGridPriceVariance
            // 
            this.fundsGridPriceVariance.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.fundsGridPriceVariance.Format = "";
            this.fundsGridPriceVariance.FormatInfo = null;
            this.fundsGridPriceVariance.HeaderText = "Price Variance";
            this.fundsGridPriceVariance.MappingName = "PriceMovementVarianceDisplay";
            this.fundsGridPriceVariance.ReadOnly = true;
            this.fundsGridPriceVariance.ToolTipProperty = "FullName";
            this.fundsGridPriceVariance.Width = 70;
            // 
            // fundsGridPriceTolerance
            // 
            this.fundsGridPriceTolerance.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.fundsGridPriceTolerance.Format = "";
            this.fundsGridPriceTolerance.FormatInfo = null;
            this.fundsGridPriceTolerance.HeaderText = "Within Price Tolerance?";
            this.fundsGridPriceTolerance.MappingName = "PriceMovementToleranceDisplay";
            this.fundsGridPriceTolerance.ReadOnly = true;
            this.fundsGridPriceTolerance.ToolTipProperty = "FullName";
            this.fundsGridPriceTolerance.Width = 120;
            // 
            // fundsgridAssetUnitPrice
            // 
            this.fundsgridAssetUnitPrice.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.fundsgridAssetUnitPrice.Format = "";
            this.fundsgridAssetUnitPrice.FormatInfo = null;
            this.fundsgridAssetUnitPrice.HeaderText = "Asset Unit Price";
            this.fundsgridAssetUnitPrice.MappingName = "AssetUnitPriceDisplay";
            this.fundsgridAssetUnitPrice.ReadOnly = true;
            this.fundsgridAssetUnitPrice.ToolTipProperty = "FullName";
            this.fundsgridAssetUnitPrice.Width = 70;
            // 
            // fundsGridAssetMovement
            // 
            this.fundsGridAssetMovement.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.fundsGridAssetMovement.Format = "";
            this.fundsGridAssetMovement.FormatInfo = null;
            this.fundsGridAssetMovement.HeaderText = "Asset Movement";
            this.fundsGridAssetMovement.MappingName = "AssetMovementDisplay";
            this.fundsGridAssetMovement.ReadOnly = true;
            this.fundsGridAssetMovement.ToolTipProperty = "FullName";
            this.fundsGridAssetMovement.Width = 70;
            // 
            // fundsGridPredictedAssetMovement
            // 
            this.fundsGridPredictedAssetMovement.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.fundsGridPredictedAssetMovement.Format = "";
            this.fundsGridPredictedAssetMovement.FormatInfo = null;
            this.fundsGridPredictedAssetMovement.HeaderText = "Predicted Asset Movement";
            this.fundsGridPredictedAssetMovement.MappingName = "PredictedAssetMovementDisplay";
            this.fundsGridPredictedAssetMovement.ReadOnly = true;
            this.fundsGridPredictedAssetMovement.ToolTipProperty = "FullName";
            this.fundsGridPredictedAssetMovement.Width = 70;
            // 
            // fundsGridAssetMovementVariance
            // 
            this.fundsGridAssetMovementVariance.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.fundsGridAssetMovementVariance.Format = "";
            this.fundsGridAssetMovementVariance.FormatInfo = null;
            this.fundsGridAssetMovementVariance.HeaderText = "Asset Movement Variance";
            this.fundsGridAssetMovementVariance.MappingName = "AssetMovementVarianceDisplay";
            this.fundsGridAssetMovementVariance.ReadOnly = true;
            this.fundsGridAssetMovementVariance.ToolTipProperty = "FullName";
            this.fundsGridAssetMovementVariance.Width = 70;
            // 
            // fundsGridAssetTolerance
            // 
            this.fundsGridAssetTolerance.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.fundsGridAssetTolerance.Format = "";
            this.fundsGridAssetTolerance.FormatInfo = null;
            this.fundsGridAssetTolerance.HeaderText = "Within Asset Movement Tolerance?";
            this.fundsGridAssetTolerance.MappingName = "AssetMovementToleranceDisplay";
            this.fundsGridAssetTolerance.ReadOnly = true;
            this.fundsGridAssetTolerance.ToolTipProperty = "FullName";
            this.fundsGridAssetTolerance.Width = 50;
            // 
            // fundsGridStatus
            // 
            this.fundsGridStatus.Format = "";
            this.fundsGridStatus.FormatInfo = null;
            this.fundsGridStatus.HeaderText = "Price Status";
            this.fundsGridStatus.MappingName = "FundStatusDisplay";
            this.fundsGridStatus.ReadOnly = true;
            this.fundsGridStatus.ToolTipProperty = "FullName";
            this.fundsGridStatus.Width = 125;
            // 
            // fundsGridChangeTime
            // 
            this.fundsGridChangeTime.Format = "";
            this.fundsGridChangeTime.FormatInfo = null;
            this.fundsGridChangeTime.HeaderText = "Status Change Time";
            this.fundsGridChangeTime.MappingName = "StatusChangedTime";
            this.fundsGridChangeTime.ReadOnly = true;
            this.fundsGridChangeTime.ToolTipProperty = "FullName";
            this.fundsGridChangeTime.Width = 110;
            // 
            // submitButton
            // 
            this.submitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.submitButton.Location = new System.Drawing.Point(58, 312);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(96, 23);
            this.submitButton.TabIndex = 3;
            this.submitButton.Text = "O&K";
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(168, 312);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(96, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // releaseAllButton
            // 
            this.releaseAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.releaseAllButton.Location = new System.Drawing.Point(278, 312);
            this.releaseAllButton.Name = "releaseAllButton";
            this.releaseAllButton.Size = new System.Drawing.Size(96, 23);
            this.releaseAllButton.TabIndex = 5;
            this.releaseAllButton.Text = "&Release All";
            this.releaseAllButton.Click += new System.EventHandler(this.releaseAllButton_Click);
            // 
            // unreleaseAllButton
            // 
            this.unreleaseAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.unreleaseAllButton.Location = new System.Drawing.Point(388, 312);
            this.unreleaseAllButton.Name = "unreleaseAllButton";
            this.unreleaseAllButton.Size = new System.Drawing.Size(96, 23);
            this.unreleaseAllButton.TabIndex = 6;
            this.unreleaseAllButton.Text = "&Unrelease All";
            this.unreleaseAllButton.Click += new System.EventHandler(this.unreleaseAllButton_Click);
            // 
            // printButton
            // 
            this.printButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.printButton.Location = new System.Drawing.Point(498, 312);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(96, 23);
            this.printButton.TabIndex = 7;
            this.printButton.Text = "&Print";
            this.printButton.Click += new System.EventHandler(this.printButton_Click);
            // 
            // exportButton
            // 
            this.exportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.exportButton.Location = new System.Drawing.Point(608, 312);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(96, 23);
            this.exportButton.TabIndex = 8;
            this.exportButton.Text = "&Export";
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // Release
            // 
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.printButton);
            this.Controls.Add(this.unreleaseAllButton);
            this.Controls.Add(this.releaseAllButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.fundsGrid);
            this.Controls.Add(this.fundGroupComboBox);
            this.Controls.Add(this.fundGroupLabel);
            this.Name = "Release";
            this.Size = new System.Drawing.Size(718, 356);
            ((System.ComponentModel.ISupportInitialize)(this.fundsGrid)).EndInit();
            this.ResumeLayout(false);

        }

		#endregion

		#region Methods

		/// <summary>
		/// Performs the initial data binding to the constituent controls.
		/// </summary>
		public void CustomInitialisation()
		{
			this.m_loaded = false;

			// Retrieve the current settings.
			UPDPrincipal updPrincipal = (UPDPrincipal) System.Threading.Thread.CurrentPrincipal;
			if (updPrincipal != null && updPrincipal.CompanyCode != null)
			{
				this.m_companyCode = updPrincipal.CompanyCode;
			}

			this.m_connectionString = ConfigurationSettings.AppSettings["connectionString"];

			// Populate the fund group drop down.
			FundGroupController controller = new FundGroupController(this.m_connectionString);
			FundGroupCollection fundGroups = controller.LoadFundGroupsByCompany(this.m_companyCode);
	
			if (fundGroups != null)
			{
				this.fundGroupComboBox.DisplayMember = "FullName";
				this.fundGroupComboBox.ValueMember = "ID";
				this.fundGroupComboBox.DataSource = fundGroups;
			}

			// Check the users permissions
			this.checkPermissions();

			// Populate the grid.
			this.loadGrid();

			this.fundsGrid.SelectedRowChanged += new HBOS.FS.AMP.Windows.Controls.DataGrid.SelectedRowChangedDelegate(fundsGrid_SelectedRowChanged);
			this.m_loaded = true;
		}

		/// <summary>
		/// Load the grid with the funds for the selected fund group.
		/// </summary>
		private void loadGrid()
		{
			if (this.fundGroupComboBox.SelectedIndex > -1 
				&& this.fundGroupComboBox.SelectedValue != null)
			{
				// Load the funds.
				FundController controller = new FundController();
				int fundGroupID = int.Parse(this.fundGroupComboBox.SelectedValue.ToString());

				FundCollection funds
					= controller.LoadReleaseFundsForFundGroup(this.m_connectionString, fundGroupID);

				if (funds != null)
				{
					this.m_restoreFunds = funds.Clone();
					this.fundsGrid.BindToCustomCollection(funds); //, typeof(Fund));
				}
			}
		}

		/// <summary>
		/// Checks the grid to determine whether the user has made any changes or not.
		/// </summary>
		/// <returns>True if one or more rows have been edited.</returns>
		private bool dataChanged()
		{
			// Only check for updates if there was data to bind initially.
			if (this.m_restoreFunds == null)
			{
				return false;
			}

			IList updated = this.fundsGrid.RetrieveUpdatedCustomCollection(); //typeof(Fund));
			
			return (updated != null && updated.Count > 0);
		}

		/// <summary>
		/// Save any changes that have been made to the grid.
		/// </summary>
		private void saveChanges()
		{
			// Retrieve only the edited rows.
			FundCollection funds = null;

			if (this.m_restoreFunds != null)
			{
				funds = (FundCollection)this.fundsGrid.RetrieveUpdatedCustomCollection(); //typeof(Fund));
			}

			// Save the valid data.
			if (funds != null && funds.Count > 0)
			{
				Cursor oldCursor = this.Cursor;
				this.Cursor = Cursors.WaitCursor;

				try
				{
					FundController controller = new FundController();
					controller.ProgressFunds(this.m_connectionString, funds, Fund.FundStatusType.Released);
				}
				catch (FundSaveException exception)
				{
					this.Cursor = oldCursor;
					ExceptionManager.Publish(exception);
					ErrorDialog.Show(exception);
				}
				catch (DatabaseException exception)
				{
					this.Cursor = oldCursor;
					ExceptionManager.Publish(exception);
					ErrorDialog.Show(exception);
				}

				this.Cursor = oldCursor;
			}
		}

		/// <summary>
		/// Loop through the fund collection and set the ProgressStatus flag for all those funds that
		/// don't already have it set to the required value.
		/// </summary>
		/// <param name="release">
		/// True if all funds are to be released, false if they are all to be unreleased.
		/// </param>
		private void updateAllFunds(bool release)
		{
			FundCollection funds = null;

			if (this.m_restoreFunds != null)
			{
				funds = (FundCollection)this.fundsGrid.RetrieveEntireCustomCollection(); //typeof(Fund));
			}
			
			if (funds != null)
			{
				for (int count = 0; count < funds.Count; count++)
				{
					Fund fund = funds[count];
					if (fund.ProgressStatus != release)
					{
						fund.ProgressStatus = release;
					}
				}

				this.fundsGrid.BindToCustomCollection(funds); //, typeof(Fund));
			}
		}

		/// <summary>
		/// Retrieve the fund from the collection bound to the data grid that corresponds with the given
		/// row number.
		/// </summary>
		/// <param name="rowNumber">The row number for which the fund is to be retrieved.</param>
		/// <returns>The fund associated with the row number.</returns>
		private Fund fundAtRow(int rowNumber)
		{
			Fund fund = null;

			if (rowNumber > -1 && this.m_restoreFunds != null)
			{
				FundCollection funds = (FundCollection)this.fundsGrid.RetrieveEntireCustomCollection(); //typeof(Fund));

				if (funds != null && funds.Count > rowNumber)
				{
					fund = funds[rowNumber];
				}
			}

			return fund;
		}

		/// <summary>
		/// Check the user's permissions.
		/// </summary>
		/// <remarks>Used for enabling the Undo ARelease button</remarks>
		private void checkPermissions()
		{
			T.E();

			try
			{
				UPDPrincipal myPrincipal = (UPDPrincipal) System.Threading.Thread.CurrentPrincipal;
				m_permissionUndoRelease = myPrincipal.IsInRole( "UndoReleasePrices" );
				m_permissionRelease = myPrincipal.IsInRole( "ReleasePrices" );

				// Unrelease permissions.
				unreleaseAllButton.Enabled = m_permissionUndoRelease;
				fundsGridRelease.CanUnCheck = m_permissionUndoRelease;

				// Release permissions
				releaseAllButton.Enabled = m_permissionRelease;
				fundsGridRelease.CanCheck = m_permissionRelease;

				fundsGridRelease.InsufficientPermissions +=new HBOS.FS.AMP.Windows.Controls.DataGridBool1ClickColumn.InsufficientPermissionsDelegate(fundsGridRelease_InsufficientPermissions);
			}
			finally
			{
				T.X();
			}
		}

		#endregion

		#region Event handlers

		/// <summary>
		/// Repopulate the grid to show the data for the new selected fund group.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Not used.</param>
		private void fundGroupComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (this.m_loaded)
			{
				// Check whether any changes need to be saved.
				if (this.dataChanged())
				{
					DialogResult feedback = MessageBox.Show("Would you like to save your changes?", "Save Changes?",
						MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

					if (feedback == DialogResult.Cancel)
					{
						return;
					}
					else if (feedback == DialogResult.Yes)
					{
						this.saveChanges();
					}
				}

				// Refresh the grid.
				this.loadGrid();
			}
		}

		/// <summary>
		/// Save any changes that have been made to the data in the grid.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Not used.</param>
		private void submitButton_Click(object sender, System.EventArgs e)
		{
			// Submit the changes to the database.
			this.saveChanges();
			this.loadGrid();
		}

		/// <summary>
		/// Refresh the grid with the saved unedited funds collection.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Not used.</param>
		private void cancelButton_Click(object sender, System.EventArgs e)
		{
			FundCollection funds = this.m_restoreFunds;

			if (funds != null)
			{
				this.fundsGrid.BindToCustomCollection(funds); //, typeof(Fund));
			}
		}

		/// <summary>
		/// Set the ProgressStatus flag on for all the funds that don't have it set on already.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Not used.</param>
		private void releaseAllButton_Click(object sender, System.EventArgs e)
		{
			this.updateAllFunds(true);
		}

		/// <summary>
		/// Unset the ProgressStatus flag for all the funds that have it set.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Not used.</param>
		private void unreleaseAllButton_Click(object sender, System.EventArgs e)
		{
			this.updateAllFunds(false);
		}

		/// <summary>
		/// Check whether the fund at the previous row was returned to its initial status and set off the
		/// IsDirty flag if this is the case.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Not used.</param>
		private void fundsGrid_SelectedRowChanged(object sender, HBOS.FS.AMP.Windows.Controls.SelectedRowChangedEventArgs e)
		{
			if (e.OldRowNumber > -1)
			{
				Fund fund = this.fundAtRow(e.OldRowNumber);

				if (fund != null)
				{
					if (fund.ProgressStatus == fund.InitialProgressStatus)
					{
						// Reset the row changed status in the grid.
						this.fundsGrid.RejectChanges(e.OldRowNumber);

						// The IsDirty flag can be reset here because no updates are done if the status
						// is not being changed.
						fund.IsDirty = false;
					}
				}
			}
		}

		/// <summary>
		/// Prints the contents of the data grid.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Not used.</param>
		private void printButton_Click(object sender, System.EventArgs e)
		{
			this.fundsGrid.PrintPreview("Funds Release");
		}

		/// <summary>
		/// Exports the data grid display as a comma delimited file.
		/// </summary>
		/// <param name="sender">Not used.</param>
		/// <param name="e">Not used.</param>
		private void exportButton_Click(object sender, System.EventArgs e)
		{
			string filename = "FundRelease.csv";
			string actualFileName = Classes.ExportData.DisplayFilePicker(ref filename, "Export Data", "c:\temp", "All Text Files(*.csv)|(*.csv)|All files (*.*)|*.*", Classes.ExportData.FilePickerTypes.SaveDialog);

			if (actualFileName.Length > 0)
			{
				this.fundsGrid.Write(actualFileName, "HBOS.FS.AMP.UPD.WinUI.UserControls.Release.xslt");
				MessageBox.Show(string.Format("{0} has been successfully exported.", actualFileName), "Export Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		/// <summary>
		/// User has insufficent permissions to unrelease a price.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void fundsGridRelease_InsufficientPermissions(object sender, HBOS.FS.AMP.Windows.Controls.CheckBoxEventArgs e)
		{
			T.E();

			try
			{
				if ( e.OldValue == true )
				{
					MessageBox.Show( "You have insufficient permissions to UnRelease a price." , "Permissions Error" , MessageBoxButtons.OK , MessageBoxIcon.Error );
				}
				else
				{
					MessageBox.Show( "You have insufficient permissions to Release a price." , "Permissions Error" , MessageBoxButtons.OK , MessageBoxIcon.Error );
				}
			}
			finally
			{
				T.X();
			}

		}

		#endregion
	}
}
