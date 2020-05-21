using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.Funds;
using HBOS.FS.AMP.UPD.WinUI.Classes;
using HBOS.FS.AMP.UPD.WinUI.UserControls;
using HBOS.FS.Support.Tex;
using DataGrid = HBOS.FS.AMP.Windows.Controls.DataGrid;
using HBOS.FS.AMP.UPD.WinUI.Reports.Reports;
using HBOS.FS.AMP.UPD.WinUI.Reports.Schemas;

namespace HBOS.FS.AMP.UPD.WinUI.Forms
{
	/// <summary>
	/// CurrentFundStatusProperties - Popup for Current Fund Status
	/// </summary>
	public class CurrentFundStatusProperties : Form
	{
		#region Controls

		private TabControl tabControl;
		private TabPage toleranceTabPage;
		private TabPage factorTabPage;
		private TabPage tabAssetMovement;
		private Label assetMovementLabel;
		private Label assetMovementValueLabel;
		private Label upperToleranceValueLabel;
		private Label upperToleranceLabel;
		private Label lowerToleranceValueLabel;
		private Label lowerToleranceLabel;
		private Label priceIncreaseLabel;
		private CheckBox priceIncreaseCheckBox;
		private Button cancelButton;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		#endregion

		private PropertiesAssetFundMovement propertiesAssetFundMovement;
		private DataGrid fundFactorsGrid;
		private System.Windows.Forms.TabPage predictedPriceTabPage;
		private System.Windows.Forms.RichTextBox predictedPriceTextBox;
		private System.Windows.Forms.Button printButton;

		#region Variables and Constants

		// Really 5 decimal places as this is a percentage
		private const string formatAsPercentage = "p4";
		private FundDecorator m_FundDecorator = null;

		#endregion

		#region Constructor

		/// <summary>
		/// Defualt Constructor
		/// </summary>
		public CurrentFundStatusProperties()
		{
			T.E();

			try
			{
				InitializeComponent();

				//
				// TODO: Add any constructor code after InitializeComponent call
				//
				this.Text = "Fund Properties";

				this.assetMovementValueLabel.Text = string.Empty;
				this.upperToleranceValueLabel.Text = string.Empty;
				this.upperToleranceValueLabel.Text = string.Empty;
				this.lowerToleranceValueLabel.Text = string.Empty;
				this.priceIncreaseCheckBox.Checked = false;

				// Bind factor controls to the fund object
//				this.valuationFactorValueLabel.Text = string.Empty;
//				this.scalingFactorValueLabel.Text = string.Empty;
//				this.xFactorValueLabel.Text = string.Empty;
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Overloaded constructor.  
		/// </summary>
		/// <param name="currentFund">Fund object</param>
		public CurrentFundStatusProperties(FundDecorator currentFund) : this()
		{
			T.E();

			try
			{
				this.m_FundDecorator = currentFund;

				this.Text = currentFund.PropertiesHeader;

				// Bind tolerance controls to fund object
				this.assetMovementValueLabel.Text = currentFund.AssetMovementTolerance.ToString(formatAsPercentage);
				this.upperToleranceValueLabel.Text = currentFund.UpperTolerance.ToString(formatAsPercentage);
				this.lowerToleranceValueLabel.Text = currentFund.LowerTolerance.ToString(formatAsPercentage);
				this.priceIncreaseCheckBox.Checked = currentFund.PriceIncreaseOnly;

				propertiesAssetFundMovement.Populate(currentFund.Fund.ParentAssetFund);

				DataGridTableStyle style = new DataGridTableStyle();

				fundFactorsGrid.TableStyles.Clear();
				fundFactorsGrid.TableStyles.Add(style);

				style.AlternatingBackColor = Color.WhiteSmoke;
				style.DataGrid = fundFactorsGrid;
				style.HeaderForeColor = SystemColors.ControlText;
				style.MappingName = "";

				const int defaultColWidth = 80;

				// Hack to allow for multi-line header rows.
				// This requires a large font (36pt) to be set for grid.HeaderFont through the properties window
				style.HeaderFont = fundFactorsGrid.Font;

				//set-up the columns
				StatusViewController.AddTextBoxReadOnlyColumnStyle(fundFactorsGrid, "DisplayName", "Type", 150, HorizontalAlignment.Left, "");
				StatusViewController.AddPercentColumnStyle(fundFactorsGrid, "EffectToday", "Value", defaultColWidth, 4, HorizontalAlignment.Right, "");
				fundFactorsGrid.TableStyles[0].GridColumnStyles[1].ReadOnly = true;

				//Retrieve and fill the grid with the factors for the Current Fund
				fundFactorsGrid.BindToCustomCollection(currentFund.Factors);

				//Predicted Price calculation label - built by Decorator
				predictedPriceTextBox.Text = currentFund.PredictedPriceFormulaDisplay;
			}
			finally
			{
				T.X();
			}
		}

		#endregion

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.tabControl = new System.Windows.Forms.TabControl();
            this.predictedPriceTabPage = new System.Windows.Forms.TabPage();
            this.predictedPriceTextBox = new System.Windows.Forms.RichTextBox();
            this.tabAssetMovement = new System.Windows.Forms.TabPage();
            this.propertiesAssetFundMovement = new HBOS.FS.AMP.UPD.WinUI.Forms.PropertiesAssetFundMovement();
            this.factorTabPage = new System.Windows.Forms.TabPage();
            this.fundFactorsGrid = new HBOS.FS.AMP.Windows.Controls.DataGrid();
            this.toleranceTabPage = new System.Windows.Forms.TabPage();
            this.upperToleranceValueLabel = new System.Windows.Forms.Label();
            this.priceIncreaseCheckBox = new System.Windows.Forms.CheckBox();
            this.priceIncreaseLabel = new System.Windows.Forms.Label();
            this.lowerToleranceValueLabel = new System.Windows.Forms.Label();
            this.lowerToleranceLabel = new System.Windows.Forms.Label();
            this.upperToleranceLabel = new System.Windows.Forms.Label();
            this.assetMovementValueLabel = new System.Windows.Forms.Label();
            this.assetMovementLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.printButton = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.predictedPriceTabPage.SuspendLayout();
            this.tabAssetMovement.SuspendLayout();
            this.factorTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fundFactorsGrid)).BeginInit();
            this.toleranceTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.predictedPriceTabPage);
            this.tabControl.Controls.Add(this.tabAssetMovement);
            this.tabControl.Controls.Add(this.factorTabPage);
            this.tabControl.Controls.Add(this.toleranceTabPage);
            this.tabControl.Location = new System.Drawing.Point(8, 8);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(610, 352);
            this.tabControl.TabIndex = 0;
            // 
            // predictedPriceTabPage
            // 
            this.predictedPriceTabPage.Controls.Add(this.predictedPriceTextBox);
            this.predictedPriceTabPage.Location = new System.Drawing.Point(4, 22);
            this.predictedPriceTabPage.Name = "predictedPriceTabPage";
            this.predictedPriceTabPage.Size = new System.Drawing.Size(602, 326);
            this.predictedPriceTabPage.TabIndex = 2;
            this.predictedPriceTabPage.Text = "Predicted Price";
            // 
            // predictedPriceTextBox
            // 
            this.predictedPriceTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.predictedPriceTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.predictedPriceTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.predictedPriceTextBox.Location = new System.Drawing.Point(13, 14);
            this.predictedPriceTextBox.Name = "predictedPriceTextBox";
            this.predictedPriceTextBox.ReadOnly = true;
            this.predictedPriceTextBox.Size = new System.Drawing.Size(479, 254);
            this.predictedPriceTextBox.TabIndex = 1;
            this.predictedPriceTextBox.Text = "";
            // 
            // tabAssetMovement
            // 
            this.tabAssetMovement.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.tabAssetMovement.Controls.Add(this.propertiesAssetFundMovement);
            this.tabAssetMovement.Location = new System.Drawing.Point(4, 22);
            this.tabAssetMovement.Name = "tabAssetMovement";
            this.tabAssetMovement.Size = new System.Drawing.Size(602, 326);
            this.tabAssetMovement.TabIndex = 0;
            this.tabAssetMovement.Text = "Predicted Asset Movement";
            // 
            // propertiesAssetFundMovement
            // 
            this.propertiesAssetFundMovement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertiesAssetFundMovement.Location = new System.Drawing.Point(0, 0);
            this.propertiesAssetFundMovement.Name = "propertiesAssetFundMovement";
            this.propertiesAssetFundMovement.Size = new System.Drawing.Size(602, 326);
            this.propertiesAssetFundMovement.TabIndex = 0;
            // 
            // factorTabPage
            // 
            this.factorTabPage.Controls.Add(this.fundFactorsGrid);
            this.factorTabPage.Location = new System.Drawing.Point(4, 22);
            this.factorTabPage.Name = "factorTabPage";
            this.factorTabPage.Size = new System.Drawing.Size(602, 326);
            this.factorTabPage.TabIndex = 1;
            this.factorTabPage.Text = "Factors";
            // 
            // fundFactorsGrid
            // 
            this.fundFactorsGrid.DataMember = "";
            this.fundFactorsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fundFactorsGrid.FlatMode = false;
            this.fundFactorsGrid.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.fundFactorsGrid.GridLineColor = System.Drawing.Color.Black;
            this.fundFactorsGrid.Location = new System.Drawing.Point(0, 0);
            this.fundFactorsGrid.Name = "fundFactorsGrid";
            this.fundFactorsGrid.PrintColumnHeadingFont = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.fundFactorsGrid.PrintColumnSettings = null;
            this.fundFactorsGrid.PrintPageHeadingFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.fundFactorsGrid.PrintStandardFont = new System.Drawing.Font("Arial", 8F);
            this.fundFactorsGrid.Size = new System.Drawing.Size(602, 326);
            this.fundFactorsGrid.TabIndex = 7;
            // 
            // toleranceTabPage
            // 
            this.toleranceTabPage.Controls.Add(this.upperToleranceValueLabel);
            this.toleranceTabPage.Controls.Add(this.priceIncreaseCheckBox);
            this.toleranceTabPage.Controls.Add(this.priceIncreaseLabel);
            this.toleranceTabPage.Controls.Add(this.lowerToleranceValueLabel);
            this.toleranceTabPage.Controls.Add(this.lowerToleranceLabel);
            this.toleranceTabPage.Controls.Add(this.upperToleranceLabel);
            this.toleranceTabPage.Controls.Add(this.assetMovementValueLabel);
            this.toleranceTabPage.Controls.Add(this.assetMovementLabel);
            this.toleranceTabPage.Location = new System.Drawing.Point(4, 22);
            this.toleranceTabPage.Name = "toleranceTabPage";
            this.toleranceTabPage.Size = new System.Drawing.Size(602, 326);
            this.toleranceTabPage.TabIndex = 0;
            this.toleranceTabPage.Text = "Tolerance";
            // 
            // upperToleranceValueLabel
            // 
            this.upperToleranceValueLabel.Location = new System.Drawing.Point(153, 40);
            this.upperToleranceValueLabel.Name = "upperToleranceValueLabel";
            this.upperToleranceValueLabel.Size = new System.Drawing.Size(184, 16);
            this.upperToleranceValueLabel.TabIndex = 8;
            this.upperToleranceValueLabel.Text = "upper tolerance value";
            this.upperToleranceValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // priceIncreaseCheckBox
            // 
            this.priceIncreaseCheckBox.Enabled = false;
            this.priceIncreaseCheckBox.Location = new System.Drawing.Point(153, 88);
            this.priceIncreaseCheckBox.Name = "priceIncreaseCheckBox";
            this.priceIncreaseCheckBox.Size = new System.Drawing.Size(187, 16);
            this.priceIncreaseCheckBox.TabIndex = 7;
            // 
            // priceIncreaseLabel
            // 
            this.priceIncreaseLabel.Location = new System.Drawing.Point(16, 88);
            this.priceIncreaseLabel.Name = "priceIncreaseLabel";
            this.priceIncreaseLabel.Size = new System.Drawing.Size(112, 16);
            this.priceIncreaseLabel.TabIndex = 6;
            this.priceIncreaseLabel.Text = "Price Increase Only:";
            // 
            // lowerToleranceValueLabel
            // 
            this.lowerToleranceValueLabel.Location = new System.Drawing.Point(153, 64);
            this.lowerToleranceValueLabel.Name = "lowerToleranceValueLabel";
            this.lowerToleranceValueLabel.Size = new System.Drawing.Size(184, 16);
            this.lowerToleranceValueLabel.TabIndex = 5;
            this.lowerToleranceValueLabel.Text = "lower tolerance value";
            this.lowerToleranceValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lowerToleranceLabel
            // 
            this.lowerToleranceLabel.Location = new System.Drawing.Point(16, 64);
            this.lowerToleranceLabel.Name = "lowerToleranceLabel";
            this.lowerToleranceLabel.Size = new System.Drawing.Size(96, 16);
            this.lowerToleranceLabel.TabIndex = 4;
            this.lowerToleranceLabel.Text = "Lower Tolerance:";
            // 
            // upperToleranceLabel
            // 
            this.upperToleranceLabel.Location = new System.Drawing.Point(16, 40);
            this.upperToleranceLabel.Name = "upperToleranceLabel";
            this.upperToleranceLabel.Size = new System.Drawing.Size(96, 16);
            this.upperToleranceLabel.TabIndex = 2;
            this.upperToleranceLabel.Text = "Upper Tolerance:";
            // 
            // assetMovementValueLabel
            // 
            this.assetMovementValueLabel.Location = new System.Drawing.Point(153, 16);
            this.assetMovementValueLabel.Name = "assetMovementValueLabel";
            this.assetMovementValueLabel.Size = new System.Drawing.Size(184, 16);
            this.assetMovementValueLabel.TabIndex = 1;
            this.assetMovementValueLabel.Text = "asset movement value";
            this.assetMovementValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // assetMovementLabel
            // 
            this.assetMovementLabel.Location = new System.Drawing.Point(16, 16);
            this.assetMovementLabel.Name = "assetMovementLabel";
            this.assetMovementLabel.Size = new System.Drawing.Size(96, 16);
            this.assetMovementLabel.TabIndex = 0;
            this.assetMovementLabel.Text = "Asset Movement:";
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(538, 368);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // printButton
            // 
            this.printButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.printButton.Location = new System.Drawing.Point(456, 368);
            this.printButton.Name = "printButton";
            this.printButton.TabIndex = 2;
            this.printButton.Text = "Print";
            this.printButton.Click += new System.EventHandler(this.printButton_Click);
            // 
            // CurrentFundStatusProperties
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(625, 399);
            this.Controls.Add(this.printButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.tabControl);
            this.MaximizeBox = false;
            this.Name = "CurrentFundStatusProperties";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Fund Properties";
            this.TopMost = true;
            this.tabControl.ResumeLayout(false);
            this.predictedPriceTabPage.ResumeLayout(false);
            this.tabAssetMovement.ResumeLayout(false);
            this.factorTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fundFactorsGrid)).EndInit();
            this.toleranceTabPage.ResumeLayout(false);
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

		/// <summary>
		/// Close the dialog
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			T.E();

			try
			{
				this.Close();
			}
			finally
			{
				T.X();
			}
		}

		#endregion

		private void printButton_Click(object sender, System.EventArgs e)
		{

			PredictedPriceSchema reportData = populateDataSet1( m_FundDecorator );

			PredictedPriceCrystalReport  predictedPriceCrystalReport = new PredictedPriceCrystalReport();
			predictedPriceCrystalReport.SetDataSource( reportData.Fund );

			PredictedPriceView f = new PredictedPriceView( predictedPriceCrystalReport );
			f.Show();		

		}


		private PredictedPriceSchema populateDataSet1( FundDecorator inFundDecorator )
		{
			PredictedPriceSchema  reportData = new PredictedPriceSchema();

			IList constituents = AssetMovementConstituentPropertiesDecorator.FromConstituentListToDecorated(
				inFundDecorator.Fund.ParentAssetFund.AssetMovementConstituents );

			//if there are no benchmarks show a zero benchmark
			if (constituents == null || constituents.Count == 0)
			{
				AssetMovementConstituentPropertiesDecorator decorator = 
										PropertiesAssetFundMovement.getDecoratorForNullConstituent(
																inFundDecorator.Fund.ParentAssetFund );
				constituents.Add(decorator);
			}

			foreach (AssetMovementConstituentPropertiesDecorator  constituent in constituents)
			{
					reportData.Fund.AddFundRow
						(
						inFundDecorator.FullName,
						inFundDecorator.Fund.HiPortfolioCode,						
						inFundDecorator.Fund.ParentAssetFund.ValuationPoint.ToShortDateString() ,

						inFundDecorator.PredictedPriceDisplay,
						inFundDecorator.PriceDisplay,
						inFundDecorator.PreviousPriceDisplay,

						inFundDecorator.Fund.ParentAssetFund.FullName, 
						inFundDecorator.Fund.ParentAssetFund.AssetFundCode, 
						inFundDecorator.Fund.ParentAssetFund.Currency.CurrencyCode,

						inFundDecorator.RevaluationFactorDisplay,
						inFundDecorator.ScalingFactorDisplay,
						inFundDecorator.TaxProvisionEstimateDisplay,
						inFundDecorator.ValuationBasisDisplay,
						inFundDecorator.XFactorDisplay2,

						inFundDecorator.UpperToleranceDisplay2,
						inFundDecorator.LowerToleranceDisplay2,
						inFundDecorator.AssetMovementToleranceDisplay2,
						inFundDecorator.PriceIncreaseOnlyDisplay,

						constituent.BenchmarkDisplayName,
						constituent.ProportionDisplay,
						constituent.CurrencyCode,
						constituent.BenchmarkMovement,
						constituent.CurrencyMovement,
						constituent.TotalMovement,
						constituent.Effect,
						constituent.Availablity,
						constituent.PreviousBenchmarkValue,
						constituent.CurrentBenchmarkValue,
						constituent.PreviousAssetFundExchangeRate,
						constituent.CurrentAssetFundExchangeRate,
						constituent.PreviousBenchmarkExchangeRate,
						constituent.CurrentBenchmarkExchangeRate
						);	
			}//... end of foreach
								
			return reportData;
		}//... end of method

	}
}
