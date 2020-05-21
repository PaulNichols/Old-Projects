using HBOS.FS.AMP.UPD.Persistence;
using HBOS.FS.AMP.UPD.Types.FundGroups;
using HBOS.FS.AMP.UPD.Types.Lookups;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Controllers
{
	/// <summary>
	/// Controller object for all things FundGroup orientated.  This is used to
	/// keep the logical layers seperate.  
	/// The controller will handle calls to the business and data layers.
	/// </summary>
	public class FundGroupController
	{
		private string m_connectionString;

		#region Constructors

		/// <summary>
		/// Default constructor for Fund Group controller
		/// </summary>
		public FundGroupController(string ConnectionString)
		{
			// Build valid connection string
			m_connectionString = ConnectionString;
		}

		#endregion Constructors

		#region Load Methods

		/// <summary>
		/// Returns the collection of Fund group objects for the passed company
		/// </summary>
		/// <param name="companyCode">The ID of the company for which the fund groups are to be retrieved.</param>
		/// <returns>A collection of matching fund groups.</returns>
		public FundGroupCollection LoadFundGroupsByCompany(string companyCode)
		{
			T.E();
			FundGroupCollection fundGroupColl = null;
			try
			{
				FundGroupPersister persister = new FundGroupPersister(m_connectionString);
				fundGroupColl = persister.LoadFundGroupsByCompany(companyCode);
			}
			finally
			{
				T.X();
			}
			return fundGroupColl;
		}

		/// <summary>
		/// Returns the collection of Fund group objects for the passed company
		/// </summary>
		/// <param name="companyCode">The ID of the company for which the fund groups are to be retrieved.</param>
		/// <param name="fundType">The 1 char code for fund group type- "A" for AssetFund, "I" for Individual (Fund)</param>
		/// <returns>A collection of matching fund groups.</returns>
		public SimpleLookupCollection LoadFundGroupsByCompanyAndTypeLookUp(string companyCode, FundGroupFactory.FundGroupTypes fundType)
		{
			T.E();
			SimpleLookupCollection Coll = null;
			try
			{
				FundGroupLookupPersister persister = new FundGroupLookupPersister(m_connectionString);
				Coll = persister.LoadFundGroupsByCompanyAndTypeLookUp(companyCode, fundType);
			}
			finally
			{
				T.X();
			}
			return Coll;
		}

		/// <summary>
		/// Returns the collection of Fund group objects for the passed company
		/// </summary>
		/// <param name="companyCode">The ID of the company for which the fund groups are to be retrieved.</param>
		/// <param name="fundType">The 1 char code for fund group type- "A" for AssetFund, "I" for Individual (Fund)</param>
		/// <returns>A collection of matching fund groups.</returns>
		public FundGroupCollection LoadFundGroupsByCompanyAndType(string companyCode, FundGroupFactory.FundGroupTypes fundType)
		{
			T.E();
			FundGroupCollection fundGroupColl = null;
			try
			{
				FundGroupPersister persister = new FundGroupPersister(m_connectionString);
				fundGroupColl = persister.LoadFundGroupsByCompanyAndType(companyCode, fundType);
			}
			finally
			{
				T.X();
			}
			return fundGroupColl;
		}

		/// <summary>
		/// Clears the persistance cache of Fund Groups
		/// </summary>
		public static void FlushFundGroups()
		{
			CacheHelper cache=new CacheHelper();
			cache.FlushFundGroups();
		}

		/// <summary>
		/// Loads the fund group lookups by company.
		/// </summary>
		/// <param name="companyCode">Company code.</param>
		/// <returns></returns>
		public SimpleLookupCollection LoadFundGroupLookupsByCompany(string companyCode)
		{
			T.E();
			FundGroupLookupPersister persister = new FundGroupLookupPersister(m_connectionString);
			SimpleLookupCollection result = persister.LoadForCompany(companyCode);
			T.X();
			return result;
		}

		/// <summary>
		/// Loads the fund group lookups associated with the passed asset fund
		/// </summary>
		/// <param name="assetFundID">The unique key of the asset fund</param>
		/// <returns></returns>
		public SimpleLookupCollection LoadFundGroupLookupsByAssetFund(string assetFundID)
		{
			T.E();
			FundGroupLookupPersister persister = new FundGroupLookupPersister(m_connectionString);
			SimpleLookupCollection result = persister.LoadForAssetFund(assetFundID);
			T.X();
			return result;
		}

//		/// <summary>
//		/// Loads the fund group lookups directly associated with the passed fund
//		/// </summary>
//		/// <param name="hiPortfolioCode">unique key of the fund</param>
//		/// <returns></returns>
//		public SimpleLookupCollection LoadFundGroupLookupsByFund(string hiPortfolioCode)
//		{
//			T.E();
//			FundGroupLookupPersister persister = new FundGroupLookupPersister(m_connectionString);
//			SimpleLookupCollection result = persister.LoadForFundCodeDirect(hiPortfolioCode);
//			T.X();
//			return result;
//		}

//		public FundGroupCollection LoadFundGroupByFund(string hiPortfolioCode)
//		{
//			T.E();
//			FundGroupPersister persister = new FundGroupPersister(m_connectionString);
//			FundGroupCollection result = persister.LoadFundGroupsByFundCode(hiPortfolioCode);
//			T.X();
//			return result;
//		}


		/// <summary>
		/// Loads the fund group static data.
		/// </summary>
		/// <param name="fundGroupId">Fund group id.</param>
		/// <returns></returns>
		public FundGroup LoadFundGroupStaticData(int fundGroupId)
		{
			T.E();
			FundGroupStaticDataPersister persister = new FundGroupStaticDataPersister(m_connectionString);
			FundGroup result = persister.Load(fundGroupId);
			T.X();
			return result;
		}

		/// <summary>
		/// Loads the fund group static data for a simple lookup.
		/// </summary>
		/// <param name="lookup">Lookup.</param>
		/// <returns></returns>
		public FundGroup LoadFundGroupStaticData(SimpleLookup lookup)
		{
			return this.LoadFundGroupStaticData(lookup.Key);
		}

		#endregion

		#region Save Methods

		/// <summary>
		/// Save the passed fund group
		/// </summary>
		/// <param name="fundGroup"></param>
		public void SaveFundGroup(FundGroup fundGroup)
		{
			T.E();
			try
			{
				if (!fundGroup.IsDeleted && !fundGroup.ForRelease && fundGroup.DistributionFiles.Count > 0)
				{
					//user must have de-selected ForRelease in which case they cannot attach any distribution
					//files, so remove those distribution files that were previously attached to the fund group
					fundGroup.DistributionFiles.Clear();
				}
				//Save the passed fund group using the persister
				FundGroupStaticDataPersister persister = new FundGroupStaticDataPersister(m_connectionString);
				persister.SaveFundGroup(fundGroup);
			}
			finally
			{
				T.X();
			}
		}

		#endregion

		#region Validation Methods

		/// <summary>
		/// Indicates what type of error occurred 
		/// </summary>
		public enum FundGroupValidationError
		{
			/// <summary>
			/// no error occurred
			/// </summary>
			NoError,
			/// <summary>
			/// a required field was not completed
			/// </summary>
			FieldEmpty,
			/// <summary>
			/// a unique field already exists in db
			/// </summary>
			DuplicateField,
			/// <summary>
			/// only applies to fund group number - must be a valid value (0 (not used), 1-10, or 30)
			/// </summary>
			InvalidValue
		}


		/// <summary>
		/// Validates that a new or updated fundgroup is valid for db save
		/// </summary>
		/// <param name="fndGrp"></param>
		/// <param name="fullNameError"></param>
		/// <param name="shortNameError"></param>
		/// <param name="validFundGroupType"></param>
		/// <returns></returns>
		public bool ValidateFundGroup (FundGroup fndGrp,
			out FundGroupValidationError fullNameError, out FundGroupValidationError shortNameError, 
			out bool validFundGroupType)
		{

			T.E();
			bool isValid = true;
			fullNameError = FundGroupValidationError.NoError;
			shortNameError = FundGroupValidationError.NoError;

			try
			{
				bool fullNameExists = false;
				bool shortNameExists = false;

				string fullName = fndGrp.FullName;
				string shortName = fndGrp.ShortName;

				if ((shortName != null && 
					shortName.Length > 0) ||
					(fullName != null &&
					fullName.Length > 0))
				{

					FundGroupStaticDataPersister persister = new FundGroupStaticDataPersister(m_connectionString);

					persister.CheckForDuplication (fndGrp.ID, fullName, shortName,
						out fullNameExists, out shortNameExists);

				}

				if (fullName == null || fullName.Length == 0)
				{
					fullNameError = FundGroupValidationError.FieldEmpty;
				}
				else if (fullNameExists)
				{
					fullNameError = FundGroupValidationError.DuplicateField;
				}

				if (shortName == null || shortName.Length == 0)
				{
					shortNameError = FundGroupValidationError.FieldEmpty;
				}
				else if (shortNameExists)
				{
					shortNameError = FundGroupValidationError.DuplicateField;
				}

				validFundGroupType = fndGrp is AssetFundGroup || fndGrp is IndividualFundGroup;

				isValid = fullNameError == FundGroupValidationError.NoError &&
					shortNameError == FundGroupValidationError.NoError &&
					validFundGroupType;

			}
			finally
			{
				T.X();
			}

			return isValid;
		}


		#endregion
	}
}