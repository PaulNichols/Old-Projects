using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Data;
using System.Windows.Forms;
using System.Reflection;
using System.Xml;

using HBOS.FS.AMP.Entities;

namespace HBOS.FS.AMP.Windows.Controls
{
	/// <summary>
	/// <p>Customised HBOS FS AMP Data Grid.</p>
	/// <p>Extends the native DataGrid functionality by providing Write (Extract to file), Print and Print Preview functionality.</p>
	/// </summary>
	/// <remarks>
	/// <p>Methods added to the grid include</p>
	/// <list type="bullet">
	///		<item><see cref="Print"/></item>
	///		<item><see cref="PrintPreview"/></item>
	///		<item><see cref="Write"/></item>
	///		<item><see cref="CopyDefaultTableStyle"/></item>
	///		<item><see cref="BindToCustomCollection"/></item>
	///		<item><see cref="RetrieveOriginalCustomCollection"/></item>
	///		<item><see cref="RetrieveUpdatedCustomCollection"/></item>
	/// </list>
	/// <p>Events added to the grid include:</p>
	/// <list type="bullet">
	///		<item><see cref="CellClicked"/></item>
	///		<item><see cref="CellDoubleClicked"/></item>
	/// </list>
	/// <p>Note: If you add a tablestyle to the grid, it will overwrite the default styling of the grid. If you wish to reapply the default datagrid style, call the <see cref="CopyDefaultTableStyle"/> method.</p>
	/// </remarks>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(DataGrid))]
	public class DataGrid : System.Windows.Forms.DataGrid
	{
		#region Designer Functionality

		/// <summary>
		/// Support for custom designer - needed so we can use the GUI to choose our custom columns.
		/// </summary>
		[Editor(typeof(HBOSTableStylesCollectionEditor), typeof(UITypeEditor))]
		public new GridTableStylesCollection TableStyles 
		{
			get {return base.TableStyles;}
		}

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor to initialise component
		/// </summary>
		public DataGrid() : base()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

		#endregion

		#region Variables

		/// <summary>
		/// What print style to use. Allows scope for multiple print styles
		/// </summary>
		public enum GridPrintStyle : int
		{
			/// <summary>
			/// Normal Print Style with Wrapping if necessary
			/// </summary>
			Normal = 0,
			/// <summary>
			/// Multiple horizontal pages.
			/// </summary>
			MultipleHorizontalPages
		}

		private bool m_printColumnHeadings = true;
		private bool m_printPageNumbers = true;
		private bool m_printInPortraitLayout = false;
		private bool m_printPageHeader = true;

		private DataView m_dataViewToPrint = null;

		private int m_columnNumber = 0;
		private int m_rowNumber = -1;
		private int m_currentToolTipRowNumber = -1;
		private int m_oldRowNumber = -1;

		private System.Windows.Forms.DataGrid.HitTestType m_hitTestType = System.Windows.Forms.DataGrid.HitTestType.None;

		private IList m_OriginalCollection = null; 
		private DataTable m_ConvertedDataTable = null;

		private GridPrintStyle m_gridPrintStyle;

		private bool m_allowDelete = false;
		private bool m_allowNew = false;

		private System.Type m_collectionIndexerType = null;

		private PrintColumnSettingsCollection m_printColumnSettings = null;
		private Font m_pageHeadingFont = new Font( "Arial" , 12 , FontStyle.Bold );
		private Font m_columnHeadingFont = new Font( "Arial" , 10 , FontStyle.Bold );
		private Font m_standardFont = new Font( "Arial" , 8 , FontStyle.Regular);

		private IPropertyDisplayFormatter m_propertyDisplayFormatter = null;

		#endregion
		
		#region GUI Controls

		private System.Windows.Forms.ToolTip toolTip1;
		private System.ComponentModel.IContainer components;

		#endregion

		#region Events & Delegates

		/// <summary>
		/// Row changed event - raised when the current row is changed
		/// </summary>
		[Description("Raised when the user clicks on a different row")]
		public event SelectedRowChangedDelegate SelectedRowChanged;

		/// <summary>
		/// Cell Clicked event - raised when a cell in the Grid is clicked
		/// </summary>
		[Description("Raised when a cell in the Grid is clicked")]
		public event CellClickedDelegate CellClicked;

		/// <summary>
		/// Occurs when a cell is double clicked
		/// </summary>
		[Description("Raised when a cell in the grid is double clicked")]
		public event CellDoubleClickedDelegate CellDoubleClicked;

		/// <summary>
		/// Occurs when the data in a row is deleted.
		/// </summary>
		[Description("Occurs when the data in a row is deleted")]
		public event RowDeletedDelegate RowDeleted;

		/// <summary>
		/// Occurs when the data in a row is changed
		/// </summary>
		[Description("Occurs when the data in a row is changed")]
		public event RowChangedDelegate RowChanged;

		/// <summary>
		/// Row Changed Delegate
		/// </summary>
		public delegate void SelectedRowChangedDelegate(object sender, SelectedRowChangedEventArgs e);

		/// <summary>
		/// Cell Clicked Delegate
		/// </summary>
		public delegate void CellClickedDelegate(object sender, CellEventArgs e);

		/// <summary>
		/// Cell Double Clicked Delegate
		/// </summary>
		public delegate void CellDoubleClickedDelegate(object sender, CellEventArgs e);

		/// <summary>
		/// Row has changed
		/// </summary>
		public delegate void RowChangedDelegate(object sender, CellEventArgs e);

		/// <summary>
		/// Row has been deleted
		/// </summary>
		public delegate void RowDeletedDelegate(object sender, CellEventArgs e);

		#endregion

		#region Component Designer generated code
		
		
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// DataGrid
			// 
			this.AllowNavigation = false;
			this.AlternatingBackColor = System.Drawing.Color.LightGray;
			this.BackColor = System.Drawing.Color.Gainsboro;
			this.BackgroundColor = System.Drawing.Color.Silver;
			this.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.CaptionBackColor = System.Drawing.Color.LightSteelBlue;
			this.CaptionForeColor = System.Drawing.Color.MidnightBlue;
			this.CaptionVisible = false;
			this.FlatMode = true;
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ForeColor = System.Drawing.Color.Black;
			this.GridLineColor = System.Drawing.Color.Black;
			this.HeaderBackColor = System.Drawing.Color.MidnightBlue;
			this.HeaderFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.HeaderForeColor = System.Drawing.Color.White;
			this.LinkColor = System.Drawing.Color.MidnightBlue;
			this.ParentRowsBackColor = System.Drawing.Color.DarkGray;
			this.ParentRowsForeColor = System.Drawing.Color.Black;
			this.SelectionBackColor = System.Drawing.Color.CadetBlue;
			this.SelectionForeColor = System.Drawing.Color.White;
			this.DoubleClick += new System.EventHandler(this.DataGrid_DoubleClick);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DataGrid_MouseUp);
			this.CurrentCellChanged += new System.EventHandler(this.DataGrid_CurrentCellChanged);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DataGrid_MouseMove);
			this.Enter += new System.EventHandler(this.DataGrid_Enter);
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}
		
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}
		#endregion

		#region Public Methods

		/// <summary>
		/// Prints the contents of the Grid.
		/// </summary>
		/// <param name="dataViewToPrint">The contents of the dataview are the actual details printed.</param>
		/// <param name="pageHeader">Page Header to print on document. Can be suppressed using PrintPageHeader.</param>
		/// <remarks>
		/// <para>Some properties on the grid affect the contents of the printout. These properties include:</para>
		/// <list type="bullet">
		///		<item><see cref="PrintColumnHeadings"/></item>
		///		<item><see cref="PrintInPortraitLayout"/></item>
		///		<item><see cref="PrintPageHeader"/></item>
		///		<item><see cref="PrintPageNumbers"/></item>
		/// </list>
		/// </remarks>
		[Description("Print the contents of the Grid")]
		public void Print( DataView dataViewToPrint , string pageHeader )
		{
			this.m_dataViewToPrint = dataViewToPrint;
			DataTable myTableToPrint = this.createTableFromDataView();

			// Do the print
			DataGridPrinter myPrinter = new DataGridPrinter( m_printPageHeader , pageHeader , m_printColumnHeadings, m_printPageNumbers , m_printInPortraitLayout , m_printColumnSettings , m_pageHeadingFont , m_columnHeadingFont , m_standardFont );
			myPrinter.Print( myTableToPrint , m_gridPrintStyle );
		}

		/// <summary>
		/// Prints the contents of the Grid.
		/// </summary>
		/// <param name="pageHeader">Page Header to print on document. Can be suppressed using PrintPageHeader.</param>
		/// <remarks>
		/// <para>Assumes the datagrid has been bound by using <see cref="BindToCustomCollection"/></para>
		/// <para>Some properties on the grid affect the contents of the printout. These properties include:</para>
		/// <list type="bullet">
		///		<item><see cref="PrintColumnHeadings"/></item>
		///		<item><see cref="PrintInPortraitLayout"/></item>
		///		<item><see cref="PrintPageHeader"/></item>
		///		<item><see cref="PrintPageNumbers"/></item>
		/// </list>
		/// </remarks>
		/// <exception cref="ApplicationException">If the grid hasn't been bound to a DataView, or bound to via BindToCustomCollection, raise an error.</exception>
		[Description("Print the contents of the Grid")]
		public void Print( string pageHeader )
		{
			DataTable myTableToPrint = null;

			if ( m_dataViewToPrint != null )
			{
				myTableToPrint = this.createTableFromDataView();

				// Do the print
				DataGridPrinter myPrinter = new DataGridPrinter( m_printPageHeader , pageHeader , m_printColumnHeadings, m_printPageNumbers , m_printInPortraitLayout , m_printColumnSettings , m_pageHeadingFont , m_columnHeadingFont , m_standardFont );
				myPrinter.Print( myTableToPrint , m_gridPrintStyle );
			}
			else
			{
				if ( this.DataSource.GetType() == typeof( DataView ) )
				{
					m_dataViewToPrint = (DataView)this.DataSource;
					myTableToPrint = this.createTableFromDataView();
					
					// Do the print
					DataGridPrinter myPrinter = new DataGridPrinter( m_printPageHeader , pageHeader , m_printColumnHeadings, m_printPageNumbers , m_printInPortraitLayout , m_printColumnSettings , m_pageHeadingFont , m_columnHeadingFont , m_standardFont );
					myPrinter.Print( myTableToPrint , m_gridPrintStyle );
				}
				else
				{
					throw new ApplicationException( "No data view has been bound to the grid, so unable to print." );
				}
			}
		}

//		/// <summary>
//		/// Prints the contents of the Grid.
//		/// </summary>
//		/// <param name="dataViewToPrint">The contents of the dataview are the actual details printed.</param>
//		/// <param name="pageHeader">Page Header to print on document. Can be suppressed using PrintPageHeader.</param>
//		/// <param name="styleOfPrint">The style to use for the printout.</param>
//		/// <remarks>
//		/// Some properties on the grid affect the contents of the printout. These properties include:
//		/// <list type="bullet">
//		///		<item><see cref="PrintColumnHeadings"/></item>
//		///		<item><see cref="PrintInPortraitLayout"/></item>
//		///		<item><see cref="PrintPageHeader"/></item>
//		///		<item><see cref="PrintPageNumbers"/></item>
//		/// </list>
//		/// </remarks>
//		[Description("Print the contents of the Grid")]
//		public void Print( DataView dataViewToPrint , string pageHeader , GridPrintStyle styleOfPrint )
//		{
//			m_dataViewToPrint = dataViewToPrint;
//			m_gridPrintStyle = styleOfPrint;
//			
//			DataTable myTableToPrint = this.createTableFromDataView();
//
//			// Do the print
//			DataGridPrinter myPrinter = new DataGridPrinter( m_printPageHeader , pageHeader , m_printColumnHeadings, m_printPageNumbers , m_printInPortraitLayout , m_printColumnSettings , m_pageHeadingFont , m_columnHeadingFont , m_standardFont );
//			myPrinter.Print( myTableToPrint , m_gridPrintStyle );
//		}

		/// <summary>
		/// Perform a Print Preview for the contents of the Data Grid
		/// </summary>
		/// <param name="dataViewToPrint">The contents of the dataview are the actual details printed.</param>
		/// <param name="pageHeader">Page Header to print on document. Can be suppressed using PrintPageHeader.</param>
		/// <remarks>
		/// Some properties on the grid affect the contents of the printout. These properties include:
		/// <list type="bullet">
		///		<item><see cref="PrintColumnHeadings"/></item>
		///		<item><see cref="PrintInPortraitLayout"/></item>
		///		<item><see cref="PrintPageHeader"/></item>
		///		<item><see cref="PrintPageNumbers"/></item>
		/// </list>
		/// </remarks> 
		[Description("Perform a Print Preview of the contents of the Grid")]
		public void PrintPreview( DataView dataViewToPrint , string pageHeader )
		{
			m_dataViewToPrint = dataViewToPrint;
			DataTable myTableToPrint = this.createTableFromDataView();

			DataGridPrinter myPrinter = new DataGridPrinter( m_printPageHeader , pageHeader , m_printColumnHeadings, m_printPageNumbers , m_printInPortraitLayout , m_printColumnSettings , m_pageHeadingFont , m_columnHeadingFont , m_standardFont );
			myPrinter.PrintPreview( myTableToPrint , m_gridPrintStyle );
		}

		/// <summary>
		/// Perform a Print Preview for the contents of the Data Grid
		/// </summary>
		/// <param name="pageHeader">Page Header to print on document. Can be suppressed using PrintPageHeader.</param>
		/// <remarks>
		/// <para>Print using the dataview that was set up as part of <see cref="BindToCustomCollection"/> </para>
		/// Some properties on the grid affect the contents of the printout. These properties include:
		/// <list type="bullet">
		///		<item><see cref="PrintColumnHeadings"/></item>
		///		<item><see cref="PrintInPortraitLayout"/></item>
		///		<item><see cref="PrintPageHeader"/></item>
		///		<item><see cref="PrintPageNumbers"/></item>
		/// </list>
		/// </remarks> 
		/// <exception cref="ApplicationException">If the grid hasn't been bound to a DataView, or bound to via BindToCustomCollection, raise an error.</exception>
		[Description("Perform a Print Preview of the contents of the Grid")]
		public void PrintPreview( string pageHeader )
		{
			DataTable myTableToPrint = null;

			if ( m_dataViewToPrint != null )
			{
				myTableToPrint = this.createTableFromDataView();

				DataGridPrinter myPrinter = new DataGridPrinter( m_printPageHeader , pageHeader , m_printColumnHeadings, m_printPageNumbers , m_printInPortraitLayout , m_printColumnSettings , m_pageHeadingFont , m_columnHeadingFont , m_standardFont );
				myPrinter.PrintPreview( myTableToPrint , m_gridPrintStyle );
			}
			else
			{
				if ( this.DataSource.GetType() == typeof(DataView ) )
				{
					m_dataViewToPrint = (DataView)this.DataSource;
					myTableToPrint = this.createTableFromDataView();

					DataGridPrinter myPrinter = new DataGridPrinter( m_printPageHeader , pageHeader , m_printColumnHeadings, m_printPageNumbers , m_printInPortraitLayout , m_printColumnSettings , m_pageHeadingFont , m_columnHeadingFont , m_standardFont );
					myPrinter.PrintPreview( myTableToPrint , m_gridPrintStyle );
				}
				else
				{
					throw new ApplicationException( "No DataView has been bound to the grid, so unable to do a print preview." );
				}
			}
		}

//		/// <summary>
//		/// Perform a Print Preview for the contents of the Data Grid
//		/// </summary>
//		/// <param name="dataViewToPrint">The contents of the dataview are the actual details printed.</param>
//		/// <param name="pageHeader">Page Header to print on document. Can be suppressed using PrintPageHeader.</param>
//		/// <param name="styleOfPrint">The style to use for the Print Preview.</param>
//		/// <remarks>
//		/// Some properties on the grid affect the contents of the printout. These properties include:
//		/// <list type="bullet">
//		///		<item><see cref="PrintColumnHeadings"/></item>
//		///		<item><see cref="PrintInPortraitLayout"/></item>
//		///		<item><see cref="PrintPageHeader"/></item>
//		///		<item><see cref="PrintPageNumbers"/></item>
//		/// </list>
//		/// </remarks>
//		[Description("Perform a Print Preview of the contents of the Grid")]
//		public void PrintPreview( DataView dataViewToPrint , string pageHeader )
//		{
//			m_dataViewToPrint = dataViewToPrint;
//			m_gridPrintStyle = styleOfPrint;
//
//			DataTable myTableToPrint = this.createTableFromDataView();
//
//			// Do the print
//			DataGridPrinter myPrinter = new DataGridPrinter( m_printPageHeader , pageHeader , m_printColumnHeadings, m_printPageNumbers , m_printInPortraitLayout , m_printColumnSettings , m_pageHeadingFont , m_columnHeadingFont , m_standardFont );
//			myPrinter.PrintPreview( myTableToPrint , m_gridPrintStyle );
//		}

		/// <summary>
		/// Write/Extract the contents of the grid into a file using an embedded XSL.
		/// </summary>
		/// <param name="dataViewToPrint">The DataView to write to the file.</param>
		/// <param name="fileName">The filename to write the grid contents to.</param>
		/// <param name="xslResourceName">The embedded Xsl resource to use when writing the grid contents to file.</param>
		/// <remarks>
		///		<para>An XSLT is used to format the output of the DataView when writing to the file.</para>
		///		<note>The top level node will be called "DataGrid".</note>
		///		<note>Each row will be tagged with the original TableName from the DataView.</note>
		///		<note>The xslt must be an embedded resource.</note>
		///	</remarks>
		[Description("Write/Extract the contents of the Grid to a file. Requires an embedded xslt file.")]
		public void Write( DataView dataViewToPrint, string fileName , string xslResourceName )
		{
			m_dataViewToPrint = dataViewToPrint;

			DataTable myTableToWrite = this.createTableFromDataView();

			// Do the write
			DataGridWriter myWriter = new DataGridWriter( fileName , xslResourceName , myTableToWrite );
			myWriter.Write();
		}

		/// <summary>
		/// Write/Extract the contents of the grid into a file using an embedded XSL.
		/// </summary>
		/// <param name="fileName">The filename to write the grid contents to.</param>
		/// <param name="xslResourceName">The embedded Xsl resource to use when writing the grid contents to file.</param>
		/// <remarks>
		///		<para>An XSLT is used to format the output of the DataView when writing to the file.</para>
		///		<note>The top level node will be called "DataGrid".</note>
		///		<note>Each row will be tagged with the original TableName from the DataView.</note>
		///		<note>The xslt must be an embedded resource.</note>
		///	</remarks>
		[Description("Write/Extract the contents of the Grid to a file. Requires an embedded xslt file.")]
		public void Write( string fileName , string xslResourceName )
		{
			DataTable myTableToWrite = null;

			if ( m_dataViewToPrint != null )
			{
				myTableToWrite = this.createTableFromDataView();
			}
			else
			{
				if ( this.DataSource.GetType() == typeof( DataView ) )
				{
					m_dataViewToPrint = (DataView)this.DataSource;
					myTableToWrite = this.createTableFromDataView();
				}
				else
				{
					throw new ApplicationException( "No Dataview has been bound to the grid, so unable to do the Write/Extract." );
				}
			}

			// Do the write
			DataGridWriter myWriter = new DataGridWriter( fileName , xslResourceName , myTableToWrite );
			myWriter.Write();
		}

		/// <summary>
		/// Copy the display-related properties of the given DataGrid to the given DataGridTableStyle
		/// </summary>
		/// <param name="datagrid">The DataGrid to copy the style information from.</param>
		/// <param name="ts">The tableStyle to copy the style information to.</param>
		[Description("Copy DataGrid style information to the DataTable")]
		public void CopyDefaultTableStyle(DataGrid datagrid, DataGridTableStyle ts)
		{
			ts.AllowSorting = datagrid.AllowSorting;
			ts.AlternatingBackColor = datagrid.AlternatingBackColor;
			ts.BackColor = datagrid.BackColor;
			ts.ColumnHeadersVisible = datagrid.ColumnHeadersVisible;
			ts.ForeColor = datagrid.ForeColor;
			ts.GridLineColor = datagrid.GridLineColor;
			ts.GridLineStyle = datagrid.GridLineStyle;
			ts.HeaderBackColor = datagrid.HeaderBackColor;
			ts.HeaderFont = datagrid.HeaderFont;
			ts.HeaderForeColor = datagrid.HeaderForeColor;
			ts.LinkColor = datagrid.LinkColor;
			ts.PreferredColumnWidth = datagrid.PreferredColumnWidth;
			ts.PreferredRowHeight = datagrid.PreferredRowHeight;
			ts.ReadOnly = datagrid.ReadOnly;
			ts.RowHeadersVisible = datagrid.RowHeadersVisible;
			ts.RowHeaderWidth = datagrid.RowHeaderWidth;
			ts.SelectionBackColor = datagrid.SelectionBackColor;
			ts.SelectionForeColor = datagrid.SelectionForeColor;
		}

		/// <summary>
		/// Copy the display-related properties of the DataGrid to the given DataGridTableStyle
		/// </summary>
		/// <param name="ts">The TableStyle to copy the style information to.</param>
		[Description("Copy DataGrid style information to the DataTable")]
		public void CopyDefaultTableStyle(DataGridTableStyle ts)
		{
			ts.AllowSorting = this.AllowSorting;
			ts.AlternatingBackColor = this.AlternatingBackColor;
			ts.BackColor = this.BackColor;
			ts.ColumnHeadersVisible = this.ColumnHeadersVisible;
			ts.ForeColor = this.ForeColor;
			ts.GridLineColor = this.GridLineColor;
			ts.GridLineStyle = this.GridLineStyle;
			ts.HeaderBackColor = this.HeaderBackColor;
			ts.HeaderFont = this.HeaderFont;
			ts.HeaderForeColor = this.HeaderForeColor;
			ts.LinkColor = this.LinkColor;
			ts.PreferredColumnWidth = this.PreferredColumnWidth;
			ts.PreferredRowHeight = this.PreferredRowHeight;
			ts.ReadOnly = this.ReadOnly;
			ts.RowHeadersVisible = this.RowHeadersVisible;
			ts.RowHeaderWidth = this.RowHeaderWidth;
			ts.SelectionBackColor = this.SelectionBackColor;
			ts.SelectionForeColor = this.SelectionForeColor;
		}

		/// <summary>
		/// Copy the display-related properties of the DataGrid to all the DataGridTableStyle
		/// </summary>
		[Description("Copy DataGrid style information to all the Table Styles")]
		public void CopyDefaultTableStyle()
		{
			for( int tableStyleIndex = 0; tableStyleIndex < this.TableStyles.Count ; tableStyleIndex ++ )
			{
				this.TableStyles[ tableStyleIndex ].AllowSorting = this.AllowSorting;
				this.TableStyles[ tableStyleIndex ].AlternatingBackColor = this.AlternatingBackColor;
				this.TableStyles[ tableStyleIndex ].BackColor = this.BackColor;
				this.TableStyles[ tableStyleIndex ].ColumnHeadersVisible = this.ColumnHeadersVisible;
				this.TableStyles[ tableStyleIndex ].ForeColor = this.ForeColor;
				this.TableStyles[ tableStyleIndex ].GridLineColor = this.GridLineColor;
				this.TableStyles[ tableStyleIndex ].GridLineStyle = this.GridLineStyle;
				this.TableStyles[ tableStyleIndex ].HeaderBackColor = this.HeaderBackColor;
				this.TableStyles[ tableStyleIndex ].HeaderFont = this.HeaderFont;
				this.TableStyles[ tableStyleIndex ].HeaderForeColor = this.HeaderForeColor;
				this.TableStyles[ tableStyleIndex ].LinkColor = this.LinkColor;
				this.TableStyles[ tableStyleIndex ].PreferredColumnWidth = this.PreferredColumnWidth;
				this.TableStyles[ tableStyleIndex ].PreferredRowHeight = this.PreferredRowHeight;
				this.TableStyles[ tableStyleIndex ].ReadOnly = this.ReadOnly;
				this.TableStyles[ tableStyleIndex ].RowHeadersVisible = this.RowHeadersVisible;
				this.TableStyles[ tableStyleIndex ].RowHeaderWidth = this.RowHeaderWidth;
				this.TableStyles[ tableStyleIndex ].SelectionBackColor = this.SelectionBackColor;
				this.TableStyles[ tableStyleIndex ].SelectionForeColor = this.SelectionForeColor;
			}
		}

		/*****  Method Deprecated due to issues with the way the grid sets back property data when recreating objects.
		 *		To be resolved by allowing 2 way databind to the custom collection itself rather than creating a table.
		 *		This is a big development effort, until this is completed avoid using custom property formatters and stick
		 *		to the tried & tested use of [propertyname]Display properties in they entity (type).
		 *		This is inelegant as it mixes UI logic with business logic, however it is a workaround until the grid 
		 *		is fixed (if it gets fixed)
		 *
		/// <summary>
		/// Allows user to pass in an object that specifies formatting for the various properties to be displayed
		/// </summary>
		/// <param name="customCollection"></param>
		/// <param name="propertyDisplayFormatter"></param>
		public void BindToCustomCollection( IList customCollection, IPropertyDisplayFormatter propertyDisplayFormatter)
		{			
			m_propertyDisplayFormatter = propertyDisplayFormatter;
			BindToCustomCollection ( customCollection);
		}
		
		**********************************/

		/// <summary>
		/// Creates a DataTable from the custom collection and binds the table's default view to the grid
		/// </summary>
		/// <param name="customCollection"></param>
		public void BindToCustomCollection( IList customCollection )
		{
			DataTable dt = new DataTable( );
			ArrayList allTypes = new ArrayList();
			System.Type previousType;

			PropertyInfo[] properties;

			System.Type collectionType = customCollection.GetType();
			System.Type collectionContainsType = collectionType.GetProperty( "Item" ).PropertyType;
			allTypes.Add( collectionContainsType );
			previousType = collectionContainsType;
			m_collectionIndexerType = collectionContainsType;

			try
			{
				properties = collectionContainsType.GetProperties();
			}
			catch
			{
				throw new InvalidOperationException("Could not retrieve the properties for " + collectionContainsType.ToString());
			}

			// Add the properties for row 1
			this.addTypeProperty( dt );
			this.addColumnsForProperties( properties , dt );

			// Create data rows for each T in our custom collection
			for (int i=0; i<customCollection.Count; i++)
			{
				try
				{
					// If this is a different type to the previous one, remember its properties.
					if ( customCollection[ i ].GetType() != previousType )
					{
						properties = customCollection[ i ].GetType().GetProperties();
						previousType = customCollection[ i ].GetType();
					}
				}
				catch
				{
					throw new InvalidOperationException("Could not retrieve the properties for " + customCollection[ i ].GetType().ToString());
				}

				// If this is a new type, add the relevant properties
				if ( allTypes.Contains( customCollection[ i ].GetType() ) == false )
				{
					// Add the properties for the type
					this.addColumnsForProperties( properties , dt );
					allTypes.Add( customCollection[ i ].GetType() );
				}

				// Create a row and set its type.
				DataRow newRow = dt.NewRow();
				newRow[ "_Type" ] = customCollection[ i ].GetType();

				for (int j=0; j<properties.Length; j++)
				{
					PropertyInfo currentProperty = (PropertyInfo)properties[j];
					try
					{
						object propertyValue = currentProperty.GetValue(customCollection[i], BindingFlags.GetProperty, null, null, null );
						if ( propertyValue == null )
						{
							newRow[ currentProperty.Name ] = System.DBNull.Value;
						}
						else
						{
							if (m_propertyDisplayFormatter != null && 
								this.TableStyles.Count == 1 &&
								this.TableStyles[0].GridColumnStyles.Contains (currentProperty.Name) &&
								m_propertyDisplayFormatter.IsCustomFormatted (currentProperty.Name))
							{
								//only bother to pass property back to caller
								//if its one thats presented in the grid (optimisation)
								newRow[ currentProperty.Name ] = m_propertyDisplayFormatter.FormatProperty(currentProperty, propertyValue, customCollection[i]);
							}
							else
							{
								newRow[ currentProperty.Name ] = propertyValue;
							}
						}
					}
					catch( Exception Ex )
					{
						string diagnostic = "Could not retrieve a value to set in column number " + j + Environment.NewLine + "The retrieved name is " + currentProperty.Name +
							", the type is " + currentProperty.PropertyType + Environment.NewLine;
    
						throw new InvalidOperationException(diagnostic , Ex );
					}
				}

				// Add the new row to the data table
				dt.Rows.Add(newRow);
			}

			this.setRowState( dt );


			// Save the unmodified collection
			m_OriginalCollection = customCollection;
			m_ConvertedDataTable = dt;

			// Finally, bind this table to the grid
			m_dataViewToPrint = dt.DefaultView;
			m_dataViewToPrint.AllowDelete = m_allowDelete;
			m_dataViewToPrint.AllowNew = m_allowNew;
			this.DataSource = m_dataViewToPrint;

			m_ConvertedDataTable.RowChanged +=new DataRowChangeEventHandler(BoundData_RowChanged);
			m_ConvertedDataTable.RowDeleted +=new DataRowChangeEventHandler(BoundData_RowDeleted);

		}

		/// <summary>
		/// Retrieves the un-modified original custom collection from the grid
		/// </summary>
		/// <returns>The un-modified custom collection</returns>
		[Description("Retrieves the un-modified custom collection from the grid")]
		public IList RetrieveOriginalCustomCollection()
		{
			return m_OriginalCollection;
		}


		/// <summary>
		/// Retrieve the updated custom collection
		/// </summary>
		/// <returns></returns>
		public IList RetrieveUpdatedCustomCollection()
		{
			if ( m_OriginalCollection != null )
			{
				// Create a new list
				IList newList = (IList)Activator.CreateInstance(m_OriginalCollection.GetType(), false);
            
				// Loop through each row of the table
				for( int i = 0 ; i < m_ConvertedDataTable.Rows.Count ; i ++ )
				{
					// Check to see if the current row is modified 
					DataRow currentRow = m_ConvertedDataTable.Rows[i];
				
					// Only worry about changed rows
					if ( currentRow.RowState != DataRowState.Unchanged )
					{
						object newObject = createObjectFromDataRow(currentRow);
						newList.Add( newObject );
					}
				}
				return newList;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Retrieve entire collection
		/// </summary>
		/// <returns></returns>
		public IList RetrieveEntireCustomCollection()
		{
			if ( m_OriginalCollection != null )
			{
				// Create a new list
				IList newList = (IList)Activator.CreateInstance(m_OriginalCollection.GetType(), false);
            
				// Loop through each row of the table
				for( int i = 0 ; i < m_dataViewToPrint.Count ; i ++ )
				{
					// Check to see if the current row is modified 
					DataRow currentRow = m_dataViewToPrint[ i ].Row;
					object newObject = createObjectFromDataRow(currentRow);
					newList.Add( newObject );
				}
				return newList;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Retrieve a single row from the grid as the appropriate type.
		/// </summary>
		/// <param name="rowNumber">Row number to retrieve from Grid</param>
		/// <returns></returns>
		public object RetrieveObject( int rowNumber )
		{
			if ( m_OriginalCollection != null )
			{
				if ( rowNumber < m_dataViewToPrint.Count )
				{
					DataRow currentRow = m_dataViewToPrint[ rowNumber ].Row;
					return this.createObjectFromDataRow( currentRow  );
				}
				else
				{
					throw new ArgumentException( String.Format("Unable to retrieve row {0} as the grid only contains {1} rows." , rowNumber , m_dataViewToPrint.Count ) );
				}
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Set the default value for a property.
		/// </summary>
		/// <param name="propertyName"></param>
		/// <param name="propertyDefaultValue"></param>
		/// <returns></returns>
		public bool SetDefaultValue( string propertyName , object propertyDefaultValue )
		{
			bool returnValue = false;

			if ( m_ConvertedDataTable != null )
			{
				// Did we get a valid property name
				if ( m_ConvertedDataTable.Columns.Contains( propertyName ) )
				{
					m_ConvertedDataTable.Columns[ propertyName ].DefaultValue = propertyDefaultValue;
					returnValue = true;
				}
			}

			return returnValue;
		}

		/// <summary>
		/// Reject the changes for a row in the Grid.
		/// </summary>
		/// <param name="rowNumber"></param>
		public void RejectChanges( int rowNumber )
		{
			if ( m_dataViewToPrint != null )
			{
				m_dataViewToPrint[ rowNumber ].Row.RejectChanges();
			}
		}

		/// <summary>
		/// Commit the changes to the Data Grid
		/// </summary>
		/// <remarks>Particularly useful if you perform batch updates against the data in the Grid.</remarks>
		public void CommitChanges()
		{
			if ( m_dataViewToPrint != null )
			{
				CurrencyManager myCurrencyManager = (CurrencyManager)this.BindingContext[ m_dataViewToPrint ];
				myCurrencyManager.Refresh();
				myCurrencyManager.EndCurrentEdit();
			}
		}

		/// <summary>
		/// Set the value for a particular property.
		/// </summary>
		/// <param name="rowNumber">Row number to set value for</param>
		/// <param name="propertyName">Property to set value for</param>
		/// <param name="propertyValue">Value to set the property to.</param>
		public void SetValue( int rowNumber , string propertyName , object propertyValue )
		{
			if ( m_dataViewToPrint != null )
			{
				if ( m_ConvertedDataTable.Columns.IndexOf( propertyName ) > 0 )
				{
					m_dataViewToPrint[ rowNumber ].Row[ propertyName ] = propertyValue;
				}
				else
				{
					throw new ArgumentException( String.Format( "{0} is not a valid property to set." , propertyName ) , propertyName  );
				}
			}
			else
			{
				DataView boundDataView = (DataView)this.DataSource;
				if ( boundDataView.Table.Columns.IndexOf( propertyName ) > 0 )
				{
					boundDataView[ rowNumber ].Row[ propertyName ] = propertyValue;
				}
				else
				{
					throw new ArgumentException( String.Format( "{0} is not a valid property to set." , propertyName ) , propertyName );
				}
			}
		}

		/// <summary>
		/// Get the value for a particular property.
		/// </summary>
		/// <param name="rowNumber">Row number to set value for</param>
		/// <param name="propertyName">Property to set value for</param>
		public object GetValue( int rowNumber , string propertyName )
		{
			if ( m_dataViewToPrint != null )
			{
				if ( m_ConvertedDataTable.Columns.IndexOf( propertyName ) >= 0 )
				{
					return m_ConvertedDataTable.Rows[ rowNumber ][ propertyName ];
				}
				else
				{
					throw new ArgumentException( String.Format( "{0} is not a valid property to retrieve." , propertyName ) , propertyName);
				}
			}
			else
			{
				DataView boundDataView = (DataView)this.DataSource;
				if ( boundDataView.Table.Columns.IndexOf( propertyName ) >= 0 )
				{
					return boundDataView[ rowNumber ][ propertyName ];
				}
				else
				{
					throw new ArgumentException( String.Format( "{0} is not a valid property to retrieve." , propertyName ) , propertyName );
				}
			}
		}

		/// <summary>
		/// Does the Grid have a particular property
		/// </summary>
		/// <param name="propertyName"></param>
		/// <returns></returns>
		public bool HasProperty( string propertyName )
		{
			bool returnValue = false;

			if ( m_dataViewToPrint != null )
			{
				if ( m_ConvertedDataTable.Columns.IndexOf( propertyName ) >= 0 )
				{
					returnValue = true;					
				}
			}
			else
			{
				DataView boundDataView = (DataView)this.DataSource;
				if ( boundDataView.Table.Columns.IndexOf( propertyName ) >= 0 )
				{
					returnValue = true;
				}
			}

			return returnValue;
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Create object from data row
		/// </summary>
		/// <param name="currentRow"></param>
		/// <returns></returns>
		private object createObjectFromDataRow(DataRow currentRow)
		{
			// Create a new object of the correct type and get the original
			System.Type rowType;

			if ( currentRow[ "_Type" ].ToString() == "" )
			{
				// If we don't know the tpye, default to the collections indexer type
				rowType = m_collectionIndexerType;
			}
			else
			{
				rowType = (System.Type)currentRow[ "_Type" ];
			}
			

			// Create a new object of the correct type and get the original
			object newObject = Activator.CreateInstance( rowType , false );

			// Get the properties of the collected type, which will be the same for the comparable object
			PropertyInfo[] properties = rowType.GetProperties();

			// Loop through each column of the table
			for( int j = 0 ; j < properties.Length ; j++ )
			{
				// Get each property for the table and for the equivalent original object
				PropertyInfo currentProperty = (PropertyInfo)properties[ j ];

				object tablePropertyValue = currentRow[ currentProperty.Name ];
				object currentPropertyValue = currentProperty.GetValue(newObject, BindingFlags.GetProperty, null, null, null );

				// We have a big problem with structs. Enums are structs. So this method won't work.
				// This is a bit unlucky, as we're going to encounter instances where we need this.
				// According to various usergroups, using Enum.ToObject should work. 
				// Certainly the below check does not.

				// if (tablePropertyValue is System.Enum)
				// {
				//     Console.WriteLine("ENUM");
				// }

				// This looks odd, but I've found it much more robust than a null check...
				// This doesn't work. Bizarrely 
				//  if (tablePropertyValue.ToString() != String.Empty)
				// This doesn't seem to work at all
				//  if (tablePropertyValue == originalPropertyValue)
				if (tablePropertyValue.ToString().Length != 0)
				{
					try
					{
						// Is the property Read / Write
						if ( currentProperty.CanWrite )
						{
							if (currentPropertyValue is System.Enum)
							{
								currentProperty.SetValue( newObject , Enum.ToObject(currentPropertyValue.GetType(), (int)tablePropertyValue) , BindingFlags.SetProperty , null , null , null ); 
							}
							else
							{
								currentProperty.SetValue( newObject , tablePropertyValue , BindingFlags.SetProperty , null , null , null ); 
							}
						}
					}
					catch
					{
						// Swallow the exception
					}
				}
			}

			if (newObject is IEntityBase)
			{
				// Sort out the dirty flag
				switch( currentRow.RowState )
				{
					case DataRowState.Modified:
						((IEntityBase)newObject).IsDirty = true;
						((IEntityBase)newObject).IsNew = false;
						break;
					case DataRowState.Added:
						((IEntityBase)newObject).IsNew = true;
						((IEntityBase)newObject).IsDirty = false;
						break;
					case DataRowState.Deleted:
						((IEntityBase)newObject).IsDeleted = true;
						break;
						// IB: We can't assume unchanged rows are not dirty and not new.
						// To add new entities, some people do a retrieve, add to the collection and do a rebind.
						// If they do this twice, it resets the Dirty and New flag on the first record.
						//					case DataRowState.Unchanged:
						//						((IEntityBase)newObject).IsDirty = false;
						//						((IEntityBase)newObject).IsNew = false;
						//						break;

				}
			}

			return newObject;

		}

		/// <summary>
		/// Add a type column to the Data Table
		/// </summary>
		/// <param name="dt"></param>
		private void addTypeProperty( DataTable dt )
		{
			DataColumn typeColumn = new DataColumn( "_Type" , typeof(System.Type) );
			typeColumn.AllowDBNull = true;
			dt.Columns.Add( typeColumn );
		}

		/// <summary>
		/// Make sure the table contains all the properties
		/// </summary>
		/// <param name="properties"></param>
		/// <param name="dt"></param>
		private void addColumnsForProperties( PropertyInfo[] properties , DataTable dt )
		{
			int numProperties = properties.Length;
            
			// Create a data column for each public property
			for (int k=0; k<numProperties; k++)
			{
				PropertyInfo currentProperty = (PropertyInfo)properties[k];
				try
				{
					// If the Data Table doesn't contain the property, add it
					if ( dt.Columns.Contains( currentProperty.Name ) == false )
					{
						DataColumn newColumn = null;
						if (m_propertyDisplayFormatter != null && m_propertyDisplayFormatter.IsCustomFormatted (currentProperty.Name))
						{
							newColumn = new DataColumn(currentProperty.Name, typeof(string) );
						}
						else //we are using custom formatting so check to see if this 
						{
							newColumn = new DataColumn(currentProperty.Name, currentProperty.PropertyType );
						}

						newColumn.AllowDBNull = true;
						dt.Columns.Add( newColumn ); 
					}
				}
				catch
				{
					string diagnostic = "Could not add a datatable row for property number " + k + Environment.NewLine + "The retrieved name is " + currentProperty.Name +
						", the type is " + currentProperty.PropertyType + Environment.NewLine;

					throw new InvalidOperationException(diagnostic);
				}
			}

		}
		        
        /// <summary>
		/// Figure out the datatable from looking at the view
		/// </summary>
		/// <returns></returns>
		private DataTable createTableFromDataView()
		{
			DataView originalDataView = m_dataViewToPrint;
			DataTable myTableToPrint = new DataTable( "DataTable" );

			if ( this.TableStyles.Count > 0 )
			{
				// Build the table
				foreach( DataGridColumnStyle indexStyle in this.TableStyles[0].GridColumnStyles )
				{
					myTableToPrint.Columns.Add( new DataColumn( indexStyle.MappingName ) );
				}

				// Copy the rows
				foreach( DataRowView indexRowView in originalDataView )
				{
					DataRow mySourceRow = indexRowView.Row;
							
					DataRow newRow = myTableToPrint.NewRow();
					// Copy the columns
					foreach( DataGridColumnStyle indexStyle in this.TableStyles[0].GridColumnStyles )
					{
						newRow[ indexStyle.MappingName ] = mySourceRow[ indexStyle.MappingName ].ToString();
								
					}
					myTableToPrint.Rows.Add( newRow );
				}
			}
			else
			{
				// Build the table
				foreach( DataColumn indexColumn in originalDataView.Table.Columns )
				{
					myTableToPrint.Columns.Add( new DataColumn( indexColumn.ColumnName ) );
				}

				// Copy the rows
				foreach( DataRowView indexRowView in originalDataView )
				{
					DataRow mySourceRow = indexRowView.Row;
							
					DataRow newRow = myTableToPrint.NewRow();
					// Copy the columns
					foreach( DataColumn indexColumn in originalDataView.Table.Columns )
					{
						newRow[ indexColumn.ColumnName ] = mySourceRow[ indexColumn.ColumnName ];
					}
					myTableToPrint.Rows.Add( newRow );
				}
			}

			return myTableToPrint;
		}

		/// <summary>
		/// Set the Data Row State for the row.
		/// </summary>
		/// <param name="newTable"></param>
		private void setRowState( DataTable newTable )
		{
			for ( int rowNumber = 0 ; rowNumber < newTable.Rows.Count ; rowNumber ++ )
			{
				DataRow indexRow = newTable.Rows[ rowNumber ];

				// Make a better job at setting the Row State
				if ( ((bool)indexRow[ "IsNew" ]) == true )
				{
					// No Operation as the row state will be Added by Default
				}
				else if ( ((bool)indexRow[ "IsDirty" ]) == true )
				{
					// You can't directly set the RowState to Modified, so bodge it - change it twice.
					indexRow.AcceptChanges(); // Will make it unchanged.
					indexRow[ "IsDirty" ] = !((bool)indexRow[ "IsDirty" ]);
					indexRow[ "IsDirty" ] = !((bool)indexRow[ "IsDirty" ]);
				}
				else
				{
					// Make it unchanged
					indexRow.AcceptChanges();
				}
			}
		}

		#endregion

		#region EventHandlers

		/// <summary>
		/// Handle a double click on the grid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <remarks>Raises a CellDoubleClicked event if we have a subscriber.</remarks>
		private void DataGrid_DoubleClick(object sender, System.EventArgs e)
		{
			// Do we need to raise a Cell Double Clicked event
			if ( CellDoubleClicked != null )
			{
				CellEventArgs cellClickedArgs = new CellEventArgs( m_rowNumber , m_columnNumber , m_hitTestType );
				CellDoubleClicked( this , cellClickedArgs );
			}
		}

		/// <summary>
		/// Mouse up event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DataGrid_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			HitTestInfo myHitTest = this.HitTest(e.X,e.Y);

			m_columnNumber = myHitTest.Column;
			m_rowNumber = myHitTest.Row;
			m_hitTestType = myHitTest.Type;

			// Only process Radio buttons if we have hit a cell
			if ( m_hitTestType == HitTestType.Cell)
			{
				if ( this.TableStyles.Count > 0 )
				{
					// If its a 1 - click bool, toogle the value
					if ( this.TableStyles[0].GridColumnStyles[ m_columnNumber ] is DataGridBool1ClickColumn )
					{
						bool myValue = (bool)this[ m_rowNumber , m_columnNumber];

						this[ m_rowNumber , m_columnNumber] = !myValue;
					}
					else
					{
						// No table style so limited in what we can do...
					}
				}
				else
				{
					// Do we need to raise a HitTest event
					if ( CellClicked != null )
					{
						CellEventArgs cellClickedArgs = new CellEventArgs( m_rowNumber , m_columnNumber , m_hitTestType );
						CellClicked( this , cellClickedArgs );
					}
				}
			}		
		}

		/// <summary>
		/// Candle a mouse down to prevent a sort on a boolean column
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseDown(MouseEventArgs e) 
 		{ 
 			Point pt = new Point(e.X, e.Y); 
 
			DataGrid.HitTestInfo hti = this.HitTest(pt); 
			int columnNumber = hti.Column;
 
			if(hti.Type == HitTestType.ColumnHeader && this.TableStyles[0].GridColumnStyles[ columnNumber ] is DataGridBool1ClickColumn ) 
			{ 
				MessageBox.Show( "Can't currently sort a boolean column" , "Sort Error" , MessageBoxButtons.OK , MessageBoxIcon.Information );
			} 
			else
			{
				base.OnMouseDown(e); 
			}
 		}  


		/// <summary>
		/// When the grid becomes the active control, make sure it has the focus
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <remarks>This is a really nasty hack. It looks like when the DataGrid becomes the active control, focus is set to
		/// the first column in the grid. If this is a ComboBox column, it causes a runtime exception.
		/// By setting the focus to the grid, the problem seems to go away.</remarks>
		private void DataGrid_Enter(object sender, System.EventArgs e)
		{
			this.Focus();
		}
	
		/// <summary>
		/// The underlying data has changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BoundData_RowChanged(object sender, DataRowChangeEventArgs e)
		{			
			if ( this.RowChanged != null )
			{
				HitTestInfo myHitTest = this.HitTest( m_columnNumber , m_rowNumber );

				CellEventArgs myEventArgs = new CellEventArgs( m_rowNumber , m_columnNumber , myHitTest.Type );
				this.RowChanged( sender , myEventArgs );
			}
		}

		/// <summary>
		/// The underlying data has been deleted
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BoundData_RowDeleted(object sender, DataRowChangeEventArgs e)
		{
			if ( this.RowDeleted != null )
			{
				HitTestInfo myHitTest = this.HitTest( m_columnNumber , m_rowNumber );

				CellEventArgs myEventArgs = new CellEventArgs( m_rowNumber , m_columnNumber , myHitTest.Type );

				this.RowDeleted( sender , myEventArgs );
			}
		}

		/// <summary>
		/// The cell has changed, so remember the row, column number
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DataGrid_CurrentCellChanged(object sender, System.EventArgs e)
		{
			m_rowNumber = this.CurrentCell.RowNumber;
			m_columnNumber = this.CurrentCell.ColumnNumber;

			// Have we changed rows
			if ( m_oldRowNumber != m_rowNumber )
			{
				// Is someone listening for an event
				if ( this.SelectedRowChanged != null )
				{
					SelectedRowChangedEventArgs rowChangedArgs = new SelectedRowChangedEventArgs( m_oldRowNumber , m_rowNumber );
					SelectedRowChanged( this , rowChangedArgs );
				}

				m_oldRowNumber = m_rowNumber;
			}
		}
	
		/// <summary>
		/// Use the mouse move to do tooltips
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DataGrid_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			// Don't bother if we haven't got table styles.
			if ( this.TableStyles.Count > 0 )
			{
				System.Windows.Forms.DataGrid.HitTestInfo myInfo = this.HitTest( e.X , e.Y );

				if ( myInfo.Row < this.Count 
					&& myInfo.Type == System.Windows.Forms.DataGrid.HitTestType.Cell 
					&& myInfo.Row != m_currentToolTipRowNumber )
				{
					m_currentToolTipRowNumber = myInfo.Row;
					this.toolTip1.Active = false;

					string toolTipProperty = "";
					string toolTipText = "";
					bool columnHasTooltip = false;

					// Does the column have a tool tip
					if ( this.TableStyles[ 0 ].GridColumnStyles[ myInfo.Column ] is ICustomDataGridColumn )
					{
						columnHasTooltip = true;
						toolTipProperty = ((ICustomDataGridColumn)this.TableStyles[ 0 ].GridColumnStyles[ myInfo.Column ]).ToolTipProperty;
					}

					// Find the tooltip
					if ( columnHasTooltip && toolTipProperty != "" )
					{
						if ( this.HasProperty( toolTipProperty ) )
						{
							object toolTip = this.GetValue( m_currentToolTipRowNumber , toolTipProperty );
							if ( toolTip != DBNull.Value )
							{
								toolTipText = (string)toolTip;
							}
							else
							{
								toolTipText = "Tooltip has a null value";
							}
						}
						else
						{
							toolTipText = String.Format( "DataGrid doesn't have property '{0}'" , toolTipProperty );
						}

						this.toolTip1.SetToolTip( this ,  toolTipText );
						this.toolTip1.Active = true; 

					}
				}
			}
		}		
		
		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets a value that indicates whether the grid's caption is visible.
		/// </summary>
		[
		Browsable(true),
		CategoryAttribute("Custom"),
		DefaultValue(false)
		]
		public new bool CaptionVisible
		{
			get
			{
				return base.CaptionVisible;
			}
			set
			{
				base.CaptionVisible = value;
			}
		}

		/// <summary>
		/// Are we allowing deletes
		/// </summary>
		[
		Browsable(true),
		CategoryAttribute("Custom"),
		Description("Allow the user to delete a row from the grid."),
		DefaultValue(false)
		]
		public bool AllowDelete
		{
			get
			{
				return m_allowDelete;
			}
			set
			{
				m_allowDelete = value;
				if ( this.DataSource != null )
				{
					if ( this.DataSource is DataView )
					{
						((DataView)this.DataSource).AllowDelete = value;
					}
				}
			}
		}

		/// <summary>
		/// Are we allowing new records
		/// </summary>
		[
		Browsable(true),
		CategoryAttribute("Custom"),
		Description("Allow the user to add a new row."),
		DefaultValue(false)
		]
		public bool AllowNew
		{
			get
			{
				return m_allowNew;
			}
			set
			{
				m_allowNew = value;
				if ( this.DataSource != null )
				{
					if ( this.DataSource is DataView )
					{
						((DataView)this.DataSource).AllowNew = value;
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the grid displays in Flat Mode.
		/// </summary>
		[
		Browsable(true),
		CategoryAttribute("Custom"),
		DefaultValue(true)
		]
		public new bool FlatMode
		{
			get
			{
				return base.FlatMode;
			}
			set
			{
				base.FlatMode = value;
			}
		}

		/// <summary>
		/// Add Column Headings to Print outs.
		/// </summary>
		[
		Browsable(true),
		CategoryAttribute("Custom"),
		Description("Add Column Headings to Print outs"),
		DefaultValue(true)
		]
		public bool PrintColumnHeadings
		{
			get
			{
				return m_printColumnHeadings;
			}
			set
			{
				m_printColumnHeadings = value;
			}
		}

		/// <summary>
		/// Add Page Numbers to the print out
		/// </summary>
		[
		Browsable(true),
		CategoryAttribute("Custom"),
		Description("Add page numbers to the print out"),
		DefaultValue(true)
		]
		public bool PrintPageNumbers
		{
			get
			{
				return m_printPageNumbers;
			}
			set
			{
				m_printPageNumbers = value;
			}
		}

		/// <summary>
		/// Print in Portrait mode
		/// </summary>
		[
		Browsable(true),
		CategoryAttribute("Custom"),
		Description("Print in Portrait Mode Layout"),
		DefaultValue(false)
		]
		public bool PrintInPortraitLayout
		{
			get
			{
				return m_printInPortraitLayout;
			}
			set
			{
				m_printInPortraitLayout = value;
			}
		}	
	
		/// <summary>
		/// Add a Page Header to the print out
		/// </summary>
		[
		Browsable(true),
		CategoryAttribute("Custom"),
		Description("Print a Page Header"),
		DefaultValue(true)
		]
		public bool PrintPageHeader
		{
			get
			{
				return m_printPageHeader;
			}
			set
			{
				m_printPageHeader = value;
			}
		}

		/// <summary>
		/// Gets or sets the colour of the non-row area of the grid.
		/// </summary>
		[
		DefaultValue( typeof(Color), "Silver" )
		]
		public new Color BackgroundColor
		{
			get
			{
				return base.BackgroundColor;
			}
			set
			{
				base.BackgroundColor = value;
			}
		}

		/// <summary>
		/// Gets or sets the background colour of even numbered rows of the grid.
		/// </summary>
		[
		DefaultValue( typeof(Color), "Gainsboro" )
		]
		public new Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}

		/// <summary>
		/// Gets or sets the background colour of odd numbered rows of the grid.
		/// </summary>
		[
		DefaultValue( typeof(Color), "LightGray" )
		]
		public new Color AlternatingBackColor
		{
			get
			{
				return base.AlternatingBackColor;
			}
			set
			{
				base.AlternatingBackColor = value;
			}
		}

		/// <summary>
		/// Gets or sets the grid's border style.
		/// </summary>
		[
		DefaultValue(typeof(System.Windows.Forms.BorderStyle), "None")
		]
		public new BorderStyle BorderStyle
		{
			get
			{
				return base.BorderStyle;
			}
			set
			{
				base.BorderStyle = value;
			}
		}

		/// <summary>
		/// Gets or sets the background colour of the caption area.
		/// </summary>
		[
		DefaultValue( typeof(Color), "LightSteelBlue" )
		]
		public new Color CaptionBackColor
		{
			get
			{
				return base.CaptionBackColor;
			}
			set
			{
				base.CaptionBackColor = value;
			}
		}

		/// <summary>
		/// Gets or sets the foreground colour of the caption area.
		/// </summary>
		[
		DefaultValue( typeof(Color), "MidnightBlue" )
		]
		public new Color CaptionForeColor
		{
			get
			{
				return base.CaptionForeColor;
			}
			set
			{
				base.CaptionForeColor = value;
			}
		}

		/// <summary>
		/// Gets or sets the foreground colour (typically the colour of the text) property of the DataGrid control.
		/// </summary>
		[
		DefaultValue( typeof(Color), "Black" )
		]
		public new Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		/// <summary>
		/// Gets or sets the color of the Grid lines.
		/// </summary>
		[
		DefaultValue( typeof(Color), "DimGray" )
		]
		public new Color GridLineColor
		{
			get
			{
				return base.GridLineColor;
			}
			set
			{
				base.GridLineColor = value;
			}
		}

		/// <summary>
		/// Gets or sets the line style of the grid.
		/// </summary>
		[
		DefaultValue(typeof(System.Windows.Forms.DataGridLineStyle), "Solid")
		]
		public new DataGridLineStyle GridLineStyle
		{
			get
			{
				return base.GridLineStyle;
			}
			set
			{
				base.GridLineStyle = value;
			}
		}	

		/// <summary>
		/// Gets or sets the background colour of all row and column headers.
		/// </summary>
		[
		DefaultValue( typeof(Color), "MidnightBlue" )
		]
		public new Color HeaderBackColor
		{
			get
			{
				return base.HeaderBackColor;
			}
			set
			{
				base.HeaderBackColor = value;
			}
		}

		/// <summary>
		/// Gets or sets the foreground colour of headers.
		/// </summary>
		[
		DefaultValue( typeof(Color), "White" )
		]
		public new Color HeaderForeColor
		{
			get
			{
				return base.HeaderForeColor;
			}
			set
			{
				base.HeaderForeColor = value;
			}
		}

		/// <summary>
		/// Gets or sets the colour of the text that you can click to navigate to a child table.
		/// </summary>
		[
		DefaultValue( typeof(Color), "MidnightBlue" )
		]
		public new Color LinkColor
		{
			get
			{
				return base.LinkColor;
			}
			set
			{
				base.LinkColor = value;
			}
		}

		/// <summary>
		/// Gets or sets the background colour of the selected rows.
		/// </summary>
		[
		DefaultValue( typeof(Color), "CadetBlue" )
		]
		public new Color SelectionBackColor
		{
			get
			{
				return base.SelectionBackColor;
			}
			set
			{
				base.SelectionBackColor = value;
			}
		}

		/// <summary>
		/// Gets or sets the foreground colour of the selected rows..
		/// </summary>
		[
		DefaultValue( typeof(Color), "White" )
		]
		public new Color SelectionForeColor
		{
			get
			{
				return base.SelectionForeColor;
			}
			set
			{
				base.SelectionForeColor = value;
			}
		}

		/// <summary>
		/// Gets or sets the background colour of parent rows.
		/// </summary>
		[
		DefaultValue( typeof(Color), "DarkGray" )
		]
		public new Color ParentRowsBackColor
		{
			get
			{
				return base.ParentRowsBackColor;
			}
			set
			{
				base.ParentRowsBackColor = value;
			}
		}

	
		/// <summary>
		/// Gets or sets the foreground colour of parent rows.
		/// </summary>
		[
		DefaultValue( typeof(Color), "Black" )
		]
		public new Color ParentRowsForeColor
		{
			get
			{
				return base.ParentRowsForeColor;
			}
			set
			{
				base.ParentRowsForeColor = value;
			}
		}

		/// <summary>
		/// Allow Navigation
		/// </summary>
		[
		Browsable(true),
		CategoryAttribute("Custom"),
		Description("Allow parent child table navigation"),
		DefaultValue(false)
		]
		public new bool AllowNavigation
		{
			get
			{
				return base.AllowNavigation;
			}
			set
			{
				base.AllowNavigation = value;
			}
		}

		/// <summary>
		/// The number of rows displayed in the DataGrid
		/// </summary>
		[
		Browsable(true),
		CategoryAttribute("Custom"),
		Description("The number of rows displayted in the DataGrid.")]
		public int Count
		{
			get
			{
				int rowCount = -1;

				// If it has a datasource, use its count
				if ( this.DataSource != null )
				{
					rowCount = this.BindingContext[ this.DataSource, this.DataMember].Count; 
				}

				return rowCount;
			}
		}

		/// <summary>
		/// User is setting the data source.
		/// </summary>
		/// <remarks>Sets up the events handles for Rows added and Deleted.</remarks>
		public new object DataSource
		{
			set
			{
				base.DataSource = value;

				if ( value is DataTable )
				{
					((DataTable)base.DataSource).RowChanged +=new DataRowChangeEventHandler( BoundData_RowChanged);
					((DataTable)base.DataSource).RowDeleted +=new DataRowChangeEventHandler(BoundData_RowDeleted);
				}
				else if ( value is DataView )
				{
					((DataView)base.DataSource).Table.RowChanged +=new DataRowChangeEventHandler(BoundData_RowChanged);
					((DataView)base.DataSource).Table.RowDeleted +=new DataRowChangeEventHandler(BoundData_RowDeleted);
				}

			}
			get
			{
				return base.DataSource;
			}
		}

		/// <summary>
		/// Set of print settings for each column
		/// </summary>
		public PrintColumnSettingsCollection PrintColumnSettings
		{
			set
			{
				this.m_printColumnSettings = value;
			}
			get
			{
				return m_printColumnSettings;
			}
		}

		/// <summary>
		/// The page heading font
		/// </summary>
		[
		Browsable(true),
		CategoryAttribute("Custom"),
		Description("The Page Header Font for Print outs"),
		DefaultValue( typeof(Font) , "Arial, 12pt, Bold")
		]
		public Font PrintPageHeadingFont
		{
			get
			{
				return m_pageHeadingFont;
			}
			set
			{
				m_pageHeadingFont = value;
			}
		}

		/// <summary>
		/// The column heading font
		/// </summary>
		[
		Browsable(true),
		CategoryAttribute("Custom"),
		Description("The Column Header Font for Print outs"),
		DefaultValue( typeof(Font) , "Arial, 10pt, Bold")
		]
		public Font PrintColumnHeadingFont
		{
			get
			{
				return m_columnHeadingFont;
			}
			set
			{
				m_columnHeadingFont = value;
			}
		}

		/// <summary>
		/// The standard font
		/// </summary>
		[
		Browsable(true),
		CategoryAttribute("Custom"),
		Description("The Standard font for Print outs"),
		DefaultValue( typeof(Font) , "Arial, 8pt, Regular")
		]
		public Font PrintStandardFont
		{
			get
			{
				return m_standardFont;
			}
			set
			{
				m_standardFont = value;
			}
		}

		/// <summary>
		/// Print Style
		/// </summary>
		[
		Browsable(true),
		CategoryAttribute("Custom"),
		Description("Print Style of Print outs"),
		DefaultValue( typeof(HBOS.FS.AMP.Windows.Controls.DataGrid.GridPrintStyle) , "Normal")
		]
		public GridPrintStyle PrintGridStyle
		{
			get
			{
				return m_gridPrintStyle;
			}
			set
			{
				m_gridPrintStyle = value;
			}
		}
		
		#endregion

		#region Legacy

		//		/// <summary>
		//		/// Retrieves ONLY the updated custom collection from the grid - unchanged items will NOT appear
		//		/// </summary>
		//		/// <param name="collectedType">The type of collected object</param>
		//		/// <returns>The updated custom collection</returns>
		//		[Description("Retrieves ONLY the modified custom collection from the grid - unchanged items will NOT appear")]
		//		public IList RetrieveUpdatedCustomCollection(System.Type collectedType)
		//		{
		//			if ( m_OriginalCollection != null )
		//			{
		//				// Create a new list
		//				IList newList = (IList)Activator.CreateInstance(m_OriginalCollection.GetType(), false);
		//            
		//				// Loop through each row of the table
		//				for( int i = 0 ; i < m_ConvertedDataTable.Rows.Count ; i ++ )
		//				{
		//					// Check to see if the current row is modified 
		//					DataRow currentRow = m_ConvertedDataTable.Rows[i];
		//				
		//					// Only worry about changed rows
		//					if ( currentRow.RowState != DataRowState.Unchanged )
		//					{
		//						object newObject = createObjectFromDataRow(currentRow, collectedType );
		//						newList.Add( newObject );
		//					}
		//				}
		//				return newList;
		//			}
		//			else
		//			{
		//				return null;
		//			}
		//		}

//        /// <summary>
//        /// Retrieves the entire custom collection from the grid, whether the items have been changed or not
//        /// </summary>
//        /// <param name="collectedType">The type of collected object</param>
//        /// <returns>The updated custom collection</returns>
//        [Description("Retrieves the modified custom collection from the grid")]
//        public IList RetrieveEntireCustomCollection(System.Type collectedType)
//        {
//			if ( m_OriginalCollection != null )
//			{
//				// Create a new list
//				IList newList = (IList)Activator.CreateInstance(m_OriginalCollection.GetType(), false);
//            
//				// Loop through each row of the table
//				for( int i = 0 ; i < m_dataViewToPrint.Count ; i ++ )
//				{
//					// Check to see if the current row is modified 
//					DataRow currentRow = m_dataViewToPrint[ i ].Row;
//					object newObject = createObjectFromDataRow(currentRow, collectedType);
//					newList.Add( newObject );
//				}
//				return newList;
//			}
//			else
//			{
//				return null;
//			}
//        }

//		/// <summary>
//		/// Retrieve a single row from the grid as the appropriate type.
//		/// </summary>
//		/// <param name="collectedType">Type of object we are expecting</param>
//		/// <param name="rowNumber">Row number to retrieve from Grid</param>
//		/// <returns></returns>
//		public object RetrieveObject( System.Type collectedType , int rowNumber )
//		{
//			if ( m_OriginalCollection != null )
//			{
//				if ( rowNumber < m_dataViewToPrint.Count )
//				{
//					DataRow currentRow = m_dataViewToPrint[ rowNumber ].Row;
//					return this.createObjectFromDataRow( currentRow , collectedType );
//				}
//				else
//				{
//					throw new ArgumentException( String.Format("Unable to retrieve row {0} as the grid only contains {1} rows." , rowNumber , m_dataViewToPrint.Count ) );
//				}
//			}
//			else
//			{
//				return null;
//			}
//		}

//		/// <summary>
//		/// Creates an object of type collectedType from a particular DataRow. This function
//		/// also needs to know which row of the table 
//		/// </summary>
//		/// <param name="currentRow"></param>
//		/// <param name="collectedType"></param>
//		/// <returns></returns>
//		private object createObjectFromDataRow(DataRow currentRow, System.Type collectedType)
//		{
//			if (m_OriginalCollection.Count == 0)
//			{
//				return null;
//			}
//            
//			// Create a new object of the correct type and get the original
//			object newObject = Activator.CreateInstance( collectedType , false );
//			//object comparableObject = m_OriginalCollection[0];
//
//			// Get the properties of the collected type, which will be the same for the comparable object
//			PropertyInfo[] properties = collectedType.GetProperties();
//
//			// Loop through each column of the table
//			for( int j = 0 ; j < properties.Length ; j++ )
//			{
//				// Get each property for the table and for the equivalent original object
//				PropertyInfo currentProperty = (PropertyInfo)properties[ j ];
//                    
//
//				object tablePropertyValue = currentRow[ currentProperty.Name ];
//				object originalPropertyValue = currentProperty.GetValue(newObject, BindingFlags.GetProperty, null, null, null );
//
//				// We have a big problem with structs. Enums are structs. So this method won't work.
//				// This is a bit unlucky, as we're going to encounter instances where we need this.
//				// According to various usergroups, using Enum.ToObject should work. 
//				// Certainly the below check does not.
//
//				// if (tablePropertyValue is System.Enum)
//				// {
//				//     Console.WriteLine("ENUM");
//				// }
//
//				// This looks odd, but I've found it much more robust than a null check...
//				// This doesn't work. Bizarrely 
//				//  if (tablePropertyValue.ToString() != String.Empty)
//				// This doesn't seem to work at all
//				//  if (tablePropertyValue == originalPropertyValue)
//				if (tablePropertyValue.ToString().Length != 0)
//				{
//					try
//					{
//						// Is the property Read / Write
//						if ( currentProperty.CanWrite )
//						{
//							if (originalPropertyValue is System.Enum)
//							{
//								currentProperty.SetValue( newObject , Enum.ToObject(originalPropertyValue.GetType(), (int)tablePropertyValue) , BindingFlags.SetProperty , null , null , null ); 
//							}
//							else
//							{
//								currentProperty.SetValue( newObject , tablePropertyValue , BindingFlags.SetProperty , null , null , null ); 
//							}
//						}
//					}
//					catch
//					{
//						// Swallow the exception
//					}
//				}
//			}
//
//			if (newObject is IEntityBase)
//			{
//				// Sort out the dirty flag
//				switch( currentRow.RowState )
//				{
//					case DataRowState.Modified:
//						((IEntityBase)newObject).IsDirty = true;
//						((IEntityBase)newObject).IsNew = false;
//						break;
//					case DataRowState.Added:
//						((IEntityBase)newObject).IsNew = true;
//						((IEntityBase)newObject).IsDirty = false;
//						break;
//					case DataRowState.Deleted:
//						((IEntityBase)newObject).IsDeleted = true;
//						break;
//						// IB: We can't assume unchanged rows are not dirty and not new.
//						// To add new entities, some people do a retrieve, add to the collection and do a rebind.
//						// If they do this twice, it resets the Dirty and New flag on the first record.
//						//					case DataRowState.Unchanged:
//						//						((IEntityBase)newObject).IsDirty = false;
//						//						((IEntityBase)newObject).IsNew = false;
//						//						break;
//
//				}
//			}
//
//			return newObject;
//		}
//
		#endregion
	}
}
 
