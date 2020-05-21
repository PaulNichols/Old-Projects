using System;
using HBOS.FS.Common.ExceptionManagement;
using System.Runtime.Serialization;

namespace HBOS.FS.AMP.UPD.Exceptions
{
	/// <summary>
	/// Used purely so we can distinguish UPD exceptions from other exceptions
	/// (in addition to namespace) and so that we can in future add base UPD specific functionality if required.
	/// Is abstract class - always derive from this (you cant just use it).
	/// </summary>
	[Serializable]
	public abstract class UPDException : BaseApplicationException
	{
		/// <summary>
		/// ctor - just passes back to base
		/// </summary>
		public UPDException() : base()
		{
		}

		/// <summary>
		/// ctor - just passes back to base
		/// </summary>
		/// <param name="message"></param>
		public UPDException (string message) : base( message )
		{
		}

		/// <summary>
		/// Creates a new <see cref="UPDException"/> instance. This is the 3rd constructor,
		/// in accordance with the best practice template
		/// </summary>
		/// <param name="message">Diagnostic message.</param>
		/// <param name="inner">The inner exception to be wrapped</param>
		public UPDException (string message, Exception inner) : base ( message, inner )
		{
		}

		/// <summary>
		/// Creates a new <see cref="UPDException"/> instance. This is the final required constructor which
		/// makes sure you can create your custom data from the serialization info.
		/// </summary>
		/// <param name="info">The deserialization info</param>
		/// <param name="context">The deserialization context</param>
		protected UPDException (SerializationInfo info, StreamingContext context) : base( info, context )
		{
		}



	}
}
