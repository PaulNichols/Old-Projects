using System;
using System.Web.UI.WebControls;
using Discovery.ComponentServices.Security;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Discovery.UI.Web.Security
{
    /*************************************************************************************************
  ** CLASS:	Roles
  **
  ** OVERVIEW:
  ** Page to show the user all roles and allow maintenance of these roles
  **
  ** MODIFICATION HISTORY:
  **
  ** Date:		Version:    Who:	Change:
  ** 19/7/06		1.0			PJN		Initial Version
  ************************************************************************************************/

    public partial class Roles : DiscoveryDataItemsPage
    {
     
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected override void Page_Load(object sender, EventArgs e)
        {
            ReadRule = "Admin: View Roles";
            CreateRule = "Admin: Edit Roles";
            DeleteRule = "Admin: Edit Roles";
            UpdateRule = "Admin: Edit Roles";

            //call base class
            base.Page_Load(sender, e);
            bool show = !string.IsNullOrEmpty(LiteralRoleName.Text);
            //if a role has been selected then show the grid of related users
            ShowUserGrid(show);
        }
        
        /// <summary>
        /// Shows or hides the users grid and the a to z filter control.
        /// </summary>
        /// <param name="show">if set to <c>true</c> [show].</param>
        private void ShowUserGrid(bool show)
        {
            GridViewUsers.Visible = show;
            //if the grid of users is shown then show the a to z control
            AToZ.Visible = show;
        }

        /// <summary>
        /// Handles the RowDeleted event of the GridView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Web.UI.WebControls.GridViewDeletedEventArgs"/> instance containing the event data.</param>
        protected void GridView_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            //handle and display any errors when a roles is deleted
            if (e.Exception != null)
            {
                //handle error
                if (ExceptionPolicy.HandleException(e.Exception, "User Interface")) throw e.Exception;
                DisplayMessage(e.Exception.InnerException.Message);
                e.ExceptionHandled = true;
            }
            else
            {
                LiteralRoleName.Text = "";
                ShowUserGrid(false);
            }
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonAddNew control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void LinkButtonAddNew_Click(object sender, EventArgs e)
        {
            //create a new role
            BusinessObjects.Role role = new BusinessObjects.Role(TextBoxRoleName.Text);
            bool show;
            try
            {
                //save the role

                    SecurityController.CreateRole(role);
                   
                //refresh the screen
                //Response.Redirect(Request.Path, true);
                GridView1.DataBind();
                TextBoxRoleName.Text = "";
                LiteralRoleName.Text = role.Name;
                GridViewUsers.DataBind();
                show = true;
            }
            catch (Exception ex)
            {
                //handle error
                 if (ExceptionPolicy.HandleException(ex, "User Interface"))  DisplayMessage(ex);
                show = false;
            }

            ShowUserGrid(show);
        }


        /// <summary>
        /// Handles the RowDataBound event of the GridViewUsers control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void GridViewUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {//if it's a data row and not a header etc

                //find the is in role ckeckbox in the grid of users
                CheckBox isInRoleCheckBox = GetControl < CheckBox>("CheckBoxUserInRole", e.Row);
                //get the name of the role which has been selected
                string roleName = LiteralRoleName.Text;
                //get the UserDetails data related with this row that is being databound
                UserDetails user = e.Row.DataItem as UserDetails;

                if (!string.IsNullOrEmpty(roleName) && user != null)
                {
                    //check or uncheck the user if they are in the selected role
                    isInRoleCheckBox.Checked = System.Web.Security.Roles.IsUserInRole(user.UserName.ToLower(), roleName.ToLower());
                }
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the CheckBoxUserInRole control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void CheckBoxUserInRole_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //save any changes to the user as it is checked/unchecked
                CheckBox checkBoxUserInRole = (CheckBox) sender;
                string roleName = LiteralRoleName.Text;
                string userName =
                    GridViewUsers.DataKeys[((GridViewRow) (checkBoxUserInRole).NamingContainer).RowIndex].Value.ToString();

                if (checkBoxUserInRole.Checked)
                {
                    if (!SecurityController.IsUserInRole(userName, roleName))
                    {
                        //relate the user to the role if they are not already
                        SecurityController.AddUsersToRole(roleName, new string[] {userName});
                    }
                }
                else
                {
                    //unrelate the user from the role
                    SecurityController.RemoveUsersFromRole(roleName, new string[] {userName});
                }
            }
            catch (Exception ex)
            {
                  if (ExceptionPolicy.HandleException(ex, "User Interface"))  DisplayMessage(ex);
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
         if (e.CommandName=="Show Users")
         {
             LiteralRoleName.Text= GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
             GridViewUsers.DataBind();
         }
        }

    }
}