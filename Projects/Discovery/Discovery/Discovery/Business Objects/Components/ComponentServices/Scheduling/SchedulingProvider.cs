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
using Discovery.Utility.Configuration;

namespace LASPortal
{
    namespace Scheduling
    {
        // ''''''''''''''''''''''''''''''''''''''''''''''''''
        // Set up our delegates so we can track
        // and react to events of the scheduler clients
        // ''''''''''''''''''''''''''''''''''''''''''''''''''
        /// <summary>
        /// Delegate for work started
        /// </summary>
        /// <param name="objSchedulerClient"></param>
        public delegate void WorkStarted(SchedulerClient objSchedulerClient);

        /// <summary>
        /// Delegate for work in processing
        /// </summary>
        /// <param name="objSchedulerClient"></param>
        public delegate void WorkProgressing(SchedulerClient objSchedulerClient);

        /// <summary>
        /// Delegate for work completed
        /// </summary>
        /// <param name="objSchedulerClient"></param>
        public delegate void WorkCompleted(SchedulerClient objSchedulerClient);

        /// <summary>
        /// delegate for work in error
        /// </summary>
        /// <param name="objSchedulerClient"></param>
        /// <param name="objException"></param>
        public delegate void WorkErrored(SchedulerClient objSchedulerClient, Exception objException);

        /// <summary>
        /// An abstract class to provide the schedule
        /// </summary>
        public abstract class SchedulingProvider
        {
            //  provider constants - eliminates need for Reflection later
            private const string ProviderType = "scheduling";

            private ProviderConfiguration _providerConfiguration =
                ProviderConfiguration.GetProviderConfiguration(ProviderType);

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

            /// <summary>
            /// An enumeration for the event name APPLICATION_START
            /// </summary>
            public EventName EventName;

            /// <summary>
            /// Gets the provider path.
            /// </summary>
            /// <value>The provider path.</value>
            public string ProviderPath
            {
                get { return _providerPath; }
            }

            /// <summary>
            /// Gets a value indicating whether this <see cref="T:SchedulingProvider"/> is debug.
            /// </summary>
            /// <value><c>true</c> if debug; otherwise, <c>false</c>.</value>
            public static bool Debug
            {
                get { return _Debug; }
            }

            /// <summary>
            /// Gets the max threads.
            /// </summary>
            /// <value>The max threads.</value>
            public static int MaxThreads
            {
                get { return _MaxThreads; }
            }

            /// <summary>
            /// Gets a value indicating whether this <see cref="T:SchedulingProvider"/> is enabled.
            /// </summary>
            /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
            public static bool Enabled
            {
                get { return _Enabled; }
            }

            // ***************************
            // User impersonation
            // ***************************

            /// <summary>
            /// Gets a value indicating whether [impersonate user].
            /// </summary>
            /// <value><c>true</c> if [impersonate user]; otherwise, <c>false</c>.</value>
            public static bool ImpersonateUser
            {
                get { return _Impersonate; }
            }

            /// <summary>
            /// Gets the name of the user.
            /// </summary>
            /// <value>The name of the user.</value>
            public static string UserName
            {
                get { return _UserName; }
            }

            /// <summary>
            /// Gets the user password.
            /// </summary>
            /// <value>The user password.</value>
            public static string UserPassword
            {
                get { return _UserPassword; }
            }

            // ***************************
            // Asynchronous
            // ***************************

            /// <summary>
            /// Gets a value indicating whether this <see cref="T:SchedulingProvider"/> is asynchronous.
            /// </summary>
            /// <value><c>true</c> if asynchronous; otherwise, <c>false</c>.</value>
            public static bool Asynchronous
            {
                get { return _Asynchronous; }
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="T:SchedulingProvider"/> class.
            /// </summary>
            public SchedulingProvider()
            {
                Provider objProvider =
                    (Provider) _providerConfiguration.Providers[_providerConfiguration.DefaultProvider];

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
                        if (objProvider.Attributes["username"] != null && objProvider.Attributes["userpassword"] != null
                            )
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

            /// <summary>
            /// Instances this instance.
            /// </summary>
            /// <returns></returns>
            public static SchedulingProvider Instance()
            {
                string strCacheKey = ProviderType + "provider";

                // --------------------------------------------------------------
                //  Use the cache because the reflection used later is expensive
                // --------------------------------------------------------------

                ConstructorInfo objConstructor = null; //= (ConstructorInfo)DataCache.GetCache(strCacheKey); 

                if (objConstructor == null)
                {
                    // --------------------------------------------------------------
                    //  Get the name of the provider
                    // --------------------------------------------------------------
                    ProviderConfiguration objProviderConfiguration =
                        ProviderConfiguration.GetProviderConfiguration(ProviderType);

                    // --------------------------------------------------------------
                    //  The assembly should be in \bin or GAC, so we simply need to
                    //  get an instance of the type
                    // --------------------------------------------------------------
                    try
                    {
                        // --------------------------------------------------------------
                        //  Get the typename of the LoggingProvider from web.config
                        // --------------------------------------------------------------
                        string strTypeName =
                            ((Provider) objProviderConfiguration.Providers[objProviderConfiguration.DefaultProvider]).
                                Type;

                        // --------------------------------------------------------------
                        //  Use reflection to store the constructor of the class that implements LoggingProvider
                        // --------------------------------------------------------------
                        Type t = Type.GetType(strTypeName, true);
                        objConstructor = t.GetConstructor(Type.EmptyTypes);

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

                return (SchedulingProvider) objConstructor.Invoke(null);
            }

            /// <summary>
            /// Gets the provider path.
            /// </summary>
            /// <returns></returns>
            public abstract string GetProviderPath();

            /// <summary>
            /// Starts this instance.
            /// </summary>
            public abstract void Start();

            /// <summary>
            /// Res the start.
            /// </summary>
            /// <param name="SourceOfRestart">The source of restart.</param>
            public abstract void ReStart(string SourceOfRestart);

            /// <summary>
            /// Starts the and wait for response.
            /// </summary>
            public abstract void StartAndWaitForResponse();

            /// <summary>
            /// Halts the specified source of halt.
            /// </summary>
            /// <param name="SourceOfHalt">The source of halt.</param>
            public abstract void Halt(string SourceOfHalt);

            /// <summary>
            /// Purges the schedule history.
            /// </summary>
            public abstract void PurgeScheduleHistory();

            /// <summary>
            /// Runs the event schedule.
            /// </summary>
            /// <param name="objEventName">Name of the obj event.</param>
            public abstract void RunEventSchedule(EventName objEventName);

            /// <summary>
            /// Gets the schedule.
            /// </summary>
            /// <returns></returns>
            public abstract ArrayList GetSchedule();

            /// <summary>
            /// Gets the schedule.
            /// </summary>
            /// <param name="ScheduleID">The schedule ID.</param>
            /// <returns></returns>
            public abstract ScheduleItem GetSchedule(int ScheduleID);

            /// <summary>
            /// Gets the schedule history.
            /// </summary>
            /// <param name="ScheduleID">The schedule ID.</param>
            /// <returns></returns>
            public abstract ArrayList GetScheduleHistory(int ScheduleID);

            /// <summary>
            /// Gets the schedule queue.
            /// </summary>
            /// <returns></returns>
            public abstract ArrayList GetScheduleQueue();

            /// <summary>
            /// Gets the schedule processing.
            /// </summary>
            /// <returns></returns>
            public abstract ArrayList GetScheduleProcessing();

            /// <summary>
            /// Gets the free thread count.
            /// </summary>
            /// <returns></returns>
            public abstract int GetFreeThreadCount();

            /// <summary>
            /// Gets the active thread count.
            /// </summary>
            /// <returns></returns>
            public abstract int GetActiveThreadCount();

            /// <summary>
            /// Gets the max thread count.
            /// </summary>
            /// <returns></returns>
            public abstract int GetMaxThreadCount();

            /// <summary>
            /// Gets the schedule status.
            /// </summary>
            /// <returns></returns>
            public abstract ScheduleStatus GetScheduleStatus();

            /// <summary>
            /// Adds the schedule.
            /// </summary>
            /// <param name="objScheduleItem">The obj schedule item.</param>
            /// <returns></returns>
            public abstract int AddSchedule(ScheduleItem objScheduleItem);

            /// <summary>
            /// Updates the schedule.
            /// </summary>
            /// <param name="objScheduleItem">The obj schedule item.</param>
            public abstract void UpdateSchedule(ScheduleItem objScheduleItem);

            /// <summary>
            /// Deletes the schedule.
            /// </summary>
            /// <param name="objScheduleItem">The obj schedule item.</param>
            public abstract void DeleteSchedule(ScheduleItem objScheduleItem);
        }
    }
}