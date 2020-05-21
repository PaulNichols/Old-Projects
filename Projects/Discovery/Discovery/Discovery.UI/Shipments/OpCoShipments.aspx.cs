/*************************************************************************************************
 ** FILE:	OpCoShipments.aspx.cs
 ** DATE:	30/05/2006
 ** AUTHOR:	Lee Spring
 **
 **
 ** OVERVIEW:
 **
 ** MODIFICATION HISTORY:
 **
 ** Date:		Version:	Who:	Change:
 ** 30/5/06		1.0		    LAS	    Initial Version
 ************************************************************************************************/
using System;
using System.Web.UI.WebControls;
using Discovery.BusinessObjects.Controllers;
using Discovery.UI.Web.UserControls;

namespace Discovery.UI.Web.Shipments
{
    public partial class OpCoShipments : DiscoveryDataItemsPage
    {
        protected override void OnInit(EventArgs e)
        {
            // Call base
            base.OnInit(e);

            // Wire up the criteria search event
            ShipmentCriteriaUserControl.SearchClicked += new ShipmentCriteriaControl.SearchClickedEventHandler(ShipmentCriteriaUserControl_SearchClicked);

            // Wire up the redirecting event from the shipments
            OpCoShipmentsUserControl.Redirecting += new OpCoShipmentsUserControlRedirect(OpCoShipmentsUserControl_Redirecting);
        }

        void OpCoShipmentsUserControl_Redirecting(ref OpCoRedirectEventArg redirectEventArg)
        {
            redirectEventArg.QueryParams = ShipmentCriteriaUserControl.Criteria.GenerateQueryString();
        }

        void ShipmentCriteriaUserControl_SearchClicked(object sender, EventArgs e)
        {
            // Reset the page number
            OpCoShipmentsUserControl.PageIndex = 0;

            // Search was clicked, refresh shipments
            OpCoShipmentsUserControl.DataBind();
        }

        protected void dataSourceShipments_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            // Assign our shipment criteria, query has not ran yet
            e.InputParameters["criteria"] = ShipmentCriteriaUserControl.Criteria;

            // Page number and max rows
            e.Arguments.MaximumRows = OpCoShipmentsUserControl.PageSize;
            e.Arguments.StartRowIndex = OpCoShipmentsUserControl.PageIndex;

            // Get the total number of rows
            OpCoShipmentsUserControl.TotalRows = OpCoShipmentController.GetShipmentsCount(ShipmentCriteriaUserControl.Criteria);

            // Sorting
            e.Arguments.SortExpression = string.Concat(OpCoShipmentsUserControl.SortExpression, " ", OpCoShipmentsUserControl.SortDirection.ToString());
        }
        
        protected override void Page_Load(object sender, EventArgs e)
        {
            CreateRule = "Shipment: Edit OpCo Shipment Detail";
            ReadRule = "Shipment: OpCo Shipment Search";
           
            base.Page_Load(sender, e);
        }
        
    
    }
}
