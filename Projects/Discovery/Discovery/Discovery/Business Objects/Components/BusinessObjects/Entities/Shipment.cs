using System;
using System.Drawing;
using System.Collections.Generic;
using Discovery.ComponentServices.Mapping;
using Discovery.Utility;
using Discovery.BusinessObjects.Controllers;
using ValidationFramework;

namespace Discovery.BusinessObjects
{
    /// <summary>
    /// A Class 'Shipment' which is an entity with namespace Discovery.BusinessObjects
    /// It is inherited from PersistableBusinessObject
    /// The class holds the shipment details
    /// </summary>

    [Serializable]
    public abstract class Shipment : PersistableBusinessObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Shipment"/> class.
        /// </summary>
        public Shipment()
            : base()
        {
            // The shipment lines
            shipmentLines = new System.Collections.Generic.List<ShipmentLine>();
            // The opco contact
            opCoContact = new OpCoContact();
            // The shipment contact
            shipmentContact = new Contact();
            // The shipment address
            shipmentAddress = new ShipmentAddress();
            // The customer address
            customerAddress = new CustomerAddress();
            // Seed the status
            status = StatusEnum.NotMapped;
            // Seed the type
            type = TypeEnum.OPCO;
            // Audit id
            auditId = Null.NullInteger;
            // Is printed
            IsPrinted = false;
            // Op Co Held
            opCoHeld = false;
            // Generated date time
            generatedDateTime = DateTime.Now;
            // Despatch number
            despatchNumber = Null.NullString;
            // Customer number
            customerNumber = Null.NullString;
            // Customer name
            customerName = Null.NullString;
            // Division code
            divisionCode = Null.NullString;
            // Transaction type
            transactionTypeCode = Null.NullString;
        }

        /// <summary>
        /// Returns true if the business object is valid otherwise throws an invalid business object exception
        /// </summary>
        public override bool IsValid
        {
            get
            {
                // Is valid return
                bool validRet = true;

                // Validate the shipment
                if (!base.IsValid) validRet = false;

                // Validate the opco contact
                if (null != opCoContact && !opCoContact.IsValid) validRet = false;

                // Validate the shipment contact
                if (null != shipmentContact && !shipmentContact.IsValid) validRet = false;

                // Validate the shipment address
                if (null != shipmentAddress && !shipmentAddress.IsValid) validRet = false;

                // Validate the customer address
                if (null != customerAddress && !customerAddress.IsValid) validRet = false;

                // Validate shipment lines
                foreach (ShipmentLine line in shipmentLines)
                {
                    // See if the line is valid
                    if (!line.IsValid) validRet = false;
                }

                // Done
                return validRet;
            }
        }

        public override List<string> ValidationMessages
        {
            get
            {
                // Get all validation messages for ValidatableBase properties
                List<String> validationMessagesAll = base.ValidationMessages;

                // Add any validation messages from the lines to the above
                foreach (ShipmentLine line in ShipmentLines)
                {
                    // See if the line has any validation errors
                    if (line.Validator.Violations.Count > 0)
                    {
                        // Add a descriptive message for the entity
                        validationMessagesAll.Add(string.Format("Shipment line {0} errors;", line.LineNumber));
                        // Add the line messages
                        validationMessagesAll.AddRange(line.ValidationMessages);
                    }
                }

                // Done
                return validationMessagesAll;
            }
        }

        /// <summary>
        /// An enumeration that indicates the shipment type, OpCo, Adhoc or Warehouse
        /// </summary>
        public enum TypeEnum
        {
            /// <summary>
            /// OpCo status of TypeEnum (1).  Indicates that the shipment is an OpCo shipment.
            /// </summary>
            OPCO = 1,
            /// <summary>
            /// Adhoc status of TypeEnum (2).  Indicates that the shipment is an adhoc shipment.
            /// </summary>
            ADH = 2,
            /// <summary>
            /// Warehouse status of TypeEnum (4).  Indicates that the shipment is a warehouse shipment.
            /// </summary>
            WHS = 4,
        }

        /// <summary>
        /// An enumeration to include shipment's status NotMapped, Mapped, Held, Routing, Routed, Printed, Completed or Cancelled
        /// </summary>
        public enum StatusEnum
        {
            /// <summary>
            /// NotMapped status of StatusEnum (OpCo Shipment Only)
            /// </summary>
            NotMapped = 0,  // OpCo Shipment Only!
            /// <summary>
            /// Mapped status of StatusEnum (OpCo Shipment Only)
            /// </summary>
            Mapped,     // OpCo Shipment Only!
            /// <summary>
            /// Held status of StatusEnum
            /// </summary>
            Held,
            /// <summary>
            /// Routing status of StatusEnum
            /// </summary>
            Routing,
            /// <summary>
            /// Routed status of StatusEnum
            /// </summary>
            Routed,
            /// <summary>
            /// Printed status of StatusEnum
            /// </summary>
            Printed,
            /// <summary>
            /// Completed status of StatusEnum
            /// </summary>
            Completed,
            /// <summary>
            /// Cancelled status of StatusEnum
            /// </summary>
            Cancelled,
            /// <summary>
            /// POD Completed status of StatusEnum
            /// </summary>
            PODCompleted
        }

        /// <summary>
        /// Updates this shipment based on the specified shipment.
        /// </summary>
        /// <param name="newShipment">The new shipment.</param>
        /// <returns></returns>
        public virtual Shipment UpdateFromShipment(Shipment newShipment)
        {
            try
            {
                // Map the new opco shipment over the existing one
                Mapper.Map(newShipment, this, "NA", "NA", null, new string[] { "Id", "ShipmentLines", "CheckSum" });

                // Update shipment lines
                foreach (ShipmentLine newShipmentLine in newShipment.ShipmentLines)
                {
                    // Find the existing line
                    ShipmentLine existingShipmentLine = ShipmentLines.Find(
                        delegate(ShipmentLine shipmentLine)
                        {
                            // If the line number matches, it's the line we want
                            return (shipmentLine.LineNumber == newShipmentLine.LineNumber);
                        });

                    // See if we found the existing line
                    if (null != existingShipmentLine)
                    {
                        // Update the existing line
                        Mapper.Map(newShipmentLine, existingShipmentLine, "NA", "NA", null, new string[] { "Id", "CheckSum" });
                    }
                    else
                    {
                        // Add the new line
                        ShipmentLines.Add(newShipmentLine);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Failed to update existing shipment {0} - {1} for OpCo {2}. The error was {3}", this.ShipmentNumber, this.DespatchNumber, this.OpCoCode, ex));
            }

            // done
            return this;
        }

        /// <summary>
        /// The Id of the User that added or updated the corresponding record in the database.
        /// </summary>
        /// <value>A User Id.</value>
        public override string UpdatedBy
        {
            get
            {
                return base.UpdatedBy;
            }
            set
            {
                base.UpdatedBy = value;

                // See if we have any shipment lines
                if (null != shipmentLines)
                {
                    // See if we need to update the updatedby value for our shipment lines
                    foreach (ShipmentLine shipmentLine in shipmentLines)
                    {
                        // If the line is new and has no updatedby set it the the shipments value
                        if (-1 == shipmentLine.Id || string.IsNullOrEmpty(shipmentLine.UpdatedBy))
                        {
                            shipmentLine.UpdatedBy = value;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets The unique identifier for this data, this corresponds with the primary key field in the database.
        /// If this value is null this will signify that the data, when saved, will trigger an insert into the database rather than an update.
        /// </summary>
        /// <value>The primary ID.</value>
        public override int Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                // Store the id
                base.Id = value;

                // See if we have any shipment lines
                if (null != shipmentLines)
                {
                    // we need to update the shipment id's for the lines
                    foreach (ShipmentLine shipmentLine in shipmentLines)
                    {
                        // Update the shipment id
                        shipmentLine.ShipmentId = value;
                    }
                }
            }
        }


        private int auditId = -1;

        /// <summary>
        /// Gets or sets the audit id.
        /// </summary>
        /// <value>The audit id.</value>
        public int AuditId
        {
            get { return auditId; }
            set { auditId = value; }
        }

        private string despatchNumber;

        /// <summary>
        /// For RHG and TPC this should be set to 1.For HSP this should be the despatch number
        /// </summary>
        /// <value>The despatch number.</value>
        [RequiredValidator("Despatch Number is required.", "*")]
        [LengthValidator(1, 10, "Despatch number must be between {0} and {1} characters in length.", "*")]
        public string DespatchNumber
        {
            get { return despatchNumber; }
            set { despatchNumber = value; }
        }

        protected Shipment.StatusEnum status;

        private string customerName;

        /// <summary>
        /// Gets or sets the name of the customer.
        /// </summary>
        /// <value>The name of the customer.</value>
        [RequiredValidator("Customer Name is required.", "*")]
        [LengthValidator(1, 50, "Customer name must be between {0} and {1} characters in length.", "*")]
        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }

        private DateTime requiredShipmentDate;
        private string shipmentNumber;
        private string opCoCode;
        private string routeCode;
        private Int64 opCoSequenceNumber;
        // Transaction Type Code passed from OpCo, etc
        private string transactionTypeCode;

        /// <summary>
        /// OpCo Route Code
        /// </summary>
        public Int64 OpCoSequenceNumber
        {
            get { return opCoSequenceNumber; }
            set { opCoSequenceNumber = value; }
        }

        private System.Collections.Generic.List<ShipmentLine> shipmentLines;

        /// <summary>
        /// Gets or sets the shipment lines.
        /// </summary>
        /// <value>The shipment lines.</value>
        public System.Collections.Generic.List<ShipmentLine> ShipmentLines
        {
            get { return shipmentLines; }
            set { shipmentLines = value; }
        }

        /// <summary>
        /// Gets or sets the route code.
        /// </summary>
        /// <value>The route code.</value>
        [RequiredValidator("Route Code is required.", "*")]
        [LengthValidator(1, 10, "Route code must be between {0} and {1} characters in length.", "*")]
        public virtual string RouteCode
        {
            get { return routeCode; }
            set { routeCode = value; }
        }

        /// <summary>
        /// The TDC types are Stock, Non-stock, TDC collection or Sample
        /// </summary>
        [RequiredValidator("Transaction Type is required.", "*")]
        [LengthValidator(1, 10, "Transaction type must be between {0} and {1} characters in length.", "*")]
        public virtual string TransactionTypeCode
        {
            get { return transactionTypeCode; }
            set { transactionTypeCode = value; }
        }

        /// <summary>
        /// Gets or sets the shipment number.
        /// </summary>
        /// <value>The shipment number.</value>
        [RequiredValidator("Shipment Number is required.", "*")]
        [LengthValidator(1, 10, "Shipment number must be between {0} and {1} characters in length.", "*")]
        public string ShipmentNumber
        {
            get { return shipmentNumber; }
            set { shipmentNumber = value; }
        }

        /// <summary>
        /// Required Date for Shipment
        /// </summary>
        [RequiredValidator("Required shipment Date is required.", "*")]
        public DateTime RequiredShipmentDate
        {
            get { return requiredShipmentDate; }
            set { requiredShipmentDate = value; }
        }

        /// <summary>
        /// Order Status
        /// </summary>
        public virtual Shipment.StatusEnum Status
        {
            get { return status; }
            set { status = value; }
        }

        /// <summary>
        /// Unique OpCo code.  Must be RHG or TPC or HSP.
        /// </summary>
        [RequiredValidator("OpCo Code is required.", "*")]
        [LengthValidator(1, 3, "OpCo code must be between {0} and {1} characters in length.", "*")]
        public string OpCoCode
        {
            get { return opCoCode; }
            set { opCoCode = value; }
        }

        private OpCoContact opCoContact;

        /// <summary>
        /// The Name of the person that should be contacted in the OpCo by the TDC for any enquiry.
        /// </summary>
        public OpCoContact OpCoContact
        {
            get { return opCoContact; }
            set { opCoContact = value; }
        }

        private string customerReference;

        /// <summary>
        /// The customers order number/reference
        /// </summary>
        [LengthValidator(0, 30, "Customer reference must be between {0} and {1} characters in length.", "*")]
        public string CustomerReference
        {
            get { return customerReference; }
            set { customerReference = value; }
        }

        /// <summary>
        /// Shipment Instructions/Directions.Will be printed on the delivery/collection note.
        /// </summary>
        private string instructions;

        /// <summary>
        /// Shipment Instructions/Directions.
        /// </summary>
        [LengthValidator(0, 250, "Instructions must be between {0} and {1} characters in length.", "*")]
        public string Instructions
        {
            get { return instructions; }
            set { instructions = value; }
        }

        private ShipmentAddress shipmentAddress;

        /// <summary>
        /// Gets or sets the shipment address.
        /// </summary>
        /// <value>The shipment address.</value>
        public ShipmentAddress ShipmentAddress
        {
            get { return shipmentAddress; }
            set { shipmentAddress = value; }
        }

        private Contact shipmentContact;

        /// <summary>
        /// Gets or sets the shipment contact.
        /// </summary>
        /// <value>The shipment contact.</value>
        public Contact ShipmentContact
        {
            get { return shipmentContact; }
            set { shipmentContact = value; }
        }

        protected string salesBranchCode;

        /// <summary>
        /// Gets or sets the sales branch code.
        /// </summary>
        /// <value>The sales branch code.</value>
        [RequiredValidator("Sales branch code is required.", "*")]
        [LengthValidator(1, 10, "Sales branch code must be between {0} and {1} characters in length.", "*")]
        public virtual string SalesBranchCode
        {
            get { return salesBranchCode; }
            set { salesBranchCode = value; }
        }

        private string afterTime;

        /// <summary>
        /// Gets or sets the after time.
        /// </summary>
        /// <value>The after time.</value>
        //Technical Spec v1.6 altered this property to non-mandatory [RequiredValidator("After Time is required.", "*")]
        [RegexValidator("^([0-1][0-9]|[2][0-3]):([0-5][0-9])(:[0-5][0-9])?$", "After time must be in format HH:MM(:SS).", "*")]
        public string AfterTime
        {
            get { return afterTime; }
            set { afterTime = value; }
        }

        private string beforeTime;

        /// <summary>
        /// Gets or sets the before time.
        /// </summary>
        /// <value>The before time.</value>
        //Technical Spec v1.6 altered this property to non-mandatory [RequiredValidator("Before Time is required.", "*")]
        [RegexValidator("^([0-1][0-9]|[2][0-3]):([0-5][0-9])(:[0-5][0-9])?$", "Before time must be in format HH:MM(:SS).", "*")]
        public string BeforeTime
        {
            get { return beforeTime; }
            set { beforeTime = value; }
        }

        private bool tailLiftRequired;

        /// <summary>
        /// Indicates if a tail lift vehicle is required
        /// </summary>
        public bool TailLiftRequired
        {
            get { return tailLiftRequired; }
            set { tailLiftRequired = value; }
        }

        private decimal vehicleMaxWeight;

        /// <summary>
        /// The maximum vehicle weight in kilos that can be assigned to the shipment address
        /// </summary>
        [RegexValidator("^([0-9]*)([.][0-9]{2})$", "Vehicle Max Weight must be in format 99999(.99).", "*")]
        public decimal VehicleMaxWeight
        {
            get { return vehicleMaxWeight; }
            set { vehicleMaxWeight = value; }
        }

        private int checkInTime;

        /// <summary>
        /// The estimated time (in minutes) to complete a dropFormat HH:MM:SS with SS always set to zero
        /// </summary>
        [RangeValidator(0, 180, "Check in time must be between {0} and {1} minutes.", "*")]
        public int CheckInTime
        {
            get { return checkInTime; }
            set { checkInTime = value; }
        }

        private CustomerAddress customerAddress;

        /// <summary>
        /// Gets or sets the customer address.
        /// </summary>
        /// <value>The customer address.</value>
        public CustomerAddress CustomerAddress
        {
            get { return customerAddress; }
            set { customerAddress = value; }
        }

        private string customerNumber;

        /// <summary>
        /// Gets or sets the customer number.
        /// </summary>
        /// <value>The customer number.</value>
        //Technical Spec v1.6 altered this property to non-mandatory [RequiredValidator("Customer Number is required.", "*")]
        [LengthValidator(1, 10, "Customer Number must be between {0} and {1} characters in length.", "*")]
        public string CustomerNumber
        {
            get { return customerNumber; }
            set { customerNumber = value; }
        }

        // The stock warehouse code
        protected string stockWarehouseCode;

        /// <summary>
        /// Gets or sets the stock warehouse code.
        /// </summary>
        [RequiredValidator("Stock Warehouse Code is required.", "*")] 
        [LengthValidator(0, 10, "Stock Warehouse Code must be between {0} and {1} characters in length.", "*")]
        public virtual string StockWarehouseCode
        {
            get { return stockWarehouseCode; }
            set { stockWarehouseCode = value; }
        }

        // The delivery warehouse code
        private string deliveryWarehouseCode;

        /// <summary>
        /// Gets or sets the delivery warehouse code.
        /// </summary>
        /// <value>The delivery warehouse code.</value>
        [RequiredValidator("Delivery Warehouse Code is required.", "*")]
        [LengthValidator(1, 10, "Delivery Warehouse Code must be between {0} and {1} characters in length.", "*")]
        public virtual string DeliveryWarehouseCode
        {
            get { return deliveryWarehouseCode; }
            set { deliveryWarehouseCode = value; }
        }

        private string shipmentName;

        /// <summary>
        /// Gets or sets the name of the shipment.
        /// </summary>
        /// <value>The name of the shipment.</value>
        [RequiredValidator("Stock Warehouse Code is required.", "*")]
        [LengthValidator(1, 50, "Shipment Name must be between {0} and {1} characters in length.", "*")]
        public string ShipmentName
        {
            get { return shipmentName; }
            set { shipmentName = value; }
        }

        private string divisionCode;

        /// <summary>
        /// Gets or sets the division code.
        /// </summary>
        /// <value>The division code.</value>
        [RequiredValidator("Division Code is required.", "*")]
        [LengthValidator(1, 3, "Division Code must be between {0} and {1} characters in length.", "*")]
        public virtual string DivisionCode
        {
            get { return divisionCode; }
            set { divisionCode = value; }
        }

        private DateTime generatedDateTime;

        /// <summary>
        /// Gets or sets the generated date time.
        /// </summary>
        /// <value>The generated date time.</value>
        [RequiredValidator("Generated Date Time is required.", "*")]
        public DateTime GeneratedDateTime
        {
            get { return generatedDateTime; }
            set { generatedDateTime = value; }
        }

        private int printCount;

        /// <summary>
        /// Gets or sets the print count.
        /// </summary>
        /// <value>The print count.</value>
        public int PrintCount
        {
            get { return printCount; }
            set { printCount = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is printed.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is printed; otherwise, <c>false</c>.
        /// </value>
        public bool IsPrinted
        {
            get
            {
                return false;
            }
            set
            {
            }
        }

        /// <summary>
        /// Gets the total line quantity.
        /// </summary>
        /// <value>The total line quantity.</value>
        public int TotalLineQuantity
        {
            get
            {
                int totalLineQuantity = 0;
                foreach (ShipmentLine shipmentLine in ShipmentLines)
                {
                    totalLineQuantity += shipmentLine.Quantity;
                }
                return totalLineQuantity;
            }
        }

        private bool opCoHeld;

        /// <summary>
        /// Gets or sets a value indicating whether [op co held].
        /// </summary>
        /// <value><c>true</c> if [op co held]; otherwise, <c>false</c>.</value>
        public bool OpCoHeld
        {
            get { return opCoHeld; }
            set { opCoHeld = value; }
        }

        /// <summary>
        /// Gets the net weight.
        /// </summary>
        /// <value>The net weight.</value>
        public decimal NetWeight
        {
            get
            {
                decimal productsNetWeight = 0;
                foreach (ShipmentLine shipmentLine in ShipmentLines)
                {
                    productsNetWeight += shipmentLine.NetWeight;
                }
                return productsNetWeight;
            }
        }

        private decimal volume = 0;

        /// <summary>
        /// Gets the volume.
        /// </summary>
        /// <value>The volume.</value>
        public decimal Volume
        {
            get
            {
                decimal productsVolume = 0;
                foreach (ShipmentLine shipmentLine in ShipmentLines)
                {
                    productsVolume += shipmentLine.Volume;
                }
                return productsVolume;
            }
        }

        /// <summary>
        /// Gets the gross weight.
        /// </summary>
        /// <value>The gross weight.</value>
        public decimal GrossWeight
        {
            get
            {
                return NetWeight * ((decimal)1.05);
            }
        }

        private Discovery.BusinessObjects.Shipment.TypeEnum type;

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public Discovery.BusinessObjects.Shipment.TypeEnum Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}