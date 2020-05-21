using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using Discovery.BusinessObjects;
using Discovery.BusinessObjects.Controllers;
using Discovery.ComponentServices.ExceptionHandling;

namespace Discovery.ComponentServices.Mapping
{
    /// <summary>
    /// A Class 'Mapper' with namespace 'Discovery.ComponentServices.Mapping'
    /// </summary>
    public class Mapper
    {
        /// <summary>
        /// A delegate 'AdvancedMapping' defined in 'Mapper' class with 4 arguments passing in
        /// </summary>
        /// <param name="sourceObject"></param>
        /// <param name="destinationObject"></param>
        /// <param name="sourceSystem"></param>
        /// <param name="destinationSystem"></param>

        public delegate void AdvancedMapping(
                    PersistableBusinessObject sourceObject,
                    PersistableBusinessObject destinationObject,
                    string sourceSystem,
                    string destinationSystem);

        /// <summary>
        ///This method will perform mapping between one object type and another as defined by the source and destination system arguments.
        ///The mapped object is passed by reference so all alterations to this object will be reflected in the calling code.
        ///This method has been designed to be generic in the hope that it will cater for mapping of other types than just Shipment mapping which is its original goal.
        ///Also with the use of Reflection and due to the data stored about a mapping it should be possible to write a much more concise body of code to perform the mapping.
        /// </summary>
        /// <param name="sourceObject">The source object.</param>
        /// <param name="destinationObject">The destination object.</param>
        /// <param name="sourceSystem">The source system.</param>
        /// <param name="destinationSystem">The destination system.</param>
        /// //<param name="advancedMappingMethod">A method that will do futher specified mapping between to two objects</param>
        /// 
        public static void Map(
                    PersistableBusinessObject sourceObject,
                    PersistableBusinessObject destinationObject,
                    string sourceSystem,
                    string destinationSystem,
                    AdvancedMapping advancedMappingMethod)
        {
            // Call mapping for deep map
            Map(sourceObject, destinationObject, sourceSystem, destinationSystem, advancedMappingMethod, null);
        }

        /// <summary>
        /// A method 'Map' defined in 'Mapper' class with 6 arguments passing in
        /// </summary>
        /// <param name="sourceObject">The source object.</param>
        /// <param name="destinationObject">The destination object.</param>
        /// <param name="sourceSystem">The source system.</param>
        /// <param name="destinationSystem">The destination system.</param>
        /// <param name="advancedMappingMethod">The advanced mapping method.</param>
        /// <param name="propertiesToIgnore">The properties to ignore.</param>
        public static void Map(
                    PersistableBusinessObject sourceObject,
                    PersistableBusinessObject destinationObject,
                    string sourceSystem,
                    string destinationSystem,
                    AdvancedMapping advancedMappingMethod,
                    string[] propertiesToIgnore)
        {
            if (destinationObject != null)
            {
                Type sourceObjectType = sourceObject.GetType();
                Type destinationObjectType = destinationObject.GetType();

                // Source property value
                object sourcePropertyValue = null;
                
                // Source property info
                PropertyInfo sourcePropertyInfo = null;

                // Get a mapping class association if we have one
                MappingClassAssociation mappingClassAssociation = MappingController.GetMappingClassAssociationByTypes(
                            sourceObjectType, 
                            destinationObjectType);

                // Get the properties for the above class association
                List<MappingPropertyAssociation> mappingPropertyAssociations = null;
                // If we have a mapping association see if there are any properties
                if (null != mappingClassAssociation)
                {
                    mappingPropertyAssociations = MappingController.GetMappingPropertyAssociationsByClassAssociationId(
                            mappingClassAssociation.Id);
                }           

                // Loop through the properties of the destination object
                foreach (PropertyInfo destinationPropertyInfo in destinationObjectType.GetProperties())
                {
                    // Only continue if we can write the destination property
                    if (destinationPropertyInfo.CanWrite)
                    {
                        // See if we have properties to ignore
                        if (null != propertiesToIgnore)
                        {
                            // See if we can find the property
                            if (null != Array.Find<string>(propertiesToIgnore,
                                        delegate(string propertyToIgnore)
                                        {
                                            // Do we ignore this property
                                            return destinationPropertyInfo.Name == propertyToIgnore;
                                        }))
                            {
                                // Ignore this property
                                continue;
                            }
                        }

                        // The mapping if one exists
                        BusinessObjects.Mapping valueMapping = null;

                        // The property association we've found, if any
                        MappingPropertyAssociation mappingPropertyAssociation = null;

                        // Is it a mappable property?
                        bool mappable = (mappingPropertyAssociations != null && 
                                    null != (mappingPropertyAssociation = mappingPropertyAssociations.Find(
                                    delegate(MappingPropertyAssociation propertyAssociation)
                                    {
                                        return propertyAssociation.DestinationProperty == destinationPropertyInfo.Name;
                                    })));

                        // If we're mappable, we need to see if the source object has a value that we can use for the lookup
                        if (mappable)
                        {
                            // Get the source property with the same name as the mapping source property
                            sourcePropertyInfo = sourceObjectType.GetProperty(mappingPropertyAssociation.SourceProperty);

                            //if the destination object has a writeable property with the same name, and their types are the same then set to cloned value
                            if (sourcePropertyInfo != null &&
                                sourcePropertyInfo.PropertyType.Equals(destinationPropertyInfo.PropertyType) &&
                                sourcePropertyInfo.CanRead)
                            {
                                // Get the source value
                                sourcePropertyValue = sourcePropertyInfo.GetValue(sourceObject, null);
                            }
                            else
                            {
                                mappable = false;
                            }
                        }

                        // See if this property is mappable and mapped
                        if (mappable && null != (valueMapping = MappingController.GetMapping(
                                    sourceObjectType,
                                    destinationObjectType,
                                    sourceSystem,
                                    destinationSystem,
                                    mappingPropertyAssociation.SourceProperty,
                                    mappingPropertyAssociation.DestinationProperty,
                                    sourcePropertyValue.ToString())))
                        {
                            // Set the destination properties value to the mapped value
                            destinationPropertyInfo.SetValue(destinationObject, valueMapping.DestinationValue, null);
                        }
                        else
                        {
                            // Get the source property with the same name as the current destination property 
                            sourcePropertyInfo = sourceObjectType.GetProperty(destinationPropertyInfo.Name);

                            //if the destination object has a writeable property with the same name, and their types are the same then set to cloned value
                            if (sourcePropertyInfo != null &&
                                sourcePropertyInfo.PropertyType.Equals(destinationPropertyInfo.PropertyType) &&
                                sourcePropertyInfo.CanRead)
                            {
                                // Get the value of the source property
                                sourcePropertyValue = sourcePropertyInfo.GetValue(sourceObject, null);

                                // Set the destination properties value to a clone of the source value 
                                destinationPropertyInfo.SetValue(destinationObject, DeepClone(sourcePropertyValue), null);
                            }
                            else
                            {
                                // Set the destination properties value to null
                                //destinationPropertyInfo.SetValue(destinationObject, Discovery.Utility.Null.SetNull(destinationPropertyInfo), null);
                            }
                        }
                    }
                }

                //call a delegate to perform futher specific mapping
                if (advancedMappingMethod != null)
                    advancedMappingMethod(sourceObject, destinationObject, sourceSystem, destinationSystem);
            }
            else
            {
                throw new MappingException("The Destination object is null.");
            }
        }

        private static object DeepClone(object objectToClone)
        {
            // Create a "deep" clone of 
            // an object. That is, copy not only
            // the object and its pointers
            // to other objects, but create 
            // copies of all the subsidiary 
            // objects as well. This code even 
            // handles recursive relationships.

            object objResult = null;
            if (objectToClone != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(ms, objectToClone);

                    // Rewind back to the beginning 
                    // of the memory stream. 
                    // Deserialize the data, then
                    // Close the memory stream.
                    ms.Position = 0;
                    objResult = bf.Deserialize(ms);
                }
            }
            return objResult;
        }
    }
}