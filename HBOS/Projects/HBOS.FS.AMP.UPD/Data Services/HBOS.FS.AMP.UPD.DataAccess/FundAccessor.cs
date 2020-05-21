using System;
using System.Data;
using System.Data.SqlClient;

using Microsoft.Practices.EnterpriseLibrary.Data;
using HBOS.FS.AMP.UPD.Types.Funds;

namespace HBOS.FS.AMP.UPD.DataAccess
{
	/// <summary>
	/// The class used for implementing all the database calls for the
	/// retrieval and maintenance of Fund objects.
	/// </summary>
	public class FundAccessor
	{
		/// <summary>
		/// Returns a single Fund object matching the given HiPortfolioCode.
		/// </summary>
		/// <param name="hiPortfolioCode">
		/// The identifier for the Fund object.
		/// </param>
		/// <returns>The Fund object matching the given HiPortfolio code.</returns>
		public Fund GetFund(string hiPortfolioCode)
		{
			Database database = DatabaseFactory.CreateDatabase();

			// Set up the command.
			string commandString = "FundGetFromHiPortfolioCode";
			DBCommandWrapper commandWrapper = database.GetStoredProcCommandWrapper(commandString);
			commandWrapper.AddInParameter("@sHiPortfolioCode", DbType.StringFixedLength, hiPortfolioCode);
			commandWrapper.AddOutParameter("@sFullName", DbType.String, 100);
			commandWrapper.AddOutParameter("@sShortName", DbType.String, 50);
			commandWrapper.AddOutParameter("@sIsinCode", DbType.String, 10);
			commandWrapper.AddOutParameter("@sMexCode", DbType.String, 10);
			commandWrapper.AddOutParameter("@sPolicyAdministrationCode", DbType.String, 10);
			commandWrapper.AddOutParameter("@sSedolNumber", DbType.String, 7);
			commandWrapper.AddOutParameter("@sFundType", DbType.StringFixedLength, 1);
			commandWrapper.AddOutParameter("@sParentHiPortfolioCode", DbType.StringFixedLength, 5);
			commandWrapper.AddOutParameter("@iFundGroupID", DbType.Int32, 1);
			commandWrapper.AddOutParameter("@sAssetFundCode", DbType.StringFixedLength, 5);

			// Execute the command.
			database.ExecuteNonQuery(commandWrapper);

			// Create the Fund object.
			string databaseType = commandWrapper.GetParameterValue("@sFundType").ToString();
			FundFactory.FundType type = this.resolveFundType(databaseType);
			Fund retrievedFund = FundFactory.CreateFund(type, string.Empty, string.Empty, string.Empty,
				string.Empty, string.Empty, string.Empty, string.Empty, 0, string.Empty, false, false,
				string.Empty, string.Empty, 0, false, null, 0, false, 0, false, DateTime.Now, false, 0,
				false, 0, false, 0, 0, 0, 0, 0, false, 0, false, 0, false, Fund.FundStatusType.Missing,
				DateTime.Now, false, null, false, null);

			if (retrievedFund != null)
			{
				retrievedFund.FullName = commandWrapper.GetParameterValue("@sFullName").ToString();
				retrievedFund.ShortName = commandWrapper.GetParameterValue("@sShortName").ToString();
				retrievedFund.IsinCode = commandWrapper.GetParameterValue("@sIsinCode").ToString();
				retrievedFund.MexxCode = commandWrapper.GetParameterValue("@sMexCode").ToString();
				retrievedFund.PolicyAdministrationCode 
					= commandWrapper.GetParameterValue("@sPolicyAdministrationCode").ToString();
				retrievedFund.SedolNumber = int.Parse(commandWrapper.GetParameterValue("@sSedolNumber").ToString());
			}

			return retrievedFund;
		}

		/// <summary>
		/// Returns the collection of Fund objects for the given company.
		/// </summary>
		/// <param name="companyID">The ID of the company for which the funds are to be retrieved.</param>
		/// <returns>A collection of matching funds.</returns>
		public FundCollection GetFundsByCompany(string companyCode)
		{
			Database database = DatabaseFactory.CreateDatabase();

			// Set up the command.
			string commandString = "FundListForCompany";
			DBCommandWrapper commandWrapper = database.GetStoredProcCommandWrapper(commandString);
			commandWrapper.AddInParameter("@sCompanyCode", DbType.String, companyCode);

			// Create the funds collection.
			FundCollection funds = new FundCollection();

			using (IDataReader dataReader = database.ExecuteReader(commandWrapper))
			{
				while (dataReader.Read())
				{
					Fund newFund = this.createNewFund(dataReader);
					funds.Add(newFund);
				}
			}

			return funds;
		}

		public FundCollection GetFundsByAssetFund(string assetFundCode)
		{
			Database database = DatabaseFactory.CreateDatabase();

			// Set up the command.
			string commandString = "FundListForAssetFund";
			DBCommandWrapper commandWrapper = database.GetStoredProcCommandWrapper(commandString);
			commandWrapper.AddInParameter("@sAssetFundCode", DbType.StringFixedLength, assetFundCode);

			// Create the funds collection.
			FundCollection funds = new FundCollection();

			using (IDataReader dataReader = database.ExecuteReader(commandWrapper))
			{
				while (dataReader.Read())
				{
					Fund newFund = this.createNewFund(dataReader);
					funds.Add(newFund);
				}
			}

			return funds;
		}

		public Fund AddFund()
		{
			return null;
		}

		public bool DeleteFund(string hiPortfolioCode)
		{
			return true;
		}

		public bool UpdateFund(Fund fund)
		{
			return true;
		}

		/// <summary>
		/// Create a new Fund object from the current database record.
		/// </summary>
		/// <param name="dataReader">The sequential data reader for the Funds dataset.</param>
		/// <returns>A populated Fund object.</returns>
		private Fund createNewFund(IDataReader dataReader)
		{
			// Create a new Fund object.
			string databaseType = dataReader["@sFundType"].ToString();
			FundFactory.FundType type = this.resolveFundType(databaseType);
			Fund newFund = FundFactory.CreateFund(type, string.Empty, string.Empty, string.Empty,
				string.Empty, string.Empty, string.Empty, string.Empty, 0, string.Empty,
				false, false, string.Empty, string.Empty, 0, false, null, 0, false, 0, false, DateTime.Now,
				false, 0, false, 0, false, 0, 0, 0, 0, 0, false, 0, false, 0, false,
				Fund.FundStatusType.Missing, DateTime.Now, false, null, false, null);

			// Populate the Fund from the database record.
			newFund.HiPortfolioCode = dataReader["HiPortfolioCode"].ToString();
			newFund.ShortName = dataReader["shortName"].ToString();
			newFund.FullName = dataReader["fullName"].ToString();
			newFund.IsinCode = dataReader["isinCode"].ToString();
			newFund.MexxCode = dataReader["mexCode"].ToString();
			newFund.PolicyAdministrationCode = dataReader["policyAdministrationCode"].ToString();
			newFund.SedolNumber = int.Parse(dataReader["sedolNumber"].ToString());

			return newFund;
		}

		/// <summary>
		/// Translates the database fund type code to a recognised enum.
		/// </summary>
		/// <param name="fundType">The fund type as returned from the database.</param>
		/// <returns>The FundType enum for the fund type.</returns>
		private FundFactory.FundType resolveFundType(string fundType)
		{
			switch (fundType)
			{
				case "G":
					return FundFactory.FundType.General;
				case "O":
					return FundFactory.FundType.Oeic;
				default:
					return FundFactory.FundType.General;
			}
		}

		/// <summary>
		/// Translates the enum to a database fund type code.
		/// </summary>
		/// <param name="fundType">The fund type enum.</param>
		/// <returns>The fund type as used in the database.</returns>
		private string resolveFundType(FundFactory.FundType fundType)
		{
			switch (fundType)
			{
				case FundFactory.FundType.General:
					return "G";
				case FundFactory.FundType.Oeic:
					return "O";
				default:
					return "G";
			}
		}
	}
}
