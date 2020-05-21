using System;
using System.Collections;
using System.Data.SqlClient;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Persistence;
using HBOS.FS.AMP.UPD.Types.Funds;
using HBOS.FS.AMP.UPD.Types.Lookups;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Controllers
{
	/// <summary>
	/// Controller object for all things Fund orientated.  This is used to
	/// keep the logical layers seperate.  
	/// The controller will handle calls to the business and data layers.
	/// </summary>
	public class FundController
	{
		private string m_connectionString;

		#region Constructor

		/// <summary>
		/// Default constructor for Fund controller
		/// </summary>
		public FundController()
		{
		}

		/// <summary>
		/// Constructor using the connection string.
		/// </summary>
		/// <remarks>This is the preferred constructor for MVS Drop II</remarks>
		/// <param name="ConnectionString">The application connection string.</param>
		public FundController(string ConnectionString)
		{
			// Build valid connection string
			m_connectionString = ConnectionString;
		}

		#endregion

		#region Drop II

		#region Member variables

		//cache this in order to generally be more efficient, but 
		//also to ensure holidays are cached and not loaded each time

		private FundPersister m_fundPersister = null;

		#endregion

		#region Load methods

		/// <summary>
		/// Return a partially populated fund matching the given HiPortfolioCode.
		/// The fund object will contain factors, tolerances and fund groups of type 'Fund'.
		/// </summary>
		/// <param name="fundCode">The code for the required fund</param>
		/// <returns>A single Fund object</returns>
		/// <remarks>This method will be used to populate the fund static data maintenance screen.</remarks>
		/// <exception cref="DatabaseException">Unable to load funds</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		public Fund LoadStaticData(string fundCode)
		{
			T.E();
			Fund fund = null;

			try
			{
				if (m_fundPersister == null || (!(m_fundPersister is FundStaticDataPersister)))
				{
					m_fundPersister = new FundStaticDataPersister(m_connectionString);
				}
				fund = ((FundStaticDataPersister) m_fundPersister).Load(fundCode);
			}
			finally
			{
				T.X();
			}

			return fund;
		}

		/// <summary>
		/// Load all fund full details for company - required for static data export
		/// </summary>
		/// <param name="companyCode"></param>
		/// <returns></returns>
		public FundCollection LoadFundsByCompany(string companyCode)
		{
			T.E();
			try
			{
				if (m_fundPersister == null || (!(m_fundPersister is FundStaticDataPersister)))
				{
					m_fundPersister = new FundStaticDataPersister(m_connectionString);
				}
				return ((FundStaticDataPersister) m_fundPersister).LoadForCompany(companyCode);
			}
			finally
			{
				T.X();
			}
		}
		/// <summary>
		/// Return a collection of partially populated fund object for the given Company Code.
		/// </summary>
		/// <param name="companyCode">The Company code for the required fund Collection</param>
		/// <param name="extension">Extension of file to filter by</param>
		/// <param name="fileName">File name to filter by</param>
		/// <returns>A fund collection</returns>
		public FundCollection LoadPartialFundsForCompanyByFile(string companyCode,string fileName,string extension)
		{
			T.E();
			FundCollection fundCollection = null;

			try
			{
				if (m_fundPersister == null || (!(m_fundPersister is ImportFundPricePersister)))
				{
					m_fundPersister = new ImportFundPricePersister(m_connectionString);
				}
				// Load the fund collection for the required company code
				T.Log("Load fund collection for the company = " + companyCode);
				fundCollection = ((ImportFundPricePersister) m_fundPersister).Load(companyCode,fileName,extension);
				T.Log("Successfully loaded the fund collection for the company = " + companyCode);
			}
			finally
			{
				T.X();
			}

			return fundCollection;
		}


		/// <summary>
		/// Return a collection of partially populated fund object for the given Company Code.
		/// </summary>
		/// <param name="companyCode">The Company code for the required fund Collection</param>
		/// <returns>A fund collection</returns>
		public FundCollection LoadPartialFundsForCompany(string companyCode)
		{
			T.E();
			FundCollection fundCollection = null;

			try
			{
				if (m_fundPersister == null || (!(m_fundPersister is ImportFundPricePersister)))
				{
					m_fundPersister = new ImportFundPricePersister(m_connectionString);
				}
				// Load the fund collection for the required company code
				T.Log("Load fund collection for the company = " + companyCode);
				fundCollection = ((ImportFundPricePersister) m_fundPersister).Load(companyCode);
				T.Log("Successfully loaded the fund collection for the company = " + companyCode);
			}
			finally
			{
				T.X();
			}

			return fundCollection;
		}

		/// <summary>
		/// Return a collection of all partially populated fund objects .
		/// </summary>
		/// <returns>A fund collection</returns>
		public FundCollection LoadAllPartialFunds()
		{
			T.E();
			FundCollection fundCollection = null;

			try
			{
				if (m_fundPersister == null || (!(m_fundPersister is ImportFundPricePersister)))
				{
					m_fundPersister = new ImportFundPricePersister(m_connectionString);
				}
				// Load the fund collection for the required company code
				T.Log("Load fund collection");
				fundCollection = ((ImportFundPricePersister) m_fundPersister).Load();
				T.Log("Successfully loaded the fund collection ");
			}
			finally
			{
				T.X();
			}

			return fundCollection;
		}

		/// <summary>
		/// Loads the fund group lookups by company.
		/// </summary>
		/// <param name="companyCode">Company code.</param>
		/// <returns></returns>
		public SimpleStringLookupCollection LoadFundLookupsByCompany(string companyCode)
		{
			T.E();
			FundLookUpPersister persister = new FundLookUpPersister(m_connectionString);
			SimpleStringLookupCollection lookup = persister.LoadForCompany(companyCode);
			T.X();
			return lookup;
		}

		/// <summary>
		/// Returns all the funds for the given fund group that have a status 
		/// lower than Released.  The ProgressStatus property is set according 
		/// to the fund status.
		/// </summary>
		/// <param name="fundGroupID">The current fund group ID.</param>
		/// <returns>
		/// A fund collection for the fund group but with any funds with 
		/// an inappropriate status removed.
		/// </returns>
		/// <remarks>
		/// Use this method for the second level authorised screen.
		/// </remarks>
		/// <exception cref="DatabaseException">Unable to load funds</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		public FundCollection LoadAuthorisedPrices(int fundGroupID)
		{
			T.E();

			FundCollection authorisablePrices = null;
			try
			{
				// Extract all funds for the given fund group
				FundCollection funds = this.LoadForFundGroup(fundGroupID);

				if (funds != null)
				{
					// Step through the fund collection and pull out authorisable funds
					for (int count = 0; count < funds.Count; count++)
					{
						Fund fund = funds[count];

						// Ignore released funds
						if (fund.FundStatus < Fund.FundStatusType.Released)
						{
							// Do we have an authorisable prices object?
							if (authorisablePrices == null)
							{
								authorisablePrices = new FundCollection();
							}

							// Add fund to authorisable fund collection
							authorisablePrices.Add(fund);
						}
					}
				}
			}
			finally
			{
				T.X();
			}

			return authorisablePrices;
		}

		/// <summary>
		/// Return all the funds for the fund group.
		/// </summary>
		/// <param name="fundGroupID">The current fund group ID.</param>
		/// <returns>All funds for the fund group.</returns>
		/// <exception cref="DatabaseException">Unable to load funds</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		public FundCollection LoadForFundGroup(int fundGroupID)
		{
			T.E();
			FundCollection funds = null;
			try
			{
				FundPricingPersister persister = new FundPricingPersister(m_connectionString);
				funds = persister.Load(fundGroupID);
			}
			finally
			{
				T.X();
			}
			return funds;
		}

		/// <summary>
		/// Return all the funds for the given company.
		/// </summary>
		/// <param name="companyCode">The code for the current company.</param>
		/// <returns>The fund list for the company.</returns>
		/// <exception cref="DatabaseException">Unable to load funds</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		public FundCollection LoadForCompany(string companyCode)
		{
			T.E();
			FundCollection funds = null;
			try
			{
				FundPricingPersister persister = new FundPricingPersister(m_connectionString);
				funds = persister.Load(companyCode);
			}
			finally
			{
				T.X();
			}
			return funds;

		}

		#endregion

		#region Update methods

		/// <summary>
		/// Save modified fund details through the persistance layer
		/// </summary>
		/// <param name="updateFund">Fund object containing modified fund details</param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public void UpdateStaticData(Fund updateFund)
		{
			T.E();
			try
			{
				SqlTransaction newTrans;

				// Establish connection
				// This will generate its own internal FINALLY statement which will dispose
				// of the connection correctly, thus reducing our own code!
				using (SqlConnection newConnection = new SqlConnection(m_connectionString))
				{
					// Establish the transaction object
					newConnection.Open();
					T.Log("Begin transaction");
					newTrans = newConnection.BeginTransaction();
					try
					{
						// Persist the fund information back to the database.
						// Note. SaveFund() will persist the factors; associated fund groups; external ids
						FundStaticDataPersister persister = new FundStaticDataPersister(m_connectionString);
						if (persister != null)
						{
							T.Log("Update fund");
							persister.SaveFund(updateFund, newTrans);
						}

						// Now, commit changes through to the database
						T.Log("Commit transaction");
						newTrans.Commit();
					}
					catch (DatabaseException)
					{
						// Attempt to rollback the transaction
						try
						{
							T.Log("Rollback transaction");
							newTrans.Rollback();
							throw; //  This will simply send the database exception back to the client
						}
						catch (SqlException sqlEx)
						{
							// Trap any rollBack exceptions
							if (null != newTrans.Connection)
							{
								string messageText = "An exception of type {0} was encountered while attempting to roll back the transaction.";
								throw new TransactionRollbackException(String.Format(messageText, sqlEx.GetType()), "State = " + newTrans.Connection.State.ToString(), sqlEx);
							}
						}
					}
				}
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Update the fund status to the next level for any funds that have had their ProgressStatus
		/// flag set.
		/// </summary>
		/// <param name="funds">The collection of edited funds.</param>
		/// <param name="progression">The status that the affected funds are to be progressed to.</param>
		/// <exception cref="DatabaseException">Unable to load funds</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		public void ProgressFunds(FundCollection funds, Fund.FundStatusType progression)
		{
			T.E();
			try
			{
				if (funds != null)
				{
					switch (progression)
					{
						case Fund.FundStatusType.SecondLevelAuthorised:
							AuthorisedFundPricePersister authorisePersister
								= new AuthorisedFundPricePersister(m_connectionString);
							authorisePersister.Save(funds);
							break;
						case Fund.FundStatusType.Released:
							ReleasePricePersister releasePersister = new ReleasePricePersister(m_connectionString);
							releasePersister.Save(funds);
							break;
						default:
							throw new NotImplementedException("Invalid fund status specified for progressing funds.");
					}
				}
			}
			finally
			{
				T.X();
			}
		}

		#endregion

		#region Validation methods

		/// <summary>
		/// Indicates what type of error occurred. Used for most fields as is generic
		/// </summary>
		public enum FundValidationError
		{
			/// <summary>
			/// no error occurred
			/// </summary>
			NoError,
			/// <summary>
			/// Required HiportCode missing
			/// </summary>
			FundFieldEmptyHiPortfolioCode,
			/// <summary>
			/// The fund is being used as a benchmark in the system so it 
			/// cannot currently be 
			/// </summary>
			FundCannotUnSelectBenchMark,
			/// <summary>
			/// No Fund Type has been selected (OEIC, Linked or Composite)
			/// </summary>
			FundTypeNotSelected,
			/// <summary>
			/// The required Full Name value is missing
			/// </summary>
			FundFieldEmptyFullName,
			/// <summary>
			/// The required Short Name value is missing
			/// </summary>
			FundFieldEmptyShortName,
			/// <summary>
			/// The Hiport code should be unique
			/// </summary>
			FundDuplicateFieldHiPortCode,
			/// <summary>
			/// The security code value must be unique
			/// </summary>
			FundDuplicateFieldSecurityCode,
			/// <summary>
			/// The full name value must be unique
			/// </summary>
			FundDuplicateFieldFullName,
			/// <summary>
			/// The short name value must be unique
			/// </summary>
			FundDuplicateFieldShortName,
			/// <summary>
			/// An asset fund must be specified for the fund
			/// </summary>
			FundFieldEmptyAssetFundID,
			/// <summary>
			/// /// The required Class Or Price Series value is missing
			/// </summary>
			FundFieldEmptyClassOrPriceSeries,
			/// <summary>
			/// The value entered for the Class Or Price Series is invalid
			/// </summary>
			FundInvalidCodeClassOrPriceSeries,
			/// <summary>
			/// An external systems does not have an identifier
			/// </summary>
			FundFieldEmptyExternalSystemId,
			/// <summary>
			/// The upper tolerance cannot be higher than the upper
			/// </summary>
			FundUpperToleranceLessThanLowerTolerance,
			/// <summary>
			/// The X Factor failed validation
			/// </summary>
			FundXFactorInvalid,
			/// <summary>
			/// The TPE is not within boundry values
			/// </summary>
			FundTPEInvalid,
			/// <summary>
			/// The TPE is not within  boundry values
			/// </summary>
			FundRevaluationFactorInvalid,
			/// <summary>
			/// The scaling factor is not within  boundry values
			/// </summary>
			FundScalingFactorInvalid,
			/// <summary>
			/// 
			/// </summary>
			FundUpperToleranceZero,
			/// <summary>
			/// 
			/// </summary>
			FundUpperToleranceNegative,
			/// <summary>
			/// 
			/// </summary>
			FundMaxUpperToleranceExceeded,
			/// <summary>
			/// 
			/// </summary>
			FundInvalidNumDecimalPlacesUpperTolerance,
			/// <summary>
			/// 
			/// </summary>
			FundLowerToleranceZero,
			/// <summary>
			/// 
			/// </summary>
			FundLowerToleranceNegative,
			/// <summary>
			/// 
			/// </summary>
			FundMaxLowerToleranceExceeded,
			/// <summary>
			/// 
			/// </summary>
			FundInvalidNumDecimalPlacesLowerTolerance,
			/// <summary>
			/// not a valid decimal
			/// </summary>
			FundLowerToleranceInvalidNumber,
			/// <summary>
			/// Not a valid decimal
			/// </summary>
			FundUpperToleranceInvalidNumber,
			/// <summary>
			/// not a valid decimal
			/// </summary>
			FundTPEInvalidNumber,
			/// <summary>
			/// not a valid decimal
			/// </summary>
			FundRevaluationFactorInvalidNumber,
			/// <summary>
			/// not a valid decimal
			/// </summary>
			FundScalingFactorInvalidNumber,
			/// <summary>
			/// not a valid decimal
			/// </summary>
			FundXFactorInvalidNumber
		}
		
		/// <summary>
		/// The maximum upper tolerance allowed
		/// </summary>
		public static decimal MaxTolerance
		{
			get
			{
				return 99.999M; //9999.9%
			}
		}

		/// <summary>
		/// Validates the upper tolerance.
		/// </summary>
		/// <param name="tolerance">Tolerance.</param>
		public FundValidationError[] ValidateUpperTolerance(decimal tolerance)
		{
			T.E();
			ArrayList errors = new ArrayList();
			try
			{
				if (tolerance == 0)
				{
					errors.Add(FundValidationError.FundUpperToleranceZero) ;
				}
				else if (tolerance < 0)
				{
					errors.Add(FundValidationError.FundUpperToleranceNegative);
				}
				else if (tolerance > MaxTolerance)
				{
					errors.Add(FundValidationError.FundMaxUpperToleranceExceeded);
				}
					//note for next line that we store tolerance as a ratio not a percentile
					//percentage is to 4dp
				else if (tolerance != decimal.Parse(tolerance.ToString("#0.000000")))
				{
					errors.Add(FundValidationError.FundInvalidNumDecimalPlacesUpperTolerance);
				}
			}
			finally
			{
				T.X();
			}
			return (FundValidationError[]) errors.ToArray(typeof (FundValidationError));
		}

		/// <summary>
		/// Validates the tolerances, checks to see upper > than lower.
		/// </summary>
		/// <param name="fundToValidate">Fund to validate.</param>
		public FundValidationError[] ValidateTolerances(Fund fundToValidate)
		{

			ArrayList errors = new ArrayList();
			if (fundToValidate.UpperTolerance < fundToValidate.LowerTolerance)
			{
				errors.Add(FundValidationError.FundUpperToleranceLessThanLowerTolerance);
			}
			return (FundValidationError[]) errors.ToArray(typeof (FundValidationError));
		}

		/// <summary>
		/// Validates the lower tolerance.
		/// </summary>
		/// <param name="tolerance">Tolerance.</param>
		public FundValidationError[] ValidateLowerTolerance(decimal tolerance)
		{
			T.E();
			ArrayList errors = new ArrayList();
			try
			{
				if (tolerance == 0)
				{
					errors.Add(FundValidationError.FundLowerToleranceZero) ;
				}
				else if (tolerance < 0)
				{
					errors.Add(FundValidationError.FundLowerToleranceNegative);
				}
				else if (tolerance > MaxTolerance)
				{
					errors.Add(FundValidationError.FundMaxLowerToleranceExceeded);
				}
					//note for next line that we store tolerance as a ratio not a percentile
					//percentage is to 4dp
				else if (tolerance != decimal.Parse(tolerance.ToString("#0.000000")))
				{
					errors.Add(FundValidationError.FundInvalidNumDecimalPlacesLowerTolerance);
				}
			}
			finally
			{
				T.X();
			}
			return (FundValidationError[]) errors.ToArray(typeof (FundValidationError));
		}

		private static bool isValidOEICClassOrSeriesCode(string classOrSeriesCode)
		{
			T.E();
			try
			{
				return classOrSeriesCode.Length == 1 && (classOrSeriesCode.CompareTo("A") >= 0 && classOrSeriesCode.CompareTo("J") <= 0);
			}
			finally
			{
				T.X();
			}
		}

		private static bool isValidNonOEICClassOrSeriesCode(string classOrSeriesCode)
		{
			T.E();
			bool isValid = false;
			try

			{
				if (classOrSeriesCode.Length == 2)
				{
					string firstDigit = classOrSeriesCode.Substring(0, 1);
					if (firstDigit == "0")
					{
						isValid = classOrSeriesCode.Substring(1, 1).CompareTo("1") >= 0 && classOrSeriesCode.Substring(1, 1).CompareTo("9") <= 0;
					}
					else if (firstDigit == "1" || firstDigit == "6")
					{
						isValid = classOrSeriesCode.Substring(1, 1) == "0";
					}
					else if (firstDigit == "5")
					{
						isValid = classOrSeriesCode.Substring(1, 1).CompareTo("1") >= 0 && classOrSeriesCode.Substring(1, 1).CompareTo("9") <= 0;
					}
				}
			}
			finally
			{
				T.X();
			}
			return isValid;

		}

		/// <summary>
		/// Validates a fund is ok
		/// </summary>
		/// <param name="fundToValidate"></param>
		/// <returns></returns>
		public  FundValidationError[] ValidateFund(Fund fundToValidate)
		{
			T.E();

			ArrayList errors = new ArrayList();
			try
			{
				if (!fundToValidate.IsBenchMarkable && CurrentlyABenchmark(fundToValidate))
				{
					errors.Add(FundValidationError.FundCannotUnSelectBenchMark);
				}

				bool fundTypeSelected = fundToValidate is Composite || fundToValidate is OEICFund || fundToValidate is LinkedFund;

				if (!fundTypeSelected)
				{
					errors.Add(FundValidationError.FundTypeNotSelected);
				}


				//validate fund id (hiportcode)	
				if (fundToValidate.HiPortfolioCode == null || fundToValidate.HiPortfolioCode == string.Empty)
				{
					errors.Add(FundValidationError.FundFieldEmptyHiPortfolioCode);
				}

				//validate full name
				if (fundToValidate.FullName == null || fundToValidate.FullName == string.Empty)
				{
					errors.Add(FundValidationError.FundFieldEmptyFullName);
				}

				//validate short name
				if (fundToValidate.ShortName == null || fundToValidate.ShortName == string.Empty)
				{
					errors.Add(FundValidationError.FundFieldEmptyShortName);
				}

				bool fullNameExists;
				bool shortNameExists;
				bool securityCodeExists;
				FundStaticDataPersister fPersister = new FundStaticDataPersister(m_connectionString);

				if (fundToValidate.IsNew)
				{
					bool hiPortCodeExists;

					fPersister.CheckFundExistence(fundToValidate, out hiPortCodeExists,
					                              out securityCodeExists, out fullNameExists, out shortNameExists);

					if (hiPortCodeExists && !((fundToValidate.HiPortfolioCode == null || fundToValidate.HiPortfolioCode == string.Empty)))
					{
						errors.Add(FundValidationError.FundDuplicateFieldHiPortCode);
					}


				}
				else
				{
					fPersister.CheckDuplicationForExistingFund(fundToValidate.HiPortfolioCode, fundToValidate.FullName, fundToValidate.ShortName, fundToValidate.SecurityCode, out fullNameExists, out shortNameExists, out securityCodeExists);
				}

				if (fullNameExists && !((fundToValidate.FullName == null || fundToValidate.FullName == string.Empty)))
				{
					errors.Add(FundValidationError.FundDuplicateFieldFullName);
				}

				if (shortNameExists && !((fundToValidate.ShortName == null || fundToValidate.ShortName == string.Empty)))
				{
					errors.Add(FundValidationError.FundDuplicateFieldShortName);
				}

				if (securityCodeExists )
				{
					errors.Add(FundValidationError.FundDuplicateFieldSecurityCode);
				}

				//validate asset fund
				if (fundToValidate.AssetFundID == string.Empty)
				{
					errors.Add(FundValidationError.FundFieldEmptyAssetFundID);
				}


				if (fundToValidate.ClassOrSeriesCode == string.Empty)
				{
					errors.Add(FundValidationError.FundFieldEmptyClassOrPriceSeries);
				}
				else if (fundToValidate is OEICFund)
				{
					if (!isValidOEICClassOrSeriesCode(fundToValidate.ClassOrSeriesCode))
					{
						errors.Add(FundValidationError.FundInvalidCodeClassOrPriceSeries);
					}
				}
				else if (fundToValidate is LinkedFund || fundToValidate is Composite)
				{
					if (!isValidNonOEICClassOrSeriesCode(fundToValidate.ClassOrSeriesCode))
					{
						errors.Add(FundValidationError.FundInvalidCodeClassOrPriceSeries);
					}
				}
				else
				{
					//type not selected but if its not valid for oeic or non oeic then we
					//may as well still show the error
					if (!isValidOEICClassOrSeriesCode(fundToValidate.ClassOrSeriesCode) || !isValidNonOEICClassOrSeriesCode(fundToValidate.ClassOrSeriesCode))
					{
						errors.Add(FundValidationError.FundInvalidCodeClassOrPriceSeries);
					}
				}

				//external id - an external code must be provided
				if (fundToValidate.SystemIDs != null)
				{
					for (int i = 0; i < fundToValidate.SystemIDs.Count; i++)
					{
						ExternalSystemID id = fundToValidate.SystemIDs[i];
						if (id.FundCode == string.Empty && !id.IsDeleted)
						{
							errors.Add(FundValidationError.FundFieldEmptyExternalSystemId);
							break;
						}
					}
				}

				errors.AddRange(ValidateUpperTolerance(fundToValidate.UpperTolerance));
				errors.AddRange(ValidateLowerTolerance(fundToValidate.LowerTolerance));

				errors.AddRange(ValidateTolerances(fundToValidate));

				//factor validation
				FundValidationError error=ValidateXFactor(fundToValidate);
				if (error!=FundValidationError.NoError )errors.Add(error);
				
				FundValidationError TPEerror=ValidateTPE(fundToValidate);
				if (TPEerror!=FundValidationError.NoError )errors.Add(TPEerror);

				FundValidationError Revalerror=ValidateRevaluationFactor(fundToValidate);
				if (Revalerror!=FundValidationError.NoError )errors.Add(Revalerror);

				if (fundToValidate is NonOEIC)
				{
					if (!((NonOEIC) fundToValidate).ScalingFactorValid())
					{
						errors.Add(FundValidationError.FundScalingFactorInvalid);
					}
				}
				
			}
			finally
			{
				T.X();
			}
				return (FundValidationError[]) errors.ToArray(typeof (FundValidationError));
		}

	

		/// <summary>
		/// Validates the revaluation factor.
		/// </summary>
		/// <param name="fundToValidate">Fund to validate.</param>
		/// <returns></returns>
		public  FundValidationError ValidateRevaluationFactor(Fund fundToValidate)
		{
			FundValidationError returnValue=FundValidationError.NoError;
			//we need them for reval factor validation!!
			if (fundToValidate is NonOEIC)
			{
				((NonOEIC) fundToValidate).Holidays = LoadHolidays();

			
				if (!((NonOEIC) fundToValidate).RevaluationFactorValid())
				{
					returnValue=FundValidationError.FundRevaluationFactorInvalid;
				}
			}
			return returnValue;
		}

		/// <summary>
		/// Validates the TPE.
		/// </summary>
		/// <param name="fundToValidate">Fund to validate.</param>
		/// <returns></returns>
		public static FundValidationError ValidateTPE(Fund fundToValidate)
		{
			FundValidationError returnValue=FundValidationError.NoError;
			if (fundToValidate is NonOEIC)
			{
				
				if (!((NonOEIC) fundToValidate).TPEValid())
				{
					returnValue=FundValidationError.FundTPEInvalid;
				}
			}
			return returnValue;
		}

		/// <summary>
		/// Validates the X factor.
		/// </summary>
		/// <param name="fundToValidate">Fund to validate.</param>
		/// <returns></returns>
		public static FundValidationError ValidateXFactor(Fund fundToValidate)
		{
			FundValidationError returnValue=FundValidationError.NoError;
			if (fundToValidate is OEICFund)
			{
				if (!fundToValidate.XFactorValid())
				{
					returnValue= FundValidationError.FundXFactorInvalid;
				}
			}
			return returnValue;
		}

		/// <summary>
		/// Determines if the Fund is being used as a benchmark by any Asset Funds
		/// </summary>
		/// <param name="fund">Fund to check.</param>
		/// <returns></returns>
		private bool CurrentlyABenchmark(Fund fund)
		{
			FundStaticDataPersister persister = new FundStaticDataPersister(m_connectionString);
			return persister.CurrentlyABenchmark( fund);
		}

		/// <summary>
		/// generates hiportfolio code (from assetFundID and share class)
		/// </summary>
		/// <param name="fund"></param>
		/// <returns></returns>
		public string GenerateHiPortfolioCode(Fund fund)
		{
			if (fund.AssetFundID == null || fund.AssetFundID.Length == 0 || fund.AssetFundID == "0" || fund.ClassOrSeriesCode == null || fund.ClassOrSeriesCode.Length == 0)
			{
				return null;
			}
			else
			{
				//simple formula add the two together but be wary of padded strings due to chars being used for key as opposed to varchar				
				return fund.AssetFundID.Trim() + fund.ClassOrSeriesCode;
			}
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Clears the cache of holidays
		/// </summary>
		public void ClearHolidays()
		{
			FundStaticDataPersister.ClearHolidays();
		}

		/// <summary>
		/// Loads a list of dates that are UK bank holidays. 
		/// </summary>
		/// <returns></returns>
		public Hashtable LoadHolidays()
		{
			T.E();
			Hashtable returnCollection=new Hashtable();
			try
			{
				FundStaticDataPersister fundPersister = new FundStaticDataPersister(m_connectionString);
				returnCollection= fundPersister.LoadHolidays();
				fundPersister=null;
			}
			finally
			{
				T.E();
			}
			return returnCollection;
		}

		#endregion

		#endregion

		#region Drop I

		/// <summary>
		/// Returns all the funds for the given fund group that have a status of either Second Level
		/// Authorised or Released.  The ProgressStatus property is set according to the fund status.
		/// </summary>
		/// <param name="connectionString">The application connection string.</param>
		/// <param name="fundGroupID">The current fund group ID.</param>
		/// <returns>
		/// The fund collection for the fund group but with any funds with an inappropriate status
		/// removed.
		/// </returns>
		/// <exception cref="DatabaseException">Unable to load funds</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		public FundCollection LoadReleaseFundsForFundGroup(string connectionString, int fundGroupID)
		{
			T.E();
			FundCollection releasableFunds = null;
			m_connectionString = connectionString;
			try
			{
				FundCollection funds = this.LoadForFundGroup(fundGroupID);
				if (funds != null)
				{
					for (int count = 0; count < funds.Count; count++)
					{
						Fund fund = funds[count];

						if (fund.FundStatus == Fund.FundStatusType.SecondLevelAuthorised
							|| fund.FundStatus == Fund.FundStatusType.Released)
						{
							if (releasableFunds == null)
							{
								releasableFunds = new FundCollection();
							}

							fund.ProgressStatus = (fund.FundStatus == Fund.FundStatusType.Released);
							fund.InitialProgressStatus = fund.ProgressStatus;
							fund.IsDirty = false;

							releasableFunds.Add(fund);
						}
					}
				}
			}
			finally
			{
				T.X();
			}

			return releasableFunds;
		}

		/// <summary>
		/// Return all the funds for the given Asset Fund.
		/// </summary>
		/// <param name="connectionString">The application connection string.</param>
		/// <param name="assetFund">The code for the current Asset Fund.</param>
		/// <returns>The fund list for the Asset Fund.</returns>
		/// <exception cref="DatabaseException">Unable to load funds</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <remarks>This will create a partially populated fund object, returning factors, tolerances, fundgroups, and external system identifiers.</remarks>
		public static FundCollection LoadFundsByAssetFund(string connectionString, string assetFund)
		{
			T.E();
			FundCollection fundColl = null;
			try
			{
				// Create a partially populated fund object
				FundStaticDataPersister persister = new FundStaticDataPersister(connectionString);
				fundColl = persister.LoadForAssetFund(assetFund);
			}
			finally
			{
				T.X();
			}
			return fundColl;
		}

		/// <summary>
		/// Update the fund status to the next level for any funds that have had their ProgressStatus
		/// flag set.
		/// </summary>
		/// <param name="connectionString">The application connection string.</param>
		/// <param name="funds">The collection of edited funds.</param>
		/// <param name="progression">The status that the affected funds are to be progressed to.</param>
		/// <exception cref="DatabaseException">Unable to load funds</exception>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		public static void ProgressFunds(string connectionString, FundCollection funds,
		                                 Fund.FundStatusType progression)
		{
			T.E();
			try
			{
				if (funds != null)
				{
					switch (progression)
					{
						case Fund.FundStatusType.SecondLevelAuthorised:
							AuthorisedFundPricePersister authorisePersister
								= new AuthorisedFundPricePersister(connectionString);
							authorisePersister.Save(funds);
							break;
						case Fund.FundStatusType.Released:
							ReleasePricePersister releasePersister = new ReleasePricePersister(connectionString);
							releasePersister.Save(funds);
							break;
						default:
							throw new NotImplementedException("This functionality has not been implemented yet.");
					}
				}
			}
			finally
			{
				T.X();
			}
		}

		#region Update methods

		/// <summary>
		/// Save modified fund details through the persistance layer
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="updatedFunds">Fund Collection object containing details of modified funds</param>
		/// <returns>Success/failure flag</returns>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update or delete items</exception>
		public static bool Update(string connectionString, FundCollection updatedFunds)
		{
			T.E();
			bool ret = false;
			try
			{
				//
				// TODO: Create a refactored Update() method in the correct persister object
				//
				//FundPersister persister = new FundPersister(connectionString);
				//ret = persister.Save(updatedFunds);
			}
			finally
			{
				T.X();
			}
			return ret;
		}

		#endregion

		#endregion

		/// <summary>
		/// Loads a fund lookupcollection by group.
		/// </summary>
		/// <param name="groupId">Group id.</param>
		/// <returns></returns>
		public FundCollection LoadFundLookupsByGroup(int groupId)
		{
			T.E();
			try
			{
				ReleasePricePersister fundReleasePersister = new ReleasePricePersister (m_connectionString);
				return fundReleasePersister.LoadForGroup(groupId);
			}
			finally
			{
				T.X();
			}
		}
	}
}