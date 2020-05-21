using System;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Diagnostics;

namespace HBOS.FS.AMP.Windows.Controls
{
	/// <summary>
	/// ColumnStylesCollectionEditor - necessary so that the we can choose a custom column for the Grid using the build in designers.
	/// </summary>
	/// <remarks>
	/// <para>As additional column types are developed, their type must be added to <see cref="CreateNewItemTypes"/></para>
	/// <note>This class is never created directly, but is created by interacting with the GUI designers for the grid</note>
	/// </remarks>
	public class HBOSColumnStylesCollectionEditor : System.ComponentModel.Design.CollectionEditor 
	{
		/// <summary>
		/// Constructor which takes the column type.
		/// </summary>
		/// <param name="type">Type of Data Grid Column to create.</param>
		public HBOSColumnStylesCollectionEditor(Type type) : base(type)
		{
		}

		/// <summary>
		/// Allows the GUI to odetermine what types can be added to the grid.
		/// </summary>
		/// <returns>An array of types which we can add as columns to the DataGrid.</returns>
		protected override System.Type[] CreateNewItemTypes()
		{
			return new Type[]
				{
					typeof(DataGridTextBoxColumn), 
					typeof(DataGridBoolColumn),
					System.Type.GetType("HBOS.FS.AMP.Windows.Controls.DataGridImageColumn"),
					System.Type.GetType("HBOS.FS.AMP.Windows.Controls.DataGridComboBoxColumn"),
					System.Type.GetType("HBOS.FS.AMP.Windows.Controls.DataGridTextBoxReadOnlyColumn"),
					System.Type.GetType("HBOS.FS.AMP.Windows.Controls.DataGridNumberColumn"),
					System.Type.GetType("HBOS.FS.AMP.Windows.Controls.DataGridPercentageColumn"),	
					System.Type.GetType("HBOS.FS.AMP.Windows.Controls.DataGridBool1ClickColumn")
				};
		}
	}
}
