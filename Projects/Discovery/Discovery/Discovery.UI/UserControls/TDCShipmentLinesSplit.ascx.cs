/*************************************************************************************************
 ** FILE:	TDCShipmentLinesSplit.aspx.cs
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
    public partial class TDCShipmentLinesSplit : System.Web.UI.UserControl
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

        public bool AlterSourceQuantities
        {
            get { return chkAlterSource.Checked; }
            set { chkAlterSource.Checked = value; }
        }

        public bool AlterWeightsAndVolumes
        {
            get { return chkAlterWeight.Checked; }
            set { chkAlterWeight.Checked = value; }
        }

        // The line we're editing
        //protected int SelectedItemIndex
        //{
        //    get
        //    {
        //        return (null == ViewState["SelectedIndex"]) ? -1 : (int)ViewState["SelectedIndex"];
        //    }
        //    set
        //    {
        //        ViewState["SelectedIndex"] = value;
        //    }
        //}

        public void RefreshData()
        {
            // Re-bind the data
            dataSourceShipmentLines.DataBind();
            // Re-bind the ui
            discoveryShipmentLines.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Hide the popup
            DisplayPopup(false);

            // See if we're enabled
            if (Enabled)
            {

                // Wire up item command for repeater
                discoveryShipmentLines.ItemCommand += new RepeaterCommandEventHandler(discoveryShipmentLines_ItemCommand);

                // Wire up selecting event for sorting
                dataSourceShipmentLines.Selecting += new ObjectDataSourceSelectingEventHandler(dataSourceShipmentLines_Selecting);
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

        public List<TDCLineSplit> GetLineSplits()
        {
            // Create an array of line splits
            List<TDCLineSplit> lineSplits = new List<TDCLineSplit>();

            try
            {
                // Generate the line splits, etc
                for (int i = 0; i < discoveryShipmentLines.Items.Count; i++)
                {
                    // The split quantity
                    int splitQuantity = 0;

                    // Get the repeater item
                    RepeaterItem repeaterItemLine = discoveryShipmentLines.Items[i];

                    // Find the line number (lblLineNumber)
                    Label lblLineNumber = repeaterItemLine.FindControl("lblLineNumber") as Label;
                    // Make sure we found the control
                    Debug.Assert(lblLineNumber != null);

                    // Find the new quantity (txtSplitQuantity)
                    TextBox txtSplitQuantity = repeaterItemLine.FindControl("txtSplitQuantity") as TextBox;
                    // Make sure we found the control
                    Debug.Assert(txtSplitQuantity != null);

                    try
                    {
                        splitQuantity = Convert.ToInt32(txtSplitQuantity.Text);
                    }
                    catch
                    {
                        splitQuantity = 0;
                    }

                    // Reset the text box value
                    txtSplitQuantity.Text = "0";

                    // See if we have a quantity
                    if (splitQuantity > 0)
                    {
                        // Create the split
                        TDCLineSplit lineSplit = new TDCLineSplit();
                        lineSplit.Line = Convert.ToInt32(lblLineNumber.Text);
                        lineSplit.Quantity = splitQuantity;

                        // Add the split to the collection
                        lineSplits.Add(lineSplit);
                    }
                }
                // Done
                return lineSplits;
            }
            catch (Exception ex)
            {
                // Done
                return null;
            }
        }

        protected void btnSplitConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                // Load the shipment we need to split
                Discovery.BusinessObjects.TDCShipment tdcShipment = TDCShipmentController.GetShipment(Convert.ToInt32(Request.QueryString["Id"]));

                // Reset the split shipment
                splitShipment = null;

                // Get the lines and quantities, etc and split the shipment
                splitShipment = tdcShipment.SplitShipment(
                            GetLineSplits(),
                            AlterSourceQuantities,
                            AlterWeightsAndVolumes,
                            Page.User.Identity.Name);

                // See if we've got something to do
                if (null != splitShipment)
                {
                    // Re-bind the data
                    dataSourceShipmentLines.DataBind();

                    // Update the UI
                    if (null != SaveComplete)
                    {
                        // Notify others that we've split the shipment
                        SaveComplete(this, new EventArgs());
                    }

                    // Display message
                    ((DiscoveryPage)Page).DisplayMessage(String.Format("Shipment {0}-{1} was split creating shipment <a href=\"TDCShipment.aspx?Id={4}\">{2}-{3}</a>.",
                                tdcShipment.ShipmentNumber,
                                tdcShipment.DespatchNumber,
                                splitShipment.ShipmentNumber,
                                splitShipment.DespatchNumber,
                                splitShipment.Id), DiscoveryMessageType.Success);
                }
                else
                {
                    // Display message
                    ((DiscoveryPage)Page).DisplayMessage("The shipment was <b>not</b> split as no quantities greater than 0 (zero) were specified.", DiscoveryMessageType.Information);
                }
            }
            catch (Exception ex)
            {
                // Display message
                ((DiscoveryPage)Page).DisplayMessage(String.Concat("There was a problem splitting the shipment.  ", ex.Message), DiscoveryMessageType.Error);

                // We failed to split the shipment
                splitShipment = null;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            // Call base
            base.OnPreRender(e);

                        // See if we're enabled
            if (Enabled)
            {
                // Generate the list of split field names
                List<String> splitFieldNames = new List<string>();
                foreach (RepeaterItem splitLine in discoveryShipmentLines.Items)
                {
                    // Find the quantity control
                    TextBox splitQuantity = splitLine.FindControl("txtSplitQuantity") as TextBox;
                    Debug.Assert(null != splitQuantity);
                    // Add it's name to the list
                    splitFieldNames.Add(splitQuantity.ClientID);
                }

                // Make sure that we have some lines
                if (splitFieldNames.Count > 0)
                {
                    // String builder for client script
                    StringBuilder sbClientScript = new StringBuilder();

                    // Add resetNumericFields javascript to the cancel client click
                    sbClientScript.Append("resetNumericFields(0");
                    foreach (String splitFieldName in splitFieldNames)
                    {
                        sbClientScript.AppendFormat(", {0}", splitFieldName);
                    }
                    sbClientScript.Append(");");
                    // Clear values on cancel
                    btnSplitCancel.OnClientClick = sbClientScript.ToString();

                    // Disable split button when submitted
                    String csName = "SplitShipmentOnSubmitScript" + this.ClientID;
                    Type csType = this.GetType();
                    // Get a ClientScriptManager reference from the Page class.
                    ClientScriptManager cs = Page.ClientScript;

                    // Check to see if the OnSubmit statement is already registered.
                    if (!cs.IsOnSubmitStatementRegistered(csType, csName))
                    {
                        // Disable the split button once clicked
                        //cs.RegisterOnSubmitStatement(csType, csName, String.Format("if (null == theForm.{0}) return true; if (typeof(ValidatorOnSubmit) == 'function' && ValidatorOnSubmit() == false) return false; theForm.{0}.disabled = true;", btnSplitConfirm.ClientID));
                    }

                    // Enable split button from main split button
                    //btnSplitShipment.OnClientClick = String.Format("{0}.disabled = false;", btnSplitConfirm.ClientID);
                }
            }

            // Enable/disable
            btnSplitShipment.Visible = this.Enabled && DiscoveryPage.HasRule("Shipment: Split Orders", Context.User);

        }

        public void DisplayPopup(bool show)
        {
            // Hide/show the popup
            if (show)
            {
                // Show the popup
                popupShipmentSplit.Show();
            }
            else
            {
                // Hide the popup
                popupShipmentSplit.Hide();
            }
        }
    }
}