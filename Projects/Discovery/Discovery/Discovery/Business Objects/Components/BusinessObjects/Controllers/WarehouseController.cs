using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using Discovery.ComponentServices.DataAccess;
using Discovery.ComponentServices.ExceptionHandling;
using Discovery.Utility;
using Discovery.Utility.DataAccess;

using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Discovery.BusinessObjects.Controllers
{
    /// <summary>
    /// A class 'WarehouseController' which is a business object controller
    /// with namespace Discovery.BusinessObjects.Controllers. It provides all methods to access/update 
    /// the warehouse table.
    /// </summary>
    public static class WarehouseController
    {
        private static List<OptrakRegion> allRegions;

        /// <summary>
        /// Gets the warehouses.
        /// </summary>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        public static List<Warehouse> GetWarehouses(string sortExpression)
        {
            return GetWarehouses(sortExpression, true);
        }

        /// <summary>
        /// Gets the op co stock warehouse codes.
        /// </summary>
        /// <param name="opCoCode">The op co code.</param>
        /// <returns></returns>
        public static List<Warehouse> GetOpCoStockWarehouseCodes(string opCoCode)
        {
            List<Warehouse> warehouses = null;
            try
            {
                warehouses = CBO<Warehouse>.FillCollection(DataAccessProvider.Instance().GetOpCoStockWarehouseCodes(
                            opCoCode),
                            null,
                            false);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return warehouses;
        }

        /// <summary>
        /// Gets the op co delivery warehouse codes.
        /// </summary>
        /// <param name="opCoCode">The op co code.</param>
        /// <returns></returns>
        public static List<Warehouse> GetOpCoDeliveryWarehouseCodes(string opCoCode)
        {
            List<Warehouse> warehouses = null;
            try
            {
                warehouses = CBO<Warehouse>.FillCollection(DataAccessProvider.Instance().GetOpCoDeliveryWarehouseCodes(
                            opCoCode),
                            null,
                            false);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return warehouses;
        }

        public static List<Warehouse> GetTDCDeliveryWarehouseCodes(string opCoCode)
        {
            List<Warehouse> warehouses = null;
            try
            {
                warehouses = CBO<Warehouse>.FillCollection(DataAccessProvider.Instance().GetTDCDeliveryWarehouseCodes(
                            opCoCode),
                            null,
                            false);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return warehouses;
        }

        /// <summary>
        /// Gets the warehouses.
        /// </summary>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="fullyPopualte">if set to <c>true</c> [fully popualte].</param>
        /// <returns></returns>
        public static List<Warehouse> GetWarehouses(string sortExpression, bool fullyPopualte)
        {
            List<Warehouse> warehouses = GetWarehouses(fullyPopualte);
            if (string.IsNullOrEmpty(sortExpression))
            {
                sortExpression = "Code";
            }
            warehouses.Sort(new UniversalComparer<Warehouse>(sortExpression));

            return warehouses;
        }

        /// <summary>
        /// Saves the warehouse.
        /// </summary>
        /// <param name="warehouse">The warehouse.</param>
        /// <returns></returns>
        public static int SaveWarehouse(Warehouse warehouse)
        {
            try
            {
                if (warehouse.IsValid)
                {
                    // Save entity
                    warehouse.Id = DataAccessProvider.Instance().SaveWarehouse(warehouse);
                    if (warehouse.Id != -1)
                    {
                        FrameworkController.GetChecksum(warehouse,"Warehouse");
                        CacheManager.Add(warehouse);
                    }
                }
                else
                {
                    // Entity is not valid
                    throw new InValidBusinessObjectException(warehouse);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            // Done
            return warehouse.Id;
        }

        /// <summary>
        /// Deletes the warehouse.
        /// </summary>
        /// <param name="warehouse">The warehouse.</param>
        /// <returns></returns>
        public static bool DeleteWarehouse(Warehouse warehouse)
        {
            bool success = false;
            try
            {
                if (warehouse != null)
                {
                    success = DataAccessProvider.Instance().DeleteWarehouse(warehouse.Id);
                    if (success)
                    {
                        CacheManager.Remove(warehouse);
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
        /// Gets the warehouse.
        /// </summary>
        /// <param name="warehouseId">The warehouse id.</param>
        /// <returns></returns>
        public static Warehouse GetWarehouse(int warehouseId)
        {
            return GetWarehouse(warehouseId, true);
        }

        /// <summary>
        /// Gets the warehouse.
        /// </summary>
        /// <param name="warehouseId">The warehouse id.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fully populate].</param>
        /// <returns></returns>
        public static Warehouse GetWarehouse(int warehouseId, bool fullyPopulate)
        {
            Warehouse warehouse = new Warehouse();
            try
            {
                warehouse = CacheManager.Get<Warehouse>(warehouseId, fullyPopulate);
                if (warehouse == null)
                {
                    warehouse = CBO<Warehouse>.FillObject(
                        DataAccessProvider.Instance().GetWarehouse(warehouseId),
                        CustomFill,
                        fullyPopulate);

                    if (warehouse != null)
                    {
                        CacheManager.Add(warehouse, fullyPopulate);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return warehouse;
        }

        /// <summary>
        /// Gets the warehouse.
        /// </summary>
        /// <param name="warehouseCode">The warehouse code.</param>
        /// <returns></returns>
        public static Warehouse GetWarehouse(string warehouseCode)
        {
            return GetWarehouse(warehouseCode, true);
        }

        /// <summary>
        /// Gets the warehouse.
        /// </summary>
        /// <param name="warehouseCode">The warehouse code.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fully populate].</param>
        /// <returns></returns>
        public static Warehouse GetWarehouse(string warehouseCode, bool fullyPopulate)
        {
            Warehouse warehouse = null;
            try
            {
                warehouse = CacheManager.Get<Warehouse>(warehouseCode, fullyPopulate);
                if (warehouse == null)
                {
                    warehouse = CBO<Warehouse>.FillObject(
                        DataAccessProvider.Instance().GetWarehouse(warehouseCode),
                        CustomFill,
                        fullyPopulate);

                    if (warehouse != null)
                    {
                        CacheManager.Add(warehouse, fullyPopulate);
                    }
                }

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return warehouse;
        }

        /// <summary>
        /// Gets the warehouses.
        /// </summary>
        /// <param name="fullyPopualte">if set to <c>true</c> [fully popualte].</param>
        /// <returns></returns>
        public static List<Warehouse> GetWarehouses(bool fullyPopualte)
        {
            List<Warehouse> warehouses = new List<Warehouse>();
            try
            {
                if (fullyPopualte)
                {
                    allRegions = OptrakRegionController.GetRegions();
                }
                warehouses = CBO<Warehouse>.FillCollection(DataAccessProvider.Instance().GetWarehouses(), CustomFill, fullyPopualte);
                allRegions = null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            if (warehouses != null) foreach (Warehouse warehouse in warehouses)
                {
                    CacheManager.Add(warehouse, fullyPopualte);
                }
            return warehouses;
        }

        /// <summary>
        /// Gets the warehouses.
        /// </summary>
        /// <returns></returns>
        public static List<Warehouse> GetWarehouses()
        {
            return GetWarehouses(true);
        }

        /// <summary>
        /// Gets the warehouses by region code.
        /// </summary>
        /// <param name="regionCode">The region code.</param>
        /// <returns></returns>
        public static List<Warehouse> GetWarehousesByRegionCode(string regionCode)
        {
            return GetWarehousesByRegionCode(regionCode, true);
        }

        /// <summary>
        /// Gets the warehouses by region code.
        /// </summary>
        /// <param name="regionCode">The region code.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fully populate].</param>
        /// <returns></returns>
        public static List<Warehouse> GetWarehousesByRegionCode(string regionCode, bool fullyPopulate)
        {
            List<Warehouse> warehouses = new List<Warehouse>();
            try
            {
                if (!string.IsNullOrEmpty(regionCode) && regionCode != "ALL")
                {

                    warehouses = CBO<Warehouse>.FillCollection(DataAccessProvider.Instance().GetWarehouses(regionCode), CustomFill, fullyPopulate);
                    return warehouses;

                }
                else
                {
                    warehouses = GetWarehouses();
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return warehouses;
        }

        /// <summary>
        /// Gets the warehouses by region.
        /// </summary>
        /// <param name="regionId">The region id.</param>
        /// <returns></returns>
        public static List<Warehouse> GetWarehousesByRegion(int regionId)
        {
            return GetWarehousesByRegion(regionId, false);
        }

        /// <summary>
        /// Gets the warehouses by region.
        /// </summary>
        /// <param name="regionId">The region id.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fully populate].</param>
        /// <returns></returns>
        public static List<Warehouse> GetWarehousesByRegion(int regionId, bool fullyPopulate)
        {
            List<Warehouse> warehouses = new List<Warehouse>();
            try
            {
                if (regionId > 0)
                {
                    warehouses = CBO<Warehouse>.FillCollection(DataAccessProvider.Instance().GetWarehousesByRegion(regionId), CustomFill, fullyPopulate);
                }
                else
                {
                    warehouses = GetWarehouses();
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return warehouses;
        }

        /// <summary>
        /// Customs the fill.
        /// </summary>
        /// <param name="warehouse">The warehouse.</param>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fully populate].</param>
        public static void CustomFill(Warehouse warehouse, IDataReader dataReader, bool fullyPopulate)
        {
            if (warehouse != null)
            {
                if (fullyPopulate)
                {
                    if (allRegions != null && allRegions.Count > 0)
                    {
                        warehouse.OptrakRegion =
                            allRegions.Find(delegate(OptrakRegion obj) { return obj.Id == warehouse.RegionId; });
                    }
                    else
                    {
                        warehouse.OptrakRegion = OptrakRegionController.GetRegion(warehouse.RegionId);
                    }


                }

                //contact
                warehouse.Contact = new Contact();
                warehouse.Contact.Name = dataReader["ContactName"].ToString();
                warehouse.Contact.TelephoneNumber = dataReader["ContactTelephone"].ToString();
                warehouse.Contact.Email = dataReader["SalesEmail"].ToString();
                //address
                warehouse.Address = new Address();
                warehouse.Address.Line1 = dataReader["AddressLine1"].ToString();
                warehouse.Address.Line2 = dataReader["AddressLine2"].ToString();
                warehouse.Address.Line3 = dataReader["AddressLine3"].ToString();
                warehouse.Address.Line4 = dataReader["AddressLine4"].ToString();
                warehouse.Address.PostCode = dataReader["PostCode"].ToString();
            }
        }
    }
}