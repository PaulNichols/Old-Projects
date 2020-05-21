/*************************************************************************************************
 ** FILE:	OpTrakTripSubscriber.cs
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

namespace Discovery.BusinessSubscribers
{
    /*************************************************************************************************
    ** CLASS:	OptrakTripSubscriber
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
    /// A Class 'OptrakTripSubscriber' with namespace 'Discovery.BusinessSubscribers'.
    /// It is inherited from ParsingSubscriber
    /// </summary>

    public class OptrakTripSubscriber : ParsingSubscriber
    {
        private List<Trip> trips = new List<Trip>();
        private List<OptrakRegion> allRegions;
        private List<Warehouse> allWarehouses;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:OptrakTripSubscriber"/> class.
        /// </summary>
        /// <param name="requestProcessor">The request processor.</param>
        public OptrakTripSubscriber(RequestProcessor requestProcessor)
            : base(requestProcessor)
        {


        }

        /// <summary>
        /// Processes the request.
        /// </summary>
        /// <param name="requestMessage">The request message.</param>
        public override void ProcessRequest(RequestMessage requestMessage)
        {
            Trip.SetTripSchema(textFieldParser.TextFields);

            //get all warehouse objects in one hit for performance
            allWarehouses = WarehouseController.GetWarehouses(false);

            //get all regions in one hit for performance
            allRegions = OptrakRegionController.GetRegions();

            try
            {
                textFieldParser.FirstLineIsHeader = true;
                textFieldParser.FileType = TextFieldParser.FileFormat.Delimited;
                textFieldParser.Delimiter = '|';
                textFieldParser.TrimWhiteSpace = true;
                // Set up event handlers for when a row is read and when a row is read but failes to match the expected schema
                textFieldParser.RecordFound += new TextFieldParser.RecordFoundHandler(textParser_RecordFound);
                textFieldParser.RecordFailed += new TextFieldParser.RecordFailedHandler(RecordFailed);

                // parse the message
                textFieldParser.ParseFileContents(requestMessage.Body);

                // Processed
                Status = SaveTrips();
            }
            catch (Exception ex)
            {
                // Store the exception
                LastError = ex;

                // Failed
                Status = SubscriberStatusEnum.Failed;
            }
        }

        private void textParser_RecordFound(ref int currentLineNumber, TextFieldCollection textFields, string lineText)
        {
            trips.Add(PopulateTrip(textFieldParser.TextFields));
        }

        private void RecordFailed(ref int CurrentLineNumber, string LineText, string ErrorMessage, ref bool Continue)
        {
            throw new OptrakParsingException(ErrorMessage, CurrentLineNumber, LineText);
        }

        private SubscriberStatusEnum SaveTrips()
        {
            foreach (Trip trip in trips)
            {
                TripController.SaveTrip(trip);
            }
            return SubscriberStatusEnum.Processed;
        }




        /// <summary>
        /// Populates the commander sales order from the item values held in the TextFieldCollection..
        /// </summary>
        /// <param name="fields">The fields.</param>
        /// <returns></returns>
        private Trip PopulateTrip(TextFieldCollection fields)
        {
            Trip trip = new Trip();
            PopulateProperties(fields, trip);
            string deport = fields["Depot"].Value.ToString();
            trip.Warehouse = allWarehouses.Find(delegate(Warehouse currentWarehouse)
                                                    {
                                                        return (currentWarehouse.Code == deport);
                                                    });
            if (trip.Warehouse == null)
            {
                throw new Exception(
                    string.Format("During Optrak Trip file processsing the Warehouse '{0}' could not be found.",
                                  deport));
            }
            string regionCode = fields["RegionCode"].Value.ToString();
            trip.OptrakRegion = allRegions.Find(delegate(OptrakRegion currentWarehouse)
                                              {
                                                  return (currentWarehouse.Code == regionCode);
                                              });
            if (trip.OptrakRegion == null)
            {
                throw new Exception(
                    string.Format("During Optrak Trip file processsing the OptrakRegion '{0}' could not be found.",
                                  regionCode));
            }

            trip.WarehouseId = trip.Warehouse.Id;
            trip.RegionId = trip.OptrakRegion.Id;

            return trip;
        }
    }
}