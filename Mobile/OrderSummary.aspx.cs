using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI;
using Vevo;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.BundlePromotion;
using Vevo.Deluxe.Domain.GiftRegistry;
using Vevo.Deluxe.Domain.Marketing;
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
using Vevo.Shared.SystemServices;
using Vevo.Shared.WebUI;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.WebUI.Orders;

public partial class Mobile_OrderSummary : Vevo.WebUI.Orders.BaseProcessCheckoutPage
{
    #region Private

    private string CurrentID
    {
        get
        {
            return DataAccessContext.CustomerRepository.GetIDFromUserName( Membership.GetUser().UserName );
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
        Product product = DataAccessContext.ProductRepository.GetOne( StoreContext.Culture, productID, new StoreRetriever().GetCurrentStoreID() );
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

        Response.Redirect( "CheckoutComplete.aspx?OrderID=" + order.OrderID +
            "&IsTransaction=True" + GenerateIsEmailOKString( emailEx ) );
    }

    private void ProcessCreditCardPaymentFailure( string errorMessage )
    {
        CheckoutNotCompletePage.RedirectToPage(
            "<h4>Error Message</h4>",
            errorMessage,
            "~/DirectPaymentSale.aspx",
            "Click here to re-enter payment information" );
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
                StoreContext.Currency,
                AffiliateHelper.GetAffiliateCode(),
                WebUtilities.GetVisitorIP(),
                recurringPaymentResult
                );

            Order order;
            OrderAmount orderAmount;
            if (!IsAnonymousCheckout())
            {
                order = PlaceOrder( orderCreateService, out orderAmount );
            }
            else
            {
                order = PlaceOrderAnonymous( SystemConst.UnknownUser, orderCreateService, checkout, out orderAmount );
            }

            AffiliateOrder affiliateorder = new AffiliateOrder();
            affiliateorder.AffiliateCode = AffiliateHelper.GetAffiliateCode();
            affiliateorder.CreateAffiliateOrder( order.OrderID, orderAmount.Subtotal, orderAmount.Discount );

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

    private void ProcessAnonymousPayment( CheckoutDetails checkout )
    {
        AnonymousPaymentMethod payment = (AnonymousPaymentMethod) checkout.PaymentMethod;

        ProcessPaymentResult paymentResult;
        bool result = payment.ProcessPayment(
            StoreContext.GetOrderAmount().Total,
            DataAccessContext.Configurations.GetValue( "PaymentCurrency" ),
            checkout,
            out paymentResult );

        if (result)
        {
            OrderCreateService orderCreateService = new OrderCreateService(
                StoreContext.ShoppingCart,
                checkout,
                StoreContext.Culture,
                StoreContext.Currency,
                AffiliateHelper.GetAffiliateCode(),
                WebUtilities.GetVisitorIP() );

            OrderAmount orderAmount;
            Order order = PlaceOrderAnonymous(
                (Page.User.Identity.IsAuthenticated) ? Membership.GetUser().UserName : SystemConst.AnonymousUser,
                orderCreateService,
                checkout, out orderAmount );

            AffiliateOrder affiliateorder = new AffiliateOrder();
            affiliateorder.AffiliateCode = AffiliateHelper.GetAffiliateCode();
            affiliateorder.CreateAffiliateOrder( order.OrderID, orderAmount.Subtotal, orderAmount.Discount );

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
            CheckoutNotCompletePage.RedirectToPage(
                "Error Message",
                paymentResult.ErrorMessage,
                "ShoppingCart.aspx",
                "Return To Shopping Cart" );
        }
    }

    private void ProcessHostedPayment( CheckoutDetails checkout )
    {
        OrderNotifyService order = CreateOrder( checkout );

        PaymentAppGateway gateway = new PaymentAppGateway( checkout );

        string xmlData = gateway.CreateHostedPaymentXml(
            StoreContext.Culture,
            StoreContext.Currency,
            StoreContext.ShoppingCart,
            UrlPath.StorefrontUrl,
            order.OrderID,
            StoreContext.GetOrderAmount().Total,
            StoreContext.WholesaleStatus,
            WebUtilities.GetVisitorIP() );

        Response.Redirect( "Gateway/GatewayPosting.aspx?OrderID=" + order.OrderID );
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
                StoreContext.Currency,
                AffiliateHelper.GetAffiliateCode(),
                WebUtilities.GetVisitorIP() );

        OrderNotifyService orderBusiness;
        Order order;
        OrderAmount orderAmount;
        if (!IsAnonymousCheckout())
        {
            order = PlaceOrder( orderCreateService, out orderAmount );
            orderBusiness = new OrderNotifyService( order.OrderID );
        }
        else
        {
            order = PlaceOrderAnonymous(
                SystemConst.AnonymousUser, orderCreateService, checkout, out orderAmount );

            orderBusiness = new OrderNotifyService( order.OrderID );
        }

        AffiliateOrder affiliateorder = new AffiliateOrder();
        affiliateorder.AffiliateCode = AffiliateHelper.GetAffiliateCode();
        affiliateorder.CreateAffiliateOrder( order.OrderID, orderAmount.Subtotal, orderAmount.Discount );

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

        Response.Redirect( "CheckoutComplete.aspx?OrderID=" + order.OrderID +
            "&IsTransaction=True" + GenerateIsEmailOKString( emailEx ) );
    }

    private void ProcessZeroPricePayment( CheckoutDetails checkout )
    {
        OrderNotifyService order = CreateOrder( checkout );

        Exception emailEx = order.SendOrderEmailNoThrow();
        StoreError.Instance.Exception = emailEx;

        order.ProcessPaymentComplete();

        StoreContext.ClearCheckoutSession();

        Response.Redirect( "CheckoutComplete.aspx?OrderID=" + order.OrderID +
            "&IsTransaction=True" + GenerateIsEmailOKString( emailEx ) );
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

    private void ShowCouponWarningMessage( ICartItem[] cart )
    {
        Coupon coupon = StoreContext.CheckoutDetails.Coupon;

        CartItemGroup cartItemGroup = new CartItemGroup( cart );
        if (coupon.Validate( cartItemGroup, StoreContext.Customer ) != Coupon.ValidationStatus.OK)
        {
            uxCouponMessageDisplay.DisplayCouponErrorMessage( coupon, cartItemGroup );
            uxWarningMessageLiteral.Text += "<p>[$Coupon.WarningNotApplied]</p>";
            uxWarningMessageDiv.Visible = true;
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
                    + item.GetName( StoreContext.Culture, StoreContext.Currency )
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
                    + item.GetName( StoreContext.Culture, StoreContext.Currency )
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
                        + item.GetName( StoreContext.Culture, StoreContext.Currency )
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
                        + item.GetName( StoreContext.Culture, StoreContext.Currency )
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
            if (GetCouponDiscount() == 0)
            {
                ShowCouponWarningMessage( cart );
            }
            else if (GetDiscountWithoutCoupon() != 0)
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
                            + cart[i].GetName( StoreContext.Culture, StoreContext.Currency )
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
            if (!item.IsPromotion)
            {
                int productStock = item.Product.GetStock( item.Options.GetUseStockOptionItemIDs() );
                int currentStock = productStock - item.Quantity;

                if (currentStock != DataAccessContext.Configurations.GetIntValue( "OutOfStockValue" ))
                {
                    if (CatalogUtilities.IsOutOfStock( currentStock, CheckUseInventory( item.ProductID ) ))
                    {
                        message += "<li>" + item.GetName( StoreContext.Culture, StoreContext.Currency );
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
            else
            {
                CartItemPromotion cartItemPromotion = (CartItemPromotion) item;
                PromotionSelected promotionSelected = cartItemPromotion.PromotionSelected;
                foreach (PromotionSelectedItem selectedItem in promotionSelected.PromotionSelectedItems)
                {
                    Product product = selectedItem.Product;
                    string[] optionsUseStock = selectedItem.GetUseStockOptionItems().ToArray( typeof( string ) ) as string[];
                    int productStock = product.GetStock( optionsUseStock );
                    int currentStock = productStock - item.Quantity;
                    if (currentStock != DataAccessContext.Configurations.GetIntValue( "OutOfStockValue" ))
                    {
                        if (CatalogUtilities.IsOutOfStock( currentStock, CheckUseInventory( product.ProductID ) ))
                        {
                            message += "<li>" + item.GetName( StoreContext.Culture, StoreContext.Currency );
                            message += "</li>";
                        }
                    }
                }
            }
        }

        if (!String.IsNullOrEmpty( message ))
        {
            message =
                "<p class=\"ErrorHeader\">[$StockError]</p>" +
                "<ul class=\"ErrorBody\">" + message + "</ul>";

            return false;
        }
        else
        {
            return true;
        }
    }

    private bool IsEnoughGiftRegistyWantQuantity( out string message )
    {
        CheckoutDetails checkout = StoreContext.CheckoutDetails;
        message = String.Empty;
        if (!String.IsNullOrEmpty( checkout.GiftRegistryID ) && checkout.GiftRegistryID != "0")
        {
            foreach (ICartItem item in StoreContext.ShoppingCart.GetCartItems())
            {
                string giftRegistryItemID = checkout.CartItemIDToGiftRegistryIDMap[item.CartItemID];

                GiftRegistryItem giftRegistryItem = DataAccessContextDeluxe.GiftRegistryItemRepository.GetOne(
                    giftRegistryItemID );

                int wantQuantity = (int) (giftRegistryItem.WantQuantity - giftRegistryItem.HasQuantity);

                if (item.Quantity > wantQuantity)
                {
                    message +=
                        "<li>"
                        + item.GetName( StoreContext.Culture, StoreContext.Currency )
                        + "</li>";
                }
            }

            if (!String.IsNullOrEmpty( message ))
            {
                message =
                    "<p class=\"ErrorHeader\">[$GiftRegistryError]</p>" +
                    "<ul class=\"ErrorBody\">" + message + "</ul>";

                return false;
            }
            else
            {
                return true;
            }
        }
        else
            return true;
    }

    private bool IsAcceptQuantity( out string message )
    {
        message = String.Empty;

        foreach (ICartItem item in StoreContext.ShoppingCart.GetCartItems())
        {
            if (item.IsPromotion) continue;
            int quantity = item.Quantity;
            int minQuantity = item.Product.MinQuantity;
            int maxQuantity = item.Product.MaxQuantity;

            if (quantity < minQuantity)
            {
                message += "<li>" + item.GetName( StoreContext.Culture, StoreContext.Currency );
                message += " ( minimum quantity " + minQuantity + " items )";
                message += "</li>";
            }

            if (maxQuantity != 0 && quantity > maxQuantity)
            {
                message += "<li>" + item.GetName( StoreContext.Culture, StoreContext.Currency );
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

    private void CreateOrderAndRedirect()
    {
        string stockMessage;
        string giftRegistryMessage;
        string minMaxQuantityMessage;

        if (!IsEnoughStock( out stockMessage ))
        {
            uxStockMessageLiteral.Text = stockMessage;
            uxBackHomeLink.Visible = true;
            uxStockMessageDiv.Visible = true;
            return;
        }

        if (!IsEnoughGiftRegistyWantQuantity( out giftRegistryMessage ))
        {
            uxStockMessageLiteral.Text = giftRegistryMessage;
            uxBackHomeLink.Visible = true;
            uxStockMessageDiv.Visible = true;
            return;
        }

        if (!IsAcceptQuantity( out minMaxQuantityMessage ))
        {
            uxQuantityMessageLiteral.Text = minMaxQuantityMessage;
            uxBackHomeLink.Visible = true;
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
        else if (checkout.PaymentMethod is AnonymousPaymentMethod)
        {
            ProcessAnonymousPayment( checkout );
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

    private bool VerifyOrderAmount( out string errorHeader, out string errorMessage )
    {
        OrderValidator orderValidator = new OrderValidator();
        return orderValidator.ValidateSubtotal(
            StoreContext.Currency, StoreContext.WholesaleStatus, StoreContext.ShoppingCart,
            out errorHeader, out errorMessage );
    }

    private void PlaceOrder()
    {
        string errorHeader;
        string errorMessage;
        if (VerifyOrderAmount( out errorHeader, out errorMessage ))
        {
            //ResetCouponIDIfAlreadyDiscounted();
            SetGiftCertificate();
            CreateOrderAndRedirect();
        }
        else
        {
            ApplicationError.RedirectToErrorPage( errorHeader, errorMessage );
        }
    }

    private bool IsAnonymousCheckout()
    {
        if (Request.QueryString["skiplogin"] == "true")
            return true;
        else
            return false;
    }

    #endregion


    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        if (StoreContext.ShoppingCart.GetCartItems().Length == 0)
            Response.Redirect( "Default.aspx" );

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
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        uxGiftCertificateTR.Visible = DataAccessContext.Configurations.GetBoolValue(
            "GiftCertificateEnabled" );
        RefreshGrid();

        OrderAmount orderAmount = StoreContext.GetOrderAmount();

        uxProductCostLabel.Text = StoreContext.Currency.FormatPrice( orderAmount.Subtotal );
        uxTaxLabel.Text = StoreContext.Currency.FormatPrice( orderAmount.Tax );
        uxShippingCostLabel.Text = StoreContext.Currency.FormatPrice( orderAmount.ShippingCost );
        uxHandlingFeeLabel.Text = StoreContext.Currency.FormatPrice( orderAmount.HandlingFee );
        uxDiscountLabel.Text = StoreContext.Currency.FormatPrice( orderAmount.Discount * -1 );
        uxGiftCertificateLabel.Text = StoreContext.Currency.FormatPrice( orderAmount.GiftCertificate * -1 );
        uxTotalLabel.Text = StoreContext.Currency.FormatPrice( orderAmount.Total );

        uxHandlingFeeTR.Visible = DataAccessContext.Configurations.GetBoolValue( "HandlingFeeEnabled" );
        ShowPartialCouponMessage();
        ShowWarningMessage();
        uxApplyCouponDiv.Visible = !DataAccessContext.Configurations.GetBoolValue( "ShippingAddressMode" );
    }

    protected void uxFinishButton_Click( object sender, EventArgs e )
    {
        PlaceOrder();
    }

    protected void uxFinishImageButton_Click( object sender, EventArgs e )
    {
        PlaceOrder();
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
            StoreContext.Culture, StoreContext.Currency, orderAmountPackage );
    }

    protected string GetUnitPriceText( ICartItem cartItem )
    {
        return StoreContext.Currency.FormatPrice( cartItem.GetCheckoutUnitPrice( StoreContext.WholesaleStatus ) );
    }

    protected string GetSubtotalText( ICartItem cartItem )
    {
        return StoreContext.Currency.FormatPrice( cartItem.GetSubtotal( StoreContext.WholesaleStatus ) );
    }

    protected string GetTooltipMainText( object cartItem )
    {
        CartDisplayService displayService = new CartDisplayService();

        return displayService.GetCheckoutTooltipMainText();
    }

    protected string GetTooltipBody( object cartItem )
    {
        CartItemGroup cartItemGroup = new CartItemGroup( (ICartItem) cartItem );

        CartDisplayService displayService = new CartDisplayService();
        string tooltipBody = displayService.GetCheckoutTooltipBody(
            StoreContext.CheckoutDetails,
            cartItemGroup,
            StoreContext.Customer,
            StoreContext.Currency );

        return WebUtilities.ReplaceNewLine( tooltipBody );
    }

    #endregion
}
