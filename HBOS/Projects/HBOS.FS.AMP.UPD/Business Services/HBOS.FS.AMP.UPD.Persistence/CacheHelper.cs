using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Types.Countries;
using HBOS.FS.AMP.UPD.Types.Currency;
using HBOS.FS.AMP.UPD.Types.FundGroups;
using HBOS.FS.Support.Tex;
using Microsoft.ApplicationBlocks.Cache;
using Microsoft.ApplicationBlocks.Cache.Expirations;
using Microsoft.ApplicationBlocks.Data;


//using HBOS.FS.AMP.UPD.Types;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// Provides strongly typed accessors to catched data.
	/// </summary>
	/// <remarks>
	/// In order to expand this class for new cached collections the following steps must be followed
	/// <list type="ul">
	/// <item>The object to be cached, and any members in that object must be Serializable".</item>
	/// <item>A get accessor must be created to retrieve the strongly typed item, checking that the item exists 
	/// in the cache. If the item does not exist it should be retrieved and cached as appropriate.</item>
	/// <item>A set accessor must be created to remove any existing cache entry then add the new value back
	/// into the cache.</item>
    /// <item>A private method must be created to retrive the item from the database and set the Cache using 
    /// the public set for the item.</item>
    /// <item>If the cached item is to be periodically a function must be created to reload the object and 
    /// set as the <see cref="CacheItemRemovedCallback"/> parameter during the object addition to the cache.</item>
    /// <item>Item removal must be added to the Flush method.</item>
   ///</list>
   ///</remarks>
   ///
	public  class CacheHelper
	{
		private CacheManager cacheManager = CacheManager.GetCacheManager();
		private static string m_ConnectionString;
		private static string m_CompanyCode;

        /// <summary>
        /// Constructs a new instance of <see cref="CacheHelper"/>.
        /// </summary>
         static CacheHelper()
        {
        }

		/// <summary>
		/// Returns a standard array of cache expiration policies, containing a single policy expiring 
		/// in 5 minutes time.
		/// </summary>
		/// <returns>A standard array of cache expiration policies, containing a single policy expiring 
		/// in 5 minutes time.</returns>
		private ICacheItemExpiration[] getExpiryArrayHourly()
		{
			ICacheItemExpiration[] expiryArray = new ICacheItemExpiration[1];
			expiryArray[0] = new AbsoluteTime(DateTime.Now.AddMinutes(60));
			return (expiryArray);
		}

        /// <summary>
        /// Returns a standard array of cache expiration policies, containing a single policy expiring 
        /// in 5 minutes time.
        /// </summary>
        /// <returns>A standard array of cache expiration policies, containing a single policy expiring 
        /// in 5 minutes time.</returns>
        private ICacheItemExpiration[] getExpiryArrayDaily()
        {
            ICacheItemExpiration[] expiryArray = new ICacheItemExpiration[1];
            expiryArray[0] = new AbsoluteTime(DateTime.Now.AddDays(1));
            return (expiryArray);
        }

        /// <summary>
        /// Retrieves the fund group list from the database and places it in the cache.
        /// </summary>
        private void refillFundGroupsCacheFromDatabase(string connectionString,string companyCode)
        {
            // Retrieve the fund group collection.
             FundGroupCachePersister persister =new FundGroupCachePersister(connectionString);
            Types.FundGroups.FundGroupCollection fundGroups  = persister.LoadFundGroupsByCompany(companyCode);            
            SetFundGroups(fundGroups);
        }

		/// <summary>
		/// Retrieves the Holidays list from the database and places it in the cache.
		/// </summary>
		private void refillHolidaysCacheFromDatabase(string connectionString)
		{
			// Retrieve the fund group collection.
			FundCachePersister persister =new FundCachePersister(connectionString);
			Hashtable holidays  = persister.LoadHolidays();            
			SetHolidays(holidays);
		}

		/// <summary>
		/// Retrieves the currencies list from the database and places it in the cache.
		/// </summary>
		private void refillCurrenciesCacheFromDatabase(string connectionString)
		{
			// Retrieve the fund group collection.
			CurrenciesCachePersister persister =new CurrenciesCachePersister(connectionString);
			Types.Currency.CurrencyCollection currencies  = persister.Load();            
			SetCurrencies(currencies);
		}

		/// <summary>
		/// Retrieves the countries list from the database and places it in the cache.
		/// </summary>
		private void refillCountriesCacheFromDatabase(string connectionString)
		{
			// Retrieve the fund group collection.
			CountriesCachePersister persister =new CountriesCachePersister(connectionString);
			Types.Countries.CountryCollection countries  = persister.LoadCountries();            
			SetCountries(countries);
		}

		
		/// <summary>
		/// A cached Hashtable of Holidays.
		/// </summary>
		/// <value>A cached Holidays Hashtable</value>
		public Hashtable GetHolidays(string connectionString)
		{
                        
			m_ConnectionString=connectionString;
			if (null == cacheManager.GetData("Holidays"))
				lock (cacheManager)
				{
					refillHolidaysCacheFromDatabase(connectionString);
				}
			return ((Hashtable)cacheManager.GetData("Holidays"));
		}

		/// <summary>
		/// A cached CurrencyCollection.
		/// </summary>
		/// <value>A cached CurrencyCollection</value>
		public Types.Currency.CurrencyCollection GetCurrencies(string connectionString)
		{
                        
			m_ConnectionString=connectionString;
			if (null == cacheManager.GetData("Currencies"))
				lock (cacheManager)
				{
					refillCurrenciesCacheFromDatabase(connectionString);
				}
			return ((Types.Currency.CurrencyCollection)cacheManager.GetData("Currencies"));
		}

        /// <summary>
        /// A cached FundGroupCollection.
        /// </summary>
        /// <value>A cached FundGroupCollection</value>
        public Types.FundGroups.FundGroupCollection GetFundGroups(string connectionString,string companyCode)
        {
                        
            m_ConnectionString=connectionString;
			m_CompanyCode=companyCode;
            if (null == cacheManager.GetData("FundGroups"))
                lock (cacheManager)
                {
                    refillFundGroupsCacheFromDatabase(connectionString,companyCode);
                }
                return ((Types.FundGroups.FundGroupCollection)cacheManager.GetData("FundGroups"));
         }

		/// <summary>
		/// A cached CountriesCollection.
		/// </summary>
		/// <value>A cached CountriesCollection</value>
		public Types.Countries.CountryCollection GetCountries(string connectionString)
		{
                        
			m_ConnectionString=connectionString;
			if (null == cacheManager.GetData("Countries"))
				lock (cacheManager)
				{
					refillCountriesCacheFromDatabase(connectionString);
				}
			return ((Types.Countries.CountryCollection)cacheManager.GetData("Countries"));
		}
       
		/// <summary>
		/// Sets the holidays.
		/// </summary>
		/// <param name="holidays">holidays.</param>
		public void SetHolidays(Hashtable holidays)
		{
			// Create a callback to trigger when the object leaves the cache.
			CacheItemRemovedCallback holidayCallback = new CacheItemRemovedCallback(this.OnHolidaysRemove);
			// Lock the cache to prepare for addition.
			lock(cacheManager)
			{
				if (null != cacheManager.GetItem("Holidays"))
					cacheManager.Remove("Holidays");                    
				this.cacheManager.Add("Holidays", holidays, getExpiryArrayDaily(), 
					CacheItemPriority.NotRemovable, holidayCallback);
			}            
		}


		/// <summary>
		/// Sets the fund groups.
		/// </summary>
		/// <param name="fundGroups">Fund groups.</param>
		public void SetFundGroups(Types.FundGroups.FundGroupCollection fundGroups)
        {
            // Create a callback to trigger when the object leaves the cache.
            CacheItemRemovedCallback fundGroupsCallback = new CacheItemRemovedCallback(this.OnFundGroupsRemove);
            // Lock the cache to prepare for addition.
            lock(cacheManager)
            {
                if (null != cacheManager.GetItem("FundGroups"))
                    cacheManager.Remove("FundGroups");                    
                this.cacheManager.Add("FundGroups", fundGroups, getExpiryArrayHourly(), 
                    CacheItemPriority.NotRemovable, fundGroupsCallback);
            }            
        }

		/// <summary>
		/// Sets the countries colection.
		/// </summary>
		/// <param name="countries">Countries to persist.</param>
		public void SetCountries(Types.Countries.CountryCollection countries)
		{
			// Create a callback to trigger when the object leaves the cache.
			CacheItemRemovedCallback countriesCallback = new CacheItemRemovedCallback(this.OnCountriesRemove);
			// Lock the cache to prepare for addition.
			lock(cacheManager)
			{
				if (null != cacheManager.GetItem("Countries"))
					cacheManager.Remove("Countries");                    
				try
				{this.cacheManager.Add("Countries", countries, getExpiryArrayHourly(), 
					CacheItemPriority.NotRemovable, countriesCallback);}
				catch (SystemException ex)
				{System.Console.WriteLine(ex);}
			}            
		}
        
		/// <summary>
		/// Sets the currency colection.
		/// </summary>
		/// <param name="currencies">Currencies to persist.</param>
		public void SetCurrencies(Types.Currency.CurrencyCollection currencies)
		{
			// Create a callback to trigger when the object leaves the cache.
			CacheItemRemovedCallback currenciesCallback = new CacheItemRemovedCallback(this.OnCurrenciesRemove);
			// Lock the cache to prepare for addition.
			lock(cacheManager)
			{
				if (null != cacheManager.GetItem("Currencies"))
					cacheManager.Remove("Currencies");                    
				this.cacheManager.Add("Currencies", currencies, getExpiryArrayHourly(), 
					CacheItemPriority.NotRemovable, currenciesCallback);
			}            
		}

		/// <summary>
		/// Callback when the "Holidays" cache is emptied.
		/// </summary>
		/// <param name="key">Cache key name</param>
		/// <param name="cause">Removal Cause</param>
		private void OnHolidaysRemove(string key, CacheItemRemoveCause cause)
		{
			if ("Holidays" == key && CacheItemRemoveCause.Removed != cause)
				refillHolidaysCacheFromDatabase(m_ConnectionString);

		}

        /// <summary>
        /// Callback when the "FundGroups" cache is emptied.
        /// </summary>
        /// <param name="key">Cache key name</param>
        /// <param name="cause">Removal Cause</param>
        private void OnFundGroupsRemove(string key, CacheItemRemoveCause cause)
        {
            if ("FundGroups" == key && CacheItemRemoveCause.Removed != cause)
                refillFundGroupsCacheFromDatabase(m_ConnectionString,m_CompanyCode);

        }

		/// <summary>
		/// Callback when the "Currencies" cache is emptied.
		/// </summary>
		/// <param name="key">Cache key name</param>
		/// <param name="cause">Removal Cause</param>
		private void OnCurrenciesRemove(string key, CacheItemRemoveCause cause)
		{
			if ("Currencies" == key && CacheItemRemoveCause.Removed != cause)
				refillCurrenciesCacheFromDatabase(m_ConnectionString);

		}

		/// <summary>
		/// Callback when the "Countries" cache is emptied.
		/// </summary>
		/// <param name="key">Cache key name</param>
		/// <param name="cause">Removal Cause</param>
		private void OnCountriesRemove(string key, CacheItemRemoveCause cause)
		{
			if ("Countries" == key && CacheItemRemoveCause.Removed != cause)
				refillCountriesCacheFromDatabase(m_ConnectionString);

		}

        /// <summary>
        /// Flush all helper created caches
        /// </summary>
        public void FlushFundGroups()
        {
            lock (cacheManager)
            {
				cacheManager.Remove("FundGroups");
            }
        }

		/// <summary>
        /// Flush all helper created caches
        /// </summary>
        public void FlushCountries()
        {
            lock (cacheManager)
            {
				cacheManager.Remove("Countries");
            }
        }

		/// <summary>
		/// Flush all helper created caches
		/// </summary>
		public void FlushCurrencies()
		{
			lock (cacheManager)
			{
				cacheManager.Remove("Currencies");
			}
		}

		private class CurrenciesCachePersister:CurrencyPersister
		{
			public CurrenciesCachePersister(string connectionString) : base(connectionString)
			{
			}

			/// <summary>
			/// Returns the collection of Currency objects.
			/// </summary>
			/// <returns>A collection of matching Currencies.</returns>
			/// <exception cref="InvalidSqlParameterException">Thrown if one of the parameters was of the incorrect value, type or length; or was null</exception>
			/// <exception cref="DatabaseException">Thrown if other unclassified Sql error occurs</exception>
			public override CurrencyCollection Load()
			{
				T.E();
				const string loadSp = "usp_CurrenciesList";

				CurrencyCollection currencies  = null;
				try
				{
					currencies = new CurrencyCollection();

					this.LoadEntityCollection(loadSp, new  SqlParameter[0], currencies );

				}
				finally
				{
					T.X();
				}
				return currencies;
			}
		}
		
		private class CountriesCachePersister:CountryPersister
		{
			public CountriesCachePersister(string connectionString) : base(connectionString)
			{
			}

			/// <summary>
			/// Returns the collection of Country objects.
			/// </summary>
			/// <returns>A collection of matching Countries.</returns>
			/// <exception cref="InvalidSqlParameterException">Thrown if one of the parameters was of the incorrect value, type or length; or was null</exception>
			/// <exception cref="DatabaseException">Thrown if other unclassified Sql error occurs</exception>
			public override CountryCollection LoadCountries()
			{
				T.E();
				const string loadSp = "usp_CountriesList";

				CountryCollection countries  = null;
				try
				{
					countries = new CountryCollection();

					this.LoadEntityCollection(loadSp, new  SqlParameter[0], countries );

				}
				finally
				{
					T.X();
				}
				return countries;
			}
		}

		private class FundCachePersister:FundStaticDataPersister
		{
			public FundCachePersister(string connectionString):base(connectionString)
			{
			}

			/// <summary>
			/// Loads a list of dates that are UK bank holidays
			/// </summary>
			/// <returns></returns>
			public new Hashtable LoadHolidays ()
			{
				SqlDataReader hols = SqlHelper.ExecuteReader (ConnectionString, CommandType.StoredProcedure, "usp_HolidaysGet");
				Hashtable returnCollection = new Hashtable();

				while (hols.Read())				
				{
					DateTime holDate = (DateTime) hols.GetValue(hols.GetOrdinal("HolidayDate"));
					returnCollection.Add (holDate, holDate);
				}
				return returnCollection;
			}
		}

		private class FundGroupCachePersister:FundGroupPersister
		{
			public FundGroupCachePersister(string connectionString) : base(connectionString)
			{
			}

			/// <summary>
			/// Returns the collection of Fund group objects for the given company.
			/// </summary>
			/// <param name="companyCode">The ID of the company for which the fund groups are to be retrieved.</param>
			/// <returns>A collection of matching fund groups.</returns>
			/// <exception cref="InvalidSqlParameterException">Thrown if one of the parameters was of the incorrect value, type or length; or was null</exception>
			/// <exception cref="DatabaseException">Thrown if other unclassified Sql error occurs</exception>
			public override FundGroupCollection LoadFundGroupsByCompany(string companyCode)
			{
				T.E();
				FundGroupCollection fundGroups = null;
				try
				{
					SqlParameter[] spParams = new SqlParameter[1];

					spParams[0] = new SqlParameter("@sCompanyCode", SqlDbType.VarChar, 10);
					spParams[0].Value = companyCode;

					fundGroups = new FundGroupCollection();

					this.LoadEntityCollection("usp_FundGroupsGetForCompanyCode", spParams, fundGroups);

				}
				finally
				{
					T.X();
				}
				return fundGroups;
			}
		}
    }
}