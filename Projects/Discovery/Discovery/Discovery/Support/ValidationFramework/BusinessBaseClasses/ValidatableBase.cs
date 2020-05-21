#region Using Directives

using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

#endregion

namespace ValidationFramework
{
    /// <summary>
    /// Provides base class that developer can (optionally) inherit from to provide
    /// minimal base functionality to sub-classes.
    /// </summary>
    [Serializable]
    public abstract class ValidatableBase : IValid
    {
        #region Private Fields

        private ValidationManager validator;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the validator.
        /// </summary>
        /// <value>The validator.</value>
        public ValidationManager Validator
        {
            get
            {
                if (validator == null)
                {
                    //Lazy initialize so that ValidationManager only instantiated when needed
                    validator = new ValidationManager(this);
                }
                return validator;
            }
        }

        /// <summary>
        /// Property can be overridden when a parent object needs to validate children.
        /// </summary>
        public virtual bool IsValid
        {
            get { return (this as IValid).IsValid; }
        }

        bool IValid.IsValid
        {
            get
            {
                //Base behavior is to perform validations and return boolean value.
                //Sub-class can override this if, for example, they are validating on the fly.
                Validator.Validate();
                return Validator.IsValid;
            }
        }

        /// <summary>
        /// Gets the validation messages.
        /// </summary>
        /// <value>The validation messages.</value>
        public virtual List<string> ValidationMessages
        {
            get
            {
                // Our validation messages
                List<string> validationMessagesAll = new List<string>();

                // Add this entities validation erros to the list
                validationMessagesAll.AddRange(Validator.ViolationMessages);

                // Get the nested types that implement ValidatableBase or implement ICollection
                PropertyInfo[] properties = this.GetType().GetProperties();
                Type propType = null;
                ValidatableBase propValidatableBase = null;

                // Iterate over the properties looking for any that are validatable
                foreach (PropertyInfo prop in properties)
                {
                    // Get the properties type
                    propType = prop.PropertyType;

                    // Make sure it's not a primitive type
                    if (!propType.IsPrimitive)
                    {
                        // See if the type is a ValidatableBase
                        if (propType.IsSubclassOf(typeof(ValidatableBase)))
                        {
                            // It's a validatable base, get it's ViolationMessages
                            propValidatableBase = prop.GetValue(this, null) as ValidatableBase;
                            if (null != propValidatableBase && propValidatableBase.Validator.Violations.Count > 0)
                            {
                                // Add a descriptive message for the entity
                                validationMessagesAll.Add(string.Format("{0} errors;", propType.Name));
                                // Add entities validation erros to the list
                                validationMessagesAll.AddRange(propValidatableBase.ValidationMessages);
                            }
                        }
                    }
                }
                // Done
                return validationMessagesAll;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets all validation messages.
        /// </summary>
        /// <param name="separator">The separator.</param>
        /// <returns></returns>
        public string GetAllValidationMessages(string separator)
        {
            return Validator.ToString(separator);
        }

        /// <summary>
        /// Determines whether IValid objects are valid while ensuring that the validation
        /// routines are executed for all items. For NotifyValidatableBase, this check
        /// is not necessary since all valdations have already been performed.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public bool ValidateItems(params IValid[] items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items", "Items cannot be null.");
            }

            bool invalidFound = false;
            foreach (IValid item in items)
            {
                if (!item.IsValid)
                {
                    invalidFound = true;
                }
            }
            return !invalidFound;
        }

        /// <summary>
        /// Determines whether IValid objects are valid while ensuring that the validation
        /// routines are executed for all items. For NotifyValidatableBase, this check
        /// is not necessary since all valdations have already been performed.
        /// </summary>
        /// <param name="violationMessages">List to store all violation messages in.</param>
        /// <param name="items">The items to validate.</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public bool ValidateItems(ref List<string> violationMessages, params IValid[] items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items", "Items cannot be null.");
            }

            if (violationMessages == null)
            {
                throw new ArgumentNullException("violationMessages", "violationMessages cannot be null.");
            }

            bool invalidFound = false;

            foreach (IValid item in items)
            {
                if (!item.IsValid)
                {
                    // We have an invalid property
                    invalidFound = true;
                    // Store the error messages
                    violationMessages.AddRange(item.ValidationMessages);
                }
            }

            return !invalidFound;
        }

        #endregion
    }
}