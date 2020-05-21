#region Using Directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace ValidationFramework.Web
{
    internal class LengthValidatorCreator : ValidatorCreator<LengthValidatorAttribute>
    {

        #region Constructors

        public LengthValidatorCreator(LengthValidatorAttribute attr, Control control)
            : base(attr, control)
        {
        }

        #endregion


        #region Overrides

        protected override BaseValidator CreateWebValidator()
        {
            RegularExpressionValidator webValidator = new RegularExpressionValidator();
            this.AssociateControl(webValidator);

            webValidator.ValidationExpression =
                "^.{" +
                this.Validator.Minimum.ToString() + "," +
                this.Validator.Maximum.ToString() + "}$";
            
            return webValidator;
        }

        #endregion

    }
}
