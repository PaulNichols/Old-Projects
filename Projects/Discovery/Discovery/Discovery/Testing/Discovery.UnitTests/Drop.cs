using System;
using System.Collections.Generic;
using System.Reflection;
using System.Transactions;
using Discovery.BusinessObjects;
using Discovery.BusinessObjects.Controllers;
using Discovery.ComponentServices.ExceptionHandling;
using Discovery.ComponentServices.Parsing;
using Discovery.Utility.DataAccess.Exceptions;
using NUnit.Framework;

namespace Discovery.UnitTests
{
    [TestFixture]
    public class DropTests : BaseTest
    {
        [Test]
        public void SetDropSchema()
        {
            TextFieldCollection fields=new TextFieldCollection();
            ShipmentDrop.SetDropSchema(fields);
            Assert.IsTrue(fields.Count==19);

            Assert.IsTrue(fields["TripNumber"]!=null);
            Assert.IsTrue(fields["OrderSequence"] != null);
            Assert.IsTrue(fields["OriginalCustomerCode"] != null);
            Assert.IsTrue(fields["ShipmentNumberAndDespatch"] != null);
            Assert.IsTrue(fields["Weight"] != null);
            Assert.IsTrue(fields["Volume"] != null);
            Assert.IsTrue(fields["ArriveTime"] != null);
            Assert.IsTrue(fields["DepartTime"] != null);
            Assert.IsTrue(fields["LoadingTime"] != null);
            Assert.IsTrue(fields["WaitingTime"] != null);
            Assert.IsTrue(fields["TravellingTime"] != null);
            Assert.IsTrue(fields["Distance"] != null);
            Assert.IsTrue(fields["CallType"] != null);
            Assert.IsTrue(fields["MatrixCost"] != null);
            Assert.IsTrue(fields["WorstError"] != null);
            Assert.IsTrue(fields["DropSequence"] != null);
            Assert.IsTrue(fields["RegionAndRoute"] != null);
            Assert.IsTrue(fields["OriginalDepot"] != null);
            Assert.IsTrue(fields["DeliveryDate"] != null);
        }
        
        [Test]
        public void Property_Volume()
        {
            string propertyToTest = "Volume";
            object newValue = new Random(1).Next();
            List<string> propertiesToExclude = new List<string>(new string[] { propertyToTest, "ValidationMessages","ValidationMessagesAll","ViolationsDictionary" ,"Validator"});

            BasicPropertyTest<ShipmentDrop>(newValue, propertiesToExclude, propertyToTest);
        }

        [Test]
        public void Property_ArriveTime()
        {
            string propertyToTest = "ArriveTime";
            object newValue = DateTime.Now.ToShortTimeString();
            List<string> propertiesToExclude = new List<string>(new string[] { propertyToTest, "ValidationMessages","ValidationMessagesAll","ViolationsDictionary" ,"Validator"});

            BasicPropertyTest<ShipmentDrop>(newValue, propertiesToExclude, propertyToTest);
        }

        [Test]
        public void Property_CallType()
        {
            string propertyToTest = "CallType";
            object newValue = Discovery.BusinessObjects.ShipmentDrop.CallTypeEnum.Collection;
            List<string> propertiesToExclude = new List<string>(new string[] { propertyToTest, "ValidationMessages","ValidationMessagesAll","ViolationsDictionary" ,"Validator"});

            BasicPropertyTest<ShipmentDrop>(newValue, propertiesToExclude, propertyToTest);
        }

        [Test]
        public void Property_TripId()
        {
            string propertyToTest = "TripId";
            object newValue = new Random(1).Next();
            List<string> propertiesToExclude = new List<string>(new string[] { propertyToTest, "ValidationMessages","ValidationMessagesAll","ViolationsDictionary" ,"Validator"});

            BasicPropertyTest<ShipmentDrop>(newValue, propertiesToExclude, propertyToTest);
        }
        
         [Test]
        public void Property_WaitingTime()
        {
            string propertyToTest = "WaitingTime";
            object newValue = (decimal)12.13;
            List<string> propertiesToExclude = new List<string>(new string[] { propertyToTest, "ValidationMessages","ValidationMessagesAll","ViolationsDictionary" ,"Validator"});

            BasicPropertyTest<ShipmentDrop>(newValue, propertiesToExclude, propertyToTest);
        }

        [Test]
        public void Property_DepartTime()
        {
            string propertyToTest = "DepartTime";
            object newValue = DateTime.Now.ToShortTimeString();
            List<string> propertiesToExclude = new List<string>(new string[] { propertyToTest, "ValidationMessages","ValidationMessagesAll","ViolationsDictionary" ,"Validator"});

            BasicPropertyTest<ShipmentDrop>(newValue, propertiesToExclude, propertyToTest);
        }

        [Test]
        public void Property_Weight()
        {
            string propertyToTest = "Weight";
            object newValue = new Random(1).Next();
            List<string> propertiesToExclude = new List<string>(new string[] { propertyToTest, "ValidationMessages","ValidationMessagesAll","ViolationsDictionary" ,"Validator"});

            BasicPropertyTest<ShipmentDrop>(newValue, propertiesToExclude, propertyToTest);
        }

        [Test]
        public void Property_TravellingTime()
        {
            string propertyToTest = "TravellingTime";
            object newValue = (decimal)2.13;
            List<string> propertiesToExclude = new List<string>(new string[] { propertyToTest, "ValidationMessages","ValidationMessagesAll","ViolationsDictionary" ,"Validator"});

            BasicPropertyTest<ShipmentDrop>(newValue, propertiesToExclude, propertyToTest);
        }

        [Test]
        public void Property_Distance()
        {
            string propertyToTest = "Distance";
            object newValue = (decimal)1.1;
            List<string> propertiesToExclude = new List<string>(new string[] { propertyToTest, "ValidationMessages","ValidationMessagesAll","ViolationsDictionary" ,"Validator"});

            BasicPropertyTest<ShipmentDrop>(newValue, propertiesToExclude, propertyToTest);
        }

        [Test]
        public void Property_Sequenece()
        {
            string propertyToTest = "OrderSequence";
            object newValue = new Random(1).Next();
            List<string> propertiesToExclude = new List<string>(new string[] { propertyToTest, "ValidationMessages","ValidationMessagesAll","ViolationsDictionary" ,"Validator"});

            BasicPropertyTest<ShipmentDrop>(newValue, propertiesToExclude, propertyToTest);
        }

       

        [Test]
        public void Property_DropSequenceNumber()
        {
            string propertyToTest = "DropSequence";
            object newValue = new Random(1).Next();
            List<string> propertiesToExclude = new List<string>(new string[] { propertyToTest, "ValidationMessages","ValidationMessagesAll","ViolationsDictionary" ,"Validator"});

            BasicPropertyTest<ShipmentDrop>(newValue, propertiesToExclude, propertyToTest);
        }

        [Test]
        public void Property_LoadingTime()
        {
            string propertyToTest = "LoadingTime";
            object newValue = (decimal)1.1;
            List<string> propertiesToExclude = new List<string>(new string[] { propertyToTest, "ValidationMessages","ValidationMessagesAll","ViolationsDictionary" ,"Validator"});

            BasicPropertyTest<ShipmentDrop>(newValue, propertiesToExclude, propertyToTest);
        }

        [Test]
        public void Property_OriginalCustomerCode()
        {
            string propertyToTest = "OriginalCustomerCode";
            object newValue = "test";
            List<string> propertiesToExclude = new List<string>(new string[] { propertyToTest, "ValidationMessages","ValidationMessagesAll","ViolationsDictionary" ,"Validator"});

            BasicPropertyTest<ShipmentDrop>(newValue, propertiesToExclude, propertyToTest);
        }

        [Test]
        public void Property_OriginalDepot()
        {
            string propertyToTest = "OriginalDepot";
            object newValue = "test";
            List<string> propertiesToExclude = new List<string>(new string[] { propertyToTest, "ValidationMessages","ValidationMessagesAll","ViolationsDictionary" ,"Validator"});

            BasicPropertyTest<ShipmentDrop>(newValue, propertiesToExclude, propertyToTest);
        }
    }
}