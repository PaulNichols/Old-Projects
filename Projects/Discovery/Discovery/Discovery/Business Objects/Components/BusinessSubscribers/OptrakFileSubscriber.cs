using System;
using System.Configuration;
using Discovery.BusinessObjects;
using Discovery.BusinessObjects.Controllers;
using Discovery.RequestManagement;
using Discovery.RequestManagerClient;

namespace Discovery.BusinessSubscribers
{
    /*************************************************************************************************
    ** CLASS:	OptrakFileSubscriber
    **
    ** OVERVIEW:
    ** The purpose of this class is check if all three optrak files have been recieved. If so update 
    ** each shipments status to Routed. As each file is recieved the recieved date fields on the Routing History
    ** table should be updated, the recieved status on that object is derieved from these three dates
    **
    ** MODIFICATION HISTORY:
    **
    ** Date:		Version:    Who:	Change:
    ** 8/8/06	    1.0			PJN		Initial Version
    ************************************************************************************************/

    /// <summary>
    /// A class 'OptrakDropSubscriber' with namespace 'Discovery.BusinessSubscribers'.
    /// </summary>
    public class OptrakFileSubscriber : RequestSubscriber
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:OptrakFileSubscriber"/> class.
        /// </summary>
        /// <param name="requestProcessor">The request processor.</param>
        public OptrakFileSubscriber(RequestProcessor requestProcessor)
            : base(requestProcessor)
        {

        }

        /// <summary>
        /// Processes the request.
        /// </summary>
        /// <param name="requestMessage">The request message.</param>
        public override void ProcessRequest(RequestMessage requestMessage)
        {
            if (RequestProcessor.RequestDictionary.ContainsKey("RoutingHistory"))
            {
                RoutingHistory routingHistory;
                routingHistory = RequestProcessor.RequestDictionary["RoutingHistory"] as RoutingHistory;

                if (routingHistory != null)
                {
                    //get the routing history record
                    if (RequestProcessor.RequestMessage.Name.Equals(ConfigurationManager.AppSettings["OptrakDropsFileName"].ToString(),StringComparison.InvariantCultureIgnoreCase))
                    {
                        routingHistory.DropFileReceivedDate = DateTime.Now;
                    }
                    else if (RequestProcessor.RequestMessage.Name.Equals(ConfigurationManager.AppSettings["OptrakTripPartFileName"].ToString(),StringComparison.InvariantCultureIgnoreCase))
                    {
                        routingHistory.TripPartFileReceivedDate = DateTime.Now;
                    }


                    //save the time we recieved the current file we're processing
                    int routingHistoryId = RoutingController.SaveRoutingHistory(routingHistory);
                    if (routingHistoryId != -1)
                    {
                        //if we saved sucessfully then see if we have all three files
                        if (routingHistory.Status == RoutingHistory.StatusEnum.Recieved)
                        {
                            // if we have all three files then update the status of shipments related to the routing history id

                            if (!RoutingController.SetShipmentsToRouted(routingHistoryId, GetType().FullName))
                            {
                                throw new Exception(
                                    string.Format(
                                        "Failed to update the status of all shipments related to the routing history id '{0}'",
                                        routingHistoryId));
                            }
                        }
                    }
                    else
                    {
                        throw new Exception(
                            string.Format(
                                "Failed to update the file recieved time of routing history  record with the id of '{0}'",
                                routingHistoryId));
                    }
                }

                Status = SubscriberStatusEnum.Processed;
            }
            else if (RequestProcessor.RequestMessage.Name ==
                       ConfigurationManager.AppSettings["OptrakDropsFileName"].ToString() ||RequestProcessor.RequestMessage.Name ==
                            ConfigurationManager.AppSettings["OptrakTripPartFileName"].ToString())
            {
                LastError =
                    new Exception(
                        "The Optrak file subscriber tried to set the shipment statuses to Routed but failed as it was unable to find a related Routing History record.");
                //we should have a RoutingHistory object in the cache so fail
                Status = SubscriberStatusEnum.Failed;
            }

        }
    }
}