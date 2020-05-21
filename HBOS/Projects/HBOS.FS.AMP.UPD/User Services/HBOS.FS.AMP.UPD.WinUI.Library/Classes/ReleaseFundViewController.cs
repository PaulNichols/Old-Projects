using System;
using System.Windows.Forms;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.WinUI.UserControls;
using HBOS.FS.Support.Tex;
using HBOS.FS.AMP.UPD.Types.Funds;
using HBOS.FS.AMP.UPD.Controllers;


using HBOSGrid = HBOS.FS.AMP.Windows.Controls.DataGrid;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// Summary description for ReleaseFundViewController.
	/// </summary>
	public class ReleaseFundViewController: StatusViewController
	{
		/// <summary>
		/// Creates a new <see cref="ReleaseFundViewController"/> instance.
		/// </summary>
		public ReleaseFundViewController(StatusView view, StatusDataManager dataManager): base(view,dataManager)
		{
			T.E();
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

			style.AlternatingBackColor = System.Drawing.Color.WhiteSmoke;
			style.DataGrid = grid;
			style.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			style.MappingName = "";

			// Hack to allow for multi-line header rows.
			// This requires a large font (28pt) to be set for grid.HeaderFont through the properties window
			style.HeaderFont = grid.Font;

			AddTextBoxReadOnlyColumnStyle(	"ShortName","Fund Name",300,HorizontalAlignment.Left,"FullName");
			AddTextBoxReadOnlyColumnStyle(	"FundStatus", "Price\nStatus ",150,HorizontalAlignment.Left, "Status");

			T.X();
		}

		/// <summary>
		/// Creates the actions.
		/// </summary>
		protected override StatusAction[] createActions()
		{
			T.E();
			
			StatusAction[] result = new StatusAction[2];

			//			result[0] = new StatusAction();
			//			result[0].Text = "&Refresh";
			//			result[0].Executed +=new EventHandler(Refresh_Executed);

			result[0] = new StatusAction();
			result[0].Text = "R&elease";
			result[0].Executed +=new EventHandler(Release_Executed);

//      Remove requested by KAJ and actioned by MAW 09/08/2005 (CR15)
//			result[1] = new StatusAction();
//			result[1].Text = "&Unrelease";
//			result[1].Executed +=new EventHandler(Unrelease_Executed);

			result[1] = new StatusAction();
			result[1].Text = "&Print";
			result[1].Executed +=new EventHandler(Print_Executed);

			//			result[3] = new StatusAction();
			//			result[3].Text = "&Export";
			//			result[3].Executed +=new EventHandler(Export_Executed);

			T.X();
			return result;
		}

		#endregion Overrides

		#region Event Handlers
		
//		private void Refresh_Executed(object sender, EventArgs e)
//		{
//			T.E();
//			this.refreshData();
//			T.X();
//		}

		private void Release_Executed(object sender, EventArgs e)
		{
			T.E();
			try
			{
				this.updateStatus(Fund.FundStatusType.SecondLevelAuthorised,Fund.FundStatusType.Released);
			}
			catch (ReleaseException ex)
			{
				MessageBoxHelper.ShowError(String.Format(ex.Message,((Fund)ex.Fund).HiPortfolioCode),"CannotReleaseTitle",ex);
			}
			T.X();
		}

//      Remove requested by KAJ and actioned by MAW 09/08/2005 (CR15)
//		private void Unrelease_Executed(object sender, EventArgs e)
//		{
//			T.E();
//			this.updateStatus(Fund.FundStatusType.Released,Fund.FundStatusType.SecondLevelAuthorised);
//			T.X();
//		}

		private void updateStatus(Fund.FundStatusType oldStatus,Fund.FundStatusType newStatus)
		{
			T.E();
			FundCollection funds = (FundCollection) view.grid.RetrieveEntireCustomCollection();
			FundController controller = new FundController(GlobalRegistry.ConnectionString);

			foreach(Fund fund in funds)
			{
				fund.IsDirty = (fund.FundStatus == oldStatus);
			}

			controller.ProgressFunds(funds,Fund.FundStatusType.Released);

			foreach(Fund fund in funds)
			{
				if (fund.IsDirty)
				{
					fund.FundStatus = newStatus;
					fund.IsDirty = false;
				}
			}
			
			view.Data = funds ;
			T.X();
		}

		private void Print_Executed(object sender, EventArgs e)
		{
			T.E();
			this.printGrid("Release Fund Prices");
			T.X();
		}

//		private void Export_Executed(object sender, EventArgs e)
//		{
//			T.E();
//			this.exportGrid("release_fund_prices.csv","HBOS.FS.AMP.UPD.WinUI.UserControls.AssetFundDetailViewControl.xslt");
//			T.X();
//		}

		#endregion

	}
}
