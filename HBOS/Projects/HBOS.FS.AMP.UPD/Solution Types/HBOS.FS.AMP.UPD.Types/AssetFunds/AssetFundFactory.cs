using System;
using HBOS.FS.AMP.UPD.Types.FundGroups;

namespace HBOS.FS.AMP.UPD.Types.AssetFunds
{
	/// <summary>
	/// Summary description for AssetFundFactory.
	/// </summary>
	public class AssetFundFactory
	{
		/// <summary>
		/// The type of asset fund to be created. These are the same enumerated 
		/// values as FundType but are as a different enum for future proofing
		/// in case fund types and asset fund types ever differ.
		/// An asset fund of one of these types can only hold funds of the relevant type.
		/// </summary>
		public enum AssetFundType : int
		{
			/// <summary>
			/// LinkedFund
			/// </summary>
			Linked = 0,

			/// <summary>
			/// Open Ended Investment Company (OEIC) fund type
			/// </summary>
			Oeic = 1,

			/// <summary>
			/// Composite Price Series
			/// </summary>
			Composite = 2
		}

		/// <summary>
		/// Checks the specific type of the abstract class and returns its type as an enum
		/// </summary>
		/// <param name="assetFund"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentException">Invalid asset fund type</exception>
		public static AssetFundType ResolveAssetFundType(AssetFund assetFund)
		{
			OEICAssetFund oeic = null;
			LinkedAssetFund linked = null;
			CompositeAssetFund comp = null;
			return ResolveAssetFundType(assetFund, out oeic, out linked, out comp);
		}

		/// <summary>
		/// Encapsulates the type checking of the abstract base class.
		/// Throws exception if not of any type. 
		/// Returns two null references and the reference to the specific object type, as well as the return enum type
		/// </summary>
		/// <param name="assetFund"></param>
		/// <param name="oeic"></param>
		/// <param name="linked"></param>
		/// <param name="comp"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentException">Invalid asset fund type</exception>
		public static AssetFundType ResolveAssetFundType(AssetFund assetFund, out OEICAssetFund oeic, out LinkedAssetFund linked, out CompositeAssetFund comp)
		{
			//this section is to save casting multiple times later
			oeic = assetFund as OEICAssetFund;
			comp = null;
			linked = null;

			AssetFundType retType;

			if (oeic == null)
			{
				linked = assetFund as LinkedAssetFund;
				if (linked == null)
				{
					comp = assetFund as CompositeAssetFund;
					if (comp == null)
					{
						throw new ArgumentException("Asset fund not of a valid asset fund type");
					}
					else
					{
						retType = AssetFundType.Composite;
					}
				}
				else
				{
					retType = AssetFundType.Linked;
				}
			}
			else
			{
				retType = AssetFundType.Oeic;
			}

			return retType;
		}

		/*
        /// <summary>
        /// Creates an empty asset fund
        /// </summary>
        /// <returns>An empty asset fund</returns>
        public static AssetFund CreateAssetFund()
        {
            return new AssetFund();
        }
*/

		/// <summary>
		/// Creates an asset fund.
		/// </summary>
		/// <param name="type">THe type of AssetFund to be created.</param>
		/// <param name="assetFundCode">Asset fund code.</param>
		/// <param name="shortName">Name of the short.</param>
		/// <param name="fullName">Full name.</param>
		/// <param name="companyCode">Company code.</param>
		/// <param name="unitPrice">Unit price.</param>
		/// <param name="unitPriceMovement">Unit price movement.</param>
		/// <param name="previousUnitPrice">Previous unit price.</param>
		/// <param name="previousUnitPriceSet">Previous unit price set.</param>
		/// <param name="authorisedUnitPrice">Authorised unit price.</param>
		/// <param name="authorisedUnitPriceSet">Authorised unit price set.</param>
		/// <param name="AMTolerance">AM tolerance.</param>
		/// <param name="valuationPoint">Valuation point.</param>
		/// <param name="unitPriceSet">Unit price set.</param>
		/// <param name="unitPriceMovementSet">Unit price movement set.</param>
		/// <param name="AMToleranceSet">AM tolerance set.</param>
		/// <param name="valuationPointSet">Valuation point set.</param>
		/// <param name="lockedForCalculation">Locked for calculation.</param>
		/// <param name="currencyRatesSnapshotID">Currency rates import ID.</param>
		/// <param name="currencyRatesSnapshotIDSet">Currency rates import ID set.</param>
		/// <param name="indexValuesSnapshotID">Index values import ID.</param>
		/// <param name="indexValuesSnapshotIDSet">Index values import ID set.</param>
		/// <param name="fundWeightingsSnapshotID">Fund weightings import ID.</param>
		/// <param name="fundWeightingsSnapshotIDSet">Fund weightings import ID set.</param>
		/// <param name="compositeFundSplitSnapshotID"></param>
		/// <param name="compositeFundSplitSnapshotIDSet"></param>
		/// <param name="timeStamp">Time stamp.</param>
		/// <param name="assetFundStatus">Asset fund status.</param>
		/// <param name="assetMovementConstituentCollection">The weighted benchmarks.</param>
		/// <param name="assetFundCurrency"></param>
		/// <returns>A re-hydrated asset fund</returns>
		public static AssetFund CreateAssetFund(AssetFundType type, string assetFundCode, string shortName, string fullName
		                                        , string companyCode, decimal unitPrice, decimal unitPriceMovement, decimal previousUnitPrice
		                                        , bool previousUnitPriceSet, decimal authorisedUnitPrice, bool authorisedUnitPriceSet
		                                        , decimal AMTolerance, DateTime valuationPoint
		                                        , AssetMovementConstituentCollection assetMovementConstituentCollection
		                                        , bool unitPriceSet
		                                        , bool unitPriceMovementSet, bool AMToleranceSet, bool valuationPointSet
		                                        , bool lockedForCalculation, long currencyRatesSnapshotID, bool currencyRatesSnapshotIDSet
		                                        , long indexValuesSnapshotID, bool indexValuesSnapshotIDSet, long fundWeightingsSnapshotID
		                                        , bool fundWeightingsSnapshotIDSet, long compositeFundSplitSnapshotID, bool compositeFundSplitSnapshotIDSet
		                                        , byte[] timeStamp, int assetFundStatus,Currency.Currency assetFundCurrency)
		{
			switch (type)
			{
				case AssetFundType.Oeic:
					return new OEICAssetFund(assetFundCode, shortName, fullName, companyCode, unitPrice, valuationPoint
					                         , unitPriceMovement, previousUnitPrice, previousUnitPriceSet, authorisedUnitPrice, authorisedUnitPriceSet
					                         , lockedForCalculation
					                         , AMTolerance,  unitPriceSet, unitPriceMovementSet
					                         , AMToleranceSet, valuationPointSet, currencyRatesSnapshotID, currencyRatesSnapshotIDSet
					                         , indexValuesSnapshotID, indexValuesSnapshotIDSet, fundWeightingsSnapshotID
					                         , fundWeightingsSnapshotIDSet, timeStamp, assetFundStatus, assetMovementConstituentCollection,assetFundCurrency);

				case AssetFundType.Linked:
					return new LinkedAssetFund(assetFundCode, shortName, fullName, companyCode, unitPrice, valuationPoint
					                           , unitPriceMovement, previousUnitPrice, previousUnitPriceSet, authorisedUnitPrice, authorisedUnitPriceSet
					                           , lockedForCalculation
					                           , AMTolerance,  unitPriceSet, unitPriceMovementSet
					                           , AMToleranceSet, valuationPointSet, currencyRatesSnapshotID, currencyRatesSnapshotIDSet
					                           , indexValuesSnapshotID, indexValuesSnapshotIDSet, fundWeightingsSnapshotID
					                           , fundWeightingsSnapshotIDSet, timeStamp, assetFundStatus, assetMovementConstituentCollection, assetFundCurrency);

				case AssetFundType.Composite:
					return new CompositeAssetFund(assetFundCode, shortName, fullName, companyCode, unitPrice, valuationPoint
					                              , unitPriceMovement, previousUnitPrice, previousUnitPriceSet, authorisedUnitPrice, authorisedUnitPriceSet
					                              , lockedForCalculation
					                              , AMTolerance,  unitPriceSet, unitPriceMovementSet
					                              , AMToleranceSet, valuationPointSet
					                              , compositeFundSplitSnapshotID, compositeFundSplitSnapshotIDSet
					                              , timeStamp, assetFundStatus, assetMovementConstituentCollection, assetFundCurrency);

				default:
					throw new ArgumentException("Invalid AssetFundType");
			}
		}

		/// <summary>
		/// Initialises the asset fund with data for static export use only
		/// </summary>
		/// <param name="afType">The type of asset fund to create.</param>
		/// <param name="assetFundID">The HiPortfolio fund code.</param>
		/// <param name="shortName">The short name of the asset fund</param>
		/// <param name="fullName">The full name of the asset fund</param>
		/// <param name="companyCode">The company code for this asset fund</param>
		/// <param name="assetMovementTolerance">am tolerance derived from child funds</param>
		/// <param name="fundGroups">The collection of fund groups associated with this asset fund</param>
		/// <param name="assetMovementConstituentCollection">The collection of weighted movements associated with this asset fund</param>
		public static AssetFund CreateAssetFund(AssetFundType afType, string assetFundID, string shortName, string fullName, string companyCode, decimal assetMovementTolerance, FundGroupCollection fundGroups, AssetMovementConstituentCollection assetMovementConstituentCollection)
		{
			switch (afType)
			{
				case AssetFundType.Composite:
					return new CompositeAssetFund(assetFundID, shortName, fullName, companyCode, assetMovementTolerance, fundGroups, assetMovementConstituentCollection);
				case AssetFundType.Linked:
					return new LinkedAssetFund(assetFundID, shortName, fullName, companyCode, assetMovementTolerance, fundGroups, assetMovementConstituentCollection);
				case AssetFundType.Oeic:
					return new OEICAssetFund(assetFundID, shortName, fullName, companyCode, assetMovementTolerance, fundGroups, assetMovementConstituentCollection);
				default:
					throw new ArgumentException("Invalid asset fund type");

			}
		}

		/// <summary>
		/// Create an asset fund from a row of data
		/// </summary>
		/// <param name="afType"></param>
		/// <param name="assetFundID"></param>
		/// <param name="shortName"></param>
		/// <param name="fullName"></param>
		/// <param name="companyCode"></param>
		/// <param name="lastUpdatedTimestamp"></param>
		/// <param name="fundGroups">The collection of fund groups associated with the asset fund</param>
		/// <param name="assetMovementConstituentCollection">The collection of weighted benchmarks associated with this asset fund</param>
		/// <returns>A populated asset fund of the correct type</returns>
		/// <exception cref="ArgumentException">Invalid asset fund type</exception>
		public static AssetFund CreateAssetFund(AssetFundType afType, string assetFundID, string shortName, string fullName, string companyCode, byte[] lastUpdatedTimestamp, FundGroupCollection fundGroups, AssetMovementConstituentCollection assetMovementConstituentCollection)
		{
			switch (afType)
			{
				case AssetFundType.Composite:
					return new CompositeAssetFund(assetFundID, shortName, fullName, companyCode, lastUpdatedTimestamp, fundGroups, assetMovementConstituentCollection);
				case AssetFundType.Linked:
					return new LinkedAssetFund(assetFundID, shortName, fullName, companyCode, lastUpdatedTimestamp, fundGroups, assetMovementConstituentCollection);
				case AssetFundType.Oeic:
					return new OEICAssetFund(assetFundID, shortName, fullName, companyCode, lastUpdatedTimestamp, fundGroups, assetMovementConstituentCollection);
				default:
					throw new ArgumentException("Invalid asset fund type");

			}
		}

		/// <summary>
		/// Create a currency
		/// </summary>
		/// <param name="currencyCode"></param>
		/// <returns>New Currency object</returns>
		public static Currency.Currency CreateCurrency(string currencyCode)
		{
			return new Currency.Currency(currencyCode);
		}

		/// <summary>
		/// Create a new Market Index
		/// </summary>
		/// <param name="countryCode"></param>
		/// <param name="indexName"></param>
		/// <param name="currencyCode"></param>
		/// <param name="marketIndexID"></param>
		/// <returns></returns>
		public static StockMarket CreateMarketIndex(string countryCode, string indexName, string currencyCode, int marketIndexID)
		{
			return new StockMarket(countryCode, indexName, currencyCode, marketIndexID);
		}
	}
}