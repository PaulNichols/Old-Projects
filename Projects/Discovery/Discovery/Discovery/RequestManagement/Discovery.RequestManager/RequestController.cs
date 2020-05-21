/*************************************************************************************************
 ** FILE:	RequestController.cs
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
using System.Diagnostics;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using System.Text;
using System.Threading;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Discovery.RequestManagement
{
    /// <summary>
    /// An enumeration to include request controller status
    /// </summary>
    public enum RequestControllerStatus
    {
        /// <summary>
        /// Started status of RequestControllerStatus enumeration
        /// </summary>
        Started,
        /// <summary>
        /// Stopped status of RequestControllerStatus enumeration
        /// </summary>
        Stopped
    }

    /// <summary>
    /// A delegate for request controller status
    /// </summary>
    /// <param name="status"></param>
    /// <param name="ex"></param>
    public delegate void RequestControllerStatusDelegate(RequestControllerStatus status, Exception ex);

    /// <summary>
    /// A class for the request controller
    /// </summary>
    public class RequestController
    {
        // Should we write trace information
        private bool trace = false;

        public bool Trace
        {
            get { return trace; }
            set { trace = value; }
        }

        // Is this request controller running
        private bool isRunning;

        // Our list of request managers controlled by this request controller
        private List<RequestManager> requestManagers;

        /// <summary>
        /// Activate the request controller status delegate with status changed
        /// </summary>
        public event RequestControllerStatusDelegate StatusChanged;

        // Hash of custom settings
        private Dictionary<String, String> customSettings;

        /// <summary>
        /// Gets the custom settings.
        /// </summary>
        /// <value>The custom settings.</value>
        public Dictionary<String, String> CustomSettings
        {
            get { return customSettings; }
        }

        /// <summary>
        /// Gets the configuration XSD.
        /// </summary>
        /// <value>The configuration XSD.</value>
        public XmlReader ConfigurationXSD
        {
            get
            {
                // Get the name of this assembly
                string AssemblyName = Assembly.GetAssembly(GetType()).GetName().Name;
                // Get the xsd from the resource
                return XmlReader.Create(Assembly.GetAssembly(GetType()).GetManifestResourceStream(AssemblyName + ".Discovery.RequestManager.xsd"));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:RequestController"/> class.
        /// </summary>
        public RequestController()
        {
            // Create the request managers list
            requestManagers = new List<RequestManager>();
            // Create the custom settings hash
            customSettings = new Dictionary<String, String>();
            // Create the validation errors collection
            validationErrors = new List<string>();
            // Seed last error
            lastError = null;
        }

        /// <summary>
        /// Indicates whether this instance of the RequestController is running.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is running; otherwise, <c>false</c>.
        /// </value>
        public bool IsRunning
        {
            get
            {
                // Are we running?
                return isRunning;
            }
        }

        /// <summary>
        /// Returns a list of request managers that are managed by this controller
        /// </summary>
        /// <value>The request managers.</value>
        public List<RequestManager> RequestManagers
        {
            get
            {
                // Request managers we control.
                return requestManagers;
            }
        }

        /// <summary>
        /// Loads the specified XML controller configuration file.
        /// </summary>
        /// <remarks>Loads the specified XML controller configuration file from disk.  If the file cannot be loaded or parsed an exception is thrown.  For each of the request managers identified in the configuration file and instance of a RequestManager is created and added to the RequestManagers collection.</remarks>
        internal void LoadConfiguration(string xmlConfig)
        {
            // Our xml config
            XmlDocument xmlDocConfig = new XmlDocument();

            // XML reader settings
            XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
            xmlReaderSettings.Schemas.Add(null, ConfigurationXSD);
            xmlReaderSettings.ValidationEventHandler += new ValidationEventHandler(readerSettings_ValidationEventHandler);
            xmlReaderSettings.ValidationType = ValidationType.Schema;
            xmlReaderSettings.ValidationFlags = xmlReaderSettings.ValidationFlags | XmlSchemaValidationFlags.ReportValidationWarnings;

            // XML reader
            XmlReader xmlReader = XmlReader.Create(xmlConfig, xmlReaderSettings);

            // Load and validate the xml configuration for this controller
            xmlDocConfig.Load(xmlReader);

            // See if we loaded the configuration
            if (validationErrors.Count > 0)
            {
                // Validation errors, throw to start
                throw new Exception("The Request Manager configuration is invalid.  " + string.Join(" ", validationErrors.ToArray()));
            }

            // Get the controller node
            XmlNode xmlNodeController = xmlDocConfig.DocumentElement;

            // See if we're tracing
            trace = Convert.ToBoolean(xmlNodeController.Attributes["Trace"].Value);

            // Load controller custom settings
            LoadCustomSettings(xmlNodeController.SelectSingleNode("CustomSettings"), customSettings);

            // Load the request managers
            foreach (XmlNode xmlNodeRequestManager in xmlNodeController.SelectSingleNode("RequestManagers").SelectNodes("RequestManager"))
            {
                // Create the request manager instance
                RequestManager reqManager = new RequestManager(this);

                // Get the request manager to load its configuration
                reqManager.LoadConfiguration(xmlNodeRequestManager);

                // Add the request manager to our list
                requestManagers.Add(reqManager);
            }
        }

        internal void readerSettings_ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            // We get here if there's an error validating the xml document using the schema
            validationErrors.Add(e.Message);
        }

        /// <summary>
        /// Starts this instance of the RequestController if it is not already running.
        /// </summary>
        /// <remarks>Starts this instance of the RequestController if it is not already running by calling Start() 
        /// on each of the RequestManagers being controlled.</remarks>
        public void Start()
        {
            // Enter the controller
            Monitor.Enter(this);

            // Make sure we're not already running
            if (!isRunning)
            {
                try
                {
                    // We're running, do this now so we can tidy up if it goes wrong
                    isRunning = true;

                    // Get the path the the executing assembly
                    string assemblyPath = Assembly.GetExecutingAssembly().Location;

                    // Load the request manager configuration (Discovery.RequestManager.Config)
                    LoadConfiguration(assemblyPath.Replace(".dll", ".xml"));

                    // See if we're tracing
                    if (this.Trace)
                    {
                        // Log debug information (using HTML file format) 
                        Logger.Write(new LogEntry(
                            "Loading configuration file <b>" + assemblyPath.Replace(".dll", ".xml") + "</b>",
                            "Request Management Trace",
                            0,
                            0,
                            TraceEventType.Information,
                            "Request Management Trace",
                            null));
                    }


                    // Start each of the request managers loaded from the config
                    foreach (RequestManager requestManager in requestManagers)
                    {
                        // Start the request manager
                        requestManager.Start();
                    }

                    // If we have a delegate for status events, call it
                    if (null != StatusChanged)
                    {
                        // Call the delegate asynchronously
                        StatusChanged.BeginInvoke(RequestControllerStatus.Started, lastError, null, null);
                    }

                    // Log that we've started ok
                    LogEntry logEntry = new LogEntry(
                        "The Request Controller has started.",
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
                    // Store the exception
                    lastError = ex;

                    // Tidy up
                    this.Stop();

                    // Log that we failed to start
                    if (ExceptionPolicy.HandleException(ex, "Request Management"))
                    {
                        // Rethrow the exception
                        throw;
                    }
                }
                finally
                {
                    // Leave the controller
                    Monitor.Exit(this);
                }
            }
        }

        /// <summary>
        /// Stops this instance of the RequestController if it has not already been stopped.
        /// </summary>
        /// <remarks>Stops this instance of the RequestController if it is not already stopped by calling Stop() on each of the RequestManagers being controlled.</remarks>
        public void Stop()
        {
            // If we're running, stop each of the controlled RequestManagers
            if (isRunning)
            {
                try
                {
                    // Stop each of the request managers
                    foreach (RequestManager requestManager in requestManagers)
                    {
                        // Stop the request manager
                        requestManager.Stop();
                    }

                    // Log that we've stopped
                    LogEntry logEntry = new LogEntry(
                        "The Request Controller has stopped.",
                        "Request Management",
                        0,
                        0,
                        TraceEventType.Stop,
                        "Request Management",
                        null);

                    // Write to the log
                    Logger.Write(logEntry);
                }
                catch (Exception ex)
                {
                    // Store the last error
                    lastError = ex;

                    // Log that we've failed
                    if (ExceptionPolicy.HandleException(ex, "Request Management"))
                    {
                        // Rethrow the exception
                        throw;
                    }
                }
                finally
                {
                    // Clear existing config
                    requestManagers.Clear();
                    customSettings.Clear();
                    validationErrors.Clear();

                    // We're not running
                    isRunning = false;

                    // If we have a delegate for status events, call it
                    if (null != StatusChanged)
                    {
                        // Call the delegate asynchronously
                        StatusChanged.BeginInvoke(RequestControllerStatus.Stopped, lastError, null, null);
                    }
                }
            }
        }

        /// <summary>
        /// Loads custom settings from an xml configuration node.
        /// </summary>
        /// <param name="xmlNodeCustomSettings">The XML node containing custom settings.</param>
        /// <param name="dictionaryCustomSettings">The dictionary to hold the custom settings.</param>
        internal static void LoadCustomSettings(
            XmlNode xmlNodeCustomSettings,
            Dictionary<String, String> dictionaryCustomSettings)
        {
            // Clear the hash
            dictionaryCustomSettings.Clear();

            // Load the xml settings into the hash
            if (null != xmlNodeCustomSettings)
            {
                foreach (XmlNode xmlNodeCustomSetting in xmlNodeCustomSettings)
                {
                    // Make sure it's not a comment
                    if (xmlNodeCustomSetting.NodeType != XmlNodeType.Comment)
                    {
                        // Add custom settings
                        dictionaryCustomSettings.Add(xmlNodeCustomSetting.Attributes["Name"].Value,
                                                     xmlNodeCustomSetting.Attributes["Value"].Value);
                    }
                }
            }

            // Load the request managers
        }

        private List<string> validationErrors;

        /// <summary>
        /// Gets the validation errors.
        /// </summary>
        /// <value>The validation errors.</value>
        public List<string> ValidationErrors
        {
            get { return validationErrors; }
        }

        internal Exception lastError;

        /// <summary>
        /// Gets the last error.
        /// </summary>
        /// <value>The last error.</value>
        public Exception LastError
        {
            get { return lastError; }
        }
    }
}