using System;
using MbUnit.Core.Framework;
using MbUnit.Framework;
using uk.gov.defra.Phoenix.BO.Party;

namespace uk.gov.defra.Phoenix.BOTest.Party
{
	[TestFixture()]
	public class KnownFacts
	{
		[Test, Ignore("Can't be 100% sure that this test is constant")]
		public void RetrievePartyByKnownFacts()
		{
			const string GatewayUserID = "HALPH9JG7M1M";
			const string GatewayPassword = "lorna2001";
			
			BOParty NewParty = BOParty.RetrievePartyByKnownFacts(GatewayUserID, GatewayPassword);

			Assert.IsNotNull(NewParty);
		}
	}
}
