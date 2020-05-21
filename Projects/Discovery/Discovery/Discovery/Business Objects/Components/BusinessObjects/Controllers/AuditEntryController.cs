/*************************************************************************************************
 ** FILE:	AudityentryController.cs
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
    /// A class to provide the message audit entry controller which is a business object controller
    /// with namespace Discovery.BusinessObjects.Controllers
    /// </summary>
    public static class AuditEntryController
    {
        /// <summary>
        /// Saves the Audit Entry.
        /// </summary>
        /// <param name="auditEntry">The audit entry.</param>
        /// <returns></returns>
        public static int SaveAuditEntry(MessageAuditEntry auditEntry)
        {
            try
            {
                if (auditEntry.IsValid)
                {
                    // Save entity
                    auditEntry.Id = DataAccessProvider.Instance().SaveAuditEntry(auditEntry);
                }
                else
                {
                    // Entity is not valid
                    throw new InValidBusinessObjectException(auditEntry);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            return auditEntry.Id;
        }


        /// <summary>
        /// Deletes the audit entry.
        /// </summary>
        /// <param name="auditEntryId">The audit entry id.</param>
        /// <returns></returns>
        public static bool DeleteAuditEntry(int auditEntryId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the audit entry.
        /// </summary>
        /// <param name="auditEntryId">The audit entry id.</param>
        /// <returns></returns>
        public static MessageAuditEntry GetAuditEntry(int auditEntryId)
        {

            return CBO<MessageAuditEntry>.FillObject(DataAccessProvider.Instance().GetAuditEntry(auditEntryId));
        }

        /// <summary>
        /// Gets the audit entries.
        /// </summary>
        /// <returns></returns>
        public static List<MessageAuditEntry> GetAuditEntries()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the audit entries.
        /// </summary>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date to.</param>
        /// <param name="sourceSystem">The source system.</param>
        /// <param name="destinationSystem">The destination system.</param>
        /// <param name="type">The type.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <returns></returns>
        public static List<MessageAuditEntry> GetAuditEntries(string strDateFrom,
                                                              string strDateTo,
                                                              string sourceSystem,
                                                              string destinationSystem,
                                                              string type,
                                                              string message,
                                                              string sortExpression,
                                                              int startRowIndex,
                                                              int maximumRows)
        {
            // Our actual from and to dates
            DateTime dateFrom = Null.NullDate;
            DateTime dateTo = Null.NullDate;

            int totalRows = 0;
            List<MessageAuditEntry> auditEntryList = new List<MessageAuditEntry>();

            // See if we have a date from in string format
            if (!string.IsNullOrEmpty(strDateFrom)) dateFrom = Convert.ToDateTime(strDateFrom);

            // See if we have a date to in string format
            if (!string.IsNullOrEmpty(strDateTo)) dateTo = Convert.ToDateTime(strDateTo);

            string sourceSystemValue = Null.NullString;
            string destinationSystemValue = Null.NullString;
            string typeValue = Null.NullString;
            string messageValue = Null.NullString;

            if (sourceSystem != "All" && !string.IsNullOrEmpty(sourceSystem)) sourceSystemValue = sourceSystem;
            if (destinationSystem != "All" && !string.IsNullOrEmpty(destinationSystem)) destinationSystemValue = destinationSystem;
            if (type != "All" && !string.IsNullOrEmpty(type)) typeValue = type;
            if (!string.IsNullOrEmpty(message)) messageValue = message;

            if (string.IsNullOrEmpty(sortExpression))
            {
                sortExpression = "ReceivedDate DESC";

            }
            int rows;

            auditEntryList =
                CBO<MessageAuditEntry>.FillCollection(
                    DataAccessProvider.Instance().GetAuditEntries(
                            dateFrom,
                            dateTo,
                            sourceSystemValue,
                            destinationSystemValue,
                            typeValue,
                            messageValue,
                            sortExpression,
                            startRowIndex,
                            maximumRows, out rows));

            totalRows = rows;
            count = totalRows;
            return auditEntryList;
        }

        private static int count;

        /// <summary>
        /// Numbers the of audit entries.
        /// </summary>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date to.</param>
        /// <param name="sourceSystem">The source system.</param>
        /// <param name="destinationSystem">The destination system.</param>
        /// <param name="type">The type.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <returns></returns>
        public static Int32 NumberOfAuditEntries(string strDateFrom,
                                                 string strDateTo,
                                                 string sourceSystem,
                                                 string destinationSystem,
                                                 string type,
                                                 string message,
                                                 string sortExpression,
                                                 int startRowIndex,
                                                 int maximumRows
          )
        {
            return count;
        }

        /// <summary>
        /// Gets the source system list.
        /// </summary>
        /// <returns></returns>
        public static List<MessageAuditEntry> GetSourceSystemList()
        {
            return CBO<MessageAuditEntry>.FillCollection(DataAccessProvider.Instance().GetSourceSystemList());

        }

        /// <summary>
        /// Gets the destination system list.
        /// </summary>
        /// <returns></returns>
        public static List<MessageAuditEntry> GetDestinationSystemList()
        {
            return CBO<MessageAuditEntry>.FillCollection(DataAccessProvider.Instance().GetDestinationSystemList());

        }

        /// <summary>
        /// Gets the type list.
        /// </summary>
        /// <returns></returns>
        public static List<MessageAuditEntry> GetTypeList()
        {
            return CBO<MessageAuditEntry>.FillCollection(DataAccessProvider.Instance().GetTypeList());

        }
    }
}