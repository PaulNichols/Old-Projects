using System;
using System.Net.Sockets;

namespace Discovery.UI.Web.Admin
{
    public partial class Connections : DiscoveryDataItemsPage
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected override void Page_Load(object sender, EventArgs e)
        {
            ReadRule = "Integration: View Connections";
            CreateRule = "Integration: Edit Connections";
            DeleteRule = "Integration: Edit Connections";
            UpdateRule = "Integration: Edit Connections";

            //call base functionality
            base.Page_Load(sender, e);
        }
   
      
      
}
}