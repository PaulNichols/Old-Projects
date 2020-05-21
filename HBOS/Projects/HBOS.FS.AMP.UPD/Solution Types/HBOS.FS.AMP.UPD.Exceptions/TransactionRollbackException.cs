using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

using HBOS.FS.Common.ExceptionManagement;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Exceptions
{
	/// <summary>
	/// The custome exception thrown when there has been an error rolling back a database transaction.
	/// </summary>
    [Serializable]
    public class TransactionRollbackException : UPDException
    {
        private const string m_RollbackExceptionInfoKey = "TransactionRollbackExceptionInfo";

        /// <summary>
        /// Custom database transaction rollback information
        /// </summary>
        private string m_RollbackExceptionInfo; 

        /// <summary>
        /// Creates a new <see cref="TransactionRollbackException"/> instance. 
        /// This is the equivalent of the default constructor, but enforces the 
        /// provision of some transaction rollback exception information.
        /// </summary>
        public TransactionRollbackException () 
        {
            T.E();
            m_RollbackExceptionInfo = "";
            T.X();
        }

        /// <summary>
        /// Creates a new <see cref="TransactionRollbackException"/> instance. 
        /// This is the equivalent of the default constructor, but enforces 
        /// the provision of some transaction rollback exception information.
        /// </summary>
        /// <param name="message">A string, in this case, representing the error message.</param>
        public TransactionRollbackException (string message) : base( message )
        {
        }

        /// <summary>
        /// Creates a new <see cref="TransactionRollbackException"/> instance. 
        /// This is the first over-loaded constructor, following the template above.
        /// </summary>
        /// <param name="message">Diagnostic message </param>
        /// <param name="rollbackInfo">Again, some export info.</param>
        public TransactionRollbackException ( string message, string rollbackInfo ): base ( message )
        {
            T.E();
            m_RollbackExceptionInfo = rollbackInfo; 
            T.X();
        }

        /// <summary>
        /// Creates a new <see cref="TransactionRollbackException"/> instance. 
        /// This is the 3rd constructor, in accordance with the best practice template
        /// </summary>
        /// <param name="message">Diagnostic message.</param>
        /// <param name="inner">The inner exception to be wrapped</param>
        public TransactionRollbackException (string message, Exception inner) : base ( message, inner )
        {
            T.E();
            m_RollbackExceptionInfo = "";
            T.X();
        }

        /// <summary>
        /// Creates a new <see cref="TransactionRollbackException"/> instance. 
        /// This is the 4th constructor, in accordance with the best practice template
        /// </summary>
        /// <param name="message">Diagnostic message.</param>
        /// <param name="rollbackInfo">Some database info.</param>
        /// <param name="inner">The inner exception to be wrapped</param>
        public TransactionRollbackException (string message, string rollbackInfo, Exception inner) : base ( message, inner )
        {
            T.E();
            m_RollbackExceptionInfo = rollbackInfo;
            T.X();
        }

        /// <summary>
        /// Creates a new <see cref="TransactionRollbackException"/> instance. 
        /// This is the final required constructor which
        /// makes sure you can create your custom data from the serialization info.
        /// </summary>
        /// <param name="info">The deserialization info</param>
        /// <param name="context">The deserialization context</param>
        protected TransactionRollbackException (SerializationInfo info, StreamingContext context) : 
            base( info, context )
        {
            T.E();
            //Retrieve our custom data from the serialization info
            m_RollbackExceptionInfo = info.GetString( m_RollbackExceptionInfoKey );
            T.X();
        }

        /// <summary>
        /// Override GetObjectData to ensure any custom data can be serialized
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The serialization context.</param>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
        public override void GetObjectData( SerializationInfo info, StreamingContext context )
        {
            T.E();
            //Always call the base class version first...
            base.GetObjectData(info, context); 
    
            //... then append your exception's custom data to the serialization info class
            info.AddValue( m_RollbackExceptionInfoKey, m_RollbackExceptionInfo );
            T.X();
        }

        /// <summary>
        /// Read-only access to the exception's custom data.
        /// </summary>
        public string RollbackInfo
        {
            get 
            { 
                return m_RollbackExceptionInfo; 
            }
        }
    }
}
