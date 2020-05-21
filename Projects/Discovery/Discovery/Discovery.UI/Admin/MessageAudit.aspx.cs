using System;
using RJS.Web.WebControl;

namespace Discovery.UI.Web.Admin
{
    public partial class MessageAudit : DiscoveryDataDetailPage
    {

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected override void Page_Load(object sender, EventArgs e)
        {
            ReadRule = "Admin: View Message Audits";
            CreateRule = "Admin: Edit Message Audits";
            DeleteRule = "Admin: Edit Message Audits";
            UpdateRule = "Admin: Edit Message Audits";

            //call base class
            base.Page_Load(sender, e);

            PopCalendar.JavaScriptCustomPath = "../App_Themes/DiscoveryDefault/";
        }

    }
}