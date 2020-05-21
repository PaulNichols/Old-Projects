/*************************************************************************************************
 ** FILE:	LoadCategoryController.cs
 ** DATE:	30/05/2006
 ** AUTHOR:	Trung Lo
 **
 **
 ** OVERVIEW:
 **
 ** MODIFICATION HISTORY:
 **
 ** Date:		Version:	Who:	Change:
 ** 30/5/06		1.0		    TVL	    Initial Version
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
    /*************************************************************************************************
    ** CLASS:	LoadCategoryController
    **
    ** OVERVIEW:
    ** This controller class contains all methods related to LoadCategory including calling data access methods
    **
    ** MODIFICATION HISTORY:
    **
    ** Date:		 Version:    Who:	Change:
    ** 03-Oct-2006   1.0         TVL    Initial Version
    ************************************************************************************************/
    /// <summary>
    /// A class to provide the load category controller which is a business object controller
    /// with namespace Discovery.BusinessObjects.Controllers
    /// </summary>
    public static class LoadCategoryController
    {

        /// <summary>
        /// Gets the load category.
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <returns></returns>
        public static LoadCategory GetLoadCategory(int categoryId)
        {
            LoadCategory loadCategory = null;

            try
            {
                loadCategory = CBO<LoadCategory>.FillObject(DataAccessProvider.Instance().GetLoadCategory(categoryId));

                
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return loadCategory;
        }

        /// <summary>
        /// Gets the load category.
        /// </summary>
        /// <param name="categoryCode">The category code.</param>
        /// <returns></returns>
        public static LoadCategory GetLoadCategory(string categoryCode)
        {
            LoadCategory loadCategory = null;

            try
            {
                loadCategory = CBO<LoadCategory>.FillObject(DataAccessProvider.Instance().GetLoadCategory(categoryCode));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return loadCategory;
        }

        /// <summary>
        /// Gets the load categories.
        /// </summary>
        /// <returns></returns>
        public static List<LoadCategory> GetLoadCategories()
        {
            List<LoadCategory> loadCateroies = new List<LoadCategory>();

            try
            {
                loadCateroies =
                    CBO<LoadCategory>.FillCollection(
                        DataAccessProvider.Instance().GetLoadCategories());
               
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return loadCateroies;
        }

        /// <summary>
        /// Gets the load categories.
        /// </summary>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        public static List<LoadCategory> GetLoadCategories(string sortExpression)
        {
            List<LoadCategory> loadCategories = GetLoadCategories();

            if (string.IsNullOrEmpty(sortExpression))
            {
                sortExpression = "Code";
            }
            
            loadCategories.Sort(new UniversalComparer<LoadCategory>(sortExpression));
            return loadCategories;
        }

        /// <summary>
        /// Saves the load category.
        /// </summary>
        /// <param name="loadCategory">The load category.</param>
        /// <returns></returns>
        public static int SaveLoadCategory(LoadCategory loadCategory)
        {
            try
            {
                if (loadCategory.IsValid)
                {
                    // Save entity
                    loadCategory.Id = DataAccessProvider.Instance().SaveLoadCategory(loadCategory);
                }
                else
                {
                    // Entity is not valid
                    throw new InValidBusinessObjectException(loadCategory);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            // Done
            return loadCategory.Id;
        }

        /// <summary>
        /// Deletes the load category.
        /// </summary>
        /// <param name="loadCategory">The load category.</param>
        /// <returns></returns>
        public static bool DeleteLoadCategory(LoadCategory loadCategory)
        {
            bool success = false;

            try
            {
                if (loadCategory != null)
                {
                    success = DataAccessProvider.Instance().DeleteLoadCategory(loadCategory.Id);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return success;
        }
   }
}