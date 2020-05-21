using System;
using System.Data;
using System.Data.SqlClient;

using HBOS.FS.AMP.Data.Adapter;

using Microsoft.ApplicationBlocks.Data;

namespace HBOS.FS.AMP.Data.Persister
{
	/// <summary>
	/// SqlWithParentPersister - SqlPersister which understands that it has a parent
	/// </summary>
	public class SqlWithParentPersister : SqlPersister
	{
		#region Variables

		private long m_parentId;
		private string m_parentParameterName;
		bool m_parentIdIsLong;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor for SqlWithParentPersister
		/// </summary>
		/// <param name="connectionString">Connection string for database</param>
		/// <param name="persistingStoredProcedure">Persisting stored procedure</param>
		/// <param name="parentParameterName">Parent Parameter Name - needed for packing the parameters</param>
		/// <param name="parentId">Parent Id</param>
		public SqlWithParentPersister( string connectionString , string persistingStoredProcedure , string parentParameterName , int parentId ) : base( connectionString , persistingStoredProcedure )
		{
			m_parentParameterName = parentParameterName;
			m_parentId = parentId;
			m_parentIdIsLong = false;
		}

		/// <summary>
		/// Constructor for SqlWithParentPersister
		/// </summary>
		/// <param name="connectionString">Connection string for database</param>
		/// <param name="persistingStoredProcedure">Persisting stored procedure</param>
		/// <param name="parentParameterName">Parent Parameter Name - needed for packing the parameters</param>
		/// <param name="parentId">Parent Id</param>
		public SqlWithParentPersister( string connectionString , string persistingStoredProcedure , string parentParameterName , long parentId ) : base( connectionString , persistingStoredProcedure )
		{
			m_parentParameterName = parentParameterName;
			m_parentId = parentId;
			m_parentIdIsLong = true;
		}


		#endregion

		#region Properties

		/// <summary>
		/// Access to the parent parameter name
		/// </summary>
		protected string ParentParameterName
		{
			get
			{
				return m_parentParameterName;
			}
		}

		/// <summary>
		/// Access to the parent ID
		/// </summary>
		protected long ParentId
		{
			get
			{
				return m_parentId;
			}
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Persist the data row
		/// </summary>
		/// <param name="dataRow"></param>
		/// <param name="schema"></param>
		public override void PersistRow(string[] dataRow, DataTable schema)
		{

			if (null == base.SqlParameters)
			{
				constructParams(schema);
			}

			// Set the parameter values
			for (int i = 0; i < dataRow.Length; i++)
			{
				base.SqlParameters[i + 1].Value = dataRow[i];
			}

			try
			{
				// persist the row
				SqlHelper.ExecuteNonQuery( base.SqlTransaction, CommandType.StoredProcedure, base.PersistingStoredProcedureName, base.SqlParameters);
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
			SqlParameter parentParameter;
            
			base.SqlParameters = new SqlParameter[numParameters + 1];

			// Add the parent parameter
			if (m_parentIdIsLong)
			{
				parentParameter = new SqlParameter( "@" + m_parentParameterName , SqlDbType.BigInt ) ;				
			}
			else
			{
				parentParameter = new SqlParameter( "@" + m_parentParameterName , SqlDbType.Int ) ;
			}
			parentParameter.Value = m_parentId;
			base.SqlParameters[0] = parentParameter;

			// Add the real parameters
			for (int i = 0; i < numParameters; i++)
			{
				SqlParameter parameter = new SqlParameter();
				parameter.ParameterName = "@" + schema.Rows[i]["ColumnName"];
				parameter.SqlDbType = DBTypeConversion.SystemTypeToDbType(Type.GetType(schema.Rows[i]["DataType"].ToString()));
				base.SqlParameters[i + 1] = parameter;
			}
		}

		#endregion
	}
}
