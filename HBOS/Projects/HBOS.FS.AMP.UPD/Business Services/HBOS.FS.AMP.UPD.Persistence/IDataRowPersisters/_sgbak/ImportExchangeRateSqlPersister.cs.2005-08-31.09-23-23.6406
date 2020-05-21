using System;
using System.Data;
using System.Data.SqlClient;
using HBOS.FS.AMP.Data.Persister;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Types.Snapshot;
using HBOS.FS.Common.ExceptionManagement;
using Microsoft.ApplicationBlocks.Data;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// ImportExchangeRateSqlPersister - Like SqlWithParentPersister but needs to remember the currencyCode value
	/// </summary>
	public class ImportExchangeRateSqlPersister : SqlWithParentPersister
	{
		#region Variables

		private string m_companyCode;

		#endregion

		#region Parameter Position

		/// <summary>
		/// Parameter Positions
		/// </summary>
		private enum ParameterPosition : int
		{
			SnapshotId = 0,
			CompanyCode,
			ValuationDate,
			CurrencyCode,
			ExchangeRate
		}

		/// <summary>
		/// Column Position in the currency exchange rate file
		/// </summary>
		public enum CurrencyExchangeRateColumnPosition : int
		{
			/// <summary>
			/// Valuation Date (0)
			/// </summary>
			ValuationDate = 0,
			/// <summary>
			/// Valuation Time (1)
			/// </summary>
			ValuationTime,
			/// <summary>
			/// Currency Code (2)
			/// </summary>
			CurrencyCode,
			/// <summary>
			/// Exchange Rate (3)
			/// </summary>
			ExchangeRate
		}


		#endregion

		#region Constructor

		/// <summary>
		/// Create a ImportExchangeRateSqlPersister
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="persistingStoredProcedure"></param>
		/// <param name="parentParameterName"></param>
		/// <param name="snapshot"></param>
		/// <param name="companyCode"></param>
		public ImportExchangeRateSqlPersister( string connectionString , string persistingStoredProcedure , string parentParameterName , Snapshot snapshot , string companyCode ) : base( connectionString , persistingStoredProcedure , parentParameterName , snapshot.Id  )
		{
			m_companyCode = companyCode;
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
				constructParams();
			}

			// Set the parameter values
			int dayNumber = int.Parse( dataRow[ (int)CurrencyExchangeRateColumnPosition.ValuationDate ].Substring(0 , 2) ) ;
			int monthNumber = int.Parse( dataRow[ (int)CurrencyExchangeRateColumnPosition.ValuationDate ].Substring(2 , 2) );
			int yearNumber = int.Parse( dataRow[ (int)CurrencyExchangeRateColumnPosition.ValuationDate ].Substring(4 , 4) );
			int hourNumber = int.Parse( dataRow[ (int)CurrencyExchangeRateColumnPosition.ValuationTime ].Substring(0 , 2) );
			int minuteNumber = int.Parse( dataRow[ (int)CurrencyExchangeRateColumnPosition.ValuationTime ].Substring(3 , 2) );

			// End of day is coming through as "24:00" so we need to subtract a minute
			if ( hourNumber == 24 && minuteNumber == 00 )
			{
				hourNumber = 23;
				minuteNumber = 59;
			}

			DateTime valuationDate = new DateTime( yearNumber , monthNumber , dayNumber , hourNumber , minuteNumber , 0 );

			base.SqlParameters[ (int)ParameterPosition.ValuationDate ].Value = valuationDate;
			base.SqlParameters[ (int)ParameterPosition.CurrencyCode ].Value = dataRow[ (int)CurrencyExchangeRateColumnPosition.CurrencyCode ];
			base.SqlParameters[ (int)ParameterPosition.ExchangeRate ].Value = dataRow[ (int)CurrencyExchangeRateColumnPosition.ExchangeRate ];

			try
			{
				// persist the row
				SqlHelper.ExecuteNonQuery( base.SqlTransaction, CommandType.StoredProcedure, base.PersistingStoredProcedureName, base.SqlParameters);

			}
			catch (SqlException ex)
			{
				ExceptionManager.Publish( ex );
				
				if ( ex.Message.IndexOf( "CK_CurrencyRates_CheckValuationPoint" ) > -1 )
				{
					throw new DatabaseException ( String.Format( "Valuation point of {1} is future dated and cannot be imported. Details of the record are: {0}{1} {2} {3}" , Environment.NewLine , valuationDate , dataRow[ (int)CurrencyExchangeRateColumnPosition.CurrencyCode ].ToString() , dataRow[ (int)CurrencyExchangeRateColumnPosition.ExchangeRate ].ToString()  ) , ex );
				}
				else
				{
					throw new DatabaseException ( String.Format( "Failed to save Exchange Rate record. Details of the records are: {0}{1} {2} {3}" , Environment.NewLine, valuationDate , dataRow[ (int)CurrencyExchangeRateColumnPosition.CurrencyCode ].ToString() , dataRow[ (int)CurrencyExchangeRateColumnPosition.ExchangeRate ].ToString()  ) , ex );
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
		private void constructParams()
		{
            
			base.SqlParameters = new SqlParameter[ 5 ];

			// Add the parent parameter
			SqlParameter parentParameter = new SqlParameter( "@iSnapshotID" , SqlDbType.Int ) ;
			parentParameter.Value = base.ParentId;
			base.SqlParameters[ (int)ParameterPosition.SnapshotId ] = parentParameter;

			SqlParameter companyCode = new SqlParameter( "@sCompanyCode" , SqlDbType.VarChar , 10 ) ;
			companyCode.Value = m_companyCode;
			base.SqlParameters[ (int)ParameterPosition.CompanyCode ] = companyCode;

			SqlParameter valuationDate = new SqlParameter( "@dtValuationDate" , SqlDbType.DateTime ) ;
			base.SqlParameters[ (int)ParameterPosition.ValuationDate ] = valuationDate;

			SqlParameter currencyCode = new SqlParameter( "@sCurrencyCode" , SqlDbType.VarChar , 10 ) ;
			base.SqlParameters[ (int)ParameterPosition.CurrencyCode ] = currencyCode;

			SqlParameter exchangeRate = new SqlParameter( "@dExchangeRate" , SqlDbType.Decimal) ;
			base.SqlParameters[ (int)ParameterPosition.ExchangeRate ] = exchangeRate;
		}

		#endregion

	}
}
