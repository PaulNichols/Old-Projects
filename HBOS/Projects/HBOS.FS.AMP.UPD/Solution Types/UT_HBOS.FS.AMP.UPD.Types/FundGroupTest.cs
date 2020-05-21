using System;
using NUnit.Framework;
using HBOS.FS.AMP.UPD.Types.FundGroups;

namespace UT_HBOS.FS.AMP.UPD.Types
{
	/// <summary>
	/// Unit Testing for FundGroups
	/// </summary>
	
	[TestFixture]
	public class FundGroupTest
	{
		#region Constructor
		
		public FundGroupTest()
		{
		} 

		#endregion Constructor

		#region AssetFundGroupSetUpTest
		[Test]
		public void AssetFundGroupSetUpTest()
		{
			string fundGroupName = "TestFundGroup";
            
			//Create specific FundGroup using factory
			FundGroup fg = FundGroupFactory.CreateFundGroup(FundGroupFactory.FundGroupTypes.Asset);
            
			//Check IsDirty flag defaults to false
			Assert.IsFalse(fg.IsDirty);

			//Set FundGroupName
			fg.FullName = fundGroupName;

			//Check our FundGroup has been set up OK and Dirty flag changed
			Assert.AreEqual(fg.FullName, fundGroupName);
			Assert.IsTrue(fg.IsDirty);
			
		}
		
		#endregion AssetFundGroupSetUpTest

		#region IndividualFundGroupSetUpTest
		[Test]
		public void IndividualFundGroupSetUpTest()
		{
			string fundGroupName = "TestFundGroup";
            
			//Create specific FundGroup using factory
			FundGroup fg = FundGroupFactory.CreateFundGroup(FundGroupFactory.FundGroupTypes.Individual);
            
			//Check IsDirty flag defaults to false
			Assert.IsFalse(fg.IsDirty);

			//Set FundGroupName
			fg.FullName = fundGroupName;

			//Check our FundGroup has been set up OK and Dirty flag changed
			Assert.AreEqual(fg.FullName, fundGroupName);
			Assert.IsTrue(fg.IsDirty);
			
		}
		#endregion IndividualFundGroupSetUpTest
	}
}
