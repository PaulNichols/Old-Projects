using System.Data;
using System.Data.SqlClient;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.Currency;
using HBOS.FS.Support.Tex;
using Microsoft.ApplicationBlocks.Data;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// Summary description for AssetFundBenchMarkPersister.
	/// </summary>
	public class AssetFundBenchMarkPersister : AssetFundStaticDataPersister
	{
		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="connectionString"></param>
		public AssetFundBenchMarkPersister(string connectionString) : base(connectionString)
		{
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Determines whether [the benchmark is related to the asset fund] [the specified initialAssetFundCode].
		/// This checks for a circular reference back to the initial asset fund from any related 
		/// child funds' parent asset fund.
		/// </summary>
		/// <param name="initialAssetFundCode">Initial asset fund code.</param>
		/// <param name="benchmarkFundCode">Benchmark fund code.</param>
		/// <returns>String: empty when false; populated with relation when true</returns>
		public string IsBenchmarkRelatedToAssetFund(string initialAssetFundCode, string benchmarkFundCode)
		{
			T.E();
			const string sp = "usp_IsBenchmarkRelatedToAssetFund";

			// Create parameter object
			SqlParameter[] spParams = new SqlParameter[4];

			// Build parameters
			spParams[0] = new SqlParameter("@assetFundCode", SqlDbType.Char, 8);
			spParams[1] = new SqlParameter("@benchmarkFundCode", SqlDbType.Char, 10);
			spParams[2] = new SqlParameter("@messageText", SqlDbType.VarChar, 100);
			spParams[2].Direction = ParameterDirection.Output;

			// Assign values to parameters
			spParams[0].Value = initialAssetFundCode;
			spParams[1].Value = benchmarkFundCode;

			// Call the stored procedure
			try
			{
				SqlHelper.ExecuteNonQuery(this.ConnectionString, CommandType.StoredProcedure, sp, spParams);
			}
			catch (SqlException ex)
			{
				ThrowDBException(ex, sp, spParams);
			}
			finally
			{
				T.X();
			}

			return spParams[2].Value as string;
		}

		#endregion

		#region Entity methods

		/// <summary>
		/// Creates the entity.
		/// </summary>
		/// <param name="reader">Reader.</param>
		/// <returns>AssetFund object</returns>
		protected override AssetFund createEntity(SafeDataReader reader)
		{
			AssetFund newAssetFund = null;
			T.E();
			try
			{
				//Create a standard object (was previously a light object, but now these are removed),
				//but just set the properties we're interested in and don't bother to load up any other data
				//also retrieve its fund groups and currency information
				AssetFund.AssetFundTypeEnum afType = resolveDBTypeToAssetFundType(reader.GetString("fundType"));
				Currency currency = new Currency(reader.GetString("currencyCode"));
				newAssetFund = new AssetFund(afType,
				                             null,
				                             null,
				                             null,
				                             null,
				                             null,
				                             null,
				                             null,
				                             currency,0);

			}
			finally
			{
				T.X();
			}
			return newAssetFund;
		}

		#endregion
	}
}