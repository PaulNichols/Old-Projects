using System;
using System.Data;
using System.Data.SqlClient;
using HBOS.FS.AMP.Data.Persister;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.Common.ExceptionManagement;
using HBOS.FS.Support.Tex;
using Microsoft.ApplicationBlocks.Data;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// ImportHi3PricesLinkedSqlPersister - persist some Hi3 Prices
	/// </summary>
	public class ImportHi3PricesLinkedSqlPersister : SqlWithParentPersister
	{
		#region Variables

	//	private string m_companyCode;
		private int m_seriesNumber;
		private bool m_OEICFund;

		#endregion

		#region Parameter Position

		/// <summary>
		/// Parameter Positions
		/// </summary>
		private enum ParameterPosition : int
		{
			ImportId = 0,
			HiPortfolioCode,
			ValuationPoint,
			BidPrice,
			OfferPrice,
			AssetUnitPrice,
			BarePrice,
			Yield,
			UnroundedBidPrice,
			UnroundedOfferPrice,
			CurrencyCode,
			ValuationBasis,
			PolicyHolderUnits,
			CompositeUnits,
			EquitableUnits
		}

		/// <summary>
		/// Hi3 Prices Column Position
		/// </summary>
		public enum Hi3PricesLinkedColumnPosition : int
		{
			/// <summary>
			/// AssetFundId
			/// </summary>
			AssetFundId = 0,
			/// <summary>
			/// ValuationDate
			/// </summary>
			ValuationDate = 1,
			/// <summary>
			/// ValuationTime
			/// </summary>
			ValuationTime = 2,
			/// <summary>
			/// ValuationBasis (3)
			/// </summary>
			ValuationBasis,
			/// <summary>
			/// CurrencyCode
			/// </summary>
			CurrencyCode,
			/// <summary>
			/// BidOrSingle1
			/// </summary>
			BidOrSingle1,
			/// <summary>
			/// UnroundedBidPrice1,
			/// </summary>
			UnroundedBidPrice1,
			/// <summary>
			/// UnroundedOfferPrice1,
			/// </summary>
			UnroundedOfferPrice1,
			/// <summary>
			/// OfferPrice1,
			/// </summary>
			OfferPrice1,
			/// <summary>
			/// PolicyHolderUnits1,
			/// </summary>
			PolicyholderUnits1,
			/// <summary>
			/// AssetUnitPrice1,
			/// </summary>
			AssetUnitPrice1,
			/// <summary>
			/// BarePrice1,
			/// </summary>
			BarePrice1,
			/// <summary>
			/// CompositeUnits1,
			/// </summary>
			CompositeUnits1,
			/// <summary>
			/// EquitableUnits1,
			/// </summary>
			EquitableUnits1,
			/// <summary>
			/// Yield1,
			/// </summary>
			Yield1,
			/// <summary>
			/// BidOrSingle2,
			/// </summary>
			BidOrSingle2,
			/// <summary>
			/// UnroundedBidPrice2,
			/// </summary>
			UnroundedBidPrice2,
			/// <summary>
			/// UnroundedOfferPrice2,
			/// </summary>
			UnroundedOfferPrice2,
			/// <summary>
			/// OfferPrice2,
			/// </summary>
			OfferPrice2,
			/// <summary>
			/// PolicyHolderUnits2,
			/// </summary>
			PolicyholderUnits2,
			/// <summary>
			/// AssetUnitPrice2,
			/// </summary>
			AssetUnitPrice2,
			/// <summary>
			/// BarePrice2,
			/// </summary>
			BarePrice2,
			/// <summary>
			/// CompositeUnits2,
			/// </summary>
			CompositeUnits2,
			/// <summary>
			/// EquitableUnits2,
			/// </summary>
			EquitableUnits2,
			/// <summary>
			/// Yield2,
			/// </summary>
			Yield2,
			/// <summary>
			/// BidOrSingle3,
			/// </summary>
			BidOrSingle3,
			/// <summary>
			/// UnroundedBidPrice3,
			/// </summary>
			UnroundedBidPrice3,
			/// <summary>
			/// UnroundedOfferPrice3,
			/// </summary>
			UnroundedOfferPrice3,
			/// <summary>
			/// OfferPrice3,
			/// </summary>
			OfferPrice3,
			/// <summary>
			/// PolicyHolderUnits3,
			/// </summary>
			PolicyholderUnits3,
			/// <summary>
			/// AssetUnitPrice3,
			/// </summary>
			AssetUnitPrice3,
			/// <summary>
			/// BarePrice3,
			/// </summary>
			BarePrice3,
			/// <summary>
			/// CompositeUnits3,
			/// </summary>
			CompositeUnits3,
			/// <summary>
			/// EquitableUnits2,
			/// </summary>
			EquitableUnits3,
			/// <summary>
			/// Yield3,
			/// </summary>
			Yield3,
			/// <summary>
			/// BidOrSingle4,
			/// </summary>
			BidOrSingle4,
			/// <summary>
			/// UnroundedBidPrice4,
			/// </summary>
			UnroundedBidPrice4,
			/// <summary>
			/// UnroundedOfferPrice4,
			/// </summary>
			UnroundedOfferPrice4,
			/// <summary>
			/// OfferPrice4,
			/// </summary>
			OfferPrice4,
			/// <summary>
			/// PolicyHolderUnits4,
			/// </summary>
			PolicyholderUnits4,
			/// <summary>
			/// AssetUnitPrice4,
			/// </summary>
			AssetUnitPrice4,
			/// <summary>
			/// BarePrice4,
			/// </summary>
			BarePrice4,
			/// <summary>
			/// CompositeUnits4,
			/// </summary>
			CompositeUnits4,
			/// <summary>
			/// EquitableUnits4,
			/// </summary>
			EquitableUnits4,
			/// <summary>
			/// Yield4,
			/// </summary>
			Yield4,
			/// <summary>
			/// BidOrSingle5,
			/// </summary>
			BidOrSingle5,
			/// <summary>
			/// UnroundedBidPrice5,
			/// </summary>
			UnroundedBidPrice5,
			/// <summary>
			/// UnroundedOfferPrice5,
			/// </summary>
			UnroundedOfferPrice5,
			/// <summary>
			/// OfferPrice5,
			/// </summary>
			OfferPrice5,
			/// <summary>
			/// PolicyHolderUnits5,
			/// </summary>
			PolicyholderUnits5,
			/// <summary>
			/// AssetUnitPrice5,
			/// </summary>
			AssetUnitPrice5,
			/// <summary>
			/// BarePrice5,
			/// </summary>
			BarePrice5,
			/// <summary>
			/// CompositeUnits5,
			/// </summary>
			CompositeUnits5,
			/// <summary>
			/// EquitableUnits5,
			/// </summary>
			EquitableUnits5,
			/// <summary>
			/// Yield5,
			/// </summary>
			Yield5,
			/// <summary>
			/// BidOrSingle6,
			/// </summary>
			BidOrSingle6,
			/// <summary>
			/// UnroundedBidPrice6,
			/// </summary>
			UnroundedBidPrice6,
			/// <summary>
			/// UnroundedOfferPrice6,
			/// </summary>
			UnroundedOfferPrice6,
			/// <summary>
			/// OfferPrice6,
			/// </summary>
			OfferPrice6,
			/// <summary>
			/// PolicyHolderUnits6,
			/// </summary>
			PolicyholderUnits6,
			/// <summary>
			/// AssetUnitPrice6,
			/// </summary>
			AssetUnitPrice6,
			/// <summary>
			/// BarePrice6,
			/// </summary>
			BarePrice6,
			/// <summary>
			/// CompositeUnits6,
			/// </summary>
			CompositeUnits6,
			/// <summary>
			/// EquitableUnits6,
			/// </summary>
			EquitableUnits6,
			/// <summary>
			/// Yield6,
			/// </summary>
			Yield6,
			/// <summary>
			/// BidOrSingle7,
			/// </summary>
			BidOrSingle7,
			/// <summary>
			/// UnroundedBidPrice7,
			/// </summary>
			UnroundedBidPrice7,
			/// <summary>
			/// UnroundedOfferPrice7,
			/// </summary>
			UnroundedOfferPrice7,
			/// <summary>
			/// OfferPrice7,
			/// </summary>
			OfferPrice7,
			/// <summary>
			/// PolicyHolderUnits7,
			/// </summary>
			PolicyholderUnits7,
			/// <summary>
			/// AssetUnitPrice7,
			/// </summary>
			AssetUnitPrice7,
			/// <summary>
			/// BarePrice7,
			/// </summary>
			BarePrice7,
			/// <summary>
			/// CompositeUnits7,
			/// </summary>
			CompositeUnits7,
			/// <summary>
			/// EquitableUnits7,
			/// </summary>
			EquitableUnits7,
			/// <summary>
			/// Yield7,
			/// </summary>
			Yield7,
			/// <summary>
			/// BidOrSingle8,
			/// </summary>
			BidOrSingle8,
			/// <summary>
			/// UnroundedBidPrice8,
			/// </summary>
			UnroundedBidPrice8,
			/// <summary>
			/// UnroundedOfferPrice8,
			/// </summary>
			UnroundedOfferPrice8,
			/// <summary>
			/// OfferPrice8,
			/// </summary>
			OfferPrice8,
			/// <summary>
			/// PolicyHolderUnits8,
			/// </summary>
			PolicyholderUnits8,
			/// <summary>
			/// AssetUnitPrice8,
			/// </summary>
			AssetUnitPrice8,
			/// <summary>
			/// BarePrice8,
			/// </summary>
			BarePrice8,
			/// <summary>
			/// CompositeUnits8,
			/// </summary>
			CompositeUnits8,
			/// <summary>
			/// EquitableUnits8,
			/// </summary>
			EquitableUnits8,
			/// <summary>
			/// Yield8,
			/// </summary>
			Yield8,
			/// <summary>
			/// BidOrSingle9,
			/// </summary>
			BidOrSingle9,
			/// <summary>
			/// UnroundedBidPrice9,
			/// </summary>
			UnroundedBidPrice9,
			/// <summary>
			/// UnroundedOfferPrice9,
			/// </summary>
			UnroundedOfferPrice9,
			/// <summary>
			/// OfferPrice9,
			/// </summary>
			OfferPrice9,
			/// <summary>
			/// PolicyHolderUnits9,
			/// </summary>
			PolicyholderUnits9,
			/// <summary>
			/// AssetUnitPrice9,
			/// </summary>
			AssetUnitPrice9,
			/// <summary>
			/// BarePrice9,
			/// </summary>
			BarePrice9,
			/// <summary>
			/// CompositeUnits9,
			/// </summary>
			CompositeUnits9,
			/// <summary>
			/// EquitableUnits9,
			/// </summary>
			EquitableUnits9,
			/// <summary>
			/// Yield9,
			/// </summary>
			Yield9,
			/// <summary>
			/// BidOrSingle10,
			/// </summary>
			BidOrSingle10,
			/// <summary>
			/// UnroundedBidPrice10,
			/// </summary>
			UnroundedBidPrice10,
			/// <summary>
			/// UnroundedOfferPrice10,
			/// </summary>
			UnroundedOfferPrice10,
			/// <summary>
			/// OfferPrice10,
			/// </summary>
			OfferPrice10,
			/// <summary>
			/// PolicyHolderUnits10,
			/// </summary>
			PolicyholderUnits10,
			/// <summary>
			/// AssetUnitPrice10,
			/// </summary>
			AssetUnitPrice10,
			/// <summary>
			/// BarePrice10,
			/// </summary>
			BarePrice10,
			/// <summary>
			/// CompositeUnits10,
			/// </summary>
			CompositeUnits10,
			/// <summary>
			/// EquitableUnits10,
			/// </summary>
			EquitableUnits10,
			/// <summary>
			/// Yield10
			/// </summary>
			Yield10
		}

		#endregion

		#region Constructor

		/// <summary>
		/// Import a Hi3 Price
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="persistingStoredProcedure"></param>
		/// <param name="parentParameterName"></param>
		/// <param name="parentId"></param>
		public ImportHi3PricesLinkedSqlPersister(string connectionString, string persistingStoredProcedure, string parentParameterName, int parentId) : base(connectionString, persistingStoredProcedure, parentParameterName, parentId)
		{
			T.E();

			try
			{
				//m_companyCode = companyCode;
			}
			finally
			{
				T.X();
			}
		}

		#endregion

		#region Properties

		/// <summary>
		/// Access to the series number
		/// </summary>
		public int SeriesNumber
		{
			get { return m_seriesNumber; }
			set { m_seriesNumber = value; }
		}

		/// <summary>
		/// Is it an OEIC fund
		/// </summary>
		public bool OEICFund
		{
			get { return m_OEICFund; }
			set { m_OEICFund = value; }
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Persist the data row
		/// </summary>
		/// <param name="dataRow"></param>
		/// <param name="schema"></param>
		public override void PersistRow(string[] dataRow, DataTable schema)
		{
			T.E();

			try
			{
				if (null == base.SqlParameters)
				{
					constructParams();
				}

				// Set the parameter values
				int dayNumber = int.Parse(dataRow[(int) Hi3PricesLinkedColumnPosition.ValuationDate].Substring(0, 2));
				int monthNumber = int.Parse(dataRow[(int) Hi3PricesLinkedColumnPosition.ValuationDate].Substring(3, 2));
				int yearNumber = 2000 + int.Parse(dataRow[(int) Hi3PricesLinkedColumnPosition.ValuationDate].Substring(6, 2));
				int hourNumber = int.Parse(dataRow[(int) Hi3PricesLinkedColumnPosition.ValuationTime].Substring(0, 2));
				int minuteNumber = int.Parse(dataRow[(int) Hi3PricesLinkedColumnPosition.ValuationTime].Substring(3, 2));

				// End of day is coming through as "24:00" so we need to subtract a minute
				if (hourNumber == 24 && minuteNumber == 00)
				{
					hourNumber = 23;
					minuteNumber = 59;
				}

				DateTime valuationDate = new DateTime(yearNumber, monthNumber, dayNumber, hourNumber, minuteNumber, 0);

				string assetFundId = dataRow[(int) Hi3PricesLinkedColumnPosition.AssetFundId].ToString();
				int seriesStartColumnPosition = CalculateSeriesStartColumnNumber(m_seriesNumber);

				base.SqlParameters[(int) ParameterPosition.HiPortfolioCode].Value = ImportHi3PricesSqlPersister.CalculatePostfolioCode(assetFundId, m_seriesNumber, m_OEICFund);
				base.SqlParameters[(int) ParameterPosition.ValuationPoint].Value = valuationDate;

				string bidPrice = dataRow[seriesStartColumnPosition].ToString();
				if (bidPrice == String.Empty)
				{
					base.SqlParameters[(int) ParameterPosition.BidPrice].Value = 0;
				}
				else
				{
					base.SqlParameters[(int) ParameterPosition.BidPrice].Value = Decimal.Parse(bidPrice);
				}

				string offerPrice = dataRow[seriesStartColumnPosition + 3];
				if (offerPrice == String.Empty)
				{
					base.SqlParameters[(int) ParameterPosition.OfferPrice].Value = 0;
				}
				else
				{
					base.SqlParameters[(int) ParameterPosition.OfferPrice].Value = Decimal.Parse(offerPrice);
				}

				string assetUnitPrice = dataRow[seriesStartColumnPosition + 5];
				if (assetUnitPrice == String.Empty)
				{
					base.SqlParameters[(int) ParameterPosition.AssetUnitPrice].Value = 0;
				}
				else
				{
					base.SqlParameters[(int) ParameterPosition.AssetUnitPrice].Value = Decimal.Parse(assetUnitPrice);
				}

				string barePrice = dataRow[seriesStartColumnPosition + 6];
				if (barePrice == String.Empty)
				{
					base.SqlParameters[(int) ParameterPosition.BarePrice].Value = 0;
				}
				else
				{
					base.SqlParameters[(int) ParameterPosition.BarePrice].Value = Decimal.Parse(barePrice);
				}

				string yield = dataRow[seriesStartColumnPosition + 8];
				if (yield == String.Empty)
				{
					base.SqlParameters[(int) ParameterPosition.Yield].Value = 0;
				}
				else
				{
					base.SqlParameters[(int) ParameterPosition.Yield].Value = Decimal.Parse(yield);
				}

				string unroundedBidPrice = dataRow[seriesStartColumnPosition + 1];
				if (unroundedBidPrice == String.Empty)
				{
					base.SqlParameters[(int) ParameterPosition.UnroundedBidPrice].Value = 0;
				}
				else
				{
					base.SqlParameters[(int) ParameterPosition.UnroundedBidPrice].Value = Decimal.Parse(unroundedBidPrice);
				}

				string unroundedOfferPrice = dataRow[seriesStartColumnPosition + 2];
				if (unroundedOfferPrice == String.Empty)
				{
					base.SqlParameters[(int) ParameterPosition.UnroundedOfferPrice].Value = 0;
				}
				else
				{
					base.SqlParameters[(int) ParameterPosition.UnroundedOfferPrice].Value = Decimal.Parse(unroundedOfferPrice);
				}

				base.SqlParameters[(int) ParameterPosition.CurrencyCode].Value = dataRow[(int) Hi3PricesLinkedColumnPosition.CurrencyCode].ToString();
				base.SqlParameters[(int) ParameterPosition.ValuationBasis].Value = ImportHi3PricesLinkedSqlPersister.TranslateValuationBasis(dataRow[(int) Hi3PricesLinkedColumnPosition.ValuationBasis]);

				string policyHolderUnits = dataRow[seriesStartColumnPosition + 4];
				if (policyHolderUnits == String.Empty)
				{
					base.SqlParameters[(int) ParameterPosition.PolicyHolderUnits].Value = 0;
				}
				else
				{
					base.SqlParameters[(int) ParameterPosition.PolicyHolderUnits].Value = Decimal.Parse(policyHolderUnits);
				}

				string compositeUnits = dataRow[seriesStartColumnPosition + 7];
				if (compositeUnits == String.Empty)
				{
					base.SqlParameters[(int) ParameterPosition.CompositeUnits].Value = 0;
				}
				else
				{
					base.SqlParameters[(int) ParameterPosition.CompositeUnits].Value = Decimal.Parse(compositeUnits);
				}

				string equitableUnits = dataRow[seriesStartColumnPosition + 8];
				if (equitableUnits == String.Empty)
				{
					base.SqlParameters[(int) ParameterPosition.EquitableUnits].Value = 0;
				}
				else
				{
					base.SqlParameters[(int) ParameterPosition.EquitableUnits].Value = Decimal.Parse(equitableUnits);
				}

				try
				{
					// persist the row
					SqlHelper.ExecuteNonQuery(base.SqlTransaction, CommandType.StoredProcedure, base.PersistingStoredProcedureName, base.SqlParameters);
				}
				catch (SqlException ex)
				{
					ExceptionManager.Publish(ex);
					T.DumpException(ex);

					switch (ex.Number)
					{
						case (int) DatabaseError.ConstraintViolation:
							throw new ConstraintViolationException(String.Format("Failed to import the Hi3 Prices data as the Valuation Date of '{0}' is in the future.", valuationDate.ToString()), base.SqlTransaction.Connection.DataSource + " " + base.SqlTransaction.Connection.Database, base.PersistingStoredProcedureName, base.SqlParameters, ex);
						default:
							throw new DatabaseException("Failed to save the Hi3 Prices data row.", base.SqlTransaction.Connection.DataSource + " " + base.SqlTransaction.Connection.Database, ex);
					}
				}
				catch
				{
					throw;
				}
			}
			finally
			{
				T.X();
			}
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Use to change 1 to B, 2 to M, 3 to O
		/// </summary>
		/// <param name="valuationBasisNumber"></param>
		/// <returns></returns>
		public static string TranslateValuationBasis(string valuationBasisNumber)
		{
			string returnValue;
			switch (valuationBasisNumber)
			{
				case "1":
					returnValue = "B";
					break;
				case "2":
					returnValue = "M";
					break;
				case "3":
					returnValue = "O";
					break;
				default:
					throw new Exception("An unknown ValuationBasis number was found to translate - " + valuationBasisNumber);
			}
			return returnValue;
		}

		/// <summary>
		/// Creates the instance level array of Sql Parameters to use when persisting a row
		/// </summary>
		private void constructParams()
		{
			T.E();

			try
			{
				base.SqlParameters = new SqlParameter[Enum.GetValues(typeof (ParameterPosition)).Length];

				SqlParameter parentParameter = new SqlParameter("@iImportID", SqlDbType.Int);
				parentParameter.Value = base.ParentId;
				base.SqlParameters[(int) ParameterPosition.ImportId] = parentParameter;

				SqlParameter portfolioCode = new SqlParameter("@sHiPortfolioCode", SqlDbType.Char, 10);
				base.SqlParameters[(int) ParameterPosition.HiPortfolioCode] = portfolioCode;

				SqlParameter valuationPoint = new SqlParameter("@dtValuationPoint", SqlDbType.DateTime);
				base.SqlParameters[(int) ParameterPosition.ValuationPoint] = valuationPoint;

				SqlParameter bidPrice = new SqlParameter("@mBidPrice", SqlDbType.Money);
				base.SqlParameters[(int) ParameterPosition.BidPrice] = bidPrice;

				SqlParameter offerPrice = new SqlParameter("@mOfferPrice", SqlDbType.Money);
				base.SqlParameters[(int) ParameterPosition.OfferPrice] = offerPrice;

				SqlParameter assetUnitPrice = new SqlParameter("@mAssetUnitPrice", SqlDbType.Money);
				base.SqlParameters[(int) ParameterPosition.AssetUnitPrice] = assetUnitPrice;

				SqlParameter barePrice = new SqlParameter("@mBarePrice", SqlDbType.Money);
				base.SqlParameters[(int) ParameterPosition.BarePrice] = barePrice;

				SqlParameter yield = new SqlParameter("@mYield", SqlDbType.Money);
				base.SqlParameters[(int) ParameterPosition.Yield] = yield;

				SqlParameter unroundedBidPrice = new SqlParameter("@mUnroundedBidPrice", SqlDbType.Money);
				base.SqlParameters[(int) ParameterPosition.UnroundedBidPrice] = unroundedBidPrice;

				SqlParameter unroundedOfferPrice = new SqlParameter("@mUnroundedOfferPrice", SqlDbType.Money);
				base.SqlParameters[(int) ParameterPosition.UnroundedOfferPrice] = unroundedOfferPrice;

				SqlParameter currencyCode = new SqlParameter("@sCurrencyCode", SqlDbType.VarChar, 10);
				base.SqlParameters[(int) ParameterPosition.CurrencyCode] = currencyCode;

				SqlParameter valuationBasis = new SqlParameter("@sValuationBasis", SqlDbType.Char, 1);
				base.SqlParameters[(int) ParameterPosition.ValuationBasis] = valuationBasis;

				SqlParameter policyHolderUnits = new SqlParameter("@dPolicyHolderUnits", SqlDbType.Decimal);
				base.SqlParameters[(int) ParameterPosition.PolicyHolderUnits] = policyHolderUnits;

				SqlParameter compositeUnits = new SqlParameter("@dCompositeUnits", SqlDbType.Decimal);
				base.SqlParameters[(int) ParameterPosition.CompositeUnits] = compositeUnits;

				SqlParameter equitableUnits = new SqlParameter("@dEquitableUnits", SqlDbType.Decimal);
				base.SqlParameters[(int) ParameterPosition.EquitableUnits] = equitableUnits;
			}
			finally
			{
				T.X();
			}
		}


		/// <summary>
		/// Calculate the column number for the beginning of the series.
		/// </summary>
		/// <returns></returns>
		public static int CalculateSeriesStartColumnNumber(int seriesNumber)
		{
			T.E();

			try
			{
				int startSeriesColumnNumber = 0;

				switch (seriesNumber)
				{
					case 1:
						startSeriesColumnNumber = (int) Hi3PricesLinkedColumnPosition.BidOrSingle1;
						break;
					case 2:
						startSeriesColumnNumber = (int) Hi3PricesLinkedColumnPosition.BidOrSingle2;
						break;
					case 3:
						startSeriesColumnNumber = (int) Hi3PricesLinkedColumnPosition.BidOrSingle3;
						break;
					case 4:
						startSeriesColumnNumber = (int) Hi3PricesLinkedColumnPosition.BidOrSingle4;
						break;
					case 5:
						startSeriesColumnNumber = (int) Hi3PricesLinkedColumnPosition.BidOrSingle5;
						break;
					case 6:
						startSeriesColumnNumber = (int) Hi3PricesLinkedColumnPosition.BidOrSingle6;
						break;
					case 7:
						startSeriesColumnNumber = (int) Hi3PricesLinkedColumnPosition.BidOrSingle7;
						break;
					case 8:
						startSeriesColumnNumber = (int) Hi3PricesLinkedColumnPosition.BidOrSingle8;
						break;
					case 9:
						startSeriesColumnNumber = (int) Hi3PricesLinkedColumnPosition.BidOrSingle9;
						break;
					case 10:
						startSeriesColumnNumber = (int) Hi3PricesLinkedColumnPosition.BidOrSingle10;
						break;
				}

				return startSeriesColumnNumber;
			}
			finally
			{
				T.X();
			}
		}

		#endregion
	}
}