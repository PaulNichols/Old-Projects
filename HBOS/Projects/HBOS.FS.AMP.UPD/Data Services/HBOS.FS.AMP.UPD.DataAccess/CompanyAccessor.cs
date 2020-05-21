using System;
using System.Data;
using System.Data.SqlClient;

using Microsoft.Practices.EnterpriseLibrary.Data;
using HBOS.FS.AMP.UPD.Types.Companies;

namespace HBOS.FS.AMP.UPD.DataAccess
{
	/// <summary>
    /// The class is used for implementing all the database calls for the
    /// retrieval and maintenance of Company objects.
    /// This is the CRUD (Create; Read; Update; Delete) object for Companies.
	/// </summary>
	public class CompanyAccessor
	{
        #region constructor
        /// <summary>
        /// Default constuctor
        /// </summary>
        public CompanyAccessor()
        {}
        #endregion contsructor

        #region methods
        /// <summary>
        /// Loads a company object from the database for a given company code
        /// </summary>
        /// <param name="companyCode">The company code for the company to load</param>
        /// <returns>The requested company object</returns>
        public Company GetCompany(string companyCode)
        {
            Database database = DatabaseFactory.CreateDatabase();

            // Set up the command.
            string commandString = "usp_HBOSCompanyGetFromCompanyCode";
            DBCommandWrapper commandWrapper = database.GetStoredProcCommandWrapper(commandString);
            
            commandWrapper.AddInParameter("@sCompanyCode", DbType.StringFixedLength, companyCode);

            // Extract the company name for the giver company code
            commandWrapper.AddOutParameter("@sCompanyName", DbType.String, 100);

            // Execute the command.
            database.ExecuteNonQuery(commandWrapper);

            // Create the Company object.
            Company newCompany = CompanyFactory.CreateCompany(companyCode);

            if (newCompany != null)
            {
                newCompany.CompanyName = commandWrapper.GetParameterValue("@sCompanyName").ToString();
                //newCompany.TimeStamp = (byte[])commandWrapper.GetParameterValue("@ts");
            }

            return newCompany;
        }

        /// <summary>
        /// Gets all the companies available to the specified user
        /// </summary>
        /// <param name="userID">The user id of the user to return companies for</param>
        /// <returns>A strongly-typed collection of the user's companies</returns>
        public CompanyCollection GetCompanies(string userID)
        {
            Database database = DatabaseFactory.CreateDatabase();

            // Set up the command.
            string commandString = "usp_HBOSCompanyListForUser";
            DBCommandWrapper commandWrapper = database.GetStoredProcCommandWrapper(commandString);
            commandWrapper.AddInParameter("@sUserID", DbType.String, userID);

            // TODO: Write code to execute the query
            // Execute the query

            // Create the companies collection.
            CompanyCollection companies = new CompanyCollection();

            using (IDataReader dataReader = database.ExecuteReader(commandWrapper))
            {
                while (dataReader.Read())
                {
                    Company newCompany = this.createNewCompany(dataReader);
                    companies.Add(newCompany);
                }
            }

            return companies;
        }

        /// <summary>
        /// Create a new Company object from the current database record.
        /// </summary>
        /// <param name="dataReader">The sequential data reader for the Companies dataset.</param>
        /// <returns>A populated Company object.</returns>
        private Company createNewCompany(IDataReader dataReader)
        {
            string companyCode = dataReader["companyCode"].ToString();
            
            // Create a new Company object.
            Company newCompany = CompanyFactory.CreateCompany(companyCode);

            // Populate the Fund from the database record.
            newCompany.CompanyName = dataReader["companyName"].ToString();
            //newCompany.CompanyCode = dataReader["companyCode"].ToString();
            //newCompany.IsDeleted = dataReader["deleted"];
            
            // TODO: convert timeStamp attribute to correct type
            //newCompany.TimeStamp = datareader["ts"];

            return newCompany;
        }

        /// <summary>
        /// Updates the supplied company in the database
        /// </summary>
        /// <param name="company">The company to update</param>
        /// <returns>bool success flag</returns>
        /// <remarks>Makes a call to the stored procedure usp_HBOSCompanyDelete</remarks>
        /// <remarks>The stored procedure accepts the following parameters: sCompanyCode, ts [timestamp]</remarks>
        public bool UpdateCompany(Company company)
        {
            Database database = DatabaseFactory.CreateDatabase();

            // Set up the command.
            string commandString = "usp_HBOSCompanyUpdate";
            DBCommandWrapper commandWrapper = database.GetStoredProcCommandWrapper(commandString);

            // Build parameters and pass values
            commandWrapper.AddInParameter("@sCompanyCode", DbType.StringFixedLength, company.CompanyCode);
            commandWrapper.AddInParameter("@sCompanyName", DbType.StringFixedLength, company.CompanyName);
            commandWrapper.AddInParameter("@bDeleted", DbType.Byte, company.IsDeleted);
            commandWrapper.AddInParameter("@ts", DbType.Binary, company.TimeStamp);

            return true;
        }

        /// <summary>
        /// Add a new company to the database
        /// </summary>
        /// <param name="company">Company object containing the new company details</param>
        /// <returns>Success/failure flag</returns>
        public bool AddCompany(Company company)
        {
            Database database = DatabaseFactory.CreateDatabase();

            // Set up the command.
            string commandString = "usp_HBOSCompanyCreate";
            DBCommandWrapper commandWrapper = database.GetStoredProcCommandWrapper(commandString);
            
            // Set up parameters and their values
            commandWrapper.AddInParameter("@sCompanyCode", DbType.StringFixedLength, company.CompanyCode);
            commandWrapper.AddInParameter("@sCompanyName", DbType.StringFixedLength, company.CompanyName);
            
            // TODO: Write code to execute the query
            // Execute the query
            return true;
        }

        /// <summary>
        /// Deletes the specified company from the database
        /// </summary>
        /// <param name="companyCode">The company code</param>
        /// <param name="timeStamp">valid timestamp for the existing company</param>
        /// <returns>bool success/failure flag</returns>
        /// <remarks>Calls the stored procedure usp_HBOSCompanyDelete</remarks>
        /// <remarks>The strored procedure accepts the following parameters: sCompanyCode, ts [timestamp]</remarks>
        public bool DeleteCompany(string companyCode, byte[] timeStamp)
        {
            Database database = DatabaseFactory.CreateDatabase();

            // Set up the command.
            string commandString = "usp_HBOSCompanyDelete";
            DBCommandWrapper commandWrapper = database.GetStoredProcCommandWrapper(commandString);
            
            // Create and pass parameter values
            commandWrapper.AddInParameter("@sCompanyCode", DbType.StringFixedLength, companyCode);
            commandWrapper.AddInParameter("@ts", DbType.Binary, timeStamp);
            
            return true;
        }
        #endregion methods
	}
}
