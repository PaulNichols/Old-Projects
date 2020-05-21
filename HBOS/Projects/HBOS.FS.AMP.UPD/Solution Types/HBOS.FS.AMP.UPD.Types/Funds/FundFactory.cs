using System;
using System.Collections;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.Factors;

namespace HBOS.FS.AMP.UPD.Types.Funds
{
    /// <summary>
    /// Create a fund based on the specified fund type.
    /// 
    /// Factory Definition:
    /// Define an interface for creating an object, but let subclasses 
    /// decide which class to instantiate. Factory Method lets a class 
    /// defer instantiation to subclasses. 
    /// </summary>
    public class FundFactory
    {
		/// <summary>
		/// This class has been introduced during refactoring to group parameters 
		/// together used for constructing fund objects
		/// </summary>
		public  class FundParameters
		{

			/// <summary>
			/// Creates a new <see cref="FundParameters"/> instance.
			/// </summary>
			public FundParameters()
			{
				
			}

			/// <summary>
			/// 
			/// </summary>
			public  FundType FundType;
			/// <summary>
			/// 
			/// </summary>
			public  string HiPortfolioCode="";
			/// <summary>
			/// 
			/// </summary>
			public  string FullName="";
			/// <summary>
			/// 
			/// </summary>
			public  string ShortName="";
			/// <summary>
			/// 
			/// </summary>
			public  string ClassOrSeriesCode="";
			/// <summary>
			/// 
			/// </summary>
            public  bool UseMidPriceAsBidPrice=false;
			/// <summary>
			/// 
			/// </summary>
			public  bool OnHiPortfolio3=false;
			/// <summary>
			/// 
			/// </summary>
			public  bool IsDualPrice=false;
			/// <summary>
			/// 
			/// </summary>
			public  AssetFund ParentAssetFund;
			/// <summary>
			/// 
			/// </summary>
			public  decimal AssetMovementTolerance=decimal.MinValue;
			/// <summary>
			/// 
			/// </summary>
			public  decimal UpperTolerance=decimal.MinValue;
			/// <summary>
			/// 
			/// </summary>
			public  decimal LowerTolerance=decimal.MinValue;
			/// <summary>
			/// 
			/// </summary>
			public  bool PriceIncreaseOnly=false;
			/// <summary>
			/// 
			/// </summary>
			public  long TolerancesID=long.MinValue;
			/// <summary>
			/// 
			/// </summary>
			public  decimal Price=decimal.MinValue;
			/// <summary>
			/// 
			/// </summary>
			public  bool PriceSet=false;
			/// <summary>
			/// 
			/// </summary>
			public  long ImportedFundPriceID=long.MinValue;
			/// <summary>
			/// 
			/// </summary>
			public  decimal ValuationBasisEffect=decimal.MinValue;
			/// <summary>
			/// 
			/// </summary>
			public  decimal PreviousPrice=decimal.MinValue;
			/// <summary>
			/// 
			/// </summary>
			public  bool PreviousPriceSet=false;
			/// <summary>
			/// 
			/// </summary>
            public  Fund.FundStatusType FundStatus;
			/// <summary>
			/// 
			/// </summary>
			public  DateTime StatusChangedTime=DateTime.MinValue;
			/// <summary>
			/// 
			/// </summary>
			public  bool StatusChangedTimeSet=false;
			/// <summary>
			/// 
			/// </summary>
			public  bool UsePredictedPrice=false;
			/// <summary>
			/// 
			/// </summary>
			public  byte[] AuthorisedPriceTimestamp;
			/// <summary>
			/// 
			/// </summary>
			public  byte[] Timestamp;
			/// <summary>
			/// 
			/// </summary>
			public  bool IsLife=false;
			/// <summary>
			/// 
			/// </summary>
			public  bool IsExDividend=false;
			/// <summary>
			/// 
			/// </summary>
			public  string ParentAssetFundID;
			/// <summary>
			/// 
			/// </summary>
			public  string CompanyCode;
			/// <summary>
			/// 
			/// </summary>
			public  string SecurityCode;
			/// <summary>
			/// 
			/// </summary>
			public  FactorCollection Factors=new FactorCollection();
			/// <summary>
			/// 
			/// </summary>
			public  bool IsBenchmarkable=false;
			/// <summary>
			/// 
			/// </summary>
			public DateTime RevaluationEffectiveDate=DateTime.MinValue ;
			/// <summary>
			/// 
			/// </summary>
			public DateTime RevaluationEndDate =DateTime.MinValue;
			/// <summary>
			/// 
			/// </summary>
			public DateTime TaxProvisionEffectiveDate=DateTime.MinValue ;
			/// <summary>
			/// 
			/// </summary>
			public DateTime ScalingFactorEffectiveDate=DateTime.MinValue ;
			/// <summary>
			/// 
			/// </summary>
			public DateTime XFactorEffectiveDate=DateTime.MinValue ;
			/// <summary>
			/// 
			/// </summary>
			public string XFactorNarrative;
			/// <summary>
			/// 
			/// </summary>
			public decimal XFactor=decimal.MinValue;
			/// <summary>
			/// 
			/// </summary>
			public decimal ScalingFactor=decimal.MinValue;
			/// <summary>
			/// 
			/// </summary>
			public decimal TaxProvisionEstimate=decimal.MinValue ;
			/// <summary>
			/// 
			/// </summary>
			public decimal RevaluationEffect=decimal.MinValue;
			/// <summary>
			/// 
			/// </summary>
			public int TaxProvisionFactorID ;
			/// <summary>
			/// 
			/// </summary>
			public int ScalingFactorID ;
			/// <summary>
			/// 
			/// </summary>
			public int XFactorID;
			/// <summary>
			/// 
			/// </summary>
			public int RevaluationFactorID ;
			/// <summary>
			/// 
			/// </summary>
			public byte[] RevaluationTimestamp ;
			/// <summary>
			/// 
			/// </summary>
			public byte[] ScalingFactorTimestamp ;
			/// <summary>
			/// 
			/// </summary>
			public byte[] TaxProvisionTimestamp ;
			/// <summary>
			/// 
			/// </summary>
			public byte[] XFactorTimestamp ;
			/// <summary>
			/// 
			/// </summary>
			public DateTime CurrentValuationDate=DateTime.MinValue;

		}

		#region Fund type

		/// <summary>
		/// The type of fund to be created.
		/// </summary>
		public enum FundType : int
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
		/// <param name="fund"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentException">Invalid asset fund type</exception>
		public static FundType ResolveFundType (Fund fund)
		{
			OEICFund oeic = null;
			LinkedFund linked = null;
			Composite comp = null;
			return ResolveFundType(fund, out oeic, out linked, out comp);
		}

		/// <summary>
		/// Encapsulates the type checking of the abstract base class.
		/// Throws exception if not of any type. 
		/// Returns two null references and the reference to the specific object type, as well as the return enum type
		/// </summary>
		/// <param name="fund"></param>
		/// <param name="oeic"></param>
		/// <param name="linked"></param>
		/// <param name="comp"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentException">Invalid fund type</exception>
		public static FundType ResolveFundType (Fund fund, out OEICFund oeic, out LinkedFund linked, out Composite comp)
		{
			//this section is to save casting multiple times later
			oeic = fund as OEICFund;
			comp = null;
			linked = null;

			FundType retType;

			if (oeic == null)
			{
				linked = fund as LinkedFund;
				if (linked == null)
				{
					comp = fund as Composite;
					if (comp == null)
					{
						throw new ArgumentException ("fund not of a valid fund type");
					}
					else
					{
						retType = FundType.Composite;
					}
				}
				else
				{
					retType = FundType.Linked;
				}
			}
			else
			{
				retType = FundType.Oeic;
			}
			return retType;
		}

		#endregion

		#region Fund creation

		/// <summary>
		/// Creates a new fund depending on type
		/// </summary>
		/// <param name="fundParameters">Object containing a necessary fund details</param>
		/// <returns>Fund</returns>
		public static Fund CreateFund (FundParameters fundParameters)
		{

			switch (fundParameters.FundType)
			{
				case FundType.Composite:
					return new Composite(fundParameters);
				case FundType.Linked:
					return new LinkedFund(fundParameters);
				case FundType.Oeic:
					return new OEICFund(fundParameters);
				default:
					throw new ApplicationException ("unknown fund type");
			}
		}

		/// <summary>
		/// Creates a new fund depending on type
		/// </summary>
		/// <param name="fundType">Type of fund to create</param>
		/// <returns>Fund</returns>
		public static Fund CreateFund (FundType fundType)
		{
			FundParameters fundParameters =new FundParameters();
			fundParameters.FundType=fundType;
			return CreateFund(fundParameters);
		}


		#endregion

		#region Factor creation

		/// <summary>
		/// Creates new factors collection depending on type
		/// </summary>
		/// <param name="fundParameters"></param>
		/// <param name="holidays"></param>
		/// <returns>Factors Collection</returns>
		public static FactorCollection CreateFundFactors(FundParameters fundParameters,Hashtable holidays)
		{
			FactorCollection factors = new FactorCollection();

			factors.Add (CreateTaxProvisionEstimate(fundParameters.TaxProvisionEstimate, fundParameters.TaxProvisionFactorID, fundParameters.TaxProvisionEffectiveDate, fundParameters.TaxProvisionTimestamp));
			factors.Add (CreateRevaluationFactor(fundParameters.RevaluationEffect, fundParameters.RevaluationFactorID, fundParameters.RevaluationEffectiveDate, fundParameters.RevaluationEndDate, fundParameters.RevaluationTimestamp,fundParameters.CurrentValuationDate));
			factors.Add (CreateScalingFactor(fundParameters.ScalingFactor, fundParameters.ScalingFactorID, fundParameters.ScalingFactorEffectiveDate, fundParameters.ScalingFactorTimestamp)); 
			factors.Add (CreateXFactor (fundParameters.XFactor, fundParameters.XFactorID, fundParameters.XFactorEffectiveDate, fundParameters.XFactorNarrative, fundParameters.XFactorTimestamp));
			factors.Add (new ValuationBasis (fundParameters.ValuationBasisEffect)); 
			factors.Holidays = holidays;

			return factors;		
		}

		#endregion

		#region External system ids

		/// <summary>
		/// Creates a rehydrated <see cref="ExternalSystemID"/> entity
		/// </summary>
		/// <param name="hiPortfolioCode">Hi portfolio code.</param>
		/// <param name="systemID">System ID.</param>
		/// <param name="systemName">Name of the system.</param>
		/// <param name="fundCode">Fund code.</param>
		/// <param name="timeStamp">Time stamp.</param>
		/// <returns>The rehydrated ExternalSystemID</returns>
		public static ExternalSystemID CreateSystem(string hiPortfolioCode, int systemID, 
			string systemName, string fundCode, byte[] timeStamp)
		{
			return new ExternalSystemID(hiPortfolioCode, systemID, systemName, fundCode, timeStamp);
		}

		/// <summary>
		/// Creates a new <see cref="ExternalSystemID"/> entity
		/// </summary>
		/// <returns>A new External System identifier</returns>
		public static ExternalSystemID CreateSystem()
		{
			return new ExternalSystemID();
		}
		
		#endregion

		#region Private methods

		/// <summary>
		/// Creates a tax provision estimate
		/// </summary>
		/// <param name="taxProvision"></param>
		/// <param name="taxProvisionEstimateID"></param>
		/// <param name="taxProvisionEffectiveDate"></param>
		/// <param name="taxProvisionEstimateTimestamp"></param>
		/// <returns></returns>
		private static TaxProvisionEstimate CreateTaxProvisionEstimate (decimal taxProvision, int taxProvisionEstimateID,  
			DateTime taxProvisionEffectiveDate, byte[] taxProvisionEstimateTimestamp)
		{
			return new TaxProvisionEstimate (taxProvision, taxProvisionEstimateID, taxProvisionEffectiveDate, taxProvisionEstimateTimestamp);
		}

		/// <summary>
		/// Creates a revaluation factor
		/// </summary>
		/// <param name="revaluationFactor"></param>
		/// <param name="revaluationFactorID"></param>
		/// <param name="revaluationEffectiveDate"></param>
		/// <param name="revaluationEndDate"></param>
		/// <param name="revaluationFactorTimestamp"></param>
		/// <param name="currentValuationDate"></param>
		/// <returns></returns>
		private static RevaluationFactor CreateRevaluationFactor (decimal revaluationFactor, int revaluationFactorID, DateTime revaluationEffectiveDate, 			
			DateTime revaluationEndDate, byte[] revaluationFactorTimestamp,DateTime currentValuationDate)
		{
			return new RevaluationFactor (revaluationFactor, revaluationFactorID, revaluationEffectiveDate, revaluationEndDate, revaluationFactorTimestamp,currentValuationDate);
		}

		/// <summary>
		/// Creates a scaling factor
		/// </summary>
		/// <param name="scalingFactor"></param>
		/// <param name="scalingFactorID"></param>
		/// <param name="scalingFactorEffectiveDate"></param>
		/// <param name="scalingFactorTimestamp"></param>
		/// <returns></returns>
		private static ScalingFactor CreateScalingFactor(decimal scalingFactor, int scalingFactorID, DateTime scalingFactorEffectiveDate, byte[] scalingFactorTimestamp)
		{
			return new ScalingFactor (scalingFactor, scalingFactorID, scalingFactorEffectiveDate, scalingFactorTimestamp);
		}
 
		/// <summary>
		/// Creates an xFactor
		/// </summary>
		/// <param name="xFactor"></param>
		/// <param name="xFactorID"></param>
		/// <param name="xFactorEffectiveDate"></param>
		/// <param name="xFactorDescription"></param>
		/// <param name="xFactorTimestamp"></param>
		/// <returns></returns>
		private static XFactor CreateXFactor (decimal xFactor, int xFactorID, DateTime xFactorEffectiveDate, string xFactorDescription, byte[] xFactorTimestamp)
		{
			return new XFactor (xFactor, xFactorID, xFactorEffectiveDate, xFactorDescription, xFactorTimestamp);
		}

		#endregion

    }
}
