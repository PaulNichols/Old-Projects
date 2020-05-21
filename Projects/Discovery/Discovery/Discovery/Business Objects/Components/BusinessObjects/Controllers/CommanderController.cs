/*************************************************************************************************
 ** FILE:	CommanderController.cs
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
using System.Data;
using System.Diagnostics;
using System.Transactions;
using Discovery.ComponentServices.DataAccess;
using Discovery.ComponentServices.ExceptionHandling;
using Discovery.RequestManagerClient;
using Discovery.Utility.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Discovery.BusinessObjects.Controllers
{
    /// <summary>
    /// A class to provide commander controller which is a business object controller to manipulate the sales order lines
    /// with namespace Discovery.BusinessObjects.Controllers
    /// </summary>
    public static class CommanderController
    {
        /// <summary>
        /// This method will iterate through the collection of CommanderSalesOrder objects supplied calling their GenerateLinesCSV() methods. The return value from these method calls will be separated by an end of line character and outputted as a body to be routed to Commander by the Integration layer.
        /// </summary>
        /// <param name="salesOrders">The sales orders.</param>
        /// <returns>Success</returns>
        public static bool GenerateSalesOrderLines(List<CommanderSalesOrder> salesOrders)
        {
            List<string> generatedLines = new List<string>();
            try
            {
                foreach (CommanderSalesOrder salesOrder in salesOrders)
                {
                    generatedLines.AddRange(salesOrder.GenerateLinesCSV());
                }

                //filename?
                string messageBody = String.Join(Environment.NewLine, generatedLines.ToArray());
                //put on queue
                //RequestManagerClient requestManagerClient = new RequestManagerClient();
                //requestManagerClient.Send("", -1, "", messageBody);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return false;
        }

        /// <summary>
        /// This method will iterate through the collection CommanderSalesOrder objects supplied calling their GenerateCSV() methods. 
        /// The return value from these method calls will be separated by an end of line character and outputted as a body to be routed to Commander by the Integration layer.
        /// </summary>
        /// <param name="salesOrders">The sales orders.</param>
        /// <param name="newLineCharacter">A string which represents the new line characters(s) to append to the end of each line.</param>
        /// <returns>Success</returns>
        public static bool GenerateSalesOrders(List<CommanderSalesOrder> salesOrders, string newLineCharacter)
        {
            List<string> generatedLines = new List<string>();
            try
            {
                foreach (CommanderSalesOrder salesOrder in salesOrders)
                {
                    generatedLines.Add(salesOrder.GenerateCSV());
                }

                //filename?
                string messageBody = String.Join(newLineCharacter, generatedLines.ToArray());
                //commander out queue
                string queueName = "";
                
#if DEBUG
                Logger.Write(new Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry(
                         "SendMSMQMessageToDestination.",
                          "Integration Trace",
                          0,
                          0,
                          TraceEventType.Information,
                          null,
                          null));
#endif
                RequestManagerClientMSMQ requestManagerClient = new RequestManagerClientMSMQ();

                requestManagerClient.QueueName = queueName;

                RequestMessage requestMessage = new RequestMessage();
                //requestMessage.DestinationSystem = task.DestinationConnectionIdentifier;
                //requestMessage.SourceSystem = task.SourceConnectionIdentifier;
                //requestMessage.Type = task.SourceConnection.ConnectionType.ToString();


                //requestMessage.Body = messageBody;
                //requestMessage.Name = fileNameToDownLoad;
                //requestMessage.Sequence = task.SequenceNumber;

#if DEBUG
                Logger.Write(new Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry(
                         string.Format("About to send message to queue '{0}.", requestManagerClient.QueueName),
                          "Integration Trace",
                          0,
                          0,
                          TraceEventType.Information,
                          null,
                          null));
#endif
                requestManagerClient.Send(requestMessage);

#if DEBUG
                Logger.Write(new Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry(
                         string.Format("Sent message to queue '{0}'. ", requestManagerClient.QueueName),
                          "Integration Trace",
                          0,
                          0,
                          TraceEventType.Information,
                          null,
                          null));
#endif
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return false;
        }

        /// <summary>
        /// This method is to be used to create CommanderSalesOrder object from a TDCShipment object. The indented use of this method will be for HSP orders but could be extended to generate for other Opcos
        /// </summary>
        /// <param name="shipmentsToGenerateFrom">The shipments to generate from.</param>
        /// <returns>Success</returns>
        public static List<CommanderSalesOrder> GenerateCommanderSalesOrders(List<TDCShipment> shipmentsToGenerateFrom)
        {
            List<CommanderSalesOrder> generatedSalesOrders = new List<CommanderSalesOrder>();
            try
            {
                foreach (TDCShipment shipment in shipmentsToGenerateFrom)
                {
                    generatedSalesOrders.Add(shipment.GenerateCommanderSalesOrder());
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return generatedSalesOrders;
        }

        /// <summary>
        /// Saves the specified CommanderSalesOrder to the underlying data store via the configured DataProvider.
        /// If a duplicate Commander Sales Order occurs then an exception will be raised.        /// </summary>
        /// <param name="commanderOrder">The commander order.</param>
        /// <param name="saveLines">if set to <c>true</c> [save lines].</param>
        /// <returns></returns>
        public static int SaveSalesOrder(CommanderSalesOrder commanderOrder, bool saveLines)
        {

            int returnValue = -1;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    returnValue = SaveSalesOrder(commanderOrder);
                    if (returnValue != -1)
                    {
                        foreach (CommanderSalesOrderLine line in commanderOrder.Lines)
                        {
                            line.CommanderSalesOrderId = returnValue;
                        }
                        SaveSalesOrderLines(commanderOrder.Lines);
                    }
                    if (Transaction.Current != null &&
                                       Transaction.Current.TransactionInformation.Status == TransactionStatus.Active)
                    {
                        scope.Complete();
                    }
                }
                return returnValue;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            return returnValue;
        }

        /// <summary>
        /// Saves the specified CommanderSalesOrder to the underlying data store via the configured DataProvider.
        /// If a duplicate Commander Sales Order occurs then an exception will be raised.
        /// </summary>
        /// <param name="commanderOrder">The commander order.</param>
        /// <returns></returns>
        public static int SaveSalesOrder(CommanderSalesOrder commanderOrder)
        {
            try
            {
                if (commanderOrder.IsValid)
                {
                    // Save entity
                    commanderOrder.Id = DataAccessProvider.Instance().SaveCommanderSalesOrder(commanderOrder);
                }
                else
                {
                    // Entity is not valid
                    throw new InValidBusinessObjectException(commanderOrder);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            return commanderOrder.Id;
        }

        /// <summary>
        /// Saves the specified CommanderSalesOrder to the underlying data store via the configured DataProvider.
        ///If a corresponding Commander order does not exist then an error will occur.
        /// </summary>
        /// <param name="commanderOrderLines">The commander order lines.</param>
        /// <returns></returns>
        public static List<int> SaveSalesOrderLines(List<CommanderSalesOrderLine> commanderOrderLines)
        {
            List<int> returnValues = new List<int>();
            try
            {
                foreach (CommanderSalesOrderLine line in commanderOrderLines)
                {
                    if (line.IsValid)
                    {
                        // Save entity
                    }
                    else
                    {
                        // Entity is not valid
                        throw new InValidBusinessObjectException(line);
                    }
                }
                returnValues = DataAccessProvider.Instance().SaveCommanderSalesOrderLines(commanderOrderLines);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            return returnValues;
        }

        /// <summary>
        /// Retrieves a single CommanderSalesOrder from the underlying data store via the configured DataProvider for the supplied CommanderSalesOrder ID.
        /// An instance of an CommanderSalesOrder is returned to the caller or Null if no CommanderSalesOrder record is found.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <param name="salesOrderId">The sales order id.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fully populate].</param>
        /// <returns></returns>
        public static CommanderSalesOrder GetSalesOrder(int salesOrderId, bool fullyPopulate)
        {
            CommanderSalesOrder salesOrder = null;
            try
            {

                salesOrder =
                    CBO<CommanderSalesOrder>.FillObject(
                            DataAccessProvider.Instance().GetCommanderSalesOrder(salesOrderId),
                            CustomFill,
                            fullyPopulate);

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return salesOrder;
        }

        /// <summary>
        /// Retrieves a list of all CommanderSalesOrderLines from the underlying data store via the configured DataProvider relating to the CommanderSalesOrder with the specified ID.
        /// A strongly typed list of CommanderSalesOrderLines is returned to the caller or an empty list if no records were found.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <param name="salesOrderId">The sales order id.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fully populate].</param>
        /// <returns></returns>
        public static List<CommanderSalesOrderLine> GetSalesOrderLines(int salesOrderId, bool fullyPopulate)
        {
            List<CommanderSalesOrderLine> lines = null;
            try
            {
                lines =
                         CBO<CommanderSalesOrderLine>.FillCollection(
                             DataAccessProvider.Instance().GetCommanderSalesOrderLines(salesOrderId));
                return lines;

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return lines;
        }

        /// <summary>
        /// Retrieves a single CommanderSalesOrder from the underlying data store via the configured DataProvider for the supplied order number.
        ///An instance of an CommanderSalesOrder is returned to the caller or Null if no CommanderSalesOrder record is found.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <param name="orderReference">The order reference.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fully populate].</param>
        /// <returns></returns>
        public static CommanderSalesOrder GetSalesOrder(string orderReference, bool fullyPopulate)
        {
            CommanderSalesOrder salesOrder = null;
            try
            {

                salesOrder =
                    CBO<CommanderSalesOrder>.FillObject(
                            DataAccessProvider.Instance().GetCommanderSalesOrderByOrderNumber(orderReference),
                            CustomFill,
                            fullyPopulate);

                return salesOrder;

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return salesOrder;
        }

        /// <summary>
        /// Retrieves a list of all CommanderSalesOrder from the underlying data store via the configured DataProvider.
        /// A strongly typed list of CommanderSalesOrder is returned to the caller or an empty list if no records were found.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <returns></returns>
        public static List<CommanderSalesOrder> GetSalesOrders(bool fullyPopulate)
        {
            List<CommanderSalesOrder> commanderSalesOrders = null;
            try
            {

                commanderSalesOrders =
                    CBO<CommanderSalesOrder>.FillCollection(
                            DataAccessProvider.Instance().GetCommanderSalesOrders(),
                            CustomFill,
                            fullyPopulate);

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return commanderSalesOrders;
        }

        private static void CustomFill(CommanderSalesOrder item, IDataReader dataReader, bool fullyPopulate)
        {
            if (fullyPopulate)
            {
                List<CommanderSalesOrderLine> lines = GetSalesOrderLines(item.Id, true);
                foreach (CommanderSalesOrderLine line in lines)
                {
                    item.Lines.Add(line);
                }
            }

            item.DeliveryAddress = new Address();
            item.DeliveryAddress.Line1 = dataReader["DeliveryAddress1"].ToString();
            item.DeliveryAddress.Line2 = dataReader["DeliveryAddress2"].ToString();
            item.DeliveryAddress.Line3 = dataReader["DeliveryAddress3"].ToString();
            item.DeliveryAddress.Line4 = dataReader["DeliveryAddress4"].ToString();
            item.DeliveryAddress.Line5 = dataReader["DeliveryAddress5"].ToString();
        }

        /// <summary>
        /// Reconsiliations this instance.
        /// </summary>
        public static void Reconsiliation()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the sales order.
        /// </summary>
        /// <param name="Id">The id.</param>
        /// <returns></returns>
        public static bool DeleteSalesOrder(int Id)
        {
            bool returnValue = false;
            try
            {
                returnValue = DataAccessProvider.Instance().DeleteCommanderSalesOrder(Id);

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return returnValue;
        }

        /// <summary>
        /// Deletes the sales order line.
        /// </summary>
        /// <param name="Id">The id.</param>
        /// <returns></returns>
        public static bool DeleteSalesOrderLine(int Id)
        {
            bool returnValue = false;
            try
            {
                returnValue = DataAccessProvider.Instance().DeleteCommanderSalesOrderLine(Id);

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return returnValue;
        }

        /// <summary>
        /// Gets the sales order line.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static CommanderSalesOrderLine GetSalesOrderLine(int id)
        {
            CommanderSalesOrderLine salesOrderLine = null;
            try
            {
                salesOrderLine =
                         CBO<CommanderSalesOrderLine>.FillObject(DataAccessProvider.Instance().GetCommanderSalesOrderLine(id));

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return salesOrderLine;
        }

        /// <summary>
        /// Saves the sales order line.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <returns></returns>
        public static int SaveSalesOrderLine(CommanderSalesOrderLine line)
        {
            try
            {
                if (line.IsValid)
                {
                    // Save entity
                    line.Id = DataAccessProvider.Instance().SaveCommanderSalesOrderLine(line);
                }
                else
                {
                    // Entity is not valid
                    throw new InValidBusinessObjectException(line);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            // Done
            return line.Id;
        }

        /// <summary>
        /// Gets the product.
        /// </summary>
        /// <param name="productCode">The product code.</param>
        /// <returns></returns>
        public static CommanderProduct GetProduct(string productCode)
        {
            CommanderProduct commanderProduct = null;
            try
            {
                commanderProduct =
                         CBO<CommanderProduct>.FillObject(DataAccessProvider.Instance().GetCommanderProduct(productCode));

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return commanderProduct;
        }

        /// <summary>
        /// Saves the product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        public static int SaveProduct(CommanderProduct product)
        {
            try
            {
                if (product.IsValid)
                {
                    // Save entity
                    product.Id = DataAccessProvider.Instance().SaveCommanderProduct(product);
                }
                else
                {
                    // Entity is not valid
                    throw new InValidBusinessObjectException(product);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }

            // Done
            return product.Id;
        }
    }
}