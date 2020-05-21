using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Entities.Users;
using DotNetNuke.Framework;
using DotNetNuke.Security.Membership;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
//using BusinessObjects;
using Image=System.Web.UI.WebControls.Image;

namespace M2.Modules.FundManager
{
    public class LoadUsers
    {
        public void CreatePortal1Users()
        {
            EntitySpaces.Interfaces.esProviderFactory.Factory = new EntitySpaces.LoaderMT.esDataProviderFactory();

            DotNetNuke.Entities.Users.UserInfo objUserInfo = new DotNetNuke.Entities.Users.UserInfo();
            DotNetNuke.Entities.Users.UserInfo objNewUser = new DotNetNuke.Entities.Users.UserInfo();

            //By default authorise registration to both portals (1 & 3).
            objNewUser.PortalID = 1;
            objNewUser.FirstName = "Fred";

            objNewUser.LastName = "Flintstone";
            objNewUser.Email = "marcus.wade@m2ws.com";
            objNewUser.Username = "fredf";
            objNewUser.DisplayName = objNewUser.FirstName.ToString() + " " + objNewUser.LastName.ToString();
            objNewUser.AffiliateID = DotNetNuke.Common.Utilities.Null.NullInteger;

            objNewUser.Membership.Password = "p4$$w0rd";
            objNewUser.Membership.Approved = true;
            objNewUser.Membership.Username = "fredf";

            objNewUser.Profile.InitialiseProfile(0);
            objNewUser.Profile.FirstName = "Fred";
            objNewUser.Profile.LastName = "Flintstone";
            objNewUser.Profile.Telephone = "";
            objNewUser.Profile.Website = "";

            objNewUser.Profile.SetProfileProperty("Unit", "");
            objNewUser.Profile.SetProfileProperty("Street", "");
            objNewUser.Profile.SetProfileProperty("City", "");
            objNewUser.Profile.SetProfileProperty("Region", "");
            objNewUser.Profile.SetProfileProperty("PostalCode", "");
            objNewUser.Profile.SetProfileProperty("Country", "");
            objNewUser.Profile.SetProfileProperty("Cell", "");
            objNewUser.Profile.SetProfileProperty("Fax", "");
            objNewUser.Profile.SetProfileProperty("IM", "");
            //objNewUser.Profile.SetProfileProperty("Suffix", dr[4].ToString());

            //Add the User
            UserCreateStatus createStatus;
            createStatus = UserController.CreateUser(ref objNewUser);

            //set roles here

            //DotNetNuke.Security.Roles.RoleController objRoleController = new DotNetNuke.Security.Roles.RoleController();
            ////now add our role that this user will have
            //objRoleController.AddUserRole(0, objNewUser.UserID, 1, DateTime.Now, DateTime.Now.AddYears(100));
        }

        public void CreatePortal3Users()
        {
            EntitySpaces.Interfaces.esProviderFactory.Factory = new EntitySpaces.LoaderMT.esDataProviderFactory();

            DotNetNuke.Entities.Users.UserInfo objUserInfo = new DotNetNuke.Entities.Users.UserInfo();
            DotNetNuke.Entities.Users.UserInfo objNewUser = new DotNetNuke.Entities.Users.UserInfo();

            //By default authorise registration to both portals (1 & 3).
            objNewUser.PortalID = 3;
            objNewUser.FirstName = "Fred";

            objNewUser.LastName = "Flintstone";
            objNewUser.Email = "marcus.wade@m2ws.com";
            objNewUser.Username = "fredf";
            objNewUser.DisplayName = objNewUser.FirstName + " " + objNewUser.LastName;
            objNewUser.AffiliateID = DotNetNuke.Common.Utilities.Null.NullInteger;

            objNewUser.Membership.Password = "p4$$w0rd";
            objNewUser.Membership.Approved = true;
            objNewUser.Membership.Username = "fredf";

            objNewUser.Profile.InitialiseProfile(0);
            objNewUser.Profile.FirstName = "Fred";
            objNewUser.Profile.LastName = "Flintstone";
            objNewUser.Profile.Telephone = "";
            objNewUser.Profile.Website = "";

            objNewUser.Profile.SetProfileProperty("Unit", "");
            objNewUser.Profile.SetProfileProperty("Street", "");
            objNewUser.Profile.SetProfileProperty("City", "");
            objNewUser.Profile.SetProfileProperty("Region", "");
            objNewUser.Profile.SetProfileProperty("PostalCode", "");
            objNewUser.Profile.SetProfileProperty("Country", "");
            objNewUser.Profile.SetProfileProperty("Cell", "");
            objNewUser.Profile.SetProfileProperty("Fax", "");
            objNewUser.Profile.SetProfileProperty("IM", "");
            //objNewUser.Profile.SetProfileProperty("Suffix", dr[4].ToString());

            //Add the User
            UserCreateStatus createStatus;
            createStatus = UserController.CreateUser(ref objNewUser);

            //set roles here

            //DotNetNuke.Security.Roles.RoleController objRoleController = new DotNetNuke.Security.Roles.RoleController();
            ////now add our role that this user will have
            //objRoleController.AddUserRole(0, objNewUser.UserID, 1, DateTime.Now, DateTime.Now.AddYears(100));
        }
    }
}
