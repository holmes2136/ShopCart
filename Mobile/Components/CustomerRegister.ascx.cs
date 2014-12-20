using System;
using System.Web.Security;
using System.Web.UI;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Marketing;
using Vevo.Domain.Shipping;
using Vevo.Domain.Stores;
using Vevo.Domain.Users;
using Vevo.Shared.Utilities;
using Vevo.Shared.WebUI;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.Deluxe.WebUI.Marketing;
using Vevo.Base.Domain;
using System.Collections.Generic;

public partial class Mobile_Components_CustomerRegister : Vevo.WebUI.International.BaseLanguageUserControl
{
    #region Private

    private void RegisterSubmitButton()
    {
        WebUtilities.TieButton( this.Page, uxUserName, uxLinkButton );
        WebUtilities.TieButton( this.Page, uxFirstName, uxLinkButton );
        WebUtilities.TieButton( this.Page, uxLastName, uxLinkButton );
        WebUtilities.TieButton( this.Page, uxCompany, uxLinkButton );
        WebUtilities.TieButton( this.Page, uxAddress1, uxLinkButton );
        WebUtilities.TieButton( this.Page, uxAddress2, uxLinkButton );
        WebUtilities.TieButton( this.Page, uxCity, uxLinkButton );
        WebUtilities.TieButton( this.Page, uxZip, uxLinkButton );
        WebUtilities.TieButton( this.Page, uxPhone, uxLinkButton );
        WebUtilities.TieButton( this.Page, uxFax, uxLinkButton );
        WebUtilities.TieButton( this.Page, uxEmail, uxLinkButton );
        WebUtilities.TieButton( this.Page, uxUseBillingAsShipping, uxLinkButton );
        WebUtilities.TieButton( this.Page, uxShippingFirstName, uxLinkButton );
        WebUtilities.TieButton( this.Page, uxShippingLastName, uxLinkButton );
        WebUtilities.TieButton( this.Page, uxShippingCompany, uxLinkButton );
        WebUtilities.TieButton( this.Page, uxShippingAddress1, uxLinkButton );
        WebUtilities.TieButton( this.Page, uxShippingAddress2, uxLinkButton );
        WebUtilities.TieButton( this.Page, uxShippingCity, uxLinkButton );
        WebUtilities.TieButton( this.Page, uxShippingZip, uxLinkButton );
        WebUtilities.TieButton( this.Page, uxShippingPhone, uxLinkButton );
        WebUtilities.TieButton( this.Page, uxShippingFax, uxLinkButton );
        WebUtilities.TieButton( this.Page, uxShippingResidentialDrop, uxLinkButton );
    }

    private bool IsCheckOut
    {
        get
        {
            if (Request.QueryString["Checkout"] != null)
                return true;
            else
                return false;
            //return true;
        }
    }

    private bool IsCustomerAutoApprove
    {
        get
        {
            return DataAccessContext.Configurations.GetBoolValue( "CustomerAutoApprove" );
        }
    }

    private void CopyBillingDetailsToShipping()
    {
        uxShippingFirstName.Text = uxFirstName.Text;
        uxShippingLastName.Text = uxLastName.Text;
        uxShippingCompany.Text = uxCompany.Text;
        uxShippingAddress1.Text = uxAddress1.Text;
        uxShippingAddress2.Text = uxAddress2.Text;
        uxShippingCity.Text = uxCity.Text;
        uxShippingCountryState.CurrentCountry = uxCountryState.CurrentCountry;
        uxShippingCountryState.CurrentState = uxCountryState.CurrentState;
        uxShippingZip.Text = uxZip.Text;
        uxShippingPhone.Text = uxPhone.Text;
        uxShippingFax.Text = uxFax.Text;
    }

    private void SetCartShippingAddress( Customer customer )
    {
        StoreContext.CheckoutDetails.ShippingAddress = customer.ShippingAddress;
    }

    private void RedirectPage()
    {
        if (!IsCustomerAutoApprove)
            Response.Redirect( "RegisterFinish.aspx" );

        if (IsCheckOut && StoreContext.CheckoutDetails.ContainsGiftRegistry())
            Response.Redirect( "checkout.aspx" );

        if (Request.QueryString["ReturnUrl"] != null)
            Response.Redirect( Request.QueryString["ReturnUrl"] );
        else
            Response.Redirect( "Default.aspx" );
    }

    private void PopulateShipping()
    {
        uxUseBillingAsShipping.Attributes.Add(
            "onclick",
            String.Format( "TieCheckBoxForDisplay( this , '{0}' , '{1}|{2}|{3}|{4}|{5}' )",
                uxShippingInfoPanel.ClientID,
                uxShippingFirstNameRequired.ClientID,
                uxShippingLastNameRequired.ClientID,
                uxShippingAddress1Required.ClientID,
                uxShippingCityRequired.ClientID,
                uxShippingZipRequired.ClientID ) );

        if (uxUseBillingAsShipping.Checked || !DataAccessContext.Configurations.GetBoolValue( "ShippingAddressMode" ))
        {
            uxShippingInfoPanel.Style["Display"] = "none";
            uxShippingFirstNameRequired.Enabled = false;
            uxShippingLastNameRequired.Enabled = false;
            uxShippingAddress1Required.Enabled = false;
            uxShippingCityRequired.Enabled = false;
            uxShippingZipRequired.Enabled = false;
        }
        else
        {
            uxShippingInfoPanel.Style["Display"] = "block";
            uxShippingFirstNameRequired.Enabled = true;
            uxShippingLastNameRequired.Enabled = true;
            uxShippingAddress1Required.Enabled = true;
            uxShippingCityRequired.Enabled = true;
            uxShippingZipRequired.Enabled = true;
        }
    }

    private void ShowHideShippingResidential()
    {
        ShippingPolicy shippingPolicy = new ShippingPolicy();
        if (shippingPolicy.RequiresResidentialStatus())
            uxShippingResidentialPanel.Visible = true;
        else
            uxShippingResidentialPanel.Visible = false;
    }

    private void ShowHideControls()
    {
        if (DataAccessContext.Configurations.GetBoolValue( "ShippingAddressMode" ))
        {
            if (uxUseBillingAsShipping.Checked)
                uxShippingInfoPanel.Style["display"] = "none";
            else
                uxShippingInfoPanel.Style.Remove( "display" );
        }
        else
        {
            uxUseBillingAsShippingPanel.Visible = false;
            uxUseBillingAsShipping.Checked = true;
            uxShippingInfoPanel.Style["display"] = "none";
        }

        ShowHideShippingResidential();
    }

    private void SendMailToCustomer( string customerEmail, string userName )
    {
        if (DataAccessContext.Configurations.GetBoolValue( "NotifyNewRegistration" ))
        {
            string subjectText;
            string bodyText;

            EmailTemplateTextVariable.ReplaceNewCustomerRegistrationText( userName, customerEmail, out subjectText, out bodyText );

            WebUtilities.SendHtmlMail(
                NamedConfig.CompanyEmail,
                customerEmail,
                subjectText,
                bodyText );
        }
    }

    private void SendMailToMerchant( string customerEmail, string userName, string id )
    {
        if (!IsCustomerAutoApprove)
        {
            string subjectText;
            string bodyText;
            string customerViewUrl = UrlPath.StorefrontUrl + DataAccessContext.Configurations.GetValue( "AdminAdvancedFolder" ) +
            "/Default.aspx#CustomerEdit,CustomerID=" + id;

            EmailTemplateTextVariable.NewCustomerApproveRegistrationText(
                customerViewUrl, userName, customerEmail, out subjectText, out bodyText );

            WebUtilities.SendHtmlMail(
                NamedConfig.CompanyEmail,
                NamedConfig.CompanyEmail,
                subjectText,
                bodyText );
        }
    }

    private Customer SetUpCustomer()
    {
        Customer customer = new Customer();
        customer.UserName = uxUserName.Text.Trim();
        customer.BillingAddress = new Address( uxFirstName.Text, uxLastName.Text, uxCompany.Text,
            uxAddress1.Text, uxAddress2.Text, uxCity.Text, uxCountryState.CurrentState, uxZip.Text,
            uxCountryState.CurrentCountry, uxPhone.Text, uxFax.Text );
        customer.Email = uxEmail.Text.Trim();
        customer.UseBillingAsShipping = uxUseBillingAsShipping.Checked;

        ShippingAddress shippingAddress = new ShippingAddress( new Address(
                    uxShippingFirstName.Text,
                    uxShippingLastName.Text,
                    uxShippingCompany.Text,
                    uxShippingAddress1.Text,
                    uxShippingAddress2.Text,
                    uxShippingCity.Text,
                    uxShippingCountryState.CurrentState,
                    uxShippingZip.Text,
                    uxShippingCountryState.CurrentCountry,
                    uxShippingPhone.Text,
                    uxShippingFax.Text ),
                    ConvertUtilities.ToBoolean( uxShippingResidentialDrop.SelectedValue ) );

        shippingAddress.AliasName = uxShippingFirstName.Text + " " + uxShippingLastName.Text + " " + uxShippingAddress1.Text;

        if (uxUseBillingAsShipping.Checked)
        {
            shippingAddress.IsSameAsBillingAddress = true;
        }
        else
        {
            shippingAddress.IsSameAsBillingAddress = false;
        }

        customer.ShippingAddresses.Add( shippingAddress );

        if (!IsCustomerAutoApprove)
        {
            customer.IsEnabled = false;
        }
        String storeID = new StoreRetriever().GetStore().StoreID;
        IList<String> storeIDList = new List<String>();
        storeIDList.Add(storeID);
        customer.StoreIDs = storeIDList;
        return customer;

    }

    private string ValidateCountryAndState( bool validateCountry, bool validateState )
    {
        if (!validateCountry)
        {
            return "Required Country.";
        }
        else if (!validateState)
        {
            return "Required State.";
        }
        else
        {
            return "";
        }
    }

    private void AddCustomer()
    {
        if (Page.IsValid)
        {
            StoreRetriever storeRetriever = new StoreRetriever();
            Store store = storeRetriever.GetStore();
            bool validateCountry, validateState, validateShippingCountry, validateShippingState;
            bool validateBilling, validateShipping;
            validateBilling = uxCountryState.Validate( out validateCountry, out validateState );

            if (uxUseBillingAsShipping.Checked)
            {
                if (!validateBilling)
                {
                    uxBillingCountryStateDiv.Visible = true;
                    uxBillingCountryStateMessage.Text = ValidateCountryAndState( validateCountry, validateState );
                    return;
                }
            }
            else
            {
                validateShipping = uxShippingCountryState.Validate( out validateShippingCountry, out validateShippingState );
                if (!validateBilling && !validateShipping)
                {
                    uxBillingCountryStateDiv.Visible = true;
                    uxBillingCountryStateMessage.Text = ValidateCountryAndState( validateCountry, validateState );
                    uxShippingCountryStateDiv.Visible = true;
                    uxShippingCountryStateMessage.Text = ValidateCountryAndState( validateShippingCountry, validateShippingState );
                    return;
                }
                else if (!validateBilling)
                {
                    uxBillingCountryStateDiv.Visible = true;
                    uxBillingCountryStateMessage.Text = ValidateCountryAndState( validateCountry, validateState );
                    return;
                }
                else
                {
                    uxShippingCountryStateDiv.Visible = true;
                    uxShippingCountryStateMessage.Text = ValidateCountryAndState( validateShippingCountry, validateShippingState );
                    return;
                }
            }

            MembershipUser user = Membership.GetUser( uxUserName.Text.Trim() );
            if (user == null && !UsernameExits( uxUserName.Text.Trim() ))
            {
                if (uxUseBillingAsShipping.Checked)
                    CopyBillingDetailsToShipping();

                if (uxSubscribeCheckBox.Checked)
                {
                    string Email = uxEmail.Text.Trim();
                    string EmailHash =
                        SecurityUtilities.HashMD5( Email + WebConfiguration.SecretKey );
                    NewsLetter newsLetter = DataAccessContext.NewsLetterRepository.GetOne( Email, store );
                    if (newsLetter.IsNull)
                    {
                        newsLetter.Email = Email;
                        newsLetter.EmailHash = EmailHash;
                        newsLetter.JoinDate = DateTime.Now;
                        newsLetter.StoreID = store.StoreID;
                        DataAccessContext.NewsLetterRepository.Create( newsLetter );
                    }
                }

                string id;

                Customer customer = SetUpCustomer();
                customer = DataAccessContext.CustomerRepository.Save( customer );
                id = customer.CustomerID;

                Membership.CreateUser( uxUserName.Text.Trim(), uxPassword.Text, uxEmail.Text.Trim() );
                Roles.AddUserToRole( uxUserName.Text, "Customers" );

                if (IsCustomerAutoApprove)
                {
                    FormsAuthentication.SetAuthCookie( uxUserName.Text, false );
                }

                SetCartShippingAddress( customer );
                try
                {
                    SendMailToCustomer( uxEmail.Text, uxUserName.Text );
                    SendMailToMerchant( uxEmail.Text, uxUserName.Text, id );
                    AffiliateHelper.UpdateAffiliateReference( uxUserName.Text );
                    RedirectPage();
                }
                catch (Exception)
                {
                    uxMessage.DisplayError( "[$SentErrorMessage]" );
                    ClearData();
                }
            }
            else
            {
                uxMessage.DisplayError( "[$User Existed]" );
            }
        }
    }

    private bool UsernameExits( string username )
    {
        string id = DataAccessContext.CustomerRepository.GetIDFromUserName( username );
        if (id == "0")
            return false;
        else
            return true;
    }

    private void ClearData()
    {
        uxUserName.Text = String.Empty;
        uxPassword.Text = String.Empty;
        uxTextBoxConfrim.Text = String.Empty;
        uxFirstName.Text = String.Empty;
        uxLastName.Text = String.Empty;
        uxCompany.Text = String.Empty;
        uxAddress1.Text = String.Empty;
        uxAddress2.Text = String.Empty;
        uxCity.Text = String.Empty;
        uxZip.Text = String.Empty;
        uxPhone.Text = String.Empty;
        uxFax.Text = String.Empty;
        uxEmail.Text = String.Empty;
        uxSubscribeCheckBox.Checked = true;
        uxUseBillingAsShipping.Checked = true;
        uxCountryState.CurrentState = "";
        Country country = DataAccessContext.CountryRepository.GetOne( DataAccessContext.Configurations.GetValue( "StoreDefaultCountry", StoreContext.CurrentStore ).ToString() );
        uxCountryState.SetCountryByName( country.CommonName );
    }

    #endregion


    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        uxBillingCountryStateDiv.Visible = false;
        uxShippingCountryStateDiv.Visible = false;

        RegisterSubmitButton();
        PopulateShipping();

        if (IsPostBack)
        {
            uxUsernameValidDIV.Attributes.Add( "display", "" );
            uxUserName.Attributes.Add(
                "onchange", "var uxMessage = document.getElementById('"
                + uxUsernameValidDIV.ClientID + "');uxMessage.innerHTML = '';" );
        }
        else
            uxUsernameValidDIV.Attributes.Add( "display", "none" );

        if (IsCheckOut)
            uxGiftCouponDetail.Visible = true;
        else
            uxGiftCouponDetail.Visible = false;

    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        ShowHideControls();
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        if (IsCheckOut)
        {
            if (uxGiftCouponDetail.ValidateAndSetUp())
                AddCustomer();
        }
        else
            AddCustomer();
    }

    protected void uxAddImageButton_Click( object sender, ImageClickEventArgs e )
    {

        if (IsCheckOut)
        {
            if (uxGiftCouponDetail.ValidateAndSetUp())
                AddCustomer();
        }
        else
            AddCustomer();
    }
    #endregion
}
