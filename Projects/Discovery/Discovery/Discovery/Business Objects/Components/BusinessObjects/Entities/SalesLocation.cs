/*************************************************************************************************
 ** FILE:	SalesLocation.cs
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
using ValidationFramework;

namespace Discovery.BusinessObjects
{
    /*************************************************************************************************
     ** CLASS:	TransactionType
     **
     ** OVERVIEW:
     ** A location represents either a OpCo/TDC Stock or delivery location.
     **
     ** MODIFICATION HISTORY:
     **
     ** Date:		Version:    Who:	Change:
     ** 19/7/06	    1.0			LAS		Initial Version
     ************************************************************************************************/

    /// <summary>
    /// A Class 'SalesLocation' which is an entity with namespace Discovery.BusinessObjects
    /// It is inherited from PersistableBusinessObject
    /// The class holds the sales location code and its description
    /// </summary>
    [Serializable]
    public class SalesLocation : PersistableBusinessObject
    {
        #region Private Fields

        private string location;
        private string description;
        private string telephoneNumber;
        private int opCoId;
        private OpCo opCo;
        
        #endregion

        #region Public Properties

      
        /// <summary>
        /// Gets or sets the description of the <see cref="T:SalesLocation"/> .
        /// </summary>
        /// <value>The description.</value>
        [RequiredValidator("Description is required.", "*")]
        [LengthValidator(256, "The maximum length of a Description is 256 characters.", "*")]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>The location.</value>
        [RequiredValidator("Location is required.", "*")]
        [LengthValidator(50, "The maximum length of a Location is 50 characters.", "*")]
        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        public string LocationDescription
        {
            get { return string.Concat(location, " - ", Description); }
        }

        /// <summary>
        /// Gets or sets the telephone number.
        /// </summary>
        /// <value>The telephone number.</value>
        [LengthValidator(20, "The maximum length of the Telephone Number is 20 characters.", "*")]
        [RegexValidator("^([0-9,\\s])*", "Telephone should be in numeric", "*")]
        public string TelephoneNumber
        {
            get { return telephoneNumber; }
            set { telephoneNumber = value; }
        }

        /// <summary>
        /// Gets or sets the opco id.
        /// </summary>
        /// <value>The op co id.</value>
        public int OpCoId
        {
            get { return opCoId; }
            set { opCoId = value; }
        }

        /// <summary>
        /// Gets or sets the opco.
        /// </summary>
        /// <value>The op co.</value>
        public OpCo OpCo
        {
            get { return opCo; }
            set { opCo = value; }
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
