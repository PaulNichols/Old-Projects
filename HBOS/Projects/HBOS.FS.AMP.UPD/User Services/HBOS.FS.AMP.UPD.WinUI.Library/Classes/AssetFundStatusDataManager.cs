using System.Collections;
using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.WinUI.UserControls;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// Manages data for status views of asset funds
	/// </summary>
	public class AssetFundStatusDataManager : StatusDataManager
	{
		/// <summary>
		/// Creates a new <see cref="AssetFundStatusDataManager"/> instance.
		/// </summary>
		public AssetFundStatusDataManager() : base()
		{
			T.E();
		//	controller = new AssetFundController();
			T.X();
		}

		//private AssetFundController controller;

		/// <summary>
		/// Loads the data from the asset fund controller
		/// </summary>
		/// <returns></returns>
		protected override IList loadDataFromSource()
		{
			T.E();
			IList result = null;

			if (this.FundGroupFilter != null)
			{
				result = AssetFundController.LoadAssetFunds(GlobalRegistry.ConnectionString, this.FundGroupFilter.Key, true);
			}
			else
			{
				result = AssetFundController.LoadAssetFunds(GlobalRegistry.ConnectionString, GlobalRegistry.CompanyCode, true);
			}

			T.X();
			return AssetFundDecorator.ToDecoratedList(result);
		}

	}
}