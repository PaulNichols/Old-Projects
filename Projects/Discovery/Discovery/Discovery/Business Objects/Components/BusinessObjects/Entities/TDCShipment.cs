/*************************************************************************************************
 ** FILE:	TDCShipment.cs
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
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Transactions;
using System.Diagnostics;
using Discovery.BusinessObjects.Controllers;
using Discovery.ComponentServices.ExceptionHandling;
using Discovery.Utility;
using Discovery.ComponentServices.Mapping;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using ValidationFramework;

namespace Discovery.BusinessObjects
{
    /// <summary>
    /// A Class 'TDCShipment' which is an entity with namespace Discovery.BusinessObjects
    /// It is inherited from Shipment
    /// The class holds the TDC shipment details
    /// </summary>
    /// 

    [Serializable]
    public partial class TDCShipment : Shipment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:TDCShipment"/> class.
        /// </summary>
        public TDCShipment()
            : base()
        {
            // We need to create an instance of the PAF address
            pafAddress = new PAFAddress();

            // Seed all nested objects to null
            salesLocation = null;                       //
            division = null;                            //
            transactionSubType = null;                  //
            transactionSubType = null;                  //
            route = null;                               //
            stockWarehouse = null;                      //
            deliveryWarehouse = null;                   //

            // See defaults
            isRecurring = false;                        //
            isValidAddress = false;                     //
            isSplit = false;                            //
            opcoShipmentId = Null.NullInteger;          //
            splitSequence = 0;                          //
            transactionSubTypeCode = Null.NullString;   //
            locationCode = Null.NullString;             //
            opcoShipmentId = Null.NullInteger;          //
            sentToWMS = Null.NullDate;                  //
            estimatedDeliveryDate = Null.NullDate;      //
            actualDeliveryDate = Null.NullDate;         //
            routingDateTime = Null.NullDate;            //
            routeTrip = Null.NullString;                //
            routeDrop = 0;                              //

            // Add custom validation method
            this.Validator.CustomValidation += new CustomValidationDelegate(ValidateShipmentType);
        }

        /// <summary>
        /// Indicates if this shipment is a transfer or conversion
        /// </summary>
        public bool IsTransferOrConversion
        {
            get
            {
                // *****************************************
                // Management Server Transfer and Delivery 
                // Notes addendum document re: Graham Bell,
                // Interpretation of Requirements 6.1.3 (ver 3) 
                // 20/01/2007
                // *****************************************

                // We have a transaction type, check the stock warehouse code
                if (!transactionType.IsCollection)
                {
                    // None of these should be null as we shouldn't have been able to save the entity
                    Debug.Assert(
                        null != StockWarehouse &&
                        null != TransactionType &&
                        null != TransactionSubType,
                        "Shipment # " + this.ShipmentNumber + " IsTransferOrConversion: StockWarehouse, TransactionType or TransactionSubType is null.");

                    // Make sure the stock warehouse is a TDC warehouse
                    if (StockWarehouse.IsTDC && TransactionType.IsStock)
                    {
                        // *****************************************
                        // STOCK, NORMAL, "Ex-Branch"
                        // *****************************************
                        if (TransactionSubType.IsNormal &&
                            OpCoCode == "TPC" &&
                            (StockWarehouseCode != DeliveryWarehouseCode)) return true;

                        // *****************************************
                        // STOCK, TRANSFER, "Transfer Note"
                        // *****************************************
                        if (TransactionSubType.IsTransfer &&
                            (StockWarehouseCode != DeliveryWarehouseCode)) return true;

                        // *****************************************
                        // STOCK, CONV3RDPARTY CONVLOCAL, "Conversion Note"
                        // *****************************************
                        if (TransactionSubType.Is3rdPartyConversion ||
                            TransactionSubType.IsLocalConversion) return true;
                    }
                }

                // No transfer or conversion note
                return false;
            }
        }

        /// <summary>
        /// Report type
        /// </summary>
        public enum ReportTypeEnum
        {
            DeliveryNote = 0,           // Print in delivery warehouse location
            TransferNote = 2,           // Print in stock warehouse location
            ConversionNote = 4,         // Print in stock warehouse location
            ExBranchSalesOrder = 8      // Print in stock warehouse location
        }

        /// <summary>
        /// Order Status
        /// </summary>
        [CustomValidator("", "ValidStatus", "*")]
        public override Shipment.StatusEnum Status
        {
            get { return status; }
            set { status = value; }
        }

        /// <summary>
        /// Custom validation method to validate division code
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:ValidationFramework.CustomValidationEventArgs"/> instance containing the event data.</param>
        public void ValidStatus(object sender, CustomValidationEventArgs e)
        {
            // Seed to a valid status
            e.IsValid = true;

            // Seed the previous status
            Shipment.StatusEnum previousStatus = Status;

            // See if we have an id, if so load previous status from the DB
            if (-1 != Id)
            {
                // Get the existing status as it exists in the db
                previousStatus = TDCShipmentController.GetShipmentStatus(Id);
            }

            // Make sure that we've transitioned the status correctly
            if (previousStatus != Status)
            {
                // Make sure that the status is valid
                switch (Status)
                {
                    case Shipment.StatusEnum.NotMapped:
                        {
                            // Not valid
                            e.IsValid = false;
                            e.ErrorMessage = string.Format("Cannot transition shipment from status {0} to status {1}.", previousStatus.ToString(), Status.ToString());
                            // Invalid
                            return;
                        }

                    case Shipment.StatusEnum.Mapped:
                        {
                            // Check previous status
                            if (previousStatus != StatusEnum.NotMapped &&
                                previousStatus != StatusEnum.Mapped &&
                                previousStatus != StatusEnum.Held &&
                                previousStatus != StatusEnum.Routing &&
                                previousStatus != StatusEnum.Routed)
                            {
                                // Not valid
                                e.IsValid = false;
                                e.ErrorMessage = string.Format("Cannot transition shipment from status {0} to status {1}.", previousStatus.ToString(), Status.ToString());
                                // Invalid
                                return;
                            }
                            // Done
                            break;
                        }

                    case Shipment.StatusEnum.Held:
                        {
                            // Check previous status
                            if (previousStatus != StatusEnum.Mapped &&
                                previousStatus != StatusEnum.NotMapped)
                            {
                                // Not valid
                                e.IsValid = false;
                                e.ErrorMessage = string.Format("Cannot transition shipment from status {0} to status {1}.", previousStatus.ToString(), Status.ToString());
                                // Invalid
                                return;
                            }
                            // Done
                            break;
                        }

                    case Shipment.StatusEnum.Routing:
                        {
                            // Check previous status
                            if (previousStatus != StatusEnum.Mapped &&
                                previousStatus != StatusEnum.Held)
                            {
                                // Not valid
                                e.IsValid = false;
                                e.ErrorMessage = string.Format("Cannot transition shipment from status {0} to status {1}.", previousStatus.ToString(), Status.ToString());
                                // Invalid
                                return;
                            }
                            // Done
                            break;
                        }

                    case Shipment.StatusEnum.Routed:
                        {
                            // Check previous status
                            if (previousStatus != StatusEnum.Mapped &&
                                previousStatus != StatusEnum.Held &&
                                previousStatus != StatusEnum.Routing)
                            {
                                // Not valid
                                e.IsValid = false;
                                e.ErrorMessage = string.Format("Cannot transition shipment from status {0} to status {1}.", previousStatus.ToString(), Status.ToString());
                                // Invalid
                                return;
                            }
                            // Done
                            break;
                        }

                    case Shipment.StatusEnum.Completed:
                        {
                            // Check previous status
                            if (previousStatus != StatusEnum.Mapped &&
                                previousStatus != StatusEnum.Held &&
                                previousStatus != StatusEnum.Routing &&
                                previousStatus != StatusEnum.Routed)
                            {
                                // Not valid
                                e.IsValid = false;
                                e.ErrorMessage = string.Format("Cannot transition shipment from status {0} to status {1}.", previousStatus.ToString(), Status.ToString());
                                // Invalid
                                return;
                            }
                            // Done
                            break;
                        }
                }
            }
        }

        /// <summary>
        /// Verifies the shipment type based on transaction type, sub type and route code
        /// </summary>
        private void ValidateShipmentType(
                object sender,                      // Sender of validation
                CustomValidationEventArgs e)        // Customer validation event argument
        {
            // We need to work out if we have to validate the shipment type again.
            // If this is a new shipment or Route Code, Transaction Type, Transaction Sub Type, 
            // Stock Warehouse or Delivery Warehouse has changed do the check.

            // Seed to valid
            if (null != e) e.IsValid = true;

            // See if it's a new shipment
            if (-1 == Id)
            {
                // Call overloaded validate shipment type to update status and print notes
                ValidateShipmentType(sender, e, true, true, true);
            }
            else
            {
                // Load previous values to see if anything has changed
                Hashtable typeData = TDCShipmentController.GetShipmentTypeData(Id);

                // See if we need to validate the shipment type
                if (typeData.Count == 0 ||
                    StockWarehouseCode != typeData["StockWarehouseCode"].ToString() ||
                    DeliveryWarehouseCode != typeData["DeliveryWarehouseCode"].ToString() ||
                    TransactionTypeCode != typeData["TransactionTypeCode"].ToString() ||
                    TransactionSubTypeCode != typeData["TransactionSubTypeCode"].ToString() ||
                    RouteCode != typeData["RouteCode"].ToString())
                {
                    // Call overloaded validate shipment type to update status and print notes
                    ValidateShipmentType(sender, e, true, true, true);
                }
            }
        }

        /// <summary>
        /// Verifies the shipment type based on transaction type, sub type and route code
        /// </summary>
        // *********************************************************************************
        // ** Tec Spec 5.5, 5.6, 5.7, verify shipment type and print notes
        // *********************************************************************************
        private void ValidateShipmentType(
                object sender,                      // Sender of validation
                CustomValidationEventArgs e,        // Customer validation event argument
                bool updateStatus,                  // Do we update the shipment status?
                bool printTransferConversionNote,   // Do we print transfer and conversion notes?
                bool printDeliveryCollectionNote)   // Do we print delivery and collection notes?
        {
            // First look at the transaction type
            if (TransactionType.IsStock)
            {
                // ****************************************************************
                // ** Transaction type is stock
                // ****************************************************************

                if (TransactionSubType.IsNormal)
                {
                    // *********************************
                    // ** Transaction sub type is normal
                    // *********************************

                    // See if the delivery warehouse is a tdc warehouse
                    if (DeliveryWarehouse.IsTDC)
                    {
                        // S1, S2, S4
                        if (Route.IsCollection && printDeliveryCollectionNote)
                        {
                            // Print 1 "Delivery Note" in DeliveryWarehouseCode
                            Print(1, ReportTypeEnum.DeliveryNote);

                            // See if we need to update the status
                            if (updateStatus && Status != StatusEnum.Routed)
                            {
                                // Set status to routed
                                Status = StatusEnum.Routed;

                                // Generate the trip and drop
                            }
                        }
                        else if (Route.IsSpecial && printDeliveryCollectionNote)
                        {
                            // Print 2 "Delivery Note" in DeliveryWarehouseCode
                            Print(2, ReportTypeEnum.DeliveryNote);

                            // See if we need to update the status
                            if (updateStatus && Status != StatusEnum.Routed)
                            {
                                // Set status to routed
                                Status = StatusEnum.Routed;

                                // Generate the trip and drop
                            }
                        }
                        else if (Status != StatusEnum.Mapped)
                        {
                            // Print 1 "Delivery Note" in DeliveryWarehouseCode (MANUAL PRINT!!!)
                            Print(1, ReportTypeEnum.DeliveryNote);
                        }
                    }
                    else if (updateStatus)
                    {
                        // S3
                        // Set shipment status to complete
                        Status = StatusEnum.Completed;
                    }

                    // See if the stock and delivery warehouse are diffirent
                    if (StockWarehouseCode != DeliveryWarehouseCode)
                    {
                        // See if the stock warehouse is a tdc warehouse
                        if (StockWarehouse.IsTDC)
                        {
                            if (OpCoCode == "TPC" && printTransferConversionNote)
                            {
                                // S2, S3
                                // Print 1 "Ex-Branch Sales Order" in StockWarehouseCode
                                Print(1, ReportTypeEnum.ExBranchSalesOrder);
                            }
                        }
                        else if (updateStatus)
                        {
                            // S4
                            // Merchant system would have printed it's own transfer note
                        }
                    }
                }
                else if (TransactionSubType.IsTransfer)
                {
                    // ********************************
                    // ** Transaction sub type is transfer
                    // ********************************

                    if (StockWarehouseCode != DeliveryWarehouseCode)
                    {
                        // See if the stock warehouse is a tdc warehouse
                        if (StockWarehouse.IsTDC)
                        {
                            // Can we update the status
                            if (updateStatus)
                            {
                                // S5, S6
                                // Set shipment status to complete
                                Status = StatusEnum.Completed;
                            }

                            // Can we print the transfer note
                            if (printTransferConversionNote)
                            {
                                // S5, S6
                                // Print 1 "Transfer Note" StockWarehouseCode
                                Print(1, ReportTypeEnum.TransferNote);
                            }
                        }
                        else if (null != e)
                        {
                            // **Error**  We should not receive this message
                            e.IsValid = false;
                            e.ErrorMessage = string.Format("Stocking location '{0}' is a non TDC warehouse and the transaction sub type '{1}' is a transfer.", StockWarehouseCode, TransactionSubTypeCode);
                        }
                    }
                    else if (null != e)
                    {
                        // **Error** It's a transfer but same delivery and stock warehouse codes
                        e.IsValid = false;
                        e.ErrorMessage = string.Format("Tranbsaction type '{0}' is a transfer but the stocking ('{1}') and delivery ('{2}') warehouse are the same.", TransactionSubTypeCode, StockWarehouseCode, DeliveryWarehouseCode);
                    }
                }
                else if (TransactionSubType.Is3rdPartyConversion || TransactionSubType.IsLocalConversion)
                {
                    // ********************************
                    // ** Transaction sub type is 3rd Party Conversion or Local Conversion
                    // ********************************

                    // S8, S9
                    // See if the stock warehouse is a tdc warehouse
                    if (StockWarehouse.IsTDC)
                    {
                        // See if we update the status
                        if (updateStatus)
                        {
                            // S8, S9, S10
                            // Set shipment status to held
                            Status = StatusEnum.Held;
                        }

                        // See if we print the conversion note
                        if (printTransferConversionNote)
                        {
                            // S8, S9, S10
                            // Print 1 "Conversion Note" in StockWarehouseCode
                            Print(1, ReportTypeEnum.ConversionNote);

                            if (StockWarehouseCode != DeliveryWarehouseCode)
                            {
                                if (OpCoCode == "TPC")
                                {
                                    // S9, S10
                                    // Print 1 "Ex-Branch Sales Order" in StockWarehouseCode
                                    Print(1, ReportTypeEnum.ExBranchSalesOrder);
                                }
                            }
                        }

                        // S8, S9, S11
                        // See if the delivery warehouse is a tdc warehouse
                        if (DeliveryWarehouse.IsTDC)
                        {
                            // See we need to print delivery note
                            if (Route.IsCollection && printDeliveryCollectionNote)
                            {
                                // Print 1 "Delivery Note" in DeliveryWarehouseCode
                                Print(1, ReportTypeEnum.DeliveryNote);
                            }
                            else if (Route.IsSpecial && printDeliveryCollectionNote)
                            {
                                // Print 2 "Delivery Note" in DeliveryWarehouseCode
                                Print(2, ReportTypeEnum.DeliveryNote);
                            }
                            else if (Status != StatusEnum.Mapped)
                            {
                                // Print 1 "Delivery Note" in DeliveryWarehouseCode (MANUAL PRINT!!!)
                                Print(1, ReportTypeEnum.DeliveryNote);
                            }
                        }
                        else if (updateStatus)
                        {
                            // S10
                            // Set shipment status to complete
                            Status = StatusEnum.Held;
                        }
                    }
                    else
                    {
                        // S11
                        // Merchant system used to print conversion note
                    }
                }
            }
            else if (TransactionType.IsNonStock)
            {
                // ****************************************************************
                // ** Transaction type is non stock
                // ****************************************************************

                if (TransactionSubType.IsNormal)
                {
                    // ********************************
                    // ** Transaction sub type is normal
                    // ********************************

                    // S12, S13, S15
                    // See if the delivery warehouse is a tdc warehouse
                    if (DeliveryWarehouse.IsTDC)
                    {
                        // See we need to print delivery note
                        if (Route.IsCollection && printDeliveryCollectionNote)
                        {
                            // Print 1 "Delivery Note" in DeliveryWarehouseCode
                            Print(1, ReportTypeEnum.DeliveryNote);
                        }
                        else if (Route.IsSpecial && printDeliveryCollectionNote)
                        {
                            // Print 2 "Delivery Note" in DeliveryWarehouseCode
                            Print(2, ReportTypeEnum.DeliveryNote);
                        }
                        else if (Status != StatusEnum.Mapped)
                        {
                            // Print 1 "Delivery Note" in DeliveryWarehouseCode (MANUAL PRINT!!!)
                            Print(1, ReportTypeEnum.DeliveryNote);
                        }
                    }
                    else if (null != e)
                    {
                        // S14
                        // **Error**?  We should not receive this message
                        e.IsValid = false;
                        e.ErrorMessage = string.Format("Transaction type '{0}' is non stock but delivery warehouse '{1}' is a non TDC warehouse.", TransactionTypeCode, DeliveryWarehouseCode);
                    }
                }
                else if (TransactionSubType.IsTransfer && null != e)
                {
                    // ********************************
                    // ** Transaction sub type is transfer
                    // ********************************

                    // S16, S17, S18
                    // **Error?**, no such thing as a non stock transfer
                    e.IsValid = false;
                    e.ErrorMessage = string.Format("Shipment cannot be a non stock transfer.");
                }
            }
            else if (TransactionType.IsCollection)
            {
                // ****************************************************************
                // ** Transaction type is collection
                // ****************************************************************

                // S19
                if (Status != StatusEnum.Mapped)
                {
                    // Print 2 "Delivery Note" in DeliveryWarehouseCode (MANUAL PRINT!!!)
                    Print(2, ReportTypeEnum.DeliveryNote);
                }
            }
            else if (TransactionType.IsSample)
            {
                // ****************************************************************
                // ** Transaction type is sample
                // ****************************************************************

                // S20
                // See if the delivery warehouse is a tdc warehouse
                if (DeliveryWarehouse.IsTDC)
                {
                    // See we need to print delivery note
                    if (Route.IsCollection && printDeliveryCollectionNote)
                    {
                        // Print 1 "Delivery Note" in DeliveryWarehouseCode
                        Print(1, ReportTypeEnum.DeliveryNote);
                    }
                    else if (Route.IsSpecial && printDeliveryCollectionNote)
                    {
                        // Print 2 "Delivery Note" in DeliveryWarehouseCode
                        Print(2, ReportTypeEnum.DeliveryNote);
                    }
                    else if (Status != StatusEnum.Mapped)
                    {
                        // Print 1 "Delivery Note" in DeliveryWarehouseCode (MANUAL PRINT!!!)
                        Print(1, ReportTypeEnum.DeliveryNote);
                    }
                }
                else if (null != e)
                {
                    // S??
                    // **Error**?  We should not receive this message
                    e.IsValid = false;
                    e.ErrorMessage = string.Format("Transaction type '{0}' is sample but delivery warehouse '{1}' is a non TDC warehouse.", TransactionTypeCode, DeliveryWarehouseCode);
                }
            }

            // Calculate tomorrows date
            //DateTime tomorrow = DateTime.Today.AddDays(1);
            //double deliverInDays = tomorrow.Subtract(this.EstimatedDeliveryDate).TotalDays;
            // *********************************************************************************
            // ** Tec Spec 5.5, Print special deliveries and collections if estimated delivery is today or tomorrow
            // *********************************************************************************
            //if (deliverInDays > 0 && deliverInDays < 2) { }
        }

        /// <summary>
        /// Gets or sets the division code.
        /// </summary>
        /// <value>The division code.</value>
        [RequiredValidator("Division Code is required.", "*")]
        [LengthValidator(1, 3, "Division code must be between {0} and {1} characters in length.", "*")]
        [CustomValidator("", "ValidDivisionCode", "*")]
        public override string DivisionCode
        {
            get { return base.DivisionCode; }
            set
            {
                // Store the division code
                base.DivisionCode = value;
                // Retrieve the division code
                division = OpcoDivisionController.GetOpCoDivision(value);
            }
        }

        /// <summary>
        /// Custom validation method to validate division code
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:ValidationFramework.CustomValidationEventArgs"/> instance containing the event data.</param>
        public void ValidDivisionCode(object sender, CustomValidationEventArgs e)
        {
            // Seed the message
            e.ErrorMessage = string.Format("Division code '{0}' is not a valid OpCo division code.", this.DivisionCode);
            // If a division code was specified see if it's valid
            e.IsValid = (null != this.division && -1 != this.division.Id);
        }

        /// <summary>
        /// Gets or sets the sales branch code.
        /// </summary>
        /// <value>The sales branch code.</value>
        [RequiredValidator("Sales branch bode is required.", "*")]
        [LengthValidator(1, 10, "Sales branch code must be between {0} and {1} characters in length.", "*")]
        [CustomValidator("", "ValidSalesBranchCode", "*")]
        public override string SalesBranchCode
        {
            get { return salesBranchCode; }
            set
            {
                // Store the sales branch code
                salesBranchCode = value;
                // Retrieve the sales location
                salesLocation = SalesLocationController.GetLocation(salesBranchCode, false);
            }
        }

        /// <summary>
        /// Custom validation method to validate sales branch location
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:ValidationFramework.CustomValidationEventArgs"/> instance containing the event data.</param>
        public void ValidSalesBranchCode(object sender, CustomValidationEventArgs e)
        {
            // Seed the message
            e.ErrorMessage = string.Format("Sales branch code '{0}' is not a valid sales location Code.", this.SalesBranchCode);
            // If a sales branch code was specified see if it's valid
            e.IsValid = (null != this.salesLocation && -1 != this.salesLocation.Id);
        }

        private SalesLocation salesLocation;
        public SalesLocation SalesLocation
        {
            get
            {
                return salesLocation;
            }
        }

        private OpCoDivision division;
        public OpCoDivision Division
        {
            get
            {
                return division;
            }
        }

        // The transaction sub type code
        private string transactionSubTypeCode;

        /// <summary>
        /// The TDC sub types are Normal, Transfer out, Conversion local and Conversion by a third party.
        /// </summary>
        [RequiredValidator("Transaction Sub Type is required.", "*")]
        [LengthValidator(1, 10, "Transaction sub type must be between {0} and {1} characters in length.", "*")]
        [CustomValidator("", "ValidTransactionSubTypeCode", "*")]
        public string TransactionSubTypeCode
        {
            get
            {
                // Return the transaction sub type code
                return transactionSubTypeCode;
            }
            set
            {
                // Store the transaction sub type code
                transactionSubTypeCode = value;

                // Retrieve the transaction sub type
                transactionSubType = TransactionSubTypeController.GetTransactionSubType(value);
            }
        }

        /// <summary>
        /// Custom validation method to validate transaction sub type
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:ValidationFramework.CustomValidationEventArgs"/> instance containing the event data.</param>
        public void ValidTransactionSubTypeCode(object sender, CustomValidationEventArgs e)
        {
            // Seed the message
            e.ErrorMessage = string.Format("Transaction sub type '{0}' does not map to a valid TDC transaction sub type.", this.transactionSubTypeCode);
            // If a sub type was specified see if it's valid
            e.IsValid = (null != this.transactionSubType && -1 != this.transactionSubType.Id);
        }

        // The transaction type object
        private TransactionSubType transactionSubType = null;

        /// <summary>
        /// The Transaction Sub Type for this shipment derived from Transaction Sub Type Code
        /// </summary>
        public TransactionSubType TransactionSubType
        {
            get { return transactionSubType; }
        }

        /// <summary>
        /// The TDC types are Stock, Non-stock, TDC collection or Sample
        /// </summary>
        [RequiredValidator("Transaction Type is required.", "*")]
        [LengthValidator(1, 10, "Transaction yype must be between {0} and {1} characters in length.", "*")]
        [CustomValidator("", "ValidTransactionTypeCode", "*")]
        public override string TransactionTypeCode
        {
            get
            {
                // Return the transaction type
                return base.TransactionTypeCode;
            }
            set
            {
                // Store the transaction type
                base.TransactionTypeCode = value;
                // We need to look up the transaction type
                this.transactionType = TransactionTypeController.GetTransactionType(value);
            }
        }

        /// <summary>
        /// Custom validation method to validate transaction sub type
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:ValidationFramework.CustomValidationEventArgs"/> instance containing the event data.</param>
        public void ValidTransactionTypeCode(object sender, CustomValidationEventArgs e)
        {
            // Seed the message
            e.ErrorMessage = string.Format("Transaction type '{0}' does not map to a valid TDC transaction type.", this.TransactionTypeCode);
            // Specify if valid
            e.IsValid = (null != this.transactionType && -1 != this.transactionType.Id);
        }

        // The transaction type object
        private TransactionType transactionType = null;

        /// <summary>
        /// The Transaction Type for this shipment derived from Transaction Type Code
        /// </summary>
        public TransactionType TransactionType
        {
            get { return transactionType; }
        }

        /// <summary>
        /// Gets or sets the route code.
        /// </summary>
        /// <value>The route code.</value>
        [LengthValidator(1, 10, "Route code must be between {0} and {1} characters in length.", "*")]
        [CustomValidator("", "ValidRouteCode", "*")]
        public override string RouteCode
        {
            get
            {
                // Return the route code
                return base.RouteCode;
            }
            set
            {
                // Store the route code
                base.RouteCode = value;

                // We need to lookup route code
                // Route code carries, "IsNextDay", "IsSameDay", "IsCollection" (Customer to collect) and "IsSpecial"
                route = RouteController.GetRoute(value);
            }
        }

        /// <summary>
        /// Custom validation method to validate route code
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:ValidationFramework.CustomValidationEventArgs"/> instance containing the event data.</param>
        public void ValidRouteCode(object sender, CustomValidationEventArgs e)
        {
            // Seed the message
            e.ErrorMessage = string.Format("Route code '{0}' does not map to a valid TDC route code.", this.RouteCode);
            // Seed to valid
            e.IsValid = (null != this.route && -1 != this.route.Id);
        }

        // The route code assigned to this shipment
        private Route route = null;

        // Return the route object, generated via a route code set
        public Route Route
        {
            get { return route; }
        }

        /// <summary>
        /// Warehouse for the sourced stock.
        /// </summary>
        [CustomValidator("", "ValidStockWarehouseCode", "*")]
        public override string StockWarehouseCode
        {
            get
            {
                // Return the stock warehouse code
                return base.StockWarehouseCode;
            }
            set
            {
                // Store the stock warehouse code
                base.StockWarehouseCode = value;
                // Lookup the stock warehouse
                if (!string.IsNullOrEmpty(value)) stockWarehouse = WarehouseController.GetWarehouse(value);
            }
        }

        /// <summary>
        /// Custom validation method to validate the stock warehouse code
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:ValidationFramework.CustomValidationEventArgs"/> instance containing the event data.</param>
        public void ValidStockWarehouseCode(object sender, CustomValidationEventArgs e)
        {
            // Seed to valid
            e.IsValid = true;

            // Make sure we have a transaction type to see if we need a stock warehouse code
            if (null == this.transactionType || -1 == this.transactionType.Id)
            {
                e.ErrorMessage = string.Format("Stock warehouse code '{0}' cannot be validated as transaction type '{1}' is invalid.", this.StockWarehouseCode, this.TransactionTypeCode);
                e.IsValid = false;
            }
            else
            {
                // We have a transaction type, check the stock warehouse code
                if (!this.transactionType.IsCollection)
                {
                    // Make sure that we have a valid stock warehouse
                    if (null == this.stockWarehouse || -1 == this.stockWarehouse.Id)
                    {
                        e.ErrorMessage = string.Format("Unable to map stock warehouse code '{0}' to a TDC warehouse code.", this.StockWarehouseCode);
                        e.IsValid = false;
                    }
                }
                else
                {
                    // Clear the stock warehouse code
                    this.stockWarehouseCode = "";
                }
            }
        }

        private Warehouse stockWarehouse;
        /// <summary>
        /// Gets or sets the stock warehouse.
        /// </summary>
        /// <value>The stock warehouse.</value>
        public Warehouse StockWarehouse
        {
            get { return stockWarehouse; }
        }

        /// <summary>
        /// Warehouse for the sourced Delivery.
        /// </summary>
        [LengthValidator(1, 10, "Delivery warehouse code must be between {0} and {1} characters in length.", "*")]
        [CustomValidator("", "ValidDeliveryWarehouseCode", "*")]
        public override string DeliveryWarehouseCode
        {
            get
            {
                // Return the Delivery warehouse code
                return base.DeliveryWarehouseCode;
            }
            set
            {
                // Store the Delivery warehouse code
                base.DeliveryWarehouseCode = value;
                // Lookup the Delivery warehouse
                deliveryWarehouse = WarehouseController.GetWarehouse(value);
            }
        }

        /// <summary>
        /// Custom validation method to validate the delivery warehouse code
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:ValidationFramework.CustomValidationEventArgs"/> instance containing the event data.</param>
        public void ValidDeliveryWarehouseCode(object sender, CustomValidationEventArgs e)
        {
            // Seed values
            e.ErrorMessage = string.Format("Unable to map delivery warehouse code '{0}' to a TDC warehouse.", this.DeliveryWarehouseCode);
            e.IsValid = true;

            // Make sure that we have a valid stock warehouse
            if (null == this.deliveryWarehouse || -1 == this.deliveryWarehouse.Id)
            {
                e.IsValid = false;
            }
        }

        private Warehouse deliveryWarehouse;
        /// <summary>
        /// Gets or sets the delivery warehouse.
        /// </summary>
        /// <value>The delivery warehouse.</value>
        public Warehouse DeliveryWarehouse
        {
            get { return deliveryWarehouse; }
            //set { deliveryWarehouse = value; }
        }

        private int splitSequence;
        private bool isSplit;
        private bool isRecurring;
        private string locationCode;

        /// <summary>
        /// Gets or sets the location unique site code.
        /// </summary>
        /// <value>The location code.</value>
        public string LocationCode
        {
            get { return locationCode; }
            set { locationCode = value; }
        }

        private DateTime sentToWMS;

        /// <summary>
        /// Gets or sets the sent to WMS.
        /// </summary>
        /// <value>The sent to WMS.</value>
        public DateTime SentToWMS
        {
            get { return sentToWMS; }
            set { sentToWMS = value; }
        }

        private DateTime estimatedDeliveryDate;

        /// <summary>
        /// Gets or sets the estimated delivery date.
        /// </summary>
        /// <value>The estimated delivery date.</value>
        [CustomValidator("", "ValidEstimatedDeliveryDate", "*")]
        public DateTime EstimatedDeliveryDate
        {
            get { return estimatedDeliveryDate; }
            set { estimatedDeliveryDate = value; }
        }

        public void ValidEstimatedDeliveryDate(object sender, CustomValidationEventArgs e)
        {
            // Seed to a valid status
            e.IsValid = true;

            // Make sure that we have an estimated delivery date and time
            if (Status == StatusEnum.Routed || Status == StatusEnum.Completed)
            {
                if (Null.IsNull(EstimatedDeliveryDate))
                {
                    // Not valid
                    e.IsValid = false;
                    e.ErrorMessage = string.Format("An estimated delivery date and time is required for a status of {0}.", Status.ToString());
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is recurring.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is recurring; otherwise, <c>false</c>.
        /// </value>
        public bool IsRecurring
        {
            get { return isRecurring; }
            set { isRecurring = value; }
        }

        /// <summary>
        /// Gets or sets the split sequence.
        /// </summary>
        /// <value>The split sequence.</value>
        public int SplitSequence
        {
            get { return splitSequence; }
            set { splitSequence = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is split.
        /// </summary>
        /// <value><c>true</c> if this instance is split; otherwise, <c>false</c>.</value>
        public bool IsSplit
        {
            get { return isSplit; }
            set { isSplit = value; }
        }

        private string routeTrip;

        /// <summary>
        /// Gets or sets the route trip.
        /// </summary>
        /// <value>The route trip.</value>
        public string RouteTrip
        {
            get { return (string.IsNullOrEmpty(routeTrip) ? "" : routeTrip); }
            set { routeTrip = value; }
        }

        private int routeDrop;

        /// <summary>
        /// Gets or sets the route drop.
        /// </summary>
        /// <value>The route drop.</value>
        public int RouteDrop
        {
            get { return routeDrop; }
            set { routeDrop = value; }
        }

        public string RouteTripDrop
        {
            get
            {
                if (!String.IsNullOrEmpty(RouteTrip) && RouteDrop > 0)
                {
                    return RouteTrip + "-" + RouteDrop.ToString();
                }
                else
                {
                    return "";
                }
            }
        }

        private int opcoShipmentId;

        /// <summary>
        /// Gets or sets the op co shipment id.
        /// </summary>
        /// <value>The op co shipment id.</value>
        public int OpCoShipmentId
        {
            get { return opcoShipmentId; }
            set { opcoShipmentId = value; }
        }

        private DateTime actualDeliveryDate;

        /// <summary>
        /// Gets or sets the actual delivery date.
        /// </summary>
        /// <value>The actual delivery date.</value>
        [CustomValidator("", "ValidActualDeliveryDate", "*")]
        public DateTime ActualDeliveryDate
        {
            get { return actualDeliveryDate; }
            set { actualDeliveryDate = value; }
        }

        public void ValidActualDeliveryDate(object sender, CustomValidationEventArgs e)
        {
            // Seed to a valid status
            e.IsValid = true;

            // Make sure that we have an actual delivery date and time
            if (Status == StatusEnum.Completed)
            {
                if (Null.IsNull(ActualDeliveryDate))
                {
                    // Not valid
                    e.IsValid = false;
                    e.ErrorMessage = string.Format("An actual shipment date and time is required for a status of {0}.", Status.ToString());
                }
            }
        }

        private DateTime routingDateTime;

        /// <summary>
        /// Gets or sets the routing date time.
        /// </summary>
        /// <value>The routing date time.</value>
        public DateTime RoutingDateTime
        {
            get { return routingDateTime; }
            set { routingDateTime = value; }
        }

        private PAFAddress pafAddress;

        /// <summary>
        /// Gets or sets the PAF address.
        /// </summary>
        /// <value>The PAF address.</value>
        public PAFAddress PAFAddress
        {
            get { return pafAddress; }
            set { pafAddress = value; }
        }

        /// <summary>
        /// Splits the shipment.
        /// </summary>
        /// <param name="lineSplits">The line splits.</param>
        /// <param name="alterSourceQuantites">if set to <c>true</c> [alter source quantites].</param>
        /// <param name="updatedBy">The updated by.</param>
        /// <returns></returns>
        public TDCShipment SplitShipment(
                    List<TDCLineSplit> lineSplits,
                    bool alterSourceQuantites,
                    bool alterWeightsAndVolumes,
                    string updatedBy)
        {
            // Split the shipment if splits specified
            if (lineSplits.Count > 0)
            {
                try
                {
                    // The split shipment
                    TDCShipment tdcSplitShipment = new TDCShipment();

                    // Map the new opco shipment over the existing one
                    Mapper.Map(this, tdcSplitShipment, "NA", "NA", null, new string[] { "Id", "ShipmentLines", "CheckSum" });

                    // Unit weight and volume
                    decimal unitWeight = 0;
                    decimal unitVolume = 0;

                    // Add the split lines to the new shipment
                    foreach (TDCLineSplit lineSplit in lineSplits)
                    {
                        // Find the split line in the existing shipment
                        ShipmentLine existingSplitLine = ShipmentLines.Find(
                            delegate(ShipmentLine shipmentLine)
                            {
                                // If the line number matches, it's the line we want
                                return (shipmentLine.LineNumber == lineSplit.Line);
                            });

                        // See if we found the existing line
                        if (null != existingSplitLine)
                        {
                            // Create the new line for the split shipment
                            TDCShipmentLine tdcShipmentLine = new TDCShipmentLine();

                            // Update the existing line
                            Mapper.Map(existingSplitLine, tdcShipmentLine, "NA", "NA", null, new string[] { "Id", "CheckSum" });

                            // Calculate the unit weight and volume
                            unitWeight = existingSplitLine.NetWeight / existingSplitLine.Quantity;
                            unitVolume = existingSplitLine.Volume / existingSplitLine.Quantity;

                            // Update the quantity
                            tdcShipmentLine.Quantity = lineSplit.Quantity;

                            // Update the weight and volume if required
                            if (alterWeightsAndVolumes)
                            {
                                tdcShipmentLine.NetWeight = unitWeight * tdcShipmentLine.Quantity;
                                tdcShipmentLine.Volume = unitVolume * tdcShipmentLine.Quantity;
                            }

                            // See if we need to update the source shipment
                            if (alterSourceQuantites)
                            {
                                // Reduce the original quantity
                                existingSplitLine.Quantity = Math.Max(0, (existingSplitLine.Quantity - lineSplit.Quantity));

                                // Update the weight and volume if required
                                if (alterWeightsAndVolumes)
                                {
                                    existingSplitLine.NetWeight = unitWeight * existingSplitLine.Quantity;
                                    existingSplitLine.Volume = unitVolume * existingSplitLine.Quantity;
                                }
                            }

                            // Add the line to the new split shipment
                            tdcSplitShipment.ShipmentLines.Add(tdcShipmentLine);
                        }
                    }

                    // Update the split sequence for the new shipment
                    tdcSplitShipment.SplitSequence = 0;

                    // Indicate that the new shipment is a split
                    tdcSplitShipment.IsSplit = true;

                    // Update the despatch number for the new split
                    int despatchNumber = Convert.ToInt32(this.DespatchNumber);
                    despatchNumber += (500 + (500 * this.SplitSequence) + ((this.IsSplit) ? 50 : 0));
                    tdcSplitShipment.DespatchNumber = despatchNumber.ToString();

                    // Increment the split sequence for the original shipment
                    this.SplitSequence++;

                    // Specify the user that made the changes
                    this.UpdatedBy = updatedBy;
                    tdcSplitShipment.UpdatedBy = updatedBy;

                    // Save the shipments to the db
                    using (TransactionScope tsShipments = new TransactionScope(TransactionScopeOption.RequiresNew))
                    {
                        // Save the new split shipment
                        TDCShipmentController.SaveShipment(tdcSplitShipment);

                        // Save the original shipment
                        TDCShipmentController.SaveShipment(this);

                        // Done;
                        tsShipments.Complete();
                    }

                    // Done
                    return tdcSplitShipment;
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Failed to split shipment. {0}", ex.Message));
                }
            }
            else
            {
                // Done
                return null;
            }
        }

        /// <summary>
        /// Calculates the delivery date.
        /// </summary>
        public void CalculateDeliveryDate()
        {
            // See if the required shipment date is in the past
            if (DateTime.Today > RequiredShipmentDate)
            {
                // Use the required shipment date
                CalculateDeliveryDate(DateTime.Today);
            }
            else
            {
                // Use the required shipment date
                CalculateDeliveryDate(RequiredShipmentDate);
            }
        }

        /// <summary>
        /// Calculates the delivery date.
        /// </summary>
        public void CalculateDeliveryDate(DateTime requiredShipmentDate)
        {
            //if RHG or IsSameDay then default to RequiredShipmentDate
            EstimatedDeliveryDate = requiredShipmentDate;

            if (OpCoCode.ToUpper() == "HSP" || OpCoCode.ToUpper() == "TPC")
            {
                //non working days for the delivery warehouse for the next month - should be enough!
                //perhaps more efficient than checking whether each day is nonworking one at a time 
                //(therefore querying the database each time)
                List<NonWorkingDay> nonWorkingDays =
                    NonWorkingDayController.GetNonWorkingDaysByWarehouse(
                            requiredShipmentDate,
                            requiredShipmentDate.AddMonths(1),
                            DeliveryWarehouse.Id);

                // See if it's a transfer
                if (StockWarehouseCode.ToUpper() != DeliveryWarehouseCode.ToUpper())
                {
                    // trunking required
                    int numberOfTrunkerDays =
                        TrunkerDaysController.GetNumberOfTrunkerDays(StockWarehouse, DeliveryWarehouse);
                    // move the estimated delivery date on one working day for each trunking day
                    for (int i = 0; i < numberOfTrunkerDays; i++)
                    {
                        EstimatedDeliveryDate =
                            NonWorkingDayController.NextWorkingDate(EstimatedDeliveryDate, nonWorkingDays);
                    }
                }
                else if (!Route.IsSameDay)
                {
                    // if next day delivery then set the EstimatedDeliveryDate to the next working date
                    EstimatedDeliveryDate =
                        NonWorkingDayController.NextWorkingDate(EstimatedDeliveryDate, nonWorkingDays);
                }
            }
        }

        /// <summary>
        /// Checks the shipment address using address lookup software.
        /// </summary>
        public void CheckAddress()
        {
            // Lookup the shipment address using address lookup software
            #if (ADDRESSLOOKUP)
                PAFAddress = AddressController.CheckAddress(this.ShipmentAddress, this.ShipmentName);
            #endif
        }

        /// <summary>
        /// This method will check if there has been a poor match an pass an exception to the exception handling
        /// </summary>
        public void NotifyIfPoorAddressMatch()
        {
            //conditionally raise an exception is the address match is not "good", depending on how the handling is configured this
            //exception will probably be emailed to a user at the opco and the operator who entered the shipment, a hyperlink will
            //probably be supplied to allow either party to edit the address and re-validate it.
            if (PAFAddress.Status == PAFAddress.PAFStatusEnum.Needs_Checking)
            {
                ExceptionPolicy.HandleException(new InvalidAddressException(OpCoCode, OpCoContact.Email,
                    string.Format(ConfigurationManager.AppSettings["InvalidAddressExceptionBody"], ShipmentNumber, DespatchNumber, Id)), "Business Logic");
            }
           
        }
        /// <summary>
        /// Copies this instance.
        /// </summary>
        /// <returns></returns>
        public TDCShipment Copy()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generates the commander sales order.
        /// </summary>
        /// <returns></returns>
        public CommanderSalesOrder GenerateCommanderSalesOrder()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the lines as splits.
        /// </summary>
        /// <returns></returns>
        public TDCLineSplit[] GetLinesAsSplits()
        {
            throw new NotImplementedException();
        }

        private bool isValidAddress;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is valid address.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is valid address; otherwise, <c>false</c>.
        /// </value>
        public bool IsValidAddress
        {
            get { return isValidAddress; }
            set { isValidAddress = value; }
        }


        /// <summary>
        /// Prints this tdc shipments transfer/collection note
        /// </summary>
        public void PrintTransferConversionNote()
        {
            // Call the validation routine and specify what we want it to do
            ValidateShipmentType(null, null, false, true, false);
        }

        /// <summary>
        /// Prints this tdc shipments delivery/collection note
        /// </summary>
        public void PrintDeliveryCollectionNote()
        {
            // Call the validation routine and specify what we want it to do
            ValidateShipmentType(null, null, false, false, true);
        }

        /// <summary>
        /// Prints this tdc shipments transfer, conversion, delivery and collection note
        /// </summary>
        public void PrintNotes()
        {
            // Call the validation routine and specify what we want it to do
            ValidateShipmentType(null, null, false, true, true);
        }

        /// <summary>
        /// Prints this tdc shipment
        /// </summary>
        /// <param name="numberOfCopies">The number of copies to print.</param>
        private void Print(int numberOfCopies, ReportTypeEnum reportType)
        {
            // Print the report via the controller
            TDCShipmentController.Print(this, numberOfCopies, reportType);

            // Update the print count
            IncrementPrintCount();
        }

        /// <summary>
        /// Increments the print count.
        /// </summary>
        /// <returns></returns>
        protected int IncrementPrintCount()
        {
            return TDCShipmentController.IncrementPrintCount(this);
        }

        public bool IsEstimatedDeliveryLate
        {
            get
            {
                // Must have a status of routed
                if (Status == StatusEnum.Routed)
                {
                    // Generate the latest required delivery date and time
                    DateTime tmpRequiredShipmentDate = RequiredShipmentDate;

                    if (!string.IsNullOrEmpty(BeforeTime) && BeforeTime.IndexOf(':') != -1)
                    {
                        // Add the after time
                        string[] HHMM = BeforeTime.Split(':');
                        int HH = Convert.ToInt32(HHMM[0]);
                        int MM = Convert.ToInt32(HHMM[1]);
                        // Add the after time
                        tmpRequiredShipmentDate = tmpRequiredShipmentDate.AddHours(HH).AddMinutes(MM);
                        // See if it's late
                        return (EstimatedDeliveryDate > tmpRequiredShipmentDate);
                    }
                    else
                    {
                        // No time
                        return false;
                    }
                }
                else
                {
                    // Not routed
                    return false;
                }
            }
        }

        public bool IsActualDeliveryLate
        {
            get
            {
                // Must have a status of completed
                if (Status == StatusEnum.Completed)
                {
                    // See if it's late
                    return (ActualDeliveryDate > EstimatedDeliveryDate);
                }
                else
                {
                    // Not completed
                    return false;
                }
            }
        }
    }

    public class TDCShipmentPrintable : TDCShipment
    {
        public override bool IsValid
        {
            get
            {
                // A TDCShipmentPrintable is always valid.  If base IsValid is called is will recurse.
                return true;
            }
        }

        // *********************************************
        // ** Division
        // *********************************************
        public byte[] DivisionLogo
        {
            get
            {
                return Division.Logo;
            }
        }

        public string DivisionLogoURI
        {
            get
            {
                return Division.LogoURI;
            }
        }

        // *********************************************
        // ** Sales Location
        // *********************************************
        public string SalesLocationDescription
        {
            get
            {
                return SalesLocation.Description;
            }
        }

        public string SalesLocationTelephoneNumber
        {
            get
            {
                return SalesLocation.TelephoneNumber;
            }
        }

        // *********************************************
        // ** Delivery Warehouse
        // *********************************************
        public string DeliveryWarehouseDescription
        {
            get
            {
                return DeliveryWarehouse.Description;
            }
        }

        public string DeliveryWarehouseCodeandDescription
        {
            get
            {
                return DeliveryWarehouse.CodeAndDescription;
            }
        }

        public string DeliveryWarehouseContactName
        {
            get
            {
                return DeliveryWarehouse.Contact.Name;
            }
        }

        public string DeliveryWarehouseContactEmail
        {
            get
            {
                return DeliveryWarehouse.Contact.Email;
            }
        }

        public string DeliveryWarehouseContactTelephoneNumber
        {
            get
            {
                return DeliveryWarehouse.Contact.TelephoneNumber;
            }
        }


        // *********************************************
        // ** Stock Warehouse
        // *********************************************
        public string StockWarehouseDescription
        {
            get
            {
                return StockWarehouse.Description;
            }
        }

        public string StockWarehouseCodeandDescription
        {
            get
            {
                return StockWarehouse.CodeAndDescription;
            }
        }

        public string StockWarehouseContactName
        {
            get
            {
                return StockWarehouse.Contact.Name;
            }
        }

        public string StockWarehouseContactEmail
        {
            get
            {
                return StockWarehouse.Contact.Email;
            }
        }

        public string StockWarehouseContactTelephoneNumber
        {
            get
            {
                return StockWarehouse.Contact.TelephoneNumber;
            }
        }

        // *********************************************
        // ** Route Code
        // *********************************************
        public string RouteCodeDescription
        {
            get
            {
                return Route.Description;
            }
        }

        public bool RouteCodeIsCollection
        {
            get
            {
                return Route.IsCollection;
            }
        }

        public bool RouteCodeIsNextDay
        {
            get
            {
                return Route.IsNextDay;
            }
        }

        public bool RouteCodeIsSameDay
        {
            get
            {
                return Route.IsSameDay;
            }
        }

        public bool RouteCodeIsSpecial
        {
            get
            {
                return Route.IsSpecial;
            }
        }

        // *********************************************
        // ** Transaction Sub Type
        // *********************************************
        public string TransactionSubTypeDescription
        {
            get
            {
                return TransactionSubType.Description;
            }
        }

        public bool TransactionSubTypeIsNormal
        {
            get
            {
                return TransactionSubType.IsNormal;
            }
        }

        public bool TransactionSubTypeIsTransfer
        {
            get
            {
                return TransactionSubType.IsTransfer;
            }
        }

        public bool TransactionSubTypeIsLocalConversion
        {
            get
            {
                return TransactionSubType.IsLocalConversion;
            }
        }

        public bool TransactionSubTypeIs3rdParyConversion
        {
            get
            {
                return TransactionSubType.Is3rdPartyConversion;
            }
        }

        // *********************************************
        // ** Transaction Type
        // *********************************************
        public string TransactionTypeDescription
        {
            get
            {
                return TransactionType.Description;
            }
        }

        public bool TransactionTypeIsStock
        {
            get
            {
                return TransactionType.IsStock;
            }
        }

        public bool TransactionTypeIsNonStock
        {
            get
            {
                return TransactionType.IsNonStock;
            }
        }


        public bool TransactionTypeIsCollection
        {
            get
            {
                return TransactionType.IsCollection;
            }
        }

        public bool TransactionTypeIsSample
        {
            get
            {
                return TransactionType.IsSample;
            }
        }

        // *********************************************
        // ** PAF Address
        // *********************************************
        public string PAFAddressLine1
        {
            get
            {
                return PAFAddress.Line1;
            }
        }

        public string PAFAddressLine2
        {
            get
            {
                return PAFAddress.Line2;
            }
        }

        public string PAFAddressLine3
        {
            get
            {
                return PAFAddress.Line3;
            }
        }

        public string PAFAddressLine4
        {
            get
            {
                return PAFAddress.Line4;
            }
        }

        public string PAFAddressLine5
        {
            get
            {
                return PAFAddress.Line5;
            }
        }

        public string PAFAddressPostCode
        {
            get
            {
                return PAFAddress.PostCode;
            }
        }

        public int PAFAddressLocation
        {
            get
            {
                return PAFAddress.Location;
            }
        }

        public int PAFAddressEasting
        {
            get
            {
                return PAFAddress.Easting;
            }
        }

        public int PAFAddressNorthing
        {
            get
            {
                return PAFAddress.Northing;
            }
        }

        public string PAFAddressDPS
        {
            get
            {
                return PAFAddress.DPS;
            }
        }

        public PAFAddress.PAFStatusEnum PAFAddressStatus
        {
            get
            {
                return PAFAddress.Status;
            }
        }

        // *********************************************
        // ** Shipment Address
        // *********************************************
        public string ShipmentAddressLine1
        {
            get
            {
                return ShipmentAddress.Line1;
            }
        }

        public string ShipmentAddressLine2
        {
            get
            {
                return ShipmentAddress.Line2;
            }
        }

        public string ShipmentAddressLine3
        {
            get
            {
                return ShipmentAddress.Line3;
            }
        }

        public string ShipmentAddressLine4
        {
            get
            {
                return ShipmentAddress.Line4;
            }
        }

        public string ShipmentAddressLine5
        {
            get
            {
                return ShipmentAddress.Line5;
            }
        }

        public string ShipmentAddressPostCode
        {
            get
            {
                return ShipmentAddress.PostCode;
            }
        }

        // *********************************************
        // ** Customer Address
        // *********************************************
        public string CustomerAddressLine1
        {
            get
            {
                return CustomerAddress.Line1;
            }
        }

        public string CustomerAddressLine2
        {
            get
            {
                return CustomerAddress.Line2;
            }
        }

        public string CustomerAddressLine3
        {
            get
            {
                return CustomerAddress.Line3;
            }
        }

        public string CustomerAddressLine4
        {
            get
            {
                return CustomerAddress.Line4;
            }
        }

        public string CustomerAddressLine5
        {
            get
            {
                return CustomerAddress.Line5;
            }
        }

        public string CustomerAddressPostCode
        {
            get
            {
                return CustomerAddress.PostCode;
            }
        }

        // *********************************************
        // ** Shipment Contact
        // *********************************************
        public string ShipmentContactName
        {
            get
            {
                return ShipmentContact.Name;
            }
        }

        public string ShipmentContactEmail
        {
            get
            {
                return ShipmentContact.Email;
            }
        }

        public string ShipmentContactTelephoneNumber
        {
            get
            {
                return ShipmentContact.TelephoneNumber;
            }
        }

        // *********************************************
        // ** Op Co Contact
        // *********************************************
        public string OpCoContactName
        {
            get
            {
                return OpCoContact.Name;
            }
        }

        public string OpCoContactEmail
        {
            get
            {
                return OpCoContact.Email;
            }
        }

        public string OpCoContactTelephoneNumber
        {
            get
            {
                return OpCoContact.TelephoneNumber;
            }
        }
    }

    /// <summary>
    /// A second class 'TDCLineSplit' which is an entity with namespace Discovery.BusinessObjects
    /// Split the TDC shipment line
    /// </summary>
    public class TDCLineSplit
    {
        private int line;

        /// <summary>
        /// Gets or sets the line.
        /// </summary>
        /// <value>The line.</value>
        public int Line
        {
            get { return line; }
            set { line = value; }
        }

        private int quantity;

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>The quantity.</value>
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
    }
}