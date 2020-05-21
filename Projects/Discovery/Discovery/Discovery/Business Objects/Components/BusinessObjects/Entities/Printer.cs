using System;
using System.Collections.Generic;
using ValidationFramework;

namespace Discovery.BusinessObjects
{
    /*************************************************************************************************
     ** CLASS:	Printer
     **
     ** OVERVIEW:
     ** This class is the business object for a single printer
     ** http://msdn.microsoft.com/library/default.asp?url=/library/en-us/wmisdk/wmi/win32_printjob.asp
     ** 
     ** MODIFICATION HISTORY:
     **
     ** Date:		Version:    Who:	Change:
     ** 19/7/06	    1.0			PJN		Initial Version
     ************************************************************************************************/
    /// <summary>
    /// A Class 'Printer' which is an entity with namespace Discovery.BusinessObjects
    /// It is inherited from DiscoveryBusinessObject
    /// The class holds the printer name
    /// </summary>
    [Serializable]
    public class Printer : PersistableBusinessObject
    {
        #region Private Fields

        private string name;
        private bool isDefault;
        #endregion

        #region Public Properties


        /// <summary>
        /// Gets or sets The region description, i.e. Scotland.
        /// </summary>
        /// <value>The description.</value>
        [RequiredValidator("Name is required.", "*")]
        [LengthValidator(256, "The maximum length of a Name is 256 characters.", "*")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is default.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is default; otherwise, <c>false</c>.
        /// </value>
        public bool IsDefault
        {
            get { return isDefault; }
            set { isDefault = value; }
        }


        #endregion

        #region Public Method(s)

        #endregion

        #region Protected Method(s)

        #endregion

        #region Private Method(s)



        #endregion

        #region Constructor(s)

        #endregion
    }

    /// <summary>
    /// A Class 'PrintJob' which is an entity with namespace Discovery.BusinessObjects
    /// The class is for holding the print job details.
    /// </summary>
    public class PrintJob
    {

        /// <summary>
        /// Gets or sets the document.
        /// </summary>
        /// <value>The document.</value>
        public string Document
        {
            get { return document; }
            set { document = value; }
        }


        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>The owner.</value>
        public string Owner
        {
            get { return owner; }
            set { owner = value; }
        }
        /// <summary>
        /// Gets or sets the name of the printer.
        /// </summary>
        /// <value>The name of the printer.</value>
        public string PrinterName
        {
            get { return printerName; }
            set { printerName = value; }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Gets or sets the caption.
        /// </summary>
        /// <value>The caption.</value>
        public string Caption
        {
            get { return caption; }
            set { caption = value; }
        }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>The color.</value>
        public string Color
        {
            get { return color; }
            set { color = value; }
        }

        /// <summary>
        /// Gets or sets the type of the data.
        /// </summary>
        /// <value>The type of the data.</value>
        public string DataType
        {
            get { return dataType; }
            set { dataType = value; }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// Gets or sets the name of the driver.
        /// </summary>
        /// <value>The name of the driver.</value>
        public string DriverName
        {
            get { return driverName; }
            set { driverName = value; }
        }

        /// <summary>
        /// Gets or sets the elapsed time.
        /// </summary>
        /// <value>The elapsed time.</value>
        public DateTime? ElapsedTime
        {
            get { return elapsedTime; }
            set { elapsedTime = value; }
        }

        /// <summary>
        /// Gets or sets the host print queue.
        /// </summary>
        /// <value>The host print queue.</value>
        public string HostPrintQueue
        {
            get { return hostPrintQueue; }
            set { hostPrintQueue = value; }
        }

        /// <summary>
        /// Gets or sets the install date.
        /// </summary>
        /// <value>The install date.</value>
        public DateTime? InstallDate
        {
            get { return installDate; }
            set { installDate = value; }
        }

        /// <summary>
        /// Gets or sets the job id.
        /// </summary>
        /// <value>The job id.</value>
        public uint? JobId
        {
            get { return jobId; }
            set { jobId = value; }
        }

        /// <summary>
        /// Gets or sets the job status.
        /// </summary>
        /// <value>The job status.</value>
        public string JobStatus
        {
            get { return jobStatus; }
            set { jobStatus = value; }
        }

        /// <summary>
        /// Gets or sets the notify.
        /// </summary>
        /// <value>The notify.</value>
        public string Notify
        {
            get { return notify; }
            set { notify = value; }
        }

        /// <summary>
        /// Gets or sets the pages printed.
        /// </summary>
        /// <value>The pages printed.</value>
        public uint? PagesPrinted
        {
            get { return pagesPrinted; }
            set { pagesPrinted = value; }
        }

        /// <summary>
        /// Gets or sets the length of the paper.
        /// </summary>
        /// <value>The length of the paper.</value>
        public uint? PaperLength
        {
            get { return paperLength; }
            set { paperLength = value; }
        }

        /// <summary>
        /// Gets or sets the size of the paper.
        /// </summary>
        /// <value>The size of the paper.</value>
        public string PaperSize
        {
            get { return paperSize; }
            set { paperSize = value; }
        }

        /// <summary>
        /// Gets or sets the width of the paper.
        /// </summary>
        /// <value>The width of the paper.</value>
        public uint? PaperWidth
        {
            get { return paperWidth; }
            set { paperWidth = value; }
        }

        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        /// <value>The parameters.</value>
        public string Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }

        /// <summary>
        /// Gets or sets the print processor.
        /// </summary>
        /// <value>The print processor.</value>
        public string PrintProcessor
        {
            get { return printProcessor; }
            set { printProcessor = value; }
        }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>The priority.</value>
        public uint? Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>The size.</value>
        public uint? Size
        {
            get { return size; }
            set { size = value; }
        }

        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>The start time.</value>
        public DateTime? StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        /// <summary>
        /// Gets or sets the status mask.
        /// </summary>
        /// <value>The status mask.</value>
        public uint? StatusMask
        {
            get { return statusMask; }
            set { statusMask = value; }
        }

        /// <summary>
        /// Gets or sets the time submitted.
        /// </summary>
        /// <value>The time submitted.</value>
        public DateTime? TimeSubmitted
        {
            get { return timeSubmitted; }
            set { timeSubmitted = value; }
        }

        /// <summary>
        /// Gets or sets the total pages.
        /// </summary>
        /// <value>The total pages.</value>
        public uint? TotalPages
        {
            get { return totalPages; }
            set { totalPages = value; }
        }

        /// <summary>
        /// Gets or sets the until time.
        /// </summary>
        /// <value>The until time.</value>
        public DateTime? UntilTime
        {
            get { return untilTime; }
            set { untilTime = value; }
        }

        private string name;
        private string document;

        private string owner;
        private string printerName;
        private string caption;
        private string color;
        private string dataType;
        private string description;
        private string driverName;
        private DateTime? elapsedTime;
        private string hostPrintQueue;
        private DateTime? installDate;
        private UInt32? jobId;
        private string jobStatus;
        private string notify;
        private UInt32? pagesPrinted;
        private UInt32? paperLength;
        private string paperSize;
        private UInt32? paperWidth;
        private string parameters;
        private string printProcessor;
        private UInt32? priority;
        private UInt32? size;
        private DateTime? startTime;
        private string status;
        private UInt32? statusMask;
        private DateTime? timeSubmitted;
        private UInt32? totalPages;
        private DateTime? untilTime;
    }
}