using System;
using System.Windows.Forms;
using Microsoft.Samples.Windows.Forms.Navigation;
using HBOS.FS.AMP.UPD.WinUI.UserControls;
using HBOS.FS.AMP.UPD.WinUI.Forms;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// Entry point for the fund group manager
	/// </summary>
	public class FundGroupManagerEntryPoint
	{
		/// <summary>
		/// Runs the fund group manager.
		/// </summary>
        [STAThread]
        public static void RunFundGroupManager()
        {
			//Run the Fund Group manager
			FundGroupManager manager = new FundGroupManager();
        }

	}
}
