/*************************************************************************************************
 ** FILE:	TDCShipmentLines.aspx.cs
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
using System.Diagnostics;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using Discovery.BusinessObjects;
using Discovery.BusinessObjects.Controllers;
using Discovery.Utility;

namespace Discovery.UI.Web.UserControls
{
    public partial class TDCShipmentLines : System.Web.UI.UserControl
    {
        // Event that is fired if we update a shipment line
        public event EventHandler SaveComplete;

        // Our shipment
        private TDCShipment shipment;

        // Can we edit the line
        private bool canEdit = false;

        // The line we're editing
        protected int SelectedItemIndex
        {
            get
            {
                return (null == ViewState["SelectedIndex"]) ? -1 : (int)ViewState["SelectedIndex"];
            }
            set
            {
                ViewState["SelectedIndex"] = value;
            }
        }

        // Are we editing an item
        protected bool InEditMode = false;

        public void RefreshData()
        {
            // Re-bind the data
            dataSourceShipmentLines.DataBind();
            
            // Re-bind the ui
            discoveryShipmentLines.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Wire up item command for repeater
            discoveryShipmentLines.ItemCommand += new RepeaterCommandEventHandler(discoveryShipmentLines_ItemCommand);

            // Wire up selecting events for sorting
            dataSourceShipmentLines.Selecting += new ObjectDataSourceSelectingEventHandler(dataSourceShipmentLines_Selecting);

            // Update ui
            if (!Page.IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["ExpandLines"]))
                {
                    if (Convert.ToBoolean(Request.QueryString["ExpandLines"]))
                    {

                    }
                }

            }
        }

        void dataSourceShipmentLines_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            // Sorting
            e.Arguments.SortExpression = string.Concat(discoveryShipmentLines.SortExpression, " ", discoveryShipmentLines.SortDirection.ToString());
        }

        void discoveryShipmentLines_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Sort":
                    {
                        // Bind data
                        discoveryShipmentLines.DataBind();
                        // Done
                        break;
                    }
            }
        }

        protected void TDCShipmentLineFormView_OnModeChanging(Object sender, FormViewModeEventArgs e)
        {
            // Get the form view
            FormView tdcShipmentLineFormView = (FormView)sender;

            // Make sure we found the form view
            Debug.Assert(null != tdcShipmentLineFormView);

            // Store the selected item
            SelectedItemIndex = (int)tdcShipmentLineFormView.DataKey.Value;
            InEditMode = (e.NewMode == FormViewMode.Edit);

            // Rebind the list of shipments
            discoveryShipmentLines.DataBind();
        }

        protected void TDCShipmentLineFormView_OnDataBound(object sender, EventArgs e)
        {
            // If we're in read only mode see if we can switch to edit mode
            if (((FormView)sender).CurrentMode == FormViewMode.ReadOnly)
            {
                // Find the edit button
                ImageButton editButton = DiscoveryPage.GetControl<ImageButton>("ButtonEdit", (FormView)sender);
                // See if we found the edit button (depends when we're data binding)
                if (editButton != null)
                {
                    // Do we already have the shipment
                    if (shipment == null)
                    {
                        // Seed can edit
                        canEdit = false;

                        // Make sure we have a query string value
                        if (null != Request.QueryString["id"])
                        {
                            // Get the shipment
                            shipment = TDCShipmentController.GetShipment(Convert.ToInt32(Request.QueryString["id"]));

                            // Now we have the shipment, load the load category codes

                            // See if we can edit the line
                            if (null != shipment && shipment.Id > 0)
                            {
                                canEdit =
                                (
                                    /* Not Complete */
                                   (
                                    shipment.Status != Shipment.StatusEnum.Completed
                                    )
                                   &&
                                   (
                                    (
                                        shipment.Type ==
                                        Shipment.TypeEnum.WHS &&
                                        DiscoveryPage.HasRule("Shipment: Edit Warehouse Order Detail", Context.User)
                                    )
                                    ||
                                    (
                                        shipment.Type ==
                                        Shipment.TypeEnum.ADH &&
                                        DiscoveryPage.HasRule("Shipment: Edit Ad-Hoc Order Detail", Context.User)
                                    )
                                    ||
                                    (
                                        shipment.Type ==
                                        Shipment.TypeEnum.OPCO &&
                                        DiscoveryPage.HasRule("Shipment: Edit TDC Shipment Detail", Context.User)
                                    )
                                   )
                                );
                            }
                        }
                    }
                    // Update the visibility
                    editButton.Visible = canEdit;
                }
            }
        }

        protected void TDCShipmentLineFormView_OnItemCommand(Object sender, FormViewCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                try
                {
                    // Get the form view
                    FormView tdcShipmentLineFormView = (FormView)sender;

                    // Get the existing shipment line
                    TDCShipmentLine tdcShipmentLine = TDCShipmentLineController.GetLine((int)tdcShipmentLineFormView.DataKey.Value);

                    // Bind ui to shipment line instance
                    tdcShipmentLine.Description1 = ((TextBox)tdcShipmentLineFormView.Row.FindControl("txtDescription1")).Text;
                    tdcShipmentLine.Description2 = ((TextBox)tdcShipmentLineFormView.Row.FindControl("txtDescription2")).Text;
                    tdcShipmentLine.ConversionInstructions = ((TextBox)tdcShipmentLineFormView.Row.FindControl("txtConversionInstructions")).Text;
                    tdcShipmentLine.Packing = ((TextBox)tdcShipmentLineFormView.Row.FindControl("txtPacking")).Text;
                    tdcShipmentLine.ProductCode = ((TextBox)tdcShipmentLineFormView.Row.FindControl("txtProductCode")).Text;
                    string quantity = ((TextBox)tdcShipmentLineFormView.Row.FindControl("txtQuantity")).Text;
                    tdcShipmentLine.Quantity = Convert.ToInt32(Null.GetNull(quantity, "0"));
                    tdcShipmentLine.QuantityUnit = ((TextBox)tdcShipmentLineFormView.Row.FindControl("txtQuantityUnit")).Text;
                    tdcShipmentLine.CustomerReference = ((TextBox)tdcShipmentLineFormView.Row.FindControl("txtCustomerReference")).Text;
                    string conversionQuantity = ((TextBox)tdcShipmentLineFormView.Row.FindControl("txtConversionQuantity")).Text;
                    tdcShipmentLine.ConversionQuantity = Convert.ToInt32(Null.GetNull(conversionQuantity, "0"));
                    tdcShipmentLine.IsPanel = ((CheckBox)tdcShipmentLineFormView.Row.FindControl("chkIsPanel")).Checked;
                    tdcShipmentLine.IsISO9000Approved = ((CheckBox)tdcShipmentLineFormView.Row.FindControl("chkIsISO9000Approved")).Checked;
                    tdcShipmentLine.LoadCategoryCode = ((DropDownList)tdcShipmentLineFormView.Row.FindControl("ddlLoadCategory")).SelectedValue;
                    string netWeight = ((TextBox)tdcShipmentLineFormView.Row.FindControl("txtNetWeight")).Text;
                    tdcShipmentLine.NetWeight = Convert.ToDecimal(Null.GetNull(netWeight, "0"));
                    string width = ((TextBox)tdcShipmentLineFormView.Row.FindControl("txtWidth")).Text;
                    tdcShipmentLine.Width = Convert.ToInt32(Null.GetNull(width, "0"));
                    string length = ((TextBox)tdcShipmentLineFormView.Row.FindControl("txtLength")).Text;
                    tdcShipmentLine.Length = Convert.ToInt32(Null.GetNull(length, "0"));
                    string volume = ((TextBox)tdcShipmentLineFormView.Row.FindControl("txtVolume")).Text;
                    tdcShipmentLine.Volume = Convert.ToDecimal(Null.GetNull(volume, "0"));
                    string microns = ((TextBox)tdcShipmentLineFormView.Row.FindControl("txtMicrons")).Text;
                    tdcShipmentLine.Microns = Convert.ToInt32(Null.GetNull(microns, "0"));
                    string grammage = ((TextBox)tdcShipmentLineFormView.Row.FindControl("txtGrammage")).Text;
                    tdcShipmentLine.Grammage = Convert.ToInt32(Null.GetNull(grammage, "0"));

                    // Save the shipment line
                    TDCShipmentLineController.SaveLine(tdcShipmentLine);

                    // Rebind the list of shipments
                    discoveryShipmentLines.DataBind();

                    // Notify any listners that we've updated a shipment line
                    if (null != SaveComplete)
                    {
                        // Fire the event
                        SaveComplete(this, new EventArgs());
                    }

                    // Display message
                    ((DiscoveryPage)Page).DisplayMessage("The shipment line was updated.", DiscoveryMessageType.Success);
                }
                catch (Exception ex)
                {
                    // Display message
                    ((DiscoveryPage)Page).DisplayMessage(String.Concat("There was a problem updating the shipment line.  ", ex.Message), DiscoveryMessageType.Error);
                }
            }
        }

        protected void discoveryShipmentLines_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Find the content panel
                Control contentCtrl = e.Item.FindControl("panelContent");
                // Find the form view
                FormView formViewCtrl = (FormView)contentCtrl.FindControl("TDCShipmentLineFormView");
                // Get the shipment line
                TDCShipmentLine tdcShipmenLine = e.Item.DataItem as TDCShipmentLine;
                // Add it to a list for data binding
                List<TDCShipmentLine> tdcShipmentLines = new List<TDCShipmentLine>(1);
                tdcShipmentLines.Add(tdcShipmenLine);
                // Set the mode of the form view
                formViewCtrl.ChangeMode(((tdcShipmenLine.Id == SelectedItemIndex) && InEditMode) ? FormViewMode.Edit : FormViewMode.ReadOnly);
                // Bind the form view data
                formViewCtrl.DataSource = tdcShipmentLines;
                formViewCtrl.DataBind();
            }
        }
    }
}