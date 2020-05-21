using System;
using System.Data;
using System.Data.SqlClient;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Types.DistributionFiles;
using HBOS.FS.AMP.UPD.Types.FundGroups;
using HBOS.FS.Support.Tex;
using Microsoft.ApplicationBlocks.Data;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// The class to use for persisting or retrieving Fund Group objects and
	/// Fund Group Collection objects.
	/// </summary>
	public class FundGroupPersister : EntityPersister
	{
		#region Constructors

		/// <summary>
		/// Constructor used to initialise the ConnectionString property.
		/// </summary>
		/// <param name="connectionString"></param>
		public FundGroupPersister(string connectionString) : base(connectionString)
		{
			T.E();
			T.X();
		}

		#endregion

		#region Load Methods

		/// <summary>
		/// Returns the collection of Fund group objects for the given company.
		/// </summary>
		/// <param name="companyCode">The ID of the company for which the fund groups are to be retrieved.</param>
		/// <returns>A collection of matching fund groups.</returns>
		/// <exception cref="InvalidSqlParameterException">Thrown if one of the parameters was of the incorrect value, type or length; or was null</exception>
		/// <exception cref="DatabaseException">Thrown if other unclassified Sql error occurs</exception>
		public virtual FundGroupCollection LoadFundGroupsByCompany(string companyCode)
		{
			CacheHelper cacheHelper = new CacheHelper();
			return cacheHelper.GetFundGroups(ConnectionString, companyCode);
//			T.E();
//			FundGroupCollection fundGroups = null;
//			try
//			{
//				SqlParameter[] spParams = new SqlParameter[1];
//
//				spParams[0] = new SqlParameter("@sCompanyCode", SqlDbType.VarChar, 10);
//				spParams[0].Value = companyCode;
//
//				fundGroups = new FundGroupCollection();
//
//				this.LoadEntityCollection("usp_FundGroupsGetForCompanyCode", spParams, fundGroups);
//
//			}
//			finally
//			{
//				T.X();
//			}
//			return fundGroups;
		}

		/// <summary>
		/// Returns fund groups by company and holding fund type (AssetFund or Fund)
		/// </summary>
		/// <param name="companyCode">10 char code</param>
		/// <param name="fundType">The 1 char code for fund group type- "A" for AssetFund, "I" for Individual (Fund)</param>
		/// <returns></returns>
		/// <exception cref="InvalidSqlParameterException">Thrown if one of the parameters was of the incorrect value, type or length; or was null</exception>
		/// <exception cref="DatabaseException">Thrown if other unclassified Sql error occurs</exception>
		public FundGroupCollection LoadFundGroupsByCompanyAndType(string companyCode, FundGroupFactory.FundGroupTypes fundType)
		{
			T.E();

			CacheHelper cacheHelper = new CacheHelper();
			return filterByType(cacheHelper.GetFundGroups(ConnectionString, companyCode), fundType);

//			FundGroupCollection fundGroups = null;
//			try
//			{
//				SqlParameter[] spParams = new SqlParameter[2];
//
//				spParams[0] = new SqlParameter("@sCompanyCode", SqlDbType.VarChar, 10);
//				spParams[0].Value = companyCode;
//
//				spParams[1] = new SqlParameter("@cContainsType", SqlDbType.Char, 1);
//				spParams[1].Value = fundType;
//
//				fundGroups = new FundGroupCollection();
//
//				this.LoadEntityCollection("usp_FundGroupsGetForCompanyCodeByType", spParams, fundGroups);
//			}
//			finally
//			{
//				T.X();
//			}
//			return fundGroups;
		}

		/// <summary>
		/// Filters the collection of fundgroupps by type (Asset or Individual).
		/// </summary>
		/// <param name="fundGroupCollection">Fund group collection.</param>
		/// <param name="type">Type.</param>
		/// <returns></returns>
		protected FundGroupCollection filterByType(FundGroupCollection fundGroupCollection, FundGroupFactory.FundGroupTypes type)
		{
			FundGroupCollection returnCollection = new FundGroupCollection();
			foreach (FundGroup fundGroup in fundGroupCollection)
			{
				// Amended by MAW 14/09/200 05 (Issue: UA131)
				// The previous switch case statement always added the fundgroup to the returning collection
				// no matter what you asked for!
				switch (type)
				{
					case FundGroupFactory.FundGroupTypes.Asset:

						if (fundGroup is AssetFundGroup)
						{
							returnCollection.Add(fundGroup);
						}
						break;
					case FundGroupFactory.FundGroupTypes.Individual:
						if (fundGroup is IndividualFundGroup)
						{
							returnCollection.Add(fundGroup);
						}
						break;
				}
			}

			return returnCollection;
		}

		/// <summary>
		/// Loads direct fund groups associated with the passed fund code
		/// </summary>
		/// <param name="fundCode"></param>
		/// <returns></returns>
		public FundGroupCollection LoadDirectFundGroupsByFundCode(string fundCode)
		{
			T.E();
			SqlParameter[] spParams = new SqlParameter[1];

			spParams[0] = new SqlParameter("@cHiPortfolioCode", SqlDbType.Char, 10);
			spParams[0].Value = fundCode;

			FundGroupCollection fundGroups = new FundGroupCollection();

			this.LoadEntityCollection("usp_FundGroupsGetForHiPortfolioCodeDirect", spParams, fundGroups);

			T.X();
			return fundGroups;
		}

		/// <summary>
		/// Loads a fund group for an asset fund by asset fund id
		/// </summary>
		/// <param name="assetFundID"></param>
		/// <returns></returns>
		public FundGroupCollection LoadFundGroupsByAssetFundCode(string assetFundID)
		{
			T.E();
			SqlParameter[] spParams = new SqlParameter[1];

			spParams[0] = new SqlParameter("@cAssetFundID", SqlDbType.Char, 8);
			spParams[0].Value = assetFundID;

			FundGroupCollection fundGroups = new FundGroupCollection();

			this.LoadEntityCollection("usp_FundGroupsGetForAssetFundID", spParams, fundGroups);

			T.X();
			return fundGroups;
		}

		/// <summary>
		/// Creates the FundGroup entity from the database.
		/// </summary>
		/// <param name="safeReader">The reader to be used to create the data.</param>
		/// <returns>The hydrated fund group</returns>
		protected override object CreateEntity(SafeDataReader safeReader)
		{
			T.E();
			FundGroup fundGroup = null;
			try
			{
				fundGroup = FundGroupFactory.CreateFundGroup(
					safeReader.GetInt32("fundGroupID"),
					safeReader.GetString("companyCode"),
					resolveFundGroupType(safeReader.GetString("containsType")),
					safeReader.GetString("fullName"),
					safeReader.GetString("shortName"),
					safeReader.GetTimestamp("ts"),
					safeReader.ColumnExists("forRelease") ? safeReader.GetBoolean("forRelease") : false,
					safeReader.ColumnExists("hasAssociatedFunds") ? safeReader.GetBoolean("hasAssociatedFunds") : false,
					safeReader.ColumnExists("allowSelectAllAuthorisation") ? safeReader.GetBoolean("allowSelectAllAuthorisation") : false
					);

			}
			finally
			{
				T.X();
			}
			return fundGroup;
		}

		/// <summary>
		/// Loads the distribution files that are associated with this fund group
		/// </summary>
		/// <param name="fundGroup"></param>
		/// <exception cref="DatabaseException">Unable to load items</exception>
		/// <exception cref="ArgumentException">Invalid fund group id</exception>
		/// <exception cref="NullReferenceException">Fund group or fund group collection is null</exception>
		protected void LoadDistributionFiles(FundGroup fundGroup)
		{
			T.E();
			const string loadSp = "usp_DistributionFilesGetForFundGroupID";
			SqlParameter[] parameters = null;

			try
			{
				if (fundGroup == null)
				{
					throw new NullReferenceException("Fund group is null");
				}
				if (fundGroup.ID < 1)
				{
					throw new ArgumentException("Invalid fund group ID");
				}
				parameters = new SqlParameter[1];
				parameters[0] = new SqlParameter("@iFundGroupID", SqlDbType.Int);
				parameters[0].Value = fundGroup.ID;

				using (SqlDataReader dataReader =
					SqlHelper.ExecuteReader(this.ConnectionString, CommandType.StoredProcedure, loadSp, parameters))
				{
					DistributionFileCollection distFiles = fundGroup.DistributionFiles;
					if (distFiles == null)
					{
						throw new NullReferenceException("Distribution Files collection is null");
					}

					// Create the fund collection from the data values.
					SafeDataReader safeReader = new SafeDataReader(dataReader);
					while (dataReader.Read())
					{
						try
						{
							DistributionFile distFile = new FundGroupDistributionFile(
																			safeReader.GetInt32("fileID"),
							                                                safeReader.GetString("FileDesc"), safeReader.GetString("Filename"),
							                                                safeReader.GetString("Filepath"), DistributionFileStatuses.Unavailable,
							                                                null,
							                                                safeReader.ColumnExists("FundGroupNumberRequired") ? safeReader.GetBoolean("FundGroupNumberRequired") : false,
							                                                safeReader.ColumnExists("DecimalPlacesRequired") ? safeReader.GetBoolean("DecimalPlacesRequired") : false,
							                                                safeReader.ColumnExists("SignificantDecimalPlacesRequired") ? safeReader.GetBoolean("SignificantDecimalPlacesRequired") : false,
							                                                safeReader.ColumnExists("MajorDenominationRequired") ? safeReader.GetBoolean("MajorDenominationRequired") : false,
																			safeReader.ColumnExists("FundGroupNumber") ? (object) safeReader.GetInt32("FundGroupNumber") : null,
																			safeReader.ColumnExists("UseMajorDenomination") ? safeReader.GetBoolean("UseMajorDenomination") : false,
																			safeReader.ColumnExists("NumberOfDecimalPlaces") ? safeReader.GetInt16("NumberOfDecimalPlaces") : 1,
																			safeReader.ColumnExists("NumberOfSignificantDecimalPlaces") ? safeReader.GetInt16("NumberOfSignificantDecimalPlaces") : 1,
							                                                safeReader.GetTimestamp("ts"));

							distFiles.Add(distFile);

						}
						catch (SchemaMismatchException ex)
						{
							// add database info to the thrown exception
							throw new SchemaMismatchException(ex.Message, DatabaseInfo, ex.Column, ex);
						}
					}
				}

			}
			catch (SqlException ex)
			{
				this.ThrowDBException(ex, ex.Procedure, parameters);
			}
			finally
			{
				T.X();
			}
		}

		#endregion

		#region SaveMethods

		/// <summary>
		/// Saves the ASSOCIATION to distribution files only, not any data to do with the files themselves.
		/// Is invoked within a transaction only as associations are saved after the main entity is saved.
		/// Does this by deleting existing associations then reinserting.
		/// </summary>
		/// <param name="fundGroup">the fund group containing the distribution files collection</param>
		/// <param name="txn">the transaction within which this association save is enlisted</param>
		/// <param name="forFundGroupInsert">An optimisation - if this is set to true, then there is no need to delete
		/// existing associateions thereby saving a round trip and sql server sp invoke</param>
		/// <exception cref="ConcurrencyViolationException">Thrown if the record being saved was changed by someone else since being loaded</exception>
		/// <exception cref="ConstraintViolationException">Thrown if the constraint violation (primary key, foreign key or check) occurred during the save</exception>
		/// <exception cref="InvalidSqlParameterException">Thrown if one of the parameters was of the incorrect value, type or length; or was null</exception>
		/// <exception cref="DatabaseException">Thrown if other unclassified Sql error occurs</exception>
		/// <exception cref="ArgumentException">Invalid fund group id</exception>
		/// <exception cref="NullReferenceException">Fund group or fund group collection is null</exception>
		protected void SaveDistributionFileAssociations(FundGroup fundGroup, SqlTransaction txn, bool forFundGroupInsert)
		{
			SqlParameter[] spParams = null;
			string spName = string.Empty;
			FundGroupDistributionFile currentFile=null;

			T.E();
			try
			{
				if (fundGroup == null)
				{
					throw new NullReferenceException("Fund group is null");
				}
				DistributionFileCollection distFiles = fundGroup.DistributionFiles;
				if (distFiles == null)
				{
					throw new NullReferenceException("Fund Group Collection is null");
				}
				if (fundGroup.ID < 1)
				{
					throw new ArgumentException("Invalid fund group ID");
				}
				if (!forFundGroupInsert)
				{
					spParams = new SqlParameter[1];
					spParams[0] = new SqlParameter("@iFundGroupID", SqlDbType.Int);
					spParams[0].Value = fundGroup.ID;

					spName = "usp_DistributionFilesDeleteForFundGroupID";
					SqlHelper.ExecuteNonQuery(txn, CommandType.StoredProcedure, spName, spParams);
				}

				spName = "usp_DistributionFileAssociateFundGroup";
				spParams = new SqlParameter[6];
				spParams[0] = new SqlParameter("@FileID", SqlDbType.Int);
				spParams[1] = new SqlParameter("@FundGroupID", SqlDbType.Int);
				spParams[2] = new SqlParameter("@FundGroupNumber", SqlDbType.Int);
				spParams[3] = new SqlParameter("@UseMajorDenomination", SqlDbType.Bit);
				spParams[4] = new SqlParameter("@NumberOfDecimalPlaces", SqlDbType.Int);
				spParams[5] = new SqlParameter("@NumberOfSignificantDecimalPlaces", SqlDbType.Int);

				spParams[1].Value = fundGroup.ID;

				//for loop implemented in this way is more efficient than foreach
				for (int i = 0; i < distFiles.Count; i++)
				{
					FundGroupDistributionFile distFile = distFiles[i] as FundGroupDistributionFile;

					currentFile=distFile;
					spParams[0].Value = distFile.FileID;
					spParams[2].Value = distFile.FundGroupNumberRequired ? distFile.FundGroupNumber:null;
					spParams[3].Value = distFile.MajorDenomination;
					spParams[4].Value = distFile.NumberOfDecimalPlaces;
					spParams[5].Value = distFile.NumberOfSignificantDecimalPlaces;

					SqlHelper.ExecuteNonQuery(txn, CommandType.StoredProcedure, spName, spParams);
				}
			}
			catch (SqlException ex)
			{
				if (ex.Number==(int) DatabaseError.CustomError)
				{
					throw new FundGroupFileAssociationException(fundGroup,currentFile,ex);
				}
				else
				{
					ThrowDBException(ex, ex.Procedure, spParams);
				}
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Deletes all distribution file associations for this fund group
		/// </summary>
		/// <param name="fundGroup"></param>
		/// <param name="txn"></param>
		/// <exception cref="ArgumentException">Invalid fund group id</exception>
		/// <exception cref="NullReferenceException">Fund group or fund group collection is null</exception>
		protected void DeleteDistributionFileAssociations(FundGroup fundGroup, SqlTransaction txn)
		{
			T.E();
			SqlParameter[] spParams = null;
			const string spName = "usp_DistributionFilesDeleteForFundGroupID";
			try
			{
				if (fundGroup == null)
				{
					throw new NullReferenceException("Fund group is null");
				}
				if (fundGroup.ID < 1)
				{
					throw new ArgumentException("Invalid fund group ID");
				}
				spParams = new SqlParameter[1];
				spParams[0] = new SqlParameter("@iFundGroupID", SqlDbType.Int);
				spParams[0].Value = fundGroup.ID;
				SqlHelper.ExecuteNonQuery(txn, CommandType.StoredProcedure, spName, spParams);

			}
			catch (SqlException ex)
			{
				//let calling client rollback the transaction
				this.ThrowDBException(ex, ex.Procedure, spParams);
			}
			finally
			{
				T.X();
			}
		}

		#endregion

		#region Helper methods

		/// <summary>
		/// Translates the database fund group type code to a recognised enum.
		/// </summary>
		/// <param name="fundGroupType">The fund group type as returned from the database.</param>
		/// <returns>The FundgroupType enum for the fund grouptype.</returns>
		protected static FundGroupFactory.FundGroupTypes resolveFundGroupType(string fundGroupType)
		{
			T.E();
			switch (fundGroupType)
			{
				case "A":
					T.X();
					return FundGroupFactory.FundGroupTypes.Asset;
				case "I":
					T.X();
					return FundGroupFactory.FundGroupTypes.Individual;
				default:
					throw new ArgumentOutOfRangeException("Type", "Unknown Fund Group type");
			}
		}

		/// <summary>
		/// Translates the enum to a database fund group type code.
		/// </summary>
		/// <param name="fundGroupType">The fund group type.</param>
		/// <returns>The fund group type as used in the database.</returns>
		protected static string resolveFundGroupType(Type fundGroupType)
		{
			T.E();
			if (fundGroupType == typeof (AssetFundGroup))
			{
				T.X();
				return "A";
			}
			else if (fundGroupType == typeof (IndividualFundGroup))
			{
				T.X();
				return "I";
			}
			else
			{
				throw new ArgumentOutOfRangeException("Type", "Unknown Fund Group type");
			}
		}

		#endregion

		#region ExceptionMessages

		/// <summary>
		/// Additional error text to concatenate on to error strings
		/// </summary>
		protected string additionalErrorText = string.Empty;

		/// <summary>
		/// Gets the concurrency violation exception message.
		/// </summary>
		/// <returns></returns>
		protected override string GetConcurrencyViolationExceptionMessage()
		{
			return string.Format("A concurrency violation occurred whilst saving {0}", additionalErrorText);
		}

		/// <summary>
		/// Gets the constraint violation duplicate key exception message.
		/// </summary>
		/// <returns></returns>
		protected override string GetConstraintViolationDuplicateKeyExceptionMessage()
		{
			return string.Format("A duplicate key violation occurred whilst saving {0}", additionalErrorText);
		}

		/// <summary>
		/// Gets the constraint violation exception message.
		/// </summary>
		/// <returns></returns>
		protected override string GetConstraintViolationExceptionMessage()
		{
			return string.Format("A constraint violation occurred whilst saving {0}", additionalErrorText);
		}

		/// <summary>
		/// Gets the database exception message.
		/// </summary>
		/// <returns></returns>
		protected override string GetDatabaseExceptionMessage()
		{
			return string.Format("A database exception occurred in fund group persister. {0}", additionalErrorText);
		}

		/// <summary>
		/// Gets the null parameter exception message.
		/// </summary>
		/// <returns></returns>
		protected override string GetNullParameterExceptionMessage()
		{
			return string.Format("A null parameter was passed to a stored procedure in fund group persister. {0}", additionalErrorText);
		}

		#endregion ExceptionMessages
	}
}