using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// Summary description for MarketIndexPersister.
	/// </summary>
	public class MarketIndexPersister : Persister
	{
        
        #region Constructor
        /// <summary>
        /// Use this class for retrieving all Market Index objects
        /// Default constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public MarketIndexPersister(string connectionString) : base(connectionString)
		{
		}
        #endregion

        #region Public methods

        /// <summary>
        /// Load the collection of market indices from the DB
        /// </summary>
        /// <returns type="MarketIndexCollection">A collection of market indices</returns>
        /// <exception cref="DatabaseException">Unable to load companies</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		public MarketIndexCollection LoadMarketIndices()
		{
        	T.E();
 			MarketIndexCollection marketIndices = new MarketIndexCollection();
			this.LoadEntityCollection("usp_MarketIndicesList",null,marketIndices);		
			T.X();

			return marketIndices;
		}

		/// <summary>
		/// Creates the entity from the data in the reader.
		/// </summary>
		/// <param name="safeReader">The reader to build the entity from.</param>
		/// <returns></returns>
		protected override object CreateEntity(SafeDataReader safeReader)
		{
			T.E();
			MarketIndex newIndex = MarketIndexFactory.CreateMarketIndex(
				safeReader.GetInt32("marketIndexID"), 
				safeReader.GetString("indexName"),
				safeReader.GetString("countryCode"),
				safeReader.GetString("currencyCode")
				);
			T.X();

			return newIndex;		
		}

		#endregion
	}
}
