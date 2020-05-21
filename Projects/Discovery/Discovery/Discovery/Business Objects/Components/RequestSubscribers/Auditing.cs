using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Discovery.RequestManagement;
using Discovery.RequestManagerClient;
using Discovery.BusinessObjects;
using Discovery.BusinessObjects.Controllers;
using System.Transactions;

namespace Discovery.RequestSubscribers
{
    /// <summary>
    /// A Class 'Auditing' with namespace 'Discovery.RequestSubscribers'.
    /// It is inherited from RequestManagement.RequestSubscriber
    /// </summary>
    public class Auditing : RequestManagement.RequestSubscriber
    {
        ///// <summary>
        ///// An object 'auditEntry' is defined as Discovery.BusinessObjects.MessageAuditEntry object
        ///// </summary>
        //private Discovery.BusinessObjects.MessageAuditEntry auditEntry;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Auditing"/> class.
        /// </summary>
        /// <param name="requestProcessor">The request processor.</param>
        public Auditing(RequestProcessor requestProcessor) : base(requestProcessor)
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
                // Create a new audit entry
                MessageAuditEntry AuditEntry = new MessageAuditEntry();
                
                // Seed values
                AuditEntry.Id = -1;
                AuditEntry.SourceSystem = requestMessage.SourceSystem;
                AuditEntry.DestinationSystem = requestMessage.DestinationSystem;
                AuditEntry.ReceivedDate = DateTime.Now;
                AuditEntry.Message = requestMessage.Body;
                AuditEntry.Sequence = requestMessage.Sequence;
                AuditEntry.Type = requestMessage.Type;
                AuditEntry.Name = requestMessage.Name;
                AuditEntry.Label = requestMessage.Label;
                AuditEntry.UpdatedBy = this.GetType().FullName;

                // Save the audit entry to the db
                AuditEntryController.SaveAuditEntry(AuditEntry);

                // Cache the audit entry for later use
                this.RequestProcessor.RequestDictionary["AuditEntry"] = AuditEntry;

                // All done
                Status = SubscriberStatusEnum.Processed;
            }
            catch(Exception ex)
            {
                // Store last error
                LastError = ex;

                // Failed
                Status = SubscriberStatusEnum.Failed;
            }
        }
    }
}
