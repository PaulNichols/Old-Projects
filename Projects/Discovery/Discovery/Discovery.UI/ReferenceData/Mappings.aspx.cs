using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Discovery.BusinessObjects.Controllers;

namespace Discovery.UI.Web.Mapping
{
    /*************************************************************************************************
    ** CLASS:	Mappings
    **
    ** OVERVIEW:
    ** This page allows a user to view all mappings
    **
    ** MODIFICATION HISTORY:
    **
    ** Date:		Version:    Who:	Change:
    ** 19/7/06		1.0			PJN		Initial Version
    ************************************************************************************************/

    public partial class Mappings : DiscoveryDataItemsPage
    {
        #region Properties

        #endregion

        #region Protected Methods

        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected override void Page_Load(object sender, EventArgs e)
        {
            ReadRule = "Reference Data: View Mappings";
            CreateRule = "Reference Data: Edit Mappings";

            //call base class
            base.Page_Load(sender, e);
            if (!IsPostBack)
            {
                SetControlStateFromQueryString(TextBoxFromValue);
                SetControlStateFromQueryString(TextBoxToValue);
                SetControlStateFromQueryString(PageGridView);
            }
            GridView1.UrlReferrer = "?"+Request.QueryString;
        }

         protected void DropDownList_DataBound(object sender, EventArgs e)
        {
             SetControlStateFromQueryString(sender as Control);
        }

        private MappingSearchParams searchParams;

        private void SetControlStateFromQueryString(Control control)
        {
            if (control!=null)
            {
                searchParams = GetParamsFromQueryString();
                ListItem listItem = null;

                if (control.ID == DropDownListSourceSystems.ID) listItem = DropDownListSourceSystems.Items.FindByValue(searchParams.SourceSystem);
                if (control.ID == DropDownListDestSystems.ID) listItem = DropDownListDestSystems.Items.FindByValue(searchParams.DestinationSystem);
                if (control.ID == DropDownListSourceTypes.ID) listItem = DropDownListSourceTypes.Items.FindByValue(searchParams.SourceType);
                if (control.ID == DropDownListDestTypes.ID) listItem = DropDownListDestTypes.Items.FindByValue(searchParams.DestinationType);
                if (control.ID == DropDownListSourceProperty.ID) listItem = DropDownListSourceProperty.Items.FindByValue(searchParams.SourceProperty);
                if (control.ID == DropDownListDestProperty.ID) listItem = DropDownListDestProperty.Items.FindByValue(searchParams.DestinationProperty);
                if (control.ID == TextBoxFromValue.ID) TextBoxFromValue.Text = searchParams.FromValue;
                if (control.ID == TextBoxToValue.ID) TextBoxToValue.Text = searchParams.ToValue;
                if (control.ID == PageGridView.ID) PageGridView.PageIndex = searchParams.PageIndex;
                if (listItem != null) listItem.Selected = true;
            }
        }

        #endregion



        protected void ImageButtonSearch_Click(object sender, ImageClickEventArgs e)
        {
            string queryString = ConstructQueryString();
            Response.Redirect(Request.Path+queryString);
        }

        private string ConstructQueryString()
        {
            return string.Concat(
                                 "?SourceSystem=", Server.UrlEncode(DropDownListSourceSystems.SelectedValue),
                                 "&DestSystem=", Server.UrlEncode(DropDownListDestSystems.SelectedValue),
                                 "&SourceType=", Server.UrlEncode(DropDownListSourceTypes.SelectedValue),
                                 "&DestType=", Server.UrlEncode(DropDownListDestTypes.SelectedValue),
                                 "&SourceProperty=", Server.UrlEncode(DropDownListSourceProperty.SelectedValue),
                                 "&DestProperty=", Server.UrlEncode(DropDownListDestProperty.SelectedValue),
                                 "&FromValue=", Server.UrlEncode(TextBoxFromValue.Text),
                                 "&ToValue=", Server.UrlEncode(TextBoxToValue.Text),
                                 "&PageIndex=",PageGridView.PageIndex
                );
        }

        protected void MappingsDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["mappingSearchParams"] =GetParamsFromQueryString();
        }

        private MappingSearchParams GetParamsFromQueryString()
        {
            return new MappingSearchParams(
                Convert.ToInt32(Request.QueryString["PageIndex"]),
                Server.UrlDecode(Request.QueryString["SourceSystem"]),
                Server.UrlDecode(Request.QueryString["DestSystem"]),
                Server.UrlDecode(Request.QueryString["SourceType"]),
                Server.UrlDecode(Request.QueryString["DestType"]),
                Server.UrlDecode(Request.QueryString["SourceProperty"]),
                Server.UrlDecode(Request.QueryString["DestProperty"]),
                Server.UrlDecode(Request.QueryString["FromValue"]),
                Server.UrlDecode(Request.QueryString["ToValue"])
                );
        }



        protected void GridView1_PageIndexChanged(object sender, EventArgs e)
        {
            string queryString = ConstructQueryString();
            Response.Redirect(Request.Path + queryString);
        }
}
}