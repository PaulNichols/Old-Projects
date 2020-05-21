using System;
using System.Collections.Specialized;
using System.IO;
using System.Web.UI.WebControls;

namespace Discovery.UI.Web.ReferenceData
{
    /*************************************************************************************************
     ** CLASS:	OpCoDivision
     **
     ** OVERVIEW:
     ** This page allows a user to add,edit,delete and view a single opco division, including uploading a logo
     **
     ** MODIFICATION HISTORY:
     **
     ** Date:		Version:    Who:	Change:
     ** 19/7/06		1.0			PJN		Initial Version
     ************************************************************************************************/

    public partial class OpCoDivision : DiscoveryDataDetailPage
    {
        #region Properties

        #endregion

        #region Protected Methods

        #endregion
       
        #region Events
        
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected override void Page_Load(object sender, EventArgs e)
        {
            ReadRule = "Reference Data: View Opco Divisions";
            CreateRule = "Reference Data: Edit Opco Divisions";
            DeleteRule = "Reference Data: Edit Opco Divisions";
            UpdateRule = "Reference Data: Edit Opco Divisions";

            //call base class
            base.Page_Load(sender, e);
        }
        
        #endregion

        /// <summary>
        /// Handles the ItemUpdating event of the MappingFormView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Web.UI.WebControls.FormViewUpdateEventArgs"/> instance containing the event data.</param>
        protected void MappingFormView_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            //try
            //{
            AddValues(e.NewValues);
            //}

            //catch (Exception ex)
            //{
            //    DisplayMessage(ex.Message);
            //    e.k
            //}
        }

        private void AddValues(IOrderedDictionary values)
        {
            FileUpload logoFileUpload = GetControl<FileUpload>("FileUploadLogo", PageFormView);
            if (logoFileUpload.HasFile)
            {
                if (logoFileUpload.PostedFile.ContentType.ToLower() == "image/pjpeg")
                {
                    BinaryReader reader =
                        new BinaryReader(logoFileUpload.PostedFile.InputStream);
                    byte[] image =
                        reader.ReadBytes(logoFileUpload.PostedFile.ContentLength);
                    values.Add("Logo", image);
                    values.Add("LogoURI", logoFileUpload.PostedFile.FileName);
                }
                else
                {
                    throw new Exception("Please choose a JPEG for the logo.");
                }
            }
            else
            {
                throw new Exception("File Upload failed.");
            }
        }

        /// <summary>
        /// Handles the ItemInserting event of the MappingFormView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Web.UI.WebControls.FormViewInsertEventArgs"/> instance containing the event data.</param>
        protected void MappingFormView_ItemInserting(object sender, FormViewInsertEventArgs e)
        {
            try
            {
                AddValues(e.Values);
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
                e.Cancel = true;
            }
        }


        protected override void SetValidation()
        {
            Validation.AddValidation("TextBoxCode", "Code");
            Validation.AddValidation("DropDownListOpCo", "OpCoId");
            //Validation.AddValidation("FileUploadLogo", "Description");
            base.SetValidation();
        }

        public string GetLogoViewerUrl(int id)
        {
            return "OpCoDivisionLogo.aspx?ID=" + id.ToString();
        }


    }
}