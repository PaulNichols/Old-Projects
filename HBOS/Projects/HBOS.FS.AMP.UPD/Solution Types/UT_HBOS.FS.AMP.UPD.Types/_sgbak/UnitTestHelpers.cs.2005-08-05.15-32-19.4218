using System;
using System.Reflection;
using HBOS.FS.AMP.Entities;
using NUnit.Framework;

namespace UT_HBOS.FS.AMP.UPD.Types
{
	/// <summary>
	/// Helper code for unit testing
	/// </summary>
	public class UnitTestHelpers
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public UnitTestHelpers()
		{
		}

		/// <summary>
		/// Use to test the IEntityBase flag implementation on default constructors
		/// </summary>
		/// <param name="entity"></param>
		public static void DefaultConstructorIEntityTest(IEntityBase entity)
		{
			String failures = string.Empty;

			if (!entity.IsNew)
				failures += "IsNew is false. ";

			if (entity.IsDirty)
				failures += "IsDirty is true. ";

			if (entity.IsDeleted)
				failures += "IsDeleted is true. ";

			Assert.IsTrue((failures == string.Empty), failures);
		}

		/// <summary>
		/// Use to test the IEntityBase flag implementation on overloaded (data backed) constructors
		/// </summary>
		/// <param name="entity"></param>
		public static void OverloadedConstructorIEntityTest(IEntityBase entity)
		{
			string failures = string.Empty;

			if (entity.IsNew)
				failures += "IsNew is true. ";

			if (entity.IsDirty)
				failures += "IsDirty is true. ";

			if (entity.IsDeleted)
				failures += "IsDeleted is true. ";

			Assert.IsTrue((failures == string.Empty), failures);
		}

		/// <summary>
		/// Use to test the IsDirty flag implementation
		/// </summary>
		/// <param name="entity"></param>
		public static void ModifiedIEntityTest(IEntityBase entity)
		{
			Assert.IsTrue(entity.IsDirty, "IsDirty is false");
		}

		/// <summary>
		/// Writes the IEntityBase members to screen
		/// </summary>
		/// <param name="entity"></param>
		public static void WriteIEntityBaseMembers(IEntityBase entity)
		{
			Console.WriteLine("IsDirty = " + entity.IsDirty);
			Console.WriteLine("IsNew = " + entity.IsNew);
			Console.WriteLine("IsDeleted = " + entity.IsDeleted);
			Console.WriteLine("Timestamp = " + entity.TimeStamp.ToString());
		}

		/// <summary>
		/// Writes out all the public properties of an object
		/// </summary>
		/// <param name="entity"></param>
		public static void WriteEntityData(object entity)
		{
			PropertyInfo[] properties = entity.GetType().GetProperties();
			for (int i = 0; i < properties.Length; i++)
			{
				PropertyInfo currentProperty = properties[i];
				try
				{
					Console.WriteLine(currentProperty.Name + " = " + currentProperty.GetValue(entity, null).ToString());
				}
				catch
				{
					Console.WriteLine("Couldn't write properties for " + currentProperty.Name);
				}
			}
		}

	}
}