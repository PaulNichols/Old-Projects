using System.Data.SqlClient;
using HBOS.FS.AMP.UPD.Types.FundGroups;
using HBOS.FS.AMP.UPD.Types.Lookups;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// Summary description for FundGroupLookupPersister.
	/// </summary>
	public class FundGroupLookupPersister : FundGroupPersister
	{
		/// <summary>
		/// Creates a new <see cref="FundGroupLookupPersister"/> instance.
		/// </summary>
		public FundGroupLookupPersister(string connectionString) : base(connectionString)
		{
		}

		/// <summary>
		/// Loads a lookup list of fund groups for the specified company
		/// </summary>
		/// <param name="companyCode">Company code.</param>
		/// <returns></returns>
		public SimpleLookupCollection LoadForCompany(string companyCode)
		{
			T.E();

			CacheHelper cacheHelper =new CacheHelper();
			return (SimpleLookupCollection)cacheHelper.GetFundGroups(ConnectionString,companyCode);

			//return (SimpleLookupCollection);

//			SimpleLookupCollection result = new SimpleLookupCollection();
//
//			SqlParameter[] parms = new SqlParameter[1];
//			parms[0] = new SqlParameter("@sCompanyCode", companyCode);
//
//			this.LoadEntityCollection("usp_FundGroupsGetLookupsForCompanyCode", parms, result);
//
//			T.X();
//			return result;
		}

		/// <summary>
		/// Loads a lookup list of fund groups associated with the passed asset fund
		/// </summary>
		/// <param name="assetFundID">Unique key of the asset fund</param>
		/// <returns></returns>
		public SimpleLookupCollection LoadForAssetFund(string assetFundID)
		{
			T.E();
			SimpleLookupCollection result = new SimpleLookupCollection();

			SqlParameter[] parms = new SqlParameter[1];
			parms[0] = new SqlParameter("@cAssetFundID", assetFundID);

			this.LoadEntityCollection("usp_FundGroupsGetLookupsForAssetFundID", parms, result);

			T.X();
			return result;
		}

		/// <summary>
		/// Returns the collection of Fund group objects for the passed company
		/// </summary>
		/// <param name="companyCode">The ID of the company for which the fund groups are to be retrieved.</param>
		/// <param name="fundType">The 1 char code for fund group type- "A" for AssetFund, "I" for Individual (Fund)</param>
		/// <returns>A lookup collection of matching fund groups.</returns>
		public SimpleLookupCollection LoadFundGroupsByCompanyAndTypeLookUp(string companyCode, FundGroupFactory.FundGroupTypes fundType)
		{
			T.E();

			return (SimpleLookupCollection)base.LoadFundGroupsByCompanyAndType(companyCode,fundType);
//			SimpleLookupCollection result = new SimpleLookupCollection();
//
//			try
//			{
//				//set the company code to be picked up in CreateEntity
//
//				SqlParameter[] spParams = new SqlParameter[2];
//
//				spParams[0] = new SqlParameter("@sCompanyCode", SqlDbType.VarChar, 10);
//				spParams[0].Value = companyCode;
//
//				spParams[1] = new SqlParameter("@cContainsType", SqlDbType.Char, 1);
//				spParams[1].Value = fundType;
//
//
//				this.LoadEntityCollection("usp_FundGroupsGetForCompanyCodeByType", spParams, result);
//			}
//			catch
//			{
//			}
//			return result;

		}

		/// <summary>
		/// Creates the lookup item from the data
		/// </summary>
		/// <param name="safeReader">Safe reader.</param>
		/// <returns></returns>
		protected override object CreateEntity(SafeDataReader safeReader)
		{
			return new SimpleLookup(safeReader.GetInt32("fundGroupId"),
			                        safeReader.GetString("shortName"));
		}

	}
}