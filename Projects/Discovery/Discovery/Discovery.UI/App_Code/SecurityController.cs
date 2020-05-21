using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Transactions;
using System.Web.Profile;
using System.Web.Security;
using Discovery.BusinessObjects;
using Discovery.BusinessObjects.Controllers;
using Discovery.Utility;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Security;
using Microsoft.Practices.EnterpriseLibrary.Security.Configuration;
using Microsoft.Practices.EnterpriseLibrary.SqlConfigurationSource;
using ValidationFramework;

/// <summary>
/// Summary description for User
/// </summary>
namespace Discovery.ComponentServices.Security
{
    /*************************************************************************************************
    ** CLASS:	UserDetails
    **
    ** OVERVIEW:
    ** This class is used within the Security Controller to represent a User and their associated profile 
    ** details
    **
    ** MODIFICATION HISTORY:
    **
    ** Date:		Version:    Who:	Change:
    ** 19/7/06		1.0			PJN		Initial Version
    ************************************************************************************************/

    public class UserDetails : ValidatableBase
    {
        #region Private Fields

        private string userName;
        private string newPassword;
        private string email;
        private string oldPassword;
        private string lastActivityDate;
        private int warehouseId;
        private int salesLocationId;
        private string opCoCode;
        private int opCoId;
        private string regionCode;
        private int regionId;
        private OptrakRegion region;
        private Warehouse warehouse;
        private OpCo opCo;
        private SalesLocation salesLocation;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the warehouse id.
        /// </summary>
        /// <value>The warehouse id.</value>
        [RangeValidator(-1, 2147483647, "Please Select a Sales Warehouse", "*")]
        public int WarehouseId
        {
            get { return warehouseId; }
            set { warehouseId = value; }
        }

        [RangeValidator(-1, 2147483647, "Please Select a Sales Opco", "*")]
        public int OpCoId
        {
            get { return opCoId; }
            set { opCoId = value; }
        }

        /// <summary>
        /// Gets or sets the newPassword.
        /// </summary>
        /// <value>The newPassword.</value>
        public string NewPassword
        {
            get { return newPassword; }
            set { newPassword = value; }
        }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }



        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [LengthValidator(256, "The maximum length of a Email is 256 characters.", "*")]
        [RegexValidator(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", "Email is Invalid", "*")]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string OldPassword
        {
            get { return oldPassword; }
            set { oldPassword = value; }
        }

        public string LastActivityDate
        {
            get { return lastActivityDate; }
            set { lastActivityDate = value; }
        }

        [RangeValidator(-1, 2147483647, "Please Select a Sales Location", "*")]
        public int SalesLocationId
        {
            get { return salesLocationId; }
            set { salesLocationId = value; }
        }

        public string OpCoCode
        {
            get { return opCoCode; }
            set { opCoCode = value; }
        }

        public string RegionCode
        {
            get { return regionCode; }
            set { regionCode = value; }
        }

        [RangeValidator(-1,2147483647,"Please Select a Sales Region", "*")]
        public int RegionId
        {
            get { return regionId; }
            set { regionId = value; }
        }

        public OptrakRegion Region
        {
            get { return region; }
            set { region = value; }
        }

        public Warehouse Warehouse
        {
            get { return warehouse; }
            set { warehouse = value; }
        }

        public OpCo OpCo
        {
            get { return opCo; }
            set { opCo = value; }
        }

        public SalesLocation SalesLocation
        {
            get { return salesLocation; }
            set { salesLocation = value; }
        }

        #endregion

        #region Public Method(s)

        #endregion

        #region Protected Method(s)

        #endregion

        #region Private Method(s)

        #endregion

        #region Constructor(s)

        public UserDetails()
            : base()
        {
            //
            // TODO: Add constructor logic here
            //
            // System.Web.Profile.ProfileManager
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:UserDetails"/> class.
        /// </summary>
        /// <param name="membershipUser">The membership user.</param>
        /// <param name="getProfile">if set to <c>true</c> [get profile].</param>
        public UserDetails(MembershipUser membershipUser, bool getProfile)
        {
            if (membershipUser != null)
            {
                UserName = membershipUser.UserName;
                Email = membershipUser.Email;
                LastActivityDate = membershipUser.LastActivityDate.ToShortDateString();

                if (getProfile)
                {
                    ProfileCommon p = new ProfileCommon();
                    ProfileCommon profile = p.GetProfile(membershipUser.UserName);


                    if (profile != null)
                    {
                        OpCoId = profile.OpCoId;
                        OpCoCode = profile.OpCoCode;
                        RegionId = profile.RegionId;
                        RegionCode = profile.RegionCode;
                        WarehouseId = profile.WarehouseId;
                        SalesLocationId = profile.SalesLocationId;
                        SalesLocation = SalesLocationController.GetLocation(salesLocationId, false);
                        OpCo = OpcoController.GetOpCo(OpCoId, false);
                        Warehouse = WarehouseController.GetWarehouse(WarehouseId);
                        Region = OptrakRegionController.GetRegion(RegionId);
                    }
                }
            }
        }

        #endregion
    }

    public static class SecurityController
    {
        /// <summary>
        /// Gets all users, sorted.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        public static List<UserDetails> GetAllUsers(string filter, string sortExpression, bool getProfile)
        {
            List<UserDetails> userDetails = GetAllUsers(filter, getProfile);

            if (string.IsNullOrEmpty(sortExpression))
            {
                sortExpression = "UserName";
            }
            userDetails.Sort(new UniversalComparer<UserDetails>(sortExpression));
            return userDetails;
        }

        /// <summary>
        /// Gets all users, sorted.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        public static List<UserDetails> GetAllUsers(string filter, string sortExpression)
        {
            return GetAllUsers(filter, sortExpression, false);
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>
        public static List<UserDetails> GetAllUsers(string filter, bool getProfile)
        {
            List<UserDetails> users = new List<UserDetails>();

            foreach (MembershipUser membershipUser in Membership.GetAllUsers())
            {
                if (filter == null || membershipUser.UserName.ToLower().StartsWith(filter.ToLower()))
                {
                    UserDetails user = new UserDetails(membershipUser, getProfile);
                    users.Add(user);
                }
            }

            users.Sort(new UniversalComparer<UserDetails>("UserName"));
            return users;
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>
        public static List<UserDetails> GetAllUsers(string filter)
        {
            return GetAllUsers(filter, false);
        }

        /// <summary>
        /// Gets the roles for user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public static List<Role> GetRolesForUser(string userName)
        {
            try
            {
                List<Role> roles = new List<Role>();
                foreach (string role in Roles.GetRolesForUser(userName))
                {
                    roles.Add(new Role(role));
                }
                roles.Sort(new UniversalComparer<Role>("Name"));
                return roles;
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the role.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns></returns>
        public static Role GetRole(string roleName)
        {
            Role role = null;
            try
            {
                if (Roles.RoleExists(roleName))
                    role = new Role(roleName);
            }
            catch (ArgumentNullException)
            {
                return null;
            }
            return role;
        }

        /// <summary>
        /// Gets the users in a role.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static List<UserDetails> GetUsersInRole(string roleName, string filter)
        {
            try
            {
                List<UserDetails> users = new List<UserDetails>();
                foreach (string user in Roles.GetUsersInRole(roleName))
                {
                    if (filter == null || user.ToLower().StartsWith(filter.ToLower()))
                    {
                        UserDetails userDetail = new UserDetails();
                        userDetail.UserName = user;
                        users.Add(userDetail);
                    }
                }
                users.Sort(new UniversalComparer<UserDetails>("UserName"));
                return users;
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }


        /// <summary>
        /// Gets the users not in role.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns></returns>
        public static List<UserDetails> GetUsersNotInRole(string roleName)
        {
            List<UserDetails> usersNotInRole = new List<UserDetails>();
            try
            {
                MembershipUserCollection users = Membership.GetAllUsers();
                foreach (MembershipUser user in users)
                {
                    if (!Roles.IsUserInRole(user.UserName, roleName))
                    {
                        UserDetails userDetail = new UserDetails(user, false);
                        usersNotInRole.Add(userDetail);
                    }
                }
            }
            catch (ArgumentNullException)
            {
                return null;
            }
            usersNotInRole.Sort(new UniversalComparer<UserDetails>("UserName"));
            return usersNotInRole;
        }

        /// <summary>
        /// Tests a rule.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="roles">The roles.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public static bool TestRule(string expression, string[] roles, string userName)
        {
            bool result;
            IIdentity identity = new GenericIdentity(userName);
            IPrincipal principal = new GenericPrincipal(identity, roles);

            Parser parser = new Parser();
            BooleanExpression booleanExpression = parser.Parse(expression);
            if (booleanExpression.Evaluate(principal))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }



        /// <summary>
        /// Deletes a rule.
        /// </summary>
        /// <param name="rule">The rule.</param>
        public static void DeleteRule(Rule rule)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                ConfigurationSourceSection section = GetConfigurationSourceSection();
                //get selected source
                SqlConfigurationSource selectedSource = GetSelectedConfigurationSource(section);
                //get authorization Provider
                SecurityConfigurationView securityConfigurationView = new SecurityConfigurationView(selectedSource);
                SecuritySettings securitySettings = securityConfigurationView.GetSecuritySettings();
                AuthorizationRuleProviderData authorizationProviderData = securitySettings.AuthorizationProviders.Get(
                                                                              securityConfigurationView.GetDefaultAuthorizationProviderName()) as AuthorizationRuleProviderData;
                //add the new rule to the collection
                authorizationProviderData.Rules.Remove(rule.Name);

                SqlConfigurationSourceElement element = GetElement(section.SelectedSource, section.Sources);
                selectedSource.Save(element.ConnectionString, element.SetStoredProcedure, "securityConfiguration", securitySettings);
                scope.Complete();

            }
        }

        /// <summary>
        /// Updates a rule.
        /// </summary>
        /// <param name="rule">The rule.</param>
        public static void UpdateRule(Rule rule)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                ConfigurationSourceSection section = GetConfigurationSourceSection();
                //get selected source
                SqlConfigurationSource selectedSource = GetSelectedConfigurationSource(section);
                //get authorization Provider
                SecurityConfigurationView securityConfigurationView = new SecurityConfigurationView(selectedSource);
                SecuritySettings securitySettings = securityConfigurationView.GetSecuritySettings();
                AuthorizationRuleProviderData authorizationProviderData = securitySettings.AuthorizationProviders.Get(
                                                                              securityConfigurationView.GetDefaultAuthorizationProviderName()) as AuthorizationRuleProviderData;
                //add the new rule to the collection
                authorizationProviderData.Rules.Get(rule.Name).Expression = rule.Expression;

                SqlConfigurationSourceElement element = GetElement(section.SelectedSource, section.Sources);
                selectedSource.Save(element.ConnectionString, element.SetStoredProcedure, "securityConfiguration", securitySettings);
                scope.Complete();

            }

        }

        /// <summary>
        /// Gets the rule.
        /// </summary>
        /// <param name="ruleName">Name of the rule.</param>
        /// <returns></returns>
        public static Rule GetRule(string ruleName)
        {
            ConfigurationSourceSection section = GetConfigurationSourceSection();
            return GetRule(ruleName, section);
        }

        private static Rule GetRule(string ruleName, ConfigurationSourceSection section)
        {
            SqlConfigurationSource source = GetSelectedConfigurationSource(section);
            //get selected source
            SqlConfigurationSource selectedSource = GetSelectedConfigurationSource(section);

            SecurityConfigurationView securityConfigurationView = new SecurityConfigurationView(selectedSource);
            AuthorizationRuleProviderData authorizationProviderData = GetAuthorizationProviderData(securityConfigurationView, source, section);

            AuthorizationRuleData authorizationRuleData = authorizationProviderData.Rules.Get(ruleName);
            return new Rule(authorizationRuleData.Name, authorizationRuleData.Expression);
        }

        /// <summary>
        /// Gets all roles.
        /// </summary>
        /// <returns></returns>
        public static List<Rule> GetAllRules(string sortExpression)
        {
            List<Rule> rules = GetAllRules();

            if (string.IsNullOrEmpty(sortExpression))
            {
                sortExpression = "Name";
            }
            rules.Sort(new UniversalComparer<Rule>(sortExpression));
            return rules;
        }

        /// <summary>
        /// Gets all rules.
        /// </summary>
        /// <returns></returns>
        public static List<Rule> GetAllRules()
        {
            ConfigurationSourceSection section = GetConfigurationSourceSection();
            SqlConfigurationSource source = GetSelectedConfigurationSource(section);
            //get selected source
            SqlConfigurationSource selectedSource = GetSelectedConfigurationSource(section);
            SecurityConfigurationView securityConfigurationView = new SecurityConfigurationView(selectedSource);
            AuthorizationRuleProviderData authorizationProviderData = GetAuthorizationProviderData(securityConfigurationView, source, section);

            List<Rule> rules = new List<Rule>();
            //loop through all the rules
            authorizationProviderData.Rules.ForEach(delegate(AuthorizationRuleData currentRule)
                                                        {
                                                            rules.Add(new Rule(currentRule.Name, currentRule.Expression));
                                                        });

            return rules;
        }

        private static AuthorizationRuleProviderData GetAuthorizationProviderData(SecurityConfigurationView securityConfigurationView, SqlConfigurationSource configurationSource, ConfigurationSourceSection section)
        {
            //find default authorisation provider - the rules provider
            return securityConfigurationView.GetSecuritySettings().AuthorizationProviders.Get(
                       securityConfigurationView.GetDefaultAuthorizationProviderName()) as AuthorizationRuleProviderData;
        }

        private static SqlConfigurationSource GetSelectedConfigurationSource(ConfigurationSourceSection section)
        {
            //find the selected configuration source ie system (app.config) or sql************
            string selectedSource = section.SelectedSource;
            NameTypeConfigurationElementCollection<ConfigurationSourceElement> sources = section.Sources;

            SqlConfigurationSourceElement element = GetElement(selectedSource, sources);
            return  new SqlConfigurationSource(element.ConnectionString, element.GetStoredProcedure,
                                                                     element.SetStoredProcedure,
                                                                     element.RefreshStoredProcedure, element.RemoveStoredProcedure);

            //*************************************************************
        }

        private static SqlConfigurationSourceElement GetElement(string selectedSource, NameTypeConfigurationElementCollection<ConfigurationSourceElement> sources)
        {
            return sources.Get(selectedSource) as SqlConfigurationSourceElement;
        }

        private static ConfigurationSourceSection GetConfigurationSourceSection()
        {
            //find the configuration source section ie system (app.config) or sql************
            return ConfigurationSourceSection.GetConfigurationSourceSection();
        }

        /// <summary>
        /// Creates the a new rule.
        /// </summary>
        /// <param name="rule">The rule.</param>
        public static void CreateRule(Rule rule)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                ConfigurationSourceSection section = GetConfigurationSourceSection();
                //get selected source
                SqlConfigurationSource selectedSource = GetSelectedConfigurationSource(section);
                //get authorization Provider
                SecurityConfigurationView securityConfigurationView = new SecurityConfigurationView(selectedSource);
                SecuritySettings securitySettings = securityConfigurationView.GetSecuritySettings();
                AuthorizationRuleProviderData authorizationProviderData = securitySettings.AuthorizationProviders.Get(
                                                                              securityConfigurationView.GetDefaultAuthorizationProviderName()) as AuthorizationRuleProviderData;
                //add the new rule to the collection
                //create a new rule
                AuthorizationRuleData newRule = new AuthorizationRuleData(rule.Name, rule.Expression);
                authorizationProviderData.Rules.Add(newRule);

                SqlConfigurationSourceElement element = GetElement(section.SelectedSource, section.Sources);
                selectedSource.Save(element.ConnectionString, element.SetStoredProcedure, "securityConfiguration", securitySettings);
                scope.Complete();

            }
        }


        /// <summary>
        /// Adds the users to role.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="users">The users.</param>
        public static void AddUsersToRole(string role, string[] users)
        {
            Roles.AddUsersToRole(users, role);
        }

        /// <summary>
        /// Removes the users from role.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="users">The users.</param>
        public static void RemoveUsersFromRole(string role, string[] users)
        {
            Roles.RemoveUsersFromRole(users, role);
        }

        /// <summary>
        /// Removes the roles from user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="roles">The roles.</param>
        public static void RemoveRolesFromUser(string userName, string[] roles)
        {
            foreach (string role in roles)
            {
                Roles.RemoveUserFromRole(userName, role);
            }
        }

        /// <summary>
        /// Adds the roles to user.
        /// </summary>
        /// <param name="roles">The roles.</param>
        /// <param name="user">The user.</param>
        public static void AddRolesToUser(string[] roles, string user)
        {
            foreach (string role in roles)
            {
                Roles.AddUserToRole(user, role);
            }
        }

        /// <summary>
        /// Gets all roles.
        /// </summary>
        /// <returns></returns>
        public static List<Role> GetAllRoles()
        {
            List<Role> roles = new List<Role>();
            foreach (string role in Roles.GetAllRoles())
            {
                roles.Add(new Role(role));
            }
            roles.Sort(new UniversalComparer<Role>("Name"));
            return roles;
        }

        /// <summary>
        /// Gets all roles.
        /// </summary>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        public static List<Role> GetAllRoles(string sortExpression)
        {
            List<Role> userDetails = GetAllRoles();

            if (string.IsNullOrEmpty(sortExpression))
            {
                sortExpression = "Name";
            }
            userDetails.Sort(new UniversalComparer<Role>(sortExpression));
            return userDetails;
        }

        /// <summary>
        /// Gets the roles not related to user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public static List<Role> GetRolesNotRelatedToUser(string userName)
        {
            List<Role> allRoles = GetAllRoles();
            List<Role> returnValues = new List<Role>();
            foreach (Role role in allRoles)
            {
                if (!Roles.IsUserInRole(userName, role.Name))
                    returnValues.Add(role);
            }
            returnValues.Sort(new UniversalComparer<Role>("Name"));
            return returnValues;
        }

        /// <summary>
        /// Gets the name of the user by.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public static UserDetails GetUserByName(string userName)
        {
            if (userName != null)
            {
                MembershipUser user = Membership.GetUser(userName);

                if (user != null)
                {
                    return new UserDetails(user, true);
                }
            }
            return null;
        }

        /// <summary>
        /// Deletes the role.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns></returns>
        public static bool DeleteRole(Role role)
        {
            return Roles.DeleteRole(role.Name);
        }

        /// <summary>
        /// Creates the role.
        /// </summary>
        /// <param name="role">The role.</param>
        public static void CreateRole(Role role)
        {
            Roles.CreateRole(role.Name);
        }


        /// <summary>
        /// Updates the user and profile.
        /// </summary>
        /// <param name="user">The user.</param>
        public static string UpdateUserAndProfile(UserDetails user)
        {
            if (user.IsValid)
            {
                MembershipUser membershipUser = Membership.GetUser(user.UserName);
                membershipUser.Email = user.Email;
                bool success = true;
                if (!string.IsNullOrEmpty(user.OldPassword) && !string.IsNullOrEmpty(user.NewPassword))
                {
                    success = membershipUser.ChangePassword(user.OldPassword, user.NewPassword);
                }
                if (success)
                {
                    Membership.UpdateUser(membershipUser);

                    //update profile too
                    ProfileCommon p = new ProfileCommon();
                    ProfileCommon selectedProfile = p.GetProfile(user.UserName);
                    selectedProfile.WarehouseId = user.WarehouseId;
                    selectedProfile.RegionId = user.RegionId;
                    selectedProfile.SalesLocationId = user.SalesLocationId;
                    selectedProfile.OpCoId = user.OpCoId;
                    selectedProfile.OpCoCode = "";
                    selectedProfile.RegionCode = "";
                    if (user.OpCoId != -1)
                    {
                        selectedProfile.OpCoCode = OpcoController.GetOpCo(user.OpCoId, false).Code;
                    }

                    if (user.RegionId != -1)
                    {
                        selectedProfile.RegionCode = OptrakRegionController.GetRegion(user.RegionId).Code;
                    }

                    selectedProfile.Save();

                    return user.UserName;
                }
                else
                {
                    throw new SystemException("The Password has not be successfully changed.");
                }
            }
            return "";
        }


        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public static bool DeleteUser(UserDetails user)
        {
            if (user != null)
                return Membership.DeleteUser(user.UserName, true);
            else
                return false;
        }

        /// <summary>
        /// Creates the user and profile.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public static string CreateUserAndProfile(UserDetails user)
        {
            if (user.IsValid)
            {
                //save profile details too
                ProfileCommon p = new ProfileCommon();
                ProfileCommon newProfile = p.GetProfile(user.UserName);
                newProfile.WarehouseId = user.WarehouseId;
                newProfile.RegionId = user.RegionId;
                newProfile.OpCoId = user.OpCoId;
                newProfile.SalesLocationId = user.SalesLocationId;
                newProfile.OpCoCode = "";
                newProfile.RegionCode = "";
                if (user.OpCoId != -1)
                {
                    newProfile.OpCoCode = OpcoController.GetOpCo(user.OpCoId, false).Code;
                }

                if (user.RegionId != -1)
                {
                    newProfile.RegionCode = OptrakRegionController.GetRegion(user.RegionId).Code;
                }

                newProfile.Save();


                Membership.CreateUser(user.UserName, user.NewPassword, user.Email);
                return user.UserName;
            }
            return "";
        }


        /// <summary>
        /// Gets the profile.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public static ProfileInfo GetProfile(string userName)
        {
            return ProfileManager.FindProfilesByUserName(ProfileAuthenticationOption.Authenticated, userName)[userName];
        }

        /// <summary>
        /// Determines whether [is user in role] [the specified user name].
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="roleName">Name of the role.</param>
        /// <returns>
        /// 	<c>true</c> if [is user in role] [the specified user name]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsUserInRole(string userName, string roleName)
        {
            List<UserDetails> usersInRole = GetUsersInRole(roleName, null);
            bool isUserInRole = false;
            foreach (UserDetails user in usersInRole)
            {
                if (user.UserName == userName)
                {
                    isUserInRole = true;
                    break;
                }
            }
            return isUserInRole;
        }
    }
}