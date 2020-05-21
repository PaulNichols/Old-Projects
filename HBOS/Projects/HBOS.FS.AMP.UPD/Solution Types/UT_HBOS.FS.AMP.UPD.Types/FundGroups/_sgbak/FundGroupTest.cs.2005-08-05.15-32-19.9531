using HBOS.FS.AMP.UPD.Types.FundGroups;
using NUnit.Framework;

namespace UT_HBOS.FS.AMP.UPD.Types
{
	/// <summary>
	/// Unit Testing for FundGroups
	/// 
	/// At the current time (20/05/2005) AssetFundGroup and IndividualFundGroup are functionally identical
	/// therefore all tests are carried out on AssetFundGroup only.
	/// </summary>
	[TestFixture]
	public class FundGroups
	{
		/// <summary>
		/// Default constructor required for NUnit
		/// </summary>
		public FundGroups()
		{
		}

		private FundGroup subject;

		private const int expectedId = 1001;
		private const string expectedCompanyCode = "MyCompCode";
		private const string expectedFullName = "My Full Name Company";
		private const string expectedShortName = "My Short Name Company";
		private readonly byte[] expectedTimeStamp = {100};
		private bool expectedBool = true;


		/// <summary>
		/// Set up routine for NUnit. Creates a AssetFundGroup as the subject of the tests
		/// </summary>
		[SetUp]
		public void SetUp()
		{
			subject = FundGroupFactory.CreateFundGroup(
				expectedId,
				expectedCompanyCode,
				FundGroupFactory.FundGroupTypes.Asset,
				expectedFullName,
				expectedShortName,
				expectedTimeStamp,
				expectedBool,
				expectedBool,
				false,
				0);
		}

		/// <summary>
		/// Test the Factory creates an initialised AssetFundGroup.
		/// </summary>
		[Test]
		public void FactoryCreatesInitialisedAssetGroup()
		{
			Assert.AreEqual(typeof (AssetFundGroup), subject.GetType());
		}

		/// <summary>
		/// Test the Factory creates a default AssetFundGroup.
		/// </summary>
		[Test]
		public void FactoryCreatesDefaultAssetGroup()
		{
			subject = FundGroupFactory.CreateFundGroup(FundGroupFactory.FundGroupTypes.Asset);
			Assert.AreEqual(typeof (AssetFundGroup), subject.GetType());
		}


		/// <summary>
		/// Test the Factory creates a default IndividualFundGroup.
		/// </summary>
		[Test]
		public void FactoryCreatesDefaultIndividualGroup()
		{
			subject = FundGroupFactory.CreateFundGroup(FundGroupFactory.FundGroupTypes.Individual);
			Assert.AreEqual(typeof (IndividualFundGroup), subject.GetType());
		}

		/// <summary>
		/// Test the Factory creates an initialised IndividualFundGroup.
		/// </summary>
		[Test]
		public void FactoryCreatesInitialisedIndividualGroup()
		{
			subject = FundGroupFactory.CreateFundGroup(
				expectedId,
				expectedCompanyCode,
				FundGroupFactory.FundGroupTypes.Individual,
				expectedFullName,
				expectedShortName,
				expectedTimeStamp,
				expectedBool,
				expectedBool,
				false,
				0);

			Assert.AreEqual(typeof (IndividualFundGroup), subject.GetType());
		}


		/// <summary>
		/// Test the ID property
		/// </summary>
		[Test]
		public void InitialisedProperties_ID()
		{
			Assert.AreEqual(expectedId, subject.ID);
		}

		/// <summary>
		/// Test the CompanyCode property
		/// </summary>
		[Test]
		public void InitialisedProperties_CompanyCode()
		{
			Assert.AreEqual(expectedCompanyCode, subject.CompanyCode);
		}

		/// <summary>
		/// Test the FullName property
		/// </summary>
		[Test]
		public void InitialisedProperties_FullName()
		{
			Assert.AreEqual(expectedFullName, subject.FullName);
		}

		/// <summary>
		/// Test the ShortName property
		/// </summary>
		[Test]
		public void InitialisedProperties_ShortName()
		{
			Assert.AreEqual(expectedShortName, subject.ShortName);
		}

		/// <summary>
		/// Test the TimeStamp property
		/// </summary>
		[Test]
		public void InitialisedProperties_TimeStamp()
		{
			Assert.AreEqual(expectedTimeStamp, subject.TimeStamp);
		}

		/// <summary>
		/// Test the FullName property
		/// </summary>
		[Test]
		public void ToStringReturnsFullName()
		{
			Assert.AreEqual(expectedFullName, subject.ToString());
		}

		/// <summary>
		/// Test the IEntityBase properties
		/// </summary>
		[Test]
		public void InitialisedProperties_IEntity()
		{
			UnitTestHelpers.OverloadedConstructorIEntityTest(subject);
		}

		/// <summary>
		/// Determines whether the IsDirty flag is changed by updating FullName.
		/// </summary>
		[Test]
		public void IsDirtyFlagChangedByUpdatingFullName()
		{
			Assert.IsFalse(subject.IsDirty, "IsDirty was TRUE at beginning of test");
			subject.FullName = "Something else";
			Assert.IsTrue(subject.IsDirty);
		}

		/// <summary>
		/// Determines whether the IsDirty flag is changed by updating ShortName.
		/// </summary>
		[Test]
		public void IsDirtyFlagChangedByUpdatingShortName()
		{
			Assert.IsFalse(subject.IsDirty, "IsDirty was TRUE at beginning of test");
			subject.ShortName = "Something else";
			Assert.IsTrue(subject.IsDirty);
		}

	}
}