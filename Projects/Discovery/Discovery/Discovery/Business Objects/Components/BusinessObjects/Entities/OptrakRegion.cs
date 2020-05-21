using System;
using ValidationFramework;

namespace Discovery.BusinessObjects
{

    /*************************************************************************************************
 ** CLASS:	OptrakRegion
 **
 ** OVERVIEW:
 ** This class represents a region that is used to group several Warehouses together when optraking.
 **
 ** MODIFICATION HISTORY:
 **
 ** Date:		Version:    Who:	Change:
 ** 19/7/06	    1.0			PJN		Initial Version
 ************************************************************************************************/
    /// <summary>
    /// A Class 'OptrakRegion' which is an entity with namespace Discovery.BusinessObjects
    /// It is inherited from PersistableBusinessObject
    /// The class holds the region code and its description
    /// </summary>
    [Serializable]
    public class OptrakRegion : PersistableBusinessObject
    {
        #region Private Fields

        private string code;
        private string description;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the OptrakRegion Short Code.
        /// </summary>
        /// <value>The code.</value>
        [RequiredValidator("Code is required.","*")]
        [LengthValidator(1, "The maximum length of a Code is 1 character.","*")]
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        /// <summary>
        /// Gets or sets The region description, i.e. Anglia.
        /// </summary>
        /// <value>The description.</value>
        [RequiredValidator("Description is required.","*")]
        [LengthValidator(256, "The maximum length of a Description is 256 characters.","*")]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string CodeAndDescription
        {
            get { return string.Concat("(",Code,")",description); }
            
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