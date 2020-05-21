using System;
using System.Data.SqlClient;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Exceptions
{
	/// <summary>
	/// Summary description for SecondLevelAuthoriseException.
	/// </summary>
	[Serializable]
	public class SecondLevelAuthorisationException : UPDException
	{
		private readonly object fund;

		/// <summary>
		/// Fund that has caused the exception
		/// </summary>
		public object Fund
		{
			get { return fund; }
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public SecondLevelAuthorisationException()
		{
			T.E();
			T.X();
		}

		/// <summary>
		/// Creates a new <see cref="SecondLevelAuthorisationException"/> instance. This enforces the
		/// provision of a diagnostic message and the originating exception to be wrapped as the
		/// inner exception.
		/// </summary>
		/// <param name="inner">The inner exception to be wrapped.</param>
		/// /// <param name="fund">Fund that has caused the exception</param>
		public SecondLevelAuthorisationException( object fund,SqlException inner) : base(inner.Message, inner)
		{
			this.fund = fund;
			T.E();
			T.X();
		}

	}
}