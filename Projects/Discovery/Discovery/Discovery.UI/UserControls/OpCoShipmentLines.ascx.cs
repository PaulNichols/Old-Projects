/*************************************************************************************************
 ** FILE:	OpCoShipmentLines.aspx.cs
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
using System.Web.UI.WebControls;


namespace Discovery.UI.Web.UserControls
{
    public partial class OpCoShipmentLines : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Wire up item command for repeater
            discoveryShipmentLines.ItemCommand += new RepeaterCommandEventHandler(discoveryShipmentLines_ItemCommand);

            // Wire up selecting events for sorting
            dataSourceShipmentLines.Selecting += new ObjectDataSourceSelectingEventHandler(dataSourceShipmentLines_Selecting);
        }

        void dataSourceShipmentLines_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            // Sorting
            e.Arguments.SortExpression = string.Concat(discoveryShipmentLines.SortExpression, " ", discoveryShipmentLines.SortDirection.ToString());
        }

        void discoveryShipmentLines_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Sort":
                    {
                        // Bind data
                        discoveryShipmentLines.DataBind();

                        // Done
                        break;
                    }
            }
        }
    }
}