using System;
using System.Data;
using System.Data.SqlClient;
using HBOS.FS.AMP.UPD.Types.Currency;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// Summary description for CurrencyPricingPersister.
	/// </summary>
	public class CurrencyPricingPersister : EntityPersister
	{
		private bool m_allowGlobalMarket = false;
		private static System.Collections.Hashtable currencyCache = 
			new System.Collections.Hashtable();
		
		#region Constructor

		/// <summary>
		/// Creates a new <see cref="CurrencyPricingPersister"/> instance.
		/// </summary>
		/// <param name="connectionString">Connection string.</param>
		public CurrencyPricingPersister(string connectionString) : base(connectionString)
		{
			currencyCache.Clear();
		}

		#endregion

		#region Properties

		/// <summary>
		/// Sets a value indicating whether [allow global market].
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [allow global market]; otherwise, <c>false</c>.
		/// </value>
		public bool AllowGlobalMarket
		{
			set
			{
				m_allowGlobalMarket = value;
			}
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Loads the rates from a data reader.
		/// </summary>
		/// <param name="safeReader">Safe reader.</param>
		/// <returns>Currency object</returns>
		internal Currency LoadRates(SafeDataReader safeReader)
		{
			return this.loadCurrencyRates(safeReader.GetString("currencyCode"), string.Empty, safeReader);
		}

		/// <summary>
		/// Loads the current an previous currency rates for the specified currency and country codes.
		/// </summary>
		/// <param name="currencyCode">Currency code.</param>
		/// <param name="countryCode">Country code.</param>
		/// <returns>Currency object</returns>
		public Currency LoadRates(string currencyCode, string countryCode)
		{
			return this.loadCurrencyRates(currencyCode, countryCode, null);
		}

		#endregion

		#region Private methods

		private Currency loadCurrencyRates(string currencyCode, string countryCode, SafeDataReader safeReader)
		{
			// Create the currency object.
			Currency currency = null;
			try
			{
				// Test the currency for sterling or being empty
				// When a stock market or asset fund then deal with it politley
				if (currencyCode.ToUpper() == "GBP")
				{
					currency = Currency.GBPound;
				}
				else if (currencyCode.Trim().Length == 0 || currencyCode.Equals(string.Empty))
				{
					if (m_allowGlobalMarket)
					{
						currency = Currency.GlobalMarket;
					}
					else
					{
						currency = null;
					}
				}
				else
				{
					// Get the rates from the database
					if (null == safeReader)
					{
						const string spName = "usp_CurrencyGetRatesForCompany";
						SqlParameter[] spParameters = new SqlParameter[2];

						// Set up and populate the stored procedure parameters.
						spParameters[0] = new SqlParameter("@currencyCode", SqlDbType.VarChar, 10);
						spParameters[1] = new SqlParameter("@companyCode", SqlDbType.VarChar, 10);

						spParameters[0].Value = currencyCode;
						spParameters[0].Value = countryCode;

						T.Log("Get the rates from the database. Execute stored proc: " + spName);
						try
						{
							currency = (Currency) this.LoadEntity(spName, spParameters);
						}
						catch (SqlException ex)
						{
							ThrowDBException(ex, spName, spParameters);
						}
						finally
						{
							T.Log("Finished getting the rates from the database");
						}
					}
					// Get the rates from the data reader.  
					// This stops multiple calls to the DB should the client be iterating
					// through a list of sorts.
					else
					{
						currency = (Currency) this.CreateEntity(safeReader);
					}

					// Test for valid object
					if (currency == null)
					{
						throw new ArgumentException(string.Format("Failed to load rates for currency ({0})", currencyCode));
					}
				}
			}
			finally
			{
				T.DumpObjectLite(currency);
				T.X();
			}

			return currency;			
		}

		#endregion

		#region Entity methods

		/// <summary>
		/// Creates the currency entity object.
		/// </summary>
		/// <param name="safeReader">Safe reader.</param>
		/// <returns></returns>
		protected override object CreateEntity(SafeDataReader safeReader)
		{
			T.E();

			Currency currency = null;
			try
			{
				//Get the currency from the cache, or create a new one
				if (!currencyCache.ContainsKey(safeReader.GetString("currencyCode")))
				{
					//Create a new currency
					if (safeReader.ColumnExists("currencyCode"))
					{
						currency = new Currency(safeReader.GetString("currencyCode"),
							safeReader.GetDecimal("currencyCurrentRate"),
							safeReader.GetDecimal("currencyPreviousRate"));
					}
					else
					{
						currency = new Currency();
					}

					//add the currency to the cache
					currencyCache.Add(safeReader.GetString("currencyCode"), currency);
					T.Log(string.Format("Created: {0}", safeReader.GetString("currencyCode")));
				}
				else
				{
					//Reuse a cached currency
					currency = (Currency)currencyCache[safeReader.GetString("currencyCode")];					
					T.Log(string.Format("Reused: {0}", safeReader.GetString("currencyCode")));
				}
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