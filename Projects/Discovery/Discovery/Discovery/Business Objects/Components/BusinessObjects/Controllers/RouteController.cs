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
    /*************************************************************************************************
   ** CLASS:	RouteController
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
    /// A class to provide the route controller which is a business object controller
    /// with namespace Discovery.BusinessObjects.Controllers
    /// </summary>
    public static class RouteController
    {
        private static int count;

        /// <summary>
        /// Gets the opco routes.
        /// </summary>
        /// <returns></returns>
        public static List<Route> GetOpCoRoutes()
        {
            List<Route> routes = new List<Route>();
            try
            {
                routes = CBO<Route>.FillCollection(DataAccessProvider.Instance().GetOpCoRoutes());
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return routes;
        }

        /// <summary>
        /// Gets the routes.
        /// </summary>
        /// <returns></returns>
        public static List<Route> GetRoutes()
        {
            return GetRoutes("", 0, 0, false);
        }

        /// <summary>
        /// Gets the routes.
        /// </summary>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fully populate].</param>
        /// <returns></returns>
        public static List<Route> GetRoutes(string sortExpression,
                                            bool fullyPopulate)
        {
            return GetRoutes(sortExpression, 0, 0, fullyPopulate);
        }

        /// <summary>
        /// Gets the routes.
        /// </summary>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="startRowIndex">Start index of the row.</param>
        /// <param name="maximumRows">The maximum rows.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fully populate].</param>
        /// <returns></returns>
        public static List<Route> GetRoutes(string sortExpression,
                                            int startRowIndex,
                                            int maximumRows,
                                            bool fullyPopulate)
        {
            int totalRows = 0;
            List<Route> routes = new List<Route>();
            try
            {
                routes = CBO<Route>.FillCollection(
                            DataAccessProvider.Instance().GetRoutes(
                                                            sortExpression,
                                                            startRowIndex,
                                                            maximumRows,
                                                            out totalRows),
                                                            CustomFill,
                                                            fullyPopulate);

                if (routes != null) foreach (Route route in routes)
                    {
                        CacheManager.Add(route, fullyPopulate);
                    }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            if (string.IsNullOrEmpty(sortExpression))
            {
                sortExpression = "Code";
            }
            if (routes != null) routes.Sort(new UniversalComparer<Route>(sortExpression));
            count = totalRows;
            return routes;
        }

        public static Int32 NumberOfRoutesCount(
                        int warehouseId,
                        string sortExpression,
                        int startRowIndex,
                        int maximumRows,
                        bool fullyPopulate)
        {
            return count;
        }

        /// <summary>
        /// Gets the routes.
        /// </summary>
        /// <param name="warehouseId">The warehouse id.</param>
        /// <returns></returns>
        public static List<Route> GetRoutes(int warehouseId)
        {
            return GetRoutes(warehouseId,"", 0, 0, false);
        }

        public static List<Route> GetRoutes(int warehouseId,
                                            string sortExpression,
                                            int startRowIndex,
                                            int maximumRows,
                                            bool fullyPopulate)
        {
            int totalRows = 0;
            List<Route> routes = new List<Route>();
            try
            {
                routes = CBO<Route>.FillCollection(
                            DataAccessProvider.Instance().GetRoutes(
                                                            warehouseId,
                                                            sortExpression,
                                                            startRowIndex,
                                                            maximumRows,
                                                            out totalRows),
                                                            CustomFill,
                                                            fullyPopulate);

                if (routes != null) foreach (Route route in routes)
                    {
                        CacheManager.Add(route, fullyPopulate);
                    }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            if (string.IsNullOrEmpty(sortExpression))
            {
                sortExpression = "Code";
            }
            if (routes != null) routes.Sort(new UniversalComparer<Route>(sortExpression));
            count = totalRows;
            return routes;
        }

        /// <summary>
        /// Deletes the specified route.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns></returns>
        public static bool DeleteRoute(Route route)
        {
            bool success = false;
            try
            {
                if (route != null)
                {
                    success = DataAccessProvider.Instance().DeleteRoute(route.Id);
                    if (success) CacheManager.Remove(route);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return success;
        }

        /// <summary>
        /// Gets a route by Id.
        /// </summary>
        /// <param name="routeId">The route id.</param>
        /// <returns></returns>
        public static Route GetRoute(int routeId)
        {
            Route route = null;
            try
            {
                route = CacheManager.Get<Route>(routeId);
                if (route == null)
                {
                    route = CBO<Route>.FillObject(DataAccessProvider.Instance().GetRoute(routeId), CustomFill, true);
                    if (route != null)
                    {
                        CacheManager.Add(route);
                    }
                }

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return route;
        }

        /// <summary>
        /// Gets a route for a given route code.
        /// </summary>
        /// <param name="routeCode">The route code.</param>
        /// <returns></returns>
        public static Route GetRoute(string warehouseCode, string routeCode)
        {
            Route route = null;
            try
            {
                route = CacheManager.Get<Route>(routeCode);
                if (route == null)
                {
                    route = CBO<Route>.FillObject(DataAccessProvider.Instance().GetRoute(warehouseCode, routeCode), CustomFill, true);
                    if (route != null)
                    {
                        CacheManager.Add(route);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return route;
        }

        /// <summary>
        /// Gets a route for a given route code.
        /// </summary>
        /// <param name="routeCode">The route code.</param>
        /// <returns></returns>
        public static Route GetRoute(string routeCode)
        {
            Route route = null;
            try
            {
                route = CacheManager.Get<Route>(routeCode);
                if (route == null)
                {
                    route = CBO<Route>.FillObject(DataAccessProvider.Instance().GetRoute(routeCode), CustomFill, true);
                    if (route != null)
                    {
                        CacheManager.Add(route);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return route;
        }


        public static void CustomFill(Route item, IDataReader dataReader, bool fullyPopulate)
        {
            if (item != null)
            {
                if (fullyPopulate)
                {
                    item.Warehouse = WarehouseController.GetWarehouse((int)dataReader["WarehouseId"], fullyPopulate);
                }
            }
        }

        /// <summary>
        /// Saves a route.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns></returns>
        public static int SaveRoute(Route route)
        {
            try
            {
                if (route.IsValid)
                {
                    route.Id = DataAccessProvider.Instance().SaveRoute(route);
                    if (route.Id != -1)
                    {
                        FrameworkController.GetChecksum(route);
                        CacheManager.Add(route,route.Warehouse!=null);
                    }
                }
                else
                {
                    // Entity is not valid
                    throw new InValidBusinessObjectException(route);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            // Done
            return route.Id;
        }

        public static List<Route> GetTDCRoutesByWarehouseCode(string warehouseCode)
        {
            List<Route> listRoutes = null;
            try
            {
                listRoutes = CBO<Route>.FillCollection(DataAccessProvider.Instance().GetTDCRoutesByWarehouseCode(
                            warehouseCode),
                            null,
                            false);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return listRoutes;
        }

    }
}