#region Using Directives

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace ValidationFramework.Web
{
    /// <summary>
    /// A class ValidatorGenerator which is used to generate ASP validation controls
    /// </summary>
    public class ValidatorGenerator
    {

        #region Private Fields

        private List<ValidationAssociation> controlList = new List<ValidationAssociation>();

        #endregion


        #region Public Methods

        /// <summary>
        /// Adds the validation.
        /// </summary>
        /// <param name="controlId">The control id.</param>
        /// <param name="propertyName">Name of the property.</param>
        public void AddValidation(string controlId, string propertyName)
        {
            controlList.Add(new ValidationAssociation(controlId, propertyName));
        }

        /// <summary>
        /// Generates the validators.
        /// </summary>
        /// <param name="containerControl">The container control.</param>
        /// <param name="target">The target.</param>
        public void GenerateValidators(Control containerControl, Type targetType)
        {
            // Generate the ui controls
            foreach (ValidationAssociation association in controlList)
            {
                // Get the control from the ui
                Control control =  GetControlRecursive<Control>(association.ControlID, containerControl);
                // Make sure we found the control
                Debug.Assert(null != control);
                // Get the property info (could be nested)
                PropertyInfo prop = GetNestedProperty(targetType, association.PropertyName);
                // Make sure we found the propertyu info
                Debug.Assert(null != prop);

                if (null != prop)
                {
                    // Get the validation attributes for the property
                    ValidatorAttribute[] attributes = (ValidatorAttribute[])prop.GetCustomAttributes(typeof(ValidatorAttribute), true);
                    foreach (ValidatorAttribute attribute in attributes)
                    {
                        // The property we're validating
                        attribute.PropertyToValidate = new PropertyInfoExtended(prop, null);

                        // Add web UI validator for each property
                        IValidatorCreator creator = GetValidator(control, attribute);
                        if (creator != null)
                        {
                            creator.CreateValidator();
                        }
                    }
                }
            }
        }

        private T GetControlRecursive<T>(string controlName, Control container) where T : Control
        {
            // The control we've found
            T theControl = container.FindControl(controlName) as T;
            // See if we found the control
            if (null == theControl)
            {
                foreach (Control childControl in container.Controls)
                {
                    // Attempt to find the control in the container
                    theControl = GetControlRecursive<T>(controlName, childControl);
                    // See if we found it
                    if (null != theControl) break;
                }
            }
            // Done
            return theControl;
        }

        private PropertyInfo GetNestedProperty(Type targetType, string propertyName)
        {
            if (propertyName.IndexOf(".") != -1)
            {
                // Get the next type, etc
                string basePropertyName = propertyName.Substring(0, propertyName.IndexOf("."));
                string subPropertyName = propertyName.Substring(propertyName.IndexOf(".") + 1);
                // Get the nested property info
                return GetNestedProperty(targetType.GetProperty(basePropertyName).PropertyType, subPropertyName);
            }
            else
            {
                // Return the property info
                return targetType.GetProperty(propertyName);
            }
        }

        #endregion

        #region Private Members

        /// <summary>
        /// Gets the validator.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="attr">The attr.</param>
        /// <returns></returns>
        private static IValidatorCreator GetValidator(Control control, ValidatorAttribute attr)
        {
            RequiredValidatorAttribute reqAttr = attr as RequiredValidatorAttribute;
            if (reqAttr != null)
            {
                return new RequiredValidatorCreator(reqAttr, control);
            }

            LengthValidatorAttribute lengthAttr = attr as LengthValidatorAttribute;
            if (lengthAttr != null)
            {
                return new LengthValidatorCreator(lengthAttr, control);
            }

            RangeValidatorAttribute rangeAttr = attr as RangeValidatorAttribute;
            if (rangeAttr != null)
            {
                return new RangeValidatorCreator(rangeAttr, control);
            }

            RegexValidatorAttribute regexAttr = attr as RegexValidatorAttribute;
            if (regexAttr != null)
            {
                return new RegexValidatorCreator(regexAttr, control);
            }

            CompareValidatorAttribute compareAttr = attr as CompareValidatorAttribute;
            if (compareAttr != null)
            {
                return new CompareValidatorCreator(compareAttr, control);
            }

            if (attr is CustomValidatorAttribute)
            {
                return null;
            }

            throw new ArgumentException("The attribute does not derive from ValidatorAttribute.", "attr");
        }

        #region class ValidationAssociation

        private class ValidationAssociation
        {

            #region Private Fields

            private string controlID;
            private string propertyName;

            #endregion


            #region Public Properties

            public string ControlID
            {
                get { return controlID; }
            }

            public string PropertyName
            {
                get { return propertyName; }
            }

            #endregion


            #region Constructors

            public ValidationAssociation(string controlId, string propName)
            {
                this.controlID = controlId;
                this.propertyName = propName;
            }

            private ValidationAssociation()
            {
            }

            #endregion

        }

        #endregion

        #endregion

    }
}
