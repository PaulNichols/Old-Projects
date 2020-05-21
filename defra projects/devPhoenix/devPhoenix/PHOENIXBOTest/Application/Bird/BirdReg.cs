using System;
using MbUnit.Core.Framework;
using uk.gov.defra.Phoenix.BO.Application.Bird.Registration.Search;
using uk.gov.defra.Phoenix.BO.Application.Bird.Registration;
using uk.gov.defra.Phoenix.BO.Application.Search;
using uk.gov.defra.Phoenix.BO.Application.Bird.Registration.SummaryData;
using MbUnit.Framework;

namespace uk.gov.defra.Phoenix.BOTest.Application
{
	[TestFixture()] 
	public class BirdReg_Existing : BirdReg
	{
		private const int SLA_PAUSE = 1;
		[TestFixtureSetUpAttribute()]
		public void LoadApplication()
		{
			//const int RingApplicationId = 2625;
			_birdReg = new BirdRegistration(BirdRegistrationApplicationId);
			Assert.IsNotNull(_birdReg);
		}
//
//		[Test()]
//		public void LoadApplicationSummaryData() 
//		{
//			////			_birdReg = new BirdRegistration(BirdRegistrationApplicationId);
//			//BaseSummary SummaryData = (BaseSummary[])_birdReg.GetSummaryGridData(SSOUSerId_Sartain); 
//			Console.WriteLine(BaseSummary.ConfigFileDirectory);
//			Console.WriteLine(BaseSummary.ConfigFileName);
//			Console.WriteLine(BaseSummary.ConnectionString);
//			EnterpriseObjects.EnterpriseApplication e = new EnterpriseObjects.EnterpriseApplication();
//			Console.WriteLine(e.ConfigFileName);
//		}
//			BaseSummary[] SummaryData = (BaseSummary[])_birdReg.GetSummaryGridData(SSOUSerId_Sartain); 
//			Assert.IsNotNull(SummaryData);
//
//			if (_birdReg.ApplicationType == RegistrationApplicationType.Clutch)
//			{
//				//as it's a clutch then make sure that the results are for a clutch
//				Assert.IsTrue(SummaryData.GetType().Equals(typeof(ChickSummary))); 
//			}
//		}

//		[Test()]
//		public void Tips()
//		{
//			_birdReg.ApplicationType = RegistrationApplicationType.Imported;
//
//			AdultImported a = (AdultImported)_birdReg.RegistrationApplication;
//			AdultImportedSpecimen ais = a.AddSpecimen();
//			ais.CountryOfOrigin = 4;
//			Console.WriteLine(ais.CountryOfOrigin_Helper);
//			_birdReg.Save();
//
//			int AppId = _birdReg.ApplicationId;
//
//			_birdReg = null;
//
//			_birdReg = new BirdRegistration(AppId);
//
//			a = (AdultImported)_birdReg.RegistrationApplication;
//			ais = (AdultImportedSpecimen)a.Specimens[0];
//			Console.WriteLine(ais.CountryOfOrigin_Helper);
//		}

		[Test()]
		public void Save()
		{
			Assert.IsNotNull(_birdReg.Save());
		}

//		[Test]
//		public void ChickDOR_Search()
//		{
//			//uk.gov.defra.Phoenix.BO.Application.Search
//			ApplicationSearchCriteriaCommon SearchInfo = new ApplicationSearchCriteriaCommon();
//			SearchInfo.ApplicationId = ApplicationId;
//			SearchInfo.Status = new int[] {0, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23};
//
//			SearchResultGroup Results = ApplicationSearch.SearchForApplications(SearchInfo,ApplicationSearch.SearchMode.Application, SSOUSerId);
//			//Assert.Results.ChickDORs.Data.Length
//			Console.WriteLine(((uk.gov.defra.Phoenix.BO.Application.Search.Data.BirdOtherSearchData_NonCustomer)Results.ChickDORs.Data[0]).Status);
//
//			Assert.IsTrue(Results.ChickDORs.Data.Length > 0, "Must find at least one result");
//		}

		[Test]
		public void SLA()
		{
			//read the current status out so that we can reset it at the end
			BO.Application.BOPermitInfo.PermitStatusTypes CurentStatus = _birdReg.ApplicationStatus;
			//read out current SLA info so it can also be reset
			int SLA = _birdReg.SLAClock;
			DateTime SLADate = _birdReg.SLAStart;
			
			//change the status to something other than DOR returned
			_birdReg.ApplicationStatus = BO.Application.BOPermitInfo.PermitStatusTypes.BeingInput_Customer;
			_birdReg.SLAClock = 0;
			_birdReg.SLAStart = new DateTime(0);

			//save it all
			_birdReg = _birdReg.Save();

			//now start tweaking things to see if it all works
			_birdReg.ApplicationStatus = BO.Application.BOPermitInfo.PermitStatusTypes.DOR_Returned;
			BO.MockSystemEnvironmental Env = new BO.MockSystemEnvironmental();
			_birdReg.SetEnvironment(Env);
			_birdReg = _birdReg.Save();

			//we have started the ball rolling
			//wait for a bit so that we can get some time allocated
			//System.Threading.Thread.Sleep(new TimeSpan(0, SLA_PAUSE, 0));
			Env.IncrementMinutes(SLA_PAUSE);

			//change the status back and save again
			_birdReg.ApplicationStatus = BO.Application.BOPermitInfo.PermitStatusTypes.BeingInput_Customer;
			_birdReg = _birdReg.Save();

			//now, we should have 1 min worth of SLA! (well, SLA_PAUSE anyway!)
			int NewSLAClock = _birdReg.SLAClock;

			//set everything back
			_birdReg.ApplicationStatus = CurentStatus;
			_birdReg.SLAClock = SLA;
			_birdReg.SLAStart = SLADate;
			_birdReg.Save();

			//did it work?!?
			Console.WriteLine("SLAClock set as {0}", NewSLAClock);
			Assert.IsTrue(NewSLAClock >= SLA_PAUSE, "SLA time not set correctly");
		}

		[Test]
		public void Clone()
		{
			BirdRegistration ClonedApp = _birdReg.Clone(SSOUSerId_Sartain);
			Assert.IsNotNull(ClonedApp);

			int ClonedApplicationId = ClonedApp.ApplicationId;

			//recloning should fail
			try
			{
				ClonedApp.Clone(SSOUSerId_Sartain);
				Assert.Fail("An application should only be able to be cloned once");
			}
			catch //(Exception ex)
			{
				//all is good if we get an exception!
			}

			//try to reload the cloned application
			ClonedApp = new BirdRegistration(ClonedApplicationId);
			Assert.IsNotNull(ClonedApp);

			//make sure we have the existing id in this app
			Assert.AreNotSame(BirdRegistrationApplicationId, ClonedApp.ApplicationId);

			//make sure we can load the existing from the cloned
			BirdRegistration ApplicationFromClonedApp = ClonedApp.GetRelatedApplication();
			Assert.IsNotNull(ApplicationFromClonedApp);
			ApplicationFromClonedApp = null;

			ClonedApp = null;

			//to reclone we need to reload the parent
			LoadApplication();
			BirdRegistration ClonedApp2 = _birdReg.Clone(SSOUSerId_Sartain);
			Assert.IsNotNull(ClonedApp2);
			int ClonedApplicationId2 = ClonedApp2.ApplicationId;


			//Check that the other app can get to the cloned app
			LoadApplication();
			int[] OtherIds = _birdReg.GetAllApplicationsBasedOnThis();
			Assert.IsNotNull(OtherIds);
			Console.WriteLine(OtherIds.Length.ToString());
			 Assert.GreaterThan(OtherIds.Length, 0);
			//SCS Assert.DoAssert(new IsNotEmptyAsserter(OtherIds));

			//check if the cloned id is in this list
			System.Collections.ArrayList ResultList = new System.Collections.ArrayList(OtherIds);
			Console.WriteLine(ResultList.Count.ToString());
			Console.WriteLine(ResultList[0].ToString());
			Assert.IsTrue(ResultList.Contains(ClonedApplicationId), "Cloned application doesn't exist in the based on list!");
			Assert.IsTrue(ResultList.Contains(ClonedApplicationId2), "Cloned application 2 doesn't exist in the based on list!");

			//tidy up cloned app
			RemoveApplication(ClonedApplicationId);
			RemoveApplication(ClonedApplicationId2);
		}
	}

	[TestFixture()]
	public class BirdReg_New : BirdReg
	{
		[SetUpAttribute()]
		public virtual void CreateApplication()
		{
			_birdReg = InternalCreateApplication();
		}

		protected virtual BirdRegistration InternalCreateApplication()
		{
			const int PartyId = 1;
			
			BirdRegistration Result = new BirdRegistration(PartyId, BirdRegLoadMode.LoadByParty, -1, string.Empty);
			Assert.IsNotNull(Result);

			return Result;
		}

//		[Test()]
//		public void DORReturned()
//		{
//			_birdReg.RegApplicationType = RegistrationApplicationType.Clutch;
//
//			//add an egg
//			Clutch Clutch= (Clutch)_birdReg.RegistrationApplication;
//			ClutchEgg NewEgg = Clutch.AddEgg();
//
//			NewEgg.CreateSpecimenFrom(Clutch);
//
//			AddEggWithOneRing(RingMark);
//
//			//add a specimen (required to persist ring info)
//			ClutchSpecimen Spec1 = ClutchApp.Eggs[0].CreateSpecimenFrom(ClutchApp);
//			Spec1.SpecimenType = new SpecimenType(BO.Application.GenderType.Male);
//			ValidateEggs();
//			IDMark NewRing = Spec1.AddRing(ClutchApp.Eggs[0].Rings[0]);
//			NewRing.GetMarkType();
//
//			7
//				_birdReg = _birdReg.SetDORReturned();
//			Assert.IsNotNull(_birdReg);
//
//
//		}

		[TestFixtureTearDown]
		public void RemoveApplication()
		{
			RemoveApplication(_birdReg.ApplicationId);
		}
	}

	[TestFixture()]
	public class BirdReg_Rings : BirdReg_New
	{
		private Clutch ClutchApp
		{
			get
			{
				return (Clutch)_birdReg.RegistrationApplication;
			}
			set
			{
				_birdReg.RegistrationApplication = value;
			}
		}

		[TestFixtureSetUpAttribute()]
		public override void CreateApplication()
		{
			base.CreateApplication();

			//make the application a clutch
			_birdReg.RegApplicationType = RegistrationApplicationType.Clutch;
		}

		private ClutchEgg AddEgg()
		{
			return AddEgg(ClutchApp);
		}

		private ClutchEgg AddEgg(Clutch clutch)
		{
			//add an egg
			ClutchEgg NewEgg = clutch.AddEgg();
			Assert.IsNotNull(NewEgg);
			Assert.IsTrue(clutch.Eggs.Length > 0, "Egg not added!");

			return NewEgg;
		}

		private IDMark AddRing(ClutchEgg egg, string mark)
		{
			//add a ring
			IDMark NewRing = egg.AddRing();
			Assert.IsNotNull(NewRing);
			Assert.IsTrue(egg.Rings.Length > 0, "Ring not added!");

			NewRing.Mark = mark;
			NewRing.GetMarkType();

			return NewRing;
		}

		private IDMark AddEggWithOneRing(string mark)
		{
			return AddEggWithOneRing(ClutchApp, mark);
		}

		private IDMark AddEggWithOneRing(Clutch clutch, string mark)
		{
			//add an egg
			ClutchEgg NewEgg = AddEgg(clutch);

			//add a ring
			IDMark NewRing = AddRing(NewEgg, mark);

			return NewRing;
		}

		private BirdRegistration ValidateEggs()
		{
			_birdReg = ValidateEggs(_birdReg);

			return _birdReg;
		}

		private BirdRegistration ValidateEggs(BirdRegistration bird)
		{
			bird = ClutchEgg.AreEggsValid(bird);
			Assert.IsNotNull(bird);

			return bird;
		}

		[Test()]
		public void CheckRingMarkValidation()
		{
			//add a egg & ring
			IDMark NewRing = AddEggWithOneRing("UK9876");

			ValidateEggs();
			//make sure there has been no validation errors
			Assert.IsNull(_birdReg.ValidationErrors);

			//should be of type 4
			Assert.AreNotSame(4, ClutchApp.Eggs[0].Rings[0].MarkType);

			//check another type
			ClutchApp.Eggs[0].Rings[0].Mark = "Z12345";
			_birdReg = ClutchEgg.AreEggsValid(_birdReg);
			Assert.IsNotNull(_birdReg);

			//make sure there has been no validation errors
			Assert.IsNull(_birdReg.ValidationErrors);

			//should be of type 5
			Assert.AreNotSame(5, ClutchApp.Eggs[0].Rings[0].MarkType);
		}

		[Test()]
		public void CheckRingMarkIdCannotBeEnteredTwiceOnSameEgg()
		{
			//add an egg
			ClutchEgg NewEgg = AddEgg();

			//add a ring & mark
			IDMark NewRing = AddRing(NewEgg, "UK9876");

			//add another ring with the same id mark
			IDMark NewRing2 = AddRing(NewEgg, "UK9876");

			//validate
			ValidateEggs();
			//make sure there has been validation errors
			Assert.IsNotNull(_birdReg.ValidationErrors);

			//the error should be RingOnEggIsUsed
			Assert.AreNotSame(Phoenix.BO.ValidationError.ValidationCodes.RingOnEggIsUsed, ((Phoenix.BO.ValidationError)_birdReg.ValidationErrors.Errors[0]).ValidationID);
		}

		[Test()]
		public void CheckRingMarkIdCannotBeEnteredTwiceOnDiffEggs()
		{
			CheckRingMarkIdEntry("UK9876");
		}

		[Test()]
		public void CheckRingMarkIdCanBeEnteredTwiceOnDiffEggs()
		{
			CheckRingMarkIdEntry("UK9876", "UK4432");
		}

		private void CheckRingMarkIdEntry(string mark)
		{
			CheckRingMarkIdEntry(mark, mark);
		}

		private void CheckRingMarkIdEntry(string mark1, string mark2)
		{
			//add an egg
			ClutchEgg NewEgg = AddEgg();

			//add a ring
			IDMark NewRing = AddRing(NewEgg, mark1);

			//add another egg
			ClutchEgg NewEgg2 = AddEgg();

			//add another ring with the next id mark
			IDMark NewRing2 = AddRing(NewEgg2, mark2);

			//validate
			ValidateEggs();

			if (mark1 == mark2)
			{
				//make sure there has been validation errors
				Assert.IsNotNull(_birdReg.ValidationErrors);

				//the error should be RingOnEggIsUsed
				Assert.AreNotSame(Phoenix.BO.ValidationError.ValidationCodes.RingOnEggIsUsed, ((Phoenix.BO.ValidationError)_birdReg.ValidationErrors.Errors[0]).ValidationID);
			}
			else
			{
				//make sure there has not been validation errors
				Assert.IsNull(_birdReg.ValidationErrors);
			}
		}


		[Test()]
		public void MustHaveOneRingForRingRequest()
		{
			//add an egg
			ClutchEgg NewEgg = AddEgg();

			//add a ring
			IDMark NewRing = AddRing(NewEgg, "UK9876");

			//add another ring
			IDMark NewRing2 = AddRing(NewEgg, "UK9877");

			BirdRegistration ClonedApp = _birdReg.Clone(SSOUSerId_Sartain);
			int ClonedApplicationId = ClonedApp.ApplicationId;

			ClonedApp = ValidateEggs(ClonedApp);

			//make sure there has been validation errors
			Assert.IsNotNull(_birdReg.ValidationErrors);

			//the error should be MustHaveAtLeastOneRingIfNoExtraEggs
			Assert.AreNotSame(Phoenix.BO.ValidationError.ValidationCodes.MustHaveAtLeastOneRingIfNoExtraEggs, ((Phoenix.BO.ValidationError)_birdReg.ValidationErrors.Errors[0]).ValidationID);

			//tidy up cloned app
			RemoveApplication(ClonedApplicationId);
		}

		[Test()]
		public void CheckRingMarkIdCannotBeEnteredTwiceOnDiffEggsOnClone()
		{
			//add an egg
			ClutchEgg NewEgg = AddEgg();

			//add a ring
			IDMark NewRing = AddRing(NewEgg, "UK9876");

			//add another egg
			ClutchEgg NewEgg2 = AddEgg();

			//add another ring with the same id mark
			IDMark NewRing2 = AddRing(NewEgg2, "UK9876");

			//clone
			BirdRegistration ClonedApp = _birdReg.Clone(SSOUSerId_Sartain);
			int ClonedApplicationId = ClonedApp.ApplicationId;

			ClonedApp = ValidateEggs(ClonedApp);

			//add a ring to each egg - existing rings will have gone
			Clutch ClonedClutch = (Clutch)ClonedApp.RegistrationApplication;
			AddRing(ClonedClutch.Eggs[0], "UK1567");
			AddRing(ClonedClutch.Eggs[1], "UK1567");

			//validate
			ValidateEggs();
			//make sure there has been validation errors
			Assert.IsNotNull(_birdReg.ValidationErrors);

			//the error should be RingOnEggIsUsed
			Assert.AreNotSame(Phoenix.BO.ValidationError.ValidationCodes.RingOnEggIsUsed, ((Phoenix.BO.ValidationError)_birdReg.ValidationErrors.Errors[0]).ValidationID);

			//tidy up cloned app
			RemoveApplication(ClonedApplicationId);
		}

		[Test()]
		public void CheckRingMarkIdCannotBeEnteredTwiceOnDiffEggsOnCloneWithNew()
		{
			//add an egg
			ClutchEgg NewEgg = AddEgg();

			//add a ring
			IDMark NewRing = AddRing(NewEgg, "UK9876");

			//clone
			BirdRegistration ClonedApp = _birdReg.Clone(SSOUSerId_Sartain);
			int ClonedApplicationId = ClonedApp.ApplicationId;

			ClonedApp = ValidateEggs(ClonedApp);

			//add a ring to egg - existing rings will have gone
			Clutch ClonedClutch = (Clutch)ClonedApp.RegistrationApplication;
			AddRing(ClonedClutch.Eggs[0], "UK1567");

			AddEggWithOneRing("UK1567");

			//validate
			ValidateEggs();
			//make sure there has been validation errors
			Assert.IsNotNull(_birdReg.ValidationErrors);

			//the error should be RingOnEggIsUsed
			Assert.AreNotSame(Phoenix.BO.ValidationError.ValidationCodes.RingOnEggIsUsed, ((Phoenix.BO.ValidationError)_birdReg.ValidationErrors.Errors[0]).ValidationID);

			//tidy up cloned app
			RemoveApplication(ClonedApplicationId);
		}

		[Test()]
		public void RingsPickedCheck()
		{
			//add an egg
			ClutchEgg NewEgg = AddEgg();

			//add a ring
			IDMark NewRing = AddRing(NewEgg, "UK9876");

			//clone
			BirdRegistration ClonedApp = _birdReg.Clone(SSOUSerId_Sartain);
			int ClonedApplicationId = ClonedApp.ApplicationId;

			ClonedApp = ValidateEggs(ClonedApp);

			//add a ring to egg - existing rings will have gone
			Clutch ClonedClutch = (Clutch)ClonedApp.RegistrationApplication;
			AddRing(ClonedClutch.Eggs[0], "UK1567");

			AddEggWithOneRing("UK1568");

			Assert.IsTrue(ClonedClutch.RingsPicked);
				
			//tidy up cloned app
			RemoveApplication(ClonedApplicationId);
		}

		[Test()]
		public void CheckRingEnterOnAnotherApplicationBeforeDOR()
		{
			const string RingMark = "AY1235";

			//add a ring 
			AddEggWithOneRing(RingMark);

			//add a specimen (required to persist ring info)
			ClutchSpecimen Spec1 = ClutchApp.Eggs[0].CreateSpecimenFrom(ClutchApp);
			Spec1.SpecimenType = new SpecimenType(BO.Application.GenderType.Male);
			ValidateEggs();
			IDMark NewRing = Spec1.AddRing(ClutchApp.Eggs[0].Rings[0]);
			NewRing.GetMarkType();

			//...and save it
			_birdReg = _birdReg.Save();

			//create another application
			BirdRegistration NewApp = InternalCreateApplication();

			//add a ring & egg
			Clutch NewClutch = (Clutch)NewApp.RegistrationApplication;
			AddEggWithOneRing(NewClutch, RingMark);

			//add a specimen (required to persist ring info)
			ClutchSpecimen Spec2 = NewClutch.Eggs[0].CreateSpecimenFrom(NewClutch);
			Spec2.SpecimenType = new SpecimenType(BO.Application.GenderType.Male);
			ValidateEggs(NewApp);
			Spec2.AddRing(NewClutch.Eggs[0].Rings[0]);

			//...and save it
			NewApp = NewApp.Save();

			//check to see if it picks up the ring from the other app
			NewApp = ValidateEggs(NewApp);

			//make sure there has been validation errors
			Assert.IsNotNull(NewApp.ValidationErrors);
	
			//tidy up additional app
			RemoveApplication(NewApp.ApplicationId);
		}

		[Test()]
		public void CheckReloadOfAdultImported()
		{
			CheckReload(RegistrationApplicationType.Imported);
		}

		[Test()]
		public void CheckReloadOfAdultOther()
		{
			CheckReload(RegistrationApplicationType.Other);
		}

		[Test()]
		public void CheckReloadOfAdultFound()
		{
			CheckReload(RegistrationApplicationType.Found);
		}

		private void CheckReload(RegistrationApplicationType appType)
		{
			//add specimens
			_birdReg.RegApplicationType = appType;

			//create a basebird object
			BaseBird Bird = _birdReg.RegistrationApplication;

			//create a specimen
			AdultSpecimenType Specimen =  Bird.AddPolymorphicSpecimen();

			Assert.IsTrue(0 < Bird.Specimens.Length, "Specimen not added");

			//save the application
			int ApplicationId = _birdReg.ApplicationId;
			_birdReg = _birdReg.Save();
			Assert.IsNotNull(_birdReg);
			Console.WriteLine(_birdReg.GetXML());

			//reload the application
			_birdReg = new BirdRegistration(ApplicationId);
			Assert.IsNotNull(_birdReg);

			//check to see if the application has specimens
			//Console.WriteLine(_birdReg.GetXML());
			Assert.IsTrue(0 < _birdReg.RegistrationApplication.Specimens.Length, "Specimens not re-loaded!");
		}
	}

	[AuthorAttribute("Steven Sartain", "steven.sartain@defra.gsi.gov.uk")]
	public class BirdReg : TestBase
	{
		internal BirdRegistration _birdReg;

		protected void RemoveApplication(int applicationId)
		{
			//SCS Assert.AreNotEqual(applicationId, 0);
			Assert.GreaterThan(applicationId, 0);
			
			//tidy up the records I created.  Could be performed with a transaction instead.
			Assert.IsTrue(BirdRegistration.Delete(applicationId));

			//remove the records from the search table
			BirdRegistrationSearch.RemoveExistingSearchInfo(applicationId, null);
		}
//		[Test]
//		public void Load()
//		{}
	}
}
