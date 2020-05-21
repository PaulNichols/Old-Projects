using System;
using System.Data;
using System.Data.SqlClient;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Types.Snapshot;
using HBOS.FS.Support.Tex;

using Microsoft.ApplicationBlocks.Data;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// ImportPersister - used to check if this is a fresh file Import
	/// </summary>
	public class ImportPersister : PersisterBase
	{
		#region Constructors

		/// <summary>
		/// Constructor used to initialise the ConnectionString property.
		/// </summary>
		/// <param name="connectionString"></param>
		public ImportPersister(string connectionString) : base(connectionString)
		{
		}

		#endregion

		#region Public Methods


		/// <summary>
		/// Clears the working table.
		/// </summary>
		/// <param name="snapShotId">Snap shot id.</param>
		/// <param name="transaction"></param>
		public void ClearWorkingTable(long snapShotId,SqlTransaction transaction)
		{
			
			T.E();		
			const string sp = "usp_ImportedSplitWorkingTableClear";
			SqlParameter[] parameters = new SqlParameter[1];
			try
			{
				// Set up the parameters.
				parameters[0] = new SqlParameter("@SnapshotID", SqlDbType.BigInt); 
				parameters[0].Value = snapShotId;
			
				SqlHelper.ExecuteNonQuery( this.ConnectionString, CommandType.StoredProcedure, sp, parameters);
			}
			catch (SqlException ex)
			{
				ThrowDBException (ex, sp, parameters);
			}
			finally
			{
				T.X();
			}
			
		}
		/// <summary>
		/// Verify that this is a new file (has not been previously saved)
		/// </summary>
		/// <param name="importFileName"></param>
		/// <param name="companyCode"></param>
		/// <returns></returns>
		/// <exception cref="DatabaseException">Unable to load company</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		public bool VerifyNewFile( string importFileName, string companyCode )
		{
			T.E();		
			const string sp = "usp_ImportedFileVerifyNewFile";
			SqlParameter[] parameters = new SqlParameter[3];
			bool result = false;
			try
			{
				// Set up the parameters.
				parameters[0] = new SqlParameter("@sImportFileName", SqlDbType.VarChar, 512); 
				parameters[0].Value = importFileName;
				parameters[1] = new SqlParameter("@companyCode", SqlDbType.Char, 10); 
				parameters[1].Value = companyCode;

				parameters[2] = new SqlParameter("@bNewFile", SqlDbType.Bit); 
				parameters[2].Direction = ParameterDirection.InputOutput;

				SqlHelper.ExecuteNonQuery( this.ConnectionString, CommandType.StoredProcedure, sp, parameters);
				result = (bool)parameters[2].Value;
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


		/// <summary>
		/// Checks the database for authorised prices.
		/// </summary>
		/// <param name="CompanyCode">The Company code to check.</param>
		/// <returns>Whether prices have been authorised or not</returns>
		/// <exception cref="DatabaseException">Unable to load company</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		public bool CheckForAuthorisedPrices(string CompanyCode)
		{
			T.E();
			const string sp = "usp_HBOSCompanyCheckForAuthorisedPrice";
			SqlParameter[] parameters = new SqlParameter[2];
			bool result = false;
			try
			{
				// Set up the parameters.
				parameters[0] = new SqlParameter("@sCompanyCode", SqlDbType.VarChar, 10); 
				parameters[0].Value = CompanyCode;

				parameters[1] = new SqlParameter("@bHasAuthorisedPrices", SqlDbType.Bit); 
				parameters[1].Direction = ParameterDirection.InputOutput;

				SqlHelper.ExecuteNonQuery( this.ConnectionString, CommandType.StoredProcedure, sp, parameters);
				result = (bool)parameters[1].Value;
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

		/// <summary>
		/// Get the imported market indices for this snapshot
		/// </summary>
		/// <param name="snapshot"></param>
		/// <returns></returns>
		/// <exception cref="DatabaseException">Unable to load company</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		public DataTable GetImportedMarketIndices(Snapshot snapshot)
		{
			T.E();
			DataTable dtMarketIndices = null;
			const string sp = "usp_ImportedIndexValuesListForSnapshotId";
			SqlParameter[] parameters = new SqlParameter[1];

			try
			{
				parameters[0] = new SqlParameter("@iSnapshotId", SqlDbType.BigInt); 
				parameters[0].Value = snapshot.Id;

				DataSet dsMarketIndices = 
					SqlHelper.ExecuteDataset(this.ConnectionString,
					CommandType.StoredProcedure,
					sp, parameters);

				dtMarketIndices = dsMarketIndices.Tables[0];                   
               
				//disassociate from this dataset so ds can be garbage collected
				//and table can be added to different dataset if necessary
				dsMarketIndices.Tables.RemoveAt(0); 

				if (dtMarketIndices.Rows.Count == 0)
					dtMarketIndices=null;
			}
			catch (SqlException ex)
			{
				ThrowDBException (ex, sp, parameters);
			}
			finally
			{
				T.X();
			}
			return (dtMarketIndices);
		}

		/// <summary>
		/// Get the imported currency rates for this snapshot
		/// </summary>
		/// <param name="snapshot"></param>
		/// <returns></returns>
		/// <exception cref="DatabaseException">Unable to load company</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		public DataTable GetImportedCurrencyRates(Snapshot snapshot)
		{
			T.E();
			const string sp = "usp_CurrencyRatesListForSnapshotId";
			DataTable dtCurrencyRates = null;
			SqlParameter[] parameters = new SqlParameter[1];

			try
			{
				parameters[0] = new SqlParameter("@iSnapshotId", SqlDbType.BigInt);
				parameters[0].Value = snapshot.Id;

				DataSet dsMarketIndices = 
					SqlHelper.ExecuteDataset(this.ConnectionString,
					CommandType.StoredProcedure,
					sp,
					parameters);

				dtCurrencyRates = dsMarketIndices.Tables[0];                                  

				//disassociate from this dataset so ds can be garbage collected
				//and table can be added to different dataset if necessary
				dsMarketIndices.Tables.RemoveAt(0); 

				if (dtCurrencyRates.Rows.Count == 0)
					dtCurrencyRates=null;
			}
			catch (SqlException ex)
			{
				ThrowDBException (ex, sp, parameters);
			}
			finally
			{
				T.X();
			}
			return (dtCurrencyRates);
		}

		/// <summary>
		/// Get the imported fund prices for this snapshot
		/// </summary>
		/// <param name="snapshot"></param>
		/// <returns></returns>
		/// <exception cref="DatabaseException">Unable to load company</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		public DataTable GetImportedFundPrices(Snapshot snapshot)
		{
			return (this.getImportResults("usp_ImportedFundPricesListForSnapshotId", snapshot));
		}

		/// <summary>
		/// Get the list of imported asset fund splits for an snapshot
		/// </summary>
		/// <param name="snapshot"></param>
		/// <returns></returns>
		/// <exception cref="DatabaseException">Unable to load company</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		public DataTable GetImportedAssetFundSplit(Snapshot snapshot)
		{
			return (this.getImportResults("usp_ImportedAssetFundSplitListForsnapshotId", snapshot));
		}

		/// <summary>
		/// Get the list of imported Composite fund splits for an snapshot
		/// </summary>
		/// <param name="snapshot"></param>
		/// <returns></returns>
		/// <exception cref="DatabaseException">Unable to load company</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		public DataTable GetImportedCompositeFundSplit(Snapshot snapshot)
		{
			return (this.getImportResults("usp_ImportedCompositeFundSplitListForSnapshotId", snapshot));
		}


		/// <summary>
		/// Transfer the Asset Fund weightings from the temp table to the real table
		/// </summary>
		/// <param name="snapshot"></param>
		/// <param name="transaction"></param>
		/// <param name="valuationPoint">The valuation point for the file being imported</param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public void TransferImportedCompositeFundWeightings( DateTime valuationPoint, Snapshot snapshot , SqlTransaction transaction )
		{
			T.E();
			const string sp = "usp_CompositeWeightingsImport" ;
			SqlParameter[] parameters = new SqlParameter[1];
			try
			{
				parameters[0] = new SqlParameter( "@SnapshotId", SqlDbType.BigInt); 
				parameters[0].Value = snapshot.Id;


				//call the weightings calculation SP, this SP also calls the clear SP which removes the 
				//imports for the current import ID from the temporary table
				SqlHelper.ExecuteNonQuery( transaction , CommandType.StoredProcedure , sp , parameters );
			}
			catch (SqlException ex)
			{
				ThrowDBException (ex, sp, parameters);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Transfer the Asset Fund weightings from the temp table to the real table
		/// </summary>
		/// <param name="snapshot"></param>
		/// <param name="transaction"></param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public void TransferImportedAssetFundWeightings( Snapshot snapshot , SqlTransaction transaction )
		{
			T.E();
			const string sp = "usp_AssetFundIndexWeightingsImport" ;
			SqlParameter[] parameters = new SqlParameter[1];
			try
			{
				parameters[0] = new SqlParameter( "@SnapshotID", SqlDbType.BigInt); 
				parameters[0].Value = snapshot.Id;

				SqlHelper.ExecuteNonQuery( transaction , CommandType.StoredProcedure , sp , parameters );
			}
			catch (SqlException ex)
			{
				ThrowDBException (ex, sp, parameters);
			}
			finally
			{
				T.X();
			}
		}
//
//		/// <summary>
//		/// Load the list of valid asset fund currency combinations so we can validate each record.
//		/// </summary>
//		/// <returns></returns>
//		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
//		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
//		public DataTable LoadValidAssetFundCurrencyCombinations()
//		{
//			T.E();
//			const string sp = "usp_AssetFundCurrencyCombinationList";
//			DataTable validAssetFundCurrencyCombinationDataTable = null;
//			try
//			{
//				DataSet validAssetFundCurrencyCombinationDataSet = new DataSet( "AssetFundCurrency" );
//				SqlHelper.FillDataset( ConnectionString, sp , validAssetFundCurrencyCombinationDataSet , new string[] { "AssetFundCurrency" } );
//
//				validAssetFundCurrencyCombinationDataTable = validAssetFundCurrencyCombinationDataSet.Tables[0];                                  
//
//				//disassociate from this dataset so ds can be garbage collected
//				//and table can be added to different dataset if necessary
//				validAssetFundCurrencyCombinationDataSet.Tables.RemoveAt(0); 
//
//				if (validAssetFundCurrencyCombinationDataTable.Rows.Count == 0)
//					validAssetFundCurrencyCombinationDataTable=null;
//
//			}
//			catch (SqlException ex)
//			{
//				ThrowDBException (ex, sp);
//			}
//			finally
//			{
//				T.X();
//			}
//			return (validAssetFundCurrencyCombinationDataTable);
//
//		}

		
		/// <summary>
		/// Load the list of benchmarks that are active in the system.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public DataTable LoadAllAssetFundBenchmarks(string companyCode,SqlTransaction transaction)
		{
			T.E();
			const string sp = "usp_BenchmarkSplitGetAllByCompanyCode";
			DataTable allAssetFundBenchmarksDataTable = null;
			try
			{
				DataSet allAssetFundBenchmarks = new DataSet( "AssetFundBenchmarks" );
				SqlHelper.FillDataset( transaction, sp , allAssetFundBenchmarks , new string[] { "AssetFundCurrency" },new object[]{companyCode} );

				allAssetFundBenchmarksDataTable = allAssetFundBenchmarks.Tables[0];                                  

				//disassociate from this dataset so ds can be garbage collected
				//and table can be added to different dataset if necessary
				allAssetFundBenchmarks.Tables.RemoveAt(0); 

				if (allAssetFundBenchmarksDataTable.Rows.Count == 0)
					allAssetFundBenchmarksDataTable=null;

			}
			catch (SqlException ex)
			{
				ThrowDBException (ex, sp);
			}
			finally
			{
				T.X();
			}
			return (allAssetFundBenchmarksDataTable);

		}

		/// <summary>
		/// Load the list of imported assetfund splits for the given snapshot.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public DataView LoadAssetFundSplitsFromTempTable(Snapshot snapshot ,SqlTransaction tran)
		{
			T.E();
			const string sp = "usp_LoadAssetFundSplitsFromTempTable";
			DataView assetFundSplits = null;
			try
			{
				DataSet assetFundSplitsDS = new DataSet( "AssetFundSplits" );
				SqlHelper.FillDataset(tran, sp , assetFundSplitsDS , new string[] { "AssetFundCurrency" },new object[]{snapshot.Id} );

				DataView d=assetFundSplitsDS.Tables[0].DefaultView;
				d.Sort="AssetFundId";
				assetFundSplits = d;                                  

				//disassociate from this dataset so ds can be garbage collected
				//and table can be added to different dataset if necessary
				assetFundSplitsDS.Tables.RemoveAt(0); 

//				if (assetFundSplitsDS.Tables[0].Rows.Count == 0)
//					assetFundSplits=null;

			}
			catch (SqlException ex)
			{
				ThrowDBException (ex, sp);
			}
			finally
			{
				T.X();
			}
			return (assetFundSplits);

		}
		#endregion

		#region Private methods

		/// <summary>
		/// retrieves import results
		/// </summary>
		/// <param name="sqlCommand"></param>
		/// <param name="snapshot"></param>
		/// <returns></returns>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		private DataTable getImportResults(string sqlCommand, Snapshot snapshot)
		{
			T.E();
			SqlParameter[] parameters = new SqlParameter[1];
			DataTable dt = null;                                  
			try
			{
				parameters[0] = new SqlParameter( "@iSnapshotId", SqlDbType.BigInt); 
				parameters[0].Value = snapshot.Id;

				DataSet ds = 
					SqlHelper.ExecuteDataset(this.ConnectionString,
					CommandType.StoredProcedure,
					sqlCommand,
					parameters);

				dt = ds.Tables[0];                                  

				//disassociate from this dataset so ds can be garbage collected
				//and table can be added to different dataset if necessary
				ds.Tables.RemoveAt(0); 

				if (dt.Rows.Count == 0)
					dt=null;
			}
			catch (SqlException ex)
			{
				ThrowDBException (ex, sqlCommand, parameters);
			}
			finally
			{
				T.X();
			}
			return (dt);
		}


		#endregion

		/// <summary>
		/// Removes from imported split temp table entries that match the given argumants.
		/// </summary>
		/// <param name="assetFundId">Asset fund id.</param>
		/// <param name="snapshotId">Snapshot id.</param>
		/// <param name="transaction">Transaction.</param>
		public void RemoveFromImportedSplitTempTable(string assetFundId,long snapshotId, SqlTransaction transaction)
		{
			T.E();
			const string sp = "usp_ImportedSplitWorkingTableDelete" ;
			SqlParameter[] parameters = new SqlParameter[2];
			try
			{
				parameters[0] = new SqlParameter( "@assetFundId", SqlDbType.Char,8); 
				parameters[0].Value =assetFundId;

				parameters[1] = new SqlParameter( "@snapshotId", SqlDbType.BigInt,8); 
				parameters[1].Value =snapshotId;

				SqlHelper.ExecuteNonQuery( transaction , CommandType.StoredProcedure , sp , parameters );
			}
			catch (SqlException ex)
			{
				ThrowDBException (ex, sp, parameters);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Checks to see if any of the countries being imported on the market split have been set up in UPD with 
		/// the proportion spread across more than one market
		/// </summary>
		/// <param name="snapshotId">Snapshot id.</param>
		/// <param name="transaction">Transaction.</param>
		/// <returns></returns>
		public bool MoreThanOneMarketForACountry(long snapshotId, SqlTransaction transaction)
		{
			T.E();
			int count=0;
			const string loadSp = "usp_ImportMoreThanOneMarketForACountry";
			// Build the parameters collection
			SqlParameter[] parameters = new SqlParameter[1];
			try
			{
					using (SqlCommand command = new SqlCommand())
					{
						// Set up the parameters.
						parameters[0] = new SqlParameter("@snapShotId", SqlDbType.BigInt);
						parameters[0].Value = snapshotId;
						command.Parameters.Add(parameters[0]);
						command.Transaction=transaction;
						command.Connection = transaction.Connection;
						command.CommandText = loadSp;
						command.CommandType = CommandType.StoredProcedure;
						count = Convert.ToInt32(command.ExecuteScalar());
					}

			}
			catch (SqlException ex)
			{
				ThrowDBException(ex, loadSp);
			}
			finally
			{
				T.X();
			}
			return count>1;
		}
	}
}