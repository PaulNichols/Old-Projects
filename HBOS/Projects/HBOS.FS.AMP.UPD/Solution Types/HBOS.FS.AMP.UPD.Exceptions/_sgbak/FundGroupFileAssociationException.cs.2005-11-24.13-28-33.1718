using System;
using System.Data.SqlClient;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Exceptions
{
	/// <summary>
	/// Summary description for SecondLevelAuthoriseException.
	/// </summary>
	[Serializable]
	public class FundGroupFileAssociationException : UPDException
	{
		private readonly object fundGroup;
		private readonly object file;

		/// <summary>
		/// Fund that has caused the exception
		/// </summary>
		public object FundGroup
		{
			get { return fundGroup; }
		}

		/// <summary>
		/// The File the fund group was being associated to at the time of the exception
		/// </summary>
		public object File
		{
			get { return file; }
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public FundGroupFileAssociationException()
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
		/// <param name="fundGroup">Fund Group that has caused the exception</param>
		/// <param name="file"></param>
		public FundGroupFileAssociationException( object fundGroup,object file,SqlException inner) : base(inner.Message, inner)
		{
			this.fundGroup = fundGroup;
			this.file=file;
			T.E();
			T.X();
		}

	}
}
