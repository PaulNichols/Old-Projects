using System;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.Currency;
using HBOS.FS.AMP.UPD.Types.StockMarketIndex;
using NUnit.Framework;

namespace UT_HBOS.FS.AMP.UPD.Types.AssetFunds
{
	/// <summary>
	/// Base class for testing the property values of AssetFund
	/// </summary>
	public abstract class AssertFundTestBase : PropertyTestBase
	{

		/// <summary>
		/// Creates a new <see cref="AssertFundTestBase"/> instance.
		/// </summary>
		protected AssertFundTestBase()
		{
			StockMarketIndex smi = new StockMarketIndex(1, "market", 6, 5, expectedCurrency);
			AssetMovementConstituent item = new AssetMovementConstituent(1, smi);
			expectedAssetMovementConstitution = new AssetMovementConstituentCollection();
			expectedAssetMovementConstitution.Add(item);
		}

		/// <summary>
		/// Expected Asset Fund Code
		/// </summary>
		protected const string expectedAssetFundCode = "asset fund code";

		/// <summary>
		/// Expected Asset Movement Tolerance
		/// </summary>
		protected const decimal expectedAssetMovementTolerance = 0.5m;

		/// <summary>
		/// Expected Company Code
		/// </summary>
		protected const string expectedCompanyCode = "my comp";

		/// <summary>
		/// Expected Currency Rate Import ID
		/// </summary>
		protected const long expectedCurrencyRateSnapshotID = 10;

		/// <summary>
		/// Expected Full Name
		/// </summary>
		protected const string expectedFullName = "bob";

		/// <summary>
		/// Expected Index Value Import ID
		/// </summary>
		protected const long expectedIndexValueSnapshotID = 33;

		/// <summary>
		/// Expected Locked For Calculation
		/// </summary>
		protected const bool expectedLockedForCalculation = true;


		/// <summary>
		/// Expected Predicted Unit Price
		/// </summary>
		protected const decimal expectedPredictedUnitPrice = 9.9m;

		/// <summary>B
		/// Expected Previous Unit Price
		/// </summary>
		protected const decimal expectedPreviousUnitPrice = 8.8m;

		/// <summary>
		/// Expected Short Name
		/// </summary>
		protected const string expectedShortName = "ken";

		/// <summary>
		/// Expected TimeStamp
		/// </summary>
		protected readonly byte[] expectedTimeStamp = new byte[] {100, 200};

		/// <summary>
		/// Expected Unit Price
		/// </summary>
		protected const decimal expectedUnitPrice = 234.56m;

		/// <summary>
		/// Expected Unit Price Movement
		/// </summary>
		protected const decimal expectedUnitPriceMovement = 1.1m;

		/// <summary>
		/// Expected Valuation Point
		/// </summary>
		protected readonly DateTime expectedValuationPoint = new DateTime(1999, 11, 5, 1, 2, 3, 4);

		/// <summary>
		/// sets up an expected asset movement constitute.
		/// </summary>
		/// <returns></returns>
		protected readonly AssetMovementConstituentCollection expectedAssetMovementConstitution;

		/// <summary>
		/// Expected Currency
		/// </summary>
		protected readonly Currency expectedCurrency = new Currency("GBP", 10, 9);


		/// <summary>
		/// Expected unit price set
		/// </summary>
		protected const bool expectedUnitPriceSet = true;

		/// <summary>
		/// Expected unit price movement set
		/// </summary>
		protected const bool expectedUnitPriceMovementSet = false;
		
		/// <summary>
		///  Expected asset movement tolerence set
		/// </summary>
		protected const bool expectedAssetMovementToleranceSet = false;
		
		/// <summary>
		/// Expected Valuation Point Set
		/// </summary>
		protected const bool expectedValuationPointSet = true;
		
		/// <summary>
		/// Expected Previous Unit Price Set
		/// </summary>
		protected const bool expectedPreviousUnitPriceSet = true;

		/// <summary>
		/// Expected Authorised Unit Price
		/// </summary>
		protected const decimal expectedAuthorisedUnitPrice = 38.291m;

		/// <summary>
		/// Expected Authorised Unit Price Set
		/// </summary>
		protected const bool expectedAuthorisedUnitPriceSet = false;

		/// <summary>
		/// Test the AssetFundCode.
		/// </summary>
		[Test]
		public void AssetFundCode()
		{
			AssertPropertyValue("AssetFundCode", expectedAssetFundCode);
		}

		/// <summary>
		/// Test the Asset Movement Tolerance
		/// </summary>
		[Test]
		public void AssetMovementTolerance()
		{
			AssertPropertyValue("AssetMovementTolerance", expectedAssetMovementTolerance);
		}

		/// <summary>
		/// Test the Company Code
		/// </summary>
		[Test]
		public void CompanyCode()
		{
			AssertPropertyValue("CompanyCode", expectedCompanyCode);
		}

		/// <summary>
		/// Test the Currency Rate Import ID
		/// </summary>
		[Test]
		public void CurrencyRateSnapshotID()
		{
			AssertPropertyValue("CurrencyRateSnapshotID", expectedCurrencyRateSnapshotID);
		}

		/// <summary>
		/// Test the Full Name
		/// </summary>
		[Test]
		public void FullName()
		{
			AssertPropertyValue("FullName", expectedFullName);
		}

		/// <summary>
		/// Test the Locked For Calculation flag
		/// </summary>
		[Test]
		public void LockedForCalculation()
		{
			AssertPropertyValue("LockedForCalculation", expectedLockedForCalculation);
		}

		/// <summary>
		/// Test the Market Index Movements
		/// </summary>
		[Test]
		public void Currency()
		{
			AssertPropertyValue("Currency", expectedCurrency);
		}

		/// <summary>
		/// Test the Market Value Splits
		/// </summary>
		[Test]
		public void AssetMovementConstituteCollection()
		{
			AssertPropertyValue("AssetMovementConstitution", expectedAssetMovementConstitution);
		}

		/// <summary>
		/// Test the Predicted Unit Price
		/// </summary>
		[Test]
		public void PredictedUnitPrice()
		{
			AssertPropertyValue("PredictedUnitPrice", expectedPredictedUnitPrice);
		}

		/// <summary>
		/// Test the Previous Unit Price
		/// </summary>
		[Test]
		public void PreviousUnitPrice()
		{
			AssertPropertyValue("PreviousUnitPrice", expectedPreviousUnitPrice);
		}

		/// <summary>
		/// Test the Short Name
		/// </summary>
		[Test]
		public void ShortName()
		{
			AssertPropertyValue("ShortName", expectedShortName);
		}

		/// <summary>
		/// Test the TimeStamp
		/// </summary>
		[Test]
		public void TimeStamp()
		{
			AssertPropertyValue("TimeStamp", expectedTimeStamp);
		}

		/// <summary>
		/// Test the Unit Price
		/// </summary>
		[Test]
		public void UnitPrice()
		{
			AssertPropertyValue("UnitPrice", expectedUnitPrice);
		}

		/// <summary>
		/// Test the Unit Price Movement
		/// </summary>
		[Test]
		public void UnitPriceMovement()
		{
			AssertPropertyValue("UnitPriceMovement", expectedUnitPriceMovement);
		}

		/// <summary>
		/// Test the Valuation Point
		/// </summary>
		[Test]
		public void ValuationPoint()
		{
			AssertPropertyValue("ValuationPoint", expectedValuationPoint);
		}

	}

	/// <summary>
	/// Class used to test the AssetFund entity when constructed with the default parameterless constructor
	/// </summary>
	[TestFixture]
	public class DefaultAssetFundTest : AssertFundTestBase
	{
		/// <summary>
		/// Default constructor for NUnit
		/// </summary>
		public DefaultAssetFundTest(): base()
		{
		}

		private const AssetFund.AssetFundTypeEnum expectedAssetFundType = AssetFund.AssetFundTypeEnum.Oeic;

		/// <summary>
		/// Creates an initialised AssetFund as the subject of the test
		/// </summary>
		/// <returns>The AssetFund under test</returns>
		protected override object CreateSubject()
		{
			return new AssetFund(expectedAssetFundType);
		}

		/// <summary>
		/// Tells the parent class the constructor type used
		/// </summary>
		protected override EntityConstructorType constructorType
		{
			get { return EntityConstructorType.Default; }
		}

		/// <summary>
		/// Assets the asset fund type.
		/// </summary>
		[Test]
		public void AssetFundType()
		{
			AssertPropertyValue("AssetFundType",expectedAssetFundType);
		}

	}


	/// <summary>
	/// Class used to test the AssetFund entity when constructed with the parametered constructor used from the database
	/// </summary>
	[TestFixture]
	public class InitialisedAssetFundTest : AssertFundTestBase
	{
		/// <summary>
		/// Default constructor for NUnit
		/// </summary>
		public InitialisedAssetFundTest(): base()
		{
		}

		/// <summary>
		/// Creates an initialised AssetFund as the subject of the test
		/// </summary>
		/// <returns>The AssetFund under test</returns>
		protected override object CreateSubject()
		{
			AssetFund aFund = new AssetFund(AssetFund.AssetFundTypeEnum.Oeic,
			                                                   expectedAssetFundCode,
			                                                   expectedShortName,
			                                                   expectedFullName,
			                                                   expectedCompanyCode,
			                                                   expectedUnitPrice,
															   expectedValuationPoint,
			                                                   expectedUnitPriceMovement,
			                                                   expectedPreviousUnitPrice,
			                                                   expectedPreviousUnitPriceSet,
			                                                   expectedAuthorisedUnitPrice,
				                                               expectedAuthorisedUnitPriceSet,
															   expectedLockedForCalculation,	
			                                                   expectedAssetMovementTolerance,
															   expectedUnitPriceSet,
															   expectedUnitPriceMovementSet,
															   expectedAssetMovementToleranceSet,
								                               expectedValuationPointSet,
			                                                   expectedTimeStamp,
			                                                   expectedAssetMovementConstitution,
			                                                   expectedCurrency); // fund status param is unused

			aFund.PredictedUnitPrice = expectedPredictedUnitPrice;
			return aFund;
		}

		/// <summary>
		/// Tells the parent class the constructor type used
		/// </summary>
		protected override EntityConstructorType constructorType
		{
			get { return EntityConstructorType.Initialised; }
		}
	}
}