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
using System.Xml;
using System.Net;
using System.IO;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using Discovery.Scheduling;

namespace Discovery.UI.Web.Scheduling
{
    public partial class ScheduleStatus : DiscoveryDataItemsPage
    {
        //protected System.Web.UI.WebControls.DataGrid dgScheduleQueue;
        //protected System.Web.UI.WebControls.Panel pnlScheduleProcessing;
        //protected System.Web.UI.WebControls.Panel pnlScheduleQueue;
        //protected System.Web.UI.WebControls.Label lblStatus;
        //protected System.Web.UI.WebControls.LinkButton btnStart;
        //protected System.Web.UI.WebControls.LinkButton btnStop;
        //protected System.Web.UI.WebControls.DataGrid dgScheduleProcessing;
        //protected System.Web.UI.WebControls.Label lblMaxThreads;
        //protected System.Web.UI.WebControls.Label lblActiveThreads;
        //protected System.Web.UI.WebControls.Label lblFreeThreads;

        private Discovery.Scheduling.ScheduleStatus Status;

        #region '" Web Form Designer Generated Code "'

        // This call is required by the Web Form Designer.
        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {

            // events handled by Page_Init
            //base.Init += new System.EventHandler(Page_Init);
            // events handled by Page_Load
            base.Load += new System.EventHandler(Page_Load);
            // events handled by btnStart_Click
            btnStart.Click += new System.EventHandler(btnStart_Click);
            // events handled by btnStop_Click
            btnStop.Click += new System.EventHandler(btnStop_Click);
        }

        #endregion 

        protected override void Page_Load(object sender, EventArgs e)
        {
            ReadRule = "Scheduling: View ScheduleStatus";
            CreateRule = "Scheduling: Edit ScheduleStatus";
            DeleteRule = "Scheduling: Edit ScheduleStatus";
            UpdateRule = "Scheduling: Edit ScheduleStatus";


            //call base class
            base.Page_Load(sender, e);

            try
            {
                if (SchedulingProvider.Enabled)
                {
                    if (!Page.IsPostBack)
                    {
                        BindData();
                        BindStatus();
                    }
                }
                else
                {
                    //Skin.AddModuleMessage(
                    //            this,
                    //            "Scheduling is currently disabled due to the value of the 'enabled' setting in web.config.",
                    //            Skins.ModuleMessage.ModuleMessageType.RedError);

                    lblStatus.Text = "Disabled";
                    btnStart.Enabled = false;
                    btnStop.Enabled = false;
                    lblFreeThreads.Text = "0";
                    lblActiveThreads.Text = "0";
                    lblMaxThreads.Text = "0";
                    pnlScheduleQueue.Visible = false;
                    pnlScheduleProcessing.Visible = false;
                }
            }
            catch (Exception exc)
            {
                //// Module failed to load
                //Exceptions.ProcessModuleLoadException(this, exc);
            }

        }

        private void BindStatus()
        {
            Status = SchedulingProvider.Instance().GetScheduleStatus();
            lblStatus.Text = Status.ToString();
            if (Status == Discovery.Scheduling.ScheduleStatus.STOPPED)
            {
                btnStart.Enabled = true;
                btnStop.Enabled = false;
            }
            else
            {
                btnStart.Enabled = false;
                btnStop.Enabled = true;
            }
        }

        private void BindData()
        {
            lblFreeThreads.Text = SchedulingProvider.Instance().GetFreeThreadCount().ToString();
            lblActiveThreads.Text = SchedulingProvider.Instance().GetActiveThreadCount().ToString();
            lblMaxThreads.Text = SchedulingProvider.Instance().GetMaxThreadCount().ToString();

            // Retrieve items in queue
            ArrayList arrScheduleQueue = SchedulingProvider.Instance().GetScheduleQueue();
            // See if any items in above queue
            if (arrScheduleQueue.Count == 0)
            {
                // No items
                pnlScheduleQueue.Visible = false;
            }
            else
            {
                // Items in queue, bind to data grid
                dgScheduleQueue.DataSource = arrScheduleQueue;
                dgScheduleQueue.DataBind();
            }

            ArrayList arrScheduleProcessing = SchedulingProvider.Instance().GetScheduleProcessing();
            if (arrScheduleProcessing.Count == 0)
            {
                pnlScheduleProcessing.Visible = false;
            }
            else
            {
                dgScheduleProcessing.DataSource = arrScheduleProcessing;
                dgScheduleProcessing.DataBind();
            }
            if (arrScheduleProcessing.Count == 0 && arrScheduleQueue.Count == 0)
            {
                //Skin.AddModuleMessage(this, "There are no tasks in the queue and no tasks are processing.", Skins.ModuleMessage.ModuleMessageType.YellowWarning);
            }
        }

        private void btnStart_Click(System.Object sender, System.EventArgs e)
        {
            SchedulingProvider.Instance().StartAndWaitForResponse();
            BindData();
            BindStatus();
        }

        private void btnStop_Click(System.Object sender, System.EventArgs e)
        {
            SchedulingProvider.Instance().Halt("Manually Stopped From Scheduler Status Page");
            BindData();
            BindStatus();
        }

        protected string GetOverdueText(double OverdueBy)
        {
            if (OverdueBy > 0)
            {
                return OverdueBy.ToString();
            }
            else
            {
                return "";
            }
        }

        #region " Web Form Designer Generated Code "
        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        #endregion 
    }
    
    public class ScheduleStatusSortRemainingTimeDescending : System.Collections.IComparer
    {
        public int Compare(Object x, Object y)
        {
            return ((LASPortal.Scheduling.ScheduleHistoryItem)x).RemainingTime.CompareTo(((LASPortal.Scheduling.ScheduleHistoryItem)y).RemainingTime);
        }
        // interface methods implemented by Compare
        int System.Collections.IComparer.Compare(Object x, Object y) { return Compare(x, y); }
    } 

}

