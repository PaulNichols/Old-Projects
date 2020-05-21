using System.Data.SqlClient;
using HBOS.FS.AMP.UPD.Types.Lookups;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// Summary description for FundLookUpPersister.
	/// </summary>
	public class FundLookUpPersister : FundPersister
	{
		/// <summary>
		/// Creates a new <see cref="FundLookUpPersister"/> instance.
		/// Constructor used to initialise the ConnectionString property.
		/// </summary>
		/// <param name="connectionString">The connection string to use for data access.</param>
		public FundLookUpPersister(string connectionString) : base(connectionString)
		{
		}

		/// <summary>
		/// Loads a lookup list of funds for the specified company
		/// </summary>
		/// <param name="companyCode">Company code.</param>
		/// <returns></returns>
		public SimpleStringLookupCollection LoadForCompany(string companyCode)
		{
			T.E();
			SimpleStringLookupCollection lookupList = new SimpleStringLookupCollection();

			SqlParameter[] parms = new SqlParameter[1];
			parms[0] = new SqlParameter("@sCompanyCode", companyCode);

			this.LoadEntityCollection("usp_FundGetLookupsForCompanyCode", parms, lookupList);

			T.X();
			return lookupList;
		}

		/// <summary>
		/// Creates the lookup item from the data
		/// </summary>
		/// <param name="safeReader">Safe reader.</param>
		/// <returns></returns>
		protected override object CreateEntity(SafeDataReader safeReader)
		{
			return new SimpleStringLookup(
				safeReader.GetString("HiPortFolioCode"),
				safeReader.GetString("shortName"));
		}


	}
}