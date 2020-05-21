using System;

namespace Discovery.UI.Web.ReferenceData
{
    /*************************************************************************************************
   ** CLASS:	AToZ
   **
   ** OVERVIEW:
   ** This page shows a list of all warehouses and allows the user to maintain the list
   **
   ** MODIFICATION HISTORY:
   **
   ** Date:		Version:    Who:	Change:
   ** 19/7/06		1.0			PJN		Initial Version
   ************************************************************************************************/

    public partial class Warehouses : DiscoveryDataItemsPage
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
            ReadRule = "Reference Data: View Warehouses";
            CreateRule =" Reference Data: Edit Warehouses";
         

            //call base class
            base.Page_Load(sender, e);
        }
        
        #endregion

    }
}