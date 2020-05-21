using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Discovery.BusinessObjects;
using ValidationFramework;

namespace Discovery.Integration
{
    /// <summary>
    /// A class to handle the connection types
    /// </summary>
    [Serializable]
    public class Connection : PersistableBusinessObject
    {
        /// <summary>
        /// An enumeration to include channel type FTP or MSMQ
        /// </summary>
        public enum ChannelTypeEnum
        {
            /// <summary>
            /// FTP type of ChannelTypeEnum
            /// </summary>
            FTP = 0,
            /// <summary>
            /// MSMQ type of ChannelTypeEnum
            /// </summary>
            MSMQ = 1
        }

        /// <summary>
        /// An enumeration to include connection type Shipment, OPTRAK, COMMANDER or MS 
        /// </summary>
        public enum ConnectionTypeEnum
        {
            /// <summary>
            /// Shipment type of ConnectionTypeEnum
            /// </summary>
            Shipment,
            /// <summary>
            /// Optrak type of ConnectionTypeEnum
            /// </summary>
            Optrak,
            /// <summary>
            /// Commander type of ConnectionTypeEnum
            /// </summary>
            Commander,
            /// <summary>
            /// MS type of ConnectionTypeEnum
            /// </summary>
            MS
        }
        
        private string name;
        
        private int scheduleId;
        private bool active;
        private ConnectionTypeEnum connectionType;
        private ChannelTypeEnum channelType;
        private ConnectionSettings settings;
        private int errorCount;
        private bool isDown;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [RequiredValidator("Name is required.", "*")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Connection"/> is active.
        /// </summary>
        /// <value><c>true</c> if active; otherwise, <c>false</c>.</value>
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        /// <summary>
        /// Gets or sets the type of the connection.
        /// </summary>
        /// <value>The type of the connection.</value>
        public ConnectionTypeEnum ConnectionType
        {
            get { return connectionType; }
            set { connectionType = value; }
        }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public ConnectionSettings Settings
        {
            get { return settings; }
            set { settings = value; }
        }

        /// <summary>
        /// Gets or sets the settings serialised.
        /// </summary>
        /// <value>The settings serialised.</value>
        public string SettingsSerialised
        {
            get
            {
                
                XmlSerializer xmlSerializer = new XmlSerializer(Settings.GetType());
                StringBuilder stringBuilder = new StringBuilder();
                StringWriter stringWriter = new StringWriter(stringBuilder);
                xmlSerializer.Serialize(stringWriter, Settings);
                return stringBuilder.ToString();
            }
            set
            {
                XmlSerializer xmlSerializer;
                 try {
                    xmlSerializer = new XmlSerializer(typeof (MSMQConnectionSettings));
                    StringReader stringReader = new StringReader(value);
                    settings = xmlSerializer.Deserialize(stringReader) as ConnectionSettings;
                }
               catch(Exception)
                {
                    xmlSerializer = new XmlSerializer(typeof (FTPConnectionSettings));
                    StringReader stringReader = new StringReader(value);
                    settings = xmlSerializer.Deserialize(stringReader) as ConnectionSettings;
                }
              
            }
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
        /// Gets or sets the schedule id.
        /// </summary>
        /// <value>The schedule id.</value>
        public int ScheduleId
        {
            get { return scheduleId; }
            set { scheduleId = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is down.
        /// </summary>
        /// <value><c>true</c> if this instance is down; otherwise, <c>false</c>.</value>
        public bool IsDown
        {
            get { return isDown; }
            set { isDown = value; }
        }

        /// <summary>
        /// Gets or sets the type of the channel.
        /// </summary>
        /// <value>The type of the channel.</value>
        public ChannelTypeEnum ChannelType
        {get { return channelType; }
            set { channelType = value; }
        }
    }
}