using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using Vevo.Domain.Orders;
using Vevo.Domain.Payments;
using Vevo.Domain.Payments.TwoCheckout;
using Vevo.Shared.WebUI;
using Vevo.WebAppLib;
using Vevo.WebUI;

public partial class Gateway_GatewayTwoCheckoutPost : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private string CreateTagHidden( string name, string value )
    {
        return "<input type=\"hidden\" name=\"" + name + "\" value=\"" + value + "\" />\n";
    }

    private string CreateTagHiddenWithID( string name, string value )
    {
        return "<input type=\"hidden\" id=\"" + name + "\" name=\"" + name + "\" value=\"" +
                value + "\" />\n";
    }

    public string CreateParameterText( NameValueCollection collection, TwoCheckoutRedirectPaymentMethod paymentMethod )
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < collection.Count; i++)
        {
            string text;
            if (paymentMethod.IsHiddenFieldUsingID)
                text = CreateTagHiddenWithID( collection.GetKey( i ), collection.Get( i ) );
            else
                text = CreateTagHidden( collection.GetKey( i ), collection.Get( i ) );

            text = CreateTagHidden( collection.GetKey( i ), collection.Get( i ) );

            sb.Append( text );
        }
        return sb.ToString();
    }

    private string OrderID
    {
        get
        {
            return Request.QueryString["OrderID"];
        }
    }

    private void CreatePostParameters( CheckoutDetails checkout )
    {
        PaymentAppGateway gateway = new PaymentAppGateway( checkout );
        TwoCheckoutRedirectPaymentMethod twoCheckoutPaymentMethod = (TwoCheckoutRedirectPaymentMethod) checkout.PaymentMethod;
        string urlHidden = twoCheckoutPaymentMethod.GetPostedUrl();

        NameValueCollection collection = twoCheckoutPaymentMethod.CreatePostParameters(
                checkout,
                StoreContext.Culture,
                StoreContext.Currency,
                StoreContext.ShoppingCart,
                UrlPath.StorefrontUrl,
                OrderID,
                StoreContext.GetOrderAmount().Total,
                StoreContext.WholesaleStatus,
                WebUtilities.GetVisitorIP() );

        uxLiteral.Text = CreateParameterText( collection, twoCheckoutPaymentMethod );

        string xmlData = gateway.CreateHostedPaymentXml(
                StoreContext.Culture,
                StoreContext.Currency,
                StoreContext.ShoppingCart,
                UrlPath.StorefrontUrl,
                OrderID,
                StoreContext.GetOrderAmount().Total,
                StoreContext.WholesaleStatus,
                WebUtilities.GetVisitorIP() );

        HostedXml.Value = HttpUtility.UrlEncode( xmlData );
        uxUrlHidden.Value = urlHidden;
        uxRefreshLink.NavigateUrl = urlHidden;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        CheckoutDetails checkout = StoreContext.CheckoutDetails;

        if (checkout.PaymentMethod.IsNull ||
            !(checkout.PaymentMethod is RedirectPaymentMethod))
        {
            Response.Redirect( "GatewayPaymentError.aspx" );
        }

        CreatePostParameters( checkout );
    }
}