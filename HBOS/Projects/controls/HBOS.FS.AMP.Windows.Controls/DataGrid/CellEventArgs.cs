using System;
using System.Windows.Forms;

namespace HBOS.FS.AMP.Windows.Controls
{
	/// <summary>
	/// CellEventArgs are the arguments for a Cell Click (or double click) event. The information includes RowNumber, ColumnNumber and CellType.
	/// </summary>
	/// <remarks>It effectively encapsulates the information from a HitTest.</remarks>
	public class CellEventArgs
	{
		#region Variables

		int m_rowNumber;
		int m_columnNumber;
		DataGrid.HitTestType m_hitTestType;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor for the column number, row number and cell type
		/// </summary>
		/// <param name="columnNumber">Column number of clicked cell.</param>
		/// <param name="rowNumber">Row number of clicked cell.</param>
		/// <param name="hitTestType">Cell type of clicked cell (e.g. header, cell, gridline etc)</param>
		public CellEventArgs( int rowNumber , int columnNumber , DataGrid.HitTestType hitTestType )
		{
			m_rowNumber = rowNumber;
			m_columnNumber = columnNumber;
			m_hitTestType = hitTestType;
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

		#endregion
	}
}
