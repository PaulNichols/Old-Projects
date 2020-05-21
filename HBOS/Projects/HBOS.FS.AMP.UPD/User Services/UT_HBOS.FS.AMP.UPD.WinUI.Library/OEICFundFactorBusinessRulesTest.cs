using System;

using NUnit.Framework;
using HBOS.FS.AMP.UPD.WinUI.Classes;
using HBOS.FS.AMP.UPD.Types.Funds;

namespace UT_HBOS.FS.AMP.UPD.WinUI.Library
{
	/// <summary>
	/// Unit tests for the fund factor business rules class
	/// </summary>
	[TestFixture]
    public class OEICFundFactorBusinessRulesTest
	{
		private OEICFund m_fund;

        /// <summary>
        /// Creates a new <see cref="OEICFundFactorBusinessRulesTest"/> instance.
        /// </summary>
        public OEICFundFactorBusinessRulesTest()
		{}

        /// <summary>
        /// Initialises the OEIC fund
        /// </summary>
        [SetUp]
        public void InitialiseFund()
        {
            m_fund = new OEICFund();
        }
        
        /// <summary>
        /// Tests the OEIC XFactor
        /// </summary>
        [Test]
        public void OEICXFactorTest()
        {
            // Should always be true
            Assert.IsTrue(FundFactorBusinessRules.CanSetXFactor(m_fund));
        }

        /// <summary>
        /// Tests the OEIC TPE
        /// </summary>
        [Test]
        public void OEICTaxProvisionEstimateTest()
        {
            // Should always be false
            Assert.IsFalse(FundFactorBusinessRules.CanSetTaxProvisionEstimate(m_fund));
        }

        /// <summary>
        /// OEIC Revaluation factor test
        /// </summary>
        [Test]
        public void OEICRevaluationFactorTest()
        {
            // Should always be false
            Assert.IsFalse(FundFactorBusinessRules.CanSetRevaluationFactor(m_fund));
        }

        /// <summary>
        ///  OEIC Scaling factor test
        /// </summary>
        [Test]
        public void OEICScalingFactorTest()
        {
            // Should always be false
            Assert.IsFalse(FundFactorBusinessRules.CanSetScalingFactor(m_fund));
        }
	}
}
