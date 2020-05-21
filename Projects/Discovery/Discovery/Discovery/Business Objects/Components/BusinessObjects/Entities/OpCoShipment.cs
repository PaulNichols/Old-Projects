/*************************************************************************************************
 ** FILE:	OpCoShipment.cs
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
using System.Text;
using Discovery.ComponentServices.ExceptionHandling;
using Discovery.ComponentServices.Parsing;
using Discovery.ComponentServices.Mapping;
using Discovery.BusinessObjects.Controllers;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Discovery.BusinessObjects
{
    /// <summary>
    /// A Class 'OpCoShipment' which is an entity with namespace Discovery.BusinessObjects
    /// It is inherited from Shipment
    /// The class holds the OpCo shipment details
    /// </summary>
    [Serializable]
    public class OpCoShipment : Shipment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:OpCoShipment"/> class.
        /// </summary>
        public OpCoShipment()
            : base()
        {
        }

        /// <summary>
        /// Sets the parser schema.
        /// </summary>
        /// <param name="fields">The fields.</param>
        public static void SetParserSchema(TextFieldCollection fields)
        {
            // Clear fields
            fields.Clear();

            // Add fields for schema
            fields.Add(new TextField("MessageType", TypeCode.String, false));                       // Not used
            fields.Add(new TextField("OpCoCode", TypeCode.String, false));
            fields.Add(new TextField("OpCoSequenceNumber", TypeCode.String, false));
            fields.Add(new TextField("ShipmentNumber", TypeCode.String, false));
            fields.Add(new TextField("DespatchNumber", TypeCode.String, false));
            fields.Add(new TextField("Amendment", TypeCode.Boolean, false));
            fields.Add(new TextField("OpCoHeld", TypeCode.Boolean, false));
            fields.Add(new TextField("RequiredShipmentDate", TypeCode.DateTime, false));
            fields.Add(new TextField("OpCoContact.Name", TypeCode.String, false));
            fields.Add(new TextField("TransactionTypeCode", TypeCode.String, false));
            fields.Add(new TextField("TransactionSubTypeCode", TypeCode.String, false));            // Not used
            fields.Add(new TextField("CustomerReference", TypeCode.String, false));
            fields.Add(new TextField("Instructions", TypeCode.String, false));
            fields.Add(new TextField("RouteCode", TypeCode.String, false));
            fields.Add(new TextField("CustomerNumber", TypeCode.String, false));
            fields.Add(new TextField("StockWarehouseCode", TypeCode.String, false));
            fields.Add(new TextField("SalesBranchCode", TypeCode.String, false));
            fields.Add(new TextField("CustomerName", TypeCode.String, false));
            fields.Add(new TextField("CustomerAddress.Line1", TypeCode.String, false));
            fields.Add(new TextField("CustomerAddress.Line2", TypeCode.String, false));
            fields.Add(new TextField("CustomerAddress.Line3", TypeCode.String, false));
            fields.Add(new TextField("CustomerAddress.Line4", TypeCode.String, false));
            fields.Add(new TextField("CustomerAddress.Line5", TypeCode.String, false));
            fields.Add(new TextField("CustomerAddress.PostCode", TypeCode.String, false));
            fields.Add(new TextField("ShipmentName", TypeCode.String, false));
            fields.Add(new TextField("ShipmentAddress.Line1", TypeCode.String, false));
            fields.Add(new TextField("ShipmentAddress.Line2", TypeCode.String, false));
            fields.Add(new TextField("ShipmentAddress.Line3", TypeCode.String, false));
            fields.Add(new TextField("ShipmentAddress.Line4", TypeCode.String, false));
            fields.Add(new TextField("ShipmentAddress.Line5", TypeCode.String, false));
            fields.Add(new TextField("ShipmentAddress.PostCode", TypeCode.String, false));
            fields.Add(new TextField("ShipmentContact.Name", TypeCode.String, false));
            fields.Add(new TextField("ShipmentContact.TelephoneNumber", TypeCode.String, false));
            fields.Add(new TextField("ShipmentContact.Email", TypeCode.String, false));
            fields.Add(new TextField("AfterTime", TypeCode.String, false));
            fields.Add(new TextField("BeforeTime", TypeCode.String, false));
            fields.Add(new TextField("TailLiftRequired", TypeCode.Boolean, false));
            fields.Add(new TextField("VehicleMaxWeight", TypeCode.Decimal, false));
            fields.Add(new TextField("CheckInTimeHHMMSS", TypeCode.String, false));
            fields.Add(new TextField("DeliveryWarehouseCode", TypeCode.String, false));
            fields.Add(new TextField("OpCoContact.Email", TypeCode.String, false));
            fields.Add(new TextField("DivisionCode", TypeCode.String, false));
            fields.Add(new TextField("GeneratedDateTime", TypeCode.DateTime, false));
            fields.Add(new TextField("GeneratedTimeHHMMSS", TypeCode.String, false));
        }

        /// <summary>
        /// Maps to TDC.
        /// </summary>
        /// <returns></returns>
        public TDCShipment MapToTDC(TDCShipment existingTDCShipment, string updatedBy, bool raiseAddressException)
        {
            try
            {
                // Create an instance of a tdc shipment
                TDCShipment tdcShipment = new TDCShipment();

                // Map the opco shipment to a tdc shipment
                Mapper.Map(this, tdcShipment, this.OpCoCode, "TDC", null, new string[] { "Id" });

                // Update the opco shipment id in the tdc shipment
                tdcShipment.OpCoShipmentId = this.Id;

                // Map the opcoshipmentlines to tdcshipment lines via the mapper class
                List<ShipmentLine> tdcShipmentLines = new List<ShipmentLine>(this.ShipmentLines.Count);

                // Map the lines from opcoshipmentlines to tdcshipmentlines
                foreach (ShipmentLine opCoShipmentLine in this.ShipmentLines)
                {
                    // The new tdc shipment line
                    TDCShipmentLine tdcShipmentLine = new TDCShipmentLine();
                    // Map the opco shipment to a tdc shipment
                    Mapper.Map(opCoShipmentLine, tdcShipmentLine, this.OpCoCode, "TDC", null, new string[] { "Id" });

                    //store the original quantity for later validation which stops the user from
                    //increasing the quantity past this original quantity.
                    tdcShipmentLine.OriginalQuantity = tdcShipmentLine.Quantity;

                    // Add the tdcshipmentline to the tdcshipmentlines list
                    tdcShipmentLines.Add(tdcShipmentLine);
                }

                // Store the new tdc shipment lines in the tdcshipment
                tdcShipment.ShipmentLines = tdcShipmentLines;

                // ********************************
                // ** UPDATE EXISTING TDC SHIPMENT
                // ********************************
                if (null != existingTDCShipment)
                {
                    // Update the existing tdc shipment with the one just mapped
                    tdcShipment = (TDCShipment)existingTDCShipment.UpdateFromShipment(tdcShipment);
                }

                // Update the status of the tdc shipment to mapped
                tdcShipment.Status = StatusEnum.Mapped;

                // See if the tdc shipment has been cancelled
                if (0 == tdcShipment.TotalLineQuantity)
                {
                    // Cancel the shipment
                    tdcShipment.Status = Shipment.StatusEnum.Cancelled;
                }

                // If the tdc shipment is valid and not cancelled, execute remaining business logic
                if (tdcShipment.IsValid)
                {
                    // *********************************************************************************
                    // ** Tec Spec 5.3, Calculate the estimated delivery date of the tdc shipment
                    // *********************************************************************************
                    tdcShipment.CalculateDeliveryDate();

                    // *********************************************************************************
                    // ** Tec Spec 5.4, Check the address
                    // *********************************************************************************
                    tdcShipment.CheckAddress();

                    // Specify that the shipment was update
                    tdcShipment.UpdatedBy = updatedBy;

                    // Save the tdc shipment
                    TDCShipmentController.SaveShipment(tdcShipment);

                    //if an invalid address should be logged or emailed then call the NotifyIfPoorAddressMatch method.
                    //This method will check if there was a poor match an pass an exception to the exception handling
                    if (raiseAddressException) tdcShipment.NotifyIfPoorAddressMatch();
                   

                    // ********************************
                    // ** UPDATE THIS OPCO SHIPMENT
                    // ********************************

                    // Update the opco shipment status if the shipment is NOTMAPPED
                    if (this.Status == Shipment.StatusEnum.NotMapped)
                    {
                        // Update the status of the opco shipment to mapped
                        this.Status = Shipment.StatusEnum.Mapped;

                        // Update the opco shipment status
                        OpCoShipmentController.UpdateShipmentStatus(this);
                    }
                }
                else
                {
                    // The tdc shipment was is invalid
                    throw new InValidBusinessObjectException(tdcShipment);
                }

                // Return the new tdc shipment
                return tdcShipment;
            }
            catch (Exception ex)
            {
                // Generate a new exception
                ex = new FailedShipmentMappingException(
                        this.OpCoCode,
                        this.OpCoContact.Email,
                        string.Format("Failed to map OpCo shipment {0} - {1} for OpCo {2}.  {3}",
                        this.ShipmentNumber,
                        this.DespatchNumber,
                        this.OpCoCode,
                        ex.Message));

                // Log an throw if configured to do so
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw ex;

                // We failed to save the shipment
                return null;
            }
        }
    }
}
