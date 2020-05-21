using System;
using System.Collections;
using HBOS.FS.AMP.UPD.WinUI.Classes;
using NUnit.Framework;

namespace UT_HBOS.FS.AMP.UPD.WinUI.Library
{
	/// <summary>
	/// Unit tests for the StatusPropertyFilter class
	/// </summary>
	[TestFixture]
	public class StatusPropertyFilterTest
	{
		/// <summary>
		/// Creates a new <see cref="StatusPropertyFilterTest"/> instance.
		/// </summary>
		public StatusPropertyFilterTest()
		{
		}

		/// <summary>
		/// NUnit setup code
		/// </summary>
		[SetUp]
		public void Setup()
		{
			subject =new PropertyFilter(new PropertyFilter.NameValuePair[]{new PropertyFilter.NameValuePair("Colour","red")},true,"Colour is red");
		}

		private PropertyFilter subject;

		private bool eventRaised;
		
		/// <summary>
		/// Test that Changings the Applied property raises an event.
		/// </summary>
		[Test]
		public void ChangingAppliedRaisesEvent()
		{
			subject.AppliedChanged +=new EventHandler(subject_AppliedChanged);
			eventRaised = false;
			subject.Applied = true;
			Assert.IsTrue(eventRaised);
		}

		private void subject_AppliedChanged(object sender, EventArgs e)
		{
			eventRaised = true;
		}
		
		private class CarFixture
		{
			public CarFixture(string colour)
			{
				this.colour = colour;
			}
			
			private string colour;
	
			public string Colour
			{
				get {return colour;}
			}
		}

		/// <summary>
		/// Test that Applying the filter removes items.
		/// </summary>
		[Test]
		public void ApplyFilterRemovesItems()
		{
			CarFixture car1 = new CarFixture("blue");
			CarFixture car2 = new CarFixture("red");
			CarFixture car3 = new CarFixture("red");
			ArrayList cars = new ArrayList();
			cars.AddRange(new CarFixture[] {car1,car2,car3});

			//subject.Apply(cars);
			Assert.AreEqual(2,cars.Count,"Incorrect count");
		}


	}
}
