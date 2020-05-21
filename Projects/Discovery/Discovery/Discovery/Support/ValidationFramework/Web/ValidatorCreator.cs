#region Using Directives

using System;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace ValidationFramework.Web
{
    internal abstract class ValidatorCreator<T> : IValidatorCreator where T : ValidatorAttribute
    {
        #region Private Fields

        private Control control;
        private T validator;

        #endregion

        #region Constructors

        protected ValidatorCreator(T validator, Control ctrl)
        {
            this.validator = validator;
            control = ctrl;
        }

        private ValidatorCreator()
        {
        }

        #endregion

        #region Public Methods

        public void CreateValidator()
        {
            BaseValidator webValidator = CreateWebValidator();
            int index = Control.Parent.Controls.IndexOf(Control);
            Control.Parent.Controls.AddAt(index + 1, webValidator);
        }

        #endregion

        #region Protected Properties

        protected T Validator
        {
            get { return validator; }
        }

        protected Control Control
        {
            get { return control; }
        }

        #endregion

        #region Protected Methods

        protected abstract BaseValidator CreateWebValidator();

        protected void AssociateControl(BaseValidator webValidator)
        {
            webValidator.ControlToValidate = Control.ID;
            webValidator.ErrorMessage = Validator.ErrorMessage;
            webValidator.Text = Validator.Text;
            webValidator.CssClass = "Validator";
            webValidator.EnableClientScript = true;
            webValidator.Display = ValidatorDisplay.Static;
        }

        protected ValidationDataType GetValidationDataType()
        {
            Type itemType = Validator.PropertyToValidate.Type;

            if (itemType == typeof (string))
            {
                return ValidationDataType.String;
            }
            else if (itemType == typeof (int))
            {
                return ValidationDataType.Integer;
            }
            else if (itemType == typeof (decimal))
            {
                return ValidationDataType.Currency;
            }
            else if (itemType == typeof (DateTime))
            {
                return ValidationDataType.Date;
            }
            else if (itemType == typeof (double))
            {
                return ValidationDataType.Double;
            }
            else
            {
                throw new ArgumentException("Invalid data type to be used with RangeValidator - " + itemType.ToString());
            }
        }

        #endregion
    }
}