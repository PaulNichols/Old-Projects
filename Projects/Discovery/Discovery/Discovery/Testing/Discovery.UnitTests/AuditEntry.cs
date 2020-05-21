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
using Discovery.Utility;

namespace Discovery.UnitTests
{
    [TestFixture]
    public class AuditEntryTests
    {
        /// <summary>
        /// Populates the new item.
        /// </summary>
        /// <returns></returns>
        internal MessageAuditEntry PopulateNewItem()
        {
            MessageAuditEntry auditEntry = new MessageAuditEntry();
            auditEntry.SourceSystem = "TestSource";
            auditEntry.DestinationSystem = "TestDestination";
            auditEntry.ReceivedDate = DateTime.Today;
            auditEntry.Message = "Message";
            auditEntry.Sequence = 1;
            auditEntry.Type = "TestType";
            auditEntry.Name = "TestName";
            auditEntry.Label = "TestLabel";
            auditEntry.UpdatedBy = "TDC Team";
            return auditEntry;
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        internal MessageAuditEntry GetItem(int id)
        {
            return AuditEntryController.GetAuditEntry(id);
        }

        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <returns></returns>
        internal List<MessageAuditEntry> GetAllItems()
        {
            return AuditEntryController.GetAuditEntries(
                "",
                "",
                "All",
                "All",
                "All",
                "",
                "",
                1,
                20);
        }

        /// <summary>
        /// Gets the source system list.
        /// </summary>
        /// <returns></returns>
        internal List<MessageAuditEntry> GetSourceSystemList()
        {
            return AuditEntryController.GetSourceSystemList();
        }

        /// <summary>
        /// Gets the destination system list.
        /// </summary>
        /// <returns></returns>
        internal List<MessageAuditEntry> GetDestinationSystemList()
        {
            return AuditEntryController.GetDestinationSystemList();
        }

        /// <summary>
        /// Saves the item.
        /// </summary>
        /// <param name="auditEntry">The audit entry.</param>
        /// <returns></returns>
        internal int SaveItem(MessageAuditEntry auditEntry)
        {
            return AuditEntryController.SaveAuditEntry(auditEntry);
        }

        /// <summary>
        /// Tests the get audit entry.
        /// </summary>
        [Test]
        public void TestGetAuditEntry()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                MessageAuditEntry auditEntry = PopulateNewItem();
                int Id = SaveItem(auditEntry);

                if (Id != -1)
                    Assert.IsNotNull(GetItem(Id));
            }
        }

        /// <summary>
        /// Tests the get all audit entries.
        /// </summary>
        [Test]
        public void TestGetAuditEntries()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                MessageAuditEntry auditEntry = PopulateNewItem();
                SaveItem(auditEntry);
                auditEntry.Id = -1;
                auditEntry.SourceSystem = "TestSource2";
                auditEntry.DestinationSystem = "TestDestination2";
                auditEntry.ReceivedDate = DateTime.Today;
                auditEntry.Message = "Message2";
                auditEntry.Sequence = 2;
                auditEntry.Type = "TestType2";
                auditEntry.Name = "TestName2";
                auditEntry.Label = "TestLabel2";
                auditEntry.UpdatedBy = "TDC Team";
                SaveItem(auditEntry);

                List<MessageAuditEntry> auditEntryList = GetAllItems();
                Assert.IsTrue(auditEntryList.Count != 0);
            }
        }

        /// <summary>
        /// Tests the save audit entry.
        /// </summary>
        [Test]
        public void TestSaveAuditEntry()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                MessageAuditEntry auditEntry = PopulateNewItem();
                int Id = SaveItem(auditEntry);
                Assert.IsTrue(Id != -1);
            }
        }

        /// <summary>
        /// Tests the update audit entry.
        /// </summary>
        [Test]
        public void TestUpdateAuditEntry()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                MessageAuditEntry auditEntry = PopulateNewItem();
                auditEntry.SourceSystem = "Original";
                auditEntry.Id = SaveItem(auditEntry);
                auditEntry = GetItem(auditEntry.Id);
                //change a value
                auditEntry.SourceSystem = "Updated";

                SaveItem(auditEntry);
                auditEntry = GetItem(auditEntry.Id);
                Assert.IsTrue(auditEntry.SourceSystem == "Updated");
            }
        }

        /// <summary>
        /// Saves the audit entry test constraint.
        /// </summary>
        [Test]
        [ExpectedException(typeof(DiscoveryException))]
        public void SaveAuditEntryTestConstraint()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                MessageAuditEntry auditEntry = PopulateNewItem();
                SaveItem(auditEntry);
                SaveItem(auditEntry);
            }
        }

        /// <summary>
        /// Updates the audit entry concurrency test.
        /// </summary>
        [Test]
        [ExpectedException(typeof(DiscoveryException))]
        //[ExpectedException(typeof (ConcurrencyException))]
        public void UpdateAuditEntryConcurrencyTest()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                MessageAuditEntry auditEntry = PopulateNewItem();
                auditEntry.Id = SaveItem(auditEntry);
                //We didn't get the new checksum so when we save again an exception should be thrown
                try
                {
                    SaveItem(auditEntry);
                }
                catch (DiscoveryException e)
                {
                    Assert.IsInstanceOfType(typeof(ConcurrencyException), e.InnerException);
                    throw e;
                }
            }
        }

        /// <summary>
        /// Tests the get source system list.
        /// </summary>
        [Test]
        public void TestGetSourceSystemList()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                MessageAuditEntry auditEntry = PopulateNewItem();
                SaveItem(auditEntry);

                auditEntry.Id = -1;
                auditEntry.SourceSystem = "TestSource2";
                auditEntry.DestinationSystem = "TestDestination2";
                auditEntry.ReceivedDate = DateTime.Today;
                auditEntry.Message = "Message2";
                auditEntry.Sequence = 2;
                auditEntry.Type = "TestType2";
                auditEntry.Name = "TestName2";
                auditEntry.Label = "TestLabel2";
                SaveItem(auditEntry);

                Assert.IsTrue(GetSourceSystemList().Count != 1);
            }
        }

        /// <summary>
        /// Tests the get destination system list.
        /// </summary>
        [Test]
        public void TestGetDestinationSystemList()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                MessageAuditEntry auditEntry = PopulateNewItem();
                SaveItem(auditEntry);

                auditEntry.Id = -1;
                auditEntry.SourceSystem = "TestSource2";
                auditEntry.DestinationSystem = "TestDestination2";
                auditEntry.ReceivedDate = DateTime.Today;
                auditEntry.Message = "Message2";
                auditEntry.Sequence = 2;
                auditEntry.Type = "TestType2";
                auditEntry.Name = "TestName2";
                auditEntry.Label = "TestLabel2";
                SaveItem(auditEntry);

                Assert.IsTrue(GetDestinationSystemList().Count != 1);
            }
        }
    }
}