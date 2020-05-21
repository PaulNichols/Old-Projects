using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using HBOS.FS.AMP.UPD.Types;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.WinUI.Forms;
using HBOS.FS.AMP.UPD.WinUI.UserControls;
using HBOS.FS.AMP.Windows.Controls;
using HBOS.FS.Support.Tex;
using CellDoubleClickedDelegate = HBOS.FS.AMP.Windows.Controls.DataGrid.CellDoubleClickedDelegate;
using DataGrid = HBOS.FS.AMP.Windows.Controls.DataGrid;
using HBOSGrid = HBOS.FS.AMP.Windows.Controls.DataGrid;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	
	/// <summary>
	/// Summary description for ReleaseFundViewController.
	/// </summary>
	public class AssetFundStatusController : StatusViewController
	{
		/// <summary>
		/// Creates a new <see cref="ReleaseFundViewController"/> instance.
		/// </summary>
		public AssetFundStatusController(StatusView view, StatusDataManager dataManager) : base(view, dataManager)
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
			AddTextBoxReadOnlyColumnStyle("AssetFundType", "Type", defaultColWidth, HorizontalAlignment.Left, "");
			AddTextBoxReadOnlyColumnStyle("UnitPriceDisplay", "Imported\nUnit Price", defaultColWidth, HorizontalAlignment.Right, "");
			AddTextBoxReadOnlyColumnStyle("UnitPriceMovementDisplay", "Imported\nPrice\nMovement", defaultColWidth, HorizontalAlignment.Right, "");
			AddTextBoxReadOnlyColumnStyle("PredictedAssetMovementDisplay", "Predicted AM", defaultColWidth, HorizontalAlignment.Right, "");
			AddTextBoxReadOnlyColumnStyle("AssetMovementVarianceDisplay", "AM Variance", defaultColWidth, HorizontalAlignment.Right, "");
			AddTextBoxReadOnlyColumnStyle("WithinAssetMovementToleranceDisplay", "Within\nTolerance", defaultColWidth, HorizontalAlignment.Left, "");

			HBOS.FS.AMP.UPD.WinUI.UserControls.ImportControl.SetGridPrinterSettings(grid,style);
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

		private void PropertiesButtonClicked(object sender, EventArgs e)
		{
			int CurrentRowIndex = view.grid.CurrentRowIndex;
			if (CurrentRowIndex >= 0)
			{
				this.displayAssetFundProperties(CurrentRowIndex);
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
				displayAssetFundProperties(e.RowNumber);
			}
			T.X();
		}

		private void printExecuted(object sender, EventArgs e)
		{
			printGrid("Current Asset Fund Status");
		}

		private void exportExecuted(object sender, EventArgs e)
		{
			exportGrid("CurrentAssetFundStatus.csv", "HBOS.FS.AMP.UPD.WinUI.UserControls.AssetFundStatus.xslt");
		}

		private void refreshExecuted(object sender, EventArgs e)
		{
			T.E();
			refreshData();
			T.X();
		}

		#endregion Event Handlers

		#region Private Members

		/// <summary>
		/// Display the currently selected fund properties
		/// </summary>
		/// <param name="selectedRow">currently selected grid row number</param>
		private void displayAssetFundProperties(int selectedRow)
		{
			T.E();
			// Make sure we have funds
			if (view.Data.Count > 0)
			{
				// Get the currently selected fund object against 
				// the selected data grid row
				AssetFundDecorator selectedAssetFund = base.assetFundAtRow(selectedRow);

				// Pass the currently selected fund object to the form 
				// and Display its properties
				CurrentAssetFundStatusProperties properties = new CurrentAssetFundStatusProperties(selectedAssetFund);
				properties.Show(); //Dialog(view.Parent);
			}
			T.X();
		}

		#endregion Private Members
	}
}