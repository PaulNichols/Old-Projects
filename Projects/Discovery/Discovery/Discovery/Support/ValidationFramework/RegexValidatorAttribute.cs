#region Using Directives

using System;
using System.Text.RegularExpressions;

#endregion

namespace ValidationFramework
{
	/// <summary>
	/// Performs a Regular Expression validation.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
    [Serializable]
	public sealed class RegexValidatorAttribute : ValidatorAttribute
	{

		#region Private Fields

		private readonly string validationPattern;

		#endregion


		#region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:RegexValidatorAttribute"/> class.
        /// </summary>
        /// <param name="validationExpression">The validation expression.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="text">The text.</param>
		public RegexValidatorAttribute(string validationExpression, string errorMessage, string text) : base(errorMessage,text)
		{
			this.validationPattern = validationExpression;
		}

		#endregion


		#region Public Properties

        /// <summary>
        /// Gets the validation expression.
        /// </summary>
        /// <value>The validation expression.</value>
		public string ValidationExpression
		{
			get
			{
				return this.validationPattern;
			}
		}

		#endregion


		#region Protected Overrides

        /// <summary>
        /// Evaluates the is valid.
        /// </summary>
        /// <returns></returns>
		protected internal override bool EvaluateIsValid()
		{
            if (this.PropertyToValidate.Type != typeof(string))
            {
               // throw new ArgumentException("Property must be a string to be used for the Regex Validator");
            }

			string text = this.PropertyToValidate.Value as string;
			if (text == null)
				return true;

            return Validation.IsRegexValid(text, this.validationPattern);
		}

		#endregion

	}//end class
}//end namespace
