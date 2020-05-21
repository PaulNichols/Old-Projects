using System;
using System.Collections.Specialized;
using System.Data;
using System.Transactions;
using ASPNET.SQLDataAccessProvider;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Discovery.BusinessObjects.Controllers
{
    public class RoleProvider : System.Web.Security.RoleProvider
    {
        private string appName = "Security";

        public override string ApplicationName
        {
            get { return appName; }
            set
            {


            }
        }

        public override void CreateRole(string roleName)
        {
            try
            {
                DataAccessProvider.Instance().CreateRole(ApplicationName, roleName);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    DataAccessProvider.Instance().AddUsersToRoles(ApplicationName, string.Join(",", roleNames), string.Join(",", usernames));
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            bool success = false;
             try
            {
                success= DataAccessProvider.Instance().DeleteRole(ApplicationName,roleName, throwOnPopulatedRole);

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return success;
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            return new string[0];
        }

        public override string[] GetAllRoles()
        {
            string[] roleArray = new string[0];
            StringCollection roles = new StringCollection();
            IDataReader reader = null;
            try
            {

                reader = DataAccessProvider.Instance().GetAllRoles(ApplicationName);
                while (reader.Read())
                {
                    roles.Add(reader.GetString(0));
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            if (roles.Count > 0)
            {
                roleArray = new string[roles.Count];
                roles.CopyTo(roleArray, 0);
            }

            return roleArray;
        }

        public override string[] GetRolesForUser(string username)
        {
            string[] roleArray = new string[0];
            StringCollection roles = new StringCollection();
            IDataReader reader = null;
            try
            {

                reader = DataAccessProvider.Instance().GetRolesForUser(ApplicationName, username);
                while (reader.Read())
                {
                    roles.Add(reader.GetString(0));
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            if (roles.Count > 0)
            {
                roleArray = new string[roles.Count];
                roles.CopyTo(roleArray, 0);
            }

            return roleArray;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            string[] usersArray = new string[0];
            StringCollection users = new StringCollection();
            IDataReader reader = null;

            try
            {

                reader = DataAccessProvider.Instance().GetUsersInRole(ApplicationName, roleName);
                while (reader.Read())
                {
                    users.Add(reader.GetString(0));
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            if (users.Count > 0)
            {
                usersArray = new string[users.Count];
                users.CopyTo(usersArray, 0);
            }

            return usersArray;
        }

        public override bool IsUserInRole(string userName, string roleName)
        {
            bool isUserInRole = false;
            try
            {
                isUserInRole = DataAccessProvider.Instance().IsUserInRole(ApplicationName, roleName, userName);

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            return isUserInRole;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
               try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    DataAccessProvider.Instance().RemoveUsersFromRoles(ApplicationName, string.Join(",", roleNames), string.Join(",", usernames));
                    scope.Complete();
                    
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

        }

        public override bool RoleExists(string roleName)
        {
             bool roleExists = false;
            try
            {
                roleExists = DataAccessProvider.Instance().RoleExists(ApplicationName, roleName);

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            return roleExists;
        }
    }
}