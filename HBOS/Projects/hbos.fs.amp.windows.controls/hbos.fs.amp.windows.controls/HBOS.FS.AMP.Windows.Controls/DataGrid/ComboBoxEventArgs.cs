using System;

namespace HBOS.FS.AMP.Windows.Controls
{
	/// <summary>
	/// ComboBoxEventArgs - arguments raised when the Combo box changes values
	/// </summary>
	public class ComboBoxEventArgs
	{
		#region Variables

		int m_rowNumber;
		object m_comboSelectedValue;
		object m_comboSelectedDisplay;

		#endregion

		#region Constructor

		/// <summary>
		/// Create a Combo Event Args
		/// </summary>
		/// <param name="rowNumber">row number in grid</param>
		/// <param name="comboSelectedValue">selected value</param>
		/// <param name="comboSelectedDisplay">selected text</param>
		public ComboBoxEventArgs( int rowNumber , object comboSelectedValue , object comboSelectedDisplay )
		{
			m_rowNumber = rowNumber;
			m_comboSelectedValue = comboSelectedValue;
			m_comboSelectedDisplay = comboSelectedDisplay;
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
		/// Selected Value
		/// </summary>
		public object SelectedValue
		{
			get
			{
				return m_comboSelectedValue;
			}
		}

		/// <summary>
		/// Selected Text that is currently displayed
		/// </summary>
		public object SelectedDisplay
		{
			get
			{
				return m_comboSelectedDisplay;
			}
		}

		#endregion
	}
}
