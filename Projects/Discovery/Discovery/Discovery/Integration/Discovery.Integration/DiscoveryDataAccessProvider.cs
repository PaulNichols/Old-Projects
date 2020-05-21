using System;
using System.Data;
using System.Reflection;
using Discovery.Utility.Configuration;
using Discovery.Utility.DataAccess;

namespace Discovery.Integration.DataAccess
{
    /// <summary>
    /// An abstract class DataAccessProvider
    /// </summary>
    public abstract class DiscoveryDataAccessProvider
    {
        #region Factory Support

        // Provider constants - eliminates need for Reflection later
        private static string providerType = "discoveryData";

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
        public static DiscoveryDataAccessProvider Instance()
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
        public static DiscoveryDataAccessProvider Instance(string providerName)
        {
            return Instance(providerName, ProviderConfiguration.GetProviderConfiguration(ProviderType));
        }

        private static DiscoveryDataAccessProvider Instance(string providerName,
                                                            ProviderConfiguration providerConfiguration)
        {
            // Cache key name
            string strCacheKey = ProviderType + "_provider_" + providerName;

            // Use the cache because the reflection used later is expensive
            ConstructorInfo objConstructor = (ConstructorInfo) DataCache.GetCache(strCacheKey);

            // Check if constructor was in cache
            if (null == objConstructor)
            {
                // The assembly should be in bin or GAC, so we simply need to get an instance of the type
                try
                {
                    // Get the typename of the default DataProvider from web.config
                    string strTypeName = ((Provider) providerConfiguration.Providers[providerName]).Type;
                    // Use reflection to store the constructor of the class that implements DataProvider
                    Type t = Type.GetType(strTypeName, true);
                    // Get public instance constructor that takes no parameters
                    objConstructor = t.GetConstructor(Type.EmptyTypes);
                    // Insert the type into the cache
                    DataCache.SetCache(strCacheKey, objConstructor);
                }
                catch (Exception)
                {
                    // Could not load the provider, this is likely due to binary compatibility issues or the assembly cannot be found 
                    throw;
                }
            }
            // Create the data provider instance
            DiscoveryDataAccessProvider objProvider = (DiscoveryDataAccessProvider) objConstructor.Invoke(null);
            // Read the configuration specific information for this provider
            objProvider.Initialise((Provider) providerConfiguration.Providers[providerName]);

            // Return the DataProvider instance
            return objProvider;
        }

        /// <summary>
        /// Initialises the specified provider name.
        /// </summary>
        /// <param name="provider">The provider.</param>
        protected abstract void Initialise(Provider provider);

        #endregion

        public abstract IDataReader GetErrorType(string exceptionType, string policyName, string opCoCode);
    }
}