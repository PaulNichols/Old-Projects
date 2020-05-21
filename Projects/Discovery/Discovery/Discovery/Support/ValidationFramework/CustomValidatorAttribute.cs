#region Using Directives

using System;

#endregion

namespace ValidationFramework
{
    /// <summary>
    /// Performs a custom validation via an System.EventHandler&lt;T&gt; delegate.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    [Serializable]
    public sealed class CustomValidatorAttribute : ValidatorAttribute
    {
        #region Private Fields

        private string methodToRun;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the validation method.
        /// </summary>
        /// <value>The validation method.</value>
        public string ValidationMethod
        {
            get { return methodToRun; }
        }

        /// <summary>
        /// Gets the unique key ONLY, no Sets method
        /// </summary>
        /// <value>The unique key.</value>
        public override string UniqueKey
        {
            get { return methodToRun; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:CustomValidatorAttribute"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="validationMethod">The validation method.</param>
        /// <param name="text">The text.</param>
        public CustomValidatorAttribute(string errorMessage, string validationMethod, string text ) : base(errorMessage, text)
        {
            methodToRun = validationMethod;
        }

        #endregion

        #region Protected Overrides

        /// <summary>
        /// Evaluates the is valid.
        /// </summary>
        /// <returns></returns>
        protected internal override bool EvaluateIsValid()
        {
            EventHandler<CustomValidationEventArgs> handler =
                Delegate.CreateDelegate(typeof (EventHandler<CustomValidationEventArgs>),
                                        PropertyToValidate.Target,
                                        ValidationMethod) as EventHandler<CustomValidationEventArgs>;

            CustomValidationEventArgs args = new CustomValidationEventArgs();
            handler(this, args);

            errMessage = args.ErrorMessage;
            return args.IsValid;
        }

        #endregion
    } //end class
} //end namespace