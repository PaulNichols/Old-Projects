using System;
using System.Web.UI;

public partial class Default2 : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ErrorLabel.Text = string.Concat("<b>Error occured</b><hr><br>",
                                   "<br><b>Error in: </b>", Server.UrlDecode(Session["Url"].ToString()),
                                   "<br><b>Error Message: </b>",
                                   Server.HtmlDecode(Session["Message"].ToString()),
                                   "<br><b>Stack Trace:</b><br>",
                                   Server.HtmlDecode(Session["Stack"].ToString()));
        
    }
}