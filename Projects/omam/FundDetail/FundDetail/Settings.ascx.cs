using System;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;

namespace YourCompany.Modules.FundDetail
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
                    object SPImageFolder = TabModuleSettings["SPImageDisplayFolder"];
                    if (SPImageFolder != null)
                    {
                        string settingValue = SPImageFolder.ToString();
                        txtSPImageFolder.Text = settingValue;
                    }

                    //Populate the CW Image Folder
                    object CWImageFolder = TabModuleSettings["CWImageDisplayFolder"];
                    if (CWImageFolder != null)
                    {
                        string settingValue = CWImageFolder.ToString();
                        txtCWImageFolder.Text = settingValue;
                    }

                    //Populate the OBSR Image Folder
                    object OBSRImageFolder = TabModuleSettings["OBSRImageDisplayFolder"];
                    if (OBSRImageFolder != null)
                    {
                        string settingValue = OBSRImageFolder.ToString();
                        txtOBSRImageFolder.Text = settingValue;
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
                controller.UpdateTabModuleSetting(this.TabModuleId, "SPImageDisplayFolder", txtSPImageFolder.Text);
                controller.UpdateTabModuleSetting(this.TabModuleId, "CWImageDisplayFolder", txtCWImageFolder.Text);
                controller.UpdateTabModuleSetting(this.TabModuleId, "OBSRImageDisplayFolder", txtOBSRImageFolder.Text);
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }
    }
}