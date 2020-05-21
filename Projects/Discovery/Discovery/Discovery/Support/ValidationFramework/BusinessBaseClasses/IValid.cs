#region Using Directives

using System;
using System.Collections.Generic;

#endregion

namespace ValidationFramework
{
    /// <summary>
    /// Checking valid or not
    /// </summary>
    public interface IValid
    {

        /// <summary>
        /// Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        bool IsValid
        {
            get;
        }

        /// <summary>
        /// Gets the validation messages.
        /// </summary>
        /// <value>The validation messages.</value>
        List<string> ValidationMessages
        {
            get;
        }

    }
}
