using System;
using System.Web.UI.WebControls;
using Discovery.ComponentServices.Security;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Discovery.UI.Web.Security
{
    /*************************************************************************************************
  ** CLASS:	Users
  **
  ** OVERVIEW:
  ** Page to display all users and allow maintenance of those users
  **
  ** MODIFICATION HISTORY:
  **
  ** Date:		Version:    Who:	Change:
  ** 19/7/06		1.0			PJN		Initial Version
  ************************************************************************************************/

    public partial class Users : DiscoveryDataItemsPage
    {

    

        /// <summary>
        /// Handles the DataBound event of the CheckBoxListRoles control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void CheckBoxListRoles_DataBound(object sender, EventArgs e)
        {
            CheckBoxListRoles.Visible = (Request.QueryString["Id"] != null);

            if (Request.QueryString["Id"] != null)
            {
                //take of the autopoost backk to stop the selected index changed event
                //shouldn't be necessary
                CheckBoxListRoles.AutoPostBack = false;
                
                //go though all items in the list of roles and tick or untick the
                //role depending on the users relationship
                foreach (ListItem item in CheckBoxListRoles.Items)
                {
                    string roleName = item.Value;
                    string userName = Request.QueryString["Id"].ToString();
                    item.Selected =
                        System.Web.Security.Roles.IsUserInRole(userName, roleName);
                }
                
                //put autopostback on again
                CheckBoxListRoles.AutoPostBack = true;
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the CheckBoxListRoles control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void CheckBoxListRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string userName = Request.QueryString["Id"].ToString();

                //persist the changes in the check box list of roles the users is or isn't related to
                foreach (ListItem item in CheckBoxListRoles.Items)
                {
                    string roleName = item.Value;
                    if (item.Selected)
                    {
                        try
                        {
                            if (!System.Web.Security.Roles.IsUserInRole(userName, roleName))
                            {
                                SecurityController.AddUsersToRole(roleName, new string[] {userName});
                            }
                        }
                        catch (Exception e1)
                        {
                            if (ExceptionPolicy.HandleException(e1, "User Interface")) DisplayMessage("Could not add user to role.");
                        }
                    }
                    else
                    {
                        try
                        {
                            if (System.Web.Security.Roles.IsUserInRole(userName, roleName))
                            {
                                SecurityController.RemoveUsersFromRole(roleName, new string[] {userName});
                            }
                        }
                        catch (Exception e1)
                        {
                            if (ExceptionPolicy.HandleException(e1, "User Interface")) DisplayMessage("Could not remove user from role.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                   if (ExceptionPolicy.HandleException(ex, "User Interface"))  DisplayMessage(ex);
            }
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected override void Page_Load(object sender, EventArgs e)
        {
            ReadRule = "Admin: View Users";
            CreateRule = "Admin: Edit Users";
            DeleteRule = "Admin: Edit Users";
            UpdateRule = "Admin: Edit Users";
                   
            //call base functionality
            base.Page_Load(sender, e);
            
            //change the text abouve the list of Roles to make it apparent which
            //users roles we are viewing/editing
            if (Request.QueryString["Id"] != null)
                LabelUser.Text = Request.QueryString["Id"].ToString() + ": Related Roles";
            else
                LabelUser.Text = "";
        }


        protected void NewButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect("~/Admin/Security/user.aspx?UrlReferrer=~/Admin/Security/user.aspx?Id={0}");
        }

}
}