using System.Data.SqlClient;
using HBOS.FS.AMP.UPD.Types.Funds;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// Summary description for FundReleasePersister.
	/// </summary>
	public class FundReleasePersister : FundPersister
	{
		/// <summary>
		/// Creates a new <see cref="FundLookUpPersister"/> instance.
		/// Constructor used to initialise the ConnectionString property.
		/// </summary>
		/// <param name="connectionString">The connection string to use for data access.</param>
		public FundReleasePersister(string connectionString) : base(connectionString)
		{
		}


	
	}
}