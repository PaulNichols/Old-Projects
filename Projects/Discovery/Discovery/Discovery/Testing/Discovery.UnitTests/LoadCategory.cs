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
    public class LoadCategoryTests
    {
        /// <summary>
        /// Populates the new item.
        /// </summary>
        /// <returns></returns>
        internal LoadCategory PopulateNewItem()
        {
            LoadCategory loadCategory = new LoadCategory();
            loadCategory.Code = "TEST";
            loadCategory.Description = "Test";
            loadCategory.UpdatedBy = "TDC Team";
            return loadCategory;
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        internal LoadCategory GetItem(int id)
        {
            return LoadCategoryController.GetLoadCategory(id);
        }

        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <returns></returns>
        internal List<LoadCategory> GetAllItems()
        {
            return LoadCategoryController.GetLoadCategories();
        }

        /// <summary>
        /// Saves the item.
        /// </summary>
        /// <param name="loadCategory">The load category.</param>
        /// <returns></returns>
        internal int SaveItem(LoadCategory loadCategory)
        {
            return LoadCategoryController.SaveLoadCategory(loadCategory);
        }

        /// <summary>
        /// Deletes the item.
        /// </summary>
        /// <param name="loadCategory">The load category.</param>
        /// <returns></returns>
        internal bool DeleteItem(LoadCategory loadCategory)
        {
            return LoadCategoryController.DeleteLoadCategory(loadCategory);
        }

        /// <summary>
        /// Tests the get load category.
        /// </summary>
        [Test]
        public void TestGetLoadCategory()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                LoadCategory loadCategory = PopulateNewItem();
                int Id = SaveItem(loadCategory);

                if (Id != -1)
                    Assert.IsNotNull(GetItem(Id));
            }
        }

        /// <summary>
        /// Tests the get load categories.
        /// </summary>
        [Test]
        public void TestGetLoadCategories()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                LoadCategory loadCategory = PopulateNewItem();
                SaveItem(loadCategory);
                loadCategory.Id = -1;
                loadCategory.Code = "TWO";
                loadCategory.Description = "Second";
                SaveItem(loadCategory);

                List<LoadCategory> loadCategoryList = GetAllItems();
                Assert.IsTrue(loadCategoryList.Count != 0);
            }
        }

        /// <summary>
        /// Tests the save load category.
        /// </summary>
        [Test]
        public void TestSaveLoadCategory()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                LoadCategory loadCategory = PopulateNewItem();
                int Id = SaveItem(loadCategory);
                Assert.IsTrue(Id != -1);
            }
        }

        /// <summary>
        /// Tests the update load category.
        /// </summary>
        [Test]
        public void TestUpdateLoadCategory()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                LoadCategory loadCategory = PopulateNewItem();
                loadCategory.Description = "Original";
                loadCategory.Id = SaveItem(loadCategory);
                loadCategory = GetItem(loadCategory.Id);
                //change a value
                loadCategory.Description = "Updated";

                SaveItem(loadCategory);
                loadCategory = GetItem(loadCategory.Id);
                Assert.IsTrue(loadCategory.Description == "Updated");
            }
        }

        /// <summary>
        /// Tests the delete load category.
        /// </summary>
       [Test]
        public void TestDeleteLoadCategory()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                int id = SaveItem(PopulateNewItem());
                if (id != -1)
                {
                    LoadCategory loadCategoryToDelete = new LoadCategory();
                    loadCategoryToDelete.Id = id;
                    Assert.IsTrue(DeleteItem(loadCategoryToDelete));
                }
            }
        }

        /// <summary>
        /// Saves the load category test constraint.
        /// </summary>
        [Test]
        [ExpectedException(typeof(DiscoveryException))]
        public void SaveLoadCategoryTestConstraint()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                LoadCategory loadCategory = PopulateNewItem();
                SaveItem(loadCategory);
                SaveItem(loadCategory);
            }
        }

        /// <summary>
        /// Updates the load category concurrency test.
        /// </summary>
        [Test]
        [ExpectedException(typeof(DiscoveryException))]
        //[ExpectedException(typeof (ConcurrencyException))]
        public void UpdateLoadCategoryConcurrencyTest()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                LoadCategory loadCategory = PopulateNewItem();
                loadCategory.Id = SaveItem(loadCategory);
                //We didn't get the new checksum so when we save again an exception should be thrown
                try
                {
                    SaveItem(loadCategory);
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