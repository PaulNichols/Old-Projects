/*************************************************************************************************
 ** FILE:	TransactionTypeController.cs
 ** DATE:	30/05/2006
 ** AUTHOR:	Lee Spring
 **
 **
 ** OVERVIEW:
 **
 ** MODIFICATION HISTORY:
 **
 ** Date:		Version:	Who:	Change:
 ** 30/5/06		1.0		    LAS	    Initial Version
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
    /// A Class 'TransactionTypeController' which is an entity with namespace Discovery.BusinessObjects
    /// </summary>
    public static class TransactionTypeController
    {
        /// <summary>
        /// Gets the transaction types.
        /// </summary>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        public static List<TransactionType> GetTransactionTypes(string sortExpression)
        {
            List<TransactionType> types = GetTransactionTypes();
            if (string.IsNullOrEmpty(sortExpression))
            {
                sortExpression = "Code";
            }
            types.Sort(new UniversalComparer<TransactionType>(sortExpression));
            return types;
        }

        /// <summary>
        /// Saves the transactionType.
        /// </summary>
        /// <param name="transactionType">The transactionType.</param>
        /// <returns></returns>
        public static int SaveTransactionType(TransactionType transactionType)
        {
            try
            {
                if (transactionType.IsValid)
                {
                    // Save entity
                    transactionType.Id = DataAccessProvider.Instance().SaveTransactionType(transactionType);
                    if (transactionType.Id != -1)
                    {
                        FrameworkController.GetChecksum(transactionType);
                        CacheManager.Add(transactionType);
                    }
                }
                else
                {
                    // Entity is not valid
                    throw new InValidBusinessObjectException(transactionType);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            // Done
            return transactionType.Id;
        }

        /// <summary>
        /// Deletes the transactionType.
        /// </summary>
        /// <param name="transactionType">The transactionType to delete.</param>
        /// <returns></returns>
        public static bool DeleteTransactionType(TransactionType transactionType)
        {
            bool success = false;
            try
            {
                if (transactionType != null)
                {
                    success = DataAccessProvider.Instance().DeleteTransactionType(transactionType.Id);
                    if (success)
                    {
                        CacheManager.Remove(transactionType);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return success;
        }

        /// <summary>
        /// Gets the transactionType.
        /// </summary>
        /// <param name="transactionTypeId">The transactionType id.</param>
        /// <returns></returns>
        public static TransactionType GetTransactionType(int transactionTypeId)
        {
            TransactionType transactionType = null;
            try
            {
                transactionType = CacheManager.Get<TransactionType>(transactionTypeId);
                if (transactionType == null)
                {
                    transactionType = CBO<TransactionType>.FillObject(DataAccessProvider.Instance().GetTransactionType(transactionTypeId));
                    if (transactionType != null)
                    {
                        CacheManager.Add(transactionType);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return transactionType;
        }

        /// <summary>
        /// Gets the type of the transaction.
        /// </summary>
        /// <param name="transactionTypeCode">The transaction type code.</param>
        /// <returns></returns>
        public static TransactionType GetTransactionType(string transactionTypeCode)
        {
            TransactionType transactionType = null;
            try
            {
                  transactionType = CacheManager.Get<TransactionType>(transactionTypeCode);
                if (transactionType == null)
                {
                    transactionType =
                        CBO<TransactionType>.FillObject(
                            DataAccessProvider.Instance().GetTransactionType(transactionTypeCode));
                    if (transactionType!=null)
                    {
                        CacheManager.Add(transactionType);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return transactionType;
        }

        /// <summary>
        /// Gets the opco transaction types.
        /// </summary>
        /// <returns></returns>
        public static List<TransactionType> GetOpCoTransactionTypes()
        {
            List<TransactionType> transactionTypes = new List<TransactionType>();
            try
            {
                transactionTypes = CBO<TransactionType>.FillCollection(DataAccessProvider.Instance().GetOpCoTransactionTypes());
               
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return transactionTypes;
        }

        /// <summary>
        /// Gets the transactionTypes.
        /// </summary>
        /// <returns></returns>
        public static List<TransactionType> GetTransactionTypes()
        {
            List<TransactionType> transactionTypes = new List<TransactionType>();
            try
            {
                transactionTypes = CBO<TransactionType>.FillCollection(DataAccessProvider.Instance().GetTransactionTypes());
                 if (transactionTypes != null) foreach (TransactionType type in transactionTypes)
                {
                    CacheManager.Add(type);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return transactionTypes;
        }
    }
}