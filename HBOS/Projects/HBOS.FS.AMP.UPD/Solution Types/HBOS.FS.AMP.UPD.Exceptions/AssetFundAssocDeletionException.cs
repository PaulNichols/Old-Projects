using System;
using System.Runtime.Serialization;

namespace HBOS.FS.AMP.UPD.Exceptions
{
	/// <summary>
	/// An exception to be used if attempting to delete an asset fund which has associated funds
	/// </summary>
	[Serializable]
	public class AssetFundAssocDeletionException : UPDException
	{
		/// <summary>
		/// ctor - just passes back to base
		/// </summary>
		public AssetFundAssocDeletionException(): base()
		{
		}
		/// <summary>
		/// ctor - just passes back to base
		/// </summary>
		/// <param name="message"></param>
		public AssetFundAssocDeletionException (string message) : base( message )
		{
		}
		/// <summary>
		/// Creates a new <see cref="AssetFundAssocDeletionException"/> instance. This is the 3rd constructor,
		/// in accordance with the best practice template
		/// </summary>
		/// <param name="message">Diagnostic message.</param>
		/// <param name="inner">The inner exception to be wrapped</param>
		public AssetFundAssocDeletionException (string message, Exception inner) : base ( message, inner )
		{
		}

		/// <summary>
		/// Creates a new <see cref="AssetFundAssocDeletionException"/> instance. This is the final required constructor which
		/// makes sure you can create your custom data from the serialization info.
		/// </summary>
		/// <param name="info">The deserialization info</param>
		/// <param name="context">The deserialization context</param>
		protected AssetFundAssocDeletionException (SerializationInfo info, StreamingContext context) : base( info, context )
		{
		}


	}
}
