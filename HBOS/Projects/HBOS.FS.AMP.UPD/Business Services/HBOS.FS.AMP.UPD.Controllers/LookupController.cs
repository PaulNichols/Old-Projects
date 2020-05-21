using System;
using System.Collections;
using HBOS.FS.AMP.UPD.Persistence;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.Countries;
using HBOS.FS.AMP.UPD.Types.Currency;
using HBOS.FS.AMP.UPD.Types.Funds;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Controllers
{
	/// <summary>
	/// LookupController - Controller to retrieve various lookups
	/// </summary>
	public class LookupController
	{
		/// <summary>
		/// Load all the Systems that we know about
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="companyCode">Company to load List for</param>
		/// <returns></returns>
		public  static ExternalSystemIDCollection LoadSystems(string connectionString,string companyCode)
		{
			T.E();
			ExternalSystemIDCollection extSysIdColl = null;
			try
			{
				ExternalSystemIDPersister persister = new ExternalSystemIDPersister(connectionString);
				extSysIdColl = persister.LoadSystems(companyCode);
			}
			finally
			{
				T.X();
			}
			return extSysIdColl;
		}

		/// <summary>
		/// Load all the Currencies that we know about
		/// </summary>
		/// <param name="connectionString"></param>
		/// <returns></returns>
		public  CurrencyCollection LoadCurrencies(string connectionString)
		{
			T.E();
			CurrencyCollection currColl = null;
			try
			{
				CurrencyPersister persister = new CurrencyPersister(connectionString);
				currColl = persister.Load();
				persister=null;
			}
			finally
			{
				T.X();
			}
			return currColl;
		}

		/// <summary>
		/// Loads all the price files.
		/// </summary>
		/// <param name="connectionString">Connection string.</param>
		/// <param name="companyCode">Only load the files for the specified country</param>
		/// <returns></returns>
		public static IList LoadPriceFiles(string connectionString,string companyCode)
		{
			T.E();
			PriceFileCollection filesCollection = null;
			try
			{
				PriceFileStaticDataPersister persister = new PriceFileStaticDataPersister(connectionString);
				filesCollection  = persister.LoadPriceFiles(companyCode);
			}
			finally
			{
				T.X();
			}
			return filesCollection ;
		}

		/// <summary>
		/// 
		/// </summary>
		public void FlushCountries()
		{
			CountryPersister persister = new CountryPersister("");
			 persister.FlushCountries();
		}

		/// <summary>
		/// 
		/// </summary>
		public void FlushCurrencies()
		{
			CurrencyPersister persister = new CurrencyPersister("");
			persister.FlushCurrencies();
		}
	
		/// <summary>
		/// Load all the Countries that we know about
		/// </summary>
		/// <param name="connectionString"></param>
		/// <returns></returns>
		public  IList LoadCountries(string connectionString)
		{
			T.E();
			CountryCollection countryColl = null;
			try
			{
				CountryPersister persister = new CountryPersister(connectionString);
				countryColl  = persister.LoadCountries();
				persister=null;
			}
			finally
			{
				T.X();
			}
			return countryColl ;
		}

		/// <summary>
		/// Load all the stock market Indices
		/// TODO this is loading up an old legacy type, but is still used by Import
		/// </summary>
		/// <param name="connectionString">Database connection string</param>
		/// <returns>Populated indices</returns>
		public  static StockMarketCollection LoadStockMarketIndices(string connectionString)
		{
			T.E();
			StockMarketCollection indices = null;

			try
			{
				StockMarketPersister persister = new StockMarketPersister(connectionString);
				indices = persister.LoadMarketIndices();
			}
			finally
			{
				T.X();
			}
			return indices;
		}

//		/// <summary>
//		/// A list of refactored object types that are only partially completed
//		/// </summary>
//		/// <param name="connectionString"></param>
//		/// <returns></returns>
//		public  static WeightedMovementCollection LoadStockMarketIndicesForAssetFundStaticMaintenance(string connectionString)
//		{
//			T.E();
//			WeightedMovementCollection indices = null;
//
//			try
//			{
//				WeightedIndexPersister persister = new WeightedIndexPersister(connectionString);
//				indices = persister.LoadMarketIndicesForAssetFundStaticMaintenance();
//			}
//			finally
//			{
//				T.X();
//			}
//			return indices;
//
//		}


		
	}
}