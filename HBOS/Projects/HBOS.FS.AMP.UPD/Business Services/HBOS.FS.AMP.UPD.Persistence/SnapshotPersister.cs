using System;
using System.Data;
using System.Data.SqlClient;
using HBOS.FS.AMP.UPD.Types.Snapshot;
using Microsoft.ApplicationBlocks.Data;

using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.Support.Tex;


namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// Summary description for SnapshotPersister.
	/// </summary>
	public class SnapshotPersister: PersisterBase
	{
		#region Public methods
		
		/// <summary>
		/// Creates a new <see cref="SnapshotPersister"/> instance.
		/// </summary>
		/// <param name="connectionString">Connection string.</param>
		public SnapshotPersister(string connectionString): base(connectionString)
		{
		}

		/// <summary>
		/// Activates all the data associated with the provided snapshotID
		/// </summary>
		/// <param name="snapshot">The snapshot to activate.</param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public void Activate(Snapshot snapshot)
		{
			T.E();
			try
			{
				executeCancelOrActivateStoredProc("usp_SnapshotActivate",snapshot.Id);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Activates all the data associated with the provided snapshotID using the provided transaction
		/// </summary>
		/// <param name="snapshot">The snapshot to activate.</param>
		/// <param name="txn">Transaction to use</param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public void Activate(Snapshot snapshot, SqlTransaction txn)
		{
			T.E();
			try
			{
				executeCancelOrActivateStoredProc("usp_SnapshotActivate",snapshot.Id,txn);
			}
			finally
			{
				T.X();
			}
		}


		/// <summary>
		/// Deletes all the data associated with the provided snapshotID and marks the batch as deleted
		/// </summary>
		/// <param name="snapshot">The snapshot to cancel.</param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public void Cancel(Snapshot snapshot)
		{
			T.E();
			try
			{
				executeCancelOrActivateStoredProc("usp_SnapshotCancel",snapshot.Id);
			}
			finally
			{
				T.X();
			}
		}


		/// <summary>
		/// Creates a new snapshot for an import process
		/// </summary>
		/// <param name="importFileName"></param>
		/// <param name="companyCode"></param>
		/// <param name="process">Specify a value for the Process</param>
		/// <returns></returns>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public Snapshot NewImportSnapshot(string importFileName ,string companyCode,SnapshotProcess process)
		{
			T.E();
			Snapshot result = createNewSnapshot(process,importFileName,companyCode,null);
			T.X();
			return result;
		}

		/// <summary>
		/// Creates a new snapshot for an import process using the passed transaction
		/// </summary>
		/// <param name="importFileName"></param>
		/// <param name="companyCode"></param>
		/// <param name="process">Specify the process value</param>
		/// <param name="transaction"></param>
		/// <returns></returns>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public Snapshot NewImportSnapshot(string importFileName ,string companyCode,SnapshotProcess process,SqlTransaction transaction)
		{
			T.E();
			Snapshot result = createNewSnapshot(process,importFileName,companyCode,transaction);
			T.X();
			return result;
		}

		/// <summary>
		/// Creates a new snapshot for a static data process
		/// </summary>
		/// <param name="companyCode"></param>
		/// <returns></returns>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public Snapshot NewStaticDataSnapshot(string companyCode)
		{
			T.E();
			Snapshot result = createNewSnapshot(SnapshotProcess.StaticData,string.Empty,companyCode,null);
			T.X();
			return result;
		}

		/// <summary>
		/// Creates a new snapshot for a static data process using the passed transaction
		/// </summary>
		/// <param name="companyCode"></param>
		/// <param name="transaction"></param>
		/// <returns></returns>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public Snapshot NewStaticDataSnapshot(string companyCode,SqlTransaction transaction)
		{
			T.E();
			Snapshot result = createNewSnapshot(SnapshotProcess.StaticData,string.Empty,companyCode,transaction);
			T.X();
			return result;
		}

		#endregion

		#region Private methods

		private void executeCancelOrActivateStoredProc(string sp, long snapshotID)
		{
			using (SqlConnection connection = new SqlConnection(this.ConnectionString))
			{
				connection.Open();
				using (SqlTransaction txn = connection.BeginTransaction())
				{
					try
					{
						executeCancelOrActivateStoredProc(sp,snapshotID,txn);
						txn.Commit();
					}
					catch
					{
						txn.Rollback();
						throw;
					}
				}
			}
			
		}
		
		private void executeCancelOrActivateStoredProc(string sp, long snapshotID, SqlTransaction txn)
		{
			SqlParameter[] parameters = new SqlParameter[1];

			try
			{
				try
				{
					parameters[0] = new SqlParameter("@iSnapshotID", SqlDbType.BigInt); 
					parameters[0].Value = snapshotID;
                    
					SqlHelper.ExecuteNonQuery(txn, CommandType.StoredProcedure, sp, parameters);
				}
				catch (SqlException ex)
				{
					ThrowDBException (ex, sp);
				}
			}
			finally
			{
				T.X();
			}

		}

		private char convertSnapshotProcessToChar(SnapshotProcess process)
		{
			char result;
			switch(process)
			{
				case SnapshotProcess.Import:
					result = 'I';
					break;
				case SnapshotProcess.StaticData:
					result = 'S';
					break;
				case SnapshotProcess.Report:
					result = 'R';
					break;
				default:
					throw new ApplicationException(string.Format("Cannot convert SnapshotProcess of {0} to database representation",process.ToString()));
			}
			return result;
		}

		/// <summary>
		/// Saves an Import Source and returns the inserted Identity.
		/// </summary>
		/// <param name="importFileName"></param>
		/// <param name="companyCode"></param>
		/// <param name="process"></param>
		/// <param name="transaction">Transaction to use</param> 
		/// <returns></returns>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		private Snapshot createNewSnapshot( SnapshotProcess process, string importFileName ,string companyCode, SqlTransaction transaction)
		{
			T.E();
			
			Snapshot result = null;
			
			const string sp = "usp_SnapshotCreate";
			SqlParameter[] parameters = new SqlParameter[7];
			
			try
			{
				parameters[0] = new SqlParameter("@cProcess",SqlDbType.Char,1);
				parameters[0].Value = convertSnapshotProcessToChar(process);

				parameters[1] = new SqlParameter("@sImportFilename",SqlDbType.VarChar,512);
				parameters[1].Value = importFileName;

				parameters[2] = new SqlParameter("@sCompanyCode",SqlDbType.Char,10);
				parameters[2].Value = companyCode;

				SqlParameter snapshotIdParam = new SqlParameter("@iSnapshotId",SqlDbType.BigInt);
				snapshotIdParam.Direction = ParameterDirection.Output;
				parameters[3] = snapshotIdParam;

				SqlParameter userIdParam = new SqlParameter("@sUserId",SqlDbType.VarChar,50);
				userIdParam.Direction = ParameterDirection.Output;
				parameters[4] = userIdParam;

				SqlParameter snapshotDateParam = new SqlParameter("@dSnapshotDate",SqlDbType.DateTime);
				snapshotDateParam.Direction = ParameterDirection.Output;
				parameters[5] = snapshotDateParam;

				SqlParameter timestampParam = new SqlParameter("@ts",SqlDbType.Timestamp);
				timestampParam.Direction = ParameterDirection.Output;
				parameters[6] = timestampParam;
					
		
				if (transaction == null)
					SqlHelper.ExecuteNonQuery( this.ConnectionString, CommandType.StoredProcedure, sp, parameters);	
				else
					SqlHelper.ExecuteNonQuery( transaction, CommandType.StoredProcedure, sp, parameters);					
				
				if (snapshotDateParam.Value != DBNull.Value)
				{
					result = new Snapshot(	(long)snapshotIdParam.Value,
						(string)userIdParam.Value,
						(DateTime)snapshotDateParam.Value,
						process,
						importFileName,
						companyCode,
						(byte[])timestampParam.Value);
				}
				
			}
	
			catch (SqlException ex)
			{
				ThrowDBException (ex, sp, parameters);
			}
			finally
			{
				T.X();
			}

			return result;			
		}


		#endregion
	}
}
