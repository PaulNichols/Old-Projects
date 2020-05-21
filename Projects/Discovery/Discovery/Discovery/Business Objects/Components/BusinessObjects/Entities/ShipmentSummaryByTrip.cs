using System;

namespace Discovery.BusinessObjects
{
    /*************************************************************************************************
   ** CLASS:	ShipmentSummaryByTrip
   **
   ** OVERVIEW:
   ** This class holds the summary details of shipments grouped by route code
   **
   ** MODIFICATION HISTORY:
   **
   ** Date:		Version:    Who:	Change:
   ** 3/8/06	 1.0		PJN		Initial Version
   ************************************************************************************************/
    /// <summary>
    /// A Class 'ShipmentSummaryByTrip' which is an entity with namespace Discovery.BusinessObjects
    /// The class holds the shipment summary details by trip
    /// </summary>

    public class ShipmentSummaryByTrip
    {
        private DateTime requiredDeliveryDate;
        private string trip;
        private string vehicle;
        private string departureTime;
        private string returnTime;
        private int numberOfDrops;
        private decimal deliveryWeight;
        private decimal collectionWeight;
        private decimal deliveryVolume;
        private decimal collectionVolume;
        private decimal distance;


        /// <summary>
        /// Gets or sets the required delivery date.
        /// </summary>
        /// <value>The required delivery date.</value>
        public DateTime RequiredDeliveryDate
        {
            get { return requiredDeliveryDate; }
            set { requiredDeliveryDate = value; }
        }

        /// <summary>
        /// Gets or sets the trip.
        /// </summary>
        /// <value>The trip.</value>
        public string Trip
        {
            get { return trip; }
            set { trip = value; }
        }

        /// <summary>
        /// Gets or sets the vehicle.
        /// </summary>
        /// <value>The vehicle.</value>
        public string Vehicle
        {
            get { return vehicle; }
            set { vehicle = value; }
        }

        /// <summary>
        /// Gets or sets the departure time.
        /// </summary>
        /// <value>The departure time.</value>
        public string DepartureTime
        {
            get { return departureTime; }
            set { departureTime = value; }
        }

        /// <summary>
        /// Gets or sets the return time.
        /// </summary>
        /// <value>The return time.</value>
        public string ReturnTime
        {
            get { return returnTime; }
            set { returnTime = value; }
        }

        /// <summary>
        /// Gets or sets the number of drops.
        /// </summary>
        /// <value>The number of drops.</value>
        public int NumberOfDrops
        {
            get { return numberOfDrops; }
            set { numberOfDrops = value; }
        }

        /// <summary>
        /// Gets or sets the delivery weight.
        /// </summary>
        /// <value>The delivery weight.</value>
        public decimal DeliveryWeight
        {
            get { return deliveryWeight; }
            set { deliveryWeight = value; }
        }

        /// <summary>
        /// Gets or sets the collection weight.
        /// </summary>
        /// <value>The collection weight.</value>
        public decimal CollectionWeight
        {
            get { return collectionWeight; }
            set { collectionWeight = value; }
        }

        /// <summary>
        /// Gets or sets the delivery volume.
        /// </summary>
        /// <value>The delivery volume.</value>
        public decimal DeliveryVolume
        {
            get { return deliveryVolume; }
            set { deliveryVolume = value; }
        }

        /// <summary>
        /// Gets or sets the collection volume.
        /// </summary>
        /// <value>The collection volume.</value>
        public decimal CollectionVolume
        {
            get { return collectionVolume; }
            set { collectionVolume = value; }
        }

        /// <summary>
        /// Gets or sets the distance.
        /// </summary>
        /// <value>The distance.</value>
        public decimal Distance
        {
            get { return distance; }
            set { distance = value; }
        }
    }
}