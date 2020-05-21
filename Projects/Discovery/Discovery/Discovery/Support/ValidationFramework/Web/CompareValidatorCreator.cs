#region Using Directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using WebControls = System.Web.UI.WebControls;

#endregion

namespace ValidationFramework.Web
{
    internal class CompareValidatorCreator : ValidatorCreator<CompareValidatorAttribute>
    {

        #region Constructors

        public CompareValidatorCreator(CompareValidatorAttribute attr, Control control)
            : base(attr, control)
        {
        }
        
        #endregion


        #region Overrides

        protected override WebControls.BaseValidator CreateWebValidator()
        {
            WebControls.CompareValidator webValidator = new WebControls.CompareValidator();
            this.AssociateControl(webValidator);
            
            webValidator.Operator = this.GetOperator();
            webValidator.Type = this.GetValidationDataType();
            if (webValidator.Type == WebControls.ValidationDataType.Date)
            {
                DateTime dateValue = Convert.ToDateTime(this.Validator.ValueToCompare);
                webValidator.ValueToCompare = dateValue.ToString("MM/dd/yyyy");
            }
            else
            {
                webValidator.ValueToCompare = this.Validator.ValueToCompare.ToString();
            }
           
            return webValidator;
        }

        #endregion


        #region Private Methods

        protected WebControls.ValidationCompareOperator GetOperator()
        {
            switch (this.Validator.ValidationOperator)
            {
                case ValidationCompareOperator.Equal:
                    return WebControls.ValidationCompareOperator.Equal;
                case ValidationCompareOperator.NotEqual:
                    return WebControls.ValidationCompareOperator.NotEqual;
                case ValidationCompareOperator.GreaterThan:
                    return WebControls.ValidationCompareOperator.GreaterThan;
                case ValidationCompareOperator.GreaterThanEqual:
                    return WebControls.ValidationCompareOperator.GreaterThanEqual;
                case ValidationCompareOperator.LessThan:
                    return WebControls.ValidationCompareOperator.LessThan;
                case ValidationCompareOperator.LessThanEqual:
                    return WebControls.ValidationCompareOperator.LessThanEqual;
                default:
                    throw new ArgumentException("Invalid operator");
            }
        }

        #endregion
    
    }
}
