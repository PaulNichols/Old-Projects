using System.Data;
using System.Data.SqlClient;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.FundGroups;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// Summary description for AssetFundPricingPersister.
	/// </summary>
	public class AssetFundStaticDataExportPersister:AssetFundPersister
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="connectionString"></param>
		public AssetFundStaticDataExportPersister(string connectionString) : base(connectionString)
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		protected override object CreateEntity(SafeDataReader reader)
		{
			AssetFund newAssetFund = createEntityForStaticDataExport(reader);
			T.E();
			logAssetFundLoad (newAssetFund);            
			T.X();
			return newAssetFund;
		}

		private AssetFund createEntityForStaticDataExport(SafeDataReader reader)
		{
			AssetFund newAssetFund = null;
			T.E();
			try
			{
				//Create a standard object (was previously a light object, but now these are removed),
				//but just set the properties we're interested in and don't bother to load up any other data
				
				string assetFundCode = reader.GetString("assetFundID");

				//also retrieve its fund groups
				FundGroupPersister fgPersister = new FundGroupPersister(this.ConnectionString);
				FundGroupCollection fgColl = fgPersister.LoadFundGroupsByAssetFundCode(assetFundCode);

				AssetFund.AssetFundTypeEnum afType = resolveDBTypeToAssetFundType(reader.GetString("fundType"));
				newAssetFund = new AssetFund(afType, 
					reader.GetString("assetFundID"), 
					reader.GetString("shortName"), 
					reader.GetString("fullName"),
					reader.GetString("companyCode"),
					reader.GetDecimal("assetMovementTolerance"),
					fgColl,
				    loadMovementConstituents(assetFundCode,false));
									

			}
			finally
			{
				T.X();
			}
			return newAssetFund;
		}

	

		/// <summary>
		/// Returns the collection of Asset Fund objects for the given company.
		/// Loads the light version only for static data screen (report)
		/// </summary>
		/// <param name="companyCode">The ID of the company for which the funds are to be retrieved</param>
		/// <returns type="AssetFundCollection">A collection of asset funds for the company</returns>
		/// <exception cref="DatabaseException">Unable to load item</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		public  AssetFundCollection LoadForCompany(string companyCode)
		{
			// This method handles the call to the vlh_AssetFundsForStaticDataExportByCompanyCode stored proc

			T.E();
			AssetFundCollection assetFunds = new AssetFundCollection();
			const string loadSp = "usp_AssetFundsForStaticDataExportByCompanyCode";
			SqlParameter[] parameters = new SqlParameter[1];

			try
			{
				// Set up the parameters.
				parameters[0] = new SqlParameter("@sCompanyCode", SqlDbType.VarChar, 10); 
				parameters[0].Value = companyCode;

				this.LoadEntityCollection(loadSp, parameters, assetFunds);
			}
			finally
			{
				T.X();
			}
			return assetFunds;


		}

	}
}
