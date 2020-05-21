using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Exceptions
{
	/// <summary>
	/// The custom exception thrown when there has been an error updating users and related details
	/// on the database.
	/// </summary>
	[Serializable]
	public class UsersSaveException : UPDException
	{
		#region Local variables

		private const string m_exceptionInfoKey = "UserSaveExceptionInfo";

		private object m_User;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new <see cref="UsersSaveException"/> instance. This is the
		/// default constructor.
		/// </summary>
		public UsersSaveException()
		{
			T.E();
			T.X();
		}

		/// <summary>
		/// Creates a new <see cref="UsersSaveException"/> instance. This is the equivalent
		/// of the default constructor, but enforces the provision of some database exception
		/// information.
		/// </summary>
		/// <param name="user">The User for which the attempted save failed.</param>
		public UsersSaveException(object user)
		{
			T.E();
			m_User = Users;
			T.X();
		}

		/// <summary>
		/// Creates a new <see cref="UsersSaveException"/> instance. This enforces the
		/// provision of some database exception information and a diagnostic message.
		/// </summary>
		/// <param name="message">A diagnostic message.</param>
		/// <param name="user">The User for which the attempted save failed.</param>
		public UsersSaveException(string message, object user) : base(message)
		{
			T.E();
			m_User = Users;
			T.X();
		}

		/// <summary>
		/// Creates a new <see cref="UsersSaveException"/> instance. This enforces the
		/// provision of a diagnostic message and the originating exception to be wrapped as the
		/// inner exception.
		/// </summary>
		/// <param name="message">A diagnostic message.</param>
		/// <param name="inner">The inner exception to be wrapped.</param>
		public UsersSaveException(string message, Exception inner) : base(message, inner)
		{
			T.E();
			T.X();
		}

		/// <summary>
		/// Creates a new <see cref="UsersSaveException"/> instance. This enforces the
		/// provision of a diagnostic message, some database excpetion information and the originating
		/// exception to be wrapped as the inner exception.
		/// </summary>
		/// <param name="message">A diagnostic message.</param>
		/// <param name="user">The User for which the attempted save failed.</param>
		/// <param name="inner">The inner exception to be wrapped.</param>
		public UsersSaveException(string message, object user, Exception inner)
			: base(message, inner)
		{
			T.E();
			m_User = Users;
			T.X();
		}

		/// <summary>
		/// Creates a new <see cref="UsersSaveException"/> instance. This ensures that the
		/// custom data can be created from the serialisation info.
		/// </summary>
		/// <param name="info">The deserialisation info.</param>
		/// <param name="context">The deserialisation context.</param>
		protected UsersSaveException(SerializationInfo info, StreamingContext context) :
			base(info, context)
		{
			T.E();

			// Retrieve the custom data from the serialization info.
			m_User = info.GetValue(m_exceptionInfoKey, typeof (object));

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

			// Call the base class version first.
			base.GetObjectData(info, context);

			// Append exception custom data to the serialization info class.
			info.AddValue(m_exceptionInfoKey, this.m_User);

			T.X();
		}

		#endregion

		#region Properties

		/// <summary>
		/// The Users for which the attempted save failed.
		/// </summary>
		public object Users
		{
			get { return m_User; }
		}

		#endregion
	}
}