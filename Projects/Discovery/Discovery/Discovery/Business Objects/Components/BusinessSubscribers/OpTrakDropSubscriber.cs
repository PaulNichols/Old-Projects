/*************************************************************************************************
 ** FILE:	OptrakDropSubscriber.cs
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
using System.Transactions;
using Discovery.BusinessObjects;
using Discovery.BusinessObjects.Controllers;
using Discovery.ComponentServices.ExceptionHandling;
using Discovery.ComponentServices.Parsing;
using Discovery.RequestManagement;
using Discovery.RequestManagerClient;
using Discovery.Utility;

namespace Discovery.BusinessSubscribers
{
    /*************************************************************************************************
    ** CLASS:	OptrakDropSubscriber
    **
    ** OVERVIEW:
    ** The purpose of this class is to parse output from Optrak and persist this information to the 
    ** management server. This subscriber persists the trip information
    **
    ** MODIFICATION HISTORY:
    **
    ** Date:		Version:    Who:	Change:
    ** 8/8/06	    1.0			PJN		Initial Version
    ************************************************************************************************/

    /// <summary>
    /// A class 'OptrakDropSubscriber' with namespace 'Discovery.BusinessSubscribers'.
    /// It is inherited from ParsingSubscriber
    /// </summary>
    public class OptrakDropSubscriber : ParsingSubscriber
    {

        List<ShipmentDrop> drops = new List<ShipmentDrop>();

        /// <summary>
        /// Initializes a new instance of the <see cref="T:OptrakDropSubscriber"/> class.
        /// </summary>
        /// <param name="requestProcessor">The request processor.</param>
        public OptrakDropSubscriber(RequestProcessor requestProcessor)
            : base(requestProcessor)
        {


        }

        /// <summary>
        /// Processes the request.
        /// </summary>
        /// <param name="requestMessage">The request message.</param>
        public override void ProcessRequest(RequestMessage requestMessage)
        {
            ShipmentDrop.SetDropSchema(textFieldParser.TextFields);

            try
            {
                textFieldParser.FirstLineIsHeader = true;
                textFieldParser.TrimWhiteSpace = true;
                textFieldParser.Delimiter = '|';
                textFieldParser.FileType = TextFieldParser.FileFormat.Delimited;
                // Set up event handlers for when a row is read and when a row is read but failes to match the expected schema
                textFieldParser.RecordFound += new TextFieldParser.RecordFoundHandler(textFieldParser_RecordFound);
                textFieldParser.RecordFailed += new TextFieldParser.RecordFailedHandler(RecordFailed);

                // parse the message
                textFieldParser.ParseFileContents(requestMessage.Body);

                // Processed
                Status = SaveDrops();
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
            drops.Add(PopulateDrop(textFieldParser.TextFields));
        }

        /// <summary>
        /// Records the failed.
        /// </summary>
        /// <param name="CurrentLineNumber">The current line number.</param>
        /// <param name="LineText">The line text.</param>
        /// <param name="ErrorMessage">The error message.</param>
        /// <param name="Continue">if set to <c>true</c> [continue].</param>
        private void RecordFailed(ref int CurrentLineNumber, string LineText, string ErrorMessage, ref bool Continue)
        {
            throw new OptrakParsingException(ErrorMessage, CurrentLineNumber, LineText);
        }

        /// <summary>
        /// Saves the drops.
        /// </summary>
        /// <returns></returns>
        private SubscriberStatusEnum SaveDrops()
        {
           
                ShipmentDrop firstDropInTrip = null;
                foreach (ShipmentDrop drop in drops)
                {
                  

                    if ((firstDropInTrip == null || firstDropInTrip.TripNumber != drop.TripNumber) && drop.CallType == ShipmentDrop.CallTypeEnum.Depot)
                    {
                        //we need to retain the first drop in the trip because we can then use the original
                        //deport data when relating the drops to a trip.
                        //the original deport data can change about after the first drop!
                        firstDropInTrip = drop;
                    }


                    if (firstDropInTrip != null)
                    {
                        DropController.SaveDrop(drop, firstDropInTrip.OriginalDepot);

                        if (RequestProcessor != null && !RequestProcessor.RequestDictionary.ContainsKey("RoutingHistory"))
                        {
                            RoutingHistory routingHistory = RoutingController.GetRoutingHistoryByShipmentId(drop.ShipmentId);
                            if (routingHistory != null)
                            {
                                RequestProcessor.RequestDictionary.Add("RoutingHistory", routingHistory);
                            }
                        }
                        drop.TripId = Null.NullInteger;
                        drop.Id = Null.NullInteger;
                    }
                      
           
                }
          
            return SubscriberStatusEnum.Processed;
        }



        /// <summary>
        /// Populates the commander sales order from the item values held in the TextFieldCollection..
        /// </summary>
        /// <param name="fields">The fields.</param>
        /// <returns></returns>
        private ShipmentDrop PopulateDrop(TextFieldCollection fields)
        {
            ShipmentDrop shipmentDrop = new ShipmentDrop();
            PopulateProperties(fields, shipmentDrop);
            // shipmentDrop.TripId=TripController.GetTripByWarehouseDateAndNumber()
            return shipmentDrop;
        }
    }
}