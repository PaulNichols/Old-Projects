/*************************************************************************************************
 ** FILE:	TransactionType.cs
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
   ** This is the TransactionType business object
   **
   ** MODIFICATION HISTORY:
   **
   ** Date:		Version:    Who:	Change:
   ** 19/7/06	    1.0		LAS		Initial Version
   ************************************************************************************************/
    /// <summary>
    /// A Class 'TransactionType' which is an entity with namespace Discovery.BusinessObjects
    /// It is inherited from PersistableBusinessObject
    /// The class holds the transaction type code and description
    /// </summary>
    [Serializable]
    public class TransactionType : PersistableBusinessObject
    {
        #region Private Fields

        private string code;
        private string description;
        private bool isStock;
        private bool isNonStock;
        private bool isCollection;
        private bool isSample;

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
        /// Gets or sets a value indicating whether this instance is Stock.
        /// </summary>
        /// <value><c>true</c> if this instance is return; otherwise, <c>false</c>.</value>
        public bool IsStock
        {
            get { return isStock; }
            set { isStock = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is Non Stock.
        /// </summary>
        /// <value><c>true</c> if this instance is return; otherwise, <c>false</c>.</value>
        public bool IsNonStock
        {
            get { return isNonStock; }
            set { isNonStock = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is a TDC Collection.
        /// </summary>
        /// <value><c>true</c> if this instance is return; otherwise, <c>false</c>.</value>
        public bool IsCollection
        {
            get { return isCollection; }
            set { isCollection = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is sample.
        /// </summary>
        /// <value><c>true</c> if this instance is sample; otherwise, <c>false</c>.</value>
        public bool IsSample
        {
            get { return isSample; }
            set { isSample = value; }
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