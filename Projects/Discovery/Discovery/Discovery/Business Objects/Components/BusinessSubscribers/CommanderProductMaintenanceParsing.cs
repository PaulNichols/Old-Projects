using System;
using System.Collections.Generic;
using Discovery.BusinessObjects;
using Discovery.BusinessObjects.Controllers;
using Discovery.ComponentServices.Parsing;
using Discovery.RequestManagement;
using Discovery.RequestManagerClient;

namespace Discovery.BusinessSubscribers
{
    /// <summary>
    /// A class to parse commander product
    /// </summary>
    public class CommanderProductMaintenanceParsing : CommanderParsing
    {
        private List<CommanderProduct> products = new List<CommanderProduct>();

        /// <summary>
        /// Initializes a new instance of the <see cref="T:CommanderProductMaintenanceParsing"/> class.
        /// </summary>
        /// <param name="requestProcessor">The request processor.</param>
        public CommanderProductMaintenanceParsing(RequestProcessor requestProcessor)
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
                CommanderProduct.SetProductMaintenanceFileHeaderSchema(textFieldParser.TextFields);

                AddHandlers();

                base.ProcessRequest(requestMessage);
                //save the commander object and return the success of this process
                Status = SaveProducts(requestMessage);
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
            if (!string.IsNullOrEmpty(lineText))
            {
                GetRowTypeAndSubType(lineText, out type, out subType);

                //check the type and subtype so we know what schema to set the parser to and which handlers to setup
                if (type == Stock_Code && subType == "A") //add
                {
                    CommanderProduct.SetCommanderProductMaintenanceAddSchema(textFieldParser.TextFields);
                }
                else if (type == Stock_Code && subType == "M") //modify
                {
                    CommanderProduct.SetCommanderProductMaintenanceModifySchema(textFieldParser.TextFields);
                }
                else if (type != Stock_Code && !((type == HeaderOrFooter && (subType == "F" || subType == "L"))))
                {
                    // Console.WriteLine(type);
                    //not sure what to do here? 
                    //this was the wrong subscriber to process the message, allow the next subscriber to have a go
                }
            }
        }

        /// <summary>
        /// Saves the commander orders.
        /// </summary>
        /// <param name="requestMessage">The request message.</param>
        /// <returns></returns>
        private SubscriberStatusEnum SaveProducts(RequestMessage requestMessage)
        {
            foreach (CommanderProduct product in products)
            {
                if (product.IsUpdate)
                {
                    //this should be an update so check to see if this record already exists
                    CommanderProduct previousRecord = CommanderController.GetProduct(product.ProductCode);
                    if (previousRecord != null)
                    {
                        //the record did not exist so log an exception
                        ////log
                        //LogEntry logEntry=new  LogEntry();
                        //logEntry.Message = "blah";
                        //Logger.Write(logEntry);
                        // throw new Exception("This message has already been sent to the warehouse.");
                        return SubscriberStatusEnum.Processed;
                    }

                    product.Id = previousRecord.Id;
                }

                //save salesOrder and lines
                try
                {
                    int savedId = CommanderController.SaveProduct(product);
                }

                catch (InValidBusinessObjectException ex)
                {
                    //log exception
                    return SubscriberStatusEnum.Failed;
                }
            }

            return SubscriberStatusEnum.Processed;
        }

        /// <summary>
        /// Adds the sales order handlers.
        /// </summary>
        private void AddHandlers()
        {
            textFieldParser.RecordFound -= new TextFieldParser.RecordFoundHandler(CommanderSalesProduct_RecordFound);
            textFieldParser.RecordFound += new TextFieldParser.RecordFoundHandler(CommanderSalesProduct_RecordFound);
        }


        /// <summary>
        /// This fires when a product is successfully read from the message
        /// </summary>
        /// <param name="currentLineNumber">The current line number.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="LineText">The line text.</param>
        private void CommanderSalesProduct_RecordFound(ref int currentLineNumber, TextFieldCollection fields, string LineText)
        {
            //found the header, so populates the object
            PopulateCommanderProduct(fields);
        }


        /// <summary>
        /// Populates a commander product object from the item values held in the TextFieldCollection.
        /// </summary>
        /// <param name="fields">The fields.</param>
        /// <returns></returns>
        private CommanderProduct PopulateCommanderProduct(TextFieldCollection fields)
        {
            CommanderProduct commanderProduct = new CommanderProduct();

            PopulateProperties(fields, commanderProduct);
            commanderProduct.UpdatedBy = GetType().ToString();

            return commanderProduct;
        }
    }
}