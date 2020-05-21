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
    public class OpcoTests
    {
        public static int SaveItem(OpCo opCo)
        {
            return OpcoController.SaveOpCo(opCo);
        }

        public static OpCo PopulateNewItem()
        {
            OpCo opCo = new OpCo();
            opCo.Code = Guid.NewGuid().ToString().Substring(0,3);
            opCo.Description = "Test";
            opCo.FtpIP = "Test";
            opCo.FtpPassword = "test";
            opCo.FtpUserName = "test";
            SalesLocationTests tests = new SalesLocationTests();
            opCo.UpdatedBy = "test";
            return opCo;
        }

        [Test]
        public  void SaveItem()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //create a new item, save it and check the retuned id
                Assert.IsTrue(SaveItem(PopulateNewItem()) != -1);
            }
        }

        [Test]
        public void UpdateItem()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                OpCo item = PopulateNewItem();

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
        [ExpectedException(typeof(DiscoveryException))]
        public void ConcurrencyTest()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    OpCo item = PopulateNewItem();
                    item.Id = SaveItem(item);
                    //change a value
                    item.Description = "Updated";

                    SaveItem(item);
                }
                catch (DiscoveryException e)
                {
                    Assert.IsInstanceOfType(typeof(ConcurrencyException), e.InnerException);
                    throw e;
                }
            }
        }

        [Test]
        [ExpectedException(typeof(DiscoveryException))]
        public void SaveOpCoTestConstraint()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //get a new item
                OpCo opCo = PopulateNewItem();
                //save the item
                SaveItem(opCo);
                //save the item again, because it's id still =-1 then another insert will occur and because the item
                //has the same code a constraint will be violated
                SaveItem(opCo);
            }
        }

        internal OpCo GetItem(int id, bool recursive)
        {
            return OpcoController.GetOpCo(id, recursive);
        }

        [Test]
        public void GetItem()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                Assert.IsNotNull(GetItem(SaveItem(PopulateNewItem()), true));
                
                
            }
           
        }

        //[Test]
        //public void GetItemRecursive()
        //{
        //    using (TransactionScope scope = new TransactionScope())
        //    {
        //        OpCo opCo = PopulateNewItem();
        //        int id = SaveItem(opCo);
           
        //        opCo = GetItem(id, true);
        //        Assert.IsNotNull(opCo);
        //        Assert.IsNotNull(opCo.SalesLocation);
                
                
        //    }
            
        //}

        [Test]
        public void GetItems()
        {
            using (TransactionScope scope = new TransactionScope())
            {

                int id=SaveItem(PopulateNewItem());

                //retrieve all opcos and the one we saved should return at least
                List<OpCo> opcos=OpcoController.GetOpCos(false);
                //so the count should be >0
                Assert.IsTrue(opcos.Count > 0);
                //check for our new id
                Assert.IsTrue(opcos.Find(delegate(OpCo currentItem)
                                                   {
                                                       return currentItem.Id == id ;
                                                   }) != null);
                
                
            }
        }

        internal bool DeleteItem(int Id)
        {
            OpCo opco = new OpCo();
            opco.Id = Id;
            return OpcoController.DeleteOpCo(opco);
        }

        [Test]
        public void DeleteItem()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                int id = SaveItem(PopulateNewItem());
                Assert.IsTrue(DeleteItem(id));
                Assert.IsNull(OpcoController.GetOpCo(id,false));
              
                
                
            }
        }
    }
}