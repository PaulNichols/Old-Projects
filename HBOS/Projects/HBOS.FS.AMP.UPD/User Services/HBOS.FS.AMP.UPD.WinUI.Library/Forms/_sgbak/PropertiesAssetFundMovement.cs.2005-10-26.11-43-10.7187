using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.StockMarketIndex;
using HBOS.FS.AMP.UPD.Types.Currency;
using HBOS.FS.AMP.UPD.WinUI.Classes;
using HBOS.FS.AMP.UPD.WinUI.Helpers;
using HBOS.FS.AMP.UPD.WinUI.UserControls;
using DataGrid = HBOS.FS.AMP.Windows.Controls.DataGrid;
using HBOSGrid = HBOS.FS.AMP.Windows.Controls.DataGrid;

namespace HBOS.FS.AMP.UPD.WinUI.Forms
{
	/// <summary>
	/// Summary description for PropertiesAssetFundMovement.
	/// </summary>
	public class PropertiesAssetFundMovement : UserControl
	{
		private DataGrid assetFundMovementGrid;
		private Label lblAFMovementValue;
		private Label lblAFMovement;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private Container components = null;


		/// <summary>
		/// This is a control shared by both the Status view screens. 
		/// When the properties for a chosen item is viewed, this control 
		/// will show the Asset fund movement in a grid
		/// </summary>
		public PropertiesAssetFundMovement()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.assetFundMovementGrid = new HBOS.FS.AMP.Windows.Controls.DataGrid();
			this.lblAFMovementValue = new System.Windows.Forms.Label();
			this.lblAFMovement = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.assetFundMovementGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// assetFundMovementGrid
			// 
			this.assetFundMovementGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.assetFundMovementGrid.DataMember = "";
			this.assetFundMovementGrid.FlatMode = false;
			this.assetFundMovementGrid.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.assetFundMovementGrid.GridLineColor = System.Drawing.Color.Black;
			this.assetFundMovementGrid.Location = new System.Drawing.Point(0, 24);
			this.assetFundMovementGrid.Name = "assetFundMovementGrid";
			this.assetFundMovementGrid.PrintColumnHeadingFont = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
			this.assetFundMovementGrid.PrintColumnSettings = null;
			this.assetFundMovementGrid.PrintPageHeadingFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.assetFundMovementGrid.PrintStandardFont = new System.Drawing.Font("Arial", 8F);
			this.assetFundMovementGrid.Size = new System.Drawing.Size(328, 152);
			this.assetFundMovementGrid.TabIndex = 6;
			// 
			// lblAFMovementValue
			// 
			this.lblAFMovementValue.Location = new System.Drawing.Point(192, 8);
			this.lblAFMovementValue.Name = "lblAFMovementValue";
			this.lblAFMovementValue.Size = new System.Drawing.Size(120, 14);
			this.lblAFMovementValue.TabIndex = 8;
			this.lblAFMovementValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblAFMovement
			// 
			this.lblAFMovement.Location = new System.Drawing.Point(8, 8);
			this.lblAFMovement.Name = "lblAFMovement";
			this.lblAFMovement.Size = new System.Drawing.Size(174, 14);
			this.lblAFMovement.TabIndex = 7;
			this.lblAFMovement.Text = "Total Asset Fund movement:";
			// 
			// PropertiesAssetFundMovement
			// 
			this.Controls.Add(this.lblAFMovementValue);
			this.Controls.Add(this.lblAFMovement);
			this.Controls.Add(this.assetFundMovementGrid);
			this.Name = "PropertiesAssetFundMovement";
			this.Size = new System.Drawing.Size(328, 176);
			((System.ComponentModel.ISupportInitialize)(this.assetFundMovementGrid)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		/// <summary> 
		/// returns decorator for null constituent
		/// </summary>
		public static AssetMovementConstituentPropertiesDecorator getDecoratorForNullConstituent(
																				AssetFund inAssetFund )
		{
			Currency curr = new Currency("n/a", 0M, 0M);
			StockMarketIndex index = new StockMarketIndex(0, 
				"Non-benchmarked Asset Fund", 0M, 0M, curr);

			AssetMovementConstituent  constituent = new AssetMovementConstituent(1, index);			
			constituent.ParentAssetFund = inAssetFund;

			AssetMovementConstituentPropertiesDecorator decorator = 
				AssetMovementConstituentPropertiesDecorator.FromConstituentToDecoratedConstituent(constituent);

			decorator.Availablity = "Non-benchmarked";
			decorator.AvailabilityMessage = string.Empty;

			return decorator;
		}//... end of getDecoratorForNullConstituent()


		/// <summary>
		/// This method will populate the control with details from the Asset fund
		/// </summary>
		/// <param name="currentAssetFund"></param>
		public void Populate(AssetFund currentAssetFund)
		{
			AssetFundDecorator decoratoredAssetFund=AssetFundDecorator.ToDecoratedObject(currentAssetFund);
			lblAFMovementValue.Text =  decoratoredAssetFund.PredictedAssetMovementDisplay;

			HBOSGrid grid = assetFundMovementGrid;
			DataGridTableStyle style = new DataGridTableStyle();

			//	grid.MouseDown+=new MouseEventHandler(grid_mousedown);
			grid.TableStyles.Clear();
			grid.TableStyles.Add(style);

			style.AlternatingBackColor = Color.WhiteSmoke;
			style.DataGrid = grid;
			style.HeaderForeColor = SystemColors.ControlText;
			style.MappingName = "";

			// Hack to allow for multi-line header rows.
			// This requires a large font (36pt) to be set for grid.HeaderFont through the properties window
			style.HeaderFont = grid.Font;

			//set-up the columns
			SetUpColumns(grid);

			IList data = AssetMovementConstituentPropertiesDecorator.FromConstituentListToDecorated(decoratoredAssetFund.AssetFund.AssetMovementConstituents);

			//if there are no benchmarks show a zero benchmark
			if (data == null || data.Count == 0)
			{
				AssetMovementConstituentPropertiesDecorator decorator = getDecoratorForNullConstituent(currentAssetFund);
                data.Add(decorator);
			}
		
			//show benchmarks in grid
			assetFundMovementGrid.BindToCustomCollection(data);
			ImportControl.SetGridPrinterSettings(grid,style);
			
		}

		private  void SetUpColumns(DataGrid grid)
		{
			const int defaultColWidth = 80;

			StatusViewController.AddTextBoxReadOnlyColumnStyle(grid, "BenchmarkDisplayName", "Name", 200, HorizontalAlignment.Left, "");
			StatusViewController.AddTextBoxReadOnlyColumnStyle(grid, "ProportionDisplay", "Weighting\n(%)", defaultColWidth, HorizontalAlignment.Right, "");
			//StatusViewController.AddTextBoxReadOnlyColumnStyle(grid, "CurrencyCode", "Benchmark.\nCurrency.", defaultColWidth, HorizontalAlignment.Left, "");
			StatusViewController.AddTextBoxReadOnlyColumnStyle(grid, "BenchmarkMovement", "Benchmark\nMovement\n(%)", defaultColWidth, HorizontalAlignment.Right, "");
			StatusViewController.AddTextBoxReadOnlyColumnStyle(grid, "CurrencyMovement", "Currency\nMovement\n(%)", defaultColWidth, HorizontalAlignment.Right, "");
			StatusViewController.AddTextBoxReadOnlyColumnStyle(grid, "TotalMovement", "Total\nMovement\n(%)", defaultColWidth, HorizontalAlignment.Right, "");
			StatusViewController.AddTextBoxReadOnlyColumnStyle(grid, "Effect", "Effect\n(%)", defaultColWidth, HorizontalAlignment.Right, "");
			StatusViewController.AddTextBoxReadOnlyColumnStyle(grid, "Availablity", "Availablity", 200, HorizontalAlignment.Left, "AvailabilityMessage");
			StatusViewController.AddTextBoxReadOnlyColumnStyle(grid, "PreviousBenchmarkValue", "Previous\nBenchmark\nValue", defaultColWidth, HorizontalAlignment.Right, "");
			StatusViewController.AddTextBoxReadOnlyColumnStyle(grid, "CurrentBenchmarkValue", "Current\nBenchmark\nValue", defaultColWidth, HorizontalAlignment.Right, "");
			StatusViewController.AddTextBoxReadOnlyColumnStyle(grid, "PreviousAssetFundExchangeRate", "Previous\nFund\nExchange Rate", 120, HorizontalAlignment.Right, "");
			StatusViewController.AddTextBoxReadOnlyColumnStyle(grid, "CurrentAssetFundExchangeRate", "Current\nFund\nExchange Rate", 120, HorizontalAlignment.Right, "");
			StatusViewController.AddTextBoxReadOnlyColumnStyle(grid, "PreviousBenchmarkExchangeRate", "Previous\nBenchmark\nExchange Rate", 120, HorizontalAlignment.Right, "");
			StatusViewController.AddTextBoxReadOnlyColumnStyle(grid, "CurrentBenchmarkExchangeRate", "Current\nBenchmark\nExchange Rate", 120, HorizontalAlignment.Right, "");
		}

		
        }
}