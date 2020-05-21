/*
 * Created by: Paul Nichols
 * Created: 15 December 2006
 */

using System.Reflection;
using Discovery.BusinessObjects;
using Microsoft.Practices.EnterpriseLibrary.Caching;

namespace Discovery.Utility
{
    public static class CacheManager
    {
        private static Microsoft.Practices.EnterpriseLibrary.Caching.CacheManager cache;

        static CacheManager()
        {
            //cache = CacheFactory.GetCacheManager();
        }

          public static void Add(string key,object value)
        {
            if (cache != null) cache.Add(key, value);
        }


        public static void Add(PersistableBusinessObject objectToAdd)
        {
            Add(objectToAdd, true);
        }

        public static void Add(PersistableBusinessObject objectToAdd, bool fullyPopulated)
        {
            //add the object to the cache using the Id as part of the key
            Add(objectToAdd, (object)objectToAdd.Id, true);
            //add the object to the cache using the Code as part of the key if there is one
            PropertyInfo propertyInfo = objectToAdd.GetType().GetProperty("Code");
            if (propertyInfo != null) Add(objectToAdd, (object)propertyInfo.GetValue(objectToAdd, null), true);
        }

        public static void Add(PersistableBusinessObject value, object key, bool fullyPopulated)
        {
            if (cache != null) cache.Add(string.Concat(key.ToString(), "|", value.GetType().ToString(), "|", fullyPopulated.ToString()), value);
        }

        public static T Get<T>(int id) where T : PersistableBusinessObject
        {
            return Get<T>((object)id, true);
        }

          public static object Get(string key) 
        {
            if (cache != null)
            {
                return cache.GetData(key);
            }
            else
            {
                return null;
            }
        }

        public static T Get<T>(string code) where T : PersistableBusinessObject
        {
            return Get<T>((object)code, true);
        }

        public static T Get<T>(object key, bool fullyPopulated) where T : PersistableBusinessObject
        {
            T returnValue = null;
            if (cache != null)
            {
                returnValue = cache.GetData(string.Concat(key.ToString(), "|", typeof(T).ToString(), "|", fullyPopulated.ToString())) as T;
            }

            if (returnValue == null && !fullyPopulated)
            {
                //we were after a non fully populated object, and there wasn't one lets see if there is a fully populated object
                if (cache != null) returnValue = cache.GetData(string.Concat(key.ToString(), "|", typeof(T).ToString(), "|", true.ToString())) as T;
            }

            return returnValue;
        }

        private static T Get<T>(string code, bool fullyPopulated) where T : PersistableBusinessObject
        {
            return Get<T>((object)code, fullyPopulated);
        }

        public static T Get<T>(int id, bool fullyPopulated) where T : PersistableBusinessObject
        {
            return Get<T>((object)id, fullyPopulated);
        }

        public static bool Contains(PersistableBusinessObject value)
        {
            return Contains(value, true);
        }

        public static bool Contains(PersistableBusinessObject value, bool fullyPopulated)
        {
            bool contains = false;
            if (cache != null) contains = cache.Contains(string.Concat(value.Id.ToString(), "|", value.GetType().ToString(), "|", fullyPopulated.ToString()));

            if (!contains && !fullyPopulated)
            {
                //we were after a non fully populated object, and there wasn't one lets see if there is a fully populated object
                if (cache != null) contains = cache.Contains(string.Concat(value.Id.ToString(), "|", value.GetType().ToString(), "|", true.ToString()));
            }

            return contains;
        }

        public static void Dispose()
        {
            if (cache != null) cache.Dispose();
        }

        public static void Flush()
        {
            if (cache != null) cache.Flush();
        }

        public static void Remove(PersistableBusinessObject objectToRemove)
        {
            //remove using the id as the key
            Remove(objectToRemove, (object)objectToRemove.Id);
            //remove using the code as the key
            PropertyInfo propertyInfo = objectToRemove.GetType().GetProperty("Code");
            if (propertyInfo != null) Remove(objectToRemove, (object)propertyInfo.GetValue(objectToRemove, null));
        }

        private static void Remove(PersistableBusinessObject value, object key)
        {
            if (cache != null)
            {
                //remove the fully populated
                cache.Remove(string.Concat(key.ToString(), "|", value.GetType().ToString(), "|", true.ToString()));
                //remove the partially populated
                cache.Remove(string.Concat(key.ToString(), "|", value.GetType().ToString(), "|", false.ToString()));
            }
        }


    }
}