using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Persistence;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.Countries;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Controllers
{
	/// <summary>
	/// Controller object for all things Country orientated.  
	/// This will handle calls to the data layer.
	/// This is used to keep the logical layers seperate.  
	/// </summary>
	public class CountryController
	{
		#region Constructor

		/// <summary>
		/// Creates a new <see cref="CountryController"/> instance.
		/// </summary>
		public CountryController()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#endregion

		#region Load methods

	
		/// <summary>
		/// Loads the stock markets related to the specified Country.
		/// </summary>
		/// <param name="countryCode">Country code.</param>
		/// <param name="connectionString">Connection string.</param>
		/// <returns></returns>
		public static StockMarketCollection LoadStockMarkets(string countryCode, string connectionString)
		{
			StockMarketCollection returnCollection=new StockMarketCollection();
			if (countryCode!=null && countryCode.Length>0)
			{
				StockMarketPersister stockMarketPersister=new StockMarketPersister(connectionString);
			returnCollection=	stockMarketPersister.LoadForCountryCode(countryCode);
			}
			return returnCollection;
		}

		/// <summary>
		/// Loads the specified Country.
		/// </summary>
		/// <param name="CountryCode">Country code.</param>
		/// <param name="connectionString"></param>
		/// <returns>Populated Country object</returns>
		/// <exception cref="DatabaseException">Unable to load Country</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		public static Country LoadCountry(string CountryCode, string connectionString)
		{
			T.E();
			Country Country = null;
			try
			{
				CountryStaticDataPersister persister = new CountryStaticDataPersister(connectionString);
				Country = persister.Load(CountryCode);
			}
			finally
			{
				T.X();
			}
			return Country;
		}

		#endregion

		#region Update methods

		/// <summary>
		/// Updates the Country item to the database.
		/// </summary>
		/// <param name="Country">Country object.</param>
		/// <param name="connectionString"></param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public static void UpdateCountry(Country Country, string connectionString)
		{
			T.E();
			try
			{
				CountryStaticDataPersister persister = new CountryStaticDataPersister(connectionString);
				persister.Save(Country);
			}
			finally
			{
				T.X();
			}
		}

		#endregion

		
	}
}