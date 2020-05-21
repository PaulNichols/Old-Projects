using HBOS.FS.AMP.UPD.Types.Lookups;
using HBOS.FS.AMP.UPD.WinUI.UserControls;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// Defines an interface to be used by an object the builds a status view
	/// </summary>
	public interface IStatusViewBuilder
	{
		/// <summary>
		/// Gets the data manager.
		/// </summary>
		/// <returns></returns>
		StatusDataManager CreateDataManager();

		/// <summary>
		/// Gets the view controller.
		/// </summary>
		/// <param name="view">View.</param>
		/// <param name="dataManager">Data manager.</param>
		/// <returns></returns>
		StatusViewController CreateViewController(StatusView view, StatusDataManager dataManager);

		/// <summary>
		/// Gets the fund group lookups.
		/// </summary>
		/// <returns></returns>
		SimpleLookupCollection LoadFundGroupLookups();

		/// <summary>
		/// Creates a list of filters to apply to the data set. Can be null if no filters.
		/// </summary>
		/// <returns></returns>
		StatusFilterCollection CreateFilters();
	}


	/// <summary>
	/// Summary description for StatusViewFactory.
	/// </summary>
	/// <pattern>
	/// Builder pattern
	/// </pattern>
	public abstract /*static*/ class StatusViewFactory
	{
		private StatusViewFactory()
		{
		}

		/// <summary>
		/// Creates and initialises the view for display
		/// </summary>
		/// <param name="builder">A builder used to construct the instance</param>
		/// <returns>The initialised view</returns>
		public static StatusView CreateView(IStatusViewBuilder builder)
		{
			T.E();
			StatusView view = new StatusView();

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