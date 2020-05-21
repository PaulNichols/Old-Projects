using System;
  using System.Security.Principal;
namespace Discovery.UI.Web
{
    public partial class _Default : DiscoveryPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {      
            CreateRule = "Basic";
            ReadRule = "Basic";
            UpdateRule = "Basic";
            DeleteRule = "Basic";
            base.Page_Load(sender, e);
            
           //WindowsIdentity id =  WindowsIdentity.GetCurrent();
           // Response.Write ("<b>Windows Identity Check</b><br>");
           // Response.Write ("Name: " + id.Name + "<br>"); 
        
      
        }

      
    }
}