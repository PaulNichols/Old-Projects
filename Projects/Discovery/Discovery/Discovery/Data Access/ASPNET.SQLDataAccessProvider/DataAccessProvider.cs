/*************************************************************************************************
 ** FILE:	DataAccessProvider.cs
 ** DATE:	
 ** AUTHOR:
 **
 **
 ** OVERVIEW:
 **
 ** MODIFICATION HISTORY:
 **
 ** Date:		Version:	Who:	Change:
 **
 ************************************************************************************************/
using System;
using System.Data;
using System.Reflection;

using Discovery.Utility.Configuration;
using Discovery.Utility.DataAccess;

namespace ASPNET.SQLDataAccessProvider
{
   /// <summary>
    /// An abstract class DataAccessProvider
    /// </summary>
    public abstract class DataAccessProvider
    {
        #region Factory Support

        // Provider constants - eliminates need for Reflection later
        private static string providerType = "aspnetData";

        /// <summary>
        /// Gets the type of the provider.
        /// </summary>
        /// <value>The type of the provider.</value>
        public static string ProviderType
        {
            get { return providerType; }
        }

        // The provider instance name 
        private string provider = "";

        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        /// <value>The provider.</value>
        public string Provider
        {
            get { return provider; }
            set { provider = value; }
        }

        /// <summary>
        /// Instances this instance.
        /// </summary>
        /// <returns></returns>
        public static DataAccessProvider Instance()
        {
            // Get the name of the provider
            ProviderConfiguration objProviderConfiguration =
                ProviderConfiguration.GetProviderConfiguration(ProviderType);

            // See if a provider was specified, if not use default
            string providerName = objProviderConfiguration.DefaultProvider;
            // Get the name of the provider

            return Instance(providerName, objProviderConfiguration);
        }

        /// <summary>
        /// Instances the specified provider name.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        /// <returns></returns>
        public static DataAccessProvider Instance(string providerName)
        {
            return Instance(providerName, ProviderConfiguration.GetProviderConfiguration(ProviderType));
        }

        private static DataAccessProvider Instance(string providerName, ProviderConfiguration providerConfiguration)
        {
            // Cache key name
            string strCacheKey = ProviderType + "_provider_" + providerName;

            // Use the cache because the reflection used later is expensive
            ConstructorInfo objConstructor = (ConstructorInfo) DataCache.GetCache(strCacheKey);

            // Check if constructor was in cache
            if (null == objConstructor)
            {
                // The assembly should be in bin or GAC, so we simply need to get an instance of the type
                //try
                //{
                    // Get the typename of the default DataProvider from web.config
                    string strTypeName = ((Provider) providerConfiguration.Providers[providerName]).Type;
                    // Use reflection to store the constructor of the class that implements DataProvider
                    Type t = Type.GetType(strTypeName, true);
                    // Get public instance constructor that takes no parameters
                    objConstructor = t.GetConstructor(Type.EmptyTypes);
                    // Insert the type into the cache
                    DataCache.SetCache(strCacheKey, objConstructor);
                //}
                //catch (Exception)
                //{
                //    // Could not load the provider, this is likely due to binary compatibility issues or the assembly cannot be found 
                //    throw;
                //}
            }
            // Create the data provider instance
            DataAccessProvider objProvider = (DataAccessProvider)objConstructor.Invoke(null);
            // Read the configuration specific information for this provider
            objProvider.Initialise((Provider)providerConfiguration.Providers[providerName]);

            // Return the DataProvider instance
            return objProvider;
        }

        /// <summary>
        /// Initialises the specified provider name.
        /// </summary>
        /// <param name="provider">The provider.</param>
        protected abstract void Initialise(Provider provider);

        #endregion

       public abstract IDataReader GetRolesForUser(string applicationName,string userName);
       public abstract IDataReader GetAllRoles(string applicationName);
       public abstract void AddUsersToRoles(string applicationName, string roleNames, string userNames);
       public abstract void CreateRole(string applicationName, string roleName);
       public abstract bool IsUserInRole(string applicationName, string roleName, object username);
       public abstract IDataReader GetUsersInRole(string name, string roleName);
       public abstract bool DeleteRole(string name, string roleName, bool role);
       public abstract bool RoleExists(string name, string roleName);
       public abstract void RemoveUsersFromRoles(string applicationName, string roleNames, string userNames);

       /// <summary>
       /// Checks the security schema version.
       /// </summary>
       /// <returns></returns>
        public abstract int CheckSchemaVersion(string expectedVersion, string[] features);

        /// <summary>
        /// Deletes the profiles specified.
        /// </summary>
        /// <param name="applicationName">Name of the application.</param>
        /// <param name="profiles">The profiles.</param>
        /// <returns></returns>
       public abstract int DeleteProfiles(string applicationName, string profiles);
    }
}
