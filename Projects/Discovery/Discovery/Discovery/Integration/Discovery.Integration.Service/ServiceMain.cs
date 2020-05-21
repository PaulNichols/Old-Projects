using System;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Threading;
using Discovery.Integration;

namespace Discovery.Integration.Service
{
    /// <summary>
    /// A service status structure such as service type, current state, control accepted, win32 exit code,
    /// service specific exit code, check point and wait hint
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SERVICE_STATUS
    {
        /// <summary>
        /// service type of SERVICE_STATUS structure
        /// </summary>
        public int serviceType;
        /// <summary>
        /// current state of SERVICE_STATUS structure
        /// </summary>
        public int currentState;
        /// <summary>
        /// controls accepted of SERVICE_STATUS structure
        /// </summary>
        public int controlsAccepted;
        /// <summary>
        /// win32 exit code of SERVICE_STATUS structure
        /// </summary>
        public int win32ExitCode;
        /// <summary>
        /// service specific exit code of SERVICE_STATUS structure
        /// </summary>
        public int serviceSpecificExitCode;
        /// <summary>
        /// check point of SERVICE_STATUS structure
        /// </summary>
        public int checkPoint;
        /// <summary>
        /// wait hint of SERVICE_STATUS structure
        /// </summary>
        public int waitHint;
    }

    /// <summary>
    /// A state enumeration
    /// </summary>
    public enum State
    {
        /// <summary>
        /// service stopped of state enumeration
        /// </summary>
        SERVICE_STOPPED = 0x00000001,
        /// <summary>
        /// service start pending of state enumeration
        /// </summary>
        SERVICE_START_PENDING = 0x00000002,
        /// <summary>
        /// service stop pending of state enumeration
        /// </summary>
        SERVICE_STOP_PENDING = 0x00000003,
        /// <summary>
        /// service running of state enumeration
        /// </summary>
        SERVICE_RUNNING = 0x00000004,
        /// <summary>
        /// service continue pending of state enumeration
        /// </summary>
        SERVICE_CONTINUE_PENDING = 0x00000005,
        /// <summary>
        /// service pause pending of state enumeration
        /// </summary>
        SERVICE_PAUSE_PENDING = 0x00000006,
        /// <summary>
        /// service paused of state enumeration
        /// </summary>
        SERVICE_PAUSED = 0x00000007,
    }

    /// <summary>
    /// A main service class which is in Discovery.Integration.Service
    /// </summary>
    public partial class ServiceMain : ServiceBase
    {



        /// <summary>
        /// Sets the service status.
        /// </summary>
        /// <param name="hServiceStatus">The h service status.</param>
        /// <param name="lpServiceStatus">The lp service status.</param>
        /// <returns></returns>
        [DllImport("ADVAPI32.DLL", EntryPoint = "SetServiceStatus")]
        public static extern bool SetServiceStatus(
            IntPtr hServiceStatus,
            ref SERVICE_STATUS lpServiceStatus);

        // The status of this service
        private SERVICE_STATUS serviceStatus;


        /// <summary>
        /// Initializes a new instance of the <see cref="T:ServiceMain"/> class.
        /// </summary>
        public ServiceMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Start command is sent to the service by the Service Control Manager (SCM) or when the operating system starts (for a service that starts automatically). Specifies actions to take when the service starts.
        /// </summary>
        /// <param name="args">Data passed by the start command.</param>
        protected override void OnStart(string[] args)
        {
            // Start the service on a seperate thread so that we don't time-out the SCM
            Thread startThread = new Thread(new ThreadStart(StartIntegration));
            startThread.IsBackground = true;
            startThread.Start();
        }

        /// <summary>
        /// Starts the integration.
        /// </summary>
        private void StartIntegration()
        {
            // Get a handle to this service instance
            IntPtr handle = ServiceHandle;

            try
            {
                // Update the service status to pending
                serviceStatus.currentState = (int)State.SERVICE_START_PENDING;
                SetServiceStatus(handle, ref serviceStatus);

                //start the integration timer going
                IntegrationController.Start();

                // Update the service status to pending
                serviceStatus.currentState = (int)State.SERVICE_RUNNING;
                SetServiceStatus(handle, ref serviceStatus);
            }
            catch (Exception ex)
            {
                // We've failed to start the service, stop it
                OnStop();
            }
        }



        /// <summary>
        /// When implemented in a derived class, executes when a Stop command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service stops running.
        /// </summary>
        protected override void OnStop()
        {
            // Get a handle to this service instance
            IntPtr handle = ServiceHandle;

            try
            {
                // Update the service status to stopped
                serviceStatus.currentState = (int)State.SERVICE_STOP_PENDING;
                SetServiceStatus(handle, ref serviceStatus);

                // If we have an exception, call stop as may not have been called via SCM
                //if (null != lastError)
                //{
                //    // We need to log that we've stopped due to error
                //    LogEntry logEntry = new LogEntry(
                //        "The Integration service stopped due to the following error;" + Environment.NewLine +
                //        lastError.Message,
                //        "RequestManager.ServiceStopped",
                //        0,
                //        0,
                //        TraceEventType.Start,
                //        null,
                //        null);

                //    // Write to the log
                //    Logger.Write(logEntry);

                //    // Set the exit code
                //    ExitCode = 1;

                //    // We could simply stop the service here
                //    Stop();
                //}
                //else
                //{
                // Update the service status to pending

                IntegrationController.Stop();
                
                serviceStatus.currentState = (int)State.SERVICE_STOPPED;
                SetServiceStatus(handle, ref serviceStatus);
                //}
            }
            catch
            {
                // Update the service status to stopped
                serviceStatus.currentState = (int)State.SERVICE_STOPPED;
                SetServiceStatus(handle, ref serviceStatus);
            }
            finally
            {
            }
        }
    }
}