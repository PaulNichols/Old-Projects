using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;


namespace HBOS.FS.AMP.Windows.Controls
{
	/// <summary>
	/// Summary description for DataGridDateColumn.
	/// </summary>
	public class DataGridDateColumn : DataGridTextBoxColumn, ICustomDataGridColumn
	{

		#region Member Variables

		private string m_toolTipProperty = "";

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

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#region constructors

		/// <summary>
		/// creates a new col
		/// </summary>
		/// <param name="container"></param>
		public DataGridDateColumn(System.ComponentModel.IContainer container)
		{
			container.Add(this);
			InitializeComponent();
			this.Format = "dd/MM/yyyy"; // the default format - UK date
		}

		/// <summary>
		/// creates a new col
		/// </summary>
		public DataGridDateColumn() 
		{
			InitializeComponent();
			this.Format = "dd/MM/yyyy"; // the default format - UK date
		}

		#endregion

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion

		#region Date specific handling

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
				try
				{
					DateTime dateEntered = Convert.ToDateTime( obj.ToString() );
					if (dateEntered == DateTime.MinValue || dateEntered == DateTime.MaxValue)
					{
						//these are used to indicate not set
						return "Unavailable";
					}
					else
					{
						return dateEntered.ToString( this.Format );
					}

				}
				catch
				{
					return "Unavailable";
				}

			}
			else
			{
				return DBNull.Value;
			}
		}

		#endregion


	}
}
