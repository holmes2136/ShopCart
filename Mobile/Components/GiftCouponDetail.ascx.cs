using System;
using System.Web.Security;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Discounts;
using Vevo.Domain.Orders;
using Vevo.Domain.Payments;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.WebUI.International;

public partial class Mobile_Components_GiftCouponDetail : BaseLanguageUserControl
{
    private void TiesTextBoxesWithButtons()
    {
        WebUtilities.TieButton( Page, uxGiftCertificateCodeText, uxVerifyGiftButton );
        WebUtilities.TieButton( Page, uxCouponIDText, uxVerifyCouponButton );
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

    private void DisplayCouponError()
    {
        Coupon coupon = DataAccessContext.CouponRepository.GetOne( uxCouponIDText.Text );
        uxCouponMessageDiv.Visible = false;
        uxCouponMessageDisplay.DisplayCouponErrorMessage( coupon, StoreContext.ShoppingCart.GetAllCartItems() );
    }

    private void DisplayNoCouponCodeError()
    {
        uxCouponMessageDisplay.HideAll();
        uxCouponMessageDiv.Visible = true;
        uxCouponMessage.DisplayErrorNoNewLine( "[$EnterCouponNumber]" );
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
                uxMessage.DisplayErrorNoNewLine( errorMessage );
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
        uxCouponMessageDiv.Visible = false;
        uxCouponMessageDisplay.DisplayCouponDetails( coupon, "match_product" );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        TiesTextBoxesWithButtons();

            uxGiftCertificateTable.Visible = DataAccessContext.Configurations.GetBoolValue( "GiftCertificateEnabled" );
            uxCouponDiv.Visible = DataAccessContext.Configurations.GetBoolValue( "CouponEnabled" );
    }

    protected void uxVerifyGiftButton_Click( object sender, EventArgs e )
    {
        if (uxGiftCertificateCodeText.Text != "")
        {
            if (ValidateGiftCertificate())
            {
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
            uxMessage.DisplayErrorNoNewLine( "[$EnterGiftCertificateNumber]" );
        }
    }

    protected void uxVeryfyCouponButton_Click( object sender, EventArgs e )
    {
        if (uxCouponIDText.Text != "")
        {
            if (ValidateCoupon())
            {
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

    public bool ValidateAndSetUp()
    {
        if (!ValidateCoupon())
        {
            DisplayCouponError();
            return false;
        }

        if (!ValidateGiftCertificate())
        {
            return false;
        }

        StoreContext.CheckoutDetails.SetCouponID( uxCouponIDText.Text );
        StoreContext.CheckoutDetails.SetGiftCertificate( uxGiftCertificateCodeText.Text );
        StoreContext.CheckoutDetails.SetCustomerComments( uxCustomerComments.Text );

        return true;
    }

    public void PopulateData()
    {
        if (String.IsNullOrEmpty( uxCouponIDText.Text ))
            uxCouponIDText.Text = StoreContext.CheckoutDetails.Coupon.CouponID;

        if (String.IsNullOrEmpty( uxGiftCertificateCodeText.Text ))
            uxGiftCertificateCodeText.Text =
                StoreContext.CheckoutDetails.GiftCertificate.GiftCertificateCode;

        if (String.IsNullOrEmpty( uxCustomerComments.Text ))
            uxCustomerComments.Text = StoreContext.CheckoutDetails.CustomerComments;

    }
}
