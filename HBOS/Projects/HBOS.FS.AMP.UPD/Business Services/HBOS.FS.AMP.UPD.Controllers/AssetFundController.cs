using System;
using System.Collections;
using System.Data.SqlClient;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Persistence;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.Countries;
using HBOS.FS.AMP.UPD.Types.Funds;
using HBOS.FS.AMP.UPD.Types.Lookups;
using HBOS.FS.AMP.UPD.Types.Snapshot;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Controllers
{
	/// <summary>
	/// Summary description for AssetFundController.
	/// </summary>
	public class AssetFundController
	{
		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public AssetFundController()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#endregion

		#region Load methods

		/// <summary>
		/// Loads a list of asset funds for supplied company
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="companyCode"></param>
		/// <returns></returns>
		public static SimpleStringLookupCollection LoadAssetFundLookupsByCompany(string connectionString, string companyCode)
		{
			AssetFundLookupPersister afLookup = new AssetFundLookupPersister(connectionString);
			return afLookup.LoadForCompany(companyCode);
		}

		/// <summary>
		/// Loads a list of asset funds for supplied company and type
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="companyCode">The company code to filter by</param>
		/// <param name="assetFundType">The asset fund type to filter by</param>
		/// <returns></returns>
		public  static SimpleStringLookupCollection LoadAssetFundLookupsByCompanyAndType
			(string connectionString, string companyCode, AssetFund.AssetFundTypeEnum assetFundType)
		{
			AssetFundLookupPersister afLookup = new AssetFundLookupPersister(connectionString);
			return afLookup.LoadForCompany(companyCode, assetFundType);
		}

		/// <summary>
		/// Lightweight asset fund load (static data)
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="assetFundID"></param>
		/// <returns></returns>
		public  static AssetFund LoadAssetFundForStaticData(string connectionString, string assetFundID)
		{
			AssetFundStaticDataPersister persister = new AssetFundStaticDataPersister(connectionString);
			return persister.Load(assetFundID);
		}

		/// <summary>
		/// Lightweight asset fund load (static data)
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="companyCode"></param>
		/// <returns></returns>
		public static AssetFundCollection LoadAssetFundsByCompanyIdForStaticDataExport(string connectionString, string companyCode)
		{
			AssetFundStaticDataExportPersister persister = new AssetFundStaticDataExportPersister(connectionString);
			return persister.LoadForCompany(companyCode);
		}

		/// <summary>
		/// Get a collection of the asset funds from the database for a specified company
		/// </summary>
		/// <param name="connectionString">Valid database connection string</param>
		/// <param name="companyCode">The ID of the company for which the funds are to be retrieved</param>
		/// <param name="currentWeightings">Extract the most recent weightings<br />
		/// <list type="bullet">
		/// <item>
		/// <description>Pass in a value of <code>true</code> will extract the most recent weightings overriding any 
		/// indices that have authorised where there are more recent values</description>
		/// </item>
		/// <list type="bullet">
		/// <listheader>
		/// <description>Pass in a value of <code>false</code> will either</description>
		/// </listheader>
		/// <item>
		/// <description>Extract weightings for authorised indices only.</description>
		/// </item>
		/// <item>
		/// <description>Or, when there are no authorised indices for the most recent weightings</description>
		/// </item>
		/// </list>
		/// </list>
		/// </param>
		/// <returns>A collection of asset fund objects</returns>
		public  static AssetFundCollection LoadAssetFunds(string connectionString, string companyCode, bool currentWeightings)
		{
			AssetFundPricingPersister assetFunds = new AssetFundPricingPersister(connectionString);
			return assetFunds.LoadForCompany(companyCode, currentWeightings);
		}

		/// <summary>
		/// Get a collection of the asset funds from the database for a specified fund group
		/// </summary>
		/// <param name="connectionString">Valid DB connection string</param>
		/// <param name="fundGroupID">The ID of the fund group for which the funds are to be retrieved</param>
		/// <param name="currentWeightings">Extract the most recent weightings<br />
		/// <list type="bullet">
		/// <item>
		/// <description>Pass in a value of <code>true</code> will extract the most recent weightings overriding any 
		/// indices that have authorised where there are more recent values</description>
		/// </item>
		/// <list type="bullet">
		/// <listheader>
		/// <description>Pass in a value of <code>false</code> will either</description>
		/// </listheader>
		/// <item>
		/// <description>Extract weightings for authorised indices only.</description>
		/// </item>
		/// <item>
		/// <description>Or, when there are no authorised indices for the most recent weightings</description>
		/// </item>
		/// </list>
		/// </list>
		/// </param>
		/// <returns>A collection of asset fund objects</returns>
		public static AssetFundCollection LoadAssetFunds(string connectionString, int fundGroupID, bool currentWeightings)
		{
			AssetFundPricingPersister assetFunds = new AssetFundPricingPersister(connectionString);
			return assetFunds.Load(fundGroupID, currentWeightings);
		}

		/// <summary>
		/// Get the passed asset fund
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="assetFundID"></param>
		/// <param name="currentWeightings"></param>
		/// <returns></returns>
		public  static AssetFund LoadAssetFund(string connectionString, string assetFundID, bool currentWeightings)
		{
			AssetFundPricingPersister persister = new AssetFundPricingPersister(connectionString);
			return persister.Load(assetFundID, currentWeightings);
		}


		#endregion

		#region Load lookup data

		/// <summary>
		/// Method to get a list of all the countries held in the DB
		/// </summary>
		/// <param name="connectionString">Valid DB connection string</param>
		/// <returns type="CountryCollection">A country collection object</returns>
		public static CountryCollection LoadCountries(string connectionString)
		{
			CountryPersister countries = new CountryPersister(connectionString);
			return countries.LoadCountries();
		}

		/// <summary>
		/// Loads the all available benchmarks.
		/// </summary>
		/// <param name="connectionString">Connection string.</param>
		/// <returns></returns>
		public static AssetMovementConstituentCollection LoadAllAvailableBenchmarks(string connectionString)
		{
			T.E();
			AssetMovementConstituentCollection parts = null;

			try
			{
				AssetMovementConstituentPersister amcp=new AssetMovementConstituentPersister(connectionString);
				parts = amcp.LoadAllBenchmarks();
			}
			finally
			{
				T.X();
			}
			return parts;
		}



		#endregion

		#region Update methods

		/// <summary>
		/// Saves a singular asset fund
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="assetFund"></param>
		public static void UpdateAssetFund(string connectionString, AssetFund assetFund)
		{
			T.E();
			try
			{
				UpdateAssetFundAndChildFunds(connectionString,assetFund,null);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Update the asset fund and associated funds
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="assetFund"></param>
		/// <param name="childFunds"></param>
		public  static void UpdateAssetFundAndChildFunds(string connectionString, AssetFund assetFund, FundCollection childFunds)
		{
			T.E();
			SqlTransaction newTrans;
			InvalidFactorException factorEx = null;
			try
			{
				using (SqlConnection newConnection = new SqlConnection(connectionString))
				{
					// Establish the transaction object
					newConnection.Open();
					newTrans = newConnection.BeginTransaction();
					AssetFundStaticDataPersister updateAssetFunds = new AssetFundStaticDataPersister(connectionString);
					try
					{
						updateAssetFunds.Save(assetFund, newTrans);
						if (childFunds != null && childFunds.Count > 0)
						{
							FundStaticDataPersister fundPersister = new FundStaticDataPersister(connectionString);
							try
							{
								fundPersister.SaveFunds(childFunds, newTrans);
							}
							catch (InvalidFactorException ex)
							{
								//if we get this we still want to commit the transaction, but
								//we want rethrow it in order to inform calling client who can then inform the user
								//through the UI.
								//Once UI validation is completed we could remove this and just always 
								//treat this as an error and always fail.
								factorEx = ex;
							}

						}
						saveMovementConstituents(newTrans,assetFund);
						newTrans.Commit();
					}
					catch
					{
						newTrans.Rollback();
						throw;
					}
				}

			}
			finally
			{
				T.X();
			}
			if (factorEx != null)
			{
				throw factorEx;
			}
		}

		private static void saveMovementConstituents(SqlTransaction trans, AssetFund assetFund)
		{
			if (hasSplitChanged(assetFund.AssetMovementConstituents))
			{
				if (assetFund.AssetMovementConstituents.Count>0)

				{
					SnapshotPersister snapshotPersister = new SnapshotPersister(trans.Connection.ConnectionString);
					Snapshot snapshot = snapshotPersister.NewStaticDataSnapshot(assetFund.CompanyCode,trans);

					AssetMovementConstituentPersister movementPersister = new AssetMovementConstituentPersister(trans.Connection.ConnectionString);
					movementPersister.Save(assetFund.AssetMovementConstituents,snapshot,trans);

					snapshotPersister.Activate(snapshot,trans);
				}
				else
				{
					AssetMovementConstituentPersister movementPersister = new AssetMovementConstituentPersister(trans.Connection.ConnectionString);
					movementPersister.Clear(assetFund.AssetFundCode,trans);
				}
			}
		}

		private static bool hasSplitChanged(AssetMovementConstituentCollection constitution)
		{
			bool result = false;
			if (constitution != null)
			{
				result =constitution.IsDirty;
				if (result!=true)
				{
					foreach(AssetMovementConstituent constituent in constitution)
					{
						result = constituent.IsDeleted || constituent.IsDirty || constituent.IsNew;
						if (result) break;
					}
				}
			}
			return result;
		}

		#endregion 

		#region Validation Methods

		/// <summary>
		/// Indicates what type of error occurred 
		/// </summary>
		public enum AssetFundValidationError
		{
			/// <summary>
			///No error occurred 
			/// </summary>
			AssetFundNoError,
			/// <summary>
			/// The Asset Funds type (OEIC, Linked or COmposite) has not been set
			/// </summary>
			AssetFundTypeNotSelected,
			/// <summary>
			/// The mandatory Price file has not been associated to the AssetFund
			/// </summary>
			AssetFundPriceFileNotSelected,
			/// <summary>
			/// market splits proportion total is less than 100%
			/// </summary>
			AssetFundSplitLessThan100Percent,
			/// <summary>
			/// market splits proportion total is greater than 100%
			/// </summary>
			AssetFundSplitMoreThan100Percent,
			/// <summary>
			/// a weighting must be valid to 4dp (6dp as a ratio)
			/// </summary>
			AssetFundInvalidNumDecimalPlacesProportion,
			/// <summary>
			/// value higher than max
			/// </summary>
			AssetFundMaxToleranceExceeded,
			/// <summary>
			/// tolerance must be provided
			/// </summary>
			AssetFundToleranceNegative,
			/// <summary>
			/// tolerance must be to 2dp
			/// </summary>
			AssetFundInvalidNumDecimalPlacesTolerance,
			/// <summary>
			/// Full Name required field empty
			/// </summary>
			AssetFundFieldEmptyFullName,
			/// <summary>
			/// Full Name unique field already exists
			/// </summary>
			AssetFundDuplicateFieldFullName,
			/// <summary>
			/// Short Name required field is empty
			/// </summary>
			AssetFundFieldEmptyShortName,
			/// <summary>
			/// Short Name unique field already exists
			/// </summary>
			AssetFundDuplicateFieldShortName,
			/// <summary>
			/// Asset Fund Code required field is empty
			/// </summary>
			AssetFundFieldEmptyCode,
			/// <summary>
			/// Asset Fund Code unique field already exists
			/// </summary>
			AssetFundDuplicateFieldCode
		}

		
		/// <summary>
		/// validation enum for tolerance validation
		/// </summary>
		public enum ToleranceValidationError
		{
		
		}

		private  static AssetFundValidationError[] validateTolerance(decimal tolerance)
		{
			T.E();
			ArrayList errors=new ArrayList();

			try
			{
				if (tolerance < 0)
				{
					errors.Add( AssetFundValidationError.AssetFundToleranceNegative);
				}
				else if (tolerance > MaxTolerance)
				{
					errors.Add( AssetFundValidationError.AssetFundMaxToleranceExceeded);
				}
					//note for next line that we store tolerance as a ratio not a percentile (is to 4dp percentile)
				else if (tolerance != decimal.Parse(tolerance.ToString("#0.000000")))
				{
					errors.Add( AssetFundValidationError.AssetFundInvalidNumDecimalPlacesTolerance);
				}
			}
			finally
			{
				T.X();
			}
			return (AssetFundValidationError[]) errors.ToArray(typeof (AssetFundValidationError));
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
		/// Validates that the short name is ok, the full name is ok, and that the market splits total 100%
		/// Precondition - full name and short name max length is validated prior to here (by restricting text field in UI)
		/// </summary>
		/// <param name="connection"></param>
		/// <param name="updatedAssetFund"></param>
		/// <returns></returns>
		public static AssetFundValidationError[] ValidateUpdatedAssetFund(string connection, AssetFund updatedAssetFund)
		{
			T.E();
			
			ArrayList errors=new ArrayList();

			try
			{
				bool fullNameExists ;
				bool shortNameExists;
				AssetFundStaticDataPersister afPersister = new AssetFundStaticDataPersister(connection);
				afPersister.CheckFullNameOrShortNameDuplicationForExistingAssetFund(updatedAssetFund.AssetFundCode,
				                                                                    updatedAssetFund.FullName, updatedAssetFund.ShortName, out fullNameExists, out shortNameExists);

				errors.AddRange( checkForDuplicateAndExistingFields(updatedAssetFund, fullNameExists, shortNameExists));

				if (updatedAssetFund.PriceFileId==0)
				{
					errors.Add(AssetFundValidationError.AssetFundPriceFileNotSelected);
				}

				errors.AddRange( validateTolerance(updatedAssetFund.AssetMovementTolerance));
			
				errors.AddRange(  validateBenchmarkValueSplit(updatedAssetFund));
			
			}
			finally
			{
				T.X();
			}
			
			return (AssetFundValidationError[]) errors.ToArray(typeof (AssetFundValidationError));
		}

		private static AssetFundValidationError[] checkForDuplicateAndExistingFields(AssetFund updatedAssetFund,  bool fullNameExists, bool shortNameExists)
		{
			ArrayList errors=new ArrayList();
			if (updatedAssetFund.FullName.Length == 0)
			{
				errors.Add(AssetFundValidationError.AssetFundFieldEmptyFullName);
			}
			else if (fullNameExists)
			{
				errors.Add(AssetFundValidationError.AssetFundDuplicateFieldFullName);
			}
	
			if (updatedAssetFund.ShortName.Length == 0)
			{
				errors.Add(AssetFundValidationError.AssetFundFieldEmptyShortName);
			}
			else if (shortNameExists)
			{
				errors.Add(AssetFundValidationError.AssetFundDuplicateFieldShortName);
			}

			return (AssetFundValidationError[]) errors.ToArray(typeof (AssetFundValidationError));
		}

		/// <summary>
		/// Validates data for the creation of a new asset fund. Accepts param data
		/// rather than an object as type also requires validation.
		/// Precondition - maxlength of fields validated in UI.
		/// </summary>
		/// <param name="connection"></param>
		/// <param name="assetFund"></param>
		/// <returns></returns>
		public  static AssetFundValidationError[] ValidateNewAssetFund(string connection, AssetFund assetFund)
		
		{

			T.E();
	
			ArrayList errors =new ArrayList();
			try
			{

				AssetFundPricingPersister afPersister = new AssetFundPricingPersister(connection);

				bool assetFundIDEXists;
				bool fullNameExists;
				bool shortNameExists;

				afPersister.CheckAssetFundExistence(assetFund.AssetFundCode, assetFund.FullName, assetFund.ShortName, out assetFundIDEXists, out fullNameExists, out shortNameExists);


				if (assetFund.AssetFundCode.Length == 0)
				{
					errors.Add(AssetFundValidationError.AssetFundFieldEmptyCode);
				}
				else if (assetFundIDEXists)
				{
					errors.Add(AssetFundValidationError.AssetFundDuplicateFieldCode);
				}

				if (assetFund.PriceFileId==0)
				{
					errors.Add(AssetFundValidationError.AssetFundPriceFileNotSelected);
				}

				errors.AddRange(checkForDuplicateAndExistingFields(assetFund,fullNameExists,shortNameExists));
			
				errors.AddRange(  validateBenchmarkValueSplit(assetFund));

			}
			finally
			{
				T.X();
			}
			
			return (AssetFundValidationError[]) errors.ToArray(typeof (AssetFundValidationError));
		}

		/// <summary>
		/// Validate the market value split proportion value of the asset fund weighted
		/// indices.  This will ensure the sum of the proportion value adds up to 100%. 
		/// Note that this can only be validated for an updated asset fund - a new asset fund
		/// cannot have mv splits as its type has not been decide upon until saved
		/// </summary>
		/// <param name="updatedAssetFund"></param>
		/// <returns></returns>
		private  static AssetFundValidationError[] validateBenchmarkValueSplit(AssetFund updatedAssetFund)
		{
			ArrayList errors= new ArrayList();
			decimal proportionSum = 0;
			T.E();
			try
			{
				AssetMovementConstituentCollection assetMovementConstituents=updatedAssetFund.AssetMovementConstituents;

				proportionSum = assetMovementConstituents.TotalProportion();

				if (assetMovementConstituents.DoAnyItemsHaveAProportionOfZero)
				{
					//errors.Add(AssetFundValidationError.AssetFundSplitSingleProportionValueOfZero);
				}

				
				if (errors.Count == 0)
				{
					if (proportionSum < 1 && assetMovementConstituents.Count>0)
					{
						errors.Add(AssetFundValidationError.AssetFundSplitLessThan100Percent);
					}
					else if (proportionSum > 1)
					{
						errors.Add(AssetFundValidationError.AssetFundSplitMoreThan100Percent);
					}
				}
				}
				
		
			finally
			{
				T.X();
			}

			return (AssetFundValidationError[]) errors.ToArray(typeof (AssetFundValidationError));
		}

		/// <summary>
		/// Validates the benchmark can be added to the specified asset fund.
		/// This is the test for any circular reference against the asset fund and benchmark fund.
		/// </summary>
		/// <param name="connectionString">Connection string.</param>
		/// <param name="assetFundCode">Asset fund code.</param>
		/// <param name="benchmarkFundCode">Benchmark fund code.</param>
		/// <returns>String: empty when true; populated when false.</returns>
		public static string CanBenchmarkBeAddedToAssetFund(string connectionString, string assetFundCode, string benchmarkFundCode)
		{
			T.E();
			
			string returnString = string.Empty;

			try
			{
				AssetFundBenchMarkPersister persister = new AssetFundBenchMarkPersister(connectionString);

				// Call method to test for benchmark circular reference back to the initial asset fund
				returnString = persister.IsBenchmarkRelatedToAssetFund(assetFundCode, benchmarkFundCode);
			}
			catch
			{
				throw;
			}
			finally
			{
				T.X();
			}

			return returnString;
		}


		#endregion

		/// <summary>
		/// Updates the movement constituents.
		/// </summary>
		/// <param name="parts">Parts.</param>
		/// <param name="fund">Fund.</param>
		/// <param name="connectionString">Connection string.</param>
		public static void UpdateMovementConstituents(AssetMovementConstituentCollection parts, AssetFund fund, string connectionString)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// This delegate is used to Asynchronously Load All benchmarks
		/// </summary>
		public delegate AssetMovementConstituentCollection LoadAllBenchMarksDelegate (string connectionString);


		/// <summary>
		/// Begins the load all available benchmarks.
		/// </summary>
		/// <param name="connectionString">Connection string.</param>
		/// <returns></returns>
		public static IAsyncResult BeginLoadAllAvailableBenchmarks(string connectionString)
		{
			return BeginLoadAllAvailableBenchmarks(connectionString,null);
		}

		/// <summary>
		/// Begins the load all available benchmarks with a call back function.
		/// </summary>
		/// <param name="connectionString">Connection string.</param>
		/// <param name="callBack">Call back.</param>
		/// <returns></returns>
		public static IAsyncResult BeginLoadAllAvailableBenchmarks(string connectionString,AsyncCallback callBack)
		{
			LoadAllBenchMarksDelegate benchMarksDelegate=new LoadAllBenchMarksDelegate(LoadAllAvailableBenchmarks);
			return 	benchMarksDelegate.BeginInvoke(connectionString,callBack,benchMarksDelegate);
		}

		/// <summary>
		/// Ends the load all available benchmarks.
		/// </summary>
		/// <param name="asyncResult">An Async Result.</param>
		/// <returns></returns>
		public static AssetMovementConstituentCollection EndLoadAllAvailableBenchmarks(IAsyncResult asyncResult)
		{
			LoadAllBenchMarksDelegate benchMarksDelegate=(LoadAllBenchMarksDelegate) asyncResult.AsyncState;
			return benchMarksDelegate.EndInvoke(asyncResult);
		}
	}
}