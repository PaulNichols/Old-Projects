using System;
using System.Data;
using System.Data.SqlClient;
using HBOS.FS.AMP.UPD.Types.Funds;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// Summary description for ImportFundPricePersister.
	/// </summary>
	public class ImportFundPricePersister : FundPersister
	{
		#region Constructor

		/// <summary>
		/// 
		/// </summary>
		/// <param name="connectionString"></param>
		public ImportFundPricePersister(string connectionString) : base(connectionString)
		{
			T.E();
			T.X();
		}

		#endregion

		#region Load methods


		/// <summary>
		/// Load all the funds on the system for the price import.
		/// </summary>
		/// <returns>
		/// FundCollection.  A partially populated Fund object containing the
		/// associated asset fund id, the fund status, and the hiPortFolio code
		/// </returns>
		public FundCollection Load()
		{
			T.E();
			const string loadSp = "usp_FundGetImportFields";

			// Create the funds collection.
			FundCollection funds = new FundCollection();
			try
			{
				this.LoadEntityCollection(loadSp, funds);

				// Test for valid object
				if (funds == null)
				{
					throw new ArgumentException("Expecting FundCollection from base load, but no object or object of invalid type returned");
				}
			}
			catch (SqlException ex)
			{
				ThrowDBException(ex, loadSp);
			}
			finally
			{
				T.X();
			}

			return funds;
		}

		/// <summary>
		/// Load the funds by company for the price import.
		/// </summary>
		/// <param name="companyCode"></param>
		/// <returns>FundCollection.  A partially populated Fund object containing the
		/// associated asset fund id, the fund status, and the hiPortFolio code</returns>
		public FundCollection Load(string companyCode)
		{
			T.E();
			const string loadSp = "usp_FundGetImportFieldsForCompanyCode";
			SqlParameter[] spParams = new SqlParameter[1];
			spParams[0] = new SqlParameter("@sCompanyCode", SqlDbType.VarChar, 10);
			spParams[0].Value = companyCode;

			// Create the funds collection.
			FundCollection funds = new FundCollection();
			try
			{
				this.LoadEntityCollection(loadSp, spParams, funds);

				// Test for valid object
				if (funds == null)
				{
					throw new ArgumentException("Expecting FundCollection from base load, but no object or object of invalid type returned");
				}
			}
			catch (SqlException ex)
			{
				ThrowDBException(ex, loadSp, spParams);
			}
			finally
			{
				T.X();
			}

			return funds;
		}

		/// <summary>
		/// Load the funds by company for the price import.
		/// </summary>
		/// <param name="companyCode"></param>
		/// <param name="extension"></param>
		/// <param name="fileName">File name to filter funds by</param>
		/// <returns>FundCollection.  A partially populated Fund object containing the
		/// associated asset fund id, the fund status, and the hiPortFolio code</returns>
		public FundCollection Load(string companyCode,string fileName,string extension)
		{
			T.E();
			const string loadSp = "usp_FundGetImportFieldsForCompanyCodeByFile";
			SqlParameter[] spParams = new SqlParameter[3];
			spParams[0] = new SqlParameter("@CompanyCode", SqlDbType.VarChar, 10);
			spParams[0].Value = companyCode;
			spParams[1] = new SqlParameter("@FileName", SqlDbType.VarChar, 50);
			spParams[1].Value = fileName;
			spParams[2] = new SqlParameter("@Extension", SqlDbType.Char, 3);
			spParams[2].Value = extension;

			// Create the funds collection.
			FundCollection funds = new FundCollection();
			try
			{
				this.LoadEntityCollection(loadSp, spParams, funds);

				// Test for valid object
				if (funds == null)
				{
					throw new ArgumentException("Expecting FundCollection from base load, but no object or object of invalid type returned");
				}
			}
			catch (SqlException ex)
			{
				ThrowDBException(ex, loadSp, spParams);
			}
			finally
			{
				T.X();
			}

			return funds;
		}

		#endregion

		#region Entity methods

		/// <summary>
		/// Called from CreateEntity.  
		/// Populate the Fund object for the price import.
		/// </summary>
		/// <param name="safeReader">The reader containing the data</param>
		/// <returns>A Fund object</returns>
		protected override object CreateEntity(SafeDataReader safeReader)
		{
			T.E();
			Fund newFund = null;

			// Resolve the database type field.
			FundFactory.FundType fundType = FundPersister.ResolveFundType(safeReader.GetString("FundType"));

			switch (fundType)
			{
				case FundFactory.FundType.Oeic:
					newFund = new OEICFund();
					break;
				case FundFactory.FundType.Linked:
					newFund = new LinkedFund();
					break;
				case FundFactory.FundType.Composite:
					newFund = new Composite();
					break;
				default:
					throw new ArgumentException("Invalid Fund type object specified.");
			}

			try
			{
				newFund.HiPortfolioCode = safeReader.GetString("HiPortFolioCode");
				newFund.ParentAssetFundID = safeReader.GetString("AssetFundID");
				newFund.SecurityCode=safeReader.GetString("securityCode");
				newFund.IsBenchMarkable=safeReader.GetBoolean("isbenchmarkable");

				// Resolve the database status field.
				Fund.FundStatusType fundStatus = FundPersister.ResolveFundStatus(safeReader.GetInt32("statusID"));
				newFund.FundStatus = fundStatus;
			}
			finally
			{
				T.X();
			}

			return newFund;
		}

		#endregion
	}
}