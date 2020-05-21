/*************************************************************************************************
 ** FILE:	TransactionSubType.cs
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
    /*************************************************************************************************
  ** CLASS:	TransactionSubTypeController
  **
  ** OVERVIEW:
  ** This controller class contains all methods related to Transaction Sub Types including calling data access methods
  **
  ** MODIFICATION HISTORY:
  **
  ** Date:		Version:    Who:	Change:
  ** 19/7/06		1.0			PJN		Initial Version
  ************************************************************************************************/
    /// <summary>
    /// A class to provide the transaction sub-type controller which is a business object controller
    /// with namespace Discovery.BusinessObjects.Controllers
    /// </summary>

    public static class TransactionSubTypeController
    {
        /// <summary>
        /// Gets the transaction sub types.
        /// </summary>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        public static List<TransactionSubType> GetTransactionSubTypes(string sortExpression)
        {
            List<TransactionSubType> subTypes = GetTransactionSubTypes();
            if (string.IsNullOrEmpty(sortExpression))
            {
                sortExpression = "Code";
            }
            subTypes.Sort(new UniversalComparer<TransactionSubType>(sortExpression));
            return subTypes;
        }

        /// <summary>
        /// Saves the transactionSubType.
        /// </summary>
        /// <param name="transactionSubType">The transactionSubType.</param>
        /// <returns></returns>
        public static int SaveTransactionSubType(TransactionSubType transactionSubType)
        {
            try
            {
                if (transactionSubType.IsValid)
                {
                    // Save entity
                    transactionSubType.Id = DataAccessProvider.Instance().SaveTransactionSubType(transactionSubType);
                    if (transactionSubType.Id != -1)
                    {
                         FrameworkController.GetChecksum(transactionSubType);
                        CacheManager.Add(transactionSubType);
                    }
                }
                else
                {
                    // Entity is not valid
                    throw new InValidBusinessObjectException(transactionSubType);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            // Done
            return transactionSubType.Id;
        }

        /// <summary>
        /// Deletes the transactionSubType.
        /// </summary>
        /// <param name="transactionSubType">The transactionSubType to delete.</param>
        /// <returns></returns>
        public static bool DeleteTransactionSubType(TransactionSubType transactionSubType)
        {
            bool success = false;
            try
            {
                if (transactionSubType != null)
                {
                    success = DataAccessProvider.Instance().DeleteTransactionSubType(transactionSubType.Id);
                    if (success)
                    {
                        CacheManager.Remove(transactionSubType);
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
        /// Gets the transactionSubType by code.
        /// </summary>
        /// <param name="transactionSubTypeId">The transactionSubType id.</param>
        /// <returns></returns>
        public static TransactionSubType GetTransactionSubType(string transactionSubTypeCode)
        {
            TransactionSubType transactionSubType = null;
            try
            {
                transactionSubType = CacheManager.Get<TransactionSubType>(transactionSubTypeCode);
                if (transactionSubType == null)
                {
                    transactionSubType =
                        CBO<TransactionSubType>.FillObject(
                            DataAccessProvider.Instance().GetTransactionSubTypeByCode(transactionSubTypeCode));
                    if (transactionSubType != null)
                    {
                        CacheManager.Add(transactionSubType);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return transactionSubType;
        }

        /// <summary>
        /// Gets the transactionSubType.
        /// </summary>
        /// <param name="transactionSubTypeId">The transactionSubType id.</param>
        /// <returns></returns>
        public static TransactionSubType GetTransactionSubType(int transactionSubTypeId)
        {
            TransactionSubType transactionSubType = new TransactionSubType();
            try
            {
                transactionSubType = CacheManager.Get<TransactionSubType>(transactionSubTypeId);
                if (transactionSubType == null)
                {
                    transactionSubType =
                        CBO<TransactionSubType>.FillObject(
                            DataAccessProvider.Instance().GetTransactionSubType(transactionSubTypeId));

                    if (transactionSubType != null)
                    {
                        CacheManager.Add(transactionSubType);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return transactionSubType;
        }

        /// <summary>
        /// Gets the transactionSubTypes.
        /// </summary>
        /// <returns></returns>
        public static List<TransactionSubType> GetTransactionSubTypes()
        {
            List<TransactionSubType> transactionSubTypes = new List<TransactionSubType>();
            try
            {
                transactionSubTypes =
                    CBO<TransactionSubType>.FillCollection(DataAccessProvider.Instance().GetTransactionSubTypes());
                if (transactionSubTypes != null) foreach (TransactionSubType type in transactionSubTypes)
                {
                    CacheManager.Add(type);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return transactionSubTypes;
        }
    }
}