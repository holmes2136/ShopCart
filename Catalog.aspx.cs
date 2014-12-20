using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Configurations;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.Shared.Utilities;
using Vevo.WebUI;
using Vevo.WebUI.International;
using Vevo.WebUI.Products;

public partial class Catalog : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private bool IsFacetSearch()
    {
        string[] allKey = Request.QueryString.AllKeys;

        if (allKey.Length > 1)
            return true;
        else
            return false;
    }

    private string CatalogID
    {
        get
        {
            if (ViewState["CatalogID"] == null)
                return "0";
            else
                return (string) ViewState["CatalogID"];
        }
        set
        {
            ViewState["CatalogID"] = value;
        }
    }

    private string CurrentCategoryID
    {
        get
        {
            string id = Request.QueryString["CategoryID"];
            if (!String.IsNullOrEmpty( id ))
            {
                return id;
            }
            else
            {
                Category category = DataAccessContext.CategoryRepository.GetOneByUrlName(
                    StoreContext.Culture, CurrentCategoryName );
                return category.CategoryID;
            }
        }
    }

    private string CurrentCategoryName
    {
        get
        {
            if (Request.QueryString["CategoryName"] == null)
                return String.Empty;
            else
                return Request.QueryString["CategoryName"];
        }
    }

    private string MinPrice
    {
        get
        {
            if (Request.QueryString["MinPrice"] == null)
                return String.Empty;
            else
                return Request.QueryString["MinPrice"];
        }
    }

    private string MaxPrice
    {
        get
        {
            if (Request.QueryString["MaxPrice"] == null)
                return String.Empty;
            else
                return Request.QueryString["MaxPrice"];
        }
    }

    private bool IsParentOfOtherCategories()
    {
        if (!String.IsNullOrEmpty( CurrentCategoryName ))
        {
            return DataAccessContext.CategoryRepository.IsUrlNameNotLeaf( CurrentCategoryName );
        }
        else if (!String.IsNullOrEmpty( CurrentCategoryID ))
        {
            return DataAccessContext.CategoryRepository.IsCategoryIDNotLeaf( CurrentCategoryID );
        }
        else
        {
            return false;
        }
    }

    private void PopulateTitleAndMeta( DynamicPageElement element )
    {
        Category category = DataAccessContext.CategoryRepository.GetOne(
            StoreContext.Culture, CurrentCategoryID );
        string title = SeoVariable.ReplaceCategoryVariable(
                category,
                StoreContext.Culture,
                DataAccessContext.Configurations.GetValue( StoreContext.Culture.CultureID, "DefaultCategoryPageTitle", StoreContext.CurrentStore ) );

        element.SetUpTitleAndMetaTags(
            category.GetPageTitle( StoreContext.Culture, title ),
            category.GetMetaDescription( StoreContext.Culture ),
            category.GetMetaKeyword( StoreContext.Culture ) );
    }

    private void PopulateControls()
    {
        DynamicPageElement element = new DynamicPageElement( this );
        if (CurrentCategoryID == "0")
        {
            if (CurrentCategoryName == "")
                element.SetUpTitleAndMetaTags( "[$Title] - " + NamedConfig.SiteName, NamedConfig.SiteDescription );
            else
                Response.Redirect( "~/Error404.aspx" );
        }
        else
        {
            Category category = DataAccessContext.CategoryRepository.GetOne(
                StoreContext.Culture, CurrentCategoryID );

            if (category.IsCategoryAvailableStore( StoreContext.CurrentStore.StoreID ) && (category.IsParentsEnable()))
                PopulateTitleAndMeta( element );
            else
                Response.Redirect( "~/Error404.aspx" );
        }
    }

    private string GetCategoryID()
    {
        string categoryID = Request.QueryString["cat"];
        if (!String.IsNullOrEmpty( categoryID ))
            return categoryID;
        else
            return CurrentCategoryID;
    }

    private string GetDepartmentID()
    {
        string departmentID = "";

        if (!String.IsNullOrEmpty( Request.QueryString["dep"] ))
        {
            departmentID = Request.QueryString["dep"];
        }

        return departmentID;
    }
    private string GetManufacturerID()
    {
        string manufacturerID = String.Empty;
        if (!String.IsNullOrEmpty( Request.QueryString["manu"] ))
        {
            manufacturerID = Request.QueryString["manu"];
        }
        return manufacturerID;
    }

    private void PopulateUserControl()
    {
        uxProductControlPanel.Visible = false;
        uxCatalogControlPanel.Visible = false;
        Category category = DataAccessContext.CategoryRepository.GetOne(
            StoreContext.Culture, CurrentCategoryID );

        uxCatalogNameLabel.Text = category.Name;
        if (IsParentOfOtherCategories())
        {
            uxCatalogControlPanel.Visible = true;
            BaseCategoryListControl catalogControl = new BaseCategoryListControl();
            if (!String.IsNullOrEmpty( category.CategoryListLayoutPath ))
                catalogControl = LoadControl( String.Format( "{0}{1}",
                        SystemConst.LayoutCategoryListPath, category.CategoryListLayoutPath ) )
                    as BaseCategoryListControl;
            else
                catalogControl = LoadControl( String.Format( "{0}{1}",
                        SystemConst.LayoutCategoryListPath,
                        DataAccessContext.Configurations.GetValue( "DefaultCategoryListLayout" ) ) )
                    as BaseCategoryListControl;

            uxCatalogControlPanel.Controls.Add( catalogControl );
        }
        else
        {
            uxProductControlPanel.Visible = true;
            BaseProductListControl productListControl = new BaseProductListControl();
            if (!String.IsNullOrEmpty( category.ProductListLayoutPath ))
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
            productListControl.DataRetriever = new DataAccessCallbacks.ProductListRetriever( GetProductList );
            productListControl.IsSearchResult = IsFacetSearch();
            productListControl.UserDefinedParameters = new object[] { 
                GetCategoryID(),
                GetDepartmentID(),
                GetManufacturerID(),
                MinPrice,
                MaxPrice,
                GetSpecItemValueList( GetAllSpecKey() ),
                IsFacetSearch()};
            uxProductControlPanel.Controls.Add( productListControl );
        }
    }

    private void SetUpBreadcrumb()
    {
        if (CurrentCategoryID == "0" && CurrentCategoryName == String.Empty)
        {
            uxCatalogBreadcrumb.Visible = false;
            uxCatalogNameLabel.CssClass = "CatalogName CatalogRoot";
        }
        else
        {
            uxCatalogBreadcrumb.SetupCategorySitemap( CurrentCategoryID );
            uxCatalogBreadcrumb.Refresh();
        }
    }

    private void Catalog_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        if (CurrentCategoryName == "")
        {
            Response.Redirect( "~/Catalog.aspx" );
        }
        else
        {
            Category category = DataAccessContext.CategoryRepository.GetOne(
                    StoreContext.Culture, CatalogID );

            if (!String.IsNullOrEmpty( category.UrlName ))
            {
                Response.Redirect( UrlManager.GetCategoryUrl( CatalogID, category.UrlName ) );
            }
            else
            {
                Response.Redirect( "~/Error404.aspx" );
            }
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        Response.Cache.SetCacheability( HttpCacheability.NoCache );
        Response.Expires = 0;
        Response.Cache.SetNoStore();
        Response.AppendHeader( "Pragma", "no-cache" );

        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler( Catalog_StoreCultureChanged );

        if (!IsPostBack)
        {
            PopulateControls();
            CatalogID = CurrentCategoryID;
        }

        PopulateUserControl();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        SetUpBreadcrumb();
    }

    private IList<string> GetAllSpecKey()
    {
        string[] allKey = Request.QueryString.AllKeys;

        string query = String.Empty;
        IList<string> specKeyList = new List<string>();
        for (int i = 0; i < allKey.Length; i++)
        {
            if (!allKey[i].ToLower().Equals( "categoryname" ) && !allKey[i].ToLower().Equals( "categoryid" ))
            {
                if (allKey[i].ToLower().Equals( "minprice" ) ||
                    allKey[i].ToLower().Equals( "maxprice" ) ||
                    allKey[i].ToLower().Equals( "cat" ) ||
                    allKey[i].ToLower().Equals( "dep" ) ||
                    allKey[i].ToLower().Equals( "manu" ))
                {
                    continue;
                }

                specKeyList.Add( allKey[i] );
            }
        }

        return specKeyList;
    }

    private IList<SpecificationItemValue> GetSpecItemValueList( IList<string> specKeyList )
    {
        IList<SpecificationItemValue> specItemValueList = new List<SpecificationItemValue>();
        foreach (string specKey in specKeyList)
        {
            string specValue = Request.QueryString[specKey];

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
        string categoryID = userDefined[0].ToString();
        string departmentID = userDefined[1].ToString();
        bool isFacet = ConvertUtilities.ToBoolean( userDefined[6] );

        IList<string> list = new List<string>();

        IList<string> categoryids = DataAccessContext.CategoryRepository.GetLeafFromCategoryID( categoryID, list );

        List<string> categoryCollection = new List<string>();
        foreach (string categoryItem in categoryids)
        {
            categoryCollection.Add( categoryItem );
        }

        IList<string> departmentIDs = new List<string>();

        if (!String.IsNullOrEmpty( departmentID ))
        {
            IList<string> depList = new List<string>();
            departmentIDs = DataAccessContext.DepartmentRepository.GetLeafFromDepartmentID( departmentID, depList );
        }
        List<string> departmentCollection = new List<string>();
        foreach (string departmentItem in departmentIDs)
        {
            departmentCollection.Add( departmentItem );
        }

        if (isFacet)
            return DataAccessContext.ProductRepository.GetFacetResultByCategoryID(
                culture,
                categoryCollection.ToArray(),
                departmentCollection.ToArray(),
                userDefined[2].ToString(),
                userDefined[3].ToString(),
            userDefined[4].ToString(),
            (IList<SpecificationItemValue>) userDefined[5],
                sortBy,
                startIndex,
                endIndex,
                BoolFilter.ShowTrue,
                out howManyItems,
                new StoreRetriever().GetCurrentStoreID()
                );
        else
            return DataAccessContext.ProductRepository.GetByCategoryID(
             culture,
             userDefined[0].ToString(),
             sortBy,
             startIndex,
             endIndex,
             BoolFilter.ShowTrue,
             out howManyItems,
             new StoreRetriever().GetCurrentStoreID()
             );
    }
}
