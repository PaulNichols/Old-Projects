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

public partial class report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void CrystalReportViewer1_Load(object sender, EventArgs e)
    {
        //CrystalReportSource1.ReportDocument.Subreports[0].Database.Tables[0].ApplyLogOnInfo(
        //    CrystalReportSource1.ReportDocument.Database.Tables[0].LogOnInfo);

    }
}
