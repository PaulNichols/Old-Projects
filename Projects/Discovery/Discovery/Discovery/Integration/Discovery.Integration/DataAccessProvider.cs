using System;
using System.Data;
using System.Reflection;

using Discovery.Utility.Configuration;
using Discovery.Integration;
using Discovery.Utility.DataAccess;

namespace Discovery.Integration.DataAccess
{
    /// <summary>
    /// An abstract class DataAccessProvider
    /// </summary>
    public abstract class DataAccessProvider
    {
        #region Factory Support

        // Provider constants - eliminates need for Reflection later
        private static string providerType = "integrationdata";

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

        /// <summary>
        /// Gets the tasks.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetTasks();
        
        /// <summary>
        /// Gets the task.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetTask(int taskId);

        /// <summary>
        /// Removes the task.
        /// </summary>
        /// <returns></returns>
        public abstract bool DeleteTask(Task taskToDelete);

        /// <summary>
        /// Saves the task.
        /// </summary>
        /// <returns></returns>
        public abstract int SaveTask(Task taskToSave);

        /// <summary>
        /// Gets the connections.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetConnections();
        
        /// <summary>
        /// Gets the connections.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetConnections(Connection.ConnectionTypeEnum connectionType, Connection.ChannelTypeEnum channelType);

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetConnection(int connectionId);

        /// <summary>
        /// Removes the connection.
        /// </summary>
        /// <param name="connectionToRemove">The connection to remove.</param>
        /// <returns></returns>
        public abstract bool DeleteConnection(Connection connectionToRemove);

        /// <summary>
        /// Saves the connection.
        /// </summary>
        /// <param name="connectionToSave">The connection to save.</param>
        /// <returns></returns>
        public abstract int SaveConnection(Connection connectionToSave);

        /// <summary>
        /// Gets the schedule.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <returns></returns>
        public abstract IDataReader GetSchedule(int scheduleId);

        /// <summary>
        /// Gets the day details for schedule.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <returns></returns>
        public abstract IDataReader GetDayDetailsForSchedule(int scheduleId);

        /// <summary>
        /// Gets the schedule day.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetScheduleDay();

        /// <summary>
        /// Gets the schedules.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetSchedules();

        /// <summary>
        /// Gets the down times.
        /// </summary>
        /// <param name="connectionId">The connection id.</param>
        /// <returns></returns>
        public abstract IDataReader GetDownTimes(int? connectionId);

        /// <summary>
        /// Gets the down time.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public abstract IDataReader GetDownTime(int id);

        /// <summary>
        /// Saves the down time.
        /// </summary>
        /// <param name="connectionDownTime">The connection down time.</param>
        /// <returns></returns>
        public abstract int SaveDownTime(ConnectionDownTime connectionDownTime);

        /// <summary>
        /// Deletes the down time.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns></returns>
        public abstract bool DeleteDownTime(ConnectionDownTime time);

    }
    
   
}