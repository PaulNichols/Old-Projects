using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Collections;
using System.Collections.Specialized;	// IEnumeration
using System.ComponentModel;			// IListSource


namespace HBOS.FS.AMP.Windows.Controls
{
	/// <summary>
	/// DataGridTextBoxColumn - allows a combo style column in a data grid
	/// </summary>
	/// <remarks>Based on http://msdn.microsoft.com/msdnmag/issues/03/08/DataGrids/default.aspx
	/// </remarks>
	/// <example>
	/// While the DataGrid is bound to a DataView, the Combo column must also be bound to a data source. 
	/// An example is as follows:
	/// <code escaped="true">
	/// dataGridComboBoxColumn1.DataSource = m_AllTitlesTable;
	/// dataGridComboBoxColumn1.DisplayMember = "TitleName";
	/// dataGridComboBoxColumn1.ValueMember = "TitleID";
	/// </code>
	/// </example>
	public class DataGridComboBoxColumn : DataGridTextBoxColumn , ICustomDataGridColumn
	{
		#region Private Member Variables

		// Hosted ComboBox control
		private ComboBox comboBox;
		private CurrencyManager cm;
		private int m_selectedRow;
		
		private IList m_iList = null;
		private ITypedList m_iTypedList = null;
		private PropertyDescriptorCollection m_propertyDescriptorCollection = null;
		private PropertyDescriptor m_propertyDescriptorDisplayMember = null;
		private PropertyDescriptor m_propertyDescriptorValueMember = null;

		private string m_toolTipProperty = "";

		#endregion
		
		#region Events and delegates

		/// <summary>
		/// Selected index changed delegate
		/// </summary>
		public delegate void SelectedIndexChangedDelegate( object sender, ComboBoxEventArgs e );

		/// <summary>
		/// Selected Index changed delegate
		/// </summary>
		public event SelectedIndexChangedDelegate SelectedIndexChanged;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor - create combobox, register selection change event handler and register lose focus event handler
		/// </summary>
		public DataGridComboBoxColumn()
		{
			this.cm = null;

			// Create ComboBox and force DropDownList style
			this.comboBox = new ComboBox();
			this.comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			
			// Add event handler for notification of when ComboBox loses focus
			this.comboBox.Leave += new EventHandler(comboBox_Leave);

			// Add event handler for selected item changes
			this.comboBox.SelectedIndexChanged += new EventHandler(comboBox_SelectedIndexChanged);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Set the combo box data source
		/// </summary>
		public object DataSource
		{
			set
			{
				comboBox.DataSource = value;
			}
			get
			{
				return comboBox.DataSource;
			}
		}

		/// <summary>
		/// Set the combo box Display Member
		/// </summary>
		public string DisplayMember
		{
			get
			{
				return comboBox.DisplayMember;
			}
			set
			{
				comboBox.DisplayMember = value;
			}
		}

		/// <summary>
		/// Set the combo box Value member
		/// </summary>
		public string ValueMember
		{
			get
			{
				return comboBox.ValueMember;
			}
			set
			{
				comboBox.ValueMember = value;
			}
		}

		/// <summary>
		/// Access to the IList for the combo box
		/// </summary>
 		private IList IList
		{
			get
			{
				if ( m_iList == null )
				{
					if ( this.comboBox.DataSource is IListSource )
					{
						m_iList = ((IListSource)this.comboBox.DataSource).GetList();
					}
					else if ( this.comboBox.DataSource is IList )
					{
						m_iList = ((IList)this.comboBox.DataSource);
					}
					else
					{
						throw new ApplicationException( "The Combo box column must have a datasource that supports either IListSource or IList" );
					}
				}

				return m_iList;
			}
		}

		/// <summary>
		/// Access to the ITyped List for the combobox
		/// </summary>
		private ITypedList ITypedList
		{
			get
			{
				if ( m_iTypedList == null )
				{
					IList myList = this.IList;

					if ( myList is ITypedList )
					{
						m_iTypedList = (ITypedList)myList;
					}
					else
					{
						throw new ApplicationException( "The DataSource for the ComboBox column must support ITypedList." );
					}
				}
				
				return m_iTypedList;
			}
		}

		/// <summary>
		/// Access to the Property Descriptor Collection
		/// </summary>
		private PropertyDescriptorCollection propertyDescriptorCollection
		{
			get
			{
				if ( m_propertyDescriptorCollection == null )
				{
					m_propertyDescriptorCollection = this.ITypedList.GetItemProperties( null );
				}

				return m_propertyDescriptorCollection;
			}
		}

		/// <summary>
		/// Access to the Display Member
		/// </summary>
		private PropertyDescriptor propertyDescriptorDisplayMember
		{
			get
			{
				if ( m_propertyDescriptorDisplayMember == null )
				{
					m_propertyDescriptorDisplayMember = this.propertyDescriptorCollection.Find( this.comboBox.DisplayMember, true);
				}

				return m_propertyDescriptorDisplayMember;
			}
		}

		/// <summary>
		/// Access to the Display Member
		/// </summary>
		private PropertyDescriptor propertyDescriptorValueMember
		{
			get
			{
				if ( m_propertyDescriptorValueMember == null )
				{
					m_propertyDescriptorValueMember = this.propertyDescriptorCollection.Find( this.comboBox.ValueMember, true);
				}

				return m_propertyDescriptorValueMember;
			}
		}

		#endregion
		
		#region Methods
																									
		/// <summary>
		/// On edit, add scroll event handler, and display combo box
		/// </summary>
		/// <param name="source">The data source for the grid</param>
		/// <param name="rowNum">The row number affected.</param>
		/// <param name="bounds"></param>
		/// <param name="readOnly">IS the grid read only</param>
		/// <param name="instantText"></param>
		/// <param name="cellIsVisible">Is the cell visible</param>
 		protected override void Edit(System.Windows.Forms.CurrencyManager source, int rowNum, System.Drawing.Rectangle bounds, bool readOnly, string instantText, bool cellIsVisible)
		{
			try
			{
				if (!readOnly && cellIsVisible)
				{
					base.Edit(source, rowNum, bounds, readOnly, instantText, cellIsVisible);

					// Save current row in the datagrid and currency manager associated with
					// the data source for the datagrid
					this.m_selectedRow = rowNum;
					this.cm = source;
		
					// Add event handler for datagrid scroll notification
					this.DataGridTableStyle.DataGrid.Scroll += new EventHandler(DataGrid_Scroll);

					// Site the combo box control within the bounds of the current cell
					this.comboBox.Parent = this.TextBox.Parent;
					Rectangle rect = this.DataGridTableStyle.DataGrid.GetCurrentCellBounds();
					this.comboBox.Location = rect.Location;
					this.comboBox.Size = new Size(this.TextBox.Size.Width, this.comboBox.Size.Height);

					// Set combo box selection to given text
					this.comboBox.SelectedIndexChanged  -= new EventHandler(comboBox_SelectedIndexChanged);
					this.comboBox.SelectedIndex = this.comboBox.FindStringExact(this.TextBox.Text);
					this.comboBox.SelectedIndexChanged += new EventHandler(comboBox_SelectedIndexChanged);

					// Make the ComboBox visible and place on top text box control
					this.comboBox.Show();
					this.comboBox.BringToFront();
					this.comboBox.Focus();
				}
				else
				{
					if ( readOnly && cellIsVisible )
					{
						//make sure previous selection is valid 
						if( m_selectedRow > -1 && m_selectedRow < source.List.Count + 1) 
						{
							this.DataGridTableStyle.DataGrid.UnSelect( m_selectedRow ); 
						}
 
						m_selectedRow = rowNum; 
 
						this.DataGridTableStyle.DataGrid.Select( m_selectedRow ); 

					}
				}
			}
			catch( Exception Ex )
			{
				MessageBox.Show( String.Format("An error occurred while editing the contents of the {0} column.{1}The error was '{2}'." , comboBox.Name , Environment.NewLine , Ex.Message ) , "Data Grid Error" , MessageBoxButtons.OK , MessageBoxIcon.Error );
			}
		}

		/// <summary>
		/// Given a row, get the value member associated with a row.  Use the value
		/// member to find the associated display member by iterating over bound datasource
		/// </summary>
		/// <param name="source">Data source for the column</param>
		/// <param name="rowNum">Affected row number</param>
		/// <returns>The column value for the row</returns>
		/// <remarks>Changed this so that if the value was Null, it sets the value to be the first in the combo</remarks>
		protected override object GetColumnValueAtRow(System.Windows.Forms.CurrencyManager source, int rowNum)
		{
			try
			{
				Debug.WriteLine(String.Format("GetColumnValueAtRow {0}", rowNum));
				// Given a row number in the datagrid, get the display member
				object obj =  base.GetColumnValueAtRow(source, rowNum);
			
				// Iterate through the datasource bound to the ColumnComboBox
				// Don't confuse this datasource with the datasource of the associated datagrid

				int i;

				// Find the match for the value
				for (i = 0; i < this.IList.Count; i++)
				{
					if ( obj.Equals( this.propertyDescriptorValueMember.GetValue( this.IList[ i ] ) ) )
						break;
				}
			
				// Have we found the right value
				if (i < this.IList.Count )
				{
					return this.propertyDescriptorDisplayMember.GetValue( this.IList[ i ] );
				}
				else
				{
					// Default to the first item
					if ( this.IList.Count > 0 )
					{
						SetColumnValueAtRow( source , rowNum , this.propertyDescriptorValueMember.GetValue( this.IList[ 0 ] ) );
						return this.propertyDescriptorDisplayMember.GetValue( this.IList[ 0 ] );
					}
					else
					{
						return DBNull.Value;
					}
				}
			}
			catch( Exception Ex )
			{
				MessageBox.Show( String.Format("An error occurred while getting the column value at a row {0}.{1}The error was '{2}'." , rowNum , Environment.NewLine , Ex.Message ) , "Data Grid Error" , MessageBoxButtons.OK , MessageBoxIcon.Error);
				return DBNull.Value;
			}
		}

		/// <summary>
		/// Given a row and a display member, iterating over bound datasource to find
		/// the associated value member.  Set this value member.
		/// </summary>
		/// <param name="source">The data source for the grid</param>
		/// <param name="rowNum">The row number affected</param>
		/// <param name="cellValue">The value to set for the cell.</param>
		protected override void SetColumnValueAtRow(System.Windows.Forms.CurrencyManager source, int rowNum, object cellValue)
		{
			try
			{
				Debug.WriteLine(String.Format("SetColumnValueAtRow {0} {1}", rowNum, cellValue));
				object s = cellValue;

				// Iterate through the datasource bound to the ColumnComboBox
				// Don't confuse this datasource with the datasource of the associated datagrid
				int i;

				for (i = 0; i < this.IList.Count; i++)
				{
					if (s.Equals( this.propertyDescriptorDisplayMember.GetValue( this.IList[ i ] ) ) )
						break;
				}

				if( i >= this.IList.Count )
				{
					// We couldn't find it in the Display Member, so look in the Value member
					for (i = 0; i < this.IList.Count; i++)
					{
						if (s.Equals( this.propertyDescriptorValueMember.GetValue( this.IList[ i ] ) ) )
							break;
					}
				}

				// If set item was found return corresponding value, otherwise return DbNull.Value
				if(i < this.IList.Count)
					s = this.propertyDescriptorValueMember.GetValue( this.IList[ i ] );
				else
					s = DBNull.Value;

				base.SetColumnValueAtRow(source, rowNum, s);
			}
			catch( Exception Ex )
			{
				MessageBox.Show( String.Format("An error occurred while setting the column value for row {0} to value {1}.{2}The error was '{3}'." , rowNum , cellValue.ToString() , Environment.NewLine , Ex.Message ) , "Data Grid Error" , MessageBoxButtons.OK , MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// On datagrid scroll, hide the combobox
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DataGrid_Scroll(object sender, EventArgs e)
		{
			try
			{
				Debug.WriteLine("Scroll");
				this.comboBox.Hide();
			}
			catch( Exception Ex )
			{
				MessageBox.Show( String.Format("An error occurred while scrolling in the dataGrid.{0}The error was '{1}'." , Environment.NewLine , Ex.Message ) , "Data Grid Error" , MessageBoxButtons.OK , MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// On combo box losing focus, set the column value, hide the combo box,
		/// and unregister scroll event handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comboBox_Leave(object sender, EventArgs e)
		{
			try
			{
				object selectedDisplay = this.propertyDescriptorDisplayMember.GetValue( this.IList[ this.comboBox.SelectedIndex ] );

				Debug.WriteLine(String.Format("Leave: {0}", selectedDisplay.ToString() ));

				SetColumnValueAtRow( this.cm, this.m_selectedRow, selectedDisplay);
				Invalidate();

				this.comboBox.Hide();
				this.DataGridTableStyle.DataGrid.Scroll -= new EventHandler(DataGrid_Scroll);			
			}
			catch( Exception Ex )
			{
				this.comboBox.Hide();
				MessageBox.Show( String.Format("An error occurred while leaving the comboBox column.{0}The error was '{1}'." , Environment.NewLine , Ex.Message ), "Data Grid Error" , MessageBoxButtons.OK , MessageBoxIcon.Error );
			}
		}

		/// <summary>
		/// Combo box has changed index
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				// Raise an event if we need to
				if ( SelectedIndexChanged != null )
				{
					object selectedValue = this.propertyDescriptorValueMember.GetValue( this.IList[ this.comboBox.SelectedIndex ] );
					object selectedDisplay = this.propertyDescriptorDisplayMember.GetValue( this.IList[ this.comboBox.SelectedIndex ] );

					ComboBoxEventArgs newComboBoxEventArgs = new ComboBoxEventArgs( m_selectedRow , selectedValue , selectedDisplay );
					SelectedIndexChanged( sender , newComboBoxEventArgs);
				}
			}
			catch( Exception Ex )
			{
				MessageBox.Show( String.Format("An error occurred while changing the selected index in the ComboBox column.{0}The error was '{1}'." , Environment.NewLine , Ex.Message ), "Data Grid Error" , MessageBoxButtons.OK , MessageBoxIcon.Error );
			}
		}

		#endregion

		#region ICustomDataGridColumn Members

		/// <summary>
		/// The tool tip property
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

