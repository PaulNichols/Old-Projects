using System;
using System.Windows.Forms;

namespace HBOS.FS.AMP.Windows.Controls
{
	/// <summary>
	///This super class has been introduced to fix an issue with HeaderText
	/// </summary>
	public class DataGridTextBoxColumnCustom : DataGridTextBoxColumn
	{
		/// <summary>
		/// Gets or sets the header text.
		/// </summary>
		/// <value></value>
		public override string HeaderText
		{
			get { return base.HeaderText; }
			set
			{
				string headerText=value;
				//this property has been overriden to stop truncating of 
				//header text when HorizontalAlignment=Right
				if (Alignment==HorizontalAlignment.Right)
				{
					headerText=headerText.Replace("\n"," \t\n") +" \t";
				}

				makeMultiline(headerText);
			}
		}

		/// <summary>
		/// Alters header font sizes to allow multiline text if necessary.
		/// Sets the header text
		/// </summary>
		/// <param name="headerText"></param>
		private void makeMultiline(string headerText)
		{
			if (headerText.IndexOf("\n")>-1 && DataGridTableStyle!=null && DataGridTableStyle.DataGrid!=null)
			{
				this.DataGridTableStyle.DataGrid.HeaderFont = new System.Drawing.Font("Tahoma", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				base.HeaderText = headerText;
				this.DataGridTableStyle.HeaderFont = DataGridTableStyle.DataGrid.Font;
			}
			else
			{
				base.HeaderText = headerText;
			}
		}
	}
}