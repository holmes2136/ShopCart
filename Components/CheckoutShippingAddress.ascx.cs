using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using Vevo.Domain;
using Vevo.Domain.Marketing;
using Vevo.Domain.Shipping;
using Vevo.Domain.Users;
using Vevo.Shared.Utilities;
using Vevo.WebUI;
using Vevo.WebUI.International;
using Vevo.WebUI.Users;
using Vevo.Base.Domain;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.GiftRegistry;

public partial class Components_CheckoutShippingAddress : BaseLanguageUserControl
{
    #region Private

    private bool IsAnonymousCheckout()
    {
        if (Request.QueryString["skiplogin"] == "true" && !Roles.IsUserInRole( "Customers" ))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool VerifyBillingCountryAndState( out string errorMessage )
    {
        errorMessage = String.Empty;

        bool result = true;
        if (uxCountryAndState.IsRequiredCountry)
        {
            if (!uxCountryAndState.VerifyCountryIsValid)
            {
                errorMessage = "Required Country.";
                result = false;
            }
        }
        if (uxCountryAndState.IsRequiredState)
        {
            if (!uxCountryAndState.VerifyStateIsValid)
            {
                errorMessage = "Required State.";
                result = false;
            }
        }

        return result;
    }

    private bool VerifyShippingCountryAndState( out string errorMessage )
    {
        errorMessage = String.Empty;

        bool result = true;
        if (uxCountryState.IsRequiredCountry && !uxUseBillingAsShipping.Checked)
        {
            if (!uxCountryState.VerifyCountryIsValid)
            {
                errorMessage = "Required Country.";
                result = false;
            }
        }

        if (uxCountryState.IsRequiredState && !uxUseBillingAsShipping.Checked)
        {
            if (!uxCountryState.VerifyStateIsValid)
            {
                errorMessage = "Required State.";
                result = false;
            }
        }

        return result;
    }

    private bool VerifyTaxExemptCountryAndState( out string errorMessage )
    {
        errorMessage = String.Empty;

        bool result = true;
        if (uxTaxExemptCountryAndState.IsRequiredCountry)
        {
            if (!uxTaxExemptCountryAndState.VerifyCountryIsValid)
            {
                errorMessage = "Required Country.";
                result = false;
            }
        }

        if (uxTaxExemptCountryAndState.IsRequiredState)
        {
            if (!uxTaxExemptCountryAndState.VerifyStateIsValid)
            {
                errorMessage = "Required State.";
                result = false;
            }
        }

        return result;
    }

    private void PopulateShipping()
    {
        string objectList = String.Format( "{0}|{1}|{2}|{3}|{4}",
            uxShippingFirstNameRequired.ClientID,
            uxShippingLastNameRequired.ClientID,
            uxShippingAddress1Required.ClientID,
            uxShippingCityRequired.ClientID,
            uxShippingZipRequired.ClientID );

        uxUseBillingAsShipping.Attributes.Add( "onclick", String.Format(
            "TieCheckBoxForNonDisplay( this, '{0}', '{1}' )", uxShippingInfoPanel.ClientID, objectList ) );
        uxPreferShippingAddressRadio.Attributes.Add( "onclick", String.Format( "EnableDisableValidator( '{0}', false )", objectList ) );
        uxAnotherShippingAddressRadio.Attributes.Add( "onclick", String.Format( "EnableDisableValidator( '{0}', true )", objectList ) );

        if (uxUseBillingAsShipping.Checked)
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

    private StringBuilder AppendString( StringBuilder str, string strPrefix, string insertString )
    {
        if (!string.IsNullOrEmpty( insertString ))
        {
            str.Append( "<br />" );
            str.Append( strPrefix + insertString );
        }
        return str;
    }

    private void PopulatePreferAddress()
    {
        StringBuilder preferAddress = new StringBuilder();
        if (StoreContext.CheckoutDetails.ContainsGiftRegistry())
        {
            GiftRegistry giftRegistry = DataAccessContextDeluxe.GiftRegistryRepository.GetOne(
                StoreContext.CheckoutDetails.GiftRegistryID );

            if (!giftRegistry.HideAddress)
            {
                preferAddress.Append( giftRegistry.ShippingAddress.FirstName + " " + giftRegistry.ShippingAddress.LastName );
                preferAddress = AppendString( preferAddress, string.Empty, giftRegistry.ShippingAddress.Company );
                preferAddress = AppendString( preferAddress, string.Empty, giftRegistry.ShippingAddress.Address1 );
                preferAddress = AppendString( preferAddress, string.Empty, giftRegistry.ShippingAddress.Address2 );
                preferAddress.Append( "<br />" );
                preferAddress.Append( giftRegistry.ShippingAddress.City + "," + giftRegistry.ShippingAddress.State +
                    " " + giftRegistry.ShippingAddress.Zip );
                preferAddress.Append( "<br />" );
                preferAddress.Append(
                    AddressUtilities.GetOnlyCountryNameByCountryCode( giftRegistry.ShippingAddress.Country ) );
                preferAddress = AppendString( preferAddress, "Phone : ", giftRegistry.ShippingAddress.Phone );
                preferAddress = AppendString( preferAddress, "Fax : ", giftRegistry.ShippingAddress.Fax );

                uxPreferAddressLiteral.Text = preferAddress.ToString();
                uxShippingInfoPanel.CssClass = "CheckoutShippingInfoPanel1";
                uxSaleTaxExemptPanel.CssClass = "CheckoutShippingInfoPanel1";
                uxSpecialPanel.CssClass = "CheckoutShippingInfoPanel1";
            }
            else
                uxPreferAddressPanel.Attributes.Add( "class", "CheckoutPreferredAddressHide" );
        }
    }

    private void CheckUseBillingAsShipping()
    {
        uxShippingFirstName.Text = uxBillingFirstName.Text;
        uxShippingLastName.Text = uxBillingLastName.Text;
        uxShippingCompany.Text = uxBillingCompany.Text;
        uxShippingAddress1.Text = uxBillingAddress1.Text;
        uxShippingAddress2.Text = uxBillingAddress2.Text;
        uxShippingCity.Text = uxBillingCity.Text;
        uxCountryState.CurrentState = uxCountryAndState.CurrentState;
        uxShippingZip.Text = uxBillingZip.Text;
        uxCountryState.CurrentCountry = uxCountryAndState.CurrentCountry;
        uxShippingPhone.Text = uxBillingPhone.Text;
        uxShippingFax.Text = uxBillingFax.Text;

        uxShippingInfoPanel.Style["display"] = "none";
    }

    private void UnCheckUseBillingAsShipping()
    {
        uxShippingInfoPanel.Style["display"] = "";
    }

    private void ShowHideShippingResidential()
    {
        ShippingPolicy shippingPolicy = new ShippingPolicy();
        if (shippingPolicy.RequiresResidentialStatus())
        {
            uxShippingResidentialLabelDiv.Visible = true;
            uxShippingResidentialDataDiv.Visible = true;
        }
        else
        {
            uxShippingResidentialLabelDiv.Visible = false;
            uxShippingResidentialDataDiv.Visible = false;
        }
    }

    private void ShowHideSpecialRequest()
    {
        uxSpecialCheck.Attributes.Add( "onclick", String.Format( "TieCheckBoxForDisplay( this , '{0}' , '{1}' )",
            uxCustomerComments.ClientID, "" ) );

        if (uxSpecialCheck.Checked)
        {
            uxCustomerComments.Style["Display"] = "block";
        }
        else
        {
            uxCustomerComments.Style["Display"] = "none";
        }
    }

    private void ShowHideSaleTaxExempt()
    {
        uxTaxExemptCheck.Attributes.Add(
            "onclick",
            String.Format( "TieCheckBoxForDisplay( this , '{0}' , '{1}' )",
                uxCustomerTaxExemptPanel.ClientID,
                uxTaxExemptIDRequired.ClientID ) );

        if (IsSaleTaxExemptVisible())
        {
            if (uxTaxExemptCheck.Checked)
            {
                uxCustomerTaxExemptPanel.Style["Display"] = "block";
                uxTaxExemptIDRequired.Enabled = true;
            }
            else
            {
                uxCustomerTaxExemptPanel.Style["Display"] = "none";
                uxTaxExemptIDRequired.Enabled = false;
            }
        }
        else
        {
            uxTaxExemptCheck.Checked = false;
            uxTaxExemptIDRequired.Enabled = false;
            uxSaleTaxExemptPanel.Style["Display"] = "none";
        }
    }

    #endregion

    #region Protected

    protected void SetBillingAddress()
    {
        if (!IsAnonymousCheckout())
        {
            string id = DataAccessContext.CustomerRepository.GetIDFromUserName( Page.User.Identity.Name );
            Customer customer = DataAccessContext.CustomerRepository.GetOne( id );
            StoreContext.CheckoutDetails.SetBillingDetails( customer.BillingAddress, customer.Email );
        }
    }

    public void SetBillingAddressAnonymous()
    {
        if (IsAnonymousCheckout())
        {
            Address billingAddress = new Address(
                uxBillingFirstName.Text,
                uxBillingLastName.Text,
                uxBillingCompany.Text,
                uxBillingAddress1.Text,
                uxBillingAddress2.Text,
                uxBillingCity.Text,
                uxCountryAndState.CurrentState,
                uxBillingZip.Text,
                uxCountryAndState.CurrentCountry,
                uxBillingPhone.Text,
                uxBillingFax.Text );

            StoreContext.CheckoutDetails.SetBillingDetails( billingAddress, uxBillingEmail.Text );
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!IsPostBack)
        {
            string id = DataAccessContext.CustomerRepository.GetIDFromUserName( Page.User.Identity.Name );
            Customer customer = DataAccessContext.CustomerRepository.GetOne( id );

            IList<ShippingAddress> shippingAddressList = new List<ShippingAddress>();
            foreach (ShippingAddress address in customer.ShippingAddresses)
            {
                if (!address.AliasName.Equals( "Other shipping address.." ))
                {
                    shippingAddressList.Add( address );
                }
            }

            ShippingAddress newShippingAddress = new ShippingAddress();
            newShippingAddress.AliasName = "Other shipping address..";
            newShippingAddress.ShippingAddressID = "0";

            shippingAddressList.Add( newShippingAddress );
            uxShippingAddressDrop.Items.Clear();
            uxShippingAddressDrop.DataSource = shippingAddressList;
            uxShippingAddressDrop.DataBind();

            if (IsAnonymousCheckout())
            {
                uxUseBillingAsShipping.Checked = true;
            }

            PopulateControls();

            RestoreShippingAddress();
            RestoreTaxExempt();
            RestoreSpecialRequest();
        }

        PopulateShipping();
        SetBillingAddress();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {

        if (DisplayNoItemToCheckout().Length != 0)
            return;

        if (DataAccessContext.Configurations.GetBoolValue( "ShippingAddressMode" ))
        {
            if (uxUseBillingAsShipping.Checked)
            {
                CheckUseBillingAsShipping();
            }
            else
            {
                UnCheckUseBillingAsShipping();
            }
        }
        else
        {
            uxUseBillingAsShipping.Style["display"] = "none";
            uxShippingInfoPanel.Style["display"] = "none";
        }

        ShowHideSaleTaxExempt();
        ShowHideSpecialRequest();
        ShowHideShippingResidential();
    }

    protected bool IsSaleTaxExemptVisible()
    {
        return DataAccessContext.Configurations.GetBoolValue( "SaleTaxExempt" );
    }

    protected void uxShippingAddressDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        PopulateControls();
    }

    #endregion

    #region Public

    public string ShippingCountry
    {
        get
        {
            return uxCountryState.CurrentCountry;
        }
        set
        {
            uxCountryState.CurrentCountry = value;
        }
    }

    public string ShippingState
    {
        get
        {
            return uxCountryState.CurrentState;
        }
        set
        {
            uxCountryState.CurrentState = value;
        }
    }

    public bool IsShippingAddressReady()
    {
        if ((!uxAnotherShippingAddressRadio.Checked) &&
            (!uxPreferShippingAddressRadio.Checked) &&
            (StoreContext.CheckoutDetails.ContainsGiftRegistry()))
        {
            uxMessageLabel.Text = "Please select shipping address";
            return false;
        }
        else
        {
            return true;
        }
    }

    public void RestoreShippingAddress()
    {
        if (StoreContext.CheckoutDetails.BillingAddress.FirstName != "")
        {
            Address billingAddress = StoreContext.CheckoutDetails.BillingAddress;
            uxBillingFirstName.Text = billingAddress.FirstName;
            uxBillingLastName.Text = billingAddress.LastName;
            uxBillingCompany.Text = billingAddress.Company;
            uxBillingAddress1.Text = billingAddress.Address1;
            uxBillingAddress2.Text = billingAddress.Address2;
            uxBillingCity.Text = billingAddress.City;
            uxCountryAndState.CurrentState = billingAddress.State;
            uxBillingZip.Text = billingAddress.Zip;
            uxCountryAndState.CurrentCountry = billingAddress.Country;
            uxBillingPhone.Text = billingAddress.Phone;
            uxBillingFax.Text = billingAddress.Fax;
            uxBillingEmail.Text = StoreContext.CheckoutDetails.Email;
        }
        else
            return;

        if (StoreContext.CheckoutDetails.ShippingAddress.FirstName != "")
        {
            if (!IsAnonymousCheckout())
                uxShippingAddressDrop.SelectedValue = StoreContext.CheckoutDetails.ShippingAddress.ShippingAddressID;
            else
                uxUseBillingAsShipping.Checked = StoreContext.CheckoutDetails.ShippingAddress.IsSameAsBillingAddress;

            ShippingAddress shippingAddress = StoreContext.CheckoutDetails.ShippingAddress;
            uxShippingFirstName.Text = shippingAddress.FirstName;
            uxShippingLastName.Text = shippingAddress.LastName;
            uxShippingCompany.Text = shippingAddress.Company;
            uxShippingAddress1.Text = shippingAddress.Address1;
            uxShippingAddress2.Text = shippingAddress.Address2;
            uxShippingCity.Text = shippingAddress.City;
            uxCountryState.CurrentState = shippingAddress.State;
            uxShippingZip.Text = shippingAddress.Zip;
            uxCountryState.CurrentCountry = shippingAddress.Country;
            uxShippingPhone.Text = shippingAddress.Phone;
            uxShippingFax.Text = shippingAddress.Fax;
            uxShippingResidentialDrop.SelectedValue = shippingAddress.Residential.ToString();
            
        }
    }

    public void RestoreTaxExempt()
    {
        if (StoreContext.CheckoutDetails.IsTaxExempt)
        {
            uxTaxExemptCheck.Checked = true;
            uxTaxExemptID.Text = StoreContext.CheckoutDetails.TaxExemptID;
            uxTaxExemptCountryAndState.CurrentCountry = StoreContext.CheckoutDetails.TaxExemptCountry;
            uxTaxExemptCountryAndState.CurrentState = StoreContext.CheckoutDetails.TaxExemptState;
        }
        else
            uxTaxExemptCheck.Checked = false;

    }

    public void RestoreSpecialRequest()
    {
        if (!String.IsNullOrEmpty(StoreContext.CheckoutDetails.CustomerComments))
        {
            uxCustomerComments.Text = StoreContext.CheckoutDetails.CustomerComments;
            uxSpecialCheck.Checked = true;
        }
    }

    public void SetAnonymousAddress()
    {
        SetBillingAddress();

        if (uxUseBillingAsShipping.Checked)
        {
            uxShippingFirstName.Text = uxBillingFirstName.Text;
            uxShippingLastName.Text = uxBillingLastName.Text;
            uxShippingCompany.Text = uxBillingCompany.Text;
            uxShippingAddress1.Text = uxBillingAddress1.Text;
            uxShippingAddress2.Text = uxBillingAddress2.Text;
            uxShippingCity.Text = uxBillingCity.Text;
            uxCountryState.CurrentState = uxCountryAndState.CurrentState;
            uxShippingZip.Text = uxBillingZip.Text;
            uxCountryState.CurrentCountry = uxCountryAndState.CurrentCountry;
            uxShippingPhone.Text = uxBillingPhone.Text;
            uxShippingFax.Text = uxBillingFax.Text;
        }

        SetShippingAddress();
    }

    public void SetShippingAddress()
    {
        bool showShippingAddress = true;
        if (StoreContext.CheckoutDetails.ContainsGiftRegistry() &&
            uxPreferShippingAddressRadio.Checked)
        {
            string giftRegistryID = StoreContext.CheckoutDetails.GiftRegistryID;
            GiftRegistry giftRegistry = DataAccessContextDeluxe.GiftRegistryRepository.GetOne( giftRegistryID );
            StoreContext.CheckoutDetails.ShippingAddress = giftRegistry.ShippingAddress;

            showShippingAddress = !giftRegistry.HideAddress;
        }
        else
        {
            Address address = new Address(
                uxShippingFirstName.Text,
                uxShippingLastName.Text,
                uxShippingCompany.Text,
                uxShippingAddress1.Text,
                uxShippingAddress2.Text,
                uxShippingCity.Text,
                uxCountryState.CurrentState,
                uxShippingZip.Text,
                uxCountryState.CurrentCountry,
                uxShippingPhone.Text,
                uxShippingFax.Text
                );

            StoreContext.CheckoutDetails.ShippingAddress = new ShippingAddress(
                address, ConvertUtilities.ToBoolean( uxShippingResidentialDrop.SelectedValue ) );

            if (!IsAnonymousCheckout())
            {
                StoreContext.CheckoutDetails.ShippingAddress.ShippingAddressID = uxShippingAddressDrop.SelectedValue;
                StoreContext.CheckoutDetails.ShippingAddress.AliasName = uxShippingAddressDrop.Text; 
            }
        }
        StoreContext.CheckoutDetails.ShippingAddress.IsSameAsBillingAddress = uxUseBillingAsShipping.Checked;
        StoreContext.CheckoutDetails.SetShowShippingAddress( showShippingAddress );
    }

    public void PopulateControls()
    {
        string id = "";

        if (!IsAnonymousCheckout())
        {
            id = DataAccessContext.CustomerRepository.GetIDFromUserName( Membership.GetUser().UserName );
            Customer customer = DataAccessContext.CustomerRepository.GetOne( id );

            string shippingId = uxShippingAddressDrop.SelectedValue;

            if (shippingId != "0" && shippingId != "")
            {

                ShippingAddress shippingAddress = customer.GetShippingAddress( shippingId );
                uxShippingFirstName.Text = shippingAddress.FirstName;
                uxShippingLastName.Text = shippingAddress.LastName;
                uxShippingCompany.Text = shippingAddress.Company;
                uxShippingAddress1.Text = shippingAddress.Address1;
                uxShippingAddress2.Text = shippingAddress.Address2;
                uxShippingCity.Text = shippingAddress.City;
                uxShippingZip.Text = shippingAddress.Zip;

                uxCountryState.CurrentCountry = shippingAddress.Country;
                uxCountryState.CurrentState = shippingAddress.State;

                uxShippingPhone.Text = shippingAddress.Phone;
                uxShippingFax.Text = shippingAddress.Fax;

                uxShippingResidentialDrop.SelectedValue = shippingAddress.Residential.ToString();
            }
            else
            {
                uxShippingFirstName.Text = String.Empty;
                uxShippingLastName.Text = String.Empty;
                uxShippingCompany.Text = String.Empty;
                uxShippingAddress1.Text = String.Empty;
                uxShippingAddress2.Text = String.Empty;
                uxShippingCity.Text = String.Empty;
                uxShippingZip.Text = String.Empty;

                uxCountryState.CurrentCountry = String.Empty;
                uxCountryState.CurrentState = String.Empty;

                uxShippingPhone.Text = String.Empty;
                uxShippingFax.Text = String.Empty;
            }


            uxBillingInfoPanel.Visible = false;
            //uxShippingTitlePanel.Visible = false;

            if (!String.IsNullOrEmpty( customer.TaxExemptID ))
            {
                uxTaxExemptCheck.Checked = true;
                uxTaxExemptID.Text = customer.TaxExemptID;
                uxTaxExemptCountryAndState.CurrentCountry = customer.TaxExemptCountry;
                uxTaxExemptCountryAndState.CurrentState = customer.TaxExemptState;
            }
        }
        else
        {
            uxBillingInfoPanel.Visible = true;
            uxShippingAliasNamePanel.Visible = false;
        }
        if (!StoreContext.CheckoutDetails.ContainsGiftRegistry())
        {
            uxPreferGiftAddressPanel.Visible = false;
            uxAnotherShippingAddressRadio.Visible = false;
            uxUseBillingAsShipping.Visible = true;
        }
        else
            PopulatePreferAddress();
    }

    public bool VerifyCountryAndState()
    {
        bool billingValid = true;
        if (IsAnonymousCheckout())
        {
            string billingMessage;
            billingValid = VerifyBillingCountryAndState( out billingMessage );
            if (!String.IsNullOrEmpty( billingMessage ))
            {
                uxBillingCountryStateDiv.Visible = true;
                uxBillingCountryStateMessage.Text = billingMessage;
            }
        }

        bool shippingValid = true;
        if (!uxPreferShippingAddressRadio.Checked)
        {
            string shippingMessage;
            shippingValid = VerifyShippingCountryAndState( out shippingMessage );
            if (!String.IsNullOrEmpty( shippingMessage ))
            {
                uxShippingCountryStateDiv.Visible = true;
                uxShippingCountryStateMessage.Text = shippingMessage;
            }
        }

        bool taxExemptValid = true;
        if (uxTaxExemptCheck.Checked)
        {
            string taxExemptMessage;
            taxExemptValid = VerifyTaxExemptCountryAndState( out taxExemptMessage );
            if (!String.IsNullOrEmpty( taxExemptMessage ))
            {
                uxTaxExemptCountryStateDiv.Visible = true;
                uxTaxExemptCountryStateMessage.Text = taxExemptMessage;
            }
        }
        return billingValid && shippingValid && taxExemptValid;
    }

    public void SetSaleTaxExempt()
    {
        if (IsSaleTaxExemptVisible() && uxTaxExemptCheck.Checked)
        {
            StoreContext.CheckoutDetails.SetTaxExempt( true, uxTaxExemptID.Text,
                uxTaxExemptCountryAndState.CurrentCountry, uxTaxExemptCountryAndState.CurrentState );
        }
        else
        {
            StoreContext.CheckoutDetails.SetTaxExempt( false, String.Empty, String.Empty, String.Empty );
        }
    }

    public void SetSpecialRequest()
    {
        if (uxSpecialCheck.Checked)
        {
            StoreContext.CheckoutDetails.CustomerComments = uxCustomerComments.Text;
        }
    }

    public string DisplayNoItemToCheckout()
    {
        if (StoreContext.ShoppingCart.GetCartItems().Length == 0)
        {
            uxShippingInfoPanel.Style.Add( "Display", "none" );
            uxBillingInfoPanel.Visible = false;
            uxSaleTaxExemptPanel.Visible = false;
            uxSpecialPanel.Visible = false;
            return "You do not have any item to checkout.";
        }
        else
            return "";
    }

    #endregion


}
