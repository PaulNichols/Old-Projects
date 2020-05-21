using System;
using System.Data;
using HBOS.FS.AMP.Data.Types;
using HBOS.FS.AMP.Data.Validator.Interface;
using HBOS.FS.AMP.UPD.Persistence;

namespace HBOS.FS.AMP.UPD.Controllers
{
	/// <summary>
	/// Hi3PricesAssetUnitPriceDataRowValidator - makes sure each class / series has the same asset unit price
	/// </summary>
	public class Hi3PricesAssetUnitPriceDataRowValidator : IDataRowValidator
	{
		#region Events

		/// <summary>
		/// Invalid Data Row Event
		/// </summary>
		public event InvalidDataRowDelegate InvalidDataRowEvent;

		#endregion

		#region IDataRowValidator Members

		/// <summary>
		/// Validate the asset unit price
		/// </summary>
		/// <param name="dataRow"></param>
		/// <param name="dataDefinition"></param>
		/// <returns></returns>
		public ValidationErrorSeverity Validate(string[] dataRow, DataTable dataDefinition)
		{
			ValidationErrorSeverity returnSeverity = ValidationErrorSeverity.None;
			if (dataRow !=null && dataRow.Length!=0)
			{
				string assetUnitPriceSeries1 = dataRow[(int) ImportHi3PricesSqlPersister.Hi3PricesColumnPosition.AssetUnitPrice1].ToString();

				for (int seriesNumber = 2; seriesNumber <= 10; seriesNumber ++)
				{
					int startSeriesColumnNumber = ImportHi3PricesSqlPersister.CalculateSeriesStartColumnNumber(seriesNumber);
					

					string bidOrSingle = dataRow[ startSeriesColumnNumber ].ToString();
					string unrounderBidPrice = dataRow[ startSeriesColumnNumber + 1 ].ToString();
					string offerPrice = dataRow[ startSeriesColumnNumber + 3 ].ToString();
					string assetUnitPrice = dataRow[startSeriesColumnNumber+5].ToString();
					string valuationBasis = dataRow[ startSeriesColumnNumber + 13 ].ToString();

					// If we have a value for either, then we say that the price series should be processed
					//PJN CR25 allow zero price import
					 if (
							(( bidOrSingle != "" && decimal.Parse( bidOrSingle )!= 0) || (unrounderBidPrice != "" && decimal.Parse( unrounderBidPrice  )!= 0) || (offerPrice!= "" && decimal.Parse( offerPrice)!= 0))
							|| valuationBasis=="9"
						)
					{
					
						// See if this series matches series 1.
						if (assetUnitPriceSeries1 != assetUnitPrice && assetUnitPrice != String.Empty)
						{
							returnSeverity = ValidationErrorSeverity.High;

							if (this.InvalidDataRowEvent != null)
							{
								this.InvalidDataRowEvent(this, new InvalidDataRowEventArgs(dataRow, dataDefinition, String.Format("Asset Unit Prices are not all the same value (e.g {0} and {1}).", assetUnitPriceSeries1, assetUnitPrice), ValidationErrorSeverity.High));
							}
							break;
						}
					}
				}
			}
		else
			{
						throw new SystemException("The Data Row was incorrect.");
			}
			return returnSeverity;
		}

		#endregion
	}
}