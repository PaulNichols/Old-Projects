using System;

namespace Discovery.UI.Web.ReferenceData
{
    /*************************************************************************************************
    ** CLASS:	Routes
    **
    ** OVERVIEW:
    ** This page displays all Routes
    **
    ** MODIFICATION HISTORY:
    **
    ** Date:		Version:    Who:	Change:
    ** 19/7/06		1.0			PJN		Initial Version
    ************************************************************************************************/
    public partial class Routes : DiscoveryDataItemsPage
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
            ReadRule = "Reference Data: View Routes";
            CreateRule = "Reference Data: Edit Routes";
            DeleteRule = "Reference Data: Edit Routes";
            UpdateRule = "Reference Data: Edit Routes";

            //call base class
            base.Page_Load(sender, e);

           
        }

        void Page_PreRender(object sender, EventArgs e)
        {
            PageGridView.UrlReferrer = BuildQueryString();
        }

        protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageGridView.UrlReferrer = BuildQueryString();
        }

        #endregion

        private string BuildQueryString()
        {
            string queryString =
                string.Concat("?WarehouseId=", ddlWarehouse.SelectedValue,
                              "&PageIndex=", PageGridView.PageIndex.ToString(),
                              "&SortExpression=", PageGridView.SortExpression,
                              "&SortDirection=", ((int)PageGridView.SortDirection).ToString());
            return queryString;

        }
        protected void ddlWarehouse_DataBound(object sender, EventArgs e)
        {
            if (Request.QueryString["WarehouseId"] != null)
            {
                System.Web.UI.WebControls.ListItem itemToSelect =
                    ddlWarehouse.Items.FindByValue(Request.QueryString["WarehouseId"].ToString());
                if (itemToSelect != null && ddlWarehouse.SelectedItem != null)
                {
                    ddlWarehouse.SelectedItem.Selected = false;
                }
                if (itemToSelect != null)
                {
                    itemToSelect.Selected = true;
                    PageGridView.DataBind();
                    PageGridView.UrlReferrer = BuildQueryString();
                }
            }
        }
    }
}