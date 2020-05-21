using System;
using System.Collections;
using System.Data;
using HBOS.FS.AMP.Data.Types;
using HBOS.FS.AMP.Data.Validator.Interface;
using HBOS.FS.AMP.UPD.Persistence;

namespace HBOS.FS.AMP.UPD.Controllers
{
	/// <summary>
	/// Hi3PricesAssetFundDataRowValidator - makes sure a row is a valid fund
	/// </summary>
	public class Hi3PricesAssetFundDataRowValidator : IDataRowValidator
	{
		#region Events

		/// <summary>
		/// Invalid Data Row Event
		/// </summary>
		public event InvalidDataRowDelegate InvalidDataRowEvent;

		#endregion

		#region Variables

		private Hashtable m_allAssetFunds;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor which remembers the market indices
		/// </summary>
		/// <param name="allAssetFunds"></param>
		public Hi3PricesAssetFundDataRowValidator(Hashtable allAssetFunds)
		{
			this.m_allAssetFunds = allAssetFunds;
		}

		#endregion

		#region IDataRowValidator Members

		/// <summary>
		/// Validate the Hi3 Price
		/// </summary>
		/// <param name="dataRow"></param>
		/// <param name="dataDefinition"></param>
		/// <returns></returns>
		public ValidationErrorSeverity Validate(string[] dataRow, DataTable dataDefinition)
		{
			ValidationErrorSeverity returnSeverity = ValidationErrorSeverity.None;
			string assetFundId = dataRow[(int) ImportHi3PricesSqlPersister.Hi3PricesColumnPosition.AssetFundId].ToString().Trim();

			if (m_allAssetFunds.Contains(assetFundId) == false)
			{
				returnSeverity = ValidationErrorSeverity.High;

				if (this.InvalidDataRowEvent != null)
				{
					InvalidDataRowEvent(this, new InvalidDataRowEventArgs(dataRow, dataDefinition, String.Format("Invalid Asset Fund value of {0}.", assetFundId), ValidationErrorSeverity.High));
				}
			}

			return returnSeverity;
		}

		#endregion
	}
}