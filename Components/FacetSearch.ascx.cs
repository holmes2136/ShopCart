using System;
using System.Collections.Generic;
using Vevo.Domain;
using Vevo.Shared.Utilities;
using Vevo.WebUI;

public partial class Components_FacetSearch : Vevo.WebUI.Products.BaseFacetSearchControl
{
    #region Private

    private string[] GetCategoryList()
    {
        string categoryID = Request.QueryString["cat"];

        if (String.IsNullOrEmpty( categoryID ))
        {
            return GetCategoryListwithLeaf();
        }
        else
            return GetCategoryListwithLeaf( categoryID );
    }

    private string GetStartPrice( decimal currentPrice )
    {
        return ConvertUtilities.ToString( ConvertUtilities.ToDecimal( currentPrice - PriceStep ) );
    }

    private string GetEndPrice( decimal currentPrice )
    {
        return ConvertUtilities.ToString( currentPrice - ConvertUtilities.ToDecimal( 0.01 ) );
    }

    private IList<string> CreateDataSource()
    {
        decimal maxPrice = DataAccessContext.ProductRepository.GetMaxPriceByCategoryDepartment(
            StoreContext.Culture,
            GetCategoryList(),
            GetDepartmentListWithLeaf(),
            BoolFilter.ShowTrue,
            StoreContext.CurrentStore.StoreID );

        IList<string> list = new List<string>();
        int i = PriceStep;
        int level = 1;
        while ((i < maxPrice || i - maxPrice < PriceStep) && level <= MaxLevel)
        {
            if (IsVisible( ConvertUtilities.ToDecimal( i ) ))
                list.Add( i.ToString() );
            i = i + PriceStep;
            level++;
        }

        if (list.Count == 0)
        {
            uxPriceTitle.Visible = false;
        }

        return list;
    }

    private void PopulateControls()
    {
        if (!IsFacetedVisible())
        {
            this.Visible = false;
        }
        else
        {
            if (DataAccessContext.Configurations.GetBoolValue( "PriceRequireLogin" ) && !Page.User.Identity.IsAuthenticated)
            {
                uxPricePanel.Visible = false;
            }
            else
            {
                uxList.DataSource = CreateDataSource();
                uxList.DataBind();
            }
        }
    }

    private string GetDisplayText()
    {
        if (String.IsNullOrEmpty( MaxPrice ))
        {
            return StoreContext.Currency.FormatPrice( ConvertUtilities.ToDecimal( MinPrice ) ) + GetLanguageText( "andabove" );
        }

        return StoreContext.Currency.FormatPrice( ConvertUtilities.ToDecimal( MinPrice ) ) + " - " + StoreContext.Currency.FormatPrice( ConvertUtilities.ToDecimal( MaxPrice ) );
    }

    private void Components_FacetSearch_StoreCurrencyChanged( object sender, CurrencyEventArgs e )
    {
        PopulateControls();
    }

    private bool IsVisible( decimal price )
    {
        if (GetCountItem( price ) > 0)
            return true;
        else
            return false;
    }

    private decimal GetCountItem( decimal price )
    {
        decimal currentPrice = ConvertUtilities.ToDecimal( price );
        string startPrice = GetStartPrice( currentPrice );
        string endPrice = GetEndPrice( currentPrice );

        decimal maxPrice = DataAccessContext.ProductRepository.GetMaxPriceByCategoryDepartment(
            StoreContext.Culture,
            GetCategoryList(),
            GetDepartmentListWithLeaf(),
            BoolFilter.ShowTrue,
            StoreContext.CurrentStore.StoreID );

        if (currentPrice >= maxPrice || currentPrice >= (PriceStep * MaxLevel))
        {
            endPrice = String.Empty;
        }

        IList<string> specKeyList = GetAllSpecKey();

        int itemCount = DataAccessContext.ProductRepository.GetFacetCount(
                StoreContext.Culture,
                GetCategoryList(),
                GetDepartmentListWithLeaf(),
                GetManufacturerID(),
                startPrice,
                endPrice,
                GetSpecItemValueList( specKeyList ),
                "",
                BoolFilter.ShowTrue,
                StoreContext.CurrentStore.StoreID );

        return itemCount;
    }

    #endregion

    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        GetStorefrontEvents().StoreCurrencyChanged +=
            new StorefrontEvents.CurrencyEventHandler( Components_FacetSearch_StoreCurrencyChanged );

        if (!String.IsNullOrEmpty( MaxPrice ) || !String.IsNullOrEmpty( MinPrice ))
        {
            uxList.Visible = false;
            uxPriceTitle.Visible = false;
        }
        else
        {
            uxList.Visible = true;
            uxPriceTitle.Visible = true;
        }

        PopulateControls();
    }


    protected string CountItem( object price )
    {
        decimal currentPrice = ConvertUtilities.ToDecimal( price );
        return " (" + GetCountItem( currentPrice ) + ") ";
    }

    protected string GetNavName( object price )
    {
        decimal currentPrice = ConvertUtilities.ToDecimal( price );
        string startPrice = GetStartPrice( currentPrice );
        string endPrice = GetEndPrice( currentPrice );

        decimal maxPrice = DataAccessContext.ProductRepository.GetMaxPriceByCategoryDepartment(
            StoreContext.Culture,
            GetCategoryList(),
            GetDepartmentListWithLeaf(),
            BoolFilter.ShowTrue,
            StoreContext.CurrentStore.StoreID );

        string startRange = StoreContext.Currency.FormatPrice( startPrice );
        string endRange = String.Empty;
        if (currentPrice >= maxPrice || currentPrice >= (PriceStep * MaxLevel))
        {
            endRange = GetLanguageText( "andabove" );
        }
        else
        {
            endRange = " - " + StoreContext.Currency.FormatPrice( endPrice );
        }

        return startRange + endRange;
    }

    protected string GetNavUrl( object price )
    {
        decimal currentPrice = ConvertUtilities.ToDecimal( price );
        string startPrice = GetStartPrice( currentPrice );
        string endPrice = GetEndPrice( currentPrice );

        string[] index = Request.Url.Query.Split( '&' );
        int count = index[0].Length;
        if (Request.Url.Query.Contains( "&" ))
        {
            count = index[0].Length + 1;
        }

        string query = Request.Url.Query.Remove( 0, count );
        string priceQuery = String.Empty;

        decimal maxPrice = DataAccessContext.ProductRepository.GetMaxPriceByCategoryDepartment(
            StoreContext.Culture,
            GetCategoryList(),
            GetDepartmentListWithLeaf(),
            BoolFilter.ShowTrue,
            StoreContext.CurrentStore.StoreID );

        if (currentPrice >= maxPrice || currentPrice >= (PriceStep * MaxLevel))
            priceQuery = "minprice=" + startPrice;
        else

            priceQuery = "minprice=" + startPrice + "&maxprice=" + endPrice;

        if (String.IsNullOrEmpty( query ))
            return Vevo.UrlManager.GetCategoryUrl( CurrentCategoryID, CurrentCategory.UrlName ) + "?" + priceQuery;
        else
            return Vevo.UrlManager.GetCategoryUrl( CurrentCategoryID, CurrentCategory.UrlName ) + "?" + query + "&" + priceQuery;
    }

    #endregion
}