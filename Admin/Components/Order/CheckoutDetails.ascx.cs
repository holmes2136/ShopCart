using System;
using System.Collections;
using System.Collections.Generic;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Shipping;
using Vevo.Domain.Stores;
using Vevo.Domain.Users;
using Vevo.Shared.Utilities;
using Vevo.WebUI;
using Vevo.Base.Domain;

public partial class Admin_Components_Order_CheckoutDetails : AdminAdvancedBaseUserControl
{
    private string CurrentCustomerID
    {
        get
        {
            return StoreContext.Customer.CustomerID;
        }
    }

    private string SelectedStoreID
    {
        get
        {
            if (MainContext.QueryString["StoreID"] == null)
                return Store.RegularStoreID;
            else
                return MainContext.QueryString["StoreID"];
        }
    }

    private Store CurrentStore
    {
        get
        {
            return DataAccessContext.StoreRepository.GetOne( SelectedStoreID );
        }
    }

    private void uxLanguageControl_RefreshHandler( object sender, EventArgs e )
    {
        uxSelectPaymentList.CultureID = uxLanguageControl.CurrentCultureID;
        uxSelectPaymentList.PopulateControls();

        uxSelectShippingList.CultureID = uxLanguageControl.CurrentCultureID;
        uxSelectShippingList.PopulateControls();
    }

    private void uxCurrencyControl_RefreshHandler( object sender, EventArgs e )
    {
        uxSelectShippingList.CurrencyCode = uxCurrencyControl.CurrentCurrencyCode;
        uxSelectShippingList.PopulateControls();
    }

    private void uxState_RefreshHandler( object sender, EventArgs e )
    {
        uxStateList.CountryCode = uxCountryList.CurrentSelected;
        uxStateList.Refresh();
    }

    private void uxShippingState_RefreshHandler( object sender, EventArgs e )
    {
        uxShippingStateList.CountryCode = uxShippingCountryList.CurrentSelected;
        uxShippingStateList.Refresh();
    }

    protected bool IsSaleTaxExemptVisible()
    {
        return DataAccessContext.Configurations.GetBoolValue( "SaleTaxExempt", CurrentStore );
    }


    private void uxTaxExemptState_RefreshHandler( object sender, EventArgs e )
    {
        uxTaxExemptStateList.CountryCode = uxTaxExemptCountryList.CurrentSelected;
        uxTaxExemptStateList.Refresh();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxCountryList.BubbleEvent += new EventHandler( uxState_RefreshHandler );
        uxShippingCountryList.BubbleEvent += new EventHandler( uxShippingState_RefreshHandler );
        uxTaxExemptCountryList.BubbleEvent += new EventHandler( uxTaxExemptState_RefreshHandler );
        uxLanguageControl.BubbleEvent += new EventHandler( uxLanguageControl_RefreshHandler );
        uxCurrencyControl.BubbleEvent += new EventHandler( uxCurrencyControl_RefreshHandler );

        if ((StoreContext.ShoppingCart.GetCartItems().Length > 0 &&
            !StoreContext.ShoppingCart.ContainsShippableItem()) ||
            !DataAccessContext.Configurations.GetBoolValue( "ShippingAddressMode" ))
        {
            uxShippingListDiv.Visible = false;
        }

        if (!MainContext.IsPostBack)
        {
            Customer customer = DataAccessContext.CustomerRepository.GetOne( CurrentCustomerID );
            IList<ShippingAddress> shippingAddressList = new List<ShippingAddress>();
            shippingAddressList = customer.ShippingAddresses.Clone();

            ShippingAddress newShippingAddress = new ShippingAddress();
            newShippingAddress.AliasName = "Other shipping address..";
            newShippingAddress.ShippingAddressID = "0";

            shippingAddressList.Add( newShippingAddress );
            uxShippingAddressDrop.Items.Clear();
            uxShippingAddressDrop.DataSource = shippingAddressList;
            uxShippingAddressDrop.DataBind();
            StoreContext.CheckoutDetails.StoreID = SelectedStoreID;
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            PopulateControls();
            uxUserName.Enabled = false;

            if (uxUseBillingAsShipping.Checked)
            {
                CheckUseBillingAsShipping();
            }
            else
            {
                UnCheckUseBillingAsShipping();
            }

            ShowHideSaleTaxExempt();
            ShowHideShippingResidential();
        }
    }

    private void SetBillingAddress()
    {

        Address billingAddress = new Address(
            uxFirstName.Text,
            uxLastName.Text,
            uxCompany.Text,
            uxAddress1.Text,
            uxAddress2.Text,
            uxCity.Text,
            uxStateList.CurrentSelected,
            uxZip.Text,
            uxCountryList.CurrentSelected,
            uxPhone.Text,
            uxFax.Text );

        StoreContext.CheckoutDetails.SetBillingDetails( billingAddress, uxEmail.Text );

    }


    private void SetShippingAddress()
    {
        Address billingAddress = new Address(
           uxFirstName.Text,
           uxLastName.Text,
           uxCompany.Text,
           uxAddress1.Text,
           uxAddress2.Text,
           uxCity.Text,
           uxStateList.CurrentSelected,
           uxZip.Text,
           uxCountryList.CurrentSelected,
           uxPhone.Text,
           uxFax.Text );

        bool showShippingAddress = true;
        Address address = new Address();
        if (uxUseBillingAsShipping.Checked)
        {
            CheckUseBillingAsShipping();

            address = billingAddress;
        }
        else
        {
            address = new Address(
                uxShippingFirstName.Text,
                uxShippingLastName.Text,
                uxShippingCompany.Text,
                uxShippingAddress1.Text,
                uxShippingAddress2.Text,
                uxShippingCity.Text,
                uxShippingStateList.CurrentSelected,
                uxShippingZip.Text,
                uxShippingCountryList.CurrentSelected,
                uxShippingPhone.Text,
                uxShippingFax.Text
                );
        }

        StoreContext.CheckoutDetails.ShippingAddress = new ShippingAddress(
            address, ConvertUtilities.ToBoolean( uxShippingResidentialDrop.SelectedValue ) );
        StoreContext.CheckoutDetails.SetShowShippingAddress( showShippingAddress );
    }

    private void PopulateControls()
    {
        if (CurrentCustomerID != null &&
             int.Parse( CurrentCustomerID ) >= 0)
        {
            Customer customer = DataAccessContext.CustomerRepository.GetOne( CurrentCustomerID );
            uxUserName.Text = customer.UserName;
            uxFirstName.Text = customer.BillingAddress.FirstName;
            uxLastName.Text = customer.BillingAddress.LastName;
            uxCompany.Text = customer.BillingAddress.Company;
            uxAddress1.Text = customer.BillingAddress.Address1;
            uxAddress2.Text = customer.BillingAddress.Address2;
            uxCity.Text = customer.BillingAddress.City;
            uxZip.Text = customer.BillingAddress.Zip;
            uxCountryList.CurrentSelected = customer.BillingAddress.Country;
            uxStateList.CountryCode = uxCountryList.CurrentSelected;
            uxStateList.Refresh();
            uxStateList.CurrentSelected = customer.BillingAddress.State;
            uxPhone.Text = customer.BillingAddress.Phone;
            uxFax.Text = customer.BillingAddress.Fax;
            uxEmail.Text = customer.Email;
            //uxIsWholesaleCheck.Checked = customer.IsWholesale;
            uxUseBillingAsShipping.Checked = customer.UseBillingAsShipping;

            PopulateShippingDetails( customer );

            uxIsTaxExemptCheck.Checked = customer.IsTaxExempt;
            uxTaxExemptID.Text = customer.TaxExemptID;
            uxTaxExemptCountryList.CurrentSelected = customer.TaxExemptCountry;
            uxTaxExemptStateList.CountryCode = uxTaxExemptCountryList.CurrentSelected;
            uxTaxExemptStateList.Refresh();
            uxTaxExemptStateList.CurrentSelected = customer.TaxExemptState;
            //uxIsEnabledCheck.Checked = customer.IsEnabled;
        }
        else
        {
            MainContext.RedirectMainControl( "OrderList.ascx" );
        }
    }

    private void PopulateShippingDetails( Customer customer )
    {
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
            uxShippingCountryList.CurrentSelected = shippingAddress.Country;
            uxShippingStateList.CountryCode = uxShippingCountryList.CurrentSelected;
            uxShippingStateList.Refresh();
            uxShippingStateList.CurrentSelected = shippingAddress.State;
            uxShippingPhone.Text = shippingAddress.Phone;
            uxShippingFax.Text = shippingAddress.Fax;
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
            uxShippingCountryList.CurrentSelected = String.Empty;
            uxShippingStateList.CountryCode = String.Empty;
            uxShippingStateList.Refresh();
            uxShippingStateList.CurrentSelected = String.Empty;
            uxShippingPhone.Text = String.Empty;
            uxShippingFax.Text = String.Empty;
        }
    }

    private void CheckUseBillingAsShipping()
    {
        uxPanelBillingAsShipping.CssClass = "dn";
        uxShippingFirstName.Text = uxFirstName.Text;
        uxShippingLastName.Text = uxLastName.Text;
        uxShippingCompany.Text = uxCompany.Text;
        uxShippingAddress1.Text = uxAddress1.Text;
        uxShippingAddress2.Text = uxAddress2.Text;
        uxShippingCity.Text = uxCity.Text;

        uxShippingZip.Text = uxZip.Text;
        uxShippingCountryList.CurrentSelected = uxCountryList.CurrentSelected;
        uxShippingStateList.CountryCode = uxShippingCountryList.CurrentSelected;
        uxShippingStateList.Refresh();
        uxShippingStateList.CurrentSelected = uxStateList.CurrentSelected;

        uxShippingPhone.Text = uxPhone.Text;
        uxShippingFax.Text = uxFax.Text;

        uxShippingFirstName.Enabled = false;
        uxShippingLastName.Enabled = false;
        uxShippingCompany.Enabled = false;
        uxShippingAddress1.Enabled = false;
        uxShippingAddress2.Enabled = false;
        uxShippingCity.Enabled = false;
        uxShippingStateList.SetEnable( false );
        uxShippingZip.Enabled = false;
        uxShippingCountryList.SetEnable( false );
        uxShippingPhone.Enabled = false;
        uxShippingFax.Enabled = false;

        uxRequiredShippingAddressValidator.EnableClientScript = false;
        uxRequiredShippingAddressValidator.Enabled = false;
        uxRequiredShippingCityValidator.EnableClientScript = false;
        uxRequiredShippingCityValidator.Enabled = false;
        uxRequiredShippingFirstnameValidator.EnableClientScript = false;
        uxRequiredShippingFirstnameValidator.Enabled = false;
        uxRequiredShippingLastNameValidator.EnableClientScript = false;
        uxRequiredShippingLastNameValidator.Enabled = false;
        uxRequiredShippingZipValidator.EnableClientScript = false;
        uxRequiredShippingZipValidator.Enabled = false;

        uxShippingAddressDrop.SelectedValue = "0";
        Customer customer = DataAccessContext.CustomerRepository.GetOne( CurrentCustomerID );
        foreach (ShippingAddress address in customer.ShippingAddresses)
        {
            if (address.IsSameAsBillingAddress)
                uxShippingAddressDrop.SelectedValue = address.ShippingAddressID;
        }
    }

    private void UnCheckUseBillingAsShipping()
    {
        uxPanelBillingAsShipping.CssClass = "";
        uxShippingFirstName.Enabled = true;
        uxShippingLastName.Enabled = true;
        uxShippingCompany.Enabled = true;
        uxShippingAddress1.Enabled = true;
        uxShippingAddress2.Enabled = true;
        uxShippingCity.Enabled = true;
        uxShippingStateList.SetEnable( true );
        uxShippingZip.Enabled = true;
        uxShippingCountryList.SetEnable( true );
        uxShippingPhone.Enabled = true;
        uxShippingFax.Enabled = true;

        uxRequiredShippingAddressValidator.EnableClientScript = true;
        uxRequiredShippingAddressValidator.Enabled = true;
        uxRequiredShippingCityValidator.EnableClientScript = true;
        uxRequiredShippingCityValidator.Enabled = true;
        uxRequiredShippingFirstnameValidator.EnableClientScript = true;
        uxRequiredShippingFirstnameValidator.Enabled = true;
        uxRequiredShippingLastNameValidator.EnableClientScript = true;
        uxRequiredShippingLastNameValidator.Enabled = true;
        uxRequiredShippingZipValidator.EnableClientScript = true;
        uxRequiredShippingZipValidator.Enabled = true;
    }

    private void DisplayTaxExemptControl()
    {
        if (uxIsTaxExemptCheck.Checked)
        {
            uxCustomerTaxExemptPanel.Visible = true;
            uxRequiredTaxExemptIDValidator.Enabled = true;
        }
        else
        {
            uxCustomerTaxExemptPanel.Visible = false;
            uxRequiredTaxExemptIDValidator.Enabled = false;
        }
    }


    private void ShowHideSaleTaxExempt()
    {
        if (IsSaleTaxExemptVisible())
        {
            if (uxIsTaxExemptCheck.Checked)
            {
                uxCustomerTaxExemptPanel.Visible = true;
                uxRequiredTaxExemptIDValidator.Enabled = true;
            }
            else
            {
                uxCustomerTaxExemptPanel.Visible = false;
                uxRequiredTaxExemptIDValidator.Enabled = false;
            }
        }
        else
        {
            uxIsTaxExemptPanel.Visible = false;
            uxCustomerTaxExemptPanel.Visible = false;
            uxRequiredTaxExemptIDValidator.Enabled = false;
        }
    }

    private void ShowHideShippingResidential()
    {
        ShippingPolicy shippingPolicy = new ShippingPolicy();
        if (shippingPolicy.RequiresResidentialStatus())
        {
            uxShippingResidentialPanel.Visible = true;
        }
        else
        {
            uxShippingResidentialPanel.Visible = false;
        }
    }

    private void SetSaleTaxExempt()
    {
        if (IsSaleTaxExemptVisible() && uxIsTaxExemptCheck.Checked)
        {
            StoreContext.CheckoutDetails.SetTaxExempt( true, uxTaxExemptID.Text,
                uxTaxExemptCountryList.CurrentSelected, uxTaxExemptStateList.CurrentSelected );
        }
        else
        {
            StoreContext.CheckoutDetails.SetTaxExempt( false, String.Empty, String.Empty, String.Empty );
        }
    }

    protected void uxChangeShippingAddrButton_Click( object sender, EventArgs e )
    {
        uxSelectShippingMethodButton.Visible = true;
        uxChangeShippingAddrButton.Visible = false;
        uxShippingListPanel.Visible = false;
        //uxAddressPanel.Enabled = true;
    }

    protected void uxSelectShippingMethodButton_Click( object sender, EventArgs e )
    {
        if (!SetCoupon())
            return;

        SetBillingAddress();
        SetShippingAddress();
        uxSelectShippingList.StoreID = SelectedStoreID;
        uxSelectShippingList.CurrencyCode = uxCurrencyControl.CurrentCurrencyCode;
        uxSelectShippingList.CultureID = uxLanguageControl.CurrentCultureID;
        uxSelectShippingList.PopulateControls();
        uxShippingListPanel.Visible = true;
        uxSelectShippingMethodButton.Visible = false;
        uxChangeShippingAddrButton.Visible = true;
        //uxAddressPanel.Enabled = false;
    }


    protected void uxSelectPaymentMethodButton_Click( object sender, EventArgs e )
    {
        uxPaymentNote.Visible = false;
        if (!SetShippingCouponGiftDetails())
        {
            return;
        }
        if (StoreContext.GetOrderAmount().Total <= 0)
        {
            uxPaymentNote.Visible = true;
            uxPaymentListPanel.Visible = false;
            uxChangeCheckOutDatailsButton.Visible = true;
            uxSelectPaymentMethodButton.Visible = false;
        }
        else
        {
            uxSelectPaymentList.CultureID = uxLanguageControl.CurrentCultureID;
            uxSelectPaymentList.PopulateControls();
            uxPaymentListPanel.Visible = true;
            uxChangeCheckOutDatailsButton.Visible = true;
            uxSelectPaymentMethodButton.Visible = false;
        }
    }

    protected void uxChangeCheckOutDatailsButton_Click( object sender, EventArgs e )
    {
        uxPaymentNote.Visible = false;
        uxPaymentListPanel.Visible = false;
        uxChangeCheckOutDatailsButton.Visible = false;
        uxSelectPaymentMethodButton.Visible = true;
    }

    protected void uxUseBillingAsShipping_CheckedChanged( object sender, EventArgs e )
    {
        if (uxUseBillingAsShipping.Checked == true)
        {
            CheckUseBillingAsShipping();
        }
        else
        {
            UnCheckUseBillingAsShipping();
        }

        Customer customer = DataAccessContext.CustomerRepository.GetOne( CurrentCustomerID );
        PopulateShippingDetails( customer );
    }

    protected void uxIsTaxExemptCheck_CheckedChanged( object sender, EventArgs e )
    {
        DisplayTaxExemptControl();
    }

    protected void uxShippingAliasNameDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        Customer customer = DataAccessContext.CustomerRepository.GetOne( CurrentCustomerID );
        PopulateShippingDetails( customer );
    }

    private bool SetPaymentMethod( out string errMsg )
    {
        errMsg = String.Empty;
        if (!uxSelectPaymentList.GetSelectedPaymentMethod().IsNull)
        {
            StoreContext.CheckoutDetails.SetPaymentMethod( uxSelectPaymentList.GetSelectedPaymentMethod() );
            return true;
        }
        else
        {
            errMsg = "Please select payment method.";
            return false;
        }
    }

    private bool SetShippingMethod( out string errMsg )
    {
        errMsg = String.Empty;
        if ((StoreContext.ShoppingCart.GetCartItems().Length > 0 &&
           !StoreContext.ShoppingCart.ContainsShippableItem()) ||
           !DataAccessContext.Configurations.GetBoolValue( "ShippingAddressMode" ))
        {
            StoreContext.CheckoutDetails.SetShipping( ShippingMethod.Null );
            return true;
        }

        if (!uxSelectShippingList.GetSelectedShippingMethod().IsNull)
        {
            StoreContext.CheckoutDetails.ShippingMethod = uxSelectShippingList.GetSelectedShippingMethod();
            return true;
        }
        else
        {
            errMsg = "Please select shipping method.";
            return false;
        }
    }

    private bool SetCoupon()
    {
        bool isValid;

        string couponCode = uxCouponGiftDetails.GetCouponCode( out isValid );
        if (!isValid)
            return false;

        StoreContext.CheckoutDetails.SetCouponID( couponCode );

        return isValid;
    }

    private bool SetShippingCouponGiftDetails()
    {
        string errMsg;
        bool isValid;

        if (!SetCoupon())
            return false;

        string giftCode = uxCouponGiftDetails.GetGiftCertificateCode( out isValid );
        if (!isValid)
            return false;


        StoreContext.CheckoutDetails.SetGiftCertificate( giftCode );

        if (DataAccessContext.Configurations.GetBoolValue( "PointSystemEnabled", CurrentStore ))
        {
            string rewardPoint = uxCouponGiftDetails.GetRewardPointValue( out isValid );
            if (!isValid)
                return false;

            StoreContext.CheckoutDetails.RedeemPrice = uxCouponGiftDetails.GetPriceFromPoint( ConvertUtilities.ToDecimal( rewardPoint ) );
            StoreContext.CheckoutDetails.RedeemPoint = ConvertUtilities.ToInt32( rewardPoint );
        }

        SetBillingAddress();
        SetShippingAddress();

        if (!SetShippingMethod( out errMsg ))
        {
            return false;
        }

        return true;
    }

    protected void uxNextButton_Click( object sender, EventArgs e )
    {
        string errMsg;

        if (!SetShippingCouponGiftDetails())
            return;

        StoreContext.CheckoutDetails.SetCustomerComments( uxCustomerComments.Text );
        StoreContext.CheckoutDetails.IsCreatedByAdmin = true;

        if (!uxSelectPaymentList.SetPayment( out errMsg ))
        {
            uxMessage.DisplayError( errMsg );
            return;
        }

        SetSaleTaxExempt();

        if (StoreContext.GetOrderAmount().Total <= 0)
        {
            MainContext.RedirectMainControl( "OrderCreateCheckOutSummary.ascx",
                String.Format( "StoreID={0}&CurrencyCode={1}", SelectedStoreID, uxCurrencyControl.CurrentCurrencyCode ) );
        }
        else
        {
            MainContext.RedirectMainControl( "OrderCreatePaymentDetails.ascx",
                   String.Format( "StoreID={0}&CurrencyCode={1}", SelectedStoreID, uxCurrencyControl.CurrentCurrencyCode ) );
        }
    }
}

