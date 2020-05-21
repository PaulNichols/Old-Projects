using System;
using NUnit.Framework;
using uk.gov.defra.Phoenix.BO.Application;
using uk.gov.defra.Phoenix.BO;
using uk.gov.defra.Phoenix.BO.Application.Search;

namespace uk.gov.defra.Phoenix.BOTest.Application
{
	/// <summary>
	/// Summary description for CITESBo.
	/// </summary>
	[TestFixture]
	public class CITESBo
	{
		/// <summary>
		/// Summary description for CITESBo.
		/// </summary>
		public CITESBo()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		private uk.gov.defra.Phoenix.BO.Application.BOApplication App;

		/// <summary>
		/// Summary description for CITESBo.
		/// </summary>
		[TestFixtureSetUp]
		public void Setup()
		{
			//Add an App to search on
			App=new BOApplication();
			App=(BOApplication)App.Save(true);
			Assert.IsNotNull(App,"The Setup Application is Null");
		}

		/// <summary>
		/// Summary description for CITESBo.
		/// </summary>
		[TestFixtureTearDown]
		public void TearDown()
		{
			//Delete the App

		}

		/// <summary>
		/// Summary description for CITESBo.
		/// </summary>
		[Test()]
		public void TestSearch()
		{
			ApplicationSearchCriteriaDetailed DetailedCriteria=new ApplicationSearchCriteriaDetailed();
			DetailedCriteria.ApplicationId=App.ApplicationId;
			uk.gov.defra.Phoenix.BO.Application.Search.SearchResultGroup Result;
			Result=ApplicationSearch.SearchForApplications(DetailedCriteria,ApplicationSearch.SearchMode.Application,1300);
			Assert.IsFalse(Result.Permits.Data.Length==0,"No Results Were returned");
		}
	}
}
