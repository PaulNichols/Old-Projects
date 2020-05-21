using System;
using HBOS.FS.AMP.UPD.Types.Currency;
using HBOS.FS.AMP.UPD.Types.FundGroups;
using HBOS.FS.AMP.UPD.Types.Funds;

namespace HBOS.FS.AMP.UPD.Types.AssetFunds
{
	/// <summary>
	/// The asset fund holding a collection of funds.
	/// </summary>
	public class AssetFund : EntityBase
	{
		/// <summary>
		/// 
		/// </summary>
		public enum AssetFundTypeEnum : int
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

		#region Member variables

		//
		// Note. As we work through the drop II use cases move the member
		//       variables that we still need in this class into this 
		//       region so what is left we know we can remove from the class.
		//

		private long m_benchmarkSplitSnapshotID=0;
		private bool m_benchmarkSplitSnapshotIDSet = false;

		private long m_currencyRateSnapshotID=0; 
		private long m_indexValueSnapshotID=0;
		private readonly bool m_IndexValueSnapshotIdSet;


		private bool m_currencyRateSnapshotIDSet=false;

		#region Drop II 

		private string m_assetFundCode; //Readonly (from db)
		private string m_shortName; //(from db)
		private string m_fullName; //(from db)

		private decimal m_AMTolerance; //(from db)
		private decimal m_authorisedUnitPrice;
		private bool m_authorisedUnitPriceSet;

		//Allow a null collection - as fund group persister returns a collection, calling client will
		//mostly just set the fund collection on the fund.
		private FundGroupCollection m_fundGroups = null;

		#region Objects

/*
        private FundCollection m_funds = new FundCollection();
*/
		//   private WeightedMovementCollection m_weightedMovements = null; //create on a get only so we don't create then throw away 

		#endregion

		#endregion

		#region Drop I

		private string m_companyCode; //Readonly (from db)
		private decimal m_unitPrice; //Readonly (from db)
		private decimal m_unitPriceMovement; //Readonly (from db)
		private decimal m_predictedAssetMovement; //Readonly
		private decimal m_previousUnitPrice; //Readonly (from db)
		private decimal m_AMVariance; //Readonly

		//m_withinAMTolerance is set by checkTolerance which is called by the constructor
		//so does not need to be passed in as a param to the constructor.
		//Note: as long as AMTolerance column exists in db table then AMTolerance set
		//will be true, and checkTolereance will set m_withinAMTolerance
		private bool m_withinAMTolerance; //Readonly 

		private DateTime m_valuationPoint; //Readonly
		private bool m_lockedForCalculation; // (from db)
		private decimal m_predictedUnitPrice;

		//Indicators
		private bool m_unitPriceSet; //Readonly
		private bool m_previousUnitPriceSet; //Readonly
		private bool m_unitPriceMovementSet; //Readonly
		private bool m_predictedAMSet;
		private bool m_AMVarianceSet;
		private bool m_predictedUnitPriceSet;
		private bool m_AMToleranceSet; //Readonly
		private bool m_valuationPointSet; //Readonly

		#region objects

		private Currency.Currency currency;
		private AssetMovementConstituentCollection assetMovementConstitution=new AssetMovementConstituentCollection();
		private AssetFundTypeEnum type;
		private int m_PriceFileId;

		#endregion

		#endregion

		#endregion

		#region New Constructors

		/// <summary>
		/// Constructor to be used when you create a new Asset Fund
		/// </summary>
		public AssetFund()
		{
			SetDefaultValues();
		}

		/// <summary>
		/// Constructor to be used when you create a new Asset Fund
		/// </summary>
		public AssetFund(AssetFundTypeEnum type)
		{
			SetDefaultValues();
			this.type = type;
		}

		private void SetDefaultValues()
		{
			this.m_assetFundCode = "";
			this.m_shortName = "";
			this.m_fullName = "";
			this.m_companyCode = "";
			this.m_unitPrice = 0;
			this.m_unitPriceMovement = 0;
			this.m_previousUnitPrice = 0;
			this.m_previousUnitPriceSet = false;
			this.m_predictedAssetMovement = 0;
			this.m_AMVariance = 0;
			this.m_AMTolerance = 0;
			this.m_withinAMTolerance = false;
			this.m_valuationPoint = DateTime.Now;
			this.m_unitPriceSet = false;
			this.m_unitPriceMovementSet = false;
			this.m_AMToleranceSet = false;
			this.m_valuationPointSet = false;
			this.m_lockedForCalculation = false;
			this.m_predictedUnitPrice = 0;
			this.m_predictedUnitPriceSet = false;
			m_PriceFileId=0;
			this.m_authorisedUnitPrice = 0M;
			this.m_authorisedUnitPriceSet = false;

			m_fundGroups = new FundGroupCollection(); //KAJ 13/05/05

			//Set up IEntityBase members
			m_isNew = true;
			m_isDeleted = false;
			m_timestamp = new byte[1];
			m_isDirty = true;
		}


		/// <summary>
		/// Constructor to be used when you create an existing <see cref="AssetFund"/> instance.
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
		/// <param name="timeStamp">Time stamp.</param>
		/// <param name="assetMovementConstituentCollection">Asset movement consitituent collection.</param>
		/// <param name="assetFundCurrency"></param>
		/// <param name="type">O=OEIC,L=Linked or C=Composite </param>
		/// <param name="benchmarkSplitSnapshotID"></param>
		/// <param name="benchmarkSplitSnapshotIDSet"></param>
		/// <param name="currencyRateSnapshotID"></param>
		/// <param name="currencyRateSnapshotIDSet"></param>
		/// <param name="indexValueSnapshotID"></param>
		/// <param name="indexValueSnapshotIDSet"></param>
		public AssetFund(AssetFundTypeEnum type, string assetFundCode, string shortName, 
			string fullName, string companyCode, decimal unitPrice, 
			DateTime valuationPoint, decimal unitPriceMovement, decimal previousUnitPrice, 
			bool previousUnitPriceSet, decimal authorisedUnitPrice, bool authorisedUnitPriceSet, 
			bool lockedForCalculation, decimal AMTolerance, bool unitPriceSet, bool unitPriceMovementSet, 
			bool AMToleranceSet, bool valuationPointSet, byte[] timeStamp, 
			AssetMovementConstituentCollection assetMovementConstituentCollection, 
			Currency.Currency assetFundCurrency,long benchmarkSplitSnapshotID,bool benchmarkSplitSnapshotIDSet, 
			long currencyRateSnapshotID,bool currencyRateSnapshotIDSet,
			long indexValueSnapshotID,bool indexValueSnapshotIDSet)
		{
			//TODO - assetFundStatus not being used!

			
			m_benchmarkSplitSnapshotID=benchmarkSplitSnapshotID;
			m_benchmarkSplitSnapshotIDSet = benchmarkSplitSnapshotIDSet;
			m_currencyRateSnapshotID=currencyRateSnapshotID;
			m_indexValueSnapshotID=indexValueSnapshotID;
			m_IndexValueSnapshotIdSet = indexValueSnapshotIDSet;
			m_currencyRateSnapshotIDSet=currencyRateSnapshotIDSet;
			this.type = type;
			this.currency = assetFundCurrency;
			this.m_assetFundCode = assetFundCode;
			this.m_shortName = shortName;
			this.m_fullName = fullName;
			this.m_companyCode = companyCode;
			this.m_unitPrice = unitPrice;
			this.m_unitPriceMovement = unitPriceMovement;
			this.m_previousUnitPrice = previousUnitPrice;
			this.m_previousUnitPriceSet = previousUnitPriceSet;
			this.m_AMTolerance = AMTolerance;
			this.m_valuationPoint = valuationPoint;
			this.m_unitPriceSet = unitPriceSet;
			this.m_unitPriceMovementSet = unitPriceMovementSet;
			this.m_AMToleranceSet = AMToleranceSet;
			this.m_valuationPointSet = valuationPointSet;
			this.m_lockedForCalculation = lockedForCalculation;

			this.m_predictedUnitPrice = 0;
			this.m_predictedUnitPriceSet = false;
			this.m_predictedAssetMovement = 0;
			this.m_predictedAMSet = false;

			//allow fundgroups to be null so that user can set them without creating one unnecessarily
			//(persisters return a FundGroupCollection which can be set directly as a property here)
			//m_fundGroups = new FundGroupCollection();		//KAJ 13/05/05

			//Set up IEntityBase members
			m_isNew = false;
			m_isDeleted = false;
			m_timestamp = timeStamp;
			m_isDirty = false;

			//Do the rest of the calculations

			if (assetMovementConstituentCollection!=null) this.AssetMovementConstituents = assetMovementConstituentCollection;

			checkTolerance();

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
		/// <param name="priceFileId"></param>
		/// <param name="assetMovementConstituentCollection">The collection of benchmarks with weightings associated with this asset fund</param>
		/// <param name="type"></param>
		/// <param name="currency">The Asset Funds Currency</param>
		public AssetFund(AssetFundTypeEnum type, string assetFundCode, string shortName, string fullName, string companyCode, byte[] lastUpdatedTimeStamp, FundGroupCollection fundGroups, AssetMovementConstituentCollection assetMovementConstituentCollection,Currency.Currency currency,int priceFileId)
		{
			this.currency=currency;
			m_PriceFileId=priceFileId;
			this.m_assetFundCode = assetFundCode;
			this.m_shortName = shortName;
			this.m_fullName = fullName;
			this.m_companyCode = companyCode;
			this.m_timestamp = lastUpdatedTimeStamp;
			this.m_fundGroups = fundGroups;
			if (assetMovementConstituentCollection!=null) AssetMovementConstituents = assetMovementConstituentCollection;

			//Set up IEntityBase members
			m_isNew = false;
			m_isDeleted = false;
			m_isDirty = false;
			this.type = type;
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
		/// <param name="type">Linked,Composte or OEIC</param>
		public AssetFund(AssetFundTypeEnum type, string assetFundCode, string shortName, string fullName, string companyCode, decimal assetMovementTolerance, FundGroupCollection fundGroups, AssetMovementConstituentCollection assetMovementConstituentCollection)
		{
			this.m_assetFundCode = assetFundCode;
			this.m_shortName = shortName;
			this.m_fullName = fullName;
			this.m_companyCode = companyCode;
			this.m_AMTolerance = assetMovementTolerance;
			this.m_AMToleranceSet = true; //is always true - even if coalesce returns a 0 - see Kevin.todo - lose this variable!
			this.m_fundGroups = fundGroups;
			if (assetMovementConstituentCollection!=null) this.AssetMovementConstituents = assetMovementConstituentCollection;

			//Set up IEntityBase members
			m_isNew = false;
			m_isDeleted = false;
			m_isDirty = false;
			this.type = type;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The collection of AssetFundGroup that detail the fund groups that this asset fund is linked to.
		/// This collection shall always exist and shall return an empty collection if the fund is not associated
		/// with any fund groups.
		/// </summary>
		public FundGroupCollection FundGroups
		{
			get
			{
				if (m_fundGroups == null)
				{
					m_fundGroups = new FundGroupCollection();
				}
				return m_fundGroups;
			}
			set { m_fundGroups = value; }
		}

		/// <summary>
		/// A string representation of the type used for display and reporting purposes.
		/// This is just to make life easier for xslt etc
		/// todo - can we remove display properties from the entity?
		/// </summary>
		public virtual string AssetFundTypeString
		{
			get
			{
				String returnType = "";
				switch (this.type)
				{
					case AssetFundTypeEnum.Oeic:
						returnType = "OEIC";
						break;
					case AssetFundTypeEnum.Linked:
						returnType = "Linked";
						break;
					case AssetFundTypeEnum.Composite:
						returnType = "Composite";
						break;
				}
				return returnType;
			}
			//todo - do we need a set for the grid?
		}

		/// <summary>
		/// The unique identifier for the asset fund.
		/// </summary>
		public string AssetFundCode
		{
			get { return this.m_assetFundCode; }


			set { this.m_assetFundCode = value; }
		}

		/// <summary>
		/// The short asset fund name.
		/// </summary>
		public string ShortName
		{
			get { return this.m_shortName; }

			set
			{
				this.m_shortName = value;
				SetDirtyFlag();
			}
		}

		/// <summary>
		/// The full asset fund name.
		/// </summary>
		public string FullName
		{
			get { return this.m_fullName; }

			set
			{
				this.m_fullName = value;
				SetDirtyFlag();
			}
		}

		/// <summary>
		/// A code that represents the Company that this Asset Fund belongs to
		/// </summary>
		public string CompanyCode
		{
			get { return this.m_companyCode; }


			set { this.m_companyCode = value; }
		}

		/// <summary>
		/// Today's price of the Asset Fund
		/// </summary>
		public decimal UnitPrice
		{
			get { return this.m_unitPrice; }


			set { this.m_unitPrice = value; }
		}

		/// <summary>
		/// Whether the Current price has been set for this asset fund
		/// </summary>
		public bool UnitPriceSet
		{
			get { return this.m_unitPriceSet; }


			set { this.m_unitPriceSet = value; }
		}

		/// <summary>
		/// Today's authorised price of the Asset Fund
		/// </summary>
		public decimal AuthorisedUnitPrice
		{
			get { return this.m_authorisedUnitPrice; }


			set { this.m_authorisedUnitPrice = value; }
		}

		/// <summary>
		/// Whether the Current authorised price has been set for this asset fund
		/// </summary>
		public bool AuthorisedUnitPriceSet
		{
			get { return this.m_authorisedUnitPriceSet; }


			set { this.m_authorisedUnitPriceSet = value; }
		}

		/// <summary>
		/// The proportional movement in the price from previous to current
		/// </summary>
		public decimal UnitPriceMovement
		{
			get { return this.m_unitPriceMovement; }


			set { this.m_unitPriceMovement = value; }
		}

		/// <summary>
		/// Whether the AM has been set for this asset fund
		/// </summary>
		public bool UnitPriceMovementSet
		{
			get { return this.m_unitPriceMovementSet; }


			set { this.m_unitPriceMovementSet = value; }
		}


		/// <summary>
		/// A calculated predicted unit price to use when the actual unit price is not available.
		/// </summary>
		public decimal PredictedUnitPrice
		{
			get { return this.m_predictedUnitPrice; }


			set { this.m_predictedUnitPrice = value; }
		}

		/// <summary>
		/// Flag indicating that the PredictedUnitPrice is a valid value.
		/// </summary>
		public bool PredictedUnitPriceSet
		{
			get { return this.m_predictedUnitPriceSet; }

		}

		/// <summary>
		/// The predicted Asset Movement for this Asset Fund (derived from Market Value splits)
		/// </summary>
		public decimal PredictedAssetMovement
		{
			get { return this.m_predictedAssetMovement; }


			set { this.m_predictedAssetMovement = value; }
		}

		/// <summary>
		/// Shows whether the Predicted Asset Movement has been set for this Asset Fund
		/// </summary>
		public bool PredictedAssetMovementSet
		{
			get { return m_predictedAMSet; }
		}


		/// <summary>
		/// The previous authorised asset unit price.
		/// </summary>
		public decimal PreviousUnitPrice
		{
			get { return this.m_previousUnitPrice; }


			set { this.m_previousUnitPrice = value; }
		}

		/// <summary>
		/// Flag inidcating whether the PreviousUnitPrice is a valid value or not.
		/// </summary>
		public bool PreviousUnitPriceSet
		{
			get { return this.m_previousUnitPriceSet; }
		}

		/// <summary>
		/// The difference between Price movement and Predicted Asset Movement
		/// </summary>
		public decimal AssetMovementVariance
		{
			get { return this.m_AMVariance; }

			set { this.m_AMVariance = value; }
		}

		/// <summary>
		/// Whether the difference between Price movement and Predicted Asset Movement has been set
		/// </summary>
		public bool AssetMovementVarianceSet
		{
			get { return this.m_AMVarianceSet; }
		}

		/// <summary>
		/// The tolerance level set for this asset fund
		/// </summary>
		public decimal AssetMovementTolerance
		{
			get { return this.m_AMTolerance; }


			set { this.m_AMTolerance = value; }
		}

		/// <summary>
		/// The tolerance level set for this asset fund as a percentage 
		/// </summary>
		public decimal AssetMovementTolerancePercentage
		{
			get { return this.m_AMTolerance*100; }


			set { this.m_AMTolerance = value/100; }
		}

		/// <summary>
		/// Whether the AM Tolerance has been set for this asset fund
		/// </summary>
		public bool AssetMovementToleranceSet
		{
			get { return this.m_AMToleranceSet; }
		}

		/// <summary>
		/// Whether the Predicted AM movement - Price movement is greater than the tolerance
		/// </summary>
		public bool WithinAssetMovementTolerance
		{
			get { return this.m_withinAMTolerance; }


			set { this.m_withinAMTolerance = value; }
		}

	

		/// <summary>
		/// The latest valuation point for this asset fund
		/// </summary>
		public DateTime ValuationPoint
		{
			get { return this.m_valuationPoint; }


			set { this.m_valuationPoint = value; }

		}

		/// <summary>
		/// Whether the Valuation Point has been set for this asset fund
		/// </summary>
		public bool ValuationPointSet
		{
			get { return this.m_valuationPointSet; }
		}

		/// <summary>
		/// True if any one fund for the asset fund has a current authorised price.  This means the
		/// imported data at the time of authorisation is used rather than the latest (in the case of
		/// reimports).
		/// </summary>
		public bool LockedForCalculation
		{
			get { return this.m_lockedForCalculation; }


			set { this.m_lockedForCalculation = value; }
		}

//        /// <summary>
//        /// The market value splits for this Asset Fund (used for configuration only)
//		/// Todo - remove
//		/// </summary>
//		public AssetFundIndexWeightedCollection MarketValueSplits
//        {
//            get
//            {
//				return this.m_marketValueSplits;
//			}
//
//            set
//            {
//				this.m_marketValueSplits = value;
//			}
//        }

//		/// <summary>
//		/// Returns the collection of movements (IWeightedMovement) which details the following:
//		///		For Linked Asset Funds or OEIC Asset Funds: 
//		///			the currency, market and country codes along with how the currencies and markets have moved in 1 day.
//		///		For Composite Asset Funds:
//		///			the Linked Fund Code and its movement
//		///		(and proportion for both)
//		/// </summary>
//		/// <returns></returns>
//		public WeightedMovementCollection WeightedMovements
//		{
//			get
//			{
//				//only create one if we need to!
//				if (m_weightedMovements == null)
//				{
//					m_weightedMovements = new WeightedMovementCollection();
//				}
//				return m_weightedMovements;
//			}
//
//			// todo - vh - do we need this set for the grid? Thought it was just for properties on the collection,
//			// not the collection itself
//			set
//			{
//				this.m_weightedMovements = value;
//                checkTolerance();
//			}
//		}
//
//		/// <summary>
//		/// The market value splits for this Asset Fund (used for calculations only)
//		/// Todo - remove
//		/// </summary>
//		public AssetFundIndexWeightedCollection MarketIndexMovements
//		{
//			get
//			{
//				return m_weightedIndices;
//			}
//
//			
//			set
//			{
//				this.m_weightedIndices = value;
//			}
//		}

		#endregion

	

		#region Methods

		/// <summary>
		/// Perform the calculations necessary to check whether the asset movement is within the
		/// allowed tolerance or not.
		/// </summary>
		private void checkTolerance()
		{
			try
			{
				this.m_predictedUnitPrice = 0;
				this.m_predictedUnitPriceSet = false;
				this.m_predictedAMSet = false;
				this.m_AMVarianceSet = false;

				//Calculate Predicted AM
				if (this.predictAssetMovement())
				{
					if (this.UnitPriceMovementSet)
					{
						//Set the Asset Movement variance (actual price movement - predicted asset movement)
						this.m_AMVariance = this.UnitPriceMovement - this.PredictedAssetMovement;
						this.m_AMVarianceSet = true;

						//Check tolerance
						if (this.AssetMovementToleranceSet)
						{
							//Check the tolerance and set the Tolerance flag
							this.m_withinAMTolerance
								= (Math.Abs(this.AssetMovementVariance) <= this.AssetMovementTolerance);
						}
					}

					if (this.PreviousUnitPriceSet)
					{
						// Calculated the predicted unit price.
						this.m_predictedUnitPrice
							= this.PreviousUnitPrice + (this.PreviousUnitPrice*this.PredictedAssetMovement);
						this.m_predictedUnitPriceSet = true;
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Calculate the predicted asset movement.
		/// </summary>
		private bool predictAssetMovement()
		{
			decimal assetMovement = 0M;

			assetMovement = assetMovementConstitution.TotalMovement();

			//Set the Predicted Asset Movement
			this.m_predictedAssetMovement = assetMovement;
			this.m_predictedAMSet = true;

			return true;
		}

		#endregion

		#region Public Methods


		/// <summary>
		/// Override the Equals as we are only worried if the indexname is the same and not the actual objects
		/// </summary>
		/// <param name="obj"></param>
		/// <returns>true is they have the same index name</returns>
		public override bool Equals(object obj)
		{
			AssetFund assetfund =  obj as AssetFund;

			if (obj is AssetFund)
			{
				return (assetfund == this);
			}
			else
			{
				return base.Equals(obj);
			}
		}

		/// <summary>
		/// Overloaded equality operator
		/// </summary>
		/// <param name="lhs">First Object to compare</param>
		/// <param name="rhs">Second Object to compare</param>
		/// <returns></returns>
		public static bool operator==(AssetFund lhs,AssetFund rhs)
		{
			if ((object)lhs !=null && (object)rhs!=null )
			{
				return (lhs.AssetFundCode.TrimEnd()==rhs.AssetFundCode.TrimEnd());
			}
			else
			{
				return (object)lhs==(object)rhs;
			}
		}

		/// <summary>
		/// Overloaded inequality operator
		/// </summary>
		/// <param name="lhs">First Object to compare</param>
		/// <param name="rhs">Second Object to compare</param>
		/// <returns></returns>
		public static bool operator!=(AssetFund lhs,AssetFund rhs)
		{
			return !(lhs==rhs);
		}

		/// <summary>
		/// Gets the hash code.
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>
		/// Indicates whether the asset fund is locked. 
		/// Checks child fund is authorised for this.
		/// </summary>
		/// <param name="assetFundChildFunds"></param>
		/// <returns></returns>
		public static bool IsLocked(FundCollection assetFundChildFunds)
		{
			//TODO - should the assetChildFunds actually be made a member? 
			bool isLocked = false;
			if (assetFundChildFunds == null)
			{
				throw new NullReferenceException("funds collection is null");
			}

			for (int i = 0; i < assetFundChildFunds.Count; i++)
			{
				Fund childFund = assetFundChildFunds[i];
				if (childFund.IsAuthorised)
				{
					isLocked = true;
					break;
				}
			}

			return isLocked;
		}

//		/// <summary>
//		/// Accepts a collection of movements following an update (by UI). 
//		/// This clears the snapshotID and updates local collection with the changed collection.
//		/// Note that composites cannot be updated so an exception is thrown if 
//		/// a collection of composite weightings is passed in
//		/// </summary>
//		/// <param name="updatedMovements"></param>
//		public void UpdateWeightedMovements(WeightedMovementCollection updatedMovements)
//		{
//			//business logic so is here, not in UI
//
//			//what we have to do is clear the existing collection of all movements,
//			//then copy these movements in, and set snapshotID to 0 to indicate that
//			//we need to generate a new SnapshotID
//
//			if (m_weightedIndices == null)
//			{
//				m_weightedMovements = new WeightedMovementCollection();
//			}
//			else
//			{
//				m_weightedMovements.Clear();
//			}
//			for (int i = 0; i < updatedMovements.Count; i++)
//			{
//				if (updatedMovements[i] is WeightedIndex)
//				{
//					((WeightedIndex) updatedMovements[i]).SnapshotID = 0;
//					((WeightedIndex) updatedMovements[i]).AssetFundCode = m_assetFundCode;
//				}
//				else
//				{
//					//currently not possible to update Composite Weightings
//					throw new ArgumentException ("Unexpected type in weighted movements collection");
//				}
//				m_weightedMovements.Add(updatedMovements[i]);
//			}
//			SetDirtyFlag();
//		}

		/// <summary>
		/// Gets the currency.
		/// </summary>
		/// <value></value>
		public Currency.Currency Currency
		{
			get
			{
				if (currency == null)
				{
					currency = HBOS.FS.AMP.UPD.Types.Currency.Currency.GlobalMarket;
				}
				return currency;
			}
		}

		/// <summary>
		/// Gets the asset movement constitute collection containing all the weighted benchmarks.
		/// </summary>
		/// <value></value>
		public AssetMovementConstituentCollection AssetMovementConstituents
		{
			get { return assetMovementConstitution; }
			set
			{
				assetMovementConstitution.Clear();

				//check that all constituent parts parent property is set to this asset fund
				//do this after the asset fund has been constructed
				if (value != null)
				{
					foreach (AssetMovementConstituent constitute in value)
					{
						if (constitute != null )
						{
							if (constitute.ParentAssetFund==null)
							{
								constitute.ParentAssetFund = this;
								assetMovementConstitution.Add(constitute);
							}
							else if(constitute.ParentAssetFund.AssetFundCode==this.AssetFundCode)
							{
								constitute.ParentAssetFund = this;
								assetMovementConstitution.Add(constitute);
							}
						}
					}

//					if (assetMovementConstitution.Count>0 &&  assetMovementConstitution.TotalProportion() != 1)
//					{
//					//	throw new InvalidOperationException("The proportion of the benchmarks must add up to 100%");
//					}
					assetMovementConstitution.IsDirty = true;
				}
				else
				{
					assetMovementConstitution=new AssetMovementConstituentCollection();
				}

			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether [currency rate snapshot ID set].
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [currency rate snapshot ID set]; otherwise, <c>false</c>.
		/// </value>
		internal bool IndexValueSnapshotIDSet
		{
			get { return this.m_indexValueSnapshotID>0; }
		}

		/// <summary>
		/// Gets or sets the currency rate snapshot ID.
		/// </summary>
		/// <value></value>
		internal long IndexValueSnapshotID
		{
			get { return m_indexValueSnapshotID; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether [currency rate snapshot ID set].
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [currency rate snapshot ID set]; otherwise, <c>false</c>.
		/// </value>
		internal bool CurrencyRateSnapshotIDSet
		{
			get { return m_currencyRateSnapshotIDSet; }
		}

		/// <summary>
	/// Gets or sets the currency rate snapshot ID.
		/// </summary>
		/// <value></value>
		internal long CurrencyRateSnapshotID
		{
			get { return m_currencyRateSnapshotID; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether [benchmark split snapshot ID set].
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [benchmark split snapshot ID set]; otherwise, <c>false</c>.
		/// </value>
		internal bool BenchmarkSplitSnapshotIDSet
		{
			get { return m_benchmarkSplitSnapshotIDSet; }
		}

		/// <summary>
		/// Gets or sets the benchmark split snapshot ID.
		/// </summary>
		/// <value></value>
		internal long BenchmarkSplitSnapshotID
		{
			get { return m_benchmarkSplitSnapshotID; }
		}

		/// <summary>
		/// Gets the asset fund type.
		/// </summary>
		/// <value></value>
		public AssetFundTypeEnum AssetFundType
		{
			get { return type; }
			set{type=value;}
		}

		internal bool IndexValueSnapshotIdSet
		{
			get { return m_IndexValueSnapshotIdSet; }
		}

		/// <summary>
		/// Gets the price file id.
		/// </summary>
		/// <value></value>
		public int PriceFileId
		{
			get { return m_PriceFileId; }
			set
			{ m_PriceFileId=value; }
		}

		#endregion
	}
}