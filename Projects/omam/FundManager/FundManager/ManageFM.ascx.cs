using System;
using System.IO;
using System.Web.UI.WebControls;
using BusinessObjects;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using EntitySpaces.Interfaces;

namespace FundManager
{
    public partial class ManageFM : PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                EntitySpaces.Interfaces.esProviderFactory.Factory = new EntitySpaces.LoaderMT.esDataProviderFactory();

                if (DotNetNuke.Framework.AJAX.IsInstalled())
                {
                    DotNetNuke.Framework.AJAX.RegisterScriptManager();
                }

                if (!IsPostBack)
                {
                    populateDropDowns();

                }
                LoadFMs();
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        protected void loadDataGrid()
        {
            gvFM.DataSource = FMList;
            gvFM.AutoGenerateColumns = false;
            gvFM.AllowPaging = true;
            gvFM.AllowSorting = true;
            gvFM.PageSize = 10;

            gvFM.Columns.Clear();

            TemplateField issueName = new TemplateField();
            issueName.HeaderText = "Manager Name";
            issueName.HeaderStyle.Width = Unit.Pixel(150);
            issueName.HeaderStyle.CssClass = "Abstracts_gvHeaderStyle";
            gvFM.Columns.Add(issueName);

            TemplateField fundsManaged = new TemplateField();
            fundsManaged.HeaderText = "Funds Managed";
            fundsManaged.HeaderStyle.Width = Unit.Pixel(100);
            fundsManaged.HeaderStyle.CssClass = "Abstracts_gvHeaderStyle";
            gvFM.Columns.Add(fundsManaged);

            gvFM.DataKeyNames = new string[] { "Id", "FullName" };

            gvFM.DataBind();
        }

        protected void gvFM_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Add the hyperlink
                DataKey x = gvFM.DataKeys[e.Row.RowIndex];

                LinkButton lbEditIssue = new LinkButton();
                lbEditIssue.ID = "lnkEditButton" + x[0].ToString();
                lbEditIssue.CommandName = x[1].ToString();
                lbEditIssue.Text = x[1].ToString();
                lbEditIssue.CommandArgument = x[0].ToString();
                lbEditIssue.Command += new CommandEventHandler(ManagerEdit);
                e.Row.Cells[0].Controls.Add(lbEditIssue);

                //Add this managers assigned funds.
                OMAMVWManagerFundsCollection managerFunds = new OMAMVWManagerFundsCollection();
                managerFunds.Query
                    .Where(managerFunds.Query.ManagerId == Convert.ToInt32(x[0].ToString()))
                    .OrderBy(managerFunds.Query.FundName.Ascending);

                managerFunds.Query.Load();

                DropDownList ddlManagersFunds = new DropDownList();
                ddlManagersFunds.CssClass = "Abstracts_Dropdownlist";
                ddlManagersFunds.ID = "ddlManagersFunds" + e.Row.RowIndex;
                ddlManagersFunds.Width = Unit.Pixel(100);
                
                ddlManagersFunds.DataTextField = "FundName";
                ddlManagersFunds.DataValueField = "FundId";
                ddlManagersFunds.DataSource = managerFunds;

                if (managerFunds.Count > 0)
                {
                    ddlManagersFunds.DataBind();
                }
                else
                {
                    ddlManagersFunds.Items.Add("None Managed");
                }

                e.Row.Cells[1].Controls.Add(ddlManagersFunds);
            }
        }

        protected void ManagerEdit(object sender, CommandEventArgs e)
        {
            lnkDeleteIssue.Enabled = true;            
            divAddActivity.Visible = true;
            divFMGrid.Visible = false;
            
            //get the fund manager details
            OMAMFundManager objManager = new OMAMFundManager();
            objManager.Query.Where(objManager.Query.Id == Convert.ToInt16(e.CommandArgument.ToString()));
            objManager.Query.Load();
            fundManager = objManager;

            txtFMName.Text = e.CommandName.ToString();
            Editor1.Text = objManager.Profile;
            //MTW if (objManager.FMImage != null)
            //{
            //    ddlFundManager.SelectedValue = objManager.FMImage.ToString();
            //}

            //OK, Now get the assigned funds
            OMAMVWManagerFundsCollection fundsAssignedCol = new OMAMVWManagerFundsCollection();
            fundsAssignedCol.Query
                .Select(fundsAssignedCol.Query.FundName, fundsAssignedCol.Query.FundId)
                .OrderBy(fundsAssignedCol.Query.FundName.Ascending)
                .Where(fundsAssignedCol.Query.ManagerId == Convert.ToInt16(e.CommandArgument.ToString()));

            fundsAssignedCol.Query.Load();

            cblFundsAssigned.DataTextField = "FundName";
            cblFundsAssigned.DataValueField = "FundId";
            cblFundsAssigned.DataSource = fundsAssignedCol;
            cblFundsAssigned.DataBind();
        }

        protected void LoadFMs()
        {
            //Now select on this collection 
            OMAMFundManagerCollection objManagers = new OMAMFundManagerCollection();

            objManagers.LoadAll();

            FMList = objManagers;
            gvFM.PageIndex = 0;
            loadDataGrid();
        }

        public OMAMFundManagerCollection FMList
        {
            get
            {
                return (OMAMFundManagerCollection)ViewState["FMList"];
            }
            set
            {
                ViewState["FMList"] = value;
            }
        }

        protected void populateDropDowns()
        {
            //OK, Now get the funds available.
            OMAMFundCollection fundCol = new OMAMFundCollection();
            fundCol.Query
                .OrderBy(fundCol.Query.FundName.Ascending);

            fundCol.LoadAll();

            cblFundsAvailable.DataSource = fundCol;
            cblFundsAvailable.DataTextField = "FundName";
            cblFundsAvailable.DataValueField = "Id";
            cblFundsAvailable.DataBind();

            //OK, Now get the Fund Manager Images.
            String sFolderPath = Server.MapPath("Uploads/FundImages");
            DirectoryInfo SPImages = new DirectoryInfo(sFolderPath);
            FileInfo[] SPFiles = SPImages.GetFiles();
            ddlFundManager.DataSource = SPFiles;
            ddlFundManager.DataValueField = "Name";
            ddlFundManager.DataTextField = "Name";
            ddlFundManager.DataBind();
        }

        protected void gvFM_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvFM_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvFM_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void lnkAdd_Click(object sender, EventArgs e)
        {
            //OK, zoom through the selected funds and add them to the selected list box
            foreach (ListItem itm in cblFundsAvailable.Items)
            {
                if (itm.Selected == true)
                {
                    //Check to ensure it doesn't already exist in the cbl
                    if (!cblFundsAssigned.Items.Contains(itm))
                    {
                        //Item is selected and doesn't already exist so add it to the selected cbl
                        itm.Selected = false;
                        cblFundsAssigned.Items.Add(itm);
                    }
                }
            }
        }

        protected void lnkRemove_Click(object sender, EventArgs e)
        {
            //OK, zoom through the selected funds and add them to the selected list box
            for (int i = 0; i < cblFundsAssigned.Items.Count; i++)
            {
                ListItem item = cblFundsAssigned.Items[i];

                if (item.Selected == true)
                {
                    //Item is selected so remove it from the selected cbl
                    cblFundsAssigned.Items.RemoveAt(i);
                    i = i - 1;
                }
            }
        }

        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            lnkDeleteIssue.Enabled = false;
            fundManager = null;
            cblFundsAssigned.Items.Clear();
            
            txtFMName.Text = string.Empty;
            Editor1.Text = string.Empty;
            divAddActivity.Visible = true;
            divFMGrid.Visible = false;
        }

        protected void lnkSaveIssue_Click(object sender, EventArgs e)
        {           
            using (esTransactionScope scope = new esTransactionScope())
            {
                OMAMFundManager objFundManager = new OMAMFundManager();

                if (fundManager == null)
                {
                    //This is an add 
                    objFundManager.FullName = txtFMName.Text.ToString();
                    objFundManager.Profile = Editor1.Text.ToString();
                    objFundManager.CreatedBy = UserId;
                    objFundManager.CreatedDate = DateTime.Now;
                    //MTW objFundManager.FMImage = ddlFundManager.SelectedValue.ToString();

                    objFundManager.Save();
                }
                else
                {
                    //This is an update    
                    objFundManager.Query.Where(objFundManager.Query.Id == Convert.ToInt32(fundManager.Id));
                    objFundManager.Query.Load();

                    objFundManager.FullName = txtFMName.Text.ToString();
                    objFundManager.Profile = Editor1.Text.ToString();
                    //MTW objFundManager.FMImage = ddlFundManager.SelectedValue.ToString();

                    objFundManager.Save();
                }

                //Remove all the assigned funds first
                OMAMLNKManagerFundsCollection objManagerFunds = new OMAMLNKManagerFundsCollection();

                objManagerFunds.Query
                    .Where(objManagerFunds.Query.ManagerId == objFundManager.Id);

                objManagerFunds.Query.Load();
                objManagerFunds.MarkAllAsDeleted();
                objManagerFunds.Save();

                //OK, Now add the newly assigned funds
                for (int i = 0; i < cblFundsAssigned.Items.Count; i++)
                {
                    OMAMLNKManagerFunds assignedFunds = new OMAMLNKManagerFunds();
                    assignedFunds.ManagerId = objFundManager.Id;
                    assignedFunds.FundId = Convert.ToInt16(cblFundsAssigned.Items[i].Value);
                    assignedFunds.Save();
                }

                scope.Complete();

                divAddActivity.Visible = false;
                divFMGrid.Visible = true;
                LoadFMs();
            }
        }

        protected void lnkDeleteIssue_Click(object sender, EventArgs e)
        {

        }

        protected void lnkCancelIssue_Click(object sender, EventArgs e)
        {
            txtFMName.Text = string.Empty;
            Editor1.Text = string.Empty;

            divAddActivity.Visible = false;
            divFMGrid.Visible = true;
        }

        public OMAMFundManager fundManager
        {
            get
            {
                return (OMAMFundManager)ViewState["fundManager"];
            }
            set
            {
                ViewState["fundManager"] = value;
            }
        }

        protected void cmdBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(), true);
        }
    }
}