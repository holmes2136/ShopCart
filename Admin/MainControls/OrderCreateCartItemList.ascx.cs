using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.DataInterfaces;
using Vevo.Domain.Orders;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.Shared.Utilities;
using Vevo.WebUI;
using Vevo.Base.Domain;
using Vevo.Deluxe.Domain.Products;
using Vevo.Deluxe.Domain.Users;
using Vevo.Domain.Users;
using Vevo.Deluxe.Domain;

public partial class Admin_MainControls_OrderCreateCartItemList : AdminAdvancedBaseListControl
{
    #region Private
    private const int ProductIDColumnIndex = 1;
    private const int StockColumnIndex = 5;
    private const int RetailColumnIndex = 6;
    private const int ReviewColumnIndex = 9;

    private string SelectedCustomerID
    {
        get
        {
            if (MainContext.QueryString["CustomerID"] == null)
                return "0";
            else
                return MainContext.QueryString["CustomerID"];
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
            //    DeleteVisible( true );
            uxPagingControl.Visible = true;
        }
        else
        {
            //  DeleteVisible( false );
            uxPagingControl.Visible = false;
        }

        if (!IsAdminModifiable())
        {
            //   uxAddButton.Visible = false;
            //  DeleteVisible( false );
        }
    }


    private bool IsAnyOptionOutOfStock( string productID )
    {
        Product product = DataAccessContext.ProductRepository.GetOne(
            uxLanguageControl.CurrentCulture, productID, new StoreRetriever().GetCurrentStoreID() );
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
            uxCategoryFilterDrop.CultureID = uxLanguageControl.CurrentCultureID;
            uxCurrencyControl.StoreID = SelectedStoreID;
        }

        RegisterGridView( uxGridProduct, "ProductID" );

        RegisterLanguageControl( uxLanguageControl );
        RegisterSearchFilter( uxSearchFilter );
        RegisterPagingControl( uxPagingControl );
        RegisterCategoryFilterDrop( uxCategoryFilterDrop );
        RegisterCurrencyFilterDrop( uxCurrencyControl );

        uxLanguageControl.BubbleEvent += new EventHandler( uxLanguageControl_BubbleEvent );
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
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            uxCurrencyControl.StoreID = SelectedStoreID;

            uxCategoryFilterDrop.RefreshCategoryDropList(
                DataAccessContext.Configurations.GetValue( "RootCategory",
                DataAccessContext.StoreRepository.GetOne( SelectedStoreID ) ) );
            StoreContext.ClearCheckoutSession();
        }

        uxTaxIncludeMsgPanel.Visible = TaxIncludeVisibility();
        uxTaxIncludeMsgLabel.ToolTip = GetTaxTooltipText();

        StoreContext.Customer = DataAccessContext.CustomerRepository.GetOne( SelectedCustomerID );
        uxCurrencyControl.StoreID = SelectedStoreID;
        PopulateControls();
        PopulateShoppingCartControls();
    }

    protected void uxGridProduct_DataBound( object sender, EventArgs e )
    {
        GridView grid = (GridView) sender;

        if (!CatalogUtilities.IsRetailMode)
        {
            grid.Columns[RetailColumnIndex].Visible = false;
        }

        if (!DataAccessContext.Configurations.GetBoolValue( "UseStockControl" ))
            grid.Columns[StockColumnIndex].Visible = false;
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
        Product product = DataAccessContext.ProductRepository.GetOne( uxLanguageControl.CurrentCulture, (string) productID, SelectedStoreID );

        decimal price;
        if (!product.IsCustomPrice)
            price = product.GetDisplayedPrice( StoreContext.WholesaleStatus, SelectedStoreID );
        else
            price = product.ProductCustomPrice.DefaultPrice;

        return uxCurrencyControl.CurrentCurrency.FormatPrice( price );

    }

    protected string GetRetailPrice( object productID )
    {
        Product product = DataAccessContext.ProductRepository.GetOne( uxLanguageControl.CurrentCulture, (string) productID, SelectedStoreID );
        ProductPrice productPrice = product.GetProductPrice( SelectedStoreID );
        return uxCurrencyControl.CurrentCurrency.FormatPrice( productPrice.RetailPrice );
    }



    protected override void RefreshGrid()
    {
        int totalItems;

        uxGridProduct.DataSource = DataAccessContext.ProductRepository.SearchProduct(
            uxLanguageControl.CurrentCulture,
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
        return ((CartItem) cartItem).GetName( uxLanguageControl.CurrentCulture, uxCurrencyControl.CurrentCurrency );
    }


    protected string GetTooltipText( object cartItem )
    {
        string mainText;
        return ((ICartItem) cartItem).GetPreCheckoutTooltip(
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
        ICartItem item = ((ICartItem) cartItem);

        return uxCurrencyControl.CurrentCurrency.FormatPrice(
            item.GetShoppingCartUnitPrice( StoreContext.WholesaleStatus )
            * item.Quantity );
    }


    protected void uxUpdateItemButton_Click( object sender, EventArgs e )
    {
        string errMeg = String.Empty;

        ProductSubscription subscription = new ProductSubscription( uxProductItemDetails.ProductID );

        if (uxProductItemDetails.UpdateCartItem( subscription.IsSubscriptionProduct(), out errMeg ))
        {
            PopulateShoppingCartControls();
            uxMessage.DisplayMessage( "The shopping cart was updated successfully." );

        }
        else
        {
            uxAddItemButtonModalPopup.Show();
        }
    }

    protected void uxAddItemButton_Click( object sender, EventArgs e )
    {
        string errMeg = String.Empty;

        ProductSubscription subscription = new ProductSubscription( uxProductItemDetails.ProductID );

        if (uxProductItemDetails.AddItemToShoppingCart( subscription.IsSubscriptionProduct(), out errMeg ))
        {
            PopulateShoppingCartControls();
            Product product = DataAccessContext.ProductRepository.GetOne(
                uxLanguageControl.CurrentCulture, uxProductItemDetails.ProductID, SelectedStoreID );
            uxMessage.DisplayMessage( "The {0} was added to the shopping cart successfully.", product.Name );

        }
        else
        {
            uxAddItemButtonModalPopup.Show();
        }
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        try
        {
            bool deleted = false;
            foreach (GridViewRow row in uxCartItemGrid.Rows)
            {
                CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
                if (deleteCheck.Checked)
                {
                    HiddenField hd = (HiddenField) row.FindControl( "uxHidden" );
                    string id = hd.Value;
                    StoreContext.ShoppingCart.DeleteItem( id );
                    deleted = true;
                }
            }

            if (deleted)
                uxMessage.DisplayMessage( Resources.OrdersMessages.DeleteSuccess );
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

    }

    protected void uxAddItemButton_Command( object sender, CommandEventArgs e )
    {
        string productID = e.CommandArgument.ToString();
        Product product = DataAccessContext.ProductRepository.GetOne(
            uxLanguageControl.CurrentCulture, productID, SelectedStoreID );

        ProductSubscription subscriptionItem = new ProductSubscription( product.ProductID );

        if (subscriptionItem.IsSubscriptionProduct())
        {
            if (CustomerSubscription.IsContainsProductSubscriptionHigherLevel(
                subscriptionItem.ProductSubscriptions[0].SubscriptionLevelID,
                DataAccessContextDeluxe.CustomerSubscriptionRepository.GetCustomerSubscriptionsByCustomerID( SelectedCustomerID ) ))
            {
                uxMessage.DisplayError(
                        "Cannot add product to the cart. This customer already has higer product subscription level." );
                return;
            }
        }

        if (StoreContext.ShoppingCart.CheckCanAddItemToCart( product ))
        {

            uxProductItemDetails.StoreID = SelectedStoreID;
            uxProductItemDetails.CultureID = uxLanguageControl.CurrentCulture.CultureID;
            uxProductItemDetails.ProductID = productID;
            uxProductItemDetails.CurrencyCode = uxCurrencyControl.CurrentCurrencyCode;
            uxProductItemDetails.PopulateControls();
            uxAddItemPanel.Visible = true;
            uxUpdateItemButton.Visible = false;
            uxAddItemButton.Visible = true;
            uxAddItemButtonModalPopup.Show();

            if (uxProductItemDetails.IsProductKit( productID ))
            {
                uxAddItemButton.Visible = false;
                uxUpdateItemButton.Visible = false;
            }
        }
        else
        {
            if (product.FreeShippingCost)
                uxMessage.DisplayError(
                    "Cannot add product to the cart. The shopping cart already has one or more product that not free shipping." );
            else
                uxMessage.DisplayError(
                    "Cannot add product to the cart. The shopping cart already has one or more product that is a free shipping product." );
        }

    }


    protected void uxEditItemButton_Command( object sender, CommandEventArgs e )
    {
        string cartItemID = e.CommandArgument.ToString();

        uxProductItemDetails.SetEditMode();
        uxProductItemDetails.StoreID = SelectedStoreID;
        uxProductItemDetails.CultureID = uxLanguageControl.CurrentCulture.CultureID;
        uxProductItemDetails.CurrencyCode = uxCurrencyControl.CurrentCurrencyCode;
        uxProductItemDetails.CartItemID = cartItemID;
        uxProductItemDetails.PopulateControls();
        uxAddItemPanel.Visible = true;
        uxUpdateItemButton.Visible = true;
        uxAddItemButton.Visible = false;
        uxAddItemButtonModalPopup.Show();
    }

    private decimal GetShoppingCartTotal()
    {
        OrderCalculator orderCalculator = new OrderCalculator();
        return orderCalculator.GetShoppingCartTotal(
            StoreContext.Customer,
            StoreContext.ShoppingCart.SeparateCartItemGroups(),
            StoreContext.CheckoutDetails.Coupon );
    }

    private void PopulateShoppingCartControls()
    {
        if (StoreContext.ShoppingCart.GetCartItems().Length > 0)
        {
            uxShoppingCartPanel.Visible = true;
            uxTotalAmountLabel.Text = uxCurrencyControl.CurrentCurrency.FormatPrice( GetShoppingCartTotal() );

            OrderCalculator orderCalculator = new OrderCalculator();
            decimal discount = orderCalculator.GetPreCheckoutDiscount(
                StoreContext.Customer,
                StoreContext.ShoppingCart.SeparateCartItemGroups(),
                StoreContext.CheckoutDetails.Coupon );

            if (discount > 0)
            {
                uxDiscountTR.Visible = true;
                uxDiscountAmountLabel.Text = uxCurrencyControl.CurrentCurrency.FormatPrice( discount * -1 );
            }
            else
                uxDiscountTR.Visible = false;

            uxCartItemGrid.DataSource = StoreContext.ShoppingCart.GetCartItems();
            uxCartItemGrid.DataBind();
        }
        else
        {
            uxShoppingCartPanel.Visible = false;
            StoreContext.CheckoutDetails.Clear();
        }
    }

    protected string GetTaxTooltipText()
    {
        return String.Format( "The price displayed here has included {0} % tax. \nDepending on your shipping destination, the final tax might be different.",
                DataAccessContext.Configurations.GetValue( "TaxPercentageIncludedInPrice" ).ToString() );
    }

    protected void uxCartItemGrid_RowDataBound( object sender, GridViewRowEventArgs e )
    {
        TextBox quantity = (TextBox) e.Row.FindControl( "uxQuantityText" );
    }

    protected void uxCartItemGrid_RowDeleting( object sender, GridViewDeleteEventArgs e )
    {
        StoreContext.ShoppingCart.DeleteItem(
           uxCartItemGrid.DataKeys[e.RowIndex]["CartItemID"].ToString() );

        uxCartStatusHidden.Value = "Deleted";

        PopulateControls();
    }

    protected void uxNextButton_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "OrderCreateCheckOutDetails.ascx",
            String.Format( "StoreID={0}&CurrencyCode={1}", SelectedStoreID, uxCurrencyControl.CurrentCurrencyCode ) );
    }

    public void ShowModalPopup()
    {
        uxAddItemButtonModalPopup.Show();
    }

}
