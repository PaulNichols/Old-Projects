#region Using Directives

using System;
using System.Text.RegularExpressions;

#endregion

namespace ValidationFramework
{
    /// <summary>
    /// Static class for all validation methods.  This public API can be used 
    /// anywhere in a solution.
    /// </summary>
    public static class Validation
    {

        #region Required Validation

        /// <summary>
        /// Determines whether [is required valid] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// 	<c>true</c> if [is required valid] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsRequiredValid<T>(T value) where T : struct
        {
            return IsRequiredValid(value, (default(T)));
        }

        /// <summary>
        /// Determines whether [is required valid] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="initialValue">The initial value.</param>
        /// <returns>
        /// 	<c>true</c> if [is required valid] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsRequiredValid<T>(T value, T initialValue) where T : struct
        {
            return (!value.Equals(initialValue));
        }

        /// <summary>
        /// Determines whether [is required valid] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// 	<c>true</c> if [is required valid] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsRequiredValid<T>(Nullable<T> value) where T : struct
        {
            return (value.HasValue);
        }

        /// <summary>
        /// Determines whether [is required valid] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// 	<c>true</c> if [is required valid] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsRequiredValid(string value)
        {
            return (!string.IsNullOrEmpty(value));
        }

        /// <summary>
        /// Determines whether [is required valid] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="initialValue">The initial value.</param>
        /// <returns>
        /// 	<c>true</c> if [is required valid] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsRequiredValid(string value, string initialValue)
        {
            return (value != initialValue);
        }

        #endregion


        #region Length Validation 

        /// <summary>
        /// Determines whether [is length valid] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="max">The max.</param>
        /// <returns>
        /// 	<c>true</c> if [is length valid] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsLengthValid(string value, int max)
        {
            return IsLengthValid(value, 0, max);
        }

        /// <summary>
        /// Determines whether [is length valid] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns>
        /// 	<c>true</c> if [is length valid] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsLengthValid(string value, int min, int max)
        {
            return (value != null && value.Length >= min && value.Length <= max);
        }

        #endregion


        #region Regex Validation

        /// <summary>
        /// Determines whether [is regex valid] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns>
        /// 	<c>true</c> if [is regex valid] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsRegexValid(string value, string pattern)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value", "value cannot be null");
            }

            Match m = Regex.Match(value, pattern);
            return
                m.Success &&
                m.Index == 0 &&
                m.Length == value.Length;
        }

        #endregion


        #region Range Validation

        /// <summary>
        /// Determines whether [is range valid] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns>
        /// 	<c>true</c> if [is range valid] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsRangeValid<T>(Nullable<T> value, T min, T max) where T : struct, IComparable, IComparable<T>
        {
            if (!value.HasValue)
                return true; //don't perform validation if no value

            return IsRangeValid(value.GetValueOrDefault(), min, max);
        }

        /// <summary>
        /// Determines whether [is range valid] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns>
        /// 	<c>true</c> if [is range valid] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsRangeValid<T>(T value, T min, T max) where T : struct, IComparable<T>
        {
            bool isAboveMin =
                Compare(value, min, ValidationCompareOperator.GreaterThanEqual);

            if (isAboveMin)
            {
                bool isBelowMax =
                    Compare(value, max, ValidationCompareOperator.LessThanEqual);

                return (isAboveMin && isBelowMax);
            }
            return false;
        }

        /// <summary>
        /// Determines whether [is range valid] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns>
        /// 	<c>true</c> if [is range valid] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsRangeValid(string value, string min, string max)
        {
            if (value == null)
                return true;

            bool isAboveMin = (value.CompareTo(min) >= 0);

            if (isAboveMin)
            {
                bool isBelowMax = (value.CompareTo(max) <= 0);

                return (isAboveMin && isBelowMax);
            }
            return false;
        }

        #endregion


        #region Compare Validation

        /// <summary>
        /// Determines whether [is compare valid] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="comparisonValue">The comparison value.</param>
        /// <param name="oper">The oper.</param>
        /// <returns>
        /// 	<c>true</c> if [is compare valid] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCompareValid<T>(Nullable<T> value, Nullable<T> comparisonValue, ValidationCompareOperator oper) where T : struct, IComparable<T>
        {
            return Compare(value, comparisonValue, oper);
        }

        /// <summary>
        /// Determines whether [is compare valid] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="comparisonValue">The comparison value.</param>
        /// <param name="oper">The oper.</param>
        /// <returns>
        /// 	<c>true</c> if [is compare valid] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCompareValid<T>(T value, T comparisonValue, ValidationCompareOperator oper) where T : struct, IComparable<T>
        {
            return Compare(value, comparisonValue, oper);
        }

        /// <summary>
        /// Determines whether [is compare valid] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="comparisonValue">The comparison value.</param>
        /// <param name="oper">The oper.</param>
        /// <returns>
        /// 	<c>true</c> if [is compare valid] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCompareValid(string value, string comparisonValue, ValidationCompareOperator oper)
        {
            return Compare(value, comparisonValue, oper);
        }

        #endregion


        #region Private Methods

        private static bool Compare<T>(T left, T right, ValidationCompareOperator oper) where T : struct, IComparable<T>
        {
            int compareResult = left.CompareTo(right);
            return IsCompareResultValid(compareResult, oper);
        }

        private static bool Compare<T>(Nullable<T> left, Nullable<T> right, ValidationCompareOperator oper) where T : struct, IComparable<T>
        {
            int compareResult;
            if (!left.HasValue && !right.HasValue)
            {
                compareResult = 0;//equal because both null
            }
            else if (!left.HasValue && right.HasValue)
            {
                compareResult = -1;
            }
            else if (left.HasValue && !right.HasValue)
            {
                compareResult = 1;
            }
            else
            {
                compareResult = left.Value.CompareTo(right.Value);
            }
            return IsCompareResultValid(compareResult, oper);
        }

        private static bool Compare(string left, string right, ValidationCompareOperator oper)
        {
            int compareResult = string.Compare(left, right);
            return IsCompareResultValid(compareResult, oper);
        }

        private static bool IsCompareResultValid(int compareResult, ValidationCompareOperator oper)
        {
            switch (oper)
            {
                case ValidationCompareOperator.Equal:
                    return (compareResult == 0);
                case ValidationCompareOperator.NotEqual:
                    return (compareResult != 0);
                case ValidationCompareOperator.GreaterThan:
                    return (compareResult > 0);
                case ValidationCompareOperator.GreaterThanEqual:
                    return (compareResult >= 0);
                case ValidationCompareOperator.LessThan:
                    return (compareResult < 0);
                case ValidationCompareOperator.LessThanEqual:
                    return (compareResult <= 0);
            }
            return true;
        }

        #endregion

    }
}
