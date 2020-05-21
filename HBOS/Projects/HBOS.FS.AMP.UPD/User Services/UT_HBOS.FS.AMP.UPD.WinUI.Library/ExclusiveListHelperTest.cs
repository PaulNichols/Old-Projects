using System;
using NUnit.Framework;
using HBOS.FS.AMP.UPD.WinUI.Helpers;
using System.Collections;

namespace UT_HBOS.FS.AMP.UPD.WinUI.Library
{
	/// <summary>
	/// Unit Test for ExclusiveListHelper class
	/// </summary>
	[TestFixture]
	public class ExclusiveListHelperTest
	{
		/// <summary>
		/// Creates a new <see cref="ExclusiveListHelperTest"/> instance.
		/// </summary>
		public ExclusiveListHelperTest()
		{
		}

		private ExclusiveListHelper subject;

		private int[] completeList      = new int[] {1,2,3,4,5,6,7,8,9,10};
		private int[] expectedIncluded  = new int[] {2,4,6,8,10};
		private int[] expectedExcluded  = new int[] {1,3,5,7,9};
		private int[] movedFromExcluded = new int[] {1,7};
		private int[] expectedIncludedAfterMove = new int[] {2,4,6,8,10,1,7};
		private int[] expectedExcludedAfterMove = new int[] {3,5,9};


		/// <summary>
		/// Set up routine for NUnit
		/// </summary>
		[SetUp]
		public void SetUp()
		{
			subject = new ExclusiveListHelper(completeList);
		}

		/// <summary>
		/// Test that items in the included list are in the included list
		/// </summary>
		[Test]
		public void IncludedItemsAreIncluded()
		{
			subject.Include(expectedIncluded);
			AssertIList(expectedIncluded,subject.Included);
		}

		/// <summary>
		/// Test that items in the included list are not in the excluded list
		/// </summary>
		[Test]
		public void ItemsNotIncludedAreExcluded()
		{
			subject.Include(expectedIncluded);
			AssertIList(expectedExcluded,subject.Excluded);			
		}

		/// <summary>
		/// Test moving the item from excluded to included.
		/// </summary>
		[Test]
		public void ItemsMovedFromExcludedAreIncluded()
		{
			subject.Include(expectedIncluded);
			subject.Include(movedFromExcluded);
			AssertIList(expectedIncludedAfterMove,subject.Included);
		}

		/// <summary>
		/// Test moving the item from excluded to included.
		/// </summary>
		[Test]
		public void ItemsNotMovedFromExcludedAreStilExcluded()
		{
			subject.Include(expectedIncluded);
			subject.Include(movedFromExcluded);
			AssertIList(expectedExcludedAfterMove,subject.Excluded);
		}

		/// <summary>
		/// Test that items in the excluded list are in the excluded list
		/// </summary>
		[Test]
		public void ExcludedItemsAreExcluded()
		{
			subject.Include(completeList);
			subject.Exclude(expectedExcluded);
			AssertIList(expectedExcluded,subject.Excluded);
		}

		/// <summary>
		/// Test that items in the excluded list are not in the included list
		/// </summary>
		[Test]
		public void ItemsNotExcludedAreIncluded()
		{
			subject.Include(completeList);
			subject.Exclude(expectedExcluded);
			AssertIList(expectedIncluded,subject.Included);			
		}

		/// <summary>
		/// Test that Excluded is empty if all items moved to Included.
		/// </summary>
		[Test]
		public void ExcludedIsEmptyIfAllMovedToIncluded()
		{
			subject.Include(completeList);
			Assert.AreEqual(0,subject.Excluded.Count);
		}

		/// <summary>
		/// Test that Included is empty if all items moved to Excluded.
		/// </summary>
		[Test]
		public void IncludedIsEmptyIfAllMovedToExcluded()
		{
			subject.Include(completeList);
			subject.Exclude(completeList);
			Assert.AreEqual(0,subject.Included.Count);
		}

		/// <summary>
		/// Exception raised when try to include an item not in the original list.
		/// </summary>
		[Test,ExpectedException(typeof(IndexOutOfRangeException))]
		public void CannotIncludeNonExistantItem()
		{
			subject.Include(new string[] {"1","2"});
		}

		/// <summary>
		/// Test that items cannot be included twice.
		/// </summary>
		[Test]
		public void ItemCannotBeIncludedTwice()
		{
			subject.Include(1);
			subject.Include(1);
			AssertIList(new int[] {1},subject.Included);
		}

		private void AssertIList(IList expected,IList actual)
		{
			Assert.AreEqual(expected.Count, actual.Count, "IList.Count is not equal");

			foreach(object exp in expected)
			{
				Assert.IsTrue(actual.Contains(exp),"Expected Value {0} is not in actual list",exp);
			}

			foreach(object act in actual)
			{
				Assert.IsTrue(expected.Contains(act),"Value {0} is not expected",act);
			}
		}
	
	}
}
