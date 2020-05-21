using System;
using System.Data;
using HBOS.FS.AMP.Data.Types;
using HBOS.FS.AMP.Data.Validator.Interface;
using HBOS.FS.AMP.UPD.Persistence;
using HBOS.FS.AMP.UPD.Types.Currency;

namespace HBOS.FS.AMP.UPD.Controllers
{
	/// <summary>
	/// CurrencyDataRowValidator - makes sure a row is a valid currency
	/// </summary>
	public class CurrencyDataRowValidator : IDataRowValidator
	{
		#region Events

		/// <summary>
		/// Invalid Data Row Event
		/// </summary>
		public event InvalidDataRowDelegate InvalidDataRowEvent;

		#endregion

		#region Variables

		private CurrencyCollection m_allCurrencies ;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor which remembers the currencies.
		/// </summary>
		/// <param name="allCurrencies"></param>
		public CurrencyDataRowValidator(CurrencyCollection allCurrencies)
		{
			this.m_allCurrencies = allCurrencies;
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
			string currencyCode = dataRow[(int) ImportExchangeRateSqlPersister.CurrencyExchangeRateColumnPosition.CurrencyCode].ToString();
			Currency rowCurrency = new Currency(currencyCode);
			ValidationErrorSeverity returnSeverity = ValidationErrorSeverity.None;

			if (m_allCurrencies.Contains(rowCurrency) == false)
			{
				returnSeverity = ValidationErrorSeverity.High;

				if (this.InvalidDataRowEvent != null)
				{
					InvalidDataRowEvent(this, new InvalidDataRowEventArgs(dataRow, dataDefinition, String.Format("Currency Code value of {0} is not valid.", currencyCode), ValidationErrorSeverity.High));
				}
			}

			return returnSeverity;
		}

		#endregion
	}
}