using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using HBOS.FS.AMP.Data.Types;
using HBOS.FS.AMP.UPD.Persistence;
using HBOS.FS.AMP.UPD.Types.Snapshot;

namespace HBOS.FS.AMP.UPD.Controllers
{
	// Compare the entries in the temp table with the real benchmark split table
	// on an asset by asset basis and see if the new split has the same number of funds 
	// and the funds are for all the same countries. If there are any problems the raise 
	//erros to be stored with the other validation errors and displayed to the user
	internal class AssetFundDataValidator 
	{

		public AssetFundDataValidator()
		{
		}

//		public ValidationErrorSeverity Validate(string[] dataRow, DataTable dataDefinition)
//		{
//			ValidationErrorSeverity returnSeverity = ValidationErrorSeverity.None;
//
//			string assetFund = dataRow[ (int)AssetFundIndexWeightingColumnPosition.AssetFund ].ToString().Trim();
//			
//			// NOTE: The currency code is the file is REALLY a countryCode
//			string filterClause = String.Format( "assetFundId = '{0}'" , assetFund  );
//
//			DataRow[] matchingRows = new DataRow[0]; 
//			if (m_AllBenchmarks!=null)
//			{
//				matchingRows=m_AllBenchmarks.Select( filterClause );
//			}
//
//			if ( matchingRows.Length == 0 )
//			{
//				returnSeverity = ValidationErrorSeverity.High;
//				string Messsage=String.Format("Asset Fund {0} and Currency Code {1} is not a valid combination for the active company." , assetFund );
//				OnInvalidDataRow(Messsage,returnSeverity);
//			}
//
//			return returnSeverity;
//		}

		/// <summary>
		/// Raises the invalid data row (Overridable).
		/// </summary>
		/// <param name="message">Error or warning message</param>
		/// ///<param name="severity">Severity of the error</param>
		protected virtual void OnInvalidDataRow(string message,ValidationErrorSeverity severity)
		{
			if ( this.InvalidDataEvent != null )
			{
				InvalidDataEvent( this ,new InvalidDataEventArgs(  message , severity ) );
			}
		}

		internal  delegate void InvalidDataDelegate(object sender, AssetFundDataValidator.InvalidDataEventArgs e);
		public event InvalidDataDelegate InvalidDataEvent;

		internal class InvalidDataEventArgs
		{
			private string message;
			private ValidationErrorSeverity validationErrorSeverity;

			internal ValidationErrorSeverity ValidationErrorSeverity
			{
				get { return validationErrorSeverity; }
			}

			internal string Message
			{
				get { return message; }
			}

			internal InvalidDataEventArgs(string message, ValidationErrorSeverity validationErrorSeverity)
			{
				this.message = message;
				this.validationErrorSeverity = validationErrorSeverity;
			}
		}

		internal void MissingAssetFundsFromPriceImport(string connectionString,ImportHi3PricesSqlPersister persister,string extension,string fileName,string companyCode,long snapShotId)
		{
			
            string[] assetFunds= persister.MissingAssetFundsFromPriceImport(connectionString,extension, fileName, companyCode, snapShotId);
			foreach (string assetFund in assetFunds)
			{
				string Messsage=String.Format("Asset Fund {0} was expected on this file." , assetFund.TrimEnd() );
				OnInvalidDataRow(Messsage,ValidationErrorSeverity.High);
			}
		}

		/// <summary>
		///Compare the entries in the temp table with the real benchmark split table
		///on an asset by asset basis and see if the new split has the same number of funds 
		///and the funds are for all the same countries. If there are any problems the raise 
		///erros to be stored with the other validation errors and displayed to the user
		/// </summary>
		/// <param name="importPersister"></param>
		/// <param name="snapshot"></param>
		/// <param name="transaction"></param>
		/// <param name="companyCode"></param>
		internal void CompareSplits(ImportPersister importPersister ,Snapshot snapshot, SqlTransaction transaction,string companyCode)
		{
			DataTable allAssetFundBenchmarks = importPersister.LoadAllAssetFundBenchmarks(companyCode,transaction);

			if (allAssetFundBenchmarks!=null)
			{

//				if (!importPersister.MoreThanOneMarketForACountry(snapshot.Id,transaction))
//				{
					DataView allFromTempTable = importPersister.LoadAssetFundSplitsFromTempTable(snapshot,transaction);
					//Holds all problem AssetFundIds
					Hashtable assetFundsToRemoveFromTempTable=new Hashtable();

					string currentAssetFundId=null;
					int assetFundIdColumnIndex=0;
					//bool skipRestOfAssetFund=false;
					ValidationErrorSeverity returnSeverity=ValidationErrorSeverity.None;

					DataRow[] matchingRowsFromAll=null; 

					foreach (DataRowView importedSplitRow in allFromTempTable)
					{
						if (assetFundChanged(currentAssetFundId, importedSplitRow, assetFundIdColumnIndex))
						{

							matchingRowsFromAll = new DataRow[0]; 

							//	skipRestOfAssetFund=false;
							returnSeverity=ValidationErrorSeverity.None;

							currentAssetFundId=importedSplitRow.Row.ItemArray[assetFundIdColumnIndex].ToString().Trim();
						
							//filter down to just the splits on the database for the specified Asset Fund
							matchingRowsFromAll=allAssetFundBenchmarks.Select(String.Format( "assetFundID = '{0}'" , currentAssetFundId  ));
						
							DataRow[] matchingRowsFromTemp = new DataRow[0];
							matchingRowsFromTemp=allFromTempTable.Table.Select(String.Format( "assetFundId = '{0}'" , currentAssetFundId  ));

							if (matchingRowsFromAll.Length != 0 && ( matchingRowsFromAll.Length != matchingRowsFromTemp.Length))
							{
								//the structure of the split in the file and the set up split for 
								//the current Asset Fund differ
								returnSeverity = ValidationErrorSeverity.High;
								string Messsage=String.Format("Asset Fund {0} has a different number of benchmarks defined in the file than in UPD." , currentAssetFundId );
								OnInvalidDataRow(Messsage,returnSeverity);
								addToListOfDataToRemove(returnSeverity, assetFundsToRemoveFromTempTable, currentAssetFundId);
							}
							else
							{
								if (doesTheSplitIncludeFunds(matchingRowsFromAll))
								{
									returnSeverity=ValidationErrorSeverity.High;
									//string Messsage=String.Format("Asset Fund {0} is set-up in UPD to include Funds not just Markets." , currentAssetFundId );
									string Messsage=String.Format("System Asset Fund ({0}) Split is different to import Asset Fund Split." , currentAssetFundId );
									OnInvalidDataRow(Messsage,returnSeverity);
									addToListOfDataToRemove(returnSeverity, assetFundsToRemoveFromTempTable, currentAssetFundId);
								}
							}
						}

						checkSplitsAreTheSame(matchingRowsFromAll, importedSplitRow, ref returnSeverity,currentAssetFundId);

						addToListOfDataToRemove(returnSeverity, assetFundsToRemoveFromTempTable, currentAssetFundId);

					}
					RemoveFromTempTable(snapshot.Id, transaction, importPersister ,assetFundsToRemoveFromTempTable);
//				}
//				else
//				{
//					importPersister.ClearWorkingTable(snapshot.Id,transaction);
//				}
			}
		}

		private  void checkSplitsAreTheSame(DataRow[] matchingRowsFromAll, DataRowView importedSplitRow, ref ValidationErrorSeverity returnSeverity,string currentAssetFundId)
		{
			if (matchingRowsFromAll.Length != 0 )
			{
				//country
				returnSeverity=ValidationErrorSeverity.High;
				foreach (DataRow dataRow in matchingRowsFromAll)
				{
					if (importedSplitRow.Row.ItemArray[1].ToString()==dataRow.ItemArray[5].ToString())
					{
						returnSeverity=ValidationErrorSeverity.None;
						break;
					}
				}
						
				if (returnSeverity==ValidationErrorSeverity.High)
				{
					string Messsage=String.Format("Country code {0} is not valid , Asset Fund={1}." , importedSplitRow.Row.ItemArray[1].ToString(),importedSplitRow.Row.ItemArray[0].ToString());
					OnInvalidDataRow(Messsage,returnSeverity);
				}

			}

		}

		private static void addToListOfDataToRemove(ValidationErrorSeverity returnSeverity, Hashtable assetFundsToRemoveFromTempTable, string currentAssetFundId)
		{
			if (returnSeverity==ValidationErrorSeverity.High)
			{
				if (!assetFundsToRemoveFromTempTable.Contains(currentAssetFundId))
				{
					assetFundsToRemoveFromTempTable.Add(currentAssetFundId,currentAssetFundId)	;
				}
			}
		}

		private static bool assetFundChanged(string currentAssetFundId, DataRowView importedSplitRow, int assetFundIdColumnIndex)
		{
			//simply determins that we are now looking at another asset fund in the list of imported ones
			return currentAssetFundId==null || importedSplitRow.Row.ItemArray[assetFundIdColumnIndex].ToString().Trim()!=currentAssetFundId;
		}

		private void RemoveFromTempTable(long snapShotId,SqlTransaction tran,ImportPersister importPersister,Hashtable assetFundsToRemoveFromTempTable)
		{
			//delete any problem asset funds

			foreach (string assetFundId in assetFundsToRemoveFromTempTable.Values)
			{
				importPersister.RemoveFromImportedSplitTempTable(assetFundId,snapShotId,tran);
			}
		}

		private bool doesTheSplitIncludeFunds(DataRow[] rows)
		{
			bool returnValue=false;
			foreach (DataRow dataRow in rows)
			{
				//check the Hiportcode column, if there is 
				//no hiportcode then the benchmark is an Index
				if (dataRow.ItemArray[1]!=Convert.DBNull)
				{
					returnValue= true;
					break;
				}
			}
			return returnValue;
		}
	}
}