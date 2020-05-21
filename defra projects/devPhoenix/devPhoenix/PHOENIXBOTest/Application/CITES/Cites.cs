using System;
using MbUnit.Core.Framework;
using MbUnit.Framework;
using uk.gov.defra.Phoenix.BO.Application;
using uk.gov.defra.Phoenix.BO;
using uk.gov.defra.Phoenix.BO.Application.Search;
using uk.gov.defra.Phoenix;
using uk.gov.defra.Phoenix.DO.DataObjects;

namespace uk.gov.defra.Phoenix.BOTest.Application.CITES
{
	public class CitesTestsTest
	{
		public void RunTest()
		{
			uk.gov.defra.Phoenix.BOTest.Application.CITES.ApplicationProgression.ApplicationProgression.ExpectedStatusList x=new uk.gov.defra.Phoenix.BOTest.Application.CITES.ApplicationProgression.ApplicationProgression.ExpectedStatusList();
			x.StatusProgressAllowed_AssignedToCaseOfficer_ByCustomer();
		}
	}
}

namespace uk.gov.defra.Phoenix.BOTest.Application.CITES.ApplicationSearch
{
	[TestFixture(), AuthorAttribute("Paul Nichols","paul.nichols@defra.gsi.gov.uk")]
	public class ApplicationSearch
	{
		/// <summary>
		/// Summary description for CITESBo.
		/// </summary>
		[Test()]
		public void TestSearch()
		{
			//				ApplicationSearchCriteriaDetailed DetailedCriteria=new ApplicationSearchCriteriaDetailed();
			//				DetailedCriteria.ApplicationId=_App.ApplicationId;
			//				uk.gov.defra.Phoenix.BO.Application.Search.SearchResultGroup Result;
			//				Result=ApplicationSearch.SearchForApplications(DetailedCriteria,ApplicationSearch.SearchMode.Application,_ssoCaseOfficerUserId);
			//				Console.WriteLine(_App.ApplicationId.ToString());
			//				Assert.IsTrue(Result.Permits.Data.Length>0,"No Results Were returned");
		}
	}
}

namespace uk.gov.defra.Phoenix.BOTest.Application.CITES.ApplicationProgression
{
	public class ApplicationProgression
	{
		private const long _ssoCustomerUserId =3405;
		private const long _ssoCaseOfficerUserId =1300;
		private const Int32 _numberOfCopies =2;

		public static uk.gov.defra.Phoenix.BO.Application.BOPermitInfo PI;
		public static BOPermitInfo[] PIs = null;

		public static void GetPermitInfo()
		{
			if (PIs !=null)
				PI=PIs[0];
			else
				PI=new uk.gov.defra.Phoenix.BO.Application.BOPermitInfo(5);
		}

		[TestFixture(), AuthorAttribute("Paul Nichols", "paul.nichols@defra.gsi.gov.uk")]
			public class ExpectedStatusList
		{

			[Test()]
			public void StatusProgressAllowed_AssignedToCaseOfficer_ByCaseOfficer()
			{
				
				ApplicationProgression.GetPermitInfo();

				ApplicationProgression.PI.PermitStatus=new BO.ReferenceData.BOPermitStatus((int)BOPermitInfo.PermitStatusTypes.ProgressAllowed);
				ApplicationProgression.PI.AssignedTo=new BO.ReferenceData.BOStatusAssignedToGroup((int)Common.AssignedToList.CaseOfficer);
			
				ApplicationProgression.PI.Save(_ssoCaseOfficerUserId);
				Console.WriteLine("Saved PI");
				
				uk.gov.defra.Phoenix.BO.Application.BOPermitInfo.StatusList[] ReturnedStatusList=ApplicationProgression.PI.GetStatusList(true,
					_ssoCaseOfficerUserId);
				

				System.Collections.ArrayList idList=new System.Collections.ArrayList();
				idList.AddRange(new int[] {(int)BOPermitInfo.PermitStatusTypes.Referred,
								(int)BOPermitInfo.PermitStatusTypes.ReferredForCAndA,
								(int)BOPermitInfo.PermitStatusTypes.ReferredForSpecimenReport,
                                (int)BOPermitInfo.PermitStatusTypes.Authorised,
                                (int)BOPermitInfo.PermitStatusTypes.Refused,
                                (int)BOPermitInfo.PermitStatusTypes.Cancelled});

				
                //imports                (int)BOPermitInfo.PermitStatusTypes.IssuedDraft

				uk.gov.defra.Phoenix.BO.Application.BOPermitInfo.StatusList[] ListToCheckAgainst=GetStatusList(idList);
				Assert.AreSame((BOPermitInfo.StatusList[])ListToCheckAgainst ,(BOPermitInfo.StatusList[])ReturnedStatusList);
			}

			[Test()]
			public void StatusProgressAllowed_AssignedToCaseOfficer_ByCustomer()
			{
				
				ApplicationProgression.GetPermitInfo();

				ApplicationProgression.PI.PermitStatus=new BO.ReferenceData.BOPermitStatus((int)BOPermitInfo.PermitStatusTypes.ProgressAllowed);
				ApplicationProgression.PI.AssignedTo=new BO.ReferenceData.BOStatusAssignedToGroup((int)Common.AssignedToList.CaseOfficer);
			
				ApplicationProgression.PI.Save(_ssoCaseOfficerUserId);
				Console.WriteLine("Saved PI");
				
				uk.gov.defra.Phoenix.BO.Application.BOPermitInfo.StatusList[] ReturnedStatusList=ApplicationProgression.PI.GetStatusList(true,
					_ssoCustomerUserId);
				

				System.Collections.ArrayList idList=new System.Collections.ArrayList();
				idList.AddRange(new int[] {(int)BOPermitInfo.PermitStatusTypes.CancelPending});

				
				//imports                (int)BOPermitInfo.PermitStatusTypes.IssuedDraft

				uk.gov.defra.Phoenix.BO.Application.BOPermitInfo.StatusList[] ListToCheckAgainst=GetStatusList(idList);
				Assert.AreSame((BOPermitInfo.StatusList[])ListToCheckAgainst ,(BOPermitInfo.StatusList[])ReturnedStatusList);
			}

			private uk.gov.defra.Phoenix.BO.Application.BOPermitInfo.StatusList[] GetStatusList(System.Collections.ArrayList idList)
			{
				uk.gov.defra.Phoenix.BO.Application.BOPermitInfo.StatusList[] ListToCheckAgainst=new uk.gov.defra.Phoenix.BO.Application.BOPermitInfo.StatusList[idList.Count];

				int index=0;
				foreach (int id in idList)
				{
					DO.DataObjects.Entity.PermitStatus Status= DO.DataObjects.Entity.PermitStatus.GetById(id);                        
					ListToCheckAgainst[index]=new uk.gov.defra.Phoenix.BO.Application.BOPermitInfo.StatusList(Status.Id, Status.Description, Status.Action);
					index++;
				}

				return ListToCheckAgainst;
			}
		}

		[TestFixture(), AuthorAttribute("Paul Nichols", "paul.nichols@defra.gsi.gov.uk")]
		public class PerformStatusChange
		{
			private BOApplication _App;

			private void SavePermit()
			{
				Console.WriteLine("Saving Permit");
				_App.Permit[0].Save(null);
			}
			private void SaveApp()
			{
				Console.WriteLine("Saving Application");
				_App=(BOApplication)_App.Save(true);
				Console.WriteLine("Application Saved");
			}

			private void CreatePermit()
			{
				Console.WriteLine("Creating a Permit");
				_App.Permit=new uk.gov.defra.Phoenix.BO.Application.BOPermit[1];
				_App.Permit[0]=new uk.gov.defra.Phoenix.BO.Application.BOPermit();
				_App.Permit[0].ApplicationId=_App.ApplicationId;
				_App.Permit[0].NumberOfCopies=_numberOfCopies;
				Console.WriteLine("Permit Created");
			}
				/// <summary>
				/// Summary description for CITESBo.
				/// </summary>
				[SetUpAttribute]
				public void Setup()
				{
					return;
					//Add an App to search on
					//			CreateApplication();
					//			SaveApp();
					//			Assert.IsNotNull(_App,"The Setup Application is Null");
					//			CreatePermit();
					//			Console.WriteLine("Created A Permit");
					//			SavePermit();
					//			Assert.IsNotNull(_App.Permit[0],"The Application has no Permit");
					//
					//			PIs=_App.Permit[0].GetPermitInfos(null);
					//			Assert.IsTrue(PIs.Length>0,"There are no Permit Infos");
					//			Console.WriteLine(PIs.Length.ToString());
				}

			

				/// <summary>
				/// Summary description for CITESBo.
				/// </summary>
				//		[TearDown]
				//		public void TearDown()
				//		{
				//			//Delete the App
				////_App.Delete
				//		}

				private void CreateApplication()
				{
					_App=new uk.gov.defra.Phoenix.BO.Application.CITES.Applications.BOImportApplication();
					_App.SSOUserId=_ssoCaseOfficerUserId;
					Console.WriteLine("Application Created");
				}
				

				[Test()]
				public void ProgressAllowedToReferredInspectorate_ByCaseOfficer()
				{

					//Get The Additional Information Object
					AdditionalInformation_UC222 AdditionalInformation=new AdditionalInformation_UC222((int)BOPermitInfo.PermitStatusTypes.Referred);
					Assert.IsTrue(Common.IsInRole(_ssoCaseOfficerUserId,Common.RolesList.CaseOfficer),"You need to change your Role in SSO to be a Case Officer");

					GetPermitInfo();

					PI.PermitStatus=new BO.ReferenceData.BOPermitStatus((int)BOPermitInfo.PermitStatusTypes.ProgressAllowed);
					PI.AssignedTo=new BO.ReferenceData.BOStatusAssignedToGroup((int)Common.AssignedToList.CaseOfficer);

					PI.Save(_ssoCaseOfficerUserId);
					Console.WriteLine("Saved PI");


					bool Success=BOPermitInfo.ChangeStatus(PI.PermitInfoId,(int)Common.AssignedToList.Inspectorate, 
						_ssoCaseOfficerUserId,AdditionalInformation);

					Assert.IsTrue(Success,"Status change Failed");

					GetPermitInfo();

					Assert.IsTrue((PI.AssignedTo.ID==(int)Common.AssignedToList.Inspectorate && PI.PermitStatus.ID==(int)BOPermitInfo.PermitStatusTypes.Referred), "State Has not Changed Correctly");
				}

				[Test()]
				public void ProgressAllowedToReferredKew_ByCaseOfficer()
				{

					//Get The Additional Information Object
					AdditionalInformation_UC222 AdditionalInformation=new AdditionalInformation_UC222((int)BOPermitInfo.PermitStatusTypes.Referred);
					Assert.IsTrue(Common.IsInRole(_ssoCaseOfficerUserId,Common.RolesList.CaseOfficer),"You need to change your Role in SSO to be a Case Officer");

					GetPermitInfo();

					PI.PermitStatus=new BO.ReferenceData.BOPermitStatus((int)BOPermitInfo.PermitStatusTypes.ProgressAllowed);
					PI.AssignedTo=new BO.ReferenceData.BOStatusAssignedToGroup((int)Common.AssignedToList.CaseOfficer);
				
					PI.Save(_ssoCaseOfficerUserId);
					Console.WriteLine("Saved PI");


					bool Success=BOPermitInfo.ChangeStatus(PI.PermitInfoId,(int)Common.AssignedToList.Kew, 
						_ssoCaseOfficerUserId,AdditionalInformation);
				
					Assert.IsTrue(Success,"Status change Failed");
				
					GetPermitInfo();

					Assert.IsTrue((PI.AssignedTo.ID==(int)Common.AssignedToList.Kew && PI.PermitStatus.ID==(int)BOPermitInfo.PermitStatusTypes.Referred), "State Has not Changed Correctly");
				}

				[Test()]
				public void ProgressAllowedToReferredTeamLeader_ByCaseOfficer()
				{

					//Get The Additional Information Object
					AdditionalInformation_UC222 AdditionalInformation=new AdditionalInformation_UC222((int)BOPermitInfo.PermitStatusTypes.Referred);
					Assert.IsTrue(Common.IsInRole(_ssoCaseOfficerUserId,Common.RolesList.CaseOfficer),"You need to change your Role in SSO to be a Case Officer");

					GetPermitInfo();

					PI.PermitStatus=new BO.ReferenceData.BOPermitStatus((int)BOPermitInfo.PermitStatusTypes.ProgressAllowed);
					PI.AssignedTo=new BO.ReferenceData.BOStatusAssignedToGroup((int)Common.AssignedToList.CaseOfficer);
				
					PI.Save(_ssoCaseOfficerUserId);
					Console.WriteLine("Saved PI");


					bool Success=BOPermitInfo.ChangeStatus(PI.PermitInfoId,(int)Common.AssignedToList.TeamLeader, 
						_ssoCaseOfficerUserId,AdditionalInformation);
				
					Assert.IsTrue(Success,"Status change Failed");
				
					GetPermitInfo();

					Assert.IsTrue((PI.AssignedTo.ID==(int)Common.AssignedToList.TeamLeader && PI.PermitStatus.ID==(int)BOPermitInfo.PermitStatusTypes.Referred), "State Has not Changed Correctly");
				}

				[Test()]
				public void ProgressAllowedToReferredEC_ByCaseOfficer()
				{

					//Get The Additional Information Object
					AdditionalInformation_UC222 AdditionalInformation=new AdditionalInformation_UC222((int)BOPermitInfo.PermitStatusTypes.Referred);
					Assert.IsTrue(Common.IsInRole(_ssoCaseOfficerUserId,Common.RolesList.CaseOfficer),"You need to change your Role in SSO to be a Case Officer");

					GetPermitInfo();

					PI.PermitStatus=new BO.ReferenceData.BOPermitStatus((int)BOPermitInfo.PermitStatusTypes.ProgressAllowed);
					PI.AssignedTo=new BO.ReferenceData.BOStatusAssignedToGroup((int)Common.AssignedToList.CaseOfficer);
				
					PI.Save(_ssoCaseOfficerUserId);
					Console.WriteLine("Saved PI");


					bool Success=BOPermitInfo.ChangeStatus(PI.PermitInfoId,(int)Common.AssignedToList.EC, 
						_ssoCaseOfficerUserId,AdditionalInformation);
				
					Assert.IsTrue(Success,"Status change Failed");
				
					GetPermitInfo();

					Assert.IsTrue((PI.AssignedTo.ID==(int)Common.AssignedToList.EC && PI.PermitStatus.ID==(int)BOPermitInfo.PermitStatusTypes.Referred), "State Has not Changed Correctly");
				}
				[Test()]
				public void ProgressAllowedToReferredForCAndA_ByCaseOfficer()
				{

					//Get The Additional Information Object
					AdditionalInformation_UC222 AdditionalInformation=new AdditionalInformation_UC222((int)BOPermitInfo.PermitStatusTypes.ReferredForCAndA);
					Assert.IsTrue(Common.IsInRole(_ssoCaseOfficerUserId,Common.RolesList.CaseOfficer),"You need to change your Role in SSO to be a Case Officer");

					GetPermitInfo();

					PI.PermitStatus=new BO.ReferenceData.BOPermitStatus((int)BOPermitInfo.PermitStatusTypes.ProgressAllowed);
					PI.AssignedTo=new BO.ReferenceData.BOStatusAssignedToGroup((int)Common.AssignedToList.CaseOfficer);
				
					PI.Save(_ssoCaseOfficerUserId);
					Console.WriteLine("Saved PI");


					bool Success=BOPermitInfo.ChangeStatus(PI.PermitInfoId,(int)Common.AssignedToList.Customer, 
						_ssoCaseOfficerUserId,AdditionalInformation);
				
					Assert.IsTrue(Success,"Status change Failed");
				
					GetPermitInfo();

					Assert.IsTrue((PI.AssignedTo.ID==(int)Common.AssignedToList.Customer && PI.PermitStatus.ID==(int)BOPermitInfo.PermitStatusTypes.ReferredForCAndA), "State Has not Changed Correctly");
				}

				[Test()]
				public void ProgressAllowedToReferredForSpecimenReport_ByCaseOfficer()
				{

					//Get The Additional Information Object
					AdditionalInformation_UC222 AdditionalInformation=new AdditionalInformation_UC222((int)BOPermitInfo.PermitStatusTypes.ReferredForSpecimenReport);
					Assert.IsTrue(Common.IsInRole(_ssoCaseOfficerUserId,Common.RolesList.CaseOfficer),"You need to change your Role in SSO to be a Case Officer");

					GetPermitInfo();

					PI.PermitStatus=new BO.ReferenceData.BOPermitStatus((int)BOPermitInfo.PermitStatusTypes.ProgressAllowed);
					PI.AssignedTo=new BO.ReferenceData.BOStatusAssignedToGroup((int)Common.AssignedToList.CaseOfficer);
				
					PI.Save(_ssoCaseOfficerUserId);
					Console.WriteLine("Saved PI");


					bool Success=BOPermitInfo.ChangeStatus(PI.PermitInfoId,(int)Common.AssignedToList.CaseOfficer , 
						_ssoCaseOfficerUserId,AdditionalInformation);
				
					Assert.IsTrue(Success,"Status change Failed");
				
					GetPermitInfo();

					Assert.IsTrue((PI.AssignedTo.ID==(int)Common.AssignedToList.CaseOfficer && PI.PermitStatus.ID==(int)BOPermitInfo.PermitStatusTypes.ReferredForSpecimenReport), "State Has not Changed Correctly");
				}

				[Test()]
				public void ProgressAllowedToReferredOther_ByCaseOfficer()
				{

					//Get The Additional Information Object
					AdditionalInformation_UC222 AdditionalInformation=new AdditionalInformation_UC222((int)BOPermitInfo.PermitStatusTypes.Referred);
					Assert.IsTrue(Common.IsInRole(_ssoCaseOfficerUserId,Common.RolesList.CaseOfficer),"You need to change your Role in SSO to be a Case Officer");

					GetPermitInfo();

					PI.PermitStatus=new BO.ReferenceData.BOPermitStatus((int)BOPermitInfo.PermitStatusTypes.ProgressAllowed);
					PI.AssignedTo=new BO.ReferenceData.BOStatusAssignedToGroup((int)Common.AssignedToList.CaseOfficer);
				
					PI.Save(_ssoCaseOfficerUserId);
					Console.WriteLine("Saved PI");


					bool Success=BOPermitInfo.ChangeStatus(PI.PermitInfoId,(int)Common.AssignedToList.Other, 
						_ssoCaseOfficerUserId,AdditionalInformation);
				
					Assert.IsTrue(Success,"Status change Failed");
				
					GetPermitInfo();

					Assert.IsTrue((PI.AssignedTo.ID==(int)Common.AssignedToList.Other && PI.PermitStatus.ID==(int)BOPermitInfo.PermitStatusTypes.Referred), "State Has not Changed Correctly");
				}

				[Test()]
				public void ProgressAllowedToReferredHMCustomsAndExcise_ByCaseOfficer()
				{

					//Get The Additional Information Object
					AdditionalInformation_UC222 AdditionalInformation=new AdditionalInformation_UC222((int)BOPermitInfo.PermitStatusTypes.Referred);
					Assert.IsTrue(Common.IsInRole(_ssoCaseOfficerUserId,Common.RolesList.CaseOfficer),"You need to change your Role in SSO to be a Case Officer");

					GetPermitInfo();

					PI.PermitStatus=new BO.ReferenceData.BOPermitStatus((int)BOPermitInfo.PermitStatusTypes.ProgressAllowed);
					PI.AssignedTo=new BO.ReferenceData.BOStatusAssignedToGroup((int)Common.AssignedToList.CaseOfficer);
				
					PI.Save(_ssoCaseOfficerUserId);
					Console.WriteLine("Saved PI");


					bool Success=BOPermitInfo.ChangeStatus(PI.PermitInfoId,(int)Common.AssignedToList.HMCustomsAndExcise, 
						_ssoCaseOfficerUserId,AdditionalInformation);
				
					Assert.IsTrue(Success,"Status change Failed");
				
					GetPermitInfo();

					Assert.IsTrue((PI.AssignedTo.ID==(int)Common.AssignedToList.HMCustomsAndExcise && PI.PermitStatus.ID==(int)BOPermitInfo.PermitStatusTypes.Referred), "State Has not Changed Correctly");
				}

				[Test()]
				public void ProgressAllowedToReferredPolicy_ByCaseOfficer()
				{

					//Get The Additional Information Object
					AdditionalInformation_UC222 AdditionalInformation=new AdditionalInformation_UC222((int)BOPermitInfo.PermitStatusTypes.Referred);
					Assert.IsTrue(Common.IsInRole(_ssoCaseOfficerUserId,Common.RolesList.CaseOfficer),"You need to change your Role in SSO to be a Case Officer");

					GetPermitInfo();

					PI.PermitStatus=new BO.ReferenceData.BOPermitStatus((int)BOPermitInfo.PermitStatusTypes.ProgressAllowed);
					PI.AssignedTo=new BO.ReferenceData.BOStatusAssignedToGroup((int)Common.AssignedToList.CaseOfficer);
				
					PI.Save(_ssoCaseOfficerUserId);
					Console.WriteLine("Saved PI");


					bool Success=BOPermitInfo.ChangeStatus(PI.PermitInfoId,(int)Common.AssignedToList.Policy, 
						_ssoCaseOfficerUserId,AdditionalInformation);
				
					Assert.IsTrue(Success,"Status change Failed");
				
					GetPermitInfo();

					Assert.IsTrue((PI.AssignedTo.ID==(int)Common.AssignedToList.Policy && PI.PermitStatus.ID==(int)BOPermitInfo.PermitStatusTypes.Referred), "State Has not Changed Correctly");
				}

				[Test()]
				public void ProgressAllowedToReferredCITESSecretariat_ByCaseOfficer()
				{

					//Get The Additional Information Object
					AdditionalInformation_UC222 AdditionalInformation=new AdditionalInformation_UC222((int)BOPermitInfo.PermitStatusTypes.Referred);
					Assert.IsTrue(Common.IsInRole(_ssoCaseOfficerUserId,Common.RolesList.CaseOfficer),"You need to change your Role in SSO to be a Case Officer");

					GetPermitInfo();

					PI.PermitStatus=new BO.ReferenceData.BOPermitStatus((int)BOPermitInfo.PermitStatusTypes.ProgressAllowed);
					PI.AssignedTo=new BO.ReferenceData.BOStatusAssignedToGroup((int)Common.AssignedToList.CaseOfficer);
				
					PI.Save(_ssoCaseOfficerUserId);
					Console.WriteLine("Saved PI");


					bool Success=BOPermitInfo.ChangeStatus(PI.PermitInfoId,(int)Common.AssignedToList.CITESSecretariat, 
						_ssoCaseOfficerUserId,AdditionalInformation);
				
					Assert.IsTrue(Success,"Status change Failed");
				
					GetPermitInfo();

					Assert.IsTrue((PI.AssignedTo.ID==(int)Common.AssignedToList.CITESSecretariat && PI.PermitStatus.ID==(int)BOPermitInfo.PermitStatusTypes.Referred), "State Has not Changed Correctly");
				}

				[Test()]
				public void ProgressAllowedToReferredJNCC_ByCaseOfficer()
				{

					//Get The Additional Information Object
					AdditionalInformation_UC222 AdditionalInformation=new AdditionalInformation_UC222((int)BOPermitInfo.PermitStatusTypes.Referred);
					Assert.IsTrue(Common.IsInRole(_ssoCaseOfficerUserId,Common.RolesList.CaseOfficer),"You need to change your Role in SSO to be a Case Officer");

					GetPermitInfo();

					PI.PermitStatus=new BO.ReferenceData.BOPermitStatus((int)BOPermitInfo.PermitStatusTypes.ProgressAllowed);
					PI.AssignedTo=new BO.ReferenceData.BOStatusAssignedToGroup((int)Common.AssignedToList.CaseOfficer);
				
					PI.Save(_ssoCaseOfficerUserId);
					Console.WriteLine("Saved PI");


					bool Success=BOPermitInfo.ChangeStatus(PI.PermitInfoId,(int)Common.AssignedToList.JNCC, 
						_ssoCaseOfficerUserId,AdditionalInformation);
				
					Assert.IsTrue(Success,"Status change Failed");
				
					GetPermitInfo();

					Assert.IsTrue((PI.AssignedTo.ID==(int)Common.AssignedToList.JNCC && PI.PermitStatus.ID==(int)BOPermitInfo.PermitStatusTypes.Referred), "State Has not Changed Correctly");
				}

				[Test()]
				public void ProgressAllowedToCancelled_ByCaseOfficer()
				{

					//Get The Additional Information Object
					AdditionalInformation_UC220 AdditionalInformation=new AdditionalInformation_UC220((int)BOPermitInfo.PermitStatusTypes.Cancelled);
					Assert.IsTrue(Common.IsInRole(_ssoCaseOfficerUserId,Common.RolesList.CaseOfficer),"You need to change your Role in SSO to be a Case Officer");

					GetPermitInfo();

					PI.PermitStatus=new BO.ReferenceData.BOPermitStatus((int)BOPermitInfo.PermitStatusTypes.ProgressAllowed);
					PI.AssignedTo=new BO.ReferenceData.BOStatusAssignedToGroup((int)Common.AssignedToList.CaseOfficer);
				
					PI.Save(_ssoCaseOfficerUserId);
					Console.WriteLine("Saved PI");


					bool Success=BOPermitInfo.ChangeStatus(PI.PermitInfoId,(int)Common.AssignedToList.CaseOfficer, 
						_ssoCaseOfficerUserId,AdditionalInformation);
				
					Assert.IsTrue(Success,"Status change Failed");
				
					GetPermitInfo();

					Assert.IsTrue((PI.AssignedTo.ID==(int)Common.AssignedToList.CaseOfficer && PI.PermitStatus.ID==(int)BOPermitInfo.PermitStatusTypes.Cancelled), "State Has not Changed Correctly");
				}

				[Test()]
				public void SubmittedByCustomerToProgressAllowed_ByCaseOfficer()
				{

					//Get The Additional Information Object
					AdditionalInformation_ProgressAllowed AdditionalInformation=new AdditionalInformation_ProgressAllowed((int)BOPermitInfo.PermitStatusTypes.ProgressAllowed);
					Assert.IsTrue(Common.IsInRole(_ssoCaseOfficerUserId,Common.RolesList.CaseOfficer),"You need to change your Role in SSO to be a Case Officer");

					GetPermitInfo();

					PI.PermitStatus=new BO.ReferenceData.BOPermitStatus((int)BOPermitInfo.PermitStatusTypes.SubmittedByCustomer);
					PI.AssignedTo=new BO.ReferenceData.BOStatusAssignedToGroup((int)Common.AssignedToList.CaseOfficer);
				
					PI.Save(_ssoCaseOfficerUserId);
					Console.WriteLine("Saved PI");


					bool Success=BOPermitInfo.ChangeStatus(PI.PermitInfoId,(int)Common.AssignedToList.CaseOfficer, 
						_ssoCaseOfficerUserId,AdditionalInformation);
					Assert.IsTrue(Success,"Status change Failed");
					PI=new uk.gov.defra.Phoenix.BO.Application.BOPermitInfo(5);
					Assert.IsTrue((PI.AssignedTo.ID==(int)Common.AssignedToList.CaseOfficer && PI.PermitStatus.ID==(int)BOPermitInfo.PermitStatusTypes.ProgressAllowed), "State Has not Changed Correctly");
				}

				[Test()]
				public void SubmittedByCustomerToCancelled_ByCaseOfficer()
				{

					//Get The Additional Information Object
					AdditionalInformation_UC220 AdditionalInformation=new AdditionalInformation_UC220((int)BOPermitInfo.PermitStatusTypes.Cancelled);
					Assert.IsTrue(Common.IsInRole(_ssoCaseOfficerUserId,Common.RolesList.CaseOfficer),"You need to change your Role in SSO to be a Case Officer");

					GetPermitInfo();

					PI.PermitStatus=new BO.ReferenceData.BOPermitStatus((int)BOPermitInfo.PermitStatusTypes.SubmittedByCustomer);
					PI.AssignedTo=new BO.ReferenceData.BOStatusAssignedToGroup((int)Common.AssignedToList.CaseOfficer);
				
					PI.Save(_ssoCaseOfficerUserId);
					Console.WriteLine("Saved PI");


					bool Success=BOPermitInfo.ChangeStatus(PI.PermitInfoId,(int)Common.AssignedToList.CaseOfficer, 
						_ssoCaseOfficerUserId,AdditionalInformation);
					Assert.IsTrue(Success,"Status change Failed");
					PI=new uk.gov.defra.Phoenix.BO.Application.BOPermitInfo(5);

					Assert.IsTrue((PI.AssignedTo.ID==(int)Common.AssignedToList.CaseOfficer && PI.PermitStatus.ID==(int)BOPermitInfo.PermitStatusTypes.Cancelled), "State Has not Changed Correctly");
				}


				[Test()]
				public void BeingInputByCaseOfficerToCancelled_ByCaseOfficer()
				{

					//Get The Additional Information Object
					AdditionalInformation_UC220 AdditionalInformation=new AdditionalInformation_UC220((int)BOPermitInfo.PermitStatusTypes.Cancelled);
					Assert.IsTrue(Common.IsInRole(_ssoCaseOfficerUserId,Common.RolesList.CaseOfficer),"You need to change your Role in SSO to be a Case Officer");

					GetPermitInfo();

					PI.PermitStatus=new BO.ReferenceData.BOPermitStatus((int)BOPermitInfo.PermitStatusTypes.BeingInput_CaseOfficer);
					PI.AssignedTo=new BO.ReferenceData.BOStatusAssignedToGroup((int)Common.AssignedToList.CaseOfficer);
				
					PI.Save(_ssoCaseOfficerUserId);
					Console.WriteLine("Saved PI");


					bool Success=BOPermitInfo.ChangeStatus(PI.PermitInfoId,(int)Common.AssignedToList.CaseOfficer, 
						_ssoCaseOfficerUserId,AdditionalInformation);
					Assert.IsTrue(Success,"Status change Failed");

					PI=new uk.gov.defra.Phoenix.BO.Application.BOPermitInfo(5);
					Assert.IsTrue((PI.AssignedTo.ID==(int)Common.AssignedToList.CaseOfficer && PI.PermitStatus.ID==(int)BOPermitInfo.PermitStatusTypes.Cancelled), "State Has not Changed Correctly");

				}

				[Test()]
				public void BeingInputByCustomerToSubmittedByCustomer_ByCustomer()
				{

					//Get The Additional Information Object
					AdditionalInformation_SubmittedByCustomer AdditionalInformation=new AdditionalInformation_SubmittedByCustomer((int)BOPermitInfo.PermitStatusTypes.SubmittedByCustomer);
				
					Assert.IsTrue(Common.IsInRole(_ssoCaseOfficerUserId,Common.RolesList.Customer),"You need to change your Role in SSO to be a Customer");

					GetPermitInfo();

					PI.PermitStatus=new BO.ReferenceData.BOPermitStatus((int)BOPermitInfo.PermitStatusTypes.BeingInput_Customer);
					PI.AssignedTo=new BO.ReferenceData.BOStatusAssignedToGroup((int)Common.AssignedToList.Customer);
				
					PI.Save(_ssoCaseOfficerUserId);
					Console.WriteLine("Saved PI");


					bool Success=BOPermitInfo.ChangeStatus(PI.PermitInfoId,(int)Common.AssignedToList.Customer, 
						_ssoCaseOfficerUserId,AdditionalInformation);
					Assert.IsTrue(Success,"Status change Failed");
				
					PI=new uk.gov.defra.Phoenix.BO.Application.BOPermitInfo(5);
					Assert.IsTrue((PI.AssignedTo.ID==(int)Common.AssignedToList.Customer && PI.PermitStatus.ID==(int)BOPermitInfo.PermitStatusTypes.SubmittedByCustomer), "State Has not Changed Correctly");
				}

				


				[Test()]
				public void BeingCaseOfficerToProgressAllowed_ByCaseOfficer()
				{

					//Get The Additional Information Object
					AdditionalInformation_ProgressAllowed AdditionalInformation=new AdditionalInformation_ProgressAllowed((int)BOPermitInfo.PermitStatusTypes.ProgressAllowed);
				
					Assert.IsTrue(Common.IsInRole(_ssoCaseOfficerUserId,Common.RolesList.CaseOfficer),"You need to change your Role in SSO to be a Case Officer");

					GetPermitInfo();
					
					//set the current status and who the permit is currently asigned to
					PI.PermitStatus=new BO.ReferenceData.BOPermitStatus((int)BOPermitInfo.PermitStatusTypes.BeingInput_CaseOfficer);
					PI.AssignedTo=new BO.ReferenceData.BOStatusAssignedToGroup((int)Common.AssignedToList.CaseOfficer);
				
					try
					{
						Console.WriteLine("About to save");
						PI.Save(_ssoCaseOfficerUserId);
					}
					catch (System.ArgumentNullException ex)
					{
						Console.WriteLine(ex.ToString());
					}

					
					Console.WriteLine("Saved PI");


					bool Success=BOPermitInfo.ChangeStatus(PI.PermitInfoId,(int)Common.AssignedToList.CaseOfficer, 
						_ssoCaseOfficerUserId,AdditionalInformation);
					Assert.IsTrue(Success,"Status change Failed");
				
					PI=new uk.gov.defra.Phoenix.BO.Application.BOPermitInfo(5);
					Assert.IsTrue((PI.AssignedTo.ID==(int)Common.AssignedToList.CaseOfficer && PI.PermitStatus.ID==(int)BOPermitInfo.PermitStatusTypes.ProgressAllowed), "State Has not Changed Correctly");
				}
			}
	}


}



	
