using System;
using System.Security.Principal;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Discovery.UI.Web.Security
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //    WindowsIdentity id =  WindowsIdentity.GetCurrent();
            //Response.Write ("<b>Windows Identity Check</b><br>");
            //Response.Write ("Name: " + id.Name + "<br>"); 
            Master.FindControl("Menu1").Visible = false;
        }

        protected void Login1_LoginError(object sender, EventArgs e)
        {
            //There was a problem logging in the user

            //See if this user exists in the database
            MembershipUser userInfo = Membership.GetUser(Login2.UserName);

            if (userInfo == null)
                //The user entered an invalid username...
                ((Label)Login2.FindControl("LoginErrorDetails")).Text = "There is no user in the database with the username " + Login2.UserName;
            else
            {
                //See if the user is locked out or not approved
                if (! userInfo.IsApproved)
                    ((Label)Login2.FindControl("LoginErrorDetails")).Text =
                        "Your account has not yet been approved by the site's administrators. Please try again later...";
                else if (userInfo.IsLockedOut)
                    ((Label)Login2.FindControl("LoginErrorDetails")).Text =
                        "Your account has been locked out because of a maximum number of incorrect Login attempts. You will NOT be able to Login until you contact a site administrator and have your account unlocked.";
                else 
                    //The password was incorrect (don't show anything, the Login control already describes the problem)
                    ((Label)Login2.FindControl("LoginErrorDetails")).Text = String.Empty;
            }
        }
    }
}