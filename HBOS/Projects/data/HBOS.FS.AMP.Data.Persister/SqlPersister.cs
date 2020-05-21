using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

using HBOS.FS.AMP.Data.Persister.Interface;
using HBOS.FS.AMP.Data.Adapter;

using Microsoft.ApplicationBlocks.Data;

namespace HBOS.FS.AMP.Data.Persister
{
	/// <summary>
	/// SqlPersister - used to persist data to SQL Server as part of the Import process.
	/// </summary>
	public class SqlPersister : IPersistRow
	{
		#region Variables

		private bool m_PendingTrans = false;
		private string m_ConnectionString;
		private string m_PersistingStoredProcName;
		private string m_InitialisingStoredProcName;

		/// <summary>
		/// Parameters for persisting stored procedure call
		/// </summary>
		private SqlParameter[] m_Parameters = null; 
		private SqlConnection m_SqlConnection;
		private SqlTransaction m_SqlTransaction;

		#endregion

		#region Properties

		/// <summary>
		/// Access to the PersistingStoredProcedureName 
		/// </summary>
		protected string PersistingStoredProcedureName
		{
			get
			{
				return m_PersistingStoredProcName;
			}
		}

		/// <summary>
		/// Access to SqlParameters
		/// </summary>
		protected SqlParameter[] SqlParameters
		{
			get
			{
				return m_Parameters;
			}
			set
			{
				m_Parameters = value;
			}
		}
		
		/// <summary>
		/// Read access to SqlConnection
		/// </summary>
		protected SqlConnection SqlConnection
		{
			get
			{
				return m_SqlConnection;
			}
		}

		/// <summary>
		/// Read access to SqlTRansaction
		/// </summary>
		protected SqlTransaction SqlTransaction
		{
			get
			{
				return m_SqlTransaction;
			}
		}
		

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor which takes a connection string and the stored procedure to persist the data to the database
		/// </summary>
		/// <param name="connectionString">connectionstring with details for the appropriate database.</param>
		/// <param name="persistingStoredProcName">stored procedure to use to persist the data</param>
		public SqlPersister(string connectionString, string persistingStoredProcName)
		{
			m_ConnectionString = connectionString;
			m_PersistingStoredProcName = persistingStoredProcName;
		}

		/// <summary>
		/// Constructor which takes a connection string and the stored procedure to persist the data to the database. Also allows scope for an inititalisation stored procedure which could be used to clear down a table.
		/// </summary>
		/// <param name="connectionString">connectionstring with details for the appropriate database.</param>
		/// <param name="persistingStoredProcName">stored procedure to use to persist the data</param>
		/// <param name="initialisingStoredProcName">stored procedure to use to initialise the Import process.</param>
		public SqlPersister(string connectionString, string persistingStoredProcName, string initialisingStoredProcName) : this(connectionString, persistingStoredProcName)
		{
			m_InitialisingStoredProcName = initialisingStoredProcName;
		}

		#endregion

		#region ITransfer Members

		/// <summary>
		/// Initialise the transaction and run the initialising stored procedure if there is one.
		/// </summary>
		public void InitialiseTransfer()
		{
			// Open the connection and start the transaction
			m_SqlConnection = new SqlConnection(m_ConnectionString);
			m_SqlConnection.Open();
			m_SqlTransaction = m_SqlConnection.BeginTransaction();

			// note that we have started a transaction
			m_PendingTrans = true;

			if (null != m_InitialisingStoredProcName)
			{
				try
				{
					SqlHelper.ExecuteNonQuery(m_SqlTransaction, CommandType.StoredProcedure, m_InitialisingStoredProcName);
				}
				catch (System.Exception ex)
				{
					Debug.WriteLine(ex.Message);

					m_SqlTransaction.Rollback();

					if(m_SqlConnection != null)
						m_SqlConnection.Dispose();
            
					throw;
				}
				
			}
		}

		/// <summary>
		/// Returns true if there is a transaction in progress
		/// </summary>
		public bool IsTransferInProgress
		{
			get { return m_PendingTrans; }
		}

		/// <summary>
		/// Commit the transaction and close the connection.
		/// </summary>
		public void CompleteTransfer()
		{
			m_SqlTransaction.Commit();
			if(m_SqlConnection != null)
				m_SqlConnection.Dispose();
		}

		/// <summary>
		/// Rollback the transaction and close the connection
		/// </summary>
		public void CancelTransfer()
		{
			m_SqlTransaction.Rollback();
			if(m_SqlConnection != null)
				m_SqlConnection.Dispose();
		}

		/// <summary>
		/// Persist a data row to the appropriate database.
		/// </summary>
		/// <param name="dataRow">Details of the DataRow to persist.</param>
		/// <param name="schema">Definition of the data which is used to pack the parameters.</param>
		/// <example>
		///		<code lang="C#">
		///			((IPersistRow)m_dataRowPersisters[resultSetNum][i]).PersistRow(dataRow, dataDefinition);
		///		</code>
		/// </example>
		public virtual void PersistRow(string[] dataRow, DataTable schema)
		{

			if (null == m_Parameters)
			{
				constructParams(schema);
			}

			// Set the parameter values
			for (int i = 0; i < dataRow.Length; i++)
			{
				m_Parameters[i].Value = dataRow[i];
			}

			try
			{
				// persist the row
				SqlHelper.ExecuteNonQuery(m_SqlTransaction, CommandType.StoredProcedure, m_PersistingStoredProcName, m_Parameters);
			}
			catch (SqlException ex)
			{
				throw new ApplicationException("Failed to persist row (" + dataRow.ToString(), ex);
			}
			catch 
			{
				throw ;
			}
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Creates the instance level array of Sql Parameters to use when persisting a row
		/// </summary>
		/// <param name="schema"></param>
		private void constructParams(DataTable schema)
		{
			int numParameters = schema.Rows.Count;
            
			m_Parameters = new SqlParameter[numParameters];

			for (int i = 0; i < numParameters; i++)
			{
				SqlParameter parameter = new SqlParameter();
				parameter.ParameterName = "@" + schema.Rows[i]["ColumnName"];
				parameter.SqlDbType = DBTypeConversion.SystemTypeToDbType(Type.GetType(schema.Rows[i]["DataType"].ToString()));
				m_Parameters[i] = parameter;
			}
		}

		#endregion
	}
}
