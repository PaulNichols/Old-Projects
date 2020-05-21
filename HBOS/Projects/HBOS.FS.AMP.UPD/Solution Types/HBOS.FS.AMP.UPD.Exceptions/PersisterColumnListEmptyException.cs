using System;

using System.Runtime.Serialization;
using System.Security.Permissions;

using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Exceptions
{
	/// <summary>
	/// PersisterColumnListEmptyException - UPD Application Database exception
	/// </summary>
	[Serializable]
	public class PersisterColumnListEmptyException : UPDException
	{
		private const string m_PersisterNameKey = "PersisterName";
		private string m_PersisterName = string.Empty;

		/// <summary>
		/// Creates a new <see cref="PersisterColumnListEmptyException"/> instance. This is the equivalent of the
		/// default constructor, but enforces the provision of some database exception information.
		/// </summary>
		public PersisterColumnListEmptyException () 
		{
			T.E();
			T.X();
		}

		/// <summary>
		/// Creates a new <see cref="PersisterColumnListEmptyException"/> instance. This is the equivalent of the
		/// default constructor, but enforces the provision of some database exception information.
		/// </summary>
		/// <param name="message">A string, in this case, representing the error message.</param>
		public PersisterColumnListEmptyException (string message) : base( message )
		{
			T.E();
			T.X();
		}

		/// <summary>
		/// Creates a new <see cref="PersisterColumnListEmptyException"/> instance. This is the first over-loaded
		/// constructor, following the template above.
		/// </summary>
		/// <param name="message">Diagnostic message </param>
		/// <param name="persisterName">The name of the persister class</param>
		public PersisterColumnListEmptyException ( string message, string persisterName ): base ( message )
		{
			T.E();
			m_PersisterName = persisterName; 
			T.X();
		}

		/// <summary>
		/// Creates a new <see cref="PersisterColumnListEmptyException"/> instance. This is the 3rd constructor,
		/// in accordance with the best practice template
		/// </summary>
		/// <param name="message">Diagnostic message.</param>
		/// <param name="inner">The inner exception to be wrapped</param>
		public PersisterColumnListEmptyException (string message, Exception inner) : base ( message, inner )
		{
			T.E();
			T.X();
		}

		/// <summary>
		/// Creates a new <see cref="PersisterColumnListEmptyException"/> instance. This is the 3rd constructor,
		/// in accordance with the best practice template
		/// </summary>
		/// <param name="message">Diagnostic message.</param>
		/// <param name="persisterName">The name of the persister class</param>
		/// <param name="inner">The inner exception to be wrapped</param>
		public PersisterColumnListEmptyException (string message, string persisterName, Exception inner) : base ( message, inner )
		{
			T.E();
			m_PersisterName = persisterName;
			T.X();
		}

		/// <summary>
		/// Creates a new <see cref="PersisterColumnListEmptyException"/> instance. This is the final required constructor which
		/// makes sure you can create your custom data from the serialization info.
		/// </summary>
		/// <param name="info">The deserialization info</param>
		/// <param name="context">The deserialization context</param>
		protected PersisterColumnListEmptyException (SerializationInfo info, StreamingContext context) : 
			base( info, context )
		{
			T.E();
			//Retrieve our custom data from the serialization info
			m_PersisterName = info.GetString( m_PersisterNameKey );
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
			info.AddValue( m_PersisterName, m_PersisterNameKey);
			T.X();
		}

		/// <summary>
		/// Read-only access to the exception's custom data.
		/// </summary>
		public string PersisterName
		{
			get 
			{ 
				return m_PersisterName;
			}
		}
	}
}

