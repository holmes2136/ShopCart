using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.WebUI;
using Vevo.Domain.Stores;
using Vevo.WebUI.Products;
using Vevo.Shared.Utilities;
using Vevo;

public partial class Mobile_Components_SearchResult : Vevo.WebUI.Products.BaseProductListControl
{
    private int ItemPerPage = 10;

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

    private string SortField
    {
        get
        {
            if (ViewState["SortField"] == null)
            {
                ViewState["SortField"] = uxSortField.Items[0].Value.ToString();
            }

            return (string) ViewState["SortField"];
        }

        set
        {
            ViewState["SortField"] = value;
        }
    }

    private string SortType
    {
        get
        {
            if (ViewState["SortType"] == null)
                ViewState["SortType"] = "ASC";

            return (string) ViewState["SortType"];
        }
        set
        {
            ViewState["SortType"] = value;
        }
    }

    private IList<Product> GetProductList( int itemsPerPage, string sortBy, out int totalItems )
    {
        string[] productSearchBy = DataAccessContext.Configurations.GetValueList( "ProductSearchBy" );
        IList<Product> productList = GetSearchResult(
            StoreContext.Culture,
            sortBy,
            (uxMobilePagingControl.CurrentPage - 1) * itemsPerPage,
            (uxMobilePagingControl.CurrentPage * itemsPerPage) - 1,
                new object[] { 
                "", 
                Keyword, 
                Price1, 
                Price2,
                productSearchBy},
            out totalItems );

        IsSearchResult = true;
        return productList;
    }

    private void PopulateProductControls()
    {
        Refresh();
    }

    private ScriptManager GetScriptManager()
    {
        return (ScriptManager) Page.Master.FindControl( "uxScriptManager" );
    }

    private void AddHistoryPoint()
    {
        GetScriptManager().AddHistoryPoint( "page", uxMobilePagingControl.CurrentPage.ToString() );
        GetScriptManager().AddHistoryPoint( "sortField", SortField );
        GetScriptManager().AddHistoryPoint( "sortType", SortType );
    }

    protected void uxSortUpLink_Click( object sender, EventArgs e )
    {
        SortType = "DESC";
        AddHistoryPoint();
        Refresh();
    }

    protected void uxSortDownLink_Click( object sender, EventArgs e )
    {
        SortType = "ASC";
        AddHistoryPoint();
        Refresh();
    }

    protected void uxFieldSortDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        SortField = uxSortField.SelectedValue;
        uxSortValueHidden.Value = uxSortField.SelectedValue;
        AddHistoryPoint();
        Refresh();
    }

    protected void uxMobilePagingControl_BubbleEvent( object sender, EventArgs e )
    {
        AddHistoryPoint();
        Refresh();
    }

    protected void DisplaySortType()
    {
        if (SortType == "ASC")
        {
            uxSortUpLink.Visible = true;
            uxSortDownLink.Visible = false;
        }
        else
        {
            uxSortUpLink.Visible = false;
            uxSortDownLink.Visible = true;
        }
    }

    protected void ScriptManager_Navigate( object sender, HistoryEventArgs e )
    {
        string args;

        if (!string.IsNullOrEmpty( e.State["sortField"] ))
        {
            SortField = e.State["sortField"].ToString();
        }
        else
        {
            SortField = uxSortField.Items[0].Value.ToString();
        }

        if (!string.IsNullOrEmpty( e.State["sortType"] ))
        {
            SortType = e.State["sortType"].ToString();
        }
        else
        {
            SortType = "ASC";
        }

        int totalItems;
        GetProductList( ItemPerPage, SortField + " " + SortType, out totalItems );

        uxMobilePagingControl.NumberOfPages = (int) System.Math.Ceiling( (double) totalItems / ItemPerPage );

        if (!string.IsNullOrEmpty( e.State["page"] ))
        {
            args = e.State["page"];
            uxMobilePagingControl.CurrentPage = int.Parse( args );
        }
        else
        {
            uxMobilePagingControl.CurrentPage = 1;
        }

        Refresh();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxMobilePagingControl.BubbleEvent += new EventHandler( uxMobilePagingControl_BubbleEvent );
        GetScriptManager().Navigate += new EventHandler<HistoryEventArgs>( ScriptManager_Navigate );

        uxMobileList.RepeatColumns = DataAccessContext.Configurations.GetIntValue( "NumberOfProductColumn" );
        uxMobileList.RepeatDirection = RepeatDirection.Horizontal;
        uxMobileList.Visible = true;
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateProductControls();
    }

    protected void uxMobileList_OnItemDataBound( object sender, DataListItemEventArgs e )
    {
        int totalItems;
        if (e.Item.ItemIndex == GetProductList( ItemPerPage, SortField + " " + SortType, out totalItems ).Count - 1)
            e.Item.BorderStyle = BorderStyle.None;
    }

    private Product GetProduct( string productID )
    {
        Product product = DataAccessContext.ProductRepository.GetOne(
            StoreContext.Culture, productID, DataAccessContext.StoreRetriever.GetCurrentStoreID() );

        return product;
    }

    public void Refresh()
    {
        DisplaySortType();
        uxSortField.SelectedValue = SortField;

        int totalItems;

        uxMobileList.DataSource = GetProductList( ItemPerPage, SortField + " " + SortType, out totalItems );
        uxMobileList.DataBind();

        uxMobilePagingControl.NumberOfPages = (int) System.Math.Ceiling( (double) totalItems / ItemPerPage );
    }


    protected string GetFormattedPriceFromContainer( object productID )
    {

        Product currentProduct = GetProduct( productID.ToString() );

        decimal price = currentProduct.GetDisplayedPrice( StoreContext.WholesaleStatus );

        return StoreContext.Currency.FormatPrice( price );
    }

    protected string GetProUrl( object productID, object urlName )
    {
        return Vevo.UrlManager.GetMobileProductUrl( productID, urlName );
    }

    private IList<Product> GetSearchResult(
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
            return DataAccessContext.ProductRepository.AdvancedSearch(
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
                DataAccessContext.Configurations.GetValue( "RootCategory", new StoreRetriever().GetStore() ),
                false,
                DataAccessContext.Configurations.GetValue( "SearchMode", new StoreRetriever().GetStore() )
                );
        }
        else
        {
            howManyItems = 0;
            return null;
        }
    }
    protected bool IsFixedPrice( object isFixedPrice, object isCustomPrice, object isCallForPrice )
    {
        if (ConvertUtilities.ToBoolean( isCallForPrice ))
            return false;

        if (ConvertUtilities.ToBoolean( isCustomPrice ))
            return false;

        if (ConvertUtilities.ToBoolean( isFixedPrice ))
            return true;
        else
            return false;
    }

    protected bool IsCallForPrice( object isCallForPrice )
    {
        if (ConvertUtilities.ToBoolean( isCallForPrice ))
            return true;
        else
            return false;
    }

    protected bool IsAuthorizedToViewPrice()
    {
        return IsAuthorizedToViewPrice( false );
    }

    protected bool IsAuthorizedToViewPrice( object isCallForPrice )
    {
        bool show = true;

        if (ConvertUtilities.ToBoolean( isCallForPrice ))
            return false;

        if (DataAccessContext.Configurations.GetBoolValue( "PriceRequireLogin" ) && !Page.User.Identity.IsAuthenticated)
        {
            show = false;
        }

        return show;
    }

    protected decimal GetRetailPrice( object productID )
    {
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, (string) productID, StoreContext.CurrentStore.StoreID );
        ProductPrice productPrice = product.GetProductPrice( StoreContext.CurrentStore.StoreID );

        return productPrice.RetailPrice;
    }

    protected string GetDiscountPercent( object productID )
    {
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, (string) productID, StoreContext.CurrentStore.StoreID );
        ProductPrice productPrice = product.GetProductPrice( StoreContext.CurrentStore.StoreID );

        if (productPrice.Price > 0 && productPrice.RetailPrice > 0)
        {
            decimal discount = (decimal) (((productPrice.RetailPrice - productPrice.Price) / productPrice.RetailPrice) * 100);
            if (discount < 1)
                return "0%";
            else
                return ((int) Math.Ceiling( discount )).ToString() + "%";
        }
        else
            return "0%";
    }

    protected bool IsDiscount( object productID )
    {
        if (GetDiscountPercent( productID ) != "0%")
            return true;
        else
            return false;
    }

    protected bool IsRetailPriceEnabled( object isFixedPrice, object isCustomPrice, decimal retailPrice, object isCallForPrice )
    {
        if (!IsAuthorizedToViewPrice())
            return false;

        if (ConvertUtilities.ToBoolean( isCallForPrice ))
            return false;

        if (ConvertUtilities.ToBoolean( isCustomPrice ))
            return false;

        if (CatalogUtilities.IsRetailMode &&
            ConvertUtilities.ToBoolean( isFixedPrice ) &&
            ConvertUtilities.ToDecimal( retailPrice ) > 0
            )
            return true;
        else
            return false;
    }
}
