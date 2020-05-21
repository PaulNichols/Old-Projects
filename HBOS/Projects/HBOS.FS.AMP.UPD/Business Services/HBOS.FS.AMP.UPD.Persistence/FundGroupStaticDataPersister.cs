using System;
using System.Data;
using System.Data.SqlClient;
using HBOS.FS.AMP.Entities;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Types.FundGroups;
using HBOS.FS.Support.Tex;
using Microsoft.ApplicationBlocks.Data;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// Summary description for FundGroupStaticDataPersister.
	/// </summary>
	public class FundGroupStaticDataPersister : FundGroupPersister
	{
		/// <summary>
		/// Creates a new <see cref="FundGroupStaticDataPersister"/> instance.
		/// </summary>
		/// <param name="connectionString">Connection string.</param>
		public FundGroupStaticDataPersister(string connectionString) : base(connectionString)
		{
		}

		#region Load methods

		/// <summary>
		/// Loads the specified fund group for a given id.
		/// </summary>
		/// <param name="fundGroupId">Fund group id.</param>
		/// <returns></returns>
		public FundGroup Load(int fundGroupId)
		{
			T.E();
			FundGroup result = null;
			try
			{
				SqlParameter[] parms = new SqlParameter[1];
				parms[0] = new SqlParameter("@iFundGroupID", fundGroupId);

				result = (FundGroup) this.LoadEntity("usp_FundGroupGetStaticData", parms);

				if (result != null) //defensive coding
				{
					//also load the collection of distribution files associated with this fund group
					this.LoadDistributionFiles(result);
				}

			}
			finally
			{
				T.X();
			}
			return result;
		}

		//public 

		#endregion Load methods

		#region Validation Methods

		/// <summary>
		/// Checks to see if the updated fullname or shortname already exists in the system for another fund group
		/// </summary>
		/// <param name="fundGroupID">ID of existing fund group (or zero if new fund group)</param>
		/// <param name="fullName"></param>
		/// <param name="shortName"></param>
		/// <param name="fullNameExists"></param>
		/// <param name="shortNameExists"></param>
		public void CheckForDuplication(int fundGroupID, string fullName, string shortName,
		                                out bool fullNameExists, out bool shortNameExists)
		{
			T.E();
			SqlParameter[] parameters = null;
			fullNameExists = false;
			shortNameExists = false;
			try
			{
				const string storedProcName = "dbo.usp_FundGroupCheckForDuplication";

				parameters = new SqlParameter[6];
				parameters[0] = new SqlParameter("@fundGroupID", SqlDbType.Int);
				parameters[0].Value = fundGroupID;
				parameters[1] = new SqlParameter("@fullName", SqlDbType.VarChar, 100);
				parameters[1].Value = fullName;
				parameters[2] = new SqlParameter("@shortName", SqlDbType.VarChar, 50);
				parameters[2].Value = shortName;
				parameters[3] = new SqlParameter("@fullNameExists", SqlDbType.Bit);
				parameters[3].Direction = ParameterDirection.Output;
				parameters[4] = new SqlParameter("@shortNameExists", SqlDbType.Bit);
				parameters[4].Direction = ParameterDirection.Output;

				SqlHelper.ExecuteNonQuery(this.ConnectionString, CommandType.StoredProcedure, storedProcName, parameters);

				fullNameExists = (bool) parameters[3].Value;
				shortNameExists = (bool) parameters[4].Value;

			}
			catch (SqlException ex)
			{
				this.ThrowDBException(ex, ex.Procedure, parameters);
			}
			finally
			{
				T.E();
			}
		}

		#endregion

		#region Save methods

		/// <summary>
		/// Saves a fundgroup to the database.
		/// </summary>
		/// <param name="fundGroup">The fund group to save</param>
		/// <exception cref="ArgumentNullException">Thrown when the fund group is null</exception>
		/// <exception cref="ArgumentException">Thrown if the object passed is not a fund group</exception>
		/// <exception cref="ConcurrencyViolationException">Thrown if the record being saved was changed by someone else since being loaded</exception>
		/// <exception cref="ConstraintViolationException">Thrown if the constraint violation (primary key, foreign key or check) occurred during the save</exception>
		/// <exception cref="InvalidSqlParameterException">Thrown if one of the parameters was of the incorrect value, type or length; or was null</exception>
		/// <exception cref="DatabaseException">Thrown if other unclassified Sql error occurs</exception>
		public void SaveFundGroup(FundGroup fundGroup)
		{
			T.E();
			try
			{
				this.SaveEntity(fundGroup);

				CacheHelper cacheHelper = new CacheHelper();
				cacheHelper.FlushFundGroups();
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Inserts the FundGroup entity into the database.
		/// </summary>
		/// <param name="entity">Entity to insert.</param>
		/// <param name="transaction">Transaction to use.</param>
		protected override void InsertEntity(IEntityBase entity, SqlTransaction transaction)
		{
			T.E();
			if (entity == null) throw new ArgumentNullException("entity", "Cannot insert a null fund group");
			if (!(entity is FundGroup)) throw new ArgumentException(string.Format("Cannot convert type {0} to type Fund Group for insertion", entity.GetType()), "entity");

			FundGroup fundGroup = (FundGroup) entity;
			SqlParameter[] spParams = null;
			string spName = string.Empty;

			try
			{
				additionalErrorText = string.Format("{0}: {1}", fundGroup.GetType().ToString(), fundGroup.FullName);

				spParams = new SqlParameter[7];
				spName = "usp_FundGroupCreate";

				spParams[0] = new SqlParameter("@sFullName", SqlDbType.VarChar, 100);
				spParams[1] = new SqlParameter("@sCompanyCode", SqlDbType.VarChar, 10);
				spParams[2] = new SqlParameter("@sShortName", SqlDbType.VarChar, 50);
				spParams[3] = new SqlParameter("@sContainsType", SqlDbType.Char, 1);
				spParams[4] = new SqlParameter("@bForRelease", SqlDbType.Bit);
				spParams[5] = new SqlParameter("@bAllowSelectAllAuthorisation", SqlDbType.Bit);
				spParams[6] = new SqlParameter("@retValue", SqlDbType.Int, 0);
				spParams[6].Direction = ParameterDirection.ReturnValue;

				spParams[0].Value = fundGroup.FullName;
				spParams[1].Value = fundGroup.CompanyCode;
				spParams[2].Value = fundGroup.ShortName;
				spParams[3].Value = resolveFundGroupType(fundGroup.GetType());
				spParams[4].Value = fundGroup.ForRelease;
				spParams[5].Value = fundGroup.AllowSelectAllAuthorisation;

				SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, spParams);

				fundGroup.ID = (int) spParams[6].Value;

				this.SaveDistributionFileAssociations(fundGroup, transaction, true);
			}
			catch (SqlException ex)
			{
				this.ThrowDBException(ex, spName, spParams);
			}
			T.X();
		}

		/// <summary>
		/// Deletes the FundGroup entity from the database.
		/// </summary>
		/// <param name="entity">Entity to delete.</param>
		/// <param name="transaction">Transaction to use.</param>
		protected override void DeleteEntity(IEntityBase entity, SqlTransaction transaction)
		{
			T.E();
			if (entity == null) throw new ArgumentNullException("entity", "Cannot delete a null fund group");
			if (!(entity is FundGroup)) throw new ArgumentException(string.Format("Cannot convert type {0} to type Fund Group for deletion", entity.GetType()), "entity");

			FundGroup fundGroup = (FundGroup) entity;
			SqlParameter[] spParams = null;
			string spName = string.Empty;

			try
			{
				DeleteDistributionFileAssociations(fundGroup, transaction);

				additionalErrorText = string.Format("{0}: {1}", fundGroup.GetType().ToString(), fundGroup.FullName);

				spParams = new SqlParameter[2];
				spName = "usp_FundGroupDelete";

				spParams[0] = new SqlParameter("@iFundGroupID", SqlDbType.Int);
				spParams[1] = new SqlParameter("@ts", SqlDbType.Timestamp);
				spParams[0].Value = fundGroup.ID;
				spParams[1].Value = fundGroup.TimeStamp;

				SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, spParams);
			}
			catch (SqlException ex)
			{
				this.ThrowDBException(ex, spName, spParams);
			}
			T.X();
		}

		/// <summary>
		/// Updates the FundGroup entity on the database.
		/// </summary>
		/// <param name="entity">Entity to update.</param>
		/// <param name="transaction">Transaction to use.</param>
		protected override void UpdateEntity(IEntityBase entity, SqlTransaction transaction)
		{
			T.E();
			if (entity == null) throw new ArgumentNullException("entity", "Cannot delete a null fund group");
			if (!(entity is FundGroup)) throw new ArgumentException(string.Format("Cannot convert type {0} to type Fund Group for deletion", entity.GetType()), "entity");

			FundGroup fundGroup = (FundGroup) entity;
			SqlParameter[] spParams = null;
			string spName = string.Empty;

			try
			{
				additionalErrorText = string.Format("{0}: {1}", fundGroup.GetType().ToString(), fundGroup.FullName);


				spParams = new SqlParameter[9];
				spName = "usp_FundGroupUpdate";

				spParams[0] = new SqlParameter("@iFundGroupID", SqlDbType.Int);
				spParams[1] = new SqlParameter("@cContainsType", SqlDbType.Char, 1);
				spParams[2] = new SqlParameter("@sFullName", SqlDbType.VarChar, 100);
				spParams[3] = new SqlParameter("@sShortName", SqlDbType.VarChar, 50);
				spParams[4] = new SqlParameter("@ts", SqlDbType.Timestamp);
				spParams[5] = new SqlParameter("@bForRelease", SqlDbType.Bit);
				spParams[6] = new SqlParameter("@bAllowSelectAllAuthorisation", SqlDbType.Bit);


				spParams[0].Value = fundGroup.ID;
				spParams[1].Value = resolveFundGroupType(fundGroup.GetType());
				spParams[2].Value = fundGroup.FullName;
				spParams[3].Value = fundGroup.ShortName;
				spParams[4].Value = fundGroup.TimeStamp;
				spParams[5].Value = fundGroup.ForRelease;
				spParams[6].Value = fundGroup.AllowSelectAllAuthorisation;


				SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, spParams);

				this.SaveDistributionFileAssociations(fundGroup, transaction, false);

			}
			catch (SqlException ex)
			{
				this.ThrowDBException(ex, spName, spParams);
			}
			T.X();
		}

		#endregion
	}
}