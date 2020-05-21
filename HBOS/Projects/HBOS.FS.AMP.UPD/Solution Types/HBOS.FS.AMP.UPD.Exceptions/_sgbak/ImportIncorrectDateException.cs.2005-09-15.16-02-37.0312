using System;
using System.Runtime.Serialization;

namespace HBOS.FS.AMP.UPD.Exceptions
{
	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	public class ImportIncorrectDateException : UPDException
	{
		private readonly DateTime expectedDate;
		private readonly DateTime fileDate;

		/// <summary>
		/// Gets the expected date.
		/// </summary>
		/// <value></value>
		public DateTime ExpectedDate
		{
			get { return expectedDate; }
		}

		/// <summary>
		/// Gets the file date.
		/// </summary>
		/// <value></value>
		public DateTime FileDate
		{
			get { return fileDate; }
		}

		/// <summary>
		/// ctor - just passes back to base
		/// </summary>
		public ImportIncorrectDateException(): base()
		{
		}
		/// <summary>
		/// ctor - just passes back to base
		/// </summary>
		/// <param name="message"></param>
		/// <param name="expectedDate"></param>
		/// <param name="fileDate"></param>
		public ImportIncorrectDateException (string message,DateTime fileDate,DateTime expectedDate) : base( String.Format(message,new object[]{fileDate,expectedDate}) )
		{
			this.expectedDate = expectedDate;
			this.fileDate = fileDate;
		}
		/// <summary>
		/// Creates a new <see cref="ImportIncorrectDateException"/> instance. This is the 3rd constructor,
		/// in accordance with the best practice template
		/// </summary>
		/// <param name="message">Diagnostic message.</param>
		/// <param name="inner">The inner exception to be wrapped</param>
		public ImportIncorrectDateException (string message, Exception inner) : base ( message, inner )
		{
		}

		/// <summary>
		/// Creates a new <see cref="ImportIncorrectDateException"/> instance. This is the final required constructor which
		/// makes sure you can create your custom data from the serialization info.
		/// </summary>
		/// <param name="info">The deserialization info</param>
		/// <param name="context">The deserialization context</param>
		protected ImportIncorrectDateException (SerializationInfo info, StreamingContext context) : base( info, context )
		{
		}


	}
}
