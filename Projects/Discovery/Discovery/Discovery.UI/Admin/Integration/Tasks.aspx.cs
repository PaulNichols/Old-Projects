using System;
using System.Net.Sockets;
using System.Web.UI.WebControls;

namespace Discovery.UI.Web.Admin
{
    public partial class Tasks : DiscoveryDataItemsPage
    {
        protected void ConnectionsDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            //if (e.Exception != null && e.Exception.InnerException != null && e.Exception.InnerException is SocketException)
            //{
            //    e.ExceptionHandled = true;
            //    DisplayMessage("An error occured whilst retriving data. It is possible that the Integration Windows Service is not running.");
            //    NewButton.Enabled = false;
            //}
        }
        
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected override void Page_Load(object sender, EventArgs e)
        {
            ReadRule = "Integration: View Tasks";
            CreateRule = "Integration: Edit Tasks";
            DeleteRule = "Integration: Edit Tasks";
            UpdateRule = "Integration: Edit Tasks";

            //call base functionality
            base.Page_Load(sender, e);
        }


    }
}