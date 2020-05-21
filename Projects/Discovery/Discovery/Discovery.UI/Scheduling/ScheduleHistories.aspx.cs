using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Discovery;
using Discovery.Utility;
using Discovery.Scheduling;

namespace Discovery.UI.Web.Scheduling
{
    public partial class ScheduleHistories : DiscoveryDataItemsPage
    {
        protected System.Web.UI.WebControls.DataGrid dgScheduleHistory;


        #region '" Web Form Designer Generated Code "'

        // This call is required by the Web Form Designer.
        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {

            // events handled by Page_Init
            //base.Init += new System.EventHandler(Page_Init);
            // events handled by Page_Load
            base.Load += new System.EventHandler(Page_Load);
        }

        #endregion

        protected override void Page_Load(object sender, EventArgs e)
        {
            ReadRule = "Scheduling: View ScheduleHistory";
            CreateRule = "Scheduling: Edit ScheduleHistory";
            DeleteRule = "Scheduling: Edit ScheduleHistory";
            UpdateRule = "Scheduling: Edit ScheduleHistory";

            //call base class
            base.Page_Load(sender, e);

        }
    }
}
