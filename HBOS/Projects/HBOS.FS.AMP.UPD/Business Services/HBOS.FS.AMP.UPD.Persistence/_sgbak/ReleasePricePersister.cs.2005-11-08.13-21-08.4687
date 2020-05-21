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
	/// Persister specifically for the releasing and unreleasing of fund prices.  A separate persister
	/// has been created so that the generic Save method can be used rather than an application-specific
	/// method on the FundPersister object.
	/// </summary>
	public class ReleasePricePersister : EntityPersister
	{
		#region Constructor

		/// <summary>
		/// Constructor initialising the connection string.
		/// </summary>
		/// <param name="connectionString">The application connection string.</param>
		public ReleasePricePersister(string connectionString) : base(connectionString)
		{
		}

		#endregion

		#region Methods

//
		/// <summary>
		/// Update the fund status for each updated fund in the collection.
		/// </summary>
		/// <param name="funds">The collection of funds being released or unreleased.</param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public void Save(FundCollection funds)
		{
			T.E();
			this.SaveEntityCollection(funds);
			T.X();
		}

		/// <summary>
		/// Update the price authorisation for the given fund object.
		/// </summary>
		/// <param name="entity">
		/// The fund for which the price authorisation information is being persisted.
		/// </param>
		/// <param name="transaction">The current transaction.</param>
		/// <returns>True if the operation is successful.</returns>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		/// <exception cref="NullReferenceException">Unable to save as persistee was null</exception>
		/// <exception cref="ArgumentException">Unable to save as persistee was not of type 'Fund'</exception>
		protected override void UpdateEntity(IEntityBase entity, SqlTransaction transaction)
		{
			T.E();
			try
			{
				// Check that the persisted object is a Fund.
				if (entity == null) throw new ArgumentNullException("entity", "The fund could not be saved as it was null.");

				Fund fund = entity as Fund;

				if (fund == null) throw new ArgumentException("The fund could not be saved as it was of an invalid type.");

				// Either release or unrelease the prices depending on the FundStatus
				if (fund.FundStatus == Fund.FundStatusType.SecondLevelAuthorised)
				{
					this.releaseFund(fund, transaction);
				}
				else if (fund.FundStatus == Fund.FundStatusType.Released)
				{
					this.unreleaseFund(fund, transaction);
				}
				else
				{
					throw new FundSaveException(string.Format("Cannot release/unrelease fund with status {0}", fund.FundStatus.ToString()), fund);
				}
			}
			catch (DatabaseException ex)
			{
				if (((SqlException)ex.InnerException).Number==(int) DatabaseError.CustomError)
				{
					throw new ReleaseException(entity,(SqlException) ex.InnerException);
				}
				else
				{
					throw;
				}
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Update the fund status to released.
		/// </summary>
		/// <param name="fund">The fund being released.</param>
		/// <param name="transaction">The current transaction.</param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		private void releaseFund(Fund fund, SqlTransaction transaction)
		{
			T.E();
			const string sp = "usp_AuthorisedFundPriceRelease";

			// Set up the parameters.
			SqlParameter[] parameters = new SqlParameter[2];

			try
			{
				parameters[0] = new SqlParameter("@cHiPortfolioCode", SqlDbType.Char, 10);
				parameters[0].Value = fund.HiPortfolioCode;

				parameters[1] = new SqlParameter("@ts", SqlDbType.Timestamp);
				parameters[1].Value = fund.AuthorisedPriceTimestamp;

				// Call the update stored procedure.
				SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, sp, parameters);

			}
			catch (SqlException ex)
			{
				ThrowDBException(ex, sp, parameters);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Creates the lookup item from the data
		/// </summary>
		/// <param name="safeReader">Safe reader.</param>
		/// <returns></returns>
		protected override object CreateEntity(SafeDataReader safeReader)
		{
			FundFactory.FundParameters fundParameters = new FundFactory.FundParameters();
			fundParameters.FundType = FundPersister.ResolveFundType(safeReader.GetString("fundType"));
			fundParameters.ShortName = safeReader.GetString("shortName");
			fundParameters.HiPortfolioCode=safeReader.GetString("HiPortFolioCode");
			fundParameters.FundStatus = FundPersister.ResolveFundStatus(safeReader.GetInt32("statusID"));
			fundParameters.AuthorisedPriceTimestamp=safeReader.GetTimestamp("AuthorisedPriceTimestamp");
			return FundFactory.CreateFund(fundParameters);
		}

		/// <summary>
		/// Loads a lookup collection for specified group.
		/// </summary>
		/// <param name="fundGroupId">Fund group id.</param>
		/// <returns></returns>
		public FundCollection LoadForGroup(int fundGroupId)
		{
			T.E();
			FundCollection lookupList = new FundCollection();

			SqlParameter[] parms = new SqlParameter[1];
			parms[0] = new SqlParameter("@FundGroupId", fundGroupId);

			this.LoadEntityCollection("usp_FundGetLookupsForFundGroupId", parms, lookupList);

			T.X();
			return lookupList;
		}

		/// <summary>
		/// Reverse the released fund status.
		/// </summary>
		/// <param name="fund">The fund being unreleased.</param>
		/// <param name="transaction">The current transaction.</param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		private void unreleaseFund(Fund fund, SqlTransaction transaction)
		{
			T.E();
			const string sp = "usp_AuthorisedFundPriceUnRelease";

			// Set up the parameters.
			SqlParameter[] parameters = new SqlParameter[2];

			try
			{
				parameters[0] = new SqlParameter("@cHiPortfolioCode", SqlDbType.Char, 10);
				parameters[0].Value = fund.HiPortfolioCode;

				parameters[1] = new SqlParameter("@ts", SqlDbType.Timestamp);
				parameters[1].Value = fund.AuthorisedPriceTimestamp;

				// Call the update stored procedure.
				SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, sp, parameters);
			}
			catch (SqlException ex)
			{
				ThrowDBException(ex, sp, parameters);
			}
			finally
			{
				T.X();
			}
		}

		#endregion
	}
}