
using System;
using System.Web.UI.WebControls;
using Discovery.BusinessObjects.Controllers;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Discovery.UI.Web.ReferenceData
{
    /*************************************************************************************************
     ** CLASS:	PrintJob
     **
     ** OVERVIEW:
     ** This page allows a user to view single PrintJob and control it
     **
     ** MODIFICATION HISTORY:
     **
     ** Date:		Version:    Who:	Change:
     ** 19/7/06		1.0			PJN		Initial Version
     ************************************************************************************************/
    public partial class PrintJob : DiscoveryDataDetailPage
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
 
        protected void ImageButtonPause_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                PrinterController.PausePrintJob(GetControl<Label>("PrinterNameLabel", PageFormView).Text,
                                                Convert.ToUInt32(GetControl<Label>("JobIdLabel", PageFormView).Text));
            }
            catch (Exception ex)
            {
                 if (ExceptionPolicy.HandleException(ex, "User Interface")) DisplayMessage("There was a problem pausing the printing");
            }
        }

        protected void ImageButtonResume_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                PrinterController.ResumePrintJob(GetControl<Label>("PrinterNameLabel", PageFormView).Text,
                                                 Convert.ToUInt32(GetControl<Label>("JobIdLabel", PageFormView).Text));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "User Interface"))  DisplayMessage("The print job could not be resumed");
            }
        }

        protected void ImageButtonCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                if (PrinterController.CancelPrintJob(GetControl<Label>("PrinterNameLabel", PageFormView).Text,
                                                     Convert.ToUInt32(GetControl<Label>("JobIdLabel", PageFormView).Text)))
                {
                    Response.Redirect(BackUrl);
                }
            }
            catch (Exception ex)
            {
                   if (ExceptionPolicy.HandleException(ex, "User Interface"))  DisplayMessage(ex);
            }
        }

    }
}