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

public partial class Components_VerifyCoupon : Vevo.WebUI.International.BaseLanguageUserControl
{
    private void TiesTextBoxesWithButtons()
    {
        
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

   

   

    

    private void DisplayCouponDetails()
    {
        Coupon coupon = DataAccessContext.CouponRepository.GetOne( uxCouponIDText.Text );
        uxCouponMessageDiv.Visible = true;
        uxCouponMessageDisplay.DisplayCouponDetails( coupon, "match_product" );
    }

    

    protected void Page_Load( object sender, EventArgs e )
    {
       
        uxCouponErrorMessageDiv.Visible = false;

        TiesTextBoxesWithButtons();

       
        uxCouponDiv.Visible = DataAccessContext.Configurations.GetBoolValue( "CouponEnabled" );

      
    }

    private void DisplayClearButton()
    {
       
        if (!String.IsNullOrEmpty( uxCouponIDText.Text ))
        {
            uxClearCouponImageButton.Visible = true;
        }
        else
        {
            uxClearCouponImageButton.Visible = false;
        }

       
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
       
        uxCouponIDText.Text = StoreContext.CheckoutDetails.Coupon.CouponID;
     

        DisplayClearButton();
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
}
