using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Discovery.RequestManagement;

namespace Discovery.RequestManager.Service
{
    /// <summary>
    /// The service status structure
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SERVICE_STATUS
    {
        /// <summary>
        /// Service type of the service status structure
        /// </summary>
        public int serviceType;
        /// <summary>
        /// Current state of the service status structure
        /// </summary>
        public int currentState;
        /// <summary>
        /// Control accepted of the service status structure
        /// </summary>
        public int controlsAccepted;
        /// <summary>
        /// win32 exit code of the service status structure
        /// </summary>
        public int win32ExitCode;
        /// <summary>
        /// Service specific exit code of the service status structure
        /// </summary>
        public int serviceSpecificExitCode;
        /// <summary>
        /// Check point of the service status structure
        /// </summary>
        public int checkPoint;
        /// <summary>
        /// Wait hint of the service status structure
        /// </summary>
        public int waitHint;
    }

    /// <summary>
    /// A state enumeration
    /// </summary>
    public enum State
    {
        /// <summary>
        /// Service stopped of the state enumeration
        /// </summary>
        SERVICE_STOPPED = 0x00000001,
        /// <summary>
        /// Service start of the state enumeration
        /// </summary>
        SERVICE_START_PENDING = 0x00000002,
        /// <summary>
        /// Service stop of the state enumeration
        /// </summary>
        SERVICE_STOP_PENDING = 0x00000003,
        /// <summary>
        /// Service running of the state enumeration
        /// </summary>
        SERVICE_RUNNING = 0x00000004,
        /// <summary>
        /// Service continue of the state enumeration
        /// </summary>
        SERVICE_CONTINUE_PENDING = 0x00000005,
        /// <summary>
        /// Service pause of the state enumeration
        /// </summary>
        SERVICE_PAUSE_PENDING = 0x00000006,
        /// <summary>
        /// Service paused of the state enumeration
        /// </summary>
        SERVICE_PAUSED = 0x00000007,
    }

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
        
        // Our request controller instance
        RequestController requestController;

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
            Thread startThread = new Thread(new ThreadStart(StartRequestManager));
            startThread.IsBackground = true;
            startThread.Start();
        }

        /// <summary>
        /// Starts the request manager.
        /// </summary>
        private void StartRequestManager()
        {
            // Get a handle to this service instance
            IntPtr handle = this.ServiceHandle;

            try
            {
                // Update the service status to pending
                serviceStatus.currentState = (int)State.SERVICE_START_PENDING;
                SetServiceStatus(handle, ref serviceStatus);

                // Create an instance of the request controller
                requestController = new RequestController();

                // Wire in the event
                requestController.StatusChanged += new RequestControllerStatusDelegate(requestController_StatusChanged);

                // Start the controller
                requestController.Start();
            }
            catch (Exception ex)
            {
                // We've failed to start the service, stop it
                OnStop();
            }
        }

        /// <summary>
        /// Requests the controller_ status changed.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="lastError">The last error.</param>
        void requestController_StatusChanged(RequestControllerStatus status, Exception lastError)
        {
            // Get a handle to this service instance
            IntPtr handle = this.ServiceHandle;

            switch (status)
            {
                case RequestControllerStatus.Started:
                    {
                        // Update the service status to pending
                        serviceStatus.currentState = (int)State.SERVICE_RUNNING;
                        SetServiceStatus(handle, ref serviceStatus);

                        break;
                    }
                case RequestControllerStatus.Stopped:
                    {
                        // If we have an exception, call stop as may not have been called via SCM
                        if (null != lastError)
                        {
                            // We need to log that we've stopped due to error
                            LogEntry logEntry = new LogEntry(
                                "The Request Manager service stopped due to the following error;" + Environment.NewLine + lastError.Message,
                                "Request Management Service Stop",
                                0,
                                0,
                                TraceEventType.Start,
                                "Request Management Service Stop",
                                null);

                            // Write to the log
                            Logger.Write(logEntry);

                            // Set the exit code
                            this.ExitCode = 1;

                            // We could simply stop the service here
                            this.Stop();
                        }
                        else
                        {
                            // Update the service status to pending
                            serviceStatus.currentState = (int)State.SERVICE_STOPPED;
                            SetServiceStatus(handle, ref serviceStatus);
                        }

                        break;
                    }
            }
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Stop command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service stops running.
        /// </summary>
        protected override void OnStop()
        {
            // Get a handle to this service instance
            IntPtr handle = this.ServiceHandle;

            try
            {
                // Update the service status to stopped
                serviceStatus.currentState = (int)State.SERVICE_STOP_PENDING;
                SetServiceStatus(handle, ref serviceStatus);

                // Stop the controller if we have one and it's running
                if (null != requestController && requestController.IsRunning)
                {
                    // Stop the controller
                    requestController.Stop();
                }
            }
            catch
            {
                // Update the service status to stopped
                serviceStatus.currentState = (int)State.SERVICE_STOPPED;
                SetServiceStatus(handle, ref serviceStatus);
            }
            finally
            {
                // Dispose of the request controller
                requestController = null;
            }
        }
    }
}
