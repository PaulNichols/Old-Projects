using System;
using System.Windows.Forms;

namespace HBOS.FS.AMP.Windows.Controls
{
	/// <summary>
	/// CheckBoxEventArgs are the arguments for a checked changed event. The information includes RowNumber, ColumnNumber, CellType, Old Value and New Value
	/// </summary>
	/// <remarks>It effectively encapsulates the information from a HitTest.</remarks>
	public class CheckBoxEventArgs
	{
		#region Variables

		int m_rowNumber;
		int m_columnNumber;
		DataGrid.HitTestType m_hitTestType;
		bool m_oldValue = false;
		bool m_newValue = true;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor for the column number, row number,cell type, old value and new value
		/// </summary>
		/// <param name="columnNumber">Column number of clicked cell.</param>
		/// <param name="rowNumber">Row number of clicked cell.</param>
		/// <param name="hitTestType">Cell type of clicked cell (e.g. header, cell, gridline etc)</param>
		/// <param name="newValue"></param>
		/// <param name="oldValue"></param>
		public CheckBoxEventArgs( int rowNumber , int columnNumber , DataGrid.HitTestType hitTestType , bool oldValue , bool newValue )
		{
			m_rowNumber = rowNumber;
			m_columnNumber = columnNumber;
			m_hitTestType = hitTestType;
			m_oldValue = oldValue;
			m_newValue = newValue;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Row Number for clicked cell
		/// </summary>
		public int RowNumber
		{
			get
			{
				return m_rowNumber;
			}
		}

		/// <summary>
		/// Column number for clicked cell
		/// </summary>
		public int ColumnNumber
		{
			get
			{
				return m_columnNumber;
			}
		}

		/// <summary>
		/// Cell type for clicked cell
		/// </summary>
		public DataGrid.HitTestType Type
		{
			get
			{
				return m_hitTestType;
			}
		}

		/// <summary>
		/// Old checked value
		/// </summary>
		public bool OldValue
		{
			get
			{
				return m_oldValue;
			}
		}

		/// <summary>
		/// New check value
		/// </summary>
		public bool NewValue
		{
			get
			{
				return m_newValue;
			}
		}

		#endregion
	}
}
