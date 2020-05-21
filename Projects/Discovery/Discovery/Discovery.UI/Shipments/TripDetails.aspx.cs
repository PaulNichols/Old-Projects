/*************************************************************************************************
 ** FILE:	TripDetails.aspx.cs
 ** DATE:	30/05/2006
 ** AUTHOR:	Paul Nichols
 **
 **
 ** OVERVIEW:
 **
 ** MODIFICATION HISTORY:
 **
 ** Date:		Version:	Who:	Change:
 ** 30/5/06		1.0		    PJN	    Initial Version
 ************************************************************************************************/
using System;

namespace Discovery.UI.Web.Shipments
{
    public partial class TripDetails : DiscoveryDataItemsPage
    {

        protected override void Page_Load(object sender, EventArgs e)
        {
            ReadRule = "Shipment: View Trip Details";
            base.Page_Load(sender, e);
        }

      
}
}