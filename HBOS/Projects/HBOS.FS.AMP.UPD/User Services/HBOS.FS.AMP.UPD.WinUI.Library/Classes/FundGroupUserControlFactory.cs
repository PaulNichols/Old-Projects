using System;
using System.Windows.Forms;
using HBOS.FS.AMP.UPD.Types;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// Create the required Fund Group User Control
	/// </summary>
	public class FundGroupUserControlFactory
	{

		#region Constructors		

		public FundGroupUserControlFactory()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#endregion Constructors
	
		public static UserControl LoadFundGroupsGrid()
		{
			return new UserControls.FundGroupsUC();
		}
	}
}
