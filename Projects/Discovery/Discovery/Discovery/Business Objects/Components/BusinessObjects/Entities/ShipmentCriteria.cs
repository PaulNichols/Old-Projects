using System;
using System.Text;

using Discovery.Utility;

namespace Discovery.BusinessObjects
{
    /// <summary>
    /// Summary Description for ShipmentCriteria
    /// </summary>
    /// 
    [Serializable]
    public class ShipmentCriteria
    {
        private string opCoCode;
        private string shipmentNumber;
        private string customerNumber;
        private string customerName;
        private int shipmentStatus;
        private int transportMode;
        private string salesBranchCode;
        private string routeCode;
        private string routeTrip;
        private int routeDrop;
        private string stockWarehouseCode;
        private string deliveryWarehouseCode;
        private DateTime requiredDateFrom;
        private DateTime requiredDateTo;
        private DateTime estimatedDateFrom;
        private DateTime estimatedDateTo;
        private string transactionType;
        private string transactionSubType;
        private int type;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ShipmentCriteria"/> class.
        /// </summary>
        public ShipmentCriteria()
        {
            // Seed values
            opCoCode = Null.NullString;
            shipmentNumber = Null.NullString;
            customerNumber = Null.NullString;
            customerName = Null.NullString;
            shipmentStatus = Null.NullInteger;
            transportMode = Null.NullInteger;
            salesBranchCode = Null.NullString;
            routeCode = Null.NullString;
            routeTrip = Null.NullString;
            routeDrop = Null.NullInteger;
            stockWarehouseCode = Null.NullString;
            deliveryWarehouseCode = Null.NullString;
            requiredDateFrom = Null.NullDate;
            requiredDateTo = Null.NullDate;
            estimatedDateTo= Null.NullDate;
            estimatedDateFrom = Null.NullDate;
            transactionType = Null.NullString;
            transactionSubType = Null.NullString;
            type = Null.NullInteger;
        }

        /// <summary>
        /// Parses the query string.
        /// </summary>
        /// <param name="queryString">The query string.</param>
        public void ParseQueryString(System.Collections.Specialized.NameValueCollection queryString)
        {
            // Seed values
            opCoCode = Convert.ToString(Null.GetNull(queryString["c_OpCoCode"], ""));
            shipmentNumber = Convert.ToString(Null.GetNull(queryString["c_ShipmentNumber"], ""));
            customerNumber = Convert.ToString(Null.GetNull(queryString["c_CustomerNumber"], ""));
            customerName = Convert.ToString(Null.GetNull(queryString["c_CustomerName"], ""));
            shipmentStatus = Convert.ToInt32(Null.GetNull(queryString["c_ShipmentStatus"], Null.NullInteger));
            transportMode = Convert.ToInt32(Null.GetNull(queryString["c_TransportMode"], Null.NullInteger));
            salesBranchCode = Convert.ToString(Null.GetNull(queryString["c_SalesBranchCode"], ""));
            routeCode = Convert.ToString(Null.GetNull(queryString["c_RouteCode"], ""));
            routeTrip = Convert.ToString(Null.GetNull(queryString["c_RouteTrip"], ""));
            routeDrop = Convert.ToInt32(Null.GetNull(queryString["c_RouteDrop"], Null.NullInteger));
            stockWarehouseCode = Convert.ToString(Null.GetNull(queryString["c_StockWarehouseCode"], ""));
            deliveryWarehouseCode = Convert.ToString(Null.GetNull(queryString["c_DeliveryWarehouseCode"], ""));
            requiredDateFrom = Convert.ToDateTime(Null.GetNull(queryString["c_RequiredDateFrom"], Null.NullDate));
            requiredDateTo = Convert.ToDateTime(Null.GetNull(queryString["c_RequiredDateTo"], Null.NullDate));
            estimatedDateFrom = Convert.ToDateTime(Null.GetNull(queryString["c_EstimatedDateFrom"], Null.NullDate));
            estimatedDateTo = Convert.ToDateTime(Null.GetNull(queryString["c_EstimatedDateTo"], Null.NullDate));
            transactionType = Convert.ToString(Null.GetNull(queryString["c_TransactionType"], Null.NullInteger));
            transactionSubType = Convert.ToString(Null.GetNull(queryString["c_TransactionSubType"], ""));
            type = Convert.ToInt32(Null.GetNull(queryString["c_Type"], Null.NullInteger));
        }

        /// <summary>
        /// Generates the query string.
        /// </summary>
        /// <returns></returns>
        public string GenerateQueryString()
        {
            StringBuilder sbQueryString = new StringBuilder();
            sbQueryString.AppendFormat("c_OpCoCode={0}", opCoCode);
            sbQueryString.AppendFormat("&c_ShipmentNumber={0}", shipmentNumber);
            sbQueryString.AppendFormat("&c_CustomerNumber={0}", customerNumber);
            sbQueryString.AppendFormat("&c_CustomerName={0}", customerName);
            sbQueryString.AppendFormat("&c_ShipmentStatus={0}", shipmentStatus);
            sbQueryString.AppendFormat("&c_TransportMode={0}", transportMode);
            sbQueryString.AppendFormat("&c_SalesBranchCode={0}", salesBranchCode);
            sbQueryString.AppendFormat("&c_RouteCode={0}", routeCode);
            sbQueryString.AppendFormat("&c_RouteTrip={0}", routeTrip);
            sbQueryString.AppendFormat("&c_RouteDrop={0}", routeDrop);
            sbQueryString.AppendFormat("&c_StockWarehouseCode={0}", stockWarehouseCode);
            sbQueryString.AppendFormat("&c_DeliveryWarehouseCode={0}", deliveryWarehouseCode);
            sbQueryString.AppendFormat("&c_RequiredDateFrom={0:d}", requiredDateFrom);
            sbQueryString.AppendFormat("&c_RequiredDateTo={0:d}", requiredDateTo);
            sbQueryString.AppendFormat("&c_EstimatedDateFrom={0:d}", estimatedDateFrom);
            sbQueryString.AppendFormat("&c_EstimatedDateTo={0:d}", estimatedDateTo);
            sbQueryString.AppendFormat("&c_TransactionType={0}", transactionType);
            sbQueryString.AppendFormat("&c_TransactionSubType={0}", transactionSubType);
            sbQueryString.AppendFormat("&c_TType={0}", type);

            // Return the generated query string
            return sbQueryString.ToString();
        }

        /// <summary>
        /// Gets or sets the route drop.
        /// </summary>
        /// <value>The route drop.</value>
        public int RouteDrop
        {
            get { return routeDrop; }
            set { routeDrop = value; }
        }

        /// <summary>
        /// Gets or sets the required date from.
        /// </summary>
        /// <value>The required date from.</value>
        public DateTime RequiredDateFrom
        {
            get { return requiredDateFrom; }
            set { requiredDateFrom = value; }

        }
        /// <summary>
        /// Gets or sets the required date to.
        /// </summary>
        /// <value>The required date to.</value>
        public DateTime RequiredDateTo
        {
            get { return requiredDateTo; }
            set { requiredDateTo = value; }
        }

        /// <summary>
        /// Gets or sets the route trip.
        /// </summary>
        /// <value>The route trip.</value>
        public string RouteTrip
        {
            get { return routeTrip; }
            set { routeTrip = value; }
        }

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
        /// Gets or sets the type of the transaction.
        /// </summary>
        /// <value>The type of the transaction.</value>
        public string TransactionType
        {
            get { return transactionType; }
            set { transactionType = value; }
        }

        /// <summary>
        /// Gets or sets the sub type of the transaction.
        /// </summary>
        /// <value>The type of the transaction.</value>
        public string TransactionSubType
        {
            get { return transactionSubType; }
            set { transactionSubType = value; }
        }

        /// <summary>
        /// Gets or sets the shipment type.
        /// </summary>
        /// <value>The type of the transaction.</value>
        public int Type
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// Gets or sets the sales branch code.
        /// </summary>
        /// <value>The sales branch code.</value>
        public string SalesBranchCode
        {
            get { return salesBranchCode; }
            set { salesBranchCode = value; }
        }

        /// <summary>
        /// Gets or sets the transport mode.
        /// </summary>
        /// <value>The transport mode.</value>
        public int TransportMode
        {
            get { return transportMode; }
            set { transportMode = value; }
        }

        /// <summary>
        /// Gets or sets the shipment status.
        /// </summary>
        /// <value>The shipment status.</value>
        public int ShipmentStatus
        {
            get { return shipmentStatus; }
            set { shipmentStatus = value; }
        }

        /// <summary>
        /// Gets or sets the name of the customer.
        /// </summary>
        /// <value>The name of the customer.</value>
        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }

        /// <summary>
        /// Gets or sets the customer account number.
        /// </summary>
        /// <value>The customer account number.</value>
        public string CustomerAccountNumber
        {
            get { return customerNumber; }
            set { customerNumber = value; }
        }

        /// <summary>
        /// Gets or sets the shipment number.
        /// </summary>
        /// <value>The shipment number.</value>
        public string ShipmentNumber
        {
            get { return shipmentNumber; }
            set { shipmentNumber = value; }
        }

        /// <summary>
        /// Gets or sets the opco code.
        /// </summary>
        /// <value>The op co code.</value>
        public string OpCoCode
        {
            get { return opCoCode; }
            set { opCoCode = value; }
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
        /// Gets or sets the delivery warehouse code.
        /// </summary>
        /// <value>The delivery warehouse code.</value>
        public string DeliveryWarehouseCode
        {
            get { return deliveryWarehouseCode; }
            set { deliveryWarehouseCode = value; }
        }

        /// <summary>
        /// Gets or sets the estimated date from.
        /// </summary>
        /// <value>The estimated date from.</value>
        public DateTime EstimatedDateFrom
        {
            get { return estimatedDateFrom; }
            set { estimatedDateFrom = value; }
        }

        /// <summary>
        /// Gets or sets the estimated date to.
        /// </summary>
        /// <value>The estimated date to.</value>
        public DateTime EstimatedDateTo
        {
            get { return estimatedDateTo; }
            set { estimatedDateTo = value; }
        }
    }
}
