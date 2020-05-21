using System;
using System.Collections.Generic;
using System.Diagnostics;
using Discovery.BusinessObjects;
using Discovery.BusinessObjects.Controllers;
using Discovery.ComponentServices.Parsing;
using Discovery.RequestManagement;
using Discovery.RequestManagerClient;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Discovery.BusinessSubscribers
{
    /// <summary>
    /// This class will aim to parse a message and then delegate the work 
    /// to the appropriate parser depending on the file type.
    /// </summary>
    public class CommanderParsingProcessor : CommanderParsing
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:CommanderProductMaintenanceParsing"/> class.
        /// </summary>
        /// <param name="requestProcessor">The request processor.</param>
        public CommanderParsingProcessor(RequestProcessor requestProcessor)
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
#if DEBUG
                // Log that the shipment cannot be updated
                Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry logEntry = new Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry(
                    string.Concat(""),
                    "Request Management",
                    0,
                    0,
                    TraceEventType.Information,
                    null,
                    null);

                // Write to the log
                Logger.Write(logEntry);
#endif
                AddCommonFieldsToSchema();
                textFieldParser.RecordFound += new TextFieldParser.RecordFoundHandler(textFieldParser_RecordFound);
                base.ProcessRequest(requestMessage);
                //save the commander object and return the success of this process
            }
            catch (Exception ex)
            {
                // Store the exception
                LastError = ex;

                // Failed
                Status = SubscriberStatusEnum.Failed;
            }
        }

        void textFieldParser_RecordFound(ref int currentLineNumber, TextFieldCollection textFields, string lineText)
        {
            string type;
            string subType;
            if (!string.IsNullOrEmpty(lineText))
            {
                GetRowTypeAndSubType(lineText, out type, out subType);

                if (type != HeaderOrFooter && !(subType == "F" || subType == "L"))
                {
                    //check the type and subtype so we know what schema to set the parser to and which handlers to setup
                    RequestSubscriber subscriber = null;
                    switch (type)
                    {
                        case Stock_Code:
                            subscriber = new CommanderProductMaintenanceParsing(this.RequestProcessor);
                            subscriber.ProcessRequest(this.RequestProcessor.RequestMessage);
                            break;
                        case Stock_Receipt:
                            subscriber = new CommanderGoodsInParsing(this.RequestProcessor);
                            subscriber.ProcessRequest(this.RequestProcessor.RequestMessage);
                            break;
                        case Customer_Order:
                            subscriber = new CommanderSalesOrderParsing(this.RequestProcessor);
                            subscriber.ProcessRequest(this.RequestProcessor.RequestMessage);
                            break;
                        case Stock_Summary:
                            subscriber = new CommanderStockParsing(this.RequestProcessor);
                            subscriber.ProcessRequest(this.RequestProcessor.RequestMessage);
                            break;
                        default:
                            // Log that a unrecognised commander message has been found
                            Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry logEntry = new Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry(
                                string.Concat(""),
                                "Request Management",
                                0,
                                0,
                                TraceEventType.Information,
                                null,
                                null);

                            // Write to the log
                            Logger.Write(logEntry);

                            //         Status=SubscriberStatusEnum.Consumed;
                            break;
                    }
                    //textFieldParser.RecordRead -= new TextFieldParser.RecordReadHandler(textFieldParser_RecordRead);
                    if (subscriber != null && subscriber.Status == SubscriberStatusEnum.Failed)
                    {
                        this.LastError = subscriber.LastError;
                        this.Status = subscriber.Status;
                    }
                }

            }
        }

        private void AddCommonFieldsToSchema()
        {
            textFieldParser.TextFields.Clear();
            textFieldParser.TextFields.Add(new TextField("LineNumber", TypeCode.Int32, 5));
            textFieldParser.TextFields.Add(new TextField("RecordType", TypeCode.Int32, 3));
            textFieldParser.TextFields.Add(new TextField("RecordSubType", TypeCode.String, 1));
        }


        /// <summary>
        /// Texts the parser_ record read.
        /// </summary>
        /// <param name="CurrentLineNumber">The current line number.</param>
        /// <param name="lineText">The line text.</param>
        protected override void textFieldParser_RecordRead(int CurrentLineNumber, string lineText)
        {
            
        }
    }
}