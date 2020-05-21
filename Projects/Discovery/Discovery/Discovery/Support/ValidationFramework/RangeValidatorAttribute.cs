#region Using Directives

using System;

#endregion

namespace ValidationFramework
{
    /// <summary>
    /// Performs a Range validation.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    [Serializable]
    public sealed class RangeValidatorAttribute : ValidatorAttribute
    {
        #region Private Fields

        private object min;
        private object max;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the minimum.
        /// </summary>
        /// <value>The minimum.</value>
        public object Minimum
        {
            get { return min; }
        }

        /// <summary>
        /// Gets the maximum.
        /// </summary>
        /// <value>The maximum.</value>
        public object Maximum
        {
            get { return max; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:RangeValidatorAttribute"/> class.
        /// </summary>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="text">The text.</param>
        public RangeValidatorAttribute(object minimum, object maximum, string errorMessage, string text)
            : base(String.Format(errorMessage, minimum, maximum), text)
        {
            min = minimum;
            max = maximum;
        }

        #endregion

        #region Protected Overrides

        /// <summary>
        /// Evaluates the is valid.
        /// </summary>
        /// <returns></returns>
        protected internal override bool EvaluateIsValid()
        {
            if (PropertyToValidate.Value == null || min == null || max == null)
                return true;

            if (PropertyToValidate.Type == typeof (string))
            {
                string minimum = min.ToString();
                string maximum = max.ToString();
                string value = PropertyToValidate.Value.ToString();
                return Validation.IsRangeValid(value, minimum, maximum);
            }
            else if (PropertyToValidate.Type == typeof (int))
            {
                int minimum = Convert.ToInt32(min);
                int maximum = Convert.ToInt32(max);
                int value = Convert.ToInt32(PropertyToValidate.Value);
                return Validation.IsRangeValid(value, minimum, maximum);
            }
            else if (PropertyToValidate.Type == typeof (decimal))
            {
                decimal minimum = Convert.ToDecimal(min);
                decimal maximum = Convert.ToDecimal(max);
                decimal value = Convert.ToDecimal(PropertyToValidate.Value);
                return Validation.IsRangeValid(value, minimum, maximum);
            }
            else if (PropertyToValidate.Type == typeof (DateTime))
            {
                DateTime minimum = Convert.ToDateTime(min);
                DateTime maximum = Convert.ToDateTime(max);
                DateTime value = Convert.ToDateTime(PropertyToValidate.Value);
                return Validation.IsRangeValid(value, minimum, maximum);
            }
            else if (PropertyToValidate.Type == typeof (double))
            {
                double minimum = Convert.ToDouble(min);
                double maximum = Convert.ToDouble(max);
                double value = Convert.ToDouble(PropertyToValidate.Value);
                return Validation.IsRangeValid(value, minimum, maximum);
            }
            else if (PropertyToValidate.Type == typeof (Nullable<int>))
            {
                int minimum = Convert.ToInt32(min);
                int maximum = Convert.ToInt32(max);
                Nullable<int> value = (Nullable<int>) PropertyToValidate.Value;
                return Validation.IsRangeValid(value, minimum, maximum);
            }
            else if (PropertyToValidate.Type == typeof (Nullable<DateTime>))
            {
                DateTime minimum = Convert.ToDateTime(min);
                DateTime maximum = Convert.ToDateTime(max);
                Nullable<DateTime> value = (Nullable<DateTime>) PropertyToValidate.Value;
                return Validation.IsRangeValid(value, minimum, maximum);
            }
            else if (PropertyToValidate.Type == typeof (Nullable<decimal>))
            {
                decimal minimum = Convert.ToDecimal(min);
                decimal maximum = Convert.ToDecimal(max);
                Nullable<decimal> value = (Nullable<decimal>) PropertyToValidate.Value;
                return Validation.IsRangeValid(value, minimum, maximum);
            }
            else if (PropertyToValidate.Type == typeof (Nullable<double>))
            {
                double minimum = Convert.ToDouble(min);
                double maximum = Convert.ToDouble(max);
                Nullable<double> value = (Nullable<double>) PropertyToValidate.Value;
                return Validation.IsRangeValid(value, minimum, maximum);
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