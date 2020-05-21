using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

using HBOS.FS.Common.ExceptionManagement;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Exceptions
{
    /// <summary>
    /// ExportExceptions - UPD Application Export data exception
    /// </summary>
    [Serializable]
    public class ExportException : UPDException
    {
        private const string m_ExportExceptionsInfoKey = "ExportExceptionsInfo";

		/// <summary>
		/// Custom Export information
		/// </summary>
        private string m_ExportExceptionsInfo; 

        /// <summary>
        /// Creates a new <see cref="ExportException"/> instance. This is the equivalent of the
        /// default constructor, but enforces the provision of some export exception information.
        /// </summary>
        public ExportException () 
        {
            T.E();
            m_ExportExceptionsInfo = "";
            T.X();
        }

        /// <summary>
        /// Creates a new <see cref="ExportException"/> instance. This is the equivalent of the
        /// default constructor, but enforces the provision of some export exception information.
        /// </summary>
        /// <param name="message">A string, in this case, representing the error message.</param>
        public ExportException (string message) : base( message )
        {
        }

        /// <summary>
        /// Creates a new <see cref="ExportException"/> instance. This is the first over-loaded
        /// constructor, following the template above.
        /// </summary>
        /// <param name="message">Diagnostic message </param>
        /// <param name="exportInfo">Again, some export info.</param>
        public ExportException ( string message, string exportInfo ): base ( message )
        {
            T.E();
            m_ExportExceptionsInfo = exportInfo; 
            T.X();
        }

        /// <summary>
        /// Creates a new <see cref="ExportException"/> instance. This is the 3rd constructor,
        /// in accordance with the best practice template
        /// </summary>
        /// <param name="message">Diagnostic message.</param>
        /// <param name="inner">The inner exception to be wrapped</param>
        public ExportException (string message, Exception inner) : base ( message, inner )
        {
            T.E();
            m_ExportExceptionsInfo = "";
            T.X();
        }

        /// <summary>
        /// Creates a new <see cref="ExportException"/> instance. This is the 3rd constructor,
        /// in accordance with the best practice template
        /// </summary>
        /// <param name="message">Diagnostic message.</param>
        /// <param name="exportInfo">Some database info.</param>
        /// <param name="inner">The inner exception to be wrapped</param>
        public ExportException (string message, string exportInfo, Exception inner) : base ( message, inner )
        {
            T.E();
            m_ExportExceptionsInfo = exportInfo;
            T.X();
        }

        /// <summary>
        /// Creates a new <see cref="ExportException"/> instance. This is the final required constructor which
        /// makes sure you can create your custom data from the serialization info.
        /// </summary>
        /// <param name="info">The deserialization info</param>
        /// <param name="context">The deserialization context</param>
        protected ExportException (SerializationInfo info, StreamingContext context) : 
            base( info, context )
        {
            T.E();
            //Retrieve our custom data from the serialization info
            m_ExportExceptionsInfo = info.GetString( m_ExportExceptionsInfoKey );
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
            info.AddValue( m_ExportExceptionsInfoKey, m_ExportExceptionsInfo );
            T.X();
        }

        /// <summary>
        /// Read-only access to the exception's custom data.
        /// </summary>
        public string DatabaseInfo
        {
            get 
            { 
                return m_ExportExceptionsInfo; 
            }
        }
    }
}