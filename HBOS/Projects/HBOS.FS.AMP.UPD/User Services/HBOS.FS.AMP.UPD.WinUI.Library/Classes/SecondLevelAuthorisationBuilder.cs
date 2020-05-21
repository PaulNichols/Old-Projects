using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.Types.FundGroups;
using HBOS.FS.AMP.UPD.Types.Funds;
using HBOS.FS.AMP.UPD.Types.Lookups;
using HBOS.FS.AMP.UPD.WinUI.UserControls;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// Factory for creating the second level authorisation process
	/// </summary>
	public class SecondLevelAuthorisationBuilder : IStatusViewBuilder
	{
		/// <summary>
		/// Creates a new <see cref="SecondLevelAuthorisationBuilder"/> instance.
		/// </summary>
		public SecondLevelAuthorisationBuilder()
		{
		}

		#region IStatusViewBuilder implementation

		/// <summary>
		/// Gets the data manager.
		/// </summary>
		/// <returns></returns>
		public StatusDataManager CreateDataManager()
		{
			return new FundStatusDataManager();
		}

		/// <summary>
		/// Gets the view controller.
		/// </summary>
		/// <param name="view">View.Must be an UpdateableStatusView though for 2nd level authorise</param>
		/// <param name="dataManager">Data manager.</param>
		/// <returns></returns>
		public StatusViewController CreateViewController(StatusView view, StatusDataManager dataManager)
		{
			return new SecondLevelAuthorisationViewController((UpdateableStatusView) view, dataManager);
		}


		/// <summary>
		/// Gets the fund group lookups.
		/// </summary>
		/// <returns></returns>
		public SimpleLookupCollection LoadFundGroupLookups()
		{
			FundGroupController controller = new FundGroupController(GlobalRegistry.ConnectionString);
			return controller.LoadFundGroupLookupsByCompany(GlobalRegistry.CompanyCode);
		}

		/// <summary>
		/// Creates the filters collection.
		/// </summary>
		/// <returns></returns>
		public StatusFilterCollection CreateFilters()
		{
			IStatusFilter statusFilter = new FundStatusFilter(Fund.FundStatusType.Missing, Fund.FundStatusType.SecondLevelAuthorised, false, string.Empty);
			statusFilter.Applied = true;

			return new StatusFilterCollection(new IStatusFilter[] {statusFilter});
		}

		#endregion 
	}
}