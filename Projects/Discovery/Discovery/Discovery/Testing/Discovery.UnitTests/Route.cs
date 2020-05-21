using System;
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
    public class RouteTests
    {
        static internal int SaveItem(Route route)
        {
            return RouteController.SaveRoute(route);
        }

        static internal Route PopulateNewItem()
        {
            Route route = new Route();
            route.Code = Guid.NewGuid().ToString().Substring(0, 9);
            route.Description = "Description";
            route.UpdatedBy = "test";
            route.IsCollection = true;
            route.IsNextDay = true;
            route.IsSameDay = false;
            route.IsSpecial = false;
            //route.Warehouse = WarehouseTests.PopulateNewItem();
            //route.WarehouseId=WarehouseController.SaveWarehouse(route.Warehouse);
            //route.Warehouse.Id = route.WarehouseId;
            
            return route;
        }

        [Test]
        public void SaveItem()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                Route route = PopulateNewItem();
                route.Id = SaveItem(route);
                Assert.IsTrue(route.Id != -1);
                
                
            }
            
        }

        [Test]
        [ExpectedException(typeof(Discovery.ComponentServices.ExceptionHandling.DiscoveryException))]
        public void SaveRouteTestConstraint()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                Route route = PopulateNewItem();

                SaveItem(route);
           
            
                SaveItem(route);
                
                
            }
           
        }

        [Test]
        public void UpdateRoute()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                Route route = PopulateNewItem();
                route.Description = "Original";
                route.Id = SaveItem(route);
                route = GetItem(route.Id);
                //change a value
                route.Description = "Updated";
            
                SaveItem(route);
                route = GetItem(route.Id);
                Assert.IsTrue(route.Description == "Updated");
                
                
            }
           
        }

        [Test]
        [ExpectedException(typeof(DiscoveryException))]
        public void UpdateRouteConcurrencyTest()
        {
  

            using (TransactionScope ts = new TransactionScope())
            {
               
                try
                {
                    Route route = PopulateNewItem();
                    route.Id = SaveItem(route);
                    //change a value
                    route.Description = "Updated";

                    SaveItem(route);
                
                }
                catch (DiscoveryException e)
                {
                    Assert.IsInstanceOfType(typeof(ConcurrencyException), e.InnerException);
                    throw e;
                }
            }
        }

        internal Route GetItem(int id)
        {
            return RouteController.GetRoute(id);
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
                int id = SaveItem(PopulateNewItem());
           
                if (id != -1)
                    Assert.IsTrue(RouteController.GetRoutes("", 0, 0, false).Count > 0);
                
                
            }
           
        }

        internal bool DeleteItem(Route route)
        {
            return RouteController.DeleteRoute(route);
        }

        [Test]
        public void DeleteItem()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                int id = SaveItem(PopulateNewItem());
                if (id != -1)
                {
                    Route routeToDelete = new Route();
                    routeToDelete.Id = id;
                    Assert.IsTrue(DeleteItem(routeToDelete));
                }
                
                
            }
        }
    }
}