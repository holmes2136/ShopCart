using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo.WebAppLib;


namespace Vevo
{
    /// <summary>
    /// Summary description for SiteMapManager
    /// </summary>
    public static class SiteMapManager
    {
        private const string ProviderCachePrefix = "SiteMapProvider";


        private static SiteMapProvider GetCachedProvider( string cultureID )
        {
            return (SiteMapProvider) HttpRuntime.Cache[ProviderCachePrefix + "_" + UrlManager.IsFacebook() + cultureID];
        }

        private static void SetCachedProvider( string cultureID, SiteMapProvider provider )
        {
            HttpRuntime.Cache.Insert(
                ProviderCachePrefix + "_" + UrlManager.IsFacebook() + cultureID,
                provider,
                null,
                Cache.NoAbsoluteExpiration,
                Cache.NoSlidingExpiration,
                CacheItemPriority.AboveNormal,      // Site map is costly to construct
                null );
        }


        public static SiteMapProvider GetProvider( string cultureID )
        {
            SiteMapProvider provider = GetCachedProvider( cultureID );

            if (provider == null)
            {
                provider = new VevoSiteMapProvider( cultureID );
                SetCachedProvider( cultureID, provider );
            }

            return provider;
        }

        public static void StackCategory( string cultureID, string categoryID )
        {
            VevoSiteMapProvider sitemap = (VevoSiteMapProvider) GetProvider( cultureID );
            sitemap.StackCategory( categoryID );
        }

        public static void StackProduct( string cultureID, string productID, string name,
            string shortDescription, string urlName, string categoryID )
        {
            VevoSiteMapProvider sitemap = (VevoSiteMapProvider) GetProvider( cultureID );
            sitemap.StackProduct( productID, name, shortDescription, urlName, categoryID );
        }

        public static void StackDepartment( string cultureID, string departmentID )
        {
            VevoSiteMapProvider sitemap = (VevoSiteMapProvider) GetProvider( cultureID );
            sitemap.StackDepartment( departmentID );
        }
        public static void StackManufacturer( string cultureID, string manufacturerID )
        {
            VevoSiteMapProvider sitemap = (VevoSiteMapProvider) GetProvider( cultureID );
            sitemap.StackManufacturer( manufacturerID );
        }
        public static void StackNews(string cultureID,string newsID, string topic, string shortDescription, string urlName)
        {
            VevoSiteMapProvider sitemap = (VevoSiteMapProvider)GetProvider(cultureID);
            sitemap.StackNews(newsID, topic, shortDescription, urlName);
        }
        public static void ClearCache()
        {
            WebUtilities.ClearCacheWithPrefix( ProviderCachePrefix + "_" + UrlManager.IsFacebook() );
        }

    }
}
