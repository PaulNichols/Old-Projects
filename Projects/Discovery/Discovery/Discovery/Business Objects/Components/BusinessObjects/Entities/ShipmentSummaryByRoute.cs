using System;
using System.Collections.Generic;
using System.Text;

namespace Discovery.BusinessObjects
{
    /*************************************************************************************************
 ** CLASS:	ShipmentTrunkedStockSummary
 **
 ** OVERVIEW:
 ** This class holds the summary details of trunked weights for shipments grouped by routes
 **
 ** MODIFICATION HISTORY:
 **
 ** Date:		Version:    Who:	Change:
 ** 7/8/06	 1.0		PJN		Initial Version
 ************************************************************************************************/
    /// <summary>
    /// A Class 'ShipmentTrunkedStockSummary' which is an entity with namespace Discovery.BusinessObjects
    /// The class holds the route code, stock warehouse code, weight and volume of the trunked shipment
    /// </summary>
    public class ShipmentTrunkedStockSummary
    {

        private string routeCode;
        private string routeDescription;
        private string stockWarehouseCode;
        private decimal weight;
        private decimal volume;
        private string deliveryWarehouseCode;
        private string stockWarehouseDescription;

        /// <summary>
        /// Gets or sets the route code.
        /// </summary>
        /// <value>The route code.</value>
        public string RouteCode
        {
            get { return routeCode; }
            set { routeCode = value; }
        }


        /// <summary>
        /// Gets or sets the net weight.
        /// </summary>
        /// <value>The weight.</value>
        public decimal Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        /// <value>The volume.</value>
        public decimal Volume
        {
            get { return volume; }
            set { volume = value; }
        }

        /// <summary>
        /// Gets or sets the stock warehouse code.
        /// </summary>
        /// <value>The stock warehouse code.</value>
        public string StockWarehouseCode
        {
            get { return stockWarehouseCode; }
            set { stockWarehouseCode = value; }
        }
 
        /// <summary>
        /// Gets or sets the stock warehouse code.
        /// </summary>
        /// <value>The stock warehouse code.</value>
        public string StockWarehouseDescription
        {
            get { return stockWarehouseDescription; }
            set { stockWarehouseDescription = value; }
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

        public string RouteDescription
        {
            get { return routeDescription; }
            set { routeDescription = value; }
        }

        public string RouteCodeDescription
        {
            get
            {
                if (!string.IsNullOrEmpty(RouteCode) && !string.IsNullOrEmpty(RouteDescription))
                {
                    return "(" + RouteCode + ") " + RouteDescription;
                }
                else
                {
                    return "";
                }
            }
            
        }
    }
    
    /*************************************************************************************************
   ** CLASS:	Contact
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
    /// A Class 'ShipmentSummaryByRoute' which is an entity with namespace Discovery.BusinessObjects
    /// The class holds the route id, route code, delivery location, required delivery date, trunked, ex-branch, weight and volume of the shipment
    /// </summary>
    public class ShipmentSummaryByRoute
    {

        private string routeCode;
        private int routeId;
        private string deliveryLocation;
        private DateTime requiredDeliveryDate;
        private decimal trunked;
        private decimal exBranch;
        private decimal weight;
        private decimal volume;
        private string deliveryWarehouseCode;

        /// <summary>
        /// Gets or sets the route code.
        /// </summary>
        /// <value>The route code.</value>
        public string RouteCode
        {
            get { return routeCode; }
            set { routeCode = value; }
        }

        /// <summary>
        /// Gets or sets the delivery location.
        /// </summary>
        /// <value>The delivery location.</value>
        public string DeliveryLocation
        {
            get { return deliveryLocation; }
            set { deliveryLocation = value; }
        }

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
        /// Gets or sets the trunked.
        /// </summary>
        /// <value>The trunked.</value>
        public decimal Trunked
        {
            get { return trunked; }
            set { trunked = value; }
        }

        /// <summary>
        /// Gets or sets the ex branch.
        /// </summary>
        /// <value>The ex branch.</value>
        public decimal ExBranch
        {
            get { return exBranch; }
            set { exBranch = value; }
        }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        /// <value>The weight.</value>
        public decimal Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        /// <value>The volume.</value>
        public decimal Volume
        {
            get { return volume; }
            set { volume = value; }
        }

        /// <summary>
        /// Gets or sets the route id.
        /// </summary>
        /// <value>The route id.</value>
        public int RouteId
        {
            get { return routeId; }
            set { routeId = value; }
        }

        public string DeliveryWarehouseCode
        {
            get { return deliveryWarehouseCode; }
            set { deliveryWarehouseCode = value; }
        }
    }
}
