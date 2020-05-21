using System;
using System.Data;

using HBOS.FS.AMP.Data.Types;
using HBOS.FS.AMP.Data.Validator.Interface;

namespace HBOS.FS.AMP.UPD.Controllers
{
	/// <summary>
	/// Validates that an Asset Fund / Currency combination is valid.
	/// </summary>
	public class AssetFundCurrencyDataRowValidator  : IDataRowValidator
	{
		#region Constructor

		/// <summary>
		/// Constructor which takes the valid Asset Fund / Currency combinations.
		/// </summary>
		public AssetFundCurrencyDataRowValidator( DataTable validAssetFundCurrencyCombinations )
		{
			m_allValidAssetFundCurrencyCombinations = validAssetFundCurrencyCombinations;
		}

		#endregion

		#region Events

		/// <summary>
		/// Invalid Data Row Event
		/// </summary>
		public event InvalidDataRowDelegate InvalidDataRowEvent;

		#endregion

		#region Variables

		private DataTable m_allValidAssetFundCurrencyCombinations ;

		/// <summary>
		/// Column Position in the asset fund index weightings file
		/// </summary>
		private enum AssetFundIndexWeightingColumnPosition : int
		{
			/// <summary>
			/// Asset Fund (0)
			/// </summary>
			AssetFund = 0,
			/// <summary>
			/// Currency Code (1)
			/// </summary>
			CurrencyCode,
			/// <summary>
			/// Value (2)
			/// </summary>
			Value
		}

		#endregion


		#region IDataRowValidator Members

		/// <summary>
		/// Validate the Asset Fund / CUrrency combination
		/// </summary>
		/// <param name="dataRow"></param>
		/// <param name="dataDefinition"></param>
		/// <returns></returns>
		public ValidationErrorSeverity Validate(string[] dataRow, System.Data.DataTable dataDefinition)
		{
			ValidationErrorSeverity returnSeverity = ValidationErrorSeverity.None;

			string assetFund = dataRow[ (int)AssetFundIndexWeightingColumnPosition.AssetFund ].ToString().Trim();
			string currencyCode = dataRow[ (int)AssetFundIndexWeightingColumnPosition.CurrencyCode ].ToString();

			// NOTE: The currency code is the file is REALLY a countryCode
			string filterClause = String.Format( "assetFundId = '{0}' AND countryCode = '{1}'" , assetFund , currencyCode );

			DataRow[] matchingRows = new DataRow[0]; 
			if (m_allValidAssetFundCurrencyCombinations!=null)
			{
				matchingRows=m_allValidAssetFundCurrencyCombinations.Select( filterClause );
			}

			if ( matchingRows.Length == 0 )
			{
				returnSeverity = ValidationErrorSeverity.High;

				if ( this.InvalidDataRowEvent != null )
				{
					InvalidDataRowEvent( this , new InvalidDataRowEventArgs( dataRow , dataDefinition , String.Format("Asset Fund {0} and Currency Code {1} is not a valid combination for the active company." , assetFund , currencyCode) , ValidationErrorSeverity.High ) );
				}
			}

			return returnSeverity;
		}

		#endregion
	}
}
