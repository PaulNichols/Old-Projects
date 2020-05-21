using System;
using ValidationFramework;

namespace Discovery.BusinessObjects
{
    /*************************************************************************************************
    ** CLASS:	Address
    **
    ** OVERVIEW:
    ** This class encapsulates an address without being specific of what type of address it represents. 
    ** Therefore it can be used to hold both Shipment and Customer addresses.
    **
    ** MODIFICATION HISTORY:
    **
    ** Date:		Version:    Who:	Change:
    ** 19/7/06	    1.0			PJN		Initial Version
    ************************************************************************************************/
    /// <summary>
    /// A Class 'Address' which is an entity with namespace Discovery.BusinessObjects
    /// It is inherited from DiscoveryBusinessObject
    /// The class holds the address details
    /// </summary>
    [Serializable]
    public class Address : DiscoveryBusinessObject
    {
        #region Protected Fields

        /// <summary>
        /// A protected string 'line1'
        /// </summary>
        protected string line1;
        /// <summary>
        /// A protected string 'line2'
        /// </summary>
        protected string line2;
        /// <summary>
        /// A protected string 'line3'
        /// </summary>
        protected string line3;
        /// <summary>
        /// A protected string 'line4'
        /// </summary>
        protected string line4;
        /// <summary>
        /// A protected string 'line5'
        /// </summary>
        protected string line5;
        /// <summary>
        /// A protected string 'postCode'
        /// </summary>
        protected string postCode;

        #endregion

        #region Public Properties

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </returns>
        public override string ToString()
        {
            string seperator = ",";
            return
                string.Concat(line1, seperator, line2, seperator, line3, seperator, line4, seperator, line5, seperator,
                              postCode);
        }

        /// <summary>
        ///  Line 1
        /// </summary>
        public virtual string Line1
        {
            get { return line1; }
            set { line1 = value; }
        }

        /// <summary>
        ///  Line 2
        /// </summary>
        public virtual string Line2
        {
            get { return line2; }
            set { line2 = value; }
        }

        /// <summary>
        ///  Line 3
        /// </summary>
        public virtual string Line3
        {
            get { return line3; }
            set { line3 = value; }
        }

        /// <summary>
        ///  Line 4
        /// </summary>
        public virtual string Line4
        {
            get { return line4; }
            set { line4 = value; }
        }

        /// <summary>
        ///  Line 5
        /// </summary>
        public virtual string Line5
        {
            get { return line5; }
            set { line5 = value; }
        }

        /// <summary>
        /// Post Code
        /// </summary>
        public virtual string PostCode
        {
            get { return postCode; }
            set { postCode = value.ToUpper(); }
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
        /// Initializes a new instance of the <see cref="T:Address"/> class.
        /// </summary>
        public Address()
        {
            // Seed default values
            line1 = "";
            line2 = "";
            line3 = "";
            line4 = "";
            line5 = "";
            postCode = "";
        }

        #endregion
    }

    /*************************************************************************************************
    ** CLASS:	PAFAddress
    **
    ** OVERVIEW:
    ** This class is an extension of an address which also holds values such as DPS value after an address
    ** has been "through" quick address
    **
    ** MODIFICATION HISTORY:
    **
    ** Date:		Version:    Who:	Change:
    ** 19/7/06	    1.0			PJN		Initial Version
    ************************************************************************************************/
    /// <summary>
    /// A Class 'PAFAddress' which is an entity with namespace Discovery.BusinessObjects
    /// It is inherited from Address
    /// </summary>
    [Serializable]
    public class PAFAddress : Address
    {
        #region Private Fields

        private string match;
        private int easting;
        private int northing;
        private string dps;
        private int location;

        private PAFStatusEnum pafStatus;
        /// <summary>
        /// An enum 'PAFStatusEnum'
        /// </summary>
        public enum PAFStatusEnum
        {
            /// <summary>
            /// An enum value 'Unknown'
            /// </summary>
            Unknown,
            /// <summary>
            /// An enum value 'NeedsChecking'
            /// </summary>
            Needs_Checking = 1,
            /// <summary>
            /// An enum value 'GoodMatch'
            /// </summary>
            Good_Match
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the match.
        /// </summary>
        /// <value>The match.</value>
        public string Match
        {
            get { return match; }
            set { match = value.ToUpper(); }
        }

        /// <summary>
        /// Gets or sets the easting.
        /// </summary>
        /// <value>The easting.</value>
        public int Easting
        {
            get { return easting; }
            set { easting = value; }
        }

        /// <summary>
        /// Gets or sets the northing.
        /// </summary>
        /// <value>The northing.</value>
        public int Northing
        {
            get { return northing; }
            set { northing = value; }
        }

        /// <summary>
        /// Gets or sets the DPS.
        /// </summary>
        /// <value>The DPS.</value>
        public string DPS
        {
            get { return dps; }
            set { dps = value; }
        }

        /// <summary>
        /// not sure if this should be used?
        /// </summary>
        /// <value>The location.</value>
        public int Location
        {
            get { return location; }
            set { location = value; }
        }


        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>The status.</value>
        public PAFStatusEnum Status
        {
            get
            {
                switch (match)
                {
                    case "R9":

                    case "R5":

                    case "P9":

                    case "P5":

                    case "O9":
                        pafStatus = PAFStatusEnum.Good_Match;
                        break;
                    default:
                        pafStatus = PAFStatusEnum.Needs_Checking;
                        break;
                }
                return pafStatus;
            }

        }



        #endregion

        #region Public Method(s)
        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </returns>
        public override string ToString()
        {
            string seperator = ",";
            return
                string.Concat(line1, seperator, line2, seperator, line3, seperator, line4, seperator, line5, seperator,
                              postCode, seperator, DPS, seperator, Northing.ToString(), Easting.ToString());
        }
        #endregion

        #region Protected Method(s)

        #endregion

        #region Private Method(s)

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PAFAddress"/> class.
        /// </summary>
        public PAFAddress()
            : base()
        {
            // Seed default values
            match = "";
            easting = 0;
            northing = 0;
            dps = "";
            location = 0;
        }

        #endregion
    }

    /*************************************************************************************************
    ** CLASS:	CustomerAddress
    **
    ** OVERVIEW:
    ** This class encapsulates an address without being specific of what type of address it represents. 
    ** Therefore it can be used to hold both Shipment and Customer addresses.
    **
    ** MODIFICATION HISTORY:
    **
    ** Date:		Version:    Who:	Change:
    ** 19/7/06	    1.0			LAS		Initial Version
    ************************************************************************************************/
    /// <summary>
    /// A Class 'Address' which is an entity with namespace Discovery.BusinessObjects
    /// It is inherited from DiscoveryBusinessObject
    /// The class holds the address details
    /// </summary>
    [Serializable]
    public class CustomerAddress : Address
    {
        #region Private Fields
        #endregion

        #region Public Properties

        /// <summary>
        ///  Line 1
        /// </summary>
       //Technical Spec v1.6 altered this property to non-mandatory  [RequiredValidator("Customer Address Line 1 is required.", "*")]
        [LengthValidator(0, 50, "Customer Address Line 1 must be between {0} and {1} characters in length.", "*")]
        public override string Line1
        {
            get { return base.line1; }
            set { base.line1 = value; }
        }

        /// <summary>
        ///  Line 2
        /// </summary>
        [LengthValidator(0, 50, "Customer Address Line 2 must be between {0} and {1} characters in length.", "*")]
        public override string Line2
        {
            get { return base.line2; }
            set { base.line2 = value; }
        }

        /// <summary>
        ///  Line 3
        /// </summary>
        [LengthValidator(0, 50, "Customer Address Line 3 must be between {0} and {1} characters in length.", "*")]
        public override string Line3
        {
            get { return base.line3; }
            set { base.line3 = value; }
        }

        /// <summary>
        ///  Line 4
        /// </summary>
        [LengthValidator(0, 50, "Customer Address Line 4 must be between {0} and {1} characters in length.", "*")]
        public override string Line4
        {
            get { return base.line4; }
            set { base.line4 = value; }
        }

        /// <summary>
        ///  Line 5
        /// </summary>
        [LengthValidator(0, 50, "Customer Address Line 5 must be between {0} and {1} characters in length.", "*")]
        public override string Line5
        {
            get { return base.line5; }
            set { base.line5 = value; }
        }

        /// <summary>
        ///  Post Code
        /// </summary>
        //[RegexValidator(@"^(([A-Z]{1,2}[0-9]{1,2})|([A-Z]{1,2}[0-9][A-Z]))\s?([0-9][A-Z]{2})$", "Customer Post Code must be a valid format.", "*")]
        public override string PostCode
        {
            get { return base.postCode; }
            set { base.postCode = value; }
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

    /*************************************************************************************************
    ** CLASS:	ShipmentAddress
    **
    ** OVERVIEW:
    ** This class encapsulates an address without being specific of what type of address it represents. 
    ** Therefore it can be used to hold both Shipment and Shipment addresses.
    **
    ** MODIFICATION HISTORY:
    **
    ** Date:		Version:    Who:	Change:
    ** 19/7/06	    1.0			LAS		Initial Version
    ************************************************************************************************/
    /// <summary>
    /// A Class 'Address' which is an entity with namespace Discovery.BusinessObjects
    /// It is inherited from DiscoveryBusinessObject
    /// The class holds the address details
    /// </summary>
    [Serializable]
    public class ShipmentAddress : Address
    {
        #region Private Fields
        #endregion

        #region Public Properties

        /// <summary>
        ///  Line 1
        /// </summary>
        [LengthValidator(0, 50, "Shipment Address Line 1 must be between {0} and {1} characters in length.", "*")]//as per //Technical Spec v1.6 
        public override string Line1
        {
            get { return base.line1; }
            set { base.line1 = value; }
        }

        /// <summary>
        ///  Line 2
        /// </summary>
        [LengthValidator(0, 50, "Shipment Address Line 2 must be between {0} and {1} characters in length.", "*")]
        public override string Line2
        {
            get { return base.line2; }
            set { base.line2 = value; }
        }

        /// <summary>
        ///  Line 3
        /// </summary>
        [LengthValidator(0, 50, "Shipment Address Line 3 must be between {0} and {1} characters in length.", "*")]
        public override string Line3
        {
            get { return base.line3; }
            set { base.line3 = value; }
        }

        /// <summary>
        ///  Line 4
        /// </summary>
        [LengthValidator(0, 50, "Shipment Address Line 4 must be between {0} and {1} characters in length.", "*")]
        public override string Line4
        {
            get { return base.line4; }
            set { base.line4 = value; }
        }

        /// <summary>
        ///  Line 5
        /// </summary>
        [LengthValidator(0, 50, "Shipment Address Line 5 must be between {0} and {1} characters in length.", "*")]
        public override string Line5
        {
            get { return base.line5; }
            set { base.line5 = value; }
        }

        /// <summary>
        ///  Post Code
        /// </summary>
        // Technical Spec v1.6 altered this property to non-mandatory  [RequiredValidator("Shipment Address Post Code is required.", "*")]
        [RegexValidator(@"^(([A-Z]{1,2}[0-9]{1,2})|([A-Z]{1,2}[0-9][A-Z]))\s?([0-9][A-Z]{2})$", "Shipment Post Code must be a valid format.", "*")]
        public override string PostCode
        {
            get { return base.postCode; }
            set { base.postCode = value; }
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