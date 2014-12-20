using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using Vevo.Domain;
using Vevo.Domain.Payments;
using System.Web.UI.HtmlControls;
using Vevo.WebUI;
using System.Web.Security;
using Vevo;
using Vevo.Shared.Utilities;
using Vevo.Domain.Orders;

public partial class Mobile_CheckoutComplete : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
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

    protected void Page_Load( object sender, EventArgs e )
    {

    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        DisplayEmailErrorMessage();
        DisplayCreditCardMultiChargeErrorMessage();

        Order order = DataAccessContext.OrderRepository.GetOne( CurrentOrderID );

        uxGiftCertificateTR.Visible = DataAccessContext.Configurations.GetBoolValue( "GiftCertificateEnabled" );

        if ((Membership.GetUser() != null &&
            String.Compare( order.UserName, Membership.GetUser().UserName, true ) == 0) || order.UserName == SystemConst.AnonymousUser)
        {
            uxProductCostLabel.Text = StoreContext.Currency.FormatPrice( Convert.ToDecimal( order.Subtotal ) );
            uxDiscountLabel.Text = StoreContext.Currency.FormatPrice(
                Convert.ToDecimal( order.CouponDiscount * -1 ) );
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
        }
        else
        {
            uxSummaryPlaceHolder.Visible = false;

            uxErrorLiteral.Visible = true;
        }

        HtmlTableRow infoShippTR = (HtmlTableRow) uxOrderView.FindControl( "InfoShippTR" );
        HtmlTableRow shippDetailsTR = (HtmlTableRow) uxOrderView.FindControl( "ShippDetailsTR" );

        infoShippTR.Visible = order.ShowShippingAddress;
        shippDetailsTR.Visible = order.ShowShippingAddress;
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

}
