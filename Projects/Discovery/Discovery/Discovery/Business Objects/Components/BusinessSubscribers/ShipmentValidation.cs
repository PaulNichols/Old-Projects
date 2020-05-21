/*************************************************************************************************
 ** FILE:	ShipmentValidation.cs
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
using System.Text;
using Discovery.RequestManagement;
using Discovery.RequestManagerClient;

namespace Discovery.Components.BusinessSubscribers
{
    /// <summary>
    /// A class 'ShipmentValidation' with namespace 'Discovery.Components.BusinessSubscribers'.
    /// It is inherited from Discovery.RequestManagement.RequestSubscriber
    /// </summary>

    public class ShipmentValidation : Discovery.RequestManagement.RequestSubscriber
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ShipmentValidation"/> class.
        /// </summary>
        /// <param name="requestProcessor">The request processor.</param>
        public ShipmentValidation(RequestProcessor requestProcessor)
            : base(requestProcessor)
        {
        }


        /// <summary>
        /// Processes the request.
        /// </summary>
        /// <param name="requestMessage">The request message.</param>
        public override void ProcessRequest(RequestMessage requestMessage)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
