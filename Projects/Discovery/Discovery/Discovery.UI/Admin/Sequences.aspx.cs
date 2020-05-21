using System;

namespace Discovery.UI.Web.Admin
{
    public partial class Sequences : DiscoveryDataItemsPage
    {
     

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected override void Page_Load(object sender, EventArgs e)
        {
            ReadRule = "Admin: View Sequence Numbers";
            CreateRule = "Admin: Edit Sequence Numbers";
            DeleteRule = "Admin: Edit Sequence Numbers";
            UpdateRule = "Admin: Edit Sequence Numbers";

            //call base class
            base.Page_Load(sender, e);
        }
    }
}