using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace HBOS.FS.AMP.Windows.Controls
{
	/// <summary>
	/// DataGridImageColumn - allow images to be displayed in Data Grid Column
	/// </summary>
	/// <remarks>
	/// <p>Based upon http://www.datagridcolumnstyles.net/easyDataGrid.asp</p>
	/// <p>It assumes the underlying data is an enum. The images displayed are stored in the ImageList and the image displayed is the associated position in the ImageList</p>
	/// <p>Both the test associated with the enum and the icon itself are displayed.</p>
	/// </remarks>
	/// <example>
	/// An imagelist must be dropped on the from, appropriate images added to the ImageList and the ImageList assigned to the ImageList property of the GridImageColumn.
	/// The Type of the enum must also be assigned to the GridImageColumn so it can load the appropriate image for the appropriate enum value.
	/// <code escaped="true">
	/// dataGridImageColumn1.EnumType = typeof(Severity);
	/// this.dataGridImageColumn1.ImageList = this.imageList1;
	/// </code>
	/// </example>
	public class DataGridImageColumn : DataGridTextBoxColumn , ICustomDataGridColumn	
	{
		#region Private Member Variables

		private ImageList m_imageList = null;
		private int m_selectedRow = -1; 
		private System.Type m_enum;
		private string m_toolTipProperty = "";

		#endregion

		#region Properties

		/// <summary>
		/// Set the Grids Image Columns associated ImageList.
		/// </summary>
		[
		Browsable(true),
		CategoryAttribute("Custom"),
		Description("Specify an Image List for the grid to use")
		]
		public ImageList ImageList
		{
			set
			{
				m_imageList = value;
			}
			get
			{
				return m_imageList;
			}
		}

		/// <summary>
		/// Set the Grids Image Columns associated enum type.
		/// </summary>
		[
		Browsable(true),
		CategoryAttribute("Custom"),
		Description("Specify the Enum Type for the column")
		]
		public System.Type EnumType
		{
			set
			{
				m_enum = value;
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Don't allow editing
		/// </summary>
		/// <param name="source"></param>
		/// <param name="rowNum"></param>
		/// <param name="bounds"></param>
		/// <param name="readOnly"></param>
		/// <param name="instantText"></param>
		/// <param name="cellIsVisible"></param>
		protected override void Edit(System.Windows.Forms.CurrencyManager source, int rowNum, System.Drawing.Rectangle bounds, bool readOnly, string instantText, bool cellIsVisible)
		{
			//make sure previous selection is valid 
			if( m_selectedRow > -1 && m_selectedRow < source.List.Count + 1) 
			{
				this.DataGridTableStyle.DataGrid.UnSelect( m_selectedRow ); 
			}
 
			m_selectedRow = rowNum; 
 
			this.DataGridTableStyle.DataGrid.Select( m_selectedRow ); 
		}

		/// <summary>
		/// Given a row, get the value member associated with a row.  Use the ImageList
		/// to find the associated display image
		/// </summary>
		/// <param name="source">The Data source of the grid.</param>
		/// <param name="rowNum">The row number we are painting.</param>
		/// <returns>The Image to paint for this cell.</returns>
		protected override object GetColumnValueAtRow(System.Windows.Forms.CurrencyManager source, int rowNum)
		{
			// Make sure we have an ImageList
			if ( this.m_imageList == null )
			{
				throw new Exception( "You must supply an ImageList for the ImageColumn" );
			}

			if ( m_enum == null )
			{
				throw new Exception( "You must specify the Enum type for the ImageColumn" );
			}

			System.Diagnostics.Debug.WriteLine( "Before GetColumnValueAtRow" );

			// Given a row number in the datagrid, get the display member
			object cellValue =  base.GetColumnValueAtRow(source, rowNum);

			System.Diagnostics.Debug.WriteLine( "After GetColumnValueAtRow " + cellValue.ToString() );

			object temp = Enum.Parse( m_enum , cellValue.ToString() );

			System.Diagnostics.Debug.WriteLine( "After Enum.Parse " + temp.ToString() );

			int numericCellValue = Convert.ToInt32( temp );

			System.Diagnostics.Debug.WriteLine( "After Cast");

			
			// Iterate through the imagelist looking for the index
			int i;

			for (i = 0; i < m_imageList.Images.Count; i++)
			{
				if ( numericCellValue.Equals( i ) )
					break;
			}
			
			if (i < m_imageList.Images.Count)
				return ImageList.Images[ i ];
			
			return DBNull.Value;
		}

		/// <summary>
		/// Given a row, get the text to display which will be theenum value.
		/// </summary>
		/// <param name="source">The data source for the grid.</param>
		/// <param name="rowNum">The row number affected.</param>
		/// <returns>The text to display for this cell which is the enum value.</returns>
		protected string GetColumnStringValueAtRow(System.Windows.Forms.CurrencyManager source, int rowNum)
		{
			// Given a row number in the datagrid, get the display member
			object cellValue =  base.GetColumnValueAtRow(source, rowNum);

			return cellValue.ToString();
		}



		/// <summary>
		/// Given a row iterate over ImageList to find
		/// the associated value member.  Set this value member.
		/// </summary>
		/// <param name="source">The data source of the grid</param>
		/// <param name="rowNum">The row number affected.</param>
		/// <param name="value">The value to specify for this cell.</param>
		protected override void SetColumnValueAtRow(System.Windows.Forms.CurrencyManager source, int rowNum, object value)
		{
			// Just set the value to the index location
			object s = value;

			base.SetColumnValueAtRow(source, rowNum, s);
		}

		/// <summary>
		/// Paint the image with the appropriate icon and text.
		/// </summary>
		/// <param name="g"></param>
		/// <param name="bounds"></param>
		/// <param name="source"></param>
		/// <param name="rowNum"></param>
		/// <param name="backBrush"></param>
		/// <param name="foreBrush"></param>
		/// <param name="alignToRight"></param>
		protected override void Paint(System.Drawing.Graphics g, System.Drawing.Rectangle bounds,
			System.Windows.Forms.CurrencyManager source, int rowNum, System.Drawing.Brush backBrush,
			System.Drawing.Brush foreBrush, bool alignToRight)
		{
			System.Diagnostics.Debug.WriteLine( "Paint 1" );

			object imageToDraw = this.GetColumnValueAtRow(source, rowNum);

			System.Diagnostics.Debug.WriteLine( "Paint 2" );

			string stringToDraw = this.GetColumnStringValueAtRow(source, rowNum);

			System.Diagnostics.Debug.WriteLine( "Paint 3" );

			// if the current row is this row, draw the selection back color
			if (this.DataGridTableStyle.DataGrid.CurrentRowIndex == rowNum)
				g.FillRectangle(new SolidBrush(this.DataGridTableStyle.SelectionBackColor), bounds);
			else
				g.FillRectangle(backBrush, bounds);

			System.Diagnostics.Debug.WriteLine( "Paint 4" );

			g.DrawImage( (Image)imageToDraw, new Point(bounds.X, bounds.Y));
			g.DrawString( stringToDraw, this.DataGridTableStyle.DataGrid.Font, foreBrush , bounds.X + 20, bounds.Y + 2);

			System.Diagnostics.Debug.WriteLine( "Paint 5" );
		}

		#endregion

		#region ICustomDataGridColumn Members

		/// <summary>
		/// Tooltip rpoperty
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
