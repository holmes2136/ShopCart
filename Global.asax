<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="Vevo.Data" %>
<%@ Import Namespace="Vevo.Deluxe.Data" %>
<%@ Import Namespace="Vevo.Deluxe.Domain" %>
<%@ Import Namespace="Vevo.Domain" %>
<%@ Import Namespace="Vevo.Domain.Security" %>
<%@ Import Namespace="Vevo.Domain.SystemServices" %>
<%@ Import Namespace="Vevo.Domain.DataInterfaces" %>
<%@ Import Namespace="Vevo.Deluxe.Domain.DataInterfaces" %>
<%@ Import Namespace="Vevo.Shared.DataAccess" %>
<%@ Import Namespace="Vevo.Shared.SystemServices" %>
<%@ Import Namespace="Vevo.Shared.WebUI" %>
<%@ Import Namespace="Vevo.WebAppLib" %>
<%@ Import Namespace="Vevo.WebUI" %>
<%@ Import Namespace="System.Reflection" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="System.Configuration" %>
<%@ Import Namespace="System.Web.Caching" %>

<script RunAt="server">
    
    void SetUpDataContext()
    {
        DataAccessContext.SetUpRepository( typeof( IAdminRepository ),
            new AdminRepository(), new AdminRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IAffiliateOrderRepository ),
            new AffiliateOrderRepository(), new AffiliateOrderRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IAffiliatePaymentRepository ),
            new AffiliatePaymentRepository(), new AffiliatePaymentRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IAffiliateRepository ),
            new AffiliateRepository(), new AffiliateRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IBannerRepository ),
            new BannerRepository(), new BannerRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IBlogRepository ),
            new BlogRepository(), new BlogRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IBlogCategoryRepository ),
               new BlogCategoryRepository(), new BlogCategoryRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IBlogCommentRepository ),
            new BlogCommentRepository(), new BlogCommentRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( ICartRepository ),
            new CartRepository(), new CartRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( ICartItemRepository ),
            new CartItemRepository(), new CartItemRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( ICategoryRepository ),
            new CategoryRepository(), new CategoryRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IConfigurationRepository ),
            new ConfigurationRepository(), new ConfigurationRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IContentMenuItemRepository ),
            new ContentMenuItemRepository(), new ContentMenuItemRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IContentMenuRepository ),
            new ContentMenuRepository(), new ContentMenuRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IContentRepository ),
            new ContentRepository(), new ContentRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( ICountryRepository ),
            new CountryRepository(), new CountryRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( ICouponRepository ),
            new CouponRepository(), new CouponRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( ICultureRepository ),
            new CultureRepository(), new CultureRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( ICurrencyRepository ),
            new CurrencyRepository(), new CurrencyRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( ICustomerRepository ),
            new CustomerRepository(), new CustomerRepositoryNoCache() );

        Vevo.Deluxe.Domain.DataAccessContextDeluxe.SetUpRepository( typeof( ICustomerRewardPointRepository ),
            new CustomerRewardPointRepository(), new CustomerRewardPointRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( ICustomerSubscriptionRepository ),
            new CustomerSubscriptionRepository(), new CustomerSubscriptionRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IDepartmentRepository ),
            new DepartmentRepository(), new DepartmentRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IGiftCertificateRepository ),
            new GiftCertificateRepository(), new GiftCertificateRepositoryNoCache() );

        Vevo.Deluxe.Domain.DataAccessContextDeluxe.SetUpRepository( typeof( IGiftRegistryItemRepository ),
            new GiftRegistryItemRepository(), new GiftRegistryItemRepositoryNoCache() );

        Vevo.Deluxe.Domain.DataAccessContextDeluxe.SetUpRepository( typeof( IGiftRegistryRepository ),
            new GiftRegistryRepository(), new GiftRegistryRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IGoogleFeedMappingRepository ),
            new GoogleFeedMappingRepository(), new GoogleFeedMappingRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IGoogleFeedTagRepository ),
            new GoogleFeedTagRepository(), new GoogleFeedTagRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IGoogleFeedTagValueRepository ),
            new GoogleFeedTagValueRepository(), new GoogleFeedTagValueRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IHelpRepository ),
            new HelpRepository(), new HelpRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IManufacturerRepository ),
            new ManufacturerRepository(), new ManufacturerRepositoryNoCache() );
        
        DataAccessContext.SetUpRepository( typeof( INewsLetterRepository ),
            new NewsLetterRepository(), new NewsLetterRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( INewsRepository ),
            new NewsRepository(), new NewsRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IOptionGroupRepository ),
            new OptionGroupRepository(), new OptionGroupRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IOptionItemRepository ),
            new OptionItemRepository(), new OptionItemRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IDiscountGroupRepository ),
            new DiscountGroupRepository(), new DiscountGroupRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IEmailTemplateDetailRepository ),
            new EmailTemplateDetailRepository(), new EmailTemplateDetailRepositoryNoCache() );

        Vevo.Deluxe.Domain.DataAccessContextDeluxe.SetUpRepository( typeof( IEBayListRepository ),
            new EBayListRepository(), new EBayListRepositoryNoCache() );

        Vevo.Deluxe.Domain.DataAccessContextDeluxe.SetUpRepository( typeof( IEBayTemplateRepository ),
            new EBayTemplateRepository(), new EBayTemplateRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IOrderRepository ),
            new OrderRepository(), new OrderRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IOrderItemRepository ),
            new OrderItemRepository(), new OrderItemRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IPaymentLogRepository ),
            new PaymentLogRepository(), new PaymentLogRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IPaymentOptionRepository ),
            new PaymentOptionRepository(), new PaymentOptionRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IPayPalIpnRepository ),
            new PayPalIpnRepository(), new PayPalIpnRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IProductKitGroupRepository ),
            new ProductKitGroupRepository(), new ProductKitGroupRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IProductRepository ),
            new ProductRepository(), new ProductRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IProductSubscriptionRepository ),
            new ProductSubscriptionRepository(), new ProductSubscriptionRepositoryNoCache() );

       Vevo.Deluxe.Domain.DataAccessContextDeluxe.SetUpRepository( typeof( IPromotionGroupRepository ),
            new PromotionGroupRepository(), new PromotionGroupRepositoryNoCache() );

        Vevo.Deluxe.Domain.DataAccessContextDeluxe.SetUpRepository( typeof( IPromotionProductRepository ),
            new PromotionProductRepository(), new PromotionProductRepository() );

        Vevo.Deluxe.Domain.DataAccessContextDeluxe.SetUpRepository( typeof( IPromotionSubGroupRepository ),
            new PromotionSubGroupRepository(), new PromotionSubGroupRepository() );

        Vevo.Deluxe.Domain.DataAccessContextDeluxe.SetUpRepository( typeof( IQuickBooksLogRepository ),
            new QuickBooksLogRepository(), new QuickBooksLogRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IRecurringProfileRepository ),
            new RecurringProfileRepository(), new RecurringProfileRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IRmaRepository ),
            new RmaRepository(), new RmaRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IRmaActionRepository ),
            new RmaActionRepository(), new RmaActionRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IShippingOptionRepository ),
            new ShippingOptionRepository(), new ShippingOptionRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IShippingOptionTypeRepository ),
            new ShippingOptionTypeRepository(), new ShippingOptionTypeRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IShippingWeightRateRepository ),
            new ShippingWeightRateRepository(), new ShippingWeightRateRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IShippingOrderTotalRateRepository ),
            new ShippingOrderTotalRateRepository(), new ShippingOrderTotalRateRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IShippingZoneGroupRepository ),
            new ShippingZoneGroupRepository(), new ShippingZoneGroupRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( ISpecificationGroupRepository ),
            new SpecificationGroupRepository(), new SpecificationGroupRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( ISpecificationItemRepository ),
            new SpecificationItemRepository(), new SpecificationItemRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( ISpecificationItemValueRepository ),
            new SpecificationItemValueRepository(), new SpecificationItemValueRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( ISubscriptionLevelRepository ),
            new SubscriptionLevelRepository(), new SubscriptionLevelRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IStateRepository ),
            new StateRepository(), new StateRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IStoreRepository ),
            new StoreRepository(), new StoreRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( ISubscriptionLevelRepository ),
            new SubscriptionLevelRepository(), new SubscriptionLevelRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( ITaxClassRepository ),
            new TaxClassRepository(), new TaxClassRepositoryNoCache() );

        DataAccessContext.SetUpRepository( typeof( IWishListRepository ),
            new WishListRepository(), new WishListRepositoryNoCache() );
    }

    void SetUpPaymentDataContext()
    {
        PaymentModuleSetup paymentModuleSetup = new PaymentModuleSetup();
        paymentModuleSetup.ProcessApplicationStarted();
    }

    void SetUpSslPolicy()
    {
        // Set up whether to accept untrusted (self-certified) SSL certificate for testing or not.
        // For a local domain (e.g. localhost, 192.168.x.x, 127.0.0.1) always allow self-signed certificates.
        SslPolicy sslPolicy = new SslPolicy();
        sslPolicy.SetUpCertificatePolicy( UrlPath.StorefrontUrl );
    }

    void Application_Start( object sender, EventArgs e )
    {
        // Code that runs on application startup
        SetUpDataContext();
        SetUpPaymentDataContext();
        ConfigurationHelper.SetDomainRegistrationKey();

        // Fixed USPS error "The remote server retuned an unexpected response(417)"
        System.Net.ServicePointManager.Expect100Continue = false;
    }

    void Application_End( object sender, EventArgs e )
    {
        //  Code that runs on application shutdown
        HttpRuntime runtime = (HttpRuntime) typeof( System.Web.HttpRuntime ).InvokeMember( "_theRuntime", BindingFlags.NonPublic
| BindingFlags.Static | BindingFlags.GetField, null, null, null );
        if (runtime == null) return;
        string shutDownMessage = (string) runtime.GetType().InvokeMember( "_shutDownMessage", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, runtime, null );
        string shutDownStack = (string) runtime.GetType().InvokeMember( "_shutDownStack", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, runtime, null );
        if (!EventLog.SourceExists( ".NET Runtime" ))
        {
            EventLog.CreateEventSource( ".NET Runtime", "Application" );
        }
        EventLog log = new EventLog();
        log.Source = ".NET Runtime";
        log.WriteEntry( String.Format( "\r\n\r\n_shutDownMessage={0}\r\n\r\n_shutDownStack={1}",
                                     shutDownMessage,
                                     shutDownStack ),
                                     EventLogEntryType.Error );
    }

    void Application_Error( object sender, EventArgs e )
    {
        WebUtilities.LogError( Server.GetLastError() );
    }

    void Session_Start( object sender, EventArgs e )
    {
        // Try to connect to database every time a session has started
        DatabaseConnectionService connectionService = new DatabaseConnectionService();
        SetUpSslPolicy();

        if (connectionService.TryConnect() && DataAccessContext.ApplicationSettings.InstallCompleted)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StoreConnection"].ConnectionString;
            if (Vevo.KeyUtilities.IsMultistoreLicense())
            {
                System.Data.SqlClient.SqlDependency.Start( connectionString );
            }
        }
        // Code that runs when a new session is started

        Session["ShoppingCart"] = new ArrayList();
        Session["ProductImage"] = new ArrayList();

        DataTransactionScope.ClearAll();



    }

    void Session_End( object sender, EventArgs e )
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
        if (Vevo.KeyUtilities.IsMultistoreLicense())
            System.Data.SqlClient.SqlDependency.Stop( ConfigurationManager.ConnectionStrings["StoreConnection"].ConnectionString );
    }

    void Application_BeginRequest( object sender, EventArgs e )
    {
        /* Fix for the Flash Player Cookie bug in Non-IE browsers.
         * Since Flash Player always sends the IE cookies even in FireFox
         * we have to bypass the cookies by sending the values as part of the POST or GET
         * and overwrite the cookies with the passed in values.
         * 
         * The theory is that at this point (BeginRequest) the cookies have not been read by
         * the Session and Authentication logic and if we update the cookies here we'll get our
         * Session and Authentication restored correctly
         */

        try
        {
            ///HttpContext.Current.Response.AddHeader( "p3p", "CP=\"CAO PSA OUR\"" );/* This Fix For IFrame*/

            string session_param_name = "ASPSESSID";
            string session_cookie_name = "ASP.NET_SESSIONID";

            if (HttpContext.Current.Request.Form[session_param_name] != null)
            {
                UpdateCookie( session_cookie_name, HttpContext.Current.Request.Form[session_param_name] );
            }
            else if (HttpContext.Current.Request.QueryString[session_param_name] != null)
            {
                UpdateCookie( session_cookie_name, HttpContext.Current.Request.QueryString[session_param_name] );
            }
        }
        catch (Exception)
        {
            Response.StatusCode = 500;
            Response.Write( "Error Initializing Session" );
        }

        try
        {
            string auth_param_name = "AUTHID";
            string auth_cookie_name = FormsAuthentication.FormsCookieName;

            if (HttpContext.Current.Request.Form[auth_param_name] != null)
            {
                UpdateCookie( auth_cookie_name, HttpContext.Current.Request.Form[auth_param_name] );
            }
            else if (HttpContext.Current.Request.QueryString[auth_param_name] != null)
            {
                UpdateCookie( auth_cookie_name, HttpContext.Current.Request.QueryString[auth_param_name] );
            }

        }
        catch (Exception)
        {
            Response.StatusCode = 500;
            Response.Write( "Error Initializing Forms Authentication" );
        }

        try
        {
            string incomingURL = HttpContext.Current.Request.RawUrl.ToString();
            string adminURL = DataAccessContext.Configurations.GetValue( "AdminAdvancedFolder" ).ToLower();
            if (Regex.IsMatch( incomingURL, @"[A-Z]" ) && incomingURL.Contains( ".aspx" ) &&
                 !incomingURL.Contains( "uxScriptManager" ) && !incomingURL.ToLower().Contains( "upload" ) &&
                 !incomingURL.ToLower().Contains( adminURL ) && !incomingURL.ToLower().Contains( "mobile" ) &&
                 !incomingURL.ToLower().Contains( "facebook" ) && !incomingURL.ToLower().Contains( "install" ) &&
                 !incomingURL.ToLower().Contains( "filedownloadmanager.aspx" ) && !incomingURL.ToLower().Contains( "filedownload.aspx" ) &&
                 !incomingURL.ToLower().Contains( "gateway" ) && !incomingURL.ToLower().Contains( "resetpassword.aspx" ) &&
                 !incomingURL.ToLower().Contains( "token" ) && !incomingURL.ToLower().Contains( "blog" ) &&
                 !incomingURL.ToLower().Contains( "subscribeconfirm.aspx" ) && !incomingURL.ToLower().Contains( "newsletter.aspx" ) &&
                 !incomingURL.ToLower().Contains( "userlogin.aspx" ) && !incomingURL.ToLower().Contains( "register" ))
            {
                string LowercaseURL = HttpContext.Current.Request.RawUrl.ToString().ToLower();
                Response.Clear();
                Response.Status = "301 Moved Permanently";
                Response.AddHeader( "Location", LowercaseURL );
                Response.End();
            }
        }
        catch (Exception)
        {

        }
    }

    void UpdateCookie( string cookie_name, string cookie_value )
    {
        HttpCookie cookie = HttpContext.Current.Request.Cookies.Get( cookie_name );
        if (cookie == null)
        {
            cookie = new HttpCookie( cookie_name );
            HttpContext.Current.Request.Cookies.Add( cookie );
        }
        cookie.Value = cookie_value;
        HttpContext.Current.Request.Cookies.Set( cookie );
    }
       
</script>

