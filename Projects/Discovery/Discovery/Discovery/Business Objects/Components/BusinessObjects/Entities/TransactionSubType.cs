/*************************************************************************************************
 ** FILE:	TransactionSubType.cs
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
    ** CLASS:	TransactionSubType
    **
    ** OVERVIEW:
    ** This is the TransactionType business object
    **
    ** MODIFICATION HISTORY:
    **
    ** Date:		Version:    Who:	Change:
    ** 19/7/06	    1.0			LAS		Initial Version
    ************************************************************************************************/
    /// <summary>
    /// A Class 'TransactionSubType' which is an entity with namespace Discovery.BusinessObjects
    /// It is inherited from PersistableBusinessObject
    /// The class holds the sub-type code and description
    /// </summary>
    [Serializable]
    public class TransactionSubType : PersistableBusinessObject
    {
        #region Private Fields

        private string code;
        private string description;
        private bool isNormal;
        private bool isTransfer;
        private bool isLocalConversion;
        private bool is3rdPartyConversion;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        [RequiredValidator("Code is required.","*")]
        [LengthValidator(10, "The maximum length of a Code is 10 characters.","*")]
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [RequiredValidator("Description is required.","*")]
        [LengthValidator(256, "The maximum length of a Description is 256 characters.","*")]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// Gets or sets the is normal boolean value.
        /// </summary>
        /// <value>True or false.</value>
        public bool IsNormal
        {
            get { return isNormal; }
            set { isNormal = value; }
        }

        /// <summary>
        /// Gets or sets the is transfer boolean value.
        /// </summary>
        /// <value>True or false.</value>
        public bool IsTransfer
        {
            get { return isTransfer; }
            set { isTransfer = value; }
        }

        /// <summary>
        /// Gets or sets the is local conversion boolean value.
        /// </summary>
        /// <value>True or false.</value>
        public bool IsLocalConversion
        {
            get { return isLocalConversion; }
            set { isLocalConversion = value; }
        }

        /// <summary>
        /// Gets or sets the is 3rd party conversion boolean value.
        /// </summary>
        /// <value>True or false.</value>
        public bool Is3rdPartyConversion
        {
            get { return is3rdPartyConversion; }
            set { is3rdPartyConversion = value; }
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