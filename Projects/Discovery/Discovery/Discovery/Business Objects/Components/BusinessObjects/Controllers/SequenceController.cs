/*************************************************************************************************
 ** FILE:	SequenceController.cs
 ** DATE:	30/05/2006
 ** AUTHOR:	Paul Nichols
 **
 **
 ** OVERVIEW:
 **
 ** MODIFICATION HISTORY:
 **
 ** Date:		Version:	Who:	Change:
 ** 30/5/06		1.0		    PJN	    Initial Version
 ************************************************************************************************/
using System;
using System.Collections.Generic;
using Discovery.ComponentServices.DataAccess;
using Discovery.ComponentServices.ExceptionHandling;
using Discovery.Utility;
using Discovery.Utility.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Discovery.BusinessObjects.Controllers
{
    /// <summary>
    /// A class to provide the message audit entry controller which is a business object controller
    /// with namespace Discovery.BusinessObjects.Controllers
    /// </summary>
    public static class SequenceController
    {
        /// <summary>
        /// Saves the sequence.
        /// </summary>
        /// <param name="sequence">The sequence to be saved.</param>
        /// <returns></returns>
        public static int SaveSequence(Sequence sequence)
        {
            try
            {
                if (sequence.IsValid)
                {
                    // Save entity
                    sequence.Id = DataAccessProvider.Instance().SaveSequence(sequence);
                }
                else
                {
                    // Entity is not valid
                    throw new InValidBusinessObjectException(sequence);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            // Done
            return sequence.Id;
        }

        /// <summary>
        /// Deletes the sequence.
        /// </summary>
        /// <param name="sequenceToDelete">The sequence id to delete.</param>
        /// <returns></returns>
        public static bool DeleteSequence(Sequence sequenceToDelete)
        {
            bool success = false;

            try
            {
                if (sequenceToDelete != null)
                {
                    success = DataAccessProvider.Instance().DeleteSequence(sequenceToDelete);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return success;
        }

        /// <summary>
        /// Gets the sequence.
        /// </summary>
        /// <param name="sequenceId">The sequence id to retrieve.</param>
        /// <returns></returns>
        public static Sequence GetSequence(int sequenceId)
        {
            Sequence sequence = null;

            try
            {
                sequence = CBO<Sequence>.FillObject(DataAccessProvider.Instance().GetSequence(sequenceId));


            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return sequence;
        }

        /// <summary>
        /// Gets the next value for the named sequence.
        /// </summary>
        /// <param name="name">The name of the sequence to return the next value from.</param>
        /// <returns></returns>
        public static int GetNextSequence(string name)
        {
            // Get the next sequence for the named sequence
            return DataAccessProvider.Instance().GetNextSequence(name.ToUpper());             
        }

        /// <summary>
        /// Gets the list of all sequences.
        /// </summary>
        /// <returns></returns>
        public static List<Sequence> GetSequences()
        {
            List<Sequence> sequences = new List<Sequence>();

            try
            {
                sequences =
                    CBO<Sequence>.FillCollection(
                        DataAccessProvider.Instance().GetSequences());

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return sequences;
        }

        /// <summary>
        /// Return the list of the sorted sequences.
        /// </summary>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        public static List<Sequence> GetSequences(string sortExpression)
        {
            List<Sequence> sequences = GetSequences();

            if (string.IsNullOrEmpty(sortExpression))
            {
                sortExpression = "Name";
            }

            sequences.Sort(new UniversalComparer<Sequence>(sortExpression));
            return sequences;
        }

    }
}