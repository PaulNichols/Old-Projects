using System;

using NUnit.Framework;
using HBOS.FS.AMP.UPD.Types.Funds;

namespace UT_HBOS.FS.AMP.UPD.Funds
{
	/// <summary>
	/// Summary description for FundTest.
	/// </summary>
    [TestFixture()]
    public class FundTest
	{
		public FundTest()
		{
			//
			// TODO: Add constructor logic here
			//
		}

        public void FundPropertyTest()
        {
            string fullName = "TestFund";
            
            Fund f = new Fund();
            
            f.FullName = fullName;
            Assert.AreEqual(fullName, f.FullName);
        }
	}
}
