using System;
using ValidationFramework;

namespace Discovery.BusinessObjects
{
    /*************************************************************************************************
     ** CLASS:	Route
     **
     ** OVERVIEW:
     ** This class is the business object for a single Route
     **
     ** MODIFICATION HISTORY:
     **
     ** Date:		Version:    Who:	Change:
     ** 19/7/06	    1.0			PJN		Initial Version
     ************************************************************************************************/
    /// <summary>
    /// A Class 'Route' which is an entity with namespace Discovery.BusinessObjects
    /// It is inherited from PersistableBusinessObject
    /// The class holds the route code, its description and the type of the route
    /// </summary>
    [Serializable]
    public class Route : PersistableBusinessObject
    {
        #region Private Fields
        
        private string code = "";
        private string description = "";
        private bool isSameDay = false;
        private bool isSpecial = false;
        private bool isNextDay = false;
        private bool isCollection = false;
        private int warehouseId = -1;
        private Warehouse warehouse = null;
        
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance is special.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is special; otherwise, <c>false</c>.
        /// </value>
        public bool IsSpecial
        {
            get { return isSpecial; }
            set { isSpecial = value; }
        }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        [RequiredValidator("Code is required.", "*")]
        [LengthValidator(10, "The maximum length of a Code is 10 characters.", "*")]
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        //public string WarehouseAndCode
        //{
        //    get
        //    {
        //        return "";
        //        return string.Concat(Warehouse.Code ,"-",code);
        //    }
        //}

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [RequiredValidator("Description is required.","*")]
        [LengthValidator(512, "The maximum length of a Description is 512 characters.", "*")]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is same day.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is same day; otherwise, <c>false</c>.
        /// </value>
        public bool IsSameDay
        {
            get { return isSameDay; }
            set { isSameDay = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is next day.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is next day; otherwise, <c>false</c>.
        /// </value>
        public bool IsNextDay
        {
            get { return isNextDay; }
            set { isNextDay = value; }
        }


        /// <summary>
        /// Gets or sets a value indicating whether this instance is collection.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is collection; otherwise, <c>false</c>.
        /// </value>
        public bool IsCollection
        {
            get { return isCollection; }
            set { isCollection = value; }
        }

        public int WarehouseId
        {
            get { return warehouseId; }
            set { warehouseId = value; }
        }

        public Warehouse Warehouse
        {
            get { return warehouse; }
            set { warehouse = value; }
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