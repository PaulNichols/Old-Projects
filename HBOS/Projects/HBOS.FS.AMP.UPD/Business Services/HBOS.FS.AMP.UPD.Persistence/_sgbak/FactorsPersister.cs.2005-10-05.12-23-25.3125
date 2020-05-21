using System;
using System.Data;
using System.Data.SqlClient;

using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Types.Factors;

using HBOS.FS.AMP.Entities;

using HBOS.FS.Support.Tex;

using Microsoft.ApplicationBlocks.Data;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// Summary description for FactorsPersister.
	/// </summary>
	public class FactorsPersister : EntityPersister
	{
        #region Local variables

        string m_fundCode = string.Empty;
        SqlTransaction m_transaction = null;

        #endregion 

        #region Constructors

        /// <summary>
        /// Constructor used to initialise the ConnectionString property.
        /// </summary>
        /// <param name="connectionString"></param>
        public FactorsPersister(string connectionString) : base(connectionString) 
		{
			//
			// TODO: Add constructor logic here
			//
		}

        #endregion

        #region Save methods

        /// <summary>
        /// Saves a Factor to the database.
        /// </summary>
        /// <param name="factor">The factor object to save</param>
        /// <param name="fundCode">Asociated fund code</param>
        /// <returns>True if successful, otherwise false</returns>
        /// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
        /// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
        /// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
        /// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
        public void Save(Factor factor, string fundCode)
        {
            T.E();
            try
            {
                if (factor.IsDirty || factor.IsNew)
                {
                    m_fundCode = fundCode;
                    this.SaveEntity(factor);
                }
            }
            finally
            {
                T.X();
            }
        }

        /// <summary>
        /// Saves a Factor to the database.
        /// </summary>
        /// <param name="factor">The factor object to save</param>
        /// <param name="fundCode">Asociated fund code</param>
        /// <param name="transaction">The transaction context to save in</param>
        /// <returns>True if successful, otherwise false</returns>
        /// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
        /// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
        /// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
        /// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
        public void Save(Factor factor, string fundCode, SqlTransaction transaction)
        {
            T.E();
            try
            {
                if (factor.IsDirty || factor.IsNew)
                {
                    m_fundCode = fundCode;
                    this.SaveEntity(factor, transaction);
                }
            }
            finally
            {
                T.X();
            }
        }

        /// <summary>
        /// Insert a new fund factor into the database
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
        /// <remarks>Any modifications to factors are treated as new so we always Insert.</remarks>
        protected override void InsertEntity(IEntityBase entity, SqlTransaction transaction)
        {
            // TODO: Interogate entity to capture the factor type 
            T.E();

            m_transaction = transaction;

			try
			{
				if (entity == null) throw new ArgumentNullException("entity", "Cannot insert null Factor");
				if (!(entity is Factor)) throw new ArgumentException("Incorrect type, expecting Factor, was "+entity.GetType().ToString(), "entity");
				Factor factor = (Factor) entity;

				IFactor factorIf = factor;

				//if the factor is zero and no other valid properties set then don't bother to insert one, it just means one has been created
				//due to property access but isn't actually being used.

				if (factorIf.IsSet())
				{

					//introduced as Asset maintenance screen may set  data on an asset fund that is not set up properly.
					//should we handle this situation differently though?

					//PJN Removed because the asset maintenance screen does the validation if needed
//					if (factorIf.IsValid()) 
//					{
						//
						// TODO: Look at a more intuituve way to apply this!
						//
						// Test the factor type and call the relevant method
						if ( factor is XFactor )
						{
							this.insertXFactor((XFactor)factor);
						}
						else if ( factor is ScalingFactor )
						{
							this.insertScalingFactor((ScalingFactor)factor);
						}
						else if ( factor is RevaluationFactor )
						{
							this.insertRevaluationFactor((RevaluationFactor)factor);
						}
						else if ( factor is TaxProvisionEstimate )
						{
							this.insertTaxProvisionFactor((TaxProvisionEstimate)factor);
						}
						else
						{
							throw new ArgumentException ("entity is not of a valid factor type");
						}
					//}
//					else
//					{
//						if ( factor is XFactor )
//						{
//							//if you're getting this you should be validating data before attempting to save!
//							//(unless you're relying on catching this exception as your validation)
//							throw new InvalidXFactorException ("XFactor data is invalid for save");
//						}
//						else if ( factor is ScalingFactor )
//						{
//							//if you're getting this you should be validating data before attempting to save!
//							//(unless you're relying on catching this exception as your validation)
//							throw new InvalidScalingFactorException ("Scaling Factor data is invalid for save");
//						}
//						else if ( factor is RevaluationFactor )
//						{
//							//if you're getting this you should be validating data before attempting to save!
//							//(unless you're relying on catching this exception as your validation)
//							throw new InvalidRevaluationFactorException ("Revaluation Factor data is invalid for save");
//						}
//						else if ( factor is TaxProvisionEstimate )
//						{
//							//if you're getting this you should be validating data before attempting to save!
//							//(unless you're relying on catching this exception as your validation)
//							throw new InvalidTaxProvisionEstimateException ("Tax Provision Estimate data is invalid for save");
//						}
//						else
//						{
//							throw new ArgumentException ("entity is not of a valid factor type");
//						}
//					}
				}
			}
			finally
			{
				T.X();
			}
        }

		/// <summary>
		/// This always just calls insert, as we never update an existing record,
		/// we insert a new record and mark all others for this days date as deleted
		/// </summary>
		/// <param name="entity">Entity to update.</param>
		/// <param name="transaction">The transaction context to save in</param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		/// <exception cref="ArgumentNullException">Entity was passed in as a null</exception>
		/// <exception cref="ArgumentException">Invalid entity type returned</exception>
		/// <remarks>Any modifications to factors are treated as new so we always Insert.</remarks>
		protected override void UpdateEntity(IEntityBase entity, SqlTransaction transaction)
		{
			T.E();
			try
			{
				InsertEntity (entity, transaction);
			}
			finally
			{
				T.X();
			}
		}

        #endregion

        #region Insert factor methods

        /// <summary>
        /// Persist the XFactor information back to the database.
        /// </summary>
        /// <remarks>
        /// We always create a new factor - the sp sets the deleted flag to the previous value and archives it
        /// </remarks>
        /// <param name="factor">X Factor object</param>
        private void insertXFactor(XFactor factor)
        {
            T.E();
            SqlParameter[] spParams = null;
            const string createSp = "usp_XFactorCreate";
            try
            {
                // Create parameter object
                spParams = new SqlParameter[4];

                // Build parameters
                spParams[0] = new SqlParameter( "@cHiPortfolioCode", SqlDbType.Char, 10 );
                spParams[1] = new SqlParameter( "@dXFactor", SqlDbType.Decimal );
                spParams[2] = new SqlParameter( "@sNarrative", SqlDbType.VarChar, 1000 );
                spParams[3] = new SqlParameter( "@newFactorID", SqlDbType.Int );

                // Assign values to parameters
                spParams[0].Value = m_fundCode;
                spParams[1].Value = factor.RatioValue; //.CalculateEffect();
                spParams[2].Value = factor.Description;
                spParams[3].Direction = ParameterDirection.Output;

                // Call create stored procedure
                SqlHelper.ExecuteNonQuery(m_transaction, CommandType.StoredProcedure, createSp, spParams);

                // Capture the factor id
                factor.FactorID = (int)spParams[3].Value;
            }            
            finally
            {
                T.X();
            }
        }

        /// <summary>
        /// Persist the Scaling Factor information back to the database.
        /// </summary>
        /// <param name="factor">Scaling Factorr object</param>
        private void insertScalingFactor(ScalingFactor factor)
        {
            T.E();
            SqlParameter[] spParams = null;
            const string createSp = "usp_ScalingFactorCreate";
            try
            {
                // Create parameter object
                spParams = new SqlParameter[3];

                // Build parameters
                spParams[0] = new SqlParameter( "@cHiPortfolioCode", SqlDbType.Char, 10 );
                spParams[1] = new SqlParameter( "@dScalingFactor", SqlDbType.Decimal );
                spParams[2] = new SqlParameter( "@newFactorID", SqlDbType.Int );

                // Assign values to parameters
                spParams[0].Value = m_fundCode;
                spParams[1].Value = factor.RatioValue; //factor.CalculateEffect();
                spParams[2].Direction = ParameterDirection.Output;

                // Call create stored procedure
                SqlHelper.ExecuteNonQuery(m_transaction, CommandType.StoredProcedure, createSp, spParams);

                // Capture the factor id
                factor.FactorID = (int)spParams[2].Value;
            }            
            finally
            {
                T.X();
            }
        }

        
        /// <summary>
        /// Persist the Scaling Factor information back to the database.
        /// </summary>
        /// <param name="factor">Scaling Factorr object</param>
        private void insertRevaluationFactor(RevaluationFactor factor)
        {
            T.E();
            SqlParameter[] spParams = null;
            const string createSp = "usp_RevaluationFactorCreate";
            try
            {
                // Create parameter object
                spParams = new SqlParameter[4];

                // Build parameters
                spParams[0] = new SqlParameter( "@cHiPortfolioCode", SqlDbType.Char, 10 );
                spParams[1] = new SqlParameter( "@dTotalChange", SqlDbType.Decimal );
				spParams[2] = new SqlParameter( "@endDate", SqlDbType.SmallDateTime );
                spParams[3] = new SqlParameter( "@newFactorID", SqlDbType.Int );

                // Assign values to parameters
                spParams[0].Value = m_fundCode;
                spParams[1].Value = factor.RatioValue;
				spParams[2].Value = factor.EndDate;
                spParams[3].Direction = ParameterDirection.Output;

                // Call create stored procedure
                SqlHelper.ExecuteNonQuery(m_transaction, CommandType.StoredProcedure, createSp, spParams);

                // Capture the factor id
                factor.FactorID = (int)spParams[3].Value;
            }            
            finally
            {
                T.X();
            }
        }

        /// <summary>
        /// Persist the Scaling Factor information back to the database.
        /// </summary>
        /// <param name="factor">Scaling Factorr object</param>
        private void insertTaxProvisionFactor(TaxProvisionEstimate factor)
        {
            T.E();
            SqlParameter[] spParams = null;
            const string createSp = "usp_TaxProvisionFactorCreate";
            try
            {
                // Create parameter object
                spParams = new SqlParameter[3];

                // Build parameters
                spParams[0] = new SqlParameter( "@cHiPortfolioCode", SqlDbType.Char, 10 );
                spParams[1] = new SqlParameter( "@dProvisionEstimate", SqlDbType.Decimal );
                spParams[2] = new SqlParameter( "@newFactorID", SqlDbType.Int );

                // Assign values to parameters
                spParams[0].Value = m_fundCode;
                spParams[1].Value = factor.RatioValue; //.CalculateEffect();
                spParams[2].Direction = ParameterDirection.Output;

                // Call create stored procedure
                SqlHelper.ExecuteNonQuery(m_transaction, CommandType.StoredProcedure, createSp, spParams);

                // Capture the factor id
                factor.FactorID = (int)spParams[2].Value;
            }            
            finally
            {
                T.X();
            }
        }

        #endregion
	}
}
