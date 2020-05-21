
using System;
using System.Diagnostics;
using Discovery.Utility;

namespace Discovery.BusinessObjects
{
    [Serializable]
    public class LogEntryCriteria : PersistableBusinessObject
    {
        public LogEntryCriteria(Nullable<bool> requiresAcknowledgement, 
            string categoryName,int categoryId, string opcoCode, 
            string severity, DateTime timeStampFrom, DateTime timeStampTo, 
            bool? acknowledged, string messageText, string errorType)
        {
            this.RequiresAcknowledgement = requiresAcknowledgement;
            this.categoryId = categoryId;
            this.opcoCode = opcoCode;
            this.severity = severity;
            this.timeStampFrom = timeStampFrom;
            this.timeStampTo = timeStampTo;
            this.acknowledged = acknowledged;
            this.messageText = messageText;
            this.errorType = errorType;
            this.categoryName = categoryName;
        }

        private  bool? requiresAcknowledgement;
        private readonly int categoryId;
        private string opcoCode;
        private string severity;
        private int priority;
        private DateTime timeStampFrom;
        private bool? acknowledged;
        private readonly string messageText;
        private readonly string errorType;
        private DateTime timeStampTo;
        private string categoryName;


        public string Severity
        {
            get { return severity; }
           
        }

        public string OpcoCode
        {
            get { return opcoCode; }
           
        }

        public int Priority
        {
            get { return priority; }
          
        }

        public DateTime TimeStampFrom
        {
            get { return timeStampFrom; }
          
        }

        public DateTime TimeStampTo
        {
            get { return timeStampTo; }
           
        }

        public bool? Acknowledged
        {
            get { return acknowledged; }
           
        }

        public string MessageText
        {
            get { return messageText; }
        }

        public string ErrorType
        {
            get { return errorType; }
        }

        public int CategoryId
        {
            get { return categoryId; }
        }

        public string CategoryName
        {
            get { return categoryName; }
        }

        public bool? RequiresAcknowledgement
        {
            get { return requiresAcknowledgement; }
            set { requiresAcknowledgement = value; }
        }
    }

    /// <summary>
    /// A Class 'LogEntry' which is an entity with namespace Discovery.BusinessObjects
    /// It is inherited from PersistableBusinessObject
    /// The class is holding an entry log details
    /// </summary>
    [Serializable]
    public class LogEntry : PersistableBusinessObject
    {
        private string formattedMessage;
        //private int priority;
        private TraceEventType severity;
        private int eventID;
        private string categoryName;
        private DateTime timeStamp;
        private string acknowledgedBy;
        private DateTime acknowledgedDate;
        private string message;
        private bool requiresAcknowledgement;
        private string errorType;

        public string Message
        {
            get
            {
                return message;
            }
            set { message = value; }
        }
        ///// <summary>
        ///// Gets or sets the priority.
        ///// </summary>
        ///// <value>The priority.</value>
        //public int Priority
        //{
        //    get { return priority; }
        //    set { priority = value; }
        //}

        /// <summary>
        /// Gets or sets the formatted message XML.
        /// </summary>
        /// <value>The formatted message XML.</value>
        public string FormattedMessageXML
        {
            get
            {
                string xml = formattedMessage;
                //Remove any text before and after the xml portion of the formatted message
                string checkString = "<Exception>";
                int indexOfCheckString = xml.IndexOf(checkString);
                if (indexOfCheckString >0)
                {
                    xml = xml.Remove(0, indexOfCheckString);
                    checkString = "</Exception>";
                    indexOfCheckString = xml.IndexOf(checkString);
                    if (indexOfCheckString > 0)
                    {
                        if (xml.Length != indexOfCheckString + checkString.Length)
                        {
                            xml = xml.Remove(indexOfCheckString + checkString.Length);
                        }
                    }
                }
                else
                {
                     xml="";    
                }
                //********************************
                return xml;
            }
            set { FormattedMessage = value; }
        }

        /// <summary>
        /// Gets or sets the severity.
        /// </summary>
        /// <value>The severity.</value>
        public TraceEventType Severity
        {
            get { return severity; }
            set { severity = value; }
        }

        /// <summary>
        /// Gets or sets the event ID.
        /// </summary>
        /// <value>The event ID.</value>
        public int EventID
        {
            get { return eventID; }
            set { eventID = value; }
        }

        /// <summary>
        /// Gets or sets the categoryName.
        /// </summary>
        /// <value>The categoryName.</value>
        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }

        /// <summary>
        /// Gets or sets the time stamp.
        /// </summary>
        /// <value>The time stamp.</value>
        public DateTime TimeStamp
        {
            get { return timeStamp; }
            set { timeStamp = value; }
        }

        /// <summary>
        /// Gets or sets the formatted message.
        /// </summary>
        /// <value>The formatted message.</value>
        public string FormattedMessage
        {
            get { return formattedMessage; }
            set { formattedMessage = value; }
        }


        /// <summary>
        /// Gets or sets the acknowledged by.
        /// </summary>
        /// <value>The acknowledged by.</value>
        public string AcknowledgedBy
        {
            get { return acknowledgedBy; }
            set { acknowledgedBy = value; }
        }

        public DateTime AcknowledgedDate
        {
            get { return acknowledgedDate; }
            set { acknowledgedDate = value; }
        }
  
        public bool Acknowledged
        {
            get { return acknowledgedDate!=Null.NullDate && acknowledgedBy!=Null.NullString; }
           
        }

        public bool RequiresAcknowledgement
        {
            get { return requiresAcknowledgement; }
            set { requiresAcknowledgement = value; }
        }

        public string ErrorType
        {
            get { return errorType; }
            set { errorType = value; }
        }
    }
}
