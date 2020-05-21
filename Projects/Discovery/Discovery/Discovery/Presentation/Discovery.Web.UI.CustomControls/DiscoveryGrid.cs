using System;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Discovery.Utility;

namespace Discovery.Web.UI.CustomControls
{
    
    // [DefaultProperty("Text")]
    /// <summary>
    /// A class to control grid movement/selection
    /// </summary>
    [ToolboxData("<{0}:DiscoveryGrid runat=server></{0}:DiscoveryGrid>")]
    public class DiscoveryGrid : GridView
    {
        /// <summary>
        /// Define a delegate for row click event
        /// </summary>
        /// <param name="dataKey"></param>
        /// <param name="rowIndex"></param>
        public delegate void RowClickedDelegate(DataKey dataKey, int rowIndex);

        /// <summary>
        /// Define a row click event
        /// </summary>
        public event RowClickedDelegate RowClicked;

        /// <summary>
        /// Gets or sets the index of the last selected.
        /// </summary>
        /// <value>The index of the last selected.</value>
        protected int LastSelectedIndex
        {
            get
            {
                if (null != ViewState["LastSelectedIndex"])
                {
                    return (int) ViewState["LastSelectedIndex"];
                }
                else
                {
                    return -1;
                }
            }
            set { ViewState["LastSelectedIndex"] = value; }
        }


        //[Bindable(true)]
        //[Category("Appearance")]
        //[DefaultValue("")]
        //[Localizable(true)]
        //public string Text
        //{
        //    get
        //    {
        //        String s = (String)ViewState["Text"];
        //        return ((s == null) ? String.Empty : s);
        //    }

        //    set
        //    {
        //        ViewState["Text"] = value;
        //    }
        //}

        //protected override void RenderContents(HtmlTextWriter output)
        //{
        //    output.Write(Text);
        //}

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.WebControls.GridView.RowCreated"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Web.UI.WebControls.GridViewRowEventArgs"></see> that contains event data.</param>
        protected override void OnRowCreated(GridViewRowEventArgs e)
        {
            base.OnRowCreated(e);

            // Hi lite item
            if (e.Row.RowType == DataControlRowType.DataRow &&
                DataControlRowState.Selected != (e.Row.RowState & DataControlRowState.Selected)) HiLiteRow(e.Row);

            // Lo lite item
            if (e.Row.RowType == DataControlRowType.DataRow &&
                DataControlRowState.Selected == (e.Row.RowState & DataControlRowState.Selected)) LoLiteRow(e.Row);

            // Add support for sorting glyph
            if (e.Row.RowType == DataControlRowType.Header) AddGlyph(e.Row);
        }

        private void AddGlyph(GridViewRow row)
        {
            // See if we need to use the default sort expression
            string tmpSortExpr = (string.IsNullOrEmpty(SortExpression)) ? DefaultSortExpression : SortExpression;

            // Mark the sort expression column
            Label glyph = new Label();
            // Mark the sort expression column
            glyph.ToolTip = ((SortDirection == SortDirection.Ascending) ? "Ascending" : "Descending");
            glyph.EnableTheming = false;
            glyph.Font.Name = "webdings";
            glyph.Font.Size = FontUnit.Small;
            glyph.Text = ((SortDirection == SortDirection.Ascending) ? "5" : "6");

            // Find the column we're sorting by
            for (int i = 0; i < Columns.Count; i++)
            {
                string colSortExpr = Columns[i].SortExpression;
                if (!string.IsNullOrEmpty(colSortExpr) && colSortExpr == tmpSortExpr)
                {
                    row.Cells[i].Controls.Add(glyph);
                    break;
                }
            }
        }

        private void HiLiteRow(GridViewRow row)
        {
            if (DoHiLiteRow)
            {
                // Change cursor to hand
                row.Style["cursor"] = "pointer";

                // The 50% color of the background
                Color BackColorLight = Color.Empty;
                Color BackColor = Color.Empty;

                // Get the back color
                BackColor = ((row.RowState == DataControlRowState.Alternate)
                                 ? AlternatingRowStyle.BackColor
                                 : RowStyle.BackColor);
                // Generate a light version
                BackColorLight = RGBHSL.ModifyBrightness(BackColor, .8);

                // Add the mouse over and mouse out javascript events here
                row.Attributes.Add("onmouseover",
                                   "javascript:this.style.backgroundColor = '" + ColorTranslator.ToHtml(BackColorLight) +
                                   "';");
                row.Attributes.Add("onmouseout",
                                   "javascript:this.style.backgroundColor = '" + ColorTranslator.ToHtml(BackColor) +
                                   "';");

                //set up the post back for a row when it is clicked
                row.Attributes.Add("onclick",
                                   Page.ClientScript.GetPostBackEventReference(this, "Select$" + row.RowIndex.ToString()));
            }
        }

        private bool doHiLiteRow = true;

        public bool DoHiLiteRow
        {
            get { return doHiLiteRow; }
            set { doHiLiteRow = value; }
        }

        private void LoLiteRow(GridViewRow row)
        {
            // Change cursor to default
            row.Style["cursor"] = "default";
            row.Attributes.Remove("onmouseover");
            row.Attributes.Remove("onmouseout");
        }

        /// <summary>
        /// Handles the <see cref="E:System.Web.UI.Control.Load"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> object that contains event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ApplySorting(DefaultSortExpression);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.WebControls.GridView.PageIndexChanged"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains event data.</param>
        protected override void OnPageIndexChanged(EventArgs e)
        {
            base.OnPageIndexChanged(e);

            // Clear selected item
            SelectedIndex = -1;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.WebControls.GridView.SelectedIndexChanged"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains event data.</param>
        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);

            if (!string.IsNullOrEmpty(DetailURL))
            {
                Page.Response.Redirect(
                    string.Concat(DetailURL, "?Id=", HttpContext.Current.Server.UrlEncode(SelectedDataKey.Value.ToString()),
                                "&UrlReferrer=", HttpContext.Current.Server.UrlEncode(UrlReferrer)));
            }

            // Add hi lite to last row
            if (-1 != LastSelectedIndex) HiLiteRow(Rows[LastSelectedIndex]);

            // Remove hi lite from selected row
            if (-1 != SelectedIndex) LoLiteRow(SelectedRow);

            // Store last selected index
            LastSelectedIndex = SelectedIndex;

            // See if we need to fire an event
            if (RowClicked != null)
            {
                RowClicked(SelectedDataKey, SelectedIndex);
            }
        }

        /// <summary>
        /// Gets or sets the detail URL.
        /// </summary>
        /// <value>The detail URL.</value>
        public string DetailURL
        {
            get { return ViewState["detailURL"] == null ? string.Empty : ViewState["detailURL"].ToString(); }
            set { ViewState.Add("detailURL", value); }
        }

        /// <summary>
        /// Gets or sets the URL referrer.
        /// </summary>
        /// <value>The URL referrer.</value>
        public string UrlReferrer
        {
            get
            {
                return Context.Server.UrlEncode(ViewState["UrlReferrer"] == null || string.IsNullOrEmpty(ViewState["UrlReferrer"].ToString())
                           ? Context.Request.Path : Context.Request.Path+ViewState["UrlReferrer"].ToString());
            }
            set
            {
                ViewState.Add("UrlReferrer", value);
              
            }
        }

        /// <summary>
        /// Gets or sets the default sort expression.
        /// </summary>
        /// <value>The default sort expression.</value>
        public string DefaultSortExpression
        {
            get
            {
                return
                    ViewState["defaultSortExpression"] == null
                        ? string.Empty
                        : ViewState["defaultSortExpression"].ToString();
            }
            set
            {
                ViewState.Add("defaultSortExpression", value);
                ApplySorting(value);
            }
        }


        private void ApplySorting(string expression)
        {
            if (AllowSorting && string.IsNullOrEmpty(SortExpression))
            {
                if (!string.IsNullOrEmpty(expression))
                {
                    Sort(expression, SortDirection.Ascending);
                }
            }
        }
    }
}