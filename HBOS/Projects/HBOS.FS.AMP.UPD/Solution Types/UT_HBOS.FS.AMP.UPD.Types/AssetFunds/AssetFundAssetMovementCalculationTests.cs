using System;
using HBOS.FS.AMP.UPD.Types.Currency;
using NUnit.Framework;
using HBOS.FS.AMP.UPD.Types.AssetFunds;

namespace UT_HBOS.FS.AMP.UPD.Types.AssetFunds
{
	/// <summary>
	/// Unit tests for the asset movement of an asset fund
	/// </summary>
	[TestFixture]    
	public class AssetFundAssetMovementCalculationTests
	{
		/// <summary>
		/// Default constructor required by NUnit
		/// </summary>
		public AssetFundAssetMovementCalculationTests()
		{
		}

		/// <summary>
		/// Creates a single index with an asset movement of 10%
		/// </summary>
		/// <returns>A AssetMovementConstituentCollection containing a single index</returns>
		/// 
		public static AssetMovementConstituentCollection CreateSingleBenchMark()
		{
			// Create a weighted index collection
			AssetMovementConstituentCollection parts = new AssetMovementConstituentCollection();

			//currency movement of 0.2
			//market movement of 0.2
			HBOS.FS.AMP.UPD.Types.StockMarketIndex.StockMarketIndex benchMark=new HBOS.FS.AMP.UPD.Types.StockMarketIndex.StockMarketIndex(
				0,"test",6m,5m,new Currency("test",6m,5m));

			//proportion of 1
			AssetMovementConstituent constituent1 = new AssetMovementConstituent(1.0m,benchMark);
//			index1.MarketMovement = 0.05m;
//			index1.CurrencyMovement = -0.05m;
//			index1.Proportion = 1.0m;

			// Total effect on asset movement = 40%

			parts.Add(constituent1);
			return parts;
		}

		/// <summary>
		/// Creates a collections with multiple positive initialised indices with a combined asset movement of 4%
		/// </summary>
		/// <returns>A AssetMovementConstituentCollection containing two indicies</returns>
		public static AssetMovementConstituentCollection CreateMultiplePositivlyWeightedBenchMarks()
		{
			// Create a weighted index collection
			AssetMovementConstituentCollection parts = new AssetMovementConstituentCollection();

			decimal proportion=0.5m;

			HBOS.FS.AMP.UPD.Types.StockMarketIndex.StockMarketIndex benchMark1=new HBOS.FS.AMP.UPD.Types.StockMarketIndex.StockMarketIndex(
				0,"test",3m,2m,new Currency("test",6m,5m));
			AssetMovementConstituent constituent1 = new AssetMovementConstituent(proportion,benchMark1 );
			//index1.MarketMovement = 0.15m;
			//index1.CurrencyMovement = 0.05m;

			// Add another index and ensure the movement is correct    
			HBOS.FS.AMP.UPD.Types.StockMarketIndex.StockMarketIndex benchMark2=new HBOS.FS.AMP.UPD.Types.StockMarketIndex.StockMarketIndex(
				0,"test",6m,5m,new Currency("test",6m,5m));
			AssetMovementConstituent constituent2 = new AssetMovementConstituent(proportion,benchMark2);
			//index2.MarketMovement = 0.02m;
			//index2.CurrencyMovement = 0.04m;


			// Total effect on asset movement = 4%

			parts.Add(constituent1);
			parts.Add(constituent2);
			return parts;
		}

		/// <summary>
		/// Creates one positive and one negative initialised indices with a combined asset movement of -2%
		/// </summary>
		/// <returns>A AssetMovementConstituentCollection containing two indicies</returns>
		public static AssetMovementConstituentCollection CreateMultiplePositiveAndNegativeBenchmarks()
		{
			// Create a weighted index collection
			AssetMovementConstituentCollection parts = new AssetMovementConstituentCollection();

			decimal proportion=0.5m;

			HBOS.FS.AMP.UPD.Types.StockMarketIndex.StockMarketIndex benchMark1=new HBOS.FS.AMP.UPD.Types.StockMarketIndex.StockMarketIndex(
				0,"test",3m,6m,new Currency("test",2m,3m));
			AssetMovementConstituent constituent1 = new AssetMovementConstituent(proportion,benchMark1 );
			//index1.MarketMovement = 0.15m;
			//index1.CurrencyMovement = 0.05m;

			// Add another index and ensure the movement is correct    
			HBOS.FS.AMP.UPD.Types.StockMarketIndex.StockMarketIndex benchMark2=new HBOS.FS.AMP.UPD.Types.StockMarketIndex.StockMarketIndex(
				0,"test",6m,5m,new Currency("test",10m,8m));
			AssetMovementConstituent constituent2 = new AssetMovementConstituent(proportion,benchMark2);
			//index2.MarketMovement = 0.02m;
			//index2.CurrencyMovement = 0.04m;


			// Total effect on asset movement = 4%

			parts.Add(constituent1);
			parts.Add(constituent2);
			return parts;
		}

		/// <summary>
		/// Creates an initialised asset fund with index movements. Tolerance is defaulted to 0.05
		/// </summary>
		/// <param name="constituents">The indicies used to calculate prices</param>
		/// <param name="currency"></param>
		/// <returns>The created AssetFund</returns>
		public static AssetFund CreateAssetFundWithBenchmarks(AssetMovementConstituentCollection constituents,Currency currency)
		{
			return CreateAssetFundWithBenchmarks(constituents,0.05m,currency);
		}

		/// <summary>
		/// Creates an initialised asset fund with index movements
		/// </summary>
		/// <param name="constituents">The indicies used to calculate prices.</param>
		/// <param name="tolerance">The allowed tolerance for the fund.</param>
		/// <param name="currency"></param>
		/// <returns></returns>
		public static AssetFund CreateAssetFundWithBenchmarks(AssetMovementConstituentCollection constituents, decimal tolerance,Currency currency)
		{
			 
			AssetFund assetFund = new AssetFund(
				AssetFund.AssetFundTypeEnum.Oeic,
				"AssetFundCode",
				"ShortName",
				"FullName",
				"CompanyCode",
				100.0m, // Unit Price
				System.DateTime.Now, // Valuation Point
				0.15m, // Unit Price Movement (15%)
				115.0m, // Previous unit price
				true, //prev unit price set
				0M, //auth unit price
				false,//auth unit price set
				true,  //locked for calculation,
				tolerance, // AM Tolerance (5%, i.e. passed)
				true, // UnitPriceSet
				true, // UnitPriceMovementSet
				true, // AMTolerance set
				true, // Valuation point set
				new byte[0],//timestamp,
				constituents, //constituaent bench marks			
				currency);
		
			return assetFund; //assetFund;
		}


		/// <summary>
		/// Test that IEntityBase is initialised.
		/// </summary>
		[Test]
		public void IEntityBaseIsInitialised()
		{
			AssetFund assetFund = CreateAssetFundWithBenchmarks(CreateSingleBenchMark(),null);
			UnitTestHelpers.OverloadedConstructorIEntityTest(assetFund);
		}
		
		/// <summary>
		/// Test the predicted asset movement.
		/// </summary>
		[Test]
		public void PredictedAssetMovement()
		{
			// Check that the predicted asset movement, on the basis of 
			// a single (100%) market benchmark with a 0.7 currency movement and a market movement of 0.2
			AssetFund assetFund = CreateAssetFundWithBenchmarks(CreateSingleBenchMark(),new Currency("test",8m,2m));
			decimal expectedAssetMovement = 1.0m*(0.2m+0.7m);
			Assert.AreEqual(expectedAssetMovement, assetFund.PredictedAssetMovement);
		}

		/// <summary>
		/// Test the movement is Within the tolerance.
		/// </summary>
		[Test]
		public void WithinTolerance()
		{
			// Check that the within tolerance calculation is correct.
			// We know the tolerance has been set at 0.25m, and are predicting
			// 0.25m, so this test should be true
			AssetFund assetFund = CreateAssetFundWithBenchmarks(CreateSingleBenchMark(),0.25m,new Currency("test",3m,2m));
			bool isAssetMovementWithinTolerance = true;
			Assert.AreEqual(isAssetMovementWithinTolerance, assetFund.WithinAssetMovementTolerance);
		}

		/// <summary>
		/// Test the movement is outside the tolerance.
		/// </summary>
		[Test]
		public void OutsideTolerance()
		{
			// Check that the within tolerance calculation is correct.
			// We know the tolerance has been set at 0.01m, and are predicting
			// 0.75m, so this test should be false
			AssetFund assetFund = CreateAssetFundWithBenchmarks(CreateSingleBenchMark(),0.01m,new Currency("test",8m,2m));
			bool isAssetMovementWithinTolerance = false;
			Assert.AreEqual(isAssetMovementWithinTolerance, assetFund.WithinAssetMovementTolerance);
		}

		/// <summary>
		/// Test the movement variance.
		/// </summary>
		[Test]
		public void MovementVariance()
		{
			// We should also be able to test the variance - we gave a unit price movement 
			// of +15% and a predicted movement of +20%, so the variance should be -5%
			AssetFund assetFund = CreateAssetFundWithBenchmarks(CreateSingleBenchMark(),null);
			decimal assetMovementVariance = -0.05m;
			Assert.AreEqual(assetMovementVariance, assetFund.AssetMovementVariance);


			// If this test passes, let's make sure that the tolerance tests are correct by changing
			// the tolerance to be equal to the asset movement          
			assetFund.AssetMovementTolerance = assetFund.PredictedAssetMovement;
            bool isAssetMovementWithinTolerance = true;
            Assert.AreEqual(isAssetMovementWithinTolerance, assetFund.WithinAssetMovementTolerance);
		}

		/// <summary>
		/// Tests that multiple positive indices returns the correct movement
		/// </summary>
		[Test]
		public void TestMultiplePositiveIndices()
		{
			AssetFund assetFund = CreateAssetFundWithBenchmarks(CreateMultiplePositivlyWeightedBenchMarks(),new Currency("test",8m,5m));
			
			// Check that the predicted asset movement, on the basis of 
			// a (50%) index of 0.25 + -0.125 is  0.325
			// a (50%) index of 0.1 + -0.125 is -0.225 = 0.6 in total

			decimal expectedAssetMovement = 0.6m;
			Assert.AreEqual(expectedAssetMovement, assetFund.PredictedAssetMovement);
		}

		/// <summary>
		/// Tests that multiple positive and negative indices return the correct movement
		/// </summary>
		/// 
		[Test]
		public void TestMultiplePositiveAndNegativeIndices()
		{
			AssetFund assetFund = CreateAssetFundWithBenchmarks(CreateMultiplePositiveAndNegativeBenchmarks(),new Currency("test",2m, 3m));

			// Check that the predicted asset movement, on the basis of 
			// a (50%) index of -0.25 +  0.00 is -0.25
			// a (50%) index of  0.01 + -0.4375 is -0.3375 = -0.5875 in total
			decimal expectedAssetMovement = -0.5875m;
			Assert.AreEqual(expectedAssetMovement, assetFund.PredictedAssetMovement);
		}

		/// <summary>
		/// Test that an exception is thrown when proportion does not total 100 percent.
		/// </summary>
		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ExceptionWhenProportionDoesNotTotal100Percent()
		{
			AssetMovementConstituentCollection benchMark = CreateSingleBenchMark();
			benchMark[0].Proportion = 0.99m;
			AssetFund assetFund = CreateAssetFundWithBenchmarks(benchMark,null);
		}
	}
}
