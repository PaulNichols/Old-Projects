/*************************************************************************************************
 ** FILE:	RequestSubscriber.cs
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
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Discovery.RequestManagerClient;

namespace Discovery.RequestManagement
{
    /// <summary>
    /// A class to handle exception for unprocessed requests
    /// </summary>
    public class UnprocessedRequestException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:UnprocessedRequestException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public UnprocessedRequestException(string message)
            : base(message)
        {
        }
    }

    /// <summary>
    /// An abstract class for request subscriber
    /// </summary>
    public abstract class RequestSubscriber
    {
        // List of filters for this subscriber
        private List<RequestFilter> requestFilters;

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

        // Our request processor
        private RequestProcessor requestProcessor;

        private bool isEnabled;
        private bool stopOnFail;

        /// <summary>
        /// Gets or sets a value indicating whether [stop on fail].
        /// </summary>
        /// <value><c>true</c> if [stop on fail]; otherwise, <c>false</c>.</value>
        public bool StopOnFail
        {
            get { return stopOnFail; }
            set { stopOnFail = value; }
        }
        private bool stopOnConsume;

        /// <summary>
        /// Gets or sets a value indicating whether [stop on consume].
        /// </summary>
        /// <value><c>true</c> if [stop on consume]; otherwise, <c>false</c>.</value>
        public bool StopOnConsume
        {
            get { return stopOnConsume; }
            set { stopOnConsume = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsEnabled
        {
            get { return isEnabled; }
            set { isEnabled = value; }
        }


        /// <summary>
        /// Gets the request processor.
        /// </summary>
        /// <value>The request processor.</value>
        public RequestProcessor RequestProcessor
        {
            get { return requestProcessor; }
        }

        /// <summary>
        /// Gets the request filters.
        /// </summary>
        /// <value>The request filters.</value>
        public List<RequestFilter> RequestFilters
        {
            get { return requestFilters; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:RequestSubscriber"/> class.
        /// </summary>
        /// <param name="requestProcessor">The request processor.</param>
        public RequestSubscriber(RequestProcessor requestProcessor)
        {
            // Create the custom settings list
            customSettings = new Dictionary<string, string>();
            // Create the filters list
            requestFilters = new List<RequestFilter>();
            // Store the request processor
            this.requestProcessor = requestProcessor;
            // Create the regular expression instances
            regexBody = null;
            regexLabel = null;
            regexOperator = "";
        }

        // Shouldn't be using the default constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="T:RequestSubscriber"/> class.
        /// </summary>
        private RequestSubscriber()
        {
            // We don't have a default constructor
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Loads the filters.
        /// </summary>
        /// <param name="xmlNodeFilters">The XML node filters.</param>
        public void LoadFilters(XmlNode xmlNodeFilters)
        {
            // Clear the list of filters
            requestFilters.Clear();

            // Load the xml settings into the hash
            if (null != xmlNodeFilters)
            {
                foreach (XmlNode xmlFilter in xmlNodeFilters.SelectNodes("Filter"))
                {
                    // Create an instance of the request filter class
                    RequestFilter requestFilter = new RequestFilter(this);
                    // Get the filter to load its configuration
                    requestFilter.LoadConfiguration(xmlFilter);
                    // Add filter to list of filters
                    requestFilters.Add(requestFilter);
                }
            }
        }

        /// <summary>
        /// Loads the configuration.
        /// </summary>
        /// <param name="xmlNodeConfig">The XML node config.</param>
        public virtual void LoadConfiguration(XmlNode xmlNodeConfig)
        {
            try
            {
                // Load request subscriber general settings
                isEnabled = Convert.ToBoolean(xmlNodeConfig.Attributes["Enabled"].Value);
                stopOnFail = Convert.ToBoolean(xmlNodeConfig.Attributes["StopOnFailed"].Value);
                stopOnConsume = Convert.ToBoolean(xmlNodeConfig.Attributes["StopOnConsumed"].Value);
                stopOnProcessed = Convert.ToBoolean(xmlNodeConfig.Attributes["StopOnProcessed"].Value);
                regexLabel = new Regex(xmlNodeConfig.Attributes["Label"].Value);
                regexBody = new Regex(xmlNodeConfig.Attributes["Body"].Value);
                regexOperator = xmlNodeConfig.Attributes["Operator"].Value;
                type = xmlNodeConfig.Attributes["Type"].Value;

                // See if we're tracing
                if (this.RequestProcessor.RequestManager.RequestController.Trace)
                {
                    // String builder message
                    StringBuilder logMessage = new StringBuilder();
                    logMessage.AppendFormat("Loaded Request Subscriber configuration.");
                    logMessage.AppendFormat("<br>Type: {0}", this.GetType().ToString());
                    logMessage.AppendFormat("<br>Enabled: {0}", isEnabled);
                    logMessage.AppendFormat("<br>Stop On Fail: {0}", stopOnFail);
                    logMessage.AppendFormat("<br>Stop on Consume: {0}", stopOnConsume);
                    logMessage.AppendFormat("<br>Stop On Processed: {0}", stopOnProcessed);
                    logMessage.AppendFormat("<br>Reg Ex Label: {0}", regexLabel);
                    logMessage.AppendFormat("<br>Reg Ex Body: {0}", regexBody);
                    logMessage.AppendFormat("<br>Reg Ex Operator: {0}", regexOperator);

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
                    // Load subscriber custom settings
                    RequestController.LoadCustomSettings(xmlNodeConfig.SelectSingleNode("CustomSettings"), customSettings);

                    // Load the filters for this subscriber
                    LoadFilters(xmlNodeConfig.SelectSingleNode("Filters"));
                }
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

        /// <summary>
        /// Processes the request.
        /// </summary>
        /// <param name="requestMessage">The request message.</param>
        public abstract void ProcessRequest(RequestMessage requestMessage);

        private bool stopOnProcessed;

        /// <summary>
        /// Gets or sets a value indicating whether [stop on processed].
        /// </summary>
        /// <value><c>true</c> if [stop on processed]; otherwise, <c>false</c>.</value>
        public bool StopOnProcessed
        {
            get { return stopOnProcessed; }
            set { stopOnProcessed = value; }
        }

        private Regex regexLabel;

        /// <summary>
        /// Gets the regex label.
        /// </summary>
        /// <value>The regex label.</value>
        public Regex RegexLabel
        {
            get { return regexLabel; }
        }
        private Regex regexBody;

        /// <summary>
        /// Gets the regex body.
        /// </summary>
        /// <value>The regex body.</value>
        public Regex RegexBody
        {
            get { return regexBody; }
        }
        private string regexOperator;

        /// <summary>
        /// Gets the regex operator.
        /// </summary>
        /// <value>The regex operator.</value>
        public string RegexOperator
        {
            get { return regexOperator; }
        }

        private string type;

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        public string Type
        {
            get { return type; }
        }

        private SubscriberStatusEnum status;

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public SubscriberStatusEnum Status
        {
            get { return status; }
            set { status = value; }
        }

        private Exception lastError;

        /// <summary>
        /// Gets or sets the last error.
        /// </summary>
        /// <value>The last error.</value>
        public Exception LastError
        {
            get { return lastError; }
            set { lastError = value; }
        }
    }

    /// <summary>
    /// A class for request filter
    /// </summary>
    public class RequestFilter
    {
        private bool isEnabled;
        private Regex regexLabel;

        /// <summary>
        /// Gets the regex label.
        /// </summary>
        /// <value>The regex label.</value>
        public Regex RegexLabel
        {
            get { return regexLabel; }
        }
        private Regex regexBody;

        /// <summary>
        /// Gets the regex body.
        /// </summary>
        /// <value>The regex body.</value>
        public Regex RegexBody
        {
            get { return regexBody; }
        }
        private string regexOperator;

        /// <summary>
        /// Gets the regex operator.
        /// </summary>
        /// <value>The regex operator.</value>
        public string RegexOperator
        {
            get { return regexOperator; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsEnabled
        {
            get { return isEnabled; }
            set { isEnabled = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:RequestFilter"/> class.
        /// </summary>
        private RequestFilter()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:RequestFilter"/> class.
        /// </summary>
        /// <param name="requestSubscriber">The request subscriber.</param>
        public RequestFilter(RequestSubscriber requestSubscriber)
        {
        }

        /// <summary>
        /// Shoulds the fire.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        private bool ShouldFire(ref string message)
        {
            if (!isEnabled)
            {
                return true;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Loads the configuration.
        /// </summary>
        /// <param name="xmlNodeConfig">The XML node config.</param>
        public virtual void LoadConfiguration(XmlNode xmlNodeConfig)
        {
            // Load request filter general settings
            isEnabled = Convert.ToBoolean(xmlNodeConfig.Attributes["Enabled"].Value);
            regexLabel = new Regex(xmlNodeConfig.Attributes["Label"].Value);
            regexBody = new Regex(xmlNodeConfig.Attributes["Body"].Value);
            regexOperator = xmlNodeConfig.Attributes["Operator"].Value;
        }
    }
}