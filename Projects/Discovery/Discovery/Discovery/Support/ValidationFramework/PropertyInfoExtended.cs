#region Using Directives

using System;
using System.Collections.Generic;
using System.Reflection;

#endregion

namespace ValidationFramework
{
    /// <summary>
    /// Extends PropertyInfo by wrapping a PropertyInfo object along with the object
    /// instanace that the Property belongs to.  This allows for "stateful" reflection.
    /// </summary>
    [Serializable]
    public class PropertyInfoExtended
    {

        #region Private Fields

        private PropertyInfo propInfo;
        private object target;

        #endregion


        #region Public Properties

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get
            {
                return this.propInfo.Name;
            }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        public object Value
        {
            get
            {
                return this.propInfo.GetValue(target, null);
            }
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        public Type Type
        {
            get
            {
                return this.propInfo.PropertyType;
            }
        }

        /// <summary>
        /// Gets the target.
        /// </summary>
        /// <value>The target.</value>
        public object Target
        {
            get
            {
                return this.target;
            }
        }

        #endregion


        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PropertyInfoExtended"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <param name="targetInstance">The target instance.</param>
        public PropertyInfoExtended(PropertyInfo propertyInfo, object targetInstance)
        {
            this.propInfo = propertyInfo;
            this.target = targetInstance;
        }

        #endregion

    }
}
