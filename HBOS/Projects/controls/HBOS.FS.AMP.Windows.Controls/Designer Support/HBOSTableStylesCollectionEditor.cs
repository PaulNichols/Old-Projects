using System;
using System.ComponentModel.Design;

namespace HBOS.FS.AMP.Windows.Controls
{
	/// <summary>
	/// TableStylesCollectionEditor - Editor for the Table Styles
	/// </summary>
	/// <remarks>
	/// <para>A custom TableStylesCollectionEditor is required, so that the custom HBOSTableStyle can be called. This ensures that our custom columns are made 
	/// available for selection by the developer when using the GUI designer for the DataGrid</para>
	/// <note>This class is never created directly, but is created by interacting with the GUI designers for the grid</note>
	/// </remarks>
	public class HBOSTableStylesCollectionEditor : System.ComponentModel.Design.CollectionEditor 
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="type"></param>
		public HBOSTableStylesCollectionEditor(Type type) : base(type)
		{
		}

		/// <summary>
		/// Create a New Item Types
		/// </summary>
		/// <returns>The HBOS Table Style which is aware of our custom Data Grid Column Styles.</returns>
		protected override System.Type[] CreateNewItemTypes() 
		{
			return new Type[] {typeof(HBOSTableStyle)};
		}
	}
}
