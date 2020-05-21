using System;
using System.Data;
using System.Data.SqlClient;
using HBOS.FS.AMP.Data.Persister;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Types.Snapshot;
using HBOS.FS.Common.ExceptionManagement;
using HBOS.FS.Support.Tex;
using Microsoft.ApplicationBlocks.Data;

namespace HBOS.FS.AMP.UPD.Persistence.IDataRowPersisters
{
	/// <summary>
	/// Summary description for ImportWorkingSplitTableSqlPersister.
	/// </summary>
	public class ImportWorkingSplitTableSqlPersister: SqlWithParentPersister
	{
		//TODO : Remove temp transaction
		/// <summary>
		/// Should be temporary!
		/// This field has been added to get round the base transaction being readonly
		/// Due to the way Compsoite split imports needs to loop through the file line by line and only comitt 
		/// data when the whole composite fund is valid, it was found that posibbly the current methods of 
		/// persistance couldn't allow the transaction to be "passed around"
		/// </summary>
		public SqlTransaction tempTran;

		#region Parameter Position

		/// <summary>
		/// Parameter Positions for the temporary table
		/// </summary>
		private enum ParameterPosition : int
		{
			snapshotID = 0,
			primaryKey,
			secondaryKey,
			importedValue		
		}

		/// <summary>
		/// Standard column positions for Split files
		/// </summary>
		public enum ColumnPosition : int
		{
			/// <summary>
			/// Asset Fund (0)
			/// </summary>
			primaryKey = 0,
			/// <summary>
			/// LinkedFund Code (1)
			/// </summary>
			secondaryKey,
			/// <summary>
			/// Value (2)
			/// </summary>
			importedValue
		}

		#endregion

		#region Constructor
		/// <summary>
		/// 
		/// </summary>
		public ImportWorkingSplitTableSqlPersister(string connectionString, string persistingStoredProcedure, string parentParameterName, Snapshot snapshot) : base( connectionString , persistingStoredProcedure , parentParameterName , snapshot.Id )
		{
			//
			// TODO: Add constructor logic here
			//
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
			T.E();

			try
			{
				if (null == base.SqlParameters)
				{
					//set up the parameters collection
					//also set the value of the import id parameter
					constructParams();
				}

				//set the values of the parameters
				base.SqlParameters[ (int)ParameterPosition.primaryKey ].Value = dataRow[ (int)ColumnPosition.primaryKey ];
				base.SqlParameters[ (int)ParameterPosition.secondaryKey ].Value = dataRow[ (int)ColumnPosition.secondaryKey ];
				base.SqlParameters[ (int)ParameterPosition.importedValue ].Value = dataRow[ (int)ColumnPosition.importedValue ];

				try
				{
					// persist the row
					SqlHelper.ExecuteNonQuery( this.tempTran, CommandType.StoredProcedure, base.PersistingStoredProcedureName, base.SqlParameters);
				}
				catch (SqlException ex)
				{
					ExceptionManager.Publish( ex );
					T.DumpException( ex );

					switch ( ex.Number )
					{
						case (int)DatabaseError.ConstraintViolation:
						
						default:
							throw new DatabaseException("Failed to save a Working Split row.." , base.SqlTransaction.Connection.DataSource + " " + base.SqlTransaction.Connection.Database ,  ex);
					}
				}
				catch 
				{
					throw ;
				}
			}
			finally
			{
				T.X();
			}
		}
		
		#endregion

		#region Private Methods

		/// <summary>
		/// Creates the instance level array of Sql Parameters to use when persisting a row
		/// </summary>
		private void constructParams()
		{
			T.E();

			try
			{
				base.SqlParameters = new SqlParameter[ Enum.GetValues(typeof (ParameterPosition)).Length ];

				SqlParameter snapshotID= new SqlParameter( "@snapshotID" , SqlDbType.Int ) ;
				snapshotID.Value = base.ParentId;
				base.SqlParameters[ (int)ParameterPosition.snapshotID ] = snapshotID;

				SqlParameter primaryKey = new SqlParameter( "@primaryKey" , SqlDbType.VarChar,25 ) ;
				base.SqlParameters[ (int)ParameterPosition.primaryKey ] = primaryKey;

				SqlParameter secondaryKey = new SqlParameter( "@secondaryKey" , SqlDbType.VarChar,25 ) ;
				base.SqlParameters[ (int)ParameterPosition.secondaryKey ] = secondaryKey;

				SqlParameter importedValue= new SqlParameter( "@importedValue" , SqlDbType.Decimal ) ;
				base.SqlParameters[ (int)ParameterPosition.importedValue ] = importedValue;
			}
			finally
			{
				T.X();
			}
		}
			#endregion
	}
}
