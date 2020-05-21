using System;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Discovery.Utility;

namespace Discovery.Web.UI.CustomControls
{
    [ToolboxData("<{0}:DiscoveryPagedRepeater runat=server></{0}:DiscoveryPagedRepeater>")]
    public class DiscoveryPagedRepeater : Repeater
    {
        public SortDirection SortDirection
        {
            get
            {
                if (null != ViewState["SortDirection"])
                {
                    return (SortDirection)ViewState["SortDirection"];
                }
                else
                {
                    return SortDirection.Ascending;
                }
            }
            set
            {
                ViewState["SortDirection"] = value;
            }
        }

        public string SortExpression
        {
            get
            {
                if (null != ViewState["SortExpression"])
                {
                    return (string)ViewState["SortExpression"];
                }
                else
                {
                    return DefaultSortExpression;
                }
            }
            set
            {
                ViewState["SortExpression"] = value;
            }
        }

        public string DefaultSortExpression
        {
            get
            {
                if (null != ViewState["DefaultSortExpression"])
                {
                    return (string)ViewState["DefaultSortExpression"];
                }
                else
                {
                    return "";
                }
            }
            set
            {
                ViewState["DefaultSortExpression"] = value;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Wire up the repeater row created
            this.ItemCreated += new RepeaterItemEventHandler(DiscoveryPagedRepeater_ItemCreated);

            this.ItemCommand += new RepeaterCommandEventHandler(DiscoveryPagedRepeater_ItemCommand);

        }

        void DiscoveryPagedRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Sort":
                    {
                        if (this.SortExpression == e.CommandArgument.ToString())
                        {
                            // Change direction
                            this.SortDirection = (this.SortDirection == SortDirection.Ascending) ? SortDirection.Descending : SortDirection.Ascending;
                        }
                        else
                        {
                            // Store the sort expression
                            this.SortExpression = e.CommandArgument.ToString();

                            // Update direction
                            this.SortDirection = SortDirection.Ascending;
                        }

                        // Done
                        break;
                    }
            }
        }

        void DiscoveryPagedRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Header:
                    {
                        // Add the glyph
                        AddGlyph(e.Item);

                        // Done
                        break;
                    }
            }
        }

        private void AddGlyph(RepeaterItem row)
        {
            // Mark the sort expression column
            string glyphText = "<span title='" + ((SortDirection == SortDirection.Ascending) ? "Ascending" : "Descending")  + "' class='pagerTitleGlyph'>" + ((SortDirection == SortDirection.Ascending) ? "5" : "6") + "</span>";

            // Get the root panel
            Control headerPanel = row.FindControl("panelHeader");

            // Make sure we found the header panel
            if (null != headerPanel)
            {
                // Iterate over the links
                foreach (Control headerLink in headerPanel.Controls)
                {
                    // Make sure it's a link
                    if (headerLink is LinkButton)
                    {
                        // Cast to link
                        LinkButton headerLinkLink = headerLink as LinkButton;
                        // See if we're sorting on this column
                        if (headerLinkLink.CommandName == "Sort" && headerLinkLink.CommandArgument == SortExpression)
                        {
                            // Add the sort glyph
                            headerLinkLink.Text = headerLinkLink.Text + glyphText;
                            // Done
                            break;
                        }
                    }
                }
            }
        }

    }
}