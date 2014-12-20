using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Payments;
using Vevo.Shared.WebUI;
using Vevo.WebAppLib;
using Vevo.WebUI;

public partial class GatewayPosting : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
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

        string urlHidden = PaymentAppGateway.GetPaymentAppUrl( "/HostedRecordHtml.aspx", UrlPath.StorefrontUrl );

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
            //throw new Exception( "Unknown payment type." );
            Response.Redirect( "GatewayPaymentError.aspx" );
        }

        CreatePostParameters( checkout );
    }
}
