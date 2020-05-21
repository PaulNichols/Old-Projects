using HBOS.FS.AMP.UPD.WinUI.UserControls;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// Summary description for UpdateableStatusViewFactory.
	/// </summary>
	/// <pattern>
	/// Builder pattern
	/// </pattern>
	public abstract /*static*/ class UpdateableStatusViewFactory
	{
		private UpdateableStatusViewFactory()
		{
		}

		/// <summary>
		/// Creates and initialises the view for display
		/// </summary>
		/// <param name="builder">A builder used to construct the instance</param>
		/// <returns>The initialised view</returns>
		public static UpdateableStatusView CreateView(IStatusViewBuilder builder)
		{
			T.E();
			UpdateableStatusView view = new UpdateableStatusView();

			StatusDataManager dataManager = builder.CreateDataManager();

			StatusViewController viewController = builder.CreateViewController(view, dataManager);

			viewController.Initialise();
			view.FundGroupLookups = builder.LoadFundGroupLookups();

			StatusFilterCollection filters = builder.CreateFilters();
			dataManager.Filters = filters;
			view.Filters = filters;

			T.X();
			return view;
		}

	}
}