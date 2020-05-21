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
	public class FundStatusDataManager : StatusDataManager
	{


		#region Member variables

		#region Cached fund group and controller

		//these are needed in order to test fundgroup to see if authorise all button can be enabled
		private FundGroupController m_fundGroupController = null;
		private FundGroup m_currentFundGroup = null;

		#endregion

		#region Cached fund controller

		/// <summary>
		/// 
		/// </summary>
		protected FundController m_fundController;

		#endregion

		#endregion


		/// <summary>
		/// Creates a new <see cref="FundStatusDataManager"/> instance.
		/// </summary>
		public FundStatusDataManager()
		{
			T.E();
			m_fundController = new FundController(GlobalRegistry.ConnectionString);
			T.X();
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
				result = m_fundController.LoadForCompany(GlobalRegistry.CompanyCode);
			}
			else
			{
				result = m_fundController.LoadForFundGroup(this.FundGroupFilter.Key);
			}

			T.X();
			return CurrentFundStatusFundDecorator.ToDecoratedList( result);
		}

		/// <summary>
		///	Loads the fund group (if a fund group has been selected)
		/// </summary>
		/// <returns>fund group corresponding to the group selected in drop down or null if no group selected</returns>
		public FundGroup RetrieveFundGroup ()
		{
			if (this.FundGroupFilter == null || this.FundGroupFilter.Key == 0)
			{
				//should never happen, we need the fund group to be selected before we can load.
				//however there may be no fund groups set up, so return null and allow client to deal with it
				m_currentFundGroup = null;
			}
			else
			{
				if (m_fundGroupController == null)
				{
					m_fundGroupController = new FundGroupController(GlobalRegistry.ConnectionString);
				}
				if (m_currentFundGroup == null || m_currentFundGroup.ID != this.FundGroupFilter.Key)
				{
					m_currentFundGroup = m_fundGroupController.LoadFundGroupStaticData(FundGroupFilter.Key);
				}
			}
			return m_currentFundGroup;
		}


	}
}