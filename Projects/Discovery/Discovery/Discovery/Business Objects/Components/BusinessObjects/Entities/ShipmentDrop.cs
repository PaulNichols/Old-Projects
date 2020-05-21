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
    public class ShipmentDrop : PersistableBusinessObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:ShipmentDrop"/> class.
        /// </summary>
        public ShipmentDrop()
        {
            tripId = Null.NullInteger;
            shipmentId = Null.NullInteger;
            originalDepotId = Null.NullInteger;
        }

        public enum CallTypeEnum
        {
            Delivery = 0,
            Collection=1,
            Depot=2
        }

        private Int32 tripId;
        private string tripNumber;
        private Int32 orderSequence;
        private string originalCustomerCode;
        private string shipmentNumberAndDespatch;
        private int shipmentId;
        private Single weight;
        private Single volume;
        private string arriveTime;
        private string departTime;
        private string loadingTime;
        private string waitingTime;
        private string travellingTime;
        private Single distance;
        private CallTypeEnum callType;
        private int dropSequence;
        private int originalDepotId;
        private string originalDepot;
        private DateTime deliveryDate;
        

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        /// <value>The volume.</value>
        public Single Volume
        {
            get { return volume; }
            set { volume = value; }
        }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        /// <value>The weight.</value>
        public Single Weight
        {
            get { return weight; }
            set { weight = value; }
        }


        /// <summary>
        /// Gets or sets the drop orderSequence number. A number shared across orders that are on the same drop
        /// </summary>
        /// <value>The drop orderSequence number.</value>
        public int DropSequence
        {
            get { return dropSequence; }
            set { dropSequence = value; }
        }

        /// <summary>
        /// Gets or sets the depart time.
        /// </summary>
        /// <value>The depart time.</value>
        public string DepartTime
        {
            get { return departTime; }
            set { departTime = value; }
        }

        /// <summary>
        /// Gets or sets the arrive time.
        /// </summary>
        /// <value>The arrive time.</value>
        public string ArriveTime
        {
            get { return arriveTime; }
            set { arriveTime = value; }
        }

        /// <summary>
        /// Gets or sets the loading time.
        /// </summary>
        /// <value>The loading time.</value>
        public string LoadingTime
        {
            get { return loadingTime; }
            set { loadingTime = value; }
        }

        /// <summary>
        /// Gets or sets the distance.
        /// </summary>
        /// <value>The distance.</value>
        public Single Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        /// <summary>
        /// Gets or sets the travelling time.
        /// </summary>
        /// <value>The travelling time.</value>
        public string TravellingTime
        {
            get { return travellingTime; }
            set { travellingTime = value; }
        }

        /// <summary>
        /// Gets the opco code.
        /// </summary>
        /// <value>The opco code.</value>
        public string OpcoCode
        {
            get
            {
                if (!string.IsNullOrEmpty(ShipmentNumberAndDespatch) && ShipmentNumberAndDespatch.IndexOf('-') > -1)
                {
                    return ShipmentNumberAndDespatch.Split('-')[0].ToString().Trim();
                }
                return "";
            }
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
                if (!string.IsNullOrEmpty(ShipmentNumberAndDespatch) && ShipmentNumberAndDespatch.IndexOf('-') > -1)
                {
                    string[] parts = ShipmentNumberAndDespatch.Split('-');
                    if (parts.Length==3)
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
                if (!string.IsNullOrEmpty(ShipmentNumberAndDespatch) && ShipmentNumberAndDespatch.IndexOf('-') > -1)
                {
                    string[] parts = ShipmentNumberAndDespatch.Split('-');
                    if (parts.Length >= 2)
                    {
                        return parts[1].ToString().Trim();
                    }
                }
                return "";
            }
        }


        /// <summary>
        /// Gets or sets the shipment number and despatch.
        /// </summary>
        /// <value>The shipment number and despatch.</value>
        public string ShipmentNumberAndDespatch
        {
            get { return shipmentNumberAndDespatch; }
            set
            {
                shipmentNumberAndDespatch =  value;
            }
        }

        /// <summary>
        /// Gets or sets the trip id this drop relates to.
        /// </summary>
        /// <value>The trip id.</value>
        public int TripId
        {
            get { return tripId; }
            set { tripId = value; }
        }

        /// <summary>
        /// Gets or sets the orderSequence. A sequential number across all drops within a trip
        /// </summary>
        /// <value>The orderSequence.</value>
        public int OrderSequence
        {
            get { return orderSequence; }
            set { orderSequence = value; }
        }

        /// <summary>
        /// Gets or sets the waiting time.
        /// </summary>
        /// <value>The waiting time.</value>
        public string WaitingTime
        {
            get { return waitingTime; }
            set { waitingTime = value; }
        }

        /// <summary>
        /// Gets or sets the type of the call.
        /// </summary>
        /// <value>The type of the call.</value>
        public CallTypeEnum CallType
        {
            get { return callType; }
            set { callType = value; }
        }

        /// <summary>
        /// Gets or sets the original depot code.
        /// </summary>
        /// <value>The original depot.</value>
        public string OriginalDepot
        {
            get { return originalDepot; }
            set { originalDepot = value; }
        }

        /// <summary>
        /// Gets or sets the original customer code.
        /// </summary>
        /// <value>The original customer code.</value>
        public string OriginalCustomerCode
        {
            get { return originalCustomerCode; }
            set { originalCustomerCode = value; }
        }

        /// <summary>
        /// Gets or sets the shipment id.
        /// </summary>
        /// <value>The shipment id.</value>
        public int ShipmentId
        {
            get { return shipmentId; }
            set { shipmentId = value; }
        }

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
        /// Gets or sets the trip number.
        /// </summary>
        /// <value>The trip number.</value>
        public string TripNumber
        {
            get { return tripNumber; }
            set { tripNumber = value; }
        }

       public int OriginalDepotId
        {
            get { return originalDepotId; }
            set { originalDepotId = value; }
        }


        /// <summary>
        /// Sets the drop schema.
        /// </summary>
        /// <param name="fields">The fields.</param>
        public static void SetDropSchema(TextFieldCollection fields)
        {
            fields.Add(new TextField("TripNumber", TypeCode.String, 8));
            fields.Add(new TextField("OrderSequence", TypeCode.Int32, 3));
            fields.Add(new TextField("OriginalCustomerCode", TypeCode.String, 16));
            fields.Add(new TextField("ShipmentNumberAndDespatch", TypeCode.String, 22));
            fields.Add(new TextField("Weight", TypeCode.Single, 10));
            fields.Add(new TextField("Volume", TypeCode.Single, 8));
            fields.Add(new TextField("ArriveTime", TypeCode.String, 5));
            fields.Add(new TextField("DepartTime", TypeCode.String, 5));
            fields.Add(new TextField("LoadingTime", TypeCode.String, 5));
            fields.Add(new TextField("WaitingTime", TypeCode.String, 5));
            fields.Add(new TextField("TravellingTime", TypeCode.String, 5));
            fields.Add(new TextField("Distance", TypeCode.Single, 6));
            fields.Add(new TextField("CallType", TypeCode.Int32, 1));
            fields.Add(new TextField("DropSequence", TypeCode.Int32, 3));
            fields.Add(new TextField("RegionAndRoute", TypeCode.String,5 ));
            fields.Add(new TextField("OriginalDepot", TypeCode.String, 18));
            fields.Add(new TextField("DeliveryDate", TypeCode.DateTime, 10));

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