/*************************************************************************************************
 ** FILE:	TDCShipment.aspx.cs
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
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AjaxControlToolkit;
using System.Diagnostics;
using System.Transactions;
using Discovery.BusinessObjects;
using Discovery.BusinessObjects.Controllers;
using Discovery.UI.Web.UserControls;
using ValidationFramework.Web;
using Discovery.Utility;

namespace Discovery.UI.Web.Shipments
{
    /*************************************************************************************************
     ** CLASS:	TDCShipment
     **
     ** OVERVIEW:
     ** This page allows users to view and edit TDC Shipment details.
     **
     ** MODIFICATION HISTORY:
     **
     ** Date:		Version:    Who:	Change:
     ** 19/7/06		1.0			LAS		Initial Version
     ************************************************************************************************/
    public partial class TDCShipment : DiscoveryDataDetailPage
    {
        #region Fields

        #endregion

        #region Properties

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

        #endregion

        #region Protected Methods

        #endregion

        #region Events

        #endregion


        protected override void Page_Load(object sender, System.EventArgs e)
        {
            // Specify rules
            CreateRule = "Shipment: Edit TDC Shipment Detail";
            ReadRule = "Shipment: View TDC Shipment Detail";
            UpdateRule = "Shipment: Edit TDC Shipment Detail";
            DeleteRule = "Shipment: Edit TDC Shipment Detail";

            // Call base, sets up mode etc
            base.Page_Load(sender, e);

            // Specify the default back url
            if (String.IsNullOrEmpty(BackUrl))
            {
                BackUrl = "~/Shipments/TDCShipments.aspx";
            }

            // Update the ui, etc
            UpdateDisplay();
        }


        // **********************************************************************************************
        // Intercept the databinding within the ItemUpdating event 
        // by passing into the FormViewUpdateEventArgs the new values that we need to update.  
        // This interception gives us the opportunity to set the selections for the cascading dropdownlist
        // without relying on the Bind method to effect the 2-way databinding
        // **********************************************************************************************
        protected void TDCShipmentFormView_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            try
            {
                // Make sure that the page is valid
                if (Page.IsValid)
                {
                    // Holders for hours and minutes
                    string[] HHMM;
                    int HH = 0;
                    int MM = 0;
                    TextBox txtTime;
                    DateTime tmpDateTime;

                    // Convert to us dates as object datasource only uses invariant culture
                    if (!String.IsNullOrEmpty(e.NewValues["GeneratedDateTime"].ToString())) e.NewValues["GeneratedDateTime"] = Convert.ToDateTime(e.NewValues["GeneratedDateTime"]).ToString("MM/dd/yyyy HH:mm:ss");
                    if (!String.IsNullOrEmpty(e.NewValues["RequiredShipmentDate"].ToString())) e.NewValues["RequiredShipmentDate"] = Convert.ToDateTime(e.NewValues["RequiredShipmentDate"]).ToString("MM/dd/yyyy HH:mm:ss");
                    
                    // Combine date time into one field
                    if (!String.IsNullOrEmpty(e.NewValues["EstimatedDeliveryDate"].ToString()))
                    {
                        // Convert to a date
                        tmpDateTime = Convert.ToDateTime(e.NewValues["EstimatedDeliveryDate"]);
                        
                        // Add the time if we have it
                        txtTime = GetControl<TextBox>("txtEstimatedDeliveryTime", TDCShipmentFormView.Row);
                        
                        // Make sure we found the text box
                        Debug.Assert(txtTime != null);
                        if (!string.IsNullOrEmpty(txtTime.Text) && txtTime.Text.IndexOf(':') != -1)
                        {
                            // Retrieve the hours and minutes
                            HHMM = txtTime.Text.Split(':');
                            HH = Convert.ToInt32(HHMM[0]);
                            MM = Convert.ToInt32(HHMM[1]);
                            // Add to the time element
                            tmpDateTime = tmpDateTime.AddHours(HH).AddMinutes(MM);
                        }
                        // Store updated date time
                        e.NewValues["EstimatedDeliveryDate"] = tmpDateTime.ToString("MM/dd/yyyy HH:mm:ss");
                    }

                    // Combine date time into one field
                    if (!String.IsNullOrEmpty(e.NewValues["ActualDeliveryDate"].ToString()))
                    {
                        // Convert to a date
                        tmpDateTime = Convert.ToDateTime(e.NewValues["ActualDeliveryDate"]);
                        // Add the time if we have it
                        txtTime = GetControl<TextBox>("txtActualDeliveryTime", TDCShipmentFormView.Row);
                        // Make sure we found the text box
                        Debug.Assert(txtTime != null);
                        if (!string.IsNullOrEmpty(txtTime.Text) && txtTime.Text.IndexOf(':') != -1)
                        {
                            // Retrieve the hours and minutes
                            HHMM = txtTime.Text.Split(':');
                            HH = Convert.ToInt32(HHMM[0]);
                            MM = Convert.ToInt32(HHMM[1]);
                            // Add to the time element
                            tmpDateTime = tmpDateTime.AddHours(HH).AddMinutes(MM);
                        }
                        // Store updated date time
                        e.NewValues["ActualDeliveryDate"] = tmpDateTime.ToString("MM/dd/yyyy HH:mm:ss");
                    }
                    if (!String.IsNullOrEmpty(e.NewValues["RoutingDateTime"].ToString())) e.NewValues["RoutingDateTime"] = Convert.ToDateTime(e.NewValues["RoutingDateTime"]).ToString("MM/dd/yyyy HH:mm:ss");
                    if (!String.IsNullOrEmpty(e.NewValues["SentToWMS"].ToString())) e.NewValues["SentToWMS"] = Convert.ToDateTime(e.NewValues["SentToWMS"]).ToString("MM/dd/yyyy hh:mm:ss");

                    // Seed any values that aren't present in the UI
                    e.NewValues["LocationCode"] = "";
                    e.NewValues["ActualDeliveryDate"] = Null.NullDate.ToString("d");
                    e.NewValues["RouteTrip"] = "1";
                    e.NewValues["SplitSequence"] = "0";
                    e.NewValues["RoutingDateTime"] = Null.NullDate.ToString("d");
                    e.NewValues["RouteDrop"] = 1;
                }
                else e.Cancel = true;
            }
            catch (Exception ex)
            {
                // Display error message
                DisplayMessage(string.Format("Please ensure that all form values have been entered correctly.  The error was, <i>\"{0}\"</i>", ex.Message), DiscoveryMessageType.Error);
                // Don't alow update, etc
                e.Cancel = true;
            }
        }

        protected void TDCShipmentFormView_ItemInserting(object sender, FormViewInsertEventArgs e)
        {
            try
            {
                // Make sure that the page is valid
                if (Page.IsValid)
                {
                    // Get the sales branch code from the drop down as we don't use data binding
                    string salesBranch = ((DropDownList)((FormView)sender).FindControl("ddlSalesBranch")).SelectedValue;
                    e.Values["SalesBranchCode"] = salesBranch;

                    // Get the division code from the drop down as we don't use data binding
                    string divisionCode = ((DropDownList)((FormView)sender).FindControl("ddlDivision")).SelectedValue;
                    e.Values["DivisionCode"] = divisionCode;

                    // Convert to invariant culture dates
                    if (!String.IsNullOrEmpty(e.Values["RequiredShipmentDate"].ToString())) e.Values["RequiredShipmentDate"] = Convert.ToDateTime(e.Values["RequiredShipmentDate"]).ToString("MM/dd/yyyy HH:mm:ss");
                    if (!String.IsNullOrEmpty(e.Values["EstimatedDeliveryDate"].ToString())) e.Values["EstimatedDeliveryDate"] = Convert.ToDateTime(e.Values["EstimatedDeliveryDate"]).ToString("MM/dd/yyyy HH:mm:ss");

                    // Done
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                // Display error message
                DisplayMessage(string.Format("Please ensure that all form values have been entered correctly.  The error was, <i>\"{0}\"</i>", ex.Message), DiscoveryMessageType.Error);
                // Don't allow update, etc
                e.Cancel = true;
            }
        }

        protected void TDCShipmentDataSource_Inserting(Object sender, ObjectDataSourceMethodEventArgs e)
        {
            try
            {
                // Entity as will be saved to the db
                Discovery.BusinessObjects.TDCShipment tdcShipment = e.InputParameters[0] as Discovery.BusinessObjects.TDCShipment;

                // Get the shipment type
                string shipmentType = GetControl<DropDownList>("ddlType", TDCShipmentFormView.Row).SelectedItem.Value;

                // Store the shipment type
                tdcShipment.Type = (Shipment.TypeEnum)Enum.Parse(typeof(Shipment.TypeEnum), shipmentType);

                // Generate the shipment number
                tdcShipment.ShipmentNumber = String.Concat(
                            shipmentType.ToUpper(),
                            SequenceController.GetNextSequence(String.Concat(shipmentType, "SHIPMENT")).ToString());

                // Specify the despatch number
                tdcShipment.DespatchNumber = "1";

                // Specify the customer name
                tdcShipment.CustomerName = string.Concat(shipmentType.ToUpper(), " ad-hoc order");

                // Specify the customer number
                tdcShipment.CustomerNumber = "Ad-hoc";

                // Specify the customer reference
                tdcShipment.CustomerReference = "Ad-hoc";

                // Specify opco contact details
                tdcShipment.OpCoContact.Name = "NA";
                tdcShipment.OpCoContact.Email = "NA";

                // Specify the shipment name
                tdcShipment.ShipmentName = tdcShipment.CustomerName;

                // Shipment contact details
                tdcShipment.ShipmentContact.Name = GetControl<TextBox>("txtShipmentContactName", TDCShipmentFormView.Row).Text;
                tdcShipment.ShipmentContact.Email = GetControl<TextBox>("txtShipmentContactEmail", TDCShipmentFormView.Row).Text;
                tdcShipment.ShipmentContact.TelephoneNumber = GetControl<TextBox>("txtShipmentContactTelephone", TDCShipmentFormView.Row).Text;

                // Shipment address details
                tdcShipment.ShipmentAddress.Line1 = GetControl<TextBox>("txtShipmentAddress1", TDCShipmentFormView.Row).Text;
                tdcShipment.ShipmentAddress.Line2 = GetControl<TextBox>("txtShipmentAddress2", TDCShipmentFormView.Row).Text;
                tdcShipment.ShipmentAddress.Line3 = GetControl<TextBox>("txtShipmentAddress3", TDCShipmentFormView.Row).Text;
                tdcShipment.ShipmentAddress.Line4 = GetControl<TextBox>("txtShipmentAddress4", TDCShipmentFormView.Row).Text;
                tdcShipment.ShipmentAddress.Line5 = GetControl<TextBox>("txtShipmentAddress5", TDCShipmentFormView.Row).Text;
                tdcShipment.ShipmentAddress.PostCode = GetControl<TextBox>("txtShipmentAddressPostCode", TDCShipmentFormView.Row).Text;

                // PAF address details
                //tdcShipment.PAFAddress.Easting = Convert.ToInt32(GetControl<TextBox>("txtPAFEasting", TDCShipmentFormView.Row).Text);
                //tdcShipment.PAFAddress.Northing = Convert.ToInt32(GetControl<TextBox>("txtPAFNorthing", TDCShipmentFormView.Row).Text);
                //tdcShipment.PAFAddress.DPS = GetControl<TextBox>("txtPAFDPS", TDCShipmentFormView.Row).Text;
                tdcShipment.PAFAddress.Line1 = GetControl<TextBox>("txtPAFAddress1", TDCShipmentFormView.Row).Text;
                tdcShipment.PAFAddress.Line2 = GetControl<TextBox>("txtPAFAddress2", TDCShipmentFormView.Row).Text;
                tdcShipment.PAFAddress.Line3 = GetControl<TextBox>("txtPAFAddress3", TDCShipmentFormView.Row).Text;
                tdcShipment.PAFAddress.Line4 = GetControl<TextBox>("txtPAFAddress4", TDCShipmentFormView.Row).Text;
                tdcShipment.PAFAddress.Line5 = GetControl<TextBox>("txtPAFAddress5", TDCShipmentFormView.Row).Text;
                tdcShipment.PAFAddress.PostCode = GetControl<TextBox>("txtPAFAddressPostCode", TDCShipmentFormView.Row).Text;
                //tdcShipment.PAFAddress.Status = GetControl<TextBox>("txtPAFStatus", TDCShipmentFormView.Row).Text;

                // Updated by
                tdcShipment.UpdatedBy = User.Identity.Name;

                // Update the back url so that we can add lines,ase class redirects to this url
                BackUrl = String.Format("~/Shipments/TdcShipment.aspx?Id={0}&AddLines=True&Type={1}", "{0}", shipmentType); ;
            }
            catch (Exception ex)
            {
                // Display error message
                DisplayMessage(string.Format("Please ensure that all form values have been entered correctly.  The error was, <i>\"{0}\"</i>", ex.Message), DiscoveryMessageType.Error);
                // Failed
                e.Cancel = true;
            }
        }

        protected void TDCShipmentDataSource_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            try
            {
                // Entity as will be saved to the db
                Discovery.BusinessObjects.TDCShipment tdcShipment = e.InputParameters[0] as Discovery.BusinessObjects.TDCShipment;

                // OpCo contact details
                tdcShipment.OpCoContact.Name = GetControl<TextBox>("txtOpCoContactName", TDCShipmentFormView.Row).Text;
                tdcShipment.OpCoContact.Email = GetControl<TextBox>("txtOpCoContactEmail", TDCShipmentFormView.Row).Text;

                // Shipment contact details
                tdcShipment.ShipmentContact.Name = GetControl<TextBox>("txtShipmentContactName", TDCShipmentFormView.Row).Text;
                tdcShipment.ShipmentContact.Email = GetControl<TextBox>("txtShipmentContactEmail", TDCShipmentFormView.Row).Text;
                tdcShipment.ShipmentContact.TelephoneNumber = GetControl<TextBox>("txtShipmentContactTelephone", TDCShipmentFormView.Row).Text;

                // Customer address details
                tdcShipment.CustomerAddress.Line1 = GetControl<TextBox>("txtCustomerAddress1", TDCShipmentFormView.Row).Text;
                tdcShipment.CustomerAddress.Line2 = GetControl<TextBox>("txtCustomerAddress2", TDCShipmentFormView.Row).Text;
                tdcShipment.CustomerAddress.Line3 = GetControl<TextBox>("txtCustomerAddress3", TDCShipmentFormView.Row).Text;
                tdcShipment.CustomerAddress.Line4 = GetControl<TextBox>("txtCustomerAddress4", TDCShipmentFormView.Row).Text;
                tdcShipment.CustomerAddress.Line5 = GetControl<TextBox>("txtCustomerAddress5", TDCShipmentFormView.Row).Text;
                tdcShipment.CustomerAddress.PostCode = GetControl<TextBox>("txtCustomerAddressPostCode", TDCShipmentFormView.Row).Text;

                // Shipment address details
                tdcShipment.ShipmentAddress.Line1 = GetControl<TextBox>("txtShipmentAddress1", TDCShipmentFormView.Row).Text;
                tdcShipment.ShipmentAddress.Line2 = GetControl<TextBox>("txtShipmentAddress2", TDCShipmentFormView.Row).Text;
                tdcShipment.ShipmentAddress.Line3 = GetControl<TextBox>("txtShipmentAddress3", TDCShipmentFormView.Row).Text;
                tdcShipment.ShipmentAddress.Line4 = GetControl<TextBox>("txtShipmentAddress4", TDCShipmentFormView.Row).Text;
                tdcShipment.ShipmentAddress.Line5 = GetControl<TextBox>("txtShipmentAddress5", TDCShipmentFormView.Row).Text;
                tdcShipment.ShipmentAddress.PostCode = GetControl<TextBox>("txtShipmentAddressPostCode", TDCShipmentFormView.Row).Text;

                // PAF address details
                tdcShipment.PAFAddress.Easting = Convert.ToInt32(GetControl<TextBox>("txtPAFEasting", TDCShipmentFormView.Row).Text);
                tdcShipment.PAFAddress.Northing = Convert.ToInt32(GetControl<TextBox>("txtPAFNorthing", TDCShipmentFormView.Row).Text);
                tdcShipment.PAFAddress.DPS = GetControl<TextBox>("txtPAFDPS", TDCShipmentFormView.Row).Text;
                tdcShipment.PAFAddress.Line1 = GetControl<TextBox>("txtPAFAddress1", TDCShipmentFormView.Row).Text;
                tdcShipment.PAFAddress.Line2 = GetControl<TextBox>("txtPAFAddress2", TDCShipmentFormView.Row).Text;
                tdcShipment.PAFAddress.Line3 = GetControl<TextBox>("txtPAFAddress3", TDCShipmentFormView.Row).Text;
                tdcShipment.PAFAddress.Line4 = GetControl<TextBox>("txtPAFAddress4", TDCShipmentFormView.Row).Text;
                tdcShipment.PAFAddress.Line5 = GetControl<TextBox>("txtPAFAddress5", TDCShipmentFormView.Row).Text;
                tdcShipment.PAFAddress.PostCode = GetControl<TextBox>("txtPAFAddressPostCode", TDCShipmentFormView.Row).Text;
                // tdcShipment.PAFAddress.Status = GetControl<TextBox>("txtPAFStatus", TDCShipmentFormView.Row).Text;
                //need to persist match value

                // Who's making the update
                tdcShipment.UpdatedBy = User.Identity.Name;
            }
            catch (Exception ex)
            {
                // Display error message
                DisplayMessage(string.Format("Please ensure that all form values have been entered correctly.  The error was, <i>\"{0}\"</i>", ex.Message), DiscoveryMessageType.Error);
                // Don't alow update, etc
                e.Cancel = true;
            }
        }

        // As the FormView becomes databound, the DataBound events of the dropdownlists
        // will trigger.  At this stage the page has not been rendered yet and we can 
        // manipulate the displayed values on the dropdownlists.
        protected void ddlSalesBranch_DataBound(object sender, EventArgs e)
        {
            // Get the drop down list control
            DropDownList ddl = (DropDownList)sender;
            // Get the formview from the ddl
            FormView frmView = (FormView)ddl.NamingContainer;
            // Make sure we found the formview
            if (frmView.DataItem != null)
            {
                //  Let's pull the opco code value from the databound item.
                // As we're bound to the tdcshipment business object we need
                // to cast it to a tdcshipment
                string opcoCode = ((Discovery.BusinessObjects.TDCShipment)frmView.DataItem).OpCoCode;

                // Remove the existing values from the ddl
                ddl.ClearSelection();

                // Make sure that the current value is in the ddl
                ListItem li = ddl.Items.FindByValue(opcoCode);
                if (li != null) li.Selected = true;
            }
        }

        // As the FormView becomes databound, the DataBound events of the dropdownlists
        // will trigger.  At this stage the page has not been rendered yet and we can 
        // manipulate the displayed values on the dropdownlists.
        protected void ddlDivision_DataBound(object sender, EventArgs e)
        {
            // Get the drop down list control
            DropDownList ddl = (DropDownList)sender;
            // Get the formview from the ddl
            FormView frmView = (FormView)ddl.NamingContainer;
            // Make sure we found the formview
            if (frmView.DataItem != null)
            {
                //  Let's pull the opco code value from the databound item.
                // As we're bound to the tdcshipment business object we need
                // to cast it to a tdcshipment
                string divisionCode = ((Discovery.BusinessObjects.TDCShipment)frmView.DataItem).DivisionCode;

                // Remove the existing values from the ddl
                ddl.ClearSelection();

                // Make sure that the current value is in the ddl
                ListItem li = ddl.Items.FindByValue(divisionCode);
                if (li != null) li.Selected = true;
            }
        }

        private void UpdateDisplay()
        {
            // See if we need to display the add lines popup
            if (!IsPostBack)
            {
                // See if we've added lines
                if (!String.IsNullOrEmpty(Request.QueryString["AddLines"]))
                {
                    // See if we display add lines message and popup
                    if (Convert.ToBoolean(Request.QueryString["AddLines"]))
                    {
                        // Display message, we've saved the shipment, add lines
                        DisplayMessage("The ad-hoc shipment was saved.", DiscoveryMessageType.Success);

                        // Display the add lines dialog
                        GetControl<TDCShipmentLinesAdd>("TDCShipmentLinesAdditions", TDCShipmentFormView.Row).DisplayPopup(true);
                    }
                }
                else if (!String.IsNullOrEmpty(Request.QueryString["ExpandLines"]))
                {
                    if (Convert.ToBoolean(Request.QueryString["ExpandLines"]))
                    {
                        // Load the collapsible panel extender
                        CollapsiblePanelExtender linesExtender = TDCShipmentFormView.Row.FindControl("collapsiblePanelShipmentLines") as CollapsiblePanelExtender;
                        // Make sure we've found it
                        Debug.Assert(null != linesExtender);
                        // Expand lines
                        linesExtender.Collapsed = false;
                    }
                }
            }
        }

        protected override void SetValidation()
        {
            // Called by the base class on a mode change event
            switch (TDCShipmentFormView.CurrentMode)
            {
                case FormViewMode.Edit:
                    {
                        // Generate ui validation controls

                        Validation.AddValidation("txtRequiredDeliveryDate", "RequiredShipmentDate");
                        Validation.AddValidation("txtAfterTime", "AfterTime");
                        Validation.AddValidation("txtBeforeTime", "BeforeTime");
                        Validation.AddValidation("txtCheckInTime", "CheckInTime");
                        Validation.AddValidation("txtVehicleMaxWeight", "VehicleMaxWeight");
                        Validation.AddValidation("txtInstructions", "Instructions");

                        // OpCo Contact
                        Validation.AddValidation("txtOpCoContactName", "OpCoContact.Name");
                        Validation.AddValidation("txtOpCoContactEmail", "OpCoContact.Email");

                        // Shipment Contact
                        Validation.AddValidation("txtShipmentContactName", "ShipmentContact.Name");
                        Validation.AddValidation("txtShipmentContactEmail", "ShipmentContact.Email");
                        Validation.AddValidation("txtShipmentContactTelephone", "ShipmentContact.TelephoneNumber");

                        // PAF Address Info
                        Validation.AddValidation("txtPAFEasting", "PAFAddress.Easting");
                        Validation.AddValidation("txtPAFNorthing", "PAFAddress.Northing");
                        Validation.AddValidation("txtPAFDPS", "PAFAddress.DPS");
                        Validation.AddValidation("txtPAFStatus", "PAFAddress.Status");

                        // Customer address
                        Validation.AddValidation("txtCustomerAddressName", "CustomerName");
                        Validation.AddValidation("txtCustomerAddress1", "CustomerAddress.Line1");
                        Validation.AddValidation("txtCustomerAddress2", "CustomerAddress.Line2");
                        Validation.AddValidation("txtCustomerAddress3", "CustomerAddress.Line3");
                        Validation.AddValidation("txtCustomerAddress4", "CustomerAddress.Line4");
                        Validation.AddValidation("txtCustomerAddress5", "CustomerAddress.Line5");
                        Validation.AddValidation("txtCustomerAddressPostCode", "CustomerAddress.PostCode");

                        // Shipment Address
                        Validation.AddValidation("txtShipmentAddressName", "ShipmentName");
                        Validation.AddValidation("txtShipmentAddress1", "ShipmentAddress.Line1");
                        Validation.AddValidation("txtShipmentAddress2", "ShipmentAddress.Line2");
                        Validation.AddValidation("txtShipmentAddress3", "ShipmentAddress.Line3");
                        Validation.AddValidation("txtShipmentAddress4", "ShipmentAddress.Line4");
                        Validation.AddValidation("txtShipmentAddress5", "ShipmentAddress.Line5");
                        Validation.AddValidation("txtShipmentAddressPostCode", "ShipmentAddress.PostCode");

                        // PAF Address
                        Validation.AddValidation("txtPAFAddress1", "PAFAddress.Line1");
                        Validation.AddValidation("txtPAFAddress2", "PAFAddress.Line2");
                        Validation.AddValidation("txtPAFAddress3", "PAFAddress.Line3");
                        Validation.AddValidation("txtPAFAddress4", "PAFAddress.Line4");
                        Validation.AddValidation("txtPAFAddress5", "PAFAddress.Line5");
                        Validation.AddValidation("txtPAFAddressPostCode", "PAFAddress.PostCode");

                        // Done
                        break;
                    }
                case FormViewMode.Insert:
                    {
                        // Set defaults if first time here
                        if (!IsPostBack)
                        {
                            // Required date
                            GetControl<TextBox>("txtRequiredDeliveryDate", TDCShipmentFormView.Row).Text = DateTime.Now.AddDays(1).ToString("d");

                            // After time
                            GetControl<TextBox>("txtAfterTime", TDCShipmentFormView.Row).Text = "00:00";

                            // Before time
                            GetControl<TextBox>("txtBeforeTime", TDCShipmentFormView.Row).Text = "00:00";
                            
                            // Check in time
                            GetControl<TextBox>("txtCheckInTime", TDCShipmentFormView.Row).Text = "0";
                            
                            // Vehicle max weight
                            GetControl<TextBox>("txtVehicleMaxWeight", TDCShipmentFormView.Row).Text = "0";
                            
                            // Status
                            DropDownList ddlStatus = GetControl<DropDownList>("ddlStatus", TDCShipmentFormView.Row);
                            
                            // Unselect any previously selected item
                            ddlStatus.SelectedIndex = -1;
                            
                            // Set to mapped
                            ddlStatus.Items.FindByValue(Shipment.StatusEnum.Mapped.ToString()).Selected = true;
                        }

                        // Generate ui validation controls
                        
                        // Check in time
                        Validation.AddValidation("txtCheckInTime", "CheckInTime");
                        // After time
                        Validation.AddValidation("txtAfterTime", "AfterTime");
                        // Before time
                        Validation.AddValidation("txtBeforeTime", "BeforeTime");

                        // Done
                        break;
                    }
            }

            // Call base implementation to render controls, etc
            base.SetValidation();
        }

        protected void ShipmentLineUpdated(object sender, System.EventArgs e)
        {
            // Lines have been updated, update the splits
            UpdateSplitLines();

            // Update line display
            UpdateShipmentLines();

            // Update display, volume and weights may have changed
            TDCShipmentFormView.DataBind();
        }

        protected void ShipmentLineAdded(object sender, System.EventArgs e)
        {
            // A line has been added, update the splits
            UpdateSplitLines();

            // Update line display
            UpdateShipmentLines();
        }

        private void UpdateShipmentLines()
        {
            // The shipment lines have been updated, we must update the shipment lines
            TDCShipmentLines shipmentLines = TDCShipmentFormView.Row.FindControl("ShipmentLines") as TDCShipmentLines;

            // Make sure we found the shipment lines split control
            Debug.Assert(shipmentLines != null);

            // Refresh the shipment lines
            shipmentLines.RefreshData();
        }

        private void UpdateSplitLines()
        {
            // The shipment lines have been updated, we must update the split lines
            TDCShipmentLinesSplit shipmentLinesSplit = TDCShipmentFormView.Row.FindControl("TDCShipmentLinesSplits") as TDCShipmentLinesSplit;

            // Make sure we found the shipment lines split control
            Debug.Assert(shipmentLinesSplit != null);

            // Refresh the shipment split lines
            shipmentLinesSplit.RefreshData();
        }

        protected void TDCShipmentLinesSplit_OnSaveComplete(object sender, System.EventArgs e)
        {
            // The order has been split, update lines, etc
            this.DataBind();
        }

        protected void btnOpCoShipmentDetail_Click(object sender, System.EventArgs e)
        {
            // Redirect to the tdc shipment page
            Response.Redirect(string.Concat("~/Shipments/OpCoShipment.aspx?Id=",
                        ((System.Web.UI.WebControls.IButtonControl)sender).CommandArgument));
        }

        protected void btnPrintTransConv_Click(object sender, System.EventArgs e)
        {
            try
            {
                // Print the shipment
                TDCShipmentController.GetShipment(Convert.ToInt32(((System.Web.UI.WebControls.IButtonControl)sender).CommandArgument)).PrintTransferConversionNote();

                // Display message
                ((DiscoveryPage)this.Page).DisplayMessage("The transfer/conversion note was printed successfully.", DiscoveryMessageType.Success);
            }
            catch (Exception ex)
            {
                // Display message
                ((DiscoveryPage)this.Page).DisplayMessage("There was an error printing the transfer/conversion note.", DiscoveryMessageType.Error);
            }
        }

        protected void btnPrintDelColl_Click(object sender, System.EventArgs e)
        {
            try
            {
                // Print the shipment
                TDCShipmentController.GetShipment(Convert.ToInt32(((System.Web.UI.WebControls.IButtonControl)sender).CommandArgument)).PrintDeliveryCollectionNote();

                // Display message
                ((DiscoveryPage)this.Page).DisplayMessage("The delivery/collection note was printed successfully.", DiscoveryMessageType.Success);
            }
            catch (Exception ex)
            {
                // Display message
                ((DiscoveryPage)this.Page).DisplayMessage("There was an error printing the delivery/collection note.", DiscoveryMessageType.Error);
            }
        }

        protected void btnCalcDeliveryDate_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            // We need to calculate the delivery date, use the textbox required date just in case it's been changed

        }

        protected void btnAuditDetail_Click(object sender, System.EventArgs e)
        {
            // Redirect to the details page
            Response.Redirect(string.Format("~/Admin/MessageAudit.aspx?Id={0}&{1}",
                        ((IButtonControl)sender).CommandArgument,
                         GenerateUrlReferrer(new string[] { string.Format("id={0}", Request.QueryString["Id"]) }) + 
                         Server.UrlEncode("&UrlReferrer=" + Request.QueryString["UrlReferrer"])));
        }

        protected void btnValidateAddress_Click(object sender, EventArgs e)
        {
            try
            {
                // Create an address that we need to validate
                Address addressToValidate = new Address();
                // Populate properties from ui
                addressToValidate.Line1 = GetControl<TextBox>("txtShipmentAddress1", PageFormView).Text;
                addressToValidate.Line2 = GetControl<TextBox>("txtShipmentAddress2", PageFormView).Text;
                addressToValidate.Line3 = GetControl<TextBox>("txtShipmentAddress3", PageFormView).Text;
                addressToValidate.Line4 = GetControl<TextBox>("txtShipmentAddress4", PageFormView).Text;
                addressToValidate.Line5 = GetControl<TextBox>("txtShipmentAddress5", PageFormView).Text;
                addressToValidate.PostCode = GetControl<TextBox>("txtShipmentAddressPostCode", PageFormView).Text;

                // Validate the address via address lookup provider, etc
                PAFAddress validatedAddress = AddressController.CheckAddress(addressToValidate, GetControl<TextBox>("txtShipmentAddressName", PageFormView).Text);

                // Update the ui with the validated address
                GetControl<TextBox>("txtPAFAddress1", PageFormView).Text = validatedAddress.Line1;
                GetControl<TextBox>("txtPAFAddress2", PageFormView).Text = validatedAddress.Line2;
                GetControl<TextBox>("txtPAFAddress3", PageFormView).Text = validatedAddress.Line3;
                GetControl<TextBox>("txtPAFAddress4", PageFormView).Text = validatedAddress.Line4;
                GetControl<TextBox>("txtPAFAddress5", PageFormView).Text = validatedAddress.Line5;
                GetControl<TextBox>("txtPAFAddressPostCode", PageFormView).Text = validatedAddress.PostCode;

                // Update the ui with the easting, northing, etc
                GetControl<TextBox>("txtPAFEasting", PageFormView).Text = validatedAddress.Easting.ToString();
                GetControl<TextBox>("txtPAFNorthing", PageFormView).Text = validatedAddress.Northing.ToString();
                GetControl<TextBox>("txtPAFDPS", PageFormView).Text = validatedAddress.DPS;
                GetControl<TextBox>("txtPAFStatus", PageFormView).Text = validatedAddress.Status.ToString();
                //set the value of a hidden control to save match
            }
            catch (Exception ex)
            {
                // Display message
                DisplayMessage(ex);
            }
        }

        protected void TDCShipmentFormView_DataBound(object sender, EventArgs e)
        {
            if (((FormView)sender).CurrentMode == FormViewMode.ReadOnly)
            {
                BusinessObjects.TDCShipment shipment = (BusinessObjects.TDCShipment)TDCShipmentFormView.DataItem;

                GetControl<ImageButton>("btnOpCoShipmentDetail", (FormView)sender).Visible = ((shipment.Type == Shipment.TypeEnum.OPCO &&
                                                                       HasRule(
                                                                           "Shipment: View OpCo Shipment Detail")));

                GetControl<ImageButton>("btnAuditDetail", (FormView)sender).Visible = (shipment.AuditId != Discovery.Utility.Null.NullInteger &&
                                                                          HasRule("Admin: View Message Audits"));

                // Hide the add line button if this is not an ad-hoc shipment
                GetControl<TDCShipmentLinesAdd>("TDCShipmentLinesAdditions", TDCShipmentFormView.Row).Visible = (shipment.Type == Shipment.TypeEnum.ADH || shipment.Type == Shipment.TypeEnum.WHS);

                // Make sure we have permission to print or that we support the type of delivery note
                GetControl<ImageButton>("btnPrintTransConv", (FormView)sender).Visible = (HasRule("Shipment: Print Orders") && shipment.IsTransferOrConversion && shipment.Status != Shipment.StatusEnum.Mapped);
                GetControl<ImageButton>("btnPrintDelColl", (FormView)sender).Visible = (HasRule("Shipment: Print Orders") && shipment.Status != Shipment.StatusEnum.Mapped);

                GetControl<ImageButton>("ButtonEdit", (FormView)sender).Visible = ((shipment.Status != Shipment.StatusEnum.Completed)
                                                                                   &&
                                                                                   (
                                                                                    (
                                                                                      shipment.Type ==
                                                                                      Shipment.TypeEnum.WHS &&
                                                                                      HasRule(
                                                                                          "Shipment: Edit Warehouse Order Detail")
                                                                                     )
                                                                                     ||
                                                                                     (
                                                                                          shipment.Type ==
                                                                                          Shipment.TypeEnum.ADH &&
                                                                                          HasRule(
                                                                                              "Shipment: Edit Ad-Hoc Order Detail")
                                                                                     )
                                                                                     ||
                                                                                     (
                                                                                          shipment.Type ==
                                                                                          Shipment.TypeEnum.OPCO &&
                                                                                          HasRule(
                                                                                              "Shipment: Edit TDC Shipment Detail")
                                                                                     )
                                                                                    )
                                                                                 );




            }
        }
    }
}
