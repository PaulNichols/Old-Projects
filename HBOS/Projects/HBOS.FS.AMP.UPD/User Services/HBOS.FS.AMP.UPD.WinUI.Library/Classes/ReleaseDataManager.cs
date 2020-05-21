using System.Collections;
using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.Types.FundGroups;
using HBOS.FS.AMP.UPD.WinUI.UserControls;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// Data manager for fund status screens
	/// </summary>
	public class ReleaseDataManager : FundStatusDataManager
	{


		/// <summary>
		/// Creates a new <see cref="FundStatusDataManager"/> instance.
		/// </summary>
		public ReleaseDataManager():base ()
		{
		}

		/// <summary>
		/// Loads the data from the fund controller
		/// </summary>
		/// <returns></returns>
		protected override IList loadDataFromSource()
		{
			T.E();
			IList result = null;

			if (this.FundGroupFilter == null)
			{
				result = new ArrayList();
			}
			else
			{
				result = m_fundController.LoadFundLookupsByGroup(this.FundGroupFilter.Key);
			}

			T.X();
			return result;
		}

	
	}
}