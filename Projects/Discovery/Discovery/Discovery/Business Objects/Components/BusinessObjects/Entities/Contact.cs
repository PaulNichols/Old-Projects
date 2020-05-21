/*************************************************************************************************
 ** FILE:	Contact.cs
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
     ** CLASS:	Contact
     **
     ** OVERVIEW:
     ** This class encapsulates contact details without being specific of what type of contact it represents. Therefore as an example it can be used to hold both Shipment and OpCo contact details.
     ** Not all properties of this class need to be populated, for example the OpCo contact telephone number may not be known or needed.</remarks>
     **
     ** MODIFICATION HISTORY:
     **
     ** Date:		Version:    Who:	Change:
     ** 19/7/06	    1.0			LAS		Initial Version
     ************************************************************************************************/

    /// <summary>
    /// A class 'Contact' which is an entity with namespace Discovery.BusinessObjects
    /// It is inherited from DiscoveryBusinessObject
    /// The class holds the contact person details (name, email and phone)
    /// </summary>
    [Serializable]
    public class Contact : DiscoveryBusinessObject
    {
        #region Private Fields

        private string name;
        private string email;
        private string telephoneNumber;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the name of the <see cref="T:Contact"/> .
        /// </summary>
        /// <value>A string that represents the <see cref="T:Contact"/> name.</value>
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Gets or sets the email of the <see cref="T:Contact"/> .
        /// </summary>
        /// <value>A string that represents the <see cref="T:Contact"/> email.</value>
        public virtual string Email
        {
            get { return email; }
            set { email = value; }
        }

        /// <summary>
        /// Gets or sets the telephone number of the <see cref="T:Contact"/> .
        /// </summary>
        /// <value>A string that represents the <see cref="T:Contact"/> telephone number.</value>
        [RegexValidator("^([0-9,\\s])*", "Telephone should be in numeric", "*")]
        public virtual string TelephoneNumber
        {
            get { return telephoneNumber; }
            set { telephoneNumber = value; }
        }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Contact"/> class.
        /// </summary>
        public Contact()
            : base()
        {
            name = "";
            email = "";
            telephoneNumber = "";
        }

        #endregion

        #region Public Method(s)

        #endregion

        #region Protected Method(s)

        #endregion

        #region Private Method(s)

        #endregion
    }
    
    [Serializable]
    public class OpCoContact : Contact
    {
        public OpCoContact()
            : base()
        {
        }

        /// <summary>
        /// Gets or sets the name of the <see cref="T:Contact"/> .
        /// </summary>
        /// <value>A string that represents the <see cref="T:Contact"/> name.</value>
        //Technical Spec v1.6 altered this property to non-mandatory [RequiredValidator("OpCo Contact Email is required.", "*")]
        [LengthValidator(0, 50, "OpCo Contact Email must be between {0} and {1} characters in length.", "*")]
        public override string Email
        {
            get { return base.Email; }
            set { base.Email = value; }
        }

        /// <summary>
        /// Gets or sets the name of the <see cref="T:Contact"/> .
        /// </summary>
        /// <value>A string that represents the <see cref="T:Contact"/> name.</value>
        //Technical Spec v1.6 altered this property to non-mandatory [RequiredValidator("OpCo Contact Name is required.", "*")]
        [LengthValidator(0, 30, "OpCo Contact Name must be between {0} and {1} characters in length.", "*")]
        public override string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }
    }
}