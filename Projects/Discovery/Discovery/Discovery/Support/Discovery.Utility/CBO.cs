using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Discovery.BusinessObjects;

using Discovery.Utility;

#if (!USEDOMAINCACHE)
//using System.Web.Caching;
#endif

namespace Discovery.Utility.DataAccess
{
    /*************************************************************************************************
	** CLASS:	CBO
	**
	** OVERVIEW:
	** Hydrates custom business objects with data
	**
	** MODIFICATION HISTORY:
	**
	** Date:		Version:    Who:	Change:
	** 1/8/04		1.0			LAS		Initial Version
	************************************************************************************************/

    /// <summary>
    /// A class 'CBO' for a generic <typeparam name="T"></typeparam> used to hydrates custom business objects with data
    /// with namespace 'Discovery.Utility.DataAccess'.
    /// </summary>
    public class CBO<T> where T : new()
    {
        /*************************************************************************************************
		** CONSTANTS
		************************************************************************************************/

        /*************************************************************************************************
		** PUBLIC MEMBER VARIABLES
		************************************************************************************************/

        /*************************************************************************************************
		** PRIVATE MEMBER VARIABLES
		************************************************************************************************/

        /*************************************************************************************************
		** PUBLIC PROPERTIES
		************************************************************************************************/

        /*************************************************************************************************
		** METHODS
		************************************************************************************************/


        /*************************************************************************************************
		** METHOD:	GetPropertyInfo
		** IN:		Type objType, type to retrieve properties of
		** OUT:		ArrayList, an array list of PropertyInfo's for the specified type
		**
		** OVERVIEW:
		** Retrieves an array list of PropertyInfo's for the specified type
		**
		** MODIFICATION HISTORY:
		**
		** Date:		Version:	Who:	Change:
		** 1/8/04		1.0			LAS		Initial Version
		************************************************************************************************/

        /// <summary>
        /// Gets the property info.
        /// </summary>
        /// <returns></returns>
        public static List<PropertyInfo> GetPropertyInfo()
        {
            //  Use the cache because the reflection used later is expensive
            List<PropertyInfo> properties = (List<PropertyInfo>)DataCache.GetCache(typeof(T).FullName);

            if (properties == null)
            {
                properties = new List<PropertyInfo>();
                foreach (PropertyInfo property in typeof(T).GetProperties())
                {
                    properties.Add(property);
                }

                DataCache.SetCache(typeof(T).FullName, properties);
            }
            return properties;
        }


        /*************************************************************************************************
		** METHOD:	GetOrdinals
		** IN:		ArrayList properties, array list of PropertyInfo
		**			IDataReader dataReader, Data reader to retrieve ordinals from
		** OUT:		void
		**
		** OVERVIEW:
		** Retrieves the ordinals of the specified properies from the data reader.
		**
		** MODIFICATION HISTORY:
		**
		** Date:		Version:	Who:	Change:
		** 1/8/04		1.0			LAS		Initial Version
		************************************************************************************************/

        private static int[] GetOrdinals(
            List<PropertyInfo> properties,
            IDataReader dataReader)
        {
            // Create an array of int's for ordinals
            int[] arrOrdinals = new int[properties.Count];

            if (null != dataReader)
            {
                for (int intProperty = 0; intProperty < properties.Count; intProperty++)
                {
                    // Seed dataReader column ordinal to -1 (no such column)
                    arrOrdinals[intProperty] = -1;

                    try
                    {
                        // Try to get the ordinal of the dataReader column by name
                        arrOrdinals[intProperty] = dataReader.GetOrdinal(properties[intProperty].Name);
                    }
                    catch
                    {
                        //  property does not exist in datareader
                    }
                }
            }
            return arrOrdinals;
        }


        /*************************************************************************************************
		** METHOD:	CreateObject
		** IN:		Type objType, type of object to create and populate
		**			IDataReader dataReader, data reader containing data
		**			ArrayList properties, list of PropertyInfo's for the type
		**			int[] arrOrdinals, ordinals in data reader for each property
		** OUT:		Object, an instance of the specifed type populated with data from the data reader
		**
		** OVERVIEW:
		**
		** MODIFICATION HISTORY:
		**
		** Date:		Version:	Who:	Change:
		** 1/8/04		1.0			LAS		Initial Version
		************************************************************************************************/

        private static T CreateObject(
            IDataReader dataReader,
            List<PropertyInfo> properties,
            int[] arrOrdinals)
        {
            // Create an instance of the specified type
            T objObject = (T)Activator.CreateInstance(typeof(T));

            //  Fill object with values from datareader
            for (int iProperty = 0; iProperty < properties.Count; iProperty++)
            {
                Type propType = properties[iProperty].PropertyType;
                // Check if we can write to the property
                if (properties[iProperty].CanWrite &&
                    !(propType.Equals(typeof(DiscoveryBusinessObject)) ||
                     propType.IsSubclassOf(typeof(DiscoveryBusinessObject))))
                {
                    // See if the property exists in the data reader
                    if (arrOrdinals[iProperty] != -1)
                    {
                        // See if the value in the column is null
                        if (dataReader.IsDBNull(arrOrdinals[iProperty]))
                        {
                            try
                            {
                                //  Translate Null value
                                properties[iProperty].SetValue(objObject, Null.SetNull(properties[iProperty]), null);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                        }
                        else
                        {
                            try
                            {
                                //  Try implicit conversion first
                                properties[iProperty].SetValue(objObject, dataReader.GetValue(arrOrdinals[iProperty]),
                                                               null);
                            }
                            catch
                            {
                                //  data types do not match
                                try
                                {
                                    // Get the type of the property
                                    Type propertyType = properties[iProperty].PropertyType;

                                    // Need to handle enumeration conversions differently than other base types
                                    if (propertyType.BaseType.Equals(typeof(Enum)))
                                    {
                                        properties[iProperty].SetValue(objObject,
                                                                       Enum.ToObject(propertyType,
                                                                                     dataReader.GetValue(
                                                                                         arrOrdinals[iProperty])), null);
                                    }
                                    else
                                    {
                                        //  Try explicit conversion
                                        properties[iProperty].SetValue(objObject,
                                                                       Convert.ChangeType(
                                                                           dataReader.GetValue(arrOrdinals[iProperty]),
                                                                           propertyType), null);
                                    }
                                }
                                catch
                                {
                                    //  Error assigning a datareader value to a property
                                    properties[iProperty].SetValue(objObject, Null.SetNull(properties[iProperty]), null);
                                }
                            }
                        }
                    }
                    else
                    {
                        //  Property does not exist in datareader
                        try
                        {
                            properties[iProperty].SetValue(objObject, Null.SetNull(properties[iProperty]), null);
                        }
                        catch{}
                    }
                }
            }
            return objObject;
        }


        /*************************************************************************************************
		** METHOD:	FillObject
		** IN:		IDataReader dataReader, data reader used to fill object
		**			Type objType, type to create and populate with data
		** OUT:		Object, instance of the specified type populated with data from specified reader
		**
		** OVERVIEW:
		**
		** MODIFICATION HISTORY:
		**
		** Date:		Version:	Who:	Change:
		** 1/8/04		1.0			LAS		Initial Version
		************************************************************************************************/

        /// <summary>
        /// Fills the object.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns></returns>
        public static T FillObject(IDataReader dataReader)
        {
            return FillObject(dataReader, null, true);
        }

        /// <summary>
        /// Fills the object.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="customFillMethod">The custom fill method.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fully populate].</param>
        /// <returns></returns>
        public static T FillObject(IDataReader dataReader, CustomFill customFillMethod, bool fullyPopulate)
        {
            T returnObject = default(T);

            //  Get properties for type
            List<PropertyInfo> properties = GetPropertyInfo();

            //  Get ordinal positions in datareader for each of the properties
            int[] arrOrdinals = GetOrdinals(properties, dataReader);

            //  read datareader
            if (dataReader.Read())
            {
                //  Fill business object
                returnObject = CreateObject(dataReader, properties, arrOrdinals);
                if (customFillMethod != null)
                    customFillMethod(returnObject, dataReader, fullyPopulate);
            }

            //  closeReader datareader
            if (null != dataReader)
            {
                dataReader.Close();
            }
            return returnObject;
        }

        /// <summary>
        /// Fills the collection.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns></returns>
        public static List<T> FillCollection(IDataReader dataReader)
        {
            return FillCollection(dataReader, null, true);
        }

        /*************************************************************************************************
        ** METHOD:	FillCollection
        ** IN:		IDataReader dataReader, data reader with multiple rows
        **			Type objType, object that represents a row in above data reader
        ** OUT:		ArrayList, an array list of objType's populated from data reader
        **
        ** OVERVIEW:
        **
        ** MODIFICATION HISTORY:
        **
        ** Date:		Version:	Who:	Change:
        ** 1/8/04		1.0			LAS		Initial Version
        ************************************************************************************************/
        /// <summary>
        /// A delegate 'CustomFill' is defined to be executed if the calling method passing in a non-spaces method
        /// </summary>
        /// <param name="item"></param>
        /// <param name="dataReader"></param>
        /// <param name="fullyPopulate"></param>
        public delegate void CustomFill(T item, IDataReader dataReader, bool fullyPopulate);

        /// <summary>
        /// Fills the collection.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="customFillMethod">The custom fill method.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fully populate].</param>
        /// <returns></returns>
        public static List<T> FillCollection(IDataReader dataReader, CustomFill customFillMethod, bool fullyPopulate)
        {
            List<T> items = new List<T>();
            if (dataReader!=null)
            {
                T item;

                //  Get properties for type
                List<PropertyInfo> properties = GetPropertyInfo();

                //  Get ordinal positions in datareader
                int[] arrOrdinals = GetOrdinals(properties, dataReader);

                //  Iterate datareader
                while (dataReader.Read())
                {
                    //  Fill business object
                    item = CreateObject(dataReader, properties, arrOrdinals);
                    //call delegate to allow any extra population to occur while the datareader is at the current position
                    if (customFillMethod != null)
                        customFillMethod(item, dataReader, fullyPopulate);

                    //  Add to collection
                    items.Add(item);
                }

                //  close datareader
                if (null != dataReader)
                {
                    dataReader.Close();
                }
                dataReader.Dispose();
            }
            return items;
        }

        /*************************************************************************************************
		** METHOD:	FillCollection
		** IN:		IDataReader dataReader, 
		**			Type objType, 
		**			ref IList collectionToFill,
		**			void
		** OUT:		void
		**
		** OVERVIEW:
		**
		** MODIFICATION HISTORY:
		**
		** Date:		Version:	Who:	Change:
		** 1/8/04		1.0			LAS		Initial Version
		************************************************************************************************/
        /// <summary>
        /// Fills the collection.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="collectionToFill">The collection to fill.</param>
        /// <returns></returns>
        public static List<T> FillCollection(
                  IDataReader dataReader,
                  ref List<T> collectionToFill)
        {
            return FillCollection(dataReader, ref collectionToFill, true);
        }

        /// <summary>
        /// Fills the collection.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="collectionToFill">The collection to fill.</param>
        /// <param name="closeReader">if set to <c>true</c> [close reader].</param>
        /// <returns></returns>
        public static List<T> FillCollection(
            IDataReader dataReader,
            ref List<T> collectionToFill, bool closeReader)
        {
            T objFillObject;

            //  Get properties for type
            List<PropertyInfo> properties = GetPropertyInfo();

            //  Get ordinal positions in datareader
            int[] arrOrdinals = GetOrdinals(properties, dataReader);

            //  Iterate datareader
            while (dataReader.Read())
            {
                //  Fill business object
                objFillObject = CreateObject(dataReader, properties, arrOrdinals);
                //  Add to collection
                collectionToFill.Add(objFillObject);
            }

            //  close datareader
            if (null != dataReader && closeReader)
            {
                dataReader.Close();
            }
            dataReader.Dispose();

            return collectionToFill;
        }


        /*************************************************************************************************
		** METHOD:	InitialiseObject
		** IN:		Object objObject, 
		**			Type objType,
		** OUT:		void
		**
		** OVERVIEW:
		**
		** MODIFICATION HISTORY:
		**
		** Date:		Version:	Who:	Change:
		** 1/8/04		1.0			LAS		Initial Version
		************************************************************************************************/
        /*
		public static T InitialiseObject(T objObject) 
		{
            //  Get properties for type
            System.Collections.Generic.List<PropertyInfo> properties = GetPropertyInfo();

            //  Initialize properties
            for (int intProperty = 0; intProperty <= properties.Count - 1; intProperty++)
            {
                // Initialise the property if we can write to it
                if (properties[intProperty].CanWrite)
                {
                    // Initialise to data type's default null
                    properties[intProperty].SetValue(objObject, Null.SetNull(properties[intProperty]), null);
                }
            }
            return objObject; 
		} 
        */

        /*************************************************************************************************
		** METHOD:	Serialise
		** IN:		Object objObject, object to serialize
		** OUT:		XmlDocument, xml document object has been serialized to
		**
		** OVERVIEW:
		**
		** MODIFICATION HISTORY:
		**
		** Date:		Version:	Who:	Change:
		** 1/8/04		1.0			LAS		Initial Version
		************************************************************************************************/

        /// <summary>
        /// Serialises the specified obj object.
        /// </summary>
        /// <param name="objObject">The obj object.</param>
        /// <returns></returns>
        public static XmlDocument Serialise(Object objObject)
        {
            XmlSerializer objXmlSerializer = new XmlSerializer(objObject.GetType());

            StringBuilder objStringBuilder = new StringBuilder();

            TextWriter objTextWriter = new StringWriter(objStringBuilder);

            objXmlSerializer.Serialize(objTextWriter, objObject);

            StringReader objStringReader = new StringReader(objTextWriter.ToString());

            DataSet objDataSet = new DataSet();

            objDataSet.ReadXml(objStringReader);

            XmlDocument xmlSerializedObject = new XmlDocument();

            xmlSerializedObject.LoadXml(objDataSet.GetXml());

            return xmlSerializedObject;
        }
    }
}