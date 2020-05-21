using System;

namespace Discovery.Utility.DataAccess.Exceptions
{
    /// <summary>
    /// A class to handle exception for general database
    /// </summary>
    public class GeneralDatabaseException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:GeneralDatabaseException"/> class.
        /// </summary>
        /// <param name="exeception">The exeception.</param>
        public GeneralDatabaseException(Exception exeception)
            : base(exeception.Message, exeception)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GeneralDatabaseException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public GeneralDatabaseException(string message)
            : base(message)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="T:GeneralDatabaseException"/> class.
        /// </summary>
        public GeneralDatabaseException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GeneralDatabaseException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        public GeneralDatabaseException(string message, Exception ex)
            : base(message, ex)
        {
        }
    }
}