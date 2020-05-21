using System;
using System.Diagnostics;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Discovery.BusinessObjects;
using Discovery.BusinessObjects.Controllers;
using Discovery.Utility;
using RJS.Web.WebControl;

namespace Discovery.UI.Web.ReferenceData
{
    /*************************************************************************************************
   ** CLASS:	LogEntries
   **
   ** OVERVIEW:
   ** This page displays all Log Entries
   **
   ** MODIFICATION HISTORY:
   **
   ** Date:		Version:    Who:	Change:
   ** 20/7/06		1.0			PJN		Initial Version
   ************************************************************************************************/

    public partial class LogEntries : DiscoveryDataItemsPage
    {
        #region Properties

        #endregion

        #region Protected Methods

        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected override void Page_Load(object sender, EventArgs e)
        {
            ReadRule = "Admin: View Log Entries";
            CreateRule = "Admin: Edit Log Entries";
            DeleteRule = "Admin: Edit Log Entries";
            UpdateRule = "Admin: Edit Log Entries";

            //call base functionality
            base.Page_Load(sender, e);
            if (!IsPostBack) SetSearchFields();

            // Set the popups theme and script directory
            PopCalendar.JavaScriptCustomPath = "../../App_Themes/DiscoveryDefault/";

        }


        private void SetSearchFields()
        {
            txtTimestampFrom.Text = Request.QueryString["timeStampfrom"] == null ? "" : Request.QueryString["timeStampfrom"];
            txtTimestampTo.Text = Request.QueryString["timeStampto"] == null ? "" : Request.QueryString["timeStampto"];
            TextBoxMessageContents.Text = Request.QueryString["messageText"] == null ? "" : Request.QueryString["messageText"];
        }

        #endregion



        private void Page_PreRender(object sender, EventArgs e)
        {
            PageGridView.UrlReferrer = BuildQueryString();

        }

        private string BuildQueryString()
        {
            string queryString =
            string.Concat(
                "?Category=", DropDownListCategory.SelectedValue,
                "&Severity=", DropDownListSeverity.SelectedValue,
                "&opcoCode=", DropDownListOpco.SelectedValue,
                "&timeStampfrom=", txtTimestampFrom.Text,
                "&timeStampto=", txtTimestampTo.Text,
                "&errrortype=", DropDownListErrorType.SelectedValue,
                DropDownListAcknowledged.SelectedValue == "-1" ? "" : "&acknowledged=" + DropDownListAcknowledged.SelectedValue,
                "&messageText=", TextBoxMessageContents.Text
                );
            return queryString;
        }

        protected void LogEntriesObjectDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["searchCriteria"] =
                new LogEntryCriteria
                   (
                        null,
                       DropDownListCategory.SelectedValue == "" ? Null.NullString : DropDownListCategory.SelectedItem.Text,
                       DropDownListCategory.SelectedValue == "" ? Null.NullInteger : Convert.ToInt32(DropDownListCategory.SelectedValue),
                       DropDownListOpco.SelectedValue == "" ? Null.NullString : DropDownListOpco.SelectedValue,
                       DropDownListSeverity.SelectedValue == "" ? Null.NullString : DropDownListSeverity.SelectedValue,
                       txtTimestampFrom.Text == "" ? Null.NullDate : Convert.ToDateTime(txtTimestampFrom.Text),
                       txtTimestampTo.Text == "" ? Null.NullDate : Convert.ToDateTime(txtTimestampTo.Text),
                       DropDownListAcknowledged.SelectedValue == "" ? Null.NullBoolean : Convert.ToBoolean(Convert.ToInt32(DropDownListAcknowledged.SelectedValue)),
                       TextBoxMessageContents.Text,
                       DropDownListErrorType.SelectedValue == "" ? Null.NullString : DropDownListErrorType.SelectedValue
                   );
        }

        protected void DropDownListOpco_DataBound2(object sender, EventArgs e)
        {
            ((DropDownList)sender).SelectedValue = Request.QueryString["opcoCode"] == null ? "" : Request.QueryString["opcoCode"];
        }

        protected void DropDownListErrorType_DataBound(object sender, EventArgs e)
        {
            ((DropDownList)sender).SelectedValue = Request.QueryString["errrortype"] == null ? "" : Request.QueryString["errrortype"];
        }

        protected void DropDownListSeverity_DataBound(object sender, EventArgs e)
        {
            ((DropDownList)sender).SelectedValue = Request.QueryString["Severity"];
        }

        protected void DropDownListAcknowledged_DataBound(object sender, EventArgs e)
        {
            ((DropDownList)sender).SelectedValue = Request.QueryString["acknowledged"] == null ? "-1" : Request.QueryString["acknowledged"];
        }

        protected void ImageButtonSearch_Click(object sender, ImageClickEventArgs e)
        {
            GridView1.UrlReferrer = BuildQueryString();
            GridView1.DataBind();
        }

        protected void DropDownListCategory_DataBound(object sender, EventArgs e)
        {
            ((DropDownList)sender).SelectedValue = Request.QueryString["Category"] == null ? "-1" : Request.QueryString["Category"];
            BindErrorTypes();
        }

        protected void DropDownListCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindErrorTypes();
        }

        private void BindErrorTypes()
        {
            if (DropDownListCategory.SelectedValue != "")
            {
                DropDownListErrorType.DataSource =
                    ErrorTypeController.GetErrorTypes(DropDownListOpco.SelectedValue,
                                                      DropDownListCategory.SelectedItem.Text,
                                                      "");
                DropDownListErrorType.Items.Clear();
                DropDownListErrorType.Items.Add(new ListItem("-", ""));
                DropDownListErrorType.DataBind();
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                BusinessObjects.LogEntry logEntry = (BusinessObjects.LogEntry)e.Row.DataItem;
                if (logEntry != null)
                {
                    if (logEntry.RequiresAcknowledgement && !logEntry.Acknowledged)
                    {
                        e.Row.ForeColor = Color.Red;
                    }
                }
            }
        }
    }
}

