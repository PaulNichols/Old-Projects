using System;
using ValidationFramework;

namespace Discovery.BusinessObjects
{
    /*************************************************************************************************
  ** CLASS:	Warehouse
  **
  ** OVERVIEW:
  ** This is the Warehouse business object
  **
  ** MODIFICATION HISTORY:
  **
  ** Date:		Version:    Who:	Change:
  ** 19/7/06	    1.0			PJN		Initial Version
  ************************************************************************************************/
    /// <summary>
    /// A class 'Warehouse' which is an entity with namespace Discovery.BusinessObjects
    /// It is inherited from PersistableBusinessObject
    /// The class holds the warehouse code, description, contact person, optrak or not, etc...
    /// </summary>

    [Serializable]
    public class Warehouse : PersistableBusinessObject
    {
        #region Private Fields

        
        private string code;
        private string description;
        private Contact contact;
        private bool hasOptrak;
        private bool hasCommander;
        private string printerName = "";
        private Address address;
        private bool isTDC;
        private OptrakRegion optrakRegion;
        private int regionId;
        //private Printer printer;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        [RequiredValidator("Required Data: Code.", "*")]
        [LengthValidator(10, "Maxiumum Length Exceded: Code can be up to 10 characters.", "*")]
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
        /// Gets or sets the description.
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

        /// <summary>
        /// Gets the code and description.
        /// </summary>
        /// <value>The code and description.</value>
        public string CodeAndDescription
        {
            get
            {
                return string.Concat("(",Code,") ",Description);
            }
           
        }

      

        /// <summary>
        /// Gets or sets the contact details.
        /// </summary>
        /// <value>The contact.</value>
        public Contact Contact
        {
            get
            {
                return contact;
            }
            set
            {
                contact = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this warehouse has optrak.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has optrak; otherwise, <c>false</c>.
        /// </value>
        public bool HasOptrak
        {
            get { return hasOptrak; }
            set { hasOptrak = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this warehouse has commander.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has commander; otherwise, <c>false</c>.
        /// </value>
        public bool HasCommander
        {
            get { return hasCommander; }
            set { hasCommander = value; }
        }

      

        /// <summary>
        /// Gets or sets the address of the warehouse.
        /// </summary>
        /// <value>The address.</value>
        public Address Address
        {
            get { return address; }
            set { address = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this is a TDC warehouse.
        /// </summary>
        /// <value><c>true</c> if this instance is TDC; otherwise, <c>false</c>.</value>
        public bool IsTDC
        {
            get { return isTDC; }
            set { isTDC = value; }
        }

        /// <summary>
        /// Gets or sets the the unique identifier of the <see cref="T:OptrakRegion"/> this location is found in.
        /// </summary>
        /// <value>The optrakRegion id.</value>
        [RequiredValidator("OptrakRegion is required.", "*")]
        public int RegionId
        {
            get { return regionId; }
            set { regionId = value; }
        }

        /// <summary>
        /// Gets or sets a <see cref="T:OptrakRegion"/> object populated from the RegionId..
        /// </summary>
        /// <value>The <see cref="T:OptrakRegion"/>.</value>
        public OptrakRegion OptrakRegion
        {
            get { return optrakRegion; }
            set { optrakRegion = value; }
        }

        /// <summary>
        /// Gets or sets the printer at the warehouse.
        /// </summary>
        /// <value>The printer.</value>
        //public Printer Printer
        //{
        //    get { return printer; }
        //    set { printer = value; }
        //}

        /// <summary>
        /// Gets or sets the name of the printer which is for printing delivery notes
        /// </summary>
        /// <value>The name of the printer.</value>
        [LengthValidator(0, 50, "Printer must be between {0} and {1} characters in length.", "*")]
        [RegexValidator("(^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$)|^([^.]*)$", "printer should be in IP address format or the printer name", "*")]
        public string PrinterName
        {
            get { return printerName; }
            set { printerName = value; }
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
        /// Initializes a new instance of the <see cref="T:Warehouse"/> class.
        /// </summary>
   public Warehouse()
            : base()
        {
            //initialise the child objects
            Contact=new Contact();
            OptrakRegion=new OptrakRegion();
            Address=new Address();
        }
        #endregion

      
    }
}