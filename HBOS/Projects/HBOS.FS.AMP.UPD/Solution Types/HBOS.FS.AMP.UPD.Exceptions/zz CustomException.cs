using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

using HBOS.FS.Common.ExceptionManagement;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Exceptions
{
    /// <summary>
    /// This is an example custom exception class that works with the HBOS Exception Management code
    /// </summary>
    [Serializable]
    public class CustomException : BaseApplicationException
    {
        private const string m_CustomExceptionInfoKey = "CustomExceptionInfo";

        private string m_CustomExceptionInfo; 

        /// <summary>
        /// Creates a new <see cref="CustomException"/> instance. This is the equivalent of the
        /// default constructor, but enforces the provision of some custom exception information.
        /// </summary>
        public CustomException () 
        {
            T.E();
            m_CustomExceptionInfo = "";
            T.X();
        }

        /// <summary>
        /// Creates a new <see cref="CustomException"/> instance. This is the equivalent of the
        /// default constructor, but enforces the provision of some custom exception information.
        /// </summary>
        /// <param name="customExceptionInfo">A string, in this case, representing some form of custom exception info.</param>
        public CustomException (string customExceptionInfo) 
        {
            T.E();
            m_CustomExceptionInfo = customExceptionInfo;
            T.X();
        }

        /// <summary>
        /// Creates a new <see cref="CustomException"/> instance. This is the first over-loaded
        /// constructor, following the template above.
        /// </summary>
        /// <param name="message">Diagnostic message </param>
        /// <param name="customInfo">Again, some custom info.</param>
        public CustomException ( string message, string customInfo ): base ( message )
        {
            T.E();
            m_CustomExceptionInfo = customInfo; 
            T.X();
        }

        /// <summary>
        /// Creates a new <see cref="CustomException"/> instance. This is the 3rd constructor,
        /// in accordance with the best practice template
        /// </summary>
        /// <param name="message">Diagnostic message.</param>
        /// <param name="inner">The inner exception to be wrapped</param>
        public CustomException (string message, Exception inner) : 
            base ( message, inner )
        {
            T.E();
            m_CustomExceptionInfo = "";
            T.X();
        }

        /// <summary>
        /// Creates a new <see cref="CustomException"/> instance. This is the 3rd constructor,
        /// in accordance with the best practice template
        /// </summary>
        /// <param name="message">Diagnostic message.</param>
        /// <param name="customInfo">Some custom info.</param>
        /// <param name="inner">The inner exception to be wrapped</param>
        public CustomException (string message, string customInfo, Exception inner) : 
            base ( message, inner )
        {
            T.E();
            m_CustomExceptionInfo = customInfo;
            T.X();
        }

        /// <summary>
        /// Creates a new <see cref="CustomException"/> instance. This is the final required constructor which
        /// makes sure you can create your custom data from the serialization info.
        /// </summary>
        /// <param name="info">The deserialization info</param>
        /// <param name="context">The deserialization context</param>
        protected CustomException (SerializationInfo info, StreamingContext context) : 
            base( info, context )
        {
            T.E();
            //Retrieve our custom data from the serialization info
            m_CustomExceptionInfo = info.GetString( m_CustomExceptionInfoKey );
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
            info.AddValue( m_CustomExceptionInfoKey, m_CustomExceptionInfo );
            T.X();
        }

        /// <summary>
        /// Read-only access to the exception's custom data.
        /// </summary>
        public string CustomInfo
        {
            get 
            { 
                return m_CustomExceptionInfo; 
            }
        }
    }

}
