using System;
using System.Web;
using Discovery.BusinessObjects.Controllers;
using Discovery.UI.Web;

public partial class ErrorHandling : DiscoveryDataItemsPage
{

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
    protected override void Page_Load(object sender, EventArgs e)
    {
        ReadRule = "Admin: View Error Handling";
        CreateRule = "Admin: View Error Handling";
        DeleteRule = "Admin: View Error Handling";
        UpdateRule = "Admin: View Error Handling";

        //call base functionality
        base.Page_Load(sender, e);

    }
    protected void GridView1_RowClicked(System.Web.UI.WebControls.DataKey dataKey, int rowIndex)
    {
        Page.Response.Redirect(
               string.Concat("ErrorType.aspx", "?Id=", HttpContext.Current.Server.UrlEncode(dataKey["Id"].ToString()),
                            "&exceptionType=", HttpContext.Current.Server.UrlEncode(dataKey["ExceptionType"].ToString()),
                            "&opcoCode=", HttpContext.Current.Server.UrlEncode(dataKey["OpCoCode"].ToString()),
                            "&policyName=", HttpContext.Current.Server.UrlEncode(dataKey["Policy"].ToString()),
                            "&UrlReferrer=", HttpContext.Current.Server.UrlEncode(GridView1.UrlReferrer)));
    }

    protected void ImageButtonSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        GridView1.Visible = (DropDownListOpCo.SelectedValue != "");
    }

    //protected void GridView1_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
    //{
    //    if (DropDownListOpCo.SelectedValue != "")
    //    {
    //        PageGridView.DataSource =
    //            ErrorTypeController.GetErrorTypes(DropDownListOpCo.SelectedValue, DropDownListPolicy.SelectedValue,
    //                                              e.SortExpression + (e.SortDirection.ToString() == "Ascending" ? "" : " Desc"));
    //        PageGridView.DataBind();
            
    //    }
    //}
}