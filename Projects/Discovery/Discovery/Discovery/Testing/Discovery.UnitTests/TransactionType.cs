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
    public class TransactionTypeTests:BaseTest
    {
        static internal int SaveItem(TransactionType transactionType)
        {
            return TransactionTypeController.SaveTransactionType(transactionType);
        }

        static internal TransactionType PopulateNewItem()
        {
            TransactionType transactionType = new TransactionType();
            transactionType.Code = Guid.NewGuid().ToString().Substring(0, 9);;
            transactionType.IsCollection = true;
            transactionType.IsSample = true;
            transactionType.Description = "Test";
            transactionType.UpdatedBy = "test";
            return transactionType;
        }

        [Test]
        public void Property_IsReturn()
        {
            string propertyToTest = "IsReturn";
            object newValue = true;
            List<string> propertiesToExclude = new List<string>(new string[] { propertyToTest,"IsValid", "ValidationMessages","ValidationMessagesAll","ViolationsDictionary" ,"Validator"});

            BasicPropertyTest<TransactionType>(newValue, propertiesToExclude, propertyToTest);
        }
        
        [Test]
        public void SaveItem()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                TransactionType transactionType = PopulateNewItem();
                transactionType.Id = SaveItem(transactionType);
                Assert.IsTrue(transactionType.Id != -1);
            }
        }

        [Test]
        [ExpectedException(typeof (DiscoveryException))]
        public void SaveTransactionTypeTestConstraint()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                TransactionType transactionType = PopulateNewItem();

                SaveItem(transactionType);

                SaveItem(transactionType);
            }
        }

        [Test]
        public void UpdateTransactionType()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                TransactionType transactionType = PopulateNewItem();
                transactionType.Description = "Original";
                transactionType.Id = SaveItem(transactionType);
                transactionType = GetItem(transactionType.Id);
                //change a value
                transactionType.Description = "Updated";
           
                SaveItem(transactionType);
                transactionType = GetItem(transactionType.Id);
                Assert.IsTrue(transactionType.Description == "Updated");
            }
            
        }

        [Test]
        [ExpectedException(typeof(DiscoveryException))]
        public void UpdateTransactionTypeConcurrencyTest()
        {
            using (TransactionScope ts = new TransactionScope())
            {

                try
                {
                    TransactionType transactionType = PopulateNewItem();
                    transactionType.Id = SaveItem(transactionType);
                    //change a value
                    transactionType.Description = "Updated";

                    SaveItem(transactionType);

                }
                catch (DiscoveryException e)
                {
                    Assert.IsInstanceOfType(typeof(ConcurrencyException), e.InnerException);
                    throw e;
                }
            }
          
           
        }

        internal TransactionType GetItem(int id)
        {
            return TransactionTypeController.GetTransactionType(id);
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



                //retrieve all transaction Types and the one we saved should return at least
                List<TransactionType> transactionTypes = TransactionTypeController.GetTransactionTypes();
                //so the count should be >0
                Assert.IsTrue(transactionTypes.Count > 0);
                //check for our new id
                Assert.IsTrue(transactionTypes.Find(delegate(TransactionType currentItem)
                                                        {
                                                            return currentItem.Id == id ;
                                                        }) != null);
            }
           
        }

        internal bool DeleteItem(TransactionType transactionType)
        {
            return TransactionTypeController.DeleteTransactionType(transactionType);
        }

        [Test]
        public void DeleteItem()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                int id = SaveItem(PopulateNewItem());
                if (id != -1)
                {
                    TransactionType transactionTypeToDelete = new TransactionType();
                    transactionTypeToDelete.Id = id;
                    Assert.IsTrue(DeleteItem(transactionTypeToDelete));
                }
            }
        }
    }
}