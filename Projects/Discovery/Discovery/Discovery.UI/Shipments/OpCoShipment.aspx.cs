/*************************************************************************************************
 ** FILE:	OpCoShipment.aspx.cs
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
using Discovery.BusinessObjects;
using Discovery.BusinessObjects.Controllers;
using Discovery.Web.UI.CustomControls;

namespace Discovery.UI.Web.Shipments
{
    /*************************************************************************************************
  ** CLASS:	OptrakRegions
  **
  ** OVERVIEW:
  ** This page allows a uers to view, add, edit and delete a single OptrakRegion
  **
  ** MODIFICATION HISTORY:
  **
  ** Date:		Version:    Who:	Change:
  ** 19/7/06		1.0			PJN		Initial Version
  ************************************************************************************************/
    public partial class OpCoShipment : DiscoveryDataDetailPage
    {
        #region Properties



        #endregion

        #region Protected Methods


        #endregion

        #region Events

        #endregion


        protected override void Page_Load(object sender, System.EventArgs e)
        {
            CreateRule = "Shipment: Edit OpCo Shipment Detail";
            ReadRule = "Shipment: View OpCo Shipment Detail";
            UpdateRule = "Shipment: Edit OpCo Shipment Detail";
            DeleteRule = "Shipment: Edit OpCo Shipment Detail";

            // Call base
            base.Page_Load(sender, e);

            // Update the display
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            // Work out what mode we're in
            switch (OpCoShipmentFormView.CurrentMode)
            {
                case FormViewMode.ReadOnly:
                    {
                        // Hide the paf address
                        //GetControl<System.Web.UI.HtmlControls.HtmlTable>("tablePAFAddress", OpCoShipmentFormView).Visible = false;
                        // Hide the paf details
                        //GetControl<System.Web.UI.HtmlControls.HtmlTable>("tablePAFDetails", OpCoShipmentFormView).Visible = false;
                        // Done
                        //  GetControl<ImageButton>("btnPrintShipment").Visible = (HasRule("Shipment: Print Orders"));



                        break;
                    }
            }
        }

        protected override void SetValidation()
        {
            base.SetValidation();
            //validation.AddValidation("TextBoxCode", "Code");
            //validation.AddValidation("TextBoxDescription", "Description");
        }

        protected void btnTDCShipmentDetails_Click(object sender, System.EventArgs e)
        {
            // Get the opco code, shipment number and the despatch number
            string[] shipmentParams = ((System.Web.UI.WebControls.IButtonControl)sender).CommandArgument.Split('|');

            // Get the tdc shipment
            Discovery.BusinessObjects.TDCShipment tdcShipment = TDCShipmentController.GetShipment(shipmentParams[0], shipmentParams[1], shipmentParams[2]);
            if (null != tdcShipment)
            {
                // Redirect to the tdc shipment page
                Response.Redirect(string.Concat("~/Shipments/TDCShipment.aspx?Id=", tdcShipment.Id));
            }
        }

        protected void btnAuditDetail_Click(object sender, System.EventArgs e)
        {
            // Redirect to the details page
            Response.Redirect(string.Format("~/Admin/MessageAudit.aspx?Id={0}&{1}",
                        ((System.Web.UI.WebControls.IButtonControl)sender).CommandArgument,
                        GenerateUrlReferrer(new string[] { string.Format("id={0}", Request.QueryString["Id"]) })));
        }

        protected void btnMapToTdc_Click(object sender, System.EventArgs e)
        {
            // Get the opco shipment
            Discovery.BusinessObjects.OpCoShipment opCoShipment = OpCoShipmentController.GetShipment(Convert.ToInt32((sender as System.Web.UI.WebControls.IButtonControl).CommandArgument));
            // Make sure that we found the shipment
            if (null != opCoShipment && opCoShipment.Id != -1)
            {
                // We have the shipment, now map it to a tdc shipment if not mapped
                if (opCoShipment.Status == Shipment.StatusEnum.NotMapped)
                {
                    // The new shipment
                    Discovery.BusinessObjects.TDCShipment tdcShipment;

                    try
                    {
                        // Map opco shipment
                        tdcShipment = opCoShipment.MapToTDC(null, Page.User.Identity.Name,false);

                        // Update the status of the opco shipment to mapped
                        opCoShipment.Status = Shipment.StatusEnum.Mapped;

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
                    OpCoShipmentFormView.DataBind();
                }
            }
        }

        protected void OpCoShipmentFormView_OnDataBound(object sender, EventArgs e)
        {
            // Work out what mode we're in
            switch (OpCoShipmentFormView.CurrentMode)
            {
                case System.Web.UI.WebControls.FormViewMode.ReadOnly:
                    {
                        // Hide the paf address
                        //GetControl<System.Web.UI.HtmlControls.HtmlTable>("tablePAFAddress", OpCoShipmentFormView).Visible = false;
                        // Hide the paf details
                        //GetControl<System.Web.UI.HtmlControls.HtmlTable>("tablePAFDetails", OpCoShipmentFormView).Visible = false;
                        // Done
                        //  GetControl<ImageButton>("btnPrintShipment").Visible = (HasRule("Shipment: Print Orders"));

                        GetControl<ImageButton>("btnTDCShipmentDetails", OpCoShipmentFormView).Visible =
                            HasRule("Shipment: View TDC Shipment Detail", Context.User);

                        BusinessObjects.OpCoShipment shipment = (BusinessObjects.OpCoShipment)((FormView)sender).DataItem;
                        GetControl<ImageButton>("btnAuditDetail", OpCoShipmentFormView).Visible = shipment.AuditId != Discovery.Utility.Null.NullInteger &&
                            HasRule("Admin: View Message Audits", Context.User);

                        break;
                    }
            }
        }
    }
}