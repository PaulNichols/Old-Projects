using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Discovery.BusinessObjects.Controllers;
using RJS.Web.WebControl;

namespace Discovery.UI.Web.Admin
{
    public partial class MessageAudits : DiscoveryDataItemsPage
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

            // //this means that when the user clicks the textboes the calendars appear
            // txtReceivedDateFrom.ReadOnly = true;
            //// txtReceivedDateFrom.Attributes.Add("onclick", "PopCalShowCalendar('ctl00_ContentPlaceHolder_txtReceivedDateFrom','ctl00_ContentPlaceHolder_PopCalendar1');");
            // txtReceivedDateTo.ReadOnly = true;
            // //txtReceivedDateTo.Attributes.Add("onclick", "PopCalShowCalendar('ctl00_ContentPlaceHolder_txtReceivedDateTo','ctl00_ContentPlaceHolder_PopCalendar1');");

            PopCalendar.JavaScriptCustomPath = "../App_Themes/DiscoveryDefault/";

            // We need to know when the page index changes
          //  grdMessageAudits.PageIndexChanged += new EventHandler(grdMessageAudits_PageIndexChanged);

            if (!IsPostBack)
            {
                // Display correct vales, etc
                UpdateDisplay();
                Page.PreRender += new EventHandler(Page_PreRender);
            }
        }

        void Page_PreRender(object sender, EventArgs e)
        {
            PageGridView.UrlReferrer = BuildQueryString();
        }

        //void grdMessageAudits_PageIndexChanged(object sender, EventArgs e)
        //{
        //    //RedirectWithQuery();
        //}

        /// <summary>
        /// Populates the arguments from the query string if there is one.
        /// </summary>
        private void UpdateDisplay()
        {
            // Get the from date
            if (!string.IsNullOrEmpty(Request.QueryString["DateFrom"]))
            {
                try
                {
                    txtReceivedDateFrom.Text = Convert.ToDateTime(Request.QueryString["DateFrom"]).ToShortDateString();
                }
                catch (Exception ex)
                {
                    txtReceivedDateFrom.Text = string.Empty;
                }
            }
            else
            {
                //txtReceivedDateFrom.Text = DateTime.Today.ToShortDateString();
                txtReceivedDateFrom.Text = string.Empty;
            }

            // Get the to date
            if (!string.IsNullOrEmpty(Request.QueryString["DateTo"]))
            {
                try
                {
                    txtReceivedDateTo.Text = Convert.ToDateTime(Request.QueryString["DateTo"]).ToShortDateString();
                }
                catch (Exception ex)
                {
                    txtReceivedDateTo.Text = string.Empty;                
                }
            }
            else
            {
                //txtReceivedDateTo.Text = DateTime.Today.ToShortDateString();
                txtReceivedDateTo.Text = string.Empty;
            }


            // Source system
            if (Request.QueryString["SourceSystem"] != null)
            {
                ddlSourceSystem.SelectedValue = Request.QueryString["SourceSystem"].ToString();
            }

            // Destination system
            if (Request.QueryString["DestinationSystem"] != null)
            {
                ddlDestinationSystem.SelectedValue = Request.QueryString["DestinationSystem"].ToString();
            }

            // Type
            if (Request.QueryString["Type"] != null)
            {
                ddlType.SelectedValue = Request.QueryString["Type"].ToString();
            }

            if (Request.QueryString["Message"] != null)
            {
                txtMessage.Text = Request.QueryString["Message"].ToString();
            }
        }


        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            PageGridView.UrlReferrer = BuildQueryString();
            PageGridView.DataBind();
        }

        private string BuildQueryString()
        {
            string queryString =
                string.Concat("?DateFrom=", txtReceivedDateFrom.Text,
                              "&DateTo=", txtReceivedDateTo.Text,
                              "&DestinationSystem=", ddlDestinationSystem.SelectedValue,
                              "&SourceSystem=", ddlSourceSystem.SelectedValue,
                              "&Type=", ddlType.SelectedValue,
                              "&message=", txtMessage.Text,
                              "&PageIndex=", grdMessageAudits.PageIndex.ToString(),
                              "&SortExpression=", grdMessageAudits.SortExpression,
                              "&SortDirection=", ((int)grdMessageAudits.SortDirection).ToString());

            return queryString;
        }


    }
}