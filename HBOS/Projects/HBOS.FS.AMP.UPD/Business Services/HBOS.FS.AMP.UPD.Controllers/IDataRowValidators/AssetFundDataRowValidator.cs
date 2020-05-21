using System;
using System.Collections;
using System.Data;
using HBOS.FS.AMP.Data.Types;
using HBOS.FS.AMP.Data.Validator.Interface;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.Funds;

namespace HBOS.FS.AMP.UPD.Controllers
{
	/// <summary>
	/// Hi3PricesAssetFundDataRowValidator - makes sure a row is a valid fund
	/// </summary>
	public class AssetFundDataRowValidator : IDataRowValidator
	{
		#region Events

		/// <summary>
		/// Invalid Data Row Event
		/// </summary>
		public event InvalidDataRowDelegate InvalidDataRowEvent;

		#endregion

		#region Variables

		private Hashtable m_allAssetFunds;
		private readonly int m_assetFundColumnPosition;
		private readonly ImportController.ImportFileType m_ImportType;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor which remembers the market indices
		/// </summary>
		/// <param name="allAssetFunds">All asset funds expected or could be valid for the type of import</param>
		/// <param name="assetFundColumnPosition">The position of the Asset Fund Id in the current file</param>
		/// <param name="importType">type of import to validate against</param>
		public AssetFundDataRowValidator(Hashtable allAssetFunds,int assetFundColumnPosition,ImportController.ImportFileType importType)
		{
			this.m_allAssetFunds = allAssetFunds;
			this.m_assetFundColumnPosition = assetFundColumnPosition;
			this.m_ImportType = importType;
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
			string assetFundId = dataRow[m_assetFundColumnPosition].ToString().Trim();

			if (! m_allAssetFunds.Contains(assetFundId) )
			{
				returnSeverity = ValidationErrorSeverity.High;

				if (this.InvalidDataRowEvent != null)
				{
					InvalidDataRowEvent(this, new InvalidDataRowEventArgs(dataRow, dataDefinition, String.Format("Asset Fund {0} was not expected on this file.", assetFundId), ValidationErrorSeverity.High));
				}
			}
			else
			{
				if (
					(m_ImportType==ImportController.ImportFileType.Hi3Prices &&  !(m_allAssetFunds[assetFundId] is OEICFund)) ||
					(m_ImportType==ImportController.ImportFileType.Hi3PricesComposite && !(m_allAssetFunds[assetFundId] is Composite)) ||
					(m_ImportType==ImportController.ImportFileType.Hi3PricesLinked && !(m_allAssetFunds[assetFundId] is LinkedFund)) 
					)
				{
					returnSeverity = ValidationErrorSeverity.High;

					if (this.InvalidDataRowEvent != null)
					{
						InvalidDataRowEvent(this, new InvalidDataRowEventArgs(dataRow, dataDefinition, String.Format("Asset Fund {0} is not of the expected type.", assetFundId), ValidationErrorSeverity.High));
					}
				}
			}

			return returnSeverity;
		}

		#endregion
	}
}