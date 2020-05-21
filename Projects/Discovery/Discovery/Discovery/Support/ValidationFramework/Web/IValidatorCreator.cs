#region Using Directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace ValidationFramework.Web
{
    /// <summary>
    /// Define this interface so that the generic ValidatorCreator classes can 
    /// be called polymorphically.
    /// </summary>
    internal interface IValidatorCreator
    {
        void CreateValidator();
    }
}
