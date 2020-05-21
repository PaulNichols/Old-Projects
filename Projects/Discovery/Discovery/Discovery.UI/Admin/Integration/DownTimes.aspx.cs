using System;
using System.Net.Sockets;
using System.Web.UI.WebControls;

namespace Discovery.UI.Web.Admin
{
    public partial class DownTimes : DiscoveryDataItemsPage
    {
        protected void ConnectionsObjectDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            e.ExceptionHandled = true;
        }

        private void HandleException(ObjectDataSourceStatusEventArgs e)
        {
            //if (e.Exception != null && e.Exception.InnerException != null && e.Exception.InnerException is SocketException)
            //{
            //    e.ExceptionHandled = true;
            //    DisplayMessage("An error occured whilst retriving data. It is possible that the Integration Windows Service is not running.");
            //    NewButton.Enabled = false;
            //}
        }

        protected void DownTimesDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            HandleException(e);
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected override void Page_Load(object sender, EventArgs e)
        {
            ReadRule = "Integration: View Down Times";
            CreateRule = "Integration: Edit Down Times";
            DeleteRule = "Integration: Edit Down Times";
            UpdateRule = "Integration: Edit Down Times";

            //call base functionality
            base.Page_Load(sender, e);
        }


     
}
}