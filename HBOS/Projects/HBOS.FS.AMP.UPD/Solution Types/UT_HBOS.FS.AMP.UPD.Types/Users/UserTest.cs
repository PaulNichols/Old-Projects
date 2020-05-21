using System;

using NUnit.Framework;
using HBOS.FS.AMP.UPD.Types.Users;

namespace UT_HBOS.FS.AMP.UPD.Types.Users
{
	/// <summary>
	/// Base class for unit testing the User entity
	/// </summary>
	public abstract class UserTestBase
	{
		/// <summary>
		/// Set up the each test before running it. Creates the subject by calling the abstract CreateSubject
		/// </summary>
		[SetUp]
		public void SetUp()
		{
			subject = CreateSubject();
		}

		/// <summary>
		/// The User under test
		/// </summary>
		protected User subject;

		/// <summary>
		/// Override to create the subject of each unit test
		/// </summary>
		/// <returns>The User entity under test</returns>
		protected abstract User CreateSubject();

		/// <summary>
		/// The expected value of UserName
		/// </summary>
		protected const string expectedUserName = "User Name";

		/// <summary>
		/// The expected value of UserId
		/// </summary>
		protected const string expectedUserId   = "User Id";

//		/// <summary>
//		/// Test the value of the Permissions property
//		/// </summary>
//		[Test]
//		public void Permissions()
//		{
//			UserPermissions permissions = new UserPermissions(false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, "HECM");
//			subject.Permissions = permissions;
//			Assert.AreSame(permissions,subject.Permissions);
//		}

		/// <summary>
		/// Test the value of the UserName property
		/// </summary>
		[Test]
		public void UserName()
		{
			Assert.AreEqual(expectedUserName,subject.UserName);
		}

		/// <summary>
		/// Test the value of the UserId property
		/// </summary>
		[Test]
		public void UserId()
		{
			Assert.AreEqual(expectedUserId,subject.LogOnID);
		}

		/// <summary>
		/// Test that IEntity is updated by a change
		/// </summary>
		[Test]
		public void IEntityUpdatedByChange()
		{
			subject.UserName = "New Name";
			UnitTestHelpers.ModifiedIEntityTest(subject);
		}

	
	}
	
	/// <summary>
	/// Unit tests for the UPD User entity created through the default constructor
	/// </summary>
	[TestFixture()]
	public class DefaultUserTest: UserTestBase
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public DefaultUserTest()
		{
		}

		/// <summary>
		/// Creates the subject of each unit test
		/// </summary>
		/// <returns>A new uninitialised user</returns>
		protected override User CreateSubject()
		{
			return new User(expectedUserId,expectedUserName);
		}


		/// <summary>
		/// Test the properties of IEntityBase
		/// </summary>
		[Test]
		public void IEntityBase()
		{
			UnitTestHelpers.DefaultConstructorIEntityTest(subject);
		}
	}


	/// <summary>
	/// Unit tests for the UPD User entity created through the database serialisation constructor
	/// </summary>
	[TestFixture]
	public class InitialisedUserTest: UserTestBase
	{
		/// <summary>
		/// Default constructor needed for NUnit
		/// </summary>
		public InitialisedUserTest()
		{
		}

		private const string expectedLastChangedBy = "me";
		private readonly DateTime expectedLastChangedDate = new DateTime(1900,02,28);
		private readonly byte[] expectedTimeStamp = {1};
		private const bool expectedDeleted = true;

		/// <summary>
		/// Creates the subject of each unit test
		/// </summary>
		/// <returns>A new initialised user</returns>
		protected override User CreateSubject()
		{
			return new User(expectedUserId,expectedUserName, expectedDeleted, expectedLastChangedBy, expectedLastChangedDate, expectedTimeStamp);
		}

		/// <summary>
		/// Test the properties of IEntityBase
		/// </summary>
		[Test]
		public void IEntityBase()
		{
			UnitTestHelpers.OverloadedConstructorIEntityTest(subject);
		}

		/// <summary>
		/// Test the value of the LastChangedBy property
		/// </summary>
		[Test]
		public void LastChangedBy()
		{
			Assert.AreEqual(expectedLastChangedBy,subject.LastChangedBy);
		}

		/// <summary>
		/// Test the value of the LastChangedDate property
		/// </summary>
		[Test]
		public void LastChangedDate()
		{
			Assert.AreEqual(expectedLastChangedDate,subject.LastChangedDate);
		}

		/// <summary>
		/// Test the value of the Timestamp property
		/// </summary>
		[Test]
		public void Timestamp()
		{
			Assert.AreEqual(expectedTimeStamp,subject.TimeStamp);
		}

		/// <summary>
		/// Test the value of the DeletedInDB property
		/// </summary>
		[Test]
		public void DeletedInDB()
		{
			Assert.AreEqual(expectedDeleted,subject.IsDeletedInDB);
		}
	}
}
