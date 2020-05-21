using System;
using System.Reflection;
using NUnit.Framework;

namespace UT_HBOS.FS.AMP.UPD.Types
{
	/// <summary>
	/// Enumeration used to describe how the subject of a test was created
	///   Default    - 
	///	  Initialied - 
	/// </summary>
	public enum EntityConstructorType
	{
		/// <summary>
		/// The parameterless constructor used for new entites is under test
		/// </summary>
		Default,

		/// <summary>
		/// The constructor with parameters used for object rehydration is under test
		/// </summary>
		Initialised
	}

	/// <summary>
	/// PropertyTestBase provides an abstract class that allows for the testing of the property values thru 
	/// reflection for entity classes
	/// </summary>
	public abstract class PropertyTestBase
	{
		private object m_subject;

		/// <summary>
		/// The subject object under test
		/// </summary>
		protected object subject
		{
			get {return m_subject;}
		}

		/// <summary>
		/// Create the subject of each test before running, calls abstract method CreateSubject
		/// </summary>
		[SetUp]
		public virtual void SetUp()
		{
			m_subject = CreateSubject();
		}

		/// <summary>
		/// Override to create the subject of the tests
		/// </summary>
		protected abstract object CreateSubject();
		
		/// <summary>
		/// Override to set the constructor type used to create the subject
		/// </summary>
		protected abstract EntityConstructorType constructorType
		{ 
			get;
		}

		/// <summary>
		/// Asserts that a property has a specified value
		/// </summary>
		/// <param name="propName">
		///		The name of the property to test (case sensitive)
		///	</param>
		/// <param name="expectedValue">
		///		The expected value of the property. For default constructors the value of the property is set to
		///		this value before doing the assert.
		///	</param>
		protected void AssertPropertyValue(string propName, object expectedValue)
		{
			Type type = subject.GetType();
			PropertyInfo pi = type.GetProperty(propName);
			if (pi == null)
				Assert.Fail("Cannot find property {0} using reflection",propName);

			if (constructorType == EntityConstructorType.Default)
				pi.SetValue(subject,expectedValue,null);

			object propValue = pi.GetValue(subject,null);
			
			Assert.AreEqual(expectedValue,propValue);
		}

		/// <summary>
		/// Tests the properties of IEntityBase
		/// </summary>
		[Test]
		public virtual void IEntityBase()
		{
			HBOS.FS.AMP.Entities.IEntityBase entity = subject as HBOS.FS.AMP.Entities.IEntityBase;
			if (entity != null)
			{
				if (constructorType == EntityConstructorType.Default)
				{
					UnitTestHelpers.DefaultConstructorIEntityTest(entity);
				}
				else
				{
					UnitTestHelpers.OverloadedConstructorIEntityTest(entity);
				}
			}
		}

	}

}
