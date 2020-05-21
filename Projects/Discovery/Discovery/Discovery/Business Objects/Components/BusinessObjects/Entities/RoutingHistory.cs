using System;
using Discovery.Utility;

namespace Discovery.BusinessObjects
{
    /// <summary>
    /// A Class 'RoutingHistory' which is an entity with namespace Discovery.BusinessObjects
    /// It is inherited from PersistableBusinessObject
    /// The class holds the history of routing
    /// </summary>

    [Serializable]
    public class RoutingHistory : PersistableBusinessObject
    {
        /// <summary>
        /// An enumeration to include status InProcess, Sent, Received, Reset and None
        /// </summary>

        public enum StatusEnum
        {
            /// <summary>
            /// InProcess status of StatusEnum
            /// </summary>
            InProcess,
            /// <summary>
            /// Sent status of StatusEnum
            /// </summary>
            Sent,
            /// <summary>
            /// Recieved status of StatusEnum
            /// </summary>
            Recieved,
            /// <summary>
            /// Reset status of StatusEnum
            /// </summary>
            Reset,
            /// <summary>
            /// None status of StatusEnum
            /// </summary>
            None
        }

        #region Private Fields

        private DateTime tripFileReceivedDate;
        private DateTime dropFileReceivedDate;
        private DateTime tripPartFileReceivedDate;
        private DateTime sentDate;
        private DateTime processStartedDate;
        private DateTime resetDate;

        private string processedBy;
        private string resetBy;
        private string regionCode;
        private int regionId;

        #endregion

        #region Public Properties


        #endregion

        #region Public Method(s)

        #endregion

        #region Protected Method(s)

        #endregion

        #region Private Method(s)

        #endregion

        #region Constructor(s)

        #endregion



        /// <summary>
        /// Gets or sets the received date.
        /// </summary>
        /// <value>The received date.</value>
        public DateTime TripFileReceivedDate
        {
            get { return tripFileReceivedDate; }
            set { tripFileReceivedDate = value; }
        }

        /// <summary>
        /// Gets or sets the sent date.
        /// </summary>
        /// <value>The sent date.</value>
        public DateTime SentDate
        {
            get { return sentDate; }
            set { sentDate = value; }
        }

        /// <summary>
        /// Gets or sets the process started date.
        /// </summary>
        /// <value>The process started date.</value>
        public DateTime ProcessStartedDate
        {
            get { return processStartedDate; }
            set { processStartedDate = value; }
        }

        /// <summary>
        /// Gets or sets the reset date.
        /// </summary>
        /// <value>The reset date.</value>
        public DateTime ResetDate
        {
            get { return resetDate; }
            set { resetDate = value; }
        }

        /// <summary>
        /// Derives the status.
        /// </summary>
        /// <value>The status.</value>
        public StatusEnum Status
        {
            get
            {
                StatusEnum status = StatusEnum.InProcess;
                if (SentDate == Null.NullDate && ResetDate == Null.NullDate && TripPartFileReceivedDate == Null.NullDate && DropFileReceivedDate == Null.NullDate)
                {
                    status = StatusEnum.InProcess;
                }
                else if (ResetDate != Null.NullDate)
                {
                    status = StatusEnum.Reset;
                }
                else if (TripPartFileReceivedDate != Null.NullDate && DropFileReceivedDate!= Null.NullDate)
                {
                    status = StatusEnum.Recieved;
                }
                else if (SentDate != Null.NullDate && ResetDate == Null.NullDate && TripPartFileReceivedDate == Null.NullDate && DropFileReceivedDate == Null.NullDate)
                {
                    status = StatusEnum.Sent;
                }
                return status;
            }

        }

        /// <summary>
        /// Gets or sets the processed by.
        /// </summary>
        /// <value>The processed by.</value>
        public string ProcessedBy
        {
            get { return processedBy; }
            set { processedBy = value; }
        }

        /// <summary>
        /// Gets or sets the reset by.
        /// </summary>
        /// <value>The reset by.</value>
        public string ResetBy
        {
            get { return resetBy; }
            set { resetBy = value; }
        }

        /// <summary>
        /// Gets or sets the warehouse codes.
        /// </summary>
        /// <value>The warehouse codes.</value>
        public string RegionCode
        {
            get { return regionCode; }
            set { regionCode = value; }
        }

        public int RegionId
        {
            get { return regionId; }
            set { regionId = value; }
        }

        public DateTime DropFileReceivedDate
        {
            get { return dropFileReceivedDate; }
            set { dropFileReceivedDate = value; }
        }

        public DateTime TripPartFileReceivedDate
        {
            get { return tripPartFileReceivedDate; }
            set { tripPartFileReceivedDate = value; }
        }
    }

}
