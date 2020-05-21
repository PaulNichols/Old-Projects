using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Discovery;
using Discovery.Utility;

namespace Discovery.UI.Web.Scheduling
{
    public partial class Schedules : DiscoveryDataItemsPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            ReadRule = "Scheduling: View Schedules";
            CreateRule = "Scheduling: Edit Schedules";
            DeleteRule = "Scheduling: Edit Schedules";
            UpdateRule = "Scheduling: Edit Schedules";


            //call base class
            base.Page_Load(sender, e);
        }

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

        protected void btnHistory_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("~/Scheduling/ScheduleHistories.aspx?Id={0}", (sender as Button).CommandArgument));
        }
    }
}
