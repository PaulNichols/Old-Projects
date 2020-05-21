#region Using Directives

using System;

#endregion

namespace ValidationFramework
{
    /// <summary>
    /// Performs a comparison validation.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    [Serializable]
    public sealed class CompareValidatorAttribute : ValidatorAttribute
    {
        #region Private Fields

        private object comparisonValue;
        private ValidationCompareOperator oper;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the value to compare.
        /// </summary>
        /// <value>The value to compare.</value>
        public object ValueToCompare
        {
            get { return comparisonValue; }
        }

        /// <summary>
        /// Gets the validation operator.
        /// </summary>
        /// <value>The validation operator.</value>
        public ValidationCompareOperator ValidationOperator
        {
            get { return oper; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:CompareValidatorAttribute"/> class.
        /// </summary>
        /// <param name="valueToCompare">The value to compare.</param>
        /// <param name="validationOperator">The validation operator.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="text">The text.</param>
        public CompareValidatorAttribute(object valueToCompare, ValidationCompareOperator validationOperator,
                                         string errorMessage, string text) : base(errorMessage, text)
        {
            comparisonValue = valueToCompare;
            oper = validationOperator;
        }

        #endregion

        #region Protected Overrides

        /// <summary>
        /// Evaluates the is valid.
        /// </summary>
        /// <returns></returns>
        protected internal override bool EvaluateIsValid()
        {
            if (PropertyToValidate.Value == null || comparisonValue == null)
                return true;

            if (PropertyToValidate.Type == typeof (string))
            {
                string comparison = comparisonValue.ToString();
                string value = PropertyToValidate.Value.ToString();
                return Validation.IsCompareValid(value, comparison, ValidationOperator);
            }
            else if (PropertyToValidate.Type == typeof (int))
            {
                int comparison = Convert.ToInt32(comparisonValue);
                int value = Convert.ToInt32(PropertyToValidate.Value);
                return Validation.IsCompareValid(value, comparison, ValidationOperator);
            }
            else if (PropertyToValidate.Type == typeof (decimal))
            {
                decimal comparison = Convert.ToDecimal(comparisonValue);
                decimal value = Convert.ToDecimal(PropertyToValidate.Value);
                return Validation.IsCompareValid(value, comparison, ValidationOperator);
            }
            else if (PropertyToValidate.Type == typeof (DateTime))
            {
                DateTime comparison = Convert.ToDateTime(comparisonValue);
                DateTime value = Convert.ToDateTime(PropertyToValidate.Value);
                return Validation.IsCompareValid(value, comparison, ValidationOperator);
            }
            else if (PropertyToValidate.Type == typeof (double))
            {
                double comparison = Convert.ToDouble(comparisonValue);
                double value = Convert.ToDouble(PropertyToValidate.Value);
                return Validation.IsCompareValid(value, comparison, ValidationOperator);
            }
            else if (PropertyToValidate.Type == typeof (Nullable<int>))
            {
                Nullable<int> comparison = Convert.ToInt32(comparisonValue);
                Nullable<int> value = (Nullable<int>) PropertyToValidate.Value;
                return Validation.IsCompareValid(value, comparison, ValidationOperator);
            }
            else if (PropertyToValidate.Type == typeof (Nullable<DateTime>))
            {
                Nullable<DateTime> comparison = Convert.ToDateTime(comparisonValue);
                Nullable<DateTime> value = (Nullable<DateTime>) PropertyToValidate.Value;
                return Validation.IsCompareValid(value, comparison, ValidationOperator);
            }
            else if (PropertyToValidate.Type == typeof (Nullable<decimal>))
            {
                Nullable<decimal> comparison = Convert.ToDecimal(comparisonValue);
                Nullable<decimal> value = (Nullable<decimal>) PropertyToValidate.Value;
                return Validation.IsCompareValid(value, comparison, ValidationOperator);
            }
            else if (PropertyToValidate.Type == typeof (Nullable<double>))
            {
                Nullable<double> comparison = Convert.ToDouble(comparisonValue);
                Nullable<double> value = (Nullable<double>) PropertyToValidate.Value;
                return Validation.IsCompareValid(value, comparison, ValidationOperator);
            }
            else
            {
                throw new ArgumentException(PropertyToValidate.Type.ToString() +
                                            " is not a valid data type to be used with a Range Validator.");
            }
        }

        #endregion
    } //end class
} //end namespace