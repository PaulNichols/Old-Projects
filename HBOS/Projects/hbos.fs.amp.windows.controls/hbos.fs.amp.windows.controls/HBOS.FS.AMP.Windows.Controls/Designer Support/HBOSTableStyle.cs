using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace HBOS.FS.AMP.Windows.Controls
{
	/// <summary>
	/// TableStyle - necessary so that the Grid knows that we need to use a custom Column styles colleciton editor.
	/// </summary>
	/// <remarks>
	///		<para>The custom column styles collection editor is aware of our custom column types.</para>
	///		<note>This class is never created directly, but is created by interacting with the GUI designers for the grid</note>
	///	</remarks>
	public class HBOSTableStyle  : System.Windows.Forms.DataGridTableStyle 
	{
		/// <summary>
		/// Access to the GridColumnStylesCollection
		/// </summary>
		[Editor(typeof(HBOSColumnStylesCollectionEditor), typeof(UITypeEditor))]
		public new GridColumnStylesCollection GridColumnStyles 
		{
			get 
			{
				return base.GridColumnStyles;
			}
		}

		/// <summary>
		/// Create a Grid Column
		/// </summary>
		/// <param name="prop"></param>
		/// <param name="isDefault"></param>
		/// <returns></returns>
		protected override DataGridColumnStyle CreateGridColumn(PropertyDescriptor prop, bool isDefault) 
		{
			return base.CreateGridColumn(prop, isDefault);
		}
	}
}
