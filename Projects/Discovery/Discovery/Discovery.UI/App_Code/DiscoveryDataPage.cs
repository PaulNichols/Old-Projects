using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Discovery.UI.Web;
using Discovery.Web.UI.CustomControls;

/*************************************************************************************************
 ** CLASS:	DiscoveryDataPage
 **
 ** OVERVIEW:
 ** This is base class for any pages that view or maintain list or a singular item of data
 **
 ** MODIFICATION HISTORY:
 **
 ** Date:		Version:    Who:	Change:
 ** 19/7/06	    1.0			PJN		Initial Version
 ************************************************************************************************/

public abstract partial class DiscoveryDataPage : DiscoveryPage
{
    #region Fields

    #endregion

    #region Properties
    /// <summary>
    /// Gets the this content place holder.
    /// </summary>
    /// <value>The this content place holder.</value>
    protected Control ThisContentPlaceHolder
    {
        get { return GetControl<ContentPlaceHolder>("ContentPlaceHolder", GetControl<Control>("Form1", Master)); }
    }

    /// <summary>
    /// Sets the back link URL.
    /// </summary>
    protected void SetBackLinkURL()
    {
        // Attempt to find the back link url on the page
        HyperLink lnkBack = GetControlRecursive<HyperLink>("HyperLinkBack", ThisContentPlaceHolder);
        // See if we found it
        if (null != lnkBack)
        {
            // Set the back url
            lnkBack.NavigateUrl = BackUrl;
        }
    }

    protected string BackUrl
    {
        get
        {
            if (ViewState["UrlReferrer"] != null)
            {
                return ViewState["UrlReferrer"].ToString();
            }
            else
            {
                return "";
            }
        }
        set 
        { 
            ViewState.Add("UrlReferrer", value); 
        }
    }

    #endregion

    #region Events
    protected override  void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender,e);
        
        if (!IsPostBack)
        {
            if (Request.QueryString["UrlReferrer"] == null)
            {
                if (Request.UrlReferrer != null)
                {
                    BackUrl = Request.UrlReferrer.ToString();
                }
            }
            else
            {
                BackUrl = Server.UrlDecode(Request.QueryString["UrlReferrer"].ToString());
            }
        }
        PreRender += new EventHandler(DiscoveryDataPage_PreRender);
    }

    void DiscoveryDataPage_PreRender(object sender, EventArgs e)
    {
        SetEnabledStateOfButtons();
          // Attempt to find the back url
            SetBackLinkURL();
    }

    protected abstract void SetEnabledStateOfButtons();

    #endregion
}