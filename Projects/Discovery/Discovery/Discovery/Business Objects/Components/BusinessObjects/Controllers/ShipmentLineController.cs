/*************************************************************************************************
 ** FILE:	ShipmentLineController.cs
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
using Discovery.ComponentServices.DataAccess;
using Discovery.ComponentServices.ExceptionHandling;
using Discovery.Utility.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Discovery.BusinessObjects.Controllers
{
    /// <summary>
    /// A class to provide the TDC shipment line controller which is a business object controller
    /// with namespace Discovery.BusinessObjects.Controllers
    /// </summary>
    public static class TDCShipmentLineController
    {
        /// <summary>
        /// Deletes the TDC shipment line.
        /// </summary>
        /// <param name="shipmentLineId">The shipment line id.</param>
        /// <returns></returns>
        public static bool DeleteLine(int shipmentLineId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves the TDC shipment line.
        /// </summary>
        /// <param name="shipmentLine">The shipment line.</param>
        /// <returns></returns>
        public static int SaveLine(TDCShipmentLine shipmentLine)
        {
            try
            {
                // Make sure the shipment line is valid
                if (shipmentLine.IsValid)
                {
                    // Save the shipment line to the db
                    shipmentLine.Id = DataAccessProvider.Instance().SaveTDCShipmentLine(shipmentLine);

                    // Get the checksum for the entity
                    FrameworkController.GetChecksum(shipmentLine, "ShipmentLine");
                }
                else
                {
                    // Entity is not valid
                    throw new InValidBusinessObjectException(shipmentLine);
                }
            }
            catch (Exception ex)
            {
                // Log an throw if configured to do so
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;

                // Failed
                return -1;
            }

            // Done
            return shipmentLine.Id;
        }

        /// <summary>
        /// Gets the TDC shipment lines.
        /// </summary>
        /// <param name="shipmentId">The shipment id.</param>
        /// <returns></returns>
        public static List<TDCShipmentLine> GetLines(int shipmentId)
        {
            return GetLines(shipmentId, "");
        }

        /// <summary>
        /// Gets the TDC shipment lines.
        /// </summary>
        /// <param name="shipmentId">The shipment id.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        public static List<TDCShipmentLine> GetLines(int shipmentId, string sortExpression)
        {
            try
            {
                return CBO<TDCShipmentLine>.FillCollection(DataAccessProvider.Instance().GetTDCShipmentLines(shipmentId, sortExpression));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;

                return null;
            }
        }

        /// <summary>
        /// Gets the TDC shipment line.
        /// </summary>
        /// <param name="shipmentLineId">The shipment line id.</param>
        /// <returns></returns>
        public static TDCShipmentLine GetLine(int shipmentLineId)
        {
            return CBO<TDCShipmentLine>.FillObject(DataAccessProvider.Instance().GetTDCShipmentLine(shipmentLineId));
        }

        internal static TDCShipmentLine GetLine(int lineCode, string opcoCode, string shipmentNumber, string despatchNumber)
        {
            return CBO<TDCShipmentLine>.FillObject(DataAccessProvider.Instance().GetTDCShipmentLine(lineCode, opcoCode, shipmentNumber, despatchNumber));
        }
    }


    /// <summary>
    /// A class to provide the OpCo shipment line controller which is a business object controller
    /// with namespace Discovery.BusinessObjects.Controllers
    /// </summary>
    public static class OpCoShipmentLineController
    {
        /// <summary>
        /// Deletes the OpCo shipment line.
        /// </summary>
        /// <param name="shipmentLineId">The shipment line id.</param>
        /// <returns></returns>
        public static bool DeleteLine(int shipmentLineId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves the OpCo shipment line.
        /// </summary>
        /// <param name="shipmentLine">The shipment line.</param>
        /// <returns></returns>
        public static int SaveLine(ShipmentLine shipmentLine)
        {
            try
            {
                // Make sure the shipment line is valid
                if (shipmentLine.IsValid)
                {
                    // Save the shipment line to the db
                    shipmentLine.Id = DataAccessProvider.Instance().SaveOpCoShipmentLine(shipmentLine);

                    // Get the checksum for the entity
                    FrameworkController.GetChecksum(shipmentLine);
                }
                else
                {
                    // Entity is not valid
                    throw new InValidBusinessObjectException(shipmentLine);
                }
            }
            catch (Exception ex)
            {
                // Log an throw if configured to do so
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;

                // Failed!
                return -1;
            }

            // Done
            return shipmentLine.Id;
        }

        /// <summary>
        /// Gets the OpCo shipment lines.
        /// </summary>
        /// <param name="shipmentId">The shipment id.</param>
        /// <returns></returns>
        public static List<OpCoShipmentLine> GetLines(int shipmentId)
        {
            return GetLines(shipmentId, "");
        }

        /// <summary>
        /// Gets the OpCo shipment lines.
        /// </summary>
        /// <param name="shipmentId">The shipment id.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        public static List<OpCoShipmentLine> GetLines(int shipmentId, string sortExpression)
        {
            try
            {
                return CBO<OpCoShipmentLine>.FillCollection(DataAccessProvider.Instance().GetOpCoShipmentLines(shipmentId, sortExpression));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;

                return null;
            }
        }

        /// <summary>
        /// Gets the OpCo shipment line.
        /// </summary>
        /// <param name="shipmentLineId">The shipment line id.</param>
        /// <returns></returns>
        public static OpCoShipmentLine GetLine(int shipmentLineId)
        {
            throw new NotImplementedException();
        }
    }
}