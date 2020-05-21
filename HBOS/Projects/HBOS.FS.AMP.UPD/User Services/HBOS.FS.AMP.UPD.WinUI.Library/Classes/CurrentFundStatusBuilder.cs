using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.Types.Funds;
using HBOS.FS.AMP.UPD.Types.Lookups;
using HBOS.FS.AMP.UPD.WinUI.UserControls;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// Factory for creating the second level authorisation process
	/// </summary>
	public class CurrentFundStatusBuilder : IStatusViewBuilder
	{
		/// <summary>
		/// Creates a new <see cref="SecondLevelAuthorisationBuilder"/> instance.
		/// </summary>
		public CurrentFundStatusBuilder()
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
		/// <param name="view">View.</param>
		/// <param name="dataManager">Data manager.</param>
		/// <returns></returns>
		public StatusViewController CreateViewController(StatusView view, StatusDataManager dataManager)
		{
			return new CurrentFundStatusController(view, dataManager);
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
			StatusFilterCollection returnCollection =new StatusFilterCollection();
			returnCollection.Add(new PropertyFilter(new PropertyFilter.NameValuePair[]{new PropertyFilter.NameValuePair("WithinAssetMovementTolerance",true),new PropertyFilter.NameValuePair("PriceOutsideTolerance",false)},true,"Auth: 1st level"));
			returnCollection.Add(new PropertyFilter(new PropertyFilter.NameValuePair[]{new PropertyFilter.NameValuePair("FundStatus",Fund.FundStatusType.SecondLevelAuthorised)},true,"Auth: 2nd level"));
			returnCollection.Add(new FundStatusFilterNegative(new Fund.FundStatusType[]{Fund.FundStatusType.SecondLevelAuthorised},true,"Auth: Not 2nd level"));
			returnCollection.Add(new FundStatusFilterNegative(new Fund.FundStatusType[]{Fund.FundStatusType.FirstLevelAuthorised , Fund.FundStatusType.SecondLevelAuthorised},true,"Auth: Not 1st or 2nd level"));
			returnCollection.Add(new PropertyFilter(new PropertyFilter.NameValuePair[]{new PropertyFilter.NameValuePair("SecondLevelButNotFirst",true)},true,"Auth: 2nd, but not first level"));
			
            // Amended by MAW 14/09/05 (Issue: UA132)
            // The value here needs to be false as we are checking to see if it is outside tolerance on a field
            // that indicates the field is within tolerance
            returnCollection.Add(new PropertyFilter(new PropertyFilter.NameValuePair[]{new PropertyFilter.NameValuePair("WithinAssetMovementTolerance",false)},true,"Tolerance: Asset Fund Movement outside"));
			returnCollection.Add(new PropertyFilter(new PropertyFilter.NameValuePair[]{new PropertyFilter.NameValuePair("PriceOutsideTolerance",true)},true,"Tolerance: Price outside"));

            // Amended by MAW 14/09/05 (Issue: UA132)
            // Fixed spelling
			returnCollection.Add(new PropertyFilter(new PropertyFilter.NameValuePair[]{new PropertyFilter.NameValuePair("FundStatus",Fund.FundStatusType.Released)},true,"Released"));
			returnCollection.Add(new PropertyFilter(new PropertyFilter.NameValuePair[]{new PropertyFilter.NameValuePair("FundStatus",Fund.FundStatusType.Distributed)},true,"Distributed"));

			return returnCollection;
		}

		#endregion 
	}
}