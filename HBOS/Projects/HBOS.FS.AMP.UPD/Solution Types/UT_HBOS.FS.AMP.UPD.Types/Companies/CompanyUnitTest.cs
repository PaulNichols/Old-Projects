using HBOS.FS.AMP.UPD.Types.Companies;
using NUnit.Framework;

namespace UT_HBOS.FS.AMP.UPD.Types.Companies
{
	/// <summary>
	/// Base class for the unit testing of the Company entity
	/// </summary>
	public abstract class CompanyTestBase : PropertyTestBase
	{
		/// <summary>
		/// Expected CompanyName value
		/// </summary>
		protected const string expectedCompanyName = "CoName";

		/// <summary>
		/// Expected CompanyCode value
		/// </summary>
		protected const string expectedCompanyCode = "CoCode";

		/// <summary>
		/// Expected TimeStamp value
		/// </summary>
		protected readonly byte[] expectedTimeStamp = {101};

		/// <summary>
		/// Test the CompanyName
		/// </summary>
		[Test]
		public void CompanyName()
		{
			AssertPropertyValue("CompanyName", expectedCompanyName);
		}

		/// <summary>
		/// Test the CompanyCode
		/// </summary>
		[Test]
		public void CompanyCode()
		{
			AssertPropertyValue("CompanyName", expectedCompanyName);
		}

		/// <summary>
		/// Test the TimeStamp
		/// </summary>
		[Test]
		public void TimeStamp()
		{
			AssertPropertyValue("TimeStamp", expectedTimeStamp);
		}
	}

	/// <summary>
	/// Class for unit testing the Company entity created with the default parameterless constuctor
	/// </summary>
	[TestFixture]
	public class DefaultCompanyTest : CompanyTestBase
	{
		/// <summary>
		/// Default constructor for NUnit
		/// </summary>
		public DefaultCompanyTest()
		{
		}

		/// <summary>
		/// Creates the subject of the unit test
		/// </summary>
		/// <returns>The company under test</returns>
		protected override object CreateSubject()
		{
			return CompanyFactory.CreateCompany();
		}

		/// <summary>
		/// Tell the parent class the constructor type used
		/// </summary>
		protected override EntityConstructorType constructorType
		{
			get { return EntityConstructorType.Default; }
		}

		/// <summary>
		/// Is the IEntity IsModified flag updated by a change to CompanyName.
		/// </summary>
		[Test]
		public void IEntityIsModifiedByChangeToCompanyName()
		{
			Company co = (Company) subject;
			co.CompanyName = "new company";
			UnitTestHelpers.ModifiedIEntityTest(co);
		}

	}

	/// <summary>
	/// Class for unit testing the Company entity created with the arametered constuctor populated by the database
	/// </summary>
	[TestFixture]
	public class InitialisedCompanyTest : CompanyTestBase
	{
		/// <summary>
		/// Default constructor used by NUnit
		/// </summary>
		public InitialisedCompanyTest()
		{
		}

		/// <summary>
		/// Creates the subject of the unit test
		/// </summary>
		/// <returns>The company under test</returns>
		protected override object CreateSubject()
		{
			return CompanyFactory.CreateCompany(expectedCompanyCode, expectedCompanyName, expectedTimeStamp);
		}

		/// <summary>
		/// Tell the parent class the constructor type used
		/// </summary>
		protected override EntityConstructorType constructorType
		{
			get { return EntityConstructorType.Initialised; }
		}

	}
}