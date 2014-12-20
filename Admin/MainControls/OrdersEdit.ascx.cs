using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Payments;
using Vevo.Domain.Payments.PayPalProUS;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.Domain.Users;
using Vevo.Shared.Utilities;
using Vevo.Shared.WebUI;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.WebUI.Orders;
using Vevo.Deluxe.Domain;
using Vevo.Base.Domain;
using Vevo.Deluxe.Domain.CustomerReward;
using Vevo.Shared.DataAccess;
using Vevo.Deluxe.Domain.Products;

public partial class AdminAdvanced_MainControls_OrdersEdit : AdminAdvancedBaseUserControl
{
    #region Private

    private const int ColumnOrderItemID = 1;
    private const int ColumnOrderItemName = 2;

    private OrderNotifyService _order;


    private string CurrentOrderID
    {
        get
        {
            return MainContext.QueryString["OrderID"];
        }
    }

    private OrderNotifyService CurrentOrder
    {
        get
        {
            if (_order == null)
                _order = new OrderNotifyService( CurrentOrderID );

            return _order;
        }
    }

    private DateTime OrderDate
    {
        get
        {
            return (DateTime) ViewState["OrderDate"];
        }
        set
        {
            ViewState["OrderDate"] = value;
        }
    }

    private string GetCompanyData()
    {
        string lang = CultureUtilities.StoreCultureID;
        string siteName = DataAccessContext.Configurations.GetValue( CultureUtilities.StoreCultureID, "SiteName" );
        string companyName = DataAccessContext.Configurations.GetValue( CultureUtilities.StoreCultureID, "CompanyName" );
        string address = DataAccessContext.Configurations.GetValue( CultureUtilities.StoreCultureID, "CompanyAddress" );
        string city = DataAccessContext.Configurations.GetValue( CultureUtilities.StoreCultureID, "CompanyCity" );
        string state = DataAccessContext.Configurations.GetValue( CultureUtilities.StoreCultureID, "CompanyState" );
        string zipCode = DataAccessContext.Configurations.GetValue( "CompanyZip" );
        string country = DataAccessContext.Configurations.GetValue( CultureUtilities.StoreCultureID, "CompanyCountry" );
        string phone = DataAccessContext.Configurations.GetValue( "CompanyPhone" );
        string fax = DataAccessContext.Configurations.GetValue( "CompanyFax" );
        string email = DataAccessContext.Configurations.GetValue( "CompanyEmail" );

        return siteName + "|" + companyName + "|" + address + "|" + city + "|" + state + "|" +
            zipCode + "|" + country + "|" + phone + "|" + fax + "|" + email;
    }

    private string GetText( string id )
    {
        return ((TextBox) uxFormView.Row.FindControl( id )).Text;
    }

    private bool GetCheck( string id )
    {
        return ((CheckBox) uxFormView.Row.FindControl( id )).Checked;
    }

    private string GetDrop( string id )
    {
        return ((DropDownList) uxFormView.Row.FindControl( id )).SelectedItem.Text;
    }

    private string GetStateList( string id )
    {
        return ((AdminAdvanced_Components_Common_StateList) uxFormView.Row.FindControl( id )).CurrentSelected;
    }

    private string GetCountryList( string id )
    {
        return ((AdminAdvanced_Components_Common_CountryList) uxFormView.Row.FindControl( id )).CurrentSelected;
    }

    private DateTime GetCalendarDate( string id )
    {
        return ((AdminAdvanced_Components_CalendarPopup) uxFormView.Row.FindControl( id )).SelectedDate;
    }

    private bool IsDropDownInitialized( DropDownList drop )
    {
        return (drop.SelectedIndex != -1);
    }

    private void ApplyPermissions()
    {
        if (!IsAdminModifiable())
        {
            uxAddButton.Visible = false;
            DeleteVisible( false );
        }
    }

    private void GetTrackingEmail( Order order, Store store, out string subjectMail, out string bodyMail )
    {
        string siteName = DataAccessContext.Configurations.GetValue( CultureUtilities.StoreCultureID, "SiteName", order.StoreID );

        string trackingUrl = GetTrackingUrl( order.TrackingMethod, order.TrackingNumber );

        EmailTemplateTextVariable.ReplaceTrackingText(
            siteName,
            order,
            trackingUrl,
            out subjectMail,
            out bodyMail );
    }

    private string GetTrackingUrl( string trackingMethod, string trackingNumber )
    {
        string trackingUrl;
        if (trackingMethod.ToLower() == "ups")
            trackingUrl = DataAccessContext.Configurations.GetValue( "UpsTrackingUrl" );
        else if (trackingMethod.ToLower() == "fedex")
            trackingUrl = DataAccessContext.Configurations.GetValue( "FedExTrackingUrl" );
        else if (trackingMethod.ToLower() == "usps")
            trackingUrl = DataAccessContext.Configurations.GetValue( "UspsTrackingUrl" );
        else
            trackingUrl = DataAccessContext.Configurations.GetValue( "OtherTrackingUrl" );

        if (!string.IsNullOrEmpty( trackingUrl ))
        {
            Regex regex = new Regex( @"\[tracknum\]", RegexOptions.IgnoreCase );
            trackingUrl = regex.Replace( trackingUrl, HttpUtility.UrlEncode( trackingNumber ) );
        }
        return trackingUrl;
    }

    private void SendTrackingMail()
    {
        string subjectMail;
        string bodyMail;

        Order order = DataAccessContext.OrderRepository.GetOne( CurrentOrderID );
        Store store = DataAccessContext.StoreRepository.GetOne( order.StoreID );

        string companyEmail = DataAccessContext.Configurations.GetValue( "CompanyEmail", store );

        GetTrackingEmail( order, store, out subjectMail, out bodyMail );

        WebUtilities.SendHtmlMail(
        companyEmail,
        order.Email,
        subjectMail,
        bodyMail,
        store );
        uxMessage.DisplayMessage( Resources.OrdersMessages.SendTrackingNumberComplete );
    }

    private void SendGiftCertificateMail()
    {
        Order order = DataAccessContext.OrderRepository.GetOne( CurrentOrderID );
        CurrentOrder.SendGiftCodeMail( order );
        uxMessage.DisplayMessage( Resources.OrdersMessages.SendGiftCertificateCodeComplete );
    }

    private void ProcessRecurringCancelPayPalPro( string referenceID, int recurringID, string emailCustomer, string username, string orderID )
    {
        string errorMessage;

        Order order = DataAccessContext.OrderRepository.GetOne( orderID );
        Store store = DataAccessContext.StoreRepository.GetOne( order.StoreID );

        string companyEmail = DataAccessContext.Configurations.GetValue( "CompanyEmail", store );

        if (PayPalProUSPaymentMethod.ManageRecurring(
            referenceID, emailCustomer, username, orderID,
            PayPalProUSPaymentMethod.RecurringStatus.Cancel, companyEmail, String.Empty, out errorMessage ))
        {
            ProcessRecurringCancelComplete( referenceID );
        }
        else
        {
            uxMessage.DisplayError( errorMessage );
        }
    }

    private void ProcessRecurringCancelComplete( string referenceID )
    {
        string recurringID = DataAccessContext.RecurringProfileRepository.GetRecurringIDFromReferenceID( referenceID );
        RecurringProfile recurringProfile = DataAccessContext.RecurringProfileRepository.GetOne( recurringID );
        recurringProfile.RecurringStatus = SystemConst.RecurringStatus.Canceled.ToString();
        recurringProfile.UpdateTime = DateTime.Now;
        DataAccessContext.RecurringProfileRepository.Save( recurringProfile );

        uxMessage.DisplayMessage( "The recurring is canceled completly." );
        //CurrentOrder.RefreshAllPricing();

        //uxFormView.DataBind();
        uxItemGrid.DataBind();
    }

    private void DeleteVisible( bool value )
    {
        uxDeleteButton.Visible = value;
        if (value)
        {
            if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Normal)
            {
                uxDeleteConfirmButton.TargetControlID = "uxDeleteButton";
                uxConfirmModalPopup.TargetControlID = "uxDeleteButton";
            }
            else
            {
                uxDeleteConfirmButton.TargetControlID = "uxDummyButton";
                uxConfirmModalPopup.TargetControlID = "uxDummyButton";
            }
        }
        else
        {
            uxDeleteConfirmButton.TargetControlID = "uxDummyButton";
            uxConfirmModalPopup.TargetControlID = "uxDummyButton";
        }
    }

    private DateTime GetOrderDate( DateTime date )
    {
        return new DateTime( date.Year, date.Month, date.Day, OrderDate.Hour, OrderDate.Minute, OrderDate.Second );
    }

    private bool IsOrderContainRecurring()
    {
        IList<OrderItem> orderItems = DataAccessContext.OrderItemRepository.GetByOrderID( CurrentOrderID );
        foreach (OrderItem item in orderItems)
        {
            if (ConvertUtilities.ToInt32( item.RecurringID ) != 0)
                return true;
        }
        return false;
    }

    private void UpdateOrder()
    {
        Order order = DataAccessContext.OrderRepository.GetOne( CurrentOrderID );
        order.OrderDate = GetOrderDate( GetCalendarDate( "uxOrderDateCalendarPopup" ) );
        order.UserName = GetText( "uxUserNameText" );
        order.Billing = new Address( GetText( "uxFirstNameText" ), GetText( "uxLastNameText" ),
                GetText( "uxCompanyText" ), GetText( "uxAddress1Text" ), GetText( "uxAddress2Text" ),
                GetText( "uxCityText" ), GetStateList( "uxStateList" ), GetText( "uxZipText" ),
                GetCountryList( "uxCountryList" ), GetText( "uxPhoneText" ), GetText( "uxFaxText" ) );
        order.Email = GetText( "uxEmailText" );
        order.Shipping = new ShippingAddress(
            new Address( GetText( "uxShippingFirstNameText" ), GetText( "uxShippingLastNameText" ),
                GetText( "uxShippingCompanyText" ), GetText( "uxShippingAddress1Text" ),
                GetText( "uxShippingAddress2Text" ), GetText( "uxShippingCityText" ),
                GetStateList( "StateListShipping" ), GetText( "uxShippingZipText" ),
                GetCountryList( "CountryListShipping" ), GetText( "uxShippingPhoneText" ),
                GetText( "uxShippingFaxText" ) ),
            false );
        order.PaymentMethod = GetText( "uxPaymentMethodText" );
        order.ShippingMethod = GetText( "uxShippingMethodText" );
        order.PaymentComplete = GetCheck( "uxPaymentCompleteCheck" );
        order.Processed = GetCheck( "uxProcessedCheck" );
        order.Status = GetDrop( "uxStatusDrop" );
        order.Cancelled = GetCheck( "uxCancelledCheck" );
        order.IPAddress = GetText( "uxIPAddressText" );
        order.Subtotal = DataAccessContext.OrderItemRepository.GetSubtotal( CurrentOrderID );
        order.Tax = ConvertUtilities.ToDecimal( GetText( "uxTaxText" ) );
        order.ShippingCost = ConvertUtilities.ToDecimal( GetText( "uxShippingCostText" ) );
        order.CouponID = GetText( "uxCouponIDText" );
        order.CouponDiscount = ConvertUtilities.ToDecimal( GetText( "uxCouponDiscountText" ) );
        order.CustomerComments = GetText( "uxCommentText" );
        order.BaseCurrencyCode = GetText( "uxBaseCodeText" );
        order.UserCurrencyCode = GetText( "uxUserCurrencyCodeText" );
        order.UserConversionRate = ConvertUtilities.ToDouble( GetText( "uxConversionRateText" ) );
        order.InvoiceNotes = GetText( "uxInvoiceNotesText" );
        order.GiftCertificateCode = GetText( "uxGiftCertificateCodeText" );
        order.GiftCertificate = ConvertUtilities.ToDecimal( (GetText( "uxGiftCertificateText" )) );
        order.TrackingNumber = GetText( "uxTrackingNumerText" );
        order.TrackingMethod = GetDrop( "uxTrackingMethodDrop" );
        order.HandlingFee = ConvertUtilities.ToDecimal( GetText( "uxHandlingFeeText" ) );
        order.ContainsRecurring = IsOrderContainRecurring();
        order.AvsAddrStatus = ((DropDownList) uxFormView.Row.FindControl( "uxAvsAddrDrop" )).SelectedValue;
        order.AvsZipStatus = ((DropDownList) uxFormView.Row.FindControl( "uxAvsZipDrop" )).SelectedValue;
        order.CvvStatus = ((DropDownList) uxFormView.Row.FindControl( "uxCvvDrop" )).SelectedValue;
        order.PONumber = GetText( "uxPONumberText" );

        if (IsSaleTaxExemptVisible( true ) && !String.IsNullOrEmpty( GetText( "uxTaxExepmtIDText" ) ))
        {
            order.IsTaxExempt = true;
            order.TaxExemptID = GetText( "uxTaxExepmtIDText" );
            order.TaxExemptCountry = GetCountryList( "uxTaxExemptCountryList" );
            order.TaxExemptState = GetStateList( "uxTaxExemptStateList" );
        }
        else
        {
            order.IsTaxExempt = false;
            order.TaxExemptID = String.Empty;
            order.TaxExemptCountry = String.Empty;
            order.TaxExemptState = String.Empty;
        }

        DataAccessContext.OrderRepository.Save( order );
        if (order.PaymentComplete)
        {
            CustomerRewardPoint.UpdateRewardPoint( order );

            if (!order.IsSubscriptionApplied)
            {
                OrderNotifyService orderNotifyService = new OrderNotifyService( order.OrderID );
                orderNotifyService.UpdateCustomerSubscription( order );
                order.IsSubscriptionApplied = true;
                DataAccessContext.OrderRepository.Save( order );
            }
        }
    }

    #endregion


    #region Protected

    protected bool IsActive( object orderItemID )
    {
        if (orderItemID != null)
        {
            OrderItemRecurring orderItemrecurring = DataAccessContext.OrderItemRepository.GetOne( orderItemID.ToString() ).OrderItemRecurring;

            if (orderItemrecurring != null)
            {
                if (orderItemrecurring.RecurringStatus == "Ongoing" || orderItemrecurring.RecurringStatus == "New" || orderItemrecurring.RecurringStatus == "CreateVerified")
                    return true;
            }
        }

        return false;
    }

    protected string RecurringStatus( object orderItemID )
    {
        if (orderItemID != null)
        {
            OrderItemRecurring orderItemrecurring = DataAccessContext.OrderItemRepository.GetOne( orderItemID.ToString() ).OrderItemRecurring;

            if (orderItemrecurring != null)
            {
                if (orderItemrecurring.RecurringStatus == "Expired")
                    return "Completed";
                else if (orderItemrecurring.RecurringStatus == "Canceled")
                    return orderItemrecurring.RecurringStatus;
            }
        }
        return "";
    }

    protected void uxCancelLinkButton_Load( object sender, EventArgs e )
    {
        WebUtilities.ButtonAddConfirmation( (LinkButton) sender, "Are you sure to cancel recurring?" );
    }

    protected void uxCancelLinkButton_Command( object sender, CommandEventArgs e )
    {
        string recurringID = e.CommandArgument.ToString();
        RecurringProfile recurringProfile = DataAccessContext.RecurringProfileRepository.GetOne( recurringID );

        Order order = DataAccessContext.OrderRepository.GetOne( CurrentOrderID );

        //if (details.GatewayName == SystemConst.PayPalPro)
        // *** use email from customer object if the customer repository return null use email from order object
        Customer customer = DataAccessContext.CustomerRepository.GetOne( order.CustomerID );
        string email = order.Email;
        if (customer != null)
            email = customer.Email;

        ProcessRecurringCancelPayPalPro( recurringProfile.ReferenceID, ConvertUtilities.ToInt32( recurringID ),
                email, order.UserName, CurrentOrderID );
        //else
        //    ProcessRecurringCancelAuthorize( recurringID, username );
    }

    protected string GenarateLink( string orderID, string productID, string name, string orderItemID )
    {
        OrderItem orderItem = DataAccessContext.OrderItemRepository.GetOne( orderItemID );

        IList<string> optionNames = orderItem.GetUploadedOptionNames();
        foreach (string optionName in optionNames)
        {
            string link = String.Format( "<a href=../{0} target='_blank' >{1}</a>",
                (SystemConst.OptionFileUpload + orderItem.GenerateUploadedFileName( optionName )).Replace( " ", "%20" ),
                optionName );

            name = name.Replace( optionName, link );
        }

        return name;
    }

    protected bool IsHandlingFeeVisible()
    {
        return DataAccessContext.Configurations.GetBoolValue( "HandlingFeeEnabled" );
    }

    protected bool IsRewardPointVisible()
    {
        Order order = DataAccessContext.OrderRepository.GetOne( CurrentOrderID );
        Store store = DataAccessContext.StoreRepository.GetOne( order.StoreID );
        return DataAccessContext.Configurations.GetBoolValue( "PointSystemEnabled", store ) || order.PointEarned > 0;
    }

    protected bool IsShippingAddressMode()
    {
        return DataAccessContext.Configurations.GetBoolValue( "ShippingAddressMode" );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (String.IsNullOrEmpty( CurrentOrderID ))
            MainContext.RedirectMainControl( "OrderList.ascx" );
        else
        {
            if (uxOrderDetailsSource.SelectParameters.Count == 0)
                uxOrderDetailsSource.SelectParameters.Add( new Parameter( "OrderID", TypeCode.String, CurrentOrderID ) );
            if (uxOrderItemsSource.SelectParameters.Count == 0)
                uxOrderItemsSource.SelectParameters.Add( new Parameter( "OrderID", TypeCode.String, CurrentOrderID ) );
        }
        //RegisterPrintButton();

        if (!MainContext.IsPostBack)
        {
            //UpdateTrackingAndClientLink();

            Order order = DataAccessContext.OrderRepository.GetOne( CurrentOrderID );
            OrderDate = order.OrderDate;

            if (order.ContainsRecurring && (ConvertUtilities.ToInt32( order.ParentID ) > 0))
            {
                uxOrderIDLabel.Text = String.Format( "{0} ( Child Order of ", CurrentOrderID );
                uxParentOrderLink.Visible = true;
                uxParentOrderLink.Text = String.Format( "Order {0}", order.ParentID );
                uxParentOrderLink.PageQueryString = String.Format( "OrderID={0}", order.ParentID );
                uxParentOrderLink.StatusBarText = String.Format( "Edit OrderID {0}", order.ParentID );
                uxParentOrderEndLabel.Text = ")";
                uxParentOrderEndLabel.Visible = true;
            }
            else
            {
                uxOrderIDLabel.Text = CurrentOrderID;
                uxParentOrderLink.Visible = false;
                uxParentOrderEndLabel.Visible = false;
            }

            //if (String.IsNullOrEmpty( order.PaymentToken ))
            //    uxViewCreditCardLink.Visible = false;
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        uxGiftCertificateTR.Visible = DataAccessContext.Configurations.GetBoolValue( "GiftCertificateEnabled" );
        uxTrackingLink.Visible = true;

        if (uxItemGrid.Rows.Count > 0)
        {
            DeleteVisible( true );
        }
        else
        {
            DeleteVisible( false );
        }

        if (uxFormView.CurrentMode == FormViewMode.Edit)
        {
            uxEditTopButton.Visible = false;
            uxPrintTopButton.Visible = false;
        }
        else
        {
            uxEditTopButton.Visible = true;
        }

        ApplyPermissions();
    }

    protected void uxStatusDrop_PreRender( object sender, EventArgs e )
    {
        DropDownList drop = (DropDownList) uxFormView.Row.FindControl( "uxStatusDrop" );
        if (!IsDropDownInitialized( drop ))
        {
            string currentStatus = ((Order) uxFormView.DataItem).Status;

            drop.Items.Clear();
            drop.Items.Add( "Please select..." );
            drop.SelectedIndex = 0;

            string[] statusList = DataAccessContext.Configurations.GetValueList( "OrderStatusList" );

            for (int i = 0; i < statusList.Length; i++)
            {
                drop.Items.Add( statusList[i] );
                if (currentStatus == statusList[i])
                    drop.SelectedIndex = i + 1;
            }
        }
    }

    protected void uxAvsAddrDrop_PreRender( object sender, EventArgs e )
    {
        DropDownList drop = (DropDownList) uxFormView.Row.FindControl( "uxAvsAddrDrop" );
        drop.SelectedValue = ((Order) uxFormView.DataItem).AvsAddrStatus;
    }

    protected void uxAvsZipDrop_PreRender( object sender, EventArgs e )
    {
        DropDownList drop = (DropDownList) uxFormView.Row.FindControl( "uxAvsZipDrop" );
        drop.SelectedValue = ((Order) uxFormView.DataItem).AvsZipStatus;
    }

    protected void uxCvvDrop_PreRender( object sender, EventArgs e )
    {
        DropDownList drop = (DropDownList) uxFormView.Row.FindControl( "uxCvvDrop" );
        drop.SelectedValue = ((Order) uxFormView.DataItem).CvvStatus;
    }

    protected void uxTrackingMethodDrop_DataBound( object sender, EventArgs e )
    {
        DropDownList drop = (DropDownList) sender;

        string currentTrackingMethod = ((Order) uxFormView.DataItem).TrackingMethod;
        if (string.IsNullOrEmpty( currentTrackingMethod ))
            currentTrackingMethod = "Other";
        drop.SelectedValue = currentTrackingMethod;
    }

    protected void uxEditButton_PreRender( object sender, EventArgs e )
    {
        if (!IsAdminModifiable())
        {
            Button button = (Button) sender;
            button.Visible = false;
        }
    }

    protected void uxEditButton_Command( object sender, CommandEventArgs e )
    {
        uxFormView.ChangeMode( FormViewMode.Edit );

    }
    protected void uxState_RefreshHandler( object sender, EventArgs e )
    {
        AdminAdvanced_Components_Common_StateList stateList = (AdminAdvanced_Components_Common_StateList) uxFormView.FindControl( "uxStateList" );
        AdminAdvanced_Components_Common_CountryList countryList = (AdminAdvanced_Components_Common_CountryList) uxFormView.FindControl( "uxCountryList" );
        stateList.CountryCode = countryList.CurrentSelected;
        stateList.Refresh();
    }

    protected void uxShippingState_RefreshHandler( object sender, EventArgs e )
    {
        AdminAdvanced_Components_Common_StateList shippingStateList = (AdminAdvanced_Components_Common_StateList) uxFormView.FindControl( "StateListShipping" );
        AdminAdvanced_Components_Common_CountryList shippingCountryList = (AdminAdvanced_Components_Common_CountryList) uxFormView.FindControl( "CountryListShipping" );

        shippingStateList.CountryCode = shippingCountryList.CurrentSelected;
        shippingStateList.Refresh();
    }

    protected void uxTaxExemptStateList_RefreshHandler( object sender, EventArgs e )
    {
        AdminAdvanced_Components_Common_StateList taxExemptStateList = (AdminAdvanced_Components_Common_StateList) uxFormView.FindControl( "uxTaxExemptStateList" );
        AdminAdvanced_Components_Common_CountryList taxExemptCountryList = (AdminAdvanced_Components_Common_CountryList) uxFormView.FindControl( "uxTaxExemptCountryList" );

        taxExemptStateList.CountryCode = taxExemptCountryList.CurrentSelected;
        taxExemptStateList.Refresh();
    }

    protected void uxFormView_DataBound( object sender, EventArgs e )
    {
        FormView formView = (FormView) sender;
        Order order = (Order) formView.DataItem;

        if (!String.IsNullOrEmpty( order.OrderID ))
        {
            uxProductCostLabel.Text = AdminUtilities.FormatPrice( order.Subtotal );
            uxDiscountLabel.Text = AdminUtilities.FormatPrice( order.CouponDiscount * (-1) );
            uxPointDiscountLabel.Text = AdminUtilities.FormatPrice( order.RedeemPrice * (-1) );
            uxGiftCertificateLabel.Text = AdminUtilities.FormatPrice( order.GiftCertificate * (-1) );
            uxTaxLabel.Text = AdminUtilities.FormatPrice( order.Tax );
            uxShippingCostLabel.Text = AdminUtilities.FormatPrice( order.ShippingCost );
            uxHandlindFeeLabel.Text = AdminUtilities.FormatPrice( order.HandlingFee );

            uxTotalLabel.Text = AdminUtilities.FormatPrice( order.Total );
            UxHandlingFeeTR.Visible = IsHandlingFeeVisible();
        }
    }

    protected void uxUpdateButton_Command( object sender, CommandEventArgs e )
    {
        try
        {
            UpdateOrder();

            uxMessage.DisplayMessage( Resources.OrdersMessages.UpdateSuccess );
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
        finally
        {
            uxFormView.ChangeMode( FormViewMode.ReadOnly );
        }
    }

    protected void uxPrintButton_Load( object sender, EventArgs e )
    {
        Button button = (Button) sender;

        string script = String.Format( "var strCompany = '{0}';" +
            "getPrint('PrintArea', strCompany, '{1}');", GetCompanyData().Replace( "'", "\\'" ), CurrentOrderID );

        button.Attributes["onclick"] = script;
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "OrderItemsAdd.ascx", String.Format( "OrderID={0}", CurrentOrderID ) );
    }

    protected void CreateInvoice_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "Invoice.ascx", String.Format( "OrderID={0}&action=invoice", CurrentOrderID ) );
    }

    protected void CreatePackingSlip_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "Invoice.ascx", String.Format( "OrderID={0}&action=packingslip", CurrentOrderID ) );
    }

    protected void uxTrackingLink_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "OrderTracking.ascx", String.Format( "OrderID={0}", CurrentOrderID ) );
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        try
        {
            bool deleted = false;
            foreach (GridViewRow row in uxItemGrid.Rows)
            {
                CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
                if (deleteCheck.Checked)
                {
                    HiddenField hd = (HiddenField) row.FindControl( "uxHidden" );
                    string id = hd.Value;

                    DataAccessContext.OrderItemRepository.Delete( id );
                    deleted = true;
                }
            }

            if (deleted)
                uxMessage.DisplayMessage( Resources.OrdersMessages.DeleteSuccess );
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        OrderEditService orderEditService = new OrderEditService();
        orderEditService.RefreshOrderAmount( CurrentOrderID );

        uxFormView.DataBind();
        uxItemGrid.DataBind();
    }

    protected bool HaveTrackingNo( string trackingNumber )
    {
        if (string.IsNullOrEmpty( trackingNumber ))
            return false;
        else
            return true;
    }

    protected void SendTrackingNumber_Click( object sender, EventArgs e )
    {
        SendTrackingMail();
    }

    protected void uxSendTrackingNumberButton_PreRender( object sender, EventArgs e )
    {
        Button button = (Button) sender;
        WebUtilities.ButtonAddConfirmation( button, "Send tracking number?" );
    }

    protected void uxSendGiftCertificateButton_Click( object sender, EventArgs e )
    {
        SendGiftCertificateMail();
    }

    protected void uxSendGiftCertificateButton_PreRender( object sender, EventArgs e )
    {
        Button button = (Button) sender;
        WebUtilities.ButtonAddConfirmation( button, "Send gift certificate code mail?" );
    }

    protected void uxSendSubscriptionMailButton_Click( object sender, EventArgs e )
    {
        CurrentOrder.SendSubscriptionContentEmailByOrderID();
        uxMessage.DisplayMessage( Resources.OrdersMessages.SendSubscriptionMailComplete );
    }

    protected void uxSendSubscriptionMailButton_PreRender( object sender, EventArgs e )
    {
        Button button = (Button) sender;
        WebUtilities.ButtonAddConfirmation( button, "Send subscription content mail?" );
    }

    protected bool TrackingVisible()
    {
        return true;
    }

    protected bool HaveGiftCertificate()
    {
        if (IsAdminModifiable())
        {
            IList<GiftCertificate> giftList = DataAccessContext.GiftCertificateRepository.GetAllByOrderID( CurrentOrderID );
            Order order = DataAccessContext.OrderRepository.GetOne( CurrentOrderID );
            if (giftList.Count > 0 && order.PaymentComplete)
                return true;
            else
                return false;
        }
        else
        {
            return false;
        }
    }

    protected bool HaveProductSubscription()
    {
        Order order = DataAccessContext.OrderRepository.GetOne( CurrentOrderID );
        if (IsAdminModifiable() && order.PaymentComplete)
        {
            IList<OrderItem> orderItemList = DataAccessContext.OrderItemRepository.GetByOrderID( CurrentOrderID );

            for (int i = 0; i < orderItemList.Count; i++)
            {
                Product product = DataAccessContext.ProductRepository.GetOne( AdminUtilities.CurrentCulture, orderItemList[i].ProductID, new StoreRetriever().GetCurrentStoreID() );
                ProductSubscription subscriptionItem = new ProductSubscription( product.ProductID );
                if (subscriptionItem.IsSubscriptionProduct())
                {
                    return true;
                }
            }
            return false;
        }
        else
            return false;
    }

    protected string GetOrderUserNameByOrderID( object orderID )
    {
        if (IsAdminModifiable() && orderID != null)
        {
            Order order = DataAccessContext.OrderRepository.GetOne( orderID.ToString() );
            return order.UserName;
        }
        else
            return String.Empty;
    }

    protected void uxSendEGoodEmailButton_Click( object sender, EventArgs e )
    {
        CurrentOrder.SendDownloadEmailByOrderID();
        uxMessage.DisplayMessage( "Already Send download link mail" );
    }

    protected bool HaveEGood()
    {
        Order order = DataAccessContext.OrderRepository.GetOne( CurrentOrderID );
        if (IsAdminModifiable() && order.PaymentComplete)
        {
            IList<OrderItem> orderItemList = DataAccessContext.OrderItemRepository.GetByOrderID( CurrentOrderID );

            for (int i = 0; i < orderItemList.Count; i++)
            {
                Product product = DataAccessContext.ProductRepository.GetOne( AdminUtilities.CurrentCulture, orderItemList[i].ProductID, new StoreRetriever().GetCurrentStoreID() );
                if (product.IsDownloadable)
                {
                    return true;
                }
            }
            return false;
        }
        else
            return false;
    }

    protected DateTime GetRecurringEndDate( object recurringID )
    {
        RecurringProfile recurringProfile = DataAccessContext.RecurringProfileRepository.GetOne( recurringID.ToString() );
        return recurringProfile.EndRecurringDate;
    }

    protected void uxViewCreditCardLink_Load( Object sender, EventArgs e )
    {
        Order order = DataAccessContext.OrderRepository.GetOne( CurrentOrderID );
        LinkButton uxViewCreditCardLink = (LinkButton) sender;
        if (String.IsNullOrEmpty( order.PaymentToken ))
            uxViewCreditCardLink.Visible = false;
        else
        {
            string redirectUrl = PaymentAppGateway.GetPaymentAppUrl( "/ViewCreditCard.aspx?Token=", UrlPath.StorefrontUrl );

            string script = String.Format( "window.open( '{0}', 'mywin','toolbar=no,resizable=yes,scrollbars=yes' ); return false;",
                redirectUrl + order.PaymentToken );

            uxViewCreditCardLink.Attributes.Add( "onclick", script );
            uxViewCreditCardLink.Visible = true;
        }
    }

    protected string GetStoreName( string storeID )
    {
        Store store = DataAccessContext.StoreRepository.GetOne( storeID );
        return store.StoreName;
    }

    protected bool IsSaleTaxExemptVisible( object isTaxExempt )
    {
        if (uxFormView.CurrentMode == FormViewMode.ReadOnly)
        {
            return DataAccessContext.Configurations.GetBoolValue( "SaleTaxExempt" ) || ConvertUtilities.ToBoolean( isTaxExempt );
        }
        else
        {
            return DataAccessContext.Configurations.GetBoolValue( "SaleTaxExempt" ) && ConvertUtilities.ToBoolean( isTaxExempt );
        }
    }

    protected bool DisplayPointDiscount()
    {
        Order order = DataAccessContext.OrderRepository.GetOne( CurrentOrderID );

        if (DataAccessContext.Configurations.GetBoolValue( "PointSystemEnabled", StoreContext.CurrentStore ))
        {
            return true;
        }

        if (order.RedeemPoint > 0 || order.RedeemPrice > 0)
            return true;

        return false;
    }

    #endregion

}
