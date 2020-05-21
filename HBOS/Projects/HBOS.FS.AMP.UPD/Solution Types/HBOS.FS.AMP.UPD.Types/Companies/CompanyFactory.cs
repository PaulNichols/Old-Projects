using System;

namespace HBOS.FS.AMP.UPD.Types.Companies
{
	/// <summary>
    /// Create a company object.
    /// 
    /// Factory Definition:
    /// Define an interface for creating an object, but let subclasses 
    /// decide which class to instantiate. Factory Method lets a class 
    /// defer instantiation to subclasses. 
    /// </summary>
	public class CompanyFactory
	{
		/// <summary>
		/// Creates a new <see cref="CompanyFactory"/> instance.
		/// </summary>
		public CompanyFactory()
		{
		}

        /// <summary>
        /// Create a new company object
        /// </summary>
        /// <returns>A company object with empty attributes</returns>
        public static Company CreateCompany()
        {
            return new Company();
        }

        /// <summary>
        /// Overloaded constructor
        /// Get the correct type of company (only 1 for now, but a factory added
        /// for future-proofing)
        /// </summary>
        /// <returns>The company object of the correct type</returns>
        public static Company CreateCompany(string companyCode)
        {
            return new Company(companyCode);
        }

        /// <summary>
        /// Overloaded constructor
        /// </summary>
        /// <param name="companyCode">Persisted value</param>
        /// <param name="companyName">Persisted value</param>
        /// <param name="timeStamp">Persisted value</param>
        /// <returns></returns>
        public static Company CreateCompany(string companyCode, string companyName, 
            byte[] timeStamp)
        {
            return new Company(companyCode, companyName, timeStamp);
        }
	}
}
