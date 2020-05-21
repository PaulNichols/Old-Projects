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
    public class SalesLocationTests
    {
        internal int SaveItem(SalesLocation salesLocation)
        {
            return SalesLocationController.SaveLocation(salesLocation);
        }

        internal SalesLocation PopulateNewItem()
        {
            SalesLocation salesLocation = new SalesLocation();
            salesLocation.TelephoneNumber = "TelephoneNumber";
            salesLocation.Location = "Location";
            
            salesLocation.OpCo = OpcoTests.PopulateNewItem();
            salesLocation.OpCo.Id=OpcoController.SaveOpCo(salesLocation.OpCo);
            salesLocation.OpCoId = salesLocation.OpCo.Id;
            
            salesLocation.Description = "Test";
            salesLocation.UpdatedBy = "test";
            return salesLocation;
        }

        [Test]
        public void UpdateItem()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                SalesLocation item = PopulateNewItem();
                item.Description = "Original";
                item.Id = SaveItem(item);


                item = GetItem(item.Id, true);
                //change a value
                item.Description = "Updated";
                SaveItem(item);
                item = GetItem(item.Id, true);
                Assert.IsTrue(item.Description == "Updated");
            }
        }

        [Test]
        [ExpectedException(typeof (DiscoveryException))]
        public void ConcurrencyTest()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    SalesLocation item = PopulateNewItem();
                    item.Id = SaveItem(item);
                    //change a value
                    item.Description = "Updated";

                    SaveItem(item);
                }
                catch (DiscoveryException e)
                {
                    Assert.IsInstanceOfType(typeof (ConcurrencyException), e.InnerException);
                    throw e;
                }
            }
        }

        [Test]
        public void SaveItem()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                int id = SaveItem(PopulateNewItem());
                Assert.IsTrue(id != -1);
            }
               
           
        }

        [Test]
        [ExpectedException(typeof (DiscoveryException))]
        public void SaveLocationTestConstraint()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                SalesLocation salesLocation = PopulateNewItem();

                SaveItem(salesLocation);

                SaveItem(salesLocation);
            }
        }

        internal SalesLocation GetItem(int id, bool recursive)
        {
            return SalesLocationController.GetLocation(id, recursive);
        }

        [Test]
        public void GetItem()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                SalesLocation salesLocation = PopulateNewItem();
                int id = SaveItem(salesLocation);


                Assert.IsNotNull(GetItem(id, true));
            }
        }


        [Test]
        public void GetItems()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                SalesLocation salesLocation = PopulateNewItem();
                int id = SaveItem(salesLocation);

                //retrieve all sales locations and the one we saved should return at least
                List<SalesLocation> salesLocations = SalesLocationController.GetLocations(true);
                //so the count should be >0
                Assert.IsTrue(salesLocations.Count > 0);
                //check for our new id
                Assert.IsTrue(salesLocations.Find(delegate(SalesLocation currentItem)
                                                      {
                                                          return (currentItem.Id == id);
                                                      }) != null);
                
                
            }
        }

        internal bool DeleteItem(int Id)
        {
            SalesLocation salesLocation = new SalesLocation();
            salesLocation.Id = Id;
            return SalesLocationController.DeleteLocation(salesLocation);
        }

        [Test]
        public void DeleteItem()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                SalesLocation salesLocation = PopulateNewItem();
                int id = SaveItem(salesLocation);
                Assert.IsTrue(DeleteItem(id));
                
                
            }
        }
    }
}