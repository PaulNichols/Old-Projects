using System.Collections.Generic;
using System.Data.SqlClient;
using System.Transactions;
using Discovery.BusinessObjects;
using Discovery.BusinessObjects.Controllers;
using Discovery.ComponentServices.ExceptionHandling;
using Discovery.Utility.DataAccess.Exceptions;
using NUnit.Framework;

namespace Discovery.UnitTests
{
    [TestFixture]
    public class PrinterTests
    {
        [Test]
        public void GetPrintJobs()
        {

            //get printers (should install a printer or have a test printer?)
            List<Printer> printers = PrinterController.GetServerPrinters("");
            foreach (Printer printer in printers)
            {
                if (printer.IsDefault)
                {
                    //if there is one then pause it
                    if (PrinterController.PausePrinter(printer.Name))
                    {
                        //print to it
                        if (PrinterController.PrintTestPageToPrinter(printer.Name))
                        {
                            //check that there is at least on job on the queue
                            Assert.IsTrue(PrinterController.GetPrintJobs(printer.Name, "").Count > 0);
                            PrinterController.PergeAllPrintJobs(printer.Name);
                            PrinterController.ResumePrinter(printer.Name);
                        }
                    }

                }
            }

        }

        [Test]
        public void GetPrintJob()
        {
            //get printers (should install a printer or have a test printer?)
            List<Printer> printers = PrinterController.GetServerPrinters("");
            foreach (Printer printer in printers)
            {
                if (printer.IsDefault)
                {
                    //if there is one then pause it
                    if (PrinterController.PausePrinter(printer.Name))
                    {
                        //print to it
                        if (PrinterController.PrintTestPageToPrinter(printer.Name))
                        {
                            //check that there is at least on job on the queue
                            List<PrintJob> printJobs = PrinterController.GetPrintJobs(printer.Name, "");
                            if (printJobs.Count > 0)
                            {
                                Assert.IsNotNull(PrinterController.GetPrintJob(printJobs[0].JobId));
                            }
                            PrinterController.PergeAllPrintJobs(printer.Name);
                            PrinterController.ResumePrinter(printer.Name);
                        }
                    }

                }
            }
        }
    }
}