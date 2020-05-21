//using HBOS.FS.AMP.UPD.DataAccess;
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
	/// The class to use for persisting or retrieving Weightedindex, CompositeWeighting and
	/// WeightedMovementCollection objects.
	/// </summary>
	public class AssetFundIndexWeightedPersister : EntityPersister
	{
		#region Local variables

		/// <summary>
		/// the import id
		/// </summary>
		protected int m_snapshotID = 0;

		/// <summary>
		/// the unique id for the asset fund needed so we can set on load then pick up again in CreateEntity
		/// </summary>
		protected string m_assetFundCode = null;

		/// <summary>
		/// valuation point
		/// </summary>
		protected DateTime m_valuationPoint = DateTime.MinValue;

		/// <summary>
		/// valuation point set
		/// </summary>
		protected bool m_valuationPointSet = false;

		#endregion        

		#region Constructors

		/// <summary>
		/// Constructor used to initialise the ConnectionString property.
		/// </summary>
		/// <param name="connectionString"></param>
		public AssetFundIndexWeightedPersister(string connectionString) : base(connectionString)
		{
		}

		#endregion

		#region Methods

		/// <summary>
		/// This routine receives a collection of indices to be peristed to the datasource
		/// </summary>
		/// <param name="movements">List of users to save</param>
		/// <param name="txn">Sql transaction within which to enlist</param>
		public virtual void Save(AssetMovementConstituentCollection movements, SqlTransaction txn)
		{
			T.E();
			try
			{
				this.SaveEntityCollection(movements, txn);
			}
			finally
			{
				T.X();
			}
		}


		/// <summary>
		/// Override to Update the entity in the database
		/// </summary>
		/// <param name="entity">Entity to update.</param>
		/// <param name="transaction">The transaction context to save in</param>
		protected override void UpdateEntity(IEntityBase entity, SqlTransaction transaction)
		{
			//we always insert a new entity as opposed to modify (see SR) .
			InsertEntity(entity, transaction);
		}


//		/// <summary>
//		/// Assign the valuation date/time for the new market split entry
//		/// </summary>
//		protected void SetValuationPoint()
//		{
//			T.E();
//			m_valuationPoint = DateTime.Now.Date;
//			m_valuationPointSet = true;
//			T.X();
//		}

		/// <summary>
		/// Activate all weighted indices asociated with the parsed import ID
		/// </summary>
		/// <param name="transaction">The transaction used for the persistence, allowing transactional rollback</param>
		/// <param name="snapshotID">Import identifyer to be actiavted</param>
		/// <returns></returns>
		/// <exception cref="DatabaseException">Unable to load company</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		public bool ActivateWeightedIndices(SqlTransaction transaction, int snapshotID)
		{
			T.E();
			const string sp = "usp_AssetFundIndexWeightingsActivate";
			bool retval = false;

			// Create parameter object
			SqlParameter[] spParams = new SqlParameter[5];

			// Build parameters
			spParams[0] = new SqlParameter("@SnapshotID", SqlDbType.BigInt);

			// Assign values to parameters
			spParams[0].Value = snapshotID;

			// Call the stored procedure
			try
			{
				SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, sp, spParams);
				retval = true;
			}
			catch (SqlException ex)
			{
				ThrowDBException(ex, sp, spParams);
			}
			finally
			{
				T.X();
			}

			return retval;
		}

		#endregion

		#region Members

		/// <summary>
		/// The import ID used when saving modified and new items.
		/// </summary>
		/// <remarks>
		/// We will be mimmicking a data import, so we must hold the Database generated import ID
		/// </remarks>
		public int SnapshotID
		{
			get { return m_snapshotID; }
			set { m_snapshotID = value; }
		}

		#endregion
	}
}