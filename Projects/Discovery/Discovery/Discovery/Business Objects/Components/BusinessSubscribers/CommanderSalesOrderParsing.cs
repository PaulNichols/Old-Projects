using System;
using System.Collections.Generic;
using Discovery.BusinessObjects;
using Discovery.BusinessObjects.Controllers;
using Discovery.ComponentServices.Parsing;
using Discovery.RequestManagement;
using Discovery.RequestManagerClient;
using Discovery.Utility;

namespace Discovery.BusinessSubscribers
{
    /// <summary>
    /// A class to parse commander sales order
    /// </summary>
    public class CommanderSalesOrderParsing : CommanderParsing
    {
        private List<CommanderSalesOrder> salesOrders = new List<CommanderSalesOrder>();

        /// <summary>
        /// Initializes a new instance of the <see cref="T:CommanderSalesOrderParsing"/> class.
        /// </summary>
        /// <param name="requestProcessor">The request processor.</param>
        public CommanderSalesOrderParsing(RequestProcessor requestProcessor)
            : base(requestProcessor)
        {
        }

        /// <summary>
        /// Processes the request.
        /// </summary>
        /// <param name="requestMessage">The request message.</param>
        /// <returns></returns>
        public override void ProcessRequest(RequestMessage requestMessage)
        {
            try
            {
                CommanderSalesOrder.SetCommanderSalesOrderFileHeaderSchema(textFieldParser.TextFields);

                base.ProcessRequest(requestMessage);
                Status = SaveCommanderOrders(requestMessage);
            }
            catch (Exception ex)
            {
                // Store the exception
                LastError = ex;

                // Failed
                Status = SubscriberStatusEnum.Failed;
            }
        }

        /// <summary>
        /// Texts the parser_ record read.
        /// </summary>
        /// <param name="CurrentLineNumber">The current line number.</param>
        /// <param name="lineText">The line text.</param>
        protected override void textFieldParser_RecordRead(int CurrentLineNumber, string lineText)
        {
            string type;
            string subType;
            GetRowTypeAndSubType(lineText, out type, out subType);

            //check the type and subtype so we know what schema to set the parser to and which handlers to setup
            if (type == Customer_Order && subType == "A")
            {
                CommanderSalesOrder.SetCommanderSalesOrderHeaderSchema(textFieldParser.TextFields);
                AddSalesOrderHandlers();
            }
            else if (type == Order_Item && subType == "A")
            {
                CommanderSalesOrderLine.SetCommanderSalesOrderLinesSchema(textFieldParser.TextFields);
                AddSalesOrderLineHandlers();
            }
        }

        /// <summary>
        /// Saves the commander orders.
        /// </summary>
        /// <param name="requestMessage">The request message.</param>
        /// <returns></returns>
        private SubscriberStatusEnum SaveCommanderOrders(RequestMessage requestMessage)
        {
            foreach (CommanderSalesOrder salesOrder in salesOrders)
            {
                //check to see if a commander order already exists for this order reference
                CommanderSalesOrder previousRecord = CommanderController.GetSalesOrder(salesOrder.OrderReference, false);
                if (previousRecord != null)
                {
                    //check to see if the related shipment exists and if it has been sent to the warehouse yet
                    TDCShipment relatedShipment =
                        TDCShipmentController.GetShipment(requestMessage.SourceSystem, previousRecord.OrderReference, "NA");

                    if (relatedShipment != null)
                    {
                        if (Null.NullDate != relatedShipment.SentToWMS)
                        {
                            ////log
                            //LogEntry logEntry=new  LogEntry();
                            //logEntry.Message = "blah";
                            //Logger.Write(logEntry);
                            // throw new Exception("This message has already been sent to the warehouse.");
                            return SubscriberStatusEnum.Processed;
                        }
                    }

                    salesOrder.Id = previousRecord.Id;
                    foreach (CommanderSalesOrderLine commanderSalesOrderLine in salesOrder.Lines)
                    {
                        commanderSalesOrderLine.CommanderSalesOrderId = previousRecord.Id;
                    }
                }

                //save salesOrder and lines
                try
                {
                    int savedId = CommanderController.SaveSalesOrder(salesOrder, true);
                }

                catch (InValidBusinessObjectException ex)
                {
                    //log exception
                }
            }

            return SubscriberStatusEnum.Processed;
        }

        /// <summary>
        /// Adds the sales order handlers.
        /// </summary>
        private void AddSalesOrderHandlers()
        {
            textFieldParser.RecordFound -= new TextFieldParser.RecordFoundHandler(CommanderSalesOrderLine_RecordFound);
            textFieldParser.RecordFound += new TextFieldParser.RecordFoundHandler(CommanderSalesOrderHeader_RecordFound);
        }

        /// <summary>
        /// Adds the sales order line handlers.
        /// </summary>
        private void AddSalesOrderLineHandlers()
        {
            textFieldParser.RecordFound -= new TextFieldParser.RecordFoundHandler(CommanderSalesOrderHeader_RecordFound);
            textFieldParser.RecordFound -= new TextFieldParser.RecordFoundHandler(CommanderSalesOrderLine_RecordFound);
            textFieldParser.RecordFound += new TextFieldParser.RecordFoundHandler(CommanderSalesOrderLine_RecordFound);
        }


        /// <summary>
        /// Commanders the sales order header_ record found.
        /// </summary>
        /// <param name="currentLineNumber">The current line number.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="LineText">The line text.</param>
        private void CommanderSalesOrderHeader_RecordFound(ref int currentLineNumber, TextFieldCollection fields, string LineText)
        {
            //found the header, so populates the object
            PopulateCommanderSalesOrder(fields);
        }


        /// <summary>
        /// Commanders the sales order line record found event handler.
        /// </summary>
        /// <param name="currentLineNumber">The current line number.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="LineText">The line text.</param>
        private void CommanderSalesOrderLine_RecordFound(ref int currentLineNumber, TextFieldCollection fields, string LineText)
        {
            //found a line, so populates the object and add to the collection of lines
            salesOrders[salesOrders.Count - 1].Lines.Add(PopulateCommanderSalesOrderLine(fields));
        }


        /// <summary>
        /// Populates the commander sales order from the item values held in the TextFieldCollection..
        /// </summary>
        /// <param name="fields">The fields.</param>
        /// <returns></returns>
        private CommanderSalesOrder PopulateCommanderSalesOrder(TextFieldCollection fields)
        {
            CommanderSalesOrder salesOrder = new CommanderSalesOrder();
            salesOrders.Add(salesOrder);
            salesOrder.DeliveryAddress = new Address();

            PopulateProperties(fields, salesOrder);

            //set the updated by field to the name of this class 
            salesOrder.UpdatedBy = GetType().ToString();

            return salesOrder;
        }

        /// <summary>
        /// Populates a commander sales order line object from the item values held in the TextFieldCollection.
        /// </summary>
        /// <param name="fields">The fields.</param>
        /// <returns></returns>
        private CommanderSalesOrderLine PopulateCommanderSalesOrderLine(TextFieldCollection fields)
        {
            CommanderSalesOrderLine newLine = new CommanderSalesOrderLine();

            PopulateProperties(fields, newLine);
            newLine.UpdatedBy = GetType().ToString();

            return newLine;
        }
    }
}