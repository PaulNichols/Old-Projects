/*************************************************************************************************
 ** FILE:	ShipmentCommanderMessage.cs
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
    class ShipmentCommanderMessage : RequestManagement.RequestSubscriber
    {
        private Discovery.BusinessObjects.TDCShipment tdcShipment;
        private Discovery.BusinessObjects.CommanderSalesOrder commanderSalesOrder;

        public ShipmentCommanderMessage(RequestProcessor requestProcessor)
            : base(requestProcessor)
        {
            throw new System.NotImplementedException();
        }

        public override void ProcessRequest(RequestMessage requestMessage)
        {
        }
    }
}
