using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Persistence;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Controllers
{
	/// <summary>
	/// Controller object for all things StockMarket orientated.  
	/// This will handle calls to the data layer.
	/// This is used to keep the logical layers seperate.  
	/// </summary>
	public class StockMarketController
	{
		#region Constructor
		/// <summary>
		/// Creates a new <see cref="StockMarketController"/> instance.
		/// </summary>
		public StockMarketController()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#endregion

		#region Load methods
		/// <summary>
		/// Loads the specified StockMarket.
		/// </summary>
		/// <param name="stockMarketIndex">StockMarket Unique Index.</param>
		/// <param name="connectionString"></param>
		/// <returns>Populated StockMarket object</returns>
		/// <exception cref="DatabaseException">Unable to load StockMarket</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		public static StockMarket LoadStockMarket(int stockMarketIndex, string connectionString)
		{
			T.E();
			StockMarket StockMarket = null;
			try
			{
				StockMarketPersister persister = new StockMarketPersister(connectionString);
				StockMarket = persister.Load(stockMarketIndex);
			}
			finally
			{
				T.X();
			}
			return StockMarket;
		}

		#endregion

		#region Update methods

		/// <summary>
		/// Updates the StockMarket item to the database.
		/// </summary>
		/// <param name="StockMarket">StockMarket object.</param>
		/// <param name="connectionString"></param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public static void UpdateStockMarket(StockMarket StockMarket, string connectionString)
		{
			T.E();
			try
			{
				StockMarketPersister persister = new StockMarketPersister(connectionString);
				persister.Save(StockMarket);
			}
			finally
			{
				T.X();
			}
		}

		#endregion
	}
}
