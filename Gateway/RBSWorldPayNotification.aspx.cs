using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Payments;
using Vevo.Shared.WebUI;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.WebUI.Orders;

public partial class Gateway_RBSWorldPayNotification : System.Web.UI.Page
{
    private string Invoice
    {
        get
        {
            return Request.Form["cartID"];
        }
    }

    private string PaymentStatus
    {
        get
        {
            return Request.Form["transStatus"];
        }
    }

    private string TransID
    {
        get
        {
            return Request.Form["transId"];
        }
    }

    private string Avs
    {
        get
        {
            return Request.Form["AVS"];
        }
    }

    private void VerifyAvsAndCvv( Order orderDetails )
    {
        if (String.IsNullOrEmpty( Avs ))
            return;

        string cvv = Avs[0].ToString();
        string avsZip = Avs[1].ToString();
        string avsAddr = Avs[2].ToString();

        if (cvv == "2")
            orderDetails.CvvStatus = "Pass";
        else if (cvv == "4")
            orderDetails.CvvStatus = "Fail";
        else
            orderDetails.CvvStatus = "Unavailable";

        if (avsZip == "2")
            orderDetails.AvsZipStatus = "Pass";
        else if (avsZip == "4")
            orderDetails.AvsZipStatus = "Fail";
        else
            orderDetails.AvsZipStatus = "Unavailable";

        if (avsAddr == "2")
            orderDetails.AvsAddrStatus = "Pass";
        else if (avsAddr == "4")
            orderDetails.AvsAddrStatus = "Fail";
        else
            orderDetails.AvsAddrStatus = "Unavailable";
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        PaymentLog paymentLog = new PaymentLog();
        paymentLog.OrderID = Invoice;
        paymentLog.PaymentResponse = Request.Form.ToString();
        paymentLog.PaymentGateway = "RBSWorldPay";
        paymentLog.PaymentType = "RBSWorldPay";
        DataAccessContext.PaymentLogRepository.Save( paymentLog );
        //PaymentLogAccess.Create( Invoice, Request.Form.ToString(), "RBSWorldPay", "RBSWorldpay" );
        ProcessRBSWorldPayIPN();
    }


    private void ProcessRBSWorldPayIPN()
    {
        OrderNotifyService order = new OrderNotifyService( Invoice );

        Order orderDetails = DataAccessContext.OrderRepository.GetOne( order.OrderID );

        if (PaymentStatus.ToUpper() == "Y")
        {
            order.SendOrderEmail();
        }

        //OrdersAccess.UpdateGatewayOrderID( Invoice, TransID );
        //OrdersAccess.UpdateGatewayStatusByGatewayOrderID( TransID, PaymentStatus );

        orderDetails.GatewayOrderID = TransID;
        orderDetails.GatewayPaymentStatus = PaymentStatus;
        VerifyAvsAndCvv( orderDetails );
        string retURL = UrlPath.StorefrontUrl;
        // Create the IPN Transaction
        if (PaymentStatus.ToUpper() == "Y")
        {
            uxCheckoutHeaderLabel.Text = "Thank you for your order.";
            uxCheckoutDetailLabel.Text = "To view order information, please click the link below.";
            order.ProcessPaymentComplete();
            uxCheckoutLink.NavigateUrl
                = String.Format( UrlPath.StorefrontUrl + "CheckoutComplete.aspx?OrderID={0}",
                order.OrderID + "&IsTransaction=true" );
            uxUrlHidden.Value
                = String.Format( UrlPath.StorefrontUrl + "CheckoutComplete.aspx?OrderID={0}",
                order.OrderID + "&IsTransaction=true" );
            uxHomeLink.Visible = false;

            retURL =  String.Format( UrlPath.StorefrontUrl + "CheckoutComplete.aspx?OrderID={0}",
                order.OrderID + "&IsTransaction=true" );
        }
        else
        {
            uxCheckoutHeaderLabel.Text = "Order Not Complete";
            uxCheckoutDetailLabel.Text = "Your Order cannot be completed.<br/><br/>Please verify your payment information and try checkout again.";
            order.ProcessPaymentFailed();
            uxCheckoutLink.NavigateUrl
                = String.Format( UrlPath.StorefrontUrl + "CheckoutNotComplete.aspx?OrderID={0}", order.OrderID );
            uxUrlHidden.Value
                = String.Format( UrlPath.StorefrontUrl + "CheckoutNotComplete.aspx?OrderID={0}", order.OrderID );
            uxCheckoutLink.Visible = false;
            uxHomeLink.NavigateUrl = UrlPath.StorefrontUrl;

            retURL= String.Format( UrlPath.StorefrontUrl + "CheckoutNotComplete.aspx?OrderID={0}", order.OrderID );
        }
            
            HtmlMeta meta = new HtmlMeta();
            meta.ID = "meta" + "refresh";
            meta.HttpEquiv = "refresh";
            meta.Content = "0;URL=" + retURL;
            Page.Header.Controls.Add( meta );
    }
}
