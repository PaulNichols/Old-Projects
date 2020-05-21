using System;
using System.Collections.Generic;
using System.Configuration;
using System.Management;
using Discovery.ComponentServices.DataAccess;
using Discovery.ComponentServices.ExceptionHandling;
using Discovery.Utility;
using Discovery.Utility.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Discovery.BusinessObjects.Controllers
{
    /*************************************************************************************************
** CLASS:	OpcoController
**
** OVERVIEW:
** This controller class contains all methods related to OpCos including calling data access methods
**
** MODIFICATION HISTORY:
**
** Date:		Version:    Who:	Change:
** 19/7/06		1.0			PJN		Initial Version
************************************************************************************************/

    /// <summary>
    /// A Class 'PrinterController' which is a business object controller
    /// </summary>
    public static class PrinterController
    {
        /// <summary>
        /// Saves the printer.
        /// </summary>
        /// <param name="printer">The printer.</param>
        /// <returns></returns>
        public static int SavePrinter(Printer printer)
        {
            int returnValue = -1;

            try
            {
                if (printer.IsValid)
                {
                    DataAccessProvider dataAccessProvider = DataAccessProvider.Instance();

                    if (dataAccessProvider != null)
                    {
                        returnValue = dataAccessProvider.SavePrinter(printer);
                    }
                    else
                    {
                        throw new DataProviderException("Null Provider Instance");
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return returnValue;
        }

        /// <summary>
        /// Deletes the printer.
        /// </summary>
        /// <param name="printer">The printer to delete.</param>
        /// <returns></returns>
        public static bool DeletePrinter(Printer printer)
        {
            bool success = false;

            try
            {
                if (printer != null)
                {
                    DataAccessProvider dataAccessProvider = DataAccessProvider.Instance();

                    if (dataAccessProvider != null)
                        success = dataAccessProvider.DeletePrinter(printer.Id);
                    else
                    {
                        Exception ex = new DataProviderException("Null Provider Instance");
                        if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return success;
        }

        /// <summary>
        /// Gets the printer.
        /// </summary>
        /// <param name="printerId">The printer id.</param>
        /// <returns></returns>
        public static Printer GetPrinter(int printerId)
        {
            Printer printer = null;
            try
            {
                DataAccessProvider dataAccessProvider = DataAccessProvider.Instance();

                if (dataAccessProvider != null)
                {
                    printer = CBO<Printer>.FillObject(dataAccessProvider.GetPrinter(printerId));

                    return printer;
                }
                else
                {
                    throw new DataProviderException("Null Provider Instance");
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            return printer;
        }

        /// <summary>
        /// Gets the print job.
        /// </summary>
        /// <param name="jobId">The job id.</param>
        /// <returns></returns>
        public static PrintJob GetPrintJob(uint? jobId)
        {
            List<PrintJob> printJobs = new List<PrintJob>();
            string searchQuery = "SELECT * FROM Win32_PrintJob";

            /*searchQuery can also be mentioned with where Attribute,
                but this is not working in Windows 2000 / ME / 98 machines 
                and throws Invalid query error*/
            ManagementObjectSearcher searchPrintJobs = new ManagementObjectSearcher(PrintServerScope(), searchQuery);
            ManagementObjectCollection searchResults = searchPrintJobs.Get();
            PrintJob printJob = null;
            foreach (ManagementObject wmiPrintJob in searchResults)
            {
                if (jobId == (uint?)wmiPrintJob.Properties["JobId"].Value)
                {
                    printJob = new PrintJob();
                    PopulatePrintJob(printJob, wmiPrintJob);
                    printJobs.Add(printJob);
                    break;
                }
            }

            return printJob;
        }

        private static void PopulatePrintJob(PrintJob printJob, ManagementObject wmiPrintJob)
        {
            printJob.Name = wmiPrintJob.Properties["Name"].Value.ToString();

            printJob.PrinterName = printJob.Name.Split(',')[0];

            try
            {
                printJob.Document = wmiPrintJob.Properties["Document"].Value as string;
            }
            catch
            {
            }
            try
            {
                printJob.DriverName = wmiPrintJob.Properties["DriverName"].Value as string;
            }
            catch
            {
            }
            try
            {
                printJob.Caption = wmiPrintJob.Properties["Caption"].Value as string;
            }
            catch
            {
            }
            try
            {
                printJob.Color = wmiPrintJob.Properties["Color"].Value as string;
            }
            catch
            {
            }
            try
            {
                printJob.DataType = wmiPrintJob.Properties["DataType"].Value as string;
            }
            catch
            {
            }
            try
            {
                printJob.Description = wmiPrintJob.Properties["Description"].Value as string;
            }
            catch
            {
            }
            try
            {
                printJob.ElapsedTime = (DateTime?)wmiPrintJob.Properties["ElapsedTime"].Value;
            }
            catch
            {
            }
            try
            {
                printJob.HostPrintQueue = wmiPrintJob.Properties["HostPrintQueue"].Value as string;
            }
            catch
            {
            }
            try
            {
                printJob.InstallDate = (DateTime?)wmiPrintJob.Properties["InstallDate"].Value;
            }
            catch
            {
            }
            try
            {
                printJob.JobId = (uint?)wmiPrintJob.Properties["JobId"].Value;
            }
            catch
            {
            }
            try
            {
                printJob.JobStatus = wmiPrintJob.Properties["JobStatus"].Value as string;
            }
            catch
            {
            }
            try
            {
                printJob.Notify = wmiPrintJob.Properties["Notify"].Value as string;
            }
            catch
            {
            }
            try
            {
                printJob.PagesPrinted = (uint?)wmiPrintJob.Properties["PagesPrinted"].Value;
            }
            catch
            {
            }
            try
            {
                printJob.PaperLength = (uint?)wmiPrintJob.Properties["PaperLength"].Value;
            }
            catch
            {
            }
            try
            {
                printJob.PaperSize = wmiPrintJob.Properties["PaperSize"].Value as string;
            }
            catch
            {
            }
            try
            {
                printJob.PaperWidth = (uint?)wmiPrintJob.Properties["PaperWidth"].Value;
            }
            catch
            {
            }
            try
            {
                printJob.Parameters = wmiPrintJob.Properties["Parameters"].Value as string;
            }
            catch
            {
            }
            try
            {
                printJob.PrintProcessor = wmiPrintJob.Properties["PrintProcessor"].Value as string;
            }
            catch
            {
            }
            try
            {
                printJob.Priority = (uint?)wmiPrintJob.Properties["Priority"].Value;
            }
            catch
            {
            }
            try
            {
                printJob.Size = (uint?)wmiPrintJob.Properties["Size"].Value;
            }
            catch
            {
            }
            try
            {
                printJob.StartTime = (DateTime?)wmiPrintJob.Properties["StartTime"].Value;
            }
            catch
            {
            }
            try
            {
                printJob.Owner = wmiPrintJob.Properties["Owner"].Value.ToString();
            }
            catch
            {
            }
            try
            {
                printJob.Status = wmiPrintJob.Properties["Status"].Value as string;
            }
            catch
            {
            }
            try
            {
                printJob.StatusMask = (uint?)wmiPrintJob.Properties["StatusMask"].Value;
            }
            catch
            {
            }
            try
            {
                printJob.TimeSubmitted = (DateTime?)wmiPrintJob.Properties["TimeSubmitted"].Value;
            }
            catch
            {
            }
            try
            {
                printJob.TotalPages = (uint?)wmiPrintJob.Properties["TotalPages"].Value;
            }
            catch
            {
            }
            try
            {
                printJob.UntilTime = (DateTime?)wmiPrintJob.Properties["UntilTime"].Value;
            }
            catch
            {
            }
        }

        /// <summary>
        /// Gets the print jobs.
        /// </summary>
        /// <param name="printerToFind">The warehouse to get a list of print jobs for.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        public static List<PrintJob> GetPrintJobs(string printerToFind, string sortExpression)
        {
            List<PrintJob> printJobs = new List<PrintJob>();

            string searchQuery = "SELECT * FROM Win32_PrintJob";

            /*searchQuery can also be mentioned with where Attribute,
                but this is not working in Windows 2000 / ME / 98 machines 
                and throws Invalid query error*/
            ManagementObjectSearcher searchPrintJobs = new ManagementObjectSearcher(PrintServerScope(), searchQuery);
            ManagementObjectCollection searchResults = searchPrintJobs.Get();
            foreach (ManagementObject wmiPrintJob in searchResults)
            {
                //Job name would be of the format [Printer name], [Job ID]


                if (!string.IsNullOrEmpty(printerToFind) &&
                    printerToFind == wmiPrintJob.Properties["Name"].Value.ToString().Split(',')[0])
                {
                    PrintJob printJob = new PrintJob();
                    PopulatePrintJob(printJob, wmiPrintJob);
                    printJobs.Add(printJob);
                }
            }
            if (string.IsNullOrEmpty(sortExpression))
            {
                sortExpression = "Name";
            }
            printJobs.Sort(new UniversalComparer<PrintJob>(sortExpression));

            return printJobs;
        }

        /// <summary>
        /// Gets the print jobs.
        /// </summary>
        /// <param name="warehouseId">The warehouse to get a list of print jobs for.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        public static List<PrintJob> GetPrintJobs(int warehouseId, string sortExpression)
        {
            List<PrintJob> printJobs = new List<PrintJob>();
            Warehouse warehouse =
                WarehouseController.GetWarehouse(warehouseId, false);
            if (warehouse != null)
            {
                printJobs = GetPrintJobs(warehouse.PrinterName, sortExpression);
            }
            return printJobs;
        }

        /// <summary>
        /// Perges all print jobs.
        /// </summary>
        /// <param name="printerName">Name of the printer.</param>
        /// <returns></returns>
        public static bool PergeAllPrintJobs(string printerName)
        {
            return PreformActionOnPrinter(printerName, "CancelAllJobs");
        }

        /// <summary>
        /// Pauses the printer.
        /// </summary>
        /// <param name="printerName">Name of the printer.</param>
        /// <returns></returns>
        public static bool PausePrinter(string printerName)
        {
            return PreformActionOnPrinter(printerName, "Pause");
        }

        /// <summary>
        /// Prints the test page to printer.
        /// </summary>
        /// <param name="printerName">Name of the printer.</param>
        /// <returns></returns>
        public static bool PrintTestPageToPrinter(string printerName)
        {
            return PreformActionOnPrinter(printerName, "PrintTestPage");
        }

        /// <summary>
        /// Resumes the printer.
        /// </summary>
        /// <param name="printerName">Name of the printer.</param>
        /// <returns></returns>
        public static bool ResumePrinter(string printerName)
        {
            return PreformActionOnPrinter(printerName, "Resume");
        }

        private static bool PreformActionOnPrinter(string printerName, string actionToPerform)
        {
            bool isActionPerformed = false;
            string searchQuery = "SELECT * FROM Win32_Printer where Name='" + printerName + "'";
            ManagementObjectSearcher searchPrinter =
                new ManagementObjectSearcher(PrintServerScope(), searchQuery);
            ManagementObjectCollection printers = searchPrinter.Get();
            foreach (ManagementObject printer in printers)
            {
                printer.InvokeMethod(actionToPerform, null);
                isActionPerformed = true;
                break;
            }

            return isActionPerformed;
        }

        /// <summary>
        /// Gets all printers, sorted.
        /// </summary>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        public static List<Printer> GetServerPrinters(string sortExpression)
        {
            List<Printer> printers = new List<Printer>();

            string searchQuery = "SELECT * FROM Win32_Printer";
            ManagementObjectSearcher searchPrinters = new ManagementObjectSearcher(PrintServerScope(), searchQuery);
            ManagementObjectCollection printerCollection = searchPrinters.Get();
            foreach (ManagementObject wmiPrinter in printerCollection)
            {
                Printer printer = new Printer();
                printer.Name = wmiPrinter.Properties["Name"].Value.ToString();
                printer.IsDefault = (bool)wmiPrinter.Properties["Default"].Value;
                printers.Add(printer);
            }

            if (string.IsNullOrEmpty(sortExpression))
            {
                sortExpression = "Name";
            }
            printers.Sort(new UniversalComparer<Printer>(sortExpression));
            return printers;
        }

        private static string PrintServerScope()
        {
            string serverName = ConfigurationManager.AppSettings["PrintServer"].ToString();
            if (string.IsNullOrEmpty(serverName))
            {
                serverName = "localhost";
            }
            return "\\\\" + serverName + "\\root\\cimv2";
        }

        /// <summary>
        /// Resumes the print job.
        /// </summary>
        /// <param name="printerName">Name of the printer.</param>
        /// <param name="printJobID">The print job ID.</param>
        /// <returns></returns>
        public static bool ResumePrintJob(string printerName, uint printJobID)
        {
            return PreformActionOnPrintJob(printerName, printJobID, "Resume");
        }

        /// <summary>
        /// Pauses the print job.
        /// </summary>
        /// <param name="printerName">Name of the printer.</param>
        /// <param name="printJobID">The print job ID.</param>
        /// <returns></returns>
        public static bool PausePrintJob(string printerName, uint printJobID)
        {
            return PreformActionOnPrintJob(printerName, printJobID, "Pause");
        }



        private static bool PreformActionOnPrintJob(string printerName, uint printJobID, string actionToPerform)
        {
            bool isActionPerformed = false;
            string searchQuery = "SELECT * FROM Win32_PrintJob";
            ManagementObjectSearcher searchPrintJobs =
                new ManagementObjectSearcher(PrintServerScope(), searchQuery);
            ManagementObjectCollection prntJobCollection = searchPrintJobs.Get();
            foreach (ManagementObject prntJob in prntJobCollection)
            {
                String jobName = prntJob.Properties["Name"].Value.ToString();
                //Job name would be of the format [Printer name], [Job ID]

                string prnterName = jobName.Split(',')[0];
                int prntJobID = Convert.ToInt32(jobName.Split(',')[1]);
                if (String.Compare(prnterName, printerName, true) == 0)
                {
                    if (prntJobID == printJobID)
                    {
                        prntJob.InvokeMethod(actionToPerform, null);
                        isActionPerformed = true;
                        break;
                    }
                }
            }
            return isActionPerformed;
        }

        /// <summary>
        /// Cancels the print job.
        /// </summary>
        /// <param name="printerName">Name of the printer.</param>
        /// <param name="printJobID">The print job ID.</param>
        /// <returns></returns>
        public static bool CancelPrintJob(string printerName, uint printJobID)
        {
            bool isActionPerformed = false;
            string searchQuery = "SELECT * FROM Win32_PrintJob";
            ManagementObjectSearcher searchPrintJobs =
                new ManagementObjectSearcher(PrintServerScope(), searchQuery);
            ManagementObjectCollection prntJobCollection = searchPrintJobs.Get();
            foreach (ManagementObject prntJob in prntJobCollection)
            {
                String jobName = prntJob.Properties["Name"].Value.ToString();
                //Job name would be of the format [Printer name], [Job ID]
                string prnterName = jobName.Split(',')[0];
                int prntJobID = Convert.ToInt32(jobName.Split(',')[1]);
                //string documentName = prntJob.Properties["Document"].Value.ToString();
                if (String.Compare(prnterName, printerName, true) == 0)
                {
                    if (prntJobID == printJobID)
                    {
                        //performs a action similar to the cancel 
                        //operation of windows print console
                        prntJob.Delete();
                        isActionPerformed = true;
                        break;
                    }
                }
            }
            return isActionPerformed;
        }

        ///// <summary>
        ///// Gets all printers, sorted.
        ///// </summary>
        ///// <param name="sortExpression">The sort expression.</param>
        ///// <returns></returns>
        //public static List<Printer> GetPrinters(string sortExpression)
        //{
        //    List<Printer> printers = GetPrinters();
        //    if (string.IsNullOrEmpty(sortExpression))
        //    {
        //        sortExpression = "Name";
        //    }
        //    printers.Sort(new UniversalComparer<Printer>(sortExpression));
        //    return printers;
        //}

        ///// <summary>
        ///// Gets the printers.
        ///// </summary>
        ///// <returns></returns>
        //public static List<Printer> GetPrinters()
        //{
        //    List<Printer> printers = null;
        //    try
        //    {
        //        DataAccessProvider dataAccessProvider = DataAccessProvider.Instance();
        //        if (dataAccessProvider != null)
        //        {
        //            printers = CBO<Printer>.FillCollection(dataAccessProvider.GetPrinters());
        //            return printers;
        //        }
        //        else
        //        {
        //            throw new DataProviderException("Null Provider Instance");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
        //    }
        //    return printers;
        //}

    }
}