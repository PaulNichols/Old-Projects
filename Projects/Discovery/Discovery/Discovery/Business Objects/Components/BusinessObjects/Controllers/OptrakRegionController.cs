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
    ** CLASS:	OptrakRegionController
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
    /// A Class 'OptrakRegionController' which is a business object controller
    /// </summary>
    public static class OptrakRegionController
    {

        /// <summary>
        /// Gets the regions.
        /// </summary>
        /// <returns></returns>
        public static List<OptrakRegion> GetRegions()
        {
            List<OptrakRegion> regions = new List<OptrakRegion>();
            try
            {
                regions = CBO<OptrakRegion>.FillCollection(DataAccessProvider.Instance().GetRegions());
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            if (regions != null) foreach (OptrakRegion region in regions)
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
        public static List<OptrakRegion> GetRegions(string sortExpression)
        {
            List<OptrakRegion> regions = GetRegions();

            if (string.IsNullOrEmpty(sortExpression))
            {
                sortExpression = "Code";
            }
            regions.Sort(new UniversalComparer<OptrakRegion>(sortExpression));

            return regions;
        }

        /// <summary>
        /// Saves the optrakRegion.
        /// </summary>
        /// <param name="optrakRegion">The optrakRegion.</param>
        /// <returns></returns>
        public static int SaveRegion(OptrakRegion optrakRegion)
        {
            try
            {
                if (optrakRegion.IsValid)
                {
                    // Save entity
                    optrakRegion.Id = DataAccessProvider.Instance().SaveRegion(optrakRegion);
                    if (optrakRegion.Id > -1)
                    {
                        FrameworkController.GetChecksum(optrakRegion, "Region");
                        CacheManager.Add(optrakRegion);
                    }
                }
                else
                {
                    // Entity is not valid
                    throw new InValidBusinessObjectException(optrakRegion);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            // Done
            return optrakRegion.Id;
        }

        /// <summary>
        /// Deletes the optrakRegion.
        /// </summary>
        /// <param name="optrakRegion">The optrakRegion to delete.</param>
        /// <returns></returns>
        public static bool DeleteRegion(OptrakRegion optrakRegion)
        {
            bool success = false;
            try
            {
                if (optrakRegion != null)
                {
                    success = DataAccessProvider.Instance().DeleteRegion(optrakRegion.Id);
                    if (success) CacheManager.Remove(optrakRegion);
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
        public static OptrakRegion GetRegion(int regionId)
        {
            OptrakRegion optrakRegion = null;
            try
            {
                optrakRegion = CacheManager.Get<OptrakRegion>(regionId);
                if (optrakRegion == null)
                {
                    optrakRegion = CBO<OptrakRegion>.FillObject(DataAccessProvider.Instance().GetRegion(regionId));
                    if (optrakRegion != null) CacheManager.Add(optrakRegion);
                }

                return optrakRegion;

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return optrakRegion;
        }


        /// <summary>
        /// Gets the region by code.
        /// </summary>
        /// <param name="regionCode">The region code.</param>
        /// <returns></returns>
        public static OptrakRegion GetRegion(string regionCode)
        {
            OptrakRegion optrakRegion = null;

            try
            {

                optrakRegion = CacheManager.Get<OptrakRegion>(regionCode);
                if (optrakRegion == null)
                {
                    optrakRegion = CBO<OptrakRegion>.FillObject(DataAccessProvider.Instance().GetRegion(regionCode));
                    if (optrakRegion != null)
                    {
                        CacheManager.Add(optrakRegion);
                    }
                }

                return optrakRegion;

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            return optrakRegion;
        }


    }
}