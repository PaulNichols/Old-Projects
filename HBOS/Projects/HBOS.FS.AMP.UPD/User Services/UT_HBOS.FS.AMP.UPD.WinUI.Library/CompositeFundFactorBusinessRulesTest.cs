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
    public class CompositeFactorBusinessRulesTest
    {
        private Composite m_fund;

        /// <summary>
        /// Creates a new <see cref="CompositeFactorBusinessRulesTest"/> instance.
        /// </summary>
        public CompositeFactorBusinessRulesTest()
        {}

        /// <summary>
        /// Initialises the Composite fund
        /// </summary>
        [SetUp]
        public void InitialiseFund()
        {
            m_fund = new Composite();
        }
        
        /// <summary>
        /// Tests the Composite fund XFactor
        /// </summary>
        [Test]
        public void CompositeXFactorTest()
        {
            // Should always be true
            Assert.IsTrue(FundFactorBusinessRules.CanSetXFactor(m_fund));
        }

        /// <summary>
        /// Tests the Composite fund TPE
        /// </summary>
        [Test]
        public void CompositeTaxProvisionEstimateTest()
        {
            // Should always be false for non-life funds
            Assert.IsFalse(FundFactorBusinessRules.CanSetTaxProvisionEstimate(m_fund));

            m_fund.IsLife = true;
            // Should now be true
            Assert.IsTrue(FundFactorBusinessRules.CanSetTaxProvisionEstimate(m_fund));
        }

        /// <summary>
        /// Tests the Composite fund Revaluation factor
        /// </summary>
        [Test]
        public void CompositeRevaluationFactorTest()
        {
            // Should always be true
            Assert.IsTrue(FundFactorBusinessRules.CanSetRevaluationFactor(m_fund));
        }

        /// <summary>
        /// Tests the Composite fund scaling factor
        /// </summary>
        [Test]
        public void CompositeScalingFactorTest()
        {
            // Should always be true
            Assert.IsTrue(FundFactorBusinessRules.CanSetScalingFactor(m_fund));
        }
    }
}
