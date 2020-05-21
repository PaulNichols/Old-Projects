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
    [ToolboxData("<{0}:DiscoveryNumericText runat=server></{0}:DiscoveryNumericRangeText>")]
    public class DiscoveryNumericText : TextBox
    {
        protected override void OnLoad(EventArgs e)
        {
            // Register the client side jabascript
            if (!this.Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "NumericText"))
            {
                this.Page.ClientScript.RegisterClientScriptBlock(
                            this.GetType(),
                            "NumericText",
                            "<script type=\"text/javascript\">" +
                            "function checkIsNumericField(){document.onkeypress = keyDown();}" +
                            "function keyDown(){var keycode =  window.event.keyCode;if ((keycode < 45 || keycode > 57)){return false;}return true;}" +
                            "function resetNumericFields(newValue, fields){for (i = 1; i < resetNumericFields.arguments.length; i++){resetNumericField(newValue, resetNumericFields.arguments[i]);}}" +
                            "function resetNumericField(newValue, field){field.value = newValue;}" +
                            "</script>");
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            // Add the on key down attribute
            this.Attributes.Add("onkeypress", "return keyDown();");
            // Call base pre render, etc
            base.OnPreRender(e);
        }
    }
}
