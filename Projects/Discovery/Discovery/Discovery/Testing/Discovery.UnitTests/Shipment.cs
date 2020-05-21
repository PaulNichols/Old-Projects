using System;
using System.Collections.Generic;
using System.IO;
using System.Transactions;
using Discovery.BusinessObjects;
using Discovery.BusinessObjects.Controllers;
using Discovery.BusinessSubscribers;
using Discovery.RequestManagement;
using Discovery.RequestManagerClient;
using NUnit.Framework;

namespace Discovery.UnitTests
{
    [TestFixture]
    public class ShipmentTests
    {

        static internal TDCShipmentLine PopulateLineItem()
        {
            TDCShipmentLine tdcShipmentLine = new TDCShipmentLine();
            //tdcShipmentLine.ConversionQuantity = 1;
            tdcShipmentLine.Description1 = "Description1";
            tdcShipmentLine.LoadCategoryCode = "xx";
            tdcShipmentLine.ProductCode = "ProductCode";
            tdcShipmentLine.QuantityUnit = "unit";
            tdcShipmentLine.UpdatedBy = "UpdatedBy";
            tdcShipmentLine.OriginalQuantity = 5;
            tdcShipmentLine.Quantity = 4;
            tdcShipmentLine.IsISO9000Approved = true;
            return tdcShipmentLine;
        }


        static internal TDCShipment PopulateItem()
        {
            TDCShipment tdcShipment =new TDCShipment();
            OpCoShipment opCoShipment = OpcoShipmentTests.PopulateNewItem();
            try
            {
                tdcShipment.OpCoShipmentId = OpCoShipmentController.SaveShipment(opCoShipment);
            }
            catch (InValidBusinessObjectException e)
            {
                Console.Write(e.ValidatableObject.ValidationMessages);
            }

            tdcShipment.OpCoCode = opCoShipment.OpCoCode;
            tdcShipment.OpCoSequenceNumber = 1;

            tdcShipment.OpCoContact.Email = "Email";
            tdcShipment.OpCoContact.Name = "Name";
            tdcShipment.DespatchNumber = "Number";
            tdcShipment.RequiredShipmentDate = DateTime.Now;
            tdcShipment.CustomerNumber = "CustNo";
            tdcShipment.CustomerName = "CustomerName";
            tdcShipment.CustomerReference = "ref";
            tdcShipment.CustomerAddress.Line1 = "Line1";
            tdcShipment.CustomerAddress.PostCode = "NN8 1NB";
            tdcShipment.ShipmentNumber = "ShipNo";
            tdcShipment.ShipmentName = "ShipmentName";
            tdcShipment.ShipmentAddress.Line1 = "Line1";
            tdcShipment.ShipmentAddress.PostCode = "NN8 1NB";
            tdcShipment.SalesBranchCode = "BranchCode";
            tdcShipment.AfterTime = "11:11";
            tdcShipment.BeforeTime = "10:10";
            tdcShipment.TailLiftRequired = false;
            tdcShipment.CheckInTime = 1;
            tdcShipment.DivisionCode = "Div";
            tdcShipment.GeneratedDateTime = DateTime.Now;
            tdcShipment.Status = Shipment.StatusEnum.Mapped;
            tdcShipment.IsRecurring = false;
            tdcShipment.IsValidAddress = false;
            tdcShipment.PAFAddress.Line1 = "Line1";
            tdcShipment.PAFAddress.PostCode = "PostCode";
            tdcShipment.UpdatedBy = "UpdatedBy";
            tdcShipment.Instructions = "Instructions";
            tdcShipment.VehicleMaxWeight = (decimal)1.1;
            OpCoDivision division = OpcoDivisionTests.PopulateNewItem();
            OpcoDivisionTests.SaveItem(division);
            tdcShipment.DivisionCode = division.Code;

            Warehouse deliveryWarehouse = WarehouseTests.PopulateNewItem();
            WarehouseTests.SaveItem(deliveryWarehouse);
            tdcShipment.DeliveryWarehouseCode = deliveryWarehouse.Code;

            Warehouse stockWarehouse = WarehouseTests.PopulateNewItem();
            WarehouseTests.SaveItem(stockWarehouse);
            tdcShipment.StockWarehouseCode = stockWarehouse.Code;

            Route route = RouteTests.PopulateNewItem();
            RouteTests.SaveItem(route);
            tdcShipment.RouteCode = route.Code;

            TransactionType transactionType = TransactionTypeTests.PopulateNewItem();
            TransactionTypeTests.SaveItem(transactionType);
            tdcShipment.TransactionTypeCode = transactionType.Code;

            TransactionSubType transactionSubType = TransactionSubTypeTests.PopulateNewItem();
            TransactionSubTypeTests.SaveItem(transactionSubType);
            tdcShipment.TransactionSubTypeCode = transactionSubType.Code;

            return tdcShipment;
        }

        //[Test]
        //public void PrintDeliveryNote()
        //{
        //    List<TDCShipment> shipments = new List<TDCShipment>();
        //    //add some shipments to print
        //    TDCShipment shipment1 = new TDCShipment();
        //    shipment1.DeliveryWarehouse = new Warehouse();
        //    //shipment1.DeliveryWarehouse.Printer = new Printer();
        //    shipment1.DeliveryWarehouse.PrinterName = "Lexmark C510 PS3";
        //    shipment1.CustomerName = "Paul Nichols";
        //    shipments.Add(shipment1);

        //    TDCShipment shipment2 = new TDCShipment();
        //    shipment1.DeliveryWarehouse = new Warehouse();
        //    //shipment1.DeliveryWarehouse.Printer = new Printer();
        //    shipment1.DeliveryWarehouse.PrinterName = "Lexmark C510 PS3";
        //    shipment1.CustomerName = "Lee Spring";
        //    shipments.Add(shipment2);

        //    int numberOfCopies = 2;
        //   TDCShipment.Print(shipments, numberOfCopies);
        //}

        [Test]
        public void ParseShipment()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                string[] files = Directory.GetFiles(@"..\..\ShipmentFiles", "*.dat");
                string[] fileContents = new string[files.Length];
                int i = 0;
                foreach (string file in files)
                {
                    using (StreamReader sr = new StreamReader(file))
                    {
                        fileContents[i] = sr.ReadToEnd();
                    }
                    i++;
                }

                ShipmentParsing parser;

                foreach (string content in fileContents)
                {
                    string x = content;

                    // Create a request message
                    RequestMessage contentMessage = new RequestMessage(x);

                    try
                    {
                        parser = new ShipmentParsing(null);
                        parser.ProcessRequest(contentMessage);
                        Assert.IsTrue(parser.Status != SubscriberStatusEnum.Failed);
                        //DeleteItem()
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(string.Format("The following Content caused an exception: '{0}'" + Environment.NewLine + content +
                                          Environment.NewLine), ex.Message);
                        Assert.IsTrue(false);
                    }


                }
            }
        }

        [Test]
        public void CalculateDeliveryDateWithWeekend()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //set-up two warehouses
                TDCShipment tdcShipment = new TDCShipment();

                Warehouse stockWarehouse = WarehouseTests.PopulateNewItem();
                WarehouseTests.SaveItem(stockWarehouse);
                tdcShipment.StockWarehouseCode = stockWarehouse.Code;

                Warehouse deliveryWarehouse = WarehouseTests.PopulateNewItem();
                WarehouseTests.SaveItem(deliveryWarehouse);
                tdcShipment.DeliveryWarehouseCode = deliveryWarehouse.Code;



                //set-up non working days
                //saturday
                NonWorkingDay nonWorkingDay = new NonWorkingDay();
                nonWorkingDay.NonWorkingDate = new DateTime(2006, 07, 29);
                nonWorkingDay.Description = "sat";
                nonWorkingDay.UpdatedBy = "me";
                nonWorkingDay.WarehouseId = tdcShipment.DeliveryWarehouse.Id;
                NonWorkingDayController.SaveNonWorkingDay(nonWorkingDay);

                //sunday
                nonWorkingDay = new NonWorkingDay();
                nonWorkingDay.NonWorkingDate = new DateTime(2006, 07, 30);
                nonWorkingDay.Description = "sun";
                nonWorkingDay.UpdatedBy = "me";
                nonWorkingDay.WarehouseId = tdcShipment.DeliveryWarehouse.Id;
                NonWorkingDayController.SaveNonWorkingDay(nonWorkingDay);

                //set-up trunker days
                TrunkerDay trunkerDay = new TrunkerDay();
                trunkerDay.Days = 3;
                trunkerDay.SourceWarehouseId = tdcShipment.StockWarehouse.Id;
                trunkerDay.DestinationWarehouseId = tdcShipment.DeliveryWarehouse.Id;
                trunkerDay.UpdatedBy = "me";
                trunkerDay.Id = TrunkerDaysController.SaveTrunkerDay(trunkerDay);


                tdcShipment.RequiredShipmentDate = new DateTime(2006, 07, 26);
                tdcShipment.OpCoCode = "HSP";
                tdcShipment.StockWarehouseCode = tdcShipment.StockWarehouse.Code;
                tdcShipment.DeliveryWarehouseCode = tdcShipment.DeliveryWarehouse.Code;
                tdcShipment.CalculateDeliveryDate();

                Assert.IsTrue(tdcShipment.EstimatedDeliveryDate == new DateTime(2006, 07, 31));
            }
        }

        [Test]
        public void CalculateDeliveryDateWithBankHolidays()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //set-up two warehouses

                TDCShipment tdcShipment = new TDCShipment();

                Warehouse stockWarehouse = WarehouseTests.PopulateNewItem();
                WarehouseTests.SaveItem(stockWarehouse);
                tdcShipment.StockWarehouseCode = stockWarehouse.Code;

                Warehouse deliveryWarehouse = WarehouseTests.PopulateNewItem();
                WarehouseTests.SaveItem(deliveryWarehouse);
                tdcShipment.DeliveryWarehouseCode = deliveryWarehouse.Code;



                //set-up non working days
                //saturday
                NonWorkingDay nonWorkingDay = new NonWorkingDay();
                nonWorkingDay.NonWorkingDate = new DateTime(2006, 07, 29);
                nonWorkingDay.Description = "sat";
                nonWorkingDay.UpdatedBy = "me";
                nonWorkingDay.WarehouseId = tdcShipment.DeliveryWarehouse.Id;
                NonWorkingDayController.SaveNonWorkingDay(nonWorkingDay);

                //sunday
                nonWorkingDay = new NonWorkingDay();
                nonWorkingDay.NonWorkingDate = new DateTime(2006, 07, 30);
                nonWorkingDay.Description = "sun";
                nonWorkingDay.UpdatedBy = "me";
                nonWorkingDay.WarehouseId = tdcShipment.DeliveryWarehouse.Id;
                NonWorkingDayController.SaveNonWorkingDay(nonWorkingDay);

                //bank 1
                nonWorkingDay = new NonWorkingDay();
                nonWorkingDay.NonWorkingDate = new DateTime(2006, 07, 25);
                nonWorkingDay.Description = "scot hol1";
                nonWorkingDay.UpdatedBy = "me";
                nonWorkingDay.WarehouseId = tdcShipment.DeliveryWarehouse.Id;
                NonWorkingDayController.SaveNonWorkingDay(nonWorkingDay);

                //bank 2
                nonWorkingDay = new NonWorkingDay();
                nonWorkingDay.NonWorkingDate = new DateTime(2006, 07, 26);
                nonWorkingDay.Description = "scot hol2";
                nonWorkingDay.UpdatedBy = "me";
                nonWorkingDay.WarehouseId = tdcShipment.DeliveryWarehouse.Id;
                NonWorkingDayController.SaveNonWorkingDay(nonWorkingDay);

                //set-up trunker days
                TrunkerDay trunkerDay = new TrunkerDay();
                trunkerDay.Days = 3;
                trunkerDay.SourceWarehouseId = tdcShipment.StockWarehouse.Id;
                trunkerDay.DestinationWarehouseId = tdcShipment.DeliveryWarehouse.Id;
                trunkerDay.UpdatedBy = "me";
                trunkerDay.Id = TrunkerDaysController.SaveTrunkerDay(trunkerDay);


                tdcShipment.RequiredShipmentDate = new DateTime(2006, 07, 26);
                tdcShipment.OpCoCode = "HSP";
                tdcShipment.CalculateDeliveryDate();

                Assert.IsTrue(tdcShipment.EstimatedDeliveryDate == new DateTime(2006, 07, 31));
            }
        }

        //[Test]
        //public void CalculateDeliveryDateWithBankHolidaysToIspwich()
        //{
        //    using (TransactionScope scope = new TransactionScope())
        //    {
        //        //set-up two warehouses

        //        TDCShipment tdcShipment = new TDCShipment();
        //        Warehouse stockWarehouse = WarehouseTests.PopulateNewItem();
        //        WarehouseTests.SaveItem(stockWarehouse);
        //        tdcShipment.StockWarehouseCode = stockWarehouse.Code;

        //        Warehouse deliveryWarehouse = WarehouseTests.PopulateNewItem();
        //        WarehouseTests.SaveItem(deliveryWarehouse);
        //        tdcShipment.DeliveryWarehouseCode = deliveryWarehouse.Code;



        //        //set-up non working days
        //        //saturday
        //        NonWorkingDay nonWorkingDay = new NonWorkingDay();
        //        nonWorkingDay.NonWorkingDate = new DateTime(2006, 07, 29);
        //        nonWorkingDay.Description = "sat";
        //        nonWorkingDay.UpdatedBy = "me";
        //        nonWorkingDay.WarehouseId = tdcShipment.DeliveryWarehouse.Id;
        //        NonWorkingDayController.SaveNonWorkingDay(nonWorkingDay);

        //        //sunday
        //        nonWorkingDay = new NonWorkingDay();
        //        nonWorkingDay.NonWorkingDate = new DateTime(2006, 07, 30);
        //        nonWorkingDay.Description = "sun";
        //        nonWorkingDay.UpdatedBy = "me";
        //        nonWorkingDay.WarehouseId = tdcShipment.DeliveryWarehouse.Id;
        //        NonWorkingDayController.SaveNonWorkingDay(nonWorkingDay);

        //        //bank hol
        //        nonWorkingDay = new NonWorkingDay();
        //        nonWorkingDay.NonWorkingDate = new DateTime(2006, 07, 25);
        //        nonWorkingDay.Description = "eng hol";
        //        nonWorkingDay.UpdatedBy = "me";
        //        nonWorkingDay.WarehouseId = tdcShipment.DeliveryWarehouse.Id;
        //        NonWorkingDayController.SaveNonWorkingDay(nonWorkingDay);

        //        //set-up trunker days
        //        TrunkerDay trunkerDay = new TrunkerDay();
        //        trunkerDay.Days = 3;
        //        trunkerDay.SourceWarehouseId = tdcShipment.StockWarehouse.Id;
        //        trunkerDay.DestinationWarehouseId = tdcShipment.DeliveryWarehouse.Id;
        //        trunkerDay.UpdatedBy = "me";
        //        trunkerDay.Id = TrunkerDaysController.SaveTrunkerDay(trunkerDay);


        //        tdcShipment.RequiredShipmentDate = new DateTime(2006, 07, 24);
        //        tdcShipment.OpCoCode = "HSP";
        //        tdcShipment.CalculateDeliveryDate();

        //        Assert.IsTrue(tdcShipment.EstimatedDeliveryDate == new DateTime(2006, 07, 28));
        //    }
        //}

        [Test]
        public void CalculateDeliveryDate()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                TDCShipment tdcShipment = new TDCShipment();

                //set-up two warehouses
                Warehouse stockWarehouse = WarehouseTests.PopulateNewItem();
                WarehouseTests.SaveItem(stockWarehouse);
                tdcShipment.StockWarehouseCode = stockWarehouse.Code;

                Warehouse deliveryWarehouse = WarehouseTests.PopulateNewItem();
                WarehouseTests.SaveItem(deliveryWarehouse);
                tdcShipment.DeliveryWarehouseCode = deliveryWarehouse.Code;

                //set-up trunker days
                TrunkerDay trunkerDay = new TrunkerDay();
                trunkerDay.Days = 3;
                trunkerDay.SourceWarehouseId = tdcShipment.StockWarehouse.Id;
                trunkerDay.DestinationWarehouseId = tdcShipment.DeliveryWarehouse.Id;
                trunkerDay.UpdatedBy = "me";
                trunkerDay.Id = TrunkerDaysController.SaveTrunkerDay(trunkerDay);


                tdcShipment.RequiredShipmentDate = new DateTime(2006, 07, 24);
                tdcShipment.OpCoCode = "HSP";
                tdcShipment.StockWarehouseCode = tdcShipment.StockWarehouse.Code;
                tdcShipment.DeliveryWarehouseCode = tdcShipment.DeliveryWarehouse.Code;
                tdcShipment.CalculateDeliveryDate();

                Assert.IsTrue(tdcShipment.EstimatedDeliveryDate == new DateTime(2006, 07, 27));
            }
        }

        [Test]
        public void CalculateDeliveryDateSameWarehouseNextDay()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //set-up two warehouses

                TDCShipment tdcShipment = new TDCShipment();

                Warehouse stockWarehouse = WarehouseTests.PopulateNewItem();
                WarehouseTests.SaveItem(stockWarehouse);
                tdcShipment.StockWarehouseCode = stockWarehouse.Code;
                tdcShipment.DeliveryWarehouseCode = stockWarehouse.Code;
                tdcShipment.RequiredShipmentDate = new DateTime(2006, 07, 24);
                Route route = RouteTests.PopulateNewItem();
                route.IsNextDay = true;
                route.IsSameDay = false;
                RouteTests.SaveItem(route);
                tdcShipment.RouteCode = route.Code;
                tdcShipment.OpCoCode = "HSP";

                tdcShipment.CalculateDeliveryDate();

                Assert.IsTrue(tdcShipment.EstimatedDeliveryDate == new DateTime(2006, 07, 25));
            }
        }

        [Test]
        public void CalculateDeliveryDateSameWarehouseSameDay()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //set-up two warehouses

                TDCShipment tdcShipment = new TDCShipment();

                Warehouse stockWarehouse = WarehouseTests.PopulateNewItem();
                WarehouseTests.SaveItem(stockWarehouse);
                tdcShipment.StockWarehouseCode = stockWarehouse.Code;
                tdcShipment.DeliveryWarehouseCode = stockWarehouse.Code;
                tdcShipment.RequiredShipmentDate = new DateTime(2006, 07, 24);
                tdcShipment.OpCoCode = "HSP";
                Route route = RouteTests.PopulateNewItem();
                route.IsNextDay = false;
                route.IsSameDay = true;
                RouteTests.SaveItem(route);
                tdcShipment.RouteCode = route.Code;

                tdcShipment.CalculateDeliveryDate();


                Assert.IsTrue(tdcShipment.EstimatedDeliveryDate == new DateTime(2006, 07, 24));
            }
        }
    }
}