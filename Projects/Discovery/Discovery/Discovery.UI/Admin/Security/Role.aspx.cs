using System;

namespace Discovery.UI.Web.Security
{
    public partial class Role : DiscoveryDataDetailPage
    {
        protected override bool RequiresAuthentication()
        {
            return true;
        }

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
        }
   }
}