#region Using Directives

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

#endregion

namespace ValidationFramework
{
    /// <summary>
    /// Validation of Collection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ValidatableCollection<T> : Collection<T>, IValid where T : ValidatableBase
    {
        
        #region IValid Members

        /// <summary>
        /// Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        public virtual bool IsValid
        {
            get 
            {
                bool isValid = true;

                //Check to see if each item is valid.
                //Even if an invalid item is found, continue to check for the rest
                //of the items so that all error messages can be accumulated.
                foreach (T item in this)
                {
                    if (!item.IsValid)
                    {
                        isValid = false;
                    }
                }
                return isValid;
            }
        }

        /// <summary>
        /// Gets the validation messages.
        /// </summary>
        /// <value>The validation messages.</value>
        public List<string> ValidationMessages
        {
            get
            {
                List<string> errorList = new List<string>();
                foreach (T item in this)
                {
                    errorList.AddRange(item.ValidationMessages);
                }
                return errorList;
            }
        }

        //public string GetValidationMessages(string separator)
        //{
        //    List<string> errorList = new List<string>();
        //    foreach (T item in this)
        //    {
        //        errorList.Add(item.GetValidationMessages(separator));
        //    }
        //    return string.Join(separator, errorList.ToArray());
        //}

        #endregion

    }
}
