using System;
using System.Collections.Generic;
using System.Transactions;
using Discovery.BusinessObjects;
using Discovery.BusinessObjects.Controllers;
using Discovery.ComponentServices.ExceptionHandling;
using Discovery.Utility.DataAccess.Exceptions;
using NUnit.Framework;

namespace Discovery.UnitTests
{
    [TestFixture]
    public class WarehouseTests
    {
        static internal int SaveItem(Warehouse Warehouse)
        {
            return WarehouseController.SaveWarehouse(Warehouse);
        }

       static  internal Warehouse PopulateNewItem()
        {
            Warehouse warehouse = new Warehouse();
            warehouse.Code = Guid.NewGuid().ToString().Substring(0,10).ToUpper();
            warehouse.Description = "Description";
            warehouse.UpdatedBy = "test";
            warehouse.Contact = new Contact();
            warehouse.Contact.Name = "ContactName";
            warehouse.Contact.TelephoneNumber = "TelephoneNumber";
            warehouse.Contact.Email = "SalesEmail";
            warehouse.HasCommander = true;
            warehouse.HasOptrak = true;
            warehouse.IsTDC = true;
            //PrinterTests printerTests=new PrinterTests();
            //warehouse.Printer = printerTests.PopulateMappingItem();
            //warehouse.Printer.Id= printerTests.SaveItem(warehouse.Printer);
            //warehouse.PrinterId = warehouse.Printer.Id;
            RegionTests regionTest = new RegionTests();
            warehouse.OptrakRegion = regionTest.PopulateNewItem();
            warehouse.OptrakRegion.Id = regionTest.SaveItem(warehouse.OptrakRegion);
            warehouse.RegionId = warehouse.OptrakRegion.Id;
            warehouse.Address = new Address();
            warehouse.Address.Line1 = "line1";
            warehouse.Address.Line2 = "line2";
            warehouse.Address.Line3 = "line3";
            warehouse.Address.Line4 = "line4";
            warehouse.Address.PostCode = "PostCode";
            return warehouse;
        }

        [Test]
        public void SaveItem()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                Warehouse warehouse = PopulateNewItem();
                warehouse.Id = SaveItem(warehouse);
                Assert.IsTrue(warehouse.Id != -1);
                //DeleteItem(warehouse);
                //PrinterTests printerTests = new PrinterTests();
                //printerTests.DeleteItem(warehouse.Printer);
                //RegionTests regionTest = new RegionTests();
                //regionTest.DeleteItem(warehouse.OptrakRegion);
            }
        }

        [Test]
        [ExpectedException(typeof (DiscoveryException))]
        public void SaveWarehouseTestConstraint()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                Warehouse warehouse = PopulateNewItem();
                SaveItem(warehouse);
                SaveItem(warehouse);
            }
        }

        [Test]
        public void UpdateWarehouse()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                Warehouse warehouse = PopulateNewItem();
                warehouse.Description = "Original";
                warehouse.Id = SaveItem(warehouse);
                warehouse = GetItem(warehouse.Id);
                //change a value
                warehouse.Description = "Updated";
                warehouse.Id = SaveItem(warehouse);
                warehouse = GetItem(warehouse.Id);
                Assert.IsTrue(warehouse.Description == "Updated");
            }

            //try
            //{
            //    SaveItem(warehouse);
            //    warehouse = GetItem(warehouse.Id);
            //    Assert.IsTrue(warehouse.Description == "Updated");
            //}

            //finally
            //{
            //    DeleteItem(warehouse);
            //    //PrinterTests printerTests = new PrinterTests();
            //    //printerTests.DeleteItem(warehouse.Printer);
            //    RegionTests regionTest = new RegionTests();
            //    regionTest.DeleteItem(warehouse.OptrakRegion);
            //}
        }

        [Test]
        [ExpectedException(typeof (DiscoveryException))]
        //[ExpectedException(typeof (ConcurrencyException))]
            public void UpdateWarehouseConcurrencyTest()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                Warehouse warehouse = PopulateNewItem();
                warehouse.Id = SaveItem(warehouse);
                //We didn't get the new checksum so when we save again an exception should be thrown
                try
                {
                    SaveItem(warehouse);
                }
                catch (DiscoveryException e)
                {
                    Assert.IsInstanceOfType(typeof (ConcurrencyException), e.InnerException);
                    throw e;
                }
            }
        }

        internal Warehouse GetItem(int id)
        {
            return WarehouseController.GetWarehouse(id);
        }

        [Test]
        public void GetItem()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                Warehouse warehouse = PopulateNewItem();
                warehouse.Id = SaveItem(warehouse);

                if (warehouse.Id != -1)
                    Assert.IsNotNull(GetItem(warehouse.Id));
            }
        }

        [Test]
        public void GetItems()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //add a warehouse
                int id = WarehouseController.SaveWarehouse(PopulateNewItem());
                if (id > -1)
                {
                    //retrieve all warehouses and the one we saved should return at least
                    List<Warehouse> warehouses = WarehouseController.GetWarehouses();
                    //so the count should be >0
                    Assert.IsTrue(warehouses.Count > 0);
                    //check for our new id
                    Assert.IsTrue(warehouses.Find(delegate(Warehouse currentItem)
                                                       {
                                                           return currentItem.Id == id;
                                                       }) != null);
                }
            }
            
          
        }

        internal bool DeleteItem(Warehouse Warehouse)
        {
            return WarehouseController.DeleteWarehouse(Warehouse);
        }

        [Test]
        public void DeleteItem()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                Warehouse warehouse = PopulateNewItem();
                warehouse.Id = SaveItem(warehouse);
                if (warehouse.Id != -1)
                {
                    Assert.IsTrue(DeleteItem(warehouse));
                    //PrinterTests printerTests = new PrinterTests();
                    //printerTests.DeleteItem(warehouse.Printer);
                    //RegionTests regionTest = new RegionTests();
                    //regionTest.DeleteItem(warehouse.OptrakRegion);
                }
            }
        }

        [Test]
        public void TestGetWarehouses()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                Warehouse warehouse = PopulateNewItem();
                int Id1 = SaveItem(warehouse);
                warehouse.Id = -1;
                warehouse.Code = "Code2";
                warehouse.Description = "Next One";
                int Id2 = SaveItem(warehouse);
                List<Warehouse> warehouseList = WarehouseController.GetWarehouses(true);

                int NoOfRows = 0;

                foreach (Warehouse wh in warehouseList)
                {
                    if (wh.Id == Id1 || wh.Id == Id2)
                    {
                        NoOfRows++;
                    }
                }
                Assert.IsTrue(NoOfRows == 2);
            }
        }

        [Test]
        public void TestGetOpCoStockWarehouseCodes()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                // Populate and Create an OpCo Shipment with default values
                OpCoShipment opCoShipment = OpcoShipmentTests.PopulateNewItem();
                OpCoShipmentController.SaveShipment(opCoShipment);

                // Save the OpCo Code
                string SaveOpCoCode = opCoShipment.OpCoCode;

                // Create a second OpCo Shipment with the same OpCo Code, but a different stock warehouse code
                opCoShipment.Id = -1;
                opCoShipment.OpCoCode = SaveOpCoCode;
                opCoShipment.StockWarehouseCode = "ZZZ";
                opCoShipment.ShipmentNumber = "Ship2";
                opCoShipment.DespatchNumber = "Despatch2";

                 OpCoShipmentController.SaveShipment(opCoShipment);

                List<Warehouse> warehouseList = WarehouseController.GetOpCoStockWarehouseCodes(SaveOpCoCode);
                Assert.IsTrue(warehouseList.Count == 2);
            }
        }
    }
}