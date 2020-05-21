using System;
using System.Collections;

using HBOS.FS.AMP.Data.Validator.Interface;
using HBOS.FS.AMP.Data.Types;

using HBOS.FS.AMP.UPD.Types.Funds;

namespace HBOS.FS.AMP.UPD.Controllers
{
	/// <summary>
	/// Hi3PricesAssetFundDataRowValidator - makes sure a row is a valid series 1
	/// </summary>
	public class Hi3PricesCompositeSeriesDataRowValidator : IDataRowValidator
	{
		#region Events

		/// <summary>
		/// Invalid Data Row Event
		/// </summary>
		public event InvalidDataRowDelegate InvalidDataRowEvent;

		#endregion

		#region Variables

		private bool m_fundIsAnOEIC;
		private int m_SeriesNumber = 0;
		private Hashtable m_allHiPortfolioCodes ;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor which remembers the market indices
		/// </summary>
		/// <param name="allHiPortfolioCodes"></param>
		public Hi3PricesCompositeSeriesDataRowValidator( Hashtable allHiPortfolioCodes  )
		{
			this.m_allHiPortfolioCodes = allHiPortfolioCodes;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Set the series number
		/// </summary>
		public int SeriesNumber
		{
			get
			{
				return m_SeriesNumber;
			}
			set
			{
				m_SeriesNumber = value;
			}
		}

		/// <summary>
		/// Access to the fund type
		/// </summary>
		public bool OEIC
		{
			get
			{
				return m_fundIsAnOEIC;
			}
			set
			{
				m_fundIsAnOEIC = value;
			}
		}

		#endregion

		#region IDataRowValidator Members

		/// <summary>
		/// Validate the series Number
		/// </summary>
		/// <param name="dataRow"></param>
		/// <param name="dataDefinition"></param>
		/// <returns></returns>
		public ValidationErrorSeverity Validate(string[] dataRow, System.Data.DataTable dataDefinition)
		{
			ValidationErrorSeverity returnSeverity = ValidationErrorSeverity.None;

			if (dataRow ==null || dataRow.Length==0)
			{
				// Figure out the hiPostfolioCode
				string assetFundId = dataRow[ (int)Persistence.ImportHi3PricesSqlPersister.Hi3PricesColumnPosition.AssetFundId ].ToString().Trim();
				string hiPortfolioCode = Persistence.ImportHi3PricesSqlPersister.CalculatePostfolioCode( assetFundId , m_SeriesNumber , m_fundIsAnOEIC );

				bool portfolioCodeInDatabase = m_allHiPortfolioCodes.Contains( hiPortfolioCode );
				bool portfolioCodeInFile = false;

				int startSeriesColumnNumber = Persistence.ImportHi3PricesSqlPersister.CalculateSeriesStartColumnNumber( m_SeriesNumber );
			
				string bidOrSingle = dataRow[ startSeriesColumnNumber ].ToString();
				string unrounderBidPrice = dataRow[ startSeriesColumnNumber + 1 ].ToString();

				// If we have a value for either, then we say the portfolio code is in the file
				if ( bidOrSingle != "" || unrounderBidPrice != "" )
				{
					portfolioCodeInFile = true;
				}


				// If the code is in the database but not in the file, then it is a validation error
				if ( portfolioCodeInDatabase == true && portfolioCodeInFile == false )
				{
					returnSeverity = ValidationErrorSeverity.High;

					if ( this.InvalidDataRowEvent != null )
					{
						InvalidDataRowEvent( this , new InvalidDataRowEventArgs( dataRow , dataDefinition , String.Format("Portfolio Code {0} is in the database but not in the file." , hiPortfolioCode ) , ValidationErrorSeverity.High ) );
					}
				}

				// If the code is not in the database, but is in the file
				if ( portfolioCodeInDatabase == false && portfolioCodeInFile == true )
				{
					returnSeverity = ValidationErrorSeverity.High;

					if ( this.InvalidDataRowEvent != null )
					{
						InvalidDataRowEvent( this , new InvalidDataRowEventArgs( dataRow , dataDefinition , String.Format("Portfolio Code {0} is in the file but not in the database."  , hiPortfolioCode ) , ValidationErrorSeverity.High ) );
					}
				}

				// If the code is not in the database, and its not in the file
				if ( portfolioCodeInDatabase == false && portfolioCodeInFile == false )
				{
					returnSeverity = ValidationErrorSeverity.Low;
				}

				// If the code is in the database and the file and has a status of authorised or higher
				if ( portfolioCodeInDatabase == true && portfolioCodeInFile == true &&
					((Fund)m_allHiPortfolioCodes[hiPortfolioCode]).FundStatus >= Fund.FundStatusType.SecondLevelAuthorised)
				{
					returnSeverity = ValidationErrorSeverity.High;

					if ( this.InvalidDataRowEvent != null )
					{
						InvalidDataRowEvent( this , new InvalidDataRowEventArgs( dataRow , dataDefinition , String.Format("Portfolio Code {0} already has an authorised price."  , hiPortfolioCode ) , ValidationErrorSeverity.High ) );
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
