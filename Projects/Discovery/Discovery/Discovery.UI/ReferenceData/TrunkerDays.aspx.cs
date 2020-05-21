using System;

namespace Discovery.UI.Web.ReferenceData
{
    /*************************************************************************************************
   ** CLASS:	TrunkerDays
   **
   ** OVERVIEW:
   ** This page shows all trunkerDays
   **
   ** MODIFICATION HISTORY:
   **
   ** Date:		Version:    Who:	Change:
   ** 19/7/06		1.0			PJN		Initial Version
   ************************************************************************************************/

    public partial class TrunkerDays : DiscoveryDataItemsPage
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
            ReadRule = "Reference Data: View Trunker Days";
            CreateRule = " Reference Data: View Trunker Days";
       
            //call base class
            base.Page_Load(sender, e);
        }
        
        #endregion

    }
}