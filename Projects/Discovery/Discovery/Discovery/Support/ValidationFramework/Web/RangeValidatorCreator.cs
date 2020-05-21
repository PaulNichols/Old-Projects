#region Using Directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebControls = System.Web.UI.WebControls;

#endregion

namespace ValidationFramework.Web
{
    internal class RangeValidatorCreator : ValidatorCreator<RangeValidatorAttribute>
    {

        #region Constructors

        public RangeValidatorCreator(RangeValidatorAttribute attr, Control control)
            : base(attr, control)
        {
        }

        #endregion


        #region Overrides

        protected override BaseValidator CreateWebValidator()
        {
            RangeValidator webValidator = new RangeValidator();
            this.AssociateControl(webValidator);

            webValidator.Type = this.GetValidationDataType();
            if (webValidator.Type == WebControls.ValidationDataType.Date)
            {
                const string dateFormat = "dd/MM/yyyy";
                DateTime minValue = Convert.ToDateTime(this.Validator.Minimum);
                DateTime maxValue = Convert.ToDateTime(this.Validator.Maximum);
                webValidator.MinimumValue = minValue.ToString(dateFormat);
                webValidator.MaximumValue = maxValue.ToString(dateFormat);
            }
            else
            {
                webValidator.MinimumValue = this.Validator.Minimum.ToString();
                webValidator.MaximumValue = this.Validator.Maximum.ToString();
            }

            return webValidator;
        }

        #endregion

    }
}
