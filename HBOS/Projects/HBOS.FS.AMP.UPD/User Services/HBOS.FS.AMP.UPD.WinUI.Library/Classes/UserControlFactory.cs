using System;
using System.Windows.Forms;

using HBOS.FS.AMP.UPD.Types.Funds;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
    /// <summary>
    /// Factory methods for Users user controls
    /// </summary>
    public class UsersUserControlFactory
    {
		/// <summary>
		/// Creates a new <see cref="UsersUserControlFactory"/> instance.
		/// </summary>
        public UsersUserControlFactory()
        {
            //
            // No constructor logic needed here
            //
        }

		/// <summary>
		/// Loads the users collection display control.
		/// </summary>
		/// <returns></returns>
        public UserControl LoadUsersCollectionDisplayControl()
        {
            return new UserControls.UsersControl();
        }
    }
}
