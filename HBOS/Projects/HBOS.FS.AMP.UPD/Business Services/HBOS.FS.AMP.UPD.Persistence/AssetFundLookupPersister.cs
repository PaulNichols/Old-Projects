using System;
using System.Data;
using System.Data.SqlClient;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.Lookups;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// Summary description for AssetFundLookupPersister.
	/// </summary>
	public class AssetFundLookupPersister : AssetFundPersister
	{
		/// <summary>
		/// Creates a new <see cref="AssetFundLookupPersister"/> instance.
		/// Constructor used to initialise the ConnectionString property.
		/// </summary>
		/// <param name="connectionString">The connection string to use for data access.</param>
		public AssetFundLookupPersister(string connectionString) : base(connectionString) 
		{
		}

		/// <summary>
		/// Loads the asset funds releated to this price file.
		/// </summary>
		/// <param name="priceFileId">Price file id.</param>
		/// <returns></returns>
		public SimpleLookupCollection LoadAssetFunds(int priceFileId)
		{
			T.E();
			SimpleLookupCollection lookupList = null;
			try
			{
				if (priceFileId<0)
				{
					throw new ArgumentException ("Invalid Id");
				}
				lookupList = new SimpleLookupCollection();

				SqlParameter[] parms = new SqlParameter[1];
				parms[0] = new SqlParameter("@PriceFileId", SqlDbType.Int);
				parms[0].Value = priceFileId;
			
				this.LoadEntityCollection("usp_AssetFundsGetLookupsForPriceFile", parms, lookupList);
			}
			finally
			{
				T.X();
			}
			return lookupList;
		}

		/// <summary>
		/// Loads a lookup list of funds for the specified company
		/// </summary>
		/// <param name="companyCode">Company code.</param>
		/// <returns></returns>
		/// <exception cref="DatabaseException">Unable to load item</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ArgumentException">Invalid company code</exception>
		public  SimpleStringLookupCollection LoadForCompany(string companyCode)
		{
			T.E();
			SimpleStringLookupCollection lookupList = null;
			try
			{
				if (companyCode == null || companyCode.Length > 10 || companyCode.Length == 0)
				{
					throw new ArgumentException ("Invalid company code");
				}
				lookupList = new SimpleStringLookupCollection();

				SqlParameter[] parms = new SqlParameter[1];
				parms[0] = new SqlParameter("@sCompanyCode", SqlDbType.VarChar, 10);
				parms[0].Value = companyCode;
			
				this.LoadEntityCollection("usp_AssetFundsGetLookupsForCompanyCode", parms, lookupList);
			}
			finally
			{
				T.X();
			}
			return lookupList;
		}

		/// <summary>
		/// Loads a lookup list of funds for the specified company
		/// </summary>
		/// <param name="companyCode">Company code.</param>
		/// <param name="assetFundType">Type of asset fund (Composite, Linked, or OEIC)</param>
		/// <returns></returns>
		/// <exception cref="DatabaseException">Unable to load item</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ArgumentException">Invalid company code</exception>
		public SimpleStringLookupCollection LoadForCompany(string companyCode, AssetFund.AssetFundTypeEnum assetFundType)
		{
			T.E();
			SimpleStringLookupCollection lookupList = null;
			try
			{
				if (companyCode == null || companyCode.Length > 10 || companyCode.Length == 0)
				{
					throw new ArgumentException ("Invalid company code");
				}
				lookupList = new SimpleStringLookupCollection();

				SqlParameter[] parms = new SqlParameter[2];
				parms[0] = new SqlParameter("@sCompanyCode", SqlDbType.VarChar, 10);
				parms[0].Value = companyCode;

				parms[1] = new SqlParameter("@cFundType", SqlDbType.Char, 1);
				parms[1].Value = AssetFundPersister.resolveAssetFundTypeToDBType(assetFundType);
			
				this.LoadEntityCollection("usp_AssetFundsGetLookupsForCompanyCodeByType", parms, lookupList);
			}
			finally
			{
				T.X();
			}
			return lookupList;
		}

		/// <summary>
		/// Creates the lookup item from the data
		/// </summary>
		/// <param name="safeReader">Safe reader.</param>
		/// <returns></returns>
		protected override object CreateEntity(SafeDataReader safeReader)
		{
			return new SimpleStringLookup(safeReader.GetString("assetFundID"),
				safeReader.GetString("shortName"));
		}

	}
}
