using System;

namespace HBOS.FS.AMP.Entities
{
    /// <summary>
    /// Attribute that can be applied to string properties only of business objects
    /// and ensures that the length of the string is at least that specified.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false, Inherited=true)]
    public class MinLengthAttribute : Attribute, IEntityValidationAttribute
    {
        private int _minLength;

        public MinLengthAttribute(int minLength)
        {
            _minLength = minLength;
        }

        #region IValidationAttribute Members

        public string InvalidDescription
        {
            get
            {
                return "The minimum length of this property is " + _minLength.ToString();
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
                return ((string)valueToValidate).Length >= _minLength ? true : false;
            }
        }
        #endregion
    }
}
