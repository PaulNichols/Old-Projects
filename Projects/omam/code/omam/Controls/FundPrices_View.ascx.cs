using System;
using System.Web.UI;
using BusinessObjects;
using EntitySpaces.Interfaces;

public partial class Controls_FundPrices_View : UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bindOffShoreGrid();

        bindMainFundGrid();
    }

    private void bindMainFundGrid()
    {
        VwFundPricesCollection vwFundPricesCollection=new VwFundPricesCollection();
        //added an order by as the view doesn't seem to do it!!
        vwFundPricesCollection.Query.OrderBy("Description", esOrderByDirection.Ascending);
        vwFundPricesCollection.Query.Load();
        GridView1.DataSource = vwFundPricesCollection;
        GridView1.DataBind();

        //just use the date in the first element as they are all the same
        if (vwFundPricesCollection.Count>0)
        {
            LabelAsOfDate.Text = vwFundPricesCollection[0].AsOf.Value.ToString("dd/MM/yyyy");
        }
    }

    private void bindOffShoreGrid()
    {
        VwLatestOffShoreFundPricesCollection vwLatestOffShoreFundPricesCollection =
            new VwLatestOffShoreFundPricesCollection();
        vwLatestOffShoreFundPricesCollection.LoadAll();
        GridViewOffShoreFunds.DataSource = vwLatestOffShoreFundPricesCollection;
        GridViewOffShoreFunds.DataBind();
    }
}