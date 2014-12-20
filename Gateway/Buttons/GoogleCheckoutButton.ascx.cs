using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using GCheckout.Checkout;
using GCheckout.Util;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Payments;
using Vevo.Domain.Payments.Google;
using Vevo.Domain.Payments.SessionState;
using Vevo.Shared.Utilities;
using Vevo.Shared.WebUI;
using Vevo.WebAppLib;
using Vevo.WebUI;

public partial class Gateway_Buttons_GoogleCheckoutButton : Vevo.WebUI.Payments.BaseButtonPaymentMethod
{
    public bool HaveQuantityDiscount()
    {
        foreach (ICartItem item in StoreContext.ShoppingCart.GetCartItems())
        {
            if (ConvertUtilities.ToInt32( item.DiscountGroupID ) > 0)
                return true;
        }
        return false;
    }

    public override void AddConfrimBox()
    {
        if (HaveQuantityDiscount())
        {
            WebUtilities.ButtonAddConfirmation( uxGCheckoutButton,
                "Google Checkout does not accept Quantity Discount. " +
                "You will not get discount with this payment method. Would you like to continue?" );
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {

    }

    protected void PostCartToGoogle( object sender, System.Web.UI.ImageClickEventArgs e )
    {
        uxGCheckoutButton.Currency = DataAccessContext.Configurations.GetValue( "PaymentCurrency" );
        CheckoutShoppingCartRequest request = uxGCheckoutButton.CreateRequest();

        GoogleCheckoutRequestHelper helper = new GoogleCheckoutRequestHelper();

        helper.PopulateRequest(
            UrlPath.StorefrontUrl,
            request,
            StoreContext.ShoppingCart,
            StoreContext.Culture,
            StoreContext.Currency,
            StoreContext.WholesaleStatus,
            StoreContext.CheckoutDetails );

        StoreContext.ClearCheckoutSession();

        Log.Debug( "----------------------------------------" );
        Log.Debug( "Request XML: " + EncodeHelper.Utf8BytesToString( request.GetXml() ) );

        GCheckoutResponse response = request.Send();
        if (response.IsGood)
        {
            if (UrlManager.IsFacebook())
            {
                string script = "window.parent.location.href='" + response.RedirectUrl + "'";
                ScriptManager.RegisterStartupScript( this, typeof( Page ), "startScript", script, true );
            }
            else
            {
                Response.Redirect( response.RedirectUrl, true );
            }
        }
        else
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine( "Google Checkout Request Error: " );
            sb.AppendLine( "ResponseXml = " + response.ResponseXml );
            sb.AppendLine( "RedirectUrl = " + response.RedirectUrl );
            sb.AppendLine( "IsGood = " + response.IsGood );
            sb.AppendLine( "ErrorMessage = " + response.ErrorMessage );

            throw new VevoException( sb.ToString() );
        }

    }

}
