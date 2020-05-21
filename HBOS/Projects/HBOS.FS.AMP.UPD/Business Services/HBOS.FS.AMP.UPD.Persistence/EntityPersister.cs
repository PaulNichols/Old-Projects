using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using HBOS.FS.AMP.Entities;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.Support.Tex;
using Microsoft.ApplicationBlocks.Data;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// The base class for all persisters concerned with the persisting of business entities.
	/// </summary>
	public abstract class EntityPersister : PersisterBase
	{
		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		protected EntityPersister(): base()
		{
		}

		/// <summary>
		/// Constructor used to initialise the ConnectionString property.
		/// </summary>
		/// <param name="connectionString">
		/// The connection string to be used for the data access layer.</param>
		protected EntityPersister(string connectionString): base(connectionString)
		{
		}

		#endregion

		#region SaveMethods

		/// <summary>
		/// Persist the entity collection to the database. The entities must implement IEntityBase
		/// </summary>
		/// <param name="list">The list of entities object being persisted.</param>
		protected internal void SaveEntityCollection(IEnumerable list)
		{
			SaveEntityCollection(list, this.ConnectionString);
		}

		/// <summary>
		/// Persist the entity collection to the database. The entities must implement IEntityBase
		/// </summary>
		/// <param name="list">The list of entities object being persisted.</param>
		/// <param name="connectionString">The connection string to use for database access.</param>
		/// <returns>True if the save is successful.</returns>
		protected internal void SaveEntityCollection(IEnumerable list, string connectionString)
		{
			SqlConnection connection = new SqlConnection(connectionString);
			connection.Open();
			this.SaveEntityCollection(list, connection);
		}

		/// <summary>
		/// Persist the entity collection to the database. The entities must implement IEntityBase
		/// </summary>
		/// <param name="list">The list of entities object being persisted.</param>
		/// <param name="connection">The connection to use for the database access.</param>
		/// <returns>True if the save is successful.</returns>
		protected internal void SaveEntityCollection(IEnumerable list, SqlConnection connection)
		{
			SqlTransaction transaction = connection.BeginTransaction();

			try
			{
				this.SaveEntityCollection(list, transaction);
				transaction.Commit();
			}
			catch
			{
				transaction.Rollback();
				throw;
			}
		}

		/// <summary>
		/// Saves an entity collection. The entity must implement IEntityBase
		/// </summary>
		/// <param name="list">The list of entities to save</param>
		/// <param name="transaction">The transaction context to save in</param>
		protected internal void SaveEntityCollection(IEnumerable list, SqlTransaction transaction)
		{
			if (list == null) throw new ArgumentNullException("list", "Cannot persist a null collection");

			foreach (IEntityBase entity in list)
			{
				SaveEntity(entity, transaction);
			}
		}

		/// <summary>
		/// Persist the base entity object to the database.
		/// </summary>
		/// <param name="entity">The base entity object being persisted.</param>
		protected internal void SaveEntity(IEntityBase entity)
		{
			SaveEntity(entity, this.ConnectionString);
		}

		/// <summary>
		/// Persist the base entity object to the database.
		/// </summary>
		/// <param name="entity">The base entity object being persisted.</param>
		/// <param name="connectionString">
		/// The connection string to use for database access.
		/// </param>
		/// <returns>True if the save is successful.</returns>
		protected internal void SaveEntity(IEntityBase entity, string connectionString)
		{
			SqlConnection connection = new SqlConnection(connectionString);
			connection.Open();
			this.SaveEntity(entity, connection);
		}

		/// <summary>
		/// Persist the base entity object to the database.
		/// </summary>
		/// <param name="entity">The base entity object being persisted.</param>
		/// <param name="connection">
		/// The connection to use for the database access.
		/// </param>
		/// <returns>True if the save is successful.</returns>
		protected internal void SaveEntity(IEntityBase entity, SqlConnection connection)
		{
			SqlTransaction transaction = connection.BeginTransaction();

			try
			{
				this.SaveEntity(entity, transaction);
				transaction.Commit();
			}
			catch
			{
				transaction.Rollback();
				throw;
			}
		}


		/// <summary>
		/// Saves (update/delete/insert) the entity.
		/// </summary>
		/// <param name="entity">The entity to save.</param>
		/// <param name="transaction">The transaction context to save in</param>
		protected internal virtual void SaveEntity(IEntityBase entity, SqlTransaction transaction)
		{
			if (entity == null) throw new ArgumentNullException("entity", "Cannot persist a null entity");

			if (entity.IsNew)
			{
				InsertEntity(entity, transaction);
			}

			else if (entity.IsDeleted)
			{
				DeleteEntity(entity, transaction);
			}

			else if (entity.IsDirty)
			{
				UpdateEntity(entity, transaction);
			}
		}

		/// <summary>
		/// Override to Insert the entity into the database
		/// </summary>
		/// <param name="entity">Entity to insert.</param>
		/// <param name="transaction">The transaction context to save in</param>
		protected virtual void InsertEntity(IEntityBase entity, SqlTransaction transaction)
		{
			throw new ApplicationException("Insert functionality is not implemented in " + this.GetType().ToString());
		}

		/// <summary>
		/// Override to Delete the entity from the database
		/// </summary>
		/// <param name="entity">Entity to delete.</param>
		/// <param name="transaction">The transaction context to save in</param>
		protected virtual void DeleteEntity(IEntityBase entity, SqlTransaction transaction)
		{
			throw new ApplicationException("Delete functionality is not implemented in " + this.GetType().ToString());
		}

		/// <summary>
		/// Override to Update the entity in the database
		/// </summary>
		/// <param name="entity">Entity to update.</param>
		/// <param name="transaction">The transaction context to save in</param>
		protected virtual void UpdateEntity(IEntityBase entity, SqlTransaction transaction)
		{
			throw new ApplicationException("Update functionality is not implemented in " + this.GetType().ToString());
		}

		#endregion

		#region LoadMethods

		/// <summary>
		/// Loads an entity from db using sp and params provided
		/// </summary>
		/// <param name="storedProcName"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		protected IEntityBase LoadEntity(string storedProcName, SqlParameter[] parameters)
		{
			T.E();
			IEntityBase retEntity = null;
			try
			{
				using (SqlDataReader dataReader =
					SqlHelper.ExecuteReader(this.ConnectionString, CommandType.StoredProcedure, storedProcName, parameters))
				{
					if (dataReader.Read())
					{
						// Create the fund collection from the data values.
						SafeDataReader safeReader = new SafeDataReader(dataReader);
						try
						{
							retEntity = this.CreateEntity(safeReader) as IEntityBase;
						}
						catch (SchemaMismatchException ex)
						{
							// add database info to the thrown exception
							throw new SchemaMismatchException(ex.Message, DatabaseInfo, ex.Column, ex);
						}
					}
				}
			}
			catch (SqlException ex)
			{
				this.ThrowDBException(ex, ex.Procedure, parameters);
			}
			finally
			{
				T.E();
			}
			return retEntity;

		}

		/// <summary>
		/// Loads an entity from db using sp and params provided
		/// </summary>
		/// <param name="storedProcName"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		protected IEntityBase LoadEntity(string storedProcName, SqlParameterCollection parameters)
		{
			T.E();
			IEntityBase retEntity = null;
			try
			{
				retEntity = LoadEntity(storedProcName,ConvertParameterCollectionToArray(parameters));
			}
			finally
			{
				T.E();
			}
			return retEntity;
		}

		/// <summary>
		/// Loads an entity from db using a stored procedure with no parameters
		/// </summary>
		/// <param name="storedProcName"></param>
		/// <returns></returns>
		protected IEntityBase LoadEntity(string storedProcName)
		{
			T.E();
			IEntityBase retEntity = null;
			try
			{
				retEntity = LoadEntity(storedProcName,new SqlParameter[0]);
			}
			finally
			{
				T.E();
			}
			return retEntity;
		}

		/// <summary>
		/// Helper method to load an entity collection from a given stored procedured
		/// </summary>
		/// <param name="storedProcName">Name of the stored procedure.</param>
		/// <param name="parameters">Parameters to pass to the stored procedure.</param>
		/// <param name="list">The collection that the entities are added to.</param>
		protected void LoadEntityCollection(string storedProcName, SqlParameter[] parameters, IList list)
		{
			T.E();
			try
			{
				using (SqlDataReader dataReader =
					SqlHelper.ExecuteReader(this.ConnectionString, CommandType.StoredProcedure, storedProcName, parameters))
				{
					// Create the fund collection from the data values.
					SafeDataReader safeReader = new SafeDataReader(dataReader);
					while (dataReader.Read())
					{
						try
						{
							list.Add(this.CreateEntity(safeReader));
						}
						catch (SchemaMismatchException ex)
						{
							// add database info to the thrown exception
							throw new SchemaMismatchException(ex.Message, DatabaseInfo, ex.Column, ex);
						}
					}
				}
			}
			catch (SqlException ex)
			{
				this.ThrowDBException(ex, ex.Procedure, parameters);
			}
			T.X();
		}

		/// <summary>
		/// Helper method to load an entity collection from a given stored procedured
		/// </summary>
		/// <param name="storedProcName">Name of the stored procedure.</param>
		/// <param name="parameters">Parameters to pass to the stored procedure.</param>
		/// <param name="list">The collection that the entities are added to.</param>
		protected void LoadEntityCollection(string storedProcName, SqlParameterCollection parameters, IList list)
		{
			T.E();
			try
			{
				LoadEntityCollection(storedProcName,ConvertParameterCollectionToArray(parameters),list);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Helper method to load an entity collection from a given stored procedured with no parameters
		/// </summary>
		/// <param name="storedProcName">Name of the stored procedure.</param>
		/// <param name="list">The collection that the entities are added to.</param>
		protected void LoadEntityCollection(string storedProcName, IList list)
		{
			T.E();
			try
			{
				LoadEntityCollection(storedProcName,new SqlParameter[0],list);
			}
			finally
			{
				T.X();
			}
		}



		/// <summary>
		/// Override in the derived classes to create the entity from the data in the SafeDataReader.
		/// </summary>
		/// <param name="safeReader">Safe reader to get the values from.</param>
		/// <returns></returns>
		protected virtual object CreateEntity(SafeDataReader safeReader)
		{
			throw new ApplicationException("Load functionality is not implemented in " + this.GetType().ToString());
		}

		#endregion LoadMethods
	}
}