/*************************************************************************************************
 ** FILE:	TDCShipments.aspx.cs
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
    public partial class TDCShipments : DiscoveryDataItemsPage
    {      
        
        protected override void Page_Load(object sender, EventArgs e)
        {
            CreateRule = "Shipment: Edit TDC Shipment Detail";
            ReadRule = "Shipment: TDC Shipment Search";
            UpdateRule = "Shipment: Edit TDC Shipment Detail";
            DeleteRule = "Shipment: Edit TDC Shipment Detail";

            // Call base
            base.Page_Load(sender, e);

            // Ajax bug
            EnsureChildControls();
        }
        
        protected override void OnInit(EventArgs e)
        {
            // Call base
            base.OnInit(e);

            // Wire up the criteria search event
            ShipmentCriteriaUserControl.SearchClicked += new ShipmentCriteriaControl.SearchClickedEventHandler(ShipmentCriteriaUserControl_SearchClicked);

            // Wire up the redirecting event from the shipments
            TDCShipmentsUserControl.Redirecting += new TDCShipmentsUserControlRedirect(TDCShipmentsUserControl_Redirecting);
        }

        void TDCShipmentsUserControl_Redirecting(ref TDCRedirectEventArg redirectEventArg)
        {
            redirectEventArg.QueryParams = ShipmentCriteriaUserControl.Criteria.GenerateQueryString();
        }

        void ShipmentCriteriaUserControl_SearchClicked(object sender, EventArgs e)
        {
            // Reset the page number
            TDCShipmentsUserControl.PageIndex = 0;

            // Search was clicked, refresh shipments
            TDCShipmentsUserControl.DataBind();
        }

        protected void dataSourceShipments_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            // Assign our shipment criteria, query has not ran yet
            e.InputParameters["criteria"] = ShipmentCriteriaUserControl.Criteria;

            // Page number and max rows
            e.Arguments.MaximumRows = TDCShipmentsUserControl.PageSize;
            e.Arguments.StartRowIndex = TDCShipmentsUserControl.PageIndex;

            // Get the total number of rows
            TDCShipmentsUserControl.TotalRows = TDCShipmentController.GetShipmentsCount(ShipmentCriteriaUserControl.Criteria);

            // Sorting
            e.Arguments.SortExpression = string.Concat(TDCShipmentsUserControl.SortExpression, " ", TDCShipmentsUserControl.SortDirection.ToString());
        }


    }
}
