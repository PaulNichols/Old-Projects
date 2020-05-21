using System;
using System.Collections.Generic;
using System.Reflection;
using System.Transactions;
using Discovery.BusinessObjects;
using Discovery.BusinessObjects.Controllers;
using Discovery.ComponentServices.ExceptionHandling;
using Discovery.Utility.DataAccess.Exceptions;
using NUnit.Framework;

namespace Discovery.UnitTests
{
    [TestFixture]
    public class MappingTests
    {


        internal Mapping PopulateMappingItem()
        {
            Mapping mapping = new Mapping();
            //Add two mapping systems
            mapping.DestinationSystem = PopulateNewDestinationMappingSystem();
            mapping.DestinationSystem.Id = MappingController.SaveMappingSystem(mapping.DestinationSystem);
            mapping.DestinationSystemId = mapping.DestinationSystem.Id;


            mapping.SourceSystem = PopulateNewSourceMappingSystem();
            mapping.SourceSystem.Id = MappingController.SaveMappingSystem(mapping.SourceSystem);
            mapping.SourceSystemId = mapping.SourceSystem.Id;

            //add a mapping property association, which describes when properties can be mapped on the above type
            mapping.MappingPropertyAssociation = PopulateNewMappingPropertyAssocation();
            mapping.MappingPropertyAssociation.Id = MappingController.SaveMappingPropertyAssociation(mapping.MappingPropertyAssociation);
            mapping.MappingPropertyAssociationId = mapping.MappingPropertyAssociation.Id;

            mapping.DestinationValue = "Test";
            mapping.SourceValue = "test";
            mapping.UpdatedBy = "test";
            return mapping;
        }

        [Test]
        public void SaveSourceMappingSystem()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (MappingController.DeleteAllMappings())
                {
                    if (MappingController.DeleteAllMappingSystems())
                    {
                        Assert.IsTrue(MappingController.SaveMappingSystem(PopulateNewSourceMappingSystem()) != -1);
                    }
                }

            }


        }

        [Test]
        public void SaveDestinationMappingSystem()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (MappingController.DeleteAllMappings())
                {
                    if (MappingController.DeleteAllMappingSystems())
                    {
                        Assert.IsTrue(MappingController.SaveMappingSystem(PopulateNewDestinationMappingSystem()) != -1);
                    }
                }

            }
        }

        [Test]
        public void SaveDestinationMappingClassAssociation()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (MappingController.DeleteAllMappings())
                {
                    if (MappingController.DeleteAllMappingSystems())
                    {
                        if (MappingController.DeleteAllMappingPropertyAssociations())
                        {
                            if (MappingController.DeleteAllMappingClassAssociations())
                            {
                                Assert.IsTrue(MappingController.SaveMappingClassAssociation(PopulateNewMappingClassAssocation()) != -1);
                            }
                        }
                    }
                }
            }
        }

        [Test]
        public void SaveMappingPropertyAssociation()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (MappingController.DeleteAllMappings())
                {
                    if (MappingController.DeleteAllMappingSystems())
                    {
                        if (MappingController.DeleteAllMappingPropertyAssociations())
                        {
                            if (MappingController.DeleteAllMappingClassAssociations())
                            {
                                Assert.IsTrue(MappingController.SaveMappingPropertyAssociation(PopulateNewMappingPropertyAssocation()) != -1);
                            }
                        }
                    }
                }
            }
        }

        private MappingPropertyAssociation PopulateNewMappingPropertyAssocation()
        {
            MappingPropertyAssociation propertyAssociation = new MappingPropertyAssociation();
            //setup the class association which defines the source and destination types the source and destination properties will belong to
            propertyAssociation.MappingClassAssociation = PopulateNewMappingClassAssocation();
            propertyAssociation.MappingClassAssociation.Id = MappingController.SaveMappingClassAssociation(propertyAssociation.MappingClassAssociation);
            propertyAssociation.MappingClassAssociationId = propertyAssociation.MappingClassAssociation.Id;

            propertyAssociation.DestinationProperty = "RouteCode";
            propertyAssociation.SourceProperty = "RouteCode";
            propertyAssociation.LookupTableName = "Discovery_Route";
            propertyAssociation.LookUpTableDisplayColumn = "Description";

            return propertyAssociation;
        }

        [Test]
        [ExpectedException(typeof(DiscoveryException))]
        public void SaveDestinationMappingClassAssociationConstraint()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                MappingController.DeleteAllMappings();
                MappingController.DeleteAllMappingSystems();
                MappingController.DeleteAllMappingPropertyAssociations();
                MappingController.DeleteAllMappingClassAssociations();

                MappingClassAssociation classAssociation = PopulateNewMappingClassAssocation();

                MappingController.SaveMappingClassAssociation(classAssociation);
                MappingController.SaveMappingClassAssociation(classAssociation);
            }
        }

        private MappingClassAssociation PopulateNewMappingClassAssocation()
        {
            MappingClassAssociation mappingClassAssociation = new MappingClassAssociation();
            mappingClassAssociation.SourceType = "OpCoShipment";
            mappingClassAssociation.DestinationType = "TDCShipment";
            mappingClassAssociation.SourceTypeFullName = "Discovery.BusinessObjects.OpCoShipment";
            mappingClassAssociation.DestinationTypeFullName = "Discovery.BusinessObjects.TDCShipment";
            return mappingClassAssociation;
        }

        [Test]
        [ExpectedException(typeof(DiscoveryException))]
        public void SaveDestinationMappingSystemConstraint()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                MappingController.DeleteAllMappings();
                MappingController.DeleteAllMappingSystems();
                MappingSystem system = PopulateNewDestinationMappingSystem();
                if (MappingController.SaveMappingSystem(system) != -1)
                {
                    MappingController.SaveMappingSystem(system);
                }
            }



        }

        private MappingSystem PopulateNewDestinationMappingSystem()
        {
            MappingSystem mappingSystem = new MappingSystem();
            mappingSystem.IsDestination = true;
            mappingSystem.IsSource = false;
            mappingSystem.Name = "TDC";
            return mappingSystem;
        }

        private MappingSystem PopulateNewSourceMappingSystem()
        {
            MappingSystem mappingSystem = new MappingSystem();
            mappingSystem.IsDestination = false;
            mappingSystem.IsSource = true;
            mappingSystem.Name = "RHG";
            return mappingSystem;
        }

        [Test]
        public void DeleteAllMappings()
        {

            using (TransactionScope scope = new TransactionScope())
            {
                MappingController.DeleteAllMappings();
                MappingController.DeleteAllMappingSystems();
                MappingController.DeleteAllMappingPropertyAssociations();
                MappingController.DeleteAllMappingClassAssociations();

                Mapping mapping = PopulateMappingItem();
                int id = MappingController.SaveMapping(mapping);
                Assert.IsTrue(id != -1);
                Assert.IsTrue(MappingController.DeleteAllMappings());

            }
        }

        [Test]
        public void DeleteAllMappingSystems()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (MappingController.DeleteAllMappings())
                {
                    Assert.IsTrue(MappingController.DeleteAllMappingSystems());
                }

            }
        }

        [Test]
        public void DeleteAllMappingPropertyAssociations()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (MappingController.DeleteAllMappings())
                {
                    if (MappingController.DeleteAllMappingSystems())
                    {
                        Assert.IsTrue(MappingController.DeleteAllMappingPropertyAssociations());
                    }
                }

            }
        }

        [Test]
        public void DeleteAllMappingClassAssociations()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (MappingController.DeleteAllMappings())
                {
                    if (MappingController.DeleteAllMappingSystems())
                    {
                        if (MappingController.DeleteAllMappingPropertyAssociations())
                        {
                            Assert.IsTrue(MappingController.DeleteAllMappingClassAssociations());
                        }
                    }
                }

            }
        }

        [Test]
        public void SaveItem()
        {
            using (TransactionScope scope = new TransactionScope())
            {

                if (MappingController.DeleteAllMappings())
                {
                    if (MappingController.DeleteAllMappingSystems())
                    {
                        if (MappingController.DeleteAllMappingPropertyAssociations())
                        {
                            if (MappingController.DeleteAllMappingClassAssociations())
                            {
                                Mapping mapping = PopulateMappingItem();
                                int id = MappingController.SaveMapping(mapping);
                                Assert.IsTrue(id != -1);
                            }
                        }
                    }
                }



            }
        }

        [Test]
        [ExpectedException(typeof(DiscoveryException))]
        public void SaveMappingTestConstraint()
        {
            using (TransactionScope scope = new TransactionScope())
            {

                MappingController.DeleteAllMappings();
                MappingController.DeleteAllMappingSystems();
                MappingController.DeleteAllMappingPropertyAssociations();
                MappingController.DeleteAllMappingClassAssociations();
                Mapping mapping = PopulateMappingItem();

                MappingController.SaveMapping(mapping);

                MappingController.SaveMapping(mapping);
                
            }

        }

        [Test]
        public void UpdateItem()
        {
            using (TransactionScope scope = new TransactionScope())
            {

                if (MappingController.DeleteAllMappings())
                {
                    if (MappingController.DeleteAllMappingSystems())
                    {
                        if (MappingController.DeleteAllMappingPropertyAssociations())
                        {
                            if (MappingController.DeleteAllMappingClassAssociations())
                            {
                                Mapping item = PopulateMappingItem();
                                item.SourceValue = "Original";
                                item.Id = MappingController.SaveMapping(item);
                                item = GetItem(item.Id);
                                //change a value
                                item.SourceValue = "Updated";

                                MappingController.SaveMapping(item);
                                item = GetItem(item.Id);
                                Assert.IsTrue(item.SourceValue == "Updated");
                            }
                        }
                    }
                }



            }


        }

        [Test]
        [ExpectedException(typeof(DiscoveryException))]
        public void ConcurrencyTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                MappingController.DeleteAllMappings();
                MappingController.DeleteAllMappingSystems();
                MappingController.DeleteAllMappingPropertyAssociations();
                MappingController.DeleteAllMappingClassAssociations();

                try
                {
                    Mapping item = PopulateMappingItem();
                    item.Id = MappingController.SaveMapping(item);
                    //change a value
                    item.MappingPropertyAssociation.DestinationProperty = "Updated";

                    MappingController.SaveMapping(item);
                }
                catch (DiscoveryException e)
                {
                    Assert.IsInstanceOfType(typeof(ConcurrencyException), e.InnerException);
                    throw e;
                }
            }

        }

        internal static Mapping GetItem(int id)
        {
            return MappingController.GetMapping(id);
        }

        [Test]
        public void GetItem()
        {

            using (TransactionScope scope = new TransactionScope())
            {
                if (MappingController.DeleteAllMappings())
                {
                    if (MappingController.DeleteAllMappingSystems())
                    {
                        if (MappingController.DeleteAllMappingPropertyAssociations())
                        {
                            if (MappingController.DeleteAllMappingClassAssociations())
                            {
                                int id = MappingController.SaveMapping(PopulateMappingItem());

                                Assert.IsNotNull(GetItem(id));
                            }

                        }
                    }
                }



            }

        }

        [Test]
        public void GetMappingLookup()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (MappingController.DeleteAllMappings())
                {
                    if (MappingController.DeleteAllMappingSystems())
                    {
                        if (MappingController.DeleteAllMappingPropertyAssociations())
                        {
                            if (MappingController.DeleteAllMappingClassAssociations())
                            {
                                Mapping mapping = PopulateMappingItem();
                                if (MappingController.SaveMapping(mapping) != -1)
                                {
                                    //we have set the lookup table to be route so we should add a route and then when we get the lookup list
                                    //back it should include the RouteCode we save in the routing table
                                    
                                     Route route = RouteTests.PopulateNewItem();
                                     if (RouteTests.SaveItem(route) != -1)
                                    {
                                        //get a list of route codes from the route table
                                        Dictionary<string, string> lookupList = MappingController.GetMappingLookup(mapping.MappingPropertyAssociationId);
                                        //so the count should be >0
                                        Assert.IsTrue(lookupList.Count > 0);
                                        //check for our new id
                                        Assert.IsTrue(lookupList.ContainsValue(route.Description));
                                    }
                                }
                            }

                        }
                    }
                }
            }


        }

        [Test]
        public void GetAllSubClasses()
        {
            List<Type> types = MappingController.GetAllMapableTypes();
            Assert.Greater(types.Count, 0);
            Console.WriteLine(types.Count.ToString() + " Types were returned");
            foreach (Type type in types)
            {
                Console.WriteLine(type);
            }
        }

        [Test]
        [Ignore()]
        public void GetAllDestinationMappingSystems()
        {

            using (TransactionScope scope = new TransactionScope())
            {
                if (MappingController.DeleteAllMappings())
                {
                    if (MappingController.DeleteAllMappingSystems())
                    {
                        MappingSystem mappingSystem = PopulateNewDestinationMappingSystem();
                        mappingSystem.Id = MappingController.SaveMappingSystem(mappingSystem);
                        if (mappingSystem.Id != -1)
                        {
                            //retrieve all mapping Systems and the one we saved should return at least
                            List<MappingSystem> mappingSystems = MappingController.GetMappingDestinationSystems();

                            //so the count should be >0
                            Assert.IsTrue(mappingSystems.Count > 0);
                            //check for our new id
                            Assert.IsTrue(mappingSystems.Find(delegate(MappingSystem currentItem)
                                                              {
                                                                  return currentItem.Id == mappingSystem.Id;
                                                              }) != null);
                        }
                    }
                }

            }

        }

        [Test]
        public void GetAllSourceMappingSystems()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (MappingController.DeleteAllMappings())
                {
                    if (MappingController.DeleteAllMappingSystems())
                    {
                        MappingSystem mappingSystem = PopulateNewSourceMappingSystem();
                        mappingSystem.Id = MappingController.SaveMappingSystem(mappingSystem);
                        if (mappingSystem.Id != -1)
                        {
                            //retrieve all mapping Systems and the one we saved should return at least
                            List<MappingSystem> mappingSystems = MappingController.GetMappingSourceSystems();

                            //so the count should be >0
                            Assert.IsTrue(mappingSystems.Count > 0);
                            //check for our new id
                            Assert.IsTrue(mappingSystems.Find(delegate(MappingSystem currentItem)
                                                              {
                                                                  return currentItem.Id == mappingSystem.Id;
                                                              }) != null);
                        }
                    }
                }

            }
        }

        [Test]
        public void GetClassesPropertiesReadable()
        {
            List<Type> types = MappingController.GetAllMapableTypes();
            if (types.Count > 0)
            {
                List<PropertyInfo> properties = MappingController.GetClassesPropertiesReadable(types[0].FullName);
                Assert.Greater(properties.Count, 0);
                Console.WriteLine(properties.Count.ToString() + " Properties were returned");
            }
        }

        [Test]
        public void GetClassesPropertiesWritable()
        {
            List<Type> types = MappingController.GetAllMapableTypes();
            if (types.Count > 0)
            {
                List<PropertyInfo> properties = MappingController.GetClassesPropertiesWritable(types[0].FullName);
                Assert.Greater(properties.Count, 0);

                Console.WriteLine(properties.Count.ToString() + " Properties were returned");
            }
        }


        [Test]
        public void Map()
        {
            using (TransactionScope scope = new TransactionScope())
            {


                if (MappingController.DeleteAllMappings())
                {
                    if (MappingController.DeleteAllMappingSystems())
                    {
                        if (MappingController.DeleteAllMappingPropertyAssociations())
                        {
                            if (MappingController.DeleteAllMappingClassAssociations())
                            {
                                //add a mapping for route code
                                Mapping mapping;
                                mapping = PopulateMappingItem();
                                mapping.SourceValue = "OpcoValue";
                                mapping.DestinationValue = "TDCValue";
                                MappingController.SaveMapping(mapping);


                                //set up source object to map to destination
                                OpCoShipment sourceObject = OpcoShipmentTests.PopulateNewItem();
                                sourceObject.RouteCode = mapping.SourceValue;

                                //set up a new instance of a TDC shipment to map the new values into
                                TDCShipment destinationObject = new TDCShipment();

                                //perform the mapping
                                MappingController.Map(sourceObject, destinationObject, mapping.SourceSystem.Name, mapping.DestinationSystem.Name, DoLines);


                                Assert.IsTrue
                                    (
                                    destinationObject.GetType().GetProperty(mapping.MappingPropertyAssociation.DestinationProperty).
                                        GetValue(destinationObject,
                                                 null).Equals(
                                        mapping.DestinationValue) &&
                                    ((sourceObject.ShipmentLines == null || sourceObject.ShipmentLines.Count == 0) || destinationObject.ShipmentLines.Count > 0) &&
                                    ((destinationObject.ShipmentLines == null || destinationObject.ShipmentLines.Count == 0) || destinationObject.ShipmentLines[0] is TDCShipmentLine) &&
                                     ((sourceObject.ShipmentLines == null || sourceObject.ShipmentLines.Count == 0) || sourceObject.ShipmentLines[0].ConversionQuantity == destinationObject.ShipmentLines[0].ConversionQuantity) &&
                                    sourceObject.CustomerAddress != null &&
                                    sourceObject.OpCoContact != null &&
                                    sourceObject.AfterTime == destinationObject.AfterTime &&
                                    sourceObject.BeforeTime == destinationObject.BeforeTime &&
                                    sourceObject.CheckInTime == destinationObject.CheckInTime &&
                                    sourceObject.CustomerAddress.Line1 == destinationObject.CustomerAddress.Line1 &&
                                    sourceObject.CustomerName == destinationObject.CustomerName &&
                                    sourceObject.CustomerNumber == destinationObject.CustomerNumber &&
                                    sourceObject.CustomerReference == destinationObject.CustomerReference &&
                                    sourceObject.DeliveryWarehouseCode == destinationObject.DeliveryWarehouseCode &&
                                    sourceObject.DespatchNumber == destinationObject.DespatchNumber &&
                                    sourceObject.DivisionCode == destinationObject.DivisionCode &&
                                    sourceObject.GeneratedDateTime == destinationObject.GeneratedDateTime &&
                                    sourceObject.Instructions == destinationObject.Instructions &&
                                    sourceObject.OpCoCode == destinationObject.OpCoCode &&
                                    sourceObject.OpCoContact.Email == destinationObject.OpCoContact.Email &&
                                    sourceObject.OpCoHeld == destinationObject.OpCoHeld &&
                                    sourceObject.OpCoSequenceNumber == destinationObject.OpCoSequenceNumber &&
                                    sourceObject.RequiredShipmentDate == destinationObject.RequiredShipmentDate &&
                                    // sourceObject.RouteCode == destinationObject.RouteCode &&
                                    sourceObject.SalesBranchCode == destinationObject.SalesBranchCode &&

                                    sourceObject.ShipmentName == destinationObject.ShipmentName &&
                                    sourceObject.ShipmentNumber == destinationObject.ShipmentNumber &&
                                    sourceObject.Status == destinationObject.Status &&
                                    sourceObject.StockWarehouseCode == destinationObject.StockWarehouseCode &&
                                    sourceObject.TailLiftRequired == destinationObject.TailLiftRequired &&
                                    sourceObject.TotalLineQuantity == destinationObject.TotalLineQuantity &&
                                    sourceObject.TransactionTypeCode == destinationObject.TransactionTypeCode &&
                                    sourceObject.VehicleMaxWeight == destinationObject.VehicleMaxWeight
                                    );
                            }
                        }
                    }
                }


            }

        }

        private void DoLines(PersistableBusinessObject sourceObject, PersistableBusinessObject destinationObject,
                             string sourceSystem, string destinationSystem)
        {
            List<ShipmentLine> lines = ((TDCShipment)destinationObject).ShipmentLines;
            for (int i = 0; i < lines.Count; i++)
            {
                TDCShipmentLine newline = new TDCShipmentLine();
                MappingController.Map(lines[i], newline, sourceSystem, destinationSystem, null);
                lines[i] = newline;
            }
        }

        [Test]
        public void GetItems()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (MappingController.DeleteAllMappings())
                {
                    if (MappingController.DeleteAllMappingSystems())
                    {
                        if (MappingController.DeleteAllMappingPropertyAssociations())
                        {
                            if (MappingController.DeleteAllMappingClassAssociations())
                            {
                                Mapping mapping = PopulateMappingItem();
                                mapping.Id = MappingController.SaveMapping(mapping);
                                if (mapping.Id != -1)
                                {
                                    List<Mapping> mappings = MappingController.GetMappings();
                                    //so the count should be >0
                                    Assert.IsTrue(mappings.Count > 0);
                                    //check for our new id
                                    Assert.IsTrue(mappings.Find(delegate(Mapping currentItem)
                                                                      {
                                                                          return currentItem.Id == mapping.Id;
                                                                      }) != null);
                                }
                            }

                        }
                    }
                }
            }



        }

        internal bool DeleteItem(int Id)
        {
            Mapping mapping = new Mapping();
            mapping.Id = Id;
            return MappingController.DeleteMapping(mapping);
        }

        [Test]
        public void DeleteItem()
        {

            using (TransactionScope scope = new TransactionScope())
            {

                if (MappingController.DeleteAllMappings())
                {
                    if (MappingController.DeleteAllMappingSystems())
                    {
                        if (MappingController.DeleteAllMappingPropertyAssociations())
                        {
                            if (MappingController.DeleteAllMappingClassAssociations())
                            {
                                Mapping mapping = PopulateMappingItem();
                                int id = MappingController.SaveMapping(mapping);
                                Assert.IsTrue(DeleteItem(id));
                            }
                        }
                    }
                }

            }


        }
    }
}
