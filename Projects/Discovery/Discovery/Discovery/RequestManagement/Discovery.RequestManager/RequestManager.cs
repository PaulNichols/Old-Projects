/*************************************************************************************************
 ** FILE:	RequestManager.cs
 ** DATE:	30/05/2006
 ** AUTHOR:	Lee Spring
 **
 **
 ** OVERVIEW:
 **
 ** MODIFICATION HISTORY:
 **
 ** Date:		Version:	Who:	Change:
 ** 30/5/06		1.0		    LAS	    Initial Version
 ************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Discovery.Utility;

namespace Discovery.RequestManagement
{
    /// <summary>
    /// Request Manager class
    /// </summary>
    public class RequestManager
    {
        // The request controller
        private RequestController requestController;
        public RequestController RequestController
        {
            get { return requestController; }
        }

        // Description
        private string description;
        private string processorType;

        // Are we running?
        private bool isRunning;

        // Thread settings
        private int maxThreads = 1;
        private int minThreads = 1;
        private decimal workPauseSeconds = 1;

        /// <summary>
        /// Gets the work pause seconds.
        /// </summary>
        /// <value>The work pause seconds.</value>
        public decimal WorkPauseSeconds
        {
            get { return workPauseSeconds; }
        }
        private decimal shutdownPauseSeconds = 20;

        /// <summary>
        /// Gets the shutdown pause seconds.
        /// </summary>
        /// <value>The shutdown pause seconds.</value>
        public decimal ShutdownPauseSeconds
        {
            get { return shutdownPauseSeconds; }
        }

        // Our thread pool for this request manager
        private DiscoveryThreadPool threadPool;

        // Our list of request processors for this request manager
        private List<RequestProcessor> requestProcessors;

        // Dictionary of custom settings
        private Dictionary<String, String> customSettings;

        /// <summary>
        /// Gets the custom settings.
        /// </summary>
        /// <value>The custom settings.</value>
        public Dictionary<String, String> CustomSettings
        {
            get { return customSettings; }
        }

        private RequestManager()
        {
            // We don't have a default constructor
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:RequestManager"/> class.
        /// </summary>
        /// <param name="requestController">The request controller.</param>
        internal RequestManager(RequestController requestController)
        {
            // Create the thread pool
            this.threadPool = new DiscoveryThreadPool();
            // Create the request processors list
            this.requestProcessors = new List<RequestProcessor>();
            // Create the custom settings dictionary
            this.customSettings = new Dictionary<String, String>();
            // Store the controller
            this.requestController = requestController;
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description
        {
            get
            {
                return description;
            }
        }

        /// <summary>
        /// The minimum number of threads to use in order to process messages from the body store.
        /// </summary>
        /// <value>1</value>
        public int MinThreads
        {
            get
            {
                return minThreads;
            }
            set
            {
                minThreads = value;
            }
        }

        /// <summary>
        /// The maximum number of threads to use to process messages from the body store.
        /// </summary>
        /// <value></value>
        public int MaxThreads
        {
            get
            {
                return maxThreads;
            }
            set
            {
                maxThreads = value;
            }
        }

        /// <summary>
        /// The number of available threads.
        /// </summary>
        /// <value></value>
        public int RunningThreads
        {
            get
            {
                return 0;//threadPool.ThreadCount;
            }
        }

        /// <summary>
        /// Indicates whether this instance of the RequestManager is running.
        /// </summary>
        /// <remarks>When an instance of the RequestManager is running it is polling a specific body channel for incomming messages.  When a body arrives (request) it is procesed via ProcessRequest()</remarks>
        public bool IsRunning
        {
            get
            {
                return isRunning;
            }
        }

        /// <summary>
        /// Gets the request processors.
        /// </summary>
        /// <value>The request processors.</value>
        public System.Collections.Generic.List<RequestProcessor> RequestProcessors
        {
            get
            {
                return requestProcessors;
            }
        }

        /// <summary>
        /// Starts this instance of the RequestManager.
        /// </summary>
        /// <remarks>If the RequestManager is not already running it is started and the ProcessMessages() method is called to receive messages from the underlying body channel.</remarks>
        internal virtual void Start()
        {
            // Make sure we're not already running and we're enabled
            if (!isRunning && isEnabled)
            {
                try
                {
                    // Configure the thread pool
                    // How long we wait inbetween processing requests
                    threadPool.ServerPauseSeconds = workPauseSeconds;
                    // How long do we wait before shutting down the processors
                    threadPool.ShutdownPauseSeconds = shutdownPauseSeconds;
                    // Setup the thread pool error handler
                    threadPool.ThreadError += new ThreadErrorHandlerDelegate(ThreadPoolExceptionHandler);
                    // Start the thread pool
                    threadPool.StartThreadPool(minThreads, maxThreads);
                    // Reset the error count
                    errorCount = 0;

                    // Place the request processors into the pool
                    foreach (RequestProcessor requestProcessor in requestProcessors)
                    {
                        // Add the process request method into the queue
                        threadPool.InsertWorkItem(new ThreadPoolWorkItem(requestProcessor.RequestManager.Description, requestProcessor.Start, new object[] { requestProcessor }, false));
                    }

                    // We're running
                    isRunning = true;

                    // Log that we've started ok
                    LogEntry logEntry = new LogEntry(
                        "The Request Manager \"" + description + "\" has started.",
                        "Request Management",
                        0,
                        0,
                        TraceEventType.Start,
                        "Request Management",
                        null);

                    // Write to the log
                    Logger.Write(logEntry);
                }
                catch (Exception ex)
                {
                    // Log that we failed to start
                    if (ExceptionPolicy.HandleException(ex, "Request Management"))
                    {
                        // Rethrow the exception
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Threads the pool exception handler.
        /// </summary>
        /// <param name="workItem">The work item.</param>
        /// <param name="lastError">The last error.</param>
        public void ThreadPoolExceptionHandler(ThreadPoolWorkItem workItem, Exception lastError)
        {
            // One of the request processors has failed,
            // Exception handling is configured to "NotifyRethrow".
            // This is being called from a thread in the thread pool,
            // we must synchronise access to the controller

            try
            {
                // Enter the controller
                Monitor.Enter(this.requestController);

                // Get the request processor that failed
                RequestProcessor requestProcessor = (RequestProcessor)workItem.objInput[0];

                // Make sure that we've stopped the request processor that failed
                requestProcessor.Stop();

                // Increment the error count
                Interlocked.Increment(ref errorCount);

                // See if we've exceeded the error count for this request manager
                if (errorCount <= maxErrors)
                {
                    // We can re-start, add the process request method back into the queue
                    threadPool.InsertWorkItem(new ThreadPoolWorkItem(requestProcessor.RequestManager.Description, requestProcessor.Start, new object[] { requestProcessor }, false));

                    // Log that we had to restart the processor
                    LogEntry logEntry = new LogEntry(
                        "A Request Processor for the Request Manager \"" + description + "\"was restarted due to the following error; " + lastError.Message,
                        "Request Management",
                        0,
                        0,
                        TraceEventType.Warning,
                        "Request Management",
                        null);

                    // Write to the log
                    Logger.Write(logEntry);
                }
                else if (requestController.IsRunning)
                {
                    // Store the error in the controller
                    requestController.lastError = new Exception(
                                "The Request Controller was stopped, the maximum number of errors (" + maxErrors.ToString() + ") was exceeded.",
                                lastError);

                    // Stop the controller, stops all the request managers and all the 
                    // request processors. We will continue to get exceptions until 
                    // this method completes, especially if the request manager is 
                    // configured for multiple threads
                    requestController.Stop();

                    // Log that we had to stop the controller
                    LogEntry logEntry = new LogEntry(
                        "The Request Controller was stopped, the maximum number of errors (" + maxErrors.ToString() + ") for Request Manager \"" + description + "\" was exceeded." + Environment.NewLine + Environment.NewLine +
                        "If you want the Request Manager to recover from errors and continue running, configure the RequestProcessor.Exception PostHandlingAction to None in your configuration file.",
                        "Request Management",
                        0,
                        0,
                        TraceEventType.Warning,
                        "Request Management",
                        null);

                    // Write to the log
                    Logger.Write(logEntry);
                }
            }
            finally
            {
                // Leave the controller
                Monitor.Exit(requestController);
            }
        }

        /// <summary>
        /// Stops this instance of the RequestManager if it has not already been stopped.
        /// </summary>
        internal virtual void Stop()
        {
            // See if we're running
            if (isRunning)
            {
                try
                {
                    // Stop the request processors
                    foreach (RequestProcessor requestProcessor in requestProcessors)
                    {
                        // Stop the processor
                        requestProcessor.Stop();
                        
                        // Wait before we attempt to stop the next processor
                        Thread.Sleep((int)(1000 * ShutdownPauseSeconds));
                    }

                    // Stop the thread pool
                    threadPool.StopThreadPool();

                    // Log that we've stopped
                    LogEntry logEntry = new LogEntry(
                        "The Request Manager \"" + description + "\" has stopped.",
                        "Request Management",
                        0,
                        0,
                        TraceEventType.Stop,
                        "Request Management",
                        null);

                    // Write to the log
                    Logger.Write(logEntry);

                }
                finally
                {
                    // Flag that we've stopped
                    isRunning = false;
                }
            }
        }

        /// <summary>
        /// Loads the configuration.
        /// </summary>
        /// <param name="xmlNodeConfig">The XML node config.</param>
        internal virtual void LoadConfiguration(XmlNode xmlNodeConfig)
        {
            // Load request manager general settings
            isEnabled = Convert.ToBoolean(xmlNodeConfig.Attributes["Enabled"].Value);
            description = xmlNodeConfig.Attributes["Description"].Value;
            processorType = xmlNodeConfig.Attributes["ProcessorType"].Value;
            minThreads = Math.Max(Convert.ToInt32(xmlNodeConfig.Attributes["MinThreads"].Value), 1);
            maxThreads = Math.Max(Convert.ToInt32(xmlNodeConfig.Attributes["MaxThreads"].Value), 1);
            workPauseSeconds = Math.Min(Convert.ToDecimal(xmlNodeConfig.Attributes["WorkPauseSeconds"].Value), 10);
            shutdownPauseSeconds = Math.Min(Convert.ToDecimal(xmlNodeConfig.Attributes["ShutdownPauseSeconds"].Value), 10);
            consumeUnprocessed = Convert.ToBoolean(xmlNodeConfig.Attributes["ConsumeUnprocessed"].Value);
            maxErrors = Math.Max(Convert.ToInt32(xmlNodeConfig.Attributes["MaxErrors"].Value), 0);

            // See if we're tracing
            if (this.RequestController.Trace)
            {
                // String builder message
                StringBuilder logMessage = new StringBuilder();
                logMessage.AppendFormat("Loaded Request Manager <b>\"{0}\"</b> configuration.", description);
                logMessage.AppendFormat("<br>Type: {0}", processorType);
                logMessage.AppendFormat("<br>Enabled: {0}", isEnabled);
                logMessage.AppendFormat("<br>Description: {0}", description);
                logMessage.AppendFormat("<br>Min Threads: {0}", minThreads);
                logMessage.AppendFormat("<br>Max Threads: {0}", maxThreads);
                logMessage.AppendFormat("<br>Work Pause Secs: {0}", workPauseSeconds);
                logMessage.AppendFormat("<br>Shutdown Pause Secs: {0}", shutdownPauseSeconds);
                logMessage.AppendFormat("<br>Consume Unprocessed: {0}", consumeUnprocessed);
                logMessage.AppendFormat("<br>Max Errors: {0}", maxErrors);

                // Log debug information (using HTML file format) 
                Logger.Write(new LogEntry(
                    logMessage.ToString(),
                    "Request Management Trace",
                    0,
                    0,
                    TraceEventType.Information,
                    "Request Management Trace",
                    null));
            }

            // If we're enabled load remaining settings
            if (IsEnabled)
            {

                // Load request manager custom settings
                RequestController.LoadCustomSettings(xmlNodeConfig.SelectSingleNode("CustomSettings"), customSettings);

                // The constructor for request processors
                ConstructorInfo requestProcessorConstructor = null;
                // Use reflection to store the constructor of the class that implements DataProvider
                Type t = Type.GetType(processorType, true);
                // Get public instance constructor that takes a RequestManager
                requestProcessorConstructor = t.GetConstructor(new Type[] { typeof(RequestManager) });

                // Create the request processors, one for each thread
                for (int i = 0; i < maxThreads; i++)
                {
                    // Create the request processor and pass this request manager
                    RequestProcessor reqProcessor = (RequestProcessor)requestProcessorConstructor.Invoke(new Object[] { this });

                    // See if we're tracing
                    if (this.RequestController.Trace)
                    {
                        // Log debug information (using HTML file format) 
                        Logger.Write(new LogEntry(
                            string.Format("Loading Subscribers for Request Processor <b>#{0}</b> for Request Manager <b>\"{1}\"</b>", i + 1, description),
                            "Request Management Trace",
                            0,
                            0,
                            TraceEventType.Information,
                            "Request Management Trace",
                            null));
                    }

                    // Get the request processor to load its configuration
                    reqProcessor.LoadConfiguration(xmlNodeConfig.SelectSingleNode("Subscribers"));
                    // Add the request processor to our list of processors
                    requestProcessors.Add(reqProcessor);
                }
            }
        }

        private bool isEnabled;

        /// <summary>
        /// Gets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsEnabled
        {
            get { return isEnabled; }
        }

        private bool consumeUnprocessed;

        /// <summary>
        /// Gets or sets a value indicating whether [consume unprocessed].
        /// </summary>
        /// <value><c>true</c> if [consume unprocessed]; otherwise, <c>false</c>.</value>
        public bool ConsumeUnprocessed
        {
            get { return consumeUnprocessed; }
            set { consumeUnprocessed = value; }
        }

        // Max number of errors alowed before we stop
        private int maxErrors;

        /// <summary>
        /// Gets the max errors.
        /// </summary>
        /// <value>The max errors.</value>
        public int MaxErrors
        {
            get { return maxErrors; }
        }

        // Number of errors for this request manager instance
        private int errorCount = 0;
    }
}
