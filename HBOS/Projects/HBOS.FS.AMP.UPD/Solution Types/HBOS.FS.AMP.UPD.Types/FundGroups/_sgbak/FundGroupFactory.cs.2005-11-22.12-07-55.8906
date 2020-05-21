using System;

namespace HBOS.FS.AMP.UPD.Types.FundGroups
{
	/// <summary>
	/// Create a FundGroup based on the specified FundGroup type
	/// </summary>
	public class FundGroupFactory
	{        
		#region Enumeration
		
		/// <summary>
		/// Specifies the different types of fund groups in the system
		/// </summary>
		public enum FundGroupTypes : int
		{
			/// <summary>
			/// A fund group for holding <see cref="HBOS.FS.AMP.UPD.Types.AssetFunds.AssetFund">AssetFunds</see>
			/// </summary>
			Asset = 0,

			/// <summary>
			/// A fund group for holding <see cref="HBOS.FS.AMP.UPD.Types.Funds.Fund">Funds</see>
			/// </summary>
			Individual = 1
		}

		#endregion
			
		#region Methods

		/// <summary>
		/// Create a single FundGroup type
		/// </summary>
		/// <param name="fundGroupType"></param>
		/// <returns>FundGroup object (default is an Individual FundGroup</returns>
		public static FundGroup CreateFundGroup(FundGroupTypes fundGroupType)
		{
			//
			// Pass specified FundGroup back to client
			//
			switch (fundGroupType)
			{
				case FundGroupTypes.Asset:
					return new AssetFundGroup();
				case FundGroupTypes.Individual:
					return new IndividualFundGroup();
				default:
					throw new ArgumentOutOfRangeException("Fund Group Type", "Unknown Fund Group Type");
			}
		}

		/// <summary>
		/// Creates a re-hydrated fund group.
		/// </summary>
		/// <param name="ID">ID.</param>
		/// <param name="companyCode">Company code.</param>
		/// <param name="fundGroupType">Fund group type.</param>
		/// <param name="fullName">Full name.</param>
		/// <param name="shortName">Name of the short.</param>
		/// <param name="timestamp">Time stamp.</param>
		/// <param name="forRelease">For release.</param>
		/// <param name="hasAssociatedFunds">Has associated funds.</param>
		/// <param name="allowSelectAllAuthorisation">Has associated funds.</param>
		/// <returns></returns>
		public static FundGroup CreateFundGroup(int ID, string companyCode, FundGroupTypes fundGroupType, 
			string fullName, string shortName, byte[] timestamp, bool forRelease, bool hasAssociatedFunds, bool allowSelectAllAuthorisation)
		{
			//
			// Pass specified FundGroup back to client
			//
			switch (fundGroupType)
			{
				case FundGroupTypes.Asset:
					return new AssetFundGroup(ID, companyCode, fullName, shortName, timestamp, forRelease, hasAssociatedFunds, allowSelectAllAuthorisation);
				case FundGroupTypes.Individual:
					return new IndividualFundGroup(ID, companyCode, fullName, shortName, timestamp, forRelease, hasAssociatedFunds, allowSelectAllAuthorisation);
				default:
					throw new ArgumentOutOfRangeException("Fund Group Type", "Unknown Fund Group Type");
			}
		}

		#endregion Methods
	}
}
