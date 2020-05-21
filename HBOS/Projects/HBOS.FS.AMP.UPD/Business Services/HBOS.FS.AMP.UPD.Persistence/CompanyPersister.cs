using System;
using System.Data;
using System.Data.SqlClient;
using HBOS.FS.AMP.UPD.Types.Companies;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.Entities;
using HBOS.FS.AMP.UPD.Types.Lookups;
using HBOS.FS.Support.Tex;
using Microsoft.ApplicationBlocks.Data;

namespace HBOS.FS.AMP.UPD.Persistence
{
    /// <summary>
    /// The class to use for persisting or retrieving Company objects and
    /// CompanyCollection objects.
    /// </summary>
    public class CompanyPersister : EntityPersister
    {

        #region Local variables

        #endregion 

        #region Constructors
        /// <summary>
        /// Constructor used to initialise the ConnectionString property.
        /// </summary>
        /// <param name="connectionString"></param>
        public CompanyPersister(string connectionString) : base(connectionString)
        {}

        #endregion

        #region Methods

		/// <summary>
		/// Progresses the company valuation day.
		/// </summary>
		/// <param name="companyCode">The code of the company to prgress the valuation day of.</param>
		public void ProgressCompanyValuationDay(string companyCode)
		{
			T.E();
			SqlParameter[] spParams = null;
			const string updateSp = "usp_CompanyPricingDayAdvance";
			try
			{
				// Create paaremeter object
				spParams = new SqlParameter[1];

				// Build parameters
				spParams[0] = new SqlParameter( "@CompanyCode", SqlDbType.Char, 10 );

				// Assign values to parameters
				spParams[0].Value = companyCode;

				// Call update stored procedure
				SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, updateSp, spParams);

			}
			catch (SqlException ex)
			{
				switch ( ex.Number )
				{
					case (int)DatabaseError.ConstraintViolation:
						throw new ConstraintViolationException( String.Format( "Failed to progress the Valuation day, the Valuation Day cannot be in the future."  ) , ConnectionString,updateSp,spParams, ex );
					default:
						throw new DatabaseException("Failed to progress the Valuation day." , ConnectionString,  ex);
				}
			}
			finally
			{
				T.X();
			}
		}


        /// <summary>
        /// Loads a company object from the database for a given company code
        /// </summary>
        /// <param name="companyCode">The company code for the company to load</param>
        /// <returns>The requested company object</returns>
		/// <exception cref="DatabaseException">Unable to load company</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="SchemaMismatchException">Column missing from data returned by stored procedure</exception>
		/// <exception cref="ArgumentException">Incorrect type loaded from base class</exception>
		public Company LoadCompany(string companyCode)
        {
			T.E();			
            SqlParameter[] spParams = new SqlParameter[1];
			Company newCompany = null;
			const string loadSp = "usp_HBOSCompanyGetForCompanyCode";

			try
			{
				// Select by parameter
				spParams[0] = new SqlParameter("@sCompanyCode", SqlDbType.VarChar, 10 ); 
				spParams[0].Value = companyCode;

				newCompany = this.LoadEntity(loadSp, spParams) as Company;
				if (newCompany == null) throw new ArgumentException("Expecting Company from base load, but no object or object of invalid type returned");

			}
			catch (SqlException ex)
			{
				ThrowDBException (ex, loadSp, spParams);
			}
			finally
			{
				T.X();
			}
			return newCompany;
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="companyName"></param>
		/// <returns></returns>
        public Company LoadCompanyByName(string companyName)
        {
			T.E();			
			SqlParameter[] spParams = new SqlParameter[1];
			Company newCompany = null;
			const string loadSp = "usp_HBOSCompanyGetForCompanyName";

			try
			{
				// Select by parameter
				spParams[0] = new SqlParameter("@sCompanyName", SqlDbType.VarChar, 50 ); 
				spParams[0].Value = companyName;

				newCompany = this.LoadEntity(loadSp, spParams) as Company;
				if (newCompany == null) throw new ArgumentException("Expecting Company from base load, but no object or object of invalid type returned");

			}
			catch (SqlException ex)
			{
				ThrowDBException (ex, loadSp, spParams);
			}
			finally
			{
				T.X();
			}
			return newCompany;
        }
		
		/// <summary>
        /// Gets all the companies available to the specified user
        /// </summary>
        /// <returns>A strongly-typed collection of the user's companies</returns>
		/// <exception cref="DatabaseException">Unable to load companies</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="SchemaMismatchException">Column missing from data returned by stored procedure</exception>
		/// <exception cref="ArgumentException">Incorrect type loaded from base class</exception>
		public SimpleStringLookupCollection LoadCompanies()
        {
			T.E();
			const string loadSp = "usp_HBOSCompanyListForUser";
			// Create the companies collection.
            SimpleStringLookupCollection companies = new SimpleStringLookupCollection();

			try
			{
				//todo - ok to pass null here?
				this.LoadEntityCollection(loadSp, companies);

			}
			catch (SqlException ex)
			{
				ThrowDBException (ex, loadSp);
			}
			finally
			{
				T.X();
			}

            return companies;
        }

        /// <summary>
        /// Gets the import directory for the supplied company code
        /// </summary>
        /// <param name="CompanyCode">Company Code</param>
        /// <returns>Import Directory</returns>
        public string GetImportDirectory(string CompanyCode)
        {
            T.E();			
            SqlParameter[] spParams = new SqlParameter[1];
            string importDirectory = string.Empty;
            const string loadSp = "usp_HBOSCompanyGetForCompanyCode";

            try
            {
                // Select by parameter
                spParams[0] = new SqlParameter("@sCompanyCode", SqlDbType.VarChar, 10 ); 
                spParams[0].Value = CompanyCode;

                SqlDataReader sr = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, loadSp,
                    spParams);

                if (!sr.IsClosed && sr.Read())
                    importDirectory = sr["defaultImportSource"].ToString();
                else
                  throw new ArgumentException("Company not found");

            }
            catch (SqlException ex)
            {
                ThrowDBException (ex, loadSp, spParams);
            }
            finally
            {
                T.X();
            }
            return importDirectory;
        }

        /// <summary>
        /// Saves a company to the database.
        /// </summary>
        /// <param name="company">The company to save</param>
        /// <returns>True if successful, otherwise false</returns>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public void SaveCompany(Company company)
        {
			T.E();
			try
			{
				if (company.IsDirty || company.IsNew)
				{
					this.SaveEntity(company);
				}
			}
			finally
			{
				T.X();
			}
        }

        /// <summary>
        /// Saves a collection of companies to the database
        /// </summary>
        /// <param name="companies">The collection of companies to save</param>
        /// <returns>True if successful, otherwise false</returns>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public bool SaveCompanies(CompanyCollection companies)
        {
            bool retval = false;
			T.E();
			try
			{
				this.SaveEntityCollection (companies);
			}
			finally
			{
				T.X();
			}
            return retval;
        }

        #endregion

        #region Private methods

		/// <summary>
		/// Insert a new company into the database
		/// </summary>
		/// <param name="transaction">The transaction used for the persistence, allowing transactional rollback.</param>
		/// <param name="entity">Persitee user object</param>
		/// <returns>Success flag</returns>
		/// <returns>True if the save is successful.</returns>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		/// <exception cref="ArgumentNullException">Entity was passed in as a null</exception>
		/// <exception cref="ArgumentException">Invalid entity type returned</exception>
		protected override void InsertEntity(IEntityBase entity, SqlTransaction transaction)
		{
			T.E();
			SqlParameter[] spParams = null;
			const string createSp = "usp_HBOSCompanyCreate";
			try
			{
				if (entity == null) throw new ArgumentNullException("entity", "Cannot insert null Company");
				if (!(entity is Company)) throw new ArgumentException("Incorrect type, expecting Company, was "+entity.GetType().ToString(), "entity");
				Company company = (Company) entity;

				// Create paaremeter object
				spParams = new SqlParameter[2];

				// Build parameters
				spParams[0] = new SqlParameter( "@sCompanyCode", SqlDbType.Char, 10 );
				spParams[1] = new SqlParameter( "@sCompanyName", SqlDbType.Char, 50 );

				// Assign values to parameters
				spParams[0].Value = company.CompanyCode;
				spParams[1].Value = company.CompanyName;

				// Call create stored procedure
				SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, createSp, spParams);

			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Update a company in the database
		/// </summary>
		/// <param name="transaction">The transaction used for the persistence, allowing transactional rollback.</param>
		/// <param name="entity">Persitee user object</param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		/// <exception cref="ArgumentNullException">Entity was passed in as a null</exception>
		/// <exception cref="ArgumentException">Invalid entity type returned</exception>
		protected override void UpdateEntity(IEntityBase entity, SqlTransaction transaction)
		{
			T.E();
			SqlParameter[] spParams = null;
			const string updateSp = "usp_HBOSCompanyUpdate";
			try
			{
				if (entity == null) throw new ArgumentNullException("entity", "Cannot insert null Company");
				if (!(entity is Company)) throw new ArgumentException("Incorrect type, expecting Company, was "+entity.GetType().ToString(), "entity");
				Company company = (Company) entity;

				// Create paaremeter object
				spParams = new SqlParameter[4];

				// Build parameters
				spParams[0] = new SqlParameter( "@sCompanyCode", SqlDbType.Char, 10 );
				spParams[1] = new SqlParameter( "@sCompanyName", SqlDbType.Char, 50 );
				spParams[2] = new SqlParameter( "@bDeleted", SqlDbType.Bit, 1 );
				spParams[3] = new SqlParameter( "@ts", SqlDbType.Timestamp, 8 );

				// Assign values to parameters
				spParams[0].Value = company.CompanyCode;
				spParams[1].Value = company.CompanyName;
				spParams[2].Value = company.IsDeleted;
				spParams[3].Value = company.TimeStamp;

				// Call update stored procedure
				SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, updateSp, spParams);

			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Creates a company index from the supplied data
		/// </summary>
		/// <param name="reader">The reader containing the data.</param>
		/// <returns></returns>
		/// <exception cref="SchemaMismatchException">Column missing from data returned by stored procedure</exception>
		protected override object CreateEntity(SafeDataReader reader)
		{
			T.E();
			Company newCompany = null;
			try
			{
				newCompany = new Company(
					reader.GetString("companyCode"),
					reader.GetString("companyName"),
					reader.GetDateTime("activepricingDay"),
					reader.GetDateTime("valuationPoint"),
					reader.ColumnExists("nextPricingDay")?reader.GetDateTime("nextPricingDay"):DateTime.MinValue,
					reader.GetTimestamp("ts"),
					reader.ColumnExists("previousPricingDay")?reader.GetDateTime("previousPricingDay"):DateTime.MinValue
					);
			}
			finally
			{
				T.X();
			}
			return newCompany;
		}

        #endregion

    }
}
