using System;
using HBOS.FS.AMP.UPD.Types.FundGroups;

namespace HBOS.FS.AMP.UPD.DataAccess
{
	/// <summary>
	/// Data Access Layer for FundGroup information
	/// </summary>
	public class DALFundGroup
	{
		#region Constructors

		public DALFundGroup()
		{}

		#endregion Constructors

		#region Methods

		/// <summary>
		/// Gets a collection of fundgroups filtered by the specified company id
		/// </summary>
		/// <param name="companyID"></param>
		/// <returns></returns>
		public static FundGroupCollection GetFundGroups(int companyID)
		{
			// Get data from our dummy database object until the SQL DB is built
			return TempDBFundGroupCollection.GetInstance().GetFundGroupCollection();
		}

		/// <summary>
		/// Persist data back to the DB through the fundgroup collection
		/// </summary>
		/// <param name="fundgroups"></param>
		public static void SaveFundGroups(FundGroupCollection fundGroups)
		{
			// Send collection back to temp DB object
			TempDBFundGroupCollection.GetInstance().SaveFundGroupCollection(fundGroups);
		}

		#endregion Methods
	}
}
