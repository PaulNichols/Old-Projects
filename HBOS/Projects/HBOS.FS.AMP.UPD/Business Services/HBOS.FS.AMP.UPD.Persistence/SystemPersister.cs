using System;
using System.Data;
using System.Data.SqlClient;
using HBOS.FS.Support.Tex;
using Microsoft.ApplicationBlocks.Data;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// Summary description for SystemPersister.
	/// </summary>
	public class SystemPersister : PersisterBase
	{
		/// <summary>
		/// Creates a new <see cref="SystemPersister"/> instance.
		/// </summary>
		public SystemPersister(string connectionString) : base(connectionString)
		{
		}

		/// <summary>
		/// Gets the server date time.
		/// </summary>
		/// <returns></returns>
		public DateTime GetServerDateTime()
		{
			string loadSp = "usp_DateTimeGet";

			try
			{
			
				return (DateTime) SqlHelper.ExecuteScalar(
					this.ConnectionString,
					CommandType.StoredProcedure,
					loadSp);
			}
			catch (SqlException ex)
			{
				ThrowDBException(ex, loadSp);
			}
			finally
			{
				T.X();
			}

			return new DateTime(0);
		}

		/// <summary>
		/// Gets the next working day.
		/// </summary>
		/// <param name="today">Today.</param>
		/// <returns></returns>
		public  DateTime GetNextWorkingDay(DateTime today)
		{
			string loadSp = "usp_GetNextWorkingDate";

			try
			{
				SqlParameter[] param;
				param = new SqlParameter[1];
				param[0] = new SqlParameter ( "@currentDate", SqlDbType.DateTime );
				param[0].Value = today;

				return (DateTime) SqlHelper.ExecuteScalar(
					ConnectionString,
					CommandType.StoredProcedure,
					loadSp,param);
			}
			catch (SqlException ex)
			{
				ThrowDBException(ex, loadSp);
			}
			finally
			{
				T.X();
			}

			return new DateTime(0);
		}
	}
}