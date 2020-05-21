using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;
using DotNetNuke.Framework.Providers;
using DotNetNuke.Security.Membership;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using DotNetNuke.Services.Mail;
using BusinessObjects;

namespace M2.Modules.FundManager
{
    public partial class ViewFundManager : PortalModuleBase, IActionable
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                EntitySpaces.Interfaces.esProviderFactory.Factory = new EntitySpaces.LoaderMT.esDataProviderFactory();

                Page.RegisterHiddenField("__EVENTTARGET", btnSearch.UniqueID);

                if (!IsPostBack)
                {
                    populateDropdowns();
                    //loadData();
                }
                else
                {
                    loadDataGrid();
                }
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        protected void loadDataGrid()
        {
            gvFunds.DataSource = FundList;
            gvFunds.AutoGenerateColumns = false;
            gvFunds.AllowPaging = true;
            gvFunds.AllowSorting = true;
            gvFunds.PageSize = 10;

            gvFunds.Columns.Clear();

            BoundField contentName = new BoundField();
            contentName.DataField = "FundName";
            contentName.HeaderText = "Fund Name";
            contentName.HeaderStyle.Width = Unit.Pixel(200);
            contentName.HeaderStyle.CssClass = "Abstracts_gvHeaderStyle";
            gvFunds.Columns.Add(contentName);

            TemplateField FundCategory = new TemplateField();
            FundCategory.HeaderText = "Categories";
            FundCategory.HeaderStyle.Width = Unit.Pixel(100);
            FundCategory.HeaderStyle.CssClass = "Abstracts_gvHeaderStyle";
            gvFunds.Columns.Add(FundCategory);

            TemplateField FundManagers = new TemplateField();
            FundManagers.HeaderText = "Managers";
            FundManagers.HeaderStyle.Width = Unit.Pixel(100);
            FundManagers.HeaderStyle.CssClass = "Abstracts_gvHeaderStyle";
            gvFunds.Columns.Add(FundManagers);

            gvFunds.DataKeyNames = new string[] { "Id", "FundCode", "FundName" };

            gvFunds.DataBind();
        }

        protected void gvFunds_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Add the hyperlink
                DataKey x = gvFunds.DataKeys[e.Row.RowIndex];

                HyperLink lnkFund = new HyperLink();
                DotNetNuke.Entities.Tabs.TabController tabCtrl = new DotNetNuke.Entities.Tabs.TabController();
                lnkFund.NavigateUrl = EditUrl("FundID", "", "", "FundId=" + x[0].ToString());

                lnkFund.Text = x[2].ToString();
                e.Row.Cells[0].Controls.Add(lnkFund);

                //Add this funds assigned categories.
                OMAMVWAssignedCategoriesCollection categoriesCol = new OMAMVWAssignedCategoriesCollection();
                categoriesCol.Query
                    .Where(categoriesCol.Query.FundId == Convert.ToInt32(x[0].ToString()))
                    .OrderBy(categoriesCol.Query.FundCategory.Ascending);

                categoriesCol.Query.Load();

                DropDownList ddlAssignedCategories = new DropDownList();
                ddlAssignedCategories.CssClass = "Abstracts_Dropdownlist";
                ddlAssignedCategories.ID = "ddlAssignedCategories" + e.Row.RowIndex;
                ddlAssignedCategories.Width = Unit.Pixel(150);

                ddlAssignedCategories.DataTextField = "FundCategory";
                ddlAssignedCategories.DataValueField = "CategoryId";
                ddlAssignedCategories.DataSource = categoriesCol;

                if (categoriesCol.Count > 0)
                {
                    ddlAssignedCategories.DataBind();
                }
                else
                {
                    ddlAssignedCategories.Items.Add("None Assigned");
                }

                e.Row.Cells[1].Controls.Add(ddlAssignedCategories);

                //Add this funds assigned managers.
                OMAMVWManagerFundsCollection managersCol = new OMAMVWManagerFundsCollection();
                managersCol.Query
                    .Where(managersCol.Query.FundId == Convert.ToInt32(x[0].ToString()))
                    .OrderBy(managersCol.Query.FullName.Ascending);

                managersCol.Query.Load();

                DropDownList ddlFundManagers = new DropDownList();
                ddlFundManagers.CssClass = "Abstracts_Dropdownlist";
                ddlFundManagers.ID = "ddlFundManagers" + e.Row.RowIndex;
                ddlFundManagers.Width = Unit.Pixel(150);
                
                ddlFundManagers.DataTextField = "FullName";
                ddlFundManagers.DataValueField = "ManagerId";
                ddlFundManagers.DataSource = managersCol;

                if (managersCol.Count > 0)
                {
                    ddlFundManagers.DataBind();
                }
                else
                {
                    ddlFundManagers.Items.Add("None Assigned");
                }

                e.Row.Cells[2].Controls.Add(ddlFundManagers);
            }
        }

        protected void populateDropdowns()
        {

            //Get All Fund Categories
            OMAMFundCategoryCollection objCategoryCol = new OMAMFundCategoryCollection();
            objCategoryCol.Query
                .Select(objCategoryCol.Query.Id, objCategoryCol.Query.FundCategory)
                .OrderBy(objCategoryCol.Query.Id.Ascending);

            objCategoryCol.Query.Load();

            ddlCategories.DataSource = objCategoryCol;
            ddlCategories.DataTextField = "FundCategory";
            ddlCategories.DataValueField = "Id";
            ddlCategories.DataBind();
            
            ddlCategories.Items.Insert(0, "All");

            ddlCategories.SelectedIndex = 0;

            Categories = objCategoryCol;
        }

        public OMAMVWFundMatrixCollection FundList
        {
            get
            {
                return (OMAMVWFundMatrixCollection)ViewState["FundList"];
            }
            set
            {
                ViewState["FundList"] = value;
            }
        }

        public OMAMFundCategoryCollection Categories
        {
            get
            {
                return (OMAMFundCategoryCollection)ViewState["Categories"];
            }
            set
            {
                ViewState["Categories"] = value;
            }
        }

        #region IActionable Members

        public DotNetNuke.Entities.Modules.Actions.ModuleActionCollection ModuleActions
        {
            get
            {
                //create a new action to add an item, this will be added to the controls
                //dropdown menu
                ModuleActionCollection actions = new ModuleActionCollection();

                //create a new action to add the news category item, this will be added to the controls
                //dropdown menu
                actions.Add(GetNextActionID(), "Fund Categories", "", "", "", EditUrl("FundCategories"), false, DotNetNuke.Security.SecurityAccessLevel.Edit, true, false);

                //create a new action to add the news issues item, this will be added to the controls
                //dropdown menu
                actions.Add(GetNextActionID(), "Fund Managers", "", "", "", EditUrl("FundManagers"), false, DotNetNuke.Security.SecurityAccessLevel.Edit, true, false);

                return actions;
            }
        }

        #endregion


        protected void gvFunds_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvFunds_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvFunds_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void ddlCategories_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadData();
        }

        protected void loadData()
        {
            //Now select distinct on this collection 
            OMAMVWFundMatrixCollection objFunds = new OMAMVWFundMatrixCollection();
            objFunds.Query.Select(objFunds.Query.Id,
                                   objFunds.Query.FundCode,
                                   objFunds.Query.FundName,
                                   objFunds.Query.CreatedDate,
                                   objFunds.Query.CreatedBy);

            objFunds.Query.es.Distinct = true;
            
            //Is a category selected?
            if (ddlCategories.SelectedIndex > 0)
            {
                objFunds.Query.Where(objFunds.Query.CategoryId == ddlCategories.SelectedValue);
            }

            objFunds.Query.OrderBy(objFunds.Query.FundName.Ascending);
            objFunds.Query.Load();

            FundList = objFunds;
            gvFunds.PageIndex = 0;
            loadDataGrid();            
        }
        
        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            DotNetNuke.Entities.Tabs.TabController tabCtrl = new DotNetNuke.Entities.Tabs.TabController();
            Response.Redirect(EditUrl("FundId", "", "", "FundId=-1"));
        }
    }
}