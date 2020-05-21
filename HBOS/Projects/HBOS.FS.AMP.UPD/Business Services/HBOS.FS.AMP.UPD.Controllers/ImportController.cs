using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using HBOS.FS.AMP.Data.Persister.Interface;
using HBOS.FS.AMP.Data.Transfer;
using HBOS.FS.AMP.Data.Types;
using HBOS.FS.AMP.Data.Validator.Interface;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Persistence;
using HBOS.FS.AMP.UPD.Persistence.IDataRowPersisters;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.Currency;
using HBOS.FS.AMP.UPD.Types.Funds;
using HBOS.FS.AMP.UPD.Types.Lookups;
using HBOS.FS.AMP.UPD.Types.Snapshot;
using HBOS.FS.Data.FileReaders;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Controllers
{
	/// <summary>
	/// ImportController - Used to manage File Import functionality
	/// </summary>
	public class ImportController : IDisposable
	{
		#region enum

		/// <summary>
		/// Import File Type
		/// </summary>
		public enum ImportFileType : int
		{
			/// <summary>
			/// Stock Market Indices (0)
			/// </summary>
			StockMarketIndices = 0,
			/// <summary>
			/// Currency Exchange Rates (1)
			/// </summary>
			CurrencyExchangeRate = 1,
			/// <summary>
			/// Hi3Prices (2)
			/// </summary>
			Hi3Prices = 2,
			/// <summary>
			/// Hi3AssetFundSplit
			/// </summary>
			Hi3AssetFundSplits = 3,
			/// <summary>
			/// Hi3CompositeFundSplit
			/// </summary>
			Hi3CompositeFundSplits = 4,
			/// <summary>
			/// Hi3Prices for Linked Funds  (5)
			/// </summary>
			Hi3PricesLinked = 5,
			/// <summary>
			/// Hi3Prices for Composite Funds (6)
			/// </summary>
			Hi3PricesComposite = 6,
			/// <summary>
			/// Price Import for the Price comparision report (7)
			/// </summary>
			PriceComparisionPrices=7
		}

		#endregion

		#region Variables

		private string m_connectionString;
		private ImportFileType m_importFileType;
		private string m_companyCode;
		private string m_importFileName;
		private string m_importFileNameExtension;
		private string m_importFileNameWithoutExtension;
		private DataTable m_validationErrors;
		private DataTable m_ImportedData;
		/// <summary>
		/// Snapshot object from the last import
		/// </summary>
		public Snapshot ImportSnapshot;

		private DateTime m_CompanyValuationPoint;

		#endregion

		#region Properties

		/// <summary>
		/// Public access to the validation errors.
		/// </summary>
		public DataTable ValidationErrors
		{
			get { return m_validationErrors; }
		}

		/// <summary>
		/// Public Access to the imported rows.
		/// </summary>
		public DataTable ImportedRows
		{
			get
			{
				if (this.m_ImportedData == null)
				{
					ImportPersister importPersister = new ImportPersister(this.m_connectionString);
					switch (m_importFileType)
					{
						// Stock Market Indices
						case ImportFileType.PriceComparisionPrices:
							goto case ImportFileType.Hi3Prices;
							// Stock Market Indices
						case ImportFileType.StockMarketIndices:
							this.m_ImportedData = importPersister.GetImportedMarketIndices(this.ImportSnapshot);
							break;
							// Currency Exchange rate
						case ImportFileType.CurrencyExchangeRate:
							this.m_ImportedData = importPersister.GetImportedCurrencyRates(this.ImportSnapshot);
							break;
							// Hi3 Prices
						case ImportFileType.Hi3Prices:
							this.m_ImportedData = importPersister.GetImportedFundPrices(this.ImportSnapshot);
							break;
							// Hi3 Asset Fund Splits
						case ImportFileType.Hi3AssetFundSplits:
							this.m_ImportedData = importPersister.GetImportedAssetFundSplit(this.ImportSnapshot);
							break;
							// Hi3 Composite Prices
						case ImportFileType.Hi3PricesComposite:
							goto case ImportFileType.Hi3Prices;
							// Hi3 Linked Prices
						case ImportFileType.Hi3PricesLinked:
							goto case ImportFileType.Hi3Prices;
							// Hi3 CompositeFund Splits
						case ImportFileType.Hi3CompositeFundSplits:
							this.m_ImportedData = importPersister.GetImportedCompositeFundSplit(this.ImportSnapshot);
							break;
						default:
							throw new NotImplementedException(String.Format("File type of {0} is not implemented.", m_importFileType.ToString()));
					}
				}
				return m_ImportedData;
			}
		}

		/// <summary>
		/// Does the company for this controller have authorised prices.
		/// </summary>
		public bool HasAuthorisedPrices
		{
			get
			{
				ImportPersister importPersister = new ImportPersister(this.m_connectionString);
				return importPersister.CheckForAuthorisedPrices(this.m_companyCode);
			}
		}

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor for Fund Group controller
		/// </summary>
		/// <param name="connectionString">Connection string to use</param>
		public ImportController(string connectionString)
		{
			// Build valid connection string
			m_connectionString = connectionString;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Verify that we have a new file
		/// </summary>
		/// <param name="importFileName">File to Import</param>
		/// <param name="companyCode"></param>
		/// <returns>True if it is a new file, False if it isn't</returns>
		public bool VerifyNewFile(string importFileName, string companyCode)
		{
			T.E();
			bool returnValue = false;
			try
			{
				ImportPersister importPersister = new ImportPersister(this.m_connectionString);
				returnValue = importPersister.VerifyNewFile(importFileName, companyCode);
			}
			finally
			{
				T.X();
			}
			return returnValue;
		}

		/// <summary>
		/// Import the file.
		/// </summary>
		/// <param name="companyCode"></param>
		/// <param name="importFileType">Type of import</param>
		/// <param name="importFileName">File being imported</param>
		/// <param name="companyValuationDate">The current companies valuation date (use to validate the file against)</param>
		/// <param name="importFileNameExtension">Extension of the file being imported</param>
		/// <param name="importFileNameWithoutExtension">Name of file being imported without the extension of path</param>
		public void Import(ImportFileType importFileType, string companyCode, string importFileName, 
				string importFileNameExtension, string importFileNameWithoutExtension,DateTime companyValuationDate)
		{
			T.E();
			try
			{
				m_importFileType = importFileType;
				m_companyCode = companyCode;
				m_importFileName = importFileName;
				m_importFileNameExtension=importFileNameExtension;
				m_importFileNameWithoutExtension=importFileNameWithoutExtension;
				m_CompanyValuationPoint=companyValuationDate;
				
				// What type of file are we importing
				switch (importFileType)
				{
						// Stock Market Indices
					case ImportFileType.StockMarketIndices:
						this.importStockMarketIndices();
						break;
						// Currency Exchange Rates
					case ImportFileType.CurrencyExchangeRate:
						this.importCurrencyExchangeRates();
						break;
					// Hi3 Prices
					case ImportFileType.PriceComparisionPrices:
						this.importHi3Prices(false,SnapshotProcess.Report);
						break;
						// Hi3 Prices
					case ImportFileType.Hi3Prices:
						this.importHi3Prices(true,SnapshotProcess.Import);
						break;
						// Hi3 Prices Composite Price Series Funds
					case ImportFileType.Hi3PricesComposite:
						goto case ImportFileType.Hi3Prices;
						// Hi3 Prices Linked Fund Prices
					case ImportFileType.Hi3PricesLinked:
						goto case ImportFileType.Hi3Prices;
						// Hi3 Prices
					case ImportFileType.Hi3AssetFundSplits:
						this.importHi3AssetFundSplit(companyCode);
						break;
						// Hi3 Prices
					case ImportFileType.Hi3CompositeFundSplits:
						this.importHi3CompositeFundSplits();
						break;
					default:
						throw new NotImplementedException(String.Format("File type of {0} is not implemented.", importFileType.ToString()));
				}
			}
			catch (RowMismatchException  ex)
			{
					// make sure we raise that exception on up
					throw new ImportFileSchemaMismatchException("The import file was in an incorrect format.",ex);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Activate the current import batch.
		/// </summary>
		public void ActivateImport()
		{
			T.E();
			try
			{
				SnapshotPersister persister = new SnapshotPersister(this.m_connectionString);
				persister.Activate(ImportSnapshot);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Cancels the current import batch.
		/// </summary>
		public void CancelImport()
		{
			T.E();
			try
			{
				SnapshotPersister persister = new SnapshotPersister(this.m_connectionString);
				persister.Cancel(ImportSnapshot);
			}
			finally
			{
				T.X();
			}
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Import Stock Market Indices
		/// </summary>
		private void importStockMarketIndices()
		{
			T.E();
			try
			{
				// Set up the data reader
				IndexFileReader fileReader = new IndexFileReader(m_importFileName);
				DateTime valuationPoint=checkHeaderValuationPoint( fileReader,0,0);
				getNewSnapShotId(null,SnapshotProcess.Import);

				 fileReader = new IndexFileReader(m_importFileName);

				// Can we read the body.
				bool bodySuccess = fileReader.NextResult();

				if (bodySuccess)
				{
					// Load the valid Currency Codes
					// TODO this is loading up an old legacy type, but is still used by Import
					StockMarketCollection allStockMarkets = LookupController.LoadStockMarketIndices(m_connectionString);

					// Set up the Validators
					IDataRowValidator validMarketIndexValidator = new MarketIndexDataRowValidator(allStockMarkets);
					
					IDataRowValidator[][] allValidators = new IDataRowValidator[1][];
					allValidators[0] = new IDataRowValidator[] {validMarketIndexValidator};

					attachValidationHandlers(allValidators);

					// Set up the persisters
					IPersistRow sqlPersister = new ImportMarketIndexSqlPersister(m_connectionString, "usp_ImportedIndexValuesCreate", "iSnapshotId", ImportSnapshot, m_companyCode, valuationPoint);

					IPersistRow[][] persisters = new IPersistRow[1][];
					persisters[0] = new IPersistRow[] {sqlPersister};

					// 4. Create the data transporter
					DataTransporter dataTransporter = new DataTransporter(false, fileReader, allValidators, persisters);

					// 6. Do the import
					dataTransporter.RunDataTransfer();

				}
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Import Currency Exchange Rates
		/// </summary>
		private void importCurrencyExchangeRates()
		{
			try
			{
				// Set up the Columns
				Column valDate = new Column("ValuationDate", Type.GetType("System.String"));
				Column valTime = new Column("ValuationTime", Type.GetType("System.String"));
				Column currencyCode = new Column("CurrencyCode", Type.GetType("System.String"));
				Column exchangeRate = new Column("ExchangeRate", Type.GetType("System.Decimal"));
				Column[] dataColumns = new Column[4] {valDate, valTime, currencyCode, exchangeRate};

				// Set up the data reader
				CsvFileReader fileReader = new CsvFileReader(m_importFileName, dataColumns);
				
				checkHeaderValuationPoint(  fileReader,0,1);

				getNewSnapShotId(null,SnapshotProcess.Import);

				// Load the valid Currency Codes
				LookupController lookupController=new LookupController();
				CurrencyCollection allCurrencies = lookupController.LoadCurrencies(m_connectionString);

				// Set up the Validators
				IDataRowValidator validCurrencyCodeValidator = new CurrencyDataRowValidator(allCurrencies);
				IDataRowValidator[][] allValidators = new IDataRowValidator[1][];
				allValidators[0] = new IDataRowValidator[] {validCurrencyCodeValidator};

				attachValidationHandlers(allValidators);

				// Set up the persisters
				IPersistRow sqlPersister = new ImportExchangeRateSqlPersister(m_connectionString, "usp_CurrencyRatesCreate", "iSnapshotId", ImportSnapshot, m_companyCode);

				IPersistRow[][] persisters = new IPersistRow[1][];
				persisters[0] = new IPersistRow[] {sqlPersister};

				// 4. Create the data transporter
				fileReader = new CsvFileReader(m_importFileName, dataColumns);

				DataTransporter dataTransporter = new DataTransporter(false, fileReader, allValidators, persisters);

				// 6. Do the import
				dataTransporter.RunDataTransfer();
			}
			finally
			{
				T.X();
			}
		}

		

		/// <summary>
		/// Import Hi3 Prices
		/// </summary>
		private void importHi3Prices(bool checkValuationPoint,SnapshotProcess process)
		{
			T.E();
			try
			{
				Column[] dataColumns = ImportController.generateHi3PricesColumns();

				// Set up the data reader
				CsvFileReader fileReader = new CsvFileReader(m_importFileName, dataColumns);
			
				if (checkValuationPoint)
				{
					checkHeaderValuationPoint( fileReader,(int) ImportHi3PricesSqlPersister.Hi3PricesColumnPosition.ValuationDate,
					(int) ImportHi3PricesSqlPersister.Hi3PricesColumnPosition.ValuationTime);
				}

				getNewSnapShotId(null,process);
		
				fileReader = new CsvFileReader(m_importFileName, dataColumns);

				this.transferHi3Prices(ImportSnapshot, fileReader,m_importFileNameWithoutExtension!="");
			}
			catch
			{
				throw;
			}
			finally
			{
				T.X();
			}
		}

		private void getNewSnapShotId(SqlTransaction tran,SnapshotProcess process)
		{
			SnapshotPersister persister = new SnapshotPersister(this.m_connectionString);
			this.ImportSnapshot = persister.NewImportSnapshot(m_importFileName, m_companyCode,process,tran);
		}

		private struct CompanyCollections
		{
			/// <summary>
			/// Creates a new <see cref="CompanyCollections"/> instance.
			/// </summary>
			/// <param name="connectionString">Connection string.</param>
			/// <param name="companyCode">Company code.</param>
			internal CompanyCollections(string connectionString,string companyCode)
			{
				FundController fundController = new FundController(connectionString);
				FundCollection companyFunds;
				companyFunds = fundController.LoadPartialFundsForCompany(companyCode);
				m_AssetFunds = new Hashtable();
				m_HiPortfolioCodes = new Hashtable(companyFunds.Count);

				populateCollections(companyFunds);
			}

			/// <summary>
			/// Creates a new <see cref="CompanyCollections"/> instance.
			/// </summary>
			/// <param name="connectionString">Connection string.</param>
			/// <param name="companyCode">Company code.</param>
			/// <param name="fileName">Name of the file.</param>
			/// <param name="extension">Extension.</param>
			internal CompanyCollections(string connectionString,string companyCode,string fileName,string extension)
			{

				FundController fundController = new FundController(connectionString);
				FundCollection companyFunds;
				companyFunds = fundController.LoadPartialFundsForCompanyByFile(companyCode,fileName,extension);
				m_AssetFunds = new Hashtable();
				m_HiPortfolioCodes = new Hashtable(companyFunds.Count);

				populateCollections(companyFunds);
			}

			private void populateCollections(FundCollection companyFunds)
			{
				for (int i = 0; i < companyFunds.Count; i ++)
				{
					m_HiPortfolioCodes.Add(companyFunds[i].HiPortfolioCode.Trim(), companyFunds[i]);
					if (m_AssetFunds.Contains(companyFunds[i].ParentAssetFundID.Trim()) == false)
					{
						m_AssetFunds.Add(companyFunds[i].ParentAssetFundID.Trim(), companyFunds[i]);
					}
				}

			}

			public Hashtable AssetFunds
			{
				get{return m_AssetFunds;}
				set { m_AssetFunds=value; }
			}
			private Hashtable m_AssetFunds;

			public Hashtable HiPortfolioCodes
			{
				get{return m_HiPortfolioCodes;}
				set { m_HiPortfolioCodes=value; }
			}
			private Hashtable m_HiPortfolioCodes;

		}

		
		private DateTime checkHeaderValuationPoint(  DelimitedFileReader fileReader,int valuationDateColumn,int valuationTimeColumn)
		{
			DateTime valuationDate = DateTime.MinValue;
	
			// Need to peek at the first row to get the Valuation Date.
			bool headerSuccess = fileReader.Read();
	
			if (headerSuccess)
			{
				string headerDateAsString = fileReader[valuationDateColumn].ToString().Replace("/","");

				if (headerDateAsString.Length != 15 && headerDateAsString.Length != 17 && headerDateAsString.Length != 6 && headerDateAsString.Length != 8 && headerDateAsString.Length != 12)
				{
					throw new ImportInvalidDateFormatException("The date in the file was not in a recognised date format.");
				}

				string headerTimeAsString ="";
				int dayNumber=0 ;
				int monthNumber=0;
				int yearNumber=0 ;

				switch (m_importFileType)
				{
					case ImportFileType.CurrencyExchangeRate:
						dayNumber = int.Parse(headerDateAsString.Substring(0, 2));
						monthNumber = int.Parse(headerDateAsString.Substring(2, 2));
						yearNumber = int.Parse(headerDateAsString.Substring(4, 4));

						headerTimeAsString=fileReader[valuationTimeColumn].ToString().Replace(":","");
						break;
					case ImportFileType.Hi3Prices:
						dayNumber = int.Parse(headerDateAsString.Substring(0, 2));
						monthNumber = int.Parse(headerDateAsString.Substring(2, 2));
						if (headerDateAsString.Length == 8)
						{
							yearNumber = int.Parse(headerDateAsString.Substring(4, 4));
						}
						else
						{
							yearNumber = 2000+int.Parse(headerDateAsString.Substring(4, 2));
						}


						headerTimeAsString=fileReader[valuationTimeColumn].ToString().Replace(":","");
						break;
					case ImportFileType.Hi3PricesComposite:
						goto case ImportFileType.Hi3Prices;
					case ImportFileType.Hi3PricesLinked:
						goto case ImportFileType.Hi3Prices;
					case ImportFileType.StockMarketIndices:
						dayNumber = int.Parse(headerDateAsString.Substring(0, 2));
						monthNumber = int.Parse(headerDateAsString.Substring(2, 2));
						if (headerDateAsString.Length == 15)
						{
							yearNumber = 2000+int.Parse(headerDateAsString.Substring(4, 2));
							headerTimeAsString=headerDateAsString.Replace(":","").Replace(" ","").Substring(6,4);
						}
						else
						{
							yearNumber = int.Parse(headerDateAsString.Substring(4, 4));
							headerTimeAsString=headerDateAsString.Replace(":","").Replace(" ","").Substring(8,4);
						}

						break;
				}
		
				
				int hourNumber = int.Parse(headerTimeAsString.Substring(0, 2));
				int minNumber = int.Parse(headerTimeAsString.Substring(2, 2));

				valuationDate = new DateTime(yearNumber, monthNumber, dayNumber,hourNumber ,minNumber,0);

				if ( valuationDate.Ticks!=m_CompanyValuationPoint.Ticks)
				{
					throw new ImportIncorrectDateException("File had an import date of {0} but only {1} is allowed.", valuationDate, m_CompanyValuationPoint);
				}
			}
			else
			{
				throw new ApplicationException("Failed to read the file header record.");
			}
	
			// Reset the file so we can proces in 1 go.
			fileReader.Close();
			fileReader.Dispose();
			return valuationDate;
		}


		/// <summary>
		/// Generate Hi3 Prices Columns
		/// </summary>
		/// <returns></returns>
		private static Column[] generateHi3PricesColumns()
		{
			T.E();
			Column[] dataColumns = null;
			try
			{
				// Set up the Columns
				Column colAssetFundId = new Column("AssetFundId", Type.GetType("System.String"));
				Column colValuationDate = new Column("ValuationDate", Type.GetType("System.String"));
				Column colValuationTime = new Column("ValuationTime", Type.GetType("System.String"));
				Column colCurrencyCode = new Column("CurrencyCode", Type.GetType("System.String"));
				// Series1
				Column colBidOrSingle1 = new Column("BidOrSeries1", Type.GetType("System.String"));
				Column colUnroundedBidPrice1 = new Column("UnroundedBidPrice1", Type.GetType("System.String"));
				Column colUnroundedOfferPrice1 = new Column("UnroundedOfferPrice1", Type.GetType("System.String"));
				Column colOfferPrice1 = new Column("OfferPrice1", Type.GetType("System.String"));
				Column colPolicyHolderUnits1 = new Column("PolicyHolderUnits1", Type.GetType("System.String"));
				Column colAssetUnitPrice1 = new Column("AsetUnitPrice1", Type.GetType("System.String"));
				Column colBarePrice1 = new Column("BarePrice1", Type.GetType("System.String"));
				Column colCompositeUnits1 = new Column("CompositeUnits1", Type.GetType("System.String"));
				Column colEquitableUnits1 = new Column("EquitableUnits1", Type.GetType("System.String"));
				Column colYield1 = new Column("Yield1", Type.GetType("System.String"));
				Column colUnroundedInitialPrice1 = new Column("UnroundedInitialPrice1", Type.GetType("System.String"));
				Column colPublishedInitialPrice1 = new Column("PublishedInitialPrice1", Type.GetType("System.String"));
				Column colPublishedInitailOffer1 = new Column("PublishedInitailOffer1", Type.GetType("System.String"));
				Column colValuationBasis1 = new Column("ValuationBasis1", Type.GetType("System.String"));

				// Series2
				Column colBidOrSingle2 = new Column("BidOrSeries2", Type.GetType("System.String"));
				Column colUnroundedBidPrice2 = new Column("UnroundedBidPrice2", Type.GetType("System.String"));
				Column colUnroundedOfferPrice2 = new Column("UnroundedOfferPrice2", Type.GetType("System.String"));
				Column colOfferPrice2 = new Column("OfferPrice2", Type.GetType("System.String"));
				Column colPolicyHolderUnits2 = new Column("PolicyHolderUnits2", Type.GetType("System.String"));
				Column colAssetUnitPrice2 = new Column("AsetUnitPrice2", Type.GetType("System.String"));
				Column colBarePrice2 = new Column("BarePrice2", Type.GetType("System.String"));
				Column colCompositeUnits2 = new Column("CompositeUnits2", Type.GetType("System.String"));
				Column colEquitableUnits2 = new Column("EquitableUnits2", Type.GetType("System.String"));
				Column colYield2 = new Column("Yield2", Type.GetType("System.String"));
				Column colUnroundedInitialPrice2 = new Column("UnroundedInitialPrice2", Type.GetType("System.String"));
				Column colPublishedInitialPrice2 = new Column("PublishedInitialPrice2", Type.GetType("System.String"));
				Column colPublishedInitailOffer2 = new Column("PublishedInitailOffer2", Type.GetType("System.String"));
				Column colValuationBasis2 = new Column("ValuationBasis2", Type.GetType("System.String"));

				// Series3
				Column colBidOrSingle3 = new Column("BidOrSeries3", Type.GetType("System.String"));
				Column colUnroundedBidPrice3 = new Column("UnroundedBidPrice3", Type.GetType("System.String"));
				Column colUnroundedOfferPrice3 = new Column("UnroundedOfferPrice3", Type.GetType("System.String"));
				Column colOfferPrice3 = new Column("OfferPrice3", Type.GetType("System.String"));
				Column colPolicyHolderUnits3 = new Column("PolicyHolderUnits3", Type.GetType("System.String"));
				Column colAssetUnitPrice3 = new Column("AsetUnitPrice3", Type.GetType("System.String"));
				Column colBarePrice3 = new Column("BarePrice3", Type.GetType("System.String"));
				Column colCompositeUnits3 = new Column("CompositeUnits3", Type.GetType("System.String"));
				Column colEquitableUnits3 = new Column("EquitableUnits3", Type.GetType("System.String"));
				Column colYield3 = new Column("Yield3", Type.GetType("System.String"));
				Column colUnroundedInitialPrice3 = new Column("UnroundedInitialPrice3", Type.GetType("System.String"));
				Column colPublishedInitialPrice3 = new Column("PublishedInitialPrice3", Type.GetType("System.String"));
				Column colPublishedInitailOffer3 = new Column("PublishedInitailOffer3", Type.GetType("System.String"));
				Column colValuationBasis3 = new Column("ValuationBasis3", Type.GetType("System.String"));

				// Series4
				Column colBidOrSingle4 = new Column("BidOrSeries4", Type.GetType("System.String"));
				Column colUnroundedBidPrice4 = new Column("UnroundedBidPrice4", Type.GetType("System.String"));
				Column colUnroundedOfferPrice4 = new Column("UnroundedOfferPrice4", Type.GetType("System.String"));
				Column colOfferPrice4 = new Column("OfferPrice4", Type.GetType("System.String"));
				Column colPolicyHolderUnits4 = new Column("PolicyHolderUnits4", Type.GetType("System.String"));
				Column colAssetUnitPrice4 = new Column("AsetUnitPrice4", Type.GetType("System.String"));
				Column colBarePrice4 = new Column("BarePrice4", Type.GetType("System.String"));
				Column colCompositeUnits4 = new Column("CompositeUnits4", Type.GetType("System.String"));
				Column colEquitableUnits4 = new Column("EquitableUnits4", Type.GetType("System.String"));
				Column colYield4 = new Column("Yield4", Type.GetType("System.String"));
				Column colUnroundedInitialPrice4 = new Column("UnroundedInitialPrice4", Type.GetType("System.String"));
				Column colPublishedInitialPrice4 = new Column("PublishedInitialPrice4", Type.GetType("System.String"));
				Column colPublishedInitailOffer4 = new Column("PublishedInitailOffer4", Type.GetType("System.String"));
				Column colValuationBasis4 = new Column("ValuationBasis4", Type.GetType("System.String"));

				// Series5
				Column colBidOrSingle5 = new Column("BidOrSeries5", Type.GetType("System.String"));
				Column colUnroundedBidPrice5 = new Column("UnroundedBidPrice5", Type.GetType("System.String"));
				Column colUnroundedOfferPrice5 = new Column("UnroundedOfferPrice5", Type.GetType("System.String"));
				Column colOfferPrice5 = new Column("OfferPrice5", Type.GetType("System.String"));
				Column colPolicyHolderUnits5 = new Column("PolicyHolderUnits5", Type.GetType("System.String"));
				Column colAssetUnitPrice5 = new Column("AsetUnitPrice5", Type.GetType("System.String"));
				Column colBarePrice5 = new Column("BarePrice5", Type.GetType("System.String"));
				Column colCompositeUnits5 = new Column("CompositeUnits5", Type.GetType("System.String"));
				Column colEquitableUnits5 = new Column("EquitableUnits5", Type.GetType("System.String"));
				Column colYield5 = new Column("Yield5", Type.GetType("System.String"));
				Column colUnroundedInitialPrice5 = new Column("UnroundedInitialPrice5", Type.GetType("System.String"));
				Column colPublishedInitialPrice5 = new Column("PublishedInitialPrice5", Type.GetType("System.String"));
				Column colPublishedInitailOffer5 = new Column("PublishedInitailOffer5", Type.GetType("System.String"));
				Column colValuationBasis5 = new Column("ValuationBasis5", Type.GetType("System.String"));

				// Series6
				Column colBidOrSingle6 = new Column("BidOrSeries6", Type.GetType("System.String"));
				Column colUnroundedBidPrice6 = new Column("UnroundedBidPrice6", Type.GetType("System.String"));
				Column colUnroundedOfferPrice6 = new Column("UnroundedOfferPrice6", Type.GetType("System.String"));
				Column colOfferPrice6 = new Column("OfferPrice6", Type.GetType("System.String"));
				Column colPolicyHolderUnits6 = new Column("PolicyHolderUnits6", Type.GetType("System.String"));
				Column colAssetUnitPrice6 = new Column("AsetUnitPrice6", Type.GetType("System.String"));
				Column colBarePrice6 = new Column("BarePrice6", Type.GetType("System.String"));
				Column colCompositeUnits6 = new Column("CompositeUnits6", Type.GetType("System.String"));
				Column colEquitableUnits6 = new Column("EquitableUnits6", Type.GetType("System.String"));
				Column colYield6 = new Column("Yield6", Type.GetType("System.String"));
				Column colUnroundedInitialPrice6 = new Column("UnroundedInitialPrice6", Type.GetType("System.String"));
				Column colPublishedInitialPrice6 = new Column("PublishedInitialPrice6", Type.GetType("System.String"));
				Column colPublishedInitailOffer6 = new Column("PublishedInitailOffer6", Type.GetType("System.String"));
				Column colValuationBasis6 = new Column("ValuationBasis6", Type.GetType("System.String"));


				// Series7
				Column colBidOrSingle7 = new Column("BidOrSeries7", Type.GetType("System.String"));
				Column colUnroundedBidPrice7 = new Column("UnroundedBidPrice7", Type.GetType("System.String"));
				Column colUnroundedOfferPrice7 = new Column("UnroundedOfferPrice7", Type.GetType("System.String"));
				Column colOfferPrice7 = new Column("OfferPrice7", Type.GetType("System.String"));
				Column colPolicyHolderUnits7 = new Column("PolicyHolderUnits7", Type.GetType("System.String"));
				Column colAssetUnitPrice7 = new Column("AsetUnitPrice7", Type.GetType("System.String"));
				Column colBarePrice7 = new Column("BarePrice7", Type.GetType("System.String"));
				Column colCompositeUnits7 = new Column("CompositeUnits7", Type.GetType("System.String"));
				Column colEquitableUnits7 = new Column("EquitableUnits7", Type.GetType("System.String"));
				Column colYield7 = new Column("Yield7", Type.GetType("System.String"));
				Column colUnroundedInitialPrice7 = new Column("UnroundedInitialPrice7", Type.GetType("System.String"));
				Column colPublishedInitialPrice7 = new Column("PublishedInitialPrice7", Type.GetType("System.String"));
				Column colPublishedInitailOffer7 = new Column("PublishedInitailOffer7", Type.GetType("System.String"));
				Column colValuationBasis7 = new Column("ValuationBasis7", Type.GetType("System.String"));

				// Series8
				Column colBidOrSingle8 = new Column("BidOrSeries8", Type.GetType("System.String"));
				Column colUnroundedBidPrice8 = new Column("UnroundedBidPrice8", Type.GetType("System.String"));
				Column colUnroundedOfferPrice8 = new Column("UnroundedOfferPrice8", Type.GetType("System.String"));
				Column colOfferPrice8 = new Column("OfferPrice8", Type.GetType("System.String"));
				Column colPolicyHolderUnits8 = new Column("PolicyHolderUnits8", Type.GetType("System.String"));
				Column colAssetUnitPrice8 = new Column("AsetUnitPrice8", Type.GetType("System.String"));
				Column colBarePrice8 = new Column("BarePrice8", Type.GetType("System.String"));
				Column colCompositeUnits8 = new Column("CompositeUnits8", Type.GetType("System.String"));
				Column colEquitableUnits8 = new Column("EquitableUnits8", Type.GetType("System.String"));
				Column colYield8 = new Column("Yield8", Type.GetType("System.String"));			
				Column colUnroundedInitialPrice8 = new Column("UnroundedInitialPrice8", Type.GetType("System.String"));
				Column colPublishedInitialPrice8 = new Column("PublishedInitialPrice8", Type.GetType("System.String"));
				Column colPublishedInitailOffer8 = new Column("PublishedInitailOffer8", Type.GetType("System.String"));
				Column colValuationBasis8 = new Column("ValuationBasis8", Type.GetType("System.String"));

				// Series9
				Column colBidOrSingle9 = new Column("BidOrSeries9", Type.GetType("System.String"));
				Column colUnroundedBidPrice9 = new Column("UnroundedBidPrice9", Type.GetType("System.String"));
				Column colUnroundedOfferPrice9 = new Column("UnroundedOfferPrice9", Type.GetType("System.String"));
				Column colOfferPrice9 = new Column("OfferPrice9", Type.GetType("System.String"));
				Column colPolicyHolderUnits9 = new Column("PolicyHolderUnits9", Type.GetType("System.String"));
				Column colAssetUnitPrice9 = new Column("AsetUnitPrice9", Type.GetType("System.String"));
				Column colBarePrice9 = new Column("BarePrice9", Type.GetType("System.String"));
				Column colCompositeUnits9 = new Column("CompositeUnits9", Type.GetType("System.String"));
				Column colEquitableUnits9 = new Column("EquitableUnits9", Type.GetType("System.String"));
				Column colYield9 = new Column("Yield9", Type.GetType("System.String"));
				Column colUnroundedInitialPrice9 = new Column("UnroundedInitialPrice9", Type.GetType("System.String"));
				Column colPublishedInitialPrice9 = new Column("PublishedInitialPrice9", Type.GetType("System.String"));
				Column colPublishedInitailOffer9 = new Column("PublishedInitailOffer9", Type.GetType("System.String"));
				Column colValuationBasis9 = new Column("ValuationBasis9", Type.GetType("System.String"));

				// Series10
				Column colBidOrSingle10 = new Column("BidOrSeries10", Type.GetType("System.String"));
				Column colUnroundedBidPrice10 = new Column("UnroundedBidPrice10", Type.GetType("System.String"));
				Column colUnroundedOfferPrice10 = new Column("UnroundedOfferPrice10", Type.GetType("System.String"));
				Column colOfferPrice10 = new Column("OfferPrice10", Type.GetType("System.String"));
				Column colPolicyHolderUnits10 = new Column("PolicyHolderUnits10", Type.GetType("System.String"));
				Column colAssetUnitPrice10 = new Column("AsetUnitPrice10", Type.GetType("System.String"));
				Column colBarePrice10 = new Column("BarePrice10", Type.GetType("System.String"));
				Column colCompositeUnits10 = new Column("CompositeUnits10", Type.GetType("System.String"));
				Column colEquitableUnits10 = new Column("EquitableUnits10", Type.GetType("System.String"));
				Column colYield10 = new Column("Yield10", Type.GetType("System.String"));
				Column colUnroundedInitialPrice10 = new Column("UnroundedInitialPrice10", Type.GetType("System.String"));
				Column colPublishedInitialPrice10 = new Column("PublishedInitialPrice10", Type.GetType("System.String"));
				Column colPublishedInitailOffer10 = new Column("PublishedInitailOffer10", Type.GetType("System.String"));
				Column colValuationBasis10 = new Column("ValuationBasis10", Type.GetType("System.String"));

				// This is a dummy column used because HiPort adds an extra comma to the end of the field and
				// apparently it's a two week job to stop this behaviour.
				
				Column colIdioticHiPort = new Column("Numpty", Type.GetType("System.String"));

				dataColumns = new Column[145] 
				{
					colAssetFundId , colValuationDate , colValuationTime ,  colCurrencyCode,
					colBidOrSingle1, colUnroundedBidPrice1, colUnroundedOfferPrice1, colOfferPrice1, colPolicyHolderUnits1, colAssetUnitPrice1, colBarePrice1, colCompositeUnits1, colEquitableUnits1, colYield1 , colUnroundedInitialPrice1, colPublishedInitialPrice1, colPublishedInitailOffer1,colValuationBasis1, 
					colBidOrSingle2, colUnroundedBidPrice2, colUnroundedOfferPrice2, colOfferPrice2, colPolicyHolderUnits2, colAssetUnitPrice2, colBarePrice2, colCompositeUnits2, colEquitableUnits2, colYield2 , colUnroundedInitialPrice2, colPublishedInitialPrice2, colPublishedInitailOffer2, colValuationBasis2,
					colBidOrSingle3, colUnroundedBidPrice3, colUnroundedOfferPrice3, colOfferPrice3, colPolicyHolderUnits3, colAssetUnitPrice3, colBarePrice3, colCompositeUnits3, colEquitableUnits3, colYield3 , colUnroundedInitialPrice3, colPublishedInitialPrice3, colPublishedInitailOffer3, colValuationBasis3,
					colBidOrSingle4, colUnroundedBidPrice4, colUnroundedOfferPrice4, colOfferPrice4, colPolicyHolderUnits4, colAssetUnitPrice4, colBarePrice4, colCompositeUnits4, colEquitableUnits4, colYield4 , colUnroundedInitialPrice4, colPublishedInitialPrice4, colPublishedInitailOffer4, colValuationBasis4,
					colBidOrSingle5, colUnroundedBidPrice5, colUnroundedOfferPrice5, colOfferPrice5, colPolicyHolderUnits5, colAssetUnitPrice5, colBarePrice5, colCompositeUnits5, colEquitableUnits5, colYield5 , colUnroundedInitialPrice5, colPublishedInitialPrice5, colPublishedInitailOffer5, colValuationBasis5,
					colBidOrSingle6, colUnroundedBidPrice6, colUnroundedOfferPrice6, colOfferPrice6, colPolicyHolderUnits6, colAssetUnitPrice6, colBarePrice6, colCompositeUnits6, colEquitableUnits6, colYield6 , colUnroundedInitialPrice6, colPublishedInitialPrice6, colPublishedInitailOffer6, colValuationBasis6,
					colBidOrSingle7, colUnroundedBidPrice7, colUnroundedOfferPrice7, colOfferPrice7, colPolicyHolderUnits7, colAssetUnitPrice7, colBarePrice7, colCompositeUnits7, colEquitableUnits7, colYield7 , colUnroundedInitialPrice7, colPublishedInitialPrice7, colPublishedInitailOffer7, colValuationBasis7,
					colBidOrSingle8, colUnroundedBidPrice8, colUnroundedOfferPrice8, colOfferPrice8, colPolicyHolderUnits8, colAssetUnitPrice8, colBarePrice8, colCompositeUnits8, colEquitableUnits8, colYield8 , colUnroundedInitialPrice8, colPublishedInitialPrice8, colPublishedInitailOffer8, colValuationBasis8,
					colBidOrSingle9, colUnroundedBidPrice9, colUnroundedOfferPrice9, colOfferPrice9, colPolicyHolderUnits9, colAssetUnitPrice9, colBarePrice9, colCompositeUnits9, colEquitableUnits9, colYield9 , colUnroundedInitialPrice9, colPublishedInitialPrice9, colPublishedInitailOffer9, colValuationBasis9,
					colBidOrSingle10, colUnroundedBidPrice10, colUnroundedOfferPrice10, colOfferPrice10, colPolicyHolderUnits10, colAssetUnitPrice10, colBarePrice10, colCompositeUnits10, colEquitableUnits10, colYield10, colUnroundedInitialPrice10, colPublishedInitialPrice10, colPublishedInitailOffer10, colValuationBasis10,colIdioticHiPort
				};

			}
			finally
			{
				T.X();
			}

			return dataColumns;
		}

		/// <summary>
		/// Transfer the data by row
		/// </summary>
		/// <param name="ImportSnapshot">Unique DB Id for the current Import</param>
		/// <param name="expectedFile">Is the price file being imported an expetced one for the current company</param>
		/// <param name="reader"></param>
		private void transferHi3Prices( Snapshot ImportSnapshot , IDataReader reader ,bool expectedFile )
		{
			T.E();
			try
			{

				CompanyCollections validationData;
				if (!expectedFile)
				{
					validationData=new CompanyCollections(m_connectionString,m_companyCode);
				}
				else
				{
					validationData=new CompanyCollections(m_connectionString,m_companyCode,m_importFileNameWithoutExtension,m_importFileNameExtension);
				}
				AssetFundDataRowValidator assetFundDataRowValidator = new AssetFundDataRowValidator( validationData.AssetFunds,(int) ImportHi3PricesSqlPersister.Hi3PricesColumnPosition.AssetFundId ,this.m_importFileType);
				Hi3PricesSeriesDataRowValidator seriesValidator = new Hi3PricesSeriesDataRowValidator( validationData.HiPortfolioCodes,m_importFileType );
				Hi3PricesAssetUnitPriceDataRowValidator assetUnitPriceValidator = new Hi3PricesAssetUnitPriceDataRowValidator();
				ImportHi3PricesSqlPersister seriesPersister = new ImportHi3PricesSqlPersister( m_connectionString , "usp_ImportedFundPricesCreate" , "iSnapshotId" , ImportSnapshot);
				ValidationErrorSeverity[] accumulationValidatorErrors = new ValidationErrorSeverity[12];


				try
				{

					// Start the transactions on all the persisters
					seriesPersister.InitialiseTransfer();

					DataTable dataDefinition = reader.GetSchemaTable();

					seriesValidator.InvalidDataRowEvent += new InvalidDataRowDelegate( storeInvalidDataRowEvent);
								
					assetFundDataRowValidator.InvalidDataRowEvent += new InvalidDataRowDelegate( storeInvalidDataRowEvent);
					assetUnitPriceValidator.InvalidDataRowEvent += new InvalidDataRowDelegate( storeInvalidDataRowEvent);

					// Process each row in the current row set
					while (reader.Read())
					{

						for (int loop = 0; loop < 2; loop++)
						{
							bool initial=(loop==1);

							// Create a string array to represent the current row                
							string[] dataRow = new string[dataDefinition.Rows.Count];

							// Hook up the validation events

							ValidationErrorSeverity[] seriesValidationError = new ValidationErrorSeverity[12];

							ValidationErrorSeverity assetValidationError=new ValidationErrorSeverity();
							ValidationErrorSeverity assetUnitPriceValidationError=new ValidationErrorSeverity();
						
							for (int i = 0; i < dataDefinition.Rows.Count; i++)
							{
								string newValue=reader[i].ToString();

								if (initial)
								{
									string columnName=dataDefinition.Rows[i]["ColumnName"].ToString();

									if (columnName.StartsWith("UnroundedBidPrice"))
									{// replace with UnroundedInitialPrice
										newValue = reader[i+9].ToString();
									}
									else if (columnName.StartsWith("BidOrSeries"))
									{// replace with PublishedInitialPrice
										newValue = reader[i+11].ToString();
									}
									else if (columnName.StartsWith("OfferPrice"))
									{// replace with PublishedInitailOffer
										newValue = reader[i+9].ToString();
									}
									else if (columnName.StartsWith("Yield") || columnName.StartsWith("UnroundedOfferPrice") || columnName.StartsWith("PolicyHolderUnits")  || columnName.StartsWith("BarePrice") || columnName.StartsWith("CompositeUnits")  || columnName.StartsWith("EquitableUnits") )
									{//replace with 0
										newValue = "0";
									}
								}
								
								dataRow[i]=newValue;
							}
							dataRow[0] = dataRow[0].Trim();

							if (!initial)
							{
							
								assetValidationError = assetFundDataRowValidator.Validate( dataRow , dataDefinition );
								assetUnitPriceValidationError = assetUnitPriceValidator.Validate( dataRow , dataDefinition );
							}

							if (initial)
							{
								seriesValidator=new Hi3InitialPricesSeriesDataRowValidator( validationData.HiPortfolioCodes,m_importFileType );
							}
							else
							{
								seriesValidator=new Hi3PricesSeriesDataRowValidator( validationData.HiPortfolioCodes,m_importFileType );
							}

							// Hook up the validation events
							seriesValidator.InvalidDataRowEvent += new InvalidDataRowDelegate( storeInvalidDataRowEvent);
						
						
							PersistAndValidateRow(initial,dataRow,dataDefinition, ref accumulationValidatorErrors,
								seriesValidator,reader,
								validationData.AssetFunds,
								seriesValidationError,
								assetValidationError,
								assetUnitPriceValidationError, 
								seriesPersister);


						}

					}

					// Make the persistances permanent
					seriesPersister.CompleteTransfer();


					validateForMissingAssetFunds(seriesPersister, ImportSnapshot);


					// Clear the validators
					assetFundDataRowValidator.InvalidDataRowEvent -= new InvalidDataRowDelegate( storeInvalidDataRowEvent);
					seriesValidator.InvalidDataRowEvent -= new InvalidDataRowDelegate( storeInvalidDataRowEvent);
					assetUnitPriceValidator.InvalidDataRowEvent -= new InvalidDataRowDelegate( storeInvalidDataRowEvent);
				}
				catch (Exception ex)
				{
					try
					{
						if ( seriesPersister.IsTransferInProgress )
						{
							seriesPersister.CancelTransfer();
						}
					}
					finally
					{
						// make sure we raise that exception on up
						throw ex;
					}
				}
				finally
				{
					// close the reader
					if (null != reader)
					{
						try
						{
							reader.Close();
							reader.Dispose();
						}
						finally
						{
							// no-op
						}
					}
				}
			}
			finally
			{
				T.X();
			}
		}

		private void validateForMissingAssetFunds(ImportHi3PricesSqlPersister seriesPersister, Snapshot ImportSnapshot)
		{
			AssetFundDataValidator assetFundValidator = new AssetFundDataValidator();
			assetFundValidator.InvalidDataEvent += new AssetFundDataValidator.InvalidDataDelegate(storeValidationError);
	
			assetFundValidator.MissingAssetFundsFromPriceImport(m_connectionString, seriesPersister,m_importFileNameExtension,
			                                                    m_importFileNameWithoutExtension,m_companyCode,ImportSnapshot.Id);
			
			assetFundValidator.InvalidDataEvent -= new AssetFundDataValidator.InvalidDataDelegate(storeValidationError);
		}

		private void PersistAndValidateRow(bool initial, string[] dataRow , DataTable dataDefinition,ref ValidationErrorSeverity[] accumulationValidatorErrors,Hi3PricesSeriesDataRowValidator seriesValidator, IDataReader reader , Hashtable allAssetFunds ,ValidationErrorSeverity[] seriesValidationError,ValidationErrorSeverity assetValidationError,ValidationErrorSeverity assetUnitPriceValidationError,ImportHi3PricesSqlPersister seriesPersister)
		{
			bool OEICFund=false;

			try
			{
				// If the asset fund is valid, check and save each class
				if ( assetValidationError == ValidationErrorSeverity.None && assetUnitPriceValidationError == ValidationErrorSeverity.None )
				{
					string assetFund = reader[ "AssetFundId" ].ToString().Trim();
					Fund activeFund = ((Fund)allAssetFunds[ assetFund ]);
						
				
					
					// See if its an OEIC fund
					if ( activeFund is OEICFund )
					{
						OEICFund = true;
					}

					seriesValidator.OEICFund=OEICFund;
					// Check each series number
					for( int seriesNumber = 1 ; seriesNumber <= 10 ; seriesNumber ++ )
					{
						if (initial)
						{
							seriesValidator.SeriesNumber = seriesNumber+50;
						}
						else
						{
							seriesValidator.SeriesNumber = seriesNumber;
						}
						seriesValidationError[ seriesNumber  + 1 ] = seriesValidator.Validate( dataRow , dataDefinition );
					}

					accumulationValidatorErrors=seriesValidationError;
				}

				// If the asset fund is valid, we can try to persist the series
				if ( assetValidationError == ValidationErrorSeverity.None && assetUnitPriceValidationError == ValidationErrorSeverity.None )
				{

					seriesPersister.OEICFund = OEICFund;

					for( int seriesNumber = 1 ; seriesNumber <= 10 ; seriesNumber ++ )
					{
						// If the series is valid, try to persist it.
						if ( seriesValidationError[ seriesNumber + 1 ] == ValidationErrorSeverity.None && ((initial && accumulationValidatorErrors[ seriesNumber + 1 ] == ValidationErrorSeverity.None) || initial==false))
						{
							if (initial)
							{
								seriesPersister.SeriesNumber = seriesNumber+50;
							}
							else
							{
								seriesPersister.SeriesNumber = seriesNumber;
							}
							seriesPersister.PersistRow( dataRow , dataDefinition );
						}
					}
				}
			}
			catch 
			{
				throw;
			}
		}

		/// <summary>
		/// Import and validate the Composte split file
		/// </summary>
		/// <param name="ImportSnapshot"></param>
		/// <param name="reader"></param>
		/// <param name="allCompanyAssetFunds">All the current companies Asset Funds</param>
		/// <param name="allCompanyHiPortfolioCodes">All the current companies Hi Portfolio Codes</param>
		/// <param name="transaction"></param>
		private void transferCompositeSplit(Snapshot ImportSnapshot, IDataReader reader, Hashtable allCompanyAssetFunds, Hashtable allCompanyHiPortfolioCodes, SqlTransaction transaction)
		{
			T.E();
			try
			{
				// Set up the validator
				CompositeSplitFundDataRowValidator compositeFundValidator = new CompositeSplitFundDataRowValidator(allCompanyAssetFunds, allCompanyHiPortfolioCodes);

				// Set up the persister
				ImportWorkingSplitTableSqlPersister compositePersister = new ImportWorkingSplitTableSqlPersister(m_connectionString, "usp_ImportedSplitWorkingTableCreate", "SnapshotID", ImportSnapshot);
				compositePersister.tempTran = transaction;

				try
				{
					// Hook up the validation events
					compositeFundValidator.InvalidDataRowEvent += new InvalidDataRowDelegate(storeInvalidDataRowEvent);
//					linkedFundValidator.InvalidDataRowEvent += new InvalidDataRowDelegate( storeInvalidDataRowEvent);

					// Start the transactions on all the persisters
					//	compositePersister.InitialiseTransfer();

					DataTable dataDefinition = reader.GetSchemaTable();


					//the following loops purpose is to check the validity of each row and when 
					//each linked fund in the composite is valid as well as the composite itself 
					//then we persist the the rows
					//because of the slightly different behavour of skipping on to the next composite when there are errors
					//it seemed that the InitialiseTransfer method  and DataTransporter object didn't do what was necessary?
					string lastAsset = null;
					string currentAsset = null;
					bool assetHasChanged = false;
					bool skipToNextAsset = false;
					ArrayList rowsToSave = new ArrayList();

					// Process each row in the current row set
					while (reader.Read())
					{
						// Create a string array to represent the current row                
						string[] dataRow = new string[dataDefinition.Rows.Count];

						for (int i = 0; i < dataDefinition.Rows.Count; i++)
						{
							dataRow[i] = reader[i].ToString();
						}
						dataRow[0] = dataRow[0].Trim();


						currentAsset = dataRow[(int) ImportWorkingSplitTableSqlPersister.ColumnPosition.primaryKey].ToString();

						if (lastAsset != null)
						{
							assetHasChanged = (currentAsset != lastAsset);
						}

						lastAsset = currentAsset;

						if (assetHasChanged)
						{
							if (rowsToSave.Count > 0)
							{
								//save rows
								SaveValidRows(rowsToSave, compositePersister, dataDefinition);
							}
							skipToNextAsset = false;
						}

					
						ValidationErrorSeverity compositeFundValidationError = compositeFundValidator.Validate(dataRow, dataDefinition);
						if (skipToNextAsset == false)
						{
							// If the asset/composite fund is valid, we can try to persist the row
							if (compositeFundValidationError == ValidationErrorSeverity.None)
							{
//								ValidationErrorSeverity linkedFundValidationError = linkedFundValidator.Validate( dataRow , dataDefinition );
//								if ( linkedFundValidationError == ValidationErrorSeverity.None )
//								{
								rowsToSave.Add(dataRow);
//								}
//								else
//								{
//									skipToNextComposite=true;
//									rowsToSave.Clear();
//								}
							}
							else
							{
								skipToNextAsset = true;
								rowsToSave.Clear();
							}
						}
					}
					SaveValidRows(rowsToSave, compositePersister, dataDefinition);

//					// Create the data transporter
//					DataTransporter dataTransporter = new DataTransporter( false, reader, validators, persisters);
//
//					// Do the import to the temp table
//					((IDataTransporter)dataTransporter).RunDataTransfer();

					// Clear the validators
					compositeFundValidator.InvalidDataRowEvent -= new InvalidDataRowDelegate(storeInvalidDataRowEvent);
					//	linkedFundValidator.InvalidDataRowEvent -= new InvalidDataRowDelegate( storeInvalidDataRowEvent);
				}
				catch (Exception ex)
				{
					try
					{
						if (compositePersister.IsTransferInProgress)
						{
							compositePersister.CancelTransfer();
						}
					}
					finally
					{
						// make sure we raise that exception on up
						throw ex;
					}
				}
				finally
				{
					// close the reader
					if (null != reader)
					{
						try
						{
							//reader.Close();
							reader.Dispose();
						}
						finally
						{
							// no-op
						}
					}
				}
			}
			finally
			{
				T.X();
			}
		}

		private static void SaveValidRows(ArrayList rowsToSave, IPersistRow compositePersister, DataTable dataDefinition)
		{
			//loop through all the valid rows for the composite and persist them
			foreach (string[] row in rowsToSave)
			{
				compositePersister.PersistRow(row, dataDefinition);
			}

			rowsToSave.Clear();
		}

//		/// <summary>
//		/// Transfer the data by row
//		/// </summary>
//		/// <param name="ImportSnapshot"></param>
//		/// <param name="reader"></param>
//		/// <param name="allAssetFunds"></param>
//		/// <param name="allHiPortfolioCodes"></param>
//		private void transferHi3PricesComposite(int ImportSnapshot, IDataReader reader, Hashtable allAssetFunds, Hashtable allHiPortfolioCodes)
//		{
//			T.E();
//			try
//			{
//				//set up validators for the rows
//				Hi3PricesCompositeAssetFundDataRowValidator assetFundValidator = new Hi3PricesCompositeAssetFundDataRowValidator(allAssetFunds);
//				Hi3PricesCompositeSeriesDataRowValidator seriesValidator = new Hi3PricesCompositeSeriesDataRowValidator(allHiPortfolioCodes);
//				Hi3PricesAssetUnitPriceDataRowValidator assetUnitPriceValidator = new Hi3PricesAssetUnitPriceDataRowValidator();
//				//set up the persister used to store the import in the ImportedFundPrices table
//				ImportHi3PricesCompositeSqlPersister seriesPersister = new ImportHi3PricesCompositeSqlPersister(m_connectionString, "usp_ImportedFundPricesCreate", "ImportSnapshot", ImportSnapshot, m_companyCode);
//
//				try
//				{
//					// Hook up the validation events
//					assetFundValidator.InvalidDataRowEvent += new InvalidDataRowDelegate(storeInvalidDataRowEvent);
//					seriesValidator.InvalidDataRowEvent += new InvalidDataRowDelegate(storeInvalidDataRowEvent);
//					assetUnitPriceValidator.InvalidDataRowEvent += new InvalidDataRowDelegate(storeInvalidDataRowEvent);
//
//					// Start the transactions on all the persisters
//					seriesPersister.InitialiseTransfer();
//
//					DataTable dataDefinition = reader.GetSchemaTable();
//
//					// Process each row in the current row set
//					while (reader.Read())
//					{
//						// Create a string array to represent the current row                
//						string[] dataRow = new string[dataDefinition.Rows.Count];
//						for (int i = 0; i < dataDefinition.Rows.Count; i++)
//						{
//							dataRow[i] = reader[i].ToString();
//						}
//						dataRow[0] = dataRow[0].Trim();
//
//						ValidationErrorSeverity[] seriesValidationError = new ValidationErrorSeverity[12];
//						ValidationErrorSeverity assetValidationError = assetFundValidator.Validate(dataRow, dataDefinition);
//						ValidationErrorSeverity assetUnitPriceValidationError = assetUnitPriceValidator.Validate(dataRow, dataDefinition);
//
//						bool OEICFund = false;
//
//						// If the asset fund is valid, check and save each class
//						if (assetValidationError == ValidationErrorSeverity.None && assetUnitPriceValidationError == ValidationErrorSeverity.None)
//						{
//							string assetFund = reader["AssetFundId"].ToString().Trim();
//							Fund activeFund = ((Fund) allAssetFunds[assetFund]);
//
//							// See if its n OEIC fund
//							if (activeFund is OEICFund)
//							{
//								OEICFund = true;
//							}
//
//							seriesValidator.OEIC = OEICFund;
//
//							// Check each series number
//							for (int seriesNumber = 1; seriesNumber <= 10; seriesNumber ++)
//							{
//								seriesValidator.SeriesNumber = seriesNumber;
//								seriesValidationError[seriesNumber + 1] = seriesValidator.Validate(dataRow, dataDefinition);
//							}
//						}
//
//						// If the asset fund is valid, we can try to persist the series
//						if (assetValidationError == ValidationErrorSeverity.None && assetUnitPriceValidationError == ValidationErrorSeverity.None)
//						{
//							seriesPersister.OEICFund = OEICFund;
//
//							for (int seriesNumber = 1; seriesNumber <= 10; seriesNumber ++)
//							{
//								// If the series is valid, try to persist it.
//								if (seriesValidationError[seriesNumber + 1] == ValidationErrorSeverity.None)
//								{
//									seriesPersister.SeriesNumber = seriesNumber;
//									seriesPersister.PersistRow(dataRow, dataDefinition);
//								}
//							}
//						}
//					}
//
//					// Make the persistances permanent
//					seriesPersister.CompleteTransfer();
//
//					// Clear the validators
//					assetFundValidator.InvalidDataRowEvent -= new InvalidDataRowDelegate(storeInvalidDataRowEvent);
//					seriesValidator.InvalidDataRowEvent -= new InvalidDataRowDelegate(storeInvalidDataRowEvent);
//					assetUnitPriceValidator.InvalidDataRowEvent -= new InvalidDataRowDelegate(storeInvalidDataRowEvent);
//				}
//				catch (Exception ex)
//				{
//					try
//					{
//						if (seriesPersister.IsTransferInProgress)
//						{
//							seriesPersister.CancelTransfer();
//						}
//					}
//					finally
//					{
//						// make sure we raise that exception on up
//						throw ex;
//					}
//				}
//				finally
//				{
//					// close the reader
//					if (null != reader)
//					{
//						try
//						{
//							reader.Close();
//							reader.Dispose();
//						}
//						finally
//						{
//							// no-op
//						}
//					}
//				}
//			}
//			finally
//			{
//				T.X();
//			}
//		}
//
//		/// <summary>
//		/// Transfer the data by row
//		/// </summary>
//		/// <param name="ImportSnapshot"></param>
//		/// <param name="reader"></param>
//		/// <param name="allAssetFunds"></param>
//		/// <param name="allHiPortfolioCodes"></param>
//		private void transferHi3PricesLinked(int ImportSnapshot, IDataReader reader, Hashtable allAssetFunds, Hashtable allHiPortfolioCodes)
//		{
//			T.E();
//			try
//			{
//				Hi3PricesLinkedAssetFundDataRowValidator assetFundValidator = new Hi3PricesLinkedAssetFundDataRowValidator(allAssetFunds);
//				Hi3PricesSeriesDataRowValidator seriesValidator = new Hi3PricesSeriesDataRowValidator(allHiPortfolioCodes);
//				Hi3PricesAssetUnitPriceDataRowValidator assetUnitPriceValidator = new Hi3PricesAssetUnitPriceDataRowValidator();
//				ImportHi3PricesLinkedSqlPersister seriesPersister = new ImportHi3PricesLinkedSqlPersister(m_connectionString, "usp_ImportedFundPricesCreate", "ImportSnapshot", ImportSnapshot);
//
//				try
//				{
//					// Hook up the validation events
//					assetFundValidator.InvalidDataRowEvent += new InvalidDataRowDelegate(storeInvalidDataRowEvent);
//					seriesValidator.InvalidDataRowEvent += new InvalidDataRowDelegate(storeInvalidDataRowEvent);
//					assetUnitPriceValidator.InvalidDataRowEvent += new InvalidDataRowDelegate(storeInvalidDataRowEvent);
//
//					// Start the transactions on all the persisters
//					seriesPersister.InitialiseTransfer();
//
//					DataTable dataDefinition = reader.GetSchemaTable();
//
//					// Process each row in the current row set
//					while (reader.Read())
//					{
//						// Create a string array to represent the current row                
//						string[] dataRow = new string[dataDefinition.Rows.Count];
//						for (int i = 0; i < dataDefinition.Rows.Count; i++)
//						{
//							dataRow[i] = reader[i].ToString();
//						}
//						dataRow[0] = dataRow[0].Trim();
//
//						ValidationErrorSeverity[] seriesValidationError = new ValidationErrorSeverity[12];
//						ValidationErrorSeverity assetValidationError = assetFundValidator.Validate(dataRow, dataDefinition);
//						ValidationErrorSeverity assetUnitPriceValidationError = assetUnitPriceValidator.Validate(dataRow, dataDefinition);
//
//						bool OEICFund = false;
//
//						// If the asset fund is valid, check and save each class
//						if (assetValidationError == ValidationErrorSeverity.None && assetUnitPriceValidationError == ValidationErrorSeverity.None)
//						{
//							string assetFund = reader["AssetFundId"].ToString().Trim();
//							Fund activeFund = ((Fund) allAssetFunds[assetFund]);
//
//							// See if its n OEIC fund
//							if (activeFund is OEICFund)
//							{
//								OEICFund = true;
//							}
//
//							seriesValidator.OEIC = OEICFund;
//
//							// Check each series number
//							for (int seriesNumber = 1; seriesNumber <= 10; seriesNumber ++)
//							{
//								seriesValidator.SeriesNumber = seriesNumber;
//								seriesValidationError[seriesNumber + 1] = seriesValidator.Validate(dataRow, dataDefinition);
//							}
//						}
//
//						// If the asset fund is valid, we can try to persist the series
//						if (assetValidationError == ValidationErrorSeverity.None && assetUnitPriceValidationError == ValidationErrorSeverity.None)
//						{
//							seriesPersister.OEICFund = OEICFund;
//
//							for (int seriesNumber = 1; seriesNumber <= 10; seriesNumber ++)
//							{
//								// If the series is valid, try to persist it.
//								if (seriesValidationError[seriesNumber + 1] == ValidationErrorSeverity.None)
//								{
//									seriesPersister.SeriesNumber = seriesNumber;
//									seriesPersister.PersistRow(dataRow, dataDefinition);
//								}
//							}
//						}
//					}
//
//					// Make the persistances permanent
//					seriesPersister.CompleteTransfer();
//
//					// Clear the validators
//					assetFundValidator.InvalidDataRowEvent -= new InvalidDataRowDelegate(storeInvalidDataRowEvent);
//					seriesValidator.InvalidDataRowEvent -= new InvalidDataRowDelegate(storeInvalidDataRowEvent);
//					assetUnitPriceValidator.InvalidDataRowEvent -= new InvalidDataRowDelegate(storeInvalidDataRowEvent);
//				}
//				catch (Exception ex)
//				{
//					try
//					{
//						if (seriesPersister.IsTransferInProgress)
//						{
//							seriesPersister.CancelTransfer();
//						}
//					}
//					finally
//					{
//						// make sure we raise that exception on up
//						throw ex;
//					}
//				}
//				finally
//				{
//					// close the reader
//					if (null != reader)
//					{
//						try
//						{
//							reader.Close();
//							reader.Dispose();
//						}
//						finally
//						{
//							// no-op
//						}
//					}
//				}
//			}
//			finally
//			{
//				T.X();
//			}
//		}

		/// <summary>
		/// Remember any validation errors
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void storeValidationError(object sender, AssetFundDataValidator.InvalidDataEventArgs e)
		{
			T.E();
			try
			{
				if (m_validationErrors == null)
				{
					// First validation error we picked up so build the datatable
					this.m_validationErrors = new DataTable("ValidationErrors");

					this.m_validationErrors.Columns.Add(new DataColumn("Message"));
					this.m_validationErrors.Columns.Add(new DataColumn("Severity"));
//					for (int rowNumber = 0; rowNumber < e.DataDefinition.Rows.Count; rowNumber ++)
//					{
//						this.m_validationErrors.Columns.Add(new DataColumn(e.DataDefinition.Rows[rowNumber]["ColumnName"].ToString()));
//					}
				}

				// Set up the Row
				object[] columnValues = new object[m_validationErrors.Columns.Count];
				columnValues[0] = e.Message;
				columnValues[1] = e.ValidationErrorSeverity;

//				for (int index = 2; index < m_validationErrors.Columns.Count; index ++)
//				{
//					columnValues[index] = e.Row[index - 2];
//				}

				m_validationErrors.Rows.Add(columnValues);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Remember any validation errors
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void storeInvalidDataRowEvent(object sender, InvalidDataRowEventArgs e)
		{
			T.E();
			try
			{
				if (m_validationErrors == null)
				{
					// First validation error we picked up so build the datatable
					this.m_validationErrors = new DataTable("ValidationErrors");

					this.m_validationErrors.Columns.Add(new DataColumn("Message"));
					this.m_validationErrors.Columns.Add(new DataColumn("Severity"));
					for (int rowNumber = 0; rowNumber < e.DataDefinition.Rows.Count; rowNumber ++)
					{
						this.m_validationErrors.Columns.Add(new DataColumn(e.DataDefinition.Rows[rowNumber]["ColumnName"].ToString()));
					}
				}

				// Set up the Row
				object[] columnValues = new object[m_validationErrors.Columns.Count];
				columnValues[0] = e.Message;
				columnValues[1] = e.ValidationErrorSeverity;

				for (int index = 2; index < m_validationErrors.Columns.Count; index ++)
				{
					columnValues[index] = e.Row[index - 2];
				}

				m_validationErrors.Rows.Add(columnValues);
			}
			finally
			{
				T.X();
			}
		}

//		/// <summary>
//		/// Import Hi3 Prices for Linked fund prices
//		/// </summary>
//		private void importHi3PricesLinked()
//		{
//			T.E();
//			try
//			{
//				Column[] dataColumns = ImportController.generateHi3PricesLinkedColumns();
//
//				// Set up the data reader
//				CsvFileReader fileReader = new CsvFileReader(m_importFileName, dataColumns);
//				DateTime valuationDate = DateTime.MinValue;
//
//				// Need to peek at the first row to get the Valuation Date.
//				bool headerSuccess = fileReader.Read();
//
//				if (headerSuccess)
//				{
//					string headerDateAsString = fileReader[(int) ImportHi3PricesLinkedSqlPersister.Hi3PricesLinkedColumnPosition.ValuationDate].ToString();
//					//string headerTimeAsString = fileReader[(int) ImportHi3PricesLinkedSqlPersister.Hi3PricesLinkedColumnPosition.ValuationTime].ToString();
//
//					if (headerDateAsString.Length != 8)
//					{
//						throw new ApplicationException(String.Format("{0} was not in a recognised date format.", headerDateAsString));
//					}
//
//					int dayNumber = int.Parse(headerDateAsString.Substring(0, 2));
//					int monthNumber = int.Parse(headerDateAsString.Substring(3, 2));
//					int yearNumber = 2000 + int.Parse(headerDateAsString.Substring(6, 2));
//
//					valuationDate = new DateTime(yearNumber, monthNumber, dayNumber);
//
//					if (! valuationDate.Date.Equals(DateTime.Today))
//					{
//						throw new ApplicationException(String.Format("File had an import date of {0} but only {1} is allowed. ", valuationDate.Date.ToShortDateString(), DateTime.Today.Date.ToShortDateString()));
//					}
//				}
//				else
//				{
//					throw new ApplicationException("Failed to read the file header record.");
//				}
//
//				// Reset the file so we can proces in 1 go.
//				fileReader.Close();
//				fileReader.Dispose();
//				fileReader = new CsvFileReader(m_importFileName, dataColumns);
//
//				// Create the Import Source
//				ImportPersister importPersister = new ImportPersister(this.m_connectionString);
//				this.ImportSnapshot = importPersister.SaveImportSource(m_importFileName, m_companyCode);
//
//				// Load the existing Funds for this company
//				FundController fundController = new FundController(this.m_connectionString);
//				FundCollection companyFunds = fundController.LoadPartialFundsForCompany(m_companyCode);
//
//				// We need to remeber all the valid AssetFund and HiPortfolioCodes
//				Hashtable allAssetFunds = new Hashtable();
//				Hashtable allHiPortfolioCodes = new Hashtable(companyFunds.Count);
//				for (int i = 0; i < companyFunds.Count; i ++)
//				{
//					allHiPortfolioCodes.Add(companyFunds[i].HiPortfolioCode.Trim(), companyFunds[i]);
//					if (allAssetFunds.Contains(companyFunds[i].ParentAssetFundID.Trim()) == false)
//					{
//						allAssetFunds.Add(companyFunds[i].ParentAssetFundID.Trim(), companyFunds[i]);
//					}
//				}
//
//				this.transferHi3PricesLinked(ImportSnapshot, fileReader, allAssetFunds, allHiPortfolioCodes);
//			}
//			finally
//			{
//				T.X();
//			}
//		}
//
//		/// <summary>
//		/// Import Hi3 Prices for Compsite Price Series Funds
//		/// </summary>
//		private void importHi3PricesComposite()
//		{
//			T.E();
//			try
//			{
//				Column[] dataColumns = ImportController.generateHi3PricesCompositeColumns();
//
//				// Set up the data reader
//				CsvFileReader fileReader = new CsvFileReader(m_importFileName, dataColumns);
//				DateTime valuationDate = DateTime.MinValue;
//
//				// Need to peek at the first row to get the Valuation Date.
//				bool headerSuccess = fileReader.Read();
//
//				if (headerSuccess)
//				{
//					string headerDateAsString = fileReader[(int) ImportHi3PricesCompositeSqlPersister.Hi3PricesCompositeColumnPosition.ValuationDate].ToString();
//					//string headerTimeAsString = fileReader[(int) ImportHi3PricesCompositeSqlPersister.Hi3PricesCompositeColumnPosition.ValuationTime].ToString();
//
//					if (headerDateAsString.Length != 8)
//					{
//						throw new ApplicationException(String.Format("{0} was not in a recognised date format.", headerDateAsString));
//					}
//
//					int dayNumber = int.Parse(headerDateAsString.Substring(0, 2));
//					int monthNumber = int.Parse(headerDateAsString.Substring(3, 2));
//					int yearNumber = 2000 + int.Parse(headerDateAsString.Substring(6, 2));
//
//					valuationDate = new DateTime(yearNumber, monthNumber, dayNumber);
//
//					if (! valuationDate.Date.Equals(DateTime.Today))
//					{
//						throw new ApplicationException(String.Format("File had an import date of {0} but only {1} is allowed. ", valuationDate.Date.ToShortDateString(), DateTime.Today.Date.ToShortDateString()));
//					}
//				}
//				else
//				{
//					throw new ApplicationException("Failed to read the file header record.");
//				}
//
//				// Reset the file so we can proces in 1 go.
//				fileReader.Close();
//				fileReader.Dispose();
//				fileReader = new CsvFileReader(m_importFileName, dataColumns);
//
//				// Create the Import Source
//				ImportPersister importPersister = new ImportPersister(this.m_connectionString);
//				this.ImportSnapshot = importPersister.SaveImportSource(m_importFileName, m_companyCode);
//
//				// Load the existing Funds for this company
//				FundController fundController = new FundController(m_connectionString);
//				FundCollection companyFunds = fundController.LoadPartialFundsForCompany(m_companyCode);
//
//				// We need to remeber all the valid AssetFund and HiPortfolioCodes
//				Hashtable allAssetFunds = new Hashtable();
//				Hashtable allHiPortfolioCodes = new Hashtable(companyFunds.Count);
//				for (int i = 0; i < companyFunds.Count; i ++)
//				{
//					allHiPortfolioCodes.Add(companyFunds[i].HiPortfolioCode.Trim(), companyFunds[i]);
//					if (allAssetFunds.Contains(companyFunds[i].ParentAssetFundID.Trim()) == false)
//					{
//						allAssetFunds.Add(companyFunds[i].ParentAssetFundID.Trim(), companyFunds[i]);
//					}
//				}
//
//				this.transferHi3PricesComposite(ImportSnapshot, fileReader, allAssetFunds, allHiPortfolioCodes);
//			}
//			finally
//			{
//				T.X();
//			}
//		}

//		private static DateTime checkFileNameForValuationPoint(string fileName)
//		{
//			FileInfo file = new FileInfo(fileName);
//			if (file.Name.IndexOf(".") == 9)
//			{
//				throw new ApplicationException(String.Format("The file name {0} was not in a recognised date format.", fileName));
//			}
//			string datePart = file.Name.Substring(0, 8);
//
//			int dayNumber = int.Parse(datePart.Substring(0, 2));
//			int monthNumber = int.Parse(datePart.Substring(2, 2));
//			int yearNumber = 2000 + int.Parse(datePart.Substring(4, 2));
//			int hourNumber = int.Parse(datePart.Substring(6, 2));
//
//			DateTime valuationDate = new DateTime(yearNumber, monthNumber, dayNumber, hourNumber, 0, 0);
//
//			if (! valuationDate.Date.Equals(DateTime.Today))
//			{
//				throw new ApplicationException(String.Format("File had an import date of {0} but only {1} is allowed. ", valuationDate.Date.ToShortDateString(), DateTime.Today.Date.ToShortDateString()));
//			}
//
//			return valuationDate;
//		}

		/// <summary>
		/// Import the Hi3 Composite Fund Splits
		/// </summary>
		private void importHi3CompositeFundSplits()
		{
			T.E();
			SqlTransaction transaction = null;

			try
			{
				
				// Set up the Columns that the import file should have
				//the names given here are displayed in the UI grid when import errors are raised
				Column compositeFundCode = new Column("Composite Fund", Type.GetType("System.String"));
				Column linkedFundCode = new Column("Linked Fund Security Code", Type.GetType("System.String"));
				Column marketValue = new Column("Value", Type.GetType("System.Decimal"));
				Column valDate = new Column("ValuationDate", Type.GetType("System.String"));
				Column valTime = new Column("ValuationTime", Type.GetType("System.String"));

				Column[] dataColumns = new Column[5] {compositeFundCode, linkedFundCode, marketValue,valDate,valTime};

				// Set up the data reader
				CsvFileReader fileReader = new CsvFileReader(m_importFileName, dataColumns);
				
				fileReader = new CsvFileReader(m_importFileName, dataColumns);

				// Begin your transaction
				SqlConnection connections = new SqlConnection(m_connectionString);
				connections.Open();
				transaction = connections.BeginTransaction();

				// Create the Import Source
				getNewSnapShotId(transaction,SnapshotProcess.Import);

				if (ImportSnapshot != null)
				{
					// Load the existing Funds for this company (partial objects)
					FundController fundController = new FundController(m_connectionString);
					FundCollection companyFunds = fundController.LoadPartialFundsForCompany(m_companyCode);

					// We need to remember all the valid AssetFund and HiPortfolioCodes
					Hashtable companyAssetFundsHash = new Hashtable();
					Hashtable allCompanySecurityCodes = new Hashtable( companyFunds.Count );	

					//populate a hastable of all the funds for the current company and 
					//another for all the Asset Funds for the company
					for (int i = 0; i < companyFunds.Count; i ++)
					{
						Fund fund = companyFunds[i];
						
						if ( allCompanySecurityCodes.Contains( fund.SecurityCode.Trim() ) == false )
						{
							allCompanySecurityCodes.Add( fund.SecurityCode.Trim().ToUpper() , fund );
						}
						if (companyAssetFundsHash.Contains(fund.ParentAssetFundID.Trim()) == false)
						{
							companyAssetFundsHash.Add(fund.ParentAssetFundID.Trim(), fund);
						}
					}

					this.transferCompositeSplit(ImportSnapshot, fileReader, companyAssetFundsHash, allCompanySecurityCodes, transaction);

					// Move the data from the temporary table to the real table and calculate 
					//the weightings for each linked fund in the composite
					ImportPersister importPersister = new ImportPersister(m_connectionString);

					DateTime valuationPoint = new DateTime(m_CompanyValuationPoint.Year, m_CompanyValuationPoint.Month, m_CompanyValuationPoint.Day,m_CompanyValuationPoint.Hour,m_CompanyValuationPoint.Minute,0);

					importPersister.TransferImportedCompositeFundWeightings(valuationPoint, ImportSnapshot, transaction);

				}
				else
				{
					throw new ApplicationException("Failed to create an ImportSource - successFlag was 0");
				}

				transaction.Commit();
			}
			catch (ApplicationException appEx)
			{
				throw appEx;
			}
			catch
			{
				try
				{
					T.Log("Rollback transaction");
					transaction.Rollback();
					throw; //  This will simply send the database exception back to the client
				}
				catch (SqlException sqlEx)
				{
					// Trap any rollBack exceptions
					if (null != transaction.Connection)
					{
						string messageText = "An exception of type {0} was encountered while attempting to roll back the transaction.";
						throw new TransactionRollbackException(String.Format(messageText, sqlEx.GetType()), "State = " + transaction.Connection.State.ToString(), sqlEx);
					}
				}
			}
			finally
			{
				T.X();
			}

		}
		

		/// <summary>
		/// Import the Hi3 Asset Fund Splits
		/// </summary>
		private void importHi3AssetFundSplit(string companyCode)
		{
			T.E();
			SqlTransaction transaction = null;

			try
			{
				// Set up the Columns
				Column assetCode = new Column("assetFundId", Type.GetType("System.String"));
				Column currencyCode = new Column("currencyCode", Type.GetType("System.String"));
				Column assetValue = new Column("assetValue", Type.GetType("System.Decimal"));
				Column[] dataColumns = new Column[3] {assetCode, currencyCode, assetValue};

				// Set up the data reader
				CsvFileReader fileReader = new CsvFileReader(m_importFileName, dataColumns);

				// get the list of valid asset fund currency combinations
				ImportPersister importPersister = new ImportPersister(this.m_connectionString);

				// Begin your transaction
				SqlConnection connections = new SqlConnection(m_connectionString);
				connections.Open();
				transaction = connections.BeginTransaction();

				getNewSnapShotId(transaction,SnapshotProcess.Import);

				if (ImportSnapshot!=null)
				{

					// Set up the persisters
					//IPersistRow sqlPersister = new SqlPersister( m_connectionString, "usp_ImportedAssetFundSplitCreate" , "usp_ImportedAssetFundSplitClear" );
					ImportWorkingSplitTableSqlPersister sqlPersister = new ImportWorkingSplitTableSqlPersister(m_connectionString, "usp_ImportedSplitWorkingTableCreate", "snapshotID", ImportSnapshot);
					sqlPersister.tempTran = transaction;
					IPersistRow[][] persisters = new IPersistRow[1][];
					persisters[0] = new IPersistRow[] {sqlPersister};


					IDataRowValidator[][] validators = setupDataRowValidatorsForAssetFundSplitImport(companyCode);

					// Create the data transporter
				
					DataTransporter dataTransporter = new DataTransporter(false, fileReader, validators, persisters);

					// Do the import
					dataTransporter.RunDataTransfer();

					removeValidationHandlers(validators);

					validateTemporarlyImportedAFS(importPersister, transaction, companyCode);

					// Move the data from the temporary table to the reaL table
					importPersister.TransferImportedAssetFundWeightings(ImportSnapshot, transaction);

				}
				else
				{
					throw new ApplicationException("Failed to create an ImportSource - successFlag was 0");
				}

				transaction.Commit();
			}
			catch
			{
				transaction.Rollback();

				throw;
			}
			finally
			{
				T.X();
			}
		}

		private void removeValidationHandlers(IDataRowValidator[][] validators)
		{
			//remove validation handlers
			foreach (IDataRowValidator[] rowValidators in validators)
			{
				foreach (IDataRowValidator dataRowValidator in rowValidators)
				{
					dataRowValidator.InvalidDataRowEvent -= new InvalidDataRowDelegate(storeInvalidDataRowEvent);
				}
			}
		}

		private IDataRowValidator[][] setupDataRowValidatorsForAssetFundSplitImport(string companyCode)
		{
			AssetFundLookupPersister assetFundLookupPersister=new AssetFundLookupPersister(m_connectionString);
			SimpleStringLookupCollection allAssetFunds= assetFundLookupPersister.LoadForCompany(companyCode);
			Hashtable allAssetFundsHash=new Hashtable();
			foreach (SimpleStringLookup assetFund in allAssetFunds)
			{
				allAssetFundsHash.Add(assetFund.Key.Trim(), assetFund.DisplayValue);
			}

			StockMarketPersister stockMarketPersister=new StockMarketPersister(m_connectionString);
			StockMarketCollection stockMarkets= stockMarketPersister.LoadMarketIndices();

			AssetFundDataRowValidator assetFundValidator = new AssetFundDataRowValidator( allAssetFundsHash ,(int) ImportWorkingSplitTableSqlPersister.ColumnPosition.primaryKey,m_importFileType);
			CountryStockmarketDataRowValidator countryStockmarketDataRowValidator = new CountryStockmarketDataRowValidator( stockMarkets ,(int) ImportWorkingSplitTableSqlPersister.ColumnPosition.secondaryKey);
			IDataRowValidator[][] validators=new IDataRowValidator[1][];
			validators[0]=new IDataRowValidator[]{assetFundValidator,countryStockmarketDataRowValidator};

			attachValidationHandlers(validators);

			return validators;
		}

		private void attachValidationHandlers(IDataRowValidator[][] validators)
		{
			foreach (IDataRowValidator[] rowValidators in validators)
			{
				foreach (IDataRowValidator dataRowValidator in rowValidators)
				{
					dataRowValidator.InvalidDataRowEvent += new InvalidDataRowDelegate(storeInvalidDataRowEvent);
				}
			}
		}

		private void validateTemporarlyImportedAFS(ImportPersister importPersister, SqlTransaction transaction, string companyCode)
		{
			// Set up the validators
			AssetFundDataValidator assetFundValidator = new AssetFundDataValidator();
			assetFundValidator.InvalidDataEvent += new AssetFundDataValidator.InvalidDataDelegate(storeValidationError);
	
			assetFundValidator.CompareSplits(importPersister,ImportSnapshot, transaction,companyCode);
		}

		#endregion

		#region IDisposable Members

		/// <summary>
		/// Disposes this instance.
		/// </summary>
		public void Dispose()
		{
			// TODO:  Add ImportController.Dispose implementation
			m_validationErrors.Dispose();
			m_ImportedData.Dispose();
		}

		#endregion
	}

	

}