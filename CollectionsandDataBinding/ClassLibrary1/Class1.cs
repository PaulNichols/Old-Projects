using System.Reflection;
using System.Collections;
using System;

public class UniversalComparer : IComparer 
{
	//private Type type;
	private SortKey[] sortKeys;

	public UniversalComparer(Type type, 
		string sort) 
	{
		// Split the list of properties.
		string[] props = sort.Split(',');
		// Prepare the array that holds information 
		// on sort criteria.
		sortKeys = new SortKey[props.Length];

		// Parse the sort string.
		for ( int i = 0; i < props.Length; i++ )
		{
			// Get the N-th member name.
			string memberName = props[i].Trim();
			if ( memberName.ToLower().EndsWith( 
				" desc") ) 
			{
				// Discard the DESC qualifier.
				sortKeys[i].Descending = true;
				memberName = memberName.Substring(0, 
					memberName.Length - 5).TrimEnd();
			}
			// Search for a property with this name.
			sortKeys[i].PropertyInfo = 
				type.GetProperty(memberName);
		} 
	}

	// This procedure is invoked when comparing 
	// two objects.

	public int Compare( object o1, object o2) 
	{
		// Deal with simplest cases first.
		if ( o1 == null ) 
		{
			// Two null objects are equal.
			if ( o2 == null ) 
				return 0;
			// A null object is less than any 
			// non-null object.
			return -1;
		} 
		else if ( o2 == null ) 
		{
			// Any non-null object is greater than 
			// a null object.
			return 1;
		}

		// Iterate over all the sort keys.
		for ( int i = 0 ; i < sortKeys.Length; i++)
		{
			object value1, value2;
			value1 = sortKeys[i].
				PropertyInfo.GetValue(o1, null);
			value2 = sortKeys[i].
				PropertyInfo.GetValue(o2, null);

			int res;
			if ( value1 == null && value2 == null ) 
			{
				// Two null objects are equal.
				res = 0;
			} 
			else if ( value1 == null ) 
			{
				// A null object is always less than 
				// a non-null object.
				res = -1;
			} 
			else if ( value2 == null ) 
			{
				// Any object is greater than a null 
				// object.
				res = 1;
			} 
			else 
			{
				// Compare the two values, assuming 
				// that they support IComparable.
				res = (value1 as IComparable).
					CompareTo(value2);
			} 

			// if ( values are different, return 
			// this value to caller.
			if ( res != 0 ) 
			{
				// negate it if sort is descending.
				if ( sortKeys[i].Descending ) 
					res = -res;
				return res;
			}
		} 
		// the two objects are equal.
		return 0;
	}

	// Nested type to store detail on sort keys
	private struct SortKey
	{
		public PropertyInfo PropertyInfo;
		// true if sort is descending.
		public bool Descending;
	} 
}

