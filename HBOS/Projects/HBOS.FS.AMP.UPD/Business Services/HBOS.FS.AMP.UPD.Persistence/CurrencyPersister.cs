using System;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Types.Currency;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// Persister for the Currency class.
	/// </summary>
	public class CurrencyPersister : EntityPersister
	{
		#region Constructor

		/// <summary>
		/// Creates a new <see cref="CurrencyPersister"/> instance.
		/// </summary>
		/// <param name="connectionString">Database connection string.</param>
		public CurrencyPersister(string connectionString) : base(connectionString)
		{
		}

		#endregion

		#region public methods

		/// <summary>
		/// 
		/// </summary>
		public void FlushCurrencies()
		{
			CacheHelper cacheHelper =new CacheHelper();
			cacheHelper.FlushCurrencies();
		}

		/// <summary>
		/// Loads the collection of currencies from the database.
		/// </summary>
		/// <returns>A collection of all our currencies</returns>
		/// <exception cref="DatabaseException">Unable to load currency</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		public virtual CurrencyCollection Load()
		{
			T.E();

			CurrencyCollection currencies = null;

			try
			{
				currencies = new CurrencyCollection();
				CacheHelper cacheHelper =new CacheHelper();
				currencies= cacheHelper.GetCurrencies(ConnectionString);

			//	this.LoadEntityCollection("usp_CurrenciesList", currencies);
			}
			finally
			{
				T.X();
			}
			return currencies;
		}

		#endregion

		#region Entity methods

		/// <summary>
		/// Creates the entity from the data reader.
		/// </summary>
		/// <param name="safeReader">Reader to get the data from.</param>
		/// <returns></returns>
		protected override object CreateEntity(SafeDataReader safeReader)
		{
			T.E();

			Currency currency = null;
			try
			{
				currency = new Currency(safeReader.GetString("currencyCode"),
				                        safeReader.GetString("currency"),
				                        safeReader.GetTimestamp("ts"));
			}
			finally
			{
				T.X();
			}
			return currency;
		}

		#endregion

	
	}
}