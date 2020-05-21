using System;
using System.Web.UI.WebControls;

namespace Discovery.UI.Web.ReferenceData
{
    /*************************************************************************************************
     ** CLASS:	LogEntry
     **
     ** OVERVIEW:
     ** This page allows a user to add,edit,delete and view a single LogEntry
     **
     ** MODIFICATION HISTORY:
     **
     ** Date:		Version:    Who:	Change:
     ** 19/7/06		1.0			PJN		Initial Version
     ************************************************************************************************/

    public partial class LogEntry : DiscoveryDataDetailPage
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
            ReadRule = "Admin: View Log Entries";
            CreateRule = "Admin: Edit Log Entries";
            DeleteRule = "Admin: Edit Log Entries";
            UpdateRule = "Admin: Edit Log Entries";

            //call base functionality
            base.Page_Load(sender, e);
        }

        #endregion

        protected override void SetValidation()
        {
            // base.SetValidation();
            //validation.AddValidation("TextBoxCode", "Code");
            //validation.AddValidation("TextBoxDescription", "Description");
        }


        protected void ButtonAcknowledged_Click(object sender, EventArgs e)
        {
            Discovery.BusinessObjects.Controllers.LogController.AcknowledgeLog(Convert.ToInt32(Request.QueryString["Id"]),
                                                                               User.Identity.Name);

            logentryFormView.DataBind();
        }

        protected void logentryFormView_DataBound(object sender, EventArgs e)
        {
            BusinessObjects.LogEntry logEntry = (BusinessObjects.LogEntry)logentryFormView.DataItem;
            GetControl<ImageButton>("ButtonAck", logentryFormView).Visible = !logEntry.Acknowledged && logEntry.RequiresAcknowledgement;
        }
    }
}