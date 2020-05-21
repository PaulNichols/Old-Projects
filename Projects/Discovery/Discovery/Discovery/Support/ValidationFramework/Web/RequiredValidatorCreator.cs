#region Using Directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace ValidationFramework.Web
{
    internal class RequiredValidatorCreator : ValidatorCreator<RequiredValidatorAttribute>
    {

        #region Constructors

        public RequiredValidatorCreator(RequiredValidatorAttribute reqAttr, Control control)
            : base(reqAttr, control)
        {
        }

        #endregion


        #region Overrides

        protected override BaseValidator CreateWebValidator()
        {
            RequiredFieldValidator webValidator = new RequiredFieldValidator();
            this.AssociateControl(webValidator);
            return webValidator;
        }

        #endregion
    }
}
