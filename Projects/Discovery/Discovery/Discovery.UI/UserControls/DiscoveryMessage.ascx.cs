/*************************************************************************************************
 ** FILE:	DiscoveryMessage.aspx.cs
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
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Diagnostics;
using Discovery.BusinessObjects;
using Discovery.BusinessObjects.Controllers;
using Discovery.Utility;

namespace Discovery.UI.Web.UserControls
{
    public partial class DiscoveryMessage : System.Web.UI.UserControl, IDiscoveryMessage
    {
        public DiscoveryMessage()
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Hide messages by default
            DisplayPopup(false);
        }

        protected override void OnPreRender(EventArgs e)
        {
            // Do we need to hide the messges
            if (tableMessages.Rows.Count == 0)
            {
                // Hide the messages
                DisplayPopup(false);
            }

            // call base
            base.OnPreRender(e);
        }

        public void DisplayPopup(bool show)
        {
            // Show/hide the popup
            panelMessagePopup.Visible = show;
        }

        #region IDiscoveryMessage Members

        public void ClearMessages()
        {
            // Remove all rows
            tableMessages.Rows.Clear();

            // Make sure we hide the message popup
            DisplayPopup(false);
        }

        public void DisplayMessage(string message)
        {
            DisplayMessage(message, DiscoveryMessageType.Information);
        }

        public void DisplayMessage(string message, DiscoveryMessageType type)
        {
            // Generate the table and cells for the message
            HtmlTableRow messageRow = new HtmlTableRow();
            HtmlTableCell iconCell = new HtmlTableCell();
            HtmlTableCell messageCell = new HtmlTableCell();

            // Display properties
            iconCell.VAlign = "middle";
            messageCell.VAlign = "middle";
            messageCell.Width = "100%";

            // Generate the icon
            Image imageIcon = new Image();
            imageIcon.SkinID = string.Concat("Message", type.ToString());

            // Generate the message
            Label labelMessage = new Label();
            labelMessage.SkinID = "MessageLabel";
            labelMessage.Text = message;

            // Add the icon to the cell
            iconCell.Controls.Add(imageIcon);

            // Add the message to the cell
            messageCell.Controls.Add(labelMessage);

            // Add the cells to the row
            messageRow.Cells.Add(iconCell);
            messageRow.Cells.Add(messageCell);

            // Add the row to the table
            tableMessages.Rows.Add(messageRow);

            // Make sure that the message is visible
            DisplayPopup(true);
        }

        #endregion
    }
}