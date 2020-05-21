/*************************************************************************************************
 ** FILE:	RequestManagerClientMSMQ.cs
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
using System.Messaging;
using System.Transactions;

namespace Discovery.RequestManagerClient
{
    /// <summary>
    /// A client MSMQ request manager
    /// </summary>
    public class RequestManagerClientMSMQ : RequestManagerClient
    {
        private string queueName;
        private MessageQueue messageQueue;
        private int receiveTimeOutSeconds;

        /// <summary>
        /// Gets or sets the receive time out seconds.
        /// </summary>
        /// <value>The receive time out seconds.</value>
        public int ReceiveTimeOutSeconds
        {
            get { return receiveTimeOutSeconds; }
            set { receiveTimeOutSeconds = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:RequestManagerClientMSMQ"/> class.
        /// </summary>
        public RequestManagerClientMSMQ()
        {
            // Seed values
            queueName = "";
            receiveTimeOutSeconds = 1;
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="T:Discovery.RequestManagerClient.RequestManagerClientMSMQ"/> is reclaimed by garbage collection.
        /// </summary>
        ~RequestManagerClientMSMQ()
        {
            // Close the queue, etc
            if (MessageQueue != null)
            {
                MessageQueue.Close();
                MessageQueue.Dispose();
            }
        }

        /// <summary>
        /// Gets or sets the name of the queue.
        /// </summary>
        /// <value>The name of the queue.</value>
        public string QueueName
        {
            get
            {
                return queueName;
            }

            set
            {
                // Check that the queue name has been specified
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Queue name cannot be blank.");
                }

                // Store the queue name
                queueName = value;

                //// Update the path in the queue
                //MessageQueue.Path = queueName;

                if (!MessageQueue.Exists(queueName))
                {
                    throw new Exception(string.Format("MSMQ '{0}' does not exist.", queueName));
                }

                // Create the queue
                MessageQueue = new MessageQueue(
                            queueName,                          // path
                            false,                              // sharedModeDenyReceive
                            false,                              // enableCache
                            QueueAccessMode.SendAndReceive);    // accessMode

                // Specify the target type
                ((XmlMessageFormatter)MessageQueue.Formatter).TargetTypes = new Type[] { typeof(RequestMessage) };
            }
        }

        /// <summary>
        /// Gets or sets the message queue.
        /// </summary>
        /// <value>The message queue.</value>
        public MessageQueue MessageQueue
        {
            get { return messageQueue; }
            set { messageQueue = value; }
        }

        /// <summary>
        /// Sends the specified request message.
        /// </summary>
        /// <param name="requestMessage">The request message.</param>
        public override void Send(RequestMessage requestMessage)
        {
            // Check that the queue name has been specified
            if (string.IsNullOrEmpty(queueName))
            {
                throw new ArgumentException("Queue name must be specified before calling Send().");
            }

            // Send the message
            MessageQueue.Send(
                       requestMessage,
                       requestMessage.Label,
                       (null == Transaction.Current) ? MessageQueueTransactionType.Single : MessageQueueTransactionType.Automatic);
        }

        /// <summary>
        /// Receives this instance.
        /// </summary>
        /// <returns></returns>
        public override RequestMessage Receive()
        {
            // Check that the queue name has been specified
            if (string.IsNullOrEmpty(queueName))
            {
                throw new ArgumentException("Queue name must be specified before calling Receive().");
            }

            // The request message
            RequestMessage requestMessage = null;

            try
            {
                // Get the next request from the queue, if there's a transaction automatically enlist in it
                requestMessage = (RequestMessage)MessageQueue.Receive(
                            new TimeSpan(0, 0, receiveTimeOutSeconds),
                            ((null == Transaction.Current) ? MessageQueueTransactionType.Single : MessageQueueTransactionType.Automatic)).Body;
            }
            catch (MessageQueueException ex)
            {
                // Check if it was a timeout, if so just return no message
                if (ex.MessageQueueErrorCode == MessageQueueErrorCode.IOTimeout)
                {
                    // Consume the exception
                    requestMessage = null;
                }
                else
                {
                    // Re throw the exception
                    throw ex;
                }
            }

            // Return the request message if we have one
            return requestMessage;
        }

        /// <summary>
        /// Receives the by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public RequestMessage ReceiveById(string id)
        {
            // Check that the queue name has been specified
            if (string.IsNullOrEmpty(queueName))
            {
                throw new ArgumentException("Queue name must be specified before calling Receive().");
            }

            // The request message
            RequestMessage requestMessage = null;

            try
            {
                // Get the next request from the queue
                requestMessage = (RequestMessage)MessageQueue.ReceiveById(id,
                                                                        (null == Transaction.Current) ? MessageQueueTransactionType.Single : MessageQueueTransactionType.Automatic).Body;
            }
            catch (MessageQueueException ex)
            {
                // Check if it was a timeout, if so just return no message
                if (ex.MessageQueueErrorCode == MessageQueueErrorCode.IOTimeout)
                {
                    // Consume the exception
                    requestMessage = null;
                }
                else
                {
                    // Re throw the exception
                    throw ex;
                }
            }

            // Return the request message if we have one
            return requestMessage;
        }
    }
}
