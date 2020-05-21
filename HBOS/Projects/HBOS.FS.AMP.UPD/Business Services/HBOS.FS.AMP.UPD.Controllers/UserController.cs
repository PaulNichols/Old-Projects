using System.Collections;
using HBOS.FS.AMP.UPD.Persistence;
using HBOS.FS.AMP.UPD.Types.Lookups;
using HBOS.FS.AMP.UPD.Types.Users;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Controllers
{
	/// <summary>
	/// Controller object for all things Fund orientated.  This is used to
	/// keep the logical layers seperate.  
	/// The controller will handle calls to the business and data layers.
	/// </summary>
	public class UserController
	{
		/// <summary>
		/// Contstructor
		/// </summary>
		public UserController()
		{
			//
			// No constructor logic
			//
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static bool UserIsValid(string connectionString)
		{
			T.E();
			try
			{
				UserPersister dataPersister = new UserPersister(connectionString);
				return dataPersister.UserIsValid();
			}
			finally
			{
				T.X();
			}
		}


		/// <summary>
		/// Return the user permissions for the user and the current company.
		/// </summary>
		/// <param name="connectionString">Connection string.</param>
		/// <param name="companyId">Company id.</param>
		/// <param name="logOnId">Log on id.</param>
		/// <returns></returns>
		public static Hashtable GrantedPermissionsAttributeCollection(string connectionString, string companyId, string logOnId)
		{
			T.E();
			try
			{
				UserPersister dataPersister = new UserPersister(connectionString);
				return dataPersister.UserPermissions(companyId, logOnId);
			}
			finally
			{
				T.X();
			}

		}

		/// <summary>
		/// Get the Company that the current user last chose
		/// </summary>
		/// <param name="connectionString"></param>
		public static CompanyDetails GetLastCompany(string connectionString)
		{
			T.E();
			try
			{
				UserPersister dataPersister = new UserPersister(connectionString);
				return new CompanyDetails(dataPersister.GetLastCompany());
			}
			finally
			{
				T.X();
			}

		}

		/// <summary>
		/// Set the Company that the current user last chose
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="companyId"></param>
		public static void SetLastCompany(string connectionString, string companyId)
		{
			T.E();
			try
			{
				UserPersister dataPersister = new UserPersister(connectionString);
				dataPersister.SetLastCompany(companyId);
			}
			finally
			{
				T.X();
			}

		}


		/// <summary>
		/// Loads a sing user by Login ID.
		/// </summary>
		/// <param name="connectionString">Connection string.</param>
		/// <param name="loginID">Login ID.</param>
		/// <param name="companyCode">Company code.</param>
		/// <returns></returns>
		public static User LoadUser(string connectionString, string loginID, string companyCode)
		{
			T.E();
			try
			{
				UserPersister dataPersister = new UserPersister(connectionString);
				return dataPersister.LoadUser(loginID, companyCode);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Get a collection of users filtered by the company code/id
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="CompanyID"></param>
		/// <returns>UserCollection</returns>
		public static SimpleStringLookupCollection LoadUsers(string connectionString, string CompanyID)
		{
			T.E();
			try
			{
				UserPersister dataPersister = new UserPersister(connectionString);
				return dataPersister.LoadAllUsersForCompany(CompanyID);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Save the users back to the Data Source
		/// </summary>
		/// <param name="users">The users to save</param>
		/// <param name="companyCode">Company code that the users is being deleted from for permission reasons</param>
		/// <param name="connectionString">The connection string to the database to save them in</param>
		public static void SaveUsers(UserCollection users, string companyCode,string connectionString)
		{
			// TODO: Add transaction functionality here
			T.E();
			try
			{
				UserPersister newUserPersister = new UserPersister(connectionString);
				newUserPersister.CompanyCode=companyCode;
				newUserPersister.Save(users);
			}
			finally
			{
				T.X();
			}
		}


		/// <summary>
		/// Gets the all permissions those granted to the user  will be indeicated.
		/// </summary>
		/// <param name="user">User.</param>
		/// <param name="companyCode">Company code.</param>
		/// <param name="connectionString">Connection string.</param>
		/// <returns></returns>
		public static Permission GetAllPermissions(User user, string companyCode, string connectionString)
		{
			UserPersister newUserPersister = new UserPersister(connectionString);
			return newUserPersister.GetAllPermissions(user, companyCode);
		}

		/// <summary>
		/// Loads the export data.
		/// </summary>
		/// <param name="connectionString">Connection string.</param>
		/// <param name="companyCode">Company code.</param>
		/// <returns></returns>
		public static UserCollection LoadExportData(string connectionString, string companyCode)
		{
			UserPersister newUserPersister = new UserPersister(connectionString);
			return new UserCollection(newUserPersister.LoadExportData(companyCode));
		}
	}
}