using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Collections;

namespace HBOS.FS.AMP.Windows.Controls
{
	/// <summary>
	/// DataGridPrinter - Handles the logic of printing a DataView / DataGrid.
	/// </summary>
	internal class DataGridPrinter
	{
		#region Constructor

		/// <summary>
		/// Constructor for Data Grid Printer - gathers print settings.
		/// </summary>
		/// <param name="printPageHeadings">Are we print page headings</param>
		/// <param name="pageHeading">Page heading to print</param>
		/// <param name="printColumnHeadings">Are we printing column headings.</param>
		/// <param name="printPageNumbers">Are we printing page numbers</param>
		/// <param name="printInPortraitLayout">Are we printing in portrait layout</param>
		public DataGridPrinter( bool printPageHeadings, string pageHeading , bool printColumnHeadings , bool printPageNumbers , bool printInPortraitLayout )
		{
			m_printPageHeading = printPageHeadings;
			m_pageHeading = pageHeading;
			m_printColumnHeadings = printColumnHeadings;
			m_printPageNumbers = printPageNumbers;
			m_printInPortraitLayout = printInPortraitLayout;
		}

		/// <summary>
		/// Constructor for Data Grid Printer providing custom column widths
		/// </summary>
		/// <param name="printPageHeadings">Are we printing page headings</param>
		/// <param name="pageHeading">Page Heading to print</param>
		/// <param name="printColumnHeadings">Are we printing column headings.</param>
		/// <param name="printPageNumbers">Are we printing page numbers</param>
		/// <param name="printInPortraitLayout">Are we printing in portrait layout.</param>
		/// <param name="printColumnSettings"></param>
		/// <param name="columnHeadingFont"></param>
		/// <param name="pageHeadingFont"></param>
		/// <param name="standardFont"></param>
		public DataGridPrinter( bool printPageHeadings, string pageHeading , bool printColumnHeadings , bool printPageNumbers , bool printInPortraitLayout , PrintColumnSettingsCollection printColumnSettings, Font pageHeadingFont , Font columnHeadingFont , Font standardFont ) : this( printPageHeadings, pageHeading , printColumnHeadings , printPageNumbers , printInPortraitLayout )
		{
			m_printColumnSettings = printColumnSettings;
			m_printPageHeadingFont = pageHeadingFont;
			m_printColumnHeadingFont = columnHeadingFont;
			m_printStandardFont = standardFont;
		}

		#endregion

		#region Variables

		// Page settings
		private bool m_printColumnHeadings = true;
		private bool m_printPageNumbers = true;
		private bool m_printPageHeading = true;
		private bool m_printInPortraitLayout = true;
		private bool m_pageOverFlow = false;
		private int m_lastColumnPrinted = 0;
		private string m_pageHeading = String.Empty;

		private PrintDocument m_printDocument = null;
		private PrintDialog m_printDialog = null;
		private PrintPreviewDialog m_printPreviewDialog = null;

		private DataTable m_dataToPrint = null;
		private HBOS.FS.AMP.Windows.Controls.DataGrid.GridPrintStyle m_printStyle = HBOS.FS.AMP.Windows.Controls.DataGrid.GridPrintStyle.Normal;

		private const int m_verticalCellGap = 10;

		private int m_currentRowCount = 0;
		private decimal m_currentPageNumber = 1.0M;
		private PrintColumnSettingsCollection m_printColumnSettings = null;
		private int m_nextColumnToPrint = 0;

		private Font m_printPageHeadingFont = new System.Drawing.Font("Arial", 12, FontStyle.Bold);
		private Font m_printColumnHeadingFont = new System.Drawing.Font("Arial", 10, FontStyle.Bold);
		private Font m_printStandardFont = new System.Drawing.Font("Arial", 8, FontStyle.Regular);

		#endregion

		#region Methods

		/// <summary>
		/// Print the Grid using the built data table
		/// </summary>
		/// <param name="dataToPrint"></param>
		/// <param name="printStyle"></param>
		public void Print( DataTable dataToPrint , HBOS.FS.AMP.Windows.Controls.DataGrid.GridPrintStyle printStyle )
		{
			m_dataToPrint = dataToPrint;
			m_printStyle = printStyle;

			m_printDocument = new PrintDocument();
			m_printDialog = new PrintDialog();

			m_currentPageNumber = 1;
			m_currentRowCount = 0;

			// Are we portrait or landscape
			if ( m_printInPortraitLayout )
			{
				m_printDocument.DefaultPageSettings.Landscape = false;
			}
			else
			{
				m_printDocument.DefaultPageSettings.Landscape = true;
			}

			m_printDocument.PrintPage +=new PrintPageEventHandler(m_printDocument_PrintPage);

			m_printDialog.ShowHelp = false;
			m_printDialog.Document = m_printDocument;

			if ( m_printDialog.ShowDialog() == DialogResult.OK )
			{
				m_printDocument.Print();
			}

			m_printDocument.PrintPage -=new PrintPageEventHandler(m_printDocument_PrintPage);
		}

		/// <summary>
		/// Do a print preview using the built Data table
		/// </summary>
		/// <param name="dataToPrint"></param>
		/// <param name="printStyle"></param>
		public void PrintPreview( DataTable dataToPrint, HBOS.FS.AMP.Windows.Controls.DataGrid.GridPrintStyle printStyle )
		{
			m_dataToPrint = dataToPrint;
			m_printStyle = printStyle;

			m_printDocument = new PrintDocument();
			m_printPreviewDialog = new PrintPreviewDialog();

			m_currentPageNumber = 1;
			m_currentRowCount = 0;

			// Are we portrait or landscape
			if ( m_printInPortraitLayout )
			{
				m_printDocument.DefaultPageSettings.Landscape = false;
			}
			else
			{
				m_printDocument.DefaultPageSettings.Landscape = true;
			}

			m_printDocument.PrintPage +=new PrintPageEventHandler(m_printDocument_PrintPage);

			m_printPreviewDialog.ClientSize = new System.Drawing.Size(600, 500);
			m_printPreviewDialog.Location = new System.Drawing.Point(29, 29);
			m_printPreviewDialog.MinimumSize = new System.Drawing.Size(400, 250);
			m_printPreviewDialog.UseAntiAlias = true;

			m_printPreviewDialog.Document = m_printDocument;

			m_printPreviewDialog.ShowDialog();

			m_printDocument.PrintPage +=new PrintPageEventHandler(m_printDocument_PrintPage);
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Calculate the column width - necessqry in case the user didn't give us enough column widths so we might need to default
		/// </summary>
		/// <param name="fieldName"></param>
		/// <param name="defaultColumnWidth"></param>
		/// <returns></returns>
		/// <remarks>Use a column width of 0 (or negative) to hide a column</remarks>
		private int calculateColumnWidth( string fieldName , int defaultColumnWidth )
		{
			int columnWidth = 0;

			// Are we using Custom column widths
			if ( m_printColumnSettings != null )
			{
				// Have we been given a width
				if ( m_printColumnSettings.Contains( fieldName ) )
				{
					PrintColumnSettings columnSettings = (PrintColumnSettings)m_printColumnSettings[ fieldName ];
					columnWidth = columnSettings.Width;
				}
				else
				{
					columnWidth = defaultColumnWidth;
				}
			}
			else
			{
				columnWidth = defaultColumnWidth;
			}

			return columnWidth;
		}

		/// <summary>
		/// Calculate the total Row Width
		/// </summary>
		/// <param name="defaultColumnWidth"></param>
		/// <param name="totalRowWidth"></param>
		/// <returns></returns>
		private int calculateRowWidth( int defaultColumnWidth , int totalRowWidth )
		{
			int defaultTotalColumnWidths = 0;
			int totalColumnWidths = 0;

			// Assume we were given the correct number of widths
			if ( m_printColumnSettings != null )
			{
				foreach( DictionaryEntry indexEntry in m_printColumnSettings )
				{
					PrintColumnSettings columnSettings = (PrintColumnSettings)indexEntry.Value;
					defaultTotalColumnWidths += columnSettings.Width;
				}

				// Were we given the right number of columns
				if ( m_printColumnSettings.Count == m_dataToPrint.Columns.Count )
				{
					totalColumnWidths = defaultTotalColumnWidths;
				}
				else
				{
					// Were we given to many
					if ( m_printColumnSettings.Count > m_dataToPrint.Columns.Count )
					{
						// Use as many as necessary
						for( int index = 0 ; index < m_dataToPrint.Columns.Count ; index ++ )
						{
							string fieldName = m_dataToPrint.Columns[ index ].ColumnName;
							if ( m_printColumnSettings.Contains( fieldName ) )
							{
								DictionaryEntry dictionaryEntry = (DictionaryEntry)m_printColumnSettings[ fieldName ];
								totalColumnWidths += ((PrintColumnSettings)dictionaryEntry.Value).Width;
							}
							else
							{
								totalColumnWidths += defaultColumnWidth;
							}
						}
					}
					else
					{
						// We were given to few, so set the remainder to default widths
						totalColumnWidths = defaultTotalColumnWidths;
						for( int remainderColumns = m_printColumnSettings.Count ; remainderColumns < m_dataToPrint.Columns.Count ; remainderColumns ++ )
						{
							totalColumnWidths += defaultColumnWidth;
						}
					}
				}
			}
			else
			{
				totalColumnWidths = totalRowWidth;
			}

			return totalColumnWidths;
		}

		#endregion

		#region Event Handlers

		/// <summary>
		/// Print each page - needs to handle page headers, column headers, data rows and page numbers.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_printDocument_PrintPage(object sender, PrintPageEventArgs e)
		{
			bool moreSpaceOnPage = true;
			m_pageOverFlow = false;
			int currentTopPosition = 20;
			int currentLeftPosition = 0;

			int pageWidth = e.PageBounds.Size.Width;

			int defaultColumnWidth = pageWidth / m_dataToPrint.Columns.Count;

			int rowWidth = calculateRowWidth( defaultColumnWidth , e.PageBounds.Size.Width );

			// Print out page header
			if ( m_printPageHeading )
			{
				e.Graphics.DrawString( m_pageHeading , m_printPageHeadingFont , Brushes.Black , currentLeftPosition , currentTopPosition , new StringFormat() );
				currentTopPosition += 30;
			}

			// Sort out column headings
			if ( m_printColumnHeadings )
			{
				RectangleF HeaderBounds  = new RectangleF(currentLeftPosition, currentTopPosition, rowWidth , m_printColumnHeadingFont.Height );
				e.Graphics.FillRectangle( Brushes.Black, HeaderBounds);

				for( int columnNumber = m_nextColumnToPrint ; columnNumber < m_dataToPrint.Columns.Count ; columnNumber ++ )
				{
					int columnWidth = this.calculateColumnWidth( m_dataToPrint.Columns[ columnNumber ].ColumnName , defaultColumnWidth );

					// Will we go off the edge of the page
					if ( currentLeftPosition + columnWidth > pageWidth  )
					{
						m_pageOverFlow = true;
						if ( this.m_printStyle == HBOS.FS.AMP.Windows.Controls.DataGrid.GridPrintStyle.Normal )
						{
							currentLeftPosition = 0;
							RectangleF subHeaderBounds  = new RectangleF(currentLeftPosition, currentTopPosition + m_printColumnHeadingFont.Height, rowWidth , 20 );
							e.Graphics.FillRectangle( Brushes.Black, subHeaderBounds);

							currentTopPosition += 20;
						}
						else
						{
							break;
						}
					}

					if ( columnWidth != 0 )
					{
						DataColumn indexColumn = m_dataToPrint.Columns[ columnNumber ];
						string columnName = indexColumn.ColumnName.ToString();
						string headingToPrint = columnName;
						
						if ( m_printColumnSettings != null && m_printColumnSettings.Contains( columnName ) )
						{
							headingToPrint = ((PrintColumnSettings)m_printColumnSettings[ columnName ]).HeaderText;
						}

						StringFormat headingFormat = new StringFormat();
						headingFormat.Alignment = StringAlignment.Center;

						e.Graphics.DrawString( headingToPrint , m_printColumnHeadingFont, Brushes.White , new RectangleF( new PointF( currentLeftPosition , currentTopPosition ) , new SizeF( columnWidth , m_printColumnHeadingFont.Height ) ) , headingFormat );
					}

					currentLeftPosition += columnWidth;
				}				
			}

			// For each row in the Table - print
			int rowIndex = m_currentRowCount;
			for( rowIndex = m_currentRowCount ; rowIndex < m_dataToPrint.Rows.Count && moreSpaceOnPage == true  ; rowIndex ++)
			{
				currentTopPosition += 20;
				currentLeftPosition = 0;
				Brush backgroundColor = Brushes.White;

				DataRow myRow = m_dataToPrint.Rows[ rowIndex ];

				// Print a background on even rows
				if ( rowIndex % 2 == 0)
				{
					backgroundColor = Brushes.LightGray;
					RectangleF rowBounds = new RectangleF(currentLeftPosition, currentTopPosition - 5, rowWidth , m_printStandardFont.Height + 5 );
					e.Graphics.FillRectangle( backgroundColor, rowBounds);
				}

				// Print each column
				for( int columnNumber = m_nextColumnToPrint; columnNumber < m_dataToPrint.Columns.Count ; columnNumber ++ )
				{
					int columnWidth = this.calculateColumnWidth( m_dataToPrint.Columns[ columnNumber ].ColumnName, defaultColumnWidth );

					// Will we go off the edge of the page
					if ( currentLeftPosition + columnWidth > pageWidth  )
					{
						if ( this.m_printStyle == HBOS.FS.AMP.Windows.Controls.DataGrid.GridPrintStyle.Normal )
						{
							currentLeftPosition = 0;
							RectangleF subHeaderBounds  = new RectangleF(currentLeftPosition, currentTopPosition + m_printStandardFont.Height, rowWidth , 20 );
							e.Graphics.FillRectangle( backgroundColor, subHeaderBounds);

							currentTopPosition += 20;
						}
						else
						{
							break;
						}
					}

					// Do we need to print the column
					if ( columnWidth > 0 )
					{
						DataColumn indexColumn = m_dataToPrint.Columns[ columnNumber ];
						string data = myRow[ indexColumn ].ToString();
						StringAlignment textAlignment = StringAlignment.Near;
						StringFormat columnFormat = new StringFormat();
						Font printFontToUse = m_printStandardFont;

						if ( m_printColumnSettings != null && m_printColumnSettings.Contains( indexColumn.ColumnName ) )
						{
							textAlignment = ((PrintColumnSettings)m_printColumnSettings[ indexColumn.ColumnName ]).Alignment;
							printFontToUse = ((PrintColumnSettings)m_printColumnSettings[ indexColumn.ColumnName ]).DetailFont;
						}
						columnFormat.Alignment = textAlignment;

						e.Graphics.DrawString( data , printFontToUse, Brushes.Black , new RectangleF( new PointF( currentLeftPosition , currentTopPosition ) , new Size( columnWidth , printFontToUse.Height ) ) , columnFormat );
					}

					currentLeftPosition += columnWidth;
					m_lastColumnPrinted = columnNumber;
				}

				if ( currentTopPosition + m_printStandardFont.Height + 100 < e.PageBounds.Size.Height )
				{
					moreSpaceOnPage = true;
				}
				else
				{
					// End of Page so do we write a page number
					moreSpaceOnPage = false;
				}
			}


			// Write a page number if we are going to
			if ( m_printPageNumbers )
			{
				e.Graphics.DrawString( m_currentPageNumber.ToString() , m_printPageHeadingFont, Brushes.Black , e.PageBounds.Size.Width / 2 , e.PageBounds.Size.Height - 60 , new StringFormat() );
			}

			// We've finished Printing
			if ( moreSpaceOnPage == true )
			{
				// Need to remember to print the next page
				if ( m_pageOverFlow && this.m_printStyle == HBOS.FS.AMP.Windows.Controls.DataGrid.GridPrintStyle.MultipleHorizontalPages )
				{
					// Is this a right hand or left hand page...
					if ( m_lastColumnPrinted == m_dataToPrint.Columns.Count )
					{
						// We have printed the last right hand page for the last page so finish !
						m_pageOverFlow = false;
						m_lastColumnPrinted = 0;
						m_nextColumnToPrint = 0;
						e.HasMorePages = false;
						m_currentRowCount = 0;
						m_currentPageNumber = 1;
					}
					else
					{
						// More right hand pages to print
						m_nextColumnToPrint = m_lastColumnPrinted + 1;
						m_currentPageNumber += .1M;
						e.HasMorePages = true;
					}
				}
				else
				{
					// No overflow and no more pages to print
					m_pageOverFlow = false;
					m_lastColumnPrinted = 0;
					m_nextColumnToPrint = 0;
					e.HasMorePages = false;
					m_currentRowCount = 0;
					m_currentPageNumber = 1;
				}
			}
			else
			{
				e.HasMorePages = true;

				if ( m_pageOverFlow && this.m_printStyle == HBOS.FS.AMP.Windows.Controls.DataGrid.GridPrintStyle.MultipleHorizontalPages )
				{
					// Is this a right hand or left hand page...
					if ( m_lastColumnPrinted == m_dataToPrint.Columns.Count )
					{
						m_currentRowCount = rowIndex;
						m_currentPageNumber = decimal.Floor( m_currentPageNumber);
						m_currentPageNumber ++;
						m_lastColumnPrinted = 0;
						m_nextColumnToPrint = 0;
					}
					else
					{
						m_nextColumnToPrint = m_lastColumnPrinted + 1;
						m_currentPageNumber += .1M;
					}
				}
				else
				{
					// Printing a new page and no overflow
					m_currentRowCount = rowIndex;
					m_currentPageNumber = decimal.Floor( m_currentPageNumber);
					m_currentPageNumber ++;
					m_lastColumnPrinted = 0;
					m_nextColumnToPrint = 0;
				}
			}
		}

		#endregion
	}
}
