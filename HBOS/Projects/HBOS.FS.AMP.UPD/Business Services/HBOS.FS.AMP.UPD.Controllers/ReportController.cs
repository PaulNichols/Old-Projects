using System;
using System.Data;
using HBOS.FS.Support.Tex;

using HBOS.FS.AMP.UPD.Persistence;

namespace HBOS.FS.AMP.UPD.Controllers
{
	/// <summary>
	/// Report Controller functionality
	/// </summary>
	public class ReportController
	{
		/// <summary>
		/// Load Predicted Prices Report By Fund Group
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="fundGroupID"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <returns></returns>
		public static DataSet LoadPredictedPricesReportByFundGroup(string connectionString, int fundGroupID, DateTime startDate, DateTime endDate )
		{
			T.E();

			try
			{
				ReportPersister reportPersister = new ReportPersister(connectionString);
				return reportPersister.PredictedPricesReportByFundGroup( fundGroupID , startDate , endDate );
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Load Predicted Prices Report By Fund
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="fundID"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <returns></returns>
		public static DataSet LoadPredictedPricesReportByFund( string connectionString , string fundID, DateTime startDate, DateTime endDate)
		{
			T.E();

			try
			{
				ReportPersister reportPersister = new ReportPersister( connectionString );
				return reportPersister.PredictedPricesReportByFund( fundID , startDate , endDate );
			}
			finally
			{
				T.X();
			}
		}

//		/// <summary>
//		/// Retrieves the data for the Fund Drift report.
//		/// </summary>
//		/// <param name="connectionString">The application connection string.</param>
//		/// <param name="fundGroupID">The fund group covered by the report.</param>
//		/// <returns>The fund drift data for the given fund group.</returns>
//		public static DataSet FundDriftPricesReport(string connectionString, int fundGroupID)
//		{
//			T.E();
//
//			try
//			{
//				ReportPersister persister = new ReportPersister(connectionString);
//				return persister.FundDriftPricesReport(fundGroupID);
//			}
//			finally
//			{
//				T.X();
//			}
//		}
		/// <summary>
		/// Loads the price comparision data.
		/// </summary>
		/// <param name="connectionString">Connection string.</param>
		/// <param name="valuationDate">Valuation date.</param>
		/// <param name="snapShotId">Snap shot id.</param>
		/// <param name="datasetToFill">Dataset to fill and return</param>
		/// <returns></returns>
		public static DataSet LoadPriceComparisionData(string connectionString ,DateTime	valuationDate, long snapShotId,DataSet datasetToFill)
		{
			T.E();

			try
			{
				ReportPersister reportPersister = new ReportPersister( connectionString );
				return reportPersister.LoadPriceComparisionData( valuationDate , snapShotId ,datasetToFill);
			}
			finally
			{
				T.X();
			}
		}
	}
}
