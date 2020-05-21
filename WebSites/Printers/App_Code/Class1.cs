using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Management;
using System.Collections.Generic;

/// <summary>
/// Summary description for Class1
/// </summary>
public static class PrinterController
{
    public class PrintJob
    {
        private string name;

public string Name
{
  get { return name; }
  set { name = value; }
}
        private string document;

public string Document
{
  get { return document; }
  set { document = value; }
}
        private string driver;

public string Driver
{
  get { return driver; }
  set { driver = value; }
}
        private string owner;

public string Owner
{
  get { return owner; }
  set { owner = value; }
}
private string printerName;

public string PrinterName
{
    get { return printerName; }
    set { printerName = value; }
}
  

    string caption;
    string color;
    string dataType;
    string description;
   
    string driverName;
    DateTime elapsedTime;
    string hostPrintQueue;
    DateTime installDate;
    UInt32 jobId;
    string jobStatus;
    string notify;

    UInt32 pagesPrinted;
    UInt32 paperLength;
    string paperSize;
    UInt32 paperWidth;
    string parameters;
    string printProcessor;
    UInt32 priority;
    UInt32 size;
    DateTime startTime;
    string status;
    UInt32 statusMask;
    DateTime timeSubmitted;
    UInt32 totalPages;
    DateTime untilTime;
}
    public static List<PrintJob> GetPrintJobs(string printerToFind)
        {
            List<PrintJob> printJobs = new List<PrintJob>();
            string searchQuery = "SELECT * FROM Win32_PrintJob";

            /*searchQuery can also be mentioned with where Attribute,
                but this is not working in Windows 2000 / ME / 98 machines 
                and throws Invalid query error*/
            ManagementObjectSearcher searchPrintJobs =new ManagementObjectSearcher(searchQuery);
            ManagementObjectCollection searchResults = searchPrintJobs.Get();
            foreach (ManagementObject wmiPrintJob in searchResults)
            {
                PrintJob printJob = new PrintJob();
                printJob.Name = wmiPrintJob.Properties["Name"].Value.ToString();
                

                //Job name would be of the format [Printer name], [Job ID]
                char[] splitArr = new char[1];
                splitArr[0] = Convert.ToChar(",");

                printJob.PrinterName = printJob.Name.Split(splitArr)[0];
                if (string.IsNullOrEmpty(printerToFind) || printerToFind == printJob.PrinterName)
                {
                printJob.Document = wmiPrintJob.Properties["Document"].Value.ToString();
                printJob.Driver = wmiPrintJob.Properties["DriverName"].Value.ToString();
                printJob.Owner = wmiPrintJob.Properties["Owner"].Value.ToString();
               
                    printJobs.Add(printJob);
                }
            }
            return printJobs;
        }

    public static List<string> GetPrinters()
    {

        List<string> printerNameCollection = new List<string>();
        string searchQuery = "SELECT * FROM Win32_Printer";
        ManagementObjectSearcher searchPrinters =               new ManagementObjectSearcher(searchQuery);
        ManagementObjectCollection printerCollection = searchPrinters.Get();
        foreach(ManagementObject printer in printerCollection)
        {
            printerNameCollection.Add(printer.Properties["Name"].Value.ToString());
        }
        return printerNameCollection;

    }

    //http://msdn.microsoft.com/library/default.asp?url=/library/en-us/wmisdk/wmi/win32_printjob.asp

    //public static bool PausePrintJob(string printerName, int printJobID)
    //{
    //    bool isActionPerformed = false;
    //    string searchQuery = "SELECT * FROM Win32_PrintJob";
    //    ManagementObjectSearcher searchPrintJobs =
    //             new ManagementObjectSearcher(searchQuery);
    //    ManagementObjectCollection prntJobCollection = searchPrintJobs.Get();
    //    foreach (ManagementObject prntJob in prntJobCollection)
    //    {
    //        System.String jobName = prntJob.Properties["Name"].Value.ToString();
    //        //Job name would be of the format [Printer name], [Job ID]
    //        char[] splitArr = new char[1];
    //        splitArr[0] = Convert.ToChar(",");
    //        string prnterName = jobName.Split(splitArr)[0];
    //        int prntJobID = Convert.ToInt32(jobName.Split(splitArr)[1]);
    //        string documentName = prntJob.Properties["Document"].Value.ToString();
    //        if (String.Compare(prnterName, printerName, true) == 0)
    //        {
    //            if (prntJobID == printJobID)
    //            {
    //                prntJob.InvokeMethod("Pause", null);
    //                isActionPerformed = true;
    //                break;
    //            }
    //        }
    //    }
    //    return isActionPerformed;
    //}

    //public static bool CancelPrintJob(string printerName, int printJobID)
    //{
    //    bool isActionPerformed = false;
    //    string searchQuery = "SELECT * FROM Win32_PrintJob";
    //    ManagementObjectSearcher searchPrintJobs =
    //           new ManagementObjectSearcher(searchQuery);
    //    ManagementObjectCollection prntJobCollection = searchPrintJobs.Get();
    //    foreach (ManagementObject prntJob in prntJobCollection)
    //    {
    //        System.String jobName = prntJob.Properties["Name"].Value.ToString();
    //        //Job name would be of the format [Printer name], [Job ID]
    //        char[] splitArr = new char[1];
    //        splitArr[0] = Convert.ToChar(",");
    //        string prnterName = jobName.Split(splitArr)[0];
    //        int prntJobID = Convert.ToInt32(jobName.Split(splitArr)[1]);
    //        string documentName = prntJob.Properties["Document"].Value.ToString();
    //        if (String.Compare(prnterName, printerName, true) == 0)
    //        {
    //            if (prntJobID == printJobID)
    //            {
    //                //performs a action similar to the cancel 
    //                //operation of windows print console
    //                prntJob.Delete();
    //                isActionPerformed = true;
    //                break;
    //            }
    //        }
    //    }
    //    return isActionPerformed;
    //}
}
