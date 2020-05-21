using System.Windows.Forms;
using HBOS.FS.AMP.UPD.WinUI.UserControls;
using HBOS.FS.AMP.Windows.Controls;
using HBOS.FS.Support.Tex;
using HBOSGrid = HBOS.FS.AMP.Windows.Controls.DataGrid;

namespace HBOS.FS.AMP.UPD.WinUI.Helpers
{
	/// <summary>
	/// Contains a number of common HBOS Data grid column style formatting routines
	/// </summary>
	/// <remarks>Abstract to stop people creating instances of the class</remarks>
	public abstract class GridColumnFormattingHelper
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <remarks>Done like this to prevent construction of the static class.  It will throw an error</remarks>
		private GridColumnFormattingHelper()
		{
		}

		#region Add column styles

		/// <summary>
		/// Add a text box column style to the data grid table style
		/// </summary>
		/// <param name="grid">HBOS Data grid object</param>
		/// <param name="mappingName">Object property name to bind to</param>
		/// <param name="headerText">Heading text displayed for the column</param>
		/// <param name="width">Default column width</param>
		/// <param name="alignment">Column alignment</param>
		/// <param name="toolTipText">Tool tip text to display</param>
		public static void AddTextBoxReadOnlyColumnStyle(HBOSGrid grid, string mappingName,
		                                                 string headerText, int width,
		                                                 HorizontalAlignment alignment, string toolTipText)
		{
			T.E();
			DataGridTextBoxReadOnlyColumn columnStyle = new DataGridTextBoxReadOnlyColumn();

			try
			{
				// Add column style to table style.
				T.Log("Add the column style to the table style");
				grid.TableStyles[0].GridColumnStyles.Add(columnStyle);

				// Build column attributes
				T.Log("Build the TextBox Read Only column attributes.");
				columnStyle.MappingName = mappingName;
				columnStyle.Alignment = alignment;
				columnStyle.HeaderText = headerText;
				columnStyle.Width = width;
				columnStyle.ToolTipProperty = toolTipText;

			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Add a number column style to the data grid table style
		/// </summary>
		/// <param name="grid">HBOS Data grid object</param>
		/// <param name="mappingName">Object property name to bind to</param>
		/// <param name="headerText">Heading text displayed for the column</param>
		/// <param name="width">Default column width</param>
		/// <param name="decimalPlaces">Number of decimal places visible</param>
		/// <param name="alignment">Column alignment</param>
		/// <param name="toolTipText">Tool tip text to display</param>
		public static void AddNumberColumnStyle(HBOSGrid grid, string mappingName,
		                                        string headerText, int width, int decimalPlaces,
		                                        HorizontalAlignment alignment, string toolTipText)
		{
			T.E();
			try
			{
				DataGridNumberColumn columnStyle = new DataGridNumberColumn();

				// Add column style to table style.
				T.Log("Add the column style to the table style");
				grid.TableStyles[0].GridColumnStyles.Add(columnStyle);

				// Build column attributes
				T.Log("Build the number column attributes.");
				columnStyle.MappingName = mappingName;
				columnStyle.Alignment = alignment;
				columnStyle.HeaderText = headerText;
				columnStyle.Width = width;
				columnStyle.DecimalPlaces = decimalPlaces;
				columnStyle.ToolTipProperty = toolTipText;

			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Add a percent column style to the data grid table style
		/// </summary>
		/// <param name="grid">HBOS Data grid object</param>
		/// <param name="mappingName">Object property name to bind to</param>
		/// <param name="headerText">Heading text displayed for the column</param>
		/// <param name="width">Default column width</param>
		/// <param name="decimalPlaces">Number of decimal places visible</param>
		/// <param name="alignment">Column alignment</param>
		/// <param name="toolTipText">Tool tip text to display</param>
		public static void AddPercentColumnStyle(HBOSGrid grid, string mappingName,
		                                         string headerText, int width, int decimalPlaces,
		                                         HorizontalAlignment alignment, string toolTipText)
		{
			T.E();
			try
			{
				DataGridPercentageColumn columnStyle = new DataGridPercentageColumn();

				// Add column style to table style.
				T.Log("Add column style to table style");
				grid.TableStyles[0].GridColumnStyles.Add(columnStyle);

				// Build column attributes
				T.Log("Build percentage column attributes");
				columnStyle.MappingName = mappingName;
				columnStyle.Alignment = alignment;
				columnStyle.HeaderText = headerText;
				columnStyle.Width = width;
				columnStyle.DecimalPlaces = decimalPlaces;
				columnStyle.ToolTipProperty = toolTipText;

			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Add a boolean style column
		/// </summary>
		/// <param name="grid">HBOS Data grid object</param>
		/// <param name="mappingName">Object property name to bind to</param>
		/// <param name="headerText">Heading text displayed for the column</param>
		/// <param name="width">Default column width</param>
		/// <param name="alignment">Column alignment</param>
		/// <returns>DataGridBool1ClickColumn</returns>
		public static DataGridBool1ClickColumn AddBooleanColumnStyle(HBOSGrid grid,
		                                                             string mappingName, string headerText, int width,
		                                                             HorizontalAlignment alignment)
		{
			T.E();
			// Add a boolean column
			DataGridBool1ClickColumn columnStyle = new DataGridBool1ClickColumn();

			try
			{
				// Add column style to table style.
				T.Log("Add the column style to the table style");
				grid.TableStyles[0].GridColumnStyles.Add(columnStyle);

				// Build column attributes
				T.Log("Build the Bool 1 Click column attributes.");
				columnStyle.MappingName = mappingName;
				columnStyle.Alignment = alignment;
				columnStyle.HeaderText = headerText;
				columnStyle.Width = width;

			}
			finally
			{
				T.X();
			}
			return columnStyle;
		}

		/// <summary>
		/// Add a text box column style to the data grid table style
		/// </summary>
		/// <param name="grid">HBOS Data grid object</param>
		/// <param name="mappingName">Object property name to bind to</param>
		/// <param name="headerText">Heading text displayed for the column</param>
		/// <param name="width">Default column width</param>
		/// <param name="alignment">Column alignment</param>
		/// <param name="toolTipText">Tool tip text to display</param>
		/// <returns>DataGridTextBoxColumn</returns>
		public static DataGridTextBoxColumn AddTextBoxColumnStyle(HBOSGrid grid, string mappingName,
		                                                          string headerText, int width,
		                                                          HorizontalAlignment alignment, string toolTipText)
		{
			DataGridTextBoxColumn columnStyle = new DataGridTextBoxColumn();

			T.E();
			try
			{
				// Add column style to table style.
				T.Log("Add the column style to the table style");
				grid.TableStyles[0].GridColumnStyles.Add(columnStyle);

				// Build column attributes
				T.Log("Build the TextBox column attributes.");
				columnStyle.MappingName = mappingName;
				columnStyle.Alignment = alignment;
				columnStyle.HeaderText = headerText;
				columnStyle.Width = width;

			}
			finally
			{
				T.X();
			}
			return columnStyle;
		}

		/// <summary>
		/// Add a column containing a button and text box style to the data grid table style
		/// </summary>
		/// <param name="grid">HBOS Data grid object</param>
		/// <param name="mappingName">Object property name to bind to</param>
		/// <param name="headerText">Heading text displayed for the column</param>
		/// <param name="width">Default column width</param>
		/// <param name="alignment">Column alignment</param>
		/// <param name="toolTipText">Tool tip text to display</param>
		/// <returns>DataGridTextBoxColumn</returns>
		public static DataGridTextBoxColumn AddButtonTextBoxColumnStyle(HBOSGrid grid, string mappingName,
		                                                                string headerText, int width,
		                                                                HorizontalAlignment alignment, string toolTipText)
		{
			DataGridTextBoxColumn columnStyle = new DataGridTextBoxColumn();
			ButtonTextBox btnTextBox = new ButtonTextBox();
			btnTextBox.Dock = DockStyle.Fill;

			T.E();
			try
			{
				// Add column style to table style.
				T.Log("Add the column style to the table style");

				// Build column attributes
				T.Log("Build the Button TextBox column attributes.");
				columnStyle.MappingName = mappingName;
				columnStyle.Alignment = alignment;
				columnStyle.HeaderText = headerText;
				columnStyle.Width = width;
				columnStyle.TextBox.TextAlign = HorizontalAlignment.Center;
				columnStyle.TextBox.Controls.Add(btnTextBox);

				grid.TableStyles[0].GridColumnStyles.Add(columnStyle);
			}
			finally
			{
				T.X();
			}
			return columnStyle;
		}

		#endregion
	}
}