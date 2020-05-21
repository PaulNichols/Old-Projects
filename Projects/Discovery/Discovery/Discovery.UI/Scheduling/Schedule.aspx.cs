using System.Reflection;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.ComponentModel;
using System.IO;
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
    public partial class Schedule : DiscoveryDataDetailPage
    {
        //protected System.Web.UI.WebControls.CheckBox chkEnabled;
        //protected System.Web.UI.WebControls.TextBox txtTimeLapse;
        //protected System.Web.UI.WebControls.DropDownList ddlTimeLapseMeasurement;
        //protected System.Web.UI.WebControls.TextBox txtRetryTimeLapse;
        //protected System.Web.UI.WebControls.DropDownList ddlRetryTimeLapseMeasurement;
        //protected System.Web.UI.WebControls.DropDownList ddlRetainHistoryNum;
        //protected System.Web.UI.WebControls.DropDownList ddlAttachToEvent;
        //protected System.Web.UI.WebControls.DropDownList ddlTypes;
        //protected System.Web.UI.WebControls.CheckBox chkCatchUpEnabled;
        //protected System.Web.UI.WebControls.TextBox txtObjectDependencies;
        //protected System.Web.UI.WebControls.Panel pnlScheduleItem;

        protected override void Page_Load(object sender, EventArgs e)
        {
            ReadRule = "Scheduling: View Schedules";
            CreateRule = "Scheduling: Edit Schedules";
            DeleteRule = "Scheduling: Edit Schedules";
            UpdateRule = "Scheduling: Edit Schedules";

            //call base class
            base.Page_Load(sender, e);

        }

        //private void BindData()
        //{
        //    ScheduleItem objScheduleItem = null;
        //    ddlTypes.DataSource = GetAssemblyTypes();
        //    ddlTypes.DataBind();

        //    // **LAS** We check view state as it's always up to date, see Page_Init
        //    if (ViewState["id"] != null)
        //    {
        //        objScheduleItem = SchedulingProvider.Instance().GetSchedule(Convert.ToInt32(ViewState["id"]));
        //        if (ddlTypes.Items.FindByValue(objScheduleItem.TypeFullName) != null)
        //        {
        //            ddlTypes.Items.FindByValue(objScheduleItem.TypeFullName).Selected = true;
        //        }
        //        chkEnabled.Checked = objScheduleItem.Enabled;
        //        if (objScheduleItem.TimeLapse == Null.NullInteger)
        //        {
        //            txtTimeLapse.Text = "";
        //        }
        //        else
        //        {
        //            txtTimeLapse.Text = Convert.ToString(objScheduleItem.TimeLapse);
        //        }

        //        if (ddlTimeLapseMeasurement.Items.FindByValue(objScheduleItem.TimeLapseMeasurement) != null)
        //        {
        //            ddlTimeLapseMeasurement.Items.FindByValue(objScheduleItem.TimeLapseMeasurement).Selected = true;
        //        }
        //        if (objScheduleItem.RetryTimeLapse == Null.NullInteger)
        //        {
        //            txtRetryTimeLapse.Text = "";
        //        }
        //        else
        //        {
        //            txtRetryTimeLapse.Text = Convert.ToString(objScheduleItem.RetryTimeLapse);
        //        }
        //        if (ddlRetryTimeLapseMeasurement.Items.FindByValue(objScheduleItem.RetryTimeLapseMeasurement) != null)
        //        {
        //            ddlRetryTimeLapseMeasurement.Items.FindByValue(objScheduleItem.RetryTimeLapseMeasurement).Selected = true;
        //        }
        //        if (ddlRetainHistoryNum.Items.FindByValue(Convert.ToString(objScheduleItem.RetainHistoryNum)) != null)
        //        {
        //            ddlRetainHistoryNum.Items.FindByValue(Convert.ToString(objScheduleItem.RetainHistoryNum)).Selected = true;
        //        }
        //        if (ddlAttachToEvent.Items.FindByValue(objScheduleItem.AttachToEvent) != null)
        //        {
        //            ddlAttachToEvent.Items.FindByValue(objScheduleItem.AttachToEvent).Selected = true;
        //        }
        //        chkCatchUpEnabled.Checked = objScheduleItem.CatchUpEnabled;
        //        txtObjectDependencies.Text = objScheduleItem.ObjectDependencies;
        //    }
        //}

      

        protected string GetTimeLapse(int TimeLapse, string TimeLapseMeasurement)
        {
            if (TimeLapse != Null.NullInteger)
            {
                string str = null;
                switch (TimeLapseMeasurement)
                {
                    case "m":
                        {
                            // Minutes
                            str = "every " + TimeLapse.ToString() + " minute";
                            break;
                        }
                    case "h":
                        {
                            // Hours
                            str = "every " + TimeLapse.ToString() + " hour";
                            break;
                        }
                    case "d":
                        {
                            // Days
                            str = "every " + TimeLapse.ToString() + " day";
                            break;
                        }
                }

                if (TimeLapse > 1)
                {
                    str += "s";
                }

                return str.ToString();
            }
            else
            {
                return "n/a";
            }
        }
    
    }
}
