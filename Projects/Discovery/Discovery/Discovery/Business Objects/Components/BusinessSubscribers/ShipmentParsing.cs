/*************************************************************************************************
 ** FILE:	ShipmentParsing.cs
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
using Discovery.ComponentServices.ExceptionHandling;
using Discovery.ComponentServices.Parsing;
using Discovery.BusinessObjects;
using Discovery.RequestManagement;
using Discovery.RequestManagerClient;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Discovery.BusinessSubscribers
{
    /// <summary>
    /// A class 'ShipmentParsing' with namespace 'Discovery.BusinessSubscribers'.
    /// It is inherited from Discovery.RequestManagement.RequestSubscriber
    /// </summary>
    public class ShipmentParsing : Discovery.RequestManagement.RequestSubscriber
    {
        // Our opco shipment that we're parsing into
        OpCoShipment opcoShipment;

        // Our parser for this request
        TextFieldParser shipmentParser;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ShipmentParsing"/> class.
        /// </summary>
        /// <param name="requestProcessor">The request processor.</param>
        public ShipmentParsing(RequestProcessor requestProcessor) : base(requestProcessor)
        {
        }

        /// <summary>
        /// Processes the request.
        /// </summary>
        /// <param name="requestMessage">The request message.</param>
        public override void ProcessRequest(RequestMessage requestMessage)
        {
            try
            {
                // Text field parser for reading request
                shipmentParser = new TextFieldParser(TextFieldParser.FileFormat.Delimited);

                // Specify the delimiter
                shipmentParser.Delimiter = '|';

                // Specify that we want to trim strings, this is a problem with the interface file format
                shipmentParser.TrimWhiteSpace = true;

                // Wire up event handlers for header
                shipmentParser.RecordFailed += new TextFieldParser.RecordFailedHandler(HeaderRecordFailed);
                shipmentParser.RecordFound += new TextFieldParser.RecordFoundHandler(HeaderRecordFound);

                // Seed fields for header
                OpCoShipment.SetParserSchema(shipmentParser.TextFields);

                // Parse the header and lines
                shipmentParser.ParseFileContents(requestMessage.Body);

                // Store the opco shipment in the request processor for other subscribers
                if (RequestProcessor != null) RequestProcessor.RequestDictionary["OpCoShipment"] = opcoShipment;

                // All worked ok
                Status = SubscriberStatusEnum.Processed;
            }
            catch (Exception ex)
            {
                // Store the exception
                LastError = ex;
                
                //raise an exception because parsing has failed, depending on how the handling is configured this
                //exception will probably be emailed to a user at the opco who could address the problem and resubmit the shipment
                ExceptionPolicy.HandleException
                    (
                        new FailedShipmentParsingException(RequestProcessor.RequestMessage.SourceSystem, ex.Message), 
                        "Business Logic"
                    );

                // Failed
                Status = SubscriberStatusEnum.Failed;
            }
        }

        /// <summary>
        /// Headers the record found.
        /// </summary>
        /// <param name="currentLineNumber">The current line number.</param>
        /// <param name="textFields">The text fields.</param>
        /// <param name="lineText">The line text.</param>
        void HeaderRecordFound(ref int currentLineNumber, TextFieldCollection textFields, string lineText)
        {
            // A matching header has been found, we need to create an instance of the opco shipment
            opcoShipment = new OpCoShipment();

            // Fill the opco shipment using the parsed header
            TextFieldParser.FillObject(opcoShipment, textFields);

            int Hrs, Mins;
            string[] HHMMSS;

            // We need manual conversion here for CheckInTimeHHMMSS as passed on interface in some ridiculous format
            HHMMSS = textFields["CheckInTimeHHMMSS"].Value.ToString().Split(':');
            Hrs = Convert.ToInt32(HHMMSS[0]);
            Mins = Convert.ToInt32(HHMMSS[1]);
            opcoShipment.CheckInTime = (Hrs * 60) + Mins;

            // We need manual conversion here for the current time, we need to add it to the generated date
            HHMMSS = textFields["GeneratedTimeHHMMSS"].Value.ToString().Split(':');
            opcoShipment.GeneratedDateTime = opcoShipment.GeneratedDateTime.AddHours(Convert.ToInt32(HHMMSS[0]));
            opcoShipment.GeneratedDateTime = opcoShipment.GeneratedDateTime.AddMinutes(Convert.ToInt32(HHMMSS[1]));
            opcoShipment.GeneratedDateTime = opcoShipment.GeneratedDateTime.AddSeconds(Convert.ToInt32(HHMMSS[2]));

            // Unwire event handlers for header
            shipmentParser.RecordFailed -= new TextFieldParser.RecordFailedHandler(HeaderRecordFailed);
            shipmentParser.RecordFound -= new TextFieldParser.RecordFoundHandler(HeaderRecordFound);

            // Wire up event handlers for line
            shipmentParser.RecordFailed += new TextFieldParser.RecordFailedHandler(LineRecordFailed);
            shipmentParser.RecordFound += new TextFieldParser.RecordFoundHandler(LineRecordFound);

            // Seed fields for line
            OpCoShipmentLine.SetParserSchema(shipmentParser.TextFields);
        }

        /// <summary>
        /// Lines the record found.
        /// </summary>
        /// <param name="currentLineNumber">The current line number.</param>
        /// <param name="textFields">The text fields.</param>
        /// <param name="lineText">The line text.</param>
        void LineRecordFound(ref int currentLineNumber, TextFieldCollection textFields, string lineText)
        {
            // A matching line has been found, we need to create an instance of the shipment line
            OpCoShipmentLine opcoShipmentLine = new OpCoShipmentLine();

            // Fill the opcoshipment using the parsed header
            TextFieldParser.FillObject(opcoShipmentLine, textFields);

            // Add the line to the shipment
            opcoShipment.ShipmentLines.Add(opcoShipmentLine);
        }

        /// <summary>
        /// Headers the record failed.
        /// </summary>
        /// <param name="CurrentLineNumber">The current line number.</param>
        /// <param name="LineText">The line text.</param>
        /// <param name="ErrorMessage">The error message.</param>
        /// <param name="Continue">if set to <c>true</c> [continue].</param>
        void HeaderRecordFailed(ref int CurrentLineNumber, string LineText, string ErrorMessage, ref bool Continue)
        {
            // Failed to parse a header record
            throw new Exception(string.Format("Error parsing Shipment header (file line {0}), {1}  Header data was, {2}.", CurrentLineNumber, "\n\n" + ErrorMessage, LineText));
        }

        /// <summary>
        /// Lines the record failed.
        /// </summary>
        /// <param name="CurrentLineNumber">The current line number.</param>
        /// <param name="LineText">The line text.</param>
        /// <param name="ErrorMessage">The error message.</param>
        /// <param name="Continue">if set to <c>true</c> [continue].</param>
        void LineRecordFailed(ref int CurrentLineNumber, string LineText, string ErrorMessage, ref bool Continue)
        {
            // Failed to parse a header record
            throw new Exception(string.Format("Error parsing Shipment line (file line {0}), {1}  Line data was, {2}.", CurrentLineNumber, "\n\n"+ErrorMessage, LineText));
        }
    }
}
