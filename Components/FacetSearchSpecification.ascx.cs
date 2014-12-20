using System;
using System.Collections.Generic;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Shared.Utilities;
using Vevo.WebUI;
using System.Data;

public partial class Components_FacetSearchSpecification : Vevo.WebUI.Products.BaseFacetSearchControl
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

    private bool IsProductExistInGroup( SpecificationItem specItem )
    {
        IList<SpecificationItemValue> specItemValueList = DataAccessContext.SpecificationItemValueRepository.GetBySpecificationItemID( StoreContext.Culture, specItem.SpecificationItemID );
        string startPrice = MinPrice;
        string endPrice = MaxPrice;

        foreach (SpecificationItemValue specItemValue in specItemValueList)
        {
            int itemCount = DataAccessContext.ProductRepository.GetFacetCount(
                    StoreContext.Culture,
                    GetCategoryList(),
                    GetDepartmentListWithLeaf(),
                    GetManufacturerID(),
                    startPrice,
                    endPrice,
                    GetSpecItemValueList( GetAllSpecKey(), specItemValue ),
                    "",
                    BoolFilter.ShowTrue,
                    StoreContext.CurrentStore.StoreID );

            if (itemCount > 0)
                return true;
        }

        return false;
    }


    private IList<SpecificationItem> GetSpecItem()
    {
        string categoryID = Request.QueryString["cat"];

        IList<String> productList = DataAccessContext.ProductRepository.GetProductIDListByCategoryDepartment(
            GetCategoryList(),
            GetDepartmentListWithLeaf(),
            GetManufacturerID(),
            BoolFilter.ShowTrue,
            StoreContext.CurrentStore.StoreID );

        IList<SpecificationItem> specItemList = new List<SpecificationItem>();

        if (productList.Count > 0)
        {
            IList<string> productSpecList = DataAccessContext.ProductRepository.GetProductSpecificationsIDByProductIDs( productList );

            foreach (string productSpec in productSpecList)
            {
                SpecificationItem specItem = DataAccessContext.SpecificationItemRepository.GetOne( StoreContext.Culture, productSpec );

                if (specItem.UseInFacetedSearch && IsSpecVisible( specItem ) && IsProductExistInGroup( specItem ))
                    specItemList.Add( specItem );
            }
        }
        return specItemList;
    }

    private void PopulateControls()
    {
        if (!IsFacetedVisible())
        {
            this.Visible = false;
        }
        else
        {
            uxList.DataSource = GetSpecItem();
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

    #endregion

    #region Public

    public bool IsSpecVisible( SpecificationItem specItem )
    {
        string[] queryKey = Request.QueryString.AllKeys;

        foreach (string key in queryKey)
        {
            if (key.ToLower() == specItem.Name.ToLower())
            {
                return false;
            }
        }

        return true;
    }

    #endregion
}