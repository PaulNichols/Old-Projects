using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;


namespace HBOS.FS.AMP.Windows.Controls
{
	/// <summary>
	/// DataGridBool1ClickColumn - check box editing with 1 click
	/// </summary>
	/// <remarks>Sourced from http://www.syncfusion.com/FAQ/WinForms/FAQ_c44c.asp#q945q</remarks>
	public class DataGridBool1ClickColumn : DataGridBoolColumn , ICustomDataGridColumn
	{
		#region Variables

		private bool m_canCheck = true;
		private bool m_canUnCheck = true;
		private string m_toolTipProperty = "";

		#endregion

		#region Properties

		/// <summary>
		/// Has the user the appropriate permissions to check the checkbox
		/// </summary>
		public bool CanCheck
		{
			get
			{
				return m_canCheck;
			}
			set
			{
				m_canCheck = value;
			}
		}

		/// <summary>
		/// Has the user got the permissions to uncheck the checkbox.
		/// </summary>
		public bool CanUnCheck
		{
			get
			{
				return m_canUnCheck;
			}
			set
			{
				m_canUnCheck = value;
			}
		}

		#endregion

		#region Events and Delegates

		/// <summary>
		/// Occurs when the data in a row is changed.
		/// </summary>
		[Description("Occurs when the data in a row is changed")]
		public event CheckedChangedDelegate CheckedChanged;

		/// <summary>
		/// Occurs when the user doen't have sufficient permissions to perform an operation!
		/// </summary>
		[Description("Occurs when the user doesn't have sufficient permissions to perform an operation.")]
		public event InsufficientPermissionsDelegate InsufficientPermissions;

		/// <summary>
		/// Check box value has changed
		/// </summary>
		public delegate void CheckedChangedDelegate(object sender, CheckBoxEventArgs e);

		/// <summary>
		/// Insufficient permissions to perform operation
		/// </summary>
		public delegate void InsufficientPermissionsDelegate(object sender, CheckBoxEventArgs e);

		#endregion

		#region Methods

		/// <summary>
		/// Signal the column to raise an CheckedChanged event if there is a listener
		/// </summary>
		protected  void OnCheckedChanged( object sender, CheckBoxEventArgs e )
		{
			// Is anyone listening.
			if ( this.CheckedChanged != null )
			{
				this.CheckedChanged( sender , e );
			}
		}

		/// <summary>
		/// Signal the column to raise an insufficient permission event if there is a listener
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void OnInsufficientPermissions( object sender, CheckBoxEventArgs e )
		{
			// Is anyone listening
			if ( this.InsufficientPermissions != null )
			{
				this.InsufficientPermissions( sender , e );
			}
		}

		/// <summary>
		/// Default to not allowing Nulls
		/// </summary>
		private void InitializeComponent()
		{
			this.AllowNull = false;
		}

		/// <summary>
		/// Set value
		/// </summary>
		/// <param name="source"></param>
		/// <param name="rowNum"></param>
		/// <param name="cellValue"></param>
		protected override void SetColumnValueAtRow(System.Windows.Forms.CurrencyManager source, int rowNum, object cellValue)
		{
			try
			{
				Debug.WriteLine(String.Format("SetColumnValueAtRow {0} {1}", rowNum, cellValue));
				object s = cellValue;

				bool newValue = (bool)s;
				bool currentValue = !newValue;


				// Figure out the column number
				int columnNumber = this.DataGridTableStyle.GridColumnStyles.IndexOf( this );

				// Is the column readonly 
				if ( this.ReadOnly == false )
				{
					
					DataTable sourceListAsDataTable = null;
					
					if (source.List is DataView)
					{
						sourceListAsDataTable = ((DataView) source.List).Table;
					}
					else
					{
						sourceListAsDataTable = source.List as DataTable;
					}

					if ( currentValue == true )
					{
						if ( this.CanUnCheck )
						{
							CheckBoxEventArgs checkBoxEventArgs = new CheckBoxEventArgs( rowNum , columnNumber , DataGrid.HitTestType.Cell , currentValue , !currentValue );
							if (sourceListAsDataTable != null)
							{
								//we need to do it this way in order to set the RowState property.
								//bug in Microsoft code means that SetColumnValueAtRow doesn't set it (or rather sets it intermittently)
								sourceListAsDataTable.Rows[rowNum][this.MappingName] = false;
							}
							else
							{
								base.SetColumnValueAtRow(source, rowNum, false);
							}

							this.OnCheckedChanged( this , checkBoxEventArgs );
						}
						else
						{
							CheckBoxEventArgs checkBoxEventArgs = new CheckBoxEventArgs( rowNum , columnNumber , DataGrid.HitTestType.Cell , currentValue , currentValue );
							if (sourceListAsDataTable != null)
							{
								//we need to do it this way in order to set the RowState property.
								//bug in Microsoft code means that SetColumnValueAtRow doesn't set it (or rather sets it intermittently)
								sourceListAsDataTable.Rows[rowNum][this.MappingName] = true;
							}
							else
							{
								base.SetColumnValueAtRow(source, rowNum, true);
							}
							this.OnInsufficientPermissions( this , checkBoxEventArgs );
						}
					}
					else
					{
						if ( this.CanCheck )
						{
							CheckBoxEventArgs checkBoxEventArgs = new CheckBoxEventArgs( rowNum , columnNumber , DataGrid.HitTestType.Cell , currentValue , !currentValue );
							if (sourceListAsDataTable != null)
							{
								//we need to do it this way in order to set the RowState property.
								//bug in Microsoft code means that SetColumnValueAtRow doesn't set it (or rather sets it intermittently)
								sourceListAsDataTable.Rows[rowNum][this.MappingName] = true;
							}
							else
							{
								base.SetColumnValueAtRow(source, rowNum, true);
							}
							this.OnCheckedChanged( this , checkBoxEventArgs );
						}
						else
						{
							CheckBoxEventArgs checkBoxEventArgs = new CheckBoxEventArgs( rowNum , columnNumber , DataGrid.HitTestType.Cell , currentValue , currentValue );
							if (sourceListAsDataTable != null)
							{
								//we need to do it this way in order to set the RowState property.
								//bug in Microsoft code means that SetColumnValueAtRow doesn't set it (or rather sets it intermittently)
								sourceListAsDataTable.Rows[rowNum][this.MappingName] = false;
							}
							else
							{
								base.SetColumnValueAtRow(source, rowNum, false);
							}
							this.OnInsufficientPermissions( this , checkBoxEventArgs );
						}
					}
				}
			}
			catch( Exception Ex )
			{
				MessageBox.Show( String.Format("An error occurred while setting the column value for row {0} to value {1}.{2}The error was '{3}'." , rowNum , cellValue.ToString() , Environment.NewLine , Ex.Message ) , "Data Grid Error" , MessageBoxButtons.OK , MessageBoxIcon.Error);
			}
		}

		#endregion

		#region ICustomDataGridColumn Members

		/// <summary>
		/// The tooltip property
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