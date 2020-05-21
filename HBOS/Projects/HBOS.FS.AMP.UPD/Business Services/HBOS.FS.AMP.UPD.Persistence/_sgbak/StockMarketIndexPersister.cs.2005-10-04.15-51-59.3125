using System.Data;
using System.Data.SqlClient;
using HBOS.FS.AMP.UPD.Types.Currency;
using HBOS.FS.AMP.UPD.Types.StockMarketIndex;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// Summary description for StockMarketIndexPersister.
	/// </summary>
	public class StockMarketIndexPersister: EntityPersister
	{
		private string m_assetFundID = string.Empty;
		private CurrencyPricingPersister m_currencyPersister = null;

		/// <summary>
		/// Creates a new <see cref="StockMarketIndexPersister"/> instance.
		/// </summary>
		/// <param name="connectionString">Connection string to use.</param>
		public StockMarketIndexPersister(string connectionString): base(connectionString)
		{
		}

		/// <summary>
		/// Loads the specified market index ID with pricing information
		/// </summary>
		/// <param name="marketIndexID">Market index ID.</param>
		/// <param name="assetFundID">Company code to load pricing data for</param>
		/// <returns></returns>
		public StockMarketIndex LoadWithPricing(int marketIndexID, string assetFundID)
		{
			T.E();
			StockMarketIndex result = null;
			m_assetFundID = assetFundID;

			SqlParameter[] parameters = new SqlParameter[2];
			parameters[0] = new SqlParameter("@iMarketIndexId", SqlDbType.Int);
			parameters[0].Value = marketIndexID;
			parameters[1] = new SqlParameter("@cAssetFundId", SqlDbType.VarChar, 8);
			parameters[1].Value = m_assetFundID;

			result = (StockMarketIndex)LoadEntity("usp_StockMarketIndexGetWithPricing", parameters);	
			
			T.X();
			return result;
		}

		/// <summary>
		/// Loads the specified market index ID with pricing information
		/// </summary>
		/// <param name="marketIndexID">Market index ID.</param>
		/// <returns></returns>
		public StockMarketIndex LoadWithoutPricing(int marketIndexID)
		{
			T.E();
			StockMarketIndex result = null;

			SqlParameter[] parameters = new SqlParameter[1];
			parameters[0] = new SqlParameter("@iMarketIndexId",SqlDbType.Int);
			parameters[0].Value = marketIndexID;
			result = (StockMarketIndex)LoadEntity("usp_StockMarketIndexGetWithoutPricing",parameters);	
			
			T.X();
			return result;

		}


		/// <summary>
		/// Creates the stock market index
		/// </summary>
		/// <param name="safeReader">Safe reader.</param>
		/// <returns></returns>
		protected override object CreateEntity(SafeDataReader safeReader)
		{
			T.E();

			StockMarketIndex result;
			
			if (safeReader.ColumnExists("currentValue") && safeReader.ColumnExists("previousValue"))
			{
				if (null == m_currencyPersister)
				{
					m_currencyPersister = new CurrencyPricingPersister(this.ConnectionString);
					m_currencyPersister.AllowGlobalMarket = true;
				}
				
				Currency marketCurrency = m_currencyPersister.LoadRates(safeReader);
				
				result =  new StockMarketIndex
					(
						safeReader.GetInt32("marketIndexID"),
						safeReader.GetString("indexName"),
						safeReader.GetDecimal("currentValue"),
						safeReader.GetDecimal("previousValue"),
						marketCurrency
					);
			}
			else
			{
				result =  new StockMarketIndex
					(
						safeReader.GetInt32("marketIndexID"),
						safeReader.GetString("indexName"),
						new Currency(safeReader.GetString("currencyCode")),
						safeReader.GetString("countryCode")
					);			
			}
			
			T.X();
			return result;
		}
	}
}
