using System;

namespace HBOS.FS.AMP.UPD.Types.FundGroups
{
	/// <summary>
	/// Summary description for AssetFundGroup.
	/// </summary>
	[Serializable]
    public class AssetFundGroup : FundGroup
	{
		#region Constructors

		/// <summary>
		/// Creates a new <see cref="AssetFundGroup"/> instance.
		/// </summary>
		public AssetFundGroup(): base()
		{
		}

		
		/// <summary>
		/// Creates a new <see cref="AssetFundGroup"/> instance.
		/// </summary>
		/// <param name="ID">ID.</param>
		/// <param name="companyCode">Company code.</param>
		/// <param name="fullName">Full name.</param>
		/// <param name="shortName">Short name.</param>
		/// <param name="timeStamp">Time stamp.</param>
		/// <param name="forRelease">For release.</param>
		/// <param name="hasAssociatedFunds">Has associated funds.</param>
		/// <param name="allowSelectAllAuthorisation"></param>
		public AssetFundGroup(int ID, string companyCode, string fullName, string shortName, byte[] timeStamp, bool forRelease, bool hasAssociatedFunds, bool allowSelectAllAuthorisation) 
			: base(ID, companyCode, fullName, shortName, timeStamp, forRelease, hasAssociatedFunds, allowSelectAllAuthorisation)
		{
		}

		#endregion Constructors

	}
}
