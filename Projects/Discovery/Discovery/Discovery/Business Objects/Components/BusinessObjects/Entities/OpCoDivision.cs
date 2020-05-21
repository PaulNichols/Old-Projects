using System;
using System.Drawing;
using ValidationFramework;

namespace Discovery.BusinessObjects
{
    /*************************************************************************************************
  ** CLASS:	OpCoDivision
  **
  ** OVERVIEW:
  ** This is the OpCo Division business object
  **
  ** MODIFICATION HISTORY:
  **
  ** Date:		Version:    Who:	Change:
  ** 19/7/06	1.0	        PJN		Initial Version
  ************************************************************************************************/

    /// <summary>
    /// A Class 'OpCoDivision' which is an entity with namespace Discovery.BusinessObjects
    /// It is inherited from PersistableBusinessObject
    /// The class holds the OpCo's division code, logo and logo URI
    /// </summary>
    [Serializable]
    public class OpCoDivision : PersistableBusinessObject
    {
        
    #region Private Fields

      
        private string code;
        private int opCoId;
        private OpCo opCo;
        private byte[] logo;
        private string logoURI;

    #endregion

    #region Public Properties


        /// <summary>
        /// Gets or sets the the short code abbreviation for the OpCo. RHG, HSP or TPC
        /// </summary>
        /// <value>The code.</value>
        [RequiredValidator("Code is required.", "*")]
        [LengthValidator(10, "The maximum length of a Code is 10 characters.", "*")]
        public string Code
        {
            get 
            { 
                return code; 
            }
            set 
            { 
                code = value; 
            }
        }

        /// <summary>
        /// Gets or sets the op co id.
        /// </summary>
        /// <value>The op co id.</value>
        public int OpCoId
        {
            get { return opCoId; }
            set { opCoId = value; }
        }

        /// <summary>
        /// Gets or sets the op co.
        /// </summary>
        /// <value>The op co.</value>
        public OpCo OpCo
        {
            get { return opCo; }
            set { opCo = value; }
        }

        /// <summary>
        /// Gets or sets the logo.
        /// </summary>
        /// <value>The logo.</value>
        public byte[] Logo
        {
            get { return logo; }
            set { logo = value; }
        }

        /// <summary>
        /// Gets or sets the logo URI.
        /// </summary>
        /// <value>The logo URI.</value>
        public string LogoURI
        {
            get { return logoURI; }
            set { logoURI = value; }
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
