/*************************************************************************************************
 ** FILE:	RequestProcessorMSMQ.cs
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
using Discovery.RequestManagerClient;

namespace Discovery.RequestManagement
{
    /// <summary>
    /// A class to handle the MSMQ request processor
    /// </summary>
    public class RequestProcessorMSMQ : RequestManagement.RequestProcessor
    {
        private string queueName;
        private RequestManagerClientMSMQ requestManagerClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:RequestProcessorMSMQ"/> class.
        /// </summary>
        /// <param name="requestManager">The request manager.</param>
        public RequestProcessorMSMQ(RequestManager requestManager) : base(requestManager)
        {
            // Create the request manager client to receive messages from the queue
            requestManagerClient = new RequestManagerClientMSMQ();

            // Set the queue name as specified via the custom configuration section
            requestManagerClient.QueueName = requestManager.CustomSettings["QueueName"];
        }

        /// <summary>
        /// Gets the next request.
        /// </summary>
        /// <returns></returns>
        public override RequestMessage GetNextRequest()
        {
            // Get the next body from MSMQ
            return requestManagerClient.Receive();
        }
    }
}
