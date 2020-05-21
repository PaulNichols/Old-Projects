using System;
using System.Collections.Generic;
using System.Data;
using Discovery.ComponentServices.DataAccess;
using Discovery.ComponentServices.ExceptionHandling;
using Discovery.Utility;
using Discovery.Utility.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Discovery.BusinessObjects.Controllers
{
    /// <summary>
    /// A class to provide the trunker day controller which is a business object controller
    /// with namespace Discovery.BusinessObjects.Controllers
    /// </summary>
    public static class TrunkerDaysController
    {
        private static List<Warehouse> allWarehouses;

        /// <summary>
        /// Deletes the trunker day.
        /// </summary>
        /// <param name="trunkerDay">The trunker day.</param>
        /// <returns></returns>
        public static bool DeleteTrunkerDay(TrunkerDay trunkerDay)
        {
            bool success = false;
            try
            {
                if (trunkerDay != null)
                {
                  success = DataAccessProvider.Instance().DeleteTrunkerDay(trunkerDay.Id);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return success;
        }

        /// <summary>
        /// Gets the trunker day.
        /// </summary>
        /// <param name="trunkerId">The trunker id.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fullyPopulate].</param>
        /// <returns></returns>
        public static TrunkerDay GetTrunkerDay(int trunkerId, bool fullyPopulate)
        {
            TrunkerDay trunkerDay = new TrunkerDay();
            try
            {
                    trunkerDay = CBO<TrunkerDay>.FillObject(DataAccessProvider.Instance().GetTrunkerDay(trunkerId),FullyPopulate,fullyPopulate);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return trunkerDay;
        }

        /// <summary>
        /// Fully populates the Trunker day
        /// </summary>
        /// <param name="trunkerDay">The trunker day.</param>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fullyPopulate].</param>
        private static void FullyPopulate( TrunkerDay trunkerDay,IDataReader dataReader,bool fullyPopulate)
        {
            if (fullyPopulate && trunkerDay != null)
            {
                if (allWarehouses!=null && allWarehouses.Count>0)
                {
                    trunkerDay.SourceWarehouse = allWarehouses.Find(delegate(Warehouse obj) {return obj.Id ==trunkerDay.SourceWarehouseId;});
                    trunkerDay.DestinationWarehouse =allWarehouses.Find(delegate(Warehouse obj) {return obj.Id ==trunkerDay.DestinationWarehouseId; });
                }
                else
                {
                    trunkerDay.SourceWarehouse = WarehouseController.GetWarehouse(trunkerDay.SourceWarehouseId);
                    trunkerDay.DestinationWarehouse =WarehouseController.GetWarehouse(trunkerDay.DestinationWarehouseId);
                }
            }
        }

        /// <summary>
        /// Gets the number of trunker days.
        /// </summary>
        /// <param name="stockWarehouse">The stock warehouse.</param>
        /// <param name="destinationWarehouse">The destination warehouse.</param>
        /// <returns></returns>
        public static int GetNumberOfTrunkerDays(Warehouse stockWarehouse, Warehouse destinationWarehouse)
        {
            Int32 trunkerDays = -1;
            try
            {
                    trunkerDays = DataAccessProvider.Instance().GetNumberOfTrunkerDays(stockWarehouse, destinationWarehouse);

                if (trunkerDays == -2)
                {
                    throw new Exception(
                        string.Format("The number of trunker days have not been defined between '{0}' and '{1}'",
                                      stockWarehouse.Description, destinationWarehouse.Description));
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return trunkerDays;
        }


        /// <summary>
        /// Gets the trunker days.
        /// </summary>
        /// <param name="fullyPopulate">if set to <c>true</c> [fullyPopulate].</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        public static List<TrunkerDay> GetTrunkerDays(bool fullyPopulate, string sortExpression)
        {
            List<TrunkerDay> trunkerDays = GetTrunkerDays(fullyPopulate);
            if (string.IsNullOrEmpty(sortExpression))
            {
                sortExpression = "SourceWarehouse.Description";
            }
            trunkerDays.Sort(new UniversalComparer<TrunkerDay>(sortExpression));

            return trunkerDays;
        }

        /// <summary>
        /// Gets the trunker days.
        /// </summary>
        /// <param name="fullyPopulate">if set to <c>true</c> [fullyPopulate].</param>
        /// <returns></returns>
        public static List<TrunkerDay> GetTrunkerDays(bool fullyPopulate)
        {
            List<TrunkerDay> trunkerDays = new List<TrunkerDay>();
            try
            {
                allWarehouses = WarehouseController.GetWarehouses();
                trunkerDays = CBO<TrunkerDay>.FillCollection(DataAccessProvider.Instance().GetTrunkerDays(),FullyPopulate,fullyPopulate);
                allWarehouses = null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return trunkerDays;
        }

        /// <summary>
        /// Saves the trunker day.
        /// </summary>
        /// <param name="trunkerDay">The trunker day.</param>
        /// <returns></returns>
        public static int SaveTrunkerDay(TrunkerDay trunkerDay)
        {
            try
            {
                if (trunkerDay.IsValid)
                {
                    // Save entity
                    trunkerDay.Id =  DataAccessProvider.Instance().SaveTrunkerDay(trunkerDay);
                }
                else
                {
                    // Entity is not valid
                    throw new InValidBusinessObjectException(trunkerDay);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            // Done
            return trunkerDay.Id;
        }
    }
}