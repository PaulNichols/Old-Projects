#region Using Directives

using System;

#endregion

namespace ValidationFramework
{
    /// <summary>
    /// Performs a Required Field validation.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    [Serializable]
    public sealed class RequiredValidatorAttribute : ValidatorAttribute
    {
        #region Private Fields

        private string initValue;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the initial value.
        /// </summary>
        /// <value>The initial value.</value>
        public string InitialValue
        {
            get { return initValue; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:RequiredValidatorAttribute"/> class.
        /// </summary>
        /// <param name="initialValue">The initial value.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="text">The text.</param>
        public RequiredValidatorAttribute(string initialValue, string errorMessage, string text)
            : base(errorMessage, text)
        {
            initValue = initialValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:RequiredValidatorAttribute"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="text">The text.</param>
        public RequiredValidatorAttribute(string errorMessage, string text) : this(null, errorMessage, text)
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
            // In order to leverage the generic method, have to use if/then
            // statement here to satisfy compiler requirements for "struct" 
            // generic constraint.  We cannot simply cast to ValueType because
            // nullable types are not allowed with the "struct" constraint.
            if (PropertyToValidate.Type == typeof (int))
            {
                int value = Convert.ToInt32(PropertyToValidate.Value);
                int initialValue = (initValue == null ? default(int) : Convert.ToInt32(initValue));
                return Validation.IsRequiredValid(value, initialValue);
            }
            else if (PropertyToValidate.Type == typeof (byte))
            {
                byte value = Convert.ToByte(PropertyToValidate.Value);
                byte initialValue = (initValue == null ? default(byte) : Convert.ToByte(initValue));
                return Validation.IsRequiredValid(value, initialValue);
            }
            else if (PropertyToValidate.Type == typeof (short))
            {
                short value = Convert.ToInt16(PropertyToValidate.Value);
                short initialValue = (initValue == null ? default(short) : Convert.ToInt16(initValue));
                return Validation.IsRequiredValid(value, initialValue);
            }
            else if (PropertyToValidate.Type == typeof (DateTime))
            {
                DateTime value = Convert.ToDateTime(PropertyToValidate.Value);
                DateTime initialValue = (initValue == null ? default(DateTime) : Convert.ToDateTime(initValue));
                return Validation.IsRequiredValid(value, initialValue);
            }
            else if (PropertyToValidate.Type == typeof (decimal))
            {
                decimal value = Convert.ToDecimal(PropertyToValidate.Value);
                decimal initialValue = (initValue == null ? default(decimal) : Convert.ToDecimal(initValue));
                return Validation.IsRequiredValid(value, initialValue);
            }
            else if (PropertyToValidate.Type == typeof (float))
            {
                float value = Convert.ToSingle(PropertyToValidate.Value);
                float initialValue = (initValue == null ? default(float) : Convert.ToSingle(initValue));
                return Validation.IsRequiredValid(value, initialValue);
            }
            else if (PropertyToValidate.Type == typeof (double))
            {
                double value = Convert.ToDouble(PropertyToValidate.Value);
                double initialValue = (initValue == null ? default(double) : Convert.ToDouble(initValue));
                return Validation.IsRequiredValid(value, initialValue);
            }
            else if (PropertyToValidate.Type == typeof (Guid))
            {
                Guid value = new Guid(PropertyToValidate.Value.ToString());
                Guid initialValue = (initValue == null ? default(Guid) : new Guid(initValue));
                return Validation.IsRequiredValid(value, initialValue);
            }
            else if (PropertyToValidate.Type == typeof (string))
            {
                string value = (PropertyToValidate.Value == null ? null : PropertyToValidate.Value.ToString());
                //Since String can be null or empty, need to do an explicit check
                if (initValue != null)
                {
                    return Validation.IsRequiredValid(value, initValue);
                }
                else
                {
                    return Validation.IsRequiredValid(value);
                }
            }
            else
            {
                throw new ArgumentException("Invalid data type to use for Required validator.");
            }
        }

        #endregion
    } //end class
} //end namespace