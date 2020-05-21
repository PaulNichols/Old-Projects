using System;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Persistence;
using HBOS.FS.AMP.UPD.Types.Companies;
using HBOS.FS.AMP.UPD.Types.Lookups;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Controllers
{
	/// <summary>
	/// Controller object for all things Company orientated.  
	/// This will handle calls to the business and data layers.
	/// This is used to keep the logical layers seperate.  
	/// </summary>
	public class CompanyController
	{
		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public CompanyController()
		{
			//
			// TODO: Set the right connection string
			//
		}

		#endregion

		#region Load methods

		/// <summary>
		/// Load the details of the specified company into a company object
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="companyCode">Code for the company data to be extracted</param>
		/// <returns>A company object containing the company details</returns>
		/// <exception cref="DatabaseException">Unable to load company</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		public  static Company LoadCompany(string connectionString, string companyCode)
		{
			T.E();
			Company co = null;
			try
			{
				CompanyPersister persister = new CompanyPersister(connectionString);
				co = persister.LoadCompany(companyCode);
			}
			finally
			{
				T.X();
			}
			return co;
		}

		/// <summary>
		/// Loads the company by name .
		/// </summary>
		/// <param name="connectionString">Connection string.</param>
		/// <param name="companyName">Name of the company.</param>
		/// <returns></returns>
		public  static CompanyDetails LoadCompanyCodeByName(string connectionString, string companyName)
		{
			T.E();
			CompanyDetails companyDetails=null;
			try
			{
				CompanyPersister persister = new CompanyPersister(connectionString);
				companyDetails=new CompanyDetails(persister.LoadCompanyByName(companyName));
			}
			finally
			{
				T.X();
			}
			return companyDetails;
		}

		/// <summary>
		/// Load the details of the specified company into a company object
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="companyCode">Code for the company data to be extracted</param>
		/// <returns>A company object containing the company details</returns>
		/// <exception cref="DatabaseException">Unable to load company</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		public  static CompanyDetails LoadCompanyByCode(string connectionString, string companyCode)
		{
			T.E();
			CompanyDetails companyDetails=null;
			try
			{
				CompanyPersister persister = new CompanyPersister(connectionString);
				companyDetails=new CompanyDetails(persister.LoadCompany(companyCode));
			}
			finally
			{
				T.X();
			}
			return companyDetails;
		}

		/// <summary>
		/// Extract all company details available to the specified user
		/// </summary>
		/// <param name="connectionString"></param>
		/// <returns>CompanyCollection object</returns>
		/// <exception cref="DatabaseException">Unable to load companies</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		public  static SimpleStringLookupCollection LoadCompanies(string connectionString)
		{
			T.E();
			SimpleStringLookupCollection coColl = null;
			try
			{
				CompanyLookupPersister persister = new CompanyLookupPersister(connectionString);
				coColl = persister.LoadCompanies();
			}
			finally
			{
				T.X();
			}
			return coColl;
		}

		/// <summary>
		/// Gets the import directory for the supplied company code
		/// </summary>
		/// <param name="connectionString">Database connection string</param>
		/// <param name="companyCode">Company to retrieve the import directory for.</param>
		/// <returns>The import driectory for teh current company</returns>
		/// <exception cref="DatabaseException">Unable to load company</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		public  static string GetImportDirectory(string connectionString, string companyCode)
		{
			T.E();
			string importDirectory = string.Empty;
			try
			{
				CompanyPersister persister = new CompanyPersister(connectionString);
				importDirectory = persister.GetImportDirectory(companyCode);
			}
			finally
			{
				T.X();
			}
			return importDirectory;
		}

		#endregion

		#region Update methods

		/// <summary>
		/// Save modified company details through the persistance layer
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="updatedCompany">Company object containing modified company details</param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public  static void UpdateCompany(string connectionString, Company updatedCompany)
		{
			T.E();
			try
			{
				CompanyPersister persister = new CompanyPersister(connectionString);
				persister.SaveCompany(updatedCompany);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Progresses the company valuation day.
		/// </summary>
		/// <param name="connectionString">Connection string.</param>
		/// <param name="companyCode">Company code.</param>
		public  static void ProgressCompanyValuationDay(string connectionString, string companyCode)
		{
			T.E();
			try
			{
				CompanyPersister persister = new CompanyPersister(connectionString);
				persister.ProgressCompanyValuationDay(companyCode);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Save a collection of modified company details through the persistance layer
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="updatedCompanies">CompanyCollection object containing any modified company details</param>
		/// <returns>Success/failure flag</returns>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public  static bool UpdateCompanies(string connectionString, CompanyCollection updatedCompanies)
		{
			T.E();
			bool ret = false;
			try
			{
				CompanyPersister persister = new CompanyPersister(connectionString);
				ret = persister.SaveCompanies(updatedCompanies);
			}
			finally
			{
				T.X();
			}
			return ret;
		}

		/// <summary>
		/// Make a call to the persistance layer to delete the specified company
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="deleteCompany">Code of the company to be deleted</param>
		/// <returns>Success/failure flag</returns>
		public  static void DeleteCompany(string connectionString, Company deleteCompany)
		{
			T.E();
			try
			{
				CompanyPersister persister = new CompanyPersister(connectionString);
				deleteCompany.IsDeleted = true;
				persister.SaveCompany(deleteCompany);
			}
			finally
			{
				T.X();
			}
		}

		#endregion
	}

	/// <summary>
	/// 
	/// </summary>
	public class CompanyDetails
	{
		private string m_CompanyCode;
		private DateTime m_CompanyValuationDate;
		private DateTime m_NextValuationDate;
		private DateTime m_PreviousValuationDate;


		/// <summary>
		/// Gets the company code.
		/// </summary>
		/// <value></value>
		public string CompanyCode
		{
			get { return m_CompanyCode; }
		}

		/// <summary>
		/// Gets the company valuation date.
		/// </summary>
		/// <value></value>
		public DateTime CompanyValuationDate
		{
			get { return m_CompanyValuationDate; }
		}

		/// <summary>
		/// Gets the next valuation date.
		/// </summary>
		/// <value></value>
		public DateTime NextValuationDate
		{
			get { return m_NextValuationDate; }
		}

		/// <summary>
		/// Gets the next valuation date.
		/// </summary>
		/// <value></value>
		public DateTime PreviousValuationDate
		{
			get { return m_PreviousValuationDate; }
		}

		/// <summary>
		/// Creates a new <see cref="CompanyDetails"/> instance.
		/// </summary>
		/// <param name="company">Company.</param>
		public CompanyDetails(Company company)
		{
			m_CompanyCode=company.CompanyCode;
			m_CompanyValuationDate=company.ValuationDate;
			m_NextValuationDate=company.NextValuationDate;
			m_PreviousValuationDate=company.PreviousValuationDate;
		}
	}
}