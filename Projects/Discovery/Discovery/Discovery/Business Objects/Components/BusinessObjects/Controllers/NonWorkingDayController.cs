/*************************************************************************************************
 ** FILE:	NonworkingdayController.cs
 ** DATE:	30/05/2006
 ** AUTHOR:	Trung Lo
 **
 **
 ** OVERVIEW:
 **
 ** MODIFICATION HISTORY:
 **
 ** Date:		Version:	Who:	Change:
 ** 30/5/06		1.0		    TVN	    Initial Version
 ************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using Discovery.ComponentServices.DataAccess;
using Discovery.Utility;
using Discovery.Utility.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Discovery.BusinessObjects.Controllers
{
    /// <summary>
    /// A class to provide the non-working day controller which is a business object controller
    /// with namespace Discovery.BusinessObjects.Controllers
    /// </summary>
    public static class NonWorkingDayController
    {
        /// <summary>
        /// Returns the next working day, taking into account any holidays specified
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="nonWorkingDays">The non working days.</param>
        /// <returns></returns>
        public static DateTime NextWorkingDate(DateTime startDate, List<NonWorkingDay> nonWorkingDays)
        {
            bool isWorkingDay;

            do
            {
                //add 1 to the passed date
                startDate = startDate.AddDays(1);
                //persume it's a working day until we find out otherwise
                isWorkingDay = true;
                //loop though passed in nonworking days
                foreach (NonWorkingDay nonWorkingDay in nonWorkingDays)
                {
                    if (nonWorkingDay.NonWorkingDate == startDate)
                    {
                        //if we find our new working date to be a non working day the
                        // end loop and try adding one again
                        isWorkingDay = false;
                        break;
                    }
                }
            } while (!isWorkingDay);

            return startDate;
        }

        // -----------------------------------------------------------------------------
        // Perform the deletion of a non-working day row in Dicovery_NonWorkingday table
        // by passing a NonWorkingDay ID
        // -----------------------------------------------------------------------------

        /// <summary>
        /// Perform the deletion of a non-working day row in Dicovery_NonWorkingday table
        /// by passing a NonWorkingDay ID
        /// </summary>
        /// <param name="nonWorkingDay">The non working day.</param>
        /// <returns></returns>
        public static bool DeleteNonWorkingDay(NonWorkingDay nonWorkingDay)
        {
            bool success = false;
            try
            {
                if (nonWorkingDay != null)
                {
                    success = DataAccessProvider.Instance().DeleteNonWorkingDay(nonWorkingDay.Id);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return success;
        }

        // ------------------------------------------------------------------------------
        // Perform the retrieval of a non-working day row in Dicovery_NonWorkingday table
        // by passing a NonWorkingDay ID
        // ------------------------------------------------------------------------------

        /// <summary>
        /// Perform the retrieval of a non-working day row in Dicovery_NonWorkingday table
        /// by passing a NonWorkingDay ID
        /// </summary>
        /// <param name="nonWorkingDayId">The non working day id.</param>
        /// <returns></returns>
        public static NonWorkingDay GetNonWorkingDay(int nonWorkingDayId)
        {
            return
                CBO<NonWorkingDay>.FillObject(DataAccessProvider.Instance().GetNonWorkingDay(nonWorkingDayId),
                                              FullyPopulate, true);
        }

        // ------------------------------------------------------------------------------
        // Perform the retrieval of a non-working day row in Dicovery_NonWorkingday table
        // by passing a warehouse code an a specific date
        // ------------------------------------------------------------------------------

        /// <summary>
        /// Perform the retrieval of a non-working day row in Dicovery_NonWorkingday table
        /// by passing a warehouse code an a specific date
        /// </summary>
        /// <param name="warehouseCode">The warehouse code.</param>
        /// <param name="nonWorkingDate">The non working date.</param>
        /// <returns></returns>
        public static NonWorkingDay GetNonWorkingDay(string warehouseCode, DateTime nonWorkingDate)
        {
            return
                CBO<NonWorkingDay>.FillObject(
                    DataAccessProvider.Instance().GetNonWorkingDay(warehouseCode, nonWorkingDate));
        }

        // --------------------------------------------------------------------------------
        // Perform the retrieval of all non-working day row in Dicovery_NonWorkingday table
        // --------------------------------------------------------------------------------

        /// <summary>
        /// Perform the retrieval of all sorted non-working day rows in Dicovery_NonWorkingday table
        /// </summary>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        public static List<NonWorkingDay> GetNonWorkingDays(string sortExpression)
        {
            List<NonWorkingDay> nonWorkingDays = GetNonWorkingDays();

            if (string.IsNullOrEmpty(sortExpression))
            {
                sortExpression = "NonWorkingDate";
            }
            nonWorkingDays.Sort(new UniversalComparer<NonWorkingDay>(sortExpression));
            return nonWorkingDays;
        }

        /// <summary>
        /// Gets the non working days.
        /// </summary>
        /// <returns></returns>
        public static List<NonWorkingDay> GetNonWorkingDays()
        {
            return
                CBO<NonWorkingDay>.FillCollection(DataAccessProvider.Instance().GetNonWorkingDays(), FullyPopulate, true);
        }

        /// <summary>
        /// Numbers the of non working days.
        /// </summary>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date to.</param>
        /// <param name="warehouseId">The warehouse id.</param>
        /// <param name="regionId">The region id.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <returns></returns>
        public static Int32 NumberOfNonWorkingDays(
            Nullable<DateTime> dateFrom,
            Nullable<DateTime> dateTo,
            int warehouseId,
            int regionId,
            string sortExpression,
            int startRowIndex,
            int maximumRows
            )
        {
            return count;
        }

        private static int count;

        /// <summary>
        /// Gets the non working days.
        /// </summary>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date to.</param>
        /// <param name="warehouseId">The warehouse id.</param>
        /// <param name="regionId">The region id.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <returns></returns>
        public static List<NonWorkingDay> GetNonWorkingDays
            (
            Nullable<DateTime> dateFrom,
            Nullable<DateTime> dateTo,
            int warehouseId,
            int regionId,
            string sortExpression,
            int startRowIndex,
            int maximumRows
            )
        {
            int totalRows = 0;
            List<NonWorkingDay> nonWorkingDays = new List<NonWorkingDay>();
            if (dateTo != null && dateFrom != null)
            {
                //if (warehouseId == 0 && regionId == 0)
                //{
                int rows;

                nonWorkingDays =
                    CBO<NonWorkingDay>.FillCollection(
                        DataAccessProvider.Instance().GetNonWorkingDays(dateFrom.Value,
                                                                        dateTo.Value,
                                                                        warehouseId,
                                                                        regionId,
                                                                        sortExpression,
                                                                        startRowIndex,
                                                                        maximumRows,
                                                                        out rows),
                        PopulateWarehouseCode, false);


                totalRows = rows;
                //}
                //else if (warehouseId == 0)
                //{
                //    nonWorkingDays = GetNonWorkingDaysByRegion(dateFrom.Value, dateTo.Value, regionId, sortExpression);
                //}
                //else
                //{
                //    nonWorkingDays =
                //        GetNonWorkingDaysByWarehouse(dateFrom.Value, dateTo.Value, warehouseId, sortExpression);
                //}


                if (string.IsNullOrEmpty(sortExpression))
                {
                    sortExpression = "NonWorkingDate";
                }
                nonWorkingDays.Sort(new UniversalComparer<NonWorkingDay>(sortExpression));
            }
            count = totalRows;
            return nonWorkingDays;
        }

      
        /// <summary>
        /// Gets the non working days sorts the results.
        /// </summary>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date to.</param>
        /// <param name="regionId">The region.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        public static List<NonWorkingDay> GetNonWorkingDaysByRegion(DateTime dateFrom, DateTime dateTo, int regionId,
                                                                    string sortExpression)
        {
            List<NonWorkingDay> nonWorkingDays = GetNonWorkingDaysByRegion(dateFrom, dateTo, regionId);

            if (string.IsNullOrEmpty(sortExpression))
            {
                sortExpression = "NonWorkingDate";
            }
            nonWorkingDays.Sort(new UniversalComparer<NonWorkingDay>(sortExpression));
            return nonWorkingDays;
        }

        // --------------------------------------------------------------------------------
        // Perform the retrieval of non-working day rows in Dicovery_NonWorkingday table
        // by passing a specific date from, date to and a region code
        // --------------------------------------------------------------------------------

        /// <summary>
        /// Perform the retrieval of non-working day rows in Dicovery_NonWorkingday table
        /// by passing a specific date from, date to and a region code
        /// </summary>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date to.</param>
        /// <param name="regionId">The region id.</param>
        /// <returns></returns>
        public static List<NonWorkingDay> GetNonWorkingDaysByRegion(DateTime dateFrom, DateTime dateTo, int regionId)
        {
            return CBO<NonWorkingDay>.FillCollection(
                DataAccessProvider.Instance().GetNonWorkingDaysByRegion(dateFrom, dateTo, regionId), FullyPopulate, true);
        }

        /// <summary>
        /// Gets the non working days by warehouse and sorts the results.
        /// </summary>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date to.</param>
        /// <param name="warehouseId">The warehouse.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        public static List<NonWorkingDay> GetNonWorkingDaysByWarehouse(DateTime dateFrom, DateTime dateTo,
                                                                       int warehouseId, string sortExpression)
        {
            List<NonWorkingDay> nonWorkingDays = GetNonWorkingDaysByWarehouse(dateFrom, dateTo, warehouseId);

            if (string.IsNullOrEmpty(sortExpression))
            {
                sortExpression = "NonWorkingDate";
            }
            nonWorkingDays.Sort(new UniversalComparer<NonWorkingDay>(sortExpression));
            return nonWorkingDays;
        }

        // --------------------------------------------------------------------------------
        // Perform the retrieval of non-working day rows in Dicovery_NonWorkingday table
        // by passing a specific date from, date to and a warehouse code
        // --------------------------------------------------------------------------------

        /// <summary>
        /// Perform the retrieval of non-working day rows in Dicovery_NonWorkingday table
        /// by passing a specific date from, date to and a warehouse code
        /// </summary>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date to.</param>
        /// <param name="warehouseId">The warehouse id.</param>
        /// <returns></returns>
        public static List<NonWorkingDay> GetNonWorkingDaysByWarehouse(DateTime dateFrom, DateTime dateTo,
                                                                       int warehouseId)
        {
            return CBO<NonWorkingDay>.FillCollection(
                DataAccessProvider.Instance().GetNonWorkingDaysByWarehouse(dateFrom, dateTo, warehouseId), FullyPopulate,
                true);
        }

        /// <summary>
        /// Saves the non working day.
        /// </summary>
        /// <param name="nonWorkingDay">The non working day.</param>
        /// <returns></returns>
        public static int SaveNonWorkingDay(NonWorkingDay nonWorkingDay)
        {
            try
            {
                if (nonWorkingDay.IsValid)
                {
                    // Save entity
                    nonWorkingDay.Id = DataAccessProvider.Instance().SaveNonWorkingDay(
                        nonWorkingDay.Id,
                        nonWorkingDay.NonWorkingDate,
                        nonWorkingDay.Description,
                        nonWorkingDay.WarehouseId,
                        nonWorkingDay.UpdatedBy,
                        nonWorkingDay.CheckSum);
                }
                else
                {
                    // Entity is not valid
                    throw new InValidBusinessObjectException(nonWorkingDay);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            // Done
            return nonWorkingDay.Id;
        }

        // --------------------------------------------------------------------------------
        // Perform the update of non-working day rows in Dicovery_NonWorkingday table
        // such the given conditions
        // --------------------------------------------------------------------------------

        /// <summary>
        /// Perform the update of non-working day rows in Dicovery_NonWorkingday table
        /// such the given conditions
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="description">The description.</param>
        /// <param name="regionId">The region id.</param>
        /// <param name="warehouseId">The warehouse id.</param>
        /// <param name="weekendOnly">if set to <c>true</c> [weekend only].</param>
        /// <param name="updatedBy">The updated by.</param>
        /// <returns></returns>
       public static int SaveNonWorkingDays(DateTime startDate,
                                             DateTime endDate,
                                             string description,
                                             int regionId,
                                             int warehouseId,
                                             bool weekendOnly,
                                             string updatedBy)
        {
            int returnValue = 0;
            try
            {
                // Create an array containing non-working days (saturday and Sunday only)

                List<DateTime> DateList = new List<DateTime>();

                // Use a 'for' loop to create a date
                // (for i=0; i< numberOfDays; i++)
                // {
                //    date currentdate;
                //    currentdate = startdate + i day
                //    test the current date in Sat or Sun
                //    if (yes) add to the array
                // }

                // initialise a variable of i to zero and check if this variable is less than the input
                // number of days and if true then ccontinue, otherwise drop out.  i is incremented by 1

                int numberOfDays = CalculateNoOfDays(startDate, endDate);

                for (int i = 0; i < numberOfDays; i++)
                {
                    DateTime tempDate = startDate.AddDays(i);

                    if (weekendOnly)
                    {
                        if (tempDate.DayOfWeek == DayOfWeek.Saturday || tempDate.DayOfWeek == DayOfWeek.Sunday)
                        {
                            DateList.Add(tempDate);
                        }
                    }
                    else
                    {
                        DateList.Add(tempDate);
                    }
                }

                // If the array of the non-working days not empty,
                // then create an array containg Warehouses Id only with the 3 possible cases belows

                // If region code is empty, then call the method to find the warehouse id
                // If region code is entered and <> "ALL", then call the method to find all warehouses
                // which belong to this region.
                // If region code is entered and = "ALL", then call the method to read the warehouse
                // table sequentially

                if (DateList.Count != 0)
                {
                    List<int> WareshouseIdList = new List<int>();

                    // RegionId = 0 it means the user has selected 'ALL' regions. This implies
                    // that the process is for all warehouses.
                    if (regionId == -1)
                    {
                        List<Warehouse> warehouses =
                            CBO<Warehouse>.FillCollection(DataAccessProvider.Instance().GetWarehouses());

                        if (warehouses != null)
                        {
                            foreach (Warehouse warehouse in warehouses)
                            {
                                WareshouseIdList.Add(warehouse.Id);
                            }
                        }
                        //Warehouse warehouse = CBO<Warehouse>.FillObject(dataAccessProvider.GetWarehouse(warehouseId));

                        //if (warehouse != null)
                        //{
                        //    WareshouseIdList.Add(warehouse.Id);
                        //    //counter++;
                        //}
                    }
                    else
                    {
                        // WarehouseId = 0 it means the user has selected 'ALL' warehouses. This implies
                        // that the process is for all warehouses within the selected region.
                        if (warehouseId == -1)
                        {
                            List<Warehouse> warehouses =
                                CBO<Warehouse>.FillCollection(
                                    DataAccessProvider.Instance().GetWarehousesByRegion(regionId));

                            if (warehouses != null)
                            {
                                foreach (Warehouse warehouse in warehouses)
                                {
                                    WareshouseIdList.Add(warehouse.Id);
                                }
                            }
                        }
                        else
                        {
                            WareshouseIdList.Add(warehouseId);
                        }
                    }

                    // If the array of the Warehouse Id not emty,
                    // then populate the class NonWorkingDay by assigning fields from the 2 arrays, and
                    // call SaveNonWorkingDay method (passing NonWorkingDay object)

                    if (WareshouseIdList.Count != 0)
                    {
                        foreach (DateTime CurrentDate in DateList)
                        {
                            foreach (int WarehouseId in WareshouseIdList)
                            {
                                int nonWorkingDayId = -1;
                                int checkSum = 0;

                                if (string.IsNullOrEmpty(description))
                                {
                                    if (weekendOnly)
                                    {
                                        if (CurrentDate.DayOfWeek == DayOfWeek.Saturday)
                                        {
                                            description = "Saturday";
                                        }
                                        else
                                        {
                                            description = "Sunday";
                                        }
                                    }
                                    else
                                    {
                                        description = "Bank holiday/Xmas";
                                    }
                                }

                                returnValue = DataAccessProvider.Instance().SaveNonWorkingDay(
                                    nonWorkingDayId,
                                    CurrentDate,
                                    description,
                                    WarehouseId,
                                    updatedBy,
                                    checkSum);

                                if (returnValue == -1)
                                {
                                    return returnValue;
                                }

                                // See if we got a -1, if so we didn't create the entity as it already exists
                                //if (-1 == returnValue)
                                //{
                                //    // It's a constrain error, row already exists
                                //    NonWorkingDay existsingWorkingDay = NonWorkingDayController.GetNonWorkingDay();

                                //    // Add the new description, and save
                                //    existsingWorkingDay.Description = description;
                                //    existsingWorkingDay.UpdatedBy = updatedBy;
                                //    NonWorkingDayController.SaveNonWorkingDay(existsingWorkingDay);
                                //} 
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return returnValue;
        }

        /// <summary>
        /// Fully populates the warehouse code
        /// </summary>
        /// <param name="nonWorkingDay">The warehousecode.</param>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fullyPopulate].</param>
        private static void FullyPopulate(NonWorkingDay nonWorkingDay, IDataReader dataReader, bool fullyPopulate)
        {
            if (fullyPopulate && nonWorkingDay != null)
            {
                Warehouse warehouse = WarehouseController.GetWarehouse(nonWorkingDay.WarehouseId);
                nonWorkingDay.WarehouseCode = warehouse.Code;
            }
        }

        /// <summary>
        /// Fully populates the warehouse code
        /// </summary>
        /// <param name="nonWorkingDay">The warehousecode.</param>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fullyPopulate].</param>
        private static void PopulateWarehouseCode(NonWorkingDay nonWorkingDay, IDataReader dataReader, bool fullyPopulate)
        {

            nonWorkingDay.WarehouseCode = dataReader["WarehouseCode"].ToString();
           
        }


        /// <summary>
        /// Calculates the no of days between a given date range
        /// </summary>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date to.</param>
        /// <returns></returns>
        public static int CalculateNoOfDays(DateTime dateFrom, DateTime dateTo)
        {
            return (dateTo.Subtract(dateFrom).Days + 1);
        }
    }
}