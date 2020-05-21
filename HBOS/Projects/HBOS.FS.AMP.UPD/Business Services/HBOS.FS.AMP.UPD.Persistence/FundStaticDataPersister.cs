using System;
using System.Data;
using System.Data.SqlClient;
using HBOS.FS.AMP.Entities;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Types.FundGroups;
using HBOS.FS.AMP.UPD.Types.Funds;
using HBOS.FS.Support.Tex;
using Microsoft.ApplicationBlocks.Data;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// Data persister object used to move fund data asociated with static data
	/// maintenance to and from the database.
	/// </summary>
	public class FundStaticDataPersister : FundPersister
	{
		#region Local variables

		private enum loadType : int
		{
			loadFund = 0,
			loadForAssetFund,
			loadForCompany
		}

		private loadType m_createEntityType = loadType.loadFund;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor used to initialise the ConnectionString property.
		/// </summary>
		/// <param name="connectionString">The connection string to use for data access.</param>
		public FundStaticDataPersister(string connectionString) : base(connectionString)
		{
			//
			// TODO: Add constructor logic here
			//
			T.E();
			T.X();
		}

		#endregion

		#region Load Methods

		/// <summary>
		/// Determines if the Fund is being used as a benchmark by any Asset Funds
		/// </summary>
		/// <param name="fund">Fund to check.</param>
		/// <returns></returns>
		public bool CurrentlyABenchmark(Fund fund)
		{
			T.E();
			int returnCount = 0;
			const string loadSp = "usp_FundNumberOfTimesUsedAsABenchmark";
			SqlParameter[] parameters = new SqlParameter[1];
			try
			{
				using (SqlConnection connection = new SqlConnection(this.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand())
					{
						parameters[0] = new SqlParameter("@HiPortFolioCode", SqlDbType.Char, 10);
						parameters[0].Value = fund.HiPortfolioCode;
						command.Parameters.Add(parameters[0]);

						command.Connection = connection;
						command.CommandText = loadSp;
						command.CommandType = CommandType.StoredProcedure;
						connection.Open();
						returnCount = Convert.ToInt32(command.ExecuteScalar());
						connection.Close();
					}
				}

			}
			catch (SqlException ex)
			{
				ThrowDBException(ex, loadSp);
			}
			finally
			{
				T.X();
			}
			return returnCount > 0;
		}

		/// <summary>
		/// Retrieves a fund matching the given HiPortfolio code.
		/// </summary>
		/// <param name="hiPortfolioCode">The code for the required fund.</param>
		/// <returns>
		/// The matching fund object populated with the factors 
		/// and tolerances associated with its authorised price if it has one 
		/// or the latest imports if not.
		/// The returned Fund object will also hold fund group and external systems
		/// objects.
		/// </returns>
		public Fund Load(string hiPortfolioCode)
		{
			T.E();
			const string spName = "usp_FundGetStaticData";
			SqlParameter[] spParameters = new SqlParameter[1];

			m_createEntityType = loadType.loadFund;

			// Set up the stored procedure parameters.
			spParameters[0] = new SqlParameter("@cHiPortfolioCode", SqlDbType.Char, 10);
			spParameters[0].Value = hiPortfolioCode;

			// Create the fund object.
			Fund fund = null;

			try
			{
				fund = (Fund) this.LoadEntity(spName, spParameters);

				// Test for valid object
				if (fund == null)
				{
					throw new ArgumentException(string.Format("Failed to load fund with HiPortfolio code '{0}'", hiPortfolioCode));
				}
			}
			catch (SqlException ex)
			{
				ThrowDBException(ex, spName, spParameters);
			}
			T.X();
			return fund;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="companyCode"></param>
		/// <returns></returns>
		public FundCollection LoadForCompany(string companyCode)
		{
			T.E();
			const string loadSp = "usp_FundGetStaticDataForCompanyCode";

			// Make sure we create the correct type of fund object
			m_createEntityType = loadType.loadForCompany;

			// Set up store proc parameters
			SqlParameter[] parameters = new SqlParameter[1];
			parameters[0] = new SqlParameter("@sCompanyCode", SqlDbType.VarChar, 10);
			parameters[0].Value = companyCode;

			// Create the funds collection.
			FundCollection funds = new FundCollection();

			try
			{
				// Now populate, this will invoke CreateEntity()
				this.LoadEntityCollection(loadSp, parameters, funds);
			}
			catch (SqlException ex)
			{
				ThrowDBException(ex, loadSp, parameters);
			}
			finally
			{
				T.X();
			}

			return funds;
		}

		/// <summary>
		/// Load a collection of funds for a specified asset fund
		/// </summary>
		/// <param name="assetFundCode">Asset fund code</param>
		/// <returns>Fund collection object</returns>
		public FundCollection LoadForAssetFund(string assetFundCode)
		{
			T.E();
			const string loadSp = "usp_FundGetStaticDataForAssetFundID";

			// Make sure we create the correct type of fund object
			m_createEntityType = loadType.loadForAssetFund;

			// Set up store proc parameters
			SqlParameter[] parameters = new SqlParameter[1];
			parameters[0] = new SqlParameter("@cAssetFundID", SqlDbType.Char, 8);
			parameters[0].Value = assetFundCode;

			// Create the funds collection.
			FundCollection funds = new FundCollection();

			try
			{
				// Now populate, this will invoke CreateEntity()
				this.LoadEntityCollection(loadSp, parameters, funds);
			}
			catch (SqlException ex)
			{
				ThrowDBException(ex, loadSp, parameters);
			}
			finally
			{
				T.X();
			}

			return funds;
		}

		#endregion

		#region Save methods

		/// <summary>
		/// Checks that no other fund exists with this fundid, fullname or shortname
		/// </summary>
		/// <param name="fund"></param>
		/// <param name="hiPortfolioCodeExists"></param>
		/// <param name="securityCodeExists"></param>
		/// <param name="fullNameExists"></param>
		/// <param name="shortNameExists"></param>
		public void CheckFundExistence(Fund fund, out bool hiPortfolioCodeExists, out bool securityCodeExists, out bool fullNameExists, out bool shortNameExists)
		{
			T.E();
			SqlParameter[] parameters = null;
			hiPortfolioCodeExists = false;
			fullNameExists = false;
			shortNameExists = false;
			securityCodeExists = false;
			try
			{
				const string storedProcName = "dbo.usp_FundCheckExistence";

				parameters = new SqlParameter[8];
				parameters[0] = new SqlParameter("@hiPortfolioCode", SqlDbType.Char, 10);
				parameters[0].Value = fund.HiPortfolioCode;
				parameters[1] = new SqlParameter("@securityCode", SqlDbType.VarChar, 8);
				parameters[1].Value = fund.SecurityCode;
				parameters[2] = new SqlParameter("@fullName", SqlDbType.VarChar, 100);
				parameters[2].Value = fund.FullName;
				parameters[3] = new SqlParameter("@shortName", SqlDbType.VarChar, 50);
				parameters[3].Value = fund.ShortName;
				parameters[4] = new SqlParameter("@hiPortfolioCodeExists", SqlDbType.Bit);
				parameters[4].Direction = ParameterDirection.Output;
				parameters[5] = new SqlParameter("@securityCodeExists", SqlDbType.Bit);
				parameters[5].Direction = ParameterDirection.Output;
				parameters[6] = new SqlParameter("@fullNameExists", SqlDbType.Bit);
				parameters[6].Direction = ParameterDirection.Output;
				parameters[7] = new SqlParameter("@shortNameExists", SqlDbType.Bit);
				parameters[7].Direction = ParameterDirection.Output;

				SqlHelper.ExecuteNonQuery(this.ConnectionString, CommandType.StoredProcedure, storedProcName, parameters);

				hiPortfolioCodeExists = (bool) parameters[4].Value;
				securityCodeExists = (bool) parameters[5].Value;
				fullNameExists = (bool) parameters[6].Value;
				shortNameExists = (bool) parameters[7].Value;

			}
			catch (SqlException ex)
			{
				this.ThrowDBException(ex, ex.Procedure, parameters);
			}
			finally
			{
				T.E();
			}
		}

		/// <summary>
		/// Checks to see if the updated fullname or shortname already exists in the system for another fund
		/// </summary>
		/// <param name="hiPortfolioCode"></param>
		/// <param name="fullName"></param>
		/// <param name="shortName"></param>
		/// <param name="securityCode"></param>
		/// <param name="fullNameExists"></param>
		/// <param name="shortNameExists"></param>
		/// <param name="securityCodeExists"></param>
		public void CheckDuplicationForExistingFund(string hiPortfolioCode, string fullName, string shortName, string securityCode,
		                                            out bool fullNameExists, out bool shortNameExists, out bool securityCodeExists)
		{
			T.E();
			SqlParameter[] parameters = null;
			fullNameExists = false;
			shortNameExists = false;
			securityCodeExists = false;
			try
			{
				const string storedProcName = "dbo.usp_FundCheckDuplication";

				parameters = new SqlParameter[7];
				parameters[0] = new SqlParameter("@hiPortfolioCode", SqlDbType.Char, 10);
				parameters[0].Value = hiPortfolioCode;
				parameters[1] = new SqlParameter("@fullName", SqlDbType.VarChar, 100);
				parameters[1].Value = fullName;
				parameters[2] = new SqlParameter("@shortName", SqlDbType.VarChar, 50);
				parameters[2].Value = shortName;
				parameters[3] = new SqlParameter("@securityCode", SqlDbType.VarChar, 8);
				parameters[3].Value = securityCode;
				parameters[4] = new SqlParameter("@fullNameExists", SqlDbType.Bit);
				parameters[4].Direction = ParameterDirection.Output;
				parameters[5] = new SqlParameter("@shortNameExists", SqlDbType.Bit);
				parameters[5].Direction = ParameterDirection.Output;
				parameters[6] = new SqlParameter("@securityCodeExists", SqlDbType.Bit);
				parameters[6].Direction = ParameterDirection.Output;

				SqlHelper.ExecuteNonQuery(this.ConnectionString, CommandType.StoredProcedure, storedProcName, parameters);

				fullNameExists = (bool) parameters[4].Value;
				shortNameExists = (bool) parameters[5].Value;
				securityCodeExists = (bool) parameters[6].Value;

			}
			catch (SqlException ex)
			{
				this.ThrowDBException(ex, ex.Procedure, parameters);
			}
			finally
			{
				T.E();
			}
		}

		/// <summary>
		/// Saves a fund to the database.
		/// </summary>
		/// <param name="fund">The fund to save</param>
		/// <returns>True if successful, otherwise false</returns>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public void SaveFund(Fund fund)
		{
			T.E();
			try
			{
				if (fund.IsDirty || fund.IsNew)
				{
					this.SaveEntity(fund);
				}
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Saves a fund to the database.
		/// </summary>
		/// <param name="fund">The fund to save</param>
		/// <param name="transaction">The transaction context to save in</param>
		/// <returns>True if successful, otherwise false</returns>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public void SaveFund(Fund fund, SqlTransaction transaction)
		{
			T.E();
			try
			{
				if (fund.IsDirty || fund.IsNew || fund.IsDeleted)
				{
					this.SaveEntity(fund, transaction);
				}
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Persists a collection of partial funds to the database
		/// </summary>
		/// <param name="funds">The collection of funds to save</param>
		/// <param name="transaction">The transaction context to save in</param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public void SaveFunds(FundCollection funds, SqlTransaction transaction)
		{
			T.E();
			InvalidFactorException factorEx = null;
			try
			{
				//this.SaveEntityCollection( funds, transaction );

				//can't use the base class method as we want to catch the invalid factor exception

				if (funds == null) throw new ArgumentNullException("list", "Cannot persist a null collection");

				foreach (Fund fund in funds)
				{
					try
					{
						SaveFund(fund, transaction);
					}
					catch (InvalidFactorException ex)
					{
						//if we get this we stil want to continue with other updates, but
						//rethrow it in order to inform calling client who can inform the user
						//through the UI (or indeed rollback the transaction if so desired).
						//Once UI validation is completed we could remove this and just always 
						//treat this as an error and always fail.
						factorEx = ex;
					}
				}

			}
			finally
			{
				T.X();
			}
			if (factorEx != null)
			{
				throw factorEx;
			}
		}

		/// <summary>
		/// Update a fund in the database
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
			const string createSp = "usp_FundUpdate";
			InvalidFactorException factorEx = null; //we need to catch this but still continue updates if we get it 
			try
			{
				if (entity == null) throw new ArgumentNullException("entity", "Cannot update null Fund");
				if (!(entity is Fund)) throw new ArgumentException("Incorrect type, expecting Fund, was " + entity.GetType().ToString(), "entity");
				Fund fund = (Fund) entity;

				// Create parameter object
				spParams = new SqlParameter[14];

				// Build parameters
				spParams[0] = new SqlParameter("@cHiPortfolioCode", SqlDbType.Char, 10);
				spParams[1] = new SqlParameter("@sFullName", SqlDbType.VarChar, 100);
				spParams[2] = new SqlParameter("@sShortName", SqlDbType.VarChar, 50);
				spParams[3] = new SqlParameter("@cFundClassOrSeries", SqlDbType.Char, 2);
				spParams[4] = new SqlParameter("@cFundType", SqlDbType.Char, 1);
				spParams[5] = new SqlParameter("@bIsMidPriceBidPrice", SqlDbType.Bit, 1);
				spParams[6] = new SqlParameter("@cAssetFundID", SqlDbType.Char, 8);
				spParams[7] = new SqlParameter("@bFundOnHi3", SqlDbType.Bit, 1);
				spParams[8] = new SqlParameter("@bIsDual", SqlDbType.Bit);
				spParams[9] = new SqlParameter("@bIsExDividend", SqlDbType.Bit);
				spParams[10] = new SqlParameter("@bIsLife", SqlDbType.Bit);
				spParams[11] = new SqlParameter("@securityCode", SqlDbType.VarChar, 8);
				spParams[12] = new SqlParameter("@ts", SqlDbType.Timestamp);
				spParams[13] = new SqlParameter("@bIsBenchmarkable", SqlDbType.Bit);

				// Assign values to parameters
				spParams[0].Value = fund.HiPortfolioCode;
				spParams[1].Value = fund.FullName;
				spParams[2].Value = fund.ShortName;
				spParams[3].Value = fund.ClassOrSeriesCode;
				spParams[4].Value = FundPersister.resolveFundToDBType(fund);
				spParams[6].Value = fund.ParentAssetFundID;
				spParams[7].Value = fund.OnHiPortfolio3;
				spParams[11].Value = fund.SecurityCode;
				spParams[12].Value = fund.TimeStamp;
				spParams[13].Value = fund.IsBenchMarkable;

				// Resolve the type of fund object
//				OEICFund oeic = fund as OEICFund;
//				Composite composite = null;
//				LinkedFund linked = null;

				FundFactory.FundType fundType = FundFactory.ResolveFundType(fund);

				switch (fundType)
				{
					case FundFactory.FundType.Linked:
						goto case FundFactory.FundType.Composite;
					case FundFactory.FundType.Composite:
						spParams[5].Value = ((NonOEIC) fund).UseMidPriceAsBidPrice;
						spParams[8].Value = ((NonOEIC) fund).IsDualPrice;
						spParams[9].Value = false;
						spParams[10].Value = ((NonOEIC) fund).IsLife;
						break;
					case FundFactory.FundType.Oeic:
						spParams[5].Value = false;
						spParams[8].Value = false;
						spParams[9].Value = ((OEICFund) fund).IsExDividend;
						spParams[10].Value = false;
						break;
					default:
						throw new ArgumentException("Unknown fund type");
						//break;
				}

				T.Log("Call create fund stored procedure");
				SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, createSp, spParams);

				// Now update the associated fund groups
				if (fund.FundGroups != null)
				{
					T.Log("Update the associated fund groups");
					this.SaveFundGroupAssociations(fund.FundGroups, fund.HiPortfolioCode, true, transaction);
				}

				// Save the funds factors
				if (fund.Factors != null)
				{
					T.Log("Save the funds factors");
					try
					{
						this.SaveFundFactors(fund.Factors, fund.HiPortfolioCode, transaction);
					}
					catch (InvalidFactorException ex)
					{
						//if we get this we stil want to continue with other updates, but
						//rethrow it in order to inform calling client who can inform the user
						//through the UI (or indeed rollback the transaction if so desired).
						//Once UI validation is completed we could remove this and just always 
						//treat this as an error and always fail.
						factorEx = ex;
					}
				}

				// Save external system ids
				if (fund.SystemIDs != null)
				{
					T.Log("Save external system ids");
					this.SaveAssociatedExternalSystemIDs(fund.SystemIDs, transaction);
				}

				// Save tolerances
				T.Log("Save tolerances");
				this.SaveTolerances(fund, transaction);
			}
			finally
			{
				T.X();
			}
			if (factorEx != null)
			{
				throw factorEx;
			}
		}

		/// <summary>
		/// Insert a new partial fund into the database
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
			const string insertSp = "usp_FundCreate";
			InvalidFactorException factorEx = null;
			try
			{
				if (entity == null) throw new ArgumentNullException("entity", "Cannot insert null Fund");
				if (!(entity is Fund)) throw new ArgumentException("Incorrect type, expecting Fund, was " + entity.GetType().ToString(), "entity");
				Fund fund = (Fund) entity;

				// Create parameter object
				spParams = new SqlParameter[13];

				// Build parameters
				spParams[0] = new SqlParameter("@hiPortfolioCode", SqlDbType.Char, 10);
				spParams[1] = new SqlParameter("@fullName", SqlDbType.VarChar, 100);
				spParams[2] = new SqlParameter("@shortName", SqlDbType.VarChar, 50);
				spParams[3] = new SqlParameter("@fundClassOrSeries", SqlDbType.Char, 2);
				spParams[4] = new SqlParameter("@fundType", SqlDbType.Char, 1);
				spParams[5] = new SqlParameter("@isMidPriceBidPrice", SqlDbType.Bit, 1);
				spParams[6] = new SqlParameter("@assetFundID", SqlDbType.Char, 8);
				spParams[7] = new SqlParameter("@FundOnHi3", SqlDbType.Bit, 1);
				spParams[8] = new SqlParameter("@bIsDual", SqlDbType.Bit, 1);
				spParams[9] = new SqlParameter("@bIsExDividend", SqlDbType.Bit, 1);
				spParams[10] = new SqlParameter("@bIsLife", SqlDbType.Bit, 10);
				spParams[11] = new SqlParameter("@sSecurityCode", SqlDbType.VarChar, 8);
				spParams[12] = new SqlParameter("@bIsBenchmarkable", SqlDbType.Bit, 8);

				// Assign values to parameters
				spParams[0].Value = fund.HiPortfolioCode;
				spParams[1].Value = fund.FullName;
				spParams[2].Value = fund.ShortName;
				spParams[3].Value = fund.ClassOrSeriesCode;
				spParams[4].Value = FundPersister.resolveFundToDBType(fund);
				spParams[6].Value = fund.ParentAssetFundID;
				spParams[7].Value = fund.OnHiPortfolio3;
				spParams[11].Value = fund.SecurityCode;
				spParams[12].Value = fund.IsBenchMarkable;

				// Resolve the type of fund object
//				OEICFund oeic = fund as OEICFund;
//				Composite composite = null;
//				LinkedFund linked = null;

				FundFactory.FundType fundType = FundFactory.ResolveFundType(fund);

				// Get the correct tolerance information
				switch (fundType)
				{
					case FundFactory.FundType.Linked:
						goto case FundFactory.FundType.Composite;
					case FundFactory.FundType.Composite:
						spParams[5].Value = ((NonOEIC) fund).UseMidPriceAsBidPrice;
						spParams[8].Value = ((NonOEIC) fund).IsDualPrice;
						spParams[9].Value = false;
						spParams[10].Value = ((NonOEIC) fund).IsLife;
						break;
					case FundFactory.FundType.Oeic:
						spParams[5].Value = false;
						spParams[8].Value = false;
						spParams[9].Value = ((OEICFund) fund).IsExDividend;
						spParams[10].Value = false;
						break;
					default:
						throw new ArgumentException("Unknown fund type");
						//break;
				}

				// Call create fund stored procedure
				T.Log("Call create fund stored procedure");
				SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, insertSp, spParams);

				// Now update the associated fund groups
				if (fund.FundGroups != null)
				{
					T.Log("Insert the associated fund groups");
					this.SaveFundGroupAssociations(fund.FundGroups, fund.HiPortfolioCode, false, transaction);
				}

				// Update factors
				if (fund.Factors != null)
				{
					T.Log("Insert factors");
					try
					{
						this.SaveFundFactors(fund.Factors, fund.HiPortfolioCode, transaction);
					}
					catch (InvalidFactorException ex)
					{
						//if we get this we stil want to continue with other updates, but
						//rethrow it in order to inform calling client who can inform the user
						//through the UI (or indeed rollback the transaction if so desired).
						//Once UI validation is completed we could remove this and just always 
						//treat this as an error and always fail.
						factorEx = ex;
					}
				}

				// Update the external system Ids
				if (fund.SystemIDs != null)
				{
					T.Log("Insert the external system Ids");
					this.SaveAssociatedExternalSystemIDs(fund.SystemIDs, transaction);
				}

				T.Log("Insert tolerances");
				this.SaveTolerances(fund, transaction);
			}
			finally
			{
				T.X();
			}
			if (factorEx != null)
			{
				throw factorEx;
			}
		}

		#endregion

		#region Entity methods

		/// <summary>
		/// Creates a fund object from the supplied data.
		/// </summary>
		/// <param name="safeReader">The reader containing the data.</param>
		/// <returns></returns>
		/// <exception cref="SchemaMismatchException">Column missing from data returned by stored procedure</exception>
		protected override object CreateEntity(SafeDataReader safeReader)
		{
			T.E();

			Fund newFund = null;
			string hiportFolioCode = safeReader.GetString("HiPortFolioCode");

			try
			{
				FundGroupCollection fundGroups = null;
				ExternalSystemIDCollection systemIDs = null;
				FundGroupPersister fgPersister = null;

				// No need to over populate the object needlessly!
				if (m_createEntityType == loadType.loadFund || m_createEntityType == loadType.loadForCompany)
				{
					fgPersister = new FundGroupPersister(this.ConnectionString);

					// Load the funds associated fund groups
					fundGroups = fgPersister.LoadDirectFundGroupsByFundCode(hiportFolioCode);

					// Load the external system ids for the fund
					systemIDs = this.LoadExternalSystemIDs(hiportFolioCode);
				}
				// Create a new fund object
				newFund = this.LoadNewFund(safeReader, fundGroups, null, systemIDs);

				//we need the parent asset fund groups for static data export
				if (m_createEntityType == loadType.loadForCompany)
				{
					newFund.ParentFundGroups = fgPersister.LoadFundGroupsByAssetFundCode(newFund.AssetFundID);
				}

			}
			finally
			{
				T.X();
			}

			return newFund;
		}

		#endregion
	}
}