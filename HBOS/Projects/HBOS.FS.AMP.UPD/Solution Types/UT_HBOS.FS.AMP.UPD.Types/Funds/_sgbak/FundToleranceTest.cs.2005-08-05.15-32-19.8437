using System.Text;
using NUnit.Framework;

namespace UT_HBOS.FS.AMP.UPD.Types.Funds
{
	/// <summary>
	/// Unit tests for Tolerance checks on funds
	/// </summary>
	[TestFixture]
	public class FundToleranceTest : FundCalculationTestBase
	{
		/// <summary>
		/// Default constructor required by NUnit
		/// </summary>
		public FundToleranceTest()
		{
		}

		/// <summary>
		/// Set up the fund under test and the default expected values
		/// </summary>
		[SetUp]
		public override void SetUp()
		{
			base.SetUp();

			subject.PriceSet = true;
			subject.PreviousPriceSet = true;
//			subject.PreviousPriceCurrentBasisSet = true;

			expectedWithinLower = false;
			expectedWithinUpper = false;
			expectedWithinDirection = false;
			expectedWithinLowerRounded = false;
			expectedWithinUpperRounded = false;
		}

		/// <summary>
		/// Test that all tolerance flags fail when price not set.
		/// </summary>
		[Test]
		public void AllFailWhenPriceNotSet()
		{
			subject.PriceSet = false;
			makeFundCalculate();
			assertTolerance();
		}

//		/// <summary>
//		/// Test that all tolerance flags fail when predicted price not set.
//		/// </summary>
//		[Test]
//		public void AllFailWhenPredictedPriceNotSet()
//		{
//			//subject.PredictedPrice = false;
//			makeFundCalculate();
//			assertTolerance();
//		}

		/// <summary>
		/// Test that all tolerance flags pass when Within the tolerance.
		/// </summary>
		[Test]
		public void WithinTolerance()
		{
			expectedWithinLower = true;
			expectedWithinUpper = true;
			expectedWithinDirection = true;
			expectedWithinLowerRounded = true;
			expectedWithinUpperRounded = true;

			setUpVariance(0.1m);
			subject.LowerTolerance = 0.15m;
			subject.UpperTolerance = 0.2m;

			assignAssetFund();
			makeFundCalculate();
			assertTolerance();
		}

		/// <summary>
		/// Test the tolerance flags when the variance is outside lowers tolerance.
		/// </summary>
		[Test]
		public void LowerToleranceFails()
		{
			expectedWithinLower = false;
			expectedWithinUpper = true;
			expectedWithinDirection = true;
			expectedWithinLowerRounded = false;
			expectedWithinUpperRounded = true;

			setUpVariance(0.175m);
			subject.LowerTolerance = 0.15m;
			subject.UpperTolerance = 0.2m;

			assignAssetFund();
			makeFundCalculate();
			assertTolerance();
		}

		/// <summary>
		/// Test the tolerance flags when the variance is outside upper tolerance.
		/// </summary>
		[Test]
		public void UpperToleranceFails()
		{
			expectedWithinLower = false;
			expectedWithinUpper = false;
			expectedWithinDirection = true;
			expectedWithinLowerRounded = false;
			expectedWithinUpperRounded = false;

			setUpVariance(0.201m);
			subject.LowerTolerance = 0.15m;
			subject.UpperTolerance = 0.2m;

			assignAssetFund();
			makeFundCalculate();
			assertTolerance();
		}

		/// <summary>
		/// Test the tolerance flags when the variance is negative
		/// </summary>
		[Test]
		public void WithinDirectionFails()
		{
			expectedWithinLower = true;
			expectedWithinUpper = true;
			expectedWithinDirection = false;
			expectedWithinLowerRounded = true;
			expectedWithinUpperRounded = true;

			setUpVariance(-0.001m);
			subject.LowerTolerance = 0.15m;
			subject.UpperTolerance = 0.2m;
			subject.PreviousPrice = 1.001m;

			assignAssetFund();
			makeFundCalculate();
			assertTolerance();
		}

		/// <summary>
		/// Test the tolerance flags when the variance is outside upper tolerance and negative.
		/// </summary>
		[Test]
		public void UpperToleranceFailsNegative()
		{
			expectedWithinLower = false;
			expectedWithinUpper = false;
			expectedWithinDirection = true;
			expectedWithinLowerRounded = false;
			expectedWithinUpperRounded = false;

			setUpVariance(-0.201m);
			subject.LowerTolerance = 0.15m;
			subject.UpperTolerance = 0.2m;

			assignAssetFund();
			makeFundCalculate();
			assertTolerance();
		}

		/// <summary>
		/// Test the tolerance flags when the variance is rounded.
		/// </summary>
		[Test]
		public void LowerRoundedToleranceFails()
		{
			expectedWithinLower = false;
			expectedWithinUpper = true;
			expectedWithinDirection = true;
			expectedWithinLowerRounded = false;
			expectedWithinUpperRounded = true;

			setUpVariance(0.00076m); // rounded to 0.0008
			subject.LowerTolerance = 0.00077m;
			subject.UpperTolerance = 0.2m;

			assignAssetFund();
			makeFundCalculate();
			assertTolerance();
		}

		#region PrivateMethods

		private bool expectedWithinLower;
		private bool expectedWithinUpper;
		private bool expectedWithinDirection;
		private bool expectedWithinLowerRounded;
		private bool expectedWithinUpperRounded;

		private void assertTolerance()
		{
			StringBuilder failures = new StringBuilder();

			if (subject.WithinPriceLowerTolerance != expectedWithinLower)
				failures.AppendFormat("WithinPriceLowerTolerance {0}\n", expectedBooleanFailureMessage(expectedWithinLower));

			if (subject.WithinPriceUpperTolerance != expectedWithinUpper)
				failures.AppendFormat("WithinPriceUpperTolerance {0}\n", expectedBooleanFailureMessage(expectedWithinLower));

			if (subject.WithinPriceDirectionTolerance != expectedWithinDirection)
				failures.AppendFormat("WithinPriceDirectionTolerance {0}\n", expectedBooleanFailureMessage(expectedWithinDirection));

			if (subject.WithinRoundedPriceLowerTolerance != expectedWithinLowerRounded)
				failures.AppendFormat("WithinRoundedPriceLowerTolerance {0}\n", expectedBooleanFailureMessage(expectedWithinLowerRounded));

			if (subject.WithinRoundedPriceUpperTolerance != expectedWithinUpperRounded)
				failures.AppendFormat("WithinRoundedPriceUpperTolerance {0}\n", expectedBooleanFailureMessage(expectedWithinUpperRounded));

			if (failures.Length > 0)
			{
				failures.Insert(0, "The following tolerance flags failed:\n");
				Assert.Fail(failures.ToString());
			}
		}

		private string expectedBooleanFailureMessage(bool expected)
		{
			if (expected)
				return "Expected TRUE but was FALSE";
			else
				return "Expected FALSE but was TRUE";
		}

		private void setUpVariance(decimal requiredVariance)
		{
			subject.Price = 1;
			subject.PreviousPrice = 1;
			subject.PredictedPrice = 1;
//			subject.PreviousPriceCurrentBasis = 1;
			subject.XFactor = requiredVariance;
		}

		#endregion PrivateMethods
	}
}