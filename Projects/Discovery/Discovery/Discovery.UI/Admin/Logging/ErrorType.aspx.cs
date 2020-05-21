using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Discovery.BusinessObjects;
using Discovery.UI.Web;

public partial class Admin_Logging_ErrorType : DiscoveryDataDetailPage
{
    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
    protected override void Page_Load(object sender, EventArgs e)
    {
        ReadRule = "Admin: View Error Handling";
        CreateRule = "Admin: View Error Handling";
        DeleteRule = "Admin: View Error Handling";
        UpdateRule = "Admin: View Error Handling";

        //call base functionality
        base.Page_Load(sender, e);

     
    }


    protected override bool AllowAutoInsertMode()
    {
        return false;
    }

    protected override bool HasPageLoadAuthorisation()
    {
       
            return CanRead;
       
    }

    protected void ErrorFormView_DataBound(object sender, EventArgs e)
    {
        //hide the email details if this execption type does not have an email handler related to it
        if (PageFormView.CurrentMode == FormViewMode.ReadOnly || PageFormView.CurrentMode == FormViewMode.Edit)
        {
            bool hasEmailHandler = ((ErrorType)PageFormView.DataItem).HasEmailHandler;
            bool emailOperator = ((ErrorType)PageFormView.DataItem).EmailOperator;
            
            GetControl<Control>("LabelEmailRecipientsText", PageFormView).Visible = hasEmailHandler;
            GetControl<Control>("LabelEmailOperatorText", PageFormView).Visible = hasEmailHandler;
            GetControl<Control>("LabelEmailSubjectText", PageFormView).Visible = hasEmailHandler;
            if (PageFormView.CurrentMode == FormViewMode.ReadOnly)
            {
                GetControl<Control>("ImageEmailOperator", PageFormView).Visible = hasEmailHandler && emailOperator;
                GetControl<Control>("ImageDontEmailOperator", PageFormView).Visible = hasEmailHandler && !emailOperator;
                GetControl<Control>("LabelEmailRecipients", PageFormView).Visible = hasEmailHandler;
                GetControl<Control>("LabelEmailSubject", PageFormView).Visible = hasEmailHandler;
            }
            else if (PageFormView.CurrentMode == FormViewMode.Edit)
            {
                GetControl<Control>("TextBoxEmailRecipients", PageFormView).Visible = hasEmailHandler;
                GetControl<Control>("CheckBoxEmailOperator", PageFormView).Visible = hasEmailHandler;
                GetControl<Control>("TextBoxEmailSubject", PageFormView).Visible = hasEmailHandler;

            }

        }
    }
}
