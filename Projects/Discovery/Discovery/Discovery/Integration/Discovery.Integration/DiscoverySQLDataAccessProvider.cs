using System;
using System.Data;
using Discovery.Utility.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Discovery.Integration.DataAccess
{
    /// <summary>
    /// A class to provide the SQL data access provider which is in integration folder
    /// with namespace Discovery.Integration.DataAccess
    /// </summary>
    public class DiscoverySQLDataAccessProvider : DiscoveryDataAccessProvider
    {
        private const int ERRORVALUE = -1;
        private string connectionString;
        private string upgradeConnectionString;
        private string providerPath;
        private string objectQualifier;
        private string databaseOwner;

        //private ProviderConfiguration m_providerConfiguration =
        //    ProviderConfiguration.GetProviderConfiguration(ProviderType);


        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        public string ConnectionString
        {
            get { return connectionString; }
        }

        /// <summary>
        /// Gets the provider path.
        /// </summary>
        /// <value>The provider path.</value>
        public string ProviderPath
        {
            get { return providerPath; }
        }

        /// <summary>
        /// Gets the object qualifier.
        /// </summary>
        /// <value>The object qualifier.</value>
        public string ObjectQualifier
        {
            get { return objectQualifier; }
        }

        /// <summary>
        /// Gets the database owner.
        /// </summary>
        /// <value>The database owner.</value>
        public string DatabaseOwner
        {
            get { return databaseOwner; }
        }

        /// <summary>
        /// Executes the reader.
        /// </summary>
        /// <param name="spNameWithoutQualifier">The sp name without qualifier.</param>
        /// <param name="sqlParameters">The SQL parameters.</param>
        /// <returns></returns>
        private IDataReader ExecuteReader(string spNameWithoutQualifier, params object[] sqlParameters)
        {
            Database database = DatabaseFactory.CreateDatabase(ConnectionString);
            return database.ExecuteReader(String.Concat(ObjectQualifier, spNameWithoutQualifier), sqlParameters);
        }


        /// <summary>
        /// Initialises the specified provider name.
        /// </summary>
        /// <param name="provider">The provider.</param>
        protected override void Initialise(Provider provider)
        {
            // Read the attributes for this provider
            connectionString = provider.Attributes["connectionString"];
            upgradeConnectionString = provider.Attributes["upgradeConnectionString"];
            providerPath = provider.Attributes["provIderPath"];
            objectQualifier = provider.Attributes["objectQualifier"];
            databaseOwner = provider.Attributes["databaseOwner"];

            if (objectQualifier != "" && !objectQualifier.EndsWith("_"))
            {
                // Append "_" to qualifier name if qualifier specified and name does not end with "_"
                objectQualifier += "_";
            }

            if (databaseOwner != "" && !databaseOwner.EndsWith("."))
            {
                // Append "." to database owner if owner specified and does not end with "."
                databaseOwner += ".";
            }

            // See if we have an upgrade connection string
            if ("" == upgradeConnectionString)
            {
                upgradeConnectionString = connectionString;
            }
        }


        public override IDataReader GetErrorType(string exceptionType, string policyName, string opCoCode)
        {
              IDataReader reader = null;

            try
            {
                reader = ExecuteReader("GetErrorType",
                                       new object[]
                                           {
                                               exceptionType,
                                               policyName,
                                               opCoCode
                                           });
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "DataProvider Policy")) throw;
            }
            return reader;
        }
    }
}