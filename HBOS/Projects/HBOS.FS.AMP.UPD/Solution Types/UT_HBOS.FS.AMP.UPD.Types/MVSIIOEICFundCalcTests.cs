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
	public class MVSIIOEICFundCalcTests
	{
		private Fund m_fund;
		private AssetFund m_assetFund;
		private WeightedMovementCollection m_weightedMovements;

		/// <summary>
		/// Creates a new <see cref="MVSIIOEICFundCalcTests"/> instance.
		/// Default constructor required by NUnit
		/// </summary>
		public MVSIIOEICFundCalcTests()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// Initialises the OEIC fund weighted movements.
		/// </summary>
		/// <returns></returns>
		public WeightedMovementCollection InitialiseOEICWeightedMovements()
		{
			WeightedMovementCollection movements = new WeightedMovementCollection();

			for (int i = 0; i < 1; i++)
			{
				IWeightedMovement newMovement = MVSIICalcHelpers.CreateWeightedIndex(0.01m, 0.03m, 1.0m);
				movements.Add(newMovement);
			}

			return movements;

		}

		/// <summary>
		/// Initialises the OEIC asset fund.
		/// </summary>
		/// <returns></returns>
		public void InitialiseOEICAssetFund()
		{
			// Create the OEIC asset fund
			m_assetFund = MVSIICalcHelpers.CreateOEICAssetFund(500.0m, 400.0m, 0.8m);

			// Initialise some weighted movements
			m_weightedMovements = this.InitialiseOEICWeightedMovements();
			m_assetFund.WeightedMovements = m_weightedMovements;
		}

		/// <summary>
		/// Initialises the OEIC fund.
		/// </summary>
		[SetUp]
		public void InitialiseOEICFund()
		{
			// Initialise the OEIC asset fund 
			InitialiseOEICAssetFund();

			m_fund = MVSIICalcHelpers.CreateOEICFund(110.0m, 100.0m, m_assetFund);

		}

		/// <summary>
		/// Tests the OEIC fund
		/// </summary>
		[Test]
		public void OEICFundTest()
		{
			Console.WriteLine(m_assetFund.WeightedMovements.Count);
			Console.WriteLine(m_assetFund.PredictedAssetMovementSet);
			Console.WriteLine(m_assetFund.PredictedAssetMovementDisplay);
			Console.WriteLine(m_fund.PredictedPriceDisplay);
		}
	}
}