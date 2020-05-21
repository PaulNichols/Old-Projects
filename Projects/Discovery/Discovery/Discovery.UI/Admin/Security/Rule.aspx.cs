using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Discovery.ComponentServices.Security;
using Discovery.Utility;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Security;

namespace Discovery.UI.Web.Security
{
    /*************************************************************************************************
   ** CLASS:	Rule
   **
   ** OVERVIEW:
   ** Page to allow maintenance of a single Authorisation Rule
   **
   ** MODIFICATION HISTORY:
   **
   ** Date:		Version:    Who:	Change:
   ** 19/7/06		1.0			PJN		Initial Version
   ************************************************************************************************/

    public partial class Rule : DiscoveryDataDetailPage
    {
        /// <summary>
        /// Handles the Click event of the ButtonTest control. This method will test an expression entered by the user
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void ButtonTest_Click(object sender, EventArgs e)
        {
            Label labelTestResult = GetControl<Label>("LabelTestResult", PageFormView);
            bool result=false ;
            TextBox textBoxExpression = GetControl<TextBox>("TextBoxExpression", PageFormView);

            //get the entered expression to test
            string expression = textBoxExpression.Text;

            if (!string.IsNullOrEmpty(expression))
            {
                string userName;

                TextBox textBoxUserName = GetControl<TextBox>("TextBoxUserName", PageFormView);

                TextBox textBoxRoles = GetControl<TextBox>("TextBoxRoles", PageFormView);

                //get the user name to test against
                userName = textBoxUserName.Text;
                //get the rule(s) to test against
                string[] roles =
                    textBoxRoles.Text.Split(new string[] {Environment.NewLine},
                                            StringSplitOptions.RemoveEmptyEntries);
                try
                {
                    //test!!
                    result = SecurityController.TestRule(expression, roles, userName);
                }
                catch (Exception ex)
                {
                      if (ExceptionPolicy.HandleException(ex, "User Interface"))  DisplayMessage("Failed to test rule.",DiscoveryMessageType.Error);
                }
            }
            //display result
            labelTestResult.Text = result? "Result: Authorised": "Result: Not Authorised";
            
        }

        /// <summary>
        /// Handles the Inserted event of the DataSource control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs"/> instance containing the event data.</param>
        protected override void DataSource_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            try
            {
                base.DataSource_Inserted(sender, e);
            }
            catch
            {
                Response.Redirect("Rules.aspx");
            }
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonAddRole control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void LinkButtonAddRole_Click(object sender, EventArgs e)
        {
            //add a new role to test against
            //get the Role text box that lists the roles to test against
            TextBox textBoxRoles = GetControl<TextBox>("TextBoxRoles", PageFormView);
            //get the drop downlist of avaliable roles
            DropDownList dropDownListRoles = GetControl<DropDownList>("DropDownListRoles", PageFormView);

            //add the new role to the text box of roles with or without a newline, depending on whether it's the first to be added or not
            if (textBoxRoles.Text != "")
                textBoxRoles.Text += Environment.NewLine;

            textBoxRoles.Text += dropDownListRoles.SelectedValue;
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected override void Page_Load(object sender, EventArgs e)
        {
            ReadRule = "Admin: View Rules";
            CreateRule = "Admin: Edit Rules";
            DeleteRule = "Admin: Edit Rules";
            UpdateRule = "Admin: Edit Rules";

            //call base functionality
            base.Page_Load(sender, e);
        }




        protected void RouteFormView_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
           // CacheManager.Add("RuleProvider", AuthorizationFactory.GetAuthorizationProvider("RuleProvider"));
        }
}
}