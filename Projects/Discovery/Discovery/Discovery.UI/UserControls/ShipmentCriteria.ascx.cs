/*************************************************************************************************
 ** FILE:	ShipmentCriteria.aspx.cs
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
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using RJS.Web.WebControl;
using Discovery.BusinessObjects.Controllers;
using Discovery.BusinessObjects;
using Discovery.Utility;

namespace Discovery.UI.Web.UserControls
{
    public partial class ShipmentCriteriaControl : System.Web.UI.UserControl
    {
        public enum ShipmentTypeEnum
        {
            OpCo = 0,
            TDC = 1
        }

        // The current search criteria
        private ShipmentCriteria criteria;
        private ShipmentCriteria tmpCriteria = null;
        private ShipmentTypeEnum shipmentType = ShipmentTypeEnum.OpCo;

        public ShipmentCriteria Criteria
        {
            get
            {
                // We don't want to keep updating the criteria from the ui, it's expensive
                if (null == tmpCriteria)
                {
                    // Populate the criteria object based on the ui values
                    PopulateCriteria();
                    
                    // Return the criteria
                    tmpCriteria = criteria;
                    
                    // Done
                    return tmpCriteria;
                }
                else
                {
                    // Done
                    return tmpCriteria;
                }
            }
        }

        public ShipmentTypeEnum ShipmentType
        {
            get
            {
                return shipmentType;
            }
            set
            {
                // Store the shipment type we're a criteria for
                shipmentType = value;
            }
        }

        private Unit width = new Unit("950px");
        public Unit Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }

        // Search clicked delegate
        public delegate void SearchClickedEventHandler(object sender, EventArgs e);

        // Search clicked event
        public event SearchClickedEventHandler SearchClicked;

        public ShipmentCriteriaControl()
        {
            // Seed shipment criteria
            criteria = new ShipmentCriteria();
        }

        // Search clicked method
        protected virtual void OnSearchClicked(EventArgs e)
        {
            // See if we have a search subscriber
            if (SearchClicked != null)
            {
                // Call event subscribers
                SearchClicked(this, e);
            }
            collapsiblePanelExtenderCriteria.Collapsed = true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Can the user search?
            btnSearch.Visible=(shipmentType == ShipmentTypeEnum.TDC && DiscoveryPage.HasRule("Shipment: TDC Shipment Search",Context.User)) ||
                                (shipmentType == ShipmentTypeEnum.OpCo && DiscoveryPage.HasRule("Shipment: OpCo Shipment Search",Context.User));

            // Set the popups theme and script directory
            PopCalendar.JavaScriptCustomPath = "../App_Themes/DiscoveryDefault/";

            // Set control width
            panelCriteria.Width = Width;

            // Display the tdc shipment info
            rowTDC1.Visible = (shipmentType == ShipmentTypeEnum.TDC);

            // We don't postback for delivery warehouse
            ddlDeliveryWarehouse.AutoPostBack = (shipmentType == ShipmentTypeEnum.TDC);

            // See if first time here
            if (!Page.IsPostBack)
            {
                // Load any values from the query string for the critera
                criteria.ParseQueryString(Request.QueryString);

                // Populate the shipment status codes
                PopulateStatusCodes();

                // Bind the various drop-downs, etc
                if (shipmentType == ShipmentTypeEnum.TDC)
                {
                    // Populate Sales Locations
                    PopulateTDCSalesLocations("");

                    // Populate warehouses
                    PopulateTDCWarehouses();

                    // Populate routes
                    PopulateTDCRoutes();

                    // Populate the trips
                    PopulateTrips();

                    // Populate shipment type
                    PopulateShipmentTypes();

                    // Populate transaction type
                    PopulateTDCTransactionTypes();

                    // Populate transaction sub types
                    PopulateTDCTransactionSubTypes();
                }
                else
                {
                    // Populate Sales Locations
                    PopulateOpCoSalesLocations("");

                    // Populate warehouses
                    PopulateOpCoWarehouses();

                    // Populate routes
                    PopulateOpCoRoutes();

                    // Populate transaction type
                    PopulateOpCoTransactionTypes();
                }

                // Populate the display from the criteria
                PopulateDisplay();
            }
        }

        private void PopulateTDCTransactionSubTypes()
        {
            // Populate tdc transaction types
            ddlTransactionSubType.DataSource = TransactionSubTypeController.GetTransactionSubTypes();
            ddlTransactionSubType.DataBind();
        }

        private void PopulateTDCTransactionTypes()
        {
            // Populate tdc transaction types
            ddlTransactionType.DataSource = TransactionTypeController.GetTransactionTypes();
            ddlTransactionType.DataBind();
        }
        
        private void PopulateOpCoTransactionTypes()
        {
            // Populate tdc transaction types
            ddlTransactionType.DataSource = TransactionTypeController.GetOpCoTransactionTypes();
            ddlTransactionType.DataBind();
        }

        private void PopulateShipmentTypes()
        {
            // Populate shipment types
            ddlType.DataSource = TDCShipmentController.GetShipmentTypes();
            ddlType.DataBind();
        }
        
        private void PopulateTDCRoutes()
        {
            // Populate the route codes
            ddlRouteCode.DataSource = RouteController.GetRoutes();
            ddlRouteCode.DataBind();
        }

        private void PopulateOpCoRoutes()
        {
            // Populate the route codes
            ddlRouteCode.DataSource = RouteController.GetOpCoRoutes();
            ddlRouteCode.DataBind();
        }
        
        private void PopulateOpCoWarehouses()
        {
            // Populate the stock warehouse from the opco shipment table
            ddlStockWarehouse.DataSource = WarehouseController.GetOpCoStockWarehouseCodes("");
            ddlStockWarehouse.DataBind();

             //Populate the delivery warehouse from the opco shipment table
            //ddlDeliveryWarehouse.DataSource = WarehouseController.GetOpCoDeliveryWarehouseCodes("");
            //ddlDeliveryWarehouse.DataBind();
            PopulateOpCoDeliveryWarehouses("");
        }

        private void PopulateTDCWarehouses()
        {
            // Populate the stock warehouse from warehouse table
            List<Warehouse> allWarehouses = WarehouseController.GetWarehouses(false);
            ddlStockWarehouse.DataSource = allWarehouses;
            ddlStockWarehouse.DataBind();

            // Populate the delivery warehouse  from warehouse table
            //ddlDeliveryWarehouse.DataSource = allWarehouses;
            //ddlDeliveryWarehouse.DataBind();
            PopulateTDCDeliveryWarehouses("");
        }

        private void PopulateOpCoDeliveryWarehouses(string opCo)
        {
            // Populate the delivery warehouses for OpCo Shipment option
            ddlDeliveryWarehouse.DataSource = WarehouseController.GetOpCoDeliveryWarehouseCodes(opCo);
            ddlDeliveryWarehouse.Items.Clear();
            ddlDeliveryWarehouse.Items.Add(new ListItem("All", "-1"));
            ddlDeliveryWarehouse.DataBind();
        }

        private void PopulateTDCDeliveryWarehouses(string opCo)
        {
            // Populate the delivery warehouses for TDC Shipment option
            ddlDeliveryWarehouse.DataSource = WarehouseController.GetTDCDeliveryWarehouseCodes(opCo);
            ddlDeliveryWarehouse.Items.Clear();
            ddlDeliveryWarehouse.Items.Add(new ListItem("All", "-1"));
            ddlDeliveryWarehouse.DataBind();
        }

        private void PopulateOpCoSalesLocations(string opCo)
        {
            // Populate the Sales Locations for OpCo Shipment option
            ddlSalesBranchCode.DataSource = SalesLocationController.GetOpCoSalesLocations(opCo);
            ddlSalesBranchCode.Items.Clear();
            ddlSalesBranchCode.Items.Add(new ListItem("All", "-1"));
            ddlSalesBranchCode.DataBind();
        }

        private void PopulateTDCSalesLocations(string opCo)
        {
            // Populate the Sales Locations for TDC Shipment option
            ddlSalesBranchCode.DataSource = SalesLocationController.GetTDCSalesLocations(opCo);
            ddlSalesBranchCode.Items.Clear();
            ddlSalesBranchCode.Items.Add(new ListItem("All", "-1"));
            ddlSalesBranchCode.DataBind();
        }

        private void PopulateTDCRoutes(string warehouseCode)
        {
            // Populate the Routes for TDC Shipment option
            ddlRouteCode.DataSource = RouteController.GetTDCRoutesByWarehouseCode(warehouseCode);
            ddlRouteCode.Items.Clear();
            ddlRouteCode.Items.Add(new ListItem("All", "-1"));
            ddlRouteCode.DataBind();
        }

        private void PopulateDisplay()
        {
            // OpCoId
            if (criteria.OpCoCode != "")
            {
                ddlOpCo.Text = criteria.OpCoCode.ToString();
            }
            else
            {
                try { ddlOpCo.Items.FindByText(criteria.OpCoCode).Selected = true; }
                catch { ddlOpCo.SelectedIndex = -1; }
            }
            // Shipment number
            txtOrderNumber.Text = criteria.ShipmentNumber;
            // Customer number
            txtCustomerNumber.Text = criteria.CustomerAccountNumber;
            // Customer name
            txtCustomerName.Text = criteria.CustomerName;
            //Shipment Status
            try { ddlStatus.Items.FindByValue(criteria.ShipmentStatus.ToString()).Selected = true; }
            catch { ddlStatus.SelectedIndex = -1; }
            //Sales Branch Code
            try { ddlSalesBranchCode.Items.FindByValue(criteria.SalesBranchCode).Selected = true; }
            catch { ddlSalesBranchCode.SelectedIndex = -1; }
            //Transaction Type 
            try { ddlTransactionType.Items.FindByValue(criteria.TransactionType).Selected = true; }
            catch { ddlTransactionType.SelectedIndex = -1; }
            //Transaction Sub Type 
            try { ddlTransactionSubType.Items.FindByValue(criteria.TransactionSubType).Selected = true; }
            catch { ddlTransactionSubType.SelectedIndex = -1; }
            //Route Code
            try { ddlRouteCode.Items.FindByValue(criteria.RouteCode).Selected = true; }
            catch { ddlRouteCode.SelectedIndex = -1; }
            //Route Trip
            try { ddlTrip.Items.FindByValue(criteria.RouteTrip).Selected = true; }
            catch { ddlTrip.SelectedIndex = -1; }
            //Route Drop
            try { ddlDrop.Items.FindByValue(criteria.RouteDrop.ToString()).Selected = true; }
            catch { ddlDrop.SelectedIndex = -1; }
            //Stock Warehouse Code
            try { ddlStockWarehouse.Items.FindByValue(criteria.StockWarehouseCode).Selected = true; }
            catch { ddlStockWarehouse.SelectedIndex = -1; }
            //Delivery Warehouse Code
            try { ddlDeliveryWarehouse.Items.FindByValue(criteria.DeliveryWarehouseCode).Selected = true; }
            catch { ddlDeliveryWarehouse.SelectedIndex = -1; }
            //Required Date From
            txtRequiredFrom.Text = ((DateTime)Null.GetNull(criteria.RequiredDateFrom, DateTime.Now)).ToString("d");
            //Required Date To
            txtRequiredTo.Text = ((DateTime)Null.GetNull(criteria.RequiredDateTo, DateTime.Now.AddDays(1))).ToString("d");
            // Shipment type
            try { ddlType.Items.FindByValue(criteria.Type.ToString()).Selected = true; }
            catch { ddlType.SelectedIndex = -1; }
        }

        private void PopulateCriteria()
        {
            // OpCoId
            criteria.OpCoCode = ("-1" == ddlOpCo.SelectedValue) ? Null.NullString : ddlOpCo.SelectedItem.Value;
            // Shipment number
            criteria.ShipmentNumber = Null.GetNull(txtOrderNumber.Text, Null.NullString) as string;
            // Customer number
            criteria.CustomerAccountNumber = Null.GetNull(txtCustomerNumber.Text, Null.NullString) as string;
            // Customer name
            criteria.CustomerName = Null.GetNull(txtCustomerName.Text, Null.NullString) as string;
            //Shipment Status
            criteria.ShipmentStatus = ("-1" == ddlStatus.SelectedValue) ? Null.NullInteger : (int)Enum.Parse(typeof(Shipment.StatusEnum), ddlStatus.SelectedValue);
            //Sales Branch Code
            criteria.SalesBranchCode = ("-1" == ddlSalesBranchCode.SelectedItem.Value) ? Null.NullString : ddlSalesBranchCode.SelectedItem.Value;
            //Transaction Type 
            criteria.TransactionType = ("-1" == ddlTransactionType.SelectedValue) ? Null.NullString : ddlTransactionType.SelectedValue;
            // Transaction Sub Type
            criteria.TransactionSubType = ("-1" == ddlTransactionSubType.SelectedValue) ? Null.NullString : ddlTransactionSubType.SelectedValue;
            //Route Code
            criteria.RouteCode = ("-1" == ddlRouteCode.SelectedValue) ? Null.NullString : ddlRouteCode.SelectedValue;
            //Route Trip
            criteria.RouteTrip = ("-1" == ddlTrip.SelectedValue) ? Null.NullString : ddlTrip.SelectedValue;
            //Route Drop
            criteria.RouteDrop = ("-1" == ddlDrop.SelectedValue) ? Null.NullInteger : Convert.ToInt32(ddlDrop.SelectedValue);
            //Stock Warehouse Code
            criteria.StockWarehouseCode = ("-1" == ddlStockWarehouse.SelectedItem.Value) ? Null.NullString : ddlStockWarehouse.SelectedItem.Value;
            //Delivery Warehouse Code
            criteria.DeliveryWarehouseCode = ("-1" == ddlDeliveryWarehouse.SelectedItem.Value) ? Null.NullString : ddlDeliveryWarehouse.SelectedItem.Value;
            //Required Date From
            criteria.RequiredDateFrom = ("" == txtRequiredFrom.Text) ? Null.NullDate : DateTime.Parse(txtRequiredFrom.Text);
            //Required Date To
            criteria.RequiredDateTo = ("" == txtRequiredTo.Text) ? Null.NullDate : DateTime.Parse(txtRequiredTo.Text);
            // Shipment Type
            criteria.Type = ("-1" == ddlType.SelectedValue) ? Null.NullInteger : Convert.ToInt32(ddlType.SelectedValue);
        }

        private void PopulateTrips()
        {
            ddlTrip.Items.Clear();
            ddlTrip.DataSource = TDCShipmentController.GetTrips(criteria); // private criteria
            ddlTrip.DataBind();
            ddlTrip.Items.Insert(0, new ListItem("All", "-1"));
        }

        private void PopulateDrops()
        {
            ddlDrop.Items.Clear();
            ddlDrop.DataSource = TDCShipmentController.GetDrops(Criteria); // public criteria
            ddlDrop.DataBind();
            ddlDrop.Items.Insert(0, new ListItem("All", "-1"));
        }

        private void PopulateStatusCodes()
        {
            // Append status codes
            string[] StatusNames = Enum.GetNames(typeof(Shipment.StatusEnum));
            Array StatusValues = Enum.GetValues(typeof(Shipment.StatusEnum));

            // Add text and values
            for (int i = 0; i < StatusNames.Length; i++)
            {
                ddlStatus.Items.Add(new ListItem(StatusNames[i], ((int)StatusValues.GetValue(i)).ToString()));
            }
        }

        /// <summary>
        /// Handles the Click event of the btnSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
         
                // Fire the event
                OnSearchClicked(new EventArgs());

        }

        protected void ddlTrip_SelectedIndexChanged(object sender, EventArgs e)
        {
            // We need to populate the drops
            PopulateDrops();
        }
        
        protected void ddlOpCo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opCo = "";
            if (ddlOpCo.SelectedValue != "-1")
                opCo = ddlOpCo.SelectedItem.Text;

            if (shipmentType == ShipmentTypeEnum.TDC)
            {
                // Populate delivery warehouses for TDC Shipment option
                PopulateTDCDeliveryWarehouses(opCo);
                // Populate Sales Locations for TDC Shipment option
                PopulateTDCSalesLocations(opCo);
            }
            else
            {
                // Populate delivery warehouses for OpCo Shipment option
                PopulateOpCoDeliveryWarehouses(opCo);
                // Populate Sales Locations for OpCo Shipment option
                PopulateOpCoSalesLocations(opCo);
            }
        }

        protected void ddlDeliveryWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            // We need to populate the routes for TDC ONLY
            if (shipmentType == ShipmentTypeEnum.TDC)
            {
                if (ddlDeliveryWarehouse.SelectedValue != "-1")
                    PopulateTDCRoutes(ddlDeliveryWarehouse.SelectedItem.Text);
                else
                    PopulateTDCRoutes();
            }
        }

}
}

