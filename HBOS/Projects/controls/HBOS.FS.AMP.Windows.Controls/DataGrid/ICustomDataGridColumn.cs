using System;

namespace HBOS.FS.AMP.Windows.Controls
{
	/// <summary>
	/// Interface for Data Grid custom columns
	/// </summary>
	public interface ICustomDataGridColumn
	{
		/// <summary>
		/// The tooltip property for this column
		/// </summary>
		string ToolTipProperty
		{ 
			get; 
			set; 
		}
	}
}
