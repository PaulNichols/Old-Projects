using System;
using System.Collections.Generic;
using System.Text;
using Discovery.ComponentServices.Parsing;

namespace Discovery.BusinessObjects
{
    /// <summary>
    /// This class holds the state of a single Commander Sales Order. The CommanderSalesOrderParsing subscriber will typically create an instance of this class.
    /// </summary>
    [Serializable]
    public class CommanderSalesOrder : PersistableBusinessObject
    {
        #region Private Fields

        private string site;
        private string orderReference;
        private string customerNumber;
        private string despatchRouteCode;
        private int dropNumber;
        private decimal totalWeight;
        private Address deliveryAddress;
        private string customerOrderReference;
        private string carrier;
        private string customerType;
        private List<CommanderSalesOrderLine> lines;

        #endregion

        #region Public Properties

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
        /// Gets or sets the despatch route code. This value will be derived in the Application Server and will be the TPC Route Code for the TDC Delivery SalesLocation for the related OpcoShipment
        /// </summary>
        /// <value>The despatch route code.</value>
        public string DespatchRouteCode
        {
            get { return despatchRouteCode; }
            set { despatchRouteCode = value; }
        }

        /// <summary>
        /// Gets or sets the order reference. This value will be the same as the related OpcoShipment Shipment Number.
        /// </summary>
        /// <value>The order reference.</value>
        public string OrderReference
        {
            get { return orderReference; }
            set { orderReference = value; }
        }


        /// <summary>
        /// Gets or sets the customer number. This value will be the same as the related OpcoShipment Customer Number.
        /// </summary>
        /// <value>The customer number.</value>
        public string CustomerNumber
        {
            get { return customerNumber; }
            set { customerNumber = value; }
        }

        /// <summary>
        /// Gets or sets the drop number. This value will always be set to 1.
        /// </summary>
        /// <value>The drop number.</value>
        public int DropNumber
        {
            get { return dropNumber; }
            set { dropNumber = value; }
        }

        /// <summary>
        /// Gets or sets the total weight. This value will be the sum of gross weight from the related OpcoShipment.
        /// </summary>
        /// <value>The total weight.</value>
        public decimal TotalWeight
        {
            get { return totalWeight; }
            set { totalWeight = value; }
        }

        /// <summary>
        /// Gets or sets the delivery address. This class will hold address lines 1 to 5 from the related OpcoShipment.
        /// </summary>
        /// <value>The delivery address.</value>
        public Address DeliveryAddress
        {
            get { return deliveryAddress; }
            set { deliveryAddress = value; }
        }

        /// <summary>
        /// Gets or sets the customer order reference. This value will be the same as the related OpcoShipment Customer Reference number.
        /// </summary>
        /// <value>The customer order reference.</value>
        public string CustomerOrderReference
        {
            get { return customerOrderReference; }
            set { customerOrderReference = value; }
        }

        /// <summary>
        /// Gets or sets the carrier. This value will always be set to 1.
        /// </summary>
        /// <value>The carrier.</value>
        public string Carrier
        {
            get { return carrier; }
            set { carrier = value; }
        }

        /// <summary>
        /// Gets or sets the type of the customer. ??
        /// </summary>
        /// <value>The type of the customer.</value>
        public string CustomerType
        {
            get { return customerType; }
            set { customerType = value; }
        }

        /// <summary>
        /// Gets or sets the lines. A collection of the related CommanderSalesOrderLine objects.
        /// </summary>
        /// <value>The lines.</value>
        public List<CommanderSalesOrderLine> Lines
        {
            get { return lines; }
        }


       

        #endregion

        #region Public Method(s)

        /// <summary>
        /// Generates an array Lines delimited values. This method will iterate through the collection of Lines calling the GenerateLinesCSV() method. The return value from each call will be gathered together and returned as string array.
        /// </summary>
        /// <returns></returns>
        public string[] GenerateLinesCSV()
        {
            List<string> generaredLines = new List<string>();
            foreach (CommanderSalesOrderLine line in Lines)
            {
                generaredLines.Add(line.GenerateLinesCSV());
            }

            return generaredLines.ToArray();
        }

        /// <summary>
        /// Generates a delimited set of values for this instance for output to Commander WMS.
        /// </summary>
        /// <returns></returns>
        public string GenerateCSV()
        {
            TextFieldCollection fields = new TextFieldCollection();
            SetCommanderSalesOrderHeaderSchema(fields);

            StringBuilder stringBuilder = new StringBuilder();

            FormatField(this,fields, stringBuilder);

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Formats the each field based on it's type and length properties and appends each field to a 
        /// string builder for use by the caller.
        /// </summary>
        /// <param name="currentInstance">The current buisiness object.</param>
        /// <param name="fields">The TextField Collection to iterate through.</param>
        /// <param name="stringBuilder">The string builder. Passed by reference for the caller to use</param>
        internal static void FormatField(DiscoveryBusinessObject currentInstance, TextFieldCollection fields, StringBuilder stringBuilder)
        {
            foreach (TextField field in fields)
            {
                string unformattedValue = currentInstance.GetType().GetProperty(field.Name).GetValue(currentInstance, null).ToString();
                //default the formatted value to be the unformatted value
                string formattedValue = unformattedValue;

                switch (field.DataType)
                {
                    case TypeCode.String:
                        {
                            formattedValue = unformattedValue.PadRight(field.Length);
                            break;
                        }
                    case TypeCode.Decimal:
                        {
                            formattedValue = string.Format("#.00", unformattedValue).PadLeft(field.Length, '0');
                            break;
                        }
                    case TypeCode.Int32:
                        {
                            formattedValue = unformattedValue.PadLeft(field.Length, '0');
                            break;
                        }
                }
                stringBuilder.Append(formattedValue);
            }
        }
        /// <summary>
        /// Sets the commander sales order file header schema.
        /// </summary>
        /// <param name="fields">The fields.</param>
        public static void SetCommanderSalesOrderFileHeaderSchema(TextFieldCollection fields)
        {
            fields.Clear();
            
            fields.Add(new TextField("LineNumber", TypeCode.Int32, 5));
            fields.Add(new TextField("RecordType", TypeCode.Int32, 3));
            fields.Add(new TextField("RecordSubType", TypeCode.String, 1));
            fields.Add(new TextField("Date", TypeCode.String, 10));
            fields.Add(new TextField("Time", TypeCode.String, 8));
          
        }
        /// <summary>
        /// Sets the commander sales order header schema.
        /// </summary>
        /// <param name="fields">The fields.</param>
        public static void SetCommanderSalesOrderHeaderSchema(TextFieldCollection fields)
        {
            //set-up fields collection, make sure the field names are the same as the 
            //CommanderSalesOrder class properties so that reflection can be used in the parsing subscriber

            fields.Clear();
            
            fields.Add(new TextField("LineNumber", TypeCode.Int32, 5));
            fields.Add(new TextField("RecordType", TypeCode.Int32, 3));
            fields.Add(new TextField("RecordSubType", TypeCode.String, 1));
            fields.Add(new TextField("Site", TypeCode.String, 10));
            fields.Add(new TextField("OrderReference", TypeCode.String, 20));
            fields.Add(new TextField("CustomerNumber", TypeCode.String, 10));
            fields.Add(new TextField("DespatchRouteCode", TypeCode.String, 10));
            fields.Add(new TextField("DropNumber", TypeCode.Int32, 6));
            fields.Add(new TextField("TotalWeight", TypeCode.Decimal, 19));
            fields.Add(new TextField("DeliveryAddress.Line1", TypeCode.String, 32));
            fields.Add(new TextField("DeliveryAddress.Line2", TypeCode.String, 32));
            fields.Add(new TextField("DeliveryAddress.Line3", TypeCode.String, 32));
            fields.Add(new TextField("DeliveryAddress.Line4", TypeCode.String, 32));
            fields.Add(new TextField("DeliveryAddress.Line5", TypeCode.String, 32));
            fields.Add(new TextField("CustomerOrderReference", TypeCode.String, 32));
            fields.Add(new TextField("Carrier", TypeCode.String, 4));
            fields.Add(new TextField("CustomerType", TypeCode.String, 3));
        }

        #endregion

        #region Protected Method(s)

        #endregion

        #region Private Method(s)

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="T:CommanderSalesOrder"/> class.
        /// </summary>
        public CommanderSalesOrder()
            : base()
        {
            lines = new List<CommanderSalesOrderLine>();
        }

        #endregion
    }
}