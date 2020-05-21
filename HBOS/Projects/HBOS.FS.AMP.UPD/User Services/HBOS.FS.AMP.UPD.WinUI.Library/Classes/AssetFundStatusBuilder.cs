using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.Types.FundGroups;
using HBOS.FS.AMP.UPD.Types.Lookups;
using HBOS.FS.AMP.UPD.WinUI.UserControls;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// Factory for creating the second level authorisation process
	/// </summary>
	public class AssetFundStatusBuilder : IStatusViewBuilder
	{
		/// <summary>
		/// Creates a new <see cref="SecondLevelAuthorisationBuilder"/> instance.
		/// </summary>
		public AssetFundStatusBuilder()
		{
		}

		#region IStatusViewBuilder implementation

		/// <summary>
		/// Gets the data manager.
		/// </summary>
		/// <returns></returns>
		public StatusDataManager CreateDataManager()
		{
			return new AssetFundStatusDataManager();
		}

		/// <summary>
		/// Gets the view controller.
		/// </summary>
		/// <param name="view">View.</param>
		/// <param name="dataManager">Data manager.</param>
		/// <returns></returns>
		public StatusViewController CreateViewController(StatusView view, StatusDataManager dataManager)
		{
			return new AssetFundStatusController(view, dataManager);
		}

		/// <summary>
		/// Gets the fund group lookups.
		/// </summary>
		/// <returns></returns>
		public SimpleLookupCollection LoadFundGroupLookups()
		{
			FundGroupController controller = new FundGroupController(GlobalRegistry.ConnectionString);
			return controller.LoadFundGroupsByCompanyAndTypeLookUp(GlobalRegistry.CompanyCode, FundGroupFactory.FundGroupTypes.Asset);
		}

		/// <summary>
		/// Creates the filters collection.
		/// </summary>
		/// <returns></returns>
		public StatusFilterCollection CreateFilters()
		{
			StatusFilterCollection returnCollection = new StatusFilterCollection();
			returnCollection.Add(new PropertyFilter(new PropertyFilter.NameValuePair[]{new PropertyFilter.NameValuePair("WithinAssetMovementTolerance",true)},true,"Within Tolerance"));
			returnCollection.Add(new PropertyFilter(new PropertyFilter.NameValuePair[]{new PropertyFilter.NameValuePair("WithinAssetMovementTolerance",false)},true,"Outside Tolerance"));
			return returnCollection;
		}

		#endregion 
	}
}