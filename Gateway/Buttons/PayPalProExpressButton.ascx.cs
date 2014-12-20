using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using PayPal.Payments.Transactions;
using PayPal.Payments.DataObjects;
using PayPal.Payments.Common.Utility;
using com.paypal.sdk.core;
using com.paypal.sdk.util;
using com.paypal.sdk.services;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Payments;
using Vevo.Domain.Payments.PayPalProExpress;
using Vevo.Shared.WebUI;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.Domain.Discounts;
using System.Collections.Generic;

public partial class Gateway_Buttons_PayPalProExpressButton : Vevo.WebUI.Payments.BaseButtonPaymentMethod
{
    protected void Page_Load( object sender, EventArgs e )
    {

    }

    protected void uxPayPalImageButton_Click( object sender, EventArgs e )
    {
        if (Page.User.Identity.IsAuthenticated)
            FormsAuthentication.SignOut();

        SetExpressCheckout();
    }

    private void SetExpressCheckout()
    {
        string storefrontUrl = String.Empty;

        if (UrlManager.IsMobile())
        {
            storefrontUrl = UrlPath.StorefrontUrl + "Mobile/";
        }
        else
        {
            storefrontUrl = UrlPath.StorefrontUrl;
        }

        string returnURL = storefrontUrl + "Shipping.aspx";
        string cancelURL = storefrontUrl + "ShoppingCart.aspx";

        IList<decimal> discountLines;
        decimal discount = GetDiscountLine( StoreContext.ShoppingCart.GetCartItems(), out discountLines );

        NVPCallerServices caller = PayPalProExpressUtilities.Instance.PayPalAPIInitialize();
        NVPCodec encoder = new NVPCodec();
        encoder["METHOD"] = "SetExpressCheckout";
        encoder["AMT"] = Vevo.Domain.Currency.ConvertPriceToUSFormat( GetShoppingCartTotal().ToString( "f2" ) );
        encoder["RETURNURL"] = returnURL;
        encoder["CANCELURL"] = cancelURL;
        if (Request.QueryString["Token"] != null)
            encoder["TOKEN"] = Request.QueryString["Token"];
        encoder["PAYMENTACTION"] = PayPalProExpressUtilities.PaymentType;
        encoder["CURRENCYCODE"] = DataAccessContext.Configurations.GetValue( "PaymentCurrency" );
        if (DataAccessContext.Configurations.GetBoolValue( "ShippingAddressMode" ))
            encoder["NOSHIPPING"] = "0";
        else
            encoder["NOSHIPPING"] = "1";

        encoder["L_NAME0"] = "Payment for " + DataAccessContext.Configurations.GetValue( StoreContext.Culture.CultureID, "SiteName" );
        encoder["L_AMT0"] = Vevo.Domain.Currency.ConvertPriceToUSFormat(GetShoppingCartTotal().ToString( "f2" ));

        if (!UrlManager.IsMobile())
        {
            encoder["VERSION"] = "65.0";
        }

        string pStrrequestforNvp = encoder.Encode();
        string pStresponsenvp = caller.Call( pStrrequestforNvp );

        NVPCodec decoder = new NVPCodec();
        decoder.Decode( pStresponsenvp );

        string strAck = decoder["ACK"];
        if (strAck != null && (strAck == "Success" || strAck == "SuccessWithWarning"))
        {
            PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne(
                StoreContext.Culture, PaymentOption.PayPalProExpress );
            StoreContext.CheckoutDetails.PaymentMethod = paymentOption.CreatePaymentMethod();
            //PayPalProExpressUtilities.Instance.Token = decoder["TOKEN"];
            StoreContext.CheckoutDetails.CustomParameters[CheckoutDetails.PayPalProExpress_TokenID] = decoder["TOKEN"];
            if (UrlManager.IsFacebook())
            {
                string script = "window.parent.location.href='" + PayPalProExpressUtilities.Instance.UrlSetExpressCheckout( UrlManager.IsMobile() ) + "&useraction=commit&token=" + decoder["TOKEN"] + "'";
                ScriptManager.RegisterStartupScript( this, typeof( Page ), "startScript", script, true );
            }
            else
            {
                Response.Redirect( PayPalProExpressUtilities.Instance.UrlSetExpressCheckout( UrlManager.IsMobile() ) + "&useraction=commit&token=" + decoder["TOKEN"] );
            }
        }
        else
        {
            //PayPalProExpressUtilities.RedirectToErrorPage( decoder );            
            CheckoutNotCompletePage.RedirectToPage(
                "Error Message",
                PayPalProExpressUtilities.GetErrorMessage( decoder ),
                "ShoppingCart.aspx",
                "Return To Shopping" );
        }
    }

    private decimal GetShoppingCartTotal()
    {
        OrderCalculator orderCalculator = new OrderCalculator();
        return orderCalculator.GetShoppingCartTotal(
            StoreContext.Customer,
            StoreContext.ShoppingCart.SeparateCartItemGroups(),
            StoreContext.CheckoutDetails.Coupon );
    }

    private decimal GetDiscountLine( ICartItem[] items, out IList<decimal> discountLines )
    {
        CartItemGroup group = new CartItemGroup( items );

        DiscountCalculator cal = new DiscountCalculator();
        return cal.GetDiscount( group, StoreContext.CheckoutDetails.Coupon, StoreContext.Customer, out discountLines );
    }
}
