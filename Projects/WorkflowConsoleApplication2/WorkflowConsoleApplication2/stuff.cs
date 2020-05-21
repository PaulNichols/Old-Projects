using System;
using System.Collections.Generic;
using System.Text;

namespace DeliveryOrder
{
    using System;
    using System.Workflow.Activities;

        [ExternalDataExchange]
        public interface IOrderService
        {
            event EventHandler<OrderEventArgs> OrderOnMessageQueue;
            event EventHandler<OrderEventArgs> MessageValidated;
            event EventHandler<OrderEventArgs> OrderProcessed;
            event EventHandler<OrderEventArgs> OrderShipped;
            event EventHandler<OrderEventArgs> OrderUpdated;
        }

}
