using System;
using System.Collections;
using MbUnit.Core.Framework;
using MbUnit.Framework;
using uk.gov.defra.Phoenix;
using uk.gov.defra.Phoenix.BO.ReferenceData;
using uk.gov.defra.Phoenix.BO.Taxonomy;
using uk.gov.defra.Phoenix.BO.SearchTaxonomy;
using uk.gov.defra.Phoenix.DO.DataObjects;
using uk.gov.defra.Phoenix.DO.DataObjects.Entity;
using uk.gov.defra.Phoenix.DO.DataObjects.EntitySet;
using uk.gov.defra.Phoenix.DO.DataObjects.Collection;

namespace uk.gov.defra.Phoenix.BOTest
{
	/// <summary>
	/// Tests the BO layer used by the TaxonomyControl methods in TaxonomyService.
	/// </summary>
	[TestFixture] 
	public class TaxonomyControl : TaxonomyBase
	{
		protected BOTaxon mSpecies;

		/// <summary>
		/// Initialises the set of tests in this file by searching Taxonomy to create a set
		/// of search results. The protected member mSpecies is set to the first taxon in the first
		/// search result.
		/// </summary>
		[TestFixtureSetUp]
		public void Setup()
		{
			SetCriteria();
			mCommonCriteria.SearchForComponentType = SearchableTaxonomyComponentEnum.SpeciesTaxon;
			mStringCriteria.SearchString = string.Empty; 
			Assert.AreEqual(mStringCriteria.SearchString, string.Empty);
			
			mSearchResults = BO.Taxonomy.TaxonomySearch.SearchTaxa(mCommonCriteria, mStringCriteria);
			Assert.IsNotNull(mSearchResults);
			Assert.IsNotNull(mSearchResults.Taxa);
			Assert.IsTrue(mSearchResults.Taxa.Length > 0);
			
			mSpecies = mSearchResults.Taxa[0];
		}		
		
		/// <summary>
		/// Simulates the GetAnimalDelegationAuthority web service call
		/// </summary>
		[Test()]
		public void GetAnimalDelegationAuthority()
		{
			LoadAnimalDelegationAuthority();
		}		
		
		/// <summary>
		/// Simulates the SaveAnimalDelegationAuthority web service call. Retrieves an
		/// animal delegation authority, stores its hyperlink, changes the hyperlink, saves the authority,
		/// then changes it back and save it again. After each save, the object is retrieved again
		/// to check that it has been amended correctly.
		/// </summary>
		[Test()]
		public void SaveAnimalDelegationAuthority()
		{
			BOAnimalDelegationAuthority authority = LoadAnimalDelegationAuthority();
			string originalHyperlink  = authority.HyperlinkRTARoadmap;
			string temporaryHyperlink = "www.unittest.com";

			SaveWithNewHyperlink(authority, temporaryHyperlink);
			SaveWithNewHyperlink(authority, originalHyperlink);
		}		
		
		/// <summary>
		/// Simulates the DeleteAnimalDelegationAuthority web service call. Tries each taxon on the
		/// search results until it finds one that has room to add an animal delegation authority. Adds the
		/// authority then deletes it, checking the count of authorities for the chosen taxon before, after and during the
		/// process.
		/// </summary>
		[Test()]
		public void DeleteAnimalDelegationAuthority()
		{
			foreach (BOTaxon taxon in mSearchResults.Taxa)
			{										// seek the first taxon we can use...
				ArrayList availableCodes = GetAvailableGuidelineCodes(taxon);
				if (availableCodes.Count > 0)		// found one with room to add
				{									// add, then delete
					int                         countBefore = GetDelegationDisplays(taxon).Length;
					BOAnimalDelegationAuthority authority   = AddAnimalDelegationAuthority(taxon, (string)availableCodes[0]);
					int                         countAfter  = GetDelegationDisplays(taxon).Length;
					Assert.IsNotNull(authority);
					Assert.IsTrue(countAfter == countBefore + 1);

					authority.Delete();
					Assert.IsTrue(GetDelegationDisplays(taxon).Length == countBefore);	//back to where we were
					return;
				}
			}
			Assert.Fail("No species found with available guidelines");
		}
		
		/// <summary>
		/// Simulates the GetAnimalLicensingDetail web service call. Seeks the
		/// first taxon with an animal licensing detail, and then gets the detail.
		/// </summary>
		[Test()]
		public void GetAnimalLicensingDetail()
		{
			GetFirstLicensingDetail();
		}		

		/// <summary>
		/// Simulates the SaveAnimalLicensingDetail web service call. Seeks the
		/// first taxon with an animal licensing detail, and then gets the detail.
		/// Increments the average lifespan, saves the detail, and then retrieves it again to check
		/// that the increment happened. Decrements the lifespan, then saves and retrieves it again
		/// to check that the record is back where it started.
		/// </summary>
		[Test()]
		public void SaveAnimalLicensingDetail()
		{
			BOAnimalLicensingDetail detail  = GetFirstLicensingDetail();
			int                     oldLife = detail.AverageLifespan;
			
			detail.AverageLifespan++;
			detail.Save();
			detail = GetFirstLicensingDetail();
			Assert.IsTrue(detail.AverageLifespan == oldLife + 1);

			detail.AverageLifespan--;
			detail.Save();
			detail = GetFirstLicensingDetail();
			Assert.IsTrue(detail.AverageLifespan == oldLife);
		}		

		/// <summary>
		/// Simulates the DeleteAnimalLicensingDetail web service call. Seeks the
		/// first taxon without an animal licensing detail, and then adds one. It is 
		/// necessary to find a taxon with no detail, as each taxon can only have a 
		/// maximum of one detail. After adding the detail, it is deleted. The method
		/// then tries to retrieve the record again, which fails with an exception.
		/// </summary>
		/// <exception cref="BO.RecordDoesNotExist">Thrown when an attempt is made to
		/// access the deleted record</exception>
		[Test()]
		[ExpectedException(typeof(BO.RecordDoesNotExist))] 
		public void DeleteAnimalLicensingDetail()
		{
			foreach (BOTaxon taxon in mSearchResults.Taxa)
			{										// seek the first taxon without a licensing display
				BOAnimalLicensingDisplay display = taxon.GetAnimalLicensing();
				if (display == null)
				{									// add one, delete it, then try to get it again
					BOAnimalLicensingDetail detail   = new BOAnimalLicensingDetail();
					int                     id;
					detail.KingdomId                 = taxon.KingdomID;
					detail.TaxonId                   = taxon.TaxonId;
					detail.TaxonTypeId               = taxon.TaxonTypeID;
					detail.AverageLifespan           = 2;
					detail.AverageNumberOfOffspring  = 2;
					detail.BirdFeeLevel              = 2;
					detail.IncubationOrGestationDays = 20;
					detail.MinimumMicrochipAge       = 1;
					detail.MinimumMicrochipSize      = 1;
					detail.OldestAcceptedAge         = 100;
					detail.SexualMaturityAge         = 2;
					detail = (BOAnimalLicensingDetail)detail.Save();
					Assert.IsNotNull(detail);
					Assert.IsTrue(detail.AnimalLicensingId > 0);

					id = detail.AnimalLicensingId;
					detail.Delete();
					detail = new BOAnimalLicensingDetail(id);	// try and get it again: should throw an exception
					Assert.Fail("No exception thrown");			// should not get here
					return;
				}
			}
			Assert.Fail("No species found without a licensing display");
		}		

		/// <summary>
		/// Simulates the GetTaxonomyNotes web service call, which returns Species Notes. Gets
		/// the species notes for the current species, adds a note and gets them again to show
		/// that an extra note is returned. Deletes the extra note to leave the system as it was.
		/// </summary>
		[Test()]
		public void GetTaxonomyNotes()
		{
			BO.Taxonomy.TaxonomyNote[] notes1  = mSpecies.GetNotes();
			BO.Taxonomy.TaxonomyNote   newNote = AddTaxonomyNote(mSpecies);
			BO.Taxonomy.TaxonomyNote[] notes2  = mSpecies.GetNotes();
			
			Assert.IsTrue(notes2.Length == notes1.Length + 1);
			newNote.Delete();
			Assert.IsTrue(mSpecies.GetNotes().Length == notes1.Length);
		}		

		/// <summary>
		/// Simulates the SaveTaxonomyNote web service call, which saves a Species Note. Seeks
		/// the first species with a species note, modifies the species note and then saves and retrieves it.
		/// Sets the content of the species note back to the original value.
		/// </summary>
		[Test()]
		public void SaveTaxonomyNote()
		{
			foreach (BOTaxon taxon in mSearchResults.Taxa)
			{										// seek the first taxon with a species note
				BO.Taxonomy.TaxonomyNote[] notes  = taxon.GetNotes();
				if (notes != null && notes.Length > 0)
				{									
					string oldContents = notes[0].Content;
					
					notes[0].Content = "Test";
					notes[0].Save();
					notes = taxon.GetNotes();
					Assert.IsTrue(notes[0].Content == "Test");
					
					notes[0].Content = oldContents;
					notes[0].Save();
					notes = taxon.GetNotes();
					Assert.IsTrue(notes[0].Content == oldContents);
					return;
				}
			}
			Assert.Fail("No species found without a licensing display");
		}		

		/// <summary>
		/// Simulates the DeleteTaxonomyNote web service call, which deletes a Species Note. A species note
		/// is added to the current species, and then deleted.
		/// </summary>
		[Test()]
		public void DeleteTaxonomyNote()
		{
			BO.Taxonomy.TaxonomyNote[] notes   = mSpecies.GetNotes();
			BO.Taxonomy.TaxonomyNote   newNote = AddTaxonomyNote(mSpecies);
			int                        id      = newNote.NoteId;

			newNote.Delete();
			Assert.IsTrue(notes.Length == mSpecies.GetNotes().Length);
		}		

		/// <summary>
		/// Simulates the GetLegislationNames web service call. The web service call has a parameter
		/// to indicate whether legislation names with or without associated permitted listing values
		/// should be returned. This method calls the underlying business object method twice: once to
		/// retrieve those legislation names that do have associated permitted listing values; and again
		/// to retrieve those legislation names that don't. The method first builds a hashtable of all
		/// the legislation names and removes both sets of names from the hashtable, which ends up being
		/// empty as proof that the two original calls returned all legislation names between them.
		/// </summary>
		[Test()]
		public void GetLegislationNames()
		{
			TaxonomyLegislationNameSet items      = TaxonomyLegislationName.GetAll();
			Hashtable                  names      = new Hashtable();
			BOLegislationNameSummary[] summaries1 = BOLegislationName.GetNames(true);
			BOLegislationNameSummary[] summaries2 = BOLegislationName.GetNames(false);
			
			foreach (TaxonomyLegislationName item in items)	// build a hashtable of all the legislation names
			{
				names.Add(item.LegislationNameID, null);
			}
			RemoveAllNames(names, summaries1);				// remove all legislation names that do have permitted listing values
			RemoveAllNames(names, summaries2);				// remove all legislation names that don't have permitted listing values
			Assert.IsTrue(names.Count == 0);				// should be none left
		}

		/// <summary>
		/// Simulates the GetPermittedListingValues web service call. Retrieves the permitted listing values
		/// for a legislation name that does have associated permitted listing values and checks that the result
		/// set is not empty; retrieves the permitted listing values
		/// for a legislation name that does not have associated permitted listing values and checks that the result
		/// set is empty.
		/// </summary>
		[Test()]
		public void GetPermittedListingValues()
		{
			BOLegislationNameSummary[] summaries1 = BOLegislationName.GetNames(true);	//names that have permitted listing values
			BOLegislationNameSummary[] summaries2 = BOLegislationName.GetNames(false);	//names that don't
			int                        nameId1    = summaries1[0].Id;
			int                        nameId2    = summaries2[0].Id;
			
			Assert.IsTrue(BOPermittedListingValue.GetByLegislationId(nameId1, null).Length > 0);
			Assert.IsTrue(BOPermittedListingValue.GetByLegislationId(nameId2, null).Length == 0);
		}		

		/// <summary>
		/// Simulates the GetPermittedListingValue web service call.
		/// </summary>
		[Test()]
		public void GetPermittedListingValue()
		{
			LoadPermittedListingValue();
		}		

		/// <summary>
		/// Simulates the SavePermittedListingValue web service call. Retrieves a permitted
		/// listing value and then saves it twice, changing the listing value the first time and
		/// changing it back the second time.
		/// </summary>
		[Test()]
		public void SavePermittedListingValue()
		{
			BOPermittedListingValue listing = LoadPermittedListingValue();
			string originalValue  = listing.ListingValue;
			string temporaryValue = "unittest";

			SaveWithNewValue(listing, temporaryValue);
			SaveWithNewValue(listing, originalValue);
		}
		
		/// <summary>
		/// Simulates the DeletePermittedListingValue web service call. Gets all the permitted listing values
		/// for a legislation name that does have associated listing values. Adds a listing value to the legislation
		/// name. Gets the permitted listing values again to show that the count has increased by one. Deletes
		/// the listing value that was added and gets the list again to show that the count is now unchanged from
		/// the original.
		/// </summary>
		[Test()]
		public void DeletePermittedListingValue()
		{
			BOLegislationNameSummary[] summaries = BOLegislationName.GetNames(true);	//names that have permitted listing values
			int                        nameId    = summaries[0].Id;
			int                        count     = BOPermittedListingValue.GetByLegislationId(nameId, null).Length;
			BOPermittedListingValue    listing   = AddListingValue(nameId);

			Assert.IsTrue(BOPermittedListingValue.GetByLegislationId(nameId, null).Length == count + 1);
			listing.Delete();
			Assert.IsTrue(BOPermittedListingValue.GetByLegislationId(nameId, null).Length == count);
		}

		/// <summary>
		/// Saves a permitted listing value with a new ListingValue.
		/// </summary>
		/// <param name="listing">the object to save</param>
		/// <param name="val">the new value</param>
		protected void SaveWithNewValue(BOPermittedListingValue listing, string val)
		{
			listing.ListingValue = val;
			listing.Save();
			listing = new BOPermittedListingValue(listing.PermittedListingId);
			Assert.IsTrue(listing.ListingValue == val);
		}		

		/// <summary>
		/// Obtains a permitted listing value, then gets it directly via its id.
		/// </summary>
		/// <returns>a permitted listing value</returns>
		protected BOPermittedListingValue LoadPermittedListingValue()
		{
			int                     listingId = GetFirstListingValue().PermittedListingId;
			BOPermittedListingValue listing   = new BOPermittedListingValue(listingId);

			Assert.IsNotNull(listing);
			Assert.IsTrue(listing.PermittedListingId == listingId);
			return listing;
		}		

		/// <summary>
		/// Gets the first permitted listing value for the first legislation name.
		/// </summary>
		/// <returns>the permitted listing value</returns>
		protected BOPermittedListingValue GetFirstListingValue()
		{
			BOLegislationNameSummary[] summaries = BOLegislationName.GetNames(true);	//names that have permitted listing values
			int                        nameId    = summaries[0].Id;
			BOPermittedListingValue[]  listings  = BOPermittedListingValue.GetByLegislationId(nameId, null);
			return listings[0];
		}

		/// <summary>
		/// Adds a permitted listing value to a particular legislation name
		/// </summary>
		/// <param name="nameId">the id of the legislation name</param>
		/// <returns>the permitted listing value</returns>
		protected BOPermittedListingValue AddListingValue(int nameId)
		{
			BOPermittedListingValue listing = new BOPermittedListingValue();
			listing.LegislationNameId = nameId;
			listing.ListingValue      = "test";
			listing = (BOPermittedListingValue)listing.Save();
			Assert.IsNotNull(listing);
			Assert.IsTrue(listing.PermittedListingId > 0);
			return listing;
		}

		/// <summary>
		/// Adds a species note to a species.
		/// </summary>
		/// <param name="taxon">the species to receive the new note</param>
		/// <returns>the species note</returns>
		protected BO.Taxonomy.TaxonomyNote AddTaxonomyNote(BOTaxon taxon)
		{
			BO.Taxonomy.TaxonomyNote note = new BO.Taxonomy.TaxonomyNote();
			note.OtherId = taxon.Id;
			note.Content = "Test";
			note.Subject = "Test";
			note.Active  = true;
			note = (BO.Taxonomy.TaxonomyNote)note.Save();
			Assert.IsNotNull(note);
			Assert.IsTrue(note.NoteId > 0);
			return note;
		}
	
		/// <summary>
		/// Removes all the legislation names in an array of legislation names from a hashtable
		/// </summary>
		/// <param name="names">the hashtable</param>
		/// <param name="summaries">the array of legislation names</param>
		protected void RemoveAllNames(Hashtable names, BOLegislationNameSummary[] summaries)
		{
			foreach (BOLegislationNameSummary summary in summaries)
			{
				Assert.IsTrue(names.ContainsKey(summary.Id));
				names.Remove(summary.Id);
			}
		}

		/// <summary>
		/// Seeks the first species with a licensing detail, and returns the detail.
		/// </summary>
		/// <returns>the licensing detail</returns>
		protected BOAnimalLicensingDetail GetFirstLicensingDetail()
		{
			foreach (BOTaxon taxon in mSearchResults.Taxa)
			{										// seek the first taxon with a licensing display
				BOAnimalLicensingDisplay display = taxon.GetAnimalLicensing();
				if (display != null)
				{
					BOAnimalLicensingDetail detail = new BOAnimalLicensingDetail(display.ID);
					Assert.IsNotNull(detail);
					Assert.IsTrue(detail.AnimalLicensingId == display.ID);
					return detail;
				}
			}
			Assert.Fail("No species found with licensing display");
			return null;
		}		

		/// <summary>
		/// Adds an animal delegation authority to a species.
		/// </summary>
		/// <param name="taxon">the species to receive the new authority</param>
		/// <param name="availableCode">a string containing two ids in the form "ddd,aaa" where ddd is
		/// the delegation code and aaa is the application type id</param>
		/// <returns>the animal delegation authority</returns>
		protected BOAnimalDelegationAuthority AddAnimalDelegationAuthority(BOTaxon taxon, string availableCode)
		{
			BOAnimalDelegationAuthority authority = new BOAnimalDelegationAuthority();
			string[]                    parts     = availableCode.Split(",".ToCharArray());
			authority.DelegationCode      = Int32.Parse(parts[0]);
			authority.ApplicationTypeID   = Int32.Parse(parts[1]);
			authority.SpeciesKingdomID    = taxon.KingdomID;
			authority.SpeciesTaxonomyID   = taxon.TaxonId;
			authority.SpeciesTaxonTypeID  = taxon.TaxonTypeID;
			authority.HyperlinkRTARoadmap = "www.unittest.com";
			return (BOAnimalDelegationAuthority)authority.Save();
		}

		/// <summary>
		/// Retrieve a collection of available guideline codes for a particular species
		/// </summary>
		/// <param name="taxon">the species</param>
		/// <returns>a collection of strings containing two ids in the form "ddd,aaa" where ddd is
		/// the delegation code and aaa is the application type id</returns>
		public ArrayList GetAvailableGuidelineCodes(BOTaxon taxon)
		{
			DelegationGuidelineBoundCollection   guidelines = BODelegationGuideline.GetAll(false);
			BOAnimalDelegationAuthorityDisplay[] displays   = GetDelegationDisplays(taxon);
			ArrayList                            available  = new ArrayList();

			Assert.IsNotNull(displays);
			Assert.IsNotNull(guidelines);
			foreach (DelegationGuideline guideline in guidelines)				// build up array of "x,y" strings
			{
				available.Add(guideline.Code.ToString() + "," + guideline.ApplicationTypeCode.ToString());
			}
			foreach (BOAnimalDelegationAuthorityDisplay display in displays)	// reduce array as "x,y" strings are found
			{
				BOAnimalDelegationAuthority authority = GetAuthority(display);
				available.Remove(authority.DelegationCode.ToString() + "," + authority.ApplicationTypeID.ToString());
			}
			return available;													// any left?
		}		
		
		/// <summary>
		/// Retrieves a collection of delegation "display" objects for a particular species.
		/// </summary>
		/// <param name="taxon">the species</param>
		/// <returns>the collection of delegation "display" objects</returns>
		protected BOAnimalDelegationAuthorityDisplay[] GetDelegationDisplays(BOTaxon taxon)
		{
			BOAnimalDelegationAuthorityDisplay[] displays = taxon.GetAnimalDelegationAuthority();
			Assert.IsNotNull(displays);
			return displays;
		}		
		
		/// <summary>
		/// Gets the first animal delegation authority object for a particular species.
		/// </summary>
		/// <returns>the authority</returns>
		protected BOAnimalDelegationAuthority LoadAnimalDelegationAuthority()
		{
			BOAnimalDelegationAuthorityDisplay[] displays = GetDelegationDisplays(mSpecies);
			return GetAuthority(displays[0]);
		}		
		
		/// <summary>
		/// Gets an animal delegation authority given an authority "display" object
		/// </summary>
		/// <param name="display">the "display" object</param>
		/// <returns>the authority</returns>
		protected BOAnimalDelegationAuthority GetAuthority(BOAnimalDelegationAuthorityDisplay display)
		{
			BOAnimalDelegationAuthority authority;
			int						    authorityId = display.AnimalDelegationAuthorityID;
			
			Assert.IsTrue(authorityId > 0);
			authority = new BOAnimalDelegationAuthority(authorityId);
			Assert.IsNotNull(authority);
			return authority;
		}		
		
		/// <summary>
		/// Saves an animal delegation authority with a new hyperlink
		/// </summary>
		/// <param name="authority">the authority to save</param>
		/// <param name="hyperlink">the new hyperlink</param>
		protected void SaveWithNewHyperlink(BOAnimalDelegationAuthority authority, string hyperlink)
		{
			authority.HyperlinkRTARoadmap = hyperlink;
			authority.Save();
			authority = LoadAnimalDelegationAuthority();
			Assert.IsTrue(authority.HyperlinkRTARoadmap == hyperlink);
		}		
	}
}
