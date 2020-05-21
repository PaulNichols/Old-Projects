using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using EntitySpaces.Interfaces;

public partial class Controls_OffShorePrice : UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            bindGrid();
    }

    private void bindGrid()
    {
        VwLatestOffShoreFundPricesCollection vwLatestOffShoreFundPricesCollection =
            new VwLatestOffShoreFundPricesCollection();
        vwLatestOffShoreFundPricesCollection.Query.OrderBy("FundName", esOrderByDirection.Ascending);
        vwLatestOffShoreFundPricesCollection.Query.Load();
        GridViewOffShoreFunds.DataSource = vwLatestOffShoreFundPricesCollection;
        GridViewOffShoreFunds.DataBind();
    }

    protected void GridViewOffShoreFunds_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridViewOffShoreFunds.EditIndex = e.NewEditIndex;
        bindGrid();
    }

    protected void GridViewOffShoreFunds_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridViewOffShoreFunds.EditIndex = -1;
        bindGrid();
    }


    protected void GridViewOffShoreFunds_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = GridViewOffShoreFunds.Rows[e.RowIndex];

        if (row != null)
        {
            //OffShoreFundPricesCollection offShoreFundPricesCollection = new OffShoreFundPricesCollection();
            //OffShoreFundPrices offShoreFundPrices = offShoreFundPricesCollection.AddNew();
            OffShoreFundPrices offShoreFundPrices=new OffShoreFundPrices();
            offShoreFundPrices.Price = double.Parse(((TextBox)row.FindControl("TextBoxPrice")).Text);
            offShoreFundPrices.CreatedDate = DateTime.Now;
            offShoreFundPrices.CreatedBy = 1;
            offShoreFundPrices.OMAMFundId = (int)GridViewOffShoreFunds.DataKeys[e.RowIndex].Value;
            offShoreFundPrices.Save();
        }

        GridViewOffShoreFunds.EditIndex = -1;
        bindGrid();
    }
}