/*************************************************************************************************
 ** FILE:	global.asax.cs
 ** DATE:	01/08/2004
 ** AUTHOR:	Lee Spring
 **
 ** COPYRIGHT:
 ** Lee Spring
 ** LAS Solutions Ltd - www.las-solutions.co.uk
 ** Copyright (c) 2004 LAS Solutions Ltd
 **
 ** THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
 ** TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
 ** THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
 ** CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 ** DEALINGS IN THE SOFTWARE.
 **
 ** OVERVIEW:
 **
 ** MODIFICATION HISTORY:
 **
 ** Date:		Version:	Who:	Change:
 ** 1/8/04		1.0			LAS		Initial Version
 ************************************************************************************************/


using System; 
using System.Configuration; 
using System.Data; 
using System.Xml; 
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using Discovery.Utility;
using Discovery.Utility.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.IO;
using System.Reflection;
using System.Web;

namespace Discovery.Scheduling 
{
    public class SchedulingController  
	{

        public static Dictionary<string,string> GetAssemblyTypes()
        {
            string AppRoot = HttpContext.Current.Server.MapPath("~/bin");

            Dictionary<string,string> arrTypes = new Dictionary<string,string>();
            string[] f = Directory.GetFiles(AppRoot, "*.dll");
            for (int i = 0; i <= f.Length - 1; i++)
            {
                Assembly ab = null;
                ab = Assembly.LoadFrom(f[i]);
                Dictionary<string, string> a = GetTypes(ab);
                if (a != null)
                {
                    foreach (KeyValuePair<string, string> item in a)
                    {
                        arrTypes.Add(item.Key,item.Value);
                    }
                  
                }
            }
            return arrTypes;
        }

        private static Dictionary<string, string> GetTypes(System.Reflection.Assembly TargetAssembly)
        {
            Dictionary<string, string> arrList = new Dictionary<string, string>();

            //  Get an array of the Types that are in this assembly
            Type[] TargetTypes = TargetAssembly.GetTypes();

            //  Loop through these types
            foreach (System.Type TypeItem in TargetTypes)
            {

                if (TypeItem.BaseType == typeof(Discovery.Scheduling.SchedulerClient))
                {
                    string key = null;
                    string value=Utility.StringManipulation.SplitStringOnUpperCase(TypeItem.Name);                    
                    key = TypeItem.FullName + ", " + TypeItem.Module.Name.Substring(0, TypeItem.Module.Name.LastIndexOf(".")).ToUpper();
                    arrList.Add(key,value);
                }
            }
            return arrList;
        }

        private static int count;

        /// <summary>
        /// Gets the schedules.
        /// </summary>
        /// <returns></returns>
        public static List<ScheduleItem> GetSchedules()
        {
            List<ScheduleItem> scheduleItem = new List<ScheduleItem>();

            try
            {
                scheduleItem =
                    CBO<ScheduleItem>.FillCollection(
                        DataProvider.Instance().GetSchedule());

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return scheduleItem;
        }


        /// <summary>
        /// Gets the schedules.
        /// </summary>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <returns></returns>
        public static List<ScheduleItem> GetSchedules(
                                                    string sortExpression,
                                                    int startRowIndex,
                                                    int maximumRows)
        {
            int totalRows = 0;
            List<ScheduleItem> scheduleItem = new List<ScheduleItem>();

            if (string.IsNullOrEmpty(sortExpression))
            {
                sortExpression = "TypeFullName DESC";

            }
            try
            {
                int rows;
                scheduleItem =
                    CBO<ScheduleItem>.FillCollection(
                        DataProvider.Instance().GetSchedules(sortExpression,
                                                         startRowIndex,
                                                         maximumRows, out rows));
                totalRows = rows;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            count = totalRows;
            return scheduleItem;
        }

        public static Int32 NumberOfScheduleCount(string sortExpression,
                                                  int startRowIndex,
                                                  int maximumRows)
        {
            return count;
        }

        /// <summary>
        /// Gets the schedule.
        /// </summary>
        /// <returns></returns>
        public List<ScheduleItem> GetSchedule() 
		{
            //ArrayList arrSource = CBO.FillCollection(DataProvider.Instance().GetSchedule(), typeof(ScheduleHistoryItem));

            List<ScheduleItem> arrSource = GetSchedules();

            List<ScheduleItem> arrDest = new List<ScheduleItem>();
            
            for (int i = 0; i < arrSource.Count; i++) 
			{ 
                ScheduleItem objScheduleItem = null; 
                objScheduleItem = (ScheduleItem)arrSource[i]; 
                Hashtable h = GetScheduleItemSettings(objScheduleItem.Id); 
                objScheduleItem.SetSettings(h); 
                arrDest.Add(objScheduleItem); 
            } 
            return arrDest; 
        }

        /// <summary>
        /// Gets the schedule.
        /// </summary>
        /// <param name="EventName">Name of the event.</param>
        /// <returns></returns>
        public List<ScheduleItem> GetSchedule(string EventName) 
		{ 
            //ArrayList arrSource = CBO<ScheduleItem>.FillCollection(DataProvider.Instance().GetSchedule(EventName), typeof(ScheduleHistoryItem)); 
            List<ScheduleItem> arrSource = new List<ScheduleItem>();

            try
            {
                arrSource = CBO<ScheduleItem>.FillCollection(DataProvider.Instance().GetSchedule(EventName));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            
            List<ScheduleItem> arrDest = new List<ScheduleItem>(); 

            for (int i = 0; i < arrSource.Count; i++) 
			{ 
                ScheduleItem objScheduleItem = null; 
                objScheduleItem = (ScheduleItem)arrSource[i]; 
                Hashtable h = GetScheduleItemSettings(objScheduleItem.Id); 
                objScheduleItem.SetSettings(h); 
                arrDest.Add(objScheduleItem); 
            } 
            return arrDest; 
        }

        /// <summary>
        /// Gets the schedule.
        /// </summary>
        /// <param name="scheduleID">The schedule ID.</param>
        /// <returns></returns>
        public ScheduleItem GetSchedule(int scheduleID) 
		{
            ScheduleItem objScheduleItem = null;

            try
            {
                objScheduleItem = CBO<ScheduleItem>.FillObject(DataProvider.Instance().GetSchedule(scheduleID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            Hashtable h = GetScheduleItemSettings(objScheduleItem.Id); 
            objScheduleItem.SetSettings(h); 
            return objScheduleItem; 
        }

        /// <summary>
        /// Gets the schedule history.
        /// </summary>
        /// <param name="scheduleID">The schedule ID.</param>
        /// <returns></returns>
        public ScheduleHistoryItem GetScheduleHistory(int scheduleId)
		{ 
            //return CBO.FillCollection(DataProvider.Instance().GetScheduleHistory(ScheduleID), typeof(ScheduleHistoryItem)); 
            ScheduleHistoryItem scheduleHistoryItem = null;

            try
            {
                scheduleHistoryItem = CBO<ScheduleHistoryItem>.FillObject(
                            DataProvider.Instance().GetScheduleHistory(scheduleId));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return scheduleHistoryItem;
        }

        public static List<ScheduleHistoryItem> GetScheduleHistories(int scheduleId,
                                                                     string sortExpression,
                                                                     int startRowIndex,
                                                                     int maximumRows)
        {
            //return CBO.FillCollection(DataProvider.Instance().GetScheduleHistory(ScheduleID), typeof(ScheduleHistoryItem)); 
            List<ScheduleHistoryItem> scheduleHistoryItems = new List<ScheduleHistoryItem>();
            int totalRows = 0;

            if (string.IsNullOrEmpty(sortExpression))
            {
                sortExpression = "StartDate DESC";
            }

            try
            {
                int rows;
                scheduleHistoryItems = CBO<ScheduleHistoryItem>.FillCollection(
                            DataProvider.Instance().GetScheduleHistories(scheduleId,
                                                                       sortExpression,
                                                                       startRowIndex,
                                                                       maximumRows, out rows));
                totalRows = rows;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            count = totalRows;
            return scheduleHistoryItems;
        }

        public static Int32 NumberOfScheduleHistoryCount(int scheduleId,
                                                         string sortExpression,
                                                         int startRowIndex,
                                                         int maximumRows)
        {
            return count;
        }
        
        /// <summary>
        /// Gets the schedule queue.
        /// </summary>
        /// <returns></returns>
        public ArrayList GetScheduleQueue() 
		{ 
            return Scheduler.CoreScheduler.GetScheduleQueue(); 
        }

        /// <summary>
        /// Gets the schedule processing.
        /// </summary>
        /// <returns></returns>
        public ArrayList GetScheduleProcessing() 
		{ 
            return Scheduler.CoreScheduler.GetScheduleInProgress(); 
        }


        /// <summary>
        /// Gets the schedule status.
        /// </summary>
        /// <returns></returns>
        public ScheduleStatus GetScheduleStatus() 
		{ 
            return Scheduler.CoreScheduler.GetScheduleStatus(); 
        }

        /// <summary>
        /// Gets the free thread count.
        /// </summary>
        /// <returns></returns>
        public int GetFreeThreadCount() 
		{ 
            return Scheduler.CoreScheduler.GetFreeThreadCount(); 
        }

        /// <summary>
        /// Gets the active thread count.
        /// </summary>
        /// <returns></returns>
        public int GetActiveThreadCount() 
		{ 
            return Scheduler.CoreScheduler.GetActiveThreadCount(); 
        }

        /// <summary>
        /// Gets the max thread count.
        /// </summary>
        /// <returns></returns>
        public int GetMaxThreadCount() 
		{ 
            return Scheduler.CoreScheduler.GetMaxThreadCount(); 
        }

        /// <summary>
        /// Reloads the schedule.
        /// </summary>
        public void ReloadSchedule() 
		{ 
            Scheduler.CoreScheduler.ReloadSchedule(); 
        }

        /// <summary>
        /// Saves the schedule.
        /// </summary>
        /// <param name="scheduleItem">The schedule item.</param>
        /// <returns></returns>
        public static int SaveSchedule(ScheduleItem scheduleItem)
        {
            try
            {
                if (scheduleItem.IsValid)
                {
                    // Save entity
                    scheduleItem.Id = DataProvider.Instance().SaveSchedule(scheduleItem);
                }
                else
                {
                    // Entity is not valid
                    throw new InValidBusinessObjectException(scheduleItem);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            // Done
            return scheduleItem.Id;
        }

        //public int AddSchedule(
        //            string TypeFullName, 
        //            int TimeLapse, 
        //            string TimeLapseMeasurement, 
        //            int RetryTimeLapse, 
        //            string RetryTimeLapseMeasurement, 
        //            int RetainHistoryNum, 
        //            string AttachToEvent, 
        //            bool CatchUpEnabled, 
        //            bool Enabled, 
        //            string ObjectDependencies) 
        //{ 
        //    int iScheduleID = Convert.ToInt32(
        //        DataProvider.Instance().AddSchedule(
        //                    TypeFullName, 
        //                    TimeLapse, 
        //                    TimeLapseMeasurement, 
        //                    RetryTimeLapse, 
        //                    RetryTimeLapseMeasurement, 
        //                    RetainHistoryNum, 
        //                    AttachToEvent, 
        //                    CatchUpEnabled, 
        //                    Enabled, 
        //                    ObjectDependencies)); 

        //    // **LAS** Return the new schedule id
        //    return iScheduleID;
        //} 
        
        
        //public void UpdateSchedule(
        //            int ScheduleID, 
        //            string TypeFullName, 
        //            int TimeLapse, 
        //            string TimeLapseMeasurement, 
        //            int RetryTimeLapse, 
        //            string RetryTimeLapseMeasurement, 
        //            int RetainHistoryNum, 
        //            string AttachToEvent, 
        //            bool CatchUpEnabled, 
        //            bool Enabled, 
        //            string ObjectDependencies) 
        //{ 
        //    DataProvider.Instance().UpdateSchedule(
        //                ScheduleID, 
        //                TypeFullName, 
        //                TimeLapse, 
        //                TimeLapseMeasurement, 
        //                RetryTimeLapse, 
        //                RetryTimeLapseMeasurement, 
        //                RetainHistoryNum, 
        //                AttachToEvent, 
        //                CatchUpEnabled, 
        //                Enabled, 
        //                ObjectDependencies); 
        //} 

        /// <summary>
        /// Deletes the schedule.
        /// </summary>
        /// <param name="scheduleID">The schedule ID.</param>
        /// <returns></returns>
        public bool DeleteSchedule(ScheduleItem scheduleItem) 
		{ 
            //DataProvider.Instance().DeleteSchedule(ScheduleID); 
            bool success = false;

            try
            {
                success = DataProvider.Instance().DeleteSchedule(scheduleItem.Id);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return success;
        }

        /// <summary>
        /// Saves the schedule history.
        /// </summary>
        /// <param name="scheduleHistoryItem">The schedule history item.</param>
        /// <returns></returns>
        public int SaveScheduleHistory(ScheduleHistoryItem scheduleHistoryItem)
        {
            try
            {
                if (scheduleHistoryItem.IsValid)
                {
                    // Save entity
                    scheduleHistoryItem.Id = DataProvider.Instance().SaveScheduleHistory(scheduleHistoryItem);
                }
                else
                {
                    // Entity is not valid
                    throw new InValidBusinessObjectException(scheduleHistoryItem);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            // Done
            return scheduleHistoryItem.Id;
        }
        
        //public int AddScheduleHistory(ScheduleHistoryItem objScheduleHistoryItem) 
        //{ 
        //    return DataProvider.Instance().AddScheduleHistory(objScheduleHistoryItem.ScheduleID, objScheduleHistoryItem.StartDate); 
        //} 
        
        //public void UpdateScheduleHistory(ScheduleHistoryItem objScheduleHistoryItem) 
        //{ 
        //    DataProvider.Instance().UpdateScheduleHistory(objScheduleHistoryItem.ScheduleHistoryID, objScheduleHistoryItem.EndDate, objScheduleHistoryItem.Succeeded, objScheduleHistoryItem.LogNotes, objScheduleHistoryItem.NextStart); 
        //} 
        
        public Hashtable GetScheduleItemSettings(int scheduleID) 
		{ 
            Hashtable h = new Hashtable(); 
            //IDataReader r = DataProvider.Instance().GetScheduleItemSettings(ScheduleID); 
            IDataReader r = null;

            try
            {
                r = DataProvider.Instance().GetScheduleItemSettings(scheduleID);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            while (r.Read()) 
			{ 
                h.Add(r["SettingName"], r["SettingValue"]); 
            } 
            return h; 
        } 
        
        public bool PurgeScheduleHistory() 
		{ 
            //DataProvider.Instance().PurgeScheduleHistory(); 
            bool success = false;

            try
            {
                success = DataProvider.Instance().PurgeScheduleHistory();
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return success;
        } 
    } 
} 
