using System;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.Currency;
using HBOS.FS.AMP.UPD.Types.Factors;
using HBOS.FS.AMP.UPD.Types.FundGroups;
using HBOS.FS.AMP.UPD.Types.Funds;
using HBOS.FS.AMP.UPD.Types.StockMarketIndex;

namespace UT_HBOS.FS.AMP.UPD.Types
{
	/// <summary>
	/// Static helpers for the MVSII calculation tests
	/// </summary>
	public abstract class MVSIICalcHelpers
	{
		/// <summary>
		/// Creates a new <see cref="MVSIICalcHelpers"/> instance.
		/// </summary>
		protected MVSIICalcHelpers()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// Creates the weighted index.
		/// </summary>
		/// <param name="marketMovement">Market movement.</param>
		/// <param name="currencyMovement">Currency movement.</param>
		/// <param name="proportion">Proportion.</param>
		/// <returns>Suitable IWeightedMovement for OEIC and Linked asset funds</returns>
		public static AssetMovementConstituent CreateWeightedBenchmark(decimal marketMovement, decimal currencyMovement, decimal proportion)
		{
			int marketIndexID = 1;
			//string assetFundCode = "AFCode";
			string indexName = "Index";
//			bool marketMovementSet = true;
//			int importedIndexValueSnapshotID = 1;
//			bool currencyMovementSet = true;
//			int currencyRatesSnapshotID = 1;
//			string countryCode = "UK";
			string currencyCode = "GBP";
//			string country = "UK";
//			bool proportionSet = true;
//			int snapshotID = 1;
//			bool fromAuthorisedAssetFundDetails = false;
//			byte[] timestamp = new byte[1];

			Currency marketCurrency=new Currency(currencyCode,1.5m,1.3m);
			StockMarketIndex smi=new StockMarketIndex(marketIndexID,indexName,6m,5.5m,marketCurrency);
			AssetMovementConstituent assetMovementConstituent = new AssetMovementConstituent(
																	proportion,
																	smi);
			return assetMovementConstituent;
		}

//		/// <summary>
//		/// Creates the composite weighting.
//		/// </summary>
//		/// <param name="linkedFundMovement">Linked fund movement.</param>
//		/// <param name="proportion">Proportion.</param>
//		/// <returns></returns>
//		public static IWeightedMovement CreateCompositeWeighting(decimal linkedFundMovement, decimal proportion)
//		{
//			string linkedFundCode = "LinkedFund";
//
//			IWeightedMovement weightedMovement = new CompositeWeighting(linkedFundCode, linkedFundMovement, proportion, true);
//			return weightedMovement;
//		}

		/// <summary>
		/// Creates the linked asset fund.
		/// </summary>
		/// <param name="unitPrice">Unit price.</param>
		/// <param name="previousUnitPrice">Previous unit price.</param>
		/// <param name="unitPriceMovement">Unit price movement.</param>
		/// <returns>A Linked Asset Fund</returns>
		public static AssetFund CreateLinkedAssetFund(decimal unitPrice, decimal previousUnitPrice, decimal unitPriceMovement)
		{
			string assetFundCode = "AssetFundCode";
			string shortName = "AFShort";
			string fullName = "AFFull";
			string companyCode = "HIFM";
			DateTime valuationPoint = DateTime.Now;
			bool previousUnitPriceSet = true;
			decimal authorisedUnitPrice = 0.0m;
			bool authorisedUnitPriceSet = false;
			bool lockedForCalculation = false;
			decimal assetMovementTolerance = 0.0m;
			bool unitPriceSet = true;
			bool unitPriceMovementSet = true;
			bool assetMovementToleranceSet = true;
			bool valuationPointSet = true;
			byte[] timeStamp = new byte[1];

			AssetFund af = new AssetFund(
				AssetFund.AssetFundTypeEnum.Linked,
				assetFundCode,
			    shortName,
				fullName,
			    companyCode,
			    unitPrice,
			    valuationPoint,
			    unitPriceMovement,
			    previousUnitPrice,
			    previousUnitPriceSet,
			    authorisedUnitPrice,
			    authorisedUnitPriceSet,
			    lockedForCalculation,
			    assetMovementTolerance,
			    unitPriceSet,
			    unitPriceMovementSet,
			    assetMovementToleranceSet,
			    valuationPointSet,
			    timeStamp,
			    null,
				null);
			return af;
		}

		/// <summary>
		/// Creates the composite asset fund.
		/// </summary>
		/// <param name="unitPrice">Unit price.</param>
		/// <param name="previousUnitPrice">Previous unit price.</param>
		/// <param name="unitPriceMovement">Unit price movement.</param>
		/// <returns>A Composite Asset Fund</returns>
		public static AssetFund CreateCompositeAssetFund(decimal unitPrice, decimal previousUnitPrice, decimal unitPriceMovement)
		{
			string assetFundCode = "AssetFundCode";
			string shortName = "AFShort";
			string fullName = "AFFull";
			byte[] timeStamp = new byte[1];
			FundGroupCollection fundGroups = null;
			AssetMovementConstituentCollection constituentCollection = null;
			string companyCode = "HBOS";
			Currency currency = Currency.GBPound;

			AssetFund af = new AssetFund(
				AssetFund.AssetFundTypeEnum.Composite,
				assetFundCode,
				shortName,
				fullName,
				companyCode,
				timeStamp,
				fundGroups,
				constituentCollection,
				currency);

			return af;
		}

		/// <summary>
		/// Creates the OEIC asset fund.
		/// </summary>
		/// <param name="unitPrice">Unit price.</param>
		/// <param name="previousUnitPrice">Previous unit price.</param>
		/// <param name="unitPriceMovement">Unit price movement.</param>
		/// <returns>An OEIC Asset Fund</returns>
		public static AssetFund CreateOEICAssetFund(decimal unitPrice, decimal previousUnitPrice, decimal unitPriceMovement)
		{
			string assetFundCode = "AssetFundCode";
			string shortName = "AFShort";
			string fullName = "AFFull";
			string companyCode = "HIFM";
			DateTime valuationPoint = DateTime.Now;
			bool previousUnitPriceSet = true;
			decimal authorisedUnitPrice = 0.0m;
			bool authorisedUnitPriceSet = false;
			bool lockedForCalculation = false;
			decimal assetMovementTolerance = 0.0m;
			bool unitPriceSet = true;
			bool unitPriceMovementSet = true;
			bool assetMovementToleranceSet = true;
			bool valuationPointSet = true;
			byte[] timeStamp = new byte[1];

			AssetFund af = new AssetFund(
				AssetFund.AssetFundTypeEnum.Oeic,
				assetFundCode,
			    shortName,
			    fullName,
			    companyCode,
			    unitPrice,
			    valuationPoint,
			    unitPriceMovement,
			    previousUnitPrice,
			    previousUnitPriceSet,
			    authorisedUnitPrice,
			    authorisedUnitPriceSet,
			    lockedForCalculation,
			    assetMovementTolerance,
			    unitPriceSet,
			    unitPriceMovementSet,
			    assetMovementToleranceSet,
			    valuationPointSet,
			    timeStamp,
			    null,
				null);

			return af;
		}


		/// <summary>
		/// Creates the linked fund.
		/// </summary>
		/// <param name="price">Price.</param>
		/// <param name="previousPrice">Previous price.</param>
		/// <param name="parentAssetFund">The parent asset fund.</param>
		/// <returns>An initialised linked fund instance</returns>
		public static Fund CreateLinkedFund(decimal price, decimal previousPrice, AssetFund parentAssetFund)
		{
			string hiPortfolioCode = "hiPortfolioCode";
			string fullName = "fullName";
			string shortName = "shortName";
			string classOrSeriesCode = "class";
			bool onHiPortfolio3 = true;

//			decimal taxProvision = 0.0m;
//			int taxProvisionFactorID = 1;
//			bool taxProvisionFactorIDSet = true;

		//	DateTime scalingFactorEffectiveDate = System.DateTime.Now;
            
//			decimal xFactor = 0.0m; 
//			int xFactorID = 1;
//			bool xFactorIDSet = true;
//			DateTime xFactorEffectiveDate = System.DateTime.Now;
//			bool xFactorEffectiveDateSet = true;
//			string xFactorDescription = "XFACTOR";
            
			decimal assetMovementTolerance = 0.0m;
			decimal upperTolerance = 0.0m;
			decimal lowerTolerance = 0.0m;
			bool priceIncreaseOnly = false;
			int tolerancesID = 1;
		//	bool tolerancesIDSet = true;
			bool priceSet = true;
            
			long importedFundPriceID = 1;
			//bool importedFundPriceIDSet = true;
			decimal valuationBasisEffect = 0.0m;
//			bool valuationBasisEffectSet = true;
//			int valuationBasisID = 1;
//			bool valuationBasisIDSet = true;
			bool previousPriceSet = true;
			Fund.FundStatusType fundStatus = Fund.FundStatusType.Imported;
			DateTime statusChangedTime = DateTime.Now;
			bool statusChangedTimeSet = true;
			bool usePredictedPrice = false;
			byte[] authorisedPriceTimestamp = new byte[1];
			byte[] timestamp = new byte[1];
			bool isExDividend = true;
//			byte[] xFactorTimestamp = new byte[1];
//			byte[] oeicTimestamp = new byte[1];
			string parentAssetFundID = "AF1";
            bool isBenchMarkable = true;


			bool isLife=true;
			bool isDualPrice=true;
			bool midPriceAsBidPrice=true;

			Fund newFund= (OEICFund) FundFactory.CreateFund(
				FundFactory.FundType.Oeic,
				hiPortfolioCode,
				fullName,
				shortName,
				classOrSeriesCode,
				midPriceAsBidPrice,
				onHiPortfolio3,
				isDualPrice,
				parentAssetFund,
				assetMovementTolerance,
				upperTolerance,
				lowerTolerance,
				priceIncreaseOnly,
				tolerancesID,
				price,
				priceSet,
				importedFundPriceID,
				valuationBasisEffect,
				previousPrice,
				previousPriceSet,
				fundStatus,
				statusChangedTime,
				statusChangedTimeSet,
				usePredictedPrice,
				authorisedPriceTimestamp,
				timestamp,
				isLife,
				isExDividend,
				parentAssetFundID, 
				"companycode","securitycode",new FactorCollection(),
                isBenchMarkable);
									
			return newFund;
		}

		/// <summary>
		/// Creates an OEIC fund.
		/// </summary>
		/// <param name="price">Price.</param>
		/// <param name="previousPrice">Previous price.</param>
		/// <param name="parentAssetFund">The parent asset fund.</param>
		/// <returns>An initialised OEIC fund instance</returns>
		public static Fund CreateOEICFund(decimal price, decimal previousPrice, AssetFund parentAssetFund)
		{
			
            string hiPortfolioCode = "hiPortfolioCode";
            string fullName = "fullName";
            string shortName = "shortName";
            string classOrSeriesCode = "class";
            bool onHiPortfolio3 = true;

//            decimal taxProvision = 0.0m;
//            int taxProvisionFactorID = 1;
//            bool taxProvisionFactorIDSet = true;

          //  DateTime scalingFactorEffectiveDate = System.DateTime.Now;
            
//            decimal xFactor = 0.0m; 
//            int xFactorID = 1;
//            bool xFactorIDSet = true;
//            DateTime xFactorEffectiveDate = System.DateTime.Now;
//            bool xFactorEffectiveDateSet = true;
//            string xFactorDescription = "XFACTOR";
            
            decimal assetMovementTolerance = 0.0m;
            decimal upperTolerance = 0.0m;
            decimal lowerTolerance = 0.0m;
            bool priceIncreaseOnly = false;
            int tolerancesID = 1;
           // bool tolerancesIDSet = true;
            bool priceSet = true;
            
            long importedFundPriceID = 1;
          //  bool importedFundPriceIDSet = true;
            decimal valuationBasisEffect = 0.0m;
          //  bool valuationBasisEffectSet = true;
          //  int valuationBasisID = 1;
          //  bool valuationBasisIDSet = true;
            bool previousPriceSet = true;
			Fund.FundStatusType fundStatus = Fund.FundStatusType.Imported;
            DateTime statusChangedTime = DateTime.Now;
            bool statusChangedTimeSet = true;
            bool usePredictedPrice = false;
            byte[] authorisedPriceTimestamp = new byte[1];
            byte[] timestamp = new byte[1];
            bool isExDividend = true;
//            byte[] xFactorTimestamp = new byte[1];
//            byte[] oeicTimestamp = new byte[1];
            string parentAssetFundID = "AF1";


			bool isLife=true;
			bool isDualPrice=true;
			bool midPriceAsBidPrice=true;
            bool isBenchMarkable = true;

			Fund newFund= (OEICFund) FundFactory.CreateFund(
				FundFactory.FundType.Linked,
									hiPortfolioCode,
                                    fullName,
                                    shortName,
                                    classOrSeriesCode,
									midPriceAsBidPrice,
                                    onHiPortfolio3,
									isDualPrice,
                                    parentAssetFund,
                                    assetMovementTolerance,
                                    upperTolerance,
                                    lowerTolerance,
                                    priceIncreaseOnly,
                                    tolerancesID,
                                    price,
                                    priceSet,
                                    importedFundPriceID,
                                    valuationBasisEffect,
                                    previousPrice,
                                    previousPriceSet,
                                    fundStatus,
                                    statusChangedTime,
                                    statusChangedTimeSet,
                                    usePredictedPrice,
                                    authorisedPriceTimestamp,
                                    timestamp,
									isLife,
                                    isExDividend,
                                    parentAssetFundID, 
									"companycode","securitycode",new FactorCollection(),
                                    isBenchMarkable);
									
			return newFund;
		}


		/// <summary>
		/// Creates the composite fund.
		/// </summary>
		/// <param name="price">Price.</param>
		/// <param name="previousPrice">Previous price.</param>
		/// <param name="parentAssetFund">The parent asset fund.</param>
		/// <returns>An initialised composite fund instance</returns>
		public static Fund CreateCompositeFund(decimal price, decimal previousPrice, AssetFund parentAssetFund)
		{
			string hiPortfolioCode = "hiPortfolioCode";
			string fullName = "fullName";
			string shortName = "shortName";
			string classOrSeriesCode = "class";
			bool onHiPortfolio3 = true;

//			decimal taxProvision = 0.0m;
//			int taxProvisionFactorID = 1;
//			bool taxProvisionFactorIDSet = true;

		//   
//			decimal xFactor = 0.0m; 
//			int xFactorID = 1;
//			bool xFactorIDSet = true;
//			DateTime xFactorEffectiveDate = System.DateTime.Now;
//			bool xFactorEffectiveDateSet = true;
//			string xFactorDescription = "XFACTOR";
            
			decimal assetMovementTolerance = 0.0m;
			decimal upperTolerance = 0.0m;
			decimal lowerTolerance = 0.0m;
			bool priceIncreaseOnly = false;
			int tolerancesID = 1;
		//	bool tolerancesIDSet = true;
			bool priceSet = true;
            
			long importedFundPriceID = 1;
			//bool importedFundPriceIDSet = true;
			decimal valuationBasisEffect = 0.0m;
//			bool valuationBasisEffectSet = true;
//			int valuationBasisID = 1;
//			bool valuationBasisIDSet = true;
			bool previousPriceSet = true;
			Fund.FundStatusType fundStatus = Fund.FundStatusType.Imported;
			DateTime statusChangedTime = DateTime.Now;
			bool statusChangedTimeSet = true;
			bool usePredictedPrice = false;
			byte[] authorisedPriceTimestamp = new byte[1];
			byte[] timestamp = new byte[1];
			bool isExDividend = true;
//			byte[] xFactorTimestamp = new byte[1];
//			byte[] oeicTimestamp = new byte[1];
			string parentAssetFundID = "AF1";


			bool isLife=true;
			bool isDualPrice=true;
			bool midPriceAsBidPrice=true;
            bool isBenchMarkable = true;

			Fund newFund= (OEICFund) FundFactory.CreateFund(
				FundFactory.FundType.Composite,
				hiPortfolioCode,
				fullName,
				shortName,
				classOrSeriesCode,
				midPriceAsBidPrice,
				onHiPortfolio3,
				isDualPrice,
				parentAssetFund,
				assetMovementTolerance,
				upperTolerance,
				lowerTolerance,
				priceIncreaseOnly,
				tolerancesID,
				price,
				priceSet,
				importedFundPriceID,
				valuationBasisEffect,
				previousPrice,
				previousPriceSet,
				fundStatus,
				statusChangedTime,
				statusChangedTimeSet,
				usePredictedPrice,
				authorisedPriceTimestamp,
				timestamp,
				isLife,
				isExDividend,
				parentAssetFundID, 
				"companycode","securitycode",new FactorCollection(),
                isBenchMarkable);
									
			return newFund;
		}

	}
}