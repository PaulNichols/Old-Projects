using System;
using System.Data;
using HBOS.FS.AMP.Data.Types;
using HBOS.FS.AMP.Data.Validator.Interface;
using HBOS.FS.AMP.UPD.Types.AssetFunds;

namespace HBOS.FS.AMP.UPD.Controllers
{
	/// <summary>
	/// CountryDataRowValidator - makes sure a row contains a valid country and the country has a market associated
	/// </summary>
	public class CountryStockmarketDataRowValidator : IDataRowValidator
	{
		#region Events

		/// <summary>
		/// Invalid Data Row Event
		/// </summary>
		public event InvalidDataRowDelegate InvalidDataRowEvent;

		#endregion

		#region Variables

		private StockMarketCollection m_allMarkets;
		private readonly int m_countryColumnPosition;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor which remembers the countries.
		/// </summary>
		/// <param name="allMarkets"></param>
		/// <param name="countryColumnPosition">The position in the text file that the country appears</param>
		public CountryStockmarketDataRowValidator(StockMarketCollection allMarkets, int countryColumnPosition)
		{
			this.m_allMarkets = allMarkets;
			this.m_countryColumnPosition = countryColumnPosition;
		}

		#endregion

		#region IDataRowValidator Members

		/// <summary>
		/// Validate the Currency Exchange Rate record
		/// </summary>
		/// <param name="dataRow"></param>
		/// <param name="dataDefinition"></param>
		/// <returns></returns>
		public ValidationErrorSeverity Validate(string[] dataRow, DataTable dataDefinition)
		{
			string countryCode = dataRow[m_countryColumnPosition].ToString();
			ValidationErrorSeverity returnSeverity = ValidationErrorSeverity.None;

			bool foundAMarketForTheCountry=false;
			foreach (StockMarket market in m_allMarkets)
			{
				if (market.CountryCode==countryCode )
				{
					foundAMarketForTheCountry=true;
					break;
				}
			}

			if (!foundAMarketForTheCountry)
			{
				returnSeverity = ValidationErrorSeverity.High;

				if (this.InvalidDataRowEvent != null)
				{
					InvalidDataRowEvent(this, new InvalidDataRowEventArgs(dataRow, dataDefinition, String.Format("Country Code value of {0} is not valid or there is not Market set-up for this country.", countryCode), ValidationErrorSeverity.High));
				}
			}

			return returnSeverity;
		}

		#endregion
	}
}