using System;
using ValidationFramework;

namespace Discovery.BusinessObjects
{

    /*************************************************************************************************
 ** CLASS:	Region
 **
 ** OVERVIEW:
 ** This class represents a region of the UK that a SalesLocation or Warehouse is located or a Non-Working Day is related to.
 **
 ** MODIFICATION HISTORY:
 **
 ** Date:		Version:    Who:	Change:
 ** 19/7/06	    1.0			PJN		Initial Version
 ************************************************************************************************/
    /// <summary>
    /// A Class 'Region' which is an entity with namespace Discovery.BusinessObjects
    /// It is inherited from PersistableBusinessObject
    /// The class holds the region code and its description
    /// </summary>
    [Serializable]
    public class Region : PersistableBusinessObject
    {
        #region Private Fields

        private string code;
        private string description;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the Region Short Code.
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
        /// Gets or sets The region description, i.e. Scotland.
        /// </summary>
        /// <value>The description.</value>
        [RequiredValidator("Description is required.","*")]
        [LengthValidator(256, "The maximum length of a Description is 256 characters.","*")]
        public string Description
        {
            get { return description; }
            set { description = value; }
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