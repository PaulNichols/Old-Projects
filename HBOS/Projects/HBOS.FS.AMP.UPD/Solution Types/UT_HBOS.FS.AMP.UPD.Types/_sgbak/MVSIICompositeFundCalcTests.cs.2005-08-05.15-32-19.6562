using System;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.Factors;
using HBOS.FS.AMP.UPD.Types.Funds;
using NUnit.Framework;

namespace UT_HBOS.FS.AMP.UPD.Types
{
	/// <summary>
	/// Unit tests for MVSII calculations
	/// </summary>
	[TestFixture]
	public class MVSIICompositeFundCalcTests
	{
		private Fund m_fund;
		private AssetFund m_assetFund;
		private WeightedMovementCollection m_weightedMovements;
		private decimal m_expectedAssetMovement;

		/// <summary>
		/// Creates a new <see cref="MVSIICompositeFundCalcTests"/> instance.
		/// Default constructor required by NUnit
		/// </summary>
		public MVSIICompositeFundCalcTests()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// Initialises the composite weighted movements.
		/// </summary>
		/// <returns></returns>
		public WeightedMovementCollection InitialiseCompositeWeightedMovements()
		{
			WeightedMovementCollection movements = new WeightedMovementCollection();
			m_expectedAssetMovement = 0.0m;

			// 50% of +1 % movement
			IWeightedMovement newMovement1 = MVSIICalcHelpers.CreateCompositeWeighting(0.01m, 0.5m);
			movements.Add(newMovement1);
			m_expectedAssetMovement += 0.005m;

			// 50% of -3 % movement
			IWeightedMovement newMovement2 = MVSIICalcHelpers.CreateCompositeWeighting(-0.03m, 0.5m);
			movements.Add(newMovement2);
			m_expectedAssetMovement += -0.015m;

			return movements;

		}

		/// <summary>
		/// Initialises the Composite asset fund.
		/// </summary>
		/// <returns></returns>
		public void InitialiseCompositeAssetFund()
		{
			// Create the Composite asset fund
			m_assetFund = MVSIICalcHelpers.CreateCompositeAssetFund(500.0m, 400.0m, 0.8m);

			// Initialise some weighted movements
			m_weightedMovements = this.InitialiseCompositeWeightedMovements();
			m_assetFund.WeightedMovements = m_weightedMovements;
		}

		/// <summary>
		/// Initialises the composite fund.
		/// </summary>
		[SetUp]
		public void InitialiseCompositeFund()
		{
			// Initialise the composite asset fund 
			InitialiseCompositeAssetFund();

			m_fund = MVSIICalcHelpers.CreateCompositeFund(110.0m, 100.0m, m_assetFund);

		}

		/// <summary>
		/// Tests the composite fund
		/// </summary>
		[Test]
		public void CompositeFundTest()
		{
			Console.WriteLine(m_assetFund.WeightedMovements.Count);
			Console.WriteLine(m_assetFund.PredictedAssetMovementSet);
			Console.WriteLine(m_assetFund.PredictedAssetMovementDisplay);
			Console.WriteLine(m_fund.PredictedPriceDisplay);

			Assert.AreEqual(m_expectedAssetMovement, m_assetFund.PredictedAssetMovement);

			Console.WriteLine(m_fund.PredictedPriceDisplay);

			Assert.AreEqual(m_expectedAssetMovement, m_assetFund.PredictedAssetMovement);

		}

		/// <summary>
		/// Tests XFactor
		/// </summary>
		[Test]
		public void TestXFactor()
		{
			decimal expectedXFactor = 0.02m;
			decimal initialXFactor;

			// Store the initial value
			initialXFactor = m_fund.XFactor;

			m_fund.XFactor = expectedXFactor;
			Assert.AreEqual(expectedXFactor, m_fund.XFactor);

			m_fund.XFactor = initialXFactor;
		}

		/// <summary>
		/// Tests Scaling Factor
		/// </summary>
		[Test]
		public void TestScalingFactor()
		{
			decimal expectedScalingFactor = 0.02m;
			decimal initialScalingFactor;

			if (m_fund is Composite)
			{
				Composite compositeFund = (Composite) m_fund;

				// Store the initial value
				initialScalingFactor = compositeFund.ScaleFactor;

				compositeFund.ScaleFactor = expectedScalingFactor;
				Assert.AreEqual(expectedScalingFactor, compositeFund.ScaleFactor);

				compositeFund.ScaleFactor = initialScalingFactor;
			}

		}

		/// <summary>
		/// Tests TPE Factor
		/// </summary>
		[Test]
		public void TestTPEFactor()
		{
			decimal expectedTaxProvisionEstimate = 0.02m;
			decimal initialTaxProvisionEstimate;

			if (m_fund is Composite)
			{
				Composite compositeFund = (Composite) m_fund;

				// Store the initial value
				initialTaxProvisionEstimate = compositeFund.TPE;

				compositeFund.TPE = expectedTaxProvisionEstimate;
				Assert.AreEqual(expectedTaxProvisionEstimate, compositeFund.TPE);

				compositeFund.TPE = initialTaxProvisionEstimate;
			}

		}

		/// <summary>
		/// Tests Revaluation Factor
		/// </summary>
		[Test]
		public void TestRevaluationFactor()
		{
			decimal expectedRevaluationChange = 0.02m;
			DateTime expectedRevaluationEndDate = DateTime.Today.AddDays(3);
			DateTime expectedRevaluationEffectiveDate = DateTime.Today.AddDays(-1);

			decimal initialRevaluationChange;
			DateTime initialRevaluationEndDate;
			DateTime initialRevaluationEffectiveDate;

			if (m_fund is Composite)
			{
				Composite compositeFund = (Composite) m_fund;

				// Store the initial values
				initialRevaluationChange = compositeFund.RevalFactor; //compositeFund.RevaluationChange;
				initialRevaluationEndDate = compositeFund.RevaluationEndDate;
				initialRevaluationEffectiveDate = compositeFund.RevaluationEffectiveDate;

				// Set the expected parameters
				compositeFund.RevalFactor = expectedRevaluationChange;
				compositeFund.RevaluationEndDate = expectedRevaluationEndDate;
				compositeFund.RevaluationEffectiveDate = expectedRevaluationEffectiveDate;
				compositeFund.RevalFactor = 7.5m;

				Console.WriteLine("Revaluation factor = " + compositeFund.RevalFactor);

				// Check they are as expected
				Assert.AreEqual(expectedRevaluationChange, compositeFund.RevalFactor);
				Assert.AreEqual(expectedRevaluationEndDate, compositeFund.RevaluationEndDate);
				Assert.AreEqual(expectedRevaluationEffectiveDate, compositeFund.RevaluationEffectiveDate);

				Console.WriteLine("Revaluation Change = " + expectedRevaluationChange);
				Console.WriteLine("Revaluation Effective Date = " + expectedRevaluationEffectiveDate);
				Console.WriteLine("Revaluation End Date = " + expectedRevaluationEndDate);
				Console.WriteLine("Now = " + DateTime.Today);

				for (int i = 0; i < compositeFund.Factors.Count; i++)
				{
					Factor currentFactor = compositeFund.Factors[i];
					Console.WriteLine("Factor " + i + ", type " + currentFactor.GetType() + ", effect = " + currentFactor.CalculateEffect());
				}

			}

		}

	}
}