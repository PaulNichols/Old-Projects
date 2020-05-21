using System;

namespace Discovery.UI.Web.ReferenceData
{
    /**************************************************************************************************
  ** CLASS:	AddressController
  **
  ** OVERVIEW:
  ** This page allows a user to view all transaction sub types
  **
  ** MODIFICATION HISTORY:
  **
  ** Date:		Version:    Who:	Change:
  ** 19/7/06		1.0			PJN		Initial Version
  ************************************************************************************************/

    public partial class TransactionSubTypes : DiscoveryDataItemsPage
    {
        #region Properties

        #endregion

        #region Protected Methods

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            ReadRule = "Reference Data: View Transaction Sub Types";
            CreateRule = "Reference Data: Edit Transaction Sub Types";
        

            //call base class
            base.Page_Load(sender, e);
        }
        
        #endregion

    }
}