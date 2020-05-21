using System;
using System.Collections;
using System.Web.UI.WebControls;
using BusinessObjects;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;

namespace M2.Modules.FundManager
{
    public partial class EditCategories : PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                EntitySpaces.Interfaces.esProviderFactory.Factory = new EntitySpaces.LoaderMT.esDataProviderFactory();

                //if (DotNetNuke.Framework.AJAX.IsInstalled())
                //{
                //    DotNetNuke.Framework.AJAX.RegisterScriptManager();
                //}

                if (categoryId == null)
                {
                    LoadCategories();                                        
                }
                else
                {
                    loadSitesDataGrid();                    
                }
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        protected void loadSitesDataGrid()
        {
            int totalRecords = 0;

            ArrayList _Portals = new ArrayList();

            _Portals = PortalController.GetPortalsByName("%", 0, 100, ref totalRecords);

            gvSites.DataSource = _Portals;
            gvSites.AutoGenerateColumns = false;
            gvSites.AllowPaging = true;
            gvSites.AllowSorting = true;
            gvSites.PageSize = 20;

            //gvSites.Columns.Clear();

            BoundField contentName = new BoundField();
            contentName.DataField = "PortalName";
            contentName.HeaderText = "Micro Site";
            contentName.HeaderStyle.Width = Unit.Pixel(200);
            contentName.HeaderStyle.CssClass = "Abstracts_gvHeaderStyle";
            gvSites.Columns.Add(contentName);

            TemplateField includeSite = new TemplateField();
            includeSite.HeaderText = "Available?";
            includeSite.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            includeSite.HeaderStyle.Width = Unit.Pixel(150);
            includeSite.HeaderStyle.CssClass = "Abstracts_gvHeaderStyle";
            gvSites.Columns.Add(includeSite);

            gvSites.DataKeyNames = new string[] { "PortalId", "PortalName" };

            gvSites.DataBind();
        }

        protected void gvSites_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Add the hyperlink
                DataKey x = gvSites.DataKeys[e.Row.RowIndex];

                //Add the allowed check box                
                CheckBox chkInclude = new CheckBox();
                chkInclude.ID = "chkInclude" + e.Row.RowIndex;

                //Go to the DB to see if this is checked.
                OMAMLnkCategoryPortal objCategoryPortal = new OMAMLnkCategoryPortal();
                objCategoryPortal.Query
                    .Where(objCategoryPortal.Query.CategoryId == categoryId, objCategoryPortal.Query.PortalId == Convert.ToInt32(x[0].ToString()));
                
                objCategoryPortal.Query.Load();
                
                if (objCategoryPortal.PortalId != null)
                {
                    chkInclude.Checked = true;
                }
                else
                {
                    chkInclude.Checked = false;
                }

                e.Row.Cells[1].Controls.Add(chkInclude);
            }
        }
        
        protected void loadDataGrid()
        {
            gvAllNews.DataSource = CategoryList;
            gvAllNews.AutoGenerateColumns = false;
            gvAllNews.AllowPaging = true;
            gvAllNews.AllowSorting = true;
            gvAllNews.PageSize = 10;

            gvAllNews.Columns.Clear();

            //BoundField categoryId = new BoundField();
            //categoryId.DataField = "Id";
            //categoryId.HeaderText = "Category Id";
            //categoryId.HeaderStyle.Width = Unit.Pixel(100);
            //categoryId.HeaderStyle.CssClass = "Abstracts_gvHeaderStyle";
            //gvAllNews.Columns.Add(categoryId);

            BoundField category = new BoundField();
            category.DataField = "FundCategory";
            category.HeaderText = "Category";
            category.HeaderStyle.Width = Unit.Pixel(100);
            category.HeaderStyle.CssClass = "Abstracts_gvHeaderStyle";
            gvAllNews.Columns.Add(category);

            gvAllNews.DataKeyNames = new string[] { "Id", "FundCategory" };

            gvAllNews.DataBind();
        }

        protected void gvAllNews_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Add the hyperlink
                DataKey x = gvAllNews.DataKeys[e.Row.RowIndex];

                LinkButton lbEditCategory = new LinkButton();
                lbEditCategory.ID = "lnkEditButton" + x[0].ToString();
                lbEditCategory.CommandName = x[1].ToString();
                lbEditCategory.Text = x[1].ToString();
                lbEditCategory.CommandArgument = x[0].ToString();
                lbEditCategory.Command += new CommandEventHandler(CategoryEdit);
                e.Row.Cells[0].Controls.Add(lbEditCategory);

                //HyperLink lnkCategory = new HyperLink();
                //DotNetNuke.Entities.Tabs.TabController tabCtrl = new DotNetNuke.Entities.Tabs.TabController();
                //lnkCategory.NavigateUrl = EditUrl("CategoryID", "", "", "CategoryID=" + x[0].ToString());

                //lnkCategory.Text = x[0].ToString();
                //e.Row.Cells[0].Controls.Add(lnkCategory);
            }
        }

        protected void CategoryEdit(object sender, CommandEventArgs e)
        {
            lnkDeleteCategory.Enabled = true;
            divAddActivity.Visible = true;
            divCategoryList.Visible = false;

            txtCategoryId.Text = e.CommandArgument.ToString();
            categoryId = Convert.ToInt16(txtCategoryId.Text.ToString());
            gvSites.Columns.Clear();
            
            txtCategoryName.Text = e.CommandName.ToString();
            loadSitesDataGrid();
        }

        public OMAMFundCategoryCollection CategoryList
        {
            get
            {
                return (OMAMFundCategoryCollection)ViewState["CategoryList"];
            }
            set
            {
                ViewState["CategoryList"] = value;
            }
        }

        public int? categoryId
        {
            get
            {
                return (int?)ViewState["categoryId"];
            }
            set
            {
                ViewState["categoryId"] = value;
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
                actions.Add(GetNextActionID(), Localization.GetString(ModuleActionType.AddContent, this.LocalResourceFile),
                    ModuleActionType.AddContent, "", "", EditUrl(), false, DotNetNuke.Security.SecurityAccessLevel.Edit,
                     true, false);

                return actions;
            }
        }

        #endregion

        protected void LoadCategories()
        {
            //Now select on this collection 
            OMAMFundCategoryCollection Categories = new OMAMFundCategoryCollection();
            Categories.Query.Select(Categories.Query.Id,
                                   Categories.Query.FundCategory);

            Categories.LoadAll();

            CategoryList = Categories;
            gvAllNews.PageIndex = 0;
            loadDataGrid();
        }

        protected void gvAllNews_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvAllNews_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvAllNews_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void lnkSaveCategory_Click(object sender, EventArgs e)
        {

            if (txtCategoryId.Text.ToString() == string.Empty)
            {
                //This is an add 
                OMAMFundCategory CategoryInfo = new OMAMFundCategory();

                CategoryInfo.FundCategory = txtCategoryName.Text.ToString();
                CategoryInfo.Save();

                //Now add the available site list
                for (int i = 0; i < gvSites.Rows.Count; i++)
                {
                    //CheckBox cbInclude = new CheckBox();
                    CheckBox cbInclude = ((CheckBox)gvSites.Rows[i].Cells[1].FindControl("chkInclude" + i));

                    if (cbInclude.Checked)
                    {
                        OMAMLnkCategoryPortal objCategoryPortal = new OMAMLnkCategoryPortal();

                        DataKey dKey = gvSites.DataKeys[i];
                        objCategoryPortal.PortalId = Convert.ToInt32(dKey[0].ToString());
                        objCategoryPortal.CategoryId = categoryId;

                        objCategoryPortal.Save();
                    }
                }

            }
            else
            {
                //This is an update    
                OMAMFundCategory CategoryInfo = new OMAMFundCategory();

                CategoryInfo.Query.Where(CategoryInfo.Query.Id == Convert.ToInt16(txtCategoryId.Text.ToString()));
                CategoryInfo.Query.Load();
                
                CategoryInfo.Id = Convert.ToInt32(txtCategoryId.Text.ToString());
                CategoryInfo.FundCategory = txtCategoryName.Text.ToString();

                CategoryInfo.Save();

                //Delete all the site contents for this category item and add them again
                OMAMLnkCategoryPortalCollection objCategoryPortals = new OMAMLnkCategoryPortalCollection();
                objCategoryPortals.Query.Where(objCategoryPortals.Query.CategoryId == categoryId);
                objCategoryPortals.Query.Load();
                objCategoryPortals.MarkAllAsDeleted();
                objCategoryPortals.Save();
                
                //Now Add the new site/content list
                //Now add the available site list
                for (int i = 0; i < gvSites.Rows.Count; i++)
                {
                    //CheckBox cbInclude = new CheckBox();
                    CheckBox cbInclude = ((CheckBox)gvSites.Rows[i].Cells[1].FindControl("chkInclude" + i));

                    if (cbInclude.Checked)
                    {
                        OMAMLnkCategoryPortal objCategoryPortal = new OMAMLnkCategoryPortal();

                        DataKey dKey = gvSites.DataKeys[i];
                        objCategoryPortal.PortalId = Convert.ToInt32(dKey[0].ToString());
                        objCategoryPortal.CategoryId = categoryId;

                        objCategoryPortal.Save();
                    }
                }
            }

            divAddActivity.Visible = false;
            divCategoryList.Visible = true;

            LoadCategories();
            categoryId = null;
        }

        protected void lnkDeleteCategory_Click(object sender, EventArgs e)
        {
            OMAMFundCategory categoryInfo = new OMAMFundCategory();
            categoryInfo.LoadByPrimaryKey(Convert.ToInt32(txtCategoryId.Text.ToString()));
            categoryInfo.MarkAsDeleted();
            categoryInfo.Save();

            divAddActivity.Visible = false;
            divCategoryList.Visible = true;
            
            loadDataGrid();
        }

        protected void lnkCancelCategory_Click(object sender, EventArgs e)
        {
            txtCategoryName.Text = string.Empty;
            txtCategoryId.Text = string.Empty;
            
            divAddActivity.Visible = false;
            divCategoryList.Visible = true;

            LoadCategories();
            categoryId = null;
        }

        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            lnkDeleteCategory.Enabled = false;

            txtCategoryName.Text = string.Empty;
            txtCategoryId.Text = string.Empty;
            divAddActivity.Visible = true;
            divCategoryList.Visible = false;
        }

        protected void cmdBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(), true);
        }

        protected void gvSites_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvSites_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvSites_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

    }
}