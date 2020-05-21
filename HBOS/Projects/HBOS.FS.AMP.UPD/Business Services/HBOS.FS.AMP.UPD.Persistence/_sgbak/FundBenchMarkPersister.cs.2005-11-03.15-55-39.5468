using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.Funds;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// Data persister object used to move fund data asociated with the benchmark split 
	/// in Asset fund data
	/// maintenance to and from the database.
	/// </summary>
	public class FundBenchMarkPersister : FundStaticDataPersister
	{
		#region Constructor

		/// <summary>
		/// Constructor used to initialise the ConnectionString property.
		/// </summary>
		/// <param name="connectionString">The connection string to use for data access.</param>
		public FundBenchMarkPersister(string connectionString) : base(connectionString)
		{
			//
			// TODO: Add constructor logic here
			//
			T.E();
			T.X();
		}

		#endregion

		#region Entity methods

		/// <summary>
		/// Creates a fund object from the supplied data.
		/// </summary>
		/// <param name="safeReader">The reader containing the data.</param>
		/// <returns></returns>
		/// <exception cref="SchemaMismatchException">Column missing from data returned by stored procedure</exception>
		protected override object CreateEntity(SafeDataReader safeReader)
		{
			T.E();

			Fund newFund = null;

			try
			{
				AssetFund assetFund = this.loadAssetFundForFund(safeReader.GetString("AssetFundID"));

				// Create a new fund object
				newFund = this.LoadNewFund(safeReader, assetFund);
			}
			finally
			{
				T.X();
			}

			return newFund;
		}

		#endregion

		/// <summary>
		/// Loads the parent asset fund with the minimum details needed to access 
		/// the currency code which is needed in the benchmark screen.
		/// </summary>
		/// <param name="assetFundID">Asset fund ID.</param>
		/// <returns></returns>
		protected override AssetFund LoadParentAssetFund(string assetFundID)
		{
			AssetFund parentAssetFund;
			//Load the asset fund specifically for this fund
			AssetFundStaticDataPersister assetFundPersister = new AssetFundBenchMarkPersister(this.ConnectionString);
			parentAssetFund = assetFundPersister.Load(assetFundID);
			return parentAssetFund;
		}

		/// <summary>
		/// Common method to populate a fund object.
		/// </summary>
		/// <remarks>
		/// This will load the correct level of data fo rthe required fund imformation.
		/// </remarks>
		/// <param name="safeReader">Safe reader.</param>
		/// <param name="assetFund">Asset fund.</param>
		/// <returns></returns>
		protected Fund LoadNewFund(SafeDataReader safeReader, AssetFund assetFund)
		{
			FundFactory.FundParameters fundParameters = new FundFactory.FundParameters();

			// Populate locals with data from the reader
			fundParameters.HiPortfolioCode = safeReader.GetString("HiPortFolioCode");
			fundParameters.ShortName = safeReader.GetString("ShortName");

			fundParameters.Timestamp = safeReader.GetTimestamp("ts");

			// Resolve the database type field.
			fundParameters.FundType = ResolveFundType(safeReader.GetString("FundType"));

			// Resolve the database status field.
			fundParameters.FundStatus = ResolveFundStatus(safeReader.GetInt32("statusID"));

			fundParameters.CompanyCode = null;
			if (safeReader.ColumnExists("companyCode")) //is for static data export only
			{
				fundParameters.CompanyCode = safeReader.GetString("companyCode");
			}

			fundParameters.IsBenchmarkable = false;

			if (safeReader.ColumnExists("isBenchmarkable"))
			{
				fundParameters.IsBenchmarkable = safeReader.GetBoolean("isBenchmarkable");
			}
			//
			// Now Create a new Fund object.
			//
			Fund newFund = null;

			newFund = FundFactory.CreateFund(fundParameters);


			// Clear dirty flag which is being set when assign values through 
			// the properties rather than the FundFactory
			newFund.IsDirty = false;

			return newFund;
		}

	}
}