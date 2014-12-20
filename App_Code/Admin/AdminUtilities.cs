using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Web;
using Vevo.DataAccessLib;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.International;
using Vevo.Domain.Payments;
using Vevo.WebAppLib;
using Vevo.WebUI.DataAccessCache;

namespace Vevo
{
    /// <summary>
    /// Summary description for AdminUtilities
    /// </summary>
    public static partial class AdminUtilities
    {
        private static string _commandUrl = AdminConfig.UrlFront + "CommandForm.aspx";


        public static void RemoveAllCacheInMemory()
        {
            StringCollection keysToRemove = new StringCollection();

            IDictionaryEnumerator cacheEnum = HttpRuntime.Cache.GetEnumerator();
            while (cacheEnum.MoveNext())
            {
                string key = cacheEnum.Key.ToString();
                keysToRemove.Add( key );
            }

            foreach (string item in keysToRemove)
            {
                HttpRuntime.Cache.Remove( item );
            }
        }

        public static void LoadSystemConfig()
        {
            SystemConfig.Load();
        }

        public static void ClearAllCache()
        {
            ClearLanguageCache();
            ClearSiteMapCache();
            ClearAdminCache();
            LoadSystemConfig();
            RemoveAllCacheInMemory();

            StandardCountryCollection.ClearCache();
        }

        public static void ClearLanguageCache()
        {
            PageAccessCache.ClearCache();
            LanguageTextAccessCache.ClearCache();
        }

        public static void ClearSiteMapCache()
        {
            SiteMapManager.ClearCache();
        }

        public static void ClearAdminCache()
        {
            DataAccessContext.ClearConfigurationCache();

            AdminMenuAdvancedAccessCache.ClearCache();
            AdminMenuPageAccessCache.ClearCache();
            AdminMenuPermissionAccessCache.ClearCache();
        }

        public static string FormatPrice( decimal price )
        {
            if(CurrencyUtilities.BaseCurrencyPosition =="Before")
            return String.Format( "{0}{1:n2}",
                CurrencyUtilities.BaseCurrencySymbol,
                price );
            else
            return String.Format( "{1:n2}{0}",
                CurrencyUtilities.BaseCurrencySymbol,
                price );
        }

        public static string DefaultCultureID
        {
            get
            {
                return CultureUtilities.DefaultCultureID;
            }
        }

        public static Culture CurrentCulture
        {
            get
            {
                return DataAccessContext.CultureRepository.GetOne( CultureUtilities.DefaultCultureID );
            }
        }
    }
}
