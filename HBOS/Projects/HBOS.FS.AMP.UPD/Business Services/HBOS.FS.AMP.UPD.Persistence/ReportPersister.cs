using System;
using System.Data;
using System.Data.SqlClient;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.Support.Tex;
using Microsoft.ApplicationBlocks.Data;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// Summary description for ReportPersister.
	/// </summary>
	public class ReportPersister : PersisterBase
	{
		#region Constructors

		/// <summary>
		/// Constructor used to initialise the ConnectionString property
		/// </summary>
		/// <param name="connectionString"></param>
		public ReportPersister(string connectionString) : base(connectionString)
		{
		}

		#endregion

		#region Report Methods


		/// <summary>
		/// Loads the price comparision data for the report.
		/// </summary>
		/// <param name="valuationDate">Valuation date to retrieve UPD prices for.</param>
		/// <param name="snapShotId">The Id of the import to be used in the comparision.</param>
		/// <param name="datasetToFill">Typed Dataset to populate and return</param>
		/// <returns></returns>
		public DataSet LoadPriceComparisionData(DateTime valuationDate, long snapShotId,DataSet datasetToFill)
		{
			T.E();
			const string reportSp = "usp_ReportLoadPriceComparisionData";
			// Build the parameters collection
			SqlParameter[] parameters = new SqlParameter[2];

			try
			{
				// Set up the parameters.
				parameters[0] = new SqlParameter("@snapShotId", SqlDbType.BigInt);
				parameters[0].Value = snapShotId;

				parameters[1] = new SqlParameter("@valuationDate", SqlDbType.DateTime);
				parameters[1].Value = valuationDate;

				// Start a new SQL connection
				SqlConnection connection = new SqlConnection(ConnectionString);
				connection.Open();

				// Execute the fill method
				SqlHelper.FillDataset(connection, reportSp, datasetToFill, new string[] {"ComparisionDetails"}, parameters);

				// We close the connection
				connection.Close();
			}
			catch (SqlException ex)
			{
				ThrowDBException(ex, reportSp, parameters);
			}
			finally
			{
				T.X();
			}
			return datasetToFill;
		}

		/// <summary>
		/// This method retrieves the predicted price data for a fund group and date range
		/// </summary>
		/// <param name="fundGroupID"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <returns></returns>
		/// <exception cref="DatabaseException">Unable to load company</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		public DataSet PredictedPricesReportByFundGroup(int fundGroupID, DateTime startDate, DateTime endDate)
		{
			T.E();
			const string reportSp = "usp_PredictedPriceHistoricalReportByFundGroup";
			// Build the parameters collection
			SqlParameter[] parameters = new SqlParameter[3];
			DataSet reportResults = null;

			try
			{
				// Set up the parameters.
				parameters[0] = new SqlParameter("@FundGroupID", SqlDbType.Int);
				parameters[0].Value = fundGroupID;

				parameters[1] = new SqlParameter("@startDate", SqlDbType.DateTime);
				parameters[1].Value = startDate;

				parameters[2] = new SqlParameter("@endDate", SqlDbType.DateTime);
				parameters[2].Value = endDate;

				// Start a new SQL connection
				SqlConnection connection = new SqlConnection(ConnectionString);
				connection.Open();

				reportResults = new DataSet();

				// Execute the fill method
				SqlHelper.FillDataset(connection, reportSp, reportResults, new string[] {"PredictedPrices"}, parameters);

				// We close the connection
				connection.Close();
			}
			catch (SqlException ex)
			{
				ThrowDBException(ex, reportSp, parameters);
			}
			finally
			{
				T.X();
			}
			return reportResults;
		}

		/// <summary>
		/// This method retrieves the predicted price data for a fund and date range
		/// </summary>
		/// <param name="fundID"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <returns></returns>
		/// <exception cref="DatabaseException">Unable to load company</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		public DataSet PredictedPricesReportByFund(string fundID, DateTime startDate, DateTime endDate)
		{
			T.E();
			const string reportSp = "usp_PredictedPriceHistoricalReportByFund";
			SqlParameter[] parameters = new SqlParameter[3];
			DataSet reportResults = null;

			try
			{
				// Set up the parameters.
				parameters[0] = new SqlParameter("@hiPortfolioCode", SqlDbType.Char, 10);
				parameters[0].Value = fundID;

				parameters[1] = new SqlParameter("@startDate", SqlDbType.DateTime);
				parameters[1].Value = startDate;

				parameters[2] = new SqlParameter("@endDate", SqlDbType.DateTime);
				parameters[2].Value = endDate;

				// Start a new SQL connection
				SqlConnection connection = new SqlConnection(ConnectionString);
				connection.Open();

				reportResults = new DataSet();

				// Execute the fill method
				SqlHelper.FillDataset(connection, reportSp, reportResults, new string[] {"PredictedPrices"}, parameters);

				// We close the connection
				connection.Close();
			}
			catch (SqlException ex)
			{
				ThrowDBException(ex, reportSp, parameters);
			}
			finally
			{
				T.X();
			}
			return reportResults;
		}

//		/// <summary>
//		/// This method retrieves the fund price drift for a fund group ID.
//		/// </summary>
//		/// <param name="fundGroupID">The ID of the fund group for which to report.</param>
//		/// <returns>A data set containing the fund drift data for the fund group.</returns>
//		/// <exception cref="DatabaseException">Unable to load company</exception>
//		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
//		public DataSet FundDriftPricesReport(int fundGroupID)
//		{
//			T.E();
//			const string reportSp = "usp_FundDriftReport";
//			// Build the parameters collection
//			SqlParameter[] parameters = new SqlParameter[1];
//			DataSet reportResults = null;
//
//			try
//			{
//				// Set up the parameters.
//				parameters[0] = new SqlParameter("@fundGroupID", SqlDbType.Int);
//				parameters[0].Value = fundGroupID;
//
//				// Start a new SQL connection
//				SqlConnection connection = new SqlConnection(ConnectionString);
//				connection.Open();
//
//				reportResults = new DataSet();
//
//				// Execute the fill method
//				SqlHelper.FillDataset(connection, reportSp, reportResults, new string[] {"FundDrift"}, parameters);
//
//				// We close the connection
//				connection.Close();
//
//			}
//			catch (SqlException ex)
//			{
//				ThrowDBException(ex, reportSp, parameters);
//			}
//			finally
//			{
//				T.X();
//			}
//
//			return reportResults;
//		}

		#endregion
	
	}
}