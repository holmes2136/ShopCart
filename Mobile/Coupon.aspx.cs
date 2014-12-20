using System;
using Vevo.Domain;
using Vevo.Domain.Discounts;
using Vevo.Domain.Products;
using Vevo.WebUI;

public partial class Mobile_Coupon : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    #region Private

    private string CouponID
    {
        get
        {
            return uxCouponIDText.Text;
        }
    }

    private void DisplayExpiredError( Coupon.ExpirationStatusEnum expirationStatus )
    {
        switch (expirationStatus)
        {
            case Coupon.ExpirationStatusEnum.ExpiredByDate:
                uxMessage.DisplayError( "[$Expire]" );
                break;
            case Coupon.ExpirationStatusEnum.ExpiredByQuantity:
                uxMessage.DisplayError( "[$Limit]" );
                break;
            default:
                uxMessage.DisplayError( "[$NotValid]" );
                break;
        }
        StoreContext.CheckoutDetails.SetCouponID( "" );
    }

    private void DisplayCouponDetails( Coupon coupon )
    {
        uxMessage.DisplayMessage( "[$Accept]" );

        uxCouponMessageDisplay.DisplayCouponDetails( coupon, "all_product" );
        uxCouponMessageDisplay.Visible = true;

        uxCouponIDText.Text = "";
    }

    private bool IsDiscountableProduct( Product product )
    {
        return product.DiscountGroupID != "0";
    }

    private bool ValidateCustomer( Coupon coupon, out string errorMessage )
    {
        errorMessage = String.Empty;
        if (coupon.CustomerFilter == Coupon.CustomerFilterEnum.ByUserNames)
        {
            if (StoreContext.Customer.IsNull)
            {
                errorMessage = "[$ThistCouponIsOnlyValidForUser]";
                //This coupon is specifie for user please log in before use this coupon.
                return false;
            }

            if (!coupon.IsCustomerDiscountable( StoreContext.Customer ))
            {
                errorMessage = "[$NotValidForYourUser]";
                //This coupon is not valid for your log-in account.
                return false;
            }
        }

        return true;
    }

    private void VerifyCoupon()
    {
        uxCouponMessageDisplay.HideAll();

        if (String.IsNullOrEmpty( CouponID ))
        {
            uxMessage.DisplayError( "[$NoCoupon]" );
            return;
        }

        Coupon coupon = DataAccessContext.CouponRepository.GetOne( CouponID );

        // Verify if coupon expired
        if (coupon.ExpirationStatus != Coupon.ExpirationStatusEnum.NotExpired)
        {
            if (coupon.IsNull)
                DisplayExpiredError( Coupon.ExpirationStatusEnum.UnknownType );
            else
                DisplayExpiredError( coupon.ExpirationStatus );
            return;
        }

        string errorMessage;
        if (!ValidateCustomer( coupon, out errorMessage ))
        {
            uxMessage.DisplayError( errorMessage );
            return;
        }

        uxCouponLiteral.Text = "\"" + CouponID + "\"";
        DisplayCouponDetails( coupon );
        StoreContext.CheckoutDetails.SetCouponID( CouponID );
    }

    #endregion

    protected void uxCouponButton_Click( object sender, EventArgs e )
    {
        VerifyCoupon();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxCouponMessageDisplay.Visible = false;
        uxCouponLiteral.Text = "";
    }
}
