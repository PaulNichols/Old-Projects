using System;

using HBOS.FS.AMP.UPD.Types.Funds;

namespace HBOS.FS.AMP.UPD.DataAccess
{
    /// <summary>
    /// Temporary class to generate dummy data
    /// </summary>
    public class TemporaryFundDatabase
    {
        FundCollection m_TemporaryFunds;
        private static TemporaryFundDatabase m_Instance;

        /// <summary>
        /// Allows internal calling whilst not exposing to the client
        /// </summary>
        private TemporaryFundDatabase()
        {
            m_TemporaryFunds = new FundCollection();
            Fund currentFund;
			string fundCode;

            for (int i=1; i<=20; i++)
            {
                fundCode = i.ToString("00");
				currentFund = FundFactory.CreateFund("OEIC");
                currentFund.FullName = "Temp Fund " + fundCode;
				currentFund.HiPortfolioCode = "HL0" + fundCode;
//				currentFund.IsinCode= "ISIN" + fundCode;
//				currentFund.MexxCode = "MEX" + fundCode;
//				currentFund.PolicyAdministrationCode = "CPAS" + fundCode;
//				currentFund.SedolNumber = i;
				currentFund.ShortName = "Fund" + fundCode;
                
				switch (i)
                {
                    case 1:
                        currentFund.FundStatus = Fund.FundStatusType.SecondLevelAuthorised;
                        break;
                    case 2:
                        currentFund.FundStatus = Fund.FundStatusType.Released;
                        break;
                    case 3:
                        currentFund.FundStatus = Fund.FundStatusType.Imported;
                        break;
                    default:
                        currentFund.FundStatus = Fund.FundStatusType.FirstLevelAuthorised;
                        break;
                }
                
                 m_TemporaryFunds.Add(currentFund);
            }
        }

        /// <summary>
        /// Make sure only one instance of the object will be instatiated 
        /// </summary>
        /// <returns></returns>
        public static TemporaryFundDatabase GetInstance()
        {
            if (m_Instance == null)
            {
                m_Instance = new TemporaryFundDatabase();
            }
            return m_Instance;
        }

        /// <summary>
        /// Get a collection of funds
        /// </summary>
        /// <returns></returns>
        public FundCollection GetFunds()
        {
            return m_TemporaryFunds;
        }

        /// <summary>
        /// Persist the funds back to the DB
        /// </summary>
        /// <param name="funds"></param>
        public void SaveFunds(FundCollection funds)
        {
            m_TemporaryFunds = funds;
        }
    }
}
