/*************************************************************************************************
 ** FILE:	LogController.cs
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
using System.Data;
using System.Diagnostics;
using Discovery.ComponentServices.DataAccess;
using Discovery.ComponentServices.ExceptionHandling;
using Discovery.Utility;
using Discovery.Utility.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Discovery.BusinessObjects.Controllers
{
    /*************************************************************************************************
   ** CLASS:	LogController
   **
   ** OVERVIEW:
   ** 
   ** 
   **
   ** MODIFICATION HISTORY:
   **
   ** Date:		Version:    Who:	Change:
   ** 20/7/06	1.0			PJN		Initial Version
   ************************************************************************************************/

    /// <summary>
    /// A class to provide the log controller which is a business object controller
    /// with namespace Discovery.BusinessObjects.Controllers
    /// </summary>
    public static class LogController
    {
        private static int logCount;
        private const string DATAPROVIDER = "LogSqlDataProvider";

        /// <summary>
        /// Gets the log entries.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <returns></returns>
        public static List<LogEntry> GetLogEntries(LogEntryCriteria searchCriteria, string sortExpression, int startRowIndex, int maximumRows)
        {
            int totalRows;
            List<LogEntry> logs = GetLogEntries(searchCriteria, sortExpression, startRowIndex, maximumRows, out totalRows);
            if (string.IsNullOrEmpty(sortExpression))
            {
                sortExpression = "TimeStamp";
            }
            logs.Sort(new UniversalComparer<LogEntry>(sortExpression));
            logCount = totalRows;
            return logs;
        }

        /// <summary>
        /// Numbers the of log entries.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <returns></returns>
        public static Int32 NumberOfLogEntries(LogEntryCriteria searchCriteria, string sortExpression, int startRowIndex,
                                               int maximumRows)
        {
            return logCount;
        }

        /// <summary>
        /// Gets the log entry.
        /// </summary>
        /// <param name="logId">The log id.</param>
        /// <returns></returns>
        public static LogEntry GetLogEntry(int logId)
        {
            LogEntry logEntry = null;
            try
            {
                logEntry = CBO<LogEntry>.FillObject(DataAccessProvider.Instance(DATAPROVIDER).GetLogEntry(logId), CustomFill, true);

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return logEntry;
        }


        /// <summary>
        /// Gets all log entries.
        /// </summary>
        /// <returns></returns>
        /// 
        public static List<LogEntry> GetLogEntries(LogEntryCriteria searchCriteria,
                    string sortExpression,
                    int startRowIndex,
                    int maximumRows,
                    out int totalRows)
        {
            List<LogEntry> entries = GetLogEntries(
                searchCriteria,
                sortExpression,
                startRowIndex,
                maximumRows,
                out totalRows,
                true);
            
            //if (searchCriteria.RequiresAcknowledgement!=null)
            //{
            //    //if the search criteria specifies that only entries that require or don't require
            //    //ack should be returned then filter the list here as the RequiresAcknowledgement property is only populated in the business
            //    //layer rather than returned from the GetLogEntires SP. It cannot be returned from the SP because
            //    //at the time of writing this the log entries are held in a seperate DB to the ErrorType table which sepcifies 
            //    //if the error type RequiresAcknowledgement
            //    entries = entries.FindAll(delegate(LogEntry obj)
            //                                  {
            //                                      return obj.RequiresAcknowledgement == searchCriteria.RequiresAcknowledgement;
            //                                  });
            //}

            return entries;
        }

        static List<ErrorType> errorTypes = new List<ErrorType>();

        /// <summary>
        /// Gets the log entries.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <param name="totalRows">The total rows.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fully populate].</param>
        /// <returns></returns>
        public static List<LogEntry> GetLogEntries(LogEntryCriteria searchCriteria,
                    string sortExpression,
                    int startRowIndex,
                    int maximumRows,
                    out int totalRows,
                    bool fullyPopulate)
        {
            List<LogEntry> logEntries = new List<LogEntry>();
            int rows = 0;
            try
            {
                //pre load an error type collection as this will be used in the custom fill method, this should be
                //more efficient than finding a related error type for each log entryone at a time
                if (searchCriteria.OpcoCode != Null.NullString && searchCriteria.CategoryName != Null.NullString &&
                       searchCriteria.ErrorType != Null.NullString)
                {
                    ErrorType errorType =
                        ErrorTypeController.GetErrorType(searchCriteria.ErrorType, searchCriteria.OpcoCode,
                                                         searchCriteria.CategoryName);

                    if (errorType != null)
                    {
                        errorTypes=new List<ErrorType>();
                        errorTypes.Add(errorType);
                    }
                }
                else
                {
                    errorTypes = ErrorTypeController.GetErrorTypes(searchCriteria.OpcoCode, searchCriteria.CategoryName, "");
                }

                logEntries =
                    CBO<LogEntry>.FillCollection(
                        DataAccessProvider.Instance(DATAPROVIDER).GetLogEntries(searchCriteria, sortExpression, startRowIndex, maximumRows, out rows),
                        CustomFill,
                        fullyPopulate);

                errorTypes.Clear();
                errorTypes = null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            totalRows = rows;
            return logEntries;
        }


        /// <summary>
        /// Gets the log categories.
        /// </summary>
        /// <returns></returns>
        public static List<KeyValuePair<int, string>> GetCategories()
        {
            List<KeyValuePair<int, string>> categories = new List<KeyValuePair<int, string>>();

            try
            {
                IDataReader dataReader = DataAccessProvider.Instance(DATAPROVIDER).GetLogCategories();
                while (dataReader.Read())
                {
                    categories.Add(new KeyValuePair<int, string>((int)dataReader["CategoryId"], dataReader["CategoryName"].ToString()));
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return categories;
        }

        /// <summary>
        /// Customs the fill.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fully populate].</param>
        private static void CustomFill(LogEntry item, IDataReader dataReader, bool fullyPopulate)
        {
            foreach (object value in Enum.GetValues(typeof(TraceEventType)))
            {
                if (dataReader["Severity"].ToString().Equals(Enum.GetName(typeof(TraceEventType), value)))
                {
                    item.Severity = (TraceEventType)value;
                    break;
                }
            }

            ErrorType relatedErrorType;
            if (errorTypes == null || errorTypes.Count==0)
            {
                relatedErrorType = ErrorTypeController.GetErrorType(item.ErrorType, "", item.CategoryName);
                if (relatedErrorType != null)
                {
                    errorTypes=new List<ErrorType>();
                    errorTypes.Add(relatedErrorType);
                }
            }
            else
            {
                //Set the RequiresAcknowledgement flag. This
                //this is done by trying to relate an error type to the log entry. If a related error type is found
                //then that item will state whether Acknowledgement is required
                //a list of error types should have been loaded up front in the calling controller method, this will stop the need to go to
                //the database for each log entry to try and recieve it's related error type
                relatedErrorType = errorTypes.Find(delegate(ErrorType errorType) { return errorType.ExceptionType == item.ErrorType; });

            }


            item.RequiresAcknowledgement = relatedErrorType != null && relatedErrorType.RequiresAcknowledgement;
        }

        public static List<KeyValuePair<int, string>> GetSeverities()
        {
            List<KeyValuePair<int, string>> returnItems = new List<KeyValuePair<int, string>>();
            foreach (int value in Enum.GetValues(typeof(TraceEventType)))
            {
                returnItems.Add(new KeyValuePair<int, string>(value, Enum.GetName(typeof(TraceEventType), value)));
            }

            return returnItems;
        }

        /// <summary>
        /// Acknowledges the log.
        /// </summary>
        /// <param name="Id">The id of the log entry to acknowledge.</param>
        /// <param name="ackBy">who ack by.</param>
        /// <returns></returns>
        public static bool AcknowledgeLog(int Id,string ackBy)
        {
            return DataAccessProvider.Instance(DATAPROVIDER).AcknowledgeLog(Id, ackBy,DateTime.Now);
        }


    }
}