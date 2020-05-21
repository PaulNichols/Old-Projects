using System;
using System.Collections.Generic;
using System.IO;
using System.Transactions;
using Discovery.BusinessObjects;
using Discovery.BusinessObjects.Controllers;
using Discovery.BusinessSubscribers;
using Discovery.ComponentServices.ExceptionHandling;
using Discovery.ComponentServices.Parsing;
using Discovery.RequestManagement;
using Discovery.RequestManagerClient;
using NUnit.Framework;

namespace Discovery.UnitTests
{
    [TestFixture]
    public class CommanderTests:BaseTest
    {

        //[Test]
        //public void SetCommanderProductMaintenanceAddSchema()
        //{
        //    TextFieldCollection fields = new TextFieldCollection();
        //    CommanderProduct.SetCommanderProductMaintenanceAddSchema(fields);
        //    //check number of fields
        //    Assert.IsTrue(fields.Count == 8);

        //    //check all expected fields have been set up
        //    Assert.IsTrue(fields["LineNumber"] != null);
        //    Assert.IsTrue(fields["RecordType"] != null);
        //    Assert.IsTrue(fields["RecordSubType"] != null);
        //    Assert.IsTrue(fields["ProductCode"] != null);
        //    Assert.IsTrue(fields["Description"] != null);
        //    Assert.IsTrue(fields["ShortDescription"] != null);
        //    Assert.IsTrue(fields["Account"] != null);
        //    Assert.IsTrue(fields["UOM"] != null);
            
        //    //check all fields have the expected length
        //    Assert.IsTrue(fields["LineNumber"].Length==5);
        //    Assert.IsTrue(fields["RecordType"].Length==3);
        //    Assert.IsTrue(fields["RecordSubType"].Length==1);
        //    Assert.IsTrue(fields["ProductCode"].Length==20);
        //    Assert.IsTrue(fields["Description"].Length==40);
        //    Assert.IsTrue(fields["ShortDescription"].Length==30);
        //    Assert.IsTrue(fields["Account"].Length==20);
        //    Assert.IsTrue(fields["UOM"].Length==10);
          
        //}
        ////[Test]
        ////public void GenerateLinesCSV()
        ////{
        ////    //fields.Add(new TextField("LineNumber", TypeCode.Int32, 5));
        ////    //fields.Add(new TextField("RecordType", TypeCode.Int32, 3));
        ////    //fields.Add(new TextField("RecordSubType", TypeCode.String, 1));
        ////    //fields.Add(new TextField("Site", TypeCode.String, 10));
        ////    //fields.Add(new TextField("OrderReference", TypeCode.String, 20));
        ////    //fields.Add(new TextField("LineNumber", TypeCode.Int32, 5));
        ////    //fields.Add(new TextField("ProductCode", TypeCode.String, 20));
        ////    //fields.Add(new TextField("QuantityOrdered", TypeCode.Int32, 19));
        ////    //fields.Add(new TextField("CustomerReferenceNumber", TypeCode.String, 20));
        ////    //fields.Add(new TextField("UOM", TypeCode.String, 10));
        ////    //fields.Add(new TextField("SpecialInstructions1", TypeCode.String, 33));
        ////    //fields.Add(new TextField("SpecialInstructions2", TypeCode.String, 33));
        ////    //fields.Add(new TextField("SpecialInstructions3", TypeCode.String, 33));
        ////    //fields.Add(new TextField("SpecialInstructions4", TypeCode.String, 33));
        ////    //fields.Add(new TextField("SpecialInstructions5", TypeCode.String, 33));

        ////    using (TransactionScope scope = new TransactionScope())
        ////    {
        ////        CommanderSalesOrderLine commanderSalesOrderLine=PopulateNewLineItem();
            
        ////        string line = commanderSalesOrderLine.GenerateLinesCSV();
        ////    }
        ////    //line.Substring()
        ////}
        
        //[Test]
        //public void SetCommanderProductMaintenanceModifySchema()
        //{
        //    TextFieldCollection fields = new TextFieldCollection();
        //    CommanderProduct.SetCommanderProductMaintenanceModifySchema(fields);
        //    //check number of fields
        //    Assert.IsTrue(fields.Count == 7);

        //    //check all expected fields have been set up
        //    Assert.IsTrue(fields["LineNumber"] != null);
        //    Assert.IsTrue(fields["RecordType"] != null);
        //    Assert.IsTrue(fields["RecordSubType"] != null);
        //    Assert.IsTrue(fields["ProductCode"] != null);
        //    Assert.IsTrue(fields["Description"] != null);
        //    Assert.IsTrue(fields["ShortDescription"] != null);
        //    Assert.IsTrue(fields["Account"] != null);
            

        //    //check all fields have the expected length
        //    Assert.IsTrue(fields["LineNumber"].Length == 5);
        //    Assert.IsTrue(fields["RecordType"].Length == 3);
        //    Assert.IsTrue(fields["RecordSubType"].Length == 1);
        //    Assert.IsTrue(fields["ProductCode"].Length == 20);
        //    Assert.IsTrue(fields["Description"].Length == 40);
        //    Assert.IsTrue(fields["ShortDescription"].Length == 30);
        //    Assert.IsTrue(fields["Account"].Length == 20);
            

        //}
        
        //#region Commander Product Properties

        //[Test]
        //public void Property_Account()
        //{
        //    string propertyToTest = "Account";
        //    object newValue = "123";
        //    List<string> propertiesToExclude = new List<string>(new string[] { propertyToTest, "ValidationMessages","ValidationMessagesAll","ViolationsDictionary" ,"Validator"});

        //    BasicPropertyTest<CommanderProduct>(newValue, propertiesToExclude, propertyToTest);
        //}

        //[Test]
        //public void Property_Description()
        //{
        //    string propertyToTest = "Description";
        //    object newValue = "Description";
        //    List<string> propertiesToExclude = new List<string>(new string[] { propertyToTest, "ValidationMessages","ValidationMessagesAll","ViolationsDictionary" ,"Validator"});

        //    BasicPropertyTest<CommanderProduct>(newValue, propertiesToExclude, propertyToTest);
        //}

        //[Test]
        //public void Property_ShortDescription()
        //{
        //    string propertyToTest = "ShortDescription";
        //    object newValue = "ShortDescription";
        //    List<string> propertiesToExclude = new List<string>(new string[] { propertyToTest, "ValidationMessages","ValidationMessagesAll","ViolationsDictionary" ,"Validator"});

        //    BasicPropertyTest<CommanderProduct>(newValue, propertiesToExclude, propertyToTest);
        //}

        //[Test]
        //public void Property_UOM()
        //{
        //    string propertyToTest = "UOM";
        //    object newValue = "UOM";
        //    List<string> propertiesToExclude = new List<string>(new string[] { propertyToTest, "ValidationMessages","ValidationMessagesAll","ViolationsDictionary" ,"Validator"});

        //    BasicPropertyTest<CommanderProduct>(newValue, propertiesToExclude, propertyToTest);
        //}
        
        //[Test]
        //public void Property_ProductCode()
        //{
        //    string propertyToTest = "ProductCode";
        //    object newValue = "ProductCode";
        //    List<string> propertiesToExclude = new List<string>(new string[] { propertyToTest, "ValidationMessages","ValidationMessagesAll","ViolationsDictionary" ,"Validator"});

        //    BasicPropertyTest<CommanderProduct>(newValue, propertiesToExclude, propertyToTest);
        //}

        //[Test]
        //public void Property_IsUpdate()
        //{
        //    object newValue = "M";

        //    CommanderProduct originalObject;
        //    List<string> propertiesToExclude;
        //    CommanderProduct objectToChange = SetRecordSubType(newValue, out originalObject, out propertiesToExclude);

        //    //check all properties except the ones specified above remain unchanged after the assignment of the test value
        //    //and also test the test property returns the correct test value
        //    Assert.IsTrue(PropertiesAreUnChanged < CommanderProduct>(originalObject, objectToChange, propertiesToExclude) && 
        //                  objectToChange.IsUpdate);
        //}

        //[Test]
        //public void Property_NotIsUpdate()
        //{
        //    object newValue = "C";

        //    CommanderProduct originalObject;
        //    List<string> propertiesToExclude;
        //    CommanderProduct objectToChange = SetRecordSubType(newValue, out originalObject, out propertiesToExclude);

        //    //check all properties except the ones specified above remain unchanged after the assignment of the test value
        //    //and also test the test property returns the correct test value
        //    Assert.IsTrue(PropertiesAreUnChanged<CommanderProduct>(originalObject, objectToChange, propertiesToExclude) && 
        //                  !objectToChange.IsUpdate);
        //}

        //private static CommanderProduct SetRecordSubType(object newValue, out CommanderProduct originalObject, out List<string> propertiesToExclude)
        //{
        //    string propertyToTest = "RecordSubType";

        //    propertiesToExclude = new List<string>(new string[] { propertyToTest,"IsUpdate", "ValidationMessages","ValidationMessagesAll","ViolationsDictionary" ,"Validator"});

        //    CommanderProduct objectToChange = new CommanderProduct();
        //    //clone so we can keep an original copy
        //    originalObject = objectToChange.DeepClone<CommanderProduct>();

        //    objectToChange.GetType().GetProperty(propertyToTest).SetValue(objectToChange, newValue, null);
        //    return objectToChange;
        //}

        //#endregion


        //#region Commander Sales Order Line tests

        //internal int SaveLineItem(CommanderSalesOrderLine commanderSalesOrderLine)
        //{
        //    return CommanderController.SaveSalesOrderLine(commanderSalesOrderLine);
        //}

        //internal CommanderSalesOrderLine PopulateNewLineItem()
        //{
        //    CommanderSalesOrderLine commanderSalesOrderLine = new CommanderSalesOrderLine();
        //    commanderSalesOrderLine.CustomerReferenceNumber = "Test";
        //    commanderSalesOrderLine.LineNumber = 2;
        //    commanderSalesOrderLine.OrderReference = "test";
        //    commanderSalesOrderLine.ProductCode = "test";
        //    commanderSalesOrderLine.QuantityOrdered = 3;
        //    commanderSalesOrderLine.Site = "test";
        //    commanderSalesOrderLine.SpecialInstructions1 = "test";
        //    commanderSalesOrderLine.SpecialInstructions2 = "test";
        //    commanderSalesOrderLine.SpecialInstructions3 = "test";
        //    commanderSalesOrderLine.SpecialInstructions4 = "test";
        //    commanderSalesOrderLine.SpecialInstructions5 = "test";

        //    commanderSalesOrderLine.CommanderSalesOrderId = SaveItem(PopulateNewItem());
        //    commanderSalesOrderLine.UOM = "meters";

        //    commanderSalesOrderLine.UpdatedBy = "test";
        //    return commanderSalesOrderLine;
        //}

        ////[Test]
        ////public void ParseAllCommanderMessages()
        ////{
        ////    string[] files = Directory.GetFiles(@"..\..\CommanderFiles\upload", "*.DAT");
        ////    string[] fileContents = new string[files.Length];
        ////    int i = 0;
        ////    foreach (string file in files)
        ////    {
        ////        using (StreamReader sr = new StreamReader(file))
        ////        {
        ////            fileContents[i] = sr.ReadToEnd();
        ////        }
        ////        i++;
        ////    }

        ////    CommanderParsingProcessor parser;

        ////    foreach (string content in fileContents)
        ////    {
        ////        string x = content;

        ////        // Create a request message
        ////        RequestMessage contentMessage = new RequestMessage(x);

        ////        try
        ////        {
        ////            parser = new CommanderParsingProcessor(null);
        ////            parser.ProcessRequest(contentMessage);
        ////            Assert.IsTrue(parser.Status != SubscriberStatusEnum.Failed);
        ////            //DeleteItem()
        ////        }
        ////        catch (CommanderParsingException)
        ////        {
        ////            Console.WriteLine("The following Content caused an exception:" + Environment.NewLine + content +
        ////                              Environment.NewLine);
        ////        }

        ////        catch (InValidBusinessObjectException)
        ////        {
        ////        }
        ////    }
        ////}
        
        //[Test]
        //public void ParseGoodsIn()
        //{
        //    string[] files = Directory.GetFiles(@"..\..\CommanderFiles\upload", "00015283.DAT");
        //    string[] fileContents = new string[files.Length];
        //    int i = 0;
        //    foreach (string file in files)
        //    {
        //        using (StreamReader sr = new StreamReader(file))
        //        {
        //            fileContents[i] = sr.ReadToEnd();
        //        }
        //        i++;
        //    }

        //    CommanderGoodsInParsing parser;

        //    foreach (string content in fileContents)
        //    {
        //        string x = content;

        //        // Create a request message
        //        RequestMessage contentMessage = new RequestMessage(x);

        //        try
        //        {
        //            parser = new CommanderGoodsInParsing(null);
        //            parser.ProcessRequest(contentMessage);
        //            Assert.IsTrue(parser.Status != SubscriberStatusEnum.Failed);
        //            //DeleteItem()
        //        }
        //        catch (CommanderParsingException)
        //        {
        //            Console.WriteLine("The following Content caused an exception:" + Environment.NewLine + content +
        //                              Environment.NewLine);
        //        }

        //        catch (InValidBusinessObjectException)
        //        {
        //        }
        //    }
        //}

        //[Test]
        //public void ParseStock()
        //{
        //    string[] files = Directory.GetFiles(@"..\..\CommanderFiles\upload", "00015282.DAT");
        //    string[] fileContents = new string[files.Length];
        //    int i = 0;
        //    foreach (string file in files)
        //    {
        //        using (StreamReader sr = new StreamReader(file))
        //        {
        //            fileContents[i] = sr.ReadToEnd();
        //        }
        //        i++;
        //    }

        //    CommanderStockParsing parser;

        //    foreach (string content in fileContents)
        //    {
        //        string x = content;

        //        // Create a request message
        //        RequestMessage contentMessage = new RequestMessage(x);

        //        try
        //        {
        //            parser = new CommanderStockParsing(null);
        //            parser.ProcessRequest(contentMessage);
        //            Assert.IsTrue(parser.Status == SubscriberStatusEnum.Processed);
        //            //DeleteItem()
        //        }
        //        catch (CommanderParsingException)
        //        {
        //            Console.WriteLine("The following Content caused an exception:" + Environment.NewLine + content +
        //                              Environment.NewLine);
        //        }

        //        catch (InValidBusinessObjectException)
        //        {
        //        }
        //    }
        //}

        //[Test]
        //public void ParseProductMaintenance()
        //{
        //    using (TransactionScope scope = new TransactionScope())
        //    {
        //        string[] files = Directory.GetFiles(@"..\..\CommanderFiles\upload", "*.DAT");
        //        string[] fileContents = new string[files.Length];
        //        int i = 0;
        //        foreach (string file in files)
        //        {
        //            using (StreamReader sr = new StreamReader(file))
        //            {
        //                fileContents[i] = sr.ReadToEnd();
        //            }
        //            i++;
        //        }

        //        CommanderProductMaintenanceParsing parser;

        //        foreach (string content in fileContents)
        //        {
        //            string x = content;

        //            // Create a request message
        //            RequestMessage contentMessage = new RequestMessage(x);

        //            try
        //            {
        //                parser = new CommanderProductMaintenanceParsing(null);
        //                parser.ProcessRequest(contentMessage);
        //                Assert.IsTrue(parser.Status == SubscriberStatusEnum.Processed);
        //                //DeleteItem()
        //            }
        //            catch (CommanderParsingException)
        //            {
        //                Console.WriteLine("The following Content caused an exception:" + Environment.NewLine + content +
        //                                  Environment.NewLine);
        //            }

        //            catch (InValidBusinessObjectException)
        //            {
        //            }
        //        }
        //    }
        //}

        //[Test]
        //public void ParseSalesOrders()
        //{
        //    using (TransactionScope scope = new TransactionScope())
        //    {
        //        string[] files = Directory.GetFiles(@"..\..\CommanderFiles\DownLoad", "*");
        //        string[] fileContents = new string[files.Length];
        //        int i = 0;
        //        foreach (string file in files)
        //        {
        //            using (StreamReader sr = new StreamReader(file))
        //            {
        //                fileContents[i] = sr.ReadToEnd();
        //            }
        //            i++;
        //        }

        //        CommanderSalesOrderParsing parser;

        //        foreach (string content in fileContents)
        //        {
        //            string x = content;

        //            // Create a request message
        //            RequestMessage contentMessage = new RequestMessage(x);

        //            try
        //            {
        //                parser = new CommanderSalesOrderParsing(null);
        //                parser.ProcessRequest(contentMessage);
        //                Assert.IsTrue(parser.Status == SubscriberStatusEnum.Processed);
        //                //DeleteItem()
        //            }
        //            catch (CommanderParsingException)
        //            {
        //                Console.WriteLine("The following Content caused an exception:" + Environment.NewLine + content +
        //                                  Environment.NewLine);
        //            }

        //            catch (InValidBusinessObjectException)
        //            {
        //            }
        //        }
        //    }
        //}

        //[Test]
        //public void SaveLineItem()
        //{
        //    CommanderSalesOrderLine commanderSalesOrderLine = PopulateNewLineItem();
        //    try
        //    {
        //        int id = SaveLineItem(commanderSalesOrderLine);
        //        Assert.IsTrue(id > 0);
        //        DeleteLineItem(id);
        //    }
        //    finally
        //    {
        //        DeleteItem(commanderSalesOrderLine.CommanderSalesOrderId);
        //    }
        //}

        //internal CommanderSalesOrderLine GetLineItem(int id)
        //{
        //    return CommanderController.GetSalesOrderLine(id);
        //}

        //[Test]
        //public void GetLineItem()
        //{
        //    CommanderSalesOrderLine commanderSalesOrderLine = PopulateNewLineItem();
        //    try
        //    {
        //        int id = SaveLineItem(commanderSalesOrderLine);
        //        Assert.IsNotNull(GetLineItem(id));
        //        DeleteLineItem(id);
        //    }
        //    finally
        //    {
        //        DeleteItem(commanderSalesOrderLine.CommanderSalesOrderId);
        //    }
        //}

        //[Test]
        //public void GetLineItems()
        //{
        //    CommanderSalesOrderLine commanderSalesOrderLine = PopulateNewLineItem();
        //    try
        //    {
        //        int id = SaveLineItem(commanderSalesOrderLine);
        //        Assert.IsTrue(CommanderController.GetSalesOrderLines(id, false).Count > 0);
        //        DeleteLineItem(id);
        //    }
        //    finally
        //    {
        //        DeleteItem(commanderSalesOrderLine.CommanderSalesOrderId);
        //    }
        //}

        //internal bool DeleteLineItem(int Id)
        //{
        //    return CommanderController.DeleteSalesOrderLine(Id);
        //}

        //[Test]
        //public void DeleteLineItem()
        //{
        //    CommanderSalesOrderLine commanderSalesOrderLine = PopulateNewLineItem();
        //    try
        //    {
        //        int id = SaveLineItem(commanderSalesOrderLine);
        //        Assert.IsTrue(DeleteLineItem(id));
        //    }
        //    finally
        //    {
        //        DeleteItem(commanderSalesOrderLine.CommanderSalesOrderId);
        //    }
        //}

        //#endregion

        //#region Commander Sales Order Tests

        //internal int SaveItem(CommanderSalesOrder commanderSalesOrder)
        //{
        //    return CommanderController.SaveSalesOrder(commanderSalesOrder);
        //}

        //internal CommanderSalesOrder PopulateNewItem()
        //{
        //    CommanderSalesOrder commanderSalesOrder = new CommanderSalesOrder();
        //    commanderSalesOrder.Carrier = "1";
        //    commanderSalesOrder.CustomerNumber = "";
        //    commanderSalesOrder.CustomerOrderReference = "";
        //    commanderSalesOrder.CustomerType = "2";
        //    commanderSalesOrder.DeliveryAddress = new Address();
        //    commanderSalesOrder.DeliveryAddress.Line1 = "1";
        //    commanderSalesOrder.DespatchRouteCode = "";
        //    commanderSalesOrder.DropNumber = 2;
        //    commanderSalesOrder.OrderReference = "";
        //    commanderSalesOrder.Site = "";
        //    commanderSalesOrder.TotalWeight = (decimal) 2.1;
        //    commanderSalesOrder.UpdatedBy = "test";
        //    commanderSalesOrder.UpdatedDate = DateTime.Today;
        //    return commanderSalesOrder;
        //}

        //[Test]
        //public void SaveItem()
        //{
        //    CommanderSalesOrder commanderSalesOrder = PopulateNewItem();
        //    try
        //    {
        //        int id = SaveItem(commanderSalesOrder);
        //        Assert.IsTrue(id > 0);
        //        DeleteItem(id);
        //    }
        //    finally
        //    {
        //    }
        //}


        //internal CommanderSalesOrder GetItem(int id, bool recursive)
        //{
        //    return CommanderController.GetSalesOrder(id, recursive);
        //}

        //[Test]
        //public void GetItem()
        //{
        //    CommanderSalesOrder commanderSalesOrder = PopulateNewItem();
        //    int id = SaveItem(commanderSalesOrder);

        //    try
        //    {
        //        Assert.IsNotNull(GetItem(id, false));
        //    }
        //    finally
        //    {
        //        DeleteItem(id);
        //    }
        //}

        //[Test]
        //[Ignore()]
        //public void GetItemRecursive()
        //{
        //    CommanderSalesOrder commanderSalesOrder = PopulateNewItem();
        //    int id = SaveItem(commanderSalesOrder);
        //    try
        //    {
        //        commanderSalesOrder = GetItem(id, true);
        //        Assert.IsNotNull(commanderSalesOrder);
        //        Assert.IsNotNull(commanderSalesOrder.Lines);
        //        Assert.IsNotNull(commanderSalesOrder.DeliveryAddress.Line1);
        //        Assert.IsTrue(commanderSalesOrder.Lines.Count > 0);
        //    }
        //    finally
        //    {
        //        DeleteItem(id);
        //    }
        //}

        //[Test]
        //public void GetItems()
        //{
        //    CommanderSalesOrder commanderSalesOrder = PopulateNewItem();
        //    int id = SaveItem(commanderSalesOrder);
        //    try
        //    {
        //        Assert.IsTrue(CommanderController.GetSalesOrders(false).Count > 0);
        //    }
        //    finally
        //    {
        //        DeleteItem(id);
        //    }
        //}

        //internal bool DeleteItem(int Id)
        //{
        //    return CommanderController.DeleteSalesOrder(Id);
        //}

        //[Test]
        //public void DeleteItem()
        //{
        //    CommanderSalesOrder commanderSalesOrder = PopulateNewItem();
        //    int id = SaveItem(commanderSalesOrder);
        //    Assert.IsTrue(DeleteItem(id));
        //}

        //#endregion
    }
}