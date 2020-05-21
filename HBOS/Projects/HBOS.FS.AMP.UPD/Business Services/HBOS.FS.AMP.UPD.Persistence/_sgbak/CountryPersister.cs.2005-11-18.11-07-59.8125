using System.Data.SqlClient;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Types.Countries;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
    /// The class to use for retrieving Country objects
    /// </summary>
	public class CountryPersister : EntityPersister
	{

		#region Constructor
		/// <summary>
		/// Constructor used to initialise the ConnectionString property.
		/// </summary>
		/// <param name="connectionString"></param>
		public CountryPersister(string connectionString) : base(connectionString)
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#endregion

		#region Public methods

		/// <summary>
		/// Flush the cach of countries
		/// </summary>
		public void FlushCountries()
		{
			CacheHelper cacheHelper =new CacheHelper();
			cacheHelper.FlushCountries();
		}

        /// <summary>
        /// Loads the collection of countries from the database.
        /// </summary>
        /// <remarks>Asset funds are the only entity that will need countries</remarks>
        /// <returns>A collection of all our countries</returns>
		/// <exception cref="DatabaseException">Unable to load countries</exception>
        /// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		public virtual CountryCollection LoadCountries()
        {
        	T.E();
			// Create the countries collection.
			CountryCollection countries = new CountryCollection();

			try
			{

				CacheHelper cacheHelper =new CacheHelper();
				countries= cacheHelper.GetCountries(ConnectionString);

				//this.LoadEntityCollection (loadSp, countries);
			}
			finally
			{
				T.X();
			}
			
			return countries;
		}

		/// <summary>
		/// Creates a country from the supplied data
		/// </summary>
		/// <param name="reader">The reader containing the data.</param>
		/// <returns></returns>
		protected override object CreateEntity(SafeDataReader reader)
		{
			T.E();
			Country newCountry = null;
			try
			{
				newCountry = new Country(reader.GetString("countryCode"), reader.GetString("country"));
			}
			finally
			{
				T.X();
			}
			return newCountry;

		}

        #endregion 

	}
}
