using System;

namespace Discovery.UI.Web.ReferenceData
{
    /*************************************************************************************************
   ** CLASS:	OptrakRegions
   **
   ** OVERVIEW:
   ** This page displays all OptrakRegions
   **
   ** MODIFICATION HISTORY:
   **
   ** Date:		Version:    Who:	Change:
   ** 19/7/06		1.0			PJN		Initial Version
   ************************************************************************************************/
    public partial class OptrakRegions : DiscoveryDataItemsPage
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

    }
}