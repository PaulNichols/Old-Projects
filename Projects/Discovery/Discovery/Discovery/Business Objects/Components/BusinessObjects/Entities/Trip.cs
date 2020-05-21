using System;
using Discovery.ComponentServices.Parsing;
using Discovery.Utility;

namespace Discovery.BusinessObjects
{
    /*************************************************************************************************
  ** CLASS:	Trip
  **
  ** OVERVIEW:
  ** This is the Trip business object holding details of a sigle trip plus summations of child weights and volumes
  **
  ** MODIFICATION HISTORY:
  **
  ** Date:		Version:    Who:	Change:
  ** 19/7/06	    1.0			PJN		Initial Version
  ************************************************************************************************/
    /// <summary>
    /// A Class 'Trip' which is an entity with namespace Discovery.BusinessObjects
    /// It is inherited from PersistableBusinessObject
    /// The class holds the trip details such as trip number, delivery date, warehouse, etc...
    /// </summary>
    [Serializable]
    public class Trip : PersistableBusinessObject
    {
        #region Private Fields

        private int warehouseId;
        private Warehouse warehouse;
        private string tripNumber;
        private string assignedDriver;
        private string leaveTime;
        private string finishTime;
        private int deliveryCount;
        private int collectionCount;
        private Single deliveryWeight;
        private Single deliveryVolume;
        private Single collectionWeight;
        private Single collectionVolume;
        private Single peakWeight;
        private Single peakVolume;
        private int feasible;
        private int itemCount;
        private Single totalDistance;
        private string travellingTime;
        private string waitingTime;
        private string loadingTime;
        private string totalTime;
        private string vehicleRegistration;
        private Single maximumLoadWeight;
        private Single maximumLoadVolume;
        private Single vehicleCost;
        private int regionId;
        private OptrakRegion optrakRegion;
        private int dropsOnTrip;
        private DateTime startDate;
        
        #endregion

        #region Public Method(s)

        #endregion

        #region Protected Method(s)

        #endregion

        #region Private Method(s)

        #endregion

        #region Constructor(s)

        public Trip()
        {
            WarehouseId = Null.NullInteger;
            RegionId = Null.NullInteger;
        }

        #endregion

         #region Public Properties
      

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
        /// Gets or sets the delivery date.
        /// </summary>
        /// <value>The delivery date.</value>
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        /// <summary>
        /// Gets or sets the warehouse id.
        /// </summary>
        /// <value>The warehouse id.</value>
        public int WarehouseId
        {
            get { return warehouseId; }
            set { warehouseId = value; }
        }

        /// <summary>
        /// Gets or sets the warehouse.
        /// </summary>
        /// <value>The warehouse.</value>
        public Warehouse Warehouse
        {
            get { return warehouse; }
            set { warehouse = value; }
        }

        /// <summary>
        /// Gets or sets the assignedDriver id.
        /// </summary>
        /// <value>The assignedDriver id.</value>
        public string AssignedDriver
        {
            get { return assignedDriver; }
            set { assignedDriver = value; }
        }

        /// <summary>
        /// Gets or sets the leave time.
        /// </summary>
        /// <value>The leave time.</value>
        public string LeaveTime
        {
            get { return leaveTime; }
            set { leaveTime = value; }
        }

        /// <summary>
        /// Gets or sets the finish time.
        /// </summary>
        /// <value>The finish time.</value>
        public string FinishTime
        {
            get { return finishTime; }
            set { finishTime = value; }
        }

        /// <summary>
        /// Gets or sets the W peak.
        /// </summary>
        /// <value>The W peak.</value>
        public Single PeakWeight
        {
            get { return peakWeight; }
            set { peakWeight = value; }
        }

        /// <summary>
        /// Gets or sets the V peak.
        /// </summary>
        /// <value>The V peak.</value>
        public Single PeakVolume
        {
            get { return peakVolume; }
            set { peakVolume = value; }
        }

        /// <summary>
        /// Gets or sets the W cap.
        /// </summary>
        /// <value>The W cap.</value>
        public Single MaximumLoadWeight
        {
            get { return maximumLoadWeight; }
            set { maximumLoadWeight = value; }
        }

        /// <summary>
        /// Gets or sets the V cap.
        /// </summary>
        /// <value>The V cap.</value>
        public Single MaximumLoadVolume
        {
            get { return maximumLoadVolume; }
            set { maximumLoadVolume = value; }
        }

        /// <summary>
        /// Gets or sets the vehicle cost.
        /// </summary>
        /// <value>The vehicle cost.</value>
        public Single VehicleCost
        {
            get { return vehicleCost; }
            set { vehicleCost = value; }
        }

      
        /// <summary>
        /// Gets or sets the optrakRegion id.
        /// </summary>
        /// <value>The optrakRegion id.</value>
        public int RegionId
        {
            get { return regionId; }
            set { regionId = value; }
        }

        /// <summary>
        /// Gets or sets the optrakRegion.
        /// </summary>
        /// <value>The optrakRegion.</value>
        public OptrakRegion OptrakRegion
        {
            get { return optrakRegion; }
            set { optrakRegion = value; }
        }

        /// <summary>
        /// Gets or sets the vehicle registration.
        /// </summary>
        /// <value>The vehicle registration.</value>
        public string VehicleRegistration
        {
            get { return vehicleRegistration; }
            set { vehicleRegistration = value; }
        }

        /// <summary>
        /// Gets or sets the collection weight.
        /// </summary>
        /// <value>The collection weight.</value>
        public Single CollectionWeight
        {
            get { return collectionWeight; }
            set { collectionWeight = value; }
        }

        /// <summary>
        /// Gets or sets the delivery weight.
        /// </summary>
        /// <value>The delivery weight.</value>
        public Single DeliveryWeight
        {
            get { return deliveryWeight; }
            set { deliveryWeight = value; }
        }

        /// <summary>
        /// Gets or sets the collection volume.
        /// </summary>
        /// <value>The collection volume.</value>
        public Single CollectionVolume
        {
            get { return collectionVolume; }
            set { collectionVolume = value; }
        }

        /// <summary>
        /// Gets or sets the delivery volume.
        /// </summary>
        /// <value>The delivery volume.</value>
        public Single DeliveryVolume
        {
            get { return deliveryVolume; }
            set { deliveryVolume = value; }
        }

        /// <summary>
        /// Gets or sets the totalDistance.
        /// </summary>
        /// <value>The totalDistance.</value>
        public Single TotalDistance
        {
            get { return totalDistance; }
            set { totalDistance = value; }
        }

        /// <summary>
        /// Gets or sets the loading time.
        /// </summary>
        /// <value>The loading time.</value>
        public string  LoadingTime
        {
            get { return loadingTime; }
            set { loadingTime = value; }
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
        /// Gets or sets the waiting time.
        /// </summary>
        /// <value>The waiting time.</value>
        public string WaitingTime
        {
            get { return waitingTime; }
            set { waitingTime = value; }
        }

        /// <summary>
        /// Gets or sets the actual number of items."Actual" 
        /// means the value that was found on the trip optrak file rather than a derived value from the 
        /// number of drops with a calltype property value of 1
        /// </summary>
        /// <value>The actual number of items.</value>
        public int ItemCount
        {
            get { return itemCount; }
            set { itemCount = value; }
        }

        /// <summary>
        /// Gets or sets the actual number of deliveries."Actual" 
        /// means the value that was found on the trip optrak file rather than a derived value from the 
        /// number of drops with a calltype property value of 0
        /// </summary>
        /// <value>The actual number of deliveries.</value>
        public int DeliveryCount
        {
            get { return deliveryCount; }
            set { deliveryCount = value; }
        }

        /// <summary>
        /// Gets or sets the actual number of collection. "Actual" 
        /// means the value that was found on the trip optrak file rather than a derived value from the 
        /// number of drops with a calltype property value of 1
        /// </summary>
        /// <value>The actual number of collection.</value>
        public int CollectionCount
        {
            get { return collectionCount; }
            set { collectionCount = value; }
        }

        /// <summary>
        /// Gets or sets the worst error.
        /// </summary>
        /// <value>The worst error.</value>
        public int Feasible
        {
            get { return feasible; }
            set { feasible = value; }
        }

        /// <summary>
        /// Gets or sets the total time. This is the actual total from the Optrak trip file not a derived value
        /// </summary>
        /// <value>The total time.</value>
        public string TotalTime
        {
            get { return totalTime; }
            set { totalTime = value; }
        }

        public int DropsOnTrip
        {
            get { return dropsOnTrip; }
            set { dropsOnTrip = value; }
        }

        #endregion


        /// <summary>
        /// Sets the trip schema.
        /// </summary>
        /// <param name="fields">The fields.</param>
        public static void SetTripSchema(TextFieldCollection fields)
        {
            fields.Add(new TextField("Depot", TypeCode.String, 18));
            fields.Add(new TextField("TripNumber", TypeCode.String, 8));
            fields.Add(new TextField("AssignedDriver", TypeCode.String, 16));
            fields.Add(new TextField("LeaveTime", TypeCode.String, 5));
            fields.Add(new TextField("FinishTime", TypeCode.String, 5));
            fields.Add(new TextField("DeliveryCount", TypeCode.Int32, 3));
            fields.Add(new TextField("CollectionCount", TypeCode.Int32, 3));
            fields.Add(new TextField("DeliveryWeight", TypeCode.Single, 10));
            fields.Add(new TextField("DeliveryVolume", TypeCode.Single, 8));
            fields.Add(new TextField("CollectionWeight", TypeCode.Single, 10));
            fields.Add(new TextField("CollectionVolume", TypeCode.Single, 8));
            fields.Add(new TextField("PeakWeight", TypeCode.Single, 10));
            fields.Add(new TextField("PeakVolume", TypeCode.Single, 8));
            fields.Add(new TextField("Feasible", TypeCode.Int32, 9));
            fields.Add(new TextField("ItemCount", TypeCode.Int32, 3));
            fields.Add(new TextField("TotalDistance", TypeCode.Single, 6));
            fields.Add(new TextField("TravellingTime", TypeCode.String, 5));
            fields.Add(new TextField("WaitingTime", TypeCode.String, 5));
            fields.Add(new TextField("LoadingTime", TypeCode.String, 5));
            fields.Add(new TextField("TotalTime", TypeCode.String, 5));
            fields.Add(new TextField("VehicleRegistration", TypeCode.String, 16));
            fields.Add(new TextField("MaximumLoadWeight", TypeCode.Single, 10));
            fields.Add(new TextField("MaximumLoadVolume", TypeCode.Single, 8));
            fields.Add(new TextField("VehicleCost", TypeCode.Single, 6));
            fields.Add(new TextField("RegionCode", TypeCode.String, 1));
            fields.Add(new TextField("DropsOnTrip", TypeCode.Int32, 3));
            fields.Add(new TextField("StartDate", TypeCode.DateTime, 10));
            
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


