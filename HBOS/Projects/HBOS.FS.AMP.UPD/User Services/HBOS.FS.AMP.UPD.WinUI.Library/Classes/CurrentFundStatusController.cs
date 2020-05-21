using System;
using System.Drawing;
using System.Windows.Forms;
using HBOS.FS.AMP.UPD.WinUI.Forms;
using HBOS.FS.AMP.UPD.WinUI.UserControls;
using HBOS.FS.AMP.Windows.Controls;
using HBOS.FS.Support.Tex;
using DataGrid = HBOS.FS.AMP.Windows.Controls.DataGrid;
using HBOSGrid = HBOS.FS.AMP.Windows.Controls.DataGrid;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// Summary description for ReleaseFundViewController.
	/// </summary>
	public class CurrentFundStatusController : StatusViewController
	{
		/// <summary>
		/// Creates a new <see cref="ReleaseFundViewController"/> instance.
		/// </summary>
		public CurrentFundStatusController(StatusView view, StatusDataManager dataManager) : base(view, dataManager)
		{
			T.E();
			view.RefreshData += new StatusView.RefreshDataDelegate(refreshData);
			T.X();
		}

		#region Overrides

		/// <summary>
		/// Configures the grid.
		/// </summary>
		protected override void configureGrid()
		{
			T.E();
			HBOSGrid grid = view.grid;
			DataGridTableStyle style = new DataGridTableStyle();

			grid.TableStyles.Clear();
			grid.TableStyles.Add(style);
			grid.CellDoubleClicked += new DataGrid.CellDoubleClickedDelegate(this.OnGridCellDoubleClicked);
			grid.PrintGridStyle = DataGrid.GridPrintStyle.MultipleHorizontalPages;

			style.AlternatingBackColor = Color.WhiteSmoke;
			style.DataGrid = grid;
			style.HeaderForeColor = SystemColors.ControlText;
			style.MappingName = "";

			// Hack to allow for multi-line header rows.
			// This requires a large font (36pt) to be set for grid.HeaderFont through the properties window
			style.HeaderFont = grid.Font;

			const int defaultColWidth = 80;

			//fund columns
			AddTextBoxReadOnlyColumnStyle("FullName", "Fund Name", 220, HorizontalAlignment.Left, "");
			AddTextBoxReadOnlyColumnStyle("PriceDisplay", "Imported\nPrice", defaultColWidth, HorizontalAlignment.Right, "");
			AddTextBoxReadOnlyColumnStyle("PredictedPriceDisplay", "Predicted \nPrice", defaultColWidth, HorizontalAlignment.Right, "");
			AddTextBoxReadOnlyColumnStyle("PriceMovementPercentDisplay", "Imported\nPrice\nMovement", defaultColWidth, HorizontalAlignment.Right, "");
			AddTextBoxReadOnlyColumnStyle("PredictedPriceMovementPercentDisplay", "Predicted\nPrice\nMovement", defaultColWidth, HorizontalAlignment.Right, "");
			AddTextBoxReadOnlyColumnStyle("PriceMovementVarianceDisplay", "Price\nVariance", defaultColWidth, HorizontalAlignment.Right, "");
			AddTextBoxReadOnlyColumnStyle("PriceMovementRoundedToleranceDisplay", "Within price\nTolerance", 140, HorizontalAlignment.Left, "");
			AddTextBoxReadOnlyColumnStyle("AssetUnitPriceDisplay", "Asset\nUnit Price", defaultColWidth, HorizontalAlignment.Right, "");
			AddTextBoxReadOnlyColumnStyle("AssetMovementDisplay", "Asset Price\nMovement", 100, HorizontalAlignment.Right, "");
			AddTextBoxReadOnlyColumnStyle("PredictedAssetMovementDisplay", "Predicted AM", 90, HorizontalAlignment.Right, "");
			AddTextBoxReadOnlyColumnStyle("AssetMovementVarianceDisplay", "AM Variance", 90, HorizontalAlignment.Right, "");
			AddTextBoxReadOnlyColumnStyle("WithinAssetMovementToleranceDisplay", "Within AM\nTolerance", defaultColWidth, HorizontalAlignment.Left, "");
			AddTextBoxReadOnlyColumnStyle("FundStatusDisplay", "Current\nStatus", defaultColWidth, HorizontalAlignment.Left, "");
			AddTextBoxReadOnlyColumnStyle("StatusChangedTime", "Last Status\nChanged", 130, HorizontalAlignment.Left, "");

			ImportControl.SetGridPrinterSettings(grid, style);
			T.X();
		}

		/// <summary>
		/// Creates the actions.
		/// </summary>
		protected override StatusAction[] createActions()
		{
			T.E();

			StatusAction[] result = new StatusAction[4];

			result[0] = new StatusAction();
			result[0].Text = "&Refresh";
			result[0].Executed += new EventHandler(refreshExecuted);

			result[1] = new StatusAction();
			result[1].Text = "&Print";
			result[1].Executed += new EventHandler(printExecuted);

			result[2] = new StatusAction();
			result[2].Text = "&Export";
			result[2].Executed += new EventHandler(exportExecuted);

			result[3] = new StatusAction();
			result[3].Text = "Pr&operties";
			result[3].Executed += new EventHandler(PropertiesButtonClicked);

			T.X();
			return result;
		}

		#endregion Overrides

		#region Event Handlers

		private void refreshExecuted(object sender, EventArgs e)
		{
			T.E();
			refreshData();
			T.X();
		}

		private void PropertiesButtonClicked(object sender, EventArgs e)
		{
			int CurrentRowIndex = view.grid.CurrentRowIndex;
			if (CurrentRowIndex >= 0)
			{
				this.displayFundProperties(CurrentRowIndex);
			}
		}

		/// <summary>
		/// hadle that the grid has been double clicked, and possible show the properties dialog
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnGridCellDoubleClicked(object sender, CellEventArgs e)
		{
			//KAJ 09/03/2005 - Double clicking header row causes exception
			T.E();
			if (e.RowNumber >= 0)
			{
				this.displayFundProperties(e.RowNumber);
			}
			T.X();
		}

		private void printExecuted(object sender, EventArgs e)
		{
			printGrid("Current Fund Status");
		}

		private void exportExecuted(object sender, EventArgs e)
		{
			exportGrid("CurrentFundStatus.csv", "HBOS.FS.AMP.UPD.WinUI.UserControls.CurrentFundStatus.xslt");
		}

		#endregion Event Handlers

		#region Private Members

		/// <summary>
		/// Display the currently selected fund properties
		/// </summary>
		/// <param name="selectedRow">currently selected grid row number</param>
		private void displayFundProperties(int selectedRow)
		{
			T.E();
			// Make sure we have funds
			if (view.Data.Count > 0)
			{
				// Get the currently selected fund object against 
				// the selected data grid row
				FundDecorator selectedFund = FundDecorator.ToDecoratedObject(base.fundAtRow(selectedRow));

				// Pass the currently selected fund object to the form 
				// and Display its properties
				CurrentFundStatusProperties properties = new CurrentFundStatusProperties(selectedFund);
				properties.Show(); //Dialog(view.Parent);
			}
			T.X();
		}

		#endregion Private Members
	}
}