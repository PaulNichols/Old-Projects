using System;
using System.Collections.Generic;
using System.Text;
using Discovery.RequestManagement;
using Discovery.RequestManagerClient;

namespace Discovery.RequestSubscribers
{
    /// <summary>
    /// A Class 'Routing' with namespace 'Discovery.RequestSubscribers'.
    /// It is inherited from RequestManagement.RequestSubscriber
    /// </summary>
    public class Routing : RequestManagement.RequestSubscriber
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Routing"/> class.
        /// </summary>
        /// <param name="requestProcessor">The request processor.</param>
        public Routing(RequestProcessor requestProcessor)
            : base(requestProcessor)
        {
        }

        /// <summary>
        /// Processes the request.
        /// </summary>
        /// <param name="requestMessage">The request message.</param>
        public override void ProcessRequest(RequestMessage requestMessage)
        {
            try
            {
                throw new Exception("ExceptionMessage exception in routing subscriber.");
            }
            catch (Exception ex)
            {
                // Store the exception
                LastError = ex;

                // Failed
                Status = SubscriberStatusEnum.Failed;
            }
        }
    }
}
