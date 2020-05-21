using System;
using ValidationFramework;

namespace Discovery.UI.Web.ReferenceData
{
    /*************************************************************************************************
  ** CLASS:	OptrakRegions
  **
  ** OVERVIEW:
  ** This page allows a uers to view, add, edit and delete a single OptrakRegion
  **
  ** MODIFICATION HISTORY:
  **
  ** Date:		Version:    Who:	Change:
  ** 19/7/06		1.0			PJN		Initial Version
  ************************************************************************************************/
    public partial class OptrakRegion : DiscoveryDataDetailPage
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
            ReadRule = "Reference Data: View Regions";
            CreateRule = "Reference Data: Edit Regions";
            DeleteRule = "Reference Data: Edit Regions";
            UpdateRule = "Reference Data: Edit Regions";

            //call base class
            base.Page_Load(sender, e);
        }
        
        #endregion

        protected override void SetValidation()
        {
            Validation.AddValidation("TextBoxCode", "Code");
            Validation.AddValidation("TextBoxDescription", "Description");
            base.SetValidation();
        }

    }
}