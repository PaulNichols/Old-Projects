using NUnit.Framework;

namespace UT_HBOS.FS.AMP.UPD.Types.Funds
{
	/// <summary>
	/// Unit tests for the Fund calculations
	/// </summary>
	[TestFixture]
	public class FundPredictionValidationTest : FundCalculationTestBase
	{
		/// <summary>
		/// NUnit insists on having a null constructor - do not place any logic in here!
		/// </summary>
		public FundPredictionValidationTest()
		{
		}

		/// <summary>
		/// Test that a new fund does not calculate.
		/// </summary>
		[Test]
		public void NewFundDoesNotCalculate()
		{
			makeFundCalculate();
			Assert.IsFalse(subject.PredictedPrice > 0);
		}

		/// <summary>
		/// Test that a fund without a previous price does not calculate.
		/// </summary>
		[Test]
		public void FundWithoutPreviousPriceDoesNotCalculate()
		{
			assignAssetFund();
			subject.PreviousPriceSet = false;
			makeFundCalculate();
			Assert.IsFalse(subject.PredictedPrice > 0);
		}


		/// <summary>
		/// Test that a fund without an asset fund does not calculate.
		/// </summary>
		[Test]
		public void FundWithoutAssetFundDoesNotCalculate()
		{
			subject.PreviousPrice = 1.23m;
			makeFundCalculate();
			Assert.IsFalse(subject.PredictedPrice > 0);
		}


		/// <summary>
		/// Test that a Fund with an asset fund and a previous price calculates.
		/// </summary>
		[Test]
		public void FundWithAssetFundAndPreviousPriceCalculates()
		{
			subject.PreviousPrice = 1.23m;
			assignAssetFund();
			makeFundCalculate();
			Assert.IsTrue(subject.PredictedPrice > 0);
		}

//		/// <summary>
//		/// Test that a fund without previous price current basis does not have a valuation factor.
//		/// </summary>
//		[Test]
//		public void NoValuationFactorWithoutPreviousPriceCurrentBasis()
//		{
//			subject.PreviousPrice = 0.1m;
//			assignAssetFund();
//			makeFundCalculate();
//			Assert.AreEqual(0m,subject.ValuationFactor);
//		}

//		/// <summary>
//		/// Test that a fund without a previous price does not have a valuation factor.
//		/// </summary>
//		[Test]
//		public void NoValuationFactorWithoutPreviousPrice()
//		{
//			subject.PreviousPriceCurrentBasisSet = true;
//			assignAssetFund();
//			makeFundCalculate();
//			Assert.AreEqual(0m,subject.ValuationFactor);
//		}


//		/// <summary>
//		/// Test the  Valuations factor calculation.
//		/// </summary>
//		[Test]
//		public void ValuationFactorCalculation()
//		{
//			subject.PreviousPriceCurrentBasisSet = true;
//			subject.PreviousPrice = 100m;
//			subject.PreviousPriceCurrentBasis = 110m;
//
//			assignAssetFund();
//			makeFundCalculate();
//			Assert.AreEqual(0.1m,subject.ValuationFactor);	
//		}

		/// <summary>
		/// Test the prediction calculation based on no factors (all set to zero).
		/// </summary>
		[Test]
		public void PredictionWithNoFactors()
		{
			subject.PreviousPrice = 100m;

			assignAssetFund();
			makeFundCalculate();
			Assert.AreEqual(100m, subject.PredictedPrice);
		}

//		/// <summary>
//		/// Test the prediction calculation based on the valuation factor.
//		/// </summary>
//		[Test]
//		public void PredictionBasedOnValuationFactor()
//		{
//			subject.PreviousPriceCurrentBasisSet = true;
//			subject.PreviousPrice = 100m;
//			subject.PreviousPriceCurrentBasis = 110m;
//
//			assignAssetFund();
//			makeFundCalculate();
//			Assert.AreEqual(110m,subject.PredictedPrice);	
//		}

		/// <summary>
		/// Test the prediction calculation based on the asset movement
		/// </summary>
		[Test]
		public void PredictionBasedOnAssetMovement()
		{
			subject.PreviousPrice = 100m;

			assignAssetFund();
			subject.ParentAssetFund.PredictedAssetMovement = 0.2m;
			makeFundCalculate();
			Assert.AreEqual(120m, subject.PredictedPrice);
		}

		/// <summary>
		/// Test the prediction calculation based on the scaling factor.
		/// </summary>
		[Test]
		public void PredictionBasedOnScalingFactor()
		{
			subject.PreviousPrice = 100m;
			subject.ScaleFactor = -0.1m;

			assignAssetFund();
			makeFundCalculate();
			Assert.AreEqual(90m, subject.PredictedPrice);
		}

		/// <summary>
		/// Test the prediction calculation based on the X factor.
		/// </summary>
		[Test]
		public void PredictionBasedOnXFactor()
		{
			subject.PreviousPrice = 100m;
			subject.XFactor = 0.15m;

			assignAssetFund();
			makeFundCalculate();
			Assert.AreEqual(115m, subject.PredictedPrice);
		}

		/// <summary>
		/// Test the prediction calculation based on all of the factors
		/// </summary>
		[Test]
		public void PredictionBasedOnAllFactors()
		{
			assignAssetFund();

			subject.PreviousPrice = 100m;
			subject.ParentAssetFund.PredictedAssetMovement = 0.25m;
			subject.ScaleFactor = -0.1m;
			subject.XFactor = 0.15m;
			//tpe=0.1

			// 25 - 10 + 15 - 5  = 25%

			makeFundCalculate();
			Assert.AreEqual(130m, subject.PredictedPrice);
		}
	}
}