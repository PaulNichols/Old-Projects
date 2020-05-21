using System;
using System.Text;
using Discovery.ComponentServices.Parsing;

namespace Discovery.BusinessObjects
{
    /*************************************************************************************************
   ** CLASS:	CommanderSalesOrderLine
   **
   ** OVERVIEW:
   ** This class represents a single commander sales order line
   **
   ** MODIFICATION HISTORY:
   **
   ** Date:		Version:    Who:	Change:
   ** 19/7/06	1.0			PJN		Initial Version
   ************************************************************************************************/
    /// <summary>
    /// A class 'CommanderSalesOrderLine' which is an entity with namespace Discovery.BusinessObjects
    /// It is inherited from PersistableBusinessObject
    /// </summary>
    [Serializable]
    public class CommanderSalesOrderLine : PersistableBusinessObject
    {
        #region Private Fields

        private int commanderSalesOrderId;
        //  private CommanderSalesOrder commanderSalesOrder;
        private string site;
        private string orderReference;
        private int lineNumber;
        private string productCode;
        private int quantityOrdered;
        private string customerReferenceNumber;
        private string uom;
        private string specialInstructions1;
        private string specialInstructions2;
        private string specialInstructions3;
        private string specialInstructions4;
        private string specialInstructions5;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the commander sales order id.
        /// </summary>
        /// <value>The commander sales order id.</value>
        public int CommanderSalesOrderId
        {
            get { return commanderSalesOrderId; }
            set { commanderSalesOrderId = value; }
        }

        /// <summary>
        /// Gets or sets the order reference. This value will be the same as the OpcoShipment Shipment Number.
        /// </summary>
        /// <value>The order reference.</value>
        public string OrderReference
        {
            get { return orderReference; }
            set { orderReference = value; }
        }

        /// <summary>
        /// Gets or sets the special instructions1. Special instructions to the Picker
        /// </summary>
        /// <value>The special instructions1.</value>
        public string SpecialInstructions1
        {
            get { return specialInstructions1; }
            set { specialInstructions1 = value; }
        }

        ///// <summary>
        ///// Gets or sets the commander sales order.
        ///// </summary>
        ///// <value>The commander sales order.</value>
        //public CommanderSalesOrder CommanderSalesOrder
        //{
        //    get 
        //    { 
        //        return commanderSalesOrder; 
        //    }
        //    set 
        //    { 
        //        commanderSalesOrder = value; 
        //    }
        //}

        /// <summary>
        /// Gets or sets the site. This value will be derived in the Application Server and will be the TPC Branch Code for the TDC Delivery SalesLocation for this Shipment.
        /// </summary>
        /// <value>The site.</value>
        public string Site
        {
            get { return site; }
            set { site = value; }
        }

        /// <summary>
        /// Gets or sets the line number. This value will be the same as the OpcoShipmentLine line number.
        /// </summary>
        /// <value>The line number.</value>
        public int LineNumber
        {
            get { return lineNumber; }
            set { lineNumber = value; }
        }

        /// <summary>
        /// Gets or sets the quantity ordered. This value will be the same as the OpcoShipmentLine quantity.
        /// </summary>
        /// <value>The quantity ordered.</value>
        public int QuantityOrdered
        {
            get { return quantityOrdered; }
            set { quantityOrdered = value; }
        }

        /// <summary>
        /// Gets or sets the product code. This value will be the same as the OpcoShipmentLine product code.
        /// </summary>
        /// <value>The product code.</value>
        public string ProductCode
        {
            get { return productCode; }
            set { productCode = value; }
        }

        /// <summary>
        /// Gets or sets the customer reference number. This value will be the same as the OpcoShipmentLine Customer Number.
        /// </summary>
        /// <value>The customer reference number.</value>
        public string CustomerReferenceNumber
        {
            get { return customerReferenceNumber; }
            set { customerReferenceNumber = value; }
        }

        /// <summary>
        /// Gets or sets the UOM. This value will be the same as the OpcoShipmentLine unit of measure.
        /// </summary>
        /// <value>The UOM.</value>
        public string UOM
        {
            get { return uom; }
            set { uom = value; }
        }

        /// <summary>
        /// Gets or sets the special instructions2. Special instructions to the Picker
        /// </summary>
        /// <value>The special instructions2.</value>
        public string SpecialInstructions2
        {
            get { return specialInstructions2; }
            set { specialInstructions2 = value; }
        }

        /// <summary>
        /// Gets or sets the special instructions3. Special instructions to the Picker
        /// </summary>
        /// <value>The special instructions3.</value>
        public string SpecialInstructions3
        {
            get { return specialInstructions3; }
            set { specialInstructions3 = value; }
        }

        /// <summary>
        /// Gets or sets the special instructions4. Special instructions to the Picker
        /// </summary>
        /// <value>The special instructions4.</value>
        public string SpecialInstructions4
        {
            get { return specialInstructions4; }
            set { specialInstructions4 = value; }
        }

        /// <summary>
        /// Gets or sets the special instructions5. Special instructions to the Picker
        /// </summary>
        /// <value>The special instructions5.</value>
        public string SpecialInstructions5
        {
            get { return specialInstructions5; }
            set { specialInstructions5 = value; }
        }


       

        #endregion

        #region Public Method(s)

        /// <summary>
        /// Sets the commander sales order lines schema.
        /// </summary>
        /// <param name="fields">The fields.</param>
        public static void SetCommanderSalesOrderLinesSchema(TextFieldCollection fields)
        {
            //set-up fields collection, make sure the field names are actually the same as the 
            //CommanderSalesOrderLine class properties so that reflection can be used in the parsing subscriber
            fields.Clear();
            
            fields.Add(new TextField("LineNumber", TypeCode.Int32, 5));
            fields.Add(new TextField("RecordType", TypeCode.Int32, 3));
            fields.Add(new TextField("RecordSubType", TypeCode.String, 1));
            fields.Add(new TextField("Site", TypeCode.String, 10));
            fields.Add(new TextField("OrderReference", TypeCode.String, 20));
            fields.Add(new TextField("LineNumber", TypeCode.Int32, 5));
            fields.Add(new TextField("ProductCode", TypeCode.String, 20));
            fields.Add(new TextField("QuantityOrdered", TypeCode.Int32, 19));
            fields.Add(new TextField("CustomerReferenceNumber", TypeCode.String, 20));
            fields.Add(new TextField("UOM", TypeCode.String, 10));
            fields.Add(new TextField("SpecialInstructions1", TypeCode.String, 33));
            fields.Add(new TextField("SpecialInstructions2", TypeCode.String, 33));
            fields.Add(new TextField("SpecialInstructions3", TypeCode.String, 33));
            fields.Add(new TextField("SpecialInstructions4", TypeCode.String, 33));
            fields.Add(new TextField("SpecialInstructions5", TypeCode.String, 33));
        }

        /// <summary>
        /// Generates a fixed length set of values for this commander sales line instance. This will be used as output to Commander WMS.
        /// </summary>
        /// <returns></returns>
        public string GenerateLinesCSV()
        {
            TextFieldCollection fields = new TextFieldCollection();
            SetCommanderSalesOrderLinesSchema(fields);

            StringBuilder stringBuilder = new StringBuilder();

            CommanderSalesOrder.FormatField(this, fields, stringBuilder);

            return stringBuilder.ToString();
        }
        
        #endregion

        #region Protected Method(s)

        #endregion

        #region Private Method(s)

        #endregion

        #region Constructor(s)

        #endregion
    }
}