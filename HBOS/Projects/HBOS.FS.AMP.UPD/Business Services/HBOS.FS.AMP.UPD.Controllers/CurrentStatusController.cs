using System;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Persistence;
using HBOS.FS.AMP.UPD.Types.Status;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Controllers
{
	/// <summary>
	/// Summary description for CurrentStatusController.
	/// </summary>
	public class CurrentStatusController
	{
		/// <summary>
		/// Create an instance of <see cref="CurrentStatusController"/>
		/// </summary>
		public CurrentStatusController()
		{
		}

		/// <summary>
		/// Gets the current import file status from the database
		/// </summary>
		/// <returns>An <see cref="CurrentImportStatus"/> object containing information on the last imported files.</returns>
		/// <exception cref="DatabaseException">Unable to load company</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		public  static CurrentImportStatus LoadCurrentStatus(string connectionString, string companyCode)
		{
			T.E();
			CurrentImportStatus importStatus = null;
			try
			{
				ImportStatusPersister importStatusPersister = new ImportStatusPersister(connectionString);
				importStatus = importStatusPersister.LoadCurrentImportStatus(companyCode);
			}
				catch (Exception ex)
				{
					System.Console.WriteLine(ex);
				}
			finally
			{
				T.X();
			}
			return importStatus;
		}

		/// <summary>
		/// Load all the import details for the passed company
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="companyCode"></param>
		/// <returns></returns>
		public  static ImportDetailsCollection LoadCompanyImportStatus(string connectionString, string companyCode)
		{
			T.E();
			ImportDetailsCollection importStatus = null;
			try
			{
				ImportStatusPersister importStatusPersister = new ImportStatusPersister(connectionString);
				importStatus = importStatusPersister.LoadImportStatusForCompany(companyCode);
			}
			finally
			{
				T.X();
			}
			return importStatus;
		}
	}
}