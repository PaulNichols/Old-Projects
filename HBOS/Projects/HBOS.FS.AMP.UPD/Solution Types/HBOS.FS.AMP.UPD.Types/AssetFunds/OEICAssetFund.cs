using System;
using HBOS.FS.AMP.UPD.Types.FundGroups;
using HBOS.FS.AMP.UPD.Types.AssetFunds;

namespace HBOS.FS.AMP.UPD.Types.AssetFunds
{
	/// <summary>
	/// Summary description for OEICAssetFund.
	/// </summary>
	public class OEICAssetFund : AssetFund
	{
        #region Member variables

        private long m_currencyRateSnapshotID = 0;        // (from db)
        private long m_indexValueSnapshotID = 0;          // (from db)
        private long m_indexWeightingSnapshotID = 0;      // (from db)

        private bool m_currencyRateSnapshotIDSet = false;
        private bool m_indexValueSnapshotIDSet = false;
        private bool m_indexWeightingSnapshotIDSet = false;

        #endregion
        
        #region Constructors

		/// <summary>
		/// Default constructor (which just calls base)
		/// </summary>
		public OEICAssetFund() : base()
		{
		}

		/// <summary>
		/// Initialises the asset fund with data for static export use only
		/// </summary>
		/// <param name="assetFundCode">The HiPortfolio fund code.</param>
		/// <param name="shortName">The short name of the asset fund</param>
		/// <param name="fullName">The full name of the asset fund</param>
		/// <param name="companyCode">The company code for this asset fund</param>
		/// <param name="assetMovementTolerance">am tolerance derived from child funds</param>
		/// <param name="fundGroups">The collection of fund groups associated with this asset fund</param>
		/// <param name="assetMovementConstituentCollection">The collection of weighted benchmarks associated with this asset fund</param>
		public OEICAssetFund(string assetFundCode, string shortName, string fullName, string companyCode, decimal assetMovementTolerance, FundGroupCollection fundGroups, AssetMovementConstituentCollection assetMovementConstituentCollection) 
			:base (assetFundCode, shortName, fullName, companyCode, assetMovementTolerance, fundGroups, assetMovementConstituentCollection)
	{
	}

		/// <summary>
		/// Initialises the asset fund with data for light use only (eg static data)
		/// </summary>
		/// <param name="assetFundCode">The HiPortfolio fund code.</param>
		/// <param name="shortName">The short name of the asset fund</param>
		/// <param name="fullName">The full name of the asset fund</param>
		/// <param name="companyCode"></param>
		/// <param name="lastUpdatedTimeStamp">The database timestamp used for concurrency checking</param>
		/// <param name="fundGroups">The collection of fund groups associated with this asset fund</param>
		/// <param name="assetMovementConstituentCollection">The collection of weighted benchmarks associated with this asset fund</param>
		public OEICAssetFund(string assetFundCode, string shortName, string fullName, string companyCode, byte[] lastUpdatedTimeStamp, FundGroupCollection fundGroups, AssetMovementConstituentCollection assetMovementConstituentCollection): 
			base(assetFundCode, shortName, fullName, companyCode, lastUpdatedTimeStamp, fundGroups, assetMovementConstituentCollection)
		{
		}

        /// <summary>
        /// Constructor with all params.
        /// </summary>
        /// <param name="assetFundCode"></param>
        /// <param name="shortName"></param>
        /// <param name="fullName"></param>
        /// <param name="companyCode"></param>
        /// <param name="unitPrice"></param>
        /// <param name="valuationPoint"></param>
        /// <param name="unitPriceMovement"></param>
        /// <param name="previousUnitPrice"></param>
        /// <param name="previousUnitPriceSet"></param>
        /// <param name="authorisedUnitPrice"></param>
        /// <param name="authorisedUnitPriceSet"></param>
        /// <param name="lockedForCalculation"></param>
        /// <param name="AMTolerance"></param>
        /// <param name="unitPriceSet"></param>
        /// <param name="unitPriceMovementSet"></param>
        /// <param name="AMToleranceSet"></param>
        /// <param name="valuationPointSet"></param>
        /// <param name="currencyRateSnapshotID"></param>
        /// <param name="currencyRateSnapshotIDSet"></param>
        /// <param name="indexValueSnapshotID"></param>
        /// <param name="indexValueSnapshotIDSet"></param>
        /// <param name="indexWeightingSnapshotID"></param>
        /// <param name="indexWeightingSnapshotIDSet"></param>
        /// <param name="timeStamp"></param>
        /// <param name="assetFundStatus"></param>
        /// <param name="assetMovementConstituentCollection"></param>
        /// <param name="assetFundCurrency"></param>
        public OEICAssetFund(string assetFundCode, string shortName, string fullName, string companyCode
			, decimal unitPrice, DateTime valuationPoint, decimal unitPriceMovement
			, decimal previousUnitPrice, bool previousUnitPriceSet, decimal authorisedUnitPrice, bool authorisedUnitPriceSet
			, bool lockedForCalculation
			, decimal AMTolerance
			, bool unitPriceSet, bool unitPriceMovementSet, bool AMToleranceSet, bool valuationPointSet
            , long currencyRateSnapshotID, bool currencyRateSnapshotIDSet, long indexValueSnapshotID
            , bool indexValueSnapshotIDSet, long indexWeightingSnapshotID, bool indexWeightingSnapshotIDSet
            , byte[] timeStamp, int assetFundStatus, AssetMovementConstituentCollection assetMovementConstituentCollection,Currency.Currency assetFundCurrency) 			
            
            : base (assetFundCode, shortName, fullName, companyCode, unitPrice, valuationPoint, unitPriceMovement
			, previousUnitPrice, previousUnitPriceSet, authorisedUnitPrice, authorisedUnitPriceSet
			, lockedForCalculation, AMTolerance
			, unitPriceSet, unitPriceMovementSet, AMToleranceSet, valuationPointSet
			, timeStamp, assetFundStatus, assetMovementConstituentCollection, assetFundCurrency)
		{

            // Assign import id's
            this.m_currencyRateSnapshotID = currencyRateSnapshotID;
            this.m_currencyRateSnapshotIDSet = currencyRateSnapshotIDSet;
            this.m_indexValueSnapshotID = indexValueSnapshotID;
            this.m_indexValueSnapshotIDSet = indexValueSnapshotIDSet;
            this.m_indexWeightingSnapshotID = indexWeightingSnapshotID;
            this.m_indexWeightingSnapshotIDSet = indexWeightingSnapshotIDSet;
		}
		#endregion

        #region Properties

        /// <summary>
        /// The import ID of the currency import file used for calculations.
        /// </summary>
        public long CurrencyRateSnapshotID
        {
            get
            {
                return this.m_currencyRateSnapshotID;
            }

            
            set
            {
                this.m_currencyRateSnapshotID = value;
                this.m_currencyRateSnapshotIDSet = true;
            }
        }

        /// <summary>
        /// Flag indicating whether the currency rate import ID has been populated from the database or not.
        /// </summary>
        public bool CurrencyRateSnapshotIDSet
        {
            get
            {
                return this.m_currencyRateSnapshotIDSet;
            }

            
            set
            {
                this.m_currencyRateSnapshotIDSet = value;
            }
        }

        /// <summary>
        /// The import ID of the index values import file used for calculations.
        /// </summary>
        public long IndexValueSnapshotID
        {
            get
            {
                return this.m_indexValueSnapshotID;
            }

            
            set
            {
                this.m_indexValueSnapshotID = value;
                this.m_indexValueSnapshotIDSet = true;
            }
        }

        /// <summary>
        /// Flag indicating whether the index value import ID has been populated from the database or not.
        /// </summary>
        public bool IndexValueSnapshotIDSet
        {
            get
            {
                return this.m_indexValueSnapshotIDSet;
            }

            
            set
            {
                this.m_indexValueSnapshotIDSet = value;
            }
        }

        /// <summary>
        /// The import ID of the fund weightings import file used for calculations.
        /// </summary>
        public long IndexWeightingSnapshotID
        {
            get
            {
                return this.m_indexWeightingSnapshotID;
            }

            
            set
            {
                this.m_indexWeightingSnapshotID = value;
                this.m_indexWeightingSnapshotIDSet = true;
            }
        }

        /// <summary>
        /// Flag indicating whether the index weighting import ID has been populated from the database or
        /// not.
        /// </summary>
        public bool IndexWeightingSnapshotIDSet
        {
            get
            {
                return this.m_indexWeightingSnapshotIDSet;
            }

            
            set
            {
                this.m_indexWeightingSnapshotIDSet = value;
            }
        }

        #endregion

        #region methods

		/// <summary>
		/// The string representation of the type name used for display and reporting purposes
		/// </summary>
		public override string AssetFundType
		{
			get
			{
				return "OEIC";
			}
			//todo - do we need a set for the grid?
		}

        #endregion
	}
}
