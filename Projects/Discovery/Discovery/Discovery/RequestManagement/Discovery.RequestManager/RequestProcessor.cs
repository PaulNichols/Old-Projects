/*************************************************************************************************
 ** FILE:	RequestProcessor.cs
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
using System.Security.Principal;
using System.Text;
using System.Xml;
using System.Reflection;
using System.Threading;
using System.Transactions;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Discovery.RequestManagerClient;

namespace Discovery.RequestManagement
{

    /// <summary>
    /// An abstract class to handle request processor
    /// </summary>
    public abstract class RequestProcessor
    {
        // Dictionary of custom objects for request subscribers to use
        private Dictionary<String, Object> requestDictionary;

        /// <summary>
        /// Gets the request dictionary.
        /// </summary>
        /// <value>The request dictionary.</value>
        public Dictionary<String, Object> RequestDictionary
        {
            get { return requestDictionary; }
        }

        // The request body
        private RequestMessage requestMessage = null;


        // The request manager managing us
        private RequestManager requestManager;

        /// <summary>
        /// Gets the request manager.
        /// </summary>
        /// <value>The request manager.</value>
        public RequestManager RequestManager
        {
            get { return requestManager; }
        }

        /// <summary>
        /// The request subscribers for this request processor
        /// </summary>
        protected List<RequestSubscriber> requestSubscribers;

        // Are we running
        private bool isRunning;

        /// <summary>
        /// Gets a value indicating whether this instance is running.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is running; otherwise, <c>false</c>.
        /// </value>
        public bool IsRunning
        {
            get { return isRunning; }
        }

        // We don't use the default constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="T:RequestProcessor"/> class.
        /// </summary>
        private RequestProcessor()
        {
            // We don't have a default constructor
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:RequestProcessor"/> class.
        /// </summary>
        /// <param name="requestManager">The request manager.</param>
        internal RequestProcessor(RequestManager requestManager)
        {
            // Create a list of request subscribers
            requestSubscribers = new List<RequestSubscriber>();

            // Create the request dictionary
            requestDictionary = new Dictionary<string, object>();

            // Store our request manager
            this.requestManager = requestManager;
        }

        /// <summary>
        /// Gets the custom settings.
        /// </summary>
        /// <value>The custom settings.</value>
        public Dictionary<String, String> CustomSettings
        {
            get
            {
                // We simply return the custom settings from the request manager
                return requestManager.CustomSettings;
            }
        }

        public RequestMessage RequestMessage
        {
            get { return requestMessage; }
        }

        /// <summary>
        /// Loads the configuration.
        /// </summary>
        /// <param name="xmlNodeConfig">The XML node config.</param>
        internal virtual void LoadConfiguration(XmlNode xmlNodeConfig)
        {
            // The constructor for each of the subscribers
            ConstructorInfo requestSubscriberConstructor = null;
            // Use reflection to store the constructor of the class
            Type t = null;

            // Create the request subscribers
            foreach (XmlNode xmlNodeSubscriber in xmlNodeConfig.SelectNodes("Subscriber"))
            {
                // Get the type
                t = Type.GetType(xmlNodeSubscriber.Attributes["Type"].Value, true);
                // Get public instance constructor that takes a RequestProcessor
                requestSubscriberConstructor = t.GetConstructor(new Type[] { typeof(RequestProcessor) });
                // Create an instance of the specific subscriber and pass this processor
                RequestSubscriber reqSubscriber = (RequestSubscriber)requestSubscriberConstructor.Invoke(new Object[] { this });
                // Get the request subscriber to load its configuration
                reqSubscriber.LoadConfiguration(xmlNodeSubscriber);
                // Add the request subscriber to our subscriber list
                requestSubscribers.Add(reqSubscriber);
            }
        }


        /// <summary>
        /// Starts the specified obj args.
        /// </summary>
        /// <param name="objArgs">The obj args.</param>
        /// <returns></returns>
        internal object Start(object[] objArgs)
        {
            // This method is called on a pooled thread

            // Make sure we're not already running
            if (!isRunning)
            {
                // We're running
                isRunning = true;

                // Is the current subscriber subscribed
                bool isSubscribed = false;

                // Has the subscriber been filtered
                bool isFiltered = false;

                // Number of times the request has been processed (Processed,Failed or Consumed)
                int processedCount = 0;

                // Log that we've started ok
                LogEntry logEntry = new LogEntry(
                    "The Request Processor for \"" + requestManager.Description + "\" has started.",
                    "Request Management",
                    0,
                    0,
                    TraceEventType.Start,
                    "Request Management",
                    null);

                // Write to the log
                Logger.Write(logEntry);

                // ***LAS*** Specify the transaction options
                // If we're in any doubt about reliability then we should use ReadCommitted
                // However, we must run single threaded in order to mitigate against database locks
                TransactionOptions transOptions = new TransactionOptions();
                transOptions.IsolationLevel = IsolationLevel.RepeatableRead;
#if DEBUG
                transOptions.Timeout = new TimeSpan(0, 0, 0, 300);
#else
                transOptions.Timeout = new TimeSpan(0, 0, 0, 10);
#endif

                // Date time we started processing the whole request
                DateTime requestStart = DateTime.Now;

                // Date time we finished processing the whole request
                DateTime requestFinish = DateTime.Now;

                // Date time we started processing request via subscriber
                DateTime subscriberStart = DateTime.Now;

                // Date time we finished processing request via subscriber
                DateTime subscriberFinish = DateTime.Now;

                // Keep checking for requests until we're told to stop
                while (isRunning)
                {
                    try
                    {
                        // Clear the request dictionary
                        requestDictionary.Clear();

                        // Reset the processed count
                        processedCount = 0;

                        // Reset the request message
                        requestMessage = null;
                        
                        // Create a transaction scope for this request
                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, transOptions))
                        {
                            // Get the next request, we enlist in the above transaction
                            requestMessage = GetNextRequest();

                            // See if we have a request and it has a body
                            if (null != requestMessage && !string.IsNullOrEmpty(requestMessage.Body))
                            {
                                // See if we're tracing
                                if (this.RequestManager.RequestController.Trace)
                                {
                                    // Date time we started processing the request
                                    requestStart = DateTime.Now;

                                    // String builder message
                                    StringBuilder logMessage = new StringBuilder();
                                    logMessage.AppendFormat("Request Message Received on Request Processor for Request Manager <b>\"{0}\"</b>.", requestManager.Description);
                                    logMessage.AppendFormat("<br>Type: {0}", requestMessage.Type);
                                    logMessage.AppendFormat("<br>Name: {0}", requestMessage.Name);
                                    logMessage.AppendFormat("<br>Sequence: {0}", requestMessage.Sequence);
                                    logMessage.AppendFormat("<br>Source Sys: {0}", requestMessage.SourceSystem);
                                    logMessage.AppendFormat("<br>Dest Sys: {0}", requestMessage.DestinationSystem);
                                    logMessage.AppendFormat("<br>Body: <font color='green'>{0}</font>", requestMessage.Body);

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

                                // We have a request, process it via the subscribers
                                foreach (RequestSubscriber requestSubscriber in requestSubscribers)
                                {
                                    // Seed status to unprocessed
                                    requestSubscriber.Status = SubscriberStatusEnum.UnProcessed;

                                    // Clear the last exception
                                    requestSubscriber.LastError = null;

                                    // See if the subscriber is enabled and we still have a transaction
                                    if (requestSubscriber.IsEnabled &&
                                        System.Transactions.Transaction.Current.TransactionInformation.Status == TransactionStatus.Active)
                                    {
                                        // See if we're subscribed, compare using appropriate operator
                                        if ("and" == requestSubscriber.RegexOperator.ToLower())
                                        {
                                            // See if we're subscribed
                                            isSubscribed = requestSubscriber.RegexLabel.IsMatch(requestMessage.Label) &&
                                                        requestSubscriber.RegexBody.IsMatch(requestMessage.Body);
                                        }
                                        else
                                        {
                                            // See if we're subscribed
                                            isSubscribed = requestSubscriber.RegexLabel.IsMatch(requestMessage.Label) ||
                                                        requestSubscriber.RegexBody.IsMatch(requestMessage.Body);
                                        }

                                        // See if this subscriber is interested in the request and it's enabled
                                        if (isSubscribed)
                                        {
                                            // We don't filter out the subscriber by default
                                            isFiltered = false;

                                            // See if there's any filters
                                            if (requestSubscriber.RequestFilters.Count > 0)
                                            {
                                                foreach (RequestFilter requestFilter in requestSubscriber.RequestFilters)
                                                {
                                                    // Make sure that the filter is enabled
                                                    if (requestFilter.IsEnabled)
                                                    {
                                                        // See if we're subscribed, compare using appropriate operator
                                                        if ("and" == requestFilter.RegexOperator.ToLower())
                                                        {
                                                            // See if we're filtered
                                                            isFiltered = requestFilter.RegexLabel.IsMatch(requestMessage.Label) &&
                                                                        requestFilter.RegexBody.IsMatch(requestMessage.Body);
                                                        }
                                                        else
                                                        {
                                                            // See if we're filtered
                                                            isFiltered = requestFilter.RegexLabel.IsMatch(requestMessage.Label) ||
                                                                        requestFilter.RegexBody.IsMatch(requestMessage.Body);
                                                        }
                                                    }

                                                    // See if we're tracing
                                                    if (this.RequestManager.RequestController.Trace)
                                                    {
                                                        // See if we've been filtered
                                                        if (isFiltered)
                                                        {
                                                            // Log debug information (using HTML file format) 
                                                            Logger.Write(new LogEntry(
                                                                string.Format("Request Subscriber <b>\"{0}\"</b> for Request Manager <b>\"{4}\"</b> removed by filter (Label=<b>\"{1}\"</b> {2} Body=<b>\"{3}\"</b>).", requestSubscriber.Type, requestFilter.RegexLabel.ToString(), requestFilter.RegexOperator, requestFilter.RegexBody.ToString(), requestManager.Description),
                                                                "Request Management Trace",
                                                                0,
                                                                0,
                                                                TraceEventType.Information,
                                                                "Request Management Trace",
                                                                null));
                                                        }
                                                    }

                                                    // If we've been filtered out don't check any more
                                                    if (isFiltered) break;
                                                }
                                            }
                                            
                                            // Move on to the next subscriber
                                            if (isFiltered) continue;

                                            // Get the subscriber to process the request if it's not been filtered out
                                            
                                            // See if we're tracing
                                            if (this.RequestManager.RequestController.Trace)
                                            {
                                                // Date time we started processing request via subscriber
                                                subscriberStart = DateTime.Now;

                                                // Log debug information (using HTML file format) 
                                                Logger.Write(new LogEntry(
                                                    string.Format("Request Subscriber <b>\"{0}\"</b> for Request Manager <b>\"{1}\"</b> subscribed.", requestSubscriber.Type, requestManager.Description),
                                                    "Request Management Trace",
                                                    0,
                                                    0,
                                                    TraceEventType.Information,
                                                    "Request Management Trace",
                                                    null));
                                            }

                                            // *******************************************************************
                                            // ** Process the request
                                            // *******************************************************************
                                            requestSubscriber.ProcessRequest(requestMessage);

                                            // If the request was handled increment the processed count (failed or)
                                            if (requestSubscriber.Status != SubscriberStatusEnum.UnProcessed)
                                            {
                                                // Request has been processed
                                                processedCount++;
                                            }

                                            // See if we're tracing
                                            if (this.RequestManager.RequestController.Trace)
                                            {
                                                // Date time we finished processing request via subscriber
                                                subscriberFinish = DateTime.Now;

                                                // Log debug information (using HTML file format) 
                                                Logger.Write(new LogEntry(
                                                    string.Format("Request Subscriber <b>\"{0}\"</b> for Request Manager <b>\"{2}\"</b> returned <font color='blue'><b>{3}</b></font> in <b>{1}</b> milliseconds.", requestSubscriber.Type, subscriberFinish.Subtract(subscriberStart).Milliseconds, requestManager.Description, requestSubscriber.Status.ToString()),
                                                    "Request Management Trace",
                                                    0,
                                                    0,
                                                    TraceEventType.Information,
                                                    "Request Management Trace",
                                                    null));
                                            }

                                            // *******************************************************************
                                            // ** Check the subscriber return status
                                            // *******************************************************************

                                            // All of these statuses will consume the message, if we wish to put the message back
                                            // into the message queue (SQL, MSMQ, etc) then the subscriber should throw an exception

                                            // See if we failed
                                            if (requestSubscriber.Status == SubscriberStatusEnum.Failed)
                                            {
                                                // Log debug information (using HTML file format) 
                                                Logger.Write(new LogEntry(
                                                    string.Format("RequestSubscriber \"{0}\" failed: {1}.  The request message label was: {2}.", requestSubscriber.Type, ((requestSubscriber.LastError != null) ? requestSubscriber.LastError.Message : "LastError was not set in subscriber."), requestMessage.Label),
                                                    "Request Management",
                                                    0,
                                                    0,
                                                    TraceEventType.Error,
                                                    "Request Management",
                                                    null));

                                                // See if we stop on fail
                                                if (requestSubscriber.StopOnFail)
                                                {
                                                    // We need to stop, don't call any more subscribers
                                                    break;
                                                }
                                            }
                                            else if (requestSubscriber.Status == SubscriberStatusEnum.Consumed)
                                            {
                                                // See if we stop on consumed
                                                if (requestSubscriber.StopOnConsume)
                                                {
                                                    // We need to stop, don't call any more subscribers
                                                    break;
                                                }
                                            }
                                            else if (requestSubscriber.Status == SubscriberStatusEnum.Processed)
                                            {
                                                // See if we stop on processed
                                                if (requestSubscriber.StopOnProcessed)
                                                {
                                                    // We need to stop, don't call any more subscribers
                                                    break;
                                                }
                                            }

                                        } // Subscribed
                                    } // Subscriber enabled and we have an active transaction
                                } // For each subscriber to this request message

                                // See if we're tracing
                                if (this.RequestManager.RequestController.Trace)
                                {
                                    // Date time we finished processing request via subscriber
                                    requestFinish = DateTime.Now;

                                    // Log debug information (using HTML file format) 
                                    Logger.Write(new LogEntry(
                                        string.Format("Request Message <b>\"{0}\"</b> for Request Manager <b>\"{2}\"</b> <font color='blue'><b>processed</b></font> by {3} subscribers in <b>{1}</b> milliseconds.", requestMessage.Type, requestFinish.Subtract(requestStart).Milliseconds, requestManager.Description, processedCount),
                                        "Request Management Trace",
                                        0,
                                        0,
                                        TraceEventType.Information,
                                        "Request Management Trace",
                                        null));
                                }

                                // See if the request was processed
                                if (processedCount == 0)
                                {
                                    // If we don't consume unprocessed messages, throw an UnprocessedRequestException
                                    if (!this.requestManager.ConsumeUnprocessed)
                                    {
                                        // We weren't interested in the message, put it back on the queue
                                        throw new UnprocessedRequestException("Request not processed.");
                                    }
                                }
                            }

                            // ***************************************************
                            // ** COMPLETE THE TRANSACTION
                            // ***************************************************
                            scope.Complete();

                        } // Using scope

                        // ***************************************************
                        // ** Transaction has been commited
                        // ***************************************************

                        // See if we had a message but the body was empty
                        if (null != requestMessage && string.IsNullOrEmpty(requestMessage.Body))
                        {
                            // We don't sleep, but should consume the empty message

                            // See if we're tracing
                            if (this.RequestManager.RequestController.Trace)
                            {

                                // Log debug information (using HTML file format) 
                                Logger.Write(new LogEntry(
                                    string.Format("Request Message <b>\"{0}\"</b> for Request Manager <b>\"{1}\"<\b> had no message body, message consumed.", requestMessage.Type, requestManager.Description),
                                    "Request Management Trace",
                                    0,
                                    0,
                                    TraceEventType.Information,
                                    "Request Management Trace",
                                    null));
                            }
                        }
                        else
                        {
                            // No request, sleep so that we don't thrash the CPU
                            Thread.Sleep((int)(1000 * this.requestManager.WorkPauseSeconds));
                        }

                        // See if we're tracing
                        if (this.RequestManager.RequestController.Trace)
                        {
                            // Log debug information (using HTML file format) 
                            if (null != requestMessage)
                            {
                                Logger.Write(new LogEntry(
                                    string.Format("Request Message <b>\"{0}\"</b> was consumed from queue for Request Manager <b>\"{1}\"</b>.", requestMessage.Type, requestManager.Description),
                                    "Request Management Trace",
                                    0,
                                    0,
                                    TraceEventType.Information,
                                    "Request Management Trace",
                                    null));
                            }
                        }
                    }
                    catch (UnprocessedRequestException)
                    {
                        // No subscribers for message and we're not configured to consume it
                        // Message back on queue

                        // See if we're tracing
                        if (this.RequestManager.RequestController.Trace)
                        {
                            // Log debug information (using HTML file format) 
                            Logger.Write(new LogEntry(
                                string.Format("Request Message <b>UNPROCESSED</b> for Request Manager <b>\"{0}\"</b>, it was put back on the queue.", requestManager.Description),
                                "Request Management Trace",
                                0,
                                0,
                                TraceEventType.Information,
                                "Request Management Trace",
                                null));
                        }
                    }
                    catch (Exception ex)
                    {
                        // See if we're tracing
                        if (this.RequestManager.RequestController.Trace)
                        {
                            // Log debug information (using HTML file format) 
                            Logger.Write(new LogEntry(
                                string.Format("Request Processor <b>ERROR</b> for Request Manager <b>\"{0}\"</b>;<br>Error: \"<font color='red'><b>{1}</b></font>\". Message was put back on the queue.", requestManager.Description, ex.Message),
                                "Request Management Trace",
                                0,
                                0,
                                TraceEventType.Information,
                                "Request Management Trace",
                                null));
                        }

                        // There was a problem processing the request, we need to log this
                        // Message back on queue
                        ExceptionPolicy.HandleException(ex, "Request Management");
                    }
                } // While running
            }
            // Done
            return null;
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        internal void Stop()
        {
            // If we're running, stop each of the controlled RequestManagers
            if (isRunning)
            {
                try
                {
                    // Log that we've started ok
                    LogEntry logEntry = new LogEntry(
                        "The Request Processor for Request Manager \"" + requestManager.Description + "\" has stopped.",
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
                    // We're not running
                    isRunning = false;
                }
            }
        }

        /// <summary>
        /// Gets the next request.
        /// </summary>
        /// <returns></returns>
        public abstract RequestMessage GetNextRequest();

    }

    /// <summary>
    /// An enumeration to include subscriber status
    /// </summary>
    public enum SubscriberStatusEnum
    {
        /// <summary>
        /// Unprocessed of SubscriberStatusEnum
        /// </summary>
        UnProcessed,
        /// <summary>
        /// Processed of SubscriberStatusEnum
        /// </summary>
        Processed,
        /// <summary>
        /// Consumed of SubscriberStatusEnum
        /// </summary>
        Consumed,
        /// <summary>
        /// Failed of SubscriberStatusEnum
        /// </summary>
        Failed
    }
}
