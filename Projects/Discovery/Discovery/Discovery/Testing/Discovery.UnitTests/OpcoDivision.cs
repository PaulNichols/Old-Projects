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
    public class OpcoDivisionTests
    {
         public static int SaveItem(OpCoDivision opCoDivision)
        {
            return OpcoDivisionController.SaveOpCoDivision(opCoDivision);
        }

        public static OpCoDivision PopulateNewItem()
        {
            OpCoDivision opCoDivision = new OpCoDivision();
            opCoDivision.Code = Guid.NewGuid().ToString().Substring(0, 3);
            opCoDivision.Logo =new byte[1];
            opCoDivision.LogoURI = "uri";
            opCoDivision.OpCo = OpcoTests.PopulateNewItem();
            opCoDivision.OpCoId = OpcoTests.SaveItem(opCoDivision.OpCo);
            opCoDivision.UpdatedBy = "test";
            return opCoDivision;
        }

        [Test]
        public void SaveItem()
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
                OpCoDivision item = PopulateNewItem();


                item.Id = SaveItem(item);
                item = GetItem(item.Id, true);
                //change a value
                string newCode = Guid.NewGuid().ToString().Substring(0, 3);
                item.Code = newCode;

                SaveItem(item);
                item = GetItem(item.Id, true);
                Assert.IsTrue(item.Code == newCode);
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
                    OpCoDivision item = PopulateNewItem();
                    item.Id = SaveItem(item);
                    //change a value
                    item.Code = Guid.NewGuid().ToString().Substring(0, 3);

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
        public void SaveOpCoDivisionTestConstraint()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //get a new item
                OpCoDivision opCoDivision = PopulateNewItem();
                //save the item
                SaveItem(opCoDivision);
                //save the item again, because it's id still =-1 then another insert will occur and because the item
                //has the same code a constraint will be violated
                SaveItem(opCoDivision);
            }
        }

        internal OpCoDivision GetItem(int id, bool recursive)
        {
            return OpcoDivisionController.GetOpCoDivision(id, recursive);
        }

        [Test]
        public void GetItem()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                Assert.IsNotNull(GetItem(SaveItem(PopulateNewItem()), true));


            }

        }

        [Test]
        public void GetItems()
        {
            using (TransactionScope scope = new TransactionScope())
            {

                int id = SaveItem(PopulateNewItem());

                //retrieve all opcos and the one we saved should return at least
                List<OpCoDivision> opcoDivisions = OpcoDivisionController.GetOpCoDivisions(false);
                //so the count should be >0
                Assert.IsTrue(opcoDivisions.Count > 0);
                //check for our new id
                Assert.IsTrue(opcoDivisions.Find(delegate(OpCoDivision currentItem)
                                                   {
                                                       return currentItem.Id == id;
                                                   }) != null);


            }
        }

        internal bool DeleteItem(int Id)
        {
            OpCoDivision opcoDivision = new OpCoDivision();
            opcoDivision.Id = Id;
            return OpcoDivisionController.DeleteOpCoDivision(opcoDivision);
        }

        [Test]
        public void DeleteItem()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                int id = SaveItem(PopulateNewItem());
                Assert.IsTrue(DeleteItem(id));
                Assert.IsNull(OpcoDivisionController.GetOpCoDivision(id, false));



            }
        }
    }
}