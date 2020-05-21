using System.Data;
using System.Data.SqlClient;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.FundGroups;
using HBOS.FS.AMP.UPD.Types.Funds;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// Summary description for FundPricingPersister.
	/// </summary>
	public class FundPricingPersister : FundPersister
	{

        #region Constructor
        
        /// <summary>
        /// Constructor used to initialise the ConnectionString property.
        /// </summary>
        /// <param name="connectionString">The connection string to use for data access.</param>
        public FundPricingPersister(string connectionString) : base(connectionString)
		{
        	T.E();
            T.X();
		}

        #endregion

        #region Load methods

        /// <summary>
        /// Returns a collection of Fund objects for the given fund group.
        /// </summary>
        /// <param name="fundGroupID">The ID of the fund group.</param>
        /// <returns>All the funds associated with the given fund group.</returns>
        /// <exception cref="InvalidSqlParameterException">Thrown if one of the parameters was of the incorrect value, type or length; or was null</exception>
        /// <exception cref="DatabaseException">Thrown if other unclassified Sql error occurs</exception>
        public FundCollection Load(int fundGroupID)
        {
            T.E();
            T.Log("Load funds for fund group: " + fundGroupID.ToString());

            //cache the asset funds for this fund group
            cacheAssetFunds(fundGroupID);
            
        	SqlParameter[] parameters = new SqlParameter[1];
            
            // Set up the parameters.
            parameters[0] = new SqlParameter("@iFundGroupID", SqlDbType.Int);
            parameters[0].Value = fundGroupID;
            
            // Create the funds collection.
            FundCollection funds = new FundCollection();
            
            const string loadSp = "usp_FundsGetPriceDetailsForFundGroupID";
            
            try
            {
                this.LoadEntityCollection(loadSp, parameters, funds);
            
            }
            catch (SqlException ex)
            {
                ThrowDBException (ex, loadSp, parameters);
            }
            finally
            {
                T.X();
            }

            return funds;
        }

        /// <summary>
        /// Returns a collection of Fund objects for the given company.
        /// </summary>
        /// <param name="companyCode">The code of the company.</param>
        /// <returns>All the funds associated with the given company.</returns>
        /// <exception cref="InvalidSqlParameterException">Thrown if one of the parameters was of the incorrect value, type or length; or was null</exception>
        /// <exception cref="DatabaseException">Thrown if other unclassified Sql error occurs</exception>
        public FundCollection Load(string companyCode)
        {
            T.E();
            T.Log("Load funds for company: " + companyCode);

            //cache the asset funds for this fund group
            cacheAssetFunds(companyCode);
            
            SqlParameter[] parameters = new SqlParameter[1];
            
            // Set up the parameters.
            parameters[0] = new SqlParameter("@cCompanyCode", SqlDbType.Char, 10);
            parameters[0].Value = companyCode;
            
            // Create the funds collection.
            FundCollection funds = new FundCollection();
            
            const string loadSp = "usp_FundsGetPriceDetailsForCompany";
            
            try
            {
                this.LoadEntityCollection(loadSp, parameters, funds);
            
            }
            catch (SqlException ex)
            {
                ThrowDBException (ex, loadSp, parameters);
            }
            finally
            {
                T.X();
            }

            return funds;
        }

		/// <summary>
		/// Returns a collection of Fund objects for the given company.
		/// </summary>
		/// <param name="hiPortfolioCode">The Hi Portfolio Code to load</param>
		/// <returns>All the funds associated with the given company.</returns>
		/// <exception cref="InvalidSqlParameterException">Thrown if one of the parameters was of the incorrect value, type or length; or was null</exception>
		/// <exception cref="DatabaseException">Thrown if other unclassified Sql error occurs</exception>
		public Fund LoadSingleFund(string hiPortfolioCode)
		{
			T.E();
			T.Log("Load fund: " + hiPortfolioCode);
           
			SqlParameter[] parameters = new SqlParameter[1];
            
			// Set up the parameters.
			parameters[0] = new SqlParameter("@cHiPortfolioCode", SqlDbType.Char, 10);
			parameters[0].Value = hiPortfolioCode;
                      
			Fund fund = null;
			
			const string loadSp = "usp_FundsGetPriceDetailsForHiPortfolioCode";
            
			try
			{
				fund = (Fund)this.LoadEntity(loadSp, parameters);           
			}
			catch (SqlException ex)
			{
				ThrowDBException (ex, loadSp, parameters);
			}
			finally
			{
				T.X();
			}

			return fund;
		}


        #endregion

		#region Entity methods

        /// <summary>
        /// Creates a fund index from the supplied data
        /// </summary>
        /// <param name="safeReader">The reader containing the data.</param>
        /// <returns>A new fund object</returns>
        /// <exception cref="SchemaMismatchException">Column missing from data returned by stored procedure</exception>
        protected override object CreateEntity(SafeDataReader safeReader)
        {
            T.E();
            Fund newFund = null;

            try
            {
                // Load the funds associated fund groups
            	FundGroupCollection fundGroups = new FundGroupCollection();//this.LoadFundGroupsForHiportFolioCode( safeReader.GetString("HiPortFolioCode") ) ;
				m_useAssetFundCache=true;
                
				// Load the associated fund groups, making sure it is a lightweight version
                AssetFund assetFund = this.loadAssetFundForFund( safeReader.GetString("AssetFundID") );

				if (AssetMovementConstituentPersister.BenchmarkCache!=null && 
					AssetMovementConstituentPersister.BenchmarkCache.ContainsKey(safeReader.GetString("HiPortFolioCode")))
				{
					newFund=AssetMovementConstituentPersister.BenchmarkCache[safeReader.GetString("HiPortFolioCode")] as Fund;
				}

	
				// Now populate our fund object to the correct level
				if (newFund==null)
				{
					newFund = this.LoadNewFund( safeReader, fundGroups, assetFund, null );
				}

            }
            finally
            {
                T.X();
            }
            return newFund;
        }

        #endregion

        #region ExceptionHandling

        private string additionalErrorText = string.Empty;

        /// <summary>
        /// Gets the concurrency violation exception message.
        /// </summary>
        /// <returns></returns>
        protected override string GetConcurrencyViolationExceptionMessage()
        {
            return string.Format("A concurrency violation occurred whilst saving {0}",additionalErrorText);
        }

        /// <summary>
        /// Gets the constraint violation duplicate key exception message.
        /// </summary>
        /// <returns></returns>
        protected override string GetConstraintViolationDuplicateKeyExceptionMessage()
        {
            return string.Format("A duplicate key violation occurred whilst saving {0}",additionalErrorText);
        }

        /// <summary>
        /// Gets the constraint violation exception message.
        /// </summary>
        /// <returns></returns>
        protected override string GetConstraintViolationExceptionMessage()
        {
            return string.Format("A constraint violation occurred whilst saving {0}",additionalErrorText);
        }

        /// <summary>
        /// Gets the database exception message.
        /// </summary>
        /// <returns></returns>
        protected override string GetDatabaseExceptionMessage()
        {
            return string.Format("A database exception occurred in fund persister. {0}",additionalErrorText);
        }

        /// <summary>
        /// Gets the null parameter exception message.
        /// </summary>
        /// <returns></returns>
        protected override string GetNullParameterExceptionMessage()
        {
            return string.Format("A null parameter was passed to a stored procedure in fund persister. {0}",additionalErrorText);
        }


        #endregion ExceptionHandling
        
        #region Cache methods

        /// <summary>
        /// Load all the Asset fund data so fund loading will be done quicker
        /// </summary>
        private void cacheAssetFunds(string companyCode)
        {
            T.E();
            try
            {
                AssetFundPricingPersister persister = new AssetFundPricingPersister(ConnectionString);
                m_assetFunds = persister.LoadForCompany(companyCode, false);
                m_useAssetFundCache = true;
            }
            finally
            {
                T.X();
            }
        }

        /// <summary>
        /// Load all the Asset fund data so fund loading will be done quicker
        /// </summary>
        private void cacheAssetFunds(int fundGroupID)
        {
            T.E();
            try
            {
                AssetFundPricingPersister persister = new AssetFundPricingPersister(ConnectionString);

				m_assetFunds.Clear();
                m_assetFunds.AddRange(persister.Load(fundGroupID, false));
                m_useAssetFundCache = true;
            }
            finally
            {
                T.X();
            }
        }

        #endregion
	}
}
