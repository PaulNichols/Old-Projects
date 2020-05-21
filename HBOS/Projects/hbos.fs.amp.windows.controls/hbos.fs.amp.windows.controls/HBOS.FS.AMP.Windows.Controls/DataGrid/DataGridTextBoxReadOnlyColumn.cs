using System;
using System.Windows.Forms;

namespace HBOS.FS.AMP.Windows.Controls
{
	/// <summary>
	/// ReadOnlyColumn - doesn't allow the cell to get the focus.
	/// </summary>
	/// <remarks>Allows the row to be selected rather than the cell. Necessary if we are capturing double click events.</remarks>
	/// <example>
	///		<para>Works in the same as a normal DataGridTextBoxColumn but is unable to receive the focus</para>
	///		<code>
	///			this.dataGridTextBoxReadOnlyColumn1.MappingName = "Column1";
	///		</code>
	///	</example>
	public class DataGridTextBoxReadOnlyColumn : DataGridTextBoxColumn , ICustomDataGridColumn
	{
		#region Variables

		private int m_selectedRow = -1; 
		private string m_toolTipProperty = "";

		#endregion

		#region Methods

		/// <summary>
		/// Make sure the edit doesn't happen
		/// </summary>
		/// <param name="source">Data Source for the grid</param>
		/// <param name="rowNum">Row number affected.</param>
		/// <param name="bounds"></param>
		/// <param name="readOnly">Is the cell readonly</param>
		/// <param name="instantText"></param>
		/// <param name="cellIsVisible">Is the cell visible.</param>
		protected override void Edit(System.Windows.Forms.CurrencyManager source, int rowNum, System.Drawing.Rectangle bounds, bool readOnly, string instantText,bool cellIsVisible) 
 		{ 
 			//make sure previous selection is valid 
			if( m_selectedRow > -1 && m_selectedRow < source.List.Count + 1) 
			{
				this.DataGridTableStyle.DataGrid.UnSelect( m_selectedRow ); 
			}
 
			m_selectedRow = rowNum; 
 
			this.DataGridTableStyle.DataGrid.Select( m_selectedRow ); 
		}  

		#endregion

		#region ICustomDataGridColumn Members

		/// <summary>
		/// Tooltip property
		/// </summary>
		public string ToolTipProperty
		{
			get
			{
				return m_toolTipProperty;
			}
			set
			{
				m_toolTipProperty = value;
			}
		}

		#endregion
	}
}
