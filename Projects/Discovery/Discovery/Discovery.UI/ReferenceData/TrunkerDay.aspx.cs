using System;

namespace Discovery.UI.Web.ReferenceData
{
    /*************************************************************************************************
     ** CLASS:	TrunkerDay
     **
     ** OVERVIEW:
     ** This page allows a uers to view, add, edit and delete a single TrunkerDay
     **
     ** MODIFICATION HISTORY:
     **
     ** Date:		Version:    Who:	Change:
     ** 19/7/06		1.0			PJN		Initial Version
     ************************************************************************************************/

    public partial class TrunkerDay : DiscoveryDataDetailPage
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
            CreateRule = "Reference Data: Edit Trunker Days";
            DeleteRule = "Reference Data: Edit Trunker Days";
            UpdateRule = "Reference Data: Edit Trunker Days";

            //call base class
            base.Page_Load(sender, e);
        }
        
        #endregion

        protected override void SetValidation()
        {
            Validation.AddValidation("TextBoxDays", "Days");
            Validation.AddValidation("DropDownListSourceWarehouse", "SourceWarehouseId");
            Validation.AddValidation("DropDownListDestinationWarehouse", "DestinationWarehouseId");
            base.SetValidation();
        }

    }
}