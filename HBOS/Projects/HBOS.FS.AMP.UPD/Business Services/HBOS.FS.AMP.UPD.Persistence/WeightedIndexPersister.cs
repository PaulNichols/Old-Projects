//using HBOS.FS.AMP.UPD.DataAccess;
using System;
using System.Data;
using System.Data.SqlClient;
using HBOS.FS.AMP.Entities;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.WeightedIndices;
using HBOS.FS.Support.Tex;
using Microsoft.ApplicationBlocks.Data;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// Summary description for WeightedIndexPersister.
	/// </summary>
	public class WeightedIndexPersister : AssetFundIndexWeightedPersister
	{
		#region Constructors

		/// <summary>
		/// 
		/// </summary>
		/// <param name="connectionString"></param>
		public WeightedIndexPersister(string connectionString) : base(connectionString)
		{
		}

		#endregion

		#region Member variables

		private enum WeightedIndexLoadType
		{
			unknown,
			full,
			assetStaticMaintenance
		}

		private WeightedIndexLoadType m_loadType = WeightedIndexLoadType.unknown;
		private const string m_guiImportRef = "marketValueSplitGUI";

		#endregion

		#region Load Methods

		/// <summary>
		/// Creates an asset fund weighted index from the supplied data
		/// </summary>
		/// <param name="reader">The reader containing the data.</param>
		/// <returns></returns>
		/// <exception cref="SchemaMismatchException">Column missing from data returned by stored procedure</exception>
		protected override object CreateEntity(SafeDataReader reader)
		{
			T.E();
			WeightedIndex newWeightedIndex = null;
			try
			{
				switch (m_loadType)
				{
					case WeightedIndexLoadType.full:


						newWeightedIndex = new WeightedIndex(reader.GetInt32("marketIndexID"), m_assetFundCode, reader.GetString("indexName"), reader.GetDecimal("marketMovement"), !(reader.IsNull("marketMovement")), reader.GetInt64("importedIndexValueImportID"), reader.GetDecimal("currencyMovement"), !(reader.IsNull("currencyMovement")), reader.GetInt64("currencyRatesImportID"), reader.GetString("CountryCode"), reader.GetString("CurrencyCode"), reader.GetString("Country"), reader.GetDecimal("proportion"), !(reader.IsNull("proportion")), reader.GetInt64("importID"), reader.GetBoolean("fromAuthorisedAssetFundDetails"), reader.GetTimestamp("ts"));
						break;

					case WeightedIndexLoadType.assetStaticMaintenance:

						newWeightedIndex = new WeightedIndex();
						newWeightedIndex.MarketIndexID = reader.GetInt32("marketIndexID");
						newWeightedIndex.IndexName = reader.GetString("indexName");
						newWeightedIndex.CountryCode = reader.GetString("countryCode");
						newWeightedIndex.CurrencyCode = reader.GetString("currencyCode");
						newWeightedIndex.TimeStamp = reader.GetTimestamp("ts");

						break;

					default:
						throw new ArgumentException("Unknown weighted index load type");

				}
			}
			finally
			{
				T.X();
			}
			return newWeightedIndex;
		}

		/// <summary>
		/// Loads a collection of composite weightings for a particular asset fund, 
		/// specfied by its asset fund code.
		/// </summary>
		/// <param name="assetFundCode">The asset fund code</param>
		/// <param name="ignorePricing">For static data pricing info not required</param>
		/// <returns>A collection of the requested asset fund weighted index objects</returns>
		/// <exception cref="DatabaseException">Unable to load company</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="SchemaMismatchException">Column missing from data returned by stored procedure</exception>
		public AssetMovementConstituentCollection LoadWeightingIndicesForAssetFund(string assetFundCode, bool ignorePricing)
		{
			T.E();
			const string loadSp = "usp_AssetFundIndexWeightingsListForAssetFundID";

			SqlParameter[] parameters = new SqlParameter[2];
			AssetMovementConstituentCollection movements = null;

			// Set up the parameters.
			try
			{
				// Select by parameter
				parameters[0] = new SqlParameter("@cAssetFundID", SqlDbType.VarChar, 8);
				parameters[0].Value = assetFundCode;

				parameters[1] = new SqlParameter("@bIgnoreAuthorisedPricing", SqlDbType.Bit);
				parameters[1].Value = ignorePricing ? 1 : 0;

				// Create the funds collection.
				movements = new AssetMovementConstituentCollection();
				m_assetFundCode = assetFundCode; //set this here so we can then pick up again in CreateEntity
				m_loadType = WeightedIndexLoadType.full;
				this.LoadEntityCollection(loadSp, parameters, movements);
			}
			finally
			{
				T.X();
			}
			return movements;
		}

		/// <summary>
		/// creates full weighted movement objects (weighted indices or composite weightings)
		/// but with data only partially completed
		/// </summary>
		/// <returns></returns>
		public AssetMovementConstituentCollection LoadMarketIndicesForAssetFundStaticMaintenance()
		{
			T.E();
			const string loadSp = "usp_MarketIndicesList";

			AssetMovementConstituentCollection movements = null;

			// Set up the parameters.
			try
			{
				// Create the funds collection.
				movements = new AssetMovementConstituentCollection();
				m_loadType = WeightedIndexLoadType.assetStaticMaintenance;
				this.LoadEntityCollection(loadSp, movements);
			}
			finally
			{
				T.X();
			}
			return movements;
		}

		#endregion

		#region Save Methods

		/// <summary>
		/// This routine receives a collection of indices to be peristed to the datasource
		/// </summary>
		/// <param name="movements">List of users to save</param>
		/// <param name="companyCode"></param>
		/// <param name="txn">Sql transaction within which to enlist</param>
		public  void Save(AssetMovementConstituentCollection movements, string companyCode,SqlTransaction txn)
		{
			T.E();
			try
			{
				int importID = this.UpdateImportIDForWeightedMovements(movements, companyCode, txn);
				this.SaveEntityCollection(movements, txn);
				if (importID > 0)
				{
					this.ActivateWeightedIndices(txn, importID);
				}
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// This routine receives a collection of indices to be peristed to the datasource
		/// </summary>
		/// <param name="movements">List of users to save</param>
		/// <param name="txn">Sql transaction within which to enlist</param>
		public override void Save(AssetMovementConstituentCollection movements, SqlTransaction txn)
		{
			T.E();
			try
			{
				//we actually need the company code and  should be invoking the save method that requires it (above).
				//therefore raise an error and insist the correct method is called
				throw new NotImplementedException ("Company code required for weighted index save, therefore this method cannot be invoked.");
			}
			finally
			{
			}
		}

		/// <summary>
		/// Where a user has manually edited weighted indices (not composite weightings) then invoke this
		/// method. This gets a new import id, sets it for the set, and activates the import reference
		/// </summary>
		/// <param name="movements"></param>
		/// <param name="txn"></param>
		/// <param name="companyCode"></param>
		/// <returns>0 if no importids updated (ie there have been no manual updates), the importid if there has been</returns>
		private int UpdateImportIDForWeightedMovements(HBOS.FS.AMP.UPD.Types.AssetFunds.AssetMovementConstituentCollection movements,string companyCode ,SqlTransaction txn)
		{
			T.E();
			int importReference = 0;
			try
			{
				if (movements.Count > 0)
				{
					//if (movements[0] is WeightedIndex)
					//{
//						if (((WeightedIndex) movements[0]).ImportID == 0)
//						{
//							//business logic (in asset fund) - if one is set to zero then whole
//							//collection is set set to zero, and we get a new import id for all.
//
//							//only weighted indices may be updated (not composite weightings)
//							ImportPersister importSource = new ImportPersister(ConnectionString);
//							bool successFlag;
//							importReference = importSource.SaveImportSource(out successFlag, m_guiImportRef,companyCode, txn);
//							//succesflag not used - an exception will be thrown if invalid
//
//							for (int i = 0; i < movements.Count; i++)
//							{
//								((WeightedIndex) movements[i]).ImportID = importReference;
//							}
//
//						}
					//}
				}
			}
			catch (Exception ex)
			{
				throw ex;

			}
			finally
			{
				T.X();
			}
			return importReference;

		}

		/// <summary>
		/// Inserts a weighted index
		/// </summary>
		/// <param name="entity">Entity to insert.</param>
		/// <param name="transaction">The transaction context to save in</param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		/// <exception cref="ArgumentNullException">Entity was passed in as a null</exception>
		/// <exception cref="ArgumentException">Invalid entity type returned</exception>
		protected override void InsertEntity(IEntityBase entity, SqlTransaction transaction)
		{
			T.E();
			if (entity == null) throw new ArgumentNullException("entity", "Cannot insert null WeightedIndex ");
			if (!(entity is WeightedIndex)) throw new ArgumentException("Incorrect type, expecting WeightedIndex, was " + entity.GetType().ToString(), "entity");

			SqlParameter[] spParams = null;
			string createSp = String.Empty;

			try
			{
				WeightedIndex weightedIndex = (WeightedIndex) entity;

				/*** Drop I code **/
				// 1. First, generate a new import source and return the import ID
				// Note. We do this once per collection and not for each item.
				if (!m_valuationPointSet)
				{
					SetValuationPoint();
				}
				/*** Drop I code **/

				//
				// 2. Now create a new index weighting or composite row in the DB
				//
				// Same as per Drop I

				createSp = "usp_AssetFundIndexWeightingsCreate";

				// Create parameter object
				spParams = new SqlParameter[5];

				// Build parameters
				spParams[0] = new SqlParameter("@iImportID", SqlDbType.BigInt);
				spParams[1] = new SqlParameter("@cAssetFundID", SqlDbType.Char, 8);
				spParams[2] = new SqlParameter("@iMarketIndexID", SqlDbType.Int);
				spParams[3] = new SqlParameter("@dWeighting", SqlDbType.Decimal);
				spParams[4] = new SqlParameter("@dtvaluationPoint", SqlDbType.DateTime);

				// Set additional parameter attributes
				spParams[3].Precision = 10;
				spParams[3].Scale = 6;

				// Assign values to parameters
				spParams[0].Value = weightedIndex.ImportID;
				spParams[1].Value = weightedIndex.AssetFundCode;
				spParams[2].Value = weightedIndex.MarketIndexID;
				spParams[3].Value = weightedIndex.Proportion;
				spParams[4].Value = m_valuationPoint;


				// Call the create stored procedure
				SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, createSp, spParams);

				weightedIndex.IsNew = false;
				weightedIndex.IsDirty = false;

			}
			finally
			{
				T.X();
			}
		}

		#endregion
	}
}