//using HBOS.FS.AMP.UPD.DataAccess;
using System;
using System.Data;
using System.Data.SqlClient;
using HBOS.FS.AMP.Entities;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.WeightedIndices;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// Summary description for CompositeWeightingPersister.
	/// </summary>
	public class CompositeWeightingPersister : AssetFundIndexWeightedPersister
	{
		#region Constructors

		/// <summary>
		/// 
		/// </summary>
		/// <param name="connectionString"></param>
		public CompositeWeightingPersister(string connectionString) : base(connectionString)
		{
		}

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
			CompositeWeighting newCompositeWeighting = null;
			try
			{
				newCompositeWeighting = new CompositeWeighting(reader.IsNull("fundName") ? "" : reader.GetString("fundName"), reader.GetString("hiPortfolioCode"), reader.GetDecimal("priceMovement"), reader.GetDecimal("proportion"), reader.GetBoolean("isAuthorised"));
			}
			finally
			{
				T.X();
			}
			return newCompositeWeighting;
		}

		/// <summary>
		/// Loads a collection of composite weightings for a particular asset fund, 
		/// specfied by its asset fund code.
		/// </summary>
		/// <param name="assetFundCode">The asset fund code</param>
		/// <param name="ignorePricing">For static data pricing info not required</param>
		/// <returns>A collection of the requested composite weightings</returns>
		/// <exception cref="DatabaseException">Unable to load company</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="SchemaMismatchException">Column missing from data returned by stored procedure</exception>
		public AssetMovementConstituentCollection LoadCompositeWeightingsForAssetFund(string assetFundCode, bool ignorePricing)
		{
			T.E();
			const string loadSp = "usp_AssetFundGetCompositionForAssetFundID";

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
				this.LoadEntityCollection(loadSp, parameters, movements);
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
			if (entity == null) throw new ArgumentNullException("entity", "Cannot insert null Composite Weighting");
			if (!(entity is CompositeWeighting)) throw new ArgumentException("Incorrect type, expecting CompositeWeighting, was " + entity.GetType().ToString(), "entity");

			//SqlParameter[] spParams = null;
		//	string createSp = String.Empty;

			try
			{
			//	CompositeWeighting composite = (CompositeWeighting) entity;

				/*** Drop I code **/
				// 1. First, generate a new import source and return the import ID
				// Note. We do this once per collection and not for each item.
				if (!m_valuationPointSet)
				{
					SetValuationPoint();
				}
				/*** Drop I code **/

				//
				// 2. Now create a new composite row in the DB
				//
				throw new NotImplementedException("Composite Weightings unavailable for static data update. Import is via separate import stored procedures, not through a persister.");


				// Call the create stored procedure
				//SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, createSp, spParams);
			}
			finally
			{
				T.X();
			}
		}

		#endregion
	}
}
