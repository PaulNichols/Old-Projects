using System;
using System.Web;
using Discovery.Utility;
using ValidationFramework;

namespace Discovery.BusinessObjects
{
    /// <summary>
    /// A Class 'ShipmentLine' which is an entity with namespace Discovery.BusinessObjects
    /// It is inherited from PersistableBusinessObject
    /// The class holds the each shipment line details
    /// </summary>
    [Serializable]
    public abstract class ShipmentLine : PersistableBusinessObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:ShipmentLine"/> class.
        /// </summary>
        public ShipmentLine()
            : base()
        {
            OriginalQuantity = Null.NullInteger;
            int totalLines = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ShipmentLine"/> class.
        /// </summary>
        /// <param name="ShipmentId">The shipment id.</param>
        public ShipmentLine(int ShipmentId)
            : base()
        {
            // Store the specified shipment id
            shipmentId = ShipmentId;
        }

        private int lineNumber;
        private string productCode;
        private string description1;
        private int quantity;
        private int originalQuantity;
        private string quantityUnit;
        private int totalLines;

        /// <summary>
        /// The unit description for the product quantity used for the UNITS field on the delivery/collection notee.g. SHEET
        /// </summary>
        [RequiredValidator("Quantity Unit is required.", "*")]
        [LengthValidator(1, 15, "Quantity unit must be between {0} and {1} characters in length.", "*")]
        public string QuantityUnit
        {
            get { return quantityUnit; }
            set { quantityUnit = value; }
        }

        /// <summary>
        /// Non converted shipped quantity
        /// </summary>
        //[RequiredValidator("Quantity is required.", "*")]
        [RangeValidator(0, 999999999, "Quantity must be between {0} and {1}.", "*")]
        [CustomValidator("","ValidateQuantity","*")]
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        /// <summary>
        /// Custom validation method to validate the quantity
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:ValidationFramework.CustomValidationEventArgs"/> instance containing the event data.</param>
        public void ValidateQuantity(object sender, CustomValidationEventArgs e)
        {
            // Seed the message
            e.ErrorMessage = string.Format("The line quantity cannot be greater than the original quantity of {0}.", OriginalQuantity);
            // Specify if valid, ensure that the current quantity if less than or equal to the original opco quantity
            e.IsValid = (Null.IsNull(OriginalQuantity) || (!Null.IsNull(OriginalQuantity) && Quantity <= OriginalQuantity));
        }

        public int OriginalQuantity
        {
            get { return originalQuantity; }
            set { originalQuantity = value; }
        }


        /// <summary>
        /// Descriptions for the product
        /// </summary>
        [RequiredValidator("Description 1 is required.", "*")]
        [LengthValidator(1, 50, "Description 1 must be between {0} and {1} characters in length.", "*")]
        public string Description1
        {
            get { return description1; }
            set { description1 = value; }
        }

        /// <summary>
        /// OpCo Product Code
        /// </summary>
        [RequiredValidator("Product Code is required.", "*")]
        [LengthValidator(1, 20, "Product code must be between {0} and {1} characters in length.", "*")]
        public string ProductCode
        {
            get { return productCode; }
            set { productCode = value; }
        }

        /// <summary>
        /// Line item Number
        /// </summary>
        //**LAS**[RequiredValidator("Line Number is required.", "*")]
        [RangeValidator(0, 999, "Line number must be between {0} and {1}.", "*")]
        public int LineNumber
        {
            get { return lineNumber; }
            set { lineNumber = value; }
        }

        private decimal netWeight;

        /// <summary>
        /// Gets or sets the net weight.
        /// </summary>
        /// <value>The net weight.</value>
        //**LAS**[RequiredValidator("Net Weight is required.", "*")]
        [RangeValidator(0, 999999999.99, "Line number must be between {0} and {1}.", "*")]
        public decimal NetWeight
        {
            get { return netWeight; }
            set { netWeight = value; }
        }

        /// <summary>
        /// Gets the gross weight, by adding 5% to the Net Weight.
        /// </summary>
        /// <value>The gross weight.</value>
        public decimal GrossWeight
        {
            get
            {
                return NetWeight * ((decimal)1.05);
            }
        }
        
        private decimal volume;

        /// <summary>
        /// Extended volume of the item in cubic metres
        /// </summary>
        [RequiredValidator("-1","Volume is required.", "*")]
        [RangeValidator(0, 99999999999.99, "Volume must be between {0} and {1}.", "*")]
        public decimal Volume
        {
            get { return volume; }
            set { volume = value; }
        }

        private int width;

        /// <summary>
        /// Unit width of the product
        /// </summary>
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        private int length;

        /// <summary>
        /// Unit length of the product
        /// </summary>
        public int Length
        {
            get { return length; }
            set { length = value; }
        }

        private int grammage;

        /// <summary>
        /// The grammage of the product
        /// </summary>
        public int Grammage
        {
            get { return grammage; }
            set { grammage = value; }
        }

        private int microns;

        /// <summary>
        /// The microns of the product
        /// </summary>
        public int Microns
        {
            get { return microns; }
            set { microns = value; }
        }

        private string packing;

        /// <summary>
        /// Packing information for the delivery/collection note
        /// </summary>
        public string Packing
        {
            get { return packing; }
            set { packing = value; }
        }

        private bool isPanel;

        /// <summary>
        /// Indicates if the product is a panel and if it will fit on the lower level compartment of a vehicle. Currently RHG set to true if the product group is either of the following:Foam PVC or Thermo P
        /// </summary>
        public bool IsPanel
        {
            get { return isPanel; }
            set { isPanel = value; }
        }

        protected string loadCategoryCode;

        /// <summary>
        /// The load category used by Optrak. Options are:BP – Bulk Packed, BU – Bundle, BX – Box, CT – Carton, DK - ?,LG – Large,MR – Misc Reel, NA – Unknown, PK – Packet, R3 – 3K Ree, lR7 – 7K Reel, R9 – 9K Reel, UN – Unit
        /// </summary>
        //***LAS** 1.6 Spec [RequiredValidator("Load Category Code is required.", "*")]
        [LengthValidator(0, 2, "Load category code must be between {0} and {1} characters in length.", "*")]
        public virtual string LoadCategoryCode
        {
            get { return loadCategoryCode; }
            set { loadCategoryCode = value; }
        }

        private string productGroup;

        /// <summary>
        /// Gets or sets the product group.
        /// </summary>
        /// <value>The product group.</value>
        public string ProductGroup
        {
            get { return productGroup; }
            set { productGroup = value; }
        }

        private bool isISO9000Approved;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is ISO 9000 approved.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is ISO 9000 approved; otherwise, <c>false</c>.
        /// </value>
        public bool IsISO9000Approved
        {
            get { return isISO9000Approved; }
            set { isISO9000Approved = value; }
        }

        private string exceptions;

        /// <summary>
        /// Gets or sets the exceptions.
        /// </summary>
        /// <value>The exceptions.</value>
        public string Exceptions
        {
            get { return exceptions; }
            set { exceptions = value; }
        }

        private int shipmentId;

        /// <summary>
        /// Gets or sets the shipment id.
        /// </summary>
        /// <value>The shipment id.</value>
        public int ShipmentId
        {
            get { return shipmentId; }
            set { shipmentId = value; }
        }

        private string description2;

        /// <summary>
        /// Gets or sets the description2.
        /// </summary>
        /// <value>The description2.</value>
        public string Description2
        {
            get { return description2; }
            set { description2 = value; }
        }

        private string customerReference;

        /// <summary>
        /// Gets or sets the customer reference.
        /// </summary>
        /// <value>The customer reference.</value>
        public string CustomerReference
        {
            get { return customerReference; }
            set { customerReference = value; }
        }

        private string conversionInstructions;

        /// <summary>
        /// Gets or sets the conversion instructions.
        /// </summary>
        /// <value>The conversion instructions.</value>
        public string ConversionInstructions
        {
            get { return conversionInstructions; }
            set { conversionInstructions = value; }
        }

        private int conversionQuantity;

        /// <summary>
        /// Gets or sets the conversion quantity.
        /// </summary>
        /// <value>The conversion quantity.</value>
        public int ConversionQuantity
        {
            get { return conversionQuantity; }
            set { conversionQuantity = value; }
        }

        public decimal EquivelentPallets
        {
            get
            {
                return NetWeight / 500;
            }
        }

        // As per 1.6 Spec
        public decimal EquivalentPallets
        {
            get
            {
                return NetWeight / 500;
            }
        }

        // As per 1.6 Spec
        public int EquivalentPicks
        {
            get
            {
                return (int) (NetWeight / 500);
            }
        }

        // % of total lines as per 1.6 Spec
        public int LinePercent
        {
            get
            {
                return 1 / TotalLines;
            }
        }

        // Total number of lines in the shipment, must be specified via the controller as per 1.6 Spec
        public int TotalLines
        {
            get
            {
                return totalLines;
            }
            set
            {
                totalLines = value;
            }
        }
    }
}