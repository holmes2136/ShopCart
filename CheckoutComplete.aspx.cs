using System;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Payments;
using Vevo.Shared.Utilities;
using Vevo.WebUI;

public partial class CheckoutComplete : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private string CurrentOrderID
    {
        get
        {
            if (!String.IsNullOrEmpty( Request.QueryString["OrderID"] ))
                return Request.QueryString["OrderID"];
            else
                return "0";
        }
    }

    private bool IsEmailOK
    {
        get
        {
            if (!String.IsNullOrEmpty( Request.QueryString["IsEmailOK"] ))
                return ConvertUtilities.ToBoolean( Request.QueryString["IsEmailOK"] );
            else
                return true;
        }
    }

    private bool ShowOrderSummary
    {
        get
        {
            if (!String.IsNullOrEmpty( Request.QueryString["showorder"] ))
                return ConvertUtilities.ToBoolean( Request.QueryString["showorder"] );
            else
                return false;
        }
    }

    private void RegisterPrintButton()
    {
        this.ClientScript.RegisterClientScriptInclude( "JSPring", "ClientScripts/print.js" );

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

        string vevoData = siteName + "|" + companyName + "|" + address + "|" + city + "|" + state + "|" +
            zipCode + "|" + country + "|" + phone + "|" + fax + "|" + email;

        string storeTheme = DataAccessContext.Configurations.GetValue( "StoreTheme" );

        vevoData = vevoData.Replace( "'", "\\'" );

        string script = String.Format( "var strCompany = '{0}';" +
            "getPrint('PrintArea', strCompany,'{1}');", vevoData, storeTheme );

        uxPrintLink.Attributes.Add( "onclick", script );
    }

    private void DisplayEmailErrorMessage()
    {
        if (!IsEmailOK)
        {
            string errorMessage = "[$EmailErrorMessage]";
            if (DataAccessContext.Configurations.GetBoolValue( "ShowDetailAjaxErrorMessage" ) &&
                StoreError.Instance != null &&
                StoreError.Instance.Exception != null)
            {
                errorMessage += "<br/><br/>System Message: " + StoreError.Instance.Exception.Message;
            }

            uxEmailErrorMessage.DisplayError( errorMessage );
        }
    }

    private void DisplayCreditCardMultiChargeErrorMessage()
    {
        if (PaymentErrors.Instance.GetErrorMessage.Length > 0)
        {
            uxErrorMessage.DisplayError( PaymentErrors.Instance.GetErrorMessage );
            PaymentErrors.Instance.Clear();
        }
    }

    private void DisplayOrderSummary()
    {
        if (ShowOrderSummary)
        {
            uxOrderSummaryPanel.Style.Add( "display", "inline" );
            uxOrderCompletePanel.Style.Add( "display", "none" );
        }
        else
        {
            uxOrderSummaryPanel.Style.Add( "display", "none" );
            uxOrderCompletePanel.Style.Add( "display", "inline" );
            uxOrderIDLink.Text = CurrentOrderID.ToString();
            uxOrderIDLinkLabel.Text = CurrentOrderID.ToString();
        }
    }

    private void AlternatePaymentRow()
    {
        Label uxPONumber = (Label) uxOrderView.Row.FindControl( "uxPONumber" );
        HtmlTableRow uxPONumberTR = (HtmlTableRow) uxOrderView.Row.FindControl( "uxPONumberTR" );
        HtmlTableRow uxOrderDateTR = (HtmlTableRow) uxOrderView.Row.FindControl( "uxOrderDateTR" );
        HtmlTableRow uxTrackingTR = (HtmlTableRow) uxOrderView.Row.FindControl( "uxTrackingTR" );

        if (String.IsNullOrEmpty( uxPONumber.Text ))
        {
            uxPONumberTR.Visible = false;
            uxOrderDateTR.Attributes.Add( "class", "CheckoutCompleteGridViewAlternatingRowStyle" );
            uxTrackingTR.Attributes.Add( "class", "CheckoutCompleteGridViewRowStyle" );
        }
        else
        {
            uxPONumberTR.Visible = true;
            uxPONumberTR.Attributes.Add( "class", "CheckoutCompleteGridViewAlternatingRowStyle" );
            uxOrderDateTR.Attributes.Add( "class", "CheckoutCompleteGridViewRowStyle" );
            uxTrackingTR.Attributes.Add( "class", "CheckoutCompleteGridViewAlternatingRowStyle" );
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!IsPostBack)
        {
            RegisterPrintButton();
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!IsPostBack)
        {
            DisplayOrderSummary();
        }

        DisplayEmailErrorMessage();
        DisplayCreditCardMultiChargeErrorMessage();

        Order order = DataAccessContext.OrderRepository.GetOne( CurrentOrderID );
        if (StoreContext.CheckoutDetails.IsCreatedByAdmin)
        {
            StoreContext.ClearCheckoutSession();
            Response.Redirect( String.Format( "admin/Default.aspx#OrdersEdit,OrderID={0}", order.OrderID ) );
            return;
        }
        StoreContext.ClearCheckoutSession();

        uxGiftCertificateTR.Visible = DataAccessContext.Configurations.GetBoolValue( "GiftCertificateEnabled" );

        if ((Membership.GetUser() != null &&
            String.Compare( order.UserName, Membership.GetUser().UserName, true ) == 0) || order.UserName == SystemConst.AnonymousUser)
        {
            uxProductCostLabel.Text = StoreContext.Currency.FormatPrice( Convert.ToDecimal( order.Subtotal ) );
            uxDiscountLabel.Text = StoreContext.Currency.FormatPrice(
                Convert.ToDecimal( order.CouponDiscount * -1 ) );
            uxPointDiscountLabel.Text = StoreContext.Currency.FormatPrice( Convert.ToDecimal( order.RedeemPrice * -1 ) );
            uxGiftCertificateLabel.Text = StoreContext.Currency.FormatPrice(
                Convert.ToDecimal( order.GiftCertificate * -1 ) );
            uxTaxLabel.Text = StoreContext.Currency.FormatPrice( Convert.ToDecimal( order.Tax ) );
            uxShippingCostLabel.Text = StoreContext.Currency.FormatPrice(
                Convert.ToDecimal( order.ShippingCost ) );
            uxHandlingFeeLabel.Text = StoreContext.Currency.FormatPrice( Convert.ToDecimal( order.HandlingFee ) );
            uxTotalLabel.Text = StoreContext.Currency.FormatPrice( Convert.ToDecimal( order.Total ) );
            uxOrderIDLabel.Text = "[$intro] : " + CurrentOrderID;

            uxHandlingFeeTR.Visible = DataAccessContext.Configurations.GetBoolValue( "HandlingFeeEnabled" );
            uxOrderItemsGrid.DataBind();

            uxClientForm.Visible = false;
            if (order.UserName == SystemConst.AnonymousUser)
            {
                uxOrderIDLink.Visible = false;
                uxOrderIDLinkLabel.Visible = true;
                uxPrintLink.Visible = false;

            }
        }
        else
        {
            uxHeadPanel.Visible = false;
            uxSummaryPlaceHolder.Visible = false;
            uxPrintLink.Visible = false;

            uxErrorLiteral.Visible = true;
        }

        HtmlTableRow infoShippTR = (HtmlTableRow) uxOrderView.FindControl( "InfoShippTR" );
        HtmlTableRow shippDetailsTR = (HtmlTableRow) uxOrderView.FindControl( "ShippDetailsTR" );

        infoShippTR.Visible = order.ShowShippingAddress;
        shippDetailsTR.Visible = order.ShowShippingAddress;

        AlternatePaymentRow();
    }

    protected string ConvertPaymentMethod( string paymentMethodName )
    {
        PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne(
            StoreContext.Culture, PaymentOption.GetSimpleName( paymentMethodName ) );
        PaymentMethod paymentMethod = paymentOption.CreatePaymentMethod();
        if (paymentOption.ShownAsCreditCard)
            return "Credit Card";
        else
            return paymentMethodName;
    }

    public string GetTrackingUrl( object trackingMethod, object trackingNumber )
    {
        string trackingUrl;
        if (trackingMethod.ToString().ToLower() == "ups")
            trackingUrl = DataAccessContext.Configurations.GetValue( "UpsTrackingUrl" );
        else if (trackingMethod.ToString().ToLower() == "fedex")
            trackingUrl = DataAccessContext.Configurations.GetValue( "FedExTrackingUrl" );
        else
            trackingUrl = DataAccessContext.Configurations.GetValue( "OtherTrackingUrl" );

        if (!string.IsNullOrEmpty( trackingUrl ))
        {
            Regex regex = new Regex( @"\[tracknum\]", RegexOptions.IgnoreCase );
            trackingUrl = regex.Replace( trackingUrl, trackingNumber.ToString() );
        }
        return trackingUrl;
    }

    protected bool GetAddress2Visible( object address2 )
    {
        if (string.IsNullOrEmpty( address2.ToString() ))
            return false;
        else
            return true;
    }

    protected bool DisplayPointEarned()
    {
        Order order = DataAccessContext.OrderRepository.GetOne( CurrentOrderID );
        if (DataAccessContext.Configurations.GetBoolValue( "PointSystemEnabled", StoreContext.CurrentStore ))
        {
            if (ConvertUtilities.ToInt32( order.CustomerID ) == 0)
                return false;

            if (order.RedeemPoint > 0 || order.RedeemPrice > 0)
                return false;

            return true;
        }
        return false;
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

    protected void uxOrderIDLink_OnClick( object sender, EventArgs e )
    {
        uxOrderCompletePanel.Visible = false;
        uxOrderSummaryPanel.Style.Add( "display", "inline" );
    }
}
