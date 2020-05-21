using System;
using System.Collections.Generic;
using Discovery.ComponentServices.Security;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Discovery.UI.Web.ReferenceData
{
    /*************************************************************************************************
     ** CLASS:	OpCo
     **
     ** OVERVIEW:
     ** This page allows a user to add,edit,delete and view a single opco
     **
     ** MODIFICATION HISTORY:
     **
     ** Date:		Version:    Who:	Change:
     ** 19/7/06		1.0			PJN		Initial Version
     ************************************************************************************************/
    public partial class OpCo : DiscoveryDataDetailPage
    {
        #region Properties

        #endregion

        #region Protected Methods

        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected override void Page_Load(object sender, EventArgs e)
        {
            ReadRule = "Reference Data: View Opcos";
            CreateRule = "Reference Data: Edit Opcos";
            DeleteRule = "Reference Data: Edit Opcos";
            UpdateRule = "Reference Data: Edit Opcos";

            //call base class
            base.Page_Load(sender, e);
        }
        #endregion

     
        protected override void SetValidation()
        {
            Validation.AddValidation("TextBoxCode", "Code");
            Validation.AddValidation("TextBoxDescription", "Description");
            base.SetValidation();
        }

        protected void OpCoFormView_ItemDeleting(object sender, System.Web.UI.WebControls.FormViewDeleteEventArgs e)
        {
            List<UserDetails> userDetails = null;
            try
            {
                userDetails = SecurityController.GetAllUsers("", "UserName", true);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "User Interface")) DisplayMessage("Failed to retrieve all users.");
            }

            if (userDetails != null && userDetails.Count > 0)
            {
                //we have a complete list of users on the system, now find a subset of those whos 
                //profile details realate to the current sales location
                List<UserDetails> relatedUsers = userDetails.FindAll(
                    delegate(UserDetails userDetail)
                    {
                        return (userDetail.OpCoId == Convert.ToInt32(Request.QueryString["Id"]));
                    });
                if (relatedUsers != null && relatedUsers.Count > 0)
                {
                    List<string> listItems = relatedUsers.ConvertAll(new Converter<UserDetails, string>(
                                                    delegate(UserDetails userDetail)
                                                    {
                                                        //method to convert the User Details to a html bullet list item
                                                        return string.Concat("<li>", userDetail.UserName, "</li>");
                                                    }
                                                    ));
                    DisplayMessage(
                        string.Format(
                            "This Operating Company cannot be deleted as it is related to the following User Profile(s) :<ul>{0}</ul>Please change these Profile(s) in the Users screen first.",
                            string.Join("", listItems.ToArray())));

                    e.Cancel = true;
                }
            }
        }

    
    }
}