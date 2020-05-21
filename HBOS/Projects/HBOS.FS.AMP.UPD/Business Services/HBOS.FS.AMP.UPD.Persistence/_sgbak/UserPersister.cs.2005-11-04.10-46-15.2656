using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using HBOS.FS.AMP.Entities;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.AMP.UPD.Types.Companies;
using HBOS.FS.AMP.UPD.Types.Lookups;
using HBOS.FS.AMP.UPD.Types.Users;
using HBOS.FS.Support.Tex;
using Microsoft.ApplicationBlocks.Data;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// This is the persister object for saving/retireiving user data from/to the datasource 
	/// </summary>
	public class UserPersister : EntityPersister
	{
		private string m_CompanyCode;

		/// <summary>
		/// Used for deletion of user permissions for a given company
		/// </summary>
		public string CompanyCode
		{
			get { return m_CompanyCode; }
			set { m_CompanyCode = value; }
		}

		#region Constructors

		/// <summary>
		/// Constructor used to initialise the ConnectionString property
		/// </summary>
		/// <param name="connectionString"></param>
		public UserPersister(string connectionString) : base(connectionString)
		{
		}

		#endregion

		#region Load Methods

		/// <summary>
		/// Loads the export data.
		/// </summary>
		/// <param name="companyCode">Company code.</param>
		/// <returns></returns>
		public ExportStaticDataUserDetails[] LoadExportData(string companyCode)
		{
			T.E();
			const string Sp = "usp_UsersStaticDataExport";
			SqlParameter[] parameters = new SqlParameter[1];
			ArrayList returnData = new ArrayList();


			// Set up the parameters.
			parameters[0] = new SqlParameter("@CompanyCode", SqlDbType.Char, 50);
			parameters[0].Value = companyCode;

			try
			{
				using (SqlDataReader dataReader =
					SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Sp, parameters))
				{
					while (dataReader.Read())
					{
						SafeDataReader safeReader = new SafeDataReader(dataReader);

						returnData.Add(new ExportStaticDataUserDetails(
							safeReader.GetString("loginID"),
							safeReader.GetString("userName"),
							safeReader.GetString("DisplayName"),
							safeReader.GetBoolean("Granted")));
					}
				}
			}
			catch
			{
				throw;
			}

			finally
			{
				T.X();
			}
			return ((ExportStaticDataUserDetails[]) returnData.ToArray(typeof (ExportStaticDataUserDetails)));
		}

		/// <summary>
		/// Loads the a single user by Login ID.
		/// </summary>
		/// <param name="loginId">Login id.</param>
		/// <param name="companyCode">Company code.</param>
		/// <returns></returns>
		public User LoadUser(string loginId, string companyCode)
		{
			T.E();
			const string spName = "usp_UserGetStaticData";
			SqlParameter[] spParameters = new SqlParameter[1];

			// Set up the stored procedure parameters.
			spParameters[0] = new SqlParameter("@loginId", SqlDbType.VarChar, 50);
			spParameters[0].Value = loginId;
			// Create the fund object.
			User user = null;

			try
			{
				user = (User) this.LoadEntity(spName, spParameters);

				// Test for valid object
				if (user == null)
				{
					throw new ArgumentException(string.Format("Failed to load a user with the login Id '{0}'", loginId));
				}
			}
			catch (SqlException ex)
			{
				ThrowDBException(ex, spName, spParameters);
			}
			T.X();
			return user;
		}

		/// <summary>
		/// Loads the all users for company.
		/// </summary>
		/// <param name="companyCode">Company code.</param>
		/// <returns></returns>
		public SimpleStringLookupCollection LoadAllUsersForCompany(string companyCode)
		{
			T.E();

			// Create the users collection.
			SimpleStringLookupCollection usersList = new SimpleStringLookupCollection();

			string spName = "usp_UsersGetAll";

			this.LoadEntityCollection(spName, usersList);

			T.X();

			return usersList;
		}

		/// <summary>
		/// Creates a user from the supplied data
		/// </summary>
		/// <param name="reader">The reader containing the data.</param>
		/// <returns></returns>
		protected override object CreateEntity(SafeDataReader reader)
		{
			T.E();
			User newUser = new User(
				reader.GetString("loginID"),
				reader.GetString("userName"),
				reader.GetBoolean("deleted"),
				reader.GetString("LastChangedBy"),
				reader.GetDateTime("LastChangedDate"),
				reader.GetTimestamp("TS")
				);

			T.X();
			return newUser;
		}

		/// <summary>
		/// Gets the all permissions those granted to the user  will be indeicated.
		/// </summary>
		/// <param name="user">User.</param>
		/// <param name="companyCode">Company code.</param>
		/// <returns></returns>
		public Permission GetAllPermissions(User user, string companyCode)
		{
			user.Permissions = null;

			T.E();
			const string spName = "usp_UserPermissionsGetAll";
			SqlParameter[] spParameters = new SqlParameter[2];

			// Set up the stored procedure parameters.
			spParameters[0] = new SqlParameter("@LoginId", SqlDbType.VarChar, 50);
			spParameters[0].Value = user.LogOnID;
			spParameters[1] = new SqlParameter("@CompanyCode", SqlDbType.VarChar, 10);
			spParameters[1].Value = companyCode;

			try
			{
				using (SqlDataReader dataReader =
					SqlHelper.ExecuteReader(this.ConnectionString, CommandType.StoredProcedure, spName, spParameters))
				{
					// Create the fund collection from the data values.
					SafeDataReader safeReader = new SafeDataReader(dataReader);
					Permission permissionById = null;
					while (dataReader.Read())
					{
						Permission newPermission;
						try
						{
							if (safeReader.GetInt32("ParentId") == 0)
							{
								newPermission = new PermissionGroup(companyCode, safeReader.GetInt32("PermissionId"), safeReader.GetString("DisplayName"));

								user.Permissions = newPermission;
								continue;
							}
							else
							{
								if (safeReader.GetBoolean("IsGroup"))
								{
									newPermission = new PermissionGroup(companyCode, safeReader.GetInt32("PermissionId"), safeReader.GetString("DisplayName"));
								}
								else
								{
									newPermission = new UserPermission(companyCode, safeReader.GetInt32("PermissionId"), safeReader.GetString("DisplayName"), safeReader.GetBoolean("Granted"), safeReader.GetString("UniqueName"));
								}

								if (permissionById == null || permissionById.PermissionId != safeReader.GetInt32("ParentId"))
								{
									permissionById = ((PermissionGroup) user.Permissions)[safeReader.GetInt32("ParentId")];
								}

								if (permissionById is PermissionGroup)
								{
									permissionById.Add(newPermission);
								}
							}
						}
						catch
						{
							throw;
						}
					}
				}

			}
			catch (SqlException ex)
			{
				ThrowDBException(ex, spName, spParameters);
			}
			T.X();

			return user.Permissions;
		}

		#endregion

		#region ExceptionHandling

		/// <summary>
		/// Override the base database exception message
		/// </summary>
		/// <returns>String</returns>
		protected override string GetDatabaseExceptionMessage()
		{
			return "Failed to build the users list";
		}

		#endregion

		#region Update Methods

		/// <summary>
		/// This routine receives a collection of users to be peristed to the datasource
		/// </summary>
		/// <param name="users">List of users to save</param>
		public void Save(UserCollection users)
		{
			this.SaveEntityCollection(users);
		}

		/// <summary>
		/// Insert a new user into the database
		/// The permissions are now updated from SaveEntity()
		/// </summary>
		/// <param name="transaction">The transaction used for the persistence, allowing transactional rollback.</param>
		/// <param name="entity">Persitee user object</param>
		/// <returns>Success flag</returns>
		protected override void InsertEntity(IEntityBase entity, SqlTransaction transaction)
		{
			T.E();
			if (entity == null) throw new ArgumentNullException("entity", "Cannot insert null User");
			if (!(entity is User)) throw new ArgumentException("Incorrect type, expecting User, was " + entity.GetType().ToString(), "entity");
			User user = (User) entity;

			if (user.LogOnID == string.Empty || user.UserName == string.Empty)
			{
				throw new UsersSaveException("Both the User Name and Login must be populated", user);
			}

			// Create parameter object
			SqlParameter[] spParams = new SqlParameter[2];

			// This will make adding/moving/copying rows easier!!
			int paramCounter = 0;

			// Build parameters
			spParams[paramCounter] = new SqlParameter("@sLoginID", SqlDbType.VarChar, 50);
			spParams[paramCounter++].Value = user.LogOnID;
			spParams[paramCounter] = new SqlParameter("@sUserName", SqlDbType.VarChar, 50);
			spParams[paramCounter++].Value = user.UserName;

			try
			{
				T.Log("Executing stored procedure: usp_UsersCreate");
				SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, "usp_UsersCreate", spParams);
			}
			catch (SqlException ex)
			{
				this.ThrowDBException(ex, ex.Procedure, spParams);
			}
			finally
			{
				T.Log("Create user for login id: " + user.LogOnID);
				T.X();
			}
		}

		/// <summary>
		/// Delete a specified user from the database.
		/// </summary>
		/// <param name="transaction">The transaction used for the persistence, allowing transactional rollback.</param>
		/// <param name="entity">Persitee user object</param>
		/// <returns>Success flag</returns>
		protected override void DeleteEntity(IEntityBase entity, SqlTransaction transaction)
		{
			T.E();
			if (entity == null) throw new ArgumentNullException("entity", "Cannot insert null User");
			if (!(entity is User)) throw new ArgumentException("Incorrect type, expecting User, was " + entity.GetType().ToString(), "entity");
			User user = (User) entity;

			SqlParameter[] spParams = new SqlParameter[3];
			int paramCounter = 0;

			spParams[paramCounter] = new SqlParameter("@sLoginID", SqlDbType.VarChar, 50);
			spParams[paramCounter++].Value = user.LogOnID;
			spParams[paramCounter] = new SqlParameter("@ts", SqlDbType.Timestamp);
			spParams[paramCounter++].Value = user.TimeStamp;
			spParams[paramCounter] = new SqlParameter("@CompanyCode", SqlDbType.VarChar,10);
			spParams[paramCounter++].Value = CompanyCode;


			
			try
			{
				T.Log("Executing stored procedure: usp_UsersDelete");
				SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, "usp_UsersDelete", spParams);
			}
			catch (SqlException ex)
			{
				this.ThrowDBException(ex, ex.Procedure, spParams);
			}
			finally
			{
				T.Log("Delete user: " + user.LogOnID);
				T.X();
			}
		}

		/// <summary>
		/// Update a user in the database
		/// </summary>
		/// <param name="transaction">The transaction used for the persistence, allowing transactional rollback.</param>
		/// <param name="entity">Persitee user object</param>
		protected override void UpdateEntity(IEntityBase entity, SqlTransaction transaction)
		{
			T.E();
			if (entity == null) throw new ArgumentNullException("entity", "Cannot insert null User");
			if (!(entity is User)) throw new ArgumentException("Incorrect type, expecting User, was " + entity.GetType().ToString(), "entity");
			User user = (User) entity;

			SqlParameter[] spParams = new SqlParameter[4];
			int paramCounter = 0;

			spParams[paramCounter] = new SqlParameter("@sLoginID", SqlDbType.VarChar, 50);
			spParams[paramCounter++].Value = user.LogOnID;

			spParams[paramCounter] = new SqlParameter("@sUserName", SqlDbType.VarChar, 50);
			spParams[paramCounter++].Value = user.UserName;

			spParams[paramCounter] = new SqlParameter("@bDeleted", SqlDbType.Bit);
			spParams[paramCounter++].Value = user.IsDeleted;

			spParams[paramCounter] = new SqlParameter("@ts", SqlDbType.Timestamp);
			spParams[paramCounter].Direction = ParameterDirection.InputOutput;
			spParams[paramCounter].Value = user.TimeStamp;

			try
			{
				// Call update stored procedure
				if (user.IsDirty)
				{
					SqlHelper.ExecuteScalar(transaction, CommandType.StoredProcedure, "usp_UsersUpdate", spParams);
					user.TimeStamp = (byte[]) spParams[3].Value;
					user.IsDirty = false;
				}

			}
			catch (SqlException ex)
			{
				this.ThrowDBException(ex, ex.Procedure, spParams);
			}
			finally
			{
				T.Log("Update user: " + user.LogOnID);
				T.X();
			}
		}

		/// <summary>
		/// Overriden to ensure an update if only the permissions have changed
		/// </summary>
		/// <param name="entity">User entity.</param>
		/// <param name="transaction">Transaction.</param>
		protected internal override void SaveEntity(IEntityBase entity, SqlTransaction transaction)
		{
			T.E();
			base.SaveEntity(entity, transaction);

			if (!(entity is User)) throw new ArgumentException("Incorrect type, expecting User, was " + entity.GetType().ToString(), "entity");
			User user = (User) entity;

			try
			{
				persistPermissions(user.Permissions, transaction, user.LogOnID);
				user.IsDirty = false;
			}
			catch
			{
				throw;
			}
			T.X();
		}

		private void persistPermissions(Permission userPermissions, SqlTransaction transaction, string logOnID)
		{
			SqlParameter[] spParams = null;

			try
			{
				foreach (Permission permission in userPermissions)
				{
					if (permission is UserPermission)
					{
						if (permission.IsDirty)
						{
							spParams = UserPersister.permissionsParameters(permission, logOnID);
							SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, "usp_UserPermissionsUpdate", spParams);
							permission.IsDirty = false;
						}
					}
					else
					{
						persistPermissions(permission, transaction, logOnID);
					}
				}
			}
			catch (SqlException ex)
			{
				this.ThrowDBException(ex, ex.Procedure, spParams);
			}
		}

		private static SqlParameter[] permissionsParameters(Permission permissionToPersist, string logonId)
		{
			// This procedure centralises the building of the paramters
			// for the update/insert of the user permissions

			// we will now add the user permissions inside the same transaction
			// Create paremeter object

			SqlParameter[] spParams = null;
			spParams = new SqlParameter[4];

			// Build parameters
			spParams[0] = new SqlParameter("@loginID", SqlDbType.VarChar, 50);
			spParams[0].Value = logonId;

			spParams[1] = new SqlParameter("@CompanyCode", SqlDbType.VarChar, 10);
			spParams[1].Value = permissionToPersist.CompanyCode;

			spParams[2] = new SqlParameter("@PermissionId", SqlDbType.Int);
			spParams[2].Value = permissionToPersist.PermissionId;

			spParams[3] = new SqlParameter("@Grant", SqlDbType.Int);
			spParams[3].Value = permissionToPersist.Granted;

			return spParams;
		}

		#endregion

		#region Other public methods

		/// <summary>
		/// Set the last company the user chose
		/// </summary>
		public Company GetLastCompany()
		{
			Company lastCompany = null;
			string loadSp = "usp_UsersGetLastCompanyForLoginID";
			try
			{
				string lastCompanyCode = "";
				using (SqlDataReader companyReader = SqlHelper.ExecuteReader(
					this.ConnectionString,
					loadSp))
				{
					if (companyReader.Read())
						lastCompanyCode = companyReader["CompanyCode"].ToString();
				}

				CompanyPersister companyPersister = new CompanyPersister(ConnectionString);
				lastCompany = companyPersister.LoadCompany(lastCompanyCode);
			}
			catch (SqlException ex)
			{
				ThrowDBException(ex, loadSp);
			}
			finally
			{
				T.X();
			}
			return lastCompany;
		}

		/// <summary>
		/// Set the last company the user chose
		/// </summary>
		public void SetLastCompany(string companyCode)
		{
			string loadSp = "usp_UsersSetLastCompanyForLoginID";
			try
			{
				SqlParameter[] spParams = new SqlParameter[1];
				spParams[0] = new SqlParameter("@sCompanyID", SqlDbType.Char, 10);
				spParams[0].Value = companyCode;

				// And finally save the new company code against the user record in the database
				SqlHelper.ExecuteNonQuery(
					this.ConnectionString,
					CommandType.StoredProcedure,
					"usp_UsersSetLastCompanyForLoginID",
					spParams);

				CacheHelper cacheHelper = new CacheHelper();
				cacheHelper.FlushFundGroups();
			}
			catch (SqlException ex)
			{
				ThrowDBException(ex, loadSp);
			}
			finally
			{
				T.X();
			}
		}


		/// <summary>
		/// Return the user permissions for the user and the current company
		/// </summary>
		/// <param name="companyId">Company id.</param>
		/// <param name="logOnId">Log on id.</param>
		/// <returns></returns>
		public Hashtable UserPermissions(string companyId, string logOnId)
		{
			string loadSp = "usp_UserCompanyPermissions";
			Hashtable returnCollection = new Hashtable();

			try
			{
				// Get the permissions record for the combination of 
				// the Identity user and specified company ID
				SqlParameter[] spParameters = new SqlParameter[2];

				// Set up the stored procedure parameters.
				spParameters[0] = new SqlParameter("@LogOnID", SqlDbType.VarChar, 50);
				spParameters[0].Value = logOnId;

				spParameters[1] = new SqlParameter("@CompanyCode", SqlDbType.VarChar, 10);
				spParameters[1].Value = companyId;

				using (SqlDataReader dataReader =
					SqlHelper.ExecuteReader(this.ConnectionString, CommandType.StoredProcedure, loadSp, spParameters))
				{
					// Create the fund collection from the data values.
					SafeDataReader safeReader = new SafeDataReader(dataReader);
					while (dataReader.Read())
					{
						returnCollection.Add(safeReader.GetString("UniqueName").ToLower(), safeReader.GetString("UniqueName").ToLower());
					}
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
			return returnCollection;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public bool UserIsValid()
		{
			T.E();
			bool isValidUser = false;
			const string loadSp = "usp_userIsValid";
			try
			{
				using (SqlConnection connection = new SqlConnection(this.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand())
					{
						command.Connection = connection;
						command.CommandText = loadSp;
						command.CommandType = CommandType.StoredProcedure;
						connection.Open();
						isValidUser = Convert.ToBoolean(command.ExecuteScalar(),
						                                CultureInfo.InvariantCulture);
						connection.Close();
					}
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
			return isValidUser;

		}

		#endregion
	}
}