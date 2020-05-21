//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Transactions;
//using Discovery.BusinessObjects;
//using Discovery.BusinessObjects.Controllers;
//using Discovery.BusinessSubscribers;
//using Discovery.RequestManagement;
//using Discovery.RequestManagerClient;
//using NUnit.Framework;

//namespace Discovery.UnitTests
//{
//    [TestFixture]
//    public class RoutingTests
//    {

//        //[Test]
//        //public void ImportTripParts()
//        //{
//        //    using (TransactionScope scope = new TransactionScope())
//        //    {
//        //        //add the trips to relate to first
                
//        //        ImportOrderDrops_AfterTripFilehasBeenRecieved();

//        //        string[] files = Directory.GetFiles(@"..\..\OptrakFiles", "trippart.txt");
//        //        string[] fileContents = new string[files.Length];
//        //        int i = 0;
//        //        foreach (string file in files)
//        //        {
//        //            using (StreamReader sr = new StreamReader(file))
//        //            {
//        //                fileContents[i] = sr.ReadToEnd();
//        //            }
//        //            i++;
//        //        }

//        //        OptrakTripPartSubscriber subscriber;


//        //        foreach (string content in fileContents)
//        //        {
//        //            string x = content;

//        //            // Create a request message
//        //            RequestMessage contentMessage = new RequestMessage(x);

//        //            try
//        //            {
//        //                subscriber = new OptrakTripPartSubscriber(null);
//        //                subscriber.ProcessRequest(contentMessage);
//        //                Assert.IsTrue(subscriber.Status == SubscriberStatusEnum.Processed);
//        //            }
//        //            catch (Exception ex)
//        //            {
//        //                Console.WriteLine("The following Content caused an exception:" + Environment.NewLine + content +
//        //                                  Environment.NewLine, ex.Message);
//        //                Assert.IsTrue(false);
//        //            }


//        //        }
//        //        if (Transaction.Current != null &&
//        //          Transaction.Current.TransactionInformation.Status == TransactionStatus.Active)
//        //        {
//        //            scope.Complete();
//        //        }


//        //    }
//        //}

        
//        //[Test]
//        //public void ImportOrderDrops_AfterTripFilehasBeenRecieved()
//        //{
//        //    using (TransactionScope scope = new TransactionScope())
//        //    {
//        //        //add the trips to relate to first
//        //        ImportTrips();


//        //        string[] files = Directory.GetFiles(@"..\..\OptrakFiles", "calls.txt");
//        //        string[] fileContents = new string[files.Length];
//        //        int i = 0;
//        //        foreach (string file in files)
//        //        {
//        //            using (StreamReader sr = new StreamReader(file))
//        //            {
//        //                fileContents[i] = sr.ReadToEnd();
//        //            }
//        //            i++;
//        //        }

//        //        OptrakDropSubscriber subscriber;


//        //        foreach (string content in fileContents)
//        //        {
//        //            string x = content;

//        //            // Create a request message
//        //            RequestMessage contentMessage = new RequestMessage(x);

//        //            try
//        //            {
//        //                subscriber = new OptrakDropSubscriber(null);
//        //                subscriber.ProcessRequest(contentMessage);
//        //                Assert.IsTrue(subscriber.Status == SubscriberStatusEnum.Processed);
//        //            }
//        //            catch (Exception ex)
//        //            {
//        //                Console.WriteLine("The following Content caused an exception:" + Environment.NewLine + content +
//        //                                  Environment.NewLine, ex.Message);
//        //                Assert.IsTrue(false);
//        //            }


//        //        }
//        //        if (Transaction.Current != null &&
//        //          Transaction.Current.TransactionInformation.Status == TransactionStatus.Active)
//        //        {
//        //            scope.Complete();
//        //        }


//        //    }
//        //}

//        //[Test]
//        //public void ImportOrderDrops_BeforeTripFileHasBeenRecieved()
//        //{
//        //    using (TransactionScope scope = new TransactionScope())
//        //    {
               
//        //        string[] files = Directory.GetFiles(@"..\..\OptrakFiles", "calls.txt");
//        //        string[] fileContents = new string[files.Length];
//        //        int i = 0;
//        //        foreach (string file in files)
//        //        {
//        //            using (StreamReader sr = new StreamReader(file))
//        //            {
//        //                fileContents[i] = sr.ReadToEnd();
//        //            }
//        //            i++;
//        //        }

//        //        OptrakDropSubscriber subscriber;


//        //        foreach (string content in fileContents)
//        //        {
//        //            string x = content;

//        //            // Create a request message
//        //            RequestMessage contentMessage = new RequestMessage(x);

//        //            try
//        //            {
//        //                subscriber = new OptrakDropSubscriber(null);
//        //                subscriber.ProcessRequest(contentMessage);
//        //                Assert.IsTrue(subscriber.Status == SubscriberStatusEnum.Processed);
//        //            }
//        //            catch (Exception ex)
//        //            {
//        //                Console.WriteLine("The following Content caused an exception:" + Environment.NewLine + content +
//        //                                  Environment.NewLine, ex.Message);
//        //                Assert.IsTrue(false);
//        //            }


//        //        }
//        //        if (Transaction.Current != null &&
//        //          Transaction.Current.TransactionInformation.Status == TransactionStatus.Active)
//        //        {
//        //            scope.Complete();
//        //        }


//        //    }
//        //}


//        [Test]
//        public void ImportTrips()
//        {
//            string[] files = Directory.GetFiles(@"..\..\OptrakFiles", "trips.txt");
//            string[] fileContents = new string[files.Length];
//            int i = 0;
//            foreach (string file in files)
//            {
//                using (StreamReader sr = new StreamReader(file))
//                {
//                    fileContents[i] = sr.ReadToEnd();
//                }
//                i++;
//            }

//            OptrakTripSubscriber subscriber;

//            using (TransactionScope scope = new TransactionScope())
//            {
//                foreach (string content in fileContents)
//                {
//                    string x = content;

//                    // Create a request message
//                    RequestMessage contentMessage = new RequestMessage(x);

//                    try
//                    {
//                        subscriber = new OptrakTripSubscriber(null);
//                        subscriber.ProcessRequest(contentMessage);
//                        Assert.IsTrue(subscriber.Status == SubscriberStatusEnum.Processed);
//                    }
//                    catch (Exception ex)
//                    {
//                        Console.WriteLine("The following Content caused an exception:" + Environment.NewLine + content +
//                                          Environment.NewLine, ex.Message);
//                        Assert.IsTrue(false);
//                    }


//                }
//                if (Transaction.Current != null &&
//                  Transaction.Current.TransactionInformation.Status == TransactionStatus.Active)
//                {
//                    scope.Complete();
//                }
//            }
//        }

//        [Test]
//        public void GenerateSites()
//        {


//            //create transaction, this will not be committed even if the test is sucessful in order 
//            //to leave the database in the state before this test
//            using (TransactionScope ts = new TransactionScope())
//            {
//                List<TDCShipment> shipmentsToRoute = new List<TDCShipment>();
//                TDCShipment tdcShipment;
//                TDCShipmentLine line;

//                tdcShipment = ShipmentTests.PopulateItem();
//                tdcShipment.LocationCode = "1";
//                tdcShipment.CheckInTime = 10;
//                tdcShipment.TailLiftRequired = true;
//                line = ShipmentTests.PopulateLineItem();
//                line.NetWeight = 3;
//                line.LineNumber = 1;
//                tdcShipment.ShipmentLines.Add(line);
//                tdcShipment.Id = TDCShipmentController.SaveShipment(tdcShipment);
//                shipmentsToRoute.Add(tdcShipment);

//                tdcShipment = ShipmentTests.PopulateItem();
//                tdcShipment.LocationCode = "1";
//                tdcShipment.CheckInTime = 11;
//                tdcShipment.TailLiftRequired = false;
//                line = ShipmentTests.PopulateLineItem();
//                line.NetWeight = 2;
//                line.LineNumber = 2;
//                tdcShipment.ShipmentLines.Add(line);
//                tdcShipment.Id = TDCShipmentController.SaveShipment(tdcShipment);
//                shipmentsToRoute.Add(tdcShipment);

//                tdcShipment = ShipmentTests.PopulateItem();
//                tdcShipment.LocationCode = "2";
//                tdcShipment.CheckInTime = 12;
//                line = ShipmentTests.PopulateLineItem();
//                line.NetWeight = 1;
//                line.LineNumber = 3;
//                tdcShipment.ShipmentLines.Add(line);
//                tdcShipment.Id = TDCShipmentController.SaveShipment(tdcShipment);
//                shipmentsToRoute.Add(tdcShipment);

//                RoutingController.GenerateSites(shipmentsToRoute);
//                Assert.IsTrue(true);
//            }
//        }

//        [Test]
//        public void GetOptrakWarehouses()
//        {
//            List<Warehouse> allOptrakWarhouses;
//            Warehouse optrakWarehouse;

//            //create transaction, this will not be committed even if the test is sucessful in order 
//            //to leave the database in the state before this test
//            using (TransactionScope ts = new TransactionScope())
//            {
//                //create a TDC warehouse that has optrak
//                optrakWarehouse = WarehouseTests.PopulateNewItem();
//                optrakWarehouse.HasOptrak = true;

//                //save the optrak warehouse
//                optrakWarehouse.Id = WarehouseController.SaveWarehouse(optrakWarehouse);

//                //retrieve all TDC/OPtrak warehouses, in reverse id order as the one we just saved should be one of the latest
//                allOptrakWarhouses = RoutingController.GetOptrakWarehouses("Id Desc");
//            }

//            //check that the saved warehouse is in the returned collection
//            Assert.IsTrue(allOptrakWarhouses.Contains(optrakWarehouse));
//        }

//        //[Test]
//        //public void MergeDeliveryPointsAutomaticallyMultipleWarehouses_DoNotMergeAcrossWarehouses()
//        //{
//        //    //setup three shipments to test with
//        //    List<TDCShipment> shipments = new List<TDCShipment>();

//        //    RouteShipments_MultipleWarehouses(shipments, false);

//        //    //check that shipments 2 and 0 have the same location code and 1 is different
//        //    Assert.IsTrue((shipments[0].LocationCode != shipments[1].LocationCode) &&
//        //                  (shipments[2].LocationCode == shipments[0].LocationCode));
//        //}


//        //private void RouteShipments_MultipleWarehouses(List<TDCShipment> shipments, bool mergeAcrossWarehouses)
//        //{
//        //    //create transaction, this will not be committed even if the test is sucessful in order 
//        //    //to leave the database in the state before this test
//        //    using (TransactionScope ts = new TransactionScope())
//        //    {
//        //        //populate the three shipments and save any related records they need
//        //        //set the same shipment name,paf postcode and dps but differeing warehouses
//        //        shipments.Add(ShipmentTests.PopulateItem());
//        //        Warehouse warehouse = WarehouseTests.PopulateNewItem();
//        //        WarehouseTests.SaveItem(warehouse);
//        //        shipments[0].DeliveryWarehouseCode = warehouse.Code;
                
//        //        shipments[0].StockWarehouseCode = shipments[0].DeliveryWarehouseCode.ToUpper();
//        //        shipments[0].PAFAddress.PostCode = shipments[0].PAFAddress.PostCode.ToUpper();
//        //        shipments[0].PAFAddress.DPS = "XXX";

//        //        //add the second shipment which is the same except the shipment number and warehouse
//        //        shipments.Add(shipments[0].DeepClone<TDCShipment>());
//        //        Warehouse secondwarehouse = warehouse.DeepClone<Warehouse>();
//        //        secondwarehouse.Code = "TEST2";
//        //        secondwarehouse.Id = -1;
//        //        WarehouseTests.SaveItem(secondwarehouse);
//        //        shipments[1].DeliveryWarehouseCode = secondwarehouse.Code;

//        //        shipments[1].ShipmentNumber = Guid.NewGuid().ToString().Substring(0, 9);

//        //        //the third shipment is the same as the first
//        //        shipments.Add(shipments[0].DeepClone<TDCShipment>());
//        //        shipments[2].ShipmentNumber = Guid.NewGuid().ToString().Substring(0, 9);

//        //        List<string> warehouseCodes = new List<string>();
//        //        warehouseCodes.Add(shipments[0].DeliveryWarehouseCode);
//        //        warehouseCodes.Add(shipments[1].DeliveryWarehouseCode);

//        //        AutomaticallyMerge(shipments, warehouseCodes);
//        //    }
//        //}

//        //[Test]
//        //public void MergeDeliveryPointsAutomaticallyMultipleWarehouses_MergeAcrossWarehouses()
//        //{
//        //    //setup three shipments to test with
//        //    List<TDCShipment> shipments = new List<TDCShipment>();

//        //    RouteShipments_MultipleWarehouses(shipments, true);

//        //    //check the three shipments have the same LocationCode property
//        //    Assert.IsTrue((shipments[0].LocationCode == shipments[1].LocationCode) &&
//        //                  (shipments[2].LocationCode == shipments[0].LocationCode));
//        //}

//        private static int? AutomaticallyMerge(List<TDCShipment> shipmentsToMerge, int regionId)
//        {
//            int? routingHistoryId = null;

//            foreach (TDCShipment shipment in shipmentsToMerge)
//            {
//                shipment.Id = TDCShipmentController.SaveShipment(shipment);
//            }

//            //if none of the saved shipments has an id of -1
//            if (shipmentsToMerge.Find
//                    (
//                    delegate(TDCShipment currentShipment)
//                    {
//                        return (currentShipment.Id == -1);
//                    }
//                    ) == null)
//            {
//                //set optrak "locks" on three shipments and get a routing history id
//                ShipmentCriteria criteria = new ShipmentCriteria();
//                criteria.RequiredDateFrom = shipmentsToMerge[0].RequiredShipmentDate;
//                criteria.RequiredDateTo = shipmentsToMerge[0].RequiredShipmentDate;
//                criteria.ShipmentStatus = (int)Shipment.StatusEnum.Mapped;
//                string lockedBy = "";

//                routingHistoryId = RoutingController.SetOptrakLocks(criteria.RequiredDateFrom , criteria.RequiredDateTo,lockedBy,regionId);

//                if (routingHistoryId != null)
//                {
//                    //run auto merge
//                    if (
//                        !
//                        RoutingController.MergeDeliveryPointsAutomatically(routingHistoryId.Value)
//                        )
//                    {
//                        return -1;
//                    }

//                    for (int i = 0; i < shipmentsToMerge.Count; i++)
//                    {
//                        shipmentsToMerge[i] = TDCShipmentController.GetShipment(shipmentsToMerge[i].Id);
//                    }
//                }
//            }
//            return routingHistoryId;
//        }

//        //[Test]
//        //public void Speed()
//        //{
//        //    Regex regex = new Regex("[^0-9A-Z]+");
//        //    int numberOfTests = 1000;
//        //    string[] tests = new string[numberOfTests];
//        //    string stringToStrip = "XXX. ddD";

//        //    for (int i = 0; i < numberOfTests; i++)
//        //    {
//        //        tests[i] = stringToStrip;
//        //    }
//        //    Int64 ticks = DateTime.Now.Ticks;
//        //    int times = 5;

//        //    for (int i = 0; i < times; i++)
//        //    {
//        //        for (int j = 0; j < numberOfTests; j++)
//        //        {
//        //            RoutingController.StripChars(tests[j]);
//        //            //RoutingController.StripChars(tests[j], regex);
//        //        }

//        //    }
//        //    Console.Write(new TimeSpan((DateTime.Now.Ticks - ticks) / times).TotalMilliseconds);
//        //}

//        //[Test]
//        //public void Transaction()
//        //{
//        //    TDCShipment tdcShipment = TDCShipmentController.GetShipment(9006);
//        //    using (TransactionScope scope = new TransactionScope())
//        //    {
//        //        tdcShipment.CustomerReference = "test";
//        //        TDCShipmentController.SaveShipment(tdcShipment);

//        //        using (TransactionScope scope2 = new TransactionScope())
//        //        {
//        //            tdcShipment = TDCShipmentController.GetShipment(9006);
//        //            tdcShipment.CustomerReference = "test2";
//        //            TDCShipmentController.SaveShipment(tdcShipment);
//        //            if (System.Transactions.Transaction.Current != null && System.Transactions.Transaction.Current.TransactionInformation.Status == TransactionStatus.Active)
//        //            {
//        //                //scope2.Complete();
//        //            }
//        //        }
//        //        if (System.Transactions.Transaction.Current != null && System.Transactions.Transaction.Current.TransactionInformation.Status == TransactionStatus.Active)
//        //        {
//        //            scope.Complete();
//        //        }
//        //    }
//        //    tdcShipment = TDCShipmentController.GetShipment(9006);
//        //    Assert.IsTrue(tdcShipment.CustomerReference == "test");
//        //}

//        //[Test]
//        //public void MergeDeliveryPointsAutomatically()
//        //{
//        //    //setup three shipments to test with
//        //    List<TDCShipment> shipments = new List<TDCShipment>();

//        //    //create transaction, this will not be committed even if the test is sucessful in order 
//        //    //to leave the database in the state before this test
//        //    using (TransactionScope ts = new TransactionScope())
//        //    {
//        //        //populate the three shipments and save any related records they need
//        //        //set two with the same shipment name, warehouse,paf postcode and dps and one with a different dps
//        //        shipments.Add(ShipmentTests.PopulateItem());
//        //        Warehouse warehouse = WarehouseTests.PopulateNewItem();
//        //        WarehouseTests.SaveItem(warehouse);
//        //        shipments[0].DeliveryWarehouseCode = warehouse.Code;
//        //        shipments[0].StockWarehouse.Code = shipments[0].DeliveryWarehouseCode;
                
//        //        shipments[0].PAFAddress.PostCode = shipments[0].PAFAddress.PostCode.ToUpper();
//        //        shipments[0].PAFAddress.DPS = "XXX. ddD";

//        //        shipments.Add(shipments[0].DeepClone<TDCShipment>());

//        //        shipments[1].ShipmentNumber = Guid.NewGuid().ToString().Substring(0, 9);

//        //        shipments.Add(shipments[0].DeepClone<TDCShipment>());
//        //        shipments[2].ShipmentNumber = Guid.NewGuid().ToString().Substring(0, 9);
//        //        shipments[2].PAFAddress.DPS = "ZZZ";

//        //        List<string> warehouseCodes = new List<string>();
//        //        warehouseCodes.Add(shipments[0].DeliveryWarehouse.Code);

//        //        AutomaticallyMerge(shipments, warehouseCodes);
//        //    }

//        //    //check the three shipments have the same LocationCode property
//        //    Assert.IsTrue((shipments[0].LocationCode == shipments[1].LocationCode) &&
//        //                  (shipments[2].LocationCode != shipments[0].LocationCode));
//        //}

//        //[Test]
//        //public void GetMergedShipments()
//        //{
//        //    List<MergedShipment> mergedShipments = new List<MergedShipment>();
//        //    mergedShipments = GetMergedShipments(mergedShipments);
//        //    Assert.IsTrue(mergedShipments[0].CustomerCount == 2);
//        //    Assert.IsTrue(mergedShipments[0].ShipmentCount == 2);
//        //    Assert.IsTrue(mergedShipments[1].ShipmentCount == 1);
//        //    Assert.IsTrue(mergedShipments[1].CustomerCount == 1);
//        //}

//        //private List<MergedShipment> GetMergedShipments(List<MergedShipment> mergedShipments)
//        //{
//        //    //setup three shipments to test with
//        //    List<TDCShipment> shipments = new List<TDCShipment>();

//        //    //create transaction, this will not be committed even if the test is sucessful in order 
//        //    //to leave the database in the state before this test
//        //    using (TransactionScope ts = new TransactionScope())
//        //    {
//        //        //populate the three shipments and save any related records they need
//        //        //set two with the same shipment name, warehouse,paf postcode and dps and one with a different dps and one with a different customer number
//        //        shipments.Add(ShipmentTests.PopulateItem());
//        //        Warehouse warehouse = WarehouseTests.PopulateNewItem();
//        //        WarehouseTests.SaveItem(warehouse);
//        //        shipments[0].DeliveryWarehouseCode = warehouse.Code;
//        //        shipments[0].StockWarehouse.Code = shipments[0].DeliveryWarehouseCode;
                
//        //        shipments[0].PAFAddress.PostCode = shipments[0].PAFAddress.PostCode.ToUpper();
//        //        shipments[0].PAFAddress.DPS = "XXX. ddD";

//        //        shipments.Add(shipments[0].DeepClone<TDCShipment>());

//        //        shipments[1].ShipmentNumber = Guid.NewGuid().ToString().Substring(0, 9);
//        //        shipments[1].CustomerNumber = Guid.NewGuid().ToString().Substring(0,9);
//        //        shipments.Add(shipments[0].DeepClone<TDCShipment>());
//        //        shipments[2].ShipmentNumber = Guid.NewGuid().ToString().Substring(0, 9);
//        //        shipments[2].PAFAddress.DPS = "ZZZ";

//        //        List<string> warehouseCodes = new List<string>();
//        //        warehouseCodes.Add(shipments[0].DeliveryWarehouse.Code);

//        //        int? routingHistoryId=null;
//        //        try
//        //        {
//        //            routingHistoryId = AutomaticallyMerge(shipments, warehouseCodes);
//        //        }
//        //        catch (InValidBusinessObjectException e)
//        //        {
//        //            Console.Write(e.ValidatableObject.ValidationMessages);
//        //        }

//        //        if (routingHistoryId != null)
//        //        {
//        //            //-1 should return all rows

//        //            mergedShipments = RoutingController.GetMergedShipments(routingHistoryId.Value, "", 0, -1);
//        //        }
//        //    }
//        //    return mergedShipments;
//        //}

//        [Test]
//        public void GetMergedShipmentsCount()
//        {
//            List<MergedShipment> mergedShipments = new List<MergedShipment>();
//            mergedShipments = GetMergedShipments(mergedShipments);
//            Assert.IsTrue(mergedShipments.Count == 2);
//        }

//        //[Test]
//        //public void GetShipmentsForDeliveryPoint()
//        //{
//        //    //setup three shipments to test with
//        //    List<TDCShipment> shipments = new List<TDCShipment>();

//        //    //create transaction, this will not be committed even if the test is sucessful in order 
//        //    //to leave the database in the state before this test
//        //    using (TransactionScope ts = new TransactionScope())
//        //    {
//        //        //populate the three shipments and save any related records they need
//        //        //set two with the same shipment name, warehouse,paf postcode and dps and one with a different dps
//        //        shipments.Add(ShipmentTests.PopulateItem());
//        //        Warehouse warehouse = WarehouseTests.PopulateNewItem();
//        //        WarehouseTests.SaveItem(warehouse);
//        //        shipments[0].DeliveryWarehouseCode = warehouse.Code;
//        //        shipments[0].StockWarehouse.Code = shipments[0].DeliveryWarehouseCode;
                
//        //        shipments[0].PAFAddress.PostCode = shipments[0].PAFAddress.PostCode.ToUpper();
//        //        shipments[0].PAFAddress.DPS = "XXX. ddD";

//        //        shipments.Add(shipments[0].DeepClone<TDCShipment>());

//        //        shipments[1].ShipmentNumber = Guid.NewGuid().ToString().Substring(0, 9);

//        //        shipments.Add(shipments[0].DeepClone<TDCShipment>());
//        //        shipments[2].ShipmentNumber = Guid.NewGuid().ToString().Substring(0, 9);
//        //        shipments[2].PAFAddress.DPS = "ZZZ";

//        //        List<string> warehouseCodes = new List<string>();
//        //        warehouseCodes.Add(shipments[0].DeliveryWarehouse.Code);

//        //        int? routingHistoryId = AutomaticallyMerge(shipments, warehouseCodes);

//        //        if (routingHistoryId != null)
//        //        {
//        //            List<TDCShipment> shipmentsForDeliveryPoint =
//        //                RoutingController.GetShipmentsForDeliveryPoint(shipments[0].LocationCode, routingHistoryId.Value,
//        //                                                               false);

//        //            Assert.IsTrue(shipmentsForDeliveryPoint.Count == 2);
//        //            Assert.IsTrue(shipmentsForDeliveryPoint[0].Id == shipments[0].Id);
//        //            Assert.IsTrue(shipmentsForDeliveryPoint[1].Id == shipments[1].Id);

//        //            shipmentsForDeliveryPoint =
//        //                RoutingController.GetShipmentsForDeliveryPoint(shipments[2].LocationCode, routingHistoryId.Value,
//        //                                                               false);
//        //            Assert.IsTrue(shipmentsForDeliveryPoint.Count == 1);
//        //            Assert.IsTrue(shipmentsForDeliveryPoint[0].Id == shipments[2].Id);
//        //        }
//        //    }
//        //}

//        [Test]
//        public void SetOptrakLocks()
//        {
//            //setup three shipments to test with
//            List<TDCShipment> shipments = new List<TDCShipment>();

//            //create transaction, this will not be committed even if the test is sucessful in order 
//            //to leave the database in the state before this test
//            using (TransactionScope ts = new TransactionScope())
//            {
//                //populate a shipment to route and merge, one will do
//                shipments.Add(ShipmentTests.PopulateItem());
//                Warehouse warehouse = WarehouseTests.PopulateNewItem();
//                WarehouseTests.SaveItem(warehouse);
//                shipments[0].DeliveryWarehouseCode = warehouse.Code;
//                shipments[0].StockWarehouse.Code = shipments[0].DeliveryWarehouseCode;
                
//                shipments[0].PAFAddress.PostCode = shipments[0].PAFAddress.PostCode.ToUpper();
//                shipments[0].PAFAddress.DPS = "XXX. ddD";


//                List<string> warehouseCodes = new List<string>();
//                warehouseCodes.Add(shipments[0].DeliveryWarehouse.Code);

//                int? routingHistoryId = AutomaticallyMerge(shipments, warehouseCodes);
//                Assert.IsNotNull(routingHistoryId);
//            }
//        }

//        //[Test]
//        //public void RemoveItemsFromRouting()
//        //{
//        //    //setup three shipments to test with
//        //    List<TDCShipment> shipments = new List<TDCShipment>();

//        //    //create transaction, this will not be committed even if the test is sucessful in order 
//        //    //to leave the database in the state before this test
//        //    using (TransactionScope ts = new TransactionScope())
//        //    {
//        //        //populate a shipment to route and merge, one will do
//        //        shipments.Add(ShipmentTests.PopulateItem());
//        //        Warehouse warehouse = WarehouseTests.PopulateNewItem();
//        //        WarehouseTests.SaveItem(warehouse);
//        //        shipments[0].DeliveryWarehouseCode = warehouse.Code;
//        //        shipments[0].StockWarehouse.Code = shipments[0].DeliveryWarehouseCode;
                
//        //        shipments[0].PAFAddress.PostCode = shipments[0].PAFAddress.PostCode.ToUpper();
//        //        shipments[0].PAFAddress.DPS = "XXX. ddD";


//        //        List<string> warehouseCodes = new List<string>();
//        //        warehouseCodes.Add(shipments[0].DeliveryWarehouse.Code);

//        //        int? routingHistoryId = AutomaticallyMerge(shipments, warehouseCodes);
//        //        if (routingHistoryId != null)
//        //        {
//        //            //there should be one shipment linked to the routing history id now
//        //            int countBeforeRemoval =
//        //                RoutingController.GetShipmentsByRoutingHistoryId(routingHistoryId.Value, false).Count;
//        //            //remove 1
//        //            if (RoutingController.RemoveItemsFromRouting(shipments, "UnitTest", routingHistoryId.Value))
//        //            {
//        //                int countAfterRemoval =
//        //                    RoutingController.GetShipmentsByRoutingHistoryId(routingHistoryId.Value, false).Count;
//        //                //there should be one less than before the removal 
//        //                Console.WriteLine(countBeforeRemoval);
//        //                Console.WriteLine(countAfterRemoval);
//        //                Assert.IsTrue(countBeforeRemoval - countAfterRemoval == 1);
//        //            }
//        //        }
//        //    }
//        //}

//        [Test]
//        public void UnMerge()
//        {
//            //setup three shipments to test with
//            List<TDCShipment> shipments = new List<TDCShipment>();

//            //create transaction, this will not be committed even if the test is sucessful in order 
//            //to leave the database in the state before this test
//            using (TransactionScope ts = new TransactionScope())
//            {
//                //populate the three shipments and save any related records they need
//                //set two with the same shipment name, warehouse,paf postcode and dps and one with a different dps
//                shipments.Add(ShipmentTests.PopulateItem());
//                Warehouse warehouse = WarehouseTests.PopulateNewItem();
//                WarehouseTests.SaveItem(warehouse);
//                shipments[0].DeliveryWarehouseCode = warehouse.Code;
//                shipments[0].StockWarehouse.Code = shipments[0].DeliveryWarehouseCode;
                
//                shipments[0].PAFAddress.PostCode = shipments[0].PAFAddress.PostCode.ToUpper();
//                shipments[0].PAFAddress.DPS = "XXX. ddD";

//                shipments.Add(shipments[0].DeepClone<TDCShipment>());
//                shipments[1].ShipmentNumber = Guid.NewGuid().ToString().Substring(0, 9);

//                shipments.Add(shipments[0].DeepClone<TDCShipment>());
//                shipments[2].ShipmentNumber = Guid.NewGuid().ToString().Substring(0, 9);
//                shipments[2].PAFAddress.DPS = "ZZZ";

//                List<string> warehouseCodes = new List<string>();
//                warehouseCodes.Add(shipments[0].DeliveryWarehouse.Code);

//                int? routingHistoryId = AutomaticallyMerge(shipments, warehouseCodes);

//                if (routingHistoryId != null)
//                {
//                    //there are two shipments merged together automatically
//                    List<TDCShipment> shipmentsForDeliveryPoint =
//                        RoutingController.GetShipmentsForDeliveryPoint(shipments[0].LocationCode, routingHistoryId.Value,
//                                                                       false);
//                    Assert.IsTrue(shipmentsForDeliveryPoint.Count == 2);

//                    //there are 2 merged groups of information in total
//                    int mergedShipmentsCount =
//                        RoutingController.GetMergedShipments(routingHistoryId.Value, "", 0, -1).Count;
//                    Assert.IsTrue(mergedShipmentsCount == 2);

//                    //unmerge one of the two automatically merged shipments
//                    RoutingController.UnMerge(shipments[0].Id, routingHistoryId.Value);

//                    //there should be 3 groups of information now
//                    mergedShipmentsCount = RoutingController.GetMergedShipments(routingHistoryId.Value, "", 0, -1).Count;
//                    Assert.IsTrue(mergedShipmentsCount == 3);

//                    //has the location codes now changed
//                    Assert.IsTrue(shipments[0].LocationCode !=
//                                  TDCShipmentController.GetShipment(shipments[0].Id).LocationCode);
//                }
//            }
//        }

//        [Test]
//        public void ResetLock()
//        {
//            //setup three shipments to test with
//            List<TDCShipment> shipments = new List<TDCShipment>();

//            //create transaction, this will not be committed even if the test is sucessful in order 
//            //to leave the database in the state before this test
//            using (TransactionScope ts = new TransactionScope())
//            {
//                //populate the three shipments and save any related records they need
//                //set two with the same shipment name, warehouse,paf postcode and dps and one with a different dps
//                shipments.Add(ShipmentTests.PopulateItem());
//                //shipments[0].DeliveryWarehouseCode = WarehouseTests.PopulateNewItem().Code;

//                Warehouse deliveryWarehouse = WarehouseTests.PopulateNewItem();
//                WarehouseTests.SaveItem(deliveryWarehouse);
//                Warehouse warehouse = WarehouseTests.PopulateNewItem();
//                WarehouseTests.SaveItem(warehouse);
//                shipments[0].DeliveryWarehouseCode = warehouse.Code;
//                shipments[0].StockWarehouse.Code = shipments[0].DeliveryWarehouseCode;
                
//                shipments[0].PAFAddress.PostCode = shipments[0].PAFAddress.PostCode.ToUpper();
//                shipments[0].PAFAddress.DPS = "XXX. ddD";

//                shipments.Add(shipments[0].DeepClone<TDCShipment>());
//                shipments[1].ShipmentNumber = Guid.NewGuid().ToString().Substring(0, 9);

//                shipments.Add(shipments[0].DeepClone<TDCShipment>());
//                shipments[2].ShipmentNumber = Guid.NewGuid().ToString().Substring(0, 9);
//                shipments[2].PAFAddress.DPS = "ZZZ";

//                List<string> warehouseCodes = new List<string>();
//                warehouseCodes.Add(shipments[0].DeliveryWarehouseCode);

//                int? routingHistoryId = AutomaticallyMerge(shipments, warehouseCodes);

//                if (routingHistoryId != null)
//                {
//                    //get the number of "locked" shipments, should be 3
//                    int lockedShipments =
//                        RoutingController.GetShipmentsByRoutingHistoryId(routingHistoryId.Value, false).Count;
//                    Assert.IsTrue(lockedShipments == 3);

//                    //reset lock for routing history
//                    if (RoutingController.ResetLock(routingHistoryId.Value, "UnitTest"))
//                    {
//                        //check all shipments grouped by the routing history id have the status of Mapped
//                        //therefore they are no longer "locked"
//                        Assert.IsTrue(
//                            RoutingController.GetShipmentsByRoutingHistoryId(routingHistoryId.Value, false).TrueForAll
//                                (
//                                delegate(TDCShipment currentShipment)
//                                {
//                                    return (currentShipment.Status == Shipment.StatusEnum.Mapped);
//                                }
//                                )
//                            );
//                    }
//                }
//            }
//        }

//        [Test]
//        public void SaveRoutingHistory()
//        {
//            List<TDCShipment> shipments = new List<TDCShipment>();

//            //create transaction, this will not be committed even if the test is sucessful in order 
//            //to leave the database in the state before this test
//            using (TransactionScope ts = new TransactionScope())
//            {
//                //populate a shipment to route and merge, one will do
//                shipments.Add(ShipmentTests.PopulateItem());
//                Warehouse warehouse = WarehouseTests.PopulateNewItem();
//                WarehouseTests.SaveItem(warehouse);
//                shipments[0].DeliveryWarehouseCode = warehouse.Code;
//                shipments[0].StockWarehouse.Code = shipments[0].DeliveryWarehouseCode;
                
//                shipments[0].PAFAddress.PostCode = shipments[0].PAFAddress.PostCode.ToUpper();
//                shipments[0].PAFAddress.DPS = "XXX. ddD";


//                List<string> warehouseCodes = new List<string>();
//                warehouseCodes.Add(shipments[0].DeliveryWarehouse.Code);

//                int? routingHistoryId = AutomaticallyMerge(shipments, warehouseCodes);
//                if (routingHistoryId != null)
//                {
//                    //retrieve the routing history record
//                    RoutingHistory routingHistory = RoutingController.GetRoutingHistory(routingHistoryId.Value);
//                    //change a property, in this case the reset date/time
//                    DateTime resetDate = DateTime.Now;
//                    routingHistory.ResetDate = resetDate;
//                    //resave the entity
//                    if (RoutingController.SaveRoutingHistory(routingHistory) != -1)
//                    {
//                        //retieve the saved entity
//                        routingHistory = RoutingController.GetRoutingHistory(routingHistoryId.Value);
//                        //check the reset date is what we expect

//                        Assert.IsTrue(routingHistory.ResetDate.ToString() == resetDate.ToString());
//                    }
//                }

//            }

//        }

//        [Test]
//        public void GetRoutingHistoryByStatus()
//        {
//            List<TDCShipment> shipments = new List<TDCShipment>();

//            //create transaction, this will not be committed even if the test is sucessful in order 
//            //to leave the database in the state before this test
//            using (TransactionScope ts = new TransactionScope())
//            {
//                //populate a shipment to route and merge, one will do
//                shipments.Add(ShipmentTests.PopulateItem());
//                Warehouse warehouse = WarehouseTests.PopulateNewItem();
//                WarehouseTests.SaveItem(warehouse);
//                shipments[0].DeliveryWarehouseCode = warehouse.Code;
//                shipments[0].StockWarehouse.Code = shipments[0].DeliveryWarehouseCode;
                
//                shipments[0].PAFAddress.PostCode = shipments[0].PAFAddress.PostCode.ToUpper();
//                shipments[0].PAFAddress.DPS = "XXX. ddD";

//                List<string> warehouseCodes = new List<string>();
//                warehouseCodes.Add(shipments[0].DeliveryWarehouse.Code);

//                int? routingHistoryId = AutomaticallyMerge(shipments, warehouseCodes);
//                if (routingHistoryId != null)
//                {
//                    //retrieve the routing history record
//                    RoutingHistory routingHistory = RoutingController.GetRoutingHistory(routingHistoryId.Value);
//                    //status should just be In process
//                    Assert.IsTrue(routingHistory.Status == RoutingHistory.StatusEnum.InProcess);

//                    //set the sent date/time
//                    routingHistory.SentDate = DateTime.Now;
//                    //status should now be Sent
//                    Assert.IsTrue(routingHistory.Status == RoutingHistory.StatusEnum.Sent);

//                    //set the Received date/time
//                    routingHistory.TripFilereceivedDate = DateTime.Now;
//                    //status should now be Recieved
//                    Assert.IsTrue(routingHistory.Status == RoutingHistory.StatusEnum.Recieved);

//                    //set the Reset date/time
//                    routingHistory.ResetDate = DateTime.Now;
//                    //status should now be Recieved
//                    Assert.IsTrue(routingHistory.Status == RoutingHistory.StatusEnum.Reset);
//                }

//            }
//        }

//        [Test]
//        public void GetRoutingHistoryById()
//        {

//            List<TDCShipment> shipments = new List<TDCShipment>();

//            //create transaction, this will not be committed even if the test is sucessful in order 
//            //to leave the database in the state before this test
//            using (TransactionScope ts = new TransactionScope())
//            {
//                //populate a shipment to route and merge, one will do
//                shipments.Add(ShipmentTests.PopulateItem());
//                Warehouse warehouse = WarehouseTests.PopulateNewItem();
//                WarehouseTests.SaveItem(warehouse);
//                shipments[0].DeliveryWarehouseCode = warehouse.Code;
//                shipments[0].StockWarehouse.Code = shipments[0].DeliveryWarehouseCode;
                
//                shipments[0].PAFAddress.PostCode = shipments[0].PAFAddress.PostCode.ToUpper();
//                shipments[0].PAFAddress.DPS = "XXX. ddD";

//                List<string> warehouseCodes = new List<string>();
//                warehouseCodes.Add(shipments[0].DeliveryWarehouse.Code);

//                int? routingHistoryId = AutomaticallyMerge(shipments, warehouseCodes);
//                if (routingHistoryId != null)
//                {
//                    //retrieve the routing history record
//                    RoutingHistory routingHistory = RoutingController.GetRoutingHistory(routingHistoryId.Value);
//                    //status should just be In process
//                    Assert.IsNotNull(routingHistory);
//                    Assert.IsTrue(routingHistory.Id != -1);
//                    Assert.IsTrue(routingHistory.Id == routingHistoryId.Value);
//                }

//            }
//        }

//        [Test]
//        public void GetShipmentsByRoutingHistoryId()
//        {
//            //setup three shipments to test with
//            List<TDCShipment> shipments = new List<TDCShipment>();

//            //create transaction, this will not be committed even if the test is sucessful in order 
//            //to leave the database in the state before this test
//            using (TransactionScope ts = new TransactionScope())
//            {
//                //populate the three shipments and save any related records they need
//                //set two with the same shipment name, warehouse,paf postcode and dps and one with a different dps
//                shipments.Add(ShipmentTests.PopulateItem());
//                Warehouse warehouse = WarehouseTests.PopulateNewItem();
//                WarehouseTests.SaveItem(warehouse);
//                shipments[0].DeliveryWarehouseCode = warehouse.Code;
//                shipments[0].StockWarehouse.Code = shipments[0].DeliveryWarehouseCode;
                
//                shipments[0].PAFAddress.PostCode = shipments[0].PAFAddress.PostCode.ToUpper();
//                shipments[0].PAFAddress.DPS = "XXX. ddD";

//                shipments.Add(shipments[0].DeepClone<TDCShipment>());
//                shipments[1].ShipmentNumber = Guid.NewGuid().ToString().Substring(0, 9);

//                shipments.Add(shipments[0].DeepClone<TDCShipment>());
//                shipments[2].ShipmentNumber = Guid.NewGuid().ToString().Substring(0, 9);
//                shipments[2].PAFAddress.DPS = "ZZZ";

//                List<string> warehouseCodes = new List<string>();
//                warehouseCodes.Add(shipments[0].DeliveryWarehouse.Code);

//                int? routingHistoryId = AutomaticallyMerge(shipments, warehouseCodes);

//                if (routingHistoryId != null)
//                {
//                    //get the number of "locked" shipments, should be 3
//                    int lockedShipments =
//                        RoutingController.GetShipmentsByRoutingHistoryId(routingHistoryId.Value, false).Count;
//                    Assert.IsTrue(lockedShipments == 3);


//                }
//            }
//        }

//        [Test]
//        public void GetShipmentsByRoutingHistoryIdCount()
//        {
//            List<TDCShipment> shipments = new List<TDCShipment>();

//            //create transaction, this will not be committed even if the test is sucessful in order 
//            //to leave the database in the state before this test
//            using (TransactionScope ts = new TransactionScope())
//            {
//                //populate the three shipments and save any related records they need
//                //set two with the same shipment name, warehouse,paf postcode and dps and one with a different dps
//                shipments.Add(ShipmentTests.PopulateItem());
//                Warehouse warehouse = WarehouseTests.PopulateNewItem();
//                WarehouseTests.SaveItem(warehouse);
//                shipments[0].DeliveryWarehouseCode = warehouse.Code;
//                shipments[0].StockWarehouse.Code = shipments[0].DeliveryWarehouseCode;
                
//                shipments[0].PAFAddress.PostCode = shipments[0].PAFAddress.PostCode.ToUpper();
//                shipments[0].PAFAddress.DPS = "XXX. ddD";

//                shipments.Add(shipments[0].DeepClone<TDCShipment>());
//                shipments[1].ShipmentNumber = Guid.NewGuid().ToString().Substring(0, 9);

//                shipments.Add(shipments[0].DeepClone<TDCShipment>());
//                shipments[2].ShipmentNumber = Guid.NewGuid().ToString().Substring(0, 9);
//                shipments[2].PAFAddress.DPS = "ZZZ";

//                List<string> warehouseCodes = new List<string>();
//                warehouseCodes.Add(shipments[0].DeliveryWarehouse.Code);

//                int? routingHistoryId = AutomaticallyMerge(shipments, warehouseCodes);

//                if (routingHistoryId != null)
//                {
//                    //get the number of "locked" shipments, should be 3
//                    int lockedShipments =
//                        RoutingController.GetShipmentsByRoutingHistoryIdCount(routingHistoryId.Value, false);
//                    Assert.IsTrue(lockedShipments == 3);


//                }
//            }
//        }

//        [Test]
//        public void MergeDeliveryPointsManually()
//        {
//            //setup three shipments to test with
//            List<TDCShipment> shipments = new List<TDCShipment>();

//            //create transaction, this will not be committed even if the test is sucessful in order 
//            //to leave the database in the state before this test
//            using (TransactionScope ts = new TransactionScope())
//            {
//                //populate the three shipments and save any related records they need
//                //set two with the same shipment name, warehouse,paf postcode and dps and one with a different dps
//                shipments.Add(ShipmentTests.PopulateItem());
//                Warehouse warehouse = WarehouseTests.PopulateNewItem();
//                WarehouseTests.SaveItem(warehouse);
//                shipments[0].DeliveryWarehouseCode = warehouse.Code;
//                shipments[0].StockWarehouse.Code = shipments[0].DeliveryWarehouseCode;
                
//                shipments[0].PAFAddress.PostCode = shipments[0].PAFAddress.PostCode.ToUpper();
//                shipments[0].PAFAddress.DPS = "XXX. ddD";

//                shipments.Add(shipments[0].DeepClone<TDCShipment>());
//                shipments[1].ShipmentNumber = Guid.NewGuid().ToString().Substring(0, 9);

//                shipments.Add(shipments[0].DeepClone<TDCShipment>());
//                shipments[2].ShipmentNumber = Guid.NewGuid().ToString().Substring(0, 9);
//                shipments[2].PAFAddress.DPS = "ZZZ";

//                List<string> warehouseCodes = new List<string>();
//                warehouseCodes.Add(shipments[0].DeliveryWarehouse.Code);

//                int? routingHistoryId = AutomaticallyMerge(shipments, warehouseCodes);

//                //check the three shipments have the same LocationCode property
//                Assert.IsTrue((shipments[0].LocationCode == shipments[1].LocationCode) &&
//                              (shipments[2].LocationCode != shipments[0].LocationCode));

//                List<int> siteCodesToMerge = new List<int>();
//                //merge shipment 2 with 0 so that all three shipments should have the same location code/delivery point
//                siteCodesToMerge.Add(Convert.ToInt32(shipments[2].LocationCode));
//                RoutingController.MergeDeliveryPointsManually(routingHistoryId.Value, Convert.ToInt32(shipments[0].LocationCode),
//                                                              siteCodesToMerge);

//                //retrieve the count of shipments with the location code of shipment 0, it should now be three
//                Assert.IsTrue(RoutingController.GetShipmentsForDeliveryPoint(shipments[0].LocationCode, routingHistoryId.Value, false).Count == 3);
//            }


//        }


//    }
//}