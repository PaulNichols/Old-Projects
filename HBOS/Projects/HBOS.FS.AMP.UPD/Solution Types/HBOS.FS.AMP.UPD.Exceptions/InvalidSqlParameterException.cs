using System;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.Security.Permissions;

using HBOS.FS.Common.ExceptionManagement;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Exceptions
{
	/// <summary>
	/// Exception thrown whenever a call to a stored procedure fails because of invalid parameters.
	/// </summary>
	[Serializable]
	public class InvalidSqlParameterException : DatabaseException
	{
		#region Local variables

		private const string m_procedureNameKey = "InvalidParameterExceptionProcedure";
		private const string m_parametersKey = "InvalidParameterExceptionParameters";

		private string m_procedureName;
		private SqlParameter[] m_parameters;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new <see cref="InvalidSqlParameterException"/> instance. This is the
		/// default constructor.
		/// </summary>
		public InvalidSqlParameterException() : base()
		{
			this.m_procedureName = string.Empty;
		}

		/// <summary>
		/// Creates a new <see cref="InvalidSqlParameterException"/> instance.  This is the
		/// equivalent of the default constructor but enforces the provision of an error message.
		/// </summary>
		/// <param name="message">A diagnostic message.</param>
		public InvalidSqlParameterException(string message) : base(message)
		{
			this.m_procedureName = string.Empty;
		}

		/// <summary>
		/// Creates a new <see cref="InvalidSqlParameterException"/> instance. This is the equivalent
		/// of the default constructor, but enforces the provision of some database exception
		/// information.
		/// </summary>
		/// <param name="databaseInfo">Some extra database exception info.</param>
		/// <param name="procedureName">The name of the stored procedure causing the violation.</param>
		/// <param name="parameters">The parameters passed to the stored procedure.</param>
		public InvalidSqlParameterException(string databaseInfo, string procedureName,
			SqlParameter[] parameters)
			: base(databaseInfo)
		{
			this.m_procedureName = procedureName;
			this.m_parameters = parameters;
		}

		/// <summary>
		/// Creates a new <see cref="InvalidSqlParameterException"/> instance. This enforces the
		/// provision of some database exception information and a diagnostic message.
		/// </summary>
		/// <param name="message">A diagnostic message.</param>
		/// <param name="databaseInfo">Some extra database exception info.</param>
		/// <param name="procedureName">The name of the stored procedure causing the violation.</param>
		/// <param name="parameters">The parameters passed to the stored procedure.</param>
		public InvalidSqlParameterException(string message, string databaseInfo, string procedureName,
			SqlParameter[] parameters)
			: base(message, databaseInfo)
		{
			this.m_procedureName = procedureName;
			this.m_parameters = parameters;
		}

		/// <summary>
		/// Creates a new <see cref="InvalidSqlParameterException"/> instance. This enforces the
		/// provision of a diagnostic message and the originating exception to be wrapped as the
		/// inner exception.
		/// </summary>
		/// <param name="message">A diagnostic message.</param>
		/// <param name="inner">The inner exception to be wrapped.</param>
		public InvalidSqlParameterException(string message, Exception inner) : base(message, inner)
		{
			this.m_procedureName = string.Empty;
		}

		/// <summary>
		/// Creates a new <see cref="InvalidSqlParameterException"/> instance. This enforces the
		/// provision of a diagnostic message, some database excpetion information and the originating
		/// exception to be wrapped as the inner exception.
		/// </summary>
		/// <param name="message">A diagnostic message.</param>
		/// <param name="databaseInfo">Some extra database exception info.</param>
		/// <param name="procedureName">The name of the stored procedure causing the violation.</param>
		/// <param name="parameters">The parameters passed to the stored procedure.</param>
		/// <param name="inner">The inner exception to be wrapped.</param>
		public InvalidSqlParameterException(string message, string databaseInfo,
			string procedureName, SqlParameter[] parameters, Exception inner)
			: base (message, databaseInfo, inner)
		{
			this.m_procedureName = procedureName;
			this.m_parameters = parameters;
		}

		/// <summary>
		/// Creates a new <see cref="InvalidSqlParameterException"/> instance. This ensures that the
		/// custom data can be created from the serialisation info.
		/// </summary>
		/// <param name="info">The deserialisation info.</param>
		/// <param name="context">The deserialisation context.</param>
		protected InvalidSqlParameterException(SerializationInfo info, StreamingContext context) : 
			base(info, context)
		{
			// Retrieve the custom data from the serialisation info.
			this.m_procedureName = info.GetString(m_procedureNameKey);
			this.m_parameters = info.GetValue(m_parametersKey, typeof(SqlParameter[])) as SqlParameter[];
		}

		#endregion

		#region Methods

		/// <summary>
		/// Override GetObjectData to ensure that any custom data can be serialised.
		/// </summary>
		/// <param name="info">The seriaslisation info.</param>
		/// <param name="context">The serialisation context.</param>
		[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			T.E();

			// Always call the base class version first...
			base.GetObjectData(info, context); 
    
			// ... then append the custom data to the serialisation info class.
			info.AddValue(m_procedureNameKey, this.m_procedureName);
			info.AddValue(m_parametersKey, this.m_parameters);

			T.X();
		}

		#endregion

		#region Properties

		/// <summary>
		/// The name of the stored procedure that resulted in the constraint violation.
		/// </summary>
		public string ProcedureName
		{
			get
			{
				return this.m_procedureName;
			}
		}

		/// <summary>
		/// The parameters being passed to the stored procedure when the violation occurred.
		/// </summary>
		public SqlParameter[] Parameters
		{
			get
			{
				return this.m_parameters;
			}
		}

		#endregion
	}
}