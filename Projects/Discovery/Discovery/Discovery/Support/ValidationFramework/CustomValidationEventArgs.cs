#region Using Directives

using System;
using System.Collections.Generic;

#endregion

namespace ValidationFramework
{
    /// <summary>
    /// A class to provide custom validation
    /// </summary>
    public class CustomValidationEventArgs : EventArgs
    {

        #region Private Fields

        private string errorMessage;
        private bool isValid;

        #endregion


        #region Public Properties

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>The error message.</value>
        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }
            set
            {
                errorMessage = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is valid.
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        public bool IsValid
        {
            get
            {
                return isValid;
            }
            set
            {
                isValid = value;
            }
        }

        #endregion


        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:CustomValidationEventArgs"/> class.
        /// </summary>
        public CustomValidationEventArgs()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:CustomValidationEventArgs"/> class.
        /// </summary>
        /// <param name="errMessage">The err message.</param>
        /// <param name="valid">if set to <c>true</c> [valid].</param>
        public CustomValidationEventArgs(string errMessage, bool valid)
        {
            this.errorMessage = errMessage;
            this.isValid = valid;
        }

        #endregion

    }
}
