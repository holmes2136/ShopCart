using System;
using System.IO;
using System.Xml;
using GCheckout.AutoGen;
using GCheckout.Util;
using Vevo.Base.Domain;
using Vevo.Deluxe.Domain.Marketing;
//using GCheckout.OrderProcessing;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Payments;
using Vevo.Domain.Payments.Google;
using Vevo.Domain.Products;
using Vevo.Domain.Shipping;
using Vevo.Domain.Shipping.Custom;
using Vevo.Domain.Stores;
using Vevo.Domain.Users;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.Deluxe.WebUI.Marketing;
using Vevo.WebUI.Orders;
using Vevo.Deluxe.Domain.Orders;

public partial class GoogleNotification : System.Web.UI.Page
{
    #region Private

    private string _serialNumber = String.Empty;

    private string GetPaymentName()
    {
        PaymentMethod payment;
        payment = new GoogleCheckoutPaymentMethod();

        return payment.Name;
    }

    private string ConvertToString( string source )
    {
        string result;
        if (!String.IsNullOrEmpty( source ))
        {
            result = source;
        }
        else
        {
            result = "";
        }
        return result;
    }

    private void SendChargeFailureEmail( Order order )
    {
        WebUtilities.SendMail(
            NamedConfig.AutoSenderEmail,
            DataAccessContext.Configurations.GetValue( "ErrorLogEmail" ),
            "Failed charging order ID " + order.OrderID,
            "There is an error while trying to charge credit card.\n" +
            "    Shopping Cart Order ID: " + order.OrderID + "\n" +
            "    Google Order ID: " + order.GatewayOrderID + "\n" +
            "\nPlease verify this order in your Google Checkout Merchant account.\n" );
    }

    private string GetGiftCertificateCode( object[] obj )
    {
        if (obj == null)
            return String.Empty;

        foreach (object o in obj)
        {
            if (o is GiftCertificateAdjustment)
            {
                GiftCertificateAdjustment result = (GiftCertificateAdjustment) o;
                return result.code;
            }
        }

        return String.Empty;
    }

    private string GetCouponCode( object[] obj )
    {
        if (obj == null)
            return String.Empty;

        foreach (object o in obj)
        {
            if (o is CouponAdjustment)
            {
                CouponAdjustment result = (CouponAdjustment) o;
                return result.code;
            }
        }

        return String.Empty;
    }

    private void BuildShoppingCart( string requestXml, NewOrderNotification notification )
    {
        foreach (Item item in notification.shoppingcart.items)
        {
            XmlNode[] node = item.merchantprivateitemdata.Any;

            Product product = DataAccessContext.ProductRepository.GetOne(
                StoreContext.Culture,
                item.merchantitemid,
                new StoreRetriever().GetCurrentStoreID()
                );

            if (node.Length <= 1)
            {
                StoreContext.ShoppingCart.AddItem(
                    product, item.quantity );
            }
            else
            {
                // Creating option item from google checkout is not details of option type is text.
                OptionItemValueCollection optionCollection = OptionItemValueCollection.Null;
                if (!String.IsNullOrEmpty( node[1].InnerText ))
                    optionCollection = new OptionItemValueCollection(
                        StoreContext.Culture, node[1].InnerText, item.merchantitemid );

                StoreContext.ShoppingCart.AddItem(
                    product, item.quantity, optionCollection );
            }
        }

        Log.Debug( "SetShippingDetails" );
        Vevo.Base.Domain.Address address = new Vevo.Base.Domain.Address(
            notification.buyershippingaddress.contactname,
            "",
            notification.buyerbillingaddress.companyname,
            notification.buyershippingaddress.address1,
            notification.buyershippingaddress.address2,
            notification.buyershippingaddress.city,
            notification.buyershippingaddress.region,
            notification.buyershippingaddress.postalcode,
            notification.buyershippingaddress.countrycode,
            notification.buyerbillingaddress.phone,
            notification.buyerbillingaddress.fax );

        StoreContext.CheckoutDetails.ShippingAddress = new ShippingAddress( address, false );

        Log.Debug( "Set Shipping " );
        MerchantCalculatedShippingAdjustment shippingAdjust = (MerchantCalculatedShippingAdjustment) notification.orderadjustment.shipping.Item;

        string shippingID = DataAccessContext.ShippingOptionRepository.GetIDFromName( shippingAdjust.shippingname );
        ShippingMethod shipping = ShippingFactory.CreateShipping( shippingID );
        shipping.Name = shippingAdjust.shippingname;
        StoreContext.CheckoutDetails.SetShipping( shipping );
        StoreContext.CheckoutDetails.SetCustomerComments( "" );

        PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne(
            StoreContext.Culture, PaymentOption.GoogleCheckout );
        StoreContext.CheckoutDetails.SetPaymentMethod(
            paymentOption.CreatePaymentMethod() );

        Log.Debug( "Set Coupon ID" );
        StoreContext.CheckoutDetails.SetCouponID( GetCouponCode( notification.orderadjustment.merchantcodes.Items ) );

        Log.Debug( "Set Gift" );
        string giftID = GetGiftCertificateCode( notification.orderadjustment.merchantcodes.Items );

        if (giftID != String.Empty)
            StoreContext.CheckoutDetails.SetGiftCertificate( giftID );

        StoreContext.CheckoutDetails.SetGiftRegistryID( "0" );
        StoreContext.CheckoutDetails.SetShowShippingAddress( true );
    }

    private void PaymentLogUpdateNewOrderNotification( NewOrderNotification notification )
    {
        string paymentResponse = "new-order-notification :: ";
        paymentResponse += String.Format( "Buyerbillingaddress: {0},", notification.buyerbillingaddress );
        paymentResponse += String.Format( "BuyerID: {0},", notification.buyerid );
        paymentResponse += String.Format( "Buyermarketingpreferences: {0},",
            notification.buyermarketingpreferences );
        paymentResponse += String.Format( "Buyershippingaddress: {0},", notification.buyershippingaddress );
        paymentResponse += String.Format( "Financialorderstate: {0},", notification.financialorderstate );
        paymentResponse += String.Format( "Fulfillmentorderstate: {0},", notification.fulfillmentorderstate );
        paymentResponse += String.Format( "Orderadjustment: {0},", notification.orderadjustment );
        paymentResponse += String.Format( "Ordertotal: {0},", notification.ordertotal );
        paymentResponse += String.Format( "Serialnumber: {0},", notification.serialnumber );
        paymentResponse += String.Format( "ShoppingCart: {0},", notification.shoppingcart );
        paymentResponse += String.Format( "Timestamp: {0},", notification.timestamp );
        Log.Debug( "Start Save Payment Log" );

        PaymentLog paymentLog = new PaymentLog();
        paymentLog.OrderID = notification.googleordernumber;
        paymentLog.PaymentResponse = paymentResponse;
        paymentLog.PaymentGateway = GetPaymentName();
        paymentLog.PaymentType = String.Empty;
        DataAccessContext.PaymentLogRepository.Save( paymentLog );
        Log.Debug( "End Save Payment Log" );
    }

    private void PaymentLogUpdateRiskInformation( RiskInformationNotification notification )
    {
        string paymentResponse = "risk-information-notification :: ";
        paymentResponse += "AvsResponse: " + notification.riskinformation.avsresponse + ",";
        paymentResponse += "BuyerAccountage: " + notification.riskinformation.buyeraccountage.ToString() + ",";
        paymentResponse += "CvnResponse: " + notification.riskinformation.cvnresponse + ",";
        paymentResponse += "EligibleForProtection: " + notification.riskinformation.eligibleforprotection + ",";
        paymentResponse += "IP Address: " + notification.riskinformation.ipaddress + ",";
        paymentResponse += "PartialNumber: " + notification.riskinformation.partialccnumber;

        Log.Debug( "Start Create Log" );
        string orderID = DataAccessContext.OrderRepository.GetOrderIDByGatewayID( notification.googleordernumber );

        PaymentLog paymentLog = new PaymentLog();
        paymentLog.OrderID = orderID;
        paymentLog.PaymentResponse = paymentResponse;
        paymentLog.PaymentGateway = GetPaymentName();
        paymentLog.PaymentType = String.Empty;
        DataAccessContext.PaymentLogRepository.Save( paymentLog );
        Log.Debug( "End Create Log" );
    }

    private void VerifyAvsAndCvv( RiskInformationNotification notification )
    {
        string orderID
            = DataAccessContext.OrderRepository.GetOrderIDByGatewayID( notification.googleordernumber );
        Order order = DataAccessContext.OrderRepository.GetOne( orderID );

        string avs = notification.riskinformation.avsresponse.ToUpper();
        string cvv = notification.riskinformation.cvnresponse.ToUpper();

        switch (avs)
        {
            case "Y":
                {
                    order.AvsZipStatus = "Pass";
                    order.AvsAddrStatus = "Pass";
                    break;
                }
            case "P":
                {
                    order.AvsZipStatus = "Pass";
                    order.AvsAddrStatus = "Fail";
                    break;
                }
            case "A":
                {
                    order.AvsZipStatus = "Fail";
                    order.AvsAddrStatus = "Pass";
                    break;
                }
            case "N":
                {
                    order.AvsZipStatus = "Fail";
                    order.AvsAddrStatus = "Fail";
                    break;
                }
            default:
                {
                    order.AvsZipStatus = "Unavailable";
                    order.AvsAddrStatus = "Unavailable";
                    break;
                }
        }


        switch (cvv)
        {
            case "M":
                {
                    order.CvvStatus = "Pass";
                    break;
                }
            case "N":
                {
                    order.CvvStatus = "Fail";
                    break;
                }
            case "E":
                {
                    order.CvvStatus = "Fail";
                    break;
                }
            default:
                {
                    order.CvvStatus = "Unavailable";
                    break;
                }
        }

        DataAccessContext.OrderRepository.Save( order );
    }

    private void PaymentLogUpdateOrderStateChange( OrderStateChangeNotification notification )
    {
        string paymentResponse = "order-state-change-notification :: ";
        paymentResponse += "NewFinancialOrderState :" + notification.newfinancialorderstate.ToString() + ",";
        paymentResponse += "NewFulFillmentOrderState :" + notification.newfulfillmentorderstate.ToString() + ",";
        paymentResponse += "PreviousFinancialOrderState :" +
            notification.previousfinancialorderstate.ToString() + ",";
        paymentResponse += "PreviousFulFillmentOrderState :" +
            notification.previousfulfillmentorderstate.ToString() + ",";
        paymentResponse += "Reason :" + notification.reason + ",";
        paymentResponse += "SerialNumber :" + notification.serialnumber + ",";
        paymentResponse += "TimeStamp :" + notification.timestamp.ToString();
        Log.Debug( "-*************** Check PaymentLog **************-" );
        Log.Debug( "notification.googleordernumber = " + notification.googleordernumber );
        Log.Debug( "paymentResponse = " + paymentResponse );
        Log.Debug( "GetPaymentName = " + GetPaymentName() );

        string orderID = DataAccessContext.OrderRepository.GetOrderIDByGatewayID( notification.googleordernumber );

        PaymentLog paymentLog = new PaymentLog();
        paymentLog.OrderID = orderID;
        paymentLog.PaymentResponse = paymentResponse;
        paymentLog.PaymentGateway = GetPaymentName();
        paymentLog.PaymentType = String.Empty;
        DataAccessContext.PaymentLogRepository.Save( paymentLog );
        Log.Debug( "-************* End Check PaymentLog ************-" );
    }

    private void PaymentLogUpdateRefundAmount( RefundAmountNotification notification )
    {
        string paymentResponse = "refund-amount-notification :: ";
        paymentResponse += "latestrefundamount :" + notification.latestrefundamount.Value.ToString() + ",";
        paymentResponse += "serialnumber :" + notification.serialnumber + ",";
        paymentResponse += "timestamp :" + notification.timestamp.ToString() + ",";
        paymentResponse += "totalrefundamount :" + notification.totalrefundamount.Value.ToString();

        string orderID = DataAccessContext.OrderRepository.GetOrderIDByGatewayID( notification.googleordernumber );

        PaymentLog paymentLog = new PaymentLog();
        paymentLog.OrderID = orderID;
        paymentLog.PaymentResponse = paymentResponse;
        paymentLog.PaymentGateway = GetPaymentName();
        paymentLog.PaymentType = String.Empty;
        DataAccessContext.PaymentLogRepository.Save( paymentLog );
    }

    private void PaymentLogUpdateChargeback( ChargebackAmountNotification notification )
    {
        string paymentResponse = "chargeback-amount-notification :: ";
        paymentResponse += "latestchargebackamount :" + notification.latestchargebackamount.Value.ToString();

        string orderID = DataAccessContext.OrderRepository.GetOrderIDByGatewayID( notification.googleordernumber );

        PaymentLog paymentLog = new PaymentLog();
        paymentLog.OrderID = orderID;
        paymentLog.PaymentResponse = paymentResponse;
        paymentLog.PaymentGateway = GetPaymentName();
        paymentLog.PaymentType = String.Empty;
        DataAccessContext.PaymentLogRepository.Save( paymentLog );
    }

    private void GoogleChargeOrder( string gatewayOrderID )
    {
        string orderID = DataAccessContext.OrderRepository.GetOrderIDByGatewayID( gatewayOrderID );
        Order order = DataAccessContext.OrderRepository.GetOne( orderID );

        Log.Debug( "Start Auto Charge for Google" );
        GCheckout.OrderProcessing.ChargeOrderRequest Req =
            new GCheckout.OrderProcessing.ChargeOrderRequest(
                WebConfiguration.GoogleMerchantID,
                WebConfiguration.GoogleMerchantKey,
                WebConfiguration.GoogleEnvironment,
                order.GatewayOrderID,
                DataAccessContext.Configurations.GetValue( "PaymentCurrency" ),
                order.Total );

        GCheckoutResponse R = Req.Send();

        if (!R.IsGood)
        {
            Log.Debug( "Can not charge - Response: " + R.ResponseXml + "\n" );
            Log.Debug( "Can not charge - RedirectUrl: " + R.RedirectUrl + "\n" );
            Log.Debug( "Can not charge - IsGood: " + R.IsGood + "\n" );
            Log.Debug( "Can not charge - ErrorMessage: " + R.ErrorMessage + "\n" );

            SendChargeFailureEmail( order );
        }
        Log.Debug( "End Auto Charge for Google" );
    }

    private void PaymentLogChargeAmountUpdate( ChargeAmountNotification N4 )
    {
        string paymentResponse = "charge-amount-notification :: ";
        paymentResponse += "LatestChargeAmount :" + N4.latestchargeamount.Value.ToString() + ",";
        paymentResponse += "SerialNumber :" + N4.serialnumber + ",";
        paymentResponse += "TimeStamp :" + N4.timestamp.ToString() + ",";
        paymentResponse += "TotalChargeAmount :" + N4.totalchargeamount.Value.ToString();

        string orderID = DataAccessContext.OrderRepository.GetOrderIDByGatewayID( N4.googleordernumber );

        PaymentLog paymentLog = new PaymentLog();
        paymentLog.OrderID = orderID;
        paymentLog.PaymentResponse = paymentResponse;
        paymentLog.PaymentGateway = GetPaymentName();
        paymentLog.PaymentType = String.Empty;
        DataAccessContext.PaymentLogRepository.Save( paymentLog );
    }

    private void SendErrorEmail( OrderStateChangeNotification notification )
    {
        string orderID = DataAccessContext.OrderRepository.GetOrderIDByGatewayID( notification.googleordernumber );
        Order order = DataAccessContext.OrderRepository.GetOne( orderID );

        WebUtilities.SendMail(
            NamedConfig.AutoSenderEmail,
            DataAccessContext.Configurations.GetValue( "ErrorLogEmail" ),
            "Failed processing order ID " + order.OrderID,
            "There is an error while processing this order.\n" +
            "    Shopping Cart Order ID: " + order.OrderID + "\n" +
            "    Google Order ID: " + order.GatewayOrderID + "\n" +
            "    Financial Order State: " + notification.newfinancialorderstate.ToString() + "\n" +
            "    Fulfillment Order State: " + notification.newfulfillmentorderstate.ToString() + "\n" +
            "\nPlease verify this order in your Google Checkout Merchant account.\n" );
    }

    private Vevo.Base.Domain.Address CreateBuyerBillingAddress( NewOrderNotification n1 )
    {
        return new Vevo.Base.Domain.Address(
            ConvertToString( n1.buyerbillingaddress.contactname ),
            " ",
            String.Empty,
            ConvertToString( n1.buyerbillingaddress.address1 ),
            ConvertToString( n1.buyerbillingaddress.address2 ),
            ConvertToString( n1.buyerbillingaddress.city ),
            ConvertToString( n1.buyerbillingaddress.region ),
            ConvertToString( n1.buyerbillingaddress.postalcode ),
            ConvertToString( n1.buyerbillingaddress.countrycode ),
            String.Empty,
            String.Empty );

    }

    #endregion


    #region Protected

    protected string SerialNumber
    {
        get { return _serialNumber; }
    }


    protected void Page_PreInit( object sender, EventArgs e )
    {
        Page.Theme = String.Empty;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        // Extract the XML from the request.
        Stream RequestStream = Request.InputStream;
        StreamReader RequestStreamReader = new StreamReader( RequestStream );
        string RequestXml = RequestStreamReader.ReadToEnd();
        RequestStream.Close();
        Log.Debug( "Request XML:\n" + RequestXml );

        string gatewayOrderID = "";
        string orderID;
        OrderNotifyService orderBusiness;
        try
        {
            // Act on the XML.
            switch (EncodeHelper.GetTopElement( RequestXml ))
            {
                case "new-order-notification":
                    Log.Debug( "Start new-order-notification" );
                    NewOrderNotification N1 =
                        (NewOrderNotification) EncodeHelper.Deserialize( RequestXml,
                        typeof( NewOrderNotification ) );
                    string OrderNumber1 = N1.googleordernumber;

                    PaymentLogUpdateNewOrderNotification( N1 );
                    _serialNumber = N1.serialnumber;
                    Log.Debug( "-********************- Check DataAccessContext.GetOrderIDByGateWayID Data -**********************-" );
                    Log.Debug( "GetOrderIDByGateWayID ( "
                        + OrderNumber1 + " ) = " + DataAccessContext.OrderRepository.GetOrderIDByGatewayID( OrderNumber1 ) );
                    Log.Debug( "-********************- END Check DataAccessContext.GetOrderIDByGateWayID Data -**********************-" );
                    if (DataAccessContext.OrderRepository.GetOrderIDByGatewayID( OrderNumber1 ) == "0")
                    {
                        BuildShoppingCart( RequestXml, N1 );

                        Log.Debug( "Start converting to order" );

                        OrderCreateService orderCreateService = new OrderCreateService(
                            StoreContext.ShoppingCart,
                            StoreContext.CheckoutDetails,
                            StoreContext.Culture,
                            StoreContext.Currency,
                            AffiliateHelper.GetAffiliateCode(),
                            WebUtilities.GetVisitorIP() );
                        
                        string storeID = EncodeHelper.GetElementValue( RequestXml, "StoreID" );
                        DataAccessContext.SetStoreRetriever( new StoreRetriever( storeID ) );

                        OrderAmount orderAmount = orderCreateService.GetOrderAmount( Customer.Null )
                            .Add( CartItemPromotion.CalculatePromotionShippingAndTax(
                            StoreContext.CheckoutDetails,
                            StoreContext.ShoppingCart.SeparateCartItemGroups(),
                            Customer.Null ) );

                        Order order = orderCreateService.PlaceOrderAnonymous(orderAmount,
                            SystemConst.UnknownUser,
                            CreateBuyerBillingAddress( N1 ),
                            ConvertToString( N1.buyerbillingaddress.email ), DataAccessContext.StoreRetriever, StoreContext.Culture );

                        AffiliateOrder affiliateorder = new AffiliateOrder();
                        affiliateorder.AffiliateCode = AffiliateHelper.GetAffiliateCode();
                        affiliateorder.CreateAffiliateOrder( order.OrderID, orderAmount.Subtotal, orderAmount.Discount );

                        orderBusiness = new OrderNotifyService( order.OrderID );

                        Log.Debug( "End converting to order" );

                        Log.Debug( "Start sending order email" );
                        orderBusiness.SendOrderEmail();
                        Log.Debug( "End sending order email" );

                        Order orderDetail = DataAccessContext.OrderRepository.GetOne( order.OrderID );
                        orderDetail.GatewayOrderID = OrderNumber1;
                        Log.Debug( "OrderDetail.GatewayOrderID = " + OrderNumber1 );
                        Log.Debug( "Start Save Order Detail" );
                        DataAccessContext.OrderRepository.Save( orderDetail );
                        Log.Debug( "End Save Order Detail" );

                        DataAccessContext.SetStoreRetriever( new StoreRetriever() );
                    }
                    else
                    {
                        Order orderDetail = DataAccessContext.OrderRepository.GetOne(
                            DataAccessContext.OrderRepository.GetOrderIDByGatewayID( OrderNumber1 ) );

                        Log.Debug( "-**************************- start Check Error -**************************-" );
                        Log.Debug( "N1.googleOrderNumber = " + N1.googleordernumber );
                        Log.Debug( "OrderNumber1 = " + OrderNumber1 );
                        Log.Debug( "N1.buyerbillingaddress.contactname = " + ConvertToString( N1.buyerbillingaddress.contactname ) );
                        Log.Debug( "N1.buyerbillingaddress.address1 = " + ConvertToString( N1.buyerbillingaddress.address1 ) );
                        Log.Debug( "N1.buyerbillingaddress.city = " + ConvertToString( N1.buyerbillingaddress.city ) );
                        Log.Debug( "N1.buyerbillingaddress.region = " + ConvertToString( N1.buyerbillingaddress.contactname ) );
                        Log.Debug( "N1.buyerbillingaddress.postalcode = " + ConvertToString( N1.buyerbillingaddress.postalcode ) );
                        Log.Debug( "orderDetail.Billing.Company = " + orderDetail.Billing.Company );
                        Log.Debug( "orderDetail.Billing.Country = " + orderDetail.Billing.Country );
                        Log.Debug( "orderDetail.Billing.Phone = " + orderDetail.Billing.Phone );
                        Log.Debug( "orderDetail.Billing.Fax = " + orderDetail.Billing.Fax );
                        Log.Debug( "-**************************- End Check Error -**************************-" );

                        orderDetail.Billing = new Vevo.Base.Domain.Address( ConvertToString( N1.buyerbillingaddress.contactname ),
                            String.Empty, orderDetail.Billing.Company,
                            ConvertToString( N1.buyerbillingaddress.address1 ),
                            ConvertToString( N1.buyerbillingaddress.address2 ),
                            ConvertToString( N1.buyerbillingaddress.city ),
                            ConvertToString( N1.buyerbillingaddress.region ),
                            ConvertToString( N1.buyerbillingaddress.postalcode ),
                            orderDetail.Billing.Country, orderDetail.Billing.Phone,
                            orderDetail.Billing.Fax );
                        orderDetail.Email = ConvertToString( N1.buyerbillingaddress.email );

                        DataAccessContext.OrderRepository.Save( orderDetail );
                    }

                    Log.Debug( "End new-order-notification" );
                    break;

                case "risk-information-notification":
                    Log.Debug( "risk-information-notification" );
                    RiskInformationNotification N2 = (RiskInformationNotification) EncodeHelper.Deserialize(
                        RequestXml, typeof( RiskInformationNotification ) );
                    // This notification tells us that Google has authorized the order 
                    // and it has passed the fraud check.
                    // Use the data below to determine if you want to accept the order, then start processing it.
                    gatewayOrderID = N2.googleordernumber;
                    _serialNumber = N2.serialnumber;

                    PaymentLogUpdateRiskInformation( N2 );
                    VerifyAvsAndCvv( N2 );
                    break;

                case "order-state-change-notification":
                    Log.Debug( "Start order-state-change-notification" );
                    OrderStateChangeNotification N3 = (OrderStateChangeNotification) EncodeHelper.Deserialize(
                        RequestXml, typeof( OrderStateChangeNotification ) );

                    _serialNumber = N3.serialnumber;

                    PaymentLogUpdateOrderStateChange( N3 );

                    if (N3.newfinancialorderstate != N3.previousfinancialorderstate)
                    {
                        Order orderDetail = DataAccessContext.OrderRepository.GetOne(
                            DataAccessContext.OrderRepository.GetOrderIDByGatewayID( N3.googleordernumber ) );
                        orderDetail.GatewayPaymentStatus = N3.newfinancialorderstate.ToString();

                        DataAccessContext.OrderRepository.Save( orderDetail );

                        switch (N3.newfinancialorderstate)
                        {
                            case FinancialOrderState.PAYMENT_DECLINED:
                            case FinancialOrderState.CANCELLED_BY_GOOGLE:
                                SendErrorEmail( N3 );
                                break;

                            case FinancialOrderState.CHARGEABLE:
                                if (DataAccessContext.Configurations.GetBoolValueNoThrow( "GCheckoutChargeAuto" ))
                                {
                                    GoogleChargeOrder( N3.googleordernumber );
                                }
                                break;
                        }
                    }

                    Log.Debug( "End order-state-change-notification" );
                    break;

                case "charge-amount-notification":
                    Log.Debug( "Start charge-amount-notification" );
                    ChargeAmountNotification N4 = (ChargeAmountNotification) EncodeHelper.Deserialize( RequestXml,
                        typeof( ChargeAmountNotification ) );
                    // Google has successfully charged the customer's credit card.
                    gatewayOrderID = N4.googleordernumber;
                    _serialNumber = N4.serialnumber;

                    PaymentLogChargeAmountUpdate( N4 );

                    orderID = DataAccessContext.OrderRepository.GetOrderIDByGatewayID( gatewayOrderID );
                    orderBusiness = new OrderNotifyService( orderID );
                    orderBusiness.ProcessPaymentComplete();
                    Log.Debug( "End charge-amount-notification" );
                    break;

                case "refund-amount-notification":
                    Log.Debug( "Start refund-amount-notification" );

                    RefundAmountNotification N5 =
                        (RefundAmountNotification) EncodeHelper.Deserialize(
                        RequestXml,
                        typeof( RefundAmountNotification ) );
                    // Google has successfully refunded the customer's credit card.
                    gatewayOrderID = N5.googleordernumber;
                    _serialNumber = N5.serialnumber;
                    //decimal RefundedAmount = N5.latestrefundamount.Value;

                    PaymentLogUpdateRefundAmount( N5 );
                    Order orderDetails = DataAccessContext.OrderRepository.GetOne(
                        DataAccessContext.OrderRepository.GetOrderIDByGatewayID( gatewayOrderID ) );
                    orderDetails.PaymentComplete = false;
                    DataAccessContext.OrderRepository.Save( orderDetails );

                    Log.Debug( "End refund-amount-notification" );
                    break;

                case "chargeback-amount-notification":
                    Log.Debug( "Start chargeback-amount-notification" );

                    ChargebackAmountNotification N6 = (ChargebackAmountNotification) EncodeHelper.Deserialize(
                        RequestXml, typeof( ChargebackAmountNotification ) );
                    // A customer initiated a chargeback with his credit card company to get her money back.
                    gatewayOrderID = N6.googleordernumber;
                    _serialNumber = N6.serialnumber;
                    decimal ChargebackAmount = N6.latestchargebackamount.Value;

                    PaymentLogUpdateChargeback( N6 );

                    orderDetails = DataAccessContext.OrderRepository.GetOne(
                        DataAccessContext.OrderRepository.GetOrderIDByGatewayID( gatewayOrderID ) );
                    orderDetails.GatewayPaymentStatus = "ChargeBack";
                    DataAccessContext.OrderRepository.Save( orderDetails );

                    Log.Debug( "End chargeback-amount-notification" );
                    break;

                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            DataAccessContext.SetStoreRetriever( new StoreRetriever() );
            Log.Debug( ex.ToString() );
        }
    }

    #endregion


    #region Public Properties

    public override string StyleSheetTheme
    {
        get
        {
            return String.Empty;
        }
    }

    #endregion
}
