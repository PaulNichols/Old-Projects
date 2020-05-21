using System;

namespace HBOS.FS.AMP.UPD.Types.Status
{
	/// <summary>
	/// Class containing information on the current system valuation point and the latest import files which
	/// decide that point.
	/// </summary>
	public class CurrentImportStatus
	{
        // Current Active Valuation Point
        private DateTime currentValuationPoint;

		// Current Active Valuation Day
        private DateTime currentValuationDay;

        /// <summary>
        /// The current market index values import details
        /// </summary>
		public ImportDetails MarketIndexValues;

		/// <summary>
		/// The current fund price import details
		/// </summary>
        public ImportDetails FundPrices;

		/// <summary>
		/// The current currency rates import details
		/// </summary>
        public ImportDetails CurrencyRates;

		/// <summary>
		/// The current asset fund market splits import details
		/// </summary>
        public ImportDetails AssetFundMarketSplits;

        /// <summary>
        /// Creates a new instance of the <see cref="CurrentImportStatus"/> class.
        /// </summary>
        /// <remarks>Creation should be done from a call to the controller/persistence layer.</remarks>      
        public CurrentImportStatus(DateTime valuationPoint)
        {
            this.currentValuationPoint = valuationPoint;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="CurrentImportStatus"/> class.
        /// </summary>
        /// <remarks>Creation should be done from a call to the controller/persistence layer.</remarks>
        /// <param name="valuationPoint"></param>
        /// <param name="marketIndexFile"></param>
        /// <param name="marketIndexImportedByUserName"></param>
        /// <param name="marketIndexImportedByLogin"></param>
        /// <param name="marketIndexImportedOn"></param>
        /// <param name="fundPriceFile"></param>
        /// <param name="fundPriceImportedByUserName"></param>
        /// <param name="fundPriceImportedByLogin"></param>
        /// <param name="fundPriceImportedOn"></param>
        /// <param name="currencyFile"></param>
        /// <param name="currencyImportedByUserName"></param>
        /// <param name="currencyImportedByLogin"></param>
        /// <param name="currencyImportedOn"></param>
        /// <param name="assetSplitFile"></param>
        /// <param name="assetSplitImportedByUserName"></param>
        /// <param name="assetSplitImportedByLogin"></param>
        /// <param name="assetSplitImportedOn"></param>
        /// <param name="valuationDay"></param>
        public CurrentImportStatus(
			DateTime valuationDay,
			DateTime valuationPoint,
            // Market Index Values
            string marketIndexFile,
            string marketIndexImportedByUserName,
            string marketIndexImportedByLogin,
            DateTime marketIndexImportedOn,
            // Fund Prices
            string fundPriceFile,
            string fundPriceImportedByUserName,
            string fundPriceImportedByLogin,
            DateTime fundPriceImportedOn,
            // Currency Rates
            string currencyFile,
            string currencyImportedByUserName,
            string currencyImportedByLogin,
            DateTime currencyImportedOn,
            // Asset Fund Market Splits
            string assetSplitFile,
            string assetSplitImportedByUserName,
            string assetSplitImportedByLogin,
            DateTime assetSplitImportedOn
            )
		{
			
			currentValuationDay=new DateTime(valuationDay.Year,valuationDay.Month, valuationDay.Day );
            this.currentValuationPoint = valuationPoint;
            this.MarketIndexValues = new ImportDetails(marketIndexFile, marketIndexImportedByUserName, marketIndexImportedByLogin, marketIndexImportedOn);
            this.FundPrices = new ImportDetails(fundPriceFile, fundPriceImportedByUserName, fundPriceImportedByLogin, fundPriceImportedOn);
            this.CurrencyRates = new ImportDetails(currencyFile, currencyImportedByUserName, currencyImportedByLogin, currencyImportedOn);
            this.AssetFundMarketSplits = new ImportDetails( assetSplitFile,
                assetSplitImportedByUserName, assetSplitImportedByLogin, assetSplitImportedOn);
        }

		/// <summary>
        /// Returns the current valuation day for the company.
        /// </summary>
        /// <value>The current valuation day for the company.</value>
        public DateTime CurrentValuationDay
        {
            get 
            {
                return this.currentValuationDay;
            }
        }
        /// <summary>
        /// Returns the current system valuation point.
        /// </summary>
        /// <value>The current system valuation point.</value>
        public DateTime CurrentValuationPoint
        {
            get 
            {
                return this.currentValuationPoint;
            }
        }

	}
}
