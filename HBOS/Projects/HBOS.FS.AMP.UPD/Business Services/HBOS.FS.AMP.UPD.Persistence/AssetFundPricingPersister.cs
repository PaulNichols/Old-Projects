using System;
using System.Data;
using System.Data.SqlClient;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.Currency;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// Summary description for AssetFundPricingPersister.
	/// </summary>
	public class AssetFundPricingPersister : AssetFundPersister
	{

		private CurrencyPricingPersister m_currencyPersister = null;
		private AssetMovementConstituentCollection assetMovementConstituents;

		#region Constructor
		/// <summary>
		/// Creates a new <see cref="AssetFundPricingPersister"/> instance.
		/// </summary>
		/// <param name="connectionString">Connection string.</param>
		public AssetFundPricingPersister(string connectionString) : base(connectionString)
		{
		}

		#endregion

		#region Public methods
		/// <summary>
		/// Loads a particular asset funds pricing data
		/// </summary>
		/// <param name="assetFundCode">The Hi3 code (??) for the asset fund</param>
		/// <param name="currentFactors">Indicates whether to load the current factors</param>
		/// <returns>The requested asset fund</returns>
		/// <exception cref="DatabaseException">Unable to load item</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		public AssetFund Load(string assetFundCode, bool currentFactors)
		{
			AssetFundCollection assetFunds;

			T.E();
			try
			{
				assetFunds = loadDetails(assetFundCode, 0, string.Empty);

				// Check that there is one and only one.
				if (assetFunds.Count != 1)
				{
					throw new DatabaseException("The Asset Fund object was not found.");
				}
			}
			finally
			{
				T.X();
			}
			return assetFunds[0];
		}

		/// <summary>
		/// Returns the collection of Fund objects for the given fund group id.
		/// Loads the asset fund pricing data
		/// </summary>
		/// <param name="fundGroupID">The ID of the fund group for which the funds are to be retrieved</param>
		/// <param name="currentWeightings">Indicates whether to load the current weightings</param>
		/// <returns type="AssetFundCollection">A collection of asset funds for the fund group</returns>
		/// <exception cref="DatabaseException">Unable to load item</exception>
		public AssetFundCollection Load(int fundGroupID, bool currentWeightings)
		{
			AssetFundCollection assetFunds;
			T.E();
			try
			{
				assetMovementConstituents=loadMovementConstituentsByGroup(fundGroupID);
				assetFunds = loadDetails(string.Empty, fundGroupID, string.Empty);
			}
			finally
			{
				T.X();
			}

			return assetFunds;
		}

	
		/// <summary>
		/// Returns the collection of Asset Fund objects for the given company.
		/// Loads the Asset Fund pricing data
		/// </summary>
		/// <param name="companyCode">The ID of the company for which the funds are to be retrieved</param>
		/// <param name="currentWeightings">Indicates whether to load the current weightings</param>
		/// <returns type="AssetFundCollection">A collection of asset funds for the company</returns>
		/// <exception cref="DatabaseException">Unable to load item</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		public AssetFundCollection LoadForCompany(string companyCode, bool currentWeightings)
		{
			AssetFundCollection assetFunds;

			assetFunds = loadDetails(string.Empty, 0, companyCode);

			return assetFunds;
		}

		#endregion

		#region Entity methods
		/// <summary>
		/// Creates a user from the supplied data
		/// </summary>
		/// <param name="reader">The reader containing the data.</param>
		/// <returns></returns>
		protected override object CreateEntity(SafeDataReader reader)
		{
			AssetFund newAssetFund = createDetailsEntity(reader);
			T.E();
#if (DEBUG)
		{
			logAssetFundLoad(newAssetFund);
		}
#endif
			T.X();
			return newAssetFund;
		}

		#endregion

		#region Private methods
		/// <summary>
		/// This method returns a collection of asset funds based upon a particualr key
		/// Used for the light object collection (no weithing collections etc)
		/// </summary>
		/// <param name="assetFundCode"></param>
		/// <param name="fundGroupID"></param>
		/// <param name="companyCode"></param>
		/// <returns></returns>
		/// <exception cref="DatabaseException">Unable to load items</exception>
		private AssetFundCollection loadDetails(string assetFundCode, int fundGroupID, string companyCode)
		{
			// This method handles the call to the usp_AssetFundDetailsForAssetFundID
			// This saves repeating it in different places when it is doing the same thing

			T.E();
			string loadSp = "usp_AssetFundsGetPriceDetailsForCompany";

			// Create an instance of the currency pricing persister
			if (null == m_currencyPersister)
			{
				m_currencyPersister = new CurrencyPricingPersister(this.ConnectionString);
				m_currencyPersister.AllowGlobalMarket = false;
			}

			AssetFundCollection assetFunds = new AssetFundCollection();
			SqlParameter[] parameters = new SqlParameter[1];
			try
			{
				// Set up the parameters.
				if (assetFundCode.Length > 0)
				{
					loadSp = "usp_AssetFundsGetPriceDetailsForAssetFundID";
					parameters[0] = new SqlParameter("@cAssetFundID", SqlDbType.Char, 8);
					parameters[0].Value = assetFundCode;
				}
				else if (fundGroupID > 0)
				{
					loadSp = "usp_AssetFundsGetPriceDetailsForFundGroupID";
					parameters[0] = new SqlParameter("@iFundGroupID", SqlDbType.Int);
					parameters[0].Value = fundGroupID;
				}
				else
				{
					parameters[0] = new SqlParameter("@cCompanyCode", SqlDbType.VarChar, 10);
					parameters[0].Value = companyCode;
				}

				this.LoadEntityCollection(loadSp, parameters, assetFunds);
			}
			finally
			{
				T.X();
			}
			return assetFunds;

		}
		
		/// <summary>
		/// Creates the pricing detail asset fund entity object.
		/// </summary>
		/// <param name="reader">Reader.</param>
		/// <returns></returns>
		private AssetFund createDetailsEntity(SafeDataReader reader)
		{
			T.E();
			AssetFund newAssetFund = null;
			try
			{
				//retrieve fund type from db and create appropriate asset fund type based on this.
				AssetFund.AssetFundTypeEnum assetFundType = resolveDBTypeToAssetFundType(reader.GetString("fundType"));
				string assetFundID = reader.GetString("assetFundID");

				//Create objects representing the Weighted Indices Values related to this Asset Fund
				if (assetMovementConstituents==null) assetMovementConstituents= loadMovementConstituents(assetFundID,true);

				// Get the current and previous currency rates for the asset fund
				Currency marketCurrency = m_currencyPersister.LoadRates(reader);

				//TODO set auth price and auth price set

				try
				{
					newAssetFund = new AssetFund(assetFundType, assetFundID,
					                             reader.GetString("shortName"), reader.GetString("fullName"), reader.GetString("CompanyCode"), reader.GetDecimal("assetUnitPrice"),
					                             reader.GetDateTime("assetUnitPriceValuationPoint"),reader.GetDecimal("assetUnitPriceMovement"), reader.GetDecimal("previousAssetUnitPrice"), !reader.IsNull("previousAssetUnitPrice"),
					                             0M, false,reader.GetBoolean("assetFundLocked") ,reader.GetDecimal("assetMovementTolerance"),
					                             !reader.IsNull("assetUnitPrice"),!reader.IsNull("assetUnitPriceMovement"),!reader.IsNull("assetMovementTolerance"), !reader.IsNull("assetUnitPriceValuationPoint"),
					                             reader.GetTimestamp("ts"),assetMovementConstituents, marketCurrency,
					                             reader.GetInt64("benchmarkSplitSnapshotID"),!reader.IsNull("benchmarkSplitSnapshotID"),
					                             reader.GetInt64("currencyRateSnapshotID"),!reader.IsNull("currencyRateSnapshotID"),
					                             reader.GetInt64("marketIndiciesSnapshotID"),!reader.IsNull("marketIndiciesSnapshotID"));
				}
				catch (Exception e)
				{
					Console.Write(e);
				}
			}
			finally
			{
				T.X();
			}
			return newAssetFund;
		}

		private AssetMovementConstituentCollection loadMovementConstituentsByGroup(int groupId)
		{
			T.E();
			AssetMovementConstituentPersister persister = new AssetMovementConstituentPersister(this.ConnectionString);
			AssetMovementConstituentCollection result = persister.LoadAssetFundMovementConstituentsByGroup(groupId);
			T.X();
			return result;
		}

		#endregion



	}
}