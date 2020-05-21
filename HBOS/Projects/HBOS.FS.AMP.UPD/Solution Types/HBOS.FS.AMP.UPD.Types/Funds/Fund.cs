using System;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.BenchMark;
using HBOS.FS.AMP.UPD.Types.Factors;
using HBOS.FS.AMP.UPD.Types.FundGroups;

namespace HBOS.FS.AMP.UPD.Types.Funds
{
	/// <summary>
	/// Base class for all funds. Is now abstract in order that fund type may be determined 
	/// by the specific class type inheriting from this base.
	/// </summary>
	public abstract class Fund : EntityBase, IBenchMark
	{
		#region FundAuthoriseInfo class used for authorisation and pricing

		/// <summary>
		/// Summary description for FundAuthoriseInfo.
		/// </summary>
		public class FundAuthoriseInfo
		{
			private string m_HiPortfolioCode;
			private string m_AssetFundID;
			private bool m_WasFromPrediction;
			private decimal m_Price;
			private int m_ScalingFactorID;
			private bool m_IsBidPriceMidPrice;
			private long m_ImportedFundPriceID;
			private decimal m_ImportedPrice;
			private decimal m_PredictedPrice;
			private decimal m_PredictedAssetUnitPrice;
			private long m_tolerancesSnapshotID;
			private int m_TaxProvisionFactorID;
			private int m_XFactorId;
			private decimal m_AssetUnitPrice;
			private long m_CurrencyRateSnapshotID;
			private long m_BenchmarkSplitSnapShotId;
			private string m_FundCurrencyCode;
			private decimal m_ValuationBasisEffect;
			private int m_RevaluationFactorId;
			private long m_MarketIndiciesSnapShotId;
			private decimal m_predictedAssetMovement;

			/// <summary>
			/// Creates a new <see cref="FundAuthoriseInfo"/> instance. 
			/// This class can be used to access information used in authorisation 
			/// or other pricing calculations
			/// </summary>
			/// <param name="fund">Fund.</param>
			public FundAuthoriseInfo(Fund fund)
			{
				m_HiPortfolioCode = fund.HiPortfolioCode;
				m_AssetFundID = fund.AssetFundID;
				m_FundCurrencyCode = fund.Currency.CurrencyCode;
				m_WasFromPrediction = fund.UsePredictedPrice;
				m_Price = predictedOrImportedPrice(fund);
				m_ScalingFactorID = scalingFactorId(fund);
				m_IsBidPriceMidPrice = isBidPriceMidPrice(fund);
				m_ImportedFundPriceID = importedFundPriceID(fund);
				m_ImportedPrice = fund.Price;
				m_PredictedPrice = fund.PredictedPrice;
				m_PredictedAssetUnitPrice = predictedAssetUnitPrice(fund);
				m_TaxProvisionFactorID = taxProvisionFactorID(fund);
				m_XFactorId = xFactorId(fund);
				m_AssetUnitPrice = assetUnitPrice(fund);
				m_CurrencyRateSnapshotID = currencyRateSnapshotID(fund);
				m_BenchmarkSplitSnapShotId = benchmarkSplitSnapshotID(fund);
				m_ValuationBasisEffect = fund.ValuationBasisEffect;
				m_tolerancesSnapshotID = tolerancesSnapshotID(fund);
				m_RevaluationFactorId = revaluationFactorId(fund);
				m_MarketIndiciesSnapShotId = marketIndiciesSnapShotId(fund);
				m_predictedAssetMovement = fund.PredictedAssetMovement;
			}

			#region Set Helper Methods

			private bool currencyRateSnapshotIDSet(Fund fund)
			{
				return fund.ParentAssetFund.CurrencyRateSnapshotIDSet;
			}

			private bool benchmarkSnapshotIDSet(Fund fund)
			{
				return fund.ParentAssetFund.BenchmarkSplitSnapshotIDSet;
			}

			private bool assetUnitPriceSet(Fund fund)
			{
				return (fund.ParentAssetFund != null && fund.ParentAssetFund.UnitPriceSet);
			}

			private bool importedFundPriceIDSet(Fund fund)
			{
				return fund.m_importedFundPriceID > 0;
			}

			/// <summary>
			/// Flag to indicate whether the tolerances ID is a valid value or not.
			/// </summary>
			public bool tolerancesSnapshotIDSet(Fund fund)
			{
				return fund.m_tolerancesID > 0;
			}

			/// <summary>
			/// Flag indicating that the predicted asset unit price is valid.
			/// </summary>
			private bool predictedAssetUnitPriceSet(Fund fund)
			{
				return fund.ParentAssetFund != null && fund.ParentAssetFund.PredictedUnitPriceSet;
			}

			private bool taxProvisionFactorIDSet(NonOEIC fund)
			{
				return fund.m_factors.TPE.FactorID > 0;
			}

			private bool scalingFactorIDSet(NonOEIC fund)
			{
				return fund.m_factors.ScalingFctr.FactorIDSet;
			}

			private bool xFactorIDSet(Fund fund)
			{
				return fund.m_factors.XFctr.FactorIDSet;
			}

			#endregion

			#region Methods to resolve values based on conditions

			private long marketIndiciesSnapShotId(Fund fund)
			{
				long returnValue = 0;

				if (fund.ParentAssetFund != null && fund.ParentAssetFund.IndexValueSnapshotIDSet)
				{
					returnValue = fund.ParentAssetFund.IndexValueSnapshotID;
				}

				return returnValue;
			}

			private long tolerancesSnapshotID(Fund fund)
			{
				long returnValue = 0;

				if (tolerancesSnapshotIDSet(fund))
				{
					returnValue = fund.m_tolerancesID;
				}

				return returnValue;
			}

			private int revaluationFactorId(Fund fund)
			{
				NonOEIC nonOEICFund = fund as NonOEIC;
				int returnValue = 0;

				if (nonOEICFund != null && nonOEICFund.RevaluationFactorIDSet)
				{
					returnValue = fund.m_factors.RevaluationFctr.FactorID;
				}
				return returnValue;
			}

			private Int64 currencyRateSnapshotID(Fund fund)
			{
				long returnValue = 0;

				if (currencyRateSnapshotIDSet(fund))
				{
					returnValue = fund.ParentAssetFund.CurrencyRateSnapshotID;
				}

				return returnValue;
			}

			private Int64 benchmarkSplitSnapshotID(Fund fund)
			{
				long returnValue = 0;

				if (benchmarkSnapshotIDSet(fund))
				{
					returnValue = fund.ParentAssetFund.BenchmarkSplitSnapshotID;
				}

				return returnValue;
			}

			private int xFactorId(Fund fund)
			{
				int returnValue = 0;
				if (xFactorIDSet(fund))
				{
					returnValue = fund.XFactorID;
				}
				return returnValue;
			}


			private int taxProvisionFactorID(Fund fund)
			{
				NonOEIC nonOEICFund = fund as NonOEIC;
				int returnValue = 0;
				if (nonOEICFund != null && taxProvisionFactorIDSet(nonOEICFund))
				{
					returnValue = nonOEICFund.m_factors.TPE.FactorID;
				}
				return returnValue;
			}

			private decimal predictedAssetUnitPrice(Fund fund)
			{
				// Predicted asset unit price
				decimal returnValue = decimal.MinValue;

				if (predictedAssetUnitPriceSet(fund))
				{
					returnValue = fund.PredictedAssetUnitPrice;
				}
				return returnValue;
			}

			private long importedFundPriceID(Fund fund)
			{
				long returnID = 0;
				if (importedFundPriceIDSet(fund))
				{
					returnID = fund.ImportedFundPriceID;
				}
				return returnID;
			}

			private int scalingFactorId(Fund fund)
			{
				NonOEIC nonOEICFund = fund as NonOEIC;
				int returnValue = 0;

				if (nonOEICFund != null)
				{
					if (scalingFactorIDSet(nonOEICFund))
					{
						returnValue = nonOEICFund.m_factors.ScalingFctr.FactorID;
					}
				}
				return returnValue;
			}

			private bool isBidPriceMidPrice(Fund fund)
			{
				// Get the correct fund type UseMidPriceAsBidPrice value
				NonOEIC nonOEICFund = fund as NonOEIC;
				bool returnValue = false;

				if (nonOEICFund != null && taxProvisionFactorIDSet(nonOEICFund))
				{
					returnValue = nonOEICFund.UseMidPriceAsBidPrice;
				}

				return returnValue;
			}

			private decimal predictedOrImportedPrice(Fund fund)
			{
				decimal returnValue = decimal.MinValue;
				if (fund.UsePredictedPrice)
				{
					returnValue = fund.PredictedPrice;
				}
				else
				{
					if (fund.PriceSet)
					{
						returnValue = fund.Price;
					}
				}
				return returnValue;
			}

			private decimal assetUnitPrice(Fund fund)
			{
				decimal returnValue = decimal.MinValue;
				if (assetUnitPriceSet(fund))
				{
					returnValue = fund.AssetUnitPrice;
				}
				else
				{
					if (predictedAssetUnitPriceSet(fund) && fund.UsePredictedPrice)
					{
						returnValue = fund.PredictedAssetUnitPrice;
					}
				}
				return returnValue;
			}

			#endregion

			#region Public Properties

			/// <summary>
			/// Returns the asset movement
			/// </summary>
			public decimal PredictedAssetMovement
			{
				get { return this.m_predictedAssetMovement; }
			}


			/// <summary>
			/// The ID for the tolerances used in the price calculation.
			/// </summary>
			public Int64 TolerancesSnapshotID
			{
				get { return this.m_tolerancesSnapshotID; }

			}

			/// <summary>
			/// Gets the hi portfolio code.
			/// </summary>
			/// <value></value>
			public string HiPortfolioCode
			{
				get { return m_HiPortfolioCode; }
			}

			/// <summary>
			/// Gets the asset fund ID.
			/// </summary>
			/// <value></value>
			public string AssetFundID
			{
				get { return m_AssetFundID; }
			}

			/// <summary>
			/// Gets a value indicating whether [was from prediction].
			/// </summary>
			/// <value>
			/// 	<c>true</c> if [was from prediction]; otherwise, <c>false</c>.
			/// </value>
			public bool WasFromPrediction
			{
				get { return m_WasFromPrediction; }
			}

			/// <summary>
			/// Gets the price.
			/// </summary>
			/// <value></value>
			public decimal Price
			{
				get { return m_Price; }
			}

			/// <summary>
			/// Gets the scaling factor ID.
			/// </summary>
			/// <value></value>
			public int ScalingFactorID
			{
				get { return m_ScalingFactorID; }
			}

			/// <summary>
			/// Gets a value indicating whether this instance is bid price mid price.
			/// </summary>
			/// <value>
			/// 	<c>true</c> if this instance is bid price mid price; otherwise, <c>false</c>.
			/// </value>
			public bool IsBidPriceMidPrice
			{
				get { return m_IsBidPriceMidPrice; }
			}

			/// <summary>
			/// Gets the imported fund price.
			/// </summary>
			/// <value></value>
			public decimal ImportedFundPrice
			{
				get { return m_ImportedPrice; }
			}

			/// <summary>
			/// Gets the predicted fund price.
			/// </summary>
			/// <value></value>
			public decimal PredictedFundPrice
			{
				get { return m_PredictedPrice; }
			}

			/// <summary>
			/// Gets the predicted asset unit price.
			/// </summary>
			/// <value></value>
			public decimal PredictedAssetUnitPrice
			{
				get { return m_PredictedAssetUnitPrice; }
			}

			/// <summary>
			/// Gets the imported fund price ID.
			/// </summary>
			/// <value></value>
			public long ImportedFundPriceID
			{
				get { return m_ImportedFundPriceID; }
			}

			/// <summary>
			/// Gets the tax provision factor ID.
			/// </summary>
			/// <value></value>
			public int TaxProvisionFactorID
			{
				get { return m_TaxProvisionFactorID; }
			}

			/// <summary>
			/// Gets the X factor id.
			/// </summary>
			/// <value></value>
			public int XFactorId
			{
				get { return m_XFactorId; }
			}

			/// <summary>
			/// Gets the asset unit price.
			/// </summary>
			/// <value></value>
			public decimal AssetUnitPrice
			{
				get { return m_AssetUnitPrice; }
			}

			/// <summary>
			/// Gets the currency rate snapshot ID.
			/// </summary>
			/// <value></value>
			public long CurrencyRateSnapshotID
			{
				get { return m_CurrencyRateSnapshotID; }
			}

			/// <summary>
			/// Gets the benchmark split snap shot id.
			/// </summary>
			/// <value></value>
			public long BenchmarkSplitSnapShotId
			{
				get { return m_BenchmarkSplitSnapShotId; }
			}

			/// <summary>
			/// Gets the fund currency code.
			/// </summary>
			/// <value></value>
			public string FundCurrencyCode
			{
				get { return m_FundCurrencyCode; }
			}

			/// <summary>
			/// Gets the valuation basis effect.
			/// </summary>
			/// <value></value>
			public decimal ValuationBasisEffect
			{
				get { return m_ValuationBasisEffect; }
			}

			/// <summary>
			/// Gets the revaluation factor id.
			/// </summary>
			/// <value></value>
			public int RevaluationFactorId
			{
				get { return m_RevaluationFactorId; }
			}

			/// <summary>
			/// Gets the market indicies snap shot id.
			/// </summary>
			/// <value></value>
			public long MarketIndiciesSnapShotId
			{
				get { return m_MarketIndiciesSnapShotId; }
			}

			#endregion
		}

		#endregion

		#region Public enumerations

		/// <summary>
		/// List of possible fund statuses
		/// </summary>
		public enum FundStatusType
		{
			/// <summary>
			/// Fund status code is unrecognised
			/// </summary>
			Unknown = 0,

			/// <summary>
			/// Fund is missing
			/// </summary>
			Missing = 1,

			/// <summary>
			/// Fund data has been imported
			/// </summary>
			Imported = 2,

			/// <summary>
			/// Fund has been first level authorised
			/// </summary>
			FirstLevelAuthorised = 4,

			/// <summary>
			/// Fund has been second level authorised
			/// </summary>
			SecondLevelAuthorised = 8,

			/// <summary>
			/// Fund has been released to distribution
			/// </summary>
			Released = 16,

			/// <summary>
			/// Fund is awaiting distribution
			/// </summary>
			AwaitingDistribution = 32,

			/// <summary>
			/// Fund is in the process of being distributed
			/// </summary>
			BeingDistributed = 64,

			/// <summary>
			/// Fund has been distributed
			/// </summary>
			Distributed = 128
		} ;

		#endregion

		#region Private variable declaration

		#region Drop II

		//
		// Note. As we work through the drop II use cases move the member
		//       variables that we still need in this class into this 
		//       region so what is left we know we can remove from the class.
		//

		// Property variables - persisted values

		private string m_fullName = string.Empty;
		private string m_shortName = string.Empty;
		private string m_classOrSeriesCode = string.Empty;
		private bool m_onHiPortfolio3 = false;
		private string m_parentAssetFundID = string.Empty;
		private AssetFund m_parentAssetFund = null;
		private decimal m_upperTolerance = 0M;
		private decimal m_lowerTolerance = 0M;
		private Currency.Currency m_Currency;

		//is a percentile to 4dp which is a fraction/ratio to 6dp
		private const int toleranceNumDecimalPlaces = 6;

		//Allow a null collection - as fund group persister returns a collection, calling client will
		//mostly just set the fund collection on the fund.
		private FundGroupCollection m_fundGroups = null;

		/// <summary>
		/// Collection of factors - added as part of type refactoring / addition of composite
		/// </summary>
		protected FactorCollection m_factors = new FactorCollection();

		//added 7th June 2005 - required for static data export
		private string m_companyCode = string.Empty;

		private string m_securityCode = string.Empty;

		//collection of fundgroups for parent asset fund
		//only used for static data export
		private FundGroupCollection m_parentFundGroups = null;

		#endregion

		#region Drop I

		private string m_fundCode = string.Empty;
		private ExternalSystemIDCollection m_systemIDs = null;

		private decimal m_assetMovementTolerance = 0M;
		private bool m_priceIncreaseOnly = false;
		private Int64 m_tolerancesID = 0;
		private decimal m_price;
		private bool m_priceSet = false;
		private long m_importedFundPriceID = 0;
		private decimal m_previousPrice = 0M;
		private bool m_previousPriceSet = false;
		private FundStatusType m_fundStatus = FundStatusType.Missing;
		private DateTime m_statusChangedTime;
		private bool m_statusChangedTimeSet = false;
		private bool m_usePredictedPrice = false;
		private byte[] m_authorisedPriceTimestamp = null;

		private bool m_progressStatus = false;
		private bool m_initialProgressStatus = false;

		#endregion 

		private bool m_benchMarkable = false;

		#endregion

		#region Constructors

		/// <summary>
		/// Default constructor creating an object with all properties initialised to the default values.
		/// </summary>
		protected Fund()
		{
		}

		/// <summary>
		/// Toes the string.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return this.ShortName;
		}

		/// <summary>
		/// Creates a new rehydrated <see cref="Fund"/> instance.
		/// </summary>
		/// <param name="fundParameters"></param>
		public Fund(FundFactory.FundParameters fundParameters)
		{
			// Populate the local member fields.
			this.m_fundCode = fundParameters.HiPortfolioCode;
			this.m_fullName = fundParameters.FullName;
			this.m_shortName = fundParameters.ShortName;

			this.m_classOrSeriesCode = fundParameters.ClassOrSeriesCode;
			this.m_onHiPortfolio3 = fundParameters.OnHiPortfolio3;
			this.m_parentAssetFund = fundParameters.ParentAssetFund;
			if (m_parentAssetFund != null) m_Currency = fundParameters.ParentAssetFund.Currency;
			this.m_assetMovementTolerance = fundParameters.AssetMovementTolerance;
			this.m_upperTolerance = fundParameters.UpperTolerance;
			this.m_lowerTolerance = fundParameters.LowerTolerance;
			this.m_priceIncreaseOnly = fundParameters.PriceIncreaseOnly;
			this.m_tolerancesID = fundParameters.TolerancesID;
			this.m_price = fundParameters.Price;
			this.m_priceSet = fundParameters.PriceSet;
			this.m_importedFundPriceID = fundParameters.ImportedFundPriceID;
			this.m_previousPrice = fundParameters.PreviousPrice;
			this.m_previousPriceSet = fundParameters.PreviousPriceSet;
			this.m_fundStatus = fundParameters.FundStatus;
			this.m_statusChangedTime = fundParameters.StatusChangedTime;
			this.m_statusChangedTimeSet = fundParameters.StatusChangedTimeSet;
			this.m_authorisedPriceTimestamp = fundParameters.AuthorisedPriceTimestamp;
			this.m_parentAssetFundID = fundParameters.ParentAssetFundID;
			this.m_usePredictedPrice = fundParameters.UsePredictedPrice;
			this.m_benchMarkable = fundParameters.IsBenchmarkable;

			// Now assign the funds progress status
			this.m_progressStatus = (this.m_fundStatus >= FundStatusType.SecondLevelAuthorised && !this.m_usePredictedPrice);
			this.m_initialProgressStatus = this.m_progressStatus;


			// Set up the IEntityBase members.
			this.m_timestamp = fundParameters.Timestamp;
			this.m_isNew = false;
			this.m_isDeleted = false;
			this.m_isDirty = false;

			this.m_companyCode = fundParameters.CompanyCode; //added 7/6/05 - used for static data report. Null is acceptable for all other uses.
			this.m_securityCode = fundParameters.SecurityCode;

			if (fundParameters.Factors == null)
			{
				throw new NullReferenceException("factors collection must be created prior to fund construction");
			}
			this.m_factors = fundParameters.Factors;
		}

		#endregion Constructor

		#region Fund properties

		/// <summary>
		/// caches the fundgroups for the parent asset fund
		/// Only used by static data export - saves loading up the whole asset fund
		/// </summary>
		public FundGroupCollection ParentFundGroups
		{
			get { return m_parentFundGroups; }
			set { m_parentFundGroups = value; }
		}


		/// <summary>
		/// Gets a value indicating whether this instance is bench markable and can be used as a bench mark.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is bench markable; otherwise, <c>false</c>.
		/// </value>
		public bool IsBenchMarkable
		{
			get { return m_benchMarkable; }
			set
			{
				m_benchMarkable = value;
				this.setDirtyFlag();
			}
		}


		/// <summary>
		/// A string representation of the type used for display and reporting purposes.
		/// todo - remove display properties from the entity once grid improved.
		/// </summary>
		public abstract string FundType { get; }

		/// <summary>
		/// Returns the collection of factors (things that can affect the predicted price)
		/// This is a collection of type Factor base, which can be one of
		///		XFactor
		///		RevaluationFactor
		///		TaxProvisionEstimate or
		///		ScalingFactor
		///	All support IFactor
		/// </summary>
		/// <returns></returns>
		public FactorCollection Factors
		{
			get { return m_factors; }
		}

		/// <summary>
		/// The collection of IndividualFundGroup that detail the fund groups that this fund is linked to.
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
		/// Company code - only used for reports
		/// </summary>
		public string CompanyCode
		{
			get { return m_companyCode; }

			set
			{
				m_companyCode = value;
				this.setDirtyFlag();
			}
		}

		/// <summary>
		/// The HiPortfolio fund code. Todo - rename to FundCode.
		/// </summary>
		public string HiPortfolioCode
		{
			get { return m_fundCode; }

			set
			{
				m_fundCode = value;
				this.setDirtyFlag();
			}
		}

		/// <summary>
		/// The full name of fund.
		/// </summary>
		public string FullName
		{
			get { return this.m_fullName; }

			set
			{
				this.m_fullName = value;
				this.setDirtyFlag();
			}
		}

		/// <summary>
		/// The short name of fund.
		/// </summary>
		public string ShortName
		{
			get { return this.m_shortName; }

			set
			{
				this.m_shortName = value;
				this.setDirtyFlag();
			}
		}

		/// <summary>
		/// The IDs used to identify this fund on external systems.
		/// </summary>
		public ExternalSystemIDCollection SystemIDs
		{
			get { return this.m_systemIDs; }

			set
			{
				this.m_systemIDs = value;
				this.setDirtyFlag();
			}
		}

		/// <summary>
		/// The share class code or price series number.
		/// </summary>
		public string ClassOrSeriesCode
		{
			get { return this.m_classOrSeriesCode; }

			set
			{
				this.m_classOrSeriesCode = value;
				this.setDirtyFlag();
			}
		}


		/// <summary>
		/// Flag indicating whether the fund is on HiPortfolio3 or not.
		/// </summary>
		public bool OnHiPortfolio3
		{
			get { return this.m_onHiPortfolio3; }

			set
			{
				this.m_onHiPortfolio3 = value;
				this.setDirtyFlag();
			}
		}


		/// <summary>
		/// The asset fund that this fund belongs to.  Used to return properties specific to the asset
		/// fund.
		/// </summary>
		public AssetFund ParentAssetFund
		{
			get { return this.m_parentAssetFund; }

			set
			{
				this.m_parentAssetFund = value;
				this.setDirtyFlag();
			}
		}

		/// <summary>
		/// A flag indicating whether or not the fund price has been authorised
		/// </summary>
		public bool IsAuthorised
		{
			get { return m_fundStatus >= FundStatusType.FirstLevelAuthorised; }

		}

		/// <summary>
		/// The tolerance for the asset movement of the fund.
		/// </summary>
		public decimal AssetMovementTolerance
		{
			get { return this.m_assetMovementTolerance; }

			set
			{
				this.m_assetMovementTolerance = value;
				this.setDirtyFlag();
			}
		}

		/// <summary>
		/// The upper limit of price movement tolerance for this fund.
		/// </summary>
		public decimal UpperTolerance
		{
			get { return this.m_upperTolerance; }

			set
			{
				this.m_upperTolerance = value;
				this.setDirtyFlag();
			}
		}

		/// <summary>
		/// The lower limit of price movement tolerance for this fund.
		/// </summary>
		public decimal LowerTolerance
		{
			get { return this.m_lowerTolerance; }

			set
			{
				this.m_lowerTolerance = value;
				this.setDirtyFlag();
			}
		}

		/// <summary>
		/// Flag indicating whether the fund price is allowed to fall or not.
		/// </summary>
		public bool PriceIncreaseOnly
		{
			get { return this.m_priceIncreaseOnly; }

			set
			{
				this.m_priceIncreaseOnly = value;
				this.setDirtyFlag();
			}
		}

		/// <summary>
		/// The ID for the tolerances used in the price calculation.
		/// </summary>
		public Int64 TolerancesID
		{
			get { return this.m_tolerancesID; }
			set { this.m_tolerancesID = value; }
		}

		/// <summary>
		/// Flag to indicate whether the tolerances ID is a valid value or not.
		/// </summary>
		public bool TolerancesIDSet
		{
			get { return this.m_tolerancesID > 0; }
		}


		/// <summary>
		/// The current imported price of the fund.
		/// </summary>
		public decimal Price
		{
			get { return m_price; }


			set { this.m_price = value; }
		}

		/// <summary>
		/// Flag indicating whether the price field has been set or not as zero may be a valid value.
		/// </summary>
		public bool PriceSet
		{
			get { return this.m_priceSet; }


			set { this.m_priceSet = value; }
		}

		/// <summary>
		/// The ID for the imported fund price used in the price calculation.
		/// </summary>
		public long ImportedFundPriceID
		{
			get { return this.m_importedFundPriceID; }


			set { this.m_importedFundPriceID = value; }
		}

		/// <summary>
		/// Flag indicating whether the imported fund price ID is a valid value or not.
		/// </summary>
		public bool ImportedFundPriceIDSet
		{
			get { return this.m_importedFundPriceID > 0; }
		}

		//
		// TODO: No requirement for this property.  Remove during the next refactoring phase
		//
		/// <summary>
		/// The ID for the valuation basis used in the price calculation.
		/// </summary>
		public virtual int ValuationBasisID
		{
			get { return this.m_factors.ValuationBasisFctr.FactorID; }


			set { this.m_factors.ValuationBasisFctr.FactorID = value; }
		}

		//
		// TODO: No requirement for this property.  Remove during the next refactoring phase
		//
		/// <summary>
		/// Flag indicating whether the valuation basis ID is a valid value or not.
		/// </summary>
		public virtual bool ValuationBasisIDSet
		{
			get { return this.m_factors.ValuationBasisFctr.FactorIDSet; }
		}

		/// <summary>
		/// The most recent previous price for the fund.
		/// </summary>
		public decimal PreviousPrice
		{
			get { return this.m_previousPrice; }


			set { this.m_previousPrice = value; }
		}

		/// <summary>
		/// Flag indicating whether the previous price field has been set or not as zero may be a 
		/// valid value.
		/// </summary>
		public bool PreviousPriceSet
		{
			get { return this.m_previousPriceSet; }


			set { this.m_previousPriceSet = value; }
		}


		/// <summary>
		/// The current fund status.
		/// </summary>
		public FundStatusType FundStatus
		{
			get { return this.m_fundStatus; }

			set
			{
				this.m_fundStatus = value;
				this.setDirtyFlag();
			}
		}


		/// <summary>
		/// The date and time that the status was changed (excluding tolerance status changes).
		/// </summary>
		public DateTime StatusChangedTime
		{
			get { return this.m_statusChangedTime; }

			set
			{
				this.m_statusChangedTime = value;
				this.m_statusChangedTimeSet = true;
				this.setDirtyFlag();
			}
		}

		/// <summary>
		/// Flag to indicate whether the StatusChangedTime has been set because it defaults to a 
		/// valid date.
		/// </summary>
		public bool StatusChangedTimeSet
		{
			get { return this.m_statusChangedTimeSet; }


			set { this.m_statusChangedTimeSet = value; }
		}

		/// <summary>
		/// Will be set to true if the predicted price is to be used as the 
		/// authorised price.
		/// </summary>
		public bool UsePredictedPrice
		{
			get { return this.m_usePredictedPrice; }

			set
			{
				this.m_usePredictedPrice = value;
				// Do not set the IsDirty flag for an update to this property because it will only be
				// updated as part of a status change operation.
			}
		}

		/// <summary>
		/// The timestamp on the authorised fund price file for this fund.
		/// </summary>
		public byte[] AuthorisedPriceTimestamp
		{
			get { return this.m_authorisedPriceTimestamp; }

			set { this.m_authorisedPriceTimestamp = value; }
		}

		/// <summary>
		/// The calculated predicted price for the current day.
		/// Calculate the predicted price for the fund using:
		/// predicted = previous * (1 + AM + RF + VF + SF + X) * (1 - TI)
		/// where - AM is asset movement (calculated)
		///       - TI is tax impact (non oeics - user input)
		///       - RF is revaluation factor 
		///       - VF is valuation factor (calculated)
		///       - SF is scaling factor (user input) and
		///       - X is X factor (user input).
		/// All the factors are held as decimal fractions rather than percentages (ie. if there is a
		/// 10% movement in asset funds then this is used as 0.1 in the calculations).
		/// The fund weightings are also held as decimal fractions rather than percentages and so must
		/// add up to 1 rather than to 100.
		/// </summary>
		public virtual decimal PredictedPrice
		{
			get
			{
				//Predicted Price Today = 
				//	Previous day's autorised price * (1 + predictedMovement)
				//		where predictedMovement = (Asset Movement + Sum of Factors (excluding TPE)) * (1 - TPE)	

				if (!this.PreviousPriceSet || !this.PredictedAssetMovementSet)
				{
					return 0M;
				}

				return this.PreviousPrice * (1 + predictedMovement) * m_factors.TPE.CalculateEffect();

			}


			set
			{
				//do nothing - it's a derived value. This is left in for the grid
			}
		}

		/// <summary>
		/// calculates the predicted movement to be applied to the price in order to calculate predicted price
		/// </summary>
		protected virtual decimal predictedMovement
		{
			get
			{
				if (!this.PreviousPriceSet || !this.PredictedAssetMovementSet)
				{
					return 0M;
				}

				// Calculate the predicted movement.
				//Predicted Movement Today = 
				//	(Asset Movement + Sum of Factors (excluding TPE)) * (1 - TPE)	

				//so this base formula is for non tpe based funds only (i.e. oeics or non life)
				//override in composite & linked to call this base and add on TPE effect
				decimal movement = this.PredictedAssetMovement;
				if (m_factors != null && m_factors.Count > 0)
				{
					movement += m_factors.SumFactors();
				}
				return movement;

			}
		}


		/// <summary>
		/// The calculated valuation basis effect used in price prediction.
		/// </summary>
		public virtual decimal ValuationBasisEffect
		{
			get { return this.m_factors.ValuationBasisFctr.RatioValue; }


			set
			{
				this.m_factors.ValuationBasisFctr.RatioValue = value;
				setDirtyFlag();
			}
		}

		/// <summary>
		/// Flag to indicate whether the asset movement is within the tolerance or not.
		/// </summary>
		public bool WithinAssetMovementTolerance
		{
			get
			{
				if (m_parentAssetFund != null)
				{
					return m_parentAssetFund.WithinAssetMovementTolerance;
				}
				return false;
			}
		}

		/// <summary>
		/// Check to see if the price fails both the upper and lower tolerance
		/// </summary>
		public bool PriceOutsideTolerance
		{
			get { return (!WithinPriceLowerTolerance && !WithinPriceUpperTolerance); }
		}


		/// <summary>
		/// Flag to indicate whether the price is within the lower tolerance or not.
		/// </summary>
		public bool WithinPriceLowerTolerance
		{
			get
			{
				if (this.PriceSet)
				{
					decimal priceVariance = Math.Abs(this.PriceMovementVariance);
					return priceVariance <= this.LowerTolerance;
				}
				else
				{
					return false;
				}
			}


			set
			{
				//do nothing - its a derived value. Set left in for the grid.
			}
		}


		/// <summary>
		/// Flag to indicate if the price is within rounded price tolerance
		/// </summary>
		public bool WithinRoundedPriceLowerTolerance
		{
			get
			{
				if (this.PriceSet)
				{
					decimal priceVariance = Math.Abs(this.PriceMovementVariance);
					decimal roundedPriceVariance = decimal.Round(priceVariance, toleranceNumDecimalPlaces);

					return roundedPriceVariance <= this.LowerTolerance;
				}
				else
				{
					return false;
				}
			}


			set
			{
				//do nothing - this is a derived value. Set is just here for the grid.
			}
		}


		/// <summary>
		/// Flag to indicate whether the price is within the upper tolerance or not.
		/// </summary>
		public bool WithinPriceUpperTolerance
		{
			get
			{
				if (this.PriceSet)
				{
					decimal priceVariance = Math.Abs(this.PriceMovementVariance);
					return priceVariance <= this.UpperTolerance;
				}
				else
				{
					return false;
				}
			}


			set
			{
				//do nothing - this is a derived value. Set is just here for the grid.
			}
		}

		/// <summary>
		/// Flag to indicate whether the price is within the upper tolerance or not.
		/// </summary>
		public bool WithinRoundedPriceUpperTolerance
		{
			get
			{
				if (this.PriceSet)
				{
					decimal priceVariance = Math.Abs(this.PriceMovementVariance);
					decimal roundedPriceVariance = decimal.Round(priceVariance, toleranceNumDecimalPlaces);

					return roundedPriceVariance <= this.UpperTolerance;
				}
				else
				{
					return false;
				}
			}


			set
			{
				//do nothing - its a derived value. Set left in for the grid.
			}
		}

		/// <summary>
		/// Flag to indicate whether the price movement is within the allowed direction or not.
		/// </summary>
		public bool WithinPriceDirectionTolerance
		{
			get
			{
				// Check the direction of the price movement.
				if (this.PriceSet)
				{
					return !this.PriceIncreaseOnly || (this.Price >= this.PreviousPrice);
				}
				else
				{
					return false;
				}
			}


			set
			{
				//do nothing - its a derived value. Set left in for the grid.				
			}
		}

		/// <summary>
		/// Will be set to true if the current status is to be progressed to the next level or reverted
		/// to the previous level.
		/// </summary>
		public bool ProgressStatus
		{
			get { return this.m_progressStatus; }

			set
			{
				this.m_progressStatus = value;

				// This property will only be set as part of one of the progression of fund status functions,
				// in which updates to other properties are not done unless the status is being changed.
				// Because of this the IsDirty flag should only be set on if the ProgressStatus value has
				// changed from its original setting.
				this.IsDirty = !(this.ProgressStatus == this.InitialProgressStatus);
			}
		}

		/// <summary>
		/// This property holds the ProgressStatus value at the time the fund is created.  This is used
		/// to prevent attempting to perform an status progress operation if the ProgressStatus field has
		/// been returned to what it was originally.
		/// </summary>
		public bool InitialProgressStatus
		{
			get { return this.m_initialProgressStatus; }

			set
			{
				this.m_initialProgressStatus = value;

				// The ProgressStatus property is only set as part of one of the progression of fund status
				// functions, in which updates to other properties are not done unless the status is being
				// changed.  Because of this the IsDirty flag should only be set on if the ProgressStatus
				// value has changed from its original setting.
				this.IsDirty = !(this.ProgressStatus == this.InitialProgressStatus);
			}
		}

		/// <summary>
		/// The calculated price movement (difference between current price and previous previous) expressed
		/// as a percentage.
		/// </summary>
		public decimal PriceMovementPercent
		{
			get
			{
				decimal result = 0;

				if (this.PriceSet && this.PreviousPriceSet && this.PreviousPrice != 0)
				{
					result = ((this.Price - this.PreviousPrice)/this.PreviousPrice);
				}

				return result;
			}
		}


		/// <summary>
		/// The calculated predicted price movement (difference between current predicted price and 
		/// previous price) expressed as a percentage.
		/// </summary>
		public decimal PredictedPriceMovementPercent
		{
			get
			{
				decimal result = 0;

				if (this.PreviousPriceSet && this.PreviousPrice != 0)
				{
					//decimal predPrice = Decimal.Round(this.PredictedPrice, 4);
					decimal predPrice = this.PredictedPrice;
					if (predPrice == 0M)
					{
						return 0M;
					}
					else
					{
						result = ((predPrice - this.PreviousPrice)/this.PreviousPrice);
					}
				}

				return Decimal.Round(result, 6);
			}
		}

		/// <summary>
		/// The difference between the actual price movement and the predicted price movement.
		/// </summary>
		public decimal PriceMovementVariance
		{
			get
			{
				decimal result = 0;

				if (this.PriceSet && this.PreviousPriceSet
					&& this.PreviousPrice != 0)
				{
					result = this.PriceMovementPercent - this.PredictedPriceMovementPercent;
				}

				return result;
			}
		}

		/// <summary>
		/// The ID of the asset fund that this fund belongs to, retrieved from the parent asset fund
		/// object.
		/// </summary>
		public string AssetFundID
		{
			get { return m_parentAssetFundID; }
		}

		/// <summary>
		/// The name of the asset fund that this fund belongs to, retrieved from the parent asset fund
		/// object.
		/// </summary>
		public string AssetFundName
		{
			get
			{
				if (this.ParentAssetFund != null)
				{
					return this.ParentAssetFund.FullName;
				}

				return string.Empty;
			}
		}

		/// <summary>
		/// The unit price for the asset fund, retrieved from the parent asset fund object.
		/// </summary>
		public decimal AssetUnitPrice
		{
			get
			{
				if (this.ParentAssetFund != null)
				{
					return this.ParentAssetFund.UnitPrice;
				}

				return 0;
			}
		}

		/// <summary>
		/// Flag indicating whether the asset unit price is a valid value or not.
		/// </summary>
		public bool AssetUnitPriceSet
		{
			get { return (this.ParentAssetFund != null && this.ParentAssetFund.UnitPriceSet); }
		}

		/// <summary>
		/// The asset movement for the asset fund, retrieved from the parent asset fund object.
		/// </summary>
		public decimal AssetMovement
		{
			get
			{
				if (this.ParentAssetFund != null)
				{
					return this.ParentAssetFund.UnitPriceMovement;
				}

				return 0;
			}
		}

		/// <summary>
		/// Flag indicating whether the asset movement has been calculated and so holds a valid value or
		/// not.
		/// </summary>
		public bool AssetMovementSet
		{
			get { return (this.ParentAssetFund != null && this.ParentAssetFund.UnitPriceMovementSet); }
		}

		/// <summary>
		/// The asset fund predicted price.
		/// </summary>
		public decimal PredictedAssetUnitPrice
		{
			get
			{
				if (this.ParentAssetFund != null)
				{
					return this.ParentAssetFund.PredictedUnitPrice;
				}

				return 0;
			}
		}


		/// <summary>
		/// The predicted asset movement for the asset fund, retrieved from the parent asset fund object.
		/// </summary>
		public decimal PredictedAssetMovement
		{
			get
			{
				if (this.ParentAssetFund != null)
				{
					return this.ParentAssetFund.PredictedAssetMovement;
				}

				return 0;
			}
		}

		/// <summary>
		/// Flag indicating whether the predicted asset movement has been set because the default may
		/// be a valid value.
		/// </summary>
		public bool PredictedAssetMovementSet
		{
			get { return (this.ParentAssetFund != null && this.ParentAssetFund.PredictedAssetMovementSet); }
		}

		/// <summary>
		/// The variance in the asset fund unit price movement, retrieved from the parent asset fund
		/// object.
		/// </summary>
		public decimal AssetMovementVariance
		{
			get
			{
				if (this.ParentAssetFund != null)
				{
					return this.ParentAssetFund.AssetMovementVariance;
				}

				return 0;
			}
		}

		/// <summary>
		/// Flag indicating whether the asset movement value is valid or not.
		/// </summary>
		public bool AssetMovementVarianceSet
		{
			get { return (this.ParentAssetFund != null && this.ParentAssetFund.AssetMovementVarianceSet); }
		}


		/// <summary>
		/// THe ID of the funds parent asset fund
		/// </summary>
		public string ParentAssetFundID
		{
			set { this.m_parentAssetFundID = value; }

			get { return this.m_parentAssetFundID; }
		}

		/// <summary>
		/// Derived flag indicating within rounded tolerance
		/// </summary>
		public bool PriceMovementRoundedTolerance
		{
			get { return this.WithinRoundedPriceLowerTolerance && this.WithinRoundedPriceUpperTolerance && this.WithinPriceDirectionTolerance; }
		}

		/// <summary>
		/// The security code needed for import to link fund to hiPort3
		/// </summary>
		public string SecurityCode
		{
			get { return this.m_securityCode; }
			set
			{
				this.m_securityCode = value;
				this.setDirtyFlag();
			}
		}

		#endregion Fund properties

		#region Methods

		/// <summary>
		/// Override the Equals as we are only worried if the indexname is the same and not the actual objects
		/// </summary>
		/// <param name="obj"></param>
		/// <returns>true is they have the same index name</returns>
		public override bool Equals(object obj)
		{
			if (obj is FundGroup)
			{
				Fund fund = obj as Fund;
				return (this == fund);
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
		public static bool operator ==(Fund lhs, Fund rhs)
		{
			if ((object) lhs != null && (object) rhs != null)
			{
				return (lhs.HiPortfolioCode == rhs.HiPortfolioCode);
			}
			else
			{
				return (object) lhs == (object) rhs;
			}
		}

		/// <summary>
		/// Overloaded inequality operator
		/// </summary>
		/// <param name="lhs">First Object to compare</param>
		/// <param name="rhs">Second Object to compare</param>
		/// <returns></returns>
		public static bool operator !=(Fund lhs, Fund rhs)
		{
			return !(lhs == rhs);
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
		/// Set the modified flag to be true.
		/// </summary>
		protected void setDirtyFlag()
		{
			//
			// TODO: Possibly check for initial loading and populating code here
			//
			this.m_isDirty = true;
		}

		#endregion

		#region XFactor Properties

		/// <summary>
		/// The X factor to use for this fund in price prediction calculations.
		/// </summary>
		public decimal XFactor
		{
			get { return this.m_factors.XFctr.RatioValue; }

			set
			{
				//during grid update, the XFactorDisplay property is always set before XFactor.
				//therefore examine this flag here we know whether to ignore this value as it may be old.
				//Reset the flag if it is set as it has been utilised

				this.m_factors.XFctr.RatioValue = value;
				this.setDirtyFlag();
			}
		}

		/// <summary>
		/// The X factor to use for this fund in price prediction calculations.
		/// </summary>
		public decimal XFactorPercent
		{
			get { return this.m_factors.XFctr.PercentValue; }

			set
			{
				//during grid update, the XFactorDisplay property is always set before XFactor.
				//therefore examine this flag here we know whether to ignore this value as it may be old.
				//Reset the flag if it is set as it has been utilised

				this.m_factors.XFctr.PercentValue = value;
				this.setDirtyFlag();
			}
		}

		/// <summary>
		/// The ID for the X factor used in the price calculation.
		/// </summary>
		public int XFactorID
		{
			get { return this.m_factors.XFctr.FactorID; }


			set { this.m_factors.XFctr.FactorID = value; }
		}

		/// <summary>
		/// returns whether or not current value is valid
		/// </summary>
		/// <returns></returns>
		public bool XFactorValid()
		{
			return this.m_factors.XFctr.IsValid();
		}

		/// <summary>
		/// Flag to indicate whether the value is a valid value or not.
		/// </summary>
		public bool XFactorIDSet
		{
			get { return this.m_factors.XFctr.FactorIDSet; }
		}

		/// <summary>
		/// The narrative text describing why the xvalue percentage is applied
		/// </summary>
		public string XFactorDescription
		{
			get { return this.m_factors.XFctr.Description; }

			set
			{
				this.m_factors.XFctr.Description = value;
				this.setDirtyFlag();
			}

		}

		#endregion

		/// <summary>
		/// Gets the Bench Mark movement.
		/// </summary>
		/// <value></value>
		public decimal Movement
		{
			get { return this.PriceMovementPercent; }
		}

		/// <summary>
		/// Gets the Bench Mark currency.
		/// </summary>
		/// <value></value>
		/// <summary>
		/// Gets the Bench Mark currency.
		/// </summary>
		/// <value></value>
		public Currency.Currency Currency
		{
			get { return m_Currency; }
			set { m_Currency = value; }
		}


		/// <summary>
		/// Gets the state of availability for the Bench Mark.
		/// </summary>
		/// <value></value>
		public virtual BenchMarkAvailabilityState Availability
		{
			get
			{
				BenchMarkAvailabilityState returnValue = BenchMarkAvailabilityState.Unavailable;
				if (IsBenchMarkable && this.FundStatus >= FundStatusType.Released)
				{
					if (Currency != null && (this.Currency.CurrentRate == 0m || this.Currency.PreviousRate == 0m))
					{
						returnValue = BenchMarkAvailabilityState.AvailableWithWarnings;
					}
					else
					{
						returnValue = BenchMarkAvailabilityState.Available;
					}
				}

				return returnValue;

			}
		}

		/// <summary>
		/// Gets the bench mark type.
		/// </summary>
		/// <value></value>
		public virtual string BenchMarkType
		{
			get { return FundType; }
		}

		/// <summary>
		/// Gets the bench mark sub type.
		/// </summary>
		/// <value></value>
		public string BenchMarkSubType
		{
			get { return this.CompanyCode; }
		}

		/// <summary>
		/// Gets the previous benchmark value.
		/// </summary>
		/// <value></value>
		public decimal PreviousBenchmarkValue
		{
			get { return PreviousPrice; }
		}

		/// <summary>
		/// Gets the current benchmark value.
		/// </summary>
		/// <value></value>
		public decimal CurrentBenchmarkValue
		{
			get { return Price; }
		}

	}


}