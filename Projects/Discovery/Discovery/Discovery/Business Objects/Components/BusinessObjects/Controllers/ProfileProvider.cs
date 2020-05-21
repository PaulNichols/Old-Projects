using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Transactions;
using System.Web;
using System.Web.Hosting;
using System.Web.Profile;
using ASPNET.SQLDataAccessProvider;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Discovery.BusinessObjects.Controllers
{
    public class ProfileProvider : System.Web.Profile.ProfileProvider
    {
        private string applicationName;
        private int schemaVersionCheck;

        internal static string GetDefaultAppName()
        {
            try
            {
                string text1 = HostingEnvironment.ApplicationVirtualPath;
                if (string.IsNullOrEmpty(text1))
                {
                    text1 = Process.GetCurrentProcess().MainModule.ModuleName;
                    int num1 = text1.IndexOf('.');
                    if (num1 != -1)
                    {
                        text1 = text1.Remove(num1);
                    }
                }
                if (string.IsNullOrEmpty(text1))
                {
                    return "/";
                }
                return text1;
            }
            catch
            {
                return "/";
            }
        }


        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }
            if ((name == null) || (name.Length < 1))
            {
                name = "DiscoveryProfileProvider";
            }
            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "DiscoveryProfileProvider");
            }
            base.Initialize(name, config);
            schemaVersionCheck = 0;

            applicationName = config["applicationName"];
            if (string.IsNullOrEmpty(applicationName))
            {
                applicationName = GetDefaultAppName();
            }
            if (applicationName.Length > 0x100)
            {
                throw new ProviderException("Provider application name too long");
            }
          
            config.Remove("applicationName");
            if (config.Count > 0)
            {
                string attribute = config.GetKey(0);
                if (!string.IsNullOrEmpty(attribute))
                {
                    throw new ProviderException(string.Format("Provider unrecognized attribute, '{0}'", attribute));
                }
            }
        }

        private void CheckSchemaVersion()
        {
            string[] features = new string[] { "Profile" };
            string version = "1";

            if (features == null)
            {
                throw new ArgumentNullException("features");
            }
            if (version == null)
            {
                throw new ArgumentNullException("version");
            }
            if (schemaVersionCheck == -1)
            {
                throw new ProviderException(string.Format("The Provider schema version for '{0}' did not match. '{1}' was expected.", this.ToString(), version));
            }
            if (schemaVersionCheck == 0)
            {
                lock (this)
                {
                    if (schemaVersionCheck == -1)
                    {
                        throw new ProviderException(string.Format("The Provider schema version for '{0}' did not match. '{1}' was expected.", this.ToString(), version));
                    }
                    if (schemaVersionCheck == 0)
                    {

                        try
                        {
                            schemaVersionCheck = DataAccessProvider.Instance().CheckSchemaVersion(version, features);
                        }
                        catch (Exception ex)
                        {
                            if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
                        }

                    }
                    if (schemaVersionCheck == -1)
                    {
                        throw new ProviderException(string.Format("The Provider schema version for '{0}' did not match. '{1}' was expected.", this.ToString(), version));
                    }
                }
            }
        }

        ///<summary>
        ///When overridden in a derived class, deletes profile properties and information for profiles that match the supplied list of user names.
        ///</summary>
        ///
        ///<returns>
        ///The number of profiles deleted from the data source.
        ///</returns>
        ///
        ///<param name="users">A string array of user names for profiles to be deleted.</param>
        public override int DeleteProfiles(string[] users)
        {
            int profilesDeleted = 0;
            try
            {
                CheckSchemaVersion();
                string userNames = "";
                foreach (string user in users)
                {
                    userNames = string.Concat(userNames, ",", user);
                }

                using (TransactionScope scope = new TransactionScope())
                {
                    profilesDeleted = DataAccessProvider.Instance().DeleteProfiles(ApplicationName, userNames);
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
      
            return profilesDeleted;
        }

        ///<summary>
        ///When overridden in a derived class, deletes profile properties and information for the supplied list of profiles.
        ///</summary>
        ///
        ///<returns>
        ///The number of profiles deleted from the data source.
        ///</returns>
        ///
        ///<param name="profiles">A <see cref="T:System.Web.Profile.ProfileInfoCollection"></see>  of information about profiles that are to be deleted.</param>
        public override int DeleteProfiles(ProfileInfoCollection profiles)
        {
            if (profiles == null)
            {
                throw new ArgumentNullException("profiles");
            }
            if (profiles.Count < 1)
            {
                throw new ArgumentException("Delete Profiles parameter collection was empty");
            }
            string[] textArray1 = new string[profiles.Count];
            int num1 = 0;
            foreach (ProfileInfo info1 in profiles)
            {
                textArray1[num1++] = info1.UserName;
            }
            return DeleteProfiles(textArray1);
        }


        ///<summary>
        ///When overridden in a derived class, deletes all user-profile data for profiles in which the last activity date occurred before the specified date.
        ///</summary>
        ///
        ///<returns>
        ///The number of profiles deleted from the data source.
        ///</returns>
        ///
        ///<param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption"></see> values, specifying whether anonymous, authenticated, or both types of profiles are deleted.</param>
        ///<param name="userInactiveSinceDate">A <see cref="T:System.DateTime"></see> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate"></see>  value of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
        public override int DeleteInactiveProfiles(ProfileAuthenticationOption authenticationOption,
                                                   DateTime userInactiveSinceDate)
        {
            //int num1;
            //try
            //{
            //    SqlConnectionHolder holder1 = null;
            //    try
            //    {
            //        holder1 = SqlConnectionHelper.GetConnection(this._sqlConnectionString, true);
            //        this.CheckSchemaVersion(holder1.Connection);
            //        SqlCommand command1 = new SqlCommand("dbo.aspnet_Profile_DeleteInactiveProfiles", holder1.Connection);
            //        command1.CommandTimeout = this.CommandTimeout;
            //        command1.CommandType = CommandType.StoredProcedure;
            //        command1.Parameters.Add(this.CreateInputParam("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName));
            //        command1.Parameters.Add(this.CreateInputParam("@ProfileAuthOptions", SqlDbType.Int, (int)authenticationOption));
            //        command1.Parameters.Add(this.CreateInputParam("@InactiveSinceDate", SqlDbType.DateTime, userInactiveSinceDate.ToUniversalTime()));
            //        object obj1 = command1.ExecuteScalar();
            //        if ((obj1 == null) || !(obj1 is int))
            //        {
            //            return 0;
            //        }
            //        num1 = (int)obj1;
            //    }
            //    finally
            //    {
            //        if (holder1 != null)
            //        {
            //            holder1.Close();
            //            holder1 = null;
            //        }
            //    }
            //}
            //catch
            //{
            //    throw;
            //}
            //return num1;

            return 0;
        }

        ///<summary>
        ///When overridden in a derived class, returns the number of profiles in which the last activity date occurred on or before the specified date.
        ///</summary>
        ///
        ///<returns>
        ///The number of profiles in which the last activity date occurred on or before the specified date.
        ///</returns>
        ///
        ///<param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption"></see> values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
        ///<param name="userInactiveSinceDate">A <see cref="T:System.DateTime"></see> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate"></see>  of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
        public override int GetNumberOfInactiveProfiles(ProfileAuthenticationOption authenticationOption,
                                                        DateTime userInactiveSinceDate)
        {
            throw new NotImplementedException();
        }

        ///<summary>
        ///When overridden in a derived class, retrieves user profile data for all profiles in the data source.
        ///</summary>
        ///
        ///<returns>
        ///A <see cref="T:System.Web.Profile.ProfileInfoCollection"></see> containing user-profile information for all profiles in the data source.
        ///</returns>
        ///
        ///<param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption"></see> values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
        ///<param name="totalRecords">When this method returns, contains the total number of profiles.</param>
        ///<param name="pageIndex">The index of the page of results to return.</param>
        ///<param name="pageSize">The size of the page of results to return.</param>
        public override ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption,
                                                             int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        ///<summary>
        ///When overridden in a derived class, retrieves user-profile data from the data source for profiles in which the last activity date occurred on or before the specified date.
        ///</summary>
        ///
        ///<returns>
        ///A <see cref="T:System.Web.Profile.ProfileInfoCollection"></see> containing user-profile information about the inactive profiles.
        ///</returns>
        ///
        ///<param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption"></see> values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
        ///<param name="userInactiveSinceDate">A <see cref="T:System.DateTime"></see> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate"></see>  of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
        ///<param name="totalRecords">When this method returns, contains the total number of profiles.</param>
        ///<param name="pageIndex">The index of the page of results to return.</param>
        ///<param name="pageSize">The size of the page of results to return.</param>
        public override ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption,
                                                                     DateTime userInactiveSinceDate, int pageIndex,
                                                                     int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        ///<summary>
        ///When overridden in a derived class, retrieves profile information for profiles in which the user name matches the specified user names.
        ///</summary>
        ///
        ///<returns>
        ///A <see cref="T:System.Web.Profile.ProfileInfoCollection"></see> containing user-profile information for profiles where the user name matches the supplied usernameToMatch parameter.
        ///</returns>
        ///
        ///<param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption"></see> values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
        ///<param name="totalRecords">When this method returns, contains the total number of profiles.</param>
        ///<param name="pageIndex">The index of the page of results to return.</param>
        ///<param name="usernameToMatch">The user name to search for.</param>
        ///<param name="pageSize">The size of the page of results to return.</param>
        public override ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption,
                                                                     string usernameToMatch, int pageIndex, int pageSize,
                                                                     out int totalRecords)
        {
            ProfileInfoCollection collection2;
            if (pageIndex < 0)
            {
                throw new ArgumentException("Invalid Page index", "pageIndex");
            }
            if (pageSize < 1)
            {
                throw new ArgumentException("Invalid Page Size", "pageSize");
            }
            long num1 = ((pageIndex * pageSize) + pageSize) - 1;
            if (num1 > 0x7fffffff)
            {
                throw new ArgumentException("Invalid Page Size and Page Index", "pageIndex and pageSize");
            }
            //try
            //{
            //    SqlConnectionHolder holder1 = null;
            //    SqlDataReader reader1 = null;
            //    try
            //    {
            //        holder1 = SqlConnectionHelper.GetConnection(this._sqlConnectionString, true);
            //        this.CheckSchemaVersion(holder1.Connection);
            //        SqlCommand command1 = new SqlCommand("dbo.aspnet_Profile_GetProfiles", holder1.Connection);
            //        command1.CommandTimeout = this.CommandTimeout;
            //        command1.CommandType = CommandType.StoredProcedure;
            //        command1.Parameters.Add(this.CreateInputParam("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName));
            //        command1.Parameters.Add(this.CreateInputParam("@ProfileAuthOptions", SqlDbType.Int, (int)authenticationOption));
            //        command1.Parameters.Add(this.CreateInputParam("@PageIndex", SqlDbType.Int, pageIndex));
            //        command1.Parameters.Add(this.CreateInputParam("@PageSize", SqlDbType.Int, pageSize));
            //        foreach (SqlParameter parameter1 in args)
            //        {
            //            command1.Parameters.Add(parameter1);
            //        }
            //        reader1 = command1.ExecuteReader(CommandBehavior.SequentialAccess);
            //        ProfileInfoCollection collection1 = new ProfileInfoCollection();
            //        while (reader1.Read())
            //        {
            //            string text1 = reader1.GetString(0);
            //            bool flag1 = reader1.GetBoolean(1);
            //            DateTime time1 = DateTime.SpecifyKind(reader1.GetDateTime(2), DateTimeKind.Utc);
            //            DateTime time2 = DateTime.SpecifyKind(reader1.GetDateTime(3), DateTimeKind.Utc);
            //            int num2 = reader1.GetInt32(4);
            //            collection1.Add(new ProfileInfo(text1, flag1, time1, time2, num2));
            //        }
            //        totalRecords = collection1.Count;
            //        if (reader1.NextResult() && reader1.Read())
            //        {
            //            totalRecords = reader1.GetInt32(0);
            //        }
            //        collection2 = collection1;
            //    }
            //    finally
            //    {
            //        if (reader1 != null)
            //        {
            //            reader1.Close();
            //        }
            //        if (holder1 != null)
            //        {
            //            holder1.Close();
            //            holder1 = null;
            //        }
            //    }
            //}
            //catch
            //{
            //    throw;
            //}
            //return collection2;

            totalRecords = 0;
            return null;
        }

        ///<summary>
        ///When overridden in a derived class, retrieves profile information for profiles in which the last activity date occurred on or before the specified date and the user name matches the specified user name.
        ///</summary>
        ///
        ///<returns>
        ///A <see cref="T:System.Web.Profile.ProfileInfoCollection"></see> containing user profile information for inactive profiles where the user name matches the supplied usernameToMatch parameter.
        ///</returns>
        ///
        ///<param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption"></see> values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
        ///<param name="userInactiveSinceDate">A <see cref="T:System.DateTime"></see> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate"></see> value of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
        ///<param name="totalRecords">When this method returns, contains the total number of profiles.</param>
        ///<param name="pageIndex">The index of the page of results to return.</param>
        ///<param name="usernameToMatch">The user name to search for.</param>
        ///<param name="pageSize">The size of the page of results to return.</param>
        public override ProfileInfoCollection FindInactiveProfilesByUserName(
            ProfileAuthenticationOption authenticationOption, string usernameToMatch, DateTime userInactiveSinceDate,
            int pageIndex, int pageSize, out int totalRecords)
        {
            //ProfileInfoCollection collection2;
            //if (pageIndex < 0)
            //{
            //    throw new ArgumentException(SR.GetString("PageIndex_bad"), "pageIndex");
            //}
            //if (pageSize < 1)
            //{
            //    throw new ArgumentException(SR.GetString("PageSize_bad"), "pageSize");
            //}
            //long num1 = ((pageIndex * pageSize) + pageSize) - 1;
            //if (num1 > 0x7fffffff)
            //{
            //    throw new ArgumentException(SR.GetString("PageIndex_PageSize_bad"), "pageIndex and pageSize");
            //}
            //try
            //{
            //    SqlConnectionHolder holder1 = null;
            //    SqlDataReader reader1 = null;
            //    try
            //    {
            //        holder1 = SqlConnectionHelper.GetConnection(this._sqlConnectionString, true);
            //        this.CheckSchemaVersion(holder1.Connection);
            //        SqlCommand command1 = new SqlCommand("dbo.aspnet_Profile_GetProfiles", holder1.Connection);
            //        command1.CommandTimeout = this.CommandTimeout;
            //        command1.CommandType = CommandType.StoredProcedure;
            //        command1.Parameters.Add(this.CreateInputParam("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName));
            //        command1.Parameters.Add(this.CreateInputParam("@ProfileAuthOptions", SqlDbType.Int, (int)authenticationOption));
            //        command1.Parameters.Add(this.CreateInputParam("@PageIndex", SqlDbType.Int, pageIndex));
            //        command1.Parameters.Add(this.CreateInputParam("@PageSize", SqlDbType.Int, pageSize));
            //        foreach (SqlParameter parameter1 in args)
            //        {
            //            command1.Parameters.Add(parameter1);
            //        }
            //        reader1 = command1.ExecuteReader(CommandBehavior.SequentialAccess);
            //        ProfileInfoCollection collection1 = new ProfileInfoCollection();
            //        while (reader1.Read())
            //        {
            //            string text1 = reader1.GetString(0);
            //            bool flag1 = reader1.GetBoolean(1);
            //            DateTime time1 = DateTime.SpecifyKind(reader1.GetDateTime(2), DateTimeKind.Utc);
            //            DateTime time2 = DateTime.SpecifyKind(reader1.GetDateTime(3), DateTimeKind.Utc);
            //            int num2 = reader1.GetInt32(4);
            //            collection1.Add(new ProfileInfo(text1, flag1, time1, time2, num2));
            //        }
            //        totalRecords = collection1.Count;
            //        if (reader1.NextResult() && reader1.Read())
            //        {
            //            totalRecords = reader1.GetInt32(0);
            //        }
            //        collection2 = collection1;
            //    }
            //    finally
            //    {
            //        if (reader1 != null)
            //        {
            //            reader1.Close();
            //        }
            //        if (holder1 != null)
            //        {
            //            holder1.Close();
            //            holder1 = null;
            //        }
            //    }
            //}
            //catch
            //{
            //    throw;
            //}
            //return collection2;

            totalRecords = 0;
            return null;
        }

        ///<summary>
        ///Returns the collection of settings property values for the specified application instance and settings property group.
        ///</summary>
        ///
        ///<returns>
        ///A <see cref="T:System.Configuration.SettingsPropertyValueCollection"></see> containing the values for the specified settings property group.
        ///</returns>
        ///
        ///<param name="sc">A <see cref="T:System.Configuration.SettingsContext"></see> describing the current application use.</param>
        ///<param name="properties">A <see cref="T:System.Configuration.SettingsPropertyCollection"></see> containing the settings property group whose values are to be retrieved.</param><filterpriority>2</filterpriority>
        public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext sc,
                                                                          SettingsPropertyCollection properties)
        {
            SettingsPropertyValueCollection propertyValues = new SettingsPropertyValueCollection();
            if (properties.Count >= 1)
            {
                string text1 = (string)sc["UserName"];
                foreach (SettingsProperty property in properties)
                {
                    if (property.SerializeAs == SettingsSerializeAs.ProviderSpecific)
                    {
                        if (property.PropertyType.IsPrimitive || (property.PropertyType == typeof(string)))
                        {
                            property.SerializeAs = SettingsSerializeAs.String;
                        }
                        else
                        {
                            property.SerializeAs = SettingsSerializeAs.Xml;
                        }
                    }
                    propertyValues.Add(new SettingsPropertyValue(property));
                }
                if (!string.IsNullOrEmpty(text1))
                {
                    GetPropertyValuesFromDatabase(text1, propertyValues);
                }
            }
            return propertyValues;

        }

        private void GetPropertyValuesFromDatabase(string userName, SettingsPropertyValueCollection svc)
        {
            //  HttpContext context1 = HttpContext.Current;

            //if (context1 != null)
            //{
            //    //if (!context1.Request.IsAuthenticated)
            //    //{
            //    //    string anonymousID = context1.Request.AnonymousID;
            //    //}
            //    //else
            //    //{
            //    //    string currentUserName = context1.User.Identity.Name;
            //    //}
            //}
            try
            {
                //string[] textArray1 = null;
                //string text1 = null;
                //byte[] buffer1 = null;
                //SqlConnectionHolder holder1 = null;
                //SqlDataReader reader1 = null;
                //try
                //{
                //    holder1 = SqlConnectionHelper.GetConnection(this._sqlConnectionString, true);
                //    this.CheckSchemaVersion(holder1.Connection);
                //    SqlCommand command1 = new SqlCommand("dbo.aspnet_Profile_GetProperties", holder1.Connection);
                //    command1.CommandTimeout = this.CommandTimeout;
                //    command1.CommandType = CommandType.StoredProcedure;
                //    command1.Parameters.Add(this.CreateInputParam("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName));
                //    command1.Parameters.Add(this.CreateInputParam("@UserName", SqlDbType.NVarChar, userName));
                //    command1.Parameters.Add(this.CreateInputParam("@CurrentTimeUtc", SqlDbType.DateTime, DateTime.UtcNow));
                //    reader1 = command1.ExecuteReader(CommandBehavior.SingleRow);
                //    if (reader1.Read())
                //    {
                //        textArray1 = reader1.GetString(0).Split(new char[] { ':' });
                //        text1 = reader1.GetString(1);
                //        int num1 = (int)reader1.GetBytes(2, (long)0, null, 0, 0);
                //        buffer1 = new byte[num1];
                //        reader1.GetBytes(2, (long)0, buffer1, 0, num1);
                //    }
                //}
                //finally
                //{
                //    if (holder1 != null)
                //    {
                //        holder1.Close();
                //        holder1 = null;
                //    }
                //    if (reader1 != null)
                //    {
                //        reader1.Close();
                //    }
                // }
                //ParseDataFromDB(textArray1, text1, buffer1, svc);

            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Prepares the data for saving.
        /// </summary>
        /// <param name="allNames">All names.</param>
        /// <param name="allValues">All values.</param>
        /// <param name="buf">The buf.</param>
        /// <param name="binarySupported">if set to <c>true</c> [binary supported].</param>
        /// <param name="properties">The properties.</param>
        /// <param name="userIsAuthenticated">if set to <c>true</c> [user is authenticated].</param>
        [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal), SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.SerializationFormatter)]
        internal static void PrepareDataForSaving(ref string allNames, ref string allValues, ref byte[] buf, bool binarySupported, SettingsPropertyValueCollection properties, bool userIsAuthenticated)
        {
            StringBuilder builder1 = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            MemoryStream stream1 = binarySupported ? new MemoryStream() : null;

            try
            {
                bool flag1 = false;
                foreach (SettingsPropertyValue value1 in properties)
                {
                    if (value1.IsDirty && (userIsAuthenticated || ((bool)value1.Property.Attributes["AllowAnonymous"])))
                    {
                        flag1 = true;
                        break;
                    }
                }
                if (!flag1)
                {
                    return;
                }
                foreach (SettingsPropertyValue value2 in properties)
                {
                    if ((userIsAuthenticated || ((bool)value2.Property.Attributes["AllowAnonymous"])) && (value2.IsDirty || !value2.UsingDefaultValue))
                    {
                        int num1;
                        int num2 = 0;
                        string text1 = null;
                        if (value2.Deserialized && (value2.PropertyValue == null))
                        {
                            num1 = -1;
                        }
                        else
                        {
                            object obj1 = value2.SerializedValue;
                            if (obj1 == null)
                            {
                                num1 = -1;
                            }
                            else
                            {
                                if (!(obj1 is string) && !binarySupported)
                                {
                                    obj1 = Convert.ToBase64String((byte[])obj1);
                                }
                                if (obj1 is string)
                                {
                                    text1 = (string)obj1;
                                    num1 = text1.Length;
                                    num2 = builder2.Length;
                                }
                                else
                                {
                                    byte[] buffer1 = (byte[])obj1;
                                    num2 = (int)stream1.Position;
                                    stream1.Write(buffer1, 0, buffer1.Length);
                                    stream1.Position = num2 + buffer1.Length;
                                    num1 = buffer1.Length;
                                }
                            }
                        }
                        builder1.Append(value2.Name + ":" + ((text1 != null) ? "S" : "B") + ":" + num2.ToString(CultureInfo.InvariantCulture) + ":" + num1.ToString(CultureInfo.InvariantCulture) + ":");
                        if (text1 != null)
                        {
                            builder2.Append(text1);
                        }
                    }
                }
                if (binarySupported)
                {
                    buf = stream1.ToArray();
                }
            }
            finally
            {
                if (stream1 != null)
                {
                    stream1.Close();
                }
            }


            allNames = builder1.ToString();
            allValues = builder2.ToString();
        }


        ///<summary>
        ///Sets the values of the specified group of property settings.
        ///</summary>
        ///
        ///<param name="context">A <see cref="T:System.Configuration.SettingsContext"></see> describing the current application usage.</param>
        ///<param name="collection">A <see cref="T:System.Configuration.SettingsPropertyValueCollection"></see> representing the group of property settings to set.</param><filterpriority>2</filterpriority>
        public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection)
        {
            throw new NotImplementedException();
        }

        ///<summary>
        ///Gets or sets the name of the currently running application.
        ///</summary>
        ///
        ///<returns>
        ///A <see cref="T:System.String"></see> that contains the application's shortened name, which does not contain a full path or extension, for example, SimpleAppSettings.
        ///</returns>
        ///<filterpriority>2</filterpriority>
        public override string ApplicationName
        {
            get { return applicationName; }
            set
            {
                if (value.Length > 0x100)
                {
                    //throw new ProviderException(SR.GetString("Provider_application_name_too_long"));
                }
                this.applicationName = value;

            }
        }
    }
}