using System;

namespace Discovery.Utility.DataAccess.Exceptions
{
    /// <summary>
    /// A class to handle concurrency exception
    /// </summary>
    public class ConcurrencyException : GeneralDatabaseException
    {
          /// <summary>
        /// Initializes a new instance of the <see cref="T:DiscoveryException"/> class.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public ConcurrencyException(Exception ex) : base(ex.Message, ex)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="T:DiscoveryException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ConcurrencyException(string message)
            : base(message)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="T:ConcurrencyException"/> class.
        /// </summary>
        public ConcurrencyException()
            : base()
    {

    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:ConcurrencyException"/> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="ex">The ex.</param>
        public ConcurrencyException(string message, Exception ex)
            : base(message, ex)
        {
        }
       
    
    }
    
    /// <summary>
    /// A class to handle the foreign ksy constraint
    /// </summary>
    public class ForeignKeyConstraint : GeneralDatabaseException
    {
        
          /// <summary>
        /// Initializes a new instance of the <see cref="T:DiscoveryException"/> class.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public ForeignKeyConstraint(Exception ex) : base(ex.Message, ex)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="T:DiscoveryException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ForeignKeyConstraint(string message)
            : base(message)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="T:ForeignKeyConstraint"/> class.
        /// </summary>
        public ForeignKeyConstraint()
            : base()
    {

    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:ForeignKeyConstraint"/> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="ex">The ex.</param>
        public ForeignKeyConstraint(string message, Exception ex)
            : base(message, ex)
        {
        }
       
    
    }
}