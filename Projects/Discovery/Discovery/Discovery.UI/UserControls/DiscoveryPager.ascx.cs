/*************************************************************************************************
 ** FILE:	DiscoveryPager.aspx.cs
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
using Discovery.Utility;

namespace Discovery.UI.Web.UserControls
{

    public partial class DiscoveryPager : System.Web.UI.UserControl
    {
        public event EventHandler PageChanged;

        public event EventHandler PageSizeChanged;

        // Maximum pages we can display
        private static int MAXPAGESTODISPLAY = 20;

        public int PageIndex
        {
            get
            {
                if (null != ViewState["PageIndex"])
                {
                    return (int)ViewState["PageIndex"];
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState["PageIndex"] = Math.Max(value, 0);
            }
        }

        public int PageSize
        {
            get
            {
                if (null != ViewState["PageSize"])
                {
                    return (int)ViewState["PageSize"];
                }
                else
                {
                    return 10;
                }
            }
            set
            {
                ViewState["PageSize"] = Math.Max(1, value);
            }
        }

        public int TotalRows
        {
            get
            {
                if (null != ViewState["TotalRows"])
                {
                    return (int)ViewState["TotalRows"];
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                // Store total rows
                ViewState["TotalRows"] = value;

                // Calculate and store total pages
                ViewState["TotalPages"] = Math.Max(0, (value / PageSize)) + (((value % PageSize)>0)?1:0);

                // Make sure page index is valid
                PageIndex = Math.Min(TotalPages, PageIndex);
            }
        }

        public int TotalPages
        {
            get
            {
                if (null != ViewState["TotalPages"])
                {
                    return (int)ViewState["TotalPages"];
                }
                else
                {
                    return 0;
                }
            }
        }

        public int PagesToDisplay
        {
            get
            {
                if (null != ViewState["PagesToDisplay"])
                {
                    return (int)ViewState["PagesToDisplay"];
                }
                else
                {
                    return MAXPAGESTODISPLAY;
                }
            }
            set
            {
                ViewState["PagesToDisplay"] = Math.Min(value, MAXPAGESTODISPLAY);
            }
        }

        public Unit Width
        {
            get
            {
                if (null != ViewState["Width"])
                {
                    return (Unit)ViewState["Width"];
                }
                else
                {
                    return new Unit("100%");
                }
            }
            set
            {
                ViewState["Width"] = value;
            }
        }

        public Unit Height
        {
            get
            {
                if (null != ViewState["Height"])
                {
                    return (Unit)ViewState["Height"];
                }
                else
                {
                    return new Unit("30px");
                }
            }
            set
            {
                ViewState["Height"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Wire up index changed event
            ddlPageSize.SelectedIndexChanged += new EventHandler(ddlPageSize_SelectedIndexChanged);

            // Load any passed query parameters
            if (!IsPostBack)
            {
                // Page index
                PageIndex = Convert.ToInt32(Null.GetNull(Request.QueryString["PageIndex"], PageIndex));

                // Page size
                PageSize = Convert.ToInt32(Null.GetNull(Request.QueryString["PageSize"], PageSize));
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            // Call base
            base.OnPreRender(e);

            // See if we need to display the pager
            if (TotalRows == 0)
            {
                // Hide the footer
                panelPager.Visible = false;
            }
            else
            {
                // Display the footer
                panelPager.Visible = true;

                // Set width
                panelPager.Width = Width;
                panelPager.Height = Height;

                // Display total pages
                lblTotalPages.Text = string.Format("({0} page{1}, {2} results)", TotalPages, ((TotalPages > 1)?"s":""), TotalRows);

                // Page to start on block
                int PageToStartOn = (PageIndex / PagesToDisplay);

                for (int i = 0; i < MAXPAGESTODISPLAY; i++)
                {
                    // Find the link button
                    LinkButton lnkPage = (LinkButton)panelPager.FindControl(string.Concat("lnkPage", (i + 1).ToString()));

                    // See if we need to display the link
                    if (i < PagesToDisplay && ((PageToStartOn * PagesToDisplay) + i) < TotalPages)
                    {
                        // Display the link
                        lnkPage.Visible = true;

                        // Link to page
                        lnkPage.Text = Convert.ToString((PageToStartOn * PagesToDisplay) + (i + 1));
                        lnkPage.Enabled = ((PageToStartOn * PagesToDisplay) + i) != PageIndex;
                        lnkPage.CommandName = "Page";
                        lnkPage.CommandArgument = Convert.ToString((PageToStartOn * PagesToDisplay) + i);
                    }
                    else
                    {
                        // Hide the link
                        lnkPage.Visible = false;
                    }
                }

                // See if we need to add the previous link
                if (PageToStartOn > 0)
                {
                    lnkPrevious.Visible = true;
                    lnkPrevious.CommandName = "Page";
                    lnkPrevious.CommandArgument = Convert.ToString((PageToStartOn - 1) * PagesToDisplay);
                }
                else
                {
                    // Hide the previous link
                    lnkPrevious.Visible = false;
                }

                // See if we need to add the next link
                if (((PageToStartOn * PagesToDisplay) + PagesToDisplay) < TotalPages)
                {
                    lnkNext.Visible = true;
                    lnkNext.CommandName = "Page";
                    lnkNext.CommandArgument = Convert.ToString((PageToStartOn * PagesToDisplay) + PagesToDisplay);
                }
                else
                {
                    // Hide the more link
                    lnkNext.Visible = false;
                }

                // Select the page size
                ddlPageSize.SelectedIndex = -1;
                try
                {
                    ddlPageSize.Items.FindByText(PageSize.ToString()).Selected = true;
                }
                catch
                {
                }
            }
        }

        protected void linkPage_Click(object sender, EventArgs e)
        {
            // Page link was clicked, raise event
            PageIndex = Convert.ToInt32((sender as LinkButton).CommandArgument);

            // Fire page changed event
            if (null != PageChanged)
            {
                PageChanged(this, new EventArgs());
            }
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set page size
            this.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);

            // Go to first page
            this.PageIndex = 0;

            // Fire page changed event
            if (null != PageSizeChanged)
            {
                PageSizeChanged(this, new EventArgs());
            }
        }
    }
}