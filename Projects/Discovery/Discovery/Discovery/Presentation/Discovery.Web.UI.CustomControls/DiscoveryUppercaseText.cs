using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Discovery.Web.UI.CustomControls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:DiscoveryUppercaseText runat=server></{0}:DiscoveryUppercaseText>")]
    public class DiscoveryUppercaseText : TextBox
    {
        protected override void OnLoad(EventArgs e)
        {
            // Register the client side jabascript
            if (!this.Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "UppercaseText"))
            {
                this.Page.ClientScript.RegisterClientScriptBlock(
                            this.GetType(),
                            "UppercaseText",
                            "<script type=\"text/javascript\">function setUpperCase(val){val.value = val.value.toUpperCase();}</script>");
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            // Add the on key down attribute
            this.Attributes.Add("onkeyup", "setUpperCase(this)");

            // Call base pre render, etc
            base.OnPreRender(e);
        }
    }
}
