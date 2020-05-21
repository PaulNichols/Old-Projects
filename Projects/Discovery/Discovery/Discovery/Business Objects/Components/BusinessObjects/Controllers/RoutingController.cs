using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Transactions;
using Discovery.ComponentServices.DataAccess;
using Discovery.ComponentServices.Parsing;
using Discovery.RequestManagerClient;
using Discovery.Utility;
using Discovery.Utility.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Discovery.BusinessObjects.Controllers
{
    /// <summary>
    /// A class to provide the routing controller which is a business object controller
    /// with namespace Discovery.BusinessObjects.Controllers
    /// </summary>
    public static class RoutingController
    {

        public enum PeriodEnum
        {
            SameDay,
            NextDay
        }

        /// <summary>
        /// Gets the a list of warehouses that have optrak.
        /// </summary>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        public static List<Warehouse> GetOptrakWarehouses(string sortExpression)
        {
            List<Warehouse> warehouses;
            List<Warehouse> optrakWarehouses = new List<Warehouse>();
            try
            {
                warehouses = CBO<Warehouse>.FillCollection(DataAccessProvider.Instance().GetWarehouses());

                foreach (Warehouse warehouse in warehouses)
                {
                    if (warehouse.HasOptrak)
                    {
                        optrakWarehouses.Add(warehouse);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            if (string.IsNullOrEmpty(sortExpression))
            {
                sortExpression = "Code,Description";
            }

            optrakWarehouses.Sort(new UniversalComparer<Warehouse>(sortExpression));

            return optrakWarehouses;
        }

        ///// <summary>
        ///// Gets the lock details for the specified warehouse id 
        ///// </summary>
        ///// <returns></returns>
        //public static List<RoutingLockDetail> GetOptrakLocks(string sortExpression)
        //{
        //    List<RoutingLockDetail> locks = new List<RoutingLockDetail>();
        //    try
        //    {
        //        locks =
        //            CBO<RoutingLockDetail>.FillCollection(DataAccessProvider.Instance().GetOptrakLocks(),
        //                                                  CustomFillForOptrakLocks, true);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
        //    }
        //    return locks;
        //}

        /// <summary>
        /// Gets the merged shipments count.
        /// </summary>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <param name="routingHistoryId">The routing history id.</param>
        /// <returns></returns>
        public static Int32 GetMergedShipmentsCount(string sortExpression, int startRowIndex, int maximumRows,
                                                    int routingHistoryId)
        {
            return count;
        }

        /// <summary>
        /// Deletes the shipment from routing.
        /// </summary>
        /// <param name="shipmentId">The shipment id.</param>
        /// <param name="routingHistoryId">The routing history id.</param>
        /// <returns></returns>
        private static bool DeleteShipmentFromRouting(int shipmentId, int routingHistoryId)
        {
            bool success = false;
            try
            {
                success = DataAccessProvider.Instance().DeleteShipmentFromRouting(shipmentId, routingHistoryId);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return success;
        }

        /// <summary>
        /// Gets the grouped details of any merged shipments.
        /// </summary>
        /// <param name="routingHistoryId">The routing history id.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows. -1 will return all</param>
        /// <returns></returns>
        public static List<MergedShipment> GetMergedShipments(int routingHistoryId, string sortExpression,
                                                              int startRowIndex,
                                                              int maximumRows)
        {
            int totalRows = 0;
            List<MergedShipment> mergedShipments = new List<MergedShipment>();
            try
            {
                int rows;
                mergedShipments =
                    CBO<MergedShipment>.FillCollection(
                        DataAccessProvider.Instance().GetMergedShipments(routingHistoryId, sortExpression, startRowIndex,
                                                                         maximumRows, out rows), null, false);
                totalRows = rows;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            count = totalRows;
            return mergedShipments;
        }

        private static int count;

        //private static void CustomFillForOptrakLocks(RoutingLockDetail routingDetail, IDataReader dataReader,
        //                                            bool fullyPopulate)
        //{
        //    if (routingDetail != null)
        //    {
        //        routingDetail.Warehouse = new Warehouse();
        //        routingDetail.Warehouse.Description = dataReader["WarehouseDescription"].ToString();
        //        routingDetail.Warehouse.Code = dataReader["WarehouseCode"].ToString();
        //    }
        //}

        private const string delimiter = "|";
        private const string textQualifier = "";

        /// <summary>
        ///// Used for threading
        ///// </summary>
        ///// <param name="shipments">The shipments.</param>
        ///// <returns>A list of TDCShipmens, each shipment holds the values used in a line of the cites file</returns>
        //private static void GenerateSites(object shipments)
        //{
        //    GenerateSites((List<TDCShipment>)shipments);
        //}
        /// <summary>
        /// Generates the sites.
        /// </summary>
        /// <param name="shipments">The shipments.</param>
        /// <returns>A list of TDCShipmens, each shipment holds the values used in a line of the cites file</returns>
        public static List<string> GenerateSites(List<TDCShipment> shipments)
        {
            List<string> fileLines = new List<string>();

            //add column headers line?
            if (shipments != null)
            {
                //set the schema for the sites file
                TextFieldCollection siteSchema = new TextFieldCollection();

                siteSchema.Add(new TextField("SiteCode", TypeCode.String, 16));
                siteSchema.Add(new TextField("OpenTime", TypeCode.String, 2));
                siteSchema.Add(new TextField("MaximumGrossWeight", TypeCode.String));
                siteSchema.Add(new TextField("CheckInDuration", TypeCode.String)); 
                siteSchema.Add(new TextField("LoadMethod", TypeCode.String, 4));
                siteSchema.Add(new TextField("UnloadMethod", TypeCode.String, 6));
                siteSchema.Add(new TextField("DefaultDepot", TypeCode.String, 18));
                siteSchema.Add(new TextField("Name", TypeCode.String, 32));
                siteSchema.Add(new TextField("Address1", TypeCode.String, 32));
                siteSchema.Add(new TextField("Address2", TypeCode.String, 32));
                siteSchema.Add(new TextField("Address3", TypeCode.String, 32));
                siteSchema.Add(new TextField("Town", TypeCode.String, 32));
                siteSchema.Add(new TextField("Region", TypeCode.String, 32));
                siteSchema.Add(new TextField("Postcode", TypeCode.String, 9));
                siteSchema.Add(new TextField("X", TypeCode.String));
                siteSchema.Add(new TextField("Y", TypeCode.String));
                siteSchema.Add(new TextField("Telephone", TypeCode.String, 15));
                siteSchema.Add(new TextField("RHRegion", TypeCode.String, 1));
                siteSchema.Add(new TextField("Route", TypeCode.String, 20));
                siteSchema.Add(new TextField("Category", TypeCode.String, 1));
                siteSchema.Add(new TextField("TailLiftRequired", TypeCode.String, 1));
                siteSchema.Add(new TextField("OriginalX", TypeCode.String));
                siteSchema.Add(new TextField("OriginalY", TypeCode.String));

                //find just the unique cites
                Dictionary<string, TDCShipment> uniqueSites = new Dictionary<string, TDCShipment>();

                //the shipments have been ordered by primary key ("id") this means
                //that the first shipment name and address we come across for shipments 
                //grouped by sitecode will be the address we use?
                foreach (TDCShipment shipment in shipments)
                {
                    string siteCode = shipment.LocationCode;
                    if (!uniqueSites.ContainsKey(siteCode))
                    {
                        uniqueSites.Add(siteCode, shipment);
                    }

                    //if opcos differ
                    if (uniqueSites[siteCode].OpCoCode != shipment.OpCoCode)
                    {
                        //maximum gross weight, use smallest if opcos differ
                        //we cannot set gross weight as it is derived from the lines collection, so lets set the shipment in the
                        //unique collection to have the same lines as the shipment with the smaller gross weight
                        if (shipment.GrossWeight < uniqueSites[siteCode].GrossWeight)
                        {
                            uniqueSites[siteCode].ShipmentLines = shipment.ShipmentLines;
                        }
                        //check in time should be highest is opcos differ
                        if (shipment.CheckInTime > uniqueSites[siteCode].CheckInTime)
                        {
                            uniqueSites[siteCode].CheckInTime = shipment.CheckInTime;
                        }

                        //tail lift required should be true is opcos differ
                        if (uniqueSites[siteCode].TailLiftRequired != shipment.TailLiftRequired)
                        {
                            uniqueSites[siteCode].TailLiftRequired = true;
                        }
                    }
                }


                AddHeader(siteSchema, fileLines);

                foreach (TDCShipment shipment in uniqueSites.Values)
                {
                    shipment.RoutingPopulateSiteFields(siteSchema);

                    BuildLine(fileLines, siteSchema);
                }
            }
            return fileLines;
        }

        /// <summary>
        /// Builds the file and send.
        /// </summary>
        /// <param name="fileLines">The file lines.</param>
        /// <param name="regionCode">The destination warehouse code.</param>
        /// <param name="requestMessageName">Name of the request message.</param>
        private static void BuildFileAndSend(List<string> fileLines, string regionCode, string requestMessageName)
        {
            string fileContent = string.Join(((char)13).ToString(), fileLines.ToArray());

            RequestManagerClientMSMQ requestManagerClientMSMQ = new RequestManagerClientMSMQ();

            ////set the queue
            requestManagerClientMSMQ.QueueName = ConfigurationManager.AppSettings["OptrakOutQueueName"];

            //create a request message and set the body (Header, message and Footer)
            RequestMessage requestMessage = new RequestMessage(fileContent);
            requestMessage.DestinationSystem = regionCode;
            requestMessage.SourceSystem = "MS";
            requestMessage.Type = "Optrak";
            requestMessage.Name = requestMessageName;

            //send message
            requestManagerClientMSMQ.Send(requestMessage);
            // File.WriteAllText(string.Concat("c:\\temp\\", requestMessageName), fileContent);
        }

        ///// <summary>
        ///// Used for threading
        ///// </summary>
        ///// <param name="shipments">The shipments.</param>
        //private static void GenerateShipments(object shipments)
        //{
        //    GenerateShipments((List<TDCShipment>)shipments);
        //}

        /// <summary>
        /// Generates the shipments.
        /// </summary>
        /// <param name="shipments">The shipments.</param>
        /// <param name="regionCode">The region code.</param>
        /// <returns></returns>
        public static List<string> GenerateShipments(List<TDCShipment> shipments, string regionCode)
        {
            List<string> fileLines = new List<string>();

            //add column headers line?
            if (shipments != null)
            {
                //set the schema for the sites file
                TextFieldCollection shipmentSchema = new TextFieldCollection();

                shipmentSchema.Add(new TextField("OrderCode", TypeCode.String, 20));
                shipmentSchema.Add(new TextField("CollectAfter", TypeCode.String, 14));
                shipmentSchema.Add(new TextField("CollectBefore", TypeCode.String, 14));
                shipmentSchema.Add(new TextField("DeliverAfter", TypeCode.String, 14));
                shipmentSchema.Add(new TextField("DeliverBefore", TypeCode.String, 14));
                shipmentSchema.Add(new TextField("CollectFrom", TypeCode.String, 18));
                shipmentSchema.Add(new TextField("DeliverTo", TypeCode.String, 18));
                shipmentSchema.Add(new TextField("Sample", TypeCode.String, 1));
                shipmentSchema.Add(new TextField("JP2", TypeCode.String, 2));
                shipmentSchema.Add(new TextField("TrampingNo", TypeCode.String, 7));
                shipmentSchema.Add(new TextField("OrigCustCode", TypeCode.String, 16));
                shipmentSchema.Add(new TextField("AssocDepot", TypeCode.String, 18));
                shipmentSchema.Add(new TextField("CustReturn", TypeCode.String, 1));
                shipmentSchema.Add(new TextField("TimeCode", TypeCode.String, 2)); //RHTimecode

                //lets load all route and transaction type objects now in one hit rather than each time the 
                //RoutingPopulateShipmentFields needs the full object
                List<Route> allRoutes = RouteController.GetRoutes("", 0, 0, false);
                List<TransactionType> allTransactionTypes = TransactionTypeController.GetTransactionTypes();
                List<NonWorkingDay> nonWorkingDaysForRegion = NonWorkingDayController.GetNonWorkingDaysByRegion(DateTime.Today,
                                                                                       DateTime.Today.AddMonths(1),
                                                                                       OptrakRegionController.GetRegion(regionCode).Id);



                AddHeader(shipmentSchema, fileLines);

                DateTime nextWorkingDate = new DateTime();

                for (int i = 0; i < shipments.Count; i++)
                {
                    //find the next working date for the warhouse, this will be outputed in the collection before/after or the delivery 
                    //before/after fields. We only need to work this out is the warhouse is different to the last shipment or this is the first time 
                    //round the loop
                    if (i==0 || shipments[i-1].DeliveryWarehouseCode!=shipments[i].DeliveryWarehouseCode)
                    {
                        if (nonWorkingDaysForRegion != null)
                            nextWorkingDate =
                                NonWorkingDayController.NextWorkingDate(DateTime.Today, nonWorkingDaysForRegion.FindAll(
                                        delegate(NonWorkingDay obj)
                                            {
                                                return obj.WarehouseCode ==shipments[i].DeliveryWarehouseCode;
                                            }));
                    }
                    shipments[i].RoutingPopulateShipmentFields(shipmentSchema, allRoutes, allTransactionTypes, nextWorkingDate);

                    BuildLine(fileLines, shipmentSchema);
                }
            }
            return fileLines;
        }

        /// <summary>
        /// Builds the line.
        /// </summary>
        /// <param name="fileLines">The file lines.</param>
        /// <param name="shipmentSchema">The shipment schema.</param>
        private static void BuildLine(List<string> fileLines,
                                      TextFieldCollection shipmentSchema)
        {
            List<string> fieldValues = new List<string>();
            foreach (TextField field in shipmentSchema)
            {
                if (field.Length > 0 && field.Value.ToString().Length > field.Length)
                {
                    //truncate the data to fit the defined schema and remove all extra delimiter charaters in the values
                    fieldValues.Add(field.Value.ToString().Substring(0, field.Length).Replace(delimiter, ""));
                }
                else
                {
                    //probably an integer
                    fieldValues.Add(field.Value.ToString());
                }
            }
            fileLines.Add(string.Join(delimiter, fieldValues.ToArray()));
        }

        ///// <summary>
        ///// Used for threading
        ///// </summary>
        ///// <param name="shipments">The shipments.</param>
        //private static void GenerateShipmentLines(object shipments)
        //{
        //    GenerateShipmentLines((List<TDCShipment>)shipments);
        //}

        /// <summary>
        /// Generates the shipment lines.
        /// </summary>
        /// <param name="shipments">The shipments.</param>
        public static List<string> GenerateShipmentLines(List<TDCShipment> shipments)
        {
            List<string> fileLines = new List<string>();

            //add column headers line?
            if (shipments != null)
            {
                //set the schema for the sites file
                TextFieldCollection shipmentLineSchema = new TextFieldCollection();

                shipmentLineSchema.Add(new TextField("Order", TypeCode.String, 20));
                shipmentLineSchema.Add(new TextField("LineCode", TypeCode.String, 16));
                shipmentLineSchema.Add(new TextField("Product", TypeCode.String, 32));
                shipmentLineSchema.Add(new TextField("MaximumUnits", TypeCode.Int32));
                shipmentLineSchema.Add(new TextField("MaximumWeight", TypeCode.String, 10));
                shipmentLineSchema.Add(new TextField("MaximumVolume", TypeCode.String, 8));
                shipmentLineSchema.Add(new TextField("Width", TypeCode.Int32));
                shipmentLineSchema.Add(new TextField("Length", TypeCode.Int32));

                AddHeader(shipmentLineSchema, fileLines);

                foreach (TDCShipment shipment in shipments)
                {
                    foreach (TDCShipmentLine shipmentLine in shipment.ShipmentLines)
                    {
                        shipmentLine.RoutingPopulateShipmentLineFields(shipmentLineSchema, shipment);
                        BuildLine(fileLines, shipmentLineSchema);
                    }
                }
            }
            return fileLines;
        }

        ///// <summary>
        ///// Generates the products. Used for threading
        ///// </summary>
        ///// <param name="shipments">The shipments.</param>
        //private static void GenerateProducts(object shipments)
        //{
        //    GenerateProducts((List<TDCShipment>)shipments);
        //}

        /// <summary>
        /// Generates the products.
        /// </summary>
        /// <param name="shipments">The shipments.</param>
        public static List<string> GenerateProducts(List<TDCShipment> shipments)
        {
            List<string> fileLines = new List<string>();

            //add column headers line?
            if (shipments != null)
            {
                //set the schema for the sites file
                TextFieldCollection productSchema = new TextFieldCollection();

                productSchema.Add(new TextField("Name", TypeCode.String, 32));
                productSchema.Add(new TextField("Description", TypeCode.String, 32));
                productSchema.Add(new TextField("LoadCategory", TypeCode.String, 32));
                productSchema.Add(new TextField("TypeData", TypeCode.String, 1));
                productSchema.Add(new TextField("ProductGroup", TypeCode.String, 20));
                productSchema.Add(new TextField("Width", TypeCode.Int32));
                productSchema.Add(new TextField("Length", TypeCode.Int32));

                SortedList<string, TDCShipmentLine> uniqueOpCoProducts = new SortedList<string, TDCShipmentLine>();

                AddHeader(productSchema, fileLines);

                TDCShipment currentshipment = null;
                TDCShipmentLine currentshipmentLine = null;
                try
                {
                    //get a unique list of products and their related shipment line which can be use to get the other details from
                    //the list will be sorted by product code
                    foreach (TDCShipment shipment in shipments)
                    {
                        currentshipment = shipment;
                        foreach (TDCShipmentLine shipmentLine in shipment.ShipmentLines)
                        {
                            currentshipmentLine = shipmentLine;
                            string key = string.Concat(shipmentLine.ProductCode, "|", shipment.OpCoCode);
                            if (!uniqueOpCoProducts.ContainsKey(key))
                            {
                                uniqueOpCoProducts.Add(key, shipmentLine);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                //take the unique products and build the lines that will make up the content of the file
                foreach (KeyValuePair<string, TDCShipmentLine> uniqueProductDetails in uniqueOpCoProducts)
                {
                    uniqueProductDetails.Value.RoutingPopulateProductFields(productSchema);
                    BuildLine(fileLines, productSchema);
                }
            }
            return fileLines;
        }

        /// <summary>
        /// Adds the header line to the filelines list using the field name property of the schema.
        /// </summary>
        /// <param name="schema">The schema.</param>
        /// <param name="fileLines">The file lines.</param>
        private static void AddHeader(TextFieldCollection schema, List<string> fileLines)
        {
            List<string> fieldValues = new List<string>();
            foreach (TextField field in schema)
            {
                fieldValues.Add(field.Name);
            }
            fileLines.Add(string.Join(delimiter, fieldValues.ToArray()));
        }

        /// <summary>
        /// Resets an Optrak "lock". Get's all the shipments for the specified Routing History Id 
        /// and then changes their status back to Mapped which will allow them to be Routed again.
        /// The Date and time and user will be recorded against the history record when this operation is performed.
        /// </summary>
        /// <param name="routingHistoryId">The routing history id.</param>
        /// <param name="updatedBy">The updated by.</param>
        /// <returns></returns>
        public static bool ResetLock(int routingHistoryId, string updatedBy)
        {
            // RoutingHistory routingHistory = GetRoutingHistory(routingHistoryId);
            //  if (routingHistory == null)
            //  {
            //      return false;
            //  }
            ////  List<TDCShipment> shipmentsToReset = GetShipmentsByRoutingHistoryId(routingHistoryId, false);
            //  using (TransactionScope scope = new TransactionScope())
            //  {
            //change status for all locked shipments to mapped from routing
            return DataAccessProvider.Instance().ResetRoutingLocks(routingHistoryId, updatedBy);

            //{
            //    return false;
            //}

            //routingHistory.ResetDate = DateTime.Now;
            //routingHistory.ResetBy = updatedBy;

            //if (SaveRoutingHistory(routingHistory) == -1)
            //{
            //    return false;
            //}

            //if (Transaction.Current != null &&
            //    Transaction.Current.TransactionInformation.Status == TransactionStatus.Active)
            //{
            //    scope.Complete();
            //}
            // }

            // return true;
        }

        /// <summary>
        /// Gets the routing history.
        /// </summary>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        public static List<RoutingHistory> GetRoutingHistory(string sortExpression, RoutingHistory.StatusEnum status)
        {
            List<RoutingHistory> routingHistory = new List<RoutingHistory>();
            try
            {
                routingHistory = CBO<RoutingHistory>.FillCollection(DataAccessProvider.Instance().GetRoutingHistory());
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            if (!string.IsNullOrEmpty(sortExpression))
            {
                routingHistory.Sort(new UniversalComparer<RoutingHistory>(sortExpression));
            }

            if (status == RoutingHistory.StatusEnum.None)
            {
                return routingHistory;
            }
            else
            {
                return routingHistory.FindAll
                    (
                    delegate(RoutingHistory currentRoutingHistory) { return (currentRoutingHistory.Status == status); }
                    );
            }
        }

        /// <summary>
        /// Gets the routing history.
        /// </summary>
        /// <param name="routingHistoryId">The routing history id.</param>
        /// <returns></returns>
        public static RoutingHistory GetRoutingHistory(int routingHistoryId)
        {
            RoutingHistory routingHistory = null;
            try
            {
                routingHistory =
                    CBO<RoutingHistory>.FillObject(DataAccessProvider.Instance().GetRoutingHistory(routingHistoryId));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return routingHistory;
        }

        #region Merge Delivery Points

        /// <summary>
        /// Merges the delivery points automatically.
        /// </summary>
        /// <param name="routingHistoryId">The routing history id.</param>
        /// <returns></returns>
        public static bool MergeDeliveryPointsAutomatically(int routingHistoryId)
        {
            //get the group of shipments to merge
            List<TDCShipment> shipmentsToMerge = GetShipmentsByRoutingHistoryId(routingHistoryId, false);
            //sort into the correct order for the merging process

            shipmentsToMerge.Sort(
                new UniversalComparer<TDCShipment>(
                    "DeliveryWarehouseCode,PAFAddress.DPS,PAFAddress.PostCode,ShipmentName"));


            string lastShipCode = "";
            string lastDPS = "";
            string lastShipmentName = "";
            int deliveryPoint = 0;
            string shipCode = "";

            foreach (TDCShipment shipment in shipmentsToMerge)
            {
                if (string.IsNullOrEmpty(shipment.PAFAddress.DPS))
                {
                    //we have no DPS
                    shipCode = StripChars(string.Concat(shipment.PAFAddress.PostCode, '-', shipment.DeliveryWarehouseCode));

                    if (!string.IsNullOrEmpty(lastShipmentName))
                    {
                        if (shipCode == StripChars(lastShipCode))
                        {
                            int confidenceLevel;
                            confidenceLevel = CompareStrings(shipment.ShipmentName, lastShipmentName);
                            if (confidenceLevel < 62) deliveryPoint++;
                        }
                        else
                        {
                            deliveryPoint++;
                        }
                    }
                    lastShipmentName = shipment.ShipmentName;
                    lastShipCode = shipCode;
                }
                else
                {
                    //we have a DPS
                    if ((!string.IsNullOrEmpty(lastDPS) && lastDPS != shipment.PAFAddress.DPS) ||
                        !string.IsNullOrEmpty(lastShipCode) && lastShipCode != shipment.PAFAddress.PostCode)
                    {
                        deliveryPoint++;
                    }
                    lastShipCode = shipment.PAFAddress.PostCode;
                    lastDPS = shipment.PAFAddress.DPS;
                }
                //update the location code/delivery point number
                shipment.LocationCode = deliveryPoint.ToString();
            }


            TDCShipmentController.SaveShipmentsLocationCode(shipmentsToMerge);


            return true;
        }

        /// <summary>
        /// Gets the shipments by routing history id count.
        /// </summary>
        /// <param name="routingHistoryId">The routing history id.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fully populate].</param>
        /// <returns></returns>
        public static Int32 GetShipmentsByRoutingHistoryIdCount(int routingHistoryId, bool fullyPopulate)
        {
            return GetShipmentsByRoutingHistoryIdCount(routingHistoryId, "Id", 0, -1, fullyPopulate);
        }

        /// <summary>
        /// Gets the shipments by routing history id count.
        /// </summary>
        /// <param name="routingHistoryId">The routing history id.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="pageIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fully populate].</param>
        /// <returns></returns>
        public static Int32 GetShipmentsByRoutingHistoryIdCount(int routingHistoryId, string sortExpression,
                                                                int pageIndex,
                                                                int maximumRows, bool fullyPopulate)
        {
            return count;
            ;
        }

        /// <summary>
        /// Gets the shipments by routing history id.
        /// </summary>
        /// <param name="routingHistoryId">The routing history id.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fully populate].</param>
        /// <returns></returns>
        public static List<TDCShipment> GetShipmentsByRoutingHistoryId(int routingHistoryId, bool fullyPopulate)
        {
            return GetShipmentsByRoutingHistoryId(routingHistoryId, "Id", 0, -1, fullyPopulate);
        }

        /// <summary>
        /// Gets the shipments by routing history id.
        /// </summary>
        /// <param name="routingHistoryId">The routing history id.</param>
        /// <returns></returns>
        private static List<TDCShipmentLine> GetShipmentLinesByRoutingHistoryId(int routingHistoryId)
        {
            List<TDCShipmentLine> shipmentLines = null;
            try
            {
                shipmentLines =
                    CBO<TDCShipmentLine>.FillCollection(
                        DataAccessProvider.Instance().GetTDCShipmentLinesByRoutingId(routingHistoryId));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            return shipmentLines;
        }

        // Custom object hydration
        /// <summary>
        /// Customs the shipment fill.
        /// </summary>
        /// <param name="shipment">The shipment.</param>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fully populate].</param>
        public static void CustomFill(TDCShipment shipment, IDataReader dataReader, bool fullyPopulate)
        {
            TDCShipmentController.CustomShipmentFill(shipment, dataReader, false);
            if (fullyPopulate)
            {
                //find any shipment lines (from the collection of all lines retreievd for the sepcified
                //Routing history id) that have a matching shipmentid as the shipment argument
                //these can then be added to the shipments lines collection
                //conversion is also required from tdc shipment down to just shipment
                shipment.ShipmentLines.Clear();
                shipment.ShipmentLines.AddRange(
                    allShipmentLines.FindAll(delegate(TDCShipmentLine obj)
                                                 {
                                                     if (obj.ShipmentId == shipment.Id)
                                                     {
                                                         //allShipmentLines.Remove(obj);
                                                         return true;
                                                     }
                                                     return false;
                                                 }).
                        ConvertAll<ShipmentLine>(delegate(TDCShipmentLine input)
                                                     {
                                                         return
                                                             (ShipmentLine)input;
                                                     }));

                shipment.ShipmentLines.ForEach(delegate(ShipmentLine obj) { allShipmentLines.Remove((TDCShipmentLine)obj); });
            }
        }

        private static List<TDCShipmentLine> allShipmentLines;

        /// <summary>
        /// Gets the shipments by routing history id.
        /// </summary>
        /// <param name="routingHistoryId">The routing history id.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="pageIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fully populate].</param>
        /// <returns></returns>
        public static List<TDCShipment> GetShipmentsByRoutingHistoryId(int routingHistoryId, string sortExpression,
                                                                       int pageIndex,
                                                                       int maximumRows, bool fullyPopulate)
        {
            int totalRows = 0;
            List<TDCShipment> shipments = new List<TDCShipment>();
            try
            {
                allShipmentLines = GetShipmentLinesByRoutingHistoryId(routingHistoryId);

                int rows;
                shipments =
                    CBO<TDCShipment>.FillCollection(
                        DataAccessProvider.Instance().GetShipmentsByRoutingHistoryId(routingHistoryId, sortExpression,
                                                                                     pageIndex, maximumRows,
                                                                                     out rows),
                        CustomFill, fullyPopulate);

                allShipmentLines = null;

                totalRows = rows;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            count = totalRows;
            return shipments;
        }

        private static int CompareStrings(string a, string b)
        {
            string a0 = a;
            string b0 = b;

            string a1 = StripChars(a0.ToUpper());
            string b1 = StripChars(b0.ToUpper());
            if (a1.Length == 0 || b1.Length == 0) return 0;

            if (a1.Length > b1.Length)
            {
                string holdstring = a1;
                a1 = b1;
                b1 = holdstring;
            }
            Char[] a2 = a1.ToCharArray();
            Char[] b2 = b1.ToCharArray();

            Char[] a3;
            Char[] b3;
            int tmpclevel;
            int strlen = a1.Length;
            int matchlevel = 0;
            int bstringpos = 0;

            for (int stringpos = 0; stringpos < a1.Length; stringpos++)
            {
                if (bstringpos <= b2.GetUpperBound(0) && a2[stringpos] == b2[bstringpos])
                    matchlevel++;
                else
                {
                    if (b2.Length > (bstringpos + 1))
                    {
                        if (a2[stringpos] == b2[bstringpos + 1])
                        {
                            a3 = new char[a2.Length - stringpos];
                            Array.Copy(a2, stringpos, a3, 0, a3.GetUpperBound(0));
                            b3 = new char[b2.Length - bstringpos];
                            Array.Copy(b2, bstringpos + 1, b3, 0, b3.GetUpperBound(0));
                            tmpclevel = CompareStrings(new string(a3), new string(b3));
                            if (tmpclevel > 70)
                            {
                                matchlevel++;
                                bstringpos++;
                            }
                        }
                    }
                }
                bstringpos++;
            }
            double clevel = strlen - matchlevel;
            clevel = clevel / strlen;
            clevel = clevel * 100;
            clevel = 100 - clevel;

            return Convert.ToInt16(clevel);
        }

        /// <summary>
        /// Strips the chars. SLOW!!!
        /// </summary>
        /// <param name="stringToStrip">The string to strip.</param>
        /// <param name="regex">The regex.</param>
        /// <returns></returns>
        public static string StripChars(string stringToStrip, Regex regex)
        {
            // Regex regex = new Regex("[^0-9A-Z]+");
            return regex.Replace(stringToStrip, string.Empty);
        }

        /// <summary>
        /// Strips the chars.
        /// </summary>
        /// <param name="stringToStrip">The string to strip.</param>
        /// <returns></returns>
        public static string StripChars(string stringToStrip)
        {
            Char[] workingchars = stringToStrip.ToCharArray();
            Char[] tmpchars = new char[workingchars.Length];
            int endchar = 0;
            for (int charno = 0; charno < workingchars.Length; charno++)
            {
                if ((workingchars[charno] >= '0' && workingchars[charno] <= '9') ||
                    (workingchars[charno] >= 'A' && workingchars[charno] <= 'Z'))
                {
                    tmpchars[endchar] = workingchars[charno];
                    if (charno < workingchars.Length) endchar++;
                }
            }
            Char[] newchars = new char[endchar];
            Array.Copy(tmpchars, newchars, endchar);

            return new string(newchars);
        }

        #endregion

        /// <summary>
        /// Merges the delivery points manually.
        /// </summary>
        /// <param name="routingHistoryId">The routing history id.</param>
        /// <param name="mainSiteCode">The main site code. All other site codes will become the same as this value</param>
        /// <param name="siteCodesToMerge">The site codes to merge. The site codes to update to be the same as the main site code.</param>
        /// <returns>-1:items from more than one delivery warehouse have been selected to be merged, this is not allowed, -2:Stored procedure error , -3: failed to reshulle and recreate the delivery point numbers</returns>
        public static int MergeDeliveryPointsManually(int routingHistoryId,
                                                      int mainSiteCode, List<int> siteCodesToMerge)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    DataAccessProvider dataAccessProvider = DataAccessProvider.Instance();

                    int returnValue =
                        dataAccessProvider.MergeDeliveryPointsManually(routingHistoryId, mainSiteCode, siteCodesToMerge);

                    if (returnValue != 0)
                    {
                        //items from more than one delivery warehouse have been selected to be merged, this is not allowed
                        return returnValue;
                    }


                    if (!ManualMergeReshuffle(routingHistoryId))
                    {
                        //failed to reshulle and recreate the delivery point numbers
                        return -3;
                    }

                    if (Transaction.Current != null &&
                        Transaction.Current.TransactionInformation.Status == TransactionStatus.Active)
                    {
                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            return 0;
        }

        /// <summary>
        /// Generates the optrak files.
        /// </summary>
        /// <param name="routingHistoryId">The routing history id.</param>
        /// <param name="regionCode">The region code.</param>
        public static void GenerateOptrakFiles(int routingHistoryId, string regionCode)
        {
            List<TDCShipment> shipmentsToOutput = GetShipmentsByRoutingHistoryId(routingHistoryId, true);

            if (shipmentsToOutput.Count > 0)
            {
                //List<Thread> threads = new List<Thread>();

                //ParameterizedThreadStart shipmentThreadStart = new ParameterizedThreadStart(GenerateShipments);
                //Thread shipmentThread = new Thread(shipmentThreadStart);
                //shipmentThread.Start(shipmentsToOutput);

                //threads.Add(shipmentThread);


                //ParameterizedThreadStart sitesThreadStart = new ParameterizedThreadStart(GenerateSites);
                //Thread sitesThread = new Thread(sitesThreadStart);
                //sitesThread.Start(shipmentsToOutput);

                //threads.Add(sitesThread);

                //ParameterizedThreadStart productsThreadStart = new ParameterizedThreadStart(GenerateProducts);
                //Thread productThread = new Thread(productsThreadStart);
                //productThread.Start(shipmentsToOutput);

                //threads.Add(productThread);

                //ParameterizedThreadStart partsThreadStart = new ParameterizedThreadStart(GenerateShipmentLines);
                //Thread partsThread = new Thread(partsThreadStart);
                //partsThread.Start(shipmentsToOutput);

                //threads.Add(partsThread);


                List<string> productFileContents = GenerateProducts(shipmentsToOutput);
                List<string> shipmentLineFileContents = GenerateShipmentLines(shipmentsToOutput);
                List<string> shipmentFileContents = GenerateShipments(shipmentsToOutput, regionCode);
                List<string> sitesFileContents = GenerateSites(shipmentsToOutput);

                using (TransactionScope scope = new TransactionScope())
                {
                    BuildFileAndSend(shipmentLineFileContents, regionCode,
                                     ConfigurationManager.AppSettings["OptrakPartsFileName"].ToString());
                    BuildFileAndSend(productFileContents, regionCode,
                                     ConfigurationManager.AppSettings["OptrakProductsFileName"].ToString());
                    BuildFileAndSend(shipmentFileContents, regionCode,
                                     ConfigurationManager.AppSettings["OptrakOrdersFileName"].ToString());
                    BuildFileAndSend(sitesFileContents, regionCode,
                                     ConfigurationManager.AppSettings["OptrakSitesFileName"].ToString());

                    //update the sent date of the history record
                    RoutingHistory routingHistory = GetRoutingHistory(routingHistoryId);
                    if (routingHistory == null)
                    {
                        return;
                    }
                    routingHistory.SentDate = DateTime.Now;


                    if (SaveRoutingHistory(routingHistory) == -1)
                    {
                        return;
                    }

                    //bool keepChecking = true;

                    //do
                    //{
                    //    foreach (Thread thread in threads)
                    //    {
                    //        if (thread.ThreadState == ThreadState.Running)
                    //        {
                    //            break;
                    //        }
                    //        keepChecking = false;
                    //    }
                    //} while (keepChecking);

                    if (Transaction.Current != null &&
                        Transaction.Current.TransactionInformation.Status == TransactionStatus.Active)
                    {
                        scope.Complete();
                    }
                }
            }
        }

        /// <summary>
        /// Saves the routing history.
        /// </summary>
        /// <param name="routingHistory">The routing history.</param>
        /// <returns></returns>
        public static int SaveRoutingHistory(RoutingHistory routingHistory)
        {
            try
            {
                if (routingHistory.IsValid)
                {
                    // Save entity
                    routingHistory.Id = DataAccessProvider.Instance().SaveRoutingHistory(routingHistory);
                }
                else
                {
                    // Entity is not valid
                    throw new InValidBusinessObjectException(routingHistory);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            // Done
            return routingHistory.Id;
        }


        /// <summary>
        /// Performs a reshuffle of site codes after a manually merge has occured.
        /// </summary>
        /// <param name="routingHistoryId">The routing History Id that groups all the items to merge.</param>
        /// <returns></returns>
        private static bool ManualMergeReshuffle(int routingHistoryId)
        {
            try
            {
                return DataAccessProvider.Instance().ManualMergeReshuffle(routingHistoryId);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            return true;
        }

        /// <summary>
        /// Unmerges a shipment from it's group and gives it a new delivery/site/location code.
        /// </summary>
        /// <param name="shipmentId">The shipment id.</param>
        /// <param name="routingHistoryId">Routing History Id used to reshuffle to delivery point/site codes of the shipments groupd by this Id.</param>
        public static void UnMerge(int shipmentId, int routingHistoryId)
        {
            TDCShipment tdcShipment = TDCShipmentController.GetShipment(shipmentId);
            tdcShipment.LocationCode = Int32.MaxValue.ToString();
            TDCShipmentController.SaveShipment(tdcShipment);

            ManualMergeReshuffle(routingHistoryId);
        }

        /// <summary>
        /// Removes the items specified from routing. Passing null to the argument of shipments
        /// to remove will remove all related to the routing history id
        /// </summary>
        /// <param name="shipmentsToRemove">The shipments to remove.</param>
        /// <param name="removedBy">The removed by.</param>
        /// <param name="routingHistoryId">The routing history id.</param>
        /// <returns></returns>
        public static bool RemoveItemsFromRouting(List<TDCShipment> shipmentsToRemove, string removedBy,
                                                  int routingHistoryId)
        {
            bool successful = false;
            if (shipmentsToRemove != null)
            {
                if (shipmentsToRemove.Count > 0)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        foreach (TDCShipment shipment in shipmentsToRemove)
                        {
                            successful = DeleteShipmentFromRouting(shipment.Id, routingHistoryId);

                            if (successful)
                            {
                                successful =
                                    TDCShipmentController.UpdateShipmentStatus(shipment, Shipment.StatusEnum.Mapped,
                                                                               removedBy);
                                if (!successful)
                                {
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }


                        if (successful)
                        {
                            if (Transaction.Current != null &&
                                Transaction.Current.TransactionInformation.Status == TransactionStatus.Active)
                            {
                                scope.Complete();
                            }
                        }
                    }
                }
                else
                {
                    successful = true;
                }
            }
            else
            {
                return DataAccessProvider.Instance().RemoveItemsFromRouting(routingHistoryId, removedBy);
            }

            return successful;
        }

        /// <summary>
        /// Sets the optrak locks.
        /// </summary>
        /// <param name="processedBy">The processed by.</param>
        /// <param name="regionCode">The region code.</param>
        /// <param name="optrakDateRange">The optrak date range.</param>
        /// <returns></returns>
        public static int? SetOptrakLocks(string processedBy, string regionCode, PeriodEnum optrakDateRange)
        {
            int? routingHistoryId = null;

            OptrakRegion region = OptrakRegionController.GetRegion(regionCode);
            if (region != null)
            {
                int regionId = region.Id;

                List<NonWorkingDay> nonWorkingDays = null;
                if (optrakDateRange == PeriodEnum.NextDay)
                {
                    nonWorkingDays = NonWorkingDayController.GetNonWorkingDaysByRegion(DateTime.Today,
                                                                                       DateTime.Today.AddMonths(1),
                                                                                       regionId);
                }

                using (TransactionScope scope = new TransactionScope())
                {
                    List<Warehouse> warehouses = WarehouseController.GetWarehousesByRegion(regionId);
                    foreach (Warehouse warehouse in warehouses)
                    {
                        DateTime estimatedDateTo = new DateTime();
                        if (optrakDateRange == PeriodEnum.NextDay)
                        {

                            if (nonWorkingDays != null) estimatedDateTo = NonWorkingDayController.NextWorkingDate(
                                DateTime.Today, nonWorkingDays.FindAll(
                                    delegate(NonWorkingDay obj)
                                    {
                                        return obj.WarehouseId == warehouse.Id;
                                    }));
                        }
                        else
                        {
                            estimatedDateTo = DateTime.Today;
                        }


                        try
                        {
                            if (routingHistoryId != null && routingHistoryId > 0)
                            {

                                int returnValue;
                                returnValue = DataAccessProvider.Instance().SetOptrakLocks(routingHistoryId.Value, estimatedDateTo, processedBy, warehouse.Id);
                                if (returnValue != -2)
                                {
                                    routingHistoryId = returnValue;
                                }
                            }
                            else
                            {
                                routingHistoryId = DataAccessProvider.Instance().SetOptrakLocks(estimatedDateTo, processedBy, warehouse.Id, regionId);
                            }
                            if (routingHistoryId == -1)
                            {
                                return null;
                            }

                        }
                        catch (Exception ex)
                        {
                            if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
                        }

                    }
                    scope.Complete();

                }
            }
            return routingHistoryId;
        }

        ///// <summary>
        ///// Sets the optrak locks. This means changing a set of shipments status to Routing and adding a Routing 
        ///// History record with a link between the shipments and the history record
        ///// </summary>
        ///// <param name="shipmentCriteria">The shipment criteria.</param>
        ///// <param name="warehouseCodes">The warehouse codes.</param>
        ///// <param name="lockedBy">The locked by.</param>
        ///// <returns></returns>
        //public static int? SetOptrakLocks(ShipmentCriteria shipmentCriteria, List<string> warehouseCodes,string lockedBy)
        //{
        //    int? routingHistoryId = null;

        //    //build up a collection of shipments to lock
        //    List<TDCShipment> shipmentsToLock = new List<TDCShipment>();
        //    foreach (string warehouseCode in warehouseCodes)
        //    {
        //        shipmentCriteria.DeliveryWarehouseCode = warehouseCode;
        //        shipmentsToLock.AddRange(TDCShipmentController.GetShipments(shipmentCriteria,"ShipmentName,pafpostcode,estimateddeliverydate", false));
        //    }

        //    if (shipmentsToLock.Count > 0)
        //    {
        //        using (TransactionScope scope = new TransactionScope())
        //        {
        //            foreach (TDCShipment shipmentToLock in shipmentsToLock)
        //            {
        //                if (!TDCShipmentController.UpdateShipmentStatus(shipmentToLock, Shipment.StatusEnum.Routing,lockedBy))
        //                {
        //                    return null;
        //                }
        //            }

        //            //save routing history details
        //            RoutingHistory routingHistory = new RoutingHistory();
        //            routingHistory.ProcessStartedDate = DateTime.Now;
        //            routingHistory.ProcessedBy = lockedBy;
        //            routingHistory.WarehouseCodes = string.Join(", ", warehouseCodes.ToArray());

        //            routingHistoryId = SaveRoutingHistory(routingHistory);

        //            //add items to the link table
        //            foreach (TDCShipment shipment in shipmentsToLock)
        //            {
        //                if (!AddShipmentToRouting(shipment.Id, routingHistoryId.Value))
        //                {
        //                    return null;
        //                }
        //            }
        //            if (Transaction.Current != null && Transaction.Current.TransactionInformation.Status == TransactionStatus.Active)
        //            {
        //                scope.Complete();
        //            }
        //        }

        //    }
        //    return routingHistoryId;
        //}

        /// <summary>
        /// Adds the shipment to routing.
        /// </summary>
        /// <param name="shipmentId">The shipment id.</param>
        /// <param name="routingHistoryId">The routing history id.</param>
        /// <returns></returns>
        private static bool AddShipmentToRouting(int shipmentId, int routingHistoryId)
        {
            try
            {
                return DataAccessProvider.Instance().AddShipmentToRouting(shipmentId, routingHistoryId);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            return true;
        }

        /// <summary>
        /// Gets the shipments for delivery point that have been merged together.
        /// </summary>
        /// <param name="siteCode">The site code.</param>
        /// <param name="routingHistoryId">The routing history id.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fully populate].</param>
        /// <returns></returns>
        public static List<TDCShipment> GetShipmentsForDeliveryPoint(string siteCode, int routingHistoryId,
                                                                     bool fullyPopulate)
        {
            List<TDCShipment> shipments = new List<TDCShipment>();
            try
            {
                shipments =
                    CBO<TDCShipment>.FillCollection(
                        DataAccessProvider.Instance().GetShipmentsForDeliveryPoint(siteCode, routingHistoryId),
                        TDCShipmentController.CustomShipmentFill, fullyPopulate);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return shipments;
        }

        /// <summary>
        /// Gets the routing history by shipment line id.There should be only one routing history record where the status is something other than reset,
        /// this will be the record returned.
        /// </summary>
        /// <param name="shipmentLineId">The shipment line id.</param>
        /// <returns></returns>
        public static RoutingHistory GetRoutingHistoryByShipmentLineId(int shipmentLineId)
        {
            RoutingHistory routingHistory = null;
            try
            {
                routingHistory =
                    CBO<RoutingHistory>.FillObject(DataAccessProvider.Instance().GetRoutingHistoryByShipmentLineId(shipmentLineId));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return routingHistory;
        }

        /// <summary>
        /// Gets the routing history by shipment id. There should be only one routing history record where the status is something other than reset,
        /// this will be the record returned.
        /// </summary>
        /// <param name="shipmentId">The shipment id.</param>
        /// <returns></returns>
        public static RoutingHistory GetRoutingHistoryByShipmentId(int shipmentId)
        {
            RoutingHistory routingHistory = null;
            try
            {
                routingHistory =
                    CBO<RoutingHistory>.FillObject(DataAccessProvider.Instance().GetRoutingHistoryByShipmentId(shipmentId));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return routingHistory;
        }

        /// <summary>
        /// Sets the shipments to routed. Only the shipments that have been grouped together by the specified id will be returned.
        /// </summary>
        /// <param name="routingHistoryId">The routing history id.</param>
        /// <param name="updatedBy">The updated by.</param>
        /// <returns></returns>
        public static bool SetShipmentsToRouted(int routingHistoryId,string updatedBy)
        {
            bool success = false;
            try
            {
                success = DataAccessProvider.Instance().SetShipmentsToRouted(routingHistoryId, updatedBy,DateTime.Now);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return success;
        }
    }
}