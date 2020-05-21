using System;

namespace Discovery.Integration
{
    /// <summary>
    /// A class for connection settings
    /// </summary>
    [Serializable]
    public abstract class ConnectionSettings
    {
    }
    
    /// <summary>
    /// A class for FTP connection settings
    /// </summary>
    [Serializable]
    public class FTPConnectionSettings : ConnectionSettings
    {
        private int port;
        private string username;
        private string password;
        private string ipAddress;
        private int errorCount;

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>The port.</value>
        public int Port
        {
            get { return port; }
            set { port = value; }
        }


        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        /// <summary>
        /// Gets or sets the error count.
        /// </summary>
        /// <value>The error count.</value>
        public int ErrorCount
        {
            get { return errorCount; }
            set { errorCount = value; }
        }


        /// <summary>
        /// Gets or sets the ip address.
        /// </summary>
        /// <value>The ip address.</value>
        public string IpAddress
        {
            get { return ipAddress; }
            set { ipAddress = value; }
        }

        /// <summary>
        /// Gets the name of the host.
        /// </summary>
        /// <value>The name of the host.</value>
        public string HostName
        {
            get
            {
                if (Port!=0)
                {
                    return string.Concat(IpAddress,":",Port.ToString());
                }
                else
                {
                    return IpAddress;
                }
            }
        }
    }

    /// <summary>
    /// A class for MSMQ connection strings
    /// </summary>
    [Serializable]
    public class MSMQConnectionSettings : ConnectionSettings
    {
        private string queueName;

        /// <summary>
        /// Gets or sets the name of the queue.
        /// </summary>
        /// <value>The name of the queue.</value>
        public string QueueName
        {
            get { return queueName; }
            set { queueName = value; }
        }
    }
}