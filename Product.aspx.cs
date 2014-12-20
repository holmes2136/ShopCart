using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.Shared.Utilities;
using Vevo.WebUI;
using Vevo.WebUI.International;
using Vevo.WebUI.Products;

public partial class ProductPage : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private string CurrentProductID
    {
        get
        {
            if ( ViewState[ "CurrentProductID" ] == null )
                return "0";
            else
                return ( string ) ViewState[ "CurrentProductID" ];
        }
        set
        {
            ViewState[ "CurrentProductID" ] = value;
        }
    }

    private string ProductID
    {
        get
        {
            if ( !String.IsNullOrEmpty( Request.QueryString[ "ProductID" ] ) )
                return Request.QueryString[ "ProductID" ];
            else
                return DataAccessContext.ProductRepository.GetProductIDFromUrlName( ProductName );
        }
    }

    private string ProductName
    {
        get
        {
            if ( Request.QueryString[ "ProductName" ] == null )
                return String.Empty;
            else
                return Request.QueryString[ "ProductName" ];
        }
    }

    private string Name
    {
        get
        {
            if ( ViewState[ "Name" ] == null )
                return String.Empty;
            else
                return ( String ) ViewState[ "Name" ];
        }
        set
        {
            ViewState[ "Name" ] = value;
        }
    }

    private string ShortDescription
    {
        get
        {
            if ( ViewState[ "ShortDescription" ] == null )
                return String.Empty;
            else
                return ( String ) ViewState[ "ShortDescription" ];
        }
        set
        {
            ViewState[ "ShortDescription" ] = value;
        }
    }

    private void Refresh()
    {
        uxProductFormView.DataBind();
    }

    private void ProductDetails_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, CurrentProductID, StoreContext.CurrentStore.StoreID );

        if ( !String.IsNullOrEmpty( product.UrlName ) )
        {
            Response.Redirect( UrlManager.GetProductUrl( CurrentProductID, product.UrlName ) );
        }
        else
        {
            Response.Redirect( "~/Error404.aspx" );
        }
    }

    private void ProductDetails_StoreCurrencyChanged( object sender, CurrencyEventArgs e )
    {
        Refresh();
    }

    private void RegisterStoreEvents()
    {
        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler( ProductDetails_StoreCultureChanged );
        GetStorefrontEvents().StoreCurrencyChanged +=
            new StorefrontEvents.CurrencyEventHandler( ProductDetails_StoreCurrencyChanged );
    }

    private void SetUpBreadcrumb( string productID, string name, string shortDescription, string urlName, string categoryID )
    {
        uxCatalogBreadcrumb.SetupProductSitemap( productID, name, shortDescription, urlName, categoryID );
        uxCatalogBreadcrumb.Refresh();
    }

    private void PopulateTitleAndMeta()
    {
        DynamicPageElement element = new DynamicPageElement( this );
        string storeID = new StoreRetriever().GetCurrentStoreID();
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, ProductID, storeID );

        element.SetUpTitleAndMetaTags(
            product.GetPageTitle( StoreContext.Culture, StoreContext.CurrentStore ),
            product.GetMetaDescription( StoreContext.Culture, storeID ),
            product.GetMetaKeyword( StoreContext.Culture, storeID ) );
    }

    private void IsProductAvailable()
    {
        string storeID = new StoreRetriever().GetCurrentStoreID();
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, ProductID, storeID );
        if ( !product.IsProductAvailable( storeID ) )
            Response.Redirect( "~/Error404.aspx" );
    }

    private void RefreshTitle()
    {
        Product product = DataAccessContext.ProductRepository.GetOne(
            StoreContext.Culture, ProductID, StoreContext.CurrentStore.StoreID );
        this.Page.Title = product.GetPageTitle( StoreContext.Culture, StoreContext.CurrentStore );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        RegisterStoreEvents();

        IsProductAvailable();

        if ( !String.IsNullOrEmpty( Name ) )
        {
            PopulateTitleAndMeta();
        }

        if ( !IsPostBack )
        {
            CurrentProductID = ProductID;
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if ( IsPostBack )
            uxCatalogBreadcrumb.Refresh();

        RefreshTitle();
    }

    protected void uxProductDetailsSource_Selecting( object sender, ObjectDataSourceSelectingEventArgs e )
    {
        e.InputParameters[ "culture" ] = StoreContext.Culture;
        e.InputParameters[ "productID" ] = ProductID;
        e.InputParameters[ "storeID" ] = new StoreRetriever().GetCurrentStoreID();
    }

    protected void uxProductFormView_DataBinding( object sender, EventArgs e )
    {
    }

    protected void uxProductFormView_DataBound( object sender, EventArgs e )
    {
        FormView formView = ( FormView ) sender;

        // FormView's DataItem property is valid only in DataBound event. 
        // Most of the time it is null.
        if ( String.IsNullOrEmpty( Name ) )
        {
            Name = ConvertUtilities.ToString( DataBinder.Eval( formView.DataItem, "Name" ) );
            ShortDescription = ConvertUtilities.ToString( DataBinder.Eval( formView.DataItem, "ShortDescription" ) );
            PopulateTitleAndMeta();
        }
        IList<String> categoryIDs = ( IList<String> ) DataBinder.Eval( formView.DataItem, "CategoryIDs" );

        string categoryID = ConvertUtilities.ToString( categoryIDs[ 0 ] );
        foreach ( string catID in categoryIDs )
        {
            Category category = DataAccessContext.CategoryRepository.GetOne( StoreContext.Culture, catID );
            if ( category.IsCategoryAvailableStore( StoreContext.CurrentStore.StoreID ) && ( category.IsParentsEnable() ) )
            {
                categoryID = catID;
                break;
            }
        }

        SetUpBreadcrumb(
            ConvertUtilities.ToString( DataBinder.Eval( formView.DataItem, "ProductID" ) ),
            ConvertUtilities.ToString( DataBinder.Eval( formView.DataItem, "Name" ) ),
            ConvertUtilities.ToString( DataBinder.Eval( formView.DataItem, "ShortDescription" ) ),
            ConvertUtilities.ToString( DataBinder.Eval( formView.DataItem, "UrlName" ) ),
            categoryID );
    }

    protected void ProductItemCreate( object sender, EventArgs e )
    {
        FormView formView = ( FormView ) sender;
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, ProductID, new StoreRetriever().GetCurrentStoreID() );

        BaseProductDetails productDetailsControl = new BaseProductDetails();
        if ( !String.IsNullOrEmpty( product.ProductDetailsLayoutPath ) )
        {
            productDetailsControl = LoadControl( String.Format( "{0}{1}", SystemConst.LayoutProductDetailsPath, product.ProductDetailsLayoutPath ) ) as BaseProductDetails;
        }
        else
        {
            productDetailsControl = LoadControl( String.Format( "{0}{1}", SystemConst.LayoutProductDetailsPath, DataAccessContext.Configurations.GetValue( "DefaultProductDetailsLayout" ) ) ) as BaseProductDetails;
        }

        productDetailsControl.CurrentProduct = product;
        productDetailsControl.DiscountGroupID = ConvertUtilities.ToString( DataBinder.Eval( formView.DataItem, "DiscountGroupID" ) );
        formView.Controls.Add( productDetailsControl );
    }
}
