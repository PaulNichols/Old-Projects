using System;

namespace HBOS.FS.AMP.Windows.Controls
{
	/// <summary>
	/// Implement this interface to allow custom formatting of a property 
	/// (to be used within the grid, but this formatting could be applied elsewhere)
	/// </summary>
	public interface IPropertyDisplayFormatter
	{
		/// <summary>
		/// Provides custom formatting on a property. To use, get the property value, 
		/// then use a case statement on the property name to provide specific formatting for that property 
		/// </summary>
		/// <param name="propertyToDisplay">The property being displayed</param>
		/// <param name="propertyValue">The value of the property being displayed</param>
		/// <param name="customType">A business object that contains other data that may affect formatting (such as a Set flag)</param>
		/// <returns>The formatted value for display purposes</returns>
		string FormatProperty (System.Reflection.PropertyInfo propertyToDisplay, Object propertyValue, Object customType);

		/// <summary>
		/// Used to determine if a given property is to be custom formatted
		/// </summary>
		/// <param name="propertyName">The name of the property in question</param>
		/// <returns>boolean flag indicating whether this property is custom formatted</returns>
		bool IsCustomFormatted (string propertyName);
	}
}
