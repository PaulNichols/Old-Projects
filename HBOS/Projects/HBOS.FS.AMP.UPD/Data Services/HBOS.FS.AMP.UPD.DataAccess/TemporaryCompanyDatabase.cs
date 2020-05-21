using System;

using HBOS.FS.AMP.UPD.Types.Companies;

namespace HBOS.FS.AMP.UPD.DataAccess
{
	/// <summary>
	/// Summary description for TemporaryCompanyDatabase.
	/// </summary>
	public class TemporaryCompanyDatabase
	{
        CompanyCollection m_TemporaryCompanies;
        private static TemporaryCompanyDatabase m_Instance;

        /// <summary>
        /// Allows internal calling whilst not exposing to the client
        /// </summary>
        private TemporaryCompanyDatabase()
        {
            m_TemporaryCompanies = new CompanyCollection();
            Company currentCompany;


            // Generate temp companies
            currentCompany = CompanyFactory.CreateCompany("HIFM");
            currentCompany.CompanyName = "Halifax Investment Fund Managers";
            m_TemporaryCompanies.Add(currentCompany);

            currentCompany = CompanyFactory.CreateCompany("HLL");
            currentCompany.CompanyName = "Halifax Life";
            m_TemporaryCompanies.Add(currentCompany);

            currentCompany = CompanyFactory.CreateCompany("CMIG");
            currentCompany.CompanyName = "Clerical Medical Insurance Group";
            m_TemporaryCompanies.Add(currentCompany);

            currentCompany = CompanyFactory.CreateCompany("SAL");
            currentCompany.CompanyName = "St Andrews Life";
            m_TemporaryCompanies.Add(currentCompany);
        }

        /// <summary>
        /// Make sure only one instance of the object will be instatiated 
        /// </summary>
        /// <returns></returns>
        public static TemporaryCompanyDatabase GetInstance()
        {
            if (m_Instance == null)
            {
                m_Instance = new TemporaryCompanyDatabase();
            }
            return m_Instance;
        }

        /// <summary>
        /// Get a collection of funds
        /// </summary>
        /// <returns></returns>
        public CompanyCollection GetCompanies()
        {
            return m_TemporaryCompanies;
        }

        /// <summary>
        /// Persist the funds back to the DB
        /// </summary>
        /// <param name="companies"></param>
        public void SaveCompanies(CompanyCollection companies)
        {
            m_TemporaryCompanies = companies;
        }        
	}
}
