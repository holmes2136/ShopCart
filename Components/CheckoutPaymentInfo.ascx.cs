using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo.Domain.Users;
using Vevo.Domain;
using Vevo.WebUI;
using Vevo.Domain.Payments;
using System.Text;
using Vevo.Shared.WebUI;
using System.Text.RegularExpressions;
using Vevo.Domain.Orders;
using Vevo.WebAppLib;
using Vevo.WebUI.International;
using System.Web.Security;
using Vevo.Base.Domain;

public partial class Components_CheckoutPaymentInfo : BaseLanguageUserControl
{
    private string EncodeSpecialCharacters( string text )
    {
        text = text.Replace( ";", "%3B" );
        text = text.Replace( "?", "%3F" );
        text = text.Replace( "/", "%2F" );
        text = text.Replace( ":", "%3A" );
        text = text.Replace( "#", "%23" );
        text = text.Replace( "&", "%26" );
        text = text.Replace( "=", "%3D" );
        text = text.Replace( "+", "%2B" );
        text = text.Replace( "$", "%24" );
        text = text.Replace( ",", "%2C" );
        text = text.Replace( " ", "%20" );
        text = text.Replace( "%", "%25" );
        text = text.Replace( "<", "%3C" );
        text = text.Replace( ">", "%3E" );
        text = text.Replace( "~", "%7E" );
        text = text.Replace( "'", "%27" );
        text = HttpUtility.UrlEncode(text);
        return text;
    }

    private bool VerifyThisPageControl( Control controlItem, string controlName )
    {
        Regex regex = new Regex( "((.+)(_|\\$))" + controlName.Trim() + "|\\b" + controlName.Trim() + "\\b" );

        if (regex.IsMatch( controlItem.ClientID ))
            return true;
        else
            return false;
    }

    private void AppendAndEncode( StringBuilder sb, string format, object value )
    {
        if (value != null)
            sb.AppendFormat( format, Server.HtmlEncode( value.ToString() ) );
    }

    private bool IsThisPageControl( Control controlItem )
    {
        bool result = false;
        if (VerifyThisPageControl( controlItem, "CreditCardType" ))
            result = true;
        else if (VerifyThisPageControl( controlItem, "ExpirationMonth" ))
            result = true;
        else if (VerifyThisPageControl( controlItem, "ExpirationYear" ))
            result = true;
        else if (VerifyThisPageControl( controlItem, "uxPayImageButton" ))
            result = true;
        return result;
    }

    private bool IsAnonymousCheckout()
    {
        if (Request.QueryString["skiplogin"] == "true")
            return true;
        else
            return false;
    }

    protected void Page_Load( object sender, EventArgs e )
    {

    }

    public void SetCheckoutBillingAddress()
    {
        if (Page.User.Identity.IsAuthenticated)
        {
            Customer customer = DataAccessContext.CustomerRepository.GetOne(
                DataAccessContext.CustomerRepository.GetIDFromUserName( Membership.GetUser().UserName ) );

            Address billingAddress = new Vevo.Base.Domain.Address(
                customer.BillingAddress.FirstName,
                customer.BillingAddress.LastName,
                customer.BillingAddress.Company,
                customer.BillingAddress.Address1,
                customer.BillingAddress.Address2,
                customer.BillingAddress.City,
                customer.BillingAddress.State,
                customer.BillingAddress.Zip,
                customer.BillingAddress.Country,
                customer.BillingAddress.Phone,
                customer.BillingAddress.Fax );

            StoreContext.CheckoutDetails.SetBillingDetails( billingAddress, customer.Email );
        }
    }

    public void SetJavaToControl( ControlCollection control )
    {
        foreach (Control controlItem in control)
        {
            if (controlItem.HasControls())
                SetJavaToControl( controlItem.Controls );
            else
            {
                if (!IsThisPageControl( controlItem ))
                {
                    if (controlItem is LinkButton)
                    {
                        LinkButton link = (LinkButton) controlItem;
                        link.Attributes.Add( "onclick", "ClearFormElements(document.getElementById('CreditCardSection'));" );
                    }
                    else if (controlItem is ImageButton)
                    {
                        ImageButton imageButton = (ImageButton) controlItem;
                        imageButton.Attributes.Add( "onclick", "ClearFormElements(document.getElementById('CreditCardSection'));" );
                    }
                    else if (controlItem is Button)
                    {
                        Button button = (Button) controlItem;
                        button.Attributes.Add( "onclick", "ClearFormElements(document.getElementById('CreditCardSection'));" );
                    }
                    else if (controlItem is DropDownList)
                    {
                        DropDownList dropdown = (DropDownList) controlItem;
                        dropdown.Attributes.Add( "onchange", "ClearFormElements(document.getElementById('CreditCardSection'));" );
                    }
                }
            }
        }
    }

    public void SetIFrameData()
    {
        PaymentOption paymentOption = DataAccessContext.PaymentOptionRepository.GetOne(
    StoreContext.Culture, StoreContext.CheckoutDetails.PaymentMethod.Name );

        string url = PaymentAppGateway.GetPaymentAppUrl( "/CreditCardInfo.aspx", UrlPath.StorefrontUrl );
        StringBuilder sb = new StringBuilder();
        AppendAndEncode( sb, "?Cvv2Required={0}", paymentOption.Cvv2Required );
        AppendAndEncode( sb, "&BillingAddressRequired={0}", paymentOption.BillingAddressRequired );
        AppendAndEncode( sb, "&SupportedCreditCards={0}", paymentOption.SupportedCreditCards );
        AppendAndEncode( sb, "&SupportedCreditCardValues={0}", paymentOption.SupportedCreditCardValues );

        // Set User Information.
        if (Page.User.Identity.IsAuthenticated)
        {
            Customer customer = DataAccessContext.CustomerRepository.GetOne(
                DataAccessContext.CustomerRepository.GetIDFromUserName( Membership.GetUser().UserName ) );
            AppendAndEncode( sb, "&BillingFirstName={0}"
                , EncodeSpecialCharacters( customer.BillingAddress.FirstName ) );
            AppendAndEncode( sb, "&BillingLastName={0}"
                , EncodeSpecialCharacters( customer.BillingAddress.LastName ) );
            AppendAndEncode( sb, "&BillingCompany={0}"
                , EncodeSpecialCharacters( customer.BillingAddress.Company ) );
            AppendAndEncode( sb, "&BillingAddress1={0}"
                , EncodeSpecialCharacters( customer.BillingAddress.Address1 ) );
            AppendAndEncode( sb, "&BillingAddress2={0}"
                , EncodeSpecialCharacters( customer.BillingAddress.Address2 ) );
            AppendAndEncode( sb, "&BillingCity={0}", EncodeSpecialCharacters( customer.BillingAddress.City ) );
            AppendAndEncode( sb, "&BillingZip={0}", EncodeSpecialCharacters( customer.BillingAddress.Zip ) );
            AppendAndEncode( sb, "&CurrentCountry={0}"
                , EncodeSpecialCharacters( customer.BillingAddress.Country ) );
            AppendAndEncode( sb, "&CurrentState={0}", EncodeSpecialCharacters( customer.BillingAddress.State ) );
            AppendAndEncode( sb, "&BillingPhone={0}", EncodeSpecialCharacters( customer.BillingAddress.Phone ) );
            AppendAndEncode( sb, "&BillingFax={0}", EncodeSpecialCharacters( customer.BillingAddress.Fax ) );
            AppendAndEncode( sb, "&Email={0}", EncodeSpecialCharacters( customer.Email ) );

            AppendAndEncode( sb, "&ShippingFirstName={0}", EncodeSpecialCharacters( customer.ShippingAddress.FirstName ) );
            AppendAndEncode( sb, "&ShippingLastName={0}", EncodeSpecialCharacters( customer.ShippingAddress.LastName ) );
            AppendAndEncode( sb, "&ShippingCompany={0}", EncodeSpecialCharacters( customer.ShippingAddress.Company ) );
            AppendAndEncode( sb, "&ShippingAddress1={0}", EncodeSpecialCharacters( customer.ShippingAddress.Address1 ) );
            AppendAndEncode( sb, "&ShippingAddress2={0}", EncodeSpecialCharacters( customer.ShippingAddress.Address2 ) );
            AppendAndEncode( sb, "&ShippingCity={0}", EncodeSpecialCharacters( customer.ShippingAddress.City ) );
            AppendAndEncode( sb, "&ShippingZip={0}", EncodeSpecialCharacters( customer.ShippingAddress.Zip ) );
            AppendAndEncode( sb, "&ShippingCountry={0}", EncodeSpecialCharacters( customer.ShippingAddress.Country ) );
            AppendAndEncode( sb, "&ShippingState={0}", EncodeSpecialCharacters( customer.ShippingAddress.State ) );
            AppendAndEncode( sb, "&ShippingPhone={0}", EncodeSpecialCharacters( customer.ShippingAddress.Phone ) );
            AppendAndEncode( sb, "&ShippingFax={0}", EncodeSpecialCharacters( customer.ShippingAddress.Fax ) );
        }
        else
        {
            CheckoutDetails checkout = StoreContext.CheckoutDetails;

            AppendAndEncode( sb, "&BillingFirstName={0}", EncodeSpecialCharacters( checkout.BillingAddress.FirstName ) );
            AppendAndEncode( sb, "&BillingLastName={0}", EncodeSpecialCharacters( checkout.BillingAddress.LastName ) );
            AppendAndEncode( sb, "&BillingCompany={0}", EncodeSpecialCharacters( checkout.BillingAddress.Company ) );
            AppendAndEncode( sb, "&BillingAddress1={0}", EncodeSpecialCharacters( checkout.BillingAddress.Address1 ) );
            AppendAndEncode( sb, "&BillingAddress2={0}", EncodeSpecialCharacters( checkout.BillingAddress.Address2 ) );
            AppendAndEncode( sb, "&BillingCity={0}", EncodeSpecialCharacters( checkout.BillingAddress.City ) );
            AppendAndEncode( sb, "&BillingZip={0}", EncodeSpecialCharacters( checkout.BillingAddress.Zip ) );
            AppendAndEncode( sb, "&CurrentCountry={0}", EncodeSpecialCharacters( checkout.BillingAddress.Country ) );
            AppendAndEncode( sb, "&CurrentState={0}", EncodeSpecialCharacters( checkout.BillingAddress.State ) );
            AppendAndEncode( sb, "&BillingPhone={0}", EncodeSpecialCharacters( checkout.BillingAddress.Phone ) );
            AppendAndEncode( sb, "&BillingFax={0}", EncodeSpecialCharacters( checkout.BillingAddress.Fax ) );
            AppendAndEncode( sb, "&Email={0}", checkout.Email );

            AppendAndEncode( sb, "&ShippingFirstName={0}", EncodeSpecialCharacters( checkout.ShippingAddress.FirstName ) );
            AppendAndEncode( sb, "&ShippingLastName={0}", EncodeSpecialCharacters( checkout.ShippingAddress.LastName ) );
            AppendAndEncode( sb, "&ShippingCompany={0}", EncodeSpecialCharacters( checkout.ShippingAddress.Company ) );
            AppendAndEncode( sb, "&ShippingAddress1={0}", EncodeSpecialCharacters( checkout.ShippingAddress.Address1 ) );
            AppendAndEncode( sb, "&ShippingAddress2={0}", EncodeSpecialCharacters( checkout.ShippingAddress.Address2 ) );
            AppendAndEncode( sb, "&ShippingCity={0}", EncodeSpecialCharacters( checkout.ShippingAddress.City ) );
            AppendAndEncode( sb, "&ShippingZip={0}", EncodeSpecialCharacters( checkout.ShippingAddress.Zip ) );
            AppendAndEncode( sb, "&ShippingCountry={0}", EncodeSpecialCharacters( checkout.ShippingAddress.Country ) );
            AppendAndEncode( sb, "&ShippingState={0}", EncodeSpecialCharacters( checkout.ShippingAddress.State ) );
            AppendAndEncode( sb, "&ShippingPhone={0}", EncodeSpecialCharacters( checkout.ShippingAddress.Phone ) );
            AppendAndEncode( sb, "&ShippingFax={0}", EncodeSpecialCharacters( checkout.ShippingAddress.Fax ) );
        }
        AppendAndEncode( sb, "&IPAddress={0}", WebUtilities.GetVisitorIP() );
        AppendAndEncode( sb, "&skiplogin={0}", IsAnonymousCheckout().ToString() );

        if (StoreContext.CheckoutDetails.PaymentMethod.Name == "OfflineCreditCard")
            sb.Append( "&IsOfflineData=True" );
        else
            sb.Append( "&IsOfflineData=False" );

        AppendAndEncode( sb, "&StoreTheme={0}", Page.StyleSheetTheme );

        //For Languagge
        AppendAndEncode( sb, "&a1={0}", "[$Credit]" );
        AppendAndEncode( sb, "&a2={0}", "[$Type]" );
        AppendAndEncode( sb, "&a21={0}", "[$Please Select]" );
        AppendAndEncode( sb, "&a3={0}", "[$CreditNumber]" );
        AppendAndEncode( sb, "&a4={0}", "[$CardholderName]" );
        AppendAndEncode( sb, "&a5={0}", "[$CreditVerifi]" );
        AppendAndEncode( sb, "&a6={0}", "[$Expiration]" );
        AppendAndEncode( sb, "&a7={0}", "[$CardIssue]" );
        AppendAndEncode( sb, "&a8={0}", "[$CardStart]" );

        AppendAndEncode( sb, "&b1={0}", "[$Billing Address]" );
        AppendAndEncode( sb, "&b2={0}", "[$Firstname]" );
        AppendAndEncode( sb, "&b3={0}", "[$Lastname]" );
        AppendAndEncode( sb, "&b4={0}", "[$Country]" );
        AppendAndEncode( sb, "&b5={0}", "[$State]" );
        AppendAndEncode( sb, "&b6={0}", "[$Company]" );
        AppendAndEncode( sb, "&b7={0}", "[$Address1]" );
        AppendAndEncode( sb, "&b8={0}", "[$City]" );
        AppendAndEncode( sb, "&b9={0}", "[$Zip]" );

        AppendAndEncode( sb, "&RedirectURL={0}", UrlPath.StorefrontUrl + "SaveBillingAddress.aspx" );        
        
        //reference = http://support.microsoft.com/default.aspx?scid=kb;EN-US;208427
        if (sb.Length + url.Length < 2083 && sb.Length < 2048)
            uxPaymentFrame.Attributes["src"] = url + sb.ToString();
        else
            throw new Exception(
                String.Format( "Query string exceedd maximum length: URL length {0}, Query length {1}",
                sb.Length + url.Length, sb.Length ) );
    }

    public void PopulateControls(ControlCollection controls)
    {
        SetIFrameData();
        SetJavaToControl( controls );
    }
}
