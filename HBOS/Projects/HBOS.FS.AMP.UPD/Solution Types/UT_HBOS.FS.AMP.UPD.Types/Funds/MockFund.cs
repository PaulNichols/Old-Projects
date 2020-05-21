using System;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.Factors;
using HBOS.FS.AMP.UPD.Types.FundGroups;
using HBOS.FS.AMP.UPD.Types.Funds;

namespace UT_HBOS.FS.AMP.UPD.Types.Funds
{
	/// <summary>
	/// Summary description for MockFund.
	/// </summary>
	// TODO: Change to static class in VS2005
	internal abstract class MockFund
	{
		internal static string expectedHiPortfolioCode = "Hi 3 Code";
		internal static string expectedFullName = "Full Name";
		internal static string expectedShortName = "Short Name";
		internal static string expectedClassOrSeriesCode = "ISIN Code";
		internal static bool expectedUseMidPriceAsBidPrice = true;
		internal static bool expectedOnHiPortfolio3 = true;
		internal static bool expectedPriceType = false;
		//todo - cannot instantiate abstract class - revisit this and make for oeic linked or composite AssetFundFactory.CreateAssetFund();

		internal static AssetFund expectedParentAssetFund = new AssetFund(
			AssetFund.AssetFundTypeEnum.Composite,
			"1",
			"shortname",
			"fullname",
			"companycode",
			1m,
			null,
			null
			);
		internal static bool expectedFundGroupIDSet = true;
		internal static decimal expectedTaxProvision = 100.1m;
		internal static bool expectedTaxProvisionAllowed = true;
		internal static int expectedTaxProvisionFactorID = 200;
		internal static bool expectedTaxProvisionFactorIDSet = true;
		internal static decimal expectedRevaluationRatioValue = 200.2m;
		internal static DateTime expectedRevaluationEffectiveDate = new DateTime(2005, 3, 5);
		internal static bool expectedRevaluationEffectiveDateSet = true;
		internal static DateTime expectedRevaluationEndDate = DateTime.MinValue;
		internal static bool expectedRevaluationEndDateSet = true;
		internal static int expectedRevaluationFactorID = 400;
		internal static bool expectedRevaluationFactorIDSet = true;
        internal static bool expectedIsBenchmarkable = true;
		internal static decimal expectedPreviousPriceCurrentBasis = 300.3m;
		internal static bool expectedPreviousPriceCurrentBasisSet = true;
		internal static decimal expectedScalingFactor = 400.4m;
		internal static int expectedScalingFactorID = 500;
		internal static bool expectedScalingfactorIDSet = true;
		internal static decimal expectedXFactor = 500.5m;
		internal static int expectedXFactorID = 600;
		internal static bool expectedXFactorIDSet = true;
		internal static decimal expectedAssetMovementTolerance = 600.6m;
		internal static decimal expectedUpperTolerance = 700.7m;
		internal static decimal expectedLowerTolerance = 800.8m;
		internal static bool expectedPriceIncreaseOnly = true;
		internal static int expectedTolerancesID = 600;
		internal static bool expectedTolerancesIDSet = true;
		internal static decimal expectedPrice = 900.9m;
		internal static bool expectedPriceSet = true;
		internal static long expectedImportedFundPriceID = 700;
		internal static bool expectedImportedFundPriceIDSet = true;
		internal static decimal expectedValuationBasisEffect = 0.05m;
		internal static bool expectedValuationBasisEffectSet = true;
		internal static int expectedValuationBasisID = 800;
		internal static bool expectedValuationBasisIDSet = true;
		internal static decimal expectedPreviousPrice = 1000.01m;
		internal static bool expectedPreviousPriceSet = true;
		internal static Fund.FundStatusType expectedFundStatus = Fund.FundStatusType.FirstLevelAuthorised;
		internal static DateTime expectedStatusChangedTime = new DateTime(2005, 12, 31, 1, 1, 1);
		internal static bool expectedStatusChangedTimeSet = true;
		internal static bool expectedUsePredictedPrice = true;
		internal static byte[] expectedAuthorisedPriceTimestamp = new byte[] {1, 2, 3};
		internal static byte[] expectedTimeStamp = new byte[] {4, 5, 6};
		internal static bool expectedIsExDividend = true;
		internal static byte[] expectedOeicTimestamp = new byte[] {7, 8, 9};
		internal static byte[] expectedRevaluationFactorTs = new byte[] {4, 5, 6};
		internal static byte[] expectedScalingFactorTs = new byte[] {4, 5, 6};
		internal static byte[] expectedTaxProvisionFactorTs = new byte[] {4, 5, 6};
		internal static byte[] expectedXFactorTs = new byte[] {4, 5, 6};
		internal static string expectedParentAssetFundID = "EQAME  ";
		internal static bool expectedIsLife=true;
		internal static string expectedCompanyCode="CMIG";
		internal static string expectedSecurityCode="testcode";

		internal static FactorCollection expectedFactorsCollection()
		{
			if (fc==null)
			{
				fc=new FactorCollection();
				Factor[] f =new Factor[4];
				//		XFactor
				f[0]=new XFactor(0.1m,1,DateTime.Today,"description",new byte[0]);
		
				//		RevaluationFactor
				f[1]=new RevaluationFactor(0.1m,1,DateTime.Today,DateTime.Today,new byte[0]);
				//		TaxProvisionEstimate or
				f[2]=new TaxProvisionEstimate(0.1m,1,DateTime.Today,new byte[0]);
				//		ScalingFactor
				f[3]=new ScalingFactor(0.1m,1,DateTime.Today,new byte[0]);
				fc.AddRange(f);
			}
			return fc;
		}

		private static FactorCollection fc=null;
		
		internal static FundGroupCollection expectedFundGroups()
		{
			if (fgc==null)
			{
				fgc=new HBOS.FS.AMP.UPD.Types.FundGroups.FundGroupCollection();
				fgc.Add( new HBOS.FS.AMP.UPD.Types.FundGroups.AssetFundGroup(0,"CMIG","fullname","shortname",new byte[0],true,true,true,3) );	
			}
			return fgc;
		}
		private static HBOS.FS.AMP.UPD.Types.FundGroups.FundGroupCollection fgc=null;

		internal static LinkedFund CreateInitialisedFund(FactorCollection fc)
		{
			
			LinkedFund lf= (LinkedFund) FundFactory.CreateFund(
				FundFactory.FundType.Linked,
				expectedHiPortfolioCode,                     
				expectedFullName,                    
				expectedShortName,                   
				expectedClassOrSeriesCode,             
				expectedUseMidPriceAsBidPrice,  			       
				expectedOnHiPortfolio3,
				expectedPriceType,       
				expectedParentAssetFund,             
				expectedAssetMovementTolerance,      
				expectedUpperTolerance,              
				expectedLowerTolerance,              
				expectedPriceIncreaseOnly,           
				expectedTolerancesID,                
				expectedPrice,                       
				expectedPriceSet,                    
				expectedImportedFundPriceID,         
				expectedValuationBasisEffect,
				expectedPreviousPrice,               
				expectedPreviousPriceSet,            
				expectedFundStatus,                  
				expectedStatusChangedTime,           
				expectedStatusChangedTimeSet,        
				expectedUsePredictedPrice,           
				expectedAuthorisedPriceTimestamp,    
				expectedTimeStamp,
				expectedIsLife,
				expectedIsExDividend,
				expectedParentAssetFundID,
				expectedCompanyCode,
				expectedSecurityCode,
				fc,
                expectedIsBenchmarkable
				);      
            
			lf.ParentFundGroups=expectedFundGroups();
			return lf;
		}
	}
}