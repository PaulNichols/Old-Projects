using System;
using System.Collections;

using NUnit.Framework;
using HBOS.FS.AMP.UPD.WinUI.Classes;
using HBOS.FS.AMP.UPD.Types.Lookups;


namespace UT_HBOS.FS.AMP.UPD.WinUI.Library
{
	/// <summary>
	/// Unit test for StatusDataManager class.
	/// </summary>
	[TestFixture]
	public class StatusDataManagerTest
	{

		/// <summary>
		/// StatusDataManager is abstract so we need a mock version
		/// </summary>
		private class MockStatusDataManager: StatusDataManager
		{
			public IList Data = new object[0];
			
			public MockStatusDataManager(): base()
			{
			}

			protected override System.Collections.IList loadDataFromSource()
			{
				return Data;
			}

		}
									   


		/// <summary>
		/// Creates a new <see cref="StatusDataManagerTest"/> instance.
		/// </summary>
		public StatusDataManagerTest()
		{
		}

		private MockStatusDataManager subject;

		/// <summary>
		/// Sets the test up.
		/// </summary>
		[SetUp]
		public void SetUp()
		{
			subject = new MockStatusDataManager();
		}

		/// <summary>
		/// Test that it contains a fund group filter.
		/// </summary>
		[Test]
		public void ContainsAFundGroupFilter()
		{
			SimpleLookup lkup = new SimpleLookup(1,"fred");
			subject.FundGroupFilter = lkup;
			Assert.AreSame(lkup,subject.FundGroupFilter);
		}

		/// <summary>
		/// Test that Changing the fund group filter raises an event.
		/// </summary>
		[Test]
		public void ChangingFundGroupFilterRaisesEvent()
		{
			dataChangedEventRaised = false;
			subject.FiltersChanged	+=new EventHandler(subject_FiltersChanged);
			subject.FundGroupFilter = new SimpleLookup(1,"fred");
			Assert.IsTrue(dataChangedEventRaised);
		}

		private bool dataChangedEventRaised = false;

		private void subject_FiltersChanged(object sender, EventArgs e)
		{
			dataChangedEventRaised = true;
		}

		/// <summary>
		/// Builds an IList of the data.
		/// </summary>
		/// <value></value>
		[Test]
		public void BuildsAnIListOfTheData()
		{
			Assert.IsNotNull(subject.GetData(),"Return null");
			Assert.AreSame(subject.Data,subject.GetData(),"Unexpected value");
		}

		/// <summary>
		/// Tests that data is not loaded when the fund group doesn't change.
		/// </summary>
		[Test]
		public void DataNotLoadedWhenFundGroupNotChanged()
		{
			IList original = subject.GetData(); // gets data
			
			//change Data in mock but fund group not changed so GetData should return original value
			subject.Data = new object[0];       		
			Assert.AreSame(original,subject.GetData());
		}

		/// <summary>
		/// Tests that data is loaded when the fund group changes.
		/// </summary>
		[Test]
		public void DataLoadedWhenFundGroupChanged()
		{
			IList original = subject.GetData(); // gets data
			
			//change Data in mock but fund group not changed so GetData should return original value
			IList replacement = new object[0]; 
			subject.Data = replacement;
			subject.FundGroupFilter = new SimpleLookup(1,"fred");
			Assert.AreSame(replacement,subject.GetData());
		}

	}
}
