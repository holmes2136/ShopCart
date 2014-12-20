using System;
using System.Web.Security;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Discounts;
using Vevo.Domain.Orders;
using Vevo.Domain.Payments;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.WebUI.International;
using Vevo.Deluxe.Domain;
using Vevo.Shared.DataAccess;

public partial class Components_GiftCouponDetail : BaseLanguageUserControl
{
    private void TiesTextBoxesWithButtons()
    {
        WebUtilities.TieButton( Page, uxGiftCertificateCodeText, uxVerifyGiftButton );
        WebUtilities.TieButton( Page, uxCouponIDText, uxVerifyCouponButton );
        WebUtilities.TieButton( Page, uxRewardPointText, uxVeryfyRewardPointButton );
    }

    private bool VerifyUsernameCoupon( Coupon coupon )
    {
        string[] usernames = coupon.CustomerUserName.Trim().Split( new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries );

        string currentUsername = Membership.GetUser().UserName.Trim();
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
        if (Int32.TryParse( text, out value ))
        {
            return true;
        }
        else
            return false;
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

    private void DisplayCouponError()
    {
        Coupon coupon = DataAccessContext.CouponRepository.GetOne( uxCouponIDText.Text );
        //uxCouponMessageDiv.Visible = true;
        //uxCouponMessageDisplay.DisplayCouponErrorMessage( coupon, StoreContext.ShoppingCart.GetAllCartItems() );

        uxCouponErrorMessageDiv.Visible = true;
        uxCouponMessage.Text = uxCouponMessageDisplay.GetCouponErrorMessage( coupon, StoreContext.ShoppingCart.GetAllCartItems() );
    }

    private void DisplayNoCouponCodeError()
    {
        uxCouponMessageDisplay.HideAll();
        uxCouponErrorMessageDiv.Visible = true;
        uxCouponMessage.Text = "[$EnterCouponNumber]";
    }

    private void DisplayRewardPointError( string errorText )
    {
        uxRewardPointMessage.Text = errorText;
        uxRewardPointMessageTR.Visible = true;
    }

    private bool ValidateGiftCertificate()
    {
        if (uxGiftCertificateCodeText.Text != "")
        {
            GiftCertificate giftCertificate = DataAccessContext.GiftCertificateRepository.GetOne( uxGiftCertificateCodeText.Text );

            string errorMessage;
            if (!giftCertificate.Verify( out errorMessage ))
            {
                uxGiftMessageTR.Visible = true;
                uxGiftRemainValueTR.Visible = false;
                uxGiftExpireDateTR.Visible = false;
                uxMessage.Text = errorMessage;
                return false;
            }
        }

        return true;
    }

    private void DisplayGiftDetail()
    {
        GiftCertificate giftCertificate = DataAccessContext.GiftCertificateRepository.GetOne( uxGiftCertificateCodeText.Text.Trim() );
        uxGiftMessageTR.Visible = false;
        uxGiftRemainValueTR.Visible = true;
        uxGiftExpireDateTR.Visible = true;
        uxGiftRemainValueLabel.Text = StoreContext.Currency.FormatPrice( giftCertificate.RemainValue );
        if (giftCertificate.IsExpirable)
            uxGiftExpireDateLabel.Text = string.Format( "{0:dd} {0:MMM} {0:yyyy}", giftCertificate.ExpireDate );
        else
            uxGiftExpireDateLabel.Text = "[$NoExpiration]";
    }


    private void DisplayCouponDetails()
    {
        Coupon coupon = DataAccessContext.CouponRepository.GetOne( uxCouponIDText.Text );
        uxCouponMessageDiv.Visible = true;
        uxCouponMessageDisplay.DisplayCouponDetails( coupon, "match_product" );
    }

    private void DisplayRewardPointDetails()
    {
        uxRewardPointMessageTR.Visible = false;
        uxRewardPointLabel.Text = "You are using " + uxRewardPointText.Text + " point ( " + StoreContext.Currency.FormatPrice( GetPriceFromPoint( ConvertUtilities.ToDecimal( uxRewardPointText.Text ) ) ) + " ) ";
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxGiftMessageTR.Visible = false;
        uxCouponErrorMessageDiv.Visible = false;

        TiesTextBoxesWithButtons();

        uxGiftCertificateTable.Visible = DataAccessContext.Configurations.GetBoolValue( "GiftCertificateEnabled" );
        uxCouponDiv.Visible = DataAccessContext.Configurations.GetBoolValue( "CouponEnabled" );

        if (DataAccessContext.Configurations.GetBoolValue( "PointSystemEnabled", StoreContext.CurrentStore ) &&
            Membership.GetUser() != null && KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName ))
        {
            uxRewardPointDiv.Visible = true;
        }
        else
        {
            uxRewardPointDiv.Visible = false;
        }
    }

    private void DisplayClearButton()
    {
        if (!String.IsNullOrEmpty( uxGiftCertificateCodeText.Text ))
        {
            uxClearGiftImageButton.Visible = true;
        }
        else
        {
            uxClearGiftImageButton.Visible = false;
        }

        if (!String.IsNullOrEmpty( uxCouponIDText.Text ))
        {
            uxClearCouponImageButton.Visible = true;
        }
        else
        {
            uxClearCouponImageButton.Visible = false;
        }

        if (!String.IsNullOrEmpty( uxRewardPointText.Text ))
        {
            uxClearRewardPointImageButton.Visible = true;
        }
        else
        {
            uxClearRewardPointImageButton.Visible = false;
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        uxGiftCertificateCodeText.Text = StoreContext.CheckoutDetails.GiftCertificate.GiftCertificateCode;
        uxCouponIDText.Text = StoreContext.CheckoutDetails.Coupon.CouponID;
        if (StoreContext.CheckoutDetails.RedeemPoint > 0)
            uxRewardPointText.Text = StoreContext.CheckoutDetails.RedeemPoint.ToString();
        else
            uxRewardPointText.Text = String.Empty;

        DisplayClearButton();
    }

    protected void uxClearGiftImageButton_Click( object sender, EventArgs e )
    {
        StoreContext.CheckoutDetails.SetGiftCertificate( String.Empty );
        uxGiftMessageTR.Visible = false;
        uxGiftRemainValueTR.Visible = false;
        uxGiftExpireDateTR.Visible = false;
    }

    protected void uxVerifyGiftButton_Click( object sender, EventArgs e )
    {
        if (uxGiftCertificateCodeText.Text != "")
        {
            if (ValidateGiftCertificate())
            {
                StoreContext.CheckoutDetails.StoreID = StoreContext.CurrentStore.StoreID;
                StoreContext.CheckoutDetails.SetGiftCertificate( uxGiftCertificateCodeText.Text );
                DisplayGiftDetail();
            }
        }
        else
        {
            uxGiftMessageTR.Visible = true;
            uxGiftRemainValueTR.Visible = false;
            uxGiftExpireDateTR.Visible = false;
            uxGiftRemainValueLabel.Text = "";
            uxGiftExpireDateLabel.Text = "";
            uxMessage.Text = "[$EnterGiftCertificateNumber]";
        }
    }

    protected void uxClearCouponImageButton_Click( object sender, EventArgs e )
    {
        StoreContext.CheckoutDetails.SetCouponID( String.Empty );
        uxCouponErrorMessageDiv.Visible = false;
        uxCouponMessageDiv.Visible = false;
    }

    protected void uxVeryfyCouponButton_Click( object sender, EventArgs e )
    {
        if (uxCouponIDText.Text != "")
        {
            if (ValidateCoupon())
            {
                StoreContext.CheckoutDetails.StoreID = StoreContext.CurrentStore.StoreID;
                StoreContext.CheckoutDetails.SetCouponID( uxCouponIDText.Text );
                DisplayCouponDetails();
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

    private bool ValidateRewardPoint()
    {
        if (!IsNumeric( uxRewardPointText.Text ))
        {
            DisplayRewardPointError( "[$InvalidInput]" );
            return false;
        }
        if (!IsInteger( uxRewardPointText.Text ))
        {
            DisplayRewardPointError( "[$InvalidInput]" );
            return false;
        }

        if (ConvertUtilities.ToInt32( uxRewardPointText.Text ) > 0 &&
                    (GetRewardPoint() == 0 || GetRewardPoint() < 0))
        {
            DisplayRewardPointError( "[$NotEnoughPoint]" );
            return false;
        }

        if (IsZeroOrLessPoint())
        {
            DisplayRewardPointError( "[$EnterLessOrZeroPoint]" );
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
                DisplayRewardPointError( "[$EnterRewardPoint]" );
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

    protected void uxClearRewardPointImageButton_Click( object sender, EventArgs e )
    {
        StoreContext.CheckoutDetails.RedeemPoint = 0;
        StoreContext.CheckoutDetails.RedeemPrice = 0;
        uxRewardPointMessageTR.Visible = false;
        uxRewardPointLabel.Text = String.Empty;
    }

    protected void uxVeryfyRewardPointButton_Click( object sender, EventArgs e )
    {
        if (VerifyRewardPoint( true ))
        {
            StoreContext.CheckoutDetails.StoreID = StoreContext.CurrentStore.StoreID;
            StoreContext.CheckoutDetails.RedeemPrice = GetPriceFromPoint( ConvertUtilities.ToDecimal( uxRewardPointText.Text ) );
            StoreContext.CheckoutDetails.RedeemPoint = ConvertUtilities.ToInt32( uxRewardPointText.Text );
        }
    }

    private decimal GetPriceFromPoint( decimal point )
    {
        return point * ConvertUtilities.ToDecimal( DataAccessContext.Configurations.GetValue( "PointValue", StoreContext.CurrentStore ) );
    }

    private decimal GetRewardPoint()
    {
        string customerID = DataAccessContext.CustomerRepository.GetIDFromUserName( Membership.GetUser().UserName );
        return DataAccessContextDeluxe.CustomerRewardPointRepository.SumCustomerIDAndStoreID( customerID, StoreContext.CurrentStore.StoreID );
    }

    private string GetRewardPointAndPointValue()
    {
        int rewardPoint = ConvertUtilities.ToInt32( GetRewardPoint() );
        decimal pointValue = GetPriceFromPoint( rewardPoint );
        if (pointValue < 0)
        {
            pointValue = 0;
        }
        return rewardPoint.ToString() + " points ( " + StoreContext.Currency.FormatPrice( pointValue ) + " ) ";
    }

    private string GetRewardPointText()
    {
        string text1 = "You have ";
        string text2 = "to use with this purchase";
        return text1 + GetRewardPointAndPointValue() + text2;
    }
}
