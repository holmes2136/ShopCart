using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.DataInterfaces;
using Vevo.Domain.Marketing;
using Vevo.Domain.Orders;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.Shared.Utilities;
using Vevo.WebUI;
using Vevo.Base.Domain;
using Vevo.Deluxe.Domain.BundlePromotion;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.Products;

public partial class Admin_Components_PromotionalProductList : AdminAdvancedBaseListControl
{
    #region Private
    private const int ProductIDColumnIndex = 1;
    private const int StockColumnIndex = 5;
    private const int RetailColumnIndex = 7;

    public Culture CurrentCulture
    {
        get
        {
            if (ViewState["CurrentCulture"] == null)
                return new Culture();
            else
                return (Culture) ViewState["CurrentCulture"];
        }
        set
        {
            ViewState["CurrentCulture"] = value;
            RefreshGrid();
        }

    }

    public string PromotionSubGroupID
    {
        get
        {
            if (ViewState["PromotionSubGroupID"] == null)
                return new PromotionSubGroup().PromotionSubGroupID;
            else
                return ViewState["PromotionSubGroupID"].ToString();
        }
        set
        {
            ViewState["PromotionSubGroupID"] = value;
        }

    }

    private string SelectedStoreID
    {
        get
        {
            if (MainContext.QueryString["StoreID"] == null)
                return Store.RegularStoreID;
            else
                return MainContext.QueryString["StoreID"];
        }
    }

    private string RootCategoryValue
    {
        get
        {
            if (!KeyUtilities.IsMultistoreLicense())
                return DataAccessContext.Configurations.GetValue(
                    "RootCategory",
                    DataAccessContext.StoreRepository.GetOne( Store.RegularStoreID ) );
            else
                return uxCategoryFilterDrop.SelectedRootValue;
        }
    }

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.ProductRepository.GetTableSchema();
        uxSearchFilter.SetUpSchema( list, "UrlName",
            "UseDefaultMetaKeyword", "UseDefaultMetaDescription",
            "UseDefaultPrice", "UseDefaultRetailPrice",
            "UseDefaultWholeSalePrice", "UseDefaultWholeSalePrice2", "UseDefaultWholeSalePrice3" );
    }

    private void PopulateControls()
    {
        if (!MainContext.IsPostBack)
        {
            RefreshGrid();
        }

        if (uxGridProduct.Rows.Count > 0)
        {
            uxPagingControl.Visible = true;
        }
        else
        {
            uxPagingControl.Visible = false;
        }
    }


    private bool IsAnyOptionOutOfStock( string productID )
    {
        Product product = DataAccessContext.ProductRepository.GetOne(
            CurrentCulture, productID, new StoreRetriever().GetCurrentStoreID() );
        return product.IsOutOfStock();
    }

    private bool TaxIncludeVisibility()
    {
        return DataAccessContext.Configurations.GetBoolValue( "TaxIncludedInPrice" );
    }

    private void SetUpGridSupportControls()
    {
        if (!MainContext.IsPostBack)
        {
            uxPagingControl.ItemsPerPages = AdminConfig.ProductItemsPerPage;
            SetUpSearchFilter();
            uxCategoryFilterDrop.CultureID = CurrentCulture.CultureID;
            uxCurrencyControl.StoreID = SelectedStoreID;
        }

        RegisterGridView( uxGridProduct, "ProductID" );

        RegisterSearchFilter( uxSearchFilter );
        RegisterPagingControl( uxPagingControl );
        RegisterCategoryFilterDrop( uxCategoryFilterDrop );
        RegisterCurrencyFilterDrop( uxCurrencyControl );

        uxSearchFilter.BubbleEvent += new EventHandler( uxSearchFilter_BubbleEvent );
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
        uxCategoryFilterDrop.BubbleEvent += new EventHandler( uxCategoryFilterDrop_BubbleEvent );
        uxCurrencyControl.BubbleEvent += new EventHandler( uxCurrencyControl_BubbleEvent );
    }

    #endregion


    protected void Page_Load( object sender, EventArgs e )
    {
        SetUpGridSupportControls();
        DataAccessContext.SetStoreRetriever( new StoreRetriever( SelectedStoreID ) );
        uxPromotionProductDetails.BubbleEvent += new EventHandler( CheckAll_RefreshHandler );
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            uxCurrencyControl.StoreID = SelectedStoreID;

            uxCategoryFilterDrop.CultureID = CurrentCulture.CultureID;
            uxCategoryFilterDrop.RefreshCategoryDropList(
            "0" );

            StoreContext.ClearCheckoutSession();
        }

        uxCurrencyControl.StoreID = SelectedStoreID;
        PopulateControls();
    }


    protected void uxGridProduct_RowDataBound( object sender, GridViewRowEventArgs e )
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string productID = e.Row.Cells[ProductIDColumnIndex].Text.Trim();
            int result;
            if (DataAccessContext.Configurations.GetBoolValue( "UseStockControl" ))
            {
                if (int.TryParse( productID, out result ))
                {
                    if (IsAnyOptionOutOfStock( productID ))
                    {
                        foreach (TableCell cell in e.Row.Cells)
                        {
                            cell.Style.Add( "color", "#ff0000" );
                        }
                    }
                }
            }

            if (e.Row.RowIndex > -1)
            {
                if ((e.Row.RowIndex % 2) == 0)
                {
                    // Even
                    e.Row.Attributes.Add( "onmouseover", "this.className='DefaultGridRowStyleHover'" );
                    e.Row.Attributes.Add( "onmouseout", "this.className='DefaultGridRowStyle'" );
                }
                else
                {
                    // Odd
                    e.Row.Attributes.Add( "onmouseover", "this.className='DefaultGridRowStyleHover'" );
                    e.Row.Attributes.Add( "onmouseout", "this.className='DefaultGridAlternatingRowStyle'" );
                }
            }
        }
    }

    protected string GetStockText( string sumStock, object isGiftCertificate, object useInventory )
    {
        if (!ConvertUtilities.ToBoolean( useInventory ))
            return "-";
        else
            return sumStock;
    }

    protected string GetPrice( object productID )
    {
        Product product = DataAccessContext.ProductRepository.GetOne( CurrentCulture, (string) productID, SelectedStoreID );

        decimal price;
        if (!product.IsCustomPrice)
            price = product.GetDisplayedPrice( StoreContext.WholesaleStatus, SelectedStoreID );
        else
            price = product.ProductCustomPrice.DefaultPrice;

        return uxCurrencyControl.CurrentCurrency.FormatPrice( price );

    }

    protected string GetRetailPrice( object productID )
    {
        Product product = DataAccessContext.ProductRepository.GetOne( CurrentCulture, (string) productID, SelectedStoreID );
        ProductPrice productPrice = product.GetProductPrice( SelectedStoreID );
        return uxCurrencyControl.CurrentCurrency.FormatPrice( productPrice.RetailPrice );
    }



    protected override void RefreshGrid()
    {
        int totalItems;

        uxGridProduct.DataSource = DataAccessContext.ProductRepository.SearchProduct(
            CurrentCulture,
            uxCategoryFilterDrop.SelectedValue,
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj,
            uxPagingControl.StartIndex,
            uxPagingControl.EndIndex,
            out totalItems,
            SelectedStoreID,
            RootCategoryValue );
        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );

        uxGridProduct.DataBind();
    }


    protected string GetName( object cartItem )
    {
        return ((CartItem) cartItem).GetName( CurrentCulture, uxCurrencyControl.CurrentCurrency );
    }


    protected string GetTooltipText( object cartItem )
    {
        string mainText;
        return ((CartItem) cartItem).GetPreCheckoutTooltip(
            uxCurrencyControl.CurrentCurrency,
            StoreContext.WholesaleStatus,
            "Recurring Billing",
            "This product requires recurring payments, which will be charged from your credit card regularly.",
            "*Price above does not include discount, tax and shipping price.",
            SelectedStoreID,
            out mainText ).Replace( "<br/>", "\n" );
    }

    protected bool IsRecurringTooltipVisible( object cartItem )
    {
        return ((CartItem) cartItem).IsRecurring;
    }

    protected string GetUnitPriceText( object cartItem )
    {
        return uxCurrencyControl.CurrentCurrency.FormatPrice( ((CartItem) cartItem).GetShoppingCartUnitPrice(
            StoreContext.WholesaleStatus ) );
    }

    protected string GetSubtotalText( object cartItem )
    {
        CartItem item = ((CartItem) cartItem);

        return uxCurrencyControl.CurrentCurrency.FormatPrice(
            item.GetShoppingCartUnitPrice( StoreContext.WholesaleStatus )
            * item.Quantity );
    }

    private bool IsOptionProduct( string productID )
    {
        Product product = DataAccessContext.ProductRepository.GetOne( CurrentCulture, productID, StoreContext.CurrentStore.StoreID );
        if (product.ProductOptionGroups.Count > 0)
            return true;
        else
            return false;
    }

    private bool IsDuplicateItems( IList<PromotionProduct> promotionProductList, PromotionProduct newPromotionProduct )
    {
        foreach (PromotionProduct promotionProduct in promotionProductList)
        {
            if (promotionProduct.ProductID == newPromotionProduct.ProductID)
            {
                return true;
            }
        }
        return false;
    }

    private void DisplayErrorAndPopup( string message )
    {
        uxPromotionProductDetails.DisplayError( message );
        uxAddItemButtonModalPopup.Show();
    }

    private void CheckAll_RefreshHandler( object sender, EventArgs e )
    {
        uxAddItemButtonModalPopup.Show();
        if (uxPromotionProductDetails.CheckAllOption)
        {
            uxPromotionProductDetails.ShowOptionPanel( false );
        }
        else
        {
            uxPromotionProductDetails.ShowOptionPanel( true );
        }
    }

    private bool IsProductValid( Product product )
    {
        bool result = true;

        ProductSubscription subscriptionItem = new ProductSubscription( product.ProductID );

        if (subscriptionItem.IsSubscriptionProduct())
        {
            uxMessage.DisplayError( "Cannot Add Subscription Product." );
            result = false;
        }
        else if (product.IsDownloadable)
        {
            uxMessage.DisplayError( "Cannot Add Downloable Product." );
            result = false;
        }
        else if (product.IsRecurring)
        {
            uxMessage.DisplayError( "Cannot Add Recurring Product." );
            result = false;
        }
        else if (product.IsGiftCertificate)
        {
            uxMessage.DisplayError( "Cannot Add Gift Certificate product." );
            result = false;
        }
        else if (product.IsProductKit)
        {
            uxMessage.DisplayError( "Cannot Add Product Kit." );
            result = false;
        }

        return result;
    }

    protected void uxAddItemButton_Click( object sender, EventArgs e )
    {
        string errMeg = String.Empty;
        try
        {
            if (uxPromotionProductDetails.VerifyValidInput( out errMeg ))
            {
                PromotionSubGroup promotionSubGroup = DataAccessContextDeluxe.PromotionSubGroupRepository.GetOne( PromotionSubGroupID );
                PromotionProduct promotionProduct = new PromotionProduct();
                promotionProduct.ProductID = uxPromotionProductDetails.ProductID;
                promotionProduct.OptionItemID = uxPromotionProductDetails.GetSelectedOptions();
                promotionProduct.PromotionSubGroupID = PromotionSubGroupID;
                promotionProduct.Quantity = uxPromotionProductDetails.GetQuantity();
                promotionProduct.SortOrder = ConvertUtilities.ToInt32( PromotionSubGroupID );


                if (!IsDuplicateItems( promotionSubGroup.PromotionProducts, promotionProduct ))
                {
                    promotionSubGroup.PromotionProducts.Add( promotionProduct );
                    DataAccessContextDeluxe.PromotionSubGroupRepository.Save( promotionSubGroup );
                    uxMessage.DisplayMessage( "Add Promotion Product Complete" );

                    OnBubbleEvent( e );
                }
                else
                {
                    uxMessage.DisplayError( "Cannot Add Duplicate Product" );
                }
            }
            else
            {
                DisplayErrorAndPopup( errMeg );
            }
        }
        catch (Exception ex)
        {
            MessageControl.DisplayError( ex.ToString() );
        }
    }

    protected void uxAddItemButton_Command( object sender, CommandEventArgs e )
    {
        string productID = e.CommandArgument.ToString();
        Product product = DataAccessContext.ProductRepository.GetOne(
            CurrentCulture, productID, SelectedStoreID );

        if (IsProductValid( product ))
        {
            uxPromotionProductDetails.StoreID = SelectedStoreID;
            uxPromotionProductDetails.CultureID = CurrentCulture.CultureID;
            uxPromotionProductDetails.ProductID = productID;
            uxPromotionProductDetails.CurrencyCode = uxCurrencyControl.CurrentCurrencyCode;
            uxPromotionProductDetails.PopulateControls();
            uxAddItemPanel.Visible = true;
            uxAddItemButton.Visible = true;
            uxAddItemButtonModalPopup.Show();
        }
    }

    public void UpdateCategoryLanguage()
    {
        uxCategoryFilterDrop.CultureID = CurrentCulture.CultureID;
        RefreshGrid();
    }
}
