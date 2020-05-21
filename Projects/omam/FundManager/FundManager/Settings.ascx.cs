using System;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;

namespace M2.Modules.FundManager
{
    public partial class Settings : ModuleSettingsBase
    {

        /// <summary>
        /// handles the loading of the module setting for this
        /// control
        /// </summary>
        public override void LoadSettings()
        {
            try
            {
                if (!IsPostBack)
                {
                    //Populate the SP Image Folder
                    object SPImageFolder = TabModuleSettings["SPImageFolder"];
                    if (SPImageFolder != null)
                    {
                        string settingValue = SPImageFolder.ToString();
                        txtSPImageFolder.Text = settingValue;
                    }

                    //Populate the CW Image Folder
                    object CWImageFolder = TabModuleSettings["CWImageFolder"];
                    if (CWImageFolder != null)
                    {
                        string settingValue = CWImageFolder.ToString();
                        txtCWImageFolder.Text = settingValue;
                    }

                    //Populate the OBSR Image Folder
                    object OBSRImageFolder = TabModuleSettings["OBSRImageFolder"];
                    if (OBSRImageFolder != null)
                    {
                        string settingValue = OBSRImageFolder.ToString();
                        txtOBSRImageFolder.Text = settingValue;
                    }

                    //Populate the Factsheet Folder
                    object FactsheetFolder = TabModuleSettings["FactsheetFolder"];
                    if (FactsheetFolder != null)
                    {
                        string settingValue = FactsheetFolder.ToString();
                        txtFactsheetFolder.Text = settingValue;
                    }
                    
                    //Populate the Reasons Why Image Folder
                    object ReasonFolder = TabModuleSettings["ReasonFolder"];
                    if (ReasonFolder != null)
                    {
                        string settingValue = ReasonFolder.ToString();
                        txtReasonFolder.Text = settingValue;
                    }

                    //Populate the AnnualReportFolder
                    object AnnualReportFolder = TabModuleSettings["AnnualReportFolder"];
                    if (AnnualReportFolder != null)
                    {
                        string settingValue = AnnualReportFolder.ToString();
                        txtAnnualReportFolder.Text = settingValue;
                    }

                    //Populate the InterimFolder
                    object InterimFolder = TabModuleSettings["InterimFolder"];
                    if (InterimFolder != null)
                    {
                        string settingValue = InterimFolder.ToString();
                        txtInterimFolder.Text = settingValue;
                    }

                    //Populate the SPReportFolder
                    object SPReportFolder = TabModuleSettings["SPReportFolder"];
                    if (SPReportFolder != null)
                    {
                        string settingValue = SPReportFolder.ToString();
                        txtSPReportFolder.Text = settingValue;
                    }

                    //Populate the OBSRReportFolder
                    object OBSRReportFolder = TabModuleSettings["OBSRReportFolder"];
                    if (OBSRReportFolder != null)
                    {
                        string settingValue = OBSRReportFolder.ToString();
                        txtOBSRReportFolder.Text = settingValue;
                    }

                    //Populate the SalesAidFolder
                    object SalesAidFolder = TabModuleSettings["SalesAidFolder"];
                    if (SalesAidFolder != null)
                    {
                        string settingValue = SalesAidFolder.ToString();
                        txtSalesAidFolder.Text = settingValue;
                    }

                    //Populate the TermsheetFolder
                    object TermsheetFolder = TabModuleSettings["TermsheetFolder"];
                    if (TermsheetFolder != null)
                    {
                        string settingValue = TermsheetFolder.ToString();
                        txtTermsheetFolder.Text = settingValue;
                    }
                }
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// handles updating the module settings for this control
        /// </summary>
        public override void UpdateSettings()
        {
            try
            {
                ModuleController controller = new ModuleController();
                controller.UpdateTabModuleSetting(this.TabModuleId, "SPImageFolder", txtSPImageFolder.Text);
                controller.UpdateTabModuleSetting(this.TabModuleId, "CWImageFolder", txtCWImageFolder.Text);
                controller.UpdateTabModuleSetting(this.TabModuleId, "OBSRImageFolder", txtOBSRImageFolder.Text);

                controller.UpdateTabModuleSetting(this.TabModuleId, "FactsheetFolder", txtFactsheetFolder.Text);
                controller.UpdateTabModuleSetting(this.TabModuleId, "ReasonFolder", txtReasonFolder.Text);
                controller.UpdateTabModuleSetting(this.TabModuleId, "AnnualReportFolder", txtAnnualReportFolder.Text);
                controller.UpdateTabModuleSetting(this.TabModuleId, "InterimFolder", txtInterimFolder.Text);
                controller.UpdateTabModuleSetting(this.TabModuleId, "SPReportFolder", txtSPReportFolder.Text);
                controller.UpdateTabModuleSetting(this.TabModuleId, "OBSRReportFolder", txtOBSRReportFolder.Text);
                controller.UpdateTabModuleSetting(this.TabModuleId, "SalesAidFolder", txtSalesAidFolder.Text);
                controller.UpdateTabModuleSetting(this.TabModuleId, "TermsheetFolder", txtTermsheetFolder.Text);
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }
    }
}