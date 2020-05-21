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
    public class LinkedFundFactorBusinessRulesTest
    {
        private LinkedFund m_fund;

        /// <summary>
        /// Creates a new <see cref="LinkedFundFactorBusinessRulesTest"/> instance.
        /// </summary>
        public LinkedFundFactorBusinessRulesTest()
        {}

        /// <summary>
        /// Initialises the Linked fund
        /// </summary>
        [SetUp]
        public void InitialiseFund()
        {
            m_fund = new LinkedFund();
        }
        
        /// <summary>
        /// Tests the linked fund XFactor
        /// </summary>
        [Test]
        public void LinkedXFactorTest()
        {
            // Should always be true
            Assert.IsTrue(FundFactorBusinessRules.CanSetXFactor(m_fund));
        }

        /// <summary>
        /// Tests the linked fund TPE
        /// </summary>
        [Test]
        public void LinkedTaxProvisionEstimateTest()
        {
            // Should always be false for non-life funds
            Assert.IsFalse(FundFactorBusinessRules.CanSetTaxProvisionEstimate(m_fund));

            m_fund.IsLife = true;
            // Should now be true
            Assert.IsTrue(FundFactorBusinessRules.CanSetTaxProvisionEstimate(m_fund));
        }

        /// <summary>
        /// Tests the linked fund Revaluation factor
        /// </summary>
        [Test]
        public void LinkedRevaluationFactorTest()
        {
            // Should always be true
            Assert.IsTrue(FundFactorBusinessRules.CanSetRevaluationFactor(m_fund));
        }

        /// <summary>
        /// Tests the linked fund scaling factor
        /// </summary>
        [Test]
        public void LinkedScalingFactorTest()
        {
            // Should always be true
            Assert.IsTrue(FundFactorBusinessRules.CanSetScalingFactor(m_fund));
        }
    }
}
