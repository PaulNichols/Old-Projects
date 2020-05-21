using System;
using MbUnit.Core.Framework;
using MbUnit.Framework;
using uk.gov.defra.Phoenix;

namespace uk.gov.defra.Phoenix.BOTest
{
	/// <summary>
	/// Base class for TaxonomySearch, TaxononyControl, etc
	/// </summary>
	
	public class TaxonomyBase
	{
		protected BO.SearchTaxonomy.BOCommonCriteria mCommonCriteria;
		protected BO.SearchTaxonomy.BOStringCriteria mStringCriteria;
		protected BO.Taxonomy.BOTaxonSearchResults   mSearchResults;		

		public TaxonomyBase()
		{
		}

		protected void SetCriteria()
		{
			//Create an object.
			mCommonCriteria = new BO.SearchTaxonomy.BOCommonCriteria();
			Assert.IsNotNull(mCommonCriteria, "Common criteria constructor failed");
			//Check any default values. MF1, Step2.
			Assert.AreEqual(mCommonCriteria.SearchForComponentType, BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.SpeciesTaxon, "Default search level is not Species");
			mCommonCriteria.SearchForComponentType = BO.SearchTaxonomy.SearchableTaxonomyComponentEnum.PhylumTaxon;
			//Set remaining search criteria.
			mCommonCriteria.SearchForRestriction = BO.SearchTaxonomy.SearchRestrictionEnum.None;
			Assert.AreEqual(mCommonCriteria.SearchForRestriction, BO.SearchTaxonomy.SearchRestrictionEnum.None, "Property assignment failed");
			mStringCriteria = new BO.SearchTaxonomy.BOStringCriteria();
			Assert.IsNotNull(mStringCriteria, "String criteria constructor failed");
		}

		
		/// <summary>
		/// This method repeats a given string any number of times.
		/// </summary>
		/// <param name="StringToRepeat"></param>
		/// <param name="Separator"></param>
		/// <param name="Repeat"></param>
		/// <returns>A string containing the repeated string.</returns>
		protected static string StringRepeater(string StringToRepeat, string Separator, System.Int32 Repeat)
		{
			System.Text.StringBuilder Builder = new System.Text.StringBuilder(string.Empty);
			for (System.Int32 x = 1; x <= Repeat; x++)
			{
				Builder.Append (StringToRepeat);
				if (x < Repeat)
				{
					Builder.Append (Separator);
				}
			}
			return Builder.ToString();
		}

		

		/// <summary>
		/// This delegate allows you to supply a custom condition function. 
		/// On custom test pass return a BOTaxon. 
		/// On custom test failure return null.
		/// </summary>
		protected delegate BO.Taxonomy.BOTaxon CustomConditionDelegate (BO.Taxonomy.BOTaxon Taxon, object[] CustomArgs);
		
		/// <summary>
		/// This is a custom condition function that allows you to specify a not-in list of taxa when retrieving taxa using the GetTaxonDescendant function.
		/// </summary>
		/// <param name="Taxon"></param>
		/// <param name="TaxonListNotIn"></param>
		/// <returns>BOTaxon on succcess or null on failure.</returns>
		protected static BO.Taxonomy.BOTaxon CustomConditionGetTaxonNotIn (BO.Taxonomy.BOTaxon Taxon, object[] TaxonListNotIn)
		{
			bool TaxonInList = false;
			foreach (object CustomArg in TaxonListNotIn)
			{
				BO.Taxonomy.BOTaxon TaxonNotAllowed = (BO.Taxonomy.BOTaxon) CustomArg;
				if (TaxonNotAllowed.Id == Taxon.Id)
				{
					TaxonInList = true;
				}
			}
			if (TaxonInList == false)
			{
				return Taxon;
			}
			else
			{
				return null;
			}
		}

		protected static BO.Taxonomy.BOTaxon CustomConditionGetTaxonHavingUsage (BO.Taxonomy.BOTaxon Taxon, object[] Taxa)
		{
			BO.Taxonomy.BOUsage [] Usages = Taxon.GetUsage ();
			if (Usages.Length > 0)
			{
				return Taxon;
			}
			else
			{
				return null;
			}
		}

		protected static BO.Taxonomy.BOTaxon CustomConditionGetTaxonHavingStock (BO.Taxonomy.BOTaxon Taxon, object[] Taxa)
		{
			BO.Taxonomy.BOTaxon [] Stocks = Taxon.GetStockNames ();
			if (Stocks.Length > 0)
			{
				return Taxon;
			}
			else
			{
				return null;
			}
		}

		protected static BO.Taxonomy.BOTaxon CustomConditionGetTaxonHavingSummaryQuotaAll (BO.Taxonomy.BOTaxon Taxon, object[] Taxa)
		{
			if (Taxon.GetSummaryQuotaAll ().Length > 0)
			{
				return Taxon;
			}
			else
			{
				return null;
			}
		}

		protected delegate BO.Taxonomy.BOTaxon [] CustomConditionTaxaDelegate (BO.Taxonomy.BOTaxon [] Taxa);

		protected static BO.Taxonomy.BOTaxon [] CustomConditionTaxaMultiple (BO.Taxonomy.BOTaxon [] Taxa)
		{
			if (Taxa.Length > 1)
			{
				return Taxa;
			}
			else
			{
				return new BO.Taxonomy.BOTaxon [0];
			}
		}

		/// <summary>
		/// This gets a taxon of the type specified that is a descendant of the supplied Taxon  For example, supply a Kingdom Taxon and specify you want a species - if one exists a species from within the Kingdom will be returned.
		/// </summary>
		/// <param name="Taxon"></param>
		/// <param name="RequiredDescendant"></param>
		/// <returns>A Taxon or null</returns>
		protected static BO.Taxonomy.BOTaxon GetTaxonDescendant (BO.Taxonomy.BOTaxon Taxon, BO.Taxonomy.TaxonTypeEnum RequiredDescendant)
		{
			return GetTaxonDescendant(Taxon, RequiredDescendant, null);
		}

		protected static BO.Taxonomy.BOTaxon GetTaxonDescendant (BO.Taxonomy.BOTaxon Taxon, BO.Taxonomy.TaxonTypeEnum RequiredDescendant, CustomConditionDelegate CustomCondition)
		{
			return GetTaxonDescendant (Taxon, RequiredDescendant, CustomCondition, null); 
		}

		protected static BO.Taxonomy.BOTaxon GetTaxonDescendant (BO.Taxonomy.BOTaxon Taxon, BO.Taxonomy.TaxonTypeEnum RequiredDescendant, CustomConditionDelegate CustomCondition, object[] CustomConditionArgs)
		{
			if (RequiredDescendant != BO.Taxonomy.TaxonTypeEnum.Stock)
			{
				//Check if the supplied Taxon is the one required.
				if (Taxon.TaxonType == RequiredDescendant)
				{
					//Check if there are any custom tests to run.
					if (CustomCondition != null)
					{ 
						return CustomCondition (Taxon, CustomConditionArgs);
					}
					else
					{
						return Taxon;
					}
				}
				else //The supplied taxon is not the one required.
				{
					BO.Taxonomy.BOTaxon[] LowerTaxa = Taxon.GetLowerTaxa();
					BO.Taxonomy.BOTaxon GotTaxon = null;
					//Check that there are children.
					if (LowerTaxa.Length > 0)
					{
						System.Int32 x = 0;
						//Loop all the child taxon until one is returned.
						while (GotTaxon == null & x < LowerTaxa.Length)
						{
							GotTaxon = GetTaxonDescendant (LowerTaxa[x], RequiredDescendant, CustomCondition, CustomConditionArgs);
							x++;
						}
						return GotTaxon;
					}
					else //The supplied taxon does not have any children.
					{
						return null;
					}
				}
			}
			else
			{
				throw new ApplicationException ("Cannot search for Stock name as these do not form part of the hierarchy");
			}
		}
		

		protected static BO.Taxonomy.BOTaxon[] GetTaxonDescendants (BO.Taxonomy.BOTaxon Taxon, BO.Taxonomy.TaxonTypeEnum RequiredDescendant, CustomConditionTaxaDelegate CustomCondition)
		{
			System.Collections.ArrayList BadParents = new System.Collections.ArrayList ();
			BO.Taxonomy.BOTaxon[] Descendants = new BO.Taxonomy.BOTaxon [0];
			BO.Taxonomy.BOTaxon Descendant = GetTaxonDescendant (Taxon, RequiredDescendant);
            //Get the parent of the found required type.
			BO.Taxonomy.BOTaxon DescendantParent = Descendant.GetParentTaxon ();
			bool DescendantsFound = false;
			while (DescendantParent != null & !DescendantsFound)
			{
				//Get the children of the parent.
				Descendants = DescendantParent.GetLowerTaxa ();
				if ((CustomCondition != null && CustomCondition (Descendants).Length == 0) & Descendants.Length == 0)
				{
					//This parent is no good so get another.
					BadParents.Add (DescendantParent);
					BO.Taxonomy.BOTaxon [] BadParentsArray = new BO.Taxonomy.BOTaxon [BadParents.Count];
					BadParents.CopyTo (BadParentsArray);		
					DescendantParent = GetTaxonDescendant (Taxon, DescendantParent.TaxonType, new CustomConditionDelegate(CustomConditionGetTaxonNotIn), BadParentsArray);
				}
				else
				{
					DescendantsFound = true;
				}
			}
			return Descendants;
		}
	}
}
