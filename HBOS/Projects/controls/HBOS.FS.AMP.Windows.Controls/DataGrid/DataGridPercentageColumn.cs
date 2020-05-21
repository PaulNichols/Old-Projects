using System;
using System.Windows.Forms;
using System.ComponentModel;

namespace HBOS.FS.AMP.Windows.Controls
{
	/// <summary>
	/// DataGridPercentageColumn - Column for displaying percentages
	/// </summary>
	public class DataGridPercentageColumn : DataGridNumberColumn 
	{
		#region Constructor

		/// <summary>
		/// Constructor which initialises the type to a percentage column
		/// </summary>
		public DataGridPercentageColumn()
		{
			this.m_numberType = NumberType.Percentage;
		}

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
		public override int DecimalPlaces
		{
			get
			{
				// 2 less than the base number as we are displaying a percentage
				return base.DecimalPlaces - 2;
			}
			set
			{
				// 2 more for the the base number as we are displaying a percentage
				base.DecimalPlaces = value + 2;
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

			// Remove the % - This is nasty - would like to have used regular expressions but gave up
			if ( base.TextBox.Text.IndexOf( "%" ) > -1 )
			{
				decimal cellValue = Decimal.Parse( base.TextBox.Text.Substring( 0 , base.TextBox.Text.Length -1 ) );
				base.TextBox.Text = cellValue.ToString();
				base.TextBox.SelectionStart = 0;
				base.TextBox.SelectionLength = base.TextBox.Text.Length;
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
			 
			base.SetColumnValueAtRow(source, rowNum, cellDecimalValue / 100 );

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
		/// Calculate the format mask
		/// </summary>
		/// <returns></returns>
		private string calculateFormatMask()
		{
			return "p" + this.DecimalPlaces.ToString();
		}

		#endregion

	}
}
