#region Using Directives

using System;

#endregion

namespace ValidationFramework
{
    /// <summary>
    /// Performs a string length validation.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    [Serializable]
    public sealed class LengthValidatorAttribute : ValidatorAttribute
    {
        #region Private Fields

        private readonly int min;
        private readonly int max;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the maximum.
        /// </summary>
        /// <value>The maximum.</value>
        public int Maximum
        {
            get { return max; }
        }

        /// <summary>
        /// Gets the minimum.
        /// </summary>
        /// <value>The minimum.</value>
        public int Minimum
        {
            get { return min; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:LengthValidatorAttribute"/> class.
        /// </summary>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="text">The text.</param>
        public LengthValidatorAttribute(int minimum, int maximum, string errorMessage, string text)
            : base(String.Format(errorMessage, minimum, maximum), text)
        {
            min = minimum;
            max = maximum;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:LengthValidatorAttribute"/> class.
        /// </summary>
        /// <param name="maximum">The maximum.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="text">The text.</param>
        public LengthValidatorAttribute(int maximum, string errorMessage, string text)
            : this(0, maximum, errorMessage, text)
        {
        }

        #endregion

        #region Protected Overrides

        /// <summary>
        /// Evaluates the is valid.
        /// </summary>
        /// <returns></returns>
        protected internal override bool EvaluateIsValid()
        {
            if (PropertyToValidate.Type != typeof (string))
            {
                throw new ArgumentException("Property must be a string to be used for the Length Validator");
            }

            string propertyToValidateValue = PropertyToValidate.Value as string;
            return Validation.IsLengthValid(propertyToValidateValue, min, max);
        }

        #endregion
    } //end class
} //end namespace