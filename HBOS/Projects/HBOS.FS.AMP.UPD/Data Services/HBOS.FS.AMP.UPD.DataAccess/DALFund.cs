using System;

using HBOS.FS.AMP.UPD.Types.Funds;

namespace HBOS.FS.AMP.UPD.DataAccess
{
	/// <summary>
	/// Data Access Layer for fund information
	/// </summary>
	public class DALFund
	{
		/// <summary>
		/// Main constructor
		/// </summary>
        public DALFund()
		{
			//
			// TODO: Add constructor logic here
			//
		}

        /// <summary>
        /// Gets a collection of funds filtered by the specified compny id
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        public FundCollection GetFunds(int companyID)
        {
            // Get data from our dummy database object until the SQL DB is built
            return TemporaryFundDatabase.GetInstance().GetFunds();
        }

        /// <summary>
        /// Gets a collection of funds filtered by the specified company id and asset fund name
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="assetFundName"></param>
        /// <returns></returns>
        public FundCollection GetFunds(int companyID, string assetFundName)
        {
            // Get data from our dummy database object until the SQL DB is built
            return TemporaryFundDatabase.GetInstance().GetFunds();
        }

        /// <summary>
        /// Persist data back to the DB through the fund collection
        /// </summary>
        /// <param name="funds"></param>
        public void SaveFunds(FundCollection funds)
        {
            // Send collection back to temp DB object
            TemporaryFundDatabase.GetInstance().SaveFunds(funds);
        }
	}
}
