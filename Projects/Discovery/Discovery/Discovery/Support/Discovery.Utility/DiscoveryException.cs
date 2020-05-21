using System;
using System.Collections.Generic;

namespace Discovery.ComponentServices.ExceptionHandling
{
    /// <summary>
    /// A class to handle exception
    /// </summary>
    public class DiscoveryException : Exception
    {
        public Dictionary<string, object> Properties
        {
            get
            {
                if (properties == null)
                {
                    properties = new Dictionary<string, object>();
                    properties.Add("OpCoCode", "");
                    properties.Add("OperatorEmail", "");
                }
                return properties;
            }
            set { properties = value; }
        }

        private Dictionary<string, object> properties;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:DiscoveryException"/> class.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public DiscoveryException(Exception ex)
            : this(ex.Message, ex)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:DiscoveryException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public DiscoveryException(string message)
            : this(message, null)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="T:DiscoveryException"/> class.
        /// </summary>
        public DiscoveryException()
            : this("", null)
        {

        }



        /// <summary>
        /// Initializes a new instance of the <see cref="T:DiscoveryException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        public DiscoveryException(string message, Exception ex)
            : base(message, ex)
        {
        }



    }
}