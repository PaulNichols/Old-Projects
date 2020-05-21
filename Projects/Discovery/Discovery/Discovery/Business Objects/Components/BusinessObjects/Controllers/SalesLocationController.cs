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
   ** CLASS:	SalesLocationController
   **
   ** OVERVIEW:
   ** This controller class contains all methods related to Sales Location including calling data access methods
   **
   ** MODIFICATION HISTORY:
   **
   ** Date:		Version:    Who:	Change:
   ** 19/7/06		1.0			PJN		Initial Version
   ************************************************************************************************/
    /// <summary>
    /// A class to provide the sales location controller which is a business object controller
    /// with namespace Discovery.BusinessObjects.Controllers
    /// </summary>

    public static class SalesLocationController
    {
        private static List<OpCo> allOpCos;

        /// <summary>
        /// Saves the specified SalesLocation to the underlying data store via the configured DataProvider.
        /// If the primary key (ID) of the supplied SalesLocation is Null, a new row is added to the underlying data store and a new primary key (ID) automatically generated.  If the primary key (ID) of the supplied SalesLocation has been specified, the existing row in the data store is updated if it has not already been altered.
        /// The primary key (ID) of the created or altered SalesLocation is returned to the caller.  If an error occurs, an exception is thrown and no updates made to the data store.
        /// For concurrency errors (the data has changed in-between load and save by an external system or user), a concurrency exception is thrown.
        /// </summary>
        /// <param name="salesLocation">The salesLocation.</param>
        /// <returns></returns>
        public static int SaveLocation(SalesLocation salesLocation)
        {
            try
            {
                if (salesLocation.IsValid)
                {
                    salesLocation.OpCo = OpcoController.GetOpCo(salesLocation.OpCoId, true);
                    // Save entity
                    salesLocation.Id = DataAccessProvider.Instance().SaveLocation(salesLocation);
                    if (salesLocation.Id!=-1)
                    {
                         FrameworkController.GetChecksum(salesLocation);
                        CacheManager.Add(salesLocation,salesLocation.OpCo!=null);
                    }
                }
                else
                {
                    // Entity is not valid
                    throw new InValidBusinessObjectException(salesLocation);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            // Done
            return salesLocation.Id;
        }

        /// <summary>
        /// Deletes the specified SalesLocation record from the underlying data store via the configured DataProvider.
        /// If the specified SalesLocation is deleted true is returned otherwise false.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <param name="location">The Sales Location to delete.</param>
        /// <returns></returns>
        public static bool DeleteLocation(SalesLocation location)
        {
            bool success = false;
            try
            {
                if (location != null)
                {
                    success = DataAccessProvider.Instance().DeleteLocation(location.Id); ;
                    if (success)
                    {
                        CacheManager.Remove(location);
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
        /// Retrieves a single SalesLocation entry from the underlying data store via the configured DataProvider for the supplied SalesLocation ID.
        ///An instance of a SalesLocation is returned to the caller or Null if no SalesLocation record is found.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <param name="locationId">The location id.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fullyPopulate].</param>
        /// <returns></returns>
        public static SalesLocation GetLocation(int locationId, bool fullyPopulate)
        {
            SalesLocation salesLocation = null;
            try
            {
                salesLocation = CacheManager.Get<SalesLocation>(locationId, fullyPopulate);
                if (salesLocation==null)
                {
                    salesLocation = CBO<SalesLocation>.FillObject(DataAccessProvider.Instance().GetLocation(locationId), FullyPopulate, fullyPopulate);
                    if (salesLocation!=null)
                    {
                        CacheManager.Add(salesLocation,fullyPopulate);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return salesLocation;
        }

        /// <summary>
        /// Retrieves a single SalesLocation entry from the underlying data store via the configured DataProvider for the supplied SalesLocation ID.
        /// An instance of a SalesLocation is returned to the caller or Null if no SalesLocation record is found.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fullyPopulate].</param>
        /// <returns></returns>
        public static SalesLocation GetLocation(string code, bool fullyPopulate)
        {
            SalesLocation salesLocation = new SalesLocation();
            try
            {
                salesLocation = CacheManager.Get<SalesLocation>(code, fullyPopulate);
                if (salesLocation==null)
                {
                    salesLocation = CBO<SalesLocation>.FillObject(DataAccessProvider.Instance().GetLocation(code), FullyPopulate, fullyPopulate);
                    if (salesLocation!=null)
                    {
                       CacheManager.Add(salesLocation,salesLocation.Location,fullyPopulate);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return salesLocation;
        }

        /// <summary>
        /// Fully populates the SalesLocation
        /// </summary>
        /// <param name="salesLocation">The salesLocation.</param>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fullyPopulate].</param>
        private static void FullyPopulate( SalesLocation salesLocation,IDataReader dataReader, bool fullyPopulate)
        {
            if (fullyPopulate && salesLocation != null)
            {
                if (allOpCos!=null && allOpCos.Count>0)
                {
                    salesLocation.OpCo = allOpCos.Find(delegate(OpCo obj) { return obj.Id==salesLocation.OpCoId; });
                }
                else
                {
                    salesLocation.OpCo = OpcoController.GetOpCo(salesLocation.OpCoId, true);
                }
            }
        }

        /// <summary>
        /// Retrieves a list of all Locations from the underlying data store via the configured DataProvider.
        /// A strongly typed list of SalesLocation entities is returned to the caller or an empty list if no records were found.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <param name="fullyPopulate">if set to <c>true</c> [fullyPopulate].</param>
        /// <returns></returns>
        public static List<SalesLocation> GetLocations(bool fullyPopulate)
        {
            List<SalesLocation> locations = new List<SalesLocation>();
            try
            {
                allOpCos = OpcoController.GetOpCos();
                locations = CBO<SalesLocation>.FillCollection(DataAccessProvider.Instance().GetLocations(),FullyPopulate,fullyPopulate);
                allOpCos = null;
                foreach (SalesLocation location in locations)
                {
                    CacheManager.Add(location,fullyPopulate);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return locations;
        }

            /// <summary>
        /// Gets the sales locations.
        /// </summary>
        /// <param name="fullyPopulate">if set to <c>true</c> [fullyPopulate].</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        public static List<SalesLocation> GetLocations(bool fullyPopulate, string sortExpression)
        {
            List<SalesLocation> locations = GetLocations(fullyPopulate);
            if (string.IsNullOrEmpty(sortExpression))
            {
                sortExpression = "Location";
            }
            locations.Sort(new UniversalComparer<SalesLocation>(sortExpression));

            return locations;
        }

        public static List<SalesLocation> GetOpCoSalesLocations(string opCoCode)
        {
            List<SalesLocation> listSalesLocations = null;
            try
            {
                listSalesLocations = CBO<SalesLocation>.FillCollection(DataAccessProvider.Instance().GetOpCoSalesLocations(
                            opCoCode),
                            null,
                            false);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return listSalesLocations;
        }

        public static List<SalesLocation> GetTDCSalesLocations(string opCoCode)
        {
            List<SalesLocation> listSalesLocations = null;
            try
            {
                listSalesLocations = CBO<SalesLocation>.FillCollection(DataAccessProvider.Instance().GetTDCSalesLocations(
                            opCoCode),
                            null,
                            false);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return listSalesLocations;
        }
    }
}