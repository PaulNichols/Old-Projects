using System;
using System.Collections;
using System.Text;
using HBOS.FS.AMP.UPD.Types;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.BenchMark;
using HBOS.FS.AMP.UPD.Types.Countries;
using HBOS.FS.AMP.UPD.Types.Currency;
using HBOS.FS.AMP.UPD.Types.Funds;
using HBOS.FS.AMP.UPD.Types.StockMarketIndex;
using HBOS.FS.AMP.UPD.WinUI.Classes;

namespace HBOS.FS.AMP.UPD.WinUI.UserControls
{
	/// <summary>
	/// Wraps the StockMarket class for display purposes
	/// </summary>
	public class StaticDataStockMarketLookupDecorator
	{
		private StockMarket m_StockMarket;
		private string m_DisplayText;

		/// <summary>
		/// Creates a decorated object from a StockMarket.
		/// </summary>
		/// <param name="stockMarket">StockMarket.</param>
		/// <returns></returns>
		public static StaticDataStockMarketLookupDecorator ToDecoratedObject(StockMarket stockMarket)
		{
			return new StaticDataStockMarketLookupDecorator(stockMarket);
		}

		/// <summary>
		/// Takes a decorated object and creates an Asset Fund.
		/// </summary>
		/// <param name="decoratedStockMarket">Decorated StockMarket.</param>
		/// <returns></returns>
		public static StockMarket FromDecoratedObject(StaticDataStockMarketLookupDecorator decoratedStockMarket)
		{
			return decoratedStockMarket.m_StockMarket;
		}

		/// <summary>
		/// Takes a decorated list of objects and returns a StockMarket Collection.
		/// </summary>
		/// <param name="decoratedStockMarkets">Decorated StockMarket.</param>
		/// <returns></returns>
		public static StockMarketCollection FromDecoratedList(IList decoratedStockMarkets)
		{
			StockMarketCollection returnStockMarketCollection = new StockMarketCollection();
			foreach (StaticDataStockMarketLookupDecorator decoratedStockMarket in decoratedStockMarkets)
			{
				returnStockMarketCollection.Add(FromDecoratedObject(decoratedStockMarket));
			}

			return returnStockMarketCollection;
		}

		/// <summary>
		/// Toes the decorated list.
		/// </summary>
		/// <param name="StockMarketCollection">StockMarket collection.</param>
		/// <returns></returns>
		public static IList ToDecoratedList(IList StockMarketCollection)
		{
			ArrayList returnList = new ArrayList();
			foreach (StockMarket StockMarket in StockMarketCollection)
			{
				returnList.Add(ToDecoratedObject(StockMarket));
			}
			return returnList;
		}

		private StaticDataStockMarketLookupDecorator(StockMarket StockMarket)
		{
			this.m_StockMarket = StockMarket;

			m_DisplayText=StockMarket.IndexName;
		}

		/// <summary>
		/// Gets the display text.
		/// </summary>
		/// <value></value>
		public string DisplayText
		{
			get{return m_DisplayText;}
		}

	

		/// <summary>
		/// Gets the country code.
		/// </summary>
		/// <value></value>
		public int MarketId
		{
			get {return m_StockMarket.MarketIndexID; }
		}
	}

	/// <summary>
	/// Wraps the Price File class for display purposes
	/// </summary>
	public class StaticDataPriceFileLookupDecorator
	{
		private PriceFile m_PriceFile;
		private string m_DisplayText;
		private int m_PriceFileId;

		/// <summary>
		/// Creates a decorated object from a Price File type.
		/// </summary>
		/// <param name="priceFile">PriceFile.</param>
		/// <returns></returns>
		public static StaticDataPriceFileLookupDecorator ToDecoratedObject(PriceFile priceFile)
		{
			return new StaticDataPriceFileLookupDecorator(priceFile);
		}

		/// <summary>
		/// Takes a decorated object and creates an Price File.
		/// </summary>
		/// <param name="decoratedPriceFile">Decorated Price File.</param>
		/// <returns></returns>
		public static PriceFile FromDecoratedObject(StaticDataPriceFileLookupDecorator decoratedPriceFile)
		{
			return decoratedPriceFile.PriceFile;
		}

		/// <summary>
		/// Takes a decorated list of objects and returns a Price File Collection.
		/// </summary>
		/// <param name="decoratedPriceFiles">Decorated Price File.</param>
		/// <returns></returns>
		public static PriceFileCollection FromDecoratedList(IList decoratedPriceFiles)
		{
			PriceFileCollection returnPriceFileCollection = new PriceFileCollection();
			foreach (StaticDataPriceFileLookupDecorator decoratedPriceFile in decoratedPriceFiles)
			{
				returnPriceFileCollection.Add(FromDecoratedObject(decoratedPriceFile));
			}

			return returnPriceFileCollection;
		}

		/// <summary>
		/// Toes the decorated list.
		/// </summary>
		/// <param name="priceFileCollection">Price File collection.</param>
		/// <returns></returns>
		public static IList ToDecoratedList(IList priceFileCollection)
		{
			ArrayList returnList = new ArrayList();
			foreach (PriceFile priceFile in priceFileCollection)
			{
				returnList.Add(ToDecoratedObject(priceFile));
			}
			return returnList;
		}

		/// <summary>
		/// Creates a new <see cref="StaticDataPriceFileLookupDecorator"/> instance.
		/// </summary>
		/// <param name="displayValue">Display value.</param>
		/// <param name="key">Key.</param>
		public  StaticDataPriceFileLookupDecorator(string displayValue, int key)
		{
			m_PriceFileId=key;
			m_DisplayText=displayValue;
		}

		private StaticDataPriceFileLookupDecorator(PriceFile priceFile)
		{
			this.m_PriceFile = priceFile;

			
			if (priceFile.FileName!=null && priceFile.Extension!=null)
			{
				m_DisplayText= priceFile.FileName + "." + priceFile.Extension;
			}

			m_PriceFileId=priceFile.FileId;
		}

		/// <summary>
		/// Gets the display text.
		/// </summary>
		/// <value></value>
		public string DisplayValue
		{
			get{return m_DisplayText;}
		}

	
		/// <summary>
		/// Gets the price file id.
		/// </summary>
		/// <value></value>
		public int Key
		{
			get{return m_PriceFileId;}
		}

		internal PriceFile PriceFile
		{
			get { return m_PriceFile; }
		}

	}

	/// <summary>
	/// Wraps the country class for display purposes
	/// </summary>
	public class StaticDataCountryLookupDecorator
	{
		private Country m_country;
		private string m_DisplayText;
		private string m_CountryCode;

		/// <summary>
		/// Creates a decorated object from a Country.
		/// </summary>
		/// <param name="country">Country.</param>
		/// <returns></returns>
		public static StaticDataCountryLookupDecorator ToDecoratedObject(Country country)
		{
			return new StaticDataCountryLookupDecorator(country);
		}

		/// <summary>
		/// Creates a new <see cref="StaticDataPriceFileLookupDecorator"/> instance.
		/// </summary>
		/// <param name="displayValue">Display value.</param>
		/// <param name="key">Key.</param>
		public  StaticDataCountryLookupDecorator(string displayValue, string key)
		{
			m_CountryCode=key;
			m_DisplayText=displayValue;
		}

		/// <summary>
		/// Takes a decorated object and creates an Asset Fund.
		/// </summary>
		/// <param name="decoratedCountry">Decorated Country.</param>
		/// <returns></returns>
		public static Country FromDecoratedObject(StaticDataCountryLookupDecorator decoratedCountry)
		{
			return decoratedCountry.Country;
		}

		/// <summary>
		/// Takes a decorated list of objects and returns a Country Collection.
		/// </summary>
		/// <param name="decoratedCountries">Decorated Country.</param>
		/// <returns></returns>
		public static CountryCollection FromDecoratedList(IList decoratedCountries)
		{
			CountryCollection returnCountryCollection = new CountryCollection();
			foreach (StaticDataCountryLookupDecorator decoratedcountry in decoratedCountries)
			{
				returnCountryCollection.Add(FromDecoratedObject(decoratedcountry));
			}

			return returnCountryCollection;
		}

		/// <summary>
		/// Toes the decorated list.
		/// </summary>
		/// <param name="countryCollection">Country collection.</param>
		/// <returns></returns>
		public static IList ToDecoratedList(IList countryCollection)
		{
			ArrayList returnList = new ArrayList();
			foreach (Country country in countryCollection)
			{
				returnList.Add(ToDecoratedObject(country));
			}
			return returnList;
		}

		private StaticDataCountryLookupDecorator(Country country)
		{
			this.m_country = country;

			
			if (Country.CountryName!=null)
			{
				if (Country.CountryName.Trim().Length > 0)
				{
					m_DisplayText= Country.CountryName + "   (" + Country.CountryCode + ")";
				}
				else
				{
					m_DisplayText= Country.CountryName;
				}
			}

			m_CountryCode=country.CountryCode;
		}

		/// <summary>
		/// Gets the display text.
		/// </summary>
		/// <value></value>
		public string DisplayText
		{
			get{return m_DisplayText;}
		}

		internal Country Country
		{
			get { return m_country; }
		}

		/// <summary>
		/// Gets the country code.
		/// </summary>
		/// <value></value>
		public string CountryCode
		{
			get { return m_CountryCode; }
		}

	}

	/// <summary>
	/// Wraps the Currency class for display purposes
	/// </summary>
	public class StaticDataCurrencyLookupDecorator
	{
		private Currency m_currency;
		private string m_DisplayText;
		private string m_CurrencyCode;

		/// <summary>
		/// Creates a decorated object from a Currency.
		/// </summary>
		/// <param name="currency">Currency.</param>
		/// <returns></returns>
		public static StaticDataCurrencyLookupDecorator ToDecoratedObject(Currency currency)
		{
			return new StaticDataCurrencyLookupDecorator(currency);
		}

		/// <summary>
		/// Creates a new <see cref="StaticDataPriceFileLookupDecorator"/> instance.
		/// </summary>
		/// <param name="displayValue">Display value.</param>
		/// <param name="key">Key.</param>
		public  StaticDataCurrencyLookupDecorator(string displayValue, string key)
		{
			m_CurrencyCode=key;
			m_DisplayText=displayValue;
		}

		/// <summary>
		/// Takes a decorated object and creates a Currency.
		/// </summary>
		/// <param name="decoratedCurrency">Decorated Currency.</param>
		/// <returns></returns>
		public static Currency FromDecoratedObject(StaticDataCurrencyLookupDecorator decoratedCurrency)
		{
			return decoratedCurrency.Currency;
		}

		/// <summary>
		/// Takes a decorated list of objects and returns a Currency Collection.
		/// </summary>
		/// <param name="decoratedCurrencies">Decorated Currency.</param>
		/// <returns></returns>
		public static CurrencyCollection FromDecoratedList(IList decoratedCurrencies)
		{
			CurrencyCollection returnCurrencyCollection = new CurrencyCollection();
			foreach (StaticDataCurrencyLookupDecorator decoratedCurrency in decoratedCurrencies)
			{
				returnCurrencyCollection.Add(FromDecoratedObject(decoratedCurrency));
			}

			return returnCurrencyCollection;
		}

		/// <summary>
		/// Toes the decorated list.
		/// </summary>
		/// <param name="currencyCollection">Currency collection.</param>
		/// <returns></returns>
		public static IList ToDecoratedList(IList currencyCollection)
		{
			ArrayList returnList = new ArrayList();
			foreach (Currency currency in currencyCollection)
			{
				returnList.Add(ToDecoratedObject(currency));
			}
			return returnList;
		}

		private StaticDataCurrencyLookupDecorator(Currency currency)
		{
			this.m_currency = currency;

			
			if (currency.CurrencyName!=null)
			{
				if (currency.CurrencyName.Trim().Length > 0)
				{
					m_DisplayText= currency.CurrencyName + "   (" + currency.CurrencyCode + ")";
				}
				else
				{
					m_DisplayText= currency.CurrencyName;
				}
			}

			m_CurrencyCode=m_currency.CurrencyCode;
		}

		/// <summary>
		/// Gets the display text.
		/// </summary>
		/// <value></value>
		public string DisplayText
		{
			get{return m_DisplayText;}
		}

		internal Currency Currency
		{
			get { return m_currency; }
		}

		/// <summary>
		/// Gets the Currency code.
		/// </summary>
		/// <value></value>
		public string CurrencyCode
		{
			get {return m_CurrencyCode; }
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class AssetMovementConstituentPropertiesDecorator:AssetMovementConstituentDecorator
	{
		private string m_BenchmarkMovement;
		private string m_CurrencyMovement;
		private string m_TotalMovement;
		private string m_Effect;
		private string m_PreviousBenchmarkValue;
		private string m_CurrentBenchmarkValue;
		private string m_PreviousAssetFundExchangeRate="0";
		private string m_CurrentAssetFundExchangeRate;
		private string m_CurrentAssetBenchmarkExchangeRate="0";
		private string m_PreviousAssetBenchmarkExchangeRate="0";
		private string m_Availablity;
		private string m_AvailabilityMessage;
		private string m_CurrentBenchmarkExchangeRate="0";

		/// <summary>
		///  Builds the decorated constituent list.
		/// </summary>
		/// <param name="constitutes">Constitutes.</param>
		/// <returns></returns>
		internal new static IList FromConstituentListToDecorated(AssetMovementConstituentCollection constitutes)
		{
			ArrayList completeList = new ArrayList();

			foreach (AssetMovementConstituent constitute in constitutes)
			{
				completeList.Add(FromConstituentToDecoratedConstituent(constitute));
			}
			return completeList;
		}

		internal new static AssetMovementConstituentPropertiesDecorator FromConstituentToDecoratedConstituent(AssetMovementConstituent constituent)
		{
			return new AssetMovementConstituentPropertiesDecorator(constituent);
		}



		/// <summary>
		/// Creates a new <see cref="AssetMovementConstituentPropertiesDecorator"/> instance.
		/// </summary>
		public AssetMovementConstituentPropertiesDecorator():base()
		{
		}

		/// <summary>
		/// Creates a new <see cref="AssetMovementConstituentPropertiesDecorator"/> instance.
		/// </summary>
		/// <param name="assetMovementConstituent">Asset movement constituent.</param>
		private AssetMovementConstituentPropertiesDecorator( AssetMovementConstituent assetMovementConstituent):base(assetMovementConstituent)
		{
			
			const string percentFormat="N4";

			BenchmarkMovement=convertToPercentWithoutSign(assetMovementConstituent.BenchMark.Movement,percentFormat);
			CurrencyMovement=convertToPercentWithoutSign(assetMovementConstituent.CurrencyMovement(),percentFormat);
			TotalMovement=convertToPercentWithoutSign(assetMovementConstituent.CalculateMovement(),percentFormat);
			Effect=convertToPercentWithoutSign(assetMovementConstituent.CalculateEffect(),percentFormat);
			PreviousBenchmarkValue=assetMovementConstituent.BenchMark.PreviousBenchmarkValue.ToString(percentFormat);
			CurrentBenchmarkValue=assetMovementConstituent.BenchMark.CurrentBenchmarkValue.ToString(percentFormat);
			PreviousAssetFundExchangeRate=assetMovementConstituent.ParentAssetFund.Currency.PreviousRate.ToString(percentFormat);
			CurrentAssetFundExchangeRate=assetMovementConstituent.ParentAssetFund.Currency.CurrentRate.ToString(percentFormat);
			if (assetMovementConstituent.BenchMark.Currency!=null)
			{
				PreviousBenchmarkExchangeRate=assetMovementConstituent.BenchMark.Currency.PreviousRate.ToString(percentFormat);
				CurrentBenchmarkExchangeRate=assetMovementConstituent.BenchMark.Currency.CurrentRate.ToString(percentFormat);
			}
			else
			{
				PreviousBenchmarkExchangeRate="0";
				CurrentBenchmarkExchangeRate="0";
			}
			CurrentAssetExchangeRate=assetMovementConstituent.ParentAssetFund.Currency.CurrentRate.ToString(percentFormat);
			Availablity=FundDecorator.TranslatePascalCasedString(assetMovementConstituent.BenchMark.Availability.ToString());
		
			SecondLevelAuthorisationViewController.BenchMarkAvailabilityInformation result;
			result = new SecondLevelAuthorisationViewController.BenchMarkAvailabilityInformation(assetMovementConstituent.BenchMark);
			AvailabilityMessage=result.UniqueMessagesString;

		}

		/// <summary>
		/// Gets or sets the current benchmark exchange rate.
		/// </summary>
		/// <value></value>
		public string CurrentBenchmarkExchangeRate
		{
			get{return m_CurrentBenchmarkExchangeRate;}
			set { m_CurrentBenchmarkExchangeRate=value; }
		}

		private string convertToPercentWithoutSign(decimal decimalToConvert, string format)
		{

			return (decimalToConvert*100).ToString(format);
		}

		/// <summary>
		/// Gets or sets the Availiblity of the benchmark.
		/// </summary>
		/// <value></value>
		public string Availablity
		{
			get{return m_Availablity;}
			set { m_Availablity=value; }
		}

		/// <summary>
		/// Gets or sets the Availiblity message of the benchmark.
		/// </summary>
		/// <value></value>
		public string AvailabilityMessage
		{
			get{return m_AvailabilityMessage;}
			set { m_AvailabilityMessage=value; }
		}

		/// <summary>
		/// Gets or sets the previous benchmark exchange rate.
		/// </summary>
		/// <value></value>
		public string PreviousBenchmarkExchangeRate
		{
			get{return m_PreviousAssetBenchmarkExchangeRate;}
			set { m_PreviousAssetBenchmarkExchangeRate=value; }
		}

		/// <summary>
		/// Gets or sets the current asset exchange rate.
		/// </summary>
		/// <value></value>
		public string CurrentAssetExchangeRate
		{
			get{return m_CurrentAssetBenchmarkExchangeRate;}
			set { m_CurrentAssetBenchmarkExchangeRate=value; }
		}

		/// <summary>
		/// Gets or sets the current asset fund exchange rate.
		/// </summary>
		/// <value></value>
		public string CurrentAssetFundExchangeRate
		{
			get{return m_CurrentAssetFundExchangeRate;}
			set { m_CurrentAssetFundExchangeRate=value; }
		}

		/// <summary>
		/// Gets or sets the previous asset fund exchange rate.
		/// </summary>
		/// <value></value>
		public string PreviousAssetFundExchangeRate
		{
			get{return m_PreviousAssetFundExchangeRate;}
			set { m_PreviousAssetFundExchangeRate=value; }
		}

		/// <summary>
		/// Gets or sets the current benchmark value.
		/// </summary>
		/// <value></value>
		public string CurrentBenchmarkValue
		{
			get{return m_CurrentBenchmarkValue;}
			set { m_CurrentBenchmarkValue=value; }
		}

		/// <summary>
		/// Gets or sets the previous benchmark value.
		/// </summary>
		/// <value></value>
		public string PreviousBenchmarkValue
		{
			get{return m_PreviousBenchmarkValue;}
			set { m_PreviousBenchmarkValue=value; }
		}

		/// <summary>
		/// Gets or sets the effect.
		/// </summary>
		/// <value></value>
		public string Effect
		{
			get{return m_Effect;}
			set { m_Effect=value; }
		}

		/// <summary>
		/// Gets or sets the total movement.
		/// </summary>
		/// <value></value>
		public string TotalMovement
		{
			get{return m_TotalMovement;}
			set { m_TotalMovement=value ;}
		}

		/// <summary>
		/// Gets or sets the currency movement.
		/// </summary>
		/// <value></value>
		public string CurrencyMovement
		{
			get{return m_CurrencyMovement;}
			set { m_CurrencyMovement=value; }
		}

		/// <summary>
		/// Gets or sets the benchmark movement.
		/// </summary>
		/// <value></value>
		public string BenchmarkMovement
		{
			get { return m_BenchmarkMovement; }
			set { m_BenchmarkMovement = value; }
		}

	}


	/// <summary>
	/// Display Properties due to grid limitations.
	/// </summary>
	public class AssetFundDecorator
	{
		private string m_FullName;
		private AssetFund.AssetFundTypeEnum m_AssetFundType;
		private string m_AssetFundTypeString;
		private bool m_IsDirty;
		private bool m_IsNew;
		private bool m_WithinAssetMovementTolerance;
		private string m_AssetMovementToleranceDisplay;
		private string m_AssetMovementToleranceDisplay2;
		private string m_UnitPriceDisplay;
		private IList m_AssetMovementConstituents;
		private string m_AssetMovementVarianceDisplay;
		private string m_UnitPriceMovementDisplay;
		private string m_PredictedAssetMovementDisplay;
		private string m_WithinAssetMovementToleranceDisplay;
		private  AssetFund assetFund;

		/// <summary>
		/// Creates a decorated object from an Asset Fund.
		/// </summary>
		/// <param name="assetFund">Asset fund.</param>
		/// <returns></returns>
		public static AssetFundDecorator ToDecoratedObject(AssetFund assetFund)
		{
			return new AssetFundDecorator(assetFund);
		}

		/// <summary>
		/// Takes a decorated object and creates an Asset Fund.
		/// </summary>
		/// <param name="decoratedAssetFund">Decorated asset fund.</param>
		/// <returns></returns>
		public static AssetFund FromDecoratedObject(AssetFundDecorator decoratedAssetFund)
		{
			return decoratedAssetFund.AssetFund;
		}

		/// <summary>
		/// Takes a decorated list of objects and returns an AssetFundCollection.
		/// </summary>
		/// <param name="decoratedAssetFunds">Decorated asset funds.</param>
		/// <returns></returns>
		public static AssetFundCollection FromDecoratedList(IList decoratedAssetFunds)
		{
			AssetFundCollection returnAssetFundCollection = new AssetFundCollection();
			foreach (AssetFundDecorator decoratedAssetFund in decoratedAssetFunds)
			{
				returnAssetFundCollection.Add(FromDecoratedObject(decoratedAssetFund));
			}

			return returnAssetFundCollection;
		}

		/// <summary>
		/// Toes the decorated list.
		/// </summary>
		/// <param name="assetFundCollection">Asset fund collection.</param>
		/// <returns></returns>
		public static IList ToDecoratedList(IList assetFundCollection)
		{
			ArrayList returnList = new ArrayList();
			foreach (AssetFund assetFund in assetFundCollection)
			{
				returnList.Add(ToDecoratedObject(assetFund));
			}
			return returnList;
		}


		/// <summary>
		/// Creates a new <see cref="AssetFundDecorator"/> instance.
		/// </summary>
		/// <param name="assetFund">Asset fund.</param>
		protected AssetFundDecorator(AssetFund assetFund)
		{
			this.AssetFund=assetFund;
			m_FullName = assetFund.FullName;
			m_IsDirty=assetFund.IsDirty;
			this.m_IsNew=assetFund.IsNew;
			m_WithinAssetMovementTolerance=assetFund.WithinAssetMovementTolerance;
			this.m_AssetFundType=assetFund.AssetFundType;
			this.m_AssetFundTypeString=assetFund.AssetFundType.ToString();
			this.m_AssetMovementToleranceDisplay= DisplayFormat.Percent(assetFund.AssetMovementTolerance, assetFund.AssetMovementToleranceSet);
			this.m_AssetMovementToleranceDisplay2=DisplayFormat.Percent(assetFund.AssetMovementTolerance, true);
			this.m_UnitPriceDisplay=DisplayFormat.Decimal(assetFund.UnitPrice, assetFund.UnitPriceSet, "N2" );
			this.m_AssetMovementConstituents=AssetMovementConstituentDecorator.FromConstituentListToDecorated(assetFund.AssetMovementConstituents);
			this.m_AssetMovementVarianceDisplay=DisplayFormat.Percent(assetFund.AssetMovementVariance, assetFund.AssetMovementVarianceSet);
			this.m_UnitPriceMovementDisplay=DisplayFormat.Percent(assetFund.UnitPriceMovement, assetFund.UnitPriceMovementSet);
			this.m_PredictedAssetMovementDisplay=DisplayFormat.Percent(assetFund.PredictedAssetMovement, assetFund.PredictedAssetMovementSet);
			this.m_WithinAssetMovementToleranceDisplay=m_WithinAssetMovementTolerance?"Y":"N";
		}

		/// <summary>
		/// Creates a new <see cref="AssetFundDecorator"/> instance.
		/// </summary>
		public AssetFundDecorator()
		{
		}

		#region Display Properties (re-introduced due to grid limitations. TODO - try to resolve and bring back property formatter)

		/// <summary>
		/// Gets the full name.
		/// </summary>
		/// <value></value>
		public string FullName
		{
			get { return m_FullName; }
			set {  m_FullName=value; }
		}

		/// <summary>
		/// Gets the full name.
		/// </summary>
		/// <value></value>
		public AssetFund.AssetFundTypeEnum AssetFundType
		{
			get { return m_AssetFundType; }
			set {  m_AssetFundType=value; }
		}

		/// <summary>
		/// Gets the full name.
		/// </summary>
		/// <value></value>
		public string AssetFundTypeString
		{
			get { return m_AssetFundTypeString; }
			set {  m_AssetFundTypeString=value; }
		}


		/// <summary>
		/// Gets a value indicating whether this instance is dirty.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is dirty; otherwise, <c>false</c>.
		/// </value>
		public bool IsDirty
		{
			get { return m_IsDirty; }
			set {  m_IsDirty=value; }
		}

		/// <summary>
		/// Gets a value indicating whether this instance is new.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is new; otherwise, <c>false</c>.
		/// </value>
		public bool IsNew
		{
			get { return m_IsNew; }
			set {  m_IsNew=value; }
		}

		/// <summary>
		/// Gets the full name.
		/// </summary>
		/// <value></value>
		public bool WithinAssetMovementTolerance
		{
			get { return m_WithinAssetMovementTolerance; }
			set {  m_WithinAssetMovementTolerance=value; }
		}

		/// <summary>
		/// This property is here for displaying in the Current Asset Fund Status grid 
		/// </summary>
		public string WithinAssetMovementToleranceDisplay
		{
			get{return m_WithinAssetMovementToleranceDisplay;}
			set {  m_WithinAssetMovementToleranceDisplay=value; }
		}

		/// <summary>
		/// Read only property that returns the AssetMovementTolerance Display string
		/// </summary>
		public string AssetMovementToleranceDisplay
		{
			get { return m_AssetMovementToleranceDisplay; }
			set {  m_AssetMovementToleranceDisplay=value; }
		}

		/// <summary>
		/// as above but displays 0 as opposed to unavailable
		/// </summary>
		public string AssetMovementToleranceDisplay2
		{
			get { return m_AssetMovementToleranceDisplay2; }
			set {  m_AssetMovementToleranceDisplay2=value; }
		}

		/// <summary>
		/// Read only property that returns the Unit Price Display string
		/// </summary>
		public string UnitPriceDisplay
		{
			get { return m_UnitPriceDisplay; }
			set {  m_UnitPriceDisplay=value; }
		}


		/// <summary>
		/// Read only property that returns the Unit Price Movement Display string
		/// </summary>
		public string UnitPriceMovementDisplay
		{
			get { return m_UnitPriceMovementDisplay; }
			set {  m_UnitPriceMovementDisplay=value; }
		}

		/// <summary>
		/// Read only property that returns the Predicted Asset Movement Display string
		/// </summary>
		public virtual string PredictedAssetMovementDisplay
		{
			//return DisplayFormat.Percent(PredictedAssetMovement, AreAllLinkedFundsAuthorised() && PredictedAssetMovementSet);
			get { return m_PredictedAssetMovementDisplay; }
			set {  m_PredictedAssetMovementDisplay=value; }
		}

		/// <summary>
		/// Read only property that returns the Asset Movement Variance Display string
		/// </summary>
		public string AssetMovementVarianceDisplay
		{
			get { return m_AssetMovementVarianceDisplay; }
			set {  m_AssetMovementVarianceDisplay=value; }
		}

		/// <summary>
		/// Gets a decorated asset movement constituents collection.
		/// </summary>
		/// <value></value>
		public IList AssetMovementConstituents
		{
			get { return m_AssetMovementConstituents; }
			set {  m_AssetMovementConstituents=value; }
		}

		/// <summary>
		/// Gets or sets the asset fund.
		/// </summary>
		/// <value></value>
		public AssetFund AssetFund
		{
			get { return assetFund; }
			set { assetFund = value; }
		}

		#endregion
	}

	/// <summary>
	/// 
	/// </summary>
	public class AssetMovementConstituentDecorator
	{

		/// <summary>
		/// Override the Equals as we are only worried if the indexname is the same and not the actual objects
		/// </summary>
		/// <param name="obj"></param>
		/// <returns>true is they have the same index name</returns>
		public override bool Equals(object obj)
		{
			if (obj is AssetMovementConstituentDecorator)
			{
				AssetMovementConstituentDecorator dec =  obj as AssetMovementConstituentDecorator;
				return (this==dec);
			}
			else
			{
				return base.Equals(obj);
			}
		}

		/// <summary>
		/// Overloaded equality operator
		/// </summary>
		/// <param name="lhs">First Object to compare</param>
		/// <param name="rhs">Second Object to compare</param>
		/// <returns></returns>
		public static bool operator==(AssetMovementConstituentDecorator lhs,AssetMovementConstituentDecorator rhs)
		{
			if ((object)lhs !=null && (object)rhs!=null )
			{
				return (lhs.m_constitue==rhs.m_constitue);
			}
			else
			{
				return (object)lhs==(object)rhs;
			}
		}

		/// <summary>
		/// Overloaded inequality operator
		/// </summary>
		/// <param name="lhs">First Object to compare</param>
		/// <param name="rhs">Second Object to compare</param>
		/// <returns></returns>
		public static bool operator!=(AssetMovementConstituentDecorator lhs,AssetMovementConstituentDecorator rhs)
		{
			return !(lhs==rhs);
		}


		/// <summary>
		/// Gets the hash code. Overridden to call the base implementation (????)
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>
		/// Builds the decorated constituent list.
		/// </summary>
		/// <param name="constitutes">Constitutes.</param>
		/// <returns></returns>
		internal static IList FromConstituentListToDecorated(AssetMovementConstituentCollection constitutes)
		{
			ArrayList completeList = new ArrayList();

			foreach (AssetMovementConstituent constitute in constitutes)
			{
				completeList.Add(FromConstituentToDecoratedConstituent(constitute));
			}
			return completeList;
		}

		internal static AssetMovementConstituentDecorator FromConstituentToDecoratedConstituent(AssetMovementConstituent constituent)
		{
			return new AssetMovementConstituentDecorator(constituent);
		}

		internal static AssetMovementConstituent FromDecoratedConstituentToConstituent(AssetMovementConstituentDecorator decoratedConstituent)
		{
			return decoratedConstituent.m_constitue;
		}

		internal static IList FromDecoratedToConstituentList(IList decoratedList)
		{
			AssetMovementConstituentCollection amcc = new AssetMovementConstituentCollection();

			foreach (AssetMovementConstituentDecorator docoratedConstituent in decoratedList)
			{
				amcc.Add(FromDecoratedConstituentToConstituent(docoratedConstituent));
			}
			return amcc;
		}

		private readonly AssetMovementConstituent m_constitue;
		private string m_BenchMarkName;
		private string m_CurrencyCode;
		private decimal m_Proportion;
		private string m_BenchMarkType;
		private string m_BenchMarkSubType;
		private bool m_IsDirty;
		private bool m_IsNew;
		private IBenchMark m_BenchMark;
		private string m_ProportionDisplay;

		/// <summary>
		/// Creates a new <see cref="AssetMovementConstituentDecorator"/> instance.
		/// </summary>
		public AssetMovementConstituentDecorator()
		{
		}

		/// <summary>
		/// Creates a new <see cref="AssetMovementConstituentDecorator"/> instance.
		/// </summary>
		/// <param name="constituent">Constituent.</param>
		protected AssetMovementConstituentDecorator(AssetMovementConstituent constituent)
		{
			this.m_constitue = constituent;
			IsNew=constituent.IsNew;
			IsDirty=constituent.IsDirty;

			this.m_BenchMarkSubType=m_constitue.BenchMark.BenchMarkSubType;

			this.m_BenchMarkType=m_constitue.BenchMark.BenchMarkType;

			//default
			m_BenchMarkName = m_constitue.BenchMark.ToString();

			CurrencyCode ="";
			if (m_constitue.BenchMark != null && m_constitue.BenchMark.Currency != null
				&& m_constitue.BenchMark.Currency.CurrencyCode != "")
			{
				CurrencyCode = m_constitue.BenchMark.Currency.CurrencyCode;
			}

			string countryCode="";

			if (m_constitue.BenchMark is Fund)
			{
				m_BenchMarkName = ((Fund) m_constitue.BenchMark).ShortName;
			}
			else if (m_constitue.BenchMark is StockMarketIndex)
			{
				m_BenchMarkName = ((StockMarketIndex) m_constitue.BenchMark).MarketName;
				countryCode= ((StockMarketIndex) m_constitue.BenchMark).CountryCode;
				if (countryCode!="" )
				{
					countryCode=countryCode==""?"":countryCode+",";
				}
			}
			
		

			if (CurrencyCode!="" || countryCode != "")
			{
				m_BenchMarkName += string.Concat(" (", countryCode,CurrencyCode, ")");
			}
			m_Proportion=constituent.Proportion;
			
			setProportionDisplay();

			BenchMark= m_constitue.BenchMark ;
		}

		private void setProportionDisplay()
		{
			decimal percentValue=Proportion*100;
			m_ProportionDisplay= percentValue.ToString("N4");
		}

		
		/// <summary>
		/// Gets the proportion display.
		/// </summary>
		/// <value></value>
		public string ProportionDisplay
		{
			get{return m_ProportionDisplay;}
		}

		/// <summary>
		/// Gets or sets the name of the benchmark display.
		/// </summary>
		/// <value></value>
		public string BenchmarkDisplayName
		{
			get{return m_BenchMarkName;}
			set{ m_BenchMarkName=value;}
		}

		/// <summary>
		/// Gets or sets the currency code.
		/// </summary>
		/// <value></value>
		public string CurrencyCode
		{
			get{return m_CurrencyCode;}
			set{m_CurrencyCode=value;}
		}

		/// <summary>
		/// Gets or sets the proportion.
		/// </summary>
		/// <value></value>
		public decimal Proportion
		{
			get { return m_Proportion; }
			set
			{
				this.m_constitue.Proportion = value;
				this.m_Proportion=value;
				setProportionDisplay();
			}
		}

		/// <summary>
		/// Gets or sets the bench mark type.
		/// </summary>
		/// <value></value>
		public string BenchMarkType
		{
			get { return m_BenchMarkType; }
			set {  m_BenchMarkType= value; }
		}

		/// <summary>
		/// Gets or sets the bench mark sub type.
		/// </summary>
		/// <value></value>
		public string BenchMarkSubType
		{
			get { return m_BenchMarkSubType; }
			set {  m_BenchMarkSubType= value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is dirty.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is dirty; otherwise, <c>false</c>.
		/// </value>
		public bool IsDirty
		{
			get { return m_IsDirty; }
			set { m_IsDirty = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is new.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is new; otherwise, <c>false</c>.
		/// </value>
		public bool IsNew
		{
			get { return m_IsNew; }
			set { m_IsNew = value; }
		}

		
		/// <summary>
		/// Gets or sets the bench mark.
		/// </summary>
		/// <value></value>
		public IBenchMark BenchMark
		{
			get { return m_BenchMark; }
			set { m_BenchMark = value; }
		}

	}

	/// <summary>
	/// This class is mainly used for display reasons in the fund static
	/// data exports
	/// </summary>
	public class FundStaticDataExportFundDecorator : FundDecorator
	{
		/// <summary>
		/// Creates a decorated object from an Fund.
		/// </summary>
		/// <param name="fund">Fund.</param>
		/// <returns></returns>
		public new static FundDecorator ToDecoratedObject(Fund fund)
		{
			return new FundStaticDataExportFundDecorator(fund);
		}

		/// <summary>
		/// From a regular to a decorated list.
		/// </summary>
		/// <param name="fundCollection">Fund collection.</param>
		/// <returns></returns>
		public new static IList ToDecoratedList(IList fundCollection)
		{
			IList returnList = new ArrayList();
			foreach (Fund fund in fundCollection)
			{
				returnList.Add(ToDecoratedObject(fund));
			}

			return returnList;
		}

		private string m_UseMidPriceAsBidPriceDisplay;
		private string m_IsExDividendDisplay;
		private string m_RevaluationEndDateDisplay;
		private string m_RevaluationEffectiveDateDisplay;
		private string m_RevaluationFactorDisplay2;
		private string m_TPEDisplay2;
		private string m_ScalingFactorDisplay;
		private string m_IsDualPriceDisplay;
		private string m_IsLifeDisplay;
		private const string NOT_AVAILABLETEXT = "n/a";

		private FundStaticDataExportFundDecorator(Fund fund) : base(fund)
		{
			if (fund is OEICFund)
			{
				OEICFund oeicFund = ((OEICFund) fund);
				m_UseMidPriceAsBidPriceDisplay = NOT_AVAILABLETEXT;
				m_IsExDividendDisplay = oeicFund.IsExDividend ? "Y" : "N";
				m_RevaluationEndDateDisplay = NOT_AVAILABLETEXT;
				m_RevaluationEffectiveDateDisplay = NOT_AVAILABLETEXT;
				m_RevaluationFactorDisplay2 = NOT_AVAILABLETEXT;
				m_TPEDisplay2 = NOT_AVAILABLETEXT;
				m_ScalingFactorDisplay = NOT_AVAILABLETEXT;
				m_IsDualPriceDisplay = NOT_AVAILABLETEXT;
				m_IsLifeDisplay = NOT_AVAILABLETEXT;
			}
			else
			{
				NonOEIC nonOEIC = ((NonOEIC) fund);
				m_UseMidPriceAsBidPriceDisplay = nonOEIC.UseMidPriceAsBidPrice ? "Y" : "N";
				m_IsExDividendDisplay = NOT_AVAILABLETEXT;
				m_RevaluationEndDateDisplay = DisplayFormat.ShortDate(nonOEIC.RevaluationEndDate, (nonOEIC.RevaluationEndDateSet));
				m_RevaluationEffectiveDateDisplay = DisplayFormat.ShortDate(nonOEIC.RevaluationEffectiveDate, (nonOEIC.RevaluationEffectiveDateSet));
				m_RevaluationFactorDisplay2 = DisplayFormat.Percent(nonOEIC.RevalFactor, true);
				m_TPEDisplay2 = DisplayFormat.Percent(nonOEIC.TPE, true);
				m_ScalingFactorDisplay = DisplayFormat.Percent(nonOEIC.ScaleFactor, true);
				m_IsDualPriceDisplay = nonOEIC.IsDualPrice ? "Y" : "N";
				m_IsLifeDisplay = nonOEIC.IsLife ? "Y" : "N";
			}
		}


		/// <summary>
		/// Displays a Y or N depending on whether the fund is a life fund
		/// </summary>
		public string UseMidPriceAsBidPriceDisplay
		{
			get { return m_UseMidPriceAsBidPriceDisplay; }
		}

		/// <summary>
		/// returns 'Y' or 'N'
		/// </summary>
		public string IsExDividendDisplay
		{
			get { return m_IsExDividendDisplay; }
		}


		/// <summary>
		/// The formatted field used to display the Revaluation Factor End Date.
		/// </summary>
		public string RevaluationEndDateDisplay
		{
			get { return m_RevaluationEndDateDisplay; }
		}

		/// <summary>
		/// The formatted field used to display the Revaluation Factor Effective Date.
		/// </summary>
		public string RevaluationEffectiveDateDisplay
		{
			get { return m_RevaluationEffectiveDateDisplay; }
		}

		/// <summary>
		/// as above but displays 0 as opposed to unavailable
		/// </summary>
		public string RevaluationFactorDisplay2
		{
			get { return m_RevaluationFactorDisplay2; }
		}

		/// <summary>
		/// as above but displays 0 as opposed to unavailable
		/// </summary>
		public string TPEDisplay2
		{
			get { return m_TPEDisplay2; }
		}

		/// <summary>
		/// The formatted field used to display the Scaling factor
		/// </summary>
		public override string ScalingFactorDisplay
		{ //display a 0 instead of 'unavailable'
			get { return m_ScalingFactorDisplay; }
		}

		/// <summary>
		/// Displays a Y or N 
		/// </summary>
		public string IsDualPriceDisplay
		{
			get { return m_IsDualPriceDisplay; }
		}

		/// <summary>
		/// Displays a Y or N 
		/// </summary>
		public string IsLifeDisplay
		{
			get { return m_IsLifeDisplay; }
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class CurrentFundStatusFundDecorator
	{
		private bool m_UsePredictedPrice;
		private string m_AssetMovementToleranceDisplay;
		private bool m_ProgressStatus;
		private string m_FullName;
		private Fund m_Fund;
		private string m_PriceDisplay;
		private string m_PriceMovementPercentDisplay;
		private string m_PredictedPriceDisplay;
		private string m_PredictedPriceMovementPercentDisplay;
		private string m_PriceMovementVarianceDisplay;
		private string m_PriceMovementRoundedToleranceDisplay;
		private string m_AssetUnitPriceDisplay;
		private string m_PredictedAssetMovementDisplay;
		private string m_AssetMovementDisplay;
		private string m_AssetMovementVarianceDisplay;
		private string m_WithinAssetMovementToleranceDisplay;
		private string m_FundStatusDisplay;
		private DateTime m_StatusChangedTime;
		private bool m_SecondLevelButNotFirst;
		private bool m_WithinAssetMovementTolerance;
		private Fund.FundStatusType m_FundStatus;
		private bool m_PriceOutsideTolerance;
		private bool m_IsDirty;
		private bool m_IsNew;


		/// <summary>
		/// 
		/// </summary>
		public CurrentFundStatusFundDecorator()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		public CurrentFundStatusFundDecorator(Fund fund)
		{
			Fund=fund;
			WithinAssetMovementTolerance = fund.WithinAssetMovementTolerance;
			SecondLevelButNotFirst = (fund.FundStatus == Fund.FundStatusType.SecondLevelAuthorised && fund.WithinAssetMovementTolerance && !fund.PriceOutsideTolerance);
			AssetMovementDisplay = DisplayFormat.Percent(fund.AssetMovement, fund.AssetMovementSet);
			m_UsePredictedPrice=fund.UsePredictedPrice;
			AssetMovementToleranceDisplay = fund.WithinAssetMovementTolerance ? "Yes" : "No";
			ProgressStatus=fund.ProgressStatus;
			PriceOutsideTolerance = fund.PriceOutsideTolerance;
			m_FullName=fund.FullName;
			PriceDisplay = DisplayFormat.Decimal(fund.Price, fund.PriceSet, "#,##0.00");
			decimal predPrice = fund.PredictedPrice;
			PredictedPriceDisplay = DisplayFormat.Decimal(predPrice, predPrice > 0M, "#,##0.00");
			PriceMovementPercentDisplay = DisplayFormat.Percent(fund.PriceMovementPercent, (fund.PriceSet && fund.PreviousPriceSet));
			PredictedPriceMovementPercentDisplay = DisplayFormat.Percent(fund.PredictedPriceMovementPercent, (fund.PreviousPriceSet));
			bool varianceSet = fund.PriceSet && fund.PreviousPriceSet;
			PriceMovementVarianceDisplay = DisplayFormat.Percent(fund.PriceMovementVariance, varianceSet);
			PriceMovementRoundedToleranceDisplay = buildPriceMovementRoundedToleranceDisplay(fund);
			AssetUnitPriceDisplay = DisplayFormat.Decimal(fund.AssetUnitPrice, fund.AssetUnitPriceSet, "N2");
			PredictedAssetMovementDisplay = DisplayFormat.Percent(fund.PredictedAssetMovement, fund.PredictedAssetMovementSet);
			AssetMovementVarianceDisplay = DisplayFormat.Percent(fund.AssetMovementVariance, fund.AssetMovementVarianceSet);
			WithinAssetMovementToleranceDisplay = fund.WithinAssetMovementTolerance ? "Y" : "N";
			FundStatusDisplay = FundDecorator.TranslatePascalCasedString(fund.FundStatus.ToString());
			StatusChangedTime = fund.StatusChangedTime;
			FundStatus = fund.FundStatus;
		}

		/// <summary>
		/// 
		/// </summary>
		public bool PriceOutsideTolerance
		{
			get{return m_PriceOutsideTolerance;}
			set { m_PriceOutsideTolerance=value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public Fund.FundStatusType FundStatus
		{
			get{return m_FundStatus;}
			set { m_FundStatus=value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is dirty.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is dirty; otherwise, <c>false</c>.
		/// </value>
		public bool IsDirty
		{
			get { return m_IsDirty; }
			set { m_IsDirty = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is new.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is new; otherwise, <c>false</c>.
		/// </value>
		public bool IsNew
		{
			get { return m_IsNew; }
			set { m_IsNew = value; }
		}


		/// <summary>
		/// 
		/// </summary>
		public bool SecondLevelButNotFirst
		{
			get{return m_SecondLevelButNotFirst;}
			set { m_SecondLevelButNotFirst=value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public bool WithinAssetMovementTolerance
		{
			get{return m_WithinAssetMovementTolerance;}
			set { m_WithinAssetMovementTolerance=value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public bool ProgressStatus
		{
			get
			{
				return m_ProgressStatus;
			}
			set
			{
				m_ProgressStatus=value;
			}
		}

		private string buildPriceMovementRoundedToleranceDisplay(Fund fund)
		{
			StringBuilder builder = new StringBuilder();

			if (fund.WithinRoundedPriceLowerTolerance && fund.WithinRoundedPriceUpperTolerance
				&& fund.WithinPriceDirectionTolerance)
			{
				builder.Append("Y");
			}
			else
			{
				builder.Append("UT - ");
				builder.Append(fund.WithinRoundedPriceUpperTolerance ? "Y" : "N");
				builder.Append("  LT - ");
				builder.Append(fund.WithinRoundedPriceLowerTolerance ? "Y" : "N");
				builder.Append("  PI - ");
				builder.Append(fund.WithinPriceDirectionTolerance ? "Y" : "N");
			}

			return builder.ToString();
		}

		/// <summary>
		/// From a regular to a decorated list.
		/// </summary>
		/// <param name="fundCollection">Fund collection.</param>
		/// <returns></returns>
		public static IList ToDecoratedList(IList fundCollection)
		{
			IList returnList = new ArrayList();
			foreach (Fund fund in fundCollection)
			{
				returnList.Add(ToDecoratedObject(fund));
			}

			return returnList;
		}

		/// <summary>
		/// Creates a decorated object from an Fund.
		/// </summary>
		/// <param name="fund">Fund.</param>
		/// <returns></returns>
		public static CurrentFundStatusFundDecorator ToDecoratedObject(Fund fund)
		{
			return new CurrentFundStatusFundDecorator(fund);
		}

		/// <summary>
		/// Gets or sets the full name.
		/// </summary>
		/// <value></value>
		public string FullName
		{
			get { return m_FullName; }
			set { m_FullName = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string PredictedPriceDisplay{get { return m_PredictedPriceDisplay; }
			set
			{ m_PredictedPriceDisplay=value; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string PriceMovementPercentDisplay{get { return m_PriceMovementPercentDisplay; }
			set
			{ m_PriceMovementPercentDisplay=value; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string PredictedPriceMovementPercentDisplay{get { return m_PredictedPriceMovementPercentDisplay; }
			set
			{ m_PredictedPriceMovementPercentDisplay=value; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string PriceMovementVarianceDisplay{get { return m_PriceMovementVarianceDisplay; }
			set
			{ m_PriceMovementVarianceDisplay=value; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string PriceMovementRoundedToleranceDisplay{get { return m_PriceMovementRoundedToleranceDisplay; }
			set
			{ m_PriceMovementRoundedToleranceDisplay=value; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string AssetUnitPriceDisplay{get { return m_AssetUnitPriceDisplay; }
			set
			{ m_AssetUnitPriceDisplay=value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string PredictedAssetMovementDisplay{get { return m_PredictedAssetMovementDisplay; }
			set
			{ m_PredictedAssetMovementDisplay=value; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string AssetMovementVarianceDisplay{get { return m_AssetMovementVarianceDisplay; }
			set
			{ m_AssetMovementVarianceDisplay=value; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string WithinAssetMovementToleranceDisplay{get { return m_WithinAssetMovementToleranceDisplay; }
			set
			{ m_WithinAssetMovementToleranceDisplay=value; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string FundStatusDisplay{get { return m_FundStatusDisplay; }
			set
			{ m_FundStatusDisplay=value; }
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime StatusChangedTime{get { return m_StatusChangedTime; }
			set
			{ m_StatusChangedTime=value; }
		}
/// <summary>
/// 
/// </summary>
		public Fund Fund
		{
			get { return m_Fund; }
			set { m_Fund = value; }
		}
/// <summary>
/// 
/// </summary>
		public string PriceDisplay
		{
			get { return m_PriceDisplay; }
			set { m_PriceDisplay = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string AssetMovementToleranceDisplay
		{
			get { return m_AssetMovementToleranceDisplay; }
			set { m_AssetMovementToleranceDisplay = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public bool UsePredictedPrice
		{
			get { return m_UsePredictedPrice; }
			set { m_UsePredictedPrice = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string AssetMovementDisplay
		{
			get { return m_AssetMovementDisplay; }
			set { m_AssetMovementDisplay = value; }
		}

		/// <summary>
		/// Takes a decorated list of objects and returns an FundCollection.
		/// </summary>
		/// <param name="decoratedFunds">Decorated funds.</param>
		/// <returns></returns>
		public static FundCollection FromDecoratedList(IList decoratedFunds)
		{
			FundCollection returnFundCollection = new FundCollection();
			foreach (CurrentFundStatusFundDecorator decoratedFund in decoratedFunds)
			{
				returnFundCollection.Add(FromDecoratedObject(decoratedFund));
			}

			return returnFundCollection;
		}

		
		/// <summary>
		/// Takes a decorated object and creates an Fund.
		/// </summary>
		/// <param name="decoratedFund">Decorated fund.</param>
		/// <returns></returns>
		public static Fund FromDecoratedObject(CurrentFundStatusFundDecorator decoratedFund)
		{
			return decoratedFund.Fund;
		}
	}


	/// <summary>
	/// This class can be used when display formating is required
	/// </summary>
	public class FundDecorator
	{
		/// <summary>
		/// Creates a decorated object from an Fund.
		/// </summary>
		/// <param name="fund">Fund.</param>
		/// <returns></returns>
		public static FundDecorator ToDecoratedObject(Fund fund)
		{
			return new FundDecorator(fund);
		}

		/// <summary>
		/// Takes a decorated object and creates an Fund.
		/// </summary>
		/// <param name="decoratedFund">Decorated fund.</param>
		/// <returns></returns>
		public static Fund FromDecoratedObject(FundDecorator decoratedFund)
		{
			return decoratedFund.Fund;
		}

		/// <summary>
		/// Takes a decorated list of objects and returns an FundCollection.
		/// </summary>
		/// <param name="decoratedFunds">Decorated funds.</param>
		/// <returns></returns>
		public static FundCollection FromDecoratedList(IList decoratedFunds)
		{
			FundCollection returnFundCollection = new FundCollection();
			foreach (FundDecorator decoratedFund in decoratedFunds)
			{
				returnFundCollection.Add(FromDecoratedObject(decoratedFund));
			}

			return returnFundCollection;
		}

		/// <summary>
		/// From a regular to a decorated list.
		/// </summary>
		/// <param name="fundCollection">Fund collection.</param>
		/// <returns></returns>
		public static IList ToDecoratedList(IList fundCollection)
		{
			IList returnList = new ArrayList();
			foreach (Fund fund in fundCollection)
			{
				returnList.Add(ToDecoratedObject(fund));
			}

			return returnList;
		}

		private Fund m_fund;
		private string m_PriceDisplay;
		private string m_PreviousPriceDisplay;
		private string m_fundStatus;
		private string m_PredictedPriceDisplay;
		private string m_PriceMovementPercentDisplay;
		private string m_PredictedPriceMovementPercentDisplay;
		private string m_PriceMovementVarianceDisplay;
		private string m_AssetUnitPriceDisplay;
		private string m_AssetMovementDisplay;
		private string m_AssetMovementDisplay2;
		private string m_PredictedAssetMovementDisplay;
		private string m_PredictedAssetMovementDisplay2;
		private string m_AssetMovementVarianceDisplay;
		private string m_WithinAssetMovementToleranceDisplay;
		private string m_AssetMovementToleranceDisplay;
		private string m_AssetMovementToleranceDisplay2;
		private string m_UpperToleranceDisplay;
		private string m_UpperToleranceDisplay2;
		private string m_FullName;
		private decimal m_AssetMovementTolerance;
		private bool m_IsNew;
		private bool m_IsDirty;
		private decimal m_UpperTolerance;
		private string m_PropertiesHeader;
		private string m_AssetFundId;
		private IList m_Factors;
		private decimal m_LowerTolerance;
		private bool m_PriceIncreaseOnly;
		private string m_PriceMovementToleranceDisplay;
		private string m_PriceMovementRoundedToleranceDisplay;
		private string m_PriceMovementLowerToleranceDisplay;
		private string m_PriceMovementUpperToleranceDisplay;
		private string m_PriceMovementDirectionToleranceDisplay;
		private string m_IsAuthorisedDisplay;
		private string m_XFactorDisplay;
		private string m_XFactorDisplay2;
		private string m_OnHiPortfolio3Display;
		private string m_IsBenchMarkableDisplay;
		private DateTime m_StatusChangedTime;
		private Fund.FundStatusType m_FundStatus;
		private bool m_WithinAssetMovementTolerance;
		private bool m_PriceOutsideTolerance;
		private bool m_SecondLevelButNotFirst;
		private bool m_UsePredictedPrice;
		private bool m_ProgressStatus;
		private string m_revaluationFactorDisplay;
		private string m_scalingFactorDisplay;
		private string m_valuationBasisDisplay;
		private string m_taxProvisionDisplay;

		/// <summary>
		/// Gets or sets the fund.
		/// </summary>
		/// <value></value>
		public Fund Fund
		{
			get { return m_fund; }
			set { m_fund = value; }
		}

		/// <summary>
		/// Creates a new <see cref="FundDecorator"/> instance.
		/// Needed for the grid to re-hydrate this object
		/// </summary>
		public FundDecorator()
		{
		}

		/// <summary>
		/// Creates a new <see cref="FundDecorator"/> instance.
		/// </summary>
		/// <param name="fund">Fund.</param>
		protected FundDecorator(Fund fund)
		{
			UsePredictedPrice = fund.UsePredictedPrice;
			ProgressStatus = fund.ProgressStatus;
			SecondLevelButNotFirst = (fund.FundStatus == Fund.FundStatusType.SecondLevelAuthorised && fund.WithinAssetMovementTolerance && !fund.PriceOutsideTolerance);
			PriceOutsideTolerance = fund.PriceOutsideTolerance;
			StatusChangedTime = fund.StatusChangedTime;
			IsBenchMarkableDisplay = fund.IsBenchMarkable ? "Y" : "N";
			OnHiPortfolio3Display = fund.OnHiPortfolio3 ? "Y" : "N";
			XFactorDisplay = DisplayFormat.Percent(fund.XFactor, true); //display a zero as opposed to 'unavailable'
			RevaluationFactorDisplay = DisplayFormat.Decimal(fund.Factors.RevaluationFctr.effectToday, true, "0.0000");
			ScalingFactorDisplay = DisplayFormat.Decimal(fund.Factors.ScalingFctr.effectToday, true, "0.0000");
			ValuationBasisDisplay = DisplayFormat.Decimal(fund.Factors.ValuationBasisFctr.effectToday, true, "0.0000");
			XFactorDisplay2 = DisplayFormat.Decimal(fund.XFactor, true, "0.0000");
			TaxProvisionEstimateDisplay = DisplayFormat.Decimal(fund.Factors.TPE.effectToday, true, "0.0000");
			IsAuthorisedDisplay = fund.IsAuthorised ? "Y" : "N";
			PriceMovementDirectionToleranceDisplay = fund.WithinPriceDirectionTolerance ? "Y" : "N";
			PriceMovementUpperToleranceDisplay = fund.WithinPriceUpperTolerance ? "Y" : "N";
			PriceMovementLowerToleranceDisplay = fund.WithinPriceLowerTolerance ? "Y" : "N";
			PriceMovementRoundedToleranceDisplay = buildPriceMovementRoundedToleranceDisplay(fund);
			PriceMovementToleranceDisplay = buildPriceMovementToleranceDisplay(fund);
			LowerTolerance = fund.LowerTolerance;
			Factors = fund.Factors;
			UpperTolerance = fund.UpperTolerance;
			IsNew = fund.IsNew;
			IsDirty = fund.IsDirty;
			Fund = fund;
			PriceIncreaseOnly = fund.PriceIncreaseOnly;
			FullName = fund.FullName;
			AssetFundID = fund.ParentAssetFundID;
			PropertiesHeader = FullName.Trim() + " Properties";
			WithinAssetMovementToleranceDisplay = fund.WithinAssetMovementTolerance ? "Y" : "N";
			WithinAssetMovementTolerance = fund.WithinAssetMovementTolerance;
			AssetMovementTolerance = fund.AssetMovementTolerance;
			AssetMovementToleranceDisplay = fund.WithinAssetMovementTolerance ? "Yes" : "No";
			AssetMovementToleranceDisplay2 = DisplayFormat.Percent(fund.AssetMovementTolerance, fund.AssetMovementTolerance > 0);
			UpperToleranceDisplay = DisplayFormat.Percent(fund.UpperTolerance, fund.UpperTolerance > 0);
			UpperToleranceDisplay2 = DisplayFormat.Percent(fund.UpperTolerance, true);
			AssetMovementVarianceDisplay = DisplayFormat.Percent(fund.AssetMovementVariance, fund.AssetMovementVarianceSet);
			PriceDisplay = DisplayFormat.Decimal(fund.Price, fund.PriceSet, "#,##0.00");
			PreviousPriceDisplay = DisplayFormat.Decimal(fund.PreviousPrice, fund.PreviousPriceSet, "#,##0.00");
			FundStatusDisplay = TranslatePascalCasedString(fund.FundStatus.ToString());
			FundStatus = fund.FundStatus;

			decimal predPrice = fund.PredictedPrice;
			PredictedPriceDisplay = DisplayFormat.Decimal(predPrice, predPrice > 0M, "#,##0.00");
			PriceMovementPercentDisplay = DisplayFormat.Percent(fund.PriceMovementPercent, (fund.PriceSet && fund.PreviousPriceSet));
			PredictedPriceMovementPercentDisplay = DisplayFormat.Percent(fund.PredictedPriceMovementPercent, (fund.PreviousPriceSet));

			bool varianceSet = fund.PriceSet && fund.PreviousPriceSet;
			PriceMovementVarianceDisplay = DisplayFormat.Percent(fund.PriceMovementVariance, varianceSet);
			AssetUnitPriceDisplay = DisplayFormat.Decimal(fund.AssetUnitPrice, fund.AssetUnitPriceSet, "N2");
			AssetMovementDisplay = DisplayFormat.Percent(fund.AssetMovement, fund.AssetMovementSet);
			AssetMovementDisplay2 = DisplayFormat.Percent(fund.AssetMovement, fund.AssetMovementSet);
			PredictedAssetMovementDisplay = DisplayFormat.Percent(fund.PredictedAssetMovement, fund.PredictedAssetMovementSet);
			PredictedAssetMovementDisplay2 = DisplayFormat.Decimal(fund.PredictedAssetMovement, fund.PredictedAssetMovementSet, "0.0000");
		}

		/// <summary>
		/// Translates the pascal cased string to a readable string with spaces.
		/// This is usefull when you'd like to show your enum names
		/// </summary>
		/// <param name="pascalCasedString">Pascal cased string.</param>
		/// <returns></returns>
		public static string TranslatePascalCasedString(string pascalCasedString)
		{
			//we should perhaps use the EnumDisplayTextAttribute class i.e		
			//[HBOS.FS.AMP.Windows.Controls.EnumDisplayTextAttribute( "Common" )]

			char[] chars = pascalCasedString.ToString().ToCharArray();
			StringBuilder returnString = new StringBuilder();
			for (int i = 0; i < chars.Length; i++)
			{
				if (i > 0 && Char.IsUpper(chars[i]))
				{
					returnString.Append(String.Concat(" ", chars[i]));
				}
				else
				{
					returnString.Append(chars[i]);
				}
			}

			return returnString.ToString();
		}

		private string buildPriceMovementRoundedToleranceDisplay(Fund fund)
		{
			StringBuilder builder = new StringBuilder();

			if (fund.WithinRoundedPriceLowerTolerance && fund.WithinRoundedPriceUpperTolerance
				&& fund.WithinPriceDirectionTolerance)
			{
				builder.Append("Y");
			}
			else
			{
				builder.Append("UT - ");
				builder.Append(fund.WithinRoundedPriceUpperTolerance ? "Y" : "N");
				builder.Append("  LT - ");
				builder.Append(fund.WithinRoundedPriceLowerTolerance ? "Y" : "N");
				builder.Append("  PI - ");
				builder.Append(fund.WithinPriceDirectionTolerance ? "Y" : "N");
			}

			return builder.ToString();
		}

		private string buildPriceMovementToleranceDisplay(Fund fund)
		{
			StringBuilder builder = new StringBuilder();

			if (fund.WithinPriceLowerTolerance && fund.WithinPriceUpperTolerance
				&& fund.WithinPriceDirectionTolerance)
			{
				builder.Append("Y");
			}
			else
			{
				builder.Append("UT - ");
				builder.Append(fund.WithinPriceUpperTolerance ? "Y" : "N");
				builder.Append("  LT - ");
				builder.Append(fund.WithinPriceLowerTolerance ? "Y" : "N");
				builder.Append("  PI - ");
				builder.Append(fund.WithinPriceDirectionTolerance ? "Y" : "N");
			}

			return builder.ToString();
		}

		/// <summary>
		/// Gets or sets a value indicating whether [progress status].
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [progress status]; otherwise, <c>false</c>.
		/// </value>
		public bool ProgressStatus
		{
			get { return m_ProgressStatus; }
			set { m_ProgressStatus = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether [use predicted price].
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [use predicted price]; otherwise, <c>false</c>.
		/// </value>
		public bool UsePredictedPrice
		{
			get { return m_UsePredictedPrice; }
			set { m_UsePredictedPrice = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether [second level but not first].
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [second level but not first]; otherwise, <c>false</c>.
		/// </value>
		public bool SecondLevelButNotFirst
		{
			get { return m_SecondLevelButNotFirst; }
			set { m_SecondLevelButNotFirst = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether [within asset movement tolerance].
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [within asset movement tolerance]; otherwise, <c>false</c>.
		/// </value>
		public bool WithinAssetMovementTolerance
		{
			get { return m_WithinAssetMovementTolerance; }
			set { m_WithinAssetMovementTolerance = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether [within asset movement tolerance].
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [within asset movement tolerance]; otherwise, <c>false</c>.
		/// </value>
		public bool PriceOutsideTolerance
		{
			get { return m_PriceOutsideTolerance; }
			set { m_PriceOutsideTolerance = value; }
		}


		/// <summary>
		/// Gets or sets the fund status.
		/// </summary>
		/// <value></value>
		public Fund.FundStatusType FundStatus
		{
			get { return m_FundStatus; }
			set { m_FundStatus = value; }
		}


		/// <summary>
		/// Gets or sets the status changed time.
		/// </summary>
		/// <value></value>
		public DateTime StatusChangedTime
		{
			get { return m_StatusChangedTime; }
			set { m_StatusChangedTime = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether [price increase only].
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [price increase only]; otherwise, <c>false</c>.
		/// </value>
		public bool PriceIncreaseOnly
		{
			get { return m_PriceIncreaseOnly; }
			set { m_PriceIncreaseOnly = value; }
		}

		/// <summary>
		/// Gets or sets the factors.
		/// </summary>
		/// <value></value>
		public IList Factors
		{
			get { return m_Factors; }
			set { m_Factors = value; }
		}

		/// <summary>
		/// Gets or sets the properties header.
		/// </summary>
		/// <value></value>
		public string PropertiesHeader
		{
			get { return m_PropertiesHeader; }
			set { m_PropertiesHeader = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is dirty.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is dirty; otherwise, <c>false</c>.
		/// </value>
		public bool IsDirty
		{
			get { return m_IsDirty; }
			set { m_IsDirty = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is new.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is new; otherwise, <c>false</c>.
		/// </value>
		public bool IsNew
		{
			get { return m_IsNew; }
			set { m_IsNew = value; }
		}

		/// <summary>
		/// The formatted field used to display the price.
		/// </summary>
		public string PriceDisplay
		{
			get { return m_PriceDisplay; }
			set { m_PriceDisplay = value; }
		}

		/// <summary>
		/// The formatted field used to display the previous price.
		/// </summary>
		public string PreviousPriceDisplay
		{
			get { return m_PreviousPriceDisplay; }
			set { m_PreviousPriceDisplay = value; }
		}

		/// <summary>
		/// The current fund status as a string
		/// </summary>
		public string FundStatusDisplay
		{
			get { return m_fundStatus; }
			set { m_fundStatus = value; }
		}

		/// <summary>
		/// The formatted field used to display the predicted price.
		/// </summary>
		public string PredictedPriceDisplay
		{
			get { return m_PredictedPriceDisplay; }
			set { m_PredictedPriceDisplay = value; }
		}


		/// <summary>
		/// The formatted field used to display the price movement.
		/// </summary>
		public string PriceMovementPercentDisplay
		{
			get { return m_PriceMovementPercentDisplay; }
			set { m_PriceMovementPercentDisplay = value; }
		}

		/// <summary>
		/// The formatted string to display the predicted price movement.
		/// </summary>
		public string PredictedPriceMovementPercentDisplay
		{
			get { return m_PredictedPriceMovementPercentDisplay; }
			set { m_PredictedPriceMovementPercentDisplay = value; }
		}

		/// <summary>
		/// A formatted string showing the price movement variance.
		/// </summary>
		public string PriceMovementVarianceDisplay
		{
			get { return m_PriceMovementVarianceDisplay; }
			set { m_PriceMovementVarianceDisplay = value; }
		}

		/// <summary>
		/// The formatted field used to display the asset unit price.
		/// </summary>
		public string AssetUnitPriceDisplay
		{
			get { return m_AssetUnitPriceDisplay; }
			set { m_AssetUnitPriceDisplay = value; }
		}

		/// <summary>
		/// The formatted field used to display the asset movement.
		/// </summary>
		public string AssetMovementDisplay
		{
			get { return m_AssetMovementDisplay; }
			set { m_AssetMovementDisplay = value; }
		}

		/// <summary>
		/// as above but displays 0 as opposed to unavailable
		/// </summary>
		public string AssetMovementDisplay2
		{
			get { return m_AssetMovementDisplay2; }
			set { m_AssetMovementDisplay2 = value; }
		}

		/// <summary>
		/// Formatted field used to display the predicted asset movement.
		/// </summary>
		public string PredictedAssetMovementDisplay
		{
			get { return m_PredictedAssetMovementDisplay; }
			set { m_PredictedAssetMovementDisplay = value; }
		}

		/// <summary>
		/// Formatted field used to display the predicted asset movement.
		/// </summary>
		public string PredictedAssetMovementDisplay2
		{
			get { return m_PredictedAssetMovementDisplay2; }
			set { m_PredictedAssetMovementDisplay2 = value; }
		}

		/// <summary>
		/// Formatted field used to display the revaluation factor
		/// </summary>
		public string RevaluationFactorDisplay
		{
			get { return m_revaluationFactorDisplay; }
			set { m_revaluationFactorDisplay = value; }
		}

		/// <summary>
		/// Formatted field used to display the scaling factor
		/// </summary>
		public virtual string ScalingFactorDisplay
		{
			get { return m_scalingFactorDisplay; }
			set { m_scalingFactorDisplay = value; }
		}

		/// <summary>
		/// Formatted field used to display the valuation basis
		/// </summary>
		public string ValuationBasisDisplay
		{
			get { return m_valuationBasisDisplay; }
			set { m_valuationBasisDisplay = value; }
		}

		/// <summary>
		/// Formatted field used to display the tax provision estimate
		/// </summary>
		public string TaxProvisionEstimateDisplay
		{
			get { return m_taxProvisionDisplay; }
			set { m_taxProvisionDisplay = value; }
		}

		/// <summary>
		/// Formatted display string for the predicted price (plus formula)
		/// </summary>
		public string PredictedPriceFormulaDisplay
		{
			get
			{
				StringBuilder builder = new StringBuilder();
				builder.Append("Predicted Price Calculation:\r\n\n\t");	
				builder.Append(PreviousPriceDisplay + " x (1 + ");
				builder.Append(PredictedAssetMovementDisplay2 + " + ");
				builder.Append(RevaluationFactorDisplay + " + ");
				builder.Append(ScalingFactorDisplay + " + ");
				builder.Append(ValuationBasisDisplay + " + ");
				builder.Append(XFactorDisplay2 + ") x (1 - ");
				builder.Append(TaxProvisionEstimateDisplay + ")");
				builder.Append("\r\n\n\tPredicted Price = ");
				builder.Append(PredictedPriceDisplay);
				builder.Append("\n\n");
				builder.Append(MessageBoxHelper.DialogText("PredictedPriceFormula"));
				return builder.ToString();
			}
		}

		/// <summary>
		/// Formatted field used to display the asset movement variance.
		/// </summary>
		public string AssetMovementVarianceDisplay
		{
			get { return m_AssetMovementVarianceDisplay; }
			set { m_AssetMovementVarianceDisplay = value; }
		}

		/// <summary>
		/// Display friendly string giving result of asset movement tolerance check.
		/// Required for new (drop 2 and 3) status screen
		/// </summary>
		public string WithinAssetMovementToleranceDisplay
		{
			get { return m_WithinAssetMovementToleranceDisplay; }
			set { m_WithinAssetMovementToleranceDisplay = value; }
		}

		/// <summary>
		/// Display friendly string giving result of asset movement tolerance check.
		/// LEGACY - todo - can we get rid of this?
		/// </summary>
		public string AssetMovementToleranceDisplay
		{
			get { return m_AssetMovementToleranceDisplay; }
			set { m_AssetMovementToleranceDisplay = value; }
		}

		/// <summary>
		/// Display friendly string of asset movement tolerance.
		/// </summary>
		public string AssetMovementToleranceDisplay2
		{
			get { return m_AssetMovementToleranceDisplay2; }
			set { m_AssetMovementToleranceDisplay2 = value; }
		}

		/// <summary>
		/// Display friendly string of upper tolerance.
		/// </summary>
		public string UpperToleranceDisplay
		{ //todo - we don't have a variable for UpperToleranceSet!
			get { return m_UpperToleranceDisplay; }
			set { m_UpperToleranceDisplay = value; }
		}

		/// <summary>
		/// as above but displays 0 as opposed to unavailable
		/// </summary>
		public string UpperToleranceDisplay2
		{ //todo - we don't have a variable for UpperToleranceSet!
			get { return m_UpperToleranceDisplay2; }
			set { m_UpperToleranceDisplay2 = value; }
		}

		/// <summary>
		/// Gets or sets the full name.
		/// </summary>
		/// <value></value>
		public string FullName
		{
			get { return m_FullName; }
			set { m_FullName = value; }
		}


		/// <summary>
		/// Gets or sets the asset movement tolerance.
		/// </summary>
		/// <value></value>
		public decimal AssetMovementTolerance
		{
			get { return m_AssetMovementTolerance; }
			set { m_AssetMovementTolerance = value; }
		}

		/// <summary>
		/// Gets or sets the asset fund ID.
		/// </summary>
		/// <value></value>
		public string AssetFundID
		{
			get { return m_AssetFundId; }
			set { m_AssetFundId = value; }
		}

		/// <summary>
		/// Gets or sets the upper tolerance.
		/// </summary>
		/// <value></value>
		public decimal UpperTolerance
		{
			get { return m_UpperTolerance; }
			set { m_UpperTolerance = value; }
		}

		/// <summary>
		/// Gets or sets the lower tolerance.
		/// </summary>
		/// <value></value>
		public decimal LowerTolerance
		{
			get { return m_LowerTolerance; }
			set { m_LowerTolerance = value; }
		}


		/// <summary>
		/// Display friendly string of lower tolerance.
		/// </summary>
		public string LowerToleranceDisplay
		{
			get
			{
				//todo - we don't have a variable for LowerToleranceSet!
				return DisplayFormat.Percent(this.LowerTolerance, this.LowerTolerance > 0);
			}
		}

		/// <summary>
		/// as above but displays 0 as opposed to unavailable
		/// </summary>
		public string LowerToleranceDisplay2
		{
			get { return DisplayFormat.Percent(this.LowerTolerance, true); }
		}

		/// <summary>
		/// Display friendly string giving result of price movement tolerance checks.
		/// </summary>
		public string PriceMovementToleranceDisplay
		{
			get { return m_PriceMovementToleranceDisplay; }
			set { m_PriceMovementToleranceDisplay = value; }
		}

		/// <summary>
		/// Display friendly string giving result of price movement tolerance checks including rounding.
		/// </summary>
		public string PriceMovementRoundedToleranceDisplay
		{
			get { return m_PriceMovementRoundedToleranceDisplay; }
			set { m_PriceMovementRoundedToleranceDisplay = value; }
		}

		/// <summary>
		/// Display friendly string giving the result of the price lower tolerance check.
		/// </summary>
		public string PriceMovementLowerToleranceDisplay
		{
			get { return m_PriceMovementLowerToleranceDisplay; }
			set { m_PriceMovementLowerToleranceDisplay = value; }
		}

		/// <summary>
		/// Display friendly string showing if this is a fund for price increase only
		/// </summary>
		public string PriceIncreaseOnlyDisplay
		{
			get { return (this.PriceIncreaseOnly ? "Y" : "N"); }
		}

		/// <summary>
		/// Display friendly string giving the result of the price upper tolerance check.
		/// </summary>
		public string PriceMovementUpperToleranceDisplay
		{
			get { return m_PriceMovementUpperToleranceDisplay; }
			set { m_PriceMovementUpperToleranceDisplay = value; }
		}

		/// <summary>
		/// Display friendly string giving the result of the price direction tolerance check.
		/// </summary>
		public string PriceMovementDirectionToleranceDisplay
		{
			get { return m_PriceMovementDirectionToleranceDisplay; }
			set { m_PriceMovementDirectionToleranceDisplay = value; }
		}

		/// <summary>
		/// Display a Y / N based string indicating whether or not the price has been authorised
		/// </summary>
		public string IsAuthorisedDisplay
		{
			get { return m_IsAuthorisedDisplay; }
			set { m_IsAuthorisedDisplay = value; }
		}

		/// <summary>
		/// formatted field for the Xfactor
		/// </summary>
		public string XFactorDisplay
		{
			get { return m_XFactorDisplay; }
			set { m_XFactorDisplay = value; }
		}
		
		/// <summary>
		/// formatted field for the Xfactor
		/// </summary>
		public string XFactorDisplay2
		{
			get { return m_XFactorDisplay2; }
			set { m_XFactorDisplay2 = value; }
		}

		/// <summary>
		/// Displays a Y or N 
		/// </summary>
		public string OnHiPortfolio3Display
		{
			get { return m_OnHiPortfolio3Display; }
			set { m_OnHiPortfolio3Display = value; }
		}

		/// <summary>
		/// Display a Y / N based string indicating whether or not the fund can be used for benchmarking
		/// </summary>
		public string IsBenchMarkableDisplay
		{
			get { return m_IsBenchMarkableDisplay; }
			set { m_IsBenchMarkableDisplay = value; }
		}

	}

	/// <summary>
	/// This class can be used when display formating is required
	/// </summary>
	public class FundDecorator3
	{
		/// <summary>
		/// Creates a decorated object from an Fund.
		/// </summary>
		/// <param name="fund">Fund.</param>
		/// <returns></returns>
		public static FundDecorator3 ToDecoratedObject(Fund fund)
		{
			return new FundDecorator3(fund);
		}

		/// <summary>
		/// Takes a decorated object and creates an Fund.
		/// </summary>
		/// <param name="decoratedFund">Decorated fund.</param>
		/// <returns></returns>
		public static Fund FromDecoratedObject(FundDecorator decoratedFund)
		{
			return decoratedFund.Fund;
		}

		/// <summary>
		/// Takes a decorated list of objects and returns an FundCollection.
		/// </summary>
		/// <param name="decoratedFunds">Decorated funds.</param>
		/// <returns></returns>
		public static FundCollection FromDecoratedList(IList decoratedFunds)
		{
			FundCollection returnFundCollection = new FundCollection();
			foreach (FundDecorator decoratedFund in decoratedFunds)
			{
				returnFundCollection.Add(FromDecoratedObject(decoratedFund));
			}

			return returnFundCollection;
		}

		/// <summary>
		/// From a regular to a decorated list.
		/// </summary>
		/// <param name="fundCollection">Fund collection.</param>
		/// <returns></returns>
		public static IList ToDecoratedList(IList fundCollection)
		{
			IList returnList = new ArrayList();
			foreach (Fund fund in fundCollection)
			{
				returnList.Add(ToDecoratedObject(fund));
			}

			return returnList;
		}

		private string m_PriceDisplay;
		private string m_PreviousPriceDisplay;
		private string m_fundStatus;
		private string m_PredictedPriceDisplay;
		private string m_PriceMovementPercentDisplay;
		private string m_PredictedPriceMovementPercentDisplay;
		private string m_PriceMovementVarianceDisplay;
		private string m_AssetUnitPriceDisplay;
		private string m_AssetMovementDisplay;
		private string m_AssetMovementDisplay2;
		private string m_PredictedAssetMovementDisplay;
		private string m_PredictedAssetMovementDisplay2;
		private string m_AssetMovementVarianceDisplay;
		private string m_WithinAssetMovementToleranceDisplay;
		private string m_AssetMovementToleranceDisplay;
		private string m_AssetMovementToleranceDisplay2;
		private string m_UpperToleranceDisplay;
		private string m_UpperToleranceDisplay2;
		private string m_FullName;
		private decimal m_AssetMovementTolerance;
		private bool m_IsNew;
		private bool m_IsDirty;
		private decimal m_UpperTolerance;
		private string m_PropertiesHeader;
		private string m_AssetFundId;
		private IList m_Factors;
		private decimal m_LowerTolerance;
		private bool m_PriceIncreaseOnly;
		private string m_PriceMovementToleranceDisplay;
		private string m_PriceMovementRoundedToleranceDisplay;
		private string m_PriceMovementLowerToleranceDisplay;
		private string m_PriceMovementUpperToleranceDisplay;
		private string m_PriceMovementDirectionToleranceDisplay;
		private string m_IsAuthorisedDisplay;
		private string m_XFactorDisplay;
		private string m_XFactorDisplay2;
		private string m_OnHiPortfolio3Display;
		private string m_IsBenchMarkableDisplay;
		private DateTime m_StatusChangedTime;
		private Fund.FundStatusType m_FundStatus;
		private bool m_WithinAssetMovementTolerance;
		private bool m_PriceOutsideTolerance;
		private bool m_SecondLevelButNotFirst;
		private bool m_UsePredictedPrice;
		private bool m_ProgressStatus;
		private string m_revaluationFactorDisplay;
		private string m_scalingFactorDisplay;
		private string m_valuationBasisDisplay;
		private string m_taxProvisionDisplay;


		/// <summary>
		/// Creates a new <see cref="FundDecorator"/> instance.
		/// Needed for the grid to re-hydrate this object
		/// </summary>
		public FundDecorator3()
		{
		}

		/// <summary>
		/// Creates a new <see cref="FundDecorator"/> instance.
		/// </summary>
		/// <param name="fund">Fund.</param>
		protected FundDecorator3(Fund fund)
		{
			UsePredictedPrice = fund.UsePredictedPrice;
			ProgressStatus = fund.ProgressStatus;
			SecondLevelButNotFirst = (fund.FundStatus == Fund.FundStatusType.SecondLevelAuthorised && fund.WithinAssetMovementTolerance && !fund.PriceOutsideTolerance);
			PriceOutsideTolerance = fund.PriceOutsideTolerance;
			StatusChangedTime = fund.StatusChangedTime;
			IsBenchMarkableDisplay = fund.IsBenchMarkable ? "Y" : "N";
			OnHiPortfolio3Display = fund.OnHiPortfolio3 ? "Y" : "N";
			XFactorDisplay = DisplayFormat.Percent(fund.XFactor, true); //display a zero as opposed to 'unavailable'
			RevaluationFactorDisplay = DisplayFormat.Decimal(fund.Factors.RevaluationFctr.effectToday, true, "0.0000");
			ScalingFactorDisplay = DisplayFormat.Decimal(fund.Factors.ScalingFctr.effectToday, true, "0.0000");
			ValuationBasisDisplay = DisplayFormat.Decimal(fund.Factors.ValuationBasisFctr.effectToday, true, "0.0000");
			XFactorDisplay2 = DisplayFormat.Decimal(fund.XFactor, true, "0.0000");
			TaxProvisionEstimateDisplay = DisplayFormat.Decimal(fund.Factors.TPE.effectToday, true, "0.0000");
			IsAuthorisedDisplay = fund.IsAuthorised ? "Y" : "N";
			PriceMovementDirectionToleranceDisplay = fund.WithinPriceDirectionTolerance ? "Y" : "N";
			PriceMovementUpperToleranceDisplay = fund.WithinPriceUpperTolerance ? "Y" : "N";
			PriceMovementLowerToleranceDisplay = fund.WithinPriceLowerTolerance ? "Y" : "N";
			PriceMovementRoundedToleranceDisplay = buildPriceMovementRoundedToleranceDisplay(fund);
			PriceMovementToleranceDisplay = buildPriceMovementToleranceDisplay(fund);
			LowerTolerance = fund.LowerTolerance;
			Factors = fund.Factors;
			UpperTolerance = fund.UpperTolerance;
			IsNew = fund.IsNew;
			IsDirty = fund.IsDirty;
			PriceIncreaseOnly = fund.PriceIncreaseOnly;
			FullName = fund.FullName;
			AssetFundID = fund.ParentAssetFundID;
			PropertiesHeader = FullName.Trim() + " Properties";
			WithinAssetMovementToleranceDisplay = fund.WithinAssetMovementTolerance ? "Y" : "N";
			WithinAssetMovementTolerance = fund.WithinAssetMovementTolerance;
			AssetMovementTolerance = fund.AssetMovementTolerance;
			AssetMovementToleranceDisplay = fund.WithinAssetMovementTolerance ? "Yes" : "No";
			AssetMovementToleranceDisplay2 = DisplayFormat.Percent(fund.AssetMovementTolerance, fund.AssetMovementTolerance > 0);
			UpperToleranceDisplay = DisplayFormat.Percent(fund.UpperTolerance, fund.UpperTolerance > 0);
			UpperToleranceDisplay2 = DisplayFormat.Percent(fund.UpperTolerance, true);
			AssetMovementVarianceDisplay = DisplayFormat.Percent(fund.AssetMovementVariance, fund.AssetMovementVarianceSet);
			PriceDisplay = DisplayFormat.Decimal(fund.Price, fund.PriceSet, "#,##0.00");
			PreviousPriceDisplay = DisplayFormat.Decimal(fund.PreviousPrice, fund.PreviousPriceSet, "#,##0.00");
			FundStatusDisplay = TranslatePascalCasedString(fund.FundStatus.ToString());
			FundStatus = fund.FundStatus;

			decimal predPrice = fund.PredictedPrice;
			PredictedPriceDisplay = DisplayFormat.Decimal(predPrice, predPrice > 0M, "#,##0.00");
			PriceMovementPercentDisplay = DisplayFormat.Percent(fund.PriceMovementPercent, (fund.PriceSet && fund.PreviousPriceSet));
			PredictedPriceMovementPercentDisplay = DisplayFormat.Percent(fund.PredictedPriceMovementPercent, (fund.PreviousPriceSet));

			bool varianceSet = fund.PriceSet && fund.PreviousPriceSet;
			PriceMovementVarianceDisplay = DisplayFormat.Percent(fund.PriceMovementVariance, varianceSet);
			AssetUnitPriceDisplay = DisplayFormat.Decimal(fund.AssetUnitPrice, fund.AssetUnitPriceSet, "N2");
			AssetMovementDisplay = DisplayFormat.Percent(fund.AssetMovement, fund.AssetMovementSet);
			AssetMovementDisplay2 = DisplayFormat.Percent(fund.AssetMovement, fund.AssetMovementSet);
			PredictedAssetMovementDisplay = DisplayFormat.Percent(fund.PredictedAssetMovement, fund.PredictedAssetMovementSet);
			PredictedAssetMovementDisplay2 = DisplayFormat.Decimal(fund.PredictedAssetMovement, fund.PredictedAssetMovementSet, "0.0000");
		}

		/// <summary>
		/// Translates the pascal cased string to a readable string with spaces.
		/// This is usefull when you'd like to show your enum names
		/// </summary>
		/// <param name="pascalCasedString">Pascal cased string.</param>
		/// <returns></returns>
		public static string TranslatePascalCasedString(string pascalCasedString)
		{
			//we should perhaps use the EnumDisplayTextAttribute class i.e		
			//[HBOS.FS.AMP.Windows.Controls.EnumDisplayTextAttribute( "Common" )]

			char[] chars = pascalCasedString.ToString().ToCharArray();
			StringBuilder returnString = new StringBuilder();
			for (int i = 0; i < chars.Length; i++)
			{
				if (i > 0 && Char.IsUpper(chars[i]))
				{
					returnString.Append(String.Concat(" ", chars[i]));
				}
				else
				{
					returnString.Append(chars[i]);
				}
			}

			return returnString.ToString();
		}

		private string buildPriceMovementRoundedToleranceDisplay(Fund fund)
		{
			StringBuilder builder = new StringBuilder();

			if (fund.WithinRoundedPriceLowerTolerance && fund.WithinRoundedPriceUpperTolerance
				&& fund.WithinPriceDirectionTolerance)
			{
				builder.Append("Y");
			}
			else
			{
				builder.Append("UT - ");
				builder.Append(fund.WithinRoundedPriceUpperTolerance ? "Y" : "N");
				builder.Append("  LT - ");
				builder.Append(fund.WithinRoundedPriceLowerTolerance ? "Y" : "N");
				builder.Append("  PI - ");
				builder.Append(fund.WithinPriceDirectionTolerance ? "Y" : "N");
			}

			return builder.ToString();
		}

		private string buildPriceMovementToleranceDisplay(Fund fund)
		{
			StringBuilder builder = new StringBuilder();

			if (fund.WithinPriceLowerTolerance && fund.WithinPriceUpperTolerance
				&& fund.WithinPriceDirectionTolerance)
			{
				builder.Append("Y");
			}
			else
			{
				builder.Append("UT - ");
				builder.Append(fund.WithinPriceUpperTolerance ? "Y" : "N");
				builder.Append("  LT - ");
				builder.Append(fund.WithinPriceLowerTolerance ? "Y" : "N");
				builder.Append("  PI - ");
				builder.Append(fund.WithinPriceDirectionTolerance ? "Y" : "N");
			}

			return builder.ToString();
		}

		/// <summary>
		/// Gets or sets a value indicating whether [progress status].
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [progress status]; otherwise, <c>false</c>.
		/// </value>
		public bool ProgressStatus
		{
			get { return m_ProgressStatus; }
			set { m_ProgressStatus = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether [use predicted price].
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [use predicted price]; otherwise, <c>false</c>.
		/// </value>
		public bool UsePredictedPrice
		{
			get { return m_UsePredictedPrice; }
			set { m_UsePredictedPrice = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether [second level but not first].
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [second level but not first]; otherwise, <c>false</c>.
		/// </value>
		public bool SecondLevelButNotFirst
		{
			get { return m_SecondLevelButNotFirst; }
			set { m_SecondLevelButNotFirst = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether [within asset movement tolerance].
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [within asset movement tolerance]; otherwise, <c>false</c>.
		/// </value>
		public bool WithinAssetMovementTolerance
		{
			get { return m_WithinAssetMovementTolerance; }
			set { m_WithinAssetMovementTolerance = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether [within asset movement tolerance].
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [within asset movement tolerance]; otherwise, <c>false</c>.
		/// </value>
		public bool PriceOutsideTolerance
		{
			get { return m_PriceOutsideTolerance; }
			set { m_PriceOutsideTolerance = value; }
		}


		/// <summary>
		/// Gets or sets the fund status.
		/// </summary>
		/// <value></value>
		public Fund.FundStatusType FundStatus
		{
			get { return m_FundStatus; }
			set { m_FundStatus = value; }
		}


		/// <summary>
		/// Gets or sets the status changed time.
		/// </summary>
		/// <value></value>
		public DateTime StatusChangedTime
		{
			get { return m_StatusChangedTime; }
			set { m_StatusChangedTime = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether [price increase only].
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [price increase only]; otherwise, <c>false</c>.
		/// </value>
		public bool PriceIncreaseOnly
		{
			get { return m_PriceIncreaseOnly; }
			set { m_PriceIncreaseOnly = value; }
		}

		/// <summary>
		/// Gets or sets the factors.
		/// </summary>
		/// <value></value>
		public IList Factors
		{
			get { return m_Factors; }
			set { m_Factors = value; }
		}

		/// <summary>
		/// Gets or sets the properties header.
		/// </summary>
		/// <value></value>
		public string PropertiesHeader
		{
			get { return m_PropertiesHeader; }
			set { m_PropertiesHeader = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is dirty.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is dirty; otherwise, <c>false</c>.
		/// </value>
		public bool IsDirty
		{
			get { return m_IsDirty; }
			set { m_IsDirty = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is new.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is new; otherwise, <c>false</c>.
		/// </value>
		public bool IsNew
		{
			get { return m_IsNew; }
			set { m_IsNew = value; }
		}

		/// <summary>
		/// The formatted field used to display the price.
		/// </summary>
		public string PriceDisplay
		{
			get { return m_PriceDisplay; }
			set { m_PriceDisplay = value; }
		}

		/// <summary>
		/// The formatted field used to display the previous price.
		/// </summary>
		public string PreviousPriceDisplay
		{
			get { return m_PreviousPriceDisplay; }
			set { m_PreviousPriceDisplay = value; }
		}

		/// <summary>
		/// The current fund status as a string
		/// </summary>
		public string FundStatusDisplay
		{
			get { return m_fundStatus; }
			set { m_fundStatus = value; }
		}

		/// <summary>
		/// The formatted field used to display the predicted price.
		/// </summary>
		public string PredictedPriceDisplay
		{
			get { return m_PredictedPriceDisplay; }
			set { m_PredictedPriceDisplay = value; }
		}


		/// <summary>
		/// The formatted field used to display the price movement.
		/// </summary>
		public string PriceMovementPercentDisplay
		{
			get { return m_PriceMovementPercentDisplay; }
			set { m_PriceMovementPercentDisplay = value; }
		}

		/// <summary>
		/// The formatted string to display the predicted price movement.
		/// </summary>
		public string PredictedPriceMovementPercentDisplay
		{
			get { return m_PredictedPriceMovementPercentDisplay; }
			set { m_PredictedPriceMovementPercentDisplay = value; }
		}

		/// <summary>
		/// A formatted string showing the price movement variance.
		/// </summary>
		public string PriceMovementVarianceDisplay
		{
			get { return m_PriceMovementVarianceDisplay; }
			set { m_PriceMovementVarianceDisplay = value; }
		}

		/// <summary>
		/// The formatted field used to display the asset unit price.
		/// </summary>
		public string AssetUnitPriceDisplay
		{
			get { return m_AssetUnitPriceDisplay; }
			set { m_AssetUnitPriceDisplay = value; }
		}

		/// <summary>
		/// The formatted field used to display the asset movement.
		/// </summary>
		public string AssetMovementDisplay
		{
			get { return m_AssetMovementDisplay; }
			set { m_AssetMovementDisplay = value; }
		}

		/// <summary>
		/// as above but displays 0 as opposed to unavailable
		/// </summary>
		public string AssetMovementDisplay2
		{
			get { return m_AssetMovementDisplay2; }
			set { m_AssetMovementDisplay2 = value; }
		}

		/// <summary>
		/// Formatted field used to display the predicted asset movement.
		/// </summary>
		public string PredictedAssetMovementDisplay
		{
			get { return m_PredictedAssetMovementDisplay; }
			set { m_PredictedAssetMovementDisplay = value; }
		}

		/// <summary>
		/// Formatted field used to display the predicted asset movement.
		/// </summary>
		public string PredictedAssetMovementDisplay2
		{
			get { return m_PredictedAssetMovementDisplay2; }
			set { m_PredictedAssetMovementDisplay2 = value; }
		}

		/// <summary>
		/// Formatted field used to display the revaluation factor
		/// </summary>
		public string RevaluationFactorDisplay
		{
			get { return m_revaluationFactorDisplay; }
			set { m_revaluationFactorDisplay = value; }
		}

		/// <summary>
		/// Formatted field used to display the scaling factor
		/// </summary>
		public virtual string ScalingFactorDisplay
		{
			get { return m_scalingFactorDisplay; }
			set { m_scalingFactorDisplay = value; }
		}

		/// <summary>
		/// Formatted field used to display the valuation basis
		/// </summary>
		public string ValuationBasisDisplay
		{
			get { return m_valuationBasisDisplay; }
			set { m_valuationBasisDisplay = value; }
		}

		/// <summary>
		/// Formatted field used to display the tax provision estimate
		/// </summary>
		public string TaxProvisionEstimateDisplay
		{
			get { return m_taxProvisionDisplay; }
			set { m_taxProvisionDisplay = value; }
		}

		/// <summary>
		/// Formatted display string for the predicted price (plus formula)
		/// </summary>
		public string PredictedPriceFormulaDisplay
		{
			get
			{
				StringBuilder builder = new StringBuilder();
				builder.Append("Predicted Price Calculation:\r\n\n\t");	
				builder.Append(PreviousPriceDisplay + " x (1 + ");
				builder.Append(PredictedAssetMovementDisplay2 + " + ");
				builder.Append(RevaluationFactorDisplay + " + ");
				builder.Append(ScalingFactorDisplay + " + ");
				builder.Append(ValuationBasisDisplay + " + ");
				builder.Append(XFactorDisplay2 + ") x (1 - ");
				builder.Append(TaxProvisionEstimateDisplay + ")");
				builder.Append("\r\n\n\tPredicted Price = ");
				builder.Append(PredictedPriceDisplay);
				builder.Append("\n\n");
				builder.Append(MessageBoxHelper.DialogText("PredictedPriceFormula"));
				return builder.ToString();
			}
		}

		/// <summary>
		/// Formatted field used to display the asset movement variance.
		/// </summary>
		public string AssetMovementVarianceDisplay
		{
			get { return m_AssetMovementVarianceDisplay; }
			set { m_AssetMovementVarianceDisplay = value; }
		}

		/// <summary>
		/// Display friendly string giving result of asset movement tolerance check.
		/// Required for new (drop 2 and 3) status screen
		/// </summary>
		public string WithinAssetMovementToleranceDisplay
		{
			get { return m_WithinAssetMovementToleranceDisplay; }
			set { m_WithinAssetMovementToleranceDisplay = value; }
		}

		/// <summary>
		/// Display friendly string giving result of asset movement tolerance check.
		/// LEGACY - todo - can we get rid of this?
		/// </summary>
		public string AssetMovementToleranceDisplay
		{
			get { return m_AssetMovementToleranceDisplay; }
			set { m_AssetMovementToleranceDisplay = value; }
		}

		/// <summary>
		/// Display friendly string of asset movement tolerance.
		/// </summary>
		public string AssetMovementToleranceDisplay2
		{
			get { return m_AssetMovementToleranceDisplay2; }
			set { m_AssetMovementToleranceDisplay2 = value; }
		}

		/// <summary>
		/// Display friendly string of upper tolerance.
		/// </summary>
		public string UpperToleranceDisplay
		{ //todo - we don't have a variable for UpperToleranceSet!
			get { return m_UpperToleranceDisplay; }
			set { m_UpperToleranceDisplay = value; }
		}

		/// <summary>
		/// as above but displays 0 as opposed to unavailable
		/// </summary>
		public string UpperToleranceDisplay2
		{ //todo - we don't have a variable for UpperToleranceSet!
			get { return m_UpperToleranceDisplay2; }
			set { m_UpperToleranceDisplay2 = value; }
		}

		/// <summary>
		/// Gets or sets the full name.
		/// </summary>
		/// <value></value>
		public string FullName
		{
			get { return m_FullName; }
			set { m_FullName = value; }
		}


		/// <summary>
		/// Gets or sets the asset movement tolerance.
		/// </summary>
		/// <value></value>
		public decimal AssetMovementTolerance
		{
			get { return m_AssetMovementTolerance; }
			set { m_AssetMovementTolerance = value; }
		}

		/// <summary>
		/// Gets or sets the asset fund ID.
		/// </summary>
		/// <value></value>
		public string AssetFundID
		{
			get { return m_AssetFundId; }
			set { m_AssetFundId = value; }
		}

		/// <summary>
		/// Gets or sets the upper tolerance.
		/// </summary>
		/// <value></value>
		public decimal UpperTolerance
		{
			get { return m_UpperTolerance; }
			set { m_UpperTolerance = value; }
		}

		/// <summary>
		/// Gets or sets the lower tolerance.
		/// </summary>
		/// <value></value>
		public decimal LowerTolerance
		{
			get { return m_LowerTolerance; }
			set { m_LowerTolerance = value; }
		}


		/// <summary>
		/// Display friendly string of lower tolerance.
		/// </summary>
		public string LowerToleranceDisplay
		{
			get
			{
				//todo - we don't have a variable for LowerToleranceSet!
				return DisplayFormat.Percent(this.LowerTolerance, this.LowerTolerance > 0);
			}
		}

		/// <summary>
		/// as above but displays 0 as opposed to unavailable
		/// </summary>
		public string LowerToleranceDisplay2
		{
			get { return DisplayFormat.Percent(this.LowerTolerance, true); }
		}

		/// <summary>
		/// Display friendly string giving result of price movement tolerance checks.
		/// </summary>
		public string PriceMovementToleranceDisplay
		{
			get { return m_PriceMovementToleranceDisplay; }
			set { m_PriceMovementToleranceDisplay = value; }
		}

		/// <summary>
		/// Display friendly string giving result of price movement tolerance checks including rounding.
		/// </summary>
		public string PriceMovementRoundedToleranceDisplay
		{
			get { return m_PriceMovementRoundedToleranceDisplay; }
			set { m_PriceMovementRoundedToleranceDisplay = value; }
		}

		/// <summary>
		/// Display friendly string giving the result of the price lower tolerance check.
		/// </summary>
		public string PriceMovementLowerToleranceDisplay
		{
			get { return m_PriceMovementLowerToleranceDisplay; }
			set { m_PriceMovementLowerToleranceDisplay = value; }
		}

		/// <summary>
		/// Display friendly string showing if this is a fund for price increase only
		/// </summary>
		public string PriceIncreaseOnlyDisplay
		{
			get { return (this.PriceIncreaseOnly ? "Y" : "N"); }
		}

		/// <summary>
		/// Display friendly string giving the result of the price upper tolerance check.
		/// </summary>
		public string PriceMovementUpperToleranceDisplay
		{
			get { return m_PriceMovementUpperToleranceDisplay; }
			set { m_PriceMovementUpperToleranceDisplay = value; }
		}

		/// <summary>
		/// Display friendly string giving the result of the price direction tolerance check.
		/// </summary>
		public string PriceMovementDirectionToleranceDisplay
		{
			get { return m_PriceMovementDirectionToleranceDisplay; }
			set { m_PriceMovementDirectionToleranceDisplay = value; }
		}

		/// <summary>
		/// Display a Y / N based string indicating whether or not the price has been authorised
		/// </summary>
		public string IsAuthorisedDisplay
		{
			get { return m_IsAuthorisedDisplay; }
			set { m_IsAuthorisedDisplay = value; }
		}

		/// <summary>
		/// formatted field for the Xfactor
		/// </summary>
		public string XFactorDisplay
		{
			get { return m_XFactorDisplay; }
			set { m_XFactorDisplay = value; }
		}
		
		/// <summary>
		/// formatted field for the Xfactor
		/// </summary>
		public string XFactorDisplay2
		{
			get { return m_XFactorDisplay2; }
			set { m_XFactorDisplay2 = value; }
		}

		/// <summary>
		/// Displays a Y or N 
		/// </summary>
		public string OnHiPortfolio3Display
		{
			get { return m_OnHiPortfolio3Display; }
			set { m_OnHiPortfolio3Display = value; }
		}

		/// <summary>
		/// Display a Y / N based string indicating whether or not the fund can be used for benchmarking
		/// </summary>
		public string IsBenchMarkableDisplay
		{
			get { return m_IsBenchMarkableDisplay; }
			set { m_IsBenchMarkableDisplay = value; }
		}

	}
}