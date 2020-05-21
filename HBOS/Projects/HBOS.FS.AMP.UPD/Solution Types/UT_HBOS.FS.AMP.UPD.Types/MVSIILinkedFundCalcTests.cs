using System;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.Funds;
using NUnit.Framework;

namespace UT_HBOS.FS.AMP.UPD.Types
{
	/// <summary>
	/// Unit tests for MVSII calculations
	/// </summary>
	[TestFixture]
	public class MVSIILinkedFundCalcTests
	{
		private Fund m_fund;
		private AssetFund m_assetFund;
		private WeightedMovementCollection m_weightedMovements;
		private decimal m_expectedAssetMovement;

		/// <summary>
		/// Creates a new <see cref="MVSIILinkedFundCalcTests"/> instance.
		/// Default constructor required by NUnit
		/// </summary>
		public MVSIILinkedFundCalcTests()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// Initialises the linked weighted movements.
		/// </summary>
		/// <returns></returns>
		public WeightedMovementCollection InitialiseLinkedWeightedMovements()
		{
			m_expectedAssetMovement = 0.0m;

			WeightedMovementCollection movements = new WeightedMovementCollection();

			// 50% of 1% - 3% = 50% of -2% = -1%
			IWeightedMovement newMovement1 = MVSIICalcHelpers.CreateWeightedIndex(0.01m, 0.03m, 0.5m);
			movements.Add(newMovement1);
			m_expectedAssetMovement += -0.01m;

			// 50% of 1% - (-3)% = 50% of 4% = +2%
			IWeightedMovement newMovement2 = MVSIICalcHelpers.CreateWeightedIndex(0.01m, -0.03m, 0.5m);
			movements.Add(newMovement2);
			m_expectedAssetMovement += 0.02m;

			return movements;

		}

		/// <summary>
		/// Initialises the linked asset fund.
		/// </summary>
		/// <returns></returns>
		public void InitialiseLinkedAssetFund()
		{
			// Create the linked asset fund
			m_assetFund = MVSIICalcHelpers.CreateLinkedAssetFund(500.0m, 400.0m, 0.8m);

			// Initialise some weighted movements
			m_weightedMovements = this.InitialiseLinkedWeightedMovements();
			m_assetFund.WeightedMovements = m_weightedMovements;
		}

		/// <summary>
		/// Initialises the linked fund.
		/// </summary>
		[SetUp]
		public void InitialiseLinkedFund()
		{
			// Initialise the linked asset fund 
			InitialiseLinkedAssetFund();

			m_fund = MVSIICalcHelpers.CreateLinkedFund(110.0m, 100.0m, m_assetFund);

		}

		/// <summary>
		/// Linkeds the fund test.
		/// </summary>
		[Test]
		public void LinkedFundTest()
		{
			Console.WriteLine(m_assetFund.WeightedMovements.Count);
			Console.WriteLine(m_assetFund.PredictedAssetMovementSet);
			Console.WriteLine(m_assetFund.PredictedAssetMovementDisplay);
			Console.WriteLine(m_fund.PredictedPriceDisplay);

			Assert.AreEqual(m_expectedAssetMovement, m_assetFund.PredictedAssetMovement);
		}
	}
}