using System;

namespace Discovery.BusinessObjects
{
    /*************************************************************************************************
  ** CLASS:	Trip
  **
  ** OVERVIEW:
  ** This class holds the summary details used in the generate optrak screen 
  ** for shipments merged at a single delivery point
  **
  ** MODIFICATION HISTORY:
  **
  ** Date:		Version:    Who:	Change:
  ** 19/9/06	1.0			PJN		Initial Version
  ************************************************************************************************/
    /// <summary>
    /// A Class 'MergedShipment' which is an entity with namespace Discovery.BusinessObjects
    /// The class holds the merged shipment details
    /// </summary>

    [Serializable]
    public class MergedShipment
    {
        #region Private Fields

        private int shipmentCount;
        private int customerCount;
        private int siteCode;
        private string shipmentName;
        private string addressLine1;
        private string postCode;
        private string dpsCode;
        private string deliveryWarehouseCode;
        
        #endregion

     
         #region Public Properties

        /// <summary>
        /// Gets or sets the site code.
        /// </summary>
        /// <value>The site code.</value>
        public int SiteCode
        {
            get { return siteCode; }
            set { siteCode = value; }
        }

        /// <summary>
        /// Gets or sets the name of the shipment.
        /// </summary>
        /// <value>The name of the shipment.</value>
        public string ShipmentName
        {
            get { return shipmentName; }
            set { shipmentName = value; }
        }

        /// <summary>
        /// Gets or sets the address line1.
        /// </summary>
        /// <value>The address line1.</value>
        public string AddressLine1
        {
            get { return addressLine1; }
            set { addressLine1 = value; }
        }

        /// <summary>
        /// Gets or sets the post code.
        /// </summary>
        /// <value>The post code.</value>
        public string PostCode
        {
            get { return postCode; }
            set { postCode = value; }
        }

        /// <summary>
        /// Gets or sets the DPS code.
        /// </summary>
        /// <value>The DPS code.</value>
        public string DpsCode
        {
            get { return dpsCode; }
            set { dpsCode = value; }
        }

        /// <summary>
        /// Gets or sets the shipment count.
        /// </summary>
        /// <value>The shipment count.</value>
        public int ShipmentCount
        {
            get { return shipmentCount; }
            set { shipmentCount = value; }
        }

        /// <summary>
        /// Gets or sets the customer count.
        /// </summary>
        /// <value>The customer count.</value>
        public int CustomerCount
        {
            get { return customerCount; }
            set { customerCount = value; }
        }

        /// <summary>
        /// Gets or sets the delivery warehouse code.
        /// </summary>
        /// <value>The delivery warehouse code.</value>
        public string DeliveryWarehouseCode
        {
            get { return deliveryWarehouseCode; }
            set { deliveryWarehouseCode = value; }
        }

        #endregion

    
    }
}

