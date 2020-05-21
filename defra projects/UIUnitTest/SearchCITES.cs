using System;
using NUnit.Framework;
using NUnit.Extensions.Asp;
using NUnit.Extensions.Asp.AspTester;

namespace UIUnitTest
{
	/// <summary>
	/// this is a test
	/// </summary>
	[TestFixture]
	public class SearchCites:WebFormTestCase
	{
		
		/// <summary>
		/// this is a test
		/// </summary>
		/// 
		protected override void SetUp() 
		{
			Browser.Credentials=System.Net.CredentialCache.DefaultCredentials;
				Browser.GetPage("http://mydevphoenixsecure/Default.aspx");
			
		}
		/// <summary>
		/// this is a test
		/// </summary>
		protected override void TearDown() 
		{
			
		}

		/// <summary>
		/// this is a test
		/// </summary>
		[Test]
		public void TestNavigation()
		{
		//	Browser.GetPage("http://mydevphoenixsecure/Default.aspx?AppType=11&Menu=Menu&Module=CITES/Applications/ApplicationManager");
//			UserControlTester SemiCompleteUserControl	=new UserControlTester("SemiComplete",Manager);
			UserControlTester AppTypeUserControl	=new UserControlTester("ApplicationType",Manager);
//			UserControlTester ActingAsAgentUserControl	=new UserControlTester("ActingAsAgent",Manager);
			LabelTester lblAppType =new LabelTester("lblAppType",AppTypeUserControl);
//			
//			Console.WriteLine(SemiCompleteUserControl.AspId);
//			Console.WriteLine(AppTypeUserControl.AspId);
//			Console.WriteLine(ActingAsAgentUserControl.AspId);
			//Console.WriteLine(NextButton.Visible.ToString());
			//AssertVisibility(lblAppType,true);
Console.WriteLine(this.Browser.CurrentPageText);
			Console.WriteLine(lblAppType.HtmlId);
			Console.WriteLine(lblAppType.Text);
			//NextButton.Click();
		}

//		private readonly NUnit.Extensions.Asp.WebFormTestCase.CurrentWebForm CurrentForm ()
//		{
//			return this.CurrentWebForm;
//		}

		private  UserControlTester Manager
		{
			get
			{
				if (manager==null)
				{
					manager=new UserControlTester("Manager",CurrentWebForm);
				}
				return manager;
			}
		}
		private UserControlTester manager;

		private  LinkButtonTester NextButton
		{
			get
			{
				if (nextButton==null)
				{
					nextButton=new LinkButtonTester("butNext",Header);
				}
				return nextButton;
			}
		}
		private LinkButtonTester nextButton;

		private   LinkButtonTester PreviousButton
		{
			get
			{
				if (previousButton==null)
				{
					previousButton=new LinkButtonTester("butPrev",Header);
				}
				return previousButton;
			}
		}
		private LinkButtonTester previousButton;

		private   UserControlTester Header
		{
			get
			{
				if (header==null)
				{
					header=new UserControlTester("ManagerHeader",Manager);
				}
				return header;
			}
		}
		private UserControlTester header;

		private   UserControlTester Footer
		{
			get
			{
				if (footer==null)
				{
					footer=new UserControlTester("ManagerHeader",Manager);
				}
				return footer;
			}
		}
		private UserControlTester footer;
	}
}
