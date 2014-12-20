using System;
using System.Collections.Generic;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.WebUI;
using Vevo.WebUI.International;
using Vevo.WebUI.Products;

public partial class SearchResult : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private string CurrentSearch
    {
        get
        {
            if (Request.QueryString["Search"] == null)
                return String.Empty;
            else
                return Request.QueryString["Search"];
        }
    }

    private int CurrentPage
    {
        get
        {
            int result;
            string page = Request.QueryString["Page"];
            if (String.IsNullOrEmpty( page ) ||
                !int.TryParse( page, out result ))
                return 1;
            else
                return result;
        }
    }

    private void SearchLog()
    {
        int howManyItems = DataAccessContext.ProductRepository.CountQuickSearchResult(
            StoreContext.Culture,
            CurrentSearch,
            DataAccessContext.Configurations.GetValueList( "ProductSearchBy" ),
            "ProductID",
            new StoreRetriever().GetCurrentStoreID(),
            DataAccessContext.Configurations.GetValue( "RootCategory", new StoreRetriever().GetStore() ),
            DataAccessContext.Configurations.GetValue( "SearchMode", new StoreRetriever().GetStore() ) );

        if (howManyItems > 0)
            SearchLogAccess.Create( CurrentSearch.ToLower().Trim(), true );
        else
            SearchLogAccess.Create( CurrentSearch.ToLower().Trim(), false );
    }

    private void PopulateProductControl()
    {
        BaseProductListControl productListControl = new BaseProductListControl();
        productListControl = LoadControl( String.Format(
            "{0}{1}",
            SystemConst.LayoutProductListPath,
            DataAccessContext.Configurations.GetValue( "DefaultProductListLayout" ) ) )
                as BaseProductListControl;

        productListControl.ID = "uxProductList";
        productListControl.DataRetriever = new DataAccessCallbacks.ProductListRetriever( GetSearchResult );
        productListControl.UserDefinedParameters = new object[] { 
            CurrentSearch, DataAccessContext.Configurations.GetValueList( "ProductSearchBy" ) };
        productListControl.IsSearchResult = true;
        uxCatalogControlPanel.Controls.Add( productListControl );
    }

    private void PopulatePromotionControl()
    {
        BaseProductListControl promotionGroupListControl = new BaseProductListControl();
        promotionGroupListControl = LoadControl( "~/Layouts/PromotionLists/PromotionListDefault.ascx" ) as BaseProductListControl;
        promotionGroupListControl.ID = "uxPromotionList";
        promotionGroupListControl.DataRetriever = new DataAccessCallbacks.ProductListRetriever( GetSearchResult );
        promotionGroupListControl.IsSearchResult = true;
        uxCatalogControlPanel.Controls.Add( promotionGroupListControl );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxDefaultTitle.Text += " for \"" + CurrentSearch + "\"";
        PopulateProductControl();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!IsPostBack)
            SearchLog();
    }


    public static IList<Product> GetSearchResult(
        Culture culture,
        string sortBy,
        int startIndex,
        int endIndex,
        object[] userDefined,
        out int howManyItems )
    {
        if (!String.IsNullOrEmpty( userDefined[0].ToString() ))
        {
            return DataAccessContext.ProductRepository.QuickSearch(
                culture,
                (string) userDefined[0],
                (string[]) userDefined[1],
                sortBy,
                startIndex,
                endIndex,
                out howManyItems, 
                new StoreRetriever().GetCurrentStoreID(),
                DataAccessContext.Configurations.GetValue( "RootCategory", new StoreRetriever().GetStore() ),
                DataAccessContext.Configurations.GetValue( "SearchMode", new StoreRetriever().GetStore() ) );
        }
        else
        {
            howManyItems = 0;
            return null;
        }
    }
}
