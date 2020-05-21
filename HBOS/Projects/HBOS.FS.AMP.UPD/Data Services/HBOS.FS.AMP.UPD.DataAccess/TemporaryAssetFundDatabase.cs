using System;

using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.Funds;
namespace HBOS.FS.AMP.UPD.DataAccess
{
	/// <summary>
	/// Summary description for TemporaryAssetFundDatabase.
	/// </summary>
	public class TemporaryAssetFundDatabase
	{
        AssetFundCollection m_TemporaryAssetFunds;
        private static TemporaryAssetFundDatabase m_Instance;

        /// <summary>
        /// Allows internal calling whilst not exposing to the client
        /// </summary>
        private TemporaryAssetFundDatabase()
        {
            m_TemporaryAssetFunds = new AssetFundCollection();
            AssetFund currentAssetFund;

            for (int i=0; i<16; i++)
            {
                currentAssetFund = AssetFundFactory.CreateAssetFund();
                currentAssetFund.FullName = "Temp AssetFund " + i;
                //currentAssetFund.AssetFundCode = "A" + i;

                m_TemporaryAssetFunds.Add(currentAssetFund);

//                Fund currentFund;
//
//                for (int j=0; j<16; j++)
//                {
//                    currentFund = FundFactory.CreateFund("OEIC");
//                    currentFund.FullName = "Temp Fund " + j.ToString();
//
//                    currentAssetFund.Funds.Add (currentFund);
//                }
            }
        }

        /// <summary>
        /// Make sure only one instance of the object will be instatiated 
        /// </summary>
        /// <returns></returns>
        public static TemporaryAssetFundDatabase GetInstance()
        {
            if (m_Instance == null)
            {
                m_Instance = new TemporaryAssetFundDatabase();
            }
            return m_Instance;
        }

        /// <summary>
        /// Get a collection of Asset funds
        /// </summary>
        /// <returns></returns>
        public AssetFundCollection GetAssetFunds()
        {
            return m_TemporaryAssetFunds;
        }

        /// <summary>
        /// Persist the asset funds back to the DB
        /// </summary>
        /// <param name="funds"></param>
        public void SaveAssetFunds(AssetFundCollection assetFunds)
        {
            m_TemporaryAssetFunds = assetFunds;
        }

	}
}
