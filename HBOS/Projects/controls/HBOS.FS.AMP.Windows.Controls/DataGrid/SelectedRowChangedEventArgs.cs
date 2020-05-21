using System;

namespace HBOS.FS.AMP.Windows.Controls
{
	/// <summary>
	/// SelectedRowChangedEventArgs - information for changing rows in a dataGrid
	/// </summary>
	public class SelectedRowChangedEventArgs
	{
		#region Variables

		int m_oldRowNumber = -1;
		int m_newRowNumber = -1;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor for a Row Changed Event Argument
		/// </summary>
		/// <param name="oldRowNumber">The old row number</param>
		/// <param name="newRowNumber">The new row number</param>
		public SelectedRowChangedEventArgs( int oldRowNumber , int newRowNumber )
		{
			m_oldRowNumber = oldRowNumber;
			m_newRowNumber = newRowNumber;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The old row number
		/// </summary>
		public int OldRowNumber
		{
			get
			{
				return m_oldRowNumber;
			}
		}

		/// <summary>
		/// The new row number
		/// </summary>
		public int NewRowNumber
		{
			get
			{
				return m_newRowNumber;
			}
		}


		#endregion
	}
}
