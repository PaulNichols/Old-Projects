using System;
using System.Data;
using System.Data.SqlClient;
using HBOS.FS.AMP.Entities;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Types.Countries;
using HBOS.FS.Support.Tex;
using Microsoft.ApplicationBlocks.Data;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// Summary description for CountryStaticDataPersister.
	/// </summary>
	public class CountryStaticDataPersister : EntityPersister
	{
		#region Constructor

		/// <summary>
		/// Creates a new <see cref="CountryStaticDataPersister"/> instance.
		/// </summary>
		/// <param name="connectionString">Database connection string.</param>
		public CountryStaticDataPersister(string connectionString) : base(connectionString)
		{
		}

		#endregion

		#region public methods

		/// <summary>
		/// Loads the static data.
		/// </summary>
		/// <param name="CountryCode">Country code.</param>
		/// <returns>A Country object</returns>
		public Country Load(string CountryCode)
		{
			T.E();

			const string spName = "usp_CountryGetStaticData";
			SqlParameter[] spParameters = new SqlParameter[1];

			// Set up the stored procedure parameters.
			spParameters[0] = new SqlParameter("@CountryCode", SqlDbType.VarChar, 10);
			spParameters[0].Value = CountryCode;

			// Create the fund object.
			Country Country = null;

			try
			{
				Country = (Country) this.LoadEntity(spName, spParameters);

				// Test for valid object
				if (Country == null)
				{
					throw new ArgumentException(string.Format("Failed to load Country ({0})", CountryCode));
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
			return Country;
		}

		/// <summary>
		/// Saves the specified Country.
		/// </summary>
		/// <param name="Country">Country.</param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public void Save(Country Country)
		{
			T.E();
			try
			{
				this.SaveEntity(Country);

				CacheHelper cacheHelper =new CacheHelper();
				cacheHelper.FlushCountries();
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Saves the specified Country.
		/// </summary>
		/// <param name="Country">Country.</param>
		/// <param name="transaction">Transaction.</param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public void Save(Country Country, SqlTransaction transaction)
		{
			T.E();
			try
			{
				this.SaveEntity(Country,transaction);

				CacheHelper cacheHelper =new CacheHelper();
				cacheHelper.FlushCountries();
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

			Country Country = null;
			try
			{
				Country = new Country(safeReader.GetString("CountryCode"),
					safeReader.GetString("Country"),
					safeReader.GetString("CurrencyCode"),
					safeReader.GetTimestamp("ts"));
			}
			finally
			{
				T.X();
			}
			return Country;
		}


		/// <summary>
		/// Deletes the Country item.
		/// </summary>
		/// <param name="entity">Entity.</param>
		/// <param name="transaction">Transaction.</param>
		/// <exception cref="ArgumentNullException">The Country object is NULL</exception>
		/// <exception cref="ArgumentException">The entity object is of the wrong type, i.e. not a Country</exception>
		/// <exception cref="DatabaseException">Unable to delete Country item</exception>
		protected override void DeleteEntity(IEntityBase entity, SqlTransaction transaction)
		{
			T.E();
			if (entity == null) throw new ArgumentNullException("entity", "Cannot delete a null Country");
			if (!(entity is Country)) throw new ArgumentException(string.Format("Cannot convert type {0} to type Country for deletion", entity.GetType()), "entity");

			Country Country = (Country) entity;
			const string spName = "usp_CountryDelete";
			SqlParameter[] spParams = new SqlParameter[2];
			spParams[0] = new SqlParameter("@CountryCode", SqlDbType.VarChar, 5);
			spParams[1] = new SqlParameter("@ts", SqlDbType.Timestamp);

			try
			{
				spParams[0].Value = Country.CountryCode;
				spParams[1].Value = Country.TimeStamp;

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
		/// <exception cref="ArgumentNullException">The Country object is NULL</exception>
		/// <exception cref="ArgumentException">The entity object is of the wrong type, i.e. not a Country</exception>
		/// <exception cref="DatabaseException">Unable to append Country item</exception>
		protected override void InsertEntity(IEntityBase entity, SqlTransaction transaction)
		{
			T.E();
			if (entity == null) throw new ArgumentNullException("entity", "Cannot insert a null Country");
			if (!(entity is Country)) throw new ArgumentException(string.Format("Cannot convert type {0} to type Country for insertion", entity.GetType()), "entity");

			// Establish stored proc environment
			const string spName = "usp_CountryCreate";
			Country Country = (Country) entity;
			SqlParameter[] spParams = new SqlParameter[4];
			spParams[0] = new SqlParameter("@CountryCode", SqlDbType.VarChar, 5);
			spParams[1] = new SqlParameter("@CountryName", SqlDbType.VarChar, 50);
			spParams[2] = new SqlParameter("@currencyCode", SqlDbType.VarChar, 10);
			spParams[3] = new SqlParameter("@ts", SqlDbType.Timestamp);
			spParams[3].Direction = ParameterDirection.Output;

			try
			{
				spParams[0].Value = Country.CountryCode;
				spParams[1].Value = Country.CountryName;
				spParams[2].Value = Country.CurrencyCode;

				// Execute insert query
				SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, spParams);

				// Capture return values
				Country.TimeStamp = (byte[]) spParams[3].Value;
				Country.IsDirty = false;
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
		/// <exception cref="ArgumentNullException">The Country object is NULL</exception>
		/// <exception cref="ArgumentException">The entity object is of the wrong type, i.e. not a Country</exception>
		/// <exception cref="DatabaseException">Unable to update Country item</exception>
		protected override void UpdateEntity(IEntityBase entity, SqlTransaction transaction)
		{
			T.E();
			if (entity == null) throw new ArgumentNullException("entity", "Cannot update a null Country");
			if (!(entity is Country)) throw new ArgumentException(string.Format("Cannot convert type {0} to type Country for insertion", entity.GetType()), "entity");

			// Establish stored proc environment
			const string spName = "usp_CountryUpdate";
			Country Country = (Country) entity;
			SqlParameter[] spParams = new SqlParameter[5];

			spParams[0] = new SqlParameter("@currentCountryCode", SqlDbType.VarChar, 10);
			spParams[1] = new SqlParameter("@CountryCode", SqlDbType.VarChar, 5);
			spParams[2] = new SqlParameter("@CountryName", SqlDbType.VarChar, 50);
			spParams[3] = new SqlParameter("@currencyCode", SqlDbType.VarChar, 10);
			spParams[4] = new SqlParameter("@ts", SqlDbType.Timestamp);
			spParams[4].Direction = ParameterDirection.InputOutput;

			try
			{
				spParams[0].Value = Country.CurrentCountryCode;
				spParams[1].Value = Country.CountryCode;
				spParams[2].Value = Country.CountryName;
				spParams[3].Value = Country.CurrencyCode;
				spParams[4].Value = Country.TimeStamp;

				// Execute insert query
				SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, spParams);

				// Capture return values
				Country.TimeStamp = (byte[]) spParams[4].Value;
				Country.IsDirty = false;
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