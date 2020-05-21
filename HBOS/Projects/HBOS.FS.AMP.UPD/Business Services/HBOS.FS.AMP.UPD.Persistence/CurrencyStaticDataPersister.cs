using System;
using System.Data;
using System.Data.SqlClient;
using HBOS.FS.AMP.Entities;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Types.Currency;
using HBOS.FS.Support.Tex;
using Microsoft.ApplicationBlocks.Data;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// Summary description for CurrencyStaticDataPersister.
	/// </summary>
	public class CurrencyStaticDataPersister : EntityPersister
	{
		#region Constructor

		/// <summary>
		/// Creates a new <see cref="CurrencyStaticDataPersister"/> instance.
		/// </summary>
		/// <param name="connectionString">Database connection string.</param>
		public CurrencyStaticDataPersister(string connectionString) : base(connectionString)
		{
		}

		#endregion

		#region public methods

		/// <summary>
		/// Loads the static data.
		/// </summary>
		/// <param name="currencyCode">Currency code.</param>
		/// <returns>A currency object</returns>
		public Currency Load(string currencyCode)
		{
			T.E();

			const string spName = "usp_CurrencyGetStaticData";
			SqlParameter[] spParameters = new SqlParameter[1];

			// Set up the stored procedure parameters.
			spParameters[0] = new SqlParameter("@currencyCode", SqlDbType.VarChar, 10);
			spParameters[0].Value = currencyCode;

			// Create the fund object.
			Currency currency = null;

			try
			{
				currency = (Currency) this.LoadEntity(spName, spParameters);

				// Test for valid object
				if (currency == null)
				{
					throw new ArgumentException(string.Format("Failed to load currency ({0})", currencyCode));
				}
			}
			catch (SqlException ex)
			{
				ThrowDBException(ex, spName, spParameters);
			}
			finally
			{
				T.X();
			}
			return currency;
		}

		/// <summary>
		/// Saves the specified currency.
		/// </summary>
		/// <param name="currency">Currency.</param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public void Save(Currency currency)
		{
			T.E();
			try
			{
				this.SaveEntity(currency);
				CacheHelper cacheHelper =new CacheHelper();
				cacheHelper.FlushCurrencies();
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Saves the specified currencies.
		/// </summary>
		/// <param name="currency">Currency.</param>
		/// <param name="transaction">Transaction.</param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public void Save(Currency currency, SqlTransaction transaction)
		{
			T.E();
			try
			{
				this.SaveEntity(currency, transaction);
				CacheHelper cacheHelper =new CacheHelper();
				cacheHelper.FlushCurrencies();
			}
			finally
			{
				T.X();
			}
		}

		#endregion

		#region Entity methods

		/// <summary>
		/// Creates the entity from the data reader.
		/// </summary>
		/// <param name="safeReader">Reader to get the data from.</param>
		/// <returns></returns>
		protected override object CreateEntity(SafeDataReader safeReader)
		{
			T.E();

			Currency currency = null;
			try
			{
				currency = new Currency(safeReader.GetString("currencyCode"),
				                        safeReader.GetString("currency"),
				                        safeReader.GetTimestamp("ts"));
			}
			finally
			{
				T.X();
			}
			return currency;
		}


		/// <summary>
		/// Deletes the currency item.
		/// </summary>
		/// <param name="entity">Entity.</param>
		/// <param name="transaction">Transaction.</param>
		/// <exception cref="ArgumentNullException">The currency object is NULL</exception>
		/// <exception cref="ArgumentException">The entity object is of the wrong type, i.e. not a currency</exception>
		/// <exception cref="DatabaseException">Unable to delete currency item</exception>
		protected override void DeleteEntity(IEntityBase entity, SqlTransaction transaction)
		{
			T.E();
			if (entity == null) throw new ArgumentNullException("entity", "Cannot delete a null currency");
			if (!(entity is Currency)) throw new ArgumentException(string.Format("Cannot convert type {0} to type currency for deletion", entity.GetType()), "entity");

			Currency currency = (Currency) entity;
			const string spName = "usp_CurrencyDelete";
			SqlParameter[] spParams = new SqlParameter[2];
			spParams[0] = new SqlParameter("@currencyCode", SqlDbType.VarChar, 10);
			spParams[1] = new SqlParameter("@ts", SqlDbType.Timestamp);

			try
			{
				spParams[0].Value = currency.CurrencyCode;
				spParams[1].Value = currency.TimeStamp;

				SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, spParams);
			}
			catch (SqlException ex)
			{
				this.ThrowDBException(ex, spName, spParams);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Inserts the entity.
		/// </summary>
		/// <param name="entity">Entity.</param>
		/// <param name="transaction">Transaction.</param>
		/// <exception cref="ArgumentNullException">The currency object is NULL</exception>
		/// <exception cref="ArgumentException">The entity object is of the wrong type, i.e. not a currency</exception>
		/// <exception cref="DatabaseException">Unable to append currency item</exception>
		protected override void InsertEntity(IEntityBase entity, SqlTransaction transaction)
		{
			T.E();
			if (entity == null) throw new ArgumentNullException("entity", "Cannot insert a null currency");
			if (!(entity is Currency)) throw new ArgumentException(string.Format("Cannot convert type {0} to type currency for insertion", entity.GetType()), "entity");

			// Establish stored proc environment
			const string spName = "usp_CurrencyCreate";
			Currency currency = (Currency) entity;
			SqlParameter[] spParams = new SqlParameter[3];
			spParams[0] = new SqlParameter("@currencyCode", SqlDbType.VarChar, 10);
			spParams[1] = new SqlParameter("@currencyName", SqlDbType.VarChar, 50);
			spParams[2] = new SqlParameter("@ts", SqlDbType.Timestamp);
			spParams[2].Direction = ParameterDirection.Output;

			try
			{
				spParams[0].Value = currency.CurrencyCode;
				spParams[1].Value = currency.CurrencyName;

				// Execute insert query
				SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, spParams);

				// Capture return values
				currency.TimeStamp = (byte[]) spParams[2].Value;
				currency.IsDirty = false;
			}
			catch (SqlException ex)
			{
				this.ThrowDBException(ex, spName, spParams);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Updates the entity.
		/// </summary>
		/// <param name="entity">Entity.</param>
		/// <param name="transaction">Transaction.</param>
		/// <exception cref="ArgumentNullException">The currency object is NULL</exception>
		/// <exception cref="ArgumentException">The entity object is of the wrong type, i.e. not a currency</exception>
		/// <exception cref="DatabaseException">Unable to update currency item</exception>
		protected override void UpdateEntity(IEntityBase entity, SqlTransaction transaction)
		{
			T.E();
			if (entity == null) throw new ArgumentNullException("entity", "Cannot update a null currency");
			if (!(entity is Currency)) throw new ArgumentException(string.Format("Cannot convert type {0} to type currency for insertion", entity.GetType()), "entity");

			// Establish stored proc environment
			const string spName = "usp_CurrencyUpdate";
			Currency currency = (Currency) entity;
			SqlParameter[] spParams = new SqlParameter[4];

			spParams[0] = new SqlParameter("@currentCurrencyCode", SqlDbType.VarChar, 10);
			spParams[1] = new SqlParameter("@currencyCode", SqlDbType.VarChar, 10);
			spParams[2] = new SqlParameter("@currencyName", SqlDbType.VarChar, 50);
			spParams[3] = new SqlParameter("@ts", SqlDbType.Timestamp);
			spParams[3].Direction = ParameterDirection.InputOutput;

			try
			{
				spParams[0].Value = currency.CurrentCurrencyCode;
				spParams[1].Value = currency.CurrencyCode;
				spParams[2].Value = currency.CurrencyName;
				spParams[3].Value = currency.TimeStamp;

				// Execute insert query
				SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, spParams);

				// Capture return values
				currency.TimeStamp = (byte[]) spParams[3].Value;
				currency.IsDirty = false;
			}
			catch (SqlException ex)
			{
				this.ThrowDBException(ex, spName, spParams);
			}
			finally
			{
				T.X();
			}
		}

		#endregion
	}
}