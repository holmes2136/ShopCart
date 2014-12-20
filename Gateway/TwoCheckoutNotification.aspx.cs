using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using GCheckout.Util;
using Vevo;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Payments;
using Vevo.Domain.Users;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.WebUI.Orders;
using Vevo.Base.Domain;

public partial class TwoCheckoutNotification : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private string OrderID
    {
        get { return Request.Form["cart_order_id"]; }
    }

    private string GatewayOrderID
    {
        get { return Request.Form["order_number"]; }
    }

    private string CreditCardProcess
    {
        get { return Request.Form["credit_card_processed"]; }
    }


    private bool VerifyReferrer()
    {
        if (Request.UrlReferrer != null)
        {
            string[] referrers = new string[] { "www.2checkout.com", "2checkout.com", "www2.2checkout.com" };

            foreach (string s in referrers)
            {
                if (String.Compare( Request.UrlReferrer.Host, s, true ) == 0)
                {
                    return true;
                }
            }
            return false;
        }
        else
        {
            return true;
        }
    }

    private void UpdateOrderDetails()
    {
        Log.Debug( Request.QueryString.ToString() );
        Order order = DataAccessContext.OrderRepository.GetOne( OrderID );

        string cardHolderName = order.Billing.FirstName + " " + order.Billing.LastName;
        if (!String.IsNullOrEmpty( Request.Form["card_holder_name"] ))
            cardHolderName = Request.Form["card_holder_name"];

        string address = order.Billing.Address1 + " " + order.Billing.Address2;
        if (!String.IsNullOrEmpty( Request.Form["street_address"] ))
            address = Request.Form["street_address"];

        string city = order.Billing.City;
        if (!String.IsNullOrEmpty( Request.Form["city"] ))
            city = Request.Form["city"];

        string state = order.Billing.State;
        if ((String.IsNullOrEmpty( Request.Form["state"] ) || Request.Form["state"] == "XX"))
            state = Request.Form["state"];

        string country = order.Billing.Country;
        if (!String.IsNullOrEmpty( Request.Form["country"] ))
            country = Request.Form["country"];

        string zip = order.Billing.Zip;
        if (!String.IsNullOrEmpty( Request.Form["zip"] ))
            zip = Request.Form["zip"];

        string phone = order.Billing.Phone;
        if (!String.IsNullOrEmpty( Request.Form["phone"] ))
            phone = Request.Form["phone"];

        order.Billing = new Address( cardHolderName, String.Empty, order.Billing.Company, address,
            String.Empty, city, state, zip, country, phone, order.Billing.Fax );
        if (!String.IsNullOrEmpty( Request.Form["email"] ))
            order.Email = Request.Form["email"];

        order.GatewayOrderID = GatewayOrderID;

        DataAccessContext.OrderRepository.Save( order );
    }

    //private string GetPaymentName()
    //{
    //    //PaymentMethod payment;
    //    //payment = new TwoCheckoutPaymentMethod();

    //    //return payment.Name;
    //    return "2Checkout";
    //}

    protected void Page_Load( object sender, EventArgs e )
    {
        string storeUrl = DataAccessContext.StoreRetriever.GetStorefrontUrlByOrderID( OrderID );

        if (VerifyReferrer())
        {
            PaymentLog paymentLog = new PaymentLog();
            paymentLog.OrderID = OrderID;
            paymentLog.PaymentResponse = Request.Form.ToString();
            paymentLog.PaymentGateway = "2Checkout";
            paymentLog.PaymentType = String.Empty;
            DataAccessContext.PaymentLogRepository.Save( paymentLog );
            //PaymentLogAccess.Create( OrderID, Request.Form.ToString(), GetPaymentName(), "" );

            if (CreditCardProcess == "Y")
            {
                UpdateOrderDetails();

                OrderNotifyService order = new OrderNotifyService( OrderID );
                order.SendOrderEmail();
                order.ProcessPaymentComplete();

                Response.Redirect( String.Format( "{0}/CheckoutComplete.aspx?OrderID={1}", storeUrl, OrderID + "&IsTransaction=true" ) );
            }
            else
            {
                Response.Redirect( String.Format( "{0}/CheckoutNotComplete.aspx?OrderID={1}", storeUrl, OrderID ) );
            }
        }
        else
        {
            Response.Redirect( String.Format( "{0}/CheckoutNotComplete.aspx?OrderID={1}", storeUrl, OrderID ) );
        }
    }

}
