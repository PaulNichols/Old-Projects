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
    public class SequenceTests
    {
        /// <summary>
        /// Populates the new item.
        /// </summary>
        /// <returns></returns>
        internal Sequence PopulateNewItem()
        {
            Sequence sequence = new Sequence();
            sequence.Name = "TEST";
            sequence.Seed = 1;
            sequence.Increment = 1;
            sequence.CurrentValue = 1;
            sequence.UpdatedBy = "TDC Team";
            return sequence;
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        internal Sequence GetItem(int id)
        {
            return SequenceController.GetSequence(id);
        }

        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <returns></returns>
        internal List<Sequence> GetAllItems()
        {
            return SequenceController.GetSequences();
        }

        /// <summary>
        /// Saves the item.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <returns></returns>
        internal int SaveItem(Sequence sequence)
        {
            return SequenceController.SaveSequence(sequence);
        }

        /// <summary>
        /// Tests the get sequence.
        /// </summary>
        [Test]
        public void TestGetSequence()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                Sequence sequence = PopulateNewItem();
                int Id = SaveItem(sequence);

                if (Id != -1)
                    Assert.IsNotNull(GetItem(Id));
            }
        }

        /// <summary>
        /// Tests the get sequences.
        /// </summary>
        [Test]
        public void TestGetSequences()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                Sequence sequence = PopulateNewItem();
                SaveItem(sequence);
                sequence.Id = -1;
                sequence.Name = "TWO";
                sequence.Seed = 2;
                sequence.Increment = 1;
                sequence.CurrentValue = 1;
                SaveItem(sequence);

                List<Sequence> sequenceList = GetAllItems();
                Assert.IsTrue(sequenceList.Count != 0);
            }
        }

        /// <summary>
        /// Tests the save sequence.
        /// </summary>
        [Test]
        public void TestSaveSequence()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                Sequence sequence = PopulateNewItem();
                int Id = SaveItem(sequence);
                Assert.IsTrue(Id != -1);
            }
        }

        /// <summary>
        /// Tests the update sequence.
        /// </summary>
        [Test]
        public void TestUpdateSequence()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                Sequence sequence = PopulateNewItem();
                sequence.Seed = 1;
                sequence.Id = SaveItem(sequence);
                sequence = GetItem(sequence.Id);
                //change a value
                sequence.Seed = 9;

                SaveItem(sequence);
                sequence = GetItem(sequence.Id);
                Assert.IsTrue(sequence.Seed == 9);
            }
        }

        /// <summary>
        /// Tests the delete sequence.
        /// </summary>
        [Test]
        public void TestDeleteSequence()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                int id = SaveItem(PopulateNewItem());
                if (id != -1)
                {
                    Sequence sequenceToDelete = new Sequence();
                    sequenceToDelete.Id = id;
                    Assert.IsTrue(SequenceController.DeleteSequence(sequenceToDelete));
                }
            }
        }

        /// <summary>
        /// Saves the sequence test constraint.
        /// </summary>
        [Test]
        [ExpectedException(typeof(DiscoveryException))]
        public void SaveSequenceTestConstraint()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                Sequence sequence = PopulateNewItem();
                SaveItem(sequence);
                SaveItem(sequence);
            }
        }

        /// <summary>
        /// Updates the sequence concurrency test.
        /// </summary>
        [Test]
        [ExpectedException(typeof(DiscoveryException))]
        //[ExpectedException(typeof (ConcurrencyException))]
        public void UpdateSequenceConcurrencyTest()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                Sequence sequence = PopulateNewItem();
                sequence.Id = SaveItem(sequence);
                //We didn't get the new checksum so when we save again an exception should be thrown
                try
                {
                    SaveItem(sequence);
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