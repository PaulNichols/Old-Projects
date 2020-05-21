/*************************************************************************************************
 ** FILE:	TDCShipmentLinesAdd.aspx.cs
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
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Diagnostics;
using AjaxControlToolkit;
using Discovery.BusinessObjects;
using Discovery.BusinessObjects.Controllers;
using Discovery.Utility;

namespace Discovery.UI.Web.UserControls
{
    public partial class TDCShipmentLinesAdd : System.Web.UI.UserControl
    {
        // Event that is fired if we update a shipment line
        public event EventHandler SaveComplete;

        public bool Enabled
        {
            get
            {
                if (null != ViewState["Enabled"])
                {
                    return (bool)ViewState["Enabled"];
                }
                return true;
            }
            set
            {
                ViewState["Enabled"] = value;
            }
        }

        // The shipment lines we're displaying, etc
        private List<Discovery.BusinessObjects.TDCShipmentLine> currentLines = null;

        // The newly created split shipment
        private TDCShipment splitShipment = null;

        public TDCShipment SplitShipment
        {
            get { return splitShipment; }
            set { splitShipment = value; }
        }

        protected override void OnPreRender(EventArgs e)
        {
            // See if we're enabled
            if (Enabled)
            {
                // Seed values for next use
                txtProductCode.Text = Request.QueryString["Type"];
                txtQuantity.Text = "1";
                txtConversionQuantity.Text = "0";
                txtNetWeight.Text = "0";
                txtWidth.Text = "0";
                txtLength.Text = "0";
                txtVolume.Text = "0";
                txtMicrons.Text = "0";
                txtGrammage.Text = "0";
                ddlLoadCategory.SelectedValue = "NA";
            }

            // Call base
            base.OnPreRender(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Enable/disable
            btnAddLinePopup.Enabled = this.Enabled;

            // Hide the popup
            DisplayPopup(false);
        }

        protected void btnAddLine_Click(object sender, EventArgs e)
        {
            try
            {
                // Load the shipment we're adding the line to
                Discovery.BusinessObjects.TDCShipment tdcShipment = TDCShipmentController.GetShipment(
                            Convert.ToInt32(Request.QueryString["Id"]));

                // Create a new shipment line
                TDCShipmentLine tdcLine = new TDCShipmentLine();

                tdcLine.Id = -1;
                tdcLine.ShipmentId = tdcShipment.Id;
                tdcLine.UpdatedBy = Page.User.Identity.Name;
                tdcLine.ConversionInstructions = txtConversionInstructions.Text;
                tdcLine.ConversionQuantity = Convert.ToInt32(txtConversionQuantity.Text);
                tdcLine.CustomerReference = txtCustomerReference.Text;
                tdcLine.Description1 = txtDescription1.Text;
                tdcLine.Description2 = txtDescription2.Text;
                tdcLine.Grammage = Convert.ToInt32(txtGrammage.Text);
                tdcLine.NetWeight = Convert.ToDecimal(txtNetWeight.Text);
                tdcLine.IsISO9000Approved = chkIsISO9000Approved.Checked;
                tdcLine.IsPanel = chkIsPanel.Checked;
                tdcLine.Length = Convert.ToInt32(txtLength.Text);
                tdcLine.LineNumber = tdcShipment.ShipmentLines.Count + 1;
                tdcLine.LoadCategoryCode = ddlLoadCategory.SelectedValue;
                tdcLine.Microns = Convert.ToInt32(txtMicrons.Text);
                tdcLine.Packing = txtPacking.Text;
                tdcLine.ProductCode = tdcShipment.Type.ToString();
                tdcLine.ProductGroup = txtProductGroup.Text;
                tdcLine.Quantity = Convert.ToInt32(txtQuantity.Text);
                tdcLine.QuantityUnit = txtQuantityUnit.Text;
                tdcLine.Volume = Convert.ToDecimal(txtVolume.Text);
                tdcLine.Width = Convert.ToInt32(txtWidth.Text);

                // Add the shipment line to the shipment
                TDCShipmentLineController.SaveLine(tdcLine);

                // Update the UI
                if (null != SaveComplete)
                {
                    // Notify others that we've split the shipment
                    SaveComplete(this, new EventArgs());
                }

                // Display message
                ((DiscoveryPage)Page).DisplayMessage(String.Format("Line #{0} product code {1} was added to shipment {2}.", tdcLine.LineNumber, tdcLine.ProductCode, tdcShipment.ShipmentNumber), DiscoveryMessageType.Success);
            }
            catch (Exception ex)
            {
                // Display message
                ((DiscoveryPage)Page).DisplayMessage(String.Concat("There was an error saving the shipment line.  ", ex.Message), DiscoveryMessageType.Error);
            }
        }

        public void DisplayPopup(bool show)
        {
            // Hide/show the popup
            if (show)
            {
                // Show the popup
                popupShipmentAdd.Show();
            }
            else
            {
                // Hide the popup
                popupShipmentAdd.Hide();
            }
        }
    }
}