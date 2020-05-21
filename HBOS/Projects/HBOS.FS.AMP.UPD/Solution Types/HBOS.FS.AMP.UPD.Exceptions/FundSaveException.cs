using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

using HBOS.FS.Common.ExceptionManagement;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Exceptions
{
	/// <summary>
	/// The custom exception thrown when there has been an error updating funds and related details
	/// on the database.
	/// </summary>
	[Serializable]
	public class FundSaveException : UPDException
	{
		#region Local variables

		private const string m_exceptionInfoKey = "FundSaveExceptionInfo";

		private object m_fund; 

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new <see cref="FundSaveException"/> instance. This is the
		/// default constructor.
		/// </summary>
		public FundSaveException() 
		{
			T.E();
			T.X();
		}

		/// <summary>
		/// Creates a new <see cref="FundSaveException"/> instance. This is the equivalent
		/// of the default constructor, but enforces the provision of some database exception
		/// information.
		/// </summary>
		/// <param name="fund">The fund for which the attempted save failed.</param>
		public FundSaveException(object fund) 
		{
			T.E();
			m_fund = fund;
			T.X();
		}

		/// <summary>
		/// Creates a new <see cref="FundSaveException"/> instance. This enforces the
		/// provision of some database exception information and a diagnostic message.
		/// </summary>
		/// <param name="message">A diagnostic message.</param>
		/// <param name="fund">The fund for which the attempted save failed.</param>
		public FundSaveException(string message, object fund) : base(message)
		{
			T.E();
			m_fund = fund;
			T.X();
		}

		/// <summary>
		/// Creates a new <see cref="FundSaveException"/> instance. This enforces the
		/// provision of a diagnostic message and the originating exception to be wrapped as the
		/// inner exception.
		/// </summary>
		/// <param name="message">A diagnostic message.</param>
		/// <param name="inner">The inner exception to be wrapped.</param>
		public FundSaveException(string message, Exception inner) : base(message, inner)
		{
			T.E();
			T.X();
		}

		/// <summary>
		/// Creates a new <see cref="FundSaveException"/> instance. This enforces the
		/// provision of a diagnostic message, some database excpetion information and the originating
		/// exception to be wrapped as the inner exception.
		/// </summary>
		/// <param name="message">A diagnostic message.</param>
		/// <param name="fund">The fund for which the attempted save failed.</param>
		/// <param name="inner">The inner exception to be wrapped.</param>
		public FundSaveException(string message, object fund, Exception inner)
			: base (message, inner)
		{
			T.E();
			m_fund = fund;
			T.X();
		}

		/// <summary>
		/// Creates a new <see cref="FundSaveException"/> instance. This ensures that the
		/// custom data can be created from the serialisation info.
		/// </summary>
		/// <param name="info">The deserialisation info.</param>
		/// <param name="context">The deserialisation context.</param>
		protected FundSaveException(SerializationInfo info, StreamingContext context) : 
			base(info, context)
		{
			T.E();

			// Retrieve the custom data from the serialization info.
			m_fund = info.GetValue(m_exceptionInfoKey, typeof(object));

			T.X();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Override GetObjectData to ensure any custom data can be serialized
		/// </summary>
		/// <param name="info">The serialization info.</param>
		/// <param name="context">The serialization context.</param>
		[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			T.E();

			// Call the base class version first.
			base.GetObjectData(info, context); 
    
			// Append exception custom data to the serialization info class.
			info.AddValue(m_exceptionInfoKey, this.m_fund);

			T.X();
		}

		#endregion

		#region Properties

		/// <summary>
		/// The fund for which the attempted save failed.
		/// </summary>
		public object Fund
		{
			get 
			{ 
				return m_fund; 
			}
		}

		#endregion
	}
}
