using System;
using System.Collections.Generic;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.Shared.Utilities;
using Vevo.WebUI;
using Vevo.WebUI.Products;

public partial class AdvancedSearchResult : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private string CategoryIDs
    {
        get
        {
            if (Request.QueryString["CategoryIDs"] == null)
                return String.Empty;
            else
                return Request.QueryString["CategoryIDs"];
        }
    }

    private string DepartmentIDs
    {
        get
        {
            if (Request.QueryString["DepartmentIDs"] == null)
                return String.Empty;
            else
                return Request.QueryString["DepartmentIDs"];
        }
    }


    private string ManufacturerID
    {
        get
        {
            if (Request.QueryString["ManufacturerID"] == null)
                return String.Empty;
            else
                return Request.QueryString["ManufacturerID"];
        }
    }

    private string Keyword
    {
        get
        {
            if (Request.QueryString["Keyword"] == null)
                return String.Empty;
            else
                return Request.QueryString["Keyword"];
        }
    }

    private string SearchType
    {
        get
        {
            if (Request.QueryString["Type"] == null)
                return String.Empty;
            else
                return Request.QueryString["Type"];
        }
    }

    private bool IsQuickSearch
    {
        get
        {
            if (Request.QueryString["Quick"] == null)
                return false;
            else
                return ConvertUtilities.ToBoolean( Request.QueryString["Quick"] );
        }
    }

    private string Price1
    {
        get
        {
            if (Request.QueryString["Price1"] == null)
                return String.Empty;
            else
                return Request.QueryString["Price1"];
        }
    }

    private string Price2
    {
        get
        {
            if (Request.QueryString["Price2"] == null)
                return String.Empty;
            else
                return Request.QueryString["Price2"];
        }
    }

    private string ProductSearchType
    {
        get
        {
            if (Request.QueryString["SearchType"] == null)
                return String.Empty;
            else
                return Request.QueryString["SearchType"];
        }
    }

    private bool IsNewSearch
    {
        get
        {
            if (Request.QueryString["IsNewSearch"] == null)
                return false;
            else
                return Convert.ToBoolean( Request.QueryString["IsNewSearch"] );
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

    private string ContentMenuItemIDs
    {
        get
        {
            if (Request.QueryString["ContentMenuItemIDs"] == null)
                return String.Empty;
            else
                return Request.QueryString["ContentMenuItemIDs"];
        }
    }

    private int CalculateNumberOfPage( int itemsPerPage, int totalItems )
    {
        return (int) Math.Ceiling( (double) totalItems / itemsPerPage );
    }

    private string[] SplitColumn( string str )
    {
        char[] delimiter = new char[] { ',', ':', ';' };
        string[] result = str.Split( delimiter );
        return result;
    }

    private void PopulateProductControl()
    {
        BaseProductListControl productListControl = new BaseProductListControl();
        productListControl = LoadControl( String.Format(
            "{0}{1}",
            SystemConst.LayoutProductListPath,
            DataAccessContext.Configurations.GetValue( "DefaultProductListLayout" ) ) ) as BaseProductListControl;
        string[] productSearchType = DataAccessContext.Configurations.GetValueList( "ProductSearchBy" );
        if (!String.IsNullOrEmpty( ProductSearchType ))
        {
            productSearchType = SplitColumn( ProductSearchType );
        }
        productListControl.ID = "uxProductList";
        productListControl.DataRetriever = new DataAccessCallbacks.ProductListRetriever( GetSearchResult );
        productListControl.IsSearchResult = true;
        productListControl.UserDefinedParameters = new object[] { 
            CategoryIDs, 
            DepartmentIDs,
            ManufacturerID,
            Keyword, 
            Price1, 
            Price2,
            productSearchType,
            SearchType,
            IsNewSearch};
        uxCatalogControlPanel.Controls.Add( productListControl );
    }

    private void PopulateDepartmentProductControl()
    {
        BaseProductListControl productListControl = new BaseProductListControl();
        productListControl = LoadControl( String.Format(
            "{0}{1}",
            SystemConst.LayoutProductListPath,
            DataAccessContext.Configurations.GetValue( "DefaultProductListLayout" ) ) ) as BaseProductListControl;

        productListControl.ID = "uxDepartmentProductList";
        productListControl.DataRetriever = new DataAccessCallbacks.ProductListRetriever( GetSearchDepartmentResult );
        productListControl.IsSearchResult = true;
        productListControl.UserDefinedParameters = new object[] { 
            DepartmentIDs, 
            Keyword, 
            Price1, 
            Price2, 
            DataAccessContext.Configurations.GetValueList( "ProductSearchBy" ),
            SearchType };
        uxDepartmentPanel.Controls.Add( productListControl );
    }


    protected void Page_Load( object sender, EventArgs e )
    {
        if (IsNewSearch)
        {
            uxDefaultTitle.Text = "[$HeadProduct] for \"" + Keyword + "\"";
        }
        else
        {
            uxBackLink.Visible = false;
        }

        PopulateProductControl();
        if (DataAccessContext.Configurations.GetBoolValue( "DepartmentListModuleDisplay", new StoreRetriever().GetStore() ) && !IsNewSearch)
        {
            if (!IsQuickSearch)
                PopulateDepartmentProductControl();
            else
                uxCheckDepartmentPanel.Visible = false;
        }
        else
        {
            uxCheckDepartmentPanel.Visible = false;
        }

        if (!IsNewSearch)
        {
            if (!IsQuickSearch)
            {
                uxContentList.DataRetriever = new DataAccessCallbacks.ContentListRetriever( GetSearchContentResult );
                uxContentList.UserDefinedParameters =
                    new object[] { ContentMenuItemIDs, Keyword, DataAccessContext.Configurations.GetValueList( "ContentSearchBy" ), SearchType };
            }
            else
            {
                uxAdvancedContentSearchResult.Visible = false;
            }
        }
        else
        {
            uxAdvancedContentSearchResult.Visible = false;
        }
    }

    private static IList<Product> GetSearchResult(
        Culture culture,
        string sortBy,
        int startIndex,
        int endIndex,
        object[] userDefined,
        out int howManyItems )
    {
        if (String.IsNullOrEmpty( userDefined[3].ToString() ))
        {
            howManyItems = 0;
            return null;
        }

        if (!String.IsNullOrEmpty( userDefined[0].ToString() )
            || !String.IsNullOrEmpty( userDefined[1].ToString() )
            || !String.IsNullOrEmpty( userDefined[2].ToString() )
            || !String.IsNullOrEmpty( userDefined[3].ToString() )
            || !String.IsNullOrEmpty( userDefined[4].ToString() )
            || !String.IsNullOrEmpty( userDefined[5].ToString() ))
        {
            if ((bool) userDefined[8])
            {
                return DataAccessContext.ProductRepository.AdvancedSearch(
                        culture,
                        (string) userDefined[0],
                        (string) userDefined[1],
                        (string) userDefined[2],
                        sortBy,
                        (string) userDefined[3],
                        (string) userDefined[4],
                        (string) userDefined[5],
                        (string[]) userDefined[6],
                        startIndex,
                        endIndex,
                        out howManyItems,
                        new StoreRetriever().GetCurrentStoreID(),
                        DataAccessContext.Configurations.GetValue( "RootCategory", new StoreRetriever().GetStore() ),
                        DataAccessContext.Configurations.GetValue( "RootDepartment", new StoreRetriever().GetStore() ),
                        (string) userDefined[7]
                        );
            }
            else
            {
                return DataAccessContext.ProductRepository.AdvancedSearch(
                    culture,
                    (string) userDefined[0],
                    sortBy,
                    (string) userDefined[3],
                    (string) userDefined[4],
                    (string) userDefined[5],
                    (string[]) userDefined[6],
                    startIndex,
                    endIndex,
                    out howManyItems,
                    new StoreRetriever().GetCurrentStoreID(),
                    DataAccessContext.Configurations.GetValue( "RootCategory", new StoreRetriever().GetStore() ),
                    (string) userDefined[7]
                    );
            }
        }
        else
        {
            howManyItems = 0;
            return null;
        }
    }

    private static IList<Product> GetSearchDepartmentResult(
        Culture culture,
        string sortBy,
        int startIndex,
        int endIndex,
        object[] userDefined,
        out int howManyItems )
    {
        if (!String.IsNullOrEmpty( userDefined[0].ToString() )
            || !String.IsNullOrEmpty( userDefined[1].ToString() )
            || !String.IsNullOrEmpty( userDefined[2].ToString() )
            || !String.IsNullOrEmpty( userDefined[3].ToString() ))
        {
            return DataAccessContext.ProductRepository.AdvancedSearchDepartment(
                culture,
                (string) userDefined[0],
                sortBy,
                (string) userDefined[1],
                (string) userDefined[2],
                (string) userDefined[3],
                (string[]) userDefined[4],
                startIndex,
                endIndex,
                out howManyItems,
                new StoreRetriever().GetCurrentStoreID(),
                DataAccessContext.Configurations.GetValue( "RootDepartment", new StoreRetriever().GetStore() ),
                (string) userDefined[5]
                );
        }
        else
        {
            howManyItems = 0;
            return null;
        }
    }

    private static IList<Vevo.Domain.Contents.Content> GetSearchContentResult(
        Culture cultureID,
        string sortBy,
        int startIndex,
        int endIndex,
        object[] userDefined,
        out int howManyItems )
    {
        if (!String.IsNullOrEmpty( userDefined[0].ToString() )
            || !String.IsNullOrEmpty( userDefined[1].ToString() ))
        {
            return DataAccessContext.ContentRepository.AdvancedSearch(
                cultureID,
                new StoreRetriever().GetCurrentStoreID(),
                (string) userDefined[0],
                sortBy,
                (string) userDefined[1],
                (string[]) userDefined[2],
                startIndex,
                endIndex,
                (string) userDefined[3],
                out howManyItems );
        }
        else
        {
            howManyItems = 0;
            return null;
        }
    }
}
