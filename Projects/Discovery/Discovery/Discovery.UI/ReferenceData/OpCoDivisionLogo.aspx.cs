using System;
using System.Web.UI;
using Discovery.BusinessObjects.Controllers;

namespace Discovery.UI.Web.ReferenceData
{
    public partial class OpCoDivisionLogo : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Id"] != null)
            {
                Response.ContentType = "Image/pjpeg";
                BusinessObjects.OpCoDivision opCoDivision =
                    OpcoDivisionController.GetOpCoDivision(Convert.ToInt32(Request.QueryString["Id"]), false);
                if (opCoDivision != null)
                {
                    Response.BinaryWrite(opCoDivision.Logo);
                }
                else
                {
                    Response.Write("No Image found.");
                }
            }
            else
            {
                Response.Write("No Image found.");
            }
        }
    }
}