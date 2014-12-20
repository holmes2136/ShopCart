using System;
using System.Collections;
using System.Collections.Generic;
using Vevo.Domain.Discounts;
using Vevo.Domain.Orders;
using Vevo.Domain.Products;
using Vevo.Shared.Utilities;
using Vevo.WebUI;

public partial class Components_CouponMessageDisplay : Vevo.WebUI.International.BaseLanguageUserControl
{
    #region Private

    private void HideAllControls()
    {
        uxCouponAmountDiv.Visible = false;
        uxCouponExpireDateDiv.Visible = false;
        uxAvailableItemHeaderListDiv.Visible = false;
        uxAvailableItemListDiv.Visible = false;
        uxErrorMessageDiv.Visible = false;
        uxPromotionWarningDiv.Visible = false;
    }

    private void DisplayError( string errorMessage )
    {
        uxErrorMessage.DisplayErrorNoNewLine( errorMessage );
        uxErrorMessageDiv.Visible = true;
    }

    private string GetCustomerError()
    {
        return "[$ErrorCouponUserName]";
    }

    private string GetProductError( Coupon coupon )
    {
        string message = "<p>[$ErrorInvalidProduct]</p> <p>[$ProductListHeader]</p>";
        message += GetApplicableProductListText( coupon, "all_product" );
        return message;
    }

    private string GetOrderAmountError( Coupon coupon )
    {
        string message = String.Format( "<p>[$ErrorCouponMinimumOrder] {0}.</p>",
            StoreContext.Currency.FormatPrice( coupon.MinimumSubtotal ) );

        if (coupon.ProductCostType == Coupon.ProductCostTypeEnum.CouponEligibleProducts)
            message += "<p>[$ErrorCouponMinimumOrderEligible]</p>";
        return message;
    }

    private string GetCategoryError( Coupon coupon )
    {
        string message = "<p>[$ErrorInvalidCategory]</p> <p>[$CategoryListHeader]</p>";
        message += GetApplicableCategoryListText( coupon );
        return message;
    }

    private string GetExpiredError( Coupon coupon )
    {
        switch (coupon.ExpirationStatus)
        {
            case Coupon.ExpirationStatusEnum.ExpiredByDate:
                return "[$InvalidExpired]";
            case Coupon.ExpirationStatusEnum.ExpiredByQuantity:
                return "[$InvalidOverLimit]";
            default:
                return "[$InvalidCoupon]";
        }
    }

    private string GetInvalidCodeError()
    {
        return "[$InvalidCoupon]";
    }

    private string GetBelowMinimumQuantityError( Coupon coupon )
    {
        string message = "<p>" + GetDiscountTypeBuyXGetYText( coupon, true ) + "</p>";
        message += "<p>[$ShoppingCartProductListHeader]</p>";
        message += "<p>" + GetApplicableProductListText( coupon, "not_match_product" ) + "</p>";

        return message;
    }

    private string GetFormattedError( Coupon coupon, CartItemGroup cartItemGroup )
    {
        switch (coupon.Validate( cartItemGroup, StoreContext.Customer ))
        {
            case Coupon.ValidationStatus.InvalidCustomer:
                return GetCustomerError();

            case Coupon.ValidationStatus.BelowMinimumOrderAmount:
                return GetOrderAmountError( coupon );

            case Coupon.ValidationStatus.InvalidProduct:
                if (coupon.ProductFilter == Coupon.ProductFilterEnum.ByProductIDs)
                    return GetProductError( coupon );
                else
                    return GetCategoryError( coupon );

            case Coupon.ValidationStatus.Expired:
                return GetExpiredError( coupon );

            case Coupon.ValidationStatus.InvalidCode:
                return GetInvalidCodeError();

            case Coupon.ValidationStatus.BelowMinimumQuantity:
                return GetBelowMinimumQuantityError( coupon );

            default:
                return String.Empty;
        }
    }

    private decimal GetCouponDiscount( Coupon coupon )
    {
        IList<decimal> discountLines;

        return coupon.GetDiscount(
            StoreContext.ShoppingCart.GetAllCartItems(),
            StoreContext.Customer,
            out discountLines );
    }

    private string GetApplicableProductListText( Coupon coupon, string productListType )
    {
        string message = String.Empty;

        switch (productListType)
        {
            case "all_product":
                {
                    message = "<ul>";
                    foreach (Product product in coupon.GetApplicableProducts( StoreContext.Culture ))
                    {
                        if (!String.IsNullOrEmpty( product.Name ))
                            message += "<li>" + product.Name + "</li> ";
                    }
                    message += "</ul>";
                }
                break;

            case "match_product":
                {
                    if (GetCouponDiscount( coupon ) > 0)
                    {
                        ICartItem[] cart = StoreContext.ShoppingCart.GetCartItems();
                        int productDiscoutableItemCount = 0;

                        if (coupon.ProductFilter != Coupon.ProductFilterEnum.All)
                        {
                            message = "<ul>";
                            for (int i = 0; i < cart.Length; i++)
                            {
                                if (coupon.IsProductDiscountable( cart[i].Product )
                                    && cart[i].DiscountGroupID == "0"
                                    && cart[i].Quantity > coupon.MinimumQuantity)
                                {
                                    message += "<li>"
                                            + cart[i].GetName( StoreContext.Culture, StoreContext.Currency )
                                            + "</li>";
                                    productDiscoutableItemCount++;
                                }
                            }
                            message += "</ul>";
                            if (productDiscoutableItemCount == cart.Length)
                            {
                                message = string.Empty;
                            }
                        }
                    }
                }
                break;

            case "not_match_product":
                {
                    if (GetCouponDiscount( coupon ) == 0)
                    {
                        ICartItem[] cart = StoreContext.ShoppingCart.GetCartItems();

                        message = "<ul>";
                        for (int i = 0; i < cart.Length; i++)
                        {
                            if (coupon.IsProductDiscountable( cart[i].Product )
                                && cart[i].DiscountGroupID == "0")
                            {
                                message += "<li>"
                                        + cart[i].GetName( StoreContext.Culture, StoreContext.Currency )
                                        + "</li>";
                            }
                        }
                        message += "</ul>";
                    }
                }
                break;
        }
        return message;
    }

    private string GetApplicableCategoryListText( Coupon coupon )
    {
        string message = "<ul>";
        foreach (Category category in coupon.GetApplicableCategories( StoreContext.Culture ))
        {
            if (!String.IsNullOrEmpty( category.Name ))
                message += "<li>" + category.Name + "</li> ";
        }
        message += "</ul>";

        return message;
    }

    private string GetDiscountTypeBuyXGetYText( Coupon coupon, bool isErrorMessage )
    {
        String message = "";
        if (!isErrorMessage)
        {
            message = "Buy " + coupon.MinimumQuantity.ToString() + " item(s) full price and get ";

            if (coupon.DiscountType == Coupon.DiscountTypeEnum.BuyXDiscountYPrice)
            {
                message += StoreContext.Currency.FormatPrice( coupon.DiscountAmount ) + " discount ";
                message += "for " + coupon.PromotionQuantity.ToString() + " item(s). ";
            }
            else //Coupon.DiscountTypeEnum.BuyXDiscountYPercentage
            {
                if (ConvertUtilities.ToInt32( coupon.Percentage ) == 100)
                {
                    message += coupon.PromotionQuantity + " item(s) free. ";
                }
                else // coupon.Percentage != 100
                {
                    message += coupon.Percentage.ToString( "0.00" ) + "% discount ";
                    message += "for " + coupon.PromotionQuantity.ToString() + " item(s). ";
                }
            }
        }
        else
        {
            message += "To use this coupon, you need to buy at least ";
            message += ConvertUtilities.ToString( coupon.MinimumQuantity + coupon.PromotionQuantity ) + " items per product.";
        }

        return message;
    }

    private void DisplayCouponAmount( Coupon coupon )
    {
        uxCouponAmountDiv.Visible = false;

        if (coupon.DiscountType == Coupon.DiscountTypeEnum.Price)
        {
            uxCouponAmountDiv.Visible = true;
            uxCouponAmountLabel.Text = StoreContext.Currency.FormatPrice( coupon.DiscountAmount );
        }
        if (coupon.DiscountType == Coupon.DiscountTypeEnum.Percentage)
        {
            uxCouponAmountDiv.Visible = true;
            uxCouponAmountLabel.Text = coupon.Percentage.ToString() + "%";
        }
        if (coupon.DiscountType == Coupon.DiscountTypeEnum.BuyXDiscountYPrice)
        {
            uxCouponAmountDiv.Visible = true;
            uxCouponAmountLabel.Text = GetDiscountTypeBuyXGetYText( coupon, false );
        }
        if (coupon.DiscountType == Coupon.DiscountTypeEnum.BuyXDiscountYPercentage)
        {
            uxCouponAmountDiv.Visible = true;
            uxCouponAmountLabel.Text = GetDiscountTypeBuyXGetYText( coupon, false );
        }

        if (coupon.DiscountType == Coupon.DiscountTypeEnum.FreeShipping)
        {
            uxCouponAmountDiv.Visible = true;
            uxCouponAmountLabel.Text = "Free Shipping Cost";
        }
    }

    private void DisplayExpirationDetails( Coupon coupon )
    {
        uxCouponExpireDateDiv.Visible = false;

        if (coupon.ExpirationType == Coupon.ExpirationTypeEnum.Date ||
            coupon.ExpirationType == Coupon.ExpirationTypeEnum.Both)
        {
            uxCouponExpireDateDiv.Visible = true;
            uxCouponExpireDateLabel.Text = String.Format( "{0:dd} {0:MMM} {0:yyyy}", coupon.ExpirationDate );
        }
    }

    private void DisplayCouponApplicableItems( Coupon coupon, string productListType )
    {
        string message = String.Empty;
        if (coupon.ProductFilter == Coupon.ProductFilterEnum.ByProductIDs)
        {
            message = GetApplicableProductListText( coupon, productListType );

            if (!String.IsNullOrEmpty( message ))
            {
                if (productListType == "all_product")
                {
                    uxAvailableItemHeaderLabel.Text = "[$ProductListHeader]";
                }
                else
                {
                    uxAvailableItemHeaderLabel.Text = "[$ShoppingCartProductListHeader]";
                }

                uxAvailableItemHeaderListDiv.Visible = true;

                uxAvailableItemLabel.Text = message;
                uxAvailableItemListDiv.Visible = true;
            }
        }
        else if (coupon.ProductFilter == Coupon.ProductFilterEnum.ByCategoryIDs)
        {
            uxAvailableItemHeaderLabel.Text = "[$CategoryListHeader]";
            uxAvailableItemLabel.Text = GetApplicableCategoryListText( coupon );

            uxAvailableItemListDiv.Visible = true;
            uxAvailableItemHeaderListDiv.Visible = true;
        }
    }

    private void DisplayPromotionWarning( Coupon coupon )
    {
        IList<ICartItem> cartItems = StoreContext.ShoppingCart.GetCartItems();
        if (Coupon.DiscountTypeEnum.FreeShipping != coupon.DiscountType)
        {
            foreach (ICartItem cartItem in cartItems)
            {
                if (cartItem.IsPromotion)
                {
                    uxPromotionWarningDiv.Visible = true;
                    break;
                }
            }
        }
    }

    #endregion


    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    #endregion


    #region Public Methods

    public void HideAll()
    {
        HideAllControls();
    }

    public void DisplayCouponDetails( Coupon coupon, string productListType )
    {
        HideAllControls();
        DisplayCouponAmount( coupon );
        DisplayExpirationDetails( coupon );
        DisplayCouponApplicableItems( coupon, productListType );
        DisplayPromotionWarning( coupon );
    }

    public void DisplayCouponErrorMessage( Coupon coupon, CartItemGroup cartItemGroup )
    {
        HideAllControls();

        string errorMessage = GetFormattedError( coupon, cartItemGroup );
        if (!String.IsNullOrEmpty( errorMessage ))
            DisplayError( errorMessage );
    }

    public string GetCouponErrorMessage( Coupon coupon, CartItemGroup cartItemGroup )
    {
        HideAllControls();

        return GetFormattedError( coupon, cartItemGroup );
    }

    #endregion

}
