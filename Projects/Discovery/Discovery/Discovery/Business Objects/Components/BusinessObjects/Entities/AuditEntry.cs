using System;

namespace Discovery.BusinessObjects
{
    /// <summary>
    /// A class 'MessageAuditEntry' which is an entity with namespace Discovery.BusinessObjects
    /// It is inherited from PersistableBusinessObject
    /// The class is auditing a message sent from OpCo
    /// </summary>
    [Serializable]
    public class MessageAuditEntry : PersistableBusinessObject
    {
        #region Private fields

        private string sourceSystem;
        private string destinationSystem;
        private string message;
        private DateTime receivedDate;
        private string type;
        private string label;
        private string name;
        private int sequence;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the source system.
        /// </summary>
        /// <value>The source system.</value>
        public string SourceSystem
        {
            get { return sourceSystem; }
            set { sourceSystem = value; }
        }

        /// <summary>
        /// Gets or sets the destination system.
        /// </summary>
        /// <value>The destination system.</value>
        public string DestinationSystem
        {
            get { return destinationSystem; }
            set { destinationSystem = value; }
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        /// <summary>
        /// Gets or sets the received date.
        /// </summary>
        /// <value>The received date.</value>
        public DateTime ReceivedDate
        {
            get { return receivedDate; }
            set { receivedDate = value; }
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>The label.</value>
        public string Label
        {
            get { return label; }
            set { label = value; }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Gets or sets the sequence.
        /// </summary>
        /// <value>The sequence.</value>
        public int Sequence
        {
            get { return sequence; }
            set { sequence = value; }
        }

       

        #endregion

        #region Public Method(s)

        #endregion

        #region Protected Method(s)

        #endregion

        #region Private Method(s)

        #endregion

        #region Constructor(s)

        #endregion

    }
}