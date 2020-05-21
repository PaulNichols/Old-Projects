using System;

namespace HBOS.FS.AMP.Windows.Controls
{
#if (DEBUG)
	/// <summary>
	/// <p>The <see cref="HBOS.FS.AMP.Windows.Controls"/> namespace provides generic AMP Window Controls.
	/// These include:</p> 
	/// <list type="bullet">
	/// <item><see cref="DataGrid"/> - an enhanced datagrid allowing printing, exporting, and custom column types.</item>
	/// <item><see cref="NumericTextBox"/> - a textbox which only allows numeric text entry, and has a Max and Min value.</item>
	/// <item><see cref="EnumComboBox"/> - a combo that can be populated with an enum with the appropriate attributes.</item>
	/// <item><see cref="SplashScreenForm"/> - a generic splash screen.</item>
	/// </list>
	/// <p>The datagrid also includes extended Designer Support. This also the user to choose the new custom column styles through the GUI designer.
	/// <list type="bullet">
	/// <item><see cref="HBOSColumnStylesCollectionEditor"></see> - custimised column styles collection editor.</item>
	/// <item><see cref="HBOSTableStyle"></see> - custimised table style editor.</item>
	/// <item><see cref="HBOSTableStylesCollectionEditor"></see> - custimised table styles collection editor.</item>
	/// </list>
	/// </p>
	/// <p>The custom column types currently supported by the Grid include
	/// <list type="bullet">
	/// <item><see cref="DataGridComboBoxColumn"></see> - a column with a combo box</item>
	/// <item><see cref="DataGridImageColumn"></see> - a column with an image.</item>
	/// <item><see cref="DataGridTextBoxReadOnlyColumn"></see> - a normal column but it can't receive the focus.</item>
	/// </list>
	/// </p>
	/// <para><see cref="HBOS.FS.AMP.Windows.Controls"/> is dependent upon HBOS.FS.AMP.Utilities. The DataGrid uses <see cref="HBOS.FS.AMP.Utilities.ResourceHelper"/> for access to embedded resource for its write functionality.</para>
	/// </summary>
	public class NamespaceDoc
	{
		/// <summary>
		/// This class is a dummy class used by NDoc to provide a namespace summary.
		/// </summary>
		public NamespaceDoc()
		{
		}
	}
#endif
}

