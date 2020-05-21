using System;
using MbUnit.Core.Framework;
using MbUnit.Framework;
using uk.gov.defra.Phoenix;

namespace uk.gov.defra.Phoenix.BOTest
{
	/// <summary>
	/// Summary description for TaxonomySearch.
	/// </summary>
	[TestFixture] 
	public class TaxonomySearch : TaxonomyBase
	{
		public TaxonomySearch()
		{
			//
			// TODO: Add constructor logic here
			//
		}
				
		[SetUp]
		public void WithNewCriteria()
		{
			SetCriteria();
		}
	
		#region MainFlowStep1-2
		/// <summary>
		/// Validate Phylum search criteria with minimum search strings.
		/// </summary>
		[Test()]
		public void TestValidateSearchStringMinPhylum()
		{
			SetCriteria();
			string SearchString = StringRepeater("CHORDATA", " ", BO.SearchTaxonomy.BOStringCriteria.MinSearchWords(BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.PhylumTaxon)); 
			mStringCriteria.SearchString = SearchString;
			ExecuteSearchString (SearchString);
		}		
		
		/// <summary>
		/// Validate Phylum search criteria with maximum search strings.
		/// </summary>
		[Test()]
		public void TestValidateSearchStringMaxPhylum()
		{
			string SearchString = StringRepeater("CHORDATA", " ", BO.SearchTaxonomy.BOStringCriteria.MaxSearchWords(BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.PhylumTaxon)); 
			mStringCriteria.SearchString = SearchString;
			ExecuteSearchString (SearchString);
		}		
		
		/// <summary>
		/// Validate Phylum search criteria with maximum + 1 search strings.
		/// </summary>
		[Test()]
		[ExpectedException(typeof(System.Exception), "Criteria Error")] 
		public void TestValidateSearchStringNPhylum()
		{
			string SearchString = StringRepeater("CHORDATA", " ", BO.SearchTaxonomy.BOStringCriteria.MaxSearchWords(BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.PhylumTaxon) + 1);
			mStringCriteria.SearchString = SearchString; 
			ExecuteSearchString (SearchString);
		}	
		
		/// <summary>
		/// Validate Class search criteria with minimum search strings.
		/// </summary>
		[Test()]
		public void TestValidateSearchStringMinClass()
		{
			string SearchString = StringRepeater("MAMMALIA", " ", BO.SearchTaxonomy.BOStringCriteria.MinSearchWords(BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.ClassTaxon));
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.ClassTaxon;
			ExecuteSearchString (SearchString);
		}		
		
		/// <summary>
		/// Validate Class search criteria with maximum search strings.
		/// </summary>
		[Test()]
		public void TestValidateSearchStringMaxClass()
		{
			string SearchString = StringRepeater("MAMMALIA", " ", BO.SearchTaxonomy.BOStringCriteria.MaxSearchWords(BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.ClassTaxon));
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.ClassTaxon;
			ExecuteSearchString (SearchString);
		}		
		
		/// <summary>
		/// Validate Class search criteria with maximum search strings.
		/// </summary>
		[Test()]
		[ExpectedException(typeof(System.Exception), "Criteria Error")] 
		public void TestValidateSearchStringNClass()
		{
			string SearchString = StringRepeater("MAMMALIA", " ", BO.SearchTaxonomy.BOStringCriteria.MaxSearchWords(BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.ClassTaxon) + 1);
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.ClassTaxon;
			ExecuteSearchString (SearchString);
		}	

		/// <summary>
		/// Validate Order search criteria with minimum search strings.
		/// </summary>
		[Test()]
		public void TestValidateSearchStringMinOrder()
		{
			string SearchString = StringRepeater("CARNIVORA", " ", BO.SearchTaxonomy.BOStringCriteria.MinSearchWords(BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.OrderTaxon));
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.OrderTaxon;
			ExecuteSearchString (SearchString);
		}		
		
		/// <summary>
		/// Validate Order search criteria with one search string.
		/// </summary>
		[Test()]
		public void TestValidateSearchStringMaxOrder()
		{
			string SearchString = StringRepeater("CARNIVORA", " ", BO.SearchTaxonomy.BOStringCriteria.MaxSearchWords(BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.OrderTaxon));
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.OrderTaxon;
			ExecuteSearchString (SearchString);
		}		
		
		/// <summary>
		/// Validate Order search criteria with maximum + 1 search strings.
		/// </summary>
		[Test()]
		[ExpectedException(typeof(System.ApplicationException),"Criteria Error")] 
		public void TestValidateSearchStringNOrder()
		{
			string SearchString = StringRepeater("CARNIVORA", " ", BO.SearchTaxonomy.BOStringCriteria.MaxSearchWords(BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.OrderTaxon) + 1);
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.OrderTaxon;
			ExecuteSearchString (SearchString);
		}	

		/// <summary>
		/// Validate Family search criteria with minimum search strings.
		/// </summary>
		[Test()]
		public void TestValidateSearchStringMinFamily()
		{
			string SearchString = StringRepeater("CARNIVORA", " ", BO.SearchTaxonomy.BOStringCriteria.MinSearchWords(BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.FamilyTaxon));
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.FamilyTaxon;
			ExecuteSearchString (SearchString);
		}		
		
		/// <summary>
		/// Validate Family search criteria with maximum search strings.
		/// </summary>
		[Test()]
		public void TestValidateSearchStringMaxFamily()
		{
			string SearchString = StringRepeater("CARNIVORA", " ", BO.SearchTaxonomy.BOStringCriteria.MaxSearchWords(BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.FamilyTaxon));
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.FamilyTaxon;
			ExecuteSearchString (SearchString);
		}		
		
		/// <summary>
		/// Validate Family search criteria with max + 1 search strings.
		/// </summary>
		[Test()]
		[ExpectedException(typeof(System.ApplicationException),"Criteria Error")] 
		public void TestValidateSearchStringNFamily()
		{
			string SearchString = StringRepeater("CARNIVORA", " ", BO.SearchTaxonomy.BOStringCriteria.MaxSearchWords(BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.FamilyTaxon) + 1);
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.FamilyTaxon;
			ExecuteSearchString (SearchString);
		}	

		/// <summary>
		/// Validate Genus search criteria with zero search strings.
		/// </summary>
		[Test()]
		public void TestValidateSearchStringMinGenus()
		{
			string SearchString = StringRepeater("CARNIVORA", " ", BO.SearchTaxonomy.BOStringCriteria.MinSearchWords(BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.GenusTaxon));
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.GenusTaxon;
			ExecuteSearchString (SearchString);
		}	

		/// <summary>
		/// Validate Genus search criteria with max search strings.
		/// </summary>
		[Test()]
		public void TestValidateSearchStringMaxGenus()
		{
			string SearchString = StringRepeater("CARNIVORA", " ", BO.SearchTaxonomy.BOStringCriteria.MaxSearchWords(BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.GenusTaxon));
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.GenusTaxon;
			ExecuteSearchString (SearchString);
		}		

		/// <summary>
		/// Validate Genus search criteria with max + 1 search strings.
		/// </summary>
		[Test()]
		[ExpectedException(typeof(System.ApplicationException),"Criteria Error")] 
		public void TestValidateSearchStringNGenus()
		{
			string SearchString = StringRepeater("CARNIVORA", " ", BO.SearchTaxonomy.BOStringCriteria.MaxSearchWords(BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.GenusTaxon) + 1);
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.GenusTaxon;
			ExecuteSearchString (SearchString);
		}		
		
		/// <summary>
		/// Validate Species search critieria with minimum search strings.
		/// </summary>
		[Test()]
		public void TestValidateSearchStringMinSpecies()
		{
			string SearchString = StringRepeater("CARNIVORA", " ", BO.SearchTaxonomy.BOStringCriteria.MinSearchWords(BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.SpeciesTaxon));
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.SpeciesTaxon;
			ExecuteSearchString (SearchString);
		}	

		/// <summary>
		/// Validate Species search criteria with maximum search strings.
		/// </summary>
		[Test()]
		public void TestValidateSearchStringMaxSpecies()
		{
			string SearchString = StringRepeater("CARNIVORA", " ", BO.SearchTaxonomy.BOStringCriteria.MaxSearchWords(BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.SpeciesTaxon));
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.SpeciesTaxon;
			ExecuteSearchString (SearchString);
		}		

		/// <summary>
		/// Validate Species search criteria with max + 1 search strings.
		/// </summary>
		[Test()]
		[ExpectedException(typeof(System.ApplicationException),"Criteria Error")] 
		public void TestValidateSearchStringNSpecies()
		{
			string SearchString = StringRepeater("CARNIVORA", " ", BO.SearchTaxonomy.BOStringCriteria.MaxSearchWords(BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.SpeciesTaxon) + 1);
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.SpeciesTaxon;
			ExecuteSearchString (SearchString);
		}

		/// <summary>
		/// Validate CommonName search criteria with minimum search strings.
		/// </summary>
		[Test()]
		public void TestValidateSearchStringMinCommonName()
		{
			string SearchString = StringRepeater("CARNIVORA", " ", BO.SearchTaxonomy.BOStringCriteria.MinSearchWords(BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.CommonName));
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.CommonName;
			ExecuteSearchString (SearchString);
		}	

		/// <summary>
		/// Validate CommonName search criteria with maximum search strings.
		/// </summary>
		[Test()]
		public void TestValidateSearchStringMaxCommonName()
		{
			string SearchString = StringRepeater("CARNIVORA", " ", BO.SearchTaxonomy.BOStringCriteria.MaxSearchWords(BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.CommonName));
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.CommonName;
			ExecuteSearchString (SearchString);
		}		
		
		/// <summary>
		/// Validate CommonName search criteria with max + 1 search strings.
		/// </summary>
		[Test()]
		[ExpectedException(typeof(System.ApplicationException),"Criteria Error")] 
		public void TestValidateSearchStringNCommonName()
		{
			string SearchString = StringRepeater("CARNIVORA", " ", BO.SearchTaxonomy.BOStringCriteria.MaxSearchWords(BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.CommonName) + 1);
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.CommonName;
			ExecuteSearchString (SearchString);
		}

		private void ExecuteSearchString(string SearchString)
		{
			mStringCriteria.SearchString = SearchString;
			Assert.AreEqual(mStringCriteria.SearchString, SearchString);
			mSearchResults = BO.Taxonomy.TaxonomySearch.SearchTaxa(mCommonCriteria, mStringCriteria);
			Assert.IsNotNull(mSearchResults);
		}

		/// <summary>
		/// Confirm that the * multi-character Wildcard works.
		/// </summary>
		[Test()]
		public void TestWildCardMulti()
		{
			//Check the multi-character wildcard finds the expected results.
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.PhylumTaxon;
			mStringCriteria.SearchString = "CHO*ATA";
			mStringCriteria.Soundex = false;
			mSearchResults = BO.Taxonomy.TaxonomySearch.SearchTaxa(mCommonCriteria, mStringCriteria);
			Assert.IsTrue (mSearchResults.Taxa.Length >= 1, "Searching 'CHO*ATA' did not return any results. At least one was expected.");
			foreach (BO.Taxonomy.BOTaxon Taxon in mSearchResults.Taxa)
			{
				Assert.IsTrue (Taxon.TaxonNameUnformatted.IndexOf ("CHORDATA") > -1, "The retrieved name does not contain 'CHORDATA'");
			}
			//Check the multi-character wildcard finds nothing when expected.
			mSearchResults = null;
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.PhylumTaxon;
			mStringCriteria.SearchString = "BBB*BBB";
			mStringCriteria.Soundex = false;
			mSearchResults = BO.Taxonomy.TaxonomySearch.SearchTaxa(mCommonCriteria, mStringCriteria);
			Assert.IsTrue (mSearchResults.Taxa.Length == 0, "Searching 'BBB*BBB' returned results. None were expected.");
		}
		
		/// <summary>
		/// Confirm that the ? single character Wildcard works.
		/// </summary>
		[Test()]
		public void TestWildCardSingle()
		{
			//Check the single character wildcard finds the expected results.
			mSearchResults = null;
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.PhylumTaxon;
			mStringCriteria.SearchString = "CHO??ATA";
			mStringCriteria.Soundex = false;
			mSearchResults = BO.Taxonomy.TaxonomySearch.SearchTaxa(mCommonCriteria, mStringCriteria);
			Assert.IsTrue (mSearchResults.Taxa.Length >= 1, "Searching 'CHO??ATA' did not return any results. At least one was expected.");
			foreach (BO.Taxonomy.BOTaxon Taxon in mSearchResults.Taxa)
			{
				Assert.IsTrue (Taxon.TaxonNameUnformatted.IndexOf ("CHORDATA") > -1, "The retrieved name does not contain 'CHORDATA'");
			}
			//Check the single character wildcard finds nothing when expected.
			mSearchResults = null;
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.PhylumTaxon;
			mStringCriteria.SearchString = "BBB?BBB";
			mStringCriteria.Soundex = false;
			mSearchResults = BO.Taxonomy.TaxonomySearch.SearchTaxa(mCommonCriteria, mStringCriteria);
			Assert.IsTrue (mSearchResults.Taxa.Length == 0, "Searching 'BBB?BBB' returned results. None were expected.");
		}

		/// <summary>
		/// Confirm that soundex searching with * wildcards is not allowed.
		/// </summary>
		[Test()]
		[ExpectedException(typeof(System.ApplicationException),"Criteria Error")]
		public void TestValidateSearchCriteriaSoundexWildCardMulti()
		{
			mSearchResults = null;
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.PhylumTaxon;
			mStringCriteria.SearchString = "BBB?BBB";
			mStringCriteria.Soundex = true;
			mSearchResults = BO.Taxonomy.TaxonomySearch.SearchTaxa(mCommonCriteria, mStringCriteria);
		}
		
		/// <summary>
		/// Confirm that soundex searching with ? wildcards is not allowed.
		/// </summary>
		[Test()]
		[ExpectedException(typeof(System.ApplicationException),"Criteria Error")]
		public void TestValidateSearchCriteriaSoundexWildCardSingle()
		{
			mSearchResults = null;
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.PhylumTaxon;
			mStringCriteria.SearchString = "BBB?BBB";
			mStringCriteria.Soundex = true;
			mSearchResults = BO.Taxonomy.TaxonomySearch.SearchTaxa(mCommonCriteria, mStringCriteria);
		}

		/// <summary>
		/// Confirm that soundex searching works with Phyla.
		/// </summary>
		[Test()]
		public void TestSoundexPhyla()
		{
			mSearchResults = null;
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.PhylumTaxon;
			mStringCriteria.SearchString = "Cordata"; //'h' removed from Chordata.
			mStringCriteria.Soundex = true;
			mSearchResults = BO.Taxonomy.TaxonomySearch.SearchTaxa(mCommonCriteria, mStringCriteria);
			Assert.IsTrue (mSearchResults.Taxa.Length >= 1, "Searching 'Cordata' with Soundex did not return any results. At least one was expected.");
			foreach (BO.Taxonomy.BOTaxon Taxon in mSearchResults.Taxa)
			{
				Assert.IsTrue (Taxon.TaxonNameUnformatted.IndexOf ("CHORDATA") > -1, "The retrieved name does not contain 'CHORDATA'");
			}
		}

		/// <summary>
		/// Confirm that soundex searching works with Classes.
		/// </summary>
		[Test()]
		public void TestSoundexClasses()
		{
			mSearchResults = null;
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.ClassTaxon;
			mStringCriteria.SearchString = "Mamalia"; //Double-m removed from Mammalia.
			mStringCriteria.Soundex = true;
			mSearchResults = BO.Taxonomy.TaxonomySearch.SearchTaxa(mCommonCriteria, mStringCriteria);
			Assert.IsTrue (mSearchResults.Taxa.Length >= 1, "Searching 'Mamalia' with Soundex did not return any results. At least one was expected.");
			foreach (BO.Taxonomy.BOTaxon Taxon in mSearchResults.Taxa)
			{
				Assert.IsTrue (Taxon.TaxonNameUnformatted.IndexOf ("MAMMALIA") > -1, "The retrieved name does not contain 'MAMMALIA'");
			}
		}

		/// <summary>
		/// Confirm that soundex searching works with Orders.
		/// </summary>
		[Test()]
		public void TestSoundexOrders()
		{
			mSearchResults = null;
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.OrderTaxon;
			mStringCriteria.SearchString = "Cernivora"; //'a' replaced with 'e' in CARNIVORA.
			mStringCriteria.Soundex = true;
			mSearchResults = BO.Taxonomy.TaxonomySearch.SearchTaxa(mCommonCriteria, mStringCriteria);
			Assert.IsTrue (mSearchResults.Taxa.Length >= 1, "Searching 'Cernivora' with Soundex did not return any results. At least one was expected.");
			foreach (BO.Taxonomy.BOTaxon Taxon in mSearchResults.Taxa)
			{
				Assert.IsTrue (Taxon.TaxonNameUnformatted.IndexOf ("CARNIVORA") > -1, "The retrieved name does not contain 'CARNIVORA'");
			}
		}

		/// <summary>
		/// Confirm that soundex searching works with Families.
		/// </summary>
		[Test()]
		public void TestSoundexFamilies()
		{
			mSearchResults = null;
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.FamilyTaxon;
			mStringCriteria.SearchString = "Acipitridae"; //Double-c removed from ACCIPITRIDAE.
			mStringCriteria.Soundex = true;
			mSearchResults = BO.Taxonomy.TaxonomySearch.SearchTaxa(mCommonCriteria, mStringCriteria);
			Assert.IsTrue (mSearchResults.Taxa.Length >= 1, "Searching 'Acipitridae' with Soundex did not return any results. At least one was expected.");
			foreach (BO.Taxonomy.BOTaxon Taxon in mSearchResults.Taxa)
			{
				Assert.IsTrue (Taxon.TaxonNameUnformatted.IndexOf ("ACCIPITRIDAE") > -1, "The retrieved name does not contain 'ACCIPITRIDAE'");
			}
		}

		/// <summary>
		/// Confirm that soundex searching works with Genera.
		/// </summary>
		[Test()]
		public void TestSoundexGenera()
		{
			mSearchResults = null;
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.GenusTaxon;
			mStringCriteria.SearchString = "Leptaseris"; //'o' replaced with 'a' in Leptoseris.
			mStringCriteria.Soundex = true;
			mSearchResults = BO.Taxonomy.TaxonomySearch.SearchTaxa(mCommonCriteria, mStringCriteria);
			Assert.IsTrue (mSearchResults.Taxa.Length >= 1, "Searching 'Leptaseris' with Soundex did not return any results. At least one was expected.");
			foreach (BO.Taxonomy.BOTaxon Taxon in mSearchResults.Taxa)
			{
				Assert.IsTrue (Taxon.TaxonNameUnformatted.IndexOf ("Leptoseris") > -1, "The retrieved name does not contain 'Leptoseris'");
			}
		}

		/// <summary>
		/// Confirm that soundex searching works with Species.
		/// </summary>
		[Test()]
		public void TestSoundexSpecies()
		{
			mSearchResults = null;
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.SpeciesTaxon;
			mStringCriteria.SearchString = "wheryi"; //Double-r removed from wherryi.
			mStringCriteria.Soundex = true;
			mSearchResults = BO.Taxonomy.TaxonomySearch.SearchTaxa(mCommonCriteria, mStringCriteria);
			Assert.IsTrue (mSearchResults.Taxa.Length >= 1, "Searching 'wheryi' with Soundex did not return any results. At least one was expected.");
			foreach (BO.Taxonomy.BOTaxon Taxon in mSearchResults.Taxa)
			{
				Assert.IsTrue (Taxon.TaxonNameUnformatted.IndexOf ("wherryi") > -1, "The retrieved name does not contain 'wherryi'");
			}
		}
		#endregion

		#region MainFlowStep3-4-5-6-7

		/// <summary>
		/// Confirm that getting the child taxa for a Kingdom is the same as searching for all Phyla below a Kingdom.
		/// </summary>
		[Test()]
		public void TestKingdomGetChildrenEqualSearchedChildren()
		{
			//Get the child taxa.
			BO.Taxonomy.BOTaxon AnimalKingdom = BO.Taxonomy.TaxonomySearch.GetAnimalKingdom();
			BO.Taxonomy.BOTaxon[] LowerTaxa = AnimalKingdom.GetLowerTaxa();
			Assert.GreaterThan (LowerTaxa.Length, 1);
			//Search for the child taxa.
			BO.Taxonomy.BOTaxonSearchResults SearchResults = null;
			mCommonCriteria.SearchForKingdomType = BO.SearchTaxonomy.SearchKingdomTypeEnum.Animal; 
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.PhylumTaxon;
			mStringCriteria.SearchString = string.Empty;
			SearchResults = BO.Taxonomy.TaxonomySearch.SearchTaxa(mCommonCriteria, mStringCriteria);
			Assert.GreaterThan (SearchResults.Taxa.Length, 1);
			//Test the 2 sets of results.
			Assert.AreSame(LowerTaxa.Length, SearchResults.Taxa.Length, "The get results are different to the search results");
			for (System.Int32 x = 0; x < LowerTaxa.Length; x++)
			{
				Assert.AreSame (LowerTaxa[x].LongScientificNameUnformatted, SearchResults.Taxa[x].LongScientificNameUnformatted, "The get results are different to the search results");
			}
		}

		/// <summary>
		/// Confirm that the list of Phyla for one Kingdom is different to the the list returned for another Kingdom.
		/// </summary>
		[Test()]
		public void TestPhylaForDifferentKingdoms()
		{
			//Get the phyla belonging to the Animal Kingdom.
			BO.Taxonomy.BOTaxonSearchResults AnimalSearchResults = null;
			mCommonCriteria.SearchForKingdomType = BO.SearchTaxonomy.SearchKingdomTypeEnum.Animal; 
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.PhylumTaxon;
			mStringCriteria.SearchString = string.Empty;
			AnimalSearchResults = BO.Taxonomy.TaxonomySearch.SearchTaxa(mCommonCriteria, mStringCriteria);
			Assert.GreaterThan (AnimalSearchResults.Taxa.Length, 0);
			//Get the phyla belonging to the Plant Kingdom.
			BO.Taxonomy.BOTaxonSearchResults PlantSearchResults = null;
			mCommonCriteria.SearchForKingdomType = BO.SearchTaxonomy.SearchKingdomTypeEnum.Plant; 
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.PhylumTaxon;
			mStringCriteria.SearchString = string.Empty;
			PlantSearchResults = BO.Taxonomy.TaxonomySearch.SearchTaxa(mCommonCriteria, mStringCriteria);
			Assert.GreaterThan (PlantSearchResults.Taxa.Length, 0);
			//Check that the animal and plant results are different. 
			foreach (BO.Taxonomy.BOTaxon PlantTaxon in PlantSearchResults.Taxa)
			{
				Assert.AreNotSame (PlantTaxon.LongScientificNameUnformatted, AnimalSearchResults.Taxa[0].LongScientificNameUnformatted,  "The same Phylum appears in more than one Kingdom.");
			}
			foreach (BO.Taxonomy.BOTaxon AnimalTaxon in AnimalSearchResults.Taxa)
			{
				Assert.AreNotSame (AnimalTaxon.LongScientificNameUnformatted, PlantSearchResults.Taxa[0].LongScientificNameUnformatted,  "The same Phylum appears in more than one Kingdom.");
			}
		}

		/// <summary>
		/// Confirm that a list of Phyla is returned in alphabetical order.
		/// </summary>
		[Test()]
		public void TestPhylaInAlphabeticalOrder()
		{
			//Get the child taxa.
			BO.Taxonomy.BOTaxon AnimalKingdom = BO.Taxonomy.TaxonomySearch.GetAnimalKingdom();
			BO.Taxonomy.BOTaxon[] LowerTaxa = AnimalKingdom.GetLowerTaxa();
			string PreviousName = string.Empty;
			foreach (BO.Taxonomy.BOTaxon Taxon in LowerTaxa)
			{
				Assert.GreaterThan (Taxon.LongScientificNameUnformatted,  PreviousName);
				PreviousName = Taxon.LongScientificNameUnformatted;
			}
		}

		/// <summary>
		/// Confirm that a list of Phyla for a Kingdom belong to that Kingdom.
		/// </summary>
		[Test()]
		public void TestPhylaBelongToKingdom()
		{
			//Get the child taxa.
			BO.Taxonomy.BOTaxon Kingdom = BO.Taxonomy.TaxonomySearch.GetAnimalKingdom();
			BO.Taxonomy.BOTaxon[] LowerTaxa = Kingdom.GetLowerTaxa();
			foreach (BO.Taxonomy.BOTaxon Taxon in LowerTaxa)
			{
				Assert.AreEqual (Taxon.ParentKingdomID,  Kingdom.KingdomID, "Phylum does not belong to Kingdom.");
				Assert.AreEqual (Taxon.ParentTaxonID,  Kingdom.TaxonId, "Phylum does not belong to Kingdom.");
				Assert.AreEqual (Taxon.ParentTaxonTypeID,  Kingdom.TaxonTypeID, "Phylum does not belong to Kingdom.");
			}
		}

		[Test()]
		public void TestSearchingForPhylumReturnsPhylum ()
		{
			BO.Taxonomy.BOTaxon Kingdom = BO.Taxonomy.TaxonomySearch.GetAnimalKingdom();
			BO.Taxonomy.BOTaxonSearchResults AnimalSearchResults = null;
			mCommonCriteria.SearchForKingdomType = BO.SearchTaxonomy.SearchKingdomTypeEnum.Animal; 
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.PhylumTaxon;
			mStringCriteria.SearchString = "Accipiter"; //Narrow the search results so that there are fewer than the maximum allowed.
			AnimalSearchResults = BO.Taxonomy.TaxonomySearch.SearchTaxa(mCommonCriteria, mStringCriteria);
			foreach (BO.Taxonomy.BOTaxon Taxon in AnimalSearchResults.Taxa)
			{
				Assert.IsTrue (Taxon.TaxonType == BO.Taxonomy.TaxonTypeEnum.Phylum);
				Assert.IsTrue (Taxon.KingdomID == Kingdom.KingdomID);
			}
		}

		[Test()]
		public void TestSearchingForClassReturnsClass ()
		{
			BO.Taxonomy.BOTaxon Kingdom = BO.Taxonomy.TaxonomySearch.GetAnimalKingdom();
			BO.Taxonomy.BOTaxonSearchResults AnimalSearchResults = null;
			mCommonCriteria.SearchForKingdomType = BO.SearchTaxonomy.SearchKingdomTypeEnum.Animal; 
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.ClassTaxon;
			mStringCriteria.SearchString = "Accipiter"; //Narrow the search results so that there are fewer than the maximum allowed.
			AnimalSearchResults = BO.Taxonomy.TaxonomySearch.SearchTaxa(mCommonCriteria, mStringCriteria);
			foreach (BO.Taxonomy.BOTaxon Taxon in AnimalSearchResults.Taxa)
			{
				Assert.IsTrue (Taxon.TaxonType == BO.Taxonomy.TaxonTypeEnum.Class);
				Assert.IsTrue (Taxon.KingdomID == Kingdom.KingdomID);
			}
		}

		[Test()]
		public void TestSearchingForOrderReturnsOrder ()
		{
			BO.Taxonomy.BOTaxon Kingdom = BO.Taxonomy.TaxonomySearch.GetAnimalKingdom();
			BO.Taxonomy.BOTaxonSearchResults AnimalSearchResults = null;
			mCommonCriteria.SearchForKingdomType = BO.SearchTaxonomy.SearchKingdomTypeEnum.Animal; 
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.OrderTaxon;
			mStringCriteria.SearchString = "Accipiter"; //Narrow the search results so that there are fewer than the maximum allowed.
			AnimalSearchResults = BO.Taxonomy.TaxonomySearch.SearchTaxa(mCommonCriteria, mStringCriteria);
			foreach (BO.Taxonomy.BOTaxon Taxon in AnimalSearchResults.Taxa)
			{
				Assert.IsTrue (Taxon.TaxonType == BO.Taxonomy.TaxonTypeEnum.Order);
				Assert.IsTrue (Taxon.KingdomID == Kingdom.KingdomID);
			}
		}

		[Test()]
		public void TestSearchingForFamilyReturnsFamily ()
		{
			BO.Taxonomy.BOTaxon Kingdom = BO.Taxonomy.TaxonomySearch.GetAnimalKingdom();
			BO.Taxonomy.BOTaxonSearchResults AnimalSearchResults = null;
			mCommonCriteria.SearchForKingdomType = BO.SearchTaxonomy.SearchKingdomTypeEnum.Animal; 
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.FamilyTaxon;
			mStringCriteria.SearchString = "Accipiter"; //Narrow the search results so that there are fewer than the maximum allowed.
			AnimalSearchResults = BO.Taxonomy.TaxonomySearch.SearchTaxa(mCommonCriteria, mStringCriteria);
			foreach (BO.Taxonomy.BOTaxon Taxon in AnimalSearchResults.Taxa)
			{
				Assert.IsTrue (Taxon.TaxonType == BO.Taxonomy.TaxonTypeEnum.Family);
				Assert.IsTrue (Taxon.KingdomID == Kingdom.KingdomID);
			}
		}

		[Test()]
		public void TestSearchingForGenusReturnsGenus ()
		{
			BO.Taxonomy.BOTaxon Kingdom = BO.Taxonomy.TaxonomySearch.GetAnimalKingdom();
			BO.Taxonomy.BOTaxonSearchResults AnimalSearchResults = null;
			mCommonCriteria.SearchForKingdomType = BO.SearchTaxonomy.SearchKingdomTypeEnum.Animal; 
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.GenusTaxon;
			mStringCriteria.SearchString = "Accipiter"; //Narrow the search results so that there are fewer than the maximum allowed.
			AnimalSearchResults = BO.Taxonomy.TaxonomySearch.SearchTaxa(mCommonCriteria, mStringCriteria);
			foreach (BO.Taxonomy.BOTaxon Taxon in AnimalSearchResults.Taxa)
			{
				Assert.IsTrue (Taxon.TaxonType == BO.Taxonomy.TaxonTypeEnum.Genus);
				Assert.IsTrue (Taxon.KingdomID == Kingdom.KingdomID);
			}
		}

		[Test()]
		public void TestSearchingForSpeciesReturnsSpecies ()
		{
			BO.Taxonomy.BOTaxon Kingdom = BO.Taxonomy.TaxonomySearch.GetAnimalKingdom();
			BO.Taxonomy.BOTaxonSearchResults AnimalSearchResults = null;
			mCommonCriteria.SearchForKingdomType = BO.SearchTaxonomy.SearchKingdomTypeEnum.Animal; 
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.SpeciesTaxon;
			mStringCriteria.SearchString = "Accipiter"; //Narrow the search results so that there are fewer than the maximum allowed.
			AnimalSearchResults = BO.Taxonomy.TaxonomySearch.SearchTaxa(mCommonCriteria, mStringCriteria);
			foreach (BO.Taxonomy.BOTaxon Taxon in AnimalSearchResults.Taxa)
			{
				Assert.IsTrue (Taxon.TaxonType == BO.Taxonomy.TaxonTypeEnum.Species | Taxon.TaxonType == BO.Taxonomy.TaxonTypeEnum.Epithet | Taxon.TaxonType == BO.Taxonomy.TaxonTypeEnum.Stock);
				Assert.IsTrue (Taxon.KingdomID == Kingdom.KingdomID);
			}
		}
		#endregion

		#region MainFlowStep8
		[Test()]
		public void TestSearchForCommonNames ()
		{
			BO.Taxonomy.BOCommonNameSearchResults AnimalSearchResults = null;
			mCommonCriteria.SearchForKingdomType = BO.SearchTaxonomy.SearchKingdomTypeEnum.Animal; 
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.CommonName;
			mStringCriteria.SearchString = "Sturgeon"; //Narrow the search results so that there are fewer than the maximum allowed.
			AnimalSearchResults = BO.Taxonomy.TaxonomySearch.SearchCommonNames(mCommonCriteria, mStringCriteria);
			Assert.IsTrue (AnimalSearchResults.CommonNames.Length > 0);
			//Check that the common names are in alphabetical order.
			string PreviousName = string.Empty;
			foreach (BO.Taxonomy.BOCommonNameResults Taxon in AnimalSearchResults.CommonNames)
			{
				Assert.GreaterThan (Taxon.Name, PreviousName);
				PreviousName = Taxon.Name;
			}
		}
		[Test()]
		public void TestSearchForCommonNamesAreUnique ()
		{
			BO.Taxonomy.BOCommonNameSearchResults AnimalSearchResults = null;
			mCommonCriteria.SearchForKingdomType = BO.SearchTaxonomy.SearchKingdomTypeEnum.Animal; 
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.CommonName;
			mStringCriteria.SearchString = "Anim"; //Narrow the search results so that there are fewer than the maximum allowed.
			AnimalSearchResults = BO.Taxonomy.TaxonomySearch.SearchCommonNames(mCommonCriteria, mStringCriteria);
			Assert.IsTrue (AnimalSearchResults.CommonNames.Length > 0);
			//Check that the common names are unique.
			foreach (BO.Taxonomy.BOCommonNameResults Taxon1 in AnimalSearchResults.CommonNames)
			{
				System.Int32 TaxonCounter = 0;
				foreach (BO.Taxonomy.BOCommonNameResults Taxon2 in AnimalSearchResults.CommonNames)
				{
					if (Taxon1.Name.CompareTo (Taxon2.Name) == 0 & Taxon1.TaxonScientificName.CompareTo (Taxon2.TaxonScientificName) == 0)
					{
						TaxonCounter ++;
					}
				}
				Assert.IsTrue (TaxonCounter == 1);
			}
		}
		#endregion
		
		#region MainFlowStep9
		[Test()]
		public void TestSearchForUsage ()
		{
			//Get a Taxon that has usage details.
			BO.Taxonomy.BOTaxon AnimalKingdom = BO.Taxonomy.TaxonomySearch.GetAnimalKingdom();
			BO.Taxonomy.BOTaxon UsageTaxon = GetTaxonDescendant (AnimalKingdom, BO.Taxonomy.TaxonTypeEnum.Species, new CustomConditionDelegate(CustomConditionGetTaxonHavingUsage));
			Assert.IsNotNull (UsageTaxon);
			BO.Taxonomy.BOUsage TaxonUsage = UsageTaxon.GetUsage () [0];
			//Search using usage details to retreive the taxon first thought of.
			BO.Taxonomy.BOTaxonSearchResults AnimalSearchResults = null;
			mCommonCriteria.SearchForKingdomType = BO.SearchTaxonomy.SearchKingdomTypeEnum.Animal; 
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.Usage;
			BO.SearchTaxonomy.BOUsageCriteria UsageCriteria = new uk.gov.defra.Phoenix.BO.SearchTaxonomy.BOUsageCriteria();
			UsageCriteria.LevelOfUseID = TaxonUsage.LevelID;
			UsageCriteria.PartID = TaxonUsage.PartID;
			UsageCriteria.UsageTypeID = TaxonUsage.TypeID;
			AnimalSearchResults = BO.Taxonomy.TaxonomySearch.SearchUsage(mCommonCriteria, UsageCriteria);
			Assert.IsTrue (AnimalSearchResults.Taxa.Length > 0);
			bool UsageTaxonMatchesSearchedTaxon = false;
			foreach (BO.Taxonomy.BOTaxon FoundTaxon in AnimalSearchResults.Taxa)
			{
				UsageTaxonMatchesSearchedTaxon = UsageTaxonMatchesSearchedTaxon | (UsageTaxon.Id == FoundTaxon.Id);
			}
			Assert.IsTrue (UsageTaxonMatchesSearchedTaxon);
		}
		#endregion

		#region MainFlowStep13
		[Test ()]
		public void TestRetriveStockNames ()
		{
			//Get a Taxon that has stock names.
			BO.Taxonomy.BOTaxon AnimalKingdom = BO.Taxonomy.TaxonomySearch.GetAnimalKingdom ();
			BO.Taxonomy.BOTaxon Parent = GetTaxonDescendant (AnimalKingdom, BO.Taxonomy.TaxonTypeEnum.Species, new CustomConditionDelegate(CustomConditionGetTaxonHavingStock));
			BO.Taxonomy.BOTaxon [] StockTaxa = Parent.GetStockNames ();
			Assert.IsTrue (StockTaxa.Length > 0);
			foreach (BO.Taxonomy.BOTaxon Stock in StockTaxa)
			{
				Assert.IsTrue (Stock.TaxonType == BO.Taxonomy.TaxonTypeEnum.Stock);
			}
		}
		[Test ()]
		public void TestStockNameParents ()
		{
			//Only species can have stock names.
			BO.Taxonomy.BOTaxon AnimalKingdom = BO.Taxonomy.TaxonomySearch.GetAnimalKingdom ();
			Assert.IsNull (GetTaxonDescendant (AnimalKingdom, BO.Taxonomy.TaxonTypeEnum.Phylum, new CustomConditionDelegate(CustomConditionGetTaxonHavingStock)));
			Assert.IsNull (GetTaxonDescendant (AnimalKingdom, BO.Taxonomy.TaxonTypeEnum.Class, new CustomConditionDelegate(CustomConditionGetTaxonHavingStock)));
			Assert.IsNull (GetTaxonDescendant (AnimalKingdom, BO.Taxonomy.TaxonTypeEnum.Order, new CustomConditionDelegate(CustomConditionGetTaxonHavingStock)));
			Assert.IsNull (GetTaxonDescendant (AnimalKingdom, BO.Taxonomy.TaxonTypeEnum.Family, new CustomConditionDelegate(CustomConditionGetTaxonHavingStock)));
			Assert.IsNull (GetTaxonDescendant (AnimalKingdom, BO.Taxonomy.TaxonTypeEnum.Genus, new CustomConditionDelegate(CustomConditionGetTaxonHavingStock)));
		}
		[Test ()]
		public void TestRetrieveSummaryQuotaAllIsCallable ()
		{
			//Get a Taxon that has Quotas.
			BO.Taxonomy.BOTaxon AnimalKingdom = BO.Taxonomy.TaxonomySearch.GetAnimalKingdom ();
			BO.Taxonomy.BOTaxon TaxonWithQuotas = GetTaxonDescendant (AnimalKingdom, BO.Taxonomy.TaxonTypeEnum.Species, new CustomConditionDelegate(CustomConditionGetTaxonHavingSummaryQuotaAll));
			Assert.IsNotNull (TaxonWithQuotas);
		}
		#endregion

		
		
	}
}
