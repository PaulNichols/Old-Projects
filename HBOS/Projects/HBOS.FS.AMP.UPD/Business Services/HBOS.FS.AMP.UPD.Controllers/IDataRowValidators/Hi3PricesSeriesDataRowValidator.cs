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
	public class Hi3InitialPricesSeriesDataRowValidator : Hi3PricesSeriesDataRowValidator
	{

		/// <summary>
		/// Creates a new <see cref="Hi3InitialPricesSeriesDataRowValidator"/> instance.
		/// </summary>
		/// <param name="importFileType"> The type of import being attempted and validated</param>
		/// <param name="allHiPortfolioCodes">All hi portfolio codes.</param>
		public Hi3InitialPricesSeriesDataRowValidator( Hashtable allHiPortfolioCodes ,ImportController.ImportFileType  importFileType):base(allHiPortfolioCodes,importFileType)
		{
		}

		/// <summary>
		/// Validate the series Number
		/// </summary>
		/// <param name="dataRow"></param>
		/// <param name="dataDefinition"></param>
		/// <returns></returns>
		public override ValidationErrorSeverity Validate(string[] dataRow, System.Data.DataTable dataDefinition)
		{
			ValidationErrorSeverity returnSeverity = ValidationErrorSeverity.None;

			// Figure out the hiPostfolioCode
			string assetFundId = dataRow[ (int)Persistence.ImportHi3PricesSqlPersister.Hi3PricesColumnPosition.AssetFundId ].ToString().Trim();
			string hiPortfolioCode = Persistence.ImportHi3PricesSqlPersister.CalculatePortfolioCode( assetFundId , m_SeriesNumber , m_fundIsAnOEIC );

			bool portfolioCodeInDatabase = m_allHiPortfolioCodes.Contains( hiPortfolioCode );
			bool portfolioCodeInFile = false;

			int startSeriesColumnNumber = Persistence.ImportHi3PricesSqlPersister.CalculateSeriesStartColumnNumber( m_SeriesNumber );
			
			string bidOrSingle = dataRow[ startSeriesColumnNumber ].ToString();
			string unrounderBidPrice = dataRow[ startSeriesColumnNumber + 1 ].ToString();
			string offerPrice = dataRow[ startSeriesColumnNumber + 3 ].ToString();

			
			// If we have a value for either, then we say the portfolio code is in the file
			if (( bidOrSingle != "" && decimal.Parse( bidOrSingle )!= 0) || (unrounderBidPrice != "" && decimal.Parse( unrounderBidPrice  )!= 0) || (offerPrice!= "" && decimal.Parse( offerPrice)!= 0))
			{
				portfolioCodeInFile = true;
			}
			else if (( bidOrSingle == "" || decimal.Parse( bidOrSingle )== 0) && (unrounderBidPrice == "" || decimal.Parse( unrounderBidPrice  )== 0) && (offerPrice== "" || decimal.Parse( offerPrice)== 0))
			{
				//this means there is no initial information
				//returnSeverity = ValidationErrorSeverity.High;
			}

			// If the code is in the database but not in the file, then it is a validation error
			if ( portfolioCodeInDatabase == true && portfolioCodeInFile == false )
			{
				returnSeverity = ValidationErrorSeverity.High;

				OnInvalidDataRowEvent(String.Format("Portfolio Code {0} is in the database but not in the file." , hiPortfolioCode ),dataRow,  dataDefinition);
			}

			// If the code is not in the database, but is in the file
			if ( portfolioCodeInDatabase == false && portfolioCodeInFile == true )
			{
				returnSeverity = ValidationErrorSeverity.High;

				OnInvalidDataRowEvent(String.Format("Portfolio Code {0} is in the file but not in the database." , hiPortfolioCode ),dataRow,  dataDefinition);
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

				OnInvalidDataRowEvent(String.Format("Portfolio Code {0} already has an authorised price." , hiPortfolioCode ),dataRow,  dataDefinition);
			}

			return returnSeverity;
		}
	}


	/// <summary>
	/// Hi3PricesAssetFundDataRowValidator - makes sure a row is a valid series 1
	/// </summary>
	public class Hi3PricesSeriesDataRowValidator : IDataRowValidator
	{
		#region Events

		/// <summary>
		/// Invalid Data Row Event
		/// </summary>
		public event InvalidDataRowDelegate InvalidDataRowEvent;

		#endregion

		#region Variables

		/// <summary>
		/// 
		/// </summary>
		protected bool m_fundIsAnOEIC;
		/// <summary>
		/// 
		/// </summary>
		protected int m_SeriesNumber = 0;
		/// <summary>
		/// 
		/// </summary>
		protected Hashtable m_allHiPortfolioCodes = null;

		private ImportController.ImportFileType m_importType;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor which remembers the market indices
		/// </summary>
		/// <param name="allHiPortfolioCodes"></param>
		/// <param name="importType">The type of import being attempted and validated</param>
		public Hi3PricesSeriesDataRowValidator( Hashtable allHiPortfolioCodes ,ImportController.ImportFileType importType)
		{
			this.m_allHiPortfolioCodes = allHiPortfolioCodes;
			m_importType=importType;
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


		#endregion

		/// <summary>
		/// Ons the invalid data row event.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="dataRow">Data row.</param>
		/// <param name="dataDefinition">Data definition.</param>
		protected void OnInvalidDataRowEvent(string message,string[] dataRow ,   System.Data.DataTable dataDefinition)
		{
			if ( this.InvalidDataRowEvent != null )
			{
				InvalidDataRowEvent( this , new InvalidDataRowEventArgs( dataRow , dataDefinition , message , ValidationErrorSeverity.High ) );
			}
		}


		#region IDataRowValidator Members

		/// <summary>
		/// Validate the series Number
		/// </summary>
		/// <param name="dataRow"></param>
		/// <param name="dataDefinition"></param>
		/// <returns></returns>
		public virtual ValidationErrorSeverity Validate(string[] dataRow, System.Data.DataTable dataDefinition)
		{
			ValidationErrorSeverity returnSeverity = ValidationErrorSeverity.None;

			if (dataRow !=null && dataRow.Length!=0)
			{
				
				// Figure out the hiPostfolioCode
				string assetFundId = dataRow[ (int)Persistence.ImportHi3PricesSqlPersister.Hi3PricesColumnPosition.AssetFundId ].ToString().Trim();
				string hiPortfolioCode = Persistence.ImportHi3PricesSqlPersister.CalculatePortfolioCode( assetFundId , m_SeriesNumber , m_fundIsAnOEIC );

				bool portfolioCodeInDatabase = m_allHiPortfolioCodes.Contains( hiPortfolioCode );
				bool portfolioCodeInFile = false;

				int startSeriesColumnNumber = Persistence.ImportHi3PricesSqlPersister.CalculateSeriesStartColumnNumber( m_SeriesNumber );
			
				string bidOrSingle = dataRow[ startSeriesColumnNumber ].ToString();
				string unrounderBidPrice = dataRow[ startSeriesColumnNumber + 1 ].ToString();
				string valuationBasis = dataRow[ startSeriesColumnNumber + 13 ].ToString();

				// If we have a value for either, then we say the portfolio code is in the file
				//PJN CR25 allow zero price import
				 if (
					 (( bidOrSingle != "" && decimal.Parse( bidOrSingle )!= 0) || (unrounderBidPrice != "" && decimal.Parse( unrounderBidPrice  )!= 0) )
					 || valuationBasis == "9"
					 )
				{
					portfolioCodeInFile = true;
				}


				// If the code is in the database but not in the file, then it is a validation error
				if ( portfolioCodeInDatabase == true && portfolioCodeInFile == false )
				{
					returnSeverity = ValidationErrorSeverity.High;

					OnInvalidDataRowEvent(String.Format("Portfolio Code {0} was expected in this file." , hiPortfolioCode ),dataRow,  dataDefinition);
				}

				// If the code is not in the database, but is in the file
				if ( portfolioCodeInDatabase == false && portfolioCodeInFile == true )
				{
					returnSeverity = ValidationErrorSeverity.High;

					OnInvalidDataRowEvent(String.Format("Portfolio Code {0} is in the file but was not expected or does not exist."  , hiPortfolioCode ),dataRow,  dataDefinition);
				}

				// If the code is not in the database, and its not in the file
				if ( portfolioCodeInDatabase == false && portfolioCodeInFile == false )
				{
					returnSeverity = ValidationErrorSeverity.Low;
				}

				// If the code is not in the database, and its not in the file
				if ( portfolioCodeInDatabase == false && portfolioCodeInFile == false )
				{
					returnSeverity = ValidationErrorSeverity.Low;
				}

				// If the code is in the database and the file and has a status of authorised or higher
				if (m_importType!=ImportController.ImportFileType.PriceComparisionPrices)
				{
					if ( portfolioCodeInDatabase == true && portfolioCodeInFile == true &&
						((Fund)m_allHiPortfolioCodes[hiPortfolioCode]).FundStatus >= Fund.FundStatusType.SecondLevelAuthorised)
					{
						returnSeverity = ValidationErrorSeverity.High;

						OnInvalidDataRowEvent(String.Format("Portfolio Code {0} already has an authorised price." , hiPortfolioCode ),dataRow,  dataDefinition);
					}
				}
			}
			else
			{
				throw new SystemException("The Data Row was incorrect.");
			}
			return returnSeverity;
		}

		/// <summary>
		/// Gets or sets a value indicating whether [OEIC fund].
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [OEIC fund]; otherwise, <c>false</c>.
		/// </value>
		public bool OEICFund
		{
			get{return m_fundIsAnOEIC;}
			set { m_fundIsAnOEIC=value; }
		}

		#endregion
	}
}