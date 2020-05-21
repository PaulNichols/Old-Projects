using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.Types.FundGroups;
using HBOS.FS.AMP.UPD.Types.Funds;
using HBOS.FS.AMP.UPD.Types.Lookups;
using HBOS.FS.AMP.UPD.WinUI.UserControls;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// View builder for the Release Fund Price view
	/// </summary>
	public class ReleaseFundViewBuilder : IStatusViewBuilder
	{
		/// <summary>
		/// Creates a new <see cref="ReleaseFundViewBuilder"/> instance.
		/// </summary>
		public ReleaseFundViewBuilder()
		{
			T.E();
			T.X();
		}

		#region IStatusViewBuilder Members

		/// <summary>
		/// Gets the data manager.
		/// </summary>
		/// <returns></returns>
		public StatusDataManager CreateDataManager()
		{
			T.E();
			StatusDataManager result = new ReleaseDataManager();
			T.X();
			return result;
		}

		/// <summary>
		/// Gets the view controller.
		/// </summary>
		/// <param name="view">View.</param>
		/// <param name="dataManager">Data manager.</param>
		/// <returns></returns>
		public StatusViewController CreateViewController(StatusView view, StatusDataManager dataManager)
		{
			T.E();
			StatusViewController result = new ReleaseFundViewController(view, dataManager);
			T.X();
			return result;
		}

		/// <summary>
		/// Gets the fund group lookups.
		/// </summary>
		/// <returns></returns>
		public SimpleLookupCollection LoadFundGroupLookups()
		{
			T.E();
			FundGroupController controller = new FundGroupController(GlobalRegistry.ConnectionString);
			FundGroupCollection fundGroups = controller.LoadFundGroupsByCompany(GlobalRegistry.CompanyCode);

			SimpleLookupCollection result = new SimpleLookupCollection();
			foreach (FundGroup fundGroup in fundGroups)
			{
				if (fundGroup.ForRelease)
				{
					result.Add(new SimpleLookup(fundGroup.ID, fundGroup.FullName));
				}
			}

			T.X();
			return result;
		}

		/// <summary>
		/// Creates the filters collection.
		/// </summary>
		/// <returns></returns>
		public StatusFilterCollection CreateFilters()
		{
			T.E();
			StatusFilterCollection result = new StatusFilterCollection();
			FundStatusFilter filter =
				new FundStatusFilter(Fund.FundStatusType.Missing, Fund.FundStatusType.Released, false, string.Empty);
			filter.Applied = true;
			result.Add(filter);
			T.X();
			return result;
		}

		#endregion
	}
}