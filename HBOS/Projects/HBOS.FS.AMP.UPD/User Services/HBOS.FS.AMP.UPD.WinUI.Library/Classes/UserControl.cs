using System;
using System.Windows.Forms;

using HBOS.FS.AMP.UPD.Types.Funds;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
    /// <summary>
    /// Summary description for UserControl.
    /// </summary>
    public class UsersUserControl
    {
        public UsersUserControl()
        {
            //
            // No constructor logic needed here
            //
        }

        public UserControl LoadUsersCollectionDisplayControl()
        {
            return new UserControls.UsersControl();
        }
    }
}
