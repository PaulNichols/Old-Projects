#region Using Directives

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using ValidatorList = System.Collections.ObjectModel.ReadOnlyCollection<ValidationFramework.ValidatorAttribute>;

#endregion

namespace ValidationFramework
{

    public delegate void CustomValidationDelegate(object sender, CustomValidationEventArgs e);

    /// <summary>
    /// This is the primary class that will be utilized by the consumer.
    /// This class responsible for exposing a simple public API to the 
    /// consumer while handling the internals of invoking all 
    /// validators properly.
    /// </summary>
    [Serializable]
    public class ValidationManager
    {
        #region Private Fields

        private Dictionary<string, ValidatorAttribute> errorsDictionary = new Dictionary<string, ValidatorAttribute>();
        private object target;
        private Type targetType;

        private Dictionary<string, ValidatorList> propertyCache;

        #endregion

        #region Public Properties

        public event CustomValidationDelegate CustomValidation;

        /// <summary>
        /// Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        public bool IsValid
        {
            get { return (errorsDictionary.Count == 0); }
        }

        /// <summary>
        /// Gets the violations.
        /// </summary>
        /// <value>The violations.</value>
        public ICollection<ValidatorAttribute> Violations
        {
            get { return errorsDictionary.Values; }
        }

        /// <summary>
        /// Gets the violation messages.
        /// </summary>
        /// <value>The violation messages.</value>
        public List<string> ViolationMessages
        {
            get
            {
                List<string> messages = new List<string>();
                foreach (ValidatorAttribute validator in errorsDictionary.Values)
                {
                    messages.Add(validator.ErrorMessage);
                }
                messages.Sort();

                return messages;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ValidationManager"/> class.
        /// </summary>
        /// <param name="targetClass">The target class.</param>
        public ValidationManager(object targetClass)
        {
            if (targetClass == null)
            {
                throw new ArgumentNullException(ExceptionStrings.TargetParam, ExceptionStrings.TargetParamError);
            }

            target = targetClass;
            targetType = target.GetType();

            InitializePropertyCache();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Validates all properties.
        /// </summary>
        public void Validate()
        {
            // Validate each of the properties
            foreach (ValidatorList list in propertyCache.Values)
            {
                CheckValidProperty(list);
            }

            // If we have custom validation code, call it
            if (null != CustomValidation)
            {
                // Create an instance of CustomValidationEventArgs
                CustomValidationEventArgs e = new CustomValidationEventArgs();

                // Validate via the custom delegate
                CustomValidation(this, e);

                if (!e.IsValid)
                {
                    // Update validation messages
                    errorsDictionary["CustomValidation"] = new CustomValidatorAttribute(e.ErrorMessage, "", "");
                }
            }

        }

        /// <summary>
        /// Validates only the specified property.
        /// </summary>
        /// <param name="propertyName">Property to validate</param>
        public void Validate(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException(ExceptionStrings.PropertyName, ExceptionStrings.PropertyNameError);
            }

            CheckValidProperty(propertyCache[propertyName]);
        }

        /// <summary>
        /// Returns a string of all error messages for a given property
        /// </summary>
        /// <param name="property">Property to retrieve error message for</param>
        /// <returns></returns>
        public string GetErrorMessages(string property)
        {
            List<string> errors = new List<string>();

            foreach (ValidatorAttribute validator in errorsDictionary.Values)
            {
                if (validator.PropertyName == property)
                {
                    errors.Add(validator.ErrorMessage);
                }
            }

            string[] errorsArr = new string[errors.Count];
            errors.CopyTo(errorsArr, 0);
            return string.Join(Environment.NewLine, errorsArr);
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </returns>
        public override string ToString()
        {
            return ToString(Environment.NewLine);
        }

        /// <summary>
        /// Toes the string.
        /// </summary>
        /// <param name="separator">The separator.</param>
        /// <returns></returns>
        public string ToString(string separator)
        {
            List<string> errors = new List<string>(ViolationMessages);
            return string.Join(separator, errors.ToArray());
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Performs initialization so that we call Type.GetProperties()
        /// and PropertyInfo.GetCustomAttributes() ONLY in this method and
        /// then cache the results for later use.  Since the reflection
        /// methods are more expensive, the goal is to execute them only 1
        /// time (in this method) and cache the results so we do not have
        /// to invoke these for each validation.
        /// </summary>
        private void InitializePropertyCache()
        {
            propertyCache = new Dictionary<string, ValidatorList>();

            PropertyInfo[] properties = targetType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in properties)
            {
                // Get all the validation attributes for this property
                ValidatorAttribute[] attributes = (ValidatorAttribute[]) prop.GetCustomAttributes(typeof (ValidatorAttribute), true);
                // Add the property to the cache
                foreach (ValidatorAttribute attr in attributes)
                {
                    // Make sure each attribute is "aware" of the property it's validating
                    attr.PropertyToValidate = new PropertyInfoExtended(prop, target);
                }

                if (attributes.Length != 0)
                {
                    propertyCache.Add(prop.Name, Array.AsReadOnly(attributes));
                }
            }
        }

        /// <summary>
        /// Performs validation for a specific property.
        /// </summary>
        /// <param name="validatorList">The validator list.</param>
        private void CheckValidProperty(ReadOnlyCollection<ValidatorAttribute> validatorList)
        {
            foreach (ValidatorAttribute attr in validatorList)
            {
                attr.Validate();

                if (!attr.IsValid)
                {
                    errorsDictionary[attr.UniqueKey] = attr;
                }
                else
                {
                    if (errorsDictionary.ContainsKey(attr.UniqueKey))
                        errorsDictionary.Remove(attr.UniqueKey);
                }
            }
        }

        private static class ExceptionStrings
        {
            public const string TargetParam = "targetClass";
            public const string TargetParamError = "The targetClass object cannot be null.";
            public const string PropertyName = "propertyName";
            public const string PropertyNameError = "The propertyName is invalid.";
        }

        #endregion
    } //end class
} //end namespace