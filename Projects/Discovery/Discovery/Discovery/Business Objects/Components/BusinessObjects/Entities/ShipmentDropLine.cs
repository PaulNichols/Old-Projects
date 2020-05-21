using System;
using Discovery.ComponentServices.Parsing;
using Discovery.Utility;

namespace Discovery.BusinessObjects
{
    /// <summary>
    /// A Class 'ShipmentDrop' which is an entity with namespace Discovery.BusinessObjects
    /// It is inherited from PersistableBusinessObject
    /// The class holds the drop details
    /// </summary>
    [Serializable]
    public class ShipmentDropLine : PersistableBusinessObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:ShipmentDropLine"/> class.
        /// </summary>
        public ShipmentDropLine()
        {
            ShipmentLineId = Null.NullInteger;
            DropId = Null.NullInteger;
            OriginalShipmentLineId = Null.NullInteger;
        }

        private DateTime deliveryDate;
        private string depot;
        private string tripNumber;
        private int dropSequence;
        private int orderSequence;
        private string product;
        private string orderCode;
        private string siteCode;
        private int lineCode;
        private int quantity;
        private Single weight;
        private Single volume;
        private bool split;
        private int shipmentLineId;
        private int dropId;
        private int originalShipmentLineId;
        
        /// <summary>
        /// Gets or sets the delivery date.
        /// </summary>
        /// <value>The delivery date.</value>
        public DateTime DeliveryDate
        {
            get { return deliveryDate; }
            set { deliveryDate = value; }
        }

        /// <summary>
        /// Gets or sets the depot.
        /// </summary>
        /// <value>The depot.</value>
        public string Depot
        {
            get { return depot; }
            set { depot = value; }
        }

        /// <summary>
        /// Gets or sets the trip number.
        /// </summary>
        /// <value>The trip number.</value>
        public string TripNumber
        {
            get { return tripNumber; }
            set { tripNumber = value; }
        }

        /// <summary>
        /// Gets or sets the drop sequence.
        /// </summary>
        /// <value>The drop sequence.</value>
        public int DropSequence
        {
            get { return dropSequence; }
            set { dropSequence = value; }
        }

        /// <summary>
        /// Gets or sets the order sequnce.
        /// </summary>
        /// <value>The order sequnce.</value>
        public int OrderSequence
        {
            get { return orderSequence; }
            set { orderSequence = value; }
        }

        /// <summary>
        /// Gets the despatch number. This is derived from the Shipment and despatch number 
        /// which are concatinated by a hyphen
        /// </summary>
        /// <value>The despatch number.</value>
        public string DespatchNumber
        {
            get
            {
                if (!string.IsNullOrEmpty(orderCode) && orderCode.IndexOf('-') > -1)
                {
                    string[] parts = orderCode.Split('-');
                    if (parts.Length == 3)
                    {
                        return parts[2].ToString().Trim();
                    }
                }
                return "";
            }
        }
        
        /// <summary>
        /// Gets or sets the shipment number.
        /// </summary>
        /// <value>The shipment number.</value>
        public string ShipmentNumber
        {
            get
            {
                if (!string.IsNullOrEmpty(orderCode) && orderCode.IndexOf('-') > -1)
                {
                    string[] parts = orderCode.Split('-');
                    if (parts.Length >= 2)
                    {
                        return parts[1].ToString().Trim();
                    }
                }
                return "";
            }
        }
        
        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>The product.</value>
        public string Product
        {
            get { return product; }
            set { product = value; }
        }

        public string OrderCode
        {
            get { return orderCode; }
            set { orderCode =  value;}
        }

        public string SiteCode
        {
            get { return siteCode; }
            set { siteCode = value; }
        }

        public int LineCode
        {
            get { return lineCode; }
            set { lineCode = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public Single Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        public Single Volume
        {
            get { return volume; }
            set { volume = value; }
        }

        public bool Split
        {
            get { return split; }
            set { split = value; }
        }

        public int ShipmentLineId
        {
            get { return shipmentLineId; }
            set { shipmentLineId = value; }
        }

        public string OpcoCode
        {
            get
            {
                if (!string.IsNullOrEmpty(OrderCode) && OrderCode.IndexOf('-') > -1)
                {
                    return OrderCode.Split('-')[0].ToString().Trim();
                }
                return "";
            }
        }

        public int DropId
        {
            get { return dropId; }
            set { dropId = value; }
        }

        public int OriginalShipmentLineId
        {
            get { return originalShipmentLineId; }
            set { originalShipmentLineId = value; }
        }

        /// <summary>
        /// Sets the drop schema.
        /// </summary>
        /// <param name="fields">The fields.</param>
        public static void SetDropLineSchema(TextFieldCollection fields)
        {
            fields.Add(new TextField("DeliveryDate", TypeCode.DateTime, 10));
            fields.Add(new TextField("Depot", TypeCode.String, 18));
            fields.Add(new TextField("TripNumber", TypeCode.String, 8));
            fields.Add(new TextField("DropSequence", TypeCode.Int32, 3));
            fields.Add(new TextField("OrderSequence", TypeCode.Int32, 3));
            fields.Add(new TextField("Product", TypeCode.String, 16));
            fields.Add(new TextField("OrderCode", TypeCode.String, 22));
            fields.Add(new TextField("SiteCode", TypeCode.String, 16));
            fields.Add(new TextField("LineCode", TypeCode.Int32, 3));
            fields.Add(new TextField("Quantity", TypeCode.Int32, 9));
            fields.Add(new TextField("Weight", TypeCode.Single, 10));
            fields.Add(new TextField("Volume", TypeCode.Single, 8));
            fields.Add(new TextField("Split", TypeCode.Boolean, 1));
           
            ////each field has an extra space between it!
            //foreach (TextField field in fields)
            //{
            //    field.Length += 1;
            //}
            ////we don't need an extra space on the last field
            //fields[fields.Count - 1].Length -= 1;
        }
    }
}