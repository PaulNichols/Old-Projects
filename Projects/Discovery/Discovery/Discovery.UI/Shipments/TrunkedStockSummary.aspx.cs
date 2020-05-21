using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Discovery.BusinessObjects;
using RJS.Web.WebControl;
/*************************************************************************************************
   ** CLASS:	TrunkedStockSummary
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
    public partial class TrunkedStockSummary : DiscoveryDataItemsPage
    {
        #region Properties

        #endregion

        #region Protected Methods

        #endregion

        #region Events

     

        #endregion

        protected override void Page_Load(object sender, EventArgs e)
        {

            ReadRule = "Shipment: View Trunked Stock Summary";
           
        
            base.Page_Load(sender, e);

            //set the popups theme and script directory
            PopCalendar.JavaScriptCustomPath = "../App_Themes/DiscoveryDefault/";

            if (!IsPostBack)
            {
                //set the date to tommorow by default or to the argument on the query string
                TextBoxRequiredDate.Text = DateTime.Today.AddDays(1).ToShortDateString();
                if (Request.QueryString["RequiredDate"] != null)
                {
                    DateTime queryStringDate;
                    DateTime.TryParse(Request.QueryString["RequiredDate"].ToString(), out queryStringDate);
                    if (queryStringDate != DateTime.MinValue && queryStringDate.Year > 1900)
                    {
                        TextBoxRequiredDate.Text = queryStringDate.ToShortDateString();
                    }
                }
              
            }
        }

        protected void DropDownListDeliveryLocation_DataBound(object sender, EventArgs e)
        {
            //set up the delivery ware house to either default to the current users profile 
            //or use the item in the query string
            

            if (Request.QueryString["DeliveryWarehouseId"] != null)
            {
                int warehouseId = Profile.WarehouseId;
                Int32.TryParse(Request.QueryString["DeliveryWarehouseId"].ToString(), out warehouseId);
                SetSelectedValue(sender, warehouseId.ToString());
            }
            else
            {
                SetUserDefaultWarehouse(sender as ListControl, Profile);
            }
            
        }

        private static void SetSelectedValue(object sender, string valueToFind)
        {
            ListItem defaultItem = ((DropDownList) sender).Items.FindByValue(valueToFind);
            if (defaultItem != null)
            {
                ((DropDownList) sender).SelectedItem.Selected = false;
                defaultItem.Selected = true;
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ShipmentTrunkedStockSummary trunkedSummary = (ShipmentTrunkedStockSummary) e.Row.DataItem;
                HyperLink routeCodeHyperLink = GetControl<HyperLink>("HyperLinkRouteCode", e.Row);

                //see if the route code was the same on the last row as we want to blank out all but the first appearence of a route code to create a kind of grouping
                if (e.Row.DataItemIndex != 0 && ((HyperLink)((GridView)sender).Rows[e.Row.DataItemIndex - 1].FindControl("HyperLinkRouteCode")).Text== trunkedSummary.RouteCodeDescription)
                {
                    routeCodeHyperLink.Text = "";
                }
                else
                {
                    
                    //set up the URL to navigate from the route code to the TDCShipment screen
                    routeCodeHyperLink.NavigateUrl = string.Format(
                        "~/Shipments/TDCShipments.aspx?c_RouteCode={0}&c_DeliveryWarehouseCode={1}&c_RequiredDateFrom={2}&c_RequiredDateTo={2}&c_StockWarehouseCode={3}",
                        Server.UrlEncode(trunkedSummary.RouteCode), Server.UrlEncode(trunkedSummary.DeliveryWarehouseCode), Server.UrlEncode(TextBoxRequiredDate.Text), Server.UrlEncode(trunkedSummary.StockWarehouseCode));
                }
                
                
            }
        }

        protected void DropDownListRouteCode_DataBound(object sender, EventArgs e)
        {
            //set up the delivery ware house to either default to the current users profile 
            //or use the item in the query string
            int routeCode = -1;

            ((DropDownList)sender).Items.Add(new ListItem("All",(-1).ToString()));

            if (Request.QueryString["RouteCode"] != null)
            {
                Int32.TryParse(Request.QueryString["RouteCode"].ToString(), out routeCode);
              
            }

            SetSelectedValue(sender, routeCode.ToString());

            if (routeCode!=-1) GridView1.DataBind();

        }


    }
}