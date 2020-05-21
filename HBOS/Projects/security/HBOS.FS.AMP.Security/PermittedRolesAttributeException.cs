using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace HBOS.FS.AMP.Security
{
    /// <summary>
    /// The exception that is thrown when the <see cref="PermittedRolesAttribute"/> is applied to a field that does nto expose a Visible property.
    /// </summary>
    [Serializable]    
    public sealed class PermittedRolesAttributeException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PermittedRolesAttributeException"/> class. This is the default constructor.
        /// </summary>
        public PermittedRolesAttributeException() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PermittedRolesAttributeException"/> class with the specified message.
        /// </summary>
        /// <param name="message">The string to display when the exception is thrown.</param>
        public PermittedRolesAttributeException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PermittedRolesAttributeException"/> class with the specified message and inner exception.
        /// </summary>
        /// <param name="message">The string to display when the exception is thrown.</param>
        /// <param name="innerException">A reference to an inner exception.</param>
        /// <remarks>
        /// You can create a new exception that catches an earlier exception. The code that handles the second 
        /// exception can make use of the additional information from the earlier exception, also called 
        /// an inner exception, to examine the cause of the initial error.
        /// </remarks>
        public PermittedRolesAttributeException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Intializes a serializable instance of the <see cref="PermittedRolesAttributeException"/>.
        /// </summary>
        /// <param name="info">SerializationInfo needed to serialize or deserialize an object.</param>
        /// <param name="context">StreamingContext containing the source or destination information for serialization.</param>
        private PermittedRolesAttributeException(SerializationInfo info, StreamingContext context): base( info, context )
        {
        }

        /// <summary>
        /// Gets the object data necessary for serialization.
        /// </summary>
        /// <param name="info">SerializationInfo needed to serialize or deserialize an object.</param>
        /// <param name="context">StreamingContext containing the source or destination information for serialization.</param>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context )
        {
            base.GetObjectData(info, context); 
        }
    }
}
