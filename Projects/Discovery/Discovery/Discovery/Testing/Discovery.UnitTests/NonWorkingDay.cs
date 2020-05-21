using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Transactions;
using Discovery.ComponentServices.ExceptionHandling;
using Discovery.Utility.DataAccess.Exceptions;
using Discovery.BusinessObjects;
using Discovery.BusinessObjects.Controllers;
using NUnit.Framework;

namespace Discovery.UnitTests
{

    [TestFixture]
    public class NonWorkingDayTests
    {

        /// <summary>
        /// Populates the new item.
        /// </summary>
        /// <returns></returns>
        internal NonWorkingDay PopulateNewItem()
        {
            NonWorkingDay nonWorkingDay = new NonWorkingDay();

            nonWorkingDay.NonWorkingDate = DateTime.Today;
            nonWorkingDay.Description = DateTime.Today.DayOfWeek.ToString();
            nonWorkingDay.UpdatedDate = DateTime.Today;
            nonWorkingDay.UpdatedBy = "TDC Team";

            Warehouse warehouse = WarehouseTests.PopulateNewItem();
            nonWorkingDay.WarehouseCode = warehouse.Code;
            nonWorkingDay.WarehouseId = WarehouseController.SaveWarehouse(warehouse);

            return nonWorkingDay;
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        internal NonWorkingDay GetItem(int id)
        {
            return NonWorkingDayController.GetNonWorkingDay(id);
        }

        /// <summary>
        /// Gets the item linked to warehouse.
        /// </summary>
        /// <param name="linkWarehouse">The link warehouse.</param>
        /// <param name="linkDate">The link date.</param>
        /// <returns></returns>
        internal NonWorkingDay GetItemLinkedToWarehouse(string linkWarehouse, DateTime linkDate)
        {
            return NonWorkingDayController.GetNonWorkingDay(linkWarehouse, linkDate);
        }

        /// <summary>
        /// Gets all non working days.
        /// </summary>
        /// <returns></returns>
        internal List<NonWorkingDay> GetAllNonWorkingDays()
        {
            return NonWorkingDayController.GetNonWorkingDays(DateTime.Parse("1/1/1900"), DateTime.Today, -1, -1, "NonWorkingDate", 1, 20);
        }

        /// <summary>
        /// Gets the next working date.
        /// </summary>
        /// <param name="linkDate">The link date.</param>
        /// <param name="linkNonWorkingDayList">The link non working day list.</param>
        /// <returns></returns>
        internal DateTime GetNextWorkingDate(DateTime linkDate, List<NonWorkingDay> linkNonWorkingDayList)
        {
            return NonWorkingDayController.NextWorkingDate(linkDate, linkNonWorkingDayList);
        }

        /// <summary>
        /// Gets the items by region.
        /// </summary>
        /// <param name="linkRegionId">The link region id.</param>
        /// <returns></returns>
        internal List<NonWorkingDay> GetItemsByRegion(int linkRegionId)
        {
            return NonWorkingDayController.GetNonWorkingDaysByRegion(DateTime.Parse("1/1/1900"), DateTime.Today, linkRegionId);
        }

        /// <summary>
        /// Gets the items by warehouse.
        /// </summary>
        /// <param name="linkWarehouseId">The link warehouse id.</param>
        /// <returns></returns>
        internal List<NonWorkingDay> GetItemsByWarehouse(int linkWarehouseId)
        {
            return NonWorkingDayController.GetNonWorkingDaysByWarehouse(DateTime.Parse("1/1/1900"), DateTime.Today, linkWarehouseId);
        }

        /// <summary>
        /// Saves the item.
        /// </summary>
        /// <param name="nonWorkingDay">The non working day.</param>
        /// <returns></returns>
        internal int SaveItem(NonWorkingDay nonWorkingDay)
        {
            return NonWorkingDayController.SaveNonWorkingDay(nonWorkingDay);
        }

        /// <summary>
        /// Deletes the item.
        /// </summary>
        /// <param name="nonWorkingDay">The non working day.</param>
        /// <returns></returns>
        internal bool DeleteItem(NonWorkingDay nonWorkingDay)
        {
            return NonWorkingDayController.DeleteNonWorkingDay(nonWorkingDay);
        }

        internal int SaveItemsWithinRange(NonWorkingDay nonWorkingDay, int linkRegionId, int linkWarehouseId, bool linkWeekendOnly)
        {
            DateTime startDate = DateTime.Today;
            DateTime endDate = DateTime.Today.AddDays(10);
            string description  = " ";

            return NonWorkingDayController.SaveNonWorkingDays(startDate,
                                                              endDate,
                                                              description,
                                                              linkRegionId,
                                                              linkWarehouseId,
                                                              linkWeekendOnly,
                                                              nonWorkingDay.UpdatedBy);
        }

        /// <summary>
        /// Saves the item.
        /// </summary>
        [Test]
        public void SaveItem()
        {
            using (TransactionScope ts =new TransactionScope())
            {
                NonWorkingDay nonWorkingDay = PopulateNewItem();
                int Id = SaveItem(nonWorkingDay);
                Assert.IsTrue(Id != -1);
            }
            
        }

        /// <summary>
        /// Saves the items all regions all warehouses week days.
        /// </summary>
        [Test]
        public void SaveItemsAllRegionsAllWarehousesWeekDays()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                NonWorkingDay nonWorkingDay = PopulateNewItem();
                int regionId = -1;
                int warehouseId = -1;
                bool weekendOnly = false;

                int Id = SaveItemsWithinRange(nonWorkingDay, regionId, warehouseId, weekendOnly);
                Assert.IsTrue(Id != 0);
            }
        }

        /// <summary>
        /// Saves the items one regions all warehouses week days.
        /// </summary>
        [Test]
        public void SaveItemsOneRegionsAllWarehousesWeekDays()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                NonWorkingDay nonWorkingDay = PopulateNewItem();
                Warehouse warehouse = WarehouseController.GetWarehouse(nonWorkingDay.WarehouseId);

                int regionId = warehouse.RegionId;
                int warehouseId = -1;
                bool weekendOnly = false;

                int Id = SaveItemsWithinRange(nonWorkingDay, regionId, warehouseId, weekendOnly);
                Assert.IsTrue(Id != 0);
            }
        }

        /// <summary>
        /// Saves the items one region one warehouse week days.
        /// </summary>
        [Test]
        public void SaveItemsOneRegionOneWarehouseWeekDays()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                NonWorkingDay nonWorkingDay = PopulateNewItem();
                Warehouse warehouse = WarehouseController.GetWarehouse(nonWorkingDay.WarehouseId);

                int regionId = warehouse.RegionId;
                bool weekendOnly = false;

                int Id = SaveItemsWithinRange(nonWorkingDay, regionId, nonWorkingDay.WarehouseId, weekendOnly);
                Assert.IsTrue(Id != 0);
            }
        }

        /// <summary>
        /// Saves the items all regions all warehouses weekend.
        /// </summary>
        [Test]
        public void SaveItemsAllRegionsAllWarehousesWeekend()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                NonWorkingDay nonWorkingDay = PopulateNewItem();
                int regionId = -1;
                int warehouseId = -1;
                bool weekendOnly = true;

                int Id = SaveItemsWithinRange(nonWorkingDay, regionId, warehouseId, weekendOnly);
                Assert.IsTrue(Id != 0);
            }
        }

        /// <summary>
        /// Saves the items one regions all warehouses weekend.
        /// </summary>
        [Test]
        public void SaveItemsOneRegionsAllWarehousesWeekend()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                NonWorkingDay nonWorkingDay = PopulateNewItem();
                Warehouse warehouse = WarehouseController.GetWarehouse(nonWorkingDay.WarehouseId);

                int regionId = warehouse.RegionId;
                int warehouseId = -1;
                bool weekendOnly = true;

                int Id = SaveItemsWithinRange(nonWorkingDay, regionId, warehouseId, weekendOnly);
                Assert.IsTrue(Id != 0);
            }
        }

        /// <summary>
        /// Saves the items one region one warehouse weekend.
        /// </summary>
        [Test]
        public void SaveItemsOneRegionOneWarehouseWeekend()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                NonWorkingDay nonWorkingDay = PopulateNewItem();
                Warehouse warehouse = WarehouseController.GetWarehouse(nonWorkingDay.WarehouseId);

                int regionId = warehouse.RegionId;
                bool weekendOnly = true;

                int Id = SaveItemsWithinRange(nonWorkingDay, regionId, nonWorkingDay.WarehouseId, weekendOnly);
                Assert.IsTrue(Id != 0);
            }
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        [Test]
        public void GetItem()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                NonWorkingDay nonWorkingDay = PopulateNewItem();
                int Id = SaveItem(nonWorkingDay);

                if (Id != -1)
                    Assert.IsNotNull(GetItem(Id));
            }           
        }

        /// <summary>
        /// Tests the next working date.
        /// </summary>
        [Test]
        public void TestNextWorkingDate()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                NonWorkingDay nonWorkingDay = PopulateNewItem();
                int Id1 = SaveItem(nonWorkingDay);

                DateTime returnDate = nonWorkingDay.NonWorkingDate.AddDays(1);
                nonWorkingDay.NonWorkingDate = returnDate;
                nonWorkingDay.Description = returnDate.DayOfWeek.ToString();
                nonWorkingDay.Id = -1;
                int Id2 = SaveItem(nonWorkingDay);

                List<NonWorkingDay> nonWorkingDayList = GetAllNonWorkingDays();

                returnDate = GetNextWorkingDate(DateTime.Today, nonWorkingDayList);

                Assert.IsNotNull(returnDate == DateTime.Today);
            }
        }

        /// <summary>
        /// Deletes the item.
        /// </summary>
        [Test]
        public void DeleteItem()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                NonWorkingDay nonWorkingDay = PopulateNewItem();
                nonWorkingDay.Id = SaveItem(nonWorkingDay);
                if (nonWorkingDay.Id != -1)
                {
                    Assert.IsTrue(DeleteItem(nonWorkingDay));
                }
            }
        }

        /// <summary>
        /// Gets the item linked to warehouse.
        /// </summary>
        [Test]
        public void GetItemLinkedToWarehouse()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                NonWorkingDay nonWorkingDay = PopulateNewItem();
                nonWorkingDay.Id = SaveItem(nonWorkingDay);

                if (nonWorkingDay.Id != -1)
                {
                    Assert.IsNotNull(GetItemLinkedToWarehouse(nonWorkingDay.WarehouseCode, nonWorkingDay.NonWorkingDate));
                }
            }
        }

        /// <summary>
        /// Tests the get items by region.
        /// </summary>
        [Test]
        public void TestGetItemsByRegion()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                NonWorkingDay nonWorkingDay = PopulateNewItem();
                nonWorkingDay.Id = SaveItem(nonWorkingDay);

                if (nonWorkingDay.Id != -1)
                {
                    Warehouse warehouse = WarehouseController.GetWarehouse(nonWorkingDay.WarehouseId);
                    Assert.IsNotNull(GetItemsByRegion(warehouse.RegionId));
                }
            }
        }

        /// <summary>
        /// Tests the get items by warehouse.
        /// </summary>
        [Test]
        public void TestGetItemsByWarehouse()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                NonWorkingDay nonWorkingDay = PopulateNewItem();
                nonWorkingDay.Id = SaveItem(nonWorkingDay);

                if (nonWorkingDay.Id != -1)
                {
                    Assert.IsNotNull(GetItemsByWarehouse(nonWorkingDay.WarehouseId));
                }
            }
        }

        /// <summary>
        /// Tests the update non working day.
        /// </summary>
        [Test]
        public void TestUpdateNonWorkingDay()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                NonWorkingDay nonWorkingDay = PopulateNewItem();
                nonWorkingDay.Description = nonWorkingDay.NonWorkingDate.DayOfWeek.ToString();
                nonWorkingDay.Id = SaveItem(nonWorkingDay);
                nonWorkingDay = GetItem(nonWorkingDay.Id);
                //change a value
                nonWorkingDay.Description = "Updated";

                SaveItem(nonWorkingDay);
                nonWorkingDay = GetItem(nonWorkingDay.Id);
                Assert.IsTrue(nonWorkingDay.Description == "Updated");
            }
        }

        /// <summary>
        /// Saves the non working day test constraint.
        /// </summary>
        [Test]
        [ExpectedException(typeof(DiscoveryException))]
        public void SaveNonWorkingDayTestConstraint()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                NonWorkingDay nonWorkingDay = PopulateNewItem();
                SaveItem(nonWorkingDay);
                SaveItem(nonWorkingDay);
            }
        }

        /// <summary>
        /// Updates the non working day concurrency test.
        /// </summary>
        [Test]
        [ExpectedException(typeof(DiscoveryException))]
        //[ExpectedException(typeof (ConcurrencyException))]
        public void UpdateNonWorkingDayConcurrencyTest()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                NonWorkingDay nonWorkingDay = PopulateNewItem();
                nonWorkingDay.Id = SaveItem(nonWorkingDay);
                //We didn't get the new checksum so when we save again an exception should be thrown
                try
                {
                    SaveItem(nonWorkingDay);
                }
                catch (DiscoveryException e)
                {
                    Assert.IsInstanceOfType(typeof(ConcurrencyException), e.InnerException);
                    throw e;
                }
            }
        }


    }
}
