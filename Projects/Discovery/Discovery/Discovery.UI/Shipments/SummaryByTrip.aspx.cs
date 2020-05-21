using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Discovery.BusinessObjects;
using RJS.Web.WebControl;
/*************************************************************************************************
   ** CLASS:	SummaryByRoute
   **
   ** OVERVIEW:
   ** This page displays all shipments, summarised by route
   **
   ** MODIFICATION HISTORY:
   **
   ** Date:		Version:    Who:	Change:
   ** 3/8/06	1.0			PJN		Initial Version
   ************************************************************************************************/

namespace Discovery.UI.Web.Shipments
{
    public partial class SummaryByTrip : DiscoveryDataItemsPage
    {
        #region Properties

        #endregion

        #region Protected Methods

        #endregion

        #region Events

        protected void ImageButtonSearch_Click(object sender, ImageClickEventArgs e)
        {
            PageGridView.DataBind();
        }

        #endregion

        protected override void Page_Load(object sender, EventArgs e)
        {
            CreateRule = "Shipment: Summary By Trip";
            ReadRule = "Shipment: Summary By Trip";
            UpdateRule = "Shipment: Summary By Trip";
            DeleteRule = "Shipment: Summary By Trip";

            base.Page_Load(sender, e);

            //set the popups theme and script directory
            PopCalendar.JavaScriptCustomPath = "../App_Themes/DiscoveryDefault/";
            if (!IsPostBack)
            {
                //default the dates to today
                TextBoxDateFrom.Text = DateTime.Today.ToShortDateString();
                TextBoxDateTo.Text = TextBoxDateFrom.Text;
            }
        }


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Trip trip = (Trip)e.Row.DataItem;

                // set up the URL to navigate from the vehicle registration to the vehicle screen
                //GetControl<HyperLink>("lnkVehicleRegistration", e.Row).NavigateUrl =
                //    string.Format("ChangeVehicle.aspx?Id={0}", trip.VehicleRegistration);

                GetControl<HyperLink>("lnkTrip", e.Row).NavigateUrl =
                    string.Format("tripdetails.aspx?Id={0}{1}{2}", trip.Id.ToString(), "&UrlReferrer=",
                                  Server.UrlEncode(Request.Url.ToString()));
            }
        }

        protected void DropDownListDeliveryLocation_DataBound1(object sender, EventArgs e)
        {
            SetUserDefaultWarehouse(sender as ListControl, Profile);
        }
    }
}