using System;
using System.Transactions;
using Discovery.BusinessObjects;
using Discovery.BusinessObjects.Controllers;
using Discovery.ComponentServices.ExceptionHandling;
using Discovery.Utility.DataAccess.Exceptions;
using NUnit.Framework;

namespace Discovery.UnitTests
{
    [TestFixture]
    public class OpcoShipmentTests
    {
        internal int SaveItem(OpCoShipment opCoShipment)
        {
            return OpCoShipmentController.SaveShipment(opCoShipment);
        }

        internal static OpCoShipment PopulateNewItem()
        {

            // Populate and Create an OpCo with default values
            OpCo opCo = OpcoTests.PopulateNewItem();
            opCo.Id = OpcoTests.SaveItem(opCo);

            OpCoShipment opCoShipment = new OpCoShipment();
            //populate item and lines
            opCoShipment.OpCoCode = opCo.Code;
            opCoShipment.OpCoSequenceNumber = 1;
            opCoShipment.OpCoContact.Email = "tdcteam@tdc.co.uk";
            opCoShipment.OpCoContact.Name = "TDC Test Team";
            opCoShipment.DespatchNumber = "Despatch";
            opCoShipment.RequiredShipmentDate = DateTime.Today;
            opCoShipment.TransactionTypeCode = "TransType";
            opCoShipment.RouteCode = "NHST";
            opCoShipment.CustomerNumber = "CustNo";
            opCoShipment.CustomerReference = "ref";
            opCoShipment.CustomerName = "Robert Horne Group Ltd.";
            opCoShipment.CustomerAddress.Line1 = "Mansion House";
            opCoShipment.CustomerAddress.PostCode = "NN3 6JL";
            opCoShipment.ShipmentNumber = "ShipNo";
            opCoShipment.ShipmentName = "Shipment Name";
            opCoShipment.ShipmentAddress.Line1 = "Shipment Address Line 1";
            opCoShipment.ShipmentAddress.PostCode = "NN3 6JL";
            opCoShipment.SalesBranchCode = "BBI";
            opCoShipment.AfterTime = "08:30:00";
            opCoShipment.BeforeTime = "23:59:59";
            opCoShipment.TailLiftRequired = true;
            opCoShipment.VehicleMaxWeight = (decimal) 1.1;
            opCoShipment.CheckInTime = 5;
            opCoShipment.DeliveryWarehouseCode = "HNH";
            opCoShipment.StockWarehouseCode = "XYZ";
            opCoShipment.DivisionCode = "00";
            opCoShipment.GeneratedDateTime = DateTime.Today;
            opCoShipment.Status = 0;
            opCoShipment.UpdatedDate = DateTime.Today;
            opCoShipment.UpdatedBy = "TDC Test Team";
            opCoShipment.Instructions = "Instructions";

            return opCoShipment;
        }

        [Test]
        public void SaveItem()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                OpCoShipment opCoShipment = PopulateNewItem();
                opCoShipment.Id = SaveItem(opCoShipment);
                Assert.IsTrue(opCoShipment.Id != -1);
            }
        }

        [Test]
        public void UpdateItem()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                OpCoShipment item = PopulateNewItem();

                item.CustomerName = "Original";
                item.Id = SaveItem(item);
                item = GetItem(item.Id, true);
                //change a value
                item.CustomerName = "Updated";

                SaveItem(item);
                item = GetItem(item.Id, true);
                Assert.IsTrue(item.CustomerName == "Updated");
            }
        }

       

        internal OpCoShipment GetItem(int id, bool recursive)
        {
            return OpCoShipmentController.GetShipment(id);
        }

        [Test]
        public void GetItem()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                OpCoShipment opCoShipment = PopulateNewItem();
                int id = SaveItem(opCoShipment);

          
                Assert.IsNotNull(GetItem(id, true));
                
                
            }
            
        }

        [Test]
        public void GetItemRecursive()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                OpCoShipment opCoShipment = PopulateNewItem();
                int id = SaveItem(opCoShipment);
          
                opCoShipment = GetItem(id, true);
                Assert.IsNotNull(opCoShipment);
                Assert.IsNotNull(opCoShipment.OpCoContact );
                
                
            }
           
        }

       

        internal bool DeleteItem(int Id)
        {
            OpCo opco = new OpCo();
            opco.Id = Id;
            return OpCoShipmentController.DeleteShipment(Id);
        }

        [Test]
        public void DeleteItem()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                OpCoShipment opCoShipment = PopulateNewItem();
                int id = SaveItem(opCoShipment);
                Assert.IsTrue(DeleteItem(id));
               
                
            }
            //call delete lines test
        }
    }
}