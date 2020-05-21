using System;
using System.Collections;
using System.Data;
using HBOS.FS.AMP.Data.Types;
using HBOS.FS.AMP.Data.Validator.Interface;
using HBOS.FS.AMP.UPD.Persistence.IDataRowPersisters;
using HBOS.FS.AMP.UPD.Types.Funds;

namespace HBOS.FS.AMP.UPD.Controllers
{
	/// <summary>
	/// CompositeSplitFundDataRowValidator - makes sure a row's Composite Asset 
	/// Fund Exists in list of Asset Funds for the company, also the linked fund 
	/// is a valid fund for the company and finally the value is a decimal and >=0
	/// </summary>
	public class CompositeSplitFundDataRowValidator : IDataRowValidator
	{
		#region Events

		/// <summary>
		/// Invalid Data Row Event
		/// </summary>
		public event InvalidDataRowDelegate InvalidDataRowEvent;

		#endregion

		#region Variables

		//variable to hold the companies asset funds to validate against
		private Hashtable m_companyAssetFunds = null;
		//variable to hold the companies funds to validate against
		private Hashtable m_companySecurityCode = null;

		/// <summary>
		/// Column Position in the composite import file
		/// </summary>
		internal enum CompositeLinkedFundColumnPosition : int
		{
			/// <summary>
			/// Asset Fund (0)
			/// </summary>
			AssetFund = 0,
			/// <summary>
			/// LinkedFund Code (1)
			/// </summary>
			LinkedFundCode,
			/// <summary>
			/// Value (2)
			/// </summary>
			Value
		}

		#endregion

		#region Constructor

		/// <summary>
		/// 
		/// </summary>
		/// <param name="companyAssetFunds">All Asset Funds for the current company</param>
		/// <param name="companyFunds">All Funds for the current company</param>
		public CompositeSplitFundDataRowValidator(Hashtable companyAssetFunds, Hashtable companyFunds)
		{
			this.m_companyAssetFunds = companyAssetFunds;
			this.m_companySecurityCode = companyFunds;
		}

		#endregion

		#region IDataRowValidator Members

		/// <summary>
		/// Validate the Composite Fund
		/// </summary>
		/// <param name="dataRow"></param>
		/// <param name="dataDefinition"></param>
		/// <returns></returns>
		public ValidationErrorSeverity Validate(string[] dataRow, DataTable dataDefinition)
		{
			//set the severity to none to start with
			ValidationErrorSeverity returnSeverity = ValidationErrorSeverity.None;
			//read out the value for the asset fund for this row
			string assetFundId = dataRow[(int) ImportWorkingSplitTableSqlPersister.ColumnPosition.primaryKey].ToString().Trim();

			//check that the collection of asset funds for the company contains this rows asset fund value
			if (m_companyAssetFunds.Contains(assetFundId) == false)
			{
				//if the asset fund is not valid the set the severity to high
				returnSeverity = ValidationErrorSeverity.High;

				//raise the event to say we have an invalid row
				if (this.InvalidDataRowEvent != null)
				{
					InvalidDataRowEvent(this, new InvalidDataRowEventArgs(dataRow, dataDefinition, String.Format("Invalid Composite Asset Fund for this Company, value of {0}. The Composite and it's linked funds will not be imported.", assetFundId), ValidationErrorSeverity.High));
				}
			}


			//read out the value for the linked fund for this row
			string securityCode = dataRow[(int) ImportWorkingSplitTableSqlPersister.ColumnPosition.secondaryKey].ToString().Trim().ToUpper();

			//check that the collection of asset funds for the company contains this rows asset fund value
			if (m_companySecurityCode.Contains(securityCode) == false || FundFactory.ResolveFundType((Fund) m_companySecurityCode[securityCode]) != FundFactory.FundType.Linked)
			{
				//if the linked fund is not valid the set the severity to high
				returnSeverity = ValidationErrorSeverity.High;

				//raise the event to say we have an invalid row
				if (this.InvalidDataRowEvent != null)
				{
					InvalidDataRowEvent(this, new InvalidDataRowEventArgs(dataRow, dataDefinition, String.Format("Invalid Linked Fund Security Code for this Company, value of {0}. Or the Fund is not a Linked Fund. The Composite and any other associated Linked funds will not be imported", securityCode), ValidationErrorSeverity.High));
				}
			}
			else
			{
				Fund fund=(Fund) m_companySecurityCode[securityCode];
				if (!fund.IsBenchMarkable)
				{
				
					//if the fund is not expected as a benchmark then set the severity to high
					returnSeverity = ValidationErrorSeverity.High;

					//raise the event to say we have an invalid row
					if (this.InvalidDataRowEvent != null)
					{
						InvalidDataRowEvent(this, new InvalidDataRowEventArgs(dataRow, dataDefinition, String.Format("The Fund {0} has not been marked as Benchmarkable, the split will not be imported for Asset Fund {1}.", fund.HiPortfolioCode.Trim(),assetFundId.Trim()), ValidationErrorSeverity.High));
					}
				}
			}
		
			//read out the value for the asset fund for this row
			string importvalue = dataRow[(int) ImportWorkingSplitTableSqlPersister.ColumnPosition.importedValue].ToString().Trim();

			try
			{
				//parse the value to see if it is decimal and also it is less than 0
				if (decimal.Parse(importvalue) < 0)
				{
					//if the value is less than 0 then set severity 
					//to high and raise an event to say we have an invalid row
					returnSeverity = ValidationErrorSeverity.High;

					if (this.InvalidDataRowEvent != null)
					{
						InvalidDataRowEvent(this, new InvalidDataRowEventArgs(dataRow, dataDefinition, String.Format("The value for the Linked fund is {0} but must be greater than 0.", importvalue), ValidationErrorSeverity.High));
					}
				}
			}
			catch (FormatException ex)
			{
				//if the vaue is not a valid decimal then raise an event and set the severity to high
				returnSeverity = ValidationErrorSeverity.High;

				if (this.InvalidDataRowEvent != null)
				{
					InvalidDataRowEvent(this, new InvalidDataRowEventArgs(dataRow, dataDefinition, ex.Message, ValidationErrorSeverity.High));
				}
			}

			return returnSeverity;
		}

		#endregion
	}
}