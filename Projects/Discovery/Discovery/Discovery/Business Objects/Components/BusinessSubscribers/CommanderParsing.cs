using System;
using System.Reflection;
using Discovery.BusinessObjects;
using Discovery.ComponentServices.ExceptionHandling;
using Discovery.ComponentServices.Parsing;
using Discovery.RequestManagement;
using Discovery.RequestManagerClient;

namespace Discovery.BusinessSubscribers
{
    /// <summary>
    /// An abstract class 'CommanderParsing' with namespace 'Discovery.BusinessSubscribers'.
    /// It is inherited from ParsingSubscriber
    /// </summary>
    public abstract class CommanderParsing : ParsingSubscriber
    {
        /// <summary>
        /// A constant string HeaderOrFooter
        /// </summary>
        protected const string HeaderOrFooter = "000";
        /// <summary>
        /// A constant string Delivery_Note_Or_Returns
        /// </summary>
        protected const string Delivery_Note_Or_Returns = "100";
        /// <summary>
        /// A constant string Advice_Note_Or_Return_Item
        /// </summary>
        protected const string Advice_Note_Or_Return_Item = "101";
        /// <summary>
        /// A constant string Stock_Code
        /// </summary>
        protected const string Stock_Code = "106";//product maintenance
        /// <summary>
        /// A constant string Customer_Order
        /// </summary>
        protected const string Customer_Order = "115";//sales order header
        /// <summary>
        /// A constant string Order_Item
        /// </summary>
        protected const string Order_Item = "116";//sales order line
        /// <summary>
        /// A constant string Stock_Alias
        /// </summary>
        protected const string Stock_Alias = "221";
        /// <summary>
        /// A constant string Advice_Note_Number
        /// </summary>
        protected const string Advice_Note_Number = "900";
        /// <summary>
        /// A constant string Pallet_Quantity_Change
        /// </summary>
        protected const string Pallet_Quantity_Change = "104";
        /// <summary>
        /// A constant string Stock_Receipt
        /// </summary>
        protected const string Stock_Receipt = "416";//goods-in
        /// <summary>
        /// A constant string Stock_Summary
        /// </summary>
        protected const string Stock_Summary = "030";//stock list

      
        /// <summary>
        /// Initializes a new instance of the <see cref="T:CommanderParsing"/> class.
        /// </summary>
        /// <param name="requestProcessor">The request processor.</param>
        public CommanderParsing(RequestProcessor requestProcessor)
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
                // Set up event handlers for when a row is read and when a row is read but failes to match the expected schema
                textFieldParser.RecordRead += new TextFieldParser.RecordReadHandler(textFieldParser_RecordRead);
                textFieldParser.RecordFailed += new TextFieldParser.RecordFailedHandler(RecordFailed);

                // parse the message
                textFieldParser.ParseFileContents(requestMessage.Body);

                // Processed
                Status = SubscriberStatusEnum.Processed;
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
        protected abstract void textFieldParser_RecordRead(int CurrentLineNumber, string lineText);

        /// <summary>
        /// Gets the type of the row type and sub.
        /// </summary>
        /// <param name="LineText">The line text.</param>
        /// <param name="type">The type.</param>
        /// <param name="subType">Type of the sub.</param>
        protected void GetRowTypeAndSubType(string LineText, out string type, out string subType)
        {
            int mark = textFieldParser.TextFields["LineNumber"].Length;
            type = LineText.Substring(mark, textFieldParser.TextFields["RecordType"].Length);
            mark += textFieldParser.TextFields["RecordType"].Length;
            subType = LineText.Substring(mark, textFieldParser.TextFields["RecordSubType"].Length);
        }


        /// <summary>
        /// Records that failed reading.
        /// </summary>
        /// <remarks>Hopefully failures should be handled as they are the expected first and last rows, otherwise an exception is thrown</remarks>
        /// <param name="CurrentLineNumber">The current line number.</param>
        /// <param name="lineText">The line text.</param>
        /// <param name="ErrorMessage">The error body.</param>
        /// <param name="Continue">if set to <c>true</c> [continue].</param>
        private void RecordFailed(ref int CurrentLineNumber, string lineText,
                                  string ErrorMessage, ref bool Continue)
        {

            Continue = true;
            return;
        }
    }

    /// <summary>
    /// A Class 'ParsingSubscriber' with namespace 'Discovery.BusinessSubscribers'.
    /// It is inherited from RequestSubscriber
    /// </summary>

    public abstract class ParsingSubscriber : RequestSubscriber
    {
        /// <summary>
        /// A protected field 'textFieldParser' defined as a class 'TextFieldParser'
        /// </summary>
        protected TextFieldParser textFieldParser=new TextFieldParser();
        
            /// <summary>
        /// Initializes a new instance of the <see cref="T:ParsingSubscriber"/> class.
        /// </summary>
        /// <param name="requestProcessor">The request processor.</param>
         public ParsingSubscriber(RequestProcessor requestProcessor)
            : base(requestProcessor)
        {
        }
         
        /// <summary>
        /// Populates the properties of the provided object using reflection.
        /// </summary>
        /// <remarks>If a property name includes a '.' an attempt will be made to populate the child property</remarks>
        /// <param name="fields">The fields.</param>
        /// <param name="objectToPopulate">The object to populate.</param>
        protected static void PopulateProperties(TextFieldCollection fields, DiscoveryBusinessObject objectToPopulate)
        {
            foreach (TextField field in fields)
            {
                String[] propertyName = field.Name.Split(new char[] { '.' });
                PropertyInfo property = objectToPopulate.GetType().GetProperty(propertyName[0]);
                if (property != null)
                {
                    object parentObject = objectToPopulate;
                    if (propertyName.Length == 2)
                    {
                        //crappy code, should be recursive but it's a start when deailing with complex custom types!
                        parentObject = property.GetValue(objectToPopulate, null);
                        property = parentObject.GetType().GetProperty(propertyName[1]);
                    }
                    if (property != null)
                    {
                        object fieldValue = field.Value;
                        //if (field.Value.GetType().Equals(typeof(String)))
                        //    fieldValue = fieldValue.ToString().Trim();

                        property.SetValue(parentObject, fieldValue, null);
                    }
                }
            }
        }
    }
}