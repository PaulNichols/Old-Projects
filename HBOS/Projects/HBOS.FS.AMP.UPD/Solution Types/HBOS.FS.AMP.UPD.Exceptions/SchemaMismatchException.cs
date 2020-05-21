using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Exceptions
{
	/// <summary>
	/// Exception raised when a column is not found in a returned query
	/// </summary>
	[Serializable]
	public class SchemaMismatchException : DatabaseException
	{
		private const string m_ColumnKey = "Column";

		private string m_Column;

		/// <summary>
		/// Creates a new <see cref="SchemaMismatchException"/> instance. This is the equivalent of the
		/// default constructor, but enforces the provision of some custom exception information.
		/// </summary>
		public SchemaMismatchException() : base()
		{
			T.E();
			m_Column = "";
			T.X();
		}

		/// <summary>
		/// Creates a new <see cref="SchemaMismatchException"/> instance. This is the equivalent of the
		/// default constructor, but enforces the provision of some custom exception information.
		/// </summary>
		/// <param name="column">The column not found.</param>
		public SchemaMismatchException(string column) : base()
		{
			T.E();
			m_Column = column;
			T.X();
		}

		/// <summary>
		/// Creates a new <see cref="SchemaMismatchException"/> instance. 
		/// </summary>
		/// <param name="message">Diagnostic message </param>
		/// <param name="column">The column not found.</param>
		public SchemaMismatchException(string message, string column) : base(message)
		{
			T.E();
			m_Column = column;
			T.X();
		}

		/// <summary>
		/// Creates a new <see cref="SchemaMismatchException"/> instance. This is the 3rd constructor,
		/// in accordance with the best practice template
		/// </summary>
		/// <param name="message">Diagnostic message.</param>
		/// <param name="inner">The inner exception to be wrapped</param>
		public SchemaMismatchException(string message, Exception inner) : base(message, inner)
		{
			T.E();
			m_Column = "";
			T.X();
		}

		/// <summary>
		/// Creates a new <see cref="SchemaMismatchException"/> instance. This is the 3rd constructor,
		/// in accordance with the best practice template
		/// </summary>
		/// <param name="message">Diagnostic message.</param>
		/// <param name="column">Some custom info.</param>
		/// <param name="inner">The inner exception to be wrapped</param>
		public SchemaMismatchException(string message, string column, Exception inner) :
			base(message, inner)
		{
			T.E();
			m_Column = column;
			T.X();
		}

		///
		/// <summary>
		/// Creates a new <see cref="SchemaMismatchException"/> instance. 
		/// </summary>
		/// <param name="message">Diagnostic message </param>
		/// <param name="databaseInfo">Some database info.</param>
		/// <param name="column">The column not found.</param>
		public SchemaMismatchException(string message, string databaseInfo, string column) : base(message, databaseInfo)
		{
			T.E();
			m_Column = column;
			T.X();
		}

		/// <summary>
		/// Creates a new <see cref="SchemaMismatchException"/> instance. This is the 3rd constructor,
		/// in accordance with the best practice template
		/// </summary>
		/// <param name="message">Diagnostic message.</param>
		/// <param name="column">Some custom info.</param>
		/// <param name="databaseInfo">Some database info.</param>
		/// <param name="inner">The inner exception to be wrapped</param>
		public SchemaMismatchException(string message, string databaseInfo, string column, Exception inner) :
			base(message, databaseInfo, inner)
		{
			T.E();
			m_Column = column;
			T.X();
		}


		/// <summary>
		/// Creates a new <see cref="SchemaMismatchException"/> instance. This is the final required constructor which
		/// makes sure you can create your custom data from the serialization info.
		/// </summary>
		/// <param name="info">The deserialization info</param>
		/// <param name="context">The deserialization context</param>
		protected SchemaMismatchException(SerializationInfo info, StreamingContext context) :
			base(info, context)
		{
			T.E();
			//Retrieve our custom data from the serialization info
			m_Column = info.GetString(m_ColumnKey);
			T.X();
		}

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
			if (info != null)
			{
				base.GetObjectData(info, context);
			}
			else
			{
				throw new ArgumentNullException("info", "info parameter was null");
			}


			//... then append your exception's custom data to the serialization info class
			info.AddValue(m_ColumnKey, m_Column);
			T.X();
		}

		/// <summary>
		/// Read-only access to the exception's custom data.
		/// </summary>
		public string Column
		{
			get { return m_Column; }
		}
	}

}