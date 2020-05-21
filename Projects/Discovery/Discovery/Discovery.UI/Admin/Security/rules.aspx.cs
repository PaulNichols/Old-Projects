using System;
using System.Collections.Generic;
using System.IO;
using Discovery.ComponentServices.Security;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Discovery.UI.Web.Security
{
    /*************************************************************************************************
    ** CLASS:	SalesLocations
    **
    ** OVERVIEW:
    ** Page to allow maintenance of a single User
    **
    ** MODIFICATION HISTORY:
    **
    ** Date:		Version:    Who:	Change:
    ** 19/7/06		1.0			PJN		Initial Version
    ************************************************************************************************/

    public partial class Rules : DiscoveryDataItemsPage
    {


        protected void ButtonExportMatrix_Click(object sender, EventArgs e)
        {
            try
            {
                List<BusinessObjects.Rule> rules = null;
                try
                {
                    rules = SecurityController.GetAllRules("Name");
                }
                catch (Exception ex)
                {
                    if (ExceptionPolicy.HandleException(ex, "User Interface")) DisplayMessage("Could not retrieve all rules.");
                }
                List<BusinessObjects.Role> roles = null;
                try
                {
                    roles = SecurityController.GetAllRoles("Name");
                }
                catch (Exception ex)
                {
                    if (ExceptionPolicy.HandleException(ex, "User Interface")) DisplayMessage("Could not retrieve all roles.");
                }
                List<string> lines = new List<string>();
                string line;

                //headers
                line = ",";
                if (roles != null) foreach (BusinessObjects.Role role in roles)
                {
                    line = string.Concat(line, role.Name, ",");
                }
                lines.Add(line.Substring(0, line.Length-1));

                if (rules != null) foreach (BusinessObjects.Rule rule in rules)
                {
                    line = string.Concat(rule.Name,",");
                    if (roles != null) foreach (BusinessObjects.Role role in roles)
                    {
                        line =
                            string.Concat(line,
                                          SecurityController.TestRule(rule.Expression, new string[] {role.Name}, "TestUser")
                                              ? "Y"
                                              : "N", ",");
                    }
                    lines.Add(line.Substring(0, line.Length - 1));
                }
            
                File.WriteAllText(Server.MapPath("\\")+"test.csv",string.Join(Environment.NewLine,lines.ToArray()));
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
            ReadRule = "Admin: View Rules";
            CreateRule = "Admin: Edit Rules";
            DeleteRule = "Admin: Edit Rules";
            UpdateRule = "Admin: Edit Rules";

            //call base functionality
            base.Page_Load(sender, e);
        }
    }
}