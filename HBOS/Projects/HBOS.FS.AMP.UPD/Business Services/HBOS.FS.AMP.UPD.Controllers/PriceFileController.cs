using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Persistence;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.Lookups;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Controllers
{
	/// <summary>
	/// Controller object for all things PriceFile orientated.  
	/// This will handle calls to the data layer.
	/// This is used to keep the logical layers seperate.  
	/// </summary>
	public class PriceFileController
	{
		#region Constructor

		/// <summary>
		/// Creates a new <see cref="PriceFileController"/> instance.
		/// </summary>
		public PriceFileController()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#endregion

		#region Load methods

	
		/// <summary>
		/// Loads the specified PriceFile.
		/// </summary>
		/// <param name="priceFileId">Unique identifier of the Price File to load.</param>
		/// <param name="connectionString"></param>
		/// <returns>Populated PriceFile object</returns>
		/// <exception cref="DatabaseException">Unable to load PriceFile</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		public static PriceFile LoadPriceFile(int priceFileId, string connectionString)
		{
			T.E();
			PriceFile PriceFile = null;
			try
			{
				PriceFileStaticDataPersister persister = new PriceFileStaticDataPersister(connectionString);
				PriceFile = persister.Load(priceFileId);
			}
			finally
			{
				T.X();
			}
			return PriceFile;
		}

		/// <summary>
		/// Loads the asset funds.
		/// </summary>
		/// <param name="priceFileId">Price file id.</param>
		/// <param name="connectionString">Connection string.</param>
		/// <returns></returns>
		public static SimpleLookupCollection LoadAssetFunds(int priceFileId, string connectionString)
		{
			T.E();
			SimpleLookupCollection returnCollection=new SimpleLookupCollection();;
			try
			{
				AssetFundLookupPersister persister = new AssetFundLookupPersister(connectionString);
				returnCollection=persister.LoadAssetFunds(priceFileId);
			}
			finally
			{
				T.X();
			}
			return returnCollection;
		}

		#endregion

		#region Update methods

		/// <summary>
		/// Updates the PriceFile item to the database.
		/// </summary>
		/// <param name="PriceFile">PriceFile object.</param>
		/// <param name="connectionString"></param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public static void UpdatePriceFile(PriceFile PriceFile, string connectionString)
		{
			T.E();
			try
			{
				PriceFileStaticDataPersister persister = new PriceFileStaticDataPersister(connectionString);
				persister.Save(PriceFile);
			}
			finally
			{
				T.X();
			}
		}

		#endregion


		/// <summary>
		/// Determines whether the specified fileName is  related to the company.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="companyCode">Company code.</param>
		/// <param name="extension">File extension</param>
		/// <param name="connectionString">Connection string.</param>
		/// <returns>
		/// 	<c>true</c> if [is file related to company] [the specified fileName]; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsFileRelatedToCompany(string fileName, string companyCode,string extension,string connectionString)
		{
			T.E();
			bool returnValue=false;
			try
			{
				PriceFileStaticDataPersister persister = new PriceFileStaticDataPersister(connectionString);
				returnValue=persister.NumberOfRelatedFiles(fileName,companyCode,extension)>0;
			}
			finally
			{
				T.X();
			}
			return returnValue;
		}
	}
}