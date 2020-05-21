using HBOS.FS.AMP.UPD.Types.Currency;
using NUnit.Framework;

namespace UT_HBOS.FS.AMP.UPD.Types.AssetFunds
{
	/// <summary>
	/// Unit tests for currency entities
	/// </summary>
	[TestFixture]
	public class CurrencyUnitTest
	{
		/// <summary>
		/// Default constructor for NUnit
		/// </summary>
		public CurrencyUnitTest()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// Test the Currency Code Property
		/// </summary>
		[Test]
		public void CurrencyCodeProperty()
		{
			string currencyCode = "CurrencyCode";
			Currency currency = new Currency(currencyCode);
			Assert.AreEqual(currencyCode, currency.CurrencyCode);
		}
	}
}