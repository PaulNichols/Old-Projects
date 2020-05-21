using System;
using System.Data;
using System.Data.SqlClient;

using HBOS.FS.AMP.Data.Persister;
using HBOS.FS.AMP.Data.Persister.Interface;
using HBOS.FS.AMP.Data.Adapter;
using HBOS.FS.Common.ExceptionManagement;
using HBOS.FS.AMP.UPD.Exceptions;

using Microsoft.ApplicationBlocks.Data;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// ImportMarketIndexSqlPersister - Likevv SqlWithParentPersister but needs to remember the currencyCode value
	/// </summary>
	public class ImportMarketIndexSqlPersister : SqlWithParentPersister
	{
		#region Variables

		private string m_companyCode;
		private DateTime m_valuationPoint;

		#endregion

		#region Constructor

		/// <summary>
		/// Create a ImportMarketIndexSqlPersister
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="persistingStoredProcedure"></param>
		/// <param name="parentParameterName"></param>
		/// <param name="parentId"></param>
		/// <param name="companyCode"></param>
		/// <param name="valuationPoint"></param>
		public ImportMarketIndexSqlPersister( string connectionString , string persistingStoredProcedure , string parentParameterName , int parentId , string companyCode , DateTime valuationPoint ) : base( connectionString , persistingStoredProcedure , parentParameterName , parentId  )
		{
			m_companyCode = companyCode;
			m_valuationPoint = valuationPoint;
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
				base.SqlParameters[i + 3].Value = dataRow[i];
			}

			try
			{
				// persist the row
				SqlHelper.ExecuteNonQuery( base.SqlTransaction, CommandType.StoredProcedure, base.PersistingStoredProcedureName, base.SqlParameters);
			}
			catch (SqlException ex)
			{
				ExceptionManager.Publish( ex );
				
				if ( ex.Message.IndexOf( "CK_ImportedIndexValues_CheckValuationPoint" ) > -1 )
				{
					throw new DatabaseException ( String.Format( "Valuation point of {1} is future dated and cannot be imported. Details of the record are: {0}{1} {2} {3} {4}" , Environment.NewLine , m_valuationPoint , m_companyCode , dataRow[ 0 ].ToString() , dataRow[ 1 ].ToString()  ) , ex );
				}
				else
				{
					throw new DatabaseException ( String.Format( "Failed to save Stock Market Index data. Details of the record are: {0}{1} {2} {3} {4}" , Environment.NewLine , m_valuationPoint , m_companyCode , dataRow[ 0 ].ToString() , dataRow[ 1 ].ToString()  ) , ex );
				}
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
            
			base.SqlParameters = new SqlParameter[numParameters + 3];

			// Add the parent parameter
			SqlParameter parentParameter = new SqlParameter( "@" + base.ParentParameterName , SqlDbType.Int ) ;
			parentParameter.Value = base.ParentId;
			base.SqlParameters[0] = parentParameter;

			SqlParameter companyCode = new SqlParameter( "@sCompanyCode" , SqlDbType.VarChar , 10 ) ;
			companyCode.Value = m_companyCode;
			base.SqlParameters[1] = companyCode;

			SqlParameter valuationPoint = new SqlParameter( "@dtValuationPoint" , SqlDbType.DateTime ) ;
			valuationPoint.Value = m_valuationPoint;
			base.SqlParameters[2] = valuationPoint;

			// Add the real parameters
			for (int i = 0; i < numParameters; i++)
			{
				SqlParameter parameter = new SqlParameter();
				parameter.ParameterName = "@" + schema.Rows[i]["ColumnName"];
				parameter.SqlDbType = DBTypeConversion.SystemTypeToDbType(Type.GetType(schema.Rows[i]["DataType"].ToString()));
				base.SqlParameters[i + 3] = parameter;
			}
		}

		#endregion

	}
}
