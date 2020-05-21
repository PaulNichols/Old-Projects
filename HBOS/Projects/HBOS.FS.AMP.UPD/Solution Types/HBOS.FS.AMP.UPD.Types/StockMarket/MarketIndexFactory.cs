using System;

namespace HBOS.FS.AMP.UPD.Types.AssetFunds
{
    /// <summary>
    /// Concrete Factory.  Implements the operations to create concrete product objects
    /// </summary>
    public class MarketIndexFactory
    {
		/// <summary>
		/// Creates a new <see cref="MarketIndexFactory"/> instance.
		/// </summary>
        public MarketIndexFactory()
        {
        }

        /// <summary>
        /// Create a new market index
        /// </summary>
        /// <returns>An empty market index object</returns>
        public static StockMarket CreateMarketIndex()
        {
            return new StockMarket();
        }

        /// <summary>
        /// Populate a market index object
        /// </summary>
        /// <param name="marketIndexID"></param>
        /// <param name="indexName"></param>
        /// <param name="countryCode"></param>
        /// <param name="currencyCode"></param>
        /// <returns>A populated stock market index object</returns>
        public static StockMarket CreateMarketIndex(int marketIndexID, string indexName, string countryCode, string currencyCode)
        {
            return new StockMarket(countryCode, indexName, currencyCode, marketIndexID);
            }
    }
}
