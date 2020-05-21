using System;
using System.Data;
using System.IO;
using System.Security;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Persistence;
using HBOS.FS.AMP.UPD.Types.DistributionFiles;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Controllers
{
	/// <summary>
	/// Summary description for DistributionFileController.
	/// </summary>
	public class DistributionFileController
	{
		/// <summary>
		/// DistributionFile constructor
		/// </summary>
		public DistributionFileController(string connectionString)
		{
			this.connectionString = connectionString;
		}

		private string connectionString;

		/// <summary>
		/// Load all the available distribution files for the passed company
		/// </summary>
		/// <param name="companyCode"></param>
		/// <returns></returns>
		public DistributionFileCollection LoadFilesForCompanyCode(string companyCode)
		{
			T.E();
			DistributionFileCollection files = new DistributionFileCollection();
			try
			{
				DistributionFilePersister persister = new DistributionFilePersister(connectionString);
				files = persister.LoadFilesForCompany(companyCode);
			}
			finally
			{
				T.X();
			}
			return files;
		}

//		/// <summary>
//		/// Load all the distribution files for the passed fund group id
//		/// </summary>
//		/// <param name="fundGroupID"></param>
//		/// <returns></returns>
//		public DistributionFileCollection LoadFilesForFundGroup(int fundGroupID)
//		{
//			T.E();
//			DistributionFileCollection files = new DistributionFileCollection();
//			try
//			{
//				DistributionFilePersister persister = new DistributionFilePersister(connectionString);
//				files = persister.LoadFilesForFundGroupID(fundGroupID);
//			}
//			finally
//			{
//				T.X();
//			}
//			return files;
//		}

		/// <summary>
		/// Load all the files for distribution for a given company
		/// </summary>
		/// <param name="companyCode"></param>
		/// <returns></returns>
		public DistributionFileCollection LoadFilesForDistribution(string companyCode)
		{
			T.E();
			DistributionFileCollection files = new DistributionFileCollection();
			try
			{
				DistributionFilePersister persister = new DistributionFilePersister(connectionString);
				files = persister.LoadFilesForDistribution(companyCode);
			}
			finally
			{
				T.X();
			}
			return files;
		}


		/// <summary>
		/// Updates the distribution file and the funds associated with that distribution once
		/// distribution has taken place.
		/// </summary>
		/// <param name="fileToUpdate">File to update.</param>
		public void SaveFileAfterDistribution(DistributionFile fileToUpdate)
		{
			T.E();

			try
			{
				DistributionFilePersister persister = new DistributionFilePersister(connectionString);
				persister.SaveFileAfterDistribution(fileToUpdate);
			}
			finally
			{
				T.X();
			}

			T.X();
		}

		/// <summary>
		/// Export the released fund data to the relevant files.
		/// </summary>
		/// <remarks>
		/// Populate a data set with all the released funds then use XSLT to structure
		/// the output file and populate it.
		/// </remarks>
		/// <param name="fileInformation">The distribution file item object</param>
		/// <param name="data">data to distribute</param>
		/// <remarks>All exceptions thrown are <exception cref="ExportException">Export Exceptions</exception></remarks>
		/// <exception cref="SecurityException">The user does not have the required permission to output the distribution file</exception>
		/// <exception cref="UnauthorizedAccessException">The exception that is thrown when the operating system denies access because of an I/O error or a specific type of security error.</exception>
		/// <exception cref="XsltCompileException">The exception that is thrown by the Load method when an error is found in the Extensible Stylesheet Transformation (XSLT) stylesheet.</exception>
		/// <exception cref="XsltException">The exception that is thrown when an error occurs while processing an Extensible Stylesheet Language (XSL) transform.</exception>
		/// <exception cref="Exception">Unable to generate the distribtion file</exception>
		/// <returns>StringBuilder</returns>
		public void Distribute(DistributionFile fileInformation,DataSet data)
		{
			T.E();
			try
			{
				if (!Directory.Exists(fileInformation.FilePath))
				{
					throw new ExportException(string.Format("The specified distribution directory path does not exists: {0}\n", fileInformation.FilePath));
				}

				archiveFile(fileInformation);

				//persistAuditInformation(fileInformation, data);

				StreamWriter outputFile = transformData(fileInformation, data);

				// Archive file if it exists, if archive folder does not exist an exception is thrown

				createFile(outputFile);
			}
			catch (SecurityException ex)
			{
				T.DumpException(ex);
				throw new ExportException(string.Format("The user does not have the required permission to output the distribution file {0}", fileInformation.FileDescription), ex);
			}
			catch (UnauthorizedAccessException ex)
			{
				//The exception that is thrown when the operating system denies access because of an I/O error or a specific type of security error.
				T.DumpException(ex);
				throw new ExportException(string.Format("Access is denied to generate the distribution file {0}.", fileInformation.FileDescription), ex);
			}
			catch (XsltCompileException ex)
			{
				//The exception that is thrown by the Load method when an error is found in the Extensible Stylesheet Transformation (XSLT) stylesheet.
				T.DumpException(ex);
				throw new ExportException(string.Format("There was an error loading the Extensible Stylesheet Transformation (XSLT) stylesheet for {0)", fileInformation.FileDescription), ex);
			}
			catch (XsltException ex)
			{
				T.DumpException(ex);
				//The exception that is thrown when an error occurs while processing an Extensible Stylesheet Language (XSL) transform.
				throw new ExportException(string.Format("There was an error processing the distribution transformation for {0}", fileInformation.FileDescription), ex);
			}
			finally
			{
				T.X();
			}

			return;
		}

		private long persistAuditInformation(DistributionFile information, DataSet data)
		{
			StringBuilder dataContent=new StringBuilder();
			StringWriter stringWriter =new StringWriter(dataContent);
			data.WriteXml(stringWriter);
			stringWriter.Close();
			DistributionFilePersister persister =new DistributionFilePersister(connectionString);
			long id=persister.CreateAudit(dataContent.ToString());
			return id;
		}

		private static void createFile(StreamWriter outputFile)
		{
			outputFile.Close();
			outputFile=null;
		}

		private StreamWriter transformData(DistributionFile fileInformation, DataSet data)
		{
			XslTransform transform = fileInformation.LoadXslt();
	
			// Implement a text writer for outputting characters in the default encoding. Note.  Overwrite file if it exist.
			string outputFileName = UPDIO.BuildFilePath(fileInformation.FilePath, fileInformation.FileName);
			StreamWriter outputFile = new StreamWriter(outputFileName, false, Encoding.Default);
	
			XmlDataDocument xmlDocument = loadDistributionDataXmlDocument(data);
	
			// Transforms the XML data using the loaded XSLT stylesheet.
			transform.Transform(xmlDocument, null, outputFile, null);
			return outputFile;
		}

		#region Private methods

		/// <summary>
		/// Returns the export data for a distribution as an untyped data set. The table 
		/// is named with the id of the distribution.
		/// </summary>
		/// <param name="data">Data to distribute.</param>
		/// <returns>XmlDataDocument</returns>
		private XmlDataDocument loadDistributionDataXmlDocument(DataSet data)
		{
			// Create an XML document from the source dataSet

			XmlDataDocument xmlDocument = null;
			if (data!=null)
			{
				xmlDocument = new XmlDataDocument(data);
			}
			else
			{
				xmlDocument = new XmlDataDocument();
			}

			return xmlDocument;
		}

		/// <summary>
		/// Retrieves the and validate data to distribute.
		/// </summary>
		/// <param name="fileToDistribute">File to distribute.</param>
		/// <returns></returns>
		public DataSet RetrieveAndValidateData(DistributionFile fileToDistribute)
		{
			T.E();
	
			DataSet data = null;
			try
			{
				DistributionFilePersister persister = new DistributionFilePersister(connectionString);
				data = persister.LoadDistributionDataSet(fileToDistribute);

				// Apply names to the tables to ease the transformation implementation
				if (data.Tables.Count > 0)
				{
					//check to see if any of the funds we intend to export 
					//do not have a alternate fund indentifier for the external system
					if (data.Tables[0].Columns.Contains("ExternalFundIdentifier"))
					{
						//string externalFundIdentifier;

						foreach (DataRow row in data.Tables[0].Rows)
						{
							if (row.IsNull("ExternalFundIdentifier"))
							{
								
							//	externalFundIdentifier = (string) row["ExternalFundIdentifier"];  
								throw new ExportException(
									string.Format("Cannot process '{0}' due to one or more of the funds not having an External Fund Identifier set-up.\nThe first Fund being - \n"
									+ "{1} \n", 
									fileToDistribute.FileDescription, 
										data.Tables[0].Columns.IndexOf("fullName")>-1?row["fullName"]:row["hiPortfolioCode"].ToString().TrimEnd() )
									);
								
							}
						}
					}

					data.DataSetName = "UPDExport";
					data.Tables[0].TableName = "ReleasedFunds";
				}
			}
			finally
			{
				T.X();
			}
			return data;
		}

		/// <summary>
		/// Archive the export file if it already exists
		/// </summary>
		/// <param name="file">Distribution file object containing the filename and path to export</param>
		private static void archiveFile(DistributionFile file)
		{
			T.E();
			try
			{
				string fileName = UPDIO.BuildFilePath(file.FilePath, file.FileName);

				// If the file exists then move to backup destination
				if (UPDIO.FileExists(fileName))
				{
					// Get a unique file name for the backup...
					string backupFileName = file.ArchiveFolder + @"\" + UPDIO.LoadNewFileNameWithDate(fileName, file.CompanyCode.Trim() + "_");

					// Move the file
					if (!UPDIO.MoveFile(fileName, backupFileName))
					{
						throw new ExportException(string.Format("It was not possible to backup the {0} file to {1}.", fileName, backupFileName));
					}
				}
			}
			catch (Exception ex)
			{
				T.DumpException(ex);
				throw;
			}
			finally
			{
				T.X();
			}

			return;
		}

		#endregion
	}
}