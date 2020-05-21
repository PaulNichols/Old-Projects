using System;
using MbUnit.Core.Framework;
using MbUnit.Framework;

namespace uk.gov.defra.Phoenix.BOTest
{
	/// <summary>
	/// Summary description for TaxonomyBaseTest.
	/// </summary>
	[TestFixture]
	public class TaxonomyBaseTest: TaxonomyBase
	{
		public TaxonomyBaseTest()
		{
			//
			// TODO: Add constructor logic here
			//
		}
        
		/// <summary>
		/// Test the string repeater.
		/// </summary>
		[Test()]
		public void TestStringRepeater()
		{
			string Repeated;
			Repeated = StringRepeater("One", " ", 1);
			Assert.AreEqual(Repeated, "One");
			Repeated = StringRepeater("Two", " ", 2);
			Assert.AreEqual(Repeated, "Two Two");
			Repeated = StringRepeater( "Zero", " ", 0);
			Assert.AreEqual(Repeated, string.Empty);
		}

		[Test()]
		public void TestGetTaxonDescendant()
		{
			BO.Taxonomy.BOTaxon Kingdom = BO.Taxonomy.TaxonomySearch.GetAnimalKingdom ();
			Assert.AreEqual (GetTaxonDescendant (Kingdom, BO.Taxonomy.TaxonTypeEnum.Phylum).TaxonType, BO.Taxonomy.TaxonTypeEnum.Phylum, "Phylum requested but not returned.");
			Assert.AreEqual (GetTaxonDescendant (Kingdom, BO.Taxonomy.TaxonTypeEnum.Class).TaxonType, BO.Taxonomy.TaxonTypeEnum.Class, "Class requested but not returned.");
			Assert.AreEqual (GetTaxonDescendant (Kingdom, BO.Taxonomy.TaxonTypeEnum.Order).TaxonType, BO.Taxonomy.TaxonTypeEnum.Order, "Order requested but not returned.");
			Assert.AreEqual (GetTaxonDescendant (Kingdom, BO.Taxonomy.TaxonTypeEnum.Family).TaxonType, BO.Taxonomy.TaxonTypeEnum.Family, "Family requested but not returned.");
			Assert.AreEqual (GetTaxonDescendant (Kingdom, BO.Taxonomy.TaxonTypeEnum.Genus).TaxonType, BO.Taxonomy.TaxonTypeEnum.Genus, "Genus requested but not returned.");
			Assert.AreEqual (GetTaxonDescendant (Kingdom, BO.Taxonomy.TaxonTypeEnum.Species).TaxonType, BO.Taxonomy.TaxonTypeEnum.Species, "Species requested but not returned.");
			Assert.AreEqual (GetTaxonDescendant (Kingdom, BO.Taxonomy.TaxonTypeEnum.Epithet).TaxonType, BO.Taxonomy.TaxonTypeEnum.Epithet, "Epithet requested but not returned.");
			Assert.AreEqual (GetTaxonDescendant (Kingdom, BO.Taxonomy.TaxonTypeEnum.Stock).TaxonType, BO.Taxonomy.TaxonTypeEnum.Stock, "Stock requested but not returned.");
		}

		[Test()]
		public void TestCustomConditionGetTaxonNotIn()
		{
			BO.Taxonomy.BOTaxon Taxon = BO.Taxonomy.TaxonomySearch.GetAnimalKingdom ();
			BO.Taxonomy.BOTaxon FirstChild = GetTaxonDescendant (Taxon, BO.Taxonomy.TaxonTypeEnum.Genus);
			Assert.IsNotNull (FirstChild);
			BO.Taxonomy.BOTaxon SecondChild = GetTaxonDescendant (Taxon, BO.Taxonomy.TaxonTypeEnum.Genus, new CustomConditionDelegate(CustomConditionGetTaxonNotIn), new object[] {FirstChild});
			Assert.IsNotNull (SecondChild);
			Assert.AreNotSame (FirstChild.Id, SecondChild.Id, "Child taxa are the same. Expected different taxa.");
		}

		[Test()]
		public void TestGetTaxonDescendants()
		{
			BO.Taxonomy.BOTaxon Kingdom = BO.Taxonomy.TaxonomySearch.GetAnimalKingdom ();
			Assert.IsTrue (GetTaxonDescendants (Kingdom, BO.Taxonomy.TaxonTypeEnum.Class, null).Length > 0);
		}

		[Test()]
		public void TestCustomConditionTaxaMultiple()
		{
			BO.Taxonomy.BOTaxon Kingdom = BO.Taxonomy.TaxonomySearch.GetAnimalKingdom ();
			BO.Taxonomy.BOTaxon [] Taxa = GetTaxonDescendants (Kingdom, BO.Taxonomy.TaxonTypeEnum.Class, new CustomConditionTaxaDelegate (CustomConditionTaxaMultiple));
			Assert.IsTrue (Taxa.Length > 1);
		}
	}
}
