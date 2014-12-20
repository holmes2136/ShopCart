using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Payments;
using Vevo.Domain.Payments.PayPalProExpress;
using Vevo.Domain.Shipping;
using Vevo.Shared.Utilities;
using Vevo.WebUI;
using Vevo.WebUI.Orders;

public partial class OnePageCheckout : BaseProcessCheckoutPage
{
    private enum CheckoutPageState
    {
        Error = 0,
        ShippingAddress = 1,
        ShippingOptions = 2,
        PaymentMethods = 3,
        PaymentInfo = 4
    }

    private bool isRestoredShippingOption = false;
    private bool isRestoredShippingMethod = false;

    private CheckoutPageState CurrentState
    {
        get
        {
            if ( ViewState[ "CurrentState" ] == null ||
                ViewState[ "CurrentState" ].ToString() == String.Empty )
            {
                return CheckoutPageState.ShippingAddress;
            }
            else
            {
                return ( CheckoutPageState ) ViewState[ "CurrentState" ];
            }
        }
        set
        {
            ViewState[ "CurrentState" ] = value;
        }
    }

    private void SwitchPanelByState( CheckoutPageState state )
    {
        CurrentState = state;
        if ( CurrentState == CheckoutPageState.Error )
        {
            uxPanelAll.Visible = false;
            uxPanelMessage.Visible = true;
        }
        else
        {
            uxPanelAll.Visible = true;
            uxPanelMessage.Visible = false;
        }

        if ( CurrentState == CheckoutPageState.ShippingAddress )
        {
            uxShippingAddressPanel.Visible = true;
            uxShippingOptionPanel.Visible = false;
            uxPaymentMethodsPanel.Visible = false;
            uxPaymentInfoPanel.Visible = false;
        }
        else if ( CurrentState == CheckoutPageState.ShippingOptions )
        {
            uxShippingAddressPanel.Visible = false;
            uxShippingOptionPanel.Visible = true;
            uxPaymentMethodsPanel.Visible = false;
            uxPaymentInfoPanel.Visible = false;
        }
        else if ( CurrentState == CheckoutPageState.PaymentMethods )
        {
            uxShippingAddressPanel.Visible = false;
            uxShippingOptionPanel.Visible = false;
            uxPaymentMethodsPanel.Visible = true;
            uxPaymentInfoPanel.Visible = false;
        }
        else
        {
            uxShippingAddressPanel.Visible = false;
            uxShippingOptionPanel.Visible = false;
            uxPaymentMethodsPanel.Visible = false;
            uxPaymentInfoPanel.Visible = true;
        }
    }

    private void PopulateByState()
    {
        if ( CurrentState == CheckoutPageState.ShippingAddress )
        {
            uxCheckoutShippingAddress.PopulateControls();
        }
        else if ( CurrentState == CheckoutPageState.ShippingOptions )
        {
            uxCheckoutShippingOption.PopulateControls();
        }
        else if ( CurrentState == CheckoutPageState.PaymentMethods )
        {
            uxCheckoutPaymentMethods.PopulateControls();
        }
        else if ( CurrentState == CheckoutPageState.PaymentInfo )
        {
            uxCheckoutPaymentInfo.PopulateControls( Master.Controls );
        }
    }

    private void RegisterStoreEvents()
    {
        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler( Shipping_StoreCultureChanged );
        GetStorefrontEvents().StoreCurrencyChanged +=
            new StorefrontEvents.CurrencyEventHandler( Shipping_CurrencyChanged );
    }

    private void Shipping_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        SwitchPanelByState( CurrentState );
        PopulateByState();
    }

    private void Shipping_CurrencyChanged( object send, CurrencyEventArgs e )
    {
        SwitchPanelByState( CurrentState );
        PopulateByState();
    }

    private void ExtractListItemValue(
    string listItemValue,
    out string shippingID,
    out decimal shippingCost,
    out decimal handlingFee )
    {
        string[] selectItem = listItemValue.Split( '-' );

        shippingID = selectItem[ 0 ];
        shippingCost = ConvertUtilities.ToDecimal( selectItem[ 1 ] );
        handlingFee = ConvertUtilities.ToDecimal( selectItem[ 2 ] );
    }

    private string ExtractNameFromListItemText( string listItemName )
    {
        int index = listItemName.LastIndexOf( "(" );
        if ( index > 0 )
            return listItemName.Substring( 0, index ).Trim();
        else
            return listItemName;
    }

    private bool HasCoupon()
    {
        return !String.IsNullOrEmpty( StoreContext.CheckoutDetails.Coupon.CouponID );
    }

    private bool HasGiftCertificate()
    {
        return !StoreContext.CheckoutDetails.GiftCertificate.IsNull;
    }

    private bool SetShippingAddressNext()
    {
        if ( !uxCheckoutShippingAddress.IsShippingAddressReady() )
        {
            return false;
        }

        if ( uxCheckoutShippingAddress.VerifyCountryAndState() )
        {
            if ( !IsAnonymousCheckout() )
                uxCheckoutShippingAddress.SetShippingAddress();
            else
                uxCheckoutShippingAddress.SetAnonymousAddress();

            uxCheckoutShippingAddress.SetSaleTaxExempt();
            uxCheckoutShippingAddress.SetSpecialRequest();

            return true;
        }
        return false;
    }

    private bool SetShippingOptionNext()
    {
        string shippingID;
        decimal shippingCost, handlingFee;
        ExtractListItemValue( uxCheckoutShippingOption.ShippingOptionList.SelectedValue,
            out shippingID, out shippingCost, out handlingFee );

        ShippingOption shippingOption = DataAccessContext.ShippingOptionRepository.GetOne(
            StoreContext.Culture, shippingID );

        if ( shippingOption.ShippingOptionType.IsRealTime
            && StoreContext.ShoppingCart.ContainsRecurringProduct() )
        {
            uxCheckoutShippingOption.DisplayRecurringWarningMessage();
            return false;
        }

        ShippingChoice shippingChoice = new ShippingChoice(
            ExtractNameFromListItemText( uxCheckoutShippingOption.ShippingOptionList.SelectedItem.Text ), shippingCost, handlingFee );

        ShippingMethod shippingMethod = shippingOption.CreateShippingMethod( shippingChoice );
        StoreContext.CheckoutDetails.ShippingMethod = shippingMethod;
        return true;
    }

    private bool SetPaymentMethodsNext()
    {
        if ( !uxCheckoutPaymentMethods.IsAnyPaymentSelected() )
        {
            uxCheckoutPaymentMethods.DisplayError( "[$ErrorNoPaymentSelected]" );
            return false;
        }
        else if ( uxCheckoutPaymentMethods.IsPolicyAgreementEnabled() && !uxCheckoutPaymentMethods.IsAgreeChecked() )
        {
            uxCheckoutPaymentMethods.DisplayPolicyAgreementError("[$ErrorNotCheckPolicyAgreement]");
            return false;
        }

        string paymentMethodName = uxCheckoutPaymentMethods.GetSelectedPaymentName();
        PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne(
            StoreContext.Culture, paymentMethodName );

        PaymentMethod paymentMethod = paymentOption.CreatePaymentMethod();
        paymentMethod.SecondaryName = uxCheckoutPaymentMethods.GetSecondaryPaymentName();

        string poNumber = String.Empty;

        if ( uxCheckoutPaymentMethods.IsPONumberEmpty( out poNumber ) )
        {
            uxCheckoutPaymentMethods.DisplayError( "[$ErrorPONumberRequired]" );
            return false;
        }

        paymentMethod.PONumber = poNumber;
        StoreContext.CheckoutDetails.SetPaymentMethod( paymentMethod );
        return true;
    }

    private bool IsAnonymousCheckout()
    {
        if ( Request.QueryString[ "skiplogin" ] == "true" && !Roles.IsUserInRole( "Customers" ) )
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private int GetPaymentGatewayAmount()
    {
        return DataAccessContext.PaymentOptionRepository.GetShownPaymentList(
            StoreContext.Culture, BoolFilter.ShowTrue ).Count;
    }

    private int GetShippingOptionAmount()
    {
        return DataAccessContext.ShippingOptionRepository.GetShipping( StoreContext.Culture, BoolFilter.ShowAll ).Count;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        RegisterStoreEvents();

        if ( DataAccessContext.Configurations.GetValue( "CheckoutMode" ).ToString() == "Normal" )
        {
            if ( !IsAnonymousCheckout() )
                Response.Redirect( "Checkout.aspx" );
            else
                Response.Redirect( "Checkout.aspx?skiplogin=true" );
        }

        if ( StoreContext.ShoppingCart.GetCartItems().Length <= 0 )
        {
            uxShippingAddressNextImageButton.Visible = false;
            SwitchPanelByState( CheckoutPageState.ShippingAddress );
            uxCheckoutShippingAddress.PopulateControls();
            return;
        }

        if ( !Page.IsPostBack )
        {
            SwitchPanelByState( CheckoutPageState.ShippingAddress );
            uxCheckoutShippingAddress.PopulateControls();

            this.ClientScript.RegisterClientScriptInclude( "JSControls", "ClientScripts/controls.js" );
        }

        Vevo.WebUI.Ajax.AjaxUtilities.ScrollToTop( this );
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if ( GetShippingOptionAmount() <= 0 )
        {
            SwitchPanelByState( CheckoutPageState.Error );
            uxValidateMessage.DisplayError( "[$NoShippingOptionError]" );
            return;
        }
        if ( GetPaymentGatewayAmount() <= 0 )
        {
            SwitchPanelByState( CheckoutPageState.Error );
            uxValidateMessage.DisplayError( "[$NoPaymentGatewayError]" );
            return;
        }

        if ( CurrentState == CheckoutPageState.ShippingAddress )
        {
            if ( !Roles.IsUserInRole( "Customers" ) && !IsAnonymousCheckout() )
            {
                Response.Redirect( "UserLogin.aspx?ReturnUrl=Checkout.aspx" );
            }

            if ( !DataAccessContext.Configurations.GetBoolValue( "ShippingAddressMode" ) )
            {
                uxCheckoutShippingAddress.SetShippingAddress();

                SwitchPanelByState( CheckoutPageState.ShippingOptions );
                uxCheckoutShippingOption.PopulateControls();
                if (isRestoredShippingOption == false)
                {
                    uxCheckoutShippingOption.RestoreSelectedShippingOption();
                    isRestoredShippingOption = true;
                }
            }
        }
        else if ( CurrentState == CheckoutPageState.ShippingOptions )
        {
            uxCheckoutShippingAddress.SetBillingAddressAnonymous();
            if ( Request.QueryString[ "Token" ] != null )
            {
                PayPalProExpressPaymentMethod payment = ( PayPalProExpressPaymentMethod )
                    StoreContext.CheckoutDetails.PaymentMethod;

                if ( !payment.ProcessPostLoginDetails( Request.QueryString[ "Token" ], StoreContext.CheckoutDetails ) )
                {
                    CheckoutNotCompletePage.RedirectToPage(
                    "Error Message",
                    payment.GetErrorMessage(),
                    "ShoppingCart.aspx",
                    "Return To Shopping" );
                }
            }

            if ( uxCheckoutShippingOption.ShippingOptionList.Items.Count <= 0 )
            {
                uxShippingOptionNextImageButton.Visible = false;
                uxCheckoutShippingOption.DisplayNoShippingOption();
            }

            if ( ( StoreContext.ShoppingCart.GetCartItems().Length > 0 &&
            !StoreContext.ShoppingCart.ContainsShippableItem() ) ||
            !DataAccessContext.Configurations.GetBoolValue( "ShippingAddressMode" ) )
            {
                StoreContext.CheckoutDetails.SetShipping( ShippingMethod.Null );

                SwitchPanelByState( CheckoutPageState.PaymentMethods );
                uxCheckoutPaymentMethods.PopulateControls();
                if (isRestoredShippingMethod == false)
                {
                    uxCheckoutPaymentMethods.RestorePaymentMethod();
                    isRestoredShippingMethod = true;
                }
            }
        }
        else if ( CurrentState == CheckoutPageState.PaymentMethods )
        {
            PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne(
                StoreContext.Culture, StoreContext.CheckoutDetails.PaymentMethod.Name );

            if ( !StoreContext.ShoppingCart.ContainsRecurringProduct() )
            {
                if ( !paymentOption.PaymentMethodSelectionAllowed )
                {
                    if ( !( Request.QueryString[ "skiplogin" ] == "true" ) )
                        Response.Redirect( "OrderSummary.aspx" );
                    else
                        Response.Redirect( "OrderSummary.aspx?skiplogin=true" );
                }

                if ( StoreContext.GetOrderAmount().Total <= 0 )
                {
                    string paymentMethodName = PaymentOption.NoPaymentName;
                    string secondaryName = String.Empty;

                    if ( HasCoupon() || HasGiftCertificate() )
                    {
                        if ( HasCoupon() )
                            secondaryName += "Coupon";

                        if ( HasGiftCertificate() )
                        {
                            if ( HasCoupon() )
                                secondaryName += " / ";

                            secondaryName += "Gift Certificate";
                        }
                    }
                    PaymentOption zeroPaymentOption = DataAccessContext.PaymentOptionRepository.GetOne(
                        StoreContext.Culture, paymentMethodName );
                    PaymentMethod paymentMethod = zeroPaymentOption.CreatePaymentMethod();
                    paymentMethod.SecondaryName = secondaryName;
                    StoreContext.CheckoutDetails.SetPaymentMethod( paymentMethod );

                    if ( !( Request.QueryString[ "skiplogin" ] == "true" ) )
                        Response.Redirect( "OrderSummary.aspx" );
                    else
                        Response.Redirect( "OrderSummary.aspx?skiplogin=true" );
                }
            }

 
            if ( StoreContext.ShoppingCart.ContainsRecurringProduct() )
            {
                if ( uxCheckoutPaymentMethods.PaymentList.Items.Count > 1 )
                {
                    uxCheckoutPaymentMethods.DisplayError( "[$RecurringPaymentError]" );
                }
            }
        }
        else if ( CurrentState == CheckoutPageState.PaymentInfo )
        {
            PaymentMethod paymentMethod = StoreContext.CheckoutDetails.PaymentMethod;
            PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne( StoreContext.Culture, paymentMethod.Name );

            if ( !paymentOption.CreditCardRequired )
            {
                uxCheckoutPaymentInfo.SetCheckoutBillingAddress();

                if ( !IsAnonymousCheckout() )
                    Response.Redirect( "OrderSummary.aspx" );
                else
                    Response.Redirect( "OrderSummary.aspx?skiplogin=true" );
            }
        }
    }

    protected void uxShippingAddressNextImageButton_Click( object sender,EventArgs e )
    {
        if ( SetShippingAddressNext() )
        {
            uxShippingCountryHidden.Value = uxCheckoutShippingAddress.ShippingCountry;
            uxShippingStateHidden.Value = uxCheckoutShippingAddress.ShippingState;

            SwitchPanelByState( CheckoutPageState.ShippingOptions );

            uxCheckoutShippingOption.PopulateControls();
            if (isRestoredShippingOption == false)
            {
                uxCheckoutShippingOption.RestoreSelectedShippingOption();
                isRestoredShippingOption = true;
            }
        }
    }

    protected void uxShippingOptionBackImageButton_Click( object sender,EventArgs e )
    {
        uxCheckoutShippingAddress.ShippingCountry = uxShippingCountryHidden.Value;
        uxCheckoutShippingAddress.ShippingState = uxShippingStateHidden.Value;

        SwitchPanelByState( CheckoutPageState.ShippingAddress );
    }

    protected void uxShippingOptionNextImageButton_Click( object sender,EventArgs e )
    {
        if ( SetShippingOptionNext() )
        {
            SwitchPanelByState( CheckoutPageState.PaymentMethods );
            uxCheckoutPaymentMethods.PopulateControls();
            if (isRestoredShippingMethod == false)
            {
                uxCheckoutPaymentMethods.RestorePaymentMethod();
                isRestoredShippingMethod = true;
            }
        }
    }

    protected void uxPaymentMethodsBackImageButton_Click( object sender,EventArgs e )
    {
        if ( ( StoreContext.ShoppingCart.GetCartItems().Length > 0 &&
            !StoreContext.ShoppingCart.ContainsShippableItem() ) ||
            !DataAccessContext.Configurations.GetBoolValue( "ShippingAddressMode" ) )
        {
            SwitchPanelByState( CheckoutPageState.ShippingAddress );
        }
        else
        {
            SwitchPanelByState( CheckoutPageState.ShippingOptions );
        }
    }

    protected void uxPaymentMethodsNextImageButton_Click( object sender,EventArgs e )
    {
        if ( SetPaymentMethodsNext() )
        {
            SwitchPanelByState( CheckoutPageState.PaymentInfo );
            uxCheckoutPaymentInfo.PopulateControls( Master.Controls );
        }
    }
}
