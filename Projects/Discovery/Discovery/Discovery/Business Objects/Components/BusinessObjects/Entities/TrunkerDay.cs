using System;
using ValidationFramework;

namespace Discovery.BusinessObjects
{
    /*************************************************************************************************
     ** CLASS:	TrunkerDay
     **
     ** OVERVIEW:
     ** This is the TrunkerDay business object
     **
     ** MODIFICATION HISTORY:
     **
     ** Date:		Version:    Who:	Change:
     ** 19/7/06	    1.0			PJN		Initial Version
     ************************************************************************************************/
    /// <summary>
    /// A Class 'TrunkerDay' which is an entity with namespace Discovery.BusinessObjects
    /// It is inherited from PersistableBusinessObject
    /// The class holds the number of days required to perform the trunking from the source warehouse to the destination warehouse
    /// </summary>
    [Serializable]
    public class TrunkerDay : PersistableBusinessObject
    {
        #region Private Fields
        
        private Warehouse sourceWarehouse;
        private int sourceWarehouseId;
        private Warehouse destinationWarehouse;
        private int destinationWarehouseId;
        private int days;
        
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the source warehouse.
        /// </summary>
        /// <value>The source location.</value>
        public Warehouse SourceWarehouse
        {
            get
            {
                return sourceWarehouse;
            }
            set
            {
                sourceWarehouse = value;
            }
        }

        /// <summary>
        /// Gets or sets the destination warehouse.
        /// </summary>
        /// <value>The destination location.</value>
        public Warehouse DestinationWarehouse
        {
            get
            {
                return destinationWarehouse;
            }
            set
            {
                destinationWarehouse = value;
            }
        }

        /// <summary>
        /// Gets or sets the number of days.
        /// </summary>
        /// <value>The days.</value>
        [RequiredValidator("Number of Days is Required.", "*")]
        public int Days
        {
            get
            {
                return days;
            }
            set
            {
                days = value;
            }
        }

      

        /// <summary>
        /// Custom validation method to compare the warehouses and check they are not the same
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:ValidationFramework.CustomValidationEventArgs"/> instance containing the event data.</param>
        public void CompareWarehouses(object sender, CustomValidationEventArgs e)
        {
            e.IsValid= (SourceWarehouseId != DestinationWarehouseId);
            e.ErrorMessage = "The Source and Destination Warehouses cannot be the same.";
        }
        
        /// <summary>
        /// Gets or sets the source warehouse id.
        /// </summary>
        /// <value>The source warehouse id.</value>
        [CompareValidator(-1,ValidationCompareOperator.NotEqual,"Source Warehouse is Required.", "*")]
        [CustomValidator("", "CompareWarehouses", "*")]
        public int SourceWarehouseId
        {
            get
            {
                return sourceWarehouseId;
            }
            set
            {
                sourceWarehouseId = value;
            }
        }

        /// <summary>
        /// Gets or sets the destination warehouse id.
        /// </summary>
        /// <value>The destination warehouse id.</value>
        [CompareValidator(-1, ValidationCompareOperator.NotEqual, "Destination Warehouse is Required.", "*")]
        [CustomValidator("", "CompareWarehouses", "*")]
        public int DestinationWarehouseId
        {
            get
            {
                return destinationWarehouseId;
            }
            set
            {
                destinationWarehouseId = value;
            }
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
}