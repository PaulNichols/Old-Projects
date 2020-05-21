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
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.ComponentModel;
using Discovery.BusinessObjects;
using Discovery.BusinessObjects.Controllers;
using Discovery.Web.UI.CustomControls;

namespace Discovery.UI.Web.UserControls
{

    public class TDCRedirectEventArg : EventArgs
    {
        private string queryParams = "";

        public string QueryParams
        {
            get { return queryParams; }
            set { queryParams = value; }
        }

        private bool cancelRedirect = false;

        public bool CancelRedirect
        {
            get { return cancelRedirect; }
            set { cancelRedirect = value; }
        }
    }

    public delegate void TDCShipmentsUserControlRedirect(ref TDCRedirectEventArg referralArgs);

    public partial class TDCShipmentsUserControl : System.Web.UI.UserControl
    {
        public event TDCShipmentsUserControlRedirect Redirecting;

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("100%")]
        public Unit Width
        {
            get
            {
                return ((null == ViewState["Width"]) ? Unit.Empty : (Unit)ViewState["Width"]);
            }
            set
            {
                ViewState["Width"] = value;
            }
        }

        private int SelectedShipmentID
        {
            get
            {
                return ((null == ViewState["SelectedShipmentID"]) ? -1 : (int)ViewState["SelectedShipmentID"]);
            }
            set
            {
                ViewState["SelectedShipmentID"] = value;
            }
        }

        public string DataSourceID
        {
            get
            {
                return discoveryShipments.DataSourceID;
            }
            set
            {
                discoveryShipments.DataSourceID = value;
            }
        }

        public int PageSize
        {
            get
            {
                return discoveryPager.PageSize;
            }
            set
            {
                discoveryPager.PageSize = value;
            }
        }

        public int PageIndex
        {
            get
            {
                return discoveryPager.PageIndex;
            }
            set
            {
                discoveryPager.PageIndex = value;
            }
        }

        public int TotalRows
        {
            get
            {
                return discoveryPager.TotalRows;
            }
            set
            {
                discoveryPager.TotalRows = value;
            }
        }

        public string SortExpression
        {
            get
            {
                return discoveryShipments.SortExpression;
            }
            set
            {
                discoveryShipments.SortExpression = value;
            }
        }

        public SortDirection SortDirection
        {
            get
            {
                return discoveryShipments.SortDirection;
            }
            set
            {
                discoveryShipments.SortDirection = value;
            }
        }

        public new void DataBind()
        {
            discoveryShipments.DataBind();
        }

        protected override void OnInit(EventArgs e)
        {
            // Call base
            base.OnInit(e);

            // Wire up item command for when an item is selected
            discoveryShipments.ItemCommand += new RepeaterCommandEventHandler(discoveryShipments_ItemCommand);

            // Get the page changed event
            discoveryPager.PageChanged += new EventHandler(discoveryPager_PageChanged);

            // Get the page size changed event
            discoveryPager.PageSizeChanged += new EventHandler(discoveryPager_PageSizeChanged);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            // Specify width of this control
            discoveryPager.Width = Width;
            panelContainer.Width = Width;
        }

        void ShipmentCriteria_SearchClicked(object sender, EventArgs e)
        {
            // Reset the page number
            discoveryPager.PageIndex = 0;

            // Search was clicked, refresh shipments
            discoveryShipments.DataBind();
        }

        void discoveryPager_PageChanged(object sender, EventArgs e)
        {
            // Page index changed, refresh shipments
            discoveryShipments.DataBind();
        }

        void discoveryPager_PageSizeChanged(object sender, EventArgs e)
        {
            // Page size changed, refresh shipments
            discoveryShipments.DataBind();
        }

        protected void discoveryShipments_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Sort":
                    {
                        // Bind data
                        discoveryShipments.DataBind();

                        // Done
                        break;
                    }
                case "OpCoDetail":
                    {
                        // See if we have a redirecting delegate
                        if (null != Redirecting)
                        {
                            TDCRedirectEventArg redirectEventArgs = new TDCRedirectEventArg();
                            Redirecting(ref redirectEventArgs);

                            // Do we continue redirecting?
                            if (!redirectEventArgs.CancelRedirect)
                            {
                                // Redirect to the details page
                                Response.Redirect(string.Format("~/Shipments/OpCoShipment.aspx?Id={0}&{1}",
                                            e.CommandArgument.ToString(),
                                            Discovery.Utility.UIHelper.GenerateUrlReferrer(HttpContext.Current, new string[] 
                                        { 
                                            redirectEventArgs.QueryParams,
                                            string.Concat("PageIndex=", discoveryPager.PageIndex.ToString()),
                                            string.Concat("PageSize=", discoveryPager.PageSize.ToString()),
                                            string.Concat("SortExpression=", string.Concat(discoveryShipments.SortExpression, " ", discoveryShipments.SortDirection.ToString()))
                                        })));
                            }
                        }

                        // Done;
                        break;
                    }
                case "TDCDetail":
                    {
                        // See if we have a redirecting delegate
                        if (null != Redirecting)
                        {
                            TDCRedirectEventArg redirectEventArgs = new TDCRedirectEventArg();
                            Redirecting(ref redirectEventArgs);

                            // Do we continue redirecting?
                            if (!redirectEventArgs.CancelRedirect)
                            {
                                // Redirect to the details page
                                Response.Redirect(string.Format("~/Shipments/TDCShipment.aspx?Id={0}&{1}",
                                            e.CommandArgument.ToString(),
                                            Discovery.Utility.UIHelper.GenerateUrlReferrer(HttpContext.Current, new string[] 
                                        { 
                                            redirectEventArgs.QueryParams,
                                            string.Concat("PageIndex=", discoveryPager.PageIndex.ToString()),
                                            string.Concat("PageSize=", discoveryPager.PageSize.ToString()),
                                            string.Concat("SortExpression=", string.Concat(discoveryShipments.SortExpression, " ", discoveryShipments.SortDirection.ToString()))
                                        })));
                            }
                        }

                        // Done;
                        break;
                    }
                case "AuditDetail":
                    {
                        if (null != Redirecting)
                        {
                            TDCRedirectEventArg redirectEventArgs = new TDCRedirectEventArg();
                            Redirecting(ref redirectEventArgs);

                            if (!redirectEventArgs.CancelRedirect)
                            {
                                // Redirect to the details page
                                Response.Redirect(string.Format("~/Admin/MessageAudit.aspx?Id={0}&{1}",
                                                            e.CommandArgument.ToString(),
                                            Discovery.Utility.UIHelper.GenerateUrlReferrer(HttpContext.Current, new string[] 
                                        { 
                                            redirectEventArgs.QueryParams,
                                            string.Concat("PageIndex=", discoveryPager.PageIndex.ToString()),
                                            string.Concat("PageSize=", discoveryPager.PageSize.ToString()),
                                            string.Concat("SortExpression=", string.Concat(discoveryShipments.SortExpression, " ", discoveryShipments.SortDirection.ToString()))
                                        })));
                            }
                        }
                        // Done
                        break;
                    }
                case "PrintDelColl":
                    {
                        try
                        {
                            // Print the shipment
                            TDCShipmentController.GetShipment(Convert.ToInt32(e.CommandArgument)).PrintDeliveryCollectionNote();

                            // Display message
                            ((DiscoveryPage)this.Page).DisplayMessage("The delivery/collection note was printed successfully.", DiscoveryMessageType.Success);
                        }
                        catch (Exception ex)
                        {
                            // Display message
                            ((DiscoveryPage)this.Page).DisplayMessage("There was an error printing the delivery/collection note.", DiscoveryMessageType.Error);
                        }
                        // Done
                        break;
                    }
                case "PrintTransConv":
                    {
                        try
                        {
                            // Print the shipment
                            TDCShipmentController.GetShipment(Convert.ToInt32(e.CommandArgument)).PrintTransferConversionNote();

                            // Display message
                            ((DiscoveryPage)this.Page).DisplayMessage("The transfer/conversion note was printed successfully.", DiscoveryMessageType.Success);
                        }
                        catch (Exception ex)
                        {
                            // Display message
                            ((DiscoveryPage)this.Page).DisplayMessage("There was an error printing the transfer/conversion note.", DiscoveryMessageType.Error);
                        }
                        // Done
                        break;
                    }
            }
        }

        protected void discoveryShipments_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Get the shipment we're bound to
                TDCShipment shipment = (TDCShipment)e.Item.DataItem;

                // Find the TDC shipment details button and hide it if we dont't have permission
                e.Item.FindControl("btnTDCShipmentDetails").Visible = ((
                                                                       shipment.Type == Shipment.TypeEnum.WHS &&
                                                                       DiscoveryPage.HasRule(
                                                                           "Shipment: View Warehouse Order Detail", Context.User)
                                                                           )
                                                                           ||
                                                                           (
                                                                        shipment.Type == Shipment.TypeEnum.ADH &&
                                                                       DiscoveryPage.HasRule(
                                                                           "Shipment: View Ad-Hoc Order Detail", Context.User)
                                                                           )
                                                                           ||
                                                                           (
                                                                        shipment.Type == Shipment.TypeEnum.OPCO &&
                                                                       DiscoveryPage.HasRule(
                                                                           "Shipment: View TDC Shipment Detail", Context.User)
                                                                           )

                                                                        );

                // Find the OpCo shipment details button and hide it if we dont't have permission
                e.Item.FindControl("btnOpCoShipmentDetail").Visible = ((
                                                                         shipment.Type == Shipment.TypeEnum.OPCO &&
                                                                         DiscoveryPage.HasRule(
                                                                             "Shipment: View OpCo Shipment Detail", Context.User)
                                                                     ));

                // See if we have permission to print (also it has a status other than not mapped)
                e.Item.FindControl("btnPrintTransConv").Visible = (DiscoveryPage.HasRule("Shipment: Print Orders", Context.User) && shipment.IsTransferOrConversion && shipment.Status != Shipment.StatusEnum.Mapped);
                e.Item.FindControl("btnPrintDelColl").Visible = (DiscoveryPage.HasRule("Shipment: Print Orders", Context.User) && shipment.Status != Shipment.StatusEnum.Mapped);

                // Se if we can view the audit messages
                e.Item.FindControl("btnAuditDetail").Visible = (DiscoveryPage.HasRule("Admin: View Message Audits", Context.User));

                // If we've been routed, see if the estimated delivery date is outside the required window
                if (shipment.IsEstimatedDeliveryLate)
                {
                    // Change the header style
                    (e.Item.FindControl("panelHeader") as Panel).CssClass = "shipmentHeaderEstLate";
                }
                else if (shipment.IsActualDeliveryLate)
                {
                    // Change the header style
                    (e.Item.FindControl("panelHeader") as Panel).CssClass = "shipmentHeaderActLate";
                }
                else if (shipment.IsTransferOrConversion)
                {
                    // If non of the above, see if we're a conversion
                    (e.Item.FindControl("panelHeader") as Panel).CssClass = "shipmentHeaderTransConv";
                }
            }
        }
    }
}