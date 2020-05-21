using System;

using HBOS.FS.AMP.UPD.Types.AssetFunds;

using HBOS.FS.AMP.Data.Types;
using HBOS.FS.AMP.Data.Validator.Interface;

namespace HBOS.FS.AMP.UPD.Controllers
{
	/// <summary>
	/// MarketIndexDataRowValidator - makes sure a row is a valid market index
	/// </summary>
	public class MarketIndexDataRowValidator : IDataRowValidator
	{
		#region Events

		/// <summary>
		/// Invalid Data Row Event
		/// </summary>
		public event InvalidDataRowDelegate InvalidDataRowEvent;

		#endregion

		#region Variables

		private StockMarketCollection m_allStockMarkets ;

		/// <summary>
		/// Column Position in the market indices file
		/// </summary>
		private enum MarketIndicesColumnPosition : int
		{
			/// <summary>
			/// Market Name (0)
			/// </summary>
			MarketName = 0,
			/// <summary>
			/// Market Value (1)
			/// </summary>
			MarketValue
		}

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor which remembers the market indices
		/// </summary>
		/// <param name="allStockMarkets"></param>
		public MarketIndexDataRowValidator( StockMarketCollection allStockMarkets )
		{
			this.m_allStockMarkets = allStockMarkets;
		}

		#endregion

		#region IDataRowValidator Members

		/// <summary>
		/// Validate the Market Rate record
		/// </summary>
		/// <param name="dataRow"></param>
		/// <param name="dataDefinition"></param>
		/// <returns></returns>
		public ValidationErrorSeverity Validate(string[] dataRow, System.Data.DataTable dataDefinition)
		{
			ValidationErrorSeverity returnSeverity = ValidationErrorSeverity.None;
			if (dataRow !=null && dataRow.Length!=0)
			{
				string marketName = dataRow[ (int)MarketIndicesColumnPosition.MarketName ].ToString();
				StockMarket rowStockMarket = new StockMarket( String.Empty , marketName , 0 ,false,new byte[1]);
				

				if ( m_allStockMarkets.Contains( rowStockMarket ) == false )
				{
					returnSeverity = ValidationErrorSeverity.High;

					if ( this.InvalidDataRowEvent != null )
					{
						InvalidDataRowEvent( this , new InvalidDataRowEventArgs( dataRow , dataDefinition , String.Format("Market Index of {0} is not valid." , marketName) , ValidationErrorSeverity.High ) );
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