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
    public class TransactionSubTypeTests
    {
        public static int SaveItem(TransactionSubType transactionSubType)
        {
            return TransactionSubTypeController.SaveTransactionSubType(transactionSubType);
        }

        public static TransactionSubType PopulateNewItem()
        {
            TransactionSubType transactionSubType = new TransactionSubType();
            transactionSubType.Code = Guid.NewGuid().ToString().Substring(0, 9);;
            transactionSubType.Description = "Test";
            transactionSubType.UpdatedBy = "test";
            return transactionSubType;
        }

        [Test]
        public  void SaveItem()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                TransactionSubType transactionSubType = PopulateNewItem();
                transactionSubType.Id = SaveItem(transactionSubType);
                Assert.IsTrue(transactionSubType.Id != -1);
            }
          
        }

        [Test]
        [ExpectedException(typeof(Discovery.ComponentServices.ExceptionHandling.DiscoveryException))]
        public void SaveTransactionSubTypeTestConstraint()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                TransactionSubType transactionSubType = PopulateNewItem();

                SaveItem(transactionSubType);
           
                SaveItem(transactionSubType);
            }
          
        }

        [Test]
        public void UpdateTransactionSubType()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                TransactionSubType transactionSubType = PopulateNewItem();
                transactionSubType.Description = "Original";
                transactionSubType.Id = SaveItem(transactionSubType);
                transactionSubType = GetItem(transactionSubType.Id);
                //change a value
                transactionSubType.Description = "Updated";
           
                SaveItem(transactionSubType);
                transactionSubType = GetItem(transactionSubType.Id);
                Assert.IsTrue(transactionSubType.Description == "Updated");
                
            }
        }

        [Test]
        [ExpectedException(typeof(DiscoveryException))]
        public void UpdateTransactionSubTypeConcurrencyTest()
        {
       

            using (TransactionScope ts = new TransactionScope())
            {

                try
                {
                    TransactionSubType transactionSubType = PopulateNewItem();
                    transactionSubType.Id = SaveItem(transactionSubType);
                    SaveItem(transactionSubType);
                }
                catch (DiscoveryException e)
                {
                    Assert.IsInstanceOfType(typeof(ConcurrencyException), e.InnerException);
                    throw e;
                }
            }
        }

        internal TransactionSubType GetItem(int id)
        {
            return TransactionSubTypeController.GetTransactionSubType(id);
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
           
                //retrieve all transaction Sub Types and the one we saved should return at least
                List<TransactionSubType> transactionSubTypes = TransactionSubTypeController.GetTransactionSubTypes();
                //so the count should be >0
                Assert.IsTrue(transactionSubTypes.Count > 0);
                //check for our new id
                Assert.IsTrue(transactionSubTypes.Find(delegate(TransactionSubType currentItem)
                                                   {
                                                       return currentItem.Id == id ;
                                                   }) != null);
            }
          
        }

        internal bool DeleteItem(TransactionSubType transactionSubType)
        {
            return TransactionSubTypeController.DeleteTransactionSubType(transactionSubType);
        }

        [Test]
        public void DeleteItem()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                int id = SaveItem(PopulateNewItem());
                if (id != -1)
                {
                    TransactionSubType transactionSubTypeToDelete = new TransactionSubType();
                    transactionSubTypeToDelete.Id = id;
                    Assert.IsTrue(DeleteItem(transactionSubTypeToDelete));
                }
            }
        }
    }
}