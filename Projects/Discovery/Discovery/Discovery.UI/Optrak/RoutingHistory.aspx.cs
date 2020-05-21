using System;
using System.Web.UI.WebControls;
using Discovery.BusinessObjects.Controllers;
using Discovery.UI.Web.UserControls;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Discovery.UI.Web.Routing
{
    public partial class RoutingHistory : DiscoveryPage
    {
       
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected override void Page_Load(object sender, EventArgs e)
        {
            ReadRule = "Optrak: View Routing History";
            CreateRule = "Optrak: Edit Routing History";
            DeleteRule = "Optrak: Edit Routing History";
            UpdateRule = "Optrak: Edit Routing History";

            //call base class
            base.Page_Load(sender, e);
        }

        protected override void OnInit(EventArgs e)
        {
            // Call base
            base.OnInit(e);
            
            TDCShipmentsUserControl.Redirecting += new TDCShipmentsUserControlRedirect(TDCShipmentsUserControl_Redirecting);
        }

        void TDCShipmentsUserControl_Redirecting(ref TDCRedirectEventArg redirectEventArg)
        {
           // redirectEventArg.QueryParams = ShipmentCriteria.Criteria.GenerateQueryString();
        }
        
        protected void GridViewHistory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Reset")
            {
                try
                {
                    if (RoutingController.ResetLock(Convert.ToInt32(e.CommandArgument), User.Identity.Name))
                    {
                        GridViewHistory.DataBind();
                    }
                    else
                    {
                        DisplayMessage("Failed to Reset Locks.",DiscoveryMessageType.Error);
                    }
                }
                catch (Exception e1)
                {
                    if (ExceptionPolicy.HandleException(e1, "User Interface")) DisplayMessage("Failed to Reset Locks.");
                }
            }
            else if (e.CommandName == "Details")
            {
                RoutingHistoryId = Convert.ToInt32(e.CommandArgument);
                MultiViewHistory.ActiveViewIndex = 1;
                
                // Reset the page number
                TDCShipmentsUserControl.PageIndex = 0;

                // Search was clicked, refresh shipments
                TDCShipmentsUserControl.DataBind();
            }
        }
        
        protected void GridViewHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                BusinessObjects.RoutingHistory.StatusEnum status =
                    ((BusinessObjects.RoutingHistory)e.Row.DataItem).Status;
                LinkButton resetLinkButton = GetControl<LinkButton>("LinkButtonReset", e.Row);
                resetLinkButton.Enabled = (status == BusinessObjects.RoutingHistory.StatusEnum.InProcess ||
                                           status == BusinessObjects.RoutingHistory.StatusEnum.Sent);

                Label labelStatus = GetControl<Label>("LabelStatus", e.Row);
                if (status==BusinessObjects.RoutingHistory.StatusEnum.InProcess)
                {
                    labelStatus.Text = "In Process";
                }
                 
            }
        }
        
        /// <summary>
        /// Gets the routing history id. This is a unique id that groups shipments together for the current routing process
        /// </summary>
        /// <value>The routing history id.</value>
        private int? RoutingHistoryId
        {
            get
            {
                if (ViewState["RoutingHistoryId"] != null)
                {
                    return Convert.ToInt32(ViewState["RoutingHistoryId"]);
                }
                else
                {
                    return null;
                }
            }
            set
            {
                ViewState.Add("RoutingHistoryId", value);
            }
        }



        protected void dataSourceShipments_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            // Assign our shipment criteria, query has not ran yet
            e.InputParameters["routingHistoryId"] = RoutingHistoryId.Value;

            // Page number and max rows
            e.Arguments.MaximumRows = TDCShipmentsUserControl.PageSize;
            e.Arguments.StartRowIndex = TDCShipmentsUserControl.PageIndex;

            // Get the total number of rows
            try
            {
                TDCShipmentsUserControl.TotalRows = RoutingController.GetShipmentsByRoutingHistoryIdCount(RoutingHistoryId.Value,false);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "User Interface")) DisplayMessage(ex);
            }
            // Sorting
            e.Arguments.SortExpression = string.Concat(TDCShipmentsUserControl.SortExpression, " ", TDCShipmentsUserControl.SortDirection.ToString());
        }
        
        protected void LinkButtonBack_Click(object sender, EventArgs e)
        {
            MultiViewHistory.ActiveViewIndex = 0;
        }
}
}