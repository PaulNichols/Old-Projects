#region Using Directives

using System;

#endregion

namespace ValidationFramework
{

    /// <summary>
    /// An enumeration for the compare operator validation
    /// </summary>
    public enum ValidationCompareOperator
    {
        /// <summary>
        /// Equal type of ValidationCompareOperator
        /// </summary>
        Equal,
        /// <summary>
        /// NotEqual type of ValidationCompareOperator
        /// </summary>
        NotEqual,
        /// <summary>
        /// GreaterThan type of ValidationCompareOperator
        /// </summary>
        GreaterThan,
        /// <summary>
        /// GreaterThanEqual type of ValidationCompareOperator
        /// </summary>
        GreaterThanEqual,
        /// <summary>
        /// LessThan type of ValidationCompareOperator
        /// </summary>
        LessThan,
        /// <summary>
        /// LessThanEqual type of ValidationCompareOperator
        /// </summary>
        LessThanEqual
    }

}
