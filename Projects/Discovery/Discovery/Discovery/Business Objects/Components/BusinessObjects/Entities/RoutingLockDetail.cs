using System;

namespace Discovery.BusinessObjects
{
    /*************************************************************************************************
  ** CLASS:	Trip
  **
  ** OVERVIEW:
  ** This class holds the details of who and when a optrak lock was placed on shipments for a particular
  ** warehouse
  **
  ** MODIFICATION HISTORY:
  **
  ** Date:		Version:    Who:	Change:
  ** 19/9/06	1.0			PJN		Initial Version
  ************************************************************************************************/

    /// <summary>
    /// A Class 'RoutingLockDetail' which is an entities
    ///
    /// </summary>
    [Serializable]
    public class RoutingLockDetail
    {
        #region Private Fields

        private string userName;
        private DateTime lockDate;
        private string status;
        private DateTime statusDate;
        private int regionId;

        #endregion

        #region Public Method(s)

        #endregion

        #region Protected Method(s)

        #endregion

        #region Private Method(s)

        #endregion

        #region Constructor(s)

        #endregion

         #region Public Properties
      

        #endregion

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        /// <summary>
        /// Gets or sets the lock date.
        /// </summary>
        /// <value>The lock date.</value>
        public DateTime LockDate
        {
            get { return lockDate; }
            set { lockDate = value; }
        }

        /// <summary>
        /// Gets or sets the warehouse.
        /// </summary>
        /// <value>The warehouse.</value>
        public int  RegionId
        {
            get { return regionId; }
            set { regionId = value; }
        }

        /// <summary>
        /// Gets or sets the status date.
        /// </summary>
        /// <value>The status date.</value>
        public DateTime StatusDate
        {
            get { return statusDate; }
            set { statusDate = value; }
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
    }
}
