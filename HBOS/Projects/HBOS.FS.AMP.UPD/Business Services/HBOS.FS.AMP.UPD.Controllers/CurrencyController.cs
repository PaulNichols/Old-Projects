using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Persistence;
using HBOS.FS.AMP.UPD.Types.Currency;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Controllers
{
	/// <summary>
	/// Controller object for all things currency orientated.  
	/// This will handle calls to the data layer.
	/// This is used to keep the logical layers seperate.  
	/// </summary>
	public class CurrencyController
	{
		#region Constructor
		/// <summary>
		/// Creates a new <see cref="CurrencyController"/> instance.
		/// </summary>
		public CurrencyController()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#endregion

		#region Load methods
		/// <summary>
		/// Loads the specified currency.
		/// </summary>
		/// <param name="currencyCode">Currency code.</param>
		/// <param name="connectionString"></param>
		/// <returns>Populated currency object</returns>
		/// <exception cref="DatabaseException">Unable to load currency</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		public static Currency LoadCurrency(string currencyCode, string connectionString)
		{
			T.E();
			Currency currency = null;
			try
			{
				CurrencyStaticDataPersister persister = new CurrencyStaticDataPersister(connectionString);
				currency = persister.Load(currencyCode);
			}
			finally
			{
				T.X();
			}
			return currency;
		}

		#endregion

		#region Update methods

		/// <summary>
		/// Updates the currency item to the database.
		/// </summary>
		/// <param name="currency">Currency object.</param>
		/// <param name="connectionString"></param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public static void UpdateCurrency(Currency currency, string connectionString)
		{
			T.E();
			try
			{
				CurrencyStaticDataPersister persister = new CurrencyStaticDataPersister(connectionString);
				persister.Save(currency);
			}
			finally
			{
				T.X();
			}
		}

		#endregion
	}
}
