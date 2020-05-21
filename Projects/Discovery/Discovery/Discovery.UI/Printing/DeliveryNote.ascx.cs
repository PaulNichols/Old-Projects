/*************************************************************************************************
 ** FILE:	DeliveryNote.ascx.cs
 ** DATE:	30/05/2006
 ** AUTHOR:	Lee Spring
 **
 **
 ** OVERVIEW:
 **
 ** MODIFICATION HISTORY:
 **
 ** Date:		Version:	Who:	Change:
 ** 30/5/06		1.0		    LAS	    Initial Version
 ************************************************************************************************/
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Discovery.BusinessObjects;
using Discovery.BusinessObjects.Controllers;
using CrystalDecisions.CrystalReports.Engine;


namespace Discovery.UI.Web.UserControls
{

    public class ReloadReportDataEvent : EventArgs
    {
        private bool cancelDataLoad = false;

        public bool CancelDataLoad
        {
            get { return cancelDataLoad; }
            set { cancelDataLoad = value; }
        }
    }

    public delegate void ReloadReportData(ref ReloadReportDataEvent referralArgs);

    public partial class Printing_DeliveryNote : System.Web.UI.UserControl
    {
        // The path to the reports
        private const string REPORTPATH = "~/Printing";
        private bool showReport = false;

        public event ReloadReportData ReloadData;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public bool PrintPreview
        {
            get
            {
                if (ViewState["PrintPreview"] == null)
                {
                    return false;
                }
                else
                {
                    return (bool)ViewState["PrintPreview"];
                }
            }
            set
            {
                ViewState["PrintPreview"] = value;
            }
        }

        // *************************************************************
        // **
        // ** Printing Support
        // **
        // *************************************************************

        public void Print(ReportDocument theReport)
        {
            // Load the report into the viewer
            DeliveryNoteViewer.ReportSource = theReport;

            // Make sure we're on the first page
            //DeliveryNoteViewer.ShowFirstPage();

            // Debug, display the report
            PrintPreview = true;

            // Do we display the print preview?
            if (PrintPreview)
            {
                // Show report
                showReport = true;
            }
            else
            {
                // Print via javascript, etc
            }
        }

        protected void DeliveryNoteViewer_Navigate(object source, CrystalDecisions.Web.NavigateEventArgs e)
        {
            // We need to reload the report data, fire event so that data can be re-loaded
            if (null != ReloadData)
            {
                // Create an instance of the delegate argument
                ReloadReportDataEvent reloadReportData = new ReloadReportDataEvent();
                
                // Call delegate
                ReloadData(ref reloadReportData);
                
                // See if we display the report
                if (!reloadReportData.CancelDataLoad)
                {
                    // We must be in preview mode, display report
                    showReport = true;
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            // Hide the report by default
            DeliveryNoteViewer.DisplayPage = false;

            // Call base
            base.OnLoad(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            // See if we need to display the report on-screen
            if (showReport)
            {
                // Hide the report by default
                DeliveryNoteViewer.DisplayPage = true;

                // Display report viewer
                popupPrintNote.Show();
            }

            // Call base
            base.OnPreRender(e);
        }
    }
}