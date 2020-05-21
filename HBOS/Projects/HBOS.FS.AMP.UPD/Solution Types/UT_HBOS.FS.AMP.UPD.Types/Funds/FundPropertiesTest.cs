using HBOS.FS.AMP.UPD.Types.Funds;
using NUnit.Framework;

namespace UT_HBOS.FS.AMP.UPD.Types.Funds
{
	/// <summary>
	/// Base class used for unit testing of properties in the Fund entity
	/// </summary>
	public abstract class FundPropertiesTestBase : PropertyTestBase
	{
		/// <summary>
		/// Test the value of the HiPortfolioCode property
		/// </summary>
		[Test]
		public void HiPortfolioCode()
		{
			AssertPropertyValue("HiPortfolioCode", MockFund.expectedHiPortfolioCode);
		}

		/// <summary>
		/// Test the value of the FullName property
		/// </summary>
		[Test]
		public void FullName()
		{
			AssertPropertyValue("FullName", MockFund.expectedFullName);
		}

		/// <summary>
		/// Test the value of the ShortName property
		/// </summary>
		[Test]
		public void ShortName()
		{
			AssertPropertyValue("ShortName", MockFund.expectedShortName);
		}

        /// <summary>
        /// test the value of the Isbenchmarkable Flag
        /// </summary>
        public void IsBenchMarkable()
        {
            AssertPropertyValue("IsBenchMarkable", true);
        }


		/// <summary>
		/// Test the value of the ClassOrSeriesCode property
		/// </summary>
		[Test]
		public void ClassOrSeriesCode()
		{
			AssertPropertyValue("ClassOrSeriesCode", MockFund.expectedClassOrSeriesCode);
		}

		/// <summary>
		/// Test the value of the UseMidPriceAsBidPrice property
		/// </summary>
		[Test]
		public void UseMidPriceAsBidPrice()
		{
			AssertPropertyValue("UseMidPriceAsBidPrice", MockFund.expectedUseMidPriceAsBidPrice);
		}

		/// <summary>
		/// Test the value of the OnHiPortfolio3 property
		/// </summary>
		[Test]
		public void OnHiPortfolio3()
		{
			AssertPropertyValue("OnHiPortfolio3", MockFund.expectedOnHiPortfolio3);
		}

//		/// <summary>
//		/// Test the value of the PriceType property
//		/// </summary>
//		[Test]
//		public void IsDual()
//		{
//			AssertPropertyValue("IsDualPrice", MockFund.expectedPriceType);
//		}

		/// <summary>
		/// Test the value of the ParentAssetFund property
		/// </summary>
		[Test]
		public void ParentAssetFund()
		{
			AssertPropertyValue("ParentAssetFund", MockFund.expectedParentAssetFund);
		}

		/// <summary>
		/// Test the FundGroups collection property
		/// </summary>
		[Test]
		public void FundGroups()
		{
			AssertPropertyValue("ParentFundGroups", MockFund.expectedFundGroups());
		}

		
		/// <summary>
		/// Test the value of the AssetMovementTolerance property
		/// </summary>
		[Test]
		public void AssetMovementTolerance()
		{
			AssertPropertyValue("AssetMovementTolerance", MockFund.expectedAssetMovementTolerance);
		}

		/// <summary>
		/// Test the value of the UpperTolerance property
		/// </summary>
		[Test]
		public void UpperTolerance()
		{
			AssertPropertyValue("UpperTolerance", MockFund.expectedUpperTolerance);
		}

		/// <summary>
		/// Test the value of the LowerTolerance property
		/// </summary>
		[Test]
		public void LowerTolerance()
		{
			AssertPropertyValue("LowerTolerance", MockFund.expectedLowerTolerance);
		}

		/// <summary>
		/// Test the value of the PriceIncreaseOnly property
		/// </summary>
		[Test]
		public void PriceIncreaseOnly()
		{
			AssertPropertyValue("PriceIncreaseOnly", MockFund.expectedPriceIncreaseOnly);
		}

		/// <summary>
		/// Test the value of the TolerancesID property
		/// </summary>
		[Test]
		public void TolerancesID()
		{
			AssertPropertyValue("TolerancesID", MockFund.expectedTolerancesID);
		}

//		/// <summary>
//		/// Test the value of the TolerancesIDSet property
//		/// </summary>
//		[Test]
//		public void TolerancesIDSet()
//		{
//			AssertPropertyValue("TolerancesIDSet", MockFund.expectedTolerancesIDSet);
//		}

		/// <summary>
		/// Test the value of the Price property
		/// </summary>
		[Test]
		public void Price()
		{
			AssertPropertyValue("Price", MockFund.expectedPrice);
		}

		/// <summary>
		/// Test the value of the PriceSet property
		/// </summary>
		[Test]
		public void PriceSet()
		{
			AssertPropertyValue("PriceSet", MockFund.expectedPriceSet);
		}

		/// <summary>
		/// Test the value of the ImportedFundPriceID property
		/// </summary>
		[Test]
		public void ImportedFundPriceID()
		{
			AssertPropertyValue("ImportedFundPriceID", MockFund.expectedImportedFundPriceID);
		}

		///// <summary>
//		/// Test the value of the ImportedFundPriceIDSet property
//		/// </summary>
//		[Test]
//		public void ImportedFundPriceIDSet()
//		{
//			AssertPropertyValue("ImportedFundPriceIDSet", MockFund.expectedImportedFundPriceIDSet);
//		}

//		/// <summary>
//		/// Test the value of the ValuationBasisID property
//		/// </summary>
//		[Test]
//		public void ValuationBasisID()
//		{
//			AssertPropertyValue("ValuationBasisID", MockFund.expectedValuationBasisID);
//		}

//		/// <summary>
//		/// Test the value of the ValuationBasisIDSet property
//		/// </summary>
//		[Test]
//		public void ValuationBasisIDSet()
//		{
//			AssertPropertyValue("ValuationBasisIDSet", MockFund.expectedValuationBasisIDSet);
//		}

		/// <summary>
		/// Test the value of the PreviousPrice property
		/// </summary>
		[Test]
		public void PreviousPrice()
		{
			AssertPropertyValue("PreviousPrice", MockFund.expectedPreviousPrice);
		}

		/// <summary>
		/// Test the value of the PreviousPriceSet property
		/// </summary>
		[Test]
		public void PreviousPriceSet()
		{
			AssertPropertyValue("PreviousPriceSet", MockFund.expectedPreviousPriceSet);
		}

		/// <summary>
		/// Test the value of the FundStatus property
		/// </summary>
		[Test]
		public void FundStatus()
		{
			AssertPropertyValue("FundStatus", MockFund.expectedFundStatus);
		}

		/// <summary>
		/// Test the value of the StatusChangedTime property
		/// </summary>
		[Test]
		public void StatusChangedTime()
		{
			AssertPropertyValue("StatusChangedTime", MockFund.expectedStatusChangedTime);
		}

		/// <summary>
		/// Test the value of the StatusChangedTimeSet property
		/// </summary>
		[Test]
		public void StatusChangedTimeSet()
		{
			AssertPropertyValue("StatusChangedTimeSet", MockFund.expectedStatusChangedTimeSet);
		}

		/// <summary>
		/// Test the value of the UsePredictedPrice property
		/// </summary>
		[Test]
		public void UsePredictedPrice()
		{
			AssertPropertyValue("UsePredictedPrice", MockFund.expectedUsePredictedPrice);
		}

		/// <summary>
		/// Test the value of the AuthorisedPriceTimestamp property
		/// </summary>
		[Test]
		public void AuthorisedPriceTimestamp()
		{
			AssertPropertyValue("AuthorisedPriceTimestamp", MockFund.expectedAuthorisedPriceTimestamp);
		}

		/// <summary>
		/// Test the value of the TimeStamp property
		/// </summary>
		[Test]
		public void TimeStamp()
		{
			AssertPropertyValue("TimeStamp", MockFund.expectedTimeStamp);
		}
	}

	/// <summary>
	/// Units tests for funds created through the default parameterless constructor
	/// </summary>
	[TestFixture]
	public class DefaultFundPropertiesTest : FundPropertiesTestBase
	{
		/// <summary>
		/// Default constructor required by NUnit
		/// </summary>
		public DefaultFundPropertiesTest()
		{
		}

		/// <summary>
		/// Creates the subject of the unit tests
		/// </summary>
		/// <returns>A new uninitialised fund</returns>
		protected override object CreateSubject()
		{
			return new HBOS.FS.AMP.UPD.Types.Funds.LinkedFund();
		}

		/// <summary>
		/// Tells the base class the constructor type used
		/// </summary>
		protected override EntityConstructorType constructorType
		{
			get { return EntityConstructorType.Default; }
		}

		/// <summary>
		/// Test that PredictedAssetMovement is zero when Asset is unassigned
		/// </summary>
		[Test]
		public void PredictedAssetMovement()
		{
			Fund fund = (Fund) subject;
			Assert.AreEqual(0, fund.PredictedAssetMovement);
		}

		/// <summary>
		/// Test that AssetMovement is zero when Asset is unassigned
		/// </summary>
		[Test]
		public void AssetMovement()
		{
			Fund fund = (Fund) subject;
			Assert.AreEqual(0, fund.AssetMovement);
		}

		/// <summary>
		/// Test that AssetUnitPrice is zero when Asset is unassigned
		/// </summary>
		[Test]
		public void AssetUnitPrice()
		{
			Fund fund = (Fund) subject;
			Assert.AreEqual(0, fund.AssetUnitPrice);
		}

	}

	/// <summary>
	/// Units tests for funds created through the parametered constructor used for database population
	/// </summary>
	[TestFixture]
	public class InitialisedFundPropertiesTest : FundPropertiesTestBase
	{
		/// <summary>
		/// Default constructor required by NUnit
		/// </summary>
		public InitialisedFundPropertiesTest()
		{
		}

		/// <summary>
		/// Creates the subject of the unit tests
		/// </summary>
		/// <returns>A new initialised fund</returns>
		protected override object CreateSubject()
		{
			return MockFund.CreateInitialisedFund(MockFund.expectedFactorsCollection());
		}

		/// <summary>
		/// Tells the base class the constructor type used
		/// </summary>
		protected override EntityConstructorType constructorType
		{
			get { return EntityConstructorType.Initialised; }
		}

		/// <summary>
		/// Test the value of the Factors property
		/// </summary>
		[Test]
		public void Factors()
		{
			AssertPropertyValue("Factors", MockFund.expectedFactorsCollection());
		}

		/// <summary>
		/// Test that PredictedAssetMovement is equal to that of the assigned Asset
		/// </summary>
		[Test]
		public void PredictedAssetMovement()
		{
			Fund fund = (Fund) subject;
			decimal expectedMovement = 33.56m;
			fund.ParentAssetFund.PredictedAssetMovement = expectedMovement;
			Assert.AreEqual(expectedMovement, fund.PredictedAssetMovement);
		}

		/// <summary>
		/// Test that AssetMovement is equal to that of the assigned Asset
		/// </summary>
		[Test]
		public void AssetMovement()
		{
			Fund fund = (Fund) subject;
			decimal expectedAssetMovement = 11.99m;
			fund.ParentAssetFund.UnitPriceMovement = expectedAssetMovement;
			Assert.AreEqual(expectedAssetMovement, fund.AssetMovement);
		}

		/// <summary>
		/// Test that AssetUnitPrice is equal to that of the assigned Asset
		/// </summary>
		[Test]
		public void AssetUnitPrice()
		{
			Fund fund = (Fund) subject;
			decimal expectedUnitPrice = 1.234m;
			fund.ParentAssetFund.UnitPrice = expectedUnitPrice;
			Assert.AreEqual(expectedUnitPrice, fund.AssetUnitPrice);
		}
	}
}