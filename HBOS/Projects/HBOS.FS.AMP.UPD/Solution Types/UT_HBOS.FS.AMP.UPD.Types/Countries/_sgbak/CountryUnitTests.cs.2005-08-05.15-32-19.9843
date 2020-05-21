using HBOS.FS.AMP.UPD.Types.Countries;
using NUnit.Framework;

namespace UT_HBOS.FS.AMP.UPD.Types.Countries
{
	/// <summary>
	/// Base class for testing Country entity
	/// </summary>
	public abstract class CountryTestBase : PropertyTestBase
	{
		/// <summary>
		/// Expected CountryName value
		/// </summary>
		protected const string expectedCountryName = "Country Name";

		/// <summary>
		/// Expected CountryCode value
		/// </summary>
		protected const string expectedCountryCode = "Country Code";

		/// <summary>
		/// Test the CountryName property
		/// </summary>
		[Test]
		public void CountryName()
		{
			AssertPropertyValue("CountryName", expectedCountryName);
		}

		/// <summary>
		/// Test the CountryCode property
		/// </summary>
		[Test]
		public void InitialisationOfCountryCode()
		{
			AssertPropertyValue("CountryCode", expectedCountryCode);
		}
	}


	/// <summary>
	/// Unit tests for the Country entity created with the default parameterless constructor
	/// </summary>
	[TestFixture]
	public class DefaultCountryTest : CountryTestBase
	{
		/// <summary>
		/// Default constructor for NUnit
		/// </summary>
		public DefaultCountryTest()
		{
		}

		/// <summary>
		/// Creates the subject of the test.
		/// </summary>
		/// <returns>A new country</returns>
		protected override object CreateSubject()
		{
			return CountryFactory.CreateCountry();
		}

		/// <summary>
		/// Tells the base class the constructor type used to create the subject
		/// </summary>
		protected override EntityConstructorType constructorType
		{
			get { return EntityConstructorType.Default; }
		}

	}

	/// <summary>
	/// Unit tests for the Country entity created with the parametered constructor used for database population
	/// </summary>
	[TestFixture]
	public class InitialisedCountryTest : CountryTestBase
	{
		/// <summary>
		/// Default constructor for NUnit
		/// </summary>
		public InitialisedCountryTest()
		{
		}

		/// <summary>
		/// Creates the subject of the test.
		/// </summary>
		/// <returns>A new country</returns>
		protected override object CreateSubject()
		{
			return CountryFactory.CreateCountry(expectedCountryCode, expectedCountryName);
		}

		/// <summary>
		/// Tells the base class the constructor type used to create the subject
		/// </summary>
		protected override EntityConstructorType constructorType
		{
			get { return EntityConstructorType.Initialised; }
		}
	}
}