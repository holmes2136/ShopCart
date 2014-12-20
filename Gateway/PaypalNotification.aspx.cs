using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized; // For NameValueCollection.
using System.IO;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using GCheckout.Util;
using Vevo;
using Vevo.DataAccessLib;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Payments;
using Vevo.Domain.Payments.PayPalProUS;
using Vevo.Shared.SystemServices;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.WebUI.Orders;

public partial class PayPalNotification : Page
{
    private string Invoice
    {
        get
        {
            return Request.Form["invoice"];
        }
    }

    //Completed,
    private string PaymentStatus
    {
        get
        {
            return Request.Form["payment_status"];
        }
    }

    // Partial list of type returning from PayPal IPN (txn_type):
    //   recurring_payment_profile_created
    //   recurring_payment
    //   recurring_payment_failed
    //   web_accept
    private string TxnType
    {
        get
        {
            return Request.Form["txn_type"];
        }
    }

    // Ex. 03:00:00 May 01, 2009 PDT, N/A
    private string NextPaymentDate
    {
        get
        {
            return Request.Form["next_payment_date"];
        }
    }

    private bool TestMode
    {
        get
        {
            if (Request.Form["test_ipn"] == "1")
                return true;
            else
                return false;
        }
    }

    // Regular, Trial
    private string PeriodType
    {
        get
        {
            if (Request.Form["period_type"] != null)
                return Request.Form["period_type"].Trim( ' ' );
            else
                return "";
        }
    }

    // I-LUHN9BSSNVH2
    private string RecurringReferenceID
    {
        get
        {
            return Request.Form["recurring_payment_id"];
        }
    }

    // 115.50
    private decimal RecurringAmount
    {
        get
        {
            return ConvertUtilities.ToDecimal( Request.Form["amount"] );
        }
    }

    private bool TestByCustomPost
    {
        get
        {
            if (Request.Form["VevoCustomPost"] == "1")
                return true;
            else
                return false;
        }
    }

    //Expired, Active
    private string RecurringStatus
    {
        get
        {
            return Request.Form["profile_status"];
        }
    }

    private string ReceiptID
    {
        get
        {
            if (Request.Form["receipt_id"] != null)
                return Request.Form["receipt_id"];
            else
                return "";
        }
    }

    private string ProductName
    {
        get
        {
            return Request.Form["product_name"];
        }
    }

    private string ReceiverID
    {
        get
        {
            return Request.Form["receiver_id"];
        }
    }

    private string PaymentType
    {
        get
        {
            return Request.Form["payment_type"];
        }
    }

    private string PaymentGross
    {
        get
        {
            return Request.Form["payment_gross"];
        }
    }

    private string MCGross
    {
        get
        {
            return Request.Form["mc_gross"];
        }
    }

    private string PaymentDate
    {
        get
        {
            return Request.Form["payment_date"];
        }
    }

    private string GetPaymentName()
    {
        PaymentMethod payment;
        if (!String.IsNullOrEmpty( Invoice ))
        {
            return "PayPal";
        }
        else
        {
            payment = new PayPalProUSPaymentMethod();
        }

        return payment.Name;
    }

    private void ProcessPayPalStandardIPN()
    {
        OrderNotifyService order = new OrderNotifyService( Invoice );
        Order details = DataAccessContext.OrderRepository.GetOne( order.OrderID );

        if (String.IsNullOrEmpty( details.GatewayPaymentStatus ))
            order.SendOrderEmail();

        details.GatewayOrderID = Invoice;
        details.GatewayPaymentStatus = PaymentStatus;
        DataAccessContext.OrderRepository.Save( details );

        // Create the IPN Transaction
        switch (PaymentStatus.ToLower())
        {
            case "completed":
            case "canceled_reversal":
                order.ProcessPaymentComplete();
                break;

            case "refunded":
            case "reversed":
                order.ProcessPaymentFailed();
                break;
        }
    }

    private void ProcessRecurringPaymentProfileCreated()
    {
        try
        {
            Log.Debug( " ----- Start ProcessRecurringPaymentProfileCreated() ----- " );
            Log.Debug( " Recurring ReferenceID : " + RecurringReferenceID );
            string orderItemID = DataAccessContext.RecurringProfileRepository.GetOrderItemIDByReferenceID(
                RecurringReferenceID );
            Log.Debug( " OrderItemID : " + orderItemID );

            Log.Debug( "********************* Create PayPalIpn() ************************" );

            PayPalIpn payPalIpn = new PayPalIpn();
            payPalIpn.ReferenceID = RecurringReferenceID;
            payPalIpn.IpnResponse = Request.Form.ToString();
            payPalIpn.IsRecurringProfileCreated = false;
            payPalIpn.ReceiveDate = DateTime.Now;

            payPalIpn = DataAccessContext.PayPalIpnRepository.Save( payPalIpn );

            Log.Debug( "********************* Create PayPalIpn() Finished ************************" );

            if (!String.IsNullOrEmpty( orderItemID ))
            {
                Log.Debug( "-*-*-*-*-*-*-*-*-*-*-*-*-*-*- Have orderItemID -*-*-*-*-*-*-*-*-*-*-*-*-*-*-" );
                Log.Debug( "IpnID : " + payPalIpn.PayPalIpnID );
                OrderItem orderItem = DataAccessContext.OrderItemRepository.GetOne( orderItemID );

                Log.Debug( " orderItem : " + orderItem.ToString() );
                Log.Debug( "orderItem.RecurringID : " + orderItem.RecurringID );

                RecurringProfile recurringProfile = DataAccessContext.RecurringProfileRepository.GetOne( orderItem.RecurringID );

                recurringProfile = DataAccessContext.RecurringProfileRepository.GetOne( recurringProfile.RecurringID );
                recurringProfile.RecurringStatus = SystemConst.RecurringStatus.CreateVerified.ToString();
                recurringProfile.UpdateTime = DateTime.Now;
                DataAccessContext.RecurringProfileRepository.Save( recurringProfile );

                orderItem.OrderItemRecurring.RecurringStatus = SystemConst.RecurringStatus.CreateVerified.ToString();
                orderItem.OrderItemRecurring.UpdateTime = DateTime.Now;
                DataAccessContext.OrderItemRepository.UpdateRecurringItem( orderItem.OrderItemRecurring );

                PaymentLog paymentLog = new PaymentLog();
                paymentLog.OrderID = orderItem.OrderID;
                paymentLog.PaymentResponse = Request.Form.ToString();
                paymentLog.PaymentGateway = GetPaymentName();
                paymentLog.PaymentType = "PayPalRecurringCreate";
                DataAccessContext.PaymentLogRepository.Save( paymentLog );

                payPalIpn.IsRecurringProfileCreated = true;
                DataAccessContext.PayPalIpnRepository.Save( payPalIpn );

                Log.Debug( "  orderItemID : " + orderItemID );
                Log.Debug( "  RecurringStatus : " + SystemConst.RecurringStatus.CreateVerified.ToString() );
                Log.Debug( "  UpdateTime : " + DateTime.Now.ToString() );
                Log.Debug( "  PaymentType : " + paymentLog.PaymentType );
            }
            Log.Debug( " ----- End ProcessRecurringPaymentProfileCreated() ----- " );
        }
        catch (Exception ex)
        {
            Log.Debug( "    ***** Start ProcessRecurringPaymentProfileCreated() Exception ***** " );
            PaymentLog paymentLog = new PaymentLog();
            paymentLog.OrderID = "0";
            paymentLog.PaymentResponse = Request.Form.ToString() + "&Exception:" + ex.Message;
            paymentLog.PaymentGateway = GetPaymentName();
            paymentLog.PaymentType = "PayPalRecurringCreateFailed";
            DataAccessContext.PaymentLogRepository.Save( paymentLog );
            Log.Debug( " Error : " + ex );
            Log.Debug( "    ***** End ProcessRecurringPaymentProfileCreated() Exception ***** " );
            Log.Debug( " ----- End ProcessRecurringPaymentProfileCreated() ----- " );

        }
    }

    private void ProcessRecurringPayment()
    {
        try
        {
            Log.Debug( " ----- Start ProcessRecurringPayment() ----- " );
            Log.Debug( " PaymentStatus : " + PaymentStatus );

            String orderID = "";
            if (PaymentStatus == "Completed")
            {
                Log.Debug( "++++++++++ Enter CreateChildOrderByPayPalRefenceID ++++++++++" );
                int allSequence;
                orderID = PayPalProUSPaymentMethod.CreateChildOrderByPayPalRefenceID(
                    RecurringReferenceID,
                    RecurringAmount,
                    RecurringStatus,
                    PaymentStatus,
                    ReceiptID,
                    PeriodType,
                    DateTime.Now,
                    out allSequence );
                Log.Debug( "++++++++++ End CreateChildOrderByPayPalRefenceID ++++++++++" );

                Log.Debug( " OrderID : " + orderID );
                Log.Debug( " RecurringReferenceID : " + RecurringReferenceID );
                Log.Debug( " RecurringAmount : " + RecurringAmount );
                Log.Debug( " RecurringStatus : " + RecurringStatus );
                Log.Debug( " PaymentStatus : " + PaymentStatus );
                Log.Debug( " ReceiptID : " + ReceiptID );
                Log.Debug( " PeriodType : " + PeriodType );
                Log.Debug( " allSequence : " + allSequence );

                if (!String.IsNullOrEmpty( orderID ))
                {
                    if (allSequence > 1)
                    {
                        Log.Debug( " Enter AllSequence > 1" );
                        OrderNotifyService order = new OrderNotifyService( orderID );
                        order.SendOrderEmail();
                        Log.Debug( " SendOrderEmail() " );
                        Log.Debug( " End AllSequence > 1" );
                    }
                    else if (allSequence == 1)
                    {
                        Log.Debug( " Enter AllSequence == 1" );

                        string recurringID =
                            DataAccessContext.RecurringProfileRepository.GetRecurringIDFromReferenceID(
                                RecurringReferenceID );
                        RecurringProfile recurringProfile =
                            DataAccessContext.RecurringProfileRepository.GetOne( recurringID );

                        Log.Debug( " RecurringID : " + recurringID );

                        recurringProfile.RecurringStatus = SystemConst.RecurringStatus.Ongoing.ToString();
                        recurringProfile.UpdateTime = DateTime.Now;
                        DataAccessContext.RecurringProfileRepository.Save( recurringProfile );

                        bool isAllRecurringPaymentComplete = true;

                        IList<OrderItem> orderItemList = DataAccessContext.OrderItemRepository.GetByOrderID( orderID );

                        Log.Debug( " OrderItemList : " + orderItemList.Count );
                        int count = 0;
                        foreach (OrderItem item in orderItemList)
                        {
                            Log.Debug( " Count : " + count );
                            Log.Debug( " item.RecurringID : " + item.RecurringID );
                            if (item.RecurringID != "0")
                            {
                                recurringProfile = DataAccessContext.RecurringProfileRepository.GetOne( item.RecurringID );

                                Log.Debug( " RecurringProfile.RecurringStatus : " + recurringProfile.RecurringStatus );

                                if (recurringProfile.RecurringStatus != SystemConst.RecurringStatus.Ongoing.ToString() &&
                                    recurringProfile.RecurringStatus != SystemConst.RecurringStatus.Expired.ToString())
                                {
                                    Log.Debug( "RecurringStatus != Ongoing && RecurringStatus != Expired" );
                                    isAllRecurringPaymentComplete = false;
                                    Log.Debug( " Break;" );
                                    break;
                                }
                            }
                            count++;
                        }

                        Log.Debug( "isAllRecurringPaymentComplete :" + isAllRecurringPaymentComplete );

                        if (isAllRecurringPaymentComplete)
                        {
                            Log.Debug( " Enter AllRecurringPaymentComplete" );
                            Order orderDetails = DataAccessContext.OrderRepository.GetOne( orderID );

                            orderDetails.PaymentComplete = true;
                            DataAccessContext.OrderRepository.Save( orderDetails );
                            Log.Debug( " Exit AllRecurringPaymentComplete" );
                        }
                        Log.Debug( " End AllSequence == 1" );
                    }

                    if (RecurringStatus == SystemConst.RecurringStatus.Expired.ToString())
                    {
                        Log.Debug( " Enter Expired Recurring Status" );

                        string recurringID =
                            DataAccessContext.RecurringProfileRepository.GetRecurringIDFromReferenceID(
                                RecurringReferenceID );
                        RecurringProfile recurringProfile =
                            DataAccessContext.RecurringProfileRepository.GetOne( recurringID );
                        recurringProfile.RecurringStatus = SystemConst.RecurringStatus.Expired.ToString();
                        recurringProfile.UpdateTime = DateTime.Now;
                        DataAccessContext.RecurringProfileRepository.Save( recurringProfile );

                        Log.Debug( " RecurringStatus : " + recurringProfile.RecurringStatus );
                        Log.Debug( " UpdateTime : " + DateTime.Now.ToString() );
                        Log.Debug( " End Expired Recurring Status" );
                    }


                }
                else
                {
                    PaymentLog paymentLog = new PaymentLog();
                    paymentLog.OrderID = "0";
                    paymentLog.PaymentResponse = Request.Form.ToString();
                    paymentLog.PaymentGateway = GetPaymentName();
                    paymentLog.PaymentType = "PayPalRecurringPaymentFailedByNullOrderID";
                    DataAccessContext.PaymentLogRepository.Save( paymentLog );

                    Log.Debug( "OrderID : " + orderID );
                    Log.Debug( "PaymentResponse : " + Request.Form.ToString() );
                    Log.Debug( "PaymentGateway : " + GetPaymentName() );
                    Log.Debug( "PaymentType : " + paymentLog.PaymentType );
                }
            }
            else
            {
                PaymentLog paymentLog = new PaymentLog();
                paymentLog.OrderID = orderID;
                paymentLog.PaymentResponse = Request.Form.ToString();
                paymentLog.PaymentGateway = GetPaymentName();
                paymentLog.PaymentType = "PayPalRecurringPaymentNotCompleted";
                DataAccessContext.PaymentLogRepository.Save( paymentLog );

                Log.Debug( "OrderID : " + orderID );
                Log.Debug( "PaymentResponse : " + Request.Form.ToString() );
                Log.Debug( "PaymentGateway : " + GetPaymentName() );
                Log.Debug( "PaymentType : " + paymentLog.PaymentType );
            }
            Log.Debug( " ----- End ProcessRecurringPayment() ----- " );

        }
        catch (Exception ex)
        {
            Log.Debug( "    ***** Start ProcessRecurringPayment() Exception ***** " );
            PaymentLog paymentLog = new PaymentLog();
            paymentLog.OrderID = "0";
            paymentLog.PaymentResponse = Request.Form.ToString() + "&Exception:" + ex.Message;
            paymentLog.PaymentGateway = GetPaymentName();
            paymentLog.PaymentType = "PayPalRecurringPaymentFailed";
            DataAccessContext.PaymentLogRepository.Save( paymentLog );

            Log.Debug( " Error : " + ex );
            Log.Debug( "    ***** End ProcessRecurringPayment() Exception ***** " );
            Log.Debug( " ----- End ProcessRecurringPayment() ----- " );
        }
    }

    private void ProcessRecurringPaymentExpired()
    {
        Log.Debug( " --------------------------- Enter ProcessRecurringPaymentExpired() -------------------------------" );

        String orderID = "";

        Log.Debug( " RecurringReferenceID : " + RecurringReferenceID );
        Log.Debug( " RecurringAmount : " + RecurringAmount );
        Log.Debug( " RecurringStatus : " + RecurringStatus );
        Log.Debug( " PaymentStatus : " + PaymentStatus );
        Log.Debug( " ReceiptID : " + ReceiptID );
        Log.Debug( " PeriodType : " + PeriodType );

        PaymentLog paymentLog = new PaymentLog();
        paymentLog.OrderID = orderID;
        paymentLog.PaymentResponse = Request.Form.ToString();
        paymentLog.PaymentGateway = GetPaymentName();
        paymentLog.PaymentType = "PayPalRecurringExpired";
        DataAccessContext.PaymentLogRepository.Save( paymentLog );

        Log.Debug( " --------------------------- End ProcessRecurringPaymentExpired() -------------------------------" );
    }

    private bool IsPayPalStandard()
    {
        if (Invoice != null && Invoice != String.Empty)
            return true;
        else
            return false;
    }

    private string GetPayPalVerificationUrl()
    {
        Log.Debug( "     -----Start GetPayPalVerificationUrl()-----" );
        Log.Debug( "          GetPaymentName():  " + GetPaymentName() );
        Log.Debug( "     -----End GetPayPalVerificationUrl()-----" );
        if (GetPaymentName() == "PayPal")
        {
            // PayPal Website Payment Standard
            if (DataAccessContext.Configurations.GetBoolValue( "PaymentByPayPalEnvironment" ))
            {
                return "https://www.sandbox.paypal.com/cgi-bin/webscr";
            }
            else
            {
                return "https://www.paypal.com/cgi-bin/webscr";
            }
        }
        else
        {
            // PayPal Website Payment Pro US (for recurring)
            if (DataAccessContext.Configurations.GetValue( "PayPalEnvironment" ) == "sandbox" ||
                DataAccessContext.Configurations.GetValue( "PayPalEnvironment" ) == "beta-sandbox")
            {
                return "https://www.sandbox.paypal.com/cgi-bin/webscr";
            }
            else
            {
                return "https://www.paypal.com/cgi-bin/webscr";
            }
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        string strResponse = String.Empty;
        Log.Debug( "Log Time:" + DateTime.Now.ToString() );
        try
        {
            Log.Debug( "------ Enter Page_Load ------" );

            HttpService httpService = new HttpService();
            strResponse = httpService.PostData(
                GetPayPalVerificationUrl(), Request.Form.ToString() + "&cmd=_notify-validate" );

            Log.Debug( " strResponse : " + strResponse );
            Log.Debug( " TxnType : " + TxnType );

            // Confirm whether the IPN was VERIFIED or INVALID. If INVALID, just ignore the IPN
            if (strResponse == "VERIFIED")
            {
                if (TxnType == "recurring_payment_profile_created")
                {
                    Log.Debug( " ++++++++++ Access ProcessRecurringPaymentProfileCreated(); ++++++++++ " );
                    ProcessRecurringPaymentProfileCreated();
                    Log.Debug( " ++++++++++ Access ProcessRecurringPaymentProfileCreated(); Finished ++++++++++ " );
                }
                else if (TxnType == "recurring_payment")
                {
                    Log.Debug( " ++++++++++ Access ProcessRecurringPayment(); ++++++++++ " );
                    ProcessRecurringPayment();
                    Log.Debug( " ++++++++++ Access ProcessRecurringPayment(); Finished ++++++++++ " );
                }
                else if (TxnType == "recurring_payment_expired")
                {
                    Log.Debug( " ++++++++++ Access ProcessRecurringPaymentExpired(); ++++++++++ " );
                    ProcessRecurringPaymentExpired();
                    Log.Debug( " ++++++++++ Access ProcessRecurringPaymentExpired(); Finished ++++++++++ " );
                }
                else if (TxnType == "recurring_payment_failed")
                {
                    Log.Debug( " ++++++++++ Access recurring_payment_failed ++++++++++ " );
                    PaymentLog paymentLog = new PaymentLog();
                    paymentLog.OrderID = String.Empty;
                    paymentLog.PaymentResponse = Request.Form.ToString();
                    paymentLog.PaymentGateway = GetPaymentName();
                    paymentLog.PaymentType = "PayPalRecurringFailed";
                    DataAccessContext.PaymentLogRepository.Save( paymentLog );
                    Log.Debug( " ++++++++++ Access recurring_payment_failed Finished ++++++++++ " );
                }
                else if (TxnType == "web_accept")
                {
                    if (IsPayPalStandard())
                    {
                        PaymentLog paymentLog = new PaymentLog();
                        paymentLog.OrderID = Invoice;
                        paymentLog.PaymentResponse = Request.Form.ToString();
                        paymentLog.PaymentGateway = GetPaymentName();
                        paymentLog.PaymentType = "PayPalStandard";
                        DataAccessContext.PaymentLogRepository.Save( paymentLog );
                        ProcessPayPalStandardIPN();
                    }
                    else
                    {
                        PaymentLog paymentLog = new PaymentLog();
                        paymentLog.OrderID = String.Empty;
                        paymentLog.PaymentResponse = Request.Form.ToString();
                        paymentLog.PaymentGateway = String.Empty;
                        paymentLog.PaymentType = TxnType;
                        DataAccessContext.PaymentLogRepository.Save( paymentLog );
                    }
                }
                else
                {
                    PaymentLog paymentLog = new PaymentLog();
                    paymentLog.OrderID = String.Empty;
                    paymentLog.PaymentResponse = Request.Form.ToString();
                    paymentLog.PaymentGateway = "Unknow Payment Type";
                    paymentLog.PaymentType = TxnType;
                    DataAccessContext.PaymentLogRepository.Save( paymentLog );
                }
            }
            else
            {
                // PayPal IPN is inavlid (not receive "VERIFIED" from PayPal)
                PaymentLog paymentLog = new PaymentLog();
                paymentLog.OrderID = "0";
                paymentLog.PaymentResponse = "Response: " + strResponse + "\n" + Request.Form.ToString();
                paymentLog.PaymentGateway = GetPaymentName();
                paymentLog.PaymentType = "PayPalVerifyFailed";
                DataAccessContext.PaymentLogRepository.Save( paymentLog );
            }
            Log.Debug( "------ End Page_Load ------" );
        }
        catch (Exception ex)
        {
            Log.Debug( "-------- Enter Page_Load Exception --------" );
            Log.Debug( "            Exception : " + ex );

            string invoice = String.Empty;
            if (String.IsNullOrEmpty( Invoice ))
                invoice = Invoice;
            else
                invoice = "0";
            PaymentLog paymentLog = new PaymentLog();
            paymentLog.OrderID = invoice;
            paymentLog.PaymentResponse = "Response: " + strResponse + "\n" + "Exception=" + ex.Message;
            paymentLog.PaymentGateway = GetPaymentName();
            paymentLog.PaymentType = "PayPalFailed";
            DataAccessContext.PaymentLogRepository.Save( paymentLog );

            Log.Debug( "-------- End Page_Load Exception --------" );
            Log.Debug( "------ End Page_Load ------" );
        }
    }
}

