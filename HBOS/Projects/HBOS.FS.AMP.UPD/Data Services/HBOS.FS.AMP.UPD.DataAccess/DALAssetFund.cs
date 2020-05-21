using System;

using HBOS.FS.AMP.UPD.Types.AssetFunds;

namespace HBOS.FS.AMP.UPD.DataAccess
{
	/// <summary>
	/// Summary description for DALAssetFund.
	/// </summary>
    public class DALAssetFund
    {
        /// <summary>
        /// Main constructor
        /// </summary>
        public DALAssetFund()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Gets a collection of asset funds filtered by the specified compny id
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        public AssetFundCollection GetAssetFunds(int companyID)
        {
            // Get data from our dummy database object until the SQL DB is built
            return TemporaryAssetFundDatabase.GetInstance().GetAssetFunds();
        }

        /// <summary>
        /// Persist data back to the DB through the asset fund collection
        /// </summary>
        /// <param name="assetfunds"></param>
        public void SaveAssetFunds(AssetFundCollection assetFunds)
        {
            // Send collection back to temp DB object
            TemporaryAssetFundDatabase.GetInstance().SaveAssetFunds(assetFunds);
        }
    }
}
