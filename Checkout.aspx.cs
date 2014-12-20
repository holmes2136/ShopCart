using System;
using System.Web.Security;
using System.Web.UI;
using Vevo;
using Vevo.Domain;
using Vevo.WebUI;
using Vevo.WebUI.International;

public partial class Checkout : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{

    private bool IsAnonymousCheckout()
    {
        if ( Request.QueryString[ "skiplogin" ] == "true" && !Roles.IsUserInRole( "Customers" ) )
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void RedirectNextPage()
    {

        if ( !IsAnonymousCheckout() )
        {
            Response.Redirect( "Shipping.aspx" );
        }
        else
            Response.Redirect( "Shipping.aspx?skiplogin=true" );
    }

    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        if ( !Roles.IsUserInRole( "Customers" ) && !IsAnonymousCheckout() )
        {
            Response.Redirect( "UserLogin.aspx?ReturnUrl=Checkout.aspx" );
        }

        if ( DataAccessContext.Configurations.GetValue( "CheckoutMode" ).ToString() != "Normal" )
        {
            if ( !IsAnonymousCheckout() )
                Response.Redirect( "OnePageCheckout.aspx" );
            else
                Response.Redirect( "OnePageCheckout.aspx?skiplogin=true" );
        }

        if ( StoreContext.ShoppingCart.GetCartItems().Length <= 0 )
        {
            uxPlaceOrderImageButton.Visible = false;
            return;
        }

        if ( !DataAccessContext.Configurations.GetBoolValue( "ShippingAddressMode" ) )
        {
            uxCheckoutDetails.SetShippingAddress();
            RedirectNextPage();
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (StoreContext.ShoppingCart.GetCartItems().Length == 0)
        {
            uxMessage.DisplayError( uxCheckoutDetails.DisplayNoItemToCheckout() );
        }
    }

    protected void uxPlaceOrderImageButton_Click( object sender, EventArgs e )
    {
        if ( !uxCheckoutDetails.IsShippingAddressReady() )
        {
            return;
        }

        if ( uxCheckoutDetails.VerifyCountryAndState() )
        {
            if ( !IsAnonymousCheckout() )
                uxCheckoutDetails.SetShippingAddress();
            else
                uxCheckoutDetails.SetAnonymousAddress();

            uxCheckoutDetails.SetBillingAddressAnonymous();
            uxCheckoutDetails.SetSaleTaxExempt();
            uxCheckoutDetails.SetSpecialRequest();
            RedirectNextPage();
        }
    }

    #endregion
}