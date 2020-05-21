using System;
using System.Reflection;
using System.Web.UI;

public partial class Main : MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoginName.Visible = Page.User.Identity.IsAuthenticated;
        LoginStatus.Visible = Page.User.Identity.IsAuthenticated;
        LabelVersion.Text = Assembly.GetAssembly(GetType()).GetName().Version.ToString();
        AssemblyCopyrightAttribute assemblyCopyrightAttribute= AssemblyCopyrightAttribute.GetCustomAttribute(Assembly.GetExecutingAssembly(),
                                                                                                             typeof (AssemblyCopyrightAttribute)) as AssemblyCopyrightAttribute;
        if (assemblyCopyrightAttribute!=null)
        {
            HyperLinkCopyright.Text = assemblyCopyrightAttribute.Copyright;
        }
    }
}