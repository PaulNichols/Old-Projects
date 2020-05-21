using System.Windows.Forms;
using HBOS.FS.AMP.UPD.WinUI.UserControls;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// An interface defining the parts needed to create a static data model
	/// </summary>
	/// <pattern>Builder, GoF Design Patterns</pattern>
	public interface IStaticDataBuilder
	{
		/// <summary>
		/// Allows the builder to configures the static data frame.
		/// </summary>
		/// <param name="frame">Frame.</param>
		void ConfigureFrame(StaticDataFrame frame);

		/// <summary>
		/// Gets the actions for the form. The actions should have their event bound as appropriate.
		/// </summary>
		StaticDataAction[] GetActions();

		/// <summary>
		/// Gets the entity editor instance for the builder.
		/// </summary>
		/// <returns></returns>
		StaticDataEditor GetEntityEditor();

		/// <summary>
		/// Configure listManager so it contains the neccessary data for the current static data screen
		/// </summary>
		/// <param name="listManager">List manager instance to configure.</param>
		void ConfigureListManager(StaticDataListManager listManager);
	}
}