using System;
using System.Collections.Generic;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Shared.Utilities;
using Vevo.WebUI;
using System.Data;

public partial class Components_FacetSearchSpecificationValue : Vevo.WebUI.Products.BaseFacetSearchControl
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


    private void PopulateControls()
    {
        if (!IsFacetedVisible())
        {
            this.Visible = false;
        }
        else
        {
            uxList.DataSource = GetSpecItemValue();
            uxList.DataBind();
        }
    }

    private void Components_FacetSearch_StoreCurrencyChanged( object sender, CurrencyEventArgs e )
    {
        PopulateControls();
    }

    #endregion

    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        GetStorefrontEvents().StoreCurrencyChanged +=
            new StorefrontEvents.CurrencyEventHandler( Components_FacetSearch_StoreCurrencyChanged );

        PopulateControls();
    }

    protected bool IsVisible( string specItemValueID )
    {
        if (GetCountItem( specItemValueID.ToString() ) > 0)
            return true;
        else
            return false;
    }

    protected int GetCountItem( string specItemValueID )
    {
        string startPrice = MinPrice;
        string endPrice = MaxPrice;
        SpecificationItemValue specItemValue = DataAccessContext.SpecificationItemValueRepository.GetOne( StoreContext.Culture, specItemValueID.ToString() );


        IList<string> specKeyList = GetAllSpecKey();


        int itemCount = DataAccessContext.ProductRepository.GetFacetCount(
                StoreContext.Culture,
                GetCategoryList(),
                GetDepartmentListWithLeaf(),
                GetManufacturerID(),
                startPrice,
                endPrice,
                GetSpecItemValueList( specKeyList, specItemValue ),
                "",
                BoolFilter.ShowTrue,
                StoreContext.CurrentStore.StoreID );

        return itemCount;
    }

    protected string GetCountSpecItem( object specItemValueID )
    {
        return " (" + GetCountItem( specItemValueID.ToString() ) + ") ";
    }

    protected string GetNavName( object specValue )
    {
        return specValue.ToString();
    }

    protected string GetNavUrl( object specValue )
    {
        string currentValue = specValue.ToString();
        string[] index = Request.Url.Query.Split( '&' );
        int count = index[0].Length;
        if (Request.Url.Query.Contains( "&" ))
        {
            count = index[0].Length + 1;
        }

        string query = Request.Url.Query.Remove( 0, count );


        if (String.IsNullOrEmpty( query ))
            return Vevo.UrlManager.GetCategoryUrl( CurrentCategoryID, CurrentCategory.UrlName ) + "?" +
                System.Web.HttpContext.Current.Server.UrlEncode( uxHiddenName.Value ) + "=" + currentValue;
        else
            return Vevo.UrlManager.GetCategoryUrl( CurrentCategoryID, CurrentCategory.UrlName ) + "?" + query + "&" +
                System.Web.HttpContext.Current.Server.UrlEncode( uxHiddenName.Value ) + "=" + currentValue;
    }

    protected string GetGroupText( object text )
    {
        return text.ToString();
    }

    #endregion

    #region Public

    public IList<SpecificationItemValue> GetSpecItemValue()
    {
        IList<SpecificationItemValue> specItemValueList = DataAccessContext.SpecificationItemValueRepository.GetBySpecificationItemID( StoreContext.Culture, uxHiddenID.Value );
        IList<SpecificationItemValue> visibleSpecItemValueList = new List<SpecificationItemValue>();

        foreach (SpecificationItemValue specValue in specItemValueList)
        {
            if (IsVisible( specValue.SpecificationItemValueID ))
                visibleSpecItemValueList.Add( specValue );
        }

        return visibleSpecItemValueList;
    }

    #endregion
}