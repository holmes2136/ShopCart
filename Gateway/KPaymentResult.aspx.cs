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
using Vevo;
using Vevo.DataAccessLib;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Payments;
using Vevo.WebUI.Orders;

public partial class Gateway_KPaymentResult : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private bool IsApproved
    {
        get
        {
            if (Request.Form["HOSTRESP"].ToString() == "00")
                return true;
            else
                return false;
            //return Request.Form["HOSTRESP"];
        }
    }

    private string RespCode
    {
        get
        {
            return Request.Form["REFCODE"];
        }
    }

    private string AuthCode
    {
        get
        {
            return Request.Form["AUTHCODE"];
        }
    }

    private string BankInvoiceID
    {
        get
        {
            return int.Parse( Request.Form["RETURNINV"] ).ToString();
        }
    }

    private string UAID
    {
        get
        {
            return Request.Form["UAID"];
        }
    }

    private string CardType
    {
        get
        {
            if (String.IsNullOrEmpty( Request.Form["FILLSPACE"] ))
                return "";
            else
                return Request.Form["FILLSPACE"];
        }
    }

    private string GetPaymentName()
    {
        return "KPayment";
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        SaveDataKPayment();
    }

    private void SaveDataKPayment()
    {
        string result = "IsApproved:" + IsApproved.ToString() + ", ";
        result += String.Format( "RespCode:{0}, ", RespCode );
        result += String.Format( "AuthCode:{0}, ", AuthCode );
        result += String.Format( "UAID:{0}, ", UAID );
        result += String.Format( "CardType:{0}", CardType );

        PaymentLog paymentLog = new PaymentLog();
        paymentLog.OrderID = BankInvoiceID;
        paymentLog.PaymentResponse = result;
        paymentLog.PaymentGateway = GetPaymentName();
        paymentLog.PaymentType = String.Empty;
        DataAccessContext.PaymentLogRepository.Save( paymentLog );
        //PaymentLogAccess.Create( BankInvoiceID, result, GetPaymentName(), "" );

        OrderNotifyService order = new OrderNotifyService( BankInvoiceID );
        if (IsApproved)
        {
            order.SendOrderEmail();
            order.ProcessPaymentComplete();

            Response.Redirect( String.Format( "~/CheckoutComplete.aspx?OrderID={0}&IsTransaction=True", BankInvoiceID ) );
        }
        else
        {
            order.ProcessPaymentFailed();

            Response.Redirect( String.Format( "~/CheckoutNotComplete.aspx?OrderID={0}", BankInvoiceID ) );
        }
    }
}
