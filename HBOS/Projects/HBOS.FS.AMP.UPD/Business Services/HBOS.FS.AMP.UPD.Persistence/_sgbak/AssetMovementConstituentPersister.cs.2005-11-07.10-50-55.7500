using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.BenchMark;
using HBOS.FS.AMP.UPD.Types.Funds;
using HBOS.FS.AMP.UPD.Types.Currency;
using HBOS.FS.AMP.UPD.Types.Snapshot;
using HBOS.FS.AMP.UPD.Types.StockMarketIndex;
using HBOS.FS.Support.Tex;
using Microsoft.ApplicationBlocks.Data;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// Persits Asset movement constiuent parts
	/// </summary>
	public class AssetMovementConstituentPersister : EntityPersister
	{
		#region Constructor

		/// <summary>
		/// Creates a new <see cref="AssetMovementConstituentPersister"/> instance.
		/// </summary>
		public AssetMovementConstituentPersister(string connectionString) : base(connectionString)
		{}

		#endregion

		#region Load methods 

		private bool m_loadForPricing = true;
		private string m_assetFundID = string.Empty;
		private CurrencyPricingPersister m_currencyPersister;


		/// <summary>
		/// Loads the all available benchmarks for all asset funds beloging to the specified group.
		/// </summary>
		/// <returns></returns>
		public AssetMovementConstituentCollection LoadAssetFundMovementConstituentsByGroup(int groupId)
		{
			T.E();

			//Clear the Cache of fund benchmarks so we get current data
			BenchmarkCache.Clear();

			AssetMovementConstituentCollection parts = new AssetMovementConstituentCollection();

			SqlParameter[] parameters = new SqlParameter[1];
			parameters[0] = new SqlParameter("@fundGroupId", SqlDbType.Int);

			m_loadForPricing = true;
			parameters[0].Value = groupId;

			m_currencyPersister = new CurrencyPricingPersister(this.ConnectionString);

			this.LoadEntityCollection("usp_BenchMarkSplitGetByFundGroupID", parameters, parts);
			T.X();
			return parts;
		}

		/// <summary>
		/// Loads the all available benchmarks.
		/// </summary>
		/// <returns></returns>
		public AssetMovementConstituentCollection LoadAssetFundMovementConstituents(string assetFundId, bool loadForPricing)
		{
			T.E();
			AssetMovementConstituentCollection parts = new AssetMovementConstituentCollection();

			SqlParameter[] parameters = new SqlParameter[1];
			parameters[0] = new SqlParameter("@cAssetFundID", SqlDbType.Char, 8);

			m_loadForPricing = loadForPricing;
			m_assetFundID = assetFundId;
			parameters[0].Value = m_assetFundID;

			m_currencyPersister = new CurrencyPricingPersister(this.ConnectionString);

			this.LoadEntityCollection("usp_BenchMarkSplitGetByAssetFundID", parameters, parts);
			T.X();
			return parts;
		}

		/// <summary>
		/// Loads all benchmarks in the system
		/// </summary>
		/// <returns></returns>
		public AssetMovementConstituentCollection LoadAllBenchmarks()
		{
			T.E();
			AssetMovementConstituentCollection results = new AssetMovementConstituentCollection();

			m_loadForPricing = false;

			this.LoadEntityCollection("usp_BenchMarkGetAll", results);

			T.X();
			return results;
		}


		/// <summary>
		/// Creates the entity from the data reader.
		/// </summary>
		/// <param name="safeReader">Reader to get the data from.</param>
		/// <returns></returns>
		protected override object CreateEntity(SafeDataReader safeReader)
		{
			T.E();
			
			AssetMovementConstituent newConstituent = null;
			try
			{
				//this benchmark should be populated with a fund or market index or other benchmarkable type				
				IBenchMark benchmark = null;

				if (!safeReader.IsNull("hiPortfolioCode"))
				{
					benchmark = loadFundBenchmark(safeReader);
				}
				else if (!safeReader.IsNull("marketIndexId"))
				{
					benchmark = loadStockMarketBenchmark(safeReader);
				}

				if (benchmark == null)
				{
					throw new DatabaseException("Cannot load the benchmark");
				}			

				decimal proportion = 0;
				if (safeReader.ColumnExists("proportion"))
				{
					proportion = safeReader.GetDecimal("proportion");
				}				

				newConstituent = new AssetMovementConstituent(proportion, benchmark);
				if (safeReader.ColumnExists("assetfundid"))
				{
					newConstituent.ParentAssetFund=new AssetFund();
					newConstituent.ParentAssetFund.AssetFundCode=safeReader.GetString("assetfundid");
				}
			}
			finally
			{
				T.X();
			}
			return newConstituent;

		}

		private IBenchMark loadFundBenchmark(SafeDataReader safeReader)
		{
			IBenchMark benchmark;

			if (m_loadForPricing)
			{
				//Get the benchmark from the cache, or create a new one
				if (!BenchmarkCache.ContainsKey(safeReader.GetString("hiPortfolioCode")))
				{
					//Create a new benchmark
					FundPricingPersister fundPersister = new FundPricingPersister(ConnectionString);
					benchmark = fundPersister.LoadSingleFund(safeReader.GetString("hiPortfolioCode"));
				
					//add the benchmark to the cache
					BenchmarkCache.Add(safeReader.GetString("hiPortfolioCode"), benchmark);
					T.Log(string.Format("Created: {0}", safeReader.GetString("hiPortfolioCode")));
				}
				else
				{
					//Reuse a cached benchmark
					benchmark =(IBenchMark)BenchmarkCache[safeReader.GetString("hiPortfolioCode")];					
					T.Log(string.Format("Reused: {0}", safeReader.GetString("hiPortfolioCode")));
				}
			}
			else
			{
				if (!safeReader.ColumnExists("BenchmarkName"))
				{
					FundStaticDataPersister fundPersister = new FundBenchMarkPersister(this.ConnectionString);
					benchmark = fundPersister.Load(safeReader.GetString("hiPortfolioCode"));

					//KAJ 02/11/05 - data for all benchmarks now returned in one hit
					//instead of proc per benchmark
					//load a static data type fund benchmark
				}
				else
				{
					Fund newFund = FundFactory.CreateFund(FundPersister.ResolveFundType( safeReader.GetString("fundType")));
					benchmark = newFund;
					newFund.HiPortfolioCode= safeReader.GetString("hiPortfolioCode");
					newFund.ShortName=safeReader.GetString("BenchmarkName");
					newFund.CompanyCode=safeReader.GetString("CompanyCode");
					newFund.Currency=new Currency(safeReader.GetString("CurrencyCode"));
				}
			}
			return benchmark;
		}

		private static System.Collections.Hashtable benchmarkCache=new System.Collections.Hashtable();

		/// <summary>
		/// 
		/// </summary>
		public static Hashtable BenchmarkCache
		{
			get { return benchmarkCache; }
		}

		private IBenchMark loadStockMarketBenchmark(SafeDataReader safeReader)
		{
			IBenchMark benchmark;
			StockMarketIndexPersister indexPersister = new StockMarketIndexPersister(this.ConnectionString);

			if (m_loadForPricing)
			{
				//Get the benchmark from the cache, or create a new one
				if (!BenchmarkCache.ContainsKey(safeReader.GetInt32("marketIndexId")))
				{
					//Create a new benchmark
					benchmark = new StockMarketIndex
						( 
						safeReader.GetInt32("marketIndexId"),
						safeReader.GetString("indexName"),
						safeReader.GetDecimal("currentValue"),
						safeReader.GetDecimal("previousValue"),
						m_currencyPersister.LoadRates(safeReader)
						);
									
					//add the benchmark to the cache
					BenchmarkCache.Add(safeReader.GetInt32("marketIndexId"),benchmark);
					T.Log(string.Format("Created: {0}", safeReader.GetString("indexName")));
				}
				else
				{
					//Reuse a cached benchmark
					benchmark =(IBenchMark)BenchmarkCache[safeReader.GetInt32("marketIndexId")];					
					T.Log(string.Format("Reused: {0}", safeReader.GetString("indexName")));
				}
			}
			else
			{
				if (!safeReader.ColumnExists("BenchmarkName"))
				{
					benchmark = indexPersister.LoadWithoutPricing(safeReader.GetInt32("marketIndexID"));
				}
				else
				{
					benchmark=new StockMarketIndex(
						safeReader.GetInt32("marketIndexId"),
						safeReader.GetString("BenchmarkName"),
						new Currency(safeReader.GetString("CurrencyCode")),
						safeReader.GetString("CountryCode")
						);
				}
							}
			return benchmark;
		}

		#endregion

		#region Save methods

		/// <summary>
		/// Associates the constituate parts with the specified Asset fund.
		/// </summary>
		///<param name="assetFundId">Identifier for the Asset Fund whose split is to be cleared</param>
		/// <param name="txn">Transaction to use</param>
		public void Clear(string assetFundId, SqlTransaction txn)
		{
			//				UPDATE	BenchmarkSplit
			//	SET		active = 1
		
				string spName = "usp_BenchmarkSplitClear";

				SqlParameter[] parameters = null;
				parameters = new SqlParameter[1];
				parameters[0] = new SqlParameter("@AssetFundID", SqlDbType.Char, 8);

				parameters[0].Value = assetFundId;
			try
			{
				SqlHelper.ExecuteNonQuery(txn, CommandType.StoredProcedure, spName, parameters);


			}
			catch (SqlException ex)
			{
				ThrowDBException(ex, spName, parameters);
			}
		}


		/// <summary>
		/// Associates the constituate parts with the specified Asset fund.
		/// </summary>
		/// <param name="parts">Constituents to save, those marked IsDeleted=true will not be saved</param>
		/// <param name="snapshot">Snapshot to associate collection with</param>
		/// <param name="txn">Transaction to use</param>
		public void Save(AssetMovementConstituentCollection parts, Snapshot snapshot, SqlTransaction txn)
		{
			string spName = "usp_BenchmarkSplitCreate";

			SqlParameter[] parameters = null;
			parameters = new SqlParameter[5];
			parameters[0] = new SqlParameter("@cAssetFundID", SqlDbType.Char, 8);
			parameters[1] = new SqlParameter("@lSnapshotID", SqlDbType.BigInt);
			parameters[2] = new SqlParameter("@cHiPortfolioCode", SqlDbType.Char, 10);
			parameters[3] = new SqlParameter("@iMarketIndexID", SqlDbType.Int);
			parameters[4] = new SqlParameter("@dProportion", SqlDbType.Decimal);

			parameters[1].Value = snapshot.Id;

			foreach (AssetMovementConstituent part in parts)
			{
				if (!part.IsDeleted)
				{
					try
					{
						string assetFundCode = "";
						if (assetFundCode == "") assetFundCode = part.ParentAssetFund.AssetFundCode;
						parameters[0].Value = assetFundCode;

						if (part.BenchMark is Fund)
						{
							Fund fund = (Fund) part.BenchMark;
							parameters[2].Value = fund.HiPortfolioCode;
							parameters[3].Value = Convert.DBNull;
						}
						else if (part.BenchMark is StockMarketIndex)
						{
							StockMarketIndex index = (StockMarketIndex) part.BenchMark;
							parameters[2].Value = Convert.DBNull;
							parameters[3].Value = index.MarketIndexId;
						}

						parameters[4].Value = part.Proportion;

						SqlHelper.ExecuteNonQuery(txn, CommandType.StoredProcedure, spName, parameters);


					}
					catch (SqlException ex)
					{
						ThrowDBException(ex, spName, parameters);
					}
				}
			}

		}

		#endregion
	}
}