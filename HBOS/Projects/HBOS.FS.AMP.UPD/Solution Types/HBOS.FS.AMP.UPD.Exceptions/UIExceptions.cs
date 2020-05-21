using System;
using System.Runtime.Serialization;

namespace HBOS.FS.AMP.UPD.Exceptions
{
	/// <summary>
	/// Exception for list to list control, raised when user types in a value in an
	/// inline edit that cannot be set back on the property
	/// </summary>
	[Serializable]
	public class UnableToCastEditedValueException : UPDException
	{
		/// <summary>
		/// ctor - just passes to base
		/// </summary>
		public UnableToCastEditedValueException() : base ()
		{
		}

		/// <summary>
		/// ctor - just passes back to base
		/// </summary>
		/// <param name="message"></param>
		public UnableToCastEditedValueException (string message) : base( message )
		{
		}

		/// <summary>
		/// Creates a new <see cref="UnableToCastEditedValueException"/> instance. This is the 3rd constructor,
		/// in accordance with the best practice template
		/// </summary>
		/// <param name="message">Diagnostic message.</param>
		/// <param name="inner">The inner exception to be wrapped</param>
		public UnableToCastEditedValueException (string message, Exception inner) : base ( message, inner )
		{
		}

		/// <summary>
		/// Creates a new <see cref="UnableToCastEditedValueException"/> instance. This is the final required constructor which
		/// makes sure you can create your custom data from the serialization info.
		/// </summary>
		/// <param name="info">The deserialization info</param>
		/// <param name="context">The deserialization context</param>
		protected UnableToCastEditedValueException (SerializationInfo info, StreamingContext context) : base( info, context )
		{
		}
	}


	/// <summary>
	/// Exception for list to list control, raised when property being edited is of an
	/// unknown type
	/// </summary>
	[Serializable]
	public class UnexpectedTypeForEditedValueException : UPDException
	{
		/// <summary>
		/// ctor - just passes back to base
		/// </summary>
		public UnexpectedTypeForEditedValueException() : base ()
		{
		}

		/// <summary>
		/// ctor - just passes back to base
		/// </summary>
		/// <param name="message"></param>
		public UnexpectedTypeForEditedValueException (string message) : base( message )
		{
		}

		/// <summary>
		/// Creates a new <see cref="UnexpectedTypeForEditedValueException"/> instance. This is the 3rd constructor,
		/// in accordance with the best practice template
		/// </summary>
		/// <param name="message">Diagnostic message.</param>
		/// <param name="inner">The inner exception to be wrapped</param>
		public UnexpectedTypeForEditedValueException (string message, Exception inner) : base ( message, inner )
		{
		}

		/// <summary>
		/// Creates a new <see cref="UnexpectedTypeForEditedValueException"/> instance. This is the final required constructor which
		/// makes sure you can create your custom data from the serialization info.
		/// </summary>
		/// <param name="info">The deserialization info</param>
		/// <param name="context">The deserialization context</param>
		protected UnexpectedTypeForEditedValueException (SerializationInfo info, StreamingContext context) : base( info, context )
		{
		}
	}

}
