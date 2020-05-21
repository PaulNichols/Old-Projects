using Discovery.BusinessObjects;
using ValidationFramework;

namespace Discovery.UI.Web.Controllers
{
    public static class WarehouseController
    {
        public static int SaveWarehouse(UIWarehouse warehouse)
        {
            return BusinessObjects.Controllers.WarehouseController.SaveWarehouse(warehouse);
        }

        //public static List<TDCShipment> GetShipments()
        //{
        //    List<TDCShipment> shipments = new List<TDCShipment>();
        //    //add some shipments to print
        //    TDCShipment shipment1 = new TDCShipment();
        //    shipment1.DeliveryWarehouse = new Warehouse();
        //    //shipment1.DeliveryWarehouse.Printer = new Printer();
        //    shipment1.DeliveryWarehouse.PrinterName = "Lexmark C510 PS3";
        //    shipment1.CustomerName = "Paul Nichols";
        //    shipment1.ShipmentNumber = "666";
        //    shipments.Add(shipment1);

        //    TDCShipment shipment2 = new TDCShipment();
        //    shipment2.DeliveryWarehouse = new Warehouse();
        //    //shipment2.DeliveryWarehouse.Printer = new Printer();
        //    shipment2.DeliveryWarehouse.PrinterName = "Lexmark C510 PS3";
        //    shipment2.CustomerName = "Lee Spring";
        //    shipment2.ShipmentNumber = "999";
        //    shipments.Add(shipment2);
        //    return shipments;
        //}

        public static bool DeleteWarehouse(UIWarehouse warehouse)
        {
            return BusinessObjects.Controllers.WarehouseController.DeleteWarehouse(warehouse);
        }

        public static UIWarehouse GetWarehouse(int warehouseId)
        {
            Warehouse warehouse = BusinessObjects.Controllers.WarehouseController.GetWarehouse(warehouseId);
            if (warehouse != null)
            {
                return new UIWarehouse(warehouse);
            }
            else
            {
                return null;
            }
        }
    }
    /// <summary>
    /// This wrapper class helps with UI binding to Contact and Address details
    /// </summary>
    public class UIWarehouse : Warehouse
    {
        public UIWarehouse() : base()
        {
        }

        public UIWarehouse(Warehouse warehouse) : base()
        {
            if (warehouse != null)
            {
                Description = warehouse.Description;
                Code = warehouse.Code;
                PrinterName = warehouse.PrinterName;
                HasCommander = warehouse.HasCommander;
                HasOptrak = warehouse.HasOptrak;
                Address = warehouse.Address;
                Contact = warehouse.Contact;
                Code = warehouse.Code;
                CheckSum = warehouse.CheckSum;
                Id = warehouse.Id;
                IsArchived = warehouse.IsArchived;
                IsTDC = warehouse.IsTDC;
                //   Printer = warehouse.Printer;
               // PrinterName = warehouse.PrinterName;
                OptrakRegion = warehouse.OptrakRegion;
                RegionId = warehouse.RegionId;
                UpdatedBy = warehouse.UpdatedBy;
                UpdatedDate = warehouse.UpdatedDate;
            }
        }

        [RequiredValidator("Email is required.", "*")]
        [LengthValidator(50, "The maximum length of a Email is 50 characters.", "*")]
        [RegexValidator(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", "Email is Invalid", "*")]
        public string SalesEmail
        {
            get { return Contact.Email; }
            set { Contact.Email = value; }
        }

        [RequiredValidator("Contact Telephone Number is required.", "*")]
        [LengthValidator(50, "The maximum length of a Contact Telephone Number is 50 characters.", "*")]
        [RegexValidator("^([0-9,\\s])*", "Telephone should be in numeric", "*")]
        public string ContactTelephoneNumber
        {
            get { return Contact.TelephoneNumber; }
            set { Contact.TelephoneNumber = value; }
        }

        [RequiredValidator("Contact Name is required.", "*")]
        [LengthValidator(50, "The maximum length of a Contact Name is 50 characters.", "*")]
        public string ContactName
        {
            get { return Contact.Name; }
            set { Contact.Name = value; }
        }

        [RequiredValidator("Description is required.", "*")]
        [LengthValidator(256, "The maximum length of a Description is 256 characters.", "*")]
        public string AddressLine1
        {
            get { return Address.Line1; }
            set { Address.Line1 = value; }
        }

        public string AddressLine2
        {
            get { return Address.Line2; }
            set { Address.Line2 = value; }
        }

        public string AddressLine3
        {
            get { return Address.Line3; }
            set { Address.Line3 = value; }
        }

        public string AddressLine4
        {
            get { return Address.Line4; }
            set { Address.Line4 = value; }
        }

        [RequiredValidator("Post Code is required.", "*")]
        [LengthValidator(15, "The maximum length of a Post Code is 15 characters.", "*")]
        [
            RegexValidator(
                @"^([A-PR-UWYZ0-9][A-HK-Y0-9][AEHMNPRTVXY0-9]?[ABEHMNPRVWXY0-9]? {1,2}[0-9][ABD-HJLN-UW-Z]{2}|GIR 0AA)",
                "The Post Code is Invalid", "*")]
        public string PostCode
        {
            get { return Address.PostCode; }
            set { Address.PostCode = value; }
        }
    }
}