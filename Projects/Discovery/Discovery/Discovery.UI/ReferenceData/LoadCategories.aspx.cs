using System;

namespace Discovery.UI.Web.ReferenceData
{
    /*************************************************************************************************
   ** CLASS:	Load Categories
   **
   ** OVERVIEW:
   ** This page displays all Load Categoriess
   **
   ** MODIFICATION HISTORY:
   **
   ** Date:		    Version:    Who:	Change:
   ** 03-Oct-2006   1.0			TVL		Initial Version
   ************************************************************************************************/
    public partial class LoadCategories : DiscoveryDataItemsPage
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
            ReadRule = "Reference Data: View Load Categories";
            CreateRule = "Reference Data: Edit Load Categories";
            DeleteRule = "Reference Data: Edit Load Categories";
            UpdateRule = "Reference Data: Edit Load Categories";

            //call base class
            base.Page_Load(sender, e);
        }
        
        #endregion

    }
}