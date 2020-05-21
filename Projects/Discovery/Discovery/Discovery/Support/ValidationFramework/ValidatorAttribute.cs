#region Using Directives

using System;

#endregion

namespace ValidationFramework
{
    /// <summary>
    /// Base class for all valiator attributes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    [Serializable]
    public abstract class ValidatorAttribute : Attribute
    {
        #region Private Fields

        private bool isValid;

        //Make errMessage internal so that CustomValidatorAttribute can change it
        //but not protected because other sub-classed validators in external
        //assemblies should not be able to access it
        internal string errMessage;

        private PropertyInfoExtended propertyToValidate;
        internal string text;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the unique key ONLY, no Sets method
        /// </summary>
        /// <value>The unique key.</value>
        public virtual string UniqueKey
        {
            get { return propertyToValidate.Name + ErrorMessage; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is valid.
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        public bool IsValid
        {
            get { return isValid; }
            set { isValid = value; }
        }

        /// <summary>
        /// Gets the text ONLY, no Sets method
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get { return text; }
        }

        /// <summary>
        /// Gets the error message ONLY, no Sets method
        /// </summary>
        /// <value>The error message.</value>
        public string ErrorMessage
        {
            get { return errMessage; }
        }

        /// <summary>
        /// Gets the name of the property ONLY, no Sets method
        /// </summary>
        /// <value>The name of the property.</value>
        public string PropertyName
        {
            get { return propertyToValidate.Name; }
        }

        /// <summary>
        /// Gets or sets the property to validate.
        /// </summary>
        /// <value>The property to validate.</value>
        public PropertyInfoExtended PropertyToValidate
        {
            get { return propertyToValidate; }
            internal set { propertyToValidate = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ValidatorAttribute"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="text">The text.</param>
        protected ValidatorAttribute(string errorMessage, string text)
        {
            errMessage = errorMessage;
            this.text = text;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ValidatorAttribute"/> class.
        /// Hide default constructor
        /// </summary>
        private ValidatorAttribute()
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Validates this instance.
        /// </summary>
        public void Validate()
        {
            isValid = EvaluateIsValid();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Evaluates the is valid.
        /// </summary>
        /// <returns></returns>
        protected internal abstract bool EvaluateIsValid();

        #endregion
    } //end class    
} //end namespace