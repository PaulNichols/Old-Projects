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
using Discovery.BusinessObjects.Controllers;
using Discovery.BusinessObjects;

namespace Discovery.UI.Web.UserControls
{

    public class OpCoRedirectEventArg : EventArgs
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


    public delegate void OpCoShipmentsUserControlRedirect(ref OpCoRedirectEventArg referralArgs);

    public partial class OpCoShipmentsUserControl : System.Web.UI.UserControl
    {
        public event OpCoShipmentsUserControlRedirect Redirecting;

        [Bindable(true)]
        [Category("Appearance")]
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
                        if (null != Redirecting)
                        {
                            OpCoRedirectEventArg redirectEventArgs = new OpCoRedirectEventArg();
                            Redirecting(ref redirectEventArgs);

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
                        if (null != Redirecting)
                        {
                            OpCoRedirectEventArg redirectEventArgs = new OpCoRedirectEventArg();
                            Redirecting(ref redirectEventArgs);

                            if (!redirectEventArgs.CancelRedirect)
                            {
                                // Get the opco code, shipment number and the despatch number for the tdc shipment
                                string[] shipmentParams = e.CommandArgument.ToString().Split('|');
                                // Get the tdc shipment
                                Discovery.BusinessObjects.TDCShipment tdcShipment = TDCShipmentController.GetShipment(shipmentParams[0], shipmentParams[1], shipmentParams[2]);
                                if (null != tdcShipment)
                                {
                                    // Redirect to the details page
                                    Response.Redirect(string.Format("~/Shipments/TDCShipment.aspx?Id={0}&{1}",
                                                tdcShipment.Id,
                                                Discovery.Utility.UIHelper.GenerateUrlReferrer(HttpContext.Current, new string[] 
                                        { 
                                            redirectEventArgs.QueryParams,
                                            string.Concat("PageIndex=", discoveryPager.PageIndex.ToString()),
                                            string.Concat("PageSize=", discoveryPager.PageSize.ToString()),
                                            string.Concat("SortExpression=", string.Concat(discoveryShipments.SortExpression, " ", discoveryShipments.SortDirection.ToString()))
                                        })));
                                }
                            }
                        }

                        // Done;
                        break;
                    }
                case "AuditDetail":
                    {
                        if (null != Redirecting)
                        {
                            OpCoRedirectEventArg redirectEventArgs = new OpCoRedirectEventArg();
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
                case "MapToTdc":
                    {
                        // Get the opco shipment
                        Discovery.BusinessObjects.OpCoShipment opCoShipment = OpCoShipmentController.GetShipment(Convert.ToInt32(e.CommandArgument));
                        // Make sure that we found the shipment
                        if (null != opCoShipment && opCoShipment.Id != -1)
                        {
                            // We have the shipment, now map it to a tdc shipment if not mapped
                            if (opCoShipment.Status == Shipment.StatusEnum.NotMapped)
                            {
                                // The new shipment
                                TDCShipment tdcShipment;

                                try
                                {
                                    //see if there is an existing TDCShipment
                                    TDCShipment existingTDCShipment = TDCShipmentController.GetShipment(
                                      opCoShipment.OpCoCode,
                                      opCoShipment.ShipmentNumber,
                                      opCoShipment.DespatchNumber);

                                    // Map opco shipment
                                    tdcShipment = opCoShipment.MapToTDC(existingTDCShipment, Page.User.Identity.Name, false);

                                    // Update the opco shipment status
                                    OpCoShipmentController.UpdateShipmentStatus(opCoShipment);

                                    // Display message
                                    DiscoveryPage.DisplayMessage(string.Format("The OpCo shipment was mapped to a new TDC shipment <a href=\"TDCShipment.aspx?Id={0}\">{1}-{2}</a>.", tdcShipment.Id, tdcShipment.ShipmentNumber, tdcShipment.DespatchNumber), DiscoveryMessageType.Success, Page.Master);
                                }
                                catch (Exception ex)
                                {
                                    DiscoveryPage.DisplayMessage(ex.Message, DiscoveryMessageType.Error, Page.Master);
                                }
                                // Status changed, re bind the data
                                discoveryShipments.DataBind();
                            }
                        }

                        // Done;
                        break;
                    }
            }
        }
    }
}