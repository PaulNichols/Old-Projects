using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using Microsoft.ApplicationBlocks.Data;
using HBOS.FS.AMP.UPD.Types.DistributionFiles;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.Support.Tex;
using HBOS.FS.AMP.Entities;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// The class to use for persisting or retrieving DistributionFile objects.
	/// </summary>
	public class DistributionFilePersister : EntityPersister
	{
		#region Constructors

		/// <summary>
		/// Constructor initialising the connection string propperty.
		/// </summary>
		/// <param name="connectionString">The connection string to use for data access.</param>
		public DistributionFilePersister(string connectionString) : base(connectionString)
		{
		}

		#endregion

		#region Load Methods

		/// <summary>
		/// Return all the files associated with the given fund group id
		/// </summary>
		/// <param name="fundGroupID">The fund group id for which the files are required.</param>
		/// <returns>All the Files associated with the given fund group id.</returns>
		/// <exception cref="DatabaseException">Unable to load</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ArgumentException">Column name not recognised or unexpected data in db</exception>
		public DistributionFileCollection LoadFilesForFundGroupID(int fundGroupID)
		{
			T.E();
			const string loadSp = "usp_DistributionFilesGetForFundGroupID";
	
			SqlParameter[] parameters = new SqlParameter[1];
			parameters[0] = new SqlParameter("@iFundGroupID", SqlDbType.Int);
			parameters[0].Value = fundGroupID;

			DistributionFileCollection files = new DistributionFileCollection();
			this.LoadEntityCollection(loadSp, parameters, files);

			T.X();
			return files;
		}

		/// <summary>
		/// Loads the collection of all distribution files from the database 
		/// for the specified company.
		/// </summary>
		/// <param name="companyCode">The company</param>
		/// <returns>A collection of all our files</returns>
		/// <exception cref="DatabaseException">Unable to load</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		public DistributionFileCollection LoadFilesForCompany(string companyCode)
		{
			T.E();
			const string sp = "usp_DistributionFilesGetLookupsForCompanyCode";

			SqlParameter[] parameters = new SqlParameter[1];
			parameters[0] = new SqlParameter("@sCompanyCode", SqlDbType.VarChar, 10);
			parameters[0].Value = companyCode;

			DistributionFileCollection files = new DistributionFileCollection();
			this.LoadEntityCollection(sp, parameters, files);

			T.X();
			return files;
		}

		/// <summary>
		/// Loads the files for distribution for a specified company.
		/// </summary>
		/// <param name="companyCode">Company code.</param>
		/// <returns></returns>
		/// <exception cref="DatabaseException">Unable to load</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		public DistributionFileCollection LoadFilesForDistribution(string companyCode)
		{
			T.E();
			const string spName = "usp_DistributionFilesGetForDistribution";

			SqlParameter[] parameters = new SqlParameter[1];
			parameters[0] = new SqlParameter("@sCompanyCode", SqlDbType.VarChar,10);
			parameters[0].Value = companyCode;

			DistributionFileCollection result = new DistributionFileCollection();
			this.LoadEntityCollection(spName,parameters,result);

			T.X();
			return result;
		}

		/// <summary>
		/// Runs a stored procedure associated with a distribution and returns the data
		/// as an untyped data set. The table is named with the id of the distribution.
		/// </summary>
		/// <param name="fileToDistribute">File to distribute.</param>
		/// <returns></returns>
		public DataSet LoadDistributionDataSet(DistributionFile fileToDistribute)
		{
			T.E();
			DataSet result = new DataSet();

//			if (fileToDistribute.Status==DistributionFileStatuses.Distributed)
//			{
//				byte[] contents=loadDistributionContents(fileToDistribute.FileID);
//				string content=DeCompress(contents);
//				StringReader stringReader =new StringReader(content);
//				result.ReadXml(stringReader);
//				stringReader.Close();
//			}
//			else
//			{
				const string spName = "usp_DistributionFilesExecute";

				SqlParameter[] parameters = new SqlParameter[1];
				parameters[0] = new SqlParameter("@iFileId",SqlDbType.Int);
				parameters[0].Value = fileToDistribute.FileID;

				try
				{
					SqlHelper.FillDataset(
						this.ConnectionString,
						CommandType.StoredProcedure,
						spName,
						result,
						new string[] {fileToDistribute.FileID.ToString()},
						parameters
						);
				}
				catch(SqlException ex)
				{
					this.ThrowDBException(ex,spName,parameters);
				}

				T.X();
//			}
			return result;
		}

		/// <summary>
		/// Creates the entity from the data in the reader
		/// </summary>
		/// <param name="safeReader">The reader to load data from</param>
		/// <returns></returns>
		protected override object CreateEntity(SafeDataReader safeReader)
		{
			T.E();
			DistributionFile file;
			if (safeReader.ColumnExists("ExpectedFundCount"))
			{
				if(safeReader.ColumnExists("archiveFolder"))
				{
					// create for distribution
					file = new DistributionFile(
						safeReader.GetInt32("fileID"),
						safeReader.GetString("fileDesc"),
						safeReader.GetString("fileName"),
						safeReader.GetString("filePath"),
						safeReader.GetString("archiveFolder"),
						safeReader.GetString("CompanyCode"),
						safeReader.ColumnExists("manipulationClassToInvoke")? safeReader.GetString("manipulationClassToInvoke"):string.Empty,
						determineStatus(safeReader),
						safeReader.GetInt32("DistributedCount"),
						safeReader.GetInt32("AvailableFundCount"),
						safeReader.GetInt32("ExpectedFundCount"),
						createXsltLoader(safeReader),
						safeReader.GetTimestamp("ts")
						);
				}
				else
				{
					// create for lookups
					file = new FundGroupDistributionFile(
						safeReader.GetInt32("fileID"),
						safeReader.GetString("fileDesc"),
						string.Empty,
						string.Empty,
                        determineStatus(safeReader),
						string.Empty,
						safeReader.ColumnExists("FundGroupNumberRequired")? safeReader.GetBoolean("FundGroupNumberRequired"):false,
						safeReader.ColumnExists("DecimalPlacesRequired")? safeReader.GetBoolean("DecimalPlacesRequired"):false,
						safeReader.ColumnExists("SignificantDecimalPlacesRequired")? safeReader.GetBoolean("SignificantDecimalPlacesRequired"):false,
						safeReader.ColumnExists("MajorDenominationRequired")? safeReader.GetBoolean("MajorDenominationRequired"):false,
						safeReader.ColumnExists("FundGroupNumber") ? 
							safeReader.GetInt32("FundGroupNumber")==0 ? (object) null: safeReader.GetInt32("FundGroupNumber")  
							: null,
						safeReader.ColumnExists("UseMajorDenomination") ? safeReader.GetBoolean("UseMajorDenomination") : false,
						safeReader.ColumnExists("NumberOfDecimalPlaces") ? safeReader.GetInt16("NumberOfDecimalPlaces") : 1,
						safeReader.ColumnExists("NumberOfSignificantDecimalPlaces") ? safeReader.GetInt16("NumberOfSignificantDecimalPlaces") : 1,
						new byte[1]
						);
				}
			}
			
			else if (safeReader.ColumnExists("ts"))
			{
				// create for fund group id
				file = new DistributionFile(
					safeReader.GetInt32("fileID"),
					safeReader.GetString("fileDesc"),
					safeReader.GetString("fileName"),
					safeReader.GetString("filePath"),
					DistributionFileStatuses.Unavailable,
					string.Empty,
					safeReader.ColumnExists("FundGroupNumberRequired")? safeReader.GetBoolean("FundGroupNumberRequired"):false,
					safeReader.ColumnExists("DecimalPlacesRequired")? safeReader.GetBoolean("DecimalPlacesRequired"):false,
					safeReader.ColumnExists("SignificantDecimalPlacesRequired")? safeReader.GetBoolean("SignificantDecimalPlacesRequired"):false,
					safeReader.ColumnExists("MajorDenominationRequired")? safeReader.GetBoolean("MajorDenominationRequired"):false,
					safeReader.GetTimestamp("ts")
					);
			}
			else
			{
				// create for lookups
				file = new DistributionFile(
					safeReader.GetInt32("fileID"),
					safeReader.GetString("fileDesc"),
					string.Empty,
					string.Empty,
					DistributionFileStatuses.Unavailable,
					string.Empty,
					safeReader.ColumnExists("FundGroupNumberRequired")? safeReader.GetBoolean("FundGroupNumberRequired"):false,
					safeReader.ColumnExists("DecimalPlacesRequired")? safeReader.GetBoolean("DecimalPlacesRequired"):false,
					safeReader.ColumnExists("SignificantDecimalPlacesRequired")? safeReader.GetBoolean("SignificantDecimalPlacesRequired"):false,
					safeReader.ColumnExists("MajorDenominationRequired")? safeReader.GetBoolean("MajorDenominationRequired"):false,
					new byte[1]
					);
			}

			T.X();
			return file;
		}

		private  static DistributionFileStatuses determineStatus(SafeDataReader reader)
		{
			T.E();
			
            int expectedFundCount = reader.GetInt32("ExpectedFundCount");
            int availableFundCount = reader.GetInt32("AvailableFundCount");
            int distributionStatus = -1;
            bool allowPartialDistributions = reader.GetBoolean("AllowPartialDistribution");

            // This only works because you cannot unrelease

            // If no funds are expected or available then we cannot distribute
			if (distributionStatus == -1 && (expectedFundCount == 0 || availableFundCount == 0))
			{
				distributionStatus = (int) DistributionFileStatuses.Unavailable;
			}
            
            //  If not all funds are ready and partial distributions are allowed then we can do a partial distribution
            if  (distributionStatus == -1 && availableFundCount != expectedFundCount)
            {
                if(allowPartialDistributions == true)
                {
                    // Having got here we must are either able to distribute or we have aleady done so
                    if (reader.IsNull("LastDistributed") || 
						reader.GetDateTime("LastDistributed") < reader.GetDateTime("activePricingDay"))
                    {
                        distributionStatus = (int) DistributionFileStatuses.Partial;
                    }
//                    else
//                    {
//                        distributionStatus = (int) DistributionFileStatuses.PartiallyDistributed;
//                    }
                }
                else
                {
                    //  If not all funds are ready and partial distributions are not allowed then we cannot distribute
                    distributionStatus = (int) DistributionFileStatuses.Unavailable;
                }

            }
            if  (distributionStatus == -1)
            {
                // Having got here we must are either able to distribute or we have aleady done so
                if (reader.IsNull("LastDistributed") || 
					reader.GetDateTime("LastDistributed") < reader.GetDateTime("activePricingDay"))
                {
                    distributionStatus = (int) DistributionFileStatuses.Available;
                }
                else
                {
                    distributionStatus = (int) DistributionFileStatuses.Distributed;
                }
            }

			T.X();
			return (DistributionFileStatuses) distributionStatus;;
		}

		private  static IXsltLoader createXsltLoader(SafeDataReader reader)
		{
			T.E();
			IXsltLoader result = null;
			
			string xsltLocation = reader.GetString("XsltLocation");
			if (xsltLocation == string.Empty)
				throw new DatabaseException(string.Format("XsltLocation is empty in table DistributionFiles for file id: {0}  desc:{1}",reader.GetString("FileId"),reader.GetString("FileDesc")));


			string[] xsltLocationSplit = xsltLocation.Split(new char[] {':'},2);
			string locationType = xsltLocationSplit[0].Trim().ToLower();

			if (locationType == "database")
			{
				result = new DatabaseXsltLoader(reader.GetString("xsltSource"));
			}
			else
			{
				if (xsltLocationSplit.Length < 2)
					throw new DatabaseException(string.Format("XsltLocation in table DistributionFiles specifies a location type of {0} but does not specify the actual location for file id: {1}  desc:{2}",locationType,reader.GetString("FileId"),reader.GetString("FileDesc")));

				string location = xsltLocationSplit[1].Trim();

				if (locationType == "file")
					result = new FileXsltLoader(location);
				else if (locationType == "embedded")
					result = new EmbeddedResourceXsltLoader(location);
				else
					throw new DatabaseException(string.Format("XsltLocation in table DistributionFiles specifies an invalid location type of {0} for file id: {1}  desc:{2}",locationType,reader.GetString("FileId"),reader.GetString("FileDesc")));
			}

			T.X();
			return result;
		}

		#endregion

		#region Save Methods

		/// <summary>
		/// Updates the distribution file and the funds associated with that distribution once
		/// distribution has taken place.
		/// </summary>
		/// <param name="fileToUpdate">File to update.</param>
		public void SaveFileAfterDistribution(DistributionFile fileToUpdate)
		{
			T.E();
			SaveEntity(fileToUpdate);
			T.X();
		}

		/// <summary>
		/// Updates the distribution file after distribution
		/// </summary>
		/// <param name="entity">DistributionFile entity to update.</param>
		/// <param name="transaction">Transaction to use.</param>
		protected override void UpdateEntity(IEntityBase entity, SqlTransaction transaction)
		{
			T.E();

			if (entity == null)
				throw new ArgumentNullException("entity","Cannot update null Distribution File");

			if (!(entity is DistributionFile))
				throw new ArgumentException("entity is not DistributionFile during update","entity");

			DistributionFile file = (DistributionFile)entity;

			const string spName = "usp_DistributionFilesUpdate";
			
			SqlParameter[] parameters = new SqlParameter[1];
			parameters[0] = new SqlParameter("@iFileId",SqlDbType.Int);
			parameters[0].Value = file.FileID;
			
			try
			{
				SqlHelper.ExecuteNonQuery(transaction,spName,parameters);
			}
			catch(SqlException ex)
			{
				this.ThrowDBException(ex,spName,parameters);
			}
			
			T.X();
		}

		#region Compression Methods

		internal static byte[] Compress(string strInput)
		{ 
			try
			{
				byte[] bytData = System.Text.Encoding.UTF8.GetBytes(strInput);
				MemoryStream ms = new MemoryStream();
				Stream s = new DeflaterOutputStream(ms);
				s.Write(bytData, 0, bytData.Length);
				s.Close();
				byte[] compressedData = ms.ToArray();
				// show the user what's going to happen---
				//MessageBox.Show("Original: " +bytData.Length.ToString()+": " +"Compressed: " +compressedData.Length.ToString());
				return compressedData;
			}
			catch(Exception e)
			{
				throw e;
			}
		} 

//		// Decompress Method accepts byte array and returns string
//		internal static string DeCompress(string strInput)
//		{
//			return (DeCompress(System.Text.Encoding.UTF8.GetBytes(strInput)));
//		}


		// Decompress Method accepts byte array and returns string
		internal static string DeCompress(byte[] bytInput)
		{
			string strResult="";
			int totalLength = 0;
			byte[] writeData = new byte[4096];
			Stream s2 = new InflaterInputStream(new MemoryStream(bytInput));

			try
			{
				while (true) 
				{
					int size = s2.Read(writeData, 0, writeData.Length);
					if (size > 0) 
					{
						totalLength += size;
						strResult+=System.Text.Encoding.ASCII.GetString(writeData, 0,
							size);
					} 
					else 
					{
						break;
					}
				}
				s2.Close();
				return strResult;
			}
			catch(Exception e)
			{
				throw e;
			}
		}
		#endregion

		#endregion Save Methods

		/// <summary>
		/// Creates an audit record.
		/// </summary>
		/// <param name="fileContents">The xml content from the dataset of distribution data.</param>
		public long CreateAudit(string fileContents)
		{
			byte[] compressedFileContents=Compress(fileContents);
			const string spName = "usp_DistributeAuditCreate";
			
			SqlParameter[] parameters = new SqlParameter[2];
			parameters[0] = new SqlParameter("@content",SqlDbType.VarBinary,8000);
			parameters[0].Value = compressedFileContents;
			SqlParameter outparam=new SqlParameter("@id",SqlDbType.BigInt);
			outparam.Direction=ParameterDirection.Output;
			parameters[1] = outparam;
			
			try
			{
				SqlHelper.ExecuteNonQuery(ConnectionString,spName,parameters);
			}
			catch(SqlException ex)
			{
				this.ThrowDBException(ex,spName,parameters);
			}
			
			T.X();
			
			return 23;//(long) parameters[1].Value;
		}


		/// <summary>
		/// Retrieves data from an audit record.
		/// </summary>
		/// <param name="id">id of reord to retrieve</param>
		private byte[] loadDistributionContents(long id)
		{

			const string spName = "usp_DistributeAuditGet";
			
			SqlParameter[] parameters = new SqlParameter[1];
			parameters[0] = new SqlParameter("@id",SqlDbType.BigInt);
			parameters[0].Value = id;
			
			
			try
			{
			return 	SqlHelper.ExecuteScalar(ConnectionString,spName,parameters) as byte[] ;
				
			}
			catch(SqlException ex)
			{
				this.ThrowDBException(ex,spName,parameters);
			}
			
			T.X();
			
			return new byte[0];
		}
	}
}