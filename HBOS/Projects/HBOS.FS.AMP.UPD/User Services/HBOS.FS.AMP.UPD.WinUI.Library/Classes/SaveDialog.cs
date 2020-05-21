using System.Windows.Forms;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// Summary description for SaveDialog.
	/// </summary>
	public class SaveDialog
	{
		/// <summary>
		/// 
		/// </summary>
		public SaveDialog()
		{
		}

		/// <summary>
		/// Displays a do you wish to save dialog box accepting Yes No or Cancel
		/// </summary>
		/// <returns></returns>
		public static YesNoCancelAction Show()
		{
			T.E();
			//default to 'no we don't want to save'
			YesNoCancelAction userAction = YesNoCancelAction.no;

			try
			{
				switch (MessageBoxHelper.Show("GenericSaveBody", "GenericSaveTitle", MessageBoxButtons.YesNoCancel))
				{
					case DialogResult.Yes:

						userAction = YesNoCancelAction.yes;
						break;

					case DialogResult.No:
						userAction = YesNoCancelAction.no;
						break;

					case DialogResult.Cancel:
						userAction = YesNoCancelAction.cancel;
						break;

					default:
						userAction = YesNoCancelAction.no;
						break;
				}
			}
			finally
			{
				T.X();
			}
			return userAction;
		}

	}
}