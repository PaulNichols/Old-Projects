/*************************************************************************************************
 ** FILE:	ShipmentMapping.cs
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
using Discovery.BusinessObjects;
using Discovery.BusinessObjects.Controllers;
using Discovery.RequestManagement;
using Discovery.RequestManagerClient;
using Discovery.ComponentServices.Mapping;
using Discovery.ComponentServices.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Diagnostics;

namespace Discovery.BusinessSubscribers
{
    /// <summary>
    /// A class to provide the shipment mapping which is a business subscriber
    /// with namespace Discovery.BusinessSubscribers
    /// </summary>

    public class ShipmentMapping : RequestManagement.RequestSubscriber
    {
        // The new opco shipment 
        private OpCoShipment opCoShipment = null;
        // The new tdc shipment
        private TDCShipment tdcShipment = null;
        // The existing opco shipment if we have one
        private OpCoShipment existingOpCoShipment = null;
        // The existing tdc shipment if we have one
        private TDCShipment existingTDCShipment = null;
        // The audit entry for this shipment
        MessageAuditEntry auditEntry = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ShipmentMapping"/> class.
        /// </summary>
        /// <param name="requestProcessor">The request processor.</param>
        public ShipmentMapping(RequestProcessor requestProcessor)
            : base(requestProcessor)
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
                // Get the opco shipment from the request processor
                if (!RequestProcessor.RequestDictionary.ContainsKey("OpCoShipment") ||
                    RequestProcessor.RequestDictionary["OpCoShipment"] == null)
                {
                    // Failed to retrieve the opco shipment from the processor
                    throw new Exception("Attempting to map an Op Co Shipment but it was not found in the Request Processor.  Make sure that the shipment parser has been subscribed before the shipment mapper.");
                }

                // Get the cached opco shipment from the processor
                opCoShipment = (OpCoShipment)RequestProcessor.RequestDictionary["OpCoShipment"];

                // Get the audit entry from the processor if we have one (optional)
                if (RequestProcessor.RequestDictionary.ContainsKey("AuditEntry") &&
                    RequestProcessor.RequestDictionary["AuditEntry"] != null)
                {
                    // Get the cached audit entry from the processor
                    auditEntry = (MessageAuditEntry)RequestProcessor.RequestDictionary["AuditEntry"];
                }

                // *****************************************************************
                // ** SEE IF WE HAVE AN EXISTING OPCO SHIPMENT
                // *****************************************************************

                // See if we already have the shipment in the db
                existingOpCoShipment = OpCoShipmentController.GetShipment(
                           opCoShipment.OpCoCode,
                           opCoShipment.ShipmentNumber,
                           opCoShipment.DespatchNumber);

                // Did we find an existing opco shipment
                if (null != existingOpCoShipment)
                {
                    // We've found the shipment, make sure that our sequence number is later
                    if (existingOpCoShipment.OpCoSequenceNumber >= opCoShipment.OpCoSequenceNumber)
                    {
                        // Consume the message as it's old
                        Status = SubscriberStatusEnum.Processed;

                        // Log that the shipment cannot be updated
                        Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry logEntry = new Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry(
                            string.Format("Unable to update existing opco shipment {0} - {1} for for opco {2}.  The request sequence number is {3} but sequence {4} has already been processed.",
                                    opCoShipment.ShipmentNumber,
                                    opCoShipment.DespatchNumber,
                                    opCoShipment.OpCoCode,
                                    opCoShipment.OpCoSequenceNumber,
                                    existingOpCoShipment.OpCoSequenceNumber),
                            "Request Management",
                            0,
                            0,
                            TraceEventType.Information,
                            "Request Management",
                            null);

                        // Write to the log
                        Logger.Write(logEntry);

                        // done
                        return;
                    }
                    else
                    {
                        // See if we need to update the tdc shipment
                        if (existingOpCoShipment.Status == Shipment.StatusEnum.Mapped)
                        {
                            // *****************************************************************
                            // ** SEE IF WE HAVE AN EXISTING TDC SHIPMENT
                            // *****************************************************************

                            // Get the existing tdc shipment
                            existingTDCShipment = TDCShipmentController.GetShipment(
                                        opCoShipment.OpCoCode,
                                        opCoShipment.ShipmentNumber,
                                        opCoShipment.DespatchNumber);

                            // Did we find an existing tdc shipment?
                            if (null != existingTDCShipment)
                            {
                                // See if the tdc shipment can be updated
                                if (existingTDCShipment.Status != Shipment.StatusEnum.Mapped)
                                {
                                    // Consume the message, we can't make the update
                                    Status = SubscriberStatusEnum.Consumed;

                                    // Log that the shipment cannot be updated
                                    Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry logEntry = new Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry(
                                        string.Format("Unable to update existing tdc shipment {0} - {1} for for opco {2}.  The status of the existing tdc shipment is {3} disallowing this update.",
                                                existingTDCShipment.ShipmentNumber,
                                                existingTDCShipment.DespatchNumber,
                                                existingTDCShipment.OpCoCode,
                                                existingTDCShipment.Status),
                                        "Request Management",
                                        0,
                                        0,
                                        TraceEventType.Information,
                                        "Request Management",
                                        null);

                                    // Write to the log
                                    Logger.Write(logEntry);

                                    // done
                                    return;
                                }
                            }
                            else
                            {
                                // We should have found the tdc shipment
                                Status = SubscriberStatusEnum.Failed;

                                // Log that the tdc shipment cannot be updated
                                Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry logEntry = new Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry(
                                    string.Concat("Unable to update tdc shipment ", opCoShipment.ShipmentNumber, "-", opCoShipment.DespatchNumber, " for opco ", opCoShipment.OpCoCode, ".  The tdc shipment was not found."),
                                    "Request Management",
                                    0,
                                    0,
                                    TraceEventType.Error,
                                    "Request Management",
                                    null);

                                // Write to the log
                                Logger.Write(logEntry);

                                // done
                                return;
                            }
                        }

                    }
                }

                // *****************************************************************
                // ** SAVE OR UPDATE THE OPCO SHIPMENT
                // *****************************************************************

                // See if we have an existing opco shipment
                if (null != existingOpCoShipment)
                {
                    // Update existing opco shipment
                    opCoShipment = (OpCoShipment)existingOpCoShipment.UpdateFromShipment(opCoShipment);
                }

                // Specify that the shipment was updated by this subscriber
                opCoShipment.UpdatedBy = this.GetType().FullName;

                // See if the shipment has been cancelled
                if (0 == opCoShipment.TotalLineQuantity)
                {
                    // Cancel the shipment
                    opCoShipment.Status = Shipment.StatusEnum.Cancelled;
                }
                else if (opCoShipment.OpCoHeld)
                {
                    // Hold the shipment
                    opCoShipment.Status = Shipment.StatusEnum.Held;
                }

                // See if we have an audit entry (not required)
                if (null != auditEntry)
                {
                    // Store the audit entry id
                    opCoShipment.AuditId = auditEntry.Id;
                }

                // Save the opco shipment to the db
                if (-1 != OpCoShipmentController.SaveShipment(opCoShipment))
                {
                    // Cache the opco shipment for other subscribers
                    RequestProcessor.RequestDictionary["OpCoShipment"] = opCoShipment;

                    // *****************************************************************
                    // ** MAP AND SAVE THE TDC SHIPMENT
                    // *****************************************************************

                    // We only create the TDC Shipment if the OpCo Shipment has not been cancelled
                    // If it has been cancelled and we have an existing TDC Shipment we still update it
                    if (opCoShipment.Status != Shipment.StatusEnum.Cancelled || null != existingTDCShipment)
                    {
                        try
                        {
                            // Map the opco shipment to a new tdc shipment
                            tdcShipment = opCoShipment.MapToTDC(existingTDCShipment, this.GetType().FullName, true);

                            // Store the tdc shipment for other subscribers
                            RequestProcessor.RequestDictionary["TDCShipment"] = tdcShipment;
                        }
                        catch (Exception ex)
                        {
                            // Log that the shipment cannot be updated
                            Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry logEntry = new Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry(
                                string.Format("The opco shipment {0} - {1} for opco {2} has a status of NOTMAPPED.  The error was {3}", opCoShipment.ShipmentNumber, opCoShipment.DespatchNumber, opCoShipment.OpCoCode, ex.Message),
                                "Request Management",
                                0,
                                0,
                                TraceEventType.Error,
                                "Request Management",
                                null);

                            // Write to the log
                            Logger.Write(logEntry);
                        }
                    }

                    // Processed
                    Status = SubscriberStatusEnum.Processed;
                }
                else
                {
                    // We failed to save the op co shipment
                }
            }
            catch (Exception ex)
            {
                // Store the exception
                LastError = ex;

                // Failed
                Status = SubscriberStatusEnum.Failed;
            }
            finally
            {
                // Cleanup
                existingOpCoShipment = null;
                existingTDCShipment = null;
            }
        }
    }
}
