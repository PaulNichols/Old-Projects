using HBOS.FS.AMP.UPD.Persistence;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Controllers
{
	/// <summary>
	/// Provides control for Asset fund movement constituant parts.
	/// </summary>
	public class AssetMovementConstituentController
	{
		#region Constructor

		/// <summary>
		/// Creates a new <see cref="AssetMovementConstituentController"/> instance.
		/// </summary>
		public AssetMovementConstituentController()
		{
		}

		#endregion

		#region Load methods

		/// <summary>
		/// Loads the all available benchmarks.
		/// </summary>
		/// <param name="connectionString">Connection string.</param>
		/// <returns></returns>
		public static AssetMovementConstituentCollection LoadAllAvailableBenchmarks(string connectionString)
		{
			T.E();
			AssetMovementConstituentCollection parts = null;

			try
			{
				AssetMovementConstituentPersister amcp=new AssetMovementConstituentPersister(connectionString);
				parts = amcp.LoadAllAvailableBenchmarks();
			}
			finally
			{
				T.X();
			}
			return parts;
		}

		#endregion

		#region Update methods

		/// <summary>
		/// Calls the Persister to associate the constituate parts with the specified Asset fund.
		/// </summary>
		/// <param name="parts">Parts.</param>
		/// <param name="assetFund">Asset fund.</param>
		/// <param name="connectionString"></param>
		public static void Update(AssetMovementConstituentCollection parts, AssetFund assetFund,string connectionString)
		{
			T.E();
			try
			{
				AssetMovementConstituentPersister amcp=new AssetMovementConstituentPersister(connectionString);
				amcp.Update(parts, assetFund);
			}
			finally
			{
				T.X();
			}
		}

		#endregion
	}

}