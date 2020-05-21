using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Exceptions
{
	/// <summary>
	/// The custom exception thrown when there has been an error loading funds from the database.
	/// </summary>
	[Serializable]
	public class FundLoadException : UPDException
	{
		#region Local variables

		private const string m_exceptionInfoKey = "FundLoadExceptionInfo";

		private string m_fundLoadExceptionInfo; 

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new <see cref="FundLoadException"/> instance. This is the
		/// default constructor.
		/// </summary>
		public FundLoadException() 
		{
			T.E();
			m_fundLoadExceptionInfo = string.Empty;
			T.X();
		}

		/// <summary>
		/// Creates a new <see cref="FundLoadException"/> instance. This is the equivalent
		/// of the default constructor, but enforces the provision of some database exception
		/// information.
		/// </summary>
		/// <param name="exceptionInfo">A string representing some form of exception info.</param>
		public FundLoadException(string exceptionInfo) 
		{
			T.E();
			m_fundLoadExceptionInfo = exceptionInfo;
			T.X();
		}

		/// <summary>
		/// Creates a new <see cref="FundLoadException"/> instance. This enforces the
		/// provision of some database exception information and a diagnostic message.
		/// </summary>
		/// <param name="message">A diagnostic message.</param>
		/// <param name="exceptionInfo">A string representing some form of exception info.</param>
		public FundLoadException(string message, string exceptionInfo) : base(message)
		{
			T.E();
			m_fundLoadExceptionInfo = exceptionInfo; 
			T.X();
		}

		/// <summary>
		/// Creates a new <see cref="FundLoadException"/> instance. This enforces the
		/// provision of a diagnostic message and the originating exception to be wrapped as the
		/// inner exception.
		/// </summary>
		/// <param name="message">A diagnostic message.</param>
		/// <param name="inner">The inner exception to be wrapped.</param>
		public FundLoadException(string message, Exception inner) : base(message, inner)
		{
			T.E();
			m_fundLoadExceptionInfo = string.Empty;
			T.X();
		}

		/// <summary>
		/// Creates a new <see cref="FundLoadException"/> instance. This enforces the
		/// provision of a diagnostic message, some database excpetion information and the originating
		/// exception to be wrapped as the inner exception.
		/// </summary>
		/// <param name="message">A diagnostic message.</param>
		/// <param name="exceptionInfo">A string representing some form of exception info.</param>
		/// <param name="inner">The inner exception to be wrapped.</param>
		public FundLoadException(string message, string exceptionInfo, Exception inner)
			: base (message, inner)
		{
			T.E();
			m_fundLoadExceptionInfo = exceptionInfo;
			T.X();
		}

		/// <summary>
		/// Creates a new <see cref="FundLoadException"/> instance. This ensures that the
		/// custom data can be created from the serialisation info.
		/// </summary>
		/// <param name="info">The deserialisation info.</param>
		/// <param name="context">The deserialisation context.</param>
		protected FundLoadException(SerializationInfo info, StreamingContext context) : 
			base(info, context)
		{
			T.E();
			//Retrieve our custom data from the serialization info
			if (info != null)
			{
				m_fundLoadExceptionInfo = info.GetString(m_exceptionInfoKey);
			}
			T.X();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Override GetObjectData to ensure any custom data can be serialized
		/// </summary>
		/// <param name="info">The serialization info.</param>
		/// <param name="context">The serialization context.</param>
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter=true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			T.E();
			//Always call the base class version first...
			base.GetObjectData(info, context); 
    
			//... then append your exception's custom data to the serialization info class

			if (info != null)
			{
				info.AddValue(m_exceptionInfoKey, m_fundLoadExceptionInfo);
			}
			else
			{
				throw new ArgumentNullException("info","info parameter was null")	;
			}
			
			T.X();
		}

		#endregion

		#region Properties

		/// <summary>
		/// Read-only access to the exception's custom data.
		/// </summary>
		public string DatabaseInfo
		{
			get 
			{ 
				return m_fundLoadExceptionInfo; 
			}
		}

		#endregion
	}
}
