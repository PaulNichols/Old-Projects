using System;

namespace Discovery.UI.Web.ReferenceData
{
    /*************************************************************************************************
   ** CLASS:	AddressController
   **
   ** OVERVIEW:
   ** This page allows a user to view, edit, delete or insert a single warehouse
   **
   ** MODIFICATION HISTORY:
   **
   ** Date:		Version:    Who:	Change:
   ** 19/7/06		1.0		PJN		Initial Version
   ************************************************************************************************/

    public partial class TransactionTypes : DiscoveryDataItemsPage
    {
        #region Properties

        #endregion

        #region Protected Methods

        #endregion

        #region Events
       
        protected override void Page_Load(object sender, EventArgs e)
        {
            ReadRule = "Reference Data: View Transaction Types";
            CreateRule = "Reference Data: View Transaction Types";

            //call base class
            base.Page_Load(sender, e);
        
        }
        #endregion


    }
}