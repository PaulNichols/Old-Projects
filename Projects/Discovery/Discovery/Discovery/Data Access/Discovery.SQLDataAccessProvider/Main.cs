/*************************************************************************************************
 ** FILE:	Main.cs
 ** DATE:	30/05/2006
 ** AUTHOR:	Lee Spring
 **
 **
 ** OVERVIEW:
 **
 ** MODIFICATION HISTORY:
 **
 ** Date:		Version:	Who:	Change:
 ** 30/5/06		1.0		    LAS	    Initial Version
 ************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Discovery.BusinessObjects;
using Discovery.BusinessObjects.Controllers;
using Discovery.Utility;
using Discovery.Utility.Configuration;
using Discovery.Utility.DataAccess.Exceptions;
using ForeignKeyConstraint = Discovery.Utility.DataAccess.Exceptions.ForeignKeyConstraint;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Discovery.ComponentServices.DataAccess
{
    ///<summary>
    ///</summary>
    public partial class SQLDataAccessProvider : DataAccessProvider
    {
        private const int ERRORVALUE = -1;

        #region Provider Details

        protected override void Initialise(Provider provider)
        {
            // Read the attributes for this provider
            connectionString = provider.Attributes["connectionString"];
            upgradeConnectionString = provider.Attributes["upgradeConnectionString"];
            providerPath = provider.Attributes["provIderPath"];
            objectQualifier = provider.Attributes["objectQualifier"];
            databaseOwner = provider.Attributes["databaseOwner"];

            if (objectQualifier != "" && !objectQualifier.EndsWith("_"))
            {
                // Append "_" to qualifier name if qualifier specified and name does not end with "_"
                objectQualifier += "_";
            }

            if (databaseOwner != "" && !databaseOwner.EndsWith("."))
            {
                // Append "." to database owner if owner specified and does not end with "."
                databaseOwner += ".";
            }

            // See if we have an upgrade connection string
            if ("" == upgradeConnectionString)
            {
                upgradeConnectionString = connectionString;
            }
        }

        private string connectionString;
        private string upgradeConnectionString;
        private string providerPath;
        private string objectQualifier;
        private string databaseOwner;

        public string ConnectionString
        {
            get { return connectionString; }
        }

        public string ProviderPath
        {
            get { return providerPath; }
        }

        public string ObjectQualifier
        {
            get { return objectQualifier; }
        }

        public string DatabaseOwner
        {
            get { return databaseOwner; }
        }

        private IDataReader ExecuteReader(string spNameWithoutQualifier)
        {
            Database database = DatabaseFactory.CreateDatabase(ConnectionString);
            return database.ExecuteReader(String.Concat(ObjectQualifier, spNameWithoutQualifier), new object[] { });
        }

        private IDataReader ExecuteReader(string spNameWithoutQualifier, params object[] sqlParameters)
        {
            Database database = DatabaseFactory.CreateDatabase(ConnectionString);
            return database.ExecuteReader(String.Concat(ObjectQualifier, spNameWithoutQualifier), sqlParameters);
        }

        private bool ExecuteNonQuery(string spNameWithoutQualifier, params object[] sqlParameters)
        {
            Database database = DatabaseFactory.CreateDatabase(ConnectionString);
            return
                (database.ExecuteNonQuery(String.Concat(ObjectQualifier, spNameWithoutQualifier), sqlParameters) > 0)
                    ? true
                    : false;
        }

        private T ExecuteScalar<T>(string spNameWithoutQualifier, T errorValue, params object[] sqlParameters)
        {
            Database database = DatabaseFactory.CreateDatabase(ConnectionString);
            object returnValue =
                database.ExecuteScalar(String.Concat(ObjectQualifier, spNameWithoutQualifier), sqlParameters);
            T identity;

            if (returnValue is T && !returnValue.Equals(errorValue))
                identity = (T)returnValue;
            else
                throw new ConcurrencyException("No records were updated, please reload your data.");

            return identity;
        }


        //private DbCommand GetCommandWithParameters(string spNameWithoutQualifier, bool openConnection)
        //{
        //    Database database = DatabaseFactory.CreateDatabase(ConnectionString);
        //    DbCommand command = database.GetStoredProcCommand(String.Concat(ObjectQualifier, spNameWithoutQualifier));
        //    database.DiscoverParameters(command);
        //    if (openConnection)
        //    {
        //        command.Connection = database.CreateConnection();
        //        command.Connection.Open();
        //    }

        //    return command;
        //}

        //private DbCommand GetCommand(string spNameWithoutQualifier, bool openConnection)
        //{
        //    Database database = DatabaseFactory.CreateDatabase(ConnectionString);
        //    DbCommand command = database.GetStoredProcCommand(String.Concat(ObjectQualifier, spNameWithoutQualifier));
        //    // database.DiscoverParameters(command);
        //    if (openConnection)
        //    {
        //        command.Connection = database.CreateConnection();
        //        command.Connection.Open();
        //    }

        //    return command;
        //}

        //private DbCommand GetCommandWithParameters(string spNameWithoutQualifier)
        //{
        //    return GetCommandWithParameters(spNameWithoutQualifier, false);
        //}

        #endregion

        #region Log Entry

        public override IDataReader GetLogEntry(int logId)
        {
            return ExecuteReader("GetLogEntry", logId);
        }

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns></returns>
        public override IDataReader GetLogCategories()
        {
            return ExecuteReader("GetLogCategories");
        }

        public override IDataReader GetLogEntries(LogEntryCriteria searchCriteria, string sortExpression, int startRowIndex, int maximumRows, out int totalRowCount)
        {
            totalRowCount = ExecuteScalar("GetLogCount", -1,
                    Null.GetNull(searchCriteria.CategoryId, DBNull.Value),
                    Null.GetNull(searchCriteria.Acknowledged, DBNull.Value),
                    Null.GetNull(searchCriteria.ErrorType, DBNull.Value),  
                    Null.GetNull(searchCriteria.MessageText, DBNull.Value),  
                    Null.GetNull(searchCriteria.OpcoCode, DBNull.Value), 
                    Null.GetNull(searchCriteria.Priority, DBNull.Value), 
                    Null.GetNull(searchCriteria.Severity, DBNull.Value), 
                    Null.GetNull(searchCriteria.TimeStampFrom, DBNull.Value), 
                    Null.GetNull(searchCriteria.TimeStampTo, DBNull.Value) 
                    );

            return ExecuteReader("GetLogEntries",
                    Null.GetNull(searchCriteria.CategoryId, DBNull.Value),
                    Null.GetNull(searchCriteria.Acknowledged, DBNull.Value),
                    Null.GetNull(searchCriteria.ErrorType, DBNull.Value),  
                    Null.GetNull(searchCriteria.MessageText, DBNull.Value),  
                    Null.GetNull(searchCriteria.OpcoCode, DBNull.Value), 
                    Null.GetNull(searchCriteria.Priority, DBNull.Value), 
                    Null.GetNull(searchCriteria.Severity, DBNull.Value), 
                    Null.GetNull(searchCriteria.TimeStampFrom, DBNull.Value), 
                    Null.GetNull(searchCriteria.TimeStampTo, DBNull.Value) ,
                    sortExpression, 
                    startRowIndex, 
                    maximumRows);
        }

        #endregion

        #region Framework Support

        /// <summary>
        /// Gets the entity checksum.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <returns></returns>
        public override int GetEntityChecksum(int id, string tableName)
        {
            return ExecuteScalar("GetEntityChecksum", -1, ObjectQualifier, id, tableName);
        }

        #endregion

        #region OpCo Shipment Methods

        //public override List<int> SaveShipments(List<OpCoShipment> shipment)
        //{
        //    throw new NotImplementedException();
        //}

        public override IDataReader GetOpCoShipment(string opCo, string shipmentNumber, string despatchNumber)
        {
            // Get the specific opco shipment
            return ExecuteReader("GetOpCoShipmentByShipmentNumber", opCo, shipmentNumber, despatchNumber);
        }

        public override IDataReader GetOpCoShipment(int shipmentId)
        {
            return ExecuteReader("GetOpCoShipment", shipmentId);
        }

        public override IDataReader GetOpCoShipments(string deliveryLocation)
        {
            return ExecuteReader("GetOpCoShipments");
        }

        public override IDataReader GetOpCoShipments(
                    ShipmentCriteria criteria,
                    string sortExpression,
                    int pageIndex,
                    int numRows)
        //ref int totalRows)
        {
            IDataReader reader;

            try
            {
                reader = ExecuteReader("GetOpCoShipmentsByCriteria",
                    Null.GetNull(criteria.OpCoCode, DBNull.Value),              // OpCoCode
                    Null.GetNull(criteria.ShipmentNumber, DBNull.Value),        // Shipment number
                    Null.GetNull(criteria.CustomerAccountNumber, DBNull.Value), // Customer number
                    Null.GetNull(criteria.CustomerName, DBNull.Value),          // Customer name                  
                    Null.GetNull(criteria.ShipmentStatus, DBNull.Value),        //Shipment Status        
                    Null.GetNull(criteria.SalesBranchCode, DBNull.Value),       //Sales Branch Code
                    Null.GetNull(criteria.RouteCode, DBNull.Value),             //Route Code
                    Null.GetNull(criteria.StockWarehouseCode, DBNull.Value),    //Stock Warehouse Code
                    Null.GetNull(criteria.DeliveryWarehouseCode, DBNull.Value), //Delivery Warehouse Code
                    Null.GetNull(criteria.RequiredDateFrom, DBNull.Value),      //Required Date From
                    Null.GetNull(criteria.RequiredDateTo, DBNull.Value),        //Required Date To
                    Null.GetNull(criteria.TransactionType, DBNull.Value),       //Transaction type
                    Null.GetNull(sortExpression, DBNull.Value),                 //Sort expression
                    pageIndex,                                                  // Page index
                    numRows                                                     // Num rows to return
                );
            }
            catch (SqlException ex)
            {
                // See if it's a contraint error
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    throw new ConstraintException(ex.Message, ex);
                }
                else
                {
                    // Re-throw the exception
                    throw;
                }
            }

            return reader;
        }

        public override int GetOpCoShipmentsCount(ShipmentCriteria criteria)
        {
            int returnValue;

            try
            {
                returnValue = ExecuteScalar("GetOpCoShipmentsCountByCriteria", ERRORVALUE,
                                    Null.GetNull(criteria.OpCoCode, DBNull.Value),              // OpCoCode
                                    Null.GetNull(criteria.ShipmentNumber, DBNull.Value),        // Shipment number
                                    Null.GetNull(criteria.CustomerAccountNumber, DBNull.Value), // Customer number
                                    Null.GetNull(criteria.CustomerName, DBNull.Value),          // Customer name                  
                                    Null.GetNull(criteria.ShipmentStatus, DBNull.Value),        //Shipment Status        
                                    Null.GetNull(criteria.SalesBranchCode, DBNull.Value),       //Sales Branch Code
                                    Null.GetNull(criteria.RouteCode, DBNull.Value),             //Route Code
                                    Null.GetNull(criteria.StockWarehouseCode, DBNull.Value),    //Stock Warehouse Code
                                    Null.GetNull(criteria.DeliveryWarehouseCode, DBNull.Value), //Delivery Warehouse Code
                                    Null.GetNull(criteria.RequiredDateFrom, DBNull.Value),      //Required Date From
                                    Null.GetNull(criteria.RequiredDateTo, DBNull.Value),        //Required Date To
                                    Null.GetNull(criteria.TransactionType, DBNull.Value)       //Transaction type
                                );
            }
            catch (SqlException ex)
            {
                // See if it's a contraint error
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    throw new ConstraintException(ex.Message, ex);
                }
                else
                {
                    // Re-throw the exception
                    throw;
                }
            }

            return returnValue;
        }

        public override int SaveOpCoShipment(OpCoShipment shipment)
        {
            int returnValue;

            try
            {
                returnValue = ExecuteScalar("SaveOpCoShipment", ERRORVALUE,
                                                 shipment.Id,
                                                 shipment.OpCoCode,
                                                 shipment.OpCoSequenceNumber,
                                                 Null.GetNull(shipment.OpCoContact.Email, DBNull.Value),
                                                 Null.GetNull(shipment.OpCoContact.Name, DBNull.Value),
                                                 shipment.DespatchNumber,
                                                 shipment.RequiredShipmentDate,
                                                 shipment.TransactionTypeCode,
                                                 Null.GetNull(shipment.CustomerReference, DBNull.Value),
                                                 Null.GetNull(shipment.Instructions, DBNull.Value),
                                                 shipment.RouteCode,
                                                 Null.GetNull(shipment.CustomerNumber, DBNull.Value),
                                                 shipment.CustomerName,
                                                 Null.GetNull(shipment.CustomerAddress.Line1, DBNull.Value),
                                                 Null.GetNull(shipment.CustomerAddress.Line2, DBNull.Value),
                                                 Null.GetNull(shipment.CustomerAddress.Line3, DBNull.Value),
                                                 Null.GetNull(shipment.CustomerAddress.Line4, DBNull.Value),
                                                 Null.GetNull(shipment.CustomerAddress.Line5, DBNull.Value),
                                                 Null.GetNull(shipment.CustomerAddress.PostCode, DBNull.Value),
                                                 shipment.ShipmentNumber,
                                                 shipment.ShipmentName,
                                                 Null.GetNull(shipment.ShipmentAddress.Line1, DBNull.Value),
                                                 Null.GetNull(shipment.ShipmentAddress.Line2, DBNull.Value),
                                                 Null.GetNull(shipment.ShipmentAddress.Line3, DBNull.Value),
                                                 Null.GetNull(shipment.ShipmentAddress.Line4, DBNull.Value),
                                                 Null.GetNull(shipment.ShipmentAddress.Line5, DBNull.Value),
                                                 Null.GetNull(shipment.ShipmentAddress.PostCode, DBNull.Value),
                                                 Null.GetNull(shipment.ShipmentContact.Name, DBNull.Value),
                                                 Null.GetNull(shipment.ShipmentContact.TelephoneNumber, DBNull.Value),
                                                 Null.GetNull(shipment.ShipmentContact.Email, DBNull.Value),
                                                 shipment.SalesBranchCode,
                                                 Null.GetNull(shipment.AfterTime, DBNull.Value),
                                                 Null.GetNull(shipment.BeforeTime, DBNull.Value),
                                                 shipment.TailLiftRequired,
                                                 Null.GetNull(shipment.VehicleMaxWeight, DBNull.Value),
                                                 Null.GetNull(shipment.CheckInTime, DBNull.Value),
                                                 shipment.DeliveryWarehouseCode,
                                                 Null.GetNull(shipment.StockWarehouseCode, DBNull.Value),
                                                 shipment.DivisionCode,
                                                 shipment.GeneratedDateTime,
                                                 shipment.Status,
                                                 Null.GetNull(shipment.AuditId, DBNull.Value),
                                                 shipment.UpdatedBy,
                                                 shipment.CheckSum
                                             );
            }
            catch (SqlException ex)
            {
                // See if it's a contraint error
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    throw new ConstraintException(ex.Message, ex);
                }
                else
                {
                    // Re-throw the exception
                    throw;
                }
            }

            return returnValue;
        }

        public override bool DeleteOpCoShipment(int shipmentId)
        {
            bool returnValue;

            try
            {
                returnValue = ExecuteNonQuery("DeleteOpCoShipment", shipmentId);
            }
            catch (SqlException ex)
            {
                // Is it a foreign key constraint
                if (ex.Number == 547)
                {
                    throw new ForeignKeyConstraint(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }
            return returnValue;
        }

        public override List<int> SaveOpCoShipments(List<OpCoShipment> shipment)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateOpCoShipmentStatus(OpCoShipment shipment)
        {
            // Seed to failed to update
            bool returnValue;

            try
            {
                returnValue = ExecuteNonQuery("UpdateOpCoShipmentStatus", shipment.Id, shipment.Status);
            }
            catch (SqlException ex)
            {
                // Is it a foreign key constraint
                if (ex.Number == 547)
                {
                    throw new ForeignKeyConstraint(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }


            // Did we update
            return returnValue;
        }

        #endregion

        #region TDC Shipment Methods

        public override List<int> SaveTDCShipments(List<TDCShipment> shipment)
        {
            throw new NotImplementedException();
        }

        public override bool SaveTDCShipmentLocationCode(TDCShipment shipment)
        {
            int returnValue;

            try
            {
                returnValue = ExecuteScalar("SaveTDCShipmentLocationCode", ERRORVALUE, shipment.Id, shipment.LocationCode);
            }
            catch (SqlException ex)
            {
                // See if it's a contraint error
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    throw new ConstraintException(ex.Message, ex);
                }
                else
                {
                    // Re-throw the exception
                    throw;
                }
            }

            return returnValue != -1;
        }

        public override int SaveTDCShipment(TDCShipment shipment)
        {
            int returnValue;

            try
            {
                returnValue = ExecuteScalar("SaveTDCShipment", ERRORVALUE,
                                                         shipment.Id,
                                                         shipment.Type,
                                                         shipment.OpCoCode,
                                                         shipment.OpCoSequenceNumber,
                                                         Null.GetNull(shipment.OpCoContact.Email, DBNull.Value),
                                                         Null.GetNull(shipment.OpCoContact.Name, DBNull.Value),
                                                         shipment.DespatchNumber,
                                                         shipment.RequiredShipmentDate,
                                                         shipment.TransactionTypeCode,
                                                         Null.GetNull(shipment.CustomerReference, DBNull.Value),
                                                         shipment.Instructions,
                                                         shipment.RouteCode,
                                                         Null.GetNull(shipment.CustomerNumber, DBNull.Value),
                                                         shipment.CustomerName,
                                                         Null.GetNull(shipment.CustomerAddress.Line1, DBNull.Value),
                                                         Null.GetNull(shipment.CustomerAddress.Line2, DBNull.Value),
                                                         Null.GetNull(shipment.CustomerAddress.Line3, DBNull.Value),
                                                         Null.GetNull(shipment.CustomerAddress.Line4, DBNull.Value),
                                                         Null.GetNull(shipment.CustomerAddress.Line5, DBNull.Value),
                                                         Null.GetNull(shipment.CustomerAddress.PostCode, DBNull.Value),
                                                         shipment.ShipmentNumber,
                                                         shipment.ShipmentName,
                                                         Null.GetNull(shipment.ShipmentAddress.Line1, DBNull.Value),
                                                         Null.GetNull(shipment.ShipmentAddress.Line2, DBNull.Value),
                                                         Null.GetNull(shipment.ShipmentAddress.Line3, DBNull.Value),
                                                         Null.GetNull(shipment.ShipmentAddress.Line4, DBNull.Value),
                                                         Null.GetNull(shipment.ShipmentAddress.Line5, DBNull.Value),
                                                         Null.GetNull(shipment.ShipmentAddress.PostCode, DBNull.Value),
                                                         Null.GetNull(shipment.ShipmentContact.Name, DBNull.Value),
                                                         Null.GetNull(shipment.ShipmentContact.TelephoneNumber, DBNull.Value),
                                                         Null.GetNull(shipment.ShipmentContact.Email, DBNull.Value),
                                                         shipment.SalesBranchCode,
                                                         Null.GetNull(shipment.AfterTime, DBNull.Value),
                                                         Null.GetNull(shipment.BeforeTime, DBNull.Value),
                                                         shipment.TailLiftRequired,
                                                         Null.GetNull(shipment.VehicleMaxWeight, DBNull.Value),
                                                         Null.GetNull(shipment.CheckInTime, DBNull.Value),
                                                         shipment.DeliveryWarehouseCode,
                                                         Null.GetNull(shipment.StockWarehouseCode, DBNull.Value),
                                                         shipment.DivisionCode,
                                                         shipment.GeneratedDateTime,
                                                         shipment.Status,
                                                         shipment.AuditId,
                                                         Null.GetNull(shipment.ActualDeliveryDate, DBNull.Value),
                                                         Null.GetNull(shipment.EstimatedDeliveryDate, DBNull.Value),
                                                         shipment.IsRecurring,
                                                         shipment.IsValidAddress,
                                                         shipment.LocationCode,
                                                         shipment.PAFAddress.Line1,
                                                         shipment.PAFAddress.Line2,
                                                         shipment.PAFAddress.Line3,
                                                         shipment.PAFAddress.Line4,
                                                         shipment.PAFAddress.Line5,
                                                         shipment.PAFAddress.PostCode,
                                                         shipment.PAFAddress.DPS,
                                                         shipment.PAFAddress.Easting,
                                                         shipment.PAFAddress.Northing,
                                                         shipment.PAFAddress.Location,
                                                         shipment.PAFAddress.Match,
                                                         Null.GetNull(shipment.OpCoShipmentId, DBNull.Value),
                                                         Null.GetNull(shipment.SentToWMS, DBNull.Value),
                                                         shipment.SplitSequence,
                                                         shipment.IsSplit,
                                                         Null.GetNull(shipment.TransactionSubTypeCode, DBNull.Value),
                                                         shipment.UpdatedBy,
                                                         shipment.CheckSum
                                                     );
            }
            catch (SqlException ex)
            {
                // See if it's a contraint error
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    throw new ConstraintException(ex.Message, ex);
                }
                else
                {
                    // Re-throw the exception
                    throw;
                }
            }

            return returnValue;
        }

        public override IDataReader GetDeliveryLocations()
        {
            throw new NotImplementedException();
        }

        public override IDataReader GetStockLocations()
        {
            throw new NotImplementedException();
        }

        public override IDataReader GetRouteCodes()
        {
            throw new NotImplementedException();
        }

        public override IDataReader GetDeliveries()
        {
            throw new NotImplementedException();
        }

        public override IDataReader GetCollections()
        {
            throw new NotImplementedException();
        }

        public override IDataReader GetTransfers()
        {
            throw new NotImplementedException();
        }


        public override IDataReader GetTDCShipment(string opCo, string shipmentNumber, string despatchNumber)
        {
            // Get the specific opco shipment
            return ExecuteReader("GetTDCShipmentByShipmentNumber", opCo, shipmentNumber, despatchNumber);
        }

        public override IDataReader GetTDCShipment(int ShipmentId)
        {
            // Get the specific opco shipment
            return ExecuteReader("GetTDCShipment", ShipmentId);
        }

        public override IDataReader GetTrunkedStockSummary(int deliveryWarehouse, DateTime requiredDate, int routeCode)
        {
            // Get the specific opco shipment
            return ExecuteReader("GetTDCShipmentTrunkedStockSummary", deliveryWarehouse, routeCode, requiredDate);
        }

        public override IDataReader GetTDCShipmentsByRoute(int deliveryWarehouseId, DateTime requiredDate, bool includeSpecials)
        {
            // Get the specific opco shipment
            return ExecuteReader("GetTDCShipmentsByRoute", deliveryWarehouseId, requiredDate, includeSpecials);
        }


        /// <summary>
        /// Saves the trip.
        /// </summary>
        /// <param name="trip">The trip.</param>
        /// <returns></returns>
        public override int SaveTrip(Trip trip)
        {
            int returnValue;

            try
            {
                returnValue = ExecuteScalar("SaveTrip", ERRORVALUE,
                                        trip.Id,
                                        trip.WarehouseId,
                                        Null.GetNull( trip.TripNumber,DBNull.Value),
                                        Null.GetNull(trip.AssignedDriver,DBNull.Value) ,    
                                        Null.GetNull( trip.LeaveTime,DBNull.Value),   
                                        Null.GetNull( trip.FinishTime,DBNull.Value),   
                                        Null.GetNull( trip.DeliveryCount,DBNull.Value) ,
                                        Null.GetNull( trip.CollectionCount,DBNull.Value) ,
                                        Null.GetNull(trip.DeliveryWeight,DBNull.Value),
                                        Null.GetNull(trip.DeliveryVolume,DBNull.Value),
                                        Null.GetNull( trip.CollectionWeight,DBNull.Value) ,
                                        Null.GetNull( trip.CollectionVolume,DBNull.Value),
                                        Null.GetNull( trip.PeakWeight,DBNull.Value),
                                        Null.GetNull( trip.PeakVolume ,DBNull.Value),
                                        Null.GetNull(trip.Feasible ,DBNull.Value),
                                        Null.GetNull( trip.ItemCount ,DBNull.Value),
                                        Null.GetNull( trip.TotalDistance,DBNull.Value),
                                        Null.GetNull(trip.TravellingTime,DBNull.Value),   
                                        Null.GetNull(trip.WaitingTime,DBNull.Value),   
                                        Null.GetNull( trip.LoadingTime,DBNull.Value),   
                                        Null.GetNull( trip.TotalTime,DBNull.Value),   
                                        Null.GetNull( trip.VehicleRegistration,DBNull.Value),
                                        Null.GetNull( trip.MaximumLoadWeight,DBNull.Value),
                                        Null.GetNull(trip.MaximumLoadVolume,DBNull.Value),
                                        Null.GetNull(trip.VehicleCost ,DBNull.Value),
                                        Null.GetNull( trip.RegionId ,DBNull.Value),
                                        Null.GetNull( trip.DropsOnTrip ,DBNull.Value),
                                        Null.GetNull( trip.StartDate ,DBNull.Value),
                                        Null.GetNull(trip.CheckSum, DBNull.Value));
            }
            catch (SqlException ex)
            {
                // See if it's a contraint error
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    throw new ConstraintException(ex.Message, ex);
                }
                else
                {
                    // Re-throw the exception
                    throw;
                }
            }

            return returnValue;
        }

        public override IDataReader GetTrips(ShipmentCriteria criteria)
        {
            IDataReader reader;

            try
            {
                reader = ExecuteReader("GetTDCShipmentTripsByCriteria",
                    Null.GetNull(criteria.OpCoCode, DBNull.Value),              // OpCoCode
                    Null.GetNull(criteria.ShipmentNumber, DBNull.Value),        // Shipment number
                    Null.GetNull(criteria.CustomerAccountNumber, DBNull.Value), // Customer number
                    Null.GetNull(criteria.CustomerName, DBNull.Value),          // Customer name                  
                    Null.GetNull(criteria.ShipmentStatus, DBNull.Value),        //Shipment Status        
                    Null.GetNull(criteria.SalesBranchCode, DBNull.Value),       //Sales Branch Code
                    Null.GetNull(criteria.RouteCode, DBNull.Value),             //Route Code
                    Null.GetNull(criteria.RouteTrip, DBNull.Value),             //Route Trip
                    Null.GetNull(criteria.RouteDrop, DBNull.Value),             //Route Drop
                    //DBNull.Value,                                               //Location
                    Null.GetNull(criteria.StockWarehouseCode, DBNull.Value),    //Stock Warehouse Code
                    Null.GetNull(criteria.DeliveryWarehouseCode, DBNull.Value), //Delivery Warehouse Code
                    Null.GetNull(criteria.RequiredDateFrom, DBNull.Value),      //Required Date From
                    Null.GetNull(criteria.RequiredDateTo, DBNull.Value)         //Required Date To
                );
            }
            catch (SqlException ex)
            {
                // See if it's a contraint error
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    throw new ConstraintException(ex.Message, ex);
                }
                else
                {
                    // Re-throw the exception
                    throw;
                }
            }

            return reader;
        }

        public override IDataReader GetDrops(ShipmentCriteria criteria)
        {
            IDataReader reader;

            try
            {
                reader = ExecuteReader("GetTDCShipmentDropsByCriteria",
                    Null.GetNull(criteria.OpCoCode, DBNull.Value),              // OpCoCode
                    Null.GetNull(criteria.ShipmentNumber, DBNull.Value),        // Shipment number
                    Null.GetNull(criteria.CustomerAccountNumber, DBNull.Value), // Customer number
                    Null.GetNull(criteria.CustomerName, DBNull.Value),          // Customer name                  
                    Null.GetNull(criteria.ShipmentStatus, DBNull.Value),        //Shipment Status        
                    Null.GetNull(criteria.SalesBranchCode, DBNull.Value),       //Sales Branch Code
                    Null.GetNull(criteria.RouteCode, DBNull.Value),             //Route Code
                    Null.GetNull(criteria.RouteTrip, DBNull.Value),             //Route Trip
                    Null.GetNull(criteria.RouteDrop, DBNull.Value),             //Route Drop
                    //DBNull.Value,                                               //Location
                    Null.GetNull(criteria.StockWarehouseCode, DBNull.Value),    //Stock Warehouse Code
                    Null.GetNull(criteria.DeliveryWarehouseCode, DBNull.Value), //Delivery Warehouse Code
                    Null.GetNull(criteria.RequiredDateFrom, DBNull.Value),      //Required Date From
                    Null.GetNull(criteria.RequiredDateTo, DBNull.Value)         //Required Date To
                );
            }
            catch (SqlException ex)
            {
                // See if it's a contraint error
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    throw new ConstraintException(ex.Message, ex);
                }
                else
                {
                    // Re-throw the exception
                    throw;
                }
            }

            return reader;

        }

        public override IDataReader GetTDCShipments(string deliveryLocation)
        {
            throw new NotImplementedException();
        }

        public override IDataReader GetTDCShipments(
                    ShipmentCriteria criteria,
                    string sortExpression,
                    int pageIndex,
                    int numRows)
        //ref int totalRows)
        {
            IDataReader reader;

            try
            {
                reader = ExecuteReader("GetTDCShipmentsByCriteria",
                    Null.GetNull(criteria.OpCoCode, DBNull.Value),              // OpCoCode
                    Null.GetNull(criteria.ShipmentNumber, DBNull.Value),        // Shipment number
                    Null.GetNull(criteria.CustomerAccountNumber, DBNull.Value), // Customer number
                    Null.GetNull(criteria.CustomerName, DBNull.Value),          // Customer name                  
                    Null.GetNull(criteria.ShipmentStatus, DBNull.Value),        //Shipment Status        
                    Null.GetNull(criteria.SalesBranchCode, DBNull.Value),       //Sales Branch Code
                    Null.GetNull(criteria.RouteCode, DBNull.Value),             //Route Code
                    Null.GetNull(criteria.RouteTrip, DBNull.Value),             //Route Trip
                    Null.GetNull(criteria.RouteDrop, DBNull.Value),             //Route Drop
                    Null.GetNull(criteria.StockWarehouseCode, DBNull.Value),    //Stock Warehouse Code
                    Null.GetNull(criteria.DeliveryWarehouseCode, DBNull.Value), //Delivery Warehouse Code
                    Null.GetNull(criteria.RequiredDateFrom, DBNull.Value),      //Required Date From
                    Null.GetNull(criteria.RequiredDateTo, DBNull.Value),        //Required Date To
                     Null.GetNull(criteria.EstimatedDateFrom, DBNull.Value),      //Estimated Date From
                    Null.GetNull(criteria.EstimatedDateTo, DBNull.Value),        //Estimated Date To
                    Null.GetNull(criteria.TransactionType, DBNull.Value),       //Transaction Type
                    Null.GetNull(criteria.TransactionSubType, DBNull.Value),    //Transaction Sub Type
                    Null.GetNull(criteria.Type, DBNull.Value),                  //Shipment type
                    Null.GetNull(sortExpression, DBNull.Value),                 //Sort expression
                    pageIndex,                                                  // Page index
                    numRows                                                     // Num rows to return
                );
            }
            catch (SqlException ex)
            {
                // See if it's a contraint error
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    throw new ConstraintException(ex.Message, ex);
                }
                else
                {
                    // Re-throw the exception
                    throw;
                }
            }

            return reader;
        }

        public override int GetTDCShipmentsCount(ShipmentCriteria criteria)
        {
            int returnValue;

            try
            {
                returnValue = ExecuteScalar("GetTDCShipmentsCountByCriteria", ERRORVALUE,
                                    Null.GetNull(criteria.OpCoCode, DBNull.Value),              // OpCoCode
                                    Null.GetNull(criteria.ShipmentNumber, DBNull.Value),        // Shipment number
                                    Null.GetNull(criteria.CustomerAccountNumber, DBNull.Value), // Customer number
                                    Null.GetNull(criteria.CustomerName, DBNull.Value),          // Customer name                  
                                    Null.GetNull(criteria.ShipmentStatus, DBNull.Value),        //Shipment Status        
                                    Null.GetNull(criteria.SalesBranchCode, DBNull.Value),       //Sales Branch Code
                                    Null.GetNull(criteria.RouteCode, DBNull.Value),             //Route Code
                                    Null.GetNull(criteria.RouteTrip, DBNull.Value),             //Route Trip
                                    Null.GetNull(criteria.RouteDrop, DBNull.Value),             //Route Drop
                                    Null.GetNull(criteria.StockWarehouseCode, DBNull.Value),    //Stock Warehouse Code
                                    Null.GetNull(criteria.DeliveryWarehouseCode, DBNull.Value), //Delivery Warehouse Code
                                    Null.GetNull(criteria.RequiredDateFrom, DBNull.Value),      //Required Date From
                                    Null.GetNull(criteria.RequiredDateTo, DBNull.Value),        //Required Date To
                                    Null.GetNull(criteria.EstimatedDateFrom, DBNull.Value),     //Estimated Date From
                                    Null.GetNull(criteria.EstimatedDateTo, DBNull.Value),       //Estimated Date To
                                    Null.GetNull(criteria.TransactionType, DBNull.Value),       //Transaction Type
                                    Null.GetNull(criteria.TransactionSubType, DBNull.Value),    //Transaction Sub Type
                                    Null.GetNull(criteria.Type, DBNull.Value)                   //Shipment type
                                );
            }
            catch (SqlException ex)
            {
                // See if it's a contraint error
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    throw new ConstraintException(ex.Message, ex);
                }
                else
                {
                    // Re-throw the exception
                    throw;
                }
            }

            return returnValue;
        }

        public override bool DeleteTDCShipment(int shipmentId)
        {
            bool returnValue = false;

            try
            {
                returnValue = ExecuteNonQuery("DeleteTDCShipment", shipmentId);
            }
            catch (SqlException ex)
            {
                // Is it a foreign key constraint
                if (ex.Number == 547)
                {
                    throw new ForeignKeyConstraint(ex.Message, ex);
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Gets the shipment type data from the db.  This includes 
        /// Route Code, Transaction Type, Transaction Sub Type, 
        /// Stock Warehouse and Delivery Warehouse.
        public override IDataReader GetTDCShipmentTypeData(int Id)
        {
            return ExecuteReader("GetTDCShipmentTypeData", Id);
        }

        public override int GetTDCShipmentStatus(int Id)
        {
            return ExecuteScalar("GetTDCShipmentStatus", ERRORVALUE, Id);
        }

        public override bool UpdateTDCShipmentStatus(TDCShipment shipment, Shipment.StatusEnum newStatus, string updatedBy)
        {
            // Seed to failed to update
            bool returnValue = false;

            try
            {
                returnValue = ExecuteNonQuery("UpdateTDCShipmentStatus", shipment.Id, newStatus, updatedBy);
            }
            catch (SqlException ex)
            {
                // Is it a foreign key constraint
                if (ex.Number == 547)
                {
                    throw new ForeignKeyConstraint(ex.Message, ex);
                }

            }

            // Did we update
            return returnValue;
        }

        #endregion

        #region TDC Shipment Line Methods

        public override IDataReader GetTDCShipmentLinesByRoutingId(int routingHistoryId)
        {
            return ExecuteReader("GetTDCShipmentLinesByRoutingId", routingHistoryId);
        }

        public override bool DeleteTDCShipmentLine(int shipmentLineId)
        {
            throw new NotImplementedException();
        }

        public override int SaveTDCShipmentLine(TDCShipmentLine shipmentLine)
        {
            int returnValue;

            try
            {
                returnValue = ExecuteScalar("SaveTDCShipmentLine", ERRORVALUE,
                                                         shipmentLine.Id,
                                                         shipmentLine.ShipmentId,
                                                         Null.GetNull(shipmentLine.ConversionInstructions, DBNull.Value),
                                                         Null.GetNull(shipmentLine.ConversionQuantity, DBNull.Value),
                                                         Null.GetNull(shipmentLine.CustomerReference, DBNull.Value),
                                                         shipmentLine.Description1,
                                                         Null.GetNull(shipmentLine.Description2, DBNull.Value),
                                                         shipmentLine.Exceptions,
                                                         Null.GetNull(shipmentLine.Grammage, DBNull.Value),
                                                         shipmentLine.IsISO9000Approved,
                                                         shipmentLine.IsPanel,
                                                         Null.GetNull(shipmentLine.Length, DBNull.Value),
                                                         shipmentLine.LineNumber,
                                                         Null.GetNull(shipmentLine.LoadCategoryCode, DBNull.Value),
                                                         Null.GetNull(shipmentLine.Microns, DBNull.Value),
                                                         Null.GetNull(shipmentLine.Packing, DBNull.Value),
                                                         shipmentLine.ProductCode,
                                                         Null.GetNull(shipmentLine.ProductGroup, DBNull.Value),
                                                         shipmentLine.Quantity,
                                                         Null.GetNull(shipmentLine.OriginalQuantity, DBNull.Value),
                                                         shipmentLine.QuantityUnit,
                                                         shipmentLine.Volume,
                                                         Null.GetNull(shipmentLine.Width, DBNull.Value),
                                                         shipmentLine.NetWeight,
                                                         shipmentLine.UpdatedBy,
                                                         shipmentLine.CheckSum
                                                     );
            }
            catch (SqlException ex)
            {
                // See if it's a contraint error
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    throw new ConstraintException(ex.Message, ex);
                }
                else
                {
                    // Re-throw the exception
                    throw;
                }
            }

            return returnValue;
        }

        public override IDataReader GetTDCShipmentLines(int shipmentId, string sortExpression)
        {
            return ExecuteReader("GetTDCShipmentLines", shipmentId, sortExpression);
        }

        public override IDataReader GetTDCShipmentLine(int ShipmentLineId)
        {
            return ExecuteReader("GetTDCShipmentLine", ShipmentLineId);
        }

        public override IDataReader GetTDCShipmentLine(int lineCode, string opcoCode, string shipmentNumber, string despatchNumber)
        {
            return ExecuteReader("GetTDCShipmentLineByLineCode", lineCode, opcoCode, shipmentNumber, despatchNumber);
        }

        #endregion

        #region OpCo Shipment Line Methods

        public override bool DeleteOpCoShipmentLine(int shipmentLineId)
        {
            bool returnValue;
            try
            {
                returnValue = ExecuteNonQuery("DeleteOpCoShipmentLine", shipmentLineId);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    throw new ForeignKeyConstraint(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }

            return returnValue;
        }

        public override int SaveOpCoShipmentLine(ShipmentLine shipmentLine)
        {
            int returnValue;

            try
            {
                returnValue = ExecuteScalar("SaveOpCoShipmentLine", ERRORVALUE,
                                                         shipmentLine.Id,
                                                         shipmentLine.ShipmentId,
                                                         Null.GetNull(shipmentLine.ConversionInstructions, DBNull.Value),
                                                         Null.GetNull(shipmentLine.ConversionQuantity, DBNull.Value),
                                                         Null.GetNull(shipmentLine.CustomerReference, DBNull.Value),
                                                         shipmentLine.Description1,
                                                         Null.GetNull(shipmentLine.Description2, DBNull.Value),
                                                         shipmentLine.Exceptions,
                                                         Null.GetNull(shipmentLine.Grammage, DBNull.Value),
                                                         shipmentLine.IsISO9000Approved,
                                                         shipmentLine.IsPanel,
                                                         Null.GetNull(shipmentLine.Length, DBNull.Value),
                                                         shipmentLine.LineNumber,
                                                         Null.GetNull(shipmentLine.LoadCategoryCode, DBNull.Value),
                                                         Null.GetNull(shipmentLine.Microns, DBNull.Value),
                                                         Null.GetNull(shipmentLine.Packing, DBNull.Value),
                                                         shipmentLine.ProductCode,
                                                         Null.GetNull(shipmentLine.ProductGroup, DBNull.Value),
                                                         shipmentLine.Quantity,
                                                         shipmentLine.QuantityUnit,
                                                         shipmentLine.Volume,
                                                         Null.GetNull(shipmentLine.Width, DBNull.Value),
                                                         shipmentLine.NetWeight,
                                                         shipmentLine.UpdatedBy,
                                                         shipmentLine.CheckSum
                                                     );
            }
            catch (SqlException ex)
            {
                // See if it's a contraint error
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    throw new ConstraintException(ex.Message, ex);
                }
                else
                {
                    // Re-throw the exception
                    throw;
                }
            }

            return returnValue;
        }

        public override IDataReader GetOpCoShipmentLines(int shipmentId, string sortExpression)
        {
            return ExecuteReader("GetOpCoShipmentLines", shipmentId, sortExpression);
        }

        #endregion

        #region Mapping Methods

        public override IDataReader GetMappingPropertyAssociations()
        {
            return ExecuteReader("GetMappingPropertyAssociations");
        }

        public override IDataReader GetMappingClassAssociation(int id)
        {
            return ExecuteReader("GetMappingClassAssociation", id);
        }

        /// <summary>
        /// Get a mapping system for the primary key.
        /// </summary>
        /// <param name="mappingSystemId">The mappingSystemId.</param>
        /// <returns></returns>
        public override IDataReader GetMappingSystem(int mappingSystemId)
        {
            return ExecuteReader("GetMappingSystem", mappingSystemId);
        }

        /// <summary>
        /// Gets the mapping lookup.
        /// </summary>
        /// <param name="mappingPropertyAssociationId"></param>
        /// <returns></returns>
        public override IDataReader GetMappingLookup(int mappingPropertyAssociationId)
        {
            return ExecuteReader("GetMappingLookup", mappingPropertyAssociationId);
        }

        /// <summary>
        /// Retrieves a list of all Mapping from the underlying data store via the configured DataProvider.
        /// A strongly typed list of Mapping entities is returned to the caller or an empty list if no records were found.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <returns></returns>
        public override IDataReader GetMappings(MappingSearchParams mappingSearchParams, int startRowIndex, int maximumRows, out int totalRows)
        {

            totalRows = ExecuteScalar("GetMappingsCount", ERRORVALUE,
                    string.IsNullOrEmpty(mappingSearchParams.SourceSystem) ? DBNull.Value : (object)mappingSearchParams.SourceSystem,
                    string.IsNullOrEmpty(mappingSearchParams.DestinationSystem) ? DBNull.Value : (object)mappingSearchParams.DestinationSystem,
                    string.IsNullOrEmpty(mappingSearchParams.SourceType) ? DBNull.Value : (object)mappingSearchParams.SourceType,
                    string.IsNullOrEmpty(mappingSearchParams.DestinationType) ? DBNull.Value : (object)mappingSearchParams.DestinationType,
                    string.IsNullOrEmpty(mappingSearchParams.SourceProperty) ? DBNull.Value : (object)mappingSearchParams.SourceProperty,
                    string.IsNullOrEmpty(mappingSearchParams.DestinationProperty) ? DBNull.Value : (object)mappingSearchParams.DestinationProperty,
                    string.IsNullOrEmpty(mappingSearchParams.FromValue) ? DBNull.Value : (object)mappingSearchParams.FromValue,
                    string.IsNullOrEmpty(mappingSearchParams.ToValue) ? DBNull.Value : (object)mappingSearchParams.ToValue
                );

            return ExecuteReader("GetMappings",
                       string.IsNullOrEmpty(mappingSearchParams.SourceSystem) ? DBNull.Value : (object)mappingSearchParams.SourceSystem,
                       string.IsNullOrEmpty(mappingSearchParams.DestinationSystem) ? DBNull.Value : (object)mappingSearchParams.DestinationSystem,
                       string.IsNullOrEmpty(mappingSearchParams.SourceType) ? DBNull.Value : (object)mappingSearchParams.SourceType,
                       string.IsNullOrEmpty(mappingSearchParams.DestinationType) ? DBNull.Value : (object)mappingSearchParams.DestinationType,
                       string.IsNullOrEmpty(mappingSearchParams.SourceProperty) ? DBNull.Value : (object)mappingSearchParams.SourceProperty,
                       string.IsNullOrEmpty(mappingSearchParams.DestinationProperty) ? DBNull.Value : (object)mappingSearchParams.DestinationProperty,
                       string.IsNullOrEmpty(mappingSearchParams.FromValue) ? DBNull.Value : (object)mappingSearchParams.FromValue,
                       string.IsNullOrEmpty(mappingSearchParams.ToValue) ? DBNull.Value : (object)mappingSearchParams.ToValue,
                       startRowIndex,
                       maximumRows
                   );
        }


        /// <summary>
        /// Retrieves a list of all Mapping from the underlying data store via the configured DataProvider.
        /// A strongly typed list of Mapping entities is returned to the caller or an empty list if no records were found.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <returns></returns>
        public override IDataReader GetMappings()
        {
            return ExecuteReader("GetMappings",
                                      DBNull.Value,
                                      DBNull.Value,
                                      DBNull.Value,
                                      DBNull.Value,
                                      DBNull.Value,
                                      DBNull.Value,
                                      DBNull.Value,
                                      DBNull.Value,
                                      DBNull.Value,
                                      DBNull.Value
                                 );
        }

        /// <summary>
        /// Gets the mapping by type and system.
        /// </summary>
        /// <param name="sourceMappingType">The type fullname of the source mapping.</param>
        /// <param name="destinationMappingType">The type fullname of the destination mapping.</param>
        /// <param name="sourceSystem">The source system.</param>
        /// <param name="destinationSystem">The destination system.</param>
        /// <returns></returns>
        public override IDataReader GetMappingByTypeAndSystem(string sourceMappingType, string destinationMappingType,
                                                              string sourceSystem, string destinationSystem)
        {
            return ExecuteReader("GetMappingByTypeAndSystem",
                                   sourceMappingType,
                                   destinationMappingType,
                                   sourceSystem,
                                   destinationSystem
                                );
        }

        /// <summary>
        /// Saves the specified Mapping to the underlying data store via the configured DataProvider.
        /// If the primary key (ID) of the supplied Mapping is Null, a new row is added to the underlying data store and a new primary key (ID) automatically generated.  If the primary key (ID) of the supplied Mapping has been specified, the existing row in the data store is updated if it has not already been altered.
        /// The primary key (ID) of the created or altered Mapping is returned to the caller.  If an error occurs, an exception is thrown and no updates made to the data store.
        /// For concurrency errors (the data has changed in-between load and save by an external system or user), a concurrency exception is thrown.
        /// </summary>
        /// <param name="mapping">The mapping.</param>
        /// <returns></returns>
        public override int SaveMapping(BusinessObjects.Mapping mapping)
        {
            int returnValue;
            try
            {
                returnValue = ExecuteScalar("SaveMapping", ERRORVALUE,
                                                 mapping.Id,
                                                 mapping.MappingPropertyAssociationId,
                                                 mapping.DestinationSystemId,
                                                 mapping.DestinationValue,
                                                 mapping.SourceSystemId,
                                                 mapping.SourceValue,
                                                 mapping.UpdatedBy,
                                                 mapping.CheckSum
                    );
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    throw new ConstraintException(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// Deletes the specified Mapping record from the underlying data store via the configured DataProvider.
        /// If the specified Mapping is deleted true is returned otherwise false.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <param name="mappingId">The mapping id.</param>
        /// <returns></returns>
        public override bool DeleteMapping(int mappingId)
        {
            bool returnValue;
            try
            {
                returnValue = ExecuteNonQuery("DeleteMapping", mappingId);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    throw new ForeignKeyConstraint(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Retrieves Mapping entries from the underlying data store via the configured DataProvider for the supplied
        /// source type and destination type.
        /// A List of  Mappings is returned to the caller or an empty list if no Mapping records are found.
        /// If an error occurs, an exception is thrown.
        /// </summary>
        /// <param name="sourceMappingType"></param>
        /// <param name="destinationMappingType"></param>
        /// <returns></returns>
        public override IDataReader GetMappingClassAssociationByTypes(string sourceMappingType, string destinationMappingType)
        {
            return ExecuteReader("GetMappingClassAssociationByTypes", sourceMappingType, destinationMappingType);
        }

        /// <summary>
        /// Retrieves a single Mapping entry from the underlying data store via the configured DataProvider for the supplied
        /// source type, destination type, source system, destination system, source property and source property value.
        /// An instance of a Mapping is returned to the caller or Null if no Mapping record is found.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <param name="sourceMappingType"></param>
        /// <param name="destinationMappingType"></param>
        /// <param name="sourceSystem"></param>
        /// <param name="destinationSystem"></param>
        /// <param name="sourcePropertyName"></param>
        /// <param name="destinationPropertyName"></param>
        /// <param name="sourcePropertyValue"></param>
        /// <returns></returns>
        public override IDataReader GetMapping(
            string sourceMappingType,
            string destinationMappingType,
            string sourceSystem,
            string destinationSystem,
            string sourcePropertyName,
            string destinationPropertyName,
            string sourcePropertyValue)
        {
            return ExecuteReader("GetMappingBySourceValue",
                                   sourceMappingType,
                                   destinationMappingType,
                                   sourceSystem,
                                   destinationSystem,
                                   sourcePropertyName,
                                   destinationPropertyName,
                                   sourcePropertyValue
                );
        }

        /// <summary>
        /// Retrieves a single Mapping entry from the underlying data store via the configured DataProvider for the supplied Mapping ID.
        /// An instance of a Mapping is returned to the caller or Null if no Mapping record is found.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <param name="mappingId">The mapping id.</param>
        /// <returns></returns>
        public override IDataReader GetMapping(int mappingId)
        {
            return ExecuteReader("GetMapping", mappingId);
        }

        /// <summary>
        /// Gets the mapping property association.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public override IDataReader GetMappingPropertyAssociation(int id)
        {
            return ExecuteReader("GetMappingPropertyAssociation", id);
        }

        /// <summary>
        /// Gets the mapping property association by class association id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public override IDataReader GetMappingPropertyAssociationByClassAssociationId(int id)
        {
            return ExecuteReader("GetMappingPropertyAssociationsByClassAssociationId", id);
        }

        /// <summary>
        /// Deletes all mapping class associations.
        /// </summary>
        /// <returns></returns>
        public override bool DeleteAllMappingClassAssociations()
        {
            bool returnValue;
            try
            {
                returnValue = ExecuteNonQuery("DeleteAllMappingClassAssociations");
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    throw new ForeignKeyConstraint(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Deletes all mapping property associations.
        /// </summary>
        /// <returns></returns>
        public override bool DeleteAllMappingPropertyAssociations()
        {
            bool returnValue;
            try
            {
                returnValue = ExecuteNonQuery("DeleteAllMappingPropertyAssociations");
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    throw new ForeignKeyConstraint(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Deletes all mapping systems.
        /// </summary>
        public override bool DeleteAllMappingSystems()
        {
            bool returnValue;
            try
            {
                returnValue = ExecuteNonQuery("DeleteAllMappingSystem");
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    throw new ForeignKeyConstraint(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Deletes all mappings.
        /// </summary>
        /// <returns></returns>
        public override bool DeleteAllMappings()
        {
            bool returnValue;
            try
            {
                returnValue = ExecuteNonQuery("DeleteAllMappings");
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    throw new ForeignKeyConstraint(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Gets the mapping class association.
        /// </summary>
        /// <param name="sourceType">Type of the source.</param>
        /// <returns></returns>
        public override IDataReader GetMappingClassAssociations(string sourceType)
        {
            return ExecuteReader("GetMappingClassAssociationsBySourceType", sourceType);
        }


        public override IDataReader GetMappingPropertyAssociations(string sourceProperty, int classAssociationId)
        {
            return ExecuteReader("GetMappingPropertyAssociationsBySourceProperty", sourceProperty, classAssociationId);
        }

        /// <summary>
        /// Gets the mapping systems.
        /// </summary>
        /// <returns></returns>
        public override IDataReader GetMappingSystems()
        {
            return ExecuteReader("GetMappingSystems");
        }

        public override IDataReader GetMappingClassAssociations()
        {
            return ExecuteReader("GetMappingClassAssociations");
        }

        /// <summary>
        /// Saves the mapping class association.
        /// </summary>
        /// <param name="association">The association.</param>
        /// <returns></returns>
        public override int SaveMappingClassAssociation(MappingClassAssociation association)
        {
            int returnValue;
            try
            {
                returnValue = ExecuteScalar("SaveMappingClassAssociation", ERRORVALUE,
                                                         association.Id,
                                                         association.SourceType,
                                                         association.SourceTypeFullName,
                                                         association.DestinationType,
                                                         association.DestinationTypeFullName,
                                                         association.CheckSum
                                                     );
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    throw new ConstraintException(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// Saves the mapping system.
        /// </summary>
        /// <param name="system">The system.</param>
        /// <returns></returns>
        public override int SaveMappingSystem(MappingSystem system)
        {
            int returnValue;
            try
            {
                returnValue = ExecuteScalar("SaveMappingSystem", ERRORVALUE,
                                                         system.Id,
                                                         system.Name,
                                                         system.IsSource,
                                                         system.IsDestination,
                                                         system.CheckSum
                                                     );
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    throw new ConstraintException(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }
            return returnValue;
        }

        /// Saves the mapping property association.
        /// </summary>
        /// <param name="association">The association.</param>
        /// <returns></returns>
        public override int SaveMappingPropertyAssociation(MappingPropertyAssociation association)
        {
            int returnValue;
            try
            {
                returnValue = ExecuteScalar("SaveMappingPropertyAssociation", ERRORVALUE,
                                                         association.Id,
                                                         association.MappingClassAssociationId,
                                                         association.SourceProperty,
                                                         association.DestinationProperty,
                                                         association.LookupTableName,
                                                         association.LookUpTableDisplayColumn,
                                                         association.CheckSum
                                                     );
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    throw new ConstraintException(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }
            return returnValue;
        }

        #endregion

        #region Non Working Day Methods

        public override bool DeleteNonWorkingDay(int id)
        {
            bool returnValue;
            try
            {
                returnValue = ExecuteNonQuery("DeleteNonWorkingDay", id);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    throw new ForeignKeyConstraint(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }

            return returnValue;
        }

        public override IDataReader GetNonWorkingDay(int nonWorkingDayId)
        {
            return ExecuteReader("GetNonWorkingDay", nonWorkingDayId);
        }

        public override IDataReader GetNonWorkingDay(String warehouseCode, DateTime nonWorkingDate)
        {
            return ExecuteReader("GetNonWorkingDayLinkedToWarehouse", warehouseCode, nonWorkingDate);
        }

        public override IDataReader GetNonWorkingDays()
        {
            return ExecuteReader("GetNonWorkingDays");
        }


        public override IDataReader GetNonWorkingDays(DateTime dateFrom, DateTime dateTo,
                                                      int warehouseId,
                                                      int regionId, string sortExpression, int startRowIndex,
                                                      int maximumRows, out int totalRows)
        {
            totalRows = ExecuteScalar("GetNonWorkingDaysCount", ERRORVALUE,
                                           dateFrom,
                                           dateTo,
                                           Null.GetNull(warehouseId, DBNull.Value),
                                           Null.GetNull(regionId, DBNull.Value)
                                       );

            return ExecuteReader("GetNonWorkingDays",
                                           dateFrom,
                                           dateTo,
                                           Null.GetNull(warehouseId, DBNull.Value),
                                           Null.GetNull(regionId, DBNull.Value),
                                           sortExpression,
                                           startRowIndex,
                                           maximumRows
                                       );
        }

        public override IDataReader GetNonWorkingDaysByRegion(DateTime dateFrom, DateTime dateTo, int regionId)
        {
            return ExecuteReader("GetNonWorkingDaysByRegion", dateFrom, dateTo, regionId);
        }

        //public override IDataReader GetNonWorkingDaysByWarehouse(DateTime dateFrom, string warehouse)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        public override IDataReader GetNonWorkingDaysByWarehouse(DateTime dateFrom, DateTime dateTo, int warehouseId)
        {
            return ExecuteReader("GetNonWorkingDaysByWarehouse", dateFrom, dateTo, warehouseId);
        }

        //public override IDataReader GetNonWorkingDays(DateTime dateFrom)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}


        //public override IDataReader GetNonWorkingDaysByRegion(DateTime dateFrom, string region)
        //{
        //    IDataReader reader = null;
        //    try
        //    {
        //        reader = ExecuteReader("GetNonWorkingDaysByRegion",
        //                               new object[]
        //                                   {
        //                                       dateFrom,
        //                                       region
        //                                   });
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, "DataProvider CategoryName")) throw;
        //    }
        //    return reader;
        //}


        public override int SaveNonWorkingDay(int id, DateTime nonWorkingDate, string description, int warehouseId,
                                              string updatedBy, int checkSum)
        {
            int returnValue;
            try
            {
                returnValue = ExecuteScalar("SaveNonWorkingDay", ERRORVALUE,
                                                 id,
                                                 nonWorkingDate,
                                                 description,
                                                 warehouseId,
                                                 updatedBy,
                                                 checkSum
                                            );
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    throw new ConstraintException(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }
            return returnValue;
        }

        #endregion

        #region Trunker Day Methods

        public override bool DeleteTrunkerDay(int trunkerDayId)
        {
            bool returnValue;
            try
            {
                returnValue = ExecuteNonQuery("DeleteTrunkerDay", trunkerDayId);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    throw new ForeignKeyConstraint(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }

            return returnValue;
        }

        public override IDataReader GetTrunkerDay(int trunkerId)
        {
            return ExecuteReader("GetTrunkerDay", trunkerId);
        }

        public override IDataReader GetTrunkerDays()
        {
            return ExecuteReader("GetTrunkerDays");
        }

        public override int SaveTrunkerDay(TrunkerDay trunkerDay)
        {
            int returnValue;
            try
            {
                returnValue = ExecuteScalar("SaveTrunkerDay", ERRORVALUE,
                                             trunkerDay.Id,
                                             trunkerDay.SourceWarehouseId,
                                             trunkerDay.DestinationWarehouseId,
                                             trunkerDay.Days,
                                             trunkerDay.UpdatedBy,
                                             trunkerDay.CheckSum
                                         );
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    throw new ConstraintException(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }
            return returnValue;
        }

        public override int GetNumberOfTrunkerDays(Warehouse sourceWarehouse, Warehouse destinationWarehouse)
        {
            return ExecuteScalar("GetNumberOfTrunkerDays", ERRORVALUE, sourceWarehouse.Id, destinationWarehouse.Id);
        }

        #endregion

        #region OpCo Methods

        /// <summary>
        /// Gets the all OpCos.
        /// </summary>
        /// <returns>Collection of OpCo Objects</returns>
        public override IDataReader GetOpCos()
        {
            return ExecuteReader("GetOpCos");
        }

        /// <summary>
        /// Gets a single OpCo for the specified Id.
        /// </summary>
        /// <param name="opcoId">The opco id.</param>
        /// <returns>An OpCo object</returns>
        public override IDataReader GetOpCo(int opcoId)
        {
            return ExecuteReader("GetOpCo", opcoId);
        }

        /// <summary>
        /// Gets a single OpCo for the specified Id.
        /// </summary>
        /// <param name="opcoCode">The opco id.</param>
        /// <returns>An OpCo object</returns>
        public override IDataReader GetOpCo(string opcoCode)
        {
            return ExecuteReader("GetOpCoByCode", opcoCode);
        }

        /// <summary>
        /// Deletes a OpCo.
        /// </summary>
        /// <param name="opcoId">The opco id.</param>
        /// <returns>Success state</returns>
        public override bool DeleteOpCo(int opcoId)
        {
            bool returnValue;
            try
            {
                returnValue = ExecuteNonQuery("DeleteOpCo", opcoId);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    throw new ForeignKeyConstraint(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Saves an OpCo object.
        /// </summary>
        /// <param name="opco">The opco.</param>
        /// <returns></returns>
        /// <remarks>If the Id property =0 then an INSERT will Occur, otherwise an UPDATE will occur</remarks>
        public override int SaveOpCo(OpCo opco)
        {
            int returnValue;
            try
            {
                returnValue = ExecuteScalar("SaveOpCo", ERRORVALUE,
                                                 opco.Id,
                                                 opco.Code,
                                                 opco.Description,
                                                 opco.UpdatedBy,
                                                 opco.CheckSum
                                             );
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    throw new ConstraintException(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }
            return returnValue;
        }

        #endregion

        #region SalesLocation

        /// <summary>
        /// Retrieves a list of all Locations from the underlying data store via the configured DataProvider.
        /// A strongly typed list of SalesLocation entities is returned to the caller or an empty list if no records were found.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <returns></returns>
        public override IDataReader GetLocations()
        {
            return ExecuteReader("GetSalesLocations");
        }

        /// <summary>
        /// Retrieves a single SalesLocation entry from the underlying data store via the configured DataProvider for the supplied SalesLocation ID.
        /// An instance of a SalesLocation is returned to the caller or Null if no SalesLocation record is found.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <param name="locationId">The location id.</param>
        /// <returns></returns>
        public override IDataReader GetLocation(int locationId)
        {
            return ExecuteReader("GetSalesLocation", locationId);
        }

        /// <summary>
        /// Retrieves a single SalesLocation entry from the underlying data store via the configured DataProvider for the supplied SalesLocation ID.
        /// An instance of a SalesLocation is returned to the caller or Null if no SalesLocation record is found.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <param name="locationCode">The sales location code.</param>
        /// <returns></returns>
        public override IDataReader GetLocation(string locationCode)
        {
            return ExecuteReader("GetSalesLocationByCode", locationCode);
        }

        /// <summary>
        /// Deletes the specified SalesLocation record from the underlying data store via the configured DataProvider.
        /// If the specified SalesLocation is deleted true is returned otherwise false.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <param name="locationId">The location id.</param>
        /// <returns></returns>
        public override bool DeleteLocation(int locationId)
        {
            bool returnValue;
            try
            {
                returnValue = ExecuteNonQuery("DeleteSalesLocation", locationId);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    throw new ForeignKeyConstraint(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Saves the specified SalesLocation to the underlying data store via the configured DataProvider.
        /// If the primary key (ID) of the supplied SalesLocation is Null, a new row is added to the underlying data store and a new primary key (ID) automatically generated.  If the primary key (ID) of the supplied SalesLocation has been specified, the existing row in the data store is updated if it has not already been altered.
        /// The primary key (ID) of the created or altered SalesLocation is returned to the caller.  If an error occurs, an exception is thrown and no updates made to the data store.
        /// For concurrency errors (the data has changed in-between load and save by an external system or user), a concurrency exception is thrown.
        /// </summary>
        /// <param name="salesLocation">The salesLocation.</param>
        /// <returns></returns>
        public override int SaveLocation(SalesLocation salesLocation)
        {
            int returnValue;
            try
            {
                returnValue = ExecuteScalar("SaveSalesLocation", ERRORVALUE,
                                                         salesLocation.Id,
                                                         salesLocation.Location,
                                                         salesLocation.TelephoneNumber,
                                                         salesLocation.Description,
                                                         salesLocation.OpCoId,
                                                         salesLocation.UpdatedBy,
                                                         salesLocation.CheckSum
                                                     );
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    throw new ConstraintException(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }
            return returnValue;
        }

        public override IDataReader GetOpCoSalesLocations(string opCoCode)
        {
            return ExecuteReader("GetOpCoSalesLocations", Null.GetNull(opCoCode, DBNull.Value));
        }

        public override IDataReader GetTDCSalesLocations(string opCoCode)
        {
            return ExecuteReader("GetTDCSalesLocations", Null.GetNull(opCoCode, DBNull.Value));
        }

        #endregion

        #region OptrakRegion

        public override IDataReader GetRegions()
        {
            return ExecuteReader("GetRegions");
        }

        public override IDataReader GetRegion(int regionId)
        {
            return ExecuteReader("GetRegion", regionId);
        }

        public override IDataReader GetRegion(string regionCode)
        {
            return ExecuteReader("GetRegionByCode", regionCode);
        }

        public override bool DeleteRegion(int regionId)
        {
            bool returnValue;
            try
            {
                returnValue = ExecuteNonQuery("DeleteRegion", regionId);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    throw new ForeignKeyConstraint(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }

            return returnValue;
        }

        public override int SaveRegion(OptrakRegion optrakRegion)
        {
            int returnValue;
            try
            {
                returnValue = ExecuteScalar("SaveRegion", ERRORVALUE,
                                             optrakRegion.Id,
                                             optrakRegion.Code,
                                             optrakRegion.Description,
                                             optrakRegion.UpdatedBy,
                                             optrakRegion.CheckSum
                                         );
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    throw new ConstraintException(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }
            return returnValue;
        }

        #endregion

        #region Transaction Type

        /// <summary>
        /// Saves the type of the transaction.
        /// </summary>
        /// <param name="transactionType">The type.</param>
        /// <returns></returns>
        public override int SaveTransactionType(TransactionType transactionType)
        {
            int returnValue;
            try
            {
                returnValue = ExecuteScalar("SaveTransactionType", ERRORVALUE,
                                                         transactionType.Id,
                                                         transactionType.Code,
                                                         transactionType.Description,
                                                         transactionType.IsStock,
                                                         transactionType.IsNonStock,
                                                         transactionType.IsCollection,
                                                         transactionType.IsSample,
                                                         transactionType.UpdatedBy,
                                                         transactionType.CheckSum
                                                     );
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    throw new ConstraintException(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// Deletes the type of the transaction.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public override bool DeleteTransactionType(int id)
        {
            bool returnValue;
            try
            {
                returnValue = ExecuteNonQuery("DeleteTransactionType", id);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    throw new ForeignKeyConstraint(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Gets the transaction type by code.
        /// </summary>
        /// <param name="code">The transaction type code.</param>
        /// <returns></returns>
        public override IDataReader GetTransactionType(string code)
        {
            return ExecuteReader("GetTransactionTypeByCode", code);
        }

        /// <summary>
        /// Gets the type of the transaction.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public override IDataReader GetTransactionType(int id)
        {
            return ExecuteReader("GetTransactionType", id);
        }

        /// <summary>
        /// Gets all opco transaction types.
        /// </summary>
        /// <returns></returns>
        public override IDataReader GetOpCoTransactionTypes()
        {
            return ExecuteReader("GetOpCoTransactionTypes");
        }

        /// <summary>
        /// Gets all transaction types.
        /// </summary>
        /// <returns></returns>
        public override IDataReader GetTransactionTypes()
        {
            return ExecuteReader("GetTransactionTypes");
        }

        #endregion

        #region Transaction Sub Type

        /// <summary>
        /// Saves the type of the transaction sub.
        /// </summary>
        /// <param name="transactionSubType">The type.</param>
        /// <returns></returns>
        public override int SaveTransactionSubType(TransactionSubType transactionSubType)
        {
            int returnValue;
            try
            {
                returnValue = ExecuteScalar("SaveTransactionSubType", ERRORVALUE,
                                                 transactionSubType.Id,
                                                 transactionSubType.Code,
                                                 transactionSubType.Description,
                                                 transactionSubType.IsNormal,
                                                 transactionSubType.IsTransfer,
                                                 transactionSubType.IsLocalConversion,
                                                 transactionSubType.Is3rdPartyConversion,
                                                 transactionSubType.UpdatedBy,
                                                 transactionSubType.CheckSum
                                             );
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    throw new ConstraintException(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// Deletes the type of the transaction sub.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public override bool DeleteTransactionSubType(int id)
        {
            bool returnValue;
            try
            {
                returnValue = ExecuteNonQuery("DeleteTransactionSubType", id);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    throw new ForeignKeyConstraint(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Gets the type of the transaction sub.
        /// </summary>
        /// <param name="transactionSubTypeCode"></param>
        /// <returns></returns>
        public override IDataReader GetTransactionSubTypeByCode(string transactionSubTypeCode)
        {
            return ExecuteReader("GetTransactionSubTypeByCode", transactionSubTypeCode);
        }

        /// <summary>
        /// Gets the type of the transaction sub.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public override IDataReader GetTransactionSubType(int id)
        {
            return ExecuteReader("GetTransactionSubType", id);
        }

        /// <summary>
        /// Gets the transaction sub types.
        /// </summary>
        /// <returns></returns>
        public override IDataReader GetTransactionSubTypes()
        {
            return ExecuteReader("GetTransactionSubTypes");
        }

        #endregion

        #region Route

        /// <summary>
        /// Deletes the route.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public override bool DeleteRoute(int id)
        {
            bool returnValue;
            try
            {
                returnValue = ExecuteNonQuery("DeleteRoute", id);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    throw new ForeignKeyConstraint(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Gets the route.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public override IDataReader GetRoute(int id)
        {
            return ExecuteReader("GetRoute", id);
        }

        public override IDataReader GetRoute(string warehouseCode, string routeCode)
        {
            return ExecuteReader("GetRouteByWarehouse", warehouseCode, routeCode);
        }

        /// <summary>
        /// Gets the route.
        /// </summary>
        /// <param name="routeCode">The route code.</param>
        /// <returns></returns>
        public override IDataReader GetRoute(string routeCode)
        {
            return ExecuteReader("GetRouteByCode", routeCode);
        }

        /// <summary>
        /// Gets the opco routes.
        /// </summary>
        /// <returns></returns>
        public override IDataReader GetOpCoRoutes()
        {
            return ExecuteReader("GetOpCoRoutes");
        }

        /// <summary>
        /// Gets the routes.
        /// </summary>
        /// <returns></returns>
        public override IDataReader GetRoutes(string sortExpression, int startRowIndex,
                                              int maximumRows, out int totalRows)
        {
            return GetRoutes(Null.NullInteger,sortExpression,startRowIndex,maximumRows,out totalRows);
        }

        public override IDataReader GetRoutes(int warehouseId, string sortExpression, int startRowIndex,
                                              int maximumRows, out int totalRows)
        {
            IDataReader reader;
            reader = ExecuteReader("GetRoutes", Null.GetNull(warehouseId,DBNull.Value), sortExpression, startRowIndex, maximumRows);
            totalRows = ExecuteScalar("GetRoutesCount", ERRORVALUE,Null.GetNull(warehouseId, DBNull.Value));
            return reader;
        }

        /// <summary>
        /// Saves the route.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns></returns>
        public override int SaveRoute(Route route)
        {
            int returnValue;
            try
            {
                returnValue = ExecuteScalar("SaveRoute", ERRORVALUE,
                                             route.Id,
                                             route.Code,
                                             route.Description,
                                             route.WarehouseId,
                                             route.IsSameDay,
                                             route.IsNextDay,
                                             route.IsCollection,
                                             route.IsSpecial,
                                             route.UpdatedBy,
                                             route.CheckSum
                                         );
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    throw new ConstraintException(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }
            return returnValue;
        }

        public override IDataReader GetTDCRoutesByWarehouseCode(string warehouseCode)
        {
            return ExecuteReader("GetTDCRoutesByWarehouseCode", Null.GetNull(warehouseCode, DBNull.Value));
        }

        #endregion

        #region Printer

        //public override int SavePrinter(Printer printer)
        //{
        //    int returnValue = ERRORVALUE;
        //    try
        //    {
        //        returnValue = ExecuteScalar<int>("SavePrinter", ERRORVALUE,
        //                                         new object[]
        //                                             {
        //                                                 printer.Id,
        //                                                 printer.Name,
        //                                                 printer.UpdatedBy,
        //                                                 printer.CheckSum
        //                                             });
        //    }
        //    catch (SqlException ex)
        //    {
        //        if (ex.Number == 2627 || ex.Number == 2627)
        //        {
        //            if (ExceptionPolicy.HandleException(new ConstraintException(ex.Message, ex), "DataProvider CategoryName"))
        //                throw;
        //        }
        //        else
        //        {
        //            if (ExceptionPolicy.HandleException(ex, "DataProvider CategoryName")) throw;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, "DataProvider CategoryName")) throw;
        //    }

        //    return returnValue;
        //}

        public override bool DeletePrinter(int printerId)
        {
            bool returnValue;
            try
            {
                returnValue = ExecuteNonQuery("DeletePrinter", printerId);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    throw new ForeignKeyConstraint(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }

            return returnValue;
        }

        public override IDataReader GetPrinter(int printerId)
        {
            return ExecuteReader("GetPrinter", printerId);
        }

        public override IDataReader GetPrinters()
        {
            return ExecuteReader("GetPrinters");
        }

        #endregion

        #region Warehouse

        /// <summary>
        /// Saves the warehouse.
        /// </summary>
        /// <param name="warehouse">The warehouse.</param>
        /// <returns></returns>
        public override int SaveWarehouse(Warehouse warehouse)
        {
            try
            {

                return ExecuteScalar("SaveWarehouse", ERRORVALUE,
                                          warehouse.Id,
                                          warehouse.Code,
                                          warehouse.Description,
                                          warehouse.PrinterName,
                                          warehouse.Contact.Email,
                                          warehouse.HasOptrak,
                                          warehouse.HasCommander,
                                          warehouse.Contact.Name,
                                          warehouse.Contact.TelephoneNumber,
                                          warehouse.Address.Line1,
                                          warehouse.Address.Line2,
                                          warehouse.Address.Line3,
                                          warehouse.Address.Line4,
                                          warehouse.Address.PostCode,
                                          warehouse.IsTDC,
                                          warehouse.RegionId,
                                          warehouse.UpdatedBy,
                                          warehouse.CheckSum
                                      );
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    throw new ConstraintException(ex.Message, ex);
                }
                else if (ex.Number == 547)
                {
                    throw new ForeignKeyConstraint(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Deletes the warehouse.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public override bool DeleteWarehouse(int id)
        {
            bool returnValue;
            try
            {
                returnValue = ExecuteNonQuery("DeleteWarehouse", id);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    throw new ForeignKeyConstraint(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Gets the warehouse.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public override IDataReader GetWarehouse(int id)
        {
            return ExecuteReader("GetWarehouse", id);
        }

        /// <summary>
        /// Gets the warehouse.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public override IDataReader GetWarehouse(string code)
        {
            return ExecuteReader("GetWarehouseByCode", code);
        }

        /// <summary>
        /// Gets the warehouses.
        /// </summary>
        /// <returns></returns>
        public override IDataReader GetWarehouses()
        {
            return ExecuteReader("GetWarehouses");
        }

        /// <summary>
        /// Gets the warehouses.
        /// </summary>
        /// <param name="regionCode">The region code.</param>
        /// <returns></returns>
        public override IDataReader GetWarehouses(string regionCode)
        {
            return ExecuteReader("GetWarehousesByRegionCode", regionCode);
        }

        /// <summary>
        /// Gets the warehouses.
        /// </summary>
        /// <param name="regionId">The region Id.</param>
        /// <returns></returns>
        public override IDataReader GetWarehousesByRegion(int regionId)
        {
            return ExecuteReader("GetWarehousesByRegion", regionId);
        }

        public override IDataReader GetOpCoStockWarehouseCodes(string opCoCode)
        {
            return ExecuteReader("GetOpCoStockWarehouseCodes", Null.GetNull(opCoCode, DBNull.Value));
        }

        public override IDataReader GetOpCoDeliveryWarehouseCodes(string opCoCode)
        {
            return ExecuteReader("GetOpCoDeliveryWarehouseCodes", Null.GetNull(opCoCode, DBNull.Value));
        }

        public override IDataReader GetTDCDeliveryWarehouseCodes(string opCoCode)
        {
            return ExecuteReader("GetTDCDeliveryWarehouseCodes", Null.GetNull(opCoCode, DBNull.Value));
        }

        #endregion

        #region OpCov Division

        /// <summary>
        /// Gets the op co divisions.
        /// </summary>
        /// <returns></returns>
        public override IDataReader GetOpCoDivisions()
        {
            return ExecuteReader("GetOpCoDivisions");
        }

        /// <summary>
        /// Gets the op co divisions by op co.
        /// </summary>
        /// <returns></returns>
        public override IDataReader GetOpCoDivisions(string opCoCode)
        {
            return ExecuteReader("GetOpCoDivisionsByOpCo", opCoCode);
        }

        /// <summary>
        /// Gets the op co division.
        /// </summary>
        /// <param name="divisionCode"></param>
        /// <returns></returns>
        public override IDataReader GetOpCoDivision(string divisionCode)
        {
            return ExecuteReader("GetOpCoDivisionByCode", divisionCode);
        }

        /// <summary>
        /// Gets the op co division.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public override IDataReader GetOpCoDivision(int id)
        {
            return ExecuteReader("GetOpCoDivision", id);
        }

        /// <summary>
        /// Deletes the op co division.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public override bool DeleteOpCoDivision(int id)
        {
            bool returnValue;
            try
            {
                returnValue = ExecuteNonQuery("DeleteOpCoDivision", id);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    throw new ForeignKeyConstraint(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Saves the op co division.
        /// </summary>
        /// <param name="division">The division.</param>
        /// <returns></returns>
        public override int SaveOpCoDivision(OpCoDivision division)
        {
            try
            {
                return ExecuteScalar("SaveOpCoDivision", ERRORVALUE,
                                          division.Id,
                                          division.Code,
                                          division.OpCoId,
                                          division.Logo,
                                          division.LogoURI,
                                          division.UpdatedBy,
                                          division.CheckSum
                                      );
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    throw new ConstraintException(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }
        }

        #endregion

        #region Audit Entry Methods

        public override int SaveAuditEntry(MessageAuditEntry auditEntry)
        {
            int returnValue;
            try
            {
                returnValue = ExecuteScalar("SaveAuditEntry", ERRORVALUE,
                                                 auditEntry.Id,
                                                 auditEntry.SourceSystem,
                                                 auditEntry.DestinationSystem,
                                                 auditEntry.ReceivedDate,
                                                 auditEntry.Message,
                                                 auditEntry.Sequence,
                                                 auditEntry.Type,
                                                 auditEntry.Name,
                                                 auditEntry.Label,
                                                 auditEntry.UpdatedBy,
                                                 auditEntry.CheckSum
                                             );
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    throw new ConstraintException(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }
            return returnValue;
        }

        public override bool DeleteAuditEntry(int auditEntryId)
        {
            throw new NotImplementedException();
        }

        public override IDataReader GetAuditEntry(int auditEntryId)
        {
            return ExecuteReader("GetAuditEntry", auditEntryId);
        }

        public override IDataReader GetAuditEntries()
        {
            throw new NotImplementedException();
        }

        public override IDataReader GetAuditEntries(
                DateTime dateFrom,
                DateTime dateTo,
                string sourceSystem,
                string destinationSystem,
                string type,
                string message,
                string sortExpression,
                int startRowIndex,
                int maximumRows, out int totalRows)
        {

            totalRows = ExecuteScalar("GetAuditEntriesCount", ERRORVALUE,
                                               Null.GetNull(dateFrom, DBNull.Value),
                                               Null.GetNull(dateTo, DBNull.Value),
                                               Null.GetNull(sourceSystem, DBNull.Value),
                                               Null.GetNull(destinationSystem, DBNull.Value),
                                               Null.GetNull(type, DBNull.Value),
                                               Null.GetNull(message, DBNull.Value)
                                           );

            return ExecuteReader("GetAuditEntries",
                                                Null.GetNull(dateFrom, DBNull.Value),
                                                Null.GetNull(dateTo, DBNull.Value),
                                                Null.GetNull(sourceSystem, DBNull.Value),
                                                Null.GetNull(destinationSystem, DBNull.Value),
                                                Null.GetNull(type, DBNull.Value),
                                                Null.GetNull(message, DBNull.Value),
                                                sortExpression,
                                                startRowIndex,
                                                maximumRows
                                       );
        }

        public override IDataReader GetSourceSystemList()
        {
            return ExecuteReader("GetSourceSystemList");
        }

        public override IDataReader GetDestinationSystemList()
        {
            return ExecuteReader("GetDestinationSystemList");
        }

        public override IDataReader GetTypeList()
        {
            return ExecuteReader("GetTypeList");
        }

        #endregion

        #region Sequence Methods

        /// <summary>
        /// Saves the sequence.
        /// </summary>
        /// <param name="sequence">The sequence to save to the db.</param>
        /// <returns></returns>
        public override int SaveSequence(Sequence sequence)
        {

            try
            {
                return ExecuteScalar("SaveSequence", ERRORVALUE,
                                                         sequence.Id,
                                                         sequence.Name,
                                                         sequence.Seed,
                                                         sequence.Increment,
                                                         Null.GetNull(sequence.CurrentValue, DBNull.Value),
                                                         sequence.UpdatedBy,
                                                         sequence.CheckSum
                                                     );
            }
            catch (SqlException ex)
            {
                // Check if it's a constraint exception
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    throw new ConstraintException(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }

        }

        /// <summary>
        /// Deletes the sequence.
        /// </summary>
        /// <param name="sequenceToDelete">The sequence id to delete.</param>
        /// <returns></returns>
        public override bool DeleteSequence(Sequence sequenceToDelete)
        {
            bool returnValue;

            try
            {
                returnValue = ExecuteNonQuery("DeleteSequence", sequenceToDelete.Id);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    throw new ForeignKeyConstraint(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Gets the sequence.
        /// </summary>
        /// <param name="sequenceId">The sequence id to retrieve.</param>
        /// <returns></returns>
        public override IDataReader GetSequence(int sequenceId)
        {
            return ExecuteReader("GetSequence", sequenceId);
        }

        /// <summary>
        /// Gets the next named sequence value.
        /// </summary>
        /// <param name="name">The name of the sequence to retrieve the next available value for.</param>
        /// <returns></returns>
        public override int GetNextSequence(string name)
        {

            return ExecuteScalar("GetNextSequence", ERRORVALUE, name);
        }

        public override IDataReader GetSequences()
        {
            return ExecuteReader("GetSequences");
        }

        #endregion

        #region Commander Methods

        public override int SaveCommanderProduct(CommanderProduct product)
        {
            int returnValue;
            try
            {
                returnValue = ExecuteScalar("SaveCommanderProduct", ERRORVALUE,
                                                         product.Id,
                                                         product.ProductCode,
                                                         product.Description,
                                                         product.ShortDescription,
                                                         product.Account,
                                                         product.UOM,
                                                         product.UpdatedBy
                                                     );
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    throw new ConstraintException(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// Gets the commander product.
        /// </summary>
        /// <param name="productCode">The product code.</param>
        /// <returns></returns>
        public override IDataReader GetCommanderProduct(string productCode)
        {
            return ExecuteReader("GetCommanderProduct", productCode);
        }

        /// <summary>
        /// Saves the specified CommanderSalesOrder to the underlying data store via the configured DataProvider.
        /// If a duplicate Commander Sales Order occurs then an exception will be raised.
        /// </summary>
        /// <param name="commanderOrder">The commander order.</param>
        /// <returns></returns>
        public override int SaveCommanderSalesOrder(CommanderSalesOrder commanderOrder)
        {
            int returnValue;
            try
            {
                returnValue = ExecuteScalar("SaveCommanderSalesOrder", ERRORVALUE,
                                                     commanderOrder.Id,
                                                     commanderOrder.Site,
                                                     commanderOrder.OrderReference,
                                                     commanderOrder.CustomerNumber,
                                                     commanderOrder.DespatchRouteCode,
                                                     commanderOrder.DropNumber,
                                                     commanderOrder.TotalWeight,
                                                     commanderOrder.DeliveryAddress.Line1,
                                                     commanderOrder.DeliveryAddress.Line2,
                                                     commanderOrder.DeliveryAddress.Line3,
                                                     commanderOrder.DeliveryAddress.Line4,
                                                     commanderOrder.DeliveryAddress.Line5,
                                                     commanderOrder.CustomerOrderReference,
                                                     commanderOrder.Carrier,
                                                     commanderOrder.CustomerType,
                                                     commanderOrder.UpdatedBy,
                                                     commanderOrder.CheckSum
                                                 );
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    throw new ConstraintException(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// Saves the specified CommanderSalesOrderLines to the underlying data store via the configured DataProvider.
        /// If a corresponding Commander order does not exist then an error will occur.
        /// </summary>
        /// <param name="commanderOrderLines">The commander order lines.</param>
        /// <returns></returns>
        public override List<int> SaveCommanderSalesOrderLines(List<CommanderSalesOrderLine> commanderOrderLines)
        {
            List<int> returnValues = new List<int>();

            try
            {
                foreach (CommanderSalesOrderLine commanderSalesOrderLine in commanderOrderLines)
                {
                    returnValues.Add(
                        ExecuteScalar("SaveCommanderSalesOrderLine", ERRORVALUE,
                                           commanderSalesOrderLine.Id,
                                           commanderSalesOrderLine.Site,
                                           commanderSalesOrderLine.OrderReference,
                                           commanderSalesOrderLine.LineNumber,
                                           commanderSalesOrderLine.ProductCode,
                                           commanderSalesOrderLine.QuantityOrdered,
                                           commanderSalesOrderLine.CustomerReferenceNumber,
                                           commanderSalesOrderLine.UOM,
                                           commanderSalesOrderLine.SpecialInstructions1,
                                           commanderSalesOrderLine.SpecialInstructions2,
                                           commanderSalesOrderLine.SpecialInstructions3,
                                           commanderSalesOrderLine.SpecialInstructions4,
                                           commanderSalesOrderLine.SpecialInstructions5,
                                           commanderSalesOrderLine.CommanderSalesOrderId,
                                           commanderSalesOrderLine.UpdatedBy,
                                           commanderSalesOrderLine.CheckSum
                                       ));
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    throw new ConstraintException(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }
            return returnValues;
        }

        /// <summary>
        /// Retrieves a single CommanderSalesOrder from the underlying data store via the configured DataProvider for the supplied CommanderSalesOrder ID.
        /// An instance of an CommanderSalesOrder is returned to the caller or Null if no CommanderSalesOrder record is found.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <param name="salesOrderId">The sales order id.</param>
        /// <returns></returns>
        public override IDataReader GetCommanderSalesOrder(int salesOrderId)
        {
            return ExecuteReader("GetCommanderSalesOrder", salesOrderId);
        }

        /// <summary>
        /// Retrieves a list of all CommanderSalesOrderLines from the underlying data store via the configured DataProvider relating to the CommanderSalesOrder with the specified ID.
        /// A strongly typed list of CommanderSalesOrderLines is returned to the caller or an empty list if no records were found.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <param name="salesOrderId">The sales order id.</param>
        /// <returns></returns>
        public override IDataReader GetCommanderSalesOrderLines(int salesOrderId)
        {
            return ExecuteReader("GetCommanderSalesOrderLines");
        }

        /// <summary>
        /// Retrieves a single CommanderSalesOrder from the underlying data store via the configured DataProvider for the supplied order number.
        /// An instance of an CommanderSalesOrder is returned to the caller or Null if no CommanderSalesOrder record is found.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <param name="orderReference">The order number.</param>
        /// <returns></returns>
        public override IDataReader GetCommanderSalesOrderByOrderNumber(string orderReference)
        {
            return ExecuteReader("GetCommanderSalesOrderByOrderNumber", orderReference);
        }

        /// <summary>
        /// Retrieves a list of all CommanderSalesOrder from the underlying data store via the configured DataProvider.
        /// A strongly typed list of CommanderSalesOrder is returned to the caller or an empty list if no records were found.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <returns></returns>
        public override IDataReader GetCommanderSalesOrders()
        {
            return ExecuteReader("GetCommanderSalesOrders");
        }

        /// <summary>
        /// Deletes the commander sales order.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public override bool DeleteCommanderSalesOrder(int id)
        {
            bool returnValue;
            try
            {
                returnValue = ExecuteNonQuery("DeleteCommanderSalesOrder", id);
            }

            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    throw new ForeignKeyConstraint(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }

            return returnValue;
        }

        public override bool DeleteCommanderSalesOrderLine(int Id)
        {
            bool returnValue;
            try
            {
                returnValue = ExecuteNonQuery("DeleteCommanderSalesOrderLine", Id);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    throw new ForeignKeyConstraint(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Saves the commander sales order line.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <returns></returns>
        public override int SaveCommanderSalesOrderLine(CommanderSalesOrderLine line)
        {
            int returnValue;
            try
            {
                List<CommanderSalesOrderLine> lines = new List<CommanderSalesOrderLine>();
                lines.Add(line);
                returnValue = SaveCommanderSalesOrderLines(lines)[0];
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    throw new ConstraintException(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }
            return returnValue;
        }

        public override IDataReader GetCommanderSalesOrderLine(int id)
        {
            return ExecuteReader("GetCommanderSalesOrderLine", id);
        }

        #endregion

        #region Trip

        /// <summary>
        /// Gets the trip by unique trip number.
        /// </summary>
        /// <param name="tripNumber">The trip number.</param>
        /// <param name="tripDate">The trip date.</param>
        /// <param name="warehouseId">The warehouse id.</param>
        /// <returns></returns>
        public override IDataReader GetTripByWarehouseDateAndNumber(string tripNumber, DateTime tripDate, int warehouseId)
        {
            return ExecuteReader("GetTripByWarehouseDateAndNumber", tripNumber, tripDate, warehouseId);
        }

        public override IDataReader GetTrips(int warehouseId, DateTime requiredDeliveryDateFrom,
                                             DateTime? requiredDeliveryDateTo)
        {
            return ExecuteReader("GetTripSummaries",
                                        warehouseId,
                                        requiredDeliveryDateFrom,
                                        requiredDeliveryDateTo == null
                                            ? (object)DBNull.Value
                                            : requiredDeliveryDateTo.Value
                                    );
        }

        public override IDataReader GetTrip(int tripId)
        {
            return ExecuteReader("GetTrip", tripId);
        }

        public override IDataReader GetShipmentDropsForTrip(int tripId)
        {
            return ExecuteReader("GetShipmentDrop", tripId);
        }

        #endregion

        #region Optrak

        /// <summary>
        /// Gets summary details of shipments merged at a delivery point.
        /// </summary>
        /// <param name="routingHistoryId">The routing history is.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <param name="totalRows">The total rows.</param>
        /// <returns></returns>
        public override IDataReader GetMergedShipments(int routingHistoryId, string sortExpression, int startRowIndex,
                                                       int maximumRows, out int totalRows)
        {
            totalRows = ExecuteScalar("GetMergedShipmentsCount", ERRORVALUE, routingHistoryId);
            return ExecuteReader("GetMergedShipments", routingHistoryId, sortExpression, startRowIndex, maximumRows);
        }


        /// <summary>
        /// Merges the delivery points. All shipments with the specified warehouse code and Site Code (SiteCodeToUpdate)
        /// with the status of routing will be updated to the main site code
        /// </summary>
        /// <param name="routingHistoryId">The routing history id.</param>
        /// <param name="mainCode">The main code.</param>
        /// <param name="codesToUpdate">The code to update.</param>
        /// <returns></returns>
        public override int MergeDeliveryPointsManually(int routingHistoryId, int mainCode, List<int> codesToUpdate)
        {
            Database database = DatabaseFactory.CreateDatabase(ConnectionString);
            SqlCommand sqlCommand = new SqlCommand(String.Concat(ObjectQualifier, "MergeDeliveryPointsManually"));

            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter1 = new SqlParameter("@ReturnValue", SqlDbType.Int);
            parameter1.Direction = ParameterDirection.ReturnValue;
            sqlCommand.Parameters.Add(parameter1);
            sqlCommand.Parameters.Add(CreateInputParam("@RoutingHistoryId", SqlDbType.Int, routingHistoryId));
            sqlCommand.Parameters.Add(CreateInputParam("@MainCode", SqlDbType.NVarChar, mainCode.ToString()));
            sqlCommand.Parameters.Add(CreateInputParam("@CodeToUpdate", SqlDbType.NVarChar, string.Join(",", codesToUpdate.ConvertAll(new Converter<int, string>(intToString)).ToArray())));
            database.ExecuteNonQuery(sqlCommand);
            return GetReturnValue(sqlCommand);
        }

        public static string intToString(int intoToConvert)
        {
            return Convert.ToString(intoToConvert);
        }

        /// <summary>
        /// Performs a reshuffle of site codes after a manually merge has occured.
        /// </summary>
        /// <param name="routingHistoryId"></param>
        /// <returns></returns>
        public override bool ManualMergeReshuffle(int routingHistoryId)
        {
            bool returnValue;
            try
            {
                returnValue = ExecuteNonQuery("ManualMergeReshuffle", routingHistoryId);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    throw new ForeignKeyConstraint(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Deletes the relationship between a routing history data and a shipment.
        /// </summary>
        /// <param name="shipmentId">The id.</param>
        /// <param name="historyId">The history id.</param>
        /// <returns></returns>
        public override bool DeleteShipmentFromRouting(int shipmentId, int historyId)
        {
            bool returnValue;
            try
            {
                returnValue = ExecuteNonQuery("DeleteShipmentFromRouting", shipmentId, historyId);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    throw new ForeignKeyConstraint(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Gets the routing history details by the specified Id.
        /// </summary>
        /// <param name="routingHistoryId">The routing history id.</param>
        /// <returns></returns>
        public override IDataReader GetRoutingHistory(int routingHistoryId)
        {
            return ExecuteReader("GetRoutingHistory", routingHistoryId);
        }

        /// <summary>
        /// Gets all routing history details.
        /// </summary>
        /// <returns></returns>
        public override IDataReader GetRoutingHistory()
        {
            return ExecuteReader("GetRoutingHistories");
        }

        /// <summary>
        /// Saves a routing history record.
        /// </summary>
        /// <param name="routingHistory">The routing history details to save.</param>
        /// <returns></returns>
        public override int SaveRoutingHistory(RoutingHistory routingHistory)
        {
            int returnValue;
            try
            {
                returnValue = ExecuteScalar("SaveRoutingHistory", ERRORVALUE,
                                            routingHistory.Id,
                                            routingHistory.RegionId,
                                            routingHistory.DropFileReceivedDate == Null.NullDate
                                                ? (object)DBNull.Value
                                                : routingHistory.DropFileReceivedDate,
                                            routingHistory.TripPartFileReceivedDate == Null.NullDate
                                                ? (object)DBNull.Value
                                                : routingHistory.TripPartFileReceivedDate,
                                            null,
                                                 routingHistory.SentDate == Null.NullDate
                                                     ? (object)DBNull.Value
                                                     : routingHistory.SentDate,
                                                 routingHistory.ProcessStartedDate,
                                                 routingHistory.ResetDate == Null.NullDate
                                                     ? (object)DBNull.Value
                                                     : routingHistory.ResetDate,
                                                 routingHistory.ProcessedBy,
                                                 string.IsNullOrEmpty(routingHistory.ResetBy)
                                                     ? (object)DBNull.Value
                                                     : routingHistory.ResetBy,
                                                 routingHistory.CheckSum
                                             );
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    throw new ConstraintException(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }
            return returnValue;
        }

        public override IDataReader GetShipmentsForDeliveryPoint(string deliveryPoint, int routingHistoryId)
        {
            return ExecuteReader("GetShipmentsForDeliveryPoint", deliveryPoint, routingHistoryId);
        }

        public override IDataReader GetShipmentsByRoutingHistoryId(int routingHistoryId, string sortExpression,
                                                                   int pageIndex, int maximumRows, out int totalRows)
        {
            totalRows = ExecuteScalar("GetShipmentsByRoutingHistoryIdCount", ERRORVALUE, routingHistoryId);
            return ExecuteReader("GetShipmentsByRoutingHistoryId", routingHistoryId, sortExpression, pageIndex, maximumRows);
        }

        public override bool AddShipmentToRouting(int shipmentId, int routingHistoryId)
        {
            bool returnValue;
            try
            {
                returnValue = ExecuteNonQuery("AddShipmentToRouting", shipmentId, routingHistoryId);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    throw new ForeignKeyConstraint(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }

            return returnValue;
        }

        #endregion

        #region Load Category

        public override IDataReader GetLoadCategory(int categoryId)
        {
            return ExecuteReader("GetLoadCategory", categoryId);
        }

        public override IDataReader GetLoadCategory(string categoryCode)
        {
            return ExecuteReader("GetLoadCategoryByCode", categoryCode);
        }

        public override IDataReader GetLoadCategories()
        {
            return ExecuteReader("GetLoadCategories");
        }

        public override int SaveLoadCategory(LoadCategory loadCategory)
        {
            int returnValue;

            try
            {
                returnValue = ExecuteScalar("SaveLoadCategory", ERRORVALUE,
                                             loadCategory.Id,
                                             loadCategory.Code,
                                             loadCategory.Description,
                                             loadCategory.UpdatedBy,
                                             loadCategory.CheckSum
                                         );
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    throw new ConstraintException(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }
            return returnValue;
        }

        public override bool DeleteLoadCategory(int categoryId)
        {
            bool returnValue;

            try
            {
                returnValue = ExecuteNonQuery("DeleteLoadCategory", categoryId);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    throw new ForeignKeyConstraint(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }

            return returnValue;
        }

        #endregion

        #region Drops

        public override int SaveDrop(ShipmentDrop shipmentDrop)
        {
            int returnValue;

            try
            {
                returnValue = ExecuteScalar("SaveDrop", ERRORVALUE,
                                             shipmentDrop.Id,
                                             shipmentDrop.TripId,
                                              Null.GetNull(shipmentDrop.OrderSequence, DBNull.Value),
                                             Null.GetNull(shipmentDrop.ShipmentId, DBNull.Value),
                                             Null.GetNull(shipmentDrop.Weight, DBNull.Value),
                                             Null.GetNull(shipmentDrop.Volume, DBNull.Value),
                                             Null.GetNull(shipmentDrop.ArriveTime, DBNull.Value),
                                             Null.GetNull(shipmentDrop.DepartTime, DBNull.Value),
                                             Null.GetNull(shipmentDrop.LoadingTime, DBNull.Value),
                                             Null.GetNull(shipmentDrop.WaitingTime, DBNull.Value),
                                             Null.GetNull(shipmentDrop.TravellingTime, DBNull.Value),
                                             Null.GetNull(shipmentDrop.Distance, DBNull.Value),
                                             shipmentDrop.CallType,
                                             shipmentDrop.DropSequence,
                                             Null.GetNull(shipmentDrop.OriginalDepotId, DBNull.Value)
                                            );
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    throw new ConstraintException(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// Gets the drop. by unique key
        /// </summary>
        /// <param name="shipmentId">The id.</param>
        /// <param name="tripId">The trip id.</param>
        /// <param name="sequence">The sequence.</param>
        /// <param name="dropSequence">The drop sequence.</param>
        /// <returns></returns>
        public override IDataReader GetDrop(int shipmentId, int tripId, int sequence, int dropSequence)
        {
            return ExecuteReader(
                "GetDropByKey",
                Null.GetNull(shipmentId, DBNull.Value),
                Null.GetNull(tripId, DBNull.Value),
                Null.GetNull(sequence, DBNull.Value),
                Null.GetNull(dropSequence, DBNull.Value)
            );
        }


        /// <summary>
        /// Saves the drop line.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <returns></returns>
        public override int SaveDropLine(ShipmentDropLine line)
        {
            int returnValue;

            try
            {
                returnValue = ExecuteScalar("SaveDropLine", ERRORVALUE,
                                                 line.Id,
                                                 line.Quantity,
                                                 line.Weight,
                                                 line.Volume,
                                                 line.ShipmentLineId,
                                                 line.DropId,
                                                 line.SiteCode,
                                                 line.OriginalShipmentLineId
                                             );
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    throw new ConstraintException(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }
            return returnValue;
        }

        #endregion

        #region Error Type

        public override IDataReader GetErrorType(string exceptionType, string policyName, string opCoCode)
        {
            return ExecuteReader("GetErrorType", exceptionType, policyName, opCoCode);
        }

        public override int SaveErrorType(ErrorType type)
        {
            int returnValue;

            try
            {
                returnValue = ExecuteScalar("SaveErrorType", ERRORVALUE,
                                                 type.Id,
                                                 type.Policy,
                                                 type.EmailOperator,
                                                 (int)type.Priority,
                                                 type.RequiresAcknowledgement,
                                                 type.ExceptionType,
                                                 type.HasEmailHandler,
                                                 type.EmailRecipients,
                                                 type.EmailSubject,
                                                 type.OpCoId,
                                                 type.CheckSum
                                             );
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    throw new ConstraintException(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }
            return returnValue;
        }

        #endregion

        private static SqlParameter CreateInputParam(string paramName, SqlDbType dbType, object objValue)
        {
            SqlParameter parameter1 = new SqlParameter(paramName, dbType);
            if (objValue == null)
            {
                objValue = string.Empty;
            }
            parameter1.Value = objValue;
            return parameter1;
        }

        private static int GetReturnValue(SqlCommand cmd)
        {
            foreach (SqlParameter parameter1 in cmd.Parameters)
            {
                if (((parameter1.Direction == ParameterDirection.ReturnValue) && (parameter1.Value != null)) &&
                    (parameter1.Value is int))
                {
                    return (int)parameter1.Value;
                }
            }
            return -1;
        }

        /// <summary>
        /// Resets the routing locks. This means that the routing history record will be updated 
        /// and the status of the shipments which are locked will return to Mapped
        /// </summary>
        /// <param name="routingHistoryId">The routing history id.</param>
        /// <param name="resetBy">The reset by.</param>
        /// <returns></returns>
        public override bool ResetRoutingLocks(int routingHistoryId, string resetBy)
        {
            return ExecuteNonQuery("ResetRoutingLocks", routingHistoryId, resetBy);
        }

        /// <summary>
        /// Removes the items from routing. 
        /// </summary>
        /// <param name="routingHistoryId">The routing history id.</param>
        /// <param name="removedBy">The removed by.</param>
        /// <returns></returns>
        public override bool RemoveItemsFromRouting(int routingHistoryId, string removedBy)
        {
            Database database = DatabaseFactory.CreateDatabase(ConnectionString);
            SqlCommand sqlCommand = new SqlCommand(String.Concat(ObjectQualifier, "RemoveFromRouting"));

            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter1 = new SqlParameter("@ReturnValue", SqlDbType.Int);
            parameter1.Direction = ParameterDirection.ReturnValue;
            sqlCommand.Parameters.Add(parameter1);
            sqlCommand.Parameters.Add(CreateInputParam("@RoutingHistoryId", SqlDbType.Int, routingHistoryId));
            sqlCommand.Parameters.Add(CreateInputParam("@RemovedBy", SqlDbType.VarChar, removedBy));
            database.ExecuteNonQuery(sqlCommand);
            return GetReturnValue(sqlCommand) != -1;
        }

        /// <summary>
        /// Saves the printer.
        /// </summary>
        /// <param name="printer">The printer.</param>
        /// <returns></returns>
        public override int SavePrinter(Printer printer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the optrak locks.
        /// </summary>
        /// <param name="estimatedDateTo">The estimated date to.</param>
        /// <param name="processedBy">The processed by.</param>
        /// <param name="warehouseId">The warehouse id.</param>
        /// <param name="regionId">The region id.</param>
        /// <returns></returns>
        public override int SetOptrakLocks(DateTime estimatedDateTo, string processedBy, int warehouseId, int regionId)
        {
            Database database = DatabaseFactory.CreateDatabase(ConnectionString);
            SqlCommand sqlCommand = new SqlCommand(String.Concat(ObjectQualifier, "SetOptrakLocks"));

            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter1 = new SqlParameter("@ReturnValue", SqlDbType.Int);
            parameter1.Direction = ParameterDirection.ReturnValue;
            sqlCommand.Parameters.Add(parameter1);
            sqlCommand.Parameters.Add(CreateInputParam("@EstimatedDateTo", SqlDbType.DateTime, Null.GetNull(estimatedDateTo, DBNull.Value)));
            sqlCommand.Parameters.Add(CreateInputParam("@LockedBy", SqlDbType.NVarChar, processedBy));
            sqlCommand.Parameters.Add(CreateInputParam("@WarehouseId", SqlDbType.Int, warehouseId));
            sqlCommand.Parameters.Add(CreateInputParam("@RegionId", SqlDbType.Int, regionId));
            database.ExecuteNonQuery(sqlCommand);
            return GetReturnValue(sqlCommand);
        }

        /// <summary>
        /// Sets the optrak locks.
        /// </summary>
        /// <param name="routingHistoryId">The routing history id.</param>
        /// <param name="estimatedDateTo">The estimated date to.</param>
        /// <param name="processedBy">The processed by.</param>
        /// <param name="warehouseId">The warehouse id.</param>
        /// <returns></returns>
        public override int SetOptrakLocks(int routingHistoryId, DateTime estimatedDateTo, string processedBy, int warehouseId)
        {
            Database database = DatabaseFactory.CreateDatabase(ConnectionString);
            SqlCommand sqlCommand = new SqlCommand(String.Concat(ObjectQualifier, "AddOptrakLocks"));

            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter1 = new SqlParameter("@ReturnValue", SqlDbType.Int);
            parameter1.Direction = ParameterDirection.ReturnValue;
            sqlCommand.Parameters.Add(parameter1);
            sqlCommand.Parameters.Add(CreateInputParam("@RoutingHistoryId", SqlDbType.Int, Null.GetNull(routingHistoryId, DBNull.Value)));
            sqlCommand.Parameters.Add(CreateInputParam("@EstimatedDateTo", SqlDbType.DateTime, Null.GetNull(estimatedDateTo, DBNull.Value)));
            sqlCommand.Parameters.Add(CreateInputParam("@LockedBy", SqlDbType.NVarChar, processedBy));
            sqlCommand.Parameters.Add(CreateInputParam("@WarehouseId", SqlDbType.Int, warehouseId));
            database.ExecuteNonQuery(sqlCommand);
            return GetReturnValue(sqlCommand);
        }

        /// <summary>
        /// Gets the routing history by shipment line id.
        /// </summary>
        /// <param name="shipmentLineId">The shipment line id.</param>
        /// <returns></returns>
        public override IDataReader GetRoutingHistoryByShipmentLineId(int shipmentLineId)
        {
            return ExecuteReader("GetRoutingHistoryByShipmentLineId", shipmentLineId);
        }

        /// <summary>
        /// Gets the routing history by shipment id.
        /// </summary>
        /// <param name="shipmentId">The shipment id.</param>
        /// <returns></returns>
        public override IDataReader GetRoutingHistoryByShipmentId(int shipmentId)
        {
            return ExecuteReader("GetRoutingHistoryByShipmentId", shipmentId);
        }

        /// <summary>
        /// Sets the shipments to routed.
        /// </summary>
        /// <param name="routingHistoryId">The routing history id.</param>
        /// <param name="updatedBy">The updated by.</param>
        /// <param name="updatedDate">The updated date.</param>
        /// <returns></returns>
        public override bool SetShipmentsToRouted(int routingHistoryId, string updatedBy, DateTime updatedDate)
        {
            bool returnValue;

            try
            {
                returnValue = ExecuteNonQuery("SetShipmentsToRouted", routingHistoryId, updatedBy, updatedDate);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    throw new ForeignKeyConstraint(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }

            return returnValue;
        }


        /// <summary>
        /// Acknowledges the log.
        /// </summary>
        /// <param name="Id">The id of the log entry to acknowledge.</param>
        /// <param name="acknowledgedBy">Who acknowledged by.</param>
        /// <param name="acknowledgedDate">The acknowledged date.</param>
        /// <returns></returns>
        public override bool AcknowledgeLog(int Id,string acknowledgedBy,DateTime acknowledgedDate)
        {
            return ExecuteNonQuery("AcknowledgeLog", Id, acknowledgedBy, acknowledgedDate);
        }
    }
}