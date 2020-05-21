/*************************************************************************************************
 ** FILE:	MappingController.cs
 ** DATE:	30/05/2006
 ** AUTHOR:	Paul Nichols
 **
 **
 ** OVERVIEW:
 **
 ** MODIFICATION HISTORY:
 **
 ** Date:		Version:	Who:	Change:
 ** 30/5/06		1.0		    PJN	    Initial Version
 ************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Discovery.ComponentServices.DataAccess;
using Discovery.ComponentServices.Mapping;
using Discovery.Utility;
using Discovery.Utility.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Discovery.BusinessObjects.Controllers
{
    /// <summary>
    /// A class to provide the mapping controller which is a business object controller
    /// with namespace Discovery.BusinessObjects.Controllers
    /// </summary>
    public static class MappingController
    {
        /// <summary>
        /// Gets a mapping for the specified object types, source property and source property value
        /// </summary>
        /// <param name="sourceMappingType">Type of the source mapping.</param>
        /// <param name="destinationMappingType">Type of the destination mapping.</param>
        /// <param name="sourceSystem">The source system.</param>
        /// <param name="destinationSystem">The destination system.</param>
        /// <param name="sourcePropertyName">Name of the source property.</param>
        /// <param name="destinationPropertyName">Name of the destination property.</param>
        /// <param name="sourcePropertyValue">The source property value.</param>
        /// <returns></returns>
        public static Mapping GetMapping(
            Type sourceMappingType,
            Type destinationMappingType,
            string sourceSystem,
            string destinationSystem,
            string sourcePropertyName,
            string destinationPropertyName,
            string sourcePropertyValue)
        {
            Mapping mapping = null;

            try
            {
                // Hydrate the mapping from the db
                mapping = CBO<Mapping>.FillObject(DataAccessProvider.Instance().GetMapping(
                                                      sourceMappingType.FullName,
                                                      destinationMappingType.FullName,
                                                      sourceSystem,
                                                      destinationSystem,
                                                      sourcePropertyName,
                                                      destinationPropertyName,
                                                      sourcePropertyValue));
            }
            catch (Exception ex)
            {
                if (
                    Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex,
                                                                                                            "Business Logic"))
                    throw;
            }

            // Done
            return mapping;
        }


        /// <summary>
        /// Fullies the populate.
        /// </summary>
        /// <param name="mapping">The mapping.</param>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fully populate].</param>
        private static void FullyPopulate(Mapping mapping, IDataReader dataReader, bool fullyPopulate)
        {
            if (fullyPopulate)
            {
                DataAccessProvider dataAccessProvider = DataAccessProvider.Instance();

                if (allSystems != null)
                {
                    mapping.SourceSystem =
                        allSystems.Find(
                            delegate(MappingSystem obj) { return (obj.Id == mapping.SourceSystemId && obj.IsSource); });

                    mapping.DestinationSystem =
                        allSystems.Find(
                            delegate(MappingSystem obj) { return (obj.Id == mapping.DestinationSystemId && obj.IsDestination); });
                }
                else
                {
                    mapping.SourceSystem =CBO<MappingSystem>.FillObject(dataAccessProvider.GetMappingSystem(mapping.SourceSystemId));
                    mapping.DestinationSystem =CBO<MappingSystem>.FillObject(dataAccessProvider.GetMappingSystem(mapping.DestinationSystemId));
                }


                if (allMappingPropertyAssociations!=null)
                {
                    mapping.MappingPropertyAssociation =
                        allMappingPropertyAssociations.Find(delegate(MappingPropertyAssociation obj)
                                                                {
                                                                    return obj.Id ==mapping.MappingPropertyAssociationId;
                                                                });
                }
                else
                {
                    mapping.MappingPropertyAssociation =
                        CBO<MappingPropertyAssociation>.FillObject(
                            dataAccessProvider.GetMappingPropertyAssociation(mapping.MappingPropertyAssociationId));
                }

                if (allClassAssociations != null)
                {
                    mapping.MappingPropertyAssociation.MappingClassAssociation =
                        allClassAssociations.Find(
                            delegate(MappingClassAssociation obj) { return obj.Id == mapping.MappingPropertyAssociation.MappingClassAssociationId; });
                }
                else
                {
                    mapping.MappingPropertyAssociation.MappingClassAssociation = CBO<MappingClassAssociation>.FillObject(dataAccessProvider.GetMappingClassAssociation(mapping.MappingPropertyAssociation.MappingClassAssociationId));

                }
            }
        }


        /// <summary>
        /// Gets the mappings.
        /// </summary>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        public static List<Mapping> GetMappings(string sortExpression)
        {
            List<Mapping> mappings = GetMappings();

            return mappings;
        }

        /// <summary>
        /// Retrieves a list of all Mapping from the underlying data store via the configured DataProvider.
        ///A strongly typed list of Mapping entities is returned to the caller or an empty list if no records were found.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <returns></returns>
        public static List<Mapping> GetMappings()
        {
            List<Mapping> mappings = new List<Mapping>();
            try
            {
                mappings = CBO<Mapping>.FillCollection(DataAccessProvider.Instance().GetMappings(), FullyPopulate, true);
            }
            catch (Exception ex)
            {
                if (
                    Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex,
                                                                                                            "Business Logic"))
                    throw;
            }
            return mappings;
        }

        /// <summary>
        /// Retrieves a list of all Mapping from the underlying data store via the configured DataProvider.
        ///A strongly typed list of Mapping entities is returned to the caller or an empty list if no records were found.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <returns></returns>
        public static List<Mapping> GetMappings(int pageIndex,string sourceSystem,string destinationSystem,string sourceType,string destinationtype, string sourceProperty,string destinationProperty,string fromValue,string toValue,int startRowIndex, int maximumRows)
        {
            return GetMappings(new MappingSearchParams(pageIndex,sourceSystem, destinationSystem, sourceType, destinationtype, sourceProperty, destinationProperty, fromValue, toValue), startRowIndex, maximumRows, "");
        }


        /// <summary>
        /// Retrieves a list of all Mapping from the underlying data store via the configured DataProvider.
        ///A strongly typed list of Mapping entities is returned to the caller or an empty list if no records were found.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <returns></returns>
        public static List<Mapping> GetMappings(MappingSearchParams mappingSearchParams, int startRowIndex, int maximumRows, string sortExpression)
        {
            List<Mapping> mappings = new List<Mapping>();
            int totalRows = 0;
            try
            {
                int rows;
                
                GetAllMappingSystems();
                GetMappingClassAssociations();
                GetMappingPropertyAssociations();

                mappings = CBO<Mapping>.FillCollection(DataAccessProvider.Instance().GetMappings(mappingSearchParams,
                                                                                                startRowIndex,
                                                                                                 maximumRows,
                                                                                                 out rows),
                                                       FullyPopulate, true);
                totalRows = rows;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Logic")) throw;
            }
            count = totalRows;
            return mappings;
        }

     

        public static List<MappingSystem> GetAllMappingSystems()
        {
            
              try
            {
                  if (allSystems==null)
                  {
                      allSystems = CBO<MappingSystem>.FillCollection(DataAccessProvider.Instance().GetMappingSystems());
                  }
            }
            catch (Exception ex)
            {
                if (
                    Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex,
                                                                                                            "Business Logic"))
                    throw;
            }

            return allSystems;
        }

        private static int count;
        private static List<MappingClassAssociation> allClassAssociations;
        private static List<MappingPropertyAssociation> allMappingPropertyAssociations;
        private static List<MappingSystem> allSystems;

        public static int GetMappingsCount(MappingSearchParams mappingSearchParams)
        {
            return count;
        }

        /// <summary>
        /// Gets the mapping source systems.
        /// </summary>
        /// <returns></returns>
        public static List<MappingSystem> GetMappingSourceSystems()
        {
            List<MappingSystem> sourceSystems = new List<MappingSystem>();
            try
            {
                GetAllMappingSystems();
                sourceSystems = new List<MappingSystem>();
                foreach (MappingSystem system in allSystems)
                {
                    if (system.IsSource)
                        sourceSystems.Add(system);
                }
            }
            catch (Exception ex)
            {
                if (
                    Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex,
                                                                                                            "Business Logic"))
                    throw;
            }
            return sourceSystems;
        }

        /// <summary>
        /// Gets the mapping destination systems.
        /// </summary>
        /// <returns></returns>
        public static List<MappingSystem> GetMappingDestinationSystems()
        {
            List<MappingSystem> destinationSystems = new List<MappingSystem>();
            try
            {
                GetAllMappingSystems();
                destinationSystems = new List<MappingSystem>();
                foreach (MappingSystem system in allSystems)
                {
                    if (system.IsDestination)
                        destinationSystems.Add(system);
                }
            }
            catch (Exception ex)
            {
                if (
                    Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex,
                                                                                                            "Business Logic"))
                    throw;
            }
            return destinationSystems;
        }


        /// <summary>
        /// Retrieves a list of Mapping items from the underlying data store via the configured DataProvider that match the specified arguments.
        ///A strongly typed list of Mapping items is returned to the caller or an empty list if no records were found.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <param name="sourceMappingType">Type of the source mapping.</param>
        /// <param name="destinationMappingType">Type of the destination mapping.</param>
        /// <param name="sourceSystem">The source system.</param>
        /// <param name="destinationSystem">The destination system.</param>
        /// <returns></returns>
        public static List<Mapping> GetMappings(Type sourceMappingType, Type destinationMappingType, string sourceSystem,
                                                string destinationSystem)
        {
            List<Mapping> mappings = new List<Mapping>();
            try
            {
                mappings = CBO<Mapping>.FillCollection(
                    DataAccessProvider.Instance().GetMappingByTypeAndSystem(sourceMappingType.FullName,
                                                                            destinationMappingType.FullName,
                                                                            sourceSystem,
                                                                            destinationSystem), FullyPopulate, true);
            }
            catch (Exception ex)
            {
                if (
                    Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex,
                                                                                                            "Business Logic"))
                    throw;
            }
            return mappings;
        }

        /// <summary>
        ///This method will call the mapping between one object type and another as defined by the source and destination system arguments.
        ///The mapped object is passed by reference so all alterations to this object will be reflected in the calling code.
        ///This method has been designed to be generic in the hope that it will cater for mapping of other types than just Shipment mapping which is its original goal.
        ///Also with the use of Reflection and due to the data stored about a mapping it should be possible to write a much more concise body of code to perform the mapping.
        /// </summary>
        /// <param name="sourceObject">The source object.</param>
        /// <param name="destinationObject">The destination object.</param>
        /// <param name="sourceSystem">The source system.</param>
        /// <param name="destinationSystem">The destination system.</param>
        /// /// //<param name="advancedMappingMethod">A method that will do futher specified mapping between to two objects</param>
        public static void Map(PersistableBusinessObject sourceObject, PersistableBusinessObject destinationObject,
                               string sourceSystem, string destinationSystem,
                               Mapper.AdvancedMapping advancedMappingMethod)
        {
            Mapper.Map(sourceObject, destinationObject, sourceSystem, destinationSystem, advancedMappingMethod);
        }

        /// <summary>
        /// Saves the specified Mapping to the underlying data store via the configured DataProvider. 
        ///If the primary key (ID) of the supplied Mapping is Null, a new row is added to the underlying data store and a new primary key (ID) automatically generated.  If the primary key (ID) of the supplied Mapping has been specified, the existing row in the data store is updated if it has not already been altered. 
        ///The primary key (ID) of the created or altered Mapping is returned to the caller.  If an error occurs, an exception is thrown and no updates made to the data store. 
        ///For concurrency errors (the data has changed in-between load and save by an external system or user), a concurrency exception is thrown.
        /// </summary>
        /// <param name="mapping">The mapping.</param>
        /// <returns></returns>
        public static int SaveMapping(Mapping mapping)
        {
            try
            {
                if (mapping.IsValid)
                {
                    // Save entity
                    mapping.Id = DataAccessProvider.Instance().SaveMapping(mapping);
                }
                else
                {
                    // Entity is not valid
                    throw new InValidBusinessObjectException(mapping);
                }
            }
            catch (Exception ex)
            {
                if (
                    Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex,
                                                                                                            "Business Logic"))
                    throw;
            }

            // Done
            return mapping.Id;
        }

        /// <summary>
        ///Deletes the specified Mapping record from the underlying data store via the configured DataProvider. 
        ///If the specified Mapping is deleted true is returned otherwise false.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <param name="mapping">The mapping to delete.</param>
        /// <returns></returns>
        public static bool DeleteMapping(Mapping mapping)
        {
            bool success = false;
            try
            {
                if (mapping != null)
                {
                    success = DataAccessProvider.Instance().DeleteMapping(mapping.Id);
                }
            }
            catch (Exception ex)
            {
                if (
                    Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex,
                                                                                                            "Business Logic"))
                    throw;
            }
            return success;
        }

        /// <summary>
        /// Gets the mapping lookup.
        /// </summary>
        /// <param name="mappingPropertyAssociationId">The mapping property association id.</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetMappingLookup(int mappingPropertyAssociationId)
        {
            Dictionary<string, string> list = new Dictionary<string, string>();
            try
            {
                IDataReader dataReader =
                    DataAccessProvider.Instance().GetMappingLookup(mappingPropertyAssociationId);

                while (dataReader.Read())
                {
                    list.Add(dataReader["Code"].ToString(), dataReader["DisplayColumn"].ToString());
                }

                dataReader.Close();

                dataReader.Dispose();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 208)
                {
                    //an invalid look up table has been defined
                }
            }
            catch (Exception ex)
            {
                if (
                    Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex,
                                                                                                            "Business Logic"))
                    throw;
            }
            return list;
        }


        /// <summary>
        /// Gets all readable public properties for a given type.
        /// </summary>
        /// <returns></returns>
        public static List<PropertyInfo> GetClassesPropertiesReadable(string fullNameOfTypeToInterogate)
        {
            List<PropertyInfo> properties = new List<PropertyInfo>();
            try
            {
                if (fullNameOfTypeToInterogate != null)
                {
                    Type typeToInterogate = Assembly.GetExecutingAssembly().GetType(fullNameOfTypeToInterogate);


                    if (typeToInterogate != null)
                    {
                        foreach (PropertyInfo property in typeToInterogate.GetProperties())
                        {
                            if (property.CanRead)
                                properties.Add(property);
                        }
                    }
                    properties.Sort(new UniversalComparer<PropertyInfo>("Name"));
                }
            }
            catch (Exception ex)
            {
                if (
                    Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex,
                                                                                                            "Business Logic"))
                    throw;
            }
            return properties;
        }

        /// <summary>
        /// Gets all writable public properties for a given type.
        /// </summary>
        /// <returns></returns>
        public static List<PropertyInfo> GetClassesPropertiesWritable(string fullNameOfTypeToInterogate)
        {
            List<PropertyInfo> properties = new List<PropertyInfo>();
            try
            {
                if (fullNameOfTypeToInterogate != null)
                {
                    Type typeToInterogate = Assembly.GetExecutingAssembly().GetType(fullNameOfTypeToInterogate);


                    if (typeToInterogate != null)
                    {
                        foreach (PropertyInfo property in typeToInterogate.GetProperties())
                        {
                            if (property.CanWrite)
                                properties.Add(property);
                        }
                    }
                    properties.Sort(new UniversalComparer<PropertyInfo>("Name"));
                }
            }
            catch (Exception ex)
            {
                if (
                    Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex,
                                                                                                            "Business Logic"))
                    throw;
            }
            return properties;
        }

        /// <summary>
        /// Gets all mapable types.
        /// </summary>
        /// <returns></returns>
        public static List<Type> GetAllMapableTypes()
        {
            List<Type> types = new List<Type>();
            try
            {
                foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
                {
                    if (type.IsSerializable && type.IsSubclassOf(typeof(PersistableBusinessObject)) &&
                        !type.Equals(typeof(PersistableBusinessObject)))
                    {
                        types.Add(type);
                    }
                }
                types.Sort(new UniversalComparer<Type>("FullName"));
            }
            catch (Exception ex)
            {
                if (
                    Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex,
                                                                                                            "Business Logic"))
                    throw;
            }
            return types;
        }

        /// <summary>
        /// Gets the mapping class associations by class association name
        /// </summary>
        /// <returns></returns>
        public static MappingClassAssociation GetMappingClassAssociationByTypes(
            Type sourceMappingType,
            Type destinationMappingType)
        {
            MappingClassAssociation mappingClassAssociation = null;
            try
            {
                // Get the mapping class associations
                mappingClassAssociation =
                    CBO<MappingClassAssociation>.FillObject(
                        DataAccessProvider.Instance().GetMappingClassAssociationByTypes(
                            sourceMappingType.FullName,
                            destinationMappingType.FullName));
            }
            catch (Exception ex)
            {
                if (
                    Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex,
                                                                                                            "Business Logic"))
                    throw;
            }
            // Done
            return mappingClassAssociation;
        }

        /// <summary>
        /// Gets the mapping class associations.
        /// </summary>
        /// <returns></returns>
        public static List<MappingClassAssociation> GetMappingClassAssociations()
        {
            
            try
            {
                if (allClassAssociations==null)
                {
                    allClassAssociations =
                    CBO<MappingClassAssociation>.FillCollection(
                        DataAccessProvider.Instance().GetMappingClassAssociations());
                }
            }
            catch (Exception ex)
            {
                if (
                    Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex,
                                                                                                            "Business Logic"))
                    throw;
            }
            return allClassAssociations;
        }

        /// <summary>
        /// Gets the unique mapping source types.
        /// </summary>
        /// <returns></returns>
        public static List<MappingClassAssociation> GetUniqueMappingSourceTypes()
        {
            List<MappingClassAssociation> mappingClassAssociations = GetMappingClassAssociations();
            List<MappingClassAssociation> uniqueSourceTypes = new List<MappingClassAssociation>();
            try
            {
                foreach (MappingClassAssociation association in mappingClassAssociations)
                {
                    bool add;
                    add = true;
                    foreach (MappingClassAssociation uniqueAssociation in uniqueSourceTypes)
                    {
                        if (uniqueAssociation.SourceType == association.SourceType)
                        {
                            add = false;
                            break;
                        }
                    }
                    if (add)
                        uniqueSourceTypes.Add(association);
                }
            }
            catch (Exception ex)
            {
                if (
                    Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex,
                                                                                                            "Business Logic"))
                    throw;
            }
            return uniqueSourceTypes;
        }


        /// <summary>
        /// Gets the mapping class associations by source type.
        /// </summary>
        /// <returns></returns>
        public static List<MappingClassAssociation> GetMappingClassAssociationsBySourceType(string sourceType)
        {
            List<MappingClassAssociation> mappingClassAssociations = new List<MappingClassAssociation>();
            try
            {
                mappingClassAssociations =
                    CBO<MappingClassAssociation>.FillCollection(
                        DataAccessProvider.Instance().GetMappingClassAssociations(sourceType));
            }
            catch (Exception ex)
            {
                if (
                    Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex,
                                                                                                            "Business Logic"))
                    throw;
            }
            return mappingClassAssociations;
        }


        /// <summary>
        /// Gets the mapping property associations by source property.
        /// </summary>
        /// <param name="sourceProperty">The source property.</param>
        /// <param name="classAssociationId">The class association id.</param>
        /// <returns></returns>
        public static List<MappingPropertyAssociation> GetMappingPropertyAssociations(string sourceProperty,
                                                                                      int classAssociationId)
        {
            List<MappingPropertyAssociation> mappingPropertyAssociations = new List<MappingPropertyAssociation>();
            try
            {
                mappingPropertyAssociations =
                    CBO<MappingPropertyAssociation>.FillCollection(
                        DataAccessProvider.Instance().GetMappingPropertyAssociations(sourceProperty, classAssociationId));
            }
            catch (Exception ex)
            {
                if (
                    Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex,
                                                                                                            "Business Logic"))
                    throw;
            }
            return mappingPropertyAssociations;
        }

  /// <summary>
        /// Gets the mapping property associations by source property.
        /// </summary>
        /// <param name="sourceProperty">The source property.</param>
        /// <param name="classAssociationId">The class association id.</param>
        /// <returns></returns>
        public static List<MappingPropertyAssociation> GetMappingPropertyAssociations()
        {
            
            try
            {
                if (allMappingPropertyAssociations==null)
                {
                    allMappingPropertyAssociations =
                    CBO<MappingPropertyAssociation>.FillCollection(
                        DataAccessProvider.Instance().GetMappingPropertyAssociations());
                }
            }
            catch (Exception ex)
            {
                if (
                    Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex,
                                                                                                            "Business Logic"))
                    throw;
            }
            return allMappingPropertyAssociations;
        }

        /// <summary>
        /// Retrieves a single Mapping entry from the underlying data store via the configured DataProvider for the supplied Mapping ID.
        /// An instance of a Mapping is returned to the caller or Null if no Mapping record is found.  If an error occurs, an exception is thrown.
        /// </summary>
        /// <param name="mappingId">The mapping id.</param>
        /// <returns></returns>
        public static Mapping GetMapping(int mappingId)
        {
            Mapping mapping = new Mapping();
            try
            {
                mapping =
                    CBO<Mapping>.FillObject(DataAccessProvider.Instance().GetMapping(mappingId), FullyPopulate, true);
            }
            catch (Exception ex)
            {
                if (
                    Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex,
                                                                                                            "Business Logic"))
                    throw;
            }
            return mapping;
        }

        /// <summary>
        /// Gets the mapping property association.
        /// </summary>
        /// <param name="classAssociationId">The class association id.</param>
        /// <returns></returns>
        public static List<MappingPropertyAssociation> GetMappingPropertyAssociationsByClassAssociationId(
            int classAssociationId)
        {
            List<MappingPropertyAssociation> mappingPropertyAssociations = new List<MappingPropertyAssociation>();
            try
            {
                mappingPropertyAssociations =
                    CBO<MappingPropertyAssociation>.FillCollection(
                        DataAccessProvider.Instance().GetMappingPropertyAssociationByClassAssociationId(
                            classAssociationId));
            }
            catch (Exception ex)
            {
                if (
                    Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex,
                                                                                                            "Business Logic"))
                    throw;
            }
            return mappingPropertyAssociations;
        }

        /// <summary>
        /// Gets the mapping property association.
        /// </summary>
        /// <param name="mappingClassAssociationId">The mapping class association id.</param>
        /// <returns></returns>
        public static MappingPropertyAssociation GetMappingPropertyAssociation(int mappingClassAssociationId)
        {
            MappingPropertyAssociation mappingPropertyAssociation = new MappingPropertyAssociation();
            try
            {
                mappingPropertyAssociation =
                    CBO<MappingPropertyAssociation>.FillObject(
                        DataAccessProvider.Instance().GetMappingPropertyAssociation(mappingClassAssociationId));
            }
            catch (Exception ex)
            {
                if (
                    Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex,
                                                                                                            "Business Logic"))
                    throw;
            }
            return mappingPropertyAssociation;
        }

        /// <summary>
        /// Deletes all mappings.
        /// </summary>
        /// <returns></returns>
        public static bool DeleteAllMappings()
        {
            bool success = false;
            try
            {
                success = DataAccessProvider.Instance().DeleteAllMappings();
            }
            catch (Exception ex)
            {
                if (
                    Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex,
                                                                                                            "Business Logic"))
                    throw;
            }
            return success;
        }

        /// <summary>
        /// Deletes all mapping systems.
        /// </summary>
        /// <returns></returns>
        public static bool DeleteAllMappingSystems()
        {
            bool success = false;
            try
            {
                success = DataAccessProvider.Instance().DeleteAllMappingSystems();
            }
            catch (Exception ex)
            {
                if (
                    Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex,
                                                                                                            "Business Logic"))
                    throw;
            }
            return success;
        }

        /// <summary>
        /// Deletes all mapping class associations.
        /// </summary>
        /// <returns></returns>
        public static bool DeleteAllMappingClassAssociations()
        {
            bool success = false;
            try
            {
                success = DataAccessProvider.Instance().DeleteAllMappingClassAssociations();
            }
            catch (Exception ex)
            {
                if (
                    Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex,
                                                                                                            "Business Logic"))
                    throw;
            }
            return success;
        }

        /// <summary>
        /// Deletes all mapping property associations.
        /// </summary>
        /// <returns></returns>
        public static bool DeleteAllMappingPropertyAssociations()
        {
            bool success = false;
            try
            {
                success = DataAccessProvider.Instance().DeleteAllMappingPropertyAssociations();
            }
            catch (Exception ex)
            {
                if (
                    Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex,
                                                                                                            "Business Logic"))
                    throw;
            }
            return success;
        }

        /// <summary>
        /// Saves the mapping system.
        /// </summary>
        /// <param name="system">The system.</param>
        public static int SaveMappingSystem(MappingSystem system)
        {
            try
            {
                if (system.IsValid)
                {
                    // Save entity
                    system.Id = DataAccessProvider.Instance().SaveMappingSystem(system);
                }
                else
                {
                    // Entity is not valid
                    throw new InValidBusinessObjectException(system);
                }
            }
            catch (Exception ex)
            {
                if (
                    Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex,
                                                                                                            "Business Logic"))
                    throw;
            }

            // Done
            return system.Id;
        }

        /// <summary>
        /// Saves the mapping class association.
        /// </summary>
        /// <param name="association">The association.</param>
        /// <returns></returns>
        public static int SaveMappingClassAssociation(MappingClassAssociation association)
        {
            try
            {
                if (association.IsValid)
                {
                    association.Id = DataAccessProvider.Instance().SaveMappingClassAssociation(association);
                }
                else
                {
                    // Entity is not valid
                    throw new InValidBusinessObjectException(association);
                }
            }
            catch (Exception ex)
            {
                if (
                    Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex,
                                                                                                            "Business Logic"))
                    throw;
            }

            // Done
            return association.Id;
        }

        /// <summary>
        /// Saves the mapping property association.
        /// </summary>
        /// <param name="association">The association.</param>
        /// <returns></returns>
        public static int SaveMappingPropertyAssociation(MappingPropertyAssociation association)
        {
            try
            {
                if (association.IsValid)
                {
                    association.Id = DataAccessProvider.Instance().SaveMappingPropertyAssociation(association);
                }
                else
                {
                    // Entity is not valid
                    throw new InValidBusinessObjectException(association);
                }
            }
            catch (Exception ex)
            {
                if (
                    Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex,
                                                                                                            "Business Logic"))
                    throw;
            }

            // Done
            return association.Id;
        }
    }

    public struct MappingSearchParams
    {
        private readonly string sourceSystem;
        private readonly string destinationSystem;
        private readonly string sourceType;
        private readonly string destinationtype;
        private readonly string sourceProperty;
        private readonly string destinationProperty;
        private readonly string fromValue;
        private readonly string toValue;
        private readonly int pageIndex;

        public MappingSearchParams(int pageIndex,string sourceSystem, string destinationSystem, string sourceType, string destinationtype, string sourceProperty, string destinationProperty, string fromValue, string toValue)
        {
            this.pageIndex = pageIndex;
            this.sourceSystem = sourceSystem;
            this.destinationSystem = destinationSystem;
            this.sourceType = sourceType;
            this.destinationtype = destinationtype;
            this.sourceProperty = sourceProperty;
            this.destinationProperty = destinationProperty;
            this.fromValue = fromValue;
            this.toValue = toValue;

        }

        public string SourceSystem
        {
            get { return sourceSystem; }
        }

        public string DestinationSystem
        {
            get { return destinationSystem; }
        }

        public string SourceType
        {
            get { return sourceType; }
        }

        public string DestinationType
        {
            get { return destinationtype; }
        }

        public string SourceProperty
        {
            get { return sourceProperty; }
        }

        public string DestinationProperty
        {
            get { return destinationProperty; }
        }

        public string FromValue
        {
            get { return fromValue; }
        }

        public string ToValue
        {
            get { return toValue; }
        }

        public int PageIndex
        {
            get { return pageIndex; }
        }
    }
}