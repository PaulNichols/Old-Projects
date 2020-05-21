using System;
using Discovery.BusinessObjects;
using ValidationFramework;

namespace Discovery.Integration
{
    /// <summary>
    /// A class to handle the connection
    /// </summary>
    [Serializable]
    public class ConnectionDownTime:PersistableBusinessObject
    {
        private int connectionId;
        private Connection connection;
        private DateTime startTime;
        private DateTime endTime;
        private DayOfWeek dayOfWeek;


        /// <summary>
        /// Gets or sets the day of week.
        /// </summary>
        /// <value>The day of week.</value>
        public DayOfWeek DayOfWeek
        {
            get { return dayOfWeek; }
            set { dayOfWeek = value; }
        }




        /// <summary>
        /// Gets or sets the connection id.
        /// </summary>
        /// <value>The connection id.</value>
        public int ConnectionId
        {
            get { return connectionId; }
            set { connectionId = value; }
        }

        /// <summary>
        /// Gets or sets the connection.
        /// </summary>
        /// <value>The connection.</value>
        public Connection Connection
        {
            get { return connection; }
            set { connection = value; }
        }

        /// <summary>
        /// Gets or sets the end time.
        /// </summary>
        /// <value>The end time.</value>
        [RequiredValidator("End Time is required.", "*")]
        [RegexValidator("^([0-1][0-9]|[2][0-3]):([0-5][0-9])$", "Please enter a valid End Time", "*")]
        public DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>The start time.</value>
        [RequiredValidator("Start Time is required.", "*")]
        [RegexValidator("^([0-1][0-9]|[2][0-3]):([0-5][0-9])$","Please enter a valid Start Time","*")]
        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }
    }
}