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
using Vevo.WebUI;

public partial class Components_CatalogBreadcrumb : Vevo.WebUI.International.BaseLanguageUserControl
{
    public void Refresh()
    {
        uxCatalogSiteMapPath.Provider = SiteMapManager.GetProvider( CultureUtilities.StoreCultureID );
        uxCatalogSiteMapPath.DataBind();
    }

    private void Control_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        Refresh();
    }

    private void RegisterStoreEvents()
    {
        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler( Control_StoreCultureChanged );
    }


    protected void Page_Load( object sender, EventArgs e )
    {
        RegisterStoreEvents();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
    }

    public void SetupCategorySitemap( string categoryID )
    {
        SiteMapManager.StackCategory( CultureUtilities.StoreCultureID, categoryID );
    }

    public void SetupProductSitemap( string productID, string name, string shortDescription, string urlName, string categoryID )
    {
        SiteMapManager.StackProduct( CultureUtilities.StoreCultureID, productID, name, shortDescription, urlName, categoryID );
    }

    public void SetupDepartmentSitemap( string departmentID )
    {
        SiteMapManager.StackDepartment( CultureUtilities.StoreCultureID, departmentID );        
    }
    public void SetupManufacturerSitemap( string manufacturerID )
    {
        SiteMapManager.StackManufacturer( CultureUtilities.StoreCultureID, manufacturerID );
    }
    public void SetupNewsSitemap(string newsID, string topic, string shortDescription, string urlName)
    {
        SiteMapManager.StackNews(CultureUtilities.StoreCultureID, newsID, topic, shortDescription, urlName);
    }
}
