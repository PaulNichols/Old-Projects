using System;
using System.Collections.Generic;
using System.Text;
using ValidationFramework;

namespace Discovery.BusinessObjects
{
    /// <summary>
    /// A Class 'LoadCategory' which is an entity with namespace Discovery.BusinessObjects
    /// It is inherited from PersistableBusinessObject
    /// The class holds the load category code and description
    /// </summary>
    [Serializable]
    public class LoadCategory : PersistableBusinessObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:LoadCategory"/> class.
        /// </summary>
        public LoadCategory()
            : base()
        {

        }

        #region Private fields

        private string code;
        private string description;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        [RequiredValidator("Code is required.", "*")]
        [LengthValidator(10, "The maximum length of a Code is 10 characters.", "*")]
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [RequiredValidator("Description is required.", "*")]
        [LengthValidator(256, "The maximum length of a Description is 256 characters.", "*")]
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
