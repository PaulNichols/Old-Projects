/*************************************************************************************************
 ** FILE:	global.asax.cs
 ** DATE:	01/08/2004
 ** AUTHOR:	Lee Spring
 **
 ** COPYRIGHT:
 ** Lee Spring
 ** LAS Solutions Ltd - www.las-solutions.co.uk
 ** Copyright (c) 2004 LAS Solutions Ltd
 **
 ** THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
 ** TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
 ** THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
 ** CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 ** DEALINGS IN THE SOFTWARE.
 **
 ** OVERVIEW:
 **
 ** MODIFICATION HISTORY:
 **
 ** Date:		Version:	Who:	Change:
 ** 1/8/04		1.0			LAS		Initial Version
 ************************************************************************************************/
 
using System; 
using System.Collections; 
using System.Reflection; 
using System.Web.Caching; 
using System.Data;
using System.Diagnostics;
using Discovery.Utility.Configuration;
using Discovery.Scheduling;
using Discovery.Utility.DataAccess;

namespace Discovery.Scheduling 
{
    public abstract class DataProvider
	{ 
        //  provider constants - eliminates need for Reflection later
		//  maps to <sectionGroup> in web.config
        private const string ProviderType = "schedulingdata"; 

        /// <summary>
        /// Instances this instance.
        /// </summary>
        /// <returns></returns>
        public static DataProvider Instance()
        {
            // Get the name of the provider
            ProviderConfiguration objProviderConfiguration =
                ProviderConfiguration.GetProviderConfiguration(ProviderType);

            // Instanciate the provider
            return Instance(objProviderConfiguration.DefaultProvider, objProviderConfiguration);
        }

        /// <summary>
        /// Instances the specified provider name.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        /// <returns></returns>
        public static DataProvider Instance(string providerName)
        {
            // Get the name of the provider
            ProviderConfiguration objProviderConfiguration =
                ProviderConfiguration.GetProviderConfiguration(ProviderType);

            // Instanciate the provider
            return Instance(providerName, objProviderConfiguration);
        }

        /// <summary>
        /// Instances the specified provider name.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        /// <param name="providerConfiguration">The provider configuration.</param>
        /// <returns></returns>
        private static DataProvider Instance(
                    string providerName, 
                    ProviderConfiguration providerConfiguration)
        {
            // Cache key name
            string strCacheKey = ProviderType + "_provider_" + providerName;

            // Use the cache because the reflection used later is expensive
            ConstructorInfo objConstructor = (ConstructorInfo)DataCache.GetCache(strCacheKey);

            // Check if constructor was in cache
            if (null == objConstructor)
            {
                // The assembly should be in bin or GAC, so we simply need to get an instance of the type
                try
                {
                    // Get the typename of the default DataProvider from web.config
                    string strTypeName = ((Provider)providerConfiguration.Providers[providerName]).Type;
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
            DataProvider objProvider = (DataProvider)objConstructor.Invoke(null);

            // Read values from config
            objProvider.Initialise(providerName);

            // Return the DataProvider instance
            return objProvider;
        }

        /// <summary>
        /// Initialises the specified provider name.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        protected abstract void Initialise(string providerName);

        //  all core methods defined below
        
        public abstract IDataReader GetSchedule();

        public abstract IDataReader GetSchedules(string sortExpression, int startRowIndex, int maximumRows, out int totalRows);
        
        public abstract IDataReader GetSchedule(int scheduleID);
        
        public abstract IDataReader GetSchedule(string eventName);
        
        public abstract IDataReader GetScheduleHistory(int scheduleID);

        public abstract IDataReader GetScheduleHistories(int scheduleID,
                                                         string sortExpression,
                                                         int startRowIndex,
                                                         int maximumRows,
                                                         out int totalRows);

        public abstract int SaveSchedule(ScheduleItem scheduleItem);
        
        //public abstract int AddSchedule(string TypeFullName, int TimeLapse, string TimeLapseMeasurement, int RetryTimeLapse, string RetryTimeLapseMeasurement, int RetainHistoryNum, string AttachToEvent, bool CatchUpEnabled, bool Enabled, string ObjectDependencies);
        
        //public abstract void UpdateSchedule(int ScheduleID, string TypeFullName, int TimeLapse, string TimeLapseMeasurement, int RetryTimeLapse, string RetryTimeLapseMeasurement, int RetainHistoryNum, string AttachToEvent, bool CatchUpEnabled, bool Enabled, string ObjectDependencies);
        
        public abstract bool DeleteSchedule(int scheduleId);
        
        public abstract IDataReader GetScheduleItemSettings(int scheduleID);

        public abstract int SaveScheduleHistory(ScheduleHistoryItem scheduleHistoryItem);
        
        //public abstract int AddScheduleHistory(int ScheduleID, DateTime StartDate);
        
        //public abstract void UpdateScheduleHistory(int ScheduleHistoryID, DateTime EndDate, bool Succeeded, string LogNotes, DateTime NextStart);
        
        public abstract bool PurgeScheduleHistory();
        
    } 
} 
