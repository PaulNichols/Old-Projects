using NUnit.Framework;

namespace UT_HBOS.FS.AMP.UPD.Types
{
	/// <summary>
	/// Unit tests for WeightedIndices entity
	/// </summary>
	[TestFixture]
	public class WeightedIndicesTest
	{
		/// <summary>
		/// Default constructor requried by NUnit
		/// </summary>
		public WeightedIndicesTest()
		{
		}

		private AssetFundIndexWeighted subject;

		/// <summary>
		/// Set up run before each unit test. Creates the subject under test.
		/// </summary>
		[SetUp]
		public void SetUp()
		{
			subject = new AssetFundIndexWeighted();
		}

		/// <summary>
		/// Test the value of the StockMarketMovement property
		/// </summary>
		[Test]
		public void StockMarketMovement()
		{
			decimal stockMarketMovement = 0.15m;
			subject.MarketMovement = stockMarketMovement;
			Assert.AreEqual(stockMarketMovement, subject.MarketMovement);
		}

		/// <summary>
		/// Test the value of the ExchangeRateMovement property
		/// </summary>
		[Test]
		public void ExchangeRateMovement()
		{
			decimal exchangeRateMovement = 0.25m;
			subject.CurrencyMovement = exchangeRateMovement;
			Assert.AreEqual(exchangeRateMovement, subject.CurrencyMovement);
		}

		/// <summary>
		/// Test the value of the Proportion property
		/// </summary>
		[Test]
		public void Proportion()
		{
			decimal proportion = 0.05m;
			subject.Proportion = proportion;
			Assert.AreEqual(proportion, subject.Proportion);
		}

	}
}