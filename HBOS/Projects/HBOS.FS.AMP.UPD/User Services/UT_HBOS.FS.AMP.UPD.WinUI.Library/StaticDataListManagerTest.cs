using System;
using System.Collections;
using System.ComponentModel;

using NUnit.Framework;

using HBOS.FS.AMP.UPD.WinUI.Classes;

namespace UT_HBOS.FS.AMP.UPD.WinUI.Library
{
	/// <summary>
	/// Summary description for StaticDataListManagerTest.
	/// </summary>
	[TestFixture]
	public class StaticDataListManagerTest
	{
		/// <summary>
		/// Creates a new <see cref="StaticDataListManagerTest"/> instance.
		/// </summary>
		public StaticDataListManagerTest()
		{
		}

		private StaticDataListManager subject;

		/// <summary>
		/// NUnit Setup code
		/// </summary>
		[SetUp]
		public void SetUp()
		{
			subject = new StaticDataListManager();
			eventRaised = false;
		}
		
		/// <summary>
		/// Test that it holds a list.
		/// </summary>
		[Test]
		public void HoldsAList()
		{
			subject.RefreshContents(new object[0]);
			Assert.IsNotNull(subject.Items);
		}

		/// <summary>
		/// Test that it refreshes the contents of the list.
		/// </summary>
		[Test]
		public void RefreshTheContentsOfTheList()
		{
			object[] contents = new object[] {new object()};
			subject.RefreshContents(contents);
			Assert.AreSame(contents[0],subject[0]);
		}

		/// <summary>
		/// Test that an Event is raised when contents refreshed.
		/// </summary>
		[Test]
		public void EventRaisedWhenContentsRefreshed()
		{
			subject.ContentsRefreshed +=new EventHandler(subject_ContentsRefreshed);
			subject.RefreshContents(new object[0]);
			Assert.IsTrue(eventRaised);
		}

		private bool eventRaised;

		private void subject_ContentsRefreshed(object sender, EventArgs e)
		{
			eventRaised = true;
		}

		/// <summary>
		/// Test that it adds a new blank item to list.
		/// </summary>
		[Test]
		public void AddNewBlankItemToList()
		{
			subject.RefreshContents(new ArrayList());
			subject.AddNew();
			Assert.AreEqual(1,subject.Items.Count);
		}


		/// <summary>
		/// Test that determines the if selected item is new.
		/// </summary>
		[Test]
		public void DetermineIfSelectedItemIsNew()
		{
			subject.RefreshContents(new ArrayList());
			subject.AddNew();
			Assert.IsTrue(subject.SelectedIsNew);
		}


		/// <summary>
		/// Ensure the index negative at start.
		/// </summary>
		[Test]
		public void SelectedIndexNegativeAtStart()
		{
			Assert.IsTrue(subject.SelectedIndex < 0);
		}

		/// <summary>
		/// Test that Added item becomes selected index.
		/// </summary>
		[Test]
		public void AddedItemBecomesSelectedIndex()
		{
			subject.RefreshContents(new ArrayList());
			subject.AddNew();
			subject.AddNew();
			subject.AddNew();
			Assert.AreEqual(2,subject.SelectedIndex);
		}

		/// <summary>
		/// Test the change of selected index.
		/// </summary>
		[Test]
		public void ChangeSelectedIndex()
		{
			subject.RefreshContents(new ArrayList());
			subject.AddNew();
			subject.AddNew();
			subject.AddNew();
			subject.AddNew();
			subject.AddNew();
			subject.SelectedIndex = 3;
			Assert.AreEqual(3,subject.SelectedIndex);
		}

		/// <summary>
		/// Check that exceptions the when selected index over range.
		/// </summary>
		[Test,ExpectedException(typeof(IndexOutOfRangeException))]
		public void ExceptionWhenSelectedIndexOverRange()
		{
			subject.RefreshContents(new ArrayList());
			subject.AddNew();
			subject.AddNew();
			subject.AddNew();
			subject.AddNew();
			subject.AddNew();
			int expectedSelectedIndex = subject.SelectedIndex;
			subject.SelectedIndex = 5;
			Assert.AreEqual(expectedSelectedIndex,subject.SelectedIndex,"Index changed when out of range");
		}

		/// <summary>
		/// Test that SelectedItem returns the selected object.
		/// </summary>
		/// <value></value>
		[Test]
		public void ReturnsTheSelectedObject()
		{
			int[] contents = new int[] {1,2,3,4,5,6};
			subject.RefreshContents(contents);
			subject.SelectedIndex = 3;
			Assert.AreEqual(4,subject.SelectedItem);
		}

		/// <summary>
		/// Test that changing the selected item changes selected index.
		/// </summary>
		[Test]
		public void ChangingSelectedItemChangesSelectedIndex()
		{
			int[] contents = new int[] {1,2,3,4,5,6};
			subject.RefreshContents(contents);
			subject.SelectedItem = 5;
			Assert.AreEqual(4,subject.SelectedIndex);
		}

		/// <summary>
		/// Test that changing the selected index raises event.
		/// </summary>
		[Test]
		public void ChangedSelectedIndexRaisesEvent()
		{
			int[] contents = new int[] {1,2,3,4,5,6};
			subject.RefreshContents(contents);
			subject.SelectedIndexChanged +=new EventHandler(subject_SelectedIndexChanged);
			subject.SelectedIndex = 2;
			Assert.IsTrue(eventRaised);
		}

		private void subject_SelectedIndexChanged(object sender, EventArgs e)
		{
			eventRaised = true;
		}
		/// <summary>
		/// Test that changing the selected index raises event.
		/// </summary>
		[Test]
		public void ChangingSelectedIndexRaisesEvent()
		{
			int[] contents = new int[] {1,2,3,4,5,6};
			subject.RefreshContents(contents);
			subject.SelectedIndexChanging +=new CancelEventHandler(subject_SelectedIndexChanging);
			subject.SelectedIndex = 2;
			Assert.IsTrue(eventRaised);
		}

		private void subject_SelectedIndexChanging(object sender, CancelEventArgs e)
		{
			eventRaised = true;
		}

		/// <summary>
		/// Determines whether this instance [can cancel change through event args].
		/// </summary>
		[Test]
		public void CanCancelChangeThroughEventArgs()
		{
			int[] contents = new int[] {1,2,3,4,5,6};
			subject.RefreshContents(contents);
			subject.SelectedIndex = 2;
			subject.SelectedIndexChanging +=new CancelEventHandler(subject_SelectedIndexCancelChanging);
			subject.SelectedIndex = 5;
			Assert.AreEqual(2,subject.SelectedIndex);
		}

		private void subject_SelectedIndexCancelChanging(object sender, CancelEventArgs e)
		{
			e.Cancel = true;
		}

		/// <summary>
		/// Determines whether this instance [can cancel change through event args].
		/// </summary>
		[Test]
		public void CanCancelNewThroughEventArgs()
		{
			ArrayList contents = new ArrayList(new  int[] {1,2,3,4,5,6});
			subject.RefreshContents(contents);
			subject.SelectedIndex = 2;
			subject.SelectedIndexChanging +=new CancelEventHandler(subject_SelectedIndexCancelChanging);
			subject.AddNew();
			Assert.AreEqual(2,subject.SelectedIndex,"Selected index change");
			Assert.AreEqual(6,subject.Items.Count,"Move cancelled, but list still has new item in");
		}

		/// <summary>
		/// Deletes the selected item.
		/// </summary>
		[Test]
		public void DeleteTheSelectedItem()
		{
			ArrayList contents = new ArrayList(new  int[] {1,2,3,4,5,6});
			subject.RefreshContents(contents);
			subject.SelectedIndex = 2;
			subject.DeleteSelected();
			Assert.AreEqual(5,subject.Items.Count);
			Assert.AreEqual(4,subject[2]);
		}

		/// <summary>
		/// Changes the selected item.
		/// </summary>
		[Test]
		public void ChangeTheSelectedItem()
		{
			ArrayList contents = new ArrayList(new  int[] {1,2,3,4,5,6});
			subject.RefreshContents(contents);
			subject.SelectedIndex = 2;
			subject.ChangeSelected(999);
			Assert.AreEqual(999,subject.SelectedItem);
		}
	}
}
