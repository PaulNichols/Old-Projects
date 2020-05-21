using System;
using System.Collections.Generic;
using System.Text;
using ValidationFramework;

namespace Discovery.BusinessObjects
{
    /// <summary>
    /// A Class 'NonWorkingDay' which is an entity with namespace Discovery.BusinessObjects
    /// It is inherited from PersistableBusinessObject
    /// The class holds the non-working date within the region and warehouse
    /// </summary>
    [Serializable]
    public class NonWorkingDay : PersistableBusinessObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:NonWorkingDay"/> class.
        /// </summary>
        public NonWorkingDay()
            : base()
        {

        }

        #region Private Fields

        private DateTime nonWorkingDate;
        private string description;
        //private OptrakRegion region;
        //private int regionId;
        private string warehouseCode;
        private int warehouseId;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the non working date.
        /// </summary>
        /// <value>The non working date.</value>
        public DateTime NonWorkingDate
        {
            get { return nonWorkingDate; }
            set { nonWorkingDate = value; }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [RequiredValidator("Description is required.", "*")]
        [LengthValidator(256, "The maximum length of a Description is 256 characters.", "*")]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        //public int RegionId
        //{
        //    get { return regionId; }
        //    set { regionId = value; }
        //}

        //public OptrakRegion OptrakRegion
        //{
        //    get { return region; }
        //    set { region = value; }
        //}

        /// <summary>
        /// Gets or sets the warehouse id.
        /// </summary>
        /// <value>The warehouse id.</value>
        public int WarehouseId
        {
            get { return warehouseId; }
            set { warehouseId = value; }
        }

        /// <summary>
        /// Gets or sets the warehouse code.
        /// </summary>
        /// <value>The warehouse code.</value>
        public string WarehouseCode
        {
            get { return warehouseCode; }
            set { warehouseCode = value; }
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
