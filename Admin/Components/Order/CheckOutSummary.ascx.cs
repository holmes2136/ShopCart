using System;
using System.Collections;
using System.Collections.Generic;
using Vevo;
using Vevo.Deluxe.Domain.CustomerReward;
using Vevo.Deluxe.Domain.GiftRegistry;
using Vevo.Deluxe.Domain.Orders;
using Vevo.Deluxe.WebUI.Marketing;
using Vevo.Domain;
using Vevo.Domain.Discounts;
using Vevo.Domain.Orders;
using Vevo.Domain.Payments;
using Vevo.Domain.Payments.OfflineCreditCard;
using Vevo.Domain.Payments.PayPal;
using Vevo.Domain.Payments.PayPalProUS;
using Vevo.Domain.Payments.TwoCheckout;
using Vevo.Domain.Payments.Zero;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.Shared.DataAccess;
using Vevo.Shared.SystemServices;
using Vevo.Shared.Utilities;
using Vevo.Shared.WebUI;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.WebUI.Orders;

public partial class Admin_Components_Order_CheckOutSummary : AdminAdvancedBaseUserControl
{
    private string SelectedStoreID
    {
        get
        {
            return DataAccessContext.StoreRetriever.GetCurrentStoreID();
        }
    }

    private Store CurrentStore
    {
        get { return DataAccessContext.StoreRepository.GetOne( SelectedStoreID ); }
    }

    public string CurrencyCode
    {
        get
        {
            if (MainContext.QueryString["CurrencyCode"] == null)
                return DataAccessContext.CurrencyRepository.GetOne(
                    DataAccessContext.Configurations.GetValueNoThrow( "DefaultDisplayCurrencyCode", CurrentStore ) ).CurrencyCode;
            else
                return MainContext.QueryString["CurrencyCode"];
        }
    }

    private Currency CurrenntCurrency
    {
        get { return DataAccessContext.CurrencyRepository.GetOne( CurrencyCode ); }
    }

    private void PopulateShippingForm()
    {
        uxShippingForm.DataSource = new object[] { StoreContext.CheckoutDetails };
        uxShippingForm.DataBind();
    }

    private void RefreshGrid()
    {
        uxGridCart.DataSource = StoreContext.ShoppingCart.GetCartItems();
        uxGridCart.DataBind();
    }

    private void PlaceOrder()
    {
        string errorHeader;
        string errorMessage;
        if (VerifyOrderAmount( out errorHeader, out errorMessage ))
        {
            SetGiftCertificate();
            CreateOrderAndRedirect();
        }
        else
        {
            ApplicationError.RedirectToErrorPage( errorHeader, errorMessage );
        }
    }


    private decimal GetCouponDiscount()
    {
        Coupon coupon = StoreContext.CheckoutDetails.Coupon;

        IList<decimal> discountLines;

        return coupon.GetDiscount(
            StoreContext.ShoppingCart.GetAllCartItems(),
            StoreContext.Customer,
            out discountLines );
    }

    private decimal GetDiscountWithoutCoupon()
    {
        QuantityDiscountCalculator quantityDiscountCalculator = new QuantityDiscountCalculator();

        CartItemGroup cartItemGroupWithoutRecurring = new CartItemGroup(
            StoreContext.ShoppingCart.GetCartItemsWithoutRecurring() );

        IList<decimal> discountLines = new List<decimal>();

        return quantityDiscountCalculator.GetDiscount(
            cartItemGroupWithoutRecurring, StoreContext.WholesaleStatus, out discountLines );
    }

    private bool CheckUseInventory( string productID )
    {
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, productID, SelectedStoreID );
        return product.UseInventory;
    }

    private string ConvertToString( object obj )
    {
        if (obj == null)
            return String.Empty;
        else
            return obj.ToString();
    }

    private string GenerateIsEmailOKString( Exception emailEx )
    {
        if (emailEx != null)
            return "&IsEmailOK=False";
        else
            return String.Empty;
    }

    private decimal GetTotalWithoutRecurring( CheckoutDetails checkout )
    {
        OrderCalculator orderCalculator = new OrderCalculator();

        IList<CartItemGroup> cartItemGroups = StoreContext.ShoppingCart.SeparateCartItemGroups();

        decimal total = 0;
        foreach (CartItemGroup cartItemGroup in cartItemGroups)
        {
            if (!cartItemGroup.IsRecurring)
                total += orderCalculator.Calculate(
                    checkout, cartItemGroup, StoreContext.Customer, 0 )
                    .Add( CartItemPromotion.CalculatePromotionShippingAndTax(
                        checkout,
                    cartItemGroup,
                    StoreContext.Customer ) ).Total;
        }

        return total;
    }

    private void ProcessCreditCardPaymentSuccess(
        OrderNotifyService order,
        string gatewayOrderID,
        string log,
        string cvvStatus,
        string AvsAddrStatus,
        string AvsZipStatus
        )
    {
        if (!String.IsNullOrEmpty( log ))
        {
            PaymentLog paymentLog = new PaymentLog();
            paymentLog.OrderID = order.OrderID;
            paymentLog.PaymentResponse = log;
            paymentLog.PaymentGateway = order.PaymentMethod;
            paymentLog.PaymentType = "ProcessCreditCard";
            DataAccessContext.PaymentLogRepository.Save( paymentLog );
        }

        if (!String.IsNullOrEmpty( gatewayOrderID ) ||
            !String.IsNullOrEmpty( cvvStatus ) ||
            !String.IsNullOrEmpty( AvsAddrStatus ) ||
            !String.IsNullOrEmpty( AvsZipStatus ))
        {
            Order orderDetails = DataAccessContext.OrderRepository.GetOne( order.OrderID );
            orderDetails.GatewayOrderID = gatewayOrderID;
            orderDetails.CvvStatus = cvvStatus;
            orderDetails.AvsAddrStatus = AvsAddrStatus;
            orderDetails.AvsZipStatus = AvsZipStatus;
            DataAccessContext.OrderRepository.Save( orderDetails );
        }

        Exception emailEx = order.SendOrderEmailNoThrow();
        StoreError.Instance.Exception = emailEx;

        order.ProcessPaymentComplete();

        StoreContext.ClearCheckoutSession();

        MainContext.RedirectMainControl( "OrdersEdit.ascx", String.Format( "OrderID={0}", order.OrderID ) );
    }

    private void ProcessCreditCardPaymentFailure( string errorMessage )
    {
        CheckoutNotCompletePage.RedirectToPage(
            "Error Message",
            errorMessage,
            "OrderCreatePaymentDetails.ascx",
            String.Format( "StoreID={0}&CurrencyCode={1}", SelectedStoreID, CurrencyCode ),
            "Click here to re-enter payment information",
            String.Format( "Default.aspx#OrderCreatePaymentDetails,StoreID={0}&CurrencyCode={1}&PaymentFailure=true", SelectedStoreID, CurrencyCode )
            );
    }

    private void ProcessOnPayPalProUSPayment( CheckoutDetails checkout )
    {
        PayPalProUSPaymentMethod paypalPayment = (PayPalProUSPaymentMethod) checkout.PaymentMethod;
        bool result = false;
        PaymentAppResult paymentResult;
        RecurringPaymentResult recurringPaymentResult;
        ProcessPaymentService process = ProcessPaymentService.CreateNew( new HttpService(), StoreContext.ShoppingCart );

        result = process.ProcessPayPalProUSPayment(
            StoreContext.GetOrderAmount().Total,
            DataAccessContext.CurrencyRepository.GetOne( DataAccessContext.Configurations.GetValue( "PaymentCurrency" ) ),
            StoreContext.Culture,
            checkout,
            StoreContext.ShoppingCart,
            StoreContext.Customer,
            UrlPath.StorefrontUrl,
            WebUtilities.GetVisitorIP(),
            out paymentResult,
            out recurringPaymentResult );

        if (result)
        {
            OrderCreateService orderCreateService = new OrderCreateService(
                StoreContext.ShoppingCart,
                StoreContext.CheckoutDetails,
                StoreContext.Culture,
                CurrenntCurrency,
                AffiliateHelper.GetAffiliateCode(),
                WebUtilities.GetVisitorIP(),
                recurringPaymentResult
                );

            Order order;
            OrderAmount amount = orderCreateService.GetOrderAmount( StoreContext.Customer )
                .Add( CartItemPromotion.CalculatePromotionShippingAndTax(
                StoreContext.CheckoutDetails,
                StoreContext.ShoppingCart.SeparateCartItemGroups(),
                StoreContext.Customer ) );
            order = orderCreateService.PlaceOrder( amount, StoreContext.Customer, DataAccessContext.StoreRetriever, StoreContext.Culture );
            GiftRegistry.UpdateGiftRegistryQuantity( StoreContext.ShoppingCart, StoreContext.CheckoutDetails );
            CustomerRewardPoint.UpdateRedeemPoint( StoreContext.CheckoutDetails, StoreContext.Customer, order );

            OrderNotifyService orderBusiness = new OrderNotifyService( order.OrderID );

            ProcessCreditCardPaymentSuccess(
                orderBusiness,
                paymentResult.GatewayOrderID,
                paymentResult.PaymentLog,
                paymentResult.CvvStatus,
                paymentResult.AvsAddrStatus,
                paymentResult.AvsZipStatus );


        }
        else
        {
            ProcessCreditCardPaymentFailure( paymentResult.ErrorMessage );
        }
    }

    private void ProcessOfflineCreditCardPayment( CheckoutDetails checkout )
    {
        PaymentAppGateway gateway = new PaymentAppGateway( checkout );
        string postData = gateway.CreateOnWebsitePaymentXml(
            StoreContext.Culture,
            StoreContext.GetOrderAmount().Total,
            "",
            WebUtilities.GetVisitorIP(),
            UrlPath.StorefrontUrl,
            true );

        PaymentAppResult paymentResult = gateway.PostCommand( new HttpService(), postData, UrlPath.StorefrontUrl );

        if (paymentResult.Status == PaymentAppResult.PaymentStatus.OK)
        {
            ProcessOfflinePaymentSuccess( checkout );
        }
        else
        {
            ProcessCreditCardPaymentFailure( paymentResult.ErrorMessage );
        }
    }

    private void ProcessOnWebsitePayment( CheckoutDetails checkout )
    {
        PaymentAppGateway gateway = new PaymentAppGateway( checkout );
        string postData = gateway.CreateOnWebsitePaymentXml(
            StoreContext.Culture,
            StoreContext.GetOrderAmount().Total,
            "",
            WebUtilities.GetVisitorIP(),
            UrlPath.StorefrontUrl,
            true );

        PaymentAppResult paymentResult = gateway.PostCommand( new HttpService(), postData, UrlPath.StorefrontUrl );

        if (paymentResult.Status == PaymentAppResult.PaymentStatus.OK)
        {
            OrderNotifyService order = CreateOrder( checkout );

            ProcessCreditCardPaymentSuccess(
                order,
                paymentResult.GatewayOrderID,
                paymentResult.PaymentLog,
                paymentResult.CvvStatus,
                paymentResult.AvsAddrStatus,
                paymentResult.AvsZipStatus );
        }
        else
        {
            ProcessCreditCardPaymentFailure( paymentResult.ErrorMessage );
        }
    }

    private void ProcessOnWebsiteRequireOrderIDPayment( CheckoutDetails checkout )
    {
        OnWebsiteRequireOrderIDPaymentMethod payment = (OnWebsiteRequireOrderIDPaymentMethod) checkout.PaymentMethod;

        OrderNotifyService order = CreateOrder( checkout );

        PaymentAppGateway gateway = new PaymentAppGateway( checkout );
        string postData = gateway.CreateOnWebsitePaymentXml(
            StoreContext.Culture,
            StoreContext.GetOrderAmount().Total,
            order.OrderID,
            WebUtilities.GetVisitorIP(),
            UrlPath.StorefrontUrl,
            true );

        PaymentAppResult paymentResult = gateway.PostCommand( new HttpService(), postData, UrlPath.StorefrontUrl );

        if (paymentResult.Status == PaymentAppResult.PaymentStatus.OK)
        {
            ProcessCreditCardPaymentSuccess(
                order,
                paymentResult.GatewayOrderID,
                paymentResult.PaymentLog,
                paymentResult.CvvStatus,
                paymentResult.AvsAddrStatus,
                paymentResult.AvsZipStatus );
        }
        else
        {
            ProcessCreditCardPaymentFailure( paymentResult.ErrorMessage );
        }
    }

    private void ProcessHostedPayment( CheckoutDetails checkout )
    {
        OrderNotifyService order = CreateOrder( checkout );

        PaymentAppGateway gateway = new PaymentAppGateway( checkout );

        string xmlData = gateway.CreateHostedPaymentXml(
            StoreContext.Culture,
            CurrenntCurrency,
            StoreContext.ShoppingCart,
            UrlPath.StorefrontUrl,
            order.OrderID,
            StoreContext.GetOrderAmount().Total,
            StoreContext.WholesaleStatus,
            WebUtilities.GetVisitorIP() );

        Response.Redirect( "../Gateway/GatewayPosting.aspx?OrderID=" + order.OrderID );
    }

    private void ProcessIntegratedPayPalPayment( CheckoutDetails checkout )
    {
        OrderNotifyService order = CreateOrder( checkout );
        Response.Redirect( "Gateway/GatewayPayPalPost.aspx?OrderID=" + order.OrderID );
    }

    private void ProcessIntegratedTwoCheckoutPayment( CheckoutDetails checkout )
    {
        OrderNotifyService order = CreateOrder( checkout );
        Response.Redirect( "Gateway/GatewayTwoCheckoutPost.aspx?OrderID=" + order.OrderID );
    }

    private OrderNotifyService CreateOrder( CheckoutDetails checkout )
    {
        OrderCreateService orderCreateService = new OrderCreateService(
                StoreContext.ShoppingCart,
                checkout,
                StoreContext.Culture,
                CurrenntCurrency,
                AffiliateHelper.GetAffiliateCode(),
                WebUtilities.GetVisitorIP() );

        OrderNotifyService orderBusiness;
        OrderAmount amount = orderCreateService.GetOrderAmount( StoreContext.Customer )
            .Add( CartItemPromotion.CalculatePromotionShippingAndTax(
            checkout,
            StoreContext.ShoppingCart.SeparateCartItemGroups(),
            StoreContext.Customer ) );

        Order order = orderCreateService.PlaceOrder( amount,
            StoreContext.Customer, DataAccessContext.StoreRetriever, StoreContext.Culture );
        GiftRegistry.UpdateGiftRegistryQuantity( StoreContext.ShoppingCart, checkout );
        CustomerRewardPoint.UpdateRedeemPoint( checkout, StoreContext.Customer, order );

        orderBusiness = new OrderNotifyService( order.OrderID );

        return orderBusiness;
    }

    private void ProcessOfflinePaymentSuccess( CheckoutDetails checkout )
    {
        OrderNotifyService order = CreateOrder( checkout );

        // Do not send electronic goods for offline payment. Merchants should send them manually.

        //********************* For Testing *****************************
        //order.SendDownloadEmailByOrderID();

        Exception emailEx = order.SendOrderEmailNoThrow();
        StoreError.Instance.Exception = emailEx;

        StoreContext.ClearCheckoutSession();

        MainContext.RedirectMainControl( "OrdersEdit.ascx", String.Format( "OrderID={0}", order.OrderID ) );
    }

    private void ProcessZeroPricePayment( CheckoutDetails checkout )
    {
        OrderNotifyService order = CreateOrder( checkout );

        Exception emailEx = order.SendOrderEmailNoThrow();
        StoreError.Instance.Exception = emailEx;

        order.ProcessPaymentComplete();

        StoreContext.ClearCheckoutSession();

        MainContext.RedirectMainControl( "OrdersEdit.ascx", String.Format( "OrderID={0}", order.OrderID ) );
    }

    private void CreateOrderAndRedirect()
    {
        string stockMessage;
        string minMaxQuantityMessage;

        if (!IsEnoughStock( out stockMessage ))
        {
            uxStockMessageLiteral.Text = stockMessage;
            uxStockMessageDiv.Visible = true;
            return;
        }

        if (!IsAcceptQuantity( out minMaxQuantityMessage ))
        {
            uxQuantityMessageLiteral.Text = minMaxQuantityMessage;
            //   uxBackHomeLink.Visible = true;
            uxQuantityMessageDiv.Visible = true;
            return;
        }

        if (GetCouponDiscount() == 0 && StoreContext.CheckoutDetails.Coupon.DiscountType != Coupon.DiscountTypeEnum.FreeShipping)
        {
            StoreContext.CheckoutDetails.SetCouponID( "" );
        }

        CheckoutDetails checkout = StoreContext.CheckoutDetails;

        if (checkout.PaymentMethod is OnPayPalProUSPaymentMethod)
        {
            ProcessOnPayPalProUSPayment( checkout );
        }
        else if (checkout.PaymentMethod is OfflineCreditCardPaymentMethod)
        {
            ProcessOfflineCreditCardPayment( checkout );
        }
        else if (checkout.PaymentMethod is OnWebsitePaymentMethod)
        {
            ProcessOnWebsitePayment( checkout );
        }
        else if (checkout.PaymentMethod is PayPalRedirectPaymentMethod)
        {
            ProcessIntegratedPayPalPayment( checkout );
        }
        else if (checkout.PaymentMethod is TwoCheckoutRedirectPaymentMethod)
        {
            ProcessIntegratedTwoCheckoutPayment( checkout );
        }
        else if (checkout.PaymentMethod is RedirectPaymentMethod)
        {
            ProcessHostedPayment( checkout );
        }
        else if (checkout.PaymentMethod is OfflinePaymentMethod)
        {
            ProcessOfflinePaymentSuccess( checkout );
        }
        else if (checkout.PaymentMethod is OnWebsiteRequireOrderIDPaymentMethod)
        {
            ProcessOnWebsiteRequireOrderIDPayment( checkout );
        }
        else if (checkout.PaymentMethod is ZeroPaymentMethod)
        {
            ProcessZeroPricePayment( checkout );
        }
        else
        {
            throw new VevoException( "Unknown payment method: " + checkout.PaymentMethod.Name );
        }
    }

    private void ShowQuantityDiscountNoCouponAndgiftWarningMessage( ICartItem[] cart )
    {
        uxWarningMessageLiteral.Text =
            "<p>The following recurring products cannot use coupon discount and GiftCertificate</p>";

        foreach (ICartItem item in cart)
        {
            if (item.IsRecurring)
            {
                uxWarningMessageLiteral.Text +=
                    "<li> "
                    + item.GetName( StoreContext.Culture, CurrenntCurrency )
                    + "</li>";
            }
        }
    }

    private void ShowRecurringGiftCertificateWarningMessage( ICartItem[] cart )
    {
        uxWarningMessageLiteral.Text =
            "<p>The following recurring products cannot use GiftCertificate</p>";

        foreach (ICartItem item in cart)
        {
            if (item.IsRecurring)
            {
                uxWarningMessageLiteral.Text +=
                    "<li> "
                    + item.GetName( StoreContext.Culture, CurrenntCurrency )
                    + "</li>";
            }
        }
    }

    private void ShowQuantityDiscountNoCouponWarningMessage( ICartItem[] cart )
    {
        if (!StoreContext.ShoppingCart.ContainsRecurringProduct())
        {
            uxWarningMessageLiteral.Text =
                "<p>The following products cannot use coupon discount since they " +
                "already have quantity discount.</p>";

            Coupon coupon = StoreContext.CheckoutDetails.Coupon;

            foreach (ICartItem item in cart)
            {
                //******************* test *****************
                // Questionable logic????
                if (item.DiscountGroupID != "0"
                    && coupon.IsProductDiscountable( item.Product ))
                {
                    uxWarningMessageLiteral.Text +=
                        "<li> "
                        + item.GetName( StoreContext.Culture, CurrenntCurrency )
                        + "</li>";
                }
            }
        }
        else
        {
            uxWarningMessageLiteral.Text =
                "<p>The following recurring products cannot use coupon discount</p>";

            foreach (ICartItem item in cart)
            {
                if (item.IsRecurring)
                {
                    uxWarningMessageLiteral.Text +=
                        "<li> "
                        + item.GetName( StoreContext.Culture, CurrenntCurrency )
                        + "</li>";
                }
            }
        }
    }

    private void ShowWarningMessage()
    {
        if (!StoreContext.CheckoutDetails.GiftCertificate.IsNull
            && StoreContext.ShoppingCart.ContainsRecurringProduct())
        {
            ICartItem[] cart = StoreContext.ShoppingCart.GetCartItems();
            if (!String.IsNullOrEmpty( StoreContext.CheckoutDetails.Coupon.CouponID )
                && StoreContext.ShoppingCart.ContainsRecurringProduct())
            {
                uxWarningMessageDiv.Visible = true;
                ShowQuantityDiscountNoCouponAndgiftWarningMessage( cart );
            }
            else if (StoreContext.ShoppingCart.ContainsRecurringProduct())
            {
                uxWarningMessageDiv.Visible = true;
                ShowRecurringGiftCertificateWarningMessage( cart );
            }
        }
        else if (!String.IsNullOrEmpty( StoreContext.CheckoutDetails.Coupon.CouponID ))
        {
            ICartItem[] cart = StoreContext.ShoppingCart.GetCartItems();
            if (GetDiscountWithoutCoupon() != 0)
            {
                ShowQuantityDiscountNoCouponWarningMessage( cart );
                uxWarningMessageLiteral.Text += "</div>";
            }
        }
    }

    private void ShowPartialCouponMessage()
    {
        if (GetCouponDiscount() > 0)
        {
            ICartItem[] cart = StoreContext.ShoppingCart.GetCartItems();

            Coupon coupon = StoreContext.CheckoutDetails.Coupon;
            int productDiscoutableItemCount = 0;

            if (coupon.ProductFilter != Coupon.ProductFilterEnum.All)
            {
                uxMessageLiteral.Text =
                    "<div style='color:blue; width:400px;margin-left:50px;'>" +
                    "<p>Coupon will not apply to all products in your shopping cart.</p>" +
                    "<p>Only following product(s) are eligible:</p>";

                for (int i = 0; i < cart.Length; i++)
                {
                    //******************* test *****************                    
                    // Questionable logic????                    
                    if (coupon.IsProductDiscountable( cart[i].Product )
                        && cart[i].DiscountGroupID == "0")
                    {
                        uxMessageLiteral.Text +=
                            "<li>"
                            + cart[i].GetName( StoreContext.Culture, CurrenntCurrency )
                            + "</li>";
                        productDiscoutableItemCount++;
                    }
                }
                uxMessageLiteral.Text += "</div>";
                if (productDiscoutableItemCount == cart.Length)
                {
                    uxMessageLiteral.Text = string.Empty;
                }
                uxMessageDiv.Visible = true;
            }
        }
    }

    private bool IsEnoughStock( out string message )
    {
        message = String.Empty;

        foreach (ICartItem item in StoreContext.ShoppingCart.GetCartItems())
        {
            int productStock = item.Product.GetStock( item.Options.GetUseStockOptionItemIDs() );
            int currentStock = productStock - item.Quantity;

            if (currentStock != DataAccessContext.Configurations.GetIntValue( "OutOfStockValue" ))
            {
                if (CatalogUtilities.IsOutOfStock( currentStock, CheckUseInventory( item.ProductID ) ))
                {
                    message += "<li>" + item.GetName( StoreContext.Culture, CurrenntCurrency );
                    if (DataAccessContext.Configurations.GetBoolValue( "ShowQuantity" ))
                    {
                        int displayStock = productStock - DataAccessContext.Configurations.GetIntValue( "OutOfStockValue" );
                        if (displayStock < 0)
                        {
                            displayStock = 0;
                        }

                        message += " ( available " + displayStock + " items )";

                    }
                    else
                    {
                        message += "</li>";
                    }
                }
            }
        }

        if (!String.IsNullOrEmpty( message ))
        {
            message =
                "<p class=\"ErrorHeader\">Stock Error</p>" +
                "<ul class=\"ErrorBody\">" + message + "</ul>";

            return false;
        }
        else
        {
            return true;
        }
    }



    private bool IsAcceptQuantity( out string message )
    {
        message = String.Empty;

        foreach (ICartItem item in StoreContext.ShoppingCart.GetCartItems())
        {
            int quantity = item.Quantity;
            int minQuantity = item.Product.MinQuantity;
            int maxQuantity = item.Product.MaxQuantity;

            if (quantity < minQuantity)
            {
                message += "<li>" + item.GetName( StoreContext.Culture, CurrenntCurrency );
                message += " ( minimum quantity " + minQuantity + " items )";
                message += "</li>";
            }

            if (maxQuantity != 0 && quantity > maxQuantity)
            {
                message += "<li>" + item.GetName( StoreContext.Culture, CurrenntCurrency );
                message += " ( maximum quantity " + maxQuantity + " items )";
                message += "</li>";
            }
        }

        if (!String.IsNullOrEmpty( message ))
        {
            message =
                "<p class=\"ErrorHeader\">[$QuantityError]</p>" +
                "<ul class=\"ErrorBody\">" + message + "</ul>";

            return false;
        }
        else
        {
            return true;
        }
    }

    private bool VerifyOrderAmount( out string errorHeader, out string errorMessage )
    {
        OrderValidator orderValidator = new OrderValidator();
        return orderValidator.ValidateSubtotal(
            CurrenntCurrency, StoreContext.WholesaleStatus, StoreContext.ShoppingCart,
            out errorHeader, out errorMessage );
    }

    private void SetGiftCertificate()
    {
        string giftCertificateCode = StoreContext.CheckoutDetails
            .GiftCertificate.GiftCertificateCode;

        if (!String.IsNullOrEmpty( giftCertificateCode ))
        {
            StoreContext.CheckoutDetails.SetGiftCertificate( giftCertificateCode );
        }
    }

    private int GetRewardPoint()
    {
        OrderAmount orderAmount = StoreContext.GetOrderAmount();
        OrderCalculator calculator = new OrderCalculator();
        return calculator.GetPointFromPrice( (orderAmount.Subtotal - orderAmount.Discount), ConvertUtilities.ToDecimal( DataAccessContext.Configurations.GetValue( "RewardPoints", StoreContext.CurrentStore ) ) );
    }

    protected string GetItemName( object cartItem )
    {
        ICartItem item = (ICartItem) cartItem;

        OrderCalculator orderCalculator = new OrderCalculator();
        OrderAmountPackage orderAmountPackage = orderCalculator.CalculatePackage(
            StoreContext.CheckoutDetails,
            new CartItemGroup( item ),
            StoreContext.Customer );

        return item.GetOrderItemName(
            StoreContext.Culture, CurrenntCurrency, orderAmountPackage );
    }

    protected string GetUnitPriceText( ICartItem cartItem )
    {
        return CurrenntCurrency.FormatPrice( cartItem.GetCheckoutUnitPrice( StoreContext.WholesaleStatus ) );
    }

    protected string GetSubtotalText( ICartItem cartItem )
    {
        return CurrenntCurrency.FormatPrice( cartItem.GetSubtotal( StoreContext.WholesaleStatus ) );
    }


    protected void Page_Load( object sender, EventArgs e )
    {
        if ((!DataAccessContext.Configurations.GetBoolValue( "ShippingAddressMode" ))
           || (!StoreContext.CheckoutDetails.ShowShippingAddress))
        {
            uxShippingForm.Visible = false;
        }
        else
        {
            PopulateShippingForm();
            uxShippingForm.Visible = true;
        }

        CheckoutDetails details = StoreContext.CheckoutDetails;
        uxOrderCommentLabel.Text = WebUtilities.ReplaceNewLine( details.CustomerComments );
        if (string.IsNullOrEmpty( uxOrderCommentLabel.Text ))
        {
            uxOrderSummaryCommentTD.Visible = false;
        }

        if (details.RedeemPoint > 0 && details.RedeemPrice > 0)
        {
            uxPointEarnedTD.Visible = false;
        }
        else
        {
            uxPointEarnedTD.Visible = DataAccessContext.Configurations.GetBoolValue( "PointSystemEnabled", StoreContext.CurrentStore ) && KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName );
            uxPointEarnedValueLabel.Text = GetRewardPoint().ToString();
        }

    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        uxGiftCertificateTR.Visible = DataAccessContext.Configurations.GetBoolValue(
                "GiftCertificateEnabled" );
        RefreshGrid();

        OrderAmount orderAmount = StoreContext.GetOrderAmount();

        uxProductCostLabel.Text = CurrenntCurrency.FormatPrice( orderAmount.Subtotal );
        uxTaxLabel.Text = CurrenntCurrency.FormatPrice( orderAmount.Tax );
        uxShippingCostLabel.Text = CurrenntCurrency.FormatPrice( orderAmount.ShippingCost );
        uxHandlingFeeLabel.Text = CurrenntCurrency.FormatPrice( orderAmount.HandlingFee );
        uxDiscountLabel.Text = CurrenntCurrency.FormatPrice( orderAmount.Discount * -1 );
        uxPointDiscountCostLabel.Text = CurrenntCurrency.FormatPrice( orderAmount.PointDiscount * -1 );
        uxGiftCertificateLabel.Text = CurrenntCurrency.FormatPrice( orderAmount.GiftCertificate * -1 );
        uxTotalLabel.Text = CurrenntCurrency.FormatPrice( orderAmount.Total );

        uxHandlingFeeTR.Visible = DataAccessContext.Configurations.GetBoolValue( "HandlingFeeEnabled" );
        uxApplyCouponDiv.Visible = !DataAccessContext.Configurations.GetBoolValue( "ShippingAddressMode" );
    }

    protected void uxNextButton_Click( object sender, EventArgs e )
    {
        StoreContext.CheckoutDetails.IsCreatedByAdmin = true;
        PlaceOrder();
    }
}
