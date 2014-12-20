using System;
using System.Collections;
using System.Collections.Generic;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Discounts;
using Vevo.Domain.Orders;
using Vevo.Domain.Payments;
using Vevo.Domain.Products;
using Vevo.Shared.Utilities;
using Vevo.WebUI;
using Vevo.Deluxe.Domain;
using Vevo.Shared.DataAccess;

public partial class Admin_Components_Order_CouponAndGiftDatails : AdminAdvancedBaseUserControl
{

    private decimal GetCouponDiscount( Coupon coupon )
    {
        IList<decimal> discountLines;

        return coupon.GetDiscount(
            StoreContext.ShoppingCart.GetAllCartItems(),
            StoreContext.Customer,
            out discountLines );
    }

    private bool VerifyUsernameCoupon( Coupon coupon )
    {
        string[] usernames = coupon.CustomerUserName.Trim().Split( new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries );

        string currentUsername = StoreContext.Customer.UserName;
        foreach (string username in usernames)
        {
            if (currentUsername == username)
            {
                return true;
            }
        }
        return false;
    }

    private bool ValidateCoupon()
    {
        if (uxCouponIDText.Text == "")
            return true;

        Coupon coupon = DataAccessContext.CouponRepository.GetOne( uxCouponIDText.Text );
        CartItemGroup cartItemGroup = StoreContext.ShoppingCart.GetAllCartItems();

        return coupon.Validate( cartItemGroup, StoreContext.Customer ) == Coupon.ValidationStatus.OK;
    }

    private void DisplayCouponError()
    {
        Coupon coupon = DataAccessContext.CouponRepository.GetOne( uxCouponIDText.Text );
        uxCouponMessagePanel.Visible = false;
        DisplayCouponErrorMessage( coupon, StoreContext.ShoppingCart.GetAllCartItems() );
    }

    private void DisplayCouponErrorMessage( Coupon coupon, CartItemGroup cartItemGroup )
    {
        HideAllControls();

        string errorMessage = GetFormattedError( coupon, cartItemGroup );
        if (!String.IsNullOrEmpty( errorMessage ))
            DisplayError( errorMessage );
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

    private string GetCustomerError()
    {
        return "This coupon is not eligible for this user";
    }

    private string GetProductError( Coupon coupon )
    {
        string message = "This coupon cannot apply to any product in your shopping cart <p>This coupon is eligible for the following products:</p>";
        message += GetApplicableProductListText( coupon, "all_product" );
        return message;
    }

    private string GetOrderAmountError( Coupon coupon )
    {
        string message = String.Format( "To use this coupon, The total amount of coupon-eligibe products must be at least {0}.",
            StoreContext.Currency.FormatPrice( coupon.MinimumSubtotal ) );

        if (coupon.ProductCostType == Coupon.ProductCostTypeEnum.CouponEligibleProducts)
            message += "<p>Only coupon-eligible products count toward this amount.</p>";
        return message;
    }

    private string GetCategoryError( Coupon coupon )
    {
        string message = "This coupon cannot apply to any product in your shopping cart <p>This coupon is eligible for the following categories:</p>";
        message += GetApplicableCategoryListText( coupon );
        return message;
    }

    private string GetExpiredError( Coupon coupon )
    {
        switch (coupon.ExpirationStatus)
        {
            case Coupon.ExpirationStatusEnum.ExpiredByDate:
                return "This coupon has been expired.";
            case Coupon.ExpirationStatusEnum.ExpiredByQuantity:
                return "This coupon is over the limit";
            default:
                return "Invalid coupon code.";
        }
    }

    private string GetInvalidCodeError()
    {
        return "Invalid coupon code.";
    }

    private string GetBelowMinimumQuantityError( Coupon coupon )
    {
        string message = GetDiscountTypeBuyXGetYText( coupon, true );
        message += "<p>This coupon is eligible for the following products in your shopping cart:</p>";
        message += "<p>" + GetApplicableProductListText( coupon, "not_match_product" ) + "</p>";

        return message;
    }

    private void DisplayError( string errorMessage )
    {
        uxCouponErrorMessage.Text = errorMessage;
        uxCouponErrorMessageDiv.Visible = true;
    }

    private void DisplayNoCouponCodeError()
    {
        //uxCouponMessageDisplay.HideAll();
        HideAllControls();
        uxCouponMessagePanel.Visible = true;
        uxCouponMessage.DisplayErrorNoNewLine( "Please enter Coupon Number" );
    }

    private bool ValidateGiftCertificate()
    {
        if (uxGiftCertificateCodeText.Text == "")
            return true;

        GiftCertificate giftCertificate = DataAccessContext.GiftCertificateRepository.GetOne( uxGiftCertificateCodeText.Text );

        string errorMessage;
        if (!giftCertificate.Verify( out errorMessage ))
        {
            uxGiftDetailsPanel.Visible = true;
            uxGiftRemainValueTR.Visible = false;
            uxGiftExpireDateTR.Visible = false;
            uxGiftErrorMessageDiv.Visible = true;
            uxGiftErrorMessage.Text = errorMessage;
            return false;
        }

        return true;
    }

    private void DisplayGiftDetail()
    {
        GiftCertificate giftCertificate = DataAccessContext.GiftCertificateRepository.GetOne( uxGiftCertificateCodeText.Text.Trim() );
        uxGiftDetailsPanel.Visible = true;
        uxGiftRemainValueTR.Visible = true;
        uxGiftExpireDateTR.Visible = true;
        uxGiftRemainValueLabel.Text = StoreContext.Currency.FormatPrice( giftCertificate.RemainValue );
        if (giftCertificate.IsExpirable)
            uxGiftExpireDateLabel.Text = string.Format( "{0:dd} {0:MMM} {0:yyyy}", giftCertificate.ExpireDate );
        else
            uxGiftExpireDateLabel.Text = "No Expiration.";
    }


    private void DisplayCouponDetails()
    {
        Coupon coupon = DataAccessContext.CouponRepository.GetOne( uxCouponIDText.Text );
        uxCouponMessagePanel.Visible = false;
        //uxCouponMessageDisplay.DisplayCouponDetails( coupon, "match_product" );
        DisplayCouponDetails( coupon, "match_product" );

    }

    private void HideAllControls()
    {
        uxCouponAmountDiv.Visible = false;
        uxCouponExpireDateDiv.Visible = false;
        uxAvailableItemHeaderListDiv.Visible = false;
        uxAvailableItemListDiv.Visible = false;
        uxGiftErrorMessageDiv.Visible = false;
        uxCouponErrorMessageDiv.Visible = false;
    }

    private void DisplayCouponDetails( Coupon coupon, string productListType )
    {
        HideAllControls();
        DisplayCouponAmount( coupon );
        DisplayExpirationDetails( coupon );
        DisplayCouponApplicableItems( coupon, productListType );
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
                    uxAvailableItemHeaderLabel.Text = "This coupon is eligible for the following products:";
                }
                else
                {
                    uxAvailableItemHeaderLabel.Text = "This coupon is eligible for the following products in your shopping cart:";
                }

                uxAvailableItemHeaderListDiv.Visible = true;

                uxAvailableItemLabel.Text = message;
                uxAvailableItemListDiv.Visible = true;
            }
        }
        else if (coupon.ProductFilter == Coupon.ProductFilterEnum.ByCategoryIDs)
        {
            uxAvailableItemHeaderLabel.Text = "This coupon is eligible for the following categories:";
            uxAvailableItemLabel.Text = GetApplicableCategoryListText( coupon );

            uxAvailableItemListDiv.Visible = true;
            uxAvailableItemHeaderListDiv.Visible = true;
        }
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

    private bool IsNumeric( string text )
    {
        decimal value;
        if (decimal.TryParse( text, out value ))
        {
            return true;
        }
        else
            return false;
    }

    private bool IsInteger( string text )
    {
        int value;
        if (int.TryParse( text, out value ))
        {
            return true;
        }
        else
            return false;
    }

    private decimal GetRewardPoint()
    {
        return DataAccessContextDeluxe.CustomerRewardPointRepository.SumCustomerIDAndStoreID( StoreContext.Customer.CustomerID, StoreContext.CurrentStore.StoreID );
    }

    public decimal GetPriceFromPoint( decimal point )
    {
        return point * ConvertUtilities.ToDecimal( DataAccessContext.Configurations.GetValue( "PointValue", StoreContext.CurrentStore ) );
    }

    private string GetRewardPointAndPointValue()
    {
        int rewardPoint = ConvertUtilities.ToInt32( GetRewardPoint() );
        decimal pointValue = GetPriceFromPoint( rewardPoint );
        return rewardPoint.ToString() + " points ( " + StoreContext.Currency.FormatPrice( pointValue ) + " ) ";
    }

    private string GetRewardPointText()
    {
        string text1 = "Your customer has ";
        string text2 = "to use with this purchase";
        return text1 + GetRewardPointAndPointValue() + text2;
    }

    private void DisplayRewardPointDetails()
    {
        uxRewardPointPanel.Visible = true;
        uxRewardPointMessage.Text = uxRewardPointText.Text + " point ( " + StoreContext.Currency.FormatPrice( GetPriceFromPoint( ConvertUtilities.ToDecimal( uxRewardPointText.Text ) ) ) + " ) ";
    }

    private void DisplayRewardPointError( string errorText )
    {
        uxRewardPointMessage.Text = errorText;
        uxRewardPointPanel.Visible = true;
    }

    private bool IsZeroOrLessPoint()
    {
        decimal rewardPoint = ConvertUtilities.ToDecimal( uxRewardPointText.Text );

        if (rewardPoint > 0 && rewardPoint > GetRewardPoint())
        {
            uxRewardPointText.Text = ConvertUtilities.ToInt32( GetRewardPoint() ).ToString();

            return false;
        }

        if (rewardPoint > 0 && rewardPoint <= GetRewardPoint())
        {
            return false;
        }

        return true;
    }

    private bool ValidateRewardPoint()
    {
        if (!IsNumeric( uxRewardPointText.Text ))
        {
            DisplayRewardPointError( "Invalid Input" );
            return false;
        }
        if (!IsInteger( uxRewardPointText.Text ))
        {
            DisplayRewardPointError( "Invalid Input" );
            return false;
        }

        if (ConvertUtilities.ToInt32( uxRewardPointText.Text ) > 0 &&
                    (GetRewardPoint() == 0 || GetRewardPoint() < 0))
        {
            DisplayRewardPointError( "You do not have enough Reward Point" );
            return false;
        }

        if (IsZeroOrLessPoint())
        {
            DisplayRewardPointError( "Reward Point cannot less than or equal to zero(0)" );
            return false;
        }

        DisplayRewardPointDetails();
        return true;
    }

    private bool VerifyRewardPoint( bool isVerify )
    {
        if (isVerify)
        {
            if (uxRewardPointText.Text != "")
            {
                return ValidateRewardPoint();
            }
            else
            {
                DisplayRewardPointError( "Please enter Reward Point" );
                return false;
            }
        }
        else
        {
            if (uxRewardPointText.Text == "")
            {
                return true;
            }
            else
            {
                return ValidateRewardPoint();
            }
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (DataAccessContext.Configurations.GetBoolValue( "PointSystemEnabled", StoreContext.CurrentStore ) && KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName ))
        {
            uxRewardPointDiv.Visible = true;
            uxRewardPointDescriptionLabel.Text = GetRewardPointText();
        }
        else
        {
            uxRewardPointDiv.Visible = false;
        }
    }

    protected void uxClearCouponButton_Click( object sender, EventArgs e )
    {
        HideAllControls();
        uxCouponIDText.Text = String.Empty;
        uxClearCouponButton.Visible = false;
    }

    protected void uxVerifyCouponButton_Click( object sender, EventArgs e )
    {
        if (uxCouponIDText.Text != "")
        {
            if (ValidateCoupon())
            {
                DisplayCouponDetails();
                uxClearCouponButton.Visible = true;
            }
            else
            {
                DisplayCouponError();
            }
        }
        else
        {
            DisplayNoCouponCodeError();
        }
    }

    protected void uxClearRewardPointButton_Click( object sender, EventArgs e )
    {
        uxRewardPointText.Text = String.Empty;
        uxRewardPointPanel.Visible = false;
        uxClearRewardPointButton.Visible = false;
    }

    protected void uxVerifyRewardPointButton_Click( object sender, EventArgs e )
    {
        bool isPointVerify = VerifyRewardPoint( true );
        if (isPointVerify)
        {
            uxClearRewardPointButton.Visible = true;
        }
    }

    protected void uxClearGiftButton_Click( object sender, EventArgs e )
    {
        uxGiftDetailsPanel.Visible = false;
        uxGiftRemainValueTR.Visible = false;
        uxGiftExpireDateTR.Visible = false;
        uxGiftErrorMessageDiv.Visible = false;
        uxGiftRemainValueLabel.Text = "";
        uxGiftExpireDateLabel.Text = "";
        uxGiftCertificateCodeText.Text = String.Empty;
        uxClearGiftButton.Visible = false;
    }

    protected void uxVerifyGiftButton_Click( object sender, EventArgs e )
    {
        if (uxGiftCertificateCodeText.Text != "")
        {
            if (ValidateGiftCertificate())
            {
                DisplayGiftDetail();
                uxClearGiftButton.Visible = true;
            }
        }
        else
        {
            uxGiftDetailsPanel.Visible = true;
            uxGiftRemainValueTR.Visible = false;
            uxGiftExpireDateTR.Visible = false;
            uxGiftRemainValueLabel.Text = "";
            uxGiftExpireDateLabel.Text = "";
            uxGiftErrorMessageDiv.Visible = true;
            uxGiftErrorMessage.Text = "Please enter Gift Certificate Number";
        }
    }

    public string GetCouponCode( out bool isValid )
    {
        //errMsg = String.Empty;
        if (ValidateCoupon())
        {
            isValid = true;
            return uxCouponIDText.Text;
        }
        else
        {
            DisplayCouponError();
            isValid = false;
            return String.Empty;
        }
    }

    public string GetGiftCertificateCode( out bool isValid )
    {
        if (ValidateGiftCertificate())
        {
            isValid = true;
            return uxGiftCertificateCodeText.Text;
        }

        isValid = false;
        return String.Empty;
    }

    public string GetRewardPointValue( out bool isValid )
    {
        if (VerifyRewardPoint( false ))
        {
            isValid = true;
            return uxRewardPointText.Text;
        }

        isValid = false;
        return String.Empty;
    }
}
