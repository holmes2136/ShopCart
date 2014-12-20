using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Shared.DataAccess;
using Vevo.WebUI;
using Vevo.Domain.Stores;

public partial class Components_CategorySiteMap : Vevo.WebUI.International.BaseLanguageUserControl
{

    private IList<Category> _catalog, _leafCatalog;

    private bool IsDisplayCategoriesOnly()
    {
        if (DataAccessContext.Configurations.GetValue( "SiteMapDisplayType" ).ToLower() == "showcategoriesonly")
            return true;
        else
            return false;
    }

    private void GetCategory()
    {
        _catalog = DataAccessContext.CategoryRepository.GetByRootID(
            StoreContext.Culture,
            DataAccessContext.Configurations.GetValue( "RootCategory", new StoreRetriever().GetStore() ),
            "Name",
            BoolFilter.ShowTrue );
        _leafCatalog = DataAccessContext.CategoryRepository.GetByRootIDLeafOnly(
            StoreContext.Culture,
            DataAccessContext.Configurations.GetValue( "RootCategory", new StoreRetriever().GetStore() ),
            "Name",
            BoolFilter.ShowTrue );
    }

    public void PopulateProduct()
    {
        GetCategory();
        uxProductDataList.DataSource = _leafCatalog;
        uxProductDataList.DataBind();
    }

    public void PopulateCategoriesOnly()
    {
        GetCategory();
        uxCategoryItemRepeater.DataSource = _catalog;
        uxCategoryItemRepeater.DataBind();
    }

    private void GenerateParent( string parentCategoryID, Panel panel )
    {
        foreach (Category category in _catalog)
        {
            if (category.CategoryID == parentCategoryID)
            {
                string name = category.Name;
                string categoryID = category.CategoryID;
                string urlName = category.UrlName;
                string newparentCategoryID = category.ParentCategoryID;

                GenerateParent( newparentCategoryID, panel );
                HyperLink link = new HyperLink();
                link.NavigateUrl = UrlManager.GetCategoryUrl( categoryID, urlName );
                link.Text = name;
                link.CssClass = "SiteMapParent";
                panel.Controls.Add( link );

                Label label = new Label();
                label.Text = " >> ";
                label.CssClass = "SiteMapSeparate";
                panel.Controls.Add( label );
            }
        }
    }

    private void GenerateBreadcrumb( DataListItem item )
    {
        Panel panel = (Panel) item.FindControl( "uxBreadcrumbPanel" );
        string name = DataBinder.Eval( item.DataItem, "Name" ).ToString();
        string categoryID = DataBinder.Eval( item.DataItem, "CategoryID" ).ToString();
        string urlName = DataBinder.Eval( item.DataItem, "UrlName" ).ToString();
        string parentCategoryID = DataBinder.Eval( item.DataItem, "ParentCategoryID" ).ToString();

        GenerateParent( parentCategoryID, panel );
        HyperLink link = new HyperLink();
        link.NavigateUrl = UrlManager.GetCategoryUrl( categoryID, urlName );
        link.Text = name;
        link.CssClass = "SiteMapBreadcrumb";

        panel.Controls.Add( link );

        GenerateProduct( item, categoryID );
    }

    private void GenerateProduct( DataListItem item, string categoryID )
    {
        Repeater list = (Repeater) item.FindControl( "uxProductItemRepeater" );
        list.DataSource = DataAccessContext.ProductRepository.GetByCategoryID( StoreContext.Culture, categoryID, "Name", BoolFilter.ShowTrue, new StoreRetriever().GetCurrentStoreID() );
        list.DataBind();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (IsDisplayCategoriesOnly())
        {
            uxProductDataList.Visible = false;
            uxCategoryOnlyPanel.Visible = true;
        }
        else
        {
            uxProductDataList.Visible = true;
            uxCategoryOnlyPanel.Visible = false;
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {

    }

    protected void uxProductDataList_ItemDataBound( object sender, DataListItemEventArgs e )
    {
        if (!IsDisplayCategoriesOnly())
        {
            GenerateBreadcrumb( e.Item );
        }
    }
}
