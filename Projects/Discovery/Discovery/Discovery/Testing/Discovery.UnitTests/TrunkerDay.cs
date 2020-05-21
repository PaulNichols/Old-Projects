using System.Collections.Generic;
using System.Data.SqlClient;
using System.Transactions;
using Discovery.BusinessObjects;
using Discovery.BusinessObjects.Controllers;
using NUnit.Framework;

namespace Discovery.UnitTests
{
    [TestFixture]
    public class TrunkerDayTests
    {
        internal int SaveItem(TrunkerDay trunkerDay)
        {
            return TrunkerDaysController.SaveTrunkerDay(trunkerDay);
        }

        internal TrunkerDay PopulateNewItem()
        {
            TrunkerDay trunkerDay = new TrunkerDay();
            trunkerDay.Days = 3;
            trunkerDay.UpdatedBy = "IanKilsby";

            // Set up 2 Warehouses
            // First warehouse is IPS created by using the warehouse pouulate method


            trunkerDay.SourceWarehouse = WarehouseTests.PopulateNewItem();
            trunkerDay.SourceWarehouse.Code = "IPS";
            trunkerDay.SourceWarehouseId = WarehouseTests.SaveItem(trunkerDay.SourceWarehouse);


            // Second warehouse cannot use the populate method due to duplicate foreign keys e.g. printer etc

            trunkerDay.DestinationWarehouse = new Warehouse();
            trunkerDay.DestinationWarehouse.Description = "London Warehouse";
            trunkerDay.DestinationWarehouse.UpdatedBy = "IanKilsby";
            trunkerDay.DestinationWarehouse.Contact = new Contact();
            trunkerDay.DestinationWarehouse.Contact.Name = "ContactName";
            trunkerDay.DestinationWarehouse.Contact.TelephoneNumber = "TelephoneNumber";
            trunkerDay.DestinationWarehouse.Contact.Email = "SalesEmail";
            trunkerDay.DestinationWarehouse.HasCommander = true;
            trunkerDay.DestinationWarehouse.HasOptrak = true;
            trunkerDay.DestinationWarehouse.IsTDC = true;

           // trunkerDay.DestinationWarehouse.PrinterName = trunkerDay.SourceWarehouse.PrinterName;
            //trunkerDay.DestinationWarehouse.PrinterId = trunkerDay.SourceWarehouse.Printer.Id;

            trunkerDay.DestinationWarehouse.OptrakRegion = trunkerDay.SourceWarehouse.OptrakRegion;
            trunkerDay.DestinationWarehouse.OptrakRegion.Id = trunkerDay.SourceWarehouse.RegionId;
            trunkerDay.DestinationWarehouse.RegionId = trunkerDay.SourceWarehouse.RegionId;
            trunkerDay.DestinationWarehouse.Address = new Address();
            trunkerDay.DestinationWarehouse.Address.Line1 = "line1";
            trunkerDay.DestinationWarehouse.Address.Line2 = "line2";
            trunkerDay.DestinationWarehouse.Address.Line3 = "line3";
            trunkerDay.DestinationWarehouse.Address.Line4 = "line4";
            trunkerDay.DestinationWarehouse.Address.PostCode = "PostCode";
            trunkerDay.DestinationWarehouse.Code = "LON";
            trunkerDay.DestinationWarehouseId = WarehouseTests.SaveItem(trunkerDay.DestinationWarehouse);

            return trunkerDay;
        }

        [Test]
        public void GetItem()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                TrunkerDay trunkerDay = PopulateNewItem();
                trunkerDay.Id = SaveItem(trunkerDay);

                if (trunkerDay.Id != -1)
                    Assert.IsNotNull(TrunkerDaysController.GetTrunkerDay(trunkerDay.Id, false));
            }
        }

        [Test]
        public void GetItem_FullPopulate()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                TrunkerDay trunkerDay = PopulateNewItem();
                trunkerDay.Id = SaveItem(trunkerDay);

                if (trunkerDay.Id != -1)
                {
                    Assert.IsNotNull(TrunkerDaysController.GetTrunkerDay(trunkerDay.Id, true).DestinationWarehouse);
                }
            }
        }

        [Test]
        public void GetItems()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //add a trunker day
                int id = TrunkerDaysController.SaveTrunkerDay(PopulateNewItem());
                if (id > -1)
                {
                    //retrieve all trunker days and the one we saved should return at least
                    List<TrunkerDay> trunkerDays = TrunkerDaysController.GetTrunkerDays(false);

                    //so the count should be >0
                    Assert.IsTrue(trunkerDays.Count > 0);
                    //check for our new id
                    Assert.IsTrue(trunkerDays.Find(delegate(TrunkerDay currentItem)
                                                       {
                                                           return currentItem.Id == id;
                                                       }) != null);
                }
            }


        }


        [Test]
        public void GetItems_CheckSort()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //add a trunker day with a high number of days
                TrunkerDay trunkerDay = PopulateNewItem();
                trunkerDay.Days = 10;
                int id = TrunkerDaysController.SaveTrunkerDay(trunkerDay);
                if (id > -1)
                {
                    //add a trunker day with a lower number of days
                    trunkerDay = trunkerDay.DeepClone<TrunkerDay>();
                    trunkerDay.Id = -1;
                    trunkerDay.DestinationWarehouse = WarehouseTests.PopulateNewItem();
                    trunkerDay.DestinationWarehouse.Id=WarehouseController.SaveWarehouse(trunkerDay.DestinationWarehouse);
                    trunkerDay.DestinationWarehouseId = trunkerDay.DestinationWarehouse.Id;
                    trunkerDay.Days = 5;
                    int id2 = TrunkerDaysController.SaveTrunkerDay(trunkerDay);
                    
                    if (id2 > -1)
                    {
                        //retrieve all trunker days and the one we saved should return at least
                        List<TrunkerDay> trunkerDays = TrunkerDaysController.GetTrunkerDays(false,"Days");

                        //so the count should be >0
                        Assert.IsTrue(trunkerDays.Count > 0);
                        //find the two we added
                        trunkerDays = trunkerDays.FindAll(delegate(TrunkerDay currentItem)
                                                              {
                                                                  return currentItem.Id == id || currentItem.Id == id2;
                                                              });
                        Assert.IsTrue(trunkerDays.Count==2);

                        Assert.IsTrue(trunkerDays[0].Days < trunkerDays[1].Days);
                    }
                }
            }
        }

        [Test]
        public void SaveItem()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                TrunkerDay trunkerDay = PopulateNewItem();
                trunkerDay.Id = SaveItem(trunkerDay);
                Assert.IsTrue(trunkerDay.Id != -1);
            }

        }

        internal bool DeleteItem(TrunkerDay trunkerDay)
        {
            return TrunkerDaysController.DeleteTrunkerDay(trunkerDay);
        }

        [Test]
        public void DeleteItem()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                TrunkerDay trunkerDay = PopulateNewItem();
                trunkerDay.Id = SaveItem(trunkerDay);
                if (trunkerDay.Id != -1)
                {
                    Assert.IsTrue(DeleteItem(trunkerDay));

                }

            }
        }

        [Test]
        public void DeleteItem_NoId()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                TrunkerDay trunkerDay = PopulateNewItem();
                // trunkerDay.Id = SaveItem(trunkerDay);

                Assert.IsFalse(DeleteItem(trunkerDay));



            }
        }

        [Test]
        [ExpectedException(typeof(InValidBusinessObjectException))]
        public void SaveTrunkerDayTestConstraint()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                TrunkerDay trunkerDay = PopulateNewItem();

                trunkerDay.DestinationWarehouseId = trunkerDay.SourceWarehouseId;
                SaveItem(trunkerDay);

            }
        }

    }
}



