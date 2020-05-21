using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;

namespace YourCompany.Modules.FundDetail
{
    public partial class ViewFundDetail : PortalModuleBase, IActionable
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
                        fundId = Int32.Parse(Request.QueryString["fundId"]);

                        //Load the page
                    }
                    else
                    {
                        //loadSitesDataGrid();
                    }

                    LoadPage();
                }
                catch (Exception ex)
                {
                    Exceptions.ProcessModuleLoadException(this, ex);
                }
            }
        }

        protected void LoadPage()
        {
            //show the text
            OMAMFund objFund = new OMAMFund();
            objFund.Query
                .Where(objFund.Query.Id == fundId);
            objFund.Query.Load();

            lblFundSnippet.Text = objFund.FundSnippet.ToString();
            lblFundAims.Text = objFund.FundAims.ToString();
            
            //Get the fund manager details
            OMAMVWManagerFunds objFundManager = new OMAMVWManagerFunds();
            objFundManager.Query.es.Top = 1;
            
            objFundManager.Query
                .Where(objFundManager.Query.FundId == objFund.Id);
            objFundManager.Query.Load();

            if (objFundManager.Profile != null)
            {
                lblFundManager.Text = objFundManager.Profile.ToString();                            
            }
            
            DisplayLiteratureLinks(objFund);
            DisplayRatings(objFund);
            DisplayRiskWarning(objFund);
            
        }

        protected void DisplayLiteratureLinks(OMAMFund objFund)
        {
            //OK, Now create the relevent Literature Links.
            //This is done by simply looking at each URL. If a value exists for it then a link will be created.
            if (objFund.FactsheetURL != null && objFund.FactsheetURL != string.Empty)
            {
                LinkButton lnkFactSheet = new LinkButton();
                lnkFactSheet.ID = "lnkFactSheet";
                lnkFactSheet.CommandName = objFund.FactsheetFile.ToString();
                lnkFactSheet.Command += new CommandEventHandler(OpenDocument);
                lnkFactSheet.CommandArgument = Server.MapPath(objFund.FactsheetURL.ToString()) + "//" + objFund.FactsheetFile.ToString();
                lnkFactSheet.Text = "Factsheet" + "<br>";
                lnkFactSheet.CssClass = "litItem";

                pnlAssets.Controls.Add(lnkFactSheet);
            }

            if (objFund.ReasonsWhyURL != null && objFund.ReasonsWhyURL != string.Empty)
            {
                LinkButton lnkReasonsWhy = new LinkButton();
                lnkReasonsWhy.ID = "lnkReasonsWhy";
                lnkReasonsWhy.CommandName = objFund.ReasonsWhyFile.ToString();
                lnkReasonsWhy.Command += new CommandEventHandler(OpenDocument);
                lnkReasonsWhy.CommandArgument = Server.MapPath(objFund.ReasonsWhyURL.ToString()) + "//" + objFund.ReasonsWhyFile.ToString();
                lnkReasonsWhy.Text = "Reasons Why" + "<br>";
                lnkReasonsWhy.CssClass = "litItem";

                pnlAssets.Controls.Add(lnkReasonsWhy);
            }

            if (objFund.AnnualReportURL != null && objFund.AnnualReportURL != string.Empty)
            {
                LinkButton lnkAnnualReport = new LinkButton();
                lnkAnnualReport.ID = "lnkAnnualReport";
                lnkAnnualReport.CommandName = objFund.AnnualReportFile.ToString();
                lnkAnnualReport.Command += new CommandEventHandler(OpenDocument);
                lnkAnnualReport.CommandArgument = Server.MapPath(objFund.AnnualReportURL.ToString()) + "//" + objFund.AnnualReportFile.ToString();
                lnkAnnualReport.Text = "Annual Report" + "<br>";
                lnkAnnualReport.CssClass = "litItem";

                pnlAssets.Controls.Add(lnkAnnualReport);
            }

            if (objFund.InterimReportURL != null && objFund.InterimReportURL != string.Empty)
            {
                LinkButton lnkInterimReport = new LinkButton();
                lnkInterimReport.ID = "lnkInterimReport";
                lnkInterimReport.CommandName = objFund.InterimReportFile.ToString();
                lnkInterimReport.Command += new CommandEventHandler(OpenDocument);
                lnkInterimReport.CommandArgument = Server.MapPath(objFund.InterimReportURL.ToString()) + "//" + objFund.InterimReportFile.ToString();
                lnkInterimReport.Text = "Interim Report" + "<br>";
                lnkInterimReport.CssClass = "litItem";

                pnlAssets.Controls.Add(lnkInterimReport);
            }

            if (objFund.SPReportURL != null && objFund.SPReportURL != string.Empty)
            {
                LinkButton lnkSPReportURL = new LinkButton();
                lnkSPReportURL.ID = "lnkSPReportURL";
                lnkSPReportURL.CommandName = objFund.SPReportFile.ToString();
                lnkSPReportURL.Command += new CommandEventHandler(OpenDocument);
                lnkSPReportURL.CommandArgument = Server.MapPath(objFund.SPReportURL.ToString()) + "//" + objFund.SPReportFile.ToString();
                lnkSPReportURL.Text = "S&P Report" + "<br>";
                lnkSPReportURL.CssClass = "litItem";

                pnlAssets.Controls.Add(lnkSPReportURL);
            }

            if (objFund.OBSRReportURL != null && objFund.OBSRReportURL != string.Empty)
            {
                LinkButton lnkOBSRReportURL = new LinkButton();
                lnkOBSRReportURL.ID = "lnkOBSRReportURL";
                lnkOBSRReportURL.CommandName = objFund.SPReportFile.ToString();
                lnkOBSRReportURL.Command += new CommandEventHandler(OpenDocument);
                lnkOBSRReportURL.CommandArgument = Server.MapPath(objFund.SPReportURL.ToString()) + "//" + objFund.SPReportFile.ToString();
                lnkOBSRReportURL.Text = "OBSR Report" + "<br>";
                lnkOBSRReportURL.CssClass = "litItem";

                pnlAssets.Controls.Add(lnkOBSRReportURL);
            }

            if (objFund.SalesAidURL != null && objFund.SalesAidURL != string.Empty)
            {
                LinkButton lnkSalesAidURL = new LinkButton();
                lnkSalesAidURL.ID = "lnkSalesAidURL";
                lnkSalesAidURL.CommandName = objFund.SalesAidFile.ToString();
                lnkSalesAidURL.Command += new CommandEventHandler(OpenDocument);
                lnkSalesAidURL.CommandArgument = Server.MapPath(objFund.SalesAidURL.ToString()) + "//" + objFund.SalesAidFile.ToString();
                lnkSalesAidURL.Text = "Sales Aid" + "<br>";
                lnkSalesAidURL.CssClass = "litItem";

                pnlAssets.Controls.Add(lnkSalesAidURL);
            }

            if (objFund.TermsheetURL != null && objFund.TermsheetURL != string.Empty)
            {
                LinkButton lnkTermsheetURL = new LinkButton();
                lnkTermsheetURL.ID = "lnkTermsheetURL";
                lnkTermsheetURL.CommandName = objFund.TermsheetFile.ToString();
                lnkTermsheetURL.Command += new CommandEventHandler(OpenDocument);
                lnkTermsheetURL.CommandArgument = Server.MapPath(objFund.TermsheetURL.ToString()) + "//" + objFund.TermsheetFile.ToString();
                lnkTermsheetURL.Text = "Termsheet" + "<br>";
                lnkTermsheetURL.CssClass = "litItem";

                pnlAssets.Controls.Add(lnkTermsheetURL);
            }            
        }
        
        protected void DisplayRatings(OMAMFund objFund)
        {
            //OK, Now create the Ratings Images
            //Put them all in one main div

            if (objFund.SPRatingURL != null && objFund.SPRatingURL != string.Empty)
            {
                Image imgSPRating = new Image();
                string strSPImageUrl = Settings["SPImageDisplayFolder"].ToString() + objFund.SPRatingURL.ToString();
                imgSPRating.ImageUrl = strSPImageUrl;
                imgSPRating.Attributes.Add("Runat", "Server");
                imgSPRating.ID = "imgSPRating";

                Panel pnlSPRatingImage = new Panel();
                pnlSPRatingImage.ID = "pnlSPRatingImage";
                pnlSPRatingImage.CssClass = "divSPRatingImage";
                pnlSPRatingImage.Controls.Add(imgSPRating);

                pnlRatings.Controls.Add(pnlSPRatingImage);

                Label lblSPRatingCopy = new Label();
                lblSPRatingCopy.CssClass = "divSPRatingCopy";
                lblSPRatingCopy.Text = objFund.SPRatingCopy.ToString();

                Panel pnlSPRatingCopy = new Panel();
                pnlSPRatingCopy.ID = "pnlSPRatingCopy";
                pnlSPRatingCopy.CssClass = "divSPRatingCopy";
                pnlSPRatingCopy.Controls.Add(lblSPRatingCopy);

                pnlRatings.Controls.Add(pnlSPRatingCopy);                
            }

            if (objFund.OBSRRatingURL != null && objFund.OBSRRatingURL != string.Empty)
            {
                Image imgOBSRRating = new Image();
                string strOBSRImageUrl = Settings["OBSRImageDisplayFolder"].ToString() + objFund.OBSRRatingURL.ToString();
                imgOBSRRating.ImageUrl = strOBSRImageUrl;
                imgOBSRRating.Attributes.Add("Runat", "Server");
                imgOBSRRating.ID = "imgOBSRRating";

                Panel pnlOBSRRatingImage = new Panel();
                pnlOBSRRatingImage.ID = "pnlOBSRRatingImage";
                pnlOBSRRatingImage.CssClass = "divOBSRRatingImage";
                pnlOBSRRatingImage.Controls.Add(imgOBSRRating);

                pnlRatings.Controls.Add(pnlOBSRRatingImage);

                Label lblOBSRRatingCopy = new Label();
                lblOBSRRatingCopy.CssClass = "divSPRatingCopy";
                lblOBSRRatingCopy.Text = objFund.OBSRRatingCopy.ToString();

                Panel pnlOBSRRatingCopy = new Panel();
                pnlOBSRRatingCopy.ID = "pnlOBSRRatingCopy";
                pnlOBSRRatingCopy.CssClass = "divOBSRRatingCopy";
                pnlOBSRRatingCopy.Controls.Add(lblOBSRRatingCopy);

                pnlRatings.Controls.Add(pnlOBSRRatingCopy);
            }

            if (objFund.CityWireRatingURL != null && objFund.CityWireRatingURL != string.Empty)
            {
                Image imgCWRating = new Image();
                string strImageUrl = Settings["CWImageDisplayFolder"].ToString() + objFund.CityWireRatingURL.ToString();
                imgCWRating.ImageUrl = strImageUrl;
                imgCWRating.Attributes.Add("Runat", "Server");
                imgCWRating.ID = "imgCWRating";

                Panel pnlCWRatingImage = new Panel();
                pnlCWRatingImage.ID = "pnlCWRatingImage";
                pnlCWRatingImage.CssClass = "divCWRatingImage";
                pnlCWRatingImage.Controls.Add(imgCWRating);

                pnlRatings.Controls.Add(pnlCWRatingImage);

                Label lblCWRatingCopy = new Label();
                lblCWRatingCopy.CssClass = "divSPRatingCopy";
                lblCWRatingCopy.Text = objFund.CityWitreRatingCopy.ToString();

                Panel pnlCWRatingCopy = new Panel();
                pnlCWRatingCopy.ID = "pnlCWRatingCopy";
                pnlCWRatingCopy.CssClass = "divCWRatingCopy";
                pnlCWRatingCopy.Controls.Add(lblCWRatingCopy);

                pnlRatings.Controls.Add(pnlCWRatingCopy);
            }
        }

        protected void DisplayRiskWarning(OMAMFund objFund)
        {
            //OK, Now create the Risk Warning Copy
            //Put them all in one main div

            if (objFund.RiskWarning != null && objFund.RiskWarning != string.Empty)
            {
                Label lblRiskWarning = new Label();
                lblRiskWarning.CssClass = "divRiskWarning";
                lblRiskWarning.Text = objFund.RiskWarning.ToString();

                Panel pnlRiskWarning = new Panel();
                pnlRiskWarning.ID = "pnlRiskWarning";
                pnlRiskWarning.CssClass = "divSPRatingCopy";
                pnlRiskWarning.Controls.Add(lblRiskWarning);

                pnlRatings.Controls.Add(pnlRiskWarning);
            }

        }

        protected void OpenDocument(object sender, CommandEventArgs e)
        {
            //Get the file details
            string uploadedFile =  e.CommandArgument.ToString();
            FileInfo file = new FileInfo(uploadedFile);

            // Checking if file exists
            if (file.Exists)
            {
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();

                string filename;
                filename = DateTime.Now.Ticks.ToString() + "_" + uploadedFile.ToString();

                Response.AppendHeader("Content-Disposition", string.Format("attachment; filename={0}", e.CommandName.ToString()));
                Response.Charset = "";

                Response.AppendHeader("Content-Length", file.Length.ToString());

                Response.ContentType = "application/pdf";
                Response.WriteFile(file.FullName);
                Response.End();

                
                //Response.Clear();
                //Response.ClearContent();
                //Response.AddHeader("Content-Disposition", "attachment; filename=" + e.CommandName.ToString());
                //Response.AddHeader("Content-Length", file.Length.ToString());
                //Response.WriteFile(file.FullName);
                //Response.End();
            }
            else
            {
                Response.Write(" <font color='Red' size='2'>File not found.</font>");
            }
        }

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
    }
}