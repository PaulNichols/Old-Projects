using System;

using HBOS.FS.AMP.Entities;

using NUnit.Framework;

namespace UT_HBOS.FS.AMP.Entities
{
	/// <summary>
	/// Summary description for EntityValidationTest.
	/// </summary>
	[TestFixture]
    public class EntityValidationTest
	{
		public EntityValidationTest()
		{}

        [Test]
        public void TestValidationAttributes()
        { 
            // Create an entity
            TestEntity newEntity = new TestEntity();

            // Make sure the NotNull fails by setting the Name property to null
            newEntity.Name = null;

            // Make sure the MinLength and MaxLength attributes both fail by having a 12 char string
            newEntity.FullName = "123456789012";

            // Validate
            EntityValidator validator = new EntityValidator();
            Assert.IsFalse(validator.ValidateProperties(newEntity));
            Console.WriteLine(validator.InvalidMessage);

        }
	}
}
