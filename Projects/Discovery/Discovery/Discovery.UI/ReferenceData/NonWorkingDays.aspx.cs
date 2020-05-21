using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Discovery.BusinessObjects.Controllers;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using RJS.Web.WebControl;

namespace Discovery.UI.Web.ReferenceData
{
    public partial class NonWorkingDays : DiscoveryDataItemsPage
    {
        private bool ddlWarehouseRefresh = false;

      
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected override void Page_Load(object sender, EventArgs e)
        {

            ReadRule = "Reference Data: View Non Working Days";
            CreateRule = "Reference Data: Edit Non Working Days";
         
            //call base class
            base.Page_Load(sender, e);
           
            
            // //this means that when the user clicks the textboes the calendars appear
            // txtStartDate.ReadOnly = true;
            //// txtStartDate.Attributes.Add("onclick", "PopCalShowCalendar('ctl00_ContentPlaceHolder_txtStartDate','ctl00_ContentPlaceHolder_PopCalendar1');");
            // txtEndDate.ReadOnly = true;
            // //txtEndDate.Attributes.Add("onclick", "PopCalShowCalendar('ctl00_ContentPlaceHolder_txtEndDate','ctl00_ContentPlaceHolder_PopCalendar1');");

            PopCalendar.JavaScriptCustomPath = "../App_Themes/DiscoveryDefault/";

            // We need to know when the page index changes
          //  grdNonWorkingDays.PageIndexChanged += new EventHandler(grdNonWorkingDays_PageIndexChanged);

            ddlWarehouseRefresh = false;

            if (!IsPostBack)
            {
               
                PopulateDates();
                ddlWarehouse.Enabled = false;
                Page.PreRender += new EventHandler(Page_PreRender);
            }
        }

        void Page_PreRender(object sender, EventArgs e)
        {
            PageGridView.UrlReferrer = BuildQueryString();
        }

        //void grdNonWorkingDays_PageIndexChanged(object sender, EventArgs e)
        //{
        //    //RedirectWithQuery();
        //}

        /// <summary>
        /// Populates the arguments from the query string if there is one.
        /// </summary>
        private void PopulateDates()
        {
            // Get the start date
            if (Request.QueryString["StartDate"] != null)
            {
                try
                {
                    txtStartDate.Text = Convert.ToDateTime(Request.QueryString["StartDate"]).ToShortDateString();
                }
                catch (Exception ex)
                {
                    txtStartDate.Text = DateTime.Today.ToShortDateString();
                }
            }
            else
            {
                txtStartDate.Text = DateTime.Today.ToShortDateString();
            }

            // Get the End Date
            if (Request.QueryString["EndDate"] != null)
            {
                try
                {
                    txtEndDate.Text = Convert.ToDateTime(Request.QueryString["EndDate"]).ToShortDateString();
                }
                catch (Exception ex)
                {
                    txtEndDate.Text = string.Empty;                
                }
            }
        }

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlWarehouse.Items.Clear();
            ddlWarehouse.Items.Add(new ListItem("All", "-1"));
            ddlWarehouseRefresh = true;
        }

    

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
//            RedirectWithQuery();
            PageGridView.UrlReferrer = BuildQueryString();
            PageGridView.DataBind();
        }

        private string BuildQueryString()
        {
            string queryString =
                string.Concat("?StartDate=", txtStartDate.Text,
                              "&EndDate=", txtEndDate.Text,
                              "&WarehouseId=",ddlWarehouse.SelectedValue,
                              "&RegionId=", ddlRegion.SelectedValue,
                              "&PageIndex=", grdNonWorkingDays.PageIndex.ToString(),
                              "&SortExpression=", grdNonWorkingDays.SortExpression,
                              "&SortDirection=", ((int)grdNonWorkingDays.SortDirection).ToString());
            return queryString;
            //Response.Redirect("NonWorkingDays.aspx" + queryString);
        }

        protected void ddlRegion_DataBound(object sender, EventArgs e)
        {
            if (Request.QueryString["RegionId"] != null)
            {
                ListItem itemToSelect = ddlRegion.Items.FindByValue(Request.QueryString["RegionId"].ToString());
                if (itemToSelect != null && ddlRegion.SelectedItem != null)
                {
                    ddlRegion.SelectedItem.Selected = false;
                }
                if (itemToSelect != null)
                {
                    itemToSelect.Selected = true;
                }
            }
        }

        protected void ddlWarehouse_DataBound(object sender, EventArgs e)
        {
            if (ddlRegion.SelectedItem.Text == "All")
            {
                ddlWarehouseRefresh = false;
            }

            if (!ddlWarehouseRefresh)
            {
                ddlWarehouse.Items.Clear();
                ddlWarehouse.Items.Add(new ListItem("All", "-1"));
                ddlWarehouseRefresh = true;
            }

            if (ddlRegion.SelectedItem.Text != "All")
            {
                if (Request.QueryString["WarehouseId"] != null)
                {
                    ListItem itemToSelect =
                        ddlWarehouse.Items.FindByValue(Request.QueryString["WarehouseId"].ToString());
                    if (itemToSelect != null && ddlWarehouse.SelectedItem != null)
                    {
                        ddlWarehouse.SelectedItem.Selected = false;
                    }
                    if (itemToSelect != null)
                    {
                        itemToSelect.Selected = true;
                    }
                }
            }
            // Enable the warehouse drop down
            ddlWarehouse.Enabled = (ddlWarehouse.Items.Count > 1);
        }


        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {
            bool returnStatus = false;
            BusinessObjects.NonWorkingDay nonWorkingDay = new BusinessObjects.NonWorkingDay();

            foreach (GridViewRow CurrRow in grdNonWorkingDays.Rows)
            {
                if (GetControl<CheckBox>("chkDelete", CurrRow).Checked)
                {
                    try
                    {
                        nonWorkingDay.Id = (int) grdNonWorkingDays.DataKeys[CurrRow.RowIndex].Value;
                        returnStatus = NonWorkingDayController.DeleteNonWorkingDay(nonWorkingDay);
                    }
                    catch (Exception ex)
                    {
                        if (ExceptionPolicy.HandleException(ex, "User Interface")) DisplayMessage("Failed to delete.");
                    }
                    if (!returnStatus)
                    {
                        DisplayMessage("Failure to delete");
                    }
                }
            }
            PageGridView.DataBind();
        }
    }
}