using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.WebAppLib;
using Vevo.WebUI.DataAccessCache;

public partial class CommandForm : System.Web.UI.Page
{
    private void ClearCategoryCache()
    {
        //CategoryAccessCache.ClearCache();
    }

    private void ClearLanguageCache()
    {
        PageAccessCache.ClearCache();
        LanguageTextAccessCache.ClearCache();
    }

    private void ClearSiteMapCache()
    {
        SiteMapManager.ClearCache();
    }

    private void ClearAllCache()
    {
        ClearCategoryCache();
        ClearLanguageCache();
        ClearSiteMapCache();
    }

    private void ClearCache( string parameters )
    {
        switch (parameters.ToLower())
        {
            case "all":
                ClearAllCache();
                break;

            case "category":
                ClearCategoryCache();
                break;

            case "language":
                ClearLanguageCache();
                break;

            case "sitemap":
                ClearSiteMapCache();
                break;
        }
    }

    private void LoadConfiguration()
    {
        SystemConfig.Load();
    }


    protected void Page_Load( object sender, EventArgs e )
    {
        if (Request.Form["clearcache"] != null)
            ClearCache( Request.Form["clearcache"] );

        if (Request.Form["loadconfig"] != null &&
            Request.Form["loadconfig"].ToLower() == "true")
            LoadConfiguration();
    }

}
