/*************************************************************************************************
 ** FILE:	SQLDataAccessProvider.cs
 ** DATE:	30/05/2006
 ** AUTHOR:	Lee Spring
 **
 **
 ** OVERVIEW:
 **
 ** MODIFICATION HISTORY:
 **
 ** Date:		Version:	Who:	Change:
 ** 30/5/06		1.0		    LAS	    Initial Version
 ************************************************************************************************/
using System;
using System.Configuration.Provider;
using System.Data;
using System.Data.SqlClient;
using Discovery.Utility.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace ASPNET.SQLDataAccessProvider
{
    public partial class SQLDataAccessProvider : DataAccessProvider
    {
        #region Provider Details

        protected override void Initialise(Provider provider)
        {
            // Read the attributes for this provider
            connectionString = provider.Attributes["connectionString"];
            upgradeConnectionString = provider.Attributes["upgradeConnectionString"];
            providerPath = provider.Attributes["provIderPath"];
            objectQualifier = provider.Attributes["objectQualifier"];
            databaseOwner = provider.Attributes["databaseOwner"];

            if (objectQualifier != "" && !objectQualifier.EndsWith("_"))
            {
                // Append "_" to qualifier name if qualifier specified and name does not end with "_"
                objectQualifier += "_";
            }

            if (databaseOwner != "" && !databaseOwner.EndsWith("."))
            {
                // Append "." to database owner if owner specified and does not end with "."
                databaseOwner += ".";
            }

            // See if we have an upgrade connection string
            if ("" == upgradeConnectionString)
            {
                upgradeConnectionString = connectionString;
            }
        }

        private string connectionString;
        private string upgradeConnectionString;
        private string providerPath;
        private string objectQualifier;
        private string databaseOwner;


        public string ConnectionString
        {
            get { return connectionString; }
        }

        public string ProviderPath
        {
            get { return providerPath; }
        }

        public string ObjectQualifier
        {
            get { return objectQualifier; }
        }

        public string DatabaseOwner
        {
            get { return databaseOwner; }
        }

        //private IDataReader ExecuteReader(string spNameWithoutQualifier)
        //{
        //    Database database = DatabaseFactory.CreateDatabase(ConnectionString);
        //    return database.ExecuteReader(String.Concat(ObjectQualifier, spNameWithoutQualifier), new object[] {});
        //}

        private IDataReader ExecuteReader(string spNameWithoutQualifier, params object[] sqlParameters)
        {
            Database database = DatabaseFactory.CreateDatabase(ConnectionString);
            return database.ExecuteReader(String.Concat(ObjectQualifier, spNameWithoutQualifier), sqlParameters);
        }

        //private bool ExecuteNonQuery(string spNameWithoutQualifier, params object[] sqlParameters)
        //{
        //    Database database = DatabaseFactory.CreateDatabase(ConnectionString);
        //    return
        //        (database.ExecuteNonQuery(String.Concat(ObjectQualifier, spNameWithoutQualifier), sqlParameters) > 0)
        //            ? true
        //            : false;
        //}

        //private T ExecuteScalar<T>(string spNameWithoutQualifier, T errorValue, params object[] sqlParameters)
        //{
        //    Database database = DatabaseFactory.CreateDatabase(ConnectionString);
        //    object returnValue =
        //        database.ExecuteScalar(String.Concat(ObjectQualifier, spNameWithoutQualifier), sqlParameters);
        //    T identity = default(T);

        //    if (returnValue is T && !returnValue.Equals(errorValue))
        //    {
        //        identity = (T)returnValue;
        //    }
        //    else
        //    {
        //        throw new ConcurrencyException("No records were updated, please reload your data.");
        //    }

        //    return identity;
        //}


        //private DbCommand GetCommandWithParameters(string spNameWithoutQualifier, bool openConnection)
        //{
        //    Database database = DatabaseFactory.CreateDatabase(ConnectionString);
        //    DbCommand command = database.GetStoredProcCommand(String.Concat(ObjectQualifier, spNameWithoutQualifier));
        //    database.DiscoverParameters(command);
        //    if (openConnection)
        //    {
        //        command.Connection = database.CreateConnection();
        //        command.Connection.Open();
        //    }

        //    return command;
        //}

        //private DbCommand GetCommand(string spNameWithoutQualifier, bool openConnection)
        //{
        //    Database database = DatabaseFactory.CreateDatabase(ConnectionString);
        //    DbCommand command = database.GetStoredProcCommand(String.Concat(ObjectQualifier, spNameWithoutQualifier));
        //    // database.DiscoverParameters(command);
        //    if (openConnection)
        //    {
        //        command.Connection = database.CreateConnection();
        //        command.Connection.Open();
        //    }

        //    return command;
        //}

        //private DbCommand GetCommandWithParameters(string spNameWithoutQualifier)
        //{
        //    return GetCommandWithParameters(spNameWithoutQualifier, false);
        //}

        #endregion

        #region RoleMethods

        public override IDataReader GetRolesForUser(string applicationName, string userName)
        {
            IDataReader reader = null;
            reader = ExecuteReader("UsersInRoles_GetRolesForUser", new object[] { applicationName, userName });
            return reader;
        }

        public override void AddUsersToRoles(string applicationName, string roleNames, string userNames)
        {
            Database database = DatabaseFactory.CreateDatabase(ConnectionString);
            SqlCommand sqlCommand = new SqlCommand(String.Concat(ObjectQualifier, "UsersInRoles_AddUsersToRoles"));

            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter1 = new SqlParameter("@ReturnValue", SqlDbType.Int);
            string returnField1 = string.Empty;
            string returnField2 = string.Empty;
            sqlCommand.CommandType = CommandType.StoredProcedure;

            parameter1.Direction = ParameterDirection.ReturnValue;
            sqlCommand.Parameters.Add(parameter1);
            sqlCommand.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, applicationName));
            sqlCommand.Parameters.Add(CreateInputParam("@RoleNames", SqlDbType.NVarChar, roleNames));
            sqlCommand.Parameters.Add(CreateInputParam("@UserNames", SqlDbType.NVarChar, userNames));
            sqlCommand.Parameters.Add(CreateInputParam("@CurrentTimeUtc", SqlDbType.DateTime, DateTime.UtcNow));

            IDataReader reader = null;
            try
            {

                reader = database.ExecuteReader(sqlCommand);
                if (reader.Read())
                {
                    if (reader.FieldCount > 0)
                    {
                        returnField1 = reader.GetString(0);
                    }
                    if (reader.FieldCount > 1)
                    {
                        returnField2 = reader.GetString(1);
                    }
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            switch (GetReturnValue(sqlCommand))
            {
                case 0:
                    return;

                case 1:
                    throw new ProviderException(string.Format("This user '{0}' was not found", returnField1));

                case 2:
                    throw new ProviderException(string.Format("Role '{0}' was not found", returnField1));

                case 3:
                    throw new ProviderException(string.Format("The User '{0}' is already in role '{1}'", returnField1, returnField2));
            }
            throw new ProviderException("Unknown failure");

        }

        public override bool IsUserInRole(string applicationName, string roleName, object username)
        {
            Database database = DatabaseFactory.CreateDatabase(ConnectionString);
            SqlCommand sqlCommand = new SqlCommand(String.Concat(ObjectQualifier, "UsersInRoles_IsUserInRole"));

            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter1 = new SqlParameter("@ReturnValue", SqlDbType.Int);
            parameter1.Direction = ParameterDirection.ReturnValue;
            sqlCommand.Parameters.Add(parameter1);
            sqlCommand.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, applicationName));
            sqlCommand.Parameters.Add(CreateInputParam("@UserName", SqlDbType.NVarChar, username));
            sqlCommand.Parameters.Add(CreateInputParam("@RoleName", SqlDbType.NVarChar, roleName));
            database.ExecuteNonQuery(sqlCommand);
            switch (GetReturnValue(sqlCommand))
            {
                case 0:
                    return false;

                case 1:
                    return true;

                case 2:
                    return false;

                case 3:
                    return false;
            }
            throw new ProviderException("Unknown Failure");
        }

        public override void CreateRole(string applicationName, string roleName)
        {
            Database database = DatabaseFactory.CreateDatabase(ConnectionString);
            SqlCommand sqlCommand = new SqlCommand(String.Concat(ObjectQualifier, "Roles_CreateRole"));
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter parameter1 = new SqlParameter("@ReturnValue", SqlDbType.Int);
            parameter1.Direction = ParameterDirection.ReturnValue;
            sqlCommand.Parameters.Add(parameter1);
            sqlCommand.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, applicationName));
            sqlCommand.Parameters.Add(CreateInputParam("@RoleName", SqlDbType.NVarChar, roleName));

            database.ExecuteNonQuery(sqlCommand);

            switch (GetReturnValue(sqlCommand))
            {
                case 0:
                    return;

                case 1:
                    break;

                default:
                    throw new ProviderException("Unknown Failure");
            }
            throw new ProviderException(string.Format("Role '{0}' already exists", roleName));
        }

        private static SqlParameter CreateInputParam(string paramName, SqlDbType dbType, object objValue)
        {
            SqlParameter parameter1 = new SqlParameter(paramName, dbType);
            if (objValue == null)
            {
                objValue = string.Empty;
            }
            parameter1.Value = objValue;
            return parameter1;
        }


        private static int GetReturnValue(SqlCommand cmd)
        {
            foreach (SqlParameter parameter1 in cmd.Parameters)
            {
                if (((parameter1.Direction == ParameterDirection.ReturnValue) && (parameter1.Value != null)) &&
                    (parameter1.Value is int))
                {
                    return (int)parameter1.Value;
                }
            }
            return -1;
        }


        public override IDataReader GetAllRoles(string applicationName)
        {
            IDataReader reader = null;
            try
            {
                reader = ExecuteReader("Roles_GetAllRoles", new object[] { applicationName });
            }
            catch (Exception ex)
            {
               throw;
            }
            return reader;
        }

        #endregion

        public override IDataReader GetUsersInRole(string applicationName, string roleName)
        {
            IDataReader reader = null;
            reader = ExecuteReader("UsersInRoles_GetUsersInRoles", new object[] { applicationName, roleName });
            return reader;
        }

        public override bool DeleteRole(string applicationName, string roleName, bool throwOnPopulatedRole)
        {
            Database database = DatabaseFactory.CreateDatabase(ConnectionString);
            SqlCommand sqlCommand = new SqlCommand(String.Concat(ObjectQualifier, "Roles_DeleteRole"));
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter parameter1 = new SqlParameter("@ReturnValue", SqlDbType.Int);
            parameter1.Direction = ParameterDirection.ReturnValue;
            sqlCommand.Parameters.Add(parameter1);
            sqlCommand.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, applicationName));
            sqlCommand.Parameters.Add(CreateInputParam("@RoleName", SqlDbType.NVarChar, roleName));
            sqlCommand.Parameters.Add(CreateInputParam("@DeleteOnlyIfRoleIsEmpty", SqlDbType.Bit, throwOnPopulatedRole ? 1 : 0));
            database.ExecuteNonQuery(sqlCommand);
            int num1 = GetReturnValue(sqlCommand);
            if (num1 == 2)
            {
                throw new ProviderException("Role is not empty");
            }
            return num1 == 0;

        }

        public override bool RoleExists(string applicationName, string roleName)
        {
            Database database = DatabaseFactory.CreateDatabase(ConnectionString);
            SqlCommand sqlCommand = new SqlCommand(String.Concat(ObjectQualifier, "Roles_RoleExists"));
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter parameter1 = new SqlParameter("@ReturnValue", SqlDbType.Int);
            parameter1.Direction = ParameterDirection.ReturnValue;
            sqlCommand.Parameters.Add(parameter1);
            sqlCommand.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, applicationName));
            sqlCommand.Parameters.Add(CreateInputParam("@RoleName", SqlDbType.NVarChar, roleName));
            database.ExecuteNonQuery(sqlCommand);
            switch (GetReturnValue(sqlCommand))
            {
                case 0:
                    return false;

                case 1:
                    return true;
            }
            throw new ProviderException("Unknown failure");

        }

        public override void RemoveUsersFromRoles(string applicationName, string roleNames, string userNames)
        {
            Database database = DatabaseFactory.CreateDatabase(ConnectionString);
            SqlCommand sqlCommand = new SqlCommand(String.Concat(ObjectQualifier, "UsersInRoles_RemoveUsersFromRoles"));

            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter1 = new SqlParameter("@ReturnValue", SqlDbType.Int);
            string returnField1 = string.Empty;
            string returnField2 = string.Empty;
            sqlCommand.CommandType = CommandType.StoredProcedure;

            parameter1.Direction = ParameterDirection.ReturnValue;
            sqlCommand.Parameters.Add(parameter1);
            sqlCommand.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, applicationName));
            sqlCommand.Parameters.Add(CreateInputParam("@RoleNames", SqlDbType.NVarChar, roleNames));
            sqlCommand.Parameters.Add(CreateInputParam("@UserNames", SqlDbType.NVarChar, userNames));


            IDataReader reader = null;
            try
            {

                reader = database.ExecuteReader(sqlCommand);
                if (reader.Read())
                {
                    if (reader.FieldCount > 0)
                    {
                        returnField1 = reader.GetString(0);
                    }
                    if (reader.FieldCount > 1)
                    {
                        returnField2 = reader.GetString(1);
                    }
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            switch (GetReturnValue(sqlCommand))
            {
                case 0:
                    return;

                case 1:
                    throw new ProviderException(string.Format("This user '{0}' was not found", returnField1));

                case 2:
                    throw new ProviderException(string.Format("Role '{0}' was not found", returnField1));

                case 3:
                    throw new ProviderException(string.Format("The User '{0}' is already not in role '{1}'", returnField1, returnField2));
            }
            throw new ProviderException("Unknown failure");
        }

        /// <summary>
        /// Checks the security schema version.
        /// </summary>
        /// <returns></returns>
        public override int CheckSchemaVersion(string expectedVersion, string[] features)
        {
            Database database = DatabaseFactory.CreateDatabase(ConnectionString);

            foreach (string feature in features)
            {
                SqlCommand sqlCommand ;
                sqlCommand = new SqlCommand(String.Concat(ObjectQualifier, "CheckSchemaVersion"));
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlParameter;
                sqlParameter = new SqlParameter("@Feature", feature);
                sqlCommand.Parameters.Add(sqlParameter);
                sqlParameter = new SqlParameter("@CompatibleSchemaVersion", expectedVersion);
                sqlCommand.Parameters.Add(sqlParameter);
                sqlParameter = new SqlParameter("@ReturnValue", SqlDbType.Int);
                sqlParameter.Direction = ParameterDirection.ReturnValue;
                sqlCommand.Parameters.Add(sqlParameter);
                database.ExecuteNonQuery(sqlCommand);
                if (((sqlParameter.Value != null) ? ((int)sqlParameter.Value) : -1) != 0)
                {
                    return -1;
                }
            }
            return 1;
        }

        /// <summary>
        /// Deletes the profiles specified.
        /// </summary>
        /// <param name="applicationName">Name of the application.</param>
        /// <param name="profiles">The profiles.</param>
        /// <returns></returns>
        public override int DeleteProfiles(string applicationName, string profiles)
        {
            Database database = DatabaseFactory.CreateDatabase(ConnectionString);
            int returnValue = 0;
            SqlCommand sqlCommand = new SqlCommand(String.Concat(ObjectQualifier, "Profile_DeleteProfiles"));
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, applicationName));
            sqlCommand.Parameters.Add(CreateInputParam("@UserNames", SqlDbType.NVarChar, profiles));
            object obj1 = database.ExecuteScalar(sqlCommand);
            if ((obj1 != null) && (obj1 is int))
            {
                returnValue = (int)obj1;
            }
            return returnValue;
        }
    }
}