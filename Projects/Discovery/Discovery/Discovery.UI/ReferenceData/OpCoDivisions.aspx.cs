using System;

namespace Discovery.UI.Web.ReferenceData
{
    /*************************************************************************************************
     ** CLASS:	Operating Company Divisions
     **
     ** OVERVIEW:
     ** This page displays all Operating Company Divisions
     **
     ** MODIFICATION HISTORY:
     **
     ** Date:		Version:    Who:	Change:
     ** 19/7/06		1.0			PJN		Initial Version
     ************************************************************************************************/

    public partial class OpCoDivisions : DiscoveryDataItemsPage
    {
        #region Properties

        /// <summary>
        /// Gets the detail URL. This property must be overriden in order for the base code
        /// to know which URL is the intended target when the user clicks on an item in
        /// the grid
        /// </summary>
        /// <value>The detail URL.</value>

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

    }
}