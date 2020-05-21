using System;
using System.Web;
using System.Web.Caching;

namespace Discovery.Utility.DataAccess
{
    /// <summary>
    /// A class to perform the get/set/remove data cache
    /// </summary>
    public class DataCache
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:DataCache"/> class.
        /// </summary>
        public DataCache()
        {
        }

        /// <summary>
        /// Gets the cache.
        /// </summary>
        /// <param name="strCacheKey">The STR cache key.</param>
        /// <returns></returns>
        public static Object GetCache(string strCacheKey)
        {
            Cache objCache = HttpRuntime.Cache;
            return objCache[strCacheKey];
        }

        /// <summary>
        /// Sets the cache.
        /// </summary>
        /// <param name="strCacheKey">The STR cache key.</param>
        /// <param name="objObject">The obj object.</param>
        public static void SetCache(string strCacheKey, Object objObject)
        {
            Cache objCache = HttpRuntime.Cache;
            objCache.Insert(strCacheKey, objObject);
        }

        /// <summary>
        /// Sets the cache.
        /// </summary>
        /// <param name="strCacheKey">The STR cache key.</param>
        /// <param name="objObject">The obj object.</param>
        /// <param name="objDependancy">The obj dependancy.</param>
        public static void SetCache(
            string strCacheKey,
            Object objObject,
            CacheDependency objDependancy)
        {
            Cache objCache = HttpRuntime.Cache;
            objCache.Insert(strCacheKey, objObject, objDependancy);
        }

        /// <summary>
        /// Sets the cache.
        /// </summary>
        /// <param name="strCacheKey">The STR cache key.</param>
        /// <param name="objObject">The obj object.</param>
        /// <param name="iSlidingExpirationSecs">The i sliding expiration secs.</param>
        public static void SetCache(
            string strCacheKey,
            Object objObject,
            int iSlidingExpirationSecs)
        {
            Cache objCache = HttpRuntime.Cache;
            objCache.Insert(strCacheKey, objObject, null, DateTime.MaxValue,
                            TimeSpan.FromSeconds(iSlidingExpirationSecs));
        }

        /// <summary>
        /// Sets the cache.
        /// </summary>
        /// <param name="strCacheKey">The STR cache key.</param>
        /// <param name="objObject">The obj object.</param>
        /// <param name="dateAbsoluteExpiration">The date absolute expiration.</param>
        public static void SetCache(
            string strCacheKey,
            Object objObject,
            DateTime dateAbsoluteExpiration)
        {
            Cache objCache = HttpRuntime.Cache;
            objCache.Insert(strCacheKey, objObject, null, dateAbsoluteExpiration,
                            TimeSpan.Zero);
        }

        /// <summary>
        /// Removes the cache.
        /// </summary>
        /// <param name="strCacheKey">The STR cache key.</param>
        public static void RemoveCache(string strCacheKey)
        {
            Cache objCache = HttpRuntime.Cache;
            if (objCache[strCacheKey] != null)
            {
                objCache.Remove(strCacheKey);
            }
        }
    }
}