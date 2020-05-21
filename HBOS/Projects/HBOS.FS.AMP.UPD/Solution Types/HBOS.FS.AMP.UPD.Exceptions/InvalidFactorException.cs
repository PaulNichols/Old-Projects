using System;
using System.Runtime.Serialization;

namespace HBOS.FS.AMP.UPD.Exceptions
{
	/// <summary>
	/// An exception to be used if data in a factor is not valid (for example when attempting to save)
	/// </summary>
	[Serializable]
	public class InvalidFactorException : UPDException
	{
		/// <summary>
		/// ctor - just passes back to base
		/// </summary>
		public InvalidFactorException() : base()
		{
		}

		/// <summary>
		/// ctor - just passes back to base
		/// </summary>
		/// <param name="message"></param>
		public InvalidFactorException (string message) : base( message )
		{
		}

		/// <summary>
		/// Creates a new <see cref="UPDException"/> instance. This is the 3rd constructor,
		/// in accordance with the best practice template
		/// </summary>
		/// <param name="message">Diagnostic message.</param>
		/// <param name="inner">The inner exception to be wrapped</param>
		public InvalidFactorException (string message, Exception inner) : base ( message, inner )
		{
		}

		/// <summary>
		/// Creates a new <see cref="UPDException"/> instance. This is the final required constructor which
		/// makes sure you can create your custom data from the serialization info.
		/// </summary>
		/// <param name="info">The deserialization info</param>
		/// <param name="context">The deserialization context</param>
		protected InvalidFactorException (SerializationInfo info, StreamingContext context) : base( info, context )
		{
		}

	}
	//these classes are added her to save creating a new file for each and bloating the project for something to small:


	/// <summary>
	/// An exception to be used if data in an X factor is not valid (for example when attempting to save)
	/// </summary>
	[Serializable]
	public class InvalidXFactorException : InvalidFactorException
	{
		/// <summary>
		/// ctor - just passes back to base
		/// </summary>
		public InvalidXFactorException() : base()
		{
		}

		/// <summary>
		/// ctor - just passes back to base
		/// </summary>
		/// <param name="message"></param>
		public InvalidXFactorException (string message) : base( message )
		{
		}

		/// <summary>
		/// Creates a new <see cref="UPDException"/> instance. This is the 3rd constructor,
		/// in accordance with the best practice template
		/// </summary>
		/// <param name="message">Diagnostic message.</param>
		/// <param name="inner">The inner exception to be wrapped</param>
		public InvalidXFactorException (string message, Exception inner) : base ( message, inner )
		{
		}

		/// <summary>
		/// Creates a new <see cref="UPDException"/> instance. This is the final required constructor which
		/// makes sure you can create your custom data from the serialization info.
		/// </summary>
		/// <param name="info">The deserialization info</param>
		/// <param name="context">The deserialization context</param>
		protected InvalidXFactorException (SerializationInfo info, StreamingContext context) : base( info, context )
		{
		}

	}

	/// <summary>
	/// An exception to be used if data in a Revaluation factor is not valid (for example when attempting to save)
	/// </summary>
	[Serializable]
	public class InvalidRevaluationFactorException : InvalidFactorException
	{
		/// <summary>
		/// ctor - just passes back to base
		/// </summary>
		public InvalidRevaluationFactorException() : base()
		{
		}

		/// <summary>
		/// ctor - just passes back to base
		/// </summary>
		/// <param name="message"></param>
		public InvalidRevaluationFactorException (string message) : base( message )
		{
		}

		/// <summary>
		/// Creates a new <see cref="UPDException"/> instance. This is the 3rd constructor,
		/// in accordance with the best practice template
		/// </summary>
		/// <param name="message">Diagnostic message.</param>
		/// <param name="inner">The inner exception to be wrapped</param>
		public InvalidRevaluationFactorException (string message, Exception inner) : base ( message, inner )
		{
		}

		/// <summary>
		/// Creates a new <see cref="UPDException"/> instance. This is the final required constructor which
		/// makes sure you can create your custom data from the serialization info.
		/// </summary>
		/// <param name="info">The deserialization info</param>
		/// <param name="context">The deserialization context</param>
		protected InvalidRevaluationFactorException (SerializationInfo info, StreamingContext context) : base( info, context )
		{
		}
	}

	/// <summary>
	/// An exception to be used if data in a Revaluation factor is not valid (for example when attempting to save)
	/// </summary>
	[Serializable]
	public class InvalidTaxProvisionEstimateException : InvalidFactorException
	{
		/// <summary>
		/// ctor - just passes back to base
		/// </summary>
		public InvalidTaxProvisionEstimateException() : base()
		{
		}

		/// <summary>
		/// ctor - just passes back to base
		/// </summary>
		/// <param name="message"></param>
		public InvalidTaxProvisionEstimateException (string message) : base( message )
		{
		}

		/// <summary>
		/// Creates a new <see cref="UPDException"/> instance. This is the 3rd constructor,
		/// in accordance with the best practice template
		/// </summary>
		/// <param name="message">Diagnostic message.</param>
		/// <param name="inner">The inner exception to be wrapped</param>
		public InvalidTaxProvisionEstimateException (string message, Exception inner) : base ( message, inner )
		{
		}

		/// <summary>
		/// Creates a new <see cref="UPDException"/> instance. This is the final required constructor which
		/// makes sure you can create your custom data from the serialization info.
		/// </summary>
		/// <param name="info">The deserialization info</param>
		/// <param name="context">The deserialization context</param>
		protected InvalidTaxProvisionEstimateException (SerializationInfo info, StreamingContext context) : base( info, context )
		{
		}
	}

	/// <summary>
	/// An exception to be used if data in a Revaluation factor is not valid (for example when attempting to save)
	/// </summary>
	[Serializable]
	public class InvalidScalingFactorException : InvalidFactorException
	{
		/// <summary>
		/// ctor - just passes back to base
		/// </summary>
		public InvalidScalingFactorException() : base()
		{
		}

		/// <summary>
		/// ctor - just passes back to base
		/// </summary>
		/// <param name="message"></param>
		public InvalidScalingFactorException (string message) : base( message )
		{
		}

		/// <summary>
		/// Creates a new <see cref="UPDException"/> instance. This is the 3rd constructor,
		/// in accordance with the best practice template
		/// </summary>
		/// <param name="message">Diagnostic message.</param>
		/// <param name="inner">The inner exception to be wrapped</param>
		public InvalidScalingFactorException (string message, Exception inner) : base ( message, inner )
		{
		}

		/// <summary>
		/// Creates a new <see cref="UPDException"/> instance. This is the final required constructor which
		/// makes sure you can create your custom data from the serialization info.
		/// </summary>
		/// <param name="info">The deserialization info</param>
		/// <param name="context">The deserialization context</param>
		protected InvalidScalingFactorException (SerializationInfo info, StreamingContext context) : base( info, context )
		{
		}
	}

}
