using System;

namespace Discovery.UI.Web.ReferenceData
{
    /*************************************************************************************************
   ** CLASS:	SalesLocations
   **
   ** OVERVIEW:
   ** Page to allow users to view and maintain all Sales Locations
   **
   ** MODIFICATION HISTORY:
   **
   ** Date:		Version:    Who:	Change:
   ** 19/7/06		1.0			PJN		Initial Version
   ************************************************************************************************/

    public partial class SalesLocations : DiscoveryDataItemsPage
    {
        #region Properties

       
        #endregion

        #region Protected Methods

        /// <summary>
        /// An overridable method allowing each page to specify if it requires the user to be authenticated
        /// </summary>
        /// <returns></returns>
        protected override bool RequiresAuthentication()
        {
            return true;
        }

        #endregion

        #region Events


        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected override void Page_Load(object sender, EventArgs e)
        {
            ReadRule = "Reference Data: View Sales Locations";
            CreateRule = "Reference Data: Edit Sales Locations";
         

            //call base class
            base.Page_Load(sender, e);
        }
        
        
        #endregion

    }
}