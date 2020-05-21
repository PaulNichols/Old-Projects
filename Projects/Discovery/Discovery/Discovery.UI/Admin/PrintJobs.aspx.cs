using System;
using Discovery.BusinessObjects.Controllers;

namespace Discovery.UI.Web.ReferenceData
{
    /*************************************************************************************************
     ** CLASS:	Printer
     **
     ** OVERVIEW:
     ** This page displays all PrintJobs
     **
     ** MODIFICATION HISTORY:
     **
     ** Date:		Version:    Who:	Change:
     ** 19/7/06		1.0			PJN		Initial Version
     ************************************************************************************************/

    public partial class PrintJobs : DiscoveryDataItemsPage
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
              ReadRule = "Admin: View Print Jobs";
              CreateRule = "Admin: Edit Print Jobs";
              DeleteRule = "Admin: Edit Print Jobs";
              UpdateRule = "Admin: Edit Print Jobs";
            
              //call base class
              base.Page_Load(sender, e);
          }
        
        #endregion
     
        //private void GetWarehousesPrinter()
        //{
        //    BusinessObjects.Warehouse warehouse =
        //        WarehouseController.GetWarehouse(Convert.ToInt32(DropDownListWarehouses.SelectedValue), false);
        //    if (warehouse != null)
        //    {
        //        HiddenFieldChosenPrinter.Value = warehouse.PrinterName;
        //    }
        //}


        //protected void tickerTimer_Tick(object sender, EventArgs e)
        //{
        //    GetWarehousesPrinter();
        //}

        //protected void DropDownListWarehouses_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    GetWarehousesPrinter();
        //}


    }
}