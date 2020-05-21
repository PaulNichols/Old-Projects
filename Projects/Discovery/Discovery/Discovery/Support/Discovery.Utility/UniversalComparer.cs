using System;
using System.Collections.Generic;
using System.Reflection;

namespace Discovery.Utility
{
    public class UniversalComparer<T> : IComparer<T>
    {
        private SortKey[] sortKeys;

        public UniversalComparer(string sort)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                // Split the list of properties.
                string[] props = sort.Split(',');
                // Prepare the array that holds information on sort criteria.
                sortKeys = new SortKey[props.Length];

                // Parse the sort string.
                for (int i = 0; i < props.Length; i++)
                {
                    // Get the N-th member name.
                    string memberName = props[i].Trim();
                    if (memberName.ToLower().EndsWith(" desc"))
                    {
                        // Discard the DESC qualifier if found.
                        sortKeys[i].Descending = true;
                        memberName = memberName.Substring(0, memberName.Length - 5).TrimEnd();
                    }
                    if (memberName.ToLower().EndsWith(" text"))
                    {
                        // Discard the TEXT qualifier if found.
                        sortKeys[i].CaseInsensitive = true;
                        memberName = memberName.Substring(0, memberName.Length - 5).TrimEnd();
                    }
                    // Search for a field with this name.
                    sortKeys[i].FieldInfo = typeof (T).GetField(memberName);
                    if (sortKeys[i].FieldInfo == null)
                    {
                        PropertyInfo prop = null;
                        sortKeys[i].FullPropertyName = memberName;
                        string[] memberNameSplit = memberName.Split('.');
                        if (memberNameSplit.Length == 1)
                        {
                            // if not found, search for a property with this name.
                           prop = typeof(T).GetProperty(memberName);
                        }
                        else  if (memberNameSplit.Length > 1)
                        {
                            
                            Type type = typeof (T);
                           
                            for(int j=0;j<memberNameSplit.Length;j++)
                            {
                                prop= type.GetProperty(memberNameSplit[j]);
                                type = prop.PropertyType;
                            }
                        }
                        sortKeys[i].PropertyInfo = prop;
                        
                    }
                }
            }
        }

        // This procedure is invoked when comparing two objects.

        #region IComparer<T> Members

        public int Compare(T x, T y)
        {
            // Deal with simplest cases first.
            if (x == null)
            {
                // Two null objects are equal.
                if (y == null)
                    return 0;
                // A null object is less than any non-null object.
                return -1;
            }
            else if (y == null)
            {
                // Any non-null object is greater than a null object.
                return 1;
            }

            if (sortKeys != null)
            {
                // Iterate over all the sort keys.
                for (int i = 0; i < sortKeys.Length; i++)
                {
                    object value1, value2;
                    // Read either the field or the property.
                    if (sortKeys[i].FieldInfo != null)
                    {
                        value1 = sortKeys[i].FieldInfo.GetValue(x);
                        value2 = sortKeys[i].FieldInfo.GetValue(y);
                    }
                    else
                    {
                        //if (x.GetType().Equals(sortKeys[i].PropertyInfo.ReflectedType))
                        //{
                        //    value1 = sortKeys[i].PropertyInfo.GetValue(x, null);
                        //    value2 = sortKeys[i].PropertyInfo.GetValue(y, null);
                        //}
                        //else
                        //{
                            string[] memberNameSplit = sortKeys[i].FullPropertyName.Split('.');
                            object parent = x;
                            PropertyInfo prop = null;
                            
                            prop = parent.GetType().GetProperty(memberNameSplit[0]);
                            for (int j = 1; j < memberNameSplit.Length; j++)
                            {
                                parent = prop.GetValue(parent,null);
                                prop = parent.GetType().GetProperty(memberNameSplit[j]);
                                
                            }
                            value1 = prop.GetValue(parent, null);

                            parent = y;
                            prop = parent.GetType().GetProperty(memberNameSplit[0]);
                            for (int j = 1; j < memberNameSplit.Length; j++)
                            {
                                parent = prop.GetValue(parent, null);
                                prop = parent.GetType().GetProperty(memberNameSplit[j]);

                            }
                            value2 = prop.GetValue(parent, null);
                            
                       // }
                    }

                    int res;
                    if (value1 == null && value2 == null)
                    {
                        // Two null objects are equal.
                        res = 0;
                    }
                    else if (value1 == null)
                    {
                        // A null object is always less than a non-null object.
                        res = -1;
                    }
                    else if (value2 == null)
                    {
                        // Any object is greater than a null object.
                        res = 1;
                    }
                    else if (!sortKeys[i].CaseInsensitive)
                    {
                        // Compare the two values, assuming that they support IComparable.
                        res = (value1 as IComparable).CompareTo(value2);
                    }
                    else
                    {
                        // if TEXT qualifier was used, compare strings in case-insensitive mode.
                        res = String.Compare(value1.ToString(), value2.ToString(), true);
                    }

                    // if ( values are different, return this value to caller.
                    if (res != 0)
                    {
                        // Reverse it if sort direction is descending.
                        if (sortKeys[i].Descending)
                            res = -res;
                        return res;
                    }
                }
            }
            // if we get here the two objects are equal.
            return 0;
        }

        #endregion

        // Nested type to store detail on sort keys
        private struct SortKey
        {
            // Only one of the following fields is used.
            public FieldInfo FieldInfo;
            public PropertyInfo PropertyInfo;
            // true if sort is descending.
            public bool Descending;
            // true if sort is case-insensitive.
            public bool CaseInsensitive;
            public string FullPropertyName;
            }
    }
}