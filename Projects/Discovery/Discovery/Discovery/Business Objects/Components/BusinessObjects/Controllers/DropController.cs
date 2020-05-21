/*************************************************************************************************
 ** FILE:	DropController.cs
 ** DATE:	30/05/2006
 ** AUTHOR:	Paul Nichols
 **
 **
 ** OVERVIEW:
 **
 ** MODIFICATION HISTORY:
 **
 ** Date:		Version:	Who:	Change:
 ** 30/5/06		1.0		    PJN	    Initial Version
 ************************************************************************************************/
using System;
using Discovery.ComponentServices.DataAccess;
using Discovery.Utility;
using Discovery.Utility.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Discovery.BusinessObjects.Controllers
{
    /*************************************************************************************************
** CLASS:	TripController
**
** OVERVIEW:
** This controller class contains all methods related to Drops including calling data access methods
**
** MODIFICATION HISTORY:
**
** Date:		Version:    Who:	Change:
** 8/8/06		1.0			PJN		Initial Version
************************************************************************************************/

    /// <summary>
    /// A class to provide the drop controller which is a business object controller
    /// with namespace Discovery.BusinessObjects.Controllers
    /// </summary>
    public static class DropController
    {


        /// <summary>
        /// Saves the shipmentDrop.
        /// </summary>
        /// <param name="shipmentDrop">The shipmentDrop.</param>
        /// <param name="depotCode">The depot code.</param>
        /// <returns></returns>
        public static Int32 SaveDrop(ShipmentDrop shipmentDrop, string depotCode)
        {

            if (shipmentDrop.TripId == Null.NullInteger)
            {
               
                //get warehouse id which will be used to find the trip id
                int warehouseId;
                Warehouse relatedWarehouse = WarehouseController.GetWarehouse(depotCode);
                if (relatedWarehouse == null)
                {
                    throw new Exception(
                            string.Format("When importing ShipmentDrop data the related warehouse could not be found. The Warehouse number was '{0}'.",
                            depotCode));
                }
                warehouseId = relatedWarehouse.Id;
                shipmentDrop.OriginalDepotId = warehouseId;

                //get and set the related trip id
                Trip relatedTrip = TripController.GetTripByWarehouseDateAndNumber(shipmentDrop.TripNumber, shipmentDrop.DeliveryDate, warehouseId);
                if (relatedTrip == null)
                {
                    //the trip file hasn't bee recieved yet so add a record to relate to for now
                    relatedTrip = new Trip();
                    relatedTrip.TripNumber = shipmentDrop.TripNumber;
                    relatedTrip.StartDate = shipmentDrop.DeliveryDate;
                    relatedTrip.WarehouseId = warehouseId;
                    relatedTrip.Id = TripController.SaveTrip(relatedTrip);
                }
                if (relatedTrip.Id == -1)
                {
                    throw new Exception(
                      string.Format("When importing ShipmentDrop data a related trip could not be created with details, Trip Number: '{0}', Search Date: '{1}', and WarehouseId: '{2}'.",
                      shipmentDrop.TripNumber, shipmentDrop.DeliveryDate.ToShortDateString(), warehouseId));
                }
                shipmentDrop.TripId = relatedTrip.Id;
            }

            if (shipmentDrop.ShipmentId == Null.NullInteger && shipmentDrop.CallType != ShipmentDrop.CallTypeEnum.Depot)
            {
                //get shipment id
                TDCShipment tdcShipment = TDCShipmentController.GetShipment(shipmentDrop.OpcoCode, shipmentDrop.ShipmentNumber, shipmentDrop.DespatchNumber);
                if (tdcShipment == null)
                {
                    throw new Exception(
                       string.Format("When importing ShipmentDrop data the related shipment could not be found. The Opco Code was '{0}', the Shipment Number was '{1}', and the Despatch Number was '{2}'.",
                       shipmentDrop.OpcoCode, shipmentDrop.ShipmentNumber, shipmentDrop.DespatchNumber));
                }
                else if(tdcShipment.Status==Shipment.StatusEnum.Routed)
                {
                    //TODO: Log that this shipment has already been routed, etc
                }

                //update the related shipments estimated date with the time now that optrak has calculated it for us
                string[] arriveTime = shipmentDrop.ArriveTime.Split(':');
                tdcShipment.EstimatedDeliveryDate.AddHours(Convert.ToDouble(arriveTime[0]));
                tdcShipment.EstimatedDeliveryDate.AddMinutes(Convert.ToDouble(arriveTime[1]));
                TDCShipmentController.SaveShipment(tdcShipment);

                shipmentDrop.ShipmentId = tdcShipment.Id;
            }


            try
            {
                if (shipmentDrop.IsValid)
                {
                    //check to see if we've had this data before or we have recieved a TRIPPART file that needed us to 
                    //add a record to maintain referential integrity
                    ShipmentDrop existingShipmentDrop = GetDrop(shipmentDrop.ShipmentId, shipmentDrop.TripId, shipmentDrop.OrderSequence, shipmentDrop.DropSequence);
                    if (existingShipmentDrop != null)
                    {
                        //just overwite with this new data
                        //log?
                        shipmentDrop.Id = existingShipmentDrop.Id;
                        shipmentDrop.CheckSum = existingShipmentDrop.CheckSum;
                    }
                    // Save entity
                    shipmentDrop.Id = DataAccessProvider.Instance().SaveDrop(shipmentDrop);

                    if (shipmentDrop.Id == -1)
                    {
                        throw new Exception(
                            string.Format("A Drop could not be saved. The Opco Code was '{0}', the Shipment Number was '{1}', and the Despatch Number was '{2}'.",
                            shipmentDrop.OpcoCode, shipmentDrop.ShipmentNumber, shipmentDrop.DespatchNumber));
                    }
                }
                else
                {
                    // Entity is not valid
                    throw new InValidBusinessObjectException(shipmentDrop);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            return shipmentDrop.Id;
        }

        /// <summary>
        /// Gets the drop.
        /// </summary>
        /// <param name="shipmentId">The shipment id.</param>
        /// <param name="tripId">The trip id.</param>
        /// <param name="sequence">The sequence.</param>
        /// <param name="dropSequence">The drop sequence.</param>
        /// <returns></returns>
        public static ShipmentDrop GetDrop(int shipmentId, int tripId, int sequence, int dropSequence)
        {
            ShipmentDrop shipmentDrop =null;
            try
            {
                shipmentDrop = CBO<ShipmentDrop>.FillObject(DataAccessProvider.Instance().GetDrop(shipmentId, tripId, sequence, dropSequence));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return shipmentDrop;
        }

        public static Int32 SaveDropLine(ShipmentDropLine line)
        {

            TDCShipmentLine tdcShipmentLine;
            //if (line.ShipmentLineId == Null.NullInteger)
            //{
            //    //get shipment id

            tdcShipmentLine = TDCShipmentLineController.GetLine(line.LineCode, line.OpcoCode, line.ShipmentNumber, line.DespatchNumber);
            if (tdcShipmentLine == null)
            {
                throw new Exception(
                    string.Format("When importing Drop Line data the related Shipment Line could not be found. The line code was '{3}', Opco Code was '{0}', the Shipment Number was '{1}', and the Despatch Number was '{2}'.",
                                  line.OpcoCode, line.ShipmentNumber, line.DespatchNumber, line.LineCode));
            }

            //if (line.ShipmentLineId == Null.NullInteger)
            //{
            //    throw new Exception(
            //        string.Format("When importing Drop Line data the related Shipment Line could not be found. The Opco Code was '{0}', the Shipment Number was '{1}', and the Despatch Number was '{2}' and the Line Code was '{3}'.",
            //                      line.OpcoCode, line.ShipmentNumber, line.DespatchNumber, line.LineCode));
            //}
            line.ShipmentLineId = tdcShipmentLine.Id;
          

            if (!line.Split)
            {

            }
            else
            {


                //optrak split this line
                //  tdcShipment.SplitShipment()

            }

            //}

            if (line.DropId == Null.NullInteger)
            {
                //get warehouse id which will be used to find the trip id
                int warehouseId;
                Warehouse relatedWarehouse = WarehouseController.GetWarehouse(line.Depot);
                if (relatedWarehouse == null)
                {
                    throw new Exception(
                            string.Format("When importing Drop Line data the related warehouse could not be found. The Warehouse number was '{0}'.",
                            line.Depot));
                }
                warehouseId = relatedWarehouse.Id;

                //get the related trip id
                Trip relatedTrip = TripController.GetTripByWarehouseDateAndNumber(line.TripNumber, line.DeliveryDate, warehouseId);
                if (relatedTrip == null)
                {
                    //the trip file hasn't bee recieved yet so add a record to relate to for now
                    relatedTrip = new Trip();
                    relatedTrip.TripNumber = line.TripNumber;
                    relatedTrip.StartDate = line.DeliveryDate;
                    relatedTrip.WarehouseId = warehouseId;
                    relatedTrip.Id = TripController.SaveTrip(relatedTrip);
                }
                if (relatedTrip.Id == -1)
                {
                    throw new Exception(
                      string.Format("When importing ShipmentDrop data a related trip could not be created with details, Trip Number: '{0}', Search Date: '{1}', and WarehouseId: '{2}'.",
                      line.TripNumber, line.DeliveryDate.ToShortDateString(), warehouseId));
                }
                

                //get drop id this line relates to
                ShipmentDrop relatedShipmentDrop = GetDrop(tdcShipmentLine.ShipmentId, relatedTrip.Id, line.OrderSequence, line.DropSequence);
                if (relatedShipmentDrop == null)
                {
                    //a related shipment drop could not be found
                    //this could be because that file has not been proced yet so create
                    //a blank one to relate to
                    relatedShipmentDrop = new ShipmentDrop();
                    relatedShipmentDrop.ShipmentId = tdcShipmentLine.ShipmentId;
                    relatedShipmentDrop.TripId = relatedTrip.Id;
                   // relatedShipmentDrop.OrderSequence = line.OrderSequence;
                    relatedShipmentDrop.DropSequence = line.DropSequence;
                    relatedShipmentDrop.Id = SaveDrop(relatedShipmentDrop, line.Depot);
                }
                if (relatedShipmentDrop.Id == -1)
                {
                    throw new Exception(
                      string.Format("When importing Drop Line data a related Drop could not be created with details, Shipment Id: '{0}', Trip Id: '{1}', and Order OrderSequence: '{2}' and Drop OrderSequence '{3}'.",
                      tdcShipmentLine.ShipmentId, relatedTrip.Id, line.OrderSequence, line.DropSequence));
                }

                line.DropId = relatedShipmentDrop.Id;

            }

            try
            {
                if (line.IsValid)
                {
                    // Save entity
                    //check to see if we've had this data before or we have recieved a TRIPPART file that needed us to 
                    //add a record to maintain referential integrity
                    ShipmentDropLine existingShipmentDropLine = null;// DropController.GetDropLine(line.ShipmentLineId);
                    if (existingShipmentDropLine != null)
                    {
                        //just overwite with this new data
                        //log?
                        line.Id = existingShipmentDropLine.Id;
                        line.CheckSum = existingShipmentDropLine.CheckSum;
                    }
                    line.OriginalShipmentLineId = line.ShipmentLineId;
                    line.Id = DataAccessProvider.Instance().SaveDropLine(line);
                }
                else
                {
                    // Entity is not valid
                    throw new InValidBusinessObjectException(line);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            // Done
            return line.Id;
        }
    }
}
