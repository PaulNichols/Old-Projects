using System;
using System.Data;
using System.Data.SqlClient;
using HBOS.FS.Support.Tex;
using Microsoft.ApplicationBlocks.Data;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// Responsible for retrieving version information from the database
	/// </summary>
	public class VersionPersister: PersisterBase
	{
		/// <summary>
		/// Creates a new <see cref="VersionPersister"/> instance.
		/// </summary>
		public VersionPersister(string connectionString): base(connectionString)
		{
		}

		/// <summary>
		/// Verifies the version against the database.
		/// </summary>
		/// <param name="version">Version to verify.</param>
		/// <returns></returns>
		public bool VerifyVersion(Version version)
		{
			T.E();	
			bool result = false;
			SqlParameter[] parameters = null;
			try
			{
				parameters = new SqlParameter[5];

				parameters[0] = new SqlParameter("@majorVersion",SqlDbType.Int);
				parameters[0].Value = version.Major;
				parameters[1] = new SqlParameter("@minorVersion",SqlDbType.Int);
				parameters[1].Value = version.Minor;
				parameters[2] = new SqlParameter("@buildVersion",SqlDbType.Int);
				parameters[2].Value = version.Build;
				parameters[3] = new SqlParameter("@revisionVersion",SqlDbType.Int);
				parameters[3].Value = version.Revision;
				parameters[4] = new SqlParameter("@clientIsOk",SqlDbType.Bit);
				parameters[4].Direction = ParameterDirection.Output;

				SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure,"usp_ClientVersionCheck", parameters);

				result = (bool)parameters[4].Value;
			}
			catch (SqlException ex)
			{
				ThrowDBException(ex, ex.Procedure, parameters);
			}
		
			T.X();
			return result;
		}
	}
}
