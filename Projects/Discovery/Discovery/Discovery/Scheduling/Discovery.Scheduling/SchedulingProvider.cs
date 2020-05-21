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

using System.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Discovery.Utility.Configuration;
using Discovery.Utility.DataAccess;

namespace Discovery.Scheduling
{
    // ''''''''''''''''''''''''''''''''''''''''''''''''''
    // Set up our delegates so we can track
    // and react to events of the scheduler clients
    // ''''''''''''''''''''''''''''''''''''''''''''''''''
    public delegate void WorkStarted(SchedulerClient objSchedulerClient);
    public delegate void WorkProgressing(SchedulerClient objSchedulerClient);
    public delegate void WorkCompleted(SchedulerClient objSchedulerClient);
    public delegate void WorkErrored(SchedulerClient objSchedulerClient, Exception objException);

    public abstract class SchedulingProvider
    {
        //  provider constants - eliminates need for Reflection later
        private const string ProviderType = "scheduling";

        private ProviderConfiguration _providerConfiguration = ProviderConfiguration.GetProviderConfiguration(ProviderType);
        private string _providerPath;
        private static bool _Debug;
        private static int _MaxThreads;
        private static bool _Enabled;

        // Do we need to impersonate a user on the scheduling thread?
        private static bool _Impersonate = false;
        private static string _UserName = "";
        private static string _UserPassword = "";

        // Do we run ansynchrounously (via the thread pool)
        private static bool _Asynchronous = true;

        public Scheduling.EventName EventName;

        public string ProviderPath
        {
            get
            {
                return _providerPath;
            }
        }

        public static bool Debug
        {
            get
            {
                return _Debug;
            }
        }

        public static int MaxThreads
        {
            get
            {
                return _MaxThreads;
            }
        }

        public static bool Enabled
        {
            get
            {
                return _Enabled;
            }
        }

        // ***************************
        // User impersonation
        // ***************************

        public static bool ImpersonateUser
        {
            get
            {
                return _Impersonate;
            }
        }

        public static string UserName
        {
            get
            {
                return _UserName;
            }
        }

        public static string UserPassword
        {
            get
            {
                return _UserPassword;
            }
        }

        // ***************************
        // Asynchronous
        // ***************************

        public static bool Asynchronous
        {
            get
            {
                return _Asynchronous;
            }
        }

        public SchedulingProvider()
        {
            Provider objProvider = (Provider)_providerConfiguration.Providers[_providerConfiguration.DefaultProvider];

            _providerPath = objProvider.Attributes["providerPath"];

            if (objProvider.Attributes["enabled"] != null)
            {
                _Enabled = Convert.ToBoolean(objProvider.Attributes["enabled"]);
            }

            if (objProvider.Attributes["debug"] != null)
            {
                _Debug = Convert.ToBoolean(objProvider.Attributes["debug"]);
            }

            if (objProvider.Attributes["maxThreads"] != null)
            {
                _MaxThreads = Convert.ToInt32(objProvider.Attributes["maxThreads"]);
            }
            else
            {
                _MaxThreads = 10;
            }

            // See if any impersonation has been specified
            if (objProvider.Attributes["impersonate"] != null)
            {
                // See if we need to impersonate a user
                if (_Impersonate = Convert.ToBoolean(objProvider.Attributes["impersonate"]))
                {
                    // We need to impersonate, see if we have a user name and password
                    if (objProvider.Attributes["username"] != null && objProvider.Attributes["userpassword"] != null)
                    {
                        // Store the user name and password
                        _UserName = objProvider.Attributes["username"];
                        _UserPassword = objProvider.Attributes["userpassword"];
                    }
                    else
                    {
                        // We can't impersonate without a user name and password
                        _Impersonate = false;
                    }
                }
            }
            else
            {
                // No impersonation specified via provider
                _Impersonate = false;
            }

            // See if we run asynchronously
            if (objProvider.Attributes["asynchronous"] != null)
            {
                _Asynchronous = Convert.ToBoolean(objProvider.Attributes["asynchronous"]);
            }
            else
            {
                // We run async by default
                _Asynchronous = true;
            }

        }

        public static SchedulingProvider Instance()
        {
            string strCacheKey = ProviderType + "provider";

            // --------------------------------------------------------------
            //  Use the cache because the reflection used later is expensive
            // --------------------------------------------------------------
            ConstructorInfo objConstructor = (ConstructorInfo)DataCache.GetCache(strCacheKey);

            if (objConstructor == null)
            {
                // --------------------------------------------------------------
                //  Get the name of the provider
                // --------------------------------------------------------------
                ProviderConfiguration objProviderConfiguration = ProviderConfiguration.GetProviderConfiguration(ProviderType);

                // --------------------------------------------------------------
                //  The assembly should be in \bin or GAC, so we simply need to
                //  get an instance of the type
                // --------------------------------------------------------------
                try
                {
                    // --------------------------------------------------------------
                    //  Get the typename of the LoggingProvider from web.config
                    // --------------------------------------------------------------
                    string strTypeName = ((Provider)objProviderConfiguration.Providers[objProviderConfiguration.DefaultProvider]).Type;

                    // --------------------------------------------------------------
                    //  Use reflection to store the constructor of the class that implements LoggingProvider
                    // --------------------------------------------------------------
                    Type t = Type.GetType(strTypeName, true);
                    objConstructor = t.GetConstructor(System.Type.EmptyTypes);

                    // --------------------------------------------------------------
                    //  Insert the type into the cache
                    // --------------------------------------------------------------
                    //DataCache.SetCache(strCacheKey, objConstructor);
                }
                catch
                {
                    // --------------------------------------------------------------
                    //  Could not load the provider - this is likely due to binary compatibility issues 
                    // --------------------------------------------------------------
                }
            }

            return (SchedulingProvider)objConstructor.Invoke(null);
        }

        public abstract string GetProviderPath();

        public abstract void Start();

        public abstract void ReStart(string SourceOfRestart);

        public abstract void StartAndWaitForResponse();

        public abstract void Halt(string SourceOfHalt);

        public abstract void PurgeScheduleHistory();

        public abstract void RunEventSchedule(Scheduling.EventName objEventName);

        public abstract List<ScheduleItem> GetSchedule();

        public abstract ScheduleItem GetSchedule(int ScheduleID);

        public abstract ScheduleHistoryItem GetScheduleHistory(int ScheduleID);

        public abstract ArrayList GetScheduleQueue();

        public abstract ArrayList GetScheduleProcessing();

        public abstract int GetFreeThreadCount();

        public abstract int GetActiveThreadCount();

        public abstract int GetMaxThreadCount();

        public abstract ScheduleStatus GetScheduleStatus();

        public abstract int SaveSchedule(ScheduleItem scheduleItem);

        //public abstract int AddSchedule(ScheduleItem objScheduleItem);

        //public abstract void UpdateSchedule(ScheduleItem objScheduleItem);

        public abstract bool DeleteSchedule(ScheduleItem objScheduleItem);
    }
}

