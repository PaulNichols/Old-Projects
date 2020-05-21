using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Discovery.BusinessObjects;
using Discovery.BusinessObjects.Controllers;
using Discovery.Utility;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Security;
using Discovery.UI.Web;


/*************************************************************************************************
** CLASS:	DiscoveryPage
**
** OVERVIEW:
** This page acts a a base class for a pages within the Discovery web site.
** All pages MUST inherit from this page.
** This page will mainly enforce authorisation and authentication, as well as handling
** and displaying errors.
**
** MODIFICATION HISTORY:
**
** Date:		Version:    Who:	Change:
** 19/7/06		1.0			PJN		Initial Version
************************************************************************************************/

public abstract partial class DiscoveryPage : Page
{

    // Holders for our rule strings
    private string readRule = "";
    private string createRule = "";
    private string deleteRule = "";
    private string updateRule = "";

    /// <summary>
    /// Generic method to find a control by name or throw an exception if it's missing
    /// </summary>
    /// <param name="controlName">Name of the control.</param>
    /// <returns></returns>
    protected T GetControl<T>(string controlName) where T : Control
    {
        return GetControl<T>(controlName, Page);
    }

    /// <summary>
    /// Gets the control.
    /// </summary>
    /// <param name="controlName">Name of the control.</param>
    /// <param name="container">The container.</param>
    /// <returns></returns>
    public static T GetControl<T>(string controlName, Control container) where T : Control
    {
        T control = null;
        if (container != null)
        {
            control = container.FindControl(controlName) as T;

            if (control == null)
            {
                Debug.Assert(control == null,
                             String.Format(
                                 "The control '{0}' was not found in the container '{1}'. Or is was not of the expected type '{2}'",
                                 controlName, container.GetType().ToString(), typeof(T).ToString()));
            }
        }
        else
        {
            Debug.Assert(container == null, "The Container was null");
        }
        return control;
    }

    public static T GetControlRecursive<T>(string controlName, Control container) where T : Control
    {
        // The control we've found
        T theControl = container.FindControl(controlName) as T;
        // See if we found the control
        if (null == theControl)
        {
            foreach (Control childControl in container.Controls)
            {
                // Attempt to find the control in the container
                theControl = GetControlRecursive<T>(controlName, childControl);
                // See if we found it
                if (null != theControl) break;
            }
        }
        // Done
        return theControl;
    }

    public static T GetControlByType<T>(Control container) where T : Control
    {
        // The control we've found
        T theControl = null;
        // Attempt to find the first control of type T
        foreach (Control childControl in container.Controls)
        {
            if (childControl is T)
            {
                theControl = childControl as T;
                break;
            }
        }
        // See if we found the control
        if (null == theControl)
        {
            foreach (Control childControl in container.Controls)
            {
                // Attempt to find the control in the container
                theControl = GetControlByType<T>(childControl);
                // See if we found it
                if (null != theControl) break;
            }
        }
        // Done
        return theControl;
    }


    protected virtual void Page_Load(object sender, EventArgs e)
    {
        Page.Error += new EventHandler(Page_Error);
        // Page.MaintainScrollPositionOnPostBack = true;

        //check to see if the current page requires Authentictaion (the user to be logged in)
        //if they do require this and are not authenticated then redirect the user to the Login page
        //this code is "belt and braces" as the menu option to access this page should not have 
        //been available. However if a user does try to access a page when the are not logged in then this "should"
        //stop them
        if (RequiresAuthentication() && !User.Identity.IsAuthenticated)
        {
            FormsAuthentication.RedirectToLoginPage();
            return;
        }

        //make a call to the HasPageLoadAuthorisation method to see if the current user has satisfies the
        //authorisation rule which has been assigned to this page in the pageAuthorisation section of the web.config file
        if (!HasPageLoadAuthorisation())
        {
            //if the user does not have required authourisation then display a message and log the security issue, 
            //also the user will be redirected to the home page

            //  DisplayMessage("You do not have the required Authorisation.");
            Response.Redirect("~/Default.aspx", false);
            Exception ex;
            if (Request.QueryString["id"] == null)
            {
                ex = new SecurityException(User.Identity.Name + " has tried to access the page: " + Page.ID +
                                       ", but did not satisfy the page access rule:" +
                                       CreateRule);
            }
            else
            {
                ex = new SecurityException(User.Identity.Name + " has tried to access the page: " + Page.ID +
                                     ", but did not satisfy the page access rule:" +
                                     ReadRule);
            }
            if (ExceptionPolicy.HandleException(ex, "User Interface")) throw ex;
        }
    }

    void Page_Error(object sender, EventArgs e)
    {
        if (Server.GetLastError() == null)
        {
            if (ExceptionPolicy.HandleException(Server.GetLastError(), "User Interface")) DisplayMessage("An unhandled error occured");
            Server.ClearError();
        }
        else
        {
            DisplayMessage("An unhandled error occured");
        }

    }

    /// <summary>
    /// This method can be used to elegently display a message in the UI to the user,
    /// for example a exception or success message.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="type">The type.</param>
    public void DisplayMessage(string message, DiscoveryMessageType type)
    {
        // See if we have a master page
        if (null == Page.Master)
        {
            // Display the message using the "message control" from this page
            DisplayMessage(message, type, Page);
        }
        else
        {
            // Display the message using the "message control" from the master page
            DisplayMessage(message, type, Page.Master);
        }
    }

    /// <summary>
    /// This method can be used to elegently display a message in the UI to the user,
    /// for example a exception or success message.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="type">The type.</param>
    public static void DisplayMessage(string message, DiscoveryMessageType type, Control container)
    {
        // Find the message popup control
        IDiscoveryMessage messagePopup = container.FindControl("MessagePopup") as IDiscoveryMessage;
        // Make sure we have the message interface
        Debug.Assert(null != messagePopup);
        // Display the message
        messagePopup.DisplayMessage(message, type);
    }

    protected void DisplayMessage(Exception ex)
    {
        List<string> messages = new List<string>();

        // See if we have an exception
        if (ex != null)
        {
            if (ex.InnerException != null)
            {
                //messages.Add( ex.InnerException.Message);
                if (ex.InnerException is InValidBusinessObjectException)
                {
                    messages.AddRange(((InValidBusinessObjectException)ex.InnerException).ValidatableObject.ValidationMessages);
                }

                else
                {
                    messages.Add(ex.InnerException.Message);
                }
            }
            else
            {
                messages.Add(ex.Message);

            }
        }

        // See if we have an error message
        if (messages.Count > 0)
        {
            foreach (string message in messages)
            {
                // Display message
                DisplayMessage(message, DiscoveryMessageType.Error);
            }

        }
    }

    /// <summary>
    /// This method can be used to elegently display a message in the UI to the user,
    /// for example a exception or success message.
    /// </summary>
    /// <param name="message">The message.</param>
    public void DisplayMessage(string message)
    {
        // Display an information message
        DisplayMessage(message, DiscoveryMessageType.Information);
    }

    /// <summary>
    /// Determines whether the specified user satisfies rule.
    /// </summary>
    /// <param name="rule">The rule.</param>
    /// <param name="user">The user.</param>
    /// <returns>
    /// 	<c>true</c> if the specified rule has rule; otherwise, <c>false</c>.
    /// </returns>
    public static bool HasRule(string rule, IPrincipal user)
    {
        return HasRules(new string[] { rule }, user);
    }

    /// <summary>
    /// Determines whether the specified user satisfies rule.
    /// </summary>
    /// <param name="rule">The rule.</param>
    /// <returns>
    /// 	<c>true</c> if the specified rule has rule; otherwise, <c>false</c>.
    /// </returns>
    protected bool HasRule(string rule)
    {
        return HasRules(new string[] { rule }, Context.User);
    }

    private static IAuthorizationProvider ruleProvider;

    private static IAuthorizationProvider GetRuleProvider
    {
        get
        {
            if (ruleProvider == null) ruleProvider = CacheManager.Get("RuleProvider") as IAuthorizationProvider;
            if (ruleProvider == null)
            {
                ruleProvider = AuthorizationFactory.GetAuthorizationProvider("RuleProvider");
                CacheManager.Add("RuleProvider", ruleProvider);
            }

            return ruleProvider;
        }
    }

    /// <summary>
    /// Determines whether the specified user satisfies rules.
    /// </summary>
    /// <param name="rules">The rules.</param>
    /// <param name="user">The user.</param>
    /// <returns>
    /// 	<c>true</c> if the specified rules has rules; otherwise, <c>false</c>.
    /// </returns>
    protected static bool HasRules(string[] rules, IPrincipal user)
    {
        bool returnValue = true;
        //check some rules have been passed
        if (rules != null)
        {
            foreach (string rule in rules)
            {
                //check the user against each rule
                try
                {
                    if (!GetRuleProvider.Authorize(user, rule))
                    {
                        //if the user does not satisfy one of the rules then return a false
                        returnValue = false;
                        break;
                    }
                }
                catch
                {
                    returnValue = false;
                    break;
                }
            }
        }
        return returnValue;
    }

    /// <summary>
    /// An overridable method allowing each page to specify if it requires the user to be authenticated
    /// </summary>
    /// <returns></returns>
    protected virtual bool RequiresAuthentication()
    {
        return true;
    }

    /// <summary>
    /// Determines whether the current user has authorisation for the rules specified for this page in the web.config file.
    /// </summary>
    /// <returns>
    /// 	<c>true</c> if [has required authorisation]; otherwise, <c>false</c>.
    /// </returns>
    protected virtual bool HasPageLoadAuthorisation()
    {
        return CanRead;
    }

    /// <summary>
    /// Gets a value indicating whether, based on the pages view rule, the current user can view the page.
    /// </summary>
    /// <value><c>true</c> if this instance can read; otherwise, <c>false</c>.</value>
    protected bool CanRead
    {
        get { return HasRule(ReadRule, Context.User); }
    }

    /// <summary>
    /// Gets a value indicating whether, based on the pages create rule, the current user can create a record.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance can create; otherwise, <c>false</c>.
    /// </value>
    protected bool CanCreate
    {
        get { return HasRule(CreateRule, Context.User); }
    }

    /// <summary>
    /// Gets a value indicating whether, based on the pages update rule, the current user can update.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance can update; otherwise, <c>false</c>.
    /// </value>
    protected bool CanUpdate
    {
        get { return HasRule(UpdateRule, Context.User); }
    }

    /// <summary>
    /// Gets a value indicating whether, based on the pages delete rule, the current user can delete.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance can delete; otherwise, <c>false</c>.
    /// </value>
    protected bool CanDelete
    {
        get { return HasRule(DeleteRule, Context.User); }
    }

    /// <summary>
    /// Gets or sets the read rule.
    /// </summary>
    /// <value>The read rule.</value>
    public string ReadRule
    {
        get { return readRule; }
        set { readRule = value; }
    }

    /// <summary>
    /// Gets or sets the create rule.
    /// </summary>
    /// <value>The create rule.</value>
    public string CreateRule
    {
        get { return createRule; }
        set { createRule = value; }
    }

    /// <summary>
    /// Gets or sets the delete rule.
    /// </summary>
    /// <value>The delete rule.</value>
    public string DeleteRule
    {
        get { return deleteRule; }
        set { deleteRule = value; }
    }

    /// <summary>
    /// Gets or sets the update rule.
    /// </summary>
    /// <value>The update rule.</value>
    public string UpdateRule
    {
        get { return updateRule; }
        set { updateRule = value; }
    }


    ///// <summary>
    ///// Gets the authorisation rules.
    ///// </summary>
    ///// <value>The authorisation rules.</value>
    //protected string[] AuthorisationRules
    //{
    //    get
    //    {
    //        //try to retrieve the security application block rules from the web cache
    //        PageAuthorisationConfiguration pageAuthorisationConfiguration = Discovery.Utility.DataAccess.DataCache.GetCache("PageAuthorisationConfiguration") as PageAuthorisationConfiguration;

    //        if (pageAuthorisationConfiguration == null)
    //        {//if the cache of rules was not found then retrieve them again and place them in the cache

    //            pageAuthorisationConfiguration = PageAuthorisationConfiguration.GetPageAuthorisationConfiguration("pageAuthorisation");

    //            Discovery.Utility.DataAccess.DataCache.SetCache("PageAuthorisationConfiguration", pageAuthorisationConfiguration);
    //        }
    //        string rules;

    //        try
    //        {
    //            //get the rules required to access the current page
    //            rules =
    //                ((PageAuthorisation)
    //                 pageAuthorisationConfiguration.PageAuthorisations[Page.GetType().ToString()]).PageRules;
    //        }
    //        catch (NullReferenceException)
    //        {
    //            //if there's a problem then retrieve the default pages rules
    //            rules =
    //                ((PageAuthorisation)
    //                 pageAuthorisationConfiguration.PageAuthorisations[
    //                     pageAuthorisationConfiguration.DefaultPageAuthorisation]).PageRules;
    //        }

    //        //split the rules into an array to return
    //        string[] authorisationRules = null;
    //        if (rules.Length > 0)
    //            authorisationRules = rules.Split(new char[] { ',' });

    //        return authorisationRules;
    //    }
    //}


    public string GenerateUrlReferrer(string[] queryParams)
    {
        // Call the utility generate url
        return Discovery.Utility.UIHelper.GenerateUrlReferrer(Context, queryParams);
    }

    protected static void SetUserDefaultWarehouse(ListControl listControl, ProfileCommon profile)
    {
        //default the warehouse dropdown to the current users warehouse
        if (profile.WarehouseId > 0)
        {
            ListItem defaultItem = new ListItem();
            if (listControl.DataValueField == "Code")
            {
                Warehouse warehouse = WarehouseController.GetWarehouse(profile.WarehouseId);
                if (warehouse != null)
                {
                    defaultItem = listControl.Items.FindByValue(warehouse.Code);
                }
            }
            else
            {
                defaultItem = listControl.Items.FindByValue(profile.WarehouseId.ToString());
            }
            if (defaultItem != null)
            {
                if (listControl.SelectedItem != null)
                {
                    listControl.SelectedItem.Selected = false;
                }
                defaultItem.Selected = true;
            }
        }

    }
    protected static void SetUserDefaultRegion(ListControl listControl, ProfileCommon profile)
    {
        //default the warehouse dropdown to the current users region
        if (profile.RegionId > 0)
        {
            ListItem defaultItem = new ListItem();
            if (listControl.DataValueField == "Code")
            {
                defaultItem = listControl.Items.FindByValue(profile.RegionCode);
            }
            else
            {
                defaultItem = listControl.Items.FindByValue(profile.RegionId.ToString());
            }
            if (defaultItem != null)
            {
                if (listControl.SelectedItem != null)
                {
                    listControl.SelectedItem.Selected = false;
                }
                defaultItem.Selected = true;
            }
        }

    }
}