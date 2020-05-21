using System;
using System.Data;
using System.Data.SqlClient;
using HBOS.FS.AMP.Entities;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.FundGroups;
using HBOS.FS.Support.Tex;
using Microsoft.ApplicationBlocks.Data;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// Summary description for AssetFundPricingPersister.
	/// </summary>
	public class AssetFundStaticDataPersister : AssetFundPersister
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="connectionString"></param>
		public AssetFundStaticDataPersister(string connectionString) : base(connectionString)
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// Loads a particular asset fund
		/// Loads the light object version for static data maintenance
		/// </summary>
		/// <param name="assetFundCode">The Hi3 code (??) for the asset fund</param>
		/// <returns>The requested asset fund</returns>
		/// <exception cref="DatabaseException">Unable to load item</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ArgumentException">Column name not recognised, unexpected data in db, or column list not initialised</exception>
		public AssetFund Load(string assetFundCode)
		{
			T.E();
			AssetFundCollection assetFunds;
			try
			{
				assetFunds = USPAssetFundDetailsLight(assetFundCode);

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
		/// This method returns a collection of asset funds based upon a particualr key
		/// Used for the light object collection (no weithing collections etc)
		/// </summary>
		/// <param name="assetFundCode"></param>
		/// <returns></returns>
		/// <exception cref="DatabaseException">Unable to load items</exception>
		/// <exception cref="ArgumentException">Column name not recognised, unexpected data in db, or column list not initialised</exception>
		private AssetFundCollection USPAssetFundDetailsLight(string assetFundCode)
		{
			// This method handles the call to the usp_AssetFundDetailsForAssetFundID
			// This saves repeating it in different places when it is doing the same thing

			T.E();
			AssetFundCollection assetFunds = new AssetFundCollection();
			const string loadSp = "usp_AssetFundGetStaticData";
			SqlParameter[] parameters = new SqlParameter[1];

			try
			{
				// Set up the parameters.
				parameters[0] = new SqlParameter("@cAssetFundID", SqlDbType.Char, 8);
				parameters[0].Value = assetFundCode;

				this.LoadEntityCollection(loadSp, parameters, assetFunds);
			}
			finally
			{
				T.X();
			}
			return assetFunds;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		protected override object CreateEntity(SafeDataReader reader)
		{
			AssetFund newAssetFund = createEntity(reader);
			T.E();
			logAssetFundLoad(newAssetFund);
			T.X();
			return newAssetFund;
		}

		/// <summary>
		/// Creates the light entity.
		/// </summary>
		/// <param name="reader">Reader.</param>
		/// <returns></returns>
		protected virtual AssetFund createEntity(SafeDataReader reader)
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
											reader.GetTimestamp("ts"),
				                            fgColl,
											loadMovementConstituents(assetFundCode,false),
											null,
											reader.GetInt32("PriceFileId"));

			}
			finally
			{
				T.X();
			}
			return newAssetFund;
		}

		#region Save Methods

		/// <summary>
		/// Saves (update/delete/insert) the entity.
		/// </summary>
		/// <param name="entity">The entity to save.</param>
		/// <param name="transaction">The transaction context to save in</param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		protected internal override void SaveEntity(IEntityBase entity, SqlTransaction transaction)
		{
			if (entity == null) throw new ArgumentNullException("entity", "Cannot insert null Asset Fund");
			if (!(entity is AssetFund)) throw new ArgumentException("Incorrect type, expecting Asset Fund, was " + entity.GetType().ToString(), "entity");

			base.SaveEntity(entity, transaction);
		}

		/// <summary>
		/// Insert a new user into the database
		/// </summary>
		/// <param name="transaction">The transaction used for the persistence, allowing transactional rollback.</param>
		/// <param name="entity">Persitee user object</param>
		/// <returns>Success flag</returns>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		protected override void InsertEntity(IEntityBase entity, SqlTransaction transaction)
		{
			const string createSp = "usp_AssetFundCreate";
			T.E();
			SqlParameter[] spParams = null;
			try
			{
				//arguments asserted in SaveEntity - the only thing that calls this
				AssetFund assetFund = (AssetFund) entity;
				spParams = new SqlParameter[7];

				// Build parameters
				spParams[0] = new SqlParameter("@cAssetFundID", SqlDbType.Char, 8);
				spParams[1] = new SqlParameter("@sShortName", SqlDbType.VarChar, 50);
				spParams[2] = new SqlParameter("@sFullName", SqlDbType.VarChar, 100);
				spParams[3] = new SqlParameter("@cFundType", SqlDbType.Char, 1);
				spParams[4] = new SqlParameter("@sCompanyCode", SqlDbType.VarChar, 10);
				spParams[5] = new SqlParameter("@PriceFileID", SqlDbType.Int);
				spParams[6] = new SqlParameter("@ts", SqlDbType.Timestamp);

				// Assign values to parameters
				spParams[0].Value = assetFund.AssetFundCode;
				spParams[1].Value = assetFund.ShortName;
				spParams[2].Value = assetFund.FullName;
				spParams[3].Value = resolveAssetFundToDBType(assetFund);
				spParams[5].Value = assetFund.PriceFileId;
				spParams[4].Value = assetFund.CompanyCode;
				spParams[6].Direction = ParameterDirection.Output;

				// Call create stored procedure
				SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, createSp, spParams);

				//save associations of the asset fund to fund groups
				SaveFundGroupAssociations(assetFund, transaction, true);

				assetFund.TimeStamp = (byte[]) spParams[6].Value;
				assetFund.IsNew = false;
				assetFund.IsDirty = false;

			}
			catch (SqlException ex)
			{
				ThrowDBException(ex, createSp, spParams);
			}
			finally
			{
				T.X();
			}
		}


		/// <summary>
		/// Delete a specified asset fund from the database.
		/// </summary>
		/// <param name="transaction">The transaction used for the persistence, allowing transactional rollback.</param>
		/// <param name="entity">Persitee user object</param>
		/// <returns>Success flag</returns>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		protected override void DeleteEntity(IEntityBase entity, SqlTransaction transaction)
		{
			const string deleteSp = "usp_AssetFundDelete";
			T.E();
			SqlParameter[] spParams = null;
			try
			{
				//arguments asserted in SaveEntity - the only thing that calls this
				AssetFund assetFund = (AssetFund) entity;

				//mark the asset fund as deleted and remove associations to fund groups

				// Create paremeter object
				spParams = new SqlParameter[2];

				// This will make adding/moving/copying rows easier!!
				int paramCounter = 0;

				// Build parameters
				spParams[paramCounter++] = new SqlParameter("@cAssetFundID", SqlDbType.Char, 8);
				spParams[paramCounter++] = new SqlParameter("@ts", SqlDbType.Timestamp, 50);

				paramCounter = 0;

				// Assign values to parameters
				spParams[paramCounter++].Value = assetFund.AssetFundCode;
				spParams[paramCounter++].Value = assetFund.TimeStamp;

				// Call delete stored procedure
				SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, deleteSp, spParams);


			}
			catch (SqlException ex)
			{
				if (ex.Message.IndexOf("active associations") >= 0)
				{
					throw new AssetFundAssocDeletionException(ex.Message, ex);
				}
				else
				{
					ThrowDBException(ex, deleteSp, spParams);
				}

			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Update a user in the database
		/// </summary>
		/// <param name="transaction">The transaction used for the persistence, allowing transactional rollback.</param>
		/// <param name="entity">Persitee user object</param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		protected override void UpdateEntity(IEntityBase entity, SqlTransaction transaction)
		{
			T.E();
			SqlParameter[] spParams = null;
			const string updateSp = "usp_AssetFundUpdate";
			try
			{
				//arguments asserted in SaveEntity - the only thing that calls this
				AssetFund assetFund = (AssetFund) entity;

				// Create paremeter object
				spParams = new SqlParameter[7];

				// Build parameters
				spParams[0] = new SqlParameter("@cAssetFundID", SqlDbType.Char, 8);
				spParams[1] = new SqlParameter("@sShortName", SqlDbType.VarChar, 50);
				spParams[2] = new SqlParameter("@sFullName", SqlDbType.VarChar, 100);
				spParams[3] = new SqlParameter("@cFundType", SqlDbType.Char, 1);
				spParams[4] = new SqlParameter("@PriceFileId", SqlDbType.Int);
				spParams[5] = new SqlParameter("@ts", SqlDbType.Timestamp, 50);

				// Assign values to parameters
				spParams[0].Value = assetFund.AssetFundCode;
				spParams[1].Value = assetFund.ShortName;
				spParams[2].Value = assetFund.FullName;
				spParams[3].Value = resolveAssetFundToDBType(assetFund);
				spParams[4].Value = assetFund.PriceFileId;

				spParams[5].Value = assetFund.TimeStamp;
				spParams[5].Direction = ParameterDirection.InputOutput;

				// Call create stored procedure
				SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, updateSp, spParams);

				SaveFundGroupAssociations(assetFund, transaction, false);

				assetFund.TimeStamp = (byte[]) spParams[5].Value;
				assetFund.IsDirty = false;
			}
			catch (SqlException ex)
			{
				ThrowDBException(ex, updateSp, spParams);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Saves a singular asset fund
		/// </summary>
		/// <param name="assetFund">the asset fund to save (or delete)</param>
		/// <param name="txn">a sql transaction within which to enlist</param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public void Save(AssetFund assetFund, SqlTransaction txn)
		{
			T.E();
			try
			{
				this.SaveEntity(assetFund, txn);
			}
			finally
			{
				T.X();
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="assetFund"></param>
		/// <param name="txn"></param>
		/// <param name="forAssetFundInsert">An optimisation - if this is set to true, then there is no need to delete
		/// existing associations thereby saving a round trip and sql server sp invoke</param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		/// <exception cref="ArgumentException">Invalid fund group id</exception>
		/// <exception cref="NullReferenceException">Asset Fund, Fund group or fund group collection is null</exception>
		private void SaveFundGroupAssociations(AssetFund assetFund, SqlTransaction txn, bool forAssetFundInsert)
		{
			T.E();
			SqlParameter[] spParams = null;
			string spName = String.Empty;
			try
			{
				//simple formula - delete existing associations, then remake all.

				if (assetFund == null)
				{
					throw new NullReferenceException("Asset Fund is null");
				}
				FundGroupCollection fundGroups = assetFund.FundGroups;
				if (fundGroups == null)
				{
					throw new NullReferenceException("Fund Group collection is null");
				}

				if (!forAssetFundInsert)
				{
					//clear the existing associations
					spName = "usp_FundGroupsDeleteForAssetFundID";
					spParams = new SqlParameter[1];
					spParams[0] = new SqlParameter("@cAssetFundID", SqlDbType.Char, 8);
					spParams[0].Value = assetFund.AssetFundCode;
					SqlHelper.ExecuteNonQuery(txn, CommandType.StoredProcedure, spName, spParams);
				}

				//create (and recreate) associations
				spName = "usp_FundGroupAssociateAssetFund";

				spParams = new SqlParameter[2];
				spParams[0] = new SqlParameter("@cAssetFundID", SqlDbType.Char, 8);
				spParams[0].Value = assetFund.AssetFundCode;
				spParams[1] = new SqlParameter("@iFundGroupID", SqlDbType.Int);

				for (int i = 0; i < fundGroups.Count; i++)
				{
					FundGroup fundGroup = fundGroups[i];
					if (fundGroup == null)
					{
						throw new NullReferenceException("Fund Group is null");
					}
					if (fundGroup.ID < 1)
					{
						throw new ArgumentException("Invalid fund group ID");
					}
					spParams[1].Value = fundGroup.ID;
					SqlHelper.ExecuteNonQuery(txn, CommandType.StoredProcedure, spName, spParams);
				}


			}
			catch (SqlException ex)
			{
				ThrowDBException(ex, spName, spParams);
			}
			finally
			{
				T.X();
			}
		}

		#endregion
	}
}