using System;
using System.Collections.Specialized;

namespace HBOS.FS.AMP.Windows.Controls
{
	/// <summary>
	/// A collection of PrintColumnSettings.
	/// </summary>
	public class PrintColumnSettingsCollection : HybridDictionary
	{
		/// <summary>
		/// Add a setting to the collection
		/// </summary>
		/// <param name="settingsToAdd"></param>
		public void Add ( PrintColumnSettings settingsToAdd )
		{
			base.Add( settingsToAdd.Field , settingsToAdd );
		}
	}
}
