/*************************************************************************************************
 ** FILE:	RequestManagerClient.cs
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

namespace Discovery.RequestManagerClient
{
    /// <summary>
    /// An abstract class for request manager client
    /// </summary>
    public abstract class RequestManagerClient
    {
        /// <summary>
        /// Sends the specified request message.
        /// </summary>
        /// <param name="requestMessage">The request message.</param>
        public abstract void Send(RequestMessage requestMessage);
        /// <summary>
        /// Receives this instance.
        /// </summary>
        /// <returns></returns>
        public abstract RequestMessage Receive();
    }
}
