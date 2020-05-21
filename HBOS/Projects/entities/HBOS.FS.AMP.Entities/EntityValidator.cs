using System;
using System.Text;
using System.Reflection;

namespace HBOS.FS.AMP.Entities
{
    /// <summary>
    /// Summary description for BusinessObjectValidator.
    /// </summary>
    public class EntityValidator
    {
        public EntityValidator()
        {
            m_invalidMessage = new StringBuilder();
        }

        private StringBuilder m_invalidMessage;

        /// <summary>
        /// This function should check all 
        /// </summary>
        /// <param name="customerToValidate"></param>
        /// <returns></returns>
        public bool ValidateProperties(IEntityBase entityToValidate)
        {
            bool isObjectValid = true;
            bool isAttributeValid = false;
        
            // Get the type of the object and its property info
            Type businessObjectType = entityToValidate.GetType();
            PropertyInfo[] properties = businessObjectType.GetProperties();


            // Get the custom attributes for each property
            foreach (PropertyInfo propertyInfo in properties)
            {
                object[] customAttributes = propertyInfo.GetCustomAttributes(true);
                foreach (object customAttribute in customAttributes)
                {
                    // Validate all the validation attributes
                    if (customAttribute is IEntityValidationAttribute)
                    {
                        IEntityValidationAttribute entityAttribute = (IEntityValidationAttribute)customAttribute;
                        object currentValue = propertyInfo.GetValue(entityToValidate, null);
                        isAttributeValid = entityAttribute.IsValid(currentValue);
                        if (!isAttributeValid)
                        {
                            m_invalidMessage.Append(formatInvalidMessage(currentValue, propertyInfo, entityToValidate, entityAttribute));
                            isObjectValid = false;
                        }
                    }
                }
            }

            return isObjectValid;
        }

        public string InvalidMessage
        {
            get
            {
                return m_invalidMessage.ToString();
            }
        }

        private string formatInvalidMessage(object currentValue, PropertyInfo propertyInfo, IEntityBase entityToValidate, IEntityValidationAttribute customAttribute)
        {
            StringBuilder invalidMessage = new StringBuilder();
            invalidMessage.Append(@"A value of """);
            if (currentValue != null)
            {
                invalidMessage.Append(currentValue.ToString());
            }
            else
            {
                invalidMessage.Append("<null>");
            }
            invalidMessage.Append(@""" for property ");
            if (propertyInfo != null)
            {
                invalidMessage.Append(propertyInfo.Name);
            }
            else
            {
                invalidMessage.Append("<null>");
            }
            invalidMessage.Append(" on object ");
            if (entityToValidate != null)
            {
                invalidMessage.Append(entityToValidate.ToString());
            }
            else
            {
                invalidMessage.Append("<null>");
            }
            invalidMessage.Append(" invalid : ");
            if (customAttribute != null)
            {
                invalidMessage.Append( ((IEntityValidationAttribute)customAttribute).InvalidDescription + Environment.NewLine );
            }
            else
            {
                invalidMessage.Append("<Description unavailable>");
            }
            
            return invalidMessage.ToString();

        }
    }
}
