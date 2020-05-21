using System;
using System.Threading;

using Microsoft.ApplicationBlocks.Cache;
using Microsoft.ApplicationBlocks.Cache.Expirations;

using HBOS.FS.AMP.UPD.Controllers;
using HBOS.FS.AMP.UPD.Security;
using HBOS.FS.AMP.UPD.Types;

namespace HBOS.FS.AMP.UPD.WinUI
{
	/// <summary>
	/// Provides strongly typed accessors to catched data.
	/// </summary>
	/// <remarks>
	/// In order to expand this class for new cached collections the following steps must be followed
	/// <list type="ul">
	/// <item>The object to be cached, and any members in that object must be Serializable".</item>
	/// <item>A get accessor must be created to retrieve the strongly typed item, checking that the item exists 
	/// in the cache. If the item does not exist it should be retrieved and cached as appropriate.</item>
	/// <item>A set accessor must be created to remove any existing cache entry then add the new value back
	/// into the cache.</item>
    /// <item>A private method must be created to retrive the item from the database and set the Cache using 
    /// the public set for the item.</item>
    /// <item>If the cached item is to be periodically a function must be created to reload the object and 
    /// set as the <see cref="CacheItemRemovedCallback"/> parameter during the object addition to the cache.</item>
    /// <item>Item removal must be added to the <see cref="Flush"/> method.</item>
	/// </list>
	/// </remarks>
	public class CacheHelper
	{
		private CacheManager cacheManager = CacheManager.GetCacheManager();
        object syncLock = new object();

        /// <summary>
        /// Constructs a new instance of <see cref="CacheHelper"/>.
        /// </summary>
        public CacheHelper()
        {
        }

        /// <summary>
        /// Returns a standard array of cache expiration policies, containing a single policy expiring 
        /// in 5 minutes time.
        /// </summary>
        /// <returns>A standard array of cache expiration policies, containing a single policy expiring 
        /// in 5 minutes time.</returns>
        private ICacheItemExpiration[] getExpiryArray()
        {
            ICacheItemExpiration[] expiryArray = new ICacheItemExpiration[1];
            expiryArray[0] = new AbsoluteTime(DateTime.Now.AddMinutes(5));
            return (expiryArray);
        }

        /// <summary>
        /// Retrieves the fund group list from the database and places it in the cache.
        /// </summary>
        private void refillFundGroupsCacheFromDatabase()
        {
            // Retrieve the fund group collection.
            FundGroupController controller = new FundGroupController(System.Configuration.ConfigurationSettings.AppSettings["connectionString"]);
            Types.FundGroups.FundGroupCollection fundGroups  = controller.LoadFundGroupsByCompany(
                ((UPDPrincipal) Thread.CurrentPrincipal).CompanyCode);            
            this.FundGroups = fundGroups;
        }

        /// <summary>
        /// A cached FundGroupCollection.
        /// </summary>
        /// <value>A cached FundGroupCollection</value>
        public Types.FundGroups.FundGroupCollection FundGroups
        {
            get
            {                 
                object obj = cacheManager.GetData("FundGroups");
                if (null == cacheManager.GetData("FundGroups"))
                  lock (cacheManager)
                  {
                      refillFundGroupsCacheFromDatabase();
                  }
                return ((Types.FundGroups.FundGroupCollection)cacheManager.GetData("FundGroups"));
            }
            set
            {
                // Create a callback to trigger when the object leaves the cache.
                CacheItemRemovedCallback fundGroupsCallback = new CacheItemRemovedCallback(this.OnFundGroupsRemove);
                // Lock the cache to prepare for addition.
                lock(cacheManager)
                {
                    if (null != cacheManager.GetItem("FundGroups"))
                        cacheManager.Remove("FundGroups");                    
                    this.cacheManager.Add("FundGroups", value, getExpiryArray(), 
                        CacheItemPriority.NotRemovable, fundGroupsCallback);
                }            
            }
        }

        /// <summary>
        /// Callback when the "FundGroups" cache is emptied.
        /// </summary>
        /// <param name="key">Cache key name</param>
        /// <param name="cause">Removal Cause</param>
        private void OnFundGroupsRemove(string key, CacheItemRemoveCause cause)
        {
            if ("FundGroups" == key && CacheItemRemoveCause.Removed != cause)
                refillFundGroupsCacheFromDatabase();

        }

        /// <summary>
        /// Flush all helper created caches
        /// </summary>
        public void Flush()
        {
            lock (cacheManager)
            {
              cacheManager.Remove("FundGroups");
            }
        }
    }
}
