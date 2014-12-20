using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Stores;
using Vevo.Domain.Users;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.Base.Domain;
using System.Web.UI.WebControls;

public partial class AdminAdvanced_Components_CustomerDetails : AdminAdvancedBaseUserControl
{
    private enum Mode { Add, Edit };

    private Mode _mode = Mode.Add;

    private int WholesaleLevel
    {
        get
        {
            return int.Parse( DataAccessContext.Configurations.GetValue( "WholesaleLevel" ) );
        }
    }

    private string CurrentID
    {
        get
        {
            return MainContext.QueryString["CustomerID"];
        }
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

    private void uxTaxExemptState_RefreshHandler( object sender, EventArgs e )
    {
        uxTaxExemptStateList.CountryCode = uxTaxExemptCountryList.CurrentSelected;
        uxTaxExemptStateList.Refresh();
    }

    private bool IsTaxExemptEnabled()
    {
        IList<Store> storeList = DataAccessContext.StoreRepository.GetAll( "" );

        foreach (Store store in storeList)
        {
            bool isTaxExempt = DataAccessContext.Configurations.GetBoolValue( "SaleTaxExempt", store );


            if (isTaxExempt)
            {
                return true;
            }
        }

        uxIsTaxExemptLabel.Visible = false;
        uxIsTaxExemptCheck.Visible = false;
        uxCustomerTaxExemptPanel.Visible = false;
        uxRequiredTaxExemptIDValidator.Enabled = false;
        return false;
    }

    private void LoadStoreGrid()
    {
        IList<Store> storeList = DataAccessContext.StoreRepository.GetAll("StoreID");
        uxStoreGrid.DataSource = storeList;
        uxStoreGrid.DataBind();

        if (!KeyUtilities.IsMultistoreLicense())
            uxStoreListDiv.Visible = false;
    }

    private CheckBox GetUseOptionCheck(GridViewRow row)
    {
        return (CheckBox)row.FindControl("uxEnableStoreCheck");
    }

    private void PopulateStoreGrid(Customer customer)
    {
        IList<string> storeList = customer.StoreIDs;
        foreach (string storeID in storeList)
        {
            for (int i = 0; i < uxStoreGrid.Rows.Count; i++)
            {
                string rowStoreID = uxStoreGrid.DataKeys[i].Value.ToString();
                if (storeID == rowStoreID)
                {
                    GridViewRow row = uxStoreGrid.Rows[i];
                    CheckBox checkbox = GetUseOptionCheck(row);
                    checkbox.Checked = true;
                }
            }
        }
    }

    private string[] GetStoreEnableListFromGrid()
    {
        List<string> storeList = new List<string>();
        for (int i = 0; i < uxStoreGrid.Rows.Count; i++)
        {
            GridViewRow row = uxStoreGrid.Rows[i];
            CheckBox checkbox = GetUseOptionCheck(row);
            if (checkbox != null)
            {
                if (checkbox.Checked)
                    storeList.Add(uxStoreGrid.DataKeys[i].Value.ToString());
            }
        }
        return storeList.ToArray();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxCountryList.BubbleEvent += new EventHandler( uxState_RefreshHandler );
        uxShippingCountryList.BubbleEvent += new EventHandler( uxShippingState_RefreshHandler );
        uxTaxExemptCountryList.BubbleEvent += new EventHandler( uxTaxExemptState_RefreshHandler );
        DisplaySendEmailButtonControl();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            LoadStoreGrid();
            if (IsEditMode())
            {
                PopulateControls();
                PaasswordTR.Visible = false;
                uxUserName.Enabled = false;
                uxAddButton.Visible = false;
                uxAddSendMailButton.Visible = false;

                uxUseBillingAsShippingPanel.Visible = false;
                uxPanelBillingAsShipping.Visible = false;
                if (IsAdminModifiable())
                {
                    uxUpdateButton.Visible = true;
                }
                else
                {
                    uxUpdateButton.Visible = false;
                }
            }
            else
            {
                uxRegisterDateCalendarPopup.SelectedDate = DateTime.Now;

                if (IsAdminModifiable())
                {
                    uxAddButton.Visible = true;
                    uxAddSendMailButton.Visible = true;
                    uxUpdateButton.Visible = false;
                    uxUpdateSendMailButton.Visible = false;
                }
                else
                {
                    MainContext.RedirectMainControl( "CustomerList.ascx" );
                }
            }

            if (IsTaxExemptEnabled())
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

            if (int.Parse( DataAccessContext.Configurations.GetValue( "WholesaleMode" ) ) == 0)
            {
                IsWholesaleTR.Visible = false;
                WholesaleLevelsTR.Visible = false;
            }
            else
            {
                IsWholesaleTR.Visible = true;
                UpdateWholesaleLevelsControls();

                switch (WholesaleLevel)
                {
                    case 1:
                        uxWholesaleLevelsDrop.Items.RemoveAt( 3 );
                        uxWholesaleLevelsDrop.Items.RemoveAt( 2 );
                        break;
                    case 2:
                        uxWholesaleLevelsDrop.Items.RemoveAt( 3 );
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void PopulateControls()
    {
        if (CurrentID != null &&
             int.Parse( CurrentID ) >= 0)
        {
            Customer customer = DataAccessContext.CustomerRepository.GetOne( CurrentID );
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
            uxIsWholesaleCheck.Checked = customer.IsWholesale;
            uxUseBillingAsShipping.Checked = customer.UseBillingAsShipping;

            uxMerchantNotes.Text = customer.MerchantNotes;
            uxRegisterDateCalendarPopup.SelectedDate = customer.RegisterDate;

            uxWholesaleLevelsDrop.SelectedValue = customer.WholesaleLevel.ToString();
            UpdateWholesaleLevelsControls();
            uxIsTaxExemptCheck.Checked = customer.IsTaxExempt;
            uxTaxExemptID.Text = customer.TaxExemptID;
            uxTaxExemptCountryList.CurrentSelected = customer.TaxExemptCountry;
            uxTaxExemptStateList.CountryCode = uxTaxExemptCountryList.CurrentSelected;
            uxTaxExemptStateList.Refresh();
            uxTaxExemptStateList.CurrentSelected = customer.TaxExemptState;
            uxIsEnabledCheck.Checked = customer.IsEnabled;

            PopulateStoreGrid(customer);
        }
    }

    private void ClearInputFields()
    {
        uxUserName.Text = "";
        uxPasswordText.Text = "";
        uxFirstName.Text = "";
        uxLastName.Text = "";
        uxCompany.Text = "";
        uxAddress1.Text = "";
        uxAddress2.Text = "";
        uxCity.Text = "";
        uxStateList.CurrentSelected = "";
        uxZip.Text = "";
        uxCountryList.CurrentSelected = "";
        uxPhone.Text = "";
        uxFax.Text = "";
        uxEmail.Text = "";
        uxIsWholesaleCheck.Checked = false;
        uxShippingFirstName.Text = "";
        uxShippingLastName.Text = "";
        uxShippingCompany.Text = "";
        uxShippingAddress1.Text = "";
        uxShippingAddress2.Text = "";
        uxShippingCity.Text = "";
        uxShippingStateList.CurrentSelected = "";
        uxShippingZip.Text = "";
        uxShippingCountryList.CurrentSelected = "";
        uxShippingPhone.Text = "";
        uxShippingFax.Text = "";
        uxMerchantNotes.Text = "";
        uxIsTaxExemptCheck.Checked = false;
        uxTaxExemptID.Text = "";
        uxTaxExemptCountryList.CurrentSelected = "";
        uxTaxExemptStateList.CurrentSelected = "";
        uxIsEnabledCheck.Checked = true;

        WholesaleLevelsTR.Visible = false;
        uxWholesaleLevelsDrop.SelectedIndex = 0;
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
        uxShippingStateList.CurrentSelected = uxStateList.CurrentSelected;
        uxShippingZip.Text = uxZip.Text;
        uxShippingCountryList.CurrentSelected = uxCountryList.CurrentSelected;
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

    private void SetShippingDetailsBlank()
    {
        uxShippingFirstName.Text = "";
        uxShippingLastName.Text = "";
        uxShippingCompany.Text = "";
        uxShippingAddress1.Text = "";
        uxShippingAddress2.Text = "";
        uxShippingCity.Text = "";
        uxShippingStateList.CurrentSelected = "";
        uxShippingZip.Text = "";
        uxShippingCountryList.CurrentSelected = "";
        uxShippingPhone.Text = "";
        uxShippingFax.Text = "";
    }

    private void UpdateWholesaleLevelsControls()
    {
        if (uxIsWholesaleCheck.Checked)
            WholesaleLevelsTR.Visible = true;
        else
            WholesaleLevelsTR.Visible = false;
    }

    private Customer SetUpCustomer( Customer customer )
    {
        customer.RegisterDate = uxRegisterDateCalendarPopup.SelectedDate;
        customer.UserName = uxUserName.Text;
        customer.BillingAddress = new Address( uxFirstName.Text, uxLastName.Text, uxCompany.Text,
            uxAddress1.Text, uxAddress2.Text, uxCity.Text, uxStateList.CurrentSelected, uxZip.Text,
            uxCountryList.CurrentSelected, uxPhone.Text, uxFax.Text );
        customer.Email = uxEmail.Text;
        customer.UseBillingAsShipping = uxUseBillingAsShipping.Checked;


        if (!IsEditMode())
        {
            ShippingAddress shippingAddress = new ShippingAddress( new Address(
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
                uxShippingFax.Text ),
                false );

            shippingAddress.AliasName = uxShippingFirstName.Text + " " + uxShippingLastName.Text + " " + uxShippingAddress1.Text;
            if (shippingAddress.AliasName.Length > 50)
                shippingAddress.AliasName = shippingAddress.AliasName.Substring( 0, 50 );

            if (uxUseBillingAsShipping.Checked)
            {
                shippingAddress.IsSameAsBillingAddress = true;
            }
            else
            {
                shippingAddress.IsSameAsBillingAddress = false;
            }

            customer.ShippingAddresses.Add( shippingAddress );
        }

        customer.MerchantNotes = uxMerchantNotes.Text;
        customer.IsWholesale = uxIsWholesaleCheck.Checked;
        customer.WholesaleLevel = ConvertUtilities.ToInt32( uxWholesaleLevelsDrop.SelectedValue );
        customer.IsTaxExempt = uxIsTaxExemptCheck.Checked;
        if (uxIsTaxExemptCheck.Checked)
        {
            customer.TaxExemptID = uxTaxExemptID.Text;
            customer.TaxExemptCountry = uxTaxExemptCountryList.CurrentSelected;
            customer.TaxExemptState = uxTaxExemptStateList.CurrentSelected;
        }
        else
        {
            customer.TaxExemptID = String.Empty;
            customer.TaxExemptCountry = String.Empty;
            customer.TaxExemptState = String.Empty;
        }
        customer.IsEnabled = uxIsEnabledCheck.Checked;

        customer.StoreIDs = GetStoreEnableListFromGrid();
        return customer;
    }

    private void DisplaySendEmailButtonControl()
    {
        if (IsEditMode())
        {
            if (IsAdminModifiable())
            {
                if (uxIsEnabledCheck.Checked)
                    uxUpdateSendMailButton.Visible = true;
                else
                    uxUpdateSendMailButton.Visible = false;
            }
            else
            {
                uxUpdateSendMailButton.Visible = false;
            }
        }
        else
        {
            if (uxIsEnabledCheck.Checked)
                uxAddSendMailButton.Visible = true;
            else
                uxAddSendMailButton.Visible = false;
        }
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

    private void Update()
    {
        try
        {
            if (uxUseBillingAsShipping.Checked)
                CheckUseBillingAsShipping();

            if (Page.IsValid)
            {
                Customer customer = DataAccessContext.CustomerRepository.GetOne( CurrentID );
                customer = SetUpCustomer( customer );
                customer = UpdateShippingAddress( customer );
                customer = DataAccessContext.CustomerRepository.Save( customer );

                PopulateControls();
                uxMessage.DisplayMessage( Resources.CustomerMessages.UpdateSuccess );
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    private Customer UpdateShippingAddress( Customer customer )
    {
        for (int i = 0; i < customer.ShippingAddresses.Count; i++)
        {
            if (customer.ShippingAddresses[i].IsSameAsBillingAddress)
            {
                string id = customer.ShippingAddresses[i].ShippingAddressID;
                string aliasName = customer.ShippingAddresses[i].AliasName;
                bool residentialValue = customer.ShippingAddresses[i].Residential;
                customer.ShippingAddresses[i] = new ShippingAddress( customer.BillingAddress, residentialValue );
                customer.ShippingAddresses[i].ShippingAddressID = id;
                customer.ShippingAddresses[i].AliasName = aliasName;
                customer.ShippingAddresses[i].IsSameAsBillingAddress = true;
            }
        }

        return customer;
    }

    private void SendMail()
    {
        string subjectMail;
        string bodyMail;

        EmailTemplateTextVariable.ReplaceCustomerApproveText(
            uxUserName.Text, uxEmail.Text, out subjectMail, out bodyMail );

        WebUtilities.SendHtmlMail(
            NamedConfig.CompanyEmail,
            uxEmail.Text,
            subjectMail,
            bodyMail );
        uxMessage.DisplayMessage( Resources.CustomerMessages.SendApproveMailSuccess );
    }

    public bool IsEditMode()
    {
        return (_mode == Mode.Edit);
    }

    public void SetEditMode()
    {
        _mode = Mode.Edit;
    }

    protected void uxPasswordText_PreRender( object sender, EventArgs e )
    {
        // Retain password value across postback
        uxPasswordText.Attributes["value"] = uxPasswordText.Text;
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        try
        {
            if (Page.IsValid)
            {
                string userID = DataAccessContext.CustomerRepository.GetIDFromUserName( uxUserName.Text );
                if (userID != "0")
                {
                    uxMessage.DisplayError( Resources.CustomerMessages.UserNameExistedError );
                    return;
                }

                if (uxUseBillingAsShipping.Checked)
                    CheckUseBillingAsShipping();

                Customer customer = new Customer();
                customer = SetUpCustomer( customer );
                customer = DataAccessContext.CustomerRepository.Save( customer );
                string newCustomerID = customer.CustomerID;

                Membership.CreateUser( uxUserName.Text, uxPasswordText.Text, uxEmail.Text.Trim() );
                Roles.AddUserToRole( uxUserName.Text, "Customers" );

                uxMessage.DisplayMessage( Resources.CustomerMessages.AddSuccess );

                ClearInputFields();
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    protected void uxAddSendMailButton_Click( object sender, EventArgs e )
    {
        try
        {
            if (Page.IsValid)
            {
                string userID = DataAccessContext.CustomerRepository.GetIDFromUserName( uxUserName.Text );
                if (userID != "0")
                {
                    uxMessage.DisplayError( Resources.CustomerMessages.UserNameExistedError );
                    return;
                }

                if (uxUseBillingAsShipping.Checked)
                    CheckUseBillingAsShipping();

                Membership.CreateUser( uxUserName.Text, uxPasswordText.Text, uxEmail.Text.Trim() );
                Roles.AddUserToRole( uxUserName.Text, "Customers" );

                Customer customer = new Customer();
                customer = SetUpCustomer( customer );
                customer = DataAccessContext.CustomerRepository.Save( customer );
                string newCustomerID = customer.CustomerID;
                SendMail();

                uxMessage.DisplayMessage( Resources.CustomerMessages.AddSuccess );

                ClearInputFields();
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    protected void uxUpdateButton_Click( object sender, EventArgs e )
    {
        Update();
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
    }

    protected void uxIsWholesaleCheck_CheckedChanged( object sender, EventArgs e )
    {
        UpdateWholesaleLevelsControls();
    }

    protected void uxIsEnabledCheck_CheckedChanged( object sender, EventArgs e )
    {
        DisplaySendEmailButtonControl();
    }

    protected void uxIsTaxExemptCheck_CheckedChanged( object sender, EventArgs e )
    {
        DisplayTaxExemptControl();
    }

    protected void uxUpdateSendMailButton_Click( object sender, EventArgs e )
    {
        try
        {
            Update();
            SendMail();
            uxMessage.DisplayMessage( Resources.CustomerMessages.UpdateSendApproveMailSuccess );
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }
}
