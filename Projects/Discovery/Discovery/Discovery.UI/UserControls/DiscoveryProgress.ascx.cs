/*************************************************************************************************
 ** FILE:	DiscoveryProgress.aspx.cs
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
using System.ComponentModel;

namespace Discovery.UI.Web.UserControls
{
    public partial class DiscoveryProgress : System.Web.UI.UserControl
    {
        private string progressMessage = "Please wait, thinking really really hard...";

        public string Message
        {
            set
            {
                progressMessage = value;
            }
            get
            {
                return progressMessage;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}