#region Using Directives

using System;
using System.Collections.ObjectModel;
using System.Text;

#endregion

namespace ValidationFramework
{
    /// <summary>
    /// A generic class NotifyValidatableCollection to validate the collection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NotifyValidatableCollection<T> : ValidatableCollection<T>, IValid where T : NotifyValidatableBase
    {
        
        #region IValid Members

        /// <summary>
        /// Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        public override bool IsValid
        {
            get 
            {
                foreach (T item in this)
                {
                    if (!item.IsValid)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        #endregion

    }
}
