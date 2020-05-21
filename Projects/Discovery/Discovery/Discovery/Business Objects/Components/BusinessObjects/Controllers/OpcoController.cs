/*************************************************************************************************
 ** FILE:	OpcoController.cs
 ** DATE:	30/05/2006
 ** AUTHOR:	Paul Nichols
 **
 **
 ** OVERVIEW:
 **
 ** MODIFICATION HISTORY:
 **
 ** Date:		Version:	Who:	Change:
 ** 30/5/06		1.0		    PJN	    Initial Version
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
   ** CLASS:	OpcoController
   **
   ** OVERVIEW:
   ** This controller class contains all methods related to OpCos including calling data access methods
   **
   ** MODIFICATION HISTORY:
   **
   ** Date:		Version:    Who:	Change:
   ** 19/7/06		1.0			PJN		Initial Version
   ************************************************************************************************/

    /// <summary>
    /// A class to provide the opco controller which is a business object controller
    /// with namespace Discovery.BusinessObjects.Controllers
    /// </summary>
    public static class OpcoController
    {

        /// <summary>
        /// Gets the op cos.
        /// </summary>
        /// <param name="fullyPopulate">if set to <c>true</c> [fullyPopulate].</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        public static List<OpCo> GetOpCos(bool fullyPopulate, string sortExpression)
        {
            List<OpCo> opCos = GetOpCos(fullyPopulate);
            if (string.IsNullOrEmpty(sortExpression))
            {
                sortExpression = "Code";
            }
            opCos.Sort(new UniversalComparer<OpCo>(sortExpression));
            return opCos;
        }

        /// <summary>
        /// Saves an OpCo object.
        /// </summary>
        /// <remarks>If the Id property =0 then an INSERT will Occur, otherwise an UPDATE will occur</remarks>
        /// <param name="opco">The opco.</param>
        /// <returns></returns>
        public static int SaveOpCo(OpCo opco)
        {
            try
            {
                if (opco.IsValid)
                {
                    opco.Id = DataAccessProvider.Instance().SaveOpCo(opco);
                }
                else
                {
                    // Entity is not valid
                    throw new InValidBusinessObjectException(opco);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            // Done
            return opco.Id;
        }

        /// <summary>
        /// Deletes a OpCo.
        /// </summary>
        /// <param name="opco">The opco to delete.</param>
        /// <returns>Success state</returns>
        public static bool DeleteOpCo(OpCo opco)
        {
            bool success = false;
            try
            {
                if (opco != null)
                {
                    success = DataAccessProvider.Instance().DeleteOpCo(opco.Id);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return success;
        }
        
        /// <summary>
        /// Gets a single OpCo for the specified Id.
        /// </summary>
        /// <param name="opcoId">The opco id.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fully populate].</param>
        /// <returns>An OpCo object</returns>
        public static OpCo GetOpCo(int opcoId, bool fullyPopulate)
        {
            OpCo opco = new OpCo();
            try
            {
                opco = CBO<OpCo>.FillObject(DataAccessProvider.Instance().GetOpCo(opcoId));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return opco;
        }

             
        /// <summary>
        /// Gets a single OpCo for the specified Code.
        /// </summary>
        /// <param name="opcoCode">The opco id.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fully populate].</param>
        /// <returns>An OpCo object</returns>
        public static OpCo GetOpCo(string opcoCode, bool fullyPopulate)
        {
            OpCo opco = new OpCo();
            try
            {
                opco = CBO<OpCo>.FillObject(DataAccessProvider.Instance().GetOpCo(opcoCode));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return opco;
        }

        /// <summary>
        /// Gets the all OpCos.
        /// </summary>
        /// <returns>Collection of OpCo Objects</returns>
        public static List<OpCo> GetOpCos()
        {
            // Don't fullt populate
            return GetOpCos(false);
        }

        /// <summary>
        /// Gets the all OpCos.
        /// </summary>
        /// <returns>Collection of OpCo Objects</returns>
        public static List<OpCo> GetOpCos(bool fullyPopulate)
        {
            List<OpCo> opcos = new List<OpCo>();
            try
            {
                opcos = CBO<OpCo>.FillCollection(DataAccessProvider.Instance().GetOpCos());
                           
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return opcos;
        }
    }
}