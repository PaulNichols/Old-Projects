using System;
using System.Collections.Generic;
using Discovery.ComponentServices.Security;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Discovery.UI.Web.ReferenceData
{
    /*************************************************************************************************
 ** CLASS:	SalesLocation
 **
 ** OVERVIEW:
 ** This page allows a user to edit, view, delete or insert a single Sales Location
 **
 ** MODIFICATION HISTORY:
 **
 ** Date:		Version:    Who:	Change:
 ** 19/7/06		1.0			PJN		Initial Version
 ************************************************************************************************/
    public partial class SalesLocation : DiscoveryDataDetailPage
    {
        #region Properties

        #endregion

        #region Protected Methods

      

        #endregion

        #region Events
        
        protected override void Page_Load(object sender, EventArgs e)
        {
            ReadRule = "Reference Data: View Sales Locations";
            CreateRule = "Reference Data: Edit Sales Locations";
            UpdateRule = "Reference Data: Edit Sales Locations";
            DeleteRule = "Reference Data: Edit Sales Locations";
            base.Page_Load(sender, e);
        }
        
        #endregion

        protected override void SetValidation()
        {
            Validation.AddValidation("TextBoxLocation", "Location");
            Validation.AddValidation("TextBoxDescription", "Description");
            Validation.AddValidation("TextBoxTelephoneNumber", "TelephoneNumber");
            base.SetValidation();
        }

       protected void SalesLocationFormView_ItemDeleting(object sender, System.Web.UI.WebControls.FormViewDeleteEventArgs e)
        {
            List<UserDetails> userDetails = null;
           try
           {
               userDetails = SecurityController.GetAllUsers("", "UserName", true);
           }
           catch (Exception ex)
           {
               if (ExceptionPolicy.HandleException(ex, "User Interface")) DisplayMessage("Faile to retrieve Users.");
           }
           if (userDetails != null && userDetails.Count > 0)
            {
                //we have a complete list of users on the system, now find a subset of those whos 
                //profile details realate to the current sales location
                List<UserDetails> relatedUsers = userDetails.FindAll(
                    delegate(UserDetails userDetail)
                    {
                        return (userDetail.SalesLocationId == Convert.ToInt32(Request.QueryString["Id"]));
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
                            "This Sales Location cannot be deleted as it is related to the following User Profile(s) :<ul>{0}</ul>Please change these Profile(s) in the Users screen first.",
                            string.Join("", listItems.ToArray())));

                    e.Cancel = true;
                }
            }
        }
}
}