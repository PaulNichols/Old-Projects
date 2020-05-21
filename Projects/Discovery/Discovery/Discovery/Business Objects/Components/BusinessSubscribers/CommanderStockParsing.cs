using System;
using System.Collections.Generic;
using System.Configuration;
using Discovery.BusinessObjects;
using Discovery.BusinessObjects.Controllers;
using Discovery.ComponentServices.Parsing;
using Discovery.RequestManagement;
using Discovery.RequestManagerClient;

namespace Discovery.BusinessSubscribers
{
    /// <summary>
    /// A class to parse the commander stock and goods
    /// </summary>
    public abstract class CommanderStockAndGoodsInParsing : CommanderParsing
    {
        private List<string> hspgItems = new List<string>();
        private List<string> tpcItems = new List<string>();
        private string headerLine;
        private string footerLine;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:CommanderStockAndGoodsInParsing"/> class.
        /// </summary>
        /// <param name="requestProcessor">The request processor.</param>
        public CommanderStockAndGoodsInParsing(RequestProcessor requestProcessor)
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
                SetFileHeaderAndFooterSchema(textFieldParser.TextFields);

                AddHandlers();

                //save the commander object
                base.ProcessRequest(requestMessage);
                Status=Send(requestMessage);
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
            if (!string.IsNullOrEmpty(lineText))
            {
                string type;
                string subType;
                GetRowTypeAndSubType(lineText, out type, out subType);

                //check the type and subtype so we know what schema to set the parser to and which handlers to setup
                if (type == GetItemType() && subType == GetSubItemType()) //add
                {
                    SetItemSchema(textFieldParser.TextFields);
                }
                else if (type == HeaderOrFooter && subType == "L") //footer
                {
                    SetFileHeaderAndFooterSchema(textFieldParser.TextFields);
                }
            }
        }

        /// <summary>
        /// Sets the item schema.
        /// </summary>
        /// <param name="fields">The fields.</param>
        protected abstract void SetItemSchema(TextFieldCollection fields);

        /// <summary>
        /// Adds the common fields.
        /// </summary>
        /// <param name="fields">The fields.</param>
        protected void AddCommonFields(TextFieldCollection fields)
        {
            fields.Add(new TextField("LineNumber", TypeCode.Int32, 5));
            fields.Add(new TextField("RecordType", TypeCode.Int32, 3));
            fields.Add(new TextField("RecordSubType", TypeCode.String, 1));
        }

        /// <summary>
        /// Sets the file header and footer schema.
        /// </summary>
        /// <param name="fields">The fields.</param>
        protected void SetFileHeaderAndFooterSchema(TextFieldCollection fields)
        {
            fields.Clear();
            AddCommonFields(fields);
            fields.Add(new TextField("Date", TypeCode.DateTime, 10));
            fields.Add(new TextField("Time", TypeCode.DateTime, 5)); //should be 8 but the test data only has 5!
        }

        /// <summary>
        /// This method will place the split message back on the queue
        /// </summary>
        /// <param name="requestMessage">The request message.</param>
        /// <returns></returns>
        private SubscriberStatusEnum Send(RequestMessage requestMessage)
        {
            RequestManagerClientMSMQ msmq = new RequestManagerClientMSMQ();
            msmq.QueueName = ConfigurationManager.AppSettings["CommanderOutQueueName"];
      
            RequestMessage newRequestMessage=null;
            
            if (hspgItems.Count > 0)
            {
                //convert the HSPG goods in records into a string
                string hspgMessage = String.Join(Environment.NewLine, hspgItems.ToArray());
                //create a request message and set the body (Header, message and Footer)
                newRequestMessage = new RequestMessage(string.Concat(headerLine, hspgMessage, footerLine));
            }
            if (tpcItems.Count > 0)
            {
                //convert TPC goods in records into a string
                string tpcMessage = String.Join(Environment.NewLine, tpcItems.ToArray());
                //create a request message and set the body (Header, message and Footer)
                newRequestMessage = new RequestMessage(string.Concat(headerLine, tpcMessage, footerLine));
            }

            if (newRequestMessage != null)
            {
                newRequestMessage.DestinationSystem = site;
                newRequestMessage.SourceSystem = requestMessage.DestinationSystem;
                newRequestMessage.Type = Integration.Connection.ConnectionTypeEnum.Commander.ToString();
               // newRequestMessage.Name = fileNameToDownLoad;??
               // newRequestMessage.Sequence = task.SequenceNumber;
                
                //send message
                msmq.Send(newRequestMessage);
            }
            return SubscriberStatusEnum.Processed;
        }

        /// <summary>
        /// Adds the sales order handlers.
        /// </summary>
        private void AddHandlers()
        {
            textFieldParser.RecordFound -= new TextFieldParser.RecordFoundHandler(RecordFound);
            textFieldParser.RecordFound += new TextFieldParser.RecordFoundHandler(RecordFound);
        }

        private string site;
        
        /// <summary>
        /// This fires when a product is successfully read from the message
        /// </summary>
        /// <param name="currentLineNumber">The current line number.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="lineText">The line text.</param>
        private void RecordFound(ref int currentLineNumber, TextFieldCollection fields, string lineText)
        {
            string type;
            string subType;
            GetRowTypeAndSubType(lineText, out type, out subType);

            if (type == GetItemType() && subType == GetSubItemType()) //add
            {
                //get commander product by product code
                string productCode = fields["ProductCode"].Value.ToString().Trim();
               if (string.IsNullOrEmpty(site)) site = fields["Site"].Value.ToString().Trim();
                CommanderProduct commanderProduct = CommanderController.GetProduct(productCode);
                if (commanderProduct == null) //there is no matching HSPG product in our product list
                {
                    hspgItems.Add(lineText);
                }
                else
                {
                    tpcItems.Add(lineText);
                }
            }
            else if (type == HeaderOrFooter && subType == "L") //footer
            {
                footerLine = lineText;
            }
            else if (type == HeaderOrFooter && subType == "F") //header
            {
                headerLine = lineText;
            }
        }

        /// <summary>
        /// Gets the type of the item.
        /// </summary>
        /// <returns></returns>
        protected abstract string GetItemType();
        /// <summary>
        /// Gets the type of the sub item.
        /// </summary>
        /// <returns></returns>
        protected abstract string GetSubItemType();
    }

    /// <summary>
    /// A class to parse the commander stock
    /// </summary>
    public class CommanderStockParsing : CommanderStockAndGoodsInParsing
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:CommanderStockParsing"/> class.
        /// </summary>
        /// <param name="requestProcessor">The request processor.</param>
        public CommanderStockParsing(RequestProcessor requestProcessor)
            : base(requestProcessor)
        {
        }


        /// <summary>
        /// Sets the item schema.
        /// </summary>
        /// <param name="fields">The fields.</param>
        protected override void SetItemSchema(TextFieldCollection fields)
        {
            fields.Clear();
            AddCommonFields(fields);
            fields.Add(new TextField("Site", TypeCode.String, 10));
            fields.Add(new TextField("ProductCode", TypeCode.String, 20));
            fields.Add(new TextField("Quantity", TypeCode.Int32, 19));
            fields.Add(new TextField("CustomerReferenceNumber", TypeCode.String, 20));
        }

        /// <summary>
        /// Gets the type of the item.
        /// </summary>
        /// <returns></returns>
        protected override string GetItemType()
        {
            return Stock_Summary;
        }

        /// <summary>
        /// Gets the type of the sub item.
        /// </summary>
        /// <returns></returns>
        protected override string GetSubItemType()
        {
            return "I";
        }
    }
}