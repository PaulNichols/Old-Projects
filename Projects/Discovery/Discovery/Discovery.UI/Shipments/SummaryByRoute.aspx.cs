using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Discovery.BusinessObjects;
using RJS.Web.WebControl;
/*************************************************************************************************
   ** CLASS:	SummaryByRoute
   **
   ** OVERVIEW:
   ** This page displays all shipments, summarised by route
   **
   ** MODIFICATION HISTORY:
   **
   ** Date:		Version:    Who:	Change:
   ** 3/8/06	1.0			PJN		Initial Version
   ************************************************************************************************/

namespace Discovery.UI.Web.Shipments
{
    public partial class SummaryByRoute : DiscoveryDataItemsPage
    {
        private decimal exBranchTotal = 0;
        private decimal trunkedTotal = 0;
        private decimal weightTotal = 0;
        private decimal volumeTotal = 0;

      

        #region Properties

        #endregion

        #region Protected Methods

        #endregion

        #region Events

        protected void ImageButtonSearch_Click(object sender, ImageClickEventArgs e)
        {
      
            PageGridView.DataBind();
        }

        #endregion

        protected override void Page_Load(object sender, EventArgs e)
        {
            ReadRule = "Shipment: Summary By Route";
           
            base.Page_Load(sender, e);
        
            //Set the popups theme and script directory
            PopCalendar.JavaScriptCustomPath = "../App_Themes/DiscoveryDefault/";

            if (!IsPostBack)
            {
                //default the date to tomorrow
                TextBoxRequiredDate.Text = DateTime.Today.AddDays(1).ToShortDateString();
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                //set the totals
                GetControl<Literal>("LiteralExBranchTotal", e.Row).Text = exBranchTotal.ToString("#0.00");
                GetControl<Literal>("LiteralVolumeTotal", e.Row).Text = volumeTotal.ToString("#0.00");
                GetControl<Literal>("LiteralWeightTotal", e.Row).Text = weightTotal.ToString("#0.00");
                GetControl<Literal>("LiteralTrunked", e.Row).Text = trunkedTotal.ToString("#0.00");
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ShipmentSummaryByRoute summaryByRoute = ((ShipmentSummaryByRoute)e.Row.DataItem);

                //set up the URL to navigate from the route code to the TDCShipment screen
                GetControl<HyperLink>("HyperLinkRouteCode", e.Row).NavigateUrl =
                    string.Format(
                        "~/Shipments/TDCShipments.aspx?c_RouteCode={0}&c_DeliveryWarehouseCode={1}&c_RequiredDateFrom={2}&c_RequiredDateTo={2}",
                        summaryByRoute.RouteCode, summaryByRoute.DeliveryWarehouseCode, TextBoxRequiredDate.Text);

                if (summaryByRoute.Trunked > 0)
                {
                    //if there are trunked items then set up the URL to navigate from the route code to the trunked stock summay screen
                    GetControl<HyperLink>("HyperLinkTrunked", e.Row).NavigateUrl =
                        string.Format(
                            "~/Shipments/TrunkedStockSummary.aspx?RouteCode={0}&DeliveryWarehouseId={1}&RequiredDate={2}",
                            summaryByRoute.RouteId, DropDownListDeliveryLocation.SelectedValue,
                            TextBoxRequiredDate.Text);
                }

                exBranchTotal += summaryByRoute.ExBranch;
                trunkedTotal += summaryByRoute.Trunked;
                weightTotal += summaryByRoute.Weight;
                volumeTotal += summaryByRoute.Volume;
            }
        }


        protected void DropDownListDeliveryLocation_DataBound(object sender, EventArgs e)
        {
            SetUserDefaultWarehouse(DropDownListDeliveryLocation,Profile);
        }
}
}