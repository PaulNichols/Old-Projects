/*************************************************************************************************
 ** FILE:	ShipmentController.cs
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
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Transactions;
using Discovery.ComponentServices.DataAccess;
using Discovery.ComponentServices.ExceptionHandling;
using Discovery.Utility;
using Discovery.Utility.DataAccess;
using Discovery.ComponentServices.Mapping;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Discovery.BusinessObjects.Controllers
{
    /// <summary>
    /// Summary Description for OpCoShipmentController
    /// </summary>
    public static class OpCoShipmentController
    {
        /// <summary>
        /// Saves the shipments.
        /// </summary>
        /// <param name="shipments">The shipments.</param>
        /// <returns></returns>
        public static List<int> SaveShipments(List<OpCoShipment> shipments)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the shipment.
        /// </summary>
        /// <param name="opCo">The op co.</param>
        /// <param name="shipmentNumber">The shipment number.</param>
        /// <param name="despatchNumber">The despatch number.</param>
        /// <returns></returns>
        public static OpCoShipment GetShipment(string opCo, string shipmentNumber, string despatchNumber)
        {
            try
            {
                return CBO<OpCoShipment>.FillObject(
                            DataAccessProvider.Instance().GetOpCoShipment(opCo, shipmentNumber, despatchNumber),
                            CustomShipmentFill,
                            true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the shipment.
        /// </summary>
        /// <param name="ShipmentId">The shipment id.</param>
        /// <returns></returns>
        public static OpCoShipment GetShipment(int ShipmentId)
        {
            try
            {
                return CBO<OpCoShipment>.FillObject(
                            DataAccessProvider.Instance().GetOpCoShipment(ShipmentId),
                            CustomShipmentFill,
                            true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the shipments.
        /// </summary>
        /// <param name="deliveryLocation">The delivery location.</param>
        /// <returns></returns>
        public static List<OpCoShipment> GetShipments(string deliveryLocation)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the shipments.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="numRows">The num rows.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fully populate].</param>
        /// <returns></returns>
        public static List<OpCoShipment> GetShipments(
                    ShipmentCriteria criteria,
                    string sortExpression,
                    int pageIndex,
                    int numRows,
                    bool fullyPopulate)
        {
            // Get the opco shipments from the db
            return CBO<OpCoShipment>.FillCollection(
                        DataAccessProvider.Instance().GetOpCoShipments(criteria, sortExpression, pageIndex, numRows),
                        CustomShipmentFill,
                        fullyPopulate);
        }

        /// <summary>
        /// Gets the shipments count.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        public static int GetShipmentsCount(ShipmentCriteria criteria)
        {
            return DataAccessProvider.Instance().GetOpCoShipmentsCount(criteria);
        }

        /// <summary>
        /// Updates the shipment status.
        /// </summary>
        /// <param name="shipment">The shipment.</param>
        public static void UpdateShipmentStatus(OpCoShipment shipment)
        {
            try
            {
                // Get an instance of the data access provider
                DataAccessProvider dataAccessProvider = DataAccessProvider.Instance();

                // Update the status of the opco shipment in the db
                dataAccessProvider.UpdateOpCoShipmentStatus(shipment);
            }
            catch (Exception ex)
            {
                // Log an throw if configured to do so
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
        }

        /// <summary>
        /// Saves the shipment.
        /// </summary>
        /// <param name="shipment">The shipment.</param>
        /// <returns></returns>
        public static int SaveShipment(OpCoShipment shipment)
        {
            try
            {
                // Make sure the shipment is valid
                if (shipment.IsValid)
                {
                    // Save the shipment to the db and update the id, this will update the shipment lines if required
                    shipment.Id = DataAccessProvider.Instance().SaveOpCoShipment(shipment);

                    // Get the checksum for the entity
                    FrameworkController.GetChecksum(shipment);

                    // Save the shipment lines to the db
                    foreach (OpCoShipmentLine opCoShipmentLine in shipment.ShipmentLines)
                    {
                        // Save the shipment line and update the shipment line id if required
                        opCoShipmentLine.Id = OpCoShipmentLineController.SaveLine(opCoShipmentLine);
                    }
                }
                else
                {
                    // Entity is not valid
                    throw new InValidBusinessObjectException(shipment);
                }
            }
            catch (Exception ex)
            {
                // Generate a new exception
                ex = new Exception(string.Format("Failed to save OpCo shipment {0} - {1} for OpCo {2}.  {3}", shipment.ShipmentNumber, shipment.DespatchNumber, shipment.OpCoCode, ex.Message));

                // Log an throw if configured to do so
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw ex;

                // We failed to save the shipment
                return -1;
            }

            // Done
            return shipment.Id;
        }

        /// <summary>
        /// Deletes the shipment.
        /// </summary>
        /// <param name="shipmentId">The shipment id.</param>
        /// <returns></returns>
        public static bool DeleteShipment(int shipmentId)
        {
            return DataAccessProvider.Instance().DeleteOpCoShipment(shipmentId);
        }

        // Custom object hydration
        /// <summary>
        /// Customs the shipment fill.
        /// </summary>
        /// <param name="shipment">The shipment.</param>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fully populate].</param>
        private static void CustomShipmentFill(OpCoShipment shipment, IDataReader dataReader, bool fullyPopulate)
        {
            // See if we fully populate this entity
            if (fullyPopulate)
            {
                // Populate shipment lines
                List<OpCoShipmentLine> opCoLines = OpCoShipmentLineController.GetLines(shipment.Id);
                // The total number of lines
                int TotalLines = opCoLines.Count;
                // Clear existing lines
                shipment.ShipmentLines.Clear();
                // Add new lines
                foreach (OpCoShipmentLine opCoLine in opCoLines)
                {
                    // Specify any derived columns that we need
                    opCoLine.TotalLines = TotalLines;
                    // Add the line
                    shipment.ShipmentLines.Add(opCoLine);
                }
            }

            // Populate opco contact
            shipment.OpCoContact.Email = dataReader["OpCoContactEmail"].ToString();
            shipment.OpCoContact.Name = dataReader["OpCoContactName"].ToString();

            // Populate shipment contact
            shipment.ShipmentContact.Email = dataReader["ShipmentContactEmail"].ToString();
            shipment.ShipmentContact.Name = dataReader["ShipmentContactName"].ToString();
            shipment.ShipmentContact.TelephoneNumber = dataReader["ShipmentContactTel"].ToString();

            // Populate shipment address
            shipment.ShipmentAddress.Line1 = dataReader["ShipmentAddress1"].ToString();
            shipment.ShipmentAddress.Line2 = dataReader["ShipmentAddress2"].ToString();
            shipment.ShipmentAddress.Line3 = dataReader["ShipmentAddress3"].ToString();
            shipment.ShipmentAddress.Line4 = dataReader["ShipmentAddress4"].ToString();
            shipment.ShipmentAddress.Line5 = dataReader["ShipmentAddress5"].ToString();
            shipment.ShipmentAddress.PostCode = dataReader["ShipmentPostCode"].ToString();

            // Populate customer address
            shipment.CustomerAddress.Line1 = dataReader["CustomerAddress1"].ToString();
            shipment.CustomerAddress.Line2 = dataReader["CustomerAddress2"].ToString();
            shipment.CustomerAddress.Line3 = dataReader["CustomerAddress3"].ToString();
            shipment.CustomerAddress.Line4 = dataReader["CustomerAddress4"].ToString();
            shipment.CustomerAddress.Line5 = dataReader["CustomerAddress5"].ToString();
            shipment.CustomerAddress.PostCode = dataReader["CustomerPostCode"].ToString();
        }
    }

    /// <summary>
    /// Summary Description for TDCShipmentController
    /// </summary>
    public static class TDCShipmentController
    {
        /// <summary>
        /// A class for shipment status
        /// </summary>
        public class ShipmentStatus
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="T:ShipmentStatus"/> class.
            /// </summary>
            /// <param name="code">The code.</param>
            /// <param name="value">The value.</param>
            public ShipmentStatus(string code, int value)
            {
                this.code = code;
                this.value = value;
            }

            private string code = "";

            /// <summary>
            /// Gets or sets the code.
            /// </summary>
            /// <value>The code.</value>
            public string Code
            {
                get { return code; }
                set { code = value; }
            }

            private int value = -1;

            /// <summary>
            /// Gets or sets the value.
            /// </summary>
            /// <value>The value.</value>
            public int Value
            {
                get { return value; }
                set { this.value = value; }
            }
        }

        /// <summary>
        /// A class for shipment type
        /// </summary>
        public class ShipmentType
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="T:ShipmentType"/> class.
            /// </summary>
            /// <param name="code">The code.</param>
            /// <param name="value">The value.</param>
            public ShipmentType(string code, int value)
            {
                this.code = code;
                this.value = value;
            }

            private string code = "";

            /// <summary>
            /// Gets or sets the code.
            /// </summary>
            /// <value>The code.</value>
            public string Code
            {
                get { return code; }
                set { code = value; }
            }

            private int value = -1;

            /// <summary>
            /// Gets or sets the value.
            /// </summary>
            /// <value>The value.</value>
            public int Value
            {
                get { return value; }
                set { this.value = value; }
            }
        }

        /// <summary>
        /// Gets the shipment statuses.
        /// </summary>
        /// <returns></returns>
        public static Hashtable GetShipmentTypeData(int Id)
        {
            // Create a hash table for the results
            Hashtable typeData = new Hashtable(10);

            // Read the values from the reader
            IDataReader readerTypeData  = DataAccessProvider.Instance().GetTDCShipmentTypeData(Id);

            try
            {
                // See if we found a shipment
                if (readerTypeData.Read())
                {
                    typeData.Add("StockWarehouseCode", readerTypeData["StockWarehouseCode"]);
                    typeData.Add("DeliveryWarehouseCode", readerTypeData["DeliveryWarehouseCode"]);
                    typeData.Add("TransactionTypeCode", readerTypeData["TransactionTypeCode"]);
                    typeData.Add("TransactionSubTypeCode", readerTypeData["TransactionSubTypeCode"]);
                    typeData.Add("RouteCode", readerTypeData["RouteCode"]);
                }
            }
            finally
            {
                // Done
                readerTypeData.Close();
            }

            // Done
            return typeData;
        }

        /// <summary>
        /// Gets the shipment statuses.
        /// </summary>
        /// <returns></returns>
        public static Shipment.StatusEnum GetShipmentStatus(int Id)
        {
            try
            {
                return (Shipment.StatusEnum)Enum.Parse(typeof(Shipment.StatusEnum), DataAccessProvider.Instance().GetTDCShipmentStatus(Id).ToString());
            }
            catch
            {
                // Not found, not mapped
                return Shipment.StatusEnum.NotMapped;
            }
        }

        /// <summary>
        /// Gets the shipment statuses.
        /// </summary>
        /// <returns></returns>
        public static List<ShipmentStatus> GetShipmentStatuses()
        {
            List<ShipmentStatus> shipmentStatuses = new List<ShipmentStatus>();

            // Append status codes
            string[] StatusNames = Enum.GetNames(typeof(Shipment.StatusEnum));
            Array StatusValues = Enum.GetValues(typeof(Shipment.StatusEnum));

            // Add text and values
            for (int i = 0; i < StatusNames.Length; i++)
            {
                shipmentStatuses.Add(new ShipmentStatus(StatusNames[i], (int)StatusValues.GetValue(i)));
            }

            // Done
            return shipmentStatuses;
        }

        /// <summary>
        /// Gets the shipment types.
        /// </summary>
        /// <returns></returns>
        public static List<ShipmentType> GetShipmentTypes()
        {
            List<ShipmentType> shipmentTypes = new List<ShipmentType>();

            // Append status codes
            string[] typeNames = Enum.GetNames(typeof(Shipment.TypeEnum));
            Array typeValues = Enum.GetValues(typeof(Shipment.TypeEnum));

            // Add text and values
            for (int i = 0; i < typeNames.Length; i++)
            {
                shipmentTypes.Add(new ShipmentType(typeNames[i], (int)typeValues.GetValue(i)));
            }
            // Done
            return shipmentTypes;
        }

        /// <summary>
        /// Gets the shipments.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="numRows">The num rows. -1 WILL BRING BACK ALL RECORDS</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fully populate].</param>
        /// <returns></returns>
        public static List<T> GetShipments<T>(
                    ShipmentCriteria criteria,
                    string sortExpression,
                    int pageIndex,
                    int numRows,
                    bool fullyPopulate) where T : TDCShipment, new()
        {
            return CBO<T>.FillCollection(
                        DataAccessProvider.Instance().GetTDCShipments(criteria, sortExpression, pageIndex, numRows),
                        CustomShipmentFill,
                        fullyPopulate);
        }

        public static List<TDCShipment> GetShipments(
                    ShipmentCriteria criteria,
                    string sortExpression,
                    int pageIndex,
                    int numRows,
                    bool fullyPopulate)
        {
            // Get the opco shipments from the db
            return GetShipments<TDCShipment>(criteria, sortExpression, pageIndex, numRows, fullyPopulate);
        }

        /// <summary>
        /// Gets the shipments count.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        public static int GetShipmentsCount(ShipmentCriteria criteria)
        {
            return DataAccessProvider.Instance().GetTDCShipmentsCount(criteria);
        }

        //required by datasource in generate optrak files screens
        /// <summary>
        /// Gets the shipments count.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="numRows">The num rows.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fully populate].</param>
        /// <returns></returns>
        public static int GetShipmentsCount(ShipmentCriteria criteria,
                           string sortExpression,
                           int pageIndex,
                           int numRows,
                           bool fullyPopulate)
        {
            return DataAccessProvider.Instance().GetTDCShipmentsCount(criteria);
        }

        /// <summary>
        /// Gets the shipments.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fully populate].</param>
        /// <returns></returns>
        public static List<TDCShipment> GetShipments(ShipmentCriteria criteria, string sortExpression, bool fullyPopulate)
        {
            return GetShipments(criteria, sortExpression, 0, -1, fullyPopulate);
        }

        /*
        public static List<TDCShipment> GetShipments(ShipmentCriteria criteria, string sortExpression, bool fullyPopulate)
        {
            // Get the opco shipments from the db
            return CBO<TDCShipment>.FillCollection(
                        DataAccessProvider.Instance().GetTDCShipments(criteria, sortExpression),
                        CustomShipmentFill,
                        fullyPopulate);
        }
         */


        /*
        public static int GetShipmentsCount(
              ShipmentCriteria shipmentCriteria,
              string sortExpression,
              int pageIndex,
              int numRows,
              bool fullyPopulate
          )
        {
            return GetShipmentsCount(shipmentCriteria);
        }
        */

        /// <summary>
        /// Saves the shipments.
        /// </summary>
        /// <param name="shipment">The shipment.</param>
        /// <returns></returns>
        public static List<int> SaveShipments(List<TDCShipment> shipment)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the trunked stock summary.
        /// </summary>
        /// <param name="deliveryWarehouseId">The delivery warehouse id.</param>
        /// <param name="requireDate">The require date.</param>
        /// <param name="routeCode">The route code.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        public static List<ShipmentTrunkedStockSummary> GetTrunkedStockSummary(int? deliveryWarehouseId, DateTime? requireDate,
                                                                    int? routeCode, string sortExpression)
        {
            List<ShipmentTrunkedStockSummary> summary = new List<ShipmentTrunkedStockSummary>();
            if (routeCode != null && deliveryWarehouseId != null && requireDate != null && requireDate != DateTime.MinValue && requireDate.Value.Year > 1900)
            {
                try
                {

                    summary =
                        CBO<ShipmentTrunkedStockSummary>.FillCollection(
                            DataAccessProvider.Instance().GetTrunkedStockSummary(deliveryWarehouseId.Value, requireDate.Value,
                                                                      routeCode.Value));


                }
                catch (Exception ex)
                {
                    if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
                }
            }
            if (string.IsNullOrEmpty(sortExpression))
            {
                sortExpression = "RouteCode,StockWarehouseCode";
            }
            summary.Sort(new UniversalComparer<ShipmentTrunkedStockSummary>(sortExpression));
            return summary;
        }


        /// <summary>
        /// Gets the shipment by route.
        /// </summary>
        /// <param name="deliveryWarehouseId">The delivery warehouse id.</param>
        /// <param name="requireDate">The require date.</param>
        /// <param name="includeSpecials">if set to <c>true</c> [include specials].</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        public static List<ShipmentSummaryByRoute> GetShipmentByRoute(int? deliveryWarehouseId, DateTime? requireDate,
                                                                      bool includeSpecials, string sortExpression)
        {
            List<ShipmentSummaryByRoute> summary = new List<ShipmentSummaryByRoute>();
            if (deliveryWarehouseId != null && requireDate != null && requireDate != DateTime.MinValue && requireDate.Value.Year > 1900)
            {
                try
                {
                    summary =
                        CBO<ShipmentSummaryByRoute>.FillCollection(
                             DataAccessProvider.Instance().GetTDCShipmentsByRoute(deliveryWarehouseId.Value, requireDate.Value,
                                                                      includeSpecials));

                }
                catch (Exception ex)
                {
                    if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
                }
            }
            if (string.IsNullOrEmpty(sortExpression))
            {
                sortExpression = "RouteCode,DeliveryLocation,RequiredShipmentDate";
            }
            summary.Sort(new UniversalComparer<ShipmentSummaryByRoute>(sortExpression));
            return summary;
        }


        /// <summary>
        /// Gets the shipment.
        /// </summary>
        /// <param name="opCo">The op co.</param>
        /// <param name="shipmentNumber">The shipment number.</param>
        /// <param name="despatchNumber">The despatch number.</param>
        /// <returns></returns>
        public static TDCShipment GetShipment(
                    string opCo,
                    string shipmentNumber,
                    string despatchNumber)
        {
            try
            {
                return CBO<TDCShipment>.FillObject(
                            DataAccessProvider.Instance().GetTDCShipment(opCo, shipmentNumber, despatchNumber),
                            CustomShipmentFill,
                            true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the shipment.
        /// </summary>
        /// <param name="shipmentId">The shipment id.</param>
        /// <returns></returns>
        public static TDCShipment GetShipment(int shipmentId)
        {
            try
            {
                return CBO<TDCShipment>.FillObject(
                            DataAccessProvider.Instance().GetTDCShipment(shipmentId),
                            CustomShipmentFill,
                            true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the trips.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        public static List<Trip> GetTrips(ShipmentCriteria criteria)
        {
            return CBO<Trip>.FillCollection(DataAccessProvider.Instance().GetTrips(criteria));
        }

        /// <summary>
        /// Gets the drops.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        public static List<ShipmentDrop> GetDrops(ShipmentCriteria criteria)
        {
            return CBO<ShipmentDrop>.FillCollection(DataAccessProvider.Instance().GetDrops(criteria));
        }


        public static void SaveShipmentsLocationCode(List<TDCShipment> shipments)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (TDCShipment tdcShipment in shipments)
                    {
                        if (!DataAccessProvider.Instance().SaveTDCShipmentLocationCode(tdcShipment))
                        {
                            throw new Exception(
                                string.Format("Failed to update the location code for TDC Shipment Id -'{0}'.",
                                              tdcShipment.Id));
                        }
                    }

                    if (Transaction.Current != null &&
                        Transaction.Current.TransactionInformation.Status == TransactionStatus.Active)
                    {
                        scope.Complete();
                    }

                }
            }
            catch (Exception e)
            {
                ExceptionPolicy.HandleException(e, "User Interface");
            }
        }

        /// <summary>
        /// Saves the shipment.
        /// </summary>
        /// <param name="shipment">The shipment.</param>
        /// <returns></returns>
        public static int SaveShipment(TDCShipment shipment)
        {
            try
            {
                // Make sure the shipment is valid
                if (shipment.IsValid)
                {
                    // Save the shipment to the db and update the id, this will update the shipment lines if required
                    shipment.Id = DataAccessProvider.Instance().SaveTDCShipment(shipment);

                    // Get the checksum for the entity
                    FrameworkController.GetChecksum(shipment, "Shipment");

                    // Save the shipment lines to the db
                    foreach (TDCShipmentLine tdcShipmentLine in shipment.ShipmentLines)
                    {
                        // Save the shipment line and update the shipment line id if required
                        tdcShipmentLine.Id = TDCShipmentLineController.SaveLine(tdcShipmentLine);
                    }
                }
                else
                {
                    // Entity is not valid
                    throw new InValidBusinessObjectException(shipment);
                }
            }
            catch (Exception ex)
            {
                // Generate a new exception
                ex = new Exception(
                        string.Format("Failed to save TDC shipment {0} - {1} for OpCo {2}.  {3}",
                        shipment.ShipmentNumber,
                        shipment.DespatchNumber,
                        shipment.OpCoCode,
                        ex.Message));

                // Log an throw if configured to do so
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw ex;

                // We failed to save the shipment
                return -1;
            }

            // Done
            return shipment.Id;
        }

        // Custom object hydration
        /// <summary>
        /// Customs the shipment fill.
        /// </summary>
        /// <param name="shipment">The shipment.</param>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fully populate].</param>
        public static void CustomShipmentFill(TDCShipment shipment, IDataReader dataReader, bool fullyPopulate)
        {
            // See if we fully populate this entity
            if (fullyPopulate)
            {
                // Populate shipment lines
                List<TDCShipmentLine> tdcLines = TDCShipmentLineController.GetLines(shipment.Id);
                // The total number of lines
                int TotalLines = tdcLines.Count;
                // Clear existing lines
                shipment.ShipmentLines.Clear();
                // Add new lines
                foreach (TDCShipmentLine tdcLine in tdcLines)
                {
                    // Specify any derived columns that we need
                    tdcLine.TotalLines = TotalLines;
                    // Add the line
                    shipment.ShipmentLines.Add(tdcLine);
                }
            }

            // Populate opco contact
            shipment.OpCoContact.Email = dataReader["OpCoContactEmail"].ToString();
            shipment.OpCoContact.Name = dataReader["OpCoContactName"].ToString();

            // Populate shipment contact
            shipment.ShipmentContact.Email = dataReader["ShipmentContactEmail"].ToString();
            shipment.ShipmentContact.Name = dataReader["ShipmentContactName"].ToString();
            shipment.ShipmentContact.TelephoneNumber = dataReader["ShipmentContactTel"].ToString();

            // Populate shipment address
            shipment.ShipmentAddress.Line1 = dataReader["ShipmentAddress1"].ToString();
            shipment.ShipmentAddress.Line2 = dataReader["ShipmentAddress2"].ToString();
            shipment.ShipmentAddress.Line3 = dataReader["ShipmentAddress3"].ToString();
            shipment.ShipmentAddress.Line4 = dataReader["ShipmentAddress4"].ToString();
            shipment.ShipmentAddress.Line5 = dataReader["ShipmentAddress5"].ToString();
            shipment.ShipmentAddress.PostCode = dataReader["ShipmentPostCode"].ToString();

            // Populate customer address
            shipment.CustomerAddress.Line1 = dataReader["CustomerAddress1"].ToString();
            shipment.CustomerAddress.Line2 = dataReader["CustomerAddress2"].ToString();
            shipment.CustomerAddress.Line3 = dataReader["CustomerAddress3"].ToString();
            shipment.CustomerAddress.Line4 = dataReader["CustomerAddress4"].ToString();
            shipment.CustomerAddress.Line5 = dataReader["CustomerAddress5"].ToString();
            shipment.CustomerAddress.PostCode = dataReader["CustomerPostCode"].ToString();

            // Populate paf address
            shipment.PAFAddress.DPS = dataReader["PAFDPS"].ToString();
            shipment.PAFAddress.Easting = Convert.ToInt32(dataReader["PAFEasting"].ToString());
            shipment.PAFAddress.Northing = Convert.ToInt32(dataReader["PAFNorthing"].ToString());
            shipment.PAFAddress.Line1 = dataReader["PAFAddress1"].ToString();
            shipment.PAFAddress.Line2 = dataReader["PAFAddress2"].ToString();
            shipment.PAFAddress.Line3 = dataReader["PAFAddress3"].ToString();
            shipment.PAFAddress.Line4 = dataReader["PAFAddress4"].ToString();
            shipment.PAFAddress.Line5 = dataReader["PAFAddress5"].ToString();
            shipment.PAFAddress.PostCode = dataReader["PAFPostCode"].ToString();
            shipment.PAFAddress.Location = Convert.ToInt32(dataReader["PAFLocation"].ToString());
            shipment.PAFAddress.Match = dataReader["PAFMatch"].ToString();
            //shipment.PAFAddress.Status = (PAFAddress.PAFStatusEnum)Enum.Parse(typeof(PAFAddress.PAFStatusEnum), dataReader["PAFStatus"].ToString());
        }

        /// <summary>
        /// Deletes the shipment.
        /// </summary>
        /// <param name="shipment">The TDC shipment.</param>
        /// <returns></returns>
        public static bool DeleteShipment(TDCShipment shipment)
        {
            return DeleteShipment(shipment.Id);
        }

        /// <summary>
        /// Deletes the shipment.
        /// </summary>
        /// <param name="shipmentId">The shipment id.</param>
        /// <returns></returns>
        public static bool DeleteShipment(int shipmentId)
        {
            return DataAccessProvider.Instance().DeleteTDCShipment(shipmentId);
        }

        /// <summary>
        /// Gets the delivery locations.
        /// </summary>
        /// <returns></returns>
        public static List<SalesLocation> GetDeliveryLocations()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the stock locations.
        /// </summary>
        /// <returns></returns>
        public static List<SalesLocation> GetStockLocations()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the route codes.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetRouteCodes()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the deliveries.
        /// </summary>
        /// <returns></returns>
        public static List<TDCShipment> GetDeliveries()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the collections.
        /// </summary>
        /// <returns></returns>
        public static List<TDCShipment> GetCollections()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the transfers.
        /// </summary>
        /// <returns></returns>
        public static List<TDCShipment> GetTransfers()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Merges the shipment.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="shipmentId">The shipment id.</param>
        public static void MergeShipment(string location, int shipmentId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Merges the shipments.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="shipmentIds">The shipment ids.</param>
        public static void MergeShipments(string location, long[] shipmentIds)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the merged shipments by location.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <returns></returns>
        public static List<TDCShipment> GetMergedShipmentsByLocation(string location)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Increments the print count.
        /// </summary>
        /// <param name="tdcShipment">The TDC shipment.</param>
        /// <returns></returns>
        internal static int IncrementPrintCount(TDCShipment tdcShipment)
        {
            return 1;
        }
        //public static void UpdateShipmentStatus(OpCoShipment shipment)
        //{
        //    try
        //    {
        //        // Get an instance of the data access provider
        //        DataAccessProvider dataAccessProvider = DataAccessProvider.Instance();

        //        // Update the status of the opco shipment in the db
        //        dataAccessProvider.UpdateOpCoShipmentStatus(shipment);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log an throw if configured to do so
        //        if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
        //    }
        //}

        /// <summary>
        /// Updates the status for all shipments, if any fail then the transaction is rolled back.
        /// </summary>
        /// <param name="shipments">The shipments.</param>
        /// <param name="newStatus">The new status.</param>
        /// <param name="updatedBy">The updated by.</param>
        /// <returns></returns>
        public static bool UpdateShipmentStatus(List<TDCShipment> shipments, Shipment.StatusEnum newStatus, string updatedBy)
        {

            using (TransactionScope scope = new TransactionScope())
            {
                foreach (TDCShipment shipment in shipments)
                {
                    if (!UpdateShipmentStatus(shipment, newStatus, updatedBy))
                    {
                        return false;
                    }
                }
                if (Transaction.Current != null &&
                   Transaction.Current.TransactionInformation.Status == TransactionStatus.Active)
                {
                    scope.Complete();
                }
            }
            return true;
        }



        /// <summary>
        /// Updates the shipment status if business rules allow it.
        /// </summary>
        /// <param name="shipmentToUpdate">The shipment to update.</param>
        /// <param name="newStatus">The new status.</param>
        /// <param name="updatedBy">The updated by.</param>
        /// <returns></returns>
        public static bool UpdateShipmentStatus(TDCShipment shipmentToUpdate, Shipment.StatusEnum newStatus, string updatedBy)
        {
            bool success = false;
            try
            {
                bool update = false;
                switch (newStatus)
                {
                    case Shipment.StatusEnum.Mapped:
                        {
                            if (shipmentToUpdate.Status == Shipment.StatusEnum.Routing)
                            {
                                update = true;
                            }
                            break;
                        }

                    case Shipment.StatusEnum.Routing:
                        {
                            if (shipmentToUpdate.Status == Shipment.StatusEnum.Mapped)
                            {
                                update = true;
                            }
                            break;
                        }

                    default:
                        {
                            throw new Exception(
                                string.Format("Cannot change status from {0} to {1}, it breaks business rules", shipmentToUpdate.Status.ToString(),
                                              newStatus.ToString()));

                        }
                }

                if (update)
                {
                    // Update the status of the shipment in the db
                    success = DataAccessProvider.Instance().UpdateTDCShipmentStatus(shipmentToUpdate, newStatus, updatedBy);
                    if (!success)
                    {
                        throw new Exception("Failed to Update Status.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log an throw if configured to do so
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return success;
        }

        // *************************************************************
        // **
        // ** Printing Support
        // **
        // *************************************************************

        /// <summary>
        /// Prints the specified shipments.
        /// </summary>
        /// <param name="shipments">The shipments.</param>
        /// <param name="numberOfCopies">The number of copies.</param>
        public static void Print(
                    List<TDCShipmentPrintable> shipments,
                    int numberOfCopies,
                    TDCShipment.ReportTypeEnum reportType)
        {
            try
            {
                // Make sure we have something to do
                if (shipments != null && numberOfCopies > 0)
                {
                    // Create the report instance
                    ReportDocument theReport = null;

                    // Our list of one shipment for data binding
                    List<TDCShipmentPrintable> printableShipment = new List<TDCShipmentPrintable>(1);

                    // Warehouse code where we are trying to print to
                    string printerWarehouse = "";

                    // Load the correct crystal report
                    switch (reportType)
                    {
                        case TDCShipment.ReportTypeEnum.DeliveryNote:
                            {
                                // Create an instance of the report
                                theReport = new DeliveryNote();
                                // Done
                                break;
                            }
                        case TDCShipment.ReportTypeEnum.ConversionNote:
                        case TDCShipment.ReportTypeEnum.TransferNote:
                        case TDCShipment.ReportTypeEnum.ExBranchSalesOrder:
                            {
                                // Create an instance of the report
                                theReport = new DeliveryNote();
                                // Done 
                                break;
                            }
                    }

                    // Specify report options
                    theReport.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
                    theReport.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;

                    // Iterate over the shipments and print (this is the order they are in the collection)
                    foreach (TDCShipmentPrintable shipment in shipments)
                    {
                        // Clear the list of shipments
                        printableShipment.Clear();

                        // Add the shipment to the list
                        printableShipment.Add(shipment);

                        // Bind main shipment data
                        theReport.SetDataSource(printableShipment);

                        // Bind the shipment lines data
                        theReport.Subreports[0].SetDataSource(TDCShipmentLineController.GetLines(shipment.Id));

                        // Specify the correct printer
                        switch (reportType)
                        {
                            case TDCShipment.ReportTypeEnum.DeliveryNote:
                                {
                                    // Store the warehouse code
                                    printerWarehouse = shipment.DeliveryWarehouseCode;
                                    // Specify the printer name/ip
                                    theReport.PrintOptions.PrinterName = shipment.DeliveryWarehouse.PrinterName;
                                    // Specify the report title
                                    theReport.SetParameterValue("reportTitle", "Delivery Note");
                                    // Done
                                    break;
                                }
                            case TDCShipment.ReportTypeEnum.ConversionNote:
                                {
                                    // Store the warehouse code
                                    printerWarehouse = shipment.StockWarehouseCode;
                                    // Specify the printer name/ip
                                    theReport.PrintOptions.PrinterName = shipment.StockWarehouse.PrinterName;
                                    // Specify the report title
                                    theReport.SetParameterValue("reportTitle", "Conversion Note");
                                    // Done 
                                    break;
                                }
                            case TDCShipment.ReportTypeEnum.TransferNote:
                                {
                                    // Store the warehouse code
                                    printerWarehouse = shipment.StockWarehouseCode;
                                    // Specify the printer name/ip
                                    theReport.PrintOptions.PrinterName = shipment.StockWarehouse.PrinterName;
                                    // Specify the report title
                                    theReport.SetParameterValue("reportTitle", "Transfer Note");
                                    // Done 
                                    break;
                                }
                            case TDCShipment.ReportTypeEnum.ExBranchSalesOrder:
                                {
                                    // Store the warehouse code
                                    printerWarehouse = shipment.StockWarehouseCode;
                                    // Specify the printer name/ip
                                    theReport.PrintOptions.PrinterName = shipment.StockWarehouse.PrinterName;
                                    // Specify the report title
                                    theReport.SetParameterValue("reportTitle", "Ex-Branch Sales Order");
                                    // Done 
                                    break;
                                }
                        }

                        // Make sure we have a printer name
                        if (!string.IsNullOrEmpty(theReport.PrintOptions.PrinterName))
                        {
                            // Print the delivery note
                            theReport.PrintToPrinter(numberOfCopies, true, 0, 0);
                        }
                        else
                        {
                            // No printer for the specified warehouse
                            throw new Exception(string.Format("The warehouse '{0}' does not have a printer specified."));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log an throw if configured to do so (should not by default)
                ExceptionPolicy.HandleException(ex, "Printing");
            }
        }

        /// <summary>
        /// Prints the specified shipment.
        /// </summary>
        /// <param name="shipment">The shipment.</param>
        /// <param name="numberOfCopies">The number of copies.</param>
        public static void Print(
                    TDCShipmentPrintable shipment,
                    int numberOfCopies,
                    TDCShipment.ReportTypeEnum reportType)
        {
            // Create a list of shipments
            List<TDCShipmentPrintable> shipments = new List<TDCShipmentPrintable>();

            // Add the shipment to the list
            shipments.Add(shipment);

            // Print the shipment
            Print(shipments, numberOfCopies, reportType);
        }

        /// <summary>
        /// Prints the specified shipment.
        /// </summary>
        /// <param name="shipment">The shipment.</param>
        /// <param name="numberOfCopies">The number of copies.</param>
        public static void Print(
                    TDCShipment shipment,
                    int numberOfCopies,
                    TDCShipment.ReportTypeEnum reportType)
        {
            // Print the shipment
            TDCShipmentPrintable printableShipment = new TDCShipmentPrintable();

            // Map this shipment to a printable one, we could have gone off the the DB
            Mapper.Map(shipment, printableShipment, "NA", "NA", null, null);

            // Print the shipment
            Print(printableShipment, numberOfCopies, reportType);
        }

        /// <summary>
        /// Prints the specified shipment via id.
        /// </summary>
        /// <param name="shipmentid">The shipment id.</param>
        /// <param name="numberOfCopies">The number of copies.</param>
        public static void Print(
                    int shipmentId,
                    int numberOfCopies,
                    TDCShipment.ReportTypeEnum reportType)
        {
            // Load the shipment and print
            Print(GetShipment(shipmentId), numberOfCopies, reportType);
        }
    }
}