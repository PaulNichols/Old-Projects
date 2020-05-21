using System;

namespace HBOS.FS.AMP.Entities
{
    /// <summary>
    /// Attribute that can be applied to string properties only of business objects
    /// and ensures that the length of the string does not exceed that specified.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false, Inherited=true)]
    public class MaxLengthAttribute : Attribute, IEntityValidationAttribute
    {
        private int _maxLength;

        public MaxLengthAttribute(int maxLength)
        {
            _maxLength = maxLength;
        }

        #region IValidationAttribute Members

        public string InvalidDescription
        {
            get
            {
                return "The maximum length of this property is " + _maxLength.ToString();
            }
        }

        public bool IsValid(object valueToValidate)
        {
            if (valueToValidate == null)
            {
                return false;
            }

            if (valueToValidate.GetType() != System.Type.GetType("System.String"))
            {
                throw new Exception("This property can only be applied to strings");
            }

            else
            {
                return ((string)valueToValidate).Length < _maxLength ? true : false;
            }
        }
        #endregion
    }
}
