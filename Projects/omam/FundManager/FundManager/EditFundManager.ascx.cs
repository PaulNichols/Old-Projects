using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using BusinessObjects;
using EntitySpaces.Interfaces;

namespace M2.Modules.FundManager
{
    public partial class EditFundManager : PortalModuleBase
    {

        int itemId = Null.NullInteger;

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
                    
                    if (Request.QueryString["fundId"] != null)
                    {
                        fundId = Int32.Parse(Request.QueryString["fundId"]);
                    }
                    else
                    {
                        fundId = -1;
                    }

                    if (fundId != -1)
                    {
                        //Load the page
                        LoadPage();
                    }
                    else
                    {
                        //This is a new article so initialise everything
                        txtFundCode.Text = string.Empty;
                        txtFundName.Text = string.Empty;
                        edtRiskWarning.Text = string.Empty;
                        edtShortDesc.Text = string.Empty;
                        edtFundAims.Text = string.Empty;
                        edtCityWireRating.Text = string.Empty;
                        edtOBSRRating.Text = string.Empty;
                        edtSPRating.Text = string.Empty;
                        
                        cblCategoriesAssigned.Items.Clear();
                    }
                }
                else
                {
                    //loadSitesDataGrid();
                }
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        protected void populateDropDowns()
        {
            string sFolderPath;

            //OK, Now get the categories available.
            OMAMFundCategoryCollection fundCol = new OMAMFundCategoryCollection();
            fundCol.Query
                .OrderBy(fundCol.Query.FundCategory.Ascending);

            fundCol.LoadAll();

            cblCategoriesAvailable.DataSource = fundCol;
            cblCategoriesAvailable.DataTextField = "FundCategory";
            cblCategoriesAvailable.DataValueField = "Id";
            cblCategoriesAvailable.DataBind();

            //OK, Now get the S&P Rating Images.
            sFolderPath = Server.MapPath(Settings["SPImageFolder"].ToString());
            DirectoryInfo SPImages = new DirectoryInfo(sFolderPath);
            FileInfo[] SPFiles = SPImages.GetFiles();
            ddlSPRating.DataSource = SPFiles;
            ddlSPRating.DataValueField = "Name";
            ddlSPRating.DataTextField = "Name";
            ddlSPRating.DataBind();

            //OK, Now get the OBSR Rating Images.
            sFolderPath = Server.MapPath(Settings["OBSRImageFolder"].ToString());
            DirectoryInfo OBSRImages = new DirectoryInfo(sFolderPath);
            FileInfo[] OBSRFiles = OBSRImages.GetFiles();
            ddlOBSRRating.DataSource = OBSRFiles;
            ddlOBSRRating.DataValueField = "Name";
            ddlOBSRRating.DataTextField = "Name";
            ddlOBSRRating.DataBind();

            //OK, Now get the CityWire Rating Images.
            sFolderPath = Server.MapPath(Settings["CWImageFolder"].ToString());
            DirectoryInfo CityWireImages = new DirectoryInfo(sFolderPath);
            FileInfo[] CWFiles = CityWireImages.GetFiles();
            ddlCitywireRating.DataSource = CWFiles;
            ddlCitywireRating.DataValueField = "Name";
            ddlCitywireRating.DataTextField = "Name";
            ddlCitywireRating.DataBind();

            //OK, Now get the Factsheet files
            sFolderPath = Server.MapPath(Settings["FactsheetFolder"].ToString());
            DirectoryInfo diFactsheets = new DirectoryInfo(sFolderPath);
            FileInfo[] fiFactsheets = diFactsheets.GetFiles();
            ddlFactsheet.DataSource = fiFactsheets;
            ddlFactsheet.DataValueField = "Name";
            ddlFactsheet.DataTextField = "Name";
            ddlFactsheet.DataBind();
            ddlFactsheet.Items.Insert(0, "Please Select");
            ddlFactsheet.SelectedIndex = 0;

            //OK, Now get the Factsheet files
            sFolderPath = Server.MapPath(Settings["ReasonFolder"].ToString());
            DirectoryInfo diReasonFolder = new DirectoryInfo(sFolderPath);
            FileInfo[] fiReasonFolder = diReasonFolder.GetFiles();
            ddlReasons.DataSource = fiReasonFolder;
            ddlReasons.DataValueField = "Name";
            ddlReasons.DataTextField = "Name";
            ddlReasons.DataBind();
            ddlReasons.Items.Insert(0, "Please Select");
            ddlReasons.SelectedIndex = 0;

            //OK, Now get the AnnualReportFolder Files
            sFolderPath = Server.MapPath(Settings["AnnualReportFolder"].ToString());
            DirectoryInfo diAnnualReportFolder = new DirectoryInfo(sFolderPath);
            FileInfo[] fiAnnualReportFolder = diAnnualReportFolder.GetFiles();
            ddlAnnual.DataSource = fiAnnualReportFolder;
            ddlAnnual.DataValueField = "Name";
            ddlAnnual.DataTextField = "Name";
            ddlAnnual.DataBind();
            ddlAnnual.Items.Insert(0, "Please Select");
            ddlAnnual.SelectedIndex = 0;

            //OK, Now get the InterimFolder Files
            sFolderPath = Server.MapPath(Settings["InterimFolder"].ToString());
            DirectoryInfo diInterimFolder = new DirectoryInfo(sFolderPath);
            FileInfo[] fiInterimFolder = diInterimFolder.GetFiles();
            ddlInterim.DataSource = fiInterimFolder;
            ddlInterim.DataValueField = "Name";
            ddlInterim.DataTextField = "Name";
            ddlInterim.DataBind();
            ddlInterim.Items.Insert(0, "Please Select");
            ddlInterim.SelectedIndex = 0;

            //OK, Now get the SPReportFolder Files
            sFolderPath = Server.MapPath(Settings["SPReportFolder"].ToString());
            DirectoryInfo diSPReportFolder = new DirectoryInfo(sFolderPath);
            FileInfo[] fiSPReportFolder = diSPReportFolder.GetFiles();
            ddlOBSRReport.DataSource = fiSPReportFolder;
            ddlOBSRReport.DataValueField = "Name";
            ddlOBSRReport.DataTextField = "Name";
            ddlOBSRReport.DataBind();
            ddlOBSRReport.Items.Insert(0, "Please Select");
            ddlOBSRReport.SelectedIndex = 0;

            //OK, Now get the OBSRReportFolder Files
            sFolderPath = Server.MapPath(Settings["OBSRReportFolder"].ToString());
            DirectoryInfo diOBSRReportFolder = new DirectoryInfo(sFolderPath);
            FileInfo[] fiOBSRReportFolder = diOBSRReportFolder.GetFiles();
            ddlSPReport.DataSource = fiOBSRReportFolder;
            ddlSPReport.DataValueField = "Name";
            ddlSPReport.DataTextField = "Name";
            ddlSPReport.DataBind();
            ddlSPReport.Items.Insert(0, "Please Select");
            ddlSPReport.SelectedIndex = 0;

            //OK, Now get the SalesAidFolder Files
            sFolderPath = Server.MapPath(Settings["SalesAidFolder"].ToString());
            DirectoryInfo diSalesAidFolder = new DirectoryInfo(sFolderPath);
            FileInfo[] fiSalesAidFolder = diSalesAidFolder.GetFiles();
            ddlSalesAid.DataSource = fiSalesAidFolder;
            ddlSalesAid.DataValueField = "Name";
            ddlSalesAid.DataTextField = "Name";
            ddlSalesAid.DataBind();
            ddlSalesAid.Items.Insert(0, "Please Select");
            ddlSalesAid.SelectedIndex = 0;

            //OK, Now get the TermsheetFolder Files
            sFolderPath = Server.MapPath(Settings["TermsheetFolder"].ToString());
            DirectoryInfo diTermsheetFolder = new DirectoryInfo(sFolderPath);
            FileInfo[] fiTermsheetFolder = diTermsheetFolder.GetFiles();
            ddlTermsheet.DataSource = fiTermsheetFolder;
            ddlTermsheet.DataValueField = "Name";
            ddlTermsheet.DataTextField = "Name";
            ddlTermsheet.DataBind();
            ddlTermsheet.Items.Insert(0, "Please Select");
            ddlTermsheet.SelectedIndex = 0;
        }
        
        protected void LoadPage()
        {
            //show the text
            OMAMFund objFund = new OMAMFund();
            objFund.Query
                .Where(objFund.Query.Id == fundId);
            objFund.Query.Load();

            if (objFund != null)
            {
                txtFundCode.Text = objFund.FundCode.ToString();
                txtFundName.Text = objFund.FundName.ToString();

                if (objFund.FundSnippet!=null)
                {
                    edtShortDesc.Text = objFund.FundSnippet.ToString();                    
                }

                if (objFund.RiskWarning != null)
                {
                    edtRiskWarning.Text = objFund.RiskWarning.ToString();
                }

                if (objFund.FundAims != null)
                {
                    edtFundAims.Text = objFund.FundAims.ToString();
                }

                if (objFund.CityWitreRatingCopy != null)
                {
                    edtCityWireRating.Text = objFund.CityWitreRatingCopy.ToString();
                }

                if (objFund.OBSRRatingCopy != null)
                {
                    edtOBSRRating.Text = objFund.OBSRRatingCopy.ToString();
                }

                if (objFund.SPRatingCopy != null)
                {
                    edtSPRating.Text = objFund.SPRatingCopy.ToString();
                }

                if (objFund.SPRatingURL != null)
                {
                    ddlSPRating.SelectedValue = objFund.SPRatingURL.ToString();                    
                }

                if (objFund.AnnualReportURL != null)
                {
                    ddlAnnual.SelectedValue = objFund.AnnualReportFile.ToString();
                }

                if (objFund.FactsheetURL != null)
                {
                    ddlFactsheet.SelectedValue = objFund.FactsheetFile.ToString();                    
                }

                if (objFund.InterimReportURL != null)
                {
                    ddlInterim.SelectedValue = objFund.InterimReportFile.ToString();                    
                }

                if (objFund.OBSRReportURL != null)
                {
                    ddlOBSRReport.SelectedValue = objFund.OBSRReportFile.ToString();
                }

                if (objFund.ReasonsWhyURL != null)
                {
                    ddlReasons.SelectedValue = objFund.ReasonsWhyFile.ToString();
                }

                if (objFund.SalesAidURL != null)
                {
                    ddlSalesAid.SelectedValue = objFund.SalesAidFile.ToString();
                }

                if (objFund.SPReportURL != null)
                {
                    ddlSPReport.SelectedValue = objFund.SPReportFile.ToString();
                }

                if (objFund.TermsheetURL != null)
                {
                    ddlTermsheet.SelectedValue = objFund.TermsheetFile.ToString();
                }


                //Get the assigned funds
                OMAMVWAssignedCategoriesCollection objAssignedCategories = new OMAMVWAssignedCategoriesCollection();
                objAssignedCategories.Query
                    .Where(objAssignedCategories.Query.FundId == fundId)
                    .OrderBy(objAssignedCategories.Query.FundCategory.Ascending);

                objAssignedCategories.Query.Load();
                assignedCategories = objAssignedCategories;

                cblCategoriesAssigned.DataTextField = "FundCategory";
                cblCategoriesAssigned.DataValueField = "CategoryId";
                cblCategoriesAssigned.DataSource = objAssignedCategories;
                cblCategoriesAssigned.DataBind();

                Fund = objFund;
            }
        }

        #region IActionable Members

        public ModuleActionCollection ModuleActions
        {
            get
            {
                //create a new action to add an item, this will be added to the controls
                //dropdown menu
                ModuleActionCollection actions = new ModuleActionCollection();
                //actions.Add(GetNextActionID(), Localization.GetString(ModuleActionType.AddContent, this.LocalResourceFile),
                //    ModuleActionType.AddContent, "", "", EditUrl(), false, SecurityAccessLevel.Edit,
                //     true, false);

                return actions;
            }
        }

        #endregion

        public int? fundId
        {
            get
            {
                return (int)ViewState["fundId"];
            }
            set
            {
                ViewState["fundId"] = value;
            }
        }

        public OMAMFundCategoryCollection FundCategories
        {
            get
            {
                return (OMAMFundCategoryCollection)ViewState["FundCategories"];
            }
            set
            {
                ViewState["FundCategories"] = value;
            }
        }

        public OMAMVWAssignedCategoriesCollection assignedCategories
        {
            get
            {
                return (OMAMVWAssignedCategoriesCollection)ViewState["assignedCategories"];
            }
            set
            {
                ViewState["assignedCategories"] = value;
            }
        }

        public OMAMFund Fund
        {
            get
            {
                return (OMAMFund)ViewState["Fund"];
            }
            set
            {
                ViewState["Fund"] = value;
            }
        }

        protected void cmdBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(), true);
        }

        protected void cmdSave_Click(object sender, EventArgs e)
        {
            //Add the news item
            using (esTransactionScope scope = new esTransactionScope())
            {
                if (fundId == -1)
                {
                    
                    //New Item
                    OMAMFund fundDetail = new OMAMFund();
                    fundDetail.FundCode = txtFundCode.Text.ToString();
                    fundDetail.FundName = txtFundName.Text.ToString();
                    fundDetail.FundSnippet = edtShortDesc.Text.ToString();
                    fundDetail.RiskWarning = edtRiskWarning.Text.ToString();
                    fundDetail.FundAims = edtFundAims.Text.ToString();
                    fundDetail.CreatedBy = UserId;
                    fundDetail.CreatedDate = DateTime.Now;
                    fundDetail.SPRatingURL = ddlSPRating.SelectedValue.ToString();
                    fundDetail.OBSRRatingURL = ddlOBSRRating.SelectedValue.ToString();
                    fundDetail.CityWireRatingURL = ddlCitywireRating.SelectedValue.ToString();
                    fundDetail.CityWitreRatingCopy = edtCityWireRating.Text.ToString();
                    fundDetail.OBSRRatingCopy = edtOBSRRating.Text.ToString();
                    fundDetail.SPRatingCopy = edtSPRating.Text.ToString();

                    if (ddlAnnual.SelectedIndex > 0)
                    {
                        fundDetail.AnnualReportFile = ddlAnnual.SelectedValue.ToString();
                        fundDetail.AnnualReportURL = Settings["AnnualReportFolder"].ToString();
                   }

                    if (ddlFactsheet.SelectedIndex > 0)
                    {
                        fundDetail.FactsheetFile = ddlFactsheet.SelectedValue.ToString();
                        fundDetail.FactsheetURL = Settings["FactsheetFolder"].ToString();
                    }

                    if (ddlInterim.SelectedIndex > 0)
                    {
                        fundDetail.InterimReportFile = ddlInterim.SelectedValue.ToString();
                        fundDetail.InterimReportURL = Settings["InterimFolder"].ToString();
                    }
                    
                    if (ddlOBSRReport.SelectedIndex > 0)
                    {
                        fundDetail.OBSRReportFile = ddlOBSRReport.SelectedValue.ToString();
                        fundDetail.OBSRReportURL = Settings["OBSRReportFolder"].ToString();
                    }
                    
                    if (ddlReasons.SelectedIndex > 0)
                    {
                        fundDetail.ReasonsWhyFile = ddlReasons.SelectedValue.ToString();
                        fundDetail.ReasonsWhyURL = Settings["ReasonFolder"].ToString();
                    }
                    
                    if (ddlSalesAid.SelectedIndex > 0)
                    {
                        fundDetail.SalesAidFile = ddlSalesAid.SelectedValue.ToString();
                        fundDetail.SalesAidURL = Settings["SalesAidFolder"].ToString();
                    }
                    
                    if (ddlSPReport.SelectedIndex > 0)
                    {
                        fundDetail.SPReportFile = ddlSPReport.SelectedValue.ToString();
                        fundDetail.SPReportURL = Settings["SPReportFolder"].ToString();
                    }

                    if (ddlTermsheet.SelectedIndex > 0)
                    {
                        fundDetail.TermsheetFile = ddlTermsheet.SelectedValue.ToString();
                        fundDetail.TermsheetURL = Settings["TermsheetFolder"].ToString();
                    }

                    fundDetail.Save();

                    fundId = fundDetail.Id;
                }
                else
                {
                    Fund.FundCode = txtFundCode.Text.ToString();
                    Fund.FundName = txtFundName.Text.ToString();
                    Fund.FundSnippet = edtShortDesc.Text.ToString();
                    Fund.RiskWarning = edtRiskWarning.Text.ToString();
                    Fund.FundAims = edtFundAims.Text.ToString();
                    Fund.SPRatingURL = ddlSPRating.SelectedValue.ToString();
                    Fund.OBSRRatingURL = ddlOBSRRating.SelectedValue.ToString();
                    Fund.CityWireRatingURL = ddlCitywireRating.SelectedValue.ToString();
                    Fund.CityWitreRatingCopy = edtCityWireRating.Text.ToString();
                    Fund.OBSRRatingCopy = edtOBSRRating.Text.ToString();
                    Fund.SPRatingCopy = edtSPRating.Text.ToString();

                    if (ddlAnnual.SelectedIndex > 0)
                    {
                        Fund.AnnualReportFile = ddlAnnual.SelectedValue.ToString();
                        Fund.AnnualReportURL = Settings["AnnualReportFolder"].ToString();
                    }

                    if (ddlFactsheet.SelectedIndex > 0)
                    {
                        Fund.FactsheetFile = ddlFactsheet.SelectedValue.ToString();
                        Fund.FactsheetURL = Settings["FactsheetFolder"].ToString();
                    }

                    if (ddlInterim.SelectedIndex > 0)
                    {
                        Fund.InterimReportFile = ddlInterim.SelectedValue.ToString();
                        Fund.InterimReportURL = Settings["InterimFolder"].ToString();
                    }

                    if (ddlOBSRReport.SelectedIndex > 0)
                    {
                        Fund.OBSRReportFile = ddlOBSRReport.SelectedValue.ToString();
                        Fund.OBSRReportURL = Settings["OBSRReportFolder"].ToString();
                    }

                    if (ddlReasons.SelectedIndex > 0)
                    {
                        Fund.ReasonsWhyFile = ddlReasons.SelectedValue.ToString();
                        Fund.ReasonsWhyURL = Settings["ReasonFolder"].ToString();
                    }

                    if (ddlSalesAid.SelectedIndex > 0)
                    {
                        Fund.SalesAidFile = ddlSalesAid.SelectedValue.ToString();
                        Fund.SalesAidURL = Settings["SalesAidFolder"].ToString();
                    }

                    if (ddlSPReport.SelectedIndex > 0)
                    {
                        Fund.SPReportFile = ddlSPReport.SelectedValue.ToString();
                        Fund.SPReportURL = Settings["SPReportFolder"].ToString();
                    }

                    if (ddlTermsheet.SelectedIndex > 0)
                    {
                        Fund.TermsheetFile = ddlTermsheet.SelectedValue.ToString();
                        Fund.TermsheetURL = Settings["TermsheetFolder"].ToString();
                    }

                    Fund.Save();
                }

                //Remove all the assigned categories first
                OMAMLNKFundCategoriesCollection fundCategories = new OMAMLNKFundCategoriesCollection();

                fundCategories.Query
                    .Where(fundCategories.Query.FundId == fundId);

                fundCategories.Query.Load();
                fundCategories.MarkAllAsDeleted();
                fundCategories.Save();

                //OK, Now add the newly assigned funds
                for (int i = 0; i < cblCategoriesAssigned.Items.Count; i++)
                {
                    OMAMLNKFundCategories assignedCategory = new OMAMLNKFundCategories();
                    assignedCategory.CategoryId = Convert.ToInt16(cblCategoriesAssigned.Items[i].Value);
                    assignedCategory.FundId = fundId;
                    assignedCategory.Save();
                }

                scope.Complete();

                Response.Redirect(Globals.NavigateURL(), true);

            }
        }

        protected void lnkAdd_Click(object sender, EventArgs e)
        {
            //OK, zoom through the selected funds and add them to the selected list box
            foreach (ListItem itm in cblCategoriesAvailable.Items)
            {
                if (itm.Selected == true)
                {
                    //Check to ensure it doesn't already exist in the cbl
                    if (!cblCategoriesAssigned.Items.Contains(itm))
                    {
                        //Item is selected and doesn't already exist so add it to the selected cbl
                        itm.Selected = false;
                        cblCategoriesAssigned.Items.Add(itm);
                    }
                }
            }
        }

        protected void lnkRemove_Click(object sender, EventArgs e)
        {
            //OK, zoom through the selected funds and add them to the selected list box
            for (int i = 0; i < cblCategoriesAssigned.Items.Count; i++)
            {
                ListItem item = cblCategoriesAssigned.Items[i];

                if (item.Selected == true)
                {
                    //Item is selected so remove it from the selected cbl
                    cblCategoriesAssigned.Items.RemoveAt(i);
                    i = i - 1;
                }
            }
        }
    }
}