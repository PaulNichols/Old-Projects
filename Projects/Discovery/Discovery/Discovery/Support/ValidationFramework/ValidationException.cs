#region Using Directives

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

#endregion

namespace ValidationFramework
{
    /// <summary>
    /// Can optionally use this exception to throw an exception from the middle tier
    /// when asked to perform an optionation on an invalid object.  For example,
    /// if an invalid object makes it to the data access code, a data mapper could
    /// could throw a ValidationException when it is asked to save an object that
    /// is invalid.  This is more descriptive than throwing an InvalidOperationException.
    /// </summary>
    [Serializable]
    public class ValidationException : Exception
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ValidationException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ValidationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ValidationException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ValidationException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"></see> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult"></see> is zero (0). </exception>
        /// <exception cref="T:System.ArgumentNullException">The info parameter is null. </exception>
        protected ValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ValidationException"/> class.
        /// </summary>
        public ValidationException()
        {
        }

        #endregion

    }
}
