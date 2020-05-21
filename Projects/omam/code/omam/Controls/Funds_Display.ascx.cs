using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using EntitySpaces.Interfaces;

public partial class Controls_Funds_Display : UserControl
{
    private const int ALL_CATS_VALUE = -1;
    private const string CAT_QUERYSTRING = "categoryid";
    private const int portalId = 1;
    private const string spImageFolder = @"~\uploads\fundImages\";
    private const string obsrImageFolder = @"~\uploads\fundImages\";
    private const string citywireImageFolder = @"~\uploads\fundImages\";
    private const int SPRATING_CELL = 4;
    private const int FACTSHEET_CELL = 3;
    private const int OSBRRATING_CELL = 5;
    private const int CWRATING_CELL = 6;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindTabs();
            bindGrid();
        }
    }

    private void setSelectedTabStyle()
    {
        foreach (ListItem listItem in CategoriesList.Items)
        {
            listItem.Attributes["class"] = listItem.Selected ? "fundCategorySelected" : "fundCategory";
        }
    }

    private void bindTabs()
    {
        //load categories for this portal
        VwPortalCategoriesCollection vwFundPricesCollection = new VwPortalCategoriesCollection();
        //added an order by as the view doesn't seem to do it!!
        vwFundPricesCollection.Query.Where(vwFundPricesCollection.Query.PortalId.Equal(portalId));
        vwFundPricesCollection.Query.Load();

        CategoriesList.Items.Clear();

        //add items plus "All"
        const string valueFormat = "{0}?" + CAT_QUERYSTRING + "={1}";
        ListItem item = new ListItem("All", string.Format(valueFormat, Request.Url.AbsolutePath, ALL_CATS_VALUE));
        int categoryId = getCategoryId();
        item.Selected = categoryId == ALL_CATS_VALUE;
        CategoriesList.Items.Add(item);

        foreach (VwPortalCategories categories in vwFundPricesCollection)
        {
            item =
                new ListItem(categories.Category, string.Format(valueFormat, Request.Url.AbsolutePath, categories.Id));
            item.Selected = categories.Id == categoryId;
            CategoriesList.Items.Add(item);
        }

        setSelectedTabStyle();
    }

    private void bindGrid()
    {
        VwFundsDisplayCollection fundsDisplayCollection = new VwFundsDisplayCollection();
        //added an order by as the view doesn't seem to do it!!
        fundsDisplayCollection.Query.OrderBy("FundName", esOrderByDirection.Ascending);
        fundsDisplayCollection.Query.Where(fundsDisplayCollection.Query.PortalId.Equal(portalId));
        int categoryId = getCategoryId();
        if (categoryId != ALL_CATS_VALUE)
        {
            fundsDisplayCollection.Query.Where(fundsDisplayCollection.Query.CategoryId.Equal(categoryId));
        }

        fundsDisplayCollection.Query.Load();

        GridViewFunds.DataSource = fundsDisplayCollection;
        GridViewFunds.DataBind();
    }

    private int getCategoryId()
    {
        string queryString = Request.QueryString[CAT_QUERYSTRING];
        int categoryId = -1;
        if (!string.IsNullOrEmpty(queryString))
        {
            int.TryParse(queryString, out categoryId);
        }
        return categoryId;
    }

    protected void GridViewFunds_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if not a header or footer etc
        if(e.Row.RowType==DataControlRowType.DataRow)
        {
            //convert to current rows dataitem to funddetails
            VwFundsDisplay vwFundsDisplay = e.Row.DataItem as VwFundsDisplay;

            if (vwFundsDisplay != null)
            {
                //add the factsheet controls
                if (!string.IsNullOrEmpty(vwFundsDisplay.FactsheetFile) &&
                    !string.IsNullOrEmpty(vwFundsDisplay.FactsheetURL))
                {
                    Image factsheetImage = new Image();
                    factsheetImage.CssClass = "FundDisplayFactsheet";
                    factsheetImage.ID = "ImageFactsheet";

                    HyperLink factsheetHyperlink = new HyperLink();
                    factsheetHyperlink.NavigateUrl = "../ViewPDF.aspx?fundid=" + vwFundsDisplay.FundId;
                    factsheetHyperlink.Target = "_blank";
                    factsheetHyperlink.Controls.Add(factsheetImage);
                    e.Row.Cells[FACTSHEET_CELL].Controls.Add(factsheetHyperlink);
                }

                //add the sp rating
                if (!string.IsNullOrEmpty(vwFundsDisplay.SPRatingURL) )
                {
                    Image imgSPRating = new Image();
                    imgSPRating.ImageUrl = spImageFolder+ vwFundsDisplay.SPRatingURL;
                    imgSPRating.ID = "imgSPRating";
                    e.Row.Cells[SPRATING_CELL].Controls.Add(imgSPRating);
                }

                //add the obsr rating
                if (!string.IsNullOrEmpty(vwFundsDisplay.OBSRRatingURL))
                {
                    Image imgOBSRRating = new Image();
                    imgOBSRRating.ImageUrl = obsrImageFolder + vwFundsDisplay.OBSRRatingURL;
                    imgOBSRRating.ID = "imgOBSRRating";
                    e.Row.Cells[OSBRRATING_CELL].Controls.Add(imgOBSRRating);
                }

                //add the citywire rating
                if (!string.IsNullOrEmpty(vwFundsDisplay.CityWireRatingURL))
                {
                    Image imgCWRating = new Image();
                    imgCWRating.ImageUrl = citywireImageFolder + vwFundsDisplay.CityWireRatingURL;
                    imgCWRating.ID = "imgCWRating";
                    e.Row.Cells[CWRATING_CELL].Controls.Add(imgCWRating);
                }
            }
        }
    }
    protected void GridViewFunds_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //add an extra header row to put the grouping in
            GridViewRow newHeader = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);

            TableHeaderCell tableHeaderCell = new TableHeaderCell();
            tableHeaderCell.ColumnSpan = 4; 
            newHeader.Cells.Add(tableHeaderCell);

            tableHeaderCell = new TableHeaderCell();
            tableHeaderCell.ColumnSpan = 3;
            tableHeaderCell.CssClass = "FundDisplayIndependentRatings";
            tableHeaderCell.Text = "Independent Ratings";
            newHeader.Cells.Add(tableHeaderCell);

            tableHeaderCell = new TableHeaderCell();
            tableHeaderCell.ColumnSpan = 1;
            newHeader.Cells.Add(tableHeaderCell);

            GridViewFunds.Controls[0].Controls.AddAt(0, newHeader);
        }

    }
}