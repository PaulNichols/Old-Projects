using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Transactions;
using Discovery.BusinessObjects;
using Discovery.BusinessObjects.Controllers;
using Discovery.ComponentServices.ExceptionHandling;
using Discovery.Utility.DataAccess.Exceptions;
using NUnit.Framework;

namespace Discovery.UnitTests
{
    [TestFixture]
    public class RegionTests
    {
        internal int SaveItem(OptrakRegion optrakRegion)
        {
            return OptrakRegionController.SaveRegion(optrakRegion);
        }

        internal OptrakRegion PopulateNewItem()
        {
            OptrakRegion optrakRegion = new OptrakRegion();
            optrakRegion.Code = Guid.NewGuid().ToString().Substring(0,10);
            optrakRegion.Description = Guid.NewGuid().ToString();
            optrakRegion.UpdatedBy = "test";
            return optrakRegion;
        }

        [Test]
        public void SaveItem()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                OptrakRegion optrakRegion = PopulateNewItem();
                optrakRegion.Id = SaveItem(optrakRegion);
                Assert.IsTrue(optrakRegion.Id != -1);
                
                
            }
           
        }

        [Test]
        [ExpectedException(typeof (Discovery.ComponentServices.ExceptionHandling.DiscoveryException))]
        public void SaveRegionTestConstraint()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                OptrakRegion optrakRegion = PopulateNewItem();

                SaveItem(optrakRegion);
           
                SaveItem(optrakRegion);
                
                
            }
           
        }

        [Test]
        public void UpdateRegion()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                OptrakRegion optrakRegion = PopulateNewItem();
                optrakRegion.Description = "Original";
                optrakRegion.Id = SaveItem(optrakRegion);
                optrakRegion = GetItem(optrakRegion.Id);
                //change a value
                optrakRegion.Description = "Updated";
            
                SaveItem(optrakRegion);
                optrakRegion = GetItem(optrakRegion.Id);
                Assert.IsTrue(optrakRegion.Description == "Updated");
                
                
            }
           
        }

        [Test]
        [ExpectedException(typeof(DiscoveryException))]
        public void UpdateRegionConcurrencyTest()
        {
         

            using (TransactionScope ts = new TransactionScope())
            {
         
                try
                {
                      OptrakRegion optrakRegion = PopulateNewItem();
            optrakRegion.Id = SaveItem(optrakRegion);
            //change a value
            optrakRegion.Description = "Updated";
                      SaveItem(optrakRegion);
                }
                catch (DiscoveryException e)
                {
                    Assert.IsInstanceOfType(typeof(ConcurrencyException), e.InnerException);
                    throw e;
                }
            }
        }

        internal OptrakRegion GetItem(int id)
        {
            return OptrakRegionController.GetRegion(id);
        }

        [Test]
        public void GetItem()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                int id = SaveItem(PopulateNewItem());
            
                if (id != -1)
                    Assert.IsNotNull(GetItem(id));
                
                
            }
           
        }

       

        [Test]
        public void GetItems()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //add a region
                int id = OptrakRegionController.SaveRegion(PopulateNewItem());
                if (id > -1)
                {
                    //retrieve all regions and the one we saved should return at least
                    List<OptrakRegion> regions = OptrakRegionController.GetRegions();
                    //so the count should be >0
                    Assert.IsTrue(regions.Count > 0);
                    //check for our new id
                    Assert.IsTrue(regions.Find(delegate(OptrakRegion currentItem)
                                                       {
                                                           return currentItem.Id == id;
                                                       }) != null);
                }
            }


        }

        internal bool DeleteItem(OptrakRegion optrakRegion)
        {
            return OptrakRegionController.DeleteRegion(optrakRegion);
        }

        [Test]
        public void DeleteItem()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                int id = SaveItem(PopulateNewItem());
                if (id != -1)
                {
                    OptrakRegion optrakRegionToDelete = new OptrakRegion();
                    optrakRegionToDelete.Id = id;
                    Assert.IsTrue(DeleteItem(optrakRegionToDelete));
                }
               
                
            }
        }
    }
}