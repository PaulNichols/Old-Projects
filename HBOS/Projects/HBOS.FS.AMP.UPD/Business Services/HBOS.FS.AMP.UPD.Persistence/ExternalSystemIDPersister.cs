using System;
using System.Data;
using System.Data.SqlClient;
using HBOS.FS.AMP.Entities;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Types.Funds;
using HBOS.FS.Support.Tex;
using Microsoft.ApplicationBlocks.Data;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// The class to use for persisting or retrieving ExternalSystemID objects.
	/// </summary>
	public class ExternalSystemIDPersister : EntityPersister
	{
		#region Constructors

		/// <summary>
		/// Constructor initialising the connection string propperty.
		/// </summary>
		/// <param name="connectionString">The connection string to use for data access.</param>
		public ExternalSystemIDPersister(string connectionString) : base(connectionString)
		{
		}

		#endregion

		#region Load Methods

		/// <summary>
		/// Return all the system IDs associated with the given HiPortfolio code.
		/// </summary>
		/// <param name="hiPortfolioCode">The fund code for which the system IDs are required.</param>
		/// <returns>All the system IDs associated with the given fund code.</returns>
		/// <exception cref="DatabaseException">Unable to load system ids</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ArgumentException">Column name not recognised or unexpected data in db</exception>
		public ExternalSystemIDCollection LoadSystemIDsForHiPortfolioCode(string hiPortfolioCode)
		{
			T.E();
			const string loadSp = "usp_FundExternalSystemIdentifiersListForHiPortfolioCode";

			SqlParameter[] parameters = new SqlParameter[1];
			parameters[0] = new SqlParameter("@cHiportfolioCode", SqlDbType.Char, 10);
			parameters[0].Value = hiPortfolioCode;

			this.hiPortfolioCode = hiPortfolioCode;
			ExternalSystemIDCollection systemIDs = new ExternalSystemIDCollection();
			this.LoadEntityCollection(loadSp, parameters, systemIDs);

			T.X();
			return systemIDs;
		}

		/// <summary>
		/// Loads the collection of all extenal systems from the database.
		/// </summary>
		/// <returns>A collection of all our systems</returns>
		/// <exception cref="DatabaseException">Unable to load company</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		public ExternalSystemIDCollection LoadSystems(string companyCode )
		{
			T.E();
			const string sp = "usp_ExternalSystemsList";

			SqlParameter[] parameters = new SqlParameter[1];
			parameters[0] = new SqlParameter("@companyCode", SqlDbType.VarChar, 10);
			parameters[0].Value = companyCode;

			ExternalSystemIDCollection systems = new ExternalSystemIDCollection();
			this.LoadEntityCollection(sp,parameters,systems);

			T.X();
			return systems;
		}

		private string hiPortfolioCode;

		/// <summary>
		/// Creates the entity from the data in the reader
		/// </summary>
		/// <param name="safeReader">The reader to load data from</param>
		/// <returns></returns>
		protected override object CreateEntity(SafeDataReader safeReader)
		{
			T.E();
			ExternalSystemID id;
			if (safeReader.ColumnExists("alternativeFundCode"))
			{
				id = new ExternalSystemID(
					this.hiPortfolioCode,
					safeReader.GetInt32("systemID"),
					safeReader.GetString("systemName"),
					safeReader.GetString("alternativeFundCode"),
					safeReader.GetTimestamp("ts")
					);
				T.X();
			}
			else
			{
				id = new ExternalSystemID(
					string.Empty,
					safeReader.GetInt32("systemID"),
					safeReader.GetString("systemName"),
					string.Empty,
					new byte[1]
					);
			}
			return id;
		}

		#endregion

		#region Save Methods

		/// <summary>
		/// Loops through the systemID collection to update those that have been changed or added.
		/// </summary>
		/// <param name="systemIDs">The collection of updated system ID objects.</param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public void Save(ExternalSystemIDCollection systemIDs)
		{
			T.E();
			this.SaveEntityCollection(systemIDs);
			T.X();
		}

		/// <summary>
		/// Loop through the systemID collection to update those that have been changed or added using the
		/// current connection and transaction.
		/// </summary>
		/// <param name="systemIDs">The collection of updated system ID objects.</param>
		/// <param name="transaction">The current transaction.</param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public void Save(ExternalSystemIDCollection systemIDs, SqlTransaction transaction)
		{
			T.E();
			this.SaveEntityCollection(systemIDs, transaction);
			T.X();
		}

		/// <summary>
		/// Inserts a new External System ID into databse.
		/// </summary>
		/// <param name="entity">Entity to insert.</param>
		/// <param name="transaction">Transaction to do insert under.</param>
		protected override void InsertEntity(IEntityBase entity, SqlTransaction transaction)
		{
			T.E();
			if (entity == null) throw new ArgumentNullException("entity", "Cannot insert null User");
			if (!(entity is ExternalSystemID)) throw new ArgumentException("Incorrect type when inserting, expecting ExternalSystemID, was " + entity.GetType().ToString(), "entity");
			ExternalSystemID externalID = (ExternalSystemID) entity;

			const string spName = "usp_FundExternalSystemIdentifiersCreate";
			SqlParameter[] parameters = null;

			try
			{
				parameters = new SqlParameter[3];

				parameters[0] = new SqlParameter("@cHiPortfolioCode", SqlDbType.Char, 10);
				parameters[0].Value = externalID.HiPortfolioCode;

				parameters[1] = new SqlParameter("@iSystemID", SqlDbType.Int);
				parameters[1].Value = externalID.SystemID;

				parameters[2] = new SqlParameter("@sAlternativeFundCode", SqlDbType.VarChar, 50);
				parameters[2].Value = externalID.FundCode;

				SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, parameters);
			}
			catch (SqlException ex)
			{
				this.ThrowDBException(ex, spName, parameters);
			}

			T.X();
		}

		/// <summary>
		/// Updates an existing External System Id in datbase
		/// </summary>
		/// <param name="entity">Entity to be updated.</param>
		/// <param name="transaction">Transaction to use.</param>
		protected override void UpdateEntity(IEntityBase entity, SqlTransaction transaction)
		{
			T.E();
			if (entity == null) throw new ArgumentNullException("entity", "Cannot update null User");
			if (!(entity is ExternalSystemID)) throw new ArgumentException("Incorrect type when updating, expecting ExternalSystemID, was " + entity.GetType().ToString(), "entity");
			ExternalSystemID externalID = (ExternalSystemID) entity;

			const string spName = "usp_FundExternalSystemIdentifiersUpdate";
			SqlParameter[] parameters = null;

			try
			{
				parameters = new SqlParameter[4];

				parameters[0] = new SqlParameter("@cHiPortfolioCode", SqlDbType.Char, 10);
				parameters[0].Value = externalID.HiPortfolioCode;

				parameters[1] = new SqlParameter("@iSystemID", SqlDbType.Int);
				parameters[1].Value = externalID.SystemID;

				parameters[2] = new SqlParameter("@sAlternativeFundCode", SqlDbType.VarChar, 50);
				parameters[2].Value = externalID.FundCode;

				parameters[3] = new SqlParameter("@ts", SqlDbType.Timestamp);
				parameters[3].Value = externalID.TimeStamp;

				SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, parameters);
			}
			catch (SqlException ex)
			{
				this.ThrowDBException(ex, spName, parameters);
			}

			T.X();
		}

		/// <summary>
		/// Deletes an external system id from the database.
		/// </summary>
		/// <param name="entity">The entity to be deleted.</param>
		/// <param name="transaction">Transaction to use.</param>
		protected override void DeleteEntity(IEntityBase entity, SqlTransaction transaction)
		{
			T.E();
			if (entity == null) throw new ArgumentNullException("entity", "Cannot delete null User");
			if (!(entity is ExternalSystemID)) throw new ArgumentException("Incorrect type when deleting, expecting ExternalSystemID, was " + entity.GetType().ToString(), "entity");
			ExternalSystemID externalID = (ExternalSystemID) entity;

			const string spName = "usp_FundExternalSystemIdentifiersDelete";
			SqlParameter[] parameters = null;

			try
			{
				parameters = new SqlParameter[3];

				parameters[0] = new SqlParameter("@cHiPortfolioCode", SqlDbType.Char, 10);
				parameters[0].Value = externalID.HiPortfolioCode;

				parameters[1] = new SqlParameter("@iSystemID", SqlDbType.Int);
				parameters[1].Value = externalID.SystemID;

				parameters[2] = new SqlParameter("@ts", SqlDbType.Timestamp);
				parameters[2].Value = externalID.TimeStamp;

				SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, parameters);
			}
			catch (SqlException ex)
			{
				this.ThrowDBException(ex, spName, parameters);
			}

			T.X();
		}

		#endregion
	}
}