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
    ** CLASS:	RegionController
    **
    ** OVERVIEW:
    ** This controller class contains all methods related to Regions including calling data access methods
    **
    ** MODIFICATION HISTORY:
    **
    ** Date:		Version:    Who:	Change:
    ** 19/7/06		1.0			PJN		Initial Version
    ************************************************************************************************/
    /// <summary>
    /// A Class 'RegionController' which is a business object controller
    /// </summary>
    public static class RegionController
    {

        /// <summary>
        /// Gets the regions.
        /// </summary>
        /// <returns></returns>
        public static List<Region> GetRegions()
        {
            List<Region> regions = new List<Region>();
            try
            {
                regions = CBO<Region>.FillCollection(DataAccessProvider.Instance().GetRegions());
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            if (regions != null) foreach (Region region in regions)
                {
                    CacheManager.Add(region);
                }
            return regions;
        }

        /// <summary>
        /// Gets the regionss.
        /// </summary>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        public static List<Region> GetRegions(string sortExpression)
        {
            List<Region> regions = GetRegions();

            if (string.IsNullOrEmpty(sortExpression))
            {
                sortExpression = "Code";
            }
            regions.Sort(new UniversalComparer<Region>(sortExpression));

            return regions;
        }

        /// <summary>
        /// Saves the region.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <returns></returns>
        public static int SaveRegion(Region region)
        {
            try
            {
                if (region.IsValid)
                {
                    // Save entity
                    region.Id = DataAccessProvider.Instance().SaveRegion(region);
                    if (region.Id > -1) CacheManager.Add(region);
                }
                else
                {
                    // Entity is not valid
                    throw new InValidBusinessObjectException(region);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            // Done
            return region.Id;
        }

        /// <summary>
        /// Deletes the region.
        /// </summary>
        /// <param name="region">The region to delete.</param>
        /// <returns></returns>
        public static bool DeleteRegion(Region region)
        {
            bool success = false;
            try
            {
                if (region != null)
                {
                    success = DataAccessProvider.Instance().DeleteRegion(region.Id);
                    if (success) CacheManager.Remove(region);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return success;
        }

        /// <summary>
        /// Gets the region.
        /// </summary>
        /// <param name="regionId">The region id.</param>
        /// <returns></returns>
        public static Region GetRegion(int regionId)
        {
            Region region = null;
            try
            {
                region = CacheManager.Get<Region>(regionId);
                if (region == null)
                {
                    region = CBO<Region>.FillObject(DataAccessProvider.Instance().GetRegion(regionId));
                    if (region != null) CacheManager.Add(region);
                }

                return region;

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return region;
        }


        /// <summary>
        /// Gets the region by code.
        /// </summary>
        /// <param name="regionCode">The region code.</param>
        /// <returns></returns>
        public static Region GetRegion(string regionCode)
        {
            Region region = null;

            try
            {

                region = CacheManager.Get<Region>(regionCode);
                if (region == null)
                {
                    region = CBO<Region>.FillObject(DataAccessProvider.Instance().GetRegion(regionCode));
                    if (region != null) CacheManager.Add(region);
                }

                return region;

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            return region;
        }


    }
}