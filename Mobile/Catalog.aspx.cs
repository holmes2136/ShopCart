using System;
using System.Collections.Generic;
using System.Web.UI;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.WebUI;
using Vevo.WebUI.Products;
using Vevo.Domain.Configurations;

public partial class Mobile_Catalog : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private string CurrentCategoryID
    {
        get
        {
            string id = Request.QueryString["CategoryID"];
            if (!string.IsNullOrEmpty( id ))
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
            {
                return String.Empty;
            }
            else
            {
                return Request.QueryString["CategoryName"].Split( ',' )[0];
            }
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

    private void RefreshTitle()
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

    private void PopulateUserControl()
    {
        Category category = DataAccessContext.CategoryRepository.GetOne(
            StoreContext.Culture, CurrentCategoryID );

        if (IsParentOfOtherCategories())
        {
            UserControl catalogControl = new UserControl();
            catalogControl = LoadControl( "Components/CatagoryList.ascx" ) as UserControl;

            uxMobileCatalogControlPanel.Controls.Add( catalogControl );
        }
        else
        {
            BaseProductListControl productListControl = new BaseProductListControl();
            productListControl = LoadControl( "Components/ProductList.ascx" ) as BaseProductListControl;

            productListControl.ID = "uxMobileProductList";
            productListControl.DataRetriever = new DataAccessCallbacks.ProductListRetriever( GetProductList );
            productListControl.UserDefinedParameters = new object[] { CurrentCategoryID };
            uxMobileCatalogControlPanel.Controls.Add( productListControl );
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        PopulateUserControl();

        if (!String.IsNullOrEmpty( CurrentCategoryName ))
        {
            Category category = DataAccessContext.CategoryRepository.GetOne( StoreContext.Culture, CurrentCategoryID );
            uxMobileCurrentCategoryName.Text = category.Name;
            uxMobileCurrentCategoryMenu.Visible = true;
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        RefreshTitle();
    }

    public static IList<Product> GetProductList(
        Culture culture,
        string sortBy,
        int startIndex,
        int endIndex,
        object[] userDefined,
        out int howManyItems )
    {
        return DataAccessContext.ProductRepository.GetByCategoryID(
            culture,
            userDefined[0].ToString(),
            sortBy,
            startIndex,
            endIndex,
            BoolFilter.ShowTrue,
            true,
            out howManyItems,
            new StoreRetriever().GetCurrentStoreID()
            );
    }
}
