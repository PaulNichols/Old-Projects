using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using Discovery.Utility;

namespace Discovery.Web.UI.CustomControls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:DiscoveryNullLabel runat=server></{0}:DiscoveryNullLabel>")]
    public class DiscoveryNullLabel : Label
    {
        private string nullText = "";
        private string dataType = "";

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public override string Text
        {
            set
            {
                base.Text = value;
            }

            get
            {
                try
                {
                    if (!String.IsNullOrEmpty(DataType))
                    {
                        // We could use reflection here but this should be cheaper (we only supprt system types)
                        switch (DataType.ToLower())
                        {
                            case "system.datetime":
                            case "datetime":
                                {
                                    return Null.GetNull(Convert.ToDateTime(base.Text), NullText, base.Text).ToString();
                                }
                            case "system.int32":
                            case "int32":
                            case "system.int16":
                            case "int16":
                                {
                                    return Null.GetNull(Convert.ToInt32(base.Text), NullText, base.Text).ToString();
                                }
                            case "system.string":
                                {
                                    return Null.GetNull(base.Text, NullText, base.Text).ToString();
                                }
                            default:
                                {
                                    return base.Text;
                                }
                        }
                    }
                    else
                    {
                        return base.Text;
                    }
                }
                catch
                {
                    return base.Text;
                }
            }
        }

        [Bindable(false)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string NullText
        {
            get
            {
                return nullText;
            }
            set
            {
                nullText = value;
            }
        }

        [Bindable(false)]
        [Category("Data")]
        [DefaultValue("System.String")]
        [Localizable(true)]
        public string DataType
        {
            get
            {
                return dataType;
            }
            set
            {
                dataType = value;
            }
        }
    }
}
