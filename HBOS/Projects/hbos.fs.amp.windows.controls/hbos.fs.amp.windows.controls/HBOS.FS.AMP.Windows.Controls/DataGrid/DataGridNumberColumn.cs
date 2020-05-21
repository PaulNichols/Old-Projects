using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace HBOS.FS.AMP.Windows.Controls
{
	/// <summary>
	/// DataGridPercentageColumn - numbers displayed as percentages but stored as fraction
	/// </summary>
	public class DataGridNumberColumn  : DataGridTextBoxColumn , ICustomDataGridColumn
	{
		#region Variables

		/// <summary>
		/// The number type for the column
		/// </summary>
		protected NumberType m_numberType = NumberType.Standard;

		/// <summary>
		/// The number of decimal places to use.
		/// </summary>
		protected int m_decimalPlaces = 2;

		/// <summary>
		/// The regular expression to use for validating the key presses
		/// </summary>
		protected Regex	m_maskExpression = null;

		/// <summary>
		/// Allow negative numbers
		/// </summary>
		protected bool m_allowNegative = false;

		/// <summary>
		/// Tool tip property
		/// </summary>
		protected string m_toolTipProperty = "";

		#endregion

		#region Properties

		/// <summary>
		/// Number of decimal places
		/// </summary>
		[
		Browsable(true),
		CategoryAttribute("Custom"),
		Description("Number of decimal places."),
		DefaultValue("2")
		]
		public virtual int DecimalPlaces
		{
			get
			{
				return m_decimalPlaces;
			}
			set
			{
				m_decimalPlaces = value;
			}
		}
		
		/// <summary>
		/// Allow negative numbers
		/// </summary>
		[
		Browsable(true),
		CategoryAttribute("Custom"),
		Description("Allow negative numbers."),
		DefaultValue(false)
		]
		public bool AllowNegative
		{
			get
			{
				return m_allowNegative;
			}
			set
			{
				m_allowNegative = value;
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Edit
		/// </summary>
		/// <param name="source"></param>
		/// <param name="rowNum"></param>
		/// <param name="bounds"></param>
		/// <param name="readOnly"></param>
		/// <param name="instantText"></param>
		/// <param name="cellIsVisible"></param>
		protected override void Edit(System.Windows.Forms.CurrencyManager source, int rowNum, System.Drawing.Rectangle bounds, bool readOnly, string instantText, bool cellIsVisible)
		{
			if (!readOnly && cellIsVisible)
			{
				base.Edit(source, rowNum, bounds, readOnly, instantText, cellIsVisible);
			}

			base.TextBox.KeyPress +=new KeyPressEventHandler(TextBox_KeyPress);
		}


		/// <summary>
		/// Commit the change
		/// </summary>
		/// <param name="source"></param>
		/// <param name="rowNum"></param>
		/// <returns></returns>
		protected override bool Commit(System.Windows.Forms.CurrencyManager source, int rowNum)
		{
			bool returnValue = base.Commit( source , rowNum );
			base.TextBox.KeyPress -=new KeyPressEventHandler(TextBox_KeyPress);

			return returnValue;
		}

		/// <summary>
		/// GetColumnValueAtRow
		/// </summary>
		/// <param name="source"></param>
		/// <param name="rowNum"></param>
		/// <returns></returns>
		protected override object GetColumnValueAtRow(System.Windows.Forms.CurrencyManager source, int rowNum)
		{
			object obj =  base.GetColumnValueAtRow(source, rowNum);

			if ( obj != null && obj != DBNull.Value )
			{
				decimal numericValue = Convert.ToDecimal( obj.ToString() );

				return numericValue.ToString( calculateFormatMask() );
			}
			else
			{
				return DBNull.Value;
			}
		}

		/// <summary>
		/// SetColumnValueAtRow
		/// </summary>
		/// <param name="source"></param>
		/// <param name="rowNum"></param>
		/// <param name="cellValue"></param>
		protected override void SetColumnValueAtRow(System.Windows.Forms.CurrencyManager source, int rowNum, object cellValue)
		{
			decimal cellDecimalValue = 0;

			if ( cellValue.ToString() != "" )
			{
				cellDecimalValue = Convert.ToDecimal( cellValue.ToString() );
			}
			 
			base.SetColumnValueAtRow(source, rowNum, cellDecimalValue.ToString() );
		}

		/// <summary>
		/// Make sure the user only types valid values.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = !validCharacter( e.KeyChar );
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Is it a valid character.
		/// </summary>
		/// <param name="proposedCharacter"></param>
		/// <returns></returns>
		protected bool validCharacter( char proposedCharacter )
		{
			if( proposedCharacter == Convert.ToChar( ((int)Keys.Back) ) )	// Backspace
				return true;

			string text = this.TextBox.Text.Remove( this.TextBox.SelectionStart, this.TextBox.SelectionLength );
			text = text.Insert( this.TextBox.SelectionStart, proposedCharacter.ToString() );

			bool valid = validString( text );

			return valid;
		}

		/// <summary>
		/// Is it a valid string
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		private bool validString( string text )
		{
			if ( m_maskExpression == null )
			{
				if ( this.m_allowNegative )
				{
					m_maskExpression = new Regex( TextValidator.NegativeRealRegEx( m_decimalPlaces) );
				}
				else
				{
					m_maskExpression = new Regex( TextValidator.PositiveRealRegEx( m_decimalPlaces) );
				}
			}

			bool valid = m_maskExpression.IsMatch( text );

			return valid;
		}

		/// <summary>
		/// Calculate the format mask
		/// </summary>
		/// <returns></returns>
		private string calculateFormatMask()
		{
			return "n" + m_decimalPlaces.ToString();
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
