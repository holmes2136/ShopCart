using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.WebUI;
using Vevo.WebUI.Ajax;
using Vevo.WebUI.Products;

public partial class Layouts_CategoryLists_CategoryListResponsive : BaseCategoryListControl
{
    private string CurrentCategoryName
    {
        get
        {
            if ( Request.QueryString[ "CategoryName" ] == null )
                return String.Empty;
            else
                return Request.QueryString[ "CategoryName" ];
        }
    }

    private string CurrentCategoryID
    {
        get
        {
            string id = Request.QueryString[ "CategoryID" ];
            if ( id != null )
            {
                return id;
            }
            else
            {
                return DataAccessContext.Configurations.GetValue( "RootCategory", new StoreRetriever().GetStore() );
            }
        }
    }

    private Category CurrentCategory
    {
        get
        {
            return DataAccessContext.CategoryRepository.GetOne( StoreContext.Culture, CurrentCategoryID );
        }
    }

    private string MinPrice
    {
        get
        {
            if ( Request.QueryString[ "MinPrice" ] == null )
                return String.Empty;
            else
                return Request.QueryString[ "MinPrice" ];
        }
    }

    private string MaxPrice
    {
        get
        {
            if ( Request.QueryString[ "MaxPrice" ] == null )
                return String.Empty;
            else
                return Request.QueryString[ "MaxPrice" ];
        }
    }

    private string ItemPerPage
    {
        get
        {
            if ( ViewState[ "ItemPerPage" ] == null )
                ViewState[ "ItemPerPage" ] = uxItemsPerPageControl.DefaultValue;

            return ( string ) ViewState[ "ItemPerPage" ];
        }
        set
        {
            ViewState[ "ItemPerPage" ] = value;
        }
    }

    private bool IsFacetedSearch()
    {
        string[] allKey = Request.QueryString.AllKeys;
        return ( allKey.Length > 1 );
    }


    private int NoOfCategoryColumn
    {
        get
        {
            return DataAccessContext.Configurations.GetIntValue( "NumberOfCategoryColumn" );
        }
    }

    private IList<Category> GetCategoryList( int itemsPerPage, out int totalItems )
    {
        if ( !String.IsNullOrEmpty( CurrentCategoryName ) )
        {
            return DataAccessContext.CategoryRepository.GetByParentUrlName(
                StoreContext.Culture,
                CurrentCategoryName,
                "SortOrder",
                BoolFilter.ShowTrue,
                ( uxPagingControl.CurrentPage - 1 ) * itemsPerPage,
                ( uxPagingControl.CurrentPage * itemsPerPage ) - 1,
                out totalItems );
        }
        else
        {
            return DataAccessContext.CategoryRepository.GetByParentID(
                StoreContext.Culture,
                CurrentCategoryID,
                "SortOrder",
                BoolFilter.ShowTrue,
                ( uxPagingControl.CurrentPage - 1 ) * itemsPerPage,
                ( uxPagingControl.CurrentPage * itemsPerPage ) - 1,
                out totalItems );
        }
    }

    private string GetCategoryDescription()
    {
        return DataAccessContext.CategoryRepository.GetOne(
                StoreContext.Culture,
                CurrentCategoryName).Description;
    }

    private string GetCategoryID()
    {
        string categoryID = Request.QueryString[ "cat" ];
        if ( !String.IsNullOrEmpty( categoryID ) )
            return categoryID;
        else
            if ( !String.IsNullOrEmpty( CurrentCategoryName ) )
                return DataAccessContext.CategoryRepository.GetOneByUrlName( StoreContext.Culture, CurrentCategoryName ).CategoryID;
            else
                return CurrentCategoryID;
    }

    private string GetDepartmentID()
    {
        string departmentID = "";

        if ( !String.IsNullOrEmpty( Request.QueryString[ "dep" ] ) )
        {
            departmentID = Request.QueryString[ "dep" ];
        }

        return departmentID;
    }

    private string GetManufacturerID()
    {
        string manufacturerID = String.Empty;
        if ( !String.IsNullOrEmpty( Request.QueryString[ "manu" ] ) )
        {
            manufacturerID = Request.QueryString[ "manu" ];
        }
        return manufacturerID;
    }

    private void PopulateCategoryControls()
    {
        uxItemsPerPageControl.SelectValue( ItemPerPage );

        int totalItems;
        int selectedValue;

        selectedValue = Convert.ToInt32( uxItemsPerPageControl.SelectedValue );
        uxList.DataSource = GetCategoryList( selectedValue, out totalItems );
        uxList.DataBind();

        uxPagingControl.NumberOfPages = ( int ) Math.Ceiling( ( double ) totalItems / selectedValue );

        uxCategoryDescriptionText.Text = GetCategoryDescription();
    }

    private void PopulateFacetProductControls()
    {
        BaseProductListControl productList = ( BaseProductListControl ) uxCatalogControlPanel.FindControl( "uxProductList" );
        if ( productList != null ) return;

        Category category = new Category();
        if ( !String.IsNullOrEmpty( CurrentCategoryName ) )
        {
            category = DataAccessContext.CategoryRepository.GetOneByUrlName( StoreContext.Culture, CurrentCategoryName );
            BaseProductListControl productListControl = new BaseProductListControl();
            if ( !String.IsNullOrEmpty( category.ProductListLayoutPath ) )
                productListControl = LoadControl( String.Format(
                    "{0}{1}",
                    SystemConst.LayoutProductListPath,
                    category.ProductListLayoutPath ) ) as BaseProductListControl;
            else
                productListControl = LoadControl( String.Format(
                    "{0}{1}",
                    SystemConst.LayoutProductListPath,
                    DataAccessContext.Configurations.GetValue( "DefaultProductListLayout" ) ) )
                        as BaseProductListControl;

            productListControl.ID = "uxProductList";
            productListControl.IsSearchResult = true;
            productListControl.UserDefinedParameters = new object[] { 
        GetCategoryID(),
        GetDepartmentID(),
        GetManufacturerID(),
        MinPrice,
        MaxPrice,
        GetSpecItemValueList( GetAllSpecKey() )};
            productListControl.DataRetriever = new DataAccessCallbacks.ProductListRetriever( GetProductList );
            uxCatalogControlPanel.Controls.Add( productListControl );
        }
        else
            uxCatalogControlPanel.Visible = false;
    }

    private BaseProductListControl GetProductList()
    {
        return ( BaseProductListControl ) uxCatalogControlPanel.FindControl( "uxProductList" );
    }

    private void CategoryList_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        Refresh();
    }

    private void RegisterStoreEvents()
    {
        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler( CategoryList_StoreCultureChanged );
    }

    private ScriptManager GetScriptManager()
    {
        return AjaxUtilities.GetScriptManager( this );
    }

    private void AddHistoryPoint()
    {
        GetScriptManager().AddHistoryPoint( "CatPage", uxPagingControl.CurrentPage.ToString() );
        GetScriptManager().AddHistoryPoint( "CatItemPerPage", uxItemsPerPageControl.SelectedValue );
    }

    protected void uxPagingControl_BubbleEvent( object sender, EventArgs e )
    {
        AddHistoryPoint();
        Refresh();
    }

    protected void uxItemsPerPageControl_BubbleEvent( object sender, EventArgs e )
    {
        ItemPerPage = uxItemsPerPageControl.SelectedValue;
        uxPagingControl.CurrentPage = 1;
        AddHistoryPoint();
        Refresh();
    }

    protected void ScriptManager_Navigate( object sender, HistoryEventArgs e )
    {
        string args;

        if ( !string.IsNullOrEmpty( e.State[ "CatItemPerPage" ] ) )
        {
            ItemPerPage = e.State[ "CatItemPerPage" ].ToString();
        }
        else
        {
            ItemPerPage = uxItemsPerPageControl.DefaultValue;
        }

        int totalItems;
        int selectedValue;
        selectedValue = Convert.ToInt32( uxItemsPerPageControl.SelectedValue );
        GetCategoryList( selectedValue, out totalItems );

        uxPagingControl.NumberOfPages = ( int ) System.Math.Ceiling( ( double ) totalItems / selectedValue );

        if ( !string.IsNullOrEmpty( e.State[ "CatPage" ] ) )
        {
            args = e.State[ "CatPage" ];
            uxPagingControl.CurrentPage = int.Parse( args );
        }
        else
        {
            uxPagingControl.CurrentPage = 1;
        }

        Refresh();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        RegisterStoreEvents();
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
        uxItemsPerPageControl.BubbleEvent += new EventHandler( uxItemsPerPageControl_BubbleEvent );
        GetScriptManager().Navigate += new EventHandler<HistoryEventArgs>( ScriptManager_Navigate );
        //AjaxUtilities.ScrollToTop( uxGoToTopLink );

        //uxList.RepeatColumns = NoOfCategoryColumn;
        //uxList.RepeatDirection = RepeatDirection.Horizontal;

        if ( !IsPostBack )
        {
            ItemPerPage = CatalogUtilities.CategoryItemsPerPage;
        }

        Refresh();
    }

    private IList<string> GetAllSpecKey()
    {
        string[] allKey = Request.QueryString.AllKeys;

        string query = String.Empty;
        IList<string> specKeyList = new List<string>();
        for ( int i = 0; i < allKey.Length; i++ )
        {
            if ( !allKey[ i ].ToLower().Equals( "categoryname" ) && !allKey[ i ].ToLower().Equals( "categoryid" ) )
            {
                if ( allKey[ i ].ToLower().Equals( "minprice" ) ||
                    allKey[ i ].ToLower().Equals( "maxprice" ) ||
                    allKey[ i ].ToLower().Equals( "cat" ) ||
                    allKey[ i ].ToLower().Equals( "dep" ) ||
                    allKey[ i ].ToLower().Equals( "manu" ) )
                {
                    continue;
                }

                specKeyList.Add( allKey[ i ] );
            }
        }

        return specKeyList;
    }

    private IList<SpecificationItemValue> GetSpecItemValueList( IList<string> specKeyList )
    {
        IList<SpecificationItemValue> specItemValueList = new List<SpecificationItemValue>();
        foreach ( string specKey in specKeyList )
        {
            string specValue = Request.QueryString[ specKey ];

            SpecificationItem specItem = DataAccessContext.SpecificationItemRepository.GetOneByName( StoreContext.Culture, specKey );

            SpecificationItemValue specItemValue = DataAccessContext.SpecificationItemValueRepository.GetOneBySpecItemIDAndValue( StoreContext.Culture, specItem.SpecificationItemID, specValue );
            specItemValueList.Add( specItemValue );
        }

        return specItemValueList;
    }

    public static IList<Product> GetProductList(
       Culture culture,
       string sortBy,
       int startIndex,
       int endIndex,
       object[] userDefined,
       out int howManyItems )
    {
        string categoryID = userDefined[ 0 ].ToString();
        string departmentID = userDefined[ 1 ].ToString();

        IList<string> list = new List<string>();

        IList<string> categoryids = DataAccessContext.CategoryRepository.GetLeafFromCategoryID( categoryID, list );

        List<string> categoryCollection = new List<string>();
        foreach ( string categoryItem in categoryids )
        {
            categoryCollection.Add( categoryItem );
        }

        IList<string> departmentIDs = new List<string>();

        if ( !String.IsNullOrEmpty( departmentID ) )
        {
            IList<string> depList = new List<string>();
            departmentIDs = DataAccessContext.DepartmentRepository.GetLeafFromDepartmentID( departmentID, depList );
        }
        List<string> departmentCollection = new List<string>();
        foreach ( string departmentItem in departmentIDs )
        {
            departmentCollection.Add( departmentItem );
        }

        return DataAccessContext.ProductRepository.GetFacetResultByCategoryID(
            culture,
            categoryCollection.ToArray(),
            departmentCollection.ToArray(),
            userDefined[ 2 ].ToString(),
            userDefined[ 3 ].ToString(),
            userDefined[ 4 ].ToString(),
            ( IList<SpecificationItemValue> ) userDefined[ 5 ],
            sortBy,
            startIndex,
            endIndex,
            BoolFilter.ShowTrue,
            out howManyItems,
            new StoreRetriever().GetCurrentStoreID()
            );
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {

        Refresh();

        CatalogUtilities.CategoryItemsPerPage = ItemPerPage;
    }

    public void Refresh()
    {
        if ( !IsFacetedSearch() )
        {
            PopulateCategoryControls();
            uxCategoryPageControlDiv.Visible = true;

            if ( DataAccessContext.Configurations.GetBoolValue( "CategoryShowProductList", new StoreRetriever().GetStore() ) )
                PopulateFacetProductControls();
        }
        else
        {
            uxCategoryPageControlDiv.Visible = false;
            PopulateFacetProductControls();
        }
    }
}
