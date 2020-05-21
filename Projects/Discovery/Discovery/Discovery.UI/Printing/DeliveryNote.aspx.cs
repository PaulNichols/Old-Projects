/*************************************************************************************************
 ** FILE:	DeliveryNote.aspx.cs
 ** DATE:	30/05/2006
 ** AUTHOR:	Lee Spring
 **
 **
 ** OVERVIEW:
 **
 ** MODIFICATION HISTORY:
 **
 ** Date:		Version:	Who:	Change:
 ** 30/5/06		1.0		    LAS	    Initial Version
 ************************************************************************************************/
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Discovery.BusinessObjects.Controllers;

public partial class Printing_DeliveryNote : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Print the specified shipment
        //ctrlDeliveryNote.Print(TDCShipmentController.Print(245, 1, Server.MapPath("~/Printing")));
    }
}
