using System;

namespace HBOS.FS.AMP.Entities
{
    /// <summary>
    /// Attribute that can be applied to properties of business objects that cannot 
    /// be null at the time they are validated (obviously not straight after new..., then)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false, Inherited=true)]
    public class NotNullAttribute : Attribute, IEntityValidationAttribute
    {
        public NotNullAttribute()
        {}

        #region IValidationAttribute Members

        public string InvalidDescription
        {
            get
            {
                return "This property cannot be null";
            }
        }

        public bool IsValid(object valueToValidate)
        {
            if (valueToValidate == null)
            {
                return false;
            }
            else if (valueToValidate.GetType() == System.Type.GetType("System.String"))
            {
                if (((string)valueToValidate) == String.Empty)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }


        #endregion
    }
}