using System;
using System.Collections.Specialized;
using System.Text;
using Vevo.Domain.Orders;
using Vevo.Domain.Payments.PayPal;
using Vevo.Shared.WebUI;
using Vevo.WebAppLib;
using Vevo.WebUI;

public partial class Gateway_GatewayPayPalPost : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
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

    private string OrderID
    {
        get
        {
            return Request.QueryString["OrderID"];
        }
    }

    private string CreateParameterText( CheckoutDetails checkoutDetails, PayPalRedirectPaymentMethod paymentMethod )
    {
        NameValueCollection collection = paymentMethod.CreatePostParameters( 
                    checkoutDetails,
                    StoreContext.Culture,
                    StoreContext.Currency,
                    StoreContext.ShoppingCart,
                    UrlPath.StorefrontUrl,
                    OrderID,
                    StoreContext.GetOrderAmount().Total,
                    StoreContext.WholesaleStatus,
                    WebUtilities.GetVisitorIP()
                    );

        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < collection.Count; i++)
        {
            string text;
            if (paymentMethod.IsHiddenFieldUsingID)
                text = CreateTagHiddenWithID( collection.GetKey( i ), collection.Get( i ) );
            else
                text = CreateTagHidden( collection.GetKey( i ), collection.Get( i ) );

            sb.Append( text );
        }

        return sb.ToString();
    }

    private void CreatePostParameters( CheckoutDetails checkout )
    {
        PayPalRedirectPaymentMethod paymentMethod = (PayPalRedirectPaymentMethod) checkout.PaymentMethod;
        uxLiteral.Text = CreateParameterText( checkout, paymentMethod );
        uxUrlHidden.Value = paymentMethod.GetPostedUrl();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckoutDetails checkout = StoreContext.CheckoutDetails;
        if (checkout.PaymentMethod.IsNull || !(checkout.PaymentMethod is PayPalRedirectPaymentMethod))
        {
            Response.Redirect( "GatewayPaymentError.aspx" );
        }
        uxRefreshLink.NavigateUrl = Request.Url.PathAndQuery;
        CreatePostParameters( checkout );
    }
}