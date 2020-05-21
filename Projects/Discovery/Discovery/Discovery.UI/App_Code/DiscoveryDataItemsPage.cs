using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Discovery.Web.UI.CustomControls;

namespace Discovery.UI.Web
{
    /*************************************************************************************************
   ** CLASS:	DiscoveryDataItemsPage
   **
   ** OVERVIEW:
   ** 
   ** This is a base class that any data maintenance screen which shows a grid of items to maintain should
   ** inherit from.
   **
   ** MODIFICATION HISTORY:
   **
   ** Date:		Version:    Who:	Change:
   ** 19/7/06		1.0			PJN		Initial Version
   ************************************************************************************************/

    public abstract partial class DiscoveryDataItemsPage : DiscoveryDataPage
    {
        #region Properties

        /// <summary>
        /// tries to find a hyperlink called "NewButton" which will be 
        /// used to navigate from this spage which a grid of items to a page which will
        /// allow the user to add a new item
        /// </summary>
        /// <value>The new hyperlink.</value>
        private ImageButton NewButton
        {
            get
            {
                //return a null or the hyperlink
                ImageButton buttonNew = null;
                //loop through all controls of the master pages placeholder to find the 
                //new hyperlink
                foreach (Control control in ThisContentPlaceHolder.Controls)
                {
                    if (control is ImageButton && control.ID == "NewButton")
                    {
                        buttonNew = (ImageButton)control;
                        //found the hyperlink so exit loop
                        break;
                    }
                }
                return buttonNew;
            }
        }

        private DiscoveryGrid pageGridView;

        /// <summary>
        /// Gets the pages grid view, no Id is requied as only one grid is currently supported.
        /// </summary>
        /// <value>The page grid view.</value>
        protected DiscoveryGrid PageGridView
        {
            get
            {
                //loop through all controls of the master pages placeholder to find the 
                //grid view
                Control container = ThisContentPlaceHolder;
                FindGrid(container);
                return pageGridView;
            }
        }

        private void FindGrid(Control container)
        {
            foreach (Control control in container.Controls)
            {
                if (control is DiscoveryGrid)
                {
                    pageGridView = control as DiscoveryGrid;
                    //found the gridview so exit loop
                    break;
                }
                else if (control.HasControls())
                {
                    FindGrid(control);
                    if (pageGridView != null)
                    {
                        break;
                    }
                }
            }
        }


        /// <summary>
        /// Gets the data source that the gridview is related to.
        /// </summary>
        /// <value>The page data source.</value>
        protected ObjectDataSource PageDataSource
        {
            get
            {
                ObjectDataSource dataSource = null;
                if (PageGridView != null)
                {
                    dataSource = GetControl<ObjectDataSource>(PageGridView.DataSourceID, ThisContentPlaceHolder);
                }

                return dataSource;
            }
        }

        #endregion

        #region Protected Methods

        protected override void SetEnabledStateOfButtons()
        {
            WebControl newButton = FindControl("NewButton") as WebControl;
            if (newButton != null)
            {
                newButton.Enabled = CanCreate;
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected override void Page_Load(object sender, EventArgs e)
        {
            //call base class
            base.Page_Load(sender, e);


            //set up the url of the hyperlink which adds new items
            ImageButton newButton = NewButton;
            if (newButton != null)
            {
                newButton.Click += new ImageClickEventHandler(newButton_Click);
                newButton.CausesValidation = false;
            }


            if (PageGridView != null && PageDataSource != null && PageGridView.AllowSorting)
            {
                PageDataSource.SortParameterName = "sortExpression";

                if (!IsPostBack)
                {
                    // Sort expression
                    if (Request.QueryString["SortExpression"] != null && Request.QueryString["SortDirection"] != null)
                    {
                        try
                        {
                            PageGridView.Sort(Request.QueryString["SortExpression"],
                                              (SortDirection)Convert.ToInt32(Request.QueryString["SortDirection"]));
                        }
                        catch (Exception ex)
                        {
                            PageGridView.Sort(Request.QueryString["SortExpression"], 0);
                        }
                    }


                    // Page index
                    if (Request.QueryString["PageIndex"] != null)
                    {
                        try
                        {
                            PageGridView.PageIndex = Convert.ToInt32(Request.QueryString["PageIndex"]);
                        }
                        catch (Exception)
                        {
                            PageGridView.PageIndex = 0;
                        }
                    }
                }
            }
        }


        public void newButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(string.Concat(PageGridView.DetailURL, "?UrlReferrer=", PageGridView.UrlReferrer));
        }

        #endregion

   
    }


}