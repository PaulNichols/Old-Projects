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
	/// Persister specifically for the authorising and unauthorising of fund prices.  A separate persister
	/// has been created so that the generic Save method can be used rather than an application-specific
	/// method on the FundPersister object.
	/// </summary>
	public class AuthorisedFundPricePersister : EntityPersister
	{
		#region Constructor

		/// <summary>
		/// Constructor initialising the connection string.
		/// </summary>
		/// <param name="connectionString">The application connection string.</param>
		public AuthorisedFundPricePersister(string connectionString) : base(connectionString)
		{
		}

		#endregion

		#region Methods

		/// <summary>
		/// Update the price authorisation for each fund in the collection.
		/// </summary>
		/// <param name="funds">The collection of funds being authorised or unauthorised.</param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public void Save(FundCollection funds)
		{
			T.E();

			try
			{
				this.SaveEntityCollection(funds);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Update a fund in the database to be authorised or unauthorised
		/// </summary>
		/// <param name="transaction">The transaction used for the persistence, allowing transactional rollback.</param>
		/// <param name="entity">Persitee user object</param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		/// <exception cref="ArgumentNullException">Entity was passed in as a null</exception>
		/// <exception cref="ArgumentException">Invalid entity type returned</exception>
		protected override void UpdateEntity(IEntityBase entity, SqlTransaction transaction)
		{
			T.E();

			try
			{
				Fund fund = (Fund) entity;

				if (fund.ProgressStatus || fund.UsePredictedPrice)
				{
					this.authoriseFund(fund, transaction);
				}
				else
				{
					this.unauthoriseFund(fund, transaction);
				}
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Update the fund to show that it has been second level authorised.
		/// </summary>
		/// <param name="fund">The fund being authorised.</param>
		/// <param name="transaction">The currently active transaction.</param>
		/// <returns>True if the operation is successful.</returns>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		private void authoriseFund(Fund fund, SqlTransaction transaction)
		{
			T.E();

			// Set up the procedure.
			const string procedure = "kaj_AuthorisedFundPriceCreate";
			SqlParameter[] parameters = new SqlParameter[22];

			try
			{

				createAuthoriseFundParameters(parameters);

				assignValuesToAuthorisedParameters(fund, parameters);

				// Call the update stored procedure.
				SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, procedure,
				                          parameters);

				updateFundWithReturnedValues(parameters, fund);
			}
			catch (SqlException ex)
			{
				if (ex.Number==(int) DatabaseError.CustomError)
				{
					throw new SecondLevelAuthorisationException(fund,ex);
				}
				else
				{
					ThrowDBException(ex, procedure, parameters);
				}
			}
			finally
			{
				T.X();
			}
		}

		private static void updateFundWithReturnedValues(SqlParameter[] parameters, Fund fund)
		{
			if (parameters[19] != null)
			{
				fund.AuthorisedPriceTimestamp = (byte[]) parameters[19].Value;
			}
	
			if (parameters[20] != null)
			{
				fund.StatusChangedTime= (DateTime)parameters[20].Value;
			}
			fund.FundStatus = Fund.FundStatusType.SecondLevelAuthorised;

		}

//		/// <summary>
//		/// Determines the type of fund object.
//		/// </summary>
//		/// <param name="nonOEICFund">NonOEIC.</param>
//		/// <param name="fund">Fund.</param>
//		private static void determineTypeOfFundObject(NonOEIC nonOEICFund, Fund fund)
//		{
//				nonOEICFund = fund as NonOEIC;			
//		}

		/// <summary>
		/// Assigns the values to authorised parameters.
		/// </summary>
		/// <param name="fund">Fund.</param>
		/// <param name="parameters">Parameters.</param>
		private static void assignValuesToAuthorisedParameters(Fund fund, SqlParameter[] parameters)
		{
		
			Fund.FundAuthoriseInfo authoriseInfo=new Fund.FundAuthoriseInfo(fund);
			
			parameters[0].Value =authoriseInfo.HiPortfolioCode;
			parameters[1].Value =authoriseInfo.FundCurrencyCode;
			if (authoriseInfo.Price!=decimal.MinValue ) parameters[2].Value =authoriseInfo.Price;
			parameters[3].Value =authoriseInfo.IsBidPriceMidPrice;
			parameters[4].Value =authoriseInfo.WasFromPrediction;
			parameters[5].Value =authoriseInfo.ValuationBasisEffect;
			parameters[6].Value =authoriseInfo.ImportedFundPrice;
			parameters[7].Value =authoriseInfo.PredictedFundPrice;
			parameters[8].Value =authoriseInfo.AssetFundID;
			parameters[9].Value =authoriseInfo.AssetUnitPrice;
			if (authoriseInfo.ImportedFundPriceID>0) parameters[10].Value =authoriseInfo.ImportedFundPriceID;
			if (authoriseInfo.TolerancesSnapshotID>0) parameters[11].Value =authoriseInfo.TolerancesSnapshotID;
			if (authoriseInfo.TaxProvisionFactorID>0) parameters[12].Value =authoriseInfo.TaxProvisionFactorID;
			if (authoriseInfo.RevaluationFactorId>0) parameters[13].Value =authoriseInfo.RevaluationFactorId;
			if (authoriseInfo.ScalingFactorID>0) parameters[14].Value =authoriseInfo.ScalingFactorID;
			if (authoriseInfo.XFactorId>0) parameters[15].Value =authoriseInfo.XFactorId;
			if (authoriseInfo.CurrencyRateSnapshotID>0) parameters[16].Value =authoriseInfo.CurrencyRateSnapshotID;
			if (authoriseInfo.MarketIndiciesSnapShotId>0) parameters[17].Value =authoriseInfo.MarketIndiciesSnapShotId;
			if (authoriseInfo.BenchmarkSplitSnapShotId>0) parameters[18].Value =authoriseInfo.BenchmarkSplitSnapShotId;
            parameters[21].Value = authoriseInfo.PredictedAssetMovement;            

//			parameters[0].Value = fund.HiPortfolioCode;
//			parameters[1].Value = fund.AssetFundID;
//			parameters[4].Value = fund.UsePredictedPrice;
	
		
//			// Predicted or imported price value
//			if (fund.UsePredictedPrice)
//			{
//				parameters[2].Value = fund.PredictedPrice;
//			}
//			else
//			{
//				if (fund.PriceSet)
//				{
//					parameters[2].Value = fund.Price;
//				}
//			}
//	
//			// Get the correct fund type UseMidPriceAsBidPrice value
//			if (nonOEICFund != null && nonOEICFund.TaxProvisionFactorIDSet)
//			{
//				parameters[3].Value = nonOEICFund.UseMidPriceAsBidPrice;
//			}
//			else
//			{
//				parameters[3].Value = false;
//			}
	
//			// Assign the imported fund price id
//			if (fund.ImportedFundPriceIDSet)
//			{
//				parameters[5].Value = fund.ImportedFundPriceID;
//			}
	
			// Assign the tolerances id
//			if (fund.TolerancesIDSet)
//			{
//				parameters[6].Value = fund.TolerancesID;
//			}
	
			// Extract the correct fund type TPE id
//			if (nonOEICFund != null && nonOEICFund.TaxProvisionFactorIDSet)
//			{
//				parameters[7].Value = nonOEICFund.TaxProvisionFactorID;
//			}
		
	
			// Extract and assign the correct fund type revaluation factor id
//			if (nonOEICFund != null)
//			{
//				if (nonOEICFund.RevaluationFactorIDSet)
//				{
//					parameters[8].Value = nonOEICFund.RevaluationFactorID;
//				}
//			}
		
	
//			// Assign the correct fund type scaling factor id
//			parameters[9].Value = DBNull.Value; //default value
//			if (nonOEICFund != null)
//			{
//				if (nonOEICFund.ScalingFactorIDSet)
//				{
//					parameters[9].Value = nonOEICFund.ScalingFactorID;
//				}
//			}
			
//	
//			// Assign the x factor id
//			if (fund.XFactorIDSet)
//			{
//				parameters[10].Value = fund.XFactorID;
//			}
	
			// Assign the valuation basis effect
			//if (fund.ValuationBasisEffectSet)
			//{
			//parameters[11].Value = fund.ValuationBasisEffect;
			//}
	
			// Assign the imported or predicted asset unit price


//			if (fund.AssetUnitPriceSet)
//			{
//				parameters[12].Value = fund.AssetUnitPrice;
//			}
//			else
//			{
//				if (fund.PredictedAssetUnitPriceSet && fund.UsePredictedPrice)
//				{
//					parameters[12].Value = fund.PredictedAssetUnitPrice;
//				}
//				else
//				{
//					// Validation prior to this should have prevented ever getting here.
//				}
//			}
	
//			// Assign currency rate import id
//			if (fund.CurrencyRateSnapshotIDSet)
//			{
//				parameters[13].Value = fund.CurrencyRateSnapshotID;
//			}
//	
//			
//			// Assign the index weighting (market value split) import id
//			if (fund.IndexWeightingSnapshotIDSet)
//			{
//				parameters[15].Value = fund.IndexWeightingSnapshotID;
//			}
////	
////			// Assign the composite asset fund split id
////			if (nonOEICFund.BenchmarkSpiltSnapshotIDSet)
////				{
////					parameters[16].Value = nonOEICFund.BenchmarkSpiltSnapshotID;
////				}
////			
//	
//			// Assign the imported fund price
//			if (fund.PriceSet)
//			{
//				parameters[17].Value = fund.Price;
//			}
//	
//			// Assign the predicted price
//			parameters[18].Value = fund.PredictedPrice;
	
//			// Predicted asset unit price
//			if (fund.PredictedAssetUnitPriceSet)
//			{
//				parameters[19].Value = fund.PredictedAssetUnitPrice;
//			}
	
			//
			// TODO: When refactoring, remove this TS as it is not used in the DB nor the App
			//
			// Mark the timstamp parameter as an output
			parameters[19].Direction = ParameterDirection.Output;
			parameters[20].Direction = ParameterDirection.Output;
		}

		/// <summary>
		/// Creates the authorise fund parameters.
		/// </summary>
		/// <param name="parameters">Parameters.</param>
		private static void createAuthoriseFundParameters(SqlParameter[] parameters)
		{
			parameters[0] = new SqlParameter("@cHiPortfolioCode", SqlDbType.Char, 10);
			parameters[1] = new SqlParameter("@cCurrencyCode", SqlDbType.VarChar, 10);
			parameters[2] = new SqlParameter("@mPrice", SqlDbType.Money);
			parameters[3] = new SqlParameter("@bIsBidPriceMidPrice", SqlDbType.Bit);
			parameters[4] = new SqlParameter("@bWasFromPrediction", SqlDbType.Bit);
			parameters[5] = new SqlParameter("@dValuationBasisEffect", SqlDbType.Decimal);
			parameters[6] = new SqlParameter("@mImportedPrice", SqlDbType.Money);
			parameters[7] = new SqlParameter("@mPredictedFundPrice", SqlDbType.Money);
			parameters[8] = new SqlParameter("@cAssetFundId", SqlDbType.Char, 8);
			parameters[9] = new SqlParameter("@mAssetUnitPrice", SqlDbType.Money);
			parameters[10] = new SqlParameter("@iFundPriceSnapshotID", SqlDbType.BigInt);
			parameters[11] = new SqlParameter("@iTolerancesID", SqlDbType.Int);
			parameters[12] = new SqlParameter("@iTaxProvisionFactorID", SqlDbType.Int);
			parameters[13] = new SqlParameter("@iRevaluationFactorID", SqlDbType.Int);
			parameters[14] = new SqlParameter("@iScalingFactorID", SqlDbType.Int);
			parameters[15] = new SqlParameter("@iXFactorID", SqlDbType.Int);
			parameters[16] = new SqlParameter("@iCurrencyRateSnapshotID", SqlDbType.BigInt);
			parameters[17] = new SqlParameter("@iMarketIndiciesSnapshotID", SqlDbType.BigInt);
			parameters[18] = new SqlParameter("@iBenchmarkSplitSnapshotID", SqlDbType.BigInt);
			parameters[19] = new SqlParameter("@ts", SqlDbType.Timestamp);
			parameters[20] = new SqlParameter("@authorisationDate", SqlDbType.DateTime);
            parameters[21] = new SqlParameter("@dAssetMovement", SqlDbType.Decimal);
		}

		/// <summary>
		/// Update the fund to show that the second level authorisation has been reversed.
		/// </summary>
		/// <param name="fund">The fund having its second level authorisation reset.</param>
		/// <param name="transaction">The currently active transaction.</param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		private void unauthoriseFund(Fund fund, SqlTransaction transaction)
		{
			T.E();
			const string spUpdate = "usp_AuthorisedFundPriceUnauthorise";

			// Set up the parameters.
			SqlParameter[] parameters = new SqlParameter[1];

			try
			{
				createUnAuthoriseParameters(parameters, fund);

				// Call the update stored procedure.
				SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure,
				                          spUpdate, parameters);

				fund.FundStatus = Fund.FundStatusType.Imported;
			}
			catch (SqlException ex)
			{
				ThrowDBException(ex, spUpdate, parameters);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Creates the un authorise parameters.
		/// </summary>
		/// <param name="parameters">Parameters.</param>
		/// <param name="fund">Fund.</param>
		private static void createUnAuthoriseParameters(SqlParameter[] parameters, Fund fund)
		{
			parameters[0] = new SqlParameter("@cHiPortfolioCode", SqlDbType.Char, 10);
			parameters[0].Value = fund.HiPortfolioCode;
		}

		#endregion
	}
}