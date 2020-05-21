using System;
using System.Collections.Generic;
using System.Reflection;
using System.Transactions;
using Discovery.BusinessObjects;
using Discovery.BusinessObjects.Controllers;
using Discovery.ComponentServices.ExceptionHandling;
using Discovery.Utility.DataAccess.Exceptions;
using NUnit.Framework;

namespace Discovery.UnitTests
{
    [TestFixture]
    public class TripTests : BaseTest
    {
        [Test]
        public void Property_DeliveryDate()
        {
            //set StartDate property to a test value
            object newValue = DateTime.Today;
            string propertyToTest = "StartDate";
            List<string> propertiesToExclude = new List<string>(new string[] { propertyToTest, "ValidationMessages","ValidationMessagesAll","ViolationsDictionary" ,"Validator"});
            
            BasicPropertyTest<Trip>(newValue, propertiesToExclude, propertyToTest);
        }

        [Test]
        public void Property_TripNumber()
        {
            //set TripNumber property to a test value
            object newValue = "12345678";
            string propertyToTest = "TripNumber";
            List<string> propertiesToExclude = new List<string>(new string[] { propertyToTest, "ValidationMessages","ValidationMessagesAll","ViolationsDictionary" ,"Validator"});

            BasicPropertyTest<Trip>(newValue, propertiesToExclude, propertyToTest);
        }

        [Test]
        public void Property_WarehouseId()
        {
            //set TripNumber property to a test value
            object newValue = new Random(1).Next();
            string propertyToTest = "WarehouseId";
            List<string> propertiesToExclude = new List<string>(new string[] { propertyToTest, "ValidationMessages","ValidationMessagesAll","ViolationsDictionary" ,"Validator"});

            BasicPropertyTest<Trip>(newValue, propertiesToExclude, propertyToTest);
        }

        internal Trip PopulateNewItem()
        {
            Trip trip = new Trip();
            trip.StartDate = DateTime.Today;
            trip.TripNumber = "12345678";

            Warehouse warehouse = WarehouseTests.PopulateNewItem();
            trip.WarehouseId = WarehouseController.SaveWarehouse(warehouse);

            trip.LeaveTime = DateTime.Now.ToShortTimeString();
            trip.FinishTime = DateTime.Now.AddMinutes(10).ToShortTimeString();
            trip.PeakWeight = 1;
            trip.PeakVolume = 2;
            trip.MaximumLoadWeight = 1;
            trip.MaximumLoadVolume = 2;
            trip.VehicleCost = 99;
            
            trip.RegionId = warehouse.RegionId;
            trip.VehicleRegistration = "NN56 TDC";
            trip.AssignedDriver = "TDC AssignedDriver";
            trip.TotalDistance = 100;
            //trip.LoadingTime = 20;
            //trip.TravellingTime = 30;
            //trip.WaitingTime = 40;
            trip.UpdatedBy = "TDC Team";

            return trip;
        }

        internal Trip GetItem(int linkId)
        {
            return TripController.GetTrip(linkId);
        }

        internal List<Trip> GetTripSummaries(int linkWarehouseId)
        {
            return TripController.GetTripSummaries(
                linkWarehouseId,
                Convert.ToDateTime("01/01/1900"),
                Convert.ToDateTime("10/12/2099"),
                "");
        }

        internal Trip GetTripByWarehouseDateAndNumber(Trip trip)
        {
            return TripController.GetTripByWarehouseDateAndNumber(
                trip.TripNumber, trip.StartDate, trip.WarehouseId);
        }

        internal int SaveItem(Trip trip)
        {
            return TripController.SaveTrip(trip);
        }

        [Test]
        public void TestGetTrip()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                Trip trip = PopulateNewItem();
                int Id = SaveItem(trip);

                if (Id != -1)
                    Assert.IsNotNull(GetItem(Id));
            }
        }

        [Test]
        public void TestGetTripSummaries()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                Trip trip = PopulateNewItem();
                int Id = SaveItem(trip);

                if (Id != -1)
                    Assert.IsNotNull(GetTripSummaries(trip.WarehouseId));
            }
        }

        [Test]
        public void TestGetTripByWarehouseDateAndNumber()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                Trip trip = PopulateNewItem();
                int Id = SaveItem(trip);
            
                if (Id != -1)
                    Assert.IsNotNull(GetTripByWarehouseDateAndNumber(trip));
            }
        }

        [Test]
        public void TestSaveTrip()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                Trip trip = PopulateNewItem();
                int Id = SaveItem(trip);
                Assert.IsTrue(Id != -1);
            }
        }

        [Test]
        public void TestUpdateTrip()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                Trip trip = PopulateNewItem();
                trip.StartDate = DateTime.Today;
                trip.Id = SaveItem(trip);
                trip = GetItem(trip.Id);
                //change a value
                trip.StartDate = DateTime.Today.AddDays(1);

                SaveItem(trip);
                trip = GetItem(trip.Id);
                Assert.IsTrue(trip.StartDate == DateTime.Today.AddDays(1));
            }
        }

        //[Test]
        //[ExpectedException(typeof(DiscoveryException))]
        //public void SaveTripTestConstraint()
        //{
        //    using (TransactionScope ts = new TransactionScope())
        //    {
        //        Trip trip = PopulateNewItem();
        //        SaveItem(trip);
        //        SaveItem(trip);
        //    }
        //}

        [Test]
        [ExpectedException(typeof (DiscoveryException))]
        //[ExpectedException(typeof (ConcurrencyException))]
            public void UpdateTripConcurrencyTest()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                Trip trip = PopulateNewItem();
                trip.Id = SaveItem(trip);
                //We didn't get the new checksum so when we save again an exception should be thrown
                try
                {
                    SaveItem(trip);
                }
                catch (DiscoveryException e)
                {
                    Assert.IsInstanceOfType(typeof (ConcurrencyException), e.InnerException);
                    throw e;
                }
            }
        }
    }
}