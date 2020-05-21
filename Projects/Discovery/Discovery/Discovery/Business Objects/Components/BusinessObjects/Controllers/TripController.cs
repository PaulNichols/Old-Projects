using System;
using System.Collections.Generic;
using System.Data;
using Discovery.ComponentServices.DataAccess;
using Discovery.ComponentServices.ExceptionHandling;
using Discovery.Utility;
using Discovery.Utility.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Discovery.BusinessObjects.Controllers
{
    /*************************************************************************************************
** CLASS:	TripController
**
** OVERVIEW:
** This controller class contains all methods related to OpCos including calling data access methods
**
** MODIFICATION HISTORY:
**
** Date:		Version:    Who:	Change:
** 8/8/06		1.0			PJN		Initial Version
************************************************************************************************/

    /// <summary>
    /// A class to provide the trip controller which is a business object controller
    /// with namespace Discovery.BusinessObjects.Controllers
    /// </summary>
    public static class TripController
    {
        /// <summary>
        /// Gets the trip summaries.
        /// </summary>
        /// <param name="warehouseId">The warehouse id.</param>
        /// <param name="requiredDeliveryDateFrom">The required delivery date from.</param>
        /// <param name="requiredDeliveryDateTo">The required delivery date to.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        public static List<Trip> GetTripSummaries(int warehouseId, DateTime? requiredDeliveryDateFrom,
                                                  DateTime? requiredDeliveryDateTo, string sortExpression)
        {
            List<Trip> trips = new List<Trip>();

            if (requiredDeliveryDateFrom != null)
            {
                try
                {

                        trips =
                            CBO<Trip>.FillCollection(
                                DataAccessProvider.Instance().GetTrips(warehouseId, requiredDeliveryDateFrom.Value,
                                                            requiredDeliveryDateTo));

                }
                catch (Exception ex)
                {
                    if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
                }
                if (string.IsNullOrEmpty(sortExpression))
                {
                    sortExpression = "TripNumber,StartDate";
                }
                trips.Sort(new UniversalComparer<Trip>(sortExpression));
            }
            return trips;
        }

        /// <summary>
        /// Saves the trip.
        /// </summary>
        /// <param name="trip">The trip.</param>
        /// <returns></returns>
        public static int SaveTrip(Trip trip)
        {
            try
            {
                if (trip.IsValid)
                {

                    //check to see if we've had this data before or we have recieved a CALL/TRIPPART file that needed us to 
                    //add a record to maintain referential integrity
                    Trip existingTrip = GetTripByWarehouseDateAndNumber(trip.TripNumber, trip.StartDate, trip.WarehouseId);
                    if (existingTrip != null)
                    {
                        //just overwite with this new data?
                        //log?
                        trip.Id = existingTrip.Id;
                        trip.CheckSum = existingTrip.CheckSum;
                    }

                    trip.Id = DataAccessProvider.Instance().SaveTrip(trip);
                }
                else
                {
                    // Entity is not valid
                    throw new InValidBusinessObjectException(trip);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            // Done
            return trip.Id;
        }

        /// <summary>
        /// Gets the trip by warehouse date and number.
        /// </summary>
        /// <param name="tripNumber">The trip number.</param>
        /// <param name="deliveryDate">The delivery date.</param>
        /// <param name="warehouseId">The warehouse id.</param>
        /// <returns></returns>
        public static Trip GetTripByWarehouseDateAndNumber(string tripNumber,DateTime deliveryDate,int warehouseId)
        {
            Trip trip = new Trip();

            try
            {
                    trip =
                        CBO<Trip>.FillObject(DataAccessProvider.Instance().GetTripByWarehouseDateAndNumber(tripNumber, deliveryDate,warehouseId));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }


            return trip;
        }

        /// <summary>
        /// Gets the trip.
        /// </summary>
        /// <param name="tripId">The trip id.</param>
        /// <returns></returns>
        public static Trip GetTrip(int tripId)
        {
            Trip trip = null;
            try
            {
                    trip = CBO<Trip>.FillObject(DataAccessProvider.Instance().GetTrip(tripId), CustomFill, true);

                    return trip;

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return trip;
        }

        /// <summary>
        /// Customs the fill.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fully populate].</param>
        public static void CustomFill(Trip item, IDataReader dataReader, bool fullyPopulate)
        {
            item.Warehouse = WarehouseController.GetWarehouse(item.WarehouseId, fullyPopulate);
        }



        /// <summary>
        /// Gets the shipment drops for trip.
        /// </summary>
        /// <param name="tripId">The trip id.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        public static List<ShipmentDrop> GetShipmentDropsForTrip(int tripId, string sortExpression)
        {
            List<ShipmentDrop> shipmentDrops = new List<ShipmentDrop>();

            try
            {

                    shipmentDrops =
                        CBO<ShipmentDrop>.FillCollection(
                            DataAccessProvider.Instance().GetShipmentDropsForTrip(tripId));

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            if (string.IsNullOrEmpty(sortExpression))
            {
                sortExpression = "DropSequence";
            }
            shipmentDrops.Sort(new UniversalComparer<ShipmentDrop>(sortExpression));

            return shipmentDrops;
        }
    }
}