/*************************************************************************************************
 ** FILE:	Framework.cs
 ** DATE:	30/05/2006
 ** AUTHOR:	Lee Spring
 **
 **
 ** OVERVIEW:
 **
 ** MODIFICATION HISTORY:
 **
 ** Date:		Version:	Who:	Change:
 ** 30/5/06		1.0		    LAS	    Initial Version
 ************************************************************************************************/
using System;
using System.Collections.Generic;
using Discovery.ComponentServices.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Discovery.BusinessObjects.Controllers
{
    /// <summary>
    /// A class to provide the frame work controller which is a business object controller
    /// with namespace Discovery.BusinessObjects.Controllers
    /// </summary>
    public static class FrameworkController
    {
        /// <summary>
        /// Gets the checksum.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void GetChecksum(PersistableBusinessObject entity)
        {
            GetChecksum(entity, entity.GetType().Name.Replace(entity.GetType().Namespace, ""));
        }

        /// <summary>
        /// Gets the checksum.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="tableName">Name of the table.</param>
        public static void GetChecksum(PersistableBusinessObject entity, string tableName)
        {
            entity.CheckSum = DataAccessProvider.Instance().GetEntityChecksum(entity.Id, tableName);
        }
    }
}