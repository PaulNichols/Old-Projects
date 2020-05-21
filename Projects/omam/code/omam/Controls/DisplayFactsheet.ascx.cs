using System;
using System.IO;
using System.Web.UI;
using BusinessObjects;

public partial class Controls_DisplayPDF : UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string queryString = Request.QueryString["FundId"];
        int fundId = -1;
        if (!string.IsNullOrEmpty(queryString))
        {
            int.TryParse(queryString, out fundId);
        }

        const string FileNotFoundMessage = " <font color='Red' size='2'>File not found.</font>";
        if (fundId > 0)
        {
            OMAMFund omamFund = new OMAMFund();
            if (omamFund.LoadByPrimaryKey(fundId))
            {
                string fileName = Server.MapPath(omamFund.FactsheetURL + "//" + omamFund.FactsheetFile);
                FileInfo file = new FileInfo(fileName);

                // Checking if file exists
                if (file.Exists)
                {
                    Response.Clear();
                    Response.ClearContent();
                    Response.ClearHeaders();
                    //remove the next 3 lines if the open/save prompt is not required
                    //Response.AppendHeader("Content-Disposition", string.Format("attachment; filename={0}", fileName));
                    //Response.Charset = "";
                    //Response.AppendHeader("Content-Length", file.Length.ToString());
                    //----------------------------------------------------------------------
                    Response.ContentType = "application/pdf";
                    Response.WriteFile(file.FullName);
                    Response.End();
                }
                else
                {
                    Response.Write(FileNotFoundMessage);
                }
            }
            else
            {
                Response.Write(FileNotFoundMessage);
            }
        }
        else
        {
            Response.Write(FileNotFoundMessage);
        }
    }
}