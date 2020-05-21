using System;
using HBOS.FS.AMP.UPD.Types.FundGroups;
using HBOS.FS.AMP.UPD.Types.Funds;

namespace HBOS.FS.AMP.UPD.Types.AssetFunds
{
	/// <summary>
	/// Summary description for CompositeAssetFund.
	/// </summary>
	public class CompositeAssetFund : AssetFund
	{
        #region Member variables

        private long m_compositeFundSplitSnapshotID;
        private bool m_compositeFundSplitSnapshotIDSet = false;

        #endregion
        
        #region Constructors

		/// <summary>
		/// Default constructor (which just calls base)
		/// </summary>
		public CompositeAssetFund() : base()
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
		/// <param name="assetMovementConstituentCollection">The collection of weighted benchmark associated with this asset fund</param>
		public CompositeAssetFund(string assetFundCode, string shortName, string fullName, string companyCode, decimal assetMovementTolerance, FundGroupCollection fundGroups, AssetMovementConstituentCollection assetMovementConstituentCollection) 
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
		public CompositeAssetFund(string assetFundCode, string shortName, string fullName, string companyCode, byte[] lastUpdatedTimeStamp, FundGroupCollection fundGroups, AssetMovementConstituentCollection assetMovementConstituentCollection): 
            base(assetFundCode, shortName, fullName, companyCode, lastUpdatedTimeStamp, fundGroups, assetMovementConstituentCollection)
		{
		}

		
		/// <summary>
		/// Creates a new <see cref="CompositeAssetFund"/> instance. Constructor with all params. Just passes params onto base
		/// </summary>
		/// <param name="assetFundCode">Asset fund code.</param>
		/// <param name="shortName">Name of the short.</param>
		/// <param name="fullName">Full name.</param>
		/// <param name="companyCode">Company code.</param>
		/// <param name="unitPrice">Unit price.</param>
		/// <param name="valuationPoint">Valuation point.</param>
		/// <param name="unitPriceMovement">Unit price movement.</param>
		/// <param name="previousUnitPrice">Previous unit price.</param>
		/// <param name="previousUnitPriceSet">Previous unit price set.</param>
		/// <param name="authorisedUnitPrice">Authorised unit price.</param>
		/// <param name="authorisedUnitPriceSet">Authorised unit price set.</param>
		/// <param name="lockedForCalculation">Locked for calculation.</param>
		/// <param name="AMTolerance">AM tolerance.</param>
		/// <param name="unitPriceSet">Unit price set.</param>
		/// <param name="unitPriceMovementSet">Unit price movement set.</param>
		/// <param name="AMToleranceSet">AM tolerance set.</param>
		/// <param name="valuationPointSet">Valuation point set.</param>
		/// <param name="fundSplitSnapshotID">Fund split import ID.</param>
		/// <param name="fundSplitSnapshotIDSet">Fund split import ID set.</param>
		/// <param name="timeStamp">Time stamp.</param>
		/// <param name="assetFundStatus">Asset fund status.</param>
		/// <param name="assetMovementConstituentCollection">Asset movement consitituent collection.</param>
		/// <param name="assetFundCurrency"></param>
        public CompositeAssetFund(string assetFundCode, string shortName, string fullName, string companyCode
            , decimal unitPrice, DateTime valuationPoint, decimal unitPriceMovement
            , decimal previousUnitPrice, bool previousUnitPriceSet, decimal authorisedUnitPrice, bool authorisedUnitPriceSet
            , bool lockedForCalculation
            , decimal AMTolerance
			, bool unitPriceSet, bool unitPriceMovementSet, bool AMToleranceSet, bool valuationPointSet
            , long fundSplitSnapshotID, bool fundSplitSnapshotIDSet
            , byte[] timeStamp, int assetFundStatus, AssetMovementConstituentCollection assetMovementConstituentCollection,Currency.Currency assetFundCurrency) 			

            : base (assetFundCode, shortName, fullName, companyCode, unitPrice, valuationPoint, unitPriceMovement
            , previousUnitPrice, previousUnitPriceSet,
			authorisedUnitPrice, authorisedUnitPriceSet, 
			lockedForCalculation,AMTolerance,
             unitPriceSet, unitPriceMovementSet, AMToleranceSet, valuationPointSet
            , timeStamp, assetFundStatus ,assetMovementConstituentCollection, assetFundCurrency)
        {
            
            // Assign composite fund split import values...
            this.m_compositeFundSplitSnapshotID = fundSplitSnapshotID;
            this.m_compositeFundSplitSnapshotIDSet = fundSplitSnapshotIDSet;
        }

		#endregion

        #region Methods

		/// <summary>
		/// The string representation of the type name used for display and reporting purposes
		/// </summary>
		public override string AssetFundType
		{
			get
			{
				return "Composite";
			}
			//todo - do we need a set for the grid?
		}

        /// <summary>
        /// Indicates if all the linked funds making up this composition are authorised.
        /// </summary>
        /// <returns></returns>
        public bool AreAllLinkedFundsAuthorised()
        {
            bool result = true;
            foreach(HBOS.FS.AMP.UPD.Types.AssetFunds.AssetMovementConstituent c in this.AssetMovementConstitute)
            {
				if (c.BenchMark is Fund )
				{
					if(! ((Fund)c.BenchMark).IsAuthorised)
					{
						result=false;
						break;
					}
				}
            }

            return result;
        }

        /// <summary>
        /// Read only property that returns the Predicted Asset Movement Display string
        /// </summary>
        public override string  PredictedAssetMovementDisplay
        {
            get
            {
                return DisplayFormat.Percent(PredictedAssetMovement, AreAllLinkedFundsAuthorised() && PredictedAssetMovementSet);
            }
        }	

        #endregion

        #region Properties

        /// <summary>
        /// The composite fund split import id.
        /// </summary>
        public long FundSplitSnapshotID
        {
            get
            {
                return this.m_compositeFundSplitSnapshotID;
            }
            
            set
            {
                this.m_compositeFundSplitSnapshotID = value;
                this.m_compositeFundSplitSnapshotIDSet = true;
            }
        }

        /// <summary>
        /// Flag indicating that the composite fund split import id has been set.
        /// </summary>
        public bool FundSplitSnapshotIDSet
        {
            get
            {
                return this.m_compositeFundSplitSnapshotIDSet;
            }
            
            set
            {
                this.m_compositeFundSplitSnapshotIDSet = value;
            }
        }

        #endregion

	}
}