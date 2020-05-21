using System;
using System.Data;
using System.Data.SqlClient;
using HBOS.FS.AMP.Entities;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.Support.Tex;
using Microsoft.ApplicationBlocks.Data;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// Summary description for MarketIndexPersister.
	/// </summary>
	public class StockMarketPersister : EntityPersister
	{
        
        #region Constructor
        /// <summary>
        /// Use this class for retrieving all Market Index objects
        /// Default constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public StockMarketPersister(string connectionString) : base(connectionString)
		{
		}
        #endregion

        #region Public methods

		/// <summary>
		/// Loads the specified market index.
		/// </summary>
		/// <param name="marketIndex">Market index.</param>
		/// <returns></returns>
		public StockMarket Load(int marketIndex)
		{
			T.E();

			const string spName = "usp_StockMarketGetStaticData";
			SqlParameter[] spParameters = new SqlParameter[1];

			// Set up the stored procedure parameters.
			spParameters[0] = new SqlParameter("@marketIndex", SqlDbType.Int);
			spParameters[0].Value = marketIndex;

			StockMarket stockMarket= null;

			try
			{
				stockMarket = (StockMarket) this.LoadEntity(spName, spParameters);

				// Test for valid object
				if (stockMarket == null)
				{
					throw new ArgumentException(string.Format("Failed to load Stock Market ({0})", marketIndex));
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
			return stockMarket;
		}

		/// <summary>
		/// Loads all stock markets related to the country code.
		/// </summary>
		/// <param name="countryCode">Country code.</param>
		/// <returns></returns>
		public  StockMarketCollection LoadForCountryCode(string countryCode)
		{
			T.E();
			StockMarketCollection stockMarkets = new StockMarketCollection();
			SqlParameter[] parameters=new SqlParameter[1];
			parameters[0] = new SqlParameter("@CountryCode", SqlDbType.Char,3); 
			parameters[0].Value=countryCode;
			this.LoadEntityCollection("usp_StockMarketListForCountryCode",parameters,stockMarkets);		
			T.X();

			return stockMarkets;
		}

        /// <summary>
        /// Load the collection of market indices from the DB
        /// </summary>
        /// <returns type="MarketIndexCollection">A collection of market indices</returns>
        /// <exception cref="DatabaseException">Unable to load companies</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		public StockMarketCollection LoadMarketIndices()
		{
        	T.E();
        	StockMarketCollection stockMarkets = new StockMarketCollection();
			this.LoadEntityCollection("usp_MarketIndicesList",stockMarkets);		
			T.X();

			return stockMarkets;
		}

		
		/// <summary>
		/// Saves the specified Stock Market.
		/// </summary>
		/// <param name="stockMarket">Stock Market to save.</param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public void Save(StockMarket stockMarket)
		{
			T.E();
			try
			{
				this.SaveEntity(stockMarket);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Saves the specified Country.
		/// </summary>
		/// <param name="stockMarket">Stock Market to save.</param>
		/// <param name="transaction">Transaction.</param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public void Save(StockMarket stockMarket, SqlTransaction transaction)
		{
			T.E();
			try
			{
				this.SaveEntity(stockMarket, transaction);
			}
			finally
			{
				T.X();
			}
		}

		#endregion

		#region Entity methods

		/// <summary>
		/// Deletes the Stock Market item.
		/// </summary>
		/// <param name="entity">Entity.</param>
		/// <param name="transaction">Transaction.</param>
		/// <exception cref="ArgumentNullException">The Stock Market object is NULL</exception>
		/// <exception cref="ArgumentException">The entity object is of the wrong type, i.e. not a Country</exception>
		/// <exception cref="DatabaseException">Unable to delete StockMarket item</exception>
		protected override void DeleteEntity(IEntityBase entity, SqlTransaction transaction)
		{
			T.E();
			if (entity == null) throw new ArgumentNullException("entity", "Cannot delete a null Stock Market");
			if (!(entity is StockMarket)) throw new ArgumentException(string.Format("Cannot convert type {0} to type Stock Market for deletion", entity.GetType()), "entity");

			StockMarket stockMarket = (StockMarket) entity;
			const string spName = "usp_StockMarketDelete";
			SqlParameter[] spParams = new SqlParameter[2];
			spParams[0] = new SqlParameter("@marketIndex", SqlDbType.Int);
			spParams[1] = new SqlParameter("@ts", SqlDbType.Timestamp);

			try
			{
				spParams[0].Value = stockMarket.MarketIndexID;
				spParams[1].Value = stockMarket.TimeStamp;

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
		/// <exception cref="ArgumentNullException">The Stock Market object is NULL</exception>
		/// <exception cref="ArgumentException">The entity object is of the wrong type, i.e. not a Stock Market</exception>
		/// <exception cref="DatabaseException">Unable to append StockMarket item</exception>
		protected override void InsertEntity(IEntityBase entity, SqlTransaction transaction)
		{
			T.E();
			if (entity == null) throw new ArgumentNullException("entity", "Cannot insert a null Stock Market");
			if (!(entity is StockMarket)) throw new ArgumentException(string.Format("Cannot convert type {0} to type Stock Market for insertion", entity.GetType()), "entity");

			// Establish stored proc environment
			const string spName = "usp_StockMarketCreate";
			StockMarket stockMarket = (StockMarket) entity;
			SqlParameter[] spParams = new SqlParameter[5];
			spParams[0] = new SqlParameter("@indexName", SqlDbType.VarChar, 50);
			spParams[1] = new SqlParameter("@global", SqlDbType.Bit);
			spParams[2] = new SqlParameter("@countryCode", SqlDbType.VarChar, 5);
			spParams[3] = new SqlParameter("@marketIndexId", SqlDbType.Int);
			spParams[3].Direction = ParameterDirection.Output;
			spParams[4] = new SqlParameter("@ts", SqlDbType.Timestamp);
			spParams[4].Direction = ParameterDirection.Output;

			try
			{
				spParams[0].Value = stockMarket.IndexName;
				spParams[1].Value = stockMarket.Global;
				spParams[2].Value = stockMarket.CountryCode;

				// Execute insert query
				SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, spParams);

				// Capture return values
				stockMarket.MarketIndexID = (int) spParams[3].Value;
				stockMarket.TimeStamp = (byte[]) spParams[4].Value;
				stockMarket.IsDirty = false;
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
			if (entity == null) throw new ArgumentNullException("entity", "Cannot update a null Stock Market");
			if (!(entity is StockMarket)) throw new ArgumentException(string.Format("Cannot convert type {0} to type Stock Market for insertion", entity.GetType()), "entity");

			// Establish stored proc environment
			const string spName = "usp_StockMarketUpdate";
			StockMarket stockMarket = (StockMarket) entity;
			SqlParameter[] spParams = new SqlParameter[5];

			spParams[0] = new SqlParameter("@marketIndexId", SqlDbType.Int);
			spParams[1] = new SqlParameter("@indexName", SqlDbType.VarChar, 50);
			spParams[2] = new SqlParameter("@global", SqlDbType.Bit);
			spParams[3] = new SqlParameter("@countryCode", SqlDbType.VarChar, 5);
			spParams[4] = new SqlParameter("@ts", SqlDbType.Timestamp);
			spParams[4].Direction = ParameterDirection.InputOutput;

			try
			{
				spParams[0].Value = stockMarket.MarketIndexID;
				spParams[1].Value = stockMarket.IndexName;
				spParams[2].Value = stockMarket.Global;
				spParams[3].Value = stockMarket.CountryCode;
				spParams[4].Value = stockMarket.TimeStamp;

				// Execute insert query
				SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, spParams);

				// Capture return values
				stockMarket.TimeStamp = (byte[]) spParams[4].Value;
				stockMarket.IsDirty = false;
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
		/// Creates the entity from the data in the reader.
		/// </summary>
		/// <param name="safeReader">The reader to build the entity from.</param>
		/// <returns></returns>
		protected override object CreateEntity(SafeDataReader safeReader)
		{
			T.E();
			StockMarket newStockMarket = new StockMarket(
				safeReader.GetString("countryCode"),
				safeReader.GetString("indexName"),
				safeReader.GetInt32("marketIndexID"),
				safeReader.GetBoolean("global"),
				safeReader.GetTimestamp("ts")
				);
			T.X();

			return newStockMarket;		
		}

		#endregion

	}
}
