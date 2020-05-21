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
   ** CLASS:	OpcoDivisionController
   **
   ** OVERVIEW:
   ** This controller class contains all methods related to OpCo Divisions including calling data access methods
   **
   ** MODIFICATION HISTORY:
   **
   ** Date:		Version:    Who:	Change:
   ** 19/7/06	1.0			PJN		Initial Version
   ************************************************************************************************/

    /// <summary>
    /// A class to provide the opco division controller which is a business object controller
    /// with namespace Discovery.BusinessObjects.Controllers
    /// </summary>
    public static class OpcoDivisionController
    {
        /// <summary>
        /// Gets the OpCo Divisions.
        /// </summary>
        /// <param name="fullyPopulate">if set to <c>true</c> [fullyPopulate].</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        public static List<OpCoDivision> GetOpCoDivisions(bool fullyPopulate, string sortExpression)
        {
            List<OpCoDivision> opCoDivisions = GetOpCoDivisions(fullyPopulate);
            if (string.IsNullOrEmpty(sortExpression))
            {
                sortExpression = "OpCo.Description,Code";
            }
            opCoDivisions.Sort(new UniversalComparer<OpCoDivision>(sortExpression));
            return opCoDivisions;
        }

        /// <summary>
        /// Saves an OpCo Division object.
        /// </summary>
        /// <remarks>If the Id property =0 then an INSERT will Occur, otherwise an UPDATE will occur</remarks>
        /// <param name="opcoDivision">The opco.</param>
        /// <returns></returns>
        public static int SaveOpCoDivision(OpCoDivision opcoDivision)
        {
            try
            {
                if (opcoDivision.IsValid)
                {
                    // Save entity
                    opcoDivision.Id = DataAccessProvider.Instance().SaveOpCoDivision(opcoDivision);
                    if (opcoDivision.Id != -1)
                    {
                        FrameworkController.GetChecksum(opcoDivision);
                        CacheManager.Add(opcoDivision, opcoDivision.OpCo != null);
                    }
                }
                else
                {
                    // Entity is not valid
                    throw new InValidBusinessObjectException(opcoDivision);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            // Done
            return opcoDivision.Id;
        }

        /// <summary>
        /// Deletes a OpCo Division.
        /// </summary>
        /// <param name="opcoDivision">The opco Division to delete.</param>
        /// <returns>Success state</returns>
        public static bool DeleteOpCoDivision(OpCoDivision opcoDivision)
        {
            bool success = false;
            try
            {
                if (opcoDivision != null)
                {
                    success = DataAccessProvider.Instance().DeleteOpCoDivision(opcoDivision.Id);
                    if (success)
                    {
                        CacheManager.Remove(opcoDivision);
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
        /// Gets the opco division.
        /// </summary>
        /// <param name="divisionCode">The division code.</param>
        /// <returns></returns>
        public static OpCoDivision GetOpCoDivision(string divisionCode)
        {
            OpCoDivision opcoDivision = null;
            try
            {
                opcoDivision = CacheManager.Get<OpCoDivision>(divisionCode);
                if (opcoDivision== null||opcoDivision.Logo == null)
                {
                    opcoDivision = CBO<OpCoDivision>.FillObject(DataAccessProvider.Instance().GetOpCoDivision(divisionCode));
                    if (opcoDivision != null)
                    {
                        CacheManager.Add(opcoDivision);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log and throw exception
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return opcoDivision;
        }

        /// <summary>
        /// Gets a single OpCo Division for the specified Id.
        /// </summary>
        /// <param name="opcoDivisionId">The opco division id.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fully populate].</param>
        /// <returns></returns>
        public static OpCoDivision GetOpCoDivision(int opcoDivisionId, bool fullyPopulate)
        {
            OpCoDivision opcoDivision = null;

            try
            {
                opcoDivision = CacheManager.Get<OpCoDivision>(opcoDivisionId, fullyPopulate);
                if (opcoDivision == null || opcoDivision.Logo == null)
                {
                    opcoDivision = CBO<OpCoDivision>.FillObject(DataAccessProvider.Instance().GetOpCoDivision(opcoDivisionId),
                        PopulateDivision,
                        fullyPopulate);
                    if (opcoDivision != null)
                    {
                        CacheManager.Add(opcoDivision);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log and throw exception
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return opcoDivision;
        }

        /// <summary>
        /// Fullies the populate.
        /// </summary>
        /// <param name="fullyPopulate">if set to <c>true</c> [fully populate].</param>
        /// <param name="dataAccessProvider">The data access provider.</param>
        /// <param name="opcoDivision">The opco division.</param>
        private static void PopulateDivision(OpCoDivision opcoDivision, IDataReader dataReader, bool fullyPopulate)
        {
            if (fullyPopulate && opcoDivision != null)
            {
                opcoDivision.OpCo = OpcoController.GetOpCo(opcoDivision.OpCoId, true);
            }
        }

        /// <summary>
        /// Gets the all opco divisions.
        /// </summary>
        /// <param name="opCoCode">The OpCo code to locate divisions for.</param>
        /// <returns>Collection of OpCo Objects</returns>
        public static List<OpCoDivision> GetOpCoDivisions(string opCoCode, bool fullyPopulate)
        {
            List<OpCoDivision> opcoDivisions = null;
            try
            {
                // Get the divisions by opco
                opcoDivisions = CBO<OpCoDivision>.FillCollection(DataAccessProvider.Instance().GetOpCoDivisions(opCoCode), 
                            PopulateDivision, 
                            fullyPopulate);

                // Add the divisions to the cache
                if (opcoDivisions != null)
                {
                    foreach (OpCoDivision division in opcoDivisions)
                    {
                        CacheManager.Add(division, fullyPopulate);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return opcoDivisions;
        }

        /// <summary>
        /// Gets the all opco divisions.
        /// </summary>
        /// <param name="fullyPopulate">if set to <c>true</c> [fully populate].</param>
        /// <returns>Collection of OpCo Objects</returns>
        public static List<OpCoDivision> GetOpCoDivisions(bool fullyPopulate)
        {
            List<OpCoDivision> opcoDivisions = null;
            try
            {
                opcoDivisions = CBO<OpCoDivision>.FillCollection(DataAccessProvider.Instance().GetOpCoDivisions(),
                        PopulateDivision,
                        fullyPopulate);
                if (opcoDivisions != null) foreach (OpCoDivision division in opcoDivisions)
                    {
                        CacheManager.Add(division, fullyPopulate);
                    }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return opcoDivisions;
        }
    }
}