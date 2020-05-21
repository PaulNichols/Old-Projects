using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using HBOS.FS.AMP.Entities;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.Factors;
using HBOS.FS.AMP.UPD.Types.FundGroups;
using HBOS.FS.AMP.UPD.Types.Funds;
using HBOS.FS.Support.Tex;
using Microsoft.ApplicationBlocks.Data;

namespace HBOS.FS.AMP.UPD.Persistence
{
    /// <summary>
    /// The class to use for persisting or retrieving Fund objects and FundCollection objects.
    /// </summary>
    public abstract class FundPersister : EntityPersister
    {

        #region ExceptionHandling

        private string additionalErrorText = string.Empty;

        /// <summary>
        /// Gets the concurrency violation exception message.
        /// </summary>
        /// <returns></returns>
        protected override string GetConcurrencyViolationExceptionMessage()
        {
            return string.Format("A concurrency violation occurred whilst saving {0}",additionalErrorText);
        }

        /// <summary>
        /// Gets the constraint violation duplicate key exception message.
        /// </summary>
        /// <returns></returns>
        protected override string GetConstraintViolationDuplicateKeyExceptionMessage()
        {
            return string.Format("A duplicate key violation occurred whilst saving {0}",additionalErrorText);
        }

        /// <summary>
        /// Gets the constraint violation exception message.
        /// </summary>
        /// <returns></returns>
        protected override string GetConstraintViolationExceptionMessage()
        {
            return string.Format("A constraint violation occurred whilst saving {0}",additionalErrorText);
        }

        /// <summary>
        /// Gets the database exception message.
        /// </summary>
        /// <returns></returns>
        protected override string GetDatabaseExceptionMessage()
        {
            return string.Format("A database exception occurred in fund persister. {0}",additionalErrorText);
        }

        /// <summary>
        /// Gets the null parameter exception message.
        /// </summary>
        /// <returns></returns>
        protected override string GetNullParameterExceptionMessage()
        {
            return string.Format("A null parameter was passed to a stored procedure in fund persister. {0}",additionalErrorText);
        }


        #endregion ExceptionHandling

        #region Drop II

        #region Constructors

        /// <summary>
        /// Constructor used to initialise the ConnectionString property.
        /// </summary>
        /// <param name="connectionString">The connection string to use for data access.</param>
        protected FundPersister(string connectionString) : base(connectionString)
        {
        	T.E();
            T.X();
        }

        #endregion

		#region Member variables

		//Load all funds for company will create a cache of asset funds to speed up load process
		//Other load methods will not

		/// <summary>
		/// 
		/// </summary>
		protected static AssetFundCollection m_assetFunds = new AssetFundCollection();
		/// <summary>
		/// 
		/// </summary>
		protected bool m_useAssetFundCache = false;

    	private static Hashtable m_Holidays;

    	#endregion


		#region Helper methods

		/// <summary>
		/// Loads the Parent Asset Fund for the current fund.
		/// </summary>
		/// <param name="assetFundID">Asset fund ID.</param>
		/// <returns></returns>
		protected AssetFund loadAssetFundForFund(string assetFundID)
		{
			AssetFund parentAssetFund = null;

			//Attempt to retrieve the asset fund for the fund from the cache
			if (m_useAssetFundCache && m_assetFunds.Count > 0)
			{
				//Get the asset fund from the cache
				foreach (AssetFund assetFund in m_assetFunds)
				{
					if (assetFund.AssetFundCode.Trim() == assetFundID.Trim())
					{
						parentAssetFund = assetFund;
						break;
					}
				}
			}

			if (parentAssetFund == null)
			{
				parentAssetFund = LoadParentAssetFund(assetFundID);
				//add fund to cache
				m_assetFunds.Add(parentAssetFund);
			}

			return parentAssetFund;
		}

		/// <summary>
		/// Loads the parent asset fund.
		/// </summary>
		/// <param name="assetFundID">Asset fund ID.</param>
		/// <returns></returns>
    	protected virtual AssetFund LoadParentAssetFund(string assetFundID)
    	{
    		AssetFund parentAssetFund;
    		//Load the asset fund specifically for this fund
    		AssetFundPricingPersister assetFundPersister = new AssetFundPricingPersister(this.ConnectionString);
    		parentAssetFund = assetFundPersister.Load(assetFundID, false);
    		return parentAssetFund;
    	}

    	/// <summary>
        /// Translates the database fund type code to a recognised enum.
        /// </summary>
        /// <param name="fundType">The fund type as returned from the database.</param>
        /// <returns>The FundType enum for the fund type.</returns>
        public  static FundFactory.FundType ResolveFundType(string fundType)
        {
            switch (fundType)
            {
                case "L":
                    return FundFactory.FundType.Linked;
                case "O":
                    return FundFactory.FundType.Oeic;
                case "C":
                    return FundFactory.FundType.Composite;
                default:
                    throw new ArgumentException ("Invalid fund type");
            }
        }

        /// <summary>
        /// Translates the enum to a database fund type code.
        /// </summary>
        /// <param name="fundType">The fund type enum.</param>
        /// <returns>The fund type as used in the database.</returns>
        protected  static string resolveFundType(FundFactory.FundType fundType)
        {
            switch (fundType)
            {
                case FundFactory.FundType.Linked:
                    return "L";
                case FundFactory.FundType.Oeic:
                    return "O";
                case FundFactory.FundType.Composite:
					return "C";
                default:
                    throw new ArgumentException ("Invalid fund type");
            }
        }

		/// <summary>
		/// resolves the object to a dbtype (as opposed to the type enum)
		/// </summary>
		/// <param name="fund"></param>
		/// <returns></returns>
		protected  static string resolveFundToDBType (Fund fund)
		{
			FundFactory.FundType fType = FundFactory.ResolveFundType (fund);

			switch (fType)
			{
				case (FundFactory.FundType.Composite):
					return "C";
				case (FundFactory.FundType.Linked):
					return "L";
				case (FundFactory.FundType.Oeic):
					return "O";
				default:
					throw new ArgumentException ("Fund not of a valid fund type");
			}
		}

        /// <summary>
        /// Translates the database fund status code to a recognised enum.
        /// </summary>
        /// <param name="status">The fund status as returned from the database.</param>
        /// <returns>The fund status enum.</returns>
        public  static Fund.FundStatusType ResolveFundStatus(int status)
        {
            switch (status)
            {
                case 0:
                    return Fund.FundStatusType.Missing;
                case 10:
                    return Fund.FundStatusType.Imported;
                case 20:
                    return Fund.FundStatusType.FirstLevelAuthorised;
                case 30:
                    return Fund.FundStatusType.SecondLevelAuthorised;
                case 40:
                    return Fund.FundStatusType.Released;
                case 50:
                    return Fund.FundStatusType.AwaitingDistribution;
                case 60:
                    return Fund.FundStatusType.BeingDistributed;
                case 70:
                    return Fund.FundStatusType.Distributed;
                default:
                    return Fund.FundStatusType.Unknown;
            }
        }

        /// <summary>
        /// Translates the enum to a database recognised status code.
        /// </summary>
        /// <param name="status">The fund status enum.</param>
        /// <returns>The corresponding database code.</returns>
        protected  static int resolveFundStatus(Fund.FundStatusType status)
        {
            switch (status)
            {
                case Fund.FundStatusType.Missing:
                    return 0;
                case Fund.FundStatusType.Imported:
                    return 10;
                case Fund.FundStatusType.FirstLevelAuthorised:
                    return 20;
                case Fund.FundStatusType.SecondLevelAuthorised:
                    return 30;
                case Fund.FundStatusType.Released:
                    return 40;
                case Fund.FundStatusType.AwaitingDistribution:
                    return 50;
                case Fund.FundStatusType.BeingDistributed:
                    return 60;
                case Fund.FundStatusType.Distributed:
                    return 70;
                default:
                    throw new ArgumentOutOfRangeException("status", "This is an unknown fund status.");
                    // TODO: Create an ApplicationException!!!
            }
        }

        /// <summary>
        /// Persist the associated fund groups ids for a given fund
        /// </summary>
        /// <param name="fundGroups">the fund group containing the associated with a Fund object</param>
        /// <param name="fundCode">fund code for the fund groups to be associated with</param>
        /// <param name="forUpdate">if this is set to false, then there is no need to delete</param>
        /// <param name="transaction">the transaction within which this association save is enlisted</param>
        /// <exception cref="ConcurrencyViolationException">Thrown if the record being saved was changed by someone else since being loaded</exception>
        /// <exception cref="ConstraintViolationException">Thrown if the constraint violation (primary key, foreign key or check) occurred during the save</exception>
        /// <exception cref="InvalidSqlParameterException">Thrown if one of the parameters was of the incorrect value, type or length; or was null</exception>
        /// <exception cref="DatabaseException">Thrown if other unclassified Sql error occurs</exception>
        /// <exception cref="ArgumentException">Invalid fund group id</exception>
        /// <exception cref="NullReferenceException">Fund group or fund group collection is null</exception>
        protected void SaveFundGroupAssociations(FundGroupCollection fundGroups, string fundCode, bool forUpdate, SqlTransaction transaction)
        {
            T.E();

            SqlParameter[] insertSPParams = new SqlParameter[2];
            string insertSPName = "usp_FundGroupAssociateFund";
            try
            {
                // Validate parameters...
                if (fundGroups == null)
                {
                    throw new NullReferenceException ("Fund group object is null");
                }
                if (fundCode.Length < 1)
                {
                    throw new ArgumentException ("Invalid fund code specified");
                }
                //
                // Simple formula - delete existing associations, then remake all.
                //
                // First delete existing associations
                if ( forUpdate )
                {
                    SqlParameter[] deleteSPParams = new SqlParameter[1];
                    deleteSPParams[0] = new SqlParameter ( "@cHiportFolioCode", SqlDbType.Char, 10 );
                    deleteSPParams[0].Value = fundCode;
                
                    string deleteSPName = "usp_FundGroupsDeleteForHiPortfolioCode";
                    try
                    {
                    	SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, deleteSPName, deleteSPParams);
                    }
                    catch (SqlException ex)
                    {
                        ThrowDBException (ex, deleteSPName, deleteSPParams);
                    }
                }

                // Now insert our fund group associations
                insertSPParams[0] = new SqlParameter ( "@cHiportFolioCode", SqlDbType.Char, 10 );
                insertSPParams[1] = new SqlParameter ( "@iFundGroupID", SqlDbType.Int );
                insertSPParams[0].Value = fundCode;

                // Step through the associated fund groups
                for (int i = 0; i < fundGroups.Count; i++)
                {
                    FundGroup fundGroup = fundGroups[i] as IndividualFundGroup;
                    if (fundGroup == null)
                    {
                      //  throw new NullReferenceException ("Fund Group is null");
                    }
                    else if (fundGroup.ID < 1)
                    {
                        throw new ArgumentException ("Invalid fund group ID");
                    }
					else
                    {
						// Assign associated fund group id
						insertSPParams[1].Value = fundGroup.ID;

						SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, insertSPName, insertSPParams);
                    }

                }
            }
            catch (SqlException ex)
            {
                ThrowDBException (ex, insertSPName, insertSPParams);
            }
            finally
            {
                T.X();
            }        
        }

		/// <summary>
		/// Save a funds factors.
		/// </summary>
		/// <param name="factors"></param>
		/// <param name="fundCode">HiportFolio code</param>
		/// <param name="transaction">the transaction within which this association save is enlisted</param>
		protected void SaveFundFactors(FactorCollection factors, string fundCode, SqlTransaction transaction)
		{
			T.E();
			InvalidFactorException factorEx = null;
			try
			{
				// Update factors
				FactorsPersister factorPersister = new FactorsPersister(this.ConnectionString);
				for (int j=0; j<factors.Count; j++)
				{
					Factor currentFactor = factors[j];	                        
					try
					{
						factorPersister.Save(currentFactor, fundCode, transaction);
					}
					catch (InvalidFactorException ex)
					{
						if (ex != null)
						{
							//right - what we need to do is attempt to save all factors,
							//whether somethings wrong with one or more of them or not.
							//But we need to tell the UI that we couldn't save all the fund factors,
							//so we'll throw an exception at the end (calling controller must catch and still commit transaction)
							//This only catches the factor type of the first exception thrown, so some 
							//factor type info may be lost but UI should be validating data first anyway!
							factorEx = ex;
						}
					}
				}
			}
			finally
			{
				T.X();
			}
			if (factorEx != null)
			{
				throw factorEx;
			}
		}

        /// <summary>
        /// Save a funds associated external system ids
        /// </summary>
        /// <param name="systemdIDs">associated system id collection</param>
        /// <param name="transaction">the transaction within which this association save is enlisted</param>
        protected void SaveAssociatedExternalSystemIDs(ExternalSystemIDCollection systemdIDs, SqlTransaction transaction)
        {
            T.E();
            try
            {
                // Update fund external systems
                ExternalSystemIDPersister systemIDPersister = new ExternalSystemIDPersister(this.ConnectionString);
                if ( systemIDPersister != null )
                {
                    systemIDPersister.Save(systemdIDs, transaction);
                }
            }
            finally
            {
                T.X();
            }
        }

        /// <summary>
        /// Update a funds tolerance values
        /// </summary>
        /// <remarks>The stored procedure will flag existing tolerances as not in use and then insert the current tolerances</remarks>
        /// <param name="fund">Object holdong the tolerances</param>
        /// <param name="transaction">the transaction within which this association save is enlisted</param>
        protected void SaveTolerances(Fund fund, SqlTransaction transaction)
        {
            T.E();

            SqlParameter[] insertSPParams = new SqlParameter[6];
            string insertSPName = "usp_FundToleranceCreate";
            try
            {
                // Validate parameters...
                if (fund == null)
                {
                    throw new NullReferenceException ("Fund object is null");
                }

                // Establish sp parameters
                insertSPParams[0] = new SqlParameter ( "@sHiPortfolioCode", SqlDbType.Char, 10 );
                insertSPParams[1] = new SqlParameter ( "@dAssetMovement", SqlDbType.Decimal );
                insertSPParams[2] = new SqlParameter ( "@dUpperTolerance", SqlDbType.Decimal );
                insertSPParams[3] = new SqlParameter ( "@dLowerTolerance", SqlDbType.Decimal );
                insertSPParams[4] = new SqlParameter ( "@bPriceIncreaseOnly", SqlDbType.Bit );
                insertSPParams[5] = new SqlParameter ( "@newToleranceID", SqlDbType.BigInt );

                // populate parameters
                insertSPParams[0].Value = fund.HiPortfolioCode;
                insertSPParams[1].Value = fund.AssetMovementTolerance;
                insertSPParams[2].Value = fund.UpperTolerance;
                insertSPParams[3].Value = fund.LowerTolerance;
                insertSPParams[4].Value = fund.PriceIncreaseOnly;
                insertSPParams[5].Direction = ParameterDirection.Output;

                // Now insert our fund tolerances, the sp will  flag existing to not in use
                SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, insertSPName, insertSPParams);

                // Extract the new tolerance id
                if (insertSPParams[5].Value != Convert.DBNull)
                {
                    fund.TolerancesID = (Int64)insertSPParams[5].Value;
                }
            }
            catch (SqlException ex)
            {
                ThrowDBException (ex, insertSPName, insertSPParams);
            }
            finally
            {
                T.X();
            }
        }

        /// <summary>
        /// Load the external system IDs for the current fund
        /// </summary>
        /// <returns>ExternalSystemIDCollection</returns>
        protected ExternalSystemIDCollection LoadExternalSystemIDs(string fundCode)
        {
            T.E();
            ExternalSystemIDPersister persister = new ExternalSystemIDPersister(this.ConnectionString);

            ExternalSystemIDCollection systemIDs = null;
            try
            {
                systemIDs = persister.LoadSystemIDsForHiPortfolioCode(fundCode);
            }
            finally
            {
                T.X();
            }

            return systemIDs;            
        }

        /// <summary>
        /// Load the fund groups associated with the sepcified fund code
        /// </summary>
        /// <returns>FundGroupCollection</returns>
        protected FundGroupCollection LoadFundGroupsForHiportFolioCode(string fundCode)
        {
            T.E();
            FundGroupPersister persister = new FundGroupPersister(this.ConnectionString);
            FundGroupCollection fundGroups = null;
            try
            {
                fundGroups = persister.LoadDirectFundGroupsByFundCode(fundCode);
            }
            finally
            {
                T.X();
            }
            return fundGroups;
        }

		/// <summary>
		/// Common method to populate a fund object.
		/// </summary>
		/// <remarks>
		/// This will load the correct level of data fo rthe required fund imformation.
		/// </remarks>
		/// <param name="safeReader"></param>
		/// <param name="fundGroups">The funds associated fund group collection</param>
		/// <param name="assetFund">The funds parent asset fund object</param>
		/// <param name="systemIDs">The funds associated external system id collection</param>
		/// <returns>A populated fund object</returns>
		protected Fund LoadNewFund(SafeDataReader safeReader, 
			FundGroupCollection fundGroups, AssetFund assetFund, ExternalSystemIDCollection systemIDs)
		{

			FundFactory.FundParameters fundParameters=new FundFactory.FundParameters();
			
			fundParameters.ParentAssetFund=assetFund;
			// Populate vars from DB and pass to factory to aid in reading and following what is being parsed
			fundParameters.RevaluationEffectiveDate = safeReader.IsNull("revaluationEffectiveDate") ? DateTime.MinValue : safeReader.GetDateTime("revaluationEffectiveDate");
			fundParameters.RevaluationEndDate = safeReader.GetDateTime("revaluationEndDate");
			fundParameters.TaxProvisionEffectiveDate = DateTime.Now.Date;
			fundParameters.ScalingFactorEffectiveDate = DateTime.Now.Date;
			fundParameters.XFactorEffectiveDate = DateTime.Now.Date;

			// Populate locals with data from the reader
			fundParameters.HiPortfolioCode = safeReader.GetString("HiPortFolioCode");
			fundParameters.ParentAssetFundID = safeReader.GetString("AssetFundID");
			fundParameters.FullName = safeReader.GetString("FullName");
			fundParameters.ShortName = safeReader.GetString("ShortName");
			fundParameters.ClassOrSeriesCode = safeReader.GetString("FundClassOrSeries").Trim();
			fundParameters.XFactorNarrative = safeReader.GetString("xFactorNarrative");

			fundParameters.XFactor = safeReader.GetDecimal("xFactor");
			fundParameters.ScalingFactor = safeReader.GetDecimal("scalingFactor");
			fundParameters.TaxProvisionEstimate = safeReader.GetDecimal("taxProvisionFactor");
			fundParameters.RevaluationEffect = safeReader.GetDecimal("revaluationEffect");
			fundParameters.AssetMovementTolerance = safeReader.GetDecimal("assetMovementTolerance");
			fundParameters.LowerTolerance = safeReader.GetDecimal("lowerTolerance");
			fundParameters.UpperTolerance = safeReader.GetDecimal("upperTolerance");

			fundParameters.TolerancesID = safeReader.GetInt64("tolerancesID");
			fundParameters.TaxProvisionFactorID = safeReader.GetInt32("taxProvisionFactorID");
			fundParameters.ScalingFactorID = safeReader.GetInt32("scalingFactorID");
			fundParameters.XFactorID = safeReader.GetInt32("xFactorID");
			fundParameters.RevaluationFactorID = safeReader.GetInt32("revaluationFactorID");

			fundParameters.OnHiPortfolio3 = safeReader.GetBoolean("FundOnHi3");
			fundParameters.PriceIncreaseOnly = safeReader.GetBoolean("priceIncreaseOnly");
            
			fundParameters.IsBenchmarkable =false;
			if (safeReader.ColumnExists("isBenchmarkable"))
			{
				fundParameters.IsBenchmarkable= safeReader.GetBoolean("isBenchmarkable");
			}

			fundParameters.Timestamp= safeReader.GetTimestamp("ts");

			// Resolve the database type field.
			fundParameters.FundType = ResolveFundType(safeReader.GetString("FundType"));

			// Resolve the database status field.
			fundParameters.FundStatus = ResolveFundStatus(safeReader.GetInt32("statusID"));

			// Timestamps...
			fundParameters.AuthorisedPriceTimestamp = safeReader.ColumnExists("authorisedPriceTS") ? safeReader.GetTimestamp("authorisedPriceTS") : new byte[1];
			fundParameters.RevaluationTimestamp = safeReader.ColumnExists("revaluationTimestamp") ? safeReader.GetTimestamp("revaluationTimestamp") : new byte[1];
			fundParameters.ScalingFactorTimestamp = safeReader.ColumnExists("scalingFactorTimestamp") ? safeReader.GetTimestamp("scalingFactorTimestamp") : new byte[1];
			fundParameters.TaxProvisionTimestamp = safeReader.ColumnExists("taxProvisionTimestamp") ? safeReader.GetTimestamp("taxProvisionTimestamp") : new byte[1];
			fundParameters.XFactorTimestamp = safeReader.ColumnExists("xFactorTimestamp") ? safeReader.GetTimestamp("xFactorTimestamp") : new byte[1];

            
			fundParameters.UsePredictedPrice = safeReader.ColumnExists("authorisedPriceFromPrediction") ? safeReader.GetBoolean("authorisedPriceFromPrediction") : false;

			fundParameters.ValuationBasisEffect = 0m;
			if ( safeReader.ColumnExists("valuationBasisEffect") )
			{
				fundParameters.ValuationBasisEffect = safeReader.GetDecimal("valuationBasisEffect");
			}

			fundParameters.Price= 0m;
			fundParameters.PriceSet = false;
			if ( safeReader.ColumnExists("currentPrice") )
			{
				fundParameters.Price = safeReader.GetDecimal("currentPrice");
				fundParameters.PriceSet = !safeReader.IsNull("currentPrice");
			}

			fundParameters.ImportedFundPriceID = 0;
			if ( safeReader.ColumnExists("FundPriceSnapshotID") )
			{
				fundParameters.ImportedFundPriceID = safeReader.GetInt64("FundPriceSnapshotID");
			}

			fundParameters.PreviousPriceSet = false;
			fundParameters.PreviousPrice = 0m;
			if ( safeReader.ColumnExists("previousPrice") )
			{
				fundParameters.PreviousPrice = safeReader.GetDecimal("previousPrice");
				fundParameters.PreviousPriceSet = !safeReader.IsNull("previousPrice");
			}

			fundParameters.StatusChangedTimeSet = false;
			fundParameters.StatusChangedTime = DateTime.MinValue;
			if ( safeReader.ColumnExists("statusChangedOn") )
			{
				fundParameters.StatusChangedTime = safeReader.GetDateTime("statusChangedOn");
				fundParameters.StatusChangedTimeSet = !safeReader.IsNull("statusChangedOn");
			}

			fundParameters.CompanyCode = null;
			if (safeReader.ColumnExists ("companyCode")) //is for static data export only
			{
				fundParameters.CompanyCode = safeReader.GetString("companyCode");
			}

			fundParameters.SecurityCode = null;
			if ( safeReader.ColumnExists("securityCode") )
			{
				fundParameters.SecurityCode=safeReader.GetString ("securityCode");
			}			

			//
		
			
			fundParameters.IsDualPrice = false;
			fundParameters.IsLife = false;
			fundParameters.UseMidPriceAsBidPrice = false;
			fundParameters.IsExDividend = false;;

			if (safeReader.ColumnExists ("IsDual")) 
			{
				fundParameters.IsDualPrice = safeReader.GetBoolean("IsDual");
			}

			if (safeReader.ColumnExists ("IsLife")) 
			{
				fundParameters.IsLife = safeReader.GetBoolean("IsLife");
			}

			if (safeReader.ColumnExists ("IsMidPriceBidPrice")) 
			{
				fundParameters.UseMidPriceAsBidPrice = safeReader.GetBoolean("IsMidPriceBidPrice");
			}

			if (safeReader.ColumnExists ("IsExDividend")) 
			{
				fundParameters.IsExDividend = safeReader.GetBoolean("IsExDividend");
			}
			
			fundParameters.CurrentValuationDate=DateTime.Now;
			if (safeReader.ColumnExists ("currentValuationDate")) 
			{
				fundParameters.CurrentValuationDate = safeReader.GetDateTime("currentValuationDate");
			}
			//
			// Now Create a new Fund object.
			//
			Fund newFund = null;

			try
			{
				fundParameters.Factors = FundFactory.CreateFundFactors (fundParameters, LoadHolidays());

				newFund = FundFactory.CreateFund (fundParameters);


				// Now add any external system ids
				T.Log("Add associated external system ids");
				newFund.SystemIDs = systemIDs;

				// Now add the fund groups this fund is directly associated too
				T.Log("Add fund group associations");
				newFund.FundGroups = fundGroups;

#if (DEBUG)

				// Complete diagnostics
				// Diagnostics 
				T.Log(" *********** FUND : " +  newFund.FullName + " **************");
				T.Log("Fund Type = " + newFund.GetType());
				if (newFund.ParentAssetFund != null)
				{
					T.Log("Asset Fund = " + newFund.ParentAssetFund.FullName);
					T.Log("Asset Fund Type = " + newFund.ParentAssetFund.GetType());
					T.Log("Predicted Asset Movement = " + newFund.ParentAssetFund.PredictedAssetMovement);
				}
				else
				{
					T.Log("Asset Fund not loaded");
				}

				if (newFund.Factors != null)
				{
					T.Log("Num Factors = " + newFund.Factors.Count);
					for (int j=0; j<newFund.Factors.Count; j++)
					{
						Factor currentFactor = newFund.Factors[j];
						T.Log("Factor [" + j + "] : Contribution = " + currentFactor.CalculateEffect() + ", type = " + currentFactor.GetType());
						if (currentFactor is RevaluationFactor)
						{
							RevaluationFactor revalFactor = (RevaluationFactor)currentFactor;
							T.Log("Revaluation Factor Effective Date = " + revalFactor.EffectiveDate.ToString());
							T.Log("Revaluation Factor End Date = " + revalFactor.EndDate);
							T.Log("Revaluation Percentage Value = " + revalFactor.RatioValue);
							T.Log("Therefore....");
							T.Log("Revaluation Factor Effect = " + revalFactor.CalculateEffect().ToString());
						}
					}
				}
				else
				{
					T.Log("Factors not loaded");
				}

#endif

				// Clear dirty flag which is being set when assign values through 
				// the properties rather than the FundFactory
				newFund.IsDirty = false;

				T.Log(Environment.NewLine);
			}
			finally
			{
				T.X();
			}
			return newFund;
		}

		/// <summary>
		/// Clears the cache of holidays
		/// </summary>
		public static void ClearHolidays()
		{
			m_Holidays=null;
		}

		/// <summary>
		/// Loads a list of dates that are UK bank holidays
		/// </summary>
		/// <returns></returns>
		public  Hashtable LoadHolidays ()
		{
			if (m_Holidays==null)
			{
				m_Holidays=new Hashtable();
				SqlDataReader hols = SqlHelper.ExecuteReader (ConnectionString, CommandType.StoredProcedure, "usp_HolidaysGet");

				while (hols.Read())				
				{
					DateTime holDate = (DateTime) hols.GetValue(hols.GetOrdinal("HolidayDate"));
					m_Holidays.Add(holDate, holDate);
				}
			}
			return m_Holidays;
			//couldn't get to work in conjunction with country and currency cache as well
//			CacheHelper cacheHelper =new CacheHelper();
//			return cacheHelper.GetHolidays(ConnectionString);
		}

        #endregion

		#region generic fund save methods

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
			const string deleteSp = "usp_FundDelete";
			T.E();
			SqlParameter[] spParams = null;
			try
			{
				//arguments asserted in SaveEntity - the only thing that calls this
				Fund fund = (Fund)entity;

				//mark the asset fund as deleted and remove associations to fund groups

				// Create paremeter object
				spParams = new SqlParameter[2];

				// This will make adding/moving/copying rows easier!!
				int paramCounter = 0;

				// Build parameters
				spParams[paramCounter++] = new SqlParameter( "@cHiPortfolioCode", SqlDbType.Char, 10);
				spParams[paramCounter++] = new SqlParameter( "@ts", SqlDbType.Timestamp, 50 );

				paramCounter = 0;

				// Assign values to parameters
				spParams[paramCounter++].Value = fund.HiPortfolioCode;
				spParams[paramCounter++].Value = fund.TimeStamp;
                    
				// Call delete stored procedure
				SqlHelper.ExecuteNonQuery(transaction , CommandType.StoredProcedure, deleteSp, spParams);

			}
			catch (SqlException ex)
			{
				ThrowDBException (ex, deleteSp, spParams);
			}
			finally
			{
				T.X();
			}
		}


		#endregion

        #endregion Drop II

    }
}