using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Discovery.ComponentServices.Security;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Discovery.UI.Web.ReferenceData
{
    /*************************************************************************************************
    ** CLASS:	AddressController
    **
    ** OVERVIEW:
    ** This page allows a user to view, edit, delete or insert a single warehouse
    **
    ** MODIFICATION HISTORY:
    **
    ** Date:		Version:    Who:	Change:
    ** 19/7/06		1.0			PJN		Initial Version
    ************************************************************************************************/

    public partial class Warehouse : DiscoveryDataDetailPage
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
            ReadRule = "Reference Data: View Warehouses";
            CreateRule = "Reference Data: Edit Warehouses";
            DeleteRule = "Reference Data: Edit Warehouses";
            UpdateRule = "Reference Data: Edit Warehouses";

            //call base class
            base.Page_Load(sender, e);
        }

        #endregion

        protected override void SetValidation()
        {
            Validation.AddValidation("TextBoxCode", "Code");
            Validation.AddValidation("TextBoxDescription", "Description");
            Validation.AddValidation("TextBoxPrinterName", "PrinterName");
            Validation.AddValidation("TextBoxSalesEmail", "SalesEmail");
            Validation.AddValidation("DropDownListRegion", "RegionId");
            Validation.AddValidation("TextBoxContactTelephone", "ContactTelephoneNumber");
            Validation.AddValidation("TextBoxContactName", "ContactName");
            Validation.AddValidation("TextBoxAddress1", "AddressLine1");
            Validation.AddValidation("TextBoxPostCode", "PostCode");
            base.SetValidation();
        }

        //protected void LabelPrinter_DataBinding(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(((BusinessObjects.Warehouse) PageFormView.DataItem).PrinterName))
        //    {
        //        ((Label) sender).Text = "(None)";
        //    }
        //}

        protected void WarehouseFormView_ItemDeleting(object sender, FormViewDeleteEventArgs e)
        {
            
            List<UserDetails> userDetails = null;
            try
            {
                userDetails = SecurityController.GetAllUsers("", "UserName",true);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "User Interface")) DisplayMessage("Failed to retrieve Users.");
            }
            if (userDetails != null && userDetails.Count>0)
            {
                //we have a complete list of users on the system, now find a subset of those whos 
                //profile details realate to the current warehouse
                List<UserDetails> relatedUsers = userDetails.FindAll(
                    delegate(UserDetails userDetail)
                        {
                            return (userDetail.WarehouseId == Convert.ToInt32(Request.QueryString["Id"]));
                        });
                if (relatedUsers != null && relatedUsers.Count>0)
                {
                    List<string> listItems=relatedUsers.ConvertAll(new Converter<UserDetails, string>(
                                                    delegate(UserDetails userDetail)
                                                        {
                                                            //method to convert the User Details to a html bullet list item
                                                            return string.Concat("<li>",userDetail.UserName,"</li>");
                                                        }
                                                    ));
                    DisplayMessage(
                        string.Format(
                            "This Warehouse cannot be deleted as it is related to the following User Profile(s) :<ul>{0}</ul>Please change these Profile(s) in the Users screen first.",
                            string.Join("",listItems.ToArray())));

                    e.Cancel = true;
                }
            }
        }
    }
}