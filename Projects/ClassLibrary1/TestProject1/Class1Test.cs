﻿// The following code was generated by Microsoft Visual Studio 2005.
// The test owner should check each test for validity.
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Collections.Generic;
using ClassLibrary1;
namespace TestProject1
{
    /// <summary>
    ///This is a test class for ClassLibrary1.Class1 and is intended
    ///to contain all ClassLibrary1.Class1 Unit Tests
    ///</summary>
    [TestClass()]
    public class Class1Test
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Equals(ByVal Object)
        ///</summary>
        [TestMethod()]
        public void EqualsTest()
        {
            Class1 target = new Class1();

            object obj = null; // TODO: Initialize to an appropriate value

            bool expected = false;
            bool actual;

            actual = target.Equals(obj);

            Assert.AreEqual(expected, actual, "ClassLibrary1.Class1.Equals did not return the expected value.");
        }

        /// <summary>
        ///A test for GetHashCode()
        ///</summary>
        [TestMethod()]
        public void GetHashCodeTest()
        {
            Class1 target = new Class1();

            int expected = 0;
            int actual;

            actual = target.GetHashCode();

            Assert.AreEqual(expected, actual, "ClassLibrary1.Class1.GetHashCode did not return the expected value.");
        }

        /// <summary>
        ///A test for Property1()
        ///</summary>
        [TestMethod()]
        public void Property1Test()
        {
            Class1 target = new Class1();

            int val = 1; // TODO: Assign to an appropriate value for the property

            target.Property1 = val;


            Assert.AreEqual(val, target.Property1, "ClassLibrary1.Class1.Property1 was not set correctly.");
        }

    }


}
