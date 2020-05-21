using System;
using ValidationFramework;

namespace Discovery.BusinessObjects
{
    /*************************************************************************************************
  ** CLASS:	OpCo
  **
  ** OVERVIEW:
  ** This is the OpCo business object
  **
  ** MODIFICATION HISTORY:
  **
  ** Date:		Version:    Who:	Change:
  ** 19/7/06	    1.0			PJN		Initial Version
  ************************************************************************************************/
    /// <summary>
    /// A Class 'OpCo' which is an entity with namespace Discovery.BusinessObjects
    /// It is inherited from PersistableBusinessObject
    /// The class holds the FTP details of an OpCo
    /// </summary>
    [Serializable]
    public class OpCo : PersistableBusinessObject
    {
        
    #region Private Fields

        private string ftpIP;
        private string ftpPassword;
        private string ftpUserName;
        private string code;
        private string description;
       
      

    #endregion

    #region Public Properties

        /// <summary>
        /// Gets or sets the FTP IP. The IP Address (used by the integration services) of the FTP site to retrieve OpCo messages from.
        /// </summary>
        /// <value>The FTP IP.</value>
        public string FtpIP
        {
            get 
            { 
                return ftpIP; 
            }
            set 
            { 
                ftpIP = value; 
            }
        }

        /// <summary>
        /// Gets or sets the FTP password. The password credential required for accessing the FTP site.
        /// </summary>
        /// <value>The FTP password.</value>
        public string FtpPassword
        {
            get 
            { 
                return ftpPassword; 
            }
            set 
            { 
                ftpPassword = value; 
            }
        }

        /// <summary>
        /// Gets or sets the name of the FTP user. The user name credential required for accessing the FTP site.
        /// </summary>
        /// <value>The name of the FTP user.</value>
        public string FtpUserName
        {
            get 
            { 
                return ftpUserName; 
            }
            set 
            { 
                ftpUserName = value; 
            }
        }

        /// <summary>
        /// Gets or sets the the short code abbreviation for the OpCo. RHG, HSP or TPC
        /// </summary>
        /// <value>The code.</value>
        [RequiredValidator("Required Data: Code.", "*")]
        [LengthValidator(3, "Maxiumum Length Exceded: Code can be up to 3 characters.", "*")]
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
        /// Gets or sets the full name for the OpCo..
        /// </summary>
        /// <value>The description.</value>
        [RequiredValidator("Required Data: Description.", "*")]
        [LengthValidator(256, "Maxiumum Length Exceded: Description can be up to 256 characters.", "*")]
        public string Description
        {
            get 
            { 
                return description; 
            }
            set 
            { 
                description = value; 
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

        /// <summary>
        /// Initializes a new instance of the <see cref="T:OpCo"/> class.
        /// </summary>
        public OpCo()
            : base()
        {

        }

    #endregion

    }
}
