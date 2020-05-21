using System;
using MbUnit.Framework;
using MbUnit.Core.Framework;
using uk.gov.defra.Phoenix.BO.Application;

namespace uk.gov.defra.Phoenix.BOTest.Application
{
	[TestFixture(), AuthorAttribute("Steven Sartain", "steven.sartain@defra.gsi.gov.uk")]
	public class XSD
	{
		[Test()]
		public void GetInfoFromBirdRegistrationXSDTest()
		{
			string[] EnumValues = System.Enum.GetNames(typeof(ApplicationUtils.BirdRegistrationXSDType));
		//	Assert.DoAssert(new IsNotEmptyAsserter(EnumValues));
			foreach (string EnumVal in EnumValues)
			{
				object FunctionGetValue = ApplicationUtils.GetInfoFromBirdRegistrationXSD((ApplicationUtils.BirdRegistrationXSDType)(System.Enum.Parse(typeof(ApplicationUtils.BirdRegistrationXSDType), EnumVal)));
				Assert.IsNotNull(FunctionGetValue);
			}
		}
	}

}
