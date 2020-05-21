namespace UT_HBOS.FS.AMP.UPD.Types.Users
{
	/// <summary>
	/// Base class for the unit testing of the UPD User Permissions entity
	/// </summary>
	public abstract class UserPermissionsTestBase
	{
//		/// <summary>
//		/// Creates the subject of the test before each test is run
//		/// </summary>
//		[SetUp]
//		public void SetUp()
//		{
//			m_subject = CreateSubject();
//		}
//
//		
//		private UserPermission m_subject;		
//		
//		/// <summary>
//		/// The UserPermission entity under test
//		/// </summary>
//		protected UserPermissions subject
//		{
//			get {return m_subject;}
//		}
//
//		/// <summary>
//		/// Override this method to create the UserPermission under test
//		/// </summary>
//		/// <returns></returns>
//		protected abstract UserPermissions CreateSubject();
//
//		/// <summary>
//		/// The value expected in the CompanyCode property
//		/// </summary>
//		protected const string expectedCompanyCode = "HBOS";
//
//		/// <summary>
//		/// Test the value of the Administrator property
//		/// </summary>
//		[Test]
//		public void Administrator()
//		{
//			Assert.IsFalse(subject.Administrator,"Not false at start");
//			subject.Administrator = true;
//			Assert.IsTrue(subject.Administrator,"Not changed to true");
//			UnitTestHelpers.ModifiedIEntityTest(subject);
//		}
//
//		/// <summary>
//		/// Test the value of the DistributePrices property
//		/// </summary>
//		[Test]
//		public void DistributePrices()
//		{
//			Assert.IsFalse(subject.DistributePrices,"Not false at start");
//			subject.DistributePrices = true;
//			Assert.IsTrue(subject.DistributePrices,"Not changed to true");
//			UnitTestHelpers.ModifiedIEntityTest(subject);
//		}
//
//		/// <summary>
//		/// Test the value of the ExportOEICSPrices property
//		/// </summary>
//		[Test]
//		public void ExportOEICSPrices()
//		{
//			Assert.IsFalse(subject.ExportOEICSPrices,"Not false at start");
//			subject.ExportOEICSPrices = true;
//			Assert.IsTrue(subject.ExportOEICSPrices,"Not changed to true");
//			UnitTestHelpers.ModifiedIEntityTest(subject);
//		}
//
//		/// <summary>
//		/// Test the value of the ImportExchangeRates property
//		/// </summary>
//		[Test]
//		public void ImportExchangeRates()
//		{
//			Assert.IsFalse(subject.ImportExchangeRates,"Not false at start");
//			subject.ImportExchangeRates = true;
//			Assert.IsTrue(subject.ImportExchangeRates,"Not changed to true");
//			UnitTestHelpers.ModifiedIEntityTest(subject);
//		}
//
//		/// <summary>
//		/// Test the value of the ImportHI3Prices property
//		/// </summary>
//		[Test]
//		public void ImportHI3Prices()
//		{
//			Assert.IsFalse(subject.CanImportOEICPrices,"Not false at start");
//			subject.CanImportOEICPrices = true;
//			Assert.IsTrue(subject.CanImportOEICPrices,"Not changed to true");
//			UnitTestHelpers.ModifiedIEntityTest(subject);
//		}
//
//		/// <summary>
//		/// Test the value of the ImportMarketIndices property
//		/// </summary>
//		[Test]
//		public void ImportMarketIndices()
//		{
//			Assert.IsFalse(subject.ImportMarketIndices,"Not false at start");
//			subject.ImportMarketIndices = true;
//			Assert.IsTrue(subject.ImportMarketIndices,"Not changed to true");
//			UnitTestHelpers.ModifiedIEntityTest(subject);
//		}
//
//		/// <summary>
//		/// Test the value of the ImportOverSeasFundWeightings property
//		/// </summary>
//		[Test]
//		public void ImportOverSeasFundWeightings()
//		{
//			Assert.IsFalse(subject.ImportOverSeasFundWeightings,"Not false at start");
//			subject.ImportOverSeasFundWeightings = true;
//			Assert.IsTrue(subject.ImportOverSeasFundWeightings,"Not changed to true");
//			UnitTestHelpers.ModifiedIEntityTest(subject);
//		}
//
//		/// <summary>
//		/// Test the value of the MaintainCalculationFactors property
//		/// </summary>
//		[Test]
//		public void MaintainCalculationFactors()
//		{
//			Assert.IsFalse(subject.MaintainCalculationFactors,"Not false at start");
//			subject.MaintainCalculationFactors = true;
//			Assert.IsTrue(subject.MaintainCalculationFactors,"Not changed to true");
//			UnitTestHelpers.ModifiedIEntityTest(subject);
//		}
//
//		/// <summary>
//		/// Test the value of the MaintainCalculationIndices property
//		/// </summary>
//		[Test]
//		public void MaintainCalculationIndices()
//		{
//			Assert.IsFalse(subject.MaintainCalculationIndices,"Not false at start");
//			subject.MaintainCalculationIndices = true;
//			Assert.IsTrue(subject.MaintainCalculationIndices,"Not changed to true");
//			UnitTestHelpers.ModifiedIEntityTest(subject);
//		}
//
//		/// <summary>
//		/// Test the value of the MaintainDistributionMethods property
//		/// </summary>
//		[Test]
//		public void MaintainDistributionMethods()
//		{
//			Assert.IsFalse(subject.MaintainDistributionMethods,"Not false at start");
//			subject.MaintainDistributionMethods = true;
//			Assert.IsTrue(subject.MaintainDistributionMethods,"Not changed to true");
//			UnitTestHelpers.ModifiedIEntityTest(subject);
//		}
//
//		/// <summary>
//		/// Test the value of the MaintainDistributionSubscribers property
//		/// </summary>
//		[Test]
//		public void MaintainDistributionSubscribers()
//		{
//			Assert.IsFalse(subject.MaintainDistributionSubscribers,"Not false at start");
//			subject.MaintainDistributionSubscribers = true;
//			Assert.IsTrue(subject.MaintainDistributionSubscribers,"Not changed to true");
//			UnitTestHelpers.ModifiedIEntityTest(subject);
//		}
//
//		/// <summary>
//		/// Test the value of the MaintainFundGroups property
//		/// </summary>
//		[Test]
//		public void MaintainFundGroups()
//		{
//			Assert.IsFalse(subject.MaintainFundGroups,"Not false at start");
//			subject.MaintainFundGroups = true;
//			Assert.IsTrue(subject.MaintainFundGroups,"Not changed to true");
//			UnitTestHelpers.ModifiedIEntityTest(subject);
//		}
//
//		/// <summary>
//		/// Test the value of the MaintainAssetFunds property
//		/// </summary>
//		[Test]
//		public void MaintainAssetFunds()
//		{
//			Assert.IsFalse(subject.MaintainAssetFunds,"Not false at start");
//			subject.MaintainAssetFunds = true;
//			Assert.IsTrue(subject.MaintainAssetFunds,"Not changed to true");
//			UnitTestHelpers.ModifiedIEntityTest(subject);
//		}
//
//		/// <summary>
//		/// Test the value of the MaintainUserAccess property
//		/// </summary>
//		[Test]
//		public void MaintainUserAccess()
//		{
//			Assert.IsFalse(subject.MaintainUserAccess,"Not false at start");
//			subject.MaintainUserAccess = true;
//			Assert.IsTrue(subject.MaintainUserAccess,"Not changed to true");
//			UnitTestHelpers.ModifiedIEntityTest(subject);
//		}
//
//		/// <summary>
//		/// Test the value of the MaintainValidationTolerances property
//		/// </summary>
//		[Test]
//		public void MaintainValidationTolerances()
//		{
//			Assert.IsFalse(subject.MaintainValidationTolerances,"Not false at start");
//			subject.MaintainValidationTolerances = true;
//			Assert.IsTrue(subject.MaintainValidationTolerances,"Not changed to true");
//			UnitTestHelpers.ModifiedIEntityTest(subject);
//		}
//
//		/// <summary>
//		/// Test the value of the ReleasePrices property
//		/// </summary>
//		[Test]
//		public void ReleasePrices()
//		{
//			Assert.IsFalse(subject.ReleasePrices,"Not false at start");
//			subject.ReleasePrices = true;
//			Assert.IsTrue(subject.ReleasePrices,"Not changed to true");
//			UnitTestHelpers.ModifiedIEntityTest(subject);
//		}
//
//		/// <summary>
//		/// Test the value of the AuthorisePrices property
//		/// </summary>
//		[Test]
//		public void AuthorisePrices()
//		{
//			Assert.IsFalse(subject.AuthorisePrices,"Not false at start");
//			subject.AuthorisePrices = true;
//			Assert.IsTrue(subject.AuthorisePrices,"Not changed to true");
//			UnitTestHelpers.ModifiedIEntityTest(subject);
//		}
//
//		/// <summary>
//		/// Test the value of the CompanyName property
//		/// </summary>
//		[Test]
//		public void CompanyName()
//		{
//			Assert.AreEqual(expectedCompanyCode,subject.CompanyCode,"Not correct at start");
//		}
//
//	}
//	
//	
//	/// <summary>
//	/// Unit tests for UserPermission entity created thru the default constructor
//	/// </summary>
//	[TestFixture]
//	public class DefaultUserPermissionsTest: UserPermissionsTestBase
//	{
//		/// <summary>
//		/// Default constructor for NUnit
//		/// </summary>
//		public DefaultUserPermissionsTest()
//		{
//		}
//
//		/// <summary>
//		/// Create the subject of the unit test
//		/// </summary>
//		/// <returns>A UserPermission created with the default constructor</returns>
//		protected override UserPermissions CreateSubject()
//		{
//			return new UserPermissions(false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, expectedCompanyCode);
//		}
//
//		/// <summary>
//		/// Test the properties of IEntityBase
//		/// </summary>
//		[Test]
//		public void IEntityBase()
//		{
//			UnitTestHelpers.DefaultConstructorIEntityTest(subject);
//		}
//	}
//
//	/// <summary>
//	/// Unit tests for UserPermission entity created thru the constructor used by database serialisation
//	/// </summary>
//	[TestFixture]
//	public class InitialisedUserPermissionsTest: UserPermissionsTestBase
//	{
//		/// <summary>
//		/// Default constructor for NUnit
//		/// </summary>
//		public InitialisedUserPermissionsTest()
//		{
//		}
//
//		private const string expectedLastChangedBy = "me";
//		private readonly DateTime expectedLastChangedDate = new DateTime(1900,02,28);
//		private readonly byte[] expectedTimeStamp = {1};
//
//		/// <summary>
//		/// Create the subject of the unit test
//		/// </summary>
//		/// <returns>A UserPermission created with the serialisation constructor</returns>
//		protected override UserPermissions CreateSubject()
//		{
//			return new UserPermissions(false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, expectedCompanyCode, expectedLastChangedBy, expectedLastChangedDate, expectedTimeStamp);
//		}
//
//		/// <summary>
//		/// Test the properties of IEntityBase
//		/// </summary>
//		[Test]
//		public void IEntityBase()
//		{
//			UnitTestHelpers.OverloadedConstructorIEntityTest(subject);
//		}
//
//		/// <summary>
//		/// Test the value of the LastChangedBy property
//		/// </summary>
//		[Test]
//		public void LastChangedBy()
//		{
//			Assert.AreEqual(expectedLastChangedBy,subject.LastChangedBy);
//		}
//
//		/// <summary>
//		/// Test the value of the LastChangedDate property
//		/// </summary>
//		[Test]
//		public void LastChangedDate()
//		{
//			Assert.AreEqual(expectedLastChangedDate,subject.LastChangedDate);
//		}
//
//		/// <summary>
//		/// Test the value of the Timestamp property
//		/// </summary>
//		[Test]
//		public void Timestamp()
//		{
//			Assert.AreEqual(expectedTimeStamp,subject.TimeStamp);
//		}
	}
}