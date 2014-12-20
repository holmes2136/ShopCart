using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Marketing;
using Vevo.Domain.Shipping;
using Vevo.Domain.Users;
using Vevo.Shared.Utilities;
using Vevo.Base.Domain;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.GiftRegistry;
using Vevo.Shared.DataAccess;

public partial class Components_GiftRegistryDetail : Vevo.WebUI.International.BaseLanguageUserControl
{
    #region Private

    private enum Mode { Add, Edit };

    private Mode _mode = Mode.Add;

    private string GiftRegistryID
    {
        get
        {
            return Request.QueryString["GiftRegistryID"];
        }
    }

    private void PopulateUserAddress()
    {
        uxEventDateCalendarPopup.SelectedDate = DateTime.Now;

        string customerID = DataAccessContext.CustomerRepository.GetIDFromUserName(
            Page.User.Identity.Name );
        Customer customer = DataAccessContext.CustomerRepository.GetOne( customerID );
        uxCompany.Text = customer.ShippingAddress.Company;
        uxAddress1.Text = customer.ShippingAddress.Address1;
        uxAddress2.Text = customer.ShippingAddress.Address2;
        uxCity.Text = customer.ShippingAddress.City;
        uxZip.Text = customer.ShippingAddress.Zip;
        uxCountryState.CurrentCountry = customer.ShippingAddress.Country;
        uxCountryState.CurrentState = customer.ShippingAddress.State;
        uxPhone.Text = customer.ShippingAddress.Phone;
        uxFax.Text = customer.ShippingAddress.Fax;
        uxResidentialDrop.SelectedValue = customer.ShippingAddress.Residential.ToString();
    }

    private void PopulateControls()
    {
        GiftRegistry giftRegistry = DataAccessContextDeluxe.GiftRegistryRepository.GetOne( GiftRegistryID );
        uxEventName.Text = giftRegistry.EventName;
        uxEventDateCalendarPopup.SelectedDate = giftRegistry.EventDate;
        uxCompany.Text = giftRegistry.ShippingAddress.Company;
        uxAddress1.Text = giftRegistry.ShippingAddress.Address1;
        uxAddress2.Text = giftRegistry.ShippingAddress.Address2;
        uxCity.Text = giftRegistry.ShippingAddress.City;
        uxZip.Text = giftRegistry.ShippingAddress.Zip;
        uxCountryState.CurrentCountry = giftRegistry.ShippingAddress.Country;
        uxCountryState.CurrentState = giftRegistry.ShippingAddress.State;
        uxPhone.Text = giftRegistry.ShippingAddress.Phone;
        uxFax.Text = giftRegistry.ShippingAddress.Fax;
        uxResidentialDrop.SelectedValue = giftRegistry.ShippingAddress.Residential.ToString();
        uxHideAddressCheck.Checked = giftRegistry.HideAddress;
        uxHideEventCheck.Checked = giftRegistry.HideEvent;
        uxNotifyNewOrderCheck.Checked = giftRegistry.NotifyNewOrder;
    }

    private void Redirect()
    {
        if (IsEditMode())
            Response.Redirect( "GiftRegistryList.aspx" );
        else
            Response.Redirect( "GiftRegistryComplete.aspx" );
    }

    private GiftRegistry SetUpGiftRegistry( GiftRegistry giftRegistry )
    {
        string customerID = DataAccessContext.CustomerRepository.GetIDFromUserName( Page.User.Identity.Name );
        Customer customer = DataAccessContext.CustomerRepository.GetOne( customerID );
        giftRegistry.EventName = uxEventName.Text;
        giftRegistry.EventDate = uxEventDateCalendarPopup.SelectedDate;
        giftRegistry.CustomerID = customerID;
        giftRegistry.UserName = customer.UserName;
        giftRegistry.ShippingAddress = new ShippingAddress( new Address(
            customer.BillingAddress.FirstName, customer.BillingAddress.LastName,
            uxCompany.Text, uxAddress1.Text, uxAddress2.Text, uxCity.Text,
            uxCountryState.CurrentState, uxZip.Text,
            uxCountryState.CurrentCountry, uxPhone.Text, uxFax.Text ),
            ConvertUtilities.ToBoolean( uxResidentialDrop.SelectedValue ) );
        giftRegistry.HideAddress = uxHideAddressCheck.Checked;
        giftRegistry.HideEvent = uxHideEventCheck.Checked;
        giftRegistry.NotifyNewOrder = uxNotifyNewOrderCheck.Checked;
        giftRegistry.StoreID = DataAccessContext.StoreRetriever.GetCurrentStoreID();
        return giftRegistry;
    }

    private void AddNewAndRedirect()
    {
        if (Page.IsValid)
        {
            bool validateCountry, validateState;
            if (!uxCountryState.Validate( out validateCountry, out validateState ))
            {
                uxSummaryLiteral.Text = uxCountryState.FormatErrorHtml( "Please correct the following errors:", validateCountry, validateState );
                return;
            }

            GiftRegistry giftRegistry = new GiftRegistry();
            giftRegistry = SetUpGiftRegistry( giftRegistry );
            giftRegistry = DataAccessContextDeluxe.GiftRegistryRepository.Save( giftRegistry );
            string giftRegistryID = giftRegistry.GiftRegistryID;

            Redirect();
        }
    }

    private void EditAndRedirect()
    {
        if (Page.IsValid)
        {
            bool validateCountry, validateState;
            if (!uxCountryState.Validate( out validateCountry, out validateState ))
            {
                uxSummaryLiteral.Text = uxCountryState.FormatErrorHtml( "Please correct the following errors:", validateCountry, validateState );
                return;
            }

            GiftRegistry giftRegistry = DataAccessContextDeluxe.GiftRegistryRepository.GetOne( GiftRegistryID );
            giftRegistry = SetUpGiftRegistry( giftRegistry );
            DataAccessContextDeluxe.GiftRegistryRepository.Save( giftRegistry );

            Redirect();
        }
    }

    private void ShowHideShippingResidential()
    {
        ShippingPolicy shippingPolicy = new ShippingPolicy();
        if (shippingPolicy.RequiresResidentialStatus())
        {
            uxResidentialLabelDiv.Visible = true;
            uxResidentialDataDiv.Visible = true;
        }
        else
        {
            uxResidentialLabelDiv.Visible = false;
            uxResidentialDataDiv.Visible = false;
        }
    }

    #endregion


    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName ))
        {
            this.Visible = false;
            return;
        }

        if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Test)
            uxEventDateCalendarPopup.SelectedDate = DateTime.Today.AddMonths( 1 );

        if (IsEditMode())
        {
            if (!IsPostBack)
            {
                ShowHideShippingResidential();
                PopulateControls();
                uxAddLinkButton.Visible = false;
                uxEditLinkButton.Visible = true;
            }
        }
        else
        {
            if (!IsPostBack)
            {
                PopulateUserAddress();
                uxHideEventCheck.Checked = true;
                uxNotifyNewOrderCheck.Checked = true;
            }
            uxAddLinkButton.Visible = true;
            uxEditLinkButton.Visible = false;
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
    }

    protected void uxAddLinkButton_Click( object sender, EventArgs e )
    {
        AddNewAndRedirect();
    }

    protected void uxEditLinkButton_Click( object sender, EventArgs e )
    {
        EditAndRedirect();
    }

    #endregion


    #region Public Methods

    public bool IsEditMode()
    {
        return (_mode == Mode.Edit);
    }

    public void SetEditMode()
    {
        _mode = Mode.Edit;
    }

    #endregion

}
