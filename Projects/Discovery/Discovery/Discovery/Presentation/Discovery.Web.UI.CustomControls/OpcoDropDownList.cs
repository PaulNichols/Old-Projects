using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Discovery.Web.UI.CustomControls
{
    [ToolboxData("<{0}:OpCoDropDownList runat=server UsersOpCo=-1 DataTextField=Code DataValueField=Code ></{0}:OpCoDropDownList>")]
    public class OpCoDropDownList : DropDownList
    {

        ///<summary>
        ///Raises the <see cref="E:System.Web.UI.Control.PreRender"></see> event.
        ///</summary>
        ///
        ///<param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            ListItem item = Items.FindByValue(UsersOpCo);
            if (item != null) item.Selected = true;
            Enabled = (item == null);
        }

        public string UsersOpCo
        {
            get
            {
                if (ViewState != null)
                {
                    return ViewState["UsersOpCo"].ToString();
                }
                else
                {
                    return "-1";
                }
            }
            set { ViewState.Add("UsersOpCo", value); }
        }
    }
}