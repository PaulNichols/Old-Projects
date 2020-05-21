using HBOS.FS.AMP.UPD.WinUI.UserControls;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// Summary description for UpdateableStatusViewController.
	/// </summary>
	public abstract class UpdateableStatusViewController : StatusViewController
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="updateView"></param>
		/// <param name="dataManager"></param>
		protected UpdateableStatusViewController(UpdateableStatusView updateView, StatusDataManager dataManager) : base(updateView, dataManager)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		protected UpdateableStatusView updateableView
		{
			get { return (UpdateableStatusView) view; }
		}

		/// <summary>
		/// Loads the data into grid.
		/// </summary>
		protected override void loadDataToGrid()
		{
			T.E();
			base.loadDataToGrid();
			updateableView.Changed = false;
			T.X();
		}

		/// <summary>
		/// Refreshes the data from the database.
		/// </summary>
		protected override void refreshData()
		{
			T.E();
			base.refreshData();
			updateableView.Changed = false;
			T.X();
		}

	}
}