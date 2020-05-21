using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Discovery.BusinessObjects;
using Discovery.BusinessObjects.Controllers;
using Discovery.Utility.Configuration;
using Discovery.Utility.DataAccess;

namespace Discovery.ComponentServices.DataAccess
{
    /// <summary>
    /// An abstract class DataAccessProvider consists of all others' abstract methods
    /// </summary>
    public abstract class DataAccessProvider
    {
        #region Factory Support

        // Provider constants - eliminates need for Reflection later
        private static string providerType = "data";

        /// <summary>
        /// Gets the type of the provider.
        /// </summary>
        /// <value>The type of the provider.</value>
        public static string ProviderType
        {
            get { return providerType; }
        }

        // The provider instance name 
        private string provider = "";

        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        /// <value>The provider.</value>
        public string Provider
        {
            get { return provider; }
            set { provider = value; }
        }

        /// <summary>
        /// Instances this instance.
        /// </summary>
        /// <returns></returns>
        public static DataAccessProvider Instance()
        {
            // Get the name of the provider
            ProviderConfiguration objProviderConfiguration =
                ProviderConfiguration.GetProviderConfiguration(ProviderType);

            // Get the name of the provider
            return Instance(objProviderConfiguration.DefaultProvider, objProviderConfiguration);
        }

        /// <summary>
        /// Instances the specified provider name.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        /// <returns></returns>
        public static DataAccessProvider Instance(string providerName)
        {
            // Get the name of the provider
            ProviderConfiguration objProviderConfiguration =
                ProviderConfiguration.GetProviderConfiguration(ProviderType);

            return Instance(providerName, objProviderConfiguration);
        }

        /// <summary>
        /// Instances the specified provider name.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        /// <param name="providerConfiguration">The provider configuration.</param>
        /// <returns></returns>
        private static DataAccessProvider Instance(string providerName, ProviderConfiguration providerConfiguration)
        {
            // Cache key name
            string strCacheKey = ProviderType + "_provider_" + providerName;

            // Use the cache because the reflection used later is expensive
            ConstructorInfo objConstructor = (ConstructorInfo)DataCache.GetCache(strCacheKey);

            // Check if constructor was in cache
            if (null == objConstructor)
            {
                // The assembly should be in bin or GAC, so we simply need to get an instance of the type
                try
                {
                    // Get the typename of the default DataProvider from web.config
                    string strTypeName = ((Provider)providerConfiguration.Providers[providerName]).Type;
                    // Use reflection to store the constructor of the class that implements DataProvider
                    Type t = Type.GetType(strTypeName, true);
                    // Get public instance constructor that takes no parameters
                    objConstructor = t.GetConstructor(Type.EmptyTypes);
                    // Insert the type into the cache
                    DataCache.SetCache(strCacheKey, objConstructor);
                }
                catch (Exception)
                {
                    // Could not load the provider, this is likely due to binary compatibility issues or the assembly cannot be found 
                    throw;
                }
            }
            // Create the data provider instance
            DataAccessProvider objProvider = (DataAccessProvider)objConstructor.Invoke(null);
            // Read the configuration specific information for this provider
            objProvider.Initialise((Provider)providerConfiguration.Providers[providerName]);

            // Return the DataProvider instance
            return objProvider;
        }

        /// <summary>
        /// Initialises the specified provider name.
        /// </summary>
        /// <param name="provider">The provider.</param>
        protected abstract void Initialise(Provider provider);

        #endregion

        #region Audit Enrty Methods

        /// <summary>
        /// Saves the audit entry.
        /// </summary>
        /// <param name="auditEntry">The audit entry.</param>
        /// <returns></returns>
        public abstract int SaveAuditEntry(MessageAuditEntry auditEntry);

        /// <summary>
        /// Deletes the audit entry.
        /// </summary>
        /// <param name="auditEntryId">The audit entry id.</param>
        /// <returns></returns>
        public abstract bool DeleteAuditEntry(int auditEntryId);

        /// <summary>
        /// Gets the audit entry.
        /// </summary>
        /// <param name="auditEntryId">The audit entry id.</param>
        /// <returns></returns>
        public abstract IDataReader GetAuditEntry(int auditEntryId);

        /// <summary>
        /// Gets the audit entries.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetAuditEntries();

        /// <summary>
        /// Gets the audit entries.
        /// </summary>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date to.</param>
        /// <param name="sourceSystem">The source system.</param>
        /// <param name="destinationSystem">The destination system.</param>
        /// <param name="type">The type.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <param name="totalRows">The total rows.</param>
        /// <returns></returns>
        public abstract IDataReader GetAuditEntries(DateTime dateFrom,
                                                    DateTime dateTo,
                                                    string sourceSystem,
                                                    string destinationSystem,
                                                    string type,
                                                    string message,
                                                    string sortExpression,
                                                    int startRowIndex,
                                                    int maximumRows, out int totalRows);

        /// <summary>
        /// Gets the source system list.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetSourceSystemList();

        /// <summary>
        /// Gets the destination system list.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetDestinationSystemList();

        /// <summary>
        /// Gets the type list.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetTypeList();

        #endregion

        #region Sequencing Methods

        /// <summary>
        /// Saves the sequence.
        /// </summary>
        /// <param name="sequence">The sequence to save to the db.</param>
        /// <returns></returns>
        public abstract int SaveSequence(Sequence sequence);

        /// <summary>
        /// Deletes the sequence.
        /// </summary>
        /// <param name="sequence">The sequence object to delete.</param>
        /// <returns></returns>
        public abstract bool DeleteSequence(Sequence sequence);

        /// <summary>
        /// Gets the sequence.
        /// </summary>
        /// <param name="sequenceId">The sequence id to retrieve.</param>
        /// <returns></returns>
        public abstract IDataReader GetSequence(int sequenceId);

        /// <summary>
        /// Gets the next named sequence value.
        /// </summary>
        /// <param name="name">The name of the sequence to retrieve the next available value for.</param>
        /// <returns></returns>
        public abstract int GetNextSequence(string name);

        /// <summary>
        /// Gets the sequences.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetSequences();


        #endregion

        #region Mapping Methods

        /// <summary>
        /// Gets the mapping property association by source property.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="classAssociationId">The class association id.</param>
        /// <returns></returns>
        public abstract IDataReader GetMappingPropertyAssociations(string property, int classAssociationId);

        /// <summary>
        /// Saves the mapping property association.
        /// </summary>
        /// <param name="association">The association.</param>
        /// <returns></returns>
        public abstract int SaveMappingPropertyAssociation(MappingPropertyAssociation association);

        /// <summary>
        /// Saves the mapping class association.
        /// </summary>
        /// <param name="association">The association.</param>
        /// <returns></returns>
        public abstract int SaveMappingClassAssociation(MappingClassAssociation association);

        /// <summary>
        /// Saves the mapping system.
        /// </summary>
        /// <param name="system">The system.</param>
        /// <returns></returns>
        public abstract int SaveMappingSystem(MappingSystem system);

        /// <summary>
        /// Deletes all mapping property associations.
        /// </summary>
        /// <returns></returns>
        public abstract bool DeleteAllMappingPropertyAssociations();

        /// <summary>
        /// Deletes all mapping class associations.
        /// </summary>
        /// <returns></returns>
        public abstract bool DeleteAllMappingClassAssociations();

        /// <summary>
        /// Deletes all mapping systems.
        /// </summary>
        public abstract bool DeleteAllMappingSystems();

        /// <summary>
        /// Deletes all mappings.
        /// </summary>
        /// <returns></returns>
        public abstract bool DeleteAllMappings();

        /// <summary>
        /// Gets the mapping lookup.
        /// </summary>
        /// <param name="mappingPropertyAssociationId">The mapping property association id.</param>
        /// <returns></returns>
        public abstract IDataReader GetMappingLookup(int mappingPropertyAssociationId);

        /// <summary>
        /// Gets the mapping property association.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public abstract IDataReader GetMappingPropertyAssociation(int id);

        /// <summary>
        /// Gets the mapping property association by class association id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public abstract IDataReader GetMappingPropertyAssociationByClassAssociationId(int id);

        /// <summary>
        /// Gets the mapping class associations.
        /// </summary>
        /// <param name="sourceType">Type of the source.</param>
        /// <returns></returns>
        public abstract IDataReader GetMappingClassAssociations(string sourceType);

        /// <summary>
        /// Gets the mapping systems.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetMappingSystems();

        /// <summary>
        /// Retrieves a list of all Mapping from the underlying data store via the configured DataProvider.
        ///A strongly typed list of Mapping entities is returned to the caller or an empty list if no records were found.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetMappings(MappingSearchParams mappingSearchParams, int startRowIndex, int maximumRows, out int rows);

        /// <summary>
        /// Retrieves a list of all Mapping from the underlying data store via the configured DataProvider.
        ///A strongly typed list of Mapping entities is returned to the caller or an empty list if no records were found.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetMappings();

        /// <summary>
        /// Retrieves a list of Mapping items from the underlying data store via the configured DataProvider that match the specified arguments.
        ///A strongly typed list of Mapping items is returned to the caller or an empty list if no records were found.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <param name="sourceMappingType">Type of the source mapping.</param>
        /// <param name="destinationMappingType">Type of the destination mapping.</param>
        /// <param name="sourceSystem">The source system.</param>
        /// <param name="destinationSystem">The destination system.</param>
        /// <returns></returns>
        public abstract IDataReader GetMappingByTypeAndSystem(string sourceMappingType, string destinationMappingType,
                                                              string sourceSystem, string destinationSystem);

        /// <summary>
        /// Saves the specified Mapping to the underlying data store via the configured DataProvider. 
        ///If the primary key (ID) of the supplied Mapping is Null, a new row is added to the underlying data store and a new primary key (ID) automatically generated.  If the primary key (ID) of the supplied Mapping has been specified, the existing row in the data store is updated if it has not already been altered. 
        ///The primary key (ID) of the created or altered Mapping is returned to the caller.  If an error occurs, an exception is thrown and no updates made to the data store. 
        ///For concurrency errors (the data has changed in-between load and save by an external system or user), a concurrency exception is thrown.
        /// </summary>
        /// <param name="mapping">The mapping.</param>
        /// <returns></returns>
        public abstract int SaveMapping(BusinessObjects.Mapping mapping);

        /// <summary>
        ///Deletes the specified Mapping record from the underlying data store via the configured DataProvider. 
        ///If the specified Mapping is deleted true is returned otherwise false.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <param name="mappingId">The mapping id.</param>
        /// <returns></returns>
        public abstract bool DeleteMapping(int mappingId);

        /// <summary>
        /// Retrieves a single Mapping entry from the underlying data store via the configured DataProvider for the supplied Mapping ID.
        /// An instance of a Mapping is returned to the caller or Null if no Mapping record is found.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <param name="mappingId">The mapping id.</param>
        /// <returns></returns>
        public abstract IDataReader GetMapping(int mappingId);

        /// <summary>
        /// Retrieves a single Mapping entry from the underlying data store via the configured DataProvider for the supplied 
        /// source type, destination type, source system, destination system, source property and source property value.
        /// An instance of a Mapping is returned to the caller or Null if no Mapping record is found.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <param name="mappingId">The mapping id.</param>
        /// <returns></returns>
        public abstract IDataReader GetMapping(
                    string sourceMappingType,
                    string destinationMappingType,
                    string sourceSystem,
                    string destinationSystem,
                    string sourcePropertyName,
                    string destinationPropertyName,
                    string sourcePropertyValue);

        /// <summary>
        /// Retrieves Mapping entries from the underlying data store via the configured DataProvider for the supplied 
        /// source type and destination type.
        /// A List of  Mappings is returned to the caller or an empty list if no Mapping records are found.  
        /// If an error occurs, an exception is thrown.
        /// </summary>
        /// <param name="mappingId">The mapping id.</param>
        /// <returns></returns>
        public abstract IDataReader GetMappingClassAssociationByTypes(
                    string sourceMappingType,
                    string destinationMappingType);

        /// <summary>
        /// Gets the classes that have been associationed for mapping.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetMappingClassAssociations();

        /// <summary>
        /// Get a mapping system for the primary key.
        /// </summary>
        /// <param name="mappingSystemId">The mappingSystemId.</param>
        /// <returns></returns>
        public abstract IDataReader GetMappingSystem(int mappingSystemId);

        /// <summary>
        /// Gets the mapping class association.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public abstract IDataReader GetMappingClassAssociation(int id);

        public abstract IDataReader GetMappingPropertyAssociations();

        #endregion

        #region Non-Working Day Methods

        /// <summary>
        /// Deletes the non working day.
        /// </summary>
        /// <param name="nonWorkingDayId">The non working day id.</param>
        /// <returns></returns>
        public abstract bool DeleteNonWorkingDay(int nonWorkingDayId);

        /// <summary>
        /// Gets the non working day.
        /// </summary>
        /// <param name="nonWorkingDayId">The non working day id.</param>
        /// <returns></returns>
        public abstract IDataReader GetNonWorkingDay(int nonWorkingDayId);

        /// <summary>
        /// Gets the non working day.
        /// </summary>
        /// <param name="warehouseCode">The warehouse code.</param>
        /// <param name="nonWorkingDate">The non working date.</param>
        /// <returns></returns>
        public abstract IDataReader GetNonWorkingDay(String warehouseCode, DateTime nonWorkingDate);

        //public abstract IDataReader GetNonWorkingDays(DateTime dateFrom);

        /// <summary>
        /// Gets the non working days.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetNonWorkingDays();

        /// <summary>
        /// Gets the non working days.
        /// </summary>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date to.</param>
        /// <param name="warehouseId">The warehouse id.</param>
        /// <param name="regionId">The region id.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <param name="totalRows">The total rows.</param>
        /// <returns></returns>
        public abstract IDataReader GetNonWorkingDays(DateTime dateFrom, DateTime dateTo,
            int warehouseId,
            int regionId, string sortExpression, int startRowIndex, int maximumRows, out int totalRows);

        //public abstract IDataReader GetNonWorkingDaysByRegion(DateTime dateFrom, string region);

        /// <summary>
        /// Gets the non working days by region.
        /// </summary>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date to.</param>
        /// <param name="regionId">The region id.</param>
        /// <returns></returns>
        public abstract IDataReader GetNonWorkingDaysByRegion(DateTime dateFrom, DateTime dateTo, int regionId);

        //public abstract IDataReader GetNonWorkingDaysByWarehouse(DateTime dateFrom, string warehouse);

        /// <summary>
        /// Gets the non working days by warehouse.
        /// </summary>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date to.</param>
        /// <param name="warehouseId">The warehouse id.</param>
        /// <returns></returns>
        public abstract IDataReader GetNonWorkingDaysByWarehouse(DateTime dateFrom, DateTime dateTo, int warehouseId);

        /// <summary>
        /// Saves the non working day.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="nonWorkingDate">The non working date.</param>
        /// <param name="description">The description.</param>
        /// <param name="warehouseId">The warehouse id.</param>
        /// <param name="updatedBy">The updated by.</param>
        /// <param name="checkSum">The check sum.</param>
        /// <returns></returns>
        public abstract int SaveNonWorkingDay(
             int id,
             DateTime nonWorkingDate,
             string description,
             int warehouseId,
             string updatedBy,
             int checkSum);

        #endregion

        #region OpCo Shipment Methods

        /// <summary>
        /// Saves the op co shipments.
        /// </summary>
        /// <param name="shipment">The shipment.</param>
        /// <returns></returns>
        public abstract List<int> SaveOpCoShipments(List<OpCoShipment> shipment);

        /// <summary>
        /// Gets the op co shipment.
        /// </summary>
        /// <param name="opCo">The op co.</param>
        /// <param name="shipmentNumber">The shipment number.</param>
        /// <param name="despatchNumber">The despatch number.</param>
        /// <returns></returns>
        public abstract IDataReader GetOpCoShipment(string opCo, string shipmentNumber, string despatchNumber);

        /// <summary>
        /// Gets the op co shipment.
        /// </summary>
        /// <param name="ShipmentId">The shipment id.</param>
        /// <returns></returns>
        public abstract IDataReader GetOpCoShipment(int ShipmentId);

        /// <summary>
        /// Gets the op co shipments.
        /// </summary>
        /// <param name="deliveryLocation">The delivery location.</param>
        /// <returns></returns>
        public abstract IDataReader GetOpCoShipments(string deliveryLocation);

        /// <summary>
        /// Gets the op co shipments.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="numRows">The num rows.</param>
        /// <returns></returns>
        public abstract IDataReader GetOpCoShipments(ShipmentCriteria criteria, string sortExpression, int pageIndex, int numRows);

        /// <summary>
        /// Gets the op co shipments count.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        public abstract int GetOpCoShipmentsCount(ShipmentCriteria criteria);

        /// <summary>
        /// Saves the op co shipment.
        /// </summary>
        /// <param name="shipment">The shipment.</param>
        /// <returns></returns>
        public abstract int SaveOpCoShipment(OpCoShipment shipment);

        /// <summary>
        /// Deletes the op co shipment.
        /// </summary>
        /// <param name="shipmentId">The shipment id.</param>
        /// <returns></returns>
        public abstract bool DeleteOpCoShipment(int shipmentId);

        /// <summary>
        /// Updates the op co shipment status.
        /// </summary>
        /// <param name="shipment">The shipment.</param>
        /// <returns></returns>
        public abstract bool UpdateOpCoShipmentStatus(OpCoShipment shipment);

        #endregion

        #region Commander Methods

        /// <summary>
        /// Saves a commander product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        public abstract int SaveCommanderProduct(CommanderProduct product);

        /// <summary>
        /// Gets a commander product.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public abstract IDataReader GetCommanderProduct(string code);

        /// <summary>
        /// Saves the specified CommanderSalesOrder to the underlying data store via the configured DataProvider.
        /// If a duplicate Commander Sales Order occurs then an exception will be raised.
        /// </summary>
        /// <param name="commanderOrder">The commander order.</param>
        /// <returns></returns>
        public abstract int SaveCommanderSalesOrder(CommanderSalesOrder commanderOrder);

        /// <summary>
        /// Saves the specified CommanderSalesOrder to the underlying data store via the configured DataProvider.
        ///If a corresponding Commander order does not exist then an error will occur.
        /// </summary>
        /// <param name="commanderOrderLines">The commander order lines.</param>
        /// <returns></returns>
        public abstract List<int> SaveCommanderSalesOrderLines(List<CommanderSalesOrderLine> commanderOrderLines);

        /// <summary>
        /// Retrieves a single CommanderSalesOrder from the underlying data store via the configured DataProvider for the supplied CommanderSalesOrder ID.
        /// An instance of an CommanderSalesOrder is returned to the caller or Null if no CommanderSalesOrder record is found.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <param name="salesOrderId">The sales order id.</param>
        /// <returns></returns>
        public abstract IDataReader GetCommanderSalesOrder(int salesOrderId);

        /// <summary>
        /// Retrieves a list of all CommanderSalesOrderLines from the underlying data store via the configured DataProvider relating to the CommanderSalesOrder with the specified ID.
        /// A strongly typed list of CommanderSalesOrderLines is returned to the caller or an empty list if no records were found.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <param name="salesOrderId">The sales order id.</param>
        /// <returns></returns>
        public abstract IDataReader GetCommanderSalesOrderLines(int salesOrderId);

        /// <summary>
        /// Retrieves a single CommanderSalesOrder from the underlying data store via the configured DataProvider for the supplied order number.
        ///An instance of an CommanderSalesOrder is returned to the caller or Null if no CommanderSalesOrder record is found.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <param name="orderReference">The order number.</param>
        /// <returns></returns>
        public abstract IDataReader GetCommanderSalesOrderByOrderNumber(string orderReference);

        /// <summary>
        /// Retrieves a list of all CommanderSalesOrder from the underlying data store via the configured DataProvider.
        /// A strongly typed list of CommanderSalesOrder is returned to the caller or an empty list if no records were found.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetCommanderSalesOrders();

        /// <summary>
        /// Deletes the commander sales order.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public abstract bool DeleteCommanderSalesOrder(int id);

        /// <summary>
        /// Deletes the commander sales order line.
        /// </summary>
        /// <param name="Id">The id.</param>
        /// <returns></returns>
        public abstract bool DeleteCommanderSalesOrderLine(int Id);

        /// <summary>
        /// Saves the commander sales order line.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <returns></returns>
        public abstract int SaveCommanderSalesOrderLine(CommanderSalesOrderLine line);

        /// <summary>
        /// Gets the commander sales order line.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public abstract IDataReader GetCommanderSalesOrderLine(int id);

        #endregion

        #region TDC Shipment Methods

        /// <summary>
        /// Gets the trunked stock summary.
        /// </summary>
        /// <param name="deliveryWarehouse">The delivery warehouse.</param>
        /// <param name="requiredDate">The required date.</param>
        /// <param name="routeCode">The route code.</param>
        /// <returns></returns>
        public abstract IDataReader GetTrunkedStockSummary(int deliveryWarehouse, DateTime requiredDate, int routeCode);

        /// <summary>
        /// Saves the TDC shipments.
        /// </summary>
        /// <param name="shipment">The shipment.</param>
        /// <returns></returns>
        public abstract List<int> SaveTDCShipments(List<TDCShipment> shipment);

        /// <summary>
        /// Gets the TDC shipment.
        /// </summary>
        /// <param name="opCo">The op co.</param>
        /// <param name="shipmentNumber">The shipment number.</param>
        /// <param name="despatchNumber">The despatch number.</param>
        /// <returns></returns>
        public abstract IDataReader GetTDCShipment(string opCo, string shipmentNumber, string despatchNumber);

        /// <summary>
        /// Gets the TDC shipment.
        /// </summary>
        /// <param name="ShipmentId">The shipment id.</param>
        /// <returns></returns>
        public abstract IDataReader GetTDCShipment(int ShipmentId);

        /// <summary>
        /// Gets the TDC shipments.
        /// </summary>
        /// <param name="deliveryLocation">The delivery location.</param>
        /// <returns></returns>
        public abstract IDataReader GetTDCShipments(string deliveryLocation);

        /// <summary>
        /// Gets the TDC shipments.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="numRows">The num rows.</param>
        /// <returns></returns>
        public abstract IDataReader GetTDCShipments(ShipmentCriteria criteria, string sortExpression, int pageIndex, int numRows);

        /// <summary>
        /// Gets the TDC shipments count.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        public abstract int GetTDCShipmentsCount(ShipmentCriteria criteria);

        /// <summary>
        /// Gets the TDC shipments by route.
        /// </summary>
        /// <param name="deliveryWarehouseId">The delivery warehouse id.</param>
        /// <param name="requireDeliveryDate">The require delivery date.</param>
        /// <param name="includSpecials">if set to <c>true</c> [includ specials].</param>
        /// <returns></returns>
        public abstract IDataReader GetTDCShipmentsByRoute(int deliveryWarehouseId, DateTime requireDeliveryDate, bool includSpecials);

        /// <summary>
        /// Gets the trips.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        public abstract IDataReader GetTrips(ShipmentCriteria criteria);

        /// <summary>
        /// Saves the trip.
        /// </summary>
        /// <param name="trip">The trip.</param>
        /// <returns></returns>
        public abstract int SaveTrip(Trip trip);

        /// <summary>
        /// Gets the drops.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        public abstract IDataReader GetDrops(ShipmentCriteria criteria);

        /// <summary>
        /// Saves the TDC shipment.
        /// </summary>
        /// <param name="shipment">The shipment.</param>
        /// <returns></returns>
        public abstract int SaveTDCShipment(TDCShipment shipment);

        /// <summary>
        /// Deletes the TDC shipment.
        /// </summary>
        /// <param name="shipmentId">The shipment id.</param>
        /// <returns></returns>
        public abstract bool DeleteTDCShipment(int shipmentId);

        /// <summary>
        /// Gets the delivery locations.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetDeliveryLocations();

        /// <summary>
        /// Gets the stock locations.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetStockLocations();

        /// <summary>
        /// Gets the route codes.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetRouteCodes();

        /// <summary>
        /// Gets the deliveries.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetDeliveries();

        /// <summary>
        /// Gets the collections.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetCollections();

        /// <summary>
        /// Gets the transfers.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetTransfers();

        /// <summary>
        /// Updates the TDC shipment status.
        /// </summary>
        /// <param name="shipment">The shipment.</param>
        /// <param name="newStatus">The new status.</param>
        /// <param name="updatedBy">The updated by.</param>
        /// <returns></returns>
        public abstract bool UpdateTDCShipmentStatus(TDCShipment shipment, Shipment.StatusEnum newStatus, string updatedBy);

        /// <summary>
        /// Gets the status of the specified TDC shipment.
        /// </summary>
        /// <returns></returns>
        public abstract int GetTDCShipmentStatus(int Id);

        /// <summary>
        /// Gets the shipment type data from the db.  This includes 
        /// Route Code, Transaction Type, Transaction Sub Type, 
        /// Stock Warehouse and Delivery Warehouse.
        public abstract IDataReader GetTDCShipmentTypeData(int Id);

        #endregion

        #region TDC Shipment Line Methods

        /// <summary>
        /// Deletes the TDC shipment line.
        /// </summary>
        /// <param name="shipmentLineId">The shipment line id.</param>
        /// <returns></returns>
        public abstract bool DeleteTDCShipmentLine(int shipmentLineId);

        /// <summary>
        /// Saves the TDC shipment line.
        /// </summary>
        /// <param name="shipmentLine">The shipment line.</param>
        /// <returns></returns>
        public abstract int SaveTDCShipmentLine(TDCShipmentLine shipmentLine);

        /// <summary>
        /// Gets the TDC shipment line.
        /// </summary>
        /// <param name="ShipmentLineId">The shipment line id.</param>
        /// <returns></returns>
        public abstract IDataReader GetTDCShipmentLine(int ShipmentLineId);

        /// <summary>
        /// Gets the TDC shipment lines.
        /// </summary>
        /// <param name="shipmentId">The shipment id.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        public abstract IDataReader GetTDCShipmentLines(int shipmentId, string sortExpression);

        #endregion

        #region OpCo Shipment Line Methods

        /// <summary>
        /// Deletes the opco shipment line.
        /// </summary>
        /// <param name="shipmentLineId">The shipment line id.</param>
        /// <returns></returns>
        public abstract bool DeleteOpCoShipmentLine(int shipmentLineId);

        /// <summary>
        /// Saves the opco shipment line.
        /// </summary>
        /// <param name="shipmentLine">The shipment line.</param>
        /// <returns></returns>
        public abstract int SaveOpCoShipmentLine(ShipmentLine shipmentLine);

        /// <summary>
        /// Gets the opco shipment lines.
        /// </summary>
        /// <param name="shipmentId">The shipment id.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        public abstract IDataReader GetOpCoShipmentLines(int shipmentId, string sortExpression);

        #endregion

        #region Trunker Day Methods

        /// <summary>
        /// Deletes the trunker day.
        /// </summary>
        /// <param name="trunkerDayId">The trunker day id.</param>
        /// <returns></returns>
        public abstract bool DeleteTrunkerDay(int trunkerDayId);

        /// <summary>
        /// Gets the trunker day.
        /// </summary>
        /// <param name="trunkerId">The trunker id.</param>
        /// <returns></returns>
        public abstract IDataReader GetTrunkerDay(int trunkerId);

        /// <summary>
        /// Gets the trunker days.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetTrunkerDays();

        /// <summary>
        /// Saves the trunker day.
        /// </summary>
        /// <param name="trunkerDay">The trunker day.</param>
        /// <returns></returns>
        public abstract int SaveTrunkerDay(TrunkerDay trunkerDay);

        /// <summary>
        /// Gets the number of trunker days.
        /// </summary>
        /// <param name="sourceWarehouse">The source warehouse.</param>
        /// <param name="destinationWarehouse">The destination warehouse.</param>
        /// <returns></returns>
        public abstract Int32 GetNumberOfTrunkerDays(Warehouse sourceWarehouse, Warehouse destinationWarehouse);

        #endregion

        #region OpCo

        /// <summary>
        /// Gets the all OpCos.
        /// </summary>
        /// <returns>Collection of OpCo Objects</returns>
        public abstract IDataReader GetOpCos();

        /// <summary>
        /// Gets a single OpCo for the specified Id.
        /// </summary>
        /// <param name="opcoId">The opco id.</param>
        /// <returns>An OpCo object</returns>
        public abstract IDataReader GetOpCo(int opcoId);

        /// <summary>
        /// Gets a single OpCo for the specified Id.
        /// </summary>
        /// <param name="opcoCode">The opco code.</param>
        /// <returns>An OpCo object</returns>
        public abstract IDataReader GetOpCo(string opcoCode);

        /// <summary>
        /// Deletes a OpCo.
        /// </summary>
        /// <param name="opcoId">The opco id.</param>
        /// <returns>Success state</returns>
        public abstract bool DeleteOpCo(int opcoId);

        /// <summary>
        /// Saves an OpCo object.
        /// </summary>
        /// <remarks>If the Id property =0 then an INSERT will Occur, otherwise an UPDATE will occur</remarks>
        /// <param name="opco">The opco.</param>
        /// <returns></returns>
        public abstract int SaveOpCo(OpCo opco);

        #endregion

        #region SalesLocation

        /// <summary>
        /// Retrieves a list of all Locations from the underlying data store via the configured DataProvider.
        /// A strongly typed list of SalesLocation entities is returned to the caller or an empty list if no records were found.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetLocations();

        /// <summary>
        /// Retrieves a single SalesLocation entry from the underlying data store via the configured DataProvider for the supplied SalesLocation ID.
        ///An instance of a SalesLocation is returned to the caller or Null if no SalesLocation record is found.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <param name="locationId">The location id.</param>
        /// <returns></returns>
        public abstract IDataReader GetLocation(int locationId);

        /// <summary>
        /// Retrieves a single SalesLocation entry from the underlying data store via the configured DataProvider for the supplied SalesLocation Code.
        ///An instance of a SalesLocation is returned to the caller or Null if no SalesLocation record is found.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <param name="locationCode">The sales location code.</param>
        /// <returns></returns>
        public abstract IDataReader GetLocation(string locationCode);

        /// <summary>
        /// Deletes the specified SalesLocation record from the underlying data store via the configured DataProvider.
        /// If the specified SalesLocation is deleted true is returned otherwise false.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <param name="locationId">The location id.</param>
        /// <returns></returns>
        public abstract bool DeleteLocation(int locationId);

        /// <summary>
        /// Saves the specified SalesLocation to the underlying data store via the configured DataProvider.
        /// If the primary key (ID) of the supplied SalesLocation is Null, a new row is added to the underlying data store and a new primary key (ID) automatically generated.  If the primary key (ID) of the supplied SalesLocation has been specified, the existing row in the data store is updated if it has not already been altered.
        /// The primary key (ID) of the created or altered SalesLocation is returned to the caller.  If an error occurs, an exception is thrown and no updates made to the data store.
        /// For concurrency errors (the data has changed in-between load and save by an external system or user), a concurrency exception is thrown.
        /// </summary>
        /// <param name="salesLocation">The salesLocation.</param>
        /// <returns></returns>
        public abstract int SaveLocation(SalesLocation salesLocation);

        public abstract IDataReader GetOpCoSalesLocations(string opCoCode);

        public abstract IDataReader GetTDCSalesLocations(string opCoCode);

        #endregion

        #region OptrakRegion

        /// <summary>
        /// Gets the regions.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetRegions();

        /// <summary>
        /// Gets the region.
        /// </summary>
        /// <param name="regionId">The region id.</param>
        /// <returns></returns>
        public abstract IDataReader GetRegion(int regionId);

        /// <summary>
        /// Gets the region.
        /// </summary>
        /// <param name="regionCode">The region code.</param>
        /// <returns></returns>
        public abstract IDataReader GetRegion(string regionCode);

        /// <summary>
        /// Deletes the region.
        /// </summary>
        /// <param name="regionId">The region id.</param>
        /// <returns></returns>
        public abstract bool DeleteRegion(int regionId);

        /// <summary>
        /// Saves the optrakRegion.
        /// </summary>
        /// <param name="optrakRegion">The optrakRegion.</param>
        /// <returns></returns>
        public abstract int SaveRegion(OptrakRegion optrakRegion);

        #endregion

        #region TransactionType

        /// <summary>
        /// Saves the type of the transaction.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public abstract int SaveTransactionType(TransactionType type);

        /// <summary>
        /// Deletes the type of the transaction.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public abstract bool DeleteTransactionType(int id);

        /// <summary>
        /// Gets the transaction type by id. 
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public abstract IDataReader GetTransactionType(int id);

        /// <summary>
        /// Gets the transaction type by code.
        /// </summary>
        /// <param name="code">The transaction type code.</param>
        /// <returns></returns>
        public abstract IDataReader GetTransactionType(string code);

        /// <summary>
        /// Gets all opco transaction types.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetOpCoTransactionTypes();

        /// <summary>
        /// Gets all transaction types.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetTransactionTypes();

        #endregion

        #region TransactionSubType

        /// <summary>
        /// Saves the type of the transaction sub.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public abstract int SaveTransactionSubType(TransactionSubType type);

        /// <summary>
        /// Deletes the type of the transaction sub.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public abstract bool DeleteTransactionSubType(int id);

        /// <summary>
        /// Gets the type of the transaction sub.
        /// </summary>
        /// <param name="transactionSubTypeCode">The transaction sub type code.</param>
        /// <returns></returns>
        public abstract IDataReader GetTransactionSubTypeByCode(string transactionSubTypeCode);

        /// <summary>
        /// Gets the type of the transaction sub.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public abstract IDataReader GetTransactionSubType(int id);

        /// <summary>
        /// Gets the transaction sub types.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetTransactionSubTypes();

        #endregion

        #region Route

        /// <summary>
        /// Deletes the route.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public abstract bool DeleteRoute(int id);

        /// <summary>
        /// Gets the route.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public abstract IDataReader GetRoute(int id);

        /// <summary>
        /// Gets the route.
        /// </summary>
        /// <param name="warehouseCode">The warehouse code.</param>
        /// <param name="routeCode">The route code.</param>
        /// <returns></returns>
        public abstract IDataReader GetRoute(string warehouseCode, string routeCode);

        /// <summary>
        /// Gets the route.
        /// </summary>
        /// <param name="routeCode">The route code.</param>
        /// <returns></returns>
        public abstract IDataReader GetRoute(string routeCode);

        /// <summary>
        /// Gets the opco routes.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetOpCoRoutes();

        /// <summary>
        /// Gets the routes.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetRoutes(string sortExpression, int startRowIndex, int maximumRows, out int totalRows);

        public abstract IDataReader GetRoutes(int warehouseId, string sortExpression, int startRowIndex, int maximumRows, out int totalRows);

        /// <summary>
        /// Saves the route.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns></returns>
        public abstract int SaveRoute(Route route);

        public abstract IDataReader GetTDCRoutesByWarehouseCode(string warehouseCode);

        #endregion

        #region Printer

        /// <summary>
        /// Saves the printer.
        /// </summary>
        /// <param name="printer">The printer.</param>
        /// <returns></returns>
        public abstract int SavePrinter(Printer printer);

        /// <summary>
        /// Deletes the printer.
        /// </summary>
        /// <param name="printer">The printer.</param>
        /// <returns></returns>
        public abstract bool DeletePrinter(int printer);

        /// <summary>
        /// Gets the printer.
        /// </summary>
        /// <param name="printerId">The printer id.</param>
        /// <returns></returns>
        public abstract IDataReader GetPrinter(int printerId);

        /// <summary>
        /// Gets the printers.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetPrinters();

        #endregion

        #region Warehouse

        /// <summary>
        /// Saves the warehouse.
        /// </summary>
        /// <param name="warehouse">The warehouse.</param>
        /// <returns></returns>
        public abstract int SaveWarehouse(Warehouse warehouse);

        /// <summary>
        /// Deletes the warehouse.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public abstract bool DeleteWarehouse(int id);

        /// <summary>
        /// Gets the warehouse.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public abstract IDataReader GetWarehouse(int id);

        /// <summary>
        /// Gets the warehouse.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public abstract IDataReader GetWarehouse(string code);

        /// <summary>
        /// Gets the warehouses.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetWarehouses();

        /// <summary>
        /// Gets the warehouses.
        /// </summary>
        /// <param name="regionCode">The region code.</param>
        /// <returns></returns>
        public abstract IDataReader GetWarehouses(string regionCode);

        /// <summary>
        /// Gets the warehouses.
        /// </summary>
        /// <param name="regionId">The region Id.</param>
        /// <returns></returns>
        public abstract IDataReader GetWarehousesByRegion(int regionId);

        /// <summary>
        /// Gets the opco stock warehouse codes.
        /// </summary>
        /// <param name="opCoCode">The opco code.</param>
        /// <returns></returns>
        public abstract IDataReader GetOpCoStockWarehouseCodes(string opCoCode);

        /// <summary>
        /// Gets the op co delivery warehouse codes.
        /// </summary>
        /// <param name="opCoCode">The op co code.</param>
        /// <returns></returns>
        public abstract IDataReader GetOpCoDeliveryWarehouseCodes(string opCoCode);

        public abstract IDataReader GetTDCDeliveryWarehouseCodes(string opCoCode);

        #endregion

        #region Logging

        /// <summary>
        /// Acknowledges the log.
        /// </summary>
        /// <param name="Id">The id of the log entry to acknowledge.</param>
        /// <param name="acknowledgedBy">Who acknowledged by.</param>
        /// <param name="acknowledgedDate">The acknowledged date.</param>
        /// <returns></returns>
        public abstract bool AcknowledgeLog(int Id, string acknowledgedBy, DateTime acknowledgedDate);

        /// <summary>
        /// Gets the log entry.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public abstract IDataReader GetLogEntry(int id);


        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetLogCategories();

        /// <summary>
        /// Gets the log entries.
        /// </summary>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <param name="totalRowCount">The total row count.</param>
        /// <returns></returns>
        public abstract IDataReader GetLogEntries(LogEntryCriteria searchCriteria, string sortExpression, int startRowIndex, int maximumRows,
                                                  out int totalRowCount);

        #endregion

        #region Framework Support

        /// <summary>
        /// Gets the entity checksum.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <returns></returns>
        public abstract int GetEntityChecksum(int id, string tableName);

        #endregion

        #region OpCov Division

        /// <summary>
        /// Gets the op co divisions.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetOpCoDivisions();

        /// <summary>
        /// Gets the op co divisions by op co.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetOpCoDivisions(string opCoCode);

        /// <summary>
        /// Gets the op co division.
        /// </summary>
        /// <param name="divisionCode">The division code.</param>
        /// <returns></returns>
        public abstract IDataReader GetOpCoDivision(string divisionCode);

        /// <summary>
        /// Gets the op co division.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public abstract IDataReader GetOpCoDivision(int id);

        /// <summary>
        /// Deletes the op co division.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public abstract bool DeleteOpCoDivision(int id);

        /// <summary>
        /// Saves the op co division.
        /// </summary>
        /// <param name="division">The division.</param>
        /// <returns></returns>
        public abstract int SaveOpCoDivision(OpCoDivision division);

        #endregion

        #region Drops

        /// <summary>
        /// Saves the drop line.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <returns></returns>
        public abstract int SaveDropLine(ShipmentDropLine line);

        /// <summary>
        /// Gets the drop. by unique key
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="tripId">The trip id.</param>
        /// <param name="sequence">The sequence.</param>
        /// <param name="dropSequence">The drop sequence.</param>
        /// <returns></returns>
        public abstract IDataReader GetDrop(int id, int tripId, int sequence, int dropSequence);

        /// <summary>
        /// Saves the drop.
        /// </summary>
        /// <param name="shipmentDrop">The shipment drop.</param>
        /// <returns></returns>
        public abstract int SaveDrop(ShipmentDrop shipmentDrop);

        #endregion

        #region Trips


        /// <summary>
        /// Gets the trip by warehouse date and number.
        /// </summary>
        /// <param name="tripNumber">The trip number.</param>
        /// <param name="tripDate">The trip date.</param>
        /// <param name="warehouseId">The warehouse id.</param>
        /// <returns></returns>
        public abstract IDataReader GetTripByWarehouseDateAndNumber(string tripNumber, DateTime tripDate, int warehouseId);

        /// <summary>
        /// Gets the shipment drops for trip.
        /// </summary>
        /// <param name="tripId">The trip id.</param>
        /// <returns></returns>
        public abstract IDataReader GetShipmentDropsForTrip(int tripId);

        /// <summary>
        /// Gets the trip.
        /// </summary>
        /// <param name="tripId">The trip id.</param>
        /// <returns></returns>
        public abstract IDataReader GetTrip(int tripId);

        /// <summary>
        /// Gets the trips.
        /// </summary>
        /// <param name="warehouseId">The warehouse id.</param>
        /// <param name="requiredDeliveryDateFrom">The required delivery date from.</param>
        /// <param name="requiredDeliveryDateTo">The required delivery date to.</param>
        /// <returns></returns>
        public abstract IDataReader GetTrips(int warehouseId, DateTime requiredDeliveryDateFrom,
                                             DateTime? requiredDeliveryDateTo);

        #endregion

        #region Optrak

        /// <summary>
        /// Sets the optrak locks.
        /// </summary>
        /// <param name="estimatedDateTo">The estimated date to.</param>
        /// <param name="processedBy">The processed by.</param>
        /// <param name="warehouseId">The warehouse id.</param>
        /// <param name="regionId">The region id.</param>
        /// <returns></returns>
        public abstract int SetOptrakLocks(DateTime estimatedDateTo, string processedBy, int warehouseId, int regionId);

        /// <summary>
        /// Sets the optrak locks.
        /// </summary>
        /// <param name="routingHistoryId">The routing history id.</param>
        /// <param name="estimatedDateTo">The estimated date to.</param>
        /// <param name="processedBy">The processed by.</param>
        /// <param name="warehouseId">The warehouse id.</param>
        /// <returns></returns>
        public abstract int SetOptrakLocks(int routingHistoryId, DateTime estimatedDateTo, string processedBy, int warehouseId);

        /// <summary>
        /// Gets summary details of shipments merged at a delivery point.
        /// </summary>
        /// <param name="routingHistoryId">.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <param name="totalRows">The total rows.</param>
        /// <returns></returns>
        public abstract IDataReader GetMergedShipments(int routingHistoryId, string sortExpression, int startRowIndex,
                                                      int maximumRows, out int totalRows);


        /// <summary>
        /// Merges the delivery points. All shipments with the specified warehouse code and Site Code (SiteCodeToUpdate)
        /// with the status of routing will be updated to the main site code
        /// </summary>
        /// <param name="routingHistoryId">The routing history id.</param>
        /// <param name="mainSiteCode">The main site code.</param>
        /// <param name="codesToUpdate">The codes to update.</param>
        /// <returns></returns>
        public abstract int MergeDeliveryPointsManually(int routingHistoryId, int mainSiteCode, List<int> codesToUpdate);

        /// <summary>
        /// Performs a reshuffle of site codes after a manually merge has occured.
        /// </summary>
        /// <param name="routingHistoryId"></param>
        /// <returns></returns>
        public abstract bool ManualMergeReshuffle(int routingHistoryId);

        /// <summary>
        /// Deletes the relationship between a routing history data and a shipment.
        /// </summary>
        /// <param name="shipmentId">The id.</param>
        /// <param name="historyId">The history id.</param>
        /// <returns></returns>
        public abstract bool DeleteShipmentFromRouting(int shipmentId, int historyId);

        /// <summary>
        /// Gets all routing history details.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetRoutingHistory();

        /// <summary>
        /// Gets the routing history details by the specified Id.
        /// </summary>
        /// <param name="routingHistoryId">The routing history id.</param>
        /// <returns></returns>
        public abstract IDataReader GetRoutingHistory(int routingHistoryId);

        /// <summary>
        /// Saves a routing history record.
        /// </summary>
        /// <param name="routingHistory">The routing history details to save.</param>
        /// <returns></returns>
        public abstract int SaveRoutingHistory(RoutingHistory routingHistory);

        /// <summary>
        /// Gets the shipments for delivery point.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public abstract IDataReader GetShipmentsForDeliveryPoint(string code, int id);

        /// <summary>
        /// Gets the shipments by routing history id.
        /// </summary>
        /// <param name="routingHistoryId">The routing history id.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="pageIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <param name="totalRows">The total rows.</param>
        /// <returns></returns>
        public abstract IDataReader GetShipmentsByRoutingHistoryId(int routingHistoryId, string sortExpression,
                                                           int pageIndex,
                                                           int maximumRows, out int totalRows);

        /// <summary>
        /// Adds the shipment to routing.
        /// </summary>
        /// <param name="shipmentId">The shipment id.</param>
        /// <param name="routingHistoryId">The routing history id.</param>
        /// <returns></returns>
        public abstract bool AddShipmentToRouting(int shipmentId, int routingHistoryId);

        /// <summary>
        /// Resets the routing locks. This means that the routing history record will be updated 
        /// and the status of the shipments which are locked will return to Mapped
        /// </summary>
        /// <param name="routingHistoryId">The routing history id.</param>
        /// <param name="resetBy">The reset by.</param>
        /// <returns></returns>
        public abstract bool ResetRoutingLocks(int routingHistoryId, string resetBy);

        /// <summary>
        /// Removes the items from routing. 
        /// </summary>
        /// <param name="routingHistoryId">The routing history id.</param>
        /// <param name="removedBy">The removed by.</param>
        /// <returns></returns>
        public abstract bool RemoveItemsFromRouting(int routingHistoryId, string removedBy);

        #endregion

        #region Load Category

        /// <summary>
        /// Gets the load category.
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <returns></returns>
        public abstract IDataReader GetLoadCategory(int categoryId);

        /// <summary>
        /// Gets the load category.
        /// </summary>
        /// <param name="categoryCode">The category code.</param>
        /// <returns></returns>
        public abstract IDataReader GetLoadCategory(string categoryCode);

        /// <summary>
        /// Gets the load categories.
        /// </summary>
        /// <returns></returns>
        public abstract IDataReader GetLoadCategories();

        /// <summary>
        /// Saves the load category.
        /// </summary>
        /// <param name="loadCategory">The load category.</param>
        /// <returns></returns>
        public abstract int SaveLoadCategory(LoadCategory loadCategory);

        /// <summary>
        /// Deletes the load category.
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <returns></returns>
        public abstract bool DeleteLoadCategory(int categoryId);

        #endregion

        /// <summary>
        /// Gets the type of the error.
        /// </summary>
        /// <param name="exceptionType">Type of the exception.</param>
        /// <param name="policyName">Name of the policy.</param>
        /// <param name="opCoCode">The op co code.</param>
        /// <returns></returns>
        public abstract IDataReader GetErrorType(string exceptionType, string policyName, string opCoCode);

        /// <summary>
        /// Saves the type of the error.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public abstract int SaveErrorType(ErrorType type);

        /// <summary>
        /// Saves the TDC shipment location code.
        /// </summary>
        /// <param name="shipment">The shipment.</param>
        /// <returns></returns>
        public abstract bool SaveTDCShipmentLocationCode(TDCShipment shipment);

        /// <summary>
        /// Gets the TDC shipment lines by routing id.
        /// </summary>
        /// <param name="routingHistoryId">The routing history id.</param>
        /// <returns></returns>
        public abstract IDataReader GetTDCShipmentLinesByRoutingId(int routingHistoryId);

        /// <summary>
        /// Gets the routing history by shipment line id.
        /// </summary>
        /// <param name="shipmentLineId">The shipment line id.</param>
        /// <returns></returns>
        public abstract IDataReader GetRoutingHistoryByShipmentLineId(int shipmentLineId);

        /// <summary>
        /// Gets the routing history by shipment id.
        /// </summary>
        /// <param name="shipmentId">The shipment id.</param>
        /// <returns></returns>
        public abstract IDataReader GetRoutingHistoryByShipmentId(int shipmentId);

        /// <summary>
        /// Sets the shipments to routed.
        /// </summary>
        /// <param name="routingHistoryId">The routing history id.</param>
        /// <param name="updatedBy">The updated by.</param>
        /// <param name="updatedDate">The updated date.</param>
        /// <returns></returns>
        public abstract bool SetShipmentsToRouted(int routingHistoryId,string updatedBy,DateTime updatedDate);

        /// <summary>
        /// Gets the TDC shipment line.
        /// </summary>
        /// <param name="lineCode">The line code.</param>
        /// <param name="opcoCode">The opco code.</param>
        /// <param name="shipmentNumber">The shipment number.</param>
        /// <param name="despatchNumber">The despatch number.</param>
        /// <returns></returns>
        public abstract IDataReader GetTDCShipmentLine(int lineCode, string opcoCode, string shipmentNumber, string despatchNumber);

    
    }
}