using System;
using System.Data;
using System.Data.SqlClient;

using Microsoft.ApplicationBlocks.Data;

using HBOS.FS.AMP.UPD.Types.Status;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// Summary description for ImportStatusPersister.
	/// </summary>
	public class ImportStatusPersister : EntityPersister
	{

        #region Constructors

        /// <summary>
        /// Constructor used to initialise the ConnectionString property.
        /// </summary>
        /// <param name="connectionString"></param>
        public ImportStatusPersister(string connectionString) : base (connectionString)
        {
        }

        #endregion

        #region Load methods
		/// <summary>
		/// Loads the current import status.
		/// </summary>
		/// <param name="companyCode">The company code to load the status for.</param>
		/// <returns>The current status of the import</returns>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public CurrentImportStatus LoadCurrentImportStatus(string companyCode)
        {
			T.E();
			const string sp = "usp_ImportSourceGetCurrent";
			CurrentImportStatus importStatus = null;

			SqlParameter[] parameters = new SqlParameter[1];
			try
			{

				parameters[0] = new SqlParameter("@sCompanyCode", SqlDbType.VarChar, 50);
				parameters[0].Value = companyCode;
			

				using (SqlDataReader dataReader = SqlHelper.ExecuteReader(this.ConnectionString, 
						   CommandType.StoredProcedure, sp, parameters))
				{
					if (!dataReader.IsClosed && dataReader.Read())
					{
						importStatus = new CurrentImportStatus(
							dataReader.IsDBNull(dataReader.GetOrdinal("activepricingday"))? DateTime.MinValue:dataReader.GetDateTime(dataReader.GetOrdinal("activepricingday")),
							dataReader.IsDBNull(dataReader.GetOrdinal("valuationPoint"))? DateTime.MinValue:dataReader.GetDateTime(dataReader.GetOrdinal("valuationPoint")),

							dataReader.IsDBNull(dataReader.GetOrdinal("indexValuesImportFileName"))? "":dataReader.GetString(dataReader.GetOrdinal("indexValuesImportFileName")),
							dataReader.IsDBNull(dataReader.GetOrdinal("indexValuesUser"))? "":dataReader.GetString(dataReader.GetOrdinal("indexValuesUser")),
							dataReader.IsDBNull(dataReader.GetOrdinal("indexValuesImportedBy"))? "":dataReader.GetString(dataReader.GetOrdinal("indexValuesImportedBy")),
							dataReader.IsDBNull(dataReader.GetOrdinal("indexValuesImportDate"))? DateTime.MinValue:dataReader.GetDateTime(dataReader.GetOrdinal("indexValuesImportDate")),

							dataReader.IsDBNull(dataReader.GetOrdinal("fundPricesImportFileName"))? "":dataReader.GetString(dataReader.GetOrdinal("fundPricesImportFileName")),
							dataReader.IsDBNull(dataReader.GetOrdinal("fundPricesUser"))? "":dataReader.GetString(dataReader.GetOrdinal("fundPricesUser")),
							dataReader.IsDBNull(dataReader.GetOrdinal("fundPricesImportedBy"))? "":dataReader.GetString(dataReader.GetOrdinal("fundPricesImportedBy")),
							dataReader.IsDBNull(dataReader.GetOrdinal("fundPricesImportDate"))? DateTime.MinValue:dataReader.GetDateTime(dataReader.GetOrdinal("fundPricesImportDate")),

							dataReader.IsDBNull(dataReader.GetOrdinal("currencyRatesImportFileName"))? "":dataReader.GetString(dataReader.GetOrdinal("currencyRatesImportFileName")),
							dataReader.IsDBNull(dataReader.GetOrdinal("currencyRatesUser"))? "":dataReader.GetString(dataReader.GetOrdinal("currencyRatesUser")),
							dataReader.IsDBNull(dataReader.GetOrdinal("currencyRatesImportedBy"))? "":dataReader.GetString(dataReader.GetOrdinal("currencyRatesImportedBy")),
							dataReader.IsDBNull(dataReader.GetOrdinal("currencyRatesImportDate"))? DateTime.MinValue:dataReader.GetDateTime(dataReader.GetOrdinal("currencyRatesImportDate")),

							dataReader.IsDBNull(dataReader.GetOrdinal("indexWeightingsImportFileName"))? "":dataReader.GetString(dataReader.GetOrdinal("indexWeightingsImportFileName")),
							dataReader.IsDBNull(dataReader.GetOrdinal("indexWeightingsUser"))? "":dataReader.GetString(dataReader.GetOrdinal("indexWeightingsUser")),
							dataReader.IsDBNull(dataReader.GetOrdinal("indexWeightingsImportedBy"))? "":dataReader.GetString(dataReader.GetOrdinal("indexWeightingsImportedBy")),
							dataReader.IsDBNull(dataReader.GetOrdinal("indexWeightingsImportDate"))? DateTime.MinValue:dataReader.GetDateTime(dataReader.GetOrdinal("indexWeightingsImportDate"))
							);
					}
                    else
                        importStatus = null;
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
			return importStatus;
        }

		/// <summary>
		/// Load details of all the imports for the passed company
		/// </summary>
		/// <param name="companyCode"></param>
		/// <returns></returns>
		public ImportDetailsCollection LoadImportStatusForCompany(string companyCode )
		{
			T.E();
			const string loadSp = "usp_SystemStatusImports";
	            
			// Set up store proc parameters
			SqlParameter[] parameters = new SqlParameter[1];
			parameters[0] = new SqlParameter("@CompanyCode", SqlDbType.VarChar, 10);
			parameters[0].Value = companyCode;

			// Create the import details collection.
			ImportDetailsCollection importDetails = new ImportDetailsCollection();

			try
			{
				// Now populate, this will invoke CreateEntity()
				this.LoadEntityCollection(loadSp, parameters, importDetails);
			}
			catch (SqlException ex)
			{
				ThrowDBException (ex, loadSp, parameters);
			}
			finally
			{
				T.X();
			}

			return importDetails;
		}

		

        #endregion

		#region Entity Methods

		/// <summary>
		/// Create an importDetails object
		/// </summary>
		/// <param name="safeReader"></param>
		/// <returns></returns>
		protected override object CreateEntity(SafeDataReader safeReader)
		{
			T.E();
			
			ImportDetails newImportDetails = null;
			
			try
			{
				string importName = safeReader.GetString("ImportName");
				DateTime lastImported = safeReader.GetDateTime("LastImported");
				string importedBy = safeReader.GetString("ImportedBy");

				newImportDetails = new ImportDetails( importName, importedBy, null, lastImported);
			}
			finally
			{
				T.X();
			}

			return newImportDetails;        
		}

		#endregion
	}
}
